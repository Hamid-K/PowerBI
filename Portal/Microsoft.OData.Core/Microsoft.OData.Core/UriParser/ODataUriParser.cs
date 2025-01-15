using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200014C RID: 332
	public sealed class ODataUriParser
	{
		// Token: 0x0600111C RID: 4380 RVA: 0x00030703 File Offset: 0x0002E903
		public ODataUriParser(IEdmModel model, Uri serviceRoot, Uri uri)
			: this(model, serviceRoot, uri, null)
		{
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00030710 File Offset: 0x0002E910
		public ODataUriParser(IEdmModel model, Uri serviceRoot, Uri uri, IServiceProvider container)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(uri, "uri");
			if (serviceRoot == null)
			{
				throw new ODataException(Strings.UriParser_NeedServiceRootForThisOverload);
			}
			if (!serviceRoot.IsAbsoluteUri)
			{
				throw new ODataException(Strings.UriParser_UriMustBeAbsolute(serviceRoot));
			}
			this.configuration = new ODataUriParserConfiguration(model, container);
			this.serviceRoot = UriUtils.EnsureTaillingSlash(serviceRoot);
			this.uri = (uri.IsAbsoluteUri ? uri : UriUtils.UriToAbsoluteUri(this.ServiceRoot, uri));
			this.queryOptions = QueryOptionUtils.ParseQueryOptions(this.uri);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0003079F File Offset: 0x0002E99F
		public ODataUriParser(IEdmModel model, Uri relativeUri)
			: this(model, relativeUri, null)
		{
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x000307AC File Offset: 0x0002E9AC
		public ODataUriParser(IEdmModel model, Uri relativeUri, IServiceProvider container)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(relativeUri, "relativeUri");
			if (relativeUri.IsAbsoluteUri)
			{
				throw new ODataException(Strings.UriParser_RelativeUriMustBeRelative);
			}
			this.configuration = new ODataUriParserConfiguration(model, container);
			this.uri = relativeUri;
			this.queryOptions = QueryOptionUtils.ParseQueryOptions(UriUtils.CreateMockAbsoluteUri(this.uri));
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x00030808 File Offset: 0x0002EA08
		public ODataUriParserSettings Settings
		{
			get
			{
				return this.configuration.Settings;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x00030815 File Offset: 0x0002EA15
		public IEdmModel Model
		{
			get
			{
				return this.configuration.Model;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x00030822 File Offset: 0x0002EA22
		public IServiceProvider Container
		{
			get
			{
				return this.configuration.Container;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x0003082F File Offset: 0x0002EA2F
		public Uri ServiceRoot
		{
			get
			{
				return this.serviceRoot;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00030837 File Offset: 0x0002EA37
		// (set) Token: 0x06001125 RID: 4389 RVA: 0x00030844 File Offset: 0x0002EA44
		public ODataUrlKeyDelimiter UrlKeyDelimiter
		{
			get
			{
				return this.configuration.UrlKeyDelimiter;
			}
			set
			{
				this.configuration.UrlKeyDelimiter = value;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x00030852 File Offset: 0x0002EA52
		// (set) Token: 0x06001127 RID: 4391 RVA: 0x0003085F File Offset: 0x0002EA5F
		public Func<string, BatchReferenceSegment> BatchReferenceCallback
		{
			get
			{
				return this.configuration.BatchReferenceCallback;
			}
			set
			{
				this.configuration.BatchReferenceCallback = value;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x0003086D File Offset: 0x0002EA6D
		// (set) Token: 0x06001129 RID: 4393 RVA: 0x0003087A File Offset: 0x0002EA7A
		public bool EnableNoDollarQueryOptions
		{
			get
			{
				return this.configuration.EnableNoDollarQueryOptions;
			}
			set
			{
				this.configuration.EnableNoDollarQueryOptions = value;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x00030888 File Offset: 0x0002EA88
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x00030895 File Offset: 0x0002EA95
		public bool EnableUriTemplateParsing
		{
			get
			{
				return this.configuration.EnableUriTemplateParsing;
			}
			set
			{
				this.configuration.EnableUriTemplateParsing = value;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x000308A3 File Offset: 0x0002EAA3
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x000308B0 File Offset: 0x0002EAB0
		public ODataUriResolver Resolver
		{
			get
			{
				return this.configuration.Resolver;
			}
			set
			{
				this.configuration.Resolver = value;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x000308BE File Offset: 0x0002EABE
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x000308CB File Offset: 0x0002EACB
		public ParseDynamicPathSegment ParseDynamicPathSegmentFunc
		{
			get
			{
				return this.configuration.ParseDynamicPathSegmentFunc;
			}
			set
			{
				this.configuration.ParseDynamicPathSegmentFunc = value;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x000308D9 File Offset: 0x0002EAD9
		public IDictionary<string, SingleValueNode> ParameterAliasNodes
		{
			get
			{
				if (this.ParameterAliasValueAccessor == null)
				{
					this.Initialize();
				}
				return this.ParameterAliasValueAccessor.ParameterAliasValueNodesCached;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x000308F4 File Offset: 0x0002EAF4
		public IList<KeyValuePair<string, string>> CustomQueryOptions
		{
			get
			{
				if (this.customQueryOptions == null)
				{
					this.InitQueryOptionDic();
				}
				return this.customQueryOptions;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x0003090A File Offset: 0x0002EB0A
		// (set) Token: 0x06001133 RID: 4403 RVA: 0x00030917 File Offset: 0x0002EB17
		internal ParameterAliasValueAccessor ParameterAliasValueAccessor
		{
			get
			{
				return this.configuration.ParameterAliasValueAccessor;
			}
			set
			{
				this.configuration.ParameterAliasValueAccessor = value;
			}
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00030925 File Offset: 0x0002EB25
		public ODataPath ParsePath()
		{
			this.Initialize();
			return this.odataPath;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00030933 File Offset: 0x0002EB33
		public FilterClause ParseFilter()
		{
			this.Initialize();
			return this.queryOptionParser.ParseFilter();
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00030946 File Offset: 0x0002EB46
		public OrderByClause ParseOrderBy()
		{
			this.Initialize();
			return this.queryOptionParser.ParseOrderBy();
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00030959 File Offset: 0x0002EB59
		public SelectExpandClause ParseSelectAndExpand()
		{
			this.Initialize();
			return this.queryOptionParser.ParseSelectAndExpand();
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0003096C File Offset: 0x0002EB6C
		public EntityIdSegment ParseEntityId()
		{
			if (this.entityIdSegment != null)
			{
				return this.entityIdSegment;
			}
			this.InitQueryOptionDic();
			string text = null;
			if (!this.queryOptionDic.TryGetValue("$id", out text) && !this.Resolver.EnableCaseInsensitive)
			{
				return null;
			}
			if (text == null && this.Resolver.EnableCaseInsensitive)
			{
				List<KeyValuePair<string, string>> list = this.queryOptionDic.Where((KeyValuePair<string, string> pair) => string.Equals("$id", pair.Key, StringComparison.OrdinalIgnoreCase)).ToList<KeyValuePair<string, string>>();
				if (list.Count == 0)
				{
					return null;
				}
				if (list.Count != 1)
				{
					throw new ODataException(Strings.QueryOptionUtils_QueryParameterMustBeSpecifiedOnce("$id"));
				}
				text = list.First<KeyValuePair<string, string>>().Value;
			}
			Uri uri = new Uri(text, UriKind.RelativeOrAbsolute);
			if (!uri.IsAbsoluteUri)
			{
				if (!this.uri.IsAbsoluteUri)
				{
					Uri uri2 = UriUtils.CreateMockAbsoluteUri(null);
					Uri uri3 = new Uri(UriUtils.CreateMockAbsoluteUri(this.uri), uri);
					uri = uri2.MakeRelativeUri(uri3);
				}
				else
				{
					uri = new Uri(this.uri, uri);
				}
			}
			this.entityIdSegment = new EntityIdSegment(uri);
			return this.entityIdSegment;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00030A8A File Offset: 0x0002EC8A
		public long? ParseTop()
		{
			this.Initialize();
			return this.queryOptionParser.ParseTop();
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00030A9D File Offset: 0x0002EC9D
		public long? ParseSkip()
		{
			this.Initialize();
			return this.queryOptionParser.ParseSkip();
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00030AB0 File Offset: 0x0002ECB0
		public long? ParseIndex()
		{
			this.Initialize();
			return this.queryOptionParser.ParseIndex();
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00030AC3 File Offset: 0x0002ECC3
		public bool? ParseCount()
		{
			this.Initialize();
			return this.queryOptionParser.ParseCount();
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00030AD6 File Offset: 0x0002ECD6
		public SearchClause ParseSearch()
		{
			this.Initialize();
			return this.queryOptionParser.ParseSearch();
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00030AE9 File Offset: 0x0002ECE9
		public ApplyClause ParseApply()
		{
			this.Initialize();
			return this.queryOptionParser.ParseApply();
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00030AFC File Offset: 0x0002ECFC
		public string ParseSkipToken()
		{
			this.Initialize();
			return this.queryOptionParser.ParseSkipToken();
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00030B0F File Offset: 0x0002ED0F
		public string ParseDeltaToken()
		{
			this.Initialize();
			return this.queryOptionParser.ParseDeltaToken();
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00030B22 File Offset: 0x0002ED22
		public ComputeClause ParseCompute()
		{
			this.Initialize();
			return this.queryOptionParser.ParseCompute();
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00030B38 File Offset: 0x0002ED38
		public ODataUri ParseUri()
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(this.configuration.Model, "model");
			ExceptionUtils.CheckArgumentNotNull<Uri>(this.uri, "uri");
			ODataPath odataPath = this.ParsePath();
			SelectExpandClause selectExpandClause = this.ParseSelectAndExpand();
			FilterClause filterClause = this.ParseFilter();
			OrderByClause orderByClause = this.ParseOrderBy();
			SearchClause searchClause = this.ParseSearch();
			ApplyClause applyClause = this.ParseApply();
			ComputeClause computeClause = this.ParseCompute();
			long? num = this.ParseTop();
			long? num2 = this.ParseSkip();
			long? num3 = this.ParseIndex();
			bool? flag = this.ParseCount();
			string text = this.ParseSkipToken();
			string text2 = this.ParseDeltaToken();
			List<QueryNode> list = new List<QueryNode>();
			return new ODataUri(this.ParameterAliasValueAccessor, odataPath, list, selectExpandClause, filterClause, orderByClause, searchClause, applyClause, num2, num, num3, flag, computeClause)
			{
				ServiceRoot = this.serviceRoot,
				SkipToken = text,
				DeltaToken = text2
			};
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00030C1C File Offset: 0x0002EE1C
		private ODataPath ParsePathImplementation()
		{
			Uri uri = this.uri;
			ExceptionUtils.CheckArgumentNotNull<Uri>(uri, "pathUri");
			UriPathParser uriPathParser;
			if (this.Container == null)
			{
				uriPathParser = new UriPathParser(this.Settings);
			}
			else
			{
				uriPathParser = this.Container.GetService<UriPathParser>();
			}
			ICollection<string> collection = uriPathParser.ParsePathIntoSegments(uri, this.ServiceRoot);
			return ODataPathFactory.BindPath(collection, this.configuration);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00030C7C File Offset: 0x0002EE7C
		private void Initialize()
		{
			if (this.odataPath != null)
			{
				return;
			}
			if (this.ParameterAliasValueAccessor == null)
			{
				this.ParameterAliasValueAccessor = new ParameterAliasValueAccessor(this.queryOptions.GetParameterAliases());
			}
			this.odataPath = this.ParsePathImplementation();
			this.InitQueryOptionDic();
			this.queryOptionParser = new ODataQueryOptionParser(this.Model, this.odataPath, this.queryOptionDic)
			{
				Configuration = this.configuration
			};
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00030CEC File Offset: 0x0002EEEC
		private void InitQueryOptionDic()
		{
			if (this.queryOptionDic != null)
			{
				return;
			}
			this.queryOptionDic = new Dictionary<string, string>(StringComparer.Ordinal);
			this.customQueryOptions = new List<KeyValuePair<string, string>>();
			if (this.queryOptions != null)
			{
				foreach (CustomQueryOptionToken customQueryOptionToken in this.queryOptions)
				{
					string name = customQueryOptionToken.Name;
					if (name != null)
					{
						string text = ((this.EnableNoDollarQueryOptions && !customQueryOptionToken.Name.StartsWith("$", StringComparison.Ordinal)) ? ("$" + name) : name);
						if (this.IsODataQueryOption(text))
						{
							if (this.queryOptionDic.ContainsKey(text))
							{
								throw new ODataException(Strings.QueryOptionUtils_QueryParameterMustBeSpecifiedOnce(this.EnableNoDollarQueryOptions ? string.Format(CultureInfo.InvariantCulture, "${0}/{0}", new object[] { text.TrimStart(new char[] { '$' }) }) : text));
							}
							this.queryOptionDic.Add(text, customQueryOptionToken.Value);
						}
						else
						{
							this.customQueryOptions.Add(new KeyValuePair<string, string>(name, customQueryOptionToken.Value));
						}
					}
				}
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00030E34 File Offset: 0x0002F034
		private bool IsODataQueryOption(string optionName)
		{
			string text = (this.Resolver.EnableCaseInsensitive ? optionName.ToLowerInvariant() : optionName);
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1805205693U)
			{
				if (num <= 789605668U)
				{
					if (num != 307062270U)
					{
						if (num != 456103474U)
						{
							if (num != 789605668U)
							{
								return false;
							}
							if (!(text == "$orderby"))
							{
								return false;
							}
						}
						else if (!(text == "$count"))
						{
							return false;
						}
					}
					else if (!(text == "$id"))
					{
						return false;
					}
				}
				else if (num <= 1211134378U)
				{
					if (num != 1171897744U)
					{
						if (num != 1211134378U)
						{
							return false;
						}
						if (!(text == "$top"))
						{
							return false;
						}
					}
					else if (!(text == "$deltatoken"))
					{
						return false;
					}
				}
				else if (num != 1469037347U)
				{
					if (num != 1805205693U)
					{
						return false;
					}
					if (!(text == "$skiptoken"))
					{
						return false;
					}
				}
				else if (!(text == "$select"))
				{
					return false;
				}
			}
			else if (num <= 2491595753U)
			{
				if (num <= 2221740796U)
				{
					if (num != 2110202789U)
					{
						if (num != 2221740796U)
						{
							return false;
						}
						if (!(text == "$compute"))
						{
							return false;
						}
					}
					else if (!(text == "$apply"))
					{
						return false;
					}
				}
				else if (num != 2272632476U)
				{
					if (num != 2491595753U)
					{
						return false;
					}
					if (!(text == "$index"))
					{
						return false;
					}
				}
				else if (!(text == "$format"))
				{
					return false;
				}
			}
			else if (num <= 3803867261U)
			{
				if (num != 2649853531U)
				{
					if (num != 3803867261U)
					{
						return false;
					}
					if (!(text == "$filter"))
					{
						return false;
					}
				}
				else if (!(text == "$expand"))
				{
					return false;
				}
			}
			else if (num != 3922661099U)
			{
				if (num != 4027612776U)
				{
					return false;
				}
				if (!(text == "$skip"))
				{
					return false;
				}
			}
			else if (!(text == "$search"))
			{
				return false;
			}
			return true;
		}

		// Token: 0x040007E9 RID: 2025
		private readonly ODataUriParserConfiguration configuration;

		// Token: 0x040007EA RID: 2026
		private readonly Uri serviceRoot;

		// Token: 0x040007EB RID: 2027
		private readonly Uri uri;

		// Token: 0x040007EC RID: 2028
		private readonly List<CustomQueryOptionToken> queryOptions;

		// Token: 0x040007ED RID: 2029
		private IDictionary<string, string> queryOptionDic;

		// Token: 0x040007EE RID: 2030
		private IList<KeyValuePair<string, string>> customQueryOptions;

		// Token: 0x040007EF RID: 2031
		private ODataQueryOptionParser queryOptionParser;

		// Token: 0x040007F0 RID: 2032
		private ODataPath odataPath;

		// Token: 0x040007F1 RID: 2033
		private EntityIdSegment entityIdSegment;
	}
}
