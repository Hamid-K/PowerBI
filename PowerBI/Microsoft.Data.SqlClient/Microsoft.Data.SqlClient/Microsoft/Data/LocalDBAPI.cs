using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using Microsoft.Data.SqlClient;

namespace Microsoft.Data
{
	// Token: 0x02000010 RID: 16
	internal static class LocalDBAPI
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x0000AAAC File Offset: 0x00008CAC
		internal static string GetLocalDbInstanceNameFromServerName(string serverName)
		{
			if (serverName != null)
			{
				ReadOnlySpan<char> readOnlySpan = MemoryExtensions.Trim(MemoryExtensions.AsSpan(serverName));
				if (MemoryExtensions.StartsWith(readOnlySpan, MemoryExtensions.AsSpan("(localdb)\\"), StringComparison.OrdinalIgnoreCase))
				{
					readOnlySpan = readOnlySpan.Slice("(localdb)\\".Length);
					if (!readOnlySpan.IsEmpty)
					{
						return readOnlySpan.ToString();
					}
				}
				else if (MemoryExtensions.StartsWith(readOnlySpan, MemoryExtensions.AsSpan("np:\\\\.\\pipe\\LOCALDB#"), StringComparison.OrdinalIgnoreCase))
				{
					return readOnlySpan.ToString();
				}
			}
			return null;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0000AB26 File Offset: 0x00008D26
		internal static void ReleaseDLLHandles()
		{
			LocalDBAPI.s_userInstanceDLLHandle = IntPtr.Zero;
			LocalDBAPI.s_localDBFormatMessage = null;
			LocalDBAPI.s_localDBCreateInstance = null;
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0000AB40 File Offset: 0x00008D40
		private static IntPtr UserInstanceDLLHandle
		{
			get
			{
				if (LocalDBAPI.s_userInstanceDLLHandle == IntPtr.Zero)
				{
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.Enter(LocalDBAPI.s_dllLock, ref flag);
						if (LocalDBAPI.s_userInstanceDLLHandle == IntPtr.Zero)
						{
							SNINativeMethodWrapper.SNIQueryInfo(SNINativeMethodWrapper.QTypes.SNI_QUERY_LOCALDB_HMODULE, ref LocalDBAPI.s_userInstanceDLLHandle);
							if (!(LocalDBAPI.s_userInstanceDLLHandle != IntPtr.Zero))
							{
								SNINativeMethodWrapper.SNI_Error sni_Error = default(SNINativeMethodWrapper.SNI_Error);
								SNINativeMethodWrapper.SNIGetLastError(out sni_Error);
								throw LocalDBAPI.CreateLocalDBException(StringsHelper.GetString("LocalDB_FailedGetDLLHandle", Array.Empty<object>()), null, 0, (int)sni_Error.sniError);
							}
							SqlClientEventSource.Log.TryTraceEvent("<sc.LocalDBAPI.UserInstanceDLLHandle> LocalDB - handle obtained");
						}
					}
					finally
					{
						if (flag)
						{
							Monitor.Exit(LocalDBAPI.s_dllLock);
						}
					}
				}
				return LocalDBAPI.s_userInstanceDLLHandle;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0000AC04 File Offset: 0x00008E04
		private static LocalDBAPI.LocalDBCreateInstanceDelegate LocalDBCreateInstance
		{
			get
			{
				if (LocalDBAPI.s_localDBCreateInstance == null)
				{
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.Enter(LocalDBAPI.s_dllLock, ref flag);
						if (LocalDBAPI.s_localDBCreateInstance == null)
						{
							IntPtr procAddress = SafeNativeMethods.GetProcAddress(LocalDBAPI.UserInstanceDLLHandle, "LocalDBCreateInstance");
							if (procAddress == IntPtr.Zero)
							{
								int lastWin32Error = Marshal.GetLastWin32Error();
								SqlClientEventSource.Log.TryTraceEvent<int>("<sc.LocalDBAPI.LocalDBCreateInstance> GetProcAddress for LocalDBCreateInstance error 0x{0}", lastWin32Error);
								throw LocalDBAPI.CreateLocalDBException(StringsHelper.GetString("LocalDB_MethodNotFound", Array.Empty<object>()), null, 0, 0);
							}
							LocalDBAPI.s_localDBCreateInstance = (LocalDBAPI.LocalDBCreateInstanceDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(LocalDBAPI.LocalDBCreateInstanceDelegate));
						}
					}
					finally
					{
						if (flag)
						{
							Monitor.Exit(LocalDBAPI.s_dllLock);
						}
					}
				}
				return LocalDBAPI.s_localDBCreateInstance;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0000ACC0 File Offset: 0x00008EC0
		private static LocalDBAPI.LocalDBFormatMessageDelegate LocalDBFormatMessage
		{
			get
			{
				if (LocalDBAPI.s_localDBFormatMessage == null)
				{
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.Enter(LocalDBAPI.s_dllLock, ref flag);
						if (LocalDBAPI.s_localDBFormatMessage == null)
						{
							IntPtr procAddress = SafeNativeMethods.GetProcAddress(LocalDBAPI.UserInstanceDLLHandle, "LocalDBFormatMessage");
							if (procAddress == IntPtr.Zero)
							{
								int lastWin32Error = Marshal.GetLastWin32Error();
								SqlClientEventSource.Log.TryTraceEvent<int>("<sc.LocalDBAPI.LocalDBFormatMessage> GetProcAddress for LocalDBFormatMessage error 0x{0}", lastWin32Error);
								throw LocalDBAPI.CreateLocalDBException(StringsHelper.GetString("LocalDB_MethodNotFound", Array.Empty<object>()), null, 0, 0);
							}
							LocalDBAPI.s_localDBFormatMessage = (LocalDBAPI.LocalDBFormatMessageDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(LocalDBAPI.LocalDBFormatMessageDelegate));
						}
					}
					finally
					{
						if (flag)
						{
							Monitor.Exit(LocalDBAPI.s_dllLock);
						}
					}
				}
				return LocalDBAPI.s_localDBFormatMessage;
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0000AD7C File Offset: 0x00008F7C
		internal static string GetLocalDBMessage(int hrCode)
		{
			string text;
			try
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				uint num = (uint)stringBuilder.Capacity;
				int num2 = LocalDBAPI.LocalDBFormatMessage(hrCode, 1U, (uint)CultureInfo.CurrentCulture.LCID, stringBuilder, ref num);
				if (num2 >= 0)
				{
					text = stringBuilder.ToString();
				}
				else
				{
					stringBuilder = new StringBuilder(1024);
					num = (uint)stringBuilder.Capacity;
					num2 = LocalDBAPI.LocalDBFormatMessage(hrCode, 1U, 0U, stringBuilder, ref num);
					if (num2 >= 0)
					{
						text = stringBuilder.ToString();
					}
					else
					{
						text = string.Format(CultureInfo.CurrentCulture, "{0} (0x{1:X}).", StringsHelper.GetString("LocalDB_UnobtainableMessage", Array.Empty<object>()), num2);
					}
				}
			}
			catch (SqlException ex)
			{
				text = string.Format(CultureInfo.CurrentCulture, "{0} ({1}).", StringsHelper.GetString("LocalDB_UnobtainableMessage", Array.Empty<object>()), ex.Message);
			}
			return text;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0000AE58 File Offset: 0x00009058
		private static SqlException CreateLocalDBException(string errorMessage, string instance = null, int localDbError = 0, int sniError = 0)
		{
			SqlErrorCollection sqlErrorCollection = new SqlErrorCollection();
			int num = ((localDbError == 0) ? sniError : localDbError);
			if (sniError != 0)
			{
				string snierrorMessage = SQL.GetSNIErrorMessage(sniError);
				errorMessage = string.Format(null, "{0} (error: {1} - {2})", errorMessage, sniError, snierrorMessage);
			}
			sqlErrorCollection.Add(new SqlError(num, 0, 20, instance, errorMessage, null, 0, null));
			if (localDbError != 0)
			{
				sqlErrorCollection.Add(new SqlError(num, 0, 20, instance, LocalDBAPI.GetLocalDBMessage(localDbError), null, 0, null));
			}
			SqlException ex = SqlException.CreateException(sqlErrorCollection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0000AED4 File Offset: 0x000090D4
		internal static void DemandLocalDBPermissions()
		{
			if (!LocalDBAPI._partialTrustAllowed)
			{
				if (!LocalDBAPI._partialTrustFlagChecked)
				{
					object data = AppDomain.CurrentDomain.GetData("ALLOW_LOCALDB_IN_PARTIAL_TRUST");
					if (data != null && data is bool)
					{
						LocalDBAPI._partialTrustAllowed = (bool)data;
					}
					LocalDBAPI._partialTrustFlagChecked = true;
					if (LocalDBAPI._partialTrustAllowed)
					{
						return;
					}
				}
				if (LocalDBAPI._fullTrust == null)
				{
					LocalDBAPI._fullTrust = new NamedPermissionSet("FullTrust");
				}
				LocalDBAPI._fullTrust.Demand();
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0000AF43 File Offset: 0x00009143
		internal static void AssertLocalDBPermissions()
		{
			LocalDBAPI._partialTrustAllowed = true;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0000AF4C File Offset: 0x0000914C
		internal static void CreateLocalDBInstance(string instance)
		{
			LocalDBAPI.DemandLocalDBPermissions();
			if (LocalDBAPI.s_configurableInstances == null)
			{
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.Enter(LocalDBAPI.s_configLock, ref flag);
					if (LocalDBAPI.s_configurableInstances == null)
					{
						Dictionary<string, LocalDBAPI.InstanceInfo> dictionary = new Dictionary<string, LocalDBAPI.InstanceInfo>(StringComparer.OrdinalIgnoreCase);
						object section = ConfigurationManager.GetSection("system.data.localdb");
						if (section != null)
						{
							LocalDBConfigurationSection localDBConfigurationSection = section as LocalDBConfigurationSection;
							if (localDBConfigurationSection == null)
							{
								throw LocalDBAPI.CreateLocalDBException(StringsHelper.GetString("LocalDB_BadConfigSectionType", Array.Empty<object>()), null, 0, 0);
							}
							using (IEnumerator enumerator = localDBConfigurationSection.LocalDbInstances.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									LocalDBInstanceElement localDBInstanceElement = (LocalDBInstanceElement)obj;
									dictionary.Add(localDBInstanceElement.Name.Trim(), new LocalDBAPI.InstanceInfo(localDBInstanceElement.Version.Trim()));
								}
								goto IL_00DF;
							}
						}
						SqlClientEventSource.Log.TryTraceEvent("<sc.LocalDBAPI.CreateLocalDBInstance> No system.data.localdb section found in configuration");
						IL_00DF:
						LocalDBAPI.s_configurableInstances = dictionary;
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(LocalDBAPI.s_configLock);
					}
				}
			}
			LocalDBAPI.InstanceInfo instanceInfo = null;
			if (!LocalDBAPI.s_configurableInstances.TryGetValue(instance, out instanceInfo))
			{
				return;
			}
			if (instanceInfo.created)
			{
				return;
			}
			if (instanceInfo.version.Contains("\0"))
			{
				throw LocalDBAPI.CreateLocalDBException(StringsHelper.GetString("LocalDB_InvalidVersion", Array.Empty<object>()), instance, 0, 0);
			}
			int num = LocalDBAPI.LocalDBCreateInstance(instanceInfo.version, instance, 0U);
			SqlClientEventSource.Log.TryTraceEvent<string, string>("<sc.LocalDBAPI.CreateLocalDBInstance> Starting creation of instance {0} version {1}", instance, instanceInfo.version);
			if (num < 0)
			{
				throw LocalDBAPI.CreateLocalDBException(StringsHelper.GetString("LocalDB_CreateFailed", Array.Empty<object>()), instance, num, 0);
			}
			SqlClientEventSource.Log.TryTraceEvent<string>("<sc.LocalDBAPI.CreateLocalDBInstance> Finished creation of instance {0}", instance);
			instanceInfo.created = true;
		}

		// Token: 0x0400000E RID: 14
		private const string LocalDbPrefix = "(localdb)\\";

		// Token: 0x0400000F RID: 15
		private const string LocalDbPrefix_NP = "np:\\\\.\\pipe\\LOCALDB#";

		// Token: 0x04000010 RID: 16
		private const string Const_partialTrustFlagKey = "ALLOW_LOCALDB_IN_PARTIAL_TRUST";

		// Token: 0x04000011 RID: 17
		private static PermissionSet _fullTrust = null;

		// Token: 0x04000012 RID: 18
		private static bool _partialTrustFlagChecked = false;

		// Token: 0x04000013 RID: 19
		private static bool _partialTrustAllowed = false;

		// Token: 0x04000014 RID: 20
		private static IntPtr s_userInstanceDLLHandle = IntPtr.Zero;

		// Token: 0x04000015 RID: 21
		private static object s_dllLock = new object();

		// Token: 0x04000016 RID: 22
		private static LocalDBAPI.LocalDBCreateInstanceDelegate s_localDBCreateInstance = null;

		// Token: 0x04000017 RID: 23
		private static LocalDBAPI.LocalDBFormatMessageDelegate s_localDBFormatMessage = null;

		// Token: 0x04000018 RID: 24
		private const uint const_LOCALDB_TRUNCATE_ERR_MESSAGE = 1U;

		// Token: 0x04000019 RID: 25
		private const int const_ErrorMessageBufferSize = 1024;

		// Token: 0x0400001A RID: 26
		private static object s_configLock = new object();

		// Token: 0x0400001B RID: 27
		private static Dictionary<string, LocalDBAPI.InstanceInfo> s_configurableInstances = null;

		// Token: 0x0200018B RID: 395
		// (Invoke) Token: 0x06001D38 RID: 7480
		[SuppressUnmanagedCodeSecurity]
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int LocalDBCreateInstanceDelegate([MarshalAs(UnmanagedType.LPWStr)] string version, [MarshalAs(UnmanagedType.LPWStr)] string instance, uint flags);

		// Token: 0x0200018C RID: 396
		// (Invoke) Token: 0x06001D3C RID: 7484
		[SuppressUnmanagedCodeSecurity]
		[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private delegate int LocalDBFormatMessageDelegate(int hrLocalDB, uint dwFlags, uint dwLanguageId, StringBuilder buffer, ref uint buflen);

		// Token: 0x0200018D RID: 397
		private class InstanceInfo
		{
			// Token: 0x06001D3F RID: 7487 RVA: 0x00077FB6 File Offset: 0x000761B6
			internal InstanceInfo(string version)
			{
				this.version = version;
				this.created = false;
			}

			// Token: 0x04000C63 RID: 3171
			internal readonly string version;

			// Token: 0x04000C64 RID: 3172
			internal bool created;
		}
	}
}
