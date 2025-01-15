using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json
{
	// Token: 0x02000027 RID: 39
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	internal sealed class JsonPropertyAttribute : Attribute
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003366 File Offset: 0x00001566
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000336E File Offset: 0x0000156E
		public Type ItemConverterType { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003377 File Offset: 0x00001577
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000337F File Offset: 0x0000157F
		[Nullable(new byte[] { 2, 0 })]
		public object[] ItemConverterParameters
		{
			[return: Nullable(new byte[] { 2, 0 })]
			get;
			[param: Nullable(new byte[] { 2, 0 })]
			set;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003388 File Offset: 0x00001588
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00003390 File Offset: 0x00001590
		public Type NamingStrategyType { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003399 File Offset: 0x00001599
		// (set) Token: 0x060000BB RID: 187 RVA: 0x000033A1 File Offset: 0x000015A1
		[Nullable(new byte[] { 2, 0 })]
		public object[] NamingStrategyParameters
		{
			[return: Nullable(new byte[] { 2, 0 })]
			get;
			[param: Nullable(new byte[] { 2, 0 })]
			set;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000033AA File Offset: 0x000015AA
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000033B7 File Offset: 0x000015B7
		public NullValueHandling NullValueHandling
		{
			get
			{
				return this._nullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._nullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000033C5 File Offset: 0x000015C5
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000033D2 File Offset: 0x000015D2
		public DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._defaultValueHandling.GetValueOrDefault();
			}
			set
			{
				this._defaultValueHandling = new DefaultValueHandling?(value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000033E0 File Offset: 0x000015E0
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000033ED File Offset: 0x000015ED
		public ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._referenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._referenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000033FB File Offset: 0x000015FB
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003408 File Offset: 0x00001608
		public ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._objectCreationHandling.GetValueOrDefault();
			}
			set
			{
				this._objectCreationHandling = new ObjectCreationHandling?(value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003416 File Offset: 0x00001616
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003423 File Offset: 0x00001623
		public TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._typeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003431 File Offset: 0x00001631
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x0000343E File Offset: 0x0000163E
		public bool IsReference
		{
			get
			{
				return this._isReference.GetValueOrDefault();
			}
			set
			{
				this._isReference = new bool?(value);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000344C File Offset: 0x0000164C
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00003459 File Offset: 0x00001659
		public int Order
		{
			get
			{
				return this._order.GetValueOrDefault();
			}
			set
			{
				this._order = new int?(value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003467 File Offset: 0x00001667
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003474 File Offset: 0x00001674
		public Required Required
		{
			get
			{
				return this._required.GetValueOrDefault();
			}
			set
			{
				this._required = new Required?(value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003482 File Offset: 0x00001682
		// (set) Token: 0x060000CD RID: 205 RVA: 0x0000348A File Offset: 0x0000168A
		public string PropertyName { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003493 File Offset: 0x00001693
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000034A0 File Offset: 0x000016A0
		public ReferenceLoopHandling ItemReferenceLoopHandling
		{
			get
			{
				return this._itemReferenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._itemReferenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000034AE File Offset: 0x000016AE
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000034BB File Offset: 0x000016BB
		public TypeNameHandling ItemTypeNameHandling
		{
			get
			{
				return this._itemTypeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._itemTypeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000034C9 File Offset: 0x000016C9
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x000034D6 File Offset: 0x000016D6
		public bool ItemIsReference
		{
			get
			{
				return this._itemIsReference.GetValueOrDefault();
			}
			set
			{
				this._itemIsReference = new bool?(value);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000034E4 File Offset: 0x000016E4
		public JsonPropertyAttribute()
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000034EC File Offset: 0x000016EC
		[NullableContext(0)]
		public JsonPropertyAttribute(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x0400004F RID: 79
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x04000050 RID: 80
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x04000051 RID: 81
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x04000052 RID: 82
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x04000053 RID: 83
		internal TypeNameHandling? _typeNameHandling;

		// Token: 0x04000054 RID: 84
		internal bool? _isReference;

		// Token: 0x04000055 RID: 85
		internal int? _order;

		// Token: 0x04000056 RID: 86
		internal Required? _required;

		// Token: 0x04000057 RID: 87
		internal bool? _itemIsReference;

		// Token: 0x04000058 RID: 88
		internal ReferenceLoopHandling? _itemReferenceLoopHandling;

		// Token: 0x04000059 RID: 89
		internal TypeNameHandling? _itemTypeNameHandling;
	}
}
