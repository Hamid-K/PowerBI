using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query.Validators
{
	// Token: 0x020000DB RID: 219
	public class FilterQueryValidator
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x0001955C File Offset: 0x0001775C
		public FilterQueryValidator(DefaultQuerySettings defaultQuerySettings)
		{
			this._defaultQuerySettings = defaultQuerySettings;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001956C File Offset: 0x0001776C
		public virtual void Validate(FilterQueryOption filterQueryOption, ODataValidationSettings settings)
		{
			if (filterQueryOption == null)
			{
				throw Error.ArgumentNull("filterQueryOption");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
			if (filterQueryOption.Context.Path != null)
			{
				this._property = filterQueryOption.Context.TargetProperty;
				this._structuredType = filterQueryOption.Context.TargetStructuredType;
			}
			this.Validate(filterQueryOption.FilterClause, settings, filterQueryOption.Context.Model);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000195DC File Offset: 0x000177DC
		public virtual void Validate(FilterClause filterClause, ODataValidationSettings settings, IEdmModel model)
		{
			this._currentAnyAllExpressionDepth = 0;
			this._currentNodeCount = 0;
			this._model = model;
			this.ValidateQueryNode(filterClause.Expression, settings);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00019600 File Offset: 0x00017800
		internal virtual void Validate(IEdmProperty property, IEdmStructuredType structuredType, FilterClause filterClause, ODataValidationSettings settings, IEdmModel model)
		{
			this._property = property;
			this._structuredType = structuredType;
			this.Validate(filterClause, settings, model);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001961C File Offset: 0x0001781C
		public virtual void ValidateAllNode(AllNode allNode, ODataValidationSettings settings)
		{
			if (allNode == null)
			{
				throw Error.ArgumentNull("allNode");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
			FilterQueryValidator.ValidateFunction("all", settings);
			this.EnterLambda(settings);
			try
			{
				this.ValidateQueryNode(allNode.Source, settings);
				this.ValidateQueryNode(allNode.Body, settings);
			}
			finally
			{
				this.ExitLambda();
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001968C File Offset: 0x0001788C
		public virtual void ValidateAnyNode(AnyNode anyNode, ODataValidationSettings settings)
		{
			if (anyNode == null)
			{
				throw Error.ArgumentNull("anyNode");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
			FilterQueryValidator.ValidateFunction("any", settings);
			this.EnterLambda(settings);
			try
			{
				this.ValidateQueryNode(anyNode.Source, settings);
				if (anyNode.Body != null && anyNode.Body.Kind != QueryNodeKind.Constant)
				{
					this.ValidateQueryNode(anyNode.Body, settings);
				}
			}
			finally
			{
				this.ExitLambda();
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00019710 File Offset: 0x00017910
		public virtual void ValidateBinaryOperatorNode(BinaryOperatorNode binaryOperatorNode, ODataValidationSettings settings)
		{
			if (binaryOperatorNode == null)
			{
				throw Error.ArgumentNull("binaryOperatorNode");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
			BinaryOperatorKind operatorKind = binaryOperatorNode.OperatorKind;
			if (operatorKind <= BinaryOperatorKind.LessThanOrEqual || operatorKind == BinaryOperatorKind.Has)
			{
				this.ValidateLogicalOperator(binaryOperatorNode, settings);
				return;
			}
			this.ValidateArithmeticOperator(binaryOperatorNode, settings);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001975C File Offset: 0x0001795C
		public virtual void ValidateLogicalOperator(BinaryOperatorNode binaryNode, ODataValidationSettings settings)
		{
			if (binaryNode == null)
			{
				throw Error.ArgumentNull("binaryNode");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
			AllowedLogicalOperators allowedLogicalOperators = FilterQueryValidator.ToLogicalOperator(binaryNode);
			if ((settings.AllowedLogicalOperators & allowedLogicalOperators) != allowedLogicalOperators)
			{
				throw new ODataException(Error.Format(SRResources.NotAllowedLogicalOperator, new object[] { allowedLogicalOperators, "AllowedLogicalOperators" }));
			}
			this.ValidateQueryNode(binaryNode.Left, settings);
			this.ValidateQueryNode(binaryNode.Right, settings);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000197D8 File Offset: 0x000179D8
		public virtual void ValidateArithmeticOperator(BinaryOperatorNode binaryNode, ODataValidationSettings settings)
		{
			if (binaryNode == null)
			{
				throw Error.ArgumentNull("binaryNode");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
			AllowedArithmeticOperators allowedArithmeticOperators = FilterQueryValidator.ToArithmeticOperator(binaryNode);
			if ((settings.AllowedArithmeticOperators & allowedArithmeticOperators) != allowedArithmeticOperators)
			{
				throw new ODataException(Error.Format(SRResources.NotAllowedArithmeticOperator, new object[] { allowedArithmeticOperators, "AllowedArithmeticOperators" }));
			}
			this.ValidateQueryNode(binaryNode.Left, settings);
			this.ValidateQueryNode(binaryNode.Right, settings);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00019854 File Offset: 0x00017A54
		public virtual void ValidateConstantNode(ConstantNode constantNode, ODataValidationSettings settings)
		{
			if (constantNode == null)
			{
				throw Error.ArgumentNull("constantNode");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00019872 File Offset: 0x00017A72
		public virtual void ValidateConvertNode(ConvertNode convertNode, ODataValidationSettings settings)
		{
			if (convertNode == null)
			{
				throw Error.ArgumentNull("convertNode");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
			this.ValidateQueryNode(convertNode.Source, settings);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000198A0 File Offset: 0x00017AA0
		public virtual void ValidateNavigationPropertyNode(QueryNode sourceNode, IEdmNavigationProperty navigationProperty, ODataValidationSettings settings)
		{
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
			if (EdmLibHelpers.IsNotFilterable(navigationProperty, this._property, this._structuredType, this._model, this._defaultQuerySettings.EnableFilter))
			{
				throw new ODataException(Error.Format(SRResources.NotFilterablePropertyUsedInFilter, new object[] { navigationProperty.Name }));
			}
			if (sourceNode != null)
			{
				this.ValidateQueryNode(sourceNode, settings);
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001990A File Offset: 0x00017B0A
		public virtual void ValidateRangeVariable(RangeVariable rangeVariable, ODataValidationSettings settings)
		{
			if (rangeVariable == null)
			{
				throw Error.ArgumentNull("rangeVariable");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00019928 File Offset: 0x00017B28
		public virtual void ValidateSingleValuePropertyAccessNode(SingleValuePropertyAccessNode propertyAccessNode, ODataValidationSettings settings)
		{
			if (propertyAccessNode == null)
			{
				throw Error.ArgumentNull("propertyAccessNode");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
			IEdmProperty property = propertyAccessNode.Property;
			bool flag = false;
			if (propertyAccessNode.Source != null)
			{
				if (propertyAccessNode.Source.Kind == QueryNodeKind.SingleNavigationNode)
				{
					SingleNavigationNode singleNavigationNode = propertyAccessNode.Source as SingleNavigationNode;
					flag = EdmLibHelpers.IsNotFilterable(property, singleNavigationNode.NavigationProperty, singleNavigationNode.NavigationProperty.ToEntityType(), this._model, this._defaultQuerySettings.EnableFilter);
				}
				else if (propertyAccessNode.Source.Kind == QueryNodeKind.SingleComplexNode)
				{
					SingleComplexNode singleComplexNode = propertyAccessNode.Source as SingleComplexNode;
					flag = EdmLibHelpers.IsNotFilterable(property, singleComplexNode.Property, property.DeclaringType, this._model, this._defaultQuerySettings.EnableFilter);
				}
				else
				{
					flag = EdmLibHelpers.IsNotFilterable(property, this._property, this._structuredType, this._model, this._defaultQuerySettings.EnableFilter);
				}
			}
			if (flag)
			{
				throw new ODataException(Error.Format(SRResources.NotFilterablePropertyUsedInFilter, new object[] { property.Name }));
			}
			this.ValidateQueryNode(propertyAccessNode.Source, settings);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00019A40 File Offset: 0x00017C40
		public virtual void ValidateSingleComplexNode(SingleComplexNode singleComplexNode, ODataValidationSettings settings)
		{
			if (singleComplexNode == null)
			{
				throw Error.ArgumentNull("singleComplexNode");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
			IEdmProperty property = singleComplexNode.Property;
			if (EdmLibHelpers.IsNotFilterable(property, this._property, this._structuredType, this._model, this._defaultQuerySettings.EnableFilter))
			{
				throw new ODataException(Error.Format(SRResources.NotFilterablePropertyUsedInFilter, new object[] { property.Name }));
			}
			this.ValidateQueryNode(singleComplexNode.Source, settings);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00019AC4 File Offset: 0x00017CC4
		public virtual void ValidateCollectionPropertyAccessNode(CollectionPropertyAccessNode propertyAccessNode, ODataValidationSettings settings)
		{
			if (propertyAccessNode == null)
			{
				throw Error.ArgumentNull("propertyAccessNode");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
			IEdmProperty property = propertyAccessNode.Property;
			if (EdmLibHelpers.IsNotFilterable(property, this._property, this._structuredType, this._model, this._defaultQuerySettings.EnableFilter))
			{
				throw new ODataException(Error.Format(SRResources.NotFilterablePropertyUsedInFilter, new object[] { property.Name }));
			}
			this.ValidateQueryNode(propertyAccessNode.Source, settings);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00019B48 File Offset: 0x00017D48
		public virtual void ValidateCollectionComplexNode(CollectionComplexNode collectionComplexNode, ODataValidationSettings settings)
		{
			if (collectionComplexNode == null)
			{
				throw Error.ArgumentNull("collectionComplexNode");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
			IEdmProperty property = collectionComplexNode.Property;
			if (EdmLibHelpers.IsNotFilterable(property, this._property, this._structuredType, this._model, this._defaultQuerySettings.EnableFilter))
			{
				throw new ODataException(Error.Format(SRResources.NotFilterablePropertyUsedInFilter, new object[] { property.Name }));
			}
			this.ValidateQueryNode(collectionComplexNode.Source, settings);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00019BCC File Offset: 0x00017DCC
		public virtual void ValidateSingleValueFunctionCallNode(SingleValueFunctionCallNode node, ODataValidationSettings settings)
		{
			if (node == null)
			{
				throw Error.ArgumentNull("node");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
			FilterQueryValidator.ValidateFunction(node.Name, settings);
			foreach (QueryNode queryNode in node.Parameters)
			{
				this.ValidateQueryNode(queryNode, settings);
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00019C44 File Offset: 0x00017E44
		public virtual void ValidateSingleResourceFunctionCallNode(SingleResourceFunctionCallNode node, ODataValidationSettings settings)
		{
			if (node == null)
			{
				throw Error.ArgumentNull("node");
			}
			if (settings == null)
			{
				throw Error.ArgumentNull("settings");
			}
			FilterQueryValidator.ValidateFunction(node.Name, settings);
			foreach (QueryNode queryNode in node.Parameters)
			{
				this.ValidateQueryNode(queryNode, settings);
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00019CBC File Offset: 0x00017EBC
		public virtual void ValidateUnaryOperatorNode(UnaryOperatorNode unaryOperatorNode, ODataValidationSettings settings)
		{
			this.ValidateQueryNode(unaryOperatorNode.Operand, settings);
			UnaryOperatorKind operatorKind = unaryOperatorNode.OperatorKind;
			if (operatorKind > UnaryOperatorKind.Not)
			{
				throw Error.NotSupported(SRResources.UnaryNodeValidationNotSupported, new object[]
				{
					unaryOperatorNode.OperatorKind,
					typeof(FilterQueryValidator).Name
				});
			}
			if ((settings.AllowedLogicalOperators & AllowedLogicalOperators.Not) != AllowedLogicalOperators.Not)
			{
				throw new ODataException(Error.Format(SRResources.NotAllowedLogicalOperator, new object[] { unaryOperatorNode.OperatorKind, "AllowedLogicalOperators" }));
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00019D54 File Offset: 0x00017F54
		public virtual void ValidateQueryNode(QueryNode node, ODataValidationSettings settings)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			SingleValueNode singleValueNode = node as SingleValueNode;
			CollectionNode collectionNode = node as CollectionNode;
			this.IncrementNodeCount(settings);
			if (singleValueNode != null)
			{
				this.ValidateSingleValueNode(singleValueNode, settings);
				return;
			}
			if (collectionNode != null)
			{
				this.ValidateCollectionNode(collectionNode, settings);
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00019D92 File Offset: 0x00017F92
		public virtual void ValidateCollectionResourceCastNode(CollectionResourceCastNode collectionResourceCastNode, ODataValidationSettings settings)
		{
			if (collectionResourceCastNode == null)
			{
				throw Error.ArgumentNull("collectionResourceCastNode");
			}
			this.ValidateQueryNode(collectionResourceCastNode.Source, settings);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00019DAF File Offset: 0x00017FAF
		public virtual void ValidateSingleResourceCastNode(SingleResourceCastNode singleResourceCastNode, ODataValidationSettings settings)
		{
			if (singleResourceCastNode == null)
			{
				throw Error.ArgumentNull("singleResourceCastNode");
			}
			this.ValidateQueryNode(singleResourceCastNode.Source, settings);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00019DCC File Offset: 0x00017FCC
		internal static FilterQueryValidator GetFilterQueryValidator(ODataQueryContext context)
		{
			if (context == null)
			{
				return new FilterQueryValidator(new DefaultQuerySettings());
			}
			if (context.RequestContainer != null)
			{
				return ServiceProviderServiceExtensions.GetRequiredService<FilterQueryValidator>(context.RequestContainer);
			}
			return new FilterQueryValidator(context.DefaultQuerySettings);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00019DFC File Offset: 0x00017FFC
		private void EnterLambda(ODataValidationSettings validationSettings)
		{
			if (this._currentAnyAllExpressionDepth >= validationSettings.MaxAnyAllExpressionDepth)
			{
				throw new ODataException(Error.Format(SRResources.MaxAnyAllExpressionLimitExceeded, new object[] { validationSettings.MaxAnyAllExpressionDepth, "MaxAnyAllExpressionDepth" }));
			}
			this._currentAnyAllExpressionDepth++;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00019E51 File Offset: 0x00018051
		private void ExitLambda()
		{
			this._currentAnyAllExpressionDepth--;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00019E64 File Offset: 0x00018064
		private void IncrementNodeCount(ODataValidationSettings validationSettings)
		{
			if (this._currentNodeCount >= validationSettings.MaxNodeCount)
			{
				throw new ODataException(Error.Format(SRResources.MaxNodeLimitExceeded, new object[] { validationSettings.MaxNodeCount, "MaxNodeCount" }));
			}
			this._currentNodeCount++;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00019EBC File Offset: 0x000180BC
		private void ValidateCollectionNode(CollectionNode node, ODataValidationSettings settings)
		{
			QueryNodeKind kind = node.Kind;
			if (kind <= QueryNodeKind.CollectionResourceCast)
			{
				if (kind == QueryNodeKind.CollectionPropertyAccess)
				{
					CollectionPropertyAccessNode collectionPropertyAccessNode = node as CollectionPropertyAccessNode;
					this.ValidateCollectionPropertyAccessNode(collectionPropertyAccessNode, settings);
					return;
				}
				if (kind == QueryNodeKind.CollectionNavigationNode)
				{
					CollectionNavigationNode collectionNavigationNode = node as CollectionNavigationNode;
					this.ValidateNavigationPropertyNode(collectionNavigationNode.Source, collectionNavigationNode.NavigationProperty, settings);
					return;
				}
				if (kind == QueryNodeKind.CollectionResourceCast)
				{
					this.ValidateCollectionResourceCastNode(node as CollectionResourceCastNode, settings);
					return;
				}
			}
			else if (kind - QueryNodeKind.CollectionFunctionCall > 1 && kind != QueryNodeKind.CollectionOpenPropertyAccess)
			{
				if (kind == QueryNodeKind.CollectionComplexNode)
				{
					CollectionComplexNode collectionComplexNode = node as CollectionComplexNode;
					this.ValidateCollectionComplexNode(collectionComplexNode, settings);
					return;
				}
			}
			throw Error.NotSupported(SRResources.QueryNodeValidationNotSupported, new object[]
			{
				node.Kind,
				typeof(FilterQueryValidator).Name
			});
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00019F74 File Offset: 0x00018174
		private void ValidateSingleValueNode(SingleValueNode node, ODataValidationSettings settings)
		{
			switch (node.Kind)
			{
			case QueryNodeKind.Constant:
				this.ValidateConstantNode(node as ConstantNode, settings);
				return;
			case QueryNodeKind.Convert:
				this.ValidateConvertNode(node as ConvertNode, settings);
				return;
			case QueryNodeKind.NonResourceRangeVariableReference:
				this.ValidateRangeVariable((node as NonResourceRangeVariableReferenceNode).RangeVariable, settings);
				return;
			case QueryNodeKind.BinaryOperator:
				this.ValidateBinaryOperatorNode(node as BinaryOperatorNode, settings);
				return;
			case QueryNodeKind.UnaryOperator:
				this.ValidateUnaryOperatorNode(node as UnaryOperatorNode, settings);
				return;
			case QueryNodeKind.SingleValuePropertyAccess:
				this.ValidateSingleValuePropertyAccessNode(node as SingleValuePropertyAccessNode, settings);
				return;
			case QueryNodeKind.SingleValueFunctionCall:
				this.ValidateSingleValueFunctionCallNode(node as SingleValueFunctionCallNode, settings);
				return;
			case QueryNodeKind.Any:
				this.ValidateAnyNode(node as AnyNode, settings);
				return;
			case QueryNodeKind.SingleNavigationNode:
			{
				SingleNavigationNode singleNavigationNode = node as SingleNavigationNode;
				this.ValidateNavigationPropertyNode(singleNavigationNode.Source, singleNavigationNode.NavigationProperty, settings);
				return;
			}
			case QueryNodeKind.SingleValueOpenPropertyAccess:
			case QueryNodeKind.In:
				return;
			case QueryNodeKind.SingleResourceCast:
				this.ValidateSingleResourceCastNode(node as SingleResourceCastNode, settings);
				return;
			case QueryNodeKind.All:
				this.ValidateAllNode(node as AllNode, settings);
				return;
			case QueryNodeKind.ResourceRangeVariableReference:
				this.ValidateRangeVariable((node as ResourceRangeVariableReferenceNode).RangeVariable, settings);
				return;
			case QueryNodeKind.SingleResourceFunctionCall:
				this.ValidateSingleResourceFunctionCallNode((SingleResourceFunctionCallNode)node, settings);
				return;
			case QueryNodeKind.SingleComplexNode:
				this.ValidateSingleComplexNode(node as SingleComplexNode, settings);
				return;
			}
			throw Error.NotSupported(SRResources.QueryNodeValidationNotSupported, new object[]
			{
				node.Kind,
				typeof(FilterQueryValidator).Name
			});
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001A124 File Offset: 0x00018324
		private static void ValidateFunction(string functionName, ODataValidationSettings settings)
		{
			AllowedFunctions allowedFunctions = FilterQueryValidator.ToODataFunction(functionName);
			if ((settings.AllowedFunctions & allowedFunctions) != allowedFunctions)
			{
				throw new ODataException(Error.Format(SRResources.NotAllowedFunction, new object[] { functionName, "AllowedFunctions" }));
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001A168 File Offset: 0x00018368
		private static AllowedFunctions ToODataFunction(string functionName)
		{
			AllowedFunctions allowedFunctions = AllowedFunctions.None;
			if (functionName != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(functionName);
				if (num <= 2611590440U)
				{
					if (num <= 1042520049U)
					{
						if (num <= 740945997U)
						{
							if (num != 187241883U)
							{
								if (num != 321211332U)
								{
									if (num == 740945997U)
									{
										if (functionName == "any")
										{
											allowedFunctions = AllowedFunctions.Any;
										}
									}
								}
								else if (functionName == "all")
								{
									allowedFunctions = AllowedFunctions.All;
								}
							}
							else if (functionName == "fractionalseconds")
							{
								allowedFunctions = AllowedFunctions.FractionalSeconds;
							}
						}
						else if (num != 790464931U)
						{
							if (num != 954666857U)
							{
								if (num == 1042520049U)
								{
									if (functionName == "tolower")
									{
										allowedFunctions = AllowedFunctions.ToLower;
									}
								}
							}
							else if (functionName == "minute")
							{
								allowedFunctions = AllowedFunctions.Minute;
							}
						}
						else if (functionName == "endswith")
						{
							allowedFunctions = AllowedFunctions.EndsWith;
						}
					}
					else if (num <= 1825239352U)
					{
						if (num != 1326178875U)
						{
							if (num != 1564253156U)
							{
								if (num == 1825239352U)
								{
									if (functionName == "contains")
									{
										allowedFunctions = AllowedFunctions.Contains;
									}
								}
							}
							else if (functionName == "time")
							{
								allowedFunctions = AllowedFunctions.Time;
							}
						}
						else if (functionName == "round")
						{
							allowedFunctions = AllowedFunctions.Round;
						}
					}
					else if (num <= 2211460629U)
					{
						if (num != 1972648905U)
						{
							if (num == 2211460629U)
							{
								if (functionName == "length")
								{
									allowedFunctions = AllowedFunctions.Length;
								}
							}
						}
						else if (functionName == "trim")
						{
							allowedFunctions = AllowedFunctions.Trim;
						}
					}
					else if (num != 2479856990U)
					{
						if (num == 2611590440U)
						{
							if (functionName == "substring")
							{
								allowedFunctions = AllowedFunctions.Substring;
							}
						}
					}
					else if (functionName == "indexof")
					{
						allowedFunctions = AllowedFunctions.IndexOf;
					}
				}
				else if (num <= 3102149661U)
				{
					if (num <= 2885211357U)
					{
						if (num != 2659683000U)
						{
							if (num != 2854572110U)
							{
								if (num == 2885211357U)
								{
									if (functionName == "second")
									{
										allowedFunctions = AllowedFunctions.Second;
									}
								}
							}
							else if (functionName == "cast")
							{
								allowedFunctions = AllowedFunctions.Cast;
							}
						}
						else if (functionName == "isof")
						{
							allowedFunctions = AllowedFunctions.IsOf;
						}
					}
					else if (num != 2927578396U)
					{
						if (num != 3053661199U)
						{
							if (num == 3102149661U)
							{
								if (functionName == "floor")
								{
									allowedFunctions = AllowedFunctions.Floor;
								}
							}
						}
						else if (functionName == "hour")
						{
							allowedFunctions = AllowedFunctions.Hour;
						}
					}
					else if (functionName == "year")
					{
						allowedFunctions = AllowedFunctions.Year;
					}
				}
				else if (num <= 3598321157U)
				{
					if (num != 3118831744U)
					{
						if (num != 3564297305U)
						{
							if (num == 3598321157U)
							{
								if (functionName == "month")
								{
									allowedFunctions = AllowedFunctions.Month;
								}
							}
						}
						else if (functionName == "date")
						{
							allowedFunctions = AllowedFunctions.Date;
						}
					}
					else if (functionName == "ceiling")
					{
						allowedFunctions = AllowedFunctions.Ceiling;
					}
				}
				else if (num <= 3830391293U)
				{
					if (num != 3691983576U)
					{
						if (num == 3830391293U)
						{
							if (functionName == "day")
							{
								allowedFunctions = AllowedFunctions.Day;
							}
						}
					}
					else if (functionName == "toupper")
					{
						allowedFunctions = AllowedFunctions.ToUpper;
					}
				}
				else if (num != 4124019837U)
				{
					if (num == 4221853948U)
					{
						if (functionName == "startswith")
						{
							allowedFunctions = AllowedFunctions.StartsWith;
						}
					}
				}
				else if (functionName == "concat")
				{
					allowedFunctions = AllowedFunctions.Concat;
				}
			}
			return allowedFunctions;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001A60C File Offset: 0x0001880C
		private static AllowedLogicalOperators ToLogicalOperator(BinaryOperatorNode binaryNode)
		{
			AllowedLogicalOperators allowedLogicalOperators = AllowedLogicalOperators.None;
			switch (binaryNode.OperatorKind)
			{
			case BinaryOperatorKind.Or:
				allowedLogicalOperators = AllowedLogicalOperators.Or;
				break;
			case BinaryOperatorKind.And:
				allowedLogicalOperators = AllowedLogicalOperators.And;
				break;
			case BinaryOperatorKind.Equal:
				allowedLogicalOperators = AllowedLogicalOperators.Equal;
				break;
			case BinaryOperatorKind.NotEqual:
				allowedLogicalOperators = AllowedLogicalOperators.NotEqual;
				break;
			case BinaryOperatorKind.GreaterThan:
				allowedLogicalOperators = AllowedLogicalOperators.GreaterThan;
				break;
			case BinaryOperatorKind.GreaterThanOrEqual:
				allowedLogicalOperators = AllowedLogicalOperators.GreaterThanOrEqual;
				break;
			case BinaryOperatorKind.LessThan:
				allowedLogicalOperators = AllowedLogicalOperators.LessThan;
				break;
			case BinaryOperatorKind.LessThanOrEqual:
				allowedLogicalOperators = AllowedLogicalOperators.LessThanOrEqual;
				break;
			case BinaryOperatorKind.Has:
				allowedLogicalOperators = AllowedLogicalOperators.Has;
				break;
			}
			return allowedLogicalOperators;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001A690 File Offset: 0x00018890
		private static AllowedArithmeticOperators ToArithmeticOperator(BinaryOperatorNode binaryNode)
		{
			AllowedArithmeticOperators allowedArithmeticOperators = AllowedArithmeticOperators.None;
			switch (binaryNode.OperatorKind)
			{
			case BinaryOperatorKind.Add:
				allowedArithmeticOperators = AllowedArithmeticOperators.Add;
				break;
			case BinaryOperatorKind.Subtract:
				allowedArithmeticOperators = AllowedArithmeticOperators.Subtract;
				break;
			case BinaryOperatorKind.Multiply:
				allowedArithmeticOperators = AllowedArithmeticOperators.Multiply;
				break;
			case BinaryOperatorKind.Divide:
				allowedArithmeticOperators = AllowedArithmeticOperators.Divide;
				break;
			case BinaryOperatorKind.Modulo:
				allowedArithmeticOperators = AllowedArithmeticOperators.Modulo;
				break;
			}
			return allowedArithmeticOperators;
		}

		// Token: 0x04000233 RID: 563
		private int _currentAnyAllExpressionDepth;

		// Token: 0x04000234 RID: 564
		private int _currentNodeCount;

		// Token: 0x04000235 RID: 565
		private readonly DefaultQuerySettings _defaultQuerySettings;

		// Token: 0x04000236 RID: 566
		private IEdmProperty _property;

		// Token: 0x04000237 RID: 567
		private IEdmStructuredType _structuredType;

		// Token: 0x04000238 RID: 568
		private IEdmModel _model;
	}
}
