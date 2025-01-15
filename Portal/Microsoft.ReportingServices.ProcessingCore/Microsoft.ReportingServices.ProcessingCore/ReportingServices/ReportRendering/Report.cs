using System;
using System.Collections;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000017 RID: 23
	public sealed class Report : IDocumentMapEntry
	{
		// Token: 0x06000368 RID: 872 RVA: 0x00008588 File Offset: 0x00006788
		internal Report(Report reportDef, ReportInstance reportInstance, RenderingContext renderingContext, string reportName, string description, CultureInfo defaultLanguage)
		{
			this.m_reportDef = reportDef;
			this.m_reportInstance = reportInstance;
			this.m_renderingContext = renderingContext;
			this.m_reportBody = null;
			this.m_pageHeader = null;
			this.m_pageFooter = null;
			this.m_reportPagination = null;
			this.m_name = reportName;
			this.m_description = description;
			this.m_reportUrl = null;
			this.m_documentMapRoot = null;
			this.m_reportParameters = null;
			if (reportDef.Language != null)
			{
				if (reportDef.Language.Type == ExpressionInfo.Types.Constant)
				{
					this.m_reportLanguage = reportDef.Language.Value;
				}
				else if (reportInstance != null)
				{
					this.m_reportLanguage = reportInstance.Language;
				}
			}
			if (this.m_reportLanguage == null && defaultLanguage != null)
			{
				this.m_reportLanguage = defaultLanguage.Name;
			}
			this.AdjustBodyWhitespace();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00008648 File Offset: 0x00006848
		private void AdjustBodyWhitespace()
		{
			if (this.m_reportDef.ReportItems != null && this.m_reportDef.ReportItems.Count != 0)
			{
				double num = 0.0;
				double num2 = 0.0;
				int count = this.m_reportDef.ReportItems.Count;
				for (int i = 0; i < count; i++)
				{
					ReportItem reportItem = this.m_reportDef.ReportItems[i];
					num = Math.Max(num, reportItem.LeftValue + reportItem.WidthValue);
					num2 = Math.Max(num2, reportItem.TopValue + reportItem.HeightValue);
				}
				this.m_reportDef.HeightValue = Math.Min(this.m_reportDef.HeightValue, num2);
				string text = "{0:0.#####}mm";
				this.m_reportDef.Height = string.Format(CultureInfo.InvariantCulture, text, this.m_reportDef.HeightValue);
				double num3 = Math.Max(1.0, this.m_reportDef.PageWidthValue - this.m_reportDef.LeftMarginValue - this.m_reportDef.RightMarginValue);
				if (this.m_reportDef.Columns > 1)
				{
					num3 -= (double)(this.m_reportDef.Columns - 1) * this.m_reportDef.ColumnSpacingValue;
					num3 = Math.Max(1.0, num3 / (double)this.m_reportDef.Columns);
				}
				num = Math.Round(num, 1);
				num3 = Math.Round(num3, 1);
				this.m_reportDef.WidthValue = Math.Min(this.m_reportDef.WidthValue, num3 * Math.Ceiling(num / num3));
				this.m_reportDef.Width = string.Format(CultureInfo.InvariantCulture, text, this.m_reportDef.WidthValue);
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00008818 File Offset: 0x00006A18
		public string UniqueName
		{
			get
			{
				if (this.m_reportInstance == null)
				{
					return null;
				}
				return this.m_reportInstance.UniqueName.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00008847 File Offset: 0x00006A47
		public string ShowHideToggle
		{
			get
			{
				return this.m_renderingContext.TopLevelReportContext.RSRequestParameters.ShowHideToggleParamValue;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000885E File Offset: 0x00006A5E
		public string SortItem
		{
			get
			{
				return this.m_renderingContext.TopLevelReportContext.RSRequestParameters.SortIdParamValue;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00008875 File Offset: 0x00006A75
		public bool InDocumentMap
		{
			get
			{
				return this.m_renderingContext.ReportSnapshot.HasDocumentMap;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00008887 File Offset: 0x00006A87
		public bool HasBookmarks
		{
			get
			{
				return this.m_renderingContext.ReportSnapshot.HasBookmarks;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00008899 File Offset: 0x00006A99
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000370 RID: 880 RVA: 0x000088A4 File Offset: 0x00006AA4
		internal DocumentMapNode DocumentMap
		{
			get
			{
				DocumentMapNode documentMapNode = this.m_documentMapRoot;
				if (this.m_documentMapRoot == null && this.InDocumentMap)
				{
					documentMapNode = new DocumentMapNode(this.m_renderingContext.ReportSnapshot.GetDocumentMap(this.m_renderingContext.ChunkManager));
					if (this.m_renderingContext.CacheState)
					{
						this.m_documentMapRoot = documentMapNode;
					}
				}
				return documentMapNode;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00008900 File Offset: 0x00006B00
		internal Bookmarks ReportBookmarks
		{
			get
			{
				Bookmarks bookmarks = this.m_bookmarksInfo;
				if (this.m_bookmarksInfo == null && this.HasBookmarks)
				{
					bookmarks = new Bookmarks(this.m_renderingContext.ReportSnapshot.GetBookmarksInfo(this.m_renderingContext.ChunkManager));
					if (this.m_renderingContext.CacheState)
					{
						this.m_bookmarksInfo = bookmarks;
					}
				}
				return bookmarks;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000895A File Offset: 0x00006B5A
		public string Description
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00008964 File Offset: 0x00006B64
		public ReportUrl Location
		{
			get
			{
				ReportUrl reportUrl = this.m_reportUrl;
				if (this.m_reportUrl == null)
				{
					reportUrl = new ReportUrl(this.m_renderingContext, null);
					if (this.m_renderingContext.CacheState)
					{
						this.m_reportUrl = reportUrl;
					}
				}
				return reportUrl;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000374 RID: 884 RVA: 0x000089A2 File Offset: 0x00006BA2
		public string ReportLanguage
		{
			get
			{
				return this.m_reportLanguage;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000375 RID: 885 RVA: 0x000089AA File Offset: 0x00006BAA
		// (set) Token: 0x06000376 RID: 886 RVA: 0x000089B7 File Offset: 0x00006BB7
		public bool CacheState
		{
			get
			{
				return this.m_renderingContext.CacheState;
			}
			set
			{
				this.m_renderingContext.CacheState = value;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000377 RID: 887 RVA: 0x000089C5 File Offset: 0x00006BC5
		public DateTime ExecutionTime
		{
			get
			{
				return this.m_renderingContext.ExecutionTime;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000378 RID: 888 RVA: 0x000089D2 File Offset: 0x00006BD2
		public string Author
		{
			get
			{
				return this.m_reportDef.Author;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000379 RID: 889 RVA: 0x000089DF File Offset: 0x00006BDF
		public string DataSetName
		{
			get
			{
				return this.m_reportDef.OneDataSetName;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600037A RID: 890 RVA: 0x000089EC File Offset: 0x00006BEC
		public bool NeedsHeaderFooterEvaluation
		{
			get
			{
				return this.m_reportDef.PageHeaderEvaluation || this.m_reportDef.PageFooterEvaluation;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00008A08 File Offset: 0x00006C08
		public PageSection PageHeader
		{
			get
			{
				PageSection pageSection = this.m_pageHeader;
				if (this.m_pageHeader == null)
				{
					if (this.m_reportDef.PageHeader == null)
					{
						return null;
					}
					string text = "ph";
					RenderingContext renderingContext = new RenderingContext(this.m_renderingContext, text);
					pageSection = new PageSection(text, this.m_reportDef.PageHeader, null, this, renderingContext, this.m_reportDef.PageHeaderEvaluation);
					if (this.m_renderingContext.CacheState)
					{
						this.m_pageHeader = pageSection;
					}
				}
				return pageSection;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00008A7C File Offset: 0x00006C7C
		public PageSection PageFooter
		{
			get
			{
				PageSection pageSection = this.m_pageFooter;
				if (this.m_pageFooter == null)
				{
					if (this.m_reportDef.PageFooter == null)
					{
						return null;
					}
					string text = "pf";
					RenderingContext renderingContext = new RenderingContext(this.m_renderingContext, text);
					pageSection = new PageSection(text, this.m_reportDef.PageFooter, null, this, renderingContext, this.m_reportDef.PageFooterEvaluation);
					if (this.m_renderingContext.CacheState)
					{
						this.m_pageFooter = pageSection;
					}
				}
				return pageSection;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00008AEF File Offset: 0x00006CEF
		public int AutoRefresh
		{
			get
			{
				return this.m_reportDef.AutoRefresh;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00008AFC File Offset: 0x00006CFC
		public ReportSize Width
		{
			get
			{
				if (this.m_reportDef.WidthForRendering == null)
				{
					this.m_reportDef.WidthForRendering = new ReportSize(this.m_reportDef.Width, this.m_reportDef.WidthValue);
				}
				return this.m_reportDef.WidthForRendering;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00008B3C File Offset: 0x00006D3C
		public ReportSize PageHeight
		{
			get
			{
				if (this.m_reportDef.PageHeightForRendering == null)
				{
					this.m_reportDef.PageHeightForRendering = new ReportSize(this.m_reportDef.PageHeight, this.m_reportDef.PageHeightValue);
				}
				return this.m_reportDef.PageHeightForRendering;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00008B7C File Offset: 0x00006D7C
		public ReportSize PageWidth
		{
			get
			{
				if (this.m_reportDef.PageWidthForRendering == null)
				{
					this.m_reportDef.PageWidthForRendering = new ReportSize(this.m_reportDef.PageWidth, this.m_reportDef.PageWidthValue);
				}
				return this.m_reportDef.PageWidthForRendering;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00008BBC File Offset: 0x00006DBC
		public int Columns
		{
			get
			{
				return this.m_reportDef.Columns;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00008BC9 File Offset: 0x00006DC9
		public ReportSize ColumnSpacing
		{
			get
			{
				if (this.m_reportDef.ColumnSpacingForRendering == null)
				{
					this.m_reportDef.ColumnSpacingForRendering = new ReportSize(this.m_reportDef.ColumnSpacing, this.m_reportDef.ColumnSpacingValue);
				}
				return this.m_reportDef.ColumnSpacingForRendering;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00008C0C File Offset: 0x00006E0C
		public PageCollection Pages
		{
			get
			{
				PageCollection pageCollection = this.m_reportPagination;
				if (this.m_reportPagination == null)
				{
					pageCollection = new PageCollection(this.m_renderingContext.RenderingInfoManager.PaginationInfo, this);
					if (this.m_renderingContext.CacheState)
					{
						this.m_reportPagination = pageCollection;
					}
				}
				return pageCollection;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00008C54 File Offset: 0x00006E54
		public ReportParameterCollection Parameters
		{
			get
			{
				ReportParameterCollection reportParameterCollection = this.m_reportParameters;
				if (this.m_reportInstance != null && this.m_reportParameters == null)
				{
					reportParameterCollection = new ReportParameterCollection(this.InstanceInfo.Parameters);
					if (this.m_renderingContext.CacheState)
					{
						this.m_reportParameters = reportParameterCollection;
					}
				}
				return reportParameterCollection;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00008CA0 File Offset: 0x00006EA0
		public Rectangle Body
		{
			get
			{
				Rectangle rectangle = this.m_reportBody;
				if (this.m_reportBody == null)
				{
					rectangle = new Rectangle(null, (this.m_reportInstance != null) ? this.InstanceInfo.BodyUniqueName : 0, this.m_reportDef, this.m_reportInstance, this.m_renderingContext, null);
					if (this.m_renderingContext.CacheState)
					{
						this.m_reportBody = rectangle;
					}
				}
				return rectangle;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00008D01 File Offset: 0x00006F01
		public ReportSize LeftMargin
		{
			get
			{
				if (this.m_reportDef.LeftMarginForRendering == null)
				{
					this.m_reportDef.LeftMarginForRendering = new ReportSize(this.m_reportDef.LeftMargin, this.m_reportDef.LeftMarginValue);
				}
				return this.m_reportDef.LeftMarginForRendering;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00008D41 File Offset: 0x00006F41
		public ReportSize RightMargin
		{
			get
			{
				if (this.m_reportDef.RightMarginForRendering == null)
				{
					this.m_reportDef.RightMarginForRendering = new ReportSize(this.m_reportDef.RightMargin, this.m_reportDef.RightMarginValue);
				}
				return this.m_reportDef.RightMarginForRendering;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00008D81 File Offset: 0x00006F81
		public ReportSize TopMargin
		{
			get
			{
				if (this.m_reportDef.TopMarginForRendering == null)
				{
					this.m_reportDef.TopMarginForRendering = new ReportSize(this.m_reportDef.TopMargin, this.m_reportDef.TopMarginValue);
				}
				return this.m_reportDef.TopMarginForRendering;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00008DC1 File Offset: 0x00006FC1
		public ReportSize BottomMargin
		{
			get
			{
				if (this.m_reportDef.BottomMarginForRendering == null)
				{
					this.m_reportDef.BottomMarginForRendering = new ReportSize(this.m_reportDef.BottomMargin, this.m_reportDef.BottomMarginValue);
				}
				return this.m_reportDef.BottomMarginForRendering;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00008E01 File Offset: 0x00007001
		// (set) Token: 0x0600038B RID: 907 RVA: 0x00008E28 File Offset: 0x00007028
		public object SharedRenderingInfo
		{
			get
			{
				return this.m_renderingContext.RenderingInfoManager.SharedRenderingInfo[this.m_reportDef.ID];
			}
			set
			{
				this.m_renderingContext.RenderingInfoManager.SharedRenderingInfo[this.m_reportDef.ID] = value;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00008E50 File Offset: 0x00007050
		// (set) Token: 0x0600038D RID: 909 RVA: 0x00008E81 File Offset: 0x00007081
		public object RenderingInfo
		{
			get
			{
				if (this.m_reportInstance == null)
				{
					return null;
				}
				return this.m_renderingContext.RenderingInfoManager.RenderingInfo[this.m_reportInstance.UniqueName];
			}
			set
			{
				if (this.m_reportInstance == null)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.m_renderingContext.RenderingInfoManager.RenderingInfo[this.m_reportInstance.UniqueName] = value;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00008EBC File Offset: 0x000070BC
		public string Custom
		{
			get
			{
				string text = this.m_reportDef.Custom;
				if (text == null && this.CustomProperties != null)
				{
					CustomProperty customProperty = this.CustomProperties["Custom"];
					if (customProperty != null && customProperty.Value != null)
					{
						text = DataTypeUtility.ConvertToInvariantString(customProperty.Value);
					}
				}
				return text;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00008F0C File Offset: 0x0000710C
		public CustomPropertyCollection CustomProperties
		{
			get
			{
				CustomPropertyCollection customPropertyCollection = this.m_customProperties;
				if (this.m_customProperties == null && this.m_reportDef.CustomProperties != null)
				{
					if (this.m_reportInstance != null)
					{
						customPropertyCollection = new CustomPropertyCollection(this.m_reportDef.CustomProperties, this.InstanceInfo.CustomPropertyInstances);
					}
					else
					{
						customPropertyCollection = new CustomPropertyCollection(this.m_reportDef.CustomProperties, null);
					}
					if (this.m_renderingContext.CacheState)
					{
						this.m_customProperties = customPropertyCollection;
					}
				}
				return customPropertyCollection;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00008F82 File Offset: 0x00007182
		public string DataTransform
		{
			get
			{
				return this.m_reportDef.DataTransform;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00008F8F File Offset: 0x0000718F
		public string DataSchema
		{
			get
			{
				return this.m_reportDef.DataSchema;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00008F9C File Offset: 0x0000719C
		public string DataElementName
		{
			get
			{
				return this.m_reportDef.DataElementName;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00008FA9 File Offset: 0x000071A9
		public Report.DataElementStyles DataElementStyle
		{
			get
			{
				if (!this.m_reportDef.DataElementStyleAttribute)
				{
					return Report.DataElementStyles.ElementNormal;
				}
				return Report.DataElementStyles.AttributeNormal;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00008FBB File Offset: 0x000071BB
		public bool ShowHideStateChanged
		{
			get
			{
				return this.m_renderingContext.ShowHideStateChanged;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00008FC8 File Offset: 0x000071C8
		internal Report ReportDef
		{
			get
			{
				return this.m_reportDef;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00008FD0 File Offset: 0x000071D0
		internal ReportInstance ReportInstance
		{
			get
			{
				return this.m_reportInstance;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00008FD8 File Offset: 0x000071D8
		internal ReportInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_reportInstance == null)
				{
					return null;
				}
				if (this.m_reportInstanceInfo == null)
				{
					this.m_reportInstanceInfo = this.m_reportInstance.GetCachedReportInstanceInfo(this.m_renderingContext.ChunkManager);
				}
				return this.m_reportInstanceInfo;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000900E File Offset: 0x0000720E
		internal RenderingContext RenderingContext
		{
			get
			{
				return this.m_renderingContext;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00009016 File Offset: 0x00007216
		public int NumberOfPages
		{
			get
			{
				if (this.m_reportInstance == null)
				{
					return 0;
				}
				return this.m_reportInstance.NumberOfPages;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00009030 File Offset: 0x00007230
		internal bool BodyHasBorderStyles
		{
			get
			{
				if (this.m_bodyStyleConstainsBorder == null)
				{
					this.m_bodyStyleConstainsBorder = new bool?(false);
					Style styleClass = this.m_reportDef.StyleClass;
					if (styleClass != null && styleClass.StyleAttributes != null && styleClass.StyleAttributes.Count > 0)
					{
						StyleAttributeHashtable styleAttributes = styleClass.StyleAttributes;
						if (styleAttributes.ContainsKey("BorderStyle"))
						{
							AttributeInfo attributeInfo = styleAttributes["BorderStyle"];
							if (attributeInfo.IsExpression || !Validator.CompareWithInvariantCulture(attributeInfo.Value, "None"))
							{
								this.m_bodyStyleConstainsBorder = new bool?(true);
								return true;
							}
						}
						if (styleAttributes.ContainsKey("BorderStyleLeft"))
						{
							AttributeInfo attributeInfo2 = styleAttributes["BorderStyleLeft"];
							if (attributeInfo2.IsExpression || !Validator.CompareWithInvariantCulture(attributeInfo2.Value, "None"))
							{
								this.m_bodyStyleConstainsBorder = new bool?(true);
								return true;
							}
						}
						if (styleAttributes.ContainsKey("BorderStyleRight"))
						{
							AttributeInfo attributeInfo3 = styleAttributes["BorderStyleRight"];
							if (attributeInfo3.IsExpression || !Validator.CompareWithInvariantCulture(attributeInfo3.Value, "None"))
							{
								this.m_bodyStyleConstainsBorder = new bool?(true);
								return true;
							}
						}
						if (styleAttributes.ContainsKey("BorderStyleTop"))
						{
							AttributeInfo attributeInfo4 = styleAttributes["BorderStyleTop"];
							if (attributeInfo4.IsExpression || !Validator.CompareWithInvariantCulture(attributeInfo4.Value, "None"))
							{
								this.m_bodyStyleConstainsBorder = new bool?(true);
								return true;
							}
						}
						if (styleAttributes.ContainsKey("BorderStyleBottom"))
						{
							AttributeInfo attributeInfo5 = styleAttributes["BorderStyleBottom"];
							if (attributeInfo5.IsExpression || !Validator.CompareWithInvariantCulture(attributeInfo5.Value, "None"))
							{
								this.m_bodyStyleConstainsBorder = new bool?(true);
								return true;
							}
						}
					}
				}
				return this.m_bodyStyleConstainsBorder.Value;
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000091E7 File Offset: 0x000073E7
		public string StreamURL(bool useSessionId, string streamName)
		{
			return this.m_renderingContext.TopLevelReportContext.RSRequestParameters.GetImageUrl(useSessionId, streamName, this.m_renderingContext.TopLevelReportContext);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000920B File Offset: 0x0000740B
		public ReportUrlBuilder GetReportUrlBuilder(string initialUrl, bool useReplacementRoot, bool addReportParameters)
		{
			return new ReportUrlBuilder(this.m_renderingContext, initialUrl, useReplacementRoot, addReportParameters);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000921C File Offset: 0x0000741C
		public bool GetResource(string resourcePath, out byte[] resource, out string mimeType)
		{
			if (this.m_renderingContext.GetResourceCallback != null)
			{
				bool flag;
				bool flag2;
				this.m_renderingContext.GetResourceCallback.GetResource(this.m_renderingContext.CurrentReportContext, resourcePath, out resource, out mimeType, out flag, out flag2);
				return true;
			}
			resource = null;
			mimeType = null;
			return false;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00009264 File Offset: 0x00007464
		public ReportItem Find(string uniqueName)
		{
			if (uniqueName == null || uniqueName.Length <= 0)
			{
				return null;
			}
			int num = ReportItem.StringToInt(uniqueName);
			if (num < 0)
			{
				return null;
			}
			return this.m_renderingContext.FindReportItemInBody(num);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00009298 File Offset: 0x00007498
		public void EnableNativeCustomReportItem()
		{
			Global.Tracer.Assert(this.m_renderingContext != null);
			this.m_renderingContext.NativeCRITypes = null;
			this.m_renderingContext.NativeAllCRITypes = true;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000092C8 File Offset: 0x000074C8
		public void EnableNativeCustomReportItem(string type)
		{
			Global.Tracer.Assert(this.m_renderingContext != null);
			if (type == null)
			{
				this.m_renderingContext.NativeCRITypes = null;
				this.m_renderingContext.NativeAllCRITypes = true;
			}
			if (this.m_renderingContext.NativeCRITypes == null)
			{
				this.m_renderingContext.NativeCRITypes = new Hashtable();
			}
			if (!this.m_renderingContext.NativeCRITypes.ContainsKey(type))
			{
				this.m_renderingContext.NativeCRITypes.Add(type, null);
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00009348 File Offset: 0x00007548
		internal bool Search(int searchPage, string findValue)
		{
			SearchContext searchContext = new SearchContext(searchPage, findValue, 0, this.NumberOfPages - 1);
			PageSection pageSection = this.PageHeader;
			PageSection pageSection2 = this.PageFooter;
			bool flag = false;
			bool flag2 = false;
			if (pageSection != null && ((searchPage > 0 && searchPage < this.NumberOfPages - 1) || (searchPage == 0 && pageSection.PrintOnFirstPage) || (searchPage != 0 && searchPage == this.NumberOfPages - 1 && pageSection.PrintOnLastPage)))
			{
				flag = true;
			}
			if (pageSection2 != null && ((searchPage > 0 && searchPage < this.NumberOfPages - 1) || (searchPage != this.NumberOfPages - 1 && searchPage == 0 && pageSection2.PrintOnFirstPage) || (searchPage == this.NumberOfPages - 1 && pageSection2.PrintOnLastPage)))
			{
				flag2 = true;
			}
			if ((flag || flag2) && this.NeedsHeaderFooterEvaluation)
			{
				PageSection pageSection3 = null;
				PageSection pageSection4 = null;
				ReportProcessing.EvaluateHeaderFooterExpressions(searchPage + 1, this.NumberOfPages, this, null, out pageSection3, out pageSection4);
				if (this.m_reportDef.PageHeaderEvaluation)
				{
					pageSection = pageSection3;
				}
				if (this.m_reportDef.PageFooterEvaluation)
				{
					pageSection2 = pageSection4;
				}
			}
			bool flag3 = false;
			if (flag)
			{
				flag3 = pageSection.Search(searchContext);
			}
			if (!flag3)
			{
				flag3 = this.Body.Search(searchContext);
				if (!flag3 && flag2)
				{
					flag3 = pageSection2.Search(searchContext);
				}
			}
			return flag3;
		}

		// Token: 0x04000050 RID: 80
		private Report m_reportDef;

		// Token: 0x04000051 RID: 81
		private ReportInstance m_reportInstance;

		// Token: 0x04000052 RID: 82
		private ReportInstanceInfo m_reportInstanceInfo;

		// Token: 0x04000053 RID: 83
		private RenderingContext m_renderingContext;

		// Token: 0x04000054 RID: 84
		private Rectangle m_reportBody;

		// Token: 0x04000055 RID: 85
		private PageSection m_pageHeader;

		// Token: 0x04000056 RID: 86
		private PageSection m_pageFooter;

		// Token: 0x04000057 RID: 87
		private PageCollection m_reportPagination;

		// Token: 0x04000058 RID: 88
		private string m_name;

		// Token: 0x04000059 RID: 89
		private string m_description;

		// Token: 0x0400005A RID: 90
		private ReportUrl m_reportUrl;

		// Token: 0x0400005B RID: 91
		private DocumentMapNode m_documentMapRoot;

		// Token: 0x0400005C RID: 92
		private ReportParameterCollection m_reportParameters;

		// Token: 0x0400005D RID: 93
		private string m_reportLanguage;

		// Token: 0x0400005E RID: 94
		private CustomPropertyCollection m_customProperties;

		// Token: 0x0400005F RID: 95
		private Bookmarks m_bookmarksInfo;

		// Token: 0x04000060 RID: 96
		private bool? m_bodyStyleConstainsBorder;

		// Token: 0x0200090A RID: 2314
		public enum DataElementStyles
		{
			// Token: 0x04003EBF RID: 16063
			AttributeNormal,
			// Token: 0x04003EC0 RID: 16064
			ElementNormal
		}
	}
}
