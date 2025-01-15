using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Engine1.Library.Content;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BCA RID: 3018
	internal class ExchangeAttachmentTableValue : TableValue
	{
		// Token: 0x06005255 RID: 21077 RVA: 0x00116218 File Offset: 0x00114418
		public ExchangeAttachmentTableValue(ExchangeVersion exchangeVersion, IExchangeService service, ExchangeSearchResult result, ExchangeColumnInfo columnInfo, HashSet<PropertyDefinitionBase> additionalPropertiesLoaded)
		{
			this.exchangeVersion = exchangeVersion;
			this.service = service;
			this.result = result;
			this.columnInfo = columnInfo;
			this.additionalPropertiesLoaded = additionalPropertiesLoaded;
		}

		// Token: 0x1700196A RID: 6506
		// (get) Token: 0x06005256 RID: 21078 RVA: 0x00116245 File Offset: 0x00114445
		public override TypeValue Type
		{
			get
			{
				return ExchangeAttachmentTableValue.AttachmentTableType;
			}
		}

		// Token: 0x06005257 RID: 21079 RVA: 0x0011624C File Offset: 0x0011444C
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			ExchangeSearchResult item = this.result;
			if (!this.result.IsAttachment)
			{
				item = this.service.GetItem(this.result.Id, this.result.FolderPath, new PropertyDefinitionBase[] { ItemSchema.Attachments }, new ExchangeColumnInfo[] { this.columnInfo });
			}
			AttachmentCollection attachmentCollection = (AttachmentCollection)item.GetColumnValue(this.columnInfo);
			foreach (Attachment attachment in attachmentCollection)
			{
				if (attachment != null)
				{
					yield return this.GetAttachmentRow(attachment);
				}
			}
			IEnumerator<Attachment> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06005258 RID: 21080 RVA: 0x0011625C File Offset: 0x0011445C
		private RecordValue GetAttachmentRow(Attachment attachment)
		{
			string text;
			if (attachment.Name == null || attachment.Name.IndexOfAny(ExchangeAttachmentTableValue.InvalidPathChars) >= 0)
			{
				text = "";
			}
			else
			{
				text = Path.GetExtension(attachment.Name);
			}
			return RecordValue.New(ExchangeAttachmentTableValue.mailContentAttachmentsKeys, new IValueReference[]
			{
				TextValue.NewOrNull(attachment.Name),
				TextValue.NewOrNull(text),
				LogicalValue.New(attachment.IsInline),
				NumberValue.New(attachment.Size),
				TextValue.NewOrNull(attachment.ContentType),
				DateTimeValue.New(attachment.LastModifiedTime),
				this.GetAttachmentContentRow(attachment, text)
			});
		}

		// Token: 0x06005259 RID: 21081 RVA: 0x00116304 File Offset: 0x00114504
		private IValueReference GetAttachmentContentRow(Attachment attachment, string extension)
		{
			FileAttachment fileAttachment = attachment as FileAttachment;
			if (fileAttachment == null)
			{
				return new ExchangeAttachmentTableValue.ItemAttachmentValue(this.exchangeVersion, (ItemAttachment)attachment);
			}
			string contentTypeForExtension = ContentHelper.GetContentTypeForExtension(extension);
			if (contentTypeForExtension != null)
			{
				return new ExchangeAttachmentTableValue.AttachmentBinaryValue(fileAttachment).NewMeta(ContentHelper.CreateContentTypeMetadata(contentTypeForExtension));
			}
			return new ExchangeAttachmentTableValue.AttachmentBinaryValue(fileAttachment);
		}

		// Token: 0x04002D53 RID: 11603
		private const string MailContentAttachmentNameKey = "Name";

		// Token: 0x04002D54 RID: 11604
		private const string MailContentAttachmentExtensionKey = "Extension";

		// Token: 0x04002D55 RID: 11605
		private const string MailContentAttachmentSizeKey = "Size";

		// Token: 0x04002D56 RID: 11606
		private const string MailContentAttachmentIsInlineKey = "IsInline";

		// Token: 0x04002D57 RID: 11607
		private const string MailContentAttachmentContentKey = "AttachmentContent";

		// Token: 0x04002D58 RID: 11608
		private const string MailContentAttachmentContentTypeKey = "ContentType";

		// Token: 0x04002D59 RID: 11609
		private const string MailContentAttachmentLastModifiedTimeKey = "Last Modified";

		// Token: 0x04002D5A RID: 11610
		private readonly ExchangeVersion exchangeVersion;

		// Token: 0x04002D5B RID: 11611
		private readonly IExchangeService service;

		// Token: 0x04002D5C RID: 11612
		private readonly ExchangeSearchResult result;

		// Token: 0x04002D5D RID: 11613
		private readonly ExchangeColumnInfo columnInfo;

		// Token: 0x04002D5E RID: 11614
		private readonly HashSet<PropertyDefinitionBase> additionalPropertiesLoaded;

		// Token: 0x04002D5F RID: 11615
		private static readonly char[] InvalidPathChars = Path.GetInvalidPathChars();

		// Token: 0x04002D60 RID: 11616
		private static readonly Keys mailContentAttachmentsKeys = Keys.New(new string[] { "Name", "Extension", "IsInline", "Size", "ContentType", "Last Modified", "AttachmentContent" });

		// Token: 0x04002D61 RID: 11617
		public static readonly TableTypeValue AttachmentTableType = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(ExchangeAttachmentTableValue.mailContentAttachmentsKeys, new Value[]
		{
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text.Nullable,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text.Nullable,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Logical.Nullable,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Number.Nullable,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text.Nullable,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.DateTime.Nullable,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Any.Nullable,
				LogicalValue.False
			})
		})));

		// Token: 0x02000BCB RID: 3019
		private class ItemAttachmentValue : IValueReference
		{
			// Token: 0x0600525B RID: 21083 RVA: 0x001164E6 File Offset: 0x001146E6
			public ItemAttachmentValue(ExchangeVersion exchangeVersion, ItemAttachment itemAttachment)
			{
				this.exchangeVersion = exchangeVersion;
				this.itemAttachment = itemAttachment;
			}

			// Token: 0x1700196B RID: 6507
			// (get) Token: 0x0600525C RID: 21084 RVA: 0x001164FC File Offset: 0x001146FC
			public bool Evaluated
			{
				get
				{
					return this.value != null;
				}
			}

			// Token: 0x1700196C RID: 6508
			// (get) Token: 0x0600525D RID: 21085 RVA: 0x00116508 File Offset: 0x00114708
			public Value Value
			{
				get
				{
					if (this.value == null)
					{
						this.itemAttachment.Load(BodyType.Text, new PropertyDefinitionBase[] { ExchangeHelper.PR_Html_Body });
						ExchangeCatalog exchangeCatalog;
						if (!ExchangeCatalogFactory.TryGetCatalog(this.exchangeVersion, this.itemAttachment.Item.ItemClass, out exchangeCatalog))
						{
							throw ValueException.NewDataSourceError<Message1>(Strings.Resource_AttachmentType_NotSupported(this.itemAttachment.Name), Value.Null, null);
						}
						this.value = ExchangeValueBuilder.CreateRowValue(this.exchangeVersion, null, new ExchangeServiceSearchResult(this.itemAttachment.Item, null, null, true), exchangeCatalog.GetAllColumnInfos(false), new HashSet<PropertyDefinitionBase>())[0];
					}
					return this.value;
				}
			}

			// Token: 0x04002D62 RID: 11618
			private readonly ExchangeVersion exchangeVersion;

			// Token: 0x04002D63 RID: 11619
			private readonly ItemAttachment itemAttachment;

			// Token: 0x04002D64 RID: 11620
			private Value value;
		}

		// Token: 0x02000BCC RID: 3020
		private class AttachmentBinaryValue : StreamedBinaryValue
		{
			// Token: 0x0600525E RID: 21086 RVA: 0x001165B0 File Offset: 0x001147B0
			public AttachmentBinaryValue(FileAttachment fileAttachment)
			{
				this.fileAttachment = fileAttachment;
			}

			// Token: 0x0600525F RID: 21087 RVA: 0x001165C0 File Offset: 0x001147C0
			public override Stream Open()
			{
				MemoryStream memoryStream = new MemoryStream(this.fileAttachment.Size);
				this.fileAttachment.Load(memoryStream);
				memoryStream.Position = 0L;
				return memoryStream;
			}

			// Token: 0x04002D65 RID: 11621
			private readonly FileAttachment fileAttachment;
		}
	}
}
