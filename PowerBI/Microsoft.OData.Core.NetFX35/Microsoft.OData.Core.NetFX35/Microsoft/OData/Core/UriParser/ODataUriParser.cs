using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.UriParser.Aggregation;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Parsers;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001F1 RID: 497
	public sealed class ODataUriParser
	{
		// Token: 0x0600121D RID: 4637 RVA: 0x00041A08 File Offset: 0x0003FC08
		public ODataUriParser(IEdmModel model, Uri serviceRoot, Uri fullUri)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(fullUri, "fullUri");
			if (serviceRoot == null)
			{
				throw new ODataException(Strings.UriParser_NeedServiceRootForThisOverload);
			}
			if (!serviceRoot.IsAbsoluteUri)
			{
				throw new ODataException(Strings.UriParser_UriMustBeAbsolute(serviceRoot));
			}
			this.configuration = new ODataUriParserConfiguration(model);
			this.serviceRoot = UriUtils.EnsureTaillingSlash(serviceRoot);
			this.fullUri = (fullUri.IsAbsoluteUri ? fullUri : UriUtils.UriToAbsoluteUri(this.ServiceRoot, fullUri));
			this.queryOptions = UriUtils.ParseQueryOptions(this.fullUri);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00041A94 File Offset: 0x0003FC94
		public ODataUriParser(IEdmModel model, Uri fullUri)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(fullUri, "fullUri");
			if (fullUri.IsAbsoluteUri)
			{
				throw new ODataException(Strings.UriParser_FullUriMustBeRelative);
			}
			this.configuration = new ODataUriParserConfiguration(model);
			this.fullUri = fullUri;
			this.queryOptions = UriUtils.ParseQueryOptions(UriUtils.CreateMockAbsoluteUri(this.fullUri));
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x00041AEE File Offset: 0x0003FCEE
		public ODataUriParserSettings Settings
		{
			get
			{
				return this.configuration.Settings;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x00041AFB File Offset: 0x0003FCFB
		public IEdmModel Model
		{
			get
			{
				return this.configuration.Model;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00041B08 File Offset: 0x0003FD08
		public Uri ServiceRoot
		{
			get
			{
				return this.serviceRoot;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00041B10 File Offset: 0x0003FD10
		// (set) Token: 0x06001223 RID: 4643 RVA: 0x00041B1D File Offset: 0x0003FD1D
		public ODataUrlConventions UrlConventions
		{
			get
			{
				return this.configuration.UrlConventions;
			}
			set
			{
				this.configuration.UrlConventions = value;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00041B2B File Offset: 0x0003FD2B
		// (set) Token: 0x06001225 RID: 4645 RVA: 0x00041B38 File Offset: 0x0003FD38
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

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x00041B46 File Offset: 0x0003FD46
		// (set) Token: 0x06001227 RID: 4647 RVA: 0x00041B53 File Offset: 0x0003FD53
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

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x00041B61 File Offset: 0x0003FD61
		// (set) Token: 0x06001229 RID: 4649 RVA: 0x00041B6E File Offset: 0x0003FD6E
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

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x00041B7C File Offset: 0x0003FD7C
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

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00041B97 File Offset: 0x0003FD97
		// (set) Token: 0x0600122C RID: 4652 RVA: 0x00041BA4 File Offset: 0x0003FDA4
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

		// Token: 0x0600122D RID: 4653 RVA: 0x00041BB2 File Offset: 0x0003FDB2
		public ODataPath ParsePath()
		{
			this.Initialize();
			return this.odataPath;
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00041BC0 File Offset: 0x0003FDC0
		public FilterClause ParseFilter()
		{
			this.Initialize();
			return this.queryOptionParser.ParseFilter();
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00041BD3 File Offset: 0x0003FDD3
		public OrderByClause ParseOrderBy()
		{
			this.Initialize();
			return this.queryOptionParser.ParseOrderBy();
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00041BE6 File Offset: 0x0003FDE6
		public SelectExpandClause ParseSelectAndExpand()
		{
			this.Initialize();
			return this.queryOptionParser.ParseSelectAndExpand();
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00041C10 File Offset: 0x0003FE10
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
				if (!this.fullUri.IsAbsoluteUri)
				{
					Uri uri2 = UriUtils.CreateMockAbsoluteUri(null);
					Uri uri3 = new Uri(UriUtils.CreateMockAbsoluteUri(this.fullUri), uri);
					uri = uri2.MakeRelativeUri(uri3);
				}
				else
				{
					uri = new Uri(this.fullUri, uri);
				}
			}
			this.entityIdSegment = new EntityIdSegment(uri);
			return this.entityIdSegment;
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00041D1B File Offset: 0x0003FF1B
		public long? ParseTop()
		{
			this.Initialize();
			return this.queryOptionParser.ParseTop();
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00041D2E File Offset: 0x0003FF2E
		public long? ParseSkip()
		{
			this.Initialize();
			return this.queryOptionParser.ParseSkip();
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00041D41 File Offset: 0x0003FF41
		public bool? ParseCount()
		{
			this.Initialize();
			return this.queryOptionParser.ParseCount();
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00041D54 File Offset: 0x0003FF54
		public SearchClause ParseSearch()
		{
			this.Initialize();
			return this.queryOptionParser.ParseSearch();
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00041D67 File Offset: 0x0003FF67
		public ApplyClause ParseApply()
		{
			this.Initialize();
			return this.queryOptionParser.ParseApply();
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00041D7A File Offset: 0x0003FF7A
		public string ParseSkipToken()
		{
			this.Initialize();
			return this.queryOptionParser.ParseSkipToken();
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00041D8D File Offset: 0x0003FF8D
		public string ParseDeltaToken()
		{
			this.Initialize();
			return this.queryOptionParser.ParseDeltaToken();
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00041DA0 File Offset: 0x0003FFA0
		public ODataUri ParseUri()
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(this.configuration.Model, "model");
			ExceptionUtils.CheckArgumentNotNull<Uri>(this.fullUri, "fullUri");
			ODataPath odataPath = this.ParsePath();
			SelectExpandClause selectExpandClause = this.ParseSelectAndExpand();
			FilterClause filterClause = this.ParseFilter();
			OrderByClause orderByClause = this.ParseOrderBy();
			SearchClause searchClause = this.ParseSearch();
			ApplyClause applyClause = this.ParseApply();
			long? num = this.ParseTop();
			long? num2 = this.ParseSkip();
			bool? flag = this.ParseCount();
			string text = this.ParseSkipToken();
			string text2 = this.ParseDeltaToken();
			List<QueryNode> list = new List<QueryNode>();
			return new ODataUri(this.ParameterAliasValueAccessor, odataPath, list, selectExpandClause, filterClause, orderByClause, searchClause, applyClause, num2, num, flag)
			{
				ServiceRoot = this.serviceRoot,
				SkipToken = text,
				DeltaToken = text2
			};
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00041E6C File Offset: 0x0004006C
		private ODataPath ParsePathImplementation()
		{
			Uri uri = this.fullUri;
			ExceptionUtils.CheckArgumentNotNull<Uri>(uri, "pathUri");
			UriPathParser uriPathParser = new UriPathParser(this.Settings.PathLimit);
			ICollection<string> collection = uriPathParser.ParsePathIntoSegments(uri, this.ServiceRoot);
			return ODataPathFactory.BindPath(collection, this.configuration);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00041EB8 File Offset: 0x000400B8
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
			ODataPathSegment odataPathSegment = this.odataPath.LastSegment;
			IEnumerator<ODataPathSegment> enumerator = this.odataPath.GetEnumerator();
			int num = 0;
			while (++num < this.odataPath.Count && enumerator.MoveNext())
			{
			}
			ODataPathSegment odataPathSegment2 = enumerator.Current;
			if (odataPathSegment != null)
			{
				if (odataPathSegment is KeySegment || odataPathSegment is CountSegment)
				{
					odataPathSegment = odataPathSegment2;
				}
				this.targetNavigationSource = odataPathSegment.TargetEdmNavigationSource;
				this.targetEdmType = odataPathSegment.TargetEdmType;
				if (this.targetEdmType != null)
				{
					IEdmCollectionType edmCollectionType = this.targetEdmType as IEdmCollectionType;
					if (edmCollectionType != null)
					{
						this.targetEdmType = edmCollectionType.ElementType.Definition;
					}
				}
			}
			this.InitQueryOptionDic();
			this.queryOptionParser = new ODataQueryOptionParser(this.Model, this.targetEdmType, this.targetNavigationSource, this.queryOptionDic)
			{
				Configuration = this.configuration
			};
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00041FC8 File Offset: 0x000401C8
		private void InitQueryOptionDic()
		{
			if (this.queryOptionDic != null)
			{
				return;
			}
			this.queryOptionDic = new Dictionary<string, string>(StringComparer.Ordinal);
			if (this.queryOptions != null)
			{
				foreach (CustomQueryOptionToken customQueryOptionToken in this.queryOptions)
				{
					if (customQueryOptionToken.Name != null)
					{
						if (this.queryOptionDic.ContainsKey(customQueryOptionToken.Name))
						{
							throw new ODataException(Strings.QueryOptionUtils_QueryParameterMustBeSpecifiedOnce(customQueryOptionToken.Name));
						}
						this.queryOptionDic.Add(customQueryOptionToken.Name, customQueryOptionToken.Value);
					}
				}
			}
		}

		// Token: 0x040007C7 RID: 1991
		private readonly ODataUriParserConfiguration configuration;

		// Token: 0x040007C8 RID: 1992
		private readonly Uri serviceRoot;

		// Token: 0x040007C9 RID: 1993
		private readonly Uri fullUri;

		// Token: 0x040007CA RID: 1994
		private readonly List<CustomQueryOptionToken> queryOptions;

		// Token: 0x040007CB RID: 1995
		private IDictionary<string, string> queryOptionDic;

		// Token: 0x040007CC RID: 1996
		private ODataQueryOptionParser queryOptionParser;

		// Token: 0x040007CD RID: 1997
		private IEdmType targetEdmType;

		// Token: 0x040007CE RID: 1998
		private IEdmNavigationSource targetNavigationSource;

		// Token: 0x040007CF RID: 1999
		private ODataPath odataPath;

		// Token: 0x040007D0 RID: 2000
		private EntityIdSegment entityIdSegment;
	}
}
