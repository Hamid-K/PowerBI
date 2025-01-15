using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Evaluator.Services;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A8C RID: 6796
	internal class RemoteFirewallRuleServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AB4F RID: 43855 RVA: 0x0023516C File Offset: 0x0023336C
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return new RemoteFirewallRuleServiceFactory.Stub(engineHost.QueryService<IFirewallRuleService>(), messenger);
		}

		// Token: 0x0600AB50 RID: 43856 RVA: 0x0023517A File Offset: 0x0023337A
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemoteFirewallRuleServiceFactory.Proxy(messenger);
		}

		// Token: 0x02001A8D RID: 6797
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IFirewallRuleService
		{
			// Token: 0x0600AB52 RID: 43858 RVA: 0x00235182 File Offset: 0x00233382
			public Proxy(IMessenger messenger)
			{
				this.messenger = messenger;
			}

			// Token: 0x0600AB53 RID: 43859 RVA: 0x00235194 File Offset: 0x00233394
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IFirewallRuleService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600AB54 RID: 43860 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0600AB55 RID: 43861 RVA: 0x002351CC File Offset: 0x002333CC
			public FirewallGroup2 CreateFirewallGroup(IResource resource)
			{
				FirewallGroup2 firewallGroup;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteFirewallRuleServiceFactory.CreateFirewallGroupRequestMessage
					{
						Resource = resource
					});
					firewallGroup = messageChannel.WaitFor<RemoteFirewallRuleServiceFactory.FirewallRuleResponseMessage>().FirewallGroup;
				}
				return firewallGroup;
			}

			// Token: 0x0600AB56 RID: 43862 RVA: 0x00235220 File Offset: 0x00233420
			public FirewallGroup2 UpdateFirewallGroup(IResource resource, FirewallGroup2 originalGroup, IValue traits)
			{
				FirewallGroup2 firewallGroup;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteFirewallRuleServiceFactory.UpdateFirewallGroupRequestMessage
					{
						Resource = resource,
						OriginalGroup = originalGroup,
						Traits = traits
					});
					firewallGroup = messageChannel.WaitFor<RemoteFirewallRuleServiceFactory.FirewallRuleResponseMessage>().FirewallGroup;
				}
				return firewallGroup;
			}

			// Token: 0x040058D4 RID: 22740
			private readonly IMessenger messenger;
		}

		// Token: 0x02001A8E RID: 6798
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600AB57 RID: 43863 RVA: 0x00235284 File Offset: 0x00233484
			public Stub(IFirewallRuleService firewallRuleService, IMessenger messenger)
			{
				this.firewallRuleService = firewallRuleService;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteFirewallRuleServiceFactory.CreateFirewallGroupRequestMessage>(this.OnCreateFirewallRuleRequest));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteFirewallRuleServiceFactory.UpdateFirewallGroupRequestMessage>(this.OnUpdateFirewallRuleRequest));
			}

			// Token: 0x0600AB58 RID: 43864 RVA: 0x002352D3 File Offset: 0x002334D3
			private void OnCreateFirewallRuleRequest(IMessageChannel channel, RemoteFirewallRuleServiceFactory.CreateFirewallGroupRequestMessage message)
			{
				channel.Post(new RemoteFirewallRuleServiceFactory.FirewallRuleResponseMessage
				{
					FirewallGroup = this.firewallRuleService.CreateFirewallGroup(message.Resource)
				});
			}

			// Token: 0x0600AB59 RID: 43865 RVA: 0x002352F7 File Offset: 0x002334F7
			private void OnUpdateFirewallRuleRequest(IMessageChannel channel, RemoteFirewallRuleServiceFactory.UpdateFirewallGroupRequestMessage message)
			{
				channel.Post(new RemoteFirewallRuleServiceFactory.FirewallRuleResponseMessage
				{
					FirewallGroup = this.firewallRuleService.UpdateFirewallGroup(message.Resource, message.OriginalGroup, message.Traits)
				});
			}

			// Token: 0x0600AB5A RID: 43866 RVA: 0x00235327 File Offset: 0x00233527
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteFirewallRuleServiceFactory.CreateFirewallGroupRequestMessage>();
				this.messenger.RemoveHandler<RemoteFirewallRuleServiceFactory.UpdateFirewallGroupRequestMessage>();
				this.messenger = null;
				this.firewallRuleService = null;
			}

			// Token: 0x040058D5 RID: 22741
			private IFirewallRuleService firewallRuleService;

			// Token: 0x040058D6 RID: 22742
			private IMessenger messenger;
		}

		// Token: 0x02001A8F RID: 6799
		public sealed class CreateFirewallGroupRequestMessage : BufferedMessage
		{
			// Token: 0x17002B4F RID: 11087
			// (get) Token: 0x0600AB5B RID: 43867 RVA: 0x0023534D File Offset: 0x0023354D
			// (set) Token: 0x0600AB5C RID: 43868 RVA: 0x00235355 File Offset: 0x00233555
			public IResource Resource { get; set; }

			// Token: 0x0600AB5D RID: 43869 RVA: 0x0023535E File Offset: 0x0023355E
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteIResource(this.Resource);
			}

			// Token: 0x0600AB5E RID: 43870 RVA: 0x0023536C File Offset: 0x0023356C
			public override void Deserialize(BinaryReader reader)
			{
				this.Resource = reader.ReadIResource();
			}
		}

		// Token: 0x02001A90 RID: 6800
		public sealed class UpdateFirewallGroupRequestMessage : BufferedMessage
		{
			// Token: 0x17002B50 RID: 11088
			// (get) Token: 0x0600AB60 RID: 43872 RVA: 0x0023537A File Offset: 0x0023357A
			// (set) Token: 0x0600AB61 RID: 43873 RVA: 0x00235382 File Offset: 0x00233582
			public IResource Resource { get; set; }

			// Token: 0x17002B51 RID: 11089
			// (get) Token: 0x0600AB62 RID: 43874 RVA: 0x0023538B File Offset: 0x0023358B
			// (set) Token: 0x0600AB63 RID: 43875 RVA: 0x00235393 File Offset: 0x00233593
			public FirewallGroup2 OriginalGroup { get; set; }

			// Token: 0x17002B52 RID: 11090
			// (get) Token: 0x0600AB64 RID: 43876 RVA: 0x0023539C File Offset: 0x0023359C
			// (set) Token: 0x0600AB65 RID: 43877 RVA: 0x002353A4 File Offset: 0x002335A4
			public IValue Traits { get; set; }

			// Token: 0x0600AB66 RID: 43878 RVA: 0x002353AD File Offset: 0x002335AD
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteIResource(this.Resource);
				writer.WriteFirewallGroup(this.OriginalGroup);
				writer.WriteString(ValueSerializer.SerializePreviewValue(MashupEngines.Version1, this.Traits, null, new ValueSerializerOptions?(RemoteFirewallRuleServiceFactory.UpdateFirewallGroupRequestMessage.traitSerializerOptions)));
			}

			// Token: 0x0600AB67 RID: 43879 RVA: 0x002353E8 File Offset: 0x002335E8
			public override void Deserialize(BinaryReader reader)
			{
				this.Resource = reader.ReadIResource();
				this.OriginalGroup = reader.ReadFirewallGroup();
				this.Traits = ValueDeserializer.Deserialize(MashupEngines.Version1, reader.ReadString());
			}

			// Token: 0x040058D8 RID: 22744
			private static readonly ValueSerializerOptions traitSerializerOptions = new ValueSerializerOptions
			{
				MaxValueDepth = 3,
				NestedRecords = true
			};
		}

		// Token: 0x02001A91 RID: 6801
		public sealed class FirewallRuleResponseMessage : BufferedMessage
		{
			// Token: 0x17002B53 RID: 11091
			// (get) Token: 0x0600AB6A RID: 43882 RVA: 0x00235443 File Offset: 0x00233643
			// (set) Token: 0x0600AB6B RID: 43883 RVA: 0x0023544B File Offset: 0x0023364B
			public FirewallGroup2 FirewallGroup { get; set; }

			// Token: 0x0600AB6C RID: 43884 RVA: 0x00235454 File Offset: 0x00233654
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteFirewallGroup(this.FirewallGroup);
			}

			// Token: 0x0600AB6D RID: 43885 RVA: 0x00235462 File Offset: 0x00233662
			public override void Deserialize(BinaryReader reader)
			{
				this.FirewallGroup = reader.ReadFirewallGroup();
			}
		}
	}
}
