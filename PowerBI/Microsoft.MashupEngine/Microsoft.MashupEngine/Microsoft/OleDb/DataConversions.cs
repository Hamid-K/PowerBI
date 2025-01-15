using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001E8A RID: 7818
	public static class DataConversions
	{
		// Token: 0x0600C146 RID: 49478 RVA: 0x0026DD26 File Offset: 0x0026BF26
		public static bool CanConvertStringToNativeString(DBTYPE destType)
		{
			return destType == DBTYPE.WSTR || destType == (DBTYPE)16514 || destType == DBTYPE.BSTR || destType == DBTYPE.VARIANT;
		}

		// Token: 0x0600C147 RID: 49479 RVA: 0x0026DD44 File Offset: 0x0026BF44
		public unsafe static DBSTATUS TryConvertStringToNativeString(string value, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			if (binding.DestType == DBTYPE.WSTR)
			{
				if (DbLength.GetLength(value).value + 2UL <= binding.DestMaxLength.value)
				{
					for (int i = 0; i < value.Length; i++)
					{
						*(short*)(destValue + (IntPtr)i * 2) = (short)value[i];
					}
					*(short*)(destValue + (IntPtr)value.Length * 2) = 0;
					destLength = DbLength.GetLength(value);
					return DBSTATUS.S_OK;
				}
				destLength = DbLength.Zero;
				return DBSTATUS.E_DATAOVERFLOW;
			}
			else
			{
				if (binding.DestType == (DBTYPE)16514)
				{
					*(IntPtr*)destValue = Marshal.StringToCoTaskMemUni(value).ToPointer();
					destLength = DbLength.GetLength(value);
					return DBSTATUS.S_OK;
				}
				if (binding.DestType == DBTYPE.BSTR)
				{
					*(IntPtr*)destValue = Marshal.StringToBSTR(value).ToPointer();
					destLength = DbLength.GetLength(value);
					return DBSTATUS.S_OK;
				}
				if (binding.DestType == DBTYPE.VARIANT)
				{
					Variant.Init((VARIANT*)destValue);
					((VARIANT*)destValue)->vt = VARTYPE.BSTR;
					((VARIANT*)destValue)->valuePtr = Marshal.StringToBSTR(value).ToPointer();
					destLength = DbLength.Variant;
					return DBSTATUS.S_OK;
				}
				destLength = DbLength.Zero;
				return DBSTATUS.E_CANTCONVERTVALUE;
			}
		}

		// Token: 0x0600C148 RID: 49480 RVA: 0x0026DE58 File Offset: 0x0026C058
		public unsafe static DBSTATUS ConvertOADateToDate(double? dateAsDouble, byte* destValue, out DBLENGTH destLength)
		{
			destLength = DbLength.Double;
			if (dateAsDouble != null)
			{
				*(double*)destValue = dateAsDouble.Value;
				return DBSTATUS.S_OK;
			}
			*(double*)destValue = 0.0;
			return DBSTATUS.S_ISNULL;
		}

		// Token: 0x0600C149 RID: 49481 RVA: 0x0026DE88 File Offset: 0x0026C088
		public unsafe static DBSTATUS ConvertOADateToVariant(double? dateAsDouble, byte* destValue, out DBLENGTH destLength)
		{
			if (dateAsDouble != null)
			{
				Variant.SetDate((VARIANT*)destValue, dateAsDouble.Value);
			}
			else
			{
				Variant.SetEmpty((VARIANT*)destValue);
			}
			destLength = DbLength.Variant;
			return DBSTATUS.S_OK;
		}
	}
}
