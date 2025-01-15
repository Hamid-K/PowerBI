using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000251 RID: 593
	public enum WellKnownFolderName
	{
		// Token: 0x04001280 RID: 4736
		[EwsEnum("calendar")]
		Calendar,
		// Token: 0x04001281 RID: 4737
		[EwsEnum("contacts")]
		Contacts,
		// Token: 0x04001282 RID: 4738
		[EwsEnum("deleteditems")]
		DeletedItems,
		// Token: 0x04001283 RID: 4739
		[EwsEnum("drafts")]
		Drafts,
		// Token: 0x04001284 RID: 4740
		[EwsEnum("inbox")]
		Inbox,
		// Token: 0x04001285 RID: 4741
		[EwsEnum("journal")]
		Journal,
		// Token: 0x04001286 RID: 4742
		[EwsEnum("notes")]
		Notes,
		// Token: 0x04001287 RID: 4743
		[EwsEnum("outbox")]
		Outbox,
		// Token: 0x04001288 RID: 4744
		[EwsEnum("sentitems")]
		SentItems,
		// Token: 0x04001289 RID: 4745
		[EwsEnum("tasks")]
		Tasks,
		// Token: 0x0400128A RID: 4746
		[EwsEnum("msgfolderroot")]
		MsgFolderRoot,
		// Token: 0x0400128B RID: 4747
		[EwsEnum("publicfoldersroot")]
		[RequiredServerVersion(ExchangeVersion.Exchange2007_SP1)]
		PublicFoldersRoot,
		// Token: 0x0400128C RID: 4748
		[EwsEnum("root")]
		Root,
		// Token: 0x0400128D RID: 4749
		[EwsEnum("junkemail")]
		JunkEmail,
		// Token: 0x0400128E RID: 4750
		[EwsEnum("searchfolders")]
		SearchFolders,
		// Token: 0x0400128F RID: 4751
		[EwsEnum("voicemail")]
		VoiceMail,
		// Token: 0x04001290 RID: 4752
		[EwsEnum("recoverableitemsroot")]
		[RequiredServerVersion(ExchangeVersion.Exchange2010_SP1)]
		RecoverableItemsRoot,
		// Token: 0x04001291 RID: 4753
		[EwsEnum("recoverableitemsdeletions")]
		[RequiredServerVersion(ExchangeVersion.Exchange2010_SP1)]
		RecoverableItemsDeletions,
		// Token: 0x04001292 RID: 4754
		[EwsEnum("recoverableitemsversions")]
		[RequiredServerVersion(ExchangeVersion.Exchange2010_SP1)]
		RecoverableItemsVersions,
		// Token: 0x04001293 RID: 4755
		[RequiredServerVersion(ExchangeVersion.Exchange2010_SP1)]
		[EwsEnum("recoverableitemspurges")]
		RecoverableItemsPurges,
		// Token: 0x04001294 RID: 4756
		[RequiredServerVersion(ExchangeVersion.Exchange2010_SP1)]
		[EwsEnum("archiveroot")]
		ArchiveRoot,
		// Token: 0x04001295 RID: 4757
		[EwsEnum("archivemsgfolderroot")]
		[RequiredServerVersion(ExchangeVersion.Exchange2010_SP1)]
		ArchiveMsgFolderRoot,
		// Token: 0x04001296 RID: 4758
		[EwsEnum("archivedeleteditems")]
		[RequiredServerVersion(ExchangeVersion.Exchange2010_SP1)]
		ArchiveDeletedItems,
		// Token: 0x04001297 RID: 4759
		[EwsEnum("archiverecoverableitemsroot")]
		[RequiredServerVersion(ExchangeVersion.Exchange2010_SP1)]
		ArchiveRecoverableItemsRoot,
		// Token: 0x04001298 RID: 4760
		[RequiredServerVersion(ExchangeVersion.Exchange2010_SP1)]
		[EwsEnum("archiverecoverableitemsdeletions")]
		ArchiveRecoverableItemsDeletions,
		// Token: 0x04001299 RID: 4761
		[EwsEnum("archiverecoverableitemsversions")]
		[RequiredServerVersion(ExchangeVersion.Exchange2010_SP1)]
		ArchiveRecoverableItemsVersions,
		// Token: 0x0400129A RID: 4762
		[RequiredServerVersion(ExchangeVersion.Exchange2010_SP1)]
		[EwsEnum("archiverecoverableitemspurges")]
		ArchiveRecoverableItemsPurges,
		// Token: 0x0400129B RID: 4763
		[EwsEnum("syncissues")]
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		SyncIssues,
		// Token: 0x0400129C RID: 4764
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		[EwsEnum("conflicts")]
		Conflicts,
		// Token: 0x0400129D RID: 4765
		[EwsEnum("localfailures")]
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		LocalFailures,
		// Token: 0x0400129E RID: 4766
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		[EwsEnum("serverfailures")]
		ServerFailures,
		// Token: 0x0400129F RID: 4767
		[EwsEnum("recipientcache")]
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		RecipientCache,
		// Token: 0x040012A0 RID: 4768
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		[EwsEnum("quickcontacts")]
		QuickContacts,
		// Token: 0x040012A1 RID: 4769
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		[EwsEnum("conversationhistory")]
		ConversationHistory,
		// Token: 0x040012A2 RID: 4770
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		[EwsEnum("todosearch")]
		ToDoSearch
	}
}
