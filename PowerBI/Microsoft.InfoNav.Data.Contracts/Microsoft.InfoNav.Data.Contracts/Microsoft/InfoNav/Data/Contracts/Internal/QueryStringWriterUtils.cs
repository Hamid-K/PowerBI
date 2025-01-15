using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002D7 RID: 727
	internal static class QueryStringWriterUtils
	{
		// Token: 0x0600184F RID: 6223 RVA: 0x0002B66C File Offset: 0x0002986C
		internal static void WriteParameters(List<QueryExpressionContainer> parameterDeclarations, QueryStringWriter w)
		{
			QueryStringWriterUtils.WriteNamedExpressionClause("parameters", parameterDeclarations, w, new Func<QueryExpressionContainer, bool>(QueryDefinitionValidator.IsParameterDeclarationValid));
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0002B686 File Offset: 0x00029886
		internal static void WriteLet(List<QueryExpressionContainer> letBindings, QueryStringWriter w)
		{
			QueryStringWriterUtils.WriteNamedExpressionClause("let", letBindings, w, new Func<QueryExpressionContainer, bool>(QueryDefinitionValidator.IsLetBindingValid));
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0002B6A0 File Offset: 0x000298A0
		private static void WriteNamedExpressionClause(string clause, List<QueryExpressionContainer> items, QueryStringWriter w, Func<QueryExpressionContainer, bool> isValid)
		{
			if (items.IsNullOrEmpty<QueryExpressionContainer>())
			{
				return;
			}
			w.WriteSeparator();
			using (w.NewClauseScope(clause, QueryStringWriter.Separator.CommaAndNewline))
			{
				foreach (QueryExpressionContainer queryExpressionContainer in items)
				{
					w.WriteSeparator();
					if (queryExpressionContainer == null || !isValid(queryExpressionContainer))
					{
						w.WriteError();
					}
					else
					{
						w.WriteExpressionAndName(queryExpressionContainer);
					}
				}
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0002B73C File Offset: 0x0002993C
		internal static void WriteFrom(List<EntitySource> from, QueryStringWriter w)
		{
			if (from == null || from.Count == 0)
			{
				return;
			}
			w.WriteSeparator();
			using (w.NewClauseScope("from", QueryStringWriter.Separator.CommaAndNewline))
			{
				foreach (EntitySource entitySource in from)
				{
					w.WriteSeparator();
					if (entitySource == null || !QueryDefinitionValidator.IsValid(entitySource))
					{
						w.WriteError();
					}
					else
					{
						entitySource.WriteQueryString(w);
					}
				}
			}
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0002B7E0 File Offset: 0x000299E0
		internal static void WriteWhere(List<QueryFilter> where, QueryStringWriter w, string[] filterRestatements)
		{
			if (where == null || where.Count == 0)
			{
				return;
			}
			w.WriteSeparator();
			using (w.NewClauseScope("where", QueryStringWriter.Separator.CommaAndNewline))
			{
				for (int i = 0; i < where.Count; i++)
				{
					QueryFilter queryFilter = where[i];
					w.WriteSeparator();
					if (queryFilter == null || !QueryDefinitionValidator.IsValid(queryFilter))
					{
						w.WriteError();
					}
					else
					{
						string text = ((filterRestatements != null && filterRestatements.Length > i) ? filterRestatements[i] : null);
						queryFilter.WriteQueryString(w, text);
					}
				}
			}
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0002B878 File Offset: 0x00029A78
		internal static void WriteVisualShape(List<QueryAxis> visualShape, QueryStringWriter w)
		{
			if (visualShape.IsNullOrEmpty<QueryAxis>())
			{
				return;
			}
			w.WriteSeparator();
			using (w.NewClauseScope("with visualshape", QueryStringWriter.Separator.CommaAndNewline))
			{
				w.WriteLine();
				foreach (QueryAxis queryAxis in visualShape)
				{
					if (queryAxis == null || !QueryDefinitionValidator.IsValid(queryAxis))
					{
						w.WriteError();
					}
					else
					{
						w.WriteSeparator();
						queryAxis.WriteQueryString(w);
					}
				}
			}
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0002B918 File Offset: 0x00029B18
		internal static void WriteFunction<TArg>(string functionName, IReadOnlyList<TArg> args, QueryStringWriter.Separator argumentSeparator, Action<TArg, QueryStringWriter> writeArg, QueryStringWriter w)
		{
			QueryStringWriterUtils.<>c__DisplayClass6_0<TArg> CS$<>8__locals1;
			CS$<>8__locals1.argumentSeparator = argumentSeparator;
			CS$<>8__locals1.w = w;
			CS$<>8__locals1.w.Write(functionName);
			CS$<>8__locals1.w.Write("(");
			if (args.Count > 0)
			{
				using (CS$<>8__locals1.w.NewIndentScope())
				{
					QueryStringWriterUtils.<WriteFunction>g__InsertLineIfNeeded|6_0<TArg>(ref CS$<>8__locals1);
					using (CS$<>8__locals1.w.NewSeparatorScope(CS$<>8__locals1.argumentSeparator))
					{
						foreach (TArg targ in args)
						{
							CS$<>8__locals1.w.WriteSeparator();
							writeArg(targ, CS$<>8__locals1.w);
						}
					}
				}
				QueryStringWriterUtils.<WriteFunction>g__InsertLineIfNeeded|6_0<TArg>(ref CS$<>8__locals1);
			}
			CS$<>8__locals1.w.Write(")");
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0002BA14 File Offset: 0x00029C14
		internal static void WriteName(string name, QueryStringWriter w)
		{
			if (string.IsNullOrEmpty(name))
			{
				return;
			}
			w.Write(" as ");
			w.WriteIdentifierCustomerContent(name);
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0002BA31 File Offset: 0x00029C31
		[CompilerGenerated]
		internal static void <WriteFunction>g__InsertLineIfNeeded|6_0<TArg>(ref QueryStringWriterUtils.<>c__DisplayClass6_0<TArg> A_0)
		{
			if (A_0.argumentSeparator == QueryStringWriter.Separator.Newline || A_0.argumentSeparator == QueryStringWriter.Separator.CommaAndNewline)
			{
				A_0.w.WriteLine();
			}
		}
	}
}
