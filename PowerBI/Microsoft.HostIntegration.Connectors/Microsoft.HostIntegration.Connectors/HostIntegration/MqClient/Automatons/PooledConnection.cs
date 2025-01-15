using System;
using System.Globalization;
using System.Threading;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.MqClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AF6 RID: 2806
	internal class PooledConnection
	{
		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x060058C9 RID: 22729 RVA: 0x0016CD3D File Offset: 0x0016AF3D
		// (set) Token: 0x060058CA RID: 22730 RVA: 0x0016CD45 File Offset: 0x0016AF45
		public AutomatonTcp Automaton { get; private set; }

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x060058CB RID: 22731 RVA: 0x0016CD4E File Offset: 0x0016AF4E
		internal ChannelParameters ChannelParameters
		{
			get
			{
				return (this.Automaton.Automaton.Context as AutomatonTcpContext).ChannelParameters;
			}
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x0016CD6C File Offset: 0x0016AF6C
		public PooledConnection(TcpConnectionParameters parameters, ChannelQueueManager channelQueueManager)
		{
			if (parameters == null)
			{
				throw new InvalidOperationException("parameters null");
			}
			if (parameters.Server == null)
			{
				throw new InvalidOperationException("parameters.Server null");
			}
			if (parameters.Port < 1 || parameters.Port > 65535)
			{
				throw new InvalidOperationException("parameters.Port out of range");
			}
			this.traceContainer = new MqTraceContainer();
			ApplicationTracePoint applicationTracePoint = new ApplicationTracePoint(this.traceContainer);
			this.tracePoint = new ConnectionTracePoint(applicationTracePoint);
			this.tracePoint[TracePointPropertyIdentifiers.Server] = parameters.Server;
			this.tracePoint[TracePointPropertyIdentifiers.Port] = parameters.Port;
			if (this.tracePoint.IsEnabled(TraceFlags.Information))
			{
				this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "TCP connection is to Server: {0}, Port: {1}", parameters.Server, parameters.Port));
			}
			this.Automaton = new AutomatonTcp(this.tracePoint, Pooling.SingletonEventLogContainer);
			AutomatonTcpContext automatonTcpContext = this.Automaton.Automaton.Context as AutomatonTcpContext;
			automatonTcpContext.ConnectionParameters = parameters;
			automatonTcpContext.ChannelQueueManager = channelQueueManager;
		}

		// Token: 0x060058CD RID: 22733 RVA: 0x0016CE98 File Offset: 0x0016B098
		~PooledConnection()
		{
			this.traceContainer.Release();
		}

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x060058CE RID: 22734 RVA: 0x0016CECC File Offset: 0x0016B0CC
		public int NextId
		{
			get
			{
				return Interlocked.Increment(ref this.ConversationId);
			}
		}

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x060058CF RID: 22735 RVA: 0x0016CED9 File Offset: 0x0016B0D9
		// (set) Token: 0x060058D0 RID: 22736 RVA: 0x0016CEE1 File Offset: 0x0016B0E1
		internal bool IsConnected { get; private set; }

		// Token: 0x060058D1 RID: 22737 RVA: 0x0016CEEC File Offset: 0x0016B0EC
		public ReturnCode Connect()
		{
			object obj = this.lockConnectObject;
			ReturnCode connectReturnCode;
			lock (obj)
			{
				AutomatonTcpContext automatonTcpContext = this.Automaton.Automaton.Context as AutomatonTcpContext;
				automatonTcpContext.ConnectReturnCode = ReturnCode.Ok;
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Connecting", Array.Empty<object>()));
				}
				this.Automaton.ProcessEvent(AutomatonTcpEvent.Connect);
				automatonTcpContext.ConnectedEvent.WaitOne();
				if (automatonTcpContext.ConnectReturnCode == ReturnCode.Ok)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Connect Succeeded", Array.Empty<object>()));
					}
					this.IsConnected = true;
				}
				else
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Information))
					{
						this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Connect Failed, Return Code: {0}", automatonTcpContext.ConnectReturnCode));
					}
					this.IsConnected = false;
					automatonTcpContext.ChannelQueueManager = null;
				}
				connectReturnCode = automatonTcpContext.ConnectReturnCode;
			}
			return connectReturnCode;
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x0016D014 File Offset: 0x0016B214
		public ReturnCode Disconnect()
		{
			object obj = this.lockDisconnectObject;
			ReturnCode disconnectReturnCode;
			lock (obj)
			{
				AutomatonTcpContext automatonTcpContext = this.Automaton.Automaton.Context as AutomatonTcpContext;
				automatonTcpContext.DisconnectReturnCode = ReturnCode.Ok;
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Disconnecting", Array.Empty<object>()));
				}
				this.Automaton.ProcessEvent(AutomatonTcpEvent.Disconnect);
				automatonTcpContext.DisconnectedEvent.WaitOne();
				if (automatonTcpContext.DisconnectReturnCode == ReturnCode.Ok)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Disconnect Succeeded", Array.Empty<object>()));
					}
				}
				else if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Disconnect Failed, Return Code: {0}", automatonTcpContext.DisconnectReturnCode));
				}
				this.IsConnected = false;
				this.Automaton.ClearBuffers();
				automatonTcpContext.ChannelQueueManager = null;
				disconnectReturnCode = automatonTcpContext.DisconnectReturnCode;
			}
			return disconnectReturnCode;
		}

		// Token: 0x040045CB RID: 17867
		private ConnectionTracePoint tracePoint;

		// Token: 0x040045CC RID: 17868
		private int ConversationId;

		// Token: 0x040045CD RID: 17869
		private object lockConnectObject = new object();

		// Token: 0x040045CE RID: 17870
		private object lockDisconnectObject = new object();

		// Token: 0x040045CF RID: 17871
		private MqTraceContainer traceContainer;
	}
}
