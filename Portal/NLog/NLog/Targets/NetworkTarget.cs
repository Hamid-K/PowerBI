using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using NLog.Common;
using NLog.Internal;
using NLog.Internal.NetworkSenders;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x0200004A RID: 74
	[Target("Network")]
	public class NetworkTarget : TargetWithLayout
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x000120B8 File Offset: 0x000102B8
		public NetworkTarget()
		{
			this.SenderFactory = NetworkSenderFactory.Default;
			this.Encoding = Encoding.UTF8;
			this.OnOverflow = NetworkTargetOverflowAction.Split;
			this.KeepConnection = true;
			this.MaxMessageSize = 65000;
			this.ConnectionCacheSize = 5;
			this.LineEnding = LineEndingMode.CRLF;
			base.OptimizeBufferReuse = base.GetType() == typeof(NetworkTarget);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001214D File Offset: 0x0001034D
		public NetworkTarget(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001215C File Offset: 0x0001035C
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x00012164 File Offset: 0x00010364
		public Layout Address { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001216D File Offset: 0x0001036D
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x00012175 File Offset: 0x00010375
		[DefaultValue(true)]
		public bool KeepConnection { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x0001217E File Offset: 0x0001037E
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x00012186 File Offset: 0x00010386
		[DefaultValue(false)]
		public bool NewLine { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0001218F File Offset: 0x0001038F
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x00012197 File Offset: 0x00010397
		[DefaultValue("CRLF")]
		public LineEndingMode LineEnding { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x000121A0 File Offset: 0x000103A0
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x000121A8 File Offset: 0x000103A8
		[DefaultValue(65000)]
		public int MaxMessageSize { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x000121B1 File Offset: 0x000103B1
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x000121B9 File Offset: 0x000103B9
		[DefaultValue(5)]
		public int ConnectionCacheSize { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x000121C2 File Offset: 0x000103C2
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x000121CA File Offset: 0x000103CA
		public int MaxConnections { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x000121D3 File Offset: 0x000103D3
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x000121DB File Offset: 0x000103DB
		public NetworkTargetConnectionsOverflowAction OnConnectionOverflow { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x000121E4 File Offset: 0x000103E4
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x000121EC File Offset: 0x000103EC
		[DefaultValue(0)]
		public int MaxQueueSize { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x000121F5 File Offset: 0x000103F5
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x000121FD File Offset: 0x000103FD
		[DefaultValue(NetworkTargetOverflowAction.Split)]
		public NetworkTargetOverflowAction OnOverflow { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x00012206 File Offset: 0x00010406
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x0001220E File Offset: 0x0001040E
		[DefaultValue("utf-8")]
		public Encoding Encoding { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x00012217 File Offset: 0x00010417
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0001221F File Offset: 0x0001041F
		public SslProtocols SslProtocols { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00012228 File Offset: 0x00010428
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x00012230 File Offset: 0x00010430
		public int KeepAliveTimeSeconds { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00012239 File Offset: 0x00010439
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x00012241 File Offset: 0x00010441
		internal INetworkSenderFactory SenderFactory { get; set; }

		// Token: 0x06000757 RID: 1879 RVA: 0x0001224C File Offset: 0x0001044C
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			NetworkTarget.<>c__DisplayClass61_0 CS$<>8__locals1 = new NetworkTarget.<>c__DisplayClass61_0();
			CS$<>8__locals1.asyncContinuation = asyncContinuation;
			LinkedList<NetworkSender> openNetworkSenders = this._openNetworkSenders;
			lock (openNetworkSenders)
			{
				CS$<>8__locals1.remainingCount = this._openNetworkSenders.Count;
				if (CS$<>8__locals1.remainingCount == 0)
				{
					CS$<>8__locals1.asyncContinuation(null);
				}
				else
				{
					foreach (NetworkSender networkSender in this._openNetworkSenders)
					{
						networkSender.FlushAsync(new AsyncContinuation(CS$<>8__locals1.<FlushAsync>g__Continuation|0));
					}
				}
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00012304 File Offset: 0x00010504
		protected override void CloseTarget()
		{
			base.CloseTarget();
			LinkedList<NetworkSender> openNetworkSenders = this._openNetworkSenders;
			lock (openNetworkSenders)
			{
				using (LinkedList<NetworkSender>.Enumerator enumerator = this._openNetworkSenders.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						enumerator.Current.Close(delegate(Exception ex)
						{
						});
					}
				}
				this._openNetworkSenders.Clear();
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000123B0 File Offset: 0x000105B0
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			NetworkTarget.<>c__DisplayClass63_0 CS$<>8__locals1 = new NetworkTarget.<>c__DisplayClass63_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.logEvent = logEvent;
			string text = base.RenderLogEvent(this.Address, CS$<>8__locals1.logEvent.LogEvent);
			InternalLogger.Trace<string, string>("NetworkTarget(Name={0}): Sending to address: '{1}'", base.Name, text);
			byte[] bytesToWrite = this.GetBytesToWrite(CS$<>8__locals1.logEvent.LogEvent);
			if (this.KeepConnection)
			{
				LinkedListNode<NetworkSender> senderNode;
				try
				{
					senderNode = this.GetCachedNetworkSender(text);
				}
				catch (Exception ex)
				{
					Exception ex3;
					InternalLogger.Error(ex3, "NetworkTarget(Name={0}): Failed to create sender to address: '{1}'", new object[] { base.Name, text });
					throw;
				}
				this.ChunkedSend(senderNode.Value, bytesToWrite, delegate(Exception ex)
				{
					if (ex != null)
					{
						InternalLogger.Error(ex, "NetworkTarget(Name={0}): Error when sending.", new object[] { CS$<>8__locals1.<>4__this.Name });
						CS$<>8__locals1.<>4__this.ReleaseCachedConnection(senderNode);
					}
					CS$<>8__locals1.logEvent.Continuation(ex);
				});
				return;
			}
			LinkedList<NetworkSender> openNetworkSenders = this._openNetworkSenders;
			LinkedListNode<NetworkSender> linkedListNode;
			NetworkSender sender;
			lock (openNetworkSenders)
			{
				if (this._openNetworkSenders.Count >= this.MaxConnections && this.MaxConnections > 0)
				{
					switch (this.OnConnectionOverflow)
					{
					case NetworkTargetConnectionsOverflowAction.AllowNewConnnection:
						InternalLogger.Debug<string>("NetworkTarget(Name={0}): Too may connections, but this is allowed", base.Name);
						break;
					case NetworkTargetConnectionsOverflowAction.DiscardMessage:
						InternalLogger.Warn<string>("NetworkTarget(Name={0}): Discarding message otherwise to many connections.", base.Name);
						CS$<>8__locals1.logEvent.Continuation(null);
						return;
					case NetworkTargetConnectionsOverflowAction.Block:
						while (this._openNetworkSenders.Count >= this.MaxConnections)
						{
							InternalLogger.Debug<string>("NetworkTarget(Name={0}): Blocking networktarget otherwhise too many connections.", base.Name);
							Monitor.Wait(this._openNetworkSenders);
							InternalLogger.Trace<string>("NetworkTarget(Name={0}): Entered critical section.", base.Name);
						}
						InternalLogger.Trace<string>("NetworkTarget(Name={0}): Limit ok.", base.Name);
						break;
					}
				}
				try
				{
					sender = this.CreateNetworkSender(text);
				}
				catch (Exception ex2)
				{
					InternalLogger.Error(ex2, "NetworkTarget(Name={0}): Failed to create sender to address: '{1}'", new object[] { base.Name, text });
					throw;
				}
				linkedListNode = this._openNetworkSenders.AddLast(sender);
			}
			this.ChunkedSend(sender, bytesToWrite, delegate(Exception ex)
			{
				LinkedList<NetworkSender> openNetworkSenders2 = CS$<>8__locals1.<>4__this._openNetworkSenders;
				lock (openNetworkSenders2)
				{
					NetworkTarget.TryRemove<NetworkSender>(CS$<>8__locals1.<>4__this._openNetworkSenders, linkedListNode);
					if (CS$<>8__locals1.<>4__this.OnConnectionOverflow == NetworkTargetConnectionsOverflowAction.Block)
					{
						Monitor.PulseAll(CS$<>8__locals1.<>4__this._openNetworkSenders);
					}
				}
				if (ex != null)
				{
					InternalLogger.Error(ex, "NetworkTarget(Name={0}): Error when sending.", new object[] { CS$<>8__locals1.<>4__this.Name });
				}
				sender.Close(delegate(Exception ex2)
				{
				});
				CS$<>8__locals1.logEvent.Continuation(ex);
			});
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00012624 File Offset: 0x00010824
		private static bool TryRemove<T>(LinkedList<T> list, LinkedListNode<T> node)
		{
			if (node == null || list != node.List)
			{
				return false;
			}
			list.Remove(node);
			return true;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001263C File Offset: 0x0001083C
		protected virtual byte[] GetBytesToWrite(LogEventInfo logEvent)
		{
			if (base.OptimizeBufferReuse)
			{
				object obj;
				if (!this.NewLine && logEvent.TryGetCachedLayoutValue(this.Layout, out obj))
				{
					InternalLogger.Trace<string, object>("NetworkTarget(Name={0}): Sending {1}", base.Name, obj);
					return this.Encoding.GetBytes(obj.ToString());
				}
				using (ReusableObjectCreator<StringBuilder>.LockOject lockOject = this.ReusableLayoutBuilder.Allocate())
				{
					this.Layout.RenderAppendBuilder(logEvent, lockOject.Result, false);
					if (this.NewLine)
					{
						lockOject.Result.Append(this.LineEnding.NewLineCharacters);
					}
					InternalLogger.Trace<string, int>("NetworkTarget(Name={0}): Sending {1} chars", base.Name, lockOject.Result.Length);
					using (ReusableObjectCreator<char[]>.LockOject lockOject2 = this._reusableEncodingBuffer.Allocate())
					{
						if (lockOject.Result.Length <= lockOject2.Result.Length)
						{
							lockOject.Result.CopyTo(0, lockOject2.Result, 0, lockOject.Result.Length);
							return this.Encoding.GetBytes(lockOject2.Result, 0, lockOject.Result.Length);
						}
						string text = lockOject.Result.ToString();
						return this.Encoding.GetBytes(text);
					}
				}
			}
			string text2 = this.Layout.Render(logEvent);
			InternalLogger.Trace<string, string>("NetworkTarget(Name={0}): Sending: {1}", base.Name, text2);
			if (this.NewLine)
			{
				text2 += this.LineEnding.NewLineCharacters;
			}
			return this.Encoding.GetBytes(text2);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000127EC File Offset: 0x000109EC
		private LinkedListNode<NetworkSender> GetCachedNetworkSender(string address)
		{
			Dictionary<string, LinkedListNode<NetworkSender>> currentSenderCache = this._currentSenderCache;
			LinkedListNode<NetworkSender> linkedListNode2;
			lock (currentSenderCache)
			{
				LinkedListNode<NetworkSender> linkedListNode;
				if (this._currentSenderCache.TryGetValue(address, out linkedListNode))
				{
					linkedListNode.Value.CheckSocket();
					linkedListNode2 = linkedListNode;
				}
				else
				{
					if (this._currentSenderCache.Count >= this.ConnectionCacheSize)
					{
						int num = int.MaxValue;
						LinkedListNode<NetworkSender> linkedListNode3 = null;
						foreach (KeyValuePair<string, LinkedListNode<NetworkSender>> keyValuePair in this._currentSenderCache)
						{
							NetworkSender value = keyValuePair.Value.Value;
							if (value.LastSendTime < num)
							{
								num = value.LastSendTime;
								linkedListNode3 = keyValuePair.Value;
							}
						}
						if (linkedListNode3 != null)
						{
							this.ReleaseCachedConnection(linkedListNode3);
						}
					}
					NetworkSender networkSender = this.CreateNetworkSender(address);
					LinkedList<NetworkSender> openNetworkSenders = this._openNetworkSenders;
					lock (openNetworkSenders)
					{
						linkedListNode = this._openNetworkSenders.AddLast(networkSender);
					}
					this._currentSenderCache.Add(address, linkedListNode);
					linkedListNode2 = linkedListNode;
				}
			}
			return linkedListNode2;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00012930 File Offset: 0x00010B30
		private NetworkSender CreateNetworkSender(string address)
		{
			NetworkSender networkSender = this.SenderFactory.Create(address, this.MaxQueueSize, this.SslProtocols, TimeSpan.FromSeconds((double)this.KeepAliveTimeSeconds));
			networkSender.Initialize();
			return networkSender;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001295C File Offset: 0x00010B5C
		private void ReleaseCachedConnection(LinkedListNode<NetworkSender> senderNode)
		{
			Dictionary<string, LinkedListNode<NetworkSender>> currentSenderCache = this._currentSenderCache;
			lock (currentSenderCache)
			{
				NetworkSender value = senderNode.Value;
				LinkedList<NetworkSender> openNetworkSenders = this._openNetworkSenders;
				lock (openNetworkSenders)
				{
					if (NetworkTarget.TryRemove<NetworkSender>(this._openNetworkSenders, senderNode))
					{
						value.Close(delegate(Exception ex)
						{
						});
					}
				}
				LinkedListNode<NetworkSender> linkedListNode;
				if (this._currentSenderCache.TryGetValue(value.Address, out linkedListNode) && senderNode == linkedListNode)
				{
					this._currentSenderCache.Remove(value.Address);
				}
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00012A2C File Offset: 0x00010C2C
		private void ChunkedSend(NetworkSender sender, byte[] buffer, AsyncContinuation continuation)
		{
			NetworkTarget.<>c__DisplayClass69_0 CS$<>8__locals1 = new NetworkTarget.<>c__DisplayClass69_0();
			CS$<>8__locals1.continuation = continuation;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.buffer = buffer;
			CS$<>8__locals1.sender = sender;
			CS$<>8__locals1.tosend = CS$<>8__locals1.buffer.Length;
			if (CS$<>8__locals1.tosend > this.MaxMessageSize)
			{
				NetworkTarget.<>c__DisplayClass69_1 CS$<>8__locals2 = new NetworkTarget.<>c__DisplayClass69_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.pos = 0;
				CS$<>8__locals2.<ChunkedSend>g__SendNextChunk|0(null);
				return;
			}
			InternalLogger.Trace<int, int>("Sending chunk, position: {0}, length: {1}", 0, CS$<>8__locals1.tosend);
			if (CS$<>8__locals1.tosend <= 0)
			{
				CS$<>8__locals1.continuation(null);
				return;
			}
			CS$<>8__locals1.sender.Send(CS$<>8__locals1.buffer, 0, CS$<>8__locals1.tosend, CS$<>8__locals1.continuation);
		}

		// Token: 0x04000154 RID: 340
		private readonly Dictionary<string, LinkedListNode<NetworkSender>> _currentSenderCache = new Dictionary<string, LinkedListNode<NetworkSender>>();

		// Token: 0x04000155 RID: 341
		private readonly LinkedList<NetworkSender> _openNetworkSenders = new LinkedList<NetworkSender>();

		// Token: 0x04000156 RID: 342
		private readonly ReusableBufferCreator _reusableEncodingBuffer = new ReusableBufferCreator(16384);
	}
}
