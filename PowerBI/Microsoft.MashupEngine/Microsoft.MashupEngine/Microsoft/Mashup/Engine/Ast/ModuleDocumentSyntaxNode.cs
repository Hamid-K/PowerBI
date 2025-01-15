using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BCB RID: 7115
	public sealed class ModuleDocumentSyntaxNode : RangeSyntaxNode, ISectionDocument, IDocument, ISyntaxNode
	{
		// Token: 0x0600B192 RID: 45458 RVA: 0x00243CDA File Offset: 0x00241EDA
		public ModuleDocumentSyntaxNode(IDocumentHost host, ITokens tokens, ISection module, TokenRange range)
			: base(range)
		{
			this.host = host;
			this.tokens = tokens;
			this.module = module;
		}

		// Token: 0x17002C98 RID: 11416
		// (get) Token: 0x0600B193 RID: 45459 RVA: 0x00002105 File Offset: 0x00000305
		public DocumentKind Kind
		{
			get
			{
				return DocumentKind.Section;
			}
		}

		// Token: 0x17002C99 RID: 11417
		// (get) Token: 0x0600B194 RID: 45460 RVA: 0x00243CF9 File Offset: 0x00241EF9
		public IDocumentHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17002C9A RID: 11418
		// (get) Token: 0x0600B195 RID: 45461 RVA: 0x00243D01 File Offset: 0x00241F01
		public ISection Section
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x17002C9B RID: 11419
		// (get) Token: 0x0600B196 RID: 45462 RVA: 0x00243D09 File Offset: 0x00241F09
		public ITokens Tokens
		{
			get
			{
				return this.tokens;
			}
		}

		// Token: 0x04005B05 RID: 23301
		private IDocumentHost host;

		// Token: 0x04005B06 RID: 23302
		private ITokens tokens;

		// Token: 0x04005B07 RID: 23303
		private ISection module;
	}
}
