using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002FC RID: 764
	public sealed class Report : IDefinitionPath, IReportScope
	{
		// Token: 0x06001BA9 RID: 7081 RVA: 0x0006EBE4 File Offset: 0x0006CDE4
		internal Report(Microsoft.ReportingServices.ReportIntermediateFormat.Report reportDef, Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, string reportName, string description)
		{
			this.m_parentDefinitionPath = null;
			this.m_isOldSnapshot = false;
			this.m_reportDef = reportDef;
			this.m_reportInstance = reportInstance;
			this.m_renderingContext = renderingContext;
			this.m_name = reportName;
			this.m_description = description;
			if (reportDef.HasHeadersOrFooters)
			{
				this.m_pageEvaluation = new OnDemandPageEvaluation(this);
				this.m_renderingContext.SetPageEvaluation(this.m_pageEvaluation);
			}
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0006EC4F File Offset: 0x0006CE4F
		internal Report(Microsoft.ReportingServices.ReportIntermediateFormat.Report reportDef, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, string reportName, string description)
		{
			this.m_parentDefinitionPath = null;
			this.m_isOldSnapshot = false;
			this.m_reportDef = reportDef;
			this.m_reportInstance = null;
			this.m_renderingContext = renderingContext;
			this.m_name = reportName;
			this.m_description = description;
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0006EC8C File Offset: 0x0006CE8C
		internal Report(Microsoft.ReportingServices.ReportProcessing.Report reportDef, Microsoft.ReportingServices.ReportProcessing.ReportInstance reportInstance, Microsoft.ReportingServices.ReportRendering.RenderingContext oldRenderingContext, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, string reportName, string description)
		{
			this.m_renderReport = new Microsoft.ReportingServices.ReportRendering.Report(reportDef, reportInstance, oldRenderingContext, reportName, description, Localization.DefaultReportServerSpecificCulture);
			this.m_parentDefinitionPath = null;
			this.m_isOldSnapshot = true;
			this.m_subreportInSubtotal = false;
			this.m_renderingContext = renderingContext;
			this.m_name = reportName;
			this.m_description = description;
			if (this.m_renderReport.NeedsHeaderFooterEvaluation)
			{
				this.m_pageEvaluation = new ShimPageEvaluation(this);
				this.m_renderingContext.SetPageEvaluation(this.m_pageEvaluation);
			}
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0006ED10 File Offset: 0x0006CF10
		internal Report(IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.ReportIntermediateFormat.Report reportDef, Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, string reportName, string description, bool subreportInSubtotal)
		{
			this.m_parentDefinitionPath = parentDefinitionPath;
			this.m_isOldSnapshot = false;
			this.m_reportDef = reportDef;
			this.m_reportInstance = reportInstance;
			this.m_isOldSnapshot = false;
			this.m_subreportInSubtotal = subreportInSubtotal;
			this.m_renderingContext = renderingContext;
			this.m_name = reportName;
			this.m_description = description;
			this.m_pageEvaluation = null;
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0006ED70 File Offset: 0x0006CF70
		internal Report(IDefinitionPath parentDefinitionPath, bool subreportInSubtotal, Microsoft.ReportingServices.ReportRendering.SubReport subReport, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			this.m_parentDefinitionPath = parentDefinitionPath;
			this.m_renderReport = subReport.Report;
			this.m_isOldSnapshot = true;
			this.m_subreportInSubtotal = subreportInSubtotal;
			if (this.m_renderReport != null)
			{
				this.m_name = this.m_renderReport.Name;
				this.m_description = this.m_renderReport.Description;
			}
			this.m_renderingContext = new Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext(renderingContext);
			this.m_pageEvaluation = null;
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x0006EDE2 File Offset: 0x0006CFE2
		public string DefinitionPath
		{
			get
			{
				if (this.m_parentDefinitionPath != null)
				{
					return this.m_parentDefinitionPath.DefinitionPath + "xS";
				}
				return "xA";
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x0006EE07 File Offset: 0x0006D007
		public IDefinitionPath ParentDefinitionPath
		{
			get
			{
				return this.m_parentDefinitionPath;
			}
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x0006EE0F File Offset: 0x0006D00F
		[Obsolete("Use ReportSection.SetPage(Int32, Int32) instead.")]
		public void SetPage(int pageNumber, int totalPages)
		{
			this.FirstSection.SetPage(pageNumber, totalPages);
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0006EE1E File Offset: 0x0006D01E
		[Obsolete("Use ReportSection.GetPageSections() instead.")]
		public void GetPageSections()
		{
			this.FirstSection.GetPageSections();
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0006EE2B File Offset: 0x0006D02B
		public void AddToCurrentPage(string textboxDefinitionName, object textboxInstanceOriginalValue)
		{
			if (this.m_pageEvaluation == null)
			{
				return;
			}
			this.m_pageEvaluation.Add(textboxDefinitionName, textboxInstanceOriginalValue);
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0006EE44 File Offset: 0x0006D044
		public void EnableNativeCustomReportItem()
		{
			if (this.IsOldSnapshot)
			{
				this.m_renderReport.RenderingContext.NativeCRITypes = null;
				this.m_renderReport.RenderingContext.NativeAllCRITypes = true;
				return;
			}
			this.m_renderingContext.NativeCRITypes = null;
			this.m_renderingContext.NativeAllCRITypes = true;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x0006EE94 File Offset: 0x0006D094
		public void EnableNativeCustomReportItem(string type)
		{
			if (type == null)
			{
				this.EnableNativeCustomReportItem();
				return;
			}
			if (this.IsOldSnapshot)
			{
				if (this.m_renderReport.RenderingContext.NativeCRITypes == null)
				{
					this.m_renderReport.RenderingContext.NativeCRITypes = new Hashtable();
				}
				if (!this.m_renderReport.RenderingContext.NativeCRITypes.ContainsKey(type))
				{
					this.m_renderReport.RenderingContext.NativeCRITypes.Add(type, null);
					return;
				}
			}
			else
			{
				if (this.m_renderingContext.NativeCRITypes == null)
				{
					this.m_renderingContext.NativeCRITypes = new Hashtable();
				}
				if (!this.m_renderingContext.NativeCRITypes.ContainsKey(type))
				{
					this.m_renderingContext.NativeCRITypes.Add(type, null);
				}
			}
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0006EF4C File Offset: 0x0006D14C
		public Stream GetOrCreateChunk(Microsoft.ReportingServices.OnDemandReportRendering.Report.ChunkTypes type, string chunkName, out bool isNewChunk)
		{
			return this.m_renderingContext.GetOrCreateChunk((ReportProcessing.ReportChunkTypes)type, chunkName, true, out isNewChunk);
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0006EF5D File Offset: 0x0006D15D
		public Stream CreateChunk(Microsoft.ReportingServices.OnDemandReportRendering.Report.ChunkTypes type, string chunkName)
		{
			return this.m_renderingContext.CreateChunk((ReportProcessing.ReportChunkTypes)type, chunkName);
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0006EF6C File Offset: 0x0006D16C
		public Stream GetChunk(Microsoft.ReportingServices.OnDemandReportRendering.Report.ChunkTypes type, string chunkName)
		{
			bool flag;
			return this.m_renderingContext.GetOrCreateChunk((ReportProcessing.ReportChunkTypes)type, chunkName, false, out flag);
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x0006EF89 File Offset: 0x0006D189
		public string ID
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderReport.Body.ID + "xB";
				}
				return this.ReportDef.RenderingModelID;
			}
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x0006EFB9 File Offset: 0x0006D1B9
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x0006EFC1 File Offset: 0x0006D1C1
		public string Description
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x0006EFC9 File Offset: 0x0006D1C9
		public string Author
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderReport.Author;
				}
				return this.m_reportDef.Author;
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x0006EFEA File Offset: 0x0006D1EA
		public string DefaultFontFamily
		{
			get
			{
				return this.m_reportDef.DefaultFontFamily;
			}
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x0006EFF7 File Offset: 0x0006D1F7
		public string DataSetName
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderReport.DataSetName;
				}
				return this.m_reportDef.OneDataSetName;
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x0006F018 File Offset: 0x0006D218
		[Obsolete("Use ReportSection.NeedsHeaderFooterEvaluation instead.")]
		public bool NeedsHeaderFooterEvaluation
		{
			get
			{
				ReportSection firstSection = this.FirstSection;
				return firstSection.NeedsTotalPages || firstSection.NeedsReportItemsOnPage;
			}
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x0006F03C File Offset: 0x0006D23C
		public bool NeedsTotalPages
		{
			get
			{
				return this.NeedsPageBreakTotalPages || this.NeedsOverallTotalPages;
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x0006F04E File Offset: 0x0006D24E
		public bool NeedsPageBreakTotalPages
		{
			get
			{
				this.CacheHeaderFooterFlags();
				return this.m_cachedNeedsPageBreakTotalPages;
			}
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x0006F05C File Offset: 0x0006D25C
		public bool NeedsOverallTotalPages
		{
			get
			{
				this.CacheHeaderFooterFlags();
				return this.m_cachedNeedsOverallTotalPages;
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x0006F06A File Offset: 0x0006D26A
		public bool NeedsReportItemsOnPage
		{
			get
			{
				this.CacheHeaderFooterFlags();
				return this.m_cachedNeedsReportItemsOnPage;
			}
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0006F078 File Offset: 0x0006D278
		private void CacheHeaderFooterFlags()
		{
			if (this.m_hasCachedHeaderFooterFlags)
			{
				return;
			}
			this.m_cachedNeedsPageBreakTotalPages = false;
			this.m_cachedNeedsReportItemsOnPage = false;
			this.m_cachedNeedsOverallTotalPages = false;
			foreach (ReportSection reportSection in this.ReportSections)
			{
				this.m_cachedNeedsReportItemsOnPage |= reportSection.NeedsReportItemsOnPage;
				this.m_cachedNeedsOverallTotalPages |= reportSection.NeedsOverallTotalPages;
				this.m_cachedNeedsPageBreakTotalPages |= reportSection.NeedsPageBreakTotalPages;
			}
			this.m_hasCachedHeaderFooterFlags = true;
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x0006F11C File Offset: 0x0006D31C
		public int AutoRefresh
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderReport.AutoRefresh;
				}
				return this.Instance.AutoRefresh;
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x0006F13D File Offset: 0x0006D33D
		[Obsolete("Use ReportSection.Width instead.")]
		public ReportSize Width
		{
			get
			{
				return this.FirstSection.Width;
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x0006F14A File Offset: 0x0006D34A
		internal DataSetCollection DataSets
		{
			get
			{
				if (this.m_dataSets == null)
				{
					if (this.m_isOldSnapshot)
					{
						return null;
					}
					this.m_dataSets = new DataSetCollection(this.m_reportDef, this.m_renderingContext);
				}
				return this.m_dataSets;
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x0006F17B File Offset: 0x0006D37B
		[Obsolete("Use ReportSection.Body instead.")]
		public Body Body
		{
			get
			{
				return this.FirstSection.Body;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x0006F188 File Offset: 0x0006D388
		[Obsolete("Use ReportSection.Page instead.")]
		public Microsoft.ReportingServices.OnDemandReportRendering.Page Page
		{
			get
			{
				return this.FirstSection.Page;
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x0006F195 File Offset: 0x0006D395
		public ReportSectionCollection ReportSections
		{
			get
			{
				if (this.m_reportSections == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_reportSections = new ReportSectionCollection(this, this.m_renderReport);
					}
					else
					{
						this.m_reportSections = new ReportSectionCollection(this);
					}
				}
				return this.m_reportSections;
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x0006F1D0 File Offset: 0x0006D3D0
		public Microsoft.ReportingServices.OnDemandReportRendering.ReportParameterCollection Parameters
		{
			get
			{
				if (this.m_parameters == null)
				{
					if (this.m_isOldSnapshot)
					{
						if (this.m_renderReport.ReportDef.Parameters != null)
						{
							this.m_parameters = new Microsoft.ReportingServices.OnDemandReportRendering.ReportParameterCollection(this.m_renderReport.ReportDef.Parameters, this.m_renderReport.Parameters);
						}
					}
					else if (this.m_reportDef.Parameters != null)
					{
						this.m_parameters = new Microsoft.ReportingServices.OnDemandReportRendering.ReportParameterCollection(this.m_renderingContext.OdpContext, this.m_reportDef.Parameters, this.m_reportInstance != null || this.m_renderingContext.OdpContext.ContextMode == OnDemandProcessingContext.Mode.DefinitionOnly);
					}
				}
				return this.m_parameters;
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x0006F27C File Offset: 0x0006D47C
		public CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.m_customProperties == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_customProperties = new CustomPropertyCollection(this.m_renderingContext, this.m_renderReport.CustomProperties);
					}
					else
					{
						this.m_customProperties = new CustomPropertyCollection(this.Instance, this.m_renderingContext, null, this.m_reportDef, Microsoft.ReportingServices.ReportProcessing.ObjectType.Report, this.m_reportDef.Name);
					}
				}
				return this.m_customProperties;
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0006F2E7 File Offset: 0x0006D4E7
		public string DataTransform
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderReport.DataTransform;
				}
				return this.m_reportDef.DataTransform;
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x0006F308 File Offset: 0x0006D508
		public string DataSchema
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderReport.DataSchema;
				}
				return this.m_reportDef.DataSchema;
			}
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x0006F329 File Offset: 0x0006D529
		public string DataElementName
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderReport.DataElementName;
				}
				return this.m_reportDef.DataElementName;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x0006F34A File Offset: 0x0006D54A
		public Microsoft.ReportingServices.OnDemandReportRendering.Report.DataElementStyles DataElementStyle
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return (Microsoft.ReportingServices.OnDemandReportRendering.Report.DataElementStyles)this.m_renderReport.DataElementStyle;
				}
				if (!this.m_reportDef.DataElementStyleAttribute)
				{
					return Microsoft.ReportingServices.OnDemandReportRendering.Report.DataElementStyles.Element;
				}
				return Microsoft.ReportingServices.OnDemandReportRendering.Report.DataElementStyles.Attribute;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x0006F370 File Offset: 0x0006D570
		public bool HasDocumentMap
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderReport.RenderingContext.ReportSnapshot.HasDocumentMap;
				}
				return this.m_renderingContext.ReportSnapshot.HasDocumentMap;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x0006F3A0 File Offset: 0x0006D5A0
		public DocumentMap DocumentMap
		{
			get
			{
				if (!this.HasDocumentMap)
				{
					return null;
				}
				if (this.m_cachedDocumentMap != null && !this.m_cachedDocumentMap.IsClosed)
				{
					this.m_cachedDocumentMap.Reset();
				}
				else
				{
					this.m_cachedDocumentMap = null;
					if (this.m_isOldSnapshot)
					{
						Microsoft.ReportingServices.ReportProcessing.DocumentMapNode documentMap = this.m_renderReport.RenderingContext.ReportSnapshot.GetDocumentMap(this.m_renderReport.RenderingContext.ChunkManager);
						if (documentMap == null)
						{
							return null;
						}
						this.m_cachedDocumentMap = new ShimDocumentMap(documentMap);
					}
					else
					{
						OnDemandProcessingContext odpContext = this.RenderingContext.OdpContext;
						Stream stream = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.OnDemandProcessingManager.OpenExistingDocumentMapStream(odpContext.OdpMetadata, odpContext.TopLevelContext.ReportContext, odpContext.ChunkFactory);
						if (stream == null)
						{
							NullRenderer nullRenderer = new NullRenderer();
							nullRenderer.Process(this, this.RenderingContext.OdpContext, true, false);
							stream = nullRenderer.DocumentMapStream;
							if (stream == null)
							{
								this.RenderingContext.ReportSnapshot.HasDocumentMap = false;
								this.m_cachedDocumentMap = null;
							}
							else
							{
								stream.Seek(0L, SeekOrigin.Begin);
								DocumentMapReader documentMapReader = new DocumentMapReader(stream);
								this.m_cachedDocumentMap = new InternalDocumentMap(documentMapReader);
							}
						}
						else
						{
							DocumentMapReader documentMapReader2 = new DocumentMapReader(stream);
							this.m_cachedDocumentMap = new InternalDocumentMap(documentMapReader2);
						}
					}
				}
				return this.m_cachedDocumentMap;
			}
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x0006F4C9 File Offset: 0x0006D6C9
		public bool HasBookmarks
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderReport.RenderingContext.ReportSnapshot.HasBookmarks;
				}
				return this.RenderingContext.OdpContext.HasBookmarks;
			}
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x0006F4FC File Offset: 0x0006D6FC
		public ReportUrl Location
		{
			get
			{
				if (this.m_location == null)
				{
					if (this.m_isOldSnapshot)
					{
						if (this.m_renderReport.Location != null)
						{
							this.m_location = new ReportUrl(this.m_renderReport.Location);
						}
					}
					else
					{
						this.m_location = new ReportUrl(this.m_renderingContext.OdpContext.ReportContext, null);
					}
				}
				return this.m_location;
			}
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x0006F560 File Offset: 0x0006D760
		public DateTime ExecutionTime
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderReport.RenderingContext.ExecutionTime;
				}
				return this.m_renderingContext.OdpContext.ExecutionTime;
			}
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x0006F58C File Offset: 0x0006D78C
		public ReportStringProperty Language
		{
			get
			{
				if (this.m_language == null)
				{
					if (this.m_isOldSnapshot)
					{
						Microsoft.ReportingServices.ReportProcessing.ExpressionInfo language = this.m_renderReport.ReportDef.Language;
						if (language == null)
						{
							this.m_language = new ReportStringProperty(false, this.m_renderReport.ReportLanguage, this.m_renderReport.ReportLanguage);
						}
						else
						{
							this.m_language = new ReportStringProperty(language);
						}
					}
					else
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo language2 = this.m_reportDef.Language;
						if (language2 == null)
						{
							string text = Localization.DefaultReportServerSpecificCulture.ToString();
							this.m_language = new ReportStringProperty(false, text, text);
						}
						else
						{
							this.m_language = new ReportStringProperty(language2);
						}
					}
				}
				return this.m_language;
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x0006F62E File Offset: 0x0006D82E
		public bool ConsumeContainerWhitespace
		{
			get
			{
				return this.m_isOldSnapshot || this.m_reportDef.ConsumeContainerWhitespace;
			}
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0006F648 File Offset: 0x0006D848
		public string GetReportUrl(bool addReportParameters)
		{
			if (this.m_isOldSnapshot)
			{
				return new ReportUrlBuilder(this.m_renderReport.RenderingContext, null, true, addReportParameters).ToString();
			}
			CatalogItemUrlBuilder catalogItemUrlBuilder = new CatalogItemUrlBuilder(this.m_renderingContext.OdpContext.ReportContext);
			if (addReportParameters && this.Parameters != null)
			{
				NameValueCollection toNameValueCollection = this.Parameters.ToNameValueCollection;
				catalogItemUrlBuilder.AppendReportParameters(toNameValueCollection);
			}
			return catalogItemUrlBuilder.ToString();
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0006F6B0 File Offset: 0x0006D8B0
		public string GetStreamUrl(bool useSessionId, string streamName)
		{
			ICatalogItemContext catalogItemContext = (this.m_isOldSnapshot ? this.m_renderReport.RenderingContext.TopLevelReportContext : this.m_renderingContext.OdpContext.ReportContext);
			string hostSpecificItemPath = catalogItemContext.HostSpecificItemPath;
			string hostRootUri = catalogItemContext.HostRootUri;
			return catalogItemContext.RSRequestParameters.GetImageUrl(useSessionId, streamName, catalogItemContext);
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x0006F704 File Offset: 0x0006D904
		public bool GetResource(string resourcePath, out byte[] resource, out string mimeType)
		{
			resource = null;
			mimeType = null;
			if (!this.m_isOldSnapshot)
			{
				bool flag;
				return this.m_renderingContext.OdpContext.GetResource(resourcePath, out resource, out mimeType, out flag);
			}
			if (this.m_renderReport.RenderingContext.GetResourceCallback != null)
			{
				bool flag2;
				bool flag3;
				this.m_renderReport.RenderingContext.GetResourceCallback.GetResource(this.m_renderReport.RenderingContext.CurrentReportContext, resourcePath, out resource, out mimeType, out flag2, out flag3);
				return true;
			}
			return false;
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x0006F778 File Offset: 0x0006D978
		public string ShowHideToggle
		{
			get
			{
				IRSRequestParameters rsrequestParameters = this.GetRSRequestParameters();
				if (rsrequestParameters != null)
				{
					return rsrequestParameters.ShowHideToggleParamValue;
				}
				return null;
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0006F798 File Offset: 0x0006D998
		public string SortItem
		{
			get
			{
				IRSRequestParameters rsrequestParameters = this.GetRSRequestParameters();
				if (rsrequestParameters != null)
				{
					return rsrequestParameters.SortIdParamValue;
				}
				return null;
			}
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x0006F7B7 File Offset: 0x0006D9B7
		private IRSRequestParameters GetRSRequestParameters()
		{
			if (this.m_isOldSnapshot)
			{
				return this.m_renderReport.RenderingContext.TopLevelReportContext.RSRequestParameters;
			}
			return this.RenderingContext.OdpContext.TopLevelContext.ReportContext.RSRequestParameters;
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x0006F7F4 File Offset: 0x0006D9F4
		public Microsoft.ReportingServices.OnDemandReportRendering.Report.SnapshotPageSize SnapshotPageSizeInfo
		{
			get
			{
				if (!this.m_isOldSnapshot)
				{
					return Microsoft.ReportingServices.OnDemandReportRendering.Report.SnapshotPageSize.Unknown;
				}
				if (this.m_renderReport.ReportDef.MainChunkSize <= 0L || this.m_renderReport.ReportInstance.NumberOfPages <= 0)
				{
					return Microsoft.ReportingServices.OnDemandReportRendering.Report.SnapshotPageSize.Unknown;
				}
				if (1000000L < this.m_renderReport.ReportDef.MainChunkSize / (long)this.m_renderReport.ReportInstance.NumberOfPages)
				{
					return Microsoft.ReportingServices.OnDemandReportRendering.Report.SnapshotPageSize.Large;
				}
				return Microsoft.ReportingServices.OnDemandReportRendering.Report.SnapshotPageSize.Small;
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x0006F861 File Offset: 0x0006DA61
		internal PageEvaluation PageEvaluation
		{
			get
			{
				return this.m_pageEvaluation;
			}
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x0006F869 File Offset: 0x0006DA69
		internal Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext HeaderFooterRenderingContext
		{
			get
			{
				if (this.m_headerFooterRenderingContext == null)
				{
					this.m_headerFooterRenderingContext = new Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext(this.m_renderingContext, this.NeedsReportItemsOnPage);
				}
				return this.m_headerFooterRenderingContext;
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x0006F890 File Offset: 0x0006DA90
		internal IJobContext JobContext
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderReport.RenderingContext.JobContext;
				}
				return this.RenderingContext.OdpContext.JobContext;
			}
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x0006F8BB File Offset: 0x0006DABB
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Report ReportDef
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return this.m_reportDef;
			}
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x0006F8D6 File Offset: 0x0006DAD6
		internal Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext RenderingContext
		{
			get
			{
				return this.m_renderingContext;
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x0006F8DE File Offset: 0x0006DADE
		internal bool IsOldSnapshot
		{
			get
			{
				return this.m_isOldSnapshot;
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x0006F8E6 File Offset: 0x0006DAE6
		internal Microsoft.ReportingServices.ReportRendering.Report RenderReport
		{
			get
			{
				if (!this.m_isOldSnapshot)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return this.m_renderReport;
			}
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x0006F901 File Offset: 0x0006DB01
		internal bool SubreportInSubtotal
		{
			get
			{
				return this.m_subreportInSubtotal;
			}
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x0006F909 File Offset: 0x0006DB09
		IReportScopeInstance IReportScope.ReportScopeInstance
		{
			get
			{
				return this.Instance;
			}
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x0006F911 File Offset: 0x0006DB11
		IRIFReportScope IReportScope.RIFReportScope
		{
			get
			{
				return this.m_reportDef;
			}
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x0006F91C File Offset: 0x0006DB1C
		internal void UpdateSubReportContents(Microsoft.ReportingServices.OnDemandReportRendering.SubReport subReport, Microsoft.ReportingServices.ReportRendering.SubReport renderSubreport)
		{
			if (renderSubreport != null)
			{
				this.m_renderReport = renderSubreport.Report;
			}
			if (this.m_reportSections != null)
			{
				this.m_reportSections[0].UpdateSubReportContents(this.m_renderReport);
			}
			if (this.m_parameters != null)
			{
				this.m_parameters.UpdateRenderReportItem(this.m_renderReport.Parameters);
			}
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x0006F975 File Offset: 0x0006DB75
		internal void SetNewContext(Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance)
		{
			this.m_reportInstance = reportInstance;
			this.SetNewContext();
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x0006F984 File Offset: 0x0006DB84
		internal void SetNewContext()
		{
			if (this.m_reportSections != null)
			{
				this.m_reportSections.SetNewContext();
			}
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_parameters != null)
			{
				this.m_parameters.SetNewContext(this.m_reportInstance != null);
			}
			if (this.m_dataSets != null)
			{
				this.m_dataSets.SetNewContext();
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x0006F9E6 File Offset: 0x0006DBE6
		internal ReportSection FirstSection
		{
			get
			{
				return this.ReportSections[0];
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x0006F9F4 File Offset: 0x0006DBF4
		public ReportStringProperty InitialPageName
		{
			get
			{
				if (this.m_initialPageName == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_initialPageName = new ReportStringProperty();
					}
					else
					{
						this.m_initialPageName = new ReportStringProperty(this.m_reportDef.InitialPageName);
					}
				}
				return this.m_initialPageName;
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x0006FA30 File Offset: 0x0006DC30
		public Microsoft.ReportingServices.OnDemandReportRendering.ReportInstance Instance
		{
			get
			{
				if (this.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_instance = new Microsoft.ReportingServices.OnDemandReportRendering.ReportInstance(this);
					}
					else
					{
						this.m_instance = new Microsoft.ReportingServices.OnDemandReportRendering.ReportInstance(this, this.m_reportInstance);
					}
				}
				return this.m_instance;
			}
		}

		// Token: 0x04000EB8 RID: 3768
		private const int m_bytesPerPage = 1000000;

		// Token: 0x04000EB9 RID: 3769
		private bool m_isOldSnapshot;

		// Token: 0x04000EBA RID: 3770
		private bool m_subreportInSubtotal;

		// Token: 0x04000EBB RID: 3771
		private IDefinitionPath m_parentDefinitionPath;

		// Token: 0x04000EBC RID: 3772
		private Microsoft.ReportingServices.ReportIntermediateFormat.Report m_reportDef;

		// Token: 0x04000EBD RID: 3773
		private Microsoft.ReportingServices.ReportRendering.Report m_renderReport;

		// Token: 0x04000EBE RID: 3774
		private Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext m_renderingContext;

		// Token: 0x04000EBF RID: 3775
		private string m_name;

		// Token: 0x04000EC0 RID: 3776
		private string m_description;

		// Token: 0x04000EC1 RID: 3777
		private ReportSectionCollection m_reportSections;

		// Token: 0x04000EC2 RID: 3778
		private DataSetCollection m_dataSets;

		// Token: 0x04000EC3 RID: 3779
		private Microsoft.ReportingServices.OnDemandReportRendering.ReportParameterCollection m_parameters;

		// Token: 0x04000EC4 RID: 3780
		private Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance m_reportInstance;

		// Token: 0x04000EC5 RID: 3781
		private Microsoft.ReportingServices.OnDemandReportRendering.ReportInstance m_instance;

		// Token: 0x04000EC6 RID: 3782
		private CustomPropertyCollection m_customProperties;

		// Token: 0x04000EC7 RID: 3783
		private PageEvaluation m_pageEvaluation;

		// Token: 0x04000EC8 RID: 3784
		private ReportUrl m_location;

		// Token: 0x04000EC9 RID: 3785
		private ReportStringProperty m_language;

		// Token: 0x04000ECA RID: 3786
		private ReportStringProperty m_initialPageName;

		// Token: 0x04000ECB RID: 3787
		private DocumentMap m_cachedDocumentMap;

		// Token: 0x04000ECC RID: 3788
		private bool m_cachedNeedsOverallTotalPages;

		// Token: 0x04000ECD RID: 3789
		private bool m_cachedNeedsPageBreakTotalPages;

		// Token: 0x04000ECE RID: 3790
		private bool m_cachedNeedsReportItemsOnPage;

		// Token: 0x04000ECF RID: 3791
		private bool m_hasCachedHeaderFooterFlags;

		// Token: 0x04000ED0 RID: 3792
		private Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext m_headerFooterRenderingContext;

		// Token: 0x02000944 RID: 2372
		public enum DataElementStyles
		{
			// Token: 0x04004038 RID: 16440
			Attribute,
			// Token: 0x04004039 RID: 16441
			Element
		}

		// Token: 0x02000945 RID: 2373
		public enum SnapshotPageSize
		{
			// Token: 0x0400403B RID: 16443
			Unknown,
			// Token: 0x0400403C RID: 16444
			Small,
			// Token: 0x0400403D RID: 16445
			Large
		}

		// Token: 0x02000946 RID: 2374
		public enum ChunkTypes
		{
			// Token: 0x0400403F RID: 16447
			Interactivity = 6,
			// Token: 0x04004040 RID: 16448
			Pagination,
			// Token: 0x04004041 RID: 16449
			Rendering
		}
	}
}
