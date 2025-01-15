using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Configuration;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Hosting
{
	// Token: 0x02000157 RID: 343
	internal static class ClientHostingManager
	{
		// Token: 0x060011A1 RID: 4513 RVA: 0x0003DCF3 File Offset: 0x0003BEF3
		static ClientHostingManager()
		{
			ClientHostingManager.Initialize();
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0003DD10 File Offset: 0x0003BF10
		// (set) Token: 0x060011A3 RID: 4515 RVA: 0x0003DD18 File Offset: 0x0003BF18
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

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0003DD60 File Offset: 0x0003BF60
		// (set) Token: 0x060011A5 RID: 4517 RVA: 0x0003DD7C File Offset: 0x0003BF7C
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

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x0003DDDC File Offset: 0x0003BFDC
		internal static IReadOnlyDictionary<int, object> ClientConfiguration
		{
			get
			{
				return ClientHostingManager.clientConfiguration;
			}
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0003DDE4 File Offset: 0x0003BFE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string MarkAsRestrictedInformation(string info, InfoRestrictionType restrictionType = InfoRestrictionType.CCON)
		{
			if (ClientHostingManager.isInfoRestrictionNeeded && !string.IsNullOrEmpty(info))
			{
				if (restrictionType == InfoRestrictionType.CCON)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", "<ccon>", info, "</ccon>");
				}
				if (restrictionType == InfoRestrictionType.EUII)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", "<euii>", info, "</euii>");
				}
			}
			return info;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0003DE44 File Offset: 0x0003C044
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

		// Token: 0x060011A9 RID: 4521 RVA: 0x0003DE9C File Offset: 0x0003C09C
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

		// Token: 0x060011AA RID: 4522 RVA: 0x0003DEEC File Offset: 0x0003C0EC
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

		// Token: 0x060011AB RID: 4523 RVA: 0x0003DFC4 File Offset: 0x0003C1C4
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

		// Token: 0x060011AC RID: 4524 RVA: 0x0003E093 File Offset: 0x0003C293
		private static void InitializeConfiguration()
		{
			ClientHostingManager.InitializeRuntimeAndClientConfiguration();
			ClientHostingManager.PersistConfigurationImpl(-1);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0003E0A0 File Offset: 0x0003C2A0
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

		// Token: 0x060011AE RID: 4526 RVA: 0x0003E104 File Offset: 0x0003C304
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

		// Token: 0x060011AF RID: 4527 RVA: 0x0003E150 File Offset: 0x0003C350
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

		// Token: 0x04000B07 RID: 2823
		private const string AppDomainKey_HostingLock = "MICROSOFT_ANALYSISSERVICES_HOSTING_LOCK";

		// Token: 0x04000B08 RID: 2824
		private const string AppDomainKey_HostingRefreshAction = "MICROSOFT_ANALYSISSERVICES_HOSTING_REFRESH_ACTIONS";

		// Token: 0x04000B09 RID: 2825
		private const string AppDomainKey_HostingManagerId = "MICROSOFT_ANALYSISSERVICES_LAST_HOSTING_MANAGER_ID";

		// Token: 0x04000B0A RID: 2826
		private const string AppDomainKey_HostingConfiguration = "MICROSOFT_ANALYSISSERVICES_HOSTING_CONFIGURATION_INFO";

		// Token: 0x04000B0B RID: 2827
		private const string CconPrefix = "<ccon>";

		// Token: 0x04000B0C RID: 2828
		private const string CconSuffix = "</ccon>";

		// Token: 0x04000B0D RID: 2829
		private const string EuiiPrefix = "<euii>";

		// Token: 0x04000B0E RID: 2830
		private const string EuiiSuffix = "</euii>";

		// Token: 0x04000B0F RID: 2831
		private const int ConfigElementsArraySize = 5;

		// Token: 0x04000B10 RID: 2832
		private const int ConfigElementIndex_SupportedVersion = 0;

		// Token: 0x04000B11 RID: 2833
		private const int ConfigElementIndex_IsInfoRestrictionNeeded = 1;

		// Token: 0x04000B12 RID: 2834
		private const int ConfigElementIndex_IsProcessWithUserInterface = 2;

		// Token: 0x04000B13 RID: 2835
		private const int ConfigElementIndex_ClientConfigurationVersion = 3;

		// Token: 0x04000B14 RID: 2836
		private const int ConfigElementIndex_ClientConfiguration = 4;

		// Token: 0x04000B15 RID: 2837
		private const int HostingManagerVersion = 2;

		// Token: 0x04000B16 RID: 2838
		private static readonly int KnownConfigurationVersion = typeof(ConfigEntry).GetFields().Length;

		// Token: 0x04000B17 RID: 2839
		private static IList<Action> onRefresh;

		// Token: 0x04000B18 RID: 2840
		private static int managerId;

		// Token: 0x04000B19 RID: 2841
		private static object[] configuration;

		// Token: 0x04000B1A RID: 2842
		private static bool isInfoRestrictionNeeded;

		// Token: 0x04000B1B RID: 2843
		private static bool? isProcessWithUserInterface;

		// Token: 0x04000B1C RID: 2844
		private static int clientConfigurationVersion;

		// Token: 0x04000B1D RID: 2845
		private static IReadOnlyDictionary<int, object> clientConfiguration;
	}
}
