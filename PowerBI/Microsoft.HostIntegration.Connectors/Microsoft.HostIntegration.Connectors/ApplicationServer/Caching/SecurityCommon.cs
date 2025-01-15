using System;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000382 RID: 898
	internal static class SecurityCommon
	{
		// Token: 0x06001F9F RID: 8095 RVA: 0x0006060C File Offset: 0x0005E80C
		internal static byte[] GetBinarySID(NTAccount account)
		{
			byte[] array2;
			try
			{
				SecurityIdentifier securityIdentifier = (SecurityIdentifier)account.Translate(typeof(SecurityIdentifier));
				byte[] array = new byte[securityIdentifier.BinaryLength];
				securityIdentifier.GetBinaryForm(array, 0);
				array2 = array;
			}
			catch (IdentityNotMappedException ex)
			{
				DataCacheException ex2 = new DataCacheException(ex.Message, ex);
				throw ex2;
			}
			return array2;
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x0006066C File Offset: 0x0005E86C
		internal static void GetWorkgroupUserDetailsOnRemoteHost(string machine, string localUser, out string remoteUserName, out byte[] sid)
		{
			string userName = SecurityCommon.GetUserName(localUser);
			sid = SecurityCommon.RetrieveSid(machine, userName);
			string text;
			SecurityCommon.RetrieveAccountName(machine, sid, out userName, out text);
			remoteUserName = string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", new object[] { machine, userName });
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x000606B8 File Offset: 0x0005E8B8
		internal static byte[] RetrieveSid(string machine, string accountName)
		{
			byte[] array = null;
			uint num = 0U;
			StringBuilder stringBuilder = new StringBuilder();
			uint capacity = (uint)stringBuilder.Capacity;
			bool flag = false;
			SID_NAME_USE sid_NAME_USE;
			if (!NativeMethods.LookupAccountNameW(machine, accountName, array, ref num, stringBuilder, ref capacity, out sid_NAME_USE))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 122 || lastWin32Error == 1004)
				{
					array = new byte[num];
					stringBuilder.EnsureCapacity((int)capacity);
					if (!NativeMethods.LookupAccountNameW(machine, accountName, array, ref num, stringBuilder, ref capacity, out sid_NAME_USE))
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				DataCacheException lastException = SecurityCommon.GetLastException();
				throw lastException;
			}
			return array;
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00060738 File Offset: 0x0005E938
		internal static void RetrieveAccountName(string machineName, byte[] sid, out string accountName, out string domainName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			uint capacity = (uint)stringBuilder.Capacity;
			StringBuilder stringBuilder2 = new StringBuilder();
			uint capacity2 = (uint)stringBuilder2.Capacity;
			bool flag = false;
			SID_NAME_USE sid_NAME_USE;
			if (!NativeMethods.LookupAccountSidW(machineName, sid, stringBuilder, ref capacity, stringBuilder2, ref capacity2, out sid_NAME_USE))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 122)
				{
					stringBuilder.EnsureCapacity((int)capacity);
					stringBuilder2.EnsureCapacity((int)capacity2);
					if (!NativeMethods.LookupAccountSidW(machineName, sid, stringBuilder, ref capacity, stringBuilder2, ref capacity2, out sid_NAME_USE))
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				DataCacheException lastException = SecurityCommon.GetLastException();
				throw lastException;
			}
			accountName = stringBuilder.ToString();
			domainName = stringBuilder2.ToString();
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x000607C8 File Offset: 0x0005E9C8
		private static DataCacheException GetLastException()
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			Win32Exception ex = new Win32Exception(lastWin32Error);
			return new DataCacheException(ex.Message, ex);
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x000607F0 File Offset: 0x0005E9F0
		internal static bool AreSameMachines(string server1, string server2)
		{
			IPHostEntry hostEntry = SecurityCommon.GetHostEntry(server1);
			IPHostEntry hostEntry2 = SecurityCommon.GetHostEntry(server2);
			return hostEntry.HostName.Equals(hostEntry2.HostName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x00060820 File Offset: 0x0005EA20
		internal static IPHostEntry GetHostEntry(string serverName)
		{
			IPHostEntry iphostEntry;
			try
			{
				iphostEntry = Dns.EndGetHostEntry(Dns.BeginGetHostEntry(serverName, null, null));
			}
			catch (SocketException ex)
			{
				throw new DataCacheException(ex.Message, ex);
			}
			return iphostEntry;
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x0006085C File Offset: 0x0005EA5C
		internal static NTAccount GetMachineAccount(string machine)
		{
			string computerDomainName = GetComputerInfo.GetComputerDomainName(machine);
			string text = machine.Split(new char[] { '.' })[0];
			NTAccount ntaccount = new NTAccount(computerDomainName + "\\" + text + "$");
			try
			{
				SecurityIdentifier securityIdentifier = (SecurityIdentifier)ntaccount.Translate(typeof(SecurityIdentifier));
				ntaccount = (NTAccount)securityIdentifier.Translate(typeof(NTAccount));
			}
			catch (IdentityNotMappedException ex)
			{
				throw new DataCacheException(ex.Message, ex);
			}
			return ntaccount;
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x000608F0 File Offset: 0x0005EAF0
		internal static NTAccount GetNetworkServiceIdRef()
		{
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
			return (NTAccount)securityIdentifier.Translate(typeof(NTAccount));
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x00060920 File Offset: 0x0005EB20
		internal static bool IsNetworkService(string user)
		{
			bool flag2;
			try
			{
				SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
				NTAccount ntaccount = new NTAccount(user);
				bool flag = securityIdentifier == ntaccount.Translate(typeof(SecurityIdentifier));
				flag2 = flag;
			}
			catch (IdentityNotMappedException ex)
			{
				throw new DataCacheException(ex.Message, ex);
			}
			return flag2;
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x0006097C File Offset: 0x0005EB7C
		internal static IdentityReference GetAccountIdRef(string account)
		{
			NTAccount ntaccount = new NTAccount(account);
			return ntaccount.Translate(typeof(SecurityIdentifier));
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x000609A2 File Offset: 0x0005EBA2
		internal static string GetNetworkServiceAccountName()
		{
			return SecurityCommon.GetNetworkServiceIdRef().Value;
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x000609B0 File Offset: 0x0005EBB0
		internal static string GetUserName(string localUser)
		{
			NTAccount ntaccount = new NTAccount(localUser);
			SecurityIdentifier securityIdentifier = (SecurityIdentifier)ntaccount.Translate(typeof(SecurityIdentifier));
			ntaccount = (NTAccount)securityIdentifier.Translate(typeof(NTAccount));
			string[] array = new string[] { "\\" };
			string[] array2 = ntaccount.ToString().Split(array, StringSplitOptions.None);
			return array2[1];
		}

		// Token: 0x02000383 RID: 899
		private enum WinErrorCodes
		{
			// Token: 0x040012D6 RID: 4822
			BUFFER_NOT_ALLOCATED = 122,
			// Token: 0x040012D7 RID: 4823
			ERROR_INVALID_FLAGS = 1004
		}
	}
}
