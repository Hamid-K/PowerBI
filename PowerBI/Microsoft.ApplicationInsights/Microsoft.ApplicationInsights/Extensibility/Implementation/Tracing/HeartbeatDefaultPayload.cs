using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x020000A8 RID: 168
	internal static class HeartbeatDefaultPayload
	{
		// Token: 0x06000512 RID: 1298 RVA: 0x00015418 File Offset: 0x00013618
		public static bool IsDefaultKeyword(string keyword)
		{
			IHeartbeatDefaultPayloadProvider[] defaultPayloadProviders = HeartbeatDefaultPayload.DefaultPayloadProviders;
			for (int i = 0; i < defaultPayloadProviders.Length; i++)
			{
				if (defaultPayloadProviders[i].IsKeyword(keyword))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00015448 File Offset: 0x00013648
		public static async Task<bool> PopulateDefaultPayload(IEnumerable<string> disabledFields, IEnumerable<string> disabledProviders, IHeartbeatProvider provider)
		{
			bool populatedFields = false;
			foreach (IHeartbeatDefaultPayloadProvider heartbeatDefaultPayloadProvider in HeartbeatDefaultPayload.DefaultPayloadProviders)
			{
				if (disabledProviders == null || !disabledProviders.Contains(heartbeatDefaultPayloadProvider.Name, StringComparer.OrdinalIgnoreCase))
				{
					bool flag = await heartbeatDefaultPayloadProvider.SetDefaultPayload(disabledFields, provider).ConfigureAwait(false);
					populatedFields = populatedFields || flag;
				}
			}
			IHeartbeatDefaultPayloadProvider[] array = null;
			return populatedFields;
		}

		// Token: 0x04000207 RID: 519
		internal static readonly IHeartbeatDefaultPayloadProvider[] DefaultPayloadProviders = new IHeartbeatDefaultPayloadProvider[]
		{
			new BaseDefaultHeartbeatPropertyProvider()
		};
	}
}
