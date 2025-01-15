using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200025D RID: 605
	[Serializable]
	internal class JsonDeserializationNotImplementedException : ServiceLocalException
	{
		// Token: 0x060015AD RID: 5549 RVA: 0x0003CD53 File Offset: 0x0003BD53
		internal JsonDeserializationNotImplementedException()
			: base(Strings.JsonDeserializationNotImplemented)
		{
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0003CD65 File Offset: 0x0003BD65
		internal JsonDeserializationNotImplementedException(string message)
			: base(message)
		{
		}
	}
}
