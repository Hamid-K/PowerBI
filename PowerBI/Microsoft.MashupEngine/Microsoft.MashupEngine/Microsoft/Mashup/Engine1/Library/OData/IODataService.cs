using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x0200072B RID: 1835
	internal interface IODataService
	{
		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06003695 RID: 13973
		IEngineHost Host { get; }

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06003696 RID: 13974
		IResource Resource { get; }

		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06003697 RID: 13975
		Uri ServiceUri { get; }

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06003698 RID: 13976
		ResourceCredentialCollection Credentials { get; }

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06003699 RID: 13977
		ODataUserSettings UserSettings { get; }
	}
}
