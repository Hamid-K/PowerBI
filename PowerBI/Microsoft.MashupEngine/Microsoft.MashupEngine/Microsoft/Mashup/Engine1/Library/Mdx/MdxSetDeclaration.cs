using System;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009A4 RID: 2468
	internal sealed class MdxSetDeclaration : MdxDeclaration
	{
		// Token: 0x0600468E RID: 18062 RVA: 0x000ECB54 File Offset: 0x000EAD54
		public MdxSetDeclaration(IdentifierMdxExpression identifier, MdxExpression definition, bool isSingledQuoted = false)
			: base(identifier, definition, isSingledQuoted)
		{
		}

		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x0600468F RID: 18063 RVA: 0x000ECB66 File Offset: 0x000EAD66
		protected override string DeclarationKind
		{
			get
			{
				return "SET";
			}
		}
	}
}
