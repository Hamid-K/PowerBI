using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001CF RID: 463
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Keeping the visitor in one place makes sense.")]
	internal class MetadataBinder
	{
		// Token: 0x06001135 RID: 4405 RVA: 0x0003CD12 File Offset: 0x0003AF12
		internal MetadataBinder(BindingState initialState)
		{
			ExceptionUtils.CheckArgumentNotNull<BindingState>(initialState, "initialState");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(initialState.Model, "initialState.Model");
			this.BindingState = initialState;
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x0003CD3C File Offset: 0x0003AF3C
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x0003CD44 File Offset: 0x0003AF44
		internal BindingState BindingState
		{
			get
			{
				return this.bindingState;
			}
			private set
			{
				this.bindingState = value;
			}
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0003CD50 File Offset: 0x0003AF50
		public static long? ProcessSkip(long? skip)
		{
			if (skip == null)
			{
				return default(long?);
			}
			if (skip < 0L)
			{
				throw new ODataException(Strings.MetadataBinder_SkipRequiresNonNegativeInteger(skip.ToString()));
			}
			return skip;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0003CDA4 File Offset: 0x0003AFA4
		public static long? ProcessTop(long? top)
		{
			if (top == null)
			{
				return default(long?);
			}
			if (top < 0L)
			{
				throw new ODataException(Strings.MetadataBinder_TopRequiresNonNegativeInteger(top.ToString()));
			}
			return top;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0003CDF8 File Offset: 0x0003AFF8
		public static List<QueryNode> ProcessQueryOptions(BindingState bindingState, MetadataBinder.QueryTokenVisitor bindMethod)
		{
			List<QueryNode> list = new List<QueryNode>();
			foreach (CustomQueryOptionToken customQueryOptionToken in bindingState.QueryOptions)
			{
				QueryNode queryNode = bindMethod(customQueryOptionToken);
				if (queryNode != null)
				{
					list.Add(queryNode);
				}
			}
			bindingState.QueryOptions = null;
			return list;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0003CE64 File Offset: 0x0003B064
		protected internal QueryNode Bind(QueryToken token)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(token, "token");
			this.BindingState.RecurseEnter();
			QueryNode queryNode;
			switch (token.Kind)
			{
			case QueryTokenKind.BinaryOperator:
				queryNode = this.BindBinaryOperator((BinaryOperatorToken)token);
				goto IL_0166;
			case QueryTokenKind.UnaryOperator:
				queryNode = this.BindUnaryOperator((UnaryOperatorToken)token);
				goto IL_0166;
			case QueryTokenKind.Literal:
				queryNode = this.BindLiteral((LiteralToken)token);
				goto IL_0166;
			case QueryTokenKind.FunctionCall:
				queryNode = this.BindFunctionCall((FunctionCallToken)token);
				goto IL_0166;
			case QueryTokenKind.EndPath:
				queryNode = this.BindEndPath((EndPathToken)token);
				goto IL_0166;
			case QueryTokenKind.Any:
				queryNode = this.BindAnyAll((AnyToken)token);
				goto IL_0166;
			case QueryTokenKind.InnerPath:
				queryNode = this.BindInnerPathSegment((InnerPathToken)token);
				goto IL_0166;
			case QueryTokenKind.DottedIdentifier:
				queryNode = this.BindCast((DottedIdentifierToken)token);
				goto IL_0166;
			case QueryTokenKind.RangeVariable:
				queryNode = this.BindRangeVariable((RangeVariableToken)token);
				goto IL_0166;
			case QueryTokenKind.All:
				queryNode = this.BindAnyAll((AllToken)token);
				goto IL_0166;
			case QueryTokenKind.FunctionParameter:
				queryNode = this.BindFunctionParameter((FunctionParameterToken)token);
				goto IL_0166;
			case QueryTokenKind.FunctionParameterAlias:
				queryNode = this.BindParameterAlias((FunctionParameterAliasToken)token);
				goto IL_0166;
			case QueryTokenKind.StringLiteral:
				queryNode = this.BindStringLiteral((StringLiteralToken)token);
				goto IL_0166;
			}
			throw new ODataException(Strings.MetadataBinder_UnsupportedQueryTokenKind(token.Kind));
			IL_0166:
			if (queryNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_BoundNodeCannotBeNull(token.Kind));
			}
			this.BindingState.RecurseLeave();
			return queryNode;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0003CFFC File Offset: 0x0003B1FC
		protected virtual SingleValueNode BindParameterAlias(FunctionParameterAliasToken functionParameterAliasToken)
		{
			ParameterAliasBinder parameterAliasBinder = new ParameterAliasBinder(new MetadataBinder.QueryTokenVisitor(this.Bind));
			return parameterAliasBinder.BindParameterAlias(this.BindingState, functionParameterAliasToken);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0003D028 File Offset: 0x0003B228
		protected virtual QueryNode BindFunctionParameter(FunctionParameterToken token)
		{
			if (token.ParameterName != null)
			{
				return new NamedFunctionParameterNode(token.ParameterName, this.Bind(token.ValueToken));
			}
			return this.Bind(token.ValueToken);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0003D058 File Offset: 0x0003B258
		protected virtual QueryNode BindInnerPathSegment(InnerPathToken token)
		{
			InnerPathTokenBinder innerPathTokenBinder = new InnerPathTokenBinder(new MetadataBinder.QueryTokenVisitor(this.Bind), this.BindingState);
			return innerPathTokenBinder.BindInnerPathSegment(token);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0003D084 File Offset: 0x0003B284
		protected virtual SingleValueNode BindRangeVariable(RangeVariableToken rangeVariableToken)
		{
			return RangeVariableBinder.BindRangeVariableToken(rangeVariableToken, this.BindingState);
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0003D092 File Offset: 0x0003B292
		protected virtual QueryNode BindLiteral(LiteralToken literalToken)
		{
			return LiteralBinder.BindLiteral(literalToken);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0003D09C File Offset: 0x0003B29C
		protected virtual QueryNode BindBinaryOperator(BinaryOperatorToken binaryOperatorToken)
		{
			BinaryOperatorBinder binaryOperatorBinder = new BinaryOperatorBinder(new Func<QueryToken, QueryNode>(this.Bind), this.BindingState.Configuration.Resolver);
			return binaryOperatorBinder.BindBinaryOperator(binaryOperatorToken);
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0003D0D4 File Offset: 0x0003B2D4
		protected virtual QueryNode BindUnaryOperator(UnaryOperatorToken unaryOperatorToken)
		{
			UnaryOperatorBinder unaryOperatorBinder = new UnaryOperatorBinder(new Func<QueryToken, QueryNode>(this.Bind));
			return unaryOperatorBinder.BindUnaryOperator(unaryOperatorToken);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0003D0FC File Offset: 0x0003B2FC
		protected virtual QueryNode BindCast(DottedIdentifierToken dottedIdentifierToken)
		{
			DottedIdentifierBinder dottedIdentifierBinder = new DottedIdentifierBinder(new MetadataBinder.QueryTokenVisitor(this.Bind), this.BindingState);
			return dottedIdentifierBinder.BindDottedIdentifier(dottedIdentifierToken);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0003D128 File Offset: 0x0003B328
		protected virtual QueryNode BindAnyAll(LambdaToken lambdaToken)
		{
			ExceptionUtils.CheckArgumentNotNull<LambdaToken>(lambdaToken, "LambdaToken");
			LambdaBinder lambdaBinder = new LambdaBinder(new MetadataBinder.QueryTokenVisitor(this.Bind));
			return lambdaBinder.BindLambdaToken(lambdaToken, this.BindingState);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0003D160 File Offset: 0x0003B360
		protected virtual QueryNode BindEndPath(EndPathToken endPathToken)
		{
			EndPathBinder endPathBinder = new EndPathBinder(new MetadataBinder.QueryTokenVisitor(this.Bind), this.BindingState);
			return endPathBinder.BindEndPath(endPathToken);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0003D18C File Offset: 0x0003B38C
		protected virtual QueryNode BindFunctionCall(FunctionCallToken functionCallToken)
		{
			FunctionCallBinder functionCallBinder = new FunctionCallBinder(new MetadataBinder.QueryTokenVisitor(this.Bind), this.BindingState);
			return functionCallBinder.BindFunctionCall(functionCallToken);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0003D1B8 File Offset: 0x0003B3B8
		protected virtual QueryNode BindStringLiteral(StringLiteralToken stringLiteralToken)
		{
			return new SearchTermNode(stringLiteralToken.Text);
		}

		// Token: 0x04000788 RID: 1928
		private BindingState bindingState;

		// Token: 0x020001D0 RID: 464
		// (Invoke) Token: 0x06001149 RID: 4425
		internal delegate QueryNode QueryTokenVisitor(QueryToken token);
	}
}
