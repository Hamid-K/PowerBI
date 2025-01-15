using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000F4 RID: 244
	internal sealed class SelectExpandBinder
	{
		// Token: 0x06000BD0 RID: 3024 RVA: 0x0001E8BC File Offset: 0x0001CABC
		public SelectExpandBinder(ODataUriParserConfiguration configuration, ODataPathInfo odataPathInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuredType>(odataPathInfo.TargetStructuredType, "edmType");
			this.configuration = configuration;
			this.edmType = odataPathInfo.TargetStructuredType;
			this.navigationSource = odataPathInfo.TargetNavigationSource;
			this.parsedSegments = Enumerable.ToList<ODataPathSegment>(odataPathInfo.Segments);
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0001E927 File Offset: 0x0001CB27
		public IEdmModel Model
		{
			get
			{
				return this.configuration.Model;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0001E934 File Offset: 0x0001CB34
		public IEdmStructuredType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0001E93C File Offset: 0x0001CB3C
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0001E944 File Offset: 0x0001CB44
		private ODataUriParserSettings Settings
		{
			get
			{
				return this.configuration.Settings;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0001E951 File Offset: 0x0001CB51
		private ODataUriParserConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0001E95C File Offset: 0x0001CB5C
		public SelectExpandClause Bind(ExpandToken tokenIn)
		{
			ExceptionUtils.CheckArgumentNotNull<ExpandToken>(tokenIn, "tokenIn");
			List<SelectItem> list = new List<SelectItem>();
			if (Enumerable.Single<ExpandTermToken>(tokenIn.ExpandTerms).ExpandOption != null)
			{
				list.AddRange(Enumerable.Where<SelectItem>(Enumerable.Select<ExpandTermToken, SelectItem>(Enumerable.Single<ExpandTermToken>(tokenIn.ExpandTerms).ExpandOption.ExpandTerms, new Func<ExpandTermToken, SelectItem>(this.GenerateExpandItem)), (SelectItem expandedNavigationSelectItem) => expandedNavigationSelectItem != null));
			}
			bool flag = Enumerable.Single<ExpandTermToken>(tokenIn.ExpandTerms).SelectOption == null;
			SelectExpandClause selectExpandClause = new SelectExpandClause(list, flag);
			if (!flag)
			{
				SelectBinder selectBinder = new SelectBinder(this.Model, this.EdmType, this.Configuration.Settings.SelectExpandLimit, selectExpandClause, this.configuration.Resolver);
				selectBinder.Bind(Enumerable.Single<ExpandTermToken>(tokenIn.ExpandTerms).SelectOption);
			}
			return selectExpandClause;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0001EA44 File Offset: 0x0001CC44
		private SelectExpandClause BindSubLevel(ExpandToken tokenIn)
		{
			List<SelectItem> list = Enumerable.ToList<SelectItem>(Enumerable.Where<SelectItem>(Enumerable.Select<ExpandTermToken, SelectItem>(tokenIn.ExpandTerms, new Func<ExpandTermToken, SelectItem>(this.GenerateExpandItem)), (SelectItem expandedNavigationSelectItem) => expandedNavigationSelectItem != null));
			return new SelectExpandClause(list, true);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0001EA9C File Offset: 0x0001CC9C
		private SelectExpandClause GenerateSubExpand(ExpandTermToken tokenIn)
		{
			SelectExpandBinder selectExpandBinder = new SelectExpandBinder(this.Configuration, new ODataPathInfo(new ODataPath(this.parsedSegments)));
			return selectExpandBinder.BindSubLevel(tokenIn.ExpandOption);
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0001EAD4 File Offset: 0x0001CCD4
		private SelectExpandClause DecorateExpandWithSelect(SelectExpandClause subExpand, IEdmNavigationProperty currentNavProp, SelectToken select)
		{
			SelectBinder selectBinder = new SelectBinder(this.Model, currentNavProp.ToEntityType(), this.Settings.SelectExpandLimit, subExpand, this.configuration.Resolver);
			return selectBinder.Bind(select);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0001EB11 File Offset: 0x0001CD11
		private static SelectExpandClause BuildDefaultSubExpand()
		{
			return new SelectExpandClause(new Collection<SelectItem>(), true);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0001EB20 File Offset: 0x0001CD20
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
			this.parsedSegments.AddRange(list);
			IEdmNavigationSource edmNavigationSource = null;
			if (this.NavigationSource != null)
			{
				IEdmPathExpression edmPathExpression;
				edmNavigationSource = this.NavigationSource.FindNavigationTarget(edmNavigationProperty, new Func<IEdmPathExpression, List<ODataPathSegment>, bool>(BindingPathHelper.MatchBindingPath), this.parsedSegments, out edmPathExpression);
			}
			NavigationPropertySegment navigationPropertySegment = new NavigationPropertySegment(edmNavigationProperty, edmNavigationSource);
			list.Add(navigationPropertySegment);
			this.parsedSegments.Add(navigationPropertySegment);
			ODataExpandPath odataExpandPath = new ODataExpandPath(list);
			FilterClause filterClause = null;
			if (tokenIn.FilterOption != null)
			{
				MetadataBinder metadataBinder = this.BuildNewMetadataBinder(edmNavigationSource);
				FilterBinder filterBinder = new FilterBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind), metadataBinder.BindingState);
				filterClause = filterBinder.BindFilter(tokenIn.FilterOption);
			}
			OrderByClause orderByClause = null;
			if (tokenIn.OrderByOptions != null)
			{
				MetadataBinder metadataBinder2 = this.BuildNewMetadataBinder(edmNavigationSource);
				OrderByBinder orderByBinder = new OrderByBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder2.Bind));
				orderByClause = orderByBinder.BindOrderBy(metadataBinder2.BindingState, tokenIn.OrderByOptions);
			}
			SearchClause searchClause = null;
			if (tokenIn.SearchOption != null)
			{
				MetadataBinder metadataBinder3 = this.BuildNewMetadataBinder(edmNavigationSource);
				SearchBinder searchBinder = new SearchBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder3.Bind));
				searchClause = searchBinder.BindSearch(tokenIn.SearchOption);
			}
			ComputeClause computeClause = null;
			if (tokenIn.ComputeOption != null)
			{
				MetadataBinder metadataBinder4 = this.BuildNewMetadataBinder(edmNavigationSource);
				ComputeBinder computeBinder = new ComputeBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder4.Bind));
				computeClause = computeBinder.BindCompute(tokenIn.ComputeOption);
			}
			if (flag)
			{
				return new ExpandedReferenceSelectItem(odataExpandPath, edmNavigationSource, filterClause, orderByClause, tokenIn.TopOption, tokenIn.SkipOption, tokenIn.CountQueryOption, searchClause, computeClause);
			}
			SelectExpandClause selectExpandClause;
			if (tokenIn.ExpandOption != null)
			{
				selectExpandClause = this.GenerateSubExpand(tokenIn);
			}
			else
			{
				selectExpandClause = SelectExpandBinder.BuildDefaultSubExpand();
			}
			selectExpandClause = this.DecorateExpandWithSelect(selectExpandClause, edmNavigationProperty, tokenIn.SelectOption);
			LevelsClause levelsClause = SelectExpandBinder.ParseLevels(tokenIn.LevelsOption, edmStructuredType, edmNavigationProperty);
			return new ExpandedNavigationSelectItem(odataExpandPath, edmNavigationSource, selectExpandClause, filterClause, orderByClause, tokenIn.TopOption, tokenIn.SkipOption, tokenIn.CountQueryOption, searchClause, levelsClause, computeClause);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0001EE28 File Offset: 0x0001D028
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

		// Token: 0x06000BDD RID: 3037 RVA: 0x0001EF54 File Offset: 0x0001D154
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

		// Token: 0x06000BDE RID: 3038 RVA: 0x0001EFB8 File Offset: 0x0001D1B8
		private MetadataBinder BuildNewMetadataBinder(IEdmNavigationSource targetNavigationSource)
		{
			BindingState bindingState = new BindingState(this.configuration)
			{
				ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable(targetNavigationSource.EntityType().ToTypeReference(), targetNavigationSource)
			};
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			return new MetadataBinder(bindingState);
		}

		// Token: 0x0400069A RID: 1690
		private readonly ODataUriParserConfiguration configuration;

		// Token: 0x0400069B RID: 1691
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x0400069C RID: 1692
		private readonly IEdmStructuredType edmType;

		// Token: 0x0400069D RID: 1693
		private List<ODataPathSegment> parsedSegments = new List<ODataPathSegment>();
	}
}
