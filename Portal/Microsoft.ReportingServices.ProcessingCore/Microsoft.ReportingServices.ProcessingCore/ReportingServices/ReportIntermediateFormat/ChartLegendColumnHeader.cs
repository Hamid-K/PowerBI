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
	// Token: 0x0200048A RID: 1162
	[Serializable]
	internal sealed class ChartLegendColumnHeader : ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003667 RID: 13927 RVA: 0x000EDADC File Offset: 0x000EBCDC
		internal ChartLegendColumnHeader()
		{
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x000EDAE4 File Offset: 0x000EBCE4
		internal ChartLegendColumnHeader(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
			: base(chart)
		{
		}

		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x06003669 RID: 13929 RVA: 0x000EDAED File Offset: 0x000EBCED
		internal ChartLegendColumnHeaderExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x0600366A RID: 13930 RVA: 0x000EDAF5 File Offset: 0x000EBCF5
		// (set) Token: 0x0600366B RID: 13931 RVA: 0x000EDAFD File Offset: 0x000EBCFD
		internal ExpressionInfo Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x000EDB06 File Offset: 0x000EBD06
		internal void SetExprHost(ChartLegendColumnHeaderExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000EDB30 File Offset: 0x000EBD30
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartLegendColumnHeaderStart();
			base.Initialize(context);
			if (this.m_value != null)
			{
				this.m_value.Initialize("Value", context);
				context.ExprHostBuilder.ChartLegendColumnHeaderValue(this.m_value);
			}
			context.ExprHostBuilder.ChartLegendColumnHeaderEnd();
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000EDB88 File Offset: 0x000EBD88
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartLegendColumnHeader chartLegendColumnHeader = (ChartLegendColumnHeader)base.PublishClone(context);
			if (this.m_value != null)
			{
				chartLegendColumnHeader.m_value = (ExpressionInfo)this.m_value.PublishClone(context);
			}
			return chartLegendColumnHeader;
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000EDBC4 File Offset: 0x000EBDC4
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegendColumnHeader, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000EDBF9 File Offset: 0x000EBDF9
		internal string EvaluateValue(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendColumnHeaderValueExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x000EDC20 File Offset: 0x000EBE20
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartLegendColumnHeader.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.Value)
				{
					writer.Write(this.m_value);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x000EDC78 File Offset: 0x000EBE78
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartLegendColumnHeader.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.Value)
				{
					this.m_value = (ExpressionInfo)reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000EDCD2 File Offset: 0x000EBED2
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x000EDCDC File Offset: 0x000EBEDC
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegendColumnHeader;
		}

		// Token: 0x04001A91 RID: 6801
		private ExpressionInfo m_value;

		// Token: 0x04001A92 RID: 6802
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartLegendColumnHeader.GetDeclaration();

		// Token: 0x04001A93 RID: 6803
		[NonSerialized]
		private ChartLegendColumnHeaderExprHost m_exprHost;
	}
}
