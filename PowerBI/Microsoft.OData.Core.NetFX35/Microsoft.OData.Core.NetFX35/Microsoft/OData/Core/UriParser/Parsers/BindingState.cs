using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001C4 RID: 452
	internal sealed class BindingState
	{
		// Token: 0x060010E4 RID: 4324 RVA: 0x0003AE43 File Offset: 0x00039043
		internal BindingState(ODataUriParserConfiguration configuration)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(configuration, "configuration");
			this.configuration = configuration;
			this.BindingRecursionDepth = 0;
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x0003AE6F File Offset: 0x0003906F
		internal IEdmModel Model
		{
			get
			{
				return this.configuration.Model;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x0003AE7C File Offset: 0x0003907C
		internal ODataUriParserConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0003AE84 File Offset: 0x00039084
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x0003AE8C File Offset: 0x0003908C
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

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x0003AE95 File Offset: 0x00039095
		internal Stack<RangeVariable> RangeVariables
		{
			get
			{
				return this.rangeVariables;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x0003AE9D File Offset: 0x0003909D
		// (set) Token: 0x060010EB RID: 4331 RVA: 0x0003AEA5 File Offset: 0x000390A5
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

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x0003AEAE File Offset: 0x000390AE
		// (set) Token: 0x060010ED RID: 4333 RVA: 0x0003AEB6 File Offset: 0x000390B6
		internal List<string> AggregatedPropertyNames { get; set; }

		// Token: 0x060010EE RID: 4334 RVA: 0x0003AEBF File Offset: 0x000390BF
		internal void RecurseEnter()
		{
			this.BindingRecursionDepth++;
			if (this.BindingRecursionDepth > this.configuration.Settings.FilterLimit)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
			}
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0003AEF2 File Offset: 0x000390F2
		internal void RecurseLeave()
		{
			this.BindingRecursionDepth--;
		}

		// Token: 0x04000778 RID: 1912
		private readonly ODataUriParserConfiguration configuration;

		// Token: 0x04000779 RID: 1913
		private readonly Stack<RangeVariable> rangeVariables = new Stack<RangeVariable>();

		// Token: 0x0400077A RID: 1914
		private RangeVariable implicitRangeVariable;

		// Token: 0x0400077B RID: 1915
		private int BindingRecursionDepth;

		// Token: 0x0400077C RID: 1916
		private List<CustomQueryOptionToken> queryOptions;
	}
}
