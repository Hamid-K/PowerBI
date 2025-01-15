using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000060 RID: 96
	internal class CatalogItemContext : CatalogItemContextBase<ExternalItemPath>
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000A2CB File Offset: 0x000084CB
		public CatalogItemContext(IPathTranslator pathTranslator)
		{
			this.m_PathTranslator = pathTranslator;
			this.Init();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000A2E0 File Offset: 0x000084E0
		public CatalogItemContext(IPathTranslator pathTranslator, CatalogItemPath catalogPath, string parameterName)
			: this(pathTranslator, pathTranslator.CatalogToExternal(catalogPath), parameterName)
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000A2F1 File Offset: 0x000084F1
		public CatalogItemContext(IPathTranslator pathTranslator, ExternalItemPath userSuppliedPath, string parameterName)
			: this(pathTranslator, ItemPathBase.SafeValue(userSuppliedPath), parameterName)
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000A304 File Offset: 0x00008504
		public CatalogItemContext(IPathTranslator pathTranslator, string userSuppliedPath, string parameterName)
		{
			if (userSuppliedPath == null)
			{
				throw new MissingParameterException(parameterName);
			}
			RSTrace.CatalogTrace.Assert(pathTranslator != null, "pathTranslator");
			this.m_PathTranslator = pathTranslator;
			this.Init();
			if (!this.SetPath(userSuppliedPath))
			{
				throw new InvalidItemPathException(userSuppliedPath);
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000A351 File Offset: 0x00008551
		private void Init()
		{
			this.m_primaryContext = this;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000A35A File Offset: 0x0000855A
		public bool SetPath(ExternalItemPath externalPath)
		{
			return this.SetPath(externalPath.Value);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000A368 File Offset: 0x00008568
		public bool SetPath(ExternalItemPath externalPath, ItemPathOptions pathOptions)
		{
			return this.SetPath(externalPath.Value, externalPath.EditSessionID, pathOptions);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000A37D File Offset: 0x0000857D
		public bool SetPath(string path)
		{
			return this.SetPath(path, ItemPathOptions.Default);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000A387 File Offset: 0x00008587
		public override bool SetPath(string path, ItemPathOptions options)
		{
			return this.SetPath(path, null, options);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000A394 File Offset: 0x00008594
		private bool SetPath(string path, string editSessionID, ItemPathOptions options)
		{
			string text = path;
			string text2 = null;
			if ((options & ItemPathOptions.AllowEditSessionSyntax) == ItemPathOptions.AllowEditSessionSyntax)
			{
				string text3 = null;
				string text4 = null;
				string text5;
				if (ItemPathBase.ParseInternalItemPathParts(text, out text2, out text5))
				{
					text = text5;
					editSessionID = text2;
					CatalogItemNameUtility.SplitPath(text, out text3, out text4);
					if (Localization.CatalogCultureCompare(text4, CatalogItemNameUtility.PathSeparatorString) == 0 || string.IsNullOrEmpty(text4))
					{
						if ((options & ItemPathOptions.Validate) == ItemPathOptions.Validate)
						{
							CatalogItemNameUtility.ValidateAndTrimItemName(text3, "name");
						}
						options &= ~ItemPathOptions.Validate;
					}
				}
			}
			bool flag = (options & ItemPathOptions.Convert) == ItemPathOptions.Convert;
			if ((options & ItemPathOptions.Validate) == ItemPathOptions.Validate)
			{
				bool flag2 = !flag;
				if (!CatalogItemNameUtility.ValidateItemPath(text, flag2))
				{
					return false;
				}
			}
			if (flag)
			{
				text = CatalogItemNameUtility.ConvertPathToInternal(text);
			}
			if ((options & ItemPathOptions.Translate) == ItemPathOptions.Translate && this.m_PathTranslator != null)
			{
				text = this.m_PathTranslator.PathToInternal(text);
			}
			if ((options & ItemPathOptions.IgnoreValidateEditSession) == ItemPathOptions.IgnoreValidateEditSession)
			{
				this.m_ItemPath = ExternalItemPath.CreateWithoutChecks(text, editSessionID);
				this.m_OriginalItemPath = ExternalOriginalItemPath.CreateWithoutChecks(path, editSessionID);
			}
			else
			{
				this.m_ItemPath = new ExternalItemPath(text, editSessionID);
				this.m_OriginalItemPath = new ExternalOriginalItemPath(path, editSessionID);
			}
			return true;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000A482 File Offset: 0x00008682
		public bool IsRoot
		{
			get
			{
				RSTrace.CatalogTrace.Assert(ItemPathBase.SafeValue(base.ItemPath) != "/");
				return ItemPathBase.IsNullOrEmpty(base.ItemPath);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000A4B0 File Offset: 0x000086B0
		public CatalogItemPath CatalogItemPath
		{
			get
			{
				if (this.m_CatalogItemPath == null)
				{
					if (ItemPathBase.IsNullOrEmpty(base.ItemPath))
					{
						this.m_CatalogItemPath = CatalogItemPath.Empty;
					}
					else
					{
						this.m_CatalogItemPath = base.ItemPath.ConvertToCatalogPath(this.m_PathTranslator);
					}
				}
				return this.m_CatalogItemPath;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000A4FC File Offset: 0x000086FC
		public override string StableItemPath
		{
			get
			{
				return this.CatalogItemPath.Value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000A509 File Offset: 0x00008709
		public override string HostSpecificItemPath
		{
			get
			{
				return base.ItemPath.FullEditSessionIdentifier;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000A516 File Offset: 0x00008716
		public override string ItemPathAsString
		{
			get
			{
				return base.ItemPath.Value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000A523 File Offset: 0x00008723
		public override string ReportDefinitionPath
		{
			get
			{
				return this.ReportDefinitionAsExternalItemPath.Value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000A530 File Offset: 0x00008730
		public ExternalItemPath ReportDefinitionAsExternalItemPath
		{
			get
			{
				if (this.m_reportDefinitionPath == null)
				{
					return base.ItemPath;
				}
				return this.m_reportDefinitionPath;
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000A547 File Offset: 0x00008747
		public void SetReportDefinitionPath(ExternalItemPath definitionPath)
		{
			if (definitionPath == null)
			{
				throw new ArgumentNullException("definitionPath");
			}
			this.m_reportDefinitionPath = definitionPath;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000A55E File Offset: 0x0000875E
		public override string AdjustSubreportOrDrillthroughReportPath(string reportPath)
		{
			reportPath = this.PathManager.EnsureReportNamePath(reportPath);
			return base.AdjustSubreportOrDrillthroughReportPath(reportPath);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000A578 File Offset: 0x00008778
		public override string MapUserProvidedPath(string path)
		{
			IPathManager pathManager = this.PathManager;
			string text = this.ReportDefinitionPath;
			if (string.IsNullOrEmpty(text))
			{
				text = this.m_PathTranslator.CatalogToExternal(new CatalogItemPath("/")).Value;
			}
			return pathManager.RelativePathToAbsolutePath(path, text);
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000A5BC File Offset: 0x000087BC
		public CatalogItemContextBase<ExternalItemPath> PrimaryContext
		{
			get
			{
				return this.m_primaryContext;
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000A5C4 File Offset: 0x000087C4
		protected override CatalogItemContextBase<ExternalItemPath> CreateContext(IPathTranslator pathTranslator)
		{
			return new CatalogItemContext(pathTranslator);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000A5CC File Offset: 0x000087CC
		public override bool IsReportServerPathOrUrl(string pathOrUrl, bool checkProtocol, out bool isRelative)
		{
			return RSPathUtil.IsReportServerPathOrUrl(pathOrUrl, checkProtocol, out isRelative);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000A5D6 File Offset: 0x000087D6
		public override bool IsSupportedProtocol(string path, bool checkProtocol)
		{
			return RSPathUtil.Instance.IsSupportedUrl(path, checkProtocol);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000A5E4 File Offset: 0x000087E4
		public override bool IsSupportedProtocol(string path, bool checkProtocol, out bool isRelative)
		{
			isRelative = false;
			return this.PathManager.IsSupportedUrl(path, checkProtocol, out isRelative);
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000A5F8 File Offset: 0x000087F8
		public override string HostRootUri
		{
			get
			{
				if (this.m_cachedServerVdir == null)
				{
					if (!string.IsNullOrEmpty(ProcessingContext.Configuration.UrlRootCalculated))
					{
						this.m_cachedServerVdir = ProcessingContext.Configuration.UrlRootCalculated;
					}
					else
					{
						this.m_cachedServerVdir = ProcessingContext.Configuration.ReportServerVirtualDirectory;
					}
				}
				return this.m_cachedServerVdir;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000A646 File Offset: 0x00008846
		public override IPathManager PathManager
		{
			get
			{
				if (this.m_pathManager == null)
				{
					this.m_pathManager = RSPathUtil.GetPathManager();
				}
				return this.m_pathManager;
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000A661 File Offset: 0x00008861
		protected override RSRequestParameters CreateRequestParametersInstance()
		{
			return new CatalogItemContext.ConfigFileBasedRequestParameters();
		}

		// Token: 0x04000163 RID: 355
		private CatalogItemPath m_CatalogItemPath;

		// Token: 0x04000164 RID: 356
		private string m_cachedServerVdir;

		// Token: 0x020000EB RID: 235
		internal sealed class ConfigFileBasedRequestParameters : RSRequestParameters
		{
			// Token: 0x060007BC RID: 1980 RVA: 0x000146A8 File Offset: 0x000128A8
			protected override void ApplyDefaultRenderingParameters()
			{
				if (base.FormatParamValue == null || base.FormatParamValue.Trim().Length == 0)
				{
					return;
				}
				RSTrace.CatalogTrace.Assert(ProcessingContext.Configuration.Extensions != null, "Extensions");
				RenderingExtension renderingExtension = (RenderingExtension)ProcessingContext.Configuration.Extensions.Renderer[base.FormatParamValue];
				if (renderingExtension == null)
				{
					return;
				}
				NameValueCollection deviceInfo = renderingExtension.DeviceInfo;
				if (deviceInfo == null || deviceInfo.Count <= 0)
				{
					return;
				}
				for (int i = 0; i < deviceInfo.Count; i++)
				{
					if (this.m_renderingParameters[deviceInfo.GetKey(i)] == null)
					{
						this.m_renderingParameters.Add(deviceInfo.GetKey(i), deviceInfo[i]);
					}
				}
			}
		}
	}
}
