using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000160 RID: 352
	internal sealed class SelectedPropertiesNode
	{
		// Token: 0x06000957 RID: 2391 RVA: 0x0001D23C File Offset: 0x0001B43C
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

		// Token: 0x06000958 RID: 2392 RVA: 0x0001D29F File Offset: 0x0001B49F
		private SelectedPropertiesNode(SelectedPropertiesNode.SelectionType selectionType)
		{
			this.selectionType = selectionType;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0001D2AE File Offset: 0x0001B4AE
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

		// Token: 0x0600095A RID: 2394 RVA: 0x0001D2D8 File Offset: 0x0001B4D8
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

		// Token: 0x0600095B RID: 2395 RVA: 0x0001D49C File Offset: 0x0001B69C
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

		// Token: 0x0600095C RID: 2396 RVA: 0x0001D560 File Offset: 0x0001B760
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

		// Token: 0x0600095D RID: 2397 RVA: 0x0001D650 File Offset: 0x0001B850
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

		// Token: 0x0600095E RID: 2398 RVA: 0x0001D7EC File Offset: 0x0001B9EC
		internal bool IsOperationSelected(IEdmEntityType entityType, IEdmFunctionImport operation, bool mustBeContainerQualified)
		{
			mustBeContainerQualified = mustBeContainerQualified || entityType.FindProperty(operation.Name) != null;
			return this.IsOperationSelectedAtThisLevel(operation, mustBeContainerQualified) || Enumerable.Any<SelectedPropertiesNode>(this.GetMatchingTypeSegments(entityType), (SelectedPropertiesNode typeSegment) => typeSegment.IsOperationSelectedAtThisLevel(operation, mustBeContainerQualified));
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001D95C File Offset: 0x0001BB5C
		private static IEnumerable<IEdmEntityType> GetBaseTypesAndSelf(IEdmEntityType entityType)
		{
			for (IEdmEntityType currentType = entityType; currentType != null; currentType = currentType.BaseEntityType())
			{
				yield return currentType;
			}
			yield break;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0001D97C File Offset: 0x0001BB7C
		private static HashSet<string> CreateSelectedPropertiesHashSet(IEnumerable<string> properties)
		{
			HashSet<string> hashSet = SelectedPropertiesNode.CreateSelectedPropertiesHashSet();
			foreach (string text in properties)
			{
				hashSet.Add(text);
			}
			return hashSet;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0001D9CC File Offset: 0x0001BBCC
		private static HashSet<string> CreateSelectedPropertiesHashSet()
		{
			return new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0001DC44 File Offset: 0x0001BE44
		private static IEnumerable<string> GetPossibleMatchesForSelectedOperation(IEdmFunctionImport operation, bool mustBeContainerQualified)
		{
			string operationName = operation.Name;
			string operationNameWithParameters = operation.NameWithParameters();
			if (!mustBeContainerQualified)
			{
				yield return operationName;
			}
			yield return operationNameWithParameters;
			string containerName = operation.Container.Name + ".";
			yield return containerName + "*";
			yield return containerName + operationName;
			yield return containerName + operationNameWithParameters;
			string qualifiedContainerName = operation.Container.FullName() + ".";
			yield return qualifiedContainerName + "*";
			yield return qualifiedContainerName + operationName;
			yield return qualifiedContainerName + operationNameWithParameters;
			yield break;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001DE54 File Offset: 0x0001C054
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

		// Token: 0x06000964 RID: 2404 RVA: 0x0001DE78 File Offset: 0x0001C078
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

		// Token: 0x06000965 RID: 2405 RVA: 0x0001DEFC File Offset: 0x0001C0FC
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

		// Token: 0x06000966 RID: 2406 RVA: 0x0001DF54 File Offset: 0x0001C154
		private bool IsOperationSelectedAtThisLevel(IEdmFunctionImport operation, bool mustBeContainerQualified)
		{
			return this.selectionType != SelectedPropertiesNode.SelectionType.Empty && (this.selectionType == SelectedPropertiesNode.SelectionType.EntireSubtree || Enumerable.Any<string>(SelectedPropertiesNode.GetPossibleMatchesForSelectedOperation(operation, mustBeContainerQualified), (string possibleMatch) => this.selectedProperties.Contains(possibleMatch)));
		}

		// Token: 0x04000385 RID: 901
		private const char PathSeparator = '/';

		// Token: 0x04000386 RID: 902
		private const char ItemSeparator = ',';

		// Token: 0x04000387 RID: 903
		private static readonly SelectedPropertiesNode Empty = new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.Empty);

		// Token: 0x04000388 RID: 904
		private static readonly SelectedPropertiesNode EntireSubtree = new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.EntireSubtree);

		// Token: 0x04000389 RID: 905
		private static readonly Dictionary<string, IEdmStructuralProperty> EmptyStreamProperties = new Dictionary<string, IEdmStructuralProperty>(StringComparer.Ordinal);

		// Token: 0x0400038A RID: 906
		private static readonly IEnumerable<IEdmNavigationProperty> EmptyNavigationProperties = Enumerable.Empty<IEdmNavigationProperty>();

		// Token: 0x0400038B RID: 907
		private readonly SelectedPropertiesNode.SelectionType selectionType;

		// Token: 0x0400038C RID: 908
		private HashSet<string> selectedProperties;

		// Token: 0x0400038D RID: 909
		private Dictionary<string, SelectedPropertiesNode> children;

		// Token: 0x0400038E RID: 910
		private bool hasWildcard;

		// Token: 0x02000161 RID: 353
		private enum SelectionType
		{
			// Token: 0x04000394 RID: 916
			Empty,
			// Token: 0x04000395 RID: 917
			EntireSubtree,
			// Token: 0x04000396 RID: 918
			PartialSubtree
		}
	}
}
