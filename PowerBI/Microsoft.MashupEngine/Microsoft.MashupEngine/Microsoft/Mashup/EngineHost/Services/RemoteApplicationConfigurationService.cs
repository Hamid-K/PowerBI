using System;
using System.Data;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Serialization;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A55 RID: 6741
	internal class RemoteApplicationConfigurationService : IRemoteServiceFactory
	{
		// Token: 0x0600AA51 RID: 43601 RVA: 0x00232A4A File Offset: 0x00230C4A
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return new RemoteApplicationConfigurationService.Stub(engineHost.QueryService<IApplicationConfigurationService>(), messenger);
		}

		// Token: 0x0600AA52 RID: 43602 RVA: 0x00232A58 File Offset: 0x00230C58
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemoteApplicationConfigurationService.Proxy(messenger);
		}

		// Token: 0x02001A56 RID: 6742
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IApplicationConfigurationService
		{
			// Token: 0x0600AA54 RID: 43604 RVA: 0x00232A60 File Offset: 0x00230C60
			public Proxy(IMessenger messenger)
			{
				this.messenger = messenger;
			}

			// Token: 0x0600AA55 RID: 43605 RVA: 0x00232A70 File Offset: 0x00230C70
			public DataTable GetDbProviderFactoryClasses()
			{
				DataTable classes;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteApplicationConfigurationService.DbProviderFactoryClassesRequestMessage());
					classes = messageChannel.WaitFor<RemoteApplicationConfigurationService.DbProviderFactoryClassesResponseMessage>().Classes;
				}
				return classes;
			}

			// Token: 0x0600AA56 RID: 43606 RVA: 0x00232AC0 File Offset: 0x00230CC0
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IApplicationConfigurationService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600AA57 RID: 43607 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x04005874 RID: 22644
			private readonly IMessenger messenger;
		}

		// Token: 0x02001A57 RID: 6743
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600AA58 RID: 43608 RVA: 0x00232AF8 File Offset: 0x00230CF8
			public Stub(IApplicationConfigurationService appConfigService, IMessenger messenger)
			{
				this.appConfigService = appConfigService;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteApplicationConfigurationService.DbProviderFactoryClassesRequestMessage>(this.OnGetFactoryClasses));
			}

			// Token: 0x0600AA59 RID: 43609 RVA: 0x00232B25 File Offset: 0x00230D25
			private void OnGetFactoryClasses(IMessageChannel channel, RemoteApplicationConfigurationService.DbProviderFactoryClassesRequestMessage message)
			{
				channel.Post(new RemoteApplicationConfigurationService.DbProviderFactoryClassesResponseMessage
				{
					Classes = this.appConfigService.GetDbProviderFactoryClasses()
				});
			}

			// Token: 0x0600AA5A RID: 43610 RVA: 0x00232B43 File Offset: 0x00230D43
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteApplicationConfigurationService.DbProviderFactoryClassesRequestMessage>();
				this.messenger = null;
				this.appConfigService = null;
			}

			// Token: 0x04005875 RID: 22645
			private IApplicationConfigurationService appConfigService;

			// Token: 0x04005876 RID: 22646
			private IMessenger messenger;
		}

		// Token: 0x02001A58 RID: 6744
		private sealed class DbProviderFactoryClassesRequestMessage : BufferedMessage
		{
			// Token: 0x0600AA5B RID: 43611 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x0600AA5C RID: 43612 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Deserialize(BinaryReader reader)
			{
			}
		}

		// Token: 0x02001A59 RID: 6745
		private sealed class DbProviderFactoryClassesResponseMessage : BufferedMessage
		{
			// Token: 0x17002B33 RID: 11059
			// (get) Token: 0x0600AA5E RID: 43614 RVA: 0x00232B5E File Offset: 0x00230D5E
			// (set) Token: 0x0600AA5F RID: 43615 RVA: 0x00232B66 File Offset: 0x00230D66
			public DataTable Classes { get; set; }

			// Token: 0x0600AA60 RID: 43616 RVA: 0x00232B70 File Offset: 0x00230D70
			public override void Serialize(BinaryWriter writer)
			{
				if (this.Classes == null)
				{
					writer.Write(false);
					return;
				}
				writer.Write(true);
				new ObjectWriter(writer).WriteDataTable(this.Classes);
			}

			// Token: 0x0600AA61 RID: 43617 RVA: 0x00232BA8 File Offset: 0x00230DA8
			public override void Deserialize(BinaryReader reader)
			{
				this.Classes = (reader.ReadBoolean() ? new ObjectReader(reader).ReadDataTable() : null);
			}
		}
	}
}
