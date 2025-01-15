using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000307 RID: 775
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ReportItem : ReportElement
	{
		// Token: 0x06001C6B RID: 7275 RVA: 0x00070D52 File Offset: 0x0006EF52
		internal ReportItem(IReportScope reportScope, IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItemDef, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(reportScope, parentDefinitionPath, reportItemDef, renderingContext)
		{
			this.m_definitionPath = DefinitionPathConstants.GetCollectionDefinitionPath(parentDefinitionPath, indexIntoParentCollectionDef);
			this.m_reportItemDef.ROMScopeInstance = this.ReportScope.ReportScopeInstance;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x00070D83 File Offset: 0x0006EF83
		internal ReportItem(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, bool inSubtotal, Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(parentDefinitionPath, renderReportItem, renderingContext)
		{
			this.m_definitionPath = DefinitionPathConstants.GetCollectionDefinitionPath(parentDefinitionPath, indexIntoParentCollectionDef);
			this.m_inSubtotal = inSubtotal;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x00070DA4 File Offset: 0x0006EFA4
		internal ReportItem(IDefinitionPath parentDefinitionPath, bool inSubtotal, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(parentDefinitionPath, renderingContext)
		{
			this.m_definitionPath = DefinitionPathConstants.GetCollectionDefinitionPath(parentDefinitionPath, 0);
			this.m_inSubtotal = inSubtotal;
			this.m_isListContentsRectangle = true;
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x00070DC9 File Offset: 0x0006EFC9
		public override string DefinitionPath
		{
			get
			{
				return this.m_definitionPath;
			}
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x00070DD1 File Offset: 0x0006EFD1
		public virtual string Name
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderReportItem.Name;
				}
				return base.ReportItemDef.Name;
			}
		}

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x00070DF2 File Offset: 0x0006EFF2
		public override string ID
		{
			get
			{
				if (!this.m_isOldSnapshot)
				{
					return base.ReportItemDef.RenderingModelID;
				}
				if (this.m_inSubtotal || this.m_isListContentsRectangle)
				{
					return this.DefinitionPath;
				}
				return this.m_renderReportItem.ID;
			}
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x00070E2A File Offset: 0x0006F02A
		public override Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_isOldSnapshot && this.m_isListContentsRectangle)
				{
					return new Microsoft.ReportingServices.OnDemandReportRendering.Style(this, this.m_renderingContext);
				}
				return base.Style;
			}
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x00070E4F File Offset: 0x0006F04F
		public virtual int LinkToChild
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x00070E54 File Offset: 0x0006F054
		public virtual ReportSize Height
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					if (this.m_cachedHeight == null)
					{
						this.m_cachedHeight = new ReportSize(this.RenderReportItem.Height);
					}
					return this.m_cachedHeight;
				}
				if (base.ReportItemDef.HeightForRendering == null)
				{
					base.ReportItemDef.HeightForRendering = new ReportSize(base.ReportItemDef.Height, base.ReportItemDef.HeightValue);
				}
				return base.ReportItemDef.HeightForRendering;
			}
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x00070ECC File Offset: 0x0006F0CC
		public virtual ReportSize Width
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					if (this.m_cachedWidth == null)
					{
						this.m_cachedWidth = new ReportSize(this.RenderReportItem.Width);
					}
					return this.m_cachedWidth;
				}
				if (base.ReportItemDef.WidthForRendering == null)
				{
					base.ReportItemDef.WidthForRendering = new ReportSize(base.ReportItemDef.Width, base.ReportItemDef.WidthValue);
				}
				return base.ReportItemDef.WidthForRendering;
			}
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x00070F44 File Offset: 0x0006F144
		public virtual ReportSize Top
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					if (this.m_cachedTop == null)
					{
						this.m_cachedTop = new ReportSize(this.RenderReportItem.Top);
					}
					return this.m_cachedTop;
				}
				if (base.ReportItemDef.TopForRendering == null)
				{
					base.ReportItemDef.TopForRendering = new ReportSize(base.ReportItemDef.Top, base.ReportItemDef.TopValue);
				}
				return base.ReportItemDef.TopForRendering;
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x00070FBC File Offset: 0x0006F1BC
		public virtual ReportSize Left
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					if (this.m_cachedLeft == null)
					{
						this.m_cachedLeft = new ReportSize(this.RenderReportItem.Left);
					}
					return this.m_cachedLeft;
				}
				if (base.ReportItemDef.LeftForRendering == null)
				{
					base.ReportItemDef.LeftForRendering = new ReportSize(base.ReportItemDef.Left, base.ReportItemDef.LeftValue);
				}
				return base.ReportItemDef.LeftForRendering;
			}
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x00071034 File Offset: 0x0006F234
		public int ZIndex
		{
			get
			{
				if (!this.m_isOldSnapshot)
				{
					return base.ReportItemDef.ZIndex;
				}
				if (this.m_isListContentsRectangle)
				{
					return 0;
				}
				return this.RenderReportItem.ZIndex;
			}
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x00071060 File Offset: 0x0006F260
		public ReportStringProperty ToolTip
		{
			get
			{
				if (this.m_toolTip == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_toolTip = new ReportStringProperty(this.RenderReportItem.ReportItemDef.ToolTip);
					}
					else
					{
						this.m_toolTip = new ReportStringProperty(this.m_reportItemDef.ToolTip);
					}
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x000710B8 File Offset: 0x0006F2B8
		public ReportStringProperty Bookmark
		{
			get
			{
				if (this.m_bookmark == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_bookmark = new ReportStringProperty(this.RenderReportItem.ReportItemDef.Bookmark);
					}
					else
					{
						this.m_bookmark = new ReportStringProperty(this.m_reportItemDef.Bookmark);
					}
				}
				return this.m_bookmark;
			}
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x00071110 File Offset: 0x0006F310
		public ReportStringProperty DocumentMapLabel
		{
			get
			{
				if (this.m_documentMapLabel == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_documentMapLabel = new ReportStringProperty(this.RenderReportItem.ReportItemDef.Label);
					}
					else
					{
						this.m_documentMapLabel = new ReportStringProperty(this.m_reportItemDef.DocumentMapLabel);
					}
				}
				return this.m_documentMapLabel;
			}
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x00071166 File Offset: 0x0006F366
		public string RepeatWith
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.RenderReportItem.ReportItemDef.RepeatWith;
				}
				return base.ReportItemDef.RepeatWith;
			}
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x0007118C File Offset: 0x0006F38C
		public bool RepeatedSibling
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.RenderReportItem.RepeatedSibling;
				}
				return base.ReportItemDef.RepeatedSibling;
			}
		}

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x000711AD File Offset: 0x0006F3AD
		public CustomPropertyCollection CustomProperties
		{
			get
			{
				this.PrepareCustomProperties();
				return this.m_customProperties;
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x000711BB File Offset: 0x0006F3BB
		public string DataElementName
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.RenderReportItem.DataElementName;
				}
				return base.ReportItemDef.DataElementName;
			}
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x000711DC File Offset: 0x0006F3DC
		public virtual DataElementOutputTypes DataElementOutput
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return (DataElementOutputTypes)this.RenderReportItem.DataElementOutput;
				}
				return base.ReportItemDef.DataElementOutput;
			}
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x00071200 File Offset: 0x0006F400
		public virtual Microsoft.ReportingServices.OnDemandReportRendering.Visibility Visibility
		{
			get
			{
				if (this.m_visibility == null)
				{
					if (this.m_isOldSnapshot && this.RenderReportItem.ReportItemDef.Visibility == null)
					{
						return null;
					}
					if (!this.m_isOldSnapshot && this.m_reportItemDef.Visibility == null)
					{
						return null;
					}
					this.m_visibility = new ReportItemVisibility(this);
				}
				return this.m_visibility;
			}
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x0007125A File Offset: 0x0006F45A
		internal bool InSubtotal
		{
			get
			{
				return this.m_inSubtotal;
			}
		}

		// Token: 0x06001C82 RID: 7298
		internal abstract ReportItemInstance GetOrCreateInstance();

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x00071262 File Offset: 0x0006F462
		internal override ReportElementInstance ReportElementInstance
		{
			get
			{
				return this.Instance;
			}
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x0007126A File Offset: 0x0006F46A
		public new ReportItemInstance Instance
		{
			get
			{
				if (this.m_renderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				ReportItemInstance orCreateInstance = this.GetOrCreateInstance();
				this.CriEvaluateInstance();
				return orCreateInstance;
			}
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x00071287 File Offset: 0x0006F487
		internal override string InstanceUniqueName
		{
			get
			{
				if (this.Instance != null)
				{
					return this.Instance.UniqueName;
				}
				return null;
			}
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x000712A0 File Offset: 0x0006F4A0
		internal void SetCachedWidth(double sizeDelta)
		{
			if (this.m_isOldSnapshot)
			{
				double num = this.RenderReportItem.Width.ToMillimeters() + sizeDelta;
				string text = num.ToString("f5", CultureInfo.InvariantCulture) + "mm";
				this.m_cachedWidth = new ReportSize(text, num);
			}
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x000712F4 File Offset: 0x0006F4F4
		internal void SetCachedHeight(double sizeDelta)
		{
			if (this.m_isOldSnapshot)
			{
				double num = this.RenderReportItem.Height.ToMillimeters() + sizeDelta;
				string text = num.ToString("f5", CultureInfo.InvariantCulture) + "mm";
				this.m_cachedHeight = new ReportSize(text, num);
			}
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x00071345 File Offset: 0x0006F545
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			base.SetNewContext();
			this.m_criGeneratedInstanceEvaluated = false;
			if (this.m_reportItemDef != null)
			{
				this.m_reportItemDef.ResetVisibilityComputationCache();
			}
			this.m_customPropertiesReady = false;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x00071381 File Offset: 0x0006F581
		internal override void SetNewContextChildren()
		{
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x00071383 File Offset: 0x0006F583
		internal void CriEvaluateInstance()
		{
			if (base.CriOwner != null && base.CriGenerationPhase == ReportElement.CriGenerationPhases.None && !this.m_criGeneratedInstanceEvaluated)
			{
				this.m_criGeneratedInstanceEvaluated = true;
				base.CriOwner.EvaluateGeneratedReportItemInstance();
			}
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x000713B0 File Offset: 0x0006F5B0
		internal virtual void UpdateRenderReportItem(Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem)
		{
			if (!this.m_isOldSnapshot)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			this.SetNewContext();
			if (renderReportItem != null)
			{
				this.m_renderReportItem = renderReportItem;
				if (this.m_customProperties != null)
				{
					this.m_customProperties.UpdateCustomProperties(renderReportItem.CustomProperties);
				}
				if (this.m_style != null && !this.m_isListContentsRectangle)
				{
					this.m_style.UpdateStyleCache(renderReportItem);
				}
			}
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x00071418 File Offset: 0x0006F618
		public CustomProperty CreateCustomProperty()
		{
			if (base.CriGenerationPhase != ReportElement.CriGenerationPhases.Definition)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			this.PrepareCustomProperties();
			Microsoft.ReportingServices.ReportIntermediateFormat.DataValue dataValue = new Microsoft.ReportingServices.ReportIntermediateFormat.DataValue();
			dataValue.Name = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			dataValue.Value = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			if (this.m_reportItemDef.CustomProperties == null)
			{
				this.m_reportItemDef.CustomProperties = new Microsoft.ReportingServices.ReportIntermediateFormat.DataValueList();
			}
			this.m_reportItemDef.CustomProperties.Add(dataValue);
			return this.CustomProperties.Add(base.RenderingContext, dataValue.Name, dataValue.Value);
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x000714A7 File Offset: 0x0006F6A7
		internal virtual Microsoft.ReportingServices.OnDemandReportRendering.ReportItem ExposeAs(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			return this;
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x000714AC File Offset: 0x0006F6AC
		internal virtual void ConstructReportItemDefinition()
		{
			Global.Tracer.Assert(false, "ConstructReportElementDefinition is not implemented on this type of report item: " + this.m_reportItemDef.ObjectType.ToString());
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x000714E8 File Offset: 0x0006F6E8
		internal virtual void CompleteCriGeneratedInstanceEvaluation()
		{
			Global.Tracer.Assert(false, "CompleteCriGeneratedInstanceEvaluation is not implemented on this type of report item: " + this.m_reportItemDef.ObjectType.ToString());
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00071524 File Offset: 0x0006F724
		internal void ConstructReportItemDefinitionImpl()
		{
			base.ConstructReportElementDefinitionImpl();
			ReportItemInstance instance = this.Instance;
			Global.Tracer.Assert(instance != null, "(instance != null)");
			if (instance.ToolTip != null)
			{
				base.ReportItemDef.ToolTip = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(instance.ToolTip);
			}
			else
			{
				base.ReportItemDef.ToolTip = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			}
			this.m_toolTip = null;
			if (instance.Bookmark != null)
			{
				base.ReportItemDef.Bookmark = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(instance.Bookmark);
			}
			else
			{
				base.ReportItemDef.Bookmark = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			}
			this.m_bookmark = null;
			if (instance.DocumentMapLabel != null)
			{
				base.ReportItemDef.DocumentMapLabel = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(instance.DocumentMapLabel);
			}
			else
			{
				base.ReportItemDef.DocumentMapLabel = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			}
			this.m_documentMapLabel = null;
			if (this.m_customProperties != null)
			{
				if (this.m_customProperties.Count == 0)
				{
					this.m_reportItemDef.CustomProperties = null;
					this.m_customProperties = null;
					return;
				}
				this.m_customProperties.ConstructCustomPropertyDefinitions(this.m_reportItemDef.CustomProperties);
			}
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00071638 File Offset: 0x0006F838
		private void PrepareCustomProperties()
		{
			if (this.m_isOldSnapshot)
			{
				if (this.m_customProperties == null && this.RenderReportItem.CustomProperties != null)
				{
					this.m_customProperties = new CustomPropertyCollection(this.m_renderingContext, this.RenderReportItem.CustomProperties);
					return;
				}
			}
			else
			{
				if (this.m_customProperties == null)
				{
					this.m_customProperties = new CustomPropertyCollection(this.ReportScope.ReportScopeInstance, this.m_renderingContext, this, this.m_reportItemDef, this.m_reportItemDef.ObjectType, this.m_reportItemDef.Name);
				}
				else if (!this.m_customPropertiesReady)
				{
					this.m_customProperties.UpdateCustomProperties(this.ReportScope.ReportScopeInstance, this.m_reportItemDef, this.m_renderingContext.OdpContext, this.m_reportItemDef.ObjectType, this.m_reportItemDef.Name);
					this.CriEvaluateInstance();
				}
				this.m_customPropertiesReady = true;
			}
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x0007171C File Offset: 0x0006F91C
		internal static int StringToInt(string intAsString)
		{
			int num = -1;
			if (int.TryParse(intAsString, NumberStyles.None, CultureInfo.InvariantCulture, out num))
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x00071740 File Offset: 0x0006F940
		internal static Microsoft.ReportingServices.OnDemandReportRendering.ReportItem CreateItem(IReportScope reportScope, IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItemDef, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			Microsoft.ReportingServices.OnDemandReportRendering.ReportItem reportItem = null;
			ObjectType objectType = reportItemDef.ObjectType;
			if (objectType <= ObjectType.Chart)
			{
				switch (objectType)
				{
				case ObjectType.Line:
					reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.Line(reportScope, parentDefinitionPath, indexIntoParentCollectionDef, (Microsoft.ReportingServices.ReportIntermediateFormat.Line)reportItemDef, renderingContext);
					break;
				case ObjectType.Rectangle:
					reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.Rectangle(reportScope, parentDefinitionPath, indexIntoParentCollectionDef, (Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)reportItemDef, renderingContext);
					break;
				case ObjectType.Checkbox:
					break;
				case ObjectType.Textbox:
					reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.TextBox(reportScope, parentDefinitionPath, indexIntoParentCollectionDef, (Microsoft.ReportingServices.ReportIntermediateFormat.TextBox)reportItemDef, renderingContext);
					break;
				case ObjectType.Image:
					reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.Image(reportScope, parentDefinitionPath, indexIntoParentCollectionDef, (Microsoft.ReportingServices.ReportIntermediateFormat.Image)reportItemDef, renderingContext);
					break;
				case ObjectType.Subreport:
					reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.SubReport(reportScope, parentDefinitionPath, indexIntoParentCollectionDef, (Microsoft.ReportingServices.ReportIntermediateFormat.SubReport)reportItemDef, renderingContext);
					break;
				default:
					if (objectType != ObjectType.GaugePanel)
					{
						if (objectType == ObjectType.Chart)
						{
							reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.Chart(parentDefinitionPath, indexIntoParentCollectionDef, (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)reportItemDef, renderingContext);
						}
					}
					else
					{
						reportItem = new GaugePanel(parentDefinitionPath, indexIntoParentCollectionDef, (GaugePanel)reportItemDef, renderingContext);
					}
					break;
				}
			}
			else if (objectType != ObjectType.CustomReportItem)
			{
				if (objectType != ObjectType.Tablix)
				{
					if (objectType == ObjectType.Map)
					{
						reportItem = new Map(reportScope, parentDefinitionPath, indexIntoParentCollectionDef, (Map)reportItemDef, renderingContext);
					}
				}
				else
				{
					reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.Tablix(parentDefinitionPath, indexIntoParentCollectionDef, (Microsoft.ReportingServices.ReportIntermediateFormat.Tablix)reportItemDef, renderingContext);
				}
			}
			else
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.CustomReportItem customReportItem = (Microsoft.ReportingServices.ReportIntermediateFormat.CustomReportItem)reportItemDef;
				reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem(reportScope, parentDefinitionPath, indexIntoParentCollectionDef, customReportItem, renderingContext);
				if (!((Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem)reportItem).Initialize(renderingContext))
				{
					reportItem = Microsoft.ReportingServices.OnDemandReportRendering.ReportItem.CreateItem(reportScope, parentDefinitionPath, customReportItem.AltReportItemIndexInParentCollectionDef, customReportItem.AltReportItem, renderingContext);
					reportItem.ReportItemDef.RepeatedSibling = customReportItem.RepeatedSibling;
					reportItem.ReportItemDef.RepeatWith = customReportItem.RepeatWith;
					Microsoft.ReportingServices.OnDemandReportRendering.ReportItem.ProcessAlternateCustomReportItem(customReportItem, reportItem, renderingContext);
				}
			}
			return reportItem;
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x000718CC File Offset: 0x0006FACC
		internal static Microsoft.ReportingServices.OnDemandReportRendering.ReportItem CreateShim(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, bool inSubtotal, Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			Microsoft.ReportingServices.OnDemandReportRendering.ReportItem reportItem = null;
			if (renderReportItem is Microsoft.ReportingServices.ReportRendering.TextBox)
			{
				reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.TextBox(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, (Microsoft.ReportingServices.ReportRendering.TextBox)renderReportItem, renderingContext);
			}
			else if (renderReportItem is Microsoft.ReportingServices.ReportRendering.Rectangle)
			{
				reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.Rectangle(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, (Microsoft.ReportingServices.ReportRendering.Rectangle)renderReportItem, renderingContext);
			}
			else if (renderReportItem is Microsoft.ReportingServices.ReportRendering.Image)
			{
				reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.Image(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, (Microsoft.ReportingServices.ReportRendering.Image)renderReportItem, renderingContext);
			}
			else if (renderReportItem is Microsoft.ReportingServices.ReportRendering.List)
			{
				reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.Tablix(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, (Microsoft.ReportingServices.ReportRendering.List)renderReportItem, renderingContext);
			}
			else if (renderReportItem is Microsoft.ReportingServices.ReportRendering.Table)
			{
				reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.Tablix(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, (Microsoft.ReportingServices.ReportRendering.Table)renderReportItem, renderingContext);
			}
			else if (renderReportItem is Microsoft.ReportingServices.ReportRendering.Matrix)
			{
				reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.Tablix(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, (Microsoft.ReportingServices.ReportRendering.Matrix)renderReportItem, renderingContext);
			}
			else if (renderReportItem is Microsoft.ReportingServices.ReportRendering.Chart)
			{
				reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.Chart(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, (Microsoft.ReportingServices.ReportRendering.Chart)renderReportItem, renderingContext);
			}
			else if (renderReportItem is Microsoft.ReportingServices.ReportRendering.CustomReportItem)
			{
				reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, (Microsoft.ReportingServices.ReportRendering.CustomReportItem)renderReportItem, renderingContext);
			}
			else if (renderReportItem is Microsoft.ReportingServices.ReportRendering.SubReport)
			{
				reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.SubReport(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, (Microsoft.ReportingServices.ReportRendering.SubReport)renderReportItem, renderingContext);
			}
			else if (renderReportItem is Microsoft.ReportingServices.ReportRendering.Line)
			{
				reportItem = new Microsoft.ReportingServices.OnDemandReportRendering.Line(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, (Microsoft.ReportingServices.ReportRendering.Line)renderReportItem, renderingContext);
			}
			return reportItem;
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x000719F8 File Offset: 0x0006FBF8
		private static void ProcessAlternateCustomReportItem(Microsoft.ReportingServices.ReportIntermediateFormat.CustomReportItem criDef, Microsoft.ReportingServices.OnDemandReportRendering.ReportItem reportItem, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			if (!criDef.ExplicitlyDefinedAltReportItem)
			{
				Global.Tracer.Assert(renderingContext.OdpContext.ExtFactory != null, "ExtFactory != null.");
				string text;
				if (!renderingContext.OdpContext.ExtFactory.IsRegisteredCustomReportItemExtension(criDef.Type))
				{
					renderingContext.OdpContext.TopLevelContext.ErrorContext.Register(ProcessingErrorCode.rsCRIControlNotInstalled, Severity.Warning, ObjectType.CustomReportItem, criDef.Name, criDef.Type, Array.Empty<string>());
					text = "The '{1}.{0}' extension is not present in the configuration file: The element '{2}' will render the AltReportItem, which is not defined. Therefore, it shows an empty space.";
				}
				else
				{
					renderingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIControlFailedToLoad, Severity.Warning, ObjectType.CustomReportItem, criDef.Name, criDef.Type, Array.Empty<string>());
					text = "The '{1}.{0}' extension failed to load: The element '{2}' will render the AltReportItem, which is not defined. Therefore, it shows an empty space.";
				}
				Global.Tracer.Trace(TraceLevel.Verbose, text, new object[] { criDef.Name, criDef.Type, reportItem.Name });
			}
		}

		// Token: 0x04000EF0 RID: 3824
		protected bool m_isListContentsRectangle;

		// Token: 0x04000EF1 RID: 3825
		protected bool m_inSubtotal;

		// Token: 0x04000EF2 RID: 3826
		protected string m_definitionPath;

		// Token: 0x04000EF3 RID: 3827
		protected ReportStringProperty m_toolTip;

		// Token: 0x04000EF4 RID: 3828
		protected ReportStringProperty m_bookmark;

		// Token: 0x04000EF5 RID: 3829
		protected ReportStringProperty m_documentMapLabel;

		// Token: 0x04000EF6 RID: 3830
		protected ReportItemInstance m_instance;

		// Token: 0x04000EF7 RID: 3831
		private bool m_criGeneratedInstanceEvaluated;

		// Token: 0x04000EF8 RID: 3832
		protected ReportBoolProperty m_startHidden;

		// Token: 0x04000EF9 RID: 3833
		private Microsoft.ReportingServices.OnDemandReportRendering.Visibility m_visibility;

		// Token: 0x04000EFA RID: 3834
		private CustomPropertyCollection m_customProperties;

		// Token: 0x04000EFB RID: 3835
		private bool m_customPropertiesReady;

		// Token: 0x04000EFC RID: 3836
		private ReportSize m_cachedTop;

		// Token: 0x04000EFD RID: 3837
		private ReportSize m_cachedLeft;

		// Token: 0x04000EFE RID: 3838
		protected ReportSize m_cachedHeight;

		// Token: 0x04000EFF RID: 3839
		protected ReportSize m_cachedWidth;
	}
}
