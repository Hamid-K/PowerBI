using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200087F RID: 2175
	internal static class ODataQueryBuilderExtensions
	{
		// Token: 0x06003E8F RID: 16015 RVA: 0x000CCEDC File Offset: 0x000CB0DC
		public static EntityRangeVariableReferenceNode ImplicitEntityRangeVariableReferenceNode(this ODataPath path)
		{
			return new EntityRangeVariableReferenceNode("$it", path.ImplicitEntityRangeVariable());
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x000CCEF0 File Offset: 0x000CB0F0
		public static SingleValueNode PropertyAccessNode(this EntityRangeVariableReferenceNode entityRangeVariableReferenceNode, string propertyName)
		{
			Microsoft.OData.Edm.IEdmProperty edmProperty = entityRangeVariableReferenceNode.TypeReference.AsStructured().FindProperty(propertyName);
			if (edmProperty == null)
			{
				edmProperty = entityRangeVariableReferenceNode.NavigationSource.EntityType().DeclaredProperties.Where((Microsoft.OData.Edm.IEdmProperty e) => e.Name.Equals(propertyName)).SingleOrDefault<Microsoft.OData.Edm.IEdmProperty>();
				if (edmProperty == null)
				{
					throw new NotSupportedException();
				}
			}
			if (edmProperty.Type.IsCollection())
			{
				Microsoft.OData.Edm.IEdmTypeReference elementType = ((Microsoft.OData.Edm.IEdmCollectionType)edmProperty.Type.AsCollection().Definition).ElementType;
				NonentityRangeVariable nonentityRangeVariable = new NonentityRangeVariable("e", elementType, null);
				EntityRangeVariable entityRangeVariable = new EntityRangeVariable("$it", entityRangeVariableReferenceNode.EntityTypeReference, entityRangeVariableReferenceNode.NavigationSource);
				return new AnyNode(new Collection<RangeVariable> { nonentityRangeVariable, entityRangeVariable }, nonentityRangeVariable)
				{
					Source = new CollectionPropertyAccessNode(new EntityRangeVariableReferenceNode("$it", entityRangeVariable), edmProperty)
				};
			}
			return new SingleValuePropertyAccessNode(entityRangeVariableReferenceNode, edmProperty);
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x000CCFDC File Offset: 0x000CB1DC
		public static bool IsTrueConstant(this SingleValueNode node)
		{
			if (node.Kind == QueryNodeKind.Constant)
			{
				ConstantNode constantNode = (ConstantNode)node;
				return constantNode.IsBooleanConstant() && (bool)constantNode.Value;
			}
			return false;
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x000CD010 File Offset: 0x000CB210
		public static bool IsFalseConstant(this SingleValueNode node)
		{
			if (node.Kind == QueryNodeKind.Constant)
			{
				ConstantNode constantNode = (ConstantNode)node;
				return constantNode.IsBooleanConstant() && !(bool)constantNode.Value;
			}
			return false;
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x000CD047 File Offset: 0x000CB247
		public static bool IsNullConstant(this SingleValueNode node)
		{
			return node.Kind == QueryNodeKind.Constant && (node as ConstantNode).Value == null;
		}

		// Token: 0x06003E94 RID: 16020 RVA: 0x000CD064 File Offset: 0x000CB264
		public static SingleValueFunctionCallNode CreateFunctionWithSingleParameterInWithSameReturnTypeOut(this QueryNode parameter, string name)
		{
			SingleValueNode singleValueNode = (SingleValueNode)parameter;
			return new SingleValueFunctionCallNode(name, new QueryNode[] { parameter }, singleValueNode.TypeReference);
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x000CD08E File Offset: 0x000CB28E
		public static SingleValueFunctionCallNode CreateStringReturnTypeFunctionCallNode(this IEnumerable<QueryNode> parameters, string name)
		{
			return new SingleValueFunctionCallNode(name, parameters, Microsoft.OData.Edm.Library.EdmCoreModel.Instance.GetPrimitive(Microsoft.OData.Edm.EdmPrimitiveTypeKind.String, true));
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x000CD0A4 File Offset: 0x000CB2A4
		public static SingleValueFunctionCallNode CreateBooleanReturnTypeFunctionCallNode(this IEnumerable<QueryNode> parameters, string name)
		{
			return new SingleValueFunctionCallNode(name, parameters, Microsoft.OData.Edm.Library.EdmCoreModel.Instance.GetPrimitive(Microsoft.OData.Edm.EdmPrimitiveTypeKind.Boolean, false));
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x000CD0B9 File Offset: 0x000CB2B9
		public static SingleValueFunctionCallNode CreateInt32ReturnTypeFunctionCallNode(this IEnumerable<QueryNode> parameters, string name)
		{
			return new SingleValueFunctionCallNode(name, parameters, Microsoft.OData.Edm.Library.EdmCoreModel.Instance.GetPrimitive(Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int32, false));
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x000CD0D0 File Offset: 0x000CB2D0
		public static EntityRangeVariable ImplicitEntityRangeVariable(this ODataPath path)
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
			return new EntityRangeVariable("$it", new Microsoft.OData.Edm.Library.EdmEntityTypeReference(edmEntityType, true), ODataQueryBuilderExtensions.NavigationSource(path));
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x000CD122 File Offset: 0x000CB322
		private static Microsoft.OData.Edm.IEdmNavigationSource NavigationSource(ODataPath path)
		{
			return path.LastSegment.TranslateWith<Microsoft.OData.Edm.IEdmNavigationSource>(new ODataQueryBuilderExtensions.DetermineNavigationSourceTranslator());
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x000CD134 File Offset: 0x000CB334
		private static bool IsBooleanConstant(this ConstantNode node)
		{
			Microsoft.OData.Edm.IEdmPrimitiveType edmPrimitiveType = node.TypeReference.Definition as Microsoft.OData.Edm.IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind == Microsoft.OData.Edm.EdmPrimitiveTypeKind.Boolean;
		}

		// Token: 0x040020E1 RID: 8417
		public const string EValue = "e";

		// Token: 0x040020E2 RID: 8418
		public const string ItValue = "$it";

		// Token: 0x02000880 RID: 2176
		private sealed class DetermineNavigationSourceTranslator : PathSegmentTranslator<Microsoft.OData.Edm.IEdmNavigationSource>
		{
			// Token: 0x06003E9B RID: 16027 RVA: 0x000CD160 File Offset: 0x000CB360
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(SingletonSegment segment)
			{
				return segment.Singleton;
			}

			// Token: 0x06003E9C RID: 16028 RVA: 0x000CD168 File Offset: 0x000CB368
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(NavigationPropertyLinkSegment segment)
			{
				return segment.NavigationSource;
			}

			// Token: 0x06003E9D RID: 16029 RVA: 0x000CD170 File Offset: 0x000CB370
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(TypeSegment segment)
			{
				return segment.NavigationSource;
			}

			// Token: 0x06003E9E RID: 16030 RVA: 0x000CD178 File Offset: 0x000CB378
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(NavigationPropertySegment segment)
			{
				return segment.NavigationSource;
			}

			// Token: 0x06003E9F RID: 16031 RVA: 0x000CD180 File Offset: 0x000CB380
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(EntitySetSegment segment)
			{
				return segment.EntitySet;
			}

			// Token: 0x06003EA0 RID: 16032 RVA: 0x000CD188 File Offset: 0x000CB388
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(KeySegment segment)
			{
				return segment.NavigationSource;
			}

			// Token: 0x06003EA1 RID: 16033 RVA: 0x000020FA File Offset: 0x000002FA
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(PropertySegment segment)
			{
				return null;
			}

			// Token: 0x06003EA2 RID: 16034 RVA: 0x000CD190 File Offset: 0x000CB390
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(OperationImportSegment segment)
			{
				return segment.EntitySet;
			}

			// Token: 0x06003EA3 RID: 16035 RVA: 0x000CD198 File Offset: 0x000CB398
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(OperationSegment segment)
			{
				return segment.EntitySet;
			}

			// Token: 0x06003EA4 RID: 16036 RVA: 0x000020FA File Offset: 0x000002FA
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(CountSegment segment)
			{
				return null;
			}

			// Token: 0x06003EA5 RID: 16037 RVA: 0x000020FA File Offset: 0x000002FA
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(OpenPropertySegment segment)
			{
				return null;
			}

			// Token: 0x06003EA6 RID: 16038 RVA: 0x000020FA File Offset: 0x000002FA
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(ValueSegment segment)
			{
				return null;
			}

			// Token: 0x06003EA7 RID: 16039 RVA: 0x000020FA File Offset: 0x000002FA
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(BatchSegment segment)
			{
				return null;
			}

			// Token: 0x06003EA8 RID: 16040 RVA: 0x000CD1A0 File Offset: 0x000CB3A0
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(BatchReferenceSegment segment)
			{
				return segment.EntitySet;
			}

			// Token: 0x06003EA9 RID: 16041 RVA: 0x000020FA File Offset: 0x000002FA
			public override Microsoft.OData.Edm.IEdmNavigationSource Translate(MetadataSegment segment)
			{
				return null;
			}
		}
	}
}
