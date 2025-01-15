using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Aggregation;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Parsers;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001EF RID: 495
	public class ODataQueryOptionParser
	{
		// Token: 0x060011F6 RID: 4598 RVA: 0x00041180 File Offset: 0x0003F380
		public ODataQueryOptionParser(IEdmModel model, IEdmType targetEdmType, IEdmNavigationSource targetNavigationSource, IDictionary<string, string> queryOptions)
		{
			ExceptionUtils.CheckArgumentNotNull<IDictionary<string, string>>(queryOptions, "queryOptions");
			this.targetEdmType = targetEdmType;
			this.targetNavigationSource = targetNavigationSource;
			this.queryOptions = queryOptions;
			ODataUriParserConfiguration odataUriParserConfiguration = new ODataUriParserConfiguration(model);
			odataUriParserConfiguration.ParameterAliasValueAccessor = new ParameterAliasValueAccessor(Enumerable.ToDictionary<KeyValuePair<string, string>, string, string>(Enumerable.Where<KeyValuePair<string, string>>(queryOptions, (KeyValuePair<string, string> _) => _.Key.StartsWith("@", 4)), (KeyValuePair<string, string> _) => _.Key, (KeyValuePair<string, string> _) => _.Value));
			this.Configuration = odataUriParserConfiguration;
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00041231 File Offset: 0x0003F431
		public ODataUriParserSettings Settings
		{
			get
			{
				return this.Configuration.Settings;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x0004123E File Offset: 0x0003F43E
		public IDictionary<string, SingleValueNode> ParameterAliasNodes
		{
			get
			{
				return this.Configuration.ParameterAliasValueAccessor.ParameterAliasValueNodesCached;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00041250 File Offset: 0x0003F450
		// (set) Token: 0x060011FA RID: 4602 RVA: 0x0004125D File Offset: 0x0003F45D
		public ODataUriResolver Resolver
		{
			get
			{
				return this.Configuration.Resolver;
			}
			set
			{
				this.Configuration.Resolver = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x0004126B File Offset: 0x0003F46B
		// (set) Token: 0x060011FC RID: 4604 RVA: 0x00041273 File Offset: 0x0003F473
		internal ODataUriParserConfiguration Configuration { get; set; }

		// Token: 0x060011FD RID: 4605 RVA: 0x0004127C File Offset: 0x0003F47C
		public FilterClause ParseFilter()
		{
			if (this.filterClause != null)
			{
				return this.filterClause;
			}
			string text;
			if (!this.TryGetQueryOption("$filter", out text) || string.IsNullOrEmpty(text) || this.targetEdmType == null)
			{
				return null;
			}
			this.filterClause = this.ParseFilterImplementation(text, this.Configuration, this.targetEdmType, this.targetNavigationSource);
			return this.filterClause;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x000412E0 File Offset: 0x0003F4E0
		public ApplyClause ParseApply()
		{
			if (this.applyClause != null)
			{
				return this.applyClause;
			}
			string text;
			if (!this.TryGetQueryOption("$apply", out text) || string.IsNullOrEmpty(text) || this.targetEdmType == null)
			{
				return null;
			}
			this.applyClause = ODataQueryOptionParser.ParseApplyImplementation(text, this.Configuration, this.targetEdmType, this.targetNavigationSource);
			return this.applyClause;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00041344 File Offset: 0x0003F544
		public SelectExpandClause ParseSelectAndExpand()
		{
			if (this.selectExpandClause != null)
			{
				return this.selectExpandClause;
			}
			string text;
			string text2;
			if (((!this.TryGetQueryOption("$select", out text) || text == null) & (!this.TryGetQueryOption("$expand", out text2) || text2 == null)) || this.targetEdmType == null)
			{
				return null;
			}
			IEdmStructuredType edmStructuredType = this.targetEdmType as IEdmStructuredType;
			if (edmStructuredType == null)
			{
				throw new ODataException(Strings.UriParser_TypeInvalidForSelectExpand(this.targetEdmType));
			}
			this.selectExpandClause = ODataQueryOptionParser.ParseSelectAndExpandImplementation(text, text2, this.Configuration, edmStructuredType, this.targetNavigationSource);
			return this.selectExpandClause;
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x000413DC File Offset: 0x0003F5DC
		public OrderByClause ParseOrderBy()
		{
			if (this.orderByClause != null)
			{
				return this.orderByClause;
			}
			string text;
			if (!this.TryGetQueryOption("$orderby", out text) || string.IsNullOrEmpty(text) || this.targetEdmType == null)
			{
				return null;
			}
			this.orderByClause = this.ParseOrderByImplementation(text, this.Configuration, this.targetEdmType, this.targetNavigationSource);
			return this.orderByClause;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00041440 File Offset: 0x0003F640
		public long? ParseTop()
		{
			string text;
			if (!this.TryGetQueryOption("$top", out text))
			{
				return default(long?);
			}
			return ODataQueryOptionParser.ParseTop(text);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0004146C File Offset: 0x0003F66C
		public long? ParseSkip()
		{
			string text;
			if (!this.TryGetQueryOption("$skip", out text))
			{
				return default(long?);
			}
			return ODataQueryOptionParser.ParseSkip(text);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00041498 File Offset: 0x0003F698
		public bool? ParseCount()
		{
			string text;
			if (!this.TryGetQueryOption("$count", out text))
			{
				return default(bool?);
			}
			return ODataQueryOptionParser.ParseCount(text);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x000414C4 File Offset: 0x0003F6C4
		public SearchClause ParseSearch()
		{
			if (this.searchClause != null)
			{
				return this.searchClause;
			}
			string text;
			if (!this.TryGetQueryOption("$search", out text) || text == null)
			{
				return null;
			}
			this.searchClause = ODataQueryOptionParser.ParseSearchImplementation(text, this.Configuration);
			return this.searchClause;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0004150C File Offset: 0x0003F70C
		public string ParseSkipToken()
		{
			string text;
			if (!this.TryGetQueryOption("$skiptoken", out text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0004152C File Offset: 0x0003F72C
		public string ParseDeltaToken()
		{
			string text;
			if (!this.TryGetQueryOption("$deltatoken", out text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0004154C File Offset: 0x0003F74C
		private FilterClause ParseFilterImplementation(string filter, ODataUriParserConfiguration configuration, IEdmType elementType, IEdmNavigationSource navigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(elementType, "elementType");
			ExceptionUtils.CheckArgumentNotNull<string>(filter, "filter");
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(configuration.Settings.FilterLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier);
			QueryToken queryToken = uriQueryExpressionParser.ParseFilter(filter);
			BindingState bindingState = new BindingState(configuration);
			bindingState.ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable(elementType.ToTypeReference(), navigationSource);
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			if (this.applyClause != null)
			{
				bindingState.AggregatedPropertyNames = this.applyClause.GetLastAggregatedPropertyNames();
			}
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			FilterBinder filterBinder = new FilterBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind), bindingState);
			return filterBinder.BindFilter(queryToken);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00041604 File Offset: 0x0003F804
		private static ApplyClause ParseApplyImplementation(string apply, ODataUriParserConfiguration configuration, IEdmType elementType, IEdmNavigationSource navigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(elementType, "elementType");
			ExceptionUtils.CheckArgumentNotNull<string>(apply, "apply");
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(configuration.Settings.FilterLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier);
			IEnumerable<QueryToken> enumerable = uriQueryExpressionParser.ParseApply(apply);
			BindingState bindingState = new BindingState(configuration);
			bindingState.ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable(elementType.ToTypeReference(), navigationSource);
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			ApplyBinder applyBinder = new ApplyBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind), bindingState);
			return applyBinder.BindApply(enumerable);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x000416A4 File Offset: 0x0003F8A4
		private static SelectExpandClause ParseSelectAndExpandImplementation(string select, string expand, ODataUriParserConfiguration configuration, IEdmStructuredType elementType, IEdmNavigationSource navigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(configuration.Model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuredType>(elementType, "elementType");
			ExpandToken expandToken;
			SelectToken selectToken;
			SelectExpandSyntacticParser.Parse(select, expand, elementType, configuration, out expandToken, out selectToken);
			SelectExpandSemanticBinder selectExpandSemanticBinder = new SelectExpandSemanticBinder();
			return selectExpandSemanticBinder.Bind(elementType, navigationSource, expandToken, selectToken, configuration);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x000416F8 File Offset: 0x0003F8F8
		private OrderByClause ParseOrderByImplementation(string orderBy, ODataUriParserConfiguration configuration, IEdmType elementType, IEdmNavigationSource navigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(configuration.Model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(elementType, "elementType");
			ExceptionUtils.CheckArgumentNotNull<string>(orderBy, "orderBy");
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(configuration.Settings.OrderByLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier);
			IEnumerable<OrderByToken> enumerable = uriQueryExpressionParser.ParseOrderBy(orderBy);
			BindingState bindingState = new BindingState(configuration);
			bindingState.ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable(elementType.ToTypeReference(), navigationSource);
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			if (this.applyClause != null)
			{
				bindingState.AggregatedPropertyNames = this.applyClause.GetLastAggregatedPropertyNames();
			}
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			OrderByBinder orderByBinder = new OrderByBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind));
			return orderByBinder.BindOrderBy(bindingState, enumerable);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x000417C0 File Offset: 0x0003F9C0
		private static long? ParseTop(string topQuery)
		{
			if (topQuery == null)
			{
				return default(long?);
			}
			long num;
			if (!long.TryParse(topQuery, ref num) || num < 0L)
			{
				throw new ODataException(Strings.SyntacticTree_InvalidTopQueryOptionValue(topQuery));
			}
			return new long?(num);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x000417FC File Offset: 0x0003F9FC
		private static long? ParseSkip(string skipQuery)
		{
			if (skipQuery == null)
			{
				return default(long?);
			}
			long num;
			if (!long.TryParse(skipQuery, ref num) || num < 0L)
			{
				throw new ODataException(Strings.SyntacticTree_InvalidSkipQueryOptionValue(skipQuery));
			}
			return new long?(num);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00041838 File Offset: 0x0003FA38
		private static bool? ParseCount(string count)
		{
			if (count == null)
			{
				return default(bool?);
			}
			string text;
			if ((text = count.Trim()) != null)
			{
				if (text == "true")
				{
					return new bool?(true);
				}
				if (text == "false")
				{
					return new bool?(false);
				}
			}
			throw new ODataException(Strings.ODataUriParser_InvalidCount(count));
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00041894 File Offset: 0x0003FA94
		private static SearchClause ParseSearchImplementation(string search, ODataUriParserConfiguration configuration)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<string>(search, "search");
			SearchParser searchParser = new SearchParser(configuration.Settings.SearchLimit);
			QueryToken queryToken = searchParser.ParseSearch(search);
			BindingState bindingState = new BindingState(configuration);
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			SearchBinder searchBinder = new SearchBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind));
			return searchBinder.BindSearch(queryToken);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00041918 File Offset: 0x0003FB18
		private bool TryGetQueryOption(string queryOptionName, out string value)
		{
			if (!this.Resolver.EnableCaseInsensitive)
			{
				return this.queryOptions.TryGetValue(queryOptionName, ref value);
			}
			value = null;
			List<KeyValuePair<string, string>> list = Enumerable.ToList<KeyValuePair<string, string>>(Enumerable.Where<KeyValuePair<string, string>>(this.queryOptions, (KeyValuePair<string, string> pair) => string.Equals(queryOptionName, pair.Key, 5)));
			if (list.Count == 0)
			{
				return false;
			}
			if (list.Count == 1)
			{
				value = Enumerable.First<KeyValuePair<string, string>>(list).Value;
				return true;
			}
			throw new ODataException(Strings.QueryOptionUtils_QueryParameterMustBeSpecifiedOnce(queryOptionName));
		}

		// Token: 0x040007B8 RID: 1976
		private readonly IEdmType targetEdmType;

		// Token: 0x040007B9 RID: 1977
		private readonly IEdmNavigationSource targetNavigationSource;

		// Token: 0x040007BA RID: 1978
		private readonly IDictionary<string, string> queryOptions;

		// Token: 0x040007BB RID: 1979
		private FilterClause filterClause;

		// Token: 0x040007BC RID: 1980
		private SelectExpandClause selectExpandClause;

		// Token: 0x040007BD RID: 1981
		private OrderByClause orderByClause;

		// Token: 0x040007BE RID: 1982
		private SearchClause searchClause;

		// Token: 0x040007BF RID: 1983
		private ApplyClause applyClause;
	}
}
