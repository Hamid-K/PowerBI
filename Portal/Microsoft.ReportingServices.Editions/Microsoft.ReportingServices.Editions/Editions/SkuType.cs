using System;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x02000014 RID: 20
	public enum SkuType
	{
		// Token: 0x0400005E RID: 94
		[SkuStrings]
		[SkuDetails(Product = ProductType.None)]
		[SkuSupportsDatabasesCreatedBy]
		[SkuCanUpgradeTo]
		None,
		// Token: 0x0400005F RID: 95
		[SkuStrings(FullName = "SQL Server Evaluation", ShortName = "Evaluation", CommandLineName = "EVAL", PkConfigName = "EVAL")]
		[SkuDetails(Product = ProductType.SqlServerReportingServices, Guid = "18F508AC-AE35-4D36-8C8C-C1AD2B86B9EB", RequiresKey = false)]
		[SkuCanUpgradeTo(new SkuType[]
		{
			SkuType.SsrsDeveloper,
			SkuType.SsrsWeb,
			SkuType.SsrsStandard,
			SkuType.SsrsEnterprise,
			SkuType.SsrsEnterpriseCore
		})]
		[SkuSupportsDatabasesCreatedBy(new SkuType[]
		{
			SkuType.SsrsEvaluation,
			SkuType.SsrsDeveloper,
			SkuType.SsrsExpress,
			SkuType.SsrsWeb,
			SkuType.SsrsStandard,
			SkuType.SsrsEnterprise,
			SkuType.SsrsEnterpriseCore
		})]
		SsrsEvaluation = 20,
		// Token: 0x04000060 RID: 96
		[SkuStrings(FullName = "SQL Server Developer", ShortName = "Developer", CommandLineName = "DEV", PkConfigName = "DEVELOPER")]
		[SkuDetails(Product = ProductType.SqlServerReportingServices, Guid = "DEE16405-1594-4D48-90BC-DBDAA97F25E0", RequiresKey = false)]
		[SkuCanUpgradeTo(new SkuType[]
		{
			SkuType.SsrsWeb,
			SkuType.SsrsStandard,
			SkuType.SsrsEnterprise,
			SkuType.SsrsEnterpriseCore
		})]
		[SkuSupportsDatabasesCreatedBy(new SkuType[]
		{
			SkuType.SsrsEvaluation,
			SkuType.SsrsDeveloper,
			SkuType.SsrsExpress,
			SkuType.SsrsWeb,
			SkuType.SsrsStandard,
			SkuType.SsrsEnterprise,
			SkuType.SsrsEnterpriseCore
		})]
		SsrsDeveloper,
		// Token: 0x04000061 RID: 97
		[SkuStrings(FullName = "SQL Server Express", ShortName = "Express", CommandLineName = "EXPR", PkConfigName = "EXPRESS_ADVANCED")]
		[SkuDetails(Product = ProductType.SqlServerReportingServices, Guid = "8CD588A6-811C-40AD-B939-24C63CF6C77C", RequiresKey = false)]
		[SkuCanUpgradeTo(new SkuType[]
		{
			SkuType.SsrsWeb,
			SkuType.SsrsStandard,
			SkuType.SsrsDeveloper,
			SkuType.SsrsEnterprise,
			SkuType.SsrsEnterpriseCore
		})]
		[SkuSupportsDatabasesCreatedBy(new SkuType[]
		{
			SkuType.SsrsEvaluation,
			SkuType.SsrsDeveloper,
			SkuType.SsrsExpress
		})]
		SsrsExpress,
		// Token: 0x04000062 RID: 98
		[SkuStrings(FullName = "SQL Server Web", ShortName = "Web", CommandLineName = "WEB", PkConfigName = "WEB")]
		[SkuDetails(Product = ProductType.SqlServerReportingServices, Guid = "ECD8539D-B652-4141-8CFF-B7674C856D8F", RequiresKey = true)]
		[SkuCanUpgradeTo(new SkuType[]
		{
			SkuType.SsrsStandard,
			SkuType.SsrsEnterprise,
			SkuType.SsrsEnterpriseCore
		})]
		[SkuSupportsDatabasesCreatedBy(new SkuType[]
		{
			SkuType.SsrsEvaluation,
			SkuType.SsrsDeveloper,
			SkuType.SsrsExpress,
			SkuType.SsrsWeb
		})]
		SsrsWeb,
		// Token: 0x04000063 RID: 99
		[SkuStrings(FullName = "SQL Server Standard", ShortName = "Standard", CommandLineName = "STANDARD", PkConfigName = "STANDARD")]
		[SkuDetails(Product = ProductType.SqlServerReportingServices, Guid = "F21BFA60-1FAB-42F2-9A8C-4D03C6E98C34", RequiresKey = true)]
		[SkuCanUpgradeTo(new SkuType[]
		{
			SkuType.SsrsEnterprise,
			SkuType.SsrsEnterpriseCore
		})]
		[SkuSupportsDatabasesCreatedBy(new SkuType[]
		{
			SkuType.SsrsEvaluation,
			SkuType.SsrsDeveloper,
			SkuType.SsrsExpress,
			SkuType.SsrsWeb,
			SkuType.SsrsStandard
		})]
		SsrsStandard,
		// Token: 0x04000064 RID: 100
		[SkuStrings(FullName = "SQL Server Enterprise", ShortName = "Enterprise", CommandLineName = "ENTERPRISE", PkConfigName = "ENTERPRISE")]
		[SkuDetails(Product = ProductType.SqlServerReportingServices, Guid = "0145C5C1-D24A-4141-9815-2FF76DDF7CEC", RequiresKey = true)]
		[SkuCanUpgradeTo(new SkuType[] { SkuType.SsrsEnterpriseCore })]
		[SkuSupportsDatabasesCreatedBy(new SkuType[]
		{
			SkuType.SsrsEvaluation,
			SkuType.SsrsDeveloper,
			SkuType.SsrsExpress,
			SkuType.SsrsWeb,
			SkuType.SsrsStandard,
			SkuType.SsrsEnterprise,
			SkuType.SsrsEnterpriseCore
		})]
		SsrsEnterprise,
		// Token: 0x04000065 RID: 101
		[SkuStrings(FullName = "SQL Server Enterprise (Core-Based Licensing)", ShortName = "Enterprise", CommandLineName = "ENTERPRISECORE", PkConfigName = "ENTERPRISE CORE")]
		[SkuDetails(Product = ProductType.SqlServerReportingServices, Guid = "A399186B-AB71-4251-B343-6681EF496FAF", RequiresKey = true)]
		[SkuCanUpgradeTo(new SkuType[] { SkuType.SsrsEnterprise })]
		[SkuSupportsDatabasesCreatedBy(new SkuType[]
		{
			SkuType.SsrsEvaluation,
			SkuType.SsrsDeveloper,
			SkuType.SsrsExpress,
			SkuType.SsrsWeb,
			SkuType.SsrsStandard,
			SkuType.SsrsEnterprise,
			SkuType.SsrsEnterpriseCore
		})]
		SsrsEnterpriseCore,
		// Token: 0x04000066 RID: 102
		[SkuStrings(FullName = "Power BI Report Server - Evaluation", ShortName = "PBIRS Evaluation", CommandLineName = "EVAL", PkConfigName = "EVAL")]
		[SkuDetails(Product = ProductType.PowerBiReportServer, Guid = "519A9098-0389-47AB-BA02-25AB120AB706", RequiresKey = false)]
		[SkuCanUpgradeTo(new SkuType[]
		{
			SkuType.PbirsDeveloper,
			SkuType.PbirsPremium,
			SkuType.PbirsSqlServerEeSa
		})]
		[SkuSupportsDatabasesCreatedBy(new SkuType[]
		{
			SkuType.SsrsEvaluation,
			SkuType.SsrsDeveloper,
			SkuType.SsrsExpress,
			SkuType.SsrsWeb,
			SkuType.SsrsStandard,
			SkuType.SsrsEnterprise,
			SkuType.SsrsEnterpriseCore,
			SkuType.PbirsEvaluation,
			SkuType.PbirsDeveloper,
			SkuType.PbirsPremium,
			SkuType.PbirsSqlServerEeSa
		})]
		PbirsEvaluation = 30,
		// Token: 0x04000067 RID: 103
		[SkuStrings(FullName = "Power BI Report Server - Developer", ShortName = "PBIRS Developer", CommandLineName = "DEV", PkConfigName = "DEVELOPER")]
		[SkuDetails(Product = ProductType.PowerBiReportServer, Guid = "78426786-77FE-462C-B921-6AE8F1AB9062", RequiresKey = false)]
		[SkuCanUpgradeTo(new SkuType[]
		{
			SkuType.PbirsPremium,
			SkuType.PbirsSqlServerEeSa
		})]
		[SkuSupportsDatabasesCreatedBy(new SkuType[]
		{
			SkuType.SsrsEvaluation,
			SkuType.SsrsDeveloper,
			SkuType.SsrsExpress,
			SkuType.SsrsWeb,
			SkuType.SsrsStandard,
			SkuType.SsrsEnterprise,
			SkuType.SsrsEnterpriseCore,
			SkuType.PbirsEvaluation,
			SkuType.PbirsDeveloper,
			SkuType.PbirsPremium,
			SkuType.PbirsSqlServerEeSa
		})]
		PbirsDeveloper,
		// Token: 0x04000068 RID: 104
		[SkuStrings(FullName = "Power BI Report Server - Premium", ShortName = "PBIRS Premium", CommandLineName = "PREMIUM", PkConfigName = "PBI PREMIUM")]
		[SkuDetails(Product = ProductType.PowerBiReportServer, Guid = "6B2E5C11-3AB7-4F3F-88CD-15FD73BF45AB", RequiresKey = true)]
		[SkuCanUpgradeTo(new SkuType[] { SkuType.PbirsSqlServerEeSa })]
		[SkuSupportsDatabasesCreatedBy(new SkuType[]
		{
			SkuType.SsrsEvaluation,
			SkuType.SsrsDeveloper,
			SkuType.SsrsExpress,
			SkuType.SsrsWeb,
			SkuType.SsrsStandard,
			SkuType.SsrsEnterprise,
			SkuType.SsrsEnterpriseCore,
			SkuType.PbirsEvaluation,
			SkuType.PbirsDeveloper,
			SkuType.PbirsPremium,
			SkuType.PbirsSqlServerEeSa
		})]
		PbirsPremium,
		// Token: 0x04000069 RID: 105
		[SkuStrings(FullName = "Power BI Report Server - SQL Server Enterprise with Software Assurance", ShortName = "PBIRS SQL EESA", CommandLineName = "SQLEESA", PkConfigName = "SQL SERVER EE SA")]
		[SkuDetails(Product = ProductType.PowerBiReportServer, Guid = "0361A3D4-EEAA-4033-9033-4F42BE2ED7AF", RequiresKey = true)]
		[SkuCanUpgradeTo(new SkuType[] { SkuType.PbirsPremium })]
		[SkuSupportsDatabasesCreatedBy(new SkuType[]
		{
			SkuType.SsrsEvaluation,
			SkuType.SsrsDeveloper,
			SkuType.SsrsExpress,
			SkuType.SsrsWeb,
			SkuType.SsrsStandard,
			SkuType.SsrsEnterprise,
			SkuType.SsrsEnterpriseCore,
			SkuType.PbirsEvaluation,
			SkuType.PbirsDeveloper,
			SkuType.PbirsPremium,
			SkuType.PbirsSqlServerEeSa
		})]
		PbirsSqlServerEeSa
	}
}
