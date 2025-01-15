using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000260 RID: 608
	public sealed class Network
	{
		// Token: 0x06001009 RID: 4105 RVA: 0x000372DC File Offset: 0x000354DC
		public static IEnumerable<IPAddress> GetLocalIpAddresses()
		{
			return Dns.GetHostEntry(string.Empty).AddressList.Where((IPAddress ip) => ip.AddressFamily == AddressFamily.InterNetwork || ip.AddressFamily == AddressFamily.InterNetworkV6).Materialize<IPAddress>();
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00037316 File Offset: 0x00035516
		public static IEnumerable<IPAddress> GetLocalIpv4Addresses()
		{
			return (from ip in Network.GetLocalIpAddresses()
				where ip.AddressFamily == AddressFamily.InterNetwork
				select ip).Materialize<IPAddress>();
		}
	}
}
