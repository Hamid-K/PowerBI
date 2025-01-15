using System;
using System.Collections.Specialized;
using System.Diagnostics;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000C9 RID: 201
	[CatalogItemType(ItemType.Unknown)]
	internal abstract class BaseExecutableCatalogItem : CatalogItem
	{
		// Token: 0x0600088F RID: 2191 RVA: 0x00004F8E File Offset: 0x0000318E
		internal BaseExecutableCatalogItem(RSService service)
			: base(service)
		{
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00022808 File Offset: 0x00020A08
		internal virtual void ThrowIfNotGoodForUnattended(bool checkNonDatasource)
		{
			if (checkNonDatasource)
			{
				this.ThrowIfQueryDependsOnUser();
				this.ThrowIfParameterValueNotSet();
			}
			this.ThrowIfDataSourcesNotGoodForUnattended();
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0002281F File Offset: 0x00020A1F
		protected virtual void ThrowIfDataSourcesNotGoodForUnattended()
		{
			if (this.DataSources != null && !DataSourceCatalogItem.GoodForUnattendedExecution(this.DataSources.GetTheOnlyDataSource()))
			{
				throw new InvalidDataSourceCredentialSettingException();
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00022844 File Offset: 0x00020A44
		protected void ThrowIfQueryDependsOnUser()
		{
			if ((this.GetReportUserProfileProperties() & UserProfileState.InQuery) != UserProfileState.None)
			{
				throw new HasUserProfileDependenciesException(this.m_itemContext.OriginalItemPath.Value);
			}
			if (this.DataSources != null && this.DataSources.HasConnectionStringUseridReference())
			{
				throw new HasUserProfileDependenciesException(this.m_itemContext.OriginalItemPath.Value);
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected virtual void ThrowIfParameterValueNotSet()
		{
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0002289C File Offset: 0x00020A9C
		protected virtual UserProfileState GetReportUserProfileProperties()
		{
			return base.Properties.DependsOnUser;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x000228AC File Offset: 0x00020AAC
		private string GetParametersXml()
		{
			if (base.ItemContext != null)
			{
				ItemType itemType;
				string text;
				Guid guid;
				byte[] array;
				base.Service.Storage.GetParameters(base.ItemContext.CatalogItemPath, out itemType, out text, out guid, out array);
				return text;
			}
			if (base.ItemID != Guid.Empty)
			{
				return base.Service.Storage.GetParametersById(base.ItemID);
			}
			throw new InternalCatalogException("BaseReportCatalogItem not initialized on GetParametersXml");
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x0002291A File Offset: 0x00020B1A
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x00022936 File Offset: 0x00020B36
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal string ParametersXml
		{
			get
			{
				if (this.m_parametersXml == null)
				{
					this.m_parametersXml = this.GetParametersXml();
				}
				return this.m_parametersXml;
			}
			set
			{
				this.m_parametersXml = value;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000898 RID: 2200
		// (set) Token: 0x06000899 RID: 2201
		internal abstract DataSourceInfoCollection DataSources { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0002293F File Offset: 0x00020B3F
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x00022960 File Offset: 0x00020B60
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal ParameterInfoCollection Parameters
		{
			get
			{
				if (this.m_parameters == null)
				{
					this.m_parameters = ParameterInfoCollection.DecodeFromXml(this.ParametersXml);
				}
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
				this.m_parametersXml = ((value == null) ? "" : value.ToXml(false));
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00022980 File Offset: 0x00020B80
		internal ParameterInfoCollection GetCombinedRequestParameters()
		{
			this.EvaluateCombinedParameters();
			if (this.m_combinedParameters == null)
			{
				return this.Parameters;
			}
			return this.m_combinedParameters;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x000229A0 File Offset: 0x00020BA0
		protected void EvaluateCombinedParameters()
		{
			if (this.m_combinedParameters != null)
			{
				return;
			}
			ParameterInfoCollection parameters = this.Parameters;
			NameValueCollection reportParameters = base.ItemContext.RSRequestParameters.ReportParameters;
			if (reportParameters != null)
			{
				bool flag = base.ThisItemType == ItemType.DataSet;
				ParameterInfoCollection parameterInfoCollection = ParameterInfoCollection.DecodeFromNameValueCollectionAndUserCulture(reportParameters, flag);
				this.m_combinedParameters = ParameterInfoCollection.Combine(parameters, parameterInfoCollection, true, false, false, flag, Localization.ReportParameterCulture);
			}
		}

		// Token: 0x04000430 RID: 1072
		private ParameterInfoCollection m_parameters;

		// Token: 0x04000431 RID: 1073
		private ParameterInfoCollection m_combinedParameters;
	}
}
