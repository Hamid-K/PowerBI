using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x0200056D RID: 1389
	internal class ViewGenerator : InternalBase
	{
		// Token: 0x060043A3 RID: 17315 RVA: 0x000EB3F4 File Offset: 0x000E95F4
		internal ViewGenerator(Set<Cell> cellGroup, ConfigViewGenerator config, List<ForeignConstraint> foreignKeyConstraints, EntityContainerMapping entityContainerMapping)
		{
			this.m_cellGroup = cellGroup;
			this.m_config = config;
			this.m_queryRewriterCache = new Dictionary<EntitySetBase, QueryRewriter>();
			this.m_foreignKeyConstraints = foreignKeyConstraints;
			this.m_entityContainerMapping = entityContainerMapping;
			Dictionary<EntityType, Set<EntityType>> dictionary = MetadataHelper.BuildUndirectedGraphOfTypes(entityContainerMapping.StorageMappingItemCollection.EdmItemCollection);
			this.SetConfiguration(entityContainerMapping);
			this.m_queryDomainMap = new MemberDomainMap(ViewTarget.QueryView, this.m_config.IsValidationEnabled, cellGroup, entityContainerMapping.StorageMappingItemCollection.EdmItemCollection, this.m_config, dictionary);
			this.m_updateDomainMap = new MemberDomainMap(ViewTarget.UpdateView, this.m_config.IsValidationEnabled, cellGroup, entityContainerMapping.StorageMappingItemCollection.EdmItemCollection, this.m_config, dictionary);
			MemberDomainMap.PropagateUpdateDomainToQueryDomain(cellGroup, this.m_queryDomainMap, this.m_updateDomainMap);
			ViewGenerator.UpdateWhereClauseForEachCell(cellGroup, this.m_queryDomainMap, this.m_updateDomainMap, this.m_config);
			MemberDomainMap openDomain = this.m_queryDomainMap.GetOpenDomain();
			MemberDomainMap openDomain2 = this.m_updateDomainMap.GetOpenDomain();
			foreach (Cell cell in cellGroup)
			{
				cell.CQuery.WhereClause.FixDomainMap(openDomain);
				cell.SQuery.WhereClause.FixDomainMap(openDomain2);
				cell.CQuery.WhereClause.ExpensiveSimplify();
				cell.SQuery.WhereClause.ExpensiveSimplify();
				cell.CQuery.WhereClause.FixDomainMap(this.m_queryDomainMap);
				cell.SQuery.WhereClause.FixDomainMap(this.m_updateDomainMap);
			}
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x000EB588 File Offset: 0x000E9788
		private void SetConfiguration(EntityContainerMapping entityContainerMapping)
		{
			this.m_config.IsValidationEnabled = entityContainerMapping.Validate;
			this.m_config.GenerateUpdateViews = entityContainerMapping.GenerateUpdateViews;
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x000EB5AC File Offset: 0x000E97AC
		internal ErrorLog GenerateAllBidirectionalViews(KeyToListMap<EntitySetBase, GeneratedView> views, CqlIdentifiers identifiers)
		{
			if (this.m_config.IsNormalTracing)
			{
				StringBuilder stringBuilder = new StringBuilder();
				Cell.CellsToBuilder(stringBuilder, this.m_cellGroup);
				Helpers.StringTraceLine(stringBuilder.ToString());
			}
			this.m_config.SetTimeForFinishedActivity(PerfType.CellCreation);
			ErrorLog errorLog = new CellGroupValidator(this.m_cellGroup, this.m_config).Validate();
			if (errorLog.Count > 0)
			{
				errorLog.PrintTrace();
				return errorLog;
			}
			this.m_config.SetTimeForFinishedActivity(PerfType.KeyConstraint);
			if (this.m_config.GenerateUpdateViews)
			{
				errorLog = this.GenerateDirectionalViews(ViewTarget.UpdateView, identifiers, views);
				if (errorLog.Count > 0)
				{
					return errorLog;
				}
			}
			if (this.m_config.IsValidationEnabled)
			{
				this.CheckForeignKeyConstraints(errorLog);
			}
			this.m_config.SetTimeForFinishedActivity(PerfType.ForeignConstraint);
			if (errorLog.Count > 0)
			{
				errorLog.PrintTrace();
				return errorLog;
			}
			this.m_updateDomainMap.ExpandDomainsToIncludeAllPossibleValues();
			return this.GenerateDirectionalViews(ViewTarget.QueryView, identifiers, views);
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x000EB68C File Offset: 0x000E988C
		internal ErrorLog GenerateQueryViewForSingleExtent(KeyToListMap<EntitySetBase, GeneratedView> views, CqlIdentifiers identifiers, EntitySetBase entity, EntityTypeBase type, ViewGenMode mode)
		{
			if (this.m_config.IsNormalTracing)
			{
				StringBuilder stringBuilder = new StringBuilder();
				Cell.CellsToBuilder(stringBuilder, this.m_cellGroup);
				Helpers.StringTraceLine(stringBuilder.ToString());
			}
			ErrorLog errorLog = new CellGroupValidator(this.m_cellGroup, this.m_config).Validate();
			if (errorLog.Count > 0)
			{
				errorLog.PrintTrace();
				return errorLog;
			}
			if (this.m_config.IsValidationEnabled)
			{
				this.CheckForeignKeyConstraints(errorLog);
			}
			if (errorLog.Count > 0)
			{
				errorLog.PrintTrace();
				return errorLog;
			}
			this.m_updateDomainMap.ExpandDomainsToIncludeAllPossibleValues();
			foreach (Cell cell in this.m_cellGroup)
			{
				cell.SQuery.WhereClause.FixDomainMap(this.m_updateDomainMap);
			}
			return this.GenerateQueryViewForExtentAndType(identifiers, views, entity, type, mode);
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x000EB77C File Offset: 0x000E997C
		private static void UpdateWhereClauseForEachCell(IEnumerable<Cell> extentCells, MemberDomainMap queryDomainMap, MemberDomainMap updateDomainMap, ConfigViewGenerator config)
		{
			foreach (Cell cell in extentCells)
			{
				cell.CQuery.UpdateWhereClause(queryDomainMap);
				cell.SQuery.UpdateWhereClause(updateDomainMap);
			}
			queryDomainMap.ReduceEnumerableDomainToEnumeratedValues(config);
			updateDomainMap.ReduceEnumerableDomainToEnumeratedValues(config);
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x000EB7E4 File Offset: 0x000E99E4
		private ErrorLog GenerateQueryViewForExtentAndType(CqlIdentifiers identifiers, KeyToListMap<EntitySetBase, GeneratedView> views, EntitySetBase entity, EntityTypeBase type, ViewGenMode mode)
		{
			ErrorLog errorLog = new ErrorLog();
			if (this.m_config.IsViewTracing)
			{
				Helpers.StringTraceLine(string.Empty);
				Helpers.StringTraceLine(string.Empty);
				Helpers.FormatTraceLine("================= Generating {0} Query View for: {1} ===========================", new object[]
				{
					(mode == ViewGenMode.OfTypeViews) ? "OfType" : "OfTypeOnly",
					entity.Name
				});
				Helpers.StringTraceLine(string.Empty);
				Helpers.StringTraceLine(string.Empty);
			}
			try
			{
				ViewgenContext viewgenContext = this.CreateViewgenContext(entity, ViewTarget.QueryView, identifiers);
				this.GenerateViewsForExtentAndType(type, viewgenContext, identifiers, views, mode);
			}
			catch (InternalMappingException ex)
			{
				errorLog.Merge(ex.ErrorLog);
			}
			return errorLog;
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x000EB894 File Offset: 0x000E9A94
		private ErrorLog GenerateDirectionalViews(ViewTarget viewTarget, CqlIdentifiers identifiers, KeyToListMap<EntitySetBase, GeneratedView> views)
		{
			bool flag = viewTarget == ViewTarget.QueryView;
			KeyToListMap<EntitySetBase, Cell> keyToListMap = ViewGenerator.GroupCellsByExtent(this.m_cellGroup, viewTarget);
			ErrorLog errorLog = new ErrorLog();
			foreach (EntitySetBase entitySetBase in keyToListMap.Keys)
			{
				if (this.m_config.IsViewTracing)
				{
					Helpers.StringTraceLine(string.Empty);
					Helpers.StringTraceLine(string.Empty);
					Helpers.FormatTraceLine("================= Generating {0} View for: {1} ===========================", new object[]
					{
						flag ? "Query" : "Update",
						entitySetBase.Name
					});
					Helpers.StringTraceLine(string.Empty);
					Helpers.StringTraceLine(string.Empty);
				}
				try
				{
					QueryRewriter queryRewriter = this.GenerateDirectionalViewsForExtent(viewTarget, entitySetBase, identifiers, views);
					if (viewTarget == ViewTarget.UpdateView && this.m_config.IsValidationEnabled)
					{
						if (this.m_config.IsViewTracing)
						{
							Helpers.StringTraceLine(string.Empty);
							Helpers.StringTraceLine(string.Empty);
							Helpers.FormatTraceLine("----------------- Validation for generated update view for: {0} -----------------", new object[] { entitySetBase.Name });
							Helpers.StringTraceLine(string.Empty);
							Helpers.StringTraceLine(string.Empty);
						}
						new RewritingValidator(queryRewriter.ViewgenContext, queryRewriter.BasicView).Validate();
					}
				}
				catch (InternalMappingException ex)
				{
					errorLog.Merge(ex.ErrorLog);
				}
			}
			return errorLog;
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x000EBA14 File Offset: 0x000E9C14
		private QueryRewriter GenerateDirectionalViewsForExtent(ViewTarget viewTarget, EntitySetBase extent, CqlIdentifiers identifiers, KeyToListMap<EntitySetBase, GeneratedView> views)
		{
			ViewgenContext viewgenContext = this.CreateViewgenContext(extent, viewTarget, identifiers);
			QueryRewriter queryRewriter = null;
			if (this.m_config.GenerateViewsForEachType)
			{
				using (IEnumerator<EdmType> enumerator = MetadataHelper.GetTypeAndSubtypesOf(extent.ElementType, this.m_entityContainerMapping.StorageMappingItemCollection.EdmItemCollection, false).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EdmType edmType = enumerator.Current;
						if (this.m_config.IsViewTracing && !edmType.Equals(extent.ElementType))
						{
							Helpers.FormatTraceLine("CQL View for {0} and type {1}", new object[] { extent.Name, edmType.Name });
						}
						queryRewriter = this.GenerateViewsForExtentAndType(edmType, viewgenContext, identifiers, views, ViewGenMode.OfTypeViews);
					}
					goto IL_00B7;
				}
			}
			queryRewriter = this.GenerateViewsForExtentAndType(extent.ElementType, viewgenContext, identifiers, views, ViewGenMode.OfTypeViews);
			IL_00B7:
			if (viewTarget == ViewTarget.QueryView)
			{
				this.m_config.SetTimeForFinishedActivity(PerfType.QueryViews);
			}
			else
			{
				this.m_config.SetTimeForFinishedActivity(PerfType.UpdateViews);
			}
			this.m_queryRewriterCache[extent] = queryRewriter;
			return queryRewriter;
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x000EBB14 File Offset: 0x000E9D14
		private ViewgenContext CreateViewgenContext(EntitySetBase extent, ViewTarget viewTarget, CqlIdentifiers identifiers)
		{
			QueryRewriter queryRewriter;
			if (!this.m_queryRewriterCache.TryGetValue(extent, out queryRewriter))
			{
				List<Cell> list = this.m_cellGroup.Where((Cell c) => c.GetLeftQuery(viewTarget).Extent == extent).ToList<Cell>();
				return new ViewgenContext(viewTarget, extent, list, identifiers, this.m_config, this.m_queryDomainMap, this.m_updateDomainMap, this.m_entityContainerMapping);
			}
			return queryRewriter.ViewgenContext;
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x000EBB9C File Offset: 0x000E9D9C
		private QueryRewriter GenerateViewsForExtentAndType(EdmType generatedType, ViewgenContext context, CqlIdentifiers identifiers, KeyToListMap<EntitySetBase, GeneratedView> views, ViewGenMode mode)
		{
			QueryRewriter queryRewriter = new QueryRewriter(generatedType, context, mode);
			queryRewriter.GenerateViewComponents();
			CellTreeNode basicView = queryRewriter.BasicView;
			if (this.m_config.IsNormalTracing)
			{
				Helpers.StringTrace("Basic View: ");
				Helpers.StringTraceLine(basicView.ToString());
			}
			CellTreeNode cellTreeNode = ViewGenerator.GenerateSimplifiedView(basicView, queryRewriter.UsedCells);
			if (this.m_config.IsNormalTracing)
			{
				Helpers.StringTraceLine(string.Empty);
				Helpers.StringTrace("Simplified View: ");
				Helpers.StringTraceLine(cellTreeNode.ToString());
			}
			CqlGenerator cqlGenerator = new CqlGenerator(cellTreeNode, queryRewriter.CaseStatements, identifiers, context.MemberMaps.ProjectedSlotMap, queryRewriter.UsedCells.Count, queryRewriter.TopLevelWhereClause, this.m_entityContainerMapping.StorageMappingItemCollection);
			string text;
			DbQueryCommandTree dbQueryCommandTree;
			if (this.m_config.GenerateEsql)
			{
				text = cqlGenerator.GenerateEsql();
				dbQueryCommandTree = null;
			}
			else
			{
				text = null;
				dbQueryCommandTree = cqlGenerator.GenerateCqt();
			}
			GeneratedView generatedView = GeneratedView.CreateGeneratedView(context.Extent, generatedType, dbQueryCommandTree, text, this.m_entityContainerMapping.StorageMappingItemCollection, this.m_config);
			views.Add(context.Extent, generatedView);
			return queryRewriter;
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x000EBCA8 File Offset: 0x000E9EA8
		private static CellTreeNode GenerateSimplifiedView(CellTreeNode basicView, List<LeftCellWrapper> usedCells)
		{
			int count = usedCells.Count;
			for (int i = 0; i < count; i++)
			{
				usedCells[i].RightCellQuery.InitializeBoolExpressions(count, i);
			}
			return CellTreeSimplifier.MergeNodes(basicView);
		}

		// Token: 0x060043AE RID: 17326 RVA: 0x000EBCE4 File Offset: 0x000E9EE4
		private void CheckForeignKeyConstraints(ErrorLog errorLog)
		{
			foreach (ForeignConstraint foreignConstraint in this.m_foreignKeyConstraints)
			{
				QueryRewriter queryRewriter = null;
				QueryRewriter queryRewriter2 = null;
				this.m_queryRewriterCache.TryGetValue(foreignConstraint.ChildTable, out queryRewriter);
				this.m_queryRewriterCache.TryGetValue(foreignConstraint.ParentTable, out queryRewriter2);
				foreignConstraint.CheckConstraint(this.m_cellGroup, queryRewriter, queryRewriter2, errorLog, this.m_config);
			}
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x000EBD74 File Offset: 0x000E9F74
		private static KeyToListMap<EntitySetBase, Cell> GroupCellsByExtent(IEnumerable<Cell> cells, ViewTarget viewTarget)
		{
			KeyToListMap<EntitySetBase, Cell> keyToListMap = new KeyToListMap<EntitySetBase, Cell>(EqualityComparer<EntitySetBase>.Default);
			foreach (Cell cell in cells)
			{
				CellQuery leftQuery = cell.GetLeftQuery(viewTarget);
				keyToListMap.Add(leftQuery.Extent, cell);
			}
			return keyToListMap;
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x000EBDD8 File Offset: 0x000E9FD8
		internal override void ToCompactString(StringBuilder builder)
		{
			Cell.CellsToBuilder(builder, this.m_cellGroup);
		}

		// Token: 0x04001845 RID: 6213
		private readonly Set<Cell> m_cellGroup;

		// Token: 0x04001846 RID: 6214
		private readonly ConfigViewGenerator m_config;

		// Token: 0x04001847 RID: 6215
		private readonly MemberDomainMap m_queryDomainMap;

		// Token: 0x04001848 RID: 6216
		private readonly MemberDomainMap m_updateDomainMap;

		// Token: 0x04001849 RID: 6217
		private readonly Dictionary<EntitySetBase, QueryRewriter> m_queryRewriterCache;

		// Token: 0x0400184A RID: 6218
		private readonly List<ForeignConstraint> m_foreignKeyConstraints;

		// Token: 0x0400184B RID: 6219
		private readonly EntityContainerMapping m_entityContainerMapping;
	}
}
