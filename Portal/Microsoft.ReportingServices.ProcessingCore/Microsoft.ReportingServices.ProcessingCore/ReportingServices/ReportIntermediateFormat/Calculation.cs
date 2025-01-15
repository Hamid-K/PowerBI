using System;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003C2 RID: 962
	internal sealed class Calculation
	{
		// Token: 0x060026DC RID: 9948 RVA: 0x000B9E3D File Offset: 0x000B803D
		internal Calculation(IInstancePath parentInstancePath, string name, ExpressionInfo expression)
		{
			this.m_parentInstancePath = parentInstancePath;
			this.m_name = name;
			this.m_expression = expression;
		}

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x000B9E5A File Offset: 0x000B805A
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x000B9E62 File Offset: 0x000B8062
		internal ExpressionInfo Expression
		{
			get
			{
				return this.m_expression;
			}
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x000B9E6A File Offset: 0x000B806A
		internal void Initialize(InitializationContext context)
		{
			if (this.m_expression != null)
			{
				this.m_expression.Initialize(this.m_name, context);
			}
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x000B9E86 File Offset: 0x000B8086
		internal object EvaluateCalculationValue(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_parentInstancePath, instance);
			return context.ReportRuntime.EvaluateDataShapeCalculationValue(this);
		}

		// Token: 0x04001664 RID: 5732
		private readonly IInstancePath m_parentInstancePath;

		// Token: 0x04001665 RID: 5733
		private readonly string m_name;

		// Token: 0x04001666 RID: 5734
		private readonly ExpressionInfo m_expression;
	}
}
