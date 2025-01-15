using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Resources;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000736 RID: 1846
	internal class StandaloneODataFallbackVersionHandler : ODataFallbackVersionHandler
	{
		// Token: 0x060036F7 RID: 14071 RVA: 0x000AF3F4 File Offset: 0x000AD5F4
		public StandaloneODataFallbackVersionHandler(IEngineHost host, HttpResource resource, Uri uri, ODataServerVersion version)
			: base(host, resource, uri)
		{
			this.serverVersion = version;
		}

		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x000AF407 File Offset: 0x000AD607
		// (set) Token: 0x060036F9 RID: 14073 RVA: 0x000AF40F File Offset: 0x000AD60F
		public override ODataServerVersion ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
			set
			{
				this.serverVersion = value;
			}
		}

		// Token: 0x04001C3B RID: 7227
		private ODataServerVersion serverVersion;
	}
}
