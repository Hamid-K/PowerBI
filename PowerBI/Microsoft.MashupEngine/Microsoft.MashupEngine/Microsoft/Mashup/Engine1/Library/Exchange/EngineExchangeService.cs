using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BC6 RID: 3014
	internal class EngineExchangeService : IExchangeService
	{
		// Token: 0x06005233 RID: 21043 RVA: 0x001158EA File Offset: 0x00113AEA
		public EngineExchangeService(IResource resource, IResourceCredential credential, IEngineHost host, string mailbox, Uri serverAddress, ExchangeVersion version)
		{
			this.host = host;
			this.resource = resource;
			this.service = new ExchangeTracingService(resource, credential, host, mailbox, serverAddress, version);
		}

		// Token: 0x17001963 RID: 6499
		// (get) Token: 0x06005234 RID: 21044 RVA: 0x00115914 File Offset: 0x00113B14
		public ExchangeService InnerService
		{
			get
			{
				return this.service.InnerServiceCore.Service;
			}
		}

		// Token: 0x17001964 RID: 6500
		// (get) Token: 0x06005235 RID: 21045 RVA: 0x00115926 File Offset: 0x00113B26
		public IEngineHost EngineHost
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x00115930 File Offset: 0x00113B30
		private void ThrowExchangeExceptions(Exception e)
		{
			if (e is ServiceRequestException)
			{
				throw ExchangeExceptions.NewExchangeServiceRequestException(this.host, (ServiceRequestException)e, this.resource);
			}
			if (e is ServiceResponseException)
			{
				throw ExchangeExceptions.NewExchangeServiceResponseException(this.host, (ServiceResponseException)e, this.resource);
			}
			if (e is ServiceVersionException)
			{
				throw ExchangeExceptions.NewExchangeVersionNotSupportedException(this.host, (ServiceVersionException)e, this.resource);
			}
			if (e is ServiceXmlDeserializationException || e is ServiceJsonDeserializationException || e is XmlException)
			{
				throw ExchangeExceptions.NewExchangeDeserializationException(this.host, e, this.resource);
			}
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x001159C8 File Offset: 0x00113BC8
		private IHostProgress GetHostProgress()
		{
			return ExchangeHelper.GetHostProgress(this.host, this.resource.Path);
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x001159E0 File Offset: 0x00113BE0
		public int FindItemCount(FolderId folderId, SearchFilter searchFilter)
		{
			int totalCount;
			try
			{
				using (new ProgressRequest(this.GetHostProgress()))
				{
					ItemView itemView = new ItemView(1);
					EngineExchangeService.FillItemView(EmptyArray<PropertyDefinitionBase>.Instance, EmptyArray<SortOption>.Instance, itemView, new PropertySet());
					FindItemsResults<Item> findItemsResults;
					this.TryFindItems(folderId, searchFilter, itemView, EmptyArray<PropertyDefinitionBase>.Instance, out findItemsResults);
					totalCount = findItemsResults.TotalCount;
				}
			}
			catch (Exception ex)
			{
				this.ThrowExchangeExceptions(ex);
				throw;
			}
			return totalCount;
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x00115A64 File Offset: 0x00113C64
		public IEnumerable<ExchangeSearchResult> FindItems(FolderId folderId, ItemView itemView, SearchFilter searchFilter, string folderPath, ExchangeColumnInfo[] columnInfos, HashSet<PropertyDefinitionBase> additionalProperties, out bool moreAvailable, out int? nextPageOffset)
		{
			IEnumerable<ExchangeSearchResult> enumerable;
			try
			{
				IHostProgress hostProgress = this.GetHostProgress();
				using (new ProgressRequest(hostProgress))
				{
					PropertyDefinitionBase[] propertiesToFetch = columnInfos.GetPropertiesToFetch(additionalProperties);
					SortOption[] topLevelSortCollection = columnInfos.GetTopLevelSortCollection();
					PropertySet propertySet = EngineExchangeService.GetPropertySet(propertiesToFetch);
					EngineExchangeService.FillItemView(propertiesToFetch, topLevelSortCollection, itemView, propertySet);
					FindItemsResults<Item> findItemsResults;
					if (this.TryFindItems(folderId, searchFilter, itemView, propertiesToFetch, out findItemsResults))
					{
						moreAvailable = findItemsResults.MoreAvailable;
						nextPageOffset = findItemsResults.NextPageOffset;
						enumerable = this.GetItemSearchResult(hostProgress, findItemsResults, null, folderPath, propertiesToFetch, columnInfos);
					}
					else
					{
						PropertySet propertySet2 = new PropertySet(BasePropertySet.IdOnly);
						itemView.PropertySet = propertySet2;
						findItemsResults = this.service.FindItems(folderId, searchFilter, itemView);
						moreAvailable = findItemsResults.MoreAvailable;
						nextPageOffset = findItemsResults.NextPageOffset;
						if (findItemsResults.Items.Count == 0)
						{
							enumerable = Enumerable.Empty<ExchangeSearchResult>();
						}
						else
						{
							propertySet.RequestedBodyType = new BodyType?(BodyType.Text);
							ServiceResponseCollection<ServiceResponse> serviceResponseCollection = this.service.LoadPropertiesForItems(propertySet, findItemsResults);
							enumerable = this.GetItemSearchResult(hostProgress, findItemsResults, serviceResponseCollection, folderPath, propertiesToFetch, columnInfos);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ThrowExchangeExceptions(ex);
				throw;
			}
			return enumerable;
		}

		// Token: 0x0600523A RID: 21050 RVA: 0x00115B94 File Offset: 0x00113D94
		public ExchangeSearchResult GetItem(string itemIdString, string folderPath, PropertyDefinitionBase[] properties, params ExchangeColumnInfo[] columnInfos)
		{
			ItemId itemId = new ItemId(itemIdString);
			PropertySet propertySet = EngineExchangeService.GetPropertySet(properties);
			propertySet.RequestedBodyType = new BodyType?(BodyType.Text);
			ExchangeSearchResult exchangeSearchResult;
			try
			{
				IHostProgress hostProgress = this.GetHostProgress();
				using (new ProgressRequest(hostProgress))
				{
					ServiceObject item = this.service.GetItem(itemId, propertySet);
					hostProgress.RecordRowRead();
					exchangeSearchResult = new ExchangeServiceSearchResult(item, itemIdString, folderPath);
				}
			}
			catch (Exception ex)
			{
				this.ThrowExchangeExceptions(ex);
				throw;
			}
			return exchangeSearchResult;
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x00115C1C File Offset: 0x00113E1C
		public IEnumerable<ExchangeSearchResult> FindFolders(FolderId folderId, FolderView folderView, SearchFilter searchFilter, out bool moreAvailable, out int? nextPageOffset)
		{
			IEnumerable<ExchangeSearchResult> folderSearchResult;
			try
			{
				PropertySet propertySet = new PropertySet(BasePropertySet.IdOnly)
				{
					FolderSchema.DisplayName,
					ExchangeHelper.PR_Folder_Path
				};
				folderView.PropertySet = propertySet;
				FindFoldersResults findFoldersResults;
				if (searchFilter == null)
				{
					findFoldersResults = this.service.FindFolders(folderId, folderView, null);
				}
				else
				{
					findFoldersResults = this.service.FindFolders(folderId, folderView, searchFilter);
				}
				moreAvailable = findFoldersResults.MoreAvailable;
				nextPageOffset = findFoldersResults.NextPageOffset;
				folderSearchResult = this.GetFolderSearchResult(findFoldersResults);
			}
			catch (Exception ex)
			{
				this.ThrowExchangeExceptions(ex);
				throw;
			}
			return folderSearchResult;
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x00115CAC File Offset: 0x00113EAC
		private IEnumerable<ExchangeSearchResult> GetFolderSearchResult(FindFoldersResults folders)
		{
			foreach (Folder folder in folders.Folders)
			{
				yield return new ExchangeServiceSearchResult(folder, folder.Id.UniqueId, ExchangeHelper.GetFolderPath(folder));
			}
			IEnumerator<Folder> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x00115CBC File Offset: 0x00113EBC
		private IEnumerable<ExchangeSearchResult> GetItemSearchResult(IHostProgress hostProgress, FindItemsResults<Item> items, ServiceResponseCollection<ServiceResponse> serviceResponses, string folderPath, PropertyDefinitionBase[] properties, ExchangeColumnInfo[] columnInfos)
		{
			Item[] itemsArray = items.ToArray<Item>();
			int num;
			for (int i = 0; i < itemsArray.Length; i = num + 1)
			{
				Item item = itemsArray[i];
				string text = null;
				if (serviceResponses != null)
				{
					text = serviceResponses[i].ErrorMessage;
				}
				if (text == null)
				{
					hostProgress.RecordRowRead();
					yield return new ExchangeServiceSearchResult(item, item.Id.UniqueId, folderPath);
				}
				else
				{
					hostProgress.RecordRowRead();
					yield return this.GetItem(item.Id.UniqueId, folderPath, properties, columnInfos);
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x00115CFC File Offset: 0x00113EFC
		private static void FillItemView(PropertyDefinitionBase[] properties, SortOption[] sortOptions, ItemView itemView, PropertySet propertySet)
		{
			foreach (SortOption sortOption in sortOptions)
			{
				itemView.OrderBy.Add(sortOption.PropertyDefinition, sortOption.SortDirection);
			}
			itemView.PropertySet = propertySet;
		}

		// Token: 0x0600523F RID: 21055 RVA: 0x00115D3C File Offset: 0x00113F3C
		private static PropertySet GetPropertySet(PropertyDefinitionBase[] properties)
		{
			PropertySet propertySet = new PropertySet(BasePropertySet.IdOnly);
			foreach (PropertyDefinitionBase propertyDefinitionBase in properties)
			{
				propertySet.Add(propertyDefinitionBase);
				IndexedPropertyDefinition[] array;
				if (SupportedExchangeTypes.TryGetAddionalProperties(propertyDefinitionBase, out array))
				{
					propertySet.AddRange(array);
				}
			}
			return propertySet;
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x00115D80 File Offset: 0x00113F80
		private bool TryFindItems(FolderId folderId, SearchFilter searchFilter, ItemView itemView, PropertyDefinitionBase[] properties, out FindItemsResults<Item> results)
		{
			if (!ExchangeFoldingAllowedLists.DefaultDelayedPropertiesAllowedList.Overlaps(properties))
			{
				try
				{
					results = this.service.FindItems(folderId, searchFilter, itemView);
					return true;
				}
				catch (ServiceValidationException)
				{
					results = null;
					return false;
				}
			}
			results = null;
			return false;
		}

		// Token: 0x04002D35 RID: 11573
		private readonly IEngineHost host;

		// Token: 0x04002D36 RID: 11574
		private readonly IResource resource;

		// Token: 0x04002D37 RID: 11575
		private readonly ExchangeTracingService service;
	}
}
