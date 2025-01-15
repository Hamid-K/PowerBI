using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010F7 RID: 4343
	internal static class OleDbTypeExtensions
	{
		// Token: 0x06007191 RID: 29073 RVA: 0x00186590 File Offset: 0x00184790
		public static TypeValue GetTypeValue(this OleDbType oleDbType)
		{
			TypeValue typeValue;
			if (!oleDbType.TryGetTypeValue(out typeValue))
			{
				throw new InvalidOperationException(oleDbType.ToString());
			}
			return typeValue;
		}

		// Token: 0x06007192 RID: 29074 RVA: 0x001865BB File Offset: 0x001847BB
		public static bool TryGetTypeValue(this OleDbType oleDbType, out TypeValue typeValue)
		{
			return OleDbTypeExtensions.oleDbTypeValue.TryGetValue(oleDbType, out typeValue);
		}

		// Token: 0x06007193 RID: 29075 RVA: 0x001865C9 File Offset: 0x001847C9
		public static bool TryGetOleDbType(this Type type, out OleDbType oleDb)
		{
			return OleDbTypeExtensions.clrTypeOleDb.TryGetValue(type, out oleDb);
		}

		// Token: 0x04003ECE RID: 16078
		private static readonly Dictionary<Type, OleDbType> clrTypeOleDb = new Dictionary<Type, OleDbType>
		{
			{
				typeof(bool),
				OleDbType.Boolean
			},
			{
				typeof(byte),
				OleDbType.UnsignedTinyInt
			},
			{
				typeof(short),
				OleDbType.SmallInt
			},
			{
				typeof(float),
				OleDbType.Single
			},
			{
				typeof(double),
				OleDbType.Double
			},
			{
				typeof(sbyte),
				OleDbType.TinyInt
			},
			{
				typeof(ulong),
				OleDbType.UnsignedBigInt
			},
			{
				typeof(uint),
				OleDbType.UnsignedInt
			},
			{
				typeof(ushort),
				OleDbType.UnsignedSmallInt
			},
			{
				typeof(int),
				OleDbType.Integer
			},
			{
				typeof(long),
				OleDbType.BigInt
			},
			{
				typeof(decimal),
				OleDbType.Decimal
			},
			{
				typeof(DateTime),
				OleDbType.Filetime
			},
			{
				typeof(TimeSpan),
				OleDbType.DBTime
			},
			{
				typeof(string),
				OleDbType.LongVarWChar
			},
			{
				typeof(Guid),
				OleDbType.Guid
			},
			{
				typeof(byte[]),
				OleDbType.LongVarBinary
			}
		};

		// Token: 0x04003ECF RID: 16079
		private static readonly Dictionary<OleDbType, TypeValue> oleDbTypeValue = new Dictionary<OleDbType, TypeValue>
		{
			{
				OleDbType.BigInt,
				TypeValue.Int64
			},
			{
				OleDbType.Binary,
				TypeValue.Binary
			},
			{
				OleDbType.Boolean,
				TypeValue.Logical
			},
			{
				OleDbType.BSTR,
				TypeValue.Text
			},
			{
				OleDbType.Char,
				TypeValue.Character
			},
			{
				OleDbType.Currency,
				TypeValue.Currency
			},
			{
				OleDbType.Date,
				TypeValue.DateTime
			},
			{
				OleDbType.DBDate,
				TypeValue.Date
			},
			{
				OleDbType.DBTime,
				TypeValue.Time
			},
			{
				OleDbType.DBTimeStamp,
				TypeValue.DateTime
			},
			{
				OleDbType.Decimal,
				TypeValue.Decimal
			},
			{
				OleDbType.Double,
				TypeValue.Double
			},
			{
				OleDbType.Empty,
				TypeValue.Null
			},
			{
				OleDbType.Error,
				TypeValue.None
			},
			{
				OleDbType.Filetime,
				TypeValue.DateTime
			},
			{
				OleDbType.Guid,
				TypeValue.Guid
			},
			{
				OleDbType.Integer,
				TypeValue.Int32
			},
			{
				OleDbType.LongVarChar,
				TypeValue.Text
			},
			{
				OleDbType.LongVarBinary,
				TypeValue.Binary
			},
			{
				OleDbType.LongVarWChar,
				TypeValue.Text
			},
			{
				OleDbType.Numeric,
				TypeValue.Decimal
			},
			{
				OleDbType.PropVariant,
				TypeValue.Any
			},
			{
				OleDbType.Single,
				TypeValue.Single
			},
			{
				OleDbType.SmallInt,
				TypeValue.Int16
			},
			{
				OleDbType.TinyInt,
				TypeValue.Int8
			},
			{
				OleDbType.UnsignedTinyInt,
				TypeValue.Byte
			},
			{
				OleDbType.UnsignedSmallInt,
				TypeValue.Int32
			},
			{
				OleDbType.UnsignedBigInt,
				TypeValue.Decimal
			},
			{
				OleDbType.UnsignedInt,
				TypeValue.Int64
			},
			{
				OleDbType.VarNumeric,
				TypeValue.Decimal
			},
			{
				OleDbType.WChar,
				TypeValue.Character
			},
			{
				OleDbType.VarChar,
				TypeValue.Text
			},
			{
				OleDbType.VarWChar,
				TypeValue.Text
			},
			{
				OleDbType.VarBinary,
				TypeValue.Binary
			},
			{
				OleDbType.Variant,
				TypeValue.Any
			}
		};
	}
}
