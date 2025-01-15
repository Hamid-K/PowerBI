using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000486 RID: 1158
	[Serializable]
	internal sealed class ChartLegendTitle : ChartTitleBase, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060035EE RID: 13806 RVA: 0x000EBF28 File Offset: 0x000EA128
		internal ChartLegendTitle()
		{
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000EBF30 File Offset: 0x000EA130
		internal ChartLegendTitle(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
			: base(chart)
		{
			this.m_chart = chart;
		}

		// Token: 0x170017D8 RID: 6104
		// (get) Token: 0x060035F0 RID: 13808 RVA: 0x000EBF40 File Offset: 0x000EA140
		// (set) Token: 0x060035F1 RID: 13809 RVA: 0x000EBF48 File Offset: 0x000EA148
		internal ExpressionInfo TitleSeparator
		{
			get
			{
				return this.m_titleSeparator;
			}
			set
			{
				this.m_titleSeparator = value;
			}
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x000EBF54 File Offset: 0x000EA154
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartLegendTitleStart();
			base.Initialize(context);
			if (this.m_titleSeparator != null)
			{
				this.m_titleSeparator.Initialize("TitleSeparator", context);
				context.ExprHostBuilder.ChartLegendTitleSeparator(this.m_titleSeparator);
			}
			context.ExprHostBuilder.ChartLegendTitleEnd();
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000EBFAC File Offset: 0x000EA1AC
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartLegendTitle chartLegendTitle = (ChartLegendTitle)base.PublishClone(context);
			if (this.m_titleSeparator != null)
			{
				chartLegendTitle.m_titleSeparator = (ExpressionInfo)this.m_titleSeparator.PublishClone(context);
			}
			return chartLegendTitle;
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000EBFE8 File Offset: 0x000EA1E8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegendTitle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartTitleBase, new List<MemberInfo>
			{
				new MemberInfo(MemberName.TitleSeparator, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x000EC020 File Offset: 0x000EA220
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartLegendTitle.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.TitleSeparator)
				{
					writer.Write(this.m_titleSeparator);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x000EC078 File Offset: 0x000EA278
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartLegendTitle.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.TitleSeparator)
				{
					this.m_titleSeparator = (ExpressionInfo)reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000EC0D5 File Offset: 0x000EA2D5
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x000EC0DF File Offset: 0x000EA2DF
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegendTitle;
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x000EC0E6 File Offset: 0x000EA2E6
		internal ChartSeparators EvaluateTitleSeparator(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartSeparator(context.ReportRuntime.EvaluateChartLegendTitleTitleSeparatorExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x04001A6B RID: 6763
		private ExpressionInfo m_titleSeparator;

		// Token: 0x04001A6C RID: 6764
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartLegendTitle.GetDeclaration();
	}
}
