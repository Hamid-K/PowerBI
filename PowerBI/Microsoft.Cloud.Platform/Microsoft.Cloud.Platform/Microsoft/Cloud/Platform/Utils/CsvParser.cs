using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001CE RID: 462
	public class CsvParser<TRecord> where TRecord : class, new()
	{
		// Token: 0x06000BD3 RID: 3027 RVA: 0x00028CC4 File Offset: 0x00026EC4
		public CsvParser()
		{
			Type typeFromHandle = typeof(TRecord);
			this.m_properties = new List<CsvParser<TRecord>.PropertyTypeInfo>();
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				TypeConverterAttribute typeConverterAttribute = propertyInfo.GetCustomAttributes(typeof(TypeConverterAttribute), true).SingleOrDefault<object>() as TypeConverterAttribute;
				TypeConverter typeConverter = ((typeConverterAttribute != null) ? ((TypeConverter)DynamicLoader.FastCreateInstance(Type.GetType(typeConverterAttribute.ConverterTypeName))) : TypeDescriptor.GetConverter(propertyInfo.PropertyType));
				this.m_properties.Add(new CsvParser<TRecord>.PropertyTypeInfo
				{
					PropertyInfo = propertyInfo,
					TypeConverter = typeConverter
				});
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00028D70 File Offset: 0x00026F70
		public TRecord ParseLine(string line)
		{
			TRecord trecord = new TRecord();
			string[] array = CsvParser<TRecord>.CsvSplit(line);
			if (array.Length < this.m_properties.Count)
			{
				throw new CsvFormatInvalidNumberOfFieldsException(array.Length, this.m_properties.Count);
			}
			if (array.Length > this.m_properties.Count)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Record contains extra fields. Actual fields count: {0}, expected: {1}. Extra fields are ignored.", new object[]
				{
					array.Length,
					this.m_properties.Count
				});
			}
			for (int i = 0; i < this.m_properties.Count; i++)
			{
				CsvParser<TRecord>.PropertyTypeInfo propertyTypeInfo = this.m_properties[i];
				try
				{
					object obj = propertyTypeInfo.TypeConverter.ConvertFromString(array[i].Replace("\"\"", "\"").Trim());
					propertyTypeInfo.PropertyInfo.SetValue(trecord, obj, null);
				}
				catch (Exception ex)
				{
					throw new CsvFormatInvalidFieldException(propertyTypeInfo.PropertyInfo.Name, array[i].ToString(), null, ex);
				}
			}
			return trecord;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00028E80 File Offset: 0x00027080
		public string ComposeLine(TRecord record)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.m_properties.Count; i++)
			{
				object value = this.m_properties[i].PropertyInfo.GetValue(record, null);
				if (value != null)
				{
					string text = this.m_properties[i].TypeConverter.ConvertToString(value);
					CsvEncoder.Encode(stringBuilder, text);
				}
				if (i < this.m_properties.Count - 1)
				{
					stringBuilder.Append(",");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00028F0C File Offset: 0x0002710C
		private static string[] CsvSplit(string source)
		{
			List<string> list = new List<string>();
			List<int> list2 = null;
			CsvParser<TRecord>.State state = CsvParser<TRecord>.State.AtBeginningOfToken;
			char[] array = source.ToCharArray();
			int num = 0;
			int num2 = array.Length;
			for (int i = 0; i < num2; i++)
			{
				switch (state)
				{
				case CsvParser<TRecord>.State.AtBeginningOfToken:
					if (array[i] == '"')
					{
						state = CsvParser<TRecord>.State.InQuotedToken;
						list2 = new List<int>();
					}
					else if (array[i] == ',')
					{
						list.Add("");
						num = i + 1;
					}
					else
					{
						state = CsvParser<TRecord>.State.InNonQuotedToken;
					}
					break;
				case CsvParser<TRecord>.State.InNonQuotedToken:
					if (array[i] == ',')
					{
						list.Add(source.Substring(num, i - num));
						state = CsvParser<TRecord>.State.AtBeginningOfToken;
						num = i + 1;
					}
					break;
				case CsvParser<TRecord>.State.InQuotedToken:
					if (array[i] == '"')
					{
						if (i + 1 < num2 && array[i + 1] == '"')
						{
							i++;
						}
						else
						{
							state = CsvParser<TRecord>.State.ExpectingComma;
						}
					}
					else if (array[i] == '\\')
					{
						state = CsvParser<TRecord>.State.InEscapedCharacter;
						list2.Add(i - num);
					}
					break;
				case CsvParser<TRecord>.State.ExpectingComma:
				{
					if (array[i] != ',')
					{
						throw new CsvFormatException("Expecting comma");
					}
					string text = source.Substring(num, i - num);
					foreach (int num3 in list2.Reverse<int>())
					{
						text = text.Remove(num3, 1);
					}
					list.Add(text.Substring(1, text.Length - 2));
					state = CsvParser<TRecord>.State.AtBeginningOfToken;
					num = i + 1;
					break;
				}
				case CsvParser<TRecord>.State.InEscapedCharacter:
					state = CsvParser<TRecord>.State.InQuotedToken;
					break;
				}
			}
			switch (state)
			{
			case CsvParser<TRecord>.State.AtBeginningOfToken:
				list.Add("");
				return list.ToArray();
			case CsvParser<TRecord>.State.InNonQuotedToken:
				list.Add(source.Substring(num, source.Length - num));
				return list.ToArray();
			case CsvParser<TRecord>.State.InQuotedToken:
				throw new CsvFormatException("Expecting ending quote");
			case CsvParser<TRecord>.State.ExpectingComma:
			{
				string text2 = source.Substring(num, source.Length - num);
				foreach (int num4 in list2.Reverse<int>())
				{
					text2 = text2.Remove(num4, 1);
				}
				list.Add(text2.Substring(1, text2.Length - 2));
				return list.ToArray();
			}
			case CsvParser<TRecord>.State.InEscapedCharacter:
				throw new CsvFormatException("Expecting escaped character");
			default:
				throw new CsvFormatException("Unexpected error");
			}
		}

		// Token: 0x04000491 RID: 1169
		private IList<CsvParser<TRecord>.PropertyTypeInfo> m_properties;

		// Token: 0x02000685 RID: 1669
		private enum State
		{
			// Token: 0x0400126C RID: 4716
			AtBeginningOfToken,
			// Token: 0x0400126D RID: 4717
			InNonQuotedToken,
			// Token: 0x0400126E RID: 4718
			InQuotedToken,
			// Token: 0x0400126F RID: 4719
			ExpectingComma,
			// Token: 0x04001270 RID: 4720
			InEscapedCharacter
		}

		// Token: 0x02000686 RID: 1670
		private struct PropertyTypeInfo
		{
			// Token: 0x04001271 RID: 4721
			public PropertyInfo PropertyInfo;

			// Token: 0x04001272 RID: 4722
			public TypeConverter TypeConverter;
		}
	}
}
