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
	// Token: 0x020004A3 RID: 1187
	[Serializable]
	internal sealed class ChartGridLines : ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003A3C RID: 14908 RVA: 0x000FCF74 File Offset: 0x000FB174
		internal ChartGridLines()
		{
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x000FCF7C File Offset: 0x000FB17C
		internal ChartGridLines(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
			: base(chart)
		{
		}

		// Token: 0x1700192F RID: 6447
		// (get) Token: 0x06003A3E RID: 14910 RVA: 0x000FCF85 File Offset: 0x000FB185
		// (set) Token: 0x06003A3F RID: 14911 RVA: 0x000FCF8D File Offset: 0x000FB18D
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

		// Token: 0x17001930 RID: 6448
		// (get) Token: 0x06003A40 RID: 14912 RVA: 0x000FCF96 File Offset: 0x000FB196
		// (set) Token: 0x06003A41 RID: 14913 RVA: 0x000FCF9E File Offset: 0x000FB19E
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

		// Token: 0x17001931 RID: 6449
		// (get) Token: 0x06003A42 RID: 14914 RVA: 0x000FCFA7 File Offset: 0x000FB1A7
		// (set) Token: 0x06003A43 RID: 14915 RVA: 0x000FCFAF File Offset: 0x000FB1AF
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

		// Token: 0x17001932 RID: 6450
		// (get) Token: 0x06003A44 RID: 14916 RVA: 0x000FCFB8 File Offset: 0x000FB1B8
		// (set) Token: 0x06003A45 RID: 14917 RVA: 0x000FCFC0 File Offset: 0x000FB1C0
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

		// Token: 0x17001933 RID: 6451
		// (get) Token: 0x06003A46 RID: 14918 RVA: 0x000FCFC9 File Offset: 0x000FB1C9
		// (set) Token: 0x06003A47 RID: 14919 RVA: 0x000FCFD1 File Offset: 0x000FB1D1
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

		// Token: 0x17001934 RID: 6452
		// (get) Token: 0x06003A48 RID: 14920 RVA: 0x000FCFDA File Offset: 0x000FB1DA
		internal ChartGridLinesExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x000FCFE4 File Offset: 0x000FB1E4
		internal void Initialize(InitializationContext context, bool isMajor)
		{
			context.ExprHostBuilder.ChartGridLinesStart(isMajor);
			base.Initialize(context);
			if (this.m_enabled != null)
			{
				this.m_enabled.Initialize("Enabled", context);
				context.ExprHostBuilder.ChartGridLinesEnabled(this.m_enabled);
			}
			if (this.m_interval != null)
			{
				this.m_interval.Initialize("Interval", context);
				context.ExprHostBuilder.ChartGridLinesInterval(this.m_interval);
			}
			if (this.m_intervalType != null)
			{
				this.m_intervalType.Initialize("IntervalType", context);
				context.ExprHostBuilder.ChartGridLinesEnabledIntervalType(this.m_intervalType);
			}
			if (this.m_intervalOffset != null)
			{
				this.m_intervalOffset.Initialize("IntervalOffset", context);
				context.ExprHostBuilder.ChartGridLinesIntervalOffset(this.m_intervalOffset);
			}
			if (this.m_intervalOffsetType != null)
			{
				this.m_intervalOffsetType.Initialize("IntervalOffsetType", context);
				context.ExprHostBuilder.ChartGridLinesIntervalOffsetType(this.m_intervalOffsetType);
			}
			context.ExprHostBuilder.ChartGridLinesEnd(isMajor);
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x000FD0E9 File Offset: 0x000FB2E9
		internal void SetExprHost(ChartGridLinesExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x06003A4B RID: 14923 RVA: 0x000FD0FC File Offset: 0x000FB2FC
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartGridLines chartGridLines = (ChartGridLines)base.PublishClone(context);
			if (this.m_enabled != null)
			{
				chartGridLines.m_enabled = (ExpressionInfo)this.m_enabled.PublishClone(context);
			}
			if (this.m_interval != null)
			{
				chartGridLines.m_interval = (ExpressionInfo)this.m_interval.PublishClone(context);
			}
			if (this.m_intervalType != null)
			{
				chartGridLines.m_intervalType = (ExpressionInfo)this.m_intervalType.PublishClone(context);
			}
			if (this.m_intervalOffset != null)
			{
				chartGridLines.m_intervalOffset = (ExpressionInfo)this.m_intervalOffset.PublishClone(context);
			}
			if (this.m_intervalOffsetType != null)
			{
				chartGridLines.m_intervalOffsetType = (ExpressionInfo)this.m_intervalOffsetType.PublishClone(context);
			}
			return chartGridLines;
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x000FD1B4 File Offset: 0x000FB3B4
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GridLines, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Enabled, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Interval, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntervalType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntervalOffset, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntervalOffsetType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x000FD240 File Offset: 0x000FB440
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartGridLines.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.Interval:
					writer.Write(this.m_interval);
					break;
				case MemberName.IntervalType:
					writer.Write(this.m_intervalType);
					break;
				case MemberName.IntervalOffset:
					writer.Write(this.m_intervalOffset);
					break;
				case MemberName.IntervalOffsetType:
					writer.Write(this.m_intervalOffsetType);
					break;
				default:
					if (memberName == MemberName.Enabled)
					{
						writer.Write(this.m_enabled);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
					break;
				}
			}
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x000FD2F8 File Offset: 0x000FB4F8
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartGridLines.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.Interval:
					this.m_interval = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.IntervalType:
					this.m_intervalType = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.IntervalOffset:
					this.m_intervalOffset = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.IntervalOffsetType:
					this.m_intervalOffsetType = (ExpressionInfo)reader.ReadRIFObject();
					break;
				default:
					if (memberName == MemberName.Enabled)
					{
						this.m_enabled = (ExpressionInfo)reader.ReadRIFObject();
					}
					else
					{
						Global.Tracer.Assert(false);
					}
					break;
				}
			}
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x000FD3C9 File Offset: 0x000FB5C9
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x000FD3D3 File Offset: 0x000FB5D3
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GridLines;
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x000FD3DA File Offset: 0x000FB5DA
		internal ChartAutoBool EvaluateEnabled(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartAutoBool(context.ReportRuntime.EvaluateChartGridLinesEnabledExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x000FD40B File Offset: 0x000FB60B
		internal double EvaluateInterval(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartGridLinesIntervalExpression(this, this.m_chart.Name, "Interval");
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x000FD436 File Offset: 0x000FB636
		internal ChartIntervalType EvaluateIntervalType(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return EnumTranslator.TranslateChartIntervalType(context.ReportRuntime.EvaluateChartGridLinesIntervalTypeExpression(this, this.m_chart.Name, "IntervalType"), context.ReportRuntime);
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x000FD46C File Offset: 0x000FB66C
		internal double EvaluateIntervalOffset(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartGridLinesIntervalOffsetExpression(this, this.m_chart.Name, "IntervalOffset");
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x000FD497 File Offset: 0x000FB697
		internal ChartIntervalType EvaluateIntervalOffsetType(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return EnumTranslator.TranslateChartIntervalType(context.ReportRuntime.EvaluateChartGridLinesIntervalOffsetTypeExpression(this, this.m_chart.Name, "IntervalOffsetType"), context.ReportRuntime);
		}

		// Token: 0x04001BD5 RID: 7125
		private ExpressionInfo m_enabled;

		// Token: 0x04001BD6 RID: 7126
		private ExpressionInfo m_interval;

		// Token: 0x04001BD7 RID: 7127
		private ExpressionInfo m_intervalType;

		// Token: 0x04001BD8 RID: 7128
		private ExpressionInfo m_intervalOffset;

		// Token: 0x04001BD9 RID: 7129
		private ExpressionInfo m_intervalOffsetType;

		// Token: 0x04001BDA RID: 7130
		[NonSerialized]
		private ChartGridLinesExprHost m_exprHost;

		// Token: 0x04001BDB RID: 7131
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartGridLines.GetDeclaration();
	}
}
