using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200003C RID: 60
	public interface IProcessRenderingConfiguration
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001CF RID: 463
		string ConfigFilePath { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001D0 RID: 464
		RunningApplication CurrentApplication { get; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001D1 RID: 465
		bool CurrentApplicationHasCatalogAccess { get; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001D2 RID: 466
		Dictionary<string, EventExtension> EventTypes { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001D3 RID: 467
		ExtensionsConfiguration Extensions { get; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001D4 RID: 468
		string InstanceID { get; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001D5 RID: 469
		string InstanceName { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001D6 RID: 470
		bool IsCustomAuthEnabled { get; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001D7 RID: 471
		bool IsExtensibilityEnabled { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001D8 RID: 472
		bool IsReportBuilderAnonymousAccessEnabled { get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001D9 RID: 473
		bool IsSurrogatePresent { get; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001DA RID: 474
		bool IsWebServiceEnabled { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001DB RID: 475
		LogonMethod LogonMethod { get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001DC RID: 476
		int MaxActiveReqForOneUser { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001DD RID: 477
		long MaxMemoryThresholdMB { get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001DE RID: 478
		int MaxQueueThreads { get; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001DF RID: 479
		int MaxScheduleWait { get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001E0 RID: 480
		TimeSpan MaxTimedAppDomainUnload { get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001E1 RID: 481
		bool NonSqlDataSourcesEnabled { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001E2 RID: 482
		IOAuthConfiguration OAuthConfiguration { get; }

		// Token: 0x060001E3 RID: 483
		void EnsureCorrectEdition(IDbConnection sqlConn, string connectionString, bool checkRestrictedSkus);

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001E4 RID: 484
		string ReportServerVirtualDirectory { get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001E5 RID: 485
		int RequestCacheSlots { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001E6 RID: 486
		int RunningRequestsScavengerCycle { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001E7 RID: 487
		string ServerProductNameAndVersion { get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001E8 RID: 488
		string ServerProductVersion { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001E9 RID: 489
		string SurrogateDomain { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001EA RID: 490
		string SurrogatePassword { get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001EB RID: 491
		string SurrogateUserName { get; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001EC RID: 492
		string UrlRootCalculated { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001ED RID: 493
		IEnumerable<string> SupportedHyperlinkSchemes { get; }
	}
}
