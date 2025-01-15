using System;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000625 RID: 1573
	internal sealed class Vertex : IEquatable<Vertex>
	{
		// Token: 0x06004BFD RID: 19453 RVA: 0x0010B94D File Offset: 0x00109B4D
		private Vertex()
		{
			this.Variable = int.MaxValue;
			this.Children = new Vertex[0];
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x0010B96C File Offset: 0x00109B6C
		internal Vertex(int variable, Vertex[] children)
		{
			if (variable >= 2147483647)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.BoolExprAssert, 0, "exceeded number of supported variables");
			}
			this.Variable = variable;
			this.Children = children;
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x0010B99C File Offset: 0x00109B9C
		[Conditional("DEBUG")]
		private static void AssertConstructorArgumentsValid(int variable, Vertex[] children)
		{
			foreach (Vertex vertex in children)
			{
			}
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x0010B9BD File Offset: 0x00109BBD
		internal bool IsOne()
		{
			return Vertex.One == this;
		}

		// Token: 0x06004C01 RID: 19457 RVA: 0x0010B9C7 File Offset: 0x00109BC7
		internal bool IsZero()
		{
			return Vertex.Zero == this;
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x0010B9D1 File Offset: 0x00109BD1
		internal bool IsSink()
		{
			return this.Variable == int.MaxValue;
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x0010B9E0 File Offset: 0x00109BE0
		public bool Equals(Vertex other)
		{
			return this == other;
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x0010B9E6 File Offset: 0x00109BE6
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x0010B9EF File Offset: 0x00109BEF
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x0010B9F8 File Offset: 0x00109BF8
		public override string ToString()
		{
			if (this.IsOne())
			{
				return "_1_";
			}
			if (this.IsZero())
			{
				return "_0_";
			}
			return string.Format(CultureInfo.InvariantCulture, "<{0}, {1}>", new object[]
			{
				this.Variable,
				StringUtil.ToCommaSeparatedString(this.Children)
			});
		}

		// Token: 0x04001A86 RID: 6790
		internal static readonly Vertex One = new Vertex();

		// Token: 0x04001A87 RID: 6791
		internal static readonly Vertex Zero = new Vertex();

		// Token: 0x04001A88 RID: 6792
		internal readonly int Variable;

		// Token: 0x04001A89 RID: 6793
		internal readonly Vertex[] Children;
	}
}
