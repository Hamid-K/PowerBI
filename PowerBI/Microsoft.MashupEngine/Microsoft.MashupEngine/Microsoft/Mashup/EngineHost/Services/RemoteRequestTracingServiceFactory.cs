using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.RequestTracing;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001AFD RID: 6909
	internal class RemoteRequestTracingServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AD4D RID: 44365 RVA: 0x00239628 File Offset: 0x00237828
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IRequestTracingService requestTracingService = engineHost.QueryService<IRequestTracingService>();
			proxyInitArgs.WriteBool(requestTracingService != null);
			if (requestTracingService != null)
			{
				return new RemoteRequestTracingServiceFactory.Stub(engineHost, requestTracingService, messenger);
			}
			return EmptyStub.Instance;
		}

		// Token: 0x0600AD4E RID: 44366 RVA: 0x00239657 File Offset: 0x00237857
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			if (proxyInitArgs.ReadBool())
			{
				return new RemoteRequestTracingServiceFactory.Proxy(engineHost, messenger);
			}
			return EmptyProxy.Instance;
		}

		// Token: 0x04005982 RID: 22914
		private const int ContainerTraceLimit = 20;

		// Token: 0x02001AFE RID: 6910
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IRequestTracingService
		{
			// Token: 0x0600AD50 RID: 44368 RVA: 0x0023966E File Offset: 0x0023786E
			public Proxy(IEngineHost host, IMessenger messenger)
			{
				this.host = host;
				this.messenger = messenger;
				this.openTraces = 0;
			}

			// Token: 0x17002B90 RID: 11152
			// (get) Token: 0x0600AD51 RID: 44369 RVA: 0x0023968B File Offset: 0x0023788B
			public IMessenger Messenger
			{
				get
				{
					return this.messenger;
				}
			}

			// Token: 0x17002B91 RID: 11153
			// (get) Token: 0x0600AD52 RID: 44370 RVA: 0x00239693 File Offset: 0x00237893
			public IEngineHost Host
			{
				get
				{
					return this.host;
				}
			}

			// Token: 0x0600AD53 RID: 44371 RVA: 0x0023969C File Offset: 0x0023789C
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IRequestTracingService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600AD54 RID: 44372 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0600AD55 RID: 44373 RVA: 0x002396D4 File Offset: 0x002378D4
			public IRequestTrace CreateTrace(Guid? activityId, IResource resource, Guid sessionId, string type)
			{
				if (this.openTraces >= 20)
				{
					using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteRequestTracingService/Proxy/CreateTrace/Dropped", this.host, TraceEventType.Warning, null))
					{
						hostTrace.Add("Resource", resource, true);
						hostTrace.Add("SessionId", sessionId, false);
						hostTrace.Add("Type", type, false);
					}
					return RequestTracingService.DroppedTrace;
				}
				this.openTraces++;
				IRequestTrace requestTrace;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteRequestTracingServiceFactory.CreateTraceRequestMessage
					{
						ActivityId = activityId,
						Resource = resource,
						SessionId = sessionId,
						Type = type
					});
					RemoteRequestTracingServiceFactory.CreateTraceResponseMessage createTraceResponseMessage = messageChannel.WaitFor<RemoteRequestTracingServiceFactory.CreateTraceResponseMessage>();
					requestTrace = new RemoteRequestTracingServiceFactory.RequestTraceProxy(this, createTraceResponseMessage.TraceId, activityId, resource, sessionId, type, createTraceResponseMessage.Timestamp);
				}
				return requestTrace;
			}

			// Token: 0x0600AD56 RID: 44374 RVA: 0x002397C8 File Offset: 0x002379C8
			public bool IsTraceEnabled(IResource resource)
			{
				bool permitted;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteRequestTracingServiceFactory.IsTracePermittedRequestMessage
					{
						Resource = resource
					});
					permitted = messageChannel.WaitFor<RemoteRequestTracingServiceFactory.IsTracePermittedResponseMessage>().Permitted;
				}
				return permitted;
			}

			// Token: 0x0600AD57 RID: 44375 RVA: 0x0023981C File Offset: 0x00237A1C
			public void DisposedTrace()
			{
				this.openTraces--;
			}

			// Token: 0x04005983 RID: 22915
			private readonly IEngineHost host;

			// Token: 0x04005984 RID: 22916
			private readonly IMessenger messenger;

			// Token: 0x04005985 RID: 22917
			private int openTraces;
		}

		// Token: 0x02001AFF RID: 6911
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600AD58 RID: 44376 RVA: 0x0023982C File Offset: 0x00237A2C
			public Stub(IEngineHost host, IRequestTracingService requestTracingService, IMessenger messenger)
			{
				this.traceLock = new object();
				this.host = host;
				this.requestTracingService = requestTracingService;
				this.messenger = messenger;
				this.openTraces = new Dictionary<int, IRequestTrace>();
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteRequestTracingServiceFactory.CreateTraceRequestMessage>(this.OnCreateTraceRequest));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteRequestTracingServiceFactory.IsTracePermittedRequestMessage>(this.OnIsTracePermittedRequest));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteRequestTracingServiceFactory.GetContentStreamRequestMessage>(this.OnGetContentStream));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteRequestTracingServiceFactory.AddMetadataRequestMessage>(this.OnAddMetadata));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteRequestTracingServiceFactory.BufferMetadataRequestMessage>(this.OnBufferMetadata));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteRequestTracingServiceFactory.TraceDisposeRequestMessage>(this.OnTraceDispose));
			}

			// Token: 0x0600AD59 RID: 44377 RVA: 0x002398F4 File Offset: 0x00237AF4
			private void OnCreateTraceRequest(IMessageChannel channel, RemoteRequestTracingServiceFactory.CreateTraceRequestMessage message)
			{
				IRequestTrace requestTrace = this.requestTracingService.CreateTrace(message.ActivityId, message.Resource, message.SessionId, message.Type);
				object obj = this.traceLock;
				lock (obj)
				{
					this.openTraces.Add(requestTrace.TraceId, requestTrace);
				}
				channel.Post(new RemoteRequestTracingServiceFactory.CreateTraceResponseMessage
				{
					TraceId = requestTrace.TraceId,
					Timestamp = requestTrace.Timestamp
				});
			}

			// Token: 0x0600AD5A RID: 44378 RVA: 0x00239988 File Offset: 0x00237B88
			private void OnIsTracePermittedRequest(IMessageChannel channel, RemoteRequestTracingServiceFactory.IsTracePermittedRequestMessage message)
			{
				channel.Post(new RemoteRequestTracingServiceFactory.IsTracePermittedResponseMessage
				{
					Permitted = this.requestTracingService.IsTraceEnabled(message.Resource)
				});
			}

			// Token: 0x0600AD5B RID: 44379 RVA: 0x002399AC File Offset: 0x00237BAC
			private void OnGetContentStream(IMessageChannel channel, RemoteRequestTracingServiceFactory.GetContentStreamRequestMessage message)
			{
				object obj = this.traceLock;
				IRequestTrace trace;
				lock (obj)
				{
					this.openTraces.TryGetValue(message.TraceId, out trace);
				}
				if (trace != null)
				{
					channel.TakeOwnership();
					GlobalizedEvaluatorThreadPool.Start(delegate
					{
						this.ReadContentStream(channel, trace);
					});
				}
			}

			// Token: 0x0600AD5C RID: 44380 RVA: 0x00239A38 File Offset: 0x00237C38
			private void ReadContentStream(IMessageChannel channel, IRequestTrace trace)
			{
				using (Stream stream = RemoteStream.CreateReaderProxy(this.host, channel, new ExceptionHandler(this.HandleException)))
				{
					using (Stream contentStream = trace.GetContentStream())
					{
						stream.CopyTo(contentStream);
					}
				}
				channel.Post(new RemoteRequestTracingServiceFactory.ContentStreamClosedMessage());
				channel.Dispose();
			}

			// Token: 0x0600AD5D RID: 44381 RVA: 0x00239AB0 File Offset: 0x00237CB0
			private void OnAddMetadata(IMessageChannel channel, RemoteRequestTracingServiceFactory.AddMetadataRequestMessage message)
			{
				object obj = this.traceLock;
				IRequestTrace requestTrace;
				lock (obj)
				{
					this.openTraces.TryGetValue(message.TraceId, out requestTrace);
				}
				if (requestTrace != null)
				{
					requestTrace.AddMetadata(message.Name, message.Value);
				}
			}

			// Token: 0x0600AD5E RID: 44382 RVA: 0x00239B14 File Offset: 0x00237D14
			private void OnBufferMetadata(IMessageChannel channel, RemoteRequestTracingServiceFactory.BufferMetadataRequestMessage message)
			{
				object obj = this.traceLock;
				IRequestTrace requestTrace;
				lock (obj)
				{
					this.openTraces.TryGetValue(message.TraceId, out requestTrace);
				}
				if (requestTrace != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in message.Metadata)
					{
						requestTrace.AddMetadata(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}

			// Token: 0x0600AD5F RID: 44383 RVA: 0x00239BB8 File Offset: 0x00237DB8
			private void OnTraceDispose(IMessageChannel channel, RemoteRequestTracingServiceFactory.TraceDisposeRequestMessage message)
			{
				object obj = this.traceLock;
				IRequestTrace requestTrace;
				lock (obj)
				{
					if (this.openTraces.TryGetValue(message.TraceId, out requestTrace))
					{
						this.openTraces.Remove(message.TraceId);
					}
				}
				if (requestTrace != null)
				{
					requestTrace.Dispose();
				}
				channel.Post(new RemoteRequestTracingServiceFactory.TraceDisposeCompleteMessage());
			}

			// Token: 0x0600AD60 RID: 44384 RVA: 0x00239C30 File Offset: 0x00237E30
			public void Dispose()
			{
				object obj = this.traceLock;
				lock (obj)
				{
					foreach (IRequestTrace requestTrace in this.openTraces.Values)
					{
						requestTrace.Dispose();
					}
					this.openTraces.Clear();
				}
				this.messenger.RemoveHandler<RemoteRequestTracingServiceFactory.CreateTraceRequestMessage>();
				this.messenger.RemoveHandler<RemoteRequestTracingServiceFactory.IsTracePermittedRequestMessage>();
				this.messenger.RemoveHandler<RemoteRequestTracingServiceFactory.GetContentStreamRequestMessage>();
				this.messenger.RemoveHandler<RemoteRequestTracingServiceFactory.AddMetadataRequestMessage>();
				this.messenger.RemoveHandler<RemoteRequestTracingServiceFactory.TraceDisposeRequestMessage>();
			}

			// Token: 0x0600AD61 RID: 44385 RVA: 0x00239CF0 File Offset: 0x00237EF0
			private bool HandleException(Exception exception, bool disposing)
			{
				bool flag;
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteRequestTracingService/Stub/ReadContentStream", this.host, TraceEventType.Information, null))
				{
					flag = SafeExceptions.TraceIsSafeException(hostTrace, exception) && disposing;
				}
				return flag;
			}

			// Token: 0x04005986 RID: 22918
			private readonly object traceLock;

			// Token: 0x04005987 RID: 22919
			private readonly IEngineHost host;

			// Token: 0x04005988 RID: 22920
			private readonly IRequestTracingService requestTracingService;

			// Token: 0x04005989 RID: 22921
			private readonly IMessenger messenger;

			// Token: 0x0400598A RID: 22922
			private readonly Dictionary<int, IRequestTrace> openTraces;
		}

		// Token: 0x02001B01 RID: 6913
		private class RequestTraceProxy : IRequestTrace, IDisposable
		{
			// Token: 0x0600AD64 RID: 44388 RVA: 0x00239D54 File Offset: 0x00237F54
			public RequestTraceProxy(RemoteRequestTracingServiceFactory.Proxy proxy, int traceId, Guid? activityId, IResource resource, Guid sessionId, string type, DateTime timestamp)
			{
				this.proxy = proxy;
				this.traceId = traceId;
				this.activityId = activityId;
				this.resource = resource;
				this.sessionId = sessionId;
				this.type = type;
				this.timestamp = timestamp;
				this.metadataBuffer = new List<KeyValuePair<string, string>>();
				this.disposed = false;
			}

			// Token: 0x17002B92 RID: 11154
			// (get) Token: 0x0600AD65 RID: 44389 RVA: 0x00239DAE File Offset: 0x00237FAE
			public int TraceId
			{
				get
				{
					return this.traceId;
				}
			}

			// Token: 0x17002B93 RID: 11155
			// (get) Token: 0x0600AD66 RID: 44390 RVA: 0x00239DB6 File Offset: 0x00237FB6
			public Guid? ActivityId
			{
				get
				{
					return this.activityId;
				}
			}

			// Token: 0x17002B94 RID: 11156
			// (get) Token: 0x0600AD67 RID: 44391 RVA: 0x00239DBE File Offset: 0x00237FBE
			public IResource Resource
			{
				get
				{
					return this.resource;
				}
			}

			// Token: 0x17002B95 RID: 11157
			// (get) Token: 0x0600AD68 RID: 44392 RVA: 0x00239DC6 File Offset: 0x00237FC6
			public Guid SessionId
			{
				get
				{
					return this.sessionId;
				}
			}

			// Token: 0x17002B96 RID: 11158
			// (get) Token: 0x0600AD69 RID: 44393 RVA: 0x00239DCE File Offset: 0x00237FCE
			public string Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17002B97 RID: 11159
			// (get) Token: 0x0600AD6A RID: 44394 RVA: 0x00239DD6 File Offset: 0x00237FD6
			public DateTime Timestamp
			{
				get
				{
					return this.timestamp;
				}
			}

			// Token: 0x0600AD6B RID: 44395 RVA: 0x00239DE0 File Offset: 0x00237FE0
			public Stream GetContentStream()
			{
				if (this.contentStream != null || this.disposed)
				{
					return this.contentStream;
				}
				this.BufferMetadata();
				this.streamChannel = this.proxy.Messenger.CreateChannel();
				this.streamChannel.Post(new RemoteRequestTracingServiceFactory.GetContentStreamRequestMessage
				{
					TraceId = this.TraceId
				});
				this.contentStream = RemoteStream.CreateWriterStub(this.proxy.Host, this.streamChannel);
				return this.contentStream;
			}

			// Token: 0x0600AD6C RID: 44396 RVA: 0x00239E60 File Offset: 0x00238060
			public void AddMetadata(string name, string value)
			{
				if (this.metadataBuffer != null)
				{
					this.metadataBuffer.Add(new KeyValuePair<string, string>(name, value));
					return;
				}
				using (IMessageChannel messageChannel = this.proxy.Messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteRequestTracingServiceFactory.AddMetadataRequestMessage
					{
						TraceId = this.TraceId,
						Name = name,
						Value = value
					});
				}
			}

			// Token: 0x0600AD6D RID: 44397 RVA: 0x00239EDC File Offset: 0x002380DC
			public void Dispose()
			{
				this.BufferMetadata();
				if (this.contentStream != null)
				{
					this.contentStream.Dispose();
					this.contentStream = null;
					this.streamChannel.WaitFor<RemoteRequestTracingServiceFactory.ContentStreamClosedMessage>();
					this.streamChannel.Dispose();
					this.streamChannel = null;
				}
				using (IMessageChannel messageChannel = this.proxy.Messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteRequestTracingServiceFactory.TraceDisposeRequestMessage
					{
						TraceId = this.TraceId
					});
					messageChannel.WaitFor<RemoteRequestTracingServiceFactory.TraceDisposeCompleteMessage>();
				}
				if (!this.disposed)
				{
					this.disposed = true;
					this.proxy.DisposedTrace();
				}
			}

			// Token: 0x0600AD6E RID: 44398 RVA: 0x00239F8C File Offset: 0x0023818C
			private void BufferMetadata()
			{
				if (this.metadataBuffer != null && this.metadataBuffer.Count > 0)
				{
					using (IMessageChannel messageChannel = this.proxy.Messenger.CreateChannel())
					{
						messageChannel.Post(new RemoteRequestTracingServiceFactory.BufferMetadataRequestMessage
						{
							TraceId = this.TraceId,
							Metadata = this.metadataBuffer
						});
					}
				}
				this.metadataBuffer = null;
			}

			// Token: 0x0400598E RID: 22926
			private readonly RemoteRequestTracingServiceFactory.Proxy proxy;

			// Token: 0x0400598F RID: 22927
			private readonly int traceId;

			// Token: 0x04005990 RID: 22928
			private readonly Guid? activityId;

			// Token: 0x04005991 RID: 22929
			private readonly IResource resource;

			// Token: 0x04005992 RID: 22930
			private readonly Guid sessionId;

			// Token: 0x04005993 RID: 22931
			private readonly string type;

			// Token: 0x04005994 RID: 22932
			private readonly DateTime timestamp;

			// Token: 0x04005995 RID: 22933
			private IMessageChannel streamChannel;

			// Token: 0x04005996 RID: 22934
			private Stream contentStream;

			// Token: 0x04005997 RID: 22935
			private List<KeyValuePair<string, string>> metadataBuffer;

			// Token: 0x04005998 RID: 22936
			private bool disposed;
		}

		// Token: 0x02001B02 RID: 6914
		private sealed class CreateTraceRequestMessage : BufferedMessage
		{
			// Token: 0x17002B98 RID: 11160
			// (get) Token: 0x0600AD6F RID: 44399 RVA: 0x0023A008 File Offset: 0x00238208
			// (set) Token: 0x0600AD70 RID: 44400 RVA: 0x0023A010 File Offset: 0x00238210
			public Guid? ActivityId { get; set; }

			// Token: 0x17002B99 RID: 11161
			// (get) Token: 0x0600AD71 RID: 44401 RVA: 0x0023A019 File Offset: 0x00238219
			// (set) Token: 0x0600AD72 RID: 44402 RVA: 0x0023A021 File Offset: 0x00238221
			public IResource Resource { get; set; }

			// Token: 0x17002B9A RID: 11162
			// (get) Token: 0x0600AD73 RID: 44403 RVA: 0x0023A02A File Offset: 0x0023822A
			// (set) Token: 0x0600AD74 RID: 44404 RVA: 0x0023A032 File Offset: 0x00238232
			public Guid SessionId { get; set; }

			// Token: 0x17002B9B RID: 11163
			// (get) Token: 0x0600AD75 RID: 44405 RVA: 0x0023A03B File Offset: 0x0023823B
			// (set) Token: 0x0600AD76 RID: 44406 RVA: 0x0023A043 File Offset: 0x00238243
			public string Type { get; set; }

			// Token: 0x0600AD77 RID: 44407 RVA: 0x0023A04C File Offset: 0x0023824C
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteBool(this.ActivityId != null);
				if (this.ActivityId != null)
				{
					writer.WriteGuid(this.ActivityId.Value);
				}
				writer.WriteIResource(this.Resource);
				writer.WriteGuid(this.SessionId);
				writer.WriteString(this.Type);
			}

			// Token: 0x0600AD78 RID: 44408 RVA: 0x0023A0B8 File Offset: 0x002382B8
			public override void Deserialize(BinaryReader reader)
			{
				if (reader.ReadBool())
				{
					this.ActivityId = new Guid?(reader.ReadGuid());
				}
				else
				{
					this.ActivityId = null;
				}
				this.Resource = reader.ReadIResource();
				this.SessionId = reader.ReadGuid();
				this.Type = reader.ReadString();
			}
		}

		// Token: 0x02001B03 RID: 6915
		private sealed class CreateTraceResponseMessage : BufferedMessage
		{
			// Token: 0x17002B9C RID: 11164
			// (get) Token: 0x0600AD7A RID: 44410 RVA: 0x0023A113 File Offset: 0x00238313
			// (set) Token: 0x0600AD7B RID: 44411 RVA: 0x0023A11B File Offset: 0x0023831B
			public int TraceId { get; set; }

			// Token: 0x17002B9D RID: 11165
			// (get) Token: 0x0600AD7C RID: 44412 RVA: 0x0023A124 File Offset: 0x00238324
			// (set) Token: 0x0600AD7D RID: 44413 RVA: 0x0023A12C File Offset: 0x0023832C
			public DateTime Timestamp { get; set; }

			// Token: 0x0600AD7E RID: 44414 RVA: 0x0023A135 File Offset: 0x00238335
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteInt32(this.TraceId);
				writer.WriteDateTime(this.Timestamp);
			}

			// Token: 0x0600AD7F RID: 44415 RVA: 0x0023A14F File Offset: 0x0023834F
			public override void Deserialize(BinaryReader reader)
			{
				this.TraceId = reader.ReadInt32();
				this.Timestamp = reader.ReadDateTime();
			}
		}

		// Token: 0x02001B04 RID: 6916
		private sealed class IsTracePermittedRequestMessage : BufferedMessage
		{
			// Token: 0x17002B9E RID: 11166
			// (get) Token: 0x0600AD81 RID: 44417 RVA: 0x0023A169 File Offset: 0x00238369
			// (set) Token: 0x0600AD82 RID: 44418 RVA: 0x0023A171 File Offset: 0x00238371
			public IResource Resource { get; set; }

			// Token: 0x0600AD83 RID: 44419 RVA: 0x0023A17A File Offset: 0x0023837A
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteIResource(this.Resource);
			}

			// Token: 0x0600AD84 RID: 44420 RVA: 0x0023A188 File Offset: 0x00238388
			public override void Deserialize(BinaryReader reader)
			{
				this.Resource = reader.ReadIResource();
			}
		}

		// Token: 0x02001B05 RID: 6917
		private class IsTracePermittedResponseMessage : BufferedMessage
		{
			// Token: 0x17002B9F RID: 11167
			// (get) Token: 0x0600AD86 RID: 44422 RVA: 0x0023A196 File Offset: 0x00238396
			// (set) Token: 0x0600AD87 RID: 44423 RVA: 0x0023A19E File Offset: 0x0023839E
			public bool Permitted { get; set; }

			// Token: 0x0600AD88 RID: 44424 RVA: 0x0023A1A7 File Offset: 0x002383A7
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteBool(this.Permitted);
			}

			// Token: 0x0600AD89 RID: 44425 RVA: 0x0023A1B5 File Offset: 0x002383B5
			public override void Deserialize(BinaryReader reader)
			{
				this.Permitted = reader.ReadBool();
			}
		}

		// Token: 0x02001B06 RID: 6918
		private sealed class GetContentStreamRequestMessage : BufferedMessage
		{
			// Token: 0x17002BA0 RID: 11168
			// (get) Token: 0x0600AD8B RID: 44427 RVA: 0x0023A1C3 File Offset: 0x002383C3
			// (set) Token: 0x0600AD8C RID: 44428 RVA: 0x0023A1CB File Offset: 0x002383CB
			public int TraceId { get; set; }

			// Token: 0x0600AD8D RID: 44429 RVA: 0x0023A1D4 File Offset: 0x002383D4
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteInt32(this.TraceId);
			}

			// Token: 0x0600AD8E RID: 44430 RVA: 0x0023A1E2 File Offset: 0x002383E2
			public override void Deserialize(BinaryReader reader)
			{
				this.TraceId = reader.ReadInt32();
			}
		}

		// Token: 0x02001B07 RID: 6919
		private sealed class ContentStreamClosedMessage : BufferedMessage
		{
			// Token: 0x0600AD90 RID: 44432 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x0600AD91 RID: 44433 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Deserialize(BinaryReader reader)
			{
			}
		}

		// Token: 0x02001B08 RID: 6920
		private sealed class AddMetadataRequestMessage : BufferedMessage
		{
			// Token: 0x17002BA1 RID: 11169
			// (get) Token: 0x0600AD93 RID: 44435 RVA: 0x0023A1F0 File Offset: 0x002383F0
			// (set) Token: 0x0600AD94 RID: 44436 RVA: 0x0023A1F8 File Offset: 0x002383F8
			public int TraceId { get; set; }

			// Token: 0x17002BA2 RID: 11170
			// (get) Token: 0x0600AD95 RID: 44437 RVA: 0x0023A201 File Offset: 0x00238401
			// (set) Token: 0x0600AD96 RID: 44438 RVA: 0x0023A209 File Offset: 0x00238409
			public string Name { get; set; }

			// Token: 0x17002BA3 RID: 11171
			// (get) Token: 0x0600AD97 RID: 44439 RVA: 0x0023A212 File Offset: 0x00238412
			// (set) Token: 0x0600AD98 RID: 44440 RVA: 0x0023A21A File Offset: 0x0023841A
			public string Value { get; set; }

			// Token: 0x0600AD99 RID: 44441 RVA: 0x0023A223 File Offset: 0x00238423
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteInt32(this.TraceId);
				writer.WriteString(this.Name);
				writer.WriteString(this.Value);
			}

			// Token: 0x0600AD9A RID: 44442 RVA: 0x0023A249 File Offset: 0x00238449
			public override void Deserialize(BinaryReader reader)
			{
				this.TraceId = reader.ReadInt32();
				this.Name = reader.ReadString();
				this.Value = reader.ReadString();
			}
		}

		// Token: 0x02001B09 RID: 6921
		private sealed class BufferMetadataRequestMessage : BufferedMessage
		{
			// Token: 0x17002BA4 RID: 11172
			// (get) Token: 0x0600AD9C RID: 44444 RVA: 0x0023A26F File Offset: 0x0023846F
			// (set) Token: 0x0600AD9D RID: 44445 RVA: 0x0023A277 File Offset: 0x00238477
			public int TraceId { get; set; }

			// Token: 0x17002BA5 RID: 11173
			// (get) Token: 0x0600AD9E RID: 44446 RVA: 0x0023A280 File Offset: 0x00238480
			// (set) Token: 0x0600AD9F RID: 44447 RVA: 0x0023A288 File Offset: 0x00238488
			public List<KeyValuePair<string, string>> Metadata { get; set; }

			// Token: 0x0600ADA0 RID: 44448 RVA: 0x0023A291 File Offset: 0x00238491
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteInt32(this.TraceId);
				writer.WriteList(this.Metadata, delegate(BinaryWriter w, KeyValuePair<string, string> kvp)
				{
					w.WriteString(kvp.Key);
					w.WriteString(kvp.Value);
				});
			}

			// Token: 0x0600ADA1 RID: 44449 RVA: 0x0023A2CA File Offset: 0x002384CA
			public override void Deserialize(BinaryReader reader)
			{
				this.TraceId = reader.ReadInt32();
				this.Metadata = reader.ReadList((BinaryReader r) => new KeyValuePair<string, string>(r.ReadString(), r.ReadString()));
			}
		}

		// Token: 0x02001B0B RID: 6923
		private sealed class TraceDisposeRequestMessage : BufferedMessage
		{
			// Token: 0x17002BA6 RID: 11174
			// (get) Token: 0x0600ADA7 RID: 44455 RVA: 0x0023A33E File Offset: 0x0023853E
			// (set) Token: 0x0600ADA8 RID: 44456 RVA: 0x0023A346 File Offset: 0x00238546
			public int TraceId { get; set; }

			// Token: 0x0600ADA9 RID: 44457 RVA: 0x0023A34F File Offset: 0x0023854F
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteInt32(this.TraceId);
			}

			// Token: 0x0600ADAA RID: 44458 RVA: 0x0023A35D File Offset: 0x0023855D
			public override void Deserialize(BinaryReader reader)
			{
				this.TraceId = reader.ReadInt32();
			}
		}

		// Token: 0x02001B0C RID: 6924
		private sealed class TraceDisposeCompleteMessage : BufferedMessage
		{
			// Token: 0x0600ADAC RID: 44460 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x0600ADAD RID: 44461 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Deserialize(BinaryReader reader)
			{
			}
		}
	}
}
