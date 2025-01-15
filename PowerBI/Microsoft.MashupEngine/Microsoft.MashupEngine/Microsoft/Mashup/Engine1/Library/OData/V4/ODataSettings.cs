using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000894 RID: 2196
	internal sealed class ODataSettings : ODataSettingsBase
	{
		// Token: 0x06003F0C RID: 16140 RVA: 0x000B4706 File Offset: 0x000B2906
		public ODataSettings(IEngineHost host, HttpResource resource, Uri uri)
			: base(host, resource, uri)
		{
			base.ServerVersion = ODataServerVersion.V4;
		}

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x06003F0D RID: 16141 RVA: 0x000CECDF File Offset: 0x000CCEDF
		// (set) Token: 0x06003F0E RID: 16142 RVA: 0x000CECE7 File Offset: 0x000CCEE7
		public Microsoft.OData.Edm.IEdmModel EdmModel { get; set; }

		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x06003F0F RID: 16143 RVA: 0x000CECF0 File Offset: 0x000CCEF0
		protected override bool HasEdmModel
		{
			get
			{
				return this.EdmModel != null;
			}
		}
	}
}
