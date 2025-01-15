using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Core.UriParser.Aggregation;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.OData.Core
{
	// Token: 0x020001A3 RID: 419
	public sealed class ODataUri
	{
		// Token: 0x06000F96 RID: 3990 RVA: 0x00035B7C File Offset: 0x00033D7C
		public ODataUri()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
			this.ParameterAliasValueAccessor = new ParameterAliasValueAccessor(dictionary);
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00035BA8 File Offset: 0x00033DA8
		internal ODataUri(ParameterAliasValueAccessor parameterAliasValueAccessor, ODataPath path, IEnumerable<QueryNode> customQueryOptions, SelectExpandClause selectAndExpand, FilterClause filter, OrderByClause orderby, SearchClause search, ApplyClause apply, long? skip, long? top, bool? queryCount)
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
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x00035C1A File Offset: 0x00033E1A
		// (set) Token: 0x06000F99 RID: 3993 RVA: 0x00035C22 File Offset: 0x00033E22
		public Uri RequestUri { get; set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00035C2B File Offset: 0x00033E2B
		// (set) Token: 0x06000F9B RID: 3995 RVA: 0x00035C34 File Offset: 0x00033E34
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

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x00035C94 File Offset: 0x00033E94
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

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x00035CAB File Offset: 0x00033EAB
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x00035CB3 File Offset: 0x00033EB3
		public ODataPath Path { get; set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00035CBC File Offset: 0x00033EBC
		// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x00035CC4 File Offset: 0x00033EC4
		public IEnumerable<QueryNode> CustomQueryOptions { get; set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00035CCD File Offset: 0x00033ECD
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x00035CD5 File Offset: 0x00033ED5
		public SelectExpandClause SelectAndExpand { get; set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00035CDE File Offset: 0x00033EDE
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x00035CE6 File Offset: 0x00033EE6
		public FilterClause Filter { get; set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00035CEF File Offset: 0x00033EEF
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x00035CF7 File Offset: 0x00033EF7
		public OrderByClause OrderBy { get; set; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00035D00 File Offset: 0x00033F00
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x00035D08 File Offset: 0x00033F08
		public SearchClause Search { get; set; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00035D11 File Offset: 0x00033F11
		// (set) Token: 0x06000FAA RID: 4010 RVA: 0x00035D19 File Offset: 0x00033F19
		public ApplyClause Apply { get; set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00035D22 File Offset: 0x00033F22
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x00035D2A File Offset: 0x00033F2A
		public long? Skip { get; set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00035D33 File Offset: 0x00033F33
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x00035D3B File Offset: 0x00033F3B
		public long? Top { get; set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00035D44 File Offset: 0x00033F44
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x00035D4C File Offset: 0x00033F4C
		public bool? QueryCount { get; set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00035D55 File Offset: 0x00033F55
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x00035D5D File Offset: 0x00033F5D
		public string SkipToken { get; set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x00035D66 File Offset: 0x00033F66
		// (set) Token: 0x06000FB4 RID: 4020 RVA: 0x00035D6E File Offset: 0x00033F6E
		public string DeltaToken { get; set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x00035D77 File Offset: 0x00033F77
		// (set) Token: 0x06000FB6 RID: 4022 RVA: 0x00035D7F File Offset: 0x00033F7F
		internal Uri MetadataDocumentUri { get; private set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x00035D88 File Offset: 0x00033F88
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x00035D90 File Offset: 0x00033F90
		internal ParameterAliasValueAccessor ParameterAliasValueAccessor { get; set; }

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00035D9C File Offset: 0x00033F9C
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

		// Token: 0x040006DB RID: 1755
		private static readonly Uri MetadataSegment = new Uri("$metadata", 2);

		// Token: 0x040006DC RID: 1756
		private Uri serviceRoot;
	}
}
