using System;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009A2 RID: 2466
	internal abstract class MdxDeclaration
	{
		// Token: 0x06004687 RID: 18055 RVA: 0x000ECABC File Offset: 0x000EACBC
		public MdxDeclaration(IdentifierMdxExpression identifier, MdxExpression definition, bool isSingledQuoted = false)
		{
			this.identifier = identifier;
			this.definition = definition;
			this.isSingledQuoted = isSingledQuoted;
		}

		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x06004688 RID: 18056 RVA: 0x000ECAD9 File Offset: 0x000EACD9
		public IdentifierMdxExpression Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x06004689 RID: 18057 RVA: 0x000ECAE1 File Offset: 0x000EACE1
		public MdxExpression Definition
		{
			get
			{
				return this.definition;
			}
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x000ECAEC File Offset: 0x000EACEC
		public void Write(MdxExpressionWriter writer)
		{
			writer.Write(this.DeclarationKind);
			this.Identifier.Write(writer);
			writer.Write("AS");
			if (this.isSingledQuoted)
			{
				writer.Write(" '");
			}
			this.Definition.Write(writer);
			if (this.isSingledQuoted)
			{
				writer.Write("'");
			}
			writer.WriteLine();
		}

		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x0600468B RID: 18059
		protected abstract string DeclarationKind { get; }

		// Token: 0x0400254B RID: 9547
		private readonly IdentifierMdxExpression identifier;

		// Token: 0x0400254C RID: 9548
		private readonly MdxExpression definition;

		// Token: 0x0400254D RID: 9549
		private readonly bool isSingledQuoted;
	}
}
