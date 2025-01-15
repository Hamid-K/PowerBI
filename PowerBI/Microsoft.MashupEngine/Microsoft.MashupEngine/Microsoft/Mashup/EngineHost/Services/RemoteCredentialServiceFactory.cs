using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A68 RID: 6760
	internal class RemoteCredentialServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AA9E RID: 43678 RVA: 0x00233768 File Offset: 0x00231968
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return new RemoteCredentialServiceFactory.Stub(engineHost.QueryService<ICredentialService>(), engineHost, messenger);
		}

		// Token: 0x0600AA9F RID: 43679 RVA: 0x00233777 File Offset: 0x00231977
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemoteCredentialServiceFactory.Proxy(engineHost, messenger);
		}

		// Token: 0x02001A69 RID: 6761
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, ICredentialService, IResourcePermissionService
		{
			// Token: 0x0600AAA1 RID: 43681 RVA: 0x00233780 File Offset: 0x00231980
			public Proxy(IEngineHost engineHost, IMessenger messenger)
			{
				this.messenger = messenger;
			}

			// Token: 0x0600AAA2 RID: 43682 RVA: 0x00233790 File Offset: 0x00231990
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(ICredentialService) || typeof(T) == typeof(IResourcePermissionService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600AAA3 RID: 43683 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0600AAA4 RID: 43684 RVA: 0x002337E4 File Offset: 0x002319E4
			public ResourceCredentialCollection RefreshCredential(IResource resource, bool forceRefresh = false)
			{
				ResourceCredentialCollection credentials;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteCredentialServiceFactory.RefreshCredentialRequestMessage
					{
						Resource = resource,
						ForceRefresh = forceRefresh
					});
					credentials = messageChannel.WaitFor<RemoteCredentialServiceFactory.CredentialResponseMessage>().Credentials;
				}
				return credentials;
			}

			// Token: 0x0600AAA5 RID: 43685 RVA: 0x00233840 File Offset: 0x00231A40
			public void UpdateExchangeCredential(IResource resource, ResourceCredentialCollection updatedCredentialCollection)
			{
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteCredentialServiceFactory.UpdateExchangeCredentialMessage
					{
						Resource = resource,
						UpdatedCredentialCollection = updatedCredentialCollection
					});
				}
			}

			// Token: 0x0600AAA6 RID: 43686 RVA: 0x00233890 File Offset: 0x00231A90
			public bool TryGetCredentials(IResource resource, out ResourceCredentialCollection credentials)
			{
				bool flag;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteCredentialServiceFactory.CredentialRequestMessage
					{
						Resource = resource
					});
					credentials = messageChannel.WaitFor<RemoteCredentialServiceFactory.CredentialResponseMessage>().Credentials;
					flag = credentials != null;
				}
				return flag;
			}

			// Token: 0x0600AAA7 RID: 43687 RVA: 0x002338EC File Offset: 0x00231AEC
			public bool IsResourceAccessPermitted(IResource resource, out ResourceCredentialCollection credentials)
			{
				return this.TryGetCredentials(resource, out credentials);
			}

			// Token: 0x04005894 RID: 22676
			private readonly IMessenger messenger;
		}

		// Token: 0x02001A6A RID: 6762
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600AAA8 RID: 43688 RVA: 0x002338F8 File Offset: 0x00231AF8
			public Stub(ICredentialService credentialService, IEngineHost engineHost, IMessenger messenger)
			{
				this.credentialService = credentialService;
				this.engineHost = engineHost;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteCredentialServiceFactory.RefreshCredentialRequestMessage>(this.OnRefreshCredentialRequest));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteCredentialServiceFactory.UpdateExchangeCredentialMessage>(this.OnUpdateExchangeCredential));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteCredentialServiceFactory.CredentialRequestMessage>(this.OnCredentialRequest));
			}

			// Token: 0x0600AAA9 RID: 43689 RVA: 0x00233965 File Offset: 0x00231B65
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteCredentialServiceFactory.RefreshCredentialRequestMessage>();
				this.messenger.RemoveHandler<RemoteCredentialServiceFactory.UpdateExchangeCredentialMessage>();
				this.messenger.RemoveHandler<RemoteCredentialServiceFactory.CredentialRequestMessage>();
				this.messenger = null;
				this.engineHost = null;
				this.credentialService = null;
			}

			// Token: 0x0600AAAA RID: 43690 RVA: 0x002339A0 File Offset: 0x00231BA0
			private void OnRefreshCredentialRequest(IMessageChannel channel, RemoteCredentialServiceFactory.RefreshCredentialRequestMessage message)
			{
				EvaluationHost.ReportExceptions("RemoteCredentialServicesFactory/Stub/OnRefreshCredentialRequest", this.engineHost, channel, delegate
				{
					channel.Post(new RemoteCredentialServiceFactory.CredentialResponseMessage
					{
						Credentials = this.credentialService.RefreshCredential(message.Resource, message.ForceRefresh)
					});
				});
			}

			// Token: 0x0600AAAB RID: 43691 RVA: 0x002339EA File Offset: 0x00231BEA
			private void OnUpdateExchangeCredential(IMessageChannel channel, RemoteCredentialServiceFactory.UpdateExchangeCredentialMessage message)
			{
				this.credentialService.UpdateExchangeCredential(message.Resource, message.UpdatedCredentialCollection);
			}

			// Token: 0x0600AAAC RID: 43692 RVA: 0x00233A04 File Offset: 0x00231C04
			private void OnCredentialRequest(IMessageChannel channel, RemoteCredentialServiceFactory.CredentialRequestMessage message)
			{
				EvaluationHost.ReportExceptions("RemoteCredentialServicesFactory/Stub/OnCredentialRequest", this.engineHost, channel, delegate
				{
					ResourceCredentialCollection resourceCredentialCollection;
					if (!this.credentialService.TryGetCredentials(message.Resource, out resourceCredentialCollection))
					{
						resourceCredentialCollection = null;
					}
					channel.Post(new RemoteCredentialServiceFactory.CredentialResponseMessage
					{
						Credentials = resourceCredentialCollection
					});
				});
			}

			// Token: 0x04005895 RID: 22677
			private ICredentialService credentialService;

			// Token: 0x04005896 RID: 22678
			private IEngineHost engineHost;

			// Token: 0x04005897 RID: 22679
			private IMessenger messenger;
		}

		// Token: 0x02001A6D RID: 6765
		public abstract class CredentialMessage : BufferedMessage
		{
			// Token: 0x17002B3A RID: 11066
			// (get) Token: 0x0600AAB1 RID: 43697 RVA: 0x00233AD1 File Offset: 0x00231CD1
			// (set) Token: 0x0600AAB2 RID: 43698 RVA: 0x00233AD9 File Offset: 0x00231CD9
			public IResource Resource { get; set; }

			// Token: 0x0600AAB3 RID: 43699 RVA: 0x00233AE2 File Offset: 0x00231CE2
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteIResource(this.Resource);
			}

			// Token: 0x0600AAB4 RID: 43700 RVA: 0x00233AF0 File Offset: 0x00231CF0
			public override void Deserialize(BinaryReader reader)
			{
				this.Resource = reader.ReadIResource();
			}
		}

		// Token: 0x02001A6E RID: 6766
		public sealed class CredentialRequestMessage : RemoteCredentialServiceFactory.CredentialMessage
		{
		}

		// Token: 0x02001A6F RID: 6767
		public sealed class RefreshCredentialRequestMessage : RemoteCredentialServiceFactory.CredentialMessage
		{
			// Token: 0x17002B3B RID: 11067
			// (get) Token: 0x0600AAB7 RID: 43703 RVA: 0x00233B06 File Offset: 0x00231D06
			// (set) Token: 0x0600AAB8 RID: 43704 RVA: 0x00233B0E File Offset: 0x00231D0E
			public bool ForceRefresh { get; set; }

			// Token: 0x0600AAB9 RID: 43705 RVA: 0x00233B17 File Offset: 0x00231D17
			public override void Serialize(BinaryWriter writer)
			{
				base.Serialize(writer);
				writer.WriteBool(this.ForceRefresh);
			}

			// Token: 0x0600AABA RID: 43706 RVA: 0x00233B2C File Offset: 0x00231D2C
			public override void Deserialize(BinaryReader reader)
			{
				base.Deserialize(reader);
				this.ForceRefresh = reader.ReadBool();
			}
		}

		// Token: 0x02001A70 RID: 6768
		public sealed class UpdateExchangeCredentialMessage : RemoteCredentialServiceFactory.CredentialMessage
		{
			// Token: 0x17002B3C RID: 11068
			// (get) Token: 0x0600AABC RID: 43708 RVA: 0x00233B41 File Offset: 0x00231D41
			// (set) Token: 0x0600AABD RID: 43709 RVA: 0x00233B49 File Offset: 0x00231D49
			public ResourceCredentialCollection UpdatedCredentialCollection { get; set; }

			// Token: 0x0600AABE RID: 43710 RVA: 0x00233B52 File Offset: 0x00231D52
			public override void Serialize(BinaryWriter writer)
			{
				base.Serialize(writer);
				writer.WriteResourceCredentialCollection(this.UpdatedCredentialCollection);
			}

			// Token: 0x0600AABF RID: 43711 RVA: 0x00233B67 File Offset: 0x00231D67
			public override void Deserialize(BinaryReader reader)
			{
				base.Deserialize(reader);
				this.UpdatedCredentialCollection = reader.ReadResourceCredentialCollection();
			}
		}

		// Token: 0x02001A71 RID: 6769
		public sealed class CredentialResponseMessage : BufferedMessage
		{
			// Token: 0x17002B3D RID: 11069
			// (get) Token: 0x0600AAC1 RID: 43713 RVA: 0x00233B7C File Offset: 0x00231D7C
			// (set) Token: 0x0600AAC2 RID: 43714 RVA: 0x00233B84 File Offset: 0x00231D84
			public ResourceCredentialCollection Credentials { get; set; }

			// Token: 0x0600AAC3 RID: 43715 RVA: 0x00233B8D File Offset: 0x00231D8D
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteBool(this.Credentials != null);
				if (this.Credentials != null)
				{
					writer.WriteResourceCredentialCollection(this.Credentials);
				}
			}

			// Token: 0x0600AAC4 RID: 43716 RVA: 0x00233BB2 File Offset: 0x00231DB2
			public override void Deserialize(BinaryReader reader)
			{
				if (reader.ReadBool())
				{
					this.Credentials = reader.ReadResourceCredentialCollection();
				}
			}
		}
	}
}
