using System;
using Microsoft.Data.Edm;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008D8 RID: 2264
	internal class ODataSettings : ODataSettingsBase
	{
		// Token: 0x060040A6 RID: 16550 RVA: 0x000D7EDE File Offset: 0x000D60DE
		public ODataSettings(IEngineHost host, HttpResource resource, Uri uri)
			: base(host, resource, uri)
		{
			base.ServerVersion = ODataServerVersion.V3;
		}

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x060040A7 RID: 16551 RVA: 0x000D7EF0 File Offset: 0x000D60F0
		// (set) Token: 0x060040A8 RID: 16552 RVA: 0x000D7EF8 File Offset: 0x000D60F8
		public IEdmModel EdmModel { get; set; }

		// Token: 0x060040A9 RID: 16553 RVA: 0x000D7F04 File Offset: 0x000D6104
		public void UpdateModel(Uri serviceLocation, Uri metadataLocation, Value headers, HttpResource resource, ResourceCredentialCollection credentials, ODataUserSettings userSettings)
		{
			if (this.EdmModel == null)
			{
				if (metadataLocation == null)
				{
					metadataLocation = ODataUri.CreateMetadataUri(serviceLocation);
				}
				bool flag;
				this.EdmModel = Http.GetServiceMetadataDocument(resource, metadataLocation, headers, credentials, base.FallbackHandler.Host, this, userSettings, out flag);
				base.IsSharePoint = flag;
			}
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x060040AA RID: 16554 RVA: 0x000D7F52 File Offset: 0x000D6152
		protected override bool HasEdmModel
		{
			get
			{
				return this.EdmModel != null;
			}
		}
	}
}
