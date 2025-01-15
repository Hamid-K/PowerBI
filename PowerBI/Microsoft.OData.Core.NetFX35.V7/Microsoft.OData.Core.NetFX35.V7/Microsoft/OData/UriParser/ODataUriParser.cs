using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000108 RID: 264
	public sealed class ODataUriParser
	{
		// Token: 0x06000C68 RID: 3176 RVA: 0x00022337 File Offset: 0x00020537
		public ODataUriParser(IEdmModel model, Uri serviceRoot, Uri uri)
			: this(model, serviceRoot, uri, null)
		{
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00022344 File Offset: 0x00020544
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

		// Token: 0x06000C6A RID: 3178 RVA: 0x000223D3 File Offset: 0x000205D3
		public ODataUriParser(IEdmModel model, Uri relativeUri)
			: this(model, relativeUri, null)
		{
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x000223E0 File Offset: 0x000205E0
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

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x0002243C File Offset: 0x0002063C
		public ODataUriParserSettings Settings
		{
			get
			{
				return this.configuration.Settings;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x00022449 File Offset: 0x00020649
		public IEdmModel Model
		{
			get
			{
				return this.configuration.Model;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00022456 File Offset: 0x00020656
		public IServiceProvider Container
		{
			get
			{
				return this.configuration.Container;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00022463 File Offset: 0x00020663
		public Uri ServiceRoot
		{
			get
			{
				return this.serviceRoot;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0002246B File Offset: 0x0002066B
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x00022478 File Offset: 0x00020678
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

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x00022486 File Offset: 0x00020686
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x00022493 File Offset: 0x00020693
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

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x000224A1 File Offset: 0x000206A1
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x000224AE File Offset: 0x000206AE
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

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x000224BC File Offset: 0x000206BC
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x000224C9 File Offset: 0x000206C9
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

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x000224D7 File Offset: 0x000206D7
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x000224E4 File Offset: 0x000206E4
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

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x000224F2 File Offset: 0x000206F2
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x000224FF File Offset: 0x000206FF
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

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0002250D File Offset: 0x0002070D
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

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x00022528 File Offset: 0x00020728
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

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0002253E File Offset: 0x0002073E
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x0002254B File Offset: 0x0002074B
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

		// Token: 0x06000C80 RID: 3200 RVA: 0x00022559 File Offset: 0x00020759
		public ODataPath ParsePath()
		{
			this.Initialize();
			return this.odataPath;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00022567 File Offset: 0x00020767
		public FilterClause ParseFilter()
		{
			this.Initialize();
			return this.queryOptionParser.ParseFilter();
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0002257A File Offset: 0x0002077A
		public OrderByClause ParseOrderBy()
		{
			this.Initialize();
			return this.queryOptionParser.ParseOrderBy();
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002258D File Offset: 0x0002078D
		public SelectExpandClause ParseSelectAndExpand()
		{
			this.Initialize();
			return this.queryOptionParser.ParseSelectAndExpand();
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x000225A0 File Offset: 0x000207A0
		public EntityIdSegment ParseEntityId()
		{
			if (this.entityIdSegment != null)
			{
				return this.entityIdSegment;
			}
			this.InitQueryOptionDic();
			string text = null;
			if (!this.Resolver.EnableCaseInsensitive)
			{
				if (!this.queryOptionDic.TryGetValue("$id", ref text))
				{
					return null;
				}
			}
			else
			{
				List<KeyValuePair<string, string>> list = Enumerable.ToList<KeyValuePair<string, string>>(Enumerable.Where<KeyValuePair<string, string>>(this.queryOptionDic, (KeyValuePair<string, string> pair) => string.Equals("$id", pair.Key, 5)));
				if (list.Count == 0)
				{
					return null;
				}
				if (list.Count != 1)
				{
					throw new ODataException(Strings.QueryOptionUtils_QueryParameterMustBeSpecifiedOnce("$id"));
				}
				text = Enumerable.First<KeyValuePair<string, string>>(list).Value;
			}
			Uri uri = new Uri(text, 0);
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

		// Token: 0x06000C85 RID: 3205 RVA: 0x000226AE File Offset: 0x000208AE
		public long? ParseTop()
		{
			this.Initialize();
			return this.queryOptionParser.ParseTop();
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x000226C1 File Offset: 0x000208C1
		public long? ParseSkip()
		{
			this.Initialize();
			return this.queryOptionParser.ParseSkip();
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x000226D4 File Offset: 0x000208D4
		public bool? ParseCount()
		{
			this.Initialize();
			return this.queryOptionParser.ParseCount();
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x000226E7 File Offset: 0x000208E7
		public SearchClause ParseSearch()
		{
			this.Initialize();
			return this.queryOptionParser.ParseSearch();
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x000226FA File Offset: 0x000208FA
		public ApplyClause ParseApply()
		{
			this.Initialize();
			return this.queryOptionParser.ParseApply();
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002270D File Offset: 0x0002090D
		public string ParseSkipToken()
		{
			this.Initialize();
			return this.queryOptionParser.ParseSkipToken();
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00022720 File Offset: 0x00020920
		public string ParseDeltaToken()
		{
			this.Initialize();
			return this.queryOptionParser.ParseDeltaToken();
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00022733 File Offset: 0x00020933
		public ComputeClause ParseCompute()
		{
			this.Initialize();
			return this.queryOptionParser.ParseCompute();
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00022748 File Offset: 0x00020948
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
			bool? flag = this.ParseCount();
			string text = this.ParseSkipToken();
			string text2 = this.ParseDeltaToken();
			List<QueryNode> list = new List<QueryNode>();
			return new ODataUri(this.ParameterAliasValueAccessor, odataPath, list, selectExpandClause, filterClause, orderByClause, searchClause, applyClause, num2, num, flag, computeClause)
			{
				ServiceRoot = this.serviceRoot,
				SkipToken = text,
				DeltaToken = text2
			};
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00022820 File Offset: 0x00020A20
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

		// Token: 0x06000C8F RID: 3215 RVA: 0x00022880 File Offset: 0x00020A80
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

		// Token: 0x06000C90 RID: 3216 RVA: 0x000228F0 File Offset: 0x00020AF0
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
						string text = ((this.EnableNoDollarQueryOptions && !customQueryOptionToken.Name.StartsWith("$", 4)) ? ("$" + name) : name);
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

		// Token: 0x06000C91 RID: 3217 RVA: 0x00022A38 File Offset: 0x00020C38
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
			else if (num <= 2272632476U)
			{
				if (num != 2110202789U)
				{
					if (num != 2221740796U)
					{
						if (num != 2272632476U)
						{
							return false;
						}
						if (!(text == "$format"))
						{
							return false;
						}
					}
					else if (!(text == "$compute"))
					{
						return false;
					}
				}
				else if (!(text == "$apply"))
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

		// Token: 0x040006CA RID: 1738
		private readonly ODataUriParserConfiguration configuration;

		// Token: 0x040006CB RID: 1739
		private readonly Uri serviceRoot;

		// Token: 0x040006CC RID: 1740
		private readonly Uri uri;

		// Token: 0x040006CD RID: 1741
		private readonly List<CustomQueryOptionToken> queryOptions;

		// Token: 0x040006CE RID: 1742
		private IDictionary<string, string> queryOptionDic;

		// Token: 0x040006CF RID: 1743
		private IList<KeyValuePair<string, string>> customQueryOptions;

		// Token: 0x040006D0 RID: 1744
		private ODataQueryOptionParser queryOptionParser;

		// Token: 0x040006D1 RID: 1745
		private ODataPath odataPath;

		// Token: 0x040006D2 RID: 1746
		private EntityIdSegment entityIdSegment;
	}
}
