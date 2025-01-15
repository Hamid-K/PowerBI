using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Serialization;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001D8D RID: 7565
	internal sealed class RemoteVariableService : IVariableService, IDisposable
	{
		// Token: 0x0600BBD6 RID: 48086 RVA: 0x00260204 File Offset: 0x0025E404
		public RemoteVariableService(IMessenger messenger, IEngineHost engineHost, IVariableService variableService)
		{
			this.syncRoot = new object();
			this.messenger = messenger;
			this.engineHost = engineHost;
			this.variableService = variableService;
			this.remoteVariables = new HashSet<string>();
			this.incomingChannels = new HashSet<IMessageChannel>();
			this.outgoingChannels = new HashSet<IMessageChannel>();
			this.writerCompleteEvents = new HashSet<WaitHandle>();
			this.messenger.AddHandler(new Action<IMessageChannel, RemoteVariableService.TryGetValueMessage>(this.OnTryGetValue));
			this.messenger.AddHandler(new Action<IMessageChannel, RemoteVariableService.AddMessage>(this.OnAdd));
			this.messenger.AddHandler(new Action<IMessageChannel, RemoteVariableService.RemoveMessage>(this.OnRemove));
			this.messenger.AddHandler(new Action<IMessageChannel, RemoteVariableService.OpenResultChannelMessage>(this.OnOpenResultChannel));
		}

		// Token: 0x0600BBD7 RID: 48087 RVA: 0x002602C0 File Offset: 0x0025E4C0
		public void Dispose()
		{
			object obj = this.syncRoot;
			WaitHandle[] array;
			lock (obj)
			{
				array = this.writerCompleteEvents.ToArray<WaitHandle>();
				this.writerCompleteEvents.Clear();
			}
			if (array.Length != 0)
			{
				WaitHandle.WaitAll(array);
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Close();
				}
			}
			this.messenger.RemoveHandler<RemoteVariableService.TryGetValueMessage>();
			this.messenger.RemoveHandler<RemoteVariableService.AddMessage>();
			this.messenger.RemoveHandler<RemoteVariableService.RemoveMessage>();
			this.messenger.RemoveHandler<RemoteVariableService.OpenResultChannelMessage>();
			obj = this.syncRoot;
			lock (obj)
			{
				RemoteVariableService.VerifyEmpty<WaitHandle>(this.writerCompleteEvents, "an active result writer");
				RemoteVariableService.VerifyEmpty<IMessageChannel>(this.incomingChannels, "an open result");
				RemoteVariableService.VerifyEmpty<IMessageChannel>(this.outgoingChannels, "an open result");
				RemoteVariableService.VerifyEmpty<string>(this.remoteVariables, "a published variable");
				this.writerCompleteEvents = null;
				this.incomingChannels = null;
				this.outgoingChannels = null;
				this.remoteVariables = null;
				if (this.writerException != null)
				{
					throw this.writerException;
				}
			}
		}

		// Token: 0x0600BBD8 RID: 48088 RVA: 0x002603F4 File Offset: 0x0025E5F4
		public bool TryGetValue(string name, out object value)
		{
			object obj = this.syncRoot;
			bool flag2;
			lock (obj)
			{
				flag2 = this.remoteVariables.Contains(name);
			}
			if (!flag2 && this.variableService.TryGetValue(name, out value))
			{
				return true;
			}
			bool flag;
			using (EvaluatorTracing.CreateTrace("RemoteVariableService/TryGetValue", this.engineHost, TraceEventType.Information, null))
			{
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteVariableService.TryGetValueMessage
					{
						Name = name
					});
					RemoteVariableService.ResultMessage result = messageChannel.WaitFor<RemoteVariableService.ResultMessage>();
					switch (result.ResultKind)
					{
					case RemoteVariableService.ResultKind.GetPageReader:
					{
						Func<IPageReader> func = () => this.GetRemotePageReader(name);
						value = func;
						flag = true;
						break;
					}
					case RemoteVariableService.ResultKind.GetStream:
					{
						Func<Stream> func2 = () => this.GetRemoteStream(name);
						value = func2;
						flag = true;
						break;
					}
					case RemoteVariableService.ResultKind.GetObject:
					{
						Func<object> func3 = () => RemoteVariableService.FromBytes(result.Bytes);
						value = func3;
						flag = true;
						break;
					}
					case RemoteVariableService.ResultKind.Object:
						value = RemoteVariableService.FromBytes(result.Bytes);
						flag = true;
						break;
					default:
						value = null;
						flag = false;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600BBD9 RID: 48089 RVA: 0x00260578 File Offset: 0x0025E778
		private T GetValue<T>(string name)
		{
			object obj;
			if (!this.TryGetValue(name, out obj))
			{
				throw new InvalidOperationException("The variable was removed while in use.");
			}
			return (T)((object)obj);
		}

		// Token: 0x0600BBDA RID: 48090 RVA: 0x002605A4 File Offset: 0x0025E7A4
		private IPageReader GetRemotePageReader(string name)
		{
			IMessageChannel channel = this.OpenResultChannel(name);
			IPageReader pageReader2;
			try
			{
				IPageReader pageReader = RemotePageReader.CreateProxy(this.engineHost, channel, new ExceptionHandler(this.HandleException));
				pageReader2 = pageReader.OnDispose(delegate
				{
					try
					{
						pageReader.Dispose();
					}
					finally
					{
						this.CleanUpResult<IPageReader>(pageReader, channel);
					}
				});
			}
			catch
			{
				this.CleanUpResult<IPageReader>(null, channel);
				throw;
			}
			return pageReader2;
		}

		// Token: 0x0600BBDB RID: 48091 RVA: 0x0026062C File Offset: 0x0025E82C
		private Stream GetRemoteStream(string name)
		{
			IMessageChannel channel = this.OpenResultChannel(name);
			Stream stream2;
			try
			{
				Stream stream = RemoteStream.CreateReaderProxy(this.engineHost, channel, new ExceptionHandler(this.HandleException));
				stream2 = stream.OnDispose(delegate
				{
					try
					{
						stream.Dispose();
					}
					finally
					{
						this.CleanUpResult<Stream>(stream, channel);
					}
				});
			}
			catch
			{
				this.CleanUpResult<Stream>(null, channel);
				throw;
			}
			return stream2;
		}

		// Token: 0x0600BBDC RID: 48092 RVA: 0x002606B4 File Offset: 0x0025E8B4
		private IMessageChannel OpenResultChannel(string name)
		{
			IMessageChannel messageChannel2;
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteVariableService/OpenResultChannel", this.engineHost, TraceEventType.Information, null))
			{
				IMessageChannel messageChannel = this.messenger.CreateChannel();
				try
				{
					messageChannel.Post(new RemoteVariableService.OpenResultChannelMessage
					{
						Name = name
					});
					object obj = this.syncRoot;
					lock (obj)
					{
						this.incomingChannels.Add(messageChannel);
					}
					messageChannel2 = messageChannel;
				}
				catch
				{
					this.CleanUpChannel(hostTrace, this.incomingChannels, messageChannel);
					throw;
				}
			}
			return messageChannel2;
		}

		// Token: 0x0600BBDD RID: 48093 RVA: 0x00260768 File Offset: 0x0025E968
		public void Add(string name, object value)
		{
			this.variableService.Add(name, value);
			using (IMessageChannel messageChannel = this.messenger.CreateChannel())
			{
				messageChannel.Post(new RemoteVariableService.AddMessage
				{
					Name = name,
					ResultKind = RemoteVariableService.GetResultKind(value)
				});
			}
		}

		// Token: 0x0600BBDE RID: 48094 RVA: 0x002607C8 File Offset: 0x0025E9C8
		public void Remove(string name)
		{
			this.variableService.Remove(name);
			using (IMessageChannel messageChannel = this.messenger.CreateChannel())
			{
				messageChannel.Post(new RemoteVariableService.RemoveMessage
				{
					Name = name
				});
			}
		}

		// Token: 0x0600BBDF RID: 48095 RVA: 0x0026081C File Offset: 0x0025EA1C
		private void OnTryGetValue(IMessageChannel channel, RemoteVariableService.TryGetValueMessage tryGetValue)
		{
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteVariableService/OnTryGetValue", this.engineHost, TraceEventType.Information, null))
			{
				this.ReportExceptions(hostTrace, channel, delegate
				{
					RemoteVariableService.ResultKind resultKind = RemoteVariableService.ResultKind.None;
					byte[] array = null;
					object obj;
					if (this.variableService.TryGetValue(tryGetValue.Name, out obj))
					{
						resultKind = RemoteVariableService.GetResultKind(obj);
						if (resultKind != RemoteVariableService.ResultKind.GetObject)
						{
							if (resultKind == RemoteVariableService.ResultKind.Object)
							{
								array = RemoteVariableService.ToBytes(obj, this.engineHost);
							}
						}
						else
						{
							array = RemoteVariableService.ToBytes(((Func<object>)obj)(), this.engineHost);
						}
					}
					channel.Post(new RemoteVariableService.ResultMessage
					{
						ResultKind = resultKind,
						Bytes = array
					});
				});
			}
		}

		// Token: 0x0600BBE0 RID: 48096 RVA: 0x0026088C File Offset: 0x0025EA8C
		private void OnOpenResultChannel(IMessageChannel channel, RemoteVariableService.OpenResultChannelMessage openResultChannel)
		{
			RemoteVariableService.<>c__DisplayClass20_0 CS$<>8__locals1 = new RemoteVariableService.<>c__DisplayClass20_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.openResultChannel = openResultChannel;
			CS$<>8__locals1.channel = channel;
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteVariableService/OnOpenResultChannel", this.engineHost, TraceEventType.Information, null))
			{
				this.ReportExceptions(hostTrace, CS$<>8__locals1.channel, delegate
				{
					RemoteVariableService.<>c__DisplayClass20_1 CS$<>8__locals2 = new RemoteVariableService.<>c__DisplayClass20_1();
					CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
					object value = CS$<>8__locals1.<>4__this.GetValue<object>(CS$<>8__locals1.openResultChannel.Name);
					CS$<>8__locals2.completeEvent = new ManualResetEvent(false);
					RemoteVariableService.ResultKind resultKind = RemoteVariableService.GetResultKind(value);
					if (resultKind != RemoteVariableService.ResultKind.GetPageReader)
					{
						if (resultKind != RemoteVariableService.ResultKind.GetStream)
						{
							throw new NotSupportedException();
						}
						Func<Stream> getStream = (Func<Stream>)value;
						CS$<>8__locals2.writer = delegate(IMessageChannel c)
						{
							CS$<>8__locals2.CS$<>8__locals1.<>4__this.Write<Stream>(c, getStream, new Action<IEngineHost, IMessageChannel, Func<Stream>>(RemoteStream.RunStub), CS$<>8__locals2.completeEvent);
						};
					}
					else
					{
						Func<IPageReader> getPageReader = (Func<IPageReader>)value;
						CS$<>8__locals2.writer = delegate(IMessageChannel c)
						{
							CS$<>8__locals2.CS$<>8__locals1.<>4__this.Write<IPageReader>(c, getPageReader, new Action<IEngineHost, IMessageChannel, Func<IPageReader>>(RemotePageReader.RunStub), CS$<>8__locals2.completeEvent);
						};
					}
					CS$<>8__locals1.channel.TakeOwnership();
					object obj = CS$<>8__locals1.<>4__this.syncRoot;
					lock (obj)
					{
						CS$<>8__locals1.<>4__this.outgoingChannels.Add(CS$<>8__locals1.channel);
						CS$<>8__locals1.<>4__this.writerCompleteEvents.Add(CS$<>8__locals2.completeEvent);
					}
					GlobalizedEvaluatorThreadPool.Start(delegate
					{
						CS$<>8__locals2.writer(CS$<>8__locals2.CS$<>8__locals1.channel);
					});
				});
			}
		}

		// Token: 0x0600BBE1 RID: 48097 RVA: 0x002608FC File Offset: 0x0025EAFC
		private void Write<T>(IMessageChannel channel, Func<T> getValue, Action<IEngineHost, IMessageChannel, Func<T>> runStub, ManualResetEvent completeEvent) where T : IDisposable
		{
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteVariableService/Write", this.engineHost, TraceEventType.Information, null))
			{
				try
				{
					this.ReportExceptions(hostTrace, channel, delegate
					{
						runStub(this.engineHost, channel, getValue);
					});
				}
				catch (Exception ex)
				{
					if (!Microsoft.Mashup.Common.SafeExceptions.TraceIsSafeException(hostTrace, ex))
					{
						throw;
					}
					object obj = this.syncRoot;
					lock (obj)
					{
						if (this.writerException == null)
						{
							this.writerException = ex;
						}
					}
				}
				finally
				{
					this.CleanUpChannel(hostTrace, this.outgoingChannels, channel);
					object obj = this.syncRoot;
					lock (obj)
					{
						completeEvent.Set();
						if (this.writerCompleteEvents.Remove(completeEvent))
						{
							completeEvent.Close();
						}
					}
				}
			}
		}

		// Token: 0x0600BBE2 RID: 48098 RVA: 0x00260A34 File Offset: 0x0025EC34
		private void OnAdd(IMessageChannel channel, RemoteVariableService.AddMessage add)
		{
			object obj;
			switch (add.ResultKind)
			{
			case RemoteVariableService.ResultKind.GetPageReader:
				obj = new Func<IPageReader>(() => this.GetValue<Func<IPageReader>>(add.Name)());
				break;
			case RemoteVariableService.ResultKind.GetStream:
				obj = new Func<Stream>(() => this.GetValue<Func<Stream>>(add.Name)());
				break;
			case RemoteVariableService.ResultKind.GetObject:
				obj = new Func<object>(() => this.GetValue<Func<object>>(add.Name)());
				break;
			default:
				obj = new Func<object>(() => this.GetValue<object>(add.Name));
				break;
			}
			object obj2 = this.syncRoot;
			lock (obj2)
			{
				this.remoteVariables.Add(add.Name);
			}
			this.variableService.Add(add.Name, obj);
		}

		// Token: 0x0600BBE3 RID: 48099 RVA: 0x00260B10 File Offset: 0x0025ED10
		private void OnRemove(IMessageChannel channel, RemoteVariableService.RemoveMessage remove)
		{
			this.variableService.Remove(remove.Name);
			object obj = this.syncRoot;
			lock (obj)
			{
				this.remoteVariables.Remove(remove.Name);
			}
		}

		// Token: 0x0600BBE4 RID: 48100 RVA: 0x00260B70 File Offset: 0x0025ED70
		private void ReportExceptions(IHostTrace trace, IMessageChannel channel, Action action)
		{
			EvaluationHost.ReportExceptions(trace, this.engineHost, channel, action);
		}

		// Token: 0x0600BBE5 RID: 48101 RVA: 0x00260B80 File Offset: 0x0025ED80
		private void CleanUpChannel(IHostTrace trace, HashSet<IMessageChannel> channels, IMessageChannel channel)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				channels.Remove(channel);
			}
			Microsoft.Mashup.Common.SafeExceptions.IgnoreSafeExceptions(this.engineHost, trace, new Action(channel.Dispose));
		}

		// Token: 0x0600BBE6 RID: 48102 RVA: 0x00260BDC File Offset: 0x0025EDDC
		private void CleanUpResult<T>(T result, IMessageChannel channel) where T : IDisposable
		{
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteVariableService/CleanUpResult", this.engineHost, TraceEventType.Information, null))
			{
				try
				{
					if (result != null)
					{
						result.Dispose();
					}
				}
				finally
				{
					this.CleanUpChannel(hostTrace, this.incomingChannels, channel);
				}
			}
		}

		// Token: 0x0600BBE7 RID: 48103 RVA: 0x00260C48 File Offset: 0x0025EE48
		private bool HandleException(Exception exception, bool disposing)
		{
			bool flag;
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteVariableService/TryGetValue", this.engineHost, TraceEventType.Information, null))
			{
				flag = Microsoft.Mashup.Common.SafeExceptions.TraceIsSafeException(hostTrace, exception) && disposing;
			}
			return flag;
		}

		// Token: 0x0600BBE8 RID: 48104 RVA: 0x00260C90 File Offset: 0x0025EE90
		private static void VerifyEmpty<T>(HashSet<T> values, string kind)
		{
			if (values != null && values.Count != 0)
			{
				throw new InvalidOperationException("RemoteVariableService was disposed with " + kind + ".");
			}
		}

		// Token: 0x0600BBE9 RID: 48105 RVA: 0x00260CB4 File Offset: 0x0025EEB4
		private static byte[] ToBytes(object value, IEngineHost engineHost)
		{
			MemoryStream memoryStream = new MemoryStream();
			ObjectWriter objectWriter = new ObjectWriter(new BinaryWriter(memoryStream));
			try
			{
				objectWriter.WriteObject(value);
			}
			catch (NotSupportedException ex)
			{
				IEngine engine = engineHost.QueryService<IEngine>();
				throw engine.Exception(engine.ExceptionRecord(engine.Text("Expression.Error"), engine.Text(Strings.Parameter_TypeNotSupported(ex.Message)), engine.Null));
			}
			return memoryStream.ToArray();
		}

		// Token: 0x0600BBEA RID: 48106 RVA: 0x00260D2C File Offset: 0x0025EF2C
		private static object FromBytes(byte[] bytes)
		{
			return new ObjectReader(new BinaryReader(new MemoryStream(bytes))).ReadObject();
		}

		// Token: 0x0600BBEB RID: 48107 RVA: 0x00260D51 File Offset: 0x0025EF51
		private static RemoteVariableService.ResultKind GetResultKind(object value)
		{
			if (value is Func<IPageReader>)
			{
				return RemoteVariableService.ResultKind.GetPageReader;
			}
			if (value is Func<Stream>)
			{
				return RemoteVariableService.ResultKind.GetStream;
			}
			if (value is Func<object>)
			{
				return RemoteVariableService.ResultKind.GetObject;
			}
			return RemoteVariableService.ResultKind.Object;
		}

		// Token: 0x04005F8F RID: 24463
		private const int previewRowCount = 100;

		// Token: 0x04005F90 RID: 24464
		private readonly object syncRoot;

		// Token: 0x04005F91 RID: 24465
		private readonly IMessenger messenger;

		// Token: 0x04005F92 RID: 24466
		private readonly IEngineHost engineHost;

		// Token: 0x04005F93 RID: 24467
		private readonly IVariableService variableService;

		// Token: 0x04005F94 RID: 24468
		private HashSet<string> remoteVariables;

		// Token: 0x04005F95 RID: 24469
		private HashSet<IMessageChannel> incomingChannels;

		// Token: 0x04005F96 RID: 24470
		private HashSet<IMessageChannel> outgoingChannels;

		// Token: 0x04005F97 RID: 24471
		private HashSet<WaitHandle> writerCompleteEvents;

		// Token: 0x04005F98 RID: 24472
		private Exception writerException;

		// Token: 0x02001D8E RID: 7566
		public enum ResultKind
		{
			// Token: 0x04005F9A RID: 24474
			None,
			// Token: 0x04005F9B RID: 24475
			GetPageReader,
			// Token: 0x04005F9C RID: 24476
			GetStream,
			// Token: 0x04005F9D RID: 24477
			GetObject,
			// Token: 0x04005F9E RID: 24478
			Object
		}

		// Token: 0x02001D8F RID: 7567
		private abstract class NamedMessage : BufferedMessage
		{
			// Token: 0x17002E5F RID: 11871
			// (get) Token: 0x0600BBEC RID: 48108 RVA: 0x00260D72 File Offset: 0x0025EF72
			// (set) Token: 0x0600BBED RID: 48109 RVA: 0x00260D7A File Offset: 0x0025EF7A
			public string Name { get; set; }

			// Token: 0x0600BBEE RID: 48110 RVA: 0x00260D83 File Offset: 0x0025EF83
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteString(this.Name);
			}

			// Token: 0x0600BBEF RID: 48111 RVA: 0x00260D91 File Offset: 0x0025EF91
			public override void Deserialize(BinaryReader reader)
			{
				this.Name = reader.ReadString();
			}
		}

		// Token: 0x02001D90 RID: 7568
		private sealed class TryGetValueMessage : RemoteVariableService.NamedMessage
		{
		}

		// Token: 0x02001D91 RID: 7569
		private sealed class OpenResultChannelMessage : RemoteVariableService.NamedMessage
		{
		}

		// Token: 0x02001D92 RID: 7570
		private sealed class AddMessage : RemoteVariableService.NamedMessage
		{
			// Token: 0x17002E60 RID: 11872
			// (get) Token: 0x0600BBF3 RID: 48115 RVA: 0x00260DA7 File Offset: 0x0025EFA7
			// (set) Token: 0x0600BBF4 RID: 48116 RVA: 0x00260DAF File Offset: 0x0025EFAF
			public RemoteVariableService.ResultKind ResultKind { get; set; }

			// Token: 0x0600BBF5 RID: 48117 RVA: 0x00260DB8 File Offset: 0x0025EFB8
			public override void Serialize(BinaryWriter writer)
			{
				base.Serialize(writer);
				writer.WriteInt32((int)this.ResultKind);
			}

			// Token: 0x0600BBF6 RID: 48118 RVA: 0x00260DCD File Offset: 0x0025EFCD
			public override void Deserialize(BinaryReader reader)
			{
				base.Deserialize(reader);
				this.ResultKind = (RemoteVariableService.ResultKind)reader.ReadInt32();
			}
		}

		// Token: 0x02001D93 RID: 7571
		private sealed class RemoveMessage : RemoteVariableService.NamedMessage
		{
		}

		// Token: 0x02001D94 RID: 7572
		private sealed class ResultMessage : BufferedMessage
		{
			// Token: 0x17002E61 RID: 11873
			// (get) Token: 0x0600BBF9 RID: 48121 RVA: 0x00260DE2 File Offset: 0x0025EFE2
			// (set) Token: 0x0600BBFA RID: 48122 RVA: 0x00260DEA File Offset: 0x0025EFEA
			public RemoteVariableService.ResultKind ResultKind { get; set; }

			// Token: 0x17002E62 RID: 11874
			// (get) Token: 0x0600BBFB RID: 48123 RVA: 0x00260DF3 File Offset: 0x0025EFF3
			// (set) Token: 0x0600BBFC RID: 48124 RVA: 0x00260DFB File Offset: 0x0025EFFB
			public byte[] Bytes { get; set; }

			// Token: 0x0600BBFD RID: 48125 RVA: 0x00260E04 File Offset: 0x0025F004
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteInt32((int)this.ResultKind);
				writer.WriteBool(this.Bytes != null);
				if (this.Bytes != null)
				{
					writer.WriteByteArray(this.Bytes);
				}
			}

			// Token: 0x0600BBFE RID: 48126 RVA: 0x00260E35 File Offset: 0x0025F035
			public override void Deserialize(BinaryReader reader)
			{
				this.ResultKind = (RemoteVariableService.ResultKind)reader.ReadInt32();
				if (reader.ReadBool())
				{
					this.Bytes = reader.ReadByteArray();
				}
			}
		}
	}
}
