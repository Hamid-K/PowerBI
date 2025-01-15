using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000122 RID: 290
	internal sealed class EndPathBinder : BinderBase
	{
		// Token: 0x06000FC6 RID: 4038 RVA: 0x000277EB File Offset: 0x000259EB
		internal EndPathBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
			: base(bindMethod, state)
		{
			this.functionCallBinder = new FunctionCallBinder(bindMethod, state);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00027804 File Offset: 0x00025A04
		internal static QueryNode GeneratePropertyAccessQueryNode(SingleResourceNode parentNode, IEdmProperty property, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleResourceNode>(parentNode, "parentNode");
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			if (property.Type.IsNonEntityCollectionType())
			{
				if (property.Type.IsStructuredCollectionType())
				{
					return new CollectionComplexNode(parentNode, property);
				}
				return new CollectionPropertyAccessNode(parentNode, property);
			}
			else if (property.PropertyKind == EdmPropertyKind.Navigation)
			{
				IEdmNavigationProperty edmNavigationProperty = (IEdmNavigationProperty)property;
				if (edmNavigationProperty.TargetMultiplicity() == EdmMultiplicity.Many)
				{
					return new CollectionNavigationNode(parentNode, edmNavigationProperty, state.ParsedSegments);
				}
				return new SingleNavigationNode(parentNode, edmNavigationProperty, state.ParsedSegments);
			}
			else
			{
				if (property.Type.IsComplex())
				{
					return new SingleComplexNode(parentNode, property);
				}
				return new SingleValuePropertyAccessNode(parentNode, property);
			}
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x000278A4 File Offset: 0x00025AA4
		internal static SingleValueNode CreateParentFromImplicitRangeVariable(BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			if (state.ImplicitRangeVariable == null)
			{
				throw new ODataException(Strings.MetadataBinder_PropertyAccessWithoutParentParameter);
			}
			return NodeFactory.CreateRangeVariableReferenceNode(state.ImplicitRangeVariable);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x000278D0 File Offset: 0x00025AD0
		internal SingleValueOpenPropertyAccessNode GeneratePropertyAccessQueryForOpenType(EndPathToken endPathToken, SingleValueNode parentNode)
		{
			if (parentNode.TypeReference == null || parentNode.TypeReference.Definition.IsOpen() || this.IsAggregatedProperty(endPathToken))
			{
				return new SingleValueOpenPropertyAccessNode(parentNode, endPathToken.Identifier);
			}
			throw new ODataException(Strings.MetadataBinder_PropertyNotDeclared(parentNode.TypeReference.FullName(), endPathToken.Identifier));
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00027928 File Offset: 0x00025B28
		internal QueryNode BindEndPath(EndPathToken endPathToken)
		{
			ExceptionUtils.CheckArgumentNotNull<EndPathToken>(endPathToken, "EndPathToken");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(endPathToken.Identifier, "EndPathToken.Identifier");
			QueryNode queryNode = this.DetermineParentNode(endPathToken);
			SingleValueNode singleValueNode = queryNode as SingleValueNode;
			if (singleValueNode != null)
			{
				if (endPathToken.Identifier == "$count")
				{
					return new CountVirtualPropertyNode();
				}
				if (this.state.IsCollapsed && !this.IsAggregatedProperty(endPathToken))
				{
					throw new ODataException(Strings.ApplyBinder_GroupByPropertyNotPropertyAccessValue(endPathToken.Identifier));
				}
				IEdmStructuredTypeReference edmStructuredTypeReference = ((singleValueNode.TypeReference == null) ? null : singleValueNode.TypeReference.AsStructuredOrNull());
				IEdmProperty edmProperty = ((edmStructuredTypeReference == null) ? null : base.Resolver.ResolveProperty(edmStructuredTypeReference.StructuredDefinition(), endPathToken.Identifier));
				if (edmProperty != null)
				{
					return EndPathBinder.GeneratePropertyAccessQueryNode(singleValueNode as SingleResourceNode, edmProperty, this.state);
				}
				QueryNode queryNode2;
				if (this.functionCallBinder.TryBindEndPathAsFunctionCall(endPathToken, singleValueNode, this.state, out queryNode2))
				{
					return queryNode2;
				}
				return this.GeneratePropertyAccessQueryForOpenType(endPathToken, singleValueNode);
			}
			else
			{
				CollectionNode collectionNode = queryNode as CollectionNode;
				if (collectionNode != null && endPathToken.Identifier.Equals("$count"))
				{
					return new CountNode(collectionNode);
				}
				CollectionNavigationNode collectionNavigationNode = queryNode as CollectionNavigationNode;
				if (collectionNavigationNode != null)
				{
					IEdmEntityTypeReference entityItemType = collectionNavigationNode.EntityItemType;
					IEdmProperty edmProperty2 = base.Resolver.ResolveProperty(entityItemType.StructuredDefinition(), endPathToken.Identifier);
					if (edmProperty2.PropertyKind == EdmPropertyKind.Structural && !edmProperty2.Type.IsCollection() && this.state.InEntitySetAggregation)
					{
						return new AggregatedCollectionPropertyNode(collectionNavigationNode, edmProperty2);
					}
				}
				QueryNode queryNode2;
				if (this.functionCallBinder.TryBindEndPathAsFunctionCall(endPathToken, queryNode, this.state, out queryNode2))
				{
					return queryNode2;
				}
				throw new ODataException(Strings.MetadataBinder_PropertyAccessSourceNotSingleValue(endPathToken.Identifier));
			}
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00027AC4 File Offset: 0x00025CC4
		private QueryNode DetermineParentNode(EndPathToken segmentToken)
		{
			ExceptionUtils.CheckArgumentNotNull<EndPathToken>(segmentToken, "segmentToken");
			ExceptionUtils.CheckArgumentNotNull<BindingState>(this.state, "state");
			if (segmentToken.NextToken != null)
			{
				return this.bindMethod(segmentToken.NextToken);
			}
			RangeVariable implicitRangeVariable = this.state.ImplicitRangeVariable;
			return NodeFactory.CreateRangeVariableReferenceNode(implicitRangeVariable);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00027B1C File Offset: 0x00025D1C
		private bool IsAggregatedProperty(EndPathToken endPath)
		{
			BindingState state = this.state;
			bool? flag;
			if (state == null)
			{
				flag = null;
			}
			else
			{
				HashSet<EndPathToken> aggregatedPropertyNames = state.AggregatedPropertyNames;
				flag = ((aggregatedPropertyNames != null) ? new bool?(aggregatedPropertyNames.Contains(endPath)) : null);
			}
			return flag ?? false;
		}

		// Token: 0x040007A2 RID: 1954
		private readonly FunctionCallBinder functionCallBinder;
	}
}
