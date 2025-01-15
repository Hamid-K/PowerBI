using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001D7 RID: 471
	internal sealed class SelectExpandBinder
	{
		// Token: 0x0600115C RID: 4444 RVA: 0x0003D7B0 File Offset: 0x0003B9B0
		public SelectExpandBinder(ODataUriParserConfiguration configuration, IEdmStructuredType edmType, IEdmNavigationSource navigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuredType>(edmType, "edmType");
			this.configuration = configuration;
			this.edmType = edmType;
			this.navigationSource = navigationSource;
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x0003D7E3 File Offset: 0x0003B9E3
		public IEdmModel Model
		{
			get
			{
				return this.configuration.Model;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0003D7F0 File Offset: 0x0003B9F0
		public IEdmStructuredType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x0003D7F8 File Offset: 0x0003B9F8
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x0003D800 File Offset: 0x0003BA00
		private ODataUriParserSettings Settings
		{
			get
			{
				return this.configuration.Settings;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x0003D80D File Offset: 0x0003BA0D
		private ODataUriParserConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0003D820 File Offset: 0x0003BA20
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

		// Token: 0x06001163 RID: 4451 RVA: 0x0003D90C File Offset: 0x0003BB0C
		private SelectExpandClause BindSubLevel(ExpandToken tokenIn)
		{
			List<SelectItem> list = Enumerable.ToList<SelectItem>(Enumerable.Where<SelectItem>(Enumerable.Select<ExpandTermToken, SelectItem>(tokenIn.ExpandTerms, new Func<ExpandTermToken, SelectItem>(this.GenerateExpandItem)), (SelectItem expandedNavigationSelectItem) => expandedNavigationSelectItem != null));
			return new SelectExpandClause(list, true);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0003D960 File Offset: 0x0003BB60
		private SelectExpandClause GenerateSubExpand(IEdmNavigationProperty currentNavProp, ExpandTermToken tokenIn)
		{
			SelectExpandBinder selectExpandBinder = new SelectExpandBinder(this.Configuration, currentNavProp.ToEntityType(), (this.NavigationSource != null) ? this.NavigationSource.FindNavigationTarget(currentNavProp) : null);
			return selectExpandBinder.BindSubLevel(tokenIn.ExpandOption);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0003D9A4 File Offset: 0x0003BBA4
		private SelectExpandClause DecorateExpandWithSelect(SelectExpandClause subExpand, IEdmNavigationProperty currentNavProp, SelectToken select)
		{
			SelectBinder selectBinder = new SelectBinder(this.Model, currentNavProp.ToEntityType(), this.Settings.SelectExpandLimit, subExpand, this.configuration.Resolver);
			return selectBinder.Bind(select);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0003D9E1 File Offset: 0x0003BBE1
		private SelectExpandClause BuildDefaultSubExpand()
		{
			return new SelectExpandClause(new Collection<SelectItem>(), true);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0003D9F0 File Offset: 0x0003BBF0
		private SelectItem GenerateExpandItem(ExpandTermToken tokenIn)
		{
			ExceptionUtils.CheckArgumentNotNull<ExpandTermToken>(tokenIn, "tokenIn");
			if (tokenIn.PathToNavProp.NextToken != null && !tokenIn.PathToNavProp.IsNamespaceOrContainerQualified() && (tokenIn.PathToNavProp.NextToken.Identifier != "$ref" || tokenIn.PathToNavProp.NextToken.NextToken != null))
			{
				throw new ODataException(Strings.ExpandItemBinder_TraversingMultipleNavPropsInTheSamePath);
			}
			PathSegmentToken pathToNavProp = tokenIn.PathToNavProp;
			IEdmStructuredType edmStructuredType = this.EdmType;
			List<ODataPathSegment> list = new List<ODataPathSegment>();
			PathSegmentToken pathSegmentToken = pathToNavProp;
			if (pathToNavProp.IsNamespaceOrContainerQualified())
			{
				list.AddRange(SelectExpandPathBinder.FollowTypeSegments(pathToNavProp, this.Model, this.Settings.SelectExpandLimit, this.configuration.Resolver, ref edmStructuredType, out pathSegmentToken));
			}
			IEdmProperty edmProperty = this.configuration.Resolver.ResolveProperty(edmStructuredType, pathSegmentToken.Identifier);
			if (edmProperty == null)
			{
				throw new ODataException(Strings.MetadataBinder_PropertyNotDeclared(edmStructuredType.FullTypeName(), pathToNavProp.Identifier));
			}
			IEdmNavigationProperty edmNavigationProperty = edmProperty as IEdmNavigationProperty;
			if (edmNavigationProperty == null)
			{
				throw new ODataException(Strings.ExpandItemBinder_PropertyIsNotANavigationProperty(pathToNavProp.Identifier, edmStructuredType.FullTypeName()));
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
			list.Add(new NavigationPropertySegment(edmNavigationProperty, null));
			ODataExpandPath odataExpandPath = new ODataExpandPath(list);
			IEdmNavigationSource edmNavigationSource = null;
			if (this.NavigationSource != null)
			{
				edmNavigationSource = this.NavigationSource.FindNavigationTarget(edmNavigationProperty);
			}
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
			if (flag)
			{
				return new ExpandedReferenceSelectItem(odataExpandPath, edmNavigationSource, filterClause, orderByClause, tokenIn.TopOption, tokenIn.SkipOption, tokenIn.CountQueryOption, searchClause);
			}
			SelectExpandClause selectExpandClause;
			if (tokenIn.ExpandOption != null)
			{
				selectExpandClause = this.GenerateSubExpand(edmNavigationProperty, tokenIn);
			}
			else
			{
				selectExpandClause = this.BuildDefaultSubExpand();
			}
			selectExpandClause = this.DecorateExpandWithSelect(selectExpandClause, edmNavigationProperty, tokenIn.SelectOption);
			LevelsClause levelsClause = this.ParseLevels(tokenIn.LevelsOption, edmStructuredType, edmNavigationProperty);
			return new ExpandedNavigationSelectItem(odataExpandPath, edmNavigationSource, selectExpandClause, filterClause, orderByClause, tokenIn.TopOption, tokenIn.SkipOption, tokenIn.CountQueryOption, searchClause, levelsClause);
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0003DCA0 File Offset: 0x0003BEA0
		private LevelsClause ParseLevels(long? levelsOption, IEdmType sourceType, IEdmNavigationProperty property)
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

		// Token: 0x06001169 RID: 4457 RVA: 0x0003DD04 File Offset: 0x0003BF04
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Rule only applies to ODataLib Serialization code.")]
		private MetadataBinder BuildNewMetadataBinder(IEdmNavigationSource targetNavigationSource)
		{
			BindingState bindingState = new BindingState(this.configuration)
			{
				ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable(targetNavigationSource.EntityType().ToTypeReference(), targetNavigationSource)
			};
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			return new MetadataBinder(bindingState);
		}

		// Token: 0x0400078D RID: 1933
		private readonly ODataUriParserConfiguration configuration;

		// Token: 0x0400078E RID: 1934
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x0400078F RID: 1935
		private readonly IEdmStructuredType edmType;
	}
}
