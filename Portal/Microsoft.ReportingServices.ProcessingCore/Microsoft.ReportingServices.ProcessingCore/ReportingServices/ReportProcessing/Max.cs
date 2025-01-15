using System;
using System.Globalization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200066B RID: 1643
	internal sealed class Max : DataAggregate
	{
		// Token: 0x06005AD7 RID: 23255 RVA: 0x00175AA1 File Offset: 0x00173CA1
		internal Max(CompareInfo compareInfo, CompareOptions compareOptions)
		{
			this.m_currentMax = null;
			this.m_compareInfo = compareInfo;
			this.m_compareOptions = compareOptions;
		}

		// Token: 0x06005AD8 RID: 23256 RVA: 0x00175ABE File Offset: 0x00173CBE
		internal override void Init()
		{
			this.m_currentMax = null;
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x00175AC8 File Offset: 0x00173CC8
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
			if (!DataAggregate.IsVariant(typeCode))
			{
				iErrorContext.Register(ProcessingErrorCode.rsMinMaxOfNonSortableData, Severity.Warning, Array.Empty<string>());
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
			if (this.m_currentMax == null)
			{
				this.m_currentMax = obj;
				return;
			}
			try
			{
				if (ReportProcessing.CompareTo(this.m_currentMax, obj, this.m_compareInfo, this.m_compareOptions) < 0)
				{
					this.m_currentMax = obj;
				}
			}
			catch
			{
				iErrorContext.Register(ProcessingErrorCode.rsMinMaxOfNonSortableData, Severity.Warning, Array.Empty<string>());
			}
		}

		// Token: 0x06005ADA RID: 23258 RVA: 0x00175BB0 File Offset: 0x00173DB0
		internal override object Result()
		{
			return this.m_currentMax;
		}

		// Token: 0x04002F34 RID: 12084
		private DataAggregate.DataTypeCode m_expressionType;

		// Token: 0x04002F35 RID: 12085
		private object m_currentMax;

		// Token: 0x04002F36 RID: 12086
		private CompareInfo m_compareInfo;

		// Token: 0x04002F37 RID: 12087
		private CompareOptions m_compareOptions;
	}
}
