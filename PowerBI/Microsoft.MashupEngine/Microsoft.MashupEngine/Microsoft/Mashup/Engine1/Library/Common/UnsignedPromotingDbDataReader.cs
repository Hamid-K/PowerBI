using System;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200115D RID: 4445
	internal static class UnsignedPromotingDbDataReader
	{
		// Token: 0x0600746D RID: 29805 RVA: 0x0018FC44 File Offset: 0x0018DE44
		public static DbDataReaderWithTableSchema New(DbDataReaderWithTableSchema reader)
		{
			int fieldCount = reader.FieldCount;
			Func<object, object>[] array = null;
			Type[] array2 = null;
			for (int i = 0; i < fieldCount; i++)
			{
				Type type = reader.GetFieldType(i);
				Func<object, object> func = null;
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.UInt16:
					func = new Func<object, object>(UnsignedPromotingDbDataReader.PromoteUInt16);
					type = typeof(int);
					break;
				case TypeCode.UInt32:
					func = new Func<object, object>(UnsignedPromotingDbDataReader.PromoteUInt32);
					type = typeof(long);
					break;
				case TypeCode.UInt64:
					func = new Func<object, object>(UnsignedPromotingDbDataReader.PromoteUInt64);
					type = typeof(decimal);
					break;
				}
				if (func != null)
				{
					if (array == null)
					{
						array = new Func<object, object>[fieldCount];
						array2 = new Type[fieldCount];
					}
					array[i] = func;
					array2[i] = type;
				}
			}
			if (array == null)
			{
				return reader;
			}
			return new ValueAdapterDbDataReader(reader, array2, array);
		}

		// Token: 0x0600746E RID: 29806 RVA: 0x0018FD20 File Offset: 0x0018DF20
		private static object PromoteUInt16(object obj)
		{
			return (int)((ushort)obj);
		}

		// Token: 0x0600746F RID: 29807 RVA: 0x0018FD2D File Offset: 0x0018DF2D
		private static object PromoteUInt32(object obj)
		{
			return (long)((ulong)((uint)obj));
		}

		// Token: 0x06007470 RID: 29808 RVA: 0x0018FD3B File Offset: 0x0018DF3B
		private static object PromoteUInt64(object obj)
		{
			return (ulong)obj;
		}
	}
}
