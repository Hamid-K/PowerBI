using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003F2 RID: 1010
	[Serializable]
	internal class GaugeScale : GaugePanelStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IActionOwner
	{
		// Token: 0x06002A19 RID: 10777 RVA: 0x000C4066 File Offset: 0x000C2266
		internal GaugeScale()
		{
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x000C406E File Offset: 0x000C226E
		internal GaugeScale(GaugePanel gaugePanel, int id)
			: base(gaugePanel)
		{
			this.m_id = id;
		}

		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x000C407E File Offset: 0x000C227E
		// (set) Token: 0x06002A1C RID: 10780 RVA: 0x000C4086 File Offset: 0x000C2286
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Action Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x000C408F File Offset: 0x000C228F
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x06002A1E RID: 10782 RVA: 0x000C4097 File Offset: 0x000C2297
		// (set) Token: 0x06002A1F RID: 10783 RVA: 0x000C409F File Offset: 0x000C229F
		List<string> IActionOwner.FieldsUsedInValueExpression
		{
			get
			{
				return this.m_fieldsUsedInValueExpression;
			}
			set
			{
				this.m_fieldsUsedInValueExpression = value;
			}
		}

		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x06002A20 RID: 10784 RVA: 0x000C40A8 File Offset: 0x000C22A8
		// (set) Token: 0x06002A21 RID: 10785 RVA: 0x000C40B0 File Offset: 0x000C22B0
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x06002A22 RID: 10786 RVA: 0x000C40B9 File Offset: 0x000C22B9
		internal int ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x06002A23 RID: 10787 RVA: 0x000C40C1 File Offset: 0x000C22C1
		// (set) Token: 0x06002A24 RID: 10788 RVA: 0x000C40C9 File Offset: 0x000C22C9
		internal List<ScaleRange> ScaleRanges
		{
			get
			{
				return this.m_scaleRanges;
			}
			set
			{
				this.m_scaleRanges = value;
			}
		}

		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x06002A25 RID: 10789 RVA: 0x000C40D2 File Offset: 0x000C22D2
		// (set) Token: 0x06002A26 RID: 10790 RVA: 0x000C40DA File Offset: 0x000C22DA
		internal List<CustomLabel> CustomLabels
		{
			get
			{
				return this.m_customLabels;
			}
			set
			{
				this.m_customLabels = value;
			}
		}

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x06002A27 RID: 10791 RVA: 0x000C40E3 File Offset: 0x000C22E3
		// (set) Token: 0x06002A28 RID: 10792 RVA: 0x000C40EB File Offset: 0x000C22EB
		internal ExpressionInfo Interval
		{
			get
			{
				return this.m_interval;
			}
			set
			{
				this.m_interval = value;
			}
		}

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x06002A29 RID: 10793 RVA: 0x000C40F4 File Offset: 0x000C22F4
		// (set) Token: 0x06002A2A RID: 10794 RVA: 0x000C40FC File Offset: 0x000C22FC
		internal ExpressionInfo IntervalOffset
		{
			get
			{
				return this.m_intervalOffset;
			}
			set
			{
				this.m_intervalOffset = value;
			}
		}

		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x000C4105 File Offset: 0x000C2305
		// (set) Token: 0x06002A2C RID: 10796 RVA: 0x000C410D File Offset: 0x000C230D
		internal ExpressionInfo Logarithmic
		{
			get
			{
				return this.m_logarithmic;
			}
			set
			{
				this.m_logarithmic = value;
			}
		}

		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x06002A2D RID: 10797 RVA: 0x000C4116 File Offset: 0x000C2316
		// (set) Token: 0x06002A2E RID: 10798 RVA: 0x000C411E File Offset: 0x000C231E
		internal ExpressionInfo LogarithmicBase
		{
			get
			{
				return this.m_logarithmicBase;
			}
			set
			{
				this.m_logarithmicBase = value;
			}
		}

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x06002A2F RID: 10799 RVA: 0x000C4127 File Offset: 0x000C2327
		// (set) Token: 0x06002A30 RID: 10800 RVA: 0x000C412F File Offset: 0x000C232F
		internal GaugeInputValue MaximumValue
		{
			get
			{
				return this.m_maximumValue;
			}
			set
			{
				this.m_maximumValue = value;
			}
		}

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x06002A31 RID: 10801 RVA: 0x000C4138 File Offset: 0x000C2338
		// (set) Token: 0x06002A32 RID: 10802 RVA: 0x000C4140 File Offset: 0x000C2340
		internal GaugeInputValue MinimumValue
		{
			get
			{
				return this.m_minimumValue;
			}
			set
			{
				this.m_minimumValue = value;
			}
		}

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x06002A33 RID: 10803 RVA: 0x000C4149 File Offset: 0x000C2349
		// (set) Token: 0x06002A34 RID: 10804 RVA: 0x000C4151 File Offset: 0x000C2351
		internal ExpressionInfo Multiplier
		{
			get
			{
				return this.m_multiplier;
			}
			set
			{
				this.m_multiplier = value;
			}
		}

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x06002A35 RID: 10805 RVA: 0x000C415A File Offset: 0x000C235A
		// (set) Token: 0x06002A36 RID: 10806 RVA: 0x000C4162 File Offset: 0x000C2362
		internal ExpressionInfo Reversed
		{
			get
			{
				return this.m_reversed;
			}
			set
			{
				this.m_reversed = value;
			}
		}

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x06002A37 RID: 10807 RVA: 0x000C416B File Offset: 0x000C236B
		// (set) Token: 0x06002A38 RID: 10808 RVA: 0x000C4173 File Offset: 0x000C2373
		internal GaugeTickMarks GaugeMajorTickMarks
		{
			get
			{
				return this.m_gaugeMajorTickMarks;
			}
			set
			{
				this.m_gaugeMajorTickMarks = value;
			}
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x06002A39 RID: 10809 RVA: 0x000C417C File Offset: 0x000C237C
		// (set) Token: 0x06002A3A RID: 10810 RVA: 0x000C4184 File Offset: 0x000C2384
		internal GaugeTickMarks GaugeMinorTickMarks
		{
			get
			{
				return this.m_gaugeMinorTickMarks;
			}
			set
			{
				this.m_gaugeMinorTickMarks = value;
			}
		}

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x06002A3B RID: 10811 RVA: 0x000C418D File Offset: 0x000C238D
		// (set) Token: 0x06002A3C RID: 10812 RVA: 0x000C4195 File Offset: 0x000C2395
		internal ScalePin MaximumPin
		{
			get
			{
				return this.m_maximumPin;
			}
			set
			{
				this.m_maximumPin = value;
			}
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x06002A3D RID: 10813 RVA: 0x000C419E File Offset: 0x000C239E
		// (set) Token: 0x06002A3E RID: 10814 RVA: 0x000C41A6 File Offset: 0x000C23A6
		internal ScalePin MinimumPin
		{
			get
			{
				return this.m_minimumPin;
			}
			set
			{
				this.m_minimumPin = value;
			}
		}

		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x06002A3F RID: 10815 RVA: 0x000C41AF File Offset: 0x000C23AF
		// (set) Token: 0x06002A40 RID: 10816 RVA: 0x000C41B7 File Offset: 0x000C23B7
		internal ScaleLabels ScaleLabels
		{
			get
			{
				return this.m_scaleLabels;
			}
			set
			{
				this.m_scaleLabels = value;
			}
		}

		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x06002A41 RID: 10817 RVA: 0x000C41C0 File Offset: 0x000C23C0
		// (set) Token: 0x06002A42 RID: 10818 RVA: 0x000C41C8 File Offset: 0x000C23C8
		internal ExpressionInfo TickMarksOnTop
		{
			get
			{
				return this.m_tickMarksOnTop;
			}
			set
			{
				this.m_tickMarksOnTop = value;
			}
		}

		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x06002A43 RID: 10819 RVA: 0x000C41D1 File Offset: 0x000C23D1
		// (set) Token: 0x06002A44 RID: 10820 RVA: 0x000C41D9 File Offset: 0x000C23D9
		internal ExpressionInfo ToolTip
		{
			get
			{
				return this.m_toolTip;
			}
			set
			{
				this.m_toolTip = value;
			}
		}

		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x06002A45 RID: 10821 RVA: 0x000C41E2 File Offset: 0x000C23E2
		// (set) Token: 0x06002A46 RID: 10822 RVA: 0x000C41EA File Offset: 0x000C23EA
		internal ExpressionInfo Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x06002A47 RID: 10823 RVA: 0x000C41F3 File Offset: 0x000C23F3
		// (set) Token: 0x06002A48 RID: 10824 RVA: 0x000C41FB File Offset: 0x000C23FB
		internal ExpressionInfo Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x06002A49 RID: 10825 RVA: 0x000C4204 File Offset: 0x000C2404
		internal string OwnerName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x06002A4A RID: 10826 RVA: 0x000C4211 File Offset: 0x000C2411
		internal GaugeScaleExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x000C4219 File Offset: 0x000C2419
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000C4224 File Offset: 0x000C2424
		internal override void Initialize(InitializationContext context)
		{
			base.Initialize(context);
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_scaleRanges != null)
			{
				for (int i = 0; i < this.m_scaleRanges.Count; i++)
				{
					this.m_scaleRanges[i].Initialize(context);
				}
			}
			if (this.m_customLabels != null)
			{
				for (int j = 0; j < this.m_customLabels.Count; j++)
				{
					this.m_customLabels[j].Initialize(context);
				}
			}
			if (this.m_interval != null)
			{
				this.m_interval.Initialize("Interval", context);
				context.ExprHostBuilder.GaugeScaleInterval(this.m_interval);
			}
			if (this.m_intervalOffset != null)
			{
				this.m_intervalOffset.Initialize("IntervalOffset", context);
				context.ExprHostBuilder.GaugeScaleIntervalOffset(this.m_intervalOffset);
			}
			if (this.m_logarithmic != null)
			{
				this.m_logarithmic.Initialize("Logarithmic", context);
				context.ExprHostBuilder.GaugeScaleLogarithmic(this.m_logarithmic);
			}
			if (this.m_logarithmicBase != null)
			{
				this.m_logarithmicBase.Initialize("LogarithmicBase", context);
				context.ExprHostBuilder.GaugeScaleLogarithmicBase(this.m_logarithmicBase);
			}
			if (this.m_multiplier != null)
			{
				this.m_multiplier.Initialize("Multiplier", context);
				context.ExprHostBuilder.GaugeScaleMultiplier(this.m_multiplier);
			}
			if (this.m_reversed != null)
			{
				this.m_reversed.Initialize("Reversed", context);
				context.ExprHostBuilder.GaugeScaleReversed(this.m_reversed);
			}
			if (this.m_gaugeMajorTickMarks != null)
			{
				this.m_gaugeMajorTickMarks.Initialize(context, true);
			}
			if (this.m_gaugeMinorTickMarks != null)
			{
				this.m_gaugeMinorTickMarks.Initialize(context, false);
			}
			if (this.m_maximumPin != null)
			{
				this.m_maximumPin.Initialize(context, true);
			}
			if (this.m_minimumPin != null)
			{
				this.m_minimumPin.Initialize(context, false);
			}
			if (this.m_scaleLabels != null)
			{
				this.m_scaleLabels.Initialize(context);
			}
			if (this.m_tickMarksOnTop != null)
			{
				this.m_tickMarksOnTop.Initialize("TickMarksOnTop", context);
				context.ExprHostBuilder.GaugeScaleTickMarksOnTop(this.m_tickMarksOnTop);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.GaugeScaleToolTip(this.m_toolTip);
			}
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.GaugeScaleHidden(this.m_hidden);
			}
			if (this.m_width != null)
			{
				this.m_width.Initialize("Width", context);
				context.ExprHostBuilder.GaugeScaleWidth(this.m_width);
			}
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000C44C4 File Offset: 0x000C26C4
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			GaugeScale gaugeScale = (GaugeScale)base.PublishClone(context);
			if (this.m_action != null)
			{
				gaugeScale.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_scaleRanges != null)
			{
				gaugeScale.m_scaleRanges = new List<ScaleRange>(this.m_scaleRanges.Count);
				foreach (ScaleRange scaleRange in this.m_scaleRanges)
				{
					gaugeScale.m_scaleRanges.Add((ScaleRange)scaleRange.PublishClone(context));
				}
			}
			if (this.m_customLabels != null)
			{
				gaugeScale.m_customLabels = new List<CustomLabel>(this.m_customLabels.Count);
				foreach (CustomLabel customLabel in this.m_customLabels)
				{
					gaugeScale.m_customLabels.Add((CustomLabel)customLabel.PublishClone(context));
				}
			}
			if (this.m_interval != null)
			{
				gaugeScale.m_interval = (ExpressionInfo)this.m_interval.PublishClone(context);
			}
			if (this.m_intervalOffset != null)
			{
				gaugeScale.m_intervalOffset = (ExpressionInfo)this.m_intervalOffset.PublishClone(context);
			}
			if (this.m_logarithmic != null)
			{
				gaugeScale.m_logarithmic = (ExpressionInfo)this.m_logarithmic.PublishClone(context);
			}
			if (this.m_logarithmicBase != null)
			{
				gaugeScale.m_logarithmicBase = (ExpressionInfo)this.m_logarithmicBase.PublishClone(context);
			}
			if (this.m_maximumValue != null)
			{
				gaugeScale.m_maximumValue = (GaugeInputValue)this.m_maximumValue.PublishClone(context);
			}
			if (this.m_minimumValue != null)
			{
				gaugeScale.m_minimumValue = (GaugeInputValue)this.m_minimumValue.PublishClone(context);
			}
			if (this.m_multiplier != null)
			{
				gaugeScale.m_multiplier = (ExpressionInfo)this.m_multiplier.PublishClone(context);
			}
			if (this.m_reversed != null)
			{
				gaugeScale.m_reversed = (ExpressionInfo)this.m_reversed.PublishClone(context);
			}
			if (this.m_gaugeMajorTickMarks != null)
			{
				gaugeScale.m_gaugeMajorTickMarks = (GaugeTickMarks)this.m_gaugeMajorTickMarks.PublishClone(context);
			}
			if (this.m_gaugeMinorTickMarks != null)
			{
				gaugeScale.m_gaugeMinorTickMarks = (GaugeTickMarks)this.m_gaugeMinorTickMarks.PublishClone(context);
			}
			if (this.m_maximumPin != null)
			{
				gaugeScale.m_maximumPin = (ScalePin)this.m_maximumPin.PublishClone(context);
			}
			if (this.m_minimumPin != null)
			{
				gaugeScale.m_minimumPin = (ScalePin)this.m_minimumPin.PublishClone(context);
			}
			if (this.m_scaleLabels != null)
			{
				gaugeScale.m_scaleLabels = (ScaleLabels)this.m_scaleLabels.PublishClone(context);
			}
			if (this.m_tickMarksOnTop != null)
			{
				gaugeScale.m_tickMarksOnTop = (ExpressionInfo)this.m_tickMarksOnTop.PublishClone(context);
			}
			if (this.m_toolTip != null)
			{
				gaugeScale.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			if (this.m_hidden != null)
			{
				gaugeScale.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
			}
			if (this.m_width != null)
			{
				gaugeScale.m_width = (ExpressionInfo)this.m_width.PublishClone(context);
			}
			return gaugeScale;
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000C47F4 File Offset: 0x000C29F4
		internal void SetExprHost(GaugeScaleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			IList<ScaleRangeExprHost> scaleRangesHostsRemotable = this.m_exprHost.ScaleRangesHostsRemotable;
			if (this.m_scaleRanges != null && scaleRangesHostsRemotable != null)
			{
				for (int i = 0; i < this.m_scaleRanges.Count; i++)
				{
					ScaleRange scaleRange = this.m_scaleRanges[i];
					if (scaleRange != null && scaleRange.ExpressionHostID > -1)
					{
						scaleRange.SetExprHost(scaleRangesHostsRemotable[scaleRange.ExpressionHostID], reportObjectModel);
					}
				}
			}
			IList<CustomLabelExprHost> customLabelsHostsRemotable = this.m_exprHost.CustomLabelsHostsRemotable;
			if (this.m_customLabels != null && customLabelsHostsRemotable != null)
			{
				for (int j = 0; j < this.m_customLabels.Count; j++)
				{
					CustomLabel customLabel = this.m_customLabels[j];
					if (customLabel != null && customLabel.ExpressionHostID > -1)
					{
						customLabel.SetExprHost(customLabelsHostsRemotable[customLabel.ExpressionHostID], reportObjectModel);
					}
				}
			}
			if (this.m_gaugeMajorTickMarks != null && this.m_exprHost.GaugeMajorTickMarksHost != null)
			{
				this.m_gaugeMajorTickMarks.SetExprHost(this.m_exprHost.GaugeMajorTickMarksHost, reportObjectModel);
			}
			if (this.m_gaugeMinorTickMarks != null && this.m_exprHost.GaugeMinorTickMarksHost != null)
			{
				this.m_gaugeMinorTickMarks.SetExprHost(this.m_exprHost.GaugeMinorTickMarksHost, reportObjectModel);
			}
			if (this.m_maximumPin != null && this.m_exprHost.MaximumPinHost != null)
			{
				this.m_maximumPin.SetExprHost(this.m_exprHost.MaximumPinHost, reportObjectModel);
			}
			if (this.m_minimumPin != null && this.m_exprHost.MinimumPinHost != null)
			{
				this.m_minimumPin.SetExprHost(this.m_exprHost.MinimumPinHost, reportObjectModel);
			}
			if (this.m_scaleLabels != null && this.m_exprHost.ScaleLabelsHost != null)
			{
				this.m_scaleLabels.SetExprHost(this.m_exprHost.ScaleLabelsHost, reportObjectModel);
			}
			if (this.m_action != null && exprHost.ActionInfoHost != null)
			{
				this.m_action.SetExprHost(exprHost.ActionInfoHost, reportObjectModel);
			}
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000C49E0 File Offset: 0x000C2BE0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.ScaleRanges, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScaleRange),
				new MemberInfo(MemberName.CustomLabels, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CustomLabel),
				new MemberInfo(MemberName.Interval, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntervalOffset, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Logarithmic, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LogarithmicBase, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MaximumValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeInputValue),
				new MemberInfo(MemberName.MinimumValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeInputValue),
				new MemberInfo(MemberName.Multiplier, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Reversed, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.GaugeMajorTickMarks, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeTickMarks),
				new MemberInfo(MemberName.GaugeMinorTickMarks, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeTickMarks),
				new MemberInfo(MemberName.MaximumPin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalePin),
				new MemberInfo(MemberName.MinimumPin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalePin),
				new MemberInfo(MemberName.ScaleLabels, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScaleLabels),
				new MemberInfo(MemberName.TickMarksOnTop, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Width, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.ID, Token.Int32)
			});
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000C4BE4 File Offset: 0x000C2DE4
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(GaugeScale.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Interval)
				{
					if (memberName <= MemberName.Name)
					{
						if (memberName == MemberName.ID)
						{
							writer.Write(this.m_id);
							continue;
						}
						if (memberName == MemberName.Name)
						{
							writer.Write(this.m_name);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.Width)
						{
							writer.Write(this.m_width);
							continue;
						}
						if (memberName == MemberName.ToolTip)
						{
							writer.Write(this.m_toolTip);
							continue;
						}
						if (memberName == MemberName.Interval)
						{
							writer.Write(this.m_interval);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.ExprHostID)
				{
					if (memberName == MemberName.IntervalOffset)
					{
						writer.Write(this.m_intervalOffset);
						continue;
					}
					if (memberName == MemberName.Hidden)
					{
						writer.Write(this.m_hidden);
						continue;
					}
					if (memberName == MemberName.ExprHostID)
					{
						writer.Write(this.m_exprHostID);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Action)
					{
						writer.Write(this.m_action);
						continue;
					}
					if (memberName == MemberName.Reversed)
					{
						writer.Write(this.m_reversed);
						continue;
					}
					switch (memberName)
					{
					case MemberName.ScaleRanges:
						writer.Write<ScaleRange>(this.m_scaleRanges);
						continue;
					case MemberName.CustomLabels:
						writer.Write<CustomLabel>(this.m_customLabels);
						continue;
					case MemberName.Logarithmic:
						writer.Write(this.m_logarithmic);
						continue;
					case MemberName.LogarithmicBase:
						writer.Write(this.m_logarithmicBase);
						continue;
					case MemberName.MaximumValue:
						writer.Write(this.m_maximumValue);
						continue;
					case MemberName.MinimumValue:
						writer.Write(this.m_minimumValue);
						continue;
					case MemberName.Multiplier:
						writer.Write(this.m_multiplier);
						continue;
					case MemberName.GaugeMajorTickMarks:
						writer.Write(this.m_gaugeMajorTickMarks);
						continue;
					case MemberName.GaugeMinorTickMarks:
						writer.Write(this.m_gaugeMinorTickMarks);
						continue;
					case MemberName.MaximumPin:
						writer.Write(this.m_maximumPin);
						continue;
					case MemberName.MinimumPin:
						writer.Write(this.m_minimumPin);
						continue;
					case MemberName.ScaleLabels:
						writer.Write(this.m_scaleLabels);
						continue;
					case MemberName.TickMarksOnTop:
						writer.Write(this.m_tickMarksOnTop);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x000C4E80 File Offset: 0x000C3080
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(GaugeScale.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Interval)
				{
					if (memberName <= MemberName.Name)
					{
						if (memberName == MemberName.ID)
						{
							this.m_id = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.Name)
						{
							this.m_name = reader.ReadString();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.Width)
						{
							this.m_width = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.ToolTip)
						{
							this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.Interval)
						{
							this.m_interval = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.ExprHostID)
				{
					if (memberName == MemberName.IntervalOffset)
					{
						this.m_intervalOffset = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Hidden)
					{
						this.m_hidden = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.ExprHostID)
					{
						this.m_exprHostID = reader.ReadInt32();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Action)
					{
						this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Reversed)
					{
						this.m_reversed = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					switch (memberName)
					{
					case MemberName.ScaleRanges:
						this.m_scaleRanges = reader.ReadGenericListOfRIFObjects<ScaleRange>();
						continue;
					case MemberName.CustomLabels:
						this.m_customLabels = reader.ReadGenericListOfRIFObjects<CustomLabel>();
						continue;
					case MemberName.Logarithmic:
						this.m_logarithmic = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.LogarithmicBase:
						this.m_logarithmicBase = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.MaximumValue:
						this.m_maximumValue = (GaugeInputValue)reader.ReadRIFObject();
						continue;
					case MemberName.MinimumValue:
						this.m_minimumValue = (GaugeInputValue)reader.ReadRIFObject();
						continue;
					case MemberName.Multiplier:
						this.m_multiplier = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.GaugeMajorTickMarks:
						this.m_gaugeMajorTickMarks = (GaugeTickMarks)reader.ReadRIFObject();
						continue;
					case MemberName.GaugeMinorTickMarks:
						this.m_gaugeMinorTickMarks = (GaugeTickMarks)reader.ReadRIFObject();
						continue;
					case MemberName.MaximumPin:
						this.m_maximumPin = (ScalePin)reader.ReadRIFObject();
						continue;
					case MemberName.MinimumPin:
						this.m_minimumPin = (ScalePin)reader.ReadRIFObject();
						continue;
					case MemberName.ScaleLabels:
						this.m_scaleLabels = (ScaleLabels)reader.ReadRIFObject();
						continue;
					case MemberName.TickMarksOnTop:
						this.m_tickMarksOnTop = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000C5178 File Offset: 0x000C3378
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
			if (this.m_id == 0)
			{
				this.m_id = this.m_gaugePanel.GenerateActionOwnerID();
			}
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000C519B File Offset: 0x000C339B
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeScale;
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000C51A2 File Offset: 0x000C33A2
		internal double EvaluateInterval(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeScaleIntervalExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000C51C8 File Offset: 0x000C33C8
		internal double EvaluateIntervalOffset(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeScaleIntervalOffsetExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000C51EE File Offset: 0x000C33EE
		internal bool EvaluateLogarithmic(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeScaleLogarithmicExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x000C5214 File Offset: 0x000C3414
		internal double EvaluateLogarithmicBase(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeScaleLogarithmicBaseExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x000C523A File Offset: 0x000C343A
		internal double EvaluateMultiplier(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeScaleMultiplierExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x000C5260 File Offset: 0x000C3460
		internal bool EvaluateReversed(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeScaleReversedExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000C5286 File Offset: 0x000C3486
		internal bool EvaluateTickMarksOnTop(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeScaleTickMarksOnTopExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x000C52AC File Offset: 0x000C34AC
		internal string EvaluateToolTip(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeScaleToolTipExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x000C52D2 File Offset: 0x000C34D2
		internal bool EvaluateHidden(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeScaleHiddenExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x000C52F8 File Offset: 0x000C34F8
		internal double EvaluateWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeScaleWidthExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x04001738 RID: 5944
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001739 RID: 5945
		protected int m_exprHostID;

		// Token: 0x0400173A RID: 5946
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x0400173B RID: 5947
		[NonSerialized]
		protected GaugeScaleExprHost m_exprHost;

		// Token: 0x0400173C RID: 5948
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = GaugeScale.GetDeclaration();

		// Token: 0x0400173D RID: 5949
		protected string m_name;

		// Token: 0x0400173E RID: 5950
		private List<ScaleRange> m_scaleRanges;

		// Token: 0x0400173F RID: 5951
		private List<CustomLabel> m_customLabels;

		// Token: 0x04001740 RID: 5952
		private ExpressionInfo m_interval;

		// Token: 0x04001741 RID: 5953
		private ExpressionInfo m_intervalOffset;

		// Token: 0x04001742 RID: 5954
		private ExpressionInfo m_logarithmic;

		// Token: 0x04001743 RID: 5955
		private ExpressionInfo m_logarithmicBase;

		// Token: 0x04001744 RID: 5956
		private GaugeInputValue m_maximumValue;

		// Token: 0x04001745 RID: 5957
		private GaugeInputValue m_minimumValue;

		// Token: 0x04001746 RID: 5958
		private ExpressionInfo m_multiplier;

		// Token: 0x04001747 RID: 5959
		private ExpressionInfo m_reversed;

		// Token: 0x04001748 RID: 5960
		private GaugeTickMarks m_gaugeMajorTickMarks;

		// Token: 0x04001749 RID: 5961
		private GaugeTickMarks m_gaugeMinorTickMarks;

		// Token: 0x0400174A RID: 5962
		private ScalePin m_maximumPin;

		// Token: 0x0400174B RID: 5963
		private ScalePin m_minimumPin;

		// Token: 0x0400174C RID: 5964
		private ScaleLabels m_scaleLabels;

		// Token: 0x0400174D RID: 5965
		private ExpressionInfo m_tickMarksOnTop;

		// Token: 0x0400174E RID: 5966
		private ExpressionInfo m_toolTip;

		// Token: 0x0400174F RID: 5967
		private ExpressionInfo m_hidden;

		// Token: 0x04001750 RID: 5968
		private ExpressionInfo m_width;

		// Token: 0x04001751 RID: 5969
		private int m_id;
	}
}
