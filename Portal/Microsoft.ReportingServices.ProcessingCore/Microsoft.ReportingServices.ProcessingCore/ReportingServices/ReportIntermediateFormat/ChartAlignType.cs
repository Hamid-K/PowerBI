using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000488 RID: 1160
	[Serializable]
	internal sealed class ChartAlignType : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003623 RID: 13859 RVA: 0x000ECBF0 File Offset: 0x000EADF0
		internal ChartAlignType()
		{
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x000ECBF8 File Offset: 0x000EADF8
		internal ChartAlignType(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
		{
			this.m_chart = chart;
		}

		// Token: 0x170017E7 RID: 6119
		// (get) Token: 0x06003625 RID: 13861 RVA: 0x000ECC07 File Offset: 0x000EAE07
		// (set) Token: 0x06003626 RID: 13862 RVA: 0x000ECC0F File Offset: 0x000EAE0F
		internal ExpressionInfo Cursor
		{
			get
			{
				return this.m_cursor;
			}
			set
			{
				this.m_cursor = value;
			}
		}

		// Token: 0x170017E8 RID: 6120
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x000ECC18 File Offset: 0x000EAE18
		// (set) Token: 0x06003628 RID: 13864 RVA: 0x000ECC20 File Offset: 0x000EAE20
		internal ExpressionInfo AxesView
		{
			get
			{
				return this.m_axesView;
			}
			set
			{
				this.m_axesView = value;
			}
		}

		// Token: 0x170017E9 RID: 6121
		// (get) Token: 0x06003629 RID: 13865 RVA: 0x000ECC29 File Offset: 0x000EAE29
		// (set) Token: 0x0600362A RID: 13866 RVA: 0x000ECC31 File Offset: 0x000EAE31
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

		// Token: 0x170017EA RID: 6122
		// (get) Token: 0x0600362B RID: 13867 RVA: 0x000ECC3A File Offset: 0x000EAE3A
		// (set) Token: 0x0600362C RID: 13868 RVA: 0x000ECC42 File Offset: 0x000EAE42
		internal ExpressionInfo InnerPlotPosition
		{
			get
			{
				return this.m_innerPlotPosition;
			}
			set
			{
				this.m_innerPlotPosition = value;
			}
		}

		// Token: 0x170017EB RID: 6123
		// (get) Token: 0x0600362D RID: 13869 RVA: 0x000ECC4B File Offset: 0x000EAE4B
		internal ChartAreaExprHost ExprHost
		{
			get
			{
				return this.m_chartArea.ExprHost;
			}
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000ECC58 File Offset: 0x000EAE58
		internal void Initialize(InitializationContext context)
		{
			if (this.m_position != null)
			{
				this.m_position.Initialize("Position", context);
				context.ExprHostBuilder.ChartAlignTypePosition(this.m_position);
			}
			if (this.m_innerPlotPosition != null)
			{
				this.m_innerPlotPosition.Initialize("InnerPlotPosition", context);
				context.ExprHostBuilder.ChartAlignTypeInnerPlotPosition(this.m_innerPlotPosition);
			}
			if (this.m_cursor != null)
			{
				this.m_cursor.Initialize("Cursor", context);
				context.ExprHostBuilder.ChartAlignTypCursor(this.m_cursor);
			}
			if (this.m_axesView != null)
			{
				this.m_axesView.Initialize("AxesView", context);
				context.ExprHostBuilder.ChartAlignTypeAxesView(this.m_axesView);
			}
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000ECD14 File Offset: 0x000EAF14
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			ChartAlignType chartAlignType = (ChartAlignType)base.MemberwiseClone();
			chartAlignType.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)context.CurrentDataRegionClone;
			if (this.m_position != null)
			{
				chartAlignType.m_position = (ExpressionInfo)this.m_position.PublishClone(context);
			}
			if (this.m_innerPlotPosition != null)
			{
				chartAlignType.m_innerPlotPosition = (ExpressionInfo)this.m_innerPlotPosition.PublishClone(context);
			}
			if (this.m_cursor != null)
			{
				chartAlignType.m_cursor = (ExpressionInfo)this.m_cursor.PublishClone(context);
			}
			if (this.m_axesView != null)
			{
				chartAlignType.m_axesView = (ExpressionInfo)this.m_axesView.PublishClone(context);
			}
			return chartAlignType;
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x000ECDBC File Offset: 0x000EAFBC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartAlignType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Cursor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.AxesView, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Position, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.InnerPlotPosition, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Chart, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Chart, Token.Reference)
			});
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x000ECE48 File Offset: 0x000EB048
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ChartAlignType.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Position)
				{
					if (memberName != MemberName.Chart)
					{
						switch (memberName)
						{
						case MemberName.Cursor:
							writer.Write(this.m_cursor);
							break;
						case MemberName.AxesView:
							writer.Write(this.m_axesView);
							break;
						case MemberName.InnerPlotPosition:
							writer.Write(this.m_innerPlotPosition);
							break;
						default:
							Global.Tracer.Assert(false);
							break;
						}
					}
					else
					{
						writer.WriteReference(this.m_chart);
					}
				}
				else
				{
					writer.Write(this.m_position);
				}
			}
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x000ECF00 File Offset: 0x000EB100
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ChartAlignType.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Position)
				{
					if (memberName != MemberName.Chart)
					{
						switch (memberName)
						{
						case MemberName.Cursor:
							this.m_cursor = (ExpressionInfo)reader.ReadRIFObject();
							break;
						case MemberName.AxesView:
							this.m_axesView = (ExpressionInfo)reader.ReadRIFObject();
							break;
						case MemberName.InnerPlotPosition:
							this.m_innerPlotPosition = (ExpressionInfo)reader.ReadRIFObject();
							break;
						default:
							Global.Tracer.Assert(false);
							break;
						}
					}
					else
					{
						this.m_chart = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.Chart>(this);
					}
				}
				else
				{
					this.m_position = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000ECFCC File Offset: 0x000EB1CC
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ChartAlignType.m_Declaration.ObjectType, out list))
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

		// Token: 0x06003634 RID: 13876 RVA: 0x000ED070 File Offset: 0x000EB270
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartAlignType;
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000ED077 File Offset: 0x000EB277
		internal void SetExprHost(ChartArea chartArea)
		{
			this.m_chartArea = chartArea;
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x000ED080 File Offset: 0x000EB280
		internal bool EvaluateAxesView(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartAlignTypeAxesViewExpression(this, this.m_chart.Name, "AxesView");
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x000ED0AB File Offset: 0x000EB2AB
		internal bool EvaluateCursor(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartAlignTypeCursorExpression(this, this.m_chart.Name, "Cursor");
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x000ED0D6 File Offset: 0x000EB2D6
		internal bool EvaluatePosition(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartAlignTypePositionExpression(this, this.m_chart.Name, "Position");
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x000ED101 File Offset: 0x000EB301
		internal bool EvaluateInnerPlotPosition(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartAlignTypeInnerPlotPositionExpression(this, this.m_chart.Name, "InnerPlotPosition");
		}

		// Token: 0x04001A7B RID: 6779
		private ExpressionInfo m_position;

		// Token: 0x04001A7C RID: 6780
		private ExpressionInfo m_axesView;

		// Token: 0x04001A7D RID: 6781
		private ExpressionInfo m_cursor;

		// Token: 0x04001A7E RID: 6782
		private ExpressionInfo m_innerPlotPosition;

		// Token: 0x04001A7F RID: 6783
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Chart m_chart;

		// Token: 0x04001A80 RID: 6784
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartAlignType.GetDeclaration();

		// Token: 0x04001A81 RID: 6785
		[NonSerialized]
		private ChartArea m_chartArea;
	}
}
