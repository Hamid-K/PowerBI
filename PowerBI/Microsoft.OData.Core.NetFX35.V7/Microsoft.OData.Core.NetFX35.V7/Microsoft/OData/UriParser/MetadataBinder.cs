using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000ED RID: 237
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Keeping the visitor in one place makes sense.")]
	internal class MetadataBinder
	{
		// Token: 0x06000BAD RID: 2989 RVA: 0x0001DD8F File Offset: 0x0001BF8F
		internal MetadataBinder(BindingState initialState)
		{
			ExceptionUtils.CheckArgumentNotNull<BindingState>(initialState, "initialState");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(initialState.Model, "initialState.Model");
			this.BindingState = initialState;
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0001DDBB File Offset: 0x0001BFBB
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x0001DDC3 File Offset: 0x0001BFC3
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

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0001DDCC File Offset: 0x0001BFCC
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

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0001DE20 File Offset: 0x0001C020
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

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0001DE74 File Offset: 0x0001C074
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

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0001DF04 File Offset: 0x0001C104
		protected internal QueryNode Bind(QueryToken token)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(token, "token");
			this.BindingState.RecurseEnter();
			QueryNode queryNode;
			switch (token.Kind)
			{
			case QueryTokenKind.BinaryOperator:
				queryNode = this.BindBinaryOperator((BinaryOperatorToken)token);
				goto IL_0167;
			case QueryTokenKind.UnaryOperator:
				queryNode = this.BindUnaryOperator((UnaryOperatorToken)token);
				goto IL_0167;
			case QueryTokenKind.Literal:
				queryNode = this.BindLiteral((LiteralToken)token);
				goto IL_0167;
			case QueryTokenKind.FunctionCall:
				queryNode = this.BindFunctionCall((FunctionCallToken)token);
				goto IL_0167;
			case QueryTokenKind.EndPath:
				queryNode = this.BindEndPath((EndPathToken)token);
				goto IL_0167;
			case QueryTokenKind.Any:
				queryNode = this.BindAnyAll((AnyToken)token);
				goto IL_0167;
			case QueryTokenKind.InnerPath:
				queryNode = this.BindInnerPathSegment((InnerPathToken)token);
				goto IL_0167;
			case QueryTokenKind.DottedIdentifier:
				queryNode = this.BindCast((DottedIdentifierToken)token);
				goto IL_0167;
			case QueryTokenKind.RangeVariable:
				queryNode = this.BindRangeVariable((RangeVariableToken)token);
				goto IL_0167;
			case QueryTokenKind.All:
				queryNode = this.BindAnyAll((AllToken)token);
				goto IL_0167;
			case QueryTokenKind.FunctionParameter:
				queryNode = this.BindFunctionParameter((FunctionParameterToken)token);
				goto IL_0167;
			case QueryTokenKind.FunctionParameterAlias:
				queryNode = this.BindParameterAlias((FunctionParameterAliasToken)token);
				goto IL_0167;
			case QueryTokenKind.StringLiteral:
				queryNode = this.BindStringLiteral((StringLiteralToken)token);
				goto IL_0167;
			}
			throw new ODataException(Strings.MetadataBinder_UnsupportedQueryTokenKind(token.Kind));
			IL_0167:
			if (queryNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_BoundNodeCannotBeNull(token.Kind));
			}
			this.BindingState.RecurseLeave();
			return queryNode;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0001E0A0 File Offset: 0x0001C2A0
		protected virtual SingleValueNode BindParameterAlias(FunctionParameterAliasToken functionParameterAliasToken)
		{
			ParameterAliasBinder parameterAliasBinder = new ParameterAliasBinder(new MetadataBinder.QueryTokenVisitor(this.Bind));
			return parameterAliasBinder.BindParameterAlias(this.BindingState, functionParameterAliasToken);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0001E0CC File Offset: 0x0001C2CC
		protected virtual QueryNode BindFunctionParameter(FunctionParameterToken token)
		{
			if (token.ParameterName != null)
			{
				return new NamedFunctionParameterNode(token.ParameterName, this.Bind(token.ValueToken));
			}
			return this.Bind(token.ValueToken);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0001E0FC File Offset: 0x0001C2FC
		protected virtual QueryNode BindInnerPathSegment(InnerPathToken token)
		{
			InnerPathTokenBinder innerPathTokenBinder = new InnerPathTokenBinder(new MetadataBinder.QueryTokenVisitor(this.Bind), this.BindingState);
			return innerPathTokenBinder.BindInnerPathSegment(token);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0001E128 File Offset: 0x0001C328
		protected virtual SingleValueNode BindRangeVariable(RangeVariableToken rangeVariableToken)
		{
			return RangeVariableBinder.BindRangeVariableToken(rangeVariableToken, this.BindingState);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0001E136 File Offset: 0x0001C336
		protected virtual QueryNode BindLiteral(LiteralToken literalToken)
		{
			return LiteralBinder.BindLiteral(literalToken);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0001E140 File Offset: 0x0001C340
		protected virtual QueryNode BindBinaryOperator(BinaryOperatorToken binaryOperatorToken)
		{
			BinaryOperatorBinder binaryOperatorBinder = new BinaryOperatorBinder(new Func<QueryToken, QueryNode>(this.Bind), this.BindingState.Configuration.Resolver);
			return binaryOperatorBinder.BindBinaryOperator(binaryOperatorToken);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0001E178 File Offset: 0x0001C378
		protected virtual QueryNode BindUnaryOperator(UnaryOperatorToken unaryOperatorToken)
		{
			UnaryOperatorBinder unaryOperatorBinder = new UnaryOperatorBinder(new Func<QueryToken, QueryNode>(this.Bind));
			return unaryOperatorBinder.BindUnaryOperator(unaryOperatorToken);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0001E1A0 File Offset: 0x0001C3A0
		protected virtual QueryNode BindCast(DottedIdentifierToken dottedIdentifierToken)
		{
			DottedIdentifierBinder dottedIdentifierBinder = new DottedIdentifierBinder(new MetadataBinder.QueryTokenVisitor(this.Bind), this.BindingState);
			return dottedIdentifierBinder.BindDottedIdentifier(dottedIdentifierToken);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0001E1CC File Offset: 0x0001C3CC
		protected virtual QueryNode BindAnyAll(LambdaToken lambdaToken)
		{
			ExceptionUtils.CheckArgumentNotNull<LambdaToken>(lambdaToken, "LambdaToken");
			LambdaBinder lambdaBinder = new LambdaBinder(new MetadataBinder.QueryTokenVisitor(this.Bind));
			return lambdaBinder.BindLambdaToken(lambdaToken, this.BindingState);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0001E204 File Offset: 0x0001C404
		protected virtual QueryNode BindEndPath(EndPathToken endPathToken)
		{
			EndPathBinder endPathBinder = new EndPathBinder(new MetadataBinder.QueryTokenVisitor(this.Bind), this.BindingState);
			return endPathBinder.BindEndPath(endPathToken);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0001E230 File Offset: 0x0001C430
		protected virtual QueryNode BindFunctionCall(FunctionCallToken functionCallToken)
		{
			FunctionCallBinder functionCallBinder = new FunctionCallBinder(new MetadataBinder.QueryTokenVisitor(this.Bind), this.BindingState);
			return functionCallBinder.BindFunctionCall(functionCallToken);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0001E25C File Offset: 0x0001C45C
		protected virtual QueryNode BindStringLiteral(StringLiteralToken stringLiteralToken)
		{
			return new SearchTermNode(stringLiteralToken.Text);
		}

		// Token: 0x04000695 RID: 1685
		private BindingState bindingState;

		// Token: 0x020002AB RID: 683
		// (Invoke) Token: 0x06001872 RID: 6258
		internal delegate QueryNode QueryTokenVisitor(QueryToken token);
	}
}
