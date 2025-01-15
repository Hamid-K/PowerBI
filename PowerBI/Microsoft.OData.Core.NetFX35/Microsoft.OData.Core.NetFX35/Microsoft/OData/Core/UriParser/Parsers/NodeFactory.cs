using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x02000207 RID: 519
	internal static class NodeFactory
	{
		// Token: 0x060012BB RID: 4795 RVA: 0x000441D0 File Offset: 0x000423D0
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
			if (edmTypeReference.IsEntity())
			{
				IEdmEntityTypeReference edmEntityTypeReference = edmTypeReference as IEdmEntityTypeReference;
				return new EntityRangeVariable("$it", edmEntityTypeReference, path.NavigationSource());
			}
			return new NonentityRangeVariable("$it", edmTypeReference, null);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00044235 File Offset: 0x00042435
		internal static RangeVariable CreateImplicitRangeVariable(IEdmTypeReference elementType, IEdmNavigationSource navigationSource)
		{
			if (elementType.IsEntity())
			{
				return new EntityRangeVariable("$it", elementType as IEdmEntityTypeReference, navigationSource);
			}
			return new NonentityRangeVariable("$it", elementType, null);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00044260 File Offset: 0x00042460
		internal static SingleValueNode CreateRangeVariableReferenceNode(RangeVariable rangeVariable)
		{
			if (rangeVariable.Kind == 1)
			{
				return new NonentityRangeVariableReferenceNode(rangeVariable.Name, (NonentityRangeVariable)rangeVariable);
			}
			EntityRangeVariable entityRangeVariable = (EntityRangeVariable)rangeVariable;
			return new EntityRangeVariableReferenceNode(entityRangeVariable.Name, entityRangeVariable);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0004429C File Offset: 0x0004249C
		internal static RangeVariable CreateParameterNode(string parameter, CollectionNode nodeToIterateOver)
		{
			IEdmTypeReference itemType = nodeToIterateOver.ItemType;
			if (itemType != null && itemType.IsEntity())
			{
				EntityCollectionNode entityCollectionNode = nodeToIterateOver as EntityCollectionNode;
				return new EntityRangeVariable(parameter, itemType as IEdmEntityTypeReference, entityCollectionNode);
			}
			return new NonentityRangeVariable(parameter, itemType, null);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x000442D8 File Offset: 0x000424D8
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
