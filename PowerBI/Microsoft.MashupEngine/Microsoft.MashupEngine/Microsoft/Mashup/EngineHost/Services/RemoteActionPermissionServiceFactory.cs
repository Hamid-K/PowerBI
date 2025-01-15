using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A50 RID: 6736
	internal class RemoteActionPermissionServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AA3C RID: 43580 RVA: 0x00232880 File Offset: 0x00230A80
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IActionPermissionService actionPermissionService = engineHost.QueryService<IActionPermissionService>();
			proxyInitArgs.WriteBool(actionPermissionService.AreActionsAvailable);
			return new RemoteActionPermissionServiceFactory.Stub(engineHost.QueryService<IActionPermissionService>(), messenger);
		}

		// Token: 0x0600AA3D RID: 43581 RVA: 0x002328AC File Offset: 0x00230AAC
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			bool flag = proxyInitArgs.ReadBool();
			return new RemoteActionPermissionServiceFactory.Proxy(messenger, flag);
		}

		// Token: 0x02001A51 RID: 6737
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IActionPermissionService
		{
			// Token: 0x17002B30 RID: 11056
			// (get) Token: 0x0600AA3F RID: 43583 RVA: 0x002328C7 File Offset: 0x00230AC7
			public bool AreActionsAvailable
			{
				get
				{
					return this.areActionsAvailable;
				}
			}

			// Token: 0x0600AA40 RID: 43584 RVA: 0x002328CF File Offset: 0x00230ACF
			public Proxy(IMessenger messenger, bool areActionsAvailable)
			{
				this.messenger = messenger;
				this.areActionsAvailable = areActionsAvailable;
			}

			// Token: 0x0600AA41 RID: 43585 RVA: 0x002328E8 File Offset: 0x00230AE8
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IActionPermissionService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600AA42 RID: 43586 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0600AA43 RID: 43587 RVA: 0x00232920 File Offset: 0x00230B20
			public bool IsActionPermitted(IResource resource)
			{
				bool permitted;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteActionPermissionServiceFactory.ActionPermissionRequestMessage
					{
						Resource = resource
					});
					permitted = messageChannel.WaitFor<RemoteActionPermissionServiceFactory.ActionPermissionResponseMessage>().Permitted;
				}
				return permitted;
			}

			// Token: 0x0400586E RID: 22638
			private readonly IMessenger messenger;

			// Token: 0x0400586F RID: 22639
			private readonly bool areActionsAvailable;
		}

		// Token: 0x02001A52 RID: 6738
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600AA44 RID: 43588 RVA: 0x00232974 File Offset: 0x00230B74
			public Stub(IActionPermissionService actionPermissionService, IMessenger messenger)
			{
				this.actionPermissionService = actionPermissionService;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteActionPermissionServiceFactory.ActionPermissionRequestMessage>(this.OnActionPermissionRequest));
			}

			// Token: 0x0600AA45 RID: 43589 RVA: 0x002329A4 File Offset: 0x00230BA4
			private void OnActionPermissionRequest(IMessageChannel channel, RemoteActionPermissionServiceFactory.ActionPermissionRequestMessage message)
			{
				bool flag = this.actionPermissionService.IsActionPermitted(message.Resource);
				channel.Post(new RemoteActionPermissionServiceFactory.ActionPermissionResponseMessage
				{
					Permitted = flag
				});
			}

			// Token: 0x0600AA46 RID: 43590 RVA: 0x002329D5 File Offset: 0x00230BD5
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteActionPermissionServiceFactory.ActionPermissionRequestMessage>();
				this.messenger = null;
				this.actionPermissionService = null;
			}

			// Token: 0x04005870 RID: 22640
			private IActionPermissionService actionPermissionService;

			// Token: 0x04005871 RID: 22641
			private IMessenger messenger;
		}

		// Token: 0x02001A53 RID: 6739
		public sealed class ActionPermissionRequestMessage : BufferedMessage
		{
			// Token: 0x17002B31 RID: 11057
			// (get) Token: 0x0600AA47 RID: 43591 RVA: 0x002329F0 File Offset: 0x00230BF0
			// (set) Token: 0x0600AA48 RID: 43592 RVA: 0x002329F8 File Offset: 0x00230BF8
			public IResource Resource { get; set; }

			// Token: 0x0600AA49 RID: 43593 RVA: 0x00232A01 File Offset: 0x00230C01
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteIResource(this.Resource);
			}

			// Token: 0x0600AA4A RID: 43594 RVA: 0x00232A0F File Offset: 0x00230C0F
			public override void Deserialize(BinaryReader reader)
			{
				this.Resource = reader.ReadIResource();
			}
		}

		// Token: 0x02001A54 RID: 6740
		public sealed class ActionPermissionResponseMessage : BufferedMessage
		{
			// Token: 0x17002B32 RID: 11058
			// (get) Token: 0x0600AA4C RID: 43596 RVA: 0x00232A1D File Offset: 0x00230C1D
			// (set) Token: 0x0600AA4D RID: 43597 RVA: 0x00232A25 File Offset: 0x00230C25
			public bool Permitted { get; set; }

			// Token: 0x0600AA4E RID: 43598 RVA: 0x00232A2E File Offset: 0x00230C2E
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteBool(this.Permitted);
			}

			// Token: 0x0600AA4F RID: 43599 RVA: 0x00232A3C File Offset: 0x00230C3C
			public override void Deserialize(BinaryReader reader)
			{
				this.Permitted = reader.ReadBool();
			}
		}
	}
}
