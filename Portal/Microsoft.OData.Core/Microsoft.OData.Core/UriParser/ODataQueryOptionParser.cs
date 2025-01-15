using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200014A RID: 330
	public class ODataQueryOptionParser
	{
		// Token: 0x060010F1 RID: 4337 RVA: 0x0002FB86 File Offset: 0x0002DD86
		public ODataQueryOptionParser(IEdmModel model, IEdmType targetEdmType, IEdmNavigationSource targetNavigationSource, IDictionary<string, string> queryOptions)
			: this(model, targetEdmType, targetNavigationSource, queryOptions, null)
		{
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0002FB94 File Offset: 0x0002DD94
		public ODataQueryOptionParser(IEdmModel model, IEdmType targetEdmType, IEdmNavigationSource targetNavigationSource, IDictionary<string, string> queryOptions, IServiceProvider container)
		{
			ExceptionUtils.CheckArgumentNotNull<IDictionary<string, string>>(queryOptions, "queryOptions");
			this.odataPathInfo = new ODataPathInfo(targetEdmType, targetNavigationSource);
			this.targetEdmType = this.odataPathInfo.TargetEdmType;
			this.queryOptions = queryOptions;
			ODataUriParserConfiguration odataUriParserConfiguration = new ODataUriParserConfiguration(model, container);
			odataUriParserConfiguration.ParameterAliasValueAccessor = new ParameterAliasValueAccessor(queryOptions.Where((KeyValuePair<string, string> _) => _.Key.StartsWith("@", StringComparison.Ordinal)).ToDictionary((KeyValuePair<string, string> _) => _.Key, (KeyValuePair<string, string> _) => _.Value));
			this.Configuration = odataUriParserConfiguration;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0002FC5C File Offset: 0x0002DE5C
		public ODataQueryOptionParser(IEdmModel model, ODataPath odataPath, IDictionary<string, string> queryOptions)
			: this(model, odataPath, queryOptions, null)
		{
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0002FC68 File Offset: 0x0002DE68
		public ODataQueryOptionParser(IEdmModel model, ODataPath odataPath, IDictionary<string, string> queryOptions, IServiceProvider container)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPath>(odataPath, "odataPath");
			ExceptionUtils.CheckArgumentNotNull<IDictionary<string, string>>(queryOptions, "queryOptions");
			this.odataPathInfo = new ODataPathInfo(odataPath);
			this.targetEdmType = this.odataPathInfo.TargetEdmType;
			this.queryOptions = queryOptions;
			ODataUriParserConfiguration odataUriParserConfiguration = new ODataUriParserConfiguration(model, container);
			odataUriParserConfiguration.ParameterAliasValueAccessor = new ParameterAliasValueAccessor(queryOptions.Where((KeyValuePair<string, string> _) => _.Key.StartsWith("@", StringComparison.Ordinal)).ToDictionary((KeyValuePair<string, string> _) => _.Key, (KeyValuePair<string, string> _) => _.Value));
			this.Configuration = odataUriParserConfiguration;
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0002FD38 File Offset: 0x0002DF38
		public ODataUriParserSettings Settings
		{
			get
			{
				return this.Configuration.Settings;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0002FD45 File Offset: 0x0002DF45
		public IDictionary<string, SingleValueNode> ParameterAliasNodes
		{
			get
			{
				return this.Configuration.ParameterAliasValueAccessor.ParameterAliasValueNodesCached;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0002FD57 File Offset: 0x0002DF57
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x0002FD64 File Offset: 0x0002DF64
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

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x0002FD72 File Offset: 0x0002DF72
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x0002FD7A File Offset: 0x0002DF7A
		internal ODataUriParserConfiguration Configuration { get; set; }

		// Token: 0x060010FB RID: 4347 RVA: 0x0002FD84 File Offset: 0x0002DF84
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
			this.filterClause = this.ParseFilterImplementation(text, this.Configuration, this.odataPathInfo);
			return this.filterClause;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0002FDE0 File Offset: 0x0002DFE0
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
			this.applyClause = ODataQueryOptionParser.ParseApplyImplementation(text, this.Configuration, this.odataPathInfo);
			return this.applyClause;
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0002FE3C File Offset: 0x0002E03C
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
			if (!(this.targetEdmType is IEdmStructuredType))
			{
				throw new ODataException(Strings.UriParser_TypeInvalidForSelectExpand(this.targetEdmType));
			}
			this.selectExpandClause = this.ParseSelectAndExpandImplementation(text, text2, this.Configuration, this.odataPathInfo);
			return this.selectExpandClause;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0002FED4 File Offset: 0x0002E0D4
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
			this.orderByClause = this.ParseOrderByImplementation(text, this.Configuration, this.odataPathInfo);
			return this.orderByClause;
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0002FF30 File Offset: 0x0002E130
		public long? ParseTop()
		{
			string text;
			if (!this.TryGetQueryOption("$top", out text))
			{
				return null;
			}
			return ODataQueryOptionParser.ParseTop(text);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0002FF5C File Offset: 0x0002E15C
		public long? ParseSkip()
		{
			string text;
			if (!this.TryGetQueryOption("$skip", out text))
			{
				return null;
			}
			return ODataQueryOptionParser.ParseSkip(text);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0002FF88 File Offset: 0x0002E188
		public long? ParseIndex()
		{
			string text;
			if (!this.TryGetQueryOption("$index", out text))
			{
				return null;
			}
			return ODataQueryOptionParser.ParseIndex(text);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0002FFB4 File Offset: 0x0002E1B4
		public bool? ParseCount()
		{
			string text;
			if (!this.TryGetQueryOption("$count", out text))
			{
				return null;
			}
			return ODataQueryOptionParser.ParseCount(text);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0002FFE0 File Offset: 0x0002E1E0
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

		// Token: 0x06001104 RID: 4356 RVA: 0x00030028 File Offset: 0x0002E228
		public string ParseSkipToken()
		{
			string text;
			if (!this.TryGetQueryOption("$skiptoken", out text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00030048 File Offset: 0x0002E248
		public string ParseDeltaToken()
		{
			string text;
			if (!this.TryGetQueryOption("$deltatoken", out text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00030068 File Offset: 0x0002E268
		public ComputeClause ParseCompute()
		{
			if (this.computeClause != null)
			{
				return this.computeClause;
			}
			string text;
			if (!this.TryGetQueryOption("$compute", out text) || string.IsNullOrEmpty(text) || this.targetEdmType == null)
			{
				return null;
			}
			this.computeClause = this.ParseComputeImplementation(text, this.Configuration, this.odataPathInfo);
			return this.computeClause;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x000300C4 File Offset: 0x0002E2C4
		private FilterClause ParseFilterImplementation(string filter, ODataUriParserConfiguration configuration, ODataPathInfo odataPathInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<ODataPathInfo>(odataPathInfo, "odataPathInfo");
			ExceptionUtils.CheckArgumentNotNull<string>(filter, "filter");
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(configuration.Settings.FilterLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier);
			QueryToken queryToken = uriQueryExpressionParser.ParseFilter(filter);
			BindingState bindingState = this.CreateBindingState(configuration, odataPathInfo);
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			FilterBinder filterBinder = new FilterBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind), bindingState);
			return filterBinder.BindFilter(queryToken);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00030144 File Offset: 0x0002E344
		private static ApplyClause ParseApplyImplementation(string apply, ODataUriParserConfiguration configuration, ODataPathInfo odataPathInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<string>(apply, "apply");
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(configuration.Settings.FilterLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier);
			IEnumerable<QueryToken> enumerable = uriQueryExpressionParser.ParseApply(apply);
			BindingState bindingState = new BindingState(configuration, odataPathInfo.Segments.ToList<ODataPathSegment>());
			bindingState.ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable(odataPathInfo.TargetEdmType.ToTypeReference(), odataPathInfo.TargetNavigationSource);
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			ApplyBinder applyBinder = new ApplyBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind), bindingState, configuration, odataPathInfo);
			return applyBinder.BindApply(enumerable);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x000301F0 File Offset: 0x0002E3F0
		private SelectExpandClause ParseSelectAndExpandImplementation(string select, string expand, ODataUriParserConfiguration configuration, ODataPathInfo odataPathInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(configuration.Model, "model");
			ExpandToken expandToken;
			SelectToken selectToken;
			SelectExpandSyntacticParser.Parse(select, expand, odataPathInfo.TargetStructuredType, configuration, out expandToken, out selectToken);
			BindingState bindingState = this.CreateBindingState(configuration, odataPathInfo);
			return SelectExpandSemanticBinder.Bind(odataPathInfo, expandToken, selectToken, configuration, bindingState);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00030244 File Offset: 0x0002E444
		private OrderByClause ParseOrderByImplementation(string orderBy, ODataUriParserConfiguration configuration, ODataPathInfo odataPathInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(configuration.Model, "model");
			ExceptionUtils.CheckArgumentNotNull<string>(orderBy, "orderBy");
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(configuration.Settings.OrderByLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier);
			IEnumerable<OrderByToken> enumerable = uriQueryExpressionParser.ParseOrderBy(orderBy);
			BindingState bindingState = this.CreateBindingState(configuration, odataPathInfo);
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			OrderByBinder orderByBinder = new OrderByBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind));
			return orderByBinder.BindOrderBy(bindingState, enumerable);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x000302CC File Offset: 0x0002E4CC
		private BindingState CreateBindingState(ODataUriParserConfiguration configuration, ODataPathInfo odataPathInfo)
		{
			BindingState bindingState = new BindingState(configuration, odataPathInfo.Segments.ToList<ODataPathSegment>());
			bindingState.ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable(odataPathInfo.TargetEdmType.ToTypeReference(), odataPathInfo.TargetNavigationSource);
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			if (this.applyClause != null)
			{
				bindingState.AggregatedPropertyNames = this.applyClause.GetLastAggregatedPropertyNames();
				if (this.applyClause.Transformations.Any((TransformationNode x) => x.Kind == TransformationNodeKind.GroupBy || x.Kind == TransformationNodeKind.Aggregate))
				{
					bindingState.IsCollapsed = true;
				}
			}
			if (this.computeClause != null)
			{
				HashSet<EndPathToken> hashSet = new HashSet<EndPathToken>(this.computeClause.ComputedItems.Select((ComputeExpression i) => new EndPathToken(i.Alias, null)));
				if (bindingState.AggregatedPropertyNames == null)
				{
					bindingState.AggregatedPropertyNames = hashSet;
				}
				else
				{
					bindingState.AggregatedPropertyNames.UnionWith(hashSet);
				}
			}
			return bindingState;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x000303C4 File Offset: 0x0002E5C4
		private static long? ParseTop(string topQuery)
		{
			if (topQuery == null)
			{
				return null;
			}
			long num;
			if (!long.TryParse(topQuery, out num) || num < 0L)
			{
				throw new ODataException(Strings.SyntacticTree_InvalidTopQueryOptionValue(topQuery));
			}
			return new long?(num);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00030400 File Offset: 0x0002E600
		private static long? ParseSkip(string skipQuery)
		{
			if (skipQuery == null)
			{
				return null;
			}
			long num;
			if (!long.TryParse(skipQuery, out num) || num < 0L)
			{
				throw new ODataException(Strings.SyntacticTree_InvalidSkipQueryOptionValue(skipQuery));
			}
			return new long?(num);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0003043C File Offset: 0x0002E63C
		private static long? ParseIndex(string indexQuery)
		{
			if (indexQuery == null)
			{
				return null;
			}
			long num;
			if (!long.TryParse(indexQuery, out num))
			{
				throw new ODataException(Strings.SyntacticTree_InvalidIndexQueryOptionValue(indexQuery));
			}
			return new long?(num);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00030474 File Offset: 0x0002E674
		private static bool? ParseCount(string count)
		{
			if (count == null)
			{
				return null;
			}
			string text = count.Trim();
			if (text == "true")
			{
				return new bool?(true);
			}
			if (!(text == "false"))
			{
				throw new ODataException(Strings.ODataUriParser_InvalidCount(count));
			}
			return new bool?(false);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x000304CC File Offset: 0x0002E6CC
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

		// Token: 0x06001111 RID: 4369 RVA: 0x00030534 File Offset: 0x0002E734
		private ComputeClause ParseComputeImplementation(string compute, ODataUriParserConfiguration configuration, ODataPathInfo odataPathInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<string>(compute, "compute");
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(configuration.Settings.FilterLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier);
			ComputeToken computeToken = uriQueryExpressionParser.ParseCompute(compute);
			BindingState bindingState = this.CreateBindingState(configuration, odataPathInfo);
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			ComputeBinder computeBinder = new ComputeBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind));
			return computeBinder.BindCompute(computeToken);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x000305A8 File Offset: 0x0002E7A8
		private bool TryGetQueryOption(string name, out string value)
		{
			value = null;
			if (name == null)
			{
				return false;
			}
			string trimmedName = name.Trim();
			bool enableCaseInsensitive = this.Resolver.EnableCaseInsensitive;
			bool enableNoDollarQueryOptions = this.Configuration.EnableNoDollarQueryOptions;
			if (!enableCaseInsensitive && !enableNoDollarQueryOptions)
			{
				return this.queryOptions.TryGetValue(trimmedName, out value);
			}
			StringComparison stringComparison = (enableCaseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			string nameWithoutDollarPrefix = ((enableNoDollarQueryOptions && trimmedName.StartsWith("$", StringComparison.Ordinal)) ? trimmedName.Substring(1) : null);
			List<KeyValuePair<string, string>> list = this.queryOptions.Where((KeyValuePair<string, string> pair) => string.Equals(trimmedName, pair.Key, stringComparison) || (nameWithoutDollarPrefix != null && string.Equals(nameWithoutDollarPrefix, pair.Key, stringComparison))).ToList<KeyValuePair<string, string>>();
			if (list.Count == 0)
			{
				return false;
			}
			if (list.Count == 1)
			{
				value = list.First<KeyValuePair<string, string>>().Value;
				return true;
			}
			throw new ODataException(Strings.QueryOptionUtils_QueryParameterMustBeSpecifiedOnce(enableNoDollarQueryOptions ? string.Format(CultureInfo.InvariantCulture, "${0}/{0}", new object[] { nameWithoutDollarPrefix ?? trimmedName }) : trimmedName));
		}

		// Token: 0x040007DC RID: 2012
		private readonly IEdmType targetEdmType;

		// Token: 0x040007DD RID: 2013
		private readonly IDictionary<string, string> queryOptions;

		// Token: 0x040007DE RID: 2014
		private FilterClause filterClause;

		// Token: 0x040007DF RID: 2015
		private SelectExpandClause selectExpandClause;

		// Token: 0x040007E0 RID: 2016
		private OrderByClause orderByClause;

		// Token: 0x040007E1 RID: 2017
		private SearchClause searchClause;

		// Token: 0x040007E2 RID: 2018
		private ApplyClause applyClause;

		// Token: 0x040007E3 RID: 2019
		private ComputeClause computeClause;

		// Token: 0x040007E4 RID: 2020
		private ODataPathInfo odataPathInfo;
	}
}
