using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Text;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Editions;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000066 RID: 102
	internal static class Sku
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
		public static StringCollection RevokePermissionsSku(StringCollection rawPemissions, SecurityItemType itemType, string instanceID)
		{
			SkuType installedSku = Sku.GetInstalledSku(instanceID);
			return SkuAndPermissionCollection.GetSkuAdjustedPermissions(rawPemissions, itemType, installedSku);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		public static bool CheckAccessOverideSku(string instanceId, ModelOperation modelOper)
		{
			return Sku.IsFeatureEnabled(instanceId, RestrictedFeatures.ReportBuilder);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000D50C File Offset: 0x0000B70C
		public static bool IsFeatureEnabled(SkuType sku, RestrictedFeatures feature)
		{
			bool flag2;
			bool flag = SkuUtil.IsFeatureEnabled(sku, feature, out flag2);
			if (!flag2)
			{
				throw new InternalCatalogException("unexpected feature " + feature.ToString());
			}
			return flag;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000D542 File Offset: 0x0000B742
		public static bool IsFeatureEnabled(string instanceId, RestrictedFeatures feature)
		{
			return Sku.IsFeatureEnabled(Sku.GetInstalledSku(instanceId), feature);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000D550 File Offset: 0x0000B750
		public static bool IsPbirsRunning(string instanceId)
		{
			return SkuUtil.IsPBIRSSku(Sku.GetInstalledSku(instanceId));
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000D55D File Offset: 0x0000B75D
		public static bool IsSsrsRunning(string instanceId)
		{
			return SkuUtil.IsSSRSSku(Sku.GetInstalledSku(instanceId));
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000D56A File Offset: 0x0000B76A
		public static void ThrowIfFeatureNotEnabled(string instanceId, RestrictedFeatures feature)
		{
			if (!Sku.IsFeatureEnabled(instanceId, feature))
			{
				SkuResources.ThrowOperationNotSupportedException(feature);
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000D57B File Offset: 0x0000B77B
		public static void ThrowIfFeatureNotEnabled(SkuType sku, RestrictedFeatures feature)
		{
			if (!Sku.IsFeatureEnabled(sku, feature))
			{
				SkuResources.ThrowOperationNotSupportedException(feature);
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000D58C File Offset: 0x0000B78C
		public static SqlServerSkuType EnsureCorrectEdition(IDbConnection sqlConn, string connectionString, bool checkRestrictedSkus)
		{
			SqlServerSkuType sqlSku;
			try
			{
				sqlSku = SkuUtil.GetSqlSku(sqlConn);
			}
			catch (Exception ex)
			{
				throw new ServerConfigurationErrorException(ex, "SELECT SERVERPROPERTY failed to execute.");
			}
			if (sqlSku == SqlServerSkuType.None)
			{
				throw new ServerConfigurationErrorException("Cannot get edition information from catalog server.");
			}
			if (RSTrace.CatalogTrace.TraceInfo)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Catalog SQL Server Edition = {0}", new object[] { sqlSku.ToString() });
			}
			SkuType installedSku = Sku.GetInstalledSku(ProcessingContext.Configuration.InstanceID);
			SkuUtil.SkuVerificationErrorCode skuVerificationErrorCode = SkuUtil.EnsureCorrectEdition(installedSku, sqlSku, connectionString, new SkuUtil.LocalDbServerVerifier(Sku.IsLocalValue), checkRestrictedSkus);
			if (skuVerificationErrorCode == SkuUtil.SkuVerificationErrorCode.LocalConnectionRequired)
			{
				SkuResources.ThrowOperationNotSupportedException(RestrictedFeatures.RemoteDataSources);
			}
			else if (skuVerificationErrorCode != SkuUtil.SkuVerificationErrorCode.Success)
			{
				IEnumerable<SqlServerSkuType> enumerable;
				IEnumerable<SqlServerSkuType> supportedDatabaseSkus = SkuUtil.GetSupportedDatabaseSkus(installedSku, out enumerable);
				if (skuVerificationErrorCode == SkuUtil.SkuVerificationErrorCode.DatabaseSkuRequired)
				{
					if (RSTrace.CatalogTrace.TraceError)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Error, "required SKUs: {0} - actual SKU: {1}", new object[]
						{
							Sku.GetSkuListForTrace(supportedDatabaseSkus),
							sqlSku.ToString()
						});
					}
					SkuResources.ThrowOperationNotSupportedException(RestrictedFeatures.OtherSkuDatasources);
				}
				else if (skuVerificationErrorCode == SkuUtil.SkuVerificationErrorCode.DatabaseSkuRestricted)
				{
					if (RSTrace.CatalogTrace.TraceError)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Error, "restricted SKUs: {0} - actual SKU: {1}", new object[]
						{
							Sku.GetSkuListForTrace(enumerable),
							sqlSku.ToString()
						});
					}
					SkuResources.ThrowOperationNotSupportedException(RestrictedFeatures.OtherSkuDatasources);
				}
			}
			return sqlSku;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000D6D0 File Offset: 0x0000B8D0
		private static bool IsLocalValue(string value)
		{
			if (WebUtil.IsWellKnownLocalServer(value))
			{
				return true;
			}
			bool flag;
			try
			{
				bool result = false;
				RevertImpersonationContext.Run(delegate
				{
					result = LocalClientUtil.IsLocalIPAddressOrHostName(value);
				});
				flag = result;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000D73C File Offset: 0x0000B93C
		internal static bool IsBiServer()
		{
			return SkuUtil.IsPBIRSSku(Sku.GetInstalledSku(ProcessingContext.Configuration.InstanceID));
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000D754 File Offset: 0x0000B954
		private static string GetSkuListForTrace(IEnumerable<SqlServerSkuType> skus)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (SqlServerSkuType sqlServerSkuType in skus)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(sqlServerSkuType.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000D7CC File Offset: 0x0000B9CC
		private static SkuInfo GetSkuInfoForInstance(string instanceId)
		{
			if (string.IsNullOrEmpty(instanceId) || (Globals.CurrentApplication != RunningApplication.WindowsService && Globals.CurrentApplication != RunningApplication.WebService && Globals.CurrentApplication != RunningApplication.ReportServerWebApp))
			{
				return new SkuInfo(SkuType.SsrsDeveloper, false);
			}
			bool isExpired = false;
			SkuType skuType = SkuType.None;
			RevertImpersonationContext.Run(delegate
			{
				RSTrace resourceUtilTrace = RSTrace.ResourceUtilTrace;
				SkuInfo skuInfo = new SkuStore().Load(instanceId);
				skuType = skuInfo.SkuType;
				if (skuType == SkuType.None)
				{
					throw new ServerConfigurationErrorException("unexpected SKU value");
				}
				if (resourceUtilTrace.TraceInfo)
				{
					resourceUtilTrace.Trace(TraceLevel.Info, "SKU: {0}", new object[] { skuInfo.SkuType.GetStrings().FullName });
				}
				if (skuInfo.Timebomb.HasExpired())
				{
					if (!Sku.IgnoreExpirationCheck)
					{
						throw new EvaluationCopyExpiredException();
					}
					isExpired = true;
				}
				if (skuType == SkuType.SsrsEvaluation && resourceUtilTrace.TraceInfo)
				{
					resourceUtilTrace.Trace(TraceLevel.Info, "Evaluation copy: {0}.", new object[] { skuInfo.Timebomb });
				}
			});
			return new SkuInfo(skuType, isExpired);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000D843 File Offset: 0x0000BA43
		private static void InitializeSkuInfo(string instanceId)
		{
			if (Sku.skuInfo == null)
			{
				Sku.skuInfo = Sku.GetSkuInfoForInstance(instanceId);
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000D857 File Offset: 0x0000BA57
		public static SkuInfo GetSkuInfo(string instanceId)
		{
			Sku.InitializeSkuInfo(instanceId);
			return Sku.skuInfo;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000D864 File Offset: 0x0000BA64
		public static SkuType GetInstalledSku(string instanceId)
		{
			Sku.InitializeSkuInfo(instanceId);
			return Sku.skuInfo.Sku;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000D878 File Offset: 0x0000BA78
		internal static string GetInstalledSkuName(string instanceId)
		{
			SkuType installedSku = Sku.GetInstalledSku(instanceId);
			if (installedSku == SkuType.SsrsEnterpriseCore)
			{
				return "Enterprise: Core-Based Licensing";
			}
			return installedSku.GetStrings().ShortName;
		}

		// Token: 0x04000332 RID: 818
		private static SkuInfo skuInfo;

		// Token: 0x04000333 RID: 819
		public static bool IgnoreExpirationCheck;
	}
}
