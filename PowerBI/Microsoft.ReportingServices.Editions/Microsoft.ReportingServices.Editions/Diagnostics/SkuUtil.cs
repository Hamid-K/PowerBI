using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.ReportingServices.Editions;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000003 RID: 3
	internal static class SkuUtil
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static bool IsWebOrHigher(SkuType sku)
		{
			return sku == SkuType.SsrsWeb || SkuUtil.IsStandardOrHigher(sku);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205F File Offset: 0x0000025F
		private static bool IsStandardOrHigher(SkuType sku)
		{
			return sku == SkuType.SsrsStandard || SkuUtil.IsEnterpriseOrHigher(sku);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000206E File Offset: 0x0000026E
		private static bool IsEvaluationOrDeveloper(SkuType sku)
		{
			return sku == SkuType.SsrsEvaluation || sku == SkuType.SsrsDeveloper || sku == SkuType.PbirsEvaluation || sku == SkuType.PbirsDeveloper;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002086 File Offset: 0x00000286
		public static bool IsEnterpriseOrHigher(SkuType sku)
		{
			return sku == SkuType.SsrsDeveloper || sku == SkuType.PbirsDeveloper || sku == SkuType.SsrsEvaluation || sku == SkuType.PbirsEvaluation || sku == SkuType.SsrsEnterprise || sku == SkuType.SsrsEnterpriseCore || sku == SkuType.PbirsPremium || sku == SkuType.PbirsSqlServerEeSa;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020B2 File Offset: 0x000002B2
		public static bool IsSSRSSku(SkuType sku)
		{
			return sku == SkuType.SsrsDeveloper || sku == SkuType.SsrsEnterprise || sku == SkuType.SsrsEnterpriseCore || sku == SkuType.SsrsEvaluation || sku == SkuType.SsrsExpress || sku == SkuType.SsrsStandard || sku == SkuType.SsrsWeb;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020D9 File Offset: 0x000002D9
		public static bool IsPBIRSSku(SkuType sku)
		{
			return sku == SkuType.PbirsDeveloper || sku == SkuType.PbirsEvaluation || sku == SkuType.PbirsPremium || sku == SkuType.PbirsSqlServerEeSa;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F4 File Offset: 0x000002F4
		public static SqlServerSkuType GetSqlSku(IDbConnection connection)
		{
			IDbCommand dbCommand = connection.CreateCommand();
			dbCommand.CommandText = "SELECT SERVERPROPERTY('Edition')";
			dbCommand.CommandType = CommandType.Text;
			object obj = dbCommand.ExecuteScalar();
			if (obj == null)
			{
				return SqlServerSkuType.None;
			}
			return SkuUtil.DbSkuFromString((string)obj);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002134 File Offset: 0x00000334
		public static SkuType GetRsDatabaseSkuType(IDbConnection connection, string databaseName)
		{
			string text = string.Format("SELECT * FROM [{0}].INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'ProductInfoHistory'", databaseName);
			IDbCommand dbCommand = connection.CreateCommand();
			dbCommand.CommandText = text;
			dbCommand.CommandType = CommandType.Text;
			if (dbCommand.ExecuteScalar() == null)
			{
				return SkuType.None;
			}
			string text2 = string.Format("SELECT TOP 1 Sku from [{0}].dbo.ProductInfoHistory order by DateTime DESC", databaseName);
			IDbCommand dbCommand2 = connection.CreateCommand();
			dbCommand2.CommandText = text2;
			dbCommand2.CommandType = CommandType.Text;
			object obj = dbCommand2.ExecuteScalar();
			SkuType skuType = SkuType.None;
			if (obj != null)
			{
				try
				{
					skuType = (SkuType)Enum.Parse(typeof(SkuType), (string)obj);
				}
				catch (Exception)
				{
				}
			}
			return skuType;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021C8 File Offset: 0x000003C8
		public static SkuUtil.SkuVerificationErrorCode EnsureCorrectEdition(SkuType rsSku, SqlServerSkuType sqlSku, string connectionString, SkuUtil.LocalDbServerVerifier localDbServerVerifier, bool checkRestrictedSkus)
		{
			if (!SkuUtil.IsStandardOrHigher(rsSku) && !SkuUtil.IsLocal(connectionString, localDbServerVerifier))
			{
				return SkuUtil.SkuVerificationErrorCode.LocalConnectionRequired;
			}
			IEnumerable<SqlServerSkuType> enumerable;
			IEnumerable<SqlServerSkuType> supportedDatabaseSkus = SkuUtil.GetSupportedDatabaseSkus(rsSku, out enumerable);
			if (supportedDatabaseSkus.Any<SqlServerSkuType>() && !supportedDatabaseSkus.Contains(sqlSku))
			{
				return SkuUtil.SkuVerificationErrorCode.DatabaseSkuRequired;
			}
			if (checkRestrictedSkus && enumerable.Any<SqlServerSkuType>() && enumerable.Contains(sqlSku))
			{
				return SkuUtil.SkuVerificationErrorCode.DatabaseSkuRestricted;
			}
			return SkuUtil.SkuVerificationErrorCode.Success;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000221C File Offset: 0x0000041C
		private static bool IsPrivateBuild()
		{
			return true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002220 File Offset: 0x00000420
		internal static IEnumerable<SqlServerSkuType> GetSupportedDatabaseSkus(SkuType reportServerSku, out IEnumerable<SqlServerSkuType> restrictedSkus)
		{
			List<SqlServerSkuType> list = new List<SqlServerSkuType>();
			List<SqlServerSkuType> list2 = new List<SqlServerSkuType>();
			switch (reportServerSku)
			{
			case SkuType.SsrsExpress:
				list.Add(SqlServerSkuType.Express);
				break;
			case SkuType.SsrsWeb:
				list.Add(SqlServerSkuType.Web);
				break;
			case SkuType.SsrsStandard:
			case SkuType.SsrsEnterprise:
			case SkuType.SsrsEnterpriseCore:
			case SkuType.PbirsPremium:
			case SkuType.PbirsSqlServerEeSa:
				list2.Add(SqlServerSkuType.Developer);
				list2.Add(SqlServerSkuType.Evaluation);
				break;
			}
			if (SkuUtil.IsStandardOrHigher(reportServerSku))
			{
				list2.Add(SqlServerSkuType.Workgroup);
				list2.Add(SqlServerSkuType.Express);
				list2.Add(SqlServerSkuType.Web);
			}
			if (SkuUtil.IsEvaluationOrDeveloper(reportServerSku) && list2.Contains(SqlServerSkuType.Express))
			{
				list2.Remove(SqlServerSkuType.Express);
			}
			restrictedSkus = list2;
			return list;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022D0 File Offset: 0x000004D0
		internal static SkuType SkuFromString(string edition)
		{
			IEnumerable<SkuType> enumerable = ((SkuType[])Enum.GetValues(typeof(SkuType))).Where((SkuType s) => s.GetAttribute<SkuStrings>().FullName.Equals(edition, StringComparison.InvariantCultureIgnoreCase));
			if (enumerable.Count<SkuType>() != 0)
			{
				return enumerable.First<SkuType>();
			}
			string edition2 = edition;
			if (edition2 == "SQL Server Express with Advanced Services")
			{
				return SkuType.SsrsExpress;
			}
			if (!(edition2 == "SQL Server Enterprise: Core-Based Licensing"))
			{
				return SkuType.None;
			}
			return SkuType.SsrsEnterpriseCore;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000234C File Offset: 0x0000054C
		internal static SqlServerSkuType DbSkuFromString(string edition)
		{
			if (edition.ToUpperInvariant().Contains("EVALUATION") || edition.ToUpperInvariant().Contains("BETA"))
			{
				return SqlServerSkuType.Evaluation;
			}
			if (edition.ToUpperInvariant().Contains("CORE"))
			{
				return SqlServerSkuType.EnterpriseCore;
			}
			if (edition.ToUpperInvariant().Contains("DEVELOPER"))
			{
				return SqlServerSkuType.Developer;
			}
			if (edition.ToUpperInvariant().Contains("ENTERPRISE"))
			{
				return SqlServerSkuType.Enterprise;
			}
			if (edition.ToUpperInvariant().Contains("SQL AZURE"))
			{
				return SqlServerSkuType.SqlAzure;
			}
			if (edition.ToUpperInvariant().Contains("STANDARD"))
			{
				return SqlServerSkuType.Standard;
			}
			if (edition.ToUpperInvariant().Contains("WORKGROUP"))
			{
				return SqlServerSkuType.Workgroup;
			}
			if (edition.ToUpperInvariant().Contains("EXPRESS"))
			{
				return SqlServerSkuType.Express;
			}
			if (edition.ToUpperInvariant().Contains("WEB"))
			{
				return SqlServerSkuType.Web;
			}
			if (edition.ToUpperInvariant().Contains("DATA CENTER") || edition.ToUpperInvariant().Contains("DATACENTER"))
			{
				return SqlServerSkuType.DataCenter;
			}
			if (edition.ToUpperInvariant().Contains("BUSINESS INTELLIGENCE") || edition.ToUpperInvariant().Contains("BUSINESSINTELLIGENCE"))
			{
				return SqlServerSkuType.BusinessIntelligence;
			}
			return SqlServerSkuType.None;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002470 File Offset: 0x00000670
		private static bool IsLocal(string sqlConnectionString, SkuUtil.LocalDbServerVerifier localDbServerVerifier)
		{
			sqlConnectionString = sqlConnectionString.ToUpperInvariant();
			foreach (string text in new string[] { "DATA SOURCE", "SERVER", "ADDRESS", "ADDR", "NETWORK ADDRESS" })
			{
				int num = sqlConnectionString.IndexOf(text, StringComparison.Ordinal);
				if (num != -1)
				{
					int num2 = sqlConnectionString.IndexOf("=", num, StringComparison.Ordinal);
					if (num2 != -1 && num2 != sqlConnectionString.Length - 1)
					{
						int num3 = num2 + 1;
						int num4 = sqlConnectionString.IndexOf(";", num3, StringComparison.Ordinal);
						if (num4 == -1)
						{
							num4 = sqlConnectionString.Length - 1;
						}
						string machineNameFromSqlInstanceName = SkuUtil.GetMachineNameFromSqlInstanceName(sqlConnectionString.Substring(num3, num4 - num3));
						return localDbServerVerifier(machineNameFromSqlInstanceName);
					}
				}
			}
			return false;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002538 File Offset: 0x00000738
		internal static string GetMachineNameFromSqlInstanceName(string sqlInstanceName)
		{
			string text = sqlInstanceName;
			int num = sqlInstanceName.IndexOf('\\');
			if (num != -1)
			{
				text = sqlInstanceName.Substring(0, num);
			}
			num = text.IndexOf(',');
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			return text;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002574 File Offset: 0x00000774
		public static bool IsFeatureEnabled(SkuType sku, RestrictedFeatures feature, out bool isFeatureExpected)
		{
			isFeatureExpected = true;
			switch (feature)
			{
			case RestrictedFeatures.NonSqlDataSources:
			case RestrictedFeatures.OtherSkuDatasources:
			case RestrictedFeatures.RemoteDataSources:
			case RestrictedFeatures.Caching:
			case RestrictedFeatures.ExecutionSnapshots:
			case RestrictedFeatures.History:
			case RestrictedFeatures.Delivery:
			case RestrictedFeatures.Scheduling:
			case RestrictedFeatures.Extensibility:
			case RestrictedFeatures.Subscriptions:
			case RestrictedFeatures.CustomRolesSecurity:
			case RestrictedFeatures.ModelItemSecurity:
			case RestrictedFeatures.DynamicDrillthrough:
			case RestrictedFeatures.EventGeneration:
			case RestrictedFeatures.ComponentLibrary:
			case RestrictedFeatures.SharedDataset:
				return SkuUtil.IsStandardOrHigher(sku);
			case RestrictedFeatures.CustomAuth:
				return true;
			case RestrictedFeatures.Sharepoint:
			case RestrictedFeatures.DataAlerting:
			case RestrictedFeatures.Crescent:
			case RestrictedFeatures.PowerBIPinning:
			case RestrictedFeatures.CommentAlerting:
				return false;
			case RestrictedFeatures.ScaleOut:
			case RestrictedFeatures.DataDrivenSubscriptions:
			case RestrictedFeatures.NoCpuThrottling:
			case RestrictedFeatures.NoMemoryThrottling:
			case RestrictedFeatures.KpiItems:
			case RestrictedFeatures.Branding:
				return SkuUtil.IsEnterpriseOrHigher(sku);
			case RestrictedFeatures.ReportBuilder:
				return SkuUtil.IsWebOrHigher(sku);
			case RestrictedFeatures.PowerBIRendering:
			case RestrictedFeatures.ExcelRendering:
				return SkuUtil.IsPBIRSSku(sku);
			default:
				isFeatureExpected = false;
				return false;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000262C File Offset: 0x0000082C
		public static bool IsFeatureEnabled(SkuType sku, RestrictedFeatures feature)
		{
			bool flag;
			return SkuUtil.IsFeatureEnabled(sku, feature, out flag);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002644 File Offset: 0x00000844
		public static bool IsSUMRequired(SkuType sku)
		{
			bool flag = false;
			if (sku == SkuType.SsrsDeveloper || sku == SkuType.SsrsStandard || sku == SkuType.PbirsDeveloper)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002668 File Offset: 0x00000868
		internal static void GetConcurrencyLimit(SkuType sku, out long maxPhysicalCpu, out long maxCores, out long minLogicalCpu)
		{
			minLogicalCpu = 0L;
			switch (sku)
			{
			case SkuType.SsrsEvaluation:
			case SkuType.SsrsDeveloper:
			case SkuType.SsrsEnterpriseCore:
			case SkuType.PbirsEvaluation:
			case SkuType.PbirsDeveloper:
			case SkuType.PbirsPremium:
			case SkuType.PbirsSqlServerEeSa:
				maxPhysicalCpu = 0L;
				maxCores = 0L;
				return;
			case SkuType.SsrsExpress:
				maxPhysicalCpu = 1L;
				maxCores = 4L;
				return;
			case SkuType.SsrsWeb:
				maxPhysicalCpu = 4L;
				maxCores = 16L;
				return;
			case SkuType.SsrsStandard:
				maxPhysicalCpu = 4L;
				maxCores = 24L;
				return;
			case SkuType.SsrsEnterprise:
				maxPhysicalCpu = 20L;
				maxCores = 20L;
				return;
			}
			throw new ArgumentException("sku", sku.ToString());
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002704 File Offset: 0x00000904
		internal static long GetMaxMemoryThresholdMB(SkuType sku)
		{
			long num;
			if (sku != SkuType.SsrsExpress)
			{
				if (sku - SkuType.SsrsWeb > 1)
				{
					throw new ArgumentException("sku", sku.ToString());
				}
				num = 64L;
			}
			else
			{
				num = 4L;
			}
			return num * 1024L;
		}

		// Token: 0x0200001D RID: 29
		public enum SkuVerificationErrorCode
		{
			// Token: 0x04000088 RID: 136
			Success,
			// Token: 0x04000089 RID: 137
			LocalConnectionRequired,
			// Token: 0x0400008A RID: 138
			DatabaseSkuRequired,
			// Token: 0x0400008B RID: 139
			DatabaseSkuRestricted
		}

		// Token: 0x0200001E RID: 30
		// (Invoke) Token: 0x06000079 RID: 121
		public delegate bool LocalDbServerVerifier(string dbServer);
	}
}
