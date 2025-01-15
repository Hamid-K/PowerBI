using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000059 RID: 89
	internal sealed class EndPathBinder
	{
		// Token: 0x0600023D RID: 573 RVA: 0x000086BD File Offset: 0x000068BD
		internal EndPathBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			this.bind = bindMethod;
			this.functionCallBinder = new FunctionCallBinder(bindMethod);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000086D8 File Offset: 0x000068D8
		internal static SingleValueOpenPropertyAccessNode GeneratePropertyAccessQueryForOpenType(EndPathToken endPathToken, SingleValueNode parentNode)
		{
			if (parentNode.TypeReference != null && !parentNode.TypeReference.Definition.IsOpenType())
			{
				throw new ODataException(Strings.MetadataBinder_PropertyNotDeclared(parentNode.TypeReference.ODataFullName(), endPathToken.Identifier));
			}
			return new SingleValueOpenPropertyAccessNode(parentNode, endPathToken.Identifier);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00008728 File Offset: 0x00006928
		internal static QueryNode GeneratePropertyAccessQueryNode(SingleValueNode parentNode, IEdmProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(parentNode, "parent");
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			if (property.Type.IsNonEntityCollectionType())
			{
				return new CollectionPropertyAccessNode(parentNode, property);
			}
			if (property.PropertyKind != EdmPropertyKind.Navigation)
			{
				return new SingleValuePropertyAccessNode(parentNode, property);
			}
			IEdmNavigationProperty edmNavigationProperty = (IEdmNavigationProperty)property;
			SingleEntityNode singleEntityNode = (SingleEntityNode)parentNode;
			if (edmNavigationProperty.TargetMultiplicityTemporary() == EdmMultiplicity.Many)
			{
				return new CollectionNavigationNode(edmNavigationProperty, singleEntityNode);
			}
			return new SingleNavigationNode(edmNavigationProperty, singleEntityNode);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00008797 File Offset: 0x00006997
		internal static SingleValueNode CreateParentFromImplicitRangeVariable(BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			if (state.ImplicitRangeVariable == null)
			{
				throw new ODataException(Strings.MetadataBinder_PropertyAccessWithoutParentParameter);
			}
			return NodeFactory.CreateRangeVariableReferenceNode(state.ImplicitRangeVariable);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000087C4 File Offset: 0x000069C4
		internal QueryNode BindEndPath(EndPathToken endPathToken, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<EndPathToken>(endPathToken, "EndPathToken");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(endPathToken.Identifier, "EndPathToken.Identifier");
			QueryNode queryNode = this.DetermineParentNode(endPathToken, state);
			SingleValueNode singleValueNode = queryNode as SingleValueNode;
			if (singleValueNode == null)
			{
				QueryNode queryNode2;
				if (this.functionCallBinder.TryBindEndPathAsFunctionCall(endPathToken, queryNode, state, out queryNode2))
				{
					return queryNode2;
				}
				throw new ODataException(Strings.MetadataBinder_PropertyAccessSourceNotSingleValue(endPathToken.Identifier));
			}
			else
			{
				IEdmStructuredTypeReference edmStructuredTypeReference = ((singleValueNode.TypeReference == null) ? null : singleValueNode.TypeReference.AsStructuredOrNull());
				IEdmProperty edmProperty = ((edmStructuredTypeReference == null) ? null : edmStructuredTypeReference.FindProperty(endPathToken.Identifier));
				if (edmProperty != null)
				{
					return EndPathBinder.GeneratePropertyAccessQueryNode(singleValueNode, edmProperty);
				}
				QueryNode queryNode2;
				if (this.functionCallBinder.TryBindEndPathAsFunctionCall(endPathToken, singleValueNode, state, out queryNode2))
				{
					return queryNode2;
				}
				return EndPathBinder.GeneratePropertyAccessQueryForOpenType(endPathToken, singleValueNode);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00008878 File Offset: 0x00006A78
		private QueryNode DetermineParentNode(EndPathToken segmentToken, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<EndPathToken>(segmentToken, "segmentToken");
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			if (segmentToken.NextToken != null)
			{
				return this.bind(segmentToken.NextToken);
			}
			RangeVariable implicitRangeVariable = state.ImplicitRangeVariable;
			return NodeFactory.CreateRangeVariableReferenceNode(implicitRangeVariable);
		}

		// Token: 0x04000089 RID: 137
		private readonly MetadataBinder.QueryTokenVisitor bind;

		// Token: 0x0400008A RID: 138
		private readonly FunctionCallBinder functionCallBinder;
	}
}
