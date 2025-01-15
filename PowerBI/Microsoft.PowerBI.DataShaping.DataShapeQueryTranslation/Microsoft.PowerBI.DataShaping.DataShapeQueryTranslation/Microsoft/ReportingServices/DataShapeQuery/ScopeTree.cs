using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQuery
{
	// Token: 0x0200000D RID: 13
	internal sealed class ScopeTree
	{
		// Token: 0x0600003F RID: 63 RVA: 0x0000297A File Offset: 0x00000B7A
		public bool TraverseUp(IScope scope, ScopeTree.DirectedVisitor func)
		{
			return this.GetNode(scope).TraverseUp(func);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000298C File Offset: 0x00000B8C
		internal void AddRoot(IScope root)
		{
			int nextDataContext = this.m_nextDataContext;
			this.m_nextDataContext = nextDataContext + 1;
			this.AddNode(new ScopeTree.RootScopeTreeNode(root, nextDataContext));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029B8 File Offset: 0x00000BB8
		internal void Add(IScope scope, IScope parent)
		{
			ScopeTree.ScopeTreeNode node = this.GetNode(parent);
			int num = node.DataContext;
			if (scope is DataMember)
			{
				int num2 = this.m_nextDataContext;
				this.m_nextDataContext = num2 + 1;
				num = num2;
			}
			else
			{
				DataShape dataShape = scope as DataShape;
				if (dataShape != null && dataShape.IsIndependent)
				{
					int num2 = this.m_nextDataContext;
					this.m_nextDataContext = num2 + 1;
					num = num2;
				}
			}
			ScopeTree.LinearScopeTreeNode linearScopeTreeNode = new ScopeTree.LinearScopeTreeNode(scope, node, num);
			this.AddNode(linearScopeTreeNode);
			node.AddChildScope(linearScopeTreeNode);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A2E File Offset: 0x00000C2E
		internal void Add(Calculation item, IScope owner)
		{
			this.Add(item, owner);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A38 File Offset: 0x00000C38
		internal void Remove(Calculation item)
		{
			Identifier id = item.Id;
			IScope scope;
			if (id != null && this.m_scopesByItem.TryGetValue(id, out scope))
			{
				this.m_scopesByItem.Remove(id);
				this.GetNode(scope).RemoveContextItem(scope.Id, item);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A88 File Offset: 0x00000C88
		private void Add(IContextItem item, IScope owner)
		{
			Identifier id = item.Id;
			if (id != null && !this.m_scopesByItem.ContainsKey(id))
			{
				this.m_scopesByItem.Add(id, owner);
				this.GetNode(owner).AddContextItem(owner.Id, item);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002AD4 File Offset: 0x00000CD4
		internal void Add(IScope scope, IScope primaryParent, IScope secondaryParent)
		{
			IScope scope2 = null;
			ScopeTree.IntersectionKey intersectionKey = new ScopeTree.IntersectionKey(primaryParent, secondaryParent);
			if (!this.m_canonicalScopesByIntersectionKey.TryGetValue(intersectionKey, out scope2))
			{
				ScopeTree.ScopeTreeNode node = this.GetNode(primaryParent);
				ScopeTree.ScopeTreeNode node2 = this.GetNode(secondaryParent);
				this.m_canonicalScopesByIntersectionId.Add(scope.Id, scope);
				ScopeTree.ScopeTreeNode scopeTreeNode = node;
				ScopeTree.ScopeTreeNode scopeTreeNode2 = node2;
				int nextDataContext = this.m_nextDataContext;
				this.m_nextDataContext = nextDataContext + 1;
				ScopeTree.IntersectionScopeTreeNode intersectionScopeTreeNode = new ScopeTree.IntersectionScopeTreeNode(scope, scopeTreeNode, scopeTreeNode2, nextDataContext);
				this.m_canonicalScopesByIntersectionKey.Add(intersectionKey, scope);
				this.AddNode(intersectionScopeTreeNode);
				node.AddChildScope(intersectionScopeTreeNode);
				node2.AddChildScope(intersectionScopeTreeNode);
				return;
			}
			((ScopeTree.IntersectionScopeTreeNode)this.GetNode(scope2)).AddAdditionalScope(scope);
			this.m_canonicalScopesByIntersectionId.Add(scope.Id, scope2);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B85 File Offset: 0x00000D85
		private void AddNode(ScopeTree.ScopeTreeNode node)
		{
			this.m_nodesByScope.Add(node.Scope.Id, node);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B9E File Offset: 0x00000D9E
		private ScopeTree.ScopeTreeNode GetNode(IScope scope)
		{
			return this.GetNode(scope.Id);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BAC File Offset: 0x00000DAC
		private ScopeTree.ScopeTreeNode GetNode(Identifier id)
		{
			ScopeTree.ScopeTreeNode scopeTreeNode;
			Contract.RetailAssert(this.TryGetNode(id, out scopeTreeNode), "Missing expected node.");
			return scopeTreeNode;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BD0 File Offset: 0x00000DD0
		private bool TryGetNode(Identifier id, out ScopeTree.ScopeTreeNode node)
		{
			IScope scope;
			return this.m_nodesByScope.TryGetValue(id, out node) || (this.m_canonicalScopesByIntersectionId.TryGetValue(id, out scope) && this.m_nodesByScope.TryGetValue(scope.Id, out node));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C12 File Offset: 0x00000E12
		internal IScope GetScope(Identifier id)
		{
			return this.GetNode(id).Scope;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C20 File Offset: 0x00000E20
		internal bool TryGetIntersectionScope(IScope primaryParent, IScope secondaryParent, out IScope intersection)
		{
			return this.m_canonicalScopesByIntersectionKey.TryGetValue(new ScopeTree.IntersectionKey(primaryParent, secondaryParent), out intersection);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C38 File Offset: 0x00000E38
		internal IScope GetIntersectionScope(IScope primaryParent, IScope secondaryParent)
		{
			IScope scope;
			Contract.RetailAssert(this.TryGetIntersectionScope(primaryParent, secondaryParent, out scope), "Expected intersection to be found but it was not.");
			return scope;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C5A File Offset: 0x00000E5A
		internal IScope GetCanonicalScope(Identifier id)
		{
			return this.GetNode(id).Scope;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C68 File Offset: 0x00000E68
		public bool HasScope(Identifier id)
		{
			ScopeTree.ScopeTreeNode scopeTreeNode;
			return this.TryGetNode(id, out scopeTreeNode);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C80 File Offset: 0x00000E80
		public IScope GetContainingScope(Calculation item)
		{
			IScope containingStructuralScope = this.GetContainingStructuralScope(item);
			return this.GetCanonicalScope(containingStructuralScope.Id);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002CA4 File Offset: 0x00000EA4
		private IScope GetContainingStructuralScope(IContextItem item)
		{
			IScope scope;
			Contract.RetailAssert(this.m_scopesByItem.TryGetValue(item.Id, out scope), "Could not find containing scope for item {0}", item.Id);
			return scope;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002CD5 File Offset: 0x00000ED5
		public IScope GetContainingStructuralScope(Calculation item)
		{
			return this.GetContainingStructuralScope(item);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002CE0 File Offset: 0x00000EE0
		internal ReadOnlyCollection<Calculation> GetCalculationsInSameScope(Calculation calculation)
		{
			IScope containingScope = this.GetContainingScope(calculation);
			return this.GetNode(containingScope).GetAllItems<Calculation>();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002D01 File Offset: 0x00000F01
		public IScope GetInnermostScopeInDataShape(DataShape dataShape)
		{
			return this.GetInnermostScopeInDataShape(dataShape, dataShape.PrimaryHierarchy.GetAllDynamicMembers().LastOrDefault<DataMember>(), dataShape.SecondaryHierarchy.GetAllDynamicMembers().LastOrDefault<DataMember>());
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D2C File Offset: 0x00000F2C
		public IScope GetInnermostScopeExcludingContextOnlyInDataShape(DataShape dataShape)
		{
			return this.GetInnermostScopeInDataShape(dataShape, dataShape.PrimaryHierarchy.GetAllDynamicMembers().LastOrDefault((DataMember member) => !member.ContextOnly), dataShape.SecondaryHierarchy.GetAllDynamicMembers().LastOrDefault((DataMember member) => !member.ContextOnly));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002D9E File Offset: 0x00000F9E
		private IScope GetInnermostScopeInDataShape(DataShape dataShape, DataMember primaryInnermost, DataMember secondaryInnermost)
		{
			return this.GetInnermostScope(primaryInnermost, secondaryInnermost) ?? dataShape;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public IScope GetInnermostScope(DataMember primaryDataMember, DataMember secondaryDataMember)
		{
			if (primaryDataMember == null || secondaryDataMember == null)
			{
				return primaryDataMember ?? secondaryDataMember;
			}
			return this.GetIntersectionScope(primaryDataMember, secondaryDataMember);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002DD4 File Offset: 0x00000FD4
		internal ReadOnlyCollection<T> GetItems<T>(Identifier scopeId)
		{
			return this.GetNode(scopeId).GetItems<T>(scopeId);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002DE4 File Offset: 0x00000FE4
		internal ReadOnlyCollection<T> GetAllItemsInScope<T>(Identifier scopeId)
		{
			ScopeTree.ScopeTreeNode node = this.GetNode(scopeId);
			ReadOnlyCollection<T> readOnlyCollection;
			if (node is ScopeTree.IntersectionScopeTreeNode)
			{
				readOnlyCollection = node.GetAllItems<T>();
			}
			else
			{
				readOnlyCollection = node.GetItems<T>(scopeId);
			}
			return readOnlyCollection;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002E18 File Offset: 0x00001018
		public bool TryGetItemById(Identifier id, out IIdentifiable item)
		{
			ScopeTree.ScopeTreeNode scopeTreeNode;
			if (this.TryGetNode(id, out scopeTreeNode))
			{
				item = scopeTreeNode.Scope;
				return true;
			}
			IScope scope;
			if (this.m_scopesByItem.TryGetValue(id, out scope))
			{
				ScopeTree.ScopeTreeNode node = this.GetNode(scope);
				item = node.GetAllItems<IContextItem>().Single((IContextItem c) => c.Id == id);
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E88 File Offset: 0x00001088
		public bool IsOrContainsGroup(IScope scope)
		{
			if (this.IsGroup(scope))
			{
				return true;
			}
			IEnumerable<ScopeTree.ScopeTreeNode> childScopes = this.GetNode(scope.Id).ChildScopes;
			if (childScopes != null)
			{
				foreach (ScopeTree.ScopeTreeNode scopeTreeNode in childScopes)
				{
					if (this.IsOrContainsGroup(scopeTreeNode.Scope))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002F00 File Offset: 0x00001100
		private bool IsGroup(IScope scope)
		{
			DataMember dataMember = scope as DataMember;
			return dataMember != null && dataMember.IsDynamic;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002F20 File Offset: 0x00001120
		public bool AreSameScope(IScope scope1, IScope scope2)
		{
			if (scope1 == scope2)
			{
				return true;
			}
			DataIntersection dataIntersection = scope1 as DataIntersection;
			DataIntersection dataIntersection2 = scope2 as DataIntersection;
			return dataIntersection != null && dataIntersection2 != null && this.AreSameIntersectionScope(dataIntersection, dataIntersection2);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002F51 File Offset: 0x00001151
		public bool AreSameIntersectionScope(DataIntersection intersection1, DataIntersection intersection2)
		{
			return this.GetCanonicalScope(intersection1.Id) == this.GetCanonicalScope(intersection2.Id);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F70 File Offset: 0x00001170
		public bool HaveEquivalentDataContext(IScope scope1, IScope scope2)
		{
			ScopeTree.ScopeTreeNode node = this.GetNode(scope1);
			ScopeTree.ScopeTreeNode node2 = this.GetNode(scope2);
			return node.DataContext == node2.DataContext;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F99 File Offset: 0x00001199
		public IEnumerable<IScope> GetSpanningScopes(IReadOnlyList<IScope> scopes)
		{
			return this.GetAllParentScopes(scopes.Last<IScope>(), scopes.First<IScope>());
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002FAD File Offset: 0x000011AD
		public IEnumerable<IScope> GetAllParentScopes(IScope startScope)
		{
			return this.GetAllParentScopes(startScope, null);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002FB7 File Offset: 0x000011B7
		public IEnumerable<IScope> GetAllParentScopes(IScope startScope, IScope stopScope)
		{
			this.GetNode(startScope);
			List<IScope> scopes = new List<IScope>();
			this.TraverseUp(startScope, delegate(IScope scope)
			{
				scopes.Add(scope);
				return scope != stopScope;
			});
			scopes.Reverse();
			HashSet<IScope> distinctScopes = new HashSet<IScope>();
			int num;
			for (int i = 0; i < scopes.Count; i = num)
			{
				IScope scope2 = scopes[i];
				if (!distinctScopes.Contains(scope2))
				{
					distinctScopes.Add(scope2);
					yield return scope2;
				}
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002FD8 File Offset: 0x000011D8
		public IScope GetParentScope(IScope scope)
		{
			ScopeTree.ScopeTreeNode node = this.GetNode(scope);
			if (ScopeTree.IsRoot(node))
			{
				return null;
			}
			return (node as ScopeTree.LinearScopeTreeNode).Parent.Scope;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003007 File Offset: 0x00001207
		public IEnumerable<IScope> GetChildScopes(IScope scope)
		{
			return from c in this.GetNode(scope).ChildScopes.EmptyIfNull<ScopeTree.ScopeTreeNode>()
				select c.Scope;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003040 File Offset: 0x00001240
		public DataShape GetParentDataShape(IScope scope)
		{
			while (!this.IsRoot(scope))
			{
				DataIntersection dataIntersection = scope as DataIntersection;
				if (dataIntersection != null)
				{
					DataShape parentDataShape = this.GetParentDataShape(this.GetPrimaryParentScope(dataIntersection));
					DataShape parentDataShape2 = this.GetParentDataShape(this.GetSecondaryParentScope(dataIntersection));
					Contract.RetailAssert(this.AreSameScope(parentDataShape, parentDataShape2), "Expected to be the same scope");
					return parentDataShape;
				}
				scope = this.GetParentScope(scope);
				if (scope.ObjectType == ObjectType.DataShape)
				{
					return (DataShape)scope;
				}
			}
			return null;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000030AB File Offset: 0x000012AB
		public DataShape GetContainingDataShapeOrSelf(IScope scope)
		{
			if (scope is DataShape)
			{
				return (DataShape)scope;
			}
			return this.GetParentDataShape(scope);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000030C3 File Offset: 0x000012C3
		private static bool IsRoot(ScopeTree.ScopeTreeNode node)
		{
			return node is ScopeTree.RootScopeTreeNode;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000030CE File Offset: 0x000012CE
		public bool IsRoot(IScope scope)
		{
			return ScopeTree.IsRoot(this.GetNode(scope));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000030DC File Offset: 0x000012DC
		public IScope GetSecondaryParentScope(IScope scope)
		{
			if (scope.ObjectType == ObjectType.DataIntersection)
			{
				DataIntersection dataIntersection = (DataIntersection)scope;
				return this.GetSecondaryParentScope(dataIntersection);
			}
			return this.GetParentScope(scope);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003109 File Offset: 0x00001309
		public IScope GetSecondaryParentScope(DataIntersection intersection)
		{
			return ((ScopeTree.IntersectionScopeTreeNode)this.GetNode(intersection)).SecondaryParent.Scope;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003124 File Offset: 0x00001324
		public IScope GetPrimaryParentScope(IScope scope)
		{
			if (scope.ObjectType == ObjectType.DataIntersection)
			{
				DataIntersection dataIntersection = (DataIntersection)scope;
				return this.GetPrimaryParentScope(dataIntersection);
			}
			return this.GetParentScope(scope);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003151 File Offset: 0x00001351
		public IScope GetPrimaryParentScope(DataIntersection intersection)
		{
			return ((ScopeTree.IntersectionScopeTreeNode)this.GetNode(intersection)).PrimaryParent.Scope;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003169 File Offset: 0x00001369
		internal bool IsProperParentScope(IScope outerScope, IScope innerScope)
		{
			return this.GetNode(innerScope).HasParentScope(outerScope, true);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003179 File Offset: 0x00001379
		internal bool IsParentScope(IScope outerScope, IScope innerScope)
		{
			return this.GetNode(innerScope).HasParentScope(outerScope, false);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003189 File Offset: 0x00001389
		internal bool IsImmediateParentScope(IScope outerScope, IScope innerScope)
		{
			return this.GetNode(innerScope).HasImmediateParentScope(outerScope, false);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003199 File Offset: 0x00001399
		internal bool IsSameOrParentScope(IScope outerScope, IScope innerScope)
		{
			return this.AreSameScope(outerScope, innerScope) || this.IsParentScope(outerScope, innerScope);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000031B0 File Offset: 0x000013B0
		internal bool IsSameWithAny(IReadOnlyList<IScope> scopes, IScope innerScope)
		{
			foreach (IScope scope in scopes)
			{
				if (this.AreSameScope(scope, innerScope))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003204 File Offset: 0x00001404
		internal IEnumerable<IScope> GetContinuousLinearScopeNodesBetween(IScope scope, IScope otherScope)
		{
			IScope scope2;
			IScope scope3;
			if (this.IsParentScope(scope, otherScope))
			{
				scope2 = scope;
				scope3 = otherScope;
			}
			else
			{
				if (!this.IsParentScope(otherScope, scope))
				{
					return null;
				}
				scope2 = otherScope;
				scope3 = scope;
			}
			ScopeTree.LinearScopeTreeNode linearScopeTreeNode = this.GetNode(scope3) as ScopeTree.LinearScopeTreeNode;
			Stack<IScope> stack = new Stack<IScope>();
			stack.Push(linearScopeTreeNode.Scope);
			while (linearScopeTreeNode.Parent.Scope != scope2)
			{
				ScopeTree.LinearScopeTreeNode linearScopeTreeNode2 = linearScopeTreeNode.Parent as ScopeTree.LinearScopeTreeNode;
				if (linearScopeTreeNode2 == null)
				{
					return null;
				}
				stack.Push(linearScopeTreeNode2.Scope);
				linearScopeTreeNode = linearScopeTreeNode2;
			}
			return stack;
		}

		// Token: 0x04000033 RID: 51
		private readonly Dictionary<Identifier, ScopeTree.ScopeTreeNode> m_nodesByScope = new Dictionary<Identifier, ScopeTree.ScopeTreeNode>();

		// Token: 0x04000034 RID: 52
		private readonly Dictionary<Identifier, IScope> m_scopesByItem = new Dictionary<Identifier, IScope>();

		// Token: 0x04000035 RID: 53
		private readonly Dictionary<Identifier, IScope> m_canonicalScopesByIntersectionId = new Dictionary<Identifier, IScope>();

		// Token: 0x04000036 RID: 54
		private readonly Dictionary<ScopeTree.IntersectionKey, IScope> m_canonicalScopesByIntersectionKey = new Dictionary<ScopeTree.IntersectionKey, IScope>();

		// Token: 0x04000037 RID: 55
		private int m_nextDataContext;

		// Token: 0x02000257 RID: 599
		// (Invoke) Token: 0x0600149D RID: 5277
		public delegate bool DirectedVisitor(IScope scope);

		// Token: 0x02000258 RID: 600
		private abstract class ScopeTreeNode
		{
			// Token: 0x060014A0 RID: 5280 RVA: 0x0004EC10 File Offset: 0x0004CE10
			protected ScopeTreeNode(IScope scope, int dataContext)
			{
				this.m_scope = scope;
				this.m_dataContext = dataContext;
			}

			// Token: 0x170003BB RID: 955
			// (get) Token: 0x060014A1 RID: 5281 RVA: 0x0004EC31 File Offset: 0x0004CE31
			public IScope Scope
			{
				get
				{
					return this.m_scope;
				}
			}

			// Token: 0x170003BC RID: 956
			// (get) Token: 0x060014A2 RID: 5282 RVA: 0x0004EC39 File Offset: 0x0004CE39
			public int DataContext
			{
				get
				{
					return this.m_dataContext;
				}
			}

			// Token: 0x170003BD RID: 957
			// (get) Token: 0x060014A3 RID: 5283 RVA: 0x0004EC41 File Offset: 0x0004CE41
			public IEnumerable<ScopeTree.ScopeTreeNode> ChildScopes
			{
				get
				{
					return this.m_childScopes;
				}
			}

			// Token: 0x060014A4 RID: 5284 RVA: 0x0004EC49 File Offset: 0x0004CE49
			public void AddChildScope(ScopeTree.ScopeTreeNode childNode)
			{
				if (this.m_childScopes == null)
				{
					this.m_childScopes = new List<ScopeTree.ScopeTreeNode>();
				}
				this.m_childScopes.Add(childNode);
			}

			// Token: 0x060014A5 RID: 5285 RVA: 0x0004EC6C File Offset: 0x0004CE6C
			public void AddContextItem(Identifier scopeId, IContextItem item)
			{
				List<IContextItem> list;
				if (this.m_contextItemsByOwner.TryGetValue(scopeId, out list))
				{
					list.Add(item);
					return;
				}
				list = new List<IContextItem> { item };
				this.m_contextItemsByOwner.Add(scopeId, list);
			}

			// Token: 0x060014A6 RID: 5286 RVA: 0x0004ECAC File Offset: 0x0004CEAC
			public void RemoveContextItem(Identifier scopeId, IContextItem item)
			{
				List<IContextItem> list;
				if (this.m_contextItemsByOwner.TryGetValue(scopeId, out list))
				{
					list.Remove(item);
				}
			}

			// Token: 0x060014A7 RID: 5287 RVA: 0x0004ECD4 File Offset: 0x0004CED4
			public ReadOnlyCollection<T> GetItems<T>(Identifier scopeId)
			{
				List<IContextItem> list;
				if (!this.m_contextItemsByOwner.TryGetValue(scopeId, out list))
				{
					return Util.EmptyReadOnlyCollection<T>();
				}
				return list.OfType<T>().ToReadOnlyCollection<T>();
			}

			// Token: 0x060014A8 RID: 5288 RVA: 0x0004ED02 File Offset: 0x0004CF02
			public ReadOnlyCollection<T> GetAllItems<T>()
			{
				return this.m_contextItemsByOwner.SelectMany((KeyValuePair<Identifier, List<IContextItem>> v) => v.Value).OfType<T>().ToReadOnlyCollection<T>();
			}

			// Token: 0x060014A9 RID: 5289
			public abstract bool TraverseUp(ScopeTree.DirectedVisitor func);

			// Token: 0x060014AA RID: 5290
			public abstract bool HasParentScope(IScope parentScope, bool isProperParent);

			// Token: 0x060014AB RID: 5291
			public abstract bool HasSameOrParentScope(IScope parentScope, bool isProperParent);

			// Token: 0x060014AC RID: 5292
			public abstract bool HasImmediateParentScope(IScope parentScope, bool isProperParent);

			// Token: 0x04000933 RID: 2355
			private readonly IScope m_scope;

			// Token: 0x04000934 RID: 2356
			private readonly int m_dataContext;

			// Token: 0x04000935 RID: 2357
			private readonly Dictionary<Identifier, List<IContextItem>> m_contextItemsByOwner = new Dictionary<Identifier, List<IContextItem>>();

			// Token: 0x04000936 RID: 2358
			private List<ScopeTree.ScopeTreeNode> m_childScopes;
		}

		// Token: 0x02000259 RID: 601
		private sealed class RootScopeTreeNode : ScopeTree.ScopeTreeNode
		{
			// Token: 0x060014AD RID: 5293 RVA: 0x0004ED38 File Offset: 0x0004CF38
			internal RootScopeTreeNode(IScope scope, int groupingIdentity)
				: base(scope, groupingIdentity)
			{
			}

			// Token: 0x060014AE RID: 5294 RVA: 0x0004ED42 File Offset: 0x0004CF42
			public override bool TraverseUp(ScopeTree.DirectedVisitor func)
			{
				return func(base.Scope);
			}

			// Token: 0x060014AF RID: 5295 RVA: 0x0004ED50 File Offset: 0x0004CF50
			public override bool HasParentScope(IScope parentScope, bool isProperParent)
			{
				return false;
			}

			// Token: 0x060014B0 RID: 5296 RVA: 0x0004ED53 File Offset: 0x0004CF53
			public override bool HasSameOrParentScope(IScope parentScope, bool isProperParent)
			{
				return base.Scope == parentScope;
			}

			// Token: 0x060014B1 RID: 5297 RVA: 0x0004ED5E File Offset: 0x0004CF5E
			public override bool HasImmediateParentScope(IScope parentScope, bool isProperParent)
			{
				return false;
			}
		}

		// Token: 0x0200025A RID: 602
		private sealed class LinearScopeTreeNode : ScopeTree.ScopeTreeNode
		{
			// Token: 0x060014B2 RID: 5298 RVA: 0x0004ED61 File Offset: 0x0004CF61
			internal LinearScopeTreeNode(IScope scope, ScopeTree.ScopeTreeNode parent, int groupingIdentity)
				: base(scope, groupingIdentity)
			{
				this.m_parent = parent;
			}

			// Token: 0x170003BE RID: 958
			// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0004ED72 File Offset: 0x0004CF72
			public ScopeTree.ScopeTreeNode Parent
			{
				get
				{
					return this.m_parent;
				}
			}

			// Token: 0x060014B4 RID: 5300 RVA: 0x0004ED7A File Offset: 0x0004CF7A
			public override bool TraverseUp(ScopeTree.DirectedVisitor func)
			{
				return func(base.Scope) && this.m_parent.TraverseUp(func);
			}

			// Token: 0x060014B5 RID: 5301 RVA: 0x0004ED98 File Offset: 0x0004CF98
			public override bool HasParentScope(IScope parentScope, bool isProperParent)
			{
				return this.m_parent.Scope == parentScope || this.m_parent.HasParentScope(parentScope, isProperParent);
			}

			// Token: 0x060014B6 RID: 5302 RVA: 0x0004EDB7 File Offset: 0x0004CFB7
			public override bool HasSameOrParentScope(IScope parentScope, bool isProperParent)
			{
				return parentScope == base.Scope || (this.m_parent != null && this.m_parent.HasSameOrParentScope(parentScope, isProperParent));
			}

			// Token: 0x060014B7 RID: 5303 RVA: 0x0004EDDB File Offset: 0x0004CFDB
			public override bool HasImmediateParentScope(IScope parentScope, bool isProperParent)
			{
				return this.m_parent.Scope == parentScope;
			}

			// Token: 0x04000937 RID: 2359
			private readonly ScopeTree.ScopeTreeNode m_parent;
		}

		// Token: 0x0200025B RID: 603
		private sealed class IntersectionScopeTreeNode : ScopeTree.ScopeTreeNode
		{
			// Token: 0x060014B8 RID: 5304 RVA: 0x0004EDEB File Offset: 0x0004CFEB
			internal IntersectionScopeTreeNode(IScope scope, ScopeTree.ScopeTreeNode primaryParent, ScopeTree.ScopeTreeNode secondaryParent, int groupingIdentity)
				: base(scope, groupingIdentity)
			{
				this.m_primaryParent = primaryParent;
				this.m_secondaryParent = secondaryParent;
				this.m_allScopes = new List<IScope>();
				this.AddAdditionalScope(scope);
			}

			// Token: 0x170003BF RID: 959
			// (get) Token: 0x060014B9 RID: 5305 RVA: 0x0004EE16 File Offset: 0x0004D016
			public ScopeTree.ScopeTreeNode PrimaryParent
			{
				get
				{
					return this.m_primaryParent;
				}
			}

			// Token: 0x170003C0 RID: 960
			// (get) Token: 0x060014BA RID: 5306 RVA: 0x0004EE1E File Offset: 0x0004D01E
			public ScopeTree.ScopeTreeNode SecondaryParent
			{
				get
				{
					return this.m_secondaryParent;
				}
			}

			// Token: 0x170003C1 RID: 961
			// (get) Token: 0x060014BB RID: 5307 RVA: 0x0004EE26 File Offset: 0x0004D026
			public IList<IScope> AllScopes
			{
				get
				{
					return this.m_allScopes;
				}
			}

			// Token: 0x060014BC RID: 5308 RVA: 0x0004EE2E File Offset: 0x0004D02E
			public void AddAdditionalScope(IScope scope)
			{
				this.m_allScopes.Add(scope);
			}

			// Token: 0x060014BD RID: 5309 RVA: 0x0004EE3C File Offset: 0x0004D03C
			public override bool TraverseUp(ScopeTree.DirectedVisitor func)
			{
				return func(base.Scope) && (this.m_secondaryParent.TraverseUp(func) & this.m_primaryParent.TraverseUp(func));
			}

			// Token: 0x060014BE RID: 5310 RVA: 0x0004EE68 File Offset: 0x0004D068
			public override bool HasParentScope(IScope parentScope, bool isProperParent)
			{
				bool flag = this.m_primaryParent.HasSameOrParentScope(parentScope, isProperParent);
				bool flag2 = this.m_secondaryParent.HasSameOrParentScope(parentScope, isProperParent);
				if (isProperParent)
				{
					return flag && flag2;
				}
				return flag || flag2;
			}

			// Token: 0x060014BF RID: 5311 RVA: 0x0004EE9C File Offset: 0x0004D09C
			public override bool HasSameOrParentScope(IScope parentScope, bool isProperParent)
			{
				if (base.Scope == parentScope)
				{
					return true;
				}
				bool flag = this.m_primaryParent.HasSameOrParentScope(parentScope, isProperParent);
				bool flag2 = this.m_secondaryParent.HasSameOrParentScope(parentScope, isProperParent);
				if (isProperParent)
				{
					return flag && flag2;
				}
				return flag || flag2;
			}

			// Token: 0x060014C0 RID: 5312 RVA: 0x0004EEDC File Offset: 0x0004D0DC
			public override bool HasImmediateParentScope(IScope parentScope, bool isProperParent)
			{
				bool flag = this.m_primaryParent.Scope == parentScope;
				bool flag2 = this.m_secondaryParent.Scope == parentScope;
				if (isProperParent)
				{
					return flag && flag2;
				}
				return flag || flag2;
			}

			// Token: 0x04000938 RID: 2360
			private readonly ScopeTree.ScopeTreeNode m_primaryParent;

			// Token: 0x04000939 RID: 2361
			private readonly ScopeTree.ScopeTreeNode m_secondaryParent;

			// Token: 0x0400093A RID: 2362
			private readonly List<IScope> m_allScopes;
		}

		// Token: 0x0200025C RID: 604
		internal sealed class IntersectionKey : IEquatable<ScopeTree.IntersectionKey>
		{
			// Token: 0x060014C1 RID: 5313 RVA: 0x0004EF11 File Offset: 0x0004D111
			internal IntersectionKey(IScope primaryParent, IScope secondaryParent)
			{
				this.m_primaryParent = primaryParent;
				this.m_secondaryParent = secondaryParent;
			}

			// Token: 0x170003C2 RID: 962
			// (get) Token: 0x060014C2 RID: 5314 RVA: 0x0004EF27 File Offset: 0x0004D127
			public IScope PrimaryParent
			{
				get
				{
					return this.m_primaryParent;
				}
			}

			// Token: 0x170003C3 RID: 963
			// (get) Token: 0x060014C3 RID: 5315 RVA: 0x0004EF2F File Offset: 0x0004D12F
			public IScope SecondaryParent
			{
				get
				{
					return this.m_secondaryParent;
				}
			}

			// Token: 0x060014C4 RID: 5316 RVA: 0x0004EF37 File Offset: 0x0004D137
			public override bool Equals(object obj)
			{
				return this.Equals(obj as ScopeTree.IntersectionKey);
			}

			// Token: 0x060014C5 RID: 5317 RVA: 0x0004EF45 File Offset: 0x0004D145
			public bool Equals(ScopeTree.IntersectionKey key)
			{
				return key.PrimaryParent.Id.Equals(this.PrimaryParent.Id) && key.SecondaryParent.Id.Equals(this.SecondaryParent.Id);
			}

			// Token: 0x060014C6 RID: 5318 RVA: 0x0004EF81 File Offset: 0x0004D181
			public override int GetHashCode()
			{
				return this.PrimaryParent.Id.GetHashCode() ^ this.SecondaryParent.Id.GetHashCode();
			}

			// Token: 0x0400093B RID: 2363
			private readonly IScope m_primaryParent;

			// Token: 0x0400093C RID: 2364
			private readonly IScope m_secondaryParent;
		}
	}
}
