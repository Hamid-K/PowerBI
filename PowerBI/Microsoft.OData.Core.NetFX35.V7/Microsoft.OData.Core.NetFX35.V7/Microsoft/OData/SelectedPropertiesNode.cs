using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x020000AE RID: 174
	internal sealed class SelectedPropertiesNode
	{
		// Token: 0x060006B5 RID: 1717 RVA: 0x00012BD0 File Offset: 0x00010DD0
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

		// Token: 0x060006B6 RID: 1718 RVA: 0x00012C26 File Offset: 0x00010E26
		private SelectedPropertiesNode(SelectedPropertiesNode.SelectionType selectionType)
		{
			this.selectionType = selectionType;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00012C35 File Offset: 0x00010E35
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

		// Token: 0x060006B8 RID: 1720 RVA: 0x00012C5C File Offset: 0x00010E5C
		internal static SelectedPropertiesNode Create(SelectExpandClause selectExpandClause)
		{
			if (selectExpandClause.AllSelected)
			{
				return SelectedPropertiesNode.EntireSubtree;
			}
			return SelectedPropertiesNode.CreateFromSelectExpandClause(selectExpandClause);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00012C74 File Offset: 0x00010E74
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

		// Token: 0x060006BA RID: 1722 RVA: 0x00012E1C File Offset: 0x0001101C
		internal SelectedPropertiesNode GetSelectedPropertiesForNavigationProperty(IEdmStructuredType structuredType, string navigationPropertyName)
		{
			if (this.selectionType == SelectedPropertiesNode.SelectionType.Empty)
			{
				return SelectedPropertiesNode.Empty;
			}
			if (this.selectionType == SelectedPropertiesNode.SelectionType.EntireSubtree)
			{
				return SelectedPropertiesNode.EntireSubtree;
			}
			if (structuredType == null)
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
				return Enumerable.Aggregate<SelectedPropertiesNode, SelectedPropertiesNode>(Enumerable.Select<SelectedPropertiesNode, SelectedPropertiesNode>(this.GetMatchingTypeSegments(structuredType), (SelectedPropertiesNode typeSegmentChild) => typeSegmentChild.GetSelectedPropertiesForNavigationProperty(structuredType, navigationPropertyName)), empty, new Func<SelectedPropertiesNode, SelectedPropertiesNode, SelectedPropertiesNode>(SelectedPropertiesNode.CombineNodes));
			}
			return SelectedPropertiesNode.Empty;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00012EDC File Offset: 0x000110DC
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
			if (this.selectionType == SelectedPropertiesNode.SelectionType.EntireSubtree || this.hasWildcard)
			{
				return structuredType.NavigationProperties();
			}
			IEnumerable<string> enumerable = this.selectedProperties;
			if (this.children != null)
			{
				enumerable = Enumerable.Concat<string>(this.children.Keys, enumerable);
			}
			IEnumerable<IEdmNavigationProperty> enumerable2 = Enumerable.OfType<IEdmNavigationProperty>(Enumerable.Select<string, IEdmProperty>(enumerable, new Func<string, IEdmProperty>(structuredType.FindProperty)));
			foreach (SelectedPropertiesNode selectedPropertiesNode in this.GetMatchingTypeSegments(structuredType))
			{
				enumerable2 = Enumerable.Concat<IEdmNavigationProperty>(enumerable2, selectedPropertiesNode.GetSelectedNavigationProperties(structuredType));
			}
			return Enumerable.Distinct<IEdmNavigationProperty>(enumerable2);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00012FA0 File Offset: 0x000111A0
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

		// Token: 0x060006BD RID: 1725 RVA: 0x00013124 File Offset: 0x00011324
		internal bool IsOperationSelected(IEdmStructuredType structuredType, IEdmOperation operation, bool mustBeNamespaceQualified)
		{
			mustBeNamespaceQualified = mustBeNamespaceQualified || structuredType.FindProperty(operation.Name) != null;
			return this.IsOperationSelectedAtThisLevel(operation, mustBeNamespaceQualified) || Enumerable.Any<SelectedPropertiesNode>(this.GetMatchingTypeSegments(structuredType), (SelectedPropertiesNode typeSegment) => typeSegment.IsOperationSelectedAtThisLevel(operation, mustBeNamespaceQualified));
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00013198 File Offset: 0x00011398
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

		// Token: 0x060006BF RID: 1727 RVA: 0x000131A8 File Offset: 0x000113A8
		private static HashSet<string> CreateSelectedPropertiesHashSet(IEnumerable<string> properties)
		{
			HashSet<string> hashSet = SelectedPropertiesNode.CreateSelectedPropertiesHashSet();
			foreach (string text in properties)
			{
				hashSet.Add(text);
			}
			return hashSet;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000131F8 File Offset: 0x000113F8
		private static HashSet<string> CreateSelectedPropertiesHashSet()
		{
			return new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00013204 File Offset: 0x00011404
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

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001321B File Offset: 0x0001141B
		private IEnumerable<SelectedPropertiesNode> GetMatchingTypeSegments(IEdmStructuredType structuredType)
		{
			if (this.children != null)
			{
				foreach (IEdmStructuredType edmStructuredType in SelectedPropertiesNode.GetBaseTypesAndSelf(structuredType))
				{
					SelectedPropertiesNode selectedPropertiesNode;
					if (this.children.TryGetValue(edmStructuredType.FullTypeName(), ref selectedPropertiesNode))
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

		// Token: 0x060006C3 RID: 1731 RVA: 0x00013234 File Offset: 0x00011434
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

		// Token: 0x060006C4 RID: 1732 RVA: 0x000132B8 File Offset: 0x000114B8
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

		// Token: 0x060006C5 RID: 1733 RVA: 0x00013302 File Offset: 0x00011502
		private bool IsOperationSelectedAtThisLevel(IEdmOperation operation, bool mustBeNamespaceQualified)
		{
			return this.selectionType != SelectedPropertiesNode.SelectionType.Empty && (this.selectionType == SelectedPropertiesNode.SelectionType.EntireSubtree || Enumerable.Any<string>(SelectedPropertiesNode.GetPossibleMatchesForSelectedOperation(operation, mustBeNamespaceQualified), (string possibleMatch) => this.selectedProperties.Contains(possibleMatch)));
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00013334 File Offset: 0x00011534
		private static SelectedPropertiesNode CreateFromSelectExpandClause(SelectExpandClause selectExpandClause)
		{
			SelectedPropertiesNode selectedPropertiesNode;
			selectExpandClause.Traverse(new Func<string, SelectedPropertiesNode, SelectedPropertiesNode>(SelectedPropertiesNode.ProcessSubExpand), new Func<IList<string>, IList<SelectedPropertiesNode>, SelectedPropertiesNode>(SelectedPropertiesNode.CombineSelectAndExpandResult), out selectedPropertiesNode);
			return selectedPropertiesNode;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00013362 File Offset: 0x00011562
		private static SelectedPropertiesNode ProcessSubExpand(string nodeName, SelectedPropertiesNode subExpandNode)
		{
			if (subExpandNode != null)
			{
				subExpandNode.nodeName = nodeName;
			}
			return subExpandNode;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00013370 File Offset: 0x00011570
		private static SelectedPropertiesNode CombineSelectAndExpandResult(IList<string> selectList, IList<SelectedPropertiesNode> expandList)
		{
			List<string> list = Enumerable.ToList<string>(selectList);
			list.RemoveAll(new Predicate<string>(Enumerable.Select<SelectedPropertiesNode, string>(expandList, new Func<SelectedPropertiesNode, string>(SelectedPropertiesNode.<>c.<>9, ldftn(<CombineSelectAndExpandResult>b__32_0))).Contains<string>));
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

		// Token: 0x040002E9 RID: 745
		internal static readonly SelectedPropertiesNode Empty = new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.Empty);

		// Token: 0x040002EA RID: 746
		internal static readonly SelectedPropertiesNode EntireSubtree = new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.EntireSubtree);

		// Token: 0x040002EB RID: 747
		private static readonly Dictionary<string, IEdmStructuralProperty> EmptyStreamProperties = new Dictionary<string, IEdmStructuralProperty>(StringComparer.Ordinal);

		// Token: 0x040002EC RID: 748
		private static readonly IEnumerable<IEdmNavigationProperty> EmptyNavigationProperties = Enumerable.Empty<IEdmNavigationProperty>();

		// Token: 0x040002ED RID: 749
		private readonly SelectedPropertiesNode.SelectionType selectionType;

		// Token: 0x040002EE RID: 750
		private const char PathSeparator = '/';

		// Token: 0x040002EF RID: 751
		private const char ItemSeparator = ',';

		// Token: 0x040002F0 RID: 752
		private const string StarSegment = "*";

		// Token: 0x040002F1 RID: 753
		private HashSet<string> selectedProperties;

		// Token: 0x040002F2 RID: 754
		private Dictionary<string, SelectedPropertiesNode> children;

		// Token: 0x040002F3 RID: 755
		private bool hasWildcard;

		// Token: 0x040002F4 RID: 756
		private string nodeName;

		// Token: 0x02000296 RID: 662
		private enum SelectionType
		{
			// Token: 0x04000B88 RID: 2952
			Empty,
			// Token: 0x04000B89 RID: 2953
			EntireSubtree,
			// Token: 0x04000B8A RID: 2954
			PartialSubtree
		}
	}
}
