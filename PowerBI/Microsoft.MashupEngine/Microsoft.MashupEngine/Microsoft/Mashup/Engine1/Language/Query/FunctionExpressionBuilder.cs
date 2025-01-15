using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001892 RID: 6290
	internal struct FunctionExpressionBuilder
	{
		// Token: 0x06009F8F RID: 40847 RVA: 0x0020F278 File Offset: 0x0020D478
		public static IFunctionExpression ToExpression(Keys columns, Identifier parameter, QueryExpression queryExpression)
		{
			return new FunctionExpressionSyntaxNode(new FunctionTypeSyntaxNode(null, new IParameter[]
			{
				new ParameterSyntaxNode(parameter, null)
			}, 1), new FunctionExpressionBuilder(columns, parameter).VisitExpression(queryExpression));
		}

		// Token: 0x06009F90 RID: 40848 RVA: 0x0020F2B1 File Offset: 0x0020D4B1
		public FunctionExpressionBuilder(Keys columns, Identifier parameter)
		{
			this.columns = columns;
			this.parameter = parameter;
		}

		// Token: 0x06009F91 RID: 40849 RVA: 0x0020F2C4 File Offset: 0x0020D4C4
		private IExpression VisitExpression(QueryExpression expression)
		{
			switch (expression.Kind)
			{
			case QueryExpressionKind.Binary:
				return this.VisitBinary((BinaryQueryExpression)expression);
			case QueryExpressionKind.Constant:
				return this.VisitConstant((ConstantQueryExpression)expression);
			case QueryExpressionKind.ColumnAccess:
				return this.VisitColumnAccess((ColumnAccessQueryExpression)expression);
			case QueryExpressionKind.If:
				return this.VisitIf((IfQueryExpression)expression);
			case QueryExpressionKind.Invocation:
				return this.VisitInvocation((InvocationQueryExpression)expression);
			case QueryExpressionKind.Unary:
				return this.VisitUnary((UnaryQueryExpression)expression);
			case QueryExpressionKind.ArgumentAccess:
				return new InclusiveIdentifierExpressionSyntaxNode(this.parameter);
			default:
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Expression not supported: {0}.", expression.Kind));
			}
		}

		// Token: 0x06009F92 RID: 40850 RVA: 0x0020F375 File Offset: 0x0020D575
		private IExpression VisitBinary(BinaryQueryExpression binary)
		{
			return BinaryExpressionSyntaxNode.New(binary.Operator, this.VisitExpression(binary.Left), this.VisitExpression(binary.Right), TokenRange.Null);
		}

		// Token: 0x06009F93 RID: 40851 RVA: 0x0020F39F File Offset: 0x0020D59F
		private IExpression VisitConstant(ConstantQueryExpression constant)
		{
			return new ConstantExpressionSyntaxNode(constant.Value);
		}

		// Token: 0x06009F94 RID: 40852 RVA: 0x0020F3AC File Offset: 0x0020D5AC
		private IExpression VisitColumnAccess(ColumnAccessQueryExpression columnAccess)
		{
			return new RequiredFieldAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(this.parameter), Identifier.New(this.columns[columnAccess.Column]));
		}

		// Token: 0x06009F95 RID: 40853 RVA: 0x0020F3D4 File Offset: 0x0020D5D4
		private IExpression VisitIf(IfQueryExpression ifExpression)
		{
			return new IfExpressionSyntaxNode(this.VisitExpression(ifExpression.Condition), this.VisitExpression(ifExpression.TrueCase), this.VisitExpression(ifExpression.FalseCase), TokenRange.Null);
		}

		// Token: 0x06009F96 RID: 40854 RVA: 0x0020F404 File Offset: 0x0020D604
		private IExpression VisitInvocation(InvocationQueryExpression invocation)
		{
			IList<QueryExpression> list;
			Value value;
			if (invocation.TryGetInvocation(Library.Record.Field, 2, out list) && list[1].TryGetConstant(out value) && value.IsText)
			{
				return new RequiredFieldAccessExpressionSyntaxNode(this.VisitExpression(list[0]), value.AsString);
			}
			IExpression[] array = new IExpression[invocation.Arguments.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.VisitExpression(invocation.Arguments[i]);
			}
			return new InvocationExpressionSyntaxNodeN(this.VisitExpression(invocation.Function), array);
		}

		// Token: 0x06009F97 RID: 40855 RVA: 0x0020F49D File Offset: 0x0020D69D
		private IExpression VisitUnary(UnaryQueryExpression unary)
		{
			return UnaryExpressionSyntaxNode.New(unary.Operator, this.VisitExpression(unary.Expression), TokenRange.Null);
		}

		// Token: 0x040053AB RID: 21419
		private readonly Identifier parameter;

		// Token: 0x040053AC RID: 21420
		private readonly Keys columns;
	}
}
