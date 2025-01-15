using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B0D RID: 6925
	internal class RemoteResourcePathServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600ADAF RID: 44463 RVA: 0x0023A36B File Offset: 0x0023856B
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return new RemoteResourcePathServiceFactory.Stub(engineHost.QueryService<IResourcePathService>(), messenger);
		}

		// Token: 0x0600ADB0 RID: 44464 RVA: 0x0023A379 File Offset: 0x00238579
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemoteResourcePathServiceFactory.Proxy(messenger);
		}

		// Token: 0x02001B0E RID: 6926
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IResourcePathService
		{
			// Token: 0x0600ADB2 RID: 44466 RVA: 0x0023A381 File Offset: 0x00238581
			public Proxy(IMessenger messenger)
			{
				this.messenger = messenger;
			}

			// Token: 0x0600ADB3 RID: 44467 RVA: 0x0023A390 File Offset: 0x00238590
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IResourcePathService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600ADB4 RID: 44468 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0600ADB5 RID: 44469 RVA: 0x0023A3C8 File Offset: 0x002385C8
			public bool StartsWith(IResource permittedResource, IResource attemptedResource)
			{
				bool result;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteResourcePathServiceFactory.ResourcePathRequestMessage
					{
						PermittedResource = permittedResource,
						AttemptedResource = attemptedResource
					});
					result = messageChannel.WaitFor<RemoteResourcePathServiceFactory.ResourcePathResponseMessage>().Result;
				}
				return result;
			}

			// Token: 0x040059AB RID: 22955
			private readonly IMessenger messenger;
		}

		// Token: 0x02001B0F RID: 6927
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600ADB6 RID: 44470 RVA: 0x0023A424 File Offset: 0x00238624
			public Stub(IResourcePathService resourcePathService, IMessenger messenger)
			{
				this.resourcePathService = resourcePathService;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteResourcePathServiceFactory.ResourcePathRequestMessage>(this.OnResourcePathRequest));
			}

			// Token: 0x0600ADB7 RID: 44471 RVA: 0x0023A451 File Offset: 0x00238651
			private void OnResourcePathRequest(IMessageChannel channel, RemoteResourcePathServiceFactory.ResourcePathRequestMessage message)
			{
				channel.Post(new RemoteResourcePathServiceFactory.ResourcePathResponseMessage
				{
					Result = this.resourcePathService.StartsWith(message.PermittedResource, message.AttemptedResource)
				});
			}

			// Token: 0x0600ADB8 RID: 44472 RVA: 0x0023A47B File Offset: 0x0023867B
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteResourcePathServiceFactory.ResourcePathRequestMessage>();
				this.messenger = null;
				this.resourcePathService = null;
			}

			// Token: 0x040059AC RID: 22956
			private IResourcePathService resourcePathService;

			// Token: 0x040059AD RID: 22957
			private IMessenger messenger;
		}

		// Token: 0x02001B10 RID: 6928
		public sealed class ResourcePathRequestMessage : BufferedMessage
		{
			// Token: 0x17002BA7 RID: 11175
			// (get) Token: 0x0600ADB9 RID: 44473 RVA: 0x0023A496 File Offset: 0x00238696
			// (set) Token: 0x0600ADBA RID: 44474 RVA: 0x0023A49E File Offset: 0x0023869E
			public IResource PermittedResource { get; set; }

			// Token: 0x17002BA8 RID: 11176
			// (get) Token: 0x0600ADBB RID: 44475 RVA: 0x0023A4A7 File Offset: 0x002386A7
			// (set) Token: 0x0600ADBC RID: 44476 RVA: 0x0023A4AF File Offset: 0x002386AF
			public IResource AttemptedResource { get; set; }

			// Token: 0x0600ADBD RID: 44477 RVA: 0x0023A4B8 File Offset: 0x002386B8
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteIResource(this.PermittedResource);
				writer.WriteIResource(this.AttemptedResource);
			}

			// Token: 0x0600ADBE RID: 44478 RVA: 0x0023A4D2 File Offset: 0x002386D2
			public override void Deserialize(BinaryReader reader)
			{
				this.PermittedResource = reader.ReadIResource();
				this.AttemptedResource = reader.ReadIResource();
			}
		}

		// Token: 0x02001B11 RID: 6929
		public sealed class ResourcePathResponseMessage : BufferedMessage
		{
			// Token: 0x17002BA9 RID: 11177
			// (get) Token: 0x0600ADC0 RID: 44480 RVA: 0x0023A4EC File Offset: 0x002386EC
			// (set) Token: 0x0600ADC1 RID: 44481 RVA: 0x0023A4F4 File Offset: 0x002386F4
			public bool Result { get; set; }

			// Token: 0x0600ADC2 RID: 44482 RVA: 0x0023A4FD File Offset: 0x002386FD
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteBool(this.Result);
			}

			// Token: 0x0600ADC3 RID: 44483 RVA: 0x0023A50B File Offset: 0x0023870B
			public override void Deserialize(BinaryReader reader)
			{
				this.Result = reader.ReadBool();
			}
		}
	}
}
