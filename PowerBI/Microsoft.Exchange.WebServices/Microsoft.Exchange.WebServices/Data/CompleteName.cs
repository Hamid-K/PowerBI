using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000043 RID: 67
	public sealed class CompleteName : ComplexProperty
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000BE77 File Offset: 0x0000AE77
		public string Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000BE7F File Offset: 0x0000AE7F
		public string GivenName
		{
			get
			{
				return this.givenName;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000BE87 File Offset: 0x0000AE87
		public string MiddleName
		{
			get
			{
				return this.middleName;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000BE8F File Offset: 0x0000AE8F
		public string Surname
		{
			get
			{
				return this.surname;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000BE97 File Offset: 0x0000AE97
		public string Suffix
		{
			get
			{
				return this.suffix;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000BE9F File Offset: 0x0000AE9F
		public string Initials
		{
			get
			{
				return this.initials;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000BEA7 File Offset: 0x0000AEA7
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000BEAF File Offset: 0x0000AEAF
		public string NickName
		{
			get
			{
				return this.nickname;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000BEB7 File Offset: 0x0000AEB7
		public string YomiGivenName
		{
			get
			{
				return this.yomiGivenName;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000BEBF File Offset: 0x0000AEBF
		public string YomiSurname
		{
			get
			{
				return this.yomiSurname;
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000BEC8 File Offset: 0x0000AEC8
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60002bc-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(10);
					dictionary.Add("Title", 0);
					dictionary.Add("FirstName", 1);
					dictionary.Add("MiddleName", 2);
					dictionary.Add("LastName", 3);
					dictionary.Add("Suffix", 4);
					dictionary.Add("Initials", 5);
					dictionary.Add("FullName", 6);
					dictionary.Add("Nickname", 7);
					dictionary.Add("YomiFirstName", 8);
					dictionary.Add("YomiLastName", 9);
					<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60002bc-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60002bc-1.TryGetValue(localName, ref num))
				{
					switch (num)
					{
					case 0:
						this.title = reader.ReadElementValue();
						return true;
					case 1:
						this.givenName = reader.ReadElementValue();
						return true;
					case 2:
						this.middleName = reader.ReadElementValue();
						return true;
					case 3:
						this.surname = reader.ReadElementValue();
						return true;
					case 4:
						this.suffix = reader.ReadElementValue();
						return true;
					case 5:
						this.initials = reader.ReadElementValue();
						return true;
					case 6:
						this.fullName = reader.ReadElementValue();
						return true;
					case 7:
						this.nickname = reader.ReadElementValue();
						return true;
					case 8:
						this.yomiGivenName = reader.ReadElementValue();
						return true;
					case 9:
						this.yomiSurname = reader.ReadElementValue();
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000C04C File Offset: 0x0000B04C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60002bd-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(10);
						dictionary.Add("Title", 0);
						dictionary.Add("FirstName", 1);
						dictionary.Add("MiddleName", 2);
						dictionary.Add("LastName", 3);
						dictionary.Add("Suffix", 4);
						dictionary.Add("Initials", 5);
						dictionary.Add("FullName", 6);
						dictionary.Add("Nickname", 7);
						dictionary.Add("YomiFirstName", 8);
						dictionary.Add("YomiLastName", 9);
						<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60002bd-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60002bd-1.TryGetValue(text2, ref num))
					{
						switch (num)
						{
						case 0:
							this.title = jsonProperty.ReadAsString(text);
							break;
						case 1:
							this.givenName = jsonProperty.ReadAsString(text);
							break;
						case 2:
							this.middleName = jsonProperty.ReadAsString(text);
							break;
						case 3:
							this.surname = jsonProperty.ReadAsString(text);
							break;
						case 4:
							this.suffix = jsonProperty.ReadAsString(text);
							break;
						case 5:
							this.initials = jsonProperty.ReadAsString(text);
							break;
						case 6:
							this.fullName = jsonProperty.ReadAsString(text);
							break;
						case 7:
							this.nickname = jsonProperty.ReadAsString(text);
							break;
						case 8:
							this.yomiGivenName = jsonProperty.ReadAsString(text);
							break;
						case 9:
							this.yomiSurname = jsonProperty.ReadAsString(text);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000C224 File Offset: 0x0000B224
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Types, "Title", this.Title);
			writer.WriteElementValue(XmlNamespace.Types, "FirstName", this.GivenName);
			writer.WriteElementValue(XmlNamespace.Types, "MiddleName", this.MiddleName);
			writer.WriteElementValue(XmlNamespace.Types, "LastName", this.Surname);
			writer.WriteElementValue(XmlNamespace.Types, "Suffix", this.Suffix);
			writer.WriteElementValue(XmlNamespace.Types, "Initials", this.Initials);
			writer.WriteElementValue(XmlNamespace.Types, "FullName", this.FullName);
			writer.WriteElementValue(XmlNamespace.Types, "Nickname", this.NickName);
			writer.WriteElementValue(XmlNamespace.Types, "YomiFirstName", this.YomiGivenName);
			writer.WriteElementValue(XmlNamespace.Types, "YomiLastName", this.YomiSurname);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000C2E8 File Offset: 0x0000B2E8
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Title", this.Title);
			jsonObject.Add("FirstName", this.GivenName);
			jsonObject.Add("MiddleName", this.MiddleName);
			jsonObject.Add("LastName", this.Surname);
			jsonObject.Add("Suffix", this.Suffix);
			jsonObject.Add("Initials", this.Initials);
			jsonObject.Add("FullName", this.FullName);
			jsonObject.Add("Nickname", this.NickName);
			jsonObject.Add("YomiFirstName", this.YomiGivenName);
			jsonObject.Add("YomiLastName", this.YomiSurname);
			return jsonObject;
		}

		// Token: 0x04000151 RID: 337
		private string title;

		// Token: 0x04000152 RID: 338
		private string givenName;

		// Token: 0x04000153 RID: 339
		private string middleName;

		// Token: 0x04000154 RID: 340
		private string surname;

		// Token: 0x04000155 RID: 341
		private string suffix;

		// Token: 0x04000156 RID: 342
		private string initials;

		// Token: 0x04000157 RID: 343
		private string fullName;

		// Token: 0x04000158 RID: 344
		private string nickname;

		// Token: 0x04000159 RID: 345
		private string yomiGivenName;

		// Token: 0x0400015A RID: 346
		private string yomiSurname;
	}
}
