using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData
{
	// Token: 0x020000D2 RID: 210
	public sealed class ODataUri
	{
		// Token: 0x0600080A RID: 2058 RVA: 0x00016878 File Offset: 0x00014A78
		public ODataUri()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
			this.ParameterAliasValueAccessor = new ParameterAliasValueAccessor(dictionary);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x000168A4 File Offset: 0x00014AA4
		internal ODataUri(ParameterAliasValueAccessor parameterAliasValueAccessor, ODataPath path, IEnumerable<QueryNode> customQueryOptions, SelectExpandClause selectAndExpand, FilterClause filter, OrderByClause orderby, SearchClause search, ApplyClause apply, long? skip, long? top, bool? queryCount, ComputeClause compute = null)
		{
			this.ParameterAliasValueAccessor = parameterAliasValueAccessor;
			this.Path = path;
			this.CustomQueryOptions = new ReadOnlyCollection<QueryNode>(Enumerable.ToList<QueryNode>(customQueryOptions));
			this.SelectAndExpand = selectAndExpand;
			this.Filter = filter;
			this.OrderBy = orderby;
			this.Search = search;
			this.Apply = apply;
			this.Skip = skip;
			this.Top = top;
			this.QueryCount = queryCount;
			this.Compute = compute;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0001691E File Offset: 0x00014B1E
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x00016926 File Offset: 0x00014B26
		public Uri RequestUri { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0001692F File Offset: 0x00014B2F
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x00016938 File Offset: 0x00014B38
		public Uri ServiceRoot
		{
			get
			{
				return this.serviceRoot;
			}
			set
			{
				if (value == null)
				{
					this.serviceRoot = null;
					this.MetadataDocumentUri = null;
					return;
				}
				if (!value.IsAbsoluteUri)
				{
					throw new ODataException(Strings.WriterValidationUtils_MessageWriterSettingsServiceDocumentUriMustBeNullOrAbsolute(UriUtils.UriToString(value)));
				}
				this.serviceRoot = UriUtils.EnsureTaillingSlash(value);
				this.MetadataDocumentUri = new Uri(this.serviceRoot, ODataUri.MetadataSegment);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00016998 File Offset: 0x00014B98
		public IDictionary<string, SingleValueNode> ParameterAliasNodes
		{
			get
			{
				if (this.ParameterAliasValueAccessor == null)
				{
					return null;
				}
				return this.ParameterAliasValueAccessor.ParameterAliasValueNodesCached;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x000169AF File Offset: 0x00014BAF
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x000169B7 File Offset: 0x00014BB7
		public ODataPath Path { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x000169C0 File Offset: 0x00014BC0
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x000169C8 File Offset: 0x00014BC8
		public IEnumerable<QueryNode> CustomQueryOptions { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x000169D1 File Offset: 0x00014BD1
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x000169D9 File Offset: 0x00014BD9
		public SelectExpandClause SelectAndExpand { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x000169E2 File Offset: 0x00014BE2
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x000169EA File Offset: 0x00014BEA
		public FilterClause Filter { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x000169F3 File Offset: 0x00014BF3
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x000169FB File Offset: 0x00014BFB
		public OrderByClause OrderBy { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00016A04 File Offset: 0x00014C04
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x00016A0C File Offset: 0x00014C0C
		public SearchClause Search { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x00016A15 File Offset: 0x00014C15
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x00016A1D File Offset: 0x00014C1D
		public ApplyClause Apply { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00016A26 File Offset: 0x00014C26
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x00016A2E File Offset: 0x00014C2E
		public ComputeClause Compute { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00016A37 File Offset: 0x00014C37
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x00016A3F File Offset: 0x00014C3F
		public long? Skip { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00016A48 File Offset: 0x00014C48
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x00016A50 File Offset: 0x00014C50
		public long? Top { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x00016A59 File Offset: 0x00014C59
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x00016A61 File Offset: 0x00014C61
		public bool? QueryCount { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00016A6A File Offset: 0x00014C6A
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x00016A72 File Offset: 0x00014C72
		public string SkipToken { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00016A7B File Offset: 0x00014C7B
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x00016A83 File Offset: 0x00014C83
		public string DeltaToken { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00016A8C File Offset: 0x00014C8C
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x00016A94 File Offset: 0x00014C94
		internal Uri MetadataDocumentUri { get; private set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00016A9D File Offset: 0x00014C9D
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x00016AA5 File Offset: 0x00014CA5
		internal ParameterAliasValueAccessor ParameterAliasValueAccessor { get; set; }

		// Token: 0x0600082F RID: 2095 RVA: 0x00016AB0 File Offset: 0x00014CB0
		public ODataUri Clone()
		{
			return new ODataUri
			{
				RequestUri = this.RequestUri,
				serviceRoot = this.ServiceRoot,
				MetadataDocumentUri = this.MetadataDocumentUri,
				ParameterAliasValueAccessor = this.ParameterAliasValueAccessor,
				Path = this.Path,
				CustomQueryOptions = this.CustomQueryOptions,
				SelectAndExpand = this.SelectAndExpand,
				Apply = this.Apply,
				Filter = this.Filter,
				OrderBy = this.OrderBy,
				Search = this.Search,
				Skip = this.Skip,
				Top = this.Top,
				QueryCount = this.QueryCount,
				SkipToken = this.SkipToken,
				DeltaToken = this.DeltaToken
			};
		}

		// Token: 0x0400037E RID: 894
		private static readonly Uri MetadataSegment = new Uri("$metadata", 2);

		// Token: 0x0400037F RID: 895
		private Uri serviceRoot;
	}
}
