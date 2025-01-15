using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common.Serialization
{
	// Token: 0x0200000D RID: 13
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class VariantValueConverter
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002764 File Offset: 0x00000964
		internal static byte[] ConvertVariantValue(object o, out ClrTypeCode variantClrTypeCode)
		{
			RuntimeChecks.CheckValue(o, "o");
			RuntimeChecks.Check(o != DBNull.Value, "o must not be DBNull");
			variantClrTypeCode = TypeCodeMapping.GetTypeCodeFrom(o.GetType());
			ClrTypeCode clrTypeCode = variantClrTypeCode;
			int num;
			if (clrTypeCode != ClrTypeCode.String)
			{
				if (clrTypeCode != ClrTypeCode.ByteArray)
				{
					if (clrTypeCode != ClrTypeCode.CharArray)
					{
						num = TypeConversionUtil.GetLengthForType(variantClrTypeCode);
					}
					else
					{
						num = ((char[])o).Length * 2;
					}
				}
				else
				{
					num = ((byte[])o).Length;
				}
			}
			else
			{
				num = ((string)o).Length * 2;
			}
			return VariantValueConverter.ConvertVariantValue(variantClrTypeCode, o, num);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027F0 File Offset: 0x000009F0
		private unsafe static byte[] ConvertVariantValue(ClrTypeCode clrTypeCode, object value, int byteLength)
		{
			RuntimeChecks.Check(clrTypeCode != ClrTypeCode.Object, "Variant column type found where it is not supported.");
			if (clrTypeCode == ClrTypeCode.String)
			{
				return Encoding.Unicode.GetBytes((string)value);
			}
			if (clrTypeCode == ClrTypeCode.ByteArray)
			{
				return (byte[])value;
			}
			if (clrTypeCode != ClrTypeCode.CharArray)
			{
				byte[] array = new byte[byteLength];
				byte[] array2;
				byte* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				switch (clrTypeCode)
				{
				case ClrTypeCode.Boolean:
					*ptr = (((bool)value) ? 1 : 0);
					return array;
				case ClrTypeCode.SByte:
					*ptr = (byte)((sbyte)value);
					return array;
				case ClrTypeCode.Byte:
					*ptr = (byte)value;
					return array;
				case ClrTypeCode.Int16:
					*(short*)ptr = (short)value;
					return array;
				case ClrTypeCode.UInt16:
					*(short*)ptr = (short)((ushort)value);
					return array;
				case ClrTypeCode.Int32:
					*(int*)ptr = (int)value;
					return array;
				case ClrTypeCode.UInt32:
					*(int*)ptr = (int)((uint)value);
					return array;
				case ClrTypeCode.Int64:
					*(long*)ptr = (long)value;
					return array;
				case ClrTypeCode.UInt64:
					*(long*)ptr = (long)((ulong)value);
					return array;
				case ClrTypeCode.Single:
					*(float*)ptr = (float)value;
					return array;
				case ClrTypeCode.Double:
					*(double*)ptr = (double)value;
					return array;
				case ClrTypeCode.Decimal:
					*(decimal*)ptr = (decimal)value;
					return array;
				case ClrTypeCode.DateTime:
					*(DateTime*)ptr = (DateTime)value;
					return array;
				case ClrTypeCode.TimeSpan:
					*(TimeSpan*)ptr = (TimeSpan)value;
					return array;
				case ClrTypeCode.DateTimeOffset:
					*(DateTimeOffset*)ptr = (DateTimeOffset)value;
					return array;
				case ClrTypeCode.Guid:
					*(Guid*)ptr = (Guid)value;
					return array;
				case ClrTypeCode.Char:
					*(short*)ptr = (short)((char)value);
					return array;
				}
				RuntimeChecks.Fail(string.Format(CultureInfo.InvariantCulture, "Unsupported variant value found: {0}.", clrTypeCode.ToString()));
				return array;
			}
			byte[] array3 = new byte[byteLength];
			int num = 0;
			char[] array4 = (char[])value;
			for (int i = 0; i < array4.Length; i++)
			{
				byte[] bytes = BitConverter.GetBytes(array4[i]);
				array3[num++] = bytes[0];
				array3[num++] = bytes[1];
			}
			return array3;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A08 File Offset: 0x00000C08
		internal static object ReadVariantValue(ClrTypeCode clrTypeCode, byte[] varDataPage, int varDataOffset)
		{
			int num = -1;
			if (TypeConversionUtil.IsVariableLengthType(clrTypeCode))
			{
				num = BitConverter.ToInt32(varDataPage, varDataOffset);
				varDataOffset += 4;
			}
			return VariantValueConverter.ReadVariantValue(clrTypeCode, varDataPage, varDataOffset, num);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A38 File Offset: 0x00000C38
		private unsafe static object ReadVariantValue(ClrTypeCode clrTypeCode, byte[] varDataPage, int varDataOffset, int byteLength)
		{
			RuntimeChecks.Check(clrTypeCode != ClrTypeCode.Object, "Variant column type found where it is not supported.");
			if (clrTypeCode == ClrTypeCode.String)
			{
				return Encoding.Unicode.GetString(varDataPage, varDataOffset, byteLength);
			}
			if (clrTypeCode == ClrTypeCode.ByteArray)
			{
				byte[] array = new byte[byteLength];
				Buffer.BlockCopy(varDataPage, varDataOffset, array, 0, byteLength);
				return array;
			}
			if (clrTypeCode != ClrTypeCode.CharArray)
			{
				switch (clrTypeCode)
				{
				case ClrTypeCode.Boolean:
					return BitConverter.ToBoolean(varDataPage, varDataOffset);
				case ClrTypeCode.SByte:
					return (sbyte)varDataPage[varDataOffset];
				case ClrTypeCode.Byte:
					return varDataPage[varDataOffset];
				case ClrTypeCode.Int16:
					return BitConverter.ToInt16(varDataPage, varDataOffset);
				case ClrTypeCode.UInt16:
					return BitConverter.ToUInt16(varDataPage, varDataOffset);
				case ClrTypeCode.Int32:
					return BitConverter.ToInt32(varDataPage, varDataOffset);
				case ClrTypeCode.UInt32:
					return BitConverter.ToUInt32(varDataPage, varDataOffset);
				case ClrTypeCode.Int64:
					return BitConverter.ToInt64(varDataPage, varDataOffset);
				case ClrTypeCode.UInt64:
					return BitConverter.ToUInt64(varDataPage, varDataOffset);
				case ClrTypeCode.Single:
					return BitConverter.ToSingle(varDataPage, varDataOffset);
				case ClrTypeCode.Double:
					return BitConverter.ToDouble(varDataPage, varDataOffset);
				case ClrTypeCode.Decimal:
					fixed (byte[] array2 = varDataPage)
					{
						byte* ptr;
						if (varDataPage == null || array2.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array2[0];
						}
						return *(decimal*)(ptr + varDataOffset);
					}
				case ClrTypeCode.DateTime:
					fixed (byte[] array2 = varDataPage)
					{
						byte* ptr2;
						if (varDataPage == null || array2.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = &array2[0];
						}
						return *(DateTime*)(ptr2 + varDataOffset);
					}
				case ClrTypeCode.TimeSpan:
					fixed (byte[] array2 = varDataPage)
					{
						byte* ptr3;
						if (varDataPage == null || array2.Length == 0)
						{
							ptr3 = null;
						}
						else
						{
							ptr3 = &array2[0];
						}
						return *(TimeSpan*)(ptr3 + varDataOffset);
					}
				case ClrTypeCode.DateTimeOffset:
					fixed (byte[] array2 = varDataPage)
					{
						byte* ptr4;
						if (varDataPage == null || array2.Length == 0)
						{
							ptr4 = null;
						}
						else
						{
							ptr4 = &array2[0];
						}
						return *(DateTimeOffset*)(ptr4 + varDataOffset);
					}
				case ClrTypeCode.Guid:
					fixed (byte[] array2 = varDataPage)
					{
						byte* ptr5;
						if (varDataPage == null || array2.Length == 0)
						{
							ptr5 = null;
						}
						else
						{
							ptr5 = &array2[0];
						}
						return *(Guid*)(ptr5 + varDataOffset);
					}
				case ClrTypeCode.Char:
					return BitConverter.ToChar(varDataPage, varDataOffset);
				}
				RuntimeChecks.Fail(string.Format(CultureInfo.InvariantCulture, "Unsupported variant value found: {0}.", clrTypeCode.ToString()));
				throw RuntimeChecks.UnreachableCodepath("ReadVariantValue", "/src/Gateway/Pipeline/RowsetSerialization/VariantValueConverter.cs", 276, "Unreachable code path reached");
			}
			char[] array3 = new char[byteLength / 2];
			int num = varDataOffset;
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i] = BitConverter.ToChar(varDataPage, num);
				num += 2;
			}
			return array3;
		}
	}
}
