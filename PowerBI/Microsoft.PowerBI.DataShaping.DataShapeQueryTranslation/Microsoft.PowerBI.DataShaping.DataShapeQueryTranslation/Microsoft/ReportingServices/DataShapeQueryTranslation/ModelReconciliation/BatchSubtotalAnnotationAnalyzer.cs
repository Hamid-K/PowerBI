using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ModelReconciliation
{
	// Token: 0x0200009C RID: 156
	internal sealed class BatchSubtotalAnnotationAnalyzer
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x0001B6FC File Offset: 0x000198FC
		internal BatchSubtotalAnnotationAnalyzer(ScopeTree scopeTree)
		{
			this.m_scopeTree = scopeTree;
			this.m_batchSubtotalAnnotations = new WritableBatchSubtotalAnnotations();
			this.m_dsContexts = new Stack<BatchSubtotalAnnotationAnalyzer.DataShapeSubtotalContext>();
			this.m_sortCoordinates = new Dictionary<DataShape, Dictionary<Calculation, BatchSubtotalAnnotationAnalyzer.SortCoordinate>>();
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0001B72C File Offset: 0x0001992C
		internal BatchSubtotalAnnotations SubtotalAnnotations
		{
			get
			{
				return this.m_batchSubtotalAnnotations;
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001B734 File Offset: 0x00019934
		internal void EnterDataShape(DataShape dataShape)
		{
			Dictionary<Calculation, BatchSubtotalAnnotationAnalyzer.SortCoordinate> dictionary = BatchSubtotalAnnotationAnalyzer.SubtotalSortCoordinatesAnalyzer.Analyze(dataShape);
			this.m_sortCoordinates.Add(dataShape, dictionary);
			this.m_dsContexts.Push(new BatchSubtotalAnnotationAnalyzer.DataShapeSubtotalContext(dataShape));
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001B766 File Offset: 0x00019966
		internal void ExitDataShape(DataShape dataShape)
		{
			this.m_dsContexts.Pop();
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001B774 File Offset: 0x00019974
		private BatchSubtotalAnnotationAnalyzer.DataShapeSubtotalContext Context
		{
			get
			{
				return this.m_dsContexts.Peek();
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001B781 File Offset: 0x00019981
		private DataShape DataShape
		{
			get
			{
				return this.Context.DataShape;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001B78E File Offset: 0x0001998E
		private IList<DataMember> AllPrimaryMembers
		{
			get
			{
				return this.Context.AllPrimaryMembers;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001B79B File Offset: 0x0001999B
		private IList<DataMember> AllSecondaryMembers
		{
			get
			{
				return this.Context.AllSecondaryMembers;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001B7A8 File Offset: 0x000199A8
		private IDictionary<Calculation, BatchSubtotalAnnotationAnalyzer.SortCoordinate> SubtotalSortCoordinates
		{
			get
			{
				return this.m_sortCoordinates[this.Context.DataShape];
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001B7C0 File Offset: 0x000199C0
		internal void AddBatchSubtotalAnnotation(Calculation calculation, Calculation referencedCalculation)
		{
			IScope containingScope = this.m_scopeTree.GetContainingScope(calculation);
			switch (containingScope.ObjectType)
			{
			case ObjectType.DataIntersection:
			{
				DataIntersection dataIntersection = (DataIntersection)containingScope;
				IScope scope = this.m_scopeTree.GetPrimaryParentScope(dataIntersection);
				this.AddBatchSubtotalAnnotation(calculation, referencedCalculation, scope, true);
				scope = this.m_scopeTree.GetSecondaryParentScope(dataIntersection);
				this.AddBatchSubtotalAnnotation(calculation, referencedCalculation, scope, false);
				return;
			}
			case ObjectType.DataMember:
			{
				DataMember dataMember = (DataMember)containingScope;
				DataShape parentDataShape = this.m_scopeTree.GetParentDataShape(dataMember);
				if (this.Context.AllPrimaryMembers.Contains(dataMember))
				{
					this.AddBatchSubtotalAnnotation(calculation, referencedCalculation, containingScope, true);
					this.AddBatchSubtotalAnnotation(calculation, referencedCalculation, parentDataShape, false);
					return;
				}
				this.AddBatchSubtotalAnnotation(calculation, referencedCalculation, parentDataShape, true);
				this.AddBatchSubtotalAnnotation(calculation, referencedCalculation, containingScope, false);
				return;
			}
			case ObjectType.DataShape:
				this.AddBatchSubtotalAnnotation(calculation, referencedCalculation, containingScope, true);
				this.AddBatchSubtotalAnnotation(calculation, referencedCalculation, containingScope, false);
				return;
			}
			Contract.RetailFail("Only data shapes, data members and interesections expected.");
			throw new InvalidOperationException("Only data shapes, data members and interesections expected.");
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001B8BC File Offset: 0x00019ABC
		internal void AddBatchExplicitSubtotalAnnotation(DataMember sourceMember)
		{
			IScope innermostScopeInDataShape = this.m_scopeTree.GetInnermostScopeInDataShape(this.DataShape);
			bool flag = this.Context.AllPrimaryMembers.Contains(sourceMember);
			IList<DataMember> list = (flag ? this.AllPrimaryMembers : this.AllSecondaryMembers);
			int num = -1;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].DataMembers != null && list[i].DataMembers.Contains(sourceMember))
				{
					num = i;
					break;
				}
			}
			IScope scope;
			List<DataMember> list2;
			if (num == -1)
			{
				scope = this.DataShape;
				list2 = TraversalExtensions.GetPeerMembers(this.DataShape, flag);
			}
			else
			{
				scope = list[num];
				list2 = TraversalExtensions.GetPeerMembers((DataMember)scope);
			}
			int num2 = -1;
			int num3 = -1;
			for (int j = 0; j < list2.Count; j++)
			{
				if (list2[j] == sourceMember)
				{
					num2 = j;
				}
				if (list2[j].IsDynamic)
				{
					num3 = j;
				}
				if (num2 >= 0 && num3 >= 0)
				{
					break;
				}
			}
			Contract.RetailAssert(num3 >= 0, "Expected to find one dynamic peer member for a context subtotal");
			DataMember dataMember = list2[num3];
			string subtotalIndicatorColumnName = BatchSubtotalAnnotations.GetSubtotalIndicatorColumnName(scope, flag, this.DataShape.Id.Value, this.m_dsContexts.Count);
			SortDirection sortDirection = ((num2 > num3) ? SortDirection.Ascending : SortDirection.Descending);
			this.AddBatchSubtotalAnnotation(sourceMember, innermostScopeInDataShape, dataMember, subtotalIndicatorColumnName, sortDirection, SubtotalUsage.Output);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001BA20 File Offset: 0x00019C20
		private void AddBatchSubtotalAnnotation(Calculation subtotalCalculation, Calculation referencedCalculation, IScope rollupParentScope, bool primary)
		{
			IScope rollupStartScope = this.GetRollupStartScope(referencedCalculation, primary);
			if (rollupStartScope == null)
			{
				return;
			}
			IScope scope;
			if (this.TryGetChildScope(rollupParentScope, rollupStartScope, primary, out scope))
			{
				string subtotalIndicatorColumnName = BatchSubtotalAnnotations.GetSubtotalIndicatorColumnName(rollupParentScope, primary, this.DataShape.Id.Value, this.m_dsContexts.Count);
				SubtotalUsage subtotalUsageBasedOnParentScope = this.GetSubtotalUsageBasedOnParentScope(rollupParentScope, primary);
				SortDirection subtotalIndicatorColumnSortDirection = this.GetSubtotalIndicatorColumnSortDirection(subtotalCalculation, referencedCalculation, primary);
				IIdentifiable subtotalSource = this.GetSubtotalSource(subtotalCalculation, primary);
				this.AddBatchSubtotalAnnotation(subtotalSource, rollupStartScope, scope, subtotalIndicatorColumnName, subtotalIndicatorColumnSortDirection, subtotalUsageBasedOnParentScope);
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001BA9C File Offset: 0x00019C9C
		private SubtotalUsage GetSubtotalUsageBasedOnParentScope(IScope scope, bool primary)
		{
			DataMember dataMember = scope as DataMember;
			if (dataMember != null)
			{
				if (!dataMember.ContextOnly)
				{
					DataMember dataMember2 = dataMember.StaticChildDataMemberOrDefault();
					if (dataMember2 == null || dataMember2.ContextOnly)
					{
						DataMember dataMember3 = dataMember.DynamicChildDataMemberOrDefault();
						if (dataMember3 == null || !dataMember3.ContextOnly)
						{
							return SubtotalUsage.VisualCalculations;
						}
					}
					return SubtotalUsage.Output;
				}
			}
			else
			{
				DataShape dataShape = scope as DataShape;
				if (dataShape != null)
				{
					DataHierarchy dataHierarchy = (primary ? dataShape.PrimaryHierarchy : dataShape.SecondaryHierarchy);
					object obj;
					if (dataHierarchy == null)
					{
						obj = null;
					}
					else
					{
						List<DataMember> dataMembers = dataHierarchy.DataMembers;
						if (dataMembers == null)
						{
							obj = null;
						}
						else
						{
							obj = dataMembers.FirstOrDefault((DataMember d) => !d.IsDynamic);
						}
					}
					object obj2 = obj;
					if (obj2 != null && !obj2.ContextOnly)
					{
						return SubtotalUsage.Output;
					}
				}
			}
			return SubtotalUsage.VisualCalculations;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001BB54 File Offset: 0x00019D54
		private IScope GetRollupStartScope(Calculation referencedCalculation, bool primary)
		{
			IScope containingScope = this.m_scopeTree.GetContainingScope(referencedCalculation);
			switch (containingScope.ObjectType)
			{
			case ObjectType.DataIntersection:
			{
				DataIntersection dataIntersection = (DataIntersection)containingScope;
				if (!primary)
				{
					return this.m_scopeTree.GetSecondaryParentScope(dataIntersection);
				}
				return this.m_scopeTree.GetPrimaryParentScope(dataIntersection);
			}
			case ObjectType.DataMember:
			{
				DataMember dataMember = (DataMember)containingScope;
				bool flag = this.Context.AllPrimaryMembers.Contains(dataMember);
				if (primary == flag)
				{
					return dataMember;
				}
				return null;
			}
			case ObjectType.DataShape:
				return null;
			}
			Contract.RetailFail("Only data shapes, data members and interesections expected.");
			throw new InvalidOperationException("Only data shapes, data members and interesections expected.");
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001BBF4 File Offset: 0x00019DF4
		private SortDirection GetSubtotalIndicatorColumnSortDirection(Calculation subtotalCalculation, Calculation referencedCalculation, bool primary)
		{
			BatchSubtotalAnnotationAnalyzer.SortCoordinate sortCoordinate = this.SubtotalSortCoordinates[subtotalCalculation];
			BatchSubtotalAnnotationAnalyzer.SortCoordinate sortCoordinate2 = this.SubtotalSortCoordinates[referencedCalculation];
			if (!(primary ? (sortCoordinate.RowCoordinate <= sortCoordinate2.RowCoordinate) : (sortCoordinate.ColumnCoordinate <= sortCoordinate2.ColumnCoordinate)))
			{
				return SortDirection.Ascending;
			}
			return SortDirection.Descending;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001BC48 File Offset: 0x00019E48
		private IIdentifiable GetSubtotalSource(Calculation subtotalCalculation, bool primary)
		{
			BatchSubtotalAnnotationAnalyzer.SortCoordinate sortCoordinate = this.SubtotalSortCoordinates[subtotalCalculation];
			int num = (primary ? sortCoordinate.RowCoordinate : sortCoordinate.ColumnCoordinate);
			if (num < 0)
			{
				return this.DataShape;
			}
			if (!primary)
			{
				return this.AllSecondaryMembers[num];
			}
			return this.AllPrimaryMembers[num];
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001BC9C File Offset: 0x00019E9C
		private void AddBatchSubtotalAnnotation(IIdentifiable subtotalSource, IScope rollupStartScope, IScope rollupStopScope, string subtotalIndicatorColumnName, SortDirection sortDirection, SubtotalUsage usage)
		{
			BatchSubtotalAnnotation batchSubtotalAnnotation;
			if (!this.m_batchSubtotalAnnotations.TryGetSubtotalAnnotation(rollupStopScope, out batchSubtotalAnnotation))
			{
				batchSubtotalAnnotation = new BatchSubtotalAnnotation(rollupStartScope, rollupStopScope, subtotalIndicatorColumnName, sortDirection, usage);
				this.m_batchSubtotalAnnotations.AddSubtotalAnnotation(rollupStopScope, batchSubtotalAnnotation);
			}
			else
			{
				BatchSubtotalAnnotation batchSubtotalAnnotation2 = new BatchSubtotalAnnotation(rollupStartScope, rollupStopScope, subtotalIndicatorColumnName, sortDirection, usage);
				batchSubtotalAnnotation.Validate(batchSubtotalAnnotation2);
			}
			if (!this.m_batchSubtotalAnnotations.ContainsSubtotalSourceAnnotation(subtotalSource))
			{
				this.m_batchSubtotalAnnotations.AddSubtotalSourceAnnotation(subtotalSource, batchSubtotalAnnotation);
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001BD08 File Offset: 0x00019F08
		private bool TryGetChildScope(IScope parentStopScope, IScope startScope, bool primary, out IScope childScope)
		{
			IScope scope = startScope;
			for (;;)
			{
				childScope = scope;
				scope = (primary ? this.m_scopeTree.GetPrimaryParentScope(childScope) : this.m_scopeTree.GetSecondaryParentScope(childScope));
				if (scope == null)
				{
					break;
				}
				if (scope == parentStopScope)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400037C RID: 892
		private readonly ScopeTree m_scopeTree;

		// Token: 0x0400037D RID: 893
		private readonly WritableBatchSubtotalAnnotations m_batchSubtotalAnnotations;

		// Token: 0x0400037E RID: 894
		private readonly Stack<BatchSubtotalAnnotationAnalyzer.DataShapeSubtotalContext> m_dsContexts;

		// Token: 0x0400037F RID: 895
		private readonly Dictionary<DataShape, Dictionary<Calculation, BatchSubtotalAnnotationAnalyzer.SortCoordinate>> m_sortCoordinates;

		// Token: 0x020002A2 RID: 674
		internal sealed class SortCoordinate
		{
			// Token: 0x060015BE RID: 5566 RVA: 0x0005061C File Offset: 0x0004E81C
			internal SortCoordinate(int rowCoordinate, int columnCoordinate)
			{
				this.m_rowCoordinate = rowCoordinate;
				this.m_columnCoordinate = columnCoordinate;
			}

			// Token: 0x170003E1 RID: 993
			// (get) Token: 0x060015BF RID: 5567 RVA: 0x00050632 File Offset: 0x0004E832
			internal int RowCoordinate
			{
				get
				{
					return this.m_rowCoordinate;
				}
			}

			// Token: 0x170003E2 RID: 994
			// (get) Token: 0x060015C0 RID: 5568 RVA: 0x0005063A File Offset: 0x0004E83A
			internal int ColumnCoordinate
			{
				get
				{
					return this.m_columnCoordinate;
				}
			}

			// Token: 0x04000A26 RID: 2598
			private readonly int m_rowCoordinate;

			// Token: 0x04000A27 RID: 2599
			private readonly int m_columnCoordinate;
		}

		// Token: 0x020002A3 RID: 675
		private sealed class SubtotalSortCoordinatesAnalyzer : DataShapeVisitor
		{
			// Token: 0x060015C1 RID: 5569 RVA: 0x00050642 File Offset: 0x0004E842
			private SubtotalSortCoordinatesAnalyzer()
			{
				this.m_calculationIndices = new Dictionary<Calculation, BatchSubtotalAnnotationAnalyzer.SortCoordinate>();
				this.m_memberSortIndices = new Dictionary<DataMember, int>();
				this.m_parents = new Stack<IIdentifiable>();
			}

			// Token: 0x060015C2 RID: 5570 RVA: 0x00050680 File Offset: 0x0004E880
			internal static Dictionary<Calculation, BatchSubtotalAnnotationAnalyzer.SortCoordinate> Analyze(DataShape dataShape)
			{
				BatchSubtotalAnnotationAnalyzer.SubtotalSortCoordinatesAnalyzer subtotalSortCoordinatesAnalyzer = new BatchSubtotalAnnotationAnalyzer.SubtotalSortCoordinatesAnalyzer();
				subtotalSortCoordinatesAnalyzer.Visit(dataShape);
				return subtotalSortCoordinatesAnalyzer.m_calculationIndices;
			}

			// Token: 0x060015C3 RID: 5571 RVA: 0x00050694 File Offset: 0x0004E894
			protected override void Visit(DataShape dataShape)
			{
				int currentRowIndex = this.m_currentRowIndex;
				int currentColumnIndex = this.m_currentColumnIndex;
				int memberIndex = this.m_memberIndex;
				List<DataMember> primaryLeafMembers = this.m_primaryLeafMembers;
				List<DataMember> secondaryLeafMembers = this.m_secondaryLeafMembers;
				this.m_currentRowIndex = -1;
				this.m_currentColumnIndex = -1;
				this.m_memberIndex = -1;
				this.m_primaryLeafMembers = new List<DataMember>();
				this.m_secondaryLeafMembers = new List<DataMember>();
				base.Visit(dataShape);
				this.m_currentRowIndex = currentRowIndex;
				this.m_currentColumnIndex = currentColumnIndex;
				this.m_memberIndex = memberIndex;
				this.m_primaryLeafMembers = primaryLeafMembers;
				this.m_secondaryLeafMembers = secondaryLeafMembers;
			}

			// Token: 0x060015C4 RID: 5572 RVA: 0x0005071C File Offset: 0x0004E91C
			protected override void TraverseDataShapeStructure(DataShape dataShape)
			{
				bool inPrimaryHierarchy = this.m_inPrimaryHierarchy;
				this.m_inPrimaryHierarchy = true;
				this.m_memberIndex = -1;
				base.Visit(dataShape.PrimaryHierarchy);
				this.m_inPrimaryHierarchy = false;
				this.m_memberIndex = -1;
				base.Visit(dataShape.SecondaryHierarchy);
				this.m_inPrimaryHierarchy = inPrimaryHierarchy;
				base.Visit<DataRow>(dataShape.DataRows, new Action<DataRow>(base.Visit));
			}

			// Token: 0x060015C5 RID: 5573 RVA: 0x00050783 File Offset: 0x0004E983
			protected override void Enter(DataShape dataShape)
			{
				this.m_parents.Push(dataShape);
			}

			// Token: 0x060015C6 RID: 5574 RVA: 0x00050791 File Offset: 0x0004E991
			protected override void Exit(DataShape dataShape)
			{
				this.m_parents.Pop();
			}

			// Token: 0x060015C7 RID: 5575 RVA: 0x000507A0 File Offset: 0x0004E9A0
			protected override void Enter(DataMember dataMember)
			{
				this.m_parents.Push(dataMember);
				this.m_memberIndex++;
				this.m_memberSortIndices.Add(dataMember, this.m_memberIndex);
				if (dataMember.DataMembers == null)
				{
					if (this.m_inPrimaryHierarchy)
					{
						this.m_primaryLeafMembers.Add(dataMember);
						return;
					}
					this.m_secondaryLeafMembers.Add(dataMember);
				}
			}

			// Token: 0x060015C8 RID: 5576 RVA: 0x00050802 File Offset: 0x0004EA02
			protected override void Exit(DataMember dataMember)
			{
				this.m_parents.Pop();
			}

			// Token: 0x060015C9 RID: 5577 RVA: 0x00050810 File Offset: 0x0004EA10
			protected override void Enter(DataRow dataRow)
			{
				this.m_currentRowIndex++;
				this.m_currentColumnIndex = -1;
			}

			// Token: 0x060015CA RID: 5578 RVA: 0x00050827 File Offset: 0x0004EA27
			protected override void Enter(DataIntersection dataIntersection)
			{
				this.m_currentColumnIndex++;
				this.m_parents.Push(dataIntersection);
			}

			// Token: 0x060015CB RID: 5579 RVA: 0x00050843 File Offset: 0x0004EA43
			protected override void Exit(DataIntersection dataIntersection)
			{
				this.m_parents.Pop();
			}

			// Token: 0x060015CC RID: 5580 RVA: 0x00050854 File Offset: 0x0004EA54
			protected override void Visit(Calculation calculation)
			{
				IIdentifiable identifiable = this.m_parents.Peek();
				BatchSubtotalAnnotationAnalyzer.SortCoordinate sortCoordinate;
				switch (identifiable.ObjectType)
				{
				case ObjectType.DataIntersection:
				{
					int num = this.m_memberSortIndices[this.m_primaryLeafMembers[this.m_currentRowIndex]];
					int num2 = this.m_memberSortIndices[this.m_secondaryLeafMembers[this.m_currentColumnIndex]];
					sortCoordinate = new BatchSubtotalAnnotationAnalyzer.SortCoordinate(num, num2);
					goto IL_00C2;
				}
				case ObjectType.DataMember:
				{
					int num3 = this.m_memberSortIndices[(DataMember)identifiable];
					sortCoordinate = (this.m_inPrimaryHierarchy ? new BatchSubtotalAnnotationAnalyzer.SortCoordinate(num3, -1) : new BatchSubtotalAnnotationAnalyzer.SortCoordinate(-1, num3));
					goto IL_00C2;
				}
				case ObjectType.DataShape:
					sortCoordinate = new BatchSubtotalAnnotationAnalyzer.SortCoordinate(-1, -1);
					goto IL_00C2;
				}
				Contract.RetailFail("Only data shapes, data members and interesections expected.");
				throw new InvalidOperationException("Only data shapes, data members and interesections expected.");
				IL_00C2:
				this.m_calculationIndices.Add(calculation, sortCoordinate);
			}

			// Token: 0x04000A28 RID: 2600
			private readonly Dictionary<Calculation, BatchSubtotalAnnotationAnalyzer.SortCoordinate> m_calculationIndices;

			// Token: 0x04000A29 RID: 2601
			private readonly Dictionary<DataMember, int> m_memberSortIndices;

			// Token: 0x04000A2A RID: 2602
			private readonly Stack<IIdentifiable> m_parents;

			// Token: 0x04000A2B RID: 2603
			private List<DataMember> m_primaryLeafMembers;

			// Token: 0x04000A2C RID: 2604
			private List<DataMember> m_secondaryLeafMembers;

			// Token: 0x04000A2D RID: 2605
			private int m_currentRowIndex = -1;

			// Token: 0x04000A2E RID: 2606
			private int m_currentColumnIndex = -1;

			// Token: 0x04000A2F RID: 2607
			private int m_memberIndex = -1;

			// Token: 0x04000A30 RID: 2608
			private bool m_inPrimaryHierarchy;
		}

		// Token: 0x020002A4 RID: 676
		private sealed class DataShapeSubtotalContext
		{
			// Token: 0x060015CD RID: 5581 RVA: 0x00050930 File Offset: 0x0004EB30
			internal DataShapeSubtotalContext(DataShape dataShape)
			{
				this.m_dataShape = dataShape;
				this.m_allPrimaryMembers = dataShape.PrimaryHierarchy.GetAllMembersDepthFirst().ToList<DataMember>();
				this.m_allSecondaryMembers = dataShape.SecondaryHierarchy.GetAllMembersDepthFirst().ToList<DataMember>();
			}

			// Token: 0x170003E3 RID: 995
			// (get) Token: 0x060015CE RID: 5582 RVA: 0x0005096B File Offset: 0x0004EB6B
			internal DataShape DataShape
			{
				get
				{
					return this.m_dataShape;
				}
			}

			// Token: 0x170003E4 RID: 996
			// (get) Token: 0x060015CF RID: 5583 RVA: 0x00050973 File Offset: 0x0004EB73
			internal IList<DataMember> AllPrimaryMembers
			{
				get
				{
					return this.m_allPrimaryMembers;
				}
			}

			// Token: 0x170003E5 RID: 997
			// (get) Token: 0x060015D0 RID: 5584 RVA: 0x0005097B File Offset: 0x0004EB7B
			internal IList<DataMember> AllSecondaryMembers
			{
				get
				{
					return this.m_allSecondaryMembers;
				}
			}

			// Token: 0x04000A31 RID: 2609
			private readonly DataShape m_dataShape;

			// Token: 0x04000A32 RID: 2610
			private readonly List<DataMember> m_allPrimaryMembers;

			// Token: 0x04000A33 RID: 2611
			private readonly List<DataMember> m_allSecondaryMembers;
		}
	}
}
