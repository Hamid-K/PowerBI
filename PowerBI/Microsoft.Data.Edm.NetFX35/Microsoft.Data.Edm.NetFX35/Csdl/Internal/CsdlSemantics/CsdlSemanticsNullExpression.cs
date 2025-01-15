using System;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x02000081 RID: 129
	internal class CsdlSemanticsNullExpression : CsdlSemanticsExpression, IEdmNullExpression, IEdmExpression, IEdmNullValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000210 RID: 528 RVA: 0x00005B31 File Offset: 0x00003D31
		public CsdlSemanticsNullExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00005B42 File Offset: 0x00003D42
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00005B4A File Offset: 0x00003D4A
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Null;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00005B4E File Offset: 0x00003D4E
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00005B5B File Offset: 0x00003D5B
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000EB RID: 235
		private readonly CsdlConstantExpression expression;
	}
}
