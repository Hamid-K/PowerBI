using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A85 RID: 6789
	internal class RemoteFeatureLoggingServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AB32 RID: 43826 RVA: 0x00234ECF File Offset: 0x002330CF
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return new RemoteFeatureLoggingServiceFactory.Stub(engineHost.QueryService<IFeatureLoggingService>(), messenger);
		}

		// Token: 0x0600AB33 RID: 43827 RVA: 0x00234EDD File Offset: 0x002330DD
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemoteFeatureLoggingServiceFactory.Proxy(messenger);
		}

		// Token: 0x02001A86 RID: 6790
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IFeatureLoggingService
		{
			// Token: 0x0600AB35 RID: 43829 RVA: 0x00234EE5 File Offset: 0x002330E5
			public Proxy(IMessenger messenger)
			{
				this.messenger = messenger;
				this.loggedFeatures = new HashSet<string>();
			}

			// Token: 0x0600AB36 RID: 43830 RVA: 0x00234F00 File Offset: 0x00233100
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IFeatureLoggingService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600AB37 RID: 43831 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0600AB38 RID: 43832 RVA: 0x00234F38 File Offset: 0x00233138
			public void LogFeature(string feature)
			{
				bool flag = false;
				HashSet<string> hashSet = this.loggedFeatures;
				lock (hashSet)
				{
					flag = this.loggedFeatures.Add(feature);
				}
				if (flag)
				{
					using (IMessageChannel messageChannel = this.messenger.CreateChannel())
					{
						messageChannel.Post(new RemoteFeatureLoggingServiceFactory.LogFeatureMessage
						{
							Feature = feature
						});
					}
				}
			}

			// Token: 0x0600AB39 RID: 43833 RVA: 0x00234FBC File Offset: 0x002331BC
			public IEnumerable<string> GetLoggedFeatures()
			{
				IEnumerable<string> features;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteFeatureLoggingServiceFactory.GetLoggedFeaturesRequestMessage());
					features = messageChannel.WaitFor<RemoteFeatureLoggingServiceFactory.GetLoggedFeaturesResponseMessage>().Features;
				}
				return features;
			}

			// Token: 0x040058CB RID: 22731
			private readonly IMessenger messenger;

			// Token: 0x040058CC RID: 22732
			private readonly HashSet<string> loggedFeatures;
		}

		// Token: 0x02001A87 RID: 6791
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600AB3A RID: 43834 RVA: 0x0023500C File Offset: 0x0023320C
			public Stub(IFeatureLoggingService featureLoggingService, IMessenger messenger)
			{
				this.featureLoggingService = featureLoggingService;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteFeatureLoggingServiceFactory.LogFeatureMessage>(this.OnLogFeature));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteFeatureLoggingServiceFactory.GetLoggedFeaturesRequestMessage>(this.OnGetLoggedFeatures));
			}

			// Token: 0x0600AB3B RID: 43835 RVA: 0x0023505B File Offset: 0x0023325B
			private void OnLogFeature(IMessageChannel channel, RemoteFeatureLoggingServiceFactory.LogFeatureMessage message)
			{
				this.featureLoggingService.LogFeature(message.Feature);
			}

			// Token: 0x0600AB3C RID: 43836 RVA: 0x0023506E File Offset: 0x0023326E
			private void OnGetLoggedFeatures(IMessageChannel channel, RemoteFeatureLoggingServiceFactory.GetLoggedFeaturesRequestMessage message)
			{
				channel.Post(new RemoteFeatureLoggingServiceFactory.GetLoggedFeaturesResponseMessage
				{
					Features = this.featureLoggingService.GetLoggedFeatures().ToArray<string>()
				});
			}

			// Token: 0x0600AB3D RID: 43837 RVA: 0x00235091 File Offset: 0x00233291
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteFeatureLoggingServiceFactory.LogFeatureMessage>();
				this.messenger.RemoveHandler<RemoteFeatureLoggingServiceFactory.GetLoggedFeaturesRequestMessage>();
				this.messenger = null;
				this.featureLoggingService = null;
			}

			// Token: 0x040058CD RID: 22733
			private IFeatureLoggingService featureLoggingService;

			// Token: 0x040058CE RID: 22734
			private IMessenger messenger;
		}

		// Token: 0x02001A88 RID: 6792
		public sealed class LogFeatureMessage : BufferedMessage
		{
			// Token: 0x17002B4D RID: 11085
			// (get) Token: 0x0600AB3E RID: 43838 RVA: 0x002350B7 File Offset: 0x002332B7
			// (set) Token: 0x0600AB3F RID: 43839 RVA: 0x002350BF File Offset: 0x002332BF
			public string Feature { get; set; }

			// Token: 0x0600AB40 RID: 43840 RVA: 0x002350C8 File Offset: 0x002332C8
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteString(this.Feature);
			}

			// Token: 0x0600AB41 RID: 43841 RVA: 0x002350D6 File Offset: 0x002332D6
			public override void Deserialize(BinaryReader reader)
			{
				this.Feature = reader.ReadString();
			}
		}

		// Token: 0x02001A89 RID: 6793
		public sealed class GetLoggedFeaturesRequestMessage : BufferedMessage
		{
			// Token: 0x0600AB43 RID: 43843 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x0600AB44 RID: 43844 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Deserialize(BinaryReader reader)
			{
			}
		}

		// Token: 0x02001A8A RID: 6794
		public sealed class GetLoggedFeaturesResponseMessage : BufferedMessage
		{
			// Token: 0x17002B4E RID: 11086
			// (get) Token: 0x0600AB46 RID: 43846 RVA: 0x002350E4 File Offset: 0x002332E4
			// (set) Token: 0x0600AB47 RID: 43847 RVA: 0x002350EC File Offset: 0x002332EC
			public string[] Features { get; set; }

			// Token: 0x0600AB48 RID: 43848 RVA: 0x002350F5 File Offset: 0x002332F5
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteArray(this.Features, delegate(BinaryWriter w, string s)
				{
					w.WriteString(s);
				});
			}

			// Token: 0x0600AB49 RID: 43849 RVA: 0x00235122 File Offset: 0x00233322
			public override void Deserialize(BinaryReader reader)
			{
				this.Features = reader.ReadArray((BinaryReader r) => r.ReadString());
			}
		}
	}
}
