using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x0200056E RID: 1390
	internal abstract class ViewgenGatekeeper : InternalBase
	{
		// Token: 0x060043B1 RID: 17329 RVA: 0x000EBDE8 File Offset: 0x000E9FE8
		internal static ViewGenResults GenerateViewsFromMapping(EntityContainerMapping containerMapping, ConfigViewGenerator config)
		{
			CellCreator cellCreator = new CellCreator(containerMapping);
			List<Cell> list = cellCreator.GenerateCells();
			CqlIdentifiers identifiers = cellCreator.Identifiers;
			return ViewgenGatekeeper.GenerateViewsFromCells(list, config, identifiers, containerMapping);
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x000EBE14 File Offset: 0x000EA014
		internal static ViewGenResults GenerateTypeSpecificQueryView(EntityContainerMapping containerMapping, ConfigViewGenerator config, EntitySetBase entity, EntityTypeBase type, bool includeSubtypes, out bool success)
		{
			if (config.IsNormalTracing)
			{
				Helpers.StringTraceLine("");
				Helpers.StringTraceLine(string.Concat(new string[]
				{
					"<<<<<<<< Generating Query View for Entity [",
					entity.Name,
					"] OfType",
					includeSubtypes ? "" : "Only",
					"(",
					type.Name,
					") >>>>>>>"
				}));
			}
			if (containerMapping.GetEntitySetMapping(entity.Name).QueryView != null)
			{
				success = false;
				return null;
			}
			InputForComputingCellGroups inputForComputingCellGroups = new InputForComputingCellGroups(containerMapping, config);
			OutputFromComputeCellGroups cellgroups = containerMapping.GetCellgroups(inputForComputingCellGroups);
			success = cellgroups.Success;
			if (!success)
			{
				return null;
			}
			List<ForeignConstraint> foreignKeyConstraints = cellgroups.ForeignKeyConstraints;
			List<Set<Cell>> list = cellgroups.CellGroups.Select((Set<Cell> setOfCells) => new Set<Cell>(setOfCells.Select((Cell cell) => new Cell(cell)))).ToList<Set<Cell>>();
			IEnumerable<Cell> cells = cellgroups.Cells;
			CqlIdentifiers identifiers = cellgroups.Identifiers;
			ViewGenResults viewGenResults = new ViewGenResults();
			ErrorLog errorLog = ViewgenGatekeeper.EnsureAllCSpaceContainerSetsAreMapped(cells, containerMapping);
			if (errorLog.Count > 0)
			{
				viewGenResults.AddErrors(errorLog);
				Helpers.StringTraceLine(viewGenResults.ErrorsToString());
				success = true;
				return viewGenResults;
			}
			foreach (Set<Cell> set in list)
			{
				if (ViewgenGatekeeper.DoesCellGroupContainEntitySet(set, entity))
				{
					ViewGenerator viewGenerator = null;
					ErrorLog errorLog2 = new ErrorLog();
					try
					{
						viewGenerator = new ViewGenerator(set, config, foreignKeyConstraints, containerMapping);
					}
					catch (InternalMappingException ex)
					{
						errorLog2 = ex.ErrorLog;
					}
					if (errorLog2.Count > 0)
					{
						break;
					}
					ViewGenMode viewGenMode = (includeSubtypes ? ViewGenMode.OfTypeViews : ViewGenMode.OfTypeOnlyViews);
					errorLog2 = viewGenerator.GenerateQueryViewForSingleExtent(viewGenResults.Views, identifiers, entity, type, viewGenMode);
					if (errorLog2.Count != 0)
					{
						viewGenResults.AddErrors(errorLog2);
					}
				}
			}
			success = true;
			return viewGenResults;
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x000EBFF4 File Offset: 0x000EA1F4
		private static ViewGenResults GenerateViewsFromCells(List<Cell> cells, ConfigViewGenerator config, CqlIdentifiers identifiers, EntityContainerMapping containerMapping)
		{
			EntityContainer storageEntityContainer = containerMapping.StorageEntityContainer;
			ViewGenResults viewGenResults = new ViewGenResults();
			ErrorLog errorLog = ViewgenGatekeeper.EnsureAllCSpaceContainerSetsAreMapped(cells, containerMapping);
			if (errorLog.Count > 0)
			{
				viewGenResults.AddErrors(errorLog);
				Helpers.StringTraceLine(viewGenResults.ErrorsToString());
				return viewGenResults;
			}
			List<ForeignConstraint> foreignConstraints = ForeignConstraint.GetForeignConstraints(storageEntityContainer);
			foreach (Set<Cell> set in new CellPartitioner(cells, foreignConstraints).GroupRelatedCells())
			{
				ViewGenerator viewGenerator = null;
				ErrorLog errorLog2 = new ErrorLog();
				try
				{
					viewGenerator = new ViewGenerator(set, config, foreignConstraints, containerMapping);
				}
				catch (InternalMappingException ex)
				{
					errorLog2 = ex.ErrorLog;
				}
				if (errorLog2.Count == 0)
				{
					errorLog2 = viewGenerator.GenerateAllBidirectionalViews(viewGenResults.Views, identifiers);
				}
				if (errorLog2.Count != 0)
				{
					viewGenResults.AddErrors(errorLog2);
				}
			}
			return viewGenResults;
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x000EC0DC File Offset: 0x000EA2DC
		private static ErrorLog EnsureAllCSpaceContainerSetsAreMapped(IEnumerable<Cell> cells, EntityContainerMapping containerMapping)
		{
			Set<EntitySetBase> set = new Set<EntitySetBase>();
			EntityContainer entityContainer = null;
			foreach (Cell cell in cells)
			{
				set.Add(cell.CQuery.Extent);
				entityContainer = cell.CQuery.Extent.EntityContainer;
			}
			List<EntitySetBase> list = new List<EntitySetBase>();
			foreach (EntitySetBase entitySetBase in entityContainer.BaseEntitySets)
			{
				if (!set.Contains(entitySetBase) && !containerMapping.HasQueryViewForSetMap(entitySetBase.Name))
				{
					AssociationSet associationSet = entitySetBase as AssociationSet;
					if (associationSet == null || !associationSet.ElementType.IsForeignKey)
					{
						list.Add(entitySetBase);
					}
				}
			}
			ErrorLog errorLog = new ErrorLog();
			if (list.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				foreach (EntitySetBase entitySetBase2 in list)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append(entitySetBase2.Name);
				}
				string text = Strings.ViewGen_Missing_Set_Mapping(stringBuilder);
				int num = -1;
				foreach (Cell cell2 in cells)
				{
					if (num == -1 || cell2.CellLabel.StartLineNumber < num)
					{
						num = cell2.CellLabel.StartLineNumber;
					}
				}
				ErrorLog.Record record = new ErrorLog.Record(new EdmSchemaError(text, 3027, EdmSchemaErrorSeverity.Error, containerMapping.SourceLocation, containerMapping.StartLineNumber, containerMapping.StartLinePosition, null));
				errorLog.AddEntry(record);
			}
			return errorLog;
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x000EC2DC File Offset: 0x000EA4DC
		private static bool DoesCellGroupContainEntitySet(Set<Cell> group, EntitySetBase entity)
		{
			using (HashSet<Cell>.Enumerator enumerator = group.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetLeftQuery(ViewTarget.QueryView).Extent.Equals(entity))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x000EC33C File Offset: 0x000EA53C
		internal override void ToCompactString(StringBuilder builder)
		{
		}
	}
}
