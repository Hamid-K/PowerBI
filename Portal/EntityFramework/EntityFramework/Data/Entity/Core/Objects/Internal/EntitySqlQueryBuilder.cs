using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Text;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200043D RID: 1085
	internal static class EntitySqlQueryBuilder
	{
		// Token: 0x060034EE RID: 13550 RVA: 0x000AA2F4 File Offset: 0x000A84F4
		private static string GetCommandText(ObjectQueryState query)
		{
			string text = null;
			if (!query.TryGetCommandText(out text))
			{
				throw new NotSupportedException(Strings.ObjectQuery_QueryBuilder_NotSupportedLinqSource);
			}
			return text;
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x000AA31C File Offset: 0x000A851C
		private static ObjectParameterCollection MergeParameters(ObjectContext context, ObjectParameterCollection sourceQueryParams, ObjectParameter[] builderMethodParams)
		{
			if (sourceQueryParams == null && builderMethodParams.Length == 0)
			{
				return null;
			}
			ObjectParameterCollection objectParameterCollection = ObjectParameterCollection.DeepCopy(sourceQueryParams);
			if (objectParameterCollection == null)
			{
				objectParameterCollection = new ObjectParameterCollection(context.Perspective);
			}
			foreach (ObjectParameter objectParameter in builderMethodParams)
			{
				objectParameterCollection.Add(objectParameter);
			}
			return objectParameterCollection;
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x000AA364 File Offset: 0x000A8564
		private static ObjectParameterCollection MergeParameters(ObjectParameterCollection query1Params, ObjectParameterCollection query2Params)
		{
			if (query1Params == null && query2Params == null)
			{
				return null;
			}
			ObjectParameterCollection objectParameterCollection;
			ObjectParameterCollection objectParameterCollection2;
			if (query1Params != null)
			{
				objectParameterCollection = ObjectParameterCollection.DeepCopy(query1Params);
				objectParameterCollection2 = query2Params;
			}
			else
			{
				objectParameterCollection = ObjectParameterCollection.DeepCopy(query2Params);
				objectParameterCollection2 = query1Params;
			}
			if (objectParameterCollection2 != null)
			{
				foreach (ObjectParameter objectParameter in objectParameterCollection2)
				{
					objectParameterCollection.Add(objectParameter.ShallowCopy());
				}
			}
			return objectParameterCollection;
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000AA3D4 File Offset: 0x000A85D4
		private static ObjectQueryState NewBuilderQuery(ObjectQueryState sourceQuery, Type elementType, StringBuilder queryText, Span newSpan, IEnumerable<ObjectParameter> enumerableParams)
		{
			return EntitySqlQueryBuilder.NewBuilderQuery(sourceQuery, elementType, queryText, false, newSpan, enumerableParams);
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x000AA3E4 File Offset: 0x000A85E4
		private static ObjectQueryState NewBuilderQuery(ObjectQueryState sourceQuery, Type elementType, StringBuilder queryText, bool allowsLimit, Span newSpan, IEnumerable<ObjectParameter> enumerableParams)
		{
			ObjectParameterCollection objectParameterCollection = enumerableParams as ObjectParameterCollection;
			if (objectParameterCollection == null && enumerableParams != null)
			{
				objectParameterCollection = new ObjectParameterCollection(sourceQuery.ObjectContext.Perspective);
				foreach (ObjectParameter objectParameter in enumerableParams)
				{
					objectParameterCollection.Add(objectParameter);
				}
			}
			EntitySqlQueryState entitySqlQueryState = new EntitySqlQueryState(elementType, queryText.ToString(), allowsLimit, sourceQuery.ObjectContext, objectParameterCollection, newSpan);
			sourceQuery.ApplySettingsTo(entitySqlQueryState);
			return entitySqlQueryState;
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x000AA46C File Offset: 0x000A866C
		private static ObjectQueryState BuildSetOp(ObjectQueryState leftQuery, ObjectQueryState rightQuery, Span newSpan, string setOp)
		{
			string commandText = EntitySqlQueryBuilder.GetCommandText(leftQuery);
			string commandText2 = EntitySqlQueryBuilder.GetCommandText(rightQuery);
			if (leftQuery.ObjectContext != rightQuery.ObjectContext)
			{
				throw new ArgumentException(Strings.ObjectQuery_QueryBuilder_InvalidQueryArgument, "query");
			}
			StringBuilder stringBuilder = new StringBuilder("(\r\n".Length + commandText.Length + setOp.Length + commandText2.Length + "\r\n)".Length);
			stringBuilder.Append("(\r\n");
			stringBuilder.Append(commandText);
			stringBuilder.Append(setOp);
			stringBuilder.Append(commandText2);
			stringBuilder.Append("\r\n)");
			return EntitySqlQueryBuilder.NewBuilderQuery(leftQuery, leftQuery.ElementType, stringBuilder, newSpan, EntitySqlQueryBuilder.MergeParameters(leftQuery.Parameters, rightQuery.Parameters));
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x000AA524 File Offset: 0x000A8724
		private static ObjectQueryState BuildSelectOrSelectValue(ObjectQueryState query, string alias, string projection, ObjectParameter[] parameters, string projectOp, Type elementType)
		{
			string commandText = EntitySqlQueryBuilder.GetCommandText(query);
			StringBuilder stringBuilder = new StringBuilder(projectOp.Length + projection.Length + "\r\nFROM (\r\n".Length + commandText.Length + "\r\n) AS ".Length + alias.Length);
			stringBuilder.Append(projectOp);
			stringBuilder.Append(projection);
			stringBuilder.Append("\r\nFROM (\r\n");
			stringBuilder.Append(commandText);
			stringBuilder.Append("\r\n) AS ");
			stringBuilder.Append(alias);
			return EntitySqlQueryBuilder.NewBuilderQuery(query, elementType, stringBuilder, null, EntitySqlQueryBuilder.MergeParameters(query.ObjectContext, query.Parameters, parameters));
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000AA5C8 File Offset: 0x000A87C8
		private static ObjectQueryState BuildOrderByOrWhere(ObjectQueryState query, string alias, string predicateOrKeys, ObjectParameter[] parameters, string op, string skipCount, bool allowsLimit)
		{
			string commandText = EntitySqlQueryBuilder.GetCommandText(query);
			int num = "SELECT VALUE ".Length + alias.Length + "\r\nFROM (\r\n".Length + commandText.Length + "\r\n) AS ".Length + alias.Length + op.Length + predicateOrKeys.Length;
			if (skipCount != null)
			{
				num += "\r\nSKIP\r\n".Length + skipCount.Length;
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			stringBuilder.Append("SELECT VALUE ");
			stringBuilder.Append(alias);
			stringBuilder.Append("\r\nFROM (\r\n");
			stringBuilder.Append(commandText);
			stringBuilder.Append("\r\n) AS ");
			stringBuilder.Append(alias);
			stringBuilder.Append(op);
			stringBuilder.Append(predicateOrKeys);
			if (skipCount != null)
			{
				stringBuilder.Append("\r\nSKIP\r\n");
				stringBuilder.Append(skipCount);
			}
			return EntitySqlQueryBuilder.NewBuilderQuery(query, query.ElementType, stringBuilder, allowsLimit, query.Span, EntitySqlQueryBuilder.MergeParameters(query.ObjectContext, query.Parameters, parameters));
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000AA6D0 File Offset: 0x000A88D0
		internal static ObjectQueryState Distinct(ObjectQueryState query)
		{
			string commandText = EntitySqlQueryBuilder.GetCommandText(query);
			StringBuilder stringBuilder = new StringBuilder("SET(\r\n".Length + commandText.Length + "\r\n)".Length);
			stringBuilder.Append("SET(\r\n");
			stringBuilder.Append(commandText);
			stringBuilder.Append("\r\n)");
			return EntitySqlQueryBuilder.NewBuilderQuery(query, query.ElementType, stringBuilder, query.Span, ObjectParameterCollection.DeepCopy(query.Parameters));
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x000AA744 File Offset: 0x000A8944
		internal static ObjectQueryState Except(ObjectQueryState leftQuery, ObjectQueryState rightQuery)
		{
			return EntitySqlQueryBuilder.BuildSetOp(leftQuery, rightQuery, leftQuery.Span, "\r\n) EXCEPT (\r\n");
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x000AA758 File Offset: 0x000A8958
		internal static ObjectQueryState GroupBy(ObjectQueryState query, string alias, string keys, string projection, ObjectParameter[] parameters)
		{
			string commandText = EntitySqlQueryBuilder.GetCommandText(query);
			StringBuilder stringBuilder = new StringBuilder("SELECT ".Length + projection.Length + "\r\nFROM (\r\n".Length + commandText.Length + "\r\n) AS ".Length + alias.Length + "\r\nGROUP BY\r\n".Length + keys.Length);
			stringBuilder.Append("SELECT ");
			stringBuilder.Append(projection);
			stringBuilder.Append("\r\nFROM (\r\n");
			stringBuilder.Append(commandText);
			stringBuilder.Append("\r\n) AS ");
			stringBuilder.Append(alias);
			stringBuilder.Append("\r\nGROUP BY\r\n");
			stringBuilder.Append(keys);
			return EntitySqlQueryBuilder.NewBuilderQuery(query, typeof(DbDataRecord), stringBuilder, null, EntitySqlQueryBuilder.MergeParameters(query.ObjectContext, query.Parameters, parameters));
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000AA830 File Offset: 0x000A8A30
		internal static ObjectQueryState Intersect(ObjectQueryState leftQuery, ObjectQueryState rightQuery)
		{
			Span span = Span.CopyUnion(leftQuery.Span, rightQuery.Span);
			return EntitySqlQueryBuilder.BuildSetOp(leftQuery, rightQuery, span, "\r\n) INTERSECT (\r\n");
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x000AA85C File Offset: 0x000A8A5C
		internal static ObjectQueryState OfType(ObjectQueryState query, EdmType newType, Type clrOfType)
		{
			string commandText = EntitySqlQueryBuilder.GetCommandText(query);
			StringBuilder stringBuilder = new StringBuilder("OFTYPE(\r\n(\r\n".Length + commandText.Length + "\r\n),\r\n[".Length + newType.NamespaceName.Length + ((!string.IsNullOrEmpty(newType.NamespaceName)) ? "].[".Length : 0) + newType.Name.Length + "]\r\n)".Length);
			stringBuilder.Append("OFTYPE(\r\n(\r\n");
			stringBuilder.Append(commandText);
			stringBuilder.Append("\r\n),\r\n[");
			if (!string.IsNullOrEmpty(newType.NamespaceName))
			{
				stringBuilder.Append(newType.NamespaceName);
				stringBuilder.Append("].[");
			}
			stringBuilder.Append(newType.Name);
			stringBuilder.Append("]\r\n)");
			return EntitySqlQueryBuilder.NewBuilderQuery(query, clrOfType, stringBuilder, query.Span, ObjectParameterCollection.DeepCopy(query.Parameters));
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x000AA948 File Offset: 0x000A8B48
		internal static ObjectQueryState OrderBy(ObjectQueryState query, string alias, string keys, ObjectParameter[] parameters)
		{
			return EntitySqlQueryBuilder.BuildOrderByOrWhere(query, alias, keys, parameters, "\r\nORDER BY\r\n", null, true);
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x000AA95A File Offset: 0x000A8B5A
		internal static ObjectQueryState Select(ObjectQueryState query, string alias, string projection, ObjectParameter[] parameters)
		{
			return EntitySqlQueryBuilder.BuildSelectOrSelectValue(query, alias, projection, parameters, "SELECT ", typeof(DbDataRecord));
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x000AA974 File Offset: 0x000A8B74
		internal static ObjectQueryState SelectValue(ObjectQueryState query, string alias, string projection, ObjectParameter[] parameters, Type projectedType)
		{
			return EntitySqlQueryBuilder.BuildSelectOrSelectValue(query, alias, projection, parameters, "SELECT VALUE ", projectedType);
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000AA986 File Offset: 0x000A8B86
		internal static ObjectQueryState Skip(ObjectQueryState query, string alias, string keys, string count, ObjectParameter[] parameters)
		{
			return EntitySqlQueryBuilder.BuildOrderByOrWhere(query, alias, keys, parameters, "\r\nORDER BY\r\n", count, true);
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000AA99C File Offset: 0x000A8B9C
		internal static ObjectQueryState Top(ObjectQueryState query, string alias, string count, ObjectParameter[] parameters)
		{
			int num = count.Length;
			string commandText = EntitySqlQueryBuilder.GetCommandText(query);
			bool allowsLimitSubclause = ((EntitySqlQueryState)query).AllowsLimitSubclause;
			if (allowsLimitSubclause)
			{
				num += commandText.Length + "\r\nLIMIT\r\n".Length;
			}
			else
			{
				num += "SELECT VALUE TOP(\r\n".Length + "\r\n) ".Length + alias.Length + "\r\nFROM (\r\n".Length + commandText.Length + "\r\n) AS ".Length + alias.Length;
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			if (allowsLimitSubclause)
			{
				stringBuilder.Append(commandText);
				stringBuilder.Append("\r\nLIMIT\r\n");
				stringBuilder.Append(count);
			}
			else
			{
				stringBuilder.Append("SELECT VALUE TOP(\r\n");
				stringBuilder.Append(count);
				stringBuilder.Append("\r\n) ");
				stringBuilder.Append(alias);
				stringBuilder.Append("\r\nFROM (\r\n");
				stringBuilder.Append(commandText);
				stringBuilder.Append("\r\n) AS ");
				stringBuilder.Append(alias);
			}
			return EntitySqlQueryBuilder.NewBuilderQuery(query, query.ElementType, stringBuilder, query.Span, EntitySqlQueryBuilder.MergeParameters(query.ObjectContext, query.Parameters, parameters));
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000AAABC File Offset: 0x000A8CBC
		internal static ObjectQueryState Union(ObjectQueryState leftQuery, ObjectQueryState rightQuery)
		{
			Span span = Span.CopyUnion(leftQuery.Span, rightQuery.Span);
			return EntitySqlQueryBuilder.BuildSetOp(leftQuery, rightQuery, span, "\r\n) UNION (\r\n");
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000AAAE8 File Offset: 0x000A8CE8
		internal static ObjectQueryState UnionAll(ObjectQueryState leftQuery, ObjectQueryState rightQuery)
		{
			Span span = Span.CopyUnion(leftQuery.Span, rightQuery.Span);
			return EntitySqlQueryBuilder.BuildSetOp(leftQuery, rightQuery, span, "\r\n) UNION ALL (\r\n");
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000AAB14 File Offset: 0x000A8D14
		internal static ObjectQueryState Where(ObjectQueryState query, string alias, string predicate, ObjectParameter[] parameters)
		{
			return EntitySqlQueryBuilder.BuildOrderByOrWhere(query, alias, predicate, parameters, "\r\nWHERE\r\n", null, false);
		}

		// Token: 0x04001117 RID: 4375
		private const string _setOpEpilog = "\r\n)";

		// Token: 0x04001118 RID: 4376
		private const string _setOpProlog = "(\r\n";

		// Token: 0x04001119 RID: 4377
		private const string _fromOp = "\r\nFROM (\r\n";

		// Token: 0x0400111A RID: 4378
		private const string _asOp = "\r\n) AS ";

		// Token: 0x0400111B RID: 4379
		private const string _distinctProlog = "SET(\r\n";

		// Token: 0x0400111C RID: 4380
		private const string _distinctEpilog = "\r\n)";

		// Token: 0x0400111D RID: 4381
		private const string _exceptOp = "\r\n) EXCEPT (\r\n";

		// Token: 0x0400111E RID: 4382
		private const string _groupByOp = "\r\nGROUP BY\r\n";

		// Token: 0x0400111F RID: 4383
		private const string _intersectOp = "\r\n) INTERSECT (\r\n";

		// Token: 0x04001120 RID: 4384
		private const string _ofTypeProlog = "OFTYPE(\r\n(\r\n";

		// Token: 0x04001121 RID: 4385
		private const string _ofTypeInfix = "\r\n),\r\n[";

		// Token: 0x04001122 RID: 4386
		private const string _ofTypeInfix2 = "].[";

		// Token: 0x04001123 RID: 4387
		private const string _ofTypeEpilog = "]\r\n)";

		// Token: 0x04001124 RID: 4388
		private const string _orderByOp = "\r\nORDER BY\r\n";

		// Token: 0x04001125 RID: 4389
		private const string _selectOp = "SELECT ";

		// Token: 0x04001126 RID: 4390
		private const string _selectValueOp = "SELECT VALUE ";

		// Token: 0x04001127 RID: 4391
		private const string _skipOp = "\r\nSKIP\r\n";

		// Token: 0x04001128 RID: 4392
		private const string _limitOp = "\r\nLIMIT\r\n";

		// Token: 0x04001129 RID: 4393
		private const string _topOp = "SELECT VALUE TOP(\r\n";

		// Token: 0x0400112A RID: 4394
		private const string _topInfix = "\r\n) ";

		// Token: 0x0400112B RID: 4395
		private const string _unionOp = "\r\n) UNION (\r\n";

		// Token: 0x0400112C RID: 4396
		private const string _unionAllOp = "\r\n) UNION ALL (\r\n";

		// Token: 0x0400112D RID: 4397
		private const string _whereOp = "\r\nWHERE\r\n";
	}
}
