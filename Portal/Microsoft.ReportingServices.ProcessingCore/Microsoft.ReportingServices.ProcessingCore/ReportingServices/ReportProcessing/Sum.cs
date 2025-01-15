using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000669 RID: 1641
	internal class Sum : DataAggregate
	{
		// Token: 0x06005ACF RID: 23247 RVA: 0x001758D3 File Offset: 0x00173AD3
		internal override void Init()
		{
			this.m_currentTotalType = DataAggregate.DataTypeCode.Null;
			this.m_currentTotal = null;
		}

		// Token: 0x06005AD0 RID: 23248 RVA: 0x001758E4 File Offset: 0x00173AE4
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			Global.Tracer.Assert(expressions != null);
			Global.Tracer.Assert(1 == expressions.Length);
			object obj = expressions[0];
			DataAggregate.DataTypeCode typeCode = DataAggregate.GetTypeCode(obj);
			if (DataAggregate.IsNull(typeCode))
			{
				return;
			}
			if (!DataTypeUtility.IsNumeric(typeCode))
			{
				iErrorContext.Register(ProcessingErrorCode.rsAggregateOfNonNumericData, Severity.Warning, Array.Empty<string>());
				throw new InvalidOperationException();
			}
			if (this.m_expressionType == DataAggregate.DataTypeCode.Null)
			{
				this.m_expressionType = typeCode;
			}
			else if (typeCode != this.m_expressionType)
			{
				iErrorContext.Register(ProcessingErrorCode.rsAggregateOfMixedDataTypes, Severity.Warning, Array.Empty<string>());
				throw new InvalidOperationException();
			}
			DataAggregate.ConvertToDoubleOrDecimal(typeCode, obj, out typeCode, out obj);
			if (this.m_currentTotal == null)
			{
				this.m_currentTotalType = typeCode;
				this.m_currentTotal = obj;
				return;
			}
			this.m_currentTotal = DataAggregate.Add(this.m_currentTotalType, this.m_currentTotal, typeCode, obj);
		}

		// Token: 0x06005AD1 RID: 23249 RVA: 0x001759AE File Offset: 0x00173BAE
		internal override object Result()
		{
			return this.m_currentTotal;
		}

		// Token: 0x04002F30 RID: 12080
		private DataAggregate.DataTypeCode m_expressionType;

		// Token: 0x04002F31 RID: 12081
		protected DataAggregate.DataTypeCode m_currentTotalType;

		// Token: 0x04002F32 RID: 12082
		protected object m_currentTotal;
	}
}
