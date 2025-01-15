using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B4B RID: 6987
	public class UniqueIdService : IUniqueIdService
	{
		// Token: 0x0600AED0 RID: 44752 RVA: 0x0023CA34 File Offset: 0x0023AC34
		public string NewUniqueId()
		{
			return Guid.NewGuid().ToString();
		}
	}
}
