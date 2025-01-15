using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000038 RID: 56
	[EditorBrowsable(1)]
	public sealed class AttachmentCollection : ComplexPropertyCollection<Attachment>, IOwnedProperty
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000A7DD File Offset: 0x000097DD
		internal AttachmentCollection()
		{
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000A7E5 File Offset: 0x000097E5
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000A7F0 File Offset: 0x000097F0
		ServiceObject IOwnedProperty.Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				Item item = value as Item;
				EwsUtilities.Assert(item != null, "AttachmentCollection.IOwnedProperty.set_Owner", "value is not a descendant of ItemBase");
				this.owner = item;
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A821 File Offset: 0x00009821
		public FileAttachment AddFileAttachment(string fileName)
		{
			return this.AddFileAttachment(Path.GetFileName(fileName), fileName);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000A830 File Offset: 0x00009830
		public FileAttachment AddFileAttachment(string name, string fileName)
		{
			FileAttachment fileAttachment = new FileAttachment(this.owner);
			fileAttachment.Name = name;
			fileAttachment.FileName = fileName;
			base.InternalAdd(fileAttachment);
			return fileAttachment;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000A860 File Offset: 0x00009860
		public FileAttachment AddFileAttachment(string name, Stream contentStream)
		{
			FileAttachment fileAttachment = new FileAttachment(this.owner);
			fileAttachment.Name = name;
			fileAttachment.ContentStream = contentStream;
			base.InternalAdd(fileAttachment);
			return fileAttachment;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000A890 File Offset: 0x00009890
		public FileAttachment AddFileAttachment(string name, byte[] content)
		{
			FileAttachment fileAttachment = new FileAttachment(this.owner);
			fileAttachment.Name = name;
			fileAttachment.Content = content;
			base.InternalAdd(fileAttachment);
			return fileAttachment;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000A8C0 File Offset: 0x000098C0
		public ItemAttachment<TItem> AddItemAttachment<TItem>() where TItem : Item
		{
			if (typeof(TItem).GetCustomAttributes(typeof(AttachableAttribute), false).Length == 0)
			{
				throw new InvalidOperationException(string.Format("Items of type {0} are not supported as attachments.", typeof(TItem).Name));
			}
			ItemAttachment<TItem> itemAttachment = new ItemAttachment<TItem>(this.owner);
			itemAttachment.Item = (TItem)((object)EwsUtilities.CreateItemFromItemClass(itemAttachment, typeof(TItem), true));
			base.InternalAdd(itemAttachment);
			return itemAttachment;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A93A File Offset: 0x0000993A
		public void Clear()
		{
			base.InternalClear();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A942 File Offset: 0x00009942
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= base.Count)
			{
				throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
			}
			base.InternalRemoveAt(index);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A96D File Offset: 0x0000996D
		public bool Remove(Attachment attachment)
		{
			EwsUtilities.ValidateParam(attachment, "attachment");
			return base.InternalRemove(attachment);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A984 File Offset: 0x00009984
		internal override Attachment CreateComplexProperty(string xmlElementName)
		{
			if (xmlElementName != null)
			{
				if (xmlElementName == "FileAttachment")
				{
					return new FileAttachment(this.owner);
				}
				if (xmlElementName == "ItemAttachment")
				{
					return new ItemAttachment(this.owner);
				}
			}
			return null;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A9CB File Offset: 0x000099CB
		internal override Attachment CreateDefaultComplexProperty()
		{
			throw new JsonDeserializationNotImplementedException();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A9D2 File Offset: 0x000099D2
		internal override string GetCollectionItemXmlElementName(Attachment complexProperty)
		{
			if (complexProperty is FileAttachment)
			{
				return "FileAttachment";
			}
			return "ItemAttachment";
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A9E8 File Offset: 0x000099E8
		internal void Save()
		{
			List<Attachment> list = new List<Attachment>();
			foreach (Attachment attachment in base.RemovedItems)
			{
				if (!attachment.IsNew)
				{
					list.Add(attachment);
				}
			}
			if (list.Count > 0)
			{
				this.InternalDeleteAttachments(list);
			}
			list.Clear();
			foreach (Attachment attachment2 in this)
			{
				if (attachment2.IsNew)
				{
					list.Add(attachment2);
				}
			}
			if (list.Count > 0)
			{
				if (this.owner.IsAttachment)
				{
					this.InternalCreateAttachments(this.owner.ParentAttachment.Id, list);
				}
				else
				{
					this.InternalCreateAttachments(this.owner.Id.UniqueId, list);
				}
			}
			foreach (Attachment attachment3 in this)
			{
				ItemAttachment itemAttachment = attachment3 as ItemAttachment;
				if (itemAttachment != null && itemAttachment.Item != null)
				{
					itemAttachment.Item.Attachments.Save();
					itemAttachment.Item.ClearChangeLog();
				}
			}
			base.ClearChangeLog();
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000AB58 File Offset: 0x00009B58
		internal bool HasUnprocessedChanges()
		{
			foreach (Attachment attachment in this)
			{
				if (attachment.IsNew)
				{
					return true;
				}
			}
			foreach (Attachment attachment2 in base.RemovedItems)
			{
				if (!attachment2.IsNew)
				{
					return true;
				}
			}
			foreach (ItemAttachment itemAttachment in Enumerable.OfType<ItemAttachment>(this))
			{
				if (itemAttachment.Item != null && itemAttachment.Item.Attachments.HasUnprocessedChanges())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000AC50 File Offset: 0x00009C50
		internal override void ClearChangeLog()
		{
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000AC54 File Offset: 0x00009C54
		internal void Validate()
		{
			bool flag = false;
			for (int i = 0; i < base.AddedItems.Count; i++)
			{
				Attachment attachment = base.AddedItems[i];
				if (attachment.IsNew)
				{
					if (this.owner.IsNew && this.owner.Service.RequestedServerVersion >= ExchangeVersion.Exchange2010_SP2)
					{
						FileAttachment fileAttachment = attachment as FileAttachment;
						if (fileAttachment != null && fileAttachment.IsContactPhoto)
						{
							if (flag)
							{
								throw new ServiceValidationException(Strings.MultipleContactPhotosInAttachment);
							}
							flag = true;
						}
					}
					attachment.Validate(i);
				}
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000ACDC File Offset: 0x00009CDC
		private void InternalDeleteAttachments(IEnumerable<Attachment> attachments)
		{
			ServiceResponseCollection<DeleteAttachmentResponse> serviceResponseCollection = this.owner.Service.DeleteAttachments(attachments);
			foreach (DeleteAttachmentResponse deleteAttachmentResponse in serviceResponseCollection)
			{
				if (deleteAttachmentResponse.Result != ServiceResult.Error)
				{
					base.RemoveFromChangeLog(deleteAttachmentResponse.Attachment);
				}
			}
			if (serviceResponseCollection.OverallResult == ServiceResult.Error)
			{
				throw new DeleteAttachmentException(serviceResponseCollection, Strings.AtLeastOneAttachmentCouldNotBeDeleted);
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000AD60 File Offset: 0x00009D60
		private void InternalCreateAttachments(string parentItemId, IEnumerable<Attachment> attachments)
		{
			ServiceResponseCollection<CreateAttachmentResponse> serviceResponseCollection = this.owner.Service.CreateAttachments(parentItemId, attachments);
			foreach (CreateAttachmentResponse createAttachmentResponse in serviceResponseCollection)
			{
				if (createAttachmentResponse.Result != ServiceResult.Error)
				{
					base.RemoveFromChangeLog(createAttachmentResponse.Attachment);
				}
			}
			if (serviceResponseCollection.OverallResult == ServiceResult.Error)
			{
				throw new CreateAttachmentException(serviceResponseCollection, Strings.AttachmentCreationFailed);
			}
		}

		// Token: 0x0400012C RID: 300
		private Item owner;
	}
}
