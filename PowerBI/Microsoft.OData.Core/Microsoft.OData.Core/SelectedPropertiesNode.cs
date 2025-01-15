using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x020000CC RID: 204
	internal sealed class SelectedPropertiesNode
	{
		// Token: 0x0600096C RID: 2412 RVA: 0x000174EB File Offset: 0x000156EB
		internal SelectedPropertiesNode(string selectClause, IEdmStructuredType structuredType, IEdmModel edmModel)
			: this(SelectedPropertiesNode.SelectionType.PartialSubtree)
		{
			this.structuredType = structuredType;
			this.edmModel = edmModel;
			this.Parse(selectClause);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0001750C File Offset: 0x0001570C
		private void Parse(string selectClause)
		{
			string[] topLevelItems = SelectedPropertiesNode.GetTopLevelItems(selectClause);
			foreach (string text in topLevelItems)
			{
				int num = text.IndexOf('(');
				string[] array2;
				if (-1 == num)
				{
					array2 = text.Split(new char[] { '/' });
				}
				else
				{
					array2 = text.Substring(0, num).Split(new char[] { '/' });
					string[] array3 = array2;
					int num2 = array2.Length - 1;
					array3[num2] += text.Substring(num);
				}
				this.ParsePathSegment(array2, 0);
			}
			this.DetermineSelectionType();
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000175A4 File Offset: 0x000157A4
		private void DetermineSelectionType()
		{
			if (this.children != null)
			{
				foreach (SelectedPropertiesNode selectedPropertiesNode in this.children.Values)
				{
					selectedPropertiesNode.DetermineSelectionType();
				}
			}
			if (this.selectedProperties == null || this.selectedProperties.Count == 0)
			{
				if (this.children != null)
				{
					if (!this.children.Values.All((SelectedPropertiesNode n) => n.selectionType == SelectedPropertiesNode.SelectionType.EntireSubtree))
					{
						return;
					}
				}
				this.selectionType = SelectedPropertiesNode.SelectionType.EntireSubtree;
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00017658 File Offset: 0x00015858
		private static string[] GetTopLevelItems(string selectClause)
		{
			List<string> list = new List<string>();
			int num = 0;
			int num2 = 0;
			char[] array = selectClause.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				switch (array[i])
				{
				case '(':
					num++;
					break;
				case ')':
					num--;
					break;
				case ',':
					if (num == 0)
					{
						string text = selectClause.Substring(num2, i - num2);
						if (text.Length != 0)
						{
							list.Add(text);
						}
						num2 = i + 1;
					}
					break;
				}
			}
			if (num2 < array.Length)
			{
				list.Add(selectClause.Substring(num2, array.Length - num2));
			}
			return list.ToArray();
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x000176FE File Offset: 0x000158FE
		internal SelectedPropertiesNode(SelectedPropertiesNode.SelectionType selectionType)
			: this(selectionType, false)
		{
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00017708 File Offset: 0x00015908
		private SelectedPropertiesNode(SelectedPropertiesNode.SelectionType selectionType, bool isExpandedNavigationProperty)
		{
			this.selectionType = selectionType;
			this.isExpandedNavigationProperty = isExpandedNavigationProperty;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00017725 File Offset: 0x00015925
		internal static SelectedPropertiesNode Create(string selectQueryOption, IEdmStructuredType structuredType, IEdmModel edmModel)
		{
			if (selectQueryOption == null)
			{
				return new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.EntireSubtree);
			}
			selectQueryOption = selectQueryOption.Trim();
			if (selectQueryOption.Length == 0)
			{
				return SelectedPropertiesNode.Empty;
			}
			return new SelectedPropertiesNode(selectQueryOption, structuredType, edmModel);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001774F File Offset: 0x0001594F
		internal static SelectedPropertiesNode Create(string selectQueryOption)
		{
			return SelectedPropertiesNode.Create(selectQueryOption, null, null);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001775C File Offset: 0x0001595C
		internal static SelectedPropertiesNode Create(SelectExpandClause selectExpandClause)
		{
			if (selectExpandClause.AllSelected)
			{
				if (selectExpandClause.SelectedItems.OfType<ExpandedNavigationSelectItem>().All((ExpandedNavigationSelectItem _) => _.SelectAndExpand.AllSelected))
				{
					return new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.EntireSubtree);
				}
			}
			return SelectedPropertiesNode.CreateFromSelectExpandClause(selectExpandClause);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000177B0 File Offset: 0x000159B0
		internal static SelectedPropertiesNode CombineNodes(SelectedPropertiesNode left, SelectedPropertiesNode right)
		{
			if (left.selectionType == SelectedPropertiesNode.SelectionType.EntireSubtree || right.selectionType == SelectedPropertiesNode.SelectionType.EntireSubtree)
			{
				return new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.EntireSubtree);
			}
			if (left.selectionType == SelectedPropertiesNode.SelectionType.Empty)
			{
				return right;
			}
			if (right.selectionType == SelectedPropertiesNode.SelectionType.Empty)
			{
				return left;
			}
			SelectedPropertiesNode selectedPropertiesNode = new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.PartialSubtree)
			{
				hasWildcard = (left.hasWildcard | right.hasWildcard)
			};
			if (left.selectedProperties != null && right.selectedProperties != null)
			{
				selectedPropertiesNode.selectedProperties = SelectedPropertiesNode.CreateSelectedPropertiesHashSet(left.selectedProperties.AsEnumerable<string>().Concat(right.selectedProperties));
			}
			else if (left.selectedProperties != null)
			{
				selectedPropertiesNode.selectedProperties = SelectedPropertiesNode.CreateSelectedPropertiesHashSet(left.selectedProperties);
			}
			else if (right.selectedProperties != null)
			{
				selectedPropertiesNode.selectedProperties = SelectedPropertiesNode.CreateSelectedPropertiesHashSet(right.selectedProperties);
			}
			if (left.children != null && right.children != null)
			{
				selectedPropertiesNode.children = new Dictionary<string, SelectedPropertiesNode>(left.children);
				using (Dictionary<string, SelectedPropertiesNode>.Enumerator enumerator = right.children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, SelectedPropertiesNode> keyValuePair = enumerator.Current;
						SelectedPropertiesNode selectedPropertiesNode2;
						if (selectedPropertiesNode.children.TryGetValue(keyValuePair.Key, out selectedPropertiesNode2))
						{
							selectedPropertiesNode.children[keyValuePair.Key] = SelectedPropertiesNode.CombineNodes(selectedPropertiesNode2, keyValuePair.Value);
						}
						else
						{
							selectedPropertiesNode.children[keyValuePair.Key] = keyValuePair.Value;
						}
					}
					return selectedPropertiesNode;
				}
			}
			if (left.children != null)
			{
				selectedPropertiesNode.children = new Dictionary<string, SelectedPropertiesNode>(left.children);
			}
			else if (right.children != null)
			{
				selectedPropertiesNode.children = new Dictionary<string, SelectedPropertiesNode>(right.children);
			}
			return selectedPropertiesNode;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00017958 File Offset: 0x00015B58
		internal SelectedPropertiesNode GetSelectedPropertiesForNavigationProperty(IEdmStructuredType structuredType, string navigationPropertyName)
		{
			if (this.selectionType == SelectedPropertiesNode.SelectionType.Empty)
			{
				return SelectedPropertiesNode.Empty;
			}
			if (this.selectionType == SelectedPropertiesNode.SelectionType.EntireSubtree)
			{
				return new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.EntireSubtree);
			}
			if (structuredType == null)
			{
				return new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.EntireSubtree);
			}
			if (this.selectedProperties != null && this.selectedProperties.Contains(navigationPropertyName) && (this.children == null || !this.children.Any((KeyValuePair<string, SelectedPropertiesNode> n) => n.Key.Equals(navigationPropertyName) && n.Value.isExpandedNavigationProperty)))
			{
				return new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.EntireSubtree);
			}
			if (this.children != null)
			{
				SelectedPropertiesNode empty;
				if (!this.children.TryGetValue(navigationPropertyName, out empty))
				{
					empty = SelectedPropertiesNode.Empty;
				}
				return (from typeSegmentChild in this.GetMatchingTypeSegments(structuredType)
					select typeSegmentChild.GetSelectedPropertiesForNavigationProperty(structuredType, navigationPropertyName)).Aggregate(empty, new Func<SelectedPropertiesNode, SelectedPropertiesNode, SelectedPropertiesNode>(SelectedPropertiesNode.CombineNodes));
			}
			return SelectedPropertiesNode.Empty;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00017A44 File Offset: 0x00015C44
		internal IEnumerable<IEdmNavigationProperty> GetSelectedNavigationProperties(IEdmStructuredType structuredType)
		{
			if (this.selectionType == SelectedPropertiesNode.SelectionType.Empty)
			{
				return SelectedPropertiesNode.EmptyNavigationProperties;
			}
			if (structuredType == null)
			{
				return SelectedPropertiesNode.EmptyNavigationProperties;
			}
			if (this.selectionType != SelectedPropertiesNode.SelectionType.EntireSubtree && !this.hasWildcard)
			{
				if (this.selectedProperties == null || this.selectedProperties.Count<string>() == 0)
				{
					if (this.children.Values.All((SelectedPropertiesNode n) => n.isExpandedNavigationProperty))
					{
						goto IL_006E;
					}
				}
				IEnumerable<string> enumerable = this.selectedProperties ?? SelectedPropertiesNode.CreateSelectedPropertiesHashSet();
				if (this.children != null)
				{
					enumerable = this.children.Keys.Concat(enumerable);
				}
				IEnumerable<IEdmNavigationProperty> enumerable2 = enumerable.Select(new Func<string, IEdmProperty>(structuredType.FindProperty)).OfType<IEdmNavigationProperty>();
				foreach (SelectedPropertiesNode selectedPropertiesNode in this.GetMatchingTypeSegments(structuredType))
				{
					enumerable2 = enumerable2.Concat(selectedPropertiesNode.GetSelectedNavigationProperties(structuredType));
				}
				return enumerable2.Distinct<IEdmNavigationProperty>();
			}
			IL_006E:
			return structuredType.NavigationProperties();
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00017B58 File Offset: 0x00015D58
		internal IDictionary<string, IEdmStructuralProperty> GetSelectedStreamProperties(IEdmEntityType entityType)
		{
			if (this.selectionType == SelectedPropertiesNode.SelectionType.Empty)
			{
				return SelectedPropertiesNode.EmptyStreamProperties;
			}
			if (entityType == null)
			{
				return SelectedPropertiesNode.EmptyStreamProperties;
			}
			if (this.selectionType == SelectedPropertiesNode.SelectionType.EntireSubtree || this.hasWildcard)
			{
				return (from sp in entityType.StructuralProperties()
					where sp.Type.IsStream()
					select sp).ToDictionary((IEdmStructuralProperty sp) => sp.Name, StringComparer.Ordinal);
			}
			IDictionary<string, IEdmStructuralProperty> dictionary;
			if (this.selectedProperties != null)
			{
				dictionary = (from p in this.selectedProperties.Select(new Func<string, IEdmProperty>(entityType.FindProperty)).OfType<IEdmStructuralProperty>()
					where p.Type.IsStream()
					select p).ToDictionary((IEdmStructuralProperty p) => p.Name, StringComparer.Ordinal);
			}
			else
			{
				dictionary = new Dictionary<string, IEdmStructuralProperty>();
			}
			IDictionary<string, IEdmStructuralProperty> dictionary2 = dictionary;
			foreach (SelectedPropertiesNode selectedPropertiesNode in this.GetMatchingTypeSegments(entityType))
			{
				IDictionary<string, IEdmStructuralProperty> selectedStreamProperties = selectedPropertiesNode.GetSelectedStreamProperties(entityType);
				foreach (KeyValuePair<string, IEdmStructuralProperty> keyValuePair in selectedStreamProperties)
				{
					dictionary2[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			return dictionary2;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00017CE8 File Offset: 0x00015EE8
		internal bool IsOperationSelected(IEdmStructuredType structuredType, IEdmOperation operation, bool mustBeNamespaceQualified)
		{
			mustBeNamespaceQualified = mustBeNamespaceQualified || structuredType.FindProperty(operation.Name) != null;
			return this.IsOperationSelectedAtThisLevel(operation, mustBeNamespaceQualified) || this.GetMatchingTypeSegments(structuredType).Any((SelectedPropertiesNode typeSegment) => typeSegment.IsOperationSelectedAtThisLevel(operation, mustBeNamespaceQualified));
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00017D5C File Offset: 0x00015F5C
		internal bool IsEntireSubtree()
		{
			return this.selectionType == SelectedPropertiesNode.SelectionType.EntireSubtree;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00017D67 File Offset: 0x00015F67
		private static IEnumerable<IEdmStructuredType> GetBaseTypesAndSelf(IEdmStructuredType structuredType)
		{
			IEdmStructuredType currentType;
			for (currentType = structuredType; currentType != null; currentType = currentType.BaseType())
			{
				yield return currentType;
			}
			currentType = null;
			yield break;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00017D78 File Offset: 0x00015F78
		private static HashSet<string> CreateSelectedPropertiesHashSet(IEnumerable<string> properties)
		{
			HashSet<string> hashSet = SelectedPropertiesNode.CreateSelectedPropertiesHashSet();
			foreach (string text in properties)
			{
				hashSet.Add(text);
			}
			return hashSet;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00017DC8 File Offset: 0x00015FC8
		private static HashSet<string> CreateSelectedPropertiesHashSet()
		{
			return new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00017DD4 File Offset: 0x00015FD4
		private static IEnumerable<string> GetPossibleMatchesForSelectedOperation(IEdmOperation operation, bool mustBeNamespaceQualified)
		{
			string operationName = operation.Name;
			string operationNameWithParameters = operation.NameWithParameters();
			if (!mustBeNamespaceQualified)
			{
				yield return operationName;
				yield return operationNameWithParameters;
			}
			string qualifiedContainerName = operation.Namespace + ".";
			yield return qualifiedContainerName + "*";
			yield return qualifiedContainerName + operationName;
			yield return qualifiedContainerName + operationNameWithParameters;
			yield break;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00017DEC File Offset: 0x00015FEC
		private bool IsValidExpandToken(string item)
		{
			int num = item.IndexOf('(');
			return num != -1 && item.EndsWith(")", StringComparison.Ordinal) && this.IsNavigationPropertyToken(item.Substring(0, num));
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00017E28 File Offset: 0x00016028
		private bool IsNavigationPropertyToken(string token)
		{
			return token.IndexOf('.') == -1 && (this.structuredType == null || this.structuredType.NavigationProperties().Any((IEdmNavigationProperty _) => _.Name.Equals(token, StringComparison.Ordinal)) || this.edmModel == null || !this.edmModel.FindBoundOperations(this.structuredType).Any((IEdmOperation op) => op.Name.Equals(token, StringComparison.Ordinal)));
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00017EB3 File Offset: 0x000160B3
		private IEnumerable<SelectedPropertiesNode> GetMatchingTypeSegments(IEdmStructuredType structuredType)
		{
			if (this.children != null)
			{
				foreach (IEdmStructuredType edmStructuredType in SelectedPropertiesNode.GetBaseTypesAndSelf(structuredType))
				{
					SelectedPropertiesNode selectedPropertiesNode;
					if (this.children.TryGetValue(edmStructuredType.FullTypeName(), out selectedPropertiesNode))
					{
						if (selectedPropertiesNode.hasWildcard)
						{
							throw new ODataException(Strings.SelectedPropertiesNode_StarSegmentAfterTypeSegment);
						}
						yield return selectedPropertiesNode;
					}
				}
				IEnumerator<IEdmStructuredType> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00017ECC File Offset: 0x000160CC
		private void ParsePathSegment(string[] segments, int index)
		{
			string text = segments[index].Trim();
			bool flag = string.CompareOrdinal("*", text) == 0;
			int num = text.IndexOf('(');
			if (num != -1 && this.IsValidExpandToken(text))
			{
				string token = text.Substring(0, num);
				SelectedPropertiesNode selectedPropertiesNode = this.EnsureChildNode(token, true);
				selectedPropertiesNode.edmModel = this.edmModel;
				if (num < text.Length - 2)
				{
					string text2 = text.Substring(num + 1, text.Length - num - 2).Trim();
					if (!string.IsNullOrEmpty(text2))
					{
						IEdmStructuredType edmStructuredType = this.structuredType;
						IEdmNavigationProperty edmNavigationProperty;
						if (edmStructuredType == null)
						{
							edmNavigationProperty = null;
						}
						else
						{
							IEnumerable<IEdmNavigationProperty> enumerable = edmStructuredType.DeclaredNavigationProperties();
							edmNavigationProperty = ((enumerable != null) ? enumerable.SingleOrDefault((IEdmNavigationProperty p) => p.Name.Equals(token, StringComparison.Ordinal)) : null);
						}
						IEdmNavigationProperty edmNavigationProperty2 = edmNavigationProperty;
						if (((edmNavigationProperty2 != null) ? edmNavigationProperty2.Type : null) != null)
						{
							selectedPropertiesNode.structuredType = edmNavigationProperty2.Type.Definition.AsElementType() as IEdmStructuredType;
						}
						selectedPropertiesNode.Parse(text2);
					}
				}
				else
				{
					selectedPropertiesNode.selectionType = SelectedPropertiesNode.SelectionType.EntireSubtree;
				}
			}
			else if (index != segments.Length - 1)
			{
				if (flag)
				{
					throw new ODataException(Strings.SelectedPropertiesNode_StarSegmentNotLastSegment);
				}
				SelectedPropertiesNode selectedPropertiesNode2 = this.EnsureChildNode(text, false);
				selectedPropertiesNode2.ParsePathSegment(segments, index + 1);
			}
			else
			{
				if (this.selectedProperties == null)
				{
					this.selectedProperties = SelectedPropertiesNode.CreateSelectedPropertiesHashSet();
				}
				this.selectedProperties.Add(text);
			}
			this.hasWildcard = this.hasWildcard || flag;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00018040 File Offset: 0x00016240
		private SelectedPropertiesNode EnsureChildNode(string segmentName, bool isExpandedNavigationProperty)
		{
			if (this.children == null)
			{
				this.children = new Dictionary<string, SelectedPropertiesNode>(StringComparer.Ordinal);
			}
			SelectedPropertiesNode selectedPropertiesNode;
			if (!this.children.TryGetValue(segmentName, out selectedPropertiesNode))
			{
				selectedPropertiesNode = new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.PartialSubtree, isExpandedNavigationProperty);
				this.children.Add(segmentName, selectedPropertiesNode);
			}
			return selectedPropertiesNode;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0001808B File Offset: 0x0001628B
		private bool IsOperationSelectedAtThisLevel(IEdmOperation operation, bool mustBeNamespaceQualified)
		{
			return this.selectionType == SelectedPropertiesNode.SelectionType.EntireSubtree || (this.selectionType != SelectedPropertiesNode.SelectionType.Empty && this.selectedProperties != null && SelectedPropertiesNode.GetPossibleMatchesForSelectedOperation(operation, mustBeNamespaceQualified).Any((string possibleMatch) => this.selectedProperties.Contains(possibleMatch)));
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x000180C4 File Offset: 0x000162C4
		private static SelectedPropertiesNode CreateFromSelectExpandClause(SelectExpandClause selectExpandClause)
		{
			SelectedPropertiesNode selectedPropertiesNode;
			selectExpandClause.Traverse(new Func<string, SelectedPropertiesNode, SelectedPropertiesNode>(SelectedPropertiesNode.ProcessSubExpand), new Func<IList<string>, IList<SelectedPropertiesNode>, SelectedPropertiesNode>(SelectedPropertiesNode.CombineSelectAndExpandResult), null, out selectedPropertiesNode);
			return selectedPropertiesNode;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000180F3 File Offset: 0x000162F3
		private static SelectedPropertiesNode ProcessSubExpand(string nodeName, SelectedPropertiesNode subExpandNode)
		{
			if (subExpandNode != null)
			{
				subExpandNode.nodeName = nodeName;
			}
			return subExpandNode;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00018100 File Offset: 0x00016300
		private static SelectedPropertiesNode CombineSelectAndExpandResult(IEnumerable<string> selectList, IEnumerable<SelectedPropertiesNode> expandList)
		{
			List<string> list = selectList.ToList<string>();
			list.RemoveAll(new Predicate<string>(expandList.Select(new Func<SelectedPropertiesNode, string>(SelectedPropertiesNode.<>c.<>9, ldftn(<CombineSelectAndExpandResult>b__43_0))).Contains<string>));
			if (list.Count == 0)
			{
				if (expandList.All((SelectedPropertiesNode n) => n.IsEntireSubtree()))
				{
					return new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.EntireSubtree);
				}
			}
			SelectedPropertiesNode selectedPropertiesNode = new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.PartialSubtree)
			{
				selectedProperties = ((list.Count > 0) ? SelectedPropertiesNode.CreateSelectedPropertiesHashSet() : null),
				children = new Dictionary<string, SelectedPropertiesNode>(StringComparer.Ordinal)
			};
			foreach (string text in list)
			{
				if ("*" == text)
				{
					selectedPropertiesNode.hasWildcard = true;
				}
				else
				{
					selectedPropertiesNode.selectedProperties.Add(text);
				}
			}
			foreach (SelectedPropertiesNode selectedPropertiesNode2 in expandList)
			{
				selectedPropertiesNode.children[selectedPropertiesNode2.nodeName] = selectedPropertiesNode2;
			}
			return selectedPropertiesNode;
		}

		// Token: 0x04000349 RID: 841
		internal static readonly SelectedPropertiesNode Empty = new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.Empty);

		// Token: 0x0400034A RID: 842
		internal static readonly SelectedPropertiesNode EntireSubtree = new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.EntireSubtree);

		// Token: 0x0400034B RID: 843
		private static readonly Dictionary<string, IEdmStructuralProperty> EmptyStreamProperties = new Dictionary<string, IEdmStructuralProperty>(StringComparer.Ordinal);

		// Token: 0x0400034C RID: 844
		private static readonly IEnumerable<IEdmNavigationProperty> EmptyNavigationProperties = Enumerable.Empty<IEdmNavigationProperty>();

		// Token: 0x0400034D RID: 845
		private SelectedPropertiesNode.SelectionType selectionType = SelectedPropertiesNode.SelectionType.PartialSubtree;

		// Token: 0x0400034E RID: 846
		internal readonly bool isExpandedNavigationProperty;

		// Token: 0x0400034F RID: 847
		private IEdmStructuredType structuredType;

		// Token: 0x04000350 RID: 848
		private IEdmModel edmModel;

		// Token: 0x04000351 RID: 849
		private const char PathSeparator = '/';

		// Token: 0x04000352 RID: 850
		private const char ItemSeparator = ',';

		// Token: 0x04000353 RID: 851
		private const string StarSegment = "*";

		// Token: 0x04000354 RID: 852
		private HashSet<string> selectedProperties;

		// Token: 0x04000355 RID: 853
		private Dictionary<string, SelectedPropertiesNode> children;

		// Token: 0x04000356 RID: 854
		private bool hasWildcard;

		// Token: 0x04000357 RID: 855
		private string nodeName;

		// Token: 0x02000332 RID: 818
		internal enum SelectionType
		{
			// Token: 0x04000DA5 RID: 3493
			Empty,
			// Token: 0x04000DA6 RID: 3494
			EntireSubtree,
			// Token: 0x04000DA7 RID: 3495
			PartialSubtree
		}
	}
}
