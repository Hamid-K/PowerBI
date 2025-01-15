using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000522 RID: 1314
	public static class EntitySerializerExtensionsMethods
	{
		// Token: 0x06002891 RID: 10385 RVA: 0x000922C0 File Offset: 0x000904C0
		public static void SerializeToStream(this IEntitySerializer entitySerializer, Stream target, object o, Type type, SerializationOptions options)
		{
			entitySerializer.SerializeToStream(target, o, type, options, Enumerable.Empty<Type>());
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000922D2 File Offset: 0x000904D2
		public static void SerializeToStream(this IEntitySerializer entitySerializer, Stream target, object o, SerializationOptions options)
		{
			entitySerializer.SerializeToStream(target, o, o.GetType(), options, Enumerable.Empty<Type>());
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000922E8 File Offset: 0x000904E8
		public static object Deserialize(this IEntitySerializer entitySerializer, Stream stream, Type type, SerializationOptions options)
		{
			return entitySerializer.Deserialize(stream, type, options, Enumerable.Empty<Type>());
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000922F8 File Offset: 0x000904F8
		public static T Deserialize<T>(this IEntitySerializer entitySerializer, Stream stream, SerializationOptions options)
		{
			return (T)((object)entitySerializer.Deserialize(stream, typeof(T), options, Enumerable.Empty<Type>()));
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x00092318 File Offset: 0x00090518
		public static byte[] Serialize(this IEntitySerializer entitySerializer, object o, Type type, SerializationOptions options, IEnumerable<Type> knownTypes)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				entitySerializer.SerializeToStream(memoryStream, o, type, options, knownTypes);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x0009235C File Offset: 0x0009055C
		public static byte[] Serialize(this IEntitySerializer entitySerializer, object o, SerializationOptions options)
		{
			return entitySerializer.Serialize(o, o.GetType(), options, Enumerable.Empty<Type>());
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x00092374 File Offset: 0x00090574
		public static object Deserialize(this IEntitySerializer entitySerializer, byte[] bytes, Type type, SerializationOptions options, IEnumerable<Type> knownTypes)
		{
			object obj;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				obj = entitySerializer.Deserialize(memoryStream, type, options, knownTypes);
			}
			return obj;
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x000923B4 File Offset: 0x000905B4
		public static T Deserialize<T>(this IEntitySerializer entitySerializer, byte[] bytes, SerializationOptions options, IEnumerable<Type> knownTypes)
		{
			return (T)((object)entitySerializer.Deserialize(bytes, typeof(T), options, knownTypes));
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000923CE File Offset: 0x000905CE
		public static T Deserialize<T>(this IEntitySerializer entitySerializer, byte[] bytes, SerializationOptions options)
		{
			return entitySerializer.Deserialize(bytes, options, Enumerable.Empty<Type>());
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x000923DD File Offset: 0x000905DD
		public static string SerializeToString(this IEntitySerializer entitySerializer, object o, SerializationOptions options)
		{
			return entitySerializer.SerializeToString(o, options, Enumerable.Empty<Type>());
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000923EC File Offset: 0x000905EC
		public static string SerializeToString(this IEntitySerializer entitySerializer, object o, SerializationOptions options, IEnumerable<Type> knownTypes)
		{
			return Convert.ToBase64String(entitySerializer.Serialize(o, o.GetType(), options, knownTypes));
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x00092402 File Offset: 0x00090602
		public static T Deserialize<T>(this IEntitySerializer entitySerializer, string s, SerializationOptions options)
		{
			return entitySerializer.Deserialize(s, options, Enumerable.Empty<Type>());
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x00092411 File Offset: 0x00090611
		public static T Deserialize<T>(this IEntitySerializer entitySerializer, string s, SerializationOptions options, IEnumerable<Type> knownTypes)
		{
			return entitySerializer.Deserialize(Convert.FromBase64String(s), options, knownTypes);
		}
	}
}
