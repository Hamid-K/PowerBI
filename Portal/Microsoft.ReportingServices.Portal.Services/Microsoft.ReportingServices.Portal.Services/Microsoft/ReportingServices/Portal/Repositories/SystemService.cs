using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.BIServer.Configuration;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Portal.Interfaces.Exceptions;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.SoapProxy;
using Microsoft.ReportingServices.Portal.Services;
using Microsoft.ReportingServices.Portal.Services.ODataExtensions;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Repositories
{
	// Token: 0x02000017 RID: 23
	internal sealed class SystemService : ISystemService
	{
		// Token: 0x06000129 RID: 297 RVA: 0x0000A321 File Offset: 0x00008521
		internal SystemService(ISoapRS2010Proxy soapProxy, ILogger logger)
		{
			if (soapProxy == null)
			{
				throw new ArgumentNullException("soapProxy");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._soapRS2010Proxy = soapProxy;
			this._logger = logger;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000A353 File Offset: 0x00008553
		public IEnumerable<CatalogItemType> GetRestrictedItemTypes()
		{
			if (SystemService._installedSku.IsExpired)
			{
				throw new EvaluationCopyExpiredException();
			}
			return SystemService._restrictedItemTypes;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000A36C File Offset: 0x0000856C
		public IEnumerable<string> GetRestrictedFeatures()
		{
			if (SystemService._installedSku.IsExpired)
			{
				throw new EvaluationCopyExpiredException();
			}
			return SystemService._restrictedSkuFeatures;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000A388 File Offset: 0x00008588
		public IEnumerable<string> GetAllowedSystemActions(IPrincipal userPrincipal)
		{
			GetSystemPermissionsAction getSystemPermissionsAction = ServicesUtil.CreateGetSystemPermissionsAction(ServicesUtil.CreateRsService(userPrincipal));
			getSystemPermissionsAction.Execute();
			return from r in getSystemPermissionsAction.ActionParameters.Operations.Cast<string>().ToList<string>()
				select r.ToString();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000A3DE File Offset: 0x000085DE
		public IQueryable<global::Model.Policy> GetSystemPolicies(IPrincipal userPrincipal)
		{
			GetSystemPoliciesAction getSystemPoliciesAction = ServicesUtil.CreateRsService(userPrincipal).GetSystemPoliciesAction;
			getSystemPoliciesAction.Execute();
			return getSystemPoliciesAction.ActionParameters.Policies.Select(new Func<Microsoft.ReportingServices.Library.Soap.Policy, global::Model.Policy>(PolicyExtensions.ToWebApiPolicy)).AsQueryable<global::Model.Policy>();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000A414 File Offset: 0x00008614
		public void SetSystemPolicies(IPrincipal userPrincipal, IEnumerable<global::Model.Policy> policies)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			Microsoft.ReportingServices.Library.Soap.Policy[] array = policies.Select(new Func<global::Model.Policy, Microsoft.ReportingServices.Library.Soap.Policy>(PolicyExtensions.ToSoapPolicy)).ToArray<Microsoft.ReportingServices.Library.Soap.Policy>();
			SetSystemPoliciesAction setSystemPoliciesAction = rsservice.SetSystemPoliciesAction;
			setSystemPoliciesAction.BatchID = Guid.Empty;
			setSystemPoliciesAction.ActionParameters.Policies = Microsoft.ReportingServices.Library.Soap.Policy.PolicyArrayToXml(array);
			setSystemPoliciesAction.Execute();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000A465 File Offset: 0x00008665
		public IQueryable<global::Model.Role> GetSystemRoles(IPrincipal userPrincipal)
		{
			ListRolesAction listRolesAction = ServicesUtil.CreateListRolesAction(ServicesUtil.CreateRsService(userPrincipal), SecurityScopeEnum.System);
			listRolesAction.Execute();
			return listRolesAction.ActionParameters.Roles.Select(new Func<Microsoft.ReportingServices.Library.Soap.Role, global::Model.Role>(RoleExtensions.ToWebApiRole)).AsQueryable<global::Model.Role>();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000A49C File Offset: 0x0000869C
		public IQueryable<global::Model.Schedule> GetSchedules(IPrincipal userPrincipal, int utcOffsetInMinutes)
		{
			ListSchedulesAction listSchedulesAction = ServicesUtil.CreateRsService(userPrincipal).ListSchedulesAction;
			listSchedulesAction.Execute();
			return listSchedulesAction.ActionParameters.Children.Select((Microsoft.ReportingServices.Library.Soap.Schedule x) => x.ToWebAPI(utcOffsetInMinutes)).AsQueryable<global::Model.Schedule>();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000A4E8 File Offset: 0x000086E8
		public global::Model.Schedule GetSchedule(IPrincipal userPrincipal, Guid key, int utcOffsetInMinutes)
		{
			GetSchedulePropertiesAction getSchedulePropertiesAction = ServicesUtil.CreateRsService(userPrincipal).GetSchedulePropertiesAction;
			getSchedulePropertiesAction.BatchID = Guid.Empty;
			getSchedulePropertiesAction.ActionParameters.ScheduleID = key.ToString();
			getSchedulePropertiesAction.Execute();
			return getSchedulePropertiesAction.ActionParameters.Schedule.ToWebAPI(utcOffsetInMinutes);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000A539 File Offset: 0x00008739
		public bool DeleteSchedule(IPrincipal userPrincipal, Guid key)
		{
			DeleteScheduleAction deleteScheduleAction = ServicesUtil.CreateRsService(userPrincipal).DeleteScheduleAction;
			deleteScheduleAction.BatchID = Guid.Empty;
			deleteScheduleAction.ActionParameters.ScheduleID = key.ToString();
			deleteScheduleAction.Execute();
			return true;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000A570 File Offset: 0x00008770
		public bool AddSchedule(IPrincipal userPrincipal, global::Model.Schedule schedule)
		{
			CreateScheduleAction createScheduleAction = ServicesUtil.CreateRsService(userPrincipal).CreateScheduleAction;
			createScheduleAction.BatchID = Guid.Empty;
			createScheduleAction.ActionParameters.Name = schedule.Name;
			createScheduleAction.ActionParameters.ScheduleDefinition = schedule.ToSoapAPI().Definition;
			createScheduleAction.Execute();
			return true;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000A5C0 File Offset: 0x000087C0
		public bool UpdateSchedule(IPrincipal userPrincipal, Guid key, global::Model.Schedule schedule)
		{
			SetSchedulePropertiesAction setSchedulePropertiesAction = ServicesUtil.CreateRsService(userPrincipal).SetSchedulePropertiesAction;
			setSchedulePropertiesAction.BatchID = Guid.Empty;
			setSchedulePropertiesAction.ActionParameters.Name = schedule.Name;
			setSchedulePropertiesAction.ActionParameters.ScheduleID = key.ToString();
			setSchedulePropertiesAction.ActionParameters.ScheduleDefinition = schedule.ToSoapAPI().Definition;
			setSchedulePropertiesAction.Execute();
			return true;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000A628 File Offset: 0x00008828
		public bool PauseSchedule(IPrincipal userPrincipal, Guid key)
		{
			PauseScheduleAction pauseScheduleAction = ServicesUtil.CreateRsService(userPrincipal).PauseScheduleAction;
			pauseScheduleAction.BatchID = Guid.Empty;
			pauseScheduleAction.ActionParameters.ScheduleID = key.ToString();
			pauseScheduleAction.Execute();
			return true;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000A65E File Offset: 0x0000885E
		public bool ResumeSchedule(IPrincipal userPrincipal, Guid key)
		{
			ResumeScheduleAction resumeScheduleAction = ServicesUtil.CreateRsService(userPrincipal).ResumeScheduleAction;
			resumeScheduleAction.BatchID = Guid.Empty;
			resumeScheduleAction.ActionParameters.ScheduleID = key.ToString();
			resumeScheduleAction.Execute();
			return true;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000A694 File Offset: 0x00008894
		public string GetScheduleDescription(global::Model.Schedule schedule, int utcOffsetInMinutes)
		{
			return schedule.ToSoapAPI().ToWebAPI(utcOffsetInMinutes).Description;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000A6A8 File Offset: 0x000088A8
		public void PopulateScheduleDescriptions(IPrincipal userPrincipal, List<global::Model.Subscription> subscriptions, int utcOffsetInMinutes)
		{
			foreach (global::Model.Subscription subscription in subscriptions)
			{
				subscription.ScheduleDescription = this.PopulateScheduleDescription(userPrincipal, subscription.Schedule, utcOffsetInMinutes);
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000A704 File Offset: 0x00008904
		public void PopulateScheduleDescriptions(IPrincipal userPrincipal, List<global::Model.CacheRefreshPlan> plans, int utcOffsetInMinutes)
		{
			foreach (global::Model.CacheRefreshPlan cacheRefreshPlan in plans)
			{
				cacheRefreshPlan.ScheduleDescription = this.PopulateScheduleDescription(userPrincipal, cacheRefreshPlan.Schedule, utcOffsetInMinutes);
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000A760 File Offset: 0x00008960
		public string PopulateScheduleDescription(IPrincipal userPrincipal, global::Model.ScheduleReference scheduleRef, int utcOffsetInMinutes)
		{
			global::Model.Schedule schedule = null;
			string text = string.Empty;
			if (scheduleRef != null)
			{
				if (!string.IsNullOrEmpty(scheduleRef.ScheduleID))
				{
					try
					{
						schedule = this.GetSchedule(userPrincipal, new Guid(scheduleRef.ScheduleID), utcOffsetInMinutes);
						goto IL_0067;
					}
					catch (Exception ex)
					{
						this._logger.Trace(TraceLevel.Info, string.Format("Message: {0}", ex.Message));
						goto IL_0067;
					}
				}
				if (scheduleRef.Definition != null)
				{
					schedule = new global::Model.Schedule
					{
						Definition = scheduleRef.Definition
					};
				}
			}
			IL_0067:
			if (schedule != null)
			{
				text = schedule.ToSoapAPI().ToWebAPI(utcOffsetInMinutes).Description;
			}
			return text;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000A7FC File Offset: 0x000089FC
		public global::Model.Extension[] ListExtensions(IPrincipal userPrincipal, ExtensionType extensionType)
		{
			global::Model.Extension[] extensions = this.GetExtensions(userPrincipal, extensionType);
			if (extensionType == ExtensionType.All)
			{
				List<global::Model.Extension> list = new List<global::Model.Extension>();
				foreach (global::Model.Extension extension in extensions)
				{
					list.Add(extension);
				}
				foreach (global::Model.Extension extension2 in this.GetExtensions(userPrincipal, ExtensionType.DeliveryUI))
				{
					extension2.ExtensionType = ExtensionType.DeliveryUI;
					list.Add(extension2);
				}
				return list.ToArray();
			}
			return extensions;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000A870 File Offset: 0x00008A70
		private global::Model.Extension[] GetExtensions(IPrincipal userPrincipal, ExtensionType extensionType)
		{
			string text = ((extensionType == ExtensionType.DeliveryUI) ? ExtensionType.Delivery.ToString() : extensionType.ToString());
			IEnumerable<Microsoft.SqlServer.ReportingServices2010.Extension> enumerable = this._soapRS2010Proxy.ListExtensions(userPrincipal, text).AsEnumerable<Microsoft.SqlServer.ReportingServices2010.Extension>();
			if (enumerable == null)
			{
				return null;
			}
			IEnumerable<global::Model.Extension> enumerable2 = enumerable.Select(new Func<Microsoft.SqlServer.ReportingServices2010.Extension, global::Model.Extension>(ExtensionExtension.ToWebApiModel));
			if (extensionType == ExtensionType.DeliveryUI)
			{
				IEnumerable<Microsoft.ReportingServices.Diagnostics.DeliveryExtension> deliveryUiExtensions = SystemService._rsConfig.Extensions.DeliveryUI.Cast<Microsoft.ReportingServices.Diagnostics.DeliveryExtension>();
				enumerable2 = enumerable2.Cast<global::Model.DeliveryExtension>().Where(delegate(global::Model.DeliveryExtension extension)
				{
					Microsoft.ReportingServices.Diagnostics.DeliveryExtension deliveryExtension = deliveryUiExtensions.FirstOrDefault((Microsoft.ReportingServices.Diagnostics.DeliveryExtension e) => e.Name == extension.Name);
					if (deliveryExtension != null)
					{
						extension.Visible = deliveryExtension.Visible;
						extension.DefaultDeliveryExtension = deliveryExtension.DefaultDeliveryExtension;
						return true;
					}
					return false;
				});
			}
			return enumerable2.ToArray<global::Model.Extension>();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000A910 File Offset: 0x00008B10
		public global::Model.ExtensionParameter[] ListExtensionParameters(IPrincipal userPrincipal, string extensionName)
		{
			if (ExtensionExtension.IsImmutableKnownDeliveryExtension(extensionName))
			{
				return new global::Model.ExtensionParameter[0];
			}
			return this._soapRS2010Proxy.ListExtensionParameters(userPrincipal, extensionName).Select(new Func<Microsoft.SqlServer.ReportingServices2010.ExtensionParameter, global::Model.ExtensionParameter>(ExtensionParameterExtensions.ToWebAPI)).ToArray<global::Model.ExtensionParameter>();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000A944 File Offset: 0x00008B44
		public global::Model.ExtensionParameter[] ValidateExtensionSettings(IPrincipal userPrincipal, string extensionName, IEnumerable<global::Model.ParameterValue> parameterValues)
		{
			if (ExtensionExtension.IsImmutableKnownDeliveryExtension(extensionName))
			{
				return new global::Model.ExtensionParameter[0];
			}
			Microsoft.SqlServer.ReportingServices2010.ParameterValueOrFieldReference[] array = parameterValues.Select(new Func<global::Model.ParameterValue, Microsoft.SqlServer.ReportingServices2010.ParameterValueOrFieldReference>(ParameterValueExtensions.ToSoapParameterValueOrFieldReference)).ToArray<Microsoft.SqlServer.ReportingServices2010.ParameterValueOrFieldReference>();
			return this._soapRS2010Proxy.ValidateExtensionSettings(userPrincipal, extensionName, array).Select(new Func<Microsoft.SqlServer.ReportingServices2010.ExtensionParameter, global::Model.ExtensionParameter>(ExtensionParameterExtensions.ToWebAPI)).ToArray<global::Model.ExtensionParameter>();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000A99C File Offset: 0x00008B9C
		public void PopulateLocalizedExtensionNames(IPrincipal userPrincipal, List<global::Model.Subscription> subscriptions)
		{
			global::Model.Extension[] array = this.ListExtensions(userPrincipal, ExtensionType.Delivery);
			if (array != null)
			{
				using (List<global::Model.Subscription>.Enumerator enumerator = subscriptions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						global::Model.Subscription subscription = enumerator.Current;
						global::Model.Extension extension = array.FirstOrDefault((global::Model.Extension x) => x.Name == subscription.DeliveryExtension);
						if (extension != null)
						{
							subscription.LocalizedDeliveryExtensionName = extension.LocalizedName;
						}
					}
				}
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000AA24 File Offset: 0x00008C24
		public IQueryable<global::Model.Property> GetSystemProperties(IPrincipal userPrincipal, IEnumerable<string> propertyNames)
		{
			GetSystemPropertiesAction getSystemPropertiesAction = ServicesUtil.CreateRsService(userPrincipal).GetSystemPropertiesAction;
			getSystemPropertiesAction.ActionParameters.RequestedProperties = propertyNames.Select((string propertyName) => new Microsoft.ReportingServices.Library.Soap.Property
			{
				Name = propertyName
			}).ToArray<Microsoft.ReportingServices.Library.Soap.Property>();
			getSystemPropertiesAction.Execute();
			return getSystemPropertiesAction.ActionParameters.SystemProperties.Select(new Func<Microsoft.ReportingServices.Library.Soap.Property, global::Model.Property>(PropertyExtensions.ToWebAPI)).AsQueryable<global::Model.Property>();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000AA97 File Offset: 0x00008C97
		public string GetServerProductName()
		{
			if (!this.IsBiServer())
			{
				return RSConfiguration.ProductNameSSRS;
			}
			return RSConfiguration.ProductNamePBIRS;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000AAAC File Offset: 0x00008CAC
		public string GetServerProductVersion()
		{
			string currentProductVersion = InstanceRegistry.GetCurrentProductVersion();
			Logger.Info("Fetched ProductVersion {0}", new object[] { currentProductVersion });
			return currentProductVersion;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000AAD4 File Offset: 0x00008CD4
		public bool UpdateSystemProperties(IPrincipal userPrincipal, IEnumerable<global::Model.Property> propertyValues)
		{
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			SetSystemPropertiesAction setSystemPropertiesAction = ServicesUtil.CreateRsService(userPrincipal).SetSystemPropertiesAction;
			Microsoft.ReportingServices.Library.Soap.Property[] array = propertyValues.Select(new Func<global::Model.Property, Microsoft.ReportingServices.Library.Soap.Property>(PropertyExtensions.ToSoapAPI)).ToArray<Microsoft.ReportingServices.Library.Soap.Property>();
			setSystemPropertiesAction.ActionParameters.SystemProperties = array;
			setSystemPropertiesAction.Execute();
			return true;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000AB24 File Offset: 0x00008D24
		public async Task<IEnumerable<global::Model.Property>> ValidateWopiUrlProperty(IEnumerable<global::Model.Property> propertyValues, string discoveryPropertyName, string excelWopiUrlPropertyName)
		{
			global::Model.Property property = propertyValues.SingleOrDefault((global::Model.Property p) => p.Name.Equals(discoveryPropertyName));
			IEnumerable<global::Model.Property> enumerable;
			if (property != null)
			{
				string text = null;
				if (!string.IsNullOrEmpty(property.Value))
				{
					text = await this.GetExcelWopiUrl(property.Value);
				}
				enumerable = propertyValues.Where((global::Model.Property p) => !p.Name.Equals(excelWopiUrlPropertyName)).Concat(new global::Model.Property[]
				{
					new global::Model.Property
					{
						Name = excelWopiUrlPropertyName,
						Value = text
					}
				});
			}
			else
			{
				enumerable = propertyValues;
			}
			return enumerable;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000AB84 File Offset: 0x00008D84
		private async Task<string> GetExcelWopiUrl(string officeOnlineDiscoveryUrl)
		{
			string text;
			try
			{
				Uri uri = await new OfficeOnlineServerConfiguration(new Uri(officeOnlineDiscoveryUrl)).GetExcelWopiUrl();
				text = ((uri != null) ? uri.ToString() : null);
			}
			catch (HttpRequestException ex)
			{
				throw new ExcelWorkbookWopiInvalidUrlException(SR.Error_InvalidExcelWopiUrl, ex);
			}
			catch (SystemException ex2)
			{
				throw new ExcelWorkbookWopiInvalidUrlException(SR.Error_InvalidExcelWopiUrl, ex2);
			}
			return text;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000ABCC File Offset: 0x00008DCC
		public string ValidatePowerBIMigrateUrl(IEnumerable<global::Model.Property> properties, string powerBIMigrateUrlPropertyName)
		{
			if (!properties.Any((global::Model.Property p) => p.Name.Equals(powerBIMigrateUrlPropertyName)))
			{
				return "";
			}
			Uri uri;
			if (!Uri.TryCreate(properties.FirstOrDefault((global::Model.Property p) => p.Name.Equals(powerBIMigrateUrlPropertyName)).Value, UriKind.Absolute, out uri))
			{
				throw new PowerBIMigrateInvalidUrlException(SR.Error_InvalidPowerBIMigrateUrl);
			}
			if (!new Regex("http.+((.powerbi.com)|(.microsoft.com)|(.windows.net)|(.powerbigov.us)|(.powerbi.cn)|(.powerbi.de))", RegexOptions.IgnoreCase | RegexOptions.Compiled).IsMatch(uri.AbsoluteUri))
			{
				throw new PowerBIMigrateInvalidUrlException(SR.Error_InvalidPowerBIMigrateUrl);
			}
			return uri.AbsoluteUri;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000AC58 File Offset: 0x00008E58
		public bool IsBiServer()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("BI_SERVER");
			return environmentVariable != null && !string.Equals(environmentVariable, "false", StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000AC84 File Offset: 0x00008E84
		public string GetDefaultServerProductName()
		{
			if (!this.IsBiServer())
			{
				return "SQL Server Reporting Services";
			}
			return "Power BI Report Server";
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000AC9C File Offset: 0x00008E9C
		public string GetTimeZoneFullName()
		{
			string text = (TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now) ? TimeZoneInfo.Local.DaylightName : TimeZoneInfo.Local.StandardName);
			TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
			return string.Format("(UTC{0:+00;-00;+00}:{1:00}) {2}", utcOffset.Hours, utcOffset.Minutes, text);
		}

		// Token: 0x04000068 RID: 104
		private static readonly RSConfiguration _rsConfig = Globals.Configuration;

		// Token: 0x04000069 RID: 105
		private static readonly SkuInfo _installedSku = Sku.GetSkuInfo(SystemService._rsConfig.InstanceID);

		// Token: 0x0400006A RID: 106
		private static readonly IEnumerable<CatalogItemType> _restrictedItemTypes = FeaturesUtil.GetRestrictedItems(SystemService._installedSku.Sku);

		// Token: 0x0400006B RID: 107
		private static readonly IEnumerable<string> _restrictedSkuFeatures = from r in FeaturesUtil.GetRestrictedFeatures(SystemService._installedSku.Sku)
			select r.ToString();

		// Token: 0x0400006C RID: 108
		private readonly ISoapRS2010Proxy _soapRS2010Proxy;

		// Token: 0x0400006D RID: 109
		private readonly ILogger _logger;

		// Token: 0x0400006E RID: 110
		private const string PbirsProductName = "Power BI Report Server";

		// Token: 0x0400006F RID: 111
		private const string SqlrsProductName = "SQL Server Reporting Services";
	}
}
