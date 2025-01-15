using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BBD RID: 7101
	public class RecordExpressionSyntaxNode : RangeSyntaxNode, IRecordExpression, IExpression, ISyntaxNode, IDeclarator
	{
		// Token: 0x0600B15C RID: 45404 RVA: 0x00243ABA File Offset: 0x00241CBA
		public RecordExpressionSyntaxNode(params VariableInitializer[] initializers)
			: this(null, initializers, TokenRange.Null)
		{
		}

		// Token: 0x0600B15D RID: 45405 RVA: 0x00243ABA File Offset: 0x00241CBA
		public RecordExpressionSyntaxNode(IList<VariableInitializer> initializers)
			: this(null, initializers, TokenRange.Null)
		{
		}

		// Token: 0x0600B15E RID: 45406 RVA: 0x00243AC9 File Offset: 0x00241CC9
		public RecordExpressionSyntaxNode(IList<VariableInitializer> initializers, TokenRange range)
			: this(null, initializers, range)
		{
		}

		// Token: 0x0600B15F RID: 45407 RVA: 0x00243AD4 File Offset: 0x00241CD4
		public RecordExpressionSyntaxNode(Identifier identifier, IList<VariableInitializer> initializers, TokenRange range)
			: base(range)
		{
			this.identifier = identifier;
			this.initializers = initializers;
		}

		// Token: 0x17002C76 RID: 11382
		// (get) Token: 0x0600B160 RID: 45408 RVA: 0x001AA8D9 File Offset: 0x001A8AD9
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Record;
			}
		}

		// Token: 0x17002C77 RID: 11383
		// (get) Token: 0x0600B161 RID: 45409 RVA: 0x00243AEB File Offset: 0x00241CEB
		public Identifier Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17002C78 RID: 11384
		// (get) Token: 0x0600B162 RID: 45410 RVA: 0x00243AF3 File Offset: 0x00241CF3
		public IList<VariableInitializer> Members
		{
			get
			{
				return this.initializers;
			}
		}

		// Token: 0x17002C79 RID: 11385
		// (get) Token: 0x0600B163 RID: 45411 RVA: 0x00243AFB File Offset: 0x00241CFB
		int IDeclarator.Count
		{
			get
			{
				return this.initializers.Count;
			}
		}

		// Token: 0x17002C7A RID: 11386
		Identifier IDeclarator.this[int index]
		{
			get
			{
				return this.initializers[index].Name;
			}
		}

		// Token: 0x04005AF1 RID: 23281
		private Identifier identifier;

		// Token: 0x04005AF2 RID: 23282
		private IList<VariableInitializer> initializers;
	}
}
