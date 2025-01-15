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
	// Token: 0x0200048E RID: 1166
	[Serializable]
	internal sealed class ChartNoMoveDirections : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003720 RID: 14112 RVA: 0x000F0818 File Offset: 0x000EEA18
		internal ChartNoMoveDirections()
		{
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000F0820 File Offset: 0x000EEA20
		internal ChartNoMoveDirections(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, ChartSeries chartSeries)
		{
			this.m_chart = chart;
			this.m_chartSeries = chartSeries;
		}

		// Token: 0x17001836 RID: 6198
		// (get) Token: 0x06003722 RID: 14114 RVA: 0x000F0836 File Offset: 0x000EEA36
		internal ChartNoMoveDirectionsExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001837 RID: 6199
		// (get) Token: 0x06003723 RID: 14115 RVA: 0x000F083E File Offset: 0x000EEA3E
		// (set) Token: 0x06003724 RID: 14116 RVA: 0x000F0846 File Offset: 0x000EEA46
		internal ExpressionInfo Up
		{
			get
			{
				return this.m_up;
			}
			set
			{
				this.m_up = value;
			}
		}

		// Token: 0x17001838 RID: 6200
		// (get) Token: 0x06003725 RID: 14117 RVA: 0x000F084F File Offset: 0x000EEA4F
		// (set) Token: 0x06003726 RID: 14118 RVA: 0x000F0857 File Offset: 0x000EEA57
		internal ExpressionInfo Down
		{
			get
			{
				return this.m_down;
			}
			set
			{
				this.m_down = value;
			}
		}

		// Token: 0x17001839 RID: 6201
		// (get) Token: 0x06003727 RID: 14119 RVA: 0x000F0860 File Offset: 0x000EEA60
		// (set) Token: 0x06003728 RID: 14120 RVA: 0x000F0868 File Offset: 0x000EEA68
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

		// Token: 0x1700183A RID: 6202
		// (get) Token: 0x06003729 RID: 14121 RVA: 0x000F0871 File Offset: 0x000EEA71
		// (set) Token: 0x0600372A RID: 14122 RVA: 0x000F0879 File Offset: 0x000EEA79
		internal ExpressionInfo Right
		{
			get
			{
				return this.m_right;
			}
			set
			{
				this.m_right = value;
			}
		}

		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x0600372B RID: 14123 RVA: 0x000F0882 File Offset: 0x000EEA82
		// (set) Token: 0x0600372C RID: 14124 RVA: 0x000F088A File Offset: 0x000EEA8A
		internal ExpressionInfo UpLeft
		{
			get
			{
				return this.m_upLeft;
			}
			set
			{
				this.m_upLeft = value;
			}
		}

		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x0600372D RID: 14125 RVA: 0x000F0893 File Offset: 0x000EEA93
		// (set) Token: 0x0600372E RID: 14126 RVA: 0x000F089B File Offset: 0x000EEA9B
		internal ExpressionInfo UpRight
		{
			get
			{
				return this.m_upRight;
			}
			set
			{
				this.m_upRight = value;
			}
		}

		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x0600372F RID: 14127 RVA: 0x000F08A4 File Offset: 0x000EEAA4
		// (set) Token: 0x06003730 RID: 14128 RVA: 0x000F08AC File Offset: 0x000EEAAC
		internal ExpressionInfo DownLeft
		{
			get
			{
				return this.m_downLeft;
			}
			set
			{
				this.m_downLeft = value;
			}
		}

		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x06003731 RID: 14129 RVA: 0x000F08B5 File Offset: 0x000EEAB5
		// (set) Token: 0x06003732 RID: 14130 RVA: 0x000F08BD File Offset: 0x000EEABD
		internal ExpressionInfo DownRight
		{
			get
			{
				return this.m_downRight;
			}
			set
			{
				this.m_downRight = value;
			}
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000F08C6 File Offset: 0x000EEAC6
		internal void SetExprHost(ChartNoMoveDirectionsExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000F08F4 File Offset: 0x000EEAF4
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartNoMoveDirectionsStart();
			if (this.m_up != null)
			{
				this.m_up.Initialize("Up", context);
				context.ExprHostBuilder.ChartNoMoveDirectionsUp(this.m_up);
			}
			if (this.m_down != null)
			{
				this.m_down.Initialize("Down", context);
				context.ExprHostBuilder.ChartNoMoveDirectionsDown(this.m_down);
			}
			if (this.m_left != null)
			{
				this.m_left.Initialize("Left", context);
				context.ExprHostBuilder.ChartNoMoveDirectionsLeft(this.m_left);
			}
			if (this.m_right != null)
			{
				this.m_right.Initialize("Right", context);
				context.ExprHostBuilder.ChartNoMoveDirectionsRight(this.m_right);
			}
			if (this.m_upLeft != null)
			{
				this.m_upLeft.Initialize("UpLeft", context);
				context.ExprHostBuilder.ChartNoMoveDirectionsUpLeft(this.m_upLeft);
			}
			if (this.m_upRight != null)
			{
				this.m_upRight.Initialize("UpRight", context);
				context.ExprHostBuilder.ChartNoMoveDirectionsUpRight(this.m_upRight);
			}
			if (this.m_downLeft != null)
			{
				this.m_downLeft.Initialize("DownLeft", context);
				context.ExprHostBuilder.ChartNoMoveDirectionsDownLeft(this.m_downLeft);
			}
			if (this.m_downRight != null)
			{
				this.m_downRight.Initialize("DownRight", context);
				context.ExprHostBuilder.ChartNoMoveDirectionsDownRight(this.m_downRight);
			}
			context.ExprHostBuilder.ChartNoMoveDirectionsEnd();
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000F0A74 File Offset: 0x000EEC74
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			ChartNoMoveDirections chartNoMoveDirections = (ChartNoMoveDirections)base.MemberwiseClone();
			chartNoMoveDirections.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)context.CurrentDataRegionClone;
			if (this.m_up != null)
			{
				chartNoMoveDirections.m_up = (ExpressionInfo)this.m_up.PublishClone(context);
			}
			if (this.m_down != null)
			{
				chartNoMoveDirections.m_down = (ExpressionInfo)this.m_down.PublishClone(context);
			}
			if (this.m_left != null)
			{
				chartNoMoveDirections.m_left = (ExpressionInfo)this.m_left.PublishClone(context);
			}
			if (this.m_right != null)
			{
				chartNoMoveDirections.m_right = (ExpressionInfo)this.m_right.PublishClone(context);
			}
			if (this.m_upLeft != null)
			{
				chartNoMoveDirections.m_upLeft = (ExpressionInfo)this.m_upLeft.PublishClone(context);
			}
			if (this.m_upRight != null)
			{
				chartNoMoveDirections.m_upRight = (ExpressionInfo)this.m_upRight.PublishClone(context);
			}
			if (this.m_downLeft != null)
			{
				chartNoMoveDirections.m_downLeft = (ExpressionInfo)this.m_downLeft.PublishClone(context);
			}
			if (this.m_downRight != null)
			{
				chartNoMoveDirections.m_downRight = (ExpressionInfo)this.m_downRight.PublishClone(context);
			}
			return chartNoMoveDirections;
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000F0B98 File Offset: 0x000EED98
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartNoMoveDirections, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Up, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Down, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Left, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Right, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.UpLeft, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.UpRight, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DownLeft, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DownRight, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Chart, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Chart, Token.Reference),
				new MemberInfo(MemberName.ChartSeries, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartSeries, Token.Reference)
			});
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000F0C8B File Offset: 0x000EEE8B
		internal bool EvaluateUp(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chartSeries, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartNoMoveDirectionsUpExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000F0CB1 File Offset: 0x000EEEB1
		internal bool EvaluateDown(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chartSeries, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartNoMoveDirectionsDownExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000F0CD7 File Offset: 0x000EEED7
		internal bool EvaluateLeft(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chartSeries, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartNoMoveDirectionsLeftExpression(this, this.m_chart.Name);
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000F0CFD File Offset: 0x000EEEFD
		internal bool EvaluateRight(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chartSeries, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartNoMoveDirectionsRightExpression(this, this.m_chart.Name);
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x000F0D23 File Offset: 0x000EEF23
		internal bool EvaluateUpLeft(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chartSeries, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartNoMoveDirectionsUpLeftExpression(this, this.m_chart.Name);
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x000F0D49 File Offset: 0x000EEF49
		internal bool EvaluateUpRight(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chartSeries, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartNoMoveDirectionsUpRightExpression(this, this.m_chart.Name);
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000F0D6F File Offset: 0x000EEF6F
		internal bool EvaluateDownLeft(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chartSeries, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartNoMoveDirectionsDownLeftExpression(this, this.m_chart.Name);
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000F0D95 File Offset: 0x000EEF95
		internal bool EvaluateDownRight(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chartSeries, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartNoMoveDirectionsDownRightExpression(this, this.m_chart.Name);
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000F0DBC File Offset: 0x000EEFBC
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ChartNoMoveDirections.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.DownRight)
				{
					if (memberName == MemberName.Left)
					{
						writer.Write(this.m_left);
						continue;
					}
					switch (memberName)
					{
					case MemberName.Up:
						writer.Write(this.m_up);
						continue;
					case MemberName.Down:
						writer.Write(this.m_down);
						continue;
					case MemberName.Right:
						writer.Write(this.m_right);
						continue;
					case MemberName.UpLeft:
						writer.Write(this.m_upLeft);
						continue;
					case MemberName.UpRight:
						writer.Write(this.m_upRight);
						continue;
					case MemberName.DownLeft:
						writer.Write(this.m_downLeft);
						continue;
					case MemberName.DownRight:
						writer.Write(this.m_downRight);
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
					if (memberName == MemberName.Chart)
					{
						writer.WriteReference(this.m_chart);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000F0EF0 File Offset: 0x000EF0F0
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ChartNoMoveDirections.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.DownRight)
				{
					if (memberName == MemberName.Left)
					{
						this.m_left = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					switch (memberName)
					{
					case MemberName.Up:
						this.m_up = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.Down:
						this.m_down = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.Right:
						this.m_right = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.UpLeft:
						this.m_upLeft = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.UpRight:
						this.m_upRight = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.DownLeft:
						this.m_downLeft = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.DownRight:
						this.m_downRight = (ExpressionInfo)reader.ReadRIFObject();
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
					if (memberName == MemberName.Chart)
					{
						this.m_chart = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.Chart>(this);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x000F1054 File Offset: 0x000EF254
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ChartNoMoveDirections.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName != MemberName.ChartSeries)
					{
						if (memberName == MemberName.Chart)
						{
							Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
							this.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)referenceableItems[memberReference.RefID];
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

		// Token: 0x06003742 RID: 14146 RVA: 0x000F1138 File Offset: 0x000EF338
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartNoMoveDirections;
		}

		// Token: 0x04001ACE RID: 6862
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Chart m_chart;

		// Token: 0x04001ACF RID: 6863
		[Reference]
		private ChartSeries m_chartSeries;

		// Token: 0x04001AD0 RID: 6864
		private ExpressionInfo m_up;

		// Token: 0x04001AD1 RID: 6865
		private ExpressionInfo m_down;

		// Token: 0x04001AD2 RID: 6866
		private ExpressionInfo m_left;

		// Token: 0x04001AD3 RID: 6867
		private ExpressionInfo m_right;

		// Token: 0x04001AD4 RID: 6868
		private ExpressionInfo m_upLeft;

		// Token: 0x04001AD5 RID: 6869
		private ExpressionInfo m_upRight;

		// Token: 0x04001AD6 RID: 6870
		private ExpressionInfo m_downLeft;

		// Token: 0x04001AD7 RID: 6871
		private ExpressionInfo m_downRight;

		// Token: 0x04001AD8 RID: 6872
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartNoMoveDirections.GetDeclaration();

		// Token: 0x04001AD9 RID: 6873
		[NonSerialized]
		private ChartNoMoveDirectionsExprHost m_exprHost;
	}
}
