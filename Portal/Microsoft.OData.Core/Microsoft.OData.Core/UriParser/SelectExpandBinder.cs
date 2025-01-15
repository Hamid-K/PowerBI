using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000131 RID: 305
	internal sealed class SelectExpandBinder
	{
		// Token: 0x0600102B RID: 4139 RVA: 0x0002A280 File Offset: 0x00028480
		public SelectExpandBinder(ODataUriParserConfiguration configuration, ODataPathInfo odataPathInfo, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuredType>(odataPathInfo.TargetStructuredType, "edmType");
			this.configuration = configuration;
			this.edmType = odataPathInfo.TargetStructuredType;
			this.navigationSource = odataPathInfo.TargetNavigationSource;
			this.parsedSegments = odataPathInfo.Segments.ToList<ODataPathSegment>();
			this.state = state;
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0002A2F2 File Offset: 0x000284F2
		public IEdmModel Model
		{
			get
			{
				return this.configuration.Model;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0002A2FF File Offset: 0x000284FF
		public IEdmStructuredType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0002A307 File Offset: 0x00028507
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x0002A30F File Offset: 0x0002850F
		private ODataUriParserSettings Settings
		{
			get
			{
				return this.configuration.Settings;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0002A31C File Offset: 0x0002851C
		private ODataUriParserConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0002A324 File Offset: 0x00028524
		public SelectExpandClause Bind(ExpandToken expandToken, SelectToken selectToken)
		{
			List<SelectItem> list = new List<SelectItem>();
			if (expandToken != null && expandToken.ExpandTerms.Any<ExpandTermToken>())
			{
				list.AddRange(from s in expandToken.ExpandTerms.Select(new Func<ExpandTermToken, SelectItem>(this.GenerateExpandItem))
					where s != null
					select s);
			}
			bool flag = true;
			if (selectToken != null && selectToken.SelectTerms.Any<SelectTermToken>())
			{
				flag = false;
				foreach (SelectTermToken selectTermToken in selectToken.SelectTerms)
				{
					SelectExpandBinder.AddToSelectedItems(this.GenerateSelectItem(selectTermToken), list);
				}
			}
			return new SelectExpandClause(list, flag);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0002A3EC File Offset: 0x000285EC
		private SelectItem GenerateSelectItem(SelectTermToken tokenIn)
		{
			ExceptionUtils.CheckArgumentNotNull<SelectTermToken>(tokenIn, "tokenIn");
			ExceptionUtils.CheckArgumentNotNull<PathSegmentToken>(tokenIn.PathToProperty, "pathToProperty");
			SelectExpandBinder.VerifySelectedPath(tokenIn);
			SelectItem selectItem;
			if (this.ProcessWildcardTokenPath(tokenIn, out selectItem))
			{
				return selectItem;
			}
			IList<ODataPathSegment> list = this.ProcessSelectTokenPath(tokenIn.PathToProperty);
			if (SelectExpandBinder.VerifySelectedNavigationProperty(list, tokenIn))
			{
				return new PathSelectItem(new ODataSelectPath(list));
			}
			IEdmNavigationSource edmNavigationSource = this.NavigationSource;
			ODataPathSegment odataPathSegment = list.Last<ODataPathSegment>();
			IEdmType edmType = odataPathSegment.TargetEdmType;
			IEdmCollectionType edmCollectionType = edmType as IEdmCollectionType;
			if (edmCollectionType != null)
			{
				edmType = edmCollectionType.ElementType.Definition;
			}
			IEdmTypeReference edmTypeReference = edmType.ToTypeReference();
			ComputeClause computeClause = this.BindCompute(tokenIn.ComputeOption, edmNavigationSource, edmTypeReference);
			HashSet<EndPathToken> generatedProperties = SelectExpandBinder.GetGeneratedProperties(computeClause, null);
			FilterClause filterClause = this.BindFilter(tokenIn.FilterOption, edmNavigationSource, edmTypeReference, generatedProperties, false);
			OrderByClause orderByClause = this.BindOrderby(tokenIn.OrderByOptions, edmNavigationSource, edmTypeReference, generatedProperties, false);
			SearchClause searchClause = this.BindSearch(tokenIn.SearchOption, edmNavigationSource, edmTypeReference);
			List<ODataPathSegment> list2 = new List<ODataPathSegment>(this.parsedSegments);
			list2.AddRange(list);
			SelectExpandClause selectExpandClause = this.BindSelectExpand(null, tokenIn.SelectOption, list2, edmNavigationSource, edmTypeReference, generatedProperties, false);
			return new PathSelectItem(new ODataSelectPath(list), edmNavigationSource, selectExpandClause, filterClause, orderByClause, tokenIn.TopOption, tokenIn.SkipOption, tokenIn.CountQueryOption, searchClause, computeClause);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0002A530 File Offset: 0x00028730
		private SelectItem GenerateExpandItem(ExpandTermToken tokenIn)
		{
			ExceptionUtils.CheckArgumentNotNull<ExpandTermToken>(tokenIn, "tokenIn");
			PathSegmentToken pathToNavigationProp = tokenIn.PathToNavigationProp;
			IEdmStructuredType edmStructuredType = this.EdmType;
			List<ODataPathSegment> list = new List<ODataPathSegment>();
			PathSegmentToken pathSegmentToken = pathToNavigationProp;
			if (pathToNavigationProp.IsNamespaceOrContainerQualified())
			{
				list.AddRange(SelectExpandPathBinder.FollowTypeSegments(pathToNavigationProp, this.Model, this.Settings.SelectExpandLimit, this.configuration.Resolver, ref edmStructuredType, out pathSegmentToken));
			}
			IEdmProperty edmProperty = this.configuration.Resolver.ResolveProperty(edmStructuredType, pathSegmentToken.Identifier);
			if (edmProperty == null)
			{
				throw new ODataException(Strings.MetadataBinder_PropertyNotDeclared(edmStructuredType.FullTypeName(), pathToNavigationProp.Identifier));
			}
			IEdmNavigationProperty edmNavigationProperty = edmProperty as IEdmNavigationProperty;
			IEdmStructuralProperty edmStructuralProperty = edmProperty as IEdmStructuralProperty;
			if (edmNavigationProperty == null && edmStructuralProperty == null)
			{
				throw new ODataException(Strings.ExpandItemBinder_PropertyIsNotANavigationPropertyOrComplexProperty(pathToNavigationProp.Identifier, edmStructuredType.FullTypeName()));
			}
			if (edmStructuralProperty != null)
			{
				edmNavigationProperty = this.ParseComplexTypesBeforeNavigation(edmStructuralProperty, ref pathSegmentToken, list);
			}
			if (pathSegmentToken.NextToken != null && pathSegmentToken.NextToken.NextToken != null)
			{
				throw new ODataException(Strings.ExpandItemBinder_TraversingMultipleNavPropsInTheSamePath);
			}
			bool flag = false;
			if (pathSegmentToken.NextToken != null)
			{
				if (!(pathSegmentToken.NextToken.Identifier == "$ref"))
				{
					throw new ODataException(Strings.ExpandItemBinder_TraversingMultipleNavPropsInTheSamePath);
				}
				flag = true;
			}
			List<ODataPathSegment> list2 = new List<ODataPathSegment>(this.parsedSegments);
			list2.AddRange(list);
			IEdmNavigationSource edmNavigationSource = null;
			if (this.NavigationSource != null)
			{
				IEdmPathExpression edmPathExpression;
				edmNavigationSource = this.NavigationSource.FindNavigationTarget(edmNavigationProperty, new Func<IEdmPathExpression, List<ODataPathSegment>, bool>(BindingPathHelper.MatchBindingPath), list2, out edmPathExpression);
			}
			NavigationPropertySegment navigationPropertySegment = new NavigationPropertySegment(edmNavigationProperty, edmNavigationSource);
			list.Add(navigationPropertySegment);
			list2.Add(navigationPropertySegment);
			ODataExpandPath odataExpandPath = new ODataExpandPath(list);
			ApplyClause applyClause = this.BindApply(tokenIn.ApplyOptions, edmNavigationSource);
			ComputeClause computeClause = this.BindCompute(tokenIn.ComputeOption, edmNavigationSource, null);
			HashSet<EndPathToken> generatedProperties = SelectExpandBinder.GetGeneratedProperties(computeClause, applyClause);
			bool flag2;
			if (applyClause == null)
			{
				flag2 = false;
			}
			else
			{
				flag2 = applyClause.Transformations.Any((TransformationNode t) => t.Kind == TransformationNodeKind.Aggregate || t.Kind == TransformationNodeKind.GroupBy);
			}
			bool flag3 = flag2;
			FilterClause filterClause = this.BindFilter(tokenIn.FilterOption, edmNavigationSource, null, generatedProperties, flag3);
			OrderByClause orderByClause = this.BindOrderby(tokenIn.OrderByOptions, edmNavigationSource, null, generatedProperties, flag3);
			SearchClause searchClause = this.BindSearch(tokenIn.SearchOption, edmNavigationSource, null);
			if (flag)
			{
				return new ExpandedReferenceSelectItem(odataExpandPath, edmNavigationSource, filterClause, orderByClause, tokenIn.TopOption, tokenIn.SkipOption, tokenIn.CountQueryOption, searchClause, computeClause, applyClause);
			}
			SelectExpandClause selectExpandClause = this.BindSelectExpand(tokenIn.ExpandOption, tokenIn.SelectOption, list2, edmNavigationSource, null, generatedProperties, flag3);
			LevelsClause levelsClause = SelectExpandBinder.ParseLevels(tokenIn.LevelsOption, edmStructuredType, edmNavigationProperty);
			return new ExpandedNavigationSelectItem(odataExpandPath, edmNavigationSource, selectExpandClause, filterClause, orderByClause, tokenIn.TopOption, tokenIn.SkipOption, tokenIn.CountQueryOption, searchClause, levelsClause, computeClause, applyClause);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0002A7DC File Offset: 0x000289DC
		private ApplyClause BindApply(IEnumerable<QueryToken> applyToken, IEdmNavigationSource navigationSource)
		{
			if (applyToken != null && applyToken.Any<QueryToken>())
			{
				MetadataBinder metadataBinder = SelectExpandBinder.BuildNewMetadataBinder(this.Configuration, navigationSource, null, null, false);
				ApplyBinder applyBinder = new ApplyBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind), metadataBinder.BindingState);
				return applyBinder.BindApply(applyToken);
			}
			return null;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0002A828 File Offset: 0x00028A28
		private ComputeClause BindCompute(ComputeToken computeToken, IEdmNavigationSource navigationSource, IEdmTypeReference elementType = null)
		{
			if (computeToken != null)
			{
				MetadataBinder metadataBinder = SelectExpandBinder.BuildNewMetadataBinder(this.Configuration, navigationSource, elementType, null, false);
				ComputeBinder computeBinder = new ComputeBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind));
				return computeBinder.BindCompute(computeToken);
			}
			return null;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0002A864 File Offset: 0x00028A64
		private FilterClause BindFilter(QueryToken filterToken, IEdmNavigationSource navigationSource, IEdmTypeReference elementType, HashSet<EndPathToken> generatedProperties, bool collapsed = false)
		{
			if (filterToken != null)
			{
				MetadataBinder metadataBinder = SelectExpandBinder.BuildNewMetadataBinder(this.Configuration, navigationSource, elementType, generatedProperties, collapsed);
				FilterBinder filterBinder = new FilterBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind), metadataBinder.BindingState);
				return filterBinder.BindFilter(filterToken);
			}
			return null;
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0002A8A8 File Offset: 0x00028AA8
		private OrderByClause BindOrderby(IEnumerable<OrderByToken> orderByToken, IEdmNavigationSource navigationSource, IEdmTypeReference elementType, HashSet<EndPathToken> generatedProperties, bool collapsed = false)
		{
			if (orderByToken != null && orderByToken.Any<OrderByToken>())
			{
				MetadataBinder metadataBinder = SelectExpandBinder.BuildNewMetadataBinder(this.Configuration, navigationSource, elementType, generatedProperties, collapsed);
				OrderByBinder orderByBinder = new OrderByBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind));
				return orderByBinder.BindOrderBy(metadataBinder.BindingState, orderByToken);
			}
			return null;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0002A8F4 File Offset: 0x00028AF4
		private SearchClause BindSearch(QueryToken searchToken, IEdmNavigationSource navigationSource, IEdmTypeReference elementType)
		{
			if (searchToken != null)
			{
				MetadataBinder metadataBinder = SelectExpandBinder.BuildNewMetadataBinder(this.Configuration, navigationSource, elementType, null, false);
				SearchBinder searchBinder = new SearchBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind));
				return searchBinder.BindSearch(searchToken);
			}
			return null;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0002A930 File Offset: 0x00028B30
		private SelectExpandClause BindSelectExpand(ExpandToken expandToken, SelectToken selectToken, IList<ODataPathSegment> segments, IEdmNavigationSource navigationSource, IEdmTypeReference elementType, HashSet<EndPathToken> generatedProperties = null, bool collapsed = false)
		{
			if (expandToken != null || selectToken != null)
			{
				BindingState bindingState = SelectExpandBinder.CreateBindingState(this.Configuration, navigationSource, elementType, generatedProperties, collapsed);
				SelectExpandBinder selectExpandBinder = new SelectExpandBinder(this.Configuration, new ODataPathInfo(new ODataPath(segments)), bindingState);
				return selectExpandBinder.Bind(expandToken, selectToken);
			}
			return new SelectExpandClause(new Collection<SelectItem>(), true);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0002A984 File Offset: 0x00028B84
		private bool ProcessWildcardTokenPath(SelectTermToken selectToken, out SelectItem newSelectItem)
		{
			newSelectItem = null;
			if (selectToken == null || selectToken.PathToProperty == null)
			{
				return false;
			}
			PathSegmentToken pathToProperty = selectToken.PathToProperty;
			if (!SelectPathSegmentTokenBinder.TryBindAsWildcard(pathToProperty, this.Model, out newSelectItem))
			{
				return false;
			}
			if (pathToProperty.NextToken != null)
			{
				throw new ODataException(Strings.SelectExpandBinder_InvalidIdentifierAfterWildcard(pathToProperty.NextToken.Identifier));
			}
			SelectExpandBinder.VerifyNoQueryOptionsNested(selectToken, pathToProperty.Identifier);
			return true;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0002A9E4 File Offset: 0x00028BE4
		private List<ODataPathSegment> ProcessSelectTokenPath(PathSegmentToken tokenIn)
		{
			List<ODataPathSegment> list = new List<ODataPathSegment>();
			IEdmStructuredType edmStructuredType = this.edmType;
			if (tokenIn.IsNamespaceOrContainerQualified() && !UriParserHelper.IsAnnotation(tokenIn.Identifier))
			{
				PathSegmentToken pathSegmentToken;
				list.AddRange(SelectExpandPathBinder.FollowTypeSegments(tokenIn, this.Model, this.Settings.SelectExpandLimit, this.configuration.Resolver, ref edmStructuredType, out pathSegmentToken));
				tokenIn = pathSegmentToken as NonSystemToken;
				if (tokenIn == null)
				{
					throw new ODataException(Strings.SelectExpandBinder_SystemTokenInSelect(pathSegmentToken.Identifier));
				}
			}
			ODataPathSegment odataPathSegment = SelectPathSegmentTokenBinder.ConvertNonTypeTokenToSegment(tokenIn, this.Model, edmStructuredType, this.configuration.Resolver, this.state);
			if (odataPathSegment != null)
			{
				list.Add(odataPathSegment);
				for (;;)
				{
					edmStructuredType = odataPathSegment.EdmType as IEdmStructuredType;
					IEdmCollectionType edmCollectionType = odataPathSegment.EdmType as IEdmCollectionType;
					IEdmPrimitiveType edmPrimitiveType = odataPathSegment.EdmType as IEdmPrimitiveType;
					DynamicPathSegment dynamicPathSegment = odataPathSegment as DynamicPathSegment;
					if ((edmStructuredType == null || edmStructuredType.TypeKind != EdmTypeKind.Complex) && (edmCollectionType == null || edmCollectionType.ElementType.TypeKind() != EdmTypeKind.Complex) && (edmPrimitiveType == null || edmPrimitiveType.TypeKind != EdmTypeKind.Primitive) && (dynamicPathSegment == null || tokenIn.NextToken == null))
					{
						goto IL_021E;
					}
					NonSystemToken nonSystemToken = tokenIn.NextToken as NonSystemToken;
					if (nonSystemToken == null)
					{
						goto IL_021E;
					}
					if (UriParserHelper.IsAnnotation(nonSystemToken.Identifier))
					{
						odataPathSegment = SelectPathSegmentTokenBinder.ConvertNonTypeTokenToSegment(nonSystemToken, this.Model, edmStructuredType, this.configuration.Resolver, null);
					}
					else if (edmPrimitiveType == null && dynamicPathSegment == null)
					{
						if (edmStructuredType == null)
						{
							edmStructuredType = edmCollectionType.ElementType.Definition as IEdmStructuredType;
						}
						odataPathSegment = SelectPathSegmentTokenBinder.ConvertNonTypeTokenToSegment(nonSystemToken, this.Model, edmStructuredType, this.configuration.Resolver, null);
					}
					else
					{
						EdmPrimitiveTypeKind primitiveTypeKind = EdmCoreModel.Instance.GetPrimitiveTypeKind(nonSystemToken.Identifier);
						IEdmPrimitiveType primitiveType = EdmCoreModel.Instance.GetPrimitiveType(primitiveTypeKind);
						if (primitiveType != null)
						{
							odataPathSegment = new TypeSegment(primitiveType, primitiveType, null);
						}
						else
						{
							if (dynamicPathSegment == null)
							{
								break;
							}
							odataPathSegment = new DynamicPathSegment(nonSystemToken.Identifier);
						}
					}
					if (odataPathSegment == null)
					{
						IEdmStructuredType edmStructuredType2 = UriEdmHelpers.FindTypeFromModel(this.Model, nonSystemToken.Identifier, this.configuration.Resolver) as IEdmStructuredType;
						if (edmStructuredType2.IsOrInheritsFrom(edmStructuredType))
						{
							odataPathSegment = new TypeSegment(edmStructuredType2, null);
						}
					}
					if (odataPathSegment == null)
					{
						goto IL_021E;
					}
					tokenIn = nonSystemToken;
					list.Add(odataPathSegment);
				}
				throw new ODataException(Strings.SelectBinder_MultiLevelPathInSelect);
			}
			IL_021E:
			if (tokenIn.NextToken != null)
			{
				throw new ODataException(Strings.SelectBinder_MultiLevelPathInSelect);
			}
			if (odataPathSegment == null)
			{
				throw new ODataException(Strings.MetadataBinder_InvalidIdentifierInQueryOption(tokenIn.Identifier));
			}
			NavigationPropertySegment navigationPropertySegment = list.LastOrDefault<ODataPathSegment>() as NavigationPropertySegment;
			if (navigationPropertySegment != null && tokenIn.NextToken != null)
			{
				throw new ODataException(Strings.SelectBinder_MultiLevelPathInSelect);
			}
			return list;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0002AC5C File Offset: 0x00028E5C
		private static HashSet<EndPathToken> GetGeneratedProperties(ComputeClause computeOption, ApplyClause applyOption)
		{
			HashSet<EndPathToken> hashSet = null;
			if (applyOption != null)
			{
				hashSet = applyOption.GetLastAggregatedPropertyNames();
			}
			if (computeOption != null)
			{
				HashSet<EndPathToken> hashSet2 = new HashSet<EndPathToken>(computeOption.ComputedItems.Select((ComputeExpression i) => new EndPathToken(i.Alias, null)));
				if (hashSet == null)
				{
					hashSet = hashSet2;
				}
				else
				{
					hashSet.UnionWith(hashSet2);
				}
			}
			return hashSet;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0002ACB8 File Offset: 0x00028EB8
		private IEdmNavigationProperty ParseComplexTypesBeforeNavigation(IEdmStructuralProperty edmProperty, ref PathSegmentToken currentToken, List<ODataPathSegment> pathSoFar)
		{
			pathSoFar.Add(new PropertySegment(edmProperty));
			if (currentToken.NextToken == null)
			{
				throw new ODataException(Strings.ExpandItemBinder_PropertyIsNotANavigationPropertyOrComplexProperty(currentToken.Identifier, edmProperty.DeclaringType.FullTypeName()));
			}
			currentToken = currentToken.NextToken;
			IEdmType edmType = edmProperty.Type.Definition;
			IEdmCollectionType edmCollectionType = edmType as IEdmCollectionType;
			if (edmCollectionType != null)
			{
				edmType = edmCollectionType.ElementType.Definition;
			}
			IEdmStructuredType edmStructuredType = edmType as IEdmStructuredType;
			if (edmStructuredType == null)
			{
				throw new ODataException(Strings.ExpandItemBinder_InvaidSegmentInExpand(currentToken.Identifier));
			}
			if (currentToken.IsNamespaceOrContainerQualified())
			{
				pathSoFar.AddRange(SelectExpandPathBinder.FollowTypeSegments(currentToken, this.Model, this.Settings.SelectExpandLimit, this.configuration.Resolver, ref edmStructuredType, out currentToken));
			}
			IEdmProperty edmProperty2 = this.configuration.Resolver.ResolveProperty(edmStructuredType, currentToken.Identifier);
			if (edmProperty == null)
			{
				throw new ODataException(Strings.MetadataBinder_PropertyNotDeclared(edmStructuredType.FullTypeName(), currentToken.Identifier));
			}
			IEdmStructuralProperty edmStructuralProperty = edmProperty2 as IEdmStructuralProperty;
			if (edmStructuralProperty != null)
			{
				edmProperty2 = this.ParseComplexTypesBeforeNavigation(edmStructuralProperty, ref currentToken, pathSoFar);
			}
			IEdmNavigationProperty edmNavigationProperty = edmProperty2 as IEdmNavigationProperty;
			if (edmNavigationProperty != null)
			{
				return edmNavigationProperty;
			}
			throw new ODataException(Strings.ExpandItemBinder_PropertyIsNotANavigationPropertyOrComplexProperty(currentToken.Identifier, edmStructuredType.FullTypeName()));
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0002ADE4 File Offset: 0x00028FE4
		private static LevelsClause ParseLevels(long? levelsOption, IEdmType sourceType, IEdmNavigationProperty property)
		{
			if (levelsOption == null)
			{
				return null;
			}
			IEdmType edmType = property.ToEntityType();
			if (sourceType != null && edmType != null && !UriEdmHelpers.IsRelatedTo(sourceType, edmType))
			{
				throw new ODataException(Strings.ExpandItemBinder_LevelsNotAllowedOnIncompatibleRelatedType(property.Name, edmType.FullTypeName(), sourceType.FullTypeName()));
			}
			return new LevelsClause(levelsOption.Value < 0L, levelsOption.Value);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0002AE48 File Offset: 0x00029048
		private static MetadataBinder BuildNewMetadataBinder(ODataUriParserConfiguration config, IEdmNavigationSource targetNavigationSource, IEdmTypeReference elementType, HashSet<EndPathToken> generatedProperties = null, bool collapsed = false)
		{
			BindingState bindingState = SelectExpandBinder.CreateBindingState(config, targetNavigationSource, elementType, generatedProperties, collapsed);
			return new MetadataBinder(bindingState);
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0002AE68 File Offset: 0x00029068
		private static BindingState CreateBindingState(ODataUriParserConfiguration config, IEdmNavigationSource targetNavigationSource, IEdmTypeReference elementType, HashSet<EndPathToken> generatedProperties = null, bool collapsed = false)
		{
			if (targetNavigationSource == null && elementType == null)
			{
				return null;
			}
			BindingState bindingState = new BindingState(config)
			{
				ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable((elementType != null) ? elementType : targetNavigationSource.EntityType().ToTypeReference(), targetNavigationSource)
			};
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			bindingState.AggregatedPropertyNames = generatedProperties;
			bindingState.IsCollapsed = collapsed;
			return bindingState;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0002AEC4 File Offset: 0x000290C4
		private static void VerifySelectedPath(SelectTermToken selectedToken)
		{
			for (PathSegmentToken pathSegmentToken = selectedToken.PathToProperty; pathSegmentToken != null; pathSegmentToken = pathSegmentToken.NextToken)
			{
				if (pathSegmentToken is SystemToken)
				{
					throw new ODataException(Strings.SelectExpandBinder_SystemTokenInSelect(pathSegmentToken.Identifier));
				}
			}
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0002AF00 File Offset: 0x00029100
		private static bool VerifySelectedNavigationProperty(IList<ODataPathSegment> selectedPath, SelectTermToken tokenIn)
		{
			NavigationPropertySegment navigationPropertySegment = selectedPath.LastOrDefault<ODataPathSegment>() as NavigationPropertySegment;
			if (navigationPropertySegment != null)
			{
				SelectExpandBinder.VerifyNoQueryOptionsNested(tokenIn, navigationPropertySegment.Identifier);
				return true;
			}
			return false;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0002AF2C File Offset: 0x0002912C
		private static void VerifyNoQueryOptionsNested(SelectTermToken selectToken, string identifier)
		{
			if (selectToken != null && (selectToken.ComputeOption != null || selectToken.FilterOption != null || selectToken.OrderByOptions != null || selectToken.SearchOption != null || selectToken.CountQueryOption != null || selectToken.SelectOption != null || selectToken.TopOption != null || selectToken.SkipOption != null))
			{
				throw new ODataException(Strings.SelectExpandBinder_InvalidQueryOptionNestedSelection(identifier));
			}
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0002AFA0 File Offset: 0x000291A0
		internal static void AddToSelectedItems(SelectItem itemToAdd, List<SelectItem> selectItems)
		{
			if (itemToAdd == null)
			{
				return;
			}
			if (selectItems.Any((SelectItem x) => x is WildcardSelectItem) && SelectExpandBinder.IsStructuralOrNavigationPropertySelectionItem(itemToAdd))
			{
				return;
			}
			PathSelectItem pathSelectItem = itemToAdd as PathSelectItem;
			if (pathSelectItem != null)
			{
				NavigationPropertySegment navigationPropertySegment = pathSelectItem.SelectedPath.LastSegment as NavigationPropertySegment;
				if (navigationPropertySegment != null && selectItems.OfType<PathSelectItem>().Any((PathSelectItem i) => i.SelectedPath.Equals(pathSelectItem.SelectedPath)))
				{
					return;
				}
			}
			if (itemToAdd is WildcardSelectItem)
			{
				List<SelectItem> list = selectItems.Where((SelectItem s) => SelectExpandBinder.IsStructuralSelectionItem(s)).ToList<SelectItem>();
				foreach (SelectItem selectItem in list)
				{
					selectItems.Remove(selectItem);
				}
			}
			selectItems.Add(itemToAdd);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0002B0AC File Offset: 0x000292AC
		private static bool IsStructuralOrNavigationPropertySelectionItem(SelectItem selectItem)
		{
			PathSelectItem pathSelectItem = selectItem as PathSelectItem;
			return pathSelectItem != null && (pathSelectItem.SelectedPath.LastSegment is NavigationPropertySegment || pathSelectItem.SelectedPath.LastSegment is PropertySegment);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0002B0EC File Offset: 0x000292EC
		private static bool IsStructuralSelectionItem(SelectItem selectItem)
		{
			PathSelectItem pathSelectItem = selectItem as PathSelectItem;
			return pathSelectItem != null && pathSelectItem.SelectedPath.LastSegment is PropertySegment;
		}

		// Token: 0x040007AC RID: 1964
		private readonly ODataUriParserConfiguration configuration;

		// Token: 0x040007AD RID: 1965
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x040007AE RID: 1966
		private readonly IEdmStructuredType edmType;

		// Token: 0x040007AF RID: 1967
		private List<ODataPathSegment> parsedSegments = new List<ODataPathSegment>();

		// Token: 0x040007B0 RID: 1968
		private BindingState state;
	}
}
