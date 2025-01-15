using System;
using System.Diagnostics;
using Microsoft.Exchange.WebServices.Autodiscover;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000C06 RID: 3078
	internal class ExchangeTracingService
	{
		// Token: 0x060053BE RID: 21438 RVA: 0x0011C3A0 File Offset: 0x0011A5A0
		public ExchangeTracingService(IResource resource, IResourceCredential credential, IEngineHost host, string mailbox, Uri serverAddress, ExchangeVersion version)
		{
			this.resource = resource;
			this.host = host;
			this.exchangeServiceCore = new ExchangeServiceCore(resource, credential, mailbox, serverAddress, version, host);
			ExchangeTracingService.EnableTracing(this.exchangeServiceCore.Service, host, SourceLevels.Verbose, this.resource);
		}

		// Token: 0x170019B1 RID: 6577
		// (get) Token: 0x060053BF RID: 21439 RVA: 0x0011C3EE File Offset: 0x0011A5EE
		public ExchangeServiceCore InnerServiceCore
		{
			get
			{
				return this.exchangeServiceCore;
			}
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x0011C3F8 File Offset: 0x0011A5F8
		public FindFoldersResults FindFolders(FolderId folderId, FolderView folderView, SearchFilter searchFilter = null)
		{
			FindFoldersResults findFoldersResults2;
			using (IHostTrace hostTrace = this.CreateTrace("FindFolders"))
			{
				ExchangeTracingService.TracePropertySet(hostTrace, folderView.PropertySet);
				if (searchFilter != null)
				{
					hostTrace.Add("SearchFilter", searchFilter.GetSearchFilterString(), true);
				}
				hostTrace.Add("PageSize", folderView.PageSize, false);
				hostTrace.Add("Offset", folderView.Offset, false);
				try
				{
					FindFoldersResults findFoldersResults = this.exchangeServiceCore.FindFolders(folderId, folderView, searchFilter);
					hostTrace.Add("ResponseMoreAvailable", findFoldersResults.MoreAvailable, false);
					hostTrace.Add("ResponseNextPageOffset", findFoldersResults.NextPageOffset, false);
					hostTrace.Add("ResponseTotalCount", findFoldersResults.TotalCount, false);
					findFoldersResults2 = findFoldersResults;
				}
				catch (Exception ex)
				{
					hostTrace.Add(ex, true);
					throw;
				}
			}
			return findFoldersResults2;
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x0011C4EC File Offset: 0x0011A6EC
		public ServiceResponseCollection<ServiceResponse> LoadPropertiesForItems(PropertySet allPropertySet, FindItemsResults<Item> items)
		{
			ServiceResponseCollection<ServiceResponse> serviceResponseCollection;
			using (IHostTrace hostTrace = this.CreateTrace("LoadPropertiesForItems"))
			{
				ExchangeTracingService.TracePropertySet(hostTrace, allPropertySet);
				try
				{
					serviceResponseCollection = this.exchangeServiceCore.LoadPropertiesForItems(allPropertySet, items);
				}
				catch (Exception ex)
				{
					hostTrace.Add(ex, true);
					throw;
				}
			}
			return serviceResponseCollection;
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x0011C550 File Offset: 0x0011A750
		public FindItemsResults<Item> FindItems(FolderId folderId, SearchFilter searchFilter, ItemView itemView)
		{
			FindItemsResults<Item> findItemsResults2;
			using (IHostTrace hostTrace = this.CreateTrace("FindItems"))
			{
				ExchangeTracingService.TracePropertySet(hostTrace, itemView.PropertySet);
				hostTrace.Add("SearchFilter", searchFilter.GetSearchFilterString(), true);
				hostTrace.Add("PageSize", itemView.PageSize, false);
				hostTrace.Add("Offset", itemView.Offset, false);
				try
				{
					FindItemsResults<Item> findItemsResults = this.exchangeServiceCore.FindItems(folderId, searchFilter, itemView);
					hostTrace.Add("ResponseMoreAvailable", findItemsResults.MoreAvailable, false);
					hostTrace.Add("ResponseNextPageOffset", findItemsResults.NextPageOffset, false);
					hostTrace.Add("ResponseTotalCount", findItemsResults.TotalCount, false);
					findItemsResults2 = findItemsResults;
				}
				catch (Exception ex)
				{
					hostTrace.Add(ex, true);
					throw;
				}
			}
			return findItemsResults2;
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x0011C640 File Offset: 0x0011A840
		public Item GetItem(ItemId itemId, PropertySet itemPropertySet)
		{
			Item item;
			using (IHostTrace hostTrace = this.CreateTrace("GetItem"))
			{
				ExchangeTracingService.TracePropertySet(hostTrace, itemPropertySet);
				try
				{
					item = this.exchangeServiceCore.GetItem(itemId, itemPropertySet);
				}
				catch (Exception ex)
				{
					hostTrace.Add(ex, true);
					throw;
				}
			}
			return item;
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x0011C6A4 File Offset: 0x0011A8A4
		private static void TracePropertySet(IHostTrace trace, PropertySet propertySet)
		{
			foreach (PropertyDefinitionBase propertyDefinitionBase in propertySet)
			{
				trace.Add("Property", propertyDefinitionBase.GetName(), false);
			}
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x0011C6F8 File Offset: 0x0011A8F8
		private IHostTrace CreateTrace(string methodName)
		{
			return TracingService.CreateTrace(this.host, "Engine/IO/Exchange/" + methodName, TraceEventType.Information, this.resource);
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x0011C717 File Offset: 0x0011A917
		public static void EnableTracing(ExchangeServiceBase exchangeService, IEngineHost host, SourceLevels requiredTraceLevel, IResource resource)
		{
			if ((TracingService.GetService(host).Levels & requiredTraceLevel) == requiredTraceLevel)
			{
				exchangeService.TraceEnabled = true;
				exchangeService.TraceFlags = (TraceFlags)9223372036854775003L;
				exchangeService.TraceListener = new ExchangeTracingService.ExchangeTraceListener(host, exchangeService is AutodiscoverService, resource);
			}
		}

		// Token: 0x04002E64 RID: 11876
		private readonly IResource resource;

		// Token: 0x04002E65 RID: 11877
		private readonly IEngineHost host;

		// Token: 0x04002E66 RID: 11878
		private readonly ExchangeServiceCore exchangeServiceCore;

		// Token: 0x02000C07 RID: 3079
		public class ExchangeTraceListener : ITraceListener
		{
			// Token: 0x060053C7 RID: 21447 RVA: 0x0011C755 File Offset: 0x0011A955
			public ExchangeTraceListener(IEngineHost host, bool isAutoDiscover, IResource resource)
			{
				this.host = host;
				this.entryName = (isAutoDiscover ? "Engine/IO/Exchange/ExchangeAutoDiscoverServiceTrace" : "Engine/IO/Exchange/ExchangeServiceTrace");
				this.resource = resource;
			}

			// Token: 0x060053C8 RID: 21448 RVA: 0x0011C780 File Offset: 0x0011A980
			public void Trace(string traceType, string traceMessage)
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, this.entryName, TraceEventType.Information, this.resource))
				{
					hostTrace.Add("TraceType", traceType, false);
					hostTrace.Add("TraceMessage", traceMessage, true);
				}
			}

			// Token: 0x04002E67 RID: 11879
			private readonly IEngineHost host;

			// Token: 0x04002E68 RID: 11880
			private readonly string entryName;

			// Token: 0x04002E69 RID: 11881
			private readonly IResource resource;
		}
	}
}
