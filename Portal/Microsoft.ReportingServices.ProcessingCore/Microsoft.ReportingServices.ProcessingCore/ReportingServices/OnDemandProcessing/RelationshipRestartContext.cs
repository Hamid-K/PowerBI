using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007FC RID: 2044
	internal sealed class RelationshipRestartContext : RestartContext
	{
		// Token: 0x060071ED RID: 29165 RVA: 0x001D8FD9 File Offset: 0x001D71D9
		public RelationshipRestartContext(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo[] expressions, Microsoft.ReportingServices.RdlExpressions.VariantResult[] values, SortDirection[] sortDirections, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet idcDataSet)
			: base(RestartMode.Query)
		{
			this.m_expressions = expressions;
			this.m_values = values;
			this.m_idcDataSet = idcDataSet;
			this.m_sortDirections = sortDirections;
			this.NormalizeValues(this.m_values);
		}

		// Token: 0x060071EE RID: 29166 RVA: 0x001D900C File Offset: 0x001D720C
		public override RowSkippingControlFlag DoesNotMatchRowRecordField(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.RecordField[] recordFields)
		{
			for (int i = 0; i < this.m_expressions.Length; i++)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = this.m_expressions[i];
				object value = this.m_values[i].Value;
				Microsoft.ReportingServices.ReportIntermediateFormat.RecordField recordField = recordFields[expressionInfo.FieldIndex];
				bool flag = this.m_sortDirections[i] == SortDirection.Ascending;
				RowSkippingControlFlag rowSkippingControlFlag = base.CompareFieldWithScopeValueAndStopOnInequality(odpContext, recordField, value, flag, ObjectType.DataSet, this.m_idcDataSet.Name, "Relationship.QueryRestart");
				if (rowSkippingControlFlag != RowSkippingControlFlag.ExactMatch)
				{
					return rowSkippingControlFlag;
				}
			}
			return RowSkippingControlFlag.ExactMatch;
		}

		// Token: 0x060071EF RID: 29167 RVA: 0x001D9084 File Offset: 0x001D7284
		private void NormalizeValues(Microsoft.ReportingServices.RdlExpressions.VariantResult[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i].Value is DBNull)
				{
					values[i].Value = null;
				}
			}
		}

		// Token: 0x060071F0 RID: 29168 RVA: 0x001D90C0 File Offset: 0x001D72C0
		public override List<ScopeValueFieldName> GetScopeValueFieldNameCollection(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet)
		{
			List<ScopeValueFieldName> list = new List<ScopeValueFieldName>();
			for (int i = 0; i < this.m_expressions.Length; i++)
			{
				string dataField = dataSet.Fields[this.m_expressions[i].FieldIndex].DataField;
				list.Add(new ScopeValueFieldName(dataField, this.m_values[i].Value));
			}
			return list;
		}

		// Token: 0x060071F1 RID: 29169 RVA: 0x001D9122 File Offset: 0x001D7322
		public override void TraceStartAtRecoveryMessage()
		{
		}

		// Token: 0x04003A92 RID: 14994
		private Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo[] m_expressions;

		// Token: 0x04003A93 RID: 14995
		private Microsoft.ReportingServices.RdlExpressions.VariantResult[] m_values;

		// Token: 0x04003A94 RID: 14996
		private readonly Microsoft.ReportingServices.ReportIntermediateFormat.DataSet m_idcDataSet;

		// Token: 0x04003A95 RID: 14997
		private SortDirection[] m_sortDirections;
	}
}
