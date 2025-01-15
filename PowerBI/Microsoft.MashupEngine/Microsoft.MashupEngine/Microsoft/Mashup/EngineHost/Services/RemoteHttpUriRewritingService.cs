using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A9C RID: 6812
	internal class RemoteHttpUriRewritingService : IRemoteServiceFactory
	{
		// Token: 0x0600AB9B RID: 43931 RVA: 0x00235A54 File Offset: 0x00233C54
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IHttpUriRewritingService httpUriRewritingService = engineHost.QueryService<IHttpUriRewritingService>();
			if (httpUriRewritingService == null)
			{
				proxyInitArgs.Write(0);
				return EmptyStub.Instance;
			}
			if (httpUriRewritingService is HttpHostRewritingService)
			{
				Uri[,] map = ((HttpHostRewritingService)httpUriRewritingService).Map;
				int length = map.GetLength(0);
				proxyInitArgs.Write(1);
				proxyInitArgs.Write(length);
				for (int i = 0; i < length; i++)
				{
					proxyInitArgs.Write(map[i, 0].AbsoluteUri);
					proxyInitArgs.Write(map[i, 1].AbsoluteUri);
				}
				return EmptyStub.Instance;
			}
			proxyInitArgs.Write(2);
			return new RemoteHttpUriRewritingService.Stub(engineHost, httpUriRewritingService, messenger);
		}

		// Token: 0x0600AB9C RID: 43932 RVA: 0x00235AEC File Offset: 0x00233CEC
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			switch (proxyInitArgs.ReadInt32())
			{
			case 0:
				return EmptyProxy.Instance;
			case 1:
				return new EngineHostServiceProxy(new SimpleEngineHost<IHttpUriRewritingService>(this.InitHostRewriteService(proxyInitArgs)));
			case 2:
				return new RemoteHttpUriRewritingService.Proxy(messenger);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600AB9D RID: 43933 RVA: 0x00235B38 File Offset: 0x00233D38
		private IHttpUriRewritingService InitHostRewriteService(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			Uri[,] array = new Uri[num, 2];
			for (int i = 0; i < num; i++)
			{
				array[i, 0] = new Uri(reader.ReadString());
				array[i, 1] = new Uri(reader.ReadString());
			}
			return new HttpHostRewritingService(array);
		}

		// Token: 0x040058EE RID: 22766
		private const int noService = 0;

		// Token: 0x040058EF RID: 22767
		private const int hostRewriteService = 1;

		// Token: 0x040058F0 RID: 22768
		private const int marshaledService = 2;

		// Token: 0x02001A9D RID: 6813
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IHttpUriRewritingService
		{
			// Token: 0x0600AB9F RID: 43935 RVA: 0x00235B8C File Offset: 0x00233D8C
			public Proxy(IMessenger messenger)
			{
				this.messenger = messenger;
			}

			// Token: 0x0600ABA0 RID: 43936 RVA: 0x00235B9C File Offset: 0x00233D9C
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IHttpUriRewritingService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600ABA1 RID: 43937 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0600ABA2 RID: 43938 RVA: 0x00235BD4 File Offset: 0x00233DD4
			public bool TryRewriteRequestUri(Uri requestUri, out Uri rewrittenUri)
			{
				bool flag;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteHttpUriRewritingService.TryRewriteUriRequestMessage
					{
						Uri = requestUri,
						IsResponse = false
					});
					rewrittenUri = messageChannel.WaitFor<RemoteHttpUriRewritingService.TryRewriteUriResponseMessage>().Uri;
					flag = rewrittenUri != null;
				}
				return flag;
			}

			// Token: 0x0600ABA3 RID: 43939 RVA: 0x00235C3C File Offset: 0x00233E3C
			public bool TryRewriteResponseUri(Uri responseUri, out Uri rewrittenUri)
			{
				bool flag;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteHttpUriRewritingService.TryRewriteUriRequestMessage
					{
						Uri = responseUri,
						IsResponse = true
					});
					rewrittenUri = messageChannel.WaitFor<RemoteHttpUriRewritingService.TryRewriteUriResponseMessage>().Uri;
					flag = rewrittenUri != null;
				}
				return flag;
			}

			// Token: 0x040058F1 RID: 22769
			private readonly IMessenger messenger;
		}

		// Token: 0x02001A9E RID: 6814
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600ABA4 RID: 43940 RVA: 0x00235CA4 File Offset: 0x00233EA4
			public Stub(IEngineHost engineHost, IHttpUriRewritingService uriRewritingService, IMessenger messenger)
			{
				this.engineHost = engineHost;
				this.uriRewritingService = uriRewritingService;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteHttpUriRewritingService.TryRewriteUriRequestMessage>(this.OnRewriteUriRequest));
			}

			// Token: 0x0600ABA5 RID: 43941 RVA: 0x00235CD8 File Offset: 0x00233ED8
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteHttpUriRewritingService.TryRewriteUriRequestMessage>();
				this.messenger = null;
				this.uriRewritingService = null;
				this.engineHost = null;
			}

			// Token: 0x0600ABA6 RID: 43942 RVA: 0x00235CFC File Offset: 0x00233EFC
			private void OnRewriteUriRequest(IMessageChannel channel, RemoteHttpUriRewritingService.TryRewriteUriRequestMessage message)
			{
				EvaluationHost.ReportExceptions("RemotePackageSectionConfigValidatorFactory/Stub/OnValidatePackageConfigRequest", this.engineHost, channel, delegate
				{
					Uri uri;
					bool flag = (message.IsResponse ? this.uriRewritingService.TryRewriteResponseUri(message.Uri, out uri) : this.uriRewritingService.TryRewriteRequestUri(message.Uri, out uri));
					channel.Post(new RemoteHttpUriRewritingService.TryRewriteUriResponseMessage
					{
						Uri = (flag ? uri : null)
					});
				});
			}

			// Token: 0x040058F2 RID: 22770
			private IEngineHost engineHost;

			// Token: 0x040058F3 RID: 22771
			private IHttpUriRewritingService uriRewritingService;

			// Token: 0x040058F4 RID: 22772
			private IMessenger messenger;
		}

		// Token: 0x02001AA0 RID: 6816
		public sealed class TryRewriteUriRequestMessage : BufferedMessage
		{
			// Token: 0x17002B57 RID: 11095
			// (get) Token: 0x0600ABA9 RID: 43945 RVA: 0x00235DBC File Offset: 0x00233FBC
			// (set) Token: 0x0600ABAA RID: 43946 RVA: 0x00235DC4 File Offset: 0x00233FC4
			public Uri Uri { get; set; }

			// Token: 0x17002B58 RID: 11096
			// (get) Token: 0x0600ABAB RID: 43947 RVA: 0x00235DCD File Offset: 0x00233FCD
			// (set) Token: 0x0600ABAC RID: 43948 RVA: 0x00235DD5 File Offset: 0x00233FD5
			public bool IsResponse { get; set; }

			// Token: 0x0600ABAD RID: 43949 RVA: 0x00235DDE File Offset: 0x00233FDE
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteString(this.Uri.AbsoluteUri);
				writer.Write(this.IsResponse);
			}

			// Token: 0x0600ABAE RID: 43950 RVA: 0x00235DFD File Offset: 0x00233FFD
			public override void Deserialize(BinaryReader reader)
			{
				this.Uri = new Uri(reader.ReadString());
				this.IsResponse = reader.ReadBoolean();
			}
		}

		// Token: 0x02001AA1 RID: 6817
		public sealed class TryRewriteUriResponseMessage : BufferedMessage
		{
			// Token: 0x17002B59 RID: 11097
			// (get) Token: 0x0600ABB0 RID: 43952 RVA: 0x00235E1C File Offset: 0x0023401C
			// (set) Token: 0x0600ABB1 RID: 43953 RVA: 0x00235E24 File Offset: 0x00234024
			public Uri Uri { get; set; }

			// Token: 0x0600ABB2 RID: 43954 RVA: 0x00235E2D File Offset: 0x0023402D
			public override void Serialize(BinaryWriter writer)
			{
				Uri uri = this.Uri;
				writer.WriteNullableString((uri != null) ? uri.AbsoluteUri : null);
			}

			// Token: 0x0600ABB3 RID: 43955 RVA: 0x00235E48 File Offset: 0x00234048
			public override void Deserialize(BinaryReader reader)
			{
				string text = reader.ReadNullableString();
				this.Uri = ((text != null) ? new Uri(text) : null);
			}
		}
	}
}
