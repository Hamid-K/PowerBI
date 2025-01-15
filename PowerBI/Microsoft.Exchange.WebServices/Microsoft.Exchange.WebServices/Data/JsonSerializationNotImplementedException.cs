using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200025E RID: 606
	[Serializable]
	internal class JsonSerializationNotImplementedException : Exception
	{
		// Token: 0x060015AF RID: 5551 RVA: 0x0003CD6E File Offset: 0x0003BD6E
		internal JsonSerializationNotImplementedException()
			: base(Strings.JsonSerializationNotImplemented)
		{
		}
	}
}
