﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000092 RID: 146
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonPrimitiveContract : JsonContract
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001D710 File Offset: 0x0001B910
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0001D718 File Offset: 0x0001B918
		internal PrimitiveTypeCode TypeCode { get; set; }

		// Token: 0x0600071F RID: 1823 RVA: 0x0001D724 File Offset: 0x0001B924
		public JsonPrimitiveContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Primitive;
			this.TypeCode = ConvertUtils.GetTypeCode(underlyingType);
			this.IsReadOnlyOrFixedSize = true;
			ReadType readType;
			if (JsonPrimitiveContract.ReadTypeMap.TryGetValue(this.NonNullableUnderlyingType, out readType))
			{
				this.InternalReadType = readType;
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001D770 File Offset: 0x0001B970
		// Note: this type is marked as 'beforefieldinit'.
		static JsonPrimitiveContract()
		{
			Dictionary<Type, ReadType> dictionary = new Dictionary<Type, ReadType>();
			Type typeFromHandle = typeof(byte[]);
			dictionary[typeFromHandle] = ReadType.ReadAsBytes;
			Type typeFromHandle2 = typeof(byte);
			dictionary[typeFromHandle2] = ReadType.ReadAsInt32;
			Type typeFromHandle3 = typeof(short);
			dictionary[typeFromHandle3] = ReadType.ReadAsInt32;
			Type typeFromHandle4 = typeof(int);
			dictionary[typeFromHandle4] = ReadType.ReadAsInt32;
			Type typeFromHandle5 = typeof(decimal);
			dictionary[typeFromHandle5] = ReadType.ReadAsDecimal;
			Type typeFromHandle6 = typeof(bool);
			dictionary[typeFromHandle6] = ReadType.ReadAsBoolean;
			Type typeFromHandle7 = typeof(string);
			dictionary[typeFromHandle7] = ReadType.ReadAsString;
			Type typeFromHandle8 = typeof(DateTime);
			dictionary[typeFromHandle8] = ReadType.ReadAsDateTime;
			Type typeFromHandle9 = typeof(DateTimeOffset);
			dictionary[typeFromHandle9] = ReadType.ReadAsDateTimeOffset;
			Type typeFromHandle10 = typeof(float);
			dictionary[typeFromHandle10] = ReadType.ReadAsDouble;
			Type typeFromHandle11 = typeof(double);
			dictionary[typeFromHandle11] = ReadType.ReadAsDouble;
			Type typeFromHandle12 = typeof(long);
			dictionary[typeFromHandle12] = ReadType.ReadAsInt64;
			JsonPrimitiveContract.ReadTypeMap = dictionary;
		}

		// Token: 0x040002A2 RID: 674
		private static readonly Dictionary<Type, ReadType> ReadTypeMap;
	}
}
