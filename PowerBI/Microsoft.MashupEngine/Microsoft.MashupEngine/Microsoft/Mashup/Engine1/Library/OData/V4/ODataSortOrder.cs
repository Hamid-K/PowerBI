using System;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000895 RID: 2197
	internal sealed class ODataSortOrder
	{
		// Token: 0x06003F10 RID: 16144 RVA: 0x000CECFB File Offset: 0x000CCEFB
		public ODataSortOrder(string[] names, bool[] ascendings)
		{
			this.names = names;
			this.ascendings = ascendings;
		}

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x06003F11 RID: 16145 RVA: 0x000CED11 File Offset: 0x000CCF11
		public string[] Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x06003F12 RID: 16146 RVA: 0x000CED19 File Offset: 0x000CCF19
		public bool[] Ascendings
		{
			get
			{
				return this.ascendings;
			}
		}

		// Token: 0x0400211C RID: 8476
		public static readonly ODataSortOrder None = new ODataSortOrder(new string[0], new bool[0]);

		// Token: 0x0400211D RID: 8477
		private readonly string[] names;

		// Token: 0x0400211E RID: 8478
		private readonly bool[] ascendings;
	}
}
