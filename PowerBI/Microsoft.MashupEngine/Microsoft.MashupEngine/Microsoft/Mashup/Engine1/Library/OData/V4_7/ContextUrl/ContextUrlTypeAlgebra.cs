using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.ContextUrl
{
	// Token: 0x0200081E RID: 2078
	internal static class ContextUrlTypeAlgebra
	{
		// Token: 0x06003BF7 RID: 15351 RVA: 0x000C2950 File Offset: 0x000C0B50
		public static TypeValue Select(TypeValue typeValue, Microsoft.OData.Edm.IEdmModel model, Microsoft.OData.Edm.IEdmType type, IEnumerable<ContextUrlSelectListItem> items)
		{
			ContextUrlTypeAlgebra.SelectionNode selectionNode = ContextUrlTypeAlgebra.SelectionNode.Create(items);
			return new ContextUrlTypeAlgebra.ProjectedTypeProcessor(model).Select(typeValue, type, selectionNode);
		}

		// Token: 0x0200081F RID: 2079
		private sealed class ProjectedTypeProcessor
		{
			// Token: 0x06003BF8 RID: 15352 RVA: 0x000C2972 File Offset: 0x000C0B72
			public ProjectedTypeProcessor(Microsoft.OData.Edm.IEdmModel model)
			{
				this.model = model;
				this.processedTypes = new Dictionary<ContextUrlTypeAlgebra.ProjectedTypeProcessor.ProcessedProjection, RecordTypeValue>();
			}

			// Token: 0x06003BF9 RID: 15353 RVA: 0x000C298C File Offset: 0x000C0B8C
			public TypeValue Select(TypeValue typeValue, Microsoft.OData.Edm.IEdmType type, ContextUrlTypeAlgebra.SelectionNode selectionNode)
			{
				RecordTypeValue recordTypeValue = (typeValue.IsTableType ? typeValue.AsTableType.ItemType : typeValue.AsRecordType);
				Microsoft.OData.Edm.IEdmStructuredType edmStructuredType = type.AsElementType() as Microsoft.OData.Edm.IEdmStructuredType;
				RecordTypeValue recordTypeValue2 = this.Select(recordTypeValue, edmStructuredType, selectionNode);
				if (typeValue.IsTableType)
				{
					ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
					foreach (string text in recordTypeValue2.Fields.Keys)
					{
						int num;
						if (recordTypeValue.Fields.Keys.TryGetKeyIndex(text, out num))
						{
							columnSelectionBuilder.Add(text, num);
						}
					}
					IList<TableKey> list = TableKeys.SelectColumns(typeValue.AsTableType.TableKeys, recordTypeValue.Fields.Keys, columnSelectionBuilder.ToColumnSelection());
					return TableTypeValue.New(RecordTypeAlgebra.EnsureClosedWithRequiredFieldsOnly(recordTypeValue2).AsRecordType, list);
				}
				return recordTypeValue2;
			}

			// Token: 0x06003BFA RID: 15354 RVA: 0x000C2A80 File Offset: 0x000C0C80
			private RecordTypeValue Select(RecordTypeValue recordTypeValue, Microsoft.OData.Edm.IEdmStructuredType structuredType, ContextUrlTypeAlgebra.SelectionNode selectionNode)
			{
				ContextUrlTypeAlgebra.ProjectedTypeProcessor.ProcessedProjection processedProjection = new ContextUrlTypeAlgebra.ProjectedTypeProcessor.ProcessedProjection(recordTypeValue, structuredType, selectionNode);
				RecordTypeValue recordTypeValue2;
				if (!this.processedTypes.TryGetValue(processedProjection, out recordTypeValue2))
				{
					recordTypeValue2 = this.SelectUnprocessed(recordTypeValue, structuredType, selectionNode);
					this.processedTypes.Add(processedProjection, recordTypeValue2);
				}
				return recordTypeValue2;
			}

			// Token: 0x06003BFB RID: 15355 RVA: 0x000C2AC0 File Offset: 0x000C0CC0
			private RecordTypeValue SelectUnprocessed(RecordTypeValue recordTypeValue, Microsoft.OData.Edm.IEdmStructuredType structuredType, ContextUrlTypeAlgebra.SelectionNode selectionNode)
			{
				ODataUriResolver odataUriResolver = new UnqualifiedODataUriResolver();
				RecordBuilder recordBuilder = new RecordBuilder(recordTypeValue.Fields.Keys.Length);
				bool allStructuralProperties = selectionNode.AllStructuralProperties;
				HashSet<string> operationNamespaces = selectionNode.OperationNamespaces;
				Dictionary<string, ContextUrlTypeAlgebra.SelectionNode> selectedItems = new Dictionary<string, ContextUrlTypeAlgebra.SelectionNode>(selectionNode.SelectedItems);
				using (RecordFieldEnumerator enumerator = recordTypeValue.Fields.GetFields().GetEnumerator())
				{
					Func<Microsoft.OData.Edm.IEdmOperation, bool> <>9__0;
					Func<Microsoft.OData.Edm.IEdmOperation, bool> <>9__1;
					while (enumerator.MoveNext())
					{
						Microsoft.Mashup.Engine1.Runtime.NamedValue field = enumerator.Current;
						string text = EdmNameEncoder.Encode(field.Key);
						ContextUrlTypeAlgebra.SelectionNode nestedItem;
						if (selectedItems.TryGetValue(text, out nestedItem))
						{
							if (nestedItem != null)
							{
								TypeValue fieldType = RecordTypeAlgebra.Field(recordTypeValue, field.Key);
								if (fieldType.IsFunctionType)
								{
									recordBuilder.Add(field.Key, field.Value, TypeValue.Any);
								}
								else
								{
									Microsoft.OData.Edm.IEdmProperty property = odataUriResolver.ResolveProperty(structuredType, text);
									IValueReference valueReference = new DelayedValue(() => RecordTypeValue.NewField(this.Select(fieldType, property.Type.Definition, nestedItem), field.Value["Optional"].AsLogical));
									recordBuilder.Add(field.Key, valueReference, TypeValue.Any);
								}
							}
							else
							{
								recordBuilder.Add(field.Key, field.Value, TypeValue.Any);
							}
							selectedItems.Remove(text);
						}
						else
						{
							IEnumerable<Microsoft.OData.Edm.IEdmOperation> enumerable = odataUriResolver.ResolveBoundOperations(this.model, text, structuredType);
							IEnumerable<Microsoft.OData.Edm.IEdmOperation> enumerable2 = enumerable;
							Func<Microsoft.OData.Edm.IEdmOperation, bool> func;
							if ((func = <>9__0) == null)
							{
								func = (<>9__0 = (Microsoft.OData.Edm.IEdmOperation o) => selectedItems.ContainsKey(o.FullName()));
							}
							if (enumerable2.Any(func))
							{
								recordBuilder.Add(field.Key, field.Value, TypeValue.Any);
								selectedItems.Remove(text);
							}
							else if (allStructuralProperties)
							{
								recordBuilder.Add(field.Key, field.Value, TypeValue.Any);
							}
							else
							{
								IEnumerable<Microsoft.OData.Edm.IEdmOperation> enumerable3 = enumerable;
								Func<Microsoft.OData.Edm.IEdmOperation, bool> func2;
								if ((func2 = <>9__1) == null)
								{
									func2 = (<>9__1 = (Microsoft.OData.Edm.IEdmOperation o) => operationNamespaces.Contains(o.Namespace));
								}
								if (enumerable3.Any(func2))
								{
									recordBuilder.Add(field.Key, field.Value, TypeValue.Any);
								}
							}
						}
					}
				}
				foreach (KeyValuePair<string, ContextUrlTypeAlgebra.SelectionNode> keyValuePair in selectedItems)
				{
					if (!keyValuePair.Key.Contains('.'))
					{
						recordBuilder.Add(EdmNameEncoder.Decode(keyValuePair.Key), RecordTypeValue.NewField(TypeValue.Any, LogicalValue.True), TypeValue.Any);
					}
				}
				string text2 = null;
				Value value;
				if (recordTypeValue.TryGetMetaField("MoreColumns", out value))
				{
					text2 = value.AsString;
					Value value2 = recordTypeValue.Fields[text2];
					Keys keys = recordBuilder.ToRecord().Keys;
					if (keys.Contains(text2))
					{
						text2 = ODataTypeServices.GetMoreColumnsColumnName(keys);
					}
					recordBuilder.Add(text2, value2, TypeValue.Any);
				}
				RecordTypeValue recordTypeValue2 = RecordTypeValue.New(recordBuilder.ToRecord(), recordTypeValue.Open && allStructuralProperties);
				if (text2 != null)
				{
					recordTypeValue2 = TypeServices.ConvertToMoreColumns(recordTypeValue2, text2);
				}
				return recordTypeValue2;
			}

			// Token: 0x04001F42 RID: 8002
			private readonly Microsoft.OData.Edm.IEdmModel model;

			// Token: 0x04001F43 RID: 8003
			private readonly Dictionary<ContextUrlTypeAlgebra.ProjectedTypeProcessor.ProcessedProjection, RecordTypeValue> processedTypes;

			// Token: 0x02000820 RID: 2080
			private sealed class ProcessedProjection : IEquatable<ContextUrlTypeAlgebra.ProjectedTypeProcessor.ProcessedProjection>
			{
				// Token: 0x06003BFC RID: 15356 RVA: 0x000C2EAC File Offset: 0x000C10AC
				public ProcessedProjection(RecordTypeValue recordTypeValue, Microsoft.OData.Edm.IEdmStructuredType structuredType, ContextUrlTypeAlgebra.SelectionNode selectionNode)
				{
					this.recordTypeValue = recordTypeValue;
					this.structuredType = structuredType;
					this.selectionNode = selectionNode;
				}

				// Token: 0x06003BFD RID: 15357 RVA: 0x000C2EC9 File Offset: 0x000C10C9
				public override int GetHashCode()
				{
					return this.recordTypeValue.GetHashCode() ^ this.structuredType.GetHashCode() ^ this.selectionNode.GetHashCode();
				}

				// Token: 0x06003BFE RID: 15358 RVA: 0x000C2EF0 File Offset: 0x000C10F0
				public override bool Equals(object obj)
				{
					ContextUrlTypeAlgebra.ProjectedTypeProcessor.ProcessedProjection processedProjection = obj as ContextUrlTypeAlgebra.ProjectedTypeProcessor.ProcessedProjection;
					return processedProjection != null && this.Equals(processedProjection);
				}

				// Token: 0x06003BFF RID: 15359 RVA: 0x000C2F10 File Offset: 0x000C1110
				public bool Equals(ContextUrlTypeAlgebra.ProjectedTypeProcessor.ProcessedProjection other)
				{
					return this.recordTypeValue.Equals(other.recordTypeValue) && this.structuredType.Equals(other.structuredType) && this.selectionNode.Equals(other.selectionNode);
				}

				// Token: 0x04001F44 RID: 8004
				private readonly RecordTypeValue recordTypeValue;

				// Token: 0x04001F45 RID: 8005
				private readonly Microsoft.OData.Edm.IEdmStructuredType structuredType;

				// Token: 0x04001F46 RID: 8006
				private readonly ContextUrlTypeAlgebra.SelectionNode selectionNode;
			}
		}

		// Token: 0x02000823 RID: 2083
		private abstract class SelectionNode
		{
			// Token: 0x170013ED RID: 5101
			// (get) Token: 0x06003C05 RID: 15365
			public abstract bool AllStructuralProperties { get; }

			// Token: 0x170013EE RID: 5102
			// (get) Token: 0x06003C06 RID: 15366
			public abstract HashSet<string> OperationNamespaces { get; }

			// Token: 0x170013EF RID: 5103
			// (get) Token: 0x06003C07 RID: 15367
			public abstract Dictionary<string, ContextUrlTypeAlgebra.SelectionNode> SelectedItems { get; }

			// Token: 0x06003C08 RID: 15368 RVA: 0x000C2FCC File Offset: 0x000C11CC
			public static ContextUrlTypeAlgebra.SelectionNode Create(IEnumerable<ContextUrlSelectListItem> items)
			{
				return items.Select(new Func<ContextUrlSelectListItem, ContextUrlTypeAlgebra.SelectionNode>(ContextUrlTypeAlgebra.SelectionNode.Create)).Aggregate(new Func<ContextUrlTypeAlgebra.SelectionNode, ContextUrlTypeAlgebra.SelectionNode, ContextUrlTypeAlgebra.SelectionNode>(ContextUrlTypeAlgebra.SelectionNode.Union));
			}

			// Token: 0x06003C09 RID: 15369 RVA: 0x000C2FF4 File Offset: 0x000C11F4
			public static ContextUrlTypeAlgebra.SelectionNode Union(ContextUrlTypeAlgebra.SelectionNode left, ContextUrlTypeAlgebra.SelectionNode right)
			{
				if (left == null)
				{
					return right;
				}
				if (right == null)
				{
					return left;
				}
				bool flag = left.AllStructuralProperties || right.AllStructuralProperties;
				HashSet<string> hashSet = new HashSet<string>();
				hashSet.UnionWith(left.OperationNamespaces);
				hashSet.UnionWith(right.OperationNamespaces);
				Dictionary<string, ContextUrlTypeAlgebra.SelectionNode> dictionary = new Dictionary<string, ContextUrlTypeAlgebra.SelectionNode>();
				foreach (KeyValuePair<string, ContextUrlTypeAlgebra.SelectionNode> keyValuePair in left.SelectedItems.Concat(right.SelectedItems))
				{
					ContextUrlTypeAlgebra.SelectionNode selectionNode;
					if (dictionary.TryGetValue(keyValuePair.Key, out selectionNode))
					{
						dictionary[keyValuePair.Key] = ContextUrlTypeAlgebra.SelectionNode.Union(selectionNode, keyValuePair.Value);
					}
					else
					{
						dictionary[keyValuePair.Key] = keyValuePair.Value;
					}
				}
				return new ContextUrlTypeAlgebra.SelectionNode.StrictSelectionNode(flag, hashSet, dictionary);
			}

			// Token: 0x06003C0A RID: 15370 RVA: 0x000C30D0 File Offset: 0x000C12D0
			private static ContextUrlTypeAlgebra.SelectionNode Create(ContextUrlSelectListItem item)
			{
				switch (item.Kind)
				{
				case SelectListItemKind.Wildcard:
					return ContextUrlTypeAlgebra.SelectionNode.Create((ContextUrlSelectListWildcardItem)item);
				case SelectListItemKind.QualifiedWildcard:
					return ContextUrlTypeAlgebra.SelectionNode.Create((ContextUrlSelectListQualifiedWildcardItem)item);
				case SelectListItemKind.Projection:
					return ContextUrlTypeAlgebra.SelectionNode.Create((ContextUrlSelectListProjectionItem)item);
				case SelectListItemKind.Expansion:
					return ContextUrlTypeAlgebra.SelectionNode.Create((ContextUrlSelectListExpansionItem)item);
				case SelectListItemKind.FunctionOverload:
					return ContextUrlTypeAlgebra.SelectionNode.Create((ContextUrlSelectListFunctionOverloadItem)item);
				default:
					throw new InvalidOperationException();
				}
			}

			// Token: 0x06003C0B RID: 15371 RVA: 0x000C3141 File Offset: 0x000C1341
			private static ContextUrlTypeAlgebra.SelectionNode Create(ContextUrlSelectListWildcardItem item)
			{
				return new ContextUrlTypeAlgebra.SelectionNode.StrictSelectionNode(true, new HashSet<string>(), new Dictionary<string, ContextUrlTypeAlgebra.SelectionNode>(0));
			}

			// Token: 0x06003C0C RID: 15372 RVA: 0x000C3154 File Offset: 0x000C1354
			private static ContextUrlTypeAlgebra.SelectionNode Create(ContextUrlSelectListQualifiedWildcardItem item)
			{
				return new ContextUrlTypeAlgebra.SelectionNode.StrictSelectionNode(false, new HashSet<string> { item.NamespaceName }, new Dictionary<string, ContextUrlTypeAlgebra.SelectionNode>(0));
			}

			// Token: 0x06003C0D RID: 15373 RVA: 0x000C3174 File Offset: 0x000C1374
			private static ContextUrlTypeAlgebra.SelectionNode Create(ContextUrlSelectListProjectionItem item)
			{
				return ContextUrlTypeAlgebra.SelectionNode.SelectionNodeCreator.Create(item.SelectedPath, null, false);
			}

			// Token: 0x06003C0E RID: 15374 RVA: 0x000C3183 File Offset: 0x000C1383
			private static ContextUrlTypeAlgebra.SelectionNode Create(ContextUrlSelectListExpansionItem item)
			{
				return ContextUrlTypeAlgebra.SelectionNode.SelectionNodeCreator.Create(item.PathToNavigationProperty, item.SelectList, item.Recursive);
			}

			// Token: 0x06003C0F RID: 15375 RVA: 0x000C319C File Offset: 0x000C139C
			private static ContextUrlTypeAlgebra.SelectionNode Create(ContextUrlSelectListFunctionOverloadItem item)
			{
				return ContextUrlTypeAlgebra.SelectionNode.SelectionNodeCreator.Create(item.PathToFunction, null, false);
			}

			// Token: 0x02000824 RID: 2084
			private sealed class StrictSelectionNode : ContextUrlTypeAlgebra.SelectionNode
			{
				// Token: 0x06003C11 RID: 15377 RVA: 0x000C31AB File Offset: 0x000C13AB
				public StrictSelectionNode(bool allStructuralProperties, HashSet<string> operationNamespaces, Dictionary<string, ContextUrlTypeAlgebra.SelectionNode> selectedItems)
				{
					this.allStructuralProperties = allStructuralProperties;
					this.operationNamespaces = operationNamespaces;
					this.selectedItems = selectedItems;
				}

				// Token: 0x170013F0 RID: 5104
				// (get) Token: 0x06003C12 RID: 15378 RVA: 0x000C31C8 File Offset: 0x000C13C8
				public override bool AllStructuralProperties
				{
					get
					{
						return this.allStructuralProperties;
					}
				}

				// Token: 0x170013F1 RID: 5105
				// (get) Token: 0x06003C13 RID: 15379 RVA: 0x000C31D0 File Offset: 0x000C13D0
				public override HashSet<string> OperationNamespaces
				{
					get
					{
						return this.operationNamespaces;
					}
				}

				// Token: 0x170013F2 RID: 5106
				// (get) Token: 0x06003C14 RID: 15380 RVA: 0x000C31D8 File Offset: 0x000C13D8
				public override Dictionary<string, ContextUrlTypeAlgebra.SelectionNode> SelectedItems
				{
					get
					{
						return this.selectedItems;
					}
				}

				// Token: 0x04001F51 RID: 8017
				private readonly bool allStructuralProperties;

				// Token: 0x04001F52 RID: 8018
				private readonly HashSet<string> operationNamespaces;

				// Token: 0x04001F53 RID: 8019
				private readonly Dictionary<string, ContextUrlTypeAlgebra.SelectionNode> selectedItems;
			}

			// Token: 0x02000825 RID: 2085
			private sealed class LazySelectionNode : ContextUrlTypeAlgebra.SelectionNode
			{
				// Token: 0x06003C15 RID: 15381 RVA: 0x000C31E0 File Offset: 0x000C13E0
				public LazySelectionNode(EdmPathExpression path, Func<ContextUrlTypeAlgebra.SelectionNode> selectionNode)
				{
					this.selectionNode = new ContextUrlTypeAlgebra.SelectionNode.LazySelectionNode.ReentrantLazy<ContextUrlTypeAlgebra.SelectionNode>(selectionNode, delegate
					{
						throw ODataCommonErrors.ODataNonTerminatingContextUrl(path.Path);
					});
				}

				// Token: 0x170013F3 RID: 5107
				// (get) Token: 0x06003C16 RID: 15382 RVA: 0x000C3218 File Offset: 0x000C1418
				public override bool AllStructuralProperties
				{
					get
					{
						return this.selectionNode.Value.AllStructuralProperties;
					}
				}

				// Token: 0x170013F4 RID: 5108
				// (get) Token: 0x06003C17 RID: 15383 RVA: 0x000C322A File Offset: 0x000C142A
				public override HashSet<string> OperationNamespaces
				{
					get
					{
						return this.selectionNode.Value.OperationNamespaces;
					}
				}

				// Token: 0x170013F5 RID: 5109
				// (get) Token: 0x06003C18 RID: 15384 RVA: 0x000C323C File Offset: 0x000C143C
				public override Dictionary<string, ContextUrlTypeAlgebra.SelectionNode> SelectedItems
				{
					get
					{
						return this.selectionNode.Value.SelectedItems;
					}
				}

				// Token: 0x04001F54 RID: 8020
				private readonly ContextUrlTypeAlgebra.SelectionNode.LazySelectionNode.ReentrantLazy<ContextUrlTypeAlgebra.SelectionNode> selectionNode;

				// Token: 0x02000826 RID: 2086
				private class ReentrantLazy<T>
				{
					// Token: 0x06003C19 RID: 15385 RVA: 0x000C324E File Offset: 0x000C144E
					public ReentrantLazy(Func<T> getValue, Action onReentrancy)
					{
						this.getValue = getValue;
						this.onReentrancy = onReentrancy;
						this.evaluating = false;
					}

					// Token: 0x170013F6 RID: 5110
					// (get) Token: 0x06003C1A RID: 15386 RVA: 0x000C326C File Offset: 0x000C146C
					public T Value
					{
						get
						{
							if (this.onReentrancy != null)
							{
								if (this.evaluating)
								{
									this.onReentrancy();
								}
								this.evaluating = true;
								this.value = this.getValue();
								this.evaluating = false;
								this.onReentrancy = null;
							}
							return this.value;
						}
					}

					// Token: 0x04001F55 RID: 8021
					private readonly Func<T> getValue;

					// Token: 0x04001F56 RID: 8022
					private Action onReentrancy;

					// Token: 0x04001F57 RID: 8023
					private T value;

					// Token: 0x04001F58 RID: 8024
					private bool evaluating;
				}
			}

			// Token: 0x02000828 RID: 2088
			private class SelectionNodeCreator
			{
				// Token: 0x06003C1D RID: 15389 RVA: 0x000C32D2 File Offset: 0x000C14D2
				private SelectionNodeCreator(EdmPathExpression path, IEnumerable<ContextUrlSelectListItem> selectList = null, bool recursive = false)
				{
					this.path = path.PathSegments.ToArray<string>();
					this.selectList = selectList;
					this.recursive = recursive;
					this.selectionNode = this.Create(0);
				}

				// Token: 0x06003C1E RID: 15390 RVA: 0x000C3306 File Offset: 0x000C1506
				public static ContextUrlTypeAlgebra.SelectionNode Create(EdmPathExpression path, IEnumerable<ContextUrlSelectListItem> selectList = null, bool recursive = false)
				{
					return new ContextUrlTypeAlgebra.SelectionNode.SelectionNodeCreator(path, selectList, recursive).selectionNode;
				}

				// Token: 0x06003C1F RID: 15391 RVA: 0x000C3318 File Offset: 0x000C1518
				private ContextUrlTypeAlgebra.SelectionNode Create(int depth)
				{
					if (depth == this.path.Length)
					{
						ContextUrlTypeAlgebra.SelectionNode nestedNode = ((this.selectList != null) ? ContextUrlTypeAlgebra.SelectionNode.Create(this.selectList) : null);
						if (this.recursive)
						{
							return new ContextUrlTypeAlgebra.SelectionNode.LazySelectionNode(new EdmPathExpression(this.path), () => ContextUrlTypeAlgebra.SelectionNode.Union(nestedNode, this.selectionNode));
						}
						return nestedNode;
					}
					else
					{
						string text = this.path[depth];
						ContextUrlTypeAlgebra.SelectionNode selectionNode = this.Create(depth + 1);
						if (text.Contains('.') && selectionNode != null)
						{
							return selectionNode;
						}
						return new ContextUrlTypeAlgebra.SelectionNode.StrictSelectionNode(false, new HashSet<string>(), new Dictionary<string, ContextUrlTypeAlgebra.SelectionNode> { { text, selectionNode } });
					}
				}

				// Token: 0x04001F5A RID: 8026
				private readonly string[] path;

				// Token: 0x04001F5B RID: 8027
				private readonly IEnumerable<ContextUrlSelectListItem> selectList;

				// Token: 0x04001F5C RID: 8028
				private readonly bool recursive;

				// Token: 0x04001F5D RID: 8029
				private readonly ContextUrlTypeAlgebra.SelectionNode selectionNode;
			}
		}
	}
}
