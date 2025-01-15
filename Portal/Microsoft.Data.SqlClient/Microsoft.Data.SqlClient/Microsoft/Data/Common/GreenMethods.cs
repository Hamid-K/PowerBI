using System;
using System.Reflection;
using System.Security.Permissions;

namespace Microsoft.Data.Common
{
	// Token: 0x02000183 RID: 387
	internal static class GreenMethods
	{
		// Token: 0x06001CF5 RID: 7413 RVA: 0x00076470 File Offset: 0x00074670
		internal static object MicrosoftDataSqlClientSqlProviderServices_Instance()
		{
			if (null == GreenMethods.MicrosoftDataSqlClientSqlProviderServices_Instance_FieldInfo)
			{
				Type type = Type.GetType("Microsoft.Data.SqlClient.SQLProviderServices, System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", false);
				if (null != type)
				{
					GreenMethods.MicrosoftDataSqlClientSqlProviderServices_Instance_FieldInfo = type.GetField("Instance", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
				}
			}
			return GreenMethods.MicrosoftDataSqlClientSqlProviderServices_Instance_GetValue();
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x000764B8 File Offset: 0x000746B8
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private static object MicrosoftDataSqlClientSqlProviderServices_Instance_GetValue()
		{
			object obj = null;
			if (null != GreenMethods.MicrosoftDataSqlClientSqlProviderServices_Instance_FieldInfo)
			{
				obj = GreenMethods.MicrosoftDataSqlClientSqlProviderServices_Instance_FieldInfo.GetValue(null);
			}
			return obj;
		}

		// Token: 0x04000C28 RID: 3112
		private const string ExtensionAssemblyRef = "System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000C29 RID: 3113
		private const string SystemDataCommonDbProviderServices_TypeName = "System.Data.Common.DbProviderServices, System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000C2A RID: 3114
		internal static Type SystemDataCommonDbProviderServices_Type = Type.GetType("System.Data.Common.DbProviderServices, System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", false);

		// Token: 0x04000C2B RID: 3115
		private const string MicrosoftDataSqlClientSqlProviderServices_TypeName = "Microsoft.Data.SqlClient.SQLProviderServices, System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000C2C RID: 3116
		private static FieldInfo MicrosoftDataSqlClientSqlProviderServices_Instance_FieldInfo;
	}
}
