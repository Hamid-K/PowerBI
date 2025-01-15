using System;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Newtonsoft.Json;

namespace Microsoft.InfoNav.Data.Contracts.DsqGeneration
{
	// Token: 0x020000FD RID: 253
	public static class ApplicationContextSerializer
	{
		// Token: 0x06000695 RID: 1685 RVA: 0x0000D956 File Offset: 0x0000BB56
		public static string Serialize(ApplicationContext applicationContext)
		{
			if (applicationContext == null)
			{
				return null;
			}
			return JsonConvert.SerializeObject(applicationContext);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0000D963 File Offset: 0x0000BB63
		public static ApplicationContext Deserialize(string applicationContextString)
		{
			if (applicationContextString == null)
			{
				return null;
			}
			return JsonConvert.DeserializeObject<ApplicationContext>(applicationContextString);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0000D970 File Offset: 0x0000BB70
		public static string SerializeForTelemetry(ApplicationContext applicationContext)
		{
			if (applicationContext == null)
			{
				return null;
			}
			return JsonConvert.SerializeObject(applicationContext, new JsonConverter[]
			{
				new ApplicationContextSourceConverterForTelemetry()
			});
		}
	}
}
