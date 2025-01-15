using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query.Validators
{
	// Token: 0x020000DA RID: 218
	public class SelectExpandQueryValidator
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x00018D4A File Offset: 0x00016F4A
		public SelectExpandQueryValidator(DefaultQuerySettings defaultQuerySettings)
		{
			this._defaultQuerySettings = defaultQuerySettings;
			this._filterQueryValidator = new FilterQueryValidator(this._defaultQuerySettings);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00018D6C File Offset: 0x00016F6C
		public virtual void Validate(SelectExpandQueryOption selectExpandQueryOption, ODataValidationSettings validationSettings)
		{
			if (selectExpandQueryOption == null)
			{
				throw Error.ArgumentNull("selectExpandQueryOption");
			}
			if (validationSettings == null)
			{
				throw Error.ArgumentNull("validationSettings");
			}
			this._orderByQueryValidator = new OrderByModelLimitationsValidator(selectExpandQueryOption.Context, this._defaultQuerySettings.EnableOrderBy);
			this._selectExpandQueryOption = selectExpandQueryOption;
			this.ValidateRestrictions(null, 0, selectExpandQueryOption.SelectExpandClause, null, validationSettings);
			if (validationSettings.MaxExpansionDepth > 0)
			{
				if (selectExpandQueryOption.LevelsMaxLiteralExpansionDepth < 0)
				{
					selectExpandQueryOption.LevelsMaxLiteralExpansionDepth = validationSettings.MaxExpansionDepth;
				}
				else if (selectExpandQueryOption.LevelsMaxLiteralExpansionDepth > validationSettings.MaxExpansionDepth)
				{
					throw new ODataException(Error.Format(SRResources.InvalidExpansionDepthValue, new object[] { "LevelsMaxLiteralExpansionDepth", "MaxExpansionDepth" }));
				}
				SelectExpandQueryValidator.ValidateDepth(selectExpandQueryOption.SelectExpandClause, validationSettings.MaxExpansionDepth);
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00018E35 File Offset: 0x00017035
		internal static SelectExpandQueryValidator GetSelectExpandQueryValidator(ODataQueryContext context)
		{
			if (context == null)
			{
				return new SelectExpandQueryValidator(new DefaultQuerySettings());
			}
			if (context.RequestContainer != null)
			{
				return ServiceProviderServiceExtensions.GetRequiredService<SelectExpandQueryValidator>(context.RequestContainer);
			}
			return new SelectExpandQueryValidator(context.DefaultQuerySettings);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00018E64 File Offset: 0x00017064
		private static void ValidateDepth(SelectExpandClause selectExpand, int maxDepth)
		{
			Stack<Tuple<int, SelectExpandClause>> stack = new Stack<Tuple<int, SelectExpandClause>>();
			stack.Push(Tuple.Create<int, SelectExpandClause>(0, selectExpand));
			while (stack.Count > 0)
			{
				Tuple<int, SelectExpandClause> tuple = stack.Pop();
				int currentDepth = tuple.Item1;
				ExpandedNavigationSelectItem[] array = tuple.Item2.SelectedItems.OfType<ExpandedNavigationSelectItem>().ToArray<ExpandedNavigationSelectItem>();
				if (array.Length != 0)
				{
					if (currentDepth != maxDepth)
					{
						goto IL_009F;
					}
					if (!array.Any((ExpandedNavigationSelectItem expandItem) => expandItem.LevelsOption == null || expandItem.LevelsOption.IsMaxLevel || expandItem.LevelsOption.Level != 0L))
					{
						goto IL_009F;
					}
					IL_00B4:
					throw new ODataException(Error.Format(SRResources.MaxExpandDepthExceeded, new object[] { maxDepth, "MaxExpansionDepth" }));
					IL_009F:
					if (array.Any((ExpandedNavigationSelectItem expandItem) => expandItem.LevelsOption != null && !expandItem.LevelsOption.IsMaxLevel && (expandItem.LevelsOption.Level > 2147483647L || expandItem.LevelsOption.Level + (long)currentDepth > (long)maxDepth)))
					{
						goto IL_00B4;
					}
				}
				foreach (ExpandedNavigationSelectItem expandedNavigationSelectItem in array)
				{
					int num = currentDepth + 1;
					if (expandedNavigationSelectItem.LevelsOption != null && !expandedNavigationSelectItem.LevelsOption.IsMaxLevel)
					{
						num = num + (int)expandedNavigationSelectItem.LevelsOption.Level - 1;
					}
					stack.Push(Tuple.Create<int, SelectExpandClause>(num, expandedNavigationSelectItem.SelectAndExpand));
				}
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00018FCC File Offset: 0x000171CC
		private void ValidateTopInExpand(IEdmProperty property, IEdmStructuredType structuredType, IEdmModel edmModel, long? topOption)
		{
			int num;
			if (topOption != null && EdmLibHelpers.IsTopLimitExceeded(property, structuredType, edmModel, (int)topOption.Value, this._defaultQuerySettings, out num))
			{
				throw new ODataException(Error.Format(SRResources.SkipTopLimitExceeded, new object[]
				{
					num,
					AllowedQueryOptions.Top,
					topOption.Value
				}));
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00019034 File Offset: 0x00017234
		private void ValidateCountInExpand(IEdmProperty property, IEdmStructuredType structuredType, IEdmModel edmModel, bool? countOption)
		{
			bool? flag = countOption;
			bool flag2 = true;
			if (((flag.GetValueOrDefault() == flag2) & (flag != null)) && EdmLibHelpers.IsNotCountable(property, structuredType, edmModel, this._defaultQuerySettings.EnableCount))
			{
				throw new InvalidOperationException(Error.Format(SRResources.NotCountablePropertyUsedForCount, new object[] { property.Name }));
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001908E File Offset: 0x0001728E
		private void ValidateOrderByInExpand(IEdmProperty property, IEdmStructuredType structuredType, OrderByClause orderByClause)
		{
			if (orderByClause != null)
			{
				this._orderByQueryValidator.TryValidate(property, structuredType, orderByClause, false);
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000190A3 File Offset: 0x000172A3
		private void ValidateFilterInExpand(IEdmProperty property, IEdmStructuredType structuredType, IEdmModel edmModel, FilterClause filterClause, ODataValidationSettings validationSettings)
		{
			if (filterClause != null)
			{
				this._filterQueryValidator.Validate(property, structuredType, filterClause, validationSettings, edmModel);
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000190BC File Offset: 0x000172BC
		private void ValidateSelectItem(SelectItem selectItem, IEdmProperty pathProperty, IEdmStructuredType pathStructuredType, IEdmModel edmModel)
		{
			PathSelectItem pathSelectItem = selectItem as PathSelectItem;
			if (pathSelectItem != null)
			{
				ODataPathSegment lastSegment = pathSelectItem.SelectedPath.LastSegment;
				NavigationPropertySegment navigationPropertySegment = lastSegment as NavigationPropertySegment;
				if (navigationPropertySegment != null)
				{
					IEdmNavigationProperty navigationProperty = navigationPropertySegment.NavigationProperty;
					if (EdmLibHelpers.IsNotNavigable(navigationProperty, edmModel))
					{
						throw new ODataException(Error.Format(SRResources.NotNavigablePropertyUsedInNavigation, new object[] { navigationProperty.Name }));
					}
				}
				else
				{
					PropertySegment propertySegment = lastSegment as PropertySegment;
					if (propertySegment != null && EdmLibHelpers.IsNotSelectable(propertySegment.Property, pathProperty, pathStructuredType, edmModel, this._defaultQuerySettings.EnableSelect))
					{
						throw new ODataException(Error.Format(SRResources.NotSelectablePropertyUsedInSelect, new object[] { propertySegment.Property.Name }));
					}
				}
			}
			else if (selectItem is WildcardSelectItem)
			{
				foreach (IEdmStructuralProperty edmStructuralProperty in pathStructuredType.StructuralProperties())
				{
					if (EdmLibHelpers.IsNotSelectable(edmStructuralProperty, pathProperty, pathStructuredType, edmModel, this._defaultQuerySettings.EnableSelect))
					{
						throw new ODataException(Error.Format(SRResources.NotSelectablePropertyUsedInSelect, new object[] { edmStructuralProperty.Name }));
					}
				}
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000191F4 File Offset: 0x000173F4
		private void ValidateLevelsOption(LevelsClause levelsClause, int depth, int currentDepth, IEdmModel edmModel, IEdmNavigationProperty property)
		{
			ExpandConfiguration expandConfiguration;
			if (EdmLibHelpers.IsExpandable(property.Name, property, property.ToEntityType(), edmModel, out expandConfiguration))
			{
				int maxDepth = expandConfiguration.MaxDepth;
				if (maxDepth > 0 && maxDepth < depth)
				{
					depth = maxDepth;
				}
				if ((depth == 0 && levelsClause.IsMaxLevel) || (long)depth < levelsClause.Level)
				{
					throw new ODataException(Error.Format(SRResources.MaxExpandDepthExceeded, new object[]
					{
						currentDepth + depth,
						"MaxExpansionDepth"
					}));
				}
			}
			else if (!this._defaultQuerySettings.EnableExpand || (expandConfiguration != null && expandConfiguration.ExpandType == SelectExpandType.Disabled))
			{
				throw new ODataException(Error.Format(SRResources.NotExpandablePropertyUsedInExpand, new object[] { property.Name }));
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000192A8 File Offset: 0x000174A8
		private void ValidateOtherQueryOptionInExpand(IEdmNavigationProperty property, IEdmModel edmModel, ExpandedNavigationSelectItem expandItem, ODataValidationSettings validationSettings)
		{
			this.ValidateTopInExpand(property, property.ToEntityType(), edmModel, expandItem.TopOption);
			this.ValidateCountInExpand(property, property.ToEntityType(), edmModel, expandItem.CountOption);
			this.ValidateOrderByInExpand(property, property.ToEntityType(), expandItem.OrderByOption);
			this.ValidateFilterInExpand(property, property.ToEntityType(), edmModel, expandItem.FilterOption, validationSettings);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00019308 File Offset: 0x00017508
		private void ValidateRestrictions(int? remainDepth, int currentDepth, SelectExpandClause selectExpandClause, IEdmNavigationProperty navigationProperty, ODataValidationSettings validationSettings)
		{
			IEdmModel model = this._selectExpandQueryOption.Context.Model;
			int? num = remainDepth;
			int? num2 = remainDepth;
			int num3 = 0;
			if ((num2.GetValueOrDefault() < num3) & (num2 != null))
			{
				throw new ODataException(Error.Format(SRResources.MaxExpandDepthExceeded, new object[]
				{
					currentDepth - 1,
					"MaxExpansionDepth"
				}));
			}
			IEdmProperty edmProperty;
			IEdmStructuredType edmStructuredType;
			if (navigationProperty == null)
			{
				edmProperty = this._selectExpandQueryOption.Context.TargetProperty;
				edmStructuredType = this._selectExpandQueryOption.Context.TargetStructuredType;
			}
			else
			{
				edmProperty = navigationProperty;
				edmStructuredType = navigationProperty.ToEntityType();
			}
			foreach (SelectItem selectItem in selectExpandClause.SelectedItems)
			{
				ExpandedNavigationSelectItem expandedNavigationSelectItem = selectItem as ExpandedNavigationSelectItem;
				if (expandedNavigationSelectItem != null)
				{
					IEdmNavigationProperty navigationProperty2 = ((NavigationPropertySegment)expandedNavigationSelectItem.PathToNavigationProperty.LastSegment).NavigationProperty;
					if (EdmLibHelpers.IsNotExpandable(navigationProperty2, model))
					{
						throw new ODataException(Error.Format(SRResources.NotExpandablePropertyUsedInExpand, new object[] { navigationProperty2.Name }));
					}
					if (model != null)
					{
						this.ValidateOtherQueryOptionInExpand(navigationProperty2, model, expandedNavigationSelectItem, validationSettings);
						ExpandConfiguration expandConfiguration;
						bool flag = EdmLibHelpers.IsExpandable(navigationProperty2.Name, edmProperty, edmStructuredType, model, out expandConfiguration);
						if (flag)
						{
							int maxDepth = expandConfiguration.MaxDepth;
							if (maxDepth > 0)
							{
								if (remainDepth != null)
								{
									int num4 = maxDepth;
									num2 = remainDepth;
									if (!((num4 < num2.GetValueOrDefault()) & (num2 != null)))
									{
										goto IL_019A;
									}
								}
								remainDepth = new int?(maxDepth);
							}
						}
						else if (!flag && (!this._defaultQuerySettings.EnableExpand || (expandConfiguration != null && expandConfiguration.ExpandType == SelectExpandType.Disabled)))
						{
							throw new ODataException(Error.Format(SRResources.NotExpandablePropertyUsedInExpand, new object[] { navigationProperty2.Name }));
						}
					}
					IL_019A:
					if (remainDepth != null)
					{
						remainDepth--;
						if (expandedNavigationSelectItem.LevelsOption != null)
						{
							this.ValidateLevelsOption(expandedNavigationSelectItem.LevelsOption, remainDepth.Value, currentDepth + 1, model, navigationProperty2);
						}
					}
					this.ValidateRestrictions(remainDepth, currentDepth + 1, expandedNavigationSelectItem.SelectAndExpand, navigationProperty2, validationSettings);
					remainDepth = num;
				}
				this.ValidateSelectItem(selectItem, edmProperty, edmStructuredType, model);
			}
		}

		// Token: 0x0400022F RID: 559
		private readonly DefaultQuerySettings _defaultQuerySettings;

		// Token: 0x04000230 RID: 560
		private readonly FilterQueryValidator _filterQueryValidator;

		// Token: 0x04000231 RID: 561
		private OrderByModelLimitationsValidator _orderByQueryValidator;

		// Token: 0x04000232 RID: 562
		private SelectExpandQueryOption _selectExpandQueryOption;
	}
}
