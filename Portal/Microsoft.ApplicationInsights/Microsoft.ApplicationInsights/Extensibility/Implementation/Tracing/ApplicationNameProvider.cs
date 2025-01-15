using System;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x02000091 RID: 145
	internal sealed class ApplicationNameProvider
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x00013C12 File Offset: 0x00011E12
		public ApplicationNameProvider()
		{
			this.Name = ApplicationNameProvider.GetApplicationName();
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00013C25 File Offset: 0x00011E25
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00013C2D File Offset: 0x00011E2D
		public string Name { get; private set; }

		// Token: 0x0600047E RID: 1150 RVA: 0x00013C38 File Offset: 0x00011E38
		private static string GetApplicationName()
		{
			string text;
			try
			{
				text = AppDomain.CurrentDomain.FriendlyName;
			}
			catch (Exception ex)
			{
				text = "Undefined " + ex.Message;
			}
			return text;
		}
	}
}
