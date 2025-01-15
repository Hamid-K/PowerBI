using System;
using System.Runtime.CompilerServices;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x02000086 RID: 134
	[NullableContext(2)]
	[Nullable(0)]
	internal class JsonContainerContract : JsonContract
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0001C8FA File Offset: 0x0001AAFA
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0001C902 File Offset: 0x0001AB02
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
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001C93C File Offset: 0x0001AB3C
		internal JsonContract FinalItemContract
		{
			get
			{
				return this._finalItemContract;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001C944 File Offset: 0x0001AB44
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0001C94C File Offset: 0x0001AB4C
		public JsonConverter ItemConverter { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001C955 File Offset: 0x0001AB55
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0001C95D File Offset: 0x0001AB5D
		public bool? ItemIsReference { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001C966 File Offset: 0x0001AB66
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x0001C96E File Offset: 0x0001AB6E
		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001C977 File Offset: 0x0001AB77
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x0001C97F File Offset: 0x0001AB7F
		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		// Token: 0x060006AB RID: 1707 RVA: 0x0001C988 File Offset: 0x0001AB88
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

		// Token: 0x0400025A RID: 602
		private JsonContract _itemContract;

		// Token: 0x0400025B RID: 603
		private JsonContract _finalItemContract;
	}
}
