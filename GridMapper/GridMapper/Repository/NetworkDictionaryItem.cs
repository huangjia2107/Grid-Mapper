using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GridMapper.NetworkModelObject;
using System.Net.NetworkInformation;
using System.Net;

namespace GridMapper.Repository
{
	internal class NetworkDictionaryItem : INetworkDictionaryItem
	{
		IHost _host;
		IHostEntity _hostEntity;
		PingReply _pingReply;
		IPAddress _ipAddress;
		PhysicalAddress _macAddress;
		IPHostEntry _hostEntry;
		bool[] _portStatus;

		public IHost Host { get { return _host; } }
		public IHostEntity HostEntity { get { return _hostEntity; } } //test si il est possible de créer un hostentity
		public PingReply PingReply { get { return _pingReply; } }
		public IPAddress IPAddress { get { return _ipAddress; } }
		public PhysicalAddress MacAddress { get { return _macAddress; } }
		public IPHostEntry HostEntry { get { return _hostEntry; } }
		public bool[] PortStatus { get { return _portStatus; } }

		//juste des idées
		public bool update( INetworkDictionaryItem networkDictionaryItem )
		{
			return true;
		}

		internal NetworkDictionaryItem()
		{
		}

		internal NetworkDictionaryItem(PingReply pingReply)
		{
			_ipAddress = pingReply.Address;
			_pingReply = pingReply;
		}
	}
}
