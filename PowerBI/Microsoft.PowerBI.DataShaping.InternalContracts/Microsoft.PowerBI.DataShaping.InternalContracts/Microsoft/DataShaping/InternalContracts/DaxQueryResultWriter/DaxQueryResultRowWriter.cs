using System;

namespace Microsoft.DataShaping.InternalContracts.DaxQueryResultWriter
{
	// Token: 0x02000033 RID: 51
	internal sealed class DaxQueryResultRowWriter : DaxQueryResultObjectWriterBase
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00004280 File Offset: 0x00002480
		internal void WriteValue(string name, object value)
		{
			if (value == null)
			{
				if (base.Settings.IncludeNulls)
				{
					base.Writer.BeginProperty(name);
					base.Writer.WriteValue(null);
				}
				return;
			}
			TypeCode typeCode = Type.GetTypeCode(value.GetType());
			if (typeCode != TypeCode.Boolean)
			{
				switch (typeCode)
				{
				case TypeCode.Int64:
					base.Writer.BeginProperty(name);
					base.Writer.WriteValue((long)value);
					return;
				case TypeCode.Double:
					base.Writer.BeginProperty(name);
					base.Writer.WriteValue((double)value);
					return;
				case TypeCode.Decimal:
					base.Writer.BeginProperty(name);
					base.Writer.WriteValue((decimal)value);
					return;
				case TypeCode.DateTime:
					base.Writer.BeginProperty(name);
					base.Writer.WriteValue((DateTime)value);
					return;
				case TypeCode.String:
					base.Writer.BeginProperty(name);
					base.Writer.WriteValue((string)value);
					return;
				}
				throw DaxQueryExecutionErrors.CreateDataTypeNotSupported(value.GetType());
			}
			base.Writer.BeginProperty(name);
			base.Writer.WriteValue((bool)value);
		}
	}
}
