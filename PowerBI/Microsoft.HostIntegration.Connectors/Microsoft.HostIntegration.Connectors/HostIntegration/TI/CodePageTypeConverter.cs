using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000750 RID: 1872
	public class CodePageTypeConverter : TypeConverter
	{
		// Token: 0x06003B54 RID: 15188 RVA: 0x000C736E File Offset: 0x000C556E
		private static void AddToCodePageTable(int codePage, string str)
		{
			CodePageTypeConverter.codePageTable[codePage] = str;
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x000C7384 File Offset: 0x000C5584
		static CodePageTypeConverter()
		{
			foreach (EncodingInfo encodingInfo in Encoding.GetEncodings())
			{
				CodePageTypeConverter.AddToCodePageTable(encodingInfo.CodePage, encodingInfo.CodePage.ToString() + " " + encodingInfo.DisplayName);
			}
			int[] array = new int[CodePageTypeConverter.codePageTable.Count];
			int j = 0;
			foreach (object obj in CodePageTypeConverter.codePageTable.Keys)
			{
				int num = (int)obj;
				array[j] = num;
				j++;
			}
			Array.Sort<int>(array);
			string[] array2 = new string[CodePageTypeConverter.codePageTable.Count];
			for (j = 0; j < array.Length; j++)
			{
				array2[j] = (string)CodePageTypeConverter.codePageTable[array[j]];
			}
			CodePageTypeConverter.defaultCodePages = new TypeConverter.StandardValuesCollection(array2);
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x000C749C File Offset: 0x000C569C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return CodePageTypeConverter.defaultCodePages;
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x000C74A3 File Offset: 0x000C56A3
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type t)
		{
			return t == typeof(string) || base.CanConvertFrom(context, t);
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x000C74C4 File Offset: 0x000C56C4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo info, object value)
		{
			if (value is string)
			{
				try
				{
					string text = (string)value;
					if (text.IndexOf(" ") != -1)
					{
						return int.Parse(text.Substring(0, text.IndexOf(" ")).Trim());
					}
					return int.Parse(text.Trim());
				}
				catch
				{
				}
				throw new ArgumentException("Can not convert '" + (string)value + "' to type Person");
			}
			return base.ConvertFrom(context, info, value);
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x000C7560 File Offset: 0x000C5760
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
		{
			if (destType == typeof(string) && value is int)
			{
				return CodePageTypeConverter.codePageTable[value];
			}
			return base.ConvertTo(context, culture, value, destType);
		}

		// Token: 0x0400238F RID: 9103
		private static Hashtable codePageTable = new Hashtable();

		// Token: 0x04002390 RID: 9104
		private static TypeConverter.StandardValuesCollection defaultCodePages;
	}
}
