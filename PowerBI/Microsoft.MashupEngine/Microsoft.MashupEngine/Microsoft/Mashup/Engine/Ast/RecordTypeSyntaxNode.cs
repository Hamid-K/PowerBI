using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BC6 RID: 7110
	public sealed class RecordTypeSyntaxNode : RangeSyntaxNode, IRecordTypeExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B17F RID: 45439 RVA: 0x00243C1C File Offset: 0x00241E1C
		public RecordTypeSyntaxNode(IList<IFieldType> fields, bool wildcard, TokenRange range)
			: base(range)
		{
			this.fields = fields;
			this.wildcard = wildcard;
		}

		// Token: 0x17002C8B RID: 11403
		// (get) Token: 0x0600B180 RID: 45440 RVA: 0x00243C33 File Offset: 0x00241E33
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.RecordType;
			}
		}

		// Token: 0x17002C8C RID: 11404
		// (get) Token: 0x0600B181 RID: 45441 RVA: 0x00243C37 File Offset: 0x00241E37
		public IList<IFieldType> Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x17002C8D RID: 11405
		// (get) Token: 0x0600B182 RID: 45442 RVA: 0x00243C3F File Offset: 0x00241E3F
		public bool Wildcard
		{
			get
			{
				return this.wildcard;
			}
		}

		// Token: 0x04005AFD RID: 23293
		private IList<IFieldType> fields;

		// Token: 0x04005AFE RID: 23294
		private bool wildcard;
	}
}
