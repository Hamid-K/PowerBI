using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Evaluator.Services;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B14 RID: 6932
	internal class RemoteSourceErrorExceptionServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600ADCF RID: 44495 RVA: 0x0023A5EC File Offset: 0x002387EC
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			ISourceErrorExceptionService sourceErrorExceptionService = engineHost.QueryService<ISourceErrorExceptionService>();
			IGetStackFrameExtendedInfo getStackFrameExtendedInfo = engineHost.QueryService<IGetStackFrameExtendedInfo>();
			return new RemoteSourceErrorExceptionServiceFactory.Stub(sourceErrorExceptionService, getStackFrameExtendedInfo, messenger);
		}

		// Token: 0x0600ADD0 RID: 44496 RVA: 0x0023A60D File Offset: 0x0023880D
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemoteSourceErrorExceptionServiceFactory.Proxy(messenger);
		}

		// Token: 0x02001B15 RID: 6933
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, ISourceErrorExceptionService
		{
			// Token: 0x0600ADD2 RID: 44498 RVA: 0x0023A615 File Offset: 0x00238815
			public Proxy(IMessenger messenger)
			{
				this.messenger = messenger;
			}

			// Token: 0x0600ADD3 RID: 44499 RVA: 0x0023A624 File Offset: 0x00238824
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(ISourceErrorExceptionService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600ADD4 RID: 44500 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0600ADD5 RID: 44501 RVA: 0x0023A65C File Offset: 0x0023885C
			public bool TryGetSourceErrorException(IPartitionKey partitionKey, IError error, out ValueException2 exception)
			{
				bool flag;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					IEngine version = MashupEngines.Version1;
					ITranslateSourceLocation translateSourceLocation = error.Location.Document as ITranslateSourceLocation;
					if (translateSourceLocation != null)
					{
						error = SerializedSourceError.OverrideLocation(error, translateSourceLocation.TranslateSourceLocation(error.Location));
					}
					messageChannel.Post(new RemoteSourceErrorExceptionServiceFactory.SourceErrorExceptionRequestMessage
					{
						PartitionKey = partitionKey,
						Error = error
					});
					string serializedException = messageChannel.WaitFor<RemoteSourceErrorExceptionServiceFactory.SourceErrorExceptionResponseMessage>().SerializedException;
					if (serializedException != null)
					{
						try
						{
							ValueDeserializer.Deserialize(version, serializedException);
						}
						catch (ValueException2 valueException)
						{
							exception = valueException;
							return true;
						}
					}
					exception = null;
					flag = false;
				}
				return flag;
			}

			// Token: 0x040059B4 RID: 22964
			private IMessenger messenger;
		}

		// Token: 0x02001B16 RID: 6934
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600ADD6 RID: 44502 RVA: 0x0023A710 File Offset: 0x00238910
			public Stub(ISourceErrorExceptionService sourceErrorExceptionService, IGetStackFrameExtendedInfo stackFrameExtendedInfo, IMessenger messenger)
			{
				this.sourceErrorExceptionService = sourceErrorExceptionService;
				this.stackFrameExtendedInfo = stackFrameExtendedInfo;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteSourceErrorExceptionServiceFactory.SourceErrorExceptionRequestMessage>(this.OnSourceErrorExceptionRequest));
			}

			// Token: 0x0600ADD7 RID: 44503 RVA: 0x0023A744 File Offset: 0x00238944
			private void OnSourceErrorExceptionRequest(IMessageChannel channel, RemoteSourceErrorExceptionServiceFactory.SourceErrorExceptionRequestMessage message)
			{
				IEngine version = MashupEngines.Version1;
				string text = null;
				ValueException2 valueException;
				if (this.sourceErrorExceptionService.TryGetSourceErrorException(message.PartitionKey, message.Error, out valueException))
				{
					text = ValueSerializer.SerializePreviewException(version, valueException, this.stackFrameExtendedInfo);
				}
				channel.Post(new RemoteSourceErrorExceptionServiceFactory.SourceErrorExceptionResponseMessage
				{
					SerializedException = text
				});
			}

			// Token: 0x0600ADD8 RID: 44504 RVA: 0x0023A794 File Offset: 0x00238994
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteSourceErrorExceptionServiceFactory.SourceErrorExceptionRequestMessage>();
				this.messenger = null;
				this.sourceErrorExceptionService = null;
			}

			// Token: 0x040059B5 RID: 22965
			private ISourceErrorExceptionService sourceErrorExceptionService;

			// Token: 0x040059B6 RID: 22966
			private IGetStackFrameExtendedInfo stackFrameExtendedInfo;

			// Token: 0x040059B7 RID: 22967
			private IMessenger messenger;
		}

		// Token: 0x02001B17 RID: 6935
		public sealed class SourceErrorExceptionRequestMessage : BufferedMessage
		{
			// Token: 0x17002BAD RID: 11181
			// (get) Token: 0x0600ADD9 RID: 44505 RVA: 0x0023A7AF File Offset: 0x002389AF
			// (set) Token: 0x0600ADDA RID: 44506 RVA: 0x0023A7B7 File Offset: 0x002389B7
			public IPartitionKey PartitionKey { get; set; }

			// Token: 0x17002BAE RID: 11182
			// (get) Token: 0x0600ADDB RID: 44507 RVA: 0x0023A7C0 File Offset: 0x002389C0
			// (set) Token: 0x0600ADDC RID: 44508 RVA: 0x0023A7C8 File Offset: 0x002389C8
			public IError Error { get; set; }

			// Token: 0x0600ADDD RID: 44509 RVA: 0x0023A7D1 File Offset: 0x002389D1
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteNullable(this.PartitionKey, delegate(BinaryWriter w, IPartitionKey p)
				{
					w.WriteIPartitionKey(p);
				});
				writer.WriteIError(this.Error);
			}

			// Token: 0x0600ADDE RID: 44510 RVA: 0x0023A80A File Offset: 0x00238A0A
			public override void Deserialize(BinaryReader reader)
			{
				this.PartitionKey = reader.ReadNullable((BinaryReader r) => r.ReadIPartitionKey());
				this.Error = reader.ReadIError();
			}
		}

		// Token: 0x02001B19 RID: 6937
		public sealed class SourceErrorExceptionResponseMessage : BufferedMessage
		{
			// Token: 0x17002BAF RID: 11183
			// (get) Token: 0x0600ADE4 RID: 44516 RVA: 0x0023A84F File Offset: 0x00238A4F
			// (set) Token: 0x0600ADE5 RID: 44517 RVA: 0x0023A857 File Offset: 0x00238A57
			public string SerializedException { get; set; }

			// Token: 0x0600ADE6 RID: 44518 RVA: 0x0023A860 File Offset: 0x00238A60
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteNullableString(this.SerializedException);
			}

			// Token: 0x0600ADE7 RID: 44519 RVA: 0x0023A86E File Offset: 0x00238A6E
			public override void Deserialize(BinaryReader reader)
			{
				this.SerializedException = reader.ReadNullableString();
			}
		}
	}
}
