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
	// Token: 0x0200049F RID: 1183
	[Serializable]
	internal sealed class ChartTickMarks : ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600395D RID: 14685 RVA: 0x000F980B File Offset: 0x000F7A0B
		internal ChartTickMarks()
		{
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x000F9813 File Offset: 0x000F7A13
		internal ChartTickMarks(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
			: base(chart)
		{
		}

		// Token: 0x170018EC RID: 6380
		// (get) Token: 0x0600395F RID: 14687 RVA: 0x000F981C File Offset: 0x000F7A1C
		internal ChartTickMarksExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170018ED RID: 6381
		// (get) Token: 0x06003960 RID: 14688 RVA: 0x000F9824 File Offset: 0x000F7A24
		// (set) Token: 0x06003961 RID: 14689 RVA: 0x000F982C File Offset: 0x000F7A2C
		internal ExpressionInfo Enabled
		{
			get
			{
				return this.m_enabled;
			}
			set
			{
				this.m_enabled = value;
			}
		}

		// Token: 0x170018EE RID: 6382
		// (get) Token: 0x06003962 RID: 14690 RVA: 0x000F9835 File Offset: 0x000F7A35
		// (set) Token: 0x06003963 RID: 14691 RVA: 0x000F983D File Offset: 0x000F7A3D
		internal ExpressionInfo Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x170018EF RID: 6383
		// (get) Token: 0x06003964 RID: 14692 RVA: 0x000F9846 File Offset: 0x000F7A46
		// (set) Token: 0x06003965 RID: 14693 RVA: 0x000F984E File Offset: 0x000F7A4E
		internal ExpressionInfo Length
		{
			get
			{
				return this.m_length;
			}
			set
			{
				this.m_length = value;
			}
		}

		// Token: 0x170018F0 RID: 6384
		// (get) Token: 0x06003966 RID: 14694 RVA: 0x000F9857 File Offset: 0x000F7A57
		// (set) Token: 0x06003967 RID: 14695 RVA: 0x000F985F File Offset: 0x000F7A5F
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

		// Token: 0x170018F1 RID: 6385
		// (get) Token: 0x06003968 RID: 14696 RVA: 0x000F9868 File Offset: 0x000F7A68
		// (set) Token: 0x06003969 RID: 14697 RVA: 0x000F9870 File Offset: 0x000F7A70
		internal ExpressionInfo IntervalType
		{
			get
			{
				return this.m_intervalType;
			}
			set
			{
				this.m_intervalType = value;
			}
		}

		// Token: 0x170018F2 RID: 6386
		// (get) Token: 0x0600396A RID: 14698 RVA: 0x000F9879 File Offset: 0x000F7A79
		// (set) Token: 0x0600396B RID: 14699 RVA: 0x000F9881 File Offset: 0x000F7A81
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

		// Token: 0x170018F3 RID: 6387
		// (get) Token: 0x0600396C RID: 14700 RVA: 0x000F988A File Offset: 0x000F7A8A
		// (set) Token: 0x0600396D RID: 14701 RVA: 0x000F9892 File Offset: 0x000F7A92
		internal ExpressionInfo IntervalOffsetType
		{
			get
			{
				return this.m_intervalOffsetType;
			}
			set
			{
				this.m_intervalOffsetType = value;
			}
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000F989B File Offset: 0x000F7A9B
		internal void SetExprHost(ChartTickMarksExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x000F98AC File Offset: 0x000F7AAC
		internal void Initialize(InitializationContext context, bool isMajor)
		{
			context.ExprHostBuilder.ChartTickMarksStart(isMajor);
			base.Initialize(context);
			if (this.m_enabled != null)
			{
				this.m_enabled.Initialize("Enabled", context);
				context.ExprHostBuilder.ChartTickMarksEnabled(this.m_enabled);
			}
			if (this.m_type != null)
			{
				this.m_type.Initialize("Type", context);
				context.ExprHostBuilder.ChartTickMarksType(this.m_type);
			}
			if (this.m_length != null)
			{
				this.m_length.Initialize("Length", context);
				context.ExprHostBuilder.ChartTickMarksLength(this.m_length);
			}
			if (this.m_interval != null)
			{
				this.m_interval.Initialize("Interval", context);
				context.ExprHostBuilder.ChartTickMarksInterval(this.m_interval);
			}
			if (this.m_intervalType != null)
			{
				this.m_intervalType.Initialize("IntervalType", context);
				context.ExprHostBuilder.ChartTickMarksIntervalType(this.m_intervalType);
			}
			if (this.m_intervalOffset != null)
			{
				this.m_intervalOffset.Initialize("IntervalOffset", context);
				context.ExprHostBuilder.ChartTickMarksIntervalOffset(this.m_intervalOffset);
			}
			if (this.m_intervalOffsetType != null)
			{
				this.m_intervalOffsetType.Initialize("IntervalOffsetType", context);
				context.ExprHostBuilder.ChartTickMarksIntervalOffsetType(this.m_intervalOffsetType);
			}
			context.ExprHostBuilder.ChartTickMarksEnd(isMajor);
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000F9A08 File Offset: 0x000F7C08
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartTickMarks chartTickMarks = (ChartTickMarks)base.PublishClone(context);
			if (this.m_enabled != null)
			{
				chartTickMarks.m_enabled = (ExpressionInfo)this.m_enabled.PublishClone(context);
			}
			if (this.m_type != null)
			{
				chartTickMarks.m_type = (ExpressionInfo)this.m_type.PublishClone(context);
			}
			if (this.m_length != null)
			{
				chartTickMarks.m_length = (ExpressionInfo)this.m_length.PublishClone(context);
			}
			if (this.m_interval != null)
			{
				chartTickMarks.m_interval = (ExpressionInfo)this.m_interval.PublishClone(context);
			}
			if (this.m_intervalType != null)
			{
				chartTickMarks.m_intervalType = (ExpressionInfo)this.m_intervalType.PublishClone(context);
			}
			if (this.m_intervalOffset != null)
			{
				chartTickMarks.m_intervalOffset = (ExpressionInfo)this.m_intervalOffset.PublishClone(context);
			}
			if (this.m_intervalOffsetType != null)
			{
				chartTickMarks.m_intervalOffsetType = (ExpressionInfo)this.m_intervalOffsetType.PublishClone(context);
			}
			return chartTickMarks;
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x000F9AFC File Offset: 0x000F7CFC
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartTickMarks, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Enabled, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Type, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Length, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Interval, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntervalType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntervalOffset, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntervalOffsetType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000F9BB2 File Offset: 0x000F7DB2
		internal string EvaluateEnabled(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartTickMarksEnabledExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x000F9BD8 File Offset: 0x000F7DD8
		internal ChartTickMarksType EvaluateType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartTickMarksType(context.ReportRuntime.EvaluateChartTickMarksTypeExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x000F9C09 File Offset: 0x000F7E09
		internal double EvaluateLength(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartTickMarksLengthExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x000F9C2F File Offset: 0x000F7E2F
		internal double EvaluateInterval(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartTickMarksIntervalExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x000F9C55 File Offset: 0x000F7E55
		internal ChartIntervalType EvaluateIntervalType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartIntervalType(context.ReportRuntime.EvaluateChartTickMarksIntervalTypeExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x000F9C86 File Offset: 0x000F7E86
		internal double EvaluateIntervalOffset(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartTickMarksIntervalOffsetExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x000F9CAC File Offset: 0x000F7EAC
		internal ChartIntervalType EvaluateIntervalOffsetType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartIntervalType(context.ReportRuntime.EvaluateChartTickMarksIntervalOffsetTypeExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x000F9CE0 File Offset: 0x000F7EE0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartTickMarks.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Length)
				{
					switch (memberName)
					{
					case MemberName.Interval:
						writer.Write(this.m_interval);
						continue;
					case MemberName.IntervalType:
						writer.Write(this.m_intervalType);
						continue;
					case MemberName.IntervalOffset:
						writer.Write(this.m_intervalOffset);
						continue;
					case MemberName.IntervalOffsetType:
						writer.Write(this.m_intervalOffsetType);
						continue;
					default:
						if (memberName == MemberName.Length)
						{
							writer.Write(this.m_length);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.Type)
					{
						writer.Write(this.m_type);
						continue;
					}
					if (memberName == MemberName.Enabled)
					{
						writer.Write(this.m_enabled);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x000F9DD0 File Offset: 0x000F7FD0
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartTickMarks.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Length)
				{
					switch (memberName)
					{
					case MemberName.Interval:
						this.m_interval = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.IntervalType:
						this.m_intervalType = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.IntervalOffset:
						this.m_intervalOffset = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.IntervalOffsetType:
						this.m_intervalOffsetType = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.Length)
						{
							this.m_length = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.Type)
					{
						this.m_type = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Enabled)
					{
						this.m_enabled = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x000F9EEC File Offset: 0x000F80EC
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x000F9EF6 File Offset: 0x000F80F6
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartTickMarks;
		}

		// Token: 0x04001B8E RID: 7054
		private ExpressionInfo m_enabled;

		// Token: 0x04001B8F RID: 7055
		private ExpressionInfo m_type;

		// Token: 0x04001B90 RID: 7056
		private ExpressionInfo m_length;

		// Token: 0x04001B91 RID: 7057
		private ExpressionInfo m_interval;

		// Token: 0x04001B92 RID: 7058
		private ExpressionInfo m_intervalType;

		// Token: 0x04001B93 RID: 7059
		private ExpressionInfo m_intervalOffset;

		// Token: 0x04001B94 RID: 7060
		private ExpressionInfo m_intervalOffsetType;

		// Token: 0x04001B95 RID: 7061
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartTickMarks.GetDeclaration();

		// Token: 0x04001B96 RID: 7062
		[NonSerialized]
		private ChartTickMarksExprHost m_exprHost;
	}
}
