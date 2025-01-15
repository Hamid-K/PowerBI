using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000024 RID: 36
	public class UrlEndpoints
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00005444 File Offset: 0x00003644
		private static IEnumerable<UrlEndpointDefinition> GetEndpointsForUsage(AccountCredentials accountCredentials, UrlEndpointUsage targetUsage)
		{
			return new List<UrlEndpointDefinition>
			{
				new UrlEndpointDefinition(accountCredentials, "ReportServerWebService", "ReportServer", "http://+:80", UrlEndpointUsage.Dev | UrlEndpointUsage.Ssrs | UrlEndpointUsage.Pbirs | UrlEndpointUsage.InConfigFile),
				new UrlEndpointDefinition(accountCredentials, "ReportServerWebApp", "Reports", "http://+:80", UrlEndpointUsage.Dev | UrlEndpointUsage.Ssrs | UrlEndpointUsage.Pbirs | UrlEndpointUsage.InConfigFile),
				new UrlEndpointDefinition(accountCredentials, "ManagementService", "", "http://+:8082", UrlEndpointUsage.Prod | UrlEndpointUsage.Dev | UrlEndpointUsage.Ssrs),
				new UrlEndpointDefinition(accountCredentials, "ManagementService", "", "http://+:8083", UrlEndpointUsage.Prod | UrlEndpointUsage.Dev | UrlEndpointUsage.Pbirs),
				new UrlEndpointDefinition(accountCredentials, "PowerBIService", "powerbi", "http://+:80", UrlEndpointUsage.Dev | UrlEndpointUsage.Pbirs | UrlEndpointUsage.PortalConfigured),
				new UrlEndpointDefinition(accountCredentials, "OfficeService", "wopi", "http://+:80", UrlEndpointUsage.Dev | UrlEndpointUsage.Pbirs | UrlEndpointUsage.PortalConfigured | UrlEndpointUsage.RegisterOnUpgrade)
			}.Where((UrlEndpointDefinition endpoint) => (endpoint.UrlEndpointUsage & targetUsage) == targetUsage);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005524 File Offset: 0x00003724
		private static UrlApplications GetApplicationsForUsage(AccountCredentials accountCredentials, UrlEndpointUsage targetUsage)
		{
			return new UrlApplications(UrlEndpoints.GetEndpointsForUsage(accountCredentials, targetUsage).Select(new Func<UrlEndpointDefinition, Application>(UrlEndpoints.CreateApplication)).ToList<Application>());
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005548 File Offset: 0x00003748
		private static UrlEndpointUsage GetProductUsage(InstanceId instanceId)
		{
			if (!(instanceId.ToString() == "SSRS"))
			{
				return UrlEndpointUsage.Pbirs;
			}
			return UrlEndpointUsage.Ssrs;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000555F File Offset: 0x0000375F
		private static UrlEndpointUsage GetProductModeUsage(bool isDeveloperMode)
		{
			if (!isDeveloperMode)
			{
				return UrlEndpointUsage.Prod;
			}
			return UrlEndpointUsage.Dev;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005568 File Offset: 0x00003768
		public static UrlApplications GetEndpointsToRegister(bool isDeveloperMode, AccountCredentials accountCredentials, InstanceId instanceId, List<Application> appsFromConfig)
		{
			UrlEndpointUsage urlEndpointUsage = UrlEndpoints.GetProductUsage(instanceId) | UrlEndpoints.GetProductModeUsage(isDeveloperMode);
			UrlApplications applicationsForUsage = UrlEndpoints.GetApplicationsForUsage(accountCredentials, urlEndpointUsage);
			applicationsForUsage.Add(UrlEndpoints.GetEndpointsToRegisterOnUpgrade(accountCredentials, instanceId, appsFromConfig));
			return applicationsForUsage;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005598 File Offset: 0x00003798
		public static UrlApplications GetAllRegisteredEndpoints(AccountCredentials accountCredentials, InstanceId instanceId, IEnumerable<Application> appsFromConfig)
		{
			UrlApplications defaultUrlApplications = UrlEndpoints.GetDefaultUrlApplications(accountCredentials, instanceId);
			defaultUrlApplications.Add(appsFromConfig);
			defaultUrlApplications.Add(UrlEndpoints.GetEndpointsBasedOnPortalEndpoint(accountCredentials, instanceId, appsFromConfig, UrlEndpointUsage.None));
			return defaultUrlApplications;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000055B8 File Offset: 0x000037B8
		private static List<Application> GetEndpointsBasedOnPortalEndpoint(AccountCredentials accountCredentials, InstanceId instanceId, IEnumerable<Application> appsFromConfig, UrlEndpointUsage usageFilter)
		{
			List<Application> list = new List<Application>();
			if (instanceId.ToString().Equals("PBIRS", StringComparison.OrdinalIgnoreCase))
			{
				Application application = appsFromConfig.FirstOrDefault((Application app) => string.Equals(app.Name, "ReportServerWebApp"));
				if (application != null)
				{
					URL url = application.URLs.FirstOrDefault<URL>();
					if (url != null)
					{
						UrlEndpointUsage urlEndpointUsage = UrlEndpointUsage.Pbirs | UrlEndpointUsage.PortalConfigured | usageFilter;
						foreach (UrlEndpointDefinition urlEndpointDefinition in UrlEndpoints.GetEndpointsForUsage(accountCredentials, urlEndpointUsage))
						{
							urlEndpointDefinition.UrlString = url.UrlString;
							urlEndpointDefinition.AccountCredentials = accountCredentials;
							list.Add(UrlEndpoints.CreateApplication(urlEndpointDefinition));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005684 File Offset: 0x00003884
		private static List<Application> GetEndpointsToRegisterOnUpgrade(AccountCredentials accountCredentials, InstanceId instanceId, List<Application> appsFromConfig)
		{
			return UrlEndpoints.GetEndpointsBasedOnPortalEndpoint(accountCredentials, instanceId, appsFromConfig, UrlEndpointUsage.RegisterOnUpgrade);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005694 File Offset: 0x00003894
		public static UrlApplications GetDefaultUrlApplications(AccountCredentials accountCredentials, InstanceId instanceId)
		{
			UrlEndpointUsage urlEndpointUsage = UrlEndpoints.GetProductUsage(instanceId) | UrlEndpointUsage.Prod;
			return UrlEndpoints.GetApplicationsForUsage(accountCredentials, urlEndpointUsage);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000056B4 File Offset: 0x000038B4
		public static UrlApplications GetDeveloperApplicationsForConfig(AccountCredentials accountCredentials, InstanceId instanceId)
		{
			UrlEndpointUsage urlEndpointUsage = UrlEndpointUsage.Dev | UrlEndpointUsage.InConfigFile | UrlEndpoints.GetProductUsage(instanceId);
			return UrlEndpoints.GetApplicationsForUsage(accountCredentials, urlEndpointUsage);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000056D4 File Offset: 0x000038D4
		public static UrlApplications GetDeveloperApplications(AccountCredentials accountCredentials, InstanceId instanceId)
		{
			UrlEndpointUsage urlEndpointUsage = UrlEndpointUsage.Dev | UrlEndpoints.GetProductUsage(instanceId);
			return UrlEndpoints.GetApplicationsForUsage(accountCredentials, urlEndpointUsage);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000056F1 File Offset: 0x000038F1
		private static Application CreateApplication(UrlEndpointDefinition urlEndpointDefinition)
		{
			return new Application(urlEndpointDefinition.Name, urlEndpointDefinition.VirtualDirectory, new URL[] { URL.Create(urlEndpointDefinition.UrlString, urlEndpointDefinition.AccountCredentials) });
		}

		// Token: 0x040000EF RID: 239
		public const string ReportServerWebService = "ReportServerWebService";

		// Token: 0x040000F0 RID: 240
		public const string ReportServerWebApp = "ReportServerWebApp";

		// Token: 0x040000F1 RID: 241
		public const string ManagementService = "ManagementService";

		// Token: 0x040000F2 RID: 242
		public const string PowerBiService = "PowerBIService";

		// Token: 0x040000F3 RID: 243
		public const string OfficeService = "OfficeService";
	}
}
