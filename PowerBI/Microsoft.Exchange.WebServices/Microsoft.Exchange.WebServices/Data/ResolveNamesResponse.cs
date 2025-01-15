using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200017C RID: 380
	internal sealed class ResolveNamesResponse : ServiceResponse
	{
		// Token: 0x060010FB RID: 4347 RVA: 0x00031BE6 File Offset: 0x00030BE6
		internal ResolveNamesResponse(ExchangeService service)
		{
			EwsUtilities.Assert(service != null, "ResolveNamesResponse.ctor", "service is null");
			this.resolutions = new NameResolutionCollection(service);
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00031C10 File Offset: 0x00030C10
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			this.Resolutions.LoadFromXml(reader);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00031C25 File Offset: 0x00030C25
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			this.Resolutions.LoadFromJson(responseObject.ReadAsJsonObject("ResolutionSet"), service);
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00031C46 File Offset: 0x00030C46
		internal override void InternalThrowIfNecessary()
		{
			if (base.ErrorCode != ServiceError.ErrorNameResolutionNoResults)
			{
				base.InternalThrowIfNecessary();
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x00031C5B File Offset: 0x00030C5B
		public NameResolutionCollection Resolutions
		{
			get
			{
				return this.resolutions;
			}
		}

		// Token: 0x040009D5 RID: 2517
		private NameResolutionCollection resolutions;
	}
}
