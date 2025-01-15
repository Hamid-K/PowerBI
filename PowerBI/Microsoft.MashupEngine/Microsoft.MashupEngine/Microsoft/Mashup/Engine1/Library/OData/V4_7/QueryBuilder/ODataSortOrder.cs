using System;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007FA RID: 2042
	internal sealed class ODataSortOrder
	{
		// Token: 0x06003AFF RID: 15103 RVA: 0x000BF4EA File Offset: 0x000BD6EA
		public ODataSortOrder(string[] names, bool[] ascendings)
		{
			this.names = names;
			this.ascendings = ascendings;
		}

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x000BF500 File Offset: 0x000BD700
		public string[] Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x06003B01 RID: 15105 RVA: 0x000BF508 File Offset: 0x000BD708
		public bool[] Ascendings
		{
			get
			{
				return this.ascendings;
			}
		}

		// Token: 0x04001E9C RID: 7836
		public static readonly ODataSortOrder None = new ODataSortOrder(new string[0], new bool[0]);

		// Token: 0x04001E9D RID: 7837
		private readonly string[] names;

		// Token: 0x04001E9E RID: 7838
		private readonly bool[] ascendings;
	}
}
