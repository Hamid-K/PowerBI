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
	// Token: 0x02000482 RID: 1154
	[Serializable]
	internal class ChartTitleBase : ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600359E RID: 13726 RVA: 0x000EAF84 File Offset: 0x000E9184
		internal ChartTitleBase()
		{
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000EAF8C File Offset: 0x000E918C
		internal ChartTitleBase(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
			: base(chart)
		{
			this.m_chart = chart;
		}

		// Token: 0x170017C6 RID: 6086
		// (get) Token: 0x060035A0 RID: 13728 RVA: 0x000EAF9C File Offset: 0x000E919C
		// (set) Token: 0x060035A1 RID: 13729 RVA: 0x000EAFA4 File Offset: 0x000E91A4
		internal ExpressionInfo Caption
		{
			get
			{
				return this.m_caption;
			}
			set
			{
				this.m_caption = value;
			}
		}

		// Token: 0x170017C7 RID: 6087
		// (get) Token: 0x060035A2 RID: 13730 RVA: 0x000EAFAD File Offset: 0x000E91AD
		internal ChartTitleBaseExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x000EAFB5 File Offset: 0x000E91B5
		internal override void SetExprHost(StyleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = (ChartTitleBaseExprHost)exprHost;
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x000EAFDF File Offset: 0x000E91DF
		internal override void Initialize(InitializationContext context)
		{
			base.Initialize(context);
			if (this.m_caption != null)
			{
				this.m_caption.Initialize("Caption", context);
				context.ExprHostBuilder.ChartCaption(this.m_caption);
			}
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x000EB014 File Offset: 0x000E9214
		internal string EvaluateCaption(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = context.ReportRuntime.EvaluateChartTitleCaptionExpression(this, base.Name, "Caption");
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

		// Token: 0x060035A6 RID: 13734 RVA: 0x000EB094 File Offset: 0x000E9294
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartTitleBase chartTitleBase = (ChartTitleBase)base.PublishClone(context);
			if (this.m_caption != null)
			{
				chartTitleBase.m_caption = (ExpressionInfo)this.m_caption.PublishClone(context);
			}
			return chartTitleBase;
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000EB0D0 File Offset: 0x000E92D0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartTitleBase, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Caption, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000EB108 File Offset: 0x000E9308
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartTitleBase.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.Caption)
				{
					writer.Write(this.m_caption);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x000EB160 File Offset: 0x000E9360
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartTitleBase.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.Caption)
				{
					this.m_caption = (ExpressionInfo)reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x000EB1BD File Offset: 0x000E93BD
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x000EB1C7 File Offset: 0x000E93C7
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartTitleBase;
		}

		// Token: 0x04001A57 RID: 6743
		private ExpressionInfo m_caption;

		// Token: 0x04001A58 RID: 6744
		[NonSerialized]
		private Formatter m_formatter;

		// Token: 0x04001A59 RID: 6745
		[NonSerialized]
		private ChartTitleBaseExprHost m_exprHost;

		// Token: 0x04001A5A RID: 6746
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartTitleBase.GetDeclaration();
	}
}
