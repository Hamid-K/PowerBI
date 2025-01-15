using System;
using System.ComponentModel;
using System.Globalization;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000068 RID: 104
	internal class SqlConnectionEncryptOptionConverter : TypeConverter
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x00017D7D File Offset: 0x00015F7D
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00017D9B File Offset: 0x00015F9B
		public override bool CanConvertTo(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00017DB9 File Offset: 0x00015FB9
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return SqlConnectionEncryptOption.Parse(value.ToString());
			}
			throw ADP.ConvertFailed(value.GetType(), typeof(SqlConnectionEncryptOption), null);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00017DE5 File Offset: 0x00015FE5
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			throw ADP.ConvertFailed(value.GetType(), typeof(SqlConnectionEncryptOption), null);
		}
	}
}
