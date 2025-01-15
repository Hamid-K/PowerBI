using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D73 RID: 3443
	internal static class SetScopeExtensions
	{
		// Token: 0x06005DAA RID: 23978 RVA: 0x00143FE4 File Offset: 0x001421E4
		public static Dimensionality NewScope(this Dimensionality dimensionality, string scope)
		{
			List<CubeLevelRange> list = new List<CubeLevelRange>();
			foreach (ICubeHierarchy cubeHierarchy in dimensionality.Hierarchies)
			{
				CubeLevelRange levelRange = dimensionality.GetLevelRange(cubeHierarchy);
				CubeLevelRange cubeLevelRange = new CubeLevelRange(levelRange.Coarse.NewScope(scope), levelRange.Fine.NewScope(scope));
				list.Add(cubeLevelRange);
			}
			return new Dimensionality(list);
		}

		// Token: 0x06005DAB RID: 23979 RVA: 0x00144068 File Offset: 0x00142268
		public static Dimensionality ReplaceScopePaths(this Dimensionality dimensionality, IDictionary<ScopePath, ScopePath> replacements)
		{
			List<CubeLevelRange> list = new List<CubeLevelRange>();
			foreach (ICubeHierarchy cubeHierarchy in dimensionality.Hierarchies)
			{
				CubeLevelRange cubeLevelRange = dimensionality.GetLevelRange(cubeHierarchy);
				ScopePath scopePath;
				ICubeLevel unscoped = cubeLevelRange.Coarse.GetUnscoped(out scopePath);
				ScopePath scopePath2;
				ICubeLevel unscoped2 = cubeLevelRange.Fine.GetUnscoped(out scopePath2);
				ScopePath scopePath3;
				if (replacements.TryGetValue(scopePath2, out scopePath3))
				{
					cubeLevelRange = new CubeLevelRange(unscoped.NewScopePath(scopePath3), unscoped2.NewScopePath(scopePath3));
				}
				list.Add(cubeLevelRange);
			}
			return new Dimensionality(list);
		}

		// Token: 0x06005DAC RID: 23980 RVA: 0x0014410C File Offset: 0x0014230C
		public static Projection NewScope(this Projection projection, string scope)
		{
			IdentifierCubeExpression[] array = new IdentifierCubeExpression[projection.Identifiers.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = projection.Identifiers[i].NewScope(scope);
			}
			return new Projection(projection.Keys, array);
		}

		// Token: 0x06005DAD RID: 23981 RVA: 0x00144154 File Offset: 0x00142354
		public static Projection ReplaceScopePaths(this Projection projection, IDictionary<ScopePath, ScopePath> replacements)
		{
			IdentifierCubeExpression[] array = new IdentifierCubeExpression[projection.Identifiers.Length];
			for (int i = 0; i < array.Length; i++)
			{
				IdentifierCubeExpression identifierCubeExpression = projection.Identifiers[i];
				ScopePath scopePath;
				IdentifierCubeExpression unscoped = identifierCubeExpression.GetUnscoped(out scopePath);
				ScopePath scopePath2;
				if (replacements.TryGetValue(scopePath, out scopePath2))
				{
					array[i] = unscoped.SetScopePath(scopePath2);
				}
				else
				{
					array[i] = identifierCubeExpression;
				}
			}
			return new Projection(projection.Keys, array);
		}

		// Token: 0x06005DAE RID: 23982 RVA: 0x001441B9 File Offset: 0x001423B9
		public static Set ReplaceScopePaths(this Set set, IDictionary<ScopePath, ScopePath> replacements)
		{
			return new SetScopeExtensions.ScopePathReplacementSetVisitor(replacements).Visit(set);
		}

		// Token: 0x02000D74 RID: 3444
		private class ScopePathReplacementSetVisitor : SetVisitor<Set, CubeExpression, CubeSortOrder>
		{
			// Token: 0x06005DAF RID: 23983 RVA: 0x001441C7 File Offset: 0x001423C7
			public ScopePathReplacementSetVisitor(IDictionary<ScopePath, ScopePath> replacements)
			{
				this.replacements = replacements;
			}

			// Token: 0x06005DB0 RID: 23984 RVA: 0x001441D8 File Offset: 0x001423D8
			protected override Set NewCrossJoin(Set[] sets)
			{
				Set set = EverythingSet.Instance;
				for (int i = 0; i < sets.Length; i++)
				{
					set = set.CrossJoin(sets[i]);
				}
				return set;
			}

			// Token: 0x06005DB1 RID: 23985 RVA: 0x00144204 File Offset: 0x00142404
			protected override Set NewDescendTo(Set set, Dimensionality from, Dimensionality to)
			{
				return set.DescendTo(to.ReplaceScopePaths(this.replacements));
			}

			// Token: 0x06005DB2 RID: 23986 RVA: 0x00144218 File Offset: 0x00142418
			protected override Set NewDistinct(Set set)
			{
				return set.EnsureUniqueHierarchyMembers();
			}

			// Token: 0x06005DB3 RID: 23987 RVA: 0x000EA786 File Offset: 0x000E8986
			protected override Set NewEverything()
			{
				return EverythingSet.Instance;
			}

			// Token: 0x06005DB4 RID: 23988 RVA: 0x00144220 File Offset: 0x00142420
			protected override Set NewExcept(Set set, Set except)
			{
				return set.Except(except);
			}

			// Token: 0x06005DB5 RID: 23989 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override Set NewFilter(Set set, CubeExpression expression)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06005DB6 RID: 23990 RVA: 0x00144229 File Offset: 0x00142429
			protected override Set NewIntersect(Set set1, Set set2)
			{
				return set1.Intersect(set2);
			}

			// Token: 0x06005DB7 RID: 23991 RVA: 0x00144232 File Offset: 0x00142432
			protected override Set NewLevel(ICubeLevel level)
			{
				return new LevelSet(level.ReplaceScopePaths(this.replacements));
			}

			// Token: 0x06005DB8 RID: 23992 RVA: 0x00144245 File Offset: 0x00142445
			protected override Set NewMember(ICubeLevel level, Value member)
			{
				return new MemberSet(level.ReplaceScopePaths(this.replacements), member);
			}

			// Token: 0x06005DB9 RID: 23993 RVA: 0x0014425C File Offset: 0x0014245C
			protected override Set NewOrderBy(Set set, CubeSortOrder[] order)
			{
				CubeSortOrder[] array = new CubeSortOrder[order.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = order[i].ReplaceScopePaths(this.replacements);
				}
				return set.OrderBy(array);
			}

			// Token: 0x06005DBA RID: 23994 RVA: 0x00144298 File Offset: 0x00142498
			protected override Set NewOrderHierarchies(Set set, Dimensionality from, Dimensionality to)
			{
				return set.OrderHierarchies(to.ReplaceScopePaths(this.replacements));
			}

			// Token: 0x06005DBB RID: 23995 RVA: 0x001442AC File Offset: 0x001424AC
			protected override Set NewProject(Set set, IEnumerable<ICubeObject> objects)
			{
				return set.Project(objects.Select((ICubeObject o) => o.ReplaceScopePaths(this.replacements)));
			}

			// Token: 0x06005DBC RID: 23996 RVA: 0x001442C6 File Offset: 0x001424C6
			protected override Set NewSkipTake(Set set, RowRange rowRange)
			{
				return set.Skip(rowRange.SkipCount).Take(rowRange.TakeCount);
			}

			// Token: 0x06005DBD RID: 23997 RVA: 0x001442E4 File Offset: 0x001424E4
			protected override Set NewUnion(Set[] sets)
			{
				Set set = EverythingSet.Instance;
				for (int i = 0; i < sets.Length; i++)
				{
					set = set.Union(sets[i]);
				}
				return set;
			}

			// Token: 0x06005DBE RID: 23998 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override Set VisitOther(Set set)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06005DBF RID: 23999 RVA: 0x000020F7 File Offset: 0x000002F7
			protected override CubeExpression VisitFilter(Set set, CubeExpression filter)
			{
				return filter;
			}

			// Token: 0x06005DC0 RID: 24000 RVA: 0x000020F7 File Offset: 0x000002F7
			protected override CubeSortOrder VisitOrder(Set set, CubeSortOrder order)
			{
				return order;
			}

			// Token: 0x06005DC1 RID: 24001 RVA: 0x00144310 File Offset: 0x00142510
			protected override Set VisitFilter(FilterSet filter)
			{
				Set set = this.Visit(filter.Set);
				Dimensionality dimensionality = filter.Dimensionality.ReplaceScopePaths(this.replacements);
				CubeExpression cubeExpression = filter.Predicate.ReplaceScopePaths(this.replacements);
				bool hasMeasureFilter = filter.HasMeasureFilter;
				return FilterSet.New(set, dimensionality, cubeExpression, hasMeasureFilter);
			}

			// Token: 0x0400337B RID: 13179
			private readonly IDictionary<ScopePath, ScopePath> replacements;
		}
	}
}
