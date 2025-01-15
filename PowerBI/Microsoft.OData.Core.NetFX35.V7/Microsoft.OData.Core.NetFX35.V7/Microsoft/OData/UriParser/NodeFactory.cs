using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000115 RID: 277
	internal static class NodeFactory
	{
		// Token: 0x06000CE5 RID: 3301 RVA: 0x00024804 File Offset: 0x00022A04
		internal static RangeVariable CreateImplicitRangeVariable(ODataPath path)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPath>(path, "path");
			IEdmTypeReference edmTypeReference = path.EdmType();
			if (edmTypeReference == null)
			{
				return null;
			}
			if (edmTypeReference.IsCollection())
			{
				edmTypeReference = edmTypeReference.AsCollection().ElementType();
			}
			if (edmTypeReference.IsStructured())
			{
				IEdmStructuredTypeReference edmStructuredTypeReference = edmTypeReference.AsStructured();
				return new ResourceRangeVariable("$it", edmStructuredTypeReference, path.NavigationSource());
			}
			return new NonResourceRangeVariable("$it", edmTypeReference, null);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002486A File Offset: 0x00022A6A
		internal static RangeVariable CreateImplicitRangeVariable(IEdmTypeReference elementType, IEdmNavigationSource navigationSource)
		{
			if (elementType.IsStructured())
			{
				return new ResourceRangeVariable("$it", elementType as IEdmStructuredTypeReference, navigationSource);
			}
			return new NonResourceRangeVariable("$it", elementType, null);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00024894 File Offset: 0x00022A94
		internal static SingleValueNode CreateRangeVariableReferenceNode(RangeVariable rangeVariable)
		{
			if (rangeVariable.Kind == 1)
			{
				return new NonResourceRangeVariableReferenceNode(rangeVariable.Name, (NonResourceRangeVariable)rangeVariable);
			}
			ResourceRangeVariable resourceRangeVariable = (ResourceRangeVariable)rangeVariable;
			return new ResourceRangeVariableReferenceNode(resourceRangeVariable.Name, resourceRangeVariable);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x000248D0 File Offset: 0x00022AD0
		internal static RangeVariable CreateParameterNode(string parameter, CollectionNode nodeToIterateOver)
		{
			IEdmTypeReference itemType = nodeToIterateOver.ItemType;
			if (itemType != null && itemType.IsStructured())
			{
				CollectionResourceNode collectionResourceNode = nodeToIterateOver as CollectionResourceNode;
				return new ResourceRangeVariable(parameter, itemType as IEdmStructuredTypeReference, collectionResourceNode);
			}
			return new NonResourceRangeVariable(parameter, itemType, null);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002490C File Offset: 0x00022B0C
		internal static LambdaNode CreateLambdaNode(BindingState state, CollectionNode parent, SingleValueNode lambdaExpression, RangeVariable newRangeVariable, QueryTokenKind queryTokenKind)
		{
			LambdaNode lambdaNode;
			if (queryTokenKind == QueryTokenKind.Any)
			{
				lambdaNode = new AnyNode(new Collection<RangeVariable>(Enumerable.ToList<RangeVariable>(state.RangeVariables)), newRangeVariable)
				{
					Body = lambdaExpression,
					Source = parent
				};
			}
			else
			{
				lambdaNode = new AllNode(new Collection<RangeVariable>(Enumerable.ToList<RangeVariable>(state.RangeVariables)), newRangeVariable)
				{
					Body = lambdaExpression,
					Source = parent
				};
			}
			return lambdaNode;
		}
	}
}
