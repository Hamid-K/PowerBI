using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Model;

namespace Microsoft.ReportingServices.Portal.Interfaces.Repositories
{
	// Token: 0x02000096 RID: 150
	public interface ISystemService
	{
		// Token: 0x060004E1 RID: 1249
		IQueryable<Policy> GetSystemPolicies(IPrincipal userPrincipal);

		// Token: 0x060004E2 RID: 1250
		void SetSystemPolicies(IPrincipal userPrincipal, IEnumerable<Policy> policies);

		// Token: 0x060004E3 RID: 1251
		IQueryable<Role> GetSystemRoles(IPrincipal userPrincipal);

		// Token: 0x060004E4 RID: 1252
		IQueryable<Schedule> GetSchedules(IPrincipal userPrincipal, int utcOffsetInMinutes);

		// Token: 0x060004E5 RID: 1253
		Schedule GetSchedule(IPrincipal userPrincipal, Guid key, int utcOffsetInMinutes);

		// Token: 0x060004E6 RID: 1254
		bool DeleteSchedule(IPrincipal userPrincipal, Guid key);

		// Token: 0x060004E7 RID: 1255
		bool AddSchedule(IPrincipal userPrincipal, Schedule schedule);

		// Token: 0x060004E8 RID: 1256
		bool UpdateSchedule(IPrincipal userPrincipal, Guid key, Schedule schedule);

		// Token: 0x060004E9 RID: 1257
		bool PauseSchedule(IPrincipal userPrincipal, Guid key);

		// Token: 0x060004EA RID: 1258
		bool ResumeSchedule(IPrincipal userPrincipal, Guid key);

		// Token: 0x060004EB RID: 1259
		string GetScheduleDescription(Schedule schedule, int utcOffsetInMinutes);

		// Token: 0x060004EC RID: 1260
		void PopulateScheduleDescriptions(IPrincipal userPrincipal, List<Subscription> subscriptions, int utcOffsetInMinutes);

		// Token: 0x060004ED RID: 1261
		void PopulateScheduleDescriptions(IPrincipal userPrincipal, List<CacheRefreshPlan> cachesRefreshPlans, int utcOffsetInMinutes);

		// Token: 0x060004EE RID: 1262
		string PopulateScheduleDescription(IPrincipal userPrincipal, ScheduleReference scheduleRef, int utcOffsetInMinutes);

		// Token: 0x060004EF RID: 1263
		Extension[] ListExtensions(IPrincipal userPrincipal, ExtensionType extensionType);

		// Token: 0x060004F0 RID: 1264
		void PopulateLocalizedExtensionNames(IPrincipal userPrincipal, List<Subscription> subscriptions);

		// Token: 0x060004F1 RID: 1265
		ExtensionParameter[] ListExtensionParameters(IPrincipal userPrincipal, string extensionName);

		// Token: 0x060004F2 RID: 1266
		ExtensionParameter[] ValidateExtensionSettings(IPrincipal userPrincipal, string extensionName, IEnumerable<ParameterValue> parameterValues);

		// Token: 0x060004F3 RID: 1267
		IQueryable<Property> GetSystemProperties(IPrincipal userPrincipal, IEnumerable<string> propertyNames);

		// Token: 0x060004F4 RID: 1268
		string GetServerProductName();

		// Token: 0x060004F5 RID: 1269
		string GetServerProductVersion();

		// Token: 0x060004F6 RID: 1270
		bool UpdateSystemProperties(IPrincipal userPrincipal, IEnumerable<Property> propertyValues);

		// Token: 0x060004F7 RID: 1271
		Task<IEnumerable<Property>> ValidateWopiUrlProperty(IEnumerable<Property> propertyValues, string discoveryPropertyName, string excelWopiUrlPropertyName);

		// Token: 0x060004F8 RID: 1272
		IEnumerable<CatalogItemType> GetRestrictedItemTypes();

		// Token: 0x060004F9 RID: 1273
		IEnumerable<string> GetRestrictedFeatures();

		// Token: 0x060004FA RID: 1274
		IEnumerable<string> GetAllowedSystemActions(IPrincipal userPrincipal);

		// Token: 0x060004FB RID: 1275
		bool IsBiServer();

		// Token: 0x060004FC RID: 1276
		string GetDefaultServerProductName();

		// Token: 0x060004FD RID: 1277
		string GetTimeZoneFullName();

		// Token: 0x060004FE RID: 1278
		string ValidatePowerBIMigrateUrl(IEnumerable<Property> properties, string powerBIMigrateUrlPropertyName);
	}
}
