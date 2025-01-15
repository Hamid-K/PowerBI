using System;
using System.Collections.Generic;
using System.Management;
using System.Net;

namespace Microsoft.BIServer.Configuration.WMI
{
	// Token: 0x0200002F RID: 47
	internal sealed class RSInstance : RSWmiInstance, ICloneable
	{
		// Token: 0x0600019C RID: 412 RVA: 0x000066BD File Offset: 0x000048BD
		public RSInstance(ManagementObject mo, string machineName)
			: base(mo, machineName)
		{
			this.machineIPAddresses = null;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000066D0 File Offset: 0x000048D0
		public IPAddress[] MachineIPAddresses
		{
			get
			{
				if (this.machineIPAddresses == null)
				{
					List<IPAddress> list = new List<IPAddress>();
					list.AddRange(Dns.GetHostEntry(base.MachineName).AddressList);
					if (WmiProvider.IsWellKnownLocalServer(base.MachineName))
					{
						list.AddRange(Dns.GetHostEntry(Environment.MachineName).AddressList);
					}
					this.machineIPAddresses = list.ToArray();
				}
				return this.machineIPAddresses;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006735 File Offset: 0x00004935
		public object Clone()
		{
			return new RSInstance(this._internalObject.Clone() as ManagementObject, base.MachineName);
		}

		// Token: 0x04000135 RID: 309
		private IPAddress[] machineIPAddresses;
	}
}
