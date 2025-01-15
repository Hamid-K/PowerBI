using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000085 RID: 133
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonContainerContract : JsonContract
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0001C8CE File Offset: 0x0001AACE
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x0001C8D6 File Offset: 0x0001AAD6
		internal JsonContract ItemContract
		{
			get
			{
				return this._itemContract;
			}
			set
			{
				this._itemContract = value;
				if (this._itemContract != null)
				{
					this._finalItemContract = (this._itemContract.UnderlyingType.IsSealed() ? this._itemContract : null);
					return;
				}
				this._finalItemContract = null;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001C910 File Offset: 0x0001AB10
		internal JsonContract FinalItemContract
		{
			get
			{
				return this._finalItemContract;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001C918 File Offset: 0x0001AB18
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x0001C920 File Offset: 0x0001AB20
		public JsonConverter ItemConverter { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0001C929 File Offset: 0x0001AB29
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x0001C931 File Offset: 0x0001AB31
		public bool? ItemIsReference { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0001C93A File Offset: 0x0001AB3A
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x0001C942 File Offset: 0x0001AB42
		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0001C94B File Offset: 0x0001AB4B
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x0001C953 File Offset: 0x0001AB53
		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		// Token: 0x060006AA RID: 1706 RVA: 0x0001C95C File Offset: 0x0001AB5C
		[NullableContext(1)]
		internal JsonContainerContract(Type underlyingType)
			: base(underlyingType)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(underlyingType);
			if (cachedAttribute != null)
			{
				if (cachedAttribute.ItemConverterType != null)
				{
					this.ItemConverter = JsonTypeReflector.CreateJsonConverterInstance(cachedAttribute.ItemConverterType, cachedAttribute.ItemConverterParameters);
				}
				this.ItemIsReference = cachedAttribute._itemIsReference;
				this.ItemReferenceLoopHandling = cachedAttribute._itemReferenceLoopHandling;
				this.ItemTypeNameHandling = cachedAttribute._itemTypeNameHandling;
			}
		}

		// Token: 0x04000259 RID: 601
		private JsonContract _itemContract;

		// Token: 0x0400025A RID: 602
		private JsonContract _finalItemContract;
	}
}
