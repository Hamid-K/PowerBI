using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Resources;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000735 RID: 1845
	internal class ODataSettingsBasedODataFallbackVersionHandler : ODataFallbackVersionHandler
	{
		// Token: 0x060036F4 RID: 14068 RVA: 0x000AF3C6 File Offset: 0x000AD5C6
		public ODataSettingsBasedODataFallbackVersionHandler(IEngineHost host, HttpResource resource, Uri uri, ODataSettingsBase settingsBase)
			: base(host, resource, uri)
		{
			this.settingsBase = settingsBase;
		}

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x060036F5 RID: 14069 RVA: 0x000AF3D9 File Offset: 0x000AD5D9
		// (set) Token: 0x060036F6 RID: 14070 RVA: 0x000AF3E6 File Offset: 0x000AD5E6
		public override ODataServerVersion ServerVersion
		{
			get
			{
				return this.settingsBase.ServerVersion;
			}
			set
			{
				this.settingsBase.ServerVersion = value;
			}
		}

		// Token: 0x04001C3A RID: 7226
		private ODataSettingsBase settingsBase;
	}
}
