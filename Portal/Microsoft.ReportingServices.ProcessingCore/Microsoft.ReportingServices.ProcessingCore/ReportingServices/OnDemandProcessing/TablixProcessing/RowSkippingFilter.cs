using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008B4 RID: 2228
	internal sealed class RowSkippingFilter
	{
		// Token: 0x06007988 RID: 31112 RVA: 0x001F47FF File Offset: 0x001F29FF
		public RowSkippingFilter(OnDemandProcessingContext odpContext, IRIFReportDataScope scope, List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo> expressions, List<object> values)
		{
			this.m_odpContext = odpContext;
			this.m_scope = scope;
			this.m_expressions = expressions;
			this.m_values = values;
		}

		// Token: 0x06007989 RID: 31113 RVA: 0x001F4824 File Offset: 0x001F2A24
		[Conditional("DEBUG")]
		private void ValidateExpressionsAndValues(List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo> expressions, List<object> values)
		{
			Global.Tracer.Assert(expressions != null && values != null && expressions.Count == values.Count, "Invalid expressions or values");
			foreach (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo in expressions)
			{
				Global.Tracer.Assert(expressionInfo.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Field, "Only simple field reference expressions can be row skipping filters.");
			}
		}

		// Token: 0x0600798A RID: 31114 RVA: 0x001F48AC File Offset: 0x001F2AAC
		public bool ShouldSkipCurrentRow()
		{
			FieldsImpl fieldsImpl = this.m_odpContext.ReportObjectModel.FieldsImpl;
			bool flag = true;
			int num = 0;
			while (num < this.m_expressions.Count && flag)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = this.m_expressions[num];
				if (expressionInfo.FieldIndex < 0)
				{
					flag = false;
				}
				else
				{
					FieldImpl fieldImpl = fieldsImpl[expressionInfo.FieldIndex];
					if (fieldImpl.FieldStatus != DataFieldStatus.None)
					{
						return false;
					}
					flag = this.m_odpContext.CompareAndStopOnError(this.m_values[num], fieldImpl.Value, this.m_scope.DataScopeObjectType, this.m_scope.Name, "GroupExpression", false) == 0;
				}
				num++;
			}
			return flag;
		}

		// Token: 0x04003CFF RID: 15615
		private readonly List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo> m_expressions;

		// Token: 0x04003D00 RID: 15616
		private readonly List<object> m_values;

		// Token: 0x04003D01 RID: 15617
		private readonly OnDemandProcessingContext m_odpContext;

		// Token: 0x04003D02 RID: 15618
		private readonly IRIFReportDataScope m_scope;
	}
}
