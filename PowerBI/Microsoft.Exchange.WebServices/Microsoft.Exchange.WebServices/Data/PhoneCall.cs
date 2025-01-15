using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000309 RID: 777
	public sealed class PhoneCall : ComplexProperty
	{
		// Token: 0x06001B9B RID: 7067 RVA: 0x000499D4 File Offset: 0x000489D4
		internal PhoneCall(ExchangeService service)
		{
			EwsUtilities.Assert(service != null, "PhoneCall.ctor", "service is null");
			this.service = service;
			this.state = PhoneCallState.Connecting;
			this.connectionFailureCause = ConnectionFailureCause.None;
			this.sipResponseText = "OK";
			this.sipResponseCode = 200;
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x00049A28 File Offset: 0x00048A28
		internal PhoneCall(ExchangeService service, PhoneCallId id)
			: this(service)
		{
			this.id = id;
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x00049A38 File Offset: 0x00048A38
		public void Refresh()
		{
			PhoneCall phoneCallInformation = this.service.UnifiedMessaging.GetPhoneCallInformation(this.id);
			this.state = phoneCallInformation.State;
			this.connectionFailureCause = phoneCallInformation.ConnectionFailureCause;
			this.sipResponseText = phoneCallInformation.SIPResponseText;
			this.sipResponseCode = phoneCallInformation.SIPResponseCode;
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x00049A8C File Offset: 0x00048A8C
		public void Disconnect()
		{
			if (this.state == PhoneCallState.Disconnected)
			{
				throw new ServiceLocalException(Strings.PhoneCallAlreadyDisconnected);
			}
			this.service.UnifiedMessaging.DisconnectPhoneCall(this.id);
			this.state = PhoneCallState.Disconnected;
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x00049AC4 File Offset: 0x00048AC4
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "PhoneCallState")
				{
					this.state = reader.ReadElementValue<PhoneCallState>();
					return true;
				}
				if (localName == "ConnectionFailureCause")
				{
					this.connectionFailureCause = reader.ReadElementValue<ConnectionFailureCause>();
					return true;
				}
				if (localName == "SIPResponseText")
				{
					this.sipResponseText = reader.ReadElementValue();
					return true;
				}
				if (localName == "SIPResponseCode")
				{
					this.sipResponseCode = reader.ReadElementValue<int>();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x00049B4C File Offset: 0x00048B4C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "PhoneCallState"))
					{
						if (!(text2 == "ConnectionFailureCause"))
						{
							if (!(text2 == "SIPResponseText"))
							{
								if (text2 == "SIPResponseCode")
								{
									this.sipResponseCode = jsonProperty.ReadAsInt(text);
								}
							}
							else
							{
								this.sipResponseText = jsonProperty.ReadAsString(text);
							}
						}
						else
						{
							this.connectionFailureCause = jsonProperty.ReadEnumValue<ConnectionFailureCause>(text);
						}
					}
					else
					{
						this.state = jsonProperty.ReadEnumValue<PhoneCallState>(text);
					}
				}
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x00049C10 File Offset: 0x00048C10
		public PhoneCallState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x00049C18 File Offset: 0x00048C18
		public ConnectionFailureCause ConnectionFailureCause
		{
			get
			{
				return this.connectionFailureCause;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x00049C20 File Offset: 0x00048C20
		public string SIPResponseText
		{
			get
			{
				return this.sipResponseText;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x00049C28 File Offset: 0x00048C28
		public int SIPResponseCode
		{
			get
			{
				return this.sipResponseCode;
			}
		}

		// Token: 0x0400145E RID: 5214
		private const string SuccessfulResponseText = "OK";

		// Token: 0x0400145F RID: 5215
		private const int SuccessfulResponseCode = 200;

		// Token: 0x04001460 RID: 5216
		private ExchangeService service;

		// Token: 0x04001461 RID: 5217
		private PhoneCallState state;

		// Token: 0x04001462 RID: 5218
		private ConnectionFailureCause connectionFailureCause;

		// Token: 0x04001463 RID: 5219
		private string sipResponseText;

		// Token: 0x04001464 RID: 5220
		private int sipResponseCode;

		// Token: 0x04001465 RID: 5221
		private PhoneCallId id;
	}
}
