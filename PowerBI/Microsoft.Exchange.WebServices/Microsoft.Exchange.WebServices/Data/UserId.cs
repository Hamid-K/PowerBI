using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000090 RID: 144
	public sealed class UserId : ComplexProperty
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x000162C0 File Offset: 0x000152C0
		public UserId()
		{
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x000162C8 File Offset: 0x000152C8
		public UserId(string primarySmtpAddress)
			: this()
		{
			this.primarySmtpAddress = primarySmtpAddress;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000162D7 File Offset: 0x000152D7
		public UserId(StandardUser standardUser)
			: this()
		{
			this.standardUser = new StandardUser?(standardUser);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x000162EC File Offset: 0x000152EC
		internal bool IsValid()
		{
			return this.StandardUser != null || !string.IsNullOrEmpty(this.PrimarySmtpAddress) || !string.IsNullOrEmpty(this.SID);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00016326 File Offset: 0x00015326
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x0001632E File Offset: 0x0001532E
		public string SID
		{
			get
			{
				return this.sID;
			}
			set
			{
				this.SetFieldValue<string>(ref this.sID, value);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001633D File Offset: 0x0001533D
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x00016345 File Offset: 0x00015345
		public string PrimarySmtpAddress
		{
			get
			{
				return this.primarySmtpAddress;
			}
			set
			{
				this.SetFieldValue<string>(ref this.primarySmtpAddress, value);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00016354 File Offset: 0x00015354
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x0001635C File Offset: 0x0001535C
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.SetFieldValue<string>(ref this.displayName, value);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001636B File Offset: 0x0001536B
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x00016373 File Offset: 0x00015373
		public StandardUser? StandardUser
		{
			get
			{
				return this.standardUser;
			}
			set
			{
				this.SetFieldValue<StandardUser?>(ref this.standardUser, value);
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00016382 File Offset: 0x00015382
		public static implicit operator UserId(string primarySmtpAddress)
		{
			return new UserId(primarySmtpAddress);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001638A File Offset: 0x0001538A
		public static implicit operator UserId(StandardUser standardUser)
		{
			return new UserId(standardUser);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00016394 File Offset: 0x00015394
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "SID")
				{
					this.sID = reader.ReadValue();
					return true;
				}
				if (localName == "PrimarySmtpAddress")
				{
					this.primarySmtpAddress = reader.ReadValue();
					return true;
				}
				if (localName == "DisplayName")
				{
					this.displayName = reader.ReadValue();
					return true;
				}
				if (localName == "DistinguishedUser")
				{
					this.standardUser = new StandardUser?(reader.ReadValue<StandardUser>());
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00016420 File Offset: 0x00015420
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "SID"))
					{
						if (!(text2 == "PrimarySmtpAddress"))
						{
							if (!(text2 == "DisplayName"))
							{
								if (text2 == "DistinguishedUser")
								{
									this.standardUser = new StandardUser?(jsonProperty.ReadEnumValue<StandardUser>(text));
								}
							}
							else
							{
								this.displayName = jsonProperty.ReadAsString(text);
							}
						}
						else
						{
							this.primarySmtpAddress = jsonProperty.ReadAsString(text);
						}
					}
					else
					{
						this.sID = jsonProperty.ReadAsString(text);
					}
				}
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000164EC File Offset: 0x000154EC
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Types, "SID", this.SID);
			writer.WriteElementValue(XmlNamespace.Types, "PrimarySmtpAddress", this.PrimarySmtpAddress);
			writer.WriteElementValue(XmlNamespace.Types, "DisplayName", this.DisplayName);
			writer.WriteElementValue(XmlNamespace.Types, "DistinguishedUser", this.StandardUser);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00016548 File Offset: 0x00015548
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("SID", this.SID);
			jsonObject.Add("PrimarySmtpAddress", this.PrimarySmtpAddress);
			jsonObject.Add("DisplayName", this.DisplayName);
			if (this.StandardUser != null)
			{
				jsonObject.Add("DistinguishedUser", this.StandardUser.Value);
			}
			return jsonObject;
		}

		// Token: 0x04000215 RID: 533
		private string sID;

		// Token: 0x04000216 RID: 534
		private string primarySmtpAddress;

		// Token: 0x04000217 RID: 535
		private string displayName;

		// Token: 0x04000218 RID: 536
		private StandardUser? standardUser;
	}
}
