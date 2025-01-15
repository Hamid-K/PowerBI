using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000120 RID: 288
	internal sealed class BindingState
	{
		// Token: 0x06000FB2 RID: 4018 RVA: 0x000274BF File Offset: 0x000256BF
		internal BindingState(ODataUriParserConfiguration configuration)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			this.configuration = configuration;
			this.BindingRecursionDepth = 0;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000274F7 File Offset: 0x000256F7
		internal BindingState(ODataUriParserConfiguration configuration, List<ODataPathSegment> parsedSegments)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			this.configuration = configuration;
			this.BindingRecursionDepth = 0;
			this.parsedSegments = parsedSegments;
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x00027536 File Offset: 0x00025736
		internal IEdmModel Model
		{
			get
			{
				return this.configuration.Model;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x00027543 File Offset: 0x00025743
		internal ODataUriParserConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0002754B File Offset: 0x0002574B
		// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x00027553 File Offset: 0x00025753
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

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x0002755C File Offset: 0x0002575C
		internal Stack<RangeVariable> RangeVariables
		{
			get
			{
				return this.rangeVariables;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00027564 File Offset: 0x00025764
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x0002756C File Offset: 0x0002576C
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

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00027575 File Offset: 0x00025775
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x0002757D File Offset: 0x0002577D
		internal HashSet<EndPathToken> AggregatedPropertyNames { get; set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x00027586 File Offset: 0x00025786
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x0002758E File Offset: 0x0002578E
		internal bool IsCollapsed { get; set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00027597 File Offset: 0x00025797
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x0002759F File Offset: 0x0002579F
		internal bool InEntitySetAggregation { get; set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x000275A8 File Offset: 0x000257A8
		internal List<ODataPathSegment> ParsedSegments
		{
			get
			{
				return this.parsedSegments;
			}
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x000275B0 File Offset: 0x000257B0
		internal void RecurseEnter()
		{
			this.BindingRecursionDepth++;
			if (this.BindingRecursionDepth > this.configuration.Settings.FilterLimit)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
			}
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x000275E3 File Offset: 0x000257E3
		internal void RecurseLeave()
		{
			this.BindingRecursionDepth--;
		}

		// Token: 0x04000799 RID: 1945
		private readonly ODataUriParserConfiguration configuration;

		// Token: 0x0400079A RID: 1946
		private readonly Stack<RangeVariable> rangeVariables = new Stack<RangeVariable>();

		// Token: 0x0400079B RID: 1947
		private RangeVariable implicitRangeVariable;

		// Token: 0x0400079C RID: 1948
		private int BindingRecursionDepth;

		// Token: 0x0400079D RID: 1949
		private List<CustomQueryOptionToken> queryOptions;

		// Token: 0x0400079E RID: 1950
		private List<ODataPathSegment> parsedSegments = new List<ODataPathSegment>();
	}
}
