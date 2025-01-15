using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200005F RID: 95
	[Serializable]
	internal abstract class CatalogItemContextBase<TPathStorageType> : ICatalogItemContext
	{
		// Token: 0x060002A2 RID: 674
		public abstract string MapUserProvidedPath(string path);

		// Token: 0x060002A3 RID: 675
		public abstract bool IsReportServerPathOrUrl(string pathOrUrl, bool checkProtocol, out bool isRelative);

		// Token: 0x060002A4 RID: 676
		public abstract bool IsSupportedProtocol(string path, bool checkProtocol);

		// Token: 0x060002A5 RID: 677
		public abstract bool IsSupportedProtocol(string path, bool checkProtocol, out bool isRelative);

		// Token: 0x060002A6 RID: 678 RVA: 0x00009FFC File Offset: 0x000081FC
		public virtual string AdjustSubreportOrDrillthroughReportPath(string reportPath)
		{
			string text;
			try
			{
				text = this.MapUserProvidedPath(reportPath);
			}
			catch (UriFormatException)
			{
				return null;
			}
			CatalogItemContextBase<TPathStorageType> catalogItemContextBase = this.CreateContext(this.m_PathTranslator);
			if (!catalogItemContextBase.SetPath(text, ItemPathOptions.Default))
			{
				return null;
			}
			if (Localization.CatalogCultureCompare(text, catalogItemContextBase.ItemPathAsString) == 0)
			{
				return reportPath;
			}
			return catalogItemContextBase.ItemPathAsString;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A05C File Offset: 0x0000825C
		public virtual ICatalogItemContext GetSubreportContext(string subreportPath)
		{
			CatalogItemContextBase<TPathStorageType> catalogItemContextBase = this.CreateContext(this.m_PathTranslator);
			this.InitSubreportContext(catalogItemContextBase, subreportPath);
			catalogItemContextBase.m_primaryContext = this.m_primaryContext;
			return catalogItemContextBase;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000A08C File Offset: 0x0000828C
		private void InitSubreportContext(CatalogItemContextBase<TPathStorageType> subreportContext, string subreportPath)
		{
			string text = this.MapUserProvidedPath(subreportPath);
			subreportContext.SetPath(text, ItemPathOptions.Validate);
			subreportContext.RSRequestParameters.SetCatalogParameters(null);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000A0B8 File Offset: 0x000082B8
		public virtual string CombineUrl(string url, bool checkProtocol, NameValueCollection unparsedParameters, out ICatalogItemContext newContext)
		{
			newContext = this;
			string text = new CatalogItemUrlBuilder(this).ToString();
			if (url == null)
			{
				return text;
			}
			if (string.Compare(url, text, StringComparison.Ordinal) == 0)
			{
				return text;
			}
			newContext = this.Combine(url, checkProtocol, true);
			if (newContext == null)
			{
				newContext = null;
				return url;
			}
			CatalogItemUrlBuilder catalogItemUrlBuilder = new CatalogItemUrlBuilder(newContext);
			if (unparsedParameters != null)
			{
				catalogItemUrlBuilder.AppendUnparsedParameters(unparsedParameters);
			}
			return catalogItemUrlBuilder.ToString();
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000A118 File Offset: 0x00008318
		public IRSRequestParameters RSRequestParameters
		{
			get
			{
				RSRequestParameters rsrequestParameters;
				if ((rsrequestParameters = this.m_parsedParameters) == null)
				{
					rsrequestParameters = (this.m_parsedParameters = this.CreateRequestParametersInstance());
				}
				return rsrequestParameters;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000A140 File Offset: 0x00008340
		public IReportParameterLookup ReportParameterLookup
		{
			get
			{
				RSRequestParameters rsrequestParameters;
				if ((rsrequestParameters = this.m_parsedParameters) == null)
				{
					rsrequestParameters = (this.m_parsedParameters = this.CreateRequestParametersInstance());
				}
				return rsrequestParameters;
			}
		}

		// Token: 0x060002AC RID: 684
		protected abstract RSRequestParameters CreateRequestParametersInstance();

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000A166 File Offset: 0x00008366
		public TPathStorageType ItemPath
		{
			get
			{
				return this.m_ItemPath;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002AE RID: 686
		public abstract string ItemPathAsString { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000A16E File Offset: 0x0000836E
		public virtual string HostSpecificItemPath
		{
			get
			{
				return this.ItemPathAsString;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000A176 File Offset: 0x00008376
		public virtual string StableItemPath
		{
			get
			{
				return this.ItemPathAsString;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000A180 File Offset: 0x00008380
		public string ItemName
		{
			get
			{
				if (this.m_ItemName == null)
				{
					string text;
					CatalogItemNameUtility.SplitPath(this.ItemPathAsString, out this.m_ItemName, out text);
					this.m_ParentPath = text;
				}
				return this.m_ItemName;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000A1B8 File Offset: 0x000083B8
		public string ParentPath
		{
			get
			{
				if (this.m_ItemName == null)
				{
					string text;
					CatalogItemNameUtility.SplitPath(this.ItemPathAsString, out this.m_ItemName, out text);
					this.m_ParentPath = text;
				}
				return this.m_ParentPath;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002B3 RID: 691
		public abstract string HostRootUri { get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000A1ED File Offset: 0x000083ED
		public virtual IPathManager PathManager
		{
			get
			{
				if (this.m_pathManager == null)
				{
					this.m_pathManager = RSPathUtil.Instance;
				}
				return this.m_pathManager;
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000A208 File Offset: 0x00008408
		public ICatalogItemContext Combine(string url)
		{
			return this.Combine(url, false);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000A212 File Offset: 0x00008412
		public CatalogItemContextBase<TPathStorageType> Combine(string url, bool externalFormat)
		{
			return this.Combine(url, true, externalFormat);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000A220 File Offset: 0x00008420
		public CatalogItemContextBase<TPathStorageType> Combine(string url, bool checkProtocol, bool externalFormat)
		{
			bool flag;
			if (!this.IsReportServerPathOrUrl(url, checkProtocol, out flag))
			{
				return null;
			}
			if (!flag)
			{
				return null;
			}
			string text = this.MapUserProvidedPath(url);
			if (externalFormat && this.m_PathTranslator != null)
			{
				string text2 = this.m_PathTranslator.PathToExternal(text);
				if (text2 != null)
				{
					text = text2;
				}
			}
			CatalogItemContextBase<TPathStorageType> catalogItemContextBase = this.CreateContext(this.m_PathTranslator);
			ItemPathOptions itemPathOptions = ItemPathOptions.Validate;
			itemPathOptions |= (externalFormat ? ItemPathOptions.Translate : ItemPathOptions.Convert);
			if (!catalogItemContextBase.SetPath(text, itemPathOptions))
			{
				throw new ItemNotFoundException(text);
			}
			catalogItemContextBase.RSRequestParameters.SetCatalogParameters(null);
			return catalogItemContextBase;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000A29A File Offset: 0x0000849A
		protected TPathStorageType StoredItemPath
		{
			get
			{
				return this.m_ItemPath;
			}
		}

		// Token: 0x060002B9 RID: 697
		public abstract bool SetPath(string path, ItemPathOptions pathOptions);

		// Token: 0x060002BA RID: 698
		protected abstract CatalogItemContextBase<TPathStorageType> CreateContext(IPathTranslator pathTranslator);

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000A2A2 File Offset: 0x000084A2
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000A2AA File Offset: 0x000084AA
		public TPathStorageType OriginalItemPath
		{
			get
			{
				return this.m_OriginalItemPath;
			}
			protected set
			{
				this.m_OriginalItemPath = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000A2B3 File Offset: 0x000084B3
		public IPathTranslator PathTranslator
		{
			get
			{
				return this.m_PathTranslator;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000A2BB File Offset: 0x000084BB
		public virtual string ReportDefinitionPath
		{
			get
			{
				return this.ItemPathAsString;
			}
		}

		// Token: 0x0400015A RID: 346
		protected TPathStorageType m_reportDefinitionPath;

		// Token: 0x0400015B RID: 347
		protected TPathStorageType m_OriginalItemPath;

		// Token: 0x0400015C RID: 348
		protected TPathStorageType m_ItemPath;

		// Token: 0x0400015D RID: 349
		protected string m_ItemName;

		// Token: 0x0400015E RID: 350
		protected string m_ParentPath;

		// Token: 0x0400015F RID: 351
		protected CatalogItemContextBase<TPathStorageType> m_primaryContext;

		// Token: 0x04000160 RID: 352
		[NonSerialized]
		private RSRequestParameters m_parsedParameters;

		// Token: 0x04000161 RID: 353
		[NonSerialized]
		protected IPathManager m_pathManager;

		// Token: 0x04000162 RID: 354
		[NonSerialized]
		protected IPathTranslator m_PathTranslator;
	}
}
