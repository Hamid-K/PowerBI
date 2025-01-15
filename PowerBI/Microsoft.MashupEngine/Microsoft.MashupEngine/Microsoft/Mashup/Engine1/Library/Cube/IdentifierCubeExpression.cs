using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CE0 RID: 3296
	internal sealed class IdentifierCubeExpression : CubeExpression
	{
		// Token: 0x0600596B RID: 22891 RVA: 0x001394C0 File Offset: 0x001376C0
		public static IdentifierCubeExpression NewUnique()
		{
			return new IdentifierCubeExpression(Guid.NewGuid().ToString());
		}

		// Token: 0x0600596C RID: 22892 RVA: 0x001394E5 File Offset: 0x001376E5
		public IdentifierCubeExpression(string identifier)
		{
			this.identifier = identifier;
		}

		// Token: 0x17001ABC RID: 6844
		// (get) Token: 0x0600596D RID: 22893 RVA: 0x00002139 File Offset: 0x00000339
		public override CubeExpressionKind Kind
		{
			get
			{
				return CubeExpressionKind.Identifier;
			}
		}

		// Token: 0x17001ABD RID: 6845
		// (get) Token: 0x0600596E RID: 22894 RVA: 0x001394F4 File Offset: 0x001376F4
		public string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x0600596F RID: 22895 RVA: 0x001394FC File Offset: 0x001376FC
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IdentifierCubeExpression);
		}

		// Token: 0x06005970 RID: 22896 RVA: 0x0013950A File Offset: 0x0013770A
		public bool Equals(IdentifierCubeExpression other)
		{
			return other != null && other.identifier == this.identifier;
		}

		// Token: 0x06005971 RID: 22897 RVA: 0x00139522 File Offset: 0x00137722
		public override int GetHashCode()
		{
			return this.identifier.GetHashCode();
		}

		// Token: 0x04003217 RID: 12823
		private readonly string identifier;

		// Token: 0x02000CE1 RID: 3297
		public sealed class EqualityComparer : IEqualityComparer<IdentifierCubeExpression>
		{
			// Token: 0x06005972 RID: 22898 RVA: 0x0013952F File Offset: 0x0013772F
			public bool Equals(IdentifierCubeExpression id1, IdentifierCubeExpression id2)
			{
				return id1.Identifier == id2.Identifier;
			}

			// Token: 0x06005973 RID: 22899 RVA: 0x00139542 File Offset: 0x00137742
			public int GetHashCode(IdentifierCubeExpression id)
			{
				return id.Identifier.GetHashCode();
			}

			// Token: 0x04003218 RID: 12824
			public static readonly IEqualityComparer<IdentifierCubeExpression> Instance = new IdentifierCubeExpression.EqualityComparer();
		}
	}
}
