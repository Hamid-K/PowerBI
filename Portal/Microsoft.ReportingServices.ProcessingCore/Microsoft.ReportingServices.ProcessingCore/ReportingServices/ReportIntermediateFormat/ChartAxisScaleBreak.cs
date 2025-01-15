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
	// Token: 0x020004A1 RID: 1185
	[Serializable]
	internal sealed class ChartAxisScaleBreak : ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600398D RID: 14733 RVA: 0x000FA132 File Offset: 0x000F8332
		internal ChartAxisScaleBreak()
		{
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x000FA13A File Offset: 0x000F833A
		internal ChartAxisScaleBreak(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
			: base(chart)
		{
		}

		// Token: 0x170018F6 RID: 6390
		// (get) Token: 0x0600398F RID: 14735 RVA: 0x000FA143 File Offset: 0x000F8343
		internal ChartAxisScaleBreakExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170018F7 RID: 6391
		// (get) Token: 0x06003990 RID: 14736 RVA: 0x000FA14B File Offset: 0x000F834B
		// (set) Token: 0x06003991 RID: 14737 RVA: 0x000FA153 File Offset: 0x000F8353
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

		// Token: 0x170018F8 RID: 6392
		// (get) Token: 0x06003992 RID: 14738 RVA: 0x000FA15C File Offset: 0x000F835C
		// (set) Token: 0x06003993 RID: 14739 RVA: 0x000FA164 File Offset: 0x000F8364
		internal ExpressionInfo BreakLineType
		{
			get
			{
				return this.m_breakLineType;
			}
			set
			{
				this.m_breakLineType = value;
			}
		}

		// Token: 0x170018F9 RID: 6393
		// (get) Token: 0x06003994 RID: 14740 RVA: 0x000FA16D File Offset: 0x000F836D
		// (set) Token: 0x06003995 RID: 14741 RVA: 0x000FA175 File Offset: 0x000F8375
		internal ExpressionInfo CollapsibleSpaceThreshold
		{
			get
			{
				return this.m_collapsibleSpaceThreshold;
			}
			set
			{
				this.m_collapsibleSpaceThreshold = value;
			}
		}

		// Token: 0x170018FA RID: 6394
		// (get) Token: 0x06003996 RID: 14742 RVA: 0x000FA17E File Offset: 0x000F837E
		// (set) Token: 0x06003997 RID: 14743 RVA: 0x000FA186 File Offset: 0x000F8386
		internal ExpressionInfo MaxNumberOfBreaks
		{
			get
			{
				return this.m_maxNumberOfBreaks;
			}
			set
			{
				this.m_maxNumberOfBreaks = value;
			}
		}

		// Token: 0x170018FB RID: 6395
		// (get) Token: 0x06003998 RID: 14744 RVA: 0x000FA18F File Offset: 0x000F838F
		// (set) Token: 0x06003999 RID: 14745 RVA: 0x000FA197 File Offset: 0x000F8397
		internal ExpressionInfo Spacing
		{
			get
			{
				return this.m_spacing;
			}
			set
			{
				this.m_spacing = value;
			}
		}

		// Token: 0x170018FC RID: 6396
		// (get) Token: 0x0600399A RID: 14746 RVA: 0x000FA1A0 File Offset: 0x000F83A0
		// (set) Token: 0x0600399B RID: 14747 RVA: 0x000FA1A8 File Offset: 0x000F83A8
		internal ExpressionInfo IncludeZero
		{
			get
			{
				return this.m_includeZero;
			}
			set
			{
				this.m_includeZero = value;
			}
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x000FA1B1 File Offset: 0x000F83B1
		internal void SetExprHost(ChartAxisScaleBreakExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x0600399D RID: 14749 RVA: 0x000FA1DC File Offset: 0x000F83DC
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartAxisScaleBreakStart();
			base.Initialize(context);
			if (this.m_enabled != null)
			{
				this.m_enabled.Initialize("Enabled", context);
				context.ExprHostBuilder.ChartAxisScaleBreakEnabled(this.m_enabled);
			}
			if (this.m_breakLineType != null)
			{
				this.m_breakLineType.Initialize("BreakLineType", context);
				context.ExprHostBuilder.ChartAxisScaleBreakBreakLineType(this.m_breakLineType);
			}
			if (this.m_collapsibleSpaceThreshold != null)
			{
				this.m_collapsibleSpaceThreshold.Initialize("CollapsibleSpaceThreshold", context);
				context.ExprHostBuilder.ChartAxisScaleBreakCollapsibleSpaceThreshold(this.m_collapsibleSpaceThreshold);
			}
			if (this.m_maxNumberOfBreaks != null)
			{
				this.m_maxNumberOfBreaks.Initialize("MaxNumberOfBreaks", context);
				context.ExprHostBuilder.ChartAxisScaleBreakMaxNumberOfBreaks(this.m_maxNumberOfBreaks);
			}
			if (this.m_spacing != null)
			{
				this.m_spacing.Initialize("Spacing", context);
				context.ExprHostBuilder.ChartAxisScaleBreakSpacing(this.m_spacing);
			}
			if (this.m_includeZero != null)
			{
				this.m_includeZero.Initialize("IncludeZero", context);
				context.ExprHostBuilder.ChartAxisScaleBreakIncludeZero(this.m_includeZero);
			}
			context.ExprHostBuilder.ChartAxisScaleBreakEnd();
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x000FA30C File Offset: 0x000F850C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartAxisScaleBreak chartAxisScaleBreak = (ChartAxisScaleBreak)base.PublishClone(context);
			if (this.m_enabled != null)
			{
				chartAxisScaleBreak.m_enabled = (ExpressionInfo)this.m_enabled.PublishClone(context);
			}
			if (this.m_breakLineType != null)
			{
				chartAxisScaleBreak.m_breakLineType = (ExpressionInfo)this.m_breakLineType.PublishClone(context);
			}
			if (this.m_collapsibleSpaceThreshold != null)
			{
				chartAxisScaleBreak.m_collapsibleSpaceThreshold = (ExpressionInfo)this.m_collapsibleSpaceThreshold.PublishClone(context);
			}
			if (this.m_maxNumberOfBreaks != null)
			{
				chartAxisScaleBreak.m_maxNumberOfBreaks = (ExpressionInfo)this.m_maxNumberOfBreaks.PublishClone(context);
			}
			if (this.m_spacing != null)
			{
				chartAxisScaleBreak.m_spacing = (ExpressionInfo)this.m_spacing.PublishClone(context);
			}
			if (this.m_includeZero != null)
			{
				chartAxisScaleBreak.m_includeZero = (ExpressionInfo)this.m_includeZero.PublishClone(context);
			}
			return chartAxisScaleBreak;
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x000FA3E4 File Offset: 0x000F85E4
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartAxisScaleBreak, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Enabled, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.BreakLineType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.CollapsibleSpaceThreshold, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MaxNumberOfBreaks, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Spacing, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IncludeZero, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x000FA485 File Offset: 0x000F8685
		internal bool EvaluateEnabled(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisScaleBreakEnabledExpression(this, this.m_chart.Name);
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000FA4AB File Offset: 0x000F86AB
		internal ChartBreakLineType EvaluateBreakLineType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartBreakLineType(context.ReportRuntime.EvaluateChartAxisScaleBreakBreakLineTypeExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x000FA4DC File Offset: 0x000F86DC
		internal int EvaluateCollapsibleSpaceThreshold(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisScaleBreakCollapsibleSpaceThresholdExpression(this, this.m_chart.Name);
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x000FA502 File Offset: 0x000F8702
		internal int EvaluateMaxNumberOfBreaks(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisScaleBreakMaxNumberOfBreaksExpression(this, this.m_chart.Name);
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x000FA528 File Offset: 0x000F8728
		internal double EvaluateSpacing(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisScaleBreakSpacingExpression(this, this.m_chart.Name);
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x000FA54E File Offset: 0x000F874E
		internal string EvaluateIncludeZero(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisScaleBreakIncludeZeroExpression(this, this.m_chart.Name);
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x000FA574 File Offset: 0x000F8774
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartAxisScaleBreak.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.IncludeZero)
				{
					if (memberName != MemberName.Enabled)
					{
						switch (memberName)
						{
						case MemberName.BreakLineType:
							writer.Write(this.m_breakLineType);
							break;
						case MemberName.CollapsibleSpaceThreshold:
							writer.Write(this.m_collapsibleSpaceThreshold);
							break;
						case MemberName.MaxNumberOfBreaks:
							writer.Write(this.m_maxNumberOfBreaks);
							break;
						case MemberName.Spacing:
							writer.Write(this.m_spacing);
							break;
						default:
							Global.Tracer.Assert(false);
							break;
						}
					}
					else
					{
						writer.Write(this.m_enabled);
					}
				}
				else
				{
					writer.Write(this.m_includeZero);
				}
			}
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x000FA648 File Offset: 0x000F8848
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartAxisScaleBreak.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.IncludeZero)
				{
					if (memberName != MemberName.Enabled)
					{
						switch (memberName)
						{
						case MemberName.BreakLineType:
							this.m_breakLineType = (ExpressionInfo)reader.ReadRIFObject();
							break;
						case MemberName.CollapsibleSpaceThreshold:
							this.m_collapsibleSpaceThreshold = (ExpressionInfo)reader.ReadRIFObject();
							break;
						case MemberName.MaxNumberOfBreaks:
							this.m_maxNumberOfBreaks = (ExpressionInfo)reader.ReadRIFObject();
							break;
						case MemberName.Spacing:
							this.m_spacing = (ExpressionInfo)reader.ReadRIFObject();
							break;
						default:
							Global.Tracer.Assert(false);
							break;
						}
					}
					else
					{
						this.m_enabled = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_includeZero = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x000FA73A File Offset: 0x000F893A
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x000FA744 File Offset: 0x000F8944
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartAxisScaleBreak;
		}

		// Token: 0x04001B9A RID: 7066
		private ExpressionInfo m_enabled;

		// Token: 0x04001B9B RID: 7067
		private ExpressionInfo m_breakLineType;

		// Token: 0x04001B9C RID: 7068
		private ExpressionInfo m_collapsibleSpaceThreshold;

		// Token: 0x04001B9D RID: 7069
		private ExpressionInfo m_maxNumberOfBreaks;

		// Token: 0x04001B9E RID: 7070
		private ExpressionInfo m_spacing;

		// Token: 0x04001B9F RID: 7071
		private ExpressionInfo m_includeZero;

		// Token: 0x04001BA0 RID: 7072
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartAxisScaleBreak.GetDeclaration();

		// Token: 0x04001BA1 RID: 7073
		[NonSerialized]
		private ChartAxisScaleBreakExprHost m_exprHost;
	}
}
