using System;
using System.IO;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200005E RID: 94
	public sealed class FileAttachment : Attachment
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x0000F370 File Offset: 0x0000E370
		internal FileAttachment(Item owner)
			: base(owner)
		{
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000F379 File Offset: 0x0000E379
		internal FileAttachment(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000F382 File Offset: 0x0000E382
		internal override string GetXmlElementName()
		{
			return "FileAttachment";
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000F389 File Offset: 0x0000E389
		internal override void Validate(int attachmentIndex)
		{
			if (string.IsNullOrEmpty(this.fileName) && this.content == null && this.contentStream == null)
			{
				throw new ServiceValidationException(string.Format(Strings.FileAttachmentContentIsNotSet, attachmentIndex));
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000F3C4 File Offset: 0x0000E3C4
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			bool flag = base.TryReadElementFromXml(reader);
			if (!flag)
			{
				if (reader.LocalName == "IsContactPhoto")
				{
					this.isContactPhoto = reader.ReadElementValue<bool>();
				}
				else if (reader.LocalName == "Content")
				{
					if (this.loadToStream != null)
					{
						reader.ReadBase64ElementValue(this.loadToStream);
					}
					else if (reader.Service.FileAttachmentContentHandler != null)
					{
						Stream outputStream = reader.Service.FileAttachmentContentHandler.GetOutputStream(base.Id);
						if (outputStream != null)
						{
							reader.ReadBase64ElementValue(outputStream);
						}
						else
						{
							this.content = reader.ReadBase64ElementValue();
						}
					}
					else
					{
						this.content = reader.ReadBase64ElementValue();
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000F474 File Offset: 0x0000E474
		internal override bool TryReadElementFromXmlToPatch(EwsServiceXmlReader reader)
		{
			return base.TryReadElementFromXml(reader);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000F480 File Offset: 0x0000E480
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			base.LoadFromJson(jsonProperty, service);
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "IsContactPhoto"))
					{
						if (text2 == "Content")
						{
							if (this.loadToStream != null)
							{
								jsonProperty.ReadAsBase64Content(text, this.loadToStream);
							}
							else if (service.FileAttachmentContentHandler != null)
							{
								Stream outputStream = service.FileAttachmentContentHandler.GetOutputStream(base.Id);
								if (outputStream != null)
								{
									jsonProperty.ReadAsBase64Content(text, outputStream);
								}
								else
								{
									this.content = jsonProperty.ReadAsBase64Content(text);
								}
							}
							else
							{
								this.content = jsonProperty.ReadAsBase64Content(text);
							}
						}
					}
					else
					{
						this.isContactPhoto = jsonProperty.ReadAsBool(text);
					}
				}
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000F568 File Offset: 0x0000E568
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			base.WriteElementsToXml(writer);
			if (writer.Service.RequestedServerVersion > ExchangeVersion.Exchange2007_SP1)
			{
				writer.WriteElementValue(XmlNamespace.Types, "IsContactPhoto", this.isContactPhoto);
			}
			writer.WriteStartElement(XmlNamespace.Types, "Content");
			if (!string.IsNullOrEmpty(this.FileName))
			{
				using (FileStream fileStream = new FileStream(this.FileName, 3, 1))
				{
					writer.WriteBase64ElementValue(fileStream);
					goto IL_00A2;
				}
			}
			if (this.ContentStream != null)
			{
				writer.WriteBase64ElementValue(this.ContentStream);
			}
			else if (this.Content != null)
			{
				writer.WriteBase64ElementValue(this.Content);
			}
			else
			{
				EwsUtilities.Assert(false, "FileAttachment.WriteElementsToXml", "The attachment's content is not set.");
			}
			IL_00A2:
			writer.WriteEndElement();
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000F630 File Offset: 0x0000E630
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = base.InternalToJson(service) as JsonObject;
			if (service.RequestedServerVersion > ExchangeVersion.Exchange2007_SP1)
			{
				jsonObject.Add("IsContactPhoto", this.isContactPhoto);
			}
			if (!string.IsNullOrEmpty(this.FileName))
			{
				using (FileStream fileStream = new FileStream(this.FileName, 3, 1))
				{
					jsonObject.AddBase64("Content", fileStream);
					return jsonObject;
				}
			}
			if (this.ContentStream != null)
			{
				jsonObject.AddBase64("Content", this.ContentStream);
			}
			else if (this.Content != null)
			{
				jsonObject.AddBase64("Content", this.Content);
			}
			else
			{
				EwsUtilities.Assert(false, "FileAttachment.WriteElementsToXml", "The attachment's content is not set.");
			}
			return jsonObject;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000F6F0 File Offset: 0x0000E6F0
		public void Load(Stream stream)
		{
			this.loadToStream = stream;
			try
			{
				base.Load();
			}
			finally
			{
				this.loadToStream = null;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000F724 File Offset: 0x0000E724
		public void Load(string fileName)
		{
			this.loadToStream = new FileStream(fileName, 2);
			try
			{
				base.Load();
			}
			finally
			{
				this.loadToStream.Dispose();
				this.loadToStream = null;
			}
			this.fileName = fileName;
			this.content = null;
			this.contentStream = null;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000F780 File Offset: 0x0000E780
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000F788 File Offset: 0x0000E788
		public string FileName
		{
			get
			{
				return this.fileName;
			}
			internal set
			{
				base.ThrowIfThisIsNotNew();
				this.fileName = value;
				this.content = null;
				this.contentStream = null;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000F7A5 File Offset: 0x0000E7A5
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x0000F7AD File Offset: 0x0000E7AD
		internal Stream ContentStream
		{
			get
			{
				return this.contentStream;
			}
			set
			{
				base.ThrowIfThisIsNotNew();
				this.contentStream = value;
				this.content = null;
				this.fileName = null;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000F7CA File Offset: 0x0000E7CA
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0000F7D2 File Offset: 0x0000E7D2
		public byte[] Content
		{
			get
			{
				return this.content;
			}
			internal set
			{
				base.ThrowIfThisIsNotNew();
				this.content = value;
				this.fileName = null;
				this.contentStream = null;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000F7EF File Offset: 0x0000E7EF
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x0000F808 File Offset: 0x0000E808
		public bool IsContactPhoto
		{
			get
			{
				EwsUtilities.ValidatePropertyVersion(base.Service, ExchangeVersion.Exchange2010, "IsContactPhoto");
				return this.isContactPhoto;
			}
			set
			{
				EwsUtilities.ValidatePropertyVersion(base.Service, ExchangeVersion.Exchange2010, "IsContactPhoto");
				base.ThrowIfThisIsNotNew();
				this.isContactPhoto = value;
			}
		}

		// Token: 0x04000190 RID: 400
		private string fileName;

		// Token: 0x04000191 RID: 401
		private Stream contentStream;

		// Token: 0x04000192 RID: 402
		private byte[] content;

		// Token: 0x04000193 RID: 403
		private Stream loadToStream;

		// Token: 0x04000194 RID: 404
		private bool isContactPhoto;
	}
}
