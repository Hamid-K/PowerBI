using System;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009A3 RID: 2467
	internal sealed class MdxMemberDeclaration : MdxDeclaration
	{
		// Token: 0x0600468C RID: 18060 RVA: 0x000ECB54 File Offset: 0x000EAD54
		public MdxMemberDeclaration(IdentifierMdxExpression identifier, MdxExpression definition, bool isSingledQuoted = false)
			: base(identifier, definition, isSingledQuoted)
		{
		}

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x0600468D RID: 18061 RVA: 0x000ECB5F File Offset: 0x000EAD5F
		protected override string DeclarationKind
		{
			get
			{
				return "MEMBER";
			}
		}
	}
}
