using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000165 RID: 357
	public sealed class GetDiscoverySearchConfigurationResponse : ServiceResponse
	{
		// Token: 0x060010A1 RID: 4257 RVA: 0x00030F7D File Offset: 0x0002FF7D
		internal GetDiscoverySearchConfigurationResponse()
		{
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00030F90 File Offset: 0x0002FF90
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			this.configurations.Clear();
			base.ReadElementsFromXml(reader);
			reader.ReadStartElement(XmlNamespace.Messages, "DiscoverySearchConfigurations");
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.IsStartElement(XmlNamespace.Types, "DiscoverySearchConfiguration"))
					{
						this.configurations.Add(DiscoverySearchConfiguration.LoadFromXml(reader));
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Messages, "DiscoverySearchConfigurations"));
			}
			reader.ReadEndElementIfNecessary(XmlNamespace.Messages, "DiscoverySearchConfigurations");
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00031004 File Offset: 0x00030004
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			this.configurations.Clear();
			base.ReadElementsFromJson(responseObject, service);
			if (responseObject.ContainsKey("DiscoverySearchConfigurations"))
			{
				foreach (object obj in responseObject.ReadAsArray("DiscoverySearchConfigurations"))
				{
					JsonObject jsonObject = obj as JsonObject;
					this.configurations.Add(DiscoverySearchConfiguration.LoadFromJson(jsonObject));
				}
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x00031067 File Offset: 0x00030067
		public DiscoverySearchConfiguration[] DiscoverySearchConfigurations
		{
			get
			{
				return this.configurations.ToArray();
			}
		}

		// Token: 0x040009B8 RID: 2488
		private List<DiscoverySearchConfiguration> configurations = new List<DiscoverySearchConfiguration>();
	}
}
