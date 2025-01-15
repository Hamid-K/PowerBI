using System;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x0200001E RID: 30
	internal static class ConceptualColumnGroupingMetadataBuilder
	{
		// Token: 0x060000EB RID: 235 RVA: 0x000049A0 File Offset: 0x00002BA0
		internal static ConceptualGroupingMetadata BuildConceptualGroupingMetadata(GroupingMetadata groupingMetadata, IConceptualEntity entity, ITracer tracer)
		{
			if (groupingMetadata == null)
			{
				return null;
			}
			return new ConceptualGroupingMetadata(groupingMetadata.GroupedColumns.Select((QueryExpressionContainer expr) => ConceptualColumnGroupingMetadataBuilder.CreateConceptualGroupedColumn(expr, entity, tracer)).WhereNonNull<ConceptualGroupedColumnContainer>().EvaluateAsReadOnlyList<ConceptualGroupedColumnContainer>(), ConceptualColumnGroupingMetadataBuilder.CreateConceptualBinningMetadata(groupingMetadata.BinningMetadata));
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000049F7 File Offset: 0x00002BF7
		private static ConceptualBinningMetadata CreateConceptualBinningMetadata(BinningMetadata binningMetadata)
		{
			if (binningMetadata == null)
			{
				return null;
			}
			return new ConceptualBinningMetadata(ConceptualColumnGroupingMetadataBuilder.CreateConceptualBinSize(binningMetadata.BinSize));
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004A0E File Offset: 0x00002C0E
		private static ConceptualBinSize CreateConceptualBinSize(BinSize binSize)
		{
			return new ConceptualBinSize(binSize.Value, binSize.Unit);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004A24 File Offset: 0x00002C24
		private static ConceptualGroupedColumnContainer CreateConceptualGroupedColumn(QueryExpressionContainer exprContainer, IConceptualEntity entity, ITracer tracer)
		{
			QueryColumnExpression column = exprContainer.Column;
			QueryHierarchyLevelExpression hierarchyLevel = exprContainer.HierarchyLevel;
			ConceptualGroupedColumnContainer conceptualGroupedColumnContainer = null;
			string text;
			if (column != null)
			{
				IConceptualColumn conceptualColumn;
				if (ConceptualColumnGroupingMetadataBuilder.TryFind(column, entity, out conceptualColumn, out text))
				{
					conceptualGroupedColumnContainer = new ConceptualGroupedColumnContainer(conceptualColumn);
				}
			}
			else if (hierarchyLevel != null)
			{
				IConceptualHierarchyLevel conceptualHierarchyLevel;
				if (ConceptualColumnGroupingMetadataBuilder.TryFind(hierarchyLevel, entity, out conceptualHierarchyLevel, out text))
				{
					conceptualGroupedColumnContainer = new ConceptualGroupedColumnContainer(conceptualHierarchyLevel);
				}
			}
			else
			{
				text = "Unexpected type";
			}
			if (text != null)
			{
				tracer.TraceError(StringUtil.FormatInvariant("{0} for GroupedColumn QueryExpressionContainer {1}", text, exprContainer.ToTraceString()));
			}
			return conceptualGroupedColumnContainer;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004AA4 File Offset: 0x00002CA4
		private static bool TryFind(QueryColumnExpression columnExpr, IConceptualEntity entity, out IConceptualColumn column, out string errorMessage)
		{
			column = null;
			errorMessage = null;
			IConceptualProperty conceptualProperty;
			if (!entity.TryGetProperty(columnExpr.Property, out conceptualProperty))
			{
				errorMessage = "Missing property";
				return false;
			}
			column = conceptualProperty as IConceptualColumn;
			if (column == null)
			{
				errorMessage = "Wrong type";
				return false;
			}
			return true;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004AE8 File Offset: 0x00002CE8
		private static bool TryFind(QueryHierarchyLevelExpression levelExpr, IConceptualEntity entity, out IConceptualHierarchyLevel level, out string errorMessage)
		{
			level = null;
			errorMessage = null;
			IConceptualHierarchy conceptualHierarchy;
			if (!entity.TryGetHierarchy(levelExpr.Expression.Hierarchy.Hierarchy, out conceptualHierarchy))
			{
				errorMessage = "Missing hierarchy";
				return false;
			}
			if (!conceptualHierarchy.TryGetLevel(levelExpr.Level, out level))
			{
				errorMessage = "Missing hierarchy level";
				return false;
			}
			return true;
		}

		// Token: 0x0400014B RID: 331
		internal const string GroupingMetadata = "GroupingMetadata";
	}
}
