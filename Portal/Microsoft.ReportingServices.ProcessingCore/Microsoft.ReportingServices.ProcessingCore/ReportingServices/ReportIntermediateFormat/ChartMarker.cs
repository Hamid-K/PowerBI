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
	// Token: 0x0200049A RID: 1178
	[Serializable]
	internal sealed class ChartMarker : ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060038AB RID: 14507 RVA: 0x000F6BBF File Offset: 0x000F4DBF
		internal ChartMarker()
		{
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x000F6BC7 File Offset: 0x000F4DC7
		internal ChartMarker(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint chartDataPoint)
			: base(chart)
		{
			this.m_chartDataPoint = chartDataPoint;
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000F6BD7 File Offset: 0x000F4DD7
		internal ChartMarker(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, ChartSeries chartSeries)
			: base(chart)
		{
			this.m_chartSeries = chartSeries;
		}

		// Token: 0x170018BD RID: 6333
		// (get) Token: 0x060038AE RID: 14510 RVA: 0x000F6BE7 File Offset: 0x000F4DE7
		// (set) Token: 0x060038AF RID: 14511 RVA: 0x000F6BEF File Offset: 0x000F4DEF
		internal ExpressionInfo Type
		{
			get
			{
				return this.m_markerType;
			}
			set
			{
				this.m_markerType = value;
			}
		}

		// Token: 0x170018BE RID: 6334
		// (get) Token: 0x060038B0 RID: 14512 RVA: 0x000F6BF8 File Offset: 0x000F4DF8
		// (set) Token: 0x060038B1 RID: 14513 RVA: 0x000F6C00 File Offset: 0x000F4E00
		internal ExpressionInfo Size
		{
			get
			{
				return this.m_markerSize;
			}
			set
			{
				this.m_markerSize = value;
			}
		}

		// Token: 0x170018BF RID: 6335
		// (get) Token: 0x060038B2 RID: 14514 RVA: 0x000F6C09 File Offset: 0x000F4E09
		internal ChartMarkerExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170018C0 RID: 6336
		// (get) Token: 0x060038B3 RID: 14515 RVA: 0x000F6C11 File Offset: 0x000F4E11
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

		// Token: 0x060038B4 RID: 14516 RVA: 0x000F6C38 File Offset: 0x000F4E38
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartMarker, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Type, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Size, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ChartDataPoint, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDataPoint, Token.Reference),
				new MemberInfo(MemberName.ChartSeries, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartSeries, Token.Reference)
			});
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000F6CB4 File Offset: 0x000F4EB4
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartMarker.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Type)
				{
					if (memberName == MemberName.Size)
					{
						writer.Write(this.m_markerSize);
						continue;
					}
					if (memberName == MemberName.Type)
					{
						writer.Write(this.m_markerType);
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
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000F6D68 File Offset: 0x000F4F68
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartMarker.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Type)
				{
					if (memberName == MemberName.Size)
					{
						this.m_markerSize = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Type)
					{
						this.m_markerType = (ExpressionInfo)reader.ReadRIFObject();
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
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000F6E28 File Offset: 0x000F5028
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ChartMarker.m_Declaration.ObjectType, out list))
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

		// Token: 0x060038B8 RID: 14520 RVA: 0x000F6F14 File Offset: 0x000F5114
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartMarker;
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x000F6F1C File Offset: 0x000F511C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartMarker chartMarker = (ChartMarker)base.PublishClone(context);
			if (this.m_markerSize != null)
			{
				chartMarker.m_markerSize = (ExpressionInfo)this.m_markerSize.PublishClone(context);
			}
			if (this.m_markerType != null)
			{
				chartMarker.m_markerType = (ExpressionInfo)this.m_markerType.PublishClone(context);
			}
			return chartMarker;
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x000F6F78 File Offset: 0x000F5178
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.DataPointMarkerStart();
			base.Initialize(context);
			if (this.m_markerSize != null)
			{
				this.m_markerSize.Initialize("Size", context);
				context.ExprHostBuilder.DataPointMarkerSize(this.m_markerSize);
			}
			if (this.m_markerType != null)
			{
				this.m_markerType.Initialize("Type", context);
				context.ExprHostBuilder.DataPointMarkerType(this.m_markerType);
			}
			context.ExprHostBuilder.DataPointMarkerEnd();
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x000F6FFA File Offset: 0x000F51FA
		internal void SetExprHost(ChartMarkerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x000F7024 File Offset: 0x000F5224
		internal string EvaluateChartMarkerSize(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartMarkerSize(this, this.m_chart.Name);
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x000F704A File Offset: 0x000F524A
		internal ChartMarkerTypes EvaluateChartMarkerType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return EnumTranslator.TranslateChartMarkerType(context.ReportRuntime.EvaluateChartMarkerType(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x04001B57 RID: 6999
		private ExpressionInfo m_markerType;

		// Token: 0x04001B58 RID: 7000
		private ExpressionInfo m_markerSize;

		// Token: 0x04001B59 RID: 7001
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint m_chartDataPoint;

		// Token: 0x04001B5A RID: 7002
		[Reference]
		private ChartSeries m_chartSeries;

		// Token: 0x04001B5B RID: 7003
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartMarker.GetDeclaration();

		// Token: 0x04001B5C RID: 7004
		[NonSerialized]
		private ChartMarkerExprHost m_exprHost;
	}
}
