using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000F0 RID: 240
	internal class SelectExpandBinder
	{
		// Token: 0x0600081B RID: 2075 RVA: 0x0001EB9F File Offset: 0x0001CD9F
		public SelectExpandBinder(ODataQuerySettings settings, ODataQueryContext context)
		{
			this._context = context;
			this._model = this._context.Model;
			this._modelID = ModelContainer.GetModelID(this._model);
			this._settings = settings;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001EBD7 File Offset: 0x0001CDD7
		public static IQueryable Bind(IQueryable queryable, ODataQuerySettings settings, SelectExpandQueryOption selectExpandQuery)
		{
			return new SelectExpandBinder(settings, selectExpandQuery.Context).Bind(queryable, selectExpandQuery);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001EBEC File Offset: 0x0001CDEC
		public static object Bind(object entity, ODataQuerySettings settings, SelectExpandQueryOption selectExpandQuery)
		{
			return new SelectExpandBinder(settings, selectExpandQuery.Context).Bind(entity, selectExpandQuery);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001EC01 File Offset: 0x0001CE01
		private object Bind(object entity, SelectExpandQueryOption selectExpandQuery)
		{
			return this.GetProjectionLambda(selectExpandQuery).Compile().DynamicInvoke(new object[] { entity });
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001EC20 File Offset: 0x0001CE20
		private IQueryable Bind(IQueryable queryable, SelectExpandQueryOption selectExpandQuery)
		{
			Type elementClrType = selectExpandQuery.Context.ElementClrType;
			LambdaExpression projectionLambda = this.GetProjectionLambda(selectExpandQuery);
			return ExpressionHelperMethods.QueryableSelectGeneric.MakeGenericMethod(new Type[]
			{
				elementClrType,
				projectionLambda.Body.Type
			}).Invoke(null, new object[] { queryable, projectionLambda }) as IQueryable;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001EC7C File Offset: 0x0001CE7C
		private LambdaExpression GetProjectionLambda(SelectExpandQueryOption selectExpandQuery)
		{
			Type elementClrType = selectExpandQuery.Context.ElementClrType;
			IEdmNavigationSource navigationSource = selectExpandQuery.Context.NavigationSource;
			ParameterExpression parameterExpression = Expression.Parameter(elementClrType);
			return Expression.Lambda(this.ProjectElement(parameterExpression, selectExpandQuery.SelectExpandClause, this._context.ElementType as IEdmStructuredType, navigationSource), new ParameterExpression[] { parameterExpression });
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001ECD4 File Offset: 0x0001CED4
		internal Expression ProjectAsWrapper(Expression source, SelectExpandClause selectExpandClause, IEdmStructuredType structuredType, IEdmNavigationSource navigationSource, OrderByClause orderByClause = null, long? topOption = null, long? skipOption = null, int? modelBoundPageSize = null)
		{
			Type type;
			if (TypeHelper.IsCollection(source.Type, out type))
			{
				return this.ProjectCollection(source, type, selectExpandClause, structuredType, navigationSource, orderByClause, topOption, skipOption, modelBoundPageSize);
			}
			return this.ProjectElement(source, selectExpandClause, structuredType, navigationSource);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001ED10 File Offset: 0x0001CF10
		internal Expression CreatePropertyNameExpression(IEdmStructuredType elementType, IEdmProperty property, Expression source)
		{
			IEdmStructuredType declaringType = property.DeclaringType;
			if (elementType != declaringType)
			{
				Type clrType = EdmLibHelpers.GetClrType(elementType, this._model);
				Type clrType2 = EdmLibHelpers.GetClrType(declaringType, this._model);
				if (clrType2 == null)
				{
					throw new ODataException(Error.Format(SRResources.MappingDoesNotContainResourceType, new object[] { declaringType.FullTypeName() }));
				}
				if (!clrType2.IsAssignableFrom(clrType))
				{
					return Expression.Condition(Expression.TypeIs(source, clrType2), Expression.Constant(property.Name), Expression.Constant(null, typeof(string)));
				}
			}
			return Expression.Constant(property.Name);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001EDA8 File Offset: 0x0001CFA8
		internal Expression CreatePropertyValueExpression(IEdmStructuredType elementType, IEdmProperty property, Expression source, FilterClause filterClause)
		{
			if (elementType != property.DeclaringType)
			{
				Type clrType = EdmLibHelpers.GetClrType(property.DeclaringType, this._model);
				if (clrType == null)
				{
					throw new ODataException(Error.Format(SRResources.MappingDoesNotContainResourceType, new object[] { property.DeclaringType.FullTypeName() }));
				}
				source = Expression.TypeAs(source, clrType);
			}
			string clrPropertyName = EdmLibHelpers.GetClrPropertyName(property, this._model);
			PropertyInfo property2 = source.Type.GetProperty(clrPropertyName);
			Expression expression = Expression.Property(source, property2);
			Type type = TypeHelper.ToNullable(expression.Type);
			Expression expression2 = ExpressionHelpers.ToNullable(expression);
			if (filterClause != null)
			{
				bool flag = property.Type.IsCollection();
				IEdmTypeReference edmTypeReference = (flag ? property.Type.AsCollection().ElementType() : property.Type);
				Type clrType2 = EdmLibHelpers.GetClrType(edmTypeReference, this._model);
				if (clrType2 == null)
				{
					throw new ODataException(Error.Format(SRResources.MappingDoesNotContainResourceType, new object[] { edmTypeReference.FullName() }));
				}
				Expression expression3 = expression2;
				ODataQuerySettings odataQuerySettings = new ODataQuerySettings
				{
					HandleNullPropagation = HandleNullPropagationOption.True
				};
				if (flag)
				{
					Expression expression4 = expression2;
					Expression expression5 = FilterBinder.Bind(null, filterClause, clrType2, this._context, odataQuerySettings);
					expression3 = Expression.Call(ExpressionHelperMethods.EnumerableWhereGeneric.MakeGenericMethod(new Type[] { clrType2 }), expression4, expression5);
					type = expression3.Type;
				}
				else if (this._settings.HandleReferenceNavigationPropertyExpandFilter)
				{
					LambdaExpression lambdaExpression = FilterBinder.Bind(null, filterClause, clrType2, this._context, odataQuerySettings) as LambdaExpression;
					if (lambdaExpression == null)
					{
						throw new ODataException(Error.Format(SRResources.ExpandFilterExpressionNotLambdaExpression, new object[] { property.Name, "LambdaExpression" }));
					}
					expression3 = Expression.Condition(new SelectExpandBinder.ReferenceNavigationPropertyExpandFilterVisitor(lambdaExpression.Parameters.First<ParameterExpression>(), expression2).Visit(lambdaExpression.Body), expression2, Expression.Constant(null, type));
				}
				if (this._settings.HandleNullPropagation == HandleNullPropagationOption.True)
				{
					expression2 = Expression.Condition(Expression.Equal(expression2, Expression.Constant(null)), Expression.Constant(null, type), expression3);
				}
				else
				{
					expression2 = expression3;
				}
			}
			if (this._settings.HandleNullPropagation == HandleNullPropagationOption.True)
			{
				expression = Expression.Condition(Expression.Equal(source, Expression.Constant(null)), Expression.Constant(null, type), expression2);
			}
			else
			{
				expression = expression2;
			}
			return expression;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001EFE8 File Offset: 0x0001D1E8
		internal Expression ProjectElement(Expression source, SelectExpandClause selectExpandClause, IEdmStructuredType structuredType, IEdmNavigationSource navigationSource)
		{
			if (structuredType == null)
			{
				return source;
			}
			Type type = source.Type;
			Type type2 = typeof(SelectExpandWrapper<>).MakeGenericType(new Type[] { type });
			List<MemberAssignment> list = new List<MemberAssignment>();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			PropertyInfo propertyInfo = type2.GetProperty("ModelID");
			Expression expression = (this._settings.EnableConstantParameterization ? LinqParameterContainer.Parameterize(typeof(string), this._modelID) : Expression.Constant(this._modelID));
			list.Add(Expression.Bind(propertyInfo, expression));
			if (SelectExpandBinder.IsSelectAll(selectExpandClause))
			{
				propertyInfo = type2.GetProperty("Instance");
				list.Add(Expression.Bind(propertyInfo, source));
				propertyInfo = type2.GetProperty("UseInstanceForProperties");
				list.Add(Expression.Bind(propertyInfo, Expression.Constant(true)));
				flag = true;
			}
			else
			{
				Expression expression2 = SelectExpandBinder.CreateTypeNameExpression(source, structuredType, this._model);
				if (expression2 != null)
				{
					flag2 = true;
					propertyInfo = type2.GetProperty("InstanceType");
					list.Add(Expression.Bind(propertyInfo, expression2));
				}
			}
			if (selectExpandClause != null)
			{
				IDictionary<IEdmStructuralProperty, PathSelectItem> dictionary;
				IDictionary<IEdmNavigationProperty, ExpandedReferenceSelectItem> dictionary2;
				ISet<IEdmStructuralProperty> set;
				bool flag4 = SelectExpandBinder.GetSelectExpandProperties(this._model, structuredType, navigationSource, selectExpandClause, out dictionary, out dictionary2, out set) || SelectExpandBinder.IsSelectAllOnOpenType(selectExpandClause, structuredType);
				if (dictionary2 != null || dictionary != null || set != null || flag4)
				{
					Expression expression3 = this.BuildPropertyContainer(source, structuredType, dictionary2, dictionary, set, flag4);
					if (expression3 != null)
					{
						propertyInfo = type2.GetProperty("Container");
						list.Add(Expression.Bind(propertyInfo, expression3));
						flag3 = true;
					}
				}
			}
			type2 = SelectExpandBinder.GetWrapperGenericType(flag, flag2, flag3).MakeGenericType(new Type[] { type });
			return Expression.MemberInit(Expression.New(type2), list);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001F184 File Offset: 0x0001D384
		internal static bool GetSelectExpandProperties(IEdmModel model, IEdmStructuredType structuredType, IEdmNavigationSource navigationSource, SelectExpandClause selectExpandClause, out IDictionary<IEdmStructuralProperty, PathSelectItem> propertiesToInclude, out IDictionary<IEdmNavigationProperty, ExpandedReferenceSelectItem> propertiesToExpand, out ISet<IEdmStructuralProperty> autoSelectedProperties)
		{
			propertiesToInclude = null;
			propertiesToExpand = null;
			autoSelectedProperties = null;
			bool flag = false;
			Dictionary<IEdmStructuralProperty, SelectExpandIncludedProperty> dictionary = new Dictionary<IEdmStructuralProperty, SelectExpandIncludedProperty>();
			foreach (SelectItem selectItem in selectExpandClause.SelectedItems)
			{
				ExpandedReferenceSelectItem expandedReferenceSelectItem = selectItem as ExpandedReferenceSelectItem;
				if (expandedReferenceSelectItem != null)
				{
					SelectExpandBinder.ProcessExpandedItem(expandedReferenceSelectItem, navigationSource, dictionary, ref propertiesToExpand);
				}
				else
				{
					PathSelectItem pathSelectItem = selectItem as PathSelectItem;
					if (pathSelectItem != null && SelectExpandBinder.ProcessSelectedItem(pathSelectItem, navigationSource, dictionary))
					{
						flag = true;
					}
				}
			}
			if (!SelectExpandBinder.IsSelectAll(selectExpandClause))
			{
				IEdmEntityType edmEntityType = structuredType as IEdmEntityType;
				if (edmEntityType != null)
				{
					foreach (IEdmStructuralProperty edmStructuralProperty in edmEntityType.Key())
					{
						if (!dictionary.Keys.Contains(edmStructuralProperty))
						{
							if (autoSelectedProperties == null)
							{
								autoSelectedProperties = new HashSet<IEdmStructuralProperty>();
							}
							autoSelectedProperties.Add(edmStructuralProperty);
						}
					}
				}
				if (navigationSource != null && model != null)
				{
					using (IEnumerator<IEdmStructuralProperty> enumerator2 = model.GetConcurrencyProperties(navigationSource).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							IEdmStructuralProperty concurrencyProperty = enumerator2.Current;
							if (structuredType.Properties().Any((IEdmProperty p) => p == concurrencyProperty) && !dictionary.Keys.Contains(concurrencyProperty))
							{
								if (autoSelectedProperties == null)
								{
									autoSelectedProperties = new HashSet<IEdmStructuralProperty>();
								}
								autoSelectedProperties.Add(concurrencyProperty);
							}
						}
					}
				}
			}
			if (dictionary.Any<KeyValuePair<IEdmStructuralProperty, SelectExpandIncludedProperty>>())
			{
				propertiesToInclude = new Dictionary<IEdmStructuralProperty, PathSelectItem>();
				foreach (KeyValuePair<IEdmStructuralProperty, SelectExpandIncludedProperty> keyValuePair in dictionary)
				{
					propertiesToInclude[keyValuePair.Key] = ((keyValuePair.Value == null) ? null : keyValuePair.Value.ToPathSelectItem());
				}
			}
			return flag;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001F39C File Offset: 0x0001D59C
		private static void ProcessExpandedItem(ExpandedReferenceSelectItem expandedItem, IEdmNavigationSource navigationSource, IDictionary<IEdmStructuralProperty, SelectExpandIncludedProperty> currentLevelPropertiesInclude, ref IDictionary<IEdmNavigationProperty, ExpandedReferenceSelectItem> propertiesToExpand)
		{
			IList<ODataPathSegment> list;
			ODataPathSegment firstNonTypeCastSegment = expandedItem.PathToNavigationProperty.GetFirstNonTypeCastSegment(out list);
			PropertySegment propertySegment = firstNonTypeCastSegment as PropertySegment;
			if (propertySegment != null)
			{
				SelectExpandIncludedProperty selectExpandIncludedProperty;
				if (!currentLevelPropertiesInclude.TryGetValue(propertySegment.Property, out selectExpandIncludedProperty))
				{
					selectExpandIncludedProperty = new SelectExpandIncludedProperty(propertySegment, navigationSource);
					currentLevelPropertiesInclude[propertySegment.Property] = selectExpandIncludedProperty;
				}
				selectExpandIncludedProperty.AddSubExpandItem(list, expandedItem);
				return;
			}
			NavigationPropertySegment navigationPropertySegment = firstNonTypeCastSegment as NavigationPropertySegment;
			if (propertiesToExpand == null)
			{
				propertiesToExpand = new Dictionary<IEdmNavigationProperty, ExpandedReferenceSelectItem>();
			}
			propertiesToExpand[navigationPropertySegment.NavigationProperty] = expandedItem;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001F414 File Offset: 0x0001D614
		private static bool ProcessSelectedItem(PathSelectItem pathSelectItem, IEdmNavigationSource navigationSource, IDictionary<IEdmStructuralProperty, SelectExpandIncludedProperty> currentLevelPropertiesInclude)
		{
			IList<ODataPathSegment> list;
			ODataPathSegment firstNonTypeCastSegment = pathSelectItem.SelectedPath.GetFirstNonTypeCastSegment(out list);
			PropertySegment propertySegment = firstNonTypeCastSegment as PropertySegment;
			if (propertySegment != null)
			{
				SelectExpandIncludedProperty selectExpandIncludedProperty;
				if (!currentLevelPropertiesInclude.TryGetValue(propertySegment.Property, out selectExpandIncludedProperty))
				{
					selectExpandIncludedProperty = new SelectExpandIncludedProperty(propertySegment, navigationSource);
					currentLevelPropertiesInclude[propertySegment.Property] = selectExpandIncludedProperty;
				}
				selectExpandIncludedProperty.AddSubSelectItem(list, pathSelectItem);
			}
			else if (firstNonTypeCastSegment is DynamicPathSegment)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001F473 File Offset: 0x0001D673
		private static bool IsSelectAllOnOpenType(SelectExpandClause selectExpandClause, IEdmStructuredType structuredType)
		{
			return structuredType != null && structuredType.IsOpen && SelectExpandBinder.IsSelectAll(selectExpandClause);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001F490 File Offset: 0x0001D690
		private Expression CreateTotalCountExpression(Expression source, bool? countOption)
		{
			Expression expression = Expression.Constant(null, typeof(long?));
			if (countOption == null || !countOption.Value)
			{
				return expression;
			}
			Type type;
			if (!TypeHelper.IsCollection(source.Type, out type))
			{
				return expression;
			}
			MethodInfo methodInfo;
			if (typeof(IQueryable).IsAssignableFrom(source.Type))
			{
				methodInfo = ExpressionHelperMethods.QueryableCountGeneric.MakeGenericMethod(new Type[] { type });
			}
			else
			{
				methodInfo = ExpressionHelperMethods.EnumerableCountGeneric.MakeGenericMethod(new Type[] { type });
			}
			expression = Expression.Call(null, methodInfo, new Expression[] { source });
			if (this._settings.HandleNullPropagation == HandleNullPropagationOption.True)
			{
				return Expression.Condition(Expression.Equal(source, Expression.Constant(null)), Expression.Constant(null, typeof(long?)), ExpressionHelpers.ToNullable(expression));
			}
			return expression;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001F560 File Offset: 0x0001D760
		private Expression BuildPropertyContainer(Expression source, IEdmStructuredType structuredType, IDictionary<IEdmNavigationProperty, ExpandedReferenceSelectItem> propertiesToExpand, IDictionary<IEdmStructuralProperty, PathSelectItem> propertiesToInclude, ISet<IEdmStructuralProperty> autoSelectedProperties, bool isSelectingOpenTypeSegments)
		{
			IList<NamedPropertyExpression> list = new List<NamedPropertyExpression>();
			if (propertiesToExpand != null)
			{
				foreach (KeyValuePair<IEdmNavigationProperty, ExpandedReferenceSelectItem> keyValuePair in propertiesToExpand)
				{
					this.BuildExpandedProperty(source, structuredType, keyValuePair.Key, keyValuePair.Value, list);
				}
			}
			if (propertiesToInclude != null)
			{
				foreach (KeyValuePair<IEdmStructuralProperty, PathSelectItem> keyValuePair2 in propertiesToInclude)
				{
					this.BuildSelectedProperty(source, structuredType, keyValuePair2.Key, keyValuePair2.Value, list);
				}
			}
			if (autoSelectedProperties != null)
			{
				foreach (IEdmStructuralProperty edmStructuralProperty in autoSelectedProperties)
				{
					Expression expression = this.CreatePropertyNameExpression(structuredType, edmStructuralProperty, source);
					Expression expression2 = this.CreatePropertyValueExpression(structuredType, edmStructuralProperty, source, null);
					list.Add(new NamedPropertyExpression(expression, expression2)
					{
						AutoSelected = true
					});
				}
			}
			if (isSelectingOpenTypeSegments)
			{
				this.BuildDynamicProperty(source, structuredType, list);
			}
			return PropertyContainer.CreatePropertyContainer(list);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001F68C File Offset: 0x0001D88C
		internal void BuildExpandedProperty(Expression source, IEdmStructuredType structuredType, IEdmNavigationProperty navigationProperty, ExpandedReferenceSelectItem expandedItem, IList<NamedPropertyExpression> includedProperties)
		{
			IEdmEntityType edmEntityType = navigationProperty.ToEntityType();
			ModelBoundQuerySettings modelBoundQuerySettings = EdmLibHelpers.GetModelBoundQuerySettings(navigationProperty, edmEntityType, this._model, null);
			Expression expression = this.CreatePropertyNameExpression(structuredType, navigationProperty, source);
			Expression expression2 = this.CreatePropertyValueExpression(structuredType, navigationProperty, source, expandedItem.FilterOption);
			SelectExpandClause orCreateSelectExpandClause = SelectExpandBinder.GetOrCreateSelectExpandClause(navigationProperty, expandedItem);
			Expression nullCheckExpression = this.GetNullCheckExpression(navigationProperty, expression2, orCreateSelectExpandClause);
			Expression expression3 = this.CreateTotalCountExpression(expression2, expandedItem.CountOption);
			int? num = ((modelBoundQuerySettings == null) ? null : modelBoundQuerySettings.PageSize);
			expression2 = this.ProjectAsWrapper(expression2, orCreateSelectExpandClause, edmEntityType, expandedItem.NavigationSource, expandedItem.OrderByOption, expandedItem.TopOption, expandedItem.SkipOption, num);
			NamedPropertyExpression namedPropertyExpression = new NamedPropertyExpression(expression, expression2);
			if (orCreateSelectExpandClause != null)
			{
				if (!navigationProperty.Type.IsCollection())
				{
					namedPropertyExpression.NullCheck = nullCheckExpression;
				}
				else if (this._settings.PageSize != null)
				{
					namedPropertyExpression.PageSize = new int?(this._settings.PageSize.Value);
				}
				else if (modelBoundQuerySettings != null && modelBoundQuerySettings.PageSize != null)
				{
					namedPropertyExpression.PageSize = new int?(modelBoundQuerySettings.PageSize.Value);
				}
				namedPropertyExpression.TotalCount = expression3;
				namedPropertyExpression.CountOption = expandedItem.CountOption;
			}
			includedProperties.Add(namedPropertyExpression);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001F7DC File Offset: 0x0001D9DC
		internal void BuildSelectedProperty(Expression source, IEdmStructuredType structuredType, IEdmStructuralProperty structuralProperty, PathSelectItem pathSelectItem, IList<NamedPropertyExpression> includedProperties)
		{
			Expression expression = this.CreatePropertyNameExpression(structuredType, structuralProperty, source);
			Expression expression2;
			if (pathSelectItem == null)
			{
				expression2 = this.CreatePropertyValueExpression(structuredType, structuralProperty, source, null);
				includedProperties.Add(new NamedPropertyExpression(expression, expression2));
				return;
			}
			SelectExpandClause selectAndExpand = pathSelectItem.SelectAndExpand;
			expression2 = this.CreatePropertyValueExpression(structuredType, structuralProperty, source, pathSelectItem.FilterOption);
			Type type = expression2.Type;
			if (type == typeof(char[]) || type == typeof(byte[]))
			{
				includedProperties.Add(new NamedPropertyExpression(expression, expression2));
				return;
			}
			Expression nullCheckExpression = SelectExpandBinder.GetNullCheckExpression(structuralProperty, expression2, selectAndExpand);
			Expression expression3 = this.CreateTotalCountExpression(expression2, pathSelectItem.CountOption);
			IEdmStructuredType edmStructuredType = structuralProperty.Type.ToStructuredType();
			ModelBoundQuerySettings modelBoundQuerySettings = null;
			if (edmStructuredType != null)
			{
				modelBoundQuerySettings = EdmLibHelpers.GetModelBoundQuerySettings(structuralProperty, edmStructuredType, this._context.Model, null);
			}
			int? num = ((modelBoundQuerySettings == null) ? null : modelBoundQuerySettings.PageSize);
			expression2 = this.ProjectAsWrapper(expression2, selectAndExpand, structuralProperty.Type.ToStructuredType(), pathSelectItem.NavigationSource, pathSelectItem.OrderByOption, pathSelectItem.TopOption, pathSelectItem.SkipOption, num);
			NamedPropertyExpression namedPropertyExpression = new NamedPropertyExpression(expression, expression2);
			if (selectAndExpand != null)
			{
				if (!structuralProperty.Type.IsCollection())
				{
					namedPropertyExpression.NullCheck = nullCheckExpression;
				}
				else if (this._settings.PageSize != null)
				{
					namedPropertyExpression.PageSize = new int?(this._settings.PageSize.Value);
				}
				else if (modelBoundQuerySettings != null && modelBoundQuerySettings.PageSize != null)
				{
					namedPropertyExpression.PageSize = new int?(modelBoundQuerySettings.PageSize.Value);
				}
				namedPropertyExpression.TotalCount = expression3;
				namedPropertyExpression.CountOption = pathSelectItem.CountOption;
			}
			includedProperties.Add(namedPropertyExpression);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001F9A4 File Offset: 0x0001DBA4
		internal void BuildDynamicProperty(Expression source, IEdmStructuredType structuredType, IList<NamedPropertyExpression> includedProperties)
		{
			PropertyInfo dynamicPropertyDictionary = EdmLibHelpers.GetDynamicPropertyDictionary(structuredType, this._model);
			if (dynamicPropertyDictionary != null)
			{
				Expression expression = Expression.Constant(dynamicPropertyDictionary.Name);
				Expression expression2 = Expression.Property(source, dynamicPropertyDictionary.Name);
				Expression expression3 = ExpressionHelpers.ToNullable(expression2);
				if (this._settings.HandleNullPropagation == HandleNullPropagationOption.True)
				{
					expression2 = Expression.Condition(Expression.Equal(source, Expression.Constant(null)), Expression.Constant(null, TypeHelper.ToNullable(expression2.Type)), expression3);
				}
				else
				{
					expression2 = expression3;
				}
				includedProperties.Add(new NamedPropertyExpression(expression, expression2));
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001FA2C File Offset: 0x0001DC2C
		private static SelectExpandClause GetOrCreateSelectExpandClause(IEdmNavigationProperty navigationProperty, ExpandedReferenceSelectItem expandedItem)
		{
			ExpandedNavigationSelectItem expandedNavigationSelectItem = expandedItem as ExpandedNavigationSelectItem;
			if (expandedNavigationSelectItem != null)
			{
				return expandedNavigationSelectItem.SelectAndExpand;
			}
			IList<SelectItem> list = new List<SelectItem>();
			foreach (IEdmStructuralProperty edmStructuralProperty in navigationProperty.ToEntityType().Key())
			{
				list.Add(new PathSelectItem(new ODataSelectPath(new ODataPathSegment[]
				{
					new PropertySegment(edmStructuralProperty)
				})));
			}
			return new SelectExpandClause(list, false);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001FAB4 File Offset: 0x0001DCB4
		private Expression AddOrderByQueryForSource(Expression source, OrderByClause orderbyClause, Type elementType)
		{
			if (orderbyClause != null)
			{
				ODataQuerySettings odataQuerySettings = new ODataQuerySettings
				{
					HandleNullPropagation = HandleNullPropagationOption.True
				};
				LambdaExpression lambdaExpression = FilterBinder.Bind(null, orderbyClause, elementType, this._context, odataQuerySettings);
				source = ExpressionHelpers.OrderBy(source, lambdaExpression, elementType, orderbyClause.Direction, false);
			}
			return source;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001FAF3 File Offset: 0x0001DCF3
		private static Expression GetNullCheckExpression(IEdmStructuralProperty propertyToInclude, Expression propertyValue, SelectExpandClause projection)
		{
			if (projection == null || propertyToInclude.Type.IsCollection())
			{
				return null;
			}
			if (SelectExpandBinder.IsSelectAll(projection) && propertyToInclude.Type.IsComplex())
			{
				return Expression.Equal(propertyValue, Expression.Constant(null));
			}
			return null;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001FB2C File Offset: 0x0001DD2C
		private Expression GetNullCheckExpression(IEdmNavigationProperty propertyToExpand, Expression propertyValue, SelectExpandClause projection)
		{
			if (projection == null || propertyToExpand.Type.IsCollection())
			{
				return null;
			}
			if (SelectExpandBinder.IsSelectAll(projection) || !propertyToExpand.ToEntityType().Key().Any<IEdmStructuralProperty>())
			{
				return Expression.Equal(propertyValue, Expression.Constant(null));
			}
			Expression expression = null;
			foreach (IEdmStructuralProperty edmStructuralProperty in propertyToExpand.ToEntityType().Key())
			{
				Expression expression2 = this.CreatePropertyValueExpression(propertyToExpand.ToEntityType(), edmStructuralProperty, propertyValue, null);
				BinaryExpression binaryExpression = Expression.Equal(expression2, Expression.Constant(null, expression2.Type));
				expression = ((expression == null) ? binaryExpression : Expression.And(expression, binaryExpression));
			}
			return expression;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001FBE8 File Offset: 0x0001DDE8
		private Expression ProjectCollection(Expression source, Type elementType, SelectExpandClause selectExpandClause, IEdmStructuredType structuredType, IEdmNavigationSource navigationSource, OrderByClause orderByClause, long? topOption, long? skipOption, int? modelBoundPageSize)
		{
			ParameterExpression parameterExpression = Expression.Parameter(elementType);
			Expression expression;
			if (structuredType != null)
			{
				expression = this.ProjectElement(parameterExpression, selectExpandClause, structuredType, navigationSource);
			}
			else
			{
				expression = parameterExpression;
			}
			LambdaExpression lambdaExpression = Expression.Lambda(expression, new ParameterExpression[] { parameterExpression });
			if (orderByClause != null)
			{
				source = this.AddOrderByQueryForSource(source, orderByClause, elementType);
			}
			bool flag = topOption != null && topOption != null;
			bool flag2 = skipOption != null && skipOption != null;
			IEdmEntityType edmEntityType = structuredType as IEdmEntityType;
			if (edmEntityType != null && (this._settings.PageSize != null || modelBoundPageSize != null || flag || flag2))
			{
				IEnumerable<IEdmStructuralProperty> enumerable2;
				if (!edmEntityType.Key().Any<IEdmStructuralProperty>())
				{
					IEnumerable<IEdmStructuralProperty> enumerable = from property in edmEntityType.StructuralProperties()
						where property.Type.IsPrimitive() && !property.Type.IsStream()
						orderby property.Name
						select property;
					enumerable2 = enumerable;
				}
				else
				{
					enumerable2 = edmEntityType.Key();
				}
				IEnumerable<IEdmStructuralProperty> enumerable3 = enumerable2;
				if (orderByClause == null)
				{
					bool flag3 = false;
					foreach (IEdmStructuralProperty edmStructuralProperty in enumerable3)
					{
						source = ExpressionHelpers.OrderByPropertyExpression(source, edmStructuralProperty.Name, elementType, flag3);
						if (!flag3)
						{
							flag3 = true;
						}
					}
				}
			}
			if (flag2)
			{
				source = ExpressionHelpers.Skip(source, (int)skipOption.Value, elementType, this._settings.EnableConstantParameterization);
			}
			if (flag)
			{
				source = ExpressionHelpers.Take(source, (int)topOption.Value, elementType, this._settings.EnableConstantParameterization);
			}
			if ((this._settings.PageSize != null || modelBoundPageSize != null || flag || flag2) && !this._settings.EnableCorrelatedSubqueryBuffering)
			{
				if (this._settings.PageSize != null)
				{
					source = ExpressionHelpers.Take(source, this._settings.PageSize.Value + 1, elementType, this._settings.EnableConstantParameterization);
				}
				else if (this._settings.ModelBoundPageSize != null)
				{
					source = ExpressionHelpers.Take(source, modelBoundPageSize.Value + 1, elementType, this._settings.EnableConstantParameterization);
				}
			}
			Expression expression2 = Expression.Call(SelectExpandBinder.GetSelectMethod(elementType, expression.Type), source, lambdaExpression);
			if (this._settings.EnableCorrelatedSubqueryBuffering)
			{
				expression2 = Expression.Call(ExpressionHelperMethods.QueryableToList.MakeGenericMethod(new Type[] { expression.Type }), expression2);
			}
			if (this._settings.HandleNullPropagation == HandleNullPropagationOption.True)
			{
				return Expression.Condition(Expression.Equal(source, Expression.Constant(null)), Expression.Constant(null, expression2.Type), expression2);
			}
			return expression2;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001FEBC File Offset: 0x0001E0BC
		internal static Expression CreateTypeNameExpression(Expression source, IEdmStructuredType elementType, IEdmModel model)
		{
			IReadOnlyList<IEdmStructuredType> allDerivedTypes = SelectExpandBinder.GetAllDerivedTypes(elementType, model);
			if (allDerivedTypes.Count == 0)
			{
				return null;
			}
			Expression expression = Expression.Constant(elementType.FullTypeName());
			for (int i = 0; i < allDerivedTypes.Count; i++)
			{
				Type clrType = EdmLibHelpers.GetClrType(allDerivedTypes[i], model);
				if (clrType == null)
				{
					throw new ODataException(Error.Format(SRResources.MappingDoesNotContainResourceType, new object[] { allDerivedTypes[0].FullTypeName() }));
				}
				expression = Expression.Condition(Expression.TypeIs(source, clrType), Expression.Constant(allDerivedTypes[i].FullTypeName()), expression);
			}
			return expression;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001FF54 File Offset: 0x0001E154
		private static IReadOnlyList<IEdmStructuredType> GetAllDerivedTypes(IEdmStructuredType baseType, IEdmModel model)
		{
			IEnumerable<IEdmStructuredType> enumerable = model.SchemaElements.OfType<IEdmStructuredType>();
			List<Tuple<int, IEdmStructuredType>> list = new List<Tuple<int, IEdmStructuredType>>();
			foreach (IEdmStructuredType edmStructuredType in enumerable)
			{
				int num = SelectExpandBinder.IsDerivedTypeOf(edmStructuredType, baseType);
				if (num > 0)
				{
					list.Add(Tuple.Create<int, IEdmStructuredType>(num, edmStructuredType));
				}
			}
			return (from tuple in list
				orderby tuple.Item1
				select tuple.Item2).ToList<IEdmStructuredType>();
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0002000C File Offset: 0x0001E20C
		private static int IsDerivedTypeOf(IEdmStructuredType type, IEdmStructuredType baseType)
		{
			int num = 0;
			while (type != null)
			{
				if (baseType == type)
				{
					return num;
				}
				type = type.BaseType();
				num++;
			}
			return -1;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00020033 File Offset: 0x0001E233
		private static MethodInfo GetSelectMethod(Type elementType, Type resultType)
		{
			return ExpressionHelperMethods.EnumerableSelectGeneric.MakeGenericMethod(new Type[] { elementType, resultType });
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0002004D File Offset: 0x0001E24D
		private static bool IsSelectAll(SelectExpandClause selectExpandClause)
		{
			return selectExpandClause == null || (selectExpandClause.AllSelected || selectExpandClause.SelectedItems.OfType<WildcardSelectItem>().Any<WildcardSelectItem>());
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00020071 File Offset: 0x0001E271
		private static Type GetWrapperGenericType(bool isInstancePropertySet, bool isTypeNamePropertySet, bool isContainerPropertySet)
		{
			if (isInstancePropertySet)
			{
				if (!isContainerPropertySet)
				{
					return typeof(SelectExpandBinder.SelectAll<>);
				}
				return typeof(SelectExpandBinder.SelectAllAndExpand<>);
			}
			else
			{
				if (!isTypeNamePropertySet)
				{
					return typeof(SelectExpandBinder.SelectSome<>);
				}
				return typeof(SelectExpandBinder.SelectSomeAndInheritance<>);
			}
		}

		// Token: 0x04000267 RID: 615
		private ODataQueryContext _context;

		// Token: 0x04000268 RID: 616
		private IEdmModel _model;

		// Token: 0x04000269 RID: 617
		private ODataQuerySettings _settings;

		// Token: 0x0400026A RID: 618
		private string _modelID;

		// Token: 0x020002B4 RID: 692
		private class ReferenceNavigationPropertyExpandFilterVisitor : ExpressionVisitor
		{
			// Token: 0x060012D4 RID: 4820 RVA: 0x00042948 File Offset: 0x00040B48
			public ReferenceNavigationPropertyExpandFilterVisitor(ParameterExpression parameterExpression, Expression source)
			{
				this._source = source;
				this._parameterExpression = parameterExpression;
			}

			// Token: 0x060012D5 RID: 4821 RVA: 0x0004295E File Offset: 0x00040B5E
			protected override Expression VisitParameter(ParameterExpression node)
			{
				if (node != this._parameterExpression)
				{
					throw new ODataException(Error.Format(SRResources.ReferenceNavigationPropertyExpandFilterVisitorUnexpectedParameter, new object[] { node.Name }));
				}
				return this._source;
			}

			// Token: 0x040005A9 RID: 1449
			private Expression _source;

			// Token: 0x040005AA RID: 1450
			private ParameterExpression _parameterExpression;
		}

		// Token: 0x020002B5 RID: 693
		private class SelectAllAndExpand<TEntity> : SelectExpandWrapper<TEntity>
		{
		}

		// Token: 0x020002B6 RID: 694
		private class SelectAll<TEntity> : SelectExpandWrapper<TEntity>
		{
		}

		// Token: 0x020002B7 RID: 695
		private class SelectSomeAndInheritance<TEntity> : SelectExpandWrapper<TEntity>
		{
		}

		// Token: 0x020002B8 RID: 696
		private class SelectSome<TEntity> : SelectExpandBinder.SelectAllAndExpand<TEntity>
		{
		}
	}
}
