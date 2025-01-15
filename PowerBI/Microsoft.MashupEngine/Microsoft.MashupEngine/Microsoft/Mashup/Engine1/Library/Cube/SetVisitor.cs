using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D75 RID: 3445
	internal abstract class SetVisitor<T, F, O>
	{
		// Token: 0x06005DC3 RID: 24003 RVA: 0x0014436C File Offset: 0x0014256C
		public virtual T Visit(Set set)
		{
			switch (set.Kind)
			{
			case SetKind.CrossJoin:
				return this.VisitCrossJoin((CrossJoinSet)set);
			case SetKind.DescendTo:
				return this.VisitDescendTo((DescendToSet)set);
			case SetKind.Distinct:
				return this.VisitDistinct((DistinctSet)set);
			case SetKind.Everything:
				return this.VisitEverything((EverythingSet)set);
			case SetKind.Except:
				return this.VisitExcept((ExceptSet)set);
			case SetKind.Filter:
				return this.VisitFilter((FilterSet)set);
			case SetKind.Intersect:
				return this.VisitIntersect((IntersectSet)set);
			case SetKind.Level:
				return this.VisitLevel((LevelSet)set);
			case SetKind.Member:
				return this.VisitMember((MemberSet)set);
			case SetKind.OrderBy:
				return this.VisitOrderBy((OrderBySet)set);
			case SetKind.OrderHierarchies:
				return this.VisitOrderHierarchies((OrderHierarchiesSet)set);
			case SetKind.Project:
				return this.VisitProject((ProjectSet)set);
			case SetKind.SkipTake:
				return this.VisitSkipTake((SkipTakeSet)set);
			case SetKind.Union:
				return this.VisitUnion((UnionSet)set);
			case SetKind.Other:
				return this.VisitOther(set);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06005DC4 RID: 24004
		protected abstract T NewCrossJoin(T[] sets);

		// Token: 0x06005DC5 RID: 24005
		protected abstract T NewDescendTo(T set, Dimensionality from, Dimensionality to);

		// Token: 0x06005DC6 RID: 24006
		protected abstract T NewDistinct(T set);

		// Token: 0x06005DC7 RID: 24007
		protected abstract T NewEverything();

		// Token: 0x06005DC8 RID: 24008
		protected abstract T NewExcept(T set, T except);

		// Token: 0x06005DC9 RID: 24009
		protected abstract T NewFilter(T set, F expression);

		// Token: 0x06005DCA RID: 24010
		protected abstract T NewIntersect(T set1, T set2);

		// Token: 0x06005DCB RID: 24011
		protected abstract T NewLevel(ICubeLevel level);

		// Token: 0x06005DCC RID: 24012
		protected abstract T NewMember(ICubeLevel level, Value member);

		// Token: 0x06005DCD RID: 24013
		protected abstract T NewOrderBy(T set, O[] order);

		// Token: 0x06005DCE RID: 24014
		protected abstract T NewOrderHierarchies(T set, Dimensionality from, Dimensionality to);

		// Token: 0x06005DCF RID: 24015
		protected abstract T NewProject(T set, IEnumerable<ICubeObject> objects);

		// Token: 0x06005DD0 RID: 24016
		protected abstract T NewSkipTake(T set, RowRange rowRange);

		// Token: 0x06005DD1 RID: 24017
		protected abstract T NewUnion(T[] sets);

		// Token: 0x06005DD2 RID: 24018
		protected abstract T VisitOther(Set set);

		// Token: 0x06005DD3 RID: 24019
		protected abstract F VisitFilter(T set, CubeExpression filter);

		// Token: 0x06005DD4 RID: 24020
		protected abstract O VisitOrder(T set, CubeSortOrder order);

		// Token: 0x06005DD5 RID: 24021 RVA: 0x0014448C File Offset: 0x0014268C
		protected virtual T VisitCrossJoin(CrossJoinSet crossJoin)
		{
			T[] array = new T[crossJoin.Sets.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.Visit(crossJoin.Sets[i]);
			}
			return this.NewCrossJoin(array);
		}

		// Token: 0x06005DD6 RID: 24022 RVA: 0x001444D1 File Offset: 0x001426D1
		protected virtual T VisitDescendTo(DescendToSet descendTo)
		{
			return this.NewDescendTo(this.Visit(descendTo.Set), descendTo.Set.Dimensionality, descendTo.Dimensionality);
		}

		// Token: 0x06005DD7 RID: 24023 RVA: 0x001444F6 File Offset: 0x001426F6
		protected virtual T VisitDistinct(DistinctSet distinct)
		{
			return this.NewDistinct(this.Visit(distinct.Set));
		}

		// Token: 0x06005DD8 RID: 24024 RVA: 0x0014450A File Offset: 0x0014270A
		protected virtual T VisitEverything(EverythingSet everything)
		{
			return this.NewEverything();
		}

		// Token: 0x06005DD9 RID: 24025 RVA: 0x00144512 File Offset: 0x00142712
		protected virtual T VisitExcept(ExceptSet except)
		{
			return this.NewExcept(this.Visit(except.Set), this.Visit(except._Except));
		}

		// Token: 0x06005DDA RID: 24026 RVA: 0x00144534 File Offset: 0x00142734
		protected virtual T VisitFilter(FilterSet filter)
		{
			T t = this.Visit(filter.Set);
			return this.NewFilter(t, this.VisitFilter(t, filter.Predicate));
		}

		// Token: 0x06005DDB RID: 24027 RVA: 0x00144562 File Offset: 0x00142762
		protected virtual T VisitIntersect(IntersectSet intersect)
		{
			return this.NewIntersect(this.Visit(intersect.Set1), this.Visit(intersect.Set2));
		}

		// Token: 0x06005DDC RID: 24028 RVA: 0x00144582 File Offset: 0x00142782
		protected virtual T VisitLevel(LevelSet level)
		{
			return this.NewLevel(level.Level);
		}

		// Token: 0x06005DDD RID: 24029 RVA: 0x00144590 File Offset: 0x00142790
		protected virtual T VisitMember(MemberSet member)
		{
			return this.NewMember(member.Level, member.Member);
		}

		// Token: 0x06005DDE RID: 24030 RVA: 0x001445A4 File Offset: 0x001427A4
		protected virtual T VisitOrderBy(OrderBySet orderBy)
		{
			T t = this.Visit(orderBy.Set);
			O[] array = new O[orderBy.Order.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.VisitOrder(t, orderBy.Order[i]);
			}
			return this.NewOrderBy(t, array);
		}

		// Token: 0x06005DDF RID: 24031 RVA: 0x001445FF File Offset: 0x001427FF
		protected virtual T VisitOrderHierarchies(OrderHierarchiesSet orderHierarchies)
		{
			return this.NewOrderHierarchies(this.Visit(orderHierarchies.Set), orderHierarchies.Set.Dimensionality, orderHierarchies.Dimensionality);
		}

		// Token: 0x06005DE0 RID: 24032 RVA: 0x00144624 File Offset: 0x00142824
		protected virtual T VisitProject(ProjectSet project)
		{
			T t = this.Visit(project.Set);
			return this.NewProject(t, project.Objects);
		}

		// Token: 0x06005DE1 RID: 24033 RVA: 0x0014464B File Offset: 0x0014284B
		protected virtual T VisitSkipTake(SkipTakeSet skipTake)
		{
			return this.NewSkipTake(this.Visit(skipTake.Set), skipTake.RowRange);
		}

		// Token: 0x06005DE2 RID: 24034 RVA: 0x00144668 File Offset: 0x00142868
		protected virtual T VisitUnion(UnionSet union)
		{
			T[] array = new T[union.Sets.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.Visit(union.Sets[i]);
			}
			return this.NewUnion(array);
		}
	}
}
