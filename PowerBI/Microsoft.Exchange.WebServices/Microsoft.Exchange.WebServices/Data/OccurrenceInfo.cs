using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200007D RID: 125
	public sealed class OccurrenceInfo : ComplexProperty
	{
		// Token: 0x060005A2 RID: 1442 RVA: 0x0001362E File Offset: 0x0001262E
		internal OccurrenceInfo()
		{
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00013638 File Offset: 0x00012638
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "ItemId")
				{
					this.itemId = new ItemId();
					this.itemId.LoadFromXml(reader, reader.LocalName);
					return true;
				}
				if (localName == "Start")
				{
					this.start = reader.ReadElementValueAsDateTime().Value;
					return true;
				}
				if (localName == "End")
				{
					this.end = reader.ReadElementValueAsDateTime().Value;
					return true;
				}
				if (localName == "OriginalStart")
				{
					this.originalStart = reader.ReadElementValueAsDateTime().Value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000136EC File Offset: 0x000126EC
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "ItemId"))
					{
						if (!(text2 == "Start"))
						{
							if (!(text2 == "End"))
							{
								if (text2 == "OriginalStart")
								{
									this.originalStart = service.ConvertUniversalDateTimeStringToLocalDateTime(jsonProperty.ReadAsString(text)).Value;
								}
							}
							else
							{
								this.end = service.ConvertUniversalDateTimeStringToLocalDateTime(jsonProperty.ReadAsString(text)).Value;
							}
						}
						else
						{
							this.start = service.ConvertUniversalDateTimeStringToLocalDateTime(jsonProperty.ReadAsString(text)).Value;
						}
					}
					else
					{
						this.itemId = new ItemId();
						this.itemId.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
					}
				}
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x000137F4 File Offset: 0x000127F4
		public ItemId ItemId
		{
			get
			{
				return this.itemId;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x000137FC File Offset: 0x000127FC
		public DateTime Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00013804 File Offset: 0x00012804
		public DateTime End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001380C File Offset: 0x0001280C
		public DateTime OriginalStart
		{
			get
			{
				return this.originalStart;
			}
		}

		// Token: 0x040001E5 RID: 485
		private ItemId itemId;

		// Token: 0x040001E6 RID: 486
		private DateTime start;

		// Token: 0x040001E7 RID: 487
		private DateTime end;

		// Token: 0x040001E8 RID: 488
		private DateTime originalStart;
	}
}
