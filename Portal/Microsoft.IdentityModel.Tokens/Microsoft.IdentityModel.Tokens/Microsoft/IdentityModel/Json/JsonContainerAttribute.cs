using System;
using System.Runtime.CompilerServices;
using Microsoft.IdentityModel.Json.Serialization;

namespace Microsoft.IdentityModel.Json
{
	// Token: 0x02000019 RID: 25
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	internal abstract class JsonContainerAttribute : Attribute
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000023BB File Offset: 0x000005BB
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000023C3 File Offset: 0x000005C3
		public string Id { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000023CC File Offset: 0x000005CC
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000023D4 File Offset: 0x000005D4
		public string Title { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000023DD File Offset: 0x000005DD
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000023E5 File Offset: 0x000005E5
		public string Description { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023EE File Offset: 0x000005EE
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000023F6 File Offset: 0x000005F6
		public Type ItemConverterType { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023FF File Offset: 0x000005FF
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002407 File Offset: 0x00000607
		[Nullable(new byte[] { 2, 1 })]
		public object[] ItemConverterParameters
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002410 File Offset: 0x00000610
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002418 File Offset: 0x00000618
		public Type NamingStrategyType
		{
			get
			{
				return this._namingStrategyType;
			}
			set
			{
				this._namingStrategyType = value;
				this.NamingStrategyInstance = null;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002428 File Offset: 0x00000628
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002430 File Offset: 0x00000630
		[Nullable(new byte[] { 2, 1 })]
		public object[] NamingStrategyParameters
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._namingStrategyParameters;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				this._namingStrategyParameters = value;
				this.NamingStrategyInstance = null;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002440 File Offset: 0x00000640
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002448 File Offset: 0x00000648
		internal NamingStrategy NamingStrategyInstance { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002451 File Offset: 0x00000651
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000245E File Offset: 0x0000065E
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000246C File Offset: 0x0000066C
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002479 File Offset: 0x00000679
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002487 File Offset: 0x00000687
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002494 File Offset: 0x00000694
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000024A2 File Offset: 0x000006A2
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000024AF File Offset: 0x000006AF
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

		// Token: 0x06000038 RID: 56 RVA: 0x000024BD File Offset: 0x000006BD
		protected JsonContainerAttribute()
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000024C5 File Offset: 0x000006C5
		[NullableContext(1)]
		protected JsonContainerAttribute(string id)
		{
			this.Id = id;
		}

		// Token: 0x0400002F RID: 47
		internal bool? _isReference;

		// Token: 0x04000030 RID: 48
		internal bool? _itemIsReference;

		// Token: 0x04000031 RID: 49
		internal ReferenceLoopHandling? _itemReferenceLoopHandling;

		// Token: 0x04000032 RID: 50
		internal TypeNameHandling? _itemTypeNameHandling;

		// Token: 0x04000033 RID: 51
		private Type _namingStrategyType;

		// Token: 0x04000034 RID: 52
		[Nullable(new byte[] { 2, 1 })]
		private object[] _namingStrategyParameters;
	}
}
