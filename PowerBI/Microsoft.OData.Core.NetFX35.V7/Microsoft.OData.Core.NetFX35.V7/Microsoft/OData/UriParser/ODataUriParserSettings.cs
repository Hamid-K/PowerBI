using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200010A RID: 266
	public sealed class ODataUriParserSettings
	{
		// Token: 0x06000CAA RID: 3242 RVA: 0x00022D88 File Offset: 0x00020F88
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

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00022DE2 File Offset: 0x00020FE2
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x00022DEA File Offset: 0x00020FEA
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

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00022E02 File Offset: 0x00021002
		// (set) Token: 0x06000CAE RID: 3246 RVA: 0x00022E0A File Offset: 0x0002100A
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

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x00022E22 File Offset: 0x00021022
		// (set) Token: 0x06000CB0 RID: 3248 RVA: 0x00022E2A File Offset: 0x0002102A
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

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00022E42 File Offset: 0x00021042
		// (set) Token: 0x06000CB2 RID: 3250 RVA: 0x00022E4A File Offset: 0x0002104A
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

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00022E62 File Offset: 0x00021062
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x00022E6A File Offset: 0x0002106A
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

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00022E82 File Offset: 0x00021082
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x00022E8A File Offset: 0x0002108A
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

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00022EA2 File Offset: 0x000210A2
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x00022EAA File Offset: 0x000210AA
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

		// Token: 0x040006DD RID: 1757
		internal const int DefaultFilterLimit = 800;

		// Token: 0x040006DE RID: 1758
		internal const int DefaultOrderByLimit = 800;

		// Token: 0x040006DF RID: 1759
		internal const int DefaultSelectExpandLimit = 800;

		// Token: 0x040006E0 RID: 1760
		internal const int DefaultPathLimit = 100;

		// Token: 0x040006E1 RID: 1761
		internal const int DefaultSearchLimit = 100;

		// Token: 0x040006E2 RID: 1762
		private int filterLimit;

		// Token: 0x040006E3 RID: 1763
		private int orderByLimit;

		// Token: 0x040006E4 RID: 1764
		private int pathLimit;

		// Token: 0x040006E5 RID: 1765
		private int selectExpandLimit;

		// Token: 0x040006E6 RID: 1766
		private int maxExpandDepth;

		// Token: 0x040006E7 RID: 1767
		private int maxExpandCount;

		// Token: 0x040006E8 RID: 1768
		private int searchLimit;
	}
}
