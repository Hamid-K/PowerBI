using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011C6 RID: 4550
	internal class ConstantAnnotationExpression : ScalarExpression
	{
		// Token: 0x0600783E RID: 30782 RVA: 0x001A101C File Offset: 0x0019F21C
		public ConstantAnnotationExpression(Value value, SqlExpression expression)
		{
			this.value = value;
			this.expression = expression;
		}

		// Token: 0x170020E8 RID: 8424
		// (get) Token: 0x0600783F RID: 30783 RVA: 0x001A1032 File Offset: 0x0019F232
		public Value Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170020E9 RID: 8425
		// (get) Token: 0x06007840 RID: 30784 RVA: 0x00002105 File Offset: 0x00000305
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06007841 RID: 30785 RVA: 0x001A103A File Offset: 0x0019F23A
		public override void WriteCreateScript(ScriptWriter writer)
		{
			this.expression.WriteCreateScript(writer);
		}

		// Token: 0x04004168 RID: 16744
		private readonly Value value;

		// Token: 0x04004169 RID: 16745
		private readonly SqlExpression expression;
	}
}
