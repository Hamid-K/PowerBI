using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A95 RID: 6805
	internal class RemoteGetStackFrameExtendedInfoFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AB78 RID: 43896 RVA: 0x00235570 File Offset: 0x00233770
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IGetStackFrameExtendedInfo getStackFrameExtendedInfo = engineHost.QueryService<IGetStackFrameExtendedInfo>();
			proxyInitArgs.WriteBool(getStackFrameExtendedInfo != null);
			return new RemoteGetStackFrameExtendedInfoFactory.Stub(getStackFrameExtendedInfo, messenger);
		}

		// Token: 0x0600AB79 RID: 43897 RVA: 0x00235595 File Offset: 0x00233795
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			if (proxyInitArgs.ReadBool())
			{
				return new RemoteGetStackFrameExtendedInfoFactory.Proxy(messenger);
			}
			return EmptyProxy.Instance;
		}

		// Token: 0x040058DE RID: 22750
		private const string LocationStart = "Start";

		// Token: 0x040058DF RID: 22751
		private const string LocationEnd = "End";

		// Token: 0x040058E0 RID: 22752
		private const string PositionRow = "Row";

		// Token: 0x040058E1 RID: 22753
		private const string PositionColumn = "Column";

		// Token: 0x02001A96 RID: 6806
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IGetStackFrameExtendedInfo
		{
			// Token: 0x0600AB7B RID: 43899 RVA: 0x002355AB File Offset: 0x002337AB
			public Proxy(IMessenger messenger)
			{
				this.messenger = messenger;
			}

			// Token: 0x0600AB7C RID: 43900 RVA: 0x002355BC File Offset: 0x002337BC
			public IRecordValue GetStackFrameExtendedInfo(IEngine engine, IValue frameLocation, IValue sectionName)
			{
				IRecordValue recordValue;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteGetStackFrameExtendedInfoFactory.StackFrameExtendedInfoRequestMessage
					{
						FrameLocation = RemoteGetStackFrameExtendedInfoFactory.Proxy.GetTextRange(frameLocation),
						SectionName = sectionName.AsString
					});
					RemoteGetStackFrameExtendedInfoFactory.StackFrameExtendedInfoResponseMessage stackFrameExtendedInfoResponseMessage = messageChannel.WaitFor<RemoteGetStackFrameExtendedInfoFactory.StackFrameExtendedInfoResponseMessage>();
					IKeys keys = engine.Keys(stackFrameExtendedInfoResponseMessage.ExtendedInfo.Keys.ToArray<string>());
					IValue[] array = stackFrameExtendedInfoResponseMessage.ExtendedInfo.Values.Select((string v) => engine.Text(v)).ToArray<ITextValue>();
					IValue[] array2 = array;
					recordValue = engine.Record(keys, array2);
				}
				return recordValue;
			}

			// Token: 0x0600AB7D RID: 43901 RVA: 0x00235680 File Offset: 0x00233880
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IGetStackFrameExtendedInfo))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600AB7E RID: 43902 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0600AB7F RID: 43903 RVA: 0x002356B8 File Offset: 0x002338B8
			private static TextRange GetTextRange(IValue range)
			{
				if (range.IsNull)
				{
					return new TextRange(new TextPosition(0, 0), new TextPosition(0, 0));
				}
				return new TextRange(RemoteGetStackFrameExtendedInfoFactory.Proxy.GetTextPosition(range.AsRecord["Start"]), RemoteGetStackFrameExtendedInfoFactory.Proxy.GetTextPosition(range.AsRecord["End"]));
			}

			// Token: 0x0600AB80 RID: 43904 RVA: 0x00235710 File Offset: 0x00233910
			private static TextPosition GetTextPosition(IValue position)
			{
				if (position.IsNull)
				{
					return new TextPosition(0, 0);
				}
				return new TextPosition(position.AsRecord["Row"].AsNumber.AsInteger32, position.AsRecord["Column"].AsNumber.AsInteger32);
			}

			// Token: 0x040058E2 RID: 22754
			private readonly IMessenger messenger;
		}

		// Token: 0x02001A98 RID: 6808
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600AB83 RID: 43907 RVA: 0x00235774 File Offset: 0x00233974
			public Stub(IGetStackFrameExtendedInfo getStackFrameExtendedInfo, IMessenger messenger)
			{
				this.getStackFrameExtendedInfo = getStackFrameExtendedInfo;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteGetStackFrameExtendedInfoFactory.StackFrameExtendedInfoRequestMessage>(this.OnStackFrameExtendedInfoRequest));
			}

			// Token: 0x0600AB84 RID: 43908 RVA: 0x002357A4 File Offset: 0x002339A4
			private void OnStackFrameExtendedInfoRequest(IMessageChannel channel, RemoteGetStackFrameExtendedInfoFactory.StackFrameExtendedInfoRequestMessage message)
			{
				IEngine version = MashupEngines.Version1;
				IRecordValue recordValue = RemoteGetStackFrameExtendedInfoFactory.Stub.CreateTextRangeRecord(version, message.FrameLocation);
				ITextValue textValue = version.Text(message.SectionName);
				IRecordValue stackFrameExtendedInfo = this.getStackFrameExtendedInfo.GetStackFrameExtendedInfo(version, recordValue, textValue);
				channel.Post(new RemoteGetStackFrameExtendedInfoFactory.StackFrameExtendedInfoResponseMessage
				{
					ExtendedInfo = RemoteGetStackFrameExtendedInfoFactory.Stub.CreateDictionary(stackFrameExtendedInfo)
				});
			}

			// Token: 0x0600AB85 RID: 43909 RVA: 0x002357F7 File Offset: 0x002339F7
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteGetStackFrameExtendedInfoFactory.StackFrameExtendedInfoRequestMessage>();
				this.messenger = null;
				this.getStackFrameExtendedInfo = null;
			}

			// Token: 0x0600AB86 RID: 43910 RVA: 0x00235814 File Offset: 0x00233A14
			private static IRecordValue CreateTextRangeRecord(IEngine engine, TextRange range)
			{
				return engine.Record(engine.Keys(new string[] { "Start", "End" }), new IValue[]
				{
					RemoteGetStackFrameExtendedInfoFactory.Stub.CreateTextPositionRecord(engine, range.Start),
					RemoteGetStackFrameExtendedInfoFactory.Stub.CreateTextPositionRecord(engine, range.End)
				});
			}

			// Token: 0x0600AB87 RID: 43911 RVA: 0x0023586C File Offset: 0x00233A6C
			private static IRecordValue CreateTextPositionRecord(IEngine engine, TextPosition position)
			{
				return engine.Record(engine.Keys(new string[] { "Row", "Column" }), new IValue[]
				{
					engine.Number((double)position.Row),
					engine.Number((double)position.Column)
				});
			}

			// Token: 0x0600AB88 RID: 43912 RVA: 0x002358C4 File Offset: 0x00233AC4
			private static Dictionary<string, string> CreateDictionary(IRecordValue record)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				foreach (string text in record.Keys)
				{
					dictionary.Add(text, record[text].AsString);
				}
				return dictionary;
			}

			// Token: 0x040058E4 RID: 22756
			private IGetStackFrameExtendedInfo getStackFrameExtendedInfo;

			// Token: 0x040058E5 RID: 22757
			private IMessenger messenger;
		}

		// Token: 0x02001A99 RID: 6809
		public sealed class StackFrameExtendedInfoRequestMessage : BufferedMessage
		{
			// Token: 0x17002B54 RID: 11092
			// (get) Token: 0x0600AB89 RID: 43913 RVA: 0x00235924 File Offset: 0x00233B24
			// (set) Token: 0x0600AB8A RID: 43914 RVA: 0x0023592C File Offset: 0x00233B2C
			public TextRange FrameLocation { get; set; }

			// Token: 0x17002B55 RID: 11093
			// (get) Token: 0x0600AB8B RID: 43915 RVA: 0x00235935 File Offset: 0x00233B35
			// (set) Token: 0x0600AB8C RID: 43916 RVA: 0x0023593D File Offset: 0x00233B3D
			public string SectionName { get; set; }

			// Token: 0x0600AB8D RID: 43917 RVA: 0x00235946 File Offset: 0x00233B46
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteTextRange(this.FrameLocation);
				writer.WriteString(this.SectionName);
			}

			// Token: 0x0600AB8E RID: 43918 RVA: 0x00235960 File Offset: 0x00233B60
			public override void Deserialize(BinaryReader reader)
			{
				this.FrameLocation = reader.ReadTextRange();
				this.SectionName = reader.ReadString();
			}
		}

		// Token: 0x02001A9A RID: 6810
		public sealed class StackFrameExtendedInfoResponseMessage : BufferedMessage
		{
			// Token: 0x17002B56 RID: 11094
			// (get) Token: 0x0600AB90 RID: 43920 RVA: 0x0023597A File Offset: 0x00233B7A
			// (set) Token: 0x0600AB91 RID: 43921 RVA: 0x00235982 File Offset: 0x00233B82
			public Dictionary<string, string> ExtendedInfo { get; set; }

			// Token: 0x0600AB92 RID: 43922 RVA: 0x0023598C File Offset: 0x00233B8C
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteDictionary(this.ExtendedInfo, delegate(BinaryWriter w, string k)
				{
					w.WriteString(k);
				}, delegate(BinaryWriter w, string v)
				{
					w.WriteNullableString(v);
				});
			}

			// Token: 0x0600AB93 RID: 43923 RVA: 0x002359E4 File Offset: 0x00233BE4
			public override void Deserialize(BinaryReader reader)
			{
				this.ExtendedInfo = new Dictionary<string, string>();
				reader.ReadDictionary(this.ExtendedInfo, (BinaryReader r) => r.ReadString(), (BinaryReader r) => r.ReadNullableString());
			}
		}
	}
}
