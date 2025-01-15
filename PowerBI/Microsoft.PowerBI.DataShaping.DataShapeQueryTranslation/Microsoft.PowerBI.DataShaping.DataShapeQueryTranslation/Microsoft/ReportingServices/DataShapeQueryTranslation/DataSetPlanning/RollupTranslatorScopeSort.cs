using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x0200010C RID: 268
	internal sealed class RollupTranslatorScopeSort
	{
		// Token: 0x06000A5B RID: 2651 RVA: 0x00028297 File Offset: 0x00026497
		private RollupTranslatorScopeSort(ReadOnlyCollection<ScopePlanElement> scopeElements, DataShapeAnnotations annotations, ScopeTree scopeTree)
		{
			this.m_scopeElements = scopeElements;
			this.m_annotations = annotations;
			this.m_scopeTree = scopeTree;
			this.m_sortedScopes = new List<ScopeElementWithDistances>();
			this.m_dataShapeInfos = new Dictionary<DataShape, RollupTranslatorScopeSort.DataShapeInfo>();
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x000282CA File Offset: 0x000264CA
		public static List<ScopeElementWithDistances> SortScopes(ReadOnlyCollection<ScopePlanElement> scopeElements, DataShapeAnnotations annotations, ScopeTree scopeTree)
		{
			return new RollupTranslatorScopeSort(scopeElements, annotations, scopeTree).Sort();
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000282DC File Offset: 0x000264DC
		private List<ScopeElementWithDistances> Sort()
		{
			this.BuildDataShapeInfos();
			foreach (DataShapePlanElement dataShapePlanElement in this.m_scopeElements.OfType<DataShapePlanElement>())
			{
				DataShape dataShape = (DataShape)dataShapePlanElement.Scope;
				RollupTranslatorScopeSort.DataShapeInfo dataShapeInfo = this.m_dataShapeInfos[dataShape];
				dataShapeInfo.ComputeDistances(this.m_scopeTree);
				List<ScopeElementWithDistances> list = dataShapeInfo.Sort();
				if (this.m_sortedScopes.Count == 0)
				{
					this.m_sortedScopes.AddRange(list);
				}
				else
				{
					IScope parentScope = this.m_scopeTree.GetParentScope(dataShape);
					int num = this.m_sortedScopes.FindIndex((ScopeElementWithDistances s) => this.m_scopeTree.AreSameScope(s.ScopeElement.Scope, parentScope));
					this.m_sortedScopes.InsertRange(num + 1, list);
				}
			}
			return this.m_sortedScopes;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000283C4 File Offset: 0x000265C4
		private void BuildDataShapeInfos()
		{
			for (int i = 0; i < this.m_scopeElements.Count; i++)
			{
				ScopePlanElement scopePlanElement = this.m_scopeElements[i];
				DataShape containingDataShape = this.m_annotations.GetContainingDataShape(scopePlanElement.Scope);
				RollupTranslatorScopeSort.DataShapeInfo dataShapeInfo;
				if (!this.m_dataShapeInfos.TryGetValue(containingDataShape, out dataShapeInfo))
				{
					dataShapeInfo = new RollupTranslatorScopeSort.DataShapeInfo(containingDataShape.PrimaryHierarchy.GetAllDynamicMembers().ToList<DataMember>(), containingDataShape.SecondaryHierarchy.GetAllDynamicMembers().ToList<DataMember>());
					this.m_dataShapeInfos[containingDataShape] = dataShapeInfo;
				}
				dataShapeInfo.AddScopeElement(scopePlanElement, i);
			}
		}

		// Token: 0x04000512 RID: 1298
		private readonly ReadOnlyCollection<ScopePlanElement> m_scopeElements;

		// Token: 0x04000513 RID: 1299
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x04000514 RID: 1300
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000515 RID: 1301
		private readonly List<ScopeElementWithDistances> m_sortedScopes;

		// Token: 0x04000516 RID: 1302
		private readonly Dictionary<DataShape, RollupTranslatorScopeSort.DataShapeInfo> m_dataShapeInfos;

		// Token: 0x020002CC RID: 716
		private sealed class DataShapeInfo
		{
			// Token: 0x06001655 RID: 5717 RVA: 0x000516BA File Offset: 0x0004F8BA
			internal DataShapeInfo(List<DataMember> primaryGroups, List<DataMember> secondaryGroups)
			{
				this.m_primaryGroups = primaryGroups;
				this.m_secondaryGroups = secondaryGroups;
				this.m_scopeElements = new List<ScopeElementWithDistances>();
			}

			// Token: 0x170003F2 RID: 1010
			// (get) Token: 0x06001656 RID: 5718 RVA: 0x000516DB File Offset: 0x0004F8DB
			public List<DataMember> PrimaryGroups
			{
				get
				{
					return this.m_primaryGroups;
				}
			}

			// Token: 0x170003F3 RID: 1011
			// (get) Token: 0x06001657 RID: 5719 RVA: 0x000516E3 File Offset: 0x0004F8E3
			public List<DataMember> SecondaryGroups
			{
				get
				{
					return this.m_secondaryGroups;
				}
			}

			// Token: 0x06001658 RID: 5720 RVA: 0x000516EC File Offset: 0x0004F8EC
			public void AddScopeElement(ScopePlanElement element, int indexInPlan)
			{
				ScopeElementWithDistances scopeElementWithDistances = new ScopeElementWithDistances(element, indexInPlan);
				this.m_scopeElements.Add(scopeElementWithDistances);
			}

			// Token: 0x06001659 RID: 5721 RVA: 0x00051710 File Offset: 0x0004F910
			public void ComputeDistances(ScopeTree scopeTree)
			{
				foreach (ScopeElementWithDistances scopeElementWithDistances in this.m_scopeElements)
				{
					IScope scope = scopeElementWithDistances.ScopeElement.Scope;
					switch (scope.ObjectType)
					{
					case ObjectType.DataIntersection:
					{
						DataIntersection dataIntersection = (DataIntersection)scope;
						IScope primaryParent = scopeTree.GetPrimaryParentScope(dataIntersection);
						int num = this.m_primaryGroups.FindIndex((DataMember g) => scopeTree.AreSameScope(g, primaryParent));
						scopeElementWithDistances.PrimaryDistance = num + 2;
						IScope secondaryParent = scopeTree.GetSecondaryParentScope(dataIntersection);
						int num2 = this.m_secondaryGroups.FindIndex((DataMember g) => scopeTree.AreSameScope(g, secondaryParent));
						scopeElementWithDistances.SecondaryDistance = num2 + 2;
						continue;
					}
					case ObjectType.DataMember:
					{
						int num3 = this.m_primaryGroups.FindIndex((DataMember g) => scopeTree.AreSameScope(g, scope));
						if (num3 < 0)
						{
							scopeElementWithDistances.PrimaryDistance = 0;
							int num4 = this.m_secondaryGroups.FindIndex((DataMember g) => scopeTree.AreSameScope(g, scope));
							scopeElementWithDistances.SecondaryDistance = num4 + 1;
							continue;
						}
						scopeElementWithDistances.PrimaryDistance = num3 + 1;
						scopeElementWithDistances.SecondaryDistance = 0;
						continue;
					}
					case ObjectType.DataShape:
						scopeElementWithDistances.PrimaryDistance = 0;
						scopeElementWithDistances.SecondaryDistance = 0;
						continue;
					}
					scopeElementWithDistances.PrimaryDistance = -1;
					scopeElementWithDistances.SecondaryDistance = -1;
				}
			}

			// Token: 0x0600165A RID: 5722 RVA: 0x000518F8 File Offset: 0x0004FAF8
			public List<ScopeElementWithDistances> Sort()
			{
				List<ScopeElementWithDistances> scopeElements = this.m_scopeElements;
				Comparison<ScopeElementWithDistances> comparison;
				if ((comparison = RollupTranslatorScopeSort.DataShapeInfo.<>O.<0>__CompareScopesByDistances) == null)
				{
					comparison = (RollupTranslatorScopeSort.DataShapeInfo.<>O.<0>__CompareScopesByDistances = new Comparison<ScopeElementWithDistances>(RollupTranslatorScopeSort.DataShapeInfo.CompareScopesByDistances));
				}
				scopeElements.Sort(comparison);
				return this.m_scopeElements;
			}

			// Token: 0x0600165B RID: 5723 RVA: 0x00051928 File Offset: 0x0004FB28
			private static int CompareScopesByDistances(ScopeElementWithDistances x, ScopeElementWithDistances y)
			{
				if (x.SecondaryDistance < y.SecondaryDistance)
				{
					return 1;
				}
				if (x.SecondaryDistance > y.SecondaryDistance)
				{
					return -1;
				}
				return y.PrimaryDistance.CompareTo(x.PrimaryDistance);
			}

			// Token: 0x04000A89 RID: 2697
			private readonly List<DataMember> m_primaryGroups;

			// Token: 0x04000A8A RID: 2698
			private readonly List<DataMember> m_secondaryGroups;

			// Token: 0x04000A8B RID: 2699
			private readonly List<ScopeElementWithDistances> m_scopeElements;

			// Token: 0x02000334 RID: 820
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x04000B92 RID: 2962
				public static Comparison<ScopeElementWithDistances> <0>__CompareScopesByDistances;
			}
		}
	}
}
