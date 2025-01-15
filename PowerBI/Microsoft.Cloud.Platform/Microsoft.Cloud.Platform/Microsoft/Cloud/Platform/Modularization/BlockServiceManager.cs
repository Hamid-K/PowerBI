using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000B6 RID: 182
	internal class BlockServiceManager : IIdentifiable
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x000135FD File Offset: 0x000117FD
		internal BlockServiceManager([NotNull] string name)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(name, "name");
			this.m_name = name;
			this.Reset();
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00013620 File Offset: 0x00011820
		[CanBeNull]
		public BlockServiceTicket TryGetService(RequestedBlockService request)
		{
			object locker = this.m_locker;
			lock (locker)
			{
				PublishedBlockService publishedBlockService = null;
				foreach (PublishedBlockService publishedBlockService2 in this.m_publishedServices)
				{
					if (publishedBlockService2.Matches(request))
					{
						publishedBlockService = publishedBlockService2;
						break;
					}
					if (publishedBlockService2.PartiallyMatches(request) && publishedBlockService == null)
					{
						publishedBlockService = publishedBlockService2;
					}
				}
				if (publishedBlockService != null)
				{
					return this.CreateServiceProducerConsumerAssociation(publishedBlockService, request);
				}
			}
			this.AddToUnsatisfiedRequests(request);
			return null;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000136D4 File Offset: 0x000118D4
		public BlockServiceTicket TryGetService(string name, IBlock serviceConsumer, Type serviceType, BlockServiceProviderIdentity serviceIdentity, object context)
		{
			return this.TryGetService(new RequestedBlockService(name, serviceConsumer, serviceType, serviceIdentity, context));
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x000136E8 File Offset: 0x000118E8
		public BlockServiceTicket TryGetService(IBlock serviceConsumer, Type serviceType, BlockServiceProviderIdentity serviceIdentity, object context)
		{
			return this.TryGetService(new RequestedBlockService(null, serviceConsumer, serviceType, serviceIdentity, context));
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x000136FB File Offset: 0x000118FB
		public bool PublishService(object service, Type serviceType, BlockServiceProviderIdentity serviceIdentity, IBlock serviceProvider)
		{
			return this.PublishService(null, service, serviceType, serviceIdentity, serviceProvider);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001370C File Offset: 0x0001190C
		public bool PublishService(string serviceIdentity, object service, Type serviceType, BlockServiceProviderIdentity serviceLevel, IBlock serviceProvider)
		{
			BlockServiceManager.ValidatePublishedService(service, serviceType);
			PublishedBlockService publishedBlockService = new PublishedBlockService(serviceIdentity, service, serviceType, serviceLevel, serviceProvider);
			object locker = this.m_locker;
			lock (locker)
			{
				if (!this.ServiceExists(serviceIdentity, serviceType, serviceLevel))
				{
					this.m_publishedServices.Add(publishedBlockService);
					return true;
				}
			}
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Warning, "The service was not published since A service of type '{0}' and of provider identity '{1}' has already been published", new object[] { serviceType, serviceLevel });
			return false;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x000137A0 File Offset: 0x000119A0
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000137A8 File Offset: 0x000119A8
		[CanBeNull]
		internal List<RequestedBlockService> UnsatisfiedServiceRequests(string blockName)
		{
			if (!this.m_unsatisfiedServiceRequests.ContainsKey(blockName))
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Warning, "Block {0} did not request any unpublished services", new object[] { blockName });
				return null;
			}
			List<RequestedBlockService> list = new List<RequestedBlockService>();
			using (List<RequestedBlockService>.Enumerator enumerator = this.m_unsatisfiedServiceRequests[blockName].GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RequestedBlockService request = enumerator.Current;
					if (!this.m_publishedServices.Any((PublishedBlockService ps) => ps.Matches(request)) && !this.m_publishedServices.Any((PublishedBlockService ps) => ps.PartiallyMatches(request)))
					{
						list.Add(request);
					}
				}
			}
			return list;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00009B3B File Offset: 0x00007D3B
		private static void ValidatePublishedService(object service, Type publishedService)
		{
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00013874 File Offset: 0x00011A74
		private bool ServiceExists(string name, Type serviceType, BlockServiceProviderIdentity serviceIdentity)
		{
			return this.m_publishedServices.FirstOrDefault((PublishedBlockService publishedService) => ((publishedService.m_name != null) ? publishedService.m_name.Equals(name, StringComparison.OrdinalIgnoreCase) : (name == null)) && publishedService.m_serviceType == serviceType && publishedService.m_serviceIdentity == serviceIdentity) != null;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000138B8 File Offset: 0x00011AB8
		private BlockServiceTicket CreateServiceProducerConsumerAssociation(PublishedBlockService publishedService, RequestedBlockService request)
		{
			return new BlockServiceTicket(this.m_associationManager.CreateWorkTicket(request.ServiceConsumer), publishedService, this, request.Context);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000138D8 File Offset: 0x00011AD8
		private void AddToUnsatisfiedRequests(RequestedBlockService request)
		{
			string text = ((request.ServiceConsumer == null) ? "unidentified consumer" : request.ServiceConsumer.Name);
			if (!this.m_unsatisfiedServiceRequests.ContainsKey(text))
			{
				this.m_unsatisfiedServiceRequests.Add(text, new List<RequestedBlockService>());
			}
			using (List<RequestedBlockService>.Enumerator enumerator = this.m_unsatisfiedServiceRequests[text].GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Matches(request))
					{
						return;
					}
				}
			}
			this.m_unsatisfiedServiceRequests[text].Add(request);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00009B3B File Offset: 0x00007D3B
		internal void OnBlockServiceTicketDisposed(BlockServiceTicket serviceTicket)
		{
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00013980 File Offset: 0x00011B80
		internal bool DidBlockPublishService(IBlock block)
		{
			return this.m_publishedServices.Where((PublishedBlockService ps) => ps.m_serviceProvider.Equals(block)).Any<PublishedBlockService>();
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000139B6 File Offset: 0x00011BB6
		private void Reset()
		{
			this.m_locker = new object();
			this.m_publishedServices = new List<PublishedBlockService>();
			this.m_unsatisfiedServiceRequests = new Dictionary<string, List<RequestedBlockService>>();
			this.m_associationManager = new WorkTicketManager(this.m_name + ".AssociationManager");
		}

		// Token: 0x040001D4 RID: 468
		private string m_name;

		// Token: 0x040001D5 RID: 469
		private object m_locker;

		// Token: 0x040001D6 RID: 470
		private List<PublishedBlockService> m_publishedServices;

		// Token: 0x040001D7 RID: 471
		private WorkTicketManager m_associationManager;

		// Token: 0x040001D8 RID: 472
		private Dictionary<string, List<RequestedBlockService>> m_unsatisfiedServiceRequests;
	}
}
