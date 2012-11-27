﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using GridMapper.NetworkModelObject;
using GridMapper.NetworkRepository;
using System.Net.NetworkInformation;

namespace GridMapper
{
	class Execution : IExecution
	{
		Option _option;
		IRepository _repository;


		#region IExecution Membres

		public Option Option
		{
			get { return _option; }
		}

		public IRepository Repository
		{
			get { return _repository; }
		}

		public void StartScan()
		{
			Task task1 = Task.Factory.StartNew( () =>
				{
					PingSender pingSender = new PingSender( Option );
					foreach( IPAddress ip in _option.IpToTest )
					{
						Task task2 = Task.Factory.StartNew( () =>
							{
								_repository.AddOrUpdate( ip, pingSender.Ping( ip ) );
							} );
					}
				} ).ContinueWith( (a) =>
					{
						ARPSender arpSender = new ARPSender();
						ReverseDnsResolver dnsResolver = new ReverseDnsResolver();
						foreach( IPAddress ip in _repository.GetIPAddresses() )
						{
							Task task2 = Task.Factory.StartNew( () =>
							{
								_repository.AddOrUpdate( ip, arpSender.GetMac( ip ) );
							} );
							Task task3 = Task.Factory.StartNew( () =>
							{
								_repository.AddOrUpdate( ip, dnsResolver.GetHostName( ip ) );
							} );
						}
					} );
		}

		#endregion

		public Execution( Option startupOptions )
		{
			_option = startupOptions;
			_repository = new Repository();
		}
	}
}
