using System;
using System.Net;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.WebServices.Dns
{
	// Token: 0x020001D8 RID: 472
	[ComVisible(false)]
	internal static class DnsNativeMethods
	{
		// Token: 0x0600156C RID: 5484
		[DllImport("dnsapi.dll", CharSet = 3, EntryPoint = "DnsQuery_W", ExactSpelling = true, SetLastError = true)]
		private static extern int DnsQuery([In] string pszName, DnsRecordType wType, DnsNativeMethods.DnsQueryOptions options, IntPtr aipServers, ref IntPtr ppQueryResults, int pReserved);

		// Token: 0x0600156D RID: 5485
		[DllImport("dnsapi.dll", CharSet = 3)]
		private static extern void DnsRecordListFree([In] IntPtr ptrRecords, [In] DnsNativeMethods.FreeType freeType);

		// Token: 0x0600156E RID: 5486 RVA: 0x0003C7A8 File Offset: 0x0003B7A8
		private static IntPtr AllocDnsServerList(IPAddress dnsServerAddress)
		{
			IntPtr intPtr = IntPtr.Zero;
			if (dnsServerAddress != null)
			{
				int num = BitConverter.ToInt32(dnsServerAddress.GetAddressBytes(), 0);
				DnsNativeMethods.DnsServerList dnsServerList;
				dnsServerList.AddressCount = 1;
				dnsServerList.ServerAddress = num;
				intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(dnsServerList));
				Marshal.StructureToPtr(dnsServerList, intPtr, false);
			}
			return intPtr;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0003C7FC File Offset: 0x0003B7FC
		internal static int DnsQuery(string domain, IPAddress dnsServerAddress, DnsRecordType recordType, ref IntPtr ppQueryResults)
		{
			IntPtr intPtr = IntPtr.Zero;
			int num;
			try
			{
				intPtr = DnsNativeMethods.AllocDnsServerList(dnsServerAddress);
				num = DnsNativeMethods.DnsQuery(domain, recordType, DnsNativeMethods.DnsQueryOptions.DNS_QUERY_STANDARD, intPtr, ref ppQueryResults, 0);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return num;
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0003C83C File Offset: 0x0003B83C
		internal static void FreeDnsQueryResults(IntPtr ptrRecords)
		{
			DnsNativeMethods.DnsRecordListFree(ptrRecords, DnsNativeMethods.FreeType.RecordList);
		}

		// Token: 0x04000CFD RID: 3325
		private const string DNSAPI = "dnsapi.dll";

		// Token: 0x020001D9 RID: 473
		private enum FreeType
		{
			// Token: 0x04000CFF RID: 3327
			RecordList = 1
		}

		// Token: 0x020001DA RID: 474
		private enum DnsQueryOptions
		{
			// Token: 0x04000D01 RID: 3329
			DNS_QUERY_STANDARD
		}

		// Token: 0x020001DB RID: 475
		[StructLayout(0, CharSet = 3)]
		private struct DnsServerList
		{
			// Token: 0x04000D02 RID: 3330
			public int AddressCount;

			// Token: 0x04000D03 RID: 3331
			public int ServerAddress;
		}
	}
}
