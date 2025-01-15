using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000175 RID: 373
	internal sealed class GetUserConfigurationResponse : ServiceResponse
	{
		// Token: 0x060010E0 RID: 4320 RVA: 0x0003188C File Offset: 0x0003088C
		internal GetUserConfigurationResponse(UserConfiguration userConfiguration)
		{
			EwsUtilities.Assert(userConfiguration != null, "GetUserConfigurationResponse.ctor", "userConfiguration is null");
			this.userConfiguration = userConfiguration;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x000318B1 File Offset: 0x000308B1
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			this.userConfiguration.LoadFromXml(reader);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x000318C6 File Offset: 0x000308C6
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			this.UserConfiguration.LoadFromJson(responseObject.ReadAsJsonObject("UserConfiguration"), service);
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x000318E7 File Offset: 0x000308E7
		public UserConfiguration UserConfiguration
		{
			get
			{
				return this.userConfiguration;
			}
		}

		// Token: 0x040009CF RID: 2511
		private UserConfiguration userConfiguration;
	}
}
