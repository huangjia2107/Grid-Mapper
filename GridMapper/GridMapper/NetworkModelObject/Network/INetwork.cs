﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridMapper.NetworkModelObject
{
	interface INetwork
	{
		IList<Host> NetworkHost{ get; set; }
	}
}
