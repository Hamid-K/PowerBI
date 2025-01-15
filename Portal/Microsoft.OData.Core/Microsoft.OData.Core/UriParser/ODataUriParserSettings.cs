using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200014E RID: 334
	public sealed class ODataUriParserSettings
	{
		// Token: 0x0600115F RID: 4447 RVA: 0x000311B8 File Offset: 0x0002F3B8
		public ODataUriParserSettings()
		{
			this.FilterLimit = 800;
			this.OrderByLimit = 800;
			this.PathLimit = 100;
			this.SelectExpandLimit = 800;
			this.SearchLimit = 100;
			this.MaximumExpansionDepth = int.MaxValue;
			this.MaximumExpansionCount = int.MaxValue;
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x00031212 File Offset: 0x0002F412
		// (set) Token: 0x06001161 RID: 4449 RVA: 0x0003121A File Offset: 0x0002F41A
		public int MaximumExpansionDepth
		{
			get
			{
				return this.maxExpandDepth;
			}
			set
			{
				if (value < 0)
				{
					throw new ODataException(Strings.UriParser_NegativeLimit);
				}
				this.maxExpandDepth = value;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x00031232 File Offset: 0x0002F432
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x0003123A File Offset: 0x0002F43A
		public int MaximumExpansionCount
		{
			get
			{
				return this.maxExpandCount;
			}
			set
			{
				if (value < 0)
				{
					throw new ODataException(Strings.UriParser_NegativeLimit);
				}
				this.maxExpandCount = value;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x00031252 File Offset: 0x0002F452
		// (set) Token: 0x06001165 RID: 4453 RVA: 0x0003125A File Offset: 0x0002F45A
		internal int SelectExpandLimit
		{
			get
			{
				return this.selectExpandLimit;
			}
			set
			{
				if (value < 0)
				{
					throw new ODataException(Strings.UriParser_NegativeLimit);
				}
				this.selectExpandLimit = value;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x00031272 File Offset: 0x0002F472
		// (set) Token: 0x06001167 RID: 4455 RVA: 0x0003127A File Offset: 0x0002F47A
		internal int FilterLimit
		{
			get
			{
				return this.filterLimit;
			}
			set
			{
				if (value < 0)
				{
					throw new ODataException(Strings.UriParser_NegativeLimit);
				}
				this.filterLimit = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x00031292 File Offset: 0x0002F492
		// (set) Token: 0x06001169 RID: 4457 RVA: 0x0003129A File Offset: 0x0002F49A
		internal int OrderByLimit
		{
			get
			{
				return this.orderByLimit;
			}
			set
			{
				if (value < 0)
				{
					throw new ODataException(Strings.UriParser_NegativeLimit);
				}
				this.orderByLimit = value;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x000312B2 File Offset: 0x0002F4B2
		// (set) Token: 0x0600116B RID: 4459 RVA: 0x000312BA File Offset: 0x0002F4BA
		internal int PathLimit
		{
			get
			{
				return this.pathLimit;
			}
			set
			{
				if (value < 0)
				{
					throw new ODataException(Strings.UriParser_NegativeLimit);
				}
				this.pathLimit = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x000312D2 File Offset: 0x0002F4D2
		// (set) Token: 0x0600116D RID: 4461 RVA: 0x000312DA File Offset: 0x0002F4DA
		internal int SearchLimit
		{
			get
			{
				return this.searchLimit;
			}
			set
			{
				if (value < 0)
				{
					throw new ODataException(Strings.UriParser_NegativeLimit);
				}
				this.searchLimit = value;
			}
		}

		// Token: 0x040007FB RID: 2043
		internal const int DefaultFilterLimit = 800;

		// Token: 0x040007FC RID: 2044
		internal const int DefaultOrderByLimit = 800;

		// Token: 0x040007FD RID: 2045
		internal const int DefaultSelectExpandLimit = 800;

		// Token: 0x040007FE RID: 2046
		internal const int DefaultPathLimit = 100;

		// Token: 0x040007FF RID: 2047
		internal const int DefaultSearchLimit = 100;

		// Token: 0x04000800 RID: 2048
		private int filterLimit;

		// Token: 0x04000801 RID: 2049
		private int orderByLimit;

		// Token: 0x04000802 RID: 2050
		private int pathLimit;

		// Token: 0x04000803 RID: 2051
		private int selectExpandLimit;

		// Token: 0x04000804 RID: 2052
		private int maxExpandDepth;

		// Token: 0x04000805 RID: 2053
		private int maxExpandCount;

		// Token: 0x04000806 RID: 2054
		private int searchLimit;
	}
}
