using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x020001B6 RID: 438
	internal sealed class SelectedPropertiesNode
	{
		// Token: 0x06001041 RID: 4161 RVA: 0x000385A8 File Offset: 0x000367A8
		internal SelectedPropertiesNode(string selectClause)
			: this(SelectedPropertiesNode.SelectionType.PartialSubtree)
		{
			string[] array = selectClause.Split(new char[] { ',' });
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[] { '/' });
				this.ParsePathSegment(array3, 0);
			}
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0003860B File Offset: 0x0003680B
		private SelectedPropertiesNode(SelectedPropertiesNode.SelectionType selectionType)
		{
			this.selectionType = selectionType;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0003861A File Offset: 0x0003681A
		internal static SelectedPropertiesNode Create(string selectQueryOption)
		{
			if (selectQueryOption == null)
			{
				return SelectedPropertiesNode.EntireSubtree;
			}
			selectQueryOption = selectQueryOption.Trim();
			if (selectQueryOption.Length == 0)
			{
				return SelectedPropertiesNode.Empty;
			}
			return new SelectedPropertiesNode(selectQueryOption);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00038641 File Offset: 0x00036841
		internal static SelectedPropertiesNode Create(SelectExpandClause selectExpandClause)
		{
			if (selectExpandClause.AllSelected)
			{
				return SelectedPropertiesNode.EntireSubtree;
			}
			return SelectedPropertiesNode.CreateFromSelectExpandClause(selectExpandClause);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00038658 File Offset: 0x00036858
		internal static SelectedPropertiesNode CombineNodes(SelectedPropertiesNode left, SelectedPropertiesNode right)
		{
			if (left.selectionType == SelectedPropertiesNode.SelectionType.EntireSubtree || right.selectionType == SelectedPropertiesNode.SelectionType.EntireSubtree)
			{
				return SelectedPropertiesNode.EntireSubtree;
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
				selectedPropertiesNode.selectedProperties = SelectedPropertiesNode.CreateSelectedPropertiesHashSet(Enumerable.Concat<string>(Enumerable.AsEnumerable<string>(left.selectedProperties), right.selectedProperties));
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
						if (selectedPropertiesNode.children.TryGetValue(keyValuePair.Key, ref selectedPropertiesNode2))
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

		// Token: 0x06001046 RID: 4166 RVA: 0x0003881C File Offset: 0x00036A1C
		internal SelectedPropertiesNode GetSelectedPropertiesForNavigationProperty(IEdmEntityType entityType, string navigationPropertyName)
		{
			if (this.selectionType == SelectedPropertiesNode.SelectionType.Empty)
			{
				return SelectedPropertiesNode.Empty;
			}
			if (this.selectionType == SelectedPropertiesNode.SelectionType.EntireSubtree)
			{
				return SelectedPropertiesNode.EntireSubtree;
			}
			if (entityType == null)
			{
				return SelectedPropertiesNode.EntireSubtree;
			}
			if (this.selectedProperties.Contains(navigationPropertyName))
			{
				return SelectedPropertiesNode.EntireSubtree;
			}
			if (this.children != null)
			{
				SelectedPropertiesNode empty;
				if (!this.children.TryGetValue(navigationPropertyName, ref empty))
				{
					empty = SelectedPropertiesNode.Empty;
				}
				return Enumerable.Aggregate<SelectedPropertiesNode, SelectedPropertiesNode>(Enumerable.Select<SelectedPropertiesNode, SelectedPropertiesNode>(this.GetMatchingTypeSegments(entityType), (SelectedPropertiesNode typeSegmentChild) => typeSegmentChild.GetSelectedPropertiesForNavigationProperty(entityType, navigationPropertyName)), empty, new Func<SelectedPropertiesNode, SelectedPropertiesNode, SelectedPropertiesNode>(SelectedPropertiesNode.CombineNodes));
			}
			return SelectedPropertiesNode.Empty;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x000388E0 File Offset: 0x00036AE0
		internal IEnumerable<IEdmNavigationProperty> GetSelectedNavigationProperties(IEdmEntityType entityType)
		{
			if (this.selectionType == SelectedPropertiesNode.SelectionType.Empty)
			{
				return SelectedPropertiesNode.EmptyNavigationProperties;
			}
			if (entityType == null)
			{
				return SelectedPropertiesNode.EmptyNavigationProperties;
			}
			if (this.selectionType == SelectedPropertiesNode.SelectionType.EntireSubtree || this.hasWildcard)
			{
				return entityType.NavigationProperties();
			}
			IEnumerable<string> enumerable = this.selectedProperties;
			if (this.children != null)
			{
				enumerable = Enumerable.Concat<string>(this.children.Keys, enumerable);
			}
			IEnumerable<IEdmNavigationProperty> enumerable2 = Enumerable.OfType<IEdmNavigationProperty>(Enumerable.Select<string, IEdmProperty>(enumerable, new Func<string, IEdmProperty>(entityType.FindProperty)));
			foreach (SelectedPropertiesNode selectedPropertiesNode in this.GetMatchingTypeSegments(entityType))
			{
				enumerable2 = Enumerable.Concat<IEdmNavigationProperty>(enumerable2, selectedPropertiesNode.GetSelectedNavigationProperties(entityType));
			}
			return Enumerable.Distinct<IEdmNavigationProperty>(enumerable2);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000389D0 File Offset: 0x00036BD0
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
				return Enumerable.ToDictionary<IEdmStructuralProperty, string>(Enumerable.Where<IEdmStructuralProperty>(entityType.StructuralProperties(), (IEdmStructuralProperty sp) => sp.Type.IsStream()), (IEdmStructuralProperty sp) => sp.Name, StringComparer.Ordinal);
			}
			IDictionary<string, IEdmStructuralProperty> dictionary = Enumerable.ToDictionary<IEdmStructuralProperty, string>(Enumerable.Where<IEdmStructuralProperty>(Enumerable.OfType<IEdmStructuralProperty>(Enumerable.Select<string, IEdmProperty>(this.selectedProperties, new Func<string, IEdmProperty>(entityType.FindProperty))), (IEdmStructuralProperty p) => p.Type.IsStream()), (IEdmStructuralProperty p) => p.Name, StringComparer.Ordinal);
			foreach (SelectedPropertiesNode selectedPropertiesNode in this.GetMatchingTypeSegments(entityType))
			{
				IDictionary<string, IEdmStructuralProperty> selectedStreamProperties = selectedPropertiesNode.GetSelectedStreamProperties(entityType);
				foreach (KeyValuePair<string, IEdmStructuralProperty> keyValuePair in selectedStreamProperties)
				{
					dictionary[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			return dictionary;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00038B6C File Offset: 0x00036D6C
		internal bool IsOperationSelected(IEdmEntityType entityType, IEdmOperation operation, bool mustBeNamespaceQualified)
		{
			mustBeNamespaceQualified = mustBeNamespaceQualified || entityType.FindProperty(operation.Name) != null;
			return this.IsOperationSelectedAtThisLevel(operation, mustBeNamespaceQualified) || Enumerable.Any<SelectedPropertiesNode>(this.GetMatchingTypeSegments(entityType), (SelectedPropertiesNode typeSegment) => typeSegment.IsOperationSelectedAtThisLevel(operation, mustBeNamespaceQualified));
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00038CDC File Offset: 0x00036EDC
		private static IEnumerable<IEdmEntityType> GetBaseTypesAndSelf(IEdmEntityType entityType)
		{
			for (IEdmEntityType currentType = entityType; currentType != null; currentType = currentType.BaseEntityType())
			{
				yield return currentType;
			}
			yield break;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00038CFC File Offset: 0x00036EFC
		private static HashSet<string> CreateSelectedPropertiesHashSet(IEnumerable<string> properties)
		{
			HashSet<string> hashSet = SelectedPropertiesNode.CreateSelectedPropertiesHashSet();
			foreach (string text in properties)
			{
				hashSet.Add(text);
			}
			return hashSet;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00038D4C File Offset: 0x00036F4C
		private static HashSet<string> CreateSelectedPropertiesHashSet()
		{
			return new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00038F1C File Offset: 0x0003711C
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

		// Token: 0x0600104E RID: 4174 RVA: 0x0003912C File Offset: 0x0003732C
		private IEnumerable<SelectedPropertiesNode> GetMatchingTypeSegments(IEdmEntityType entityType)
		{
			if (this.children != null)
			{
				foreach (IEdmEntityType currentType in SelectedPropertiesNode.GetBaseTypesAndSelf(entityType))
				{
					SelectedPropertiesNode typeSegmentChild;
					if (this.children.TryGetValue(currentType.FullName(), ref typeSegmentChild))
					{
						if (typeSegmentChild.hasWildcard)
						{
							throw new ODataException(Strings.SelectedPropertiesNode_StarSegmentAfterTypeSegment);
						}
						yield return typeSegmentChild;
					}
				}
			}
			yield break;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00039150 File Offset: 0x00037350
		private void ParsePathSegment(string[] segments, int index)
		{
			string text = segments[index].Trim();
			if (this.selectedProperties == null)
			{
				this.selectedProperties = SelectedPropertiesNode.CreateSelectedPropertiesHashSet();
			}
			bool flag = string.CompareOrdinal("*", text) == 0;
			if (index != segments.Length - 1)
			{
				if (flag)
				{
					throw new ODataException(Strings.SelectedPropertiesNode_StarSegmentNotLastSegment);
				}
				SelectedPropertiesNode selectedPropertiesNode = this.EnsureChildAnnotation(text);
				selectedPropertiesNode.ParsePathSegment(segments, index + 1);
			}
			else
			{
				this.selectedProperties.Add(text);
			}
			this.hasWildcard = this.hasWildcard || flag;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000391D4 File Offset: 0x000373D4
		private SelectedPropertiesNode EnsureChildAnnotation(string segmentName)
		{
			if (this.children == null)
			{
				this.children = new Dictionary<string, SelectedPropertiesNode>(StringComparer.Ordinal);
			}
			SelectedPropertiesNode selectedPropertiesNode;
			if (!this.children.TryGetValue(segmentName, ref selectedPropertiesNode))
			{
				selectedPropertiesNode = new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.PartialSubtree);
				this.children.Add(segmentName, selectedPropertiesNode);
			}
			return selectedPropertiesNode;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0003922C File Offset: 0x0003742C
		private bool IsOperationSelectedAtThisLevel(IEdmOperation operation, bool mustBeNamespaceQualified)
		{
			return this.selectionType != SelectedPropertiesNode.SelectionType.Empty && (this.selectionType == SelectedPropertiesNode.SelectionType.EntireSubtree || Enumerable.Any<string>(SelectedPropertiesNode.GetPossibleMatchesForSelectedOperation(operation, mustBeNamespaceQualified), (string possibleMatch) => this.selectedProperties.Contains(possibleMatch)));
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0003925C File Offset: 0x0003745C
		private static SelectedPropertiesNode CreateFromSelectExpandClause(SelectExpandClause selectExpandClause)
		{
			SelectedPropertiesNode selectedPropertiesNode;
			selectExpandClause.Traverse(new Func<string, SelectedPropertiesNode, SelectedPropertiesNode>(SelectedPropertiesNode.ProcessSubExpand), new Func<IList<string>, IList<SelectedPropertiesNode>, SelectedPropertiesNode>(SelectedPropertiesNode.CombineSelectAndExpandResult), out selectedPropertiesNode);
			return selectedPropertiesNode;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0003928A File Offset: 0x0003748A
		private static SelectedPropertiesNode ProcessSubExpand(string nodeName, SelectedPropertiesNode subExpandNode)
		{
			if (subExpandNode != null)
			{
				subExpandNode.nodeName = nodeName;
			}
			return subExpandNode;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x000392A0 File Offset: 0x000374A0
		private static SelectedPropertiesNode CombineSelectAndExpandResult(IList<string> selectList, IList<SelectedPropertiesNode> expandList)
		{
			List<string> list = Enumerable.ToList<string>(selectList);
			list.RemoveAll(new Predicate<string>(Enumerable.Select<SelectedPropertiesNode, string>(expandList, new Func<SelectedPropertiesNode, string>(null, ldftn(<CombineSelectAndExpandResult>b__23))).Contains<string>));
			SelectedPropertiesNode selectedPropertiesNode = new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.PartialSubtree)
			{
				selectedProperties = SelectedPropertiesNode.CreateSelectedPropertiesHashSet(),
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

		// Token: 0x04000752 RID: 1874
		private const char PathSeparator = '/';

		// Token: 0x04000753 RID: 1875
		private const char ItemSeparator = ',';

		// Token: 0x04000754 RID: 1876
		internal static readonly SelectedPropertiesNode Empty = new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.Empty);

		// Token: 0x04000755 RID: 1877
		internal static readonly SelectedPropertiesNode EntireSubtree = new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.EntireSubtree);

		// Token: 0x04000756 RID: 1878
		private static readonly Dictionary<string, IEdmStructuralProperty> EmptyStreamProperties = new Dictionary<string, IEdmStructuralProperty>(StringComparer.Ordinal);

		// Token: 0x04000757 RID: 1879
		private static readonly IEnumerable<IEdmNavigationProperty> EmptyNavigationProperties = Enumerable.Empty<IEdmNavigationProperty>();

		// Token: 0x04000758 RID: 1880
		private readonly SelectedPropertiesNode.SelectionType selectionType;

		// Token: 0x04000759 RID: 1881
		private HashSet<string> selectedProperties;

		// Token: 0x0400075A RID: 1882
		private Dictionary<string, SelectedPropertiesNode> children;

		// Token: 0x0400075B RID: 1883
		private bool hasWildcard;

		// Token: 0x0400075C RID: 1884
		private string nodeName;

		// Token: 0x020001B7 RID: 439
		private enum SelectionType
		{
			// Token: 0x04000763 RID: 1891
			Empty,
			// Token: 0x04000764 RID: 1892
			EntireSubtree,
			// Token: 0x04000765 RID: 1893
			PartialSubtree
		}
	}
}
