using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002BA RID: 698
	[EditorBrowsable(1)]
	public abstract class SubscriptionBase
	{
		// Token: 0x060018DF RID: 6367 RVA: 0x00043E79 File Offset: 0x00042E79
		internal SubscriptionBase(ExchangeService service)
		{
			EwsUtilities.ValidateParam(service, "service");
			this.service = service;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x00043E93 File Offset: 0x00042E93
		internal SubscriptionBase(ExchangeService service, string id)
			: this(service)
		{
			EwsUtilities.ValidateParam(id, "id");
			this.id = id;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x00043EAE File Offset: 0x00042EAE
		internal SubscriptionBase(ExchangeService service, string id, string watermark)
			: this(service, id)
		{
			this.watermark = watermark;
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x00043EBF File Offset: 0x00042EBF
		internal virtual void LoadFromXml(EwsServiceXmlReader reader)
		{
			this.id = reader.ReadElementValue(XmlNamespace.Messages, "SubscriptionId");
			if (this.UsesWatermark)
			{
				this.watermark = reader.ReadElementValue(XmlNamespace.Messages, "Watermark");
			}
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x00043EED File Offset: 0x00042EED
		internal virtual void LoadFromJson(JsonObject jsonResponse, ExchangeService service)
		{
			this.id = jsonResponse.ReadAsString("SubscriptionId");
			if (this.UsesWatermark)
			{
				this.watermark = jsonResponse.ReadAsString("Watermark");
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x00043F19 File Offset: 0x00042F19
		internal ExchangeService Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x00043F21 File Offset: 0x00042F21
		// (set) Token: 0x060018E6 RID: 6374 RVA: 0x00043F29 File Offset: 0x00042F29
		public string Id
		{
			get
			{
				return this.id;
			}
			internal set
			{
				this.id = value;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x00043F32 File Offset: 0x00042F32
		// (set) Token: 0x060018E8 RID: 6376 RVA: 0x00043F3A File Offset: 0x00042F3A
		public string Watermark
		{
			get
			{
				return this.watermark;
			}
			internal set
			{
				this.watermark = value;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x00043F43 File Offset: 0x00042F43
		protected virtual bool UsesWatermark
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040013DE RID: 5086
		private ExchangeService service;

		// Token: 0x040013DF RID: 5087
		private string id;

		// Token: 0x040013E0 RID: 5088
		private string watermark;
	}
}
