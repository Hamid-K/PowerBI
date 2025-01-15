using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7
{
	// Token: 0x02000779 RID: 1913
	internal sealed class ODataSettings : ODataSettingsBase
	{
		// Token: 0x0600384A RID: 14410 RVA: 0x000B4706 File Offset: 0x000B2906
		public ODataSettings(IEngineHost host, HttpResource resource, Uri uri)
			: base(host, resource, uri)
		{
			base.ServerVersion = ODataServerVersion.V4;
		}

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x0600384B RID: 14411 RVA: 0x000B4718 File Offset: 0x000B2918
		// (set) Token: 0x0600384C RID: 14412 RVA: 0x000B4720 File Offset: 0x000B2920
		public Microsoft.OData.Edm.IEdmModel EdmModel { get; set; }

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x000B4729 File Offset: 0x000B2929
		// (set) Token: 0x0600384E RID: 14414 RVA: 0x000B4731 File Offset: 0x000B2931
		public int EdmModelSize { get; set; }

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x000B473A File Offset: 0x000B293A
		protected override bool HasEdmModel
		{
			get
			{
				return this.EdmModel != null;
			}
		}
	}
}
