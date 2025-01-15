using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000036 RID: 54
	public abstract class Attachment : ComplexProperty
	{
		// Token: 0x06000265 RID: 613 RVA: 0x0000A1A3 File Offset: 0x000091A3
		internal Attachment(Item owner)
		{
			this.owner = owner;
			if (owner != null)
			{
				this.service = this.owner.Service;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000A1C6 File Offset: 0x000091C6
		internal Attachment(ExchangeService service)
		{
			this.service = service;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000A1D5 File Offset: 0x000091D5
		internal void ThrowIfThisIsNotNew()
		{
			if (!this.IsNew)
			{
				throw new InvalidOperationException(Strings.AttachmentCannotBeUpdated);
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000A1EF File Offset: 0x000091EF
		internal override void SetFieldValue<T>(ref T field, T value)
		{
			this.ThrowIfThisIsNotNew();
			base.SetFieldValue<T>(ref field, value);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000A1FF File Offset: 0x000091FF
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000A207 File Offset: 0x00009207
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

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000A210 File Offset: 0x00009210
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000A218 File Offset: 0x00009218
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.SetFieldValue<string>(ref this.name, value);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000A227 File Offset: 0x00009227
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000A22F File Offset: 0x0000922F
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
			set
			{
				this.SetFieldValue<string>(ref this.contentType, value);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000A23E File Offset: 0x0000923E
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000A246 File Offset: 0x00009246
		public string ContentId
		{
			get
			{
				return this.contentId;
			}
			set
			{
				this.SetFieldValue<string>(ref this.contentId, value);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000A255 File Offset: 0x00009255
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000A25D File Offset: 0x0000925D
		public string ContentLocation
		{
			get
			{
				return this.contentLocation;
			}
			set
			{
				this.SetFieldValue<string>(ref this.contentLocation, value);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000A26C File Offset: 0x0000926C
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000A285 File Offset: 0x00009285
		public int Size
		{
			get
			{
				EwsUtilities.ValidatePropertyVersion(this.service, ExchangeVersion.Exchange2010, "Size");
				return this.size;
			}
			internal set
			{
				EwsUtilities.ValidatePropertyVersion(this.service, ExchangeVersion.Exchange2010, "Size");
				this.SetFieldValue<int>(ref this.size, value);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000A2A5 File Offset: 0x000092A5
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000A2BE File Offset: 0x000092BE
		public DateTime LastModifiedTime
		{
			get
			{
				EwsUtilities.ValidatePropertyVersion(this.service, ExchangeVersion.Exchange2010, "LastModifiedTime");
				return this.lastModifiedTime;
			}
			internal set
			{
				EwsUtilities.ValidatePropertyVersion(this.service, ExchangeVersion.Exchange2010, "LastModifiedTime");
				this.SetFieldValue<DateTime>(ref this.lastModifiedTime, value);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000A2DE File Offset: 0x000092DE
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000A2F7 File Offset: 0x000092F7
		public bool IsInline
		{
			get
			{
				EwsUtilities.ValidatePropertyVersion(this.service, ExchangeVersion.Exchange2010, "IsInline");
				return this.isInline;
			}
			set
			{
				EwsUtilities.ValidatePropertyVersion(this.service, ExchangeVersion.Exchange2010, "IsInline");
				this.SetFieldValue<bool>(ref this.isInline, value);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000A317 File Offset: 0x00009317
		internal bool IsNew
		{
			get
			{
				return string.IsNullOrEmpty(this.Id);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000A324 File Offset: 0x00009324
		internal Item Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000A32C File Offset: 0x0000932C
		internal ExchangeService Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x0600027C RID: 636
		internal abstract string GetXmlElementName();

		// Token: 0x0600027D RID: 637 RVA: 0x0000A334 File Offset: 0x00009334
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000226-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(8);
					dictionary.Add("AttachmentId", 0);
					dictionary.Add("Name", 1);
					dictionary.Add("ContentType", 2);
					dictionary.Add("ContentId", 3);
					dictionary.Add("ContentLocation", 4);
					dictionary.Add("Size", 5);
					dictionary.Add("LastModifiedTime", 6);
					dictionary.Add("IsInline", 7);
					<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000226-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000226-1.TryGetValue(localName, ref num))
				{
					switch (num)
					{
					case 0:
						this.id = reader.ReadAttributeValue("Id");
						if (this.Owner != null)
						{
							string text = reader.ReadAttributeValue("RootItemChangeKey");
							if (!string.IsNullOrEmpty(text))
							{
								this.Owner.RootItemId.ChangeKey = text;
							}
						}
						reader.ReadEndElementIfNecessary(XmlNamespace.Types, "AttachmentId");
						return true;
					case 1:
						this.name = reader.ReadElementValue();
						return true;
					case 2:
						this.contentType = reader.ReadElementValue();
						return true;
					case 3:
						this.contentId = reader.ReadElementValue();
						return true;
					case 4:
						this.contentLocation = reader.ReadElementValue();
						return true;
					case 5:
						this.size = reader.ReadElementValue<int>();
						return true;
					case 6:
						this.lastModifiedTime = reader.ReadElementValueAsDateTime().Value;
						return true;
					case 7:
						this.isInline = reader.ReadElementValue<bool>();
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A4BC File Offset: 0x000094BC
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000227-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(8);
						dictionary.Add("AttachmentId", 0);
						dictionary.Add("Name", 1);
						dictionary.Add("ContentType", 2);
						dictionary.Add("ContentId", 3);
						dictionary.Add("ContentLocation", 4);
						dictionary.Add("Size", 5);
						dictionary.Add("LastModifiedTime", 6);
						dictionary.Add("IsInline", 7);
						<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000227-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000227-1.TryGetValue(text2, ref num))
					{
						switch (num)
						{
						case 0:
							this.LoadAttachmentIdFromJson(jsonProperty.ReadAsJsonObject(text));
							break;
						case 1:
							this.name = jsonProperty.ReadAsString(text);
							break;
						case 2:
							this.contentType = jsonProperty.ReadAsString(text);
							break;
						case 3:
							this.contentId = jsonProperty.ReadAsString(text);
							break;
						case 4:
							this.contentLocation = jsonProperty.ReadAsString(text);
							break;
						case 5:
							this.size = jsonProperty.ReadAsInt(text);
							break;
						case 6:
							this.lastModifiedTime = service.ConvertUniversalDateTimeStringToLocalDateTime(jsonProperty.ReadAsString(text)).Value;
							break;
						case 7:
							this.isInline = jsonProperty.ReadAsBool(text);
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A65C File Offset: 0x0000965C
		private void LoadAttachmentIdFromJson(JsonObject jsonObject)
		{
			this.id = jsonObject.ReadAsString("Id");
			if (this.Owner != null && jsonObject.ContainsKey("RootItemChangeKey"))
			{
				string text = jsonObject.ReadAsString("RootItemChangeKey");
				if (!string.IsNullOrEmpty(text))
				{
					this.Owner.RootItemId.ChangeKey = text;
				}
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A6B4 File Offset: 0x000096B4
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Types, "Name", this.Name);
			writer.WriteElementValue(XmlNamespace.Types, "ContentType", this.ContentType);
			writer.WriteElementValue(XmlNamespace.Types, "ContentId", this.ContentId);
			writer.WriteElementValue(XmlNamespace.Types, "ContentLocation", this.ContentLocation);
			if (writer.Service.RequestedServerVersion > ExchangeVersion.Exchange2007_SP1)
			{
				writer.WriteElementValue(XmlNamespace.Types, "IsInline", this.IsInline);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A730 File Offset: 0x00009730
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.AddTypeParameter(this.GetXmlElementName());
			jsonObject.Add("Name", this.Name);
			jsonObject.Add("ContentType", this.ContentType);
			jsonObject.Add("ContentId", this.ContentId);
			jsonObject.Add("ContentLocation", this.ContentLocation);
			if (service.RequestedServerVersion > ExchangeVersion.Exchange2007_SP1)
			{
				jsonObject.Add("IsInline", this.IsInline);
			}
			return jsonObject;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A7AE File Offset: 0x000097AE
		internal void InternalLoad(BodyType? bodyType, IEnumerable<PropertyDefinitionBase> additionalProperties)
		{
			this.service.GetAttachment(this, bodyType, additionalProperties);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000A7BE File Offset: 0x000097BE
		internal virtual void Validate(int attachmentIndex)
		{
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000A7C0 File Offset: 0x000097C0
		public void Load()
		{
			this.InternalLoad(default(BodyType?), null);
		}

		// Token: 0x04000122 RID: 290
		private Item owner;

		// Token: 0x04000123 RID: 291
		private string id;

		// Token: 0x04000124 RID: 292
		private string name;

		// Token: 0x04000125 RID: 293
		private string contentType;

		// Token: 0x04000126 RID: 294
		private string contentId;

		// Token: 0x04000127 RID: 295
		private string contentLocation;

		// Token: 0x04000128 RID: 296
		private int size;

		// Token: 0x04000129 RID: 297
		private DateTime lastModifiedTime;

		// Token: 0x0400012A RID: 298
		private bool isInline;

		// Token: 0x0400012B RID: 299
		private ExchangeService service;
	}
}
