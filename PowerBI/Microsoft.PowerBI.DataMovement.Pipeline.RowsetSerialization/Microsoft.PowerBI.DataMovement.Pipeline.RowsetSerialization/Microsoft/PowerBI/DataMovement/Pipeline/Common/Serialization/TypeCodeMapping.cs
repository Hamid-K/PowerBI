using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common.Serialization
{
	// Token: 0x0200000B RID: 11
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class TypeCodeMapping
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000023AB File Offset: 0x000005AB
		internal static ClrTypeCode GetTypeCodeFrom(Type type)
		{
			return TypeCodeMapping.TypeToTypeCodeMapping[type];
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023B8 File Offset: 0x000005B8
		internal static Type GetTypeFrom(ClrTypeCode typeCode)
		{
			return TypeCodeMapping.TypeCodeToTypeMapping[typeCode];
		}

		// Token: 0x04000014 RID: 20
		private static readonly Dictionary<Type, ClrTypeCode> TypeToTypeCodeMapping = new Dictionary<Type, ClrTypeCode>
		{
			{
				typeof(bool),
				ClrTypeCode.Boolean
			},
			{
				typeof(sbyte),
				ClrTypeCode.SByte
			},
			{
				typeof(byte),
				ClrTypeCode.Byte
			},
			{
				typeof(short),
				ClrTypeCode.Int16
			},
			{
				typeof(ushort),
				ClrTypeCode.UInt16
			},
			{
				typeof(int),
				ClrTypeCode.Int32
			},
			{
				typeof(uint),
				ClrTypeCode.UInt32
			},
			{
				typeof(long),
				ClrTypeCode.Int64
			},
			{
				typeof(ulong),
				ClrTypeCode.UInt64
			},
			{
				typeof(float),
				ClrTypeCode.Single
			},
			{
				typeof(double),
				ClrTypeCode.Double
			},
			{
				typeof(decimal),
				ClrTypeCode.Decimal
			},
			{
				typeof(DateTime),
				ClrTypeCode.DateTime
			},
			{
				typeof(string),
				ClrTypeCode.String
			},
			{
				typeof(TimeSpan),
				ClrTypeCode.TimeSpan
			},
			{
				typeof(DateTimeOffset),
				ClrTypeCode.DateTimeOffset
			},
			{
				typeof(byte[]),
				ClrTypeCode.ByteArray
			},
			{
				typeof(Guid),
				ClrTypeCode.Guid
			},
			{
				typeof(object),
				ClrTypeCode.Object
			},
			{
				typeof(char),
				ClrTypeCode.Char
			},
			{
				typeof(char[]),
				ClrTypeCode.CharArray
			}
		};

		// Token: 0x04000015 RID: 21
		private static readonly Dictionary<ClrTypeCode, Type> TypeCodeToTypeMapping = new Dictionary<ClrTypeCode, Type>
		{
			{
				ClrTypeCode.Boolean,
				typeof(bool)
			},
			{
				ClrTypeCode.SByte,
				typeof(sbyte)
			},
			{
				ClrTypeCode.Byte,
				typeof(byte)
			},
			{
				ClrTypeCode.Int16,
				typeof(short)
			},
			{
				ClrTypeCode.UInt16,
				typeof(ushort)
			},
			{
				ClrTypeCode.Int32,
				typeof(int)
			},
			{
				ClrTypeCode.UInt32,
				typeof(uint)
			},
			{
				ClrTypeCode.Int64,
				typeof(long)
			},
			{
				ClrTypeCode.UInt64,
				typeof(ulong)
			},
			{
				ClrTypeCode.Single,
				typeof(float)
			},
			{
				ClrTypeCode.Double,
				typeof(double)
			},
			{
				ClrTypeCode.Decimal,
				typeof(decimal)
			},
			{
				ClrTypeCode.DateTime,
				typeof(DateTime)
			},
			{
				ClrTypeCode.String,
				typeof(string)
			},
			{
				ClrTypeCode.TimeSpan,
				typeof(TimeSpan)
			},
			{
				ClrTypeCode.DateTimeOffset,
				typeof(DateTimeOffset)
			},
			{
				ClrTypeCode.ByteArray,
				typeof(byte[])
			},
			{
				ClrTypeCode.Guid,
				typeof(Guid)
			},
			{
				ClrTypeCode.Object,
				typeof(object)
			},
			{
				ClrTypeCode.Char,
				typeof(char)
			},
			{
				ClrTypeCode.CharArray,
				typeof(char[])
			}
		};
	}
}
