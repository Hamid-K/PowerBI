using System;
using System.Collections.Generic;
using System.Security;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Dns;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000009 RID: 9
	internal class AutodiscoverDnsClient
	{
		// Token: 0x0600001D RID: 29 RVA: 0x0000234E File Offset: 0x0000134E
		internal AutodiscoverDnsClient(AutodiscoverService service)
		{
			this.service = service;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002360 File Offset: 0x00001360
		internal string FindAutodiscoverHostFromSrv(string domain)
		{
			string text = "_autodiscover._tcp." + domain;
			DnsSrvRecord dnsSrvRecord = this.FindBestMatchingSrvRecord(text);
			if (dnsSrvRecord == null || string.IsNullOrEmpty(dnsSrvRecord.NameTarget))
			{
				this.service.TraceMessage(TraceFlags.AutodiscoverConfiguration, "No appropriate SRV record was found.");
				return null;
			}
			this.service.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("DNS query for SRV record for domain {0} found {1}", domain, dnsSrvRecord.NameTarget));
			return dnsSrvRecord.NameTarget;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002400 File Offset: 0x00001400
		private DnsSrvRecord FindBestMatchingSrvRecord(string domain)
		{
			List<DnsSrvRecord> list;
			try
			{
				list = DnsClient.DnsQuery<DnsSrvRecord>(domain, this.service.DnsServerAddress);
			}
			catch (DnsException ex)
			{
				string text = string.Format("DnsQuery returned error error '{0}' error code 0x{1:X8}.", ex.Message, ex.NativeErrorCode);
				this.service.TraceMessage(TraceFlags.AutodiscoverConfiguration, text);
				return null;
			}
			catch (SecurityException ex2)
			{
				this.service.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("DnsQuery cannot be called. Security error: {0}.", ex2.Message));
				return null;
			}
			this.service.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("{0} SRV records were returned.", list.Count));
			int priority = int.MinValue;
			int weight = int.MinValue;
			bool flag = false;
			foreach (DnsSrvRecord dnsSrvRecord in list)
			{
				if (dnsSrvRecord.Port == 443)
				{
					priority = dnsSrvRecord.Priority;
					weight = dnsSrvRecord.Weight;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.service.TraceMessage(TraceFlags.AutodiscoverConfiguration, "No appropriate SRV records were found.");
				return null;
			}
			List<DnsSrvRecord> list2 = list.FindAll((DnsSrvRecord record) => record.Port == 443 && record.Priority == priority && record.Weight == weight);
			EwsUtilities.Assert(list.Count > 0, "AutodiscoverDnsClient.FindBestMatchingSrvRecord", "At least one DNS SRV record must match the criteria.");
			int num = ((list2.Count > 1) ? AutodiscoverDnsClient.randomTieBreakerSelector.Next(list2.Count) : 0);
			DnsSrvRecord dnsSrvRecord2 = list2[num];
			string text2 = string.Format("Returning SRV record {0} of {1} records. Target: {2}, Priority: {3}, Weight: {4}", new object[] { num, list.Count, dnsSrvRecord2.NameTarget, dnsSrvRecord2.Priority, dnsSrvRecord2.Weight });
			this.service.TraceMessage(TraceFlags.AutodiscoverConfiguration, text2);
			return dnsSrvRecord2;
		}

		// Token: 0x0400000C RID: 12
		private const string AutoDiscoverSrvPrefix = "_autodiscover._tcp.";

		// Token: 0x0400000D RID: 13
		private const int SslPort = 443;

		// Token: 0x0400000E RID: 14
		private static Random randomTieBreakerSelector = new Random();

		// Token: 0x0400000F RID: 15
		private AutodiscoverService service;
	}
}
