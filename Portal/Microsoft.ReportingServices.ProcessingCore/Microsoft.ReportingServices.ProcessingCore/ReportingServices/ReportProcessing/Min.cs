using System;
using System.Globalization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200066C RID: 1644
	internal sealed class Min : DataAggregate
	{
		// Token: 0x06005ADB RID: 23259 RVA: 0x00175BB8 File Offset: 0x00173DB8
		internal Min(CompareInfo compareInfo, CompareOptions compareOptions)
		{
			this.m_currentMin = null;
			this.m_compareInfo = compareInfo;
			this.m_compareOptions = compareOptions;
		}

		// Token: 0x06005ADC RID: 23260 RVA: 0x00175BD5 File Offset: 0x00173DD5
		internal override void Init()
		{
			this.m_currentMin = null;
		}

		// Token: 0x06005ADD RID: 23261 RVA: 0x00175BE0 File Offset: 0x00173DE0
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
			if (this.m_currentMin == null)
			{
				this.m_currentMin = obj;
				return;
			}
			try
			{
				if (ReportProcessing.CompareTo(this.m_currentMin, obj, this.m_compareInfo, this.m_compareOptions) > 0)
				{
					this.m_currentMin = obj;
				}
			}
			catch
			{
				iErrorContext.Register(ProcessingErrorCode.rsMinMaxOfNonSortableData, Severity.Warning, Array.Empty<string>());
			}
		}

		// Token: 0x06005ADE RID: 23262 RVA: 0x00175CC8 File Offset: 0x00173EC8
		internal override object Result()
		{
			return this.m_currentMin;
		}

		// Token: 0x04002F38 RID: 12088
		private DataAggregate.DataTypeCode m_expressionType;

		// Token: 0x04002F39 RID: 12089
		private object m_currentMin;

		// Token: 0x04002F3A RID: 12090
		private CompareInfo m_compareInfo;

		// Token: 0x04002F3B RID: 12091
		private CompareOptions m_compareOptions;
	}
}
