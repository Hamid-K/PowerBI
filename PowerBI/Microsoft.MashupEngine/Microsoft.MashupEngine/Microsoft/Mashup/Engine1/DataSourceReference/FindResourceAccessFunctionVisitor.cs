using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018C3 RID: 6339
	internal class FindResourceAccessFunctionVisitor : AstVisitor
	{
		// Token: 0x0600A1A4 RID: 41380 RVA: 0x00213EDC File Offset: 0x002120DC
		private FindResourceAccessFunctionVisitor()
		{
		}

		// Token: 0x0600A1A5 RID: 41381 RVA: 0x00218AEC File Offset: 0x00216CEC
		public static FunctionValue Find(IExpression expression)
		{
			FindResourceAccessFunctionVisitor findResourceAccessFunctionVisitor = new FindResourceAccessFunctionVisitor();
			findResourceAccessFunctionVisitor.VisitExpression(expression);
			if (findResourceAccessFunctionVisitor.found == null)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.ExpectedSingleDataSourceFunction, null, null);
			}
			return findResourceAccessFunctionVisitor.found;
		}

		// Token: 0x0600A1A6 RID: 41382 RVA: 0x00218B18 File Offset: 0x00216D18
		protected override IExpression VisitConstant(IConstantExpression identifier)
		{
			if (identifier.Value.IsFunction && identifier.Value.AsFunction.IsResourceAccessFunction)
			{
				if (this.found != null)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.ExpectedSingleDataSourceFunction, null, null);
				}
				this.found = identifier.Value.AsFunction;
			}
			return base.VisitConstant(identifier);
		}

		// Token: 0x04005495 RID: 21653
		private FunctionValue found;
	}
}
