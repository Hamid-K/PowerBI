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
	// Token: 0x0200048F RID: 1167
	[Serializable]
	internal sealed class ChartElementPosition : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003744 RID: 14148 RVA: 0x000F114B File Offset: 0x000EF34B
		internal ChartElementPosition()
		{
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000F1153 File Offset: 0x000EF353
		internal ChartElementPosition(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
		{
			this.m_chart = chart;
		}

		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x06003746 RID: 14150 RVA: 0x000F1162 File Offset: 0x000EF362
		// (set) Token: 0x06003747 RID: 14151 RVA: 0x000F116A File Offset: 0x000EF36A
		internal ExpressionInfo Top
		{
			get
			{
				return this.m_top;
			}
			set
			{
				this.m_top = value;
			}
		}

		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x06003748 RID: 14152 RVA: 0x000F1173 File Offset: 0x000EF373
		// (set) Token: 0x06003749 RID: 14153 RVA: 0x000F117B File Offset: 0x000EF37B
		internal ExpressionInfo Left
		{
			get
			{
				return this.m_left;
			}
			set
			{
				this.m_left = value;
			}
		}

		// Token: 0x17001841 RID: 6209
		// (get) Token: 0x0600374A RID: 14154 RVA: 0x000F1184 File Offset: 0x000EF384
		// (set) Token: 0x0600374B RID: 14155 RVA: 0x000F118C File Offset: 0x000EF38C
		internal ExpressionInfo Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		// Token: 0x17001842 RID: 6210
		// (get) Token: 0x0600374C RID: 14156 RVA: 0x000F1195 File Offset: 0x000EF395
		// (set) Token: 0x0600374D RID: 14157 RVA: 0x000F119D File Offset: 0x000EF39D
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

		// Token: 0x17001843 RID: 6211
		// (get) Token: 0x0600374E RID: 14158 RVA: 0x000F11A6 File Offset: 0x000EF3A6
		internal string OwnerName
		{
			get
			{
				return this.m_chart.Name;
			}
		}

		// Token: 0x17001844 RID: 6212
		// (get) Token: 0x0600374F RID: 14159 RVA: 0x000F11B3 File Offset: 0x000EF3B3
		internal ChartElementPositionExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x000F11BB File Offset: 0x000EF3BB
		internal void Initialize(InitializationContext context)
		{
			this.Initialize(context, false);
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x000F11C8 File Offset: 0x000EF3C8
		internal void Initialize(InitializationContext context, bool innerPlot)
		{
			context.ExprHostBuilder.ChartElementPositionStart(innerPlot);
			if (this.m_top != null)
			{
				this.m_top.Initialize("Top", context);
				context.ExprHostBuilder.ChartElementPositionTop(this.m_top);
			}
			if (this.m_left != null)
			{
				this.m_left.Initialize("Left", context);
				context.ExprHostBuilder.ChartElementPositionLeft(this.m_left);
			}
			if (this.m_height != null)
			{
				this.m_height.Initialize("Height", context);
				context.ExprHostBuilder.ChartElementPositionHeight(this.m_height);
			}
			if (this.m_width != null)
			{
				this.m_width.Initialize("Width", context);
				context.ExprHostBuilder.ChartElementPositionWidth(this.m_width);
			}
			context.ExprHostBuilder.ChartElementPositionEnd(innerPlot);
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x000F129C File Offset: 0x000EF49C
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			ChartElementPosition chartElementPosition = (ChartElementPosition)base.MemberwiseClone();
			chartElementPosition.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)context.CurrentDataRegionClone;
			if (this.m_top != null)
			{
				chartElementPosition.m_top = (ExpressionInfo)this.m_top.PublishClone(context);
			}
			if (this.m_left != null)
			{
				chartElementPosition.m_left = (ExpressionInfo)this.m_left.PublishClone(context);
			}
			if (this.m_height != null)
			{
				chartElementPosition.m_height = (ExpressionInfo)this.m_height.PublishClone(context);
			}
			if (this.m_width != null)
			{
				chartElementPosition.m_width = (ExpressionInfo)this.m_width.PublishClone(context);
			}
			return chartElementPosition;
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x000F1344 File Offset: 0x000EF544
		internal void SetExprHost(ChartElementPositionExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000F1374 File Offset: 0x000EF574
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartElementPosition, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Top, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Left, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Height, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Width, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Chart, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Chart, Token.Reference)
			});
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000F1400 File Offset: 0x000EF600
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ChartElementPosition.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.Chart)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000F14A4 File Offset: 0x000EF6A4
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ChartElementPosition.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.Top:
					writer.Write(this.m_top);
					continue;
				case MemberName.TopValue:
				case MemberName.LeftValue:
				case MemberName.HeightValue:
					break;
				case MemberName.Left:
					writer.Write(this.m_left);
					continue;
				case MemberName.Height:
					writer.Write(this.m_height);
					continue;
				case MemberName.Width:
					writer.Write(this.m_width);
					continue;
				default:
					if (memberName == MemberName.Chart)
					{
						writer.WriteReference(this.m_chart);
						continue;
					}
					break;
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000F1564 File Offset: 0x000EF764
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ChartElementPosition.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.Top:
					this.m_top = (ExpressionInfo)reader.ReadRIFObject();
					continue;
				case MemberName.TopValue:
				case MemberName.LeftValue:
				case MemberName.HeightValue:
					break;
				case MemberName.Left:
					this.m_left = (ExpressionInfo)reader.ReadRIFObject();
					continue;
				case MemberName.Height:
					this.m_height = (ExpressionInfo)reader.ReadRIFObject();
					continue;
				case MemberName.Width:
					this.m_width = (ExpressionInfo)reader.ReadRIFObject();
					continue;
				default:
					if (memberName == MemberName.Chart)
					{
						this.m_chart = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.Chart>(this);
						continue;
					}
					break;
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000F1638 File Offset: 0x000EF838
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartElementPosition;
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x000F163F File Offset: 0x000EF83F
		internal double EvaluateTop(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartElementPositionExpression(this.Top, "Top", this.ExprHost, ChartElementPosition.Position.Top, this.m_chart.Name);
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x000F1676 File Offset: 0x000EF876
		internal double EvaluateLeft(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartElementPositionExpression(this.Left, "Left", this.ExprHost, ChartElementPosition.Position.Left, this.m_chart.Name);
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x000F16AD File Offset: 0x000EF8AD
		internal double EvaluateHeight(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartElementPositionExpression(this.Height, "Height", this.ExprHost, ChartElementPosition.Position.Height, this.m_chart.Name);
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x000F16E4 File Offset: 0x000EF8E4
		internal double EvaluateWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartElementPositionExpression(this.Width, "Width", this.ExprHost, ChartElementPosition.Position.Width, this.m_chart.Name);
		}

		// Token: 0x04001ADA RID: 6874
		[NonSerialized]
		private ChartElementPositionExprHost m_exprHost;

		// Token: 0x04001ADB RID: 6875
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Chart m_chart;

		// Token: 0x04001ADC RID: 6876
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartElementPosition.GetDeclaration();

		// Token: 0x04001ADD RID: 6877
		private ExpressionInfo m_top;

		// Token: 0x04001ADE RID: 6878
		private ExpressionInfo m_left;

		// Token: 0x04001ADF RID: 6879
		private ExpressionInfo m_height;

		// Token: 0x04001AE0 RID: 6880
		private ExpressionInfo m_width;

		// Token: 0x0200096F RID: 2415
		internal enum Position
		{
			// Token: 0x040040C0 RID: 16576
			Top,
			// Token: 0x040040C1 RID: 16577
			Left,
			// Token: 0x040040C2 RID: 16578
			Height,
			// Token: 0x040040C3 RID: 16579
			Width
		}
	}
}
