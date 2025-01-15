using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002DA RID: 730
	internal class AppSettingsInMemoryProvider : AppSettingsBaseProvider
	{
		// Token: 0x06001381 RID: 4993 RVA: 0x00043AC4 File Offset: 0x00041CC4
		public AppSettingsInMemoryProvider(string id, AppSettingsBaseProvider.OnAppSettingsChanged onAppSettingsChanged)
			: base(id, onAppSettingsChanged)
		{
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00043ACE File Offset: 0x00041CCE
		public new void SetNameValue(string name, string value)
		{
			base.SetNameValue(name, value);
			base.InvokedOnAppSettingsChanged();
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00043ADE File Offset: 0x00041CDE
		public override string ToString()
		{
			return string.Concat(new object[] { "AppSettingsInMemoryProvider (", base.Name, "): ", base.CountForDebugging, " items" });
		}
	}
}
