using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ResolvedSemanticQuery;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000055 RID: 85
	internal static class DsqGenerationUtils
	{
		// Token: 0x060003C6 RID: 966 RVA: 0x0000CF40 File Offset: 0x0000B140
		internal static bool HasSubtotals(this DataShapeBindingAxis axis)
		{
			if (axis == null || axis.Groupings == null)
			{
				return false;
			}
			return axis.Groupings.Any(delegate(DataShapeBindingAxisGrouping g)
			{
				SubtotalType? subtotal = g.Subtotal;
				SubtotalType subtotalType = SubtotalType.None;
				return !((subtotal.GetValueOrDefault() == subtotalType) & (subtotal != null));
			});
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000CF80 File Offset: 0x0000B180
		internal static bool HasSuppressedProjections(this DataShapeBindingAxis axis)
		{
			if (axis == null || axis.Groupings.IsNullOrEmptyCollection<DataShapeBindingAxisGrouping>())
			{
				return false;
			}
			return axis.Groupings.Any((DataShapeBindingAxisGrouping g) => !g.SuppressedProjections.IsNullOrEmpty<int>());
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000CFCF File Offset: 0x0000B1CF
		internal static bool HasInstanceFilters(this DataShapeBindingAxis axis)
		{
			if (axis == null || axis.Groupings == null)
			{
				return false;
			}
			return axis.Groupings.Any((DataShapeBindingAxisGrouping g) => !g.InstanceFilters.IsNullOrEmptyCollection<FilterDefinition>());
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000D00E File Offset: 0x0000B20E
		internal static bool HasScopedAggregates(this DataShapeBindingAxis axis)
		{
			if (axis == null || axis.Groupings == null)
			{
				return false;
			}
			return axis.Groupings.Any((DataShapeBindingAxisGrouping g) => g.HasScopedAggregates());
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000D050 File Offset: 0x0000B250
		internal static bool HasScopedAggregates(this DataShapeBindingAxisGrouping axisGrouping)
		{
			if (axisGrouping == null || axisGrouping.Aggregates.IsNullOrEmptyCollection<DataShapeBindingAggregate>())
			{
				return false;
			}
			return axisGrouping.Aggregates.Any((DataShapeBindingAggregate agg) => agg.Aggregations.Any((DataShapeBindingAggregateContainer aggAgg) => aggAgg.Scope != null));
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
		internal static List<TOut> BuildList<TOut, TIn>(IReadOnlyList<TIn> items, Func<TIn, TOut> buildItem)
		{
			if (items.IsNullOrEmpty<TIn>())
			{
				return null;
			}
			List<TOut> list = new List<TOut>(items.Count);
			foreach (TIn tin in items)
			{
				TOut tout = buildItem(tin);
				list.Add(tout);
			}
			return list;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000D108 File Offset: 0x0000B308
		internal static bool TryGetInnermostDynamic(this IReadOnlyList<DataMemberBuilderPair> dynamics, out DataMember innermostDynamic)
		{
			if (dynamics == null || dynamics.Count == 0)
			{
				innermostDynamic = null;
				return false;
			}
			DataMemberBuilderPair dataMemberBuilderPair = dynamics[dynamics.Count - 1];
			innermostDynamic = dataMemberBuilderPair.Dynamic.Parent();
			return true;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000D144 File Offset: 0x0000B344
		[DebuggerStepThrough]
		internal static bool ContainsAll<T>(this IReadOnlyList<T> collection, IReadOnlyList<T> elements, IEqualityComparer<T> comparer)
		{
			for (int i = 0; i < elements.Count; i++)
			{
				T t = elements[i];
				if (!collection.Contains(t, comparer))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000D178 File Offset: 0x0000B378
		[DebuggerStepThrough]
		internal static bool ContainsNone<T>(this IEnumerable<T> collection, IReadOnlyList<T> elements, IEqualityComparer<T> comparer)
		{
			for (int i = 0; i < elements.Count; i++)
			{
				T t = elements[i];
				if (collection.Contains(t, comparer))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000D1AB File Offset: 0x0000B3AB
		internal static bool MatchesExpression(this ProjectedDsqExpression projection, ExpressionNode expression)
		{
			return !projection.Value.IsEmpty && projection.Value.DsqExpression.Equals(expression);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000D1CD File Offset: 0x0000B3CD
		internal static bool IsSelfIdentity(this IConceptualColumn column)
		{
			return column.Grouping.IdentityColumns.Contains(column);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000D1E0 File Offset: 0x0000B3E0
		internal static ConceptualCapabilities GetCapabilities(this IFederatedConceptualSchema federatedSchema)
		{
			IConceptualSchema conceptualSchema;
			federatedSchema.TryGetDefaultSchema(out conceptualSchema);
			return conceptualSchema.Capabilities;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000D1FC File Offset: 0x0000B3FC
		internal static string ToTraceString(this ResolvedQueryExpression expression)
		{
			string text = expression.Accept<QueryExpression>(ResolvedQueryExpressionSerializer.Instance).ToTraceString();
			if (text.Length <= 2000)
			{
				return text;
			}
			return text.Substring(0, 2000);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000D235 File Offset: 0x0000B435
		internal static ScrubbedEntityPropertyReference GetPropertyNameForError(this IConceptualProperty comparedProperty)
		{
			return new ScrubbedEntityPropertyReference("'" + comparedProperty.Entity.Name + "'", comparedProperty.Name, comparedProperty.Entity.Schema.SchemaId);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000D26C File Offset: 0x0000B46C
		internal static void PopulateModelPropertyNames(IConceptualSchema schema, NamingContext namingContext)
		{
			IReadOnlyList<IConceptualEntity> entities = schema.Entities;
			if (entities.IsNullOrEmpty<IConceptualEntity>())
			{
				return;
			}
			foreach (IConceptualEntity conceptualEntity in entities)
			{
				DsqGenerationUtils.PopulateModelPropertyNames(conceptualEntity, namingContext);
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000D2C4 File Offset: 0x0000B4C4
		internal static void PopulateModelPropertyNames(IConceptualEntity entity, NamingContext namingContext)
		{
			IReadOnlyList<IConceptualProperty> properties = entity.Properties;
			if (properties == null)
			{
				return;
			}
			foreach (IConceptualProperty conceptualProperty in properties)
			{
				namingContext.RegisterUniqueName(conceptualProperty.Name);
			}
		}

		// Token: 0x04000226 RID: 550
		private const int MaxExpressionTraceLength = 2000;
	}
}
