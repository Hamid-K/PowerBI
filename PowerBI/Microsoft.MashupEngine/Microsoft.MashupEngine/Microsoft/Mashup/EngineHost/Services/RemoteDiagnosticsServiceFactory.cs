using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Serialization;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A74 RID: 6772
	internal class RemoteDiagnosticsServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AACC RID: 43724 RVA: 0x00233CB0 File Offset: 0x00231EB0
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IDiagnosticsService diagnosticsService = engineHost.QueryService<IDiagnosticsService>();
			if (diagnosticsService == null || diagnosticsService.EnabledChannels.Count == 0)
			{
				proxyInitArgs.Write(0);
				return EmptyStub.Instance;
			}
			RemoteDiagnosticsServiceFactory.Stub stub = new RemoteDiagnosticsServiceFactory.Stub(diagnosticsService, messenger);
			stub.Initialize(proxyInitArgs);
			return stub;
		}

		// Token: 0x0600AACD RID: 43725 RVA: 0x00233CF0 File Offset: 0x00231EF0
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			int num = proxyInitArgs.ReadInt32();
			if (num == 0)
			{
				return EmptyProxy.Instance;
			}
			RemoteDiagnosticsServiceFactory.Proxy proxy = new RemoteDiagnosticsServiceFactory.Proxy(messenger);
			proxy.Initialize(proxyInitArgs, num);
			return proxy;
		}

		// Token: 0x02001A75 RID: 6773
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IDiagnosticsService
		{
			// Token: 0x0600AACF RID: 43727 RVA: 0x00233D1B File Offset: 0x00231F1B
			public Proxy(IMessenger messenger)
			{
				this.enabledChannels = new HashSet<string>();
				this.messenger = messenger;
				this.symbolInterner = new RemoteDiagnosticsServiceFactory.SymbolInterner();
			}

			// Token: 0x17002B3E RID: 11070
			// (get) Token: 0x0600AAD0 RID: 43728 RVA: 0x00233D40 File Offset: 0x00231F40
			public HashSet<string> EnabledChannels
			{
				get
				{
					return this.enabledChannels;
				}
			}

			// Token: 0x0600AAD1 RID: 43729 RVA: 0x00233D48 File Offset: 0x00231F48
			public void Initialize(BinaryReader reader, int count)
			{
				for (int i = 0; i < count; i++)
				{
					string text = this.symbolInterner.Read(reader);
					this.enabledChannels.Add(text);
				}
			}

			// Token: 0x0600AAD2 RID: 43730 RVA: 0x00233D7C File Offset: 0x00231F7C
			public void Emit(string channelName, string eventName, DateTime eventTime, IResource resource, Dictionary<string, object> properties)
			{
				RemoteDiagnosticsServiceFactory.SymbolInterner symbolInterner = this.symbolInterner;
				RemoteDiagnosticsServiceFactory.EmitDiagnosticMessage emitDiagnosticMessage;
				lock (symbolInterner)
				{
					int num;
					if (!this.symbolInterner.TryLookup(channelName, out num))
					{
						return;
					}
					emitDiagnosticMessage = new RemoteDiagnosticsServiceFactory.EmitDiagnosticMessage();
					emitDiagnosticMessage.Set(this.symbolInterner, num, eventName, eventTime, resource, properties);
				}
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(emitDiagnosticMessage);
				}
			}

			// Token: 0x0600AAD3 RID: 43731 RVA: 0x00233E10 File Offset: 0x00232010
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IDiagnosticsService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600AAD4 RID: 43732 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x040058A2 RID: 22690
			private readonly HashSet<string> enabledChannels;

			// Token: 0x040058A3 RID: 22691
			private readonly IMessenger messenger;

			// Token: 0x040058A4 RID: 22692
			private readonly RemoteDiagnosticsServiceFactory.SymbolInterner symbolInterner;
		}

		// Token: 0x02001A76 RID: 6774
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600AAD5 RID: 43733 RVA: 0x00233E48 File Offset: 0x00232048
			public Stub(IDiagnosticsService diagnosticsService, IMessenger messenger)
			{
				this.diagnosticsService = diagnosticsService;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteDiagnosticsServiceFactory.EmitDiagnosticMessage>(this.OnEmit));
				this.symbolInterner = new RemoteDiagnosticsServiceFactory.SymbolInterner();
			}

			// Token: 0x0600AAD6 RID: 43734 RVA: 0x00233E80 File Offset: 0x00232080
			public void Initialize(BinaryWriter writer)
			{
				writer.WriteInt32(this.diagnosticsService.EnabledChannels.Count);
				foreach (string text in this.diagnosticsService.EnabledChannels)
				{
					this.symbolInterner.Write(writer, text);
				}
			}

			// Token: 0x0600AAD7 RID: 43735 RVA: 0x00233EF4 File Offset: 0x002320F4
			private void OnEmit(IMessageChannel channel, RemoteDiagnosticsServiceFactory.EmitDiagnosticMessage message)
			{
				message.Emit(this.diagnosticsService, this.symbolInterner);
			}

			// Token: 0x0600AAD8 RID: 43736 RVA: 0x00233F08 File Offset: 0x00232108
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteDiagnosticsServiceFactory.EmitDiagnosticMessage>();
				this.messenger = null;
				this.diagnosticsService = null;
				this.symbolInterner = null;
			}

			// Token: 0x040058A5 RID: 22693
			private IDiagnosticsService diagnosticsService;

			// Token: 0x040058A6 RID: 22694
			private IMessenger messenger;

			// Token: 0x040058A7 RID: 22695
			private RemoteDiagnosticsServiceFactory.SymbolInterner symbolInterner;
		}

		// Token: 0x02001A77 RID: 6775
		private sealed class EmitDiagnosticMessage : BufferedMessage
		{
			// Token: 0x17002B3F RID: 11071
			// (get) Token: 0x0600AAD9 RID: 43737 RVA: 0x00233F2A File Offset: 0x0023212A
			// (set) Token: 0x0600AADA RID: 43738 RVA: 0x00233F32 File Offset: 0x00232132
			public RemoteDiagnosticsServiceFactory.AddedSymbols AddedSymbols { get; set; }

			// Token: 0x17002B40 RID: 11072
			// (get) Token: 0x0600AADB RID: 43739 RVA: 0x00233F3B File Offset: 0x0023213B
			// (set) Token: 0x0600AADC RID: 43740 RVA: 0x00233F43 File Offset: 0x00232143
			public int ChannelName { get; set; }

			// Token: 0x17002B41 RID: 11073
			// (get) Token: 0x0600AADD RID: 43741 RVA: 0x00233F4C File Offset: 0x0023214C
			// (set) Token: 0x0600AADE RID: 43742 RVA: 0x00233F54 File Offset: 0x00232154
			public int EventName { get; set; }

			// Token: 0x17002B42 RID: 11074
			// (get) Token: 0x0600AADF RID: 43743 RVA: 0x00233F5D File Offset: 0x0023215D
			// (set) Token: 0x0600AAE0 RID: 43744 RVA: 0x00233F65 File Offset: 0x00232165
			public DateTime EventTime { get; set; }

			// Token: 0x17002B43 RID: 11075
			// (get) Token: 0x0600AAE1 RID: 43745 RVA: 0x00233F6E File Offset: 0x0023216E
			// (set) Token: 0x0600AAE2 RID: 43746 RVA: 0x00233F76 File Offset: 0x00232176
			public int ResourceKind { get; set; }

			// Token: 0x17002B44 RID: 11076
			// (get) Token: 0x0600AAE3 RID: 43747 RVA: 0x00233F7F File Offset: 0x0023217F
			// (set) Token: 0x0600AAE4 RID: 43748 RVA: 0x00233F87 File Offset: 0x00232187
			public string ResourcePath { get; set; }

			// Token: 0x17002B45 RID: 11077
			// (get) Token: 0x0600AAE5 RID: 43749 RVA: 0x00233F90 File Offset: 0x00232190
			// (set) Token: 0x0600AAE6 RID: 43750 RVA: 0x00233F98 File Offset: 0x00232198
			public KeyValuePair<int, object>[] Properties { get; set; }

			// Token: 0x0600AAE7 RID: 43751 RVA: 0x00233FA4 File Offset: 0x002321A4
			public void Set(RemoteDiagnosticsServiceFactory.SymbolInterner interner, int channelId, string eventName, DateTime eventTime, IResource resource, Dictionary<string, object> properties)
			{
				int count = interner.Count;
				this.ChannelName = channelId;
				this.EventName = interner.Intern(eventName);
				this.EventTime = eventTime;
				this.ResourceKind = interner.Intern((resource != null) ? resource.Kind : null);
				this.ResourcePath = ((resource != null) ? resource.Path : null);
				this.Properties = new KeyValuePair<int, object>[properties.Count];
				int num = 0;
				foreach (KeyValuePair<string, object> keyValuePair in properties)
				{
					this.Properties[num++] = new KeyValuePair<int, object>(interner.Intern(keyValuePair.Key), keyValuePair.Value);
				}
				this.AddedSymbols = interner.Since(count);
			}

			// Token: 0x0600AAE8 RID: 43752 RVA: 0x00234088 File Offset: 0x00232288
			public void Emit(IDiagnosticsService service, RemoteDiagnosticsServiceFactory.SymbolInterner interner)
			{
				string text;
				string text2;
				IResource resource;
				Dictionary<string, object> dictionary;
				lock (interner)
				{
					interner.Add(this.AddedSymbols);
					text = interner[this.ChannelName];
					text2 = interner[this.EventName];
					string text3 = interner[this.ResourceKind];
					resource = ((text3 == null) ? null : new Resource(text3, this.ResourcePath, this.ResourcePath));
					dictionary = new Dictionary<string, object>(this.Properties.Length);
					for (int i = 0; i < this.Properties.Length; i++)
					{
						dictionary[interner[this.Properties[i].Key]] = this.Properties[i].Value;
					}
				}
				service.Emit(text, text2, this.EventTime, resource, dictionary);
			}

			// Token: 0x0600AAE9 RID: 43753 RVA: 0x00234178 File Offset: 0x00232378
			public override void Serialize(BinaryWriter writer)
			{
				this.AddedSymbols.Serialize(writer);
				writer.WriteInt32(this.ChannelName);
				writer.WriteInt32(this.EventName);
				writer.WriteDateTime(this.EventTime);
				writer.WriteInt32(this.ResourceKind);
				if (this.ResourceKind != 0)
				{
					writer.WriteString(this.ResourcePath);
				}
				ObjectWriter objectWriter = new ObjectWriter(writer);
				writer.WriteInt32(this.Properties.Length);
				for (int i = 0; i < this.Properties.Length; i++)
				{
					writer.WriteInt32(this.Properties[i].Key);
					objectWriter.WriteObject(this.Properties[i].Value);
				}
			}

			// Token: 0x0600AAEA RID: 43754 RVA: 0x00234230 File Offset: 0x00232430
			public override void Deserialize(BinaryReader reader)
			{
				this.AddedSymbols = new RemoteDiagnosticsServiceFactory.AddedSymbols(reader);
				this.ChannelName = reader.ReadInt32();
				this.EventName = reader.ReadInt32();
				this.EventTime = reader.ReadDateTime();
				this.ResourceKind = reader.ReadInt32();
				if (this.ResourceKind != 0)
				{
					this.ResourcePath = reader.ReadString();
				}
				ObjectReader objectReader = new ObjectReader(reader);
				int num = reader.ReadInt32();
				this.Properties = new KeyValuePair<int, object>[num];
				for (int i = 0; i < num; i++)
				{
					this.Properties[i] = new KeyValuePair<int, object>(reader.ReadInt32(), objectReader.ReadObject());
				}
			}
		}

		// Token: 0x02001A78 RID: 6776
		private struct AddedSymbols
		{
			// Token: 0x17002B46 RID: 11078
			// (get) Token: 0x0600AAEC RID: 43756 RVA: 0x002342D2 File Offset: 0x002324D2
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x17002B47 RID: 11079
			// (get) Token: 0x0600AAED RID: 43757 RVA: 0x002342DA File Offset: 0x002324DA
			public int FirstSymbol
			{
				get
				{
					return this.firstSymbol;
				}
			}

			// Token: 0x17002B48 RID: 11080
			// (get) Token: 0x0600AAEE RID: 43758 RVA: 0x002342E2 File Offset: 0x002324E2
			public int LastSymbol
			{
				get
				{
					return this.firstSymbol + this.count - 1;
				}
			}

			// Token: 0x17002B49 RID: 11081
			public string this[int index]
			{
				get
				{
					return this.symbols[index + this.position];
				}
			}

			// Token: 0x0600AAF0 RID: 43760 RVA: 0x00234308 File Offset: 0x00232508
			public AddedSymbols(List<string> symbols, int start)
			{
				this.symbols = symbols;
				this.firstSymbol = start;
				this.position = start;
				this.count = symbols.Count - start;
			}

			// Token: 0x0600AAF1 RID: 43761 RVA: 0x00234330 File Offset: 0x00232530
			public AddedSymbols(BinaryReader reader)
			{
				this.position = 0;
				this.firstSymbol = reader.ReadInt32();
				this.count = reader.ReadInt32();
				this.symbols = new string[this.count];
				for (int i = 0; i < this.count; i++)
				{
					this.symbols[i] = reader.ReadString();
				}
			}

			// Token: 0x0600AAF2 RID: 43762 RVA: 0x00234390 File Offset: 0x00232590
			public void Serialize(BinaryWriter writer)
			{
				writer.WriteInt32(this.firstSymbol);
				writer.WriteInt32(this.count);
				for (int i = 0; i < this.count; i++)
				{
					writer.WriteString(this.symbols[i + this.position]);
				}
			}

			// Token: 0x040058AF RID: 22703
			private readonly IList<string> symbols;

			// Token: 0x040058B0 RID: 22704
			private readonly int firstSymbol;

			// Token: 0x040058B1 RID: 22705
			private readonly int position;

			// Token: 0x040058B2 RID: 22706
			private readonly int count;
		}

		// Token: 0x02001A79 RID: 6777
		private sealed class SymbolInterner
		{
			// Token: 0x0600AAF3 RID: 43763 RVA: 0x002343DF File Offset: 0x002325DF
			public SymbolInterner()
			{
				this.symbols = new Dictionary<string, int>();
				this.strings = new List<string>();
				this.strings.Add(null);
			}

			// Token: 0x17002B4A RID: 11082
			// (get) Token: 0x0600AAF4 RID: 43764 RVA: 0x00234409 File Offset: 0x00232609
			public int Count
			{
				get
				{
					return this.strings.Count;
				}
			}

			// Token: 0x17002B4B RID: 11083
			public string this[int index]
			{
				get
				{
					return this.strings[index];
				}
			}

			// Token: 0x0600AAF6 RID: 43766 RVA: 0x00234424 File Offset: 0x00232624
			public bool TryLookup(string symbol, out int index)
			{
				return this.symbols.TryGetValue(symbol, out index);
			}

			// Token: 0x0600AAF7 RID: 43767 RVA: 0x00234434 File Offset: 0x00232634
			public int Intern(string symbol)
			{
				if (symbol == null)
				{
					return 0;
				}
				bool flag;
				return this.InternValue(symbol, out flag);
			}

			// Token: 0x0600AAF8 RID: 43768 RVA: 0x0023444F File Offset: 0x0023264F
			public RemoteDiagnosticsServiceFactory.AddedSymbols Since(int start)
			{
				return new RemoteDiagnosticsServiceFactory.AddedSymbols(this.strings, start);
			}

			// Token: 0x0600AAF9 RID: 43769 RVA: 0x00234460 File Offset: 0x00232660
			public void Add(RemoteDiagnosticsServiceFactory.AddedSymbols symbols)
			{
				this.ExpandTo(symbols.LastSymbol);
				for (int i = 0; i < symbols.Count; i++)
				{
					this.SetValue(symbols.FirstSymbol + i, symbols[i]);
				}
			}

			// Token: 0x0600AAFA RID: 43770 RVA: 0x002344A4 File Offset: 0x002326A4
			public string Read(BinaryReader reader)
			{
				int num = reader.ReadInt32();
				if (num == 0)
				{
					return null;
				}
				if (num < 0)
				{
					num = -num;
					string text = reader.ReadString();
					this.ExpandTo(num);
					this.SetValue(num, text);
					return text;
				}
				return this.strings[num];
			}

			// Token: 0x0600AAFB RID: 43771 RVA: 0x002344E8 File Offset: 0x002326E8
			public void Write(BinaryWriter writer, string symbol)
			{
				if (symbol == null)
				{
					writer.WriteInt32(0);
					return;
				}
				bool flag;
				int num = this.InternValue(symbol, out flag);
				if (flag)
				{
					writer.WriteInt32(-num);
					writer.WriteString(symbol);
					return;
				}
				writer.WriteInt32(num);
			}

			// Token: 0x0600AAFC RID: 43772 RVA: 0x00234524 File Offset: 0x00232724
			private int InternValue(string symbol, out bool wasAdded)
			{
				wasAdded = false;
				int count;
				if (!this.symbols.TryGetValue(symbol, out count))
				{
					count = this.strings.Count;
					this.strings.Add(symbol);
					this.symbols[symbol] = count;
					wasAdded = true;
				}
				return count;
			}

			// Token: 0x0600AAFD RID: 43773 RVA: 0x0023456D File Offset: 0x0023276D
			private void ExpandTo(int value)
			{
				while (this.strings.Count <= value)
				{
					this.strings.Add(null);
				}
			}

			// Token: 0x0600AAFE RID: 43774 RVA: 0x0023458B File Offset: 0x0023278B
			private void SetValue(int value, string symbol)
			{
				this.symbols[symbol] = value;
				this.strings[value] = symbol;
			}

			// Token: 0x040058B3 RID: 22707
			private readonly Dictionary<string, int> symbols;

			// Token: 0x040058B4 RID: 22708
			private readonly List<string> strings;
		}
	}
}
