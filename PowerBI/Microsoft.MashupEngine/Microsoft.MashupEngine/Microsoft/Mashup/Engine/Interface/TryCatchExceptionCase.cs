using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000D6 RID: 214
	public struct TryCatchExceptionCase : IDeclarator
	{
		// Token: 0x06000332 RID: 818 RVA: 0x00004943 File Offset: 0x00002B43
		public TryCatchExceptionCase(Identifier variable, IExpression expression)
		{
			this.variable = variable;
			this.expression = expression;
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00004953 File Offset: 0x00002B53
		public Identifier Variable
		{
			get
			{
				return this.variable;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000495B File Offset: 0x00002B5B
		public IExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00002139 File Offset: 0x00000339
		int IDeclarator.Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000128 RID: 296
		Identifier IDeclarator.this[int index]
		{
			get
			{
				return this.variable;
			}
		}

		// Token: 0x040001F9 RID: 505
		private Identifier variable;

		// Token: 0x040001FA RID: 506
		private IExpression expression;
	}
}
