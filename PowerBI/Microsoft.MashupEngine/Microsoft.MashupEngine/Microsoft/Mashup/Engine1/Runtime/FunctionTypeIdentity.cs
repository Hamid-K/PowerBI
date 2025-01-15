using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200131E RID: 4894
	internal class FunctionTypeIdentity : IFunctionIdentity, IEquatable<IFunctionIdentity>
	{
		// Token: 0x06008131 RID: 33073 RVA: 0x001B81EF File Offset: 0x001B63EF
		public FunctionTypeIdentity(Type type)
		{
			this.type = type;
		}

		// Token: 0x06008132 RID: 33074 RVA: 0x001B81FE File Offset: 0x001B63FE
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FunctionTypeIdentity);
		}

		// Token: 0x06008133 RID: 33075 RVA: 0x001B81FE File Offset: 0x001B63FE
		public bool Equals(IFunctionIdentity identity)
		{
			return this.Equals(identity as FunctionTypeIdentity);
		}

		// Token: 0x06008134 RID: 33076 RVA: 0x001B820C File Offset: 0x001B640C
		public bool Equals(FunctionTypeIdentity identity)
		{
			return identity != null && identity.type == this.type;
		}

		// Token: 0x06008135 RID: 33077 RVA: 0x001B8224 File Offset: 0x001B6424
		public override int GetHashCode()
		{
			return this.type.GetHashCode();
		}

		// Token: 0x0400467F RID: 18047
		private readonly Type type;
	}
}
