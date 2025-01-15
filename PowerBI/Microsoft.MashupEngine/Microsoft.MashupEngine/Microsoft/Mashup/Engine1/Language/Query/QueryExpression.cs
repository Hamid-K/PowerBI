using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001883 RID: 6275
	internal abstract class QueryExpression
	{
		// Token: 0x06009F2F RID: 40751 RVA: 0x0020E5F8 File Offset: 0x0020C7F8
		public static QueryExpression FromListOfColumnAccesses(ColumnAccessQueryExpression[] columnAccesses)
		{
			Value[] array = new Value[columnAccesses.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = NumberValue.New(columnAccesses[i].Column);
			}
			return new InvocationQueryExpression(new ConstantQueryExpression(Library.Record.SelectFieldsByIndex), new QueryExpression[]
			{
				ArgumentAccessQueryExpression.Instance,
				new ConstantQueryExpression(ListValue.New(array))
			});
		}

		// Token: 0x17002918 RID: 10520
		// (get) Token: 0x06009F30 RID: 40752
		public abstract QueryExpressionKind Kind { get; }

		// Token: 0x06009F31 RID: 40753
		public abstract void Analyze(Func<QueryExpression, bool> analyzer);

		// Token: 0x06009F32 RID: 40754
		public abstract QueryExpression Rewrite(Func<QueryExpression, QueryExpression> rewrite);

		// Token: 0x06009F33 RID: 40755 RVA: 0x000E6755 File Offset: 0x000E4955
		public virtual bool TryGetConstant(out Value value)
		{
			value = null;
			return false;
		}

		// Token: 0x06009F34 RID: 40756 RVA: 0x0020E657 File Offset: 0x0020C857
		public virtual bool TryGetColumnAccess(out int column)
		{
			column = -1;
			return false;
		}

		// Token: 0x06009F35 RID: 40757 RVA: 0x000912D6 File Offset: 0x0008F4D6
		public virtual bool TryGetInvocation(object functionIdentity, int argumentCount, out IList<QueryExpression> arguments)
		{
			arguments = null;
			return false;
		}

		// Token: 0x06009F36 RID: 40758 RVA: 0x000E6755 File Offset: 0x000E4955
		public virtual bool TryGetListOfColumnAccesses(out ColumnAccessQueryExpression[] columnAccesses)
		{
			columnAccesses = null;
			return false;
		}

		// Token: 0x06009F37 RID: 40759 RVA: 0x0020E660 File Offset: 0x0020C860
		public bool TryGetOptionalColumnAccess(out string columnName)
		{
			IList<QueryExpression> list;
			Value value;
			if ((this.TryGetInvocation(Library.Record.FieldOrDefault, 2, out list) || this.TryGetInvocation(Library.Collection.FieldOrNull, 2, out list)) && list[0].Kind == QueryExpressionKind.ArgumentAccess && list[1].TryGetConstant(out value) && value.IsText)
			{
				columnName = value.AsString;
				return true;
			}
			columnName = null;
			return false;
		}

		// Token: 0x06009F38 RID: 40760 RVA: 0x0020E6C4 File Offset: 0x0020C8C4
		public bool AllAccess(Func<InvocationQueryExpression, bool> rowAccess, Func<int, bool> columnAccess)
		{
			bool flag = true;
			this.Analyze(delegate(QueryExpression node)
			{
				switch (node.Kind)
				{
				case QueryExpressionKind.ColumnAccess:
					flag &= columnAccess(((ColumnAccessQueryExpression)node).Column);
					break;
				case QueryExpressionKind.Invocation:
				{
					InvocationQueryExpression invocationQueryExpression = (InvocationQueryExpression)node;
					if (invocationQueryExpression.Function.Kind == QueryExpressionKind.Constant)
					{
						ColumnAccessQueryExpression[] array;
						if (node.TryGetListOfColumnAccesses(out array))
						{
							int num = 0;
							while (flag)
							{
								if (num >= array.Length)
								{
									break;
								}
								flag &= columnAccess(array[num].Column);
								num++;
							}
						}
						else
						{
							bool flag2 = false;
							int num2 = 0;
							while (flag && num2 < invocationQueryExpression.Arguments.Count)
							{
								if (invocationQueryExpression.Arguments[num2].Kind == QueryExpressionKind.ArgumentAccess)
								{
									flag2 = true;
								}
								else
								{
									flag &= invocationQueryExpression.Arguments[num2].AllAccess(rowAccess, columnAccess);
								}
								num2++;
							}
							if (flag && flag2)
							{
								flag = rowAccess(invocationQueryExpression);
							}
						}
						return false;
					}
					break;
				}
				case QueryExpressionKind.ArgumentAccess:
					flag &= rowAccess(null);
					break;
				}
				return flag;
			});
			return flag;
		}

		// Token: 0x06009F39 RID: 40761 RVA: 0x0020E704 File Offset: 0x0020C904
		public QueryExpression AdjustColumnAccess(ColumnSelection columnSelection)
		{
			return this.AdjustColumnAccess((int column) => columnSelection.GetColumn(column));
		}

		// Token: 0x06009F3A RID: 40762 RVA: 0x0020E730 File Offset: 0x0020C930
		public QueryExpression AdjustColumnAccess(Func<int, int> adjustor)
		{
			return this.Rewrite(delegate(QueryExpression node)
			{
				QueryExpressionKind kind = node.Kind;
				if (kind != QueryExpressionKind.ColumnAccess)
				{
					if (kind == QueryExpressionKind.Invocation)
					{
						ColumnAccessQueryExpression[] array;
						if (node.TryGetListOfColumnAccesses(out array))
						{
							for (int i = 0; i < array.Length; i++)
							{
								array[i] = new ColumnAccessQueryExpression(adjustor(array[i].Column));
							}
							return QueryExpression.FromListOfColumnAccesses(array);
						}
					}
					return node;
				}
				ColumnAccessQueryExpression columnAccessQueryExpression = (ColumnAccessQueryExpression)node;
				int num = adjustor(columnAccessQueryExpression.Column);
				if (num != columnAccessQueryExpression.Column)
				{
					columnAccessQueryExpression = new ColumnAccessQueryExpression(num);
				}
				return columnAccessQueryExpression;
			});
		}
	}
}
