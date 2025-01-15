using System;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x0200099B RID: 2459
	internal sealed class IdentifierMdxExpression : MdxExpression
	{
		// Token: 0x06004668 RID: 18024 RVA: 0x000EC63C File Offset: 0x000EA83C
		public IdentifierMdxExpression(string identifier)
		{
			this.identifier = identifier;
		}

		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x06004669 RID: 18025 RVA: 0x000EC64B File Offset: 0x000EA84B
		public string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x000EC653 File Offset: 0x000EA853
		public bool Equals(IdentifierMdxExpression other)
		{
			return other != null && this.identifier == other.identifier;
		}

		// Token: 0x0600466B RID: 18027 RVA: 0x000EC66B File Offset: 0x000EA86B
		public override bool Equals(object other)
		{
			return this.Equals(other as IdentifierMdxExpression);
		}

		// Token: 0x0600466C RID: 18028 RVA: 0x000EC679 File Offset: 0x000EA879
		public override int GetHashCode()
		{
			return this.identifier.GetHashCode();
		}

		// Token: 0x0600466D RID: 18029 RVA: 0x000EC686 File Offset: 0x000EA886
		public override void Write(MdxExpressionWriter writer)
		{
			writer.Write(this.Identifier);
		}

		// Token: 0x0400253A RID: 9530
		private readonly string identifier;
	}
}
