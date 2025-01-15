using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001ABD RID: 6845
	internal class RemotePartitionDisplayNameServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AC2B RID: 44075 RVA: 0x00236B25 File Offset: 0x00234D25
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return new RemotePartitionDisplayNameServiceFactory.Stub(engineHost.QueryService<IPartitionDisplayNameService>(), messenger);
		}

		// Token: 0x0600AC2C RID: 44076 RVA: 0x00236B33 File Offset: 0x00234D33
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemotePartitionDisplayNameServiceFactory.Proxy(messenger);
		}

		// Token: 0x02001ABE RID: 6846
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IPartitionDisplayNameService
		{
			// Token: 0x0600AC2E RID: 44078 RVA: 0x00236B3B File Offset: 0x00234D3B
			public Proxy(IMessenger messenger)
			{
				this.messenger = messenger;
			}

			// Token: 0x0600AC2F RID: 44079 RVA: 0x00236B4C File Offset: 0x00234D4C
			public string GetDisplayNameForPartition(IPartitionKey partitionKey)
			{
				string displayName;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemotePartitionDisplayNameServiceFactory.GetDisplayNameRequestMessage
					{
						PartitionKey = partitionKey
					});
					displayName = messageChannel.WaitFor<RemotePartitionDisplayNameServiceFactory.GetDisplayNameResponseMessage>().DisplayName;
				}
				return displayName;
			}

			// Token: 0x0600AC30 RID: 44080 RVA: 0x00236BA0 File Offset: 0x00234DA0
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IPartitionDisplayNameService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600AC31 RID: 44081 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x04005917 RID: 22807
			private readonly IMessenger messenger;
		}

		// Token: 0x02001ABF RID: 6847
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600AC32 RID: 44082 RVA: 0x00236BD8 File Offset: 0x00234DD8
			public Stub(IPartitionDisplayNameService partitionDisplayNameService, IMessenger messenger)
			{
				this.partitionDisplayNameService = partitionDisplayNameService;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePartitionDisplayNameServiceFactory.GetDisplayNameRequestMessage>(this.OnGetDisplayNameRequest));
			}

			// Token: 0x0600AC33 RID: 44083 RVA: 0x00236C05 File Offset: 0x00234E05
			private void OnGetDisplayNameRequest(IMessageChannel channel, RemotePartitionDisplayNameServiceFactory.GetDisplayNameRequestMessage message)
			{
				channel.Post(new RemotePartitionDisplayNameServiceFactory.GetDisplayNameResponseMessage
				{
					DisplayName = this.partitionDisplayNameService.GetDisplayNameForPartition(message.PartitionKey)
				});
			}

			// Token: 0x0600AC34 RID: 44084 RVA: 0x00236C29 File Offset: 0x00234E29
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemotePartitionDisplayNameServiceFactory.GetDisplayNameRequestMessage>();
				this.messenger = null;
				this.partitionDisplayNameService = null;
			}

			// Token: 0x04005918 RID: 22808
			private IPartitionDisplayNameService partitionDisplayNameService;

			// Token: 0x04005919 RID: 22809
			private IMessenger messenger;
		}

		// Token: 0x02001AC0 RID: 6848
		public sealed class GetDisplayNameRequestMessage : BufferedMessage
		{
			// Token: 0x17002B67 RID: 11111
			// (get) Token: 0x0600AC35 RID: 44085 RVA: 0x00236C44 File Offset: 0x00234E44
			// (set) Token: 0x0600AC36 RID: 44086 RVA: 0x00236C4C File Offset: 0x00234E4C
			public IPartitionKey PartitionKey { get; set; }

			// Token: 0x0600AC37 RID: 44087 RVA: 0x00236C55 File Offset: 0x00234E55
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteNullable(this.PartitionKey, delegate(BinaryWriter w, IPartitionKey p)
				{
					w.WriteIPartitionKey(p);
				});
			}

			// Token: 0x0600AC38 RID: 44088 RVA: 0x00236C82 File Offset: 0x00234E82
			public override void Deserialize(BinaryReader reader)
			{
				this.PartitionKey = reader.ReadNullable((BinaryReader r) => r.ReadIPartitionKey());
			}
		}

		// Token: 0x02001AC2 RID: 6850
		public sealed class GetDisplayNameResponseMessage : BufferedMessage
		{
			// Token: 0x17002B68 RID: 11112
			// (get) Token: 0x0600AC3E RID: 44094 RVA: 0x00236CC4 File Offset: 0x00234EC4
			// (set) Token: 0x0600AC3F RID: 44095 RVA: 0x00236CCC File Offset: 0x00234ECC
			public string DisplayName { get; set; }

			// Token: 0x0600AC40 RID: 44096 RVA: 0x00236CD5 File Offset: 0x00234ED5
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteString(this.DisplayName);
			}

			// Token: 0x0600AC41 RID: 44097 RVA: 0x00236CE3 File Offset: 0x00234EE3
			public override void Deserialize(BinaryReader reader)
			{
				this.DisplayName = reader.ReadString();
			}
		}
	}
}
