using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Threading;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000015 RID: 21
	internal static class LocalClientUtil
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000279C File Offset: 0x0000099C
		private static HashSet<IPAddress> LocalIPAddresses
		{
			get
			{
				HashSet<IPAddress> hashSet = LocalClientUtil.localIPAddresses;
				if (hashSet == null)
				{
					bool flag = false;
					hashSet = new HashSet<IPAddress>();
					try
					{
						foreach (IPAddress ipaddress in Dns.GetHostEntry(Environment.MachineName).AddressList)
						{
							hashSet.Add(ipaddress);
						}
					}
					catch (ManagementException)
					{
						flag = true;
					}
					try
					{
						foreach (IPAddress ipaddress2 in Dns.GetHostEntry("localhost").AddressList)
						{
							hashSet.Add(ipaddress2);
						}
					}
					catch (ManagementException)
					{
						flag = true;
					}
					if (flag)
					{
						return hashSet;
					}
					Interlocked.CompareExchange<HashSet<IPAddress>>(ref LocalClientUtil.localIPAddresses, hashSet, null);
				}
				return LocalClientUtil.localIPAddresses;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002858 File Offset: 0x00000A58
		internal static bool IsLocalIPAddressOrHostName(string host)
		{
			if (WebUtil.IsWellKnownLocalServer(host))
			{
				return true;
			}
			bool flag;
			try
			{
				HashSet<IPAddress> addresses = LocalClientUtil.LocalIPAddresses;
				flag = Dns.GetHostEntry(host).AddressList.Any((IPAddress a) => addresses.Contains(a));
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000044 RID: 68
		private static HashSet<IPAddress> localIPAddresses;
	}
}
