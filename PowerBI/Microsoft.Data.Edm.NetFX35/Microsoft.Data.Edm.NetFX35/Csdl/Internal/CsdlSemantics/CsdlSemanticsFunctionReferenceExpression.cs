using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x02000050 RID: 80
	internal class CsdlSemanticsFunctionReferenceExpression : CsdlSemanticsExpression, IEdmFunctionReferenceExpression, IEdmExpression, IEdmElement, IEdmCheckable
	{
		// Token: 0x0600012E RID: 302 RVA: 0x000046C3 File Offset: 0x000028C3
		public CsdlSemanticsFunctionReferenceExpression(CsdlFunctionReferenceExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000046E6 File Offset: 0x000028E6
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000046EE File Offset: 0x000028EE
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.FunctionReference;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000046F2 File Offset: 0x000028F2
		public IEdmFunction ReferencedFunction
		{
			get
			{
				return this.referencedCache.GetValue(this, CsdlSemanticsFunctionReferenceExpression.ComputeReferencedFunc, null);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00004706 File Offset: 0x00002906
		public IEnumerable<EdmError> Errors
		{
			get
			{
				if (this.ReferencedFunction is IUnresolvedElement)
				{
					return this.ReferencedFunction.Errors();
				}
				return Enumerable.Empty<EdmError>();
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004726 File Offset: 0x00002926
		private IEdmFunction ComputeReferenced()
		{
			return new UnresolvedFunction(this.expression.Function, Strings.Bad_UnresolvedFunction(this.expression.Function), base.Location);
		}

		// Token: 0x04000073 RID: 115
		private readonly CsdlFunctionReferenceExpression expression;

		// Token: 0x04000074 RID: 116
		private readonly IEdmEntityType bindingContext;

		// Token: 0x04000075 RID: 117
		private readonly Cache<CsdlSemanticsFunctionReferenceExpression, IEdmFunction> referencedCache = new Cache<CsdlSemanticsFunctionReferenceExpression, IEdmFunction>();

		// Token: 0x04000076 RID: 118
		private static readonly Func<CsdlSemanticsFunctionReferenceExpression, IEdmFunction> ComputeReferencedFunc = (CsdlSemanticsFunctionReferenceExpression me) => me.ComputeReferenced();
	}
}
