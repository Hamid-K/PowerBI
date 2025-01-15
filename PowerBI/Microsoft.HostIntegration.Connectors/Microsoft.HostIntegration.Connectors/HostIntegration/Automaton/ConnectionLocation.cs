using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.Automaton
{
	// Token: 0x020004C7 RID: 1223
	public class ConnectionLocation
	{
		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x060029BA RID: 10682 RVA: 0x0007DA7C File Offset: 0x0007BC7C
		// (set) Token: 0x060029BB RID: 10683 RVA: 0x0007DA84 File Offset: 0x0007BC84
		public AsynchronousConnectionClient Client { get; private set; }

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060029BC RID: 10684 RVA: 0x0007DA8D File Offset: 0x0007BC8D
		// (set) Token: 0x060029BD RID: 10685 RVA: 0x0007DA95 File Offset: 0x0007BC95
		public ConnectionType ConnectionType { get; private set; }

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060029BE RID: 10686 RVA: 0x0007DA9E File Offset: 0x0007BC9E
		// (set) Token: 0x060029BF RID: 10687 RVA: 0x0007DAA6 File Offset: 0x0007BCA6
		public int ConnectionEnumeration { get; private set; }

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060029C0 RID: 10688 RVA: 0x0007DAAF File Offset: 0x0007BCAF
		// (set) Token: 0x060029C1 RID: 10689 RVA: 0x0007DAB7 File Offset: 0x0007BCB7
		public int OtherEndConnectionEnumeration { get; private set; }

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060029C2 RID: 10690 RVA: 0x0007DAC0 File Offset: 0x0007BCC0
		// (set) Token: 0x060029C3 RID: 10691 RVA: 0x0007DAC8 File Offset: 0x0007BCC8
		public ProcessAsynchronousMessage ProcessAsynchronousMessage { get; private set; }

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060029C4 RID: 10692 RVA: 0x0007DAD1 File Offset: 0x0007BCD1
		// (set) Token: 0x060029C5 RID: 10693 RVA: 0x0007DAD9 File Offset: 0x0007BCD9
		public ProcessDisconnect ProcessDisconnect { get; private set; }

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x0007DAE4 File Offset: 0x0007BCE4
		internal AsynchronousConnection OnlyConnection
		{
			get
			{
				object obj = this.lockObject;
				AsynchronousConnection asynchronousConnection;
				lock (obj)
				{
					asynchronousConnection = this.connections[0];
				}
				return asynchronousConnection;
			}
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x0007DB28 File Offset: 0x0007BD28
		public ConnectionLocation(AsynchronousConnectionClient client, ProcessAsynchronousMessage processAsynchronousMessage, ProcessDisconnect processDisconnect, int connectionEnumeration, int otherEndConnectionEnumeration, ConnectionType connectionType, int maximumNumberOfConnections)
		{
			if (maximumNumberOfConnections == -1)
			{
				this.isDynamic = true;
			}
			else
			{
				this.isDynamic = false;
			}
			object obj = this.lockObject;
			lock (obj)
			{
				this.Client = client;
				this.ProcessAsynchronousMessage = processAsynchronousMessage;
				this.ProcessDisconnect = processDisconnect;
				this.ConnectionEnumeration = connectionEnumeration;
				this.OtherEndConnectionEnumeration = otherEndConnectionEnumeration;
				this.ConnectionType = connectionType;
				if (this.isDynamic)
				{
					this.connectionsDynamic = new Dictionary<int, AsynchronousConnection>();
				}
				else
				{
					this.connections = new AsynchronousConnection[maximumNumberOfConnections];
					for (int i = 0; i < maximumNumberOfConnections; i++)
					{
						this.connections[i] = new AsynchronousConnection(this);
					}
				}
			}
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x0007DC00 File Offset: 0x0007BE00
		public void Receive(AsynchronousConnectionMessage message)
		{
			List<AsynchronousConnectionMessage> list = this.messages;
			lock (list)
			{
				this.messages.Add(message);
				this.Client.MessageReceived(this);
			}
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x0007DC58 File Offset: 0x0007BE58
		public void ProcessAnyReceivedMessages()
		{
			bool flag = true;
			while (flag)
			{
				List<AsynchronousConnectionMessage> list = this.messages;
				AsynchronousConnectionMessage asynchronousConnectionMessage;
				lock (list)
				{
					if (this.messages.Count == 0)
					{
						flag = false;
						continue;
					}
					asynchronousConnectionMessage = this.messages[0];
					this.messages.RemoveAt(0);
				}
				this.ProcessAsynchronousMessage(asynchronousConnectionMessage);
			}
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x0007DCD8 File Offset: 0x0007BED8
		public void Send(AsynchronousConnectionMessage message)
		{
			AsynchronousConnection asynchronousConnection = null;
			object obj = this.lockObject;
			lock (obj)
			{
				asynchronousConnection = this.connections[0];
			}
			asynchronousConnection.Send(message);
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x0007DD24 File Offset: 0x0007BF24
		public bool Send(AsynchronousConnectionMessage message, int determinant)
		{
			if (determinant == -1)
			{
				this.Broadcast(message);
				return true;
			}
			AsynchronousConnection asynchronousConnection = null;
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.isDynamic)
				{
					if (!this.connectionsDynamic.TryGetValue(determinant, out asynchronousConnection))
					{
						return false;
					}
				}
				else
				{
					asynchronousConnection = this.connections[determinant];
				}
			}
			if (asynchronousConnection == null)
			{
				return false;
			}
			asynchronousConnection.Send(message);
			return true;
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x0007DDA4 File Offset: 0x0007BFA4
		private void Broadcast(AsynchronousConnectionMessage message)
		{
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.isDynamic)
				{
					using (Dictionary<int, AsynchronousConnection>.ValueCollection.Enumerator enumerator = this.connectionsDynamic.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							AsynchronousConnection asynchronousConnection = enumerator.Current;
							if (asynchronousConnection != null)
							{
								asynchronousConnection.Send(message);
							}
						}
						return;
					}
				}
				foreach (AsynchronousConnection asynchronousConnection2 in this.connections)
				{
					if (asynchronousConnection2 != null)
					{
						asynchronousConnection2.Send(message);
					}
				}
			}
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x0007DE60 File Offset: 0x0007C060
		internal void ConnectTo(AutomatonDriverAsCode otherEnd, int connectionEnumeration)
		{
			AsynchronousConnection asynchronousConnection = null;
			object obj = this.lockObject;
			lock (obj)
			{
				asynchronousConnection = this.connections[0];
			}
			FlagBasedTracePoint automatonTracePoint = (this.Client as AutomatonDriverAsCode).Automaton.Context.AutomatonTracePoint;
			if (automatonTracePoint.IsEnabled(TraceFlags.Verbose))
			{
				automatonTracePoint.Trace(TraceFlags.Verbose, string.Format("Automaton: {0}, Connecting to: {1}", this.Client.Name, otherEnd.Name));
			}
			ConnectionLocation connectionLocation = otherEnd.GetConnectionLocation(this.OtherEndConnectionEnumeration);
			AsynchronousConnection onlyConnection = connectionLocation.OnlyConnection;
			asynchronousConnection.OtherEnd = onlyConnection;
			onlyConnection.OtherEnd = asynchronousConnection;
			FlagBasedTracePoint automatonTracePoint2 = (connectionLocation.Client as AutomatonDriverAsCode).Automaton.Context.AutomatonTracePoint;
			if (automatonTracePoint2.IsEnabled(TraceFlags.Verbose))
			{
				automatonTracePoint2.Trace(TraceFlags.Verbose, string.Format("Automaton: {0}, Connected from: {1}", otherEnd.Name, this.Client.Name));
			}
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x0007DF5C File Offset: 0x0007C15C
		public void ConnectTo(int connectionEnumeration, AutomatonDriverAsCode otherEnd, int determinant)
		{
			AsynchronousConnection onlyConnection = this.OnlyConnection;
			FlagBasedTracePoint automatonTracePoint = (this.Client as AutomatonDriverAsCode).Automaton.Context.AutomatonTracePoint;
			if (automatonTracePoint.IsEnabled(TraceFlags.Verbose))
			{
				automatonTracePoint.Trace(TraceFlags.Verbose, string.Format("Automaton: {0}, Connecting to: {1}, with Determinant: {2}", this.Client.Name, otherEnd.Name, determinant.ToString(CultureInfo.InvariantCulture)));
			}
			ConnectionLocation connectionLocation = otherEnd.GetConnectionLocation(this.OtherEndConnectionEnumeration);
			AsynchronousConnection connection = connectionLocation.GetConnection(determinant);
			onlyConnection.OtherEnd = connection;
			connection.OtherEnd = onlyConnection;
			FlagBasedTracePoint automatonTracePoint2 = (connectionLocation.Client as AutomatonDriverAsCode).Automaton.Context.AutomatonTracePoint;
			if (automatonTracePoint2.IsEnabled(TraceFlags.Verbose))
			{
				automatonTracePoint2.Trace(TraceFlags.Verbose, string.Format("Automaton: {0}, Connected from: {1}, Determinant: {2}", otherEnd.Name, this.Client.Name, determinant.ToString(CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x0007E03C File Offset: 0x0007C23C
		internal void DisconnectFrom(int connectionEnumeration)
		{
			AsynchronousConnection onlyConnection = this.OnlyConnection;
			AsynchronousConnection otherEnd = onlyConnection.OtherEnd;
			FlagBasedTracePoint automatonTracePoint = (this.Client as AutomatonDriverAsCode).Automaton.Context.AutomatonTracePoint;
			if (automatonTracePoint.IsEnabled(TraceFlags.Verbose))
			{
				automatonTracePoint.Trace(TraceFlags.Verbose, string.Format("Automaton: {0}, Disconnecting from: {1}", this.Client.Name, otherEnd.Location.Client.Name));
			}
			otherEnd.Remove();
			FlagBasedTracePoint automatonTracePoint2 = (otherEnd.Location.Client as AutomatonDriverAsCode).Automaton.Context.AutomatonTracePoint;
			if (automatonTracePoint2.IsEnabled(TraceFlags.Verbose))
			{
				automatonTracePoint2.Trace(TraceFlags.Verbose, string.Format("Automaton: {0}, Disconnected from: {1}, Determinant: {2}", otherEnd.Location.Client.Name, this.Client.Name));
			}
			onlyConnection.OtherEnd = null;
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x0007E10C File Offset: 0x0007C30C
		public void DisconnectFrom(int connectionEnumeration, int determinant, bool needToInformOtherEnd)
		{
			AsynchronousConnection onlyConnection = this.OnlyConnection;
			AsynchronousConnection otherEnd = onlyConnection.OtherEnd;
			FlagBasedTracePoint automatonTracePoint = (this.Client as AutomatonDriverAsCode).Automaton.Context.AutomatonTracePoint;
			if (automatonTracePoint.IsEnabled(TraceFlags.Verbose))
			{
				automatonTracePoint.Trace(TraceFlags.Verbose, string.Format("Automaton: {0}, Disconnecting from: {1}, Determinant: {2}", this.Client.Name, otherEnd.Location.Client.Name, determinant.ToString(CultureInfo.InvariantCulture)));
			}
			otherEnd.Remove(determinant, needToInformOtherEnd);
			FlagBasedTracePoint automatonTracePoint2 = (otherEnd.Location.Client as AutomatonDriverAsCode).Automaton.Context.AutomatonTracePoint;
			if (automatonTracePoint2.IsEnabled(TraceFlags.Verbose))
			{
				automatonTracePoint2.Trace(TraceFlags.Verbose, string.Format("Automaton: {0}, Disconnected from: {1}, Determinant: {2}", otherEnd.Location.Client.Name, this.Client.Name, determinant.ToString(CultureInfo.InvariantCulture)));
			}
			onlyConnection.OtherEnd = null;
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x0007E1F8 File Offset: 0x0007C3F8
		internal AsynchronousConnection GetConnection(int determinant)
		{
			AsynchronousConnection asynchronousConnection = null;
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.isDynamic)
				{
					if (!this.connectionsDynamic.ContainsKey(determinant))
					{
						asynchronousConnection = new AsynchronousConnection(this);
						this.connectionsDynamic[determinant] = asynchronousConnection;
					}
				}
				else
				{
					asynchronousConnection = this.connections[determinant];
				}
			}
			return asynchronousConnection;
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x0007E26C File Offset: 0x0007C46C
		internal void RemoveConnection()
		{
			this.RemoveConnection(0, false);
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x0007E278 File Offset: 0x0007C478
		internal void RemoveConnection(int determinant, bool needToInformOtherEnd)
		{
			AsynchronousConnection asynchronousConnection = null;
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.isDynamic)
				{
					if (this.connectionsDynamic.ContainsKey(determinant))
					{
						asynchronousConnection = this.connectionsDynamic[determinant];
						this.connectionsDynamic.Remove(determinant);
						if (this.ProcessDisconnect != null && needToInformOtherEnd)
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(ConnectionLocation.ProcessDisconnectInThread), this);
						}
					}
				}
				else
				{
					asynchronousConnection = this.connections[determinant];
					this.connections[determinant] = null;
				}
			}
			asynchronousConnection.OtherEnd = null;
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x0007E320 File Offset: 0x0007C520
		public static void ProcessDisconnectInThread(object context)
		{
			(context as ConnectionLocation).ProcessDisconnectInThread();
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x0007E32D File Offset: 0x0007C52D
		public void ProcessDisconnectInThread()
		{
			this.ProcessDisconnect();
		}

		// Token: 0x04001896 RID: 6294
		private AsynchronousConnection[] connections;

		// Token: 0x04001897 RID: 6295
		private Dictionary<int, AsynchronousConnection> connectionsDynamic;

		// Token: 0x04001898 RID: 6296
		private volatile List<AsynchronousConnectionMessage> messages = new List<AsynchronousConnectionMessage>();

		// Token: 0x04001899 RID: 6297
		private bool isDynamic;

		// Token: 0x0400189A RID: 6298
		private object lockObject = new object();
	}
}
