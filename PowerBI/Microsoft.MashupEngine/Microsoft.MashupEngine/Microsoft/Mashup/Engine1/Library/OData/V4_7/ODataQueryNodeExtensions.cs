using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7
{
	// Token: 0x02000772 RID: 1906
	internal static class ODataQueryNodeExtensions
	{
		// Token: 0x06003819 RID: 14361 RVA: 0x000B3C7C File Offset: 0x000B1E7C
		public static ResourceRangeVariableReferenceNode ImplicitResourceRangeVariableReferenceNode(this ODataPath path)
		{
			return new ResourceRangeVariableReferenceNode("$it", path.ImplicitResourceRangeVariable());
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x000B3C90 File Offset: 0x000B1E90
		public static SingleValueNode PropertyAccessNode(this ResourceRangeVariableReferenceNode resourceRangeVariableReferenceNode, string propertyName)
		{
			Microsoft.OData.Edm.IEdmProperty edmProperty = resourceRangeVariableReferenceNode.TypeReference.AsStructured().FindProperty(propertyName);
			if (edmProperty == null)
			{
				edmProperty = resourceRangeVariableReferenceNode.NavigationSource.EntityType().DeclaredProperties.Where((Microsoft.OData.Edm.IEdmProperty e) => e.Name.Equals(propertyName)).SingleOrDefault<Microsoft.OData.Edm.IEdmProperty>();
				if (edmProperty == null)
				{
					throw new NotSupportedException();
				}
			}
			if (edmProperty.Type.IsCollection())
			{
				Microsoft.OData.Edm.IEdmTypeReference elementType = ((Microsoft.OData.Edm.IEdmCollectionType)edmProperty.Type.AsCollection().Definition).ElementType;
				NonResourceRangeVariable nonResourceRangeVariable = new NonResourceRangeVariable("e", elementType, null);
				ResourceRangeVariable resourceRangeVariable = new ResourceRangeVariable("$it", resourceRangeVariableReferenceNode.StructuredTypeReference, resourceRangeVariableReferenceNode.NavigationSource);
				return new AnyNode(new Collection<RangeVariable> { nonResourceRangeVariable, resourceRangeVariable }, nonResourceRangeVariable)
				{
					Source = new CollectionPropertyAccessNode(new ResourceRangeVariableReferenceNode("$it", resourceRangeVariable), edmProperty)
				};
			}
			return new SingleValuePropertyAccessNode(resourceRangeVariableReferenceNode, edmProperty);
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x000B3D7C File Offset: 0x000B1F7C
		public static bool IsTrueConstant(this SingleValueNode node)
		{
			if (node.Kind == QueryNodeKind.Constant)
			{
				ConstantNode constantNode = (ConstantNode)node;
				return constantNode.IsBooleanConstant() && (bool)constantNode.Value;
			}
			return false;
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x000B3DB0 File Offset: 0x000B1FB0
		public static bool IsFalseConstant(this SingleValueNode node)
		{
			if (node.Kind == QueryNodeKind.Constant)
			{
				ConstantNode constantNode = (ConstantNode)node;
				return constantNode.IsBooleanConstant() && !(bool)constantNode.Value;
			}
			return false;
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x000B3DE7 File Offset: 0x000B1FE7
		public static bool IsNullConstant(this SingleValueNode node)
		{
			return node.Kind == QueryNodeKind.Constant && (node as ConstantNode).Value == null;
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x000B3E04 File Offset: 0x000B2004
		public static SingleValueFunctionCallNode CreateFunctionWithSingleParameterInWithSameReturnTypeOut(this QueryNode parameter, string name)
		{
			SingleValueNode singleValueNode = (SingleValueNode)parameter;
			return new SingleValueFunctionCallNode(name, new QueryNode[] { parameter }, singleValueNode.TypeReference);
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000B3E2E File Offset: 0x000B202E
		public static SingleValueFunctionCallNode CreateStringReturnTypeFunctionCallNode(this IEnumerable<QueryNode> parameters, string name)
		{
			return new SingleValueFunctionCallNode(name, parameters, EdmCoreModel.Instance.GetPrimitive(Microsoft.OData.Edm.EdmPrimitiveTypeKind.String, true));
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x000B3E44 File Offset: 0x000B2044
		public static SingleValueFunctionCallNode CreateBooleanReturnTypeFunctionCallNode(this IEnumerable<QueryNode> parameters, string name)
		{
			return new SingleValueFunctionCallNode(name, parameters, EdmCoreModel.Instance.GetPrimitive(Microsoft.OData.Edm.EdmPrimitiveTypeKind.Boolean, false));
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x000B3E59 File Offset: 0x000B2059
		public static SingleValueFunctionCallNode CreateInt32ReturnTypeFunctionCallNode(this IEnumerable<QueryNode> parameters, string name)
		{
			return new SingleValueFunctionCallNode(name, parameters, EdmCoreModel.Instance.GetPrimitive(Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int32, false));
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000B3E70 File Offset: 0x000B2070
		public static ResourceRangeVariable ImplicitResourceRangeVariable(this ODataPath path)
		{
			Microsoft.OData.Edm.IEdmType edmType = path.LastSegment.EdmType;
			if (edmType == null)
			{
				return null;
			}
			Microsoft.OData.Edm.IEdmCollectionType edmCollectionType = edmType as Microsoft.OData.Edm.IEdmCollectionType;
			if (edmCollectionType != null)
			{
				edmType = edmCollectionType.ElementType.Definition;
			}
			Microsoft.OData.Edm.IEdmEntityType edmEntityType = (Microsoft.OData.Edm.IEdmEntityType)edmType;
			return new ResourceRangeVariable("$it", new EdmEntityTypeReference(edmEntityType, true), path.NavigationSource());
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000B3EC2 File Offset: 0x000B20C2
		public static Microsoft.OData.Edm.IEdmNavigationSource NavigationSource(this ODataPath path)
		{
			return path.LastSegment.TranslateWith<Microsoft.OData.Edm.IEdmNavigationSource>(new ODataQueryNodeExtensions.DetermineNavigationSourceTranslator());
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000B3ED4 File Offset: 0x000B20D4
		private static bool IsBooleanConstant(this ConstantNode node)
		{
			Microsoft.OData.Edm.IEdmPrimitiveType edmPrimitiveType = node.TypeReference.Definition as Microsoft.OData.Edm.IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind == Microsoft.OData.Edm.EdmPrimitiveTypeKind.Boolean;
		}

		// Token: 0x04001D12 RID: 7442
		public const string EValue = "e";

		// Token: 0x04001D13 RID: 7443
		public const string ItValue = "$it";

		// Token: 0x02000773 RID: 1907
		private sealed class DetermineNavigationSourceTranslator : PathSegmentTranslator<Microsoft.OData.Edm.IEdmNavigationSource>
		{
			// Token: 0x06003825 RID: 14373 RVA: 0x000B3F00 File Offset: 0x000B2100
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(SingletonSegment segment)
			{
				return segment.Singleton;
			}

			// Token: 0x06003826 RID: 14374 RVA: 0x000B3F08 File Offset: 0x000B2108
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(NavigationPropertyLinkSegment segment)
			{
				return segment.NavigationSource;
			}

			// Token: 0x06003827 RID: 14375 RVA: 0x000B3F10 File Offset: 0x000B2110
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(TypeSegment segment)
			{
				return segment.NavigationSource;
			}

			// Token: 0x06003828 RID: 14376 RVA: 0x000B3F18 File Offset: 0x000B2118
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(NavigationPropertySegment segment)
			{
				return segment.NavigationSource;
			}

			// Token: 0x06003829 RID: 14377 RVA: 0x000B3F20 File Offset: 0x000B2120
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(EntitySetSegment segment)
			{
				return segment.EntitySet;
			}

			// Token: 0x0600382A RID: 14378 RVA: 0x000B3F28 File Offset: 0x000B2128
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(KeySegment segment)
			{
				return segment.NavigationSource;
			}

			// Token: 0x0600382B RID: 14379 RVA: 0x000020FA File Offset: 0x000002FA
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(PropertySegment segment)
			{
				return null;
			}

			// Token: 0x0600382C RID: 14380 RVA: 0x000B3F30 File Offset: 0x000B2130
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(OperationImportSegment segment)
			{
				return segment.EntitySet;
			}

			// Token: 0x0600382D RID: 14381 RVA: 0x000B3F38 File Offset: 0x000B2138
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(OperationSegment segment)
			{
				return segment.EntitySet;
			}

			// Token: 0x0600382E RID: 14382 RVA: 0x000020FA File Offset: 0x000002FA
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(CountSegment segment)
			{
				return null;
			}

			// Token: 0x0600382F RID: 14383 RVA: 0x000020FA File Offset: 0x000002FA
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(DynamicPathSegment segment)
			{
				return null;
			}

			// Token: 0x06003830 RID: 14384 RVA: 0x000020FA File Offset: 0x000002FA
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(ValueSegment segment)
			{
				return null;
			}

			// Token: 0x06003831 RID: 14385 RVA: 0x000020FA File Offset: 0x000002FA
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(BatchSegment segment)
			{
				return null;
			}

			// Token: 0x06003832 RID: 14386 RVA: 0x000B3F40 File Offset: 0x000B2140
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(BatchReferenceSegment segment)
			{
				return segment.EntitySet;
			}

			// Token: 0x06003833 RID: 14387 RVA: 0x000020FA File Offset: 0x000002FA
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(MetadataSegment segment)
			{
				return null;
			}
		}
	}
}
