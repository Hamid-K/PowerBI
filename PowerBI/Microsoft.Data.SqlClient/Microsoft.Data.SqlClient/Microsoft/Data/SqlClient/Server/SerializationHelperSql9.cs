using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Data.Common;
using Microsoft.SqlServer.Server;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000142 RID: 322
	internal sealed class SerializationHelperSql9
	{
		// Token: 0x0600196A RID: 6506 RVA: 0x000027D1 File Offset: 0x000009D1
		private SerializationHelperSql9()
		{
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0006B214 File Offset: 0x00069414
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static int SizeInBytes(Type t)
		{
			return SerializationHelperSql9.SizeInBytes(Activator.CreateInstance(t));
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0006B224 File Offset: 0x00069424
		internal static int SizeInBytes(object instance)
		{
			Type type = instance.GetType();
			SerializationHelperSql9.GetFormat(type);
			DummyStream dummyStream = new DummyStream();
			Serializer serializer = SerializationHelperSql9.GetSerializer(instance.GetType());
			serializer.Serialize(dummyStream, instance);
			return (int)dummyStream.Length;
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0006B260 File Offset: 0x00069460
		internal static void Serialize(Stream s, object instance)
		{
			SerializationHelperSql9.GetSerializer(instance.GetType()).Serialize(s, instance);
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0006B274 File Offset: 0x00069474
		internal static object Deserialize(Stream s, Type resultType)
		{
			return SerializationHelperSql9.GetSerializer(resultType).Deserialize(s);
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x0006B282 File Offset: 0x00069482
		private static Format GetFormat(Type t)
		{
			return SerializationHelperSql9.GetUdtAttribute(t).Format;
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0006B290 File Offset: 0x00069490
		private static Serializer GetSerializer(Type t)
		{
			if (SerializationHelperSql9.s_types2Serializers == null)
			{
				SerializationHelperSql9.s_types2Serializers = new ConcurrentDictionary<Type, Serializer>();
			}
			Serializer newSerializer;
			if (!SerializationHelperSql9.s_types2Serializers.TryGetValue(t, out newSerializer))
			{
				newSerializer = SerializationHelperSql9.GetNewSerializer(t);
				SerializationHelperSql9.s_types2Serializers[t] = newSerializer;
			}
			return newSerializer;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0006B2D4 File Offset: 0x000694D4
		internal static int GetUdtMaxLength(Type t)
		{
			SqlUdtInfo fromType = SqlUdtInfo.GetFromType(t);
			if (Format.Native == fromType.SerializationFormat)
			{
				return SerializationHelperSql9.SizeInBytes(t);
			}
			return fromType.MaxByteSize;
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0006B2FE File Offset: 0x000694FE
		private static object[] GetCustomAttributes(Type t)
		{
			return t.GetCustomAttributes(typeof(SqlUserDefinedTypeAttribute), false);
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0006B314 File Offset: 0x00069514
		internal static SqlUserDefinedTypeAttribute GetUdtAttribute(Type t)
		{
			object[] customAttributes = SerializationHelperSql9.GetCustomAttributes(t);
			if (customAttributes != null && customAttributes.Length == 1)
			{
				return (SqlUserDefinedTypeAttribute)customAttributes[0];
			}
			throw ADP.CreateInvalidUdtException(t, "SqlUdtReason_NoUdtAttribute");
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0006B34C File Offset: 0x0006954C
		private static Serializer GetNewSerializer(Type t)
		{
			SqlUserDefinedTypeAttribute udtAttribute = SerializationHelperSql9.GetUdtAttribute(t);
			switch (udtAttribute.Format)
			{
			case Format.Native:
				return new NormalizedSerializer(t);
			case Format.UserDefined:
				return new BinarySerializeSerializer(t);
			}
			throw ADP.InvalidUserDefinedTypeSerializationFormat(udtAttribute.Format);
		}

		// Token: 0x040009CD RID: 2509
		private static ConcurrentDictionary<Type, Serializer> s_types2Serializers;
	}
}
