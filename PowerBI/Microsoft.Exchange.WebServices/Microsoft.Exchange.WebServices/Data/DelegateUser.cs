using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200004E RID: 78
	public sealed class DelegateUser : ComplexProperty
	{
		// Token: 0x0600037C RID: 892 RVA: 0x0000CF62 File Offset: 0x0000BF62
		public DelegateUser()
		{
			this.receiveCopiesOfMeetingMessages = false;
			this.viewPrivateItems = false;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000CF8E File Offset: 0x0000BF8E
		public DelegateUser(string primarySmtpAddress)
			: this()
		{
			this.userId.PrimarySmtpAddress = primarySmtpAddress;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000CFA2 File Offset: 0x0000BFA2
		public DelegateUser(StandardUser standardUser)
			: this()
		{
			this.userId.StandardUser = new StandardUser?(standardUser);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000CFBB File Offset: 0x0000BFBB
		public UserId UserId
		{
			get
			{
				return this.userId;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000CFC3 File Offset: 0x0000BFC3
		public DelegatePermissions Permissions
		{
			get
			{
				return this.permissions;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000CFCB File Offset: 0x0000BFCB
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000CFD3 File Offset: 0x0000BFD3
		public bool ReceiveCopiesOfMeetingMessages
		{
			get
			{
				return this.receiveCopiesOfMeetingMessages;
			}
			set
			{
				this.receiveCopiesOfMeetingMessages = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000CFDC File Offset: 0x0000BFDC
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000CFE4 File Offset: 0x0000BFE4
		public bool ViewPrivateItems
		{
			get
			{
				return this.viewPrivateItems;
			}
			set
			{
				this.viewPrivateItems = value;
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000CFF0 File Offset: 0x0000BFF0
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "UserId")
				{
					this.userId = new UserId();
					this.userId.LoadFromXml(reader, reader.LocalName);
					return true;
				}
				if (localName == "DelegatePermissions")
				{
					this.permissions.Reset();
					this.permissions.LoadFromXml(reader, reader.LocalName);
					return true;
				}
				if (localName == "ReceiveCopiesOfMeetingMessages")
				{
					this.receiveCopiesOfMeetingMessages = reader.ReadElementValue<bool>();
					return true;
				}
				if (localName == "ViewPrivateItems")
				{
					this.viewPrivateItems = reader.ReadElementValue<bool>();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000D09C File Offset: 0x0000C09C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "UserId"))
					{
						if (!(text2 == "DelegatePermissions"))
						{
							if (!(text2 == "ReceiveCopiesOfMeetingMessages"))
							{
								if (text2 == "ViewPrivateItems")
								{
									this.viewPrivateItems = jsonProperty.ReadAsBool(text);
								}
							}
							else
							{
								this.receiveCopiesOfMeetingMessages = jsonProperty.ReadAsBool(text);
							}
						}
						else
						{
							this.permissions.Reset();
							this.permissions.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
						}
					}
					else
					{
						this.userId = new UserId();
						this.userId.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
					}
				}
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000D188 File Offset: 0x0000C188
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.UserId.WriteToXml(writer, "UserId");
			this.Permissions.WriteToXml(writer, "DelegatePermissions");
			writer.WriteElementValue(XmlNamespace.Types, "ReceiveCopiesOfMeetingMessages", this.ReceiveCopiesOfMeetingMessages);
			writer.WriteElementValue(XmlNamespace.Types, "ViewPrivateItems", this.ViewPrivateItems);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000D1E8 File Offset: 0x0000C1E8
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("UserId", this.UserId.InternalToJson(service));
			jsonObject.Add("DelegatePermissions", this.Permissions.InternalToJson(service));
			jsonObject.Add("ReceiveCopiesOfMeetingMessages", this.ReceiveCopiesOfMeetingMessages);
			jsonObject.Add("ViewPrivateItems", this.ViewPrivateItems);
			return jsonObject;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000D24C File Offset: 0x0000C24C
		internal override void InternalValidate()
		{
			if (this.UserId == null)
			{
				throw new ServiceValidationException(Strings.UserIdForDelegateUserNotSpecified);
			}
			if (!this.UserId.IsValid())
			{
				throw new ServiceValidationException(Strings.DelegateUserHasInvalidUserId);
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000D283 File Offset: 0x0000C283
		internal void ValidateAddDelegate()
		{
			this.permissions.ValidateAddDelegate();
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000D290 File Offset: 0x0000C290
		internal void ValidateUpdateDelegate()
		{
			this.permissions.ValidateUpdateDelegate();
		}

		// Token: 0x04000175 RID: 373
		private UserId userId = new UserId();

		// Token: 0x04000176 RID: 374
		private DelegatePermissions permissions = new DelegatePermissions();

		// Token: 0x04000177 RID: 375
		private bool receiveCopiesOfMeetingMessages;

		// Token: 0x04000178 RID: 376
		private bool viewPrivateItems;
	}
}
