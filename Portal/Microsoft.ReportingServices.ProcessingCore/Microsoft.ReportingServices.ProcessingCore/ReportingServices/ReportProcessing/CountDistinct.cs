using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200066E RID: 1646
	internal sealed class CountDistinct : DataAggregate
	{
		// Token: 0x06005AE3 RID: 23267 RVA: 0x00175D2C File Offset: 0x00173F2C
		internal override void Init()
		{
			this.m_distinctValues.Clear();
		}

		// Token: 0x06005AE4 RID: 23268 RVA: 0x00175D3C File Offset: 0x00173F3C
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
				iErrorContext.Register(ProcessingErrorCode.rsInvalidExpressionDataType, Severity.Warning, Array.Empty<string>());
				throw new InvalidOperationException();
			}
			if (!this.m_distinctValues.ContainsKey(obj))
			{
				this.m_distinctValues.Add(obj, null);
			}
		}

		// Token: 0x06005AE5 RID: 23269 RVA: 0x00175DB5 File Offset: 0x00173FB5
		internal override object Result()
		{
			return this.m_distinctValues.Count;
		}

		// Token: 0x04002F3D RID: 12093
		private Hashtable m_distinctValues = new Hashtable();
	}
}
