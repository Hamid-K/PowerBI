using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200005F RID: 95
	public sealed class Flag : ComplexProperty
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x0000F830 File Offset: 0x0000E830
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "FlagStatus")
				{
					this.flagStatus = reader.ReadElementValue<ItemFlagStatus>();
					return true;
				}
				if (localName == "StartDate")
				{
					this.startDate = reader.ReadElementValueAsDateTime().Value;
					return true;
				}
				if (localName == "DueDate")
				{
					this.dueDate = reader.ReadElementValueAsDateTime().Value;
					return true;
				}
				if (localName == "CompleteDate")
				{
					this.completeDate = reader.ReadElementValueAsDateTime().Value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000F8D4 File Offset: 0x0000E8D4
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "FlagStatus"))
					{
						if (!(text2 == "StartDate"))
						{
							if (!(text2 == "DueDate"))
							{
								if (text2 == "CompleteDate")
								{
									this.completeDate = service.ConvertUniversalDateTimeStringToLocalDateTime(jsonProperty.ReadAsString(text)).Value;
								}
							}
							else
							{
								this.dueDate = service.ConvertUniversalDateTimeStringToLocalDateTime(jsonProperty.ReadAsString(text)).Value;
							}
						}
						else
						{
							this.startDate = service.ConvertUniversalDateTimeStringToLocalDateTime(jsonProperty.ReadAsString(text)).Value;
						}
					}
					else
					{
						this.flagStatus = jsonProperty.ReadEnumValue<ItemFlagStatus>(text);
					}
				}
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000F9CC File Offset: 0x0000E9CC
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Types, "FlagStatus", this.FlagStatus);
			if (this.FlagStatus == ItemFlagStatus.Flagged)
			{
				DateTime dateTime = this.StartDate;
				DateTime dateTime2 = this.DueDate;
				writer.WriteElementValue(XmlNamespace.Types, "StartDate", this.StartDate);
				writer.WriteElementValue(XmlNamespace.Types, "DueDate", this.DueDate);
				return;
			}
			if (this.FlagStatus == ItemFlagStatus.Complete)
			{
				DateTime dateTime3 = this.CompleteDate;
				writer.WriteElementValue(XmlNamespace.Types, "CompleteDate", this.CompleteDate);
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000FA60 File Offset: 0x0000EA60
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("FlagStatus", this.FlagStatus);
			if (this.FlagStatus == ItemFlagStatus.Flagged)
			{
				DateTime dateTime = this.StartDate;
				DateTime dateTime2 = this.DueDate;
				jsonObject.Add("StartDate", this.StartDate);
				jsonObject.Add("DueDate", this.DueDate);
			}
			else if (this.FlagStatus == ItemFlagStatus.Complete)
			{
				DateTime dateTime3 = this.CompleteDate;
				jsonObject.Add("CompleteDate", this.CompleteDate);
			}
			return jsonObject;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000FAF5 File Offset: 0x0000EAF5
		internal void Validate()
		{
			EwsUtilities.ValidateParam(this.flagStatus, "FlagStatus");
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000FB0C File Offset: 0x0000EB0C
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0000FB14 File Offset: 0x0000EB14
		public ItemFlagStatus FlagStatus
		{
			get
			{
				return this.flagStatus;
			}
			set
			{
				this.SetFieldValue<ItemFlagStatus>(ref this.flagStatus, value);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000FB23 File Offset: 0x0000EB23
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x0000FB2B File Offset: 0x0000EB2B
		public DateTime StartDate
		{
			get
			{
				return this.startDate;
			}
			set
			{
				this.SetFieldValue<DateTime>(ref this.startDate, value);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000FB3A File Offset: 0x0000EB3A
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x0000FB42 File Offset: 0x0000EB42
		public DateTime DueDate
		{
			get
			{
				return this.dueDate;
			}
			set
			{
				this.SetFieldValue<DateTime>(ref this.dueDate, value);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000FB51 File Offset: 0x0000EB51
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x0000FB59 File Offset: 0x0000EB59
		public DateTime CompleteDate
		{
			get
			{
				return this.completeDate;
			}
			set
			{
				this.SetFieldValue<DateTime>(ref this.completeDate, value);
			}
		}

		// Token: 0x04000195 RID: 405
		private ItemFlagStatus flagStatus;

		// Token: 0x04000196 RID: 406
		private DateTime startDate;

		// Token: 0x04000197 RID: 407
		private DateTime dueDate;

		// Token: 0x04000198 RID: 408
		private DateTime completeDate;
	}
}
