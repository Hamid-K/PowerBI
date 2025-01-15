using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001899 RID: 6297
	internal static class QueryExpressionMatcher
	{
		// Token: 0x06009FBC RID: 40892 RVA: 0x00210037 File Offset: 0x0020E237
		public static bool AreEqual(QueryExpression x, QueryExpression y)
		{
			return QueryExpressionMatcher.MatchesExpression(x, y, null);
		}

		// Token: 0x06009FBD RID: 40893 RVA: 0x00210044 File Offset: 0x0020E244
		public static bool Matches(QueryExpression pattern, QueryExpression expr, QueryExpression[] captures)
		{
			for (int i = 0; i < captures.Length; i++)
			{
				captures[i] = null;
			}
			return QueryExpressionMatcher.MatchesExpression(pattern, expr, captures);
		}

		// Token: 0x06009FBE RID: 40894 RVA: 0x0021006B File Offset: 0x0020E26B
		public static QueryExpression Capture(int position, Func<QueryExpression, bool> test = null)
		{
			return new QueryExpressionMatcher.CaptureQueryExpression(position, test);
		}

		// Token: 0x06009FBF RID: 40895 RVA: 0x00210074 File Offset: 0x0020E274
		public static QueryExpression CaptureConstant(int position, ValueKind kind, Func<Value, bool> test = null)
		{
			Value tmp;
			return new QueryExpressionMatcher.CaptureQueryExpression(position, (QueryExpression expr) => expr.TryGetConstant(out tmp) && tmp.Kind == kind && (test == null || test(tmp)));
		}

		// Token: 0x06009FC0 RID: 40896 RVA: 0x002100A7 File Offset: 0x0020E2A7
		public static QueryExpression OneOf(params QueryExpression[] expressions)
		{
			return new QueryExpressionMatcher.OneOfQueryExpression(expressions);
		}

		// Token: 0x06009FC1 RID: 40897 RVA: 0x002100B0 File Offset: 0x0020E2B0
		private static bool MatchesExpression(QueryExpression x, QueryExpression y, QueryExpression[] captures)
		{
			if (x.Kind != y.Kind && (captures == null || x.Kind < QueryExpressionKind.Count))
			{
				return false;
			}
			switch (x.Kind)
			{
			case QueryExpressionKind.Binary:
				return QueryExpressionMatcher.MatchBinary((BinaryQueryExpression)x, (BinaryQueryExpression)y, captures);
			case QueryExpressionKind.Constant:
				return QueryExpressionMatcher.MatchConstant((ConstantQueryExpression)x, (ConstantQueryExpression)y, captures);
			case QueryExpressionKind.ColumnAccess:
				return QueryExpressionMatcher.MatchColumnAccess((ColumnAccessQueryExpression)x, (ColumnAccessQueryExpression)y, captures);
			case QueryExpressionKind.If:
				return QueryExpressionMatcher.MatchIf((IfQueryExpression)x, (IfQueryExpression)y, captures);
			case QueryExpressionKind.Invocation:
				return QueryExpressionMatcher.MatchInvocation((InvocationQueryExpression)x, (InvocationQueryExpression)y, captures);
			case QueryExpressionKind.Unary:
				return QueryExpressionMatcher.MatchUnary((UnaryQueryExpression)x, (UnaryQueryExpression)y, captures);
			case QueryExpressionKind.ArgumentAccess:
				return true;
			case QueryExpressionKind.Count:
				return ((QueryExpressionMatcher.CaptureQueryExpression)x).Matches(y, captures);
			case (QueryExpressionKind)8:
				return ((QueryExpressionMatcher.OneOfQueryExpression)x).Matches(y, captures);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06009FC2 RID: 40898 RVA: 0x002101A4 File Offset: 0x0020E3A4
		private static bool MatchBinary(BinaryQueryExpression x, BinaryQueryExpression y, QueryExpression[] captures)
		{
			return x.Operator == y.Operator && QueryExpressionMatcher.MatchesExpression(x.Left, y.Left, captures) && QueryExpressionMatcher.MatchesExpression(x.Right, y.Right, captures);
		}

		// Token: 0x06009FC3 RID: 40899 RVA: 0x002101DC File Offset: 0x0020E3DC
		private static bool MatchColumnAccess(ColumnAccessQueryExpression x, ColumnAccessQueryExpression y, QueryExpression[] captures)
		{
			return x.Column == y.Column;
		}

		// Token: 0x06009FC4 RID: 40900 RVA: 0x002101EC File Offset: 0x0020E3EC
		private static bool MatchConstant(ConstantQueryExpression x, ConstantQueryExpression y, QueryExpression[] captures)
		{
			return x.Value.Equals(y.Value);
		}

		// Token: 0x06009FC5 RID: 40901 RVA: 0x002101FF File Offset: 0x0020E3FF
		private static bool MatchIf(IfQueryExpression x, IfQueryExpression y, QueryExpression[] captures)
		{
			return QueryExpressionMatcher.MatchesExpression(x.Condition, y.Condition, captures) && QueryExpressionMatcher.MatchesExpression(x.TrueCase, y.TrueCase, captures) && QueryExpressionMatcher.MatchesExpression(x.FalseCase, y.FalseCase, captures);
		}

		// Token: 0x06009FC6 RID: 40902 RVA: 0x00210240 File Offset: 0x0020E440
		private static bool MatchInvocation(InvocationQueryExpression x, InvocationQueryExpression y, QueryExpression[] captures)
		{
			bool flag = x.Arguments.Count == y.Arguments.Count && QueryExpressionMatcher.MatchesExpression(x.Function, y.Function, captures);
			int num = 0;
			while (flag && num < x.Arguments.Count)
			{
				flag = flag && QueryExpressionMatcher.MatchesExpression(x.Arguments[num], y.Arguments[num], captures);
				num++;
			}
			return flag;
		}

		// Token: 0x06009FC7 RID: 40903 RVA: 0x002102BA File Offset: 0x0020E4BA
		private static bool MatchUnary(UnaryQueryExpression x, UnaryQueryExpression y, QueryExpression[] captures)
		{
			return x.Operator == y.Operator && QueryExpressionMatcher.MatchesExpression(x.Expression, y.Expression, captures);
		}

		// Token: 0x040053BC RID: 21436
		private const QueryExpressionKind CaptureKind = QueryExpressionKind.Count;

		// Token: 0x040053BD RID: 21437
		private const QueryExpressionKind OneOfKind = (QueryExpressionKind)8;

		// Token: 0x0200189A RID: 6298
		private sealed class CaptureQueryExpression : QueryExpression
		{
			// Token: 0x06009FC8 RID: 40904 RVA: 0x002102DE File Offset: 0x0020E4DE
			public CaptureQueryExpression(int position, Func<QueryExpression, bool> test)
			{
				this.position = position;
				this.test = test;
			}

			// Token: 0x1700292C RID: 10540
			// (get) Token: 0x06009FC9 RID: 40905 RVA: 0x00002475 File Offset: 0x00000675
			public override QueryExpressionKind Kind
			{
				get
				{
					return QueryExpressionKind.Count;
				}
			}

			// Token: 0x06009FCA RID: 40906 RVA: 0x000033E7 File Offset: 0x000015E7
			public override void Analyze(Func<QueryExpression, bool> analyzer)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06009FCB RID: 40907 RVA: 0x000033E7 File Offset: 0x000015E7
			public override QueryExpression Rewrite(Func<QueryExpression, QueryExpression> rewrite)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06009FCC RID: 40908 RVA: 0x002102F4 File Offset: 0x0020E4F4
			public bool Matches(QueryExpression y, QueryExpression[] captures)
			{
				if (this.test == null || (this.test(y) && (captures[this.position] == null || QueryExpressionMatcher.AreEqual(captures[this.position], y))))
				{
					captures[this.position] = y;
					return true;
				}
				return false;
			}

			// Token: 0x040053BE RID: 21438
			private readonly int position;

			// Token: 0x040053BF RID: 21439
			private readonly Func<QueryExpression, bool> test;
		}

		// Token: 0x0200189B RID: 6299
		private sealed class OneOfQueryExpression : QueryExpression
		{
			// Token: 0x06009FCD RID: 40909 RVA: 0x00210332 File Offset: 0x0020E532
			public OneOfQueryExpression(QueryExpression[] expressions)
			{
				this.expressions = expressions;
			}

			// Token: 0x1700292D RID: 10541
			// (get) Token: 0x06009FCE RID: 40910 RVA: 0x000024ED File Offset: 0x000006ED
			public override QueryExpressionKind Kind
			{
				get
				{
					return (QueryExpressionKind)8;
				}
			}

			// Token: 0x06009FCF RID: 40911 RVA: 0x000033E7 File Offset: 0x000015E7
			public override void Analyze(Func<QueryExpression, bool> analyzer)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06009FD0 RID: 40912 RVA: 0x000033E7 File Offset: 0x000015E7
			public override QueryExpression Rewrite(Func<QueryExpression, QueryExpression> rewrite)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06009FD1 RID: 40913 RVA: 0x00210344 File Offset: 0x0020E544
			public bool Matches(QueryExpression y, QueryExpression[] captures)
			{
				BitArray nulls = QueryExpressionMatcher.OneOfQueryExpression.GetNulls(captures);
				for (int i = 0; i < this.expressions.Length; i++)
				{
					if (QueryExpressionMatcher.MatchesExpression(this.expressions[i], y, captures))
					{
						return true;
					}
					QueryExpressionMatcher.OneOfQueryExpression.SetNulls(nulls, captures);
				}
				return false;
			}

			// Token: 0x06009FD2 RID: 40914 RVA: 0x00210388 File Offset: 0x0020E588
			private static BitArray GetNulls(QueryExpression[] captures)
			{
				BitArray bitArray = new BitArray(captures.Length);
				for (int i = 0; i < captures.Length; i++)
				{
					if (captures[i] == null)
					{
						bitArray[i] = true;
					}
				}
				return bitArray;
			}

			// Token: 0x06009FD3 RID: 40915 RVA: 0x002103BC File Offset: 0x0020E5BC
			private static void SetNulls(BitArray nulls, QueryExpression[] captures)
			{
				for (int i = 0; i < captures.Length; i++)
				{
					if (nulls[i])
					{
						captures[i] = null;
					}
				}
			}

			// Token: 0x040053C0 RID: 21440
			private readonly QueryExpression[] expressions;
		}
	}
}
