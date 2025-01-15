using System;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Json.Serialization;

namespace Microsoft.Identity.Json
{
	// Token: 0x02000019 RID: 25
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	internal abstract class JsonContainerAttribute : Attribute
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000023A7 File Offset: 0x000005A7
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000023AF File Offset: 0x000005AF
		public string Id { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000023B8 File Offset: 0x000005B8
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000023C0 File Offset: 0x000005C0
		public string Title { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000023C9 File Offset: 0x000005C9
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000023D1 File Offset: 0x000005D1
		public string Description { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023DA File Offset: 0x000005DA
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000023E2 File Offset: 0x000005E2
		public Type ItemConverterType { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023EB File Offset: 0x000005EB
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000023F3 File Offset: 0x000005F3
		[Nullable(new byte[] { 2, 0 })]
		public object[] ItemConverterParameters
		{
			[return: Nullable(new byte[] { 2, 0 })]
			get;
			[param: Nullable(new byte[] { 2, 0 })]
			set;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000023FC File Offset: 0x000005FC
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002404 File Offset: 0x00000604
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
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002414 File Offset: 0x00000614
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000241C File Offset: 0x0000061C
		[Nullable(new byte[] { 2, 0 })]
		public object[] NamingStrategyParameters
		{
			[return: Nullable(new byte[] { 2, 0 })]
			get
			{
				return this._namingStrategyParameters;
			}
			[param: Nullable(new byte[] { 2, 0 })]
			set
			{
				this._namingStrategyParameters = value;
				this.NamingStrategyInstance = null;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000242C File Offset: 0x0000062C
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002434 File Offset: 0x00000634
		internal NamingStrategy NamingStrategyInstance { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000243D File Offset: 0x0000063D
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000244A File Offset: 0x0000064A
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
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002458 File Offset: 0x00000658
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002465 File Offset: 0x00000665
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
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002473 File Offset: 0x00000673
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002480 File Offset: 0x00000680
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
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000248E File Offset: 0x0000068E
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000249B File Offset: 0x0000069B
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

		// Token: 0x06000038 RID: 56 RVA: 0x000024A9 File Offset: 0x000006A9
		protected JsonContainerAttribute()
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000024B1 File Offset: 0x000006B1
		[NullableContext(0)]
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
		[Nullable(new byte[] { 2, 0 })]
		private object[] _namingStrategyParameters;
	}
}
