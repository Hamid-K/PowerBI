using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.InfoNav.Common;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000018 RID: 24
	[ImmutableObject(true)]
	internal sealed class ConceptualSchemaStatisticsComputer
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00003984 File Offset: 0x00001B84
		internal ConceptualSchemaStatisticsComputer(IReadOnlyList<IConceptualEntity> entities)
		{
			this._entities = entities;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003994 File Offset: 0x00001B94
		internal ConceptualSchemaStatistics Compute()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			MutableWeightedGraph<IConceptualEntity, IConceptualNavigationProperty> mutableWeightedGraph = MutableWeightedGraph<IConceptualEntity, IConceptualNavigationProperty>.CreateUndirectedGraph(4, 4, null);
			foreach (IConceptualEntity conceptualEntity in this._entities)
			{
				int num4 = 0;
				foreach (IConceptualNavigationProperty conceptualNavigationProperty in conceptualEntity.NavigationProperties)
				{
					mutableWeightedGraph.AddVertex(conceptualEntity);
					mutableWeightedGraph.AddEdge(conceptualEntity, conceptualNavigationProperty.TargetEntity, conceptualNavigationProperty);
					if (conceptualNavigationProperty.TargetMultiplicity == ConceptualMultiplicity.One || conceptualNavigationProperty.TargetMultiplicity == ConceptualMultiplicity.ZeroOrOne)
					{
						num4++;
					}
					if (conceptualNavigationProperty.IsActive)
					{
						num++;
					}
					else
					{
						num2++;
					}
				}
				if (num4 > 1)
				{
					num3++;
				}
			}
			int num5 = mutableWeightedGraph.RawGraph.GetVertexIslands(null, null, null).Count<ReadOnlyCollection<int>>();
			return new ConceptualSchemaStatistics(num, num2, num5, num3);
		}

		// Token: 0x040000A7 RID: 167
		private readonly IReadOnlyList<IConceptualEntity> _entities;
	}
}
