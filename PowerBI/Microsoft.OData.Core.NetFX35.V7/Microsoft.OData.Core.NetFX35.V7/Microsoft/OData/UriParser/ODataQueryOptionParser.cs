using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000106 RID: 262
	public class ODataQueryOptionParser
	{
		// Token: 0x06000C3F RID: 3135 RVA: 0x0002185B File Offset: 0x0001FA5B
		public ODataQueryOptionParser(IEdmModel model, IEdmType targetEdmType, IEdmNavigationSource targetNavigationSource, IDictionary<string, string> queryOptions)
			: this(model, targetEdmType, targetNavigationSource, queryOptions, null)
		{
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0002186C File Offset: 0x0001FA6C
		public ODataQueryOptionParser(IEdmModel model, IEdmType targetEdmType, IEdmNavigationSource targetNavigationSource, IDictionary<string, string> queryOptions, IServiceProvider container)
		{
			ExceptionUtils.CheckArgumentNotNull<IDictionary<string, string>>(queryOptions, "queryOptions");
			this.odataPathInfo = new ODataPathInfo(targetEdmType, targetNavigationSource);
			this.targetEdmType = this.odataPathInfo.TargetEdmType;
			this.queryOptions = queryOptions;
			ODataUriParserConfiguration odataUriParserConfiguration = new ODataUriParserConfiguration(model, container);
			odataUriParserConfiguration.ParameterAliasValueAccessor = new ParameterAliasValueAccessor(Enumerable.ToDictionary<KeyValuePair<string, string>, string, string>(Enumerable.Where<KeyValuePair<string, string>>(queryOptions, (KeyValuePair<string, string> _) => _.Key.StartsWith("@", 4)), (KeyValuePair<string, string> _) => _.Key, (KeyValuePair<string, string> _) => _.Value));
			this.Configuration = odataUriParserConfiguration;
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00021934 File Offset: 0x0001FB34
		public ODataQueryOptionParser(IEdmModel model, ODataPath odataPath, IDictionary<string, string> queryOptions)
			: this(model, odataPath, queryOptions, null)
		{
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00021940 File Offset: 0x0001FB40
		public ODataQueryOptionParser(IEdmModel model, ODataPath odataPath, IDictionary<string, string> queryOptions, IServiceProvider container)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPath>(odataPath, "odataPath");
			ExceptionUtils.CheckArgumentNotNull<IDictionary<string, string>>(queryOptions, "queryOptions");
			this.odataPathInfo = new ODataPathInfo(odataPath);
			this.targetEdmType = this.odataPathInfo.TargetEdmType;
			this.queryOptions = queryOptions;
			ODataUriParserConfiguration odataUriParserConfiguration = new ODataUriParserConfiguration(model, container);
			odataUriParserConfiguration.ParameterAliasValueAccessor = new ParameterAliasValueAccessor(Enumerable.ToDictionary<KeyValuePair<string, string>, string, string>(Enumerable.Where<KeyValuePair<string, string>>(queryOptions, (KeyValuePair<string, string> _) => _.Key.StartsWith("@", 4)), (KeyValuePair<string, string> _) => _.Key, (KeyValuePair<string, string> _) => _.Value));
			this.Configuration = odataUriParserConfiguration;
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x00021A10 File Offset: 0x0001FC10
		public ODataUriParserSettings Settings
		{
			get
			{
				return this.Configuration.Settings;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00021A1D File Offset: 0x0001FC1D
		public IDictionary<string, SingleValueNode> ParameterAliasNodes
		{
			get
			{
				return this.Configuration.ParameterAliasValueAccessor.ParameterAliasValueNodesCached;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x00021A2F File Offset: 0x0001FC2F
		// (set) Token: 0x06000C46 RID: 3142 RVA: 0x00021A3C File Offset: 0x0001FC3C
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

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00021A4A File Offset: 0x0001FC4A
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x00021A52 File Offset: 0x0001FC52
		internal ODataUriParserConfiguration Configuration { get; set; }

		// Token: 0x06000C49 RID: 3145 RVA: 0x00021A5C File Offset: 0x0001FC5C
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

		// Token: 0x06000C4A RID: 3146 RVA: 0x00021AB8 File Offset: 0x0001FCB8
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

		// Token: 0x06000C4B RID: 3147 RVA: 0x00021B14 File Offset: 0x0001FD14
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
			this.selectExpandClause = ODataQueryOptionParser.ParseSelectAndExpandImplementation(text, text2, this.Configuration, this.odataPathInfo);
			return this.selectExpandClause;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00021BA8 File Offset: 0x0001FDA8
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

		// Token: 0x06000C4D RID: 3149 RVA: 0x00021C04 File Offset: 0x0001FE04
		public long? ParseTop()
		{
			string text;
			if (!this.TryGetQueryOption("$top", out text))
			{
				return default(long?);
			}
			return ODataQueryOptionParser.ParseTop(text);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00021C30 File Offset: 0x0001FE30
		public long? ParseSkip()
		{
			string text;
			if (!this.TryGetQueryOption("$skip", out text))
			{
				return default(long?);
			}
			return ODataQueryOptionParser.ParseSkip(text);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00021C5C File Offset: 0x0001FE5C
		public bool? ParseCount()
		{
			string text;
			if (!this.TryGetQueryOption("$count", out text))
			{
				return default(bool?);
			}
			return ODataQueryOptionParser.ParseCount(text);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00021C88 File Offset: 0x0001FE88
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

		// Token: 0x06000C51 RID: 3153 RVA: 0x00021CD0 File Offset: 0x0001FED0
		public string ParseSkipToken()
		{
			string text;
			if (!this.TryGetQueryOption("$skiptoken", out text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00021CF0 File Offset: 0x0001FEF0
		public string ParseDeltaToken()
		{
			string text;
			if (!this.TryGetQueryOption("$deltatoken", out text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00021D10 File Offset: 0x0001FF10
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
			this.computeClause = ODataQueryOptionParser.ParseComputeImplementation(text, this.Configuration, this.odataPathInfo);
			return this.computeClause;
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00021D6C File Offset: 0x0001FF6C
		private FilterClause ParseFilterImplementation(string filter, ODataUriParserConfiguration configuration, ODataPathInfo odataPathInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<ODataPathInfo>(odataPathInfo, "odataPathInfo");
			ExceptionUtils.CheckArgumentNotNull<string>(filter, "filter");
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(configuration.Settings.FilterLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier);
			QueryToken queryToken = uriQueryExpressionParser.ParseFilter(filter);
			BindingState bindingState = new BindingState(configuration, Enumerable.ToList<ODataPathSegment>(odataPathInfo.Segments));
			bindingState.ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable(odataPathInfo.TargetEdmType.ToTypeReference(), odataPathInfo.TargetNavigationSource);
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			if (this.applyClause != null)
			{
				bindingState.AggregatedPropertyNames = this.applyClause.GetLastAggregatedPropertyNames();
			}
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			FilterBinder filterBinder = new FilterBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind), bindingState);
			return filterBinder.BindFilter(queryToken);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00021E3C File Offset: 0x0002003C
		private static ApplyClause ParseApplyImplementation(string apply, ODataUriParserConfiguration configuration, ODataPathInfo odataPathInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<string>(apply, "apply");
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(configuration.Settings.FilterLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier);
			IEnumerable<QueryToken> enumerable = uriQueryExpressionParser.ParseApply(apply);
			BindingState bindingState = new BindingState(configuration, Enumerable.ToList<ODataPathSegment>(odataPathInfo.Segments));
			bindingState.ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable(odataPathInfo.TargetEdmType.ToTypeReference(), odataPathInfo.TargetNavigationSource);
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			ApplyBinder applyBinder = new ApplyBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind), bindingState);
			return applyBinder.BindApply(enumerable);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00021EE8 File Offset: 0x000200E8
		private static SelectExpandClause ParseSelectAndExpandImplementation(string select, string expand, ODataUriParserConfiguration configuration, ODataPathInfo odataPathInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(configuration.Model, "model");
			ExpandToken expandToken;
			SelectToken selectToken;
			SelectExpandSyntacticParser.Parse(select, expand, odataPathInfo.TargetStructuredType, configuration, out expandToken, out selectToken);
			return SelectExpandSemanticBinder.Bind(odataPathInfo, expandToken, selectToken, configuration);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00021F30 File Offset: 0x00020130
		private OrderByClause ParseOrderByImplementation(string orderBy, ODataUriParserConfiguration configuration, ODataPathInfo odataPathInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(configuration.Model, "model");
			ExceptionUtils.CheckArgumentNotNull<string>(orderBy, "orderBy");
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(configuration.Settings.OrderByLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier);
			IEnumerable<OrderByToken> enumerable = uriQueryExpressionParser.ParseOrderBy(orderBy);
			BindingState bindingState = new BindingState(configuration, Enumerable.ToList<ODataPathSegment>(odataPathInfo.Segments));
			bindingState.ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable(odataPathInfo.TargetEdmType.ToTypeReference(), odataPathInfo.TargetNavigationSource);
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			if (this.applyClause != null)
			{
				bindingState.AggregatedPropertyNames = this.applyClause.GetLastAggregatedPropertyNames();
			}
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			OrderByBinder orderByBinder = new OrderByBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind));
			return orderByBinder.BindOrderBy(bindingState, enumerable);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00022004 File Offset: 0x00020204
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

		// Token: 0x06000C59 RID: 3161 RVA: 0x00022040 File Offset: 0x00020240
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

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002207C File Offset: 0x0002027C
		private static bool? ParseCount(string count)
		{
			if (count == null)
			{
				return default(bool?);
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

		// Token: 0x06000C5B RID: 3163 RVA: 0x000220D4 File Offset: 0x000202D4
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

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002213C File Offset: 0x0002033C
		private static ComputeClause ParseComputeImplementation(string compute, ODataUriParserConfiguration configuration, ODataPathInfo odataPathInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			ExceptionUtils.CheckArgumentNotNull<string>(compute, "compute");
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(configuration.Settings.FilterLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier);
			ComputeToken computeToken = uriQueryExpressionParser.ParseCompute(compute);
			BindingState bindingState = new BindingState(configuration);
			bindingState.ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable(odataPathInfo.TargetEdmType.ToTypeReference(), odataPathInfo.TargetNavigationSource);
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			ComputeBinder computeBinder = new ComputeBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind));
			return computeBinder.BindCompute(computeToken);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x000221DC File Offset: 0x000203DC
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
				return this.queryOptions.TryGetValue(trimmedName, ref value);
			}
			StringComparison stringComparison = (enableCaseInsensitive ? 5 : 4);
			string nameWithoutDollarPrefix = ((enableNoDollarQueryOptions && trimmedName.StartsWith("$", 4)) ? trimmedName.Substring(1) : null);
			List<KeyValuePair<string, string>> list = Enumerable.ToList<KeyValuePair<string, string>>(Enumerable.Where<KeyValuePair<string, string>>(this.queryOptions, (KeyValuePair<string, string> pair) => string.Equals(trimmedName, pair.Key, stringComparison) || (nameWithoutDollarPrefix != null && string.Equals(nameWithoutDollarPrefix, pair.Key, stringComparison))));
			if (list.Count == 0)
			{
				return false;
			}
			if (list.Count == 1)
			{
				value = Enumerable.First<KeyValuePair<string, string>>(list).Value;
				return true;
			}
			throw new ODataException(Strings.QueryOptionUtils_QueryParameterMustBeSpecifiedOnce(enableNoDollarQueryOptions ? string.Format(CultureInfo.InvariantCulture, "${0}/{0}", new object[] { nameWithoutDollarPrefix ?? trimmedName }) : trimmedName));
		}

		// Token: 0x040006BD RID: 1725
		private readonly IEdmType targetEdmType;

		// Token: 0x040006BE RID: 1726
		private readonly IDictionary<string, string> queryOptions;

		// Token: 0x040006BF RID: 1727
		private FilterClause filterClause;

		// Token: 0x040006C0 RID: 1728
		private SelectExpandClause selectExpandClause;

		// Token: 0x040006C1 RID: 1729
		private OrderByClause orderByClause;

		// Token: 0x040006C2 RID: 1730
		private SearchClause searchClause;

		// Token: 0x040006C3 RID: 1731
		private ApplyClause applyClause;

		// Token: 0x040006C4 RID: 1732
		private ComputeClause computeClause;

		// Token: 0x040006C5 RID: 1733
		private ODataPathInfo odataPathInfo;
	}
}
