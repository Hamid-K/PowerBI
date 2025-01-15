using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQuery
{
	// Token: 0x0200000E RID: 14
	internal sealed class ScopeTreeBuilder : DataShapeVisitor
	{
		// Token: 0x06000073 RID: 115 RVA: 0x000032BA File Offset: 0x000014BA
		private ScopeTreeBuilder()
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000032E3 File Offset: 0x000014E3
		public static ScopeTree BuildScopeTree(DataShape dataShape)
		{
			ScopeTreeBuilder scopeTreeBuilder = new ScopeTreeBuilder();
			scopeTreeBuilder.Visit(dataShape);
			return scopeTreeBuilder.m_scopeTree;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000032F8 File Offset: 0x000014F8
		protected override void Enter(DataShape dataShape)
		{
			if (ScopeTreeBuilder.ShouldSkip(dataShape))
			{
				return;
			}
			if (!this.m_scopeTree.HasScope(dataShape.Id))
			{
				if (this.m_activeScopes.Count == 0)
				{
					this.m_scopeTree.AddRoot(dataShape);
				}
				else
				{
					this.m_scopeTree.Add(dataShape, this.ActiveScope);
				}
			}
			this.m_activeDataShapes.Push(new ScopeTreeBuilder.ScopeTreeBuilderContext(dataShape));
			this.m_activeScopes.Push(dataShape);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000336B File Offset: 0x0000156B
		protected override void Exit(DataShape dataShape)
		{
			if (ScopeTreeBuilder.ShouldSkip(dataShape))
			{
				return;
			}
			Contract.RetailAssert(this.m_activeDataShapes.Pop().DataShape == dataShape, "Exited wrong DataShape");
			ScopeTreeBuilder.PopStack<IScope>(this.m_activeScopes, dataShape);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000033A0 File Offset: 0x000015A0
		protected override void TraverseDataShapeStructure(DataShape dataShape)
		{
			if (ScopeTreeBuilder.ShouldSkip(dataShape))
			{
				return;
			}
			ScopeTreeBuilder.ScopeTreeBuilderContext activeDataShapeContext = this.ActiveDataShapeContext;
			activeDataShapeContext.InPrimaryHierarchy = false;
			activeDataShapeContext.VisitIntersections = false;
			base.Visit(dataShape.SecondaryHierarchy);
			activeDataShapeContext.InPrimaryHierarchy = true;
			activeDataShapeContext.VisitIntersections = true;
			base.Visit(dataShape.PrimaryHierarchy);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000033F0 File Offset: 0x000015F0
		protected override void Enter(DataMember dataMember)
		{
			if (ScopeTreeBuilder.ShouldSkip(dataMember))
			{
				return;
			}
			if (dataMember.IsDynamic)
			{
				this.ActiveDataShapeContext.Push(dataMember);
				if (!this.m_scopeTree.HasScope(dataMember.Id))
				{
					this.m_scopeTree.Add(dataMember, this.ActiveScope);
				}
				this.m_activeScopes.Push(dataMember);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000344B File Offset: 0x0000164B
		protected override void Exit(DataMember dataMember)
		{
			if (ScopeTreeBuilder.ShouldSkip(dataMember))
			{
				return;
			}
			if (dataMember.IsDynamic)
			{
				this.ActiveDataShapeContext.Pop(dataMember);
				ScopeTreeBuilder.PopStack<IScope>(this.m_activeScopes, dataMember);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003478 File Offset: 0x00001678
		protected override void TraverseDataMemberStructure(DataMember dataMember)
		{
			if (ScopeTreeBuilder.ShouldSkip(dataMember))
			{
				return;
			}
			if (dataMember.DataMembers == null)
			{
				ScopeTreeBuilder.ScopeTreeBuilderContext activeDataShapeContext = this.ActiveDataShapeContext;
				if (activeDataShapeContext.InPrimaryHierarchy)
				{
					activeDataShapeContext.NextPrimaryLeaf();
					if (activeDataShapeContext.VisitIntersections)
					{
						activeDataShapeContext.SkipCalculations = true;
						activeDataShapeContext.InPrimaryHierarchy = false;
						base.Visit(activeDataShapeContext.DataShape.SecondaryHierarchy);
						activeDataShapeContext.InPrimaryHierarchy = true;
						activeDataShapeContext.SkipCalculations = false;
						return;
					}
				}
				else
				{
					activeDataShapeContext.NextSecondaryLeaf();
					if (activeDataShapeContext.VisitIntersections && activeDataShapeContext.IsValidIntersection())
					{
						DataIntersection activeIntersection = activeDataShapeContext.GetActiveIntersection();
						this.Visit(activeIntersection);
						return;
					}
				}
			}
			else
			{
				base.Visit<DataMember>(dataMember.DataMembers, new Action<DataMember>(this.Visit));
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003520 File Offset: 0x00001720
		protected override void Enter(DataIntersection dataIntersection)
		{
			if (ScopeTreeBuilder.ShouldSkip(dataIntersection))
			{
				return;
			}
			ScopeTreeBuilder.ScopeTreeBuilderContext activeDataShapeContext = this.ActiveDataShapeContext;
			activeDataShapeContext.SkipCalculations = false;
			if (activeDataShapeContext.IsEligibleIntersectionScope)
			{
				if (!this.m_scopeTree.HasScope(dataIntersection.Id))
				{
					this.m_scopeTree.Add(dataIntersection, activeDataShapeContext.ActivePrimaryMember, activeDataShapeContext.ActiveSecondaryMember);
				}
				this.m_activeScopes.Push(dataIntersection);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003583 File Offset: 0x00001783
		protected override void Exit(DataIntersection dataIntersection)
		{
			if (ScopeTreeBuilder.ShouldSkip(dataIntersection))
			{
				return;
			}
			ScopeTreeBuilder.ScopeTreeBuilderContext activeDataShapeContext = this.ActiveDataShapeContext;
			activeDataShapeContext.SkipCalculations = true;
			if (activeDataShapeContext.IsEligibleIntersectionScope)
			{
				ScopeTreeBuilder.PopStack<IScope>(this.m_activeScopes, dataIntersection);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000035AE File Offset: 0x000017AE
		protected override void Visit(Calculation calculation)
		{
			if (!this.ActiveDataShapeContext.SkipCalculations)
			{
				this.m_scopeTree.Add(calculation, this.ActiveScope);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000035CF File Offset: 0x000017CF
		protected override void Visit(Filter filter, Identifier dataShapeId)
		{
			if (filter == null || filter.Condition == null)
			{
				return;
			}
			ContextFilterDataShapeVisitor.Visit(filter, new VisitDataShapeDelegate(this.VisitFilterConditionDataShape));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000035EF File Offset: 0x000017EF
		private void VisitFilterConditionDataShape(DataShape dataShape, ObjectType filterConditionType)
		{
			this.Visit(dataShape);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000035F8 File Offset: 0x000017F8
		private ScopeTreeBuilder.ScopeTreeBuilderContext ActiveDataShapeContext
		{
			get
			{
				return ScopeTreeBuilder.PeekStack<ScopeTreeBuilder.ScopeTreeBuilderContext>(this.m_activeDataShapes);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003605 File Offset: 0x00001805
		private IScope ActiveScope
		{
			get
			{
				return ScopeTreeBuilder.PeekStack<IScope>(this.m_activeScopes);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003612 File Offset: 0x00001812
		private static bool ShouldSkip(IScope scope)
		{
			return scope == null || scope.Id == null;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003625 File Offset: 0x00001825
		private static void PopStack<T>(Stack<T> stack, T expectedItem) where T : class
		{
			stack.Pop();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000362E File Offset: 0x0000182E
		private static T PeekStack<T>(Stack<T> stack)
		{
			return stack.Peek();
		}

		// Token: 0x04000038 RID: 56
		private readonly Stack<ScopeTreeBuilder.ScopeTreeBuilderContext> m_activeDataShapes = new Stack<ScopeTreeBuilder.ScopeTreeBuilderContext>();

		// Token: 0x04000039 RID: 57
		private readonly ScopeTree m_scopeTree = new ScopeTree();

		// Token: 0x0400003A RID: 58
		private Stack<IScope> m_activeScopes = new Stack<IScope>();

		// Token: 0x02000261 RID: 609
		private sealed class ScopeTreeBuilderContext : DataShapeVisitorContext
		{
			// Token: 0x060014D8 RID: 5336 RVA: 0x0004F1C7 File Offset: 0x0004D3C7
			internal ScopeTreeBuilderContext(DataShape dataShape)
				: base(dataShape)
			{
			}

			// Token: 0x170003C6 RID: 966
			// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0004F1E6 File Offset: 0x0004D3E6
			public DataMember ActivePrimaryMember
			{
				get
				{
					return ScopeTreeBuilder.PeekStack<DataMember>(this.m_primaryDynamics);
				}
			}

			// Token: 0x170003C7 RID: 967
			// (get) Token: 0x060014DA RID: 5338 RVA: 0x0004F1F3 File Offset: 0x0004D3F3
			public DataMember ActiveSecondaryMember
			{
				get
				{
					return ScopeTreeBuilder.PeekStack<DataMember>(this.m_secondaryDynamics);
				}
			}

			// Token: 0x170003C8 RID: 968
			// (get) Token: 0x060014DB RID: 5339 RVA: 0x0004F200 File Offset: 0x0004D400
			// (set) Token: 0x060014DC RID: 5340 RVA: 0x0004F208 File Offset: 0x0004D408
			public bool VisitIntersections
			{
				get
				{
					return this.m_visitIntersections;
				}
				set
				{
					this.m_visitIntersections = value;
				}
			}

			// Token: 0x170003C9 RID: 969
			// (get) Token: 0x060014DD RID: 5341 RVA: 0x0004F211 File Offset: 0x0004D411
			// (set) Token: 0x060014DE RID: 5342 RVA: 0x0004F219 File Offset: 0x0004D419
			public bool SkipCalculations { get; set; }

			// Token: 0x170003CA RID: 970
			// (get) Token: 0x060014DF RID: 5343 RVA: 0x0004F222 File Offset: 0x0004D422
			public bool IsEligibleIntersectionScope
			{
				get
				{
					return this.m_primaryDynamics.Count > 0 && this.m_secondaryDynamics.Count > 0;
				}
			}

			// Token: 0x060014E0 RID: 5344 RVA: 0x0004F242 File Offset: 0x0004D442
			public void Push(DataMember member)
			{
				this.ActiveMemberStack.Push(member);
			}

			// Token: 0x060014E1 RID: 5345 RVA: 0x0004F250 File Offset: 0x0004D450
			public void Pop(DataMember member)
			{
				ScopeTreeBuilder.PopStack<DataMember>(this.ActiveMemberStack, member);
			}

			// Token: 0x170003CB RID: 971
			// (get) Token: 0x060014E2 RID: 5346 RVA: 0x0004F25E File Offset: 0x0004D45E
			private Stack<DataMember> ActiveMemberStack
			{
				get
				{
					if (!base.InPrimaryHierarchy)
					{
						return this.m_secondaryDynamics;
					}
					return this.m_primaryDynamics;
				}
			}

			// Token: 0x0400094F RID: 2383
			private readonly Stack<DataMember> m_primaryDynamics = new Stack<DataMember>();

			// Token: 0x04000950 RID: 2384
			private readonly Stack<DataMember> m_secondaryDynamics = new Stack<DataMember>();

			// Token: 0x04000951 RID: 2385
			private bool m_visitIntersections;
		}
	}
}
