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
		// Token: 0x06001105 RID: 4357 RVA: 0x0003B033 File Offset: 0x00039233
		static ClientHostingManager()
		{
			ClientHostingManager.Initialize();
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x0003B050 File Offset: 0x00039250
		// (set) Token: 0x06001107 RID: 4359 RVA: 0x0003B058 File Offset: 0x00039258
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

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x0003B0A0 File Offset: 0x000392A0
		// (set) Token: 0x06001109 RID: 4361 RVA: 0x0003B0BC File Offset: 0x000392BC
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

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x0003B11C File Offset: 0x0003931C
		internal static IReadOnlyDictionary<int, object> ClientConfiguration
		{
			get
			{
				return ClientHostingManager.clientConfiguration;
			}
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0003B124 File Offset: 0x00039324
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

		// Token: 0x0600110C RID: 4364 RVA: 0x0003B184 File Offset: 0x00039384
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

		// Token: 0x0600110D RID: 4365 RVA: 0x0003B1DC File Offset: 0x000393DC
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

		// Token: 0x0600110E RID: 4366 RVA: 0x0003B22C File Offset: 0x0003942C
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

		// Token: 0x0600110F RID: 4367 RVA: 0x0003B304 File Offset: 0x00039504
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

		// Token: 0x06001110 RID: 4368 RVA: 0x0003B3D3 File Offset: 0x000395D3
		private static void InitializeConfiguration()
		{
			ClientHostingManager.InitializeRuntimeAndClientConfiguration();
			ClientHostingManager.PersistConfigurationImpl(-1);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0003B3E0 File Offset: 0x000395E0
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

		// Token: 0x06001112 RID: 4370 RVA: 0x0003B444 File Offset: 0x00039644
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

		// Token: 0x06001113 RID: 4371 RVA: 0x0003B490 File Offset: 0x00039690
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

		// Token: 0x04000B41 RID: 2881
		private const string AppDomainKey_HostingLock = "MICROSOFT_ANALYSISSERVICES_HOSTING_LOCK";

		// Token: 0x04000B42 RID: 2882
		private const string AppDomainKey_HostingRefreshAction = "MICROSOFT_ANALYSISSERVICES_HOSTING_REFRESH_ACTIONS";

		// Token: 0x04000B43 RID: 2883
		private const string AppDomainKey_HostingManagerId = "MICROSOFT_ANALYSISSERVICES_LAST_HOSTING_MANAGER_ID";

		// Token: 0x04000B44 RID: 2884
		private const string AppDomainKey_HostingConfiguration = "MICROSOFT_ANALYSISSERVICES_HOSTING_CONFIGURATION_INFO";

		// Token: 0x04000B45 RID: 2885
		private const string CconPrefix = "<ccon>";

		// Token: 0x04000B46 RID: 2886
		private const string CconSuffix = "</ccon>";

		// Token: 0x04000B47 RID: 2887
		private const string EuiiPrefix = "<euii>";

		// Token: 0x04000B48 RID: 2888
		private const string EuiiSuffix = "</euii>";

		// Token: 0x04000B49 RID: 2889
		private const int ConfigElementsArraySize = 5;

		// Token: 0x04000B4A RID: 2890
		private const int ConfigElementIndex_SupportedVersion = 0;

		// Token: 0x04000B4B RID: 2891
		private const int ConfigElementIndex_IsInfoRestrictionNeeded = 1;

		// Token: 0x04000B4C RID: 2892
		private const int ConfigElementIndex_IsProcessWithUserInterface = 2;

		// Token: 0x04000B4D RID: 2893
		private const int ConfigElementIndex_ClientConfigurationVersion = 3;

		// Token: 0x04000B4E RID: 2894
		private const int ConfigElementIndex_ClientConfiguration = 4;

		// Token: 0x04000B4F RID: 2895
		private const int HostingManagerVersion = 2;

		// Token: 0x04000B50 RID: 2896
		private static readonly int KnownConfigurationVersion = typeof(ConfigEntry).GetFields().Length;

		// Token: 0x04000B51 RID: 2897
		private static IList<Action> onRefresh;

		// Token: 0x04000B52 RID: 2898
		private static int managerId;

		// Token: 0x04000B53 RID: 2899
		private static object[] configuration;

		// Token: 0x04000B54 RID: 2900
		private static bool isInfoRestrictionNeeded;

		// Token: 0x04000B55 RID: 2901
		private static bool? isProcessWithUserInterface;

		// Token: 0x04000B56 RID: 2902
		private static int clientConfigurationVersion;

		// Token: 0x04000B57 RID: 2903
		private static IReadOnlyDictionary<int, object> clientConfiguration;
	}
}
