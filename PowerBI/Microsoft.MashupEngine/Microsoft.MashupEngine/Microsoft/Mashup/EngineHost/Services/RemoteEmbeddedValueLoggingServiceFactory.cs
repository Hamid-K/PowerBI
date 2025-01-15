using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A7A RID: 6778
	internal class RemoteEmbeddedValueLoggingServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AAFF RID: 43775 RVA: 0x002345A7 File Offset: 0x002327A7
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return new RemoteEmbeddedValueLoggingServiceFactory.Stub(engineHost.QueryService<IEmbeddedValueLoggingService>(), messenger);
		}

		// Token: 0x0600AB00 RID: 43776 RVA: 0x002345B5 File Offset: 0x002327B5
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemoteEmbeddedValueLoggingServiceFactory.Proxy(messenger);
		}

		// Token: 0x02001A7B RID: 6779
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IEmbeddedValueLoggingService
		{
			// Token: 0x0600AB02 RID: 43778 RVA: 0x002345BD File Offset: 0x002327BD
			public Proxy(IMessenger messenger)
			{
				this.messenger = messenger;
			}

			// Token: 0x0600AB03 RID: 43779 RVA: 0x002345CC File Offset: 0x002327CC
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IEmbeddedValueLoggingService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600AB04 RID: 43780 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0600AB05 RID: 43781 RVA: 0x00234604 File Offset: 0x00232804
			public void LogEmbeddedValue(string path)
			{
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteEmbeddedValueLoggingServiceFactory.LogEmbeddedValueMessage
					{
						Path = path
					});
				}
			}

			// Token: 0x040058B5 RID: 22709
			private readonly IMessenger messenger;
		}

		// Token: 0x02001A7C RID: 6780
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600AB06 RID: 43782 RVA: 0x0023464C File Offset: 0x0023284C
			public Stub(IEmbeddedValueLoggingService embeddedValueLoggingService, IMessenger messenger)
			{
				this.embeddedValueLoggingService = embeddedValueLoggingService;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteEmbeddedValueLoggingServiceFactory.LogEmbeddedValueMessage>(this.OnLogEmbeddedValue));
			}

			// Token: 0x0600AB07 RID: 43783 RVA: 0x00234679 File Offset: 0x00232879
			private void OnLogEmbeddedValue(IMessageChannel channel, RemoteEmbeddedValueLoggingServiceFactory.LogEmbeddedValueMessage message)
			{
				this.embeddedValueLoggingService.LogEmbeddedValue(message.Path);
			}

			// Token: 0x0600AB08 RID: 43784 RVA: 0x0023468C File Offset: 0x0023288C
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteEmbeddedValueLoggingServiceFactory.LogEmbeddedValueMessage>();
				this.messenger = null;
				this.embeddedValueLoggingService = null;
			}

			// Token: 0x040058B6 RID: 22710
			private IEmbeddedValueLoggingService embeddedValueLoggingService;

			// Token: 0x040058B7 RID: 22711
			private IMessenger messenger;
		}

		// Token: 0x02001A7D RID: 6781
		public sealed class LogEmbeddedValueMessage : BufferedMessage
		{
			// Token: 0x17002B4C RID: 11084
			// (get) Token: 0x0600AB09 RID: 43785 RVA: 0x002346A7 File Offset: 0x002328A7
			// (set) Token: 0x0600AB0A RID: 43786 RVA: 0x002346AF File Offset: 0x002328AF
			public string Path { get; set; }

			// Token: 0x0600AB0B RID: 43787 RVA: 0x002346B8 File Offset: 0x002328B8
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteNullableString(this.Path);
			}

			// Token: 0x0600AB0C RID: 43788 RVA: 0x002346C6 File Offset: 0x002328C6
			public override void Deserialize(BinaryReader reader)
			{
				this.Path = reader.ReadNullableString();
			}
		}
	}
}
