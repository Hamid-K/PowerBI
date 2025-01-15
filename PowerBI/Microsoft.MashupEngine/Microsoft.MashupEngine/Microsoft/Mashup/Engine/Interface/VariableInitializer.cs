using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000CF RID: 207
	public struct VariableInitializer
	{
		// Token: 0x06000327 RID: 807 RVA: 0x00004923 File Offset: 0x00002B23
		public VariableInitializer(Identifier name, IExpression expression)
		{
			this.name = name;
			this.expression = expression;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00004933 File Offset: 0x00002B33
		public Identifier Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000493B File Offset: 0x00002B3B
		public IExpression Value
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x040001F7 RID: 503
		private Identifier name;

		// Token: 0x040001F8 RID: 504
		private IExpression expression;
	}
}
