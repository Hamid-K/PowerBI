using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005A4 RID: 1444
	internal class LeftCellWrapper : InternalBase
	{
		// Token: 0x06004604 RID: 17924 RVA: 0x000F6FDC File Offset: 0x000F51DC
		internal LeftCellWrapper(ViewTarget viewTarget, Set<MemberPath> attrs, FragmentQuery fragmentQuery, CellQuery leftCellQuery, CellQuery rightCellQuery, MemberMaps memberMaps, IEnumerable<Cell> inputCells)
		{
			this.m_leftFragmentQuery = fragmentQuery;
			this.m_rightCellQuery = rightCellQuery;
			this.m_leftCellQuery = leftCellQuery;
			this.m_attributes = attrs;
			this.m_viewTarget = viewTarget;
			this.m_memberMaps = memberMaps;
			this.m_mergedCells = new HashSet<Cell>(inputCells);
		}

		// Token: 0x06004605 RID: 17925 RVA: 0x000F702C File Offset: 0x000F522C
		internal LeftCellWrapper(ViewTarget viewTarget, Set<MemberPath> attrs, FragmentQuery fragmentQuery, CellQuery leftCellQuery, CellQuery rightCellQuery, MemberMaps memberMaps, Cell inputCell)
			: this(viewTarget, attrs, fragmentQuery, leftCellQuery, rightCellQuery, memberMaps, Enumerable.Repeat<Cell>(inputCell, 1))
		{
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06004606 RID: 17926 RVA: 0x000F7050 File Offset: 0x000F5250
		internal FragmentQuery FragmentQuery
		{
			get
			{
				return this.m_leftFragmentQuery;
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06004607 RID: 17927 RVA: 0x000F7058 File Offset: 0x000F5258
		internal Set<MemberPath> Attributes
		{
			get
			{
				return this.m_attributes;
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06004608 RID: 17928 RVA: 0x000F7060 File Offset: 0x000F5260
		internal string OriginalCellNumberString
		{
			get
			{
				return StringUtil.ToSeparatedString(this.m_mergedCells.Select((Cell cell) => cell.CellNumberAsString), "+", "");
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06004609 RID: 17929 RVA: 0x000F709B File Offset: 0x000F529B
		internal MemberDomainMap RightDomainMap
		{
			get
			{
				return this.m_memberMaps.RightDomainMap;
			}
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x000F70A8 File Offset: 0x000F52A8
		[Conditional("DEBUG")]
		internal void AssertHasUniqueCell()
		{
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x0600460B RID: 17931 RVA: 0x000F70AA File Offset: 0x000F52AA
		internal IEnumerable<Cell> Cells
		{
			get
			{
				return this.m_mergedCells;
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x0600460C RID: 17932 RVA: 0x000F70B2 File Offset: 0x000F52B2
		internal Cell OnlyInputCell
		{
			get
			{
				return this.m_mergedCells.First<Cell>();
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x0600460D RID: 17933 RVA: 0x000F70BF File Offset: 0x000F52BF
		internal CellQuery RightCellQuery
		{
			get
			{
				return this.m_rightCellQuery;
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x0600460E RID: 17934 RVA: 0x000F70C7 File Offset: 0x000F52C7
		internal CellQuery LeftCellQuery
		{
			get
			{
				return this.m_leftCellQuery;
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x0600460F RID: 17935 RVA: 0x000F70CF File Offset: 0x000F52CF
		internal EntitySetBase LeftExtent
		{
			get
			{
				return this.m_mergedCells.First<Cell>().GetLeftQuery(this.m_viewTarget).Extent;
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06004610 RID: 17936 RVA: 0x000F70EC File Offset: 0x000F52EC
		internal EntitySetBase RightExtent
		{
			get
			{
				return this.m_rightCellQuery.Extent;
			}
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x000F70F9 File Offset: 0x000F52F9
		internal static IEnumerable<Cell> GetInputCellsForWrappers(IEnumerable<LeftCellWrapper> wrappers)
		{
			foreach (LeftCellWrapper leftCellWrapper in wrappers)
			{
				foreach (Cell cell in leftCellWrapper.m_mergedCells)
				{
					yield return cell;
				}
				HashSet<Cell>.Enumerator enumerator2 = default(HashSet<Cell>.Enumerator);
			}
			IEnumerator<LeftCellWrapper> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x000F710C File Offset: 0x000F530C
		internal RoleBoolean CreateRoleBoolean()
		{
			if (this.RightExtent is AssociationSet)
			{
				Set<AssociationEndMember> endsForTablePrimaryKey = this.GetEndsForTablePrimaryKey();
				if (endsForTablePrimaryKey.Count == 1)
				{
					return new RoleBoolean(((AssociationSet)this.RightExtent).AssociationSetEnds[endsForTablePrimaryKey.First<AssociationEndMember>().Name]);
				}
			}
			return new RoleBoolean(this.RightExtent);
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x000F7168 File Offset: 0x000F5368
		internal static string GetExtentListAsUserString(IEnumerable<LeftCellWrapper> wrappers)
		{
			Set<EntitySetBase> set = new Set<EntitySetBase>(EqualityComparer<EntitySetBase>.Default);
			foreach (LeftCellWrapper leftCellWrapper in wrappers)
			{
				set.Add(leftCellWrapper.RightExtent);
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (EntitySetBase entitySetBase in set)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append(entitySetBase.Name);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x000F7228 File Offset: 0x000F5428
		internal override void ToFullString(StringBuilder builder)
		{
			builder.Append("P[");
			StringUtil.ToSeparatedString(builder, this.m_attributes, ",");
			builder.Append("] = ");
			this.m_rightCellQuery.ToFullString(builder);
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x000F725F File Offset: 0x000F545F
		internal override void ToCompactString(StringBuilder stringBuilder)
		{
			stringBuilder.Append(this.OriginalCellNumberString);
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x000F7270 File Offset: 0x000F5470
		internal static void WrappersToStringBuilder(StringBuilder builder, List<LeftCellWrapper> wrappers, string header)
		{
			builder.AppendLine().Append(header).AppendLine();
			LeftCellWrapper[] array = wrappers.ToArray();
			Array.Sort<LeftCellWrapper>(array, LeftCellWrapper.OriginalCellIdComparer);
			foreach (LeftCellWrapper leftCellWrapper in array)
			{
				leftCellWrapper.ToCompactString(builder);
				builder.Append(" = ");
				leftCellWrapper.ToFullString(builder);
				builder.AppendLine();
			}
		}

		// Token: 0x06004617 RID: 17943 RVA: 0x000F72D4 File Offset: 0x000F54D4
		private Set<AssociationEndMember> GetEndsForTablePrimaryKey()
		{
			CellQuery rightCellQuery = this.RightCellQuery;
			Set<AssociationEndMember> set = new Set<AssociationEndMember>(EqualityComparer<AssociationEndMember>.Default);
			foreach (int num in this.m_memberMaps.ProjectedSlotMap.KeySlots)
			{
				AssociationEndMember associationEndMember = (AssociationEndMember)((MemberProjectedSlot)rightCellQuery.ProjectedSlotAt(num)).MemberPath.RootEdmMember;
				set.Add(associationEndMember);
			}
			return set;
		}

		// Token: 0x06004618 RID: 17944 RVA: 0x000F735C File Offset: 0x000F555C
		internal MemberProjectedSlot GetLeftSideMappedSlotForRightSideMember(MemberPath member)
		{
			int projectedPosition = this.RightCellQuery.GetProjectedPosition(new MemberProjectedSlot(member));
			if (projectedPosition == -1)
			{
				return null;
			}
			ProjectedSlot projectedSlot = this.LeftCellQuery.ProjectedSlotAt(projectedPosition);
			if (projectedSlot == null || projectedSlot is ConstantProjectedSlot)
			{
				return null;
			}
			return projectedSlot as MemberProjectedSlot;
		}

		// Token: 0x06004619 RID: 17945 RVA: 0x000F73A4 File Offset: 0x000F55A4
		internal MemberProjectedSlot GetRightSideMappedSlotForLeftSideMember(MemberPath member)
		{
			int projectedPosition = this.LeftCellQuery.GetProjectedPosition(new MemberProjectedSlot(member));
			if (projectedPosition == -1)
			{
				return null;
			}
			ProjectedSlot projectedSlot = this.RightCellQuery.ProjectedSlotAt(projectedPosition);
			if (projectedSlot == null || projectedSlot is ConstantProjectedSlot)
			{
				return null;
			}
			return projectedSlot as MemberProjectedSlot;
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x000F73E9 File Offset: 0x000F55E9
		internal MemberProjectedSlot GetCSideMappedSlotForSMember(MemberPath member)
		{
			if (this.m_viewTarget == ViewTarget.QueryView)
			{
				return this.GetLeftSideMappedSlotForRightSideMember(member);
			}
			return this.GetRightSideMappedSlotForLeftSideMember(member);
		}

		// Token: 0x04001905 RID: 6405
		internal static readonly IEqualityComparer<LeftCellWrapper> BoolEqualityComparer = new LeftCellWrapper.BoolWrapperComparer();

		// Token: 0x04001906 RID: 6406
		private readonly Set<MemberPath> m_attributes;

		// Token: 0x04001907 RID: 6407
		private readonly MemberMaps m_memberMaps;

		// Token: 0x04001908 RID: 6408
		private readonly CellQuery m_leftCellQuery;

		// Token: 0x04001909 RID: 6409
		private readonly CellQuery m_rightCellQuery;

		// Token: 0x0400190A RID: 6410
		private readonly HashSet<Cell> m_mergedCells;

		// Token: 0x0400190B RID: 6411
		private readonly ViewTarget m_viewTarget;

		// Token: 0x0400190C RID: 6412
		private readonly FragmentQuery m_leftFragmentQuery;

		// Token: 0x0400190D RID: 6413
		internal static readonly IComparer<LeftCellWrapper> Comparer = new LeftCellWrapper.LeftCellWrapperComparer();

		// Token: 0x0400190E RID: 6414
		internal static readonly IComparer<LeftCellWrapper> OriginalCellIdComparer = new LeftCellWrapper.CellIdComparer();

		// Token: 0x02000BCF RID: 3023
		private class BoolWrapperComparer : IEqualityComparer<LeftCellWrapper>
		{
			// Token: 0x06006804 RID: 26628 RVA: 0x00162BD8 File Offset: 0x00160DD8
			public bool Equals(LeftCellWrapper left, LeftCellWrapper right)
			{
				if (left == right)
				{
					return true;
				}
				if (left == null || right == null)
				{
					return false;
				}
				bool flag = BoolExpression.EqualityComparer.Equals(left.RightCellQuery.WhereClause, right.RightCellQuery.WhereClause);
				return left.RightExtent.Equals(right.RightExtent) && flag;
			}

			// Token: 0x06006805 RID: 26629 RVA: 0x00162C27 File Offset: 0x00160E27
			public int GetHashCode(LeftCellWrapper wrapper)
			{
				return BoolExpression.EqualityComparer.GetHashCode(wrapper.RightCellQuery.WhereClause) ^ wrapper.RightExtent.GetHashCode();
			}
		}

		// Token: 0x02000BD0 RID: 3024
		private class LeftCellWrapperComparer : IComparer<LeftCellWrapper>
		{
			// Token: 0x06006807 RID: 26631 RVA: 0x00162C54 File Offset: 0x00160E54
			public int Compare(LeftCellWrapper x, LeftCellWrapper y)
			{
				if (x.FragmentQuery.Attributes.Count > y.FragmentQuery.Attributes.Count)
				{
					return -1;
				}
				if (x.FragmentQuery.Attributes.Count < y.FragmentQuery.Attributes.Count)
				{
					return 1;
				}
				return string.CompareOrdinal(x.OriginalCellNumberString, y.OriginalCellNumberString);
			}
		}

		// Token: 0x02000BD1 RID: 3025
		internal class CellIdComparer : IComparer<LeftCellWrapper>
		{
			// Token: 0x06006809 RID: 26633 RVA: 0x00162CC2 File Offset: 0x00160EC2
			public int Compare(LeftCellWrapper x, LeftCellWrapper y)
			{
				return StringComparer.Ordinal.Compare(x.OriginalCellNumberString, y.OriginalCellNumberString);
			}
		}
	}
}
