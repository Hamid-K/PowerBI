using System;
using System.Reflection;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009BC RID: 2492
	internal static class EntityClientUtils
	{
		// Token: 0x06004D0B RID: 19723 RVA: 0x00134C8C File Offset: 0x00132E8C
		internal static object ProviderServicesInstance()
		{
			if (EntityClientUtils.ProviderServicesInstanceFieldInfo == null)
			{
				Type type = Type.GetType("Microsoft.HostIntegration.EntityClient.DrdaProviderServices, Microsoft.HostIntegration.DrdaEntityClient, Version=10.0.1000.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", false);
				if (type != null)
				{
					EntityClientUtils.ProviderServicesInstanceFieldInfo = type.GetField("Instance", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
				}
			}
			object obj = null;
			if (EntityClientUtils.ProviderServicesInstanceFieldInfo != null)
			{
				obj = EntityClientUtils.ProviderServicesInstanceFieldInfo.GetValue(null);
			}
			return obj;
		}

		// Token: 0x04003D07 RID: 15623
		private const string SystemDataCommonDbProviderServicesTypeName = "System.Data.Common.DbProviderServices, System.Data.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04003D08 RID: 15624
		private const string DrdaProviderServicesTypeName = "Microsoft.HostIntegration.EntityClient.DrdaProviderServices, Microsoft.HostIntegration.DrdaEntityClient, Version=10.0.1000.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

		// Token: 0x04003D09 RID: 15625
		private static FieldInfo ProviderServicesInstanceFieldInfo;

		// Token: 0x04003D0A RID: 15626
		internal static Type SystemDataCommonDbProviderServicesType = Type.GetType("System.Data.Common.DbProviderServices, System.Data.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", false);
	}
}
