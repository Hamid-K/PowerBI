using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000199 RID: 409
	[ServiceObjectDefinition("Conversation")]
	public class Conversation : ServiceObject
	{
		// Token: 0x06001310 RID: 4880 RVA: 0x0003554C File Offset: 0x0003454C
		internal Conversation(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00035555 File Offset: 0x00034555
		internal override ServiceObjectSchema GetSchema()
		{
			return ConversationSchema.Instance;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0003555C File Offset: 0x0003455C
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010_SP1;
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0003555F File Offset: 0x0003455F
		internal override PropertyDefinition GetIdPropertyDefinition()
		{
			return ConversationSchema.Id;
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00035566 File Offset: 0x00034566
		internal override void InternalLoad(PropertySet propertySet)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0003556D File Offset: 0x0003456D
		internal override void InternalDelete(DeleteMode deleteMode, SendCancellationsMode? sendCancellationsMode, AffectedTaskOccurrence? affectedTaskOccurrences)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00035574 File Offset: 0x00034574
		internal override string GetChangeXmlElementName()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0003557B File Offset: 0x0003457B
		internal override string GetDeleteFieldXmlElementName()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00035582 File Offset: 0x00034582
		internal override string GetSetFieldXmlElementName()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00035589 File Offset: 0x00034589
		internal override bool GetIsTimeZoneHeaderRequired(bool isUpdateOperation)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00035590 File Offset: 0x00034590
		internal override ExtendedPropertyCollection GetExtendedProperties()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00035598 File Offset: 0x00034598
		public void EnableAlwaysCategorizeItems(IEnumerable<string> categories, bool processSynchronously)
		{
			base.Service.EnableAlwaysCategorizeItemsInConversations(new ConversationId[] { this.Id }, categories, processSynchronously)[0].ThrowIfNecessary();
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x000355D0 File Offset: 0x000345D0
		public void DisableAlwaysCategorizeItems(bool processSynchronously)
		{
			base.Service.DisableAlwaysCategorizeItemsInConversations(new ConversationId[] { this.Id }, processSynchronously)[0].ThrowIfNecessary();
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00035608 File Offset: 0x00034608
		public void EnableAlwaysDeleteItems(bool processSynchronously)
		{
			base.Service.EnableAlwaysDeleteItemsInConversations(new ConversationId[] { this.Id }, processSynchronously)[0].ThrowIfNecessary();
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00035640 File Offset: 0x00034640
		public void DisableAlwaysDeleteItems(bool processSynchronously)
		{
			base.Service.DisableAlwaysDeleteItemsInConversations(new ConversationId[] { this.Id }, processSynchronously)[0].ThrowIfNecessary();
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00035678 File Offset: 0x00034678
		public void EnableAlwaysMoveItems(FolderId destinationFolderId, bool processSynchronously)
		{
			base.Service.EnableAlwaysMoveItemsInConversations(new ConversationId[] { this.Id }, destinationFolderId, processSynchronously)[0].ThrowIfNecessary();
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x000356B0 File Offset: 0x000346B0
		public void DisableAlwaysMoveItemsInConversation(bool processSynchronously)
		{
			base.Service.DisableAlwaysMoveItemsInConversations(new ConversationId[] { this.Id }, processSynchronously)[0].ThrowIfNecessary();
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x000356E8 File Offset: 0x000346E8
		public void DeleteItems(FolderId contextFolderId, DeleteMode deleteMode)
		{
			base.Service.DeleteItemsInConversations(new KeyValuePair<ConversationId, DateTime?>[]
			{
				new KeyValuePair<ConversationId, DateTime?>(this.Id, new DateTime?(this.GlobalLastDeliveryTime))
			}, contextFolderId, deleteMode)[0].ThrowIfNecessary();
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00035738 File Offset: 0x00034738
		public void MoveItemsInConversation(FolderId contextFolderId, FolderId destinationFolderId)
		{
			base.Service.MoveItemsInConversations(new KeyValuePair<ConversationId, DateTime?>[]
			{
				new KeyValuePair<ConversationId, DateTime?>(this.Id, new DateTime?(this.GlobalLastDeliveryTime))
			}, contextFolderId, destinationFolderId)[0].ThrowIfNecessary();
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00035788 File Offset: 0x00034788
		public void CopyItemsInConversation(FolderId contextFolderId, FolderId destinationFolderId)
		{
			base.Service.CopyItemsInConversations(new KeyValuePair<ConversationId, DateTime?>[]
			{
				new KeyValuePair<ConversationId, DateTime?>(this.Id, new DateTime?(this.GlobalLastDeliveryTime))
			}, contextFolderId, destinationFolderId)[0].ThrowIfNecessary();
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x000357D8 File Offset: 0x000347D8
		public void SetReadStateForItemsInConversation(FolderId contextFolderId, bool isRead)
		{
			base.Service.SetReadStateForItemsInConversations(new KeyValuePair<ConversationId, DateTime?>[]
			{
				new KeyValuePair<ConversationId, DateTime?>(this.Id, new DateTime?(this.GlobalLastDeliveryTime))
			}, contextFolderId, isRead)[0].ThrowIfNecessary();
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00035828 File Offset: 0x00034828
		public void SetReadStateForItemsInConversation(FolderId contextFolderId, bool isRead, bool suppressReadReceipts)
		{
			base.Service.SetReadStateForItemsInConversations(new KeyValuePair<ConversationId, DateTime?>[]
			{
				new KeyValuePair<ConversationId, DateTime?>(this.Id, new DateTime?(this.GlobalLastDeliveryTime))
			}, contextFolderId, isRead, suppressReadReceipts)[0].ThrowIfNecessary();
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00035878 File Offset: 0x00034878
		public void SetRetentionPolicyForItemsInConversation(FolderId contextFolderId, RetentionType retentionPolicyType, Guid? retentionPolicyTagId)
		{
			base.Service.SetRetentionPolicyForItemsInConversations(new KeyValuePair<ConversationId, DateTime?>[]
			{
				new KeyValuePair<ConversationId, DateTime?>(this.Id, new DateTime?(this.GlobalLastDeliveryTime))
			}, contextFolderId, retentionPolicyType, retentionPolicyTagId)[0].ThrowIfNecessary();
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x000358C8 File Offset: 0x000348C8
		public void FlagItemsComplete(FolderId contextFolderId, DateTime? completeDate)
		{
			Flag flag = new Flag
			{
				FlagStatus = ItemFlagStatus.Complete
			};
			if (completeDate != null)
			{
				flag.CompleteDate = completeDate.Value;
			}
			base.Service.SetFlagStatusForItemsInConversations(new KeyValuePair<ConversationId, DateTime?>[]
			{
				new KeyValuePair<ConversationId, DateTime?>(this.Id, new DateTime?(this.GlobalLastDeliveryTime))
			}, contextFolderId, flag)[0].ThrowIfNecessary();
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0003593C File Offset: 0x0003493C
		public void ClearItemFlags(FolderId contextFolderId)
		{
			Flag flag = new Flag
			{
				FlagStatus = ItemFlagStatus.NotFlagged
			};
			base.Service.SetFlagStatusForItemsInConversations(new KeyValuePair<ConversationId, DateTime?>[]
			{
				new KeyValuePair<ConversationId, DateTime?>(this.Id, new DateTime?(this.GlobalLastDeliveryTime))
			}, contextFolderId, flag)[0].ThrowIfNecessary();
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0003599C File Offset: 0x0003499C
		public void FlagItems(FolderId contextFolderId, DateTime? startDate, DateTime? dueDate)
		{
			Flag flag = new Flag
			{
				FlagStatus = ItemFlagStatus.Flagged
			};
			if (startDate != null)
			{
				flag.StartDate = startDate.Value;
			}
			if (dueDate != null)
			{
				flag.DueDate = dueDate.Value;
			}
			base.Service.SetFlagStatusForItemsInConversations(new KeyValuePair<ConversationId, DateTime?>[]
			{
				new KeyValuePair<ConversationId, DateTime?>(this.Id, new DateTime?(this.GlobalLastDeliveryTime))
			}, contextFolderId, flag)[0].ThrowIfNecessary();
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x00035A26 File Offset: 0x00034A26
		public ConversationId Id
		{
			get
			{
				return (ConversationId)base.PropertyBag[this.GetIdPropertyDefinition()];
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x00035A40 File Offset: 0x00034A40
		public string Topic
		{
			get
			{
				string empty = string.Empty;
				if (base.PropertyBag.Contains(ConversationSchema.Topic))
				{
					base.PropertyBag.TryGetProperty<string>(ConversationSchema.Topic, out empty);
				}
				return empty;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x00035A79 File Offset: 0x00034A79
		public StringList UniqueRecipients
		{
			get
			{
				return (StringList)base.PropertyBag[ConversationSchema.UniqueRecipients];
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x00035A90 File Offset: 0x00034A90
		public StringList GlobalUniqueRecipients
		{
			get
			{
				return (StringList)base.PropertyBag[ConversationSchema.GlobalUniqueRecipients];
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x00035AA8 File Offset: 0x00034AA8
		public StringList UniqueUnreadSenders
		{
			get
			{
				StringList stringList = null;
				if (base.PropertyBag.Contains(ConversationSchema.UniqueUnreadSenders))
				{
					base.PropertyBag.TryGetProperty<StringList>(ConversationSchema.UniqueUnreadSenders, out stringList);
				}
				return stringList;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x00035AE0 File Offset: 0x00034AE0
		public StringList GlobalUniqueUnreadSenders
		{
			get
			{
				StringList stringList = null;
				if (base.PropertyBag.Contains(ConversationSchema.GlobalUniqueUnreadSenders))
				{
					base.PropertyBag.TryGetProperty<StringList>(ConversationSchema.GlobalUniqueUnreadSenders, out stringList);
				}
				return stringList;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x00035B15 File Offset: 0x00034B15
		public StringList UniqueSenders
		{
			get
			{
				return (StringList)base.PropertyBag[ConversationSchema.UniqueSenders];
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x00035B2C File Offset: 0x00034B2C
		public StringList GlobalUniqueSenders
		{
			get
			{
				return (StringList)base.PropertyBag[ConversationSchema.GlobalUniqueSenders];
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x00035B43 File Offset: 0x00034B43
		public DateTime LastDeliveryTime
		{
			get
			{
				return (DateTime)base.PropertyBag[ConversationSchema.LastDeliveryTime];
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x00035B5A File Offset: 0x00034B5A
		public DateTime GlobalLastDeliveryTime
		{
			get
			{
				return (DateTime)base.PropertyBag[ConversationSchema.GlobalLastDeliveryTime];
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x00035B74 File Offset: 0x00034B74
		public StringList Categories
		{
			get
			{
				StringList stringList = null;
				if (base.PropertyBag.Contains(ConversationSchema.Categories))
				{
					base.PropertyBag.TryGetProperty<StringList>(ConversationSchema.Categories, out stringList);
				}
				return stringList;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x00035BAC File Offset: 0x00034BAC
		public StringList GlobalCategories
		{
			get
			{
				StringList stringList = null;
				if (base.PropertyBag.Contains(ConversationSchema.GlobalCategories))
				{
					base.PropertyBag.TryGetProperty<StringList>(ConversationSchema.GlobalCategories, out stringList);
				}
				return stringList;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x00035BE4 File Offset: 0x00034BE4
		public ConversationFlagStatus FlagStatus
		{
			get
			{
				ConversationFlagStatus conversationFlagStatus = ConversationFlagStatus.NotFlagged;
				if (base.PropertyBag.Contains(ConversationSchema.FlagStatus))
				{
					base.PropertyBag.TryGetProperty<ConversationFlagStatus>(ConversationSchema.FlagStatus, out conversationFlagStatus);
				}
				return conversationFlagStatus;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x00035C1C File Offset: 0x00034C1C
		public ConversationFlagStatus GlobalFlagStatus
		{
			get
			{
				ConversationFlagStatus conversationFlagStatus = ConversationFlagStatus.NotFlagged;
				if (base.PropertyBag.Contains(ConversationSchema.GlobalFlagStatus))
				{
					base.PropertyBag.TryGetProperty<ConversationFlagStatus>(ConversationSchema.GlobalFlagStatus, out conversationFlagStatus);
				}
				return conversationFlagStatus;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x00035C51 File Offset: 0x00034C51
		public bool HasAttachments
		{
			get
			{
				return (bool)base.PropertyBag[ConversationSchema.HasAttachments];
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x00035C68 File Offset: 0x00034C68
		public bool GlobalHasAttachments
		{
			get
			{
				return (bool)base.PropertyBag[ConversationSchema.GlobalHasAttachments];
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x00035C7F File Offset: 0x00034C7F
		public int MessageCount
		{
			get
			{
				return (int)base.PropertyBag[ConversationSchema.MessageCount];
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x00035C96 File Offset: 0x00034C96
		public int GlobalMessageCount
		{
			get
			{
				return (int)base.PropertyBag[ConversationSchema.GlobalMessageCount];
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x00035CB0 File Offset: 0x00034CB0
		public int UnreadCount
		{
			get
			{
				int num = 0;
				if (base.PropertyBag.Contains(ConversationSchema.UnreadCount))
				{
					base.PropertyBag.TryGetProperty<int>(ConversationSchema.UnreadCount, out num);
				}
				return num;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x00035CE8 File Offset: 0x00034CE8
		public int GlobalUnreadCount
		{
			get
			{
				int num = 0;
				if (base.PropertyBag.Contains(ConversationSchema.GlobalUnreadCount))
				{
					base.PropertyBag.TryGetProperty<int>(ConversationSchema.GlobalUnreadCount, out num);
				}
				return num;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x00035D1D File Offset: 0x00034D1D
		public int Size
		{
			get
			{
				return (int)base.PropertyBag[ConversationSchema.Size];
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x00035D34 File Offset: 0x00034D34
		public int GlobalSize
		{
			get
			{
				return (int)base.PropertyBag[ConversationSchema.GlobalSize];
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x00035D4B File Offset: 0x00034D4B
		public StringList ItemClasses
		{
			get
			{
				return (StringList)base.PropertyBag[ConversationSchema.ItemClasses];
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x00035D62 File Offset: 0x00034D62
		public StringList GlobalItemClasses
		{
			get
			{
				return (StringList)base.PropertyBag[ConversationSchema.GlobalItemClasses];
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x00035D79 File Offset: 0x00034D79
		public Importance Importance
		{
			get
			{
				return (Importance)base.PropertyBag[ConversationSchema.Importance];
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x00035D90 File Offset: 0x00034D90
		public Importance GlobalImportance
		{
			get
			{
				return (Importance)base.PropertyBag[ConversationSchema.GlobalImportance];
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x00035DA7 File Offset: 0x00034DA7
		public ItemIdCollection ItemIds
		{
			get
			{
				return (ItemIdCollection)base.PropertyBag[ConversationSchema.ItemIds];
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x00035DBE File Offset: 0x00034DBE
		public ItemIdCollection GlobalItemIds
		{
			get
			{
				return (ItemIdCollection)base.PropertyBag[ConversationSchema.GlobalItemIds];
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x00035DD5 File Offset: 0x00034DD5
		public DateTime LastModifiedTime
		{
			get
			{
				return (DateTime)base.PropertyBag[ConversationSchema.LastModifiedTime];
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00035DEC File Offset: 0x00034DEC
		public byte[] InstanceKey
		{
			get
			{
				return (byte[])base.PropertyBag[ConversationSchema.InstanceKey];
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x00035E03 File Offset: 0x00034E03
		public string Preview
		{
			get
			{
				return (string)base.PropertyBag[ConversationSchema.Preview];
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x00035E1A File Offset: 0x00034E1A
		public IconIndex IconIndex
		{
			get
			{
				return (IconIndex)base.PropertyBag[ConversationSchema.IconIndex];
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x00035E31 File Offset: 0x00034E31
		public IconIndex GlobalIconIndex
		{
			get
			{
				return (IconIndex)base.PropertyBag[ConversationSchema.GlobalIconIndex];
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x00035E48 File Offset: 0x00034E48
		public ItemIdCollection DraftItemIds
		{
			get
			{
				return (ItemIdCollection)base.PropertyBag[ConversationSchema.DraftItemIds];
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x00035E5F File Offset: 0x00034E5F
		public bool HasIrm
		{
			get
			{
				return (bool)base.PropertyBag[ConversationSchema.HasIrm];
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x00035E76 File Offset: 0x00034E76
		public bool GlobalHasIrm
		{
			get
			{
				return (bool)base.PropertyBag[ConversationSchema.GlobalHasIrm];
			}
		}
	}
}
