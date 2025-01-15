using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000E1 RID: 225
	internal sealed class BindingState
	{
		// Token: 0x06000B5A RID: 2906 RVA: 0x0001BD4F File Offset: 0x00019F4F
		internal BindingState(ODataUriParserConfiguration configuration)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			this.configuration = configuration;
			this.BindingRecursionDepth = 0;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0001BD87 File Offset: 0x00019F87
		internal BindingState(ODataUriParserConfiguration configuration, List<ODataPathSegment> parsedSegments)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			this.configuration = configuration;
			this.BindingRecursionDepth = 0;
			this.parsedSegments = parsedSegments;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0001BDC6 File Offset: 0x00019FC6
		internal IEdmModel Model
		{
			get
			{
				return this.configuration.Model;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0001BDD3 File Offset: 0x00019FD3
		internal ODataUriParserConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0001BDDB File Offset: 0x00019FDB
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x0001BDE3 File Offset: 0x00019FE3
		internal RangeVariable ImplicitRangeVariable
		{
			get
			{
				return this.implicitRangeVariable;
			}
			set
			{
				this.implicitRangeVariable = value;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0001BDEC File Offset: 0x00019FEC
		internal Stack<RangeVariable> RangeVariables
		{
			get
			{
				return this.rangeVariables;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		// (set) Token: 0x06000B62 RID: 2914 RVA: 0x0001BDFC File Offset: 0x00019FFC
		internal List<CustomQueryOptionToken> QueryOptions
		{
			get
			{
				return this.queryOptions;
			}
			set
			{
				this.queryOptions = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0001BE05 File Offset: 0x0001A005
		// (set) Token: 0x06000B64 RID: 2916 RVA: 0x0001BE0D File Offset: 0x0001A00D
		internal List<string> AggregatedPropertyNames { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0001BE16 File Offset: 0x0001A016
		internal List<ODataPathSegment> ParsedSegments
		{
			get
			{
				return this.parsedSegments;
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0001BE1E File Offset: 0x0001A01E
		internal void RecurseEnter()
		{
			this.BindingRecursionDepth++;
			if (this.BindingRecursionDepth > this.configuration.Settings.FilterLimit)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0001BE51 File Offset: 0x0001A051
		internal void RecurseLeave()
		{
			this.BindingRecursionDepth--;
		}

		// Token: 0x04000687 RID: 1671
		private readonly ODataUriParserConfiguration configuration;

		// Token: 0x04000688 RID: 1672
		private readonly Stack<RangeVariable> rangeVariables = new Stack<RangeVariable>();

		// Token: 0x04000689 RID: 1673
		private RangeVariable implicitRangeVariable;

		// Token: 0x0400068A RID: 1674
		private int BindingRecursionDepth;

		// Token: 0x0400068B RID: 1675
		private List<CustomQueryOptionToken> queryOptions;

		// Token: 0x0400068C RID: 1676
		private List<ODataPathSegment> parsedSegments = new List<ODataPathSegment>();
	}
}
