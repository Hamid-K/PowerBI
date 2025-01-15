using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200009B RID: 155
	internal class RSReportContext
	{
		// Token: 0x06000631 RID: 1585 RVA: 0x00019FAF File Offset: 0x000181AF
		internal RSReportContext(RSService catalogService, CatalogItemContext itemContext)
		{
			this.m_catalogService = catalogService;
			this.m_itemContext = itemContext;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00019FDB File Offset: 0x000181DB
		internal CatalogItemContext ItemContext
		{
			get
			{
				return this.m_itemContext;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00019FE3 File Offset: 0x000181E3
		internal ItemProperties Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00019FEB File Offset: 0x000181EB
		internal ParameterInfoCollection Parameters
		{
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00019FF3 File Offset: 0x000181F3
		internal RuntimeDataSourceInfoCollection AllDataSources
		{
			get
			{
				return this.m_allDataSources;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00019FFB File Offset: 0x000181FB
		internal ItemType Type
		{
			get
			{
				return this.m_itemType;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001A003 File Offset: 0x00018203
		internal Guid ItemID
		{
			get
			{
				return this.m_itemID;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0001A00B File Offset: 0x0001820B
		internal Guid LinkID
		{
			get
			{
				return this.m_linkID;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001A013 File Offset: 0x00018213
		internal byte[] SecurityDescriptor
		{
			get
			{
				return this.m_secDesc;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001A01B File Offset: 0x0001821B
		internal int ExecutionOptions
		{
			get
			{
				return this.m_executionOptions;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0001A023 File Offset: 0x00018223
		internal int HistoryLimit
		{
			get
			{
				return this.m_historyLimit;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001A02B File Offset: 0x0001822B
		internal ReportSnapshot CompiledDefinition
		{
			get
			{
				return this.m_compiledDefinition;
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001A034 File Offset: 0x00018234
		internal void RetrieveProperties()
		{
			ItemProperties itemProperties;
			string text;
			if (!this.m_catalogService.Storage.GetAllProperties(this.m_itemContext.ItemPath, out itemProperties, out this.m_itemID, out this.m_linkID, out this.m_itemType, out this.m_secDesc, out this.m_executionOptions, out this.m_historyLimit, out text))
			{
				throw new ItemNotFoundException(this.m_itemContext.OriginalItemPath.Value);
			}
			this.m_properties = itemProperties;
			if (this.m_itemType != ItemType.Report && this.m_itemType != ItemType.LinkedReport && this.m_itemType != ItemType.DataSet)
			{
				throw new WrongItemTypeException(this.m_itemContext.OriginalItemPath.Value);
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001A0D4 File Offset: 0x000182D4
		internal void RetrieveParameters()
		{
			string text;
			if (!this.m_catalogService.Storage.GetParameters(this.m_itemContext.CatalogItemPath, out this.m_itemType, out text, out this.m_itemID, out this.m_secDesc))
			{
				throw new ItemNotFoundException(this.m_itemContext.OriginalItemPath.Value);
			}
			ParameterInfoCollection parameterInfoCollection = ParameterInfoCollection.DecodeFromXml(text);
			if (this.ItemContext.RSRequestParameters.ReportParameters != null)
			{
				bool flag = this.m_itemType == ItemType.DataSet;
				ParameterInfoCollection parameterInfoCollection2 = ParameterInfoCollection.DecodeFromNameValueCollectionAndUserCulture(this.ItemContext.RSRequestParameters.ReportParameters, flag);
				this.m_parameters = ParameterInfoCollection.Combine(parameterInfoCollection, parameterInfoCollection2, true, false, false, flag, Localization.ClientPrimaryCulture);
				return;
			}
			this.m_parameters = parameterInfoCollection;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001A180 File Offset: 0x00018380
		internal void RetrieveAllDataSources(bool checkIfUsable, bool userServiceConnectionForRepublishing)
		{
			try
			{
				CatalogItem catalogItem = this.m_catalogService.CatalogItemFactory.GetCatalogItem(this.m_itemContext);
				catalogItem.ThrowIfWrongItemType(new ItemType[]
				{
					ItemType.Report,
					ItemType.LinkedReport,
					ItemType.DataSet
				});
				BaseReportCatalogItem baseReportCatalogItem = catalogItem as BaseReportCatalogItem;
				RuntimeDataSetInfoCollection runtimeDataSetInfoCollection;
				this.m_catalogService.GetAllDataSources(baseReportCatalogItem, checkIfUsable, userServiceConnectionForRepublishing, out this.m_compiledDefinition, out this.m_allDataSources, out runtimeDataSetInfoCollection);
			}
			finally
			{
			}
			if (this.m_itemContext.RSRequestParameters.DatasourcesCred != null)
			{
				this.m_allDataSources.SetCredentials(this.m_itemContext.RSRequestParameters.DatasourcesCred, DataProtection.Instance);
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001A224 File Offset: 0x00018424
		internal void ThrowIfNoRights(ReportOperation operation)
		{
			if (!this.m_catalogService.SecMgr.CheckAccess(this.m_itemType, this.m_secDesc, operation, this.ItemContext.ItemPath))
			{
				throw new AccessDeniedException(this.m_catalogService.UserName, ErrorCode.rsAccessDenied);
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001A264 File Offset: 0x00018464
		internal void ThrowIfNoRights(ReportOperation orOperation1, ReportOperation orOperation2)
		{
			if (!this.m_catalogService.SecMgr.CheckAccess(this.m_itemType, this.m_secDesc, orOperation1, this.ItemContext.ItemPath) && !this.m_catalogService.SecMgr.CheckAccess(this.m_itemType, this.m_secDesc, orOperation2, this.ItemContext.ItemPath))
			{
				throw new AccessDeniedException(this.m_catalogService.UserName, ErrorCode.rsAccessDenied);
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001A2D8 File Offset: 0x000184D8
		internal void ThrowIfNotUsableByProperties()
		{
			this.ThrowIfBrokenReportLink();
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001A2E0 File Offset: 0x000184E0
		internal void ThrowIfNotCacheableByProperties()
		{
			this.ThrowIfNotUsableByProperties();
			this.ThrowIfQueryDependsOnUser();
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001A2EE File Offset: 0x000184EE
		internal void ThrowIfNotSubscribableByProperties(bool forDataDriven)
		{
			this.ThrowIfBrokenReportLink();
			if (forDataDriven)
			{
				this.ThrowIfQueryDependsOnUser();
				this.ThrowIfLayoutDependsOnUser();
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001A305 File Offset: 0x00018505
		internal void ThrowIfNotGoodForUnattended(bool checkNonDatasource)
		{
			if (checkNonDatasource)
			{
				this.ThrowIfQueryDependsOnUser();
				this.ThrowIfParameterValueNotSet();
			}
			this.ThrowIfDataSourcesNotGoodForUnattended();
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001A31C File Offset: 0x0001851C
		private void ThrowIfBrokenReportLink()
		{
			if (this.m_itemType == ItemType.LinkedReport && this.m_linkID == Guid.Empty)
			{
				throw new InvalidReportLinkException();
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001A33F File Offset: 0x0001853F
		private void ThrowIfDataSourcesNotGoodForUnattended()
		{
			if (!DataSourceCatalogItem.GoodForUnattendedExecution(this.AllDataSources))
			{
				throw new InvalidDataSourceCredentialSettingException();
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001A354 File Offset: 0x00018554
		private void ThrowIfParameterValueNotSet()
		{
			string parameterWithNoValue = this.Parameters.GetParameterWithNoValue();
			if (parameterWithNoValue != null)
			{
				throw new ReportParameterValueNotSetException(parameterWithNoValue);
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001A378 File Offset: 0x00018578
		private UserProfileState GetReportUserProfileProperties()
		{
			if (this.m_itemType == ItemType.LinkedReport)
			{
				if (this.m_linkedPropertyResolver == null)
				{
					LinkedReportProperyResolver linkedReportProperyResolver = new LinkedReportProperyResolver(this.m_itemContext.ReportDefinitionAsExternalItemPath, this.m_catalogService);
					linkedReportProperyResolver.Resolve();
					this.m_linkedPropertyResolver = linkedReportProperyResolver;
				}
				RSTrace.CatalogTrace.Assert(this.m_linkedPropertyResolver != null);
				return this.m_linkedPropertyResolver.DependsOnUser;
			}
			return this.Properties.DependsOnUser;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001A3E4 File Offset: 0x000185E4
		private void ThrowIfQueryDependsOnUser()
		{
			if ((this.GetReportUserProfileProperties() & UserProfileState.InQuery) != UserProfileState.None)
			{
				throw new HasUserProfileDependenciesException(this.m_itemContext.OriginalItemPath.Value);
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001A406 File Offset: 0x00018606
		private void ThrowIfLayoutDependsOnUser()
		{
			if ((this.GetReportUserProfileProperties() & UserProfileState.InReport) != UserProfileState.None)
			{
				throw new HasUserProfileDependenciesException(this.m_itemContext.OriginalItemPath.Value);
			}
		}

		// Token: 0x0400034E RID: 846
		private CatalogItemContext m_itemContext;

		// Token: 0x0400034F RID: 847
		private RSService m_catalogService;

		// Token: 0x04000350 RID: 848
		private ItemProperties m_properties;

		// Token: 0x04000351 RID: 849
		private ParameterInfoCollection m_parameters;

		// Token: 0x04000352 RID: 850
		private RuntimeDataSourceInfoCollection m_allDataSources;

		// Token: 0x04000353 RID: 851
		private ItemType m_itemType;

		// Token: 0x04000354 RID: 852
		private Guid m_itemID = Guid.Empty;

		// Token: 0x04000355 RID: 853
		private Guid m_linkID = Guid.Empty;

		// Token: 0x04000356 RID: 854
		private byte[] m_secDesc;

		// Token: 0x04000357 RID: 855
		private int m_executionOptions;

		// Token: 0x04000358 RID: 856
		private int m_historyLimit;

		// Token: 0x04000359 RID: 857
		private ReportSnapshot m_compiledDefinition;

		// Token: 0x0400035A RID: 858
		private LinkedReportProperyResolver m_linkedPropertyResolver;
	}
}
