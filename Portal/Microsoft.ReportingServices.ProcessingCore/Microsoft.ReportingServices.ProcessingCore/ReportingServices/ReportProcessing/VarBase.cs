using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000670 RID: 1648
	internal abstract class VarBase : DataAggregate
	{
		// Token: 0x06005AEB RID: 23275 RVA: 0x00175E08 File Offset: 0x00174008
		internal override void Init()
		{
			this.m_currentCount = 0U;
			this.m_sumOfXType = DataAggregate.DataTypeCode.Null;
			this.m_sumOfX = null;
			this.m_sumOfXSquared = null;
		}

		// Token: 0x06005AEC RID: 23276 RVA: 0x00175E28 File Offset: 0x00174028
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
			object obj2 = DataAggregate.Square(typeCode, obj);
			if (this.m_sumOfX == null)
			{
				this.m_sumOfXType = typeCode;
				this.m_sumOfX = obj;
				this.m_sumOfXSquared = obj2;
			}
			else
			{
				this.m_sumOfX = DataAggregate.Add(this.m_sumOfXType, this.m_sumOfX, typeCode, obj);
				this.m_sumOfXSquared = DataAggregate.Add(this.m_sumOfXType, this.m_sumOfXSquared, typeCode, obj2);
			}
			this.m_currentCount += 1U;
		}

		// Token: 0x04002F3F RID: 12095
		private DataAggregate.DataTypeCode m_expressionType;

		// Token: 0x04002F40 RID: 12096
		protected uint m_currentCount;

		// Token: 0x04002F41 RID: 12097
		protected DataAggregate.DataTypeCode m_sumOfXType;

		// Token: 0x04002F42 RID: 12098
		protected object m_sumOfX;

		// Token: 0x04002F43 RID: 12099
		protected object m_sumOfXSquared;
	}
}
