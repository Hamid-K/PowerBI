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
	// Token: 0x020003F5 RID: 1013
	[Serializable]
	internal sealed class ScaleLabels : GaugePanelStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002A89 RID: 10889 RVA: 0x000C5D5F File Offset: 0x000C3F5F
		internal ScaleLabels()
		{
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x000C5D67 File Offset: 0x000C3F67
		internal ScaleLabels(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x06002A8B RID: 10891 RVA: 0x000C5D70 File Offset: 0x000C3F70
		// (set) Token: 0x06002A8C RID: 10892 RVA: 0x000C5D78 File Offset: 0x000C3F78
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

		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x06002A8D RID: 10893 RVA: 0x000C5D81 File Offset: 0x000C3F81
		// (set) Token: 0x06002A8E RID: 10894 RVA: 0x000C5D89 File Offset: 0x000C3F89
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

		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x06002A8F RID: 10895 RVA: 0x000C5D92 File Offset: 0x000C3F92
		// (set) Token: 0x06002A90 RID: 10896 RVA: 0x000C5D9A File Offset: 0x000C3F9A
		internal ExpressionInfo AllowUpsideDown
		{
			get
			{
				return this.m_allowUpsideDown;
			}
			set
			{
				this.m_allowUpsideDown = value;
			}
		}

		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x000C5DA3 File Offset: 0x000C3FA3
		// (set) Token: 0x06002A92 RID: 10898 RVA: 0x000C5DAB File Offset: 0x000C3FAB
		internal ExpressionInfo DistanceFromScale
		{
			get
			{
				return this.m_distanceFromScale;
			}
			set
			{
				this.m_distanceFromScale = value;
			}
		}

		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x06002A93 RID: 10899 RVA: 0x000C5DB4 File Offset: 0x000C3FB4
		// (set) Token: 0x06002A94 RID: 10900 RVA: 0x000C5DBC File Offset: 0x000C3FBC
		internal ExpressionInfo FontAngle
		{
			get
			{
				return this.m_fontAngle;
			}
			set
			{
				this.m_fontAngle = value;
			}
		}

		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x06002A95 RID: 10901 RVA: 0x000C5DC5 File Offset: 0x000C3FC5
		// (set) Token: 0x06002A96 RID: 10902 RVA: 0x000C5DCD File Offset: 0x000C3FCD
		internal ExpressionInfo Placement
		{
			get
			{
				return this.m_placement;
			}
			set
			{
				this.m_placement = value;
			}
		}

		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x06002A97 RID: 10903 RVA: 0x000C5DD6 File Offset: 0x000C3FD6
		// (set) Token: 0x06002A98 RID: 10904 RVA: 0x000C5DDE File Offset: 0x000C3FDE
		internal ExpressionInfo RotateLabels
		{
			get
			{
				return this.m_rotateLabels;
			}
			set
			{
				this.m_rotateLabels = value;
			}
		}

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x06002A99 RID: 10905 RVA: 0x000C5DE7 File Offset: 0x000C3FE7
		// (set) Token: 0x06002A9A RID: 10906 RVA: 0x000C5DEF File Offset: 0x000C3FEF
		internal ExpressionInfo ShowEndLabels
		{
			get
			{
				return this.m_showEndLabels;
			}
			set
			{
				this.m_showEndLabels = value;
			}
		}

		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x06002A9B RID: 10907 RVA: 0x000C5DF8 File Offset: 0x000C3FF8
		// (set) Token: 0x06002A9C RID: 10908 RVA: 0x000C5E00 File Offset: 0x000C4000
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

		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x06002A9D RID: 10909 RVA: 0x000C5E09 File Offset: 0x000C4009
		// (set) Token: 0x06002A9E RID: 10910 RVA: 0x000C5E11 File Offset: 0x000C4011
		internal ExpressionInfo UseFontPercent
		{
			get
			{
				return this.m_useFontPercent;
			}
			set
			{
				this.m_useFontPercent = value;
			}
		}

		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x000C5E1A File Offset: 0x000C401A
		internal string OwnerName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x06002AA0 RID: 10912 RVA: 0x000C5E27 File Offset: 0x000C4027
		internal ScaleLabelsExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x000C5E30 File Offset: 0x000C4030
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ScaleLabelsStart();
			base.Initialize(context);
			if (this.m_interval != null)
			{
				this.m_interval.Initialize("Interval", context);
				context.ExprHostBuilder.ScaleLabelsInterval(this.m_interval);
			}
			if (this.m_intervalOffset != null)
			{
				this.m_intervalOffset.Initialize("IntervalOffset", context);
				context.ExprHostBuilder.ScaleLabelsIntervalOffset(this.m_intervalOffset);
			}
			if (this.m_allowUpsideDown != null)
			{
				this.m_allowUpsideDown.Initialize("AllowUpsideDown", context);
				context.ExprHostBuilder.ScaleLabelsAllowUpsideDown(this.m_allowUpsideDown);
			}
			if (this.m_distanceFromScale != null)
			{
				this.m_distanceFromScale.Initialize("DistanceFromScale", context);
				context.ExprHostBuilder.ScaleLabelsDistanceFromScale(this.m_distanceFromScale);
			}
			if (this.m_fontAngle != null)
			{
				this.m_fontAngle.Initialize("FontAngle", context);
				context.ExprHostBuilder.ScaleLabelsFontAngle(this.m_fontAngle);
			}
			if (this.m_placement != null)
			{
				this.m_placement.Initialize("Placement", context);
				context.ExprHostBuilder.ScaleLabelsPlacement(this.m_placement);
			}
			if (this.m_rotateLabels != null)
			{
				this.m_rotateLabels.Initialize("RotateLabels", context);
				context.ExprHostBuilder.ScaleLabelsRotateLabels(this.m_rotateLabels);
			}
			if (this.m_showEndLabels != null)
			{
				this.m_showEndLabels.Initialize("ShowEndLabels", context);
				context.ExprHostBuilder.ScaleLabelsShowEndLabels(this.m_showEndLabels);
			}
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.ScaleLabelsHidden(this.m_hidden);
			}
			if (this.m_useFontPercent != null)
			{
				this.m_useFontPercent.Initialize("UseFontPercent", context);
				context.ExprHostBuilder.ScaleLabelsUseFontPercent(this.m_useFontPercent);
			}
			context.ExprHostBuilder.ScaleLabelsEnd();
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000C600C File Offset: 0x000C420C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ScaleLabels scaleLabels = (ScaleLabels)base.PublishClone(context);
			if (this.m_interval != null)
			{
				scaleLabels.m_interval = (ExpressionInfo)this.m_interval.PublishClone(context);
			}
			if (this.m_intervalOffset != null)
			{
				scaleLabels.m_intervalOffset = (ExpressionInfo)this.m_intervalOffset.PublishClone(context);
			}
			if (this.m_allowUpsideDown != null)
			{
				scaleLabels.m_allowUpsideDown = (ExpressionInfo)this.m_allowUpsideDown.PublishClone(context);
			}
			if (this.m_distanceFromScale != null)
			{
				scaleLabels.m_distanceFromScale = (ExpressionInfo)this.m_distanceFromScale.PublishClone(context);
			}
			if (this.m_fontAngle != null)
			{
				scaleLabels.m_fontAngle = (ExpressionInfo)this.m_fontAngle.PublishClone(context);
			}
			if (this.m_placement != null)
			{
				scaleLabels.m_placement = (ExpressionInfo)this.m_placement.PublishClone(context);
			}
			if (this.m_rotateLabels != null)
			{
				scaleLabels.m_rotateLabels = (ExpressionInfo)this.m_rotateLabels.PublishClone(context);
			}
			if (this.m_showEndLabels != null)
			{
				scaleLabels.m_showEndLabels = (ExpressionInfo)this.m_showEndLabels.PublishClone(context);
			}
			if (this.m_hidden != null)
			{
				scaleLabels.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
			}
			if (this.m_useFontPercent != null)
			{
				scaleLabels.m_useFontPercent = (ExpressionInfo)this.m_useFontPercent.PublishClone(context);
			}
			return scaleLabels;
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000C615D File Offset: 0x000C435D
		internal void SetExprHost(ScaleLabelsExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000C6184 File Offset: 0x000C4384
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScaleLabels, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Interval, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntervalOffset, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.AllowUpsideDown, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DistanceFromScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.FontAngle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Placement, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.RotateLabels, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ShowEndLabels, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.UseFontPercent, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000C627C File Offset: 0x000C447C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ScaleLabels.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.RotateLabels)
				{
					if (memberName <= MemberName.IntervalOffset)
					{
						if (memberName == MemberName.Interval)
						{
							writer.Write(this.m_interval);
							continue;
						}
						if (memberName == MemberName.IntervalOffset)
						{
							writer.Write(this.m_intervalOffset);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.Hidden)
						{
							writer.Write(this.m_hidden);
							continue;
						}
						if (memberName == MemberName.AllowUpsideDown)
						{
							writer.Write(this.m_allowUpsideDown);
							continue;
						}
						if (memberName == MemberName.RotateLabels)
						{
							writer.Write(this.m_rotateLabels);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.Placement)
				{
					if (memberName == MemberName.DistanceFromScale)
					{
						writer.Write(this.m_distanceFromScale);
						continue;
					}
					if (memberName == MemberName.Placement)
					{
						writer.Write(this.m_placement);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.UseFontPercent)
					{
						writer.Write(this.m_useFontPercent);
						continue;
					}
					if (memberName == MemberName.FontAngle)
					{
						writer.Write(this.m_fontAngle);
						continue;
					}
					if (memberName == MemberName.ShowEndLabels)
					{
						writer.Write(this.m_showEndLabels);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000C63E8 File Offset: 0x000C45E8
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ScaleLabels.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.RotateLabels)
				{
					if (memberName <= MemberName.IntervalOffset)
					{
						if (memberName == MemberName.Interval)
						{
							this.m_interval = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.IntervalOffset)
						{
							this.m_intervalOffset = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.Hidden)
						{
							this.m_hidden = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.AllowUpsideDown)
						{
							this.m_allowUpsideDown = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.RotateLabels)
						{
							this.m_rotateLabels = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.Placement)
				{
					if (memberName == MemberName.DistanceFromScale)
					{
						this.m_distanceFromScale = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Placement)
					{
						this.m_placement = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.UseFontPercent)
					{
						this.m_useFontPercent = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.FontAngle)
					{
						this.m_fontAngle = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.ShowEndLabels)
					{
						this.m_showEndLabels = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000C6596 File Offset: 0x000C4796
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScaleLabels;
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x000C659D File Offset: 0x000C479D
		internal double EvaluateInterval(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleLabelsIntervalExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000C65C3 File Offset: 0x000C47C3
		internal double EvaluateIntervalOffset(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleLabelsIntervalOffsetExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000C65E9 File Offset: 0x000C47E9
		internal bool EvaluateAllowUpsideDown(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleLabelsAllowUpsideDownExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000C660F File Offset: 0x000C480F
		internal double EvaluateDistanceFromScale(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleLabelsDistanceFromScaleExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x000C6635 File Offset: 0x000C4835
		internal double EvaluateFontAngle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleLabelsFontAngleExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x000C665B File Offset: 0x000C485B
		internal GaugeLabelPlacements EvaluatePlacement(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeLabelPlacements(context.ReportRuntime.EvaluateScaleLabelsPlacementExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000C668C File Offset: 0x000C488C
		internal bool EvaluateRotateLabels(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleLabelsRotateLabelsExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000C66B2 File Offset: 0x000C48B2
		internal bool EvaluateShowEndLabels(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleLabelsShowEndLabelsExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000C66D8 File Offset: 0x000C48D8
		internal bool EvaluateHidden(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleLabelsHiddenExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x000C66FE File Offset: 0x000C48FE
		internal bool EvaluateUseFontPercent(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleLabelsUseFontPercentExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x0400175C RID: 5980
		[NonSerialized]
		private ScaleLabelsExprHost m_exprHost;

		// Token: 0x0400175D RID: 5981
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ScaleLabels.GetDeclaration();

		// Token: 0x0400175E RID: 5982
		private ExpressionInfo m_interval;

		// Token: 0x0400175F RID: 5983
		private ExpressionInfo m_intervalOffset;

		// Token: 0x04001760 RID: 5984
		private ExpressionInfo m_allowUpsideDown;

		// Token: 0x04001761 RID: 5985
		private ExpressionInfo m_distanceFromScale;

		// Token: 0x04001762 RID: 5986
		private ExpressionInfo m_fontAngle;

		// Token: 0x04001763 RID: 5987
		private ExpressionInfo m_placement;

		// Token: 0x04001764 RID: 5988
		private ExpressionInfo m_rotateLabels;

		// Token: 0x04001765 RID: 5989
		private ExpressionInfo m_showEndLabels;

		// Token: 0x04001766 RID: 5990
		private ExpressionInfo m_hidden;

		// Token: 0x04001767 RID: 5991
		private ExpressionInfo m_useFontPercent;
	}
}
