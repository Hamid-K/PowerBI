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
	// Token: 0x02000499 RID: 1177
	[Serializable]
	internal sealed class ChartItemInLegend : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IActionOwner
	{
		// Token: 0x0600388F RID: 14479 RVA: 0x000F645C File Offset: 0x000F465C
		internal ChartItemInLegend()
		{
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x000F6464 File Offset: 0x000F4664
		internal ChartItemInLegend(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint chartDataPoint)
		{
			this.m_chart = chart;
			this.m_chartDataPoint = chartDataPoint;
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x000F647A File Offset: 0x000F467A
		internal ChartItemInLegend(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, ChartSeries chartSeries)
		{
			this.m_chart = chart;
			this.m_chartSeries = chartSeries;
		}

		// Token: 0x170018B5 RID: 6325
		// (get) Token: 0x06003892 RID: 14482 RVA: 0x000F6490 File Offset: 0x000F4690
		internal ChartDataPointInLegendExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170018B6 RID: 6326
		// (get) Token: 0x06003893 RID: 14483 RVA: 0x000F6498 File Offset: 0x000F4698
		// (set) Token: 0x06003894 RID: 14484 RVA: 0x000F64A0 File Offset: 0x000F46A0
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

		// Token: 0x170018B7 RID: 6327
		// (get) Token: 0x06003895 RID: 14485 RVA: 0x000F64A9 File Offset: 0x000F46A9
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x170018B8 RID: 6328
		// (get) Token: 0x06003896 RID: 14486 RVA: 0x000F64B1 File Offset: 0x000F46B1
		// (set) Token: 0x06003897 RID: 14487 RVA: 0x000F64B9 File Offset: 0x000F46B9
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

		// Token: 0x170018B9 RID: 6329
		// (get) Token: 0x06003898 RID: 14488 RVA: 0x000F64C2 File Offset: 0x000F46C2
		// (set) Token: 0x06003899 RID: 14489 RVA: 0x000F64CA File Offset: 0x000F46CA
		internal ExpressionInfo LegendText
		{
			get
			{
				return this.m_legendText;
			}
			set
			{
				this.m_legendText = value;
			}
		}

		// Token: 0x170018BA RID: 6330
		// (get) Token: 0x0600389A RID: 14490 RVA: 0x000F64D3 File Offset: 0x000F46D3
		// (set) Token: 0x0600389B RID: 14491 RVA: 0x000F64DB File Offset: 0x000F46DB
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

		// Token: 0x170018BB RID: 6331
		// (get) Token: 0x0600389C RID: 14492 RVA: 0x000F64E4 File Offset: 0x000F46E4
		// (set) Token: 0x0600389D RID: 14493 RVA: 0x000F64EC File Offset: 0x000F46EC
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

		// Token: 0x170018BC RID: 6332
		// (get) Token: 0x0600389E RID: 14494 RVA: 0x000F64F5 File Offset: 0x000F46F5
		private IInstancePath InstancePath
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
				return this.m_chart;
			}
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000F651C File Offset: 0x000F471C
		internal void SetExprHost(ChartDataPointInLegendExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_action != null && this.m_exprHost.ActionInfoHost != null)
			{
				this.m_action.SetExprHost(this.m_exprHost.ActionInfoHost, reportObjectModel);
			}
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000F6584 File Offset: 0x000F4784
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartItemInLegendStart();
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_legendText != null)
			{
				this.m_legendText.Initialize("LegendText", context);
				context.ExprHostBuilder.ChartItemInLegendLegendText(this.m_legendText);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.ChartItemInLegendToolTip(this.m_toolTip);
			}
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.ChartItemInLegendHidden(this.m_hidden);
			}
			context.ExprHostBuilder.ChartItemInLegendEnd();
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x000F6640 File Offset: 0x000F4840
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			ChartItemInLegend chartItemInLegend = (ChartItemInLegend)base.MemberwiseClone();
			chartItemInLegend.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)context.CurrentDataRegionClone;
			if (this.m_action != null)
			{
				chartItemInLegend.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_legendText != null)
			{
				chartItemInLegend.m_legendText = (ExpressionInfo)this.m_legendText.PublishClone(context);
			}
			if (this.m_toolTip != null)
			{
				chartItemInLegend.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			if (this.m_hidden != null)
			{
				chartItemInLegend.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
			}
			return chartItemInLegend;
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x000F66E8 File Offset: 0x000F48E8
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartItemInLegend, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.LegendText, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Chart, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Chart, Token.Reference),
				new MemberInfo(MemberName.ChartSeries, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartSeries, Token.Reference),
				new MemberInfo(MemberName.ChartDataPoint, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDataPoint, Token.Reference),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000F679D File Offset: 0x000F499D
		internal string EvaluateLegendText(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartItemInLegendLegendTextExpression(this, this.m_chart.Name);
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x000F67C4 File Offset: 0x000F49C4
		internal string EvaluateToolTip(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = context.ReportRuntime.EvaluateChartItemInLegendToolTipExpression(this, this.m_chart.Name);
			string text = null;
			if (variantResult.ErrorOccurred)
			{
				text = RPRes.rsExpressionErrorValue;
			}
			else if (variantResult.Value != null)
			{
				text = Formatter.Format(variantResult.Value, ref this.m_formatter, this.m_chart.StyleClass, null, context, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, this.m_chart.Name);
			}
			return text;
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x000F683D File Offset: 0x000F4A3D
		internal bool EvaluateHidden(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartItemInLegendHiddenExpression(this, this.m_chart.Name);
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x000F6864 File Offset: 0x000F4A64
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ChartItemInLegend.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Hidden)
				{
					if (memberName == MemberName.ToolTip)
					{
						writer.Write(this.m_toolTip);
						continue;
					}
					if (memberName == MemberName.LegendText)
					{
						writer.Write(this.m_legendText);
						continue;
					}
					if (memberName == MemberName.Hidden)
					{
						writer.Write(this.m_hidden);
						continue;
					}
				}
				else if (memberName <= MemberName.ChartSeries)
				{
					if (memberName == MemberName.Action)
					{
						writer.Write(this.m_action);
						continue;
					}
					if (memberName == MemberName.ChartSeries)
					{
						writer.WriteReference(this.m_chartSeries);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Chart)
					{
						writer.WriteReference(this.m_chart);
						continue;
					}
					if (memberName == MemberName.ChartDataPoint)
					{
						writer.WriteReference(this.m_chartDataPoint);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000F6968 File Offset: 0x000F4B68
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ChartItemInLegend.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Hidden)
				{
					if (memberName == MemberName.ToolTip)
					{
						this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.LegendText)
					{
						this.m_legendText = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Hidden)
					{
						this.m_hidden = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else if (memberName <= MemberName.ChartSeries)
				{
					if (memberName == MemberName.Action)
					{
						this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.ChartSeries)
					{
						this.m_chartSeries = reader.ReadReference<ChartSeries>(this);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Chart)
					{
						this.m_chart = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.Chart>(this);
						continue;
					}
					if (memberName == MemberName.ChartDataPoint)
					{
						this.m_chartDataPoint = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint>(this);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x000F6A8C File Offset: 0x000F4C8C
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ChartItemInLegend.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName != MemberName.ChartSeries)
					{
						if (memberName != MemberName.Chart)
						{
							if (memberName != MemberName.ChartDataPoint)
							{
								Global.Tracer.Assert(false);
							}
							else
							{
								Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
								this.m_chartDataPoint = (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint)referenceableItems[memberReference.RefID];
							}
						}
						else
						{
							Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
							this.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)referenceableItems[memberReference.RefID];
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

		// Token: 0x060038A9 RID: 14505 RVA: 0x000F6BAC File Offset: 0x000F4DAC
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartItemInLegend;
		}

		// Token: 0x04001B4C RID: 6988
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Chart m_chart;

		// Token: 0x04001B4D RID: 6989
		[Reference]
		private ChartSeries m_chartSeries;

		// Token: 0x04001B4E RID: 6990
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint m_chartDataPoint;

		// Token: 0x04001B4F RID: 6991
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001B50 RID: 6992
		private ExpressionInfo m_legendText;

		// Token: 0x04001B51 RID: 6993
		private ExpressionInfo m_toolTip;

		// Token: 0x04001B52 RID: 6994
		private ExpressionInfo m_hidden;

		// Token: 0x04001B53 RID: 6995
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x04001B54 RID: 6996
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartItemInLegend.GetDeclaration();

		// Token: 0x04001B55 RID: 6997
		[NonSerialized]
		private ChartDataPointInLegendExprHost m_exprHost;

		// Token: 0x04001B56 RID: 6998
		[NonSerialized]
		private Formatter m_formatter;
	}
}
