using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BC5 RID: 7109
	public sealed class FieldTypeSyntaxNode : IFieldType
	{
		// Token: 0x0600B17B RID: 45435 RVA: 0x00243BE7 File Offset: 0x00241DE7
		public FieldTypeSyntaxNode(Identifier identifier, IExpression type, bool optional)
		{
			this.name = identifier;
			this.type = type;
			this.optional = optional;
		}

		// Token: 0x17002C88 RID: 11400
		// (get) Token: 0x0600B17C RID: 45436 RVA: 0x00243C04 File Offset: 0x00241E04
		public Identifier Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17002C89 RID: 11401
		// (get) Token: 0x0600B17D RID: 45437 RVA: 0x00243C0C File Offset: 0x00241E0C
		public IExpression Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17002C8A RID: 11402
		// (get) Token: 0x0600B17E RID: 45438 RVA: 0x00243C14 File Offset: 0x00241E14
		public bool Optional
		{
			get
			{
				return this.optional;
			}
		}

		// Token: 0x04005AFA RID: 23290
		private Identifier name;

		// Token: 0x04005AFB RID: 23291
		private IExpression type;

		// Token: 0x04005AFC RID: 23292
		private bool optional;
	}
}
