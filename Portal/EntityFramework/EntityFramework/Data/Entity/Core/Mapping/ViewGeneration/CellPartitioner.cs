using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.Update.Internal;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x02000564 RID: 1380
	internal class CellPartitioner : InternalBase
	{
		// Token: 0x06004347 RID: 17223 RVA: 0x000E86D5 File Offset: 0x000E68D5
		internal CellPartitioner(IEnumerable<Cell> cells, IEnumerable<ForeignConstraint> foreignKeyConstraints)
		{
			this.m_foreignKeyConstraints = foreignKeyConstraints;
			this.m_cells = cells;
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x000E86EC File Offset: 0x000E68EC
		internal List<Set<Cell>> GroupRelatedCells()
		{
			UndirectedGraph<EntitySetBase> undirectedGraph = new UndirectedGraph<EntitySetBase>(EqualityComparer<EntitySetBase>.Default);
			Dictionary<EntitySetBase, Set<Cell>> extentToCell = new Dictionary<EntitySetBase, Set<Cell>>(EqualityComparer<EntitySetBase>.Default);
			foreach (Cell cell in this.m_cells)
			{
				foreach (EntitySetBase entitySetBase in new EntitySetBase[]
				{
					cell.CQuery.Extent,
					cell.SQuery.Extent
				})
				{
					Set<Cell> set;
					if (!extentToCell.TryGetValue(entitySetBase, out set))
					{
						set = (extentToCell[entitySetBase] = new Set<Cell>());
					}
					set.Add(cell);
					undirectedGraph.AddVertex(entitySetBase);
				}
				undirectedGraph.AddEdge(cell.CQuery.Extent, cell.SQuery.Extent);
				AssociationSet associationSet = cell.CQuery.Extent as AssociationSet;
				if (associationSet != null)
				{
					foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
					{
						undirectedGraph.AddEdge(associationSetEnd.EntitySet, associationSet);
					}
				}
			}
			foreach (ForeignConstraint foreignConstraint in this.m_foreignKeyConstraints)
			{
				undirectedGraph.AddEdge(foreignConstraint.ChildTable, foreignConstraint.ParentTable);
			}
			KeyToListMap<int, EntitySetBase> keyToListMap = undirectedGraph.GenerateConnectedComponents();
			List<Set<Cell>> list = new List<Set<Cell>>();
			Func<EntitySetBase, Set<Cell>> <>9__0;
			foreach (int num in keyToListMap.Keys)
			{
				IEnumerable<EntitySetBase> enumerable = keyToListMap.ListForKey(num);
				Func<EntitySetBase, Set<Cell>> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (EntitySetBase e) => extentToCell[e]);
				}
				IEnumerable<Set<Cell>> enumerable2 = enumerable.Select(func);
				Set<Cell> set2 = new Set<Cell>();
				foreach (Set<Cell> set3 in enumerable2)
				{
					set2.AddRange(set3);
				}
				list.Add(set2);
			}
			return list;
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x000E89AC File Offset: 0x000E6BAC
		internal override void ToCompactString(StringBuilder builder)
		{
			Cell.CellsToBuilder(builder, this.m_cells);
		}

		// Token: 0x04001806 RID: 6150
		private readonly IEnumerable<Cell> m_cells;

		// Token: 0x04001807 RID: 6151
		private readonly IEnumerable<ForeignConstraint> m_foreignKeyConstraints;
	}
}
