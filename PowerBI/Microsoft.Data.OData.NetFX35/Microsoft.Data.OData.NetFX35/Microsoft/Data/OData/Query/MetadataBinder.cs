using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000CE RID: 206
	internal class MetadataBinder
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x000115D7 File Offset: 0x0000F7D7
		internal MetadataBinder(BindingState initialState)
		{
			ExceptionUtils.CheckArgumentNotNull<BindingState>(initialState, "initialState");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(initialState.Model, "initialState.Model");
			this.BindingState = initialState;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00011601 File Offset: 0x0000F801
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x00011609 File Offset: 0x0000F809
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

		// Token: 0x06000508 RID: 1288 RVA: 0x00011614 File Offset: 0x0000F814
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

		// Token: 0x06000509 RID: 1289 RVA: 0x00011668 File Offset: 0x0000F868
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

		// Token: 0x0600050A RID: 1290 RVA: 0x000116BC File Offset: 0x0000F8BC
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

		// Token: 0x0600050B RID: 1291 RVA: 0x00011728 File Offset: 0x0000F928
		protected internal QueryNode Bind(QueryToken token)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(token, "token");
			QueryNode queryNode;
			switch (token.Kind)
			{
			case QueryTokenKind.BinaryOperator:
				queryNode = this.BindBinaryOperator((BinaryOperatorToken)token);
				goto IL_012F;
			case QueryTokenKind.UnaryOperator:
				queryNode = this.BindUnaryOperator((UnaryOperatorToken)token);
				goto IL_012F;
			case QueryTokenKind.Literal:
				queryNode = this.BindLiteral((LiteralToken)token);
				goto IL_012F;
			case QueryTokenKind.FunctionCall:
				queryNode = this.BindFunctionCall((FunctionCallToken)token);
				goto IL_012F;
			case QueryTokenKind.EndPath:
				queryNode = this.BindEndPath((EndPathToken)token);
				goto IL_012F;
			case QueryTokenKind.Any:
				queryNode = this.BindAnyAll((AnyToken)token);
				goto IL_012F;
			case QueryTokenKind.InnerPath:
				queryNode = this.BindInnerPathSegment((InnerPathToken)token);
				goto IL_012F;
			case QueryTokenKind.DottedIdentifier:
				queryNode = this.BindCast((DottedIdentifierToken)token);
				goto IL_012F;
			case QueryTokenKind.RangeVariable:
				queryNode = this.BindRangeVariable((RangeVariableToken)token);
				goto IL_012F;
			case QueryTokenKind.All:
				queryNode = this.BindAnyAll((AllToken)token);
				goto IL_012F;
			case QueryTokenKind.FunctionParameter:
				queryNode = this.BindFunctionParameter((FunctionParameterToken)token);
				goto IL_012F;
			}
			throw new ODataException(Strings.MetadataBinder_UnsupportedQueryTokenKind(token.Kind));
			IL_012F:
			if (queryNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_BoundNodeCannotBeNull(token.Kind));
			}
			return queryNode;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001187E File Offset: 0x0000FA7E
		protected virtual QueryNode BindFunctionParameter(FunctionParameterToken token)
		{
			if (token.ParameterName != null)
			{
				return new NamedFunctionParameterNode(token.ParameterName, this.Bind(token.ValueToken));
			}
			return this.Bind(token.ValueToken);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000118AC File Offset: 0x0000FAAC
		protected virtual QueryNode BindInnerPathSegment(InnerPathToken token)
		{
			InnerPathTokenBinder innerPathTokenBinder = new InnerPathTokenBinder(new MetadataBinder.QueryTokenVisitor(this.Bind));
			return innerPathTokenBinder.BindInnerPathSegment(token, this.BindingState);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000118D8 File Offset: 0x0000FAD8
		protected virtual SingleValueNode BindRangeVariable(RangeVariableToken rangeVariableToken)
		{
			return RangeVariableBinder.BindRangeVariableToken(rangeVariableToken, this.BindingState);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000118E6 File Offset: 0x0000FAE6
		protected virtual QueryNode BindLiteral(LiteralToken literalToken)
		{
			return LiteralBinder.BindLiteral(literalToken);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000118F0 File Offset: 0x0000FAF0
		protected virtual QueryNode BindBinaryOperator(BinaryOperatorToken binaryOperatorToken)
		{
			BinaryOperatorBinder binaryOperatorBinder = new BinaryOperatorBinder(new Func<QueryToken, QueryNode>(this.Bind));
			return binaryOperatorBinder.BindBinaryOperator(binaryOperatorToken);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00011918 File Offset: 0x0000FB18
		protected virtual QueryNode BindUnaryOperator(UnaryOperatorToken unaryOperatorToken)
		{
			UnaryOperatorBinder unaryOperatorBinder = new UnaryOperatorBinder(new Func<QueryToken, QueryNode>(this.Bind));
			return unaryOperatorBinder.BindUnaryOperator(unaryOperatorToken);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00011940 File Offset: 0x0000FB40
		protected virtual QueryNode BindCast(DottedIdentifierToken dottedIdentifierToken)
		{
			DottedIdentifierBinder dottedIdentifierBinder = new DottedIdentifierBinder(new MetadataBinder.QueryTokenVisitor(this.Bind));
			return dottedIdentifierBinder.BindDottedIdentifier(dottedIdentifierToken, this.BindingState);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001196C File Offset: 0x0000FB6C
		protected virtual QueryNode BindAnyAll(LambdaToken lambdaToken)
		{
			ExceptionUtils.CheckArgumentNotNull<LambdaToken>(lambdaToken, "LambdaToken");
			LambdaBinder lambdaBinder = new LambdaBinder(new MetadataBinder.QueryTokenVisitor(this.Bind));
			return lambdaBinder.BindLambdaToken(lambdaToken, this.BindingState);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000119A4 File Offset: 0x0000FBA4
		protected virtual QueryNode BindEndPath(EndPathToken endPathToken)
		{
			EndPathBinder endPathBinder = new EndPathBinder(new MetadataBinder.QueryTokenVisitor(this.Bind));
			return endPathBinder.BindEndPath(endPathToken, this.BindingState);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000119D0 File Offset: 0x0000FBD0
		protected virtual QueryNode BindFunctionCall(FunctionCallToken functionCallToken)
		{
			FunctionCallBinder functionCallBinder = new FunctionCallBinder(new MetadataBinder.QueryTokenVisitor(this.Bind));
			return functionCallBinder.BindFunctionCall(functionCallToken, this.BindingState);
		}

		// Token: 0x040001DC RID: 476
		private BindingState bindingState;

		// Token: 0x020000CF RID: 207
		// (Invoke) Token: 0x06000517 RID: 1303
		internal delegate QueryNode QueryTokenVisitor(QueryToken token);
	}
}
