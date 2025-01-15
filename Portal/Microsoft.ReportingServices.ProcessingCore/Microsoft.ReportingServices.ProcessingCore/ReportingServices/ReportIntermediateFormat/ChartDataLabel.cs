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
	// Token: 0x0200049B RID: 1179
	[Serializable]
	internal sealed class ChartDataLabel : ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060038BF RID: 14527 RVA: 0x000F7087 File Offset: 0x000F5287
		internal ChartDataLabel()
		{
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x000F708F File Offset: 0x000F528F
		internal ChartDataLabel(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint chartDataPoint)
			: base(chart)
		{
			this.m_chartDataPoint = chartDataPoint;
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x000F709F File Offset: 0x000F529F
		internal ChartDataLabel(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, ChartSeries chartSeries)
			: base(chart)
		{
			this.m_chartSeries = chartSeries;
		}

		// Token: 0x170018C1 RID: 6337
		// (get) Token: 0x060038C2 RID: 14530 RVA: 0x000F70AF File Offset: 0x000F52AF
		// (set) Token: 0x060038C3 RID: 14531 RVA: 0x000F70B7 File Offset: 0x000F52B7
		internal ExpressionInfo Visible
		{
			get
			{
				return this.m_visible;
			}
			set
			{
				this.m_visible = value;
			}
		}

		// Token: 0x170018C2 RID: 6338
		// (get) Token: 0x060038C4 RID: 14532 RVA: 0x000F70C0 File Offset: 0x000F52C0
		// (set) Token: 0x060038C5 RID: 14533 RVA: 0x000F70C8 File Offset: 0x000F52C8
		internal ExpressionInfo Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x170018C3 RID: 6339
		// (get) Token: 0x060038C6 RID: 14534 RVA: 0x000F70D1 File Offset: 0x000F52D1
		// (set) Token: 0x060038C7 RID: 14535 RVA: 0x000F70D9 File Offset: 0x000F52D9
		internal ExpressionInfo UseValueAsLabel
		{
			get
			{
				return this.m_useValueAsLabel;
			}
			set
			{
				this.m_useValueAsLabel = value;
			}
		}

		// Token: 0x170018C4 RID: 6340
		// (get) Token: 0x060038C8 RID: 14536 RVA: 0x000F70E2 File Offset: 0x000F52E2
		// (set) Token: 0x060038C9 RID: 14537 RVA: 0x000F70EA File Offset: 0x000F52EA
		internal ExpressionInfo Position
		{
			get
			{
				return this.m_position;
			}
			set
			{
				this.m_position = value;
			}
		}

		// Token: 0x170018C5 RID: 6341
		// (get) Token: 0x060038CA RID: 14538 RVA: 0x000F70F3 File Offset: 0x000F52F3
		// (set) Token: 0x060038CB RID: 14539 RVA: 0x000F70FB File Offset: 0x000F52FB
		internal ExpressionInfo Rotation
		{
			get
			{
				return this.m_rotation;
			}
			set
			{
				this.m_rotation = value;
			}
		}

		// Token: 0x170018C6 RID: 6342
		// (get) Token: 0x060038CC RID: 14540 RVA: 0x000F7104 File Offset: 0x000F5304
		// (set) Token: 0x060038CD RID: 14541 RVA: 0x000F710C File Offset: 0x000F530C
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

		// Token: 0x170018C7 RID: 6343
		// (get) Token: 0x060038CE RID: 14542 RVA: 0x000F7115 File Offset: 0x000F5315
		// (set) Token: 0x060038CF RID: 14543 RVA: 0x000F711D File Offset: 0x000F531D
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

		// Token: 0x170018C8 RID: 6344
		// (get) Token: 0x060038D0 RID: 14544 RVA: 0x000F7126 File Offset: 0x000F5326
		internal ChartDataLabelExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170018C9 RID: 6345
		// (get) Token: 0x060038D1 RID: 14545 RVA: 0x000F712E File Offset: 0x000F532E
		public override IInstancePath InstancePath
		{
			get
			{
				if (this.m_chartDataPoint != null)
				{
					return this.m_chartDataPoint;
				}
				if (this.m_chartSeries != null)
				{
					return this.m_chartSeries;
				}
				return base.InstancePath;
			}
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x000F7154 File Offset: 0x000F5354
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel chartDataLabel = (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel)base.PublishClone(context);
			if (this.m_label != null)
			{
				chartDataLabel.m_label = (ExpressionInfo)this.m_label.PublishClone(context);
			}
			if (this.m_visible != null)
			{
				chartDataLabel.m_visible = (ExpressionInfo)this.m_visible.PublishClone(context);
			}
			if (this.m_position != null)
			{
				chartDataLabel.m_position = (ExpressionInfo)this.m_position.PublishClone(context);
			}
			if (this.m_rotation != null)
			{
				chartDataLabel.m_rotation = (ExpressionInfo)this.m_rotation.PublishClone(context);
			}
			if (this.m_useValueAsLabel != null)
			{
				chartDataLabel.m_useValueAsLabel = (ExpressionInfo)this.m_useValueAsLabel.PublishClone(context);
			}
			if (this.m_action != null)
			{
				chartDataLabel.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_toolTip != null)
			{
				chartDataLabel.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			return chartDataLabel;
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x000F7248 File Offset: 0x000F5448
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.DataLabelStart();
			base.Initialize(context);
			if (this.m_label != null)
			{
				this.m_label.Initialize("Label", context);
				context.ExprHostBuilder.DataLabelLabel(this.m_label);
			}
			if (this.m_visible != null)
			{
				this.m_visible.Initialize("Visible", context);
				context.ExprHostBuilder.DataLabelVisible(this.m_visible);
			}
			if (this.m_position != null)
			{
				this.m_position.Initialize("Position", context);
				context.ExprHostBuilder.DataLabelPosition(this.m_position);
			}
			if (this.m_rotation != null)
			{
				this.m_rotation.Initialize("Rotation", context);
				context.ExprHostBuilder.DataLabelRotation(this.m_rotation);
			}
			if (this.m_useValueAsLabel != null)
			{
				this.m_useValueAsLabel.Initialize("UseValueAsLabel", context);
				context.ExprHostBuilder.DataLabelUseValueAsLabel(this.m_useValueAsLabel);
			}
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.ChartDataLabelToolTip(this.m_toolTip);
			}
			context.ExprHostBuilder.DataLabelEnd();
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x000F738C File Offset: 0x000F558C
		internal void SetExprHost(ChartDataLabelExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(null != exprHost && null != reportObjectModel)");
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_action != null && this.m_exprHost.ActionInfoHost != null)
			{
				this.m_action.SetExprHost(this.m_exprHost.ActionInfoHost, reportObjectModel);
			}
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x000F73F0 File Offset: 0x000F55F0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDataLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Visible, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Label, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.UseValueAsLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Position, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Rotation, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.ChartDataPoint, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDataPoint, Token.Reference),
				new MemberInfo(MemberName.ChartSeries, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartSeries, Token.Reference),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000F74D4 File Offset: 0x000F56D4
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Visible)
				{
					if (memberName <= MemberName.Position)
					{
						if (memberName == MemberName.Label)
						{
							writer.Write(this.m_label);
							continue;
						}
						if (memberName == MemberName.Position)
						{
							writer.Write(this.m_position);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ToolTip)
						{
							writer.Write(this.m_toolTip);
							continue;
						}
						if (memberName == MemberName.Visible)
						{
							writer.Write(this.m_visible);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.Rotation)
				{
					if (memberName == MemberName.Action)
					{
						writer.Write(this.m_action);
						continue;
					}
					if (memberName == MemberName.Rotation)
					{
						writer.Write(this.m_rotation);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.ChartSeries)
					{
						writer.WriteReference(this.m_chartSeries);
						continue;
					}
					if (memberName == MemberName.ChartDataPoint)
					{
						writer.WriteReference(this.m_chartDataPoint);
						continue;
					}
					if (memberName == MemberName.UseValueAsLabel)
					{
						writer.Write(this.m_useValueAsLabel);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x000F7624 File Offset: 0x000F5824
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Visible)
				{
					if (memberName <= MemberName.Position)
					{
						if (memberName == MemberName.Label)
						{
							this.m_label = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.Position)
						{
							this.m_position = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ToolTip)
						{
							this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.Visible)
						{
							this.m_visible = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.Rotation)
				{
					if (memberName == MemberName.Action)
					{
						this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Rotation)
					{
						this.m_rotation = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.ChartSeries)
					{
						this.m_chartSeries = reader.ReadReference<ChartSeries>(this);
						continue;
					}
					if (memberName == MemberName.ChartDataPoint)
					{
						this.m_chartDataPoint = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint>(this);
						continue;
					}
					if (memberName == MemberName.UseValueAsLabel)
					{
						this.m_useValueAsLabel = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x000F77A8 File Offset: 0x000F59A8
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName != MemberName.ChartSeries)
					{
						if (memberName == MemberName.ChartDataPoint)
						{
							Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
							this.m_chartDataPoint = (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint)referenceableItems[memberReference.RefID];
						}
						else
						{
							Global.Tracer.Assert(false);
						}
					}
					else
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_chartSeries = (ChartSeries)referenceableItems[memberReference.RefID];
					}
				}
			}
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x000F7894 File Offset: 0x000F5A94
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDataLabel;
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x000F789B File Offset: 0x000F5A9B
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateLabel(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataLabelLabelExpression(this, base.Name);
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x000F78BC File Offset: 0x000F5ABC
		internal ChartDataLabelPositions EvaluatePosition(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return EnumTranslator.TranslateChartDataLabelPosition(context.ReportRuntime.EvaluateChartDataLabePositionExpression(this, base.Name), context.ReportRuntime);
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x000F78E8 File Offset: 0x000F5AE8
		internal int EvaluateRotation(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataLabelRotationExpression(this, base.Name);
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x000F7909 File Offset: 0x000F5B09
		internal bool EvaluateUseValueAsLabel(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataLabelUseValueAsLabelExpression(this, base.Name);
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x000F792A File Offset: 0x000F5B2A
		internal bool EvaluateVisible(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataLabelVisibleExpression(this, base.Name);
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x000F794C File Offset: 0x000F5B4C
		internal string GetFormattedValue(Microsoft.ReportingServices.RdlExpressions.VariantResult originalValue, IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			if (originalValue.ErrorOccurred)
			{
				return RPRes.rsExpressionErrorValue;
			}
			if (originalValue.Value != null)
			{
				return Formatter.Format(originalValue.Value, ref this.m_formatter, this.m_chart.StyleClass, this.m_styleClass, context, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, this.m_chart.Name);
			}
			return null;
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x000F79B0 File Offset: 0x000F5BB0
		internal string EvaluateToolTip(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = context.ReportRuntime.EvaluateChartDataLabelToolTipExpression(this, this.m_chart.Name);
			string text = null;
			if (variantResult.ErrorOccurred)
			{
				text = RPRes.rsExpressionErrorValue;
			}
			else if (variantResult.Value != null)
			{
				text = Formatter.Format(variantResult.Value, ref this.m_formatter, this.m_chart.StyleClass, this.m_styleClass, context, base.ObjectType, base.Name);
			}
			return text;
		}

		// Token: 0x04001B5D RID: 7005
		private ExpressionInfo m_visible;

		// Token: 0x04001B5E RID: 7006
		private ExpressionInfo m_label;

		// Token: 0x04001B5F RID: 7007
		private ExpressionInfo m_position;

		// Token: 0x04001B60 RID: 7008
		private ExpressionInfo m_rotation;

		// Token: 0x04001B61 RID: 7009
		private ExpressionInfo m_useValueAsLabel;

		// Token: 0x04001B62 RID: 7010
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001B63 RID: 7011
		private ExpressionInfo m_toolTip;

		// Token: 0x04001B64 RID: 7012
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint m_chartDataPoint;

		// Token: 0x04001B65 RID: 7013
		[Reference]
		private ChartSeries m_chartSeries;

		// Token: 0x04001B66 RID: 7014
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel.GetDeclaration();

		// Token: 0x04001B67 RID: 7015
		[NonSerialized]
		private Formatter m_formatter;

		// Token: 0x04001B68 RID: 7016
		[NonSerialized]
		private ChartDataLabelExprHost m_exprHost;
	}
}
