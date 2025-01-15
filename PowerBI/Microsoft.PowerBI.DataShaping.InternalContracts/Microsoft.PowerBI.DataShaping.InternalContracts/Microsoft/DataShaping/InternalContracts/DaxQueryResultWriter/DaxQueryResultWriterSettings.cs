using System;

namespace Microsoft.DataShaping.InternalContracts.DaxQueryResultWriter
{
	// Token: 0x02000038 RID: 56
	internal sealed class DaxQueryResultWriterSettings
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00004512 File Offset: 0x00002712
		internal DaxQueryResultWriterSettings(bool includeNulls)
		{
			this.IncludeNulls = includeNulls;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00004521 File Offset: 0x00002721
		internal bool IncludeNulls { get; }

		// Token: 0x0400009A RID: 154
		internal static readonly DaxQueryResultWriterSettings DefaultInstance = new DaxQueryResultWriterSettings(false);

		// Token: 0x0400009B RID: 155
		internal static readonly DaxQueryResultWriterSettings IncludeNullsInstance = new DaxQueryResultWriterSettings(true);
	}
}
