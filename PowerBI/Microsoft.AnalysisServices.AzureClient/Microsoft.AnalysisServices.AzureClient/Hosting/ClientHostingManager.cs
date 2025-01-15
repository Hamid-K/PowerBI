using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.AzureClient.Configuration;
using Microsoft.AnalysisServices.AzureClient.Utilities;

namespace Microsoft.AnalysisServices.AzureClient.Hosting
{
	// Token: 0x02000040 RID: 64
	internal static class ClientHostingManager
	{
		// Token: 0x060001EF RID: 495 RVA: 0x0000983F File Offset: 0x00007A3F
		static ClientHostingManager()
		{
			ClientHostingManager.Initialize();
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000985C File Offset: 0x00007A5C
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00009864 File Offset: 0x00007A64
		public static bool IsInfoRestrictionNeeded
		{
			get
			{
				return ClientHostingManager.isInfoRestrictionNeeded;
			}
			set
			{
				if (value != ClientHostingManager.isInfoRestrictionNeeded)
				{
					using (GlobalContext.CreateGlobalLockScope("MICROSOFT_ANALYSISSERVICES_HOSTING_LOCK"))
					{
						ClientHostingManager.isInfoRestrictionNeeded = value;
						ClientHostingManager.PersistConfiguration();
					}
				}
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000098AC File Offset: 0x00007AAC
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x000098C8 File Offset: 0x00007AC8
		public static bool IsProcessWithUserInterface
		{
			get
			{
				return ClientHostingManager.isProcessWithUserInterface != null && ClientHostingManager.isProcessWithUserInterface.Value;
			}
			set
			{
				if (ClientHostingManager.isProcessWithUserInterface == null || value != ClientHostingManager.isProcessWithUserInterface.Value)
				{
					using (GlobalContext.CreateGlobalLockScope("MICROSOFT_ANALYSISSERVICES_HOSTING_LOCK"))
					{
						ClientHostingManager.isProcessWithUserInterface = new bool?(value);
						ClientHostingManager.PersistConfiguration();
					}
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00009928 File Offset: 0x00007B28
		internal static IReadOnlyDictionary<int, object> ClientConfiguration
		{
			get
			{
				return ClientHostingManager.clientConfiguration;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009930 File Offset: 0x00007B30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string MarkAsRestrictedInformation(string info, InfoRestrictionType restrictionType = InfoRestrictionType.CCON)
		{
			if (ClientHostingManager.isInfoRestrictionNeeded && !string.IsNullOrEmpty(info))
			{
				if (restrictionType == InfoRestrictionType.CCON)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[] { "<ccon>", info, "</ccon>" });
				}
				if (restrictionType == InfoRestrictionType.EUII)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[] { "<euii>", info, "</euii>" });
				}
			}
			return info;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000099AC File Offset: 0x00007BAC
		internal static void UpdateProcessWithUserInterfaceStatus(Func<bool> checkMethod)
		{
			if (ClientHostingManager.isProcessWithUserInterface == null)
			{
				using (GlobalContext.CreateGlobalLockScope("MICROSOFT_ANALYSISSERVICES_HOSTING_LOCK"))
				{
					ClientHostingManager.isProcessWithUserInterface = new bool?(checkMethod());
					ClientHostingManager.PersistConfiguration();
				}
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009A04 File Offset: 0x00007C04
		internal static void UpdateClientConfiguration(int configVersion, IReadOnlyDictionary<int, object> config)
		{
			if (config != ClientHostingManager.clientConfiguration)
			{
				using (GlobalContext.CreateGlobalLockScope("MICROSOFT_ANALYSISSERVICES_HOSTING_LOCK"))
				{
					ClientHostingManager.clientConfigurationVersion = configVersion;
					ClientHostingManager.clientConfiguration = config;
					ClientHostingManager.PersistConfiguration();
				}
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009A54 File Offset: 0x00007C54
		private static void Initialize()
		{
			using (GlobalContext.CreateGlobalLockScope("MICROSOFT_ANALYSISSERVICES_HOSTING_LOCK"))
			{
				if (GlobalContext.TryGetGlobalObject<IList<Action>>("MICROSOFT_ANALYSISSERVICES_HOSTING_REFRESH_ACTIONS", out ClientHostingManager.onRefresh))
				{
					ClientHostingManager.onRefresh.Add(new Action(ClientHostingManager.UpdateConfiguration));
					ClientHostingManager.managerId = GlobalContext.GetGlobalObject<int>("MICROSOFT_ANALYSISSERVICES_LAST_HOSTING_MANAGER_ID") + 1;
					GlobalContext.SetGlobalObject("MICROSOFT_ANALYSISSERVICES_LAST_HOSTING_MANAGER_ID", ClientHostingManager.managerId);
					ClientHostingManager.UpdateConfiguration();
				}
				else
				{
					ClientHostingManager.onRefresh = new List<Action>();
					ClientHostingManager.onRefresh.Add(new Action(ClientHostingManager.UpdateConfiguration));
					GlobalContext.SetGlobalObject("MICROSOFT_ANALYSISSERVICES_HOSTING_REFRESH_ACTIONS", ClientHostingManager.onRefresh);
					ClientHostingManager.managerId = 0;
					GlobalContext.SetGlobalObject("MICROSOFT_ANALYSISSERVICES_LAST_HOSTING_MANAGER_ID", ClientHostingManager.managerId);
					ClientHostingManager.InitializeConfiguration();
				}
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009B2C File Offset: 0x00007D2C
		private static void UpdateConfiguration()
		{
			ClientHostingManager.configuration = GlobalContext.GetGlobalObject<object[]>("MICROSOFT_ANALYSISSERVICES_HOSTING_CONFIGURATION_INFO");
			int num = (int)ClientHostingManager.configuration[0];
			ClientHostingManager.isInfoRestrictionNeeded = (bool)ClientHostingManager.configuration[1];
			ClientHostingManager.isProcessWithUserInterface = (bool?)ClientHostingManager.configuration[2];
			bool flag;
			if (num >= 2)
			{
				ClientHostingManager.clientConfigurationVersion = (int)ClientHostingManager.configuration[3];
				if (ClientHostingManager.clientConfigurationVersion == 0)
				{
					ClientHostingManager.InitializeRuntimeAndClientConfiguration();
					flag = true;
				}
				else if (ClientHostingManager.clientConfigurationVersion < ClientHostingManager.KnownConfigurationVersion)
				{
					ClientHostingManager.clientConfiguration = ClientConfigLoader.LoadClientConfiguration();
					ClientHostingManager.clientConfigurationVersion = ClientHostingManager.KnownConfigurationVersion;
					flag = true;
				}
				else
				{
					ClientHostingManager.clientConfiguration = (IReadOnlyDictionary<int, object>)ClientHostingManager.configuration[4];
					flag = false;
				}
			}
			else
			{
				ClientHostingManager.InitializeRuntimeAndClientConfiguration();
				flag = true;
			}
			if (num < 2 || flag)
			{
				if (num >= 2 && flag)
				{
					ClientHostingManager.PersistConfiguration();
					return;
				}
				ClientHostingManager.PersistConfigurationImpl(num);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009BFB File Offset: 0x00007DFB
		private static void InitializeConfiguration()
		{
			ClientHostingManager.InitializeRuntimeAndClientConfiguration();
			ClientHostingManager.PersistConfigurationImpl(-1);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009C08 File Offset: 0x00007E08
		private static void InitializeRuntimeAndClientConfiguration()
		{
			ClientHostingManager.clientConfigurationVersion = ClientHostingManager.KnownConfigurationVersion;
			ClientHostingManager.clientConfiguration = ClientConfigLoader.LoadClientConfiguration();
			object obj;
			if (ClientHostingManager.clientConfiguration.TryGetValue(65538, out obj))
			{
				ClientHostingManager.isInfoRestrictionNeeded = (bool)obj;
			}
			if (ClientHostingManager.clientConfiguration.TryGetValue(65537, out obj))
			{
				ClientHostingManager.isProcessWithUserInterface = new bool?((bool)obj);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009C6C File Offset: 0x00007E6C
		private static void PersistConfiguration()
		{
			ClientHostingManager.PersistConfigurationImpl((int)ClientHostingManager.configuration[0]);
			for (int i = 0; i < ClientHostingManager.onRefresh.Count; i++)
			{
				if (i != ClientHostingManager.managerId)
				{
					ClientHostingManager.onRefresh[i]();
				}
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009CB8 File Offset: 0x00007EB8
		private static void PersistConfigurationImpl(int currentVersion)
		{
			if (currentVersion < 2)
			{
				ClientHostingManager.configuration = new object[5];
				ClientHostingManager.configuration[0] = 2;
				ClientHostingManager.configuration[1] = ClientHostingManager.isInfoRestrictionNeeded;
				ClientHostingManager.configuration[2] = ClientHostingManager.isProcessWithUserInterface;
				ClientHostingManager.configuration[3] = ClientHostingManager.clientConfigurationVersion;
				ClientHostingManager.configuration[4] = ClientHostingManager.clientConfiguration;
				GlobalContext.SetGlobalObject("MICROSOFT_ANALYSISSERVICES_HOSTING_CONFIGURATION_INFO", ClientHostingManager.configuration);
				return;
			}
			ClientHostingManager.configuration[1] = ClientHostingManager.isInfoRestrictionNeeded;
			ClientHostingManager.configuration[2] = ClientHostingManager.isProcessWithUserInterface;
			ClientHostingManager.configuration[3] = ClientHostingManager.clientConfigurationVersion;
			ClientHostingManager.configuration[4] = ClientHostingManager.clientConfiguration;
		}

		// Token: 0x040000FF RID: 255
		private const string AppDomainKey_HostingLock = "MICROSOFT_ANALYSISSERVICES_HOSTING_LOCK";

		// Token: 0x04000100 RID: 256
		private const string AppDomainKey_HostingRefreshAction = "MICROSOFT_ANALYSISSERVICES_HOSTING_REFRESH_ACTIONS";

		// Token: 0x04000101 RID: 257
		private const string AppDomainKey_HostingManagerId = "MICROSOFT_ANALYSISSERVICES_LAST_HOSTING_MANAGER_ID";

		// Token: 0x04000102 RID: 258
		private const string AppDomainKey_HostingConfiguration = "MICROSOFT_ANALYSISSERVICES_HOSTING_CONFIGURATION_INFO";

		// Token: 0x04000103 RID: 259
		private const string CconPrefix = "<ccon>";

		// Token: 0x04000104 RID: 260
		private const string CconSuffix = "</ccon>";

		// Token: 0x04000105 RID: 261
		private const string EuiiPrefix = "<euii>";

		// Token: 0x04000106 RID: 262
		private const string EuiiSuffix = "</euii>";

		// Token: 0x04000107 RID: 263
		private const int ConfigElementsArraySize = 5;

		// Token: 0x04000108 RID: 264
		private const int ConfigElementIndex_SupportedVersion = 0;

		// Token: 0x04000109 RID: 265
		private const int ConfigElementIndex_IsInfoRestrictionNeeded = 1;

		// Token: 0x0400010A RID: 266
		private const int ConfigElementIndex_IsProcessWithUserInterface = 2;

		// Token: 0x0400010B RID: 267
		private const int ConfigElementIndex_ClientConfigurationVersion = 3;

		// Token: 0x0400010C RID: 268
		private const int ConfigElementIndex_ClientConfiguration = 4;

		// Token: 0x0400010D RID: 269
		private const int HostingManagerVersion = 2;

		// Token: 0x0400010E RID: 270
		private static readonly int KnownConfigurationVersion = typeof(ConfigEntry).GetFields().Length;

		// Token: 0x0400010F RID: 271
		private static IList<Action> onRefresh;

		// Token: 0x04000110 RID: 272
		private static int managerId;

		// Token: 0x04000111 RID: 273
		private static object[] configuration;

		// Token: 0x04000112 RID: 274
		private static bool isInfoRestrictionNeeded;

		// Token: 0x04000113 RID: 275
		private static bool? isProcessWithUserInterface;

		// Token: 0x04000114 RID: 276
		private static int clientConfigurationVersion;

		// Token: 0x04000115 RID: 277
		private static IReadOnlyDictionary<int, object> clientConfiguration;
	}
}
