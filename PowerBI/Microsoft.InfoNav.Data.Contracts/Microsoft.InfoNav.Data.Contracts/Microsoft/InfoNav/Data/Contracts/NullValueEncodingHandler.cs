using System;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x0200008B RID: 139
	internal class NullValueEncodingHandler : EncodingHandlerBase
	{
		// Token: 0x06000334 RID: 820 RVA: 0x00009316 File Offset: 0x00007516
		internal NullValueEncodingHandler()
		{
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000931E File Offset: 0x0000751E
		protected override object GetValue(int position)
		{
			return null;
		}
	}
}
