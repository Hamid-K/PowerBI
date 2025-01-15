using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x0200040E RID: 1038
	internal abstract class EnumListConverter : TypeConverter
	{
		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x060020FD RID: 8445
		internal abstract Array Values { get; }

		// Token: 0x060020FE RID: 8446 RVA: 0x000800D2 File Offset: 0x0007E2D2
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(this.Values);
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x0007FD42 File Offset: 0x0007DF42
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x000800DF File Offset: 0x0007E2DF
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return Enum.Parse(((IList)this.Values)[0].GetType(), (string)value);
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
