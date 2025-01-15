using System;
using System.Reflection;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020006F9 RID: 1785
	public class UserContext : ApplicationInsightsTypeImplement<UserContext>
	{
		// Token: 0x060038C6 RID: 14534 RVA: 0x000BE5BC File Offset: 0x000BC7BC
		static UserContext()
		{
			ApplicationInsightsTypeImplement<UserContext>.Type = TelemetryClient.Assembly.GetType("Microsoft.ApplicationInsights.Extensibility.Implementation.UserContext");
			UserContext._idInfo = ApplicationInsightsTypeImplement<UserContext>.Type.GetProperty("Id");
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x000BE5E6 File Offset: 0x000BC7E6
		internal UserContext(object value)
		{
			if (value != null)
			{
				base.Value = value.GetType().GetProperty("User").GetValue(value);
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x060038C8 RID: 14536 RVA: 0x000BE60D File Offset: 0x000BC80D
		// (set) Token: 0x060038C9 RID: 14537 RVA: 0x000BE624 File Offset: 0x000BC824
		public string Id
		{
			get
			{
				return UserContext._idInfo.GetValue(base.Value) as string;
			}
			set
			{
				UserContext._idInfo.SetValue(base.Value, value);
			}
		}

		// Token: 0x040020F2 RID: 8434
		private static PropertyInfo _idInfo;
	}
}
