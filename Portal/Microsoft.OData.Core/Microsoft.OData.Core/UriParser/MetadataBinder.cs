using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200012B RID: 299
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Keeping the visitor in one place makes sense.")]
	internal class MetadataBinder
	{
		// Token: 0x06001008 RID: 4104 RVA: 0x00029700 File Offset: 0x00027900
		internal MetadataBinder(BindingState initialState)
		{
			ExceptionUtils.CheckArgumentNotNull<BindingState>(initialState, "initialState");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(initialState.Model, "initialState.Model");
			this.BindingState = initialState;
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x0002972C File Offset: 0x0002792C
		// (set) Token: 0x0600100A RID: 4106 RVA: 0x00029734 File Offset: 0x00027934
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

		// Token: 0x0600100B RID: 4107 RVA: 0x00029740 File Offset: 0x00027940
		public static long? ProcessSkip(long? skip)
		{
			if (skip == null)
			{
				return null;
			}
			if (skip < 0L)
			{
				throw new ODataException(Strings.MetadataBinder_SkipRequiresNonNegativeInteger(skip.ToString()));
			}
			return skip;
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00029794 File Offset: 0x00027994
		public static long? ProcessTop(long? top)
		{
			if (top == null)
			{
				return null;
			}
			if (top < 0L)
			{
				throw new ODataException(Strings.MetadataBinder_TopRequiresNonNegativeInteger(top.ToString()));
			}
			return top;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x000297E8 File Offset: 0x000279E8
		public static List<QueryNode> ProcessQueryOptions(BindingState bindingState, MetadataBinder.QueryTokenVisitor bindMethod)
		{
			if (bindingState == null || bindingState.QueryOptions == null)
			{
				throw new ODataException(Strings.MetadataBinder_QueryOptionsBindStateCannotBeNull);
			}
			if (bindMethod == null)
			{
				throw new ODataException(Strings.MetadataBinder_QueryOptionsBindMethodCannotBeNull);
			}
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

		// Token: 0x0600100E RID: 4110 RVA: 0x00029878 File Offset: 0x00027A78
		protected internal QueryNode Bind(QueryToken token)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(token, "token");
			this.BindingState.RecurseEnter();
			QueryTokenKind kind = token.Kind;
			QueryNode queryNode;
			switch (kind)
			{
			case QueryTokenKind.BinaryOperator:
				queryNode = this.BindBinaryOperator((BinaryOperatorToken)token);
				goto IL_0181;
			case QueryTokenKind.UnaryOperator:
				queryNode = this.BindUnaryOperator((UnaryOperatorToken)token);
				goto IL_0181;
			case QueryTokenKind.Literal:
				queryNode = this.BindLiteral((LiteralToken)token);
				goto IL_0181;
			case QueryTokenKind.FunctionCall:
				queryNode = this.BindFunctionCall((FunctionCallToken)token);
				goto IL_0181;
			case QueryTokenKind.EndPath:
				queryNode = this.BindEndPath((EndPathToken)token);
				goto IL_0181;
			case QueryTokenKind.OrderBy:
			case QueryTokenKind.CustomQueryOption:
			case QueryTokenKind.Select:
			case QueryTokenKind.Star:
			case (QueryTokenKind)12:
			case QueryTokenKind.Expand:
			case QueryTokenKind.TypeSegment:
			case QueryTokenKind.ExpandTerm:
				break;
			case QueryTokenKind.Any:
				queryNode = this.BindAnyAll((AnyToken)token);
				goto IL_0181;
			case QueryTokenKind.InnerPath:
				queryNode = this.BindInnerPathSegment((InnerPathToken)token);
				goto IL_0181;
			case QueryTokenKind.DottedIdentifier:
				queryNode = this.BindCast((DottedIdentifierToken)token);
				goto IL_0181;
			case QueryTokenKind.RangeVariable:
				queryNode = this.BindRangeVariable((RangeVariableToken)token);
				goto IL_0181;
			case QueryTokenKind.All:
				queryNode = this.BindAnyAll((AllToken)token);
				goto IL_0181;
			case QueryTokenKind.FunctionParameter:
				queryNode = this.BindFunctionParameter((FunctionParameterToken)token);
				goto IL_0181;
			case QueryTokenKind.FunctionParameterAlias:
				queryNode = this.BindParameterAlias((FunctionParameterAliasToken)token);
				goto IL_0181;
			case QueryTokenKind.StringLiteral:
				queryNode = this.BindStringLiteral((StringLiteralToken)token);
				goto IL_0181;
			default:
				if (kind == QueryTokenKind.In)
				{
					queryNode = this.BindIn((InToken)token);
					goto IL_0181;
				}
				break;
			}
			throw new ODataException(Strings.MetadataBinder_UnsupportedQueryTokenKind(token.Kind));
			IL_0181:
			if (queryNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_BoundNodeCannotBeNull(token.Kind));
			}
			this.BindingState.RecurseLeave();
			return queryNode;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00029A2C File Offset: 0x00027C2C
		protected virtual SingleValueNode BindParameterAlias(FunctionParameterAliasToken functionParameterAliasToken)
		{
			ParameterAliasBinder parameterAliasBinder = new ParameterAliasBinder(new MetadataBinder.QueryTokenVisitor(this.Bind));
			return parameterAliasBinder.BindParameterAlias(this.BindingState, functionParameterAliasToken);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00029A58 File Offset: 0x00027C58
		protected virtual QueryNode BindFunctionParameter(FunctionParameterToken token)
		{
			if (token.ParameterName != null)
			{
				return new NamedFunctionParameterNode(token.ParameterName, this.Bind(token.ValueToken));
			}
			return this.Bind(token.ValueToken);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00029A88 File Offset: 0x00027C88
		protected virtual QueryNode BindInnerPathSegment(InnerPathToken token)
		{
			InnerPathTokenBinder innerPathTokenBinder = new InnerPathTokenBinder(new MetadataBinder.QueryTokenVisitor(this.Bind), this.BindingState);
			return innerPathTokenBinder.BindInnerPathSegment(token);
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00029AB4 File Offset: 0x00027CB4
		protected virtual SingleValueNode BindRangeVariable(RangeVariableToken rangeVariableToken)
		{
			return RangeVariableBinder.BindRangeVariableToken(rangeVariableToken, this.BindingState);
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00029AC2 File Offset: 0x00027CC2
		protected virtual QueryNode BindLiteral(LiteralToken literalToken)
		{
			return LiteralBinder.BindLiteral(literalToken);
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00029ACC File Offset: 0x00027CCC
		protected virtual QueryNode BindBinaryOperator(BinaryOperatorToken binaryOperatorToken)
		{
			BinaryOperatorBinder binaryOperatorBinder = new BinaryOperatorBinder(new Func<QueryToken, QueryNode>(this.Bind), this.BindingState.Configuration.Resolver);
			return binaryOperatorBinder.BindBinaryOperator(binaryOperatorToken);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00029B04 File Offset: 0x00027D04
		protected virtual QueryNode BindUnaryOperator(UnaryOperatorToken unaryOperatorToken)
		{
			UnaryOperatorBinder unaryOperatorBinder = new UnaryOperatorBinder(new Func<QueryToken, QueryNode>(this.Bind));
			return unaryOperatorBinder.BindUnaryOperator(unaryOperatorToken);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00029B2C File Offset: 0x00027D2C
		protected virtual QueryNode BindCast(DottedIdentifierToken dottedIdentifierToken)
		{
			DottedIdentifierBinder dottedIdentifierBinder = new DottedIdentifierBinder(new MetadataBinder.QueryTokenVisitor(this.Bind), this.BindingState);
			return dottedIdentifierBinder.BindDottedIdentifier(dottedIdentifierToken);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00029B58 File Offset: 0x00027D58
		protected virtual QueryNode BindAnyAll(LambdaToken lambdaToken)
		{
			ExceptionUtils.CheckArgumentNotNull<LambdaToken>(lambdaToken, "LambdaToken");
			LambdaBinder lambdaBinder = new LambdaBinder(new MetadataBinder.QueryTokenVisitor(this.Bind));
			return lambdaBinder.BindLambdaToken(lambdaToken, this.BindingState);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00029B90 File Offset: 0x00027D90
		protected virtual QueryNode BindEndPath(EndPathToken endPathToken)
		{
			EndPathBinder endPathBinder = new EndPathBinder(new MetadataBinder.QueryTokenVisitor(this.Bind), this.BindingState);
			return endPathBinder.BindEndPath(endPathToken);
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00029BBC File Offset: 0x00027DBC
		protected virtual QueryNode BindFunctionCall(FunctionCallToken functionCallToken)
		{
			FunctionCallBinder functionCallBinder = new FunctionCallBinder(new MetadataBinder.QueryTokenVisitor(this.Bind), this.BindingState);
			return functionCallBinder.BindFunctionCall(functionCallToken);
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00029BE8 File Offset: 0x00027DE8
		protected virtual QueryNode BindStringLiteral(StringLiteralToken stringLiteralToken)
		{
			return new SearchTermNode(stringLiteralToken.Text);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00029BF8 File Offset: 0x00027DF8
		protected virtual QueryNode BindIn(InToken inToken)
		{
			Func<QueryToken, QueryNode> func = delegate(QueryToken queryToken)
			{
				ExceptionUtils.CheckArgumentNotNull<QueryToken>(queryToken, "queryToken");
				if (queryToken.Kind == QueryTokenKind.Literal)
				{
					return LiteralBinder.BindInLiteral((LiteralToken)queryToken);
				}
				return this.Bind(queryToken);
			};
			InBinder inBinder = new InBinder(func);
			return inBinder.BindInOperator(inToken, this.BindingState);
		}

		// Token: 0x040007A8 RID: 1960
		private BindingState bindingState;

		// Token: 0x0200037F RID: 895
		// (Invoke) Token: 0x06001F44 RID: 8004
		internal delegate QueryNode QueryTokenVisitor(QueryToken token);
	}
}
