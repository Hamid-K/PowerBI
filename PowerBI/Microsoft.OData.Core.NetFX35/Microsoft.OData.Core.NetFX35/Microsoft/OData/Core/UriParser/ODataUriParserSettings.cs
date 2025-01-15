using System;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001F3 RID: 499
	public sealed class ODataUriParserSettings
	{
		// Token: 0x0600124E RID: 4686 RVA: 0x0004216C File Offset: 0x0004036C
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

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x000421C6 File Offset: 0x000403C6
		// (set) Token: 0x06001250 RID: 4688 RVA: 0x000421CE File Offset: 0x000403CE
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

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x000421E6 File Offset: 0x000403E6
		// (set) Token: 0x06001252 RID: 4690 RVA: 0x000421EE File Offset: 0x000403EE
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

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x00042206 File Offset: 0x00040406
		// (set) Token: 0x06001254 RID: 4692 RVA: 0x0004220E File Offset: 0x0004040E
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

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x00042226 File Offset: 0x00040426
		// (set) Token: 0x06001256 RID: 4694 RVA: 0x0004222E File Offset: 0x0004042E
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

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x00042246 File Offset: 0x00040446
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x0004224E File Offset: 0x0004044E
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

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00042266 File Offset: 0x00040466
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x0004226E File Offset: 0x0004046E
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

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x00042286 File Offset: 0x00040486
		// (set) Token: 0x0600125C RID: 4700 RVA: 0x0004228E File Offset: 0x0004048E
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

		// Token: 0x040007D9 RID: 2009
		internal const int DefaultFilterLimit = 800;

		// Token: 0x040007DA RID: 2010
		internal const int DefaultOrderByLimit = 800;

		// Token: 0x040007DB RID: 2011
		internal const int DefaultSelectExpandLimit = 800;

		// Token: 0x040007DC RID: 2012
		internal const int DefaultPathLimit = 100;

		// Token: 0x040007DD RID: 2013
		internal const int DefaultSearchLimit = 100;

		// Token: 0x040007DE RID: 2014
		private int filterLimit;

		// Token: 0x040007DF RID: 2015
		private int orderByLimit;

		// Token: 0x040007E0 RID: 2016
		private int pathLimit;

		// Token: 0x040007E1 RID: 2017
		private int selectExpandLimit;

		// Token: 0x040007E2 RID: 2018
		private int maxExpandDepth;

		// Token: 0x040007E3 RID: 2019
		private int maxExpandCount;

		// Token: 0x040007E4 RID: 2020
		private int searchLimit;
	}
}
