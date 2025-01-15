using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x0200040C RID: 1036
	public abstract class StringListConverter : TypeConverter
	{
		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x060020F2 RID: 8434 RVA: 0x00005C88 File Offset: 0x00003E88
		internal virtual string[] Values
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x0008000F File Offset: 0x0007E20F
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(this.Values);
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x00005BEF File Offset: 0x00003DEF
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x0008001C File Offset: 0x0007E21C
		public static void ValidReportString(string paramName, string[] stringValues, string defaultValue, string testValue, ref string propValue)
		{
			if (testValue != propValue)
			{
				if (testValue.Length > 0 && testValue[0] == '=')
				{
					propValue = testValue;
					return;
				}
				StringListConverter.ValidStandardValue(paramName, stringValues, defaultValue, testValue, ref propValue);
			}
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x00080050 File Offset: 0x0007E250
		public static void ValidStandardValue(string paramName, string[] stringValues, string defaultValue, string testValue, ref string propValue)
		{
			string text = null;
			if (testValue == null || testValue.Length == 0)
			{
				propValue = defaultValue;
				return;
			}
			foreach (string text2 in stringValues)
			{
				if (string.Compare(text2, testValue, StringComparison.OrdinalIgnoreCase) == 0)
				{
					text = text2;
					break;
				}
			}
			if (text == null || text.Length == 0)
			{
				throw new ArgumentOutOfRangeException(paramName, SRErrors.InvalidValue(testValue));
			}
			propValue = text;
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x0007FD42 File Offset: 0x0007DF42
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x000800AB File Offset: 0x0007E2AB
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return ((string)value).Trim();
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
