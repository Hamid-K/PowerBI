using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using Microsoft.HostIntegration.EventLogging;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.MqClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AF9 RID: 2809
	public class Pooling : IPooling
	{
		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x060058F2 RID: 22770 RVA: 0x0016E891 File Offset: 0x0016CA91
		// (set) Token: 0x060058F3 RID: 22771 RVA: 0x0016E898 File Offset: 0x0016CA98
		public bool Pool
		{
			get
			{
				return Pooling.pool;
			}
			set
			{
				if (Pooling.pool && !value)
				{
					int num = Pooling.timeout;
					Pooling.cleanupTimer.Change(-1, -1);
					Pooling.cleanupTimer = null;
					Pooling.timeout = 0;
					Pooling.CleanupProcedure(null);
					Pooling.timeout = num;
				}
				if (value)
				{
					if (Pooling.cleanupTimer != null)
					{
						Pooling.cleanupTimer.Change(Pooling.timerInterval, Pooling.timerInterval);
					}
					else
					{
						Pooling.cleanupTimer = new Timer(new TimerCallback(Pooling.CleanupProcedure), null, Pooling.timerInterval, Pooling.timerInterval);
					}
				}
				Pooling.pool = value;
			}
		}

		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x060058F4 RID: 22772 RVA: 0x0016E920 File Offset: 0x0016CB20
		// (set) Token: 0x060058F5 RID: 22773 RVA: 0x0016E928 File Offset: 0x0016CB28
		public int Timeout
		{
			get
			{
				return Pooling.timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				Pooling.timeout = value;
				if (Pooling.timeout != 0)
				{
					int num = 1000;
					if (Pooling.timeout > 4)
					{
						num = Pooling.timeout * 250;
					}
					Pooling.timerInterval = num;
					Pooling.timeoutTimespan = new TimeSpan((long)Pooling.timeout * 10000000L);
					if (this.Pool)
					{
						if (Pooling.cleanupTimer != null)
						{
							Pooling.cleanupTimer.Change(Pooling.timerInterval, Pooling.timerInterval);
							return;
						}
						Pooling.cleanupTimer = new Timer(new TimerCallback(Pooling.CleanupProcedure), null, Pooling.timerInterval, Pooling.timerInterval);
					}
					return;
				}
				if (Pooling.cleanupTimer == null)
				{
					return;
				}
				Pooling.cleanupTimer.Change(-1, -1);
				Pooling.cleanupTimer = null;
				Pooling.CleanupProcedure(null);
			}
		}

		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x060058F6 RID: 22774 RVA: 0x0016E9F4 File Offset: 0x0016CBF4
		// (set) Token: 0x060058F7 RID: 22775 RVA: 0x0016E9FC File Offset: 0x0016CBFC
		public int QueueManagersPerConversation
		{
			get
			{
				return Pooling.queueManagersPerConversation;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				Pooling.queueManagersPerConversation = value;
				if (Pooling.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					Pooling.tracePoint.Trace(TraceFlags.Verbose, "QueueManagersPerConversation: " + Pooling.queueManagersPerConversation.ToString(CultureInfo.InvariantCulture));
				}
			}
		}

		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x060058F8 RID: 22776 RVA: 0x0016EA51 File Offset: 0x0016CC51
		// (set) Token: 0x060058F9 RID: 22777 RVA: 0x0016EA58 File Offset: 0x0016CC58
		public bool AllowDifferentChannels
		{
			get
			{
				return Pooling.allowDifferentChannels;
			}
			set
			{
				Pooling.allowDifferentChannels = value;
			}
		}

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x060058FA RID: 22778 RVA: 0x0016EA60 File Offset: 0x0016CC60
		// (set) Token: 0x060058FB RID: 22779 RVA: 0x0016EA67 File Offset: 0x0016CC67
		public bool OneUserPerConversation
		{
			get
			{
				return Pooling.oneUserPerConversation;
			}
			set
			{
				Pooling.oneUserPerConversation = value;
			}
		}

		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x060058FC RID: 22780 RVA: 0x0016EA6F File Offset: 0x0016CC6F
		// (set) Token: 0x060058FD RID: 22781 RVA: 0x0016EA76 File Offset: 0x0016CC76
		internal static EventLogContainer SingletonEventLogContainer { get; private set; }

		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x060058FE RID: 22782 RVA: 0x0016EA7E File Offset: 0x0016CC7E
		// (set) Token: 0x060058FF RID: 22783 RVA: 0x0016EA85 File Offset: 0x0016CC85
		public EventLogContainer EventLogContainer
		{
			get
			{
				return Pooling.SingletonEventLogContainer;
			}
			set
			{
				Pooling.SingletonEventLogContainer = value;
			}
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x0016EAC8 File Offset: 0x0016CCC8
		private static void CleanupProcedure(object state)
		{
			DateTime now = DateTime.Now;
			DateTime dateTime;
			if (Pooling.timeout == 0)
			{
				dateTime = now.AddYears(1);
			}
			else
			{
				dateTime = now.Subtract(Pooling.timeoutTimespan);
			}
			if (Pooling.tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				if (Pooling.timeout == 0)
				{
					Pooling.tracePoint.Trace(TraceFlags.Verbose, "Clearing timeout collection, timeout = 0");
				}
				else
				{
					Pooling.tracePoint.Trace(TraceFlags.Verbose, "Checking timeout collection, then : " + dateTime.ToLongTimeString());
				}
			}
			List<WrappedPooledQueueManager> list = new List<WrappedPooledQueueManager>();
			object obj = Pooling.lockObject;
			lock (obj)
			{
				foreach (WrappedPooledQueueManager wrappedPooledQueueManager in Pooling.queueManagersTimingOut)
				{
					if (wrappedPooledQueueManager.TimeLastUsed < dateTime)
					{
						list.Add(wrappedPooledQueueManager);
					}
				}
				foreach (WrappedPooledQueueManager wrappedPooledQueueManager2 in list)
				{
					Pooling.queueManagersTimingOut.Remove(wrappedPooledQueueManager2);
					wrappedPooledQueueManager2.BeingDeleted = true;
				}
				foreach (WrappedPooledQueueManager wrappedPooledQueueManager3 in list)
				{
					if (wrappedPooledQueueManager3 != null)
					{
						NameQueueManager nameQueueManager = wrappedPooledQueueManager3.NameQueueManager;
						if (Pooling.tracePoint.IsEnabled(TraceFlags.Information))
						{
							Pooling.tracePoint.Trace(TraceFlags.Information, string.Format("Timed-out {0}", wrappedPooledQueueManager3));
						}
						nameQueueManager.QueueManagers.Remove(wrappedPooledQueueManager3);
						wrappedPooledQueueManager3.AlreadyRemoved = true;
						int num = (int)wrappedPooledQueueManager3.QueueManager.Disconnect();
						if (num != 0 && Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
						{
							Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("Disconnect RC: {0}", num));
						}
						ChannelQueueManager channelQueueManager = nameQueueManager.ChannelQueueManager;
						ChannelQueueManagerCollection channelQueueManagerCollection = channelQueueManager.ChannelQueueManagerCollection;
						bool flag2 = channelQueueManager.ReleaseAutomatonQm() != 0;
						if (Pooling.tracePoint.IsEnabled(TraceFlags.Verbose))
						{
							Pooling.tracePoint.Trace(TraceFlags.Verbose, string.Format("ChannelQueueManager {0}", channelQueueManager));
						}
						if (!flag2)
						{
							if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
							{
								Pooling.tracePoint.Trace(TraceFlags.Debug, "Disconnecting Socket");
							}
							if (wrappedPooledQueueManager3.QueueManager != null && wrappedPooledQueueManager3.QueueManager.Connection != null)
							{
								int num2 = (int)wrappedPooledQueueManager3.QueueManager.Connection.Disconnect();
								if (num2 != 0 && Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
								{
									Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("Disconnect Socket RC: {0}", num2));
								}
							}
							channelQueueManagerCollection.ChannelQueueManagers.Remove(channelQueueManager);
							channelQueueManager.AlreadyRemoved = true;
						}
					}
				}
			}
		}

		// Token: 0x06005902 RID: 22786 RVA: 0x0016EDD8 File Offset: 0x0016CFD8
		public IPooledQueue AcquireQueue(QueueConnectionParameters qParameters, IWrappedPooledQueueManager iWrappedPooledQueueManager)
		{
			WrappedPooledQueueManager wrappedPooledQueueManager = iWrappedPooledQueueManager as WrappedPooledQueueManager;
			return new PooledQueue(qParameters, wrappedPooledQueueManager);
		}

		// Token: 0x06005903 RID: 22787 RVA: 0x0016EDF4 File Offset: 0x0016CFF4
		public ReturnCode AcquireQueueManager(QueueManagerConnectionParameters qmParameters, TcpConnectionParameters tcpParameters, out IWrappedPooledQueueManager wrappedQueueManager)
		{
			WindowsIdentity windowsIdentity = null;
			wrappedQueueManager = null;
			if (Pooling.tracePoint.IsEnabled(TraceFlags.Information))
			{
				Pooling.tracePoint.Trace(TraceFlags.Information, string.Format("Acquire Name: {0}, Channel: {1}, Server: '{2}', Port: {3}", new object[] { qmParameters.Name, qmParameters.Channel, tcpParameters.Server, tcpParameters.Port }));
			}
			try
			{
				if (!this.Pool || qmParameters.IsTransactional)
				{
					PooledConnection pooledConnection = new PooledConnection(tcpParameters, null);
					ReturnCode returnCode = pooledConnection.Connect();
					if (returnCode != ReturnCode.Ok)
					{
						if (Pooling.tracePoint.IsEnabled(TraceFlags.Error))
						{
							Pooling.tracePoint.Trace(TraceFlags.Error, string.Format("Failed to get a non-pooled connection, Code: {0}", returnCode));
						}
						return returnCode;
					}
					PooledQueueManager pooledQueueManager = new PooledQueueManager(pooledConnection.NextId, qmParameters, pooledConnection);
					WrappedPooledQueueManager wrappedPooledQueueManager = new WrappedPooledQueueManager(pooledQueueManager, null);
					pooledQueueManager.WrappedPooledQueueManager = null;
					windowsIdentity = WindowsIdentity.GetCurrent();
					ReturnCode returnCode2 = pooledQueueManager.Connect(windowsIdentity);
					if (returnCode2 != ReturnCode.Ok)
					{
						if (Pooling.tracePoint.IsEnabled(TraceFlags.Error))
						{
							Pooling.tracePoint.Trace(TraceFlags.Error, string.Format("Failed to get a non-pooled Queue Manager, Code: {0}", returnCode2));
						}
						ReturnCode returnCode3 = pooledConnection.Disconnect();
						if (returnCode3 != ReturnCode.Ok && Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
						{
							Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("Disconnect Code: {0}", returnCode3));
						}
						return returnCode2;
					}
					wrappedQueueManager = wrappedPooledQueueManager;
					if (Pooling.tracePoint.IsEnabled(TraceFlags.Information))
					{
						Pooling.tracePoint.Trace(TraceFlags.Information, string.Format("Got (non-pooled) {0}", wrappedPooledQueueManager));
					}
					return ReturnCode.Ok;
				}
				else
				{
					string text = (Pooling.allowDifferentChannels ? "All" : qmParameters.Channel);
					string text2;
					if (Pooling.oneUserPerConversation)
					{
						windowsIdentity = WindowsIdentity.GetCurrent();
						if (qmParameters.ConnectAs == null && qmParameters.AuthorizationUser == null)
						{
							text2 = "ID:" + windowsIdentity.Name;
						}
						else if (qmParameters.AuthorizationUser == null)
						{
							text2 = "CA:" + qmParameters.ConnectAs;
						}
						else
						{
							text2 = ((qmParameters.ConnectAs == null) ? ("AU:" + qmParameters.AuthorizationUser) : ("CU:" + qmParameters.ConnectAs + "/" + qmParameters.AuthorizationUser));
						}
					}
					else
					{
						text2 = "Everybody";
					}
					if (Pooling.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						Pooling.tracePoint.Trace(TraceFlags.Verbose, string.Concat(new string[] { "User: '", text2, "', Channel: '", text, "'" }));
					}
					ReturnCode orCreateChannelQueueManager;
					WrappedPooledQueueManager wrappedPooledQueueManager2;
					for (;;)
					{
						ChannelQueueManager channelQueueManager;
						orCreateChannelQueueManager = Pooling.GetOrCreateChannelQueueManager(text2, tcpParameters, text, out channelQueueManager);
						if (orCreateChannelQueueManager != ReturnCode.Ok)
						{
							break;
						}
						if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
						{
							Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("Channel QM {0}", channelQueueManager));
						}
						Dictionary<string, NameQueueManager> nameToNameQueueManagers = channelQueueManager.NameToNameQueueManagers;
						PooledConnection connection = channelQueueManager.Connection;
						NameQueueManager nameQueueManager = null;
						Dictionary<string, NameQueueManager> dictionary = nameToNameQueueManagers;
						lock (dictionary)
						{
							if (!nameToNameQueueManagers.TryGetValue(qmParameters.Name, out nameQueueManager))
							{
								if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
								{
									Pooling.tracePoint.Trace(TraceFlags.Debug, "Generating new NameQueueManager instance for QM Name: '" + qmParameters.Name + "'");
								}
								nameQueueManager = new NameQueueManager(qmParameters.Name, channelQueueManager);
								nameToNameQueueManagers.Add(qmParameters.Name, nameQueueManager);
							}
						}
						wrappedPooledQueueManager2 = null;
						object obj = Pooling.lockObject;
						lock (obj)
						{
							bool flag2 = false;
							foreach (WrappedPooledQueueManager wrappedPooledQueueManager3 in nameQueueManager.QueueManagers)
							{
								if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
								{
									Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("Trying (PQM): {0}", wrappedPooledQueueManager3));
								}
								if (wrappedPooledQueueManager3.ReferenceCount < Pooling.queueManagersPerConversation)
								{
									wrappedPooledQueueManager2 = wrappedPooledQueueManager3;
									wrappedPooledQueueManager3.AddReference();
									break;
								}
							}
							if (wrappedPooledQueueManager2 == null)
							{
								if (!connection.IsConnected)
								{
									if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
									{
										Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("Connection: {0} is not connected", connection));
									}
									continue;
								}
								PooledQueueManager pooledQueueManager2 = new PooledQueueManager(connection.NextId, qmParameters, connection);
								wrappedPooledQueueManager2 = new WrappedPooledQueueManager(pooledQueueManager2, nameQueueManager);
								if (Pooling.tracePoint.IsEnabled(TraceFlags.Verbose))
								{
									Pooling.tracePoint.Trace(TraceFlags.Verbose, string.Format("Generating new WrappedPooledQueueManager {0}", wrappedPooledQueueManager2));
								}
								if (windowsIdentity == null)
								{
									windowsIdentity = WindowsIdentity.GetCurrent();
								}
								if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
								{
									Pooling.tracePoint.Trace(TraceFlags.Debug, "Connecting new WrappedPooledQueueManager");
								}
								ReturnCode returnCode4 = pooledQueueManager2.Connect(windowsIdentity);
								if (returnCode4 != ReturnCode.Ok)
								{
									if (Pooling.tracePoint.IsEnabled(TraceFlags.Error))
									{
										Pooling.tracePoint.Trace(TraceFlags.Error, string.Format("WrappedPooledQueueManager Connect Code: {0}", returnCode4));
									}
									channelQueueManager.Release();
									if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
									{
										Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("ChannelQueueManager {0}", channelQueueManager));
									}
									if (channelQueueManager.AutomatonQmReferenceCount == 0)
									{
										if (!channelQueueManager.AlreadyRemoved)
										{
											channelQueueManager.AlreadyRemoved = true;
											channelQueueManager.ChannelQueueManagerCollection.ChannelQueueManagers.Remove(channelQueueManager);
										}
										int num = (int)channelQueueManager.Connection.Disconnect();
										if (num != 0 && Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
										{
											Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("Disconnect Socket RC: {0}", num));
										}
									}
									if (Pooling.tracePoint.IsEnabled(TraceFlags.Information))
									{
										Pooling.tracePoint.Trace(TraceFlags.Information, "Failed to get a pooled Queue Manager");
									}
									return returnCode4;
								}
								nameQueueManager.QueueManagers.Add(wrappedPooledQueueManager2);
								wrappedPooledQueueManager2.CreatedPooled = true;
								wrappedPooledQueueManager2.AddReference();
								channelQueueManager.AddAutomatonQmReference();
							}
							else
							{
								flag2 = true;
								if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
								{
									Pooling.tracePoint.Trace(TraceFlags.Debug, "Re-using QM");
								}
								if (wrappedPooledQueueManager2.TimingOut)
								{
									if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
									{
										Pooling.tracePoint.Trace(TraceFlags.Debug, "Removing instance from TimingOut collection");
									}
									Pooling.queueManagersTimingOut.Remove(wrappedPooledQueueManager2);
									wrappedPooledQueueManager2.TimingOut = false;
								}
							}
							if (Pooling.tracePoint.IsEnabled(TraceFlags.Information))
							{
								if (flag2)
								{
									Pooling.tracePoint.Trace(TraceFlags.Information, string.Format("Got (Re-Used) {0}", wrappedPooledQueueManager2));
								}
								else
								{
									Pooling.tracePoint.Trace(TraceFlags.Information, string.Format("Got (Created) {0}", wrappedPooledQueueManager2));
								}
								Pooling.tracePoint.Trace(TraceFlags.Information, string.Format("On Channel QM {0}", channelQueueManager));
							}
						}
						goto IL_065E;
					}
					if (Pooling.tracePoint.IsEnabled(TraceFlags.Error))
					{
						Pooling.tracePoint.Trace(TraceFlags.Error, string.Format("Failed to get a pooled connection, Code: {0}", orCreateChannelQueueManager));
					}
					return orCreateChannelQueueManager;
					IL_065E:
					wrappedQueueManager = wrappedPooledQueueManager2;
				}
			}
			catch (Exception ex)
			{
				if (Pooling.tracePoint.IsEnabled(TraceFlags.Error))
				{
					Pooling.tracePoint.Trace(TraceFlags.Error, ex);
				}
				throw;
			}
			return ReturnCode.Ok;
		}

		// Token: 0x06005904 RID: 22788 RVA: 0x0016F4EC File Offset: 0x0016D6EC
		public ReturnCode ReturnQueueManager(IWrappedPooledQueueManager iWrappedQueueManager)
		{
			WrappedPooledQueueManager wrappedPooledQueueManager = iWrappedQueueManager as WrappedPooledQueueManager;
			if (Pooling.tracePoint.IsEnabled(TraceFlags.Information))
			{
				Pooling.tracePoint.Trace(TraceFlags.Information, string.Format("Returning {0}", wrappedPooledQueueManager));
			}
			object obj = Pooling.lockObject;
			lock (obj)
			{
				if (this.Pool && wrappedPooledQueueManager.CreatedPooled)
				{
					ChannelQueueManager channelQueueManager = wrappedPooledQueueManager.NameQueueManager.ChannelQueueManager;
					channelQueueManager.Release();
					if (Pooling.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						Pooling.tracePoint.Trace(TraceFlags.Verbose, string.Format("ChannelQueueManager {0}", channelQueueManager));
					}
				}
				if (!this.Pool || !wrappedPooledQueueManager.CreatedPooled)
				{
					ReturnCode returnCode = wrappedPooledQueueManager.QueueManager.Disconnect();
					if (returnCode != ReturnCode.Ok && Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
					{
						Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("Disconnect RC: {0}", returnCode));
					}
					ReturnCode returnCode2 = wrappedPooledQueueManager.QueueManager.Connection.Disconnect();
					if (returnCode2 != ReturnCode.Ok && Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
					{
						Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("Disconnect Socket RC: {0}", returnCode2));
					}
					if (Pooling.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						Pooling.tracePoint.Trace(TraceFlags.Verbose, "Returned with no pooling");
					}
					return ReturnCode.Ok;
				}
				if (wrappedPooledQueueManager.Release() != 0)
				{
					if (Pooling.tracePoint.IsEnabled(TraceFlags.Information))
					{
						Pooling.tracePoint.Trace(TraceFlags.Information, string.Format("Returned (non-zero) {0}", wrappedPooledQueueManager));
					}
					return ReturnCode.Ok;
				}
				if (this.Timeout == 0 || wrappedPooledQueueManager.AlreadyRemoved)
				{
					if (Pooling.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						Pooling.tracePoint.Trace(TraceFlags.Verbose, "Timeout 0, or already removed");
					}
					ReturnCode returnCode3 = wrappedPooledQueueManager.QueueManager.Disconnect();
					if (returnCode3 != ReturnCode.Ok && Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
					{
						Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("Disconnect RC: {0}", returnCode3));
					}
					NameQueueManager nameQueueManager = wrappedPooledQueueManager.NameQueueManager;
					if (!wrappedPooledQueueManager.AlreadyRemoved)
					{
						nameQueueManager.QueueManagers.Remove(wrappedPooledQueueManager);
						wrappedPooledQueueManager.AlreadyRemoved = true;
					}
					ChannelQueueManager channelQueueManager2 = nameQueueManager.ChannelQueueManager;
					bool flag2 = channelQueueManager2.ReleaseAutomatonQm() != 0;
					if (Pooling.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						Pooling.tracePoint.Trace(TraceFlags.Verbose, string.Format("ChannelQueueManager {0}", channelQueueManager2));
					}
					if (!flag2)
					{
						if (!channelQueueManager2.AlreadyRemoved)
						{
							if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
							{
								Pooling.tracePoint.Trace(TraceFlags.Debug, "Channel QM already removed");
							}
							channelQueueManager2.ChannelQueueManagerCollection.ChannelQueueManagers.Remove(channelQueueManager2);
							channelQueueManager2.AlreadyRemoved = true;
						}
						ReturnCode returnCode4 = wrappedPooledQueueManager.QueueManager.Connection.Disconnect();
						if (returnCode4 != ReturnCode.Ok && Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
						{
							Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("Disconnect Socket RC: {0}", returnCode4));
						}
					}
					if (Pooling.tracePoint.IsEnabled(TraceFlags.Information))
					{
						Pooling.tracePoint.Trace(TraceFlags.Information, string.Format("Returned (removed) {0}", wrappedPooledQueueManager));
					}
					return ReturnCode.Ok;
				}
				wrappedPooledQueueManager.TimingOut = true;
				wrappedPooledQueueManager.TimeLastUsed = DateTime.Now;
				Pooling.queueManagersTimingOut.Add(wrappedPooledQueueManager);
				if (Pooling.tracePoint.IsEnabled(TraceFlags.Information))
				{
					Pooling.tracePoint.Trace(TraceFlags.Information, string.Format("Returned (timing out) {0}", wrappedPooledQueueManager));
				}
			}
			return ReturnCode.Ok;
		}

		// Token: 0x06005905 RID: 22789 RVA: 0x0016F850 File Offset: 0x0016DA50
		internal static void FailConnection(ChannelQueueManager channelQueueManager)
		{
			if (channelQueueManager == null)
			{
				return;
			}
			ThreadPool.QueueUserWorkItem(new WaitCallback(Pooling.FailConnectionOnThread), channelQueueManager);
		}

		// Token: 0x06005906 RID: 22790 RVA: 0x0016F86C File Offset: 0x0016DA6C
		internal static void FailConnectionOnThread(object context)
		{
			ChannelQueueManager channelQueueManager = (ChannelQueueManager)context;
			if (channelQueueManager.CreatedPooled)
			{
				object obj = Pooling.lockObject;
				lock (obj)
				{
					if (Pooling.tracePoint.IsEnabled(TraceFlags.Error))
					{
						Pooling.tracePoint.Trace(TraceFlags.Error, string.Format("Failing pooled {0}", channelQueueManager));
					}
					channelQueueManager.ChannelQueueManagerCollection.ChannelQueueManagers.Remove(channelQueueManager);
					channelQueueManager.AlreadyRemoved = true;
					return;
				}
			}
			if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
			{
				Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("Failing non-pooled {0}", channelQueueManager));
			}
		}

		// Token: 0x06005907 RID: 22791 RVA: 0x0016F918 File Offset: 0x0016DB18
		public static void FailQueueManager(WrappedPooledQueueManager wrappedQueueManager)
		{
			if (wrappedQueueManager == null)
			{
				return;
			}
			if (wrappedQueueManager.CreatedPooled)
			{
				if (Pooling.tracePoint.IsEnabled(TraceFlags.Error))
				{
					Pooling.tracePoint.Trace(TraceFlags.Error, string.Format("Failing pooled {0}", wrappedQueueManager));
				}
				object obj = Pooling.lockObject;
				lock (obj)
				{
					Pooling.queueManagersTimingOut.Remove(wrappedQueueManager);
					lock (wrappedQueueManager)
					{
						wrappedQueueManager.TimingOut = false;
						wrappedQueueManager.BeingDeleted = true;
					}
					wrappedQueueManager.NameQueueManager.QueueManagers.Remove(wrappedQueueManager);
					wrappedQueueManager.AlreadyRemoved = true;
					return;
				}
			}
			if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
			{
				Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("Failing non-pooled {0}", wrappedQueueManager));
			}
		}

		// Token: 0x06005908 RID: 22792 RVA: 0x0016FA00 File Offset: 0x0016DC00
		private static ReturnCode GetOrCreateChannelQueueManager(string userName, TcpConnectionParameters tcpParameters, string channelNameToUse, out ChannelQueueManager channelQueueManager)
		{
			if (Pooling.tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				Pooling.tracePoint.Trace(TraceFlags.Verbose, string.Format("Get CQM Name: {0}, Channel: {1}, Server: '{2}', Port: {3}", new object[] { userName, channelNameToUse, tcpParameters.Server, tcpParameters.Port }));
			}
			channelQueueManager = null;
			Dictionary<string, Dictionary<string, HostConnection>> dictionary = Pooling.userNameToHostToHostConnections;
			Dictionary<string, HostConnection> dictionary2;
			lock (dictionary)
			{
				if (!Pooling.userNameToHostToHostConnections.TryGetValue(userName, out dictionary2))
				{
					dictionary2 = new Dictionary<string, HostConnection>();
					Pooling.userNameToHostToHostConnections.Add(userName, dictionary2);
					if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
					{
						Pooling.tracePoint.Trace(TraceFlags.Debug, "Generating new HostToHostConnections instance for UserName: '" + userName + "'");
					}
				}
			}
			Dictionary<string, HostConnection> dictionary3 = dictionary2;
			HostConnection hostConnection;
			lock (dictionary3)
			{
				if (!dictionary2.TryGetValue(tcpParameters.Server, out hostConnection))
				{
					hostConnection = new HostConnection(tcpParameters.Server);
					dictionary2.Add(tcpParameters.Server, hostConnection);
					if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
					{
						Pooling.tracePoint.Trace(TraceFlags.Debug, string.Concat(new string[] { "Generating new HostConnection instance for UserName: '", userName, "', Server: '", tcpParameters.Server, "'" }));
					}
				}
			}
			Dictionary<int, PortConnection> portToPortConnections = hostConnection.PortToPortConnections;
			PortConnection portConnection;
			lock (portToPortConnections)
			{
				if (!hostConnection.PortToPortConnections.TryGetValue(tcpParameters.Port, out portConnection))
				{
					portConnection = new PortConnection(tcpParameters.Port, hostConnection, tcpParameters.UseSsl);
					hostConnection.PortToPortConnections.Add(tcpParameters.Port, portConnection);
					if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
					{
						Pooling.tracePoint.Trace(TraceFlags.Debug, string.Concat(new string[]
						{
							"Generating new PortConnection instance for UserName: '",
							userName,
							"', Server: '",
							tcpParameters.Server,
							"', Port: ",
							tcpParameters.Port.ToString(CultureInfo.InvariantCulture)
						}));
					}
				}
			}
			if (portConnection.UseSsl != tcpParameters.UseSsl)
			{
				return ReturnCode.TcpPoolingDifferentSsl;
			}
			ChannelQueueManagerCollection channelQueueManagerCollection = null;
			Dictionary<string, ChannelQueueManagerCollection> channelToChannelQueueManagerCollections = portConnection.ChannelToChannelQueueManagerCollections;
			lock (channelToChannelQueueManagerCollections)
			{
				if (!portConnection.ChannelToChannelQueueManagerCollections.TryGetValue(channelNameToUse, out channelQueueManagerCollection))
				{
					channelQueueManagerCollection = new ChannelQueueManagerCollection(channelNameToUse, portConnection);
					portConnection.ChannelToChannelQueueManagerCollections.Add(channelNameToUse, channelQueueManagerCollection);
					if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
					{
						Pooling.tracePoint.Trace(TraceFlags.Debug, string.Concat(new string[]
						{
							"Generating new ChannelQueueManagerCollection instance for UserName: '",
							userName,
							"', Server: '",
							tcpParameters.Server,
							"', Port: ",
							tcpParameters.Port.ToString(CultureInfo.InvariantCulture),
							", Channel: '",
							channelNameToUse,
							"'"
						}));
					}
				}
			}
			object obj = Pooling.lockObject;
			lock (obj)
			{
				if (channelQueueManagerCollection.NumberOfConversationsPerSocket == -1)
				{
					foreach (ChannelQueueManager channelQueueManager2 in channelQueueManagerCollection.ChannelQueueManagers)
					{
						if (channelQueueManager2.Connection.ChannelParameters != null)
						{
							channelQueueManagerCollection.NumberOfConversationsPerSocket = channelQueueManager2.Connection.ChannelParameters.NumberOfConversationsPerSocket;
							break;
						}
					}
				}
				int num = 1;
				if (channelQueueManagerCollection.NumberOfConversationsPerSocket != -1)
				{
					num = channelQueueManagerCollection.NumberOfConversationsPerSocket;
				}
				int num2 = num * Pooling.queueManagersPerConversation;
				if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
				{
					Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("ConvPerSocket: {0}, QMCIs: {1}", num, num2));
				}
				int num3 = 0;
				foreach (ChannelQueueManager channelQueueManager3 in channelQueueManagerCollection.ChannelQueueManagers)
				{
					if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
					{
						Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("Trying (CQM): {0}", channelQueueManager3));
					}
					if (channelQueueManager3.ReferenceCount < num2)
					{
						int num4 = num2 - channelQueueManager3.ReferenceCount;
						if (num4 > num3)
						{
							channelQueueManager = channelQueueManager3;
							num3 = num4;
						}
					}
				}
				if (channelQueueManager == null)
				{
					channelQueueManager = new ChannelQueueManager(channelNameToUse, channelQueueManagerCollection);
					if (Pooling.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						Pooling.tracePoint.Trace(TraceFlags.Verbose, string.Format("New ChannelQueueManager {0}", channelQueueManager));
					}
					channelQueueManager.Connection = new PooledConnection(tcpParameters, channelQueueManager);
					ReturnCode returnCode = channelQueueManager.Connection.Connect();
					if (returnCode != ReturnCode.Ok)
					{
						if (Pooling.tracePoint.IsEnabled(TraceFlags.Error))
						{
							Pooling.tracePoint.Trace(TraceFlags.Error, string.Format("Tcp Connect failed, code: {0}", returnCode));
						}
						return returnCode;
					}
					channelQueueManagerCollection.ChannelQueueManagers.Add(channelQueueManager);
					channelQueueManager.CreatedPooled = true;
				}
				else if (Pooling.tracePoint.IsEnabled(TraceFlags.Debug))
				{
					Pooling.tracePoint.Trace(TraceFlags.Debug, string.Format("Reusing ChannelQueueManager {0}", channelQueueManager));
				}
				channelQueueManager.AddReference();
				if (Pooling.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					Pooling.tracePoint.Trace(TraceFlags.Verbose, string.Format("Channel QM {0}", channelQueueManager));
				}
			}
			return ReturnCode.Ok;
		}

		// Token: 0x040045E2 RID: 17890
		private static Dictionary<string, Dictionary<string, HostConnection>> userNameToHostToHostConnections = new Dictionary<string, Dictionary<string, HostConnection>>();

		// Token: 0x040045E3 RID: 17891
		private static HashSet<WrappedPooledQueueManager> queueManagersTimingOut = new HashSet<WrappedPooledQueueManager>();

		// Token: 0x040045E4 RID: 17892
		private static PoolingTracePoint tracePoint = new PoolingTracePoint(new MqTraceContainer());

		// Token: 0x040045E5 RID: 17893
		private static Timer cleanupTimer;

		// Token: 0x040045E6 RID: 17894
		private static TimeSpan timeoutTimespan;

		// Token: 0x040045E7 RID: 17895
		private static int timerInterval = 15000;

		// Token: 0x040045E8 RID: 17896
		private static object lockObject = new object();

		// Token: 0x040045E9 RID: 17897
		private static bool pool;

		// Token: 0x040045EA RID: 17898
		private static int timeout;

		// Token: 0x040045EB RID: 17899
		private static int queueManagersPerConversation;

		// Token: 0x040045EC RID: 17900
		private static bool allowDifferentChannels;

		// Token: 0x040045ED RID: 17901
		private static bool oneUserPerConversation;
	}
}
