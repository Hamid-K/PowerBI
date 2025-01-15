using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000CA RID: 202
	[CatalogItemType(ItemType.Unknown)]
	internal abstract class BaseReportCatalogItem : BaseExecutableCatalogItem
	{
		// Token: 0x0600089E RID: 2206 RVA: 0x000229F9 File Offset: 0x00020BF9
		internal BaseReportCatalogItem(RSService service)
			: base(service)
		{
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00022A02 File Offset: 0x00020C02
		internal virtual void Initialize(CatalogItemContext itemContext, Guid itemID, byte[] securityDescriptor, int snapshotLimit, int execOptions, Guid linkID)
		{
			base.Initialize(itemContext, itemID, securityDescriptor);
			this.m_executionOptions = execOptions;
			this.m_historyLimit = snapshotLimit;
			this.m_linkID = linkID;
			base.SetInitFlag();
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00022A2C File Offset: 0x00020C2C
		internal void LoadParameters()
		{
			ItemType itemType;
			if (!base.Service.Storage.GetParameters(base.ItemContext.CatalogItemPath, out itemType, out this.m_parametersXml, out this.m_itemID, out this.m_securityDescriptor, out this.m_linkID, out this.m_executionOptions))
			{
				throw new ItemNotFoundException(base.ItemContext.OriginalItemPath.Value);
			}
			base.ThisItemType = itemType;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00022A93 File Offset: 0x00020C93
		internal void CombineParameters(ParameterInfoCollection newParameters)
		{
			RSTrace.CatalogTrace.Assert(base.ThisItemType != ItemType.DataSet, "ThisItemType");
			base.Parameters = ParameterInfoCollection.Combine(base.Parameters, newParameters, false, false, true, false, Localization.ClientPrimaryCulture);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00022ACC File Offset: 0x00020CCC
		private void LoadRuntimeDataSources()
		{
			ReportSnapshot reportSnapshot;
			RuntimeDataSourceInfoCollection runtimeDataSourceInfoCollection;
			RuntimeDataSetInfoCollection runtimeDataSetInfoCollection;
			base.Service.GetAllDataSources(this, true, true, out reportSnapshot, out runtimeDataSourceInfoCollection, out runtimeDataSetInfoCollection);
			if (this.m_itemContext.RSRequestParameters.DatasourcesCred != null)
			{
				runtimeDataSourceInfoCollection.SetCredentials(this.m_itemContext.RSRequestParameters.DatasourcesCred, DataProtection.Instance);
			}
			this.m_compiledDefinition = reportSnapshot;
			this.m_allDataSources = runtimeDataSourceInfoCollection;
			this.m_allSharedDataSets = runtimeDataSetInfoCollection;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00022B2F File Offset: 0x00020D2F
		internal void ThrowIfNotUsableByProperties()
		{
			this.ThrowIfBrokenReportLink();
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00022B37 File Offset: 0x00020D37
		internal void ThrowIfNotCacheableByProperties()
		{
			this.ThrowIfNotUsableByProperties();
			base.ThrowIfQueryDependsOnUser();
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00022B45 File Offset: 0x00020D45
		protected override void ThrowIfDataSourcesNotGoodForUnattended()
		{
			this.ThrowIfDataSourcesNotGoodForUnattended(this.RuntimeDataSources);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00022B53 File Offset: 0x00020D53
		private void ThrowIfDataSourcesNotGoodForUnattended(RuntimeDataSourceInfoCollection allDataSources)
		{
			if (allDataSources != null && !DataSourceCatalogItem.GoodForUnattendedExecution(allDataSources))
			{
				throw new InvalidDataSourceCredentialSettingException();
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00022B68 File Offset: 0x00020D68
		protected void DeriveProperties()
		{
			bool flag = base.Parameters.GetParameterWithNoValue() == null;
			base.ItemMetadata.HasParameters = base.Parameters.Count > 0;
			RuntimeDataSourceInfoCollection allDataSources = base.Service.GetAllDataSources(this);
			base.ItemMetadata.HasDataSources = this.DataSources.Count > 0;
			base.ItemMetadata.HasSharedDataSets = this.SharedDataSets.Count > 0;
			bool flag2 = DataSourceCatalogItem.GoodForUnattendedExecution(allDataSources);
			base.Properties.FixupIfConnectionStringUserDependency(allDataSources);
			bool queryDependsOnUser = base.Properties.QueryDependsOnUser;
			bool flag3 = false;
			if (flag2 && flag && !queryDependsOnUser)
			{
				flag3 = true;
			}
			base.Properties.CanRunUnattended = flag3.ToString();
			base.Properties.HasScheduleReadyDataSources = flag2.ToString();
			base.Properties.HasParameterDefaultValues = flag.ToString();
			base.Properties.IsSnapshotExecution = Microsoft.ReportingServices.Library.ExecutionOptions.IsSnapshotExecution(this.m_executionOptions).ToString();
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00022C5F File Offset: 0x00020E5F
		internal int ExecutionOptions
		{
			get
			{
				RSTrace.CatalogTrace.Assert(this.m_executionOptionsInit);
				return this.m_executionOptions;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00022C77 File Offset: 0x00020E77
		internal int HistorySnapshotLimit
		{
			get
			{
				RSTrace.CatalogTrace.Assert(this.m_historyLimitInit);
				return this.m_historyLimit;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00022C8F File Offset: 0x00020E8F
		internal ServerSnapshot CompiledDefinition
		{
			get
			{
				return this.m_compiledDefinition;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00022C97 File Offset: 0x00020E97
		internal ReportSnapshot ReportCompiledDefinition
		{
			get
			{
				return (ReportSnapshot)this.m_compiledDefinition;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00022CA4 File Offset: 0x00020EA4
		internal RuntimeDataSourceInfoCollection RuntimeDataSources
		{
			get
			{
				if (this.m_allDataSources == null)
				{
					this.LoadRuntimeDataSources();
				}
				return this.m_allDataSources;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00022CBA File Offset: 0x00020EBA
		internal RuntimeDataSetInfoCollection RuntimeSharedDataSets
		{
			get
			{
				if (this.m_allSharedDataSets == null)
				{
					this.LoadRuntimeDataSources();
				}
				return this.m_allSharedDataSets;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060008AE RID: 2222
		// (set) Token: 0x060008AF RID: 2223
		internal abstract DataSetInfoCollection SharedDataSets { get; set; }

		// Token: 0x060008B0 RID: 2224 RVA: 0x00022CD0 File Offset: 0x00020ED0
		internal void ThrowIfNoRights(ReportOperation orOperation1, ReportOperation orOperation2)
		{
			if (!base.Service.SecMgr.CheckAccess(base.ThisItemType, base.SecurityDescriptor, orOperation1, base.ItemContext.ItemPath) && !base.Service.SecMgr.CheckAccess(base.ThisItemType, base.SecurityDescriptor, orOperation2, base.ItemContext.ItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00022D44 File Offset: 0x00020F44
		internal void SaveParameters()
		{
			base.Service.Storage.SetParameters(base.ItemContext.CatalogItemPath, base.ParametersXml);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00022D67 File Offset: 0x00020F67
		internal ItemParameterDefinition LoadParametersForExecution(string historyId, bool forRendering)
		{
			return ItemParameterDefinition.Load(base.ItemContext, historyId, forRendering, base.Service, SecurityRequirements.GenerateForExecuteReport(base.Service.SecMgr, base.Service.UserName));
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00022D97 File Offset: 0x00020F97
		internal void ThrowIfNotSubscribableByProperties(bool forDataDriven)
		{
			this.ThrowIfBrokenReportLink();
			if (forDataDriven)
			{
				base.ThrowIfQueryDependsOnUser();
				this.ThrowIfLayoutDependsOnUser();
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00022DAE File Offset: 0x00020FAE
		private void ThrowIfBrokenReportLink()
		{
			if (base.ThisItemType == ItemType.LinkedReport && this.m_linkID == Guid.Empty)
			{
				throw new InvalidReportLinkException();
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00022DD4 File Offset: 0x00020FD4
		protected override void ThrowIfParameterValueNotSet()
		{
			string parameterWithNoValue = base.GetCombinedRequestParameters().GetParameterWithNoValue();
			if (parameterWithNoValue != null)
			{
				throw new ReportParameterValueNotSetException(parameterWithNoValue);
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00022DF7 File Offset: 0x00020FF7
		private void ThrowIfLayoutDependsOnUser()
		{
			if ((this.GetReportUserProfileProperties() & UserProfileState.InReport) != UserProfileState.None)
			{
				throw new HasUserProfileDependenciesException(this.m_itemContext.OriginalItemPath.Value);
			}
		}

		// Token: 0x04000432 RID: 1074
		private RuntimeDataSourceInfoCollection m_allDataSources;

		// Token: 0x04000433 RID: 1075
		private RuntimeDataSetInfoCollection m_allSharedDataSets;

		// Token: 0x04000434 RID: 1076
		protected DataSetInfoCollection m_sharedDataSets;
	}
}
