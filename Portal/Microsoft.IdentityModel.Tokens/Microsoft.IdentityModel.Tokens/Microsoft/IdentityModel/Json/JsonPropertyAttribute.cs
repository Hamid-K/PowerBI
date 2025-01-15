using System;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json
{
	// Token: 0x02000027 RID: 39
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	internal sealed class JsonPropertyAttribute : Attribute
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000337A File Offset: 0x0000157A
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003382 File Offset: 0x00001582
		public Type ItemConverterType { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000338B File Offset: 0x0000158B
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003393 File Offset: 0x00001593
		[Nullable(new byte[] { 2, 1 })]
		public object[] ItemConverterParameters
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000339C File Offset: 0x0000159C
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000033A4 File Offset: 0x000015A4
		public Type NamingStrategyType { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000033AD File Offset: 0x000015AD
		// (set) Token: 0x060000BB RID: 187 RVA: 0x000033B5 File Offset: 0x000015B5
		[Nullable(new byte[] { 2, 1 })]
		public object[] NamingStrategyParameters
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000033BE File Offset: 0x000015BE
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000033CB File Offset: 0x000015CB
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
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000033D9 File Offset: 0x000015D9
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000033E6 File Offset: 0x000015E6
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
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000033F4 File Offset: 0x000015F4
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003401 File Offset: 0x00001601
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
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000340F File Offset: 0x0000160F
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x0000341C File Offset: 0x0000161C
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
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000342A File Offset: 0x0000162A
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003437 File Offset: 0x00001637
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
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003445 File Offset: 0x00001645
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003452 File Offset: 0x00001652
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
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003460 File Offset: 0x00001660
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x0000346D File Offset: 0x0000166D
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
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000347B File Offset: 0x0000167B
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003488 File Offset: 0x00001688
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
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003496 File Offset: 0x00001696
		// (set) Token: 0x060000CD RID: 205 RVA: 0x0000349E File Offset: 0x0000169E
		public string PropertyName { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000034A7 File Offset: 0x000016A7
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000034B4 File Offset: 0x000016B4
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
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000034C2 File Offset: 0x000016C2
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000034CF File Offset: 0x000016CF
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
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000034DD File Offset: 0x000016DD
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x000034EA File Offset: 0x000016EA
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

		// Token: 0x060000D4 RID: 212 RVA: 0x000034F8 File Offset: 0x000016F8
		public JsonPropertyAttribute()
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003500 File Offset: 0x00001700
		[NullableContext(1)]
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
