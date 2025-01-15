using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200017E RID: 382
	[Serializable]
	internal class TSqlFragmentFactory
	{
		// Token: 0x06002124 RID: 8484 RVA: 0x0015C98C File Offset: 0x0015AB8C
		public void SetTokenStream(IList<TSqlParserToken> tokenStream)
		{
			this._tokenStream = tokenStream;
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0015C998 File Offset: 0x0015AB98
		public virtual FragmentType CreateFragment<FragmentType>() where FragmentType : TSqlFragment, new()
		{
			Func<TSqlFragment> func;
			TSqlFragment tsqlFragment;
			if (TSqlFragmentFactory.ctors.TryGetValue(typeof(FragmentType), ref func))
			{
				tsqlFragment = func.Invoke();
			}
			else
			{
				tsqlFragment = new FragmentType();
			}
			tsqlFragment.ScriptTokenStream = this._tokenStream;
			return (FragmentType)((object)tsqlFragment);
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x0015CABC File Offset: 0x0015ACBC
		// Note: this type is marked as 'beforefieldinit'.
		static TSqlFragmentFactory()
		{
			Dictionary<Type, Func<TSqlFragment>> dictionary = new Dictionary<Type, Func<TSqlFragment>>();
			dictionary.Add(typeof(Identifier), () => new Identifier());
			dictionary.Add(typeof(MultiPartIdentifier), () => new MultiPartIdentifier());
			dictionary.Add(typeof(SchemaObjectName), () => new SchemaObjectName());
			dictionary.Add(typeof(SelectScalarExpression), () => new SelectScalarExpression());
			dictionary.Add(typeof(SelectStarExpression), () => new SelectStarExpression());
			dictionary.Add(typeof(IdentifierOrValueExpression), () => new IdentifierOrValueExpression());
			dictionary.Add(typeof(ColumnReferenceExpression), () => new ColumnReferenceExpression());
			dictionary.Add(typeof(QuerySpecification), () => new QuerySpecification());
			dictionary.Add(typeof(FromClause), () => new FromClause());
			dictionary.Add(typeof(QueryDerivedTable), () => new QueryDerivedTable());
			dictionary.Add(typeof(NamedTableReference), () => new NamedTableReference());
			dictionary.Add(typeof(BooleanParenthesisExpression), () => new BooleanParenthesisExpression());
			dictionary.Add(typeof(BooleanComparisonExpression), () => new BooleanComparisonExpression());
			dictionary.Add(typeof(JoinParenthesisTableReference), () => new JoinParenthesisTableReference());
			dictionary.Add(typeof(QualifiedJoin), () => new QualifiedJoin());
			dictionary.Add(typeof(FunctionCall), () => new FunctionCall());
			dictionary.Add(typeof(StringLiteral), () => new StringLiteral());
			dictionary.Add(typeof(SqlDataTypeReference), () => new SqlDataTypeReference());
			dictionary.Add(typeof(ExpressionGroupingSpecification), () => new ExpressionGroupingSpecification());
			dictionary.Add(typeof(CastCall), () => new CastCall());
			dictionary.Add(typeof(SelectStatement), () => new SelectStatement());
			dictionary.Add(typeof(GroupByClause), () => new GroupByClause());
			dictionary.Add(typeof(InPredicate), () => new InPredicate());
			dictionary.Add(typeof(TSqlBatch), () => new TSqlBatch());
			dictionary.Add(typeof(TopRowFilter), () => new TopRowFilter());
			dictionary.Add(typeof(BooleanBinaryExpression), () => new BooleanBinaryExpression());
			dictionary.Add(typeof(WhereClause), () => new WhereClause());
			dictionary.Add(typeof(IntegerLiteral), () => new IntegerLiteral());
			dictionary.Add(typeof(ParenthesisExpression), () => new ParenthesisExpression());
			dictionary.Add(typeof(UserDataTypeReference), () => new UserDataTypeReference());
			dictionary.Add(typeof(TSqlScript), () => new TSqlScript());
			TSqlFragmentFactory.ctors = dictionary;
		}

		// Token: 0x0400194C RID: 6476
		private IList<TSqlParserToken> _tokenStream;

		// Token: 0x0400194D RID: 6477
		private static readonly Dictionary<Type, Func<TSqlFragment>> ctors;
	}
}
