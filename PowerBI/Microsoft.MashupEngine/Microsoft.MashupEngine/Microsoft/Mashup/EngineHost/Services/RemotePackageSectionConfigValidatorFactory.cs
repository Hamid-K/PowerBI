using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001AB6 RID: 6838
	internal class RemotePackageSectionConfigValidatorFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AC15 RID: 44053 RVA: 0x002368FC File Offset: 0x00234AFC
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IPackageSectionConfigValidator packageSectionConfigValidator = engineHost.QueryService<IPackageSectionConfigValidator>();
			return new RemotePackageSectionConfigValidatorFactory.Stub(engineHost, packageSectionConfigValidator, messenger);
		}

		// Token: 0x0600AC16 RID: 44054 RVA: 0x00236918 File Offset: 0x00234B18
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemotePackageSectionConfigValidatorFactory.Proxy(messenger);
		}

		// Token: 0x02001AB7 RID: 6839
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IPackageSectionConfigValidator
		{
			// Token: 0x0600AC18 RID: 44056 RVA: 0x00236920 File Offset: 0x00234B20
			public Proxy(IMessenger messenger)
			{
				this.messenger = messenger;
			}

			// Token: 0x0600AC19 RID: 44057 RVA: 0x00236930 File Offset: 0x00234B30
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IPackageSectionConfigValidator))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600AC1A RID: 44058 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0600AC1B RID: 44059 RVA: 0x00236968 File Offset: 0x00234B68
			public void ValidatePackageSectionConfig(IPackageSectionConfig packageSectionConfig)
			{
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemotePackageSectionConfigValidatorFactory.ValidatePackageConfigRequestMessage
					{
						PackageConfig = (PackageConfig)packageSectionConfig
					});
					messageChannel.WaitFor<RemotePackageSectionConfigValidatorFactory.ValidatePackageConfigResponseMessage>();
				}
			}

			// Token: 0x0400590F RID: 22799
			private readonly IMessenger messenger;
		}

		// Token: 0x02001AB8 RID: 6840
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600AC1C RID: 44060 RVA: 0x002369BC File Offset: 0x00234BBC
			public Stub(IEngineHost engineHost, IPackageSectionConfigValidator configValidator, IMessenger messenger)
			{
				this.engineHost = engineHost;
				this.configValidator = configValidator;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePackageSectionConfigValidatorFactory.ValidatePackageConfigRequestMessage>(this.OnValidatePackageConfigRequest));
			}

			// Token: 0x0600AC1D RID: 44061 RVA: 0x002369F0 File Offset: 0x00234BF0
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemotePackageSectionConfigValidatorFactory.ValidatePackageConfigRequestMessage>();
				this.messenger = null;
				this.configValidator = null;
				this.engineHost = null;
			}

			// Token: 0x0600AC1E RID: 44062 RVA: 0x00236A14 File Offset: 0x00234C14
			private void OnValidatePackageConfigRequest(IMessageChannel channel, RemotePackageSectionConfigValidatorFactory.ValidatePackageConfigRequestMessage message)
			{
				EvaluationHost.ReportExceptions("RemotePackageSectionConfigValidatorFactory/Stub/OnValidatePackageConfigRequest", this.engineHost, channel, delegate
				{
					this.configValidator.ValidatePackageSectionConfig(PackageConfigPackageSectionConfig.New(message.PackageConfig, null, default(SegmentedString)));
					channel.Post(new RemotePackageSectionConfigValidatorFactory.ValidatePackageConfigResponseMessage());
				});
			}

			// Token: 0x04005910 RID: 22800
			private IEngineHost engineHost;

			// Token: 0x04005911 RID: 22801
			private IPackageSectionConfigValidator configValidator;

			// Token: 0x04005912 RID: 22802
			private IMessenger messenger;
		}

		// Token: 0x02001ABA RID: 6842
		public sealed class ValidatePackageConfigRequestMessage : BufferedMessage
		{
			// Token: 0x17002B66 RID: 11110
			// (get) Token: 0x0600AC21 RID: 44065 RVA: 0x00236AA7 File Offset: 0x00234CA7
			// (set) Token: 0x0600AC22 RID: 44066 RVA: 0x00236AAF File Offset: 0x00234CAF
			public PackageConfig PackageConfig { get; set; }

			// Token: 0x0600AC23 RID: 44067 RVA: 0x00236AB8 File Offset: 0x00234CB8
			public override void Serialize(BinaryWriter writer)
			{
				writer.WritePackageConfig(this.PackageConfig);
			}

			// Token: 0x0600AC24 RID: 44068 RVA: 0x00236AC6 File Offset: 0x00234CC6
			public override void Deserialize(BinaryReader reader)
			{
				this.PackageConfig = reader.ReadPackageConfig();
			}
		}

		// Token: 0x02001ABB RID: 6843
		public sealed class ValidatePackageConfigResponseMessage : BufferedMessage
		{
			// Token: 0x0600AC26 RID: 44070 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x0600AC27 RID: 44071 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Deserialize(BinaryReader reader)
			{
			}
		}
	}
}
