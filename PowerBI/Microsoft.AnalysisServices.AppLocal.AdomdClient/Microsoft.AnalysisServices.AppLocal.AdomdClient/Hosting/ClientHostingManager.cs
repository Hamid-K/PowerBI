using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.AdomdClient.Configuration;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient.Hosting
{
	// Token: 0x02000161 RID: 353
	internal static class ClientHostingManager
	{
		// Token: 0x06001112 RID: 4370 RVA: 0x0003B363 File Offset: 0x00039563
		static ClientHostingManager()
		{
			ClientHostingManager.Initialize();
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x0003B380 File Offset: 0x00039580
		// (set) Token: 0x06001114 RID: 4372 RVA: 0x0003B388 File Offset: 0x00039588
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

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x0003B3D0 File Offset: 0x000395D0
		// (set) Token: 0x06001116 RID: 4374 RVA: 0x0003B3EC File Offset: 0x000395EC
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

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x0003B44C File Offset: 0x0003964C
		internal static IReadOnlyDictionary<int, object> ClientConfiguration
		{
			get
			{
				return ClientHostingManager.clientConfiguration;
			}
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0003B454 File Offset: 0x00039654
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

		// Token: 0x06001119 RID: 4377 RVA: 0x0003B4B4 File Offset: 0x000396B4
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

		// Token: 0x0600111A RID: 4378 RVA: 0x0003B50C File Offset: 0x0003970C
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

		// Token: 0x0600111B RID: 4379 RVA: 0x0003B55C File Offset: 0x0003975C
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

		// Token: 0x0600111C RID: 4380 RVA: 0x0003B634 File Offset: 0x00039834
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

		// Token: 0x0600111D RID: 4381 RVA: 0x0003B703 File Offset: 0x00039903
		private static void InitializeConfiguration()
		{
			ClientHostingManager.InitializeRuntimeAndClientConfiguration();
			ClientHostingManager.PersistConfigurationImpl(-1);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0003B710 File Offset: 0x00039910
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

		// Token: 0x0600111F RID: 4383 RVA: 0x0003B774 File Offset: 0x00039974
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

		// Token: 0x06001120 RID: 4384 RVA: 0x0003B7C0 File Offset: 0x000399C0
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

		// Token: 0x04000B4E RID: 2894
		private const string AppDomainKey_HostingLock = "MICROSOFT_ANALYSISSERVICES_HOSTING_LOCK";

		// Token: 0x04000B4F RID: 2895
		private const string AppDomainKey_HostingRefreshAction = "MICROSOFT_ANALYSISSERVICES_HOSTING_REFRESH_ACTIONS";

		// Token: 0x04000B50 RID: 2896
		private const string AppDomainKey_HostingManagerId = "MICROSOFT_ANALYSISSERVICES_LAST_HOSTING_MANAGER_ID";

		// Token: 0x04000B51 RID: 2897
		private const string AppDomainKey_HostingConfiguration = "MICROSOFT_ANALYSISSERVICES_HOSTING_CONFIGURATION_INFO";

		// Token: 0x04000B52 RID: 2898
		private const string CconPrefix = "<ccon>";

		// Token: 0x04000B53 RID: 2899
		private const string CconSuffix = "</ccon>";

		// Token: 0x04000B54 RID: 2900
		private const string EuiiPrefix = "<euii>";

		// Token: 0x04000B55 RID: 2901
		private const string EuiiSuffix = "</euii>";

		// Token: 0x04000B56 RID: 2902
		private const int ConfigElementsArraySize = 5;

		// Token: 0x04000B57 RID: 2903
		private const int ConfigElementIndex_SupportedVersion = 0;

		// Token: 0x04000B58 RID: 2904
		private const int ConfigElementIndex_IsInfoRestrictionNeeded = 1;

		// Token: 0x04000B59 RID: 2905
		private const int ConfigElementIndex_IsProcessWithUserInterface = 2;

		// Token: 0x04000B5A RID: 2906
		private const int ConfigElementIndex_ClientConfigurationVersion = 3;

		// Token: 0x04000B5B RID: 2907
		private const int ConfigElementIndex_ClientConfiguration = 4;

		// Token: 0x04000B5C RID: 2908
		private const int HostingManagerVersion = 2;

		// Token: 0x04000B5D RID: 2909
		private static readonly int KnownConfigurationVersion = typeof(ConfigEntry).GetFields().Length;

		// Token: 0x04000B5E RID: 2910
		private static IList<Action> onRefresh;

		// Token: 0x04000B5F RID: 2911
		private static int managerId;

		// Token: 0x04000B60 RID: 2912
		private static object[] configuration;

		// Token: 0x04000B61 RID: 2913
		private static bool isInfoRestrictionNeeded;

		// Token: 0x04000B62 RID: 2914
		private static bool? isProcessWithUserInterface;

		// Token: 0x04000B63 RID: 2915
		private static int clientConfigurationVersion;

		// Token: 0x04000B64 RID: 2916
		private static IReadOnlyDictionary<int, object> clientConfiguration;
	}
}
