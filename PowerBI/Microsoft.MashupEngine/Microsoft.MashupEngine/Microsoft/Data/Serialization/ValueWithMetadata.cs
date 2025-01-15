using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.Data.Serialization
{
	// Token: 0x0200015F RID: 351
	public class ValueWithMetadata
	{
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0000ADDF File Offset: 0x00008FDF
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0000ADE7 File Offset: 0x00008FE7
		public object Value { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0000ADF0 File Offset: 0x00008FF0
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		public IDataRecord Metadata { get; set; }

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000AE04 File Offset: 0x00009004
		public static bool HasMetadata(Type type, out Type underlyingType)
		{
			Dictionary<Type, Type> dictionary = ValueWithMetadata.fromMetadataMap;
			bool flag2;
			lock (dictionary)
			{
				underlyingType = null;
				if (type.IsGenericType && !ValueWithMetadata.fromMetadataMap.TryGetValue(type, out underlyingType))
				{
					underlyingType = (typeof(ValueWithMetadata).IsAssignableFrom(type) ? type.GetGenericArguments()[0] : null);
					ValueWithMetadata.fromMetadataMap[type] = underlyingType;
				}
				if (underlyingType != null)
				{
					flag2 = true;
				}
				else
				{
					underlyingType = type;
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0000AE98 File Offset: 0x00009098
		public static Type AddMetadata(Type type, bool hasMetadata)
		{
			if (!hasMetadata)
			{
				return type;
			}
			return ValueWithMetadata.AddMetadata(type);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0000AEA8 File Offset: 0x000090A8
		private static Type AddMetadata(Type type)
		{
			Dictionary<Type, Type> dictionary = ValueWithMetadata.toMetadataMap;
			Type type3;
			lock (dictionary)
			{
				Type type2;
				if (!ValueWithMetadata.toMetadataMap.TryGetValue(type, out type2))
				{
					type2 = typeof(ValueWithMetadata<>).MakeGenericType(new Type[] { type });
					ValueWithMetadata.toMetadataMap[type] = type2;
				}
				type3 = type2;
			}
			return type3;
		}

		// Token: 0x040003F9 RID: 1017
		private static readonly Dictionary<Type, Type> toMetadataMap = new Dictionary<Type, Type>();

		// Token: 0x040003FA RID: 1018
		private static readonly Dictionary<Type, Type> fromMetadataMap = new Dictionary<Type, Type>();
	}
}
