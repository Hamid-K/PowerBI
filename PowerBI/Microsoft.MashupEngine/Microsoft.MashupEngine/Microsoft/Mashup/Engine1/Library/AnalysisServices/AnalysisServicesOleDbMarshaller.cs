using System;
using System.Data.OleDb;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F2D RID: 3885
	internal static class AnalysisServicesOleDbMarshaller
	{
		// Token: 0x060066CE RID: 26318 RVA: 0x00161C28 File Offset: 0x0015FE28
		public static IValueReference Marshal(OleDbType oledbType, object obj, IResource resource)
		{
			if (obj == null || obj is DBNull)
			{
				return Value.Null;
			}
			if (oledbType > OleDbType.Guid)
			{
				switch (oledbType)
				{
				case OleDbType.Binary:
					break;
				case OleDbType.Char:
				case OleDbType.WChar:
					goto IL_01A0;
				case OleDbType.Numeric:
				case OleDbType.VarNumeric:
					goto IL_0164;
				case (OleDbType)132:
				case (OleDbType)136:
				case (OleDbType)137:
					goto IL_0234;
				case OleDbType.DBDate:
					return DateValue.New((DateTime)obj);
				case OleDbType.DBTime:
					return TimeValue.New((TimeSpan)obj);
				case OleDbType.DBTimeStamp:
					goto IL_017C;
				case OleDbType.PropVariant:
					goto IL_0201;
				default:
					if (oledbType - OleDbType.VarChar <= 3)
					{
						goto IL_01A0;
					}
					if (oledbType - OleDbType.VarBinary > 1)
					{
						goto IL_0234;
					}
					break;
				}
				return BinaryValue.New((byte[])obj);
			}
			switch (oledbType)
			{
			case OleDbType.Empty:
				return Value.Null;
			case (OleDbType)1:
			case OleDbType.IDispatch:
			case OleDbType.IUnknown:
			case (OleDbType)15:
				goto IL_0234;
			case OleDbType.SmallInt:
				return NumberValue.New((int)((short)obj));
			case OleDbType.Integer:
				return NumberValue.New((int)obj);
			case OleDbType.Single:
				return NumberValue.New((double)((float)obj));
			case OleDbType.Double:
				return NumberValue.New((double)obj);
			case OleDbType.Currency:
			case OleDbType.Decimal:
				break;
			case OleDbType.Date:
				return DateTimeValue.New((DateTime)obj);
			case OleDbType.BSTR:
				goto IL_01A0;
			case OleDbType.Error:
				return new ExceptionValueReference((obj is ValueException) ? ((ValueException)obj) : AnalysisServicesOleDbMarshaller.NewAdomdValueException(null, (Exception)obj, resource));
			case OleDbType.Boolean:
				return LogicalValue.New((bool)obj);
			case OleDbType.Variant:
				goto IL_0201;
			case OleDbType.TinyInt:
				return NumberValue.New((int)((sbyte)obj));
			case OleDbType.UnsignedTinyInt:
				return NumberValue.New((int)((byte)obj));
			case OleDbType.UnsignedSmallInt:
				return NumberValue.New((int)((ushort)obj));
			case OleDbType.UnsignedInt:
				return NumberValue.New((long)((ulong)((uint)obj)));
			case OleDbType.BigInt:
				return NumberValue.New((long)obj);
			case OleDbType.UnsignedBigInt:
				return NumberValue.New((ulong)obj);
			default:
				if (oledbType == OleDbType.Filetime)
				{
					goto IL_017C;
				}
				if (oledbType != OleDbType.Guid)
				{
					goto IL_0234;
				}
				return TextValue.New(((Guid)obj).ToString());
			}
			IL_0164:
			return NumberValue.New((decimal)obj);
			IL_017C:
			return DateTimeValue.New((DateTime)obj);
			IL_01A0:
			return TextValue.New((string)obj);
			IL_0201:
			if (obj == null)
			{
				return Value.Null;
			}
			if (obj.GetType().TryGetOleDbType(out oledbType))
			{
				return AnalysisServicesOleDbMarshaller.Marshal(oledbType, obj, resource);
			}
			if (obj is Exception)
			{
				return AnalysisServicesOleDbMarshaller.Marshal(OleDbType.Error, obj, resource);
			}
			IL_0234:
			string text = "Could not marshal the object of type: ";
			Type type = obj.GetType();
			throw new InvalidOperationException(text + ((type != null) ? type.ToString() : null) + ".");
		}

		// Token: 0x060066CF RID: 26319 RVA: 0x00161E8F File Offset: 0x0016008F
		private static ValueException NewAdomdValueException(IEngineHost engineHost, Exception exception, IResource resource)
		{
			return DataSourceException.NewDataSourceError(engineHost, exception.Message, resource, null, exception);
		}
	}
}
