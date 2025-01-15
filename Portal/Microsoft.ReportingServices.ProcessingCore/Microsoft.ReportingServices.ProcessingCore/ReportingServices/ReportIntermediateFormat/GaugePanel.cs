using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003E4 RID: 996
	[Serializable]
	internal sealed class GaugePanel : Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060028E3 RID: 10467 RVA: 0x000BF186 File Offset: 0x000BD386
		internal GaugePanel(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000BF18F File Offset: 0x000BD38F
		internal GaugePanel(int id, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(id, parent)
		{
			base.RowCount = 1;
			base.ColumnCount = 1;
		}

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x000BF1A7 File Offset: 0x000BD3A7
		internal override Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel;
			}
		}

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x060028E6 RID: 10470 RVA: 0x000BF1AB File Offset: 0x000BD3AB
		internal override HierarchyNodeList ColumnMembers
		{
			get
			{
				return this.m_columnMembers;
			}
		}

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x000BF1B3 File Offset: 0x000BD3B3
		internal override HierarchyNodeList RowMembers
		{
			get
			{
				return this.m_rowMembers;
			}
		}

		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x060028E8 RID: 10472 RVA: 0x000BF1BB File Offset: 0x000BD3BB
		internal override RowList Rows
		{
			get
			{
				return this.m_rows;
			}
		}

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x060028E9 RID: 10473 RVA: 0x000BF1C3 File Offset: 0x000BD3C3
		// (set) Token: 0x060028EA RID: 10474 RVA: 0x000BF1E9 File Offset: 0x000BD3E9
		internal GaugeMember GaugeMember
		{
			get
			{
				if (this.m_columnMembers != null && this.m_columnMembers.Count > 0)
				{
					return this.m_columnMembers[0];
				}
				return null;
			}
			set
			{
				if (this.m_columnMembers == null)
				{
					this.m_columnMembers = new GaugeMemberList();
				}
				else
				{
					this.m_columnMembers.Clear();
				}
				this.m_columnMembers.Add(value);
			}
		}

		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x000BF218 File Offset: 0x000BD418
		// (set) Token: 0x060028EC RID: 10476 RVA: 0x000BF23E File Offset: 0x000BD43E
		internal GaugeMember GaugeRowMember
		{
			get
			{
				if (this.m_rowMembers != null && this.m_rowMembers.Count == 1)
				{
					return this.m_rowMembers[0];
				}
				return null;
			}
			set
			{
				if (this.m_rowMembers == null)
				{
					this.m_rowMembers = new GaugeMemberList();
				}
				else
				{
					this.m_rowMembers.Clear();
				}
				this.m_rowMembers.Add(value);
			}
		}

		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x060028ED RID: 10477 RVA: 0x000BF26D File Offset: 0x000BD46D
		// (set) Token: 0x060028EE RID: 10478 RVA: 0x000BF293 File Offset: 0x000BD493
		internal GaugeRow GaugeRow
		{
			get
			{
				if (this.m_rows != null && this.m_rows.Count > 0)
				{
					return this.m_rows[0];
				}
				return null;
			}
			set
			{
				if (this.m_rows == null)
				{
					this.m_rows = new GaugeRowList();
				}
				else
				{
					this.m_rows.Clear();
				}
				this.m_rows.Add(value);
			}
		}

		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x060028EF RID: 10479 RVA: 0x000BF2C2 File Offset: 0x000BD4C2
		// (set) Token: 0x060028F0 RID: 10480 RVA: 0x000BF2CA File Offset: 0x000BD4CA
		internal List<LinearGauge> LinearGauges
		{
			get
			{
				return this.m_linearGauges;
			}
			set
			{
				this.m_linearGauges = value;
			}
		}

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x000BF2D3 File Offset: 0x000BD4D3
		// (set) Token: 0x060028F2 RID: 10482 RVA: 0x000BF2DB File Offset: 0x000BD4DB
		internal List<RadialGauge> RadialGauges
		{
			get
			{
				return this.m_radialGauges;
			}
			set
			{
				this.m_radialGauges = value;
			}
		}

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x060028F3 RID: 10483 RVA: 0x000BF2E4 File Offset: 0x000BD4E4
		// (set) Token: 0x060028F4 RID: 10484 RVA: 0x000BF2EC File Offset: 0x000BD4EC
		internal List<NumericIndicator> NumericIndicators
		{
			get
			{
				return this.m_numericIndicators;
			}
			set
			{
				this.m_numericIndicators = value;
			}
		}

		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x060028F5 RID: 10485 RVA: 0x000BF2F5 File Offset: 0x000BD4F5
		// (set) Token: 0x060028F6 RID: 10486 RVA: 0x000BF2FD File Offset: 0x000BD4FD
		internal List<StateIndicator> StateIndicators
		{
			get
			{
				return this.m_stateIndicators;
			}
			set
			{
				this.m_stateIndicators = value;
			}
		}

		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x000BF306 File Offset: 0x000BD506
		// (set) Token: 0x060028F8 RID: 10488 RVA: 0x000BF30E File Offset: 0x000BD50E
		internal List<GaugeImage> GaugeImages
		{
			get
			{
				return this.m_gaugeImages;
			}
			set
			{
				this.m_gaugeImages = value;
			}
		}

		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x000BF317 File Offset: 0x000BD517
		// (set) Token: 0x060028FA RID: 10490 RVA: 0x000BF31F File Offset: 0x000BD51F
		internal List<GaugeLabel> GaugeLabels
		{
			get
			{
				return this.m_gaugeLabels;
			}
			set
			{
				this.m_gaugeLabels = value;
			}
		}

		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x060028FB RID: 10491 RVA: 0x000BF328 File Offset: 0x000BD528
		// (set) Token: 0x060028FC RID: 10492 RVA: 0x000BF330 File Offset: 0x000BD530
		internal ExpressionInfo AntiAliasing
		{
			get
			{
				return this.m_antiAliasing;
			}
			set
			{
				this.m_antiAliasing = value;
			}
		}

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x060028FD RID: 10493 RVA: 0x000BF339 File Offset: 0x000BD539
		// (set) Token: 0x060028FE RID: 10494 RVA: 0x000BF341 File Offset: 0x000BD541
		internal ExpressionInfo AutoLayout
		{
			get
			{
				return this.m_autoLayout;
			}
			set
			{
				this.m_autoLayout = value;
			}
		}

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x060028FF RID: 10495 RVA: 0x000BF34A File Offset: 0x000BD54A
		// (set) Token: 0x06002900 RID: 10496 RVA: 0x000BF352 File Offset: 0x000BD552
		internal BackFrame BackFrame
		{
			get
			{
				return this.m_backFrame;
			}
			set
			{
				this.m_backFrame = value;
			}
		}

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x06002901 RID: 10497 RVA: 0x000BF35B File Offset: 0x000BD55B
		// (set) Token: 0x06002902 RID: 10498 RVA: 0x000BF363 File Offset: 0x000BD563
		internal ExpressionInfo ShadowIntensity
		{
			get
			{
				return this.m_shadowIntensity;
			}
			set
			{
				this.m_shadowIntensity = value;
			}
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x06002903 RID: 10499 RVA: 0x000BF36C File Offset: 0x000BD56C
		// (set) Token: 0x06002904 RID: 10500 RVA: 0x000BF374 File Offset: 0x000BD574
		internal ExpressionInfo TextAntiAliasingQuality
		{
			get
			{
				return this.m_textAntiAliasingQuality;
			}
			set
			{
				this.m_textAntiAliasingQuality = value;
			}
		}

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x06002905 RID: 10501 RVA: 0x000BF37D File Offset: 0x000BD57D
		// (set) Token: 0x06002906 RID: 10502 RVA: 0x000BF385 File Offset: 0x000BD585
		internal TopImage TopImage
		{
			get
			{
				return this.m_topImage;
			}
			set
			{
				this.m_topImage = value;
			}
		}

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x06002907 RID: 10503 RVA: 0x000BF38E File Offset: 0x000BD58E
		internal GaugePanelExprHost GaugePanelExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x06002908 RID: 10504 RVA: 0x000BF396 File Offset: 0x000BD596
		protected override IndexedExprHost UserSortExpressionsHost
		{
			get
			{
				if (this.m_exprHost == null)
				{
					return null;
				}
				return this.m_exprHost.UserSortExpressionsHost;
			}
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x000BF3B0 File Offset: 0x000BD5B0
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			if ((context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDetail) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 && (context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InGrouping) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsDataRegionInDetailList, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
			}
			else
			{
				if (!context.RegisterDataRegion(this))
				{
					return false;
				}
				context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataSet | Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion;
				context.ExprHostBuilder.DataRegionStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.GaugePanel, this.m_name);
				base.Initialize(context);
				base.ExprHostID = context.ExprHostBuilder.DataRegionEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.GaugePanel);
				context.UnRegisterDataRegion(this);
			}
			return false;
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x000BF468 File Offset: 0x000BD668
		protected override void InitializeCorner(InitializationContext context)
		{
			if (this.GaugeRow != null)
			{
				this.GaugeRow.Initialize(context);
			}
			if (this.m_linearGauges != null)
			{
				for (int i = 0; i < this.m_linearGauges.Count; i++)
				{
					this.m_linearGauges[i].Initialize(context);
				}
			}
			if (this.m_radialGauges != null)
			{
				for (int j = 0; j < this.m_radialGauges.Count; j++)
				{
					this.m_radialGauges[j].Initialize(context);
				}
			}
			if (this.m_numericIndicators != null)
			{
				for (int k = 0; k < this.m_numericIndicators.Count; k++)
				{
					this.m_numericIndicators[k].Initialize(context);
				}
			}
			if (this.m_stateIndicators != null)
			{
				for (int l = 0; l < this.m_stateIndicators.Count; l++)
				{
					this.m_stateIndicators[l].Initialize(context);
				}
			}
			if (this.m_gaugeImages != null)
			{
				for (int m = 0; m < this.m_gaugeImages.Count; m++)
				{
					this.m_gaugeImages[m].Initialize(context);
				}
			}
			if (this.m_gaugeLabels != null)
			{
				for (int n = 0; n < this.m_gaugeLabels.Count; n++)
				{
					this.m_gaugeLabels[n].Initialize(context);
				}
			}
			if (this.m_antiAliasing != null)
			{
				this.m_antiAliasing.Initialize("AntiAliasing", context);
				context.ExprHostBuilder.GaugePanelAntiAliasing(this.m_antiAliasing);
			}
			if (this.m_autoLayout != null)
			{
				this.m_autoLayout.Initialize("AutoLayout", context);
				context.ExprHostBuilder.GaugePanelAutoLayout(this.m_autoLayout);
			}
			if (this.m_backFrame != null)
			{
				this.m_backFrame.Initialize(context);
			}
			if (this.m_shadowIntensity != null)
			{
				this.m_shadowIntensity.Initialize("ShadowIntensity", context);
				context.ExprHostBuilder.GaugePanelShadowIntensity(this.m_shadowIntensity);
			}
			if (this.m_textAntiAliasingQuality != null)
			{
				this.m_textAntiAliasingQuality.Initialize("TextAntiAliasingQuality", context);
				context.ExprHostBuilder.GaugePanelTextAntiAliasingQuality(this.m_textAntiAliasingQuality);
			}
			if (this.m_topImage != null)
			{
				this.m_topImage.Initialize(context);
			}
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x000BF687 File Offset: 0x000BD887
		protected override bool ValidateInnerStructure(InitializationContext context)
		{
			return true;
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x000BF68C File Offset: 0x000BD88C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			GaugePanel gaugePanel = (GaugePanel)base.PublishClone(context);
			context.CurrentDataRegionClone = gaugePanel;
			gaugePanel.m_rows = new GaugeRowList();
			gaugePanel.m_rowMembers = new GaugeMemberList();
			gaugePanel.m_columnMembers = new GaugeMemberList();
			if (this.GaugeMember != null)
			{
				gaugePanel.GaugeMember = (GaugeMember)this.GaugeMember.PublishClone(context, gaugePanel);
			}
			if (this.GaugeRowMember != null)
			{
				gaugePanel.GaugeRowMember = (GaugeMember)this.GaugeRowMember.PublishClone(context);
			}
			if (this.GaugeRow != null)
			{
				gaugePanel.GaugeRow = (GaugeRow)this.GaugeRow.PublishClone(context);
			}
			if (this.m_linearGauges != null)
			{
				gaugePanel.m_linearGauges = new List<LinearGauge>(this.m_linearGauges.Count);
				foreach (LinearGauge linearGauge in this.m_linearGauges)
				{
					gaugePanel.m_linearGauges.Add((LinearGauge)linearGauge.PublishClone(context));
				}
			}
			if (this.m_radialGauges != null)
			{
				gaugePanel.m_radialGauges = new List<RadialGauge>(this.m_radialGauges.Count);
				foreach (RadialGauge radialGauge in this.m_radialGauges)
				{
					gaugePanel.m_radialGauges.Add((RadialGauge)radialGauge.PublishClone(context));
				}
			}
			if (this.m_numericIndicators != null)
			{
				gaugePanel.m_numericIndicators = new List<NumericIndicator>(this.m_numericIndicators.Count);
				foreach (NumericIndicator numericIndicator in this.m_numericIndicators)
				{
					gaugePanel.m_numericIndicators.Add((NumericIndicator)numericIndicator.PublishClone(context));
				}
			}
			if (this.m_stateIndicators != null)
			{
				gaugePanel.m_stateIndicators = new List<StateIndicator>(this.m_stateIndicators.Count);
				foreach (StateIndicator stateIndicator in this.m_stateIndicators)
				{
					gaugePanel.m_stateIndicators.Add((StateIndicator)stateIndicator.PublishClone(context));
				}
			}
			if (this.m_gaugeImages != null)
			{
				gaugePanel.m_gaugeImages = new List<GaugeImage>(this.m_gaugeImages.Count);
				foreach (GaugeImage gaugeImage in this.m_gaugeImages)
				{
					gaugePanel.m_gaugeImages.Add((GaugeImage)gaugeImage.PublishClone(context));
				}
			}
			if (this.m_gaugeLabels != null)
			{
				gaugePanel.m_gaugeLabels = new List<GaugeLabel>(this.m_gaugeLabels.Count);
				foreach (GaugeLabel gaugeLabel in this.m_gaugeLabels)
				{
					gaugePanel.m_gaugeLabels.Add((GaugeLabel)gaugeLabel.PublishClone(context));
				}
			}
			if (this.m_antiAliasing != null)
			{
				gaugePanel.m_antiAliasing = (ExpressionInfo)this.m_antiAliasing.PublishClone(context);
			}
			if (this.m_autoLayout != null)
			{
				gaugePanel.m_autoLayout = (ExpressionInfo)this.m_autoLayout.PublishClone(context);
			}
			if (this.m_backFrame != null)
			{
				gaugePanel.m_backFrame = (BackFrame)this.m_backFrame.PublishClone(context);
			}
			if (this.m_shadowIntensity != null)
			{
				gaugePanel.m_shadowIntensity = (ExpressionInfo)this.m_shadowIntensity.PublishClone(context);
			}
			if (this.m_textAntiAliasingQuality != null)
			{
				gaugePanel.m_textAntiAliasingQuality = (ExpressionInfo)this.m_textAntiAliasingQuality.PublishClone(context);
			}
			if (this.m_topImage != null)
			{
				gaugePanel.m_topImage = (TopImage)this.m_topImage.PublishClone(context);
			}
			return gaugePanel;
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x000BFA9C File Offset: 0x000BDC9C
		internal GaugeAntiAliasings EvaluateAntiAliasing(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return EnumTranslator.TranslateGaugeAntiAliasings(context.ReportRuntime.EvaluateGaugePanelAntiAliasingExpression(this, base.Name), context.ReportRuntime);
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x000BFAC3 File Offset: 0x000BDCC3
		internal bool EvaluateAutoLayout(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugePanelAutoLayoutExpression(this, base.Name);
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x000BFADF File Offset: 0x000BDCDF
		internal double EvaluateShadowIntensity(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugePanelShadowIntensityExpression(this, base.Name);
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x000BFAFB File Offset: 0x000BDCFB
		internal TextAntiAliasingQualities EvaluateTextAntiAliasingQuality(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return EnumTranslator.TranslateTextAntiAliasingQualities(context.ReportRuntime.EvaluateGaugePanelTextAntiAliasingQualityExpression(this, base.Name), context.ReportRuntime);
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x000BFB24 File Offset: 0x000BDD24
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegion, new List<MemberInfo>
			{
				new ReadOnlyMemberInfo(MemberName.GaugeMember, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeMember),
				new ReadOnlyMemberInfo(MemberName.GaugeRowMember, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeMember),
				new ReadOnlyMemberInfo(MemberName.GaugeRow, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeRow),
				new MemberInfo(MemberName.LinearGauges, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LinearGauge),
				new MemberInfo(MemberName.RadialGauges, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RadialGauge),
				new MemberInfo(MemberName.NumericIndicators, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.NumericIndicator),
				new MemberInfo(MemberName.StateIndicators, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StateIndicator),
				new MemberInfo(MemberName.GaugeImages, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeImage),
				new MemberInfo(MemberName.GaugeLabels, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeLabel),
				new MemberInfo(MemberName.AntiAliasing, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.AutoLayout, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.BackFrame, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BackFrame),
				new MemberInfo(MemberName.ShadowIntensity, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TextAntiAliasingQuality, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TopImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TopImage),
				new MemberInfo(MemberName.ColumnMembers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeMember),
				new MemberInfo(MemberName.RowMembers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeMember),
				new MemberInfo(MemberName.Rows, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeRow)
			});
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000BFCC4 File Offset: 0x000BDEC4
		internal List<GaugeInputValue> GetGaugeInputValues()
		{
			List<GaugeInputValue> list = new List<GaugeInputValue>();
			if (this.RadialGauges != null)
			{
				foreach (RadialGauge radialGauge in this.RadialGauges)
				{
					if (radialGauge.GaugeScales != null)
					{
						foreach (RadialScale radialScale in radialGauge.GaugeScales)
						{
							if (radialScale.MaximumValue != null)
							{
								list.Add(radialScale.MaximumValue);
							}
							if (radialScale.MinimumValue != null)
							{
								list.Add(radialScale.MinimumValue);
							}
							if (radialScale.GaugePointers != null)
							{
								foreach (RadialPointer radialPointer in radialScale.GaugePointers)
								{
									if (radialPointer.GaugeInputValue != null)
									{
										list.Add(radialPointer.GaugeInputValue);
									}
								}
							}
							if (radialScale.ScaleRanges != null)
							{
								foreach (ScaleRange scaleRange in radialScale.ScaleRanges)
								{
									if (scaleRange.StartValue != null)
									{
										list.Add(scaleRange.StartValue);
									}
									if (scaleRange.EndValue != null)
									{
										list.Add(scaleRange.EndValue);
									}
								}
							}
						}
					}
				}
			}
			if (this.LinearGauges != null)
			{
				foreach (LinearGauge linearGauge in this.LinearGauges)
				{
					if (linearGauge.GaugeScales != null)
					{
						foreach (LinearScale linearScale in linearGauge.GaugeScales)
						{
							if (linearScale.MaximumValue != null)
							{
								list.Add(linearScale.MaximumValue);
							}
							if (linearScale.MinimumValue != null)
							{
								list.Add(linearScale.MinimumValue);
							}
							if (linearScale.GaugePointers != null)
							{
								foreach (LinearPointer linearPointer in linearScale.GaugePointers)
								{
									if (linearPointer.GaugeInputValue != null)
									{
										list.Add(linearPointer.GaugeInputValue);
									}
								}
							}
							if (linearScale.ScaleRanges != null)
							{
								foreach (ScaleRange scaleRange2 in linearScale.ScaleRanges)
								{
									if (scaleRange2.StartValue != null)
									{
										list.Add(scaleRange2.StartValue);
									}
									if (scaleRange2.EndValue != null)
									{
										list.Add(scaleRange2.EndValue);
									}
								}
							}
						}
					}
				}
			}
			if (this.NumericIndicators != null)
			{
				foreach (NumericIndicator numericIndicator in this.NumericIndicators)
				{
					if (numericIndicator.GaugeInputValue != null)
					{
						list.Add(numericIndicator.GaugeInputValue);
					}
					if (numericIndicator.MaximumValue != null)
					{
						list.Add(numericIndicator.MaximumValue);
					}
					if (numericIndicator.MinimumValue != null)
					{
						list.Add(numericIndicator.MinimumValue);
					}
					if (numericIndicator.NumericIndicatorRanges != null)
					{
						foreach (NumericIndicatorRange numericIndicatorRange in numericIndicator.NumericIndicatorRanges)
						{
							if (numericIndicatorRange.StartValue != null)
							{
								list.Add(numericIndicatorRange.StartValue);
							}
							if (numericIndicatorRange.EndValue != null)
							{
								list.Add(numericIndicatorRange.EndValue);
							}
						}
					}
				}
			}
			if (this.StateIndicators != null)
			{
				foreach (StateIndicator stateIndicator in this.StateIndicators)
				{
					if (stateIndicator.GaugeInputValue != null)
					{
						list.Add(stateIndicator.GaugeInputValue);
					}
					if (stateIndicator.MinimumValue != null)
					{
						list.Add(stateIndicator.MinimumValue);
					}
					if (stateIndicator.MaximumValue != null)
					{
						list.Add(stateIndicator.MaximumValue);
					}
					if (stateIndicator.IndicatorStates != null)
					{
						foreach (IndicatorState indicatorState in stateIndicator.IndicatorStates)
						{
							if (indicatorState.StartValue != null)
							{
								list.Add(indicatorState.StartValue);
							}
							if (indicatorState.EndValue != null)
							{
								list.Add(indicatorState.EndValue);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x000C02A4 File Offset: 0x000BE4A4
		internal int GenerateActionOwnerID()
		{
			int num = this.m_actionOwnerCounter + 1;
			this.m_actionOwnerCounter = num;
			return num;
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000C02C4 File Offset: 0x000BE4C4
		public override void CreateDomainScopeMember(ReportHierarchyNode parentNode, Grouping grouping, AutomaticSubtotalContext context)
		{
			GaugeMember gaugeMember = new GaugeMember(context.GenerateID(), this);
			gaugeMember.Grouping = grouping.CloneForDomainScope(context, gaugeMember);
			HierarchyNodeList hierarchyNodeList = ((parentNode != null) ? parentNode.InnerHierarchy : this.ColumnMembers);
			if (hierarchyNodeList == null)
			{
				return;
			}
			hierarchyNodeList.Add(gaugeMember);
			gaugeMember.IsColumn = true;
			this.GaugeRow.Cells.Insert(this.ColumnMembers.GetMemberIndex(gaugeMember), new GaugeCell(context.GenerateID(), this));
			base.ColumnCount++;
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x000C034C File Offset: 0x000BE54C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(GaugePanel.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ColumnMembers)
				{
					if (memberName == MemberName.RowMembers)
					{
						writer.Write(this.m_rowMembers);
						continue;
					}
					if (memberName == MemberName.ColumnMembers)
					{
						writer.Write(this.m_columnMembers);
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.BackFrame:
						writer.Write(this.m_backFrame);
						continue;
					case MemberName.ClipContent:
					case MemberName.MinPercent:
					case MemberName.MaxPercent:
					case MemberName.AddConstant:
						break;
					case MemberName.TopImage:
						writer.Write(this.m_topImage);
						continue;
					case MemberName.LinearGauges:
						writer.Write<LinearGauge>(this.m_linearGauges);
						continue;
					case MemberName.RadialGauges:
						writer.Write<RadialGauge>(this.m_radialGauges);
						continue;
					case MemberName.NumericIndicators:
						writer.Write<NumericIndicator>(this.m_numericIndicators);
						continue;
					case MemberName.StateIndicators:
						writer.Write<StateIndicator>(this.m_stateIndicators);
						continue;
					case MemberName.GaugeImages:
						writer.Write<GaugeImage>(this.m_gaugeImages);
						continue;
					case MemberName.GaugeLabels:
						writer.Write<GaugeLabel>(this.m_gaugeLabels);
						continue;
					case MemberName.AntiAliasing:
						writer.Write(this.m_antiAliasing);
						continue;
					case MemberName.AutoLayout:
						writer.Write(this.m_autoLayout);
						continue;
					case MemberName.ShadowIntensity:
						writer.Write(this.m_shadowIntensity);
						continue;
					case MemberName.TextAntiAliasingQuality:
						writer.Write(this.m_textAntiAliasingQuality);
						continue;
					default:
						if (memberName == MemberName.Rows)
						{
							writer.Write(this.m_rows);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000C0504 File Offset: 0x000BE704
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(GaugePanel.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ColumnMembers)
				{
					if (memberName == MemberName.RowMembers)
					{
						this.m_rowMembers = reader.ReadListOfRIFObjects<GaugeMemberList>();
						continue;
					}
					if (memberName == MemberName.ColumnMembers)
					{
						this.m_columnMembers = reader.ReadListOfRIFObjects<GaugeMemberList>();
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.GaugeMember:
						this.GaugeMember = (GaugeMember)reader.ReadRIFObject();
						continue;
					case MemberName.GaugeRowMember:
						this.GaugeRowMember = (GaugeMember)reader.ReadRIFObject();
						continue;
					case MemberName.GaugeRow:
						this.GaugeRow = (GaugeRow)reader.ReadRIFObject();
						continue;
					default:
						switch (memberName)
						{
						case MemberName.BackFrame:
							this.m_backFrame = (BackFrame)reader.ReadRIFObject();
							continue;
						case MemberName.ClipContent:
						case MemberName.MinPercent:
						case MemberName.MaxPercent:
						case MemberName.AddConstant:
							break;
						case MemberName.TopImage:
							this.m_topImage = (TopImage)reader.ReadRIFObject();
							continue;
						case MemberName.LinearGauges:
							this.m_linearGauges = reader.ReadGenericListOfRIFObjects<LinearGauge>();
							continue;
						case MemberName.RadialGauges:
							this.m_radialGauges = reader.ReadGenericListOfRIFObjects<RadialGauge>();
							continue;
						case MemberName.NumericIndicators:
							this.m_numericIndicators = reader.ReadGenericListOfRIFObjects<NumericIndicator>();
							continue;
						case MemberName.StateIndicators:
							this.m_stateIndicators = reader.ReadGenericListOfRIFObjects<StateIndicator>();
							continue;
						case MemberName.GaugeImages:
							this.m_gaugeImages = reader.ReadGenericListOfRIFObjects<GaugeImage>();
							continue;
						case MemberName.GaugeLabels:
							this.m_gaugeLabels = reader.ReadGenericListOfRIFObjects<GaugeLabel>();
							continue;
						case MemberName.AntiAliasing:
							this.m_antiAliasing = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.AutoLayout:
							this.m_autoLayout = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.ShadowIntensity:
							this.m_shadowIntensity = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.TextAntiAliasingQuality:
							this.m_textAntiAliasingQuality = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						default:
							if (memberName == MemberName.Rows)
							{
								this.m_rows = reader.ReadListOfRIFObjects<GaugeRowList>();
								continue;
							}
							break;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000C0737 File Offset: 0x000BE937
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000C0741 File Offset: 0x000BE941
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanel;
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000C0748 File Offset: 0x000BE948
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.GaugePanelHostsRemotable[base.ExprHostID];
				base.DataRegionSetExprHost(this.m_exprHost, this.m_exprHost.SortHost, this.m_exprHost.FilterHostsRemotable, this.m_exprHost.UserSortExpressionsHost, this.m_exprHost.PageBreakExprHost, this.m_exprHost.JoinConditionExprHostsRemotable, reportObjectModel);
			}
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000C07D0 File Offset: 0x000BE9D0
		internal override void DataRegionContentsSetExprHost(ObjectModelImpl reportObjectModel, bool traverseDataRegions)
		{
			if (this.m_exprHost != null)
			{
				IList<LinearGaugeExprHost> linearGaugesHostsRemotable = this.m_exprHost.LinearGaugesHostsRemotable;
				if (this.m_linearGauges != null && linearGaugesHostsRemotable != null)
				{
					for (int i = 0; i < this.m_linearGauges.Count; i++)
					{
						LinearGauge linearGauge = this.m_linearGauges[i];
						if (linearGauge != null && linearGauge.ExpressionHostID > -1)
						{
							linearGauge.SetExprHost(linearGaugesHostsRemotable[linearGauge.ExpressionHostID], reportObjectModel);
						}
					}
				}
				IList<RadialGaugeExprHost> radialGaugesHostsRemotable = this.m_exprHost.RadialGaugesHostsRemotable;
				if (this.m_radialGauges != null && radialGaugesHostsRemotable != null)
				{
					for (int j = 0; j < this.m_radialGauges.Count; j++)
					{
						RadialGauge radialGauge = this.m_radialGauges[j];
						if (radialGauge != null && radialGauge.ExpressionHostID > -1)
						{
							radialGauge.SetExprHost(radialGaugesHostsRemotable[radialGauge.ExpressionHostID], reportObjectModel);
						}
					}
				}
				IList<NumericIndicatorExprHost> numericIndicatorsHostsRemotable = this.m_exprHost.NumericIndicatorsHostsRemotable;
				if (this.m_numericIndicators != null && numericIndicatorsHostsRemotable != null)
				{
					for (int k = 0; k < this.m_numericIndicators.Count; k++)
					{
						NumericIndicator numericIndicator = this.m_numericIndicators[k];
						if (numericIndicator != null && numericIndicator.ExpressionHostID > -1)
						{
							numericIndicator.SetExprHost(numericIndicatorsHostsRemotable[numericIndicator.ExpressionHostID], reportObjectModel);
						}
					}
				}
				IList<StateIndicatorExprHost> stateIndicatorsHostsRemotable = this.m_exprHost.StateIndicatorsHostsRemotable;
				if (this.m_stateIndicators != null && stateIndicatorsHostsRemotable != null)
				{
					for (int l = 0; l < this.m_stateIndicators.Count; l++)
					{
						StateIndicator stateIndicator = this.m_stateIndicators[l];
						if (stateIndicator != null && stateIndicator.ExpressionHostID > -1)
						{
							stateIndicator.SetExprHost(stateIndicatorsHostsRemotable[stateIndicator.ExpressionHostID], reportObjectModel);
						}
					}
				}
				IList<GaugeImageExprHost> gaugeImagesHostsRemotable = this.m_exprHost.GaugeImagesHostsRemotable;
				if (this.m_gaugeImages != null && gaugeImagesHostsRemotable != null)
				{
					for (int m = 0; m < this.m_gaugeImages.Count; m++)
					{
						GaugeImage gaugeImage = this.m_gaugeImages[m];
						if (gaugeImage != null && gaugeImage.ExpressionHostID > -1)
						{
							gaugeImage.SetExprHost(gaugeImagesHostsRemotable[gaugeImage.ExpressionHostID], reportObjectModel);
						}
					}
				}
				IList<GaugeLabelExprHost> gaugeLabelsHostsRemotable = this.m_exprHost.GaugeLabelsHostsRemotable;
				if (this.m_gaugeLabels != null && gaugeLabelsHostsRemotable != null)
				{
					for (int n = 0; n < this.m_gaugeLabels.Count; n++)
					{
						GaugeLabel gaugeLabel = this.m_gaugeLabels[n];
						if (gaugeLabel != null && gaugeLabel.ExpressionHostID > -1)
						{
							gaugeLabel.SetExprHost(gaugeLabelsHostsRemotable[gaugeLabel.ExpressionHostID], reportObjectModel);
						}
					}
				}
				if (this.m_backFrame != null && this.m_exprHost.BackFrameHost != null)
				{
					this.m_backFrame.SetExprHost(this.m_exprHost.BackFrameHost, reportObjectModel);
				}
				if (this.m_topImage != null && this.m_exprHost.TopImageHost != null)
				{
					this.m_topImage.SetExprHost(this.m_exprHost.TopImageHost, reportObjectModel);
				}
				IList<GaugeCellExprHost> cellHostsRemotable = this.m_exprHost.CellHostsRemotable;
				if (cellHostsRemotable != null && this.GaugeRow != null && cellHostsRemotable.Count > 0 && this.GaugeRow.GaugeCell != null)
				{
					this.GaugeRow.GaugeCell.SetExprHost(cellHostsRemotable[0], reportObjectModel);
				}
			}
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000C0AE1 File Offset: 0x000BECE1
		internal override object EvaluateNoRowsMessageExpression()
		{
			return this.m_exprHost.NoRowsExpr;
		}

		// Token: 0x040016E0 RID: 5856
		private List<LinearGauge> m_linearGauges;

		// Token: 0x040016E1 RID: 5857
		private List<RadialGauge> m_radialGauges;

		// Token: 0x040016E2 RID: 5858
		private List<NumericIndicator> m_numericIndicators;

		// Token: 0x040016E3 RID: 5859
		private List<StateIndicator> m_stateIndicators;

		// Token: 0x040016E4 RID: 5860
		private List<GaugeImage> m_gaugeImages;

		// Token: 0x040016E5 RID: 5861
		private List<GaugeLabel> m_gaugeLabels;

		// Token: 0x040016E6 RID: 5862
		private ExpressionInfo m_antiAliasing;

		// Token: 0x040016E7 RID: 5863
		private ExpressionInfo m_autoLayout;

		// Token: 0x040016E8 RID: 5864
		private BackFrame m_backFrame;

		// Token: 0x040016E9 RID: 5865
		private ExpressionInfo m_shadowIntensity;

		// Token: 0x040016EA RID: 5866
		private ExpressionInfo m_textAntiAliasingQuality;

		// Token: 0x040016EB RID: 5867
		private TopImage m_topImage;

		// Token: 0x040016EC RID: 5868
		private GaugeMemberList m_columnMembers;

		// Token: 0x040016ED RID: 5869
		private GaugeMemberList m_rowMembers;

		// Token: 0x040016EE RID: 5870
		private GaugeRowList m_rows;

		// Token: 0x040016EF RID: 5871
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = GaugePanel.GetDeclaration();

		// Token: 0x040016F0 RID: 5872
		[NonSerialized]
		private GaugePanelExprHost m_exprHost;

		// Token: 0x040016F1 RID: 5873
		[NonSerialized]
		private int m_actionOwnerCounter;
	}
}
