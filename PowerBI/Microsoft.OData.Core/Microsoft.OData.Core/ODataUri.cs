using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData
{
	// Token: 0x020000D5 RID: 213
	public sealed class ODataUri
	{
		// Token: 0x060009EE RID: 2542 RVA: 0x00019B0C File Offset: 0x00017D0C
		public ODataUri()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
			this.ParameterAliasValueAccessor = new ParameterAliasValueAccessor(dictionary);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00019B38 File Offset: 0x00017D38
		internal ODataUri(ParameterAliasValueAccessor parameterAliasValueAccessor, ODataPath path, IEnumerable<QueryNode> customQueryOptions, SelectExpandClause selectAndExpand, FilterClause filter, OrderByClause orderby, SearchClause search, ApplyClause apply, long? skip, long? top, long? index, bool? queryCount, ComputeClause compute = null)
		{
			this.ParameterAliasValueAccessor = parameterAliasValueAccessor;
			this.Path = path;
			this.CustomQueryOptions = new ReadOnlyCollection<QueryNode>(customQueryOptions.ToList<QueryNode>());
			this.SelectAndExpand = selectAndExpand;
			this.Filter = filter;
			this.OrderBy = orderby;
			this.Search = search;
			this.Apply = apply;
			this.Skip = skip;
			this.Top = top;
			this.Index = index;
			this.QueryCount = queryCount;
			this.Compute = compute;
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00019BBA File Offset: 0x00017DBA
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x00019BC2 File Offset: 0x00017DC2
		public Uri RequestUri { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00019BCB File Offset: 0x00017DCB
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x00019BD4 File Offset: 0x00017DD4
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

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00019C34 File Offset: 0x00017E34
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

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00019C4B File Offset: 0x00017E4B
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x00019C53 File Offset: 0x00017E53
		public ODataPath Path { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00019C5C File Offset: 0x00017E5C
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x00019C64 File Offset: 0x00017E64
		public IEnumerable<QueryNode> CustomQueryOptions { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00019C6D File Offset: 0x00017E6D
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x00019C75 File Offset: 0x00017E75
		public SelectExpandClause SelectAndExpand { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x00019C7E File Offset: 0x00017E7E
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x00019C86 File Offset: 0x00017E86
		public FilterClause Filter { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00019C8F File Offset: 0x00017E8F
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x00019C97 File Offset: 0x00017E97
		public OrderByClause OrderBy { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00019CA0 File Offset: 0x00017EA0
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x00019CA8 File Offset: 0x00017EA8
		public SearchClause Search { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00019CB1 File Offset: 0x00017EB1
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x00019CB9 File Offset: 0x00017EB9
		public ApplyClause Apply { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00019CC2 File Offset: 0x00017EC2
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x00019CCA File Offset: 0x00017ECA
		public ComputeClause Compute { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00019CD3 File Offset: 0x00017ED3
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x00019CDB File Offset: 0x00017EDB
		public long? Skip { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00019CE4 File Offset: 0x00017EE4
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x00019CEC File Offset: 0x00017EEC
		public long? Top { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00019CF5 File Offset: 0x00017EF5
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x00019CFD File Offset: 0x00017EFD
		public long? Index { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00019D06 File Offset: 0x00017F06
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x00019D0E File Offset: 0x00017F0E
		public bool? QueryCount { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00019D17 File Offset: 0x00017F17
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x00019D1F File Offset: 0x00017F1F
		public string SkipToken { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x00019D28 File Offset: 0x00017F28
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x00019D30 File Offset: 0x00017F30
		public string DeltaToken { get; set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00019D39 File Offset: 0x00017F39
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x00019D41 File Offset: 0x00017F41
		internal Uri MetadataDocumentUri { get; private set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x00019D4A File Offset: 0x00017F4A
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x00019D52 File Offset: 0x00017F52
		internal ParameterAliasValueAccessor ParameterAliasValueAccessor { get; set; }

		// Token: 0x06000A15 RID: 2581 RVA: 0x00019D5C File Offset: 0x00017F5C
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
				Index = this.Index,
				QueryCount = this.QueryCount,
				SkipToken = this.SkipToken,
				DeltaToken = this.DeltaToken
			};
		}

		// Token: 0x040003AC RID: 940
		private static readonly Uri MetadataSegment = new Uri("$metadata", UriKind.Relative);

		// Token: 0x040003AD RID: 941
		private Uri serviceRoot;
	}
}
