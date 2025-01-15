using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200188C RID: 6284
	internal class InvocationQueryExpression : QueryExpression
	{
		// Token: 0x06009F6B RID: 40811 RVA: 0x0020EBC9 File Offset: 0x0020CDC9
		public InvocationQueryExpression(QueryExpression function, IList<QueryExpression> arguments)
		{
			this.function = function;
			this.arguments = arguments;
		}

		// Token: 0x06009F6C RID: 40812 RVA: 0x0020EBDF File Offset: 0x0020CDDF
		public InvocationQueryExpression(QueryExpression function, params QueryExpression[] arguments)
			: this(function, arguments)
		{
		}

		// Token: 0x17002926 RID: 10534
		// (get) Token: 0x06009F6D RID: 40813 RVA: 0x0000244F File Offset: 0x0000064F
		public override QueryExpressionKind Kind
		{
			get
			{
				return QueryExpressionKind.Invocation;
			}
		}

		// Token: 0x17002927 RID: 10535
		// (get) Token: 0x06009F6E RID: 40814 RVA: 0x0020EBE9 File Offset: 0x0020CDE9
		public QueryExpression Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x17002928 RID: 10536
		// (get) Token: 0x06009F6F RID: 40815 RVA: 0x0020EBF1 File Offset: 0x0020CDF1
		public IList<QueryExpression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x06009F70 RID: 40816 RVA: 0x0020EBFC File Offset: 0x0020CDFC
		public override bool TryGetInvocation(object functionIdentity, int argumentCount, out IList<QueryExpression> arguments)
		{
			Value value;
			if (this.arguments.Count == argumentCount && this.function.TryGetConstant(out value) && value.IsFunction && value.AsFunction.FunctionIdentity.Equals(functionIdentity))
			{
				arguments = this.arguments;
				return true;
			}
			arguments = null;
			return false;
		}

		// Token: 0x06009F71 RID: 40817 RVA: 0x0020EC50 File Offset: 0x0020CE50
		public override bool TryGetListOfColumnAccesses(out ColumnAccessQueryExpression[] columnAccesses)
		{
			IList<QueryExpression> list;
			Value value;
			if (this.TryGetInvocation(Library.Record.SelectFieldsByIndex, 2, out list) && list[0].Kind == QueryExpressionKind.ArgumentAccess && list[1].TryGetConstant(out value) && value.IsList)
			{
				ListValue asList = value.AsList;
				columnAccesses = new ColumnAccessQueryExpression[asList.Count];
				for (int i = 0; i < columnAccesses.Length; i++)
				{
					columnAccesses[i] = new ColumnAccessQueryExpression(asList[i].AsInteger32);
				}
				return true;
			}
			columnAccesses = null;
			return false;
		}

		// Token: 0x06009F72 RID: 40818 RVA: 0x0020ECD4 File Offset: 0x0020CED4
		public override void Analyze(Func<QueryExpression, bool> analyzer)
		{
			if (analyzer(this))
			{
				this.function.Analyze(analyzer);
				for (int i = 0; i < this.arguments.Count; i++)
				{
					this.arguments[i].Analyze(analyzer);
				}
			}
		}

		// Token: 0x06009F73 RID: 40819 RVA: 0x0020ED20 File Offset: 0x0020CF20
		public override QueryExpression Rewrite(Func<QueryExpression, QueryExpression> rewrite)
		{
			QueryExpression queryExpression = this.function.Rewrite(rewrite);
			QueryExpression[] array = null;
			for (int i = 0; i < this.arguments.Count; i++)
			{
				QueryExpression queryExpression2 = this.arguments[i].Rewrite(rewrite);
				if (queryExpression2 != this.arguments[i] && array == null)
				{
					array = new QueryExpression[this.arguments.Count];
					for (int j = 0; j < i; j++)
					{
						array[j] = this.arguments[j];
					}
				}
				if (array != null)
				{
					array[i] = queryExpression2;
				}
			}
			QueryExpression queryExpression3 = this;
			if (queryExpression != this.function || array != null)
			{
				QueryExpression queryExpression4 = queryExpression;
				IList<QueryExpression> list = array;
				queryExpression3 = new InvocationQueryExpression(queryExpression4, list ?? this.arguments);
			}
			return rewrite(queryExpression3);
		}

		// Token: 0x040053A5 RID: 21413
		private QueryExpression function;

		// Token: 0x040053A6 RID: 21414
		private IList<QueryExpression> arguments;
	}
}
