using System;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Http;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BF4 RID: 3060
	internal class ExchangeServiceCore
	{
		// Token: 0x0600534D RID: 21325 RVA: 0x0011A40F File Offset: 0x0011860F
		public ExchangeServiceCore(IResource resource, IResourceCredential credential, string mailbox, Uri serverAddress, ExchangeVersion version, IEngineHost host)
		{
			this.resource = resource;
			this.credential = credential;
			this.mailbox = mailbox;
			this.serverAddress = serverAddress;
			this.version = version;
			this.host = host;
		}

		// Token: 0x1700199E RID: 6558
		// (get) Token: 0x0600534E RID: 21326 RVA: 0x0011A444 File Offset: 0x00118644
		public ExchangeService Service
		{
			get
			{
				if (this.service == null)
				{
					this.service = new ExchangeService(this.version);
					ExchangeHelper.InitializeExchangeService(this.credential, this.service);
					this.service.Url = this.serverAddress;
					this.service.PreAuthenticate = true;
					this.service.HttpHeaders.Add("X-AnchorMailbox", this.mailbox);
				}
				return this.service;
			}
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x0011A4BC File Offset: 0x001186BC
		public FindFoldersResults FindFolders(FolderId folderId, FolderView folderView, SearchFilter searchFilter = null)
		{
			Tracer tracer = new Tracer(this.host, "Engine/IO/Exchange/FindFolders/", this.resource, null, null);
			return this.ExecuteRefreshRetry<FindFoldersResults>(delegate
			{
				if (searchFilter != null)
				{
					return this.Service.FindFolders(folderId, searchFilter, folderView);
				}
				return this.Service.FindFolders(folderId, folderView);
			}, tracer);
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x0011A518 File Offset: 0x00118718
		public ServiceResponseCollection<ServiceResponse> LoadPropertiesForItems(PropertySet allPropertySet, FindItemsResults<Item> items)
		{
			Tracer tracer = new Tracer(this.host, "Engine/IO/Exchange/LoadPropertiesForItems/", this.resource, null, null);
			return this.ExecuteRefreshRetry<ServiceResponseCollection<ServiceResponse>>(() => this.Service.LoadPropertiesForItems(items, allPropertySet), tracer);
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x0011A56C File Offset: 0x0011876C
		public FindItemsResults<Item> FindItems(FolderId folderId, SearchFilter searchFilter, ItemView itemView)
		{
			Tracer tracer = new Tracer(this.host, "Engine/IO/Exchange/FindItems/", this.resource, null, null);
			return this.ExecuteRefreshRetry<FindItemsResults<Item>>(() => this.Service.FindItems(folderId, searchFilter, itemView), tracer);
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x0011A5C8 File Offset: 0x001187C8
		public Item GetItem(ItemId itemId, PropertySet itemPropertySet)
		{
			Tracer tracer = new Tracer(this.host, "Engine/IO/Exchange/GetItem/", this.resource, null, null);
			return this.ExecuteRefreshRetry<Item>(() => Item.Bind(this.Service, itemId, itemPropertySet), tracer);
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x0011A61C File Offset: 0x0011881C
		public TResult ExecuteRefreshRetry<TResult>(Func<TResult> func, Tracer tracer)
		{
			OAuthCredential oauthCredential = this.credential as OAuthCredential;
			if (oauthCredential != null)
			{
				this.credential = oauthCredential.RefreshTokenAsNeeded(this.host, this.resource, false);
				ExchangeHelper.InitializeExchangeService(this.credential, this.service);
			}
			return ExchangeServiceCore.retryPolicy.Execute<TResult>(this.host, this.resource, func, tracer);
		}

		// Token: 0x06005354 RID: 21332 RVA: 0x0011A67C File Offset: 0x0011887C
		private static RetryHandlerResult RetryHandler(Exception exception)
		{
			ServerBusyException ex = exception as ServerBusyException;
			if (ex != null)
			{
				return RetryHandlerResult.RetryAfterDelay(TimeSpan.FromMilliseconds((double)ex.BackOffMilliseconds));
			}
			return RetryHandlerResult.FailWithOriginalException;
		}

		// Token: 0x04002E01 RID: 11777
		private const string AnchorMailboxHeader = "X-AnchorMailbox";

		// Token: 0x04002E02 RID: 11778
		private static readonly RetryPolicy retryPolicy = new RetryPolicy(3, new Func<Exception, RetryHandlerResult>(ExchangeServiceCore.RetryHandler));

		// Token: 0x04002E03 RID: 11779
		private readonly IResource resource;

		// Token: 0x04002E04 RID: 11780
		private readonly ExchangeVersion version;

		// Token: 0x04002E05 RID: 11781
		private readonly string mailbox;

		// Token: 0x04002E06 RID: 11782
		private readonly Uri serverAddress;

		// Token: 0x04002E07 RID: 11783
		private readonly IEngineHost host;

		// Token: 0x04002E08 RID: 11784
		private IResourceCredential credential;

		// Token: 0x04002E09 RID: 11785
		private ExchangeService service;
	}
}
