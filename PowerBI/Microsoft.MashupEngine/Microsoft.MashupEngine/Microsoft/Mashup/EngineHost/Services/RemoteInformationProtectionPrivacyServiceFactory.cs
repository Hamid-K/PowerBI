using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Evaluator.Services;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B22 RID: 6946
	public class RemoteInformationProtectionPrivacyServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AE01 RID: 44545 RVA: 0x0023AAA0 File Offset: 0x00238CA0
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			ITraitPrivacyService traitPrivacyService = engineHost.QueryService<ITraitPrivacyService>();
			IDocumentEvaluationConfigService documentEvaluationConfigService = engineHost.QueryService<IDocumentEvaluationConfigService>();
			if (traitPrivacyService == null || (documentEvaluationConfigService != null && !documentEvaluationConfigService.Config.enableFirewall))
			{
				proxyInitArgs.WriteBool(false);
				return EmptyStub.Instance;
			}
			proxyInitArgs.WriteBool(true);
			return new RemoteInformationProtectionPrivacyServiceFactory.Stub(engineHost, traitPrivacyService, messenger);
		}

		// Token: 0x0600AE02 RID: 44546 RVA: 0x0023AAEA File Offset: 0x00238CEA
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			if (proxyInitArgs.ReadBool())
			{
				return new RemoteInformationProtectionPrivacyServiceFactory.Proxy(messenger);
			}
			return EmptyProxy.Instance;
		}

		// Token: 0x02001B23 RID: 6947
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, ITraitPrivacyService
		{
			// Token: 0x0600AE04 RID: 44548 RVA: 0x0023AB00 File Offset: 0x00238D00
			public Proxy(IMessenger messenger)
			{
				this.messenger = messenger;
			}

			// Token: 0x0600AE05 RID: 44549 RVA: 0x0023AB10 File Offset: 0x00238D10
			public T QueryService<T>() where T : class
			{
				if (typeof(T) == typeof(ITraitPrivacyService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600AE06 RID: 44550 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0600AE07 RID: 44551 RVA: 0x0023AB48 File Offset: 0x00238D48
			public void VerifyPrivacyTrait(IResource resource, IValue trait)
			{
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteInformationProtectionPrivacyServiceFactory.ConfirmPrivacyLevelRequestMessage
					{
						Resource = resource,
						Trait = trait
					});
					Exception exception = messageChannel.WaitFor<RemoteInformationProtectionPrivacyServiceFactory.ConfirmPrivacyLevelResponseMessage>().Exception;
					if (exception != null)
					{
						throw exception;
					}
				}
			}

			// Token: 0x040059C2 RID: 22978
			private readonly IMessenger messenger;
		}

		// Token: 0x02001B24 RID: 6948
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600AE08 RID: 44552 RVA: 0x0023ABA8 File Offset: 0x00238DA8
			public Stub(IEngineHost engineHost, ITraitPrivacyService service, IMessenger messenger)
			{
				this.engineHost = engineHost;
				this.service = service;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteInformationProtectionPrivacyServiceFactory.ConfirmPrivacyLevelRequestMessage>(this.OnConfirmPrivacyLevel));
			}

			// Token: 0x0600AE09 RID: 44553 RVA: 0x0023ABDC File Offset: 0x00238DDC
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteInformationProtectionPrivacyServiceFactory.ConfirmPrivacyLevelRequestMessage>();
				this.messenger = null;
				this.service = null;
			}

			// Token: 0x0600AE0A RID: 44554 RVA: 0x0023ABF8 File Offset: 0x00238DF8
			private void OnConfirmPrivacyLevel(IMessageChannel channel, RemoteInformationProtectionPrivacyServiceFactory.ConfirmPrivacyLevelRequestMessage message)
			{
				RemoteInformationProtectionPrivacyServiceFactory.ConfirmPrivacyLevelResponseMessage confirmPrivacyLevelResponseMessage = new RemoteInformationProtectionPrivacyServiceFactory.ConfirmPrivacyLevelResponseMessage();
				try
				{
					this.service.VerifyPrivacyTrait(message.Resource, message.Trait);
				}
				catch (FirewallException2 firewallException)
				{
					confirmPrivacyLevelResponseMessage.Exception = firewallException;
				}
				channel.Post(confirmPrivacyLevelResponseMessage);
			}

			// Token: 0x040059C3 RID: 22979
			private IEngineHost engineHost;

			// Token: 0x040059C4 RID: 22980
			private ITraitPrivacyService service;

			// Token: 0x040059C5 RID: 22981
			private IMessenger messenger;
		}

		// Token: 0x02001B25 RID: 6949
		private sealed class ConfirmPrivacyLevelRequestMessage : BufferedMessage
		{
			// Token: 0x17002BB0 RID: 11184
			// (get) Token: 0x0600AE0B RID: 44555 RVA: 0x0023AC48 File Offset: 0x00238E48
			// (set) Token: 0x0600AE0C RID: 44556 RVA: 0x0023AC50 File Offset: 0x00238E50
			public IResource Resource { get; set; }

			// Token: 0x17002BB1 RID: 11185
			// (get) Token: 0x0600AE0D RID: 44557 RVA: 0x0023AC59 File Offset: 0x00238E59
			// (set) Token: 0x0600AE0E RID: 44558 RVA: 0x0023AC61 File Offset: 0x00238E61
			public IValue Trait { get; set; }

			// Token: 0x0600AE0F RID: 44559 RVA: 0x0023AC6A File Offset: 0x00238E6A
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteIResource(this.Resource);
				writer.WriteString(ValueSerializer.SerializePreviewValue(MashupEngines.Version1, this.Trait, null, new ValueSerializerOptions?(RemoteInformationProtectionPrivacyServiceFactory.ConfirmPrivacyLevelRequestMessage.traitSerializerOptions)));
			}

			// Token: 0x0600AE10 RID: 44560 RVA: 0x0023AC99 File Offset: 0x00238E99
			public override void Deserialize(BinaryReader reader)
			{
				this.Resource = reader.ReadIResource();
				this.Trait = ValueDeserializer.Deserialize(MashupEngines.Version1, reader.ReadString()).AsRecord;
			}

			// Token: 0x040059C6 RID: 22982
			private static readonly ValueSerializerOptions traitSerializerOptions = new ValueSerializerOptions
			{
				MaxValueDepth = 3,
				NestedRecords = true
			};
		}

		// Token: 0x02001B26 RID: 6950
		private sealed class ConfirmPrivacyLevelResponseMessage : BufferedMessage
		{
			// Token: 0x17002BB2 RID: 11186
			// (get) Token: 0x0600AE13 RID: 44563 RVA: 0x0023ACEF File Offset: 0x00238EEF
			// (set) Token: 0x0600AE14 RID: 44564 RVA: 0x0023ACF7 File Offset: 0x00238EF7
			public Exception Exception { get; set; }

			// Token: 0x0600AE15 RID: 44565 RVA: 0x0023AD00 File Offset: 0x00238F00
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteNullable(this.Exception, delegate(BinaryWriter w, Exception e)
				{
					w.WriteException(e);
				});
			}

			// Token: 0x0600AE16 RID: 44566 RVA: 0x0023AD2D File Offset: 0x00238F2D
			public override void Deserialize(BinaryReader reader)
			{
				this.Exception = reader.ReadNullable((BinaryReader r) => r.ReadException());
			}
		}
	}
}
