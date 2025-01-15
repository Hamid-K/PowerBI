using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004EA RID: 1258
	[Serializable]
	internal class Page : IDOwner, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IStyleContainer, IRIFReportScope, IInstancePath, IAggregateHolder
	{
		// Token: 0x06003F83 RID: 16259 RVA: 0x0010D368 File Offset: 0x0010B568
		internal Page()
		{
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x0010D3F4 File Offset: 0x0010B5F4
		internal Page(int id)
			: base(id)
		{
			this.m_pageAggregates = new List<DataAggregateInfo>();
		}

		// Token: 0x17001AD9 RID: 6873
		// (get) Token: 0x06003F85 RID: 16261 RVA: 0x0010D48C File Offset: 0x0010B68C
		// (set) Token: 0x06003F86 RID: 16262 RVA: 0x0010D494 File Offset: 0x0010B694
		internal Microsoft.ReportingServices.ReportIntermediateFormat.PageSection PageHeader
		{
			get
			{
				return this.m_pageHeader;
			}
			set
			{
				this.m_pageHeader = value;
			}
		}

		// Token: 0x17001ADA RID: 6874
		// (get) Token: 0x06003F87 RID: 16263 RVA: 0x0010D49D File Offset: 0x0010B69D
		internal bool UpgradedSnapshotPageHeaderEvaluation
		{
			get
			{
				return this.m_pageHeader != null && this.m_pageHeader.UpgradedSnapshotPostProcessEvaluate;
			}
		}

		// Token: 0x17001ADB RID: 6875
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x0010D4B4 File Offset: 0x0010B6B4
		// (set) Token: 0x06003F89 RID: 16265 RVA: 0x0010D4BC File Offset: 0x0010B6BC
		internal Microsoft.ReportingServices.ReportIntermediateFormat.PageSection PageFooter
		{
			get
			{
				return this.m_pageFooter;
			}
			set
			{
				this.m_pageFooter = value;
			}
		}

		// Token: 0x17001ADC RID: 6876
		// (get) Token: 0x06003F8A RID: 16266 RVA: 0x0010D4C5 File Offset: 0x0010B6C5
		internal bool UpgradedSnapshotPageFooterEvaluation
		{
			get
			{
				return this.m_pageFooter != null && this.m_pageFooter.UpgradedSnapshotPostProcessEvaluate;
			}
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x0010D4DC File Offset: 0x0010B6DC
		internal double GetPageSectionWidth(double width)
		{
			if (this.m_columns > 1)
			{
				width += (double)(this.m_columns - 1) * (width + this.m_columnSpacingValue);
			}
			return width;
		}

		// Token: 0x17001ADD RID: 6877
		// (get) Token: 0x06003F8C RID: 16268 RVA: 0x0010D4FE File Offset: 0x0010B6FE
		// (set) Token: 0x06003F8D RID: 16269 RVA: 0x0010D506 File Offset: 0x0010B706
		internal string PageHeight
		{
			get
			{
				return this.m_pageHeight;
			}
			set
			{
				this.m_pageHeight = value;
			}
		}

		// Token: 0x17001ADE RID: 6878
		// (get) Token: 0x06003F8E RID: 16270 RVA: 0x0010D50F File Offset: 0x0010B70F
		// (set) Token: 0x06003F8F RID: 16271 RVA: 0x0010D517 File Offset: 0x0010B717
		internal double PageHeightValue
		{
			get
			{
				return this.m_pageHeightValue;
			}
			set
			{
				this.m_pageHeightValue = value;
			}
		}

		// Token: 0x17001ADF RID: 6879
		// (get) Token: 0x06003F90 RID: 16272 RVA: 0x0010D520 File Offset: 0x0010B720
		// (set) Token: 0x06003F91 RID: 16273 RVA: 0x0010D528 File Offset: 0x0010B728
		internal string PageWidth
		{
			get
			{
				return this.m_pageWidth;
			}
			set
			{
				this.m_pageWidth = value;
			}
		}

		// Token: 0x17001AE0 RID: 6880
		// (get) Token: 0x06003F92 RID: 16274 RVA: 0x0010D531 File Offset: 0x0010B731
		// (set) Token: 0x06003F93 RID: 16275 RVA: 0x0010D539 File Offset: 0x0010B739
		internal double PageWidthValue
		{
			get
			{
				return this.m_pageWidthValue;
			}
			set
			{
				this.m_pageWidthValue = value;
			}
		}

		// Token: 0x17001AE1 RID: 6881
		// (get) Token: 0x06003F94 RID: 16276 RVA: 0x0010D542 File Offset: 0x0010B742
		// (set) Token: 0x06003F95 RID: 16277 RVA: 0x0010D54A File Offset: 0x0010B74A
		internal string LeftMargin
		{
			get
			{
				return this.m_leftMargin;
			}
			set
			{
				this.m_leftMargin = value;
			}
		}

		// Token: 0x17001AE2 RID: 6882
		// (get) Token: 0x06003F96 RID: 16278 RVA: 0x0010D553 File Offset: 0x0010B753
		// (set) Token: 0x06003F97 RID: 16279 RVA: 0x0010D55B File Offset: 0x0010B75B
		internal double LeftMarginValue
		{
			get
			{
				return this.m_leftMarginValue;
			}
			set
			{
				this.m_leftMarginValue = value;
			}
		}

		// Token: 0x17001AE3 RID: 6883
		// (get) Token: 0x06003F98 RID: 16280 RVA: 0x0010D564 File Offset: 0x0010B764
		// (set) Token: 0x06003F99 RID: 16281 RVA: 0x0010D56C File Offset: 0x0010B76C
		internal string RightMargin
		{
			get
			{
				return this.m_rightMargin;
			}
			set
			{
				this.m_rightMargin = value;
			}
		}

		// Token: 0x17001AE4 RID: 6884
		// (get) Token: 0x06003F9A RID: 16282 RVA: 0x0010D575 File Offset: 0x0010B775
		// (set) Token: 0x06003F9B RID: 16283 RVA: 0x0010D57D File Offset: 0x0010B77D
		internal double RightMarginValue
		{
			get
			{
				return this.m_rightMarginValue;
			}
			set
			{
				this.m_rightMarginValue = value;
			}
		}

		// Token: 0x17001AE5 RID: 6885
		// (get) Token: 0x06003F9C RID: 16284 RVA: 0x0010D586 File Offset: 0x0010B786
		// (set) Token: 0x06003F9D RID: 16285 RVA: 0x0010D58E File Offset: 0x0010B78E
		internal string TopMargin
		{
			get
			{
				return this.m_topMargin;
			}
			set
			{
				this.m_topMargin = value;
			}
		}

		// Token: 0x17001AE6 RID: 6886
		// (get) Token: 0x06003F9E RID: 16286 RVA: 0x0010D597 File Offset: 0x0010B797
		// (set) Token: 0x06003F9F RID: 16287 RVA: 0x0010D59F File Offset: 0x0010B79F
		internal double TopMarginValue
		{
			get
			{
				return this.m_topMarginValue;
			}
			set
			{
				this.m_topMarginValue = value;
			}
		}

		// Token: 0x17001AE7 RID: 6887
		// (get) Token: 0x06003FA0 RID: 16288 RVA: 0x0010D5A8 File Offset: 0x0010B7A8
		// (set) Token: 0x06003FA1 RID: 16289 RVA: 0x0010D5B0 File Offset: 0x0010B7B0
		internal string BottomMargin
		{
			get
			{
				return this.m_bottomMargin;
			}
			set
			{
				this.m_bottomMargin = value;
			}
		}

		// Token: 0x17001AE8 RID: 6888
		// (get) Token: 0x06003FA2 RID: 16290 RVA: 0x0010D5B9 File Offset: 0x0010B7B9
		// (set) Token: 0x06003FA3 RID: 16291 RVA: 0x0010D5C1 File Offset: 0x0010B7C1
		internal double BottomMarginValue
		{
			get
			{
				return this.m_bottomMarginValue;
			}
			set
			{
				this.m_bottomMarginValue = value;
			}
		}

		// Token: 0x17001AE9 RID: 6889
		// (get) Token: 0x06003FA4 RID: 16292 RVA: 0x0010D5CA File Offset: 0x0010B7CA
		// (set) Token: 0x06003FA5 RID: 16293 RVA: 0x0010D5D2 File Offset: 0x0010B7D2
		internal ReportSize PageHeightForRendering
		{
			get
			{
				return this.m_pageHeightForRendering;
			}
			set
			{
				this.m_pageHeightForRendering = value;
			}
		}

		// Token: 0x17001AEA RID: 6890
		// (get) Token: 0x06003FA6 RID: 16294 RVA: 0x0010D5DB File Offset: 0x0010B7DB
		// (set) Token: 0x06003FA7 RID: 16295 RVA: 0x0010D5E3 File Offset: 0x0010B7E3
		internal ReportSize PageWidthForRendering
		{
			get
			{
				return this.m_pageWidthForRendering;
			}
			set
			{
				this.m_pageWidthForRendering = value;
			}
		}

		// Token: 0x17001AEB RID: 6891
		// (get) Token: 0x06003FA8 RID: 16296 RVA: 0x0010D5EC File Offset: 0x0010B7EC
		// (set) Token: 0x06003FA9 RID: 16297 RVA: 0x0010D5F4 File Offset: 0x0010B7F4
		internal ReportSize LeftMarginForRendering
		{
			get
			{
				return this.m_leftMarginForRendering;
			}
			set
			{
				this.m_leftMarginForRendering = value;
			}
		}

		// Token: 0x17001AEC RID: 6892
		// (get) Token: 0x06003FAA RID: 16298 RVA: 0x0010D5FD File Offset: 0x0010B7FD
		// (set) Token: 0x06003FAB RID: 16299 RVA: 0x0010D605 File Offset: 0x0010B805
		internal ReportSize RightMarginForRendering
		{
			get
			{
				return this.m_rightMarginForRendering;
			}
			set
			{
				this.m_rightMarginForRendering = value;
			}
		}

		// Token: 0x17001AED RID: 6893
		// (get) Token: 0x06003FAC RID: 16300 RVA: 0x0010D60E File Offset: 0x0010B80E
		// (set) Token: 0x06003FAD RID: 16301 RVA: 0x0010D616 File Offset: 0x0010B816
		internal ReportSize TopMarginForRendering
		{
			get
			{
				return this.m_topMarginForRendering;
			}
			set
			{
				this.m_topMarginForRendering = value;
			}
		}

		// Token: 0x17001AEE RID: 6894
		// (get) Token: 0x06003FAE RID: 16302 RVA: 0x0010D61F File Offset: 0x0010B81F
		// (set) Token: 0x06003FAF RID: 16303 RVA: 0x0010D627 File Offset: 0x0010B827
		internal ReportSize BottomMarginForRendering
		{
			get
			{
				return this.m_bottomMarginForRendering;
			}
			set
			{
				this.m_bottomMarginForRendering = value;
			}
		}

		// Token: 0x17001AEF RID: 6895
		// (get) Token: 0x06003FB0 RID: 16304 RVA: 0x0010D630 File Offset: 0x0010B830
		// (set) Token: 0x06003FB1 RID: 16305 RVA: 0x0010D638 File Offset: 0x0010B838
		internal string InteractiveHeight
		{
			get
			{
				return this.m_interactiveHeight;
			}
			set
			{
				this.m_interactiveHeight = value;
			}
		}

		// Token: 0x17001AF0 RID: 6896
		// (get) Token: 0x06003FB2 RID: 16306 RVA: 0x0010D641 File Offset: 0x0010B841
		// (set) Token: 0x06003FB3 RID: 16307 RVA: 0x0010D661 File Offset: 0x0010B861
		internal double InteractiveHeightValue
		{
			get
			{
				if (this.m_interactiveHeightValue >= 0.0)
				{
					return this.m_interactiveHeightValue;
				}
				return this.m_pageHeightValue;
			}
			set
			{
				this.m_interactiveHeightValue = value;
			}
		}

		// Token: 0x17001AF1 RID: 6897
		// (get) Token: 0x06003FB4 RID: 16308 RVA: 0x0010D66A File Offset: 0x0010B86A
		// (set) Token: 0x06003FB5 RID: 16309 RVA: 0x0010D672 File Offset: 0x0010B872
		internal string InteractiveWidth
		{
			get
			{
				return this.m_interactiveWidth;
			}
			set
			{
				this.m_interactiveWidth = value;
			}
		}

		// Token: 0x17001AF2 RID: 6898
		// (get) Token: 0x06003FB6 RID: 16310 RVA: 0x0010D67B File Offset: 0x0010B87B
		// (set) Token: 0x06003FB7 RID: 16311 RVA: 0x0010D69B File Offset: 0x0010B89B
		internal double InteractiveWidthValue
		{
			get
			{
				if (this.m_interactiveWidthValue >= 0.0)
				{
					return this.m_interactiveWidthValue;
				}
				return this.m_pageWidthValue;
			}
			set
			{
				this.m_interactiveWidthValue = value;
			}
		}

		// Token: 0x17001AF3 RID: 6899
		// (get) Token: 0x06003FB8 RID: 16312 RVA: 0x0010D6A4 File Offset: 0x0010B8A4
		// (set) Token: 0x06003FB9 RID: 16313 RVA: 0x0010D6AC File Offset: 0x0010B8AC
		internal int Columns
		{
			get
			{
				return this.m_columns;
			}
			set
			{
				this.m_columns = value;
			}
		}

		// Token: 0x17001AF4 RID: 6900
		// (get) Token: 0x06003FBA RID: 16314 RVA: 0x0010D6B5 File Offset: 0x0010B8B5
		// (set) Token: 0x06003FBB RID: 16315 RVA: 0x0010D6BD File Offset: 0x0010B8BD
		internal string ColumnSpacing
		{
			get
			{
				return this.m_columnSpacing;
			}
			set
			{
				this.m_columnSpacing = value;
			}
		}

		// Token: 0x17001AF5 RID: 6901
		// (get) Token: 0x06003FBC RID: 16316 RVA: 0x0010D6C6 File Offset: 0x0010B8C6
		// (set) Token: 0x06003FBD RID: 16317 RVA: 0x0010D6CE File Offset: 0x0010B8CE
		internal double ColumnSpacingValue
		{
			get
			{
				return this.m_columnSpacingValue;
			}
			set
			{
				this.m_columnSpacingValue = value;
			}
		}

		// Token: 0x17001AF6 RID: 6902
		// (get) Token: 0x06003FBE RID: 16318 RVA: 0x0010D6D7 File Offset: 0x0010B8D7
		// (set) Token: 0x06003FBF RID: 16319 RVA: 0x0010D6DF File Offset: 0x0010B8DF
		internal ReportSize ColumnSpacingForRendering
		{
			get
			{
				return this.m_columnSpacingForRendering;
			}
			set
			{
				this.m_columnSpacingForRendering = value;
			}
		}

		// Token: 0x17001AF7 RID: 6903
		// (get) Token: 0x06003FC0 RID: 16320 RVA: 0x0010D6E8 File Offset: 0x0010B8E8
		IInstancePath IStyleContainer.InstancePath
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001AF8 RID: 6904
		// (get) Token: 0x06003FC1 RID: 16321 RVA: 0x0010D6EB File Offset: 0x0010B8EB
		// (set) Token: 0x06003FC2 RID: 16322 RVA: 0x0010D6F3 File Offset: 0x0010B8F3
		public Microsoft.ReportingServices.ReportIntermediateFormat.Style StyleClass
		{
			get
			{
				return this.m_styleClass;
			}
			set
			{
				this.m_styleClass = value;
			}
		}

		// Token: 0x17001AF9 RID: 6905
		// (get) Token: 0x06003FC3 RID: 16323 RVA: 0x0010D6FC File Offset: 0x0010B8FC
		public Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Page;
			}
		}

		// Token: 0x17001AFA RID: 6906
		// (get) Token: 0x06003FC4 RID: 16324 RVA: 0x0010D700 File Offset: 0x0010B900
		public string Name
		{
			get
			{
				return "Page";
			}
		}

		// Token: 0x17001AFB RID: 6907
		// (get) Token: 0x06003FC5 RID: 16325 RVA: 0x0010D707 File Offset: 0x0010B907
		// (set) Token: 0x06003FC6 RID: 16326 RVA: 0x0010D70F File Offset: 0x0010B90F
		internal int ExprHostID
		{
			get
			{
				return this.m_exprHostID;
			}
			set
			{
				this.m_exprHostID = value;
			}
		}

		// Token: 0x17001AFC RID: 6908
		// (get) Token: 0x06003FC7 RID: 16327 RVA: 0x0010D718 File Offset: 0x0010B918
		// (set) Token: 0x06003FC8 RID: 16328 RVA: 0x0010D720 File Offset: 0x0010B920
		internal ReportSize InteractiveHeightForRendering
		{
			get
			{
				return this.m_interactiveHeightForRendering;
			}
			set
			{
				this.m_interactiveHeightForRendering = value;
			}
		}

		// Token: 0x17001AFD RID: 6909
		// (get) Token: 0x06003FC9 RID: 16329 RVA: 0x0010D729 File Offset: 0x0010B929
		// (set) Token: 0x06003FCA RID: 16330 RVA: 0x0010D731 File Offset: 0x0010B931
		internal ReportSize InteractiveWidthForRendering
		{
			get
			{
				return this.m_interactiveWidthForRendering;
			}
			set
			{
				this.m_interactiveWidthForRendering = value;
			}
		}

		// Token: 0x17001AFE RID: 6910
		// (get) Token: 0x06003FCB RID: 16331 RVA: 0x0010D73A File Offset: 0x0010B93A
		// (set) Token: 0x06003FCC RID: 16332 RVA: 0x0010D742 File Offset: 0x0010B942
		internal List<DataAggregateInfo> PageAggregates
		{
			get
			{
				return this.m_pageAggregates;
			}
			set
			{
				this.m_pageAggregates = value;
			}
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x0010D74C File Offset: 0x0010B94C
		internal void Initialize(InitializationContext context)
		{
			this.m_pageHeightValue = context.ValidateSize(ref this.m_pageHeight, "PageHeight");
			if (this.m_pageHeightValue <= 0.0)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsInvalidSize, Severity.Error, context.ObjectType, context.ObjectName, "PageHeight", new string[] { this.m_pageHeightValue.ToString(CultureInfo.InvariantCulture) });
			}
			this.m_pageWidthValue = context.ValidateSize(ref this.m_pageWidth, "PageWidth");
			if (this.m_pageWidthValue <= 0.0)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsInvalidSize, Severity.Error, context.ObjectType, context.ObjectName, "PageWidth", new string[] { this.m_pageWidthValue.ToString(CultureInfo.InvariantCulture) });
			}
			if (this.m_interactiveHeight != null)
			{
				this.m_interactiveHeightValue = context.ValidateSize(ref this.m_interactiveHeight, false, "InteractiveHeight");
				if (0.0 == this.m_interactiveHeightValue)
				{
					this.m_interactiveHeightValue = double.MaxValue;
				}
				else if (this.m_interactiveHeightValue < 0.0)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidSize, Severity.Error, context.ObjectType, context.ObjectName, "InteractiveHeight", new string[] { this.m_interactiveHeightValue.ToString(CultureInfo.InvariantCulture) });
				}
			}
			if (this.m_interactiveWidth != null)
			{
				this.m_interactiveWidthValue = context.ValidateSize(ref this.m_interactiveWidth, false, "InteractiveWidth");
				if (0.0 == this.m_interactiveWidthValue)
				{
					this.m_interactiveWidthValue = double.MaxValue;
				}
				else if (this.m_interactiveWidthValue < 0.0)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidSize, Severity.Error, context.ObjectType, context.ObjectName, "InteractiveWidth", new string[] { this.m_interactiveWidthValue.ToString(CultureInfo.InvariantCulture) });
				}
			}
			this.m_leftMarginValue = context.ValidateSize(ref this.m_leftMargin, "LeftMargin");
			this.m_rightMarginValue = context.ValidateSize(ref this.m_rightMargin, "RightMargin");
			this.m_topMarginValue = context.ValidateSize(ref this.m_topMargin, "TopMargin");
			this.m_bottomMarginValue = context.ValidateSize(ref this.m_bottomMargin, "BottomMargin");
			this.m_columnSpacingValue = context.ValidateSize(ref this.m_columnSpacing, "ColumnSpacing");
			if (this.m_styleClass != null)
			{
				context.ExprHostBuilder.PageStart();
				this.m_styleClass.Initialize(context);
				this.ExprHostID = context.ExprHostBuilder.PageEnd();
			}
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x0010DA00 File Offset: 0x0010BC00
		internal void PageHeaderFooterInitialize(InitializationContext context)
		{
			context.RegisterPageSectionScope(this, this.m_pageAggregates);
			if (this.m_pageHeader != null)
			{
				context.RegisterReportItems(this.m_pageHeader.ReportItems);
			}
			if (this.m_pageFooter != null)
			{
				context.RegisterReportItems(this.m_pageFooter.ReportItems);
			}
			this.m_textboxesInScope = context.GetCurrentReferencableTextboxesInSection();
			if (this.m_pageHeader != null)
			{
				this.m_pageHeader.Initialize(context);
			}
			if (this.m_pageFooter != null)
			{
				this.m_pageFooter.Initialize(context);
			}
			if (this.m_pageHeader != null)
			{
				context.UnRegisterReportItems(this.m_pageHeader.ReportItems);
			}
			if (this.m_pageFooter != null)
			{
				context.UnRegisterReportItems(this.m_pageFooter.ReportItems);
			}
			context.ValidateToggleItems();
			context.UnRegisterPageSectionScope();
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x0010DAC8 File Offset: 0x0010BCC8
		internal void SetExprHost(ReportExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			if (this.m_styleClass != null && this.ExprHostID >= 0)
			{
				StyleExprHost styleExprHost = null;
				if (exprHost.PageHostsRemotable != null)
				{
					styleExprHost = exprHost.PageHostsRemotable[this.ExprHostID];
				}
				else if (this.ExprHostID == 0)
				{
					styleExprHost = exprHost.PageHost;
					if (styleExprHost == null)
					{
						return;
					}
				}
				else
				{
					Global.Tracer.Assert(false, "Missing ReportExprHost.PageHostRemotable for Page ExprHostID: {0}", new object[] { this.ExprHostID });
				}
				styleExprHost.SetReportObjectModel(reportObjectModel);
				this.m_styleClass.SetStyleExprHost(styleExprHost);
			}
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x0010DB67 File Offset: 0x0010BD67
		public bool TextboxInScope(int sequenceIndex)
		{
			return SequenceIndex.GetBit(this.m_textboxesInScope, sequenceIndex, true);
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x0010DB76 File Offset: 0x0010BD76
		public void AddInScopeTextBox(Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textbox)
		{
			if (this.m_inScopeTextBoxes == null)
			{
				this.m_inScopeTextBoxes = new List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox>();
			}
			this.m_inScopeTextBoxes.Add(textbox);
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x0010DB98 File Offset: 0x0010BD98
		public void ResetTextBoxImpls(OnDemandProcessingContext context)
		{
			if (this.m_inScopeTextBoxes != null)
			{
				for (int i = 0; i < this.m_inScopeTextBoxes.Count; i++)
				{
					this.m_inScopeTextBoxes[i].ResetTextBoxImpl(context);
				}
			}
		}

		// Token: 0x17001AFF RID: 6911
		// (get) Token: 0x06003FD3 RID: 16339 RVA: 0x0010DBD5 File Offset: 0x0010BDD5
		// (set) Token: 0x06003FD4 RID: 16340 RVA: 0x0010DBD8 File Offset: 0x0010BDD8
		public bool NeedToCacheDataRows
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x0010DBDA File Offset: 0x0010BDDA
		public bool VariableInScope(int sequenceIndex)
		{
			return SequenceIndex.GetBit(this.m_variablesInScope, sequenceIndex, true);
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x0010DBE9 File Offset: 0x0010BDE9
		public void AddInScopeEventSource(IInScopeEventSource eventSource)
		{
			Global.Tracer.Assert(false, "Top level event sources should be registered on the Report, not ReportSection");
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x0010DBFB File Offset: 0x0010BDFB
		List<DataAggregateInfo> IAggregateHolder.GetAggregateList()
		{
			return this.m_pageAggregates;
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x0010DC03 File Offset: 0x0010BE03
		List<DataAggregateInfo> IAggregateHolder.GetPostSortAggregateList()
		{
			return null;
		}

		// Token: 0x17001B00 RID: 6912
		// (get) Token: 0x06003FD9 RID: 16345 RVA: 0x0010DC06 File Offset: 0x0010BE06
		DataScopeInfo IAggregateHolder.DataScopeInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x0010DC09 File Offset: 0x0010BE09
		void IAggregateHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_pageAggregates != null, "(null != m_pageAggregates)");
			if (this.m_pageAggregates.Count == 0)
			{
				this.m_pageAggregates = null;
			}
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x0010DC37 File Offset: 0x0010BE37
		internal void SetTextboxesInScope(byte[] items)
		{
			this.m_textboxesInScope = items;
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x0010DC40 File Offset: 0x0010BE40
		internal void SetInScopeTextBoxes(List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox> items)
		{
			this.m_inScopeTextBoxes = items;
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x0010DC4C File Offset: 0x0010BE4C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Page, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.PageHeader, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PageSection),
				new MemberInfo(MemberName.PageFooter, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PageSection),
				new MemberInfo(MemberName.PageHeight, Token.String),
				new MemberInfo(MemberName.PageHeightValue, Token.Double),
				new MemberInfo(MemberName.PageWidth, Token.String),
				new MemberInfo(MemberName.PageWidthValue, Token.Double),
				new MemberInfo(MemberName.LeftMargin, Token.String),
				new MemberInfo(MemberName.LeftMarginValue, Token.Double),
				new MemberInfo(MemberName.RightMargin, Token.String),
				new MemberInfo(MemberName.RightMarginValue, Token.Double),
				new MemberInfo(MemberName.TopMargin, Token.String),
				new MemberInfo(MemberName.TopMarginValue, Token.Double),
				new MemberInfo(MemberName.BottomMargin, Token.String),
				new MemberInfo(MemberName.BottomMarginValue, Token.Double),
				new MemberInfo(MemberName.InteractiveHeight, Token.String),
				new MemberInfo(MemberName.InteractiveHeightValue, Token.Double),
				new MemberInfo(MemberName.InteractiveWidth, Token.String),
				new MemberInfo(MemberName.InteractiveWidthValue, Token.Double),
				new MemberInfo(MemberName.Columns, Token.Int32),
				new MemberInfo(MemberName.ColumnSpacing, Token.String),
				new MemberInfo(MemberName.ColumnSpacingValue, Token.Double),
				new MemberInfo(MemberName.StyleClass, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Style),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.InScopeTextBoxes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TextBox),
				new MemberInfo(MemberName.TextboxesInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Byte),
				new MemberInfo(MemberName.VariablesInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Byte),
				new MemberInfo(MemberName.PageAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfo)
			});
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x0010DEA8 File Offset: 0x0010C0A8
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Page.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ExprHostID)
				{
					if (memberName == MemberName.StyleClass)
					{
						writer.Write(this.m_styleClass);
						continue;
					}
					switch (memberName)
					{
					case MemberName.PageHeader:
						writer.Write(this.m_pageHeader);
						continue;
					case MemberName.PageFooter:
						writer.Write(this.m_pageFooter);
						continue;
					case MemberName.ReportItems:
					case MemberName.DataSources:
					case MemberName.ClassName:
					case MemberName.InstanceName:
					case MemberName.CodeModules:
					case MemberName.CodeClasses:
						break;
					case MemberName.PageHeight:
						writer.Write(this.m_pageHeight);
						continue;
					case MemberName.PageHeightValue:
						writer.Write(this.m_pageHeightValue);
						continue;
					case MemberName.PageWidth:
						writer.Write(this.m_pageWidth);
						continue;
					case MemberName.PageWidthValue:
						writer.Write(this.m_pageWidthValue);
						continue;
					case MemberName.LeftMargin:
						writer.Write(this.m_leftMargin);
						continue;
					case MemberName.LeftMarginValue:
						writer.Write(this.m_leftMarginValue);
						continue;
					case MemberName.RightMargin:
						writer.Write(this.m_rightMargin);
						continue;
					case MemberName.RightMarginValue:
						writer.Write(this.m_rightMarginValue);
						continue;
					case MemberName.TopMargin:
						writer.Write(this.m_topMargin);
						continue;
					case MemberName.TopMarginValue:
						writer.Write(this.m_topMarginValue);
						continue;
					case MemberName.BottomMargin:
						writer.Write(this.m_bottomMargin);
						continue;
					case MemberName.BottomMarginValue:
						writer.Write(this.m_bottomMarginValue);
						continue;
					case MemberName.Columns:
						writer.Write(this.m_columns);
						continue;
					case MemberName.ColumnSpacing:
						writer.Write(this.m_columnSpacing);
						continue;
					case MemberName.ColumnSpacingValue:
						writer.Write(this.m_columnSpacingValue);
						continue;
					case MemberName.PageAggregates:
						writer.Write<DataAggregateInfo>(this.m_pageAggregates);
						continue;
					default:
						if (memberName == MemberName.ExprHostID)
						{
							writer.Write(this.m_exprHostID);
							continue;
						}
						break;
					}
				}
				else if (memberName <= MemberName.InScopeTextBoxes)
				{
					switch (memberName)
					{
					case MemberName.InteractiveHeight:
						writer.Write(this.m_interactiveHeight);
						continue;
					case MemberName.InteractiveHeightValue:
						writer.Write(this.m_interactiveHeightValue);
						continue;
					case MemberName.InteractiveWidth:
						writer.Write(this.m_interactiveWidth);
						continue;
					case MemberName.InteractiveWidthValue:
						writer.Write(this.m_interactiveWidthValue);
						continue;
					default:
						if (memberName == MemberName.InScopeTextBoxes)
						{
							writer.WriteListOfReferences(this.m_inScopeTextBoxes);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.VariablesInScope)
					{
						writer.Write(this.m_variablesInScope);
						continue;
					}
					if (memberName == MemberName.TextboxesInScope)
					{
						writer.Write(this.m_textboxesInScope);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x0010E1A0 File Offset: 0x0010C3A0
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Page.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ExprHostID)
				{
					if (memberName == MemberName.StyleClass)
					{
						this.m_styleClass = (Microsoft.ReportingServices.ReportIntermediateFormat.Style)reader.ReadRIFObject();
						continue;
					}
					switch (memberName)
					{
					case MemberName.PageHeader:
						this.m_pageHeader = (Microsoft.ReportingServices.ReportIntermediateFormat.PageSection)reader.ReadRIFObject();
						continue;
					case MemberName.PageFooter:
						this.m_pageFooter = (Microsoft.ReportingServices.ReportIntermediateFormat.PageSection)reader.ReadRIFObject();
						continue;
					case MemberName.ReportItems:
					case MemberName.DataSources:
					case MemberName.ClassName:
					case MemberName.InstanceName:
					case MemberName.CodeModules:
					case MemberName.CodeClasses:
						break;
					case MemberName.PageHeight:
						this.m_pageHeight = reader.ReadString();
						continue;
					case MemberName.PageHeightValue:
						this.m_pageHeightValue = reader.ReadDouble();
						continue;
					case MemberName.PageWidth:
						this.m_pageWidth = reader.ReadString();
						continue;
					case MemberName.PageWidthValue:
						this.m_pageWidthValue = reader.ReadDouble();
						continue;
					case MemberName.LeftMargin:
						this.m_leftMargin = reader.ReadString();
						continue;
					case MemberName.LeftMarginValue:
						this.m_leftMarginValue = reader.ReadDouble();
						continue;
					case MemberName.RightMargin:
						this.m_rightMargin = reader.ReadString();
						continue;
					case MemberName.RightMarginValue:
						this.m_rightMarginValue = reader.ReadDouble();
						continue;
					case MemberName.TopMargin:
						this.m_topMargin = reader.ReadString();
						continue;
					case MemberName.TopMarginValue:
						this.m_topMarginValue = reader.ReadDouble();
						continue;
					case MemberName.BottomMargin:
						this.m_bottomMargin = reader.ReadString();
						continue;
					case MemberName.BottomMarginValue:
						this.m_bottomMarginValue = reader.ReadDouble();
						continue;
					case MemberName.Columns:
						this.m_columns = reader.ReadInt32();
						continue;
					case MemberName.ColumnSpacing:
						this.m_columnSpacing = reader.ReadString();
						continue;
					case MemberName.ColumnSpacingValue:
						this.m_columnSpacingValue = reader.ReadDouble();
						continue;
					case MemberName.PageAggregates:
						this.m_pageAggregates = reader.ReadGenericListOfRIFObjects<DataAggregateInfo>();
						continue;
					default:
						if (memberName == MemberName.ExprHostID)
						{
							this.m_exprHostID = reader.ReadInt32();
							continue;
						}
						break;
					}
				}
				else if (memberName <= MemberName.InScopeTextBoxes)
				{
					switch (memberName)
					{
					case MemberName.InteractiveHeight:
						this.m_interactiveHeight = reader.ReadString();
						continue;
					case MemberName.InteractiveHeightValue:
						this.m_interactiveHeightValue = reader.ReadDouble();
						continue;
					case MemberName.InteractiveWidth:
						this.m_interactiveWidth = reader.ReadString();
						continue;
					case MemberName.InteractiveWidthValue:
						this.m_interactiveWidthValue = reader.ReadDouble();
						continue;
					default:
						if (memberName == MemberName.InScopeTextBoxes)
						{
							this.m_inScopeTextBoxes = reader.ReadGenericListOfReferences<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox>(this);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.VariablesInScope)
					{
						this.m_variablesInScope = reader.ReadByteArray();
						continue;
					}
					if (memberName == MemberName.TextboxesInScope)
					{
						this.m_textboxesInScope = reader.ReadByteArray();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x0010E4A8 File Offset: 0x0010C6A8
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(Page.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.InScopeTextBoxes)
					{
						if (this.m_inScopeTextBoxes == null)
						{
							this.m_inScopeTextBoxes = new List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox>();
						}
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable;
						referenceableItems.TryGetValue(memberReference.RefID, out referenceable);
						Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textBox = (Microsoft.ReportingServices.ReportIntermediateFormat.TextBox)referenceable;
						this.m_inScopeTextBoxes.Add(textBox);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x0010E554 File Offset: 0x0010C754
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Page;
		}

		// Token: 0x04001D4B RID: 7499
		private Microsoft.ReportingServices.ReportIntermediateFormat.PageSection m_pageHeader;

		// Token: 0x04001D4C RID: 7500
		private Microsoft.ReportingServices.ReportIntermediateFormat.PageSection m_pageFooter;

		// Token: 0x04001D4D RID: 7501
		private string m_pageHeight = "11in";

		// Token: 0x04001D4E RID: 7502
		private double m_pageHeightValue;

		// Token: 0x04001D4F RID: 7503
		private string m_pageWidth = "8.5in";

		// Token: 0x04001D50 RID: 7504
		private double m_pageWidthValue;

		// Token: 0x04001D51 RID: 7505
		private string m_leftMargin = "0in";

		// Token: 0x04001D52 RID: 7506
		private double m_leftMarginValue;

		// Token: 0x04001D53 RID: 7507
		private string m_rightMargin = "0in";

		// Token: 0x04001D54 RID: 7508
		private double m_rightMarginValue;

		// Token: 0x04001D55 RID: 7509
		private string m_topMargin = "0in";

		// Token: 0x04001D56 RID: 7510
		private double m_topMarginValue;

		// Token: 0x04001D57 RID: 7511
		private string m_bottomMargin = "0in";

		// Token: 0x04001D58 RID: 7512
		private double m_bottomMarginValue;

		// Token: 0x04001D59 RID: 7513
		private string m_interactiveHeight;

		// Token: 0x04001D5A RID: 7514
		private double m_interactiveHeightValue = -1.0;

		// Token: 0x04001D5B RID: 7515
		private string m_interactiveWidth;

		// Token: 0x04001D5C RID: 7516
		private double m_interactiveWidthValue = -1.0;

		// Token: 0x04001D5D RID: 7517
		private int m_columns = 1;

		// Token: 0x04001D5E RID: 7518
		private string m_columnSpacing = "0.5in";

		// Token: 0x04001D5F RID: 7519
		private double m_columnSpacingValue;

		// Token: 0x04001D60 RID: 7520
		private int m_exprHostID = -1;

		// Token: 0x04001D61 RID: 7521
		private byte[] m_textboxesInScope;

		// Token: 0x04001D62 RID: 7522
		private byte[] m_variablesInScope;

		// Token: 0x04001D63 RID: 7523
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox> m_inScopeTextBoxes;

		// Token: 0x04001D64 RID: 7524
		private List<DataAggregateInfo> m_pageAggregates;

		// Token: 0x04001D65 RID: 7525
		[NonSerialized]
		internal const int UpgradedExprHostId = 0;

		// Token: 0x04001D66 RID: 7526
		[NonSerialized]
		private ReportSize m_columnSpacingForRendering;

		// Token: 0x04001D67 RID: 7527
		[NonSerialized]
		private ReportSize m_pageWidthForRendering;

		// Token: 0x04001D68 RID: 7528
		[NonSerialized]
		private ReportSize m_pageHeightForRendering;

		// Token: 0x04001D69 RID: 7529
		private Microsoft.ReportingServices.ReportIntermediateFormat.Style m_styleClass;

		// Token: 0x04001D6A RID: 7530
		[NonSerialized]
		private ReportSize m_bottomMarginForRendering;

		// Token: 0x04001D6B RID: 7531
		[NonSerialized]
		private ReportSize m_topMarginForRendering;

		// Token: 0x04001D6C RID: 7532
		[NonSerialized]
		private ReportSize m_rightMarginForRendering;

		// Token: 0x04001D6D RID: 7533
		[NonSerialized]
		private ReportSize m_leftMarginForRendering;

		// Token: 0x04001D6E RID: 7534
		[NonSerialized]
		private ReportSize m_interactiveHeightForRendering;

		// Token: 0x04001D6F RID: 7535
		[NonSerialized]
		private ReportSize m_interactiveWidthForRendering;

		// Token: 0x04001D70 RID: 7536
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Page.GetDeclaration();
	}
}
