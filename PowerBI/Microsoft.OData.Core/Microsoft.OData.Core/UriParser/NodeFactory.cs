using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000158 RID: 344
	internal static class NodeFactory
	{
		// Token: 0x06001190 RID: 4496 RVA: 0x0003233C File Offset: 0x0003053C
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

		// Token: 0x06001191 RID: 4497 RVA: 0x000323A2 File Offset: 0x000305A2
		internal static RangeVariable CreateImplicitRangeVariable(IEdmTypeReference elementType, IEdmNavigationSource navigationSource)
		{
			if (elementType.IsStructured())
			{
				return new ResourceRangeVariable("$it", elementType as IEdmStructuredTypeReference, navigationSource);
			}
			return new NonResourceRangeVariable("$it", elementType, null);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x000323CC File Offset: 0x000305CC
		internal static SingleValueNode CreateRangeVariableReferenceNode(RangeVariable rangeVariable)
		{
			if (rangeVariable.Kind == 1)
			{
				return new NonResourceRangeVariableReferenceNode(rangeVariable.Name, (NonResourceRangeVariable)rangeVariable);
			}
			ResourceRangeVariable resourceRangeVariable = (ResourceRangeVariable)rangeVariable;
			return new ResourceRangeVariableReferenceNode(resourceRangeVariable.Name, resourceRangeVariable);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00032408 File Offset: 0x00030608
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

		// Token: 0x06001194 RID: 4500 RVA: 0x00032444 File Offset: 0x00030644
		internal static LambdaNode CreateLambdaNode(BindingState state, CollectionNode parent, SingleValueNode lambdaExpression, RangeVariable newRangeVariable, QueryTokenKind queryTokenKind)
		{
			LambdaNode lambdaNode;
			if (queryTokenKind == QueryTokenKind.Any)
			{
				lambdaNode = new AnyNode(new Collection<RangeVariable>(state.RangeVariables.ToList<RangeVariable>()), newRangeVariable)
				{
					Body = lambdaExpression,
					Source = parent
				};
			}
			else
			{
				lambdaNode = new AllNode(new Collection<RangeVariable>(state.RangeVariables.ToList<RangeVariable>()), newRangeVariable)
				{
					Body = lambdaExpression,
					Source = parent
				};
			}
			return lambdaNode;
		}
	}
}
