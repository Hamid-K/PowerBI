using System;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000073 RID: 115
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	public class JsonDerivedTypeAttribute : JsonAttribute
	{
		// Token: 0x0600080B RID: 2059 RVA: 0x000249FB File Offset: 0x00022BFB
		public JsonDerivedTypeAttribute(Type derivedType)
		{
			this.DerivedType = derivedType;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00024A0A File Offset: 0x00022C0A
		public JsonDerivedTypeAttribute(Type derivedType, string typeDiscriminator)
		{
			this.DerivedType = derivedType;
			this.TypeDiscriminator = typeDiscriminator;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00024A20 File Offset: 0x00022C20
		public JsonDerivedTypeAttribute(Type derivedType, int typeDiscriminator)
		{
			this.DerivedType = derivedType;
			this.TypeDiscriminator = typeDiscriminator;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00024A3B File Offset: 0x00022C3B
		public Type DerivedType { get; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00024A43 File Offset: 0x00022C43
		[Nullable(2)]
		public object TypeDiscriminator
		{
			[NullableContext(2)]
			get;
		}
	}
}
