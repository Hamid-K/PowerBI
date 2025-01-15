using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x02000253 RID: 595
	internal sealed class SubtotalSortDirectionAnalyzer
	{
		// Token: 0x06001490 RID: 5264 RVA: 0x0004E97A File Offset: 0x0004CB7A
		private SubtotalSortDirectionAnalyzer(TranslationErrorContext errorContext, DataMemberAnnotations dataMemberAnnotations, DataShape owningDataShape, List<IIdentifiable> parents, int currentRowIndex, int currentColumnIndex)
		{
			this.m_errorContext = errorContext;
			this.m_dataMemberAnnotations = dataMemberAnnotations;
			this.m_owningDataShape = owningDataShape;
			this.m_parents = parents;
			this.m_currentRowIndex = currentRowIndex;
			this.m_currentColumnIndex = currentColumnIndex;
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0004E9AF File Offset: 0x0004CBAF
		public static SortDirection? ComputeSubtotalSortDirection(TranslationErrorContext errorContext, DataMemberAnnotations dataMemberAnnotations, DataShape owningDataShape, List<IIdentifiable> parents, int currentRowIndex, int currentColumnIndex, out IScope rollupParent)
		{
			return new SubtotalSortDirectionAnalyzer(errorContext, dataMemberAnnotations, owningDataShape, parents, currentRowIndex, currentColumnIndex).ComputeSubtotalSortDirection(out rollupParent);
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0004E9C8 File Offset: 0x0004CBC8
		private SortDirection? ComputeSubtotalSortDirection(out IScope rollupParent)
		{
			SortDirection? sortDirection = null;
			IIdentifiable identifiable = this.m_parents[0];
			switch (identifiable.ObjectType)
			{
			case ObjectType.DataIntersection:
				return this.GetStaticMemberPositionRelativeToPeerDynamic(this.m_owningDataShape, false, this.m_currentColumnIndex, out rollupParent);
			case ObjectType.DataMember:
			{
				DataMember dataMember = (DataMember)identifiable;
				if (dataMember.IsDynamic)
				{
					sortDirection = new SortDirection?(SortDirection.Descending);
					rollupParent = dataMember;
					return sortDirection;
				}
				IIdentifiable parent = this.GetParent(dataMember);
				return this.GetStaticMemberPositionRelativeToPeerDynamic(dataMember, (IScope)parent, out rollupParent);
			}
			case ObjectType.DataShape:
				sortDirection = new SortDirection?(SortDirection.Descending);
				rollupParent = (DataShape)identifiable;
				return sortDirection;
			}
			Contract.RetailAssert(false, "Calculations are only supported on data shapes, members and intersections");
			throw new InvalidOperationException("Calculations are only supported on data shapes, members and intersections");
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0004EA88 File Offset: 0x0004CC88
		private SortDirection? GetStaticMemberPositionRelativeToPeerDynamic(DataShape dataShape, bool primaryMember, int leafIndex, out IScope rollupParent)
		{
			IScope scope;
			DataMember leafMember = dataShape.GetLeafMember(this.m_dataMemberAnnotations, primaryMember, leafIndex, out scope);
			if (leafMember.IsDynamic)
			{
				rollupParent = null;
				return null;
			}
			return this.GetStaticMemberPositionRelativeToPeerDynamic(leafMember, scope, out rollupParent);
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0004EAC8 File Offset: 0x0004CCC8
		private SortDirection? GetStaticMemberPositionRelativeToPeerDynamic(DataMember dataMember, IScope dataMemberParentScope, out IScope rollupParent)
		{
			List<DataMember> list;
			if (dataMemberParentScope.ObjectType == ObjectType.DataShape)
			{
				list = TraversalExtensions.GetPeerMembers((DataShape)dataMemberParentScope, this.m_dataMemberAnnotations.IsPrimaryMember(dataMember));
			}
			else
			{
				list = TraversalExtensions.GetPeerMembers((DataMember)dataMemberParentScope);
			}
			rollupParent = null;
			bool flag = false;
			int i;
			for (i = 0; i < list.Count; i++)
			{
				DataMember dataMember2 = list[i];
				if (dataMember2 == dataMember)
				{
					flag = true;
				}
				else if (dataMember2.IsDynamic)
				{
					rollupParent = dataMember2;
					break;
				}
			}
			if (i < list.Count)
			{
				return new SortDirection?(flag ? SortDirection.Descending : SortDirection.Ascending);
			}
			return null;
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0004EB58 File Offset: 0x0004CD58
		private IIdentifiable GetParent(IIdentifiable identifiable)
		{
			return this.m_parents.SkipWhile((IIdentifiable m) => m != identifiable).Skip(1).FirstOrDefault<IIdentifiable>();
		}

		// Token: 0x04000925 RID: 2341
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000926 RID: 2342
		private readonly DataMemberAnnotations m_dataMemberAnnotations;

		// Token: 0x04000927 RID: 2343
		private readonly DataShape m_owningDataShape;

		// Token: 0x04000928 RID: 2344
		private readonly List<IIdentifiable> m_parents;

		// Token: 0x04000929 RID: 2345
		private readonly int m_currentRowIndex;

		// Token: 0x0400092A RID: 2346
		private readonly int m_currentColumnIndex;
	}
}
