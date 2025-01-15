using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Dns
{
	// Token: 0x020001D7 RID: 471
	internal class DnsClient
	{
		// Token: 0x06001568 RID: 5480 RVA: 0x0003C668 File Offset: 0x0003B668
		internal static List<T> DnsQuery<T>(string domain, IPAddress dnsServerAddress) where T : DnsRecord, new()
		{
			List<T> list = new List<T>();
			DnsRecordType dnsRecordType = DnsClient.typeToDnsTypeMap.Member[typeof(T)];
			IntPtr zero = IntPtr.Zero;
			try
			{
				int num = DnsNativeMethods.DnsQuery(domain, dnsServerAddress, dnsRecordType, ref zero);
				if (num != 0)
				{
					throw new DnsException(num);
				}
				IntPtr intPtr = zero;
				while (!intPtr.Equals(IntPtr.Zero))
				{
					DnsRecordHeader dnsRecordHeader = (DnsRecordHeader)Marshal.PtrToStructure(intPtr, typeof(DnsRecordHeader));
					T t = new T();
					if (dnsRecordHeader.RecordType == t.RecordType)
					{
						t.Load(dnsRecordHeader, intPtr);
						list.Add(t);
					}
					intPtr = dnsRecordHeader.NextRecord;
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					DnsNativeMethods.FreeDnsQueryResults(zero);
				}
			}
			return list;
		}

		// Token: 0x04000CFA RID: 3322
		private const int Win32Success = 0;

		// Token: 0x04000CFB RID: 3323
		private static LazyMember<Dictionary<Type, DnsRecordType>> typeToDnsTypeMap = new LazyMember<Dictionary<Type, DnsRecordType>>(delegate
		{
			Dictionary<Type, DnsRecordType> dictionary = new Dictionary<Type, DnsRecordType>();
			dictionary.Add(typeof(DnsSrvRecord), DnsRecordType.SRV);
			return dictionary;
		});
	}
}
