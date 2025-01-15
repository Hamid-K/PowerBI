using System;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x0200009D RID: 157
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct JsonDerivedType
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x000280C6 File Offset: 0x000262C6
		public JsonDerivedType(Type derivedType)
		{
			this.DerivedType = derivedType;
			this.TypeDiscriminator = null;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000280D6 File Offset: 0x000262D6
		public JsonDerivedType(Type derivedType, int typeDiscriminator)
		{
			this.DerivedType = derivedType;
			this.TypeDiscriminator = typeDiscriminator;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x000280EB File Offset: 0x000262EB
		public JsonDerivedType(Type derivedType, string typeDiscriminator)
		{
			this.DerivedType = derivedType;
			this.TypeDiscriminator = typeDiscriminator;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000280FB File Offset: 0x000262FB
		internal JsonDerivedType(Type derivedType, object typeDiscriminator)
		{
			this.DerivedType = derivedType;
			this.TypeDiscriminator = typeDiscriminator;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0002810B File Offset: 0x0002630B
		public Type DerivedType { get; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00028113 File Offset: 0x00026313
		[Nullable(2)]
		public object TypeDiscriminator
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0002811B File Offset: 0x0002631B
		internal void Deconstruct(out Type derivedType, out object typeDiscriminator)
		{
			derivedType = this.DerivedType;
			typeDiscriminator = this.TypeDiscriminator;
		}
	}
}
