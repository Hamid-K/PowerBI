using System;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x02000085 RID: 133
	[NullableContext(2)]
	[Nullable(0)]
	internal class JsonContainerContract : JsonContract
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0001C326 File Offset: 0x0001A526
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x0001C32E File Offset: 0x0001A52E
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
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0001C368 File Offset: 0x0001A568
		internal JsonContract FinalItemContract
		{
			get
			{
				return this._finalItemContract;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0001C370 File Offset: 0x0001A570
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x0001C378 File Offset: 0x0001A578
		public JsonConverter ItemConverter { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001C381 File Offset: 0x0001A581
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0001C389 File Offset: 0x0001A589
		public bool? ItemIsReference { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0001C392 File Offset: 0x0001A592
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x0001C39A File Offset: 0x0001A59A
		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0001C3A3 File Offset: 0x0001A5A3
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x0001C3AB File Offset: 0x0001A5AB
		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001C3B4 File Offset: 0x0001A5B4
		[NullableContext(0)]
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

		// Token: 0x0400023F RID: 575
		private JsonContract _itemContract;

		// Token: 0x04000240 RID: 576
		private JsonContract _finalItemContract;
	}
}
