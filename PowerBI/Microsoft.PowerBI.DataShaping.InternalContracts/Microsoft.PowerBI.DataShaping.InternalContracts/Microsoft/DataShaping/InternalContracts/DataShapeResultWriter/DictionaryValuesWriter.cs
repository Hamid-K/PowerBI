using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000046 RID: 70
	internal sealed class DictionaryValuesWriter : DsrObjectWriterBase
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00004BB0 File Offset: 0x00002DB0
		internal void WriteValues(string identifier, bool isTypeEncoded, IReadOnlyList<object> values)
		{
			base.Writer.BeginProperty(identifier);
			base.Writer.BeginArray();
			foreach (object obj in values)
			{
				if (isTypeEncoded)
				{
					base.Writer.WriteTypeEncodedValue(obj);
				}
				else
				{
					base.Writer.WriteSimpleEncodedValue(obj);
				}
			}
			base.Writer.EndArray();
		}
	}
}
