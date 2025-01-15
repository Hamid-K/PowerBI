using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query.Expressions;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x020001A2 RID: 418
	public class SelectExpandNode
	{
		// Token: 0x06000DBF RID: 3519 RVA: 0x00002557 File Offset: 0x00000757
		public SelectExpandNode()
		{
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000372C8 File Offset: 0x000354C8
		public SelectExpandNode(SelectExpandNode selectExpandNodeToCopy)
		{
			this.SelectedStructuralProperties = ((selectExpandNodeToCopy.SelectedStructuralProperties == null) ? null : new HashSet<IEdmStructuralProperty>(selectExpandNodeToCopy.SelectedStructuralProperties));
			this.SelectedComplexTypeProperties = ((selectExpandNodeToCopy.SelectedComplexTypeProperties == null) ? null : new Dictionary<IEdmStructuralProperty, PathSelectItem>(selectExpandNodeToCopy.SelectedComplexTypeProperties));
			this.SelectedNavigationProperties = ((selectExpandNodeToCopy.SelectedNavigationProperties == null) ? null : new HashSet<IEdmNavigationProperty>(selectExpandNodeToCopy.SelectedNavigationProperties));
			this.ExpandedProperties = ((selectExpandNodeToCopy.ExpandedProperties == null) ? null : new Dictionary<IEdmNavigationProperty, ExpandedNavigationSelectItem>(selectExpandNodeToCopy.ExpandedProperties));
			this.ReferencedProperties = ((selectExpandNodeToCopy.ReferencedProperties == null) ? null : new Dictionary<IEdmNavigationProperty, ExpandedReferenceSelectItem>());
			this.SelectAllDynamicProperties = selectExpandNodeToCopy.SelectAllDynamicProperties;
			this.SelectedDynamicProperties = ((selectExpandNodeToCopy.SelectedDynamicProperties == null) ? null : new HashSet<string>(selectExpandNodeToCopy.SelectedDynamicProperties));
			this.SelectedActions = ((selectExpandNodeToCopy.SelectedActions == null) ? null : new HashSet<IEdmAction>(selectExpandNodeToCopy.SelectedActions));
			this.SelectedFunctions = ((selectExpandNodeToCopy.SelectedFunctions == null) ? null : new HashSet<IEdmFunction>(selectExpandNodeToCopy.SelectedFunctions));
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x000373C1 File Offset: 0x000355C1
		public SelectExpandNode(IEdmStructuredType structuredType, ODataSerializerContext writeContext)
			: this()
		{
			this.Initialize(writeContext.SelectExpandClause, structuredType, writeContext.Model, writeContext.ExpandReference);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x000373E2 File Offset: 0x000355E2
		public SelectExpandNode(SelectExpandClause selectExpandClause, IEdmStructuredType structuredType, IEdmModel model)
			: this()
		{
			this.Initialize(selectExpandClause, structuredType, model, false);
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x000373F4 File Offset: 0x000355F4
		[Obsolete("This property will be removed later, please use ReferencedProperties.")]
		public ISet<IEdmNavigationProperty> ReferencedNavigationProperties
		{
			get
			{
				if (this.ReferencedProperties == null)
				{
					return null;
				}
				return new HashSet<IEdmNavigationProperty>(this.ReferencedProperties.Keys);
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00037410 File Offset: 0x00035610
		[Obsolete("This property will be removed later, please use SelectedComplexTypeProperties.")]
		public ISet<IEdmStructuralProperty> SelectedComplexProperties
		{
			get
			{
				if (this.SelectedComplexTypeProperties == null)
				{
					return null;
				}
				return new HashSet<IEdmStructuralProperty>(this.SelectedComplexTypeProperties.Keys);
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x0003742C File Offset: 0x0003562C
		// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x00037434 File Offset: 0x00035634
		public ISet<IEdmStructuralProperty> SelectedStructuralProperties { get; internal set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0003743D File Offset: 0x0003563D
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x00037445 File Offset: 0x00035645
		public ISet<IEdmNavigationProperty> SelectedNavigationProperties { get; internal set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x0003744E File Offset: 0x0003564E
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x00037456 File Offset: 0x00035656
		public IDictionary<IEdmStructuralProperty, PathSelectItem> SelectedComplexTypeProperties { get; internal set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x0003745F File Offset: 0x0003565F
		// (set) Token: 0x06000DCC RID: 3532 RVA: 0x00037467 File Offset: 0x00035667
		public IDictionary<IEdmNavigationProperty, ExpandedNavigationSelectItem> ExpandedProperties { get; internal set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00037470 File Offset: 0x00035670
		// (set) Token: 0x06000DCE RID: 3534 RVA: 0x00037478 File Offset: 0x00035678
		public IDictionary<IEdmNavigationProperty, ExpandedReferenceSelectItem> ReferencedProperties { get; internal set; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00037481 File Offset: 0x00035681
		// (set) Token: 0x06000DD0 RID: 3536 RVA: 0x00037489 File Offset: 0x00035689
		public ISet<string> SelectedDynamicProperties { get; internal set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00037492 File Offset: 0x00035692
		// (set) Token: 0x06000DD2 RID: 3538 RVA: 0x0003749A File Offset: 0x0003569A
		public bool SelectAllDynamicProperties { get; internal set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x000374A3 File Offset: 0x000356A3
		// (set) Token: 0x06000DD4 RID: 3540 RVA: 0x000374AB File Offset: 0x000356AB
		public ISet<IEdmAction> SelectedActions { get; internal set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x000374B4 File Offset: 0x000356B4
		// (set) Token: 0x06000DD6 RID: 3542 RVA: 0x000374BC File Offset: 0x000356BC
		public ISet<IEdmFunction> SelectedFunctions { get; internal set; }

		// Token: 0x06000DD7 RID: 3543 RVA: 0x000374C8 File Offset: 0x000356C8
		private void Initialize(SelectExpandClause selectExpandClause, IEdmStructuredType structuredType, IEdmModel model, bool expandedReference)
		{
			if (structuredType == null)
			{
				throw Error.ArgumentNull("structuredType");
			}
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			IEdmEntityType edmEntityType = structuredType as IEdmEntityType;
			if (expandedReference)
			{
				this.SelectAllDynamicProperties = false;
				if (edmEntityType != null)
				{
					this.SelectedStructuralProperties = new HashSet<IEdmStructuralProperty>(edmEntityType.Key());
					return;
				}
			}
			else
			{
				SelectExpandNode.EdmStructuralTypeInfo edmStructuralTypeInfo = new SelectExpandNode.EdmStructuralTypeInfo(model, structuredType);
				if (selectExpandClause == null)
				{
					this.SelectAllDynamicProperties = true;
					this.SelectedNavigationProperties = edmStructuralTypeInfo.AllNavigationProperties;
					this.SelectedActions = edmStructuralTypeInfo.AllActions;
					this.SelectedFunctions = edmStructuralTypeInfo.AllFunctions;
					if (edmStructuralTypeInfo.AllStructuralProperties == null)
					{
						goto IL_00BD;
					}
					using (IEnumerator<IEdmStructuralProperty> enumerator = edmStructuralTypeInfo.AllStructuralProperties.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							IEdmStructuralProperty edmStructuralProperty = enumerator.Current;
							this.AddStructuralProperty(edmStructuralProperty, null);
						}
						goto IL_00BD;
					}
				}
				this.BuildSelectExpand(selectExpandClause, edmStructuralTypeInfo);
				IL_00BD:
				this.AdjustSelectNavigationProperties();
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x000375A8 File Offset: 0x000357A8
		private void BuildSelectExpand(SelectExpandClause selectExpandClause, SelectExpandNode.EdmStructuralTypeInfo structuralTypeInfo)
		{
			Dictionary<IEdmStructuralProperty, SelectExpandIncludedProperty> dictionary = new Dictionary<IEdmStructuralProperty, SelectExpandIncludedProperty>();
			this.SelectAllDynamicProperties = false;
			foreach (SelectItem selectItem in selectExpandClause.SelectedItems)
			{
				ExpandedReferenceSelectItem expandedReferenceSelectItem = selectItem as ExpandedReferenceSelectItem;
				if (expandedReferenceSelectItem != null)
				{
					this.BuildExpandItem(expandedReferenceSelectItem, dictionary, structuralTypeInfo);
				}
				else
				{
					PathSelectItem pathSelectItem = selectItem as PathSelectItem;
					if (pathSelectItem != null)
					{
						this.BuildSelectItem(pathSelectItem, dictionary, structuralTypeInfo);
					}
					else if (selectItem is WildcardSelectItem)
					{
						SelectExpandNode.MergeAllStructuralProperties(structuralTypeInfo.AllStructuralProperties, dictionary);
						this.MergeSelectedNavigationProperties(structuralTypeInfo.AllNavigationProperties);
						this.SelectAllDynamicProperties = true;
					}
					else
					{
						NamespaceQualifiedWildcardSelectItem namespaceQualifiedWildcardSelectItem = selectItem as NamespaceQualifiedWildcardSelectItem;
						if (namespaceQualifiedWildcardSelectItem == null)
						{
							throw new ODataException(Error.Format(SRResources.SelectionTypeNotSupported, new object[] { selectItem.GetType().Name }));
						}
						this.AddNamespaceWildcardOperation(namespaceQualifiedWildcardSelectItem, structuralTypeInfo.AllActions, structuralTypeInfo.AllFunctions);
					}
				}
			}
			if (selectExpandClause.AllSelected)
			{
				SelectExpandNode.MergeAllStructuralProperties(structuralTypeInfo.AllStructuralProperties, dictionary);
				this.MergeSelectedNavigationProperties(structuralTypeInfo.AllNavigationProperties);
				this.MergeSelectedAction(structuralTypeInfo.AllActions);
				this.MergeSelectedFunction(structuralTypeInfo.AllFunctions);
				this.SelectAllDynamicProperties = true;
			}
			if (structuralTypeInfo.AllStructuralProperties != null)
			{
				foreach (IEdmStructuralProperty edmStructuralProperty in structuralTypeInfo.AllStructuralProperties)
				{
					SelectExpandIncludedProperty selectExpandIncludedProperty;
					if (dictionary.TryGetValue(edmStructuralProperty, out selectExpandIncludedProperty))
					{
						PathSelectItem pathSelectItem2 = ((selectExpandIncludedProperty == null) ? null : selectExpandIncludedProperty.ToPathSelectItem());
						this.AddStructuralProperty(edmStructuralProperty, pathSelectItem2);
					}
				}
			}
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00037748 File Offset: 0x00035948
		private void BuildExpandItem(ExpandedReferenceSelectItem expandReferenceItem, IDictionary<IEdmStructuralProperty, SelectExpandIncludedProperty> currentLevelPropertiesInclude, SelectExpandNode.EdmStructuralTypeInfo structuralTypeInfo)
		{
			IList<ODataPathSegment> list;
			ODataPathSegment firstNonTypeCastSegment = expandReferenceItem.PathToNavigationProperty.GetFirstNonTypeCastSegment(out list);
			PropertySegment propertySegment = firstNonTypeCastSegment as PropertySegment;
			if (propertySegment != null)
			{
				if (structuralTypeInfo.IsStructuralPropertyDefined(propertySegment.Property))
				{
					SelectExpandIncludedProperty selectExpandIncludedProperty;
					if (!currentLevelPropertiesInclude.TryGetValue(propertySegment.Property, out selectExpandIncludedProperty))
					{
						selectExpandIncludedProperty = new SelectExpandIncludedProperty(propertySegment);
						currentLevelPropertiesInclude[propertySegment.Property] = selectExpandIncludedProperty;
					}
					selectExpandIncludedProperty.AddSubExpandItem(list, expandReferenceItem);
					return;
				}
			}
			else
			{
				NavigationPropertySegment navigationPropertySegment = firstNonTypeCastSegment as NavigationPropertySegment;
				if (structuralTypeInfo.IsNavigationPropertyDefined(navigationPropertySegment.NavigationProperty))
				{
					ExpandedNavigationSelectItem expandedNavigationSelectItem = expandReferenceItem as ExpandedNavigationSelectItem;
					if (expandedNavigationSelectItem != null)
					{
						if (this.ExpandedProperties == null)
						{
							this.ExpandedProperties = new Dictionary<IEdmNavigationProperty, ExpandedNavigationSelectItem>();
						}
						this.ExpandedProperties[navigationPropertySegment.NavigationProperty] = expandedNavigationSelectItem;
						return;
					}
					if (this.ReferencedProperties == null)
					{
						this.ReferencedProperties = new Dictionary<IEdmNavigationProperty, ExpandedReferenceSelectItem>();
					}
					this.ReferencedProperties[navigationPropertySegment.NavigationProperty] = expandReferenceItem;
				}
			}
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0003781C File Offset: 0x00035A1C
		private void BuildSelectItem(PathSelectItem pathSelectItem, IDictionary<IEdmStructuralProperty, SelectExpandIncludedProperty> currentLevelPropertiesInclude, SelectExpandNode.EdmStructuralTypeInfo structuralTypeInfo)
		{
			IList<ODataPathSegment> list;
			ODataPathSegment firstNonTypeCastSegment = pathSelectItem.SelectedPath.GetFirstNonTypeCastSegment(out list);
			PropertySegment propertySegment = firstNonTypeCastSegment as PropertySegment;
			if (propertySegment != null)
			{
				if (structuralTypeInfo.IsStructuralPropertyDefined(propertySegment.Property))
				{
					SelectExpandIncludedProperty selectExpandIncludedProperty;
					if (!currentLevelPropertiesInclude.TryGetValue(propertySegment.Property, out selectExpandIncludedProperty))
					{
						selectExpandIncludedProperty = new SelectExpandIncludedProperty(propertySegment);
						currentLevelPropertiesInclude[propertySegment.Property] = selectExpandIncludedProperty;
					}
					selectExpandIncludedProperty.AddSubSelectItem(list, pathSelectItem);
				}
				return;
			}
			NavigationPropertySegment navigationPropertySegment = firstNonTypeCastSegment as NavigationPropertySegment;
			if (navigationPropertySegment != null)
			{
				if (structuralTypeInfo.IsNavigationPropertyDefined(navigationPropertySegment.NavigationProperty))
				{
					if (this.SelectedNavigationProperties == null)
					{
						this.SelectedNavigationProperties = new HashSet<IEdmNavigationProperty>();
					}
					this.SelectedNavigationProperties.Add(navigationPropertySegment.NavigationProperty);
				}
				return;
			}
			OperationSegment operationSegment = firstNonTypeCastSegment as OperationSegment;
			if (operationSegment != null)
			{
				this.AddOperations(operationSegment, structuralTypeInfo.AllActions, structuralTypeInfo.AllFunctions);
				return;
			}
			DynamicPathSegment dynamicPathSegment = firstNonTypeCastSegment as DynamicPathSegment;
			if (dynamicPathSegment != null)
			{
				if (this.SelectedDynamicProperties == null)
				{
					this.SelectedDynamicProperties = new HashSet<string>();
				}
				this.SelectedDynamicProperties.Add(dynamicPathSegment.Identifier);
				return;
			}
			throw new ODataException(Error.Format(SRResources.SelectionTypeNotSupported, new object[] { firstNonTypeCastSegment.GetType().Name }));
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00037934 File Offset: 0x00035B34
		private static void MergeAllStructuralProperties(ISet<IEdmStructuralProperty> allStructuralProperties, IDictionary<IEdmStructuralProperty, SelectExpandIncludedProperty> currentLevelPropertiesInclude)
		{
			if (allStructuralProperties == null)
			{
				return;
			}
			foreach (IEdmStructuralProperty edmStructuralProperty in allStructuralProperties)
			{
				if (!currentLevelPropertiesInclude.ContainsKey(edmStructuralProperty))
				{
					currentLevelPropertiesInclude[edmStructuralProperty] = null;
				}
			}
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0003798C File Offset: 0x00035B8C
		private void MergeSelectedNavigationProperties(ISet<IEdmNavigationProperty> allNavigationProperties)
		{
			if (allNavigationProperties == null)
			{
				return;
			}
			if (this.SelectedNavigationProperties == null)
			{
				this.SelectedNavigationProperties = allNavigationProperties;
				return;
			}
			this.SelectedNavigationProperties.UnionWith(allNavigationProperties);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x000379AE File Offset: 0x00035BAE
		private void MergeSelectedAction(ISet<IEdmAction> allActions)
		{
			if (allActions == null)
			{
				return;
			}
			if (this.SelectedActions == null)
			{
				this.SelectedActions = allActions;
				return;
			}
			this.SelectedActions.UnionWith(allActions);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x000379D0 File Offset: 0x00035BD0
		private void MergeSelectedFunction(ISet<IEdmFunction> allFunctions)
		{
			if (allFunctions == null)
			{
				return;
			}
			if (this.SelectedFunctions == null)
			{
				this.SelectedFunctions = allFunctions;
				return;
			}
			this.SelectedFunctions.UnionWith(allFunctions);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x000379F4 File Offset: 0x00035BF4
		private void AddStructuralProperty(IEdmStructuralProperty structuralProperty, PathSelectItem pathSelectItem)
		{
			if (SelectExpandNode.IsComplexOrCollectionComplex(structuralProperty))
			{
				if (this.SelectedComplexTypeProperties == null)
				{
					this.SelectedComplexTypeProperties = new Dictionary<IEdmStructuralProperty, PathSelectItem>();
				}
				this.SelectedComplexTypeProperties[structuralProperty] = pathSelectItem;
				return;
			}
			if (this.SelectedStructuralProperties == null)
			{
				this.SelectedStructuralProperties = new HashSet<IEdmStructuralProperty>();
			}
			this.SelectedStructuralProperties.Add(structuralProperty);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00037A4C File Offset: 0x00035C4C
		private void AddNamespaceWildcardOperation(NamespaceQualifiedWildcardSelectItem namespaceSelectItem, ISet<IEdmAction> allActions, ISet<IEdmFunction> allFunctions)
		{
			if (allActions == null)
			{
				this.SelectedActions = null;
			}
			else
			{
				this.SelectedActions = new HashSet<IEdmAction>(allActions.Where((IEdmAction a) => a.Namespace == namespaceSelectItem.Namespace));
			}
			if (allFunctions == null)
			{
				this.SelectedFunctions = null;
				return;
			}
			this.SelectedFunctions = new HashSet<IEdmFunction>(allFunctions.Where((IEdmFunction a) => a.Namespace == namespaceSelectItem.Namespace));
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00037AB8 File Offset: 0x00035CB8
		private void AddOperations(OperationSegment operationSegment, ISet<IEdmAction> allActions, ISet<IEdmFunction> allFunctions)
		{
			foreach (IEdmOperation edmOperation in operationSegment.Operations)
			{
				IEdmAction edmAction = edmOperation as IEdmAction;
				if (edmAction != null && allActions.Contains(edmAction))
				{
					if (this.SelectedActions == null)
					{
						this.SelectedActions = new HashSet<IEdmAction>();
					}
					this.SelectedActions.Add(edmAction);
				}
				IEdmFunction edmFunction = edmOperation as IEdmFunction;
				if (edmFunction != null && allFunctions.Contains(edmFunction))
				{
					if (this.SelectedFunctions == null)
					{
						this.SelectedFunctions = new HashSet<IEdmFunction>();
					}
					this.SelectedFunctions.Add(edmFunction);
				}
			}
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00037B64 File Offset: 0x00035D64
		private void AdjustSelectNavigationProperties()
		{
			if (this.SelectedNavigationProperties != null)
			{
				if (this.ExpandedProperties != null)
				{
					this.SelectedNavigationProperties.ExceptWith(this.ExpandedProperties.Keys);
				}
				if (this.ReferencedProperties != null)
				{
					this.SelectedNavigationProperties.ExceptWith(this.ReferencedProperties.Keys);
				}
			}
			if (this.SelectedNavigationProperties != null && !this.SelectedNavigationProperties.Any<IEdmNavigationProperty>())
			{
				this.SelectedNavigationProperties = null;
			}
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00037BD1 File Offset: 0x00035DD1
		internal static bool IsComplexOrCollectionComplex(IEdmStructuralProperty structuralProperty)
		{
			return structuralProperty != null && (structuralProperty.Type.IsComplex() || (structuralProperty.Type.IsCollection() && structuralProperty.Type.AsCollection().ElementType().IsComplex()));
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00037C10 File Offset: 0x00035E10
		[Obsolete("This public method is not used anymore. It will be removed later.")]
		public static void GetStructuralProperties(IEdmStructuredType structuredType, HashSet<IEdmStructuralProperty> structuralProperties, HashSet<IEdmStructuralProperty> nestedStructuralProperties)
		{
			if (structuredType == null)
			{
				throw Error.ArgumentNull("structuredType");
			}
			if (structuralProperties == null)
			{
				throw Error.ArgumentNull("structuralProperties");
			}
			if (nestedStructuralProperties == null)
			{
				throw Error.ArgumentNull("nestedStructuralProperties");
			}
			foreach (IEdmStructuralProperty edmStructuralProperty in structuredType.StructuralProperties())
			{
				if (edmStructuralProperty.Type.IsComplex())
				{
					nestedStructuralProperties.Add(edmStructuralProperty);
				}
				else if (edmStructuralProperty.Type.IsCollection())
				{
					if (edmStructuralProperty.Type.AsCollection().ElementType().IsComplex())
					{
						nestedStructuralProperties.Add(edmStructuralProperty);
					}
					else
					{
						structuralProperties.Add(edmStructuralProperty);
					}
				}
				else
				{
					structuralProperties.Add(edmStructuralProperty);
				}
			}
		}

		// Token: 0x0200035B RID: 859
		internal class EdmStructuralTypeInfo
		{
			// Token: 0x170004A9 RID: 1193
			// (get) Token: 0x060014F9 RID: 5369 RVA: 0x0004516B File Offset: 0x0004336B
			public ISet<IEdmStructuralProperty> AllStructuralProperties { get; }

			// Token: 0x170004AA RID: 1194
			// (get) Token: 0x060014FA RID: 5370 RVA: 0x00045173 File Offset: 0x00043373
			public ISet<IEdmNavigationProperty> AllNavigationProperties { get; }

			// Token: 0x170004AB RID: 1195
			// (get) Token: 0x060014FB RID: 5371 RVA: 0x0004517B File Offset: 0x0004337B
			public ISet<IEdmAction> AllActions { get; }

			// Token: 0x170004AC RID: 1196
			// (get) Token: 0x060014FC RID: 5372 RVA: 0x00045183 File Offset: 0x00043383
			public ISet<IEdmFunction> AllFunctions { get; }

			// Token: 0x060014FD RID: 5373 RVA: 0x0004518C File Offset: 0x0004338C
			public EdmStructuralTypeInfo(IEdmModel model, IEdmStructuredType structuredType)
			{
				foreach (IEdmProperty edmProperty in structuredType.Properties())
				{
					EdmPropertyKind propertyKind = edmProperty.PropertyKind;
					if (propertyKind != EdmPropertyKind.Structural)
					{
						if (propertyKind == EdmPropertyKind.Navigation)
						{
							if (this.AllNavigationProperties == null)
							{
								this.AllNavigationProperties = new HashSet<IEdmNavigationProperty>();
							}
							this.AllNavigationProperties.Add((IEdmNavigationProperty)edmProperty);
						}
					}
					else
					{
						if (this.AllStructuralProperties == null)
						{
							this.AllStructuralProperties = new HashSet<IEdmStructuralProperty>();
						}
						this.AllStructuralProperties.Add((IEdmStructuralProperty)edmProperty);
					}
				}
				IEdmEntityType edmEntityType = structuredType as IEdmEntityType;
				if (edmEntityType != null)
				{
					IEnumerable<IEdmAction> availableActions = model.GetAvailableActions(edmEntityType);
					this.AllActions = (availableActions.Any<IEdmAction>() ? new HashSet<IEdmAction>(availableActions) : null);
					IEnumerable<IEdmFunction> availableFunctions = model.GetAvailableFunctions(edmEntityType);
					this.AllFunctions = (availableFunctions.Any<IEdmFunction>() ? new HashSet<IEdmFunction>(availableFunctions) : null);
				}
			}

			// Token: 0x060014FE RID: 5374 RVA: 0x00045284 File Offset: 0x00043484
			public bool IsStructuralPropertyDefined(IEdmStructuralProperty property)
			{
				return this.AllStructuralProperties != null && this.AllStructuralProperties.Contains(property);
			}

			// Token: 0x060014FF RID: 5375 RVA: 0x0004529C File Offset: 0x0004349C
			public bool IsNavigationPropertyDefined(IEdmNavigationProperty property)
			{
				return this.AllNavigationProperties != null && this.AllNavigationProperties.Contains(property);
			}
		}
	}
}
