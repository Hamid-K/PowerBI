using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	internal static class DataAccessUtils
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000022B2 File Offset: 0x000004B2
		internal static bool IsMashupProvider(string providerString)
		{
			return providerString.StartsWith("Microsoft.Mashup.OleDb");
		}

		// Token: 0x04000015 RID: 21
		private const string MashupOledbProviderString = "Microsoft.Mashup.OleDb";
	}
}
