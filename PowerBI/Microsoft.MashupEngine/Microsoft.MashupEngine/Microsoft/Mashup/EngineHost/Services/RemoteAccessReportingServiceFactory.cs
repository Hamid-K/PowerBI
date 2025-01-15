using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A46 RID: 6726
	internal class RemoteAccessReportingServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AA0C RID: 43532 RVA: 0x00232311 File Offset: 0x00230511
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return new RemoteAccessReportingServiceFactory.Stub(engineHost.QueryService<IReportCultureAccess>(), engineHost.QueryService<IReportResourceAccess>(), engineHost.QueryService<IReportPartitionResources>(), engineHost.QueryService<IReportStaleness>(), engineHost.QueryService<IReportSampling>(), messenger);
		}

		// Token: 0x0600AA0D RID: 43533 RVA: 0x00232337 File Offset: 0x00230537
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemoteAccessReportingServiceFactory.Proxy(messenger);
		}

		// Token: 0x02001A47 RID: 6727
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IReportCultureAccess, IReportResourceAccess, IReportPartitionResources, IReportStaleness, IReportSampling
		{
			// Token: 0x0600AA0F RID: 43535 RVA: 0x0023233F File Offset: 0x0023053F
			public Proxy(IMessenger messenger)
			{
				this.messenger = messenger;
			}

			// Token: 0x0600AA10 RID: 43536 RVA: 0x00232350 File Offset: 0x00230550
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IReportCultureAccess) || typeof(T) == typeof(IReportResourceAccess) || typeof(T) == typeof(IReportPartitionResources) || typeof(T) == typeof(IReportStaleness) || typeof(T) == typeof(IReportSampling))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600AA11 RID: 43537 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0600AA12 RID: 43538 RVA: 0x002323F4 File Offset: 0x002305F4
			public void CultureAccessed()
			{
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteAccessReportingServiceFactory.CultureAccessedMessage());
				}
			}

			// Token: 0x0600AA13 RID: 43539 RVA: 0x00232434 File Offset: 0x00230634
			public void ResourceAccessed(IResource resource)
			{
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteAccessReportingServiceFactory.ResourceAccessedMessage
					{
						Resource = resource
					});
				}
			}

			// Token: 0x0600AA14 RID: 43540 RVA: 0x0023247C File Offset: 0x0023067C
			public void PartitionResources(IPartitionKey partitionKey, IEnumerable<IResource> resources)
			{
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteAccessReportingServiceFactory.PartitionResourcesMessage
					{
						PartitionKey = partitionKey,
						Resources = resources
					});
				}
			}

			// Token: 0x0600AA15 RID: 43541 RVA: 0x002324CC File Offset: 0x002306CC
			public void StaleSince(DateTime staleSince)
			{
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteAccessReportingServiceFactory.StaleSinceMessage
					{
						StaleSince = staleSince
					});
				}
			}

			// Token: 0x0600AA16 RID: 43542 RVA: 0x00232514 File Offset: 0x00230714
			public void SamplingUsed()
			{
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteAccessReportingServiceFactory.SamplingUsedMessage());
				}
			}

			// Token: 0x0400585E RID: 22622
			private readonly IMessenger messenger;
		}

		// Token: 0x02001A48 RID: 6728
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600AA17 RID: 43543 RVA: 0x00232554 File Offset: 0x00230754
			public Stub(IReportCultureAccess reportCultureAccess, IReportResourceAccess reportResourceAccess, IReportPartitionResources reportPartitionResources, IReportStaleness reportStaleness, IReportSampling reportSampling, IMessenger messenger)
			{
				this.reportCultureAccess = reportCultureAccess;
				this.reportResourceAccess = reportResourceAccess;
				this.reportPartitionResources = reportPartitionResources;
				this.reportStaleness = reportStaleness;
				this.reportSampling = reportSampling;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteAccessReportingServiceFactory.CultureAccessedMessage>(this.OnCultureAccessed));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteAccessReportingServiceFactory.ResourceAccessedMessage>(this.OnResourceAccessed));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteAccessReportingServiceFactory.PartitionResourcesMessage>(this.OnPartitionResources));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteAccessReportingServiceFactory.StaleSinceMessage>(this.OnStaleSince));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteAccessReportingServiceFactory.SamplingUsedMessage>(this.OnSamplingUsed));
			}

			// Token: 0x0600AA18 RID: 43544 RVA: 0x00232608 File Offset: 0x00230808
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteAccessReportingServiceFactory.CultureAccessedMessage>();
				this.messenger.RemoveHandler<RemoteAccessReportingServiceFactory.ResourceAccessedMessage>();
				this.messenger.RemoveHandler<RemoteAccessReportingServiceFactory.PartitionResourcesMessage>();
				this.messenger.RemoveHandler<RemoteAccessReportingServiceFactory.StaleSinceMessage>();
				this.messenger.RemoveHandler<RemoteAccessReportingServiceFactory.SamplingUsedMessage>();
				this.messenger = null;
				this.reportCultureAccess = null;
				this.reportResourceAccess = null;
				this.reportPartitionResources = null;
				this.reportStaleness = null;
				this.reportSampling = null;
			}

			// Token: 0x0600AA19 RID: 43545 RVA: 0x00232676 File Offset: 0x00230876
			private void OnCultureAccessed(IMessageChannel channel, RemoteAccessReportingServiceFactory.CultureAccessedMessage message)
			{
				if (this.reportCultureAccess != null)
				{
					this.reportCultureAccess.CultureAccessed();
				}
			}

			// Token: 0x0600AA1A RID: 43546 RVA: 0x0023268B File Offset: 0x0023088B
			private void OnResourceAccessed(IMessageChannel channel, RemoteAccessReportingServiceFactory.ResourceAccessedMessage message)
			{
				if (this.reportResourceAccess != null)
				{
					this.reportResourceAccess.ResourceAccessed(message.Resource);
				}
			}

			// Token: 0x0600AA1B RID: 43547 RVA: 0x002326A6 File Offset: 0x002308A6
			private void OnPartitionResources(IMessageChannel channel, RemoteAccessReportingServiceFactory.PartitionResourcesMessage message)
			{
				if (this.reportPartitionResources != null)
				{
					this.reportPartitionResources.PartitionResources(message.PartitionKey, message.Resources);
				}
			}

			// Token: 0x0600AA1C RID: 43548 RVA: 0x002326C7 File Offset: 0x002308C7
			private void OnStaleSince(IMessageChannel channel, RemoteAccessReportingServiceFactory.StaleSinceMessage message)
			{
				if (this.reportStaleness != null)
				{
					this.reportStaleness.StaleSince(message.StaleSince);
				}
			}

			// Token: 0x0600AA1D RID: 43549 RVA: 0x002326E2 File Offset: 0x002308E2
			private void OnSamplingUsed(IMessageChannel channel, RemoteAccessReportingServiceFactory.SamplingUsedMessage message)
			{
				if (this.reportSampling != null)
				{
					this.reportSampling.SamplingUsed();
				}
			}

			// Token: 0x0400585F RID: 22623
			private IReportCultureAccess reportCultureAccess;

			// Token: 0x04005860 RID: 22624
			private IReportResourceAccess reportResourceAccess;

			// Token: 0x04005861 RID: 22625
			private IReportPartitionResources reportPartitionResources;

			// Token: 0x04005862 RID: 22626
			private IReportStaleness reportStaleness;

			// Token: 0x04005863 RID: 22627
			private IReportSampling reportSampling;

			// Token: 0x04005864 RID: 22628
			private IMessenger messenger;
		}

		// Token: 0x02001A49 RID: 6729
		public sealed class CultureAccessedMessage : BufferedMessage
		{
			// Token: 0x0600AA1E RID: 43550 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x0600AA1F RID: 43551 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Deserialize(BinaryReader reader)
			{
			}
		}

		// Token: 0x02001A4A RID: 6730
		public sealed class ResourceAccessedMessage : BufferedMessage
		{
			// Token: 0x17002B2C RID: 11052
			// (get) Token: 0x0600AA21 RID: 43553 RVA: 0x002326FF File Offset: 0x002308FF
			// (set) Token: 0x0600AA22 RID: 43554 RVA: 0x00232707 File Offset: 0x00230907
			public IResource Resource { get; set; }

			// Token: 0x0600AA23 RID: 43555 RVA: 0x00232710 File Offset: 0x00230910
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteIResource(this.Resource);
			}

			// Token: 0x0600AA24 RID: 43556 RVA: 0x0023271E File Offset: 0x0023091E
			public override void Deserialize(BinaryReader reader)
			{
				this.Resource = reader.ReadIResource();
			}
		}

		// Token: 0x02001A4B RID: 6731
		public sealed class PartitionResourcesMessage : BufferedMessage
		{
			// Token: 0x17002B2D RID: 11053
			// (get) Token: 0x0600AA26 RID: 43558 RVA: 0x0023272C File Offset: 0x0023092C
			// (set) Token: 0x0600AA27 RID: 43559 RVA: 0x00232734 File Offset: 0x00230934
			public IPartitionKey PartitionKey { get; set; }

			// Token: 0x17002B2E RID: 11054
			// (get) Token: 0x0600AA28 RID: 43560 RVA: 0x0023273D File Offset: 0x0023093D
			// (set) Token: 0x0600AA29 RID: 43561 RVA: 0x00232745 File Offset: 0x00230945
			public IEnumerable<IResource> Resources { get; set; }

			// Token: 0x0600AA2A RID: 43562 RVA: 0x00232750 File Offset: 0x00230950
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteNullable(this.PartitionKey, delegate(BinaryWriter w, IPartitionKey p)
				{
					writer.WriteIPartitionKey(p);
				});
				writer.WriteArray(this.Resources.ToArray<IResource>(), delegate(BinaryWriter w, IResource r)
				{
					w.WriteIResource(r);
				});
			}

			// Token: 0x0600AA2B RID: 43563 RVA: 0x002327BC File Offset: 0x002309BC
			public override void Deserialize(BinaryReader reader)
			{
				this.PartitionKey = reader.ReadNullable((BinaryReader r) => r.ReadIPartitionKey());
				this.Resources = reader.ReadArray((BinaryReader r) => r.ReadIResource());
			}
		}

		// Token: 0x02001A4E RID: 6734
		public sealed class StaleSinceMessage : BufferedMessage
		{
			// Token: 0x17002B2F RID: 11055
			// (get) Token: 0x0600AA34 RID: 43572 RVA: 0x00232852 File Offset: 0x00230A52
			// (set) Token: 0x0600AA35 RID: 43573 RVA: 0x0023285A File Offset: 0x00230A5A
			public DateTime StaleSince { get; set; }

			// Token: 0x0600AA36 RID: 43574 RVA: 0x00232863 File Offset: 0x00230A63
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteDateTime(this.StaleSince);
			}

			// Token: 0x0600AA37 RID: 43575 RVA: 0x00232871 File Offset: 0x00230A71
			public override void Deserialize(BinaryReader reader)
			{
				this.StaleSince = reader.ReadDateTime();
			}
		}

		// Token: 0x02001A4F RID: 6735
		public sealed class SamplingUsedMessage : BufferedMessage
		{
			// Token: 0x0600AA39 RID: 43577 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x0600AA3A RID: 43578 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Deserialize(BinaryReader reader)
			{
			}
		}
	}
}
