using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018B1 RID: 6321
	internal sealed class ConstantListExpressionSyntaxNode : ListExpressionSyntaxNode, IConstantValue, IConstantValue2
	{
		// Token: 0x0600A100 RID: 41216 RVA: 0x00215EF9 File Offset: 0x002140F9
		public ConstantListExpressionSyntaxNode(Value value, IList<IExpression> elements)
			: this(value, elements, TokenRange.Null)
		{
		}

		// Token: 0x0600A101 RID: 41217 RVA: 0x00215F08 File Offset: 0x00214108
		public ConstantListExpressionSyntaxNode(Value value, IList<IExpression> elements, TokenRange range)
			: base(elements, range)
		{
			this.value = value;
		}

		// Token: 0x17002939 RID: 10553
		// (get) Token: 0x0600A102 RID: 41218 RVA: 0x00215F19 File Offset: 0x00214119
		public Value Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700293A RID: 10554
		// (get) Token: 0x0600A103 RID: 41219 RVA: 0x00215F19 File Offset: 0x00214119
		IValue IConstantValue2.Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400546C RID: 21612
		private Value value;
	}
}
