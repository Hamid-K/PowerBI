using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000485 RID: 1157
	[Serializable]
	internal sealed class ChartAxisTitle : ChartTitleBase, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060035DE RID: 13790 RVA: 0x000EBC4E File Offset: 0x000E9E4E
		internal ChartAxisTitle()
		{
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000EBC56 File Offset: 0x000E9E56
		internal ChartAxisTitle(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
			: base(chart)
		{
			this.m_chart = chart;
		}

		// Token: 0x170017D6 RID: 6102
		// (get) Token: 0x060035E0 RID: 13792 RVA: 0x000EBC66 File Offset: 0x000E9E66
		// (set) Token: 0x060035E1 RID: 13793 RVA: 0x000EBC6E File Offset: 0x000E9E6E
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

		// Token: 0x170017D7 RID: 6103
		// (get) Token: 0x060035E2 RID: 13794 RVA: 0x000EBC77 File Offset: 0x000E9E77
		// (set) Token: 0x060035E3 RID: 13795 RVA: 0x000EBC7F File Offset: 0x000E9E7F
		internal ExpressionInfo TextOrientation
		{
			get
			{
				return this.m_textOrientation;
			}
			set
			{
				this.m_textOrientation = value;
			}
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000EBC88 File Offset: 0x000E9E88
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartAxisTitleStart();
			base.Initialize(context);
			if (this.m_position != null)
			{
				this.m_position.Initialize("Position", context);
				context.ExprHostBuilder.ChartTitlePosition(this.m_position);
			}
			if (this.m_textOrientation != null)
			{
				this.m_textOrientation.Initialize("TextOrientation", context);
				context.ExprHostBuilder.ChartAxisTitleTextOrientation(this.m_textOrientation);
			}
			context.ExprHostBuilder.ChartAxisTitleEnd();
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x000EBD0C File Offset: 0x000E9F0C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartAxisTitle chartAxisTitle = (ChartAxisTitle)base.PublishClone(context);
			if (this.m_position != null)
			{
				chartAxisTitle.m_position = (ExpressionInfo)this.m_position.PublishClone(context);
			}
			if (this.m_textOrientation != null)
			{
				chartAxisTitle.m_textOrientation = (ExpressionInfo)this.m_textOrientation.PublishClone(context);
			}
			return chartAxisTitle;
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000EBD68 File Offset: 0x000E9F68
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartAxisTitle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartTitle, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Position, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TextOrientation, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000EBDB8 File Offset: 0x000E9FB8
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartAxisTitle.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Position)
				{
					if (memberName != MemberName.TextOrientation)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_textOrientation);
					}
				}
				else
				{
					writer.Write(this.m_position);
				}
			}
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x000EBE2C File Offset: 0x000EA02C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartAxisTitle.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Position)
				{
					if (memberName != MemberName.TextOrientation)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_textOrientation = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_position = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x000EBEA9 File Offset: 0x000EA0A9
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x000EBEB3 File Offset: 0x000EA0B3
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartAxisTitle;
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x000EBEBA File Offset: 0x000EA0BA
		internal ChartAxisTitlePositions EvaluatePosition(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return EnumTranslator.TranslateChartAxisTitlePosition(context.ReportRuntime.EvaluateChartAxisTitlePositionExpression(this, base.Name, "Position"), context.ReportRuntime);
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000EBEEB File Offset: 0x000EA0EB
		internal TextOrientations EvaluateTextOrientation(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateTextOrientations(context.ReportRuntime.EvaluateChartAxisTitleTextOrientationExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x04001A68 RID: 6760
		private ExpressionInfo m_position;

		// Token: 0x04001A69 RID: 6761
		private ExpressionInfo m_textOrientation;

		// Token: 0x04001A6A RID: 6762
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartAxisTitle.GetDeclaration();
	}
}
