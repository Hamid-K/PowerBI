using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000A6 RID: 166
	public sealed class CalendarEventDetails : ComplexProperty
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x0001938C File Offset: 0x0001838C
		internal CalendarEventDetails()
		{
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00019394 File Offset: 0x00018394
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000714-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(8);
					dictionary.Add("ID", 0);
					dictionary.Add("Subject", 1);
					dictionary.Add("Location", 2);
					dictionary.Add("IsMeeting", 3);
					dictionary.Add("IsRecurring", 4);
					dictionary.Add("IsException", 5);
					dictionary.Add("IsReminderSet", 6);
					dictionary.Add("IsPrivate", 7);
					<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000714-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000714-1.TryGetValue(localName, ref num))
				{
					switch (num)
					{
					case 0:
						this.storeId = reader.ReadElementValue();
						return true;
					case 1:
						this.subject = reader.ReadElementValue();
						return true;
					case 2:
						this.location = reader.ReadElementValue();
						return true;
					case 3:
						this.isMeeting = reader.ReadElementValue<bool>();
						return true;
					case 4:
						this.isRecurring = reader.ReadElementValue<bool>();
						return true;
					case 5:
						this.isException = reader.ReadElementValue<bool>();
						return true;
					case 6:
						this.isReminderSet = reader.ReadElementValue<bool>();
						return true;
					case 7:
						this.isPrivate = reader.ReadElementValue<bool>();
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000194D4 File Offset: 0x000184D4
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000715-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(8);
						dictionary.Add("ID", 0);
						dictionary.Add("Subject", 1);
						dictionary.Add("Location", 2);
						dictionary.Add("IsMeeting", 3);
						dictionary.Add("IsRecurring", 4);
						dictionary.Add("IsException", 5);
						dictionary.Add("IsReminderSet", 6);
						dictionary.Add("IsPrivate", 7);
						<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000715-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000715-1.TryGetValue(text2, ref num))
					{
						switch (num)
						{
						case 0:
							this.storeId = jsonProperty.ReadAsString(text);
							break;
						case 1:
							this.subject = jsonProperty.ReadAsString(text);
							break;
						case 2:
							this.location = jsonProperty.ReadAsString(text);
							break;
						case 3:
							this.isMeeting = jsonProperty.ReadAsBool(text);
							break;
						case 4:
							this.isRecurring = jsonProperty.ReadAsBool(text);
							break;
						case 5:
							this.isException = jsonProperty.ReadAsBool(text);
							break;
						case 6:
							this.isReminderSet = jsonProperty.ReadAsBool(text);
							break;
						case 7:
							this.isPrivate = jsonProperty.ReadAsBool(text);
							break;
						}
					}
				}
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00019664 File Offset: 0x00018664
		public string StoreId
		{
			get
			{
				return this.storeId;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0001966C File Offset: 0x0001866C
		public string Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x00019674 File Offset: 0x00018674
		public string Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0001967C File Offset: 0x0001867C
		public bool IsMeeting
		{
			get
			{
				return this.isMeeting;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00019684 File Offset: 0x00018684
		public bool IsRecurring
		{
			get
			{
				return this.isRecurring;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0001968C File Offset: 0x0001868C
		public bool IsException
		{
			get
			{
				return this.isException;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00019694 File Offset: 0x00018694
		public bool IsReminderSet
		{
			get
			{
				return this.isReminderSet;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0001969C File Offset: 0x0001869C
		public bool IsPrivate
		{
			get
			{
				return this.isPrivate;
			}
		}

		// Token: 0x0400026E RID: 622
		private string storeId;

		// Token: 0x0400026F RID: 623
		private string subject;

		// Token: 0x04000270 RID: 624
		private string location;

		// Token: 0x04000271 RID: 625
		private bool isMeeting;

		// Token: 0x04000272 RID: 626
		private bool isRecurring;

		// Token: 0x04000273 RID: 627
		private bool isException;

		// Token: 0x04000274 RID: 628
		private bool isReminderSet;

		// Token: 0x04000275 RID: 629
		private bool isPrivate;
	}
}
