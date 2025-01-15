using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using Microsoft.Exchange.WebServices.Autodiscover;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000D4 RID: 212
	public sealed class ExchangeService : ExchangeServiceBase
	{
		// Token: 0x060009D6 RID: 2518 RVA: 0x00020704 File Offset: 0x0001F704
		internal List<Item> InternalCreateResponseObject(ServiceObject responseObject, FolderId parentFolderId, MessageDisposition? messageDisposition)
		{
			ServiceResponseCollection<CreateResponseObjectResponse> serviceResponseCollection = new CreateResponseObjectRequest(this, ServiceErrorHandling.ThrowOnError)
			{
				ParentFolderId = parentFolderId,
				Items = new ServiceObject[] { responseObject },
				MessageDisposition = messageDisposition
			}.Execute();
			return serviceResponseCollection[0].Items;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002074C File Offset: 0x0001F74C
		internal void CreateFolder(Folder folder, FolderId parentFolderId)
		{
			new CreateFolderRequest(this, ServiceErrorHandling.ThrowOnError)
			{
				Folders = new Folder[] { folder },
				ParentFolderId = parentFolderId
			}.Execute();
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00020784 File Offset: 0x0001F784
		internal void UpdateFolder(Folder folder)
		{
			UpdateFolderRequest updateFolderRequest = new UpdateFolderRequest(this, ServiceErrorHandling.ThrowOnError);
			updateFolderRequest.Folders.Add(folder);
			updateFolderRequest.Execute();
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000207AC File Offset: 0x0001F7AC
		internal Folder CopyFolder(FolderId folderId, FolderId destinationFolderId)
		{
			ServiceResponseCollection<MoveCopyFolderResponse> serviceResponseCollection = new CopyFolderRequest(this, ServiceErrorHandling.ThrowOnError)
			{
				DestinationFolderId = destinationFolderId,
				FolderIds = { folderId }
			}.Execute();
			return serviceResponseCollection[0].Folder;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000207E8 File Offset: 0x0001F7E8
		internal Folder MoveFolder(FolderId folderId, FolderId destinationFolderId)
		{
			ServiceResponseCollection<MoveCopyFolderResponse> serviceResponseCollection = new MoveFolderRequest(this, ServiceErrorHandling.ThrowOnError)
			{
				DestinationFolderId = destinationFolderId,
				FolderIds = { folderId }
			}.Execute();
			return serviceResponseCollection[0].Folder;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00020824 File Offset: 0x0001F824
		private ServiceResponseCollection<FindFolderResponse> InternalFindFolders(IEnumerable<FolderId> parentFolderIds, SearchFilter searchFilter, FolderView view, ServiceErrorHandling errorHandlingMode)
		{
			FindFolderRequest findFolderRequest = new FindFolderRequest(this, errorHandlingMode);
			findFolderRequest.ParentFolderIds.AddRange(parentFolderIds);
			findFolderRequest.SearchFilter = searchFilter;
			findFolderRequest.View = view;
			return findFolderRequest.Execute();
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002085C File Offset: 0x0001F85C
		public FindFoldersResults FindFolders(FolderId parentFolderId, SearchFilter searchFilter, FolderView view)
		{
			EwsUtilities.ValidateParam(parentFolderId, "parentFolderId");
			EwsUtilities.ValidateParam(view, "view");
			EwsUtilities.ValidateParamAllowNull(searchFilter, "searchFilter");
			ServiceResponseCollection<FindFolderResponse> serviceResponseCollection = this.InternalFindFolders(new FolderId[] { parentFolderId }, searchFilter, view, ServiceErrorHandling.ThrowOnError);
			return serviceResponseCollection[0].Results;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x000208AC File Offset: 0x0001F8AC
		public FindFoldersResults FindFolders(FolderId parentFolderId, FolderView view)
		{
			EwsUtilities.ValidateParam(parentFolderId, "parentFolderId");
			EwsUtilities.ValidateParam(view, "view");
			ServiceResponseCollection<FindFolderResponse> serviceResponseCollection = this.InternalFindFolders(new FolderId[] { parentFolderId }, null, view, ServiceErrorHandling.ThrowOnError);
			return serviceResponseCollection[0].Results;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000208F1 File Offset: 0x0001F8F1
		public FindFoldersResults FindFolders(WellKnownFolderName parentFolderName, SearchFilter searchFilter, FolderView view)
		{
			return this.FindFolders(new FolderId(parentFolderName), searchFilter, view);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00020901 File Offset: 0x0001F901
		public FindFoldersResults FindFolders(WellKnownFolderName parentFolderName, FolderView view)
		{
			return this.FindFolders(new FolderId(parentFolderName), view);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00020910 File Offset: 0x0001F910
		internal void LoadPropertiesForFolder(Folder folder, PropertySet propertySet)
		{
			EwsUtilities.ValidateParam(folder, "folder");
			EwsUtilities.ValidateParam(propertySet, "propertySet");
			new GetFolderRequestForLoad(this, ServiceErrorHandling.ThrowOnError)
			{
				FolderIds = { folder },
				PropertySet = propertySet
			}.Execute();
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00020958 File Offset: 0x0001F958
		internal Folder BindToFolder(FolderId folderId, PropertySet propertySet)
		{
			EwsUtilities.ValidateParam(folderId, "folderId");
			EwsUtilities.ValidateParam(propertySet, "propertySet");
			ServiceResponseCollection<GetFolderResponse> serviceResponseCollection = new GetFolderRequest(this, ServiceErrorHandling.ThrowOnError)
			{
				FolderIds = { folderId },
				PropertySet = propertySet
			}.Execute();
			return serviceResponseCollection[0].Folder;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x000209AC File Offset: 0x0001F9AC
		internal TFolder BindToFolder<TFolder>(FolderId folderId, PropertySet propertySet) where TFolder : Folder
		{
			Folder folder = this.BindToFolder(folderId, propertySet);
			if (folder is TFolder)
			{
				return (TFolder)((object)folder);
			}
			throw new ServiceLocalException(string.Format(Strings.FolderTypeNotCompatible, folder.GetType().Name, typeof(TFolder).Name));
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00020A00 File Offset: 0x0001FA00
		internal void DeleteFolder(FolderId folderId, DeleteMode deleteMode)
		{
			EwsUtilities.ValidateParam(folderId, "folderId");
			new DeleteFolderRequest(this, ServiceErrorHandling.ThrowOnError)
			{
				FolderIds = { folderId },
				DeleteMode = deleteMode
			}.Execute();
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00020A3C File Offset: 0x0001FA3C
		internal void EmptyFolder(FolderId folderId, DeleteMode deleteMode, bool deleteSubFolders)
		{
			EwsUtilities.ValidateParam(folderId, "folderId");
			new EmptyFolderRequest(this, ServiceErrorHandling.ThrowOnError)
			{
				FolderIds = { folderId },
				DeleteMode = deleteMode,
				DeleteSubFolders = deleteSubFolders
			}.Execute();
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00020A80 File Offset: 0x0001FA80
		internal void MarkAllItemsAsRead(FolderId folderId, bool readFlag, bool suppressReadReceipts)
		{
			EwsUtilities.ValidateParam(folderId, "folderId");
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2013, "MarkAllItemsAsRead");
			new MarkAllItemsAsReadRequest(this, ServiceErrorHandling.ThrowOnError)
			{
				FolderIds = { folderId },
				ReadFlag = readFlag,
				SuppressReadReceipts = suppressReadReceipts
			}.Execute();
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00020AD0 File Offset: 0x0001FAD0
		private ServiceResponseCollection<ServiceResponse> InternalCreateItems(IEnumerable<Item> items, FolderId parentFolderId, MessageDisposition? messageDisposition, SendInvitationsMode? sendInvitationsMode, ServiceErrorHandling errorHandling)
		{
			return new CreateItemRequest(this, errorHandling)
			{
				ParentFolderId = parentFolderId,
				Items = items,
				MessageDisposition = messageDisposition,
				SendInvitationsMode = sendInvitationsMode
			}.Execute();
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00020B1C File Offset: 0x0001FB1C
		public ServiceResponseCollection<ServiceResponse> CreateItems(IEnumerable<Item> items, FolderId parentFolderId, MessageDisposition? messageDisposition, SendInvitationsMode? sendInvitationsMode)
		{
			if (!items.TrueForAll((Item item) => item.IsNew))
			{
				throw new ServiceValidationException(Strings.CreateItemsDoesNotHandleExistingItems);
			}
			if (!items.TrueForAll((Item item) => !item.HasUnprocessedAttachmentChanges()))
			{
				throw new ServiceValidationException(Strings.CreateItemsDoesNotAllowAttachments);
			}
			return this.InternalCreateItems(items, parentFolderId, messageDisposition, sendInvitationsMode, ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00020BA0 File Offset: 0x0001FBA0
		internal void CreateItem(Item item, FolderId parentFolderId, MessageDisposition? messageDisposition, SendInvitationsMode? sendInvitationsMode)
		{
			this.InternalCreateItems(new Item[] { item }, parentFolderId, messageDisposition, sendInvitationsMode, ServiceErrorHandling.ThrowOnError);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00020BC8 File Offset: 0x0001FBC8
		private ServiceResponseCollection<UpdateItemResponse> InternalUpdateItems(IEnumerable<Item> items, FolderId savedItemsDestinationFolderId, ConflictResolutionMode conflictResolution, MessageDisposition? messageDisposition, SendInvitationsOrCancellationsMode? sendInvitationsOrCancellationsMode, ServiceErrorHandling errorHandling, bool suppressReadReceipt)
		{
			UpdateItemRequest updateItemRequest = new UpdateItemRequest(this, errorHandling);
			updateItemRequest.Items.AddRange(items);
			updateItemRequest.SavedItemsDestinationFolder = savedItemsDestinationFolderId;
			updateItemRequest.MessageDisposition = messageDisposition;
			updateItemRequest.ConflictResolutionMode = conflictResolution;
			updateItemRequest.SendInvitationsOrCancellationsMode = sendInvitationsOrCancellationsMode;
			updateItemRequest.SuppressReadReceipts = suppressReadReceipt;
			return updateItemRequest.Execute();
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00020C16 File Offset: 0x0001FC16
		public ServiceResponseCollection<UpdateItemResponse> UpdateItems(IEnumerable<Item> items, FolderId savedItemsDestinationFolderId, ConflictResolutionMode conflictResolution, MessageDisposition? messageDisposition, SendInvitationsOrCancellationsMode? sendInvitationsOrCancellationsMode)
		{
			return this.UpdateItems(items, savedItemsDestinationFolderId, conflictResolution, messageDisposition, sendInvitationsOrCancellationsMode, false);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00020C44 File Offset: 0x0001FC44
		public ServiceResponseCollection<UpdateItemResponse> UpdateItems(IEnumerable<Item> items, FolderId savedItemsDestinationFolderId, ConflictResolutionMode conflictResolution, MessageDisposition? messageDisposition, SendInvitationsOrCancellationsMode? sendInvitationsOrCancellationsMode, bool suppressReadReceipts)
		{
			if (!items.TrueForAll((Item item) => !item.IsNew && item.IsDirty))
			{
				throw new ServiceValidationException(Strings.UpdateItemsDoesNotSupportNewOrUnchangedItems);
			}
			if (!items.TrueForAll((Item item) => !item.HasUnprocessedAttachmentChanges()))
			{
				throw new ServiceValidationException(Strings.UpdateItemsDoesNotAllowAttachments);
			}
			return this.InternalUpdateItems(items, savedItemsDestinationFolderId, conflictResolution, messageDisposition, sendInvitationsOrCancellationsMode, ServiceErrorHandling.ReturnErrors, suppressReadReceipts);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00020CCB File Offset: 0x0001FCCB
		internal Item UpdateItem(Item item, FolderId savedItemsDestinationFolderId, ConflictResolutionMode conflictResolution, MessageDisposition? messageDisposition, SendInvitationsOrCancellationsMode? sendInvitationsOrCancellationsMode)
		{
			return this.UpdateItem(item, savedItemsDestinationFolderId, conflictResolution, messageDisposition, sendInvitationsOrCancellationsMode, false);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00020CDC File Offset: 0x0001FCDC
		internal Item UpdateItem(Item item, FolderId savedItemsDestinationFolderId, ConflictResolutionMode conflictResolution, MessageDisposition? messageDisposition, SendInvitationsOrCancellationsMode? sendInvitationsOrCancellationsMode, bool suppressReadReceipts)
		{
			ServiceResponseCollection<UpdateItemResponse> serviceResponseCollection = this.InternalUpdateItems(new Item[] { item }, savedItemsDestinationFolderId, conflictResolution, messageDisposition, sendInvitationsOrCancellationsMode, ServiceErrorHandling.ThrowOnError, suppressReadReceipts);
			return serviceResponseCollection[0].ReturnedItem;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00020D14 File Offset: 0x0001FD14
		internal void SendItem(Item item, FolderId savedCopyDestinationFolderId)
		{
			new SendItemRequest(this, ServiceErrorHandling.ThrowOnError)
			{
				Items = new Item[] { item },
				SavedCopyDestinationFolderId = savedCopyDestinationFolderId
			}.Execute();
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00020D4C File Offset: 0x0001FD4C
		private ServiceResponseCollection<MoveCopyItemResponse> InternalCopyItems(IEnumerable<ItemId> itemIds, FolderId destinationFolderId, bool? returnNewItemIds, ServiceErrorHandling errorHandling)
		{
			CopyItemRequest copyItemRequest = new CopyItemRequest(this, errorHandling);
			copyItemRequest.ItemIds.AddRange(itemIds);
			copyItemRequest.DestinationFolderId = destinationFolderId;
			copyItemRequest.ReturnNewItemIds = returnNewItemIds;
			return copyItemRequest.Execute();
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00020D84 File Offset: 0x0001FD84
		public ServiceResponseCollection<MoveCopyItemResponse> CopyItems(IEnumerable<ItemId> itemIds, FolderId destinationFolderId)
		{
			return this.InternalCopyItems(itemIds, destinationFolderId, default(bool?), ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00020DA3 File Offset: 0x0001FDA3
		public ServiceResponseCollection<MoveCopyItemResponse> CopyItems(IEnumerable<ItemId> itemIds, FolderId destinationFolderId, bool returnNewItemIds)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010_SP1, "CopyItems");
			return this.InternalCopyItems(itemIds, destinationFolderId, new bool?(returnNewItemIds), ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00020DC0 File Offset: 0x0001FDC0
		internal Item CopyItem(ItemId itemId, FolderId destinationFolderId)
		{
			return this.InternalCopyItems(new ItemId[] { itemId }, destinationFolderId, default(bool?), ServiceErrorHandling.ThrowOnError)[0].Item;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00020DF8 File Offset: 0x0001FDF8
		private ServiceResponseCollection<MoveCopyItemResponse> InternalMoveItems(IEnumerable<ItemId> itemIds, FolderId destinationFolderId, bool? returnNewItemIds, ServiceErrorHandling errorHandling)
		{
			MoveItemRequest moveItemRequest = new MoveItemRequest(this, errorHandling);
			moveItemRequest.ItemIds.AddRange(itemIds);
			moveItemRequest.DestinationFolderId = destinationFolderId;
			moveItemRequest.ReturnNewItemIds = returnNewItemIds;
			return moveItemRequest.Execute();
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00020E30 File Offset: 0x0001FE30
		public ServiceResponseCollection<MoveCopyItemResponse> MoveItems(IEnumerable<ItemId> itemIds, FolderId destinationFolderId)
		{
			return this.InternalMoveItems(itemIds, destinationFolderId, default(bool?), ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00020E4F File Offset: 0x0001FE4F
		public ServiceResponseCollection<MoveCopyItemResponse> MoveItems(IEnumerable<ItemId> itemIds, FolderId destinationFolderId, bool returnNewItemIds)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010_SP1, "MoveItems");
			return this.InternalMoveItems(itemIds, destinationFolderId, new bool?(returnNewItemIds), ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00020E6C File Offset: 0x0001FE6C
		internal Item MoveItem(ItemId itemId, FolderId destinationFolderId)
		{
			return this.InternalMoveItems(new ItemId[] { itemId }, destinationFolderId, default(bool?), ServiceErrorHandling.ThrowOnError)[0].Item;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00020EA4 File Offset: 0x0001FEA4
		public ServiceResponseCollection<ArchiveItemResponse> ArchiveItems(IEnumerable<ItemId> itemIds, FolderId sourceFolderId)
		{
			ArchiveItemRequest archiveItemRequest = new ArchiveItemRequest(this, ServiceErrorHandling.ReturnErrors);
			archiveItemRequest.Ids.AddRange(itemIds);
			archiveItemRequest.SourceFolderId = sourceFolderId;
			return archiveItemRequest.Execute();
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00020ED4 File Offset: 0x0001FED4
		internal ServiceResponseCollection<FindItemResponse<TItem>> FindItems<TItem>(IEnumerable<FolderId> parentFolderIds, SearchFilter searchFilter, string queryString, ViewBase view, Grouping groupBy, ServiceErrorHandling errorHandlingMode) where TItem : Item
		{
			EwsUtilities.ValidateParamCollection(parentFolderIds, "parentFolderIds");
			EwsUtilities.ValidateParam(view, "view");
			EwsUtilities.ValidateParamAllowNull(groupBy, "groupBy");
			EwsUtilities.ValidateParamAllowNull(queryString, "queryString");
			EwsUtilities.ValidateParamAllowNull(searchFilter, "searchFilter");
			FindItemRequest<TItem> findItemRequest = new FindItemRequest<TItem>(this, errorHandlingMode);
			findItemRequest.ParentFolderIds.AddRange(parentFolderIds);
			findItemRequest.SearchFilter = searchFilter;
			findItemRequest.QueryString = queryString;
			findItemRequest.View = view;
			findItemRequest.GroupBy = groupBy;
			return findItemRequest.Execute();
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00020F54 File Offset: 0x0001FF54
		public FindItemsResults<Item> FindItems(FolderId parentFolderId, string queryString, ViewBase view)
		{
			EwsUtilities.ValidateParamAllowNull(queryString, "queryString");
			ServiceResponseCollection<FindItemResponse<Item>> serviceResponseCollection = this.FindItems<Item>(new FolderId[] { parentFolderId }, null, queryString, view, null, ServiceErrorHandling.ThrowOnError);
			return serviceResponseCollection[0].Results;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00020F90 File Offset: 0x0001FF90
		public FindItemsResults<Item> FindItems(FolderId parentFolderId, string queryString, bool returnHighlightTerms, ViewBase view)
		{
			FolderId[] array = new FolderId[] { parentFolderId };
			EwsUtilities.ValidateParamCollection(array, "parentFolderIds");
			EwsUtilities.ValidateParam(view, "view");
			EwsUtilities.ValidateParamAllowNull(queryString, "queryString");
			EwsUtilities.ValidateParamAllowNull(returnHighlightTerms, "returnHighlightTerms");
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2013, "FindItems");
			FindItemRequest<Item> findItemRequest = new FindItemRequest<Item>(this, ServiceErrorHandling.ThrowOnError);
			findItemRequest.ParentFolderIds.AddRange(array);
			findItemRequest.QueryString = queryString;
			findItemRequest.ReturnHighlightTerms = returnHighlightTerms;
			findItemRequest.View = view;
			ServiceResponseCollection<FindItemResponse<Item>> serviceResponseCollection = findItemRequest.Execute();
			return serviceResponseCollection[0].Results;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00021028 File Offset: 0x00020028
		public GroupedFindItemsResults<Item> FindItems(FolderId parentFolderId, string queryString, bool returnHighlightTerms, ViewBase view, Grouping groupBy)
		{
			FolderId[] array = new FolderId[] { parentFolderId };
			EwsUtilities.ValidateParamCollection(array, "parentFolderIds");
			EwsUtilities.ValidateParam(view, "view");
			EwsUtilities.ValidateParam(groupBy, "groupBy");
			EwsUtilities.ValidateParamAllowNull(queryString, "queryString");
			EwsUtilities.ValidateParamAllowNull(returnHighlightTerms, "returnHighlightTerms");
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2013, "FindItems");
			FindItemRequest<Item> findItemRequest = new FindItemRequest<Item>(this, ServiceErrorHandling.ThrowOnError);
			findItemRequest.ParentFolderIds.AddRange(array);
			findItemRequest.QueryString = queryString;
			findItemRequest.ReturnHighlightTerms = returnHighlightTerms;
			findItemRequest.View = view;
			findItemRequest.GroupBy = groupBy;
			ServiceResponseCollection<FindItemResponse<Item>> serviceResponseCollection = findItemRequest.Execute();
			return serviceResponseCollection[0].GroupedFindResults;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000210D4 File Offset: 0x000200D4
		public FindItemsResults<Item> FindItems(FolderId parentFolderId, SearchFilter searchFilter, ViewBase view)
		{
			EwsUtilities.ValidateParamAllowNull(searchFilter, "searchFilter");
			ServiceResponseCollection<FindItemResponse<Item>> serviceResponseCollection = this.FindItems<Item>(new FolderId[] { parentFolderId }, searchFilter, null, view, null, ServiceErrorHandling.ThrowOnError);
			return serviceResponseCollection[0].Results;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00021110 File Offset: 0x00020110
		public FindItemsResults<Item> FindItems(FolderId parentFolderId, ViewBase view)
		{
			ServiceResponseCollection<FindItemResponse<Item>> serviceResponseCollection = this.FindItems<Item>(new FolderId[] { parentFolderId }, null, null, view, null, ServiceErrorHandling.ThrowOnError);
			return serviceResponseCollection[0].Results;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00021141 File Offset: 0x00020141
		public FindItemsResults<Item> FindItems(WellKnownFolderName parentFolderName, string queryString, ViewBase view)
		{
			return this.FindItems(new FolderId(parentFolderName), queryString, view);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00021151 File Offset: 0x00020151
		public FindItemsResults<Item> FindItems(WellKnownFolderName parentFolderName, SearchFilter searchFilter, ViewBase view)
		{
			return this.FindItems(new FolderId(parentFolderName), searchFilter, view);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00021161 File Offset: 0x00020161
		public FindItemsResults<Item> FindItems(WellKnownFolderName parentFolderName, ViewBase view)
		{
			return this.FindItems(new FolderId(parentFolderName), null, view);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00021174 File Offset: 0x00020174
		public GroupedFindItemsResults<Item> FindItems(FolderId parentFolderId, string queryString, ViewBase view, Grouping groupBy)
		{
			EwsUtilities.ValidateParam(groupBy, "groupBy");
			EwsUtilities.ValidateParamAllowNull(queryString, "queryString");
			ServiceResponseCollection<FindItemResponse<Item>> serviceResponseCollection = this.FindItems<Item>(new FolderId[] { parentFolderId }, null, queryString, view, groupBy, ServiceErrorHandling.ThrowOnError);
			return serviceResponseCollection[0].GroupedFindResults;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000211C0 File Offset: 0x000201C0
		public GroupedFindItemsResults<Item> FindItems(FolderId parentFolderId, SearchFilter searchFilter, ViewBase view, Grouping groupBy)
		{
			EwsUtilities.ValidateParam(groupBy, "groupBy");
			EwsUtilities.ValidateParamAllowNull(searchFilter, "searchFilter");
			ServiceResponseCollection<FindItemResponse<Item>> serviceResponseCollection = this.FindItems<Item>(new FolderId[] { parentFolderId }, searchFilter, null, view, groupBy, ServiceErrorHandling.ThrowOnError);
			return serviceResponseCollection[0].GroupedFindResults;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0002120C File Offset: 0x0002020C
		public GroupedFindItemsResults<Item> FindItems(FolderId parentFolderId, ViewBase view, Grouping groupBy)
		{
			EwsUtilities.ValidateParam(groupBy, "groupBy");
			ServiceResponseCollection<FindItemResponse<Item>> serviceResponseCollection = this.FindItems<Item>(new FolderId[] { parentFolderId }, null, null, view, groupBy, ServiceErrorHandling.ThrowOnError);
			return serviceResponseCollection[0].GroupedFindResults;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00021248 File Offset: 0x00020248
		internal ServiceResponseCollection<FindItemResponse<TItem>> FindItems<TItem>(FolderId parentFolderId, SearchFilter searchFilter, ViewBase view, Grouping groupBy) where TItem : Item
		{
			return this.FindItems<TItem>(new FolderId[] { parentFolderId }, searchFilter, null, view, groupBy, ServiceErrorHandling.ThrowOnError);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0002126D File Offset: 0x0002026D
		public GroupedFindItemsResults<Item> FindItems(WellKnownFolderName parentFolderName, string queryString, ViewBase view, Grouping groupBy)
		{
			EwsUtilities.ValidateParam(groupBy, "groupBy");
			return this.FindItems(new FolderId(parentFolderName), queryString, view, groupBy);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0002128B File Offset: 0x0002028B
		public GroupedFindItemsResults<Item> FindItems(WellKnownFolderName parentFolderName, SearchFilter searchFilter, ViewBase view, Grouping groupBy)
		{
			return this.FindItems(new FolderId(parentFolderName), searchFilter, view, groupBy);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x000212A0 File Offset: 0x000202A0
		public FindItemsResults<Appointment> FindAppointments(FolderId parentFolderId, CalendarView calendarView)
		{
			ServiceResponseCollection<FindItemResponse<Appointment>> serviceResponseCollection = this.FindItems<Appointment>(new FolderId[] { parentFolderId }, null, null, calendarView, null, ServiceErrorHandling.ThrowOnError);
			return serviceResponseCollection[0].Results;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000212D1 File Offset: 0x000202D1
		public FindItemsResults<Appointment> FindAppointments(WellKnownFolderName parentFolderName, CalendarView calendarView)
		{
			return this.FindAppointments(new FolderId(parentFolderName), calendarView);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x000212E0 File Offset: 0x000202E0
		public ServiceResponseCollection<ServiceResponse> LoadPropertiesForItems(IEnumerable<Item> items, PropertySet propertySet)
		{
			EwsUtilities.ValidateParamCollection(items, "items");
			EwsUtilities.ValidateParam(propertySet, "propertySet");
			return this.InternalLoadPropertiesForItems(items, propertySet, ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00021304 File Offset: 0x00020304
		internal ServiceResponseCollection<ServiceResponse> InternalLoadPropertiesForItems(IEnumerable<Item> items, PropertySet propertySet, ServiceErrorHandling errorHandling)
		{
			GetItemRequestForLoad getItemRequestForLoad = new GetItemRequestForLoad(this, errorHandling);
			getItemRequestForLoad.ItemIds.AddRange(items);
			getItemRequestForLoad.PropertySet = propertySet;
			return getItemRequestForLoad.Execute();
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00021334 File Offset: 0x00020334
		private ServiceResponseCollection<GetItemResponse> InternalBindToItems(IEnumerable<ItemId> itemIds, PropertySet propertySet, ServiceErrorHandling errorHandling)
		{
			GetItemRequest getItemRequest = new GetItemRequest(this, errorHandling);
			getItemRequest.ItemIds.AddRange(itemIds);
			getItemRequest.PropertySet = propertySet;
			return getItemRequest.Execute();
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00021362 File Offset: 0x00020362
		public ServiceResponseCollection<GetItemResponse> BindToItems(IEnumerable<ItemId> itemIds, PropertySet propertySet)
		{
			EwsUtilities.ValidateParamCollection(itemIds, "itemIds");
			EwsUtilities.ValidateParam(propertySet, "propertySet");
			return this.InternalBindToItems(itemIds, propertySet, ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00021384 File Offset: 0x00020384
		internal Item BindToItem(ItemId itemId, PropertySet propertySet)
		{
			EwsUtilities.ValidateParam(itemId, "itemId");
			EwsUtilities.ValidateParam(propertySet, "propertySet");
			ServiceResponseCollection<GetItemResponse> serviceResponseCollection = this.InternalBindToItems(new ItemId[] { itemId }, propertySet, ServiceErrorHandling.ThrowOnError);
			return serviceResponseCollection[0].Item;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x000213C8 File Offset: 0x000203C8
		internal TItem BindToItem<TItem>(ItemId itemId, PropertySet propertySet) where TItem : Item
		{
			Item item = this.BindToItem(itemId, propertySet);
			if (item is TItem)
			{
				return (TItem)((object)item);
			}
			throw new ServiceLocalException(string.Format(Strings.ItemTypeNotCompatible, item.GetType().Name, typeof(TItem).Name));
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002141C File Offset: 0x0002041C
		private ServiceResponseCollection<ServiceResponse> InternalDeleteItems(IEnumerable<ItemId> itemIds, DeleteMode deleteMode, SendCancellationsMode? sendCancellationsMode, AffectedTaskOccurrence? affectedTaskOccurrences, ServiceErrorHandling errorHandling, bool suppressReadReceipts)
		{
			DeleteItemRequest deleteItemRequest = new DeleteItemRequest(this, errorHandling);
			deleteItemRequest.ItemIds.AddRange(itemIds);
			deleteItemRequest.DeleteMode = deleteMode;
			deleteItemRequest.SendCancellationsMode = sendCancellationsMode;
			deleteItemRequest.AffectedTaskOccurrences = affectedTaskOccurrences;
			deleteItemRequest.SuppressReadReceipts = suppressReadReceipts;
			return deleteItemRequest.Execute();
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00021462 File Offset: 0x00020462
		public ServiceResponseCollection<ServiceResponse> DeleteItems(IEnumerable<ItemId> itemIds, DeleteMode deleteMode, SendCancellationsMode? sendCancellationsMode, AffectedTaskOccurrence? affectedTaskOccurrences)
		{
			return this.DeleteItems(itemIds, deleteMode, sendCancellationsMode, affectedTaskOccurrences, false);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00021470 File Offset: 0x00020470
		public ServiceResponseCollection<ServiceResponse> DeleteItems(IEnumerable<ItemId> itemIds, DeleteMode deleteMode, SendCancellationsMode? sendCancellationsMode, AffectedTaskOccurrence? affectedTaskOccurrences, bool suppressReadReceipt)
		{
			EwsUtilities.ValidateParamCollection(itemIds, "itemIds");
			return this.InternalDeleteItems(itemIds, deleteMode, sendCancellationsMode, affectedTaskOccurrences, ServiceErrorHandling.ReturnErrors, suppressReadReceipt);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002148B File Offset: 0x0002048B
		internal void DeleteItem(ItemId itemId, DeleteMode deleteMode, SendCancellationsMode? sendCancellationsMode, AffectedTaskOccurrence? affectedTaskOccurrences)
		{
			this.DeleteItem(itemId, deleteMode, sendCancellationsMode, affectedTaskOccurrences, false);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002149C File Offset: 0x0002049C
		internal void DeleteItem(ItemId itemId, DeleteMode deleteMode, SendCancellationsMode? sendCancellationsMode, AffectedTaskOccurrence? affectedTaskOccurrences, bool suppressReadReceipts)
		{
			EwsUtilities.ValidateParam(itemId, "itemId");
			this.InternalDeleteItems(new ItemId[] { itemId }, deleteMode, sendCancellationsMode, affectedTaskOccurrences, ServiceErrorHandling.ThrowOnError, suppressReadReceipts);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000214D0 File Offset: 0x000204D0
		public ServiceResponseCollection<MarkAsJunkResponse> MarkAsJunk(IEnumerable<ItemId> itemIds, bool isJunk, bool moveItem)
		{
			MarkAsJunkRequest markAsJunkRequest = new MarkAsJunkRequest(this, ServiceErrorHandling.ReturnErrors);
			markAsJunkRequest.ItemIds.AddRange(itemIds);
			markAsJunkRequest.IsJunk = isJunk;
			markAsJunkRequest.MoveItem = moveItem;
			return markAsJunkRequest.Execute();
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00021508 File Offset: 0x00020508
		private ServiceResponseCollection<GetAttachmentResponse> InternalGetAttachments(IEnumerable<Attachment> attachments, BodyType? bodyType, IEnumerable<PropertyDefinitionBase> additionalProperties, ServiceErrorHandling errorHandling)
		{
			GetAttachmentRequest getAttachmentRequest = new GetAttachmentRequest(this, errorHandling);
			getAttachmentRequest.Attachments.AddRange(attachments);
			getAttachmentRequest.BodyType = bodyType;
			if (additionalProperties != null)
			{
				getAttachmentRequest.AdditionalProperties.AddRange(additionalProperties);
			}
			return getAttachmentRequest.Execute();
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00021546 File Offset: 0x00020546
		public ServiceResponseCollection<GetAttachmentResponse> GetAttachments(Attachment[] attachments, BodyType? bodyType, IEnumerable<PropertyDefinitionBase> additionalProperties)
		{
			return this.InternalGetAttachments(attachments, bodyType, additionalProperties, ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00021554 File Offset: 0x00020554
		public ServiceResponseCollection<GetAttachmentResponse> GetAttachments(string[] attachmentIds, BodyType? bodyType, IEnumerable<PropertyDefinitionBase> additionalProperties)
		{
			GetAttachmentRequest getAttachmentRequest = new GetAttachmentRequest(this, ServiceErrorHandling.ReturnErrors);
			getAttachmentRequest.AttachmentIds.AddRange(attachmentIds);
			getAttachmentRequest.BodyType = bodyType;
			if (additionalProperties != null)
			{
				getAttachmentRequest.AdditionalProperties.AddRange(additionalProperties);
			}
			return getAttachmentRequest.Execute();
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00021594 File Offset: 0x00020594
		internal void GetAttachment(Attachment attachment, BodyType? bodyType, IEnumerable<PropertyDefinitionBase> additionalProperties)
		{
			this.InternalGetAttachments(new Attachment[] { attachment }, bodyType, additionalProperties, ServiceErrorHandling.ThrowOnError);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x000215B8 File Offset: 0x000205B8
		internal ServiceResponseCollection<CreateAttachmentResponse> CreateAttachments(string parentItemId, IEnumerable<Attachment> attachments)
		{
			CreateAttachmentRequest createAttachmentRequest = new CreateAttachmentRequest(this, ServiceErrorHandling.ReturnErrors);
			createAttachmentRequest.ParentItemId = parentItemId;
			createAttachmentRequest.Attachments.AddRange(attachments);
			return createAttachmentRequest.Execute();
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000215E8 File Offset: 0x000205E8
		internal ServiceResponseCollection<DeleteAttachmentResponse> DeleteAttachments(IEnumerable<Attachment> attachments)
		{
			DeleteAttachmentRequest deleteAttachmentRequest = new DeleteAttachmentRequest(this, ServiceErrorHandling.ReturnErrors);
			deleteAttachmentRequest.Attachments.AddRange(attachments);
			return deleteAttachmentRequest.Execute();
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0002160F File Offset: 0x0002060F
		public NameResolutionCollection ResolveName(string nameToResolve)
		{
			return this.ResolveName(nameToResolve, ResolveNameSearchLocation.ContactsThenDirectory, false);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0002161A File Offset: 0x0002061A
		public NameResolutionCollection ResolveName(string nameToResolve, IEnumerable<FolderId> parentFolderIds, ResolveNameSearchLocation searchScope, bool returnContactDetails)
		{
			return this.ResolveName(nameToResolve, parentFolderIds, searchScope, returnContactDetails, null);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00021628 File Offset: 0x00020628
		public NameResolutionCollection ResolveName(string nameToResolve, IEnumerable<FolderId> parentFolderIds, ResolveNameSearchLocation searchScope, bool returnContactDetails, PropertySet contactDataPropertySet)
		{
			if (contactDataPropertySet != null)
			{
				EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010_SP1, "ResolveName");
			}
			EwsUtilities.ValidateParam(nameToResolve, "nameToResolve");
			if (parentFolderIds != null)
			{
				EwsUtilities.ValidateParamCollection(parentFolderIds, "parentFolderIds");
			}
			ResolveNamesRequest resolveNamesRequest = new ResolveNamesRequest(this);
			resolveNamesRequest.NameToResolve = nameToResolve;
			resolveNamesRequest.ReturnFullContactData = returnContactDetails;
			resolveNamesRequest.ParentFolderIds.AddRange(parentFolderIds);
			resolveNamesRequest.SearchLocation = searchScope;
			resolveNamesRequest.ContactDataPropertySet = contactDataPropertySet;
			return resolveNamesRequest.Execute()[0].Resolutions;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x000216A0 File Offset: 0x000206A0
		public NameResolutionCollection ResolveName(string nameToResolve, ResolveNameSearchLocation searchScope, bool returnContactDetails, PropertySet contactDataPropertySet)
		{
			return this.ResolveName(nameToResolve, null, searchScope, returnContactDetails, contactDataPropertySet);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x000216AE File Offset: 0x000206AE
		public NameResolutionCollection ResolveName(string nameToResolve, ResolveNameSearchLocation searchScope, bool returnContactDetails)
		{
			return this.ResolveName(nameToResolve, null, searchScope, returnContactDetails);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x000216BC File Offset: 0x000206BC
		public ExpandGroupResults ExpandGroup(EmailAddress emailAddress)
		{
			EwsUtilities.ValidateParam(emailAddress, "emailAddress");
			return new ExpandGroupRequest(this)
			{
				EmailAddress = emailAddress
			}.Execute()[0].Members;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x000216F4 File Offset: 0x000206F4
		public ExpandGroupResults ExpandGroup(ItemId groupId)
		{
			EwsUtilities.ValidateParam(groupId, "groupId");
			return this.ExpandGroup(new EmailAddress
			{
				Id = groupId
			});
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00021720 File Offset: 0x00020720
		public ExpandGroupResults ExpandGroup(string smtpAddress)
		{
			EwsUtilities.ValidateParam(smtpAddress, "smtpAddress");
			return this.ExpandGroup(new EmailAddress(smtpAddress));
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002173C File Offset: 0x0002073C
		public ExpandGroupResults ExpandGroup(string address, string routingType)
		{
			EwsUtilities.ValidateParam(address, "address");
			EwsUtilities.ValidateParam(routingType, "routingType");
			return this.ExpandGroup(new EmailAddress(address)
			{
				RoutingType = routingType
			});
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00021774 File Offset: 0x00020774
		public DateTime? GetPasswordExpirationDate(string mailboxSmtpAddress)
		{
			return new GetPasswordExpirationDateRequest(this)
			{
				MailboxSmtpAddress = mailboxSmtpAddress
			}.Execute().PasswordExpirationDate;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002179A File Offset: 0x0002079A
		public PullSubscription SubscribeToPullNotifications(IEnumerable<FolderId> folderIds, int timeout, string watermark, params EventType[] eventTypes)
		{
			EwsUtilities.ValidateParamCollection(folderIds, "folderIds");
			return this.BuildSubscribeToPullNotificationsRequest(folderIds, timeout, watermark, eventTypes).Execute()[0].Subscription;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x000217C2 File Offset: 0x000207C2
		public IAsyncResult BeginSubscribeToPullNotifications(AsyncCallback callback, object state, IEnumerable<FolderId> folderIds, int timeout, string watermark, params EventType[] eventTypes)
		{
			EwsUtilities.ValidateParamCollection(folderIds, "folderIds");
			return this.BuildSubscribeToPullNotificationsRequest(folderIds, timeout, watermark, eventTypes).BeginExecute(callback, state);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x000217E3 File Offset: 0x000207E3
		public PullSubscription SubscribeToPullNotificationsOnAllFolders(int timeout, string watermark, params EventType[] eventTypes)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010, "SubscribeToPullNotificationsOnAllFolders");
			return this.BuildSubscribeToPullNotificationsRequest(null, timeout, watermark, eventTypes).Execute()[0].Subscription;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002180B File Offset: 0x0002080B
		public IAsyncResult BeginSubscribeToPullNotificationsOnAllFolders(AsyncCallback callback, object state, int timeout, string watermark, params EventType[] eventTypes)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010, "BeginSubscribeToPullNotificationsOnAllFolders");
			return this.BuildSubscribeToPullNotificationsRequest(null, timeout, watermark, eventTypes).BeginExecute(callback, state);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002182C File Offset: 0x0002082C
		public PullSubscription EndSubscribeToPullNotifications(IAsyncResult asyncResult)
		{
			SubscribeToPullNotificationsRequest subscribeToPullNotificationsRequest = AsyncRequestResult.ExtractServiceRequest<SubscribeToPullNotificationsRequest>(this, asyncResult);
			return subscribeToPullNotificationsRequest.EndExecute(asyncResult)[0].Subscription;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00021854 File Offset: 0x00020854
		private SubscribeToPullNotificationsRequest BuildSubscribeToPullNotificationsRequest(IEnumerable<FolderId> folderIds, int timeout, string watermark, EventType[] eventTypes)
		{
			if (timeout < 1 || timeout > 1440)
			{
				throw new ArgumentOutOfRangeException("timeout", Strings.TimeoutMustBeBetween1And1440);
			}
			EwsUtilities.ValidateParamCollection(eventTypes, "eventTypes");
			SubscribeToPullNotificationsRequest subscribeToPullNotificationsRequest = new SubscribeToPullNotificationsRequest(this);
			if (folderIds != null)
			{
				subscribeToPullNotificationsRequest.FolderIds.AddRange(folderIds);
			}
			subscribeToPullNotificationsRequest.Timeout = timeout;
			subscribeToPullNotificationsRequest.EventTypes.AddRange(eventTypes);
			subscribeToPullNotificationsRequest.Watermark = watermark;
			return subscribeToPullNotificationsRequest;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x000218C0 File Offset: 0x000208C0
		internal void Unsubscribe(string subscriptionId)
		{
			this.BuildUnsubscribeRequest(subscriptionId).Execute();
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000218CF File Offset: 0x000208CF
		internal IAsyncResult BeginUnsubscribe(AsyncCallback callback, object state, string subscriptionId)
		{
			return this.BuildUnsubscribeRequest(subscriptionId).BeginExecute(callback, state);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x000218E0 File Offset: 0x000208E0
		internal void EndUnsubscribe(IAsyncResult asyncResult)
		{
			UnsubscribeRequest unsubscribeRequest = AsyncRequestResult.ExtractServiceRequest<UnsubscribeRequest>(this, asyncResult);
			unsubscribeRequest.EndExecute(asyncResult);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00021900 File Offset: 0x00020900
		private UnsubscribeRequest BuildUnsubscribeRequest(string subscriptionId)
		{
			EwsUtilities.ValidateParam(subscriptionId, "subscriptionId");
			return new UnsubscribeRequest(this)
			{
				SubscriptionId = subscriptionId
			};
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00021927 File Offset: 0x00020927
		internal GetEventsResults GetEvents(string subscriptionId, string watermark)
		{
			return this.BuildGetEventsRequest(subscriptionId, watermark).Execute()[0].Results;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00021941 File Offset: 0x00020941
		internal IAsyncResult BeginGetEvents(AsyncCallback callback, object state, string subscriptionId, string watermark)
		{
			return this.BuildGetEventsRequest(subscriptionId, watermark).BeginExecute(callback, state);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00021954 File Offset: 0x00020954
		internal GetEventsResults EndGetEvents(IAsyncResult asyncResult)
		{
			GetEventsRequest getEventsRequest = AsyncRequestResult.ExtractServiceRequest<GetEventsRequest>(this, asyncResult);
			return getEventsRequest.EndExecute(asyncResult)[0].Results;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002197C File Offset: 0x0002097C
		private GetEventsRequest BuildGetEventsRequest(string subscriptionId, string watermark)
		{
			EwsUtilities.ValidateParam(subscriptionId, "subscriptionId");
			EwsUtilities.ValidateParam(watermark, "watermark");
			return new GetEventsRequest(this)
			{
				SubscriptionId = subscriptionId,
				Watermark = watermark
			};
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000219B5 File Offset: 0x000209B5
		public PushSubscription SubscribeToPushNotifications(IEnumerable<FolderId> folderIds, Uri url, int frequency, string watermark, params EventType[] eventTypes)
		{
			EwsUtilities.ValidateParamCollection(folderIds, "folderIds");
			return this.BuildSubscribeToPushNotificationsRequest(folderIds, url, frequency, watermark, null, eventTypes).Execute()[0].Subscription;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000219E0 File Offset: 0x000209E0
		public IAsyncResult BeginSubscribeToPushNotifications(AsyncCallback callback, object state, IEnumerable<FolderId> folderIds, Uri url, int frequency, string watermark, params EventType[] eventTypes)
		{
			EwsUtilities.ValidateParamCollection(folderIds, "folderIds");
			return this.BuildSubscribeToPushNotificationsRequest(folderIds, url, frequency, watermark, null, eventTypes).BeginExecute(callback, state);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00021A04 File Offset: 0x00020A04
		public PushSubscription SubscribeToPushNotificationsOnAllFolders(Uri url, int frequency, string watermark, params EventType[] eventTypes)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010, "SubscribeToPushNotificationsOnAllFolders");
			return this.BuildSubscribeToPushNotificationsRequest(null, url, frequency, watermark, null, eventTypes).Execute()[0].Subscription;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00021A2F File Offset: 0x00020A2F
		public IAsyncResult BeginSubscribeToPushNotificationsOnAllFolders(AsyncCallback callback, object state, Uri url, int frequency, string watermark, params EventType[] eventTypes)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010, "BeginSubscribeToPushNotificationsOnAllFolders");
			return this.BuildSubscribeToPushNotificationsRequest(null, url, frequency, watermark, null, eventTypes).BeginExecute(callback, state);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00021A53 File Offset: 0x00020A53
		public PushSubscription SubscribeToPushNotifications(IEnumerable<FolderId> folderIds, Uri url, int frequency, string watermark, string callerData, params EventType[] eventTypes)
		{
			EwsUtilities.ValidateParamCollection(folderIds, "folderIds");
			return this.BuildSubscribeToPushNotificationsRequest(folderIds, url, frequency, watermark, callerData, eventTypes).Execute()[0].Subscription;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00021A7F File Offset: 0x00020A7F
		public IAsyncResult BeginSubscribeToPushNotifications(AsyncCallback callback, object state, IEnumerable<FolderId> folderIds, Uri url, int frequency, string watermark, string callerData, params EventType[] eventTypes)
		{
			EwsUtilities.ValidateParamCollection(folderIds, "folderIds");
			return this.BuildSubscribeToPushNotificationsRequest(folderIds, url, frequency, watermark, callerData, eventTypes).BeginExecute(callback, state);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00021AA4 File Offset: 0x00020AA4
		public PushSubscription SubscribeToPushNotificationsOnAllFolders(Uri url, int frequency, string watermark, string callerData, params EventType[] eventTypes)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010, "SubscribeToPushNotificationsOnAllFolders");
			return this.BuildSubscribeToPushNotificationsRequest(null, url, frequency, watermark, callerData, eventTypes).Execute()[0].Subscription;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00021AD0 File Offset: 0x00020AD0
		public IAsyncResult BeginSubscribeToPushNotificationsOnAllFolders(AsyncCallback callback, object state, Uri url, int frequency, string watermark, string callerData, params EventType[] eventTypes)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010, "BeginSubscribeToPushNotificationsOnAllFolders");
			return this.BuildSubscribeToPushNotificationsRequest(null, url, frequency, watermark, callerData, eventTypes).BeginExecute(callback, state);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00021AF8 File Offset: 0x00020AF8
		public PushSubscription EndSubscribeToPushNotifications(IAsyncResult asyncResult)
		{
			SubscribeToPushNotificationsRequest subscribeToPushNotificationsRequest = AsyncRequestResult.ExtractServiceRequest<SubscribeToPushNotificationsRequest>(this, asyncResult);
			return subscribeToPushNotificationsRequest.EndExecute(asyncResult)[0].Subscription;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00021B20 File Offset: 0x00020B20
		public void SetTeamMailbox(EmailAddress emailAddress, Uri sharePointSiteUrl, TeamMailboxLifecycleState state)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2013, "SetTeamMailbox");
			if (emailAddress == null)
			{
				throw new ArgumentNullException("emailAddress");
			}
			if (sharePointSiteUrl == null)
			{
				throw new ArgumentNullException("sharePointSiteUrl");
			}
			SetTeamMailboxRequest setTeamMailboxRequest = new SetTeamMailboxRequest(this, emailAddress, sharePointSiteUrl, state);
			setTeamMailboxRequest.Execute();
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00021B6C File Offset: 0x00020B6C
		public void UnpinTeamMailbox(EmailAddress emailAddress)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2013, "UnpinTeamMailbox");
			if (emailAddress == null)
			{
				throw new ArgumentNullException("emailAddress");
			}
			UnpinTeamMailboxRequest unpinTeamMailboxRequest = new UnpinTeamMailboxRequest(this, emailAddress);
			unpinTeamMailboxRequest.Execute();
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00021BA4 File Offset: 0x00020BA4
		private SubscribeToPushNotificationsRequest BuildSubscribeToPushNotificationsRequest(IEnumerable<FolderId> folderIds, Uri url, int frequency, string watermark, string callerData, EventType[] eventTypes)
		{
			EwsUtilities.ValidateParam(url, "url");
			if (frequency < 1 || frequency > 1440)
			{
				throw new ArgumentOutOfRangeException("frequency", Strings.FrequencyMustBeBetween1And1440);
			}
			EwsUtilities.ValidateParamCollection(eventTypes, "eventTypes");
			SubscribeToPushNotificationsRequest subscribeToPushNotificationsRequest = new SubscribeToPushNotificationsRequest(this);
			if (folderIds != null)
			{
				subscribeToPushNotificationsRequest.FolderIds.AddRange(folderIds);
			}
			subscribeToPushNotificationsRequest.Url = url;
			subscribeToPushNotificationsRequest.Frequency = frequency;
			subscribeToPushNotificationsRequest.EventTypes.AddRange(eventTypes);
			subscribeToPushNotificationsRequest.Watermark = watermark;
			subscribeToPushNotificationsRequest.CallerData = callerData;
			return subscribeToPushNotificationsRequest;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00021C2B File Offset: 0x00020C2B
		public StreamingSubscription SubscribeToStreamingNotifications(IEnumerable<FolderId> folderIds, params EventType[] eventTypes)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010_SP1, "SubscribeToStreamingNotifications");
			EwsUtilities.ValidateParamCollection(folderIds, "folderIds");
			return this.BuildSubscribeToStreamingNotificationsRequest(folderIds, eventTypes).Execute()[0].Subscription;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00021C5C File Offset: 0x00020C5C
		public IAsyncResult BeginSubscribeToStreamingNotifications(AsyncCallback callback, object state, IEnumerable<FolderId> folderIds, params EventType[] eventTypes)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010_SP1, "BeginSubscribeToStreamingNotifications");
			EwsUtilities.ValidateParamCollection(folderIds, "folderIds");
			return this.BuildSubscribeToStreamingNotificationsRequest(folderIds, eventTypes).BeginExecute(callback, state);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00021C85 File Offset: 0x00020C85
		public StreamingSubscription SubscribeToStreamingNotificationsOnAllFolders(params EventType[] eventTypes)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010_SP1, "SubscribeToStreamingNotificationsOnAllFolders");
			return this.BuildSubscribeToStreamingNotificationsRequest(null, eventTypes).Execute()[0].Subscription;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00021CAB File Offset: 0x00020CAB
		public IAsyncResult BeginSubscribeToStreamingNotificationsOnAllFolders(AsyncCallback callback, object state, params EventType[] eventTypes)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010_SP1, "BeginSubscribeToStreamingNotificationsOnAllFolders");
			return this.BuildSubscribeToStreamingNotificationsRequest(null, eventTypes).BeginExecute(callback, state);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00021CC8 File Offset: 0x00020CC8
		public StreamingSubscription EndSubscribeToStreamingNotifications(IAsyncResult asyncResult)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010_SP1, "EndSubscribeToStreamingNotifications");
			SubscribeToStreamingNotificationsRequest subscribeToStreamingNotificationsRequest = AsyncRequestResult.ExtractServiceRequest<SubscribeToStreamingNotificationsRequest>(this, asyncResult);
			return subscribeToStreamingNotificationsRequest.EndExecute(asyncResult)[0].Subscription;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00021CFC File Offset: 0x00020CFC
		private SubscribeToStreamingNotificationsRequest BuildSubscribeToStreamingNotificationsRequest(IEnumerable<FolderId> folderIds, EventType[] eventTypes)
		{
			EwsUtilities.ValidateParamCollection(eventTypes, "eventTypes");
			SubscribeToStreamingNotificationsRequest subscribeToStreamingNotificationsRequest = new SubscribeToStreamingNotificationsRequest(this);
			if (folderIds != null)
			{
				subscribeToStreamingNotificationsRequest.FolderIds.AddRange(folderIds);
			}
			subscribeToStreamingNotificationsRequest.EventTypes.AddRange(eventTypes);
			return subscribeToStreamingNotificationsRequest;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00021D37 File Offset: 0x00020D37
		public ChangeCollection<ItemChange> SyncFolderItems(FolderId syncFolderId, PropertySet propertySet, IEnumerable<ItemId> ignoredItemIds, int maxChangesReturned, SyncFolderItemsScope syncScope, string syncState)
		{
			return this.BuildSyncFolderItemsRequest(syncFolderId, propertySet, ignoredItemIds, maxChangesReturned, syncScope, syncState).Execute()[0].Changes;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00021D58 File Offset: 0x00020D58
		public IAsyncResult BeginSyncFolderItems(AsyncCallback callback, object state, FolderId syncFolderId, PropertySet propertySet, IEnumerable<ItemId> ignoredItemIds, int maxChangesReturned, SyncFolderItemsScope syncScope, string syncState)
		{
			return this.BuildSyncFolderItemsRequest(syncFolderId, propertySet, ignoredItemIds, maxChangesReturned, syncScope, syncState).BeginExecute(callback, state);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00021D74 File Offset: 0x00020D74
		public ChangeCollection<ItemChange> EndSyncFolderItems(IAsyncResult asyncResult)
		{
			SyncFolderItemsRequest syncFolderItemsRequest = AsyncRequestResult.ExtractServiceRequest<SyncFolderItemsRequest>(this, asyncResult);
			return syncFolderItemsRequest.EndExecute(asyncResult)[0].Changes;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00021D9C File Offset: 0x00020D9C
		private SyncFolderItemsRequest BuildSyncFolderItemsRequest(FolderId syncFolderId, PropertySet propertySet, IEnumerable<ItemId> ignoredItemIds, int maxChangesReturned, SyncFolderItemsScope syncScope, string syncState)
		{
			EwsUtilities.ValidateParam(syncFolderId, "syncFolderId");
			EwsUtilities.ValidateParam(propertySet, "propertySet");
			SyncFolderItemsRequest syncFolderItemsRequest = new SyncFolderItemsRequest(this);
			syncFolderItemsRequest.SyncFolderId = syncFolderId;
			syncFolderItemsRequest.PropertySet = propertySet;
			if (ignoredItemIds != null)
			{
				syncFolderItemsRequest.IgnoredItemIds.AddRange(ignoredItemIds);
			}
			syncFolderItemsRequest.MaxChangesReturned = maxChangesReturned;
			syncFolderItemsRequest.SyncScope = syncScope;
			syncFolderItemsRequest.SyncState = syncState;
			return syncFolderItemsRequest;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00021DFC File Offset: 0x00020DFC
		public ChangeCollection<FolderChange> SyncFolderHierarchy(FolderId syncFolderId, PropertySet propertySet, string syncState)
		{
			return this.BuildSyncFolderHierarchyRequest(syncFolderId, propertySet, syncState).Execute()[0].Changes;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00021E17 File Offset: 0x00020E17
		public IAsyncResult BeginSyncFolderHierarchy(AsyncCallback callback, object state, FolderId syncFolderId, PropertySet propertySet, string syncState)
		{
			return this.BuildSyncFolderHierarchyRequest(syncFolderId, propertySet, syncState).BeginExecute(callback, state);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00021E2B File Offset: 0x00020E2B
		public ChangeCollection<FolderChange> SyncFolderHierarchy(PropertySet propertySet, string syncState)
		{
			return this.SyncFolderHierarchy(null, propertySet, syncState);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00021E36 File Offset: 0x00020E36
		public IAsyncResult BeginSyncFolderHierarchy(AsyncCallback callback, object state, PropertySet propertySet, string syncState)
		{
			return this.BeginSyncFolderHierarchy(callback, state, null, propertySet, syncState);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00021E44 File Offset: 0x00020E44
		public ChangeCollection<FolderChange> EndSyncFolderHierarchy(IAsyncResult asyncResult)
		{
			SyncFolderHierarchyRequest syncFolderHierarchyRequest = AsyncRequestResult.ExtractServiceRequest<SyncFolderHierarchyRequest>(this, asyncResult);
			return syncFolderHierarchyRequest.EndExecute(asyncResult)[0].Changes;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00021E6C File Offset: 0x00020E6C
		private SyncFolderHierarchyRequest BuildSyncFolderHierarchyRequest(FolderId syncFolderId, PropertySet propertySet, string syncState)
		{
			EwsUtilities.ValidateParamAllowNull(syncFolderId, "syncFolderId");
			EwsUtilities.ValidateParam(propertySet, "propertySet");
			return new SyncFolderHierarchyRequest(this)
			{
				PropertySet = propertySet,
				SyncFolderId = syncFolderId,
				SyncState = syncState
			};
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00021EAC File Offset: 0x00020EAC
		public OofSettings GetUserOofSettings(string smtpAddress)
		{
			EwsUtilities.ValidateParam(smtpAddress, "smtpAddress");
			return new GetUserOofSettingsRequest(this)
			{
				SmtpAddress = smtpAddress
			}.Execute().OofSettings;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00021EE0 File Offset: 0x00020EE0
		public void SetUserOofSettings(string smtpAddress, OofSettings oofSettings)
		{
			EwsUtilities.ValidateParam(smtpAddress, "smtpAddress");
			EwsUtilities.ValidateParam(oofSettings, "oofSettings");
			new SetUserOofSettingsRequest(this)
			{
				SmtpAddress = smtpAddress,
				OofSettings = oofSettings
			}.Execute();
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00021F20 File Offset: 0x00020F20
		public GetUserAvailabilityResults GetUserAvailability(IEnumerable<AttendeeInfo> attendees, TimeWindow timeWindow, AvailabilityData requestedData, AvailabilityOptions options)
		{
			EwsUtilities.ValidateParamCollection(attendees, "attendees");
			EwsUtilities.ValidateParam(timeWindow, "timeWindow");
			EwsUtilities.ValidateParam(options, "options");
			return new GetUserAvailabilityRequest(this)
			{
				Attendees = attendees,
				TimeWindow = timeWindow,
				RequestedData = requestedData,
				Options = options
			}.Execute();
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00021F79 File Offset: 0x00020F79
		public GetUserAvailabilityResults GetUserAvailability(IEnumerable<AttendeeInfo> attendees, TimeWindow timeWindow, AvailabilityData requestedData)
		{
			return this.GetUserAvailability(attendees, timeWindow, requestedData, new AvailabilityOptions());
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00021F8C File Offset: 0x00020F8C
		public EmailAddressCollection GetRoomLists()
		{
			GetRoomListsRequest getRoomListsRequest = new GetRoomListsRequest(this);
			return getRoomListsRequest.Execute().RoomLists;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00021FAC File Offset: 0x00020FAC
		public Collection<EmailAddress> GetRooms(EmailAddress emailAddress)
		{
			EwsUtilities.ValidateParam(emailAddress, "emailAddress");
			return new GetRoomsRequest(this)
			{
				RoomList = emailAddress
			}.Execute().Rooms;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00021FE0 File Offset: 0x00020FE0
		public ICollection<Conversation> FindConversation(ViewBase view, FolderId folderId)
		{
			EwsUtilities.ValidateParam(view, "view");
			EwsUtilities.ValidateParam(folderId, "folderId");
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010_SP1, "FindConversation");
			return new FindConversationRequest(this)
			{
				View = view,
				FolderId = new FolderIdWrapper(folderId)
			}.Execute().Conversations;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00022034 File Offset: 0x00021034
		public ICollection<Conversation> FindConversation(ViewBase view, FolderId folderId, string queryString)
		{
			EwsUtilities.ValidateParam(view, "view");
			EwsUtilities.ValidateParamAllowNull(queryString, "queryString");
			EwsUtilities.ValidateParam(folderId, "folderId");
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2013, "FindConversation");
			return new FindConversationRequest(this)
			{
				View = view,
				FolderId = new FolderIdWrapper(folderId),
				QueryString = queryString
			}.Execute().Conversations;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002209C File Offset: 0x0002109C
		public FindConversationResults FindConversation(ViewBase view, FolderId folderId, string queryString, bool returnHighlightTerms)
		{
			EwsUtilities.ValidateParam(view, "view");
			EwsUtilities.ValidateParamAllowNull(queryString, "queryString");
			EwsUtilities.ValidateParam(returnHighlightTerms, "returnHighlightTerms");
			EwsUtilities.ValidateParam(folderId, "folderId");
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2013, "FindConversation");
			return new FindConversationRequest(this)
			{
				View = view,
				FolderId = new FolderIdWrapper(folderId),
				QueryString = queryString,
				ReturnHighlightTerms = returnHighlightTerms
			}.Execute().Results;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002211C File Offset: 0x0002111C
		public FindConversationResults FindConversation(ViewBase view, FolderId folderId, string queryString, bool returnHighlightTerms, MailboxSearchLocation? mailboxScope)
		{
			EwsUtilities.ValidateParam(view, "view");
			EwsUtilities.ValidateParamAllowNull(queryString, "queryString");
			EwsUtilities.ValidateParam(returnHighlightTerms, "returnHighlightTerms");
			EwsUtilities.ValidateParam(folderId, "folderId");
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2013, "FindConversation");
			return new FindConversationRequest(this)
			{
				View = view,
				FolderId = new FolderIdWrapper(folderId),
				QueryString = queryString,
				ReturnHighlightTerms = returnHighlightTerms,
				MailboxScope = mailboxScope
			}.Execute().Results;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000221A4 File Offset: 0x000211A4
		internal ServiceResponseCollection<GetConversationItemsResponse> InternalGetConversationItems(IEnumerable<ConversationRequest> conversations, PropertySet propertySet, IEnumerable<FolderId> foldersToIgnore, ConversationSortOrder? sortOrder, MailboxSearchLocation? mailboxScope, int? maxItemsToReturn, ServiceErrorHandling errorHandling)
		{
			EwsUtilities.ValidateParam(conversations, "conversations");
			EwsUtilities.ValidateParam(propertySet, "itemProperties");
			EwsUtilities.ValidateParamAllowNull(foldersToIgnore, "foldersToIgnore");
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2013, "GetConversationItems");
			return new GetConversationItemsRequest(this, errorHandling)
			{
				ItemProperties = propertySet,
				FoldersToIgnore = new FolderIdCollection(foldersToIgnore),
				SortOrder = sortOrder,
				MailboxScope = mailboxScope,
				MaxItemsToReturn = maxItemsToReturn,
				Conversations = Enumerable.ToList<ConversationRequest>(conversations)
			}.Execute();
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00022224 File Offset: 0x00021224
		public ServiceResponseCollection<GetConversationItemsResponse> GetConversationItems(IEnumerable<ConversationRequest> conversations, PropertySet propertySet, IEnumerable<FolderId> foldersToIgnore, ConversationSortOrder? sortOrder)
		{
			return this.InternalGetConversationItems(conversations, propertySet, foldersToIgnore, default(ConversationSortOrder?), default(MailboxSearchLocation?), default(int?), ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00022258 File Offset: 0x00021258
		public ConversationResponse GetConversationItems(ConversationId conversationId, PropertySet propertySet, string syncState, IEnumerable<FolderId> foldersToIgnore, ConversationSortOrder? sortOrder)
		{
			List<ConversationRequest> list = new List<ConversationRequest>();
			list.Add(new ConversationRequest(conversationId, syncState));
			return this.InternalGetConversationItems(list, propertySet, foldersToIgnore, sortOrder, default(MailboxSearchLocation?), default(int?), ServiceErrorHandling.ThrowOnError)[0].Conversation;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x000222A4 File Offset: 0x000212A4
		public ServiceResponseCollection<GetConversationItemsResponse> GetConversationItems(IEnumerable<ConversationRequest> conversations, PropertySet propertySet, IEnumerable<FolderId> foldersToIgnore, ConversationSortOrder? sortOrder, MailboxSearchLocation? mailboxScope)
		{
			return this.InternalGetConversationItems(conversations, propertySet, foldersToIgnore, default(ConversationSortOrder?), mailboxScope, default(int?), ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000222D0 File Offset: 0x000212D0
		private ServiceResponseCollection<ServiceResponse> ApplyConversationAction(ConversationActionType actionType, IEnumerable<ConversationId> conversationIds, bool processRightAway, StringList categories, bool enableAlwaysDelete, FolderId destinationFolderId, ServiceErrorHandling errorHandlingMode)
		{
			EwsUtilities.Assert(actionType == ConversationActionType.AlwaysCategorize || actionType == ConversationActionType.AlwaysMove || actionType == ConversationActionType.AlwaysDelete, "ApplyConversationAction", "Invalic actionType");
			EwsUtilities.ValidateParam(conversationIds, "conversationId");
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010_SP1, "ApplyConversationAction");
			ApplyConversationActionRequest applyConversationActionRequest = new ApplyConversationActionRequest(this, errorHandlingMode);
			ConversationAction conversationAction = new ConversationAction();
			foreach (ConversationId conversationId in conversationIds)
			{
				conversationAction.Action = actionType;
				conversationAction.ConversationId = conversationId;
				conversationAction.ProcessRightAway = processRightAway;
				conversationAction.Categories = categories;
				conversationAction.EnableAlwaysDelete = enableAlwaysDelete;
				conversationAction.DestinationFolderId = ((destinationFolderId != null) ? new FolderIdWrapper(destinationFolderId) : null);
				applyConversationActionRequest.ConversationActions.Add(conversationAction);
			}
			return applyConversationActionRequest.Execute();
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000223A0 File Offset: 0x000213A0
		private ServiceResponseCollection<ServiceResponse> ApplyConversationOneTimeAction(ConversationActionType actionType, IEnumerable<KeyValuePair<ConversationId, DateTime?>> idTimePairs, FolderId contextFolderId, FolderId destinationFolderId, DeleteMode? deleteType, bool? isRead, RetentionType? retentionPolicyType, Guid? retentionPolicyTagId, Flag flag, bool? suppressReadReceipts, ServiceErrorHandling errorHandlingMode)
		{
			EwsUtilities.Assert(actionType == ConversationActionType.Move || actionType == ConversationActionType.Delete || actionType == ConversationActionType.SetReadState || actionType == ConversationActionType.SetRetentionPolicy || actionType == ConversationActionType.Copy || actionType == ConversationActionType.Flag, "ApplyConversationOneTimeAction", "Invalid actionType");
			EwsUtilities.ValidateParamCollection(idTimePairs, "idTimePairs");
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2010_SP1, "ApplyConversationAction");
			ApplyConversationActionRequest applyConversationActionRequest = new ApplyConversationActionRequest(this, errorHandlingMode);
			foreach (KeyValuePair<ConversationId, DateTime?> keyValuePair in idTimePairs)
			{
				ConversationAction conversationAction = new ConversationAction();
				conversationAction.Action = actionType;
				conversationAction.ConversationId = keyValuePair.Key;
				conversationAction.ContextFolderId = ((contextFolderId != null) ? new FolderIdWrapper(contextFolderId) : null);
				conversationAction.DestinationFolderId = ((destinationFolderId != null) ? new FolderIdWrapper(destinationFolderId) : null);
				conversationAction.ConversationLastSyncTime = keyValuePair.Value;
				conversationAction.IsRead = isRead;
				conversationAction.DeleteType = deleteType;
				conversationAction.RetentionPolicyType = retentionPolicyType;
				conversationAction.RetentionPolicyTagId = retentionPolicyTagId;
				conversationAction.Flag = flag;
				conversationAction.SuppressReadReceipts = suppressReadReceipts;
				applyConversationActionRequest.ConversationActions.Add(conversationAction);
			}
			return applyConversationActionRequest.Execute();
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000224C0 File Offset: 0x000214C0
		public ServiceResponseCollection<ServiceResponse> EnableAlwaysCategorizeItemsInConversations(IEnumerable<ConversationId> conversationId, IEnumerable<string> categories, bool processSynchronously)
		{
			EwsUtilities.ValidateParamCollection(categories, "categories");
			return this.ApplyConversationAction(ConversationActionType.AlwaysCategorize, conversationId, processSynchronously, new StringList(categories), false, null, ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000224DF File Offset: 0x000214DF
		public ServiceResponseCollection<ServiceResponse> DisableAlwaysCategorizeItemsInConversations(IEnumerable<ConversationId> conversationId, bool processSynchronously)
		{
			return this.ApplyConversationAction(ConversationActionType.AlwaysCategorize, conversationId, processSynchronously, null, false, null, ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000224EE File Offset: 0x000214EE
		public ServiceResponseCollection<ServiceResponse> EnableAlwaysDeleteItemsInConversations(IEnumerable<ConversationId> conversationId, bool processSynchronously)
		{
			return this.ApplyConversationAction(ConversationActionType.AlwaysDelete, conversationId, processSynchronously, null, true, null, ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000224FD File Offset: 0x000214FD
		public ServiceResponseCollection<ServiceResponse> DisableAlwaysDeleteItemsInConversations(IEnumerable<ConversationId> conversationId, bool processSynchronously)
		{
			return this.ApplyConversationAction(ConversationActionType.AlwaysDelete, conversationId, processSynchronously, null, false, null, ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002250C File Offset: 0x0002150C
		public ServiceResponseCollection<ServiceResponse> EnableAlwaysMoveItemsInConversations(IEnumerable<ConversationId> conversationId, FolderId destinationFolderId, bool processSynchronously)
		{
			EwsUtilities.ValidateParam(destinationFolderId, "destinationFolderId");
			return this.ApplyConversationAction(ConversationActionType.AlwaysMove, conversationId, processSynchronously, null, false, destinationFolderId, ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00022526 File Offset: 0x00021526
		public ServiceResponseCollection<ServiceResponse> DisableAlwaysMoveItemsInConversations(IEnumerable<ConversationId> conversationIds, bool processSynchronously)
		{
			return this.ApplyConversationAction(ConversationActionType.AlwaysMove, conversationIds, processSynchronously, null, false, null, ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00022538 File Offset: 0x00021538
		public ServiceResponseCollection<ServiceResponse> MoveItemsInConversations(IEnumerable<KeyValuePair<ConversationId, DateTime?>> idLastSyncTimePairs, FolderId contextFolderId, FolderId destinationFolderId)
		{
			EwsUtilities.ValidateParam(destinationFolderId, "destinationFolderId");
			return this.ApplyConversationOneTimeAction(ConversationActionType.Move, idLastSyncTimePairs, contextFolderId, destinationFolderId, default(DeleteMode?), default(bool?), default(RetentionType?), default(Guid?), null, default(bool?), ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002258C File Offset: 0x0002158C
		public ServiceResponseCollection<ServiceResponse> CopyItemsInConversations(IEnumerable<KeyValuePair<ConversationId, DateTime?>> idLastSyncTimePairs, FolderId contextFolderId, FolderId destinationFolderId)
		{
			EwsUtilities.ValidateParam(destinationFolderId, "destinationFolderId");
			return this.ApplyConversationOneTimeAction(ConversationActionType.Copy, idLastSyncTimePairs, contextFolderId, destinationFolderId, default(DeleteMode?), default(bool?), default(RetentionType?), default(Guid?), null, default(bool?), ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000225E0 File Offset: 0x000215E0
		public ServiceResponseCollection<ServiceResponse> DeleteItemsInConversations(IEnumerable<KeyValuePair<ConversationId, DateTime?>> idLastSyncTimePairs, FolderId contextFolderId, DeleteMode deleteMode)
		{
			return this.ApplyConversationOneTimeAction(ConversationActionType.Delete, idLastSyncTimePairs, contextFolderId, null, new DeleteMode?(deleteMode), default(bool?), default(RetentionType?), default(Guid?), null, default(bool?), ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00022624 File Offset: 0x00021624
		public ServiceResponseCollection<ServiceResponse> SetReadStateForItemsInConversations(IEnumerable<KeyValuePair<ConversationId, DateTime?>> idLastSyncTimePairs, FolderId contextFolderId, bool isRead)
		{
			return this.ApplyConversationOneTimeAction(ConversationActionType.SetReadState, idLastSyncTimePairs, contextFolderId, null, default(DeleteMode?), new bool?(isRead), default(RetentionType?), default(Guid?), null, default(bool?), ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00022668 File Offset: 0x00021668
		public ServiceResponseCollection<ServiceResponse> SetReadStateForItemsInConversations(IEnumerable<KeyValuePair<ConversationId, DateTime?>> idLastSyncTimePairs, FolderId contextFolderId, bool isRead, bool suppressReadReceipts)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2013, "SetReadStateForItemsInConversations");
			return this.ApplyConversationOneTimeAction(ConversationActionType.SetReadState, idLastSyncTimePairs, contextFolderId, null, default(DeleteMode?), new bool?(isRead), default(RetentionType?), default(Guid?), null, new bool?(suppressReadReceipts), ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000226B8 File Offset: 0x000216B8
		public ServiceResponseCollection<ServiceResponse> SetRetentionPolicyForItemsInConversations(IEnumerable<KeyValuePair<ConversationId, DateTime?>> idLastSyncTimePairs, FolderId contextFolderId, RetentionType retentionPolicyType, Guid? retentionPolicyTagId)
		{
			return this.ApplyConversationOneTimeAction(ConversationActionType.SetRetentionPolicy, idLastSyncTimePairs, contextFolderId, null, default(DeleteMode?), default(bool?), new RetentionType?(retentionPolicyType), retentionPolicyTagId, null, default(bool?), ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000226F4 File Offset: 0x000216F4
		public ServiceResponseCollection<ServiceResponse> SetFlagStatusForItemsInConversations(IEnumerable<KeyValuePair<ConversationId, DateTime?>> idLastSyncTimePairs, FolderId contextFolderId, Flag flagStatus)
		{
			EwsUtilities.ValidateMethodVersion(this, ExchangeVersion.Exchange2013, "SetFlagStatusForItemsInConversations");
			return this.ApplyConversationOneTimeAction(ConversationActionType.Flag, idLastSyncTimePairs, contextFolderId, null, default(DeleteMode?), default(bool?), default(RetentionType?), default(Guid?), flagStatus, default(bool?), ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00022748 File Offset: 0x00021748
		private ServiceResponseCollection<ConvertIdResponse> InternalConvertIds(IEnumerable<AlternateIdBase> ids, IdFormat destinationFormat, ServiceErrorHandling errorHandling)
		{
			EwsUtilities.ValidateParamCollection(ids, "ids");
			ConvertIdRequest convertIdRequest = new ConvertIdRequest(this, errorHandling);
			convertIdRequest.Ids.AddRange(ids);
			convertIdRequest.DestinationFormat = destinationFormat;
			return convertIdRequest.Execute();
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00022781 File Offset: 0x00021781
		public ServiceResponseCollection<ConvertIdResponse> ConvertIds(IEnumerable<AlternateIdBase> ids, IdFormat destinationFormat)
		{
			EwsUtilities.ValidateParamCollection(ids, "ids");
			return this.InternalConvertIds(ids, destinationFormat, ServiceErrorHandling.ReturnErrors);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00022798 File Offset: 0x00021798
		public AlternateIdBase ConvertId(AlternateIdBase id, IdFormat destinationFormat)
		{
			EwsUtilities.ValidateParam(id, "id");
			ServiceResponseCollection<ConvertIdResponse> serviceResponseCollection = this.InternalConvertIds(new AlternateIdBase[] { id }, destinationFormat, ServiceErrorHandling.ThrowOnError);
			return serviceResponseCollection[0].ConvertedId;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x000227D1 File Offset: 0x000217D1
		public Collection<DelegateUserResponse> AddDelegates(Mailbox mailbox, MeetingRequestsDeliveryScope? meetingRequestsDeliveryScope, params DelegateUser[] delegateUsers)
		{
			return this.AddDelegates(mailbox, meetingRequestsDeliveryScope, (IEnumerable<DelegateUser>)delegateUsers);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x000227E4 File Offset: 0x000217E4
		public Collection<DelegateUserResponse> AddDelegates(Mailbox mailbox, MeetingRequestsDeliveryScope? meetingRequestsDeliveryScope, IEnumerable<DelegateUser> delegateUsers)
		{
			EwsUtilities.ValidateParam(mailbox, "mailbox");
			EwsUtilities.ValidateParamCollection(delegateUsers, "delegateUsers");
			AddDelegateRequest addDelegateRequest = new AddDelegateRequest(this);
			addDelegateRequest.Mailbox = mailbox;
			addDelegateRequest.DelegateUsers.AddRange(delegateUsers);
			addDelegateRequest.MeetingRequestsDeliveryScope = meetingRequestsDeliveryScope;
			DelegateManagementResponse delegateManagementResponse = addDelegateRequest.Execute();
			return delegateManagementResponse.DelegateUserResponses;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00022835 File Offset: 0x00021835
		public Collection<DelegateUserResponse> UpdateDelegates(Mailbox mailbox, MeetingRequestsDeliveryScope? meetingRequestsDeliveryScope, params DelegateUser[] delegateUsers)
		{
			return this.UpdateDelegates(mailbox, meetingRequestsDeliveryScope, (IEnumerable<DelegateUser>)delegateUsers);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00022848 File Offset: 0x00021848
		public Collection<DelegateUserResponse> UpdateDelegates(Mailbox mailbox, MeetingRequestsDeliveryScope? meetingRequestsDeliveryScope, IEnumerable<DelegateUser> delegateUsers)
		{
			EwsUtilities.ValidateParam(mailbox, "mailbox");
			EwsUtilities.ValidateParamCollection(delegateUsers, "delegateUsers");
			UpdateDelegateRequest updateDelegateRequest = new UpdateDelegateRequest(this);
			updateDelegateRequest.Mailbox = mailbox;
			updateDelegateRequest.DelegateUsers.AddRange(delegateUsers);
			updateDelegateRequest.MeetingRequestsDeliveryScope = meetingRequestsDeliveryScope;
			DelegateManagementResponse delegateManagementResponse = updateDelegateRequest.Execute();
			return delegateManagementResponse.DelegateUserResponses;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00022899 File Offset: 0x00021899
		public Collection<DelegateUserResponse> RemoveDelegates(Mailbox mailbox, params UserId[] userIds)
		{
			return this.RemoveDelegates(mailbox, (IEnumerable<UserId>)userIds);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000228A8 File Offset: 0x000218A8
		public Collection<DelegateUserResponse> RemoveDelegates(Mailbox mailbox, IEnumerable<UserId> userIds)
		{
			EwsUtilities.ValidateParam(mailbox, "mailbox");
			EwsUtilities.ValidateParamCollection(userIds, "userIds");
			RemoveDelegateRequest removeDelegateRequest = new RemoveDelegateRequest(this);
			removeDelegateRequest.Mailbox = mailbox;
			removeDelegateRequest.UserIds.AddRange(userIds);
			DelegateManagementResponse delegateManagementResponse = removeDelegateRequest.Execute();
			return delegateManagementResponse.DelegateUserResponses;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000228F2 File Offset: 0x000218F2
		public DelegateInformation GetDelegates(Mailbox mailbox, bool includePermissions, params UserId[] userIds)
		{
			return this.GetDelegates(mailbox, includePermissions, (IEnumerable<UserId>)userIds);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00022904 File Offset: 0x00021904
		public DelegateInformation GetDelegates(Mailbox mailbox, bool includePermissions, IEnumerable<UserId> userIds)
		{
			EwsUtilities.ValidateParam(mailbox, "mailbox");
			GetDelegateRequest getDelegateRequest = new GetDelegateRequest(this);
			getDelegateRequest.Mailbox = mailbox;
			getDelegateRequest.UserIds.AddRange(userIds);
			getDelegateRequest.IncludePermissions = includePermissions;
			GetDelegateResponse getDelegateResponse = getDelegateRequest.Execute();
			return new DelegateInformation(getDelegateResponse.DelegateUserResponses, getDelegateResponse.MeetingRequestsDeliveryScope);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00022958 File Offset: 0x00021958
		internal void CreateUserConfiguration(UserConfiguration userConfiguration)
		{
			EwsUtilities.ValidateParam(userConfiguration, "userConfiguration");
			new CreateUserConfigurationRequest(this)
			{
				UserConfiguration = userConfiguration
			}.Execute();
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00022988 File Offset: 0x00021988
		internal void DeleteUserConfiguration(string name, FolderId parentFolderId)
		{
			EwsUtilities.ValidateParam(name, "name");
			EwsUtilities.ValidateParam(parentFolderId, "parentFolderId");
			new DeleteUserConfigurationRequest(this)
			{
				Name = name,
				ParentFolderId = parentFolderId
			}.Execute();
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x000229C8 File Offset: 0x000219C8
		internal UserConfiguration GetUserConfiguration(string name, FolderId parentFolderId, UserConfigurationProperties properties)
		{
			EwsUtilities.ValidateParam(name, "name");
			EwsUtilities.ValidateParam(parentFolderId, "parentFolderId");
			return new GetUserConfigurationRequest(this)
			{
				Name = name,
				ParentFolderId = parentFolderId,
				Properties = properties
			}.Execute()[0].UserConfiguration;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00022A18 File Offset: 0x00021A18
		internal void LoadPropertiesForUserConfiguration(UserConfiguration userConfiguration, UserConfigurationProperties properties)
		{
			EwsUtilities.Assert(userConfiguration != null, "ExchangeService.LoadPropertiesForUserConfiguration", "userConfiguration is null");
			new GetUserConfigurationRequest(this)
			{
				UserConfiguration = userConfiguration,
				Properties = properties
			}.Execute();
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00022A58 File Offset: 0x00021A58
		internal void UpdateUserConfiguration(UserConfiguration userConfiguration)
		{
			EwsUtilities.ValidateParam(userConfiguration, "userConfiguration");
			new UpdateUserConfigurationRequest(this)
			{
				UserConfiguration = userConfiguration
			}.Execute();
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00022A88 File Offset: 0x00021A88
		public RuleCollection GetInboxRules()
		{
			GetInboxRulesRequest getInboxRulesRequest = new GetInboxRulesRequest(this);
			return getInboxRulesRequest.Execute().Rules;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00022AA8 File Offset: 0x00021AA8
		public RuleCollection GetInboxRules(string mailboxSmtpAddress)
		{
			EwsUtilities.ValidateParam(mailboxSmtpAddress, "MailboxSmtpAddress");
			return new GetInboxRulesRequest(this)
			{
				MailboxSmtpAddress = mailboxSmtpAddress
			}.Execute().Rules;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00022ADC File Offset: 0x00021ADC
		public void UpdateInboxRules(IEnumerable<RuleOperation> operations, bool removeOutlookRuleBlob)
		{
			new UpdateInboxRulesRequest(this)
			{
				InboxRuleOperations = operations,
				RemoveOutlookRuleBlob = removeOutlookRuleBlob
			}.Execute();
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00022B08 File Offset: 0x00021B08
		public void UpdateInboxRules(IEnumerable<RuleOperation> operations, bool removeOutlookRuleBlob, string mailboxSmtpAddress)
		{
			new UpdateInboxRulesRequest(this)
			{
				InboxRuleOperations = operations,
				RemoveOutlookRuleBlob = removeOutlookRuleBlob,
				MailboxSmtpAddress = mailboxSmtpAddress
			}.Execute();
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00022B38 File Offset: 0x00021B38
		public GetDiscoverySearchConfigurationResponse GetDiscoverySearchConfiguration(string searchId, bool expandGroupMembership, bool inPlaceHoldConfigurationOnly)
		{
			return new GetDiscoverySearchConfigurationRequest(this)
			{
				SearchId = searchId,
				ExpandGroupMembership = expandGroupMembership,
				InPlaceHoldConfigurationOnly = inPlaceHoldConfigurationOnly
			}.Execute();
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00022B68 File Offset: 0x00021B68
		public GetSearchableMailboxesResponse GetSearchableMailboxes(string searchFilter, bool expandGroupMembership)
		{
			return new GetSearchableMailboxesRequest(this)
			{
				SearchFilter = searchFilter,
				ExpandGroupMembership = expandGroupMembership
			}.Execute();
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00022B90 File Offset: 0x00021B90
		public ServiceResponseCollection<SearchMailboxesResponse> SearchMailboxes(IEnumerable<MailboxQuery> mailboxQueries, SearchResultType resultType)
		{
			SearchMailboxesRequest searchMailboxesRequest = new SearchMailboxesRequest(this, ServiceErrorHandling.ReturnErrors);
			if (mailboxQueries != null)
			{
				searchMailboxesRequest.SearchQueries.AddRange(mailboxQueries);
			}
			searchMailboxesRequest.ResultType = resultType;
			return searchMailboxesRequest.Execute();
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00022BC4 File Offset: 0x00021BC4
		public ServiceResponseCollection<SearchMailboxesResponse> SearchMailboxes(IEnumerable<MailboxQuery> mailboxQueries, SearchResultType resultType, string sortByProperty, SortDirection sortOrder, int pageSize, SearchPageDirection pageDirection, string pageItemReference)
		{
			SearchMailboxesRequest searchMailboxesRequest = new SearchMailboxesRequest(this, ServiceErrorHandling.ReturnErrors);
			if (mailboxQueries != null)
			{
				searchMailboxesRequest.SearchQueries.AddRange(mailboxQueries);
			}
			searchMailboxesRequest.ResultType = resultType;
			searchMailboxesRequest.SortByProperty = sortByProperty;
			searchMailboxesRequest.SortOrder = sortOrder;
			searchMailboxesRequest.PageSize = pageSize;
			searchMailboxesRequest.PageDirection = pageDirection;
			searchMailboxesRequest.PageItemReference = pageItemReference;
			return searchMailboxesRequest.Execute();
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00022C1C File Offset: 0x00021C1C
		public ServiceResponseCollection<SearchMailboxesResponse> SearchMailboxes(SearchMailboxesParameters searchParameters)
		{
			EwsUtilities.ValidateParam(searchParameters, "searchParameters");
			EwsUtilities.ValidateParam(searchParameters.SearchQueries, "searchParameters.SearchQueries");
			SearchMailboxesRequest searchMailboxesRequest = this.CreateSearchMailboxesRequest(searchParameters);
			return searchMailboxesRequest.Execute();
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00022C54 File Offset: 0x00021C54
		public IAsyncResult BeginSearchMailboxes(AsyncCallback callback, object state, SearchMailboxesParameters searchParameters)
		{
			EwsUtilities.ValidateParam(searchParameters, "searchParameters");
			EwsUtilities.ValidateParam(searchParameters.SearchQueries, "searchParameters.SearchQueries");
			SearchMailboxesRequest searchMailboxesRequest = this.CreateSearchMailboxesRequest(searchParameters);
			return searchMailboxesRequest.BeginExecute(callback, state);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00022C8C File Offset: 0x00021C8C
		public ServiceResponseCollection<SearchMailboxesResponse> EndSearchMailboxes(IAsyncResult asyncResult)
		{
			SearchMailboxesRequest searchMailboxesRequest = AsyncRequestResult.ExtractServiceRequest<SearchMailboxesRequest>(this, asyncResult);
			return searchMailboxesRequest.EndExecute(asyncResult);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00022CA8 File Offset: 0x00021CA8
		public SetHoldOnMailboxesResponse SetHoldOnMailboxes(string holdId, HoldAction actionType, string query, string[] mailboxes)
		{
			return new SetHoldOnMailboxesRequest(this)
			{
				HoldId = holdId,
				ActionType = actionType,
				Query = query,
				Mailboxes = mailboxes,
				InPlaceHoldIdentity = null
			}.Execute();
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00022CE6 File Offset: 0x00021CE6
		public SetHoldOnMailboxesResponse SetHoldOnMailboxes(string holdId, HoldAction actionType, string query, string inPlaceHoldIdentity)
		{
			return this.SetHoldOnMailboxes(holdId, actionType, query, inPlaceHoldIdentity, null);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00022CF4 File Offset: 0x00021CF4
		public SetHoldOnMailboxesResponse SetHoldOnMailboxes(string holdId, HoldAction actionType, string query, string inPlaceHoldIdentity, string itemHoldPeriod)
		{
			return new SetHoldOnMailboxesRequest(this)
			{
				HoldId = holdId,
				ActionType = actionType,
				Query = query,
				Mailboxes = null,
				InPlaceHoldIdentity = inPlaceHoldIdentity,
				ItemHoldPeriod = itemHoldPeriod
			}.Execute();
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00022D3C File Offset: 0x00021D3C
		public SetHoldOnMailboxesResponse SetHoldOnMailboxes(SetHoldOnMailboxesParameters parameters)
		{
			EwsUtilities.ValidateParam(parameters, "parameters");
			return new SetHoldOnMailboxesRequest(this)
			{
				HoldId = parameters.HoldId,
				ActionType = parameters.ActionType,
				Query = parameters.Query,
				Mailboxes = parameters.Mailboxes,
				Language = parameters.Language,
				InPlaceHoldIdentity = parameters.InPlaceHoldIdentity
			}.Execute();
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00022DAC File Offset: 0x00021DAC
		public GetHoldOnMailboxesResponse GetHoldOnMailboxes(string holdId)
		{
			return new GetHoldOnMailboxesRequest(this)
			{
				HoldId = holdId
			}.Execute();
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00022DD0 File Offset: 0x00021DD0
		public GetNonIndexableItemDetailsResponse GetNonIndexableItemDetails(string[] mailboxes)
		{
			return this.GetNonIndexableItemDetails(mailboxes, default(int?), null, default(SearchPageDirection?));
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00022DF8 File Offset: 0x00021DF8
		public GetNonIndexableItemDetailsResponse GetNonIndexableItemDetails(string[] mailboxes, int? pageSize, string pageItemReference, SearchPageDirection? pageDirection)
		{
			GetNonIndexableItemDetailsParameters getNonIndexableItemDetailsParameters = new GetNonIndexableItemDetailsParameters
			{
				Mailboxes = mailboxes,
				PageSize = pageSize,
				PageItemReference = pageItemReference,
				PageDirection = pageDirection,
				SearchArchiveOnly = false
			};
			return this.GetNonIndexableItemDetails(getNonIndexableItemDetailsParameters);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00022E38 File Offset: 0x00021E38
		public GetNonIndexableItemDetailsResponse GetNonIndexableItemDetails(GetNonIndexableItemDetailsParameters parameters)
		{
			GetNonIndexableItemDetailsRequest getNonIndexableItemDetailsRequest = this.CreateGetNonIndexableItemDetailsRequest(parameters);
			return getNonIndexableItemDetailsRequest.Execute();
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00022E54 File Offset: 0x00021E54
		public IAsyncResult BeginGetNonIndexableItemDetails(AsyncCallback callback, object state, GetNonIndexableItemDetailsParameters parameters)
		{
			GetNonIndexableItemDetailsRequest getNonIndexableItemDetailsRequest = this.CreateGetNonIndexableItemDetailsRequest(parameters);
			return getNonIndexableItemDetailsRequest.BeginExecute(callback, state);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00022E74 File Offset: 0x00021E74
		public GetNonIndexableItemDetailsResponse EndGetNonIndexableItemDetails(IAsyncResult asyncResult)
		{
			GetNonIndexableItemDetailsRequest getNonIndexableItemDetailsRequest = AsyncRequestResult.ExtractServiceRequest<GetNonIndexableItemDetailsRequest>(this, asyncResult);
			return (GetNonIndexableItemDetailsResponse)getNonIndexableItemDetailsRequest.EndInternalExecute(asyncResult);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00022E98 File Offset: 0x00021E98
		public GetNonIndexableItemStatisticsResponse GetNonIndexableItemStatistics(string[] mailboxes)
		{
			GetNonIndexableItemStatisticsParameters getNonIndexableItemStatisticsParameters = new GetNonIndexableItemStatisticsParameters
			{
				Mailboxes = mailboxes,
				SearchArchiveOnly = false
			};
			return this.GetNonIndexableItemStatistics(getNonIndexableItemStatisticsParameters);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00022EC4 File Offset: 0x00021EC4
		public GetNonIndexableItemStatisticsResponse GetNonIndexableItemStatistics(GetNonIndexableItemStatisticsParameters parameters)
		{
			GetNonIndexableItemStatisticsRequest getNonIndexableItemStatisticsRequest = this.CreateGetNonIndexableItemStatisticsRequest(parameters);
			return getNonIndexableItemStatisticsRequest.Execute();
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00022EE0 File Offset: 0x00021EE0
		public IAsyncResult BeginGetNonIndexableItemStatistics(AsyncCallback callback, object state, GetNonIndexableItemStatisticsParameters parameters)
		{
			GetNonIndexableItemStatisticsRequest getNonIndexableItemStatisticsRequest = this.CreateGetNonIndexableItemStatisticsRequest(parameters);
			return getNonIndexableItemStatisticsRequest.BeginExecute(callback, state);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00022F00 File Offset: 0x00021F00
		public GetNonIndexableItemStatisticsResponse EndGetNonIndexableItemStatistics(IAsyncResult asyncResult)
		{
			GetNonIndexableItemStatisticsRequest getNonIndexableItemStatisticsRequest = AsyncRequestResult.ExtractServiceRequest<GetNonIndexableItemStatisticsRequest>(this, asyncResult);
			return (GetNonIndexableItemStatisticsResponse)getNonIndexableItemStatisticsRequest.EndInternalExecute(asyncResult);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00022F24 File Offset: 0x00021F24
		private GetNonIndexableItemDetailsRequest CreateGetNonIndexableItemDetailsRequest(GetNonIndexableItemDetailsParameters parameters)
		{
			EwsUtilities.ValidateParam(parameters, "parameters");
			EwsUtilities.ValidateParam(parameters.Mailboxes, "parameters.Mailboxes");
			return new GetNonIndexableItemDetailsRequest(this)
			{
				Mailboxes = parameters.Mailboxes,
				PageSize = parameters.PageSize,
				PageItemReference = parameters.PageItemReference,
				PageDirection = parameters.PageDirection,
				SearchArchiveOnly = parameters.SearchArchiveOnly
			};
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00022F90 File Offset: 0x00021F90
		private GetNonIndexableItemStatisticsRequest CreateGetNonIndexableItemStatisticsRequest(GetNonIndexableItemStatisticsParameters parameters)
		{
			EwsUtilities.ValidateParam(parameters, "parameters");
			EwsUtilities.ValidateParam(parameters.Mailboxes, "parameters.Mailboxes");
			return new GetNonIndexableItemStatisticsRequest(this)
			{
				Mailboxes = parameters.Mailboxes,
				SearchArchiveOnly = parameters.SearchArchiveOnly
			};
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00022FD8 File Offset: 0x00021FD8
		private SearchMailboxesRequest CreateSearchMailboxesRequest(SearchMailboxesParameters searchParameters)
		{
			SearchMailboxesRequest searchMailboxesRequest = new SearchMailboxesRequest(this, ServiceErrorHandling.ReturnErrors);
			searchMailboxesRequest.SearchQueries.AddRange(searchParameters.SearchQueries);
			searchMailboxesRequest.ResultType = searchParameters.ResultType;
			searchMailboxesRequest.PreviewItemResponseShape = searchParameters.PreviewItemResponseShape;
			searchMailboxesRequest.SortByProperty = searchParameters.SortBy;
			searchMailboxesRequest.SortOrder = searchParameters.SortOrder;
			searchMailboxesRequest.Language = searchParameters.Language;
			searchMailboxesRequest.PerformDeduplication = searchParameters.PerformDeduplication;
			searchMailboxesRequest.PageSize = searchParameters.PageSize;
			searchMailboxesRequest.PageDirection = searchParameters.PageDirection;
			searchMailboxesRequest.PageItemReference = searchParameters.PageItemReference;
			return searchMailboxesRequest;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002306C File Offset: 0x0002206C
		public GetUserRetentionPolicyTagsResponse GetUserRetentionPolicyTags()
		{
			GetUserRetentionPolicyTagsRequest getUserRetentionPolicyTagsRequest = new GetUserRetentionPolicyTagsRequest(this);
			return getUserRetentionPolicyTagsRequest.Execute();
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00023086 File Offset: 0x00022086
		private bool DefaultAutodiscoverRedirectionUrlValidationCallback(string redirectionUrl)
		{
			throw new AutodiscoverLocalException(string.Format(Strings.AutodiscoverRedirectBlocked, redirectionUrl));
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0002309D File Offset: 0x0002209D
		public void AutodiscoverUrl(string emailAddress)
		{
			this.AutodiscoverUrl(emailAddress, new AutodiscoverRedirectionUrlValidationCallback(this.DefaultAutodiscoverRedirectionUrlValidationCallback));
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000230B4 File Offset: 0x000220B4
		public void AutodiscoverUrl(string emailAddress, AutodiscoverRedirectionUrlValidationCallback validateRedirectionUrlCallback)
		{
			Uri uri;
			if (base.RequestedServerVersion > ExchangeVersion.Exchange2007_SP1)
			{
				try
				{
					uri = this.GetAutodiscoverUrl(emailAddress, base.RequestedServerVersion, validateRedirectionUrlCallback);
					this.Url = this.AdjustServiceUriFromCredentials(uri);
					return;
				}
				catch (AutodiscoverLocalException ex)
				{
					base.TraceMessage(TraceFlags.AutodiscoverResponse, string.Format("Autodiscover service call failed with error '{0}'. Will try legacy service", ex.Message));
				}
				catch (ServiceRemoteException ex2)
				{
					if (ex2 is AccountIsLockedException)
					{
						throw;
					}
					base.TraceMessage(TraceFlags.AutodiscoverResponse, string.Format("Autodiscover service call failed with error '{0}'. Will try legacy service", ex2.Message));
				}
			}
			uri = this.GetAutodiscoverUrl(emailAddress, ExchangeVersion.Exchange2007_SP1, validateRedirectionUrlCallback);
			this.Url = this.AdjustServiceUriFromCredentials(uri);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00023160 File Offset: 0x00022160
		private Uri AdjustServiceUriFromCredentials(Uri uri)
		{
			if (base.Credentials == null)
			{
				return uri;
			}
			return base.Credentials.AdjustUrl(uri);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00023178 File Offset: 0x00022178
		private Uri GetAutodiscoverUrl(string emailAddress, ExchangeVersion requestedServerVersion, AutodiscoverRedirectionUrlValidationCallback validateRedirectionUrlCallback)
		{
			AutodiscoverService autodiscoverService = new AutodiscoverService(this, requestedServerVersion)
			{
				RedirectionUrlValidationCallback = validateRedirectionUrlCallback,
				EnableScpLookup = this.EnableScpLookup
			};
			GetUserSettingsResponse userSettings = autodiscoverService.GetUserSettings(emailAddress, new UserSettingName[]
			{
				UserSettingName.InternalEwsUrl,
				UserSettingName.ExternalEwsUrl
			});
			switch (userSettings.ErrorCode)
			{
			case AutodiscoverErrorCode.NoError:
				return this.GetEwsUrlFromResponse(userSettings, autodiscoverService.IsExternal.GetValueOrDefault(true));
			case AutodiscoverErrorCode.InvalidUser:
				throw new ServiceRemoteException(string.Format(Strings.InvalidUser, emailAddress));
			case AutodiscoverErrorCode.InvalidRequest:
				throw new ServiceRemoteException(string.Format(Strings.InvalidAutodiscoverRequest, userSettings.ErrorMessage));
			}
			base.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("No EWS Url returned for user {0}, error code is {1}", emailAddress, userSettings.ErrorCode));
			throw new ServiceRemoteException(userSettings.ErrorMessage);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00023254 File Offset: 0x00022254
		private Uri GetEwsUrlFromResponse(GetUserSettingsResponse response, bool isExternal)
		{
			string text;
			if (isExternal && response.TryGetSettingValue<string>(UserSettingName.ExternalEwsUrl, out text) && !string.IsNullOrEmpty(text))
			{
				return new Uri(text);
			}
			if ((response.TryGetSettingValue<string>(UserSettingName.InternalEwsUrl, out text) || response.TryGetSettingValue<string>(UserSettingName.ExternalEwsUrl, out text)) && !string.IsNullOrEmpty(text))
			{
				return new Uri(text);
			}
			throw new AutodiscoverLocalException(Strings.AutodiscoverDidNotReturnEwsUrl);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000232B8 File Offset: 0x000222B8
		public ServiceResponseCollection<GetClientAccessTokenResponse> GetClientAccessToken(IEnumerable<KeyValuePair<string, ClientAccessTokenType>> idAndTypes)
		{
			new GetClientAccessTokenRequest(this, ServiceErrorHandling.ReturnErrors);
			List<ClientAccessTokenRequest> list = new List<ClientAccessTokenRequest>();
			foreach (KeyValuePair<string, ClientAccessTokenType> keyValuePair in idAndTypes)
			{
				ClientAccessTokenRequest clientAccessTokenRequest = new ClientAccessTokenRequest(keyValuePair.Key, keyValuePair.Value);
				list.Add(clientAccessTokenRequest);
			}
			return this.GetClientAccessToken(list.ToArray());
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00023330 File Offset: 0x00022330
		public ServiceResponseCollection<GetClientAccessTokenResponse> GetClientAccessToken(ClientAccessTokenRequest[] tokenRequests)
		{
			return new GetClientAccessTokenRequest(this, ServiceErrorHandling.ReturnErrors)
			{
				TokenRequests = tokenRequests
			}.Execute();
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00023354 File Offset: 0x00022354
		public Collection<XmlDocument> GetAppManifests()
		{
			GetAppManifestsRequest getAppManifestsRequest = new GetAppManifestsRequest(this);
			return getAppManifestsRequest.Execute().Manifests;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00023374 File Offset: 0x00022374
		public Collection<ClientApp> GetAppManifests(string apiVersionSupported, string schemaVersionSupported)
		{
			return new GetAppManifestsRequest(this)
			{
				ApiVersionSupported = apiVersionSupported,
				SchemaVersionSupported = schemaVersionSupported
			}.Execute().Apps;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000233A4 File Offset: 0x000223A4
		public void InstallApp(Stream manifestStream)
		{
			EwsUtilities.ValidateParam(manifestStream, "manifestStream");
			InstallAppRequest installAppRequest = new InstallAppRequest(this, manifestStream);
			installAppRequest.Execute();
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000233CC File Offset: 0x000223CC
		public void UninstallApp(string id)
		{
			EwsUtilities.ValidateParam(id, "id");
			UninstallAppRequest uninstallAppRequest = new UninstallAppRequest(this, id);
			uninstallAppRequest.Execute();
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000233F4 File Offset: 0x000223F4
		public void DisableApp(string id, DisableReasonType disableReason)
		{
			EwsUtilities.ValidateParam(id, "id");
			EwsUtilities.ValidateParam(disableReason, "disableReason");
			DisableAppRequest disableAppRequest = new DisableAppRequest(this, id, disableReason);
			disableAppRequest.Execute();
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002342C File Offset: 0x0002242C
		public string GetAppMarketplaceUrl()
		{
			return this.GetAppMarketplaceUrl(null, null);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00023438 File Offset: 0x00022438
		public string GetAppMarketplaceUrl(string apiVersionSupported, string schemaVersionSupported)
		{
			return new GetAppMarketplaceUrlRequest(this)
			{
				ApiVersionSupported = apiVersionSupported,
				SchemaVersionSupported = schemaVersionSupported
			}.Execute().AppMarketplaceUrl;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00023468 File Offset: 0x00022468
		public GetClientExtensionResponse GetClientExtension(StringList requestedExtensionIds, bool shouldReturnEnabledOnly, bool isUserScope, string userId, StringList userEnabledExtensionIds, StringList userDisabledExtensionIds, bool isDebug)
		{
			GetClientExtensionRequest getClientExtensionRequest = new GetClientExtensionRequest(this, requestedExtensionIds, shouldReturnEnabledOnly, isUserScope, userId, userEnabledExtensionIds, userDisabledExtensionIds, isDebug);
			return getClientExtensionRequest.Execute();
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00023490 File Offset: 0x00022490
		public GetEncryptionConfigurationResponse GetEncryptionConfiguration()
		{
			GetEncryptionConfigurationRequest getEncryptionConfigurationRequest = new GetEncryptionConfigurationRequest(this);
			return getEncryptionConfigurationRequest.Execute();
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000234AC File Offset: 0x000224AC
		public void SetEncryptionConfiguration(string imageBase64, string emailText, string portalText, string disclaimerText)
		{
			SetEncryptionConfigurationRequest setEncryptionConfigurationRequest = new SetEncryptionConfigurationRequest(this, imageBase64, emailText, portalText, disclaimerText);
			setEncryptionConfigurationRequest.Execute();
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000234CC File Offset: 0x000224CC
		public void SetClientExtension(List<SetClientExtensionAction> actions)
		{
			SetClientExtensionRequest setClientExtensionRequest = new SetClientExtensionRequest(this, actions);
			setClientExtensionRequest.Execute();
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000234E8 File Offset: 0x000224E8
		internal XmlDocument ExecuteDiagnosticMethod(string verb, XmlNode parameter)
		{
			return new ExecuteDiagnosticMethodRequest(this)
			{
				Verb = verb,
				Parameter = parameter
			}.Execute()[0].ReturnValue;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002351C File Offset: 0x0002251C
		internal override void Validate()
		{
			base.Validate();
			if (this.Url == null)
			{
				throw new ServiceLocalException(Strings.ServiceUrlMustBeSet);
			}
			if (this.PrivilegedUserId != null && this.ImpersonatedUserId != null)
			{
				throw new ServiceLocalException(Strings.CannotSetBothImpersonatedAndPrivilegedUser);
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00023570 File Offset: 0x00022570
		internal static void ValidateTargetVersion(string version)
		{
			if (string.IsNullOrEmpty(version))
			{
				throw new ArgumentException("Target version must not be empty.");
			}
			string[] array = version.Trim().Split(new char[] { ';' });
			switch (array.Length)
			{
			case 1:
				break;
			case 2:
			{
				string text = array[1].Trim();
				string[] array2 = text.Split(new char[] { '=' });
				if (array2.Length != 2 || !array2[0].Trim().Equals("minimum", 5) || !ExchangeService.IsMajorMinor(array2[1].Trim()))
				{
					throw new ArgumentException("Target version must match X.Y or Exchange20XX.");
				}
				break;
			}
			default:
				throw new ArgumentException("Target version should have the form.");
			}
			string text2 = array[0].Trim();
			if (array[0].StartsWith("Exchange20"))
			{
				return;
			}
			if (ExchangeService.IsMajorMinor(text2))
			{
				return;
			}
			throw new ArgumentException("Target version must match X.Y or Exchange20XX.");
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00023654 File Offset: 0x00022654
		private static bool IsMajorMinor(string versionPart)
		{
			string[] array = versionPart.Split(new char[] { '.' });
			if (array.Length != 2)
			{
				return false;
			}
			foreach (string text in array)
			{
				string text2 = text;
				for (int j = 0; j < text2.Length; j++)
				{
					char c = text2.get_Chars(j);
					if (!char.IsDigit(c))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000236CE File Offset: 0x000226CE
		public ExchangeService()
		{
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000236E4 File Offset: 0x000226E4
		public ExchangeService(TimeZoneInfo timeZone)
			: base(timeZone)
		{
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000236FB File Offset: 0x000226FB
		public ExchangeService(ExchangeVersion requestedServerVersion)
			: base(requestedServerVersion)
		{
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00023712 File Offset: 0x00022712
		public ExchangeService(ExchangeVersion requestedServerVersion, TimeZoneInfo timeZone)
			: base(requestedServerVersion, timeZone)
		{
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002372A File Offset: 0x0002272A
		internal ExchangeService(string targetServerVersion)
			: base(ExchangeVersion.Exchange2013)
		{
			ExchangeService.ValidateTargetVersion(targetServerVersion);
			this.TargetServerVersion = targetServerVersion;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002374E File Offset: 0x0002274E
		internal ExchangeService(string targetServerVersion, TimeZoneInfo timeZone)
			: base(ExchangeVersion.Exchange2013, timeZone)
		{
			ExchangeService.ValidateTargetVersion(targetServerVersion);
			this.TargetServerVersion = targetServerVersion;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00023774 File Offset: 0x00022774
		internal IEwsHttpWebRequest PrepareHttpWebRequest(string methodName)
		{
			Uri uri = this.Url;
			if (this.RenderingMethod == ExchangeService.RenderingMode.JSON)
			{
				uri = new Uri(uri, string.Format("{0}/{1}{2}", uri.AbsolutePath, methodName, uri.Query));
			}
			else
			{
				uri = this.AdjustServiceUriFromCredentials(uri);
			}
			IEwsHttpWebRequest ewsHttpWebRequest = base.PrepareHttpWebRequestForUrl(uri, base.AcceptGzipEncoding, true);
			if (!string.IsNullOrEmpty(this.TargetServerVersion))
			{
				ewsHttpWebRequest.Headers.Set("X-EWS-TargetVersion", this.TargetServerVersion);
			}
			return ewsHttpWebRequest;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000237EC File Offset: 0x000227EC
		internal override void SetContentType(IEwsHttpWebRequest request)
		{
			if (this.renderingMode == ExchangeService.RenderingMode.Xml)
			{
				request.ContentType = "text/xml; charset=utf-8";
				request.Accept = "text/xml";
				return;
			}
			if (this.renderingMode == ExchangeService.RenderingMode.JSON)
			{
				request.ContentType = "application/json; charset=utf-8";
				request.Accept = "application/json";
				return;
			}
			base.SetContentType(request);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002383F File Offset: 0x0002283F
		internal override void ProcessHttpErrorResponse(IEwsHttpWebResponse httpWebResponse, WebException webException)
		{
			base.InternalProcessHttpErrorResponse(httpWebResponse, webException, TraceFlags.EwsResponseHttpHeaders, TraceFlags.EwsResponse);
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0002384D File Offset: 0x0002284D
		// (set) Token: 0x06000ABA RID: 2746 RVA: 0x00023855 File Offset: 0x00022855
		public Uri Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0002385E File Offset: 0x0002285E
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x00023866 File Offset: 0x00022866
		public ImpersonatedUserId ImpersonatedUserId
		{
			get
			{
				return this.impersonatedUserId;
			}
			set
			{
				this.impersonatedUserId = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0002386F File Offset: 0x0002286F
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x00023877 File Offset: 0x00022877
		internal PrivilegedUserId PrivilegedUserId
		{
			get
			{
				return this.privilegedUserId;
			}
			set
			{
				this.privilegedUserId = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00023880 File Offset: 0x00022880
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x00023888 File Offset: 0x00022888
		public ManagementRoles ManagementRoles
		{
			get
			{
				return this.managementRoles;
			}
			set
			{
				this.managementRoles = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00023891 File Offset: 0x00022891
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x00023899 File Offset: 0x00022899
		public CultureInfo PreferredCulture
		{
			get
			{
				return this.preferredCulture;
			}
			set
			{
				this.preferredCulture = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x000238A2 File Offset: 0x000228A2
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x000238AA File Offset: 0x000228AA
		public DateTimePrecision DateTimePrecision
		{
			get
			{
				return this.dateTimePrecision;
			}
			set
			{
				this.dateTimePrecision = value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x000238B3 File Offset: 0x000228B3
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x000238BB File Offset: 0x000228BB
		public IFileAttachmentContentHandler FileAttachmentContentHandler
		{
			get
			{
				return this.fileAttachmentContentHandler;
			}
			set
			{
				this.fileAttachmentContentHandler = value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x000238C4 File Offset: 0x000228C4
		public new TimeZoneInfo TimeZone
		{
			get
			{
				return base.TimeZone;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x000238CC File Offset: 0x000228CC
		public UnifiedMessaging UnifiedMessaging
		{
			get
			{
				if (this.unifiedMessaging == null)
				{
					this.unifiedMessaging = new UnifiedMessaging(this);
				}
				return this.unifiedMessaging;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x000238E8 File Offset: 0x000228E8
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x000238F0 File Offset: 0x000228F0
		public bool EnableScpLookup
		{
			get
			{
				return this.enableScpLookup;
			}
			set
			{
				this.enableScpLookup = value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x000238F9 File Offset: 0x000228F9
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x00023901 File Offset: 0x00022901
		internal bool Exchange2007CompatibilityMode
		{
			get
			{
				return this.exchange2007CompatibilityMode;
			}
			set
			{
				this.exchange2007CompatibilityMode = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0002390A File Offset: 0x0002290A
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x00023912 File Offset: 0x00022912
		internal ExchangeService.RenderingMode RenderingMethod
		{
			get
			{
				return this.renderingMode;
			}
			set
			{
				this.renderingMode = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0002391B File Offset: 0x0002291B
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x00023923 File Offset: 0x00022923
		public bool TraceEnablePrettyPrinting
		{
			get
			{
				return this.traceEnablePrettyPrinting;
			}
			set
			{
				this.traceEnablePrettyPrinting = value;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0002392C File Offset: 0x0002292C
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x00023934 File Offset: 0x00022934
		internal string TargetServerVersion
		{
			get
			{
				return this.targetServerVersion;
			}
			set
			{
				ExchangeService.ValidateTargetVersion(value);
				this.targetServerVersion = value;
			}
		}

		// Token: 0x040002FE RID: 766
		private const string TargetServerVersionHeaderName = "X-EWS-TargetVersion";

		// Token: 0x040002FF RID: 767
		private Uri url;

		// Token: 0x04000300 RID: 768
		private CultureInfo preferredCulture;

		// Token: 0x04000301 RID: 769
		private DateTimePrecision dateTimePrecision;

		// Token: 0x04000302 RID: 770
		private ImpersonatedUserId impersonatedUserId;

		// Token: 0x04000303 RID: 771
		private PrivilegedUserId privilegedUserId;

		// Token: 0x04000304 RID: 772
		private ManagementRoles managementRoles;

		// Token: 0x04000305 RID: 773
		private IFileAttachmentContentHandler fileAttachmentContentHandler;

		// Token: 0x04000306 RID: 774
		private UnifiedMessaging unifiedMessaging;

		// Token: 0x04000307 RID: 775
		private bool enableScpLookup = true;

		// Token: 0x04000308 RID: 776
		private ExchangeService.RenderingMode renderingMode;

		// Token: 0x04000309 RID: 777
		private bool traceEnablePrettyPrinting = true;

		// Token: 0x0400030A RID: 778
		private string targetServerVersion;

		// Token: 0x0400030B RID: 779
		private bool exchange2007CompatibilityMode;

		// Token: 0x020000D5 RID: 213
		public enum RenderingMode
		{
			// Token: 0x04000311 RID: 785
			Xml,
			// Token: 0x04000312 RID: 786
			JSON
		}
	}
}
