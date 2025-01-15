using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000075 RID: 117
	internal class SqlDependencyPerAppDomainDispatcher : MarshalByRefObject
	{
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0001D039 File Offset: 0x0001B239
		internal int ObjectID { get; } = Interlocked.Increment(ref SqlDependencyPerAppDomainDispatcher.s_objectTypeCount);

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001D044 File Offset: 0x0001B244
		private SqlDependencyPerAppDomainDispatcher()
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlDependencyPerAppDomainDispatcher|DEP> {0}", this.ObjectID);
			try
			{
				this._dependencyIdToDependencyHash = new Dictionary<string, SqlDependency>();
				this._notificationIdToDependenciesHash = new Dictionary<string, SqlDependencyPerAppDomainDispatcher.DependencyList>();
				this._commandHashToNotificationId = new Dictionary<string, string>();
				this._timeoutTimer = new Timer(new TimerCallback(SqlDependencyPerAppDomainDispatcher.TimeoutTimerCallback), null, -1, -1);
				AppDomain.CurrentDomain.DomainUnload += this.UnloadEventHandler;
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001D0F8 File Offset: 0x0001B2F8
		private void UnloadEventHandler(object sender, EventArgs e)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("SqlDependencyPerAppDomainDispatcher.UnloadEventHandler | DEP | Object Id {0}", this.ObjectID);
			try
			{
				SqlDependencyProcessDispatcher processDispatcher = SqlDependency.ProcessDispatcher;
				if (processDispatcher != null)
				{
					processDispatcher.QueueAppDomainUnloading(SqlDependency.AppDomainKey);
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x000021D8 File Offset: 0x000003D8
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001D150 File Offset: 0x0001B350
		internal void AddDependencyEntry(SqlDependency dep)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, int>("<sc.SqlDependencyPerAppDomainDispatcher.AddDependencyEntry|DEP> {0}, SqlDependency: {1}", this.ObjectID, dep.ObjectID);
			try
			{
				object instanceLock = this._instanceLock;
				lock (instanceLock)
				{
					this._dependencyIdToDependencyHash.Add(dep.Id, dep);
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001D1D0 File Offset: 0x0001B3D0
		internal string AddCommandEntry(string commandHash, SqlDependency dep)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, string, int>("<sc.SqlDependencyPerAppDomainDispatcher.AddCommandEntry|DEP> {0}, commandHash: '{1}', SqlDependency: {2}", this.ObjectID, commandHash, dep.ObjectID);
			string text = string.Empty;
			string text2;
			try
			{
				object instanceLock = this._instanceLock;
				lock (instanceLock)
				{
					if (!this._dependencyIdToDependencyHash.ContainsKey(dep.Id))
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyPerAppDomainDispatcher.AddCommandEntry|DEP> Dependency not present in depId->dep hash, must have been invalidated.");
					}
					else if (this._commandHashToNotificationId.TryGetValue(commandHash, out text))
					{
						SqlDependencyPerAppDomainDispatcher.DependencyList dependencyList;
						if (!this._notificationIdToDependenciesHash.TryGetValue(text, out dependencyList))
						{
							throw ADP.InternalError(ADP.InternalErrorCode.SqlDependencyCommandHashIsNotAssociatedWithNotification);
						}
						if (!dependencyList.Contains(dep))
						{
							SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyPerAppDomainDispatcher.AddCommandEntry|DEP> Dependency not present for commandHash, adding.");
							dependencyList.Add(dep);
						}
						else
						{
							SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyPerAppDomainDispatcher.AddCommandEntry|DEP> Dependency already present for commandHash.");
						}
					}
					else
					{
						text = string.Format(CultureInfo.InvariantCulture, "{0};{1}", SqlDependency.AppDomainKey, Guid.NewGuid().ToString("D", CultureInfo.InvariantCulture));
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyPerAppDomainDispatcher.AddCommandEntry|DEP> Creating new Dependencies list for commandHash.");
						SqlDependencyPerAppDomainDispatcher.DependencyList dependencyList2 = new SqlDependencyPerAppDomainDispatcher.DependencyList(commandHash);
						dependencyList2.Add(dep);
						this._commandHashToNotificationId.Add(commandHash, text);
						this._notificationIdToDependenciesHash.Add(text, dependencyList2);
					}
				}
				text2 = text;
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
			return text2;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001D354 File Offset: 0x0001B554
		internal void InvalidateCommandID(SqlNotification sqlNotification)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, string>("<sc.SqlDependencyPerAppDomainDispatcher.InvalidateCommandID|DEP> {0}, commandHash: '{1}'", this.ObjectID, sqlNotification.Key);
			try
			{
				List<SqlDependency> list = null;
				object instanceLock = this._instanceLock;
				lock (instanceLock)
				{
					list = this.LookupCommandEntryWithRemove(sqlNotification.Key);
					if (list != null)
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyPerAppDomainDispatcher.InvalidateCommandID|DEP> commandHash found in hashtable.");
						using (List<SqlDependency>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								SqlDependency sqlDependency = enumerator.Current;
								this.LookupDependencyEntryWithRemove(sqlDependency.Id);
								this.RemoveDependencyFromCommandToDependenciesHash(sqlDependency);
							}
							goto IL_00AA;
						}
					}
					SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyPerAppDomainDispatcher.InvalidateCommandID|DEP> commandHash NOT found in hashtable.");
				}
				IL_00AA:
				if (list != null)
				{
					foreach (SqlDependency sqlDependency2 in list)
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyPerAppDomainDispatcher.InvalidateCommandID|DEP> Dependency found in commandHash dependency ArrayList - calling invalidate.");
						try
						{
							sqlDependency2.Invalidate(sqlNotification.Type, sqlNotification.Info, sqlNotification.Source);
						}
						catch (Exception ex)
						{
							if (!ADP.IsCatchableExceptionType(ex))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(ex);
						}
					}
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001D4C8 File Offset: 0x0001B6C8
		internal void InvalidateServer(string server, SqlNotification sqlNotification)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, string>("<sc.SqlDependencyPerAppDomainDispatcher.Invalidate|DEP> {0}, server: '{1}'", this.ObjectID, server);
			try
			{
				List<SqlDependency> list = new List<SqlDependency>();
				object instanceLock = this._instanceLock;
				lock (instanceLock)
				{
					foreach (KeyValuePair<string, SqlDependency> keyValuePair in this._dependencyIdToDependencyHash)
					{
						SqlDependency value = keyValuePair.Value;
						if (value.ContainsServer(server))
						{
							list.Add(value);
						}
					}
					foreach (SqlDependency sqlDependency in list)
					{
						this.LookupDependencyEntryWithRemove(sqlDependency.Id);
						this.RemoveDependencyFromCommandToDependenciesHash(sqlDependency);
					}
				}
				foreach (SqlDependency sqlDependency2 in list)
				{
					try
					{
						sqlDependency2.Invalidate(sqlNotification.Type, sqlNotification.Info, sqlNotification.Source);
					}
					catch (Exception ex)
					{
						if (!ADP.IsCatchableExceptionType(ex))
						{
							throw;
						}
						ADP.TraceExceptionWithoutRethrow(ex);
					}
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001D698 File Offset: 0x0001B898
		internal SqlDependency LookupDependencyEntry(string id)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, string>("<sc.SqlDependencyPerAppDomainDispatcher.LookupDependencyEntry|DEP> {0}, Key: '{1}'", this.ObjectID, id);
			SqlDependency sqlDependency2;
			try
			{
				if (id == null)
				{
					throw ADP.ArgumentNull("id");
				}
				if (string.IsNullOrEmpty(id))
				{
					throw SQL.SqlDependencyIdMismatch();
				}
				SqlDependency sqlDependency = null;
				object instanceLock = this._instanceLock;
				lock (instanceLock)
				{
					if (this._dependencyIdToDependencyHash.ContainsKey(id))
					{
						sqlDependency = this._dependencyIdToDependencyHash[id];
					}
					else
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyPerAppDomainDispatcher.LookupDependencyEntry|DEP|ERR> ERROR - dependency ID mismatch - not throwing.");
					}
				}
				sqlDependency2 = sqlDependency;
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
			return sqlDependency2;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0001D754 File Offset: 0x0001B954
		private void LookupDependencyEntryWithRemove(string id)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, string>("<sc.SqlDependencyPerAppDomainDispatcher.LookupDependencyEntryWithRemove|DEP> {0}, id: '{1}'", this.ObjectID, id);
			try
			{
				object instanceLock = this._instanceLock;
				lock (instanceLock)
				{
					if (this._dependencyIdToDependencyHash.ContainsKey(id))
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyPerAppDomainDispatcher.LookupDependencyEntryWithRemove|DEP> Entry found in hashtable - removing.");
						this._dependencyIdToDependencyHash.Remove(id);
						if (this._dependencyIdToDependencyHash.Count == 0)
						{
							this._timeoutTimer.Change(-1, -1);
							this._sqlDependencyTimeOutTimerStarted = false;
						}
					}
					else
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyPerAppDomainDispatcher.LookupDependencyEntryWithRemove|DEP> Entry NOT found in hashtable.");
					}
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0001D81C File Offset: 0x0001BA1C
		private List<SqlDependency> LookupCommandEntryWithRemove(string notificationId)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, string>("<sc.SqlDependencyPerAppDomainDispatcher.LookupCommandEntryWithRemove|DEP> {0}, commandHash: '{1}'", this.ObjectID, notificationId);
			List<SqlDependency> list;
			try
			{
				SqlDependencyPerAppDomainDispatcher.DependencyList dependencyList = null;
				object instanceLock = this._instanceLock;
				lock (instanceLock)
				{
					if (this._notificationIdToDependenciesHash.TryGetValue(notificationId, out dependencyList))
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyPerAppDomainDispatcher.LookupDependencyEntriesWithRemove|DEP> Entries found in hashtable - removing.");
						this._notificationIdToDependenciesHash.Remove(notificationId);
						this._commandHashToNotificationId.Remove(dependencyList.CommandHash);
					}
					else
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyPerAppDomainDispatcher.LookupDependencyEntriesWithRemove|DEP> Entries NOT found in hashtable.");
					}
				}
				list = dependencyList;
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
			return list;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001D8E0 File Offset: 0x0001BAE0
		private void RemoveDependencyFromCommandToDependenciesHash(SqlDependency dependency)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, int>("<sc.SqlDependencyPerAppDomainDispatcher.RemoveDependencyFromCommandToDependenciesHash|DEP> {0}, SqlDependency: {1}", this.ObjectID, dependency.ObjectID);
			try
			{
				object instanceLock = this._instanceLock;
				lock (instanceLock)
				{
					List<string> list = new List<string>();
					List<string> list2 = new List<string>();
					foreach (KeyValuePair<string, SqlDependencyPerAppDomainDispatcher.DependencyList> keyValuePair in this._notificationIdToDependenciesHash)
					{
						SqlDependencyPerAppDomainDispatcher.DependencyList value = keyValuePair.Value;
						if (value.Remove(dependency))
						{
							SqlClientEventSource.Log.TryNotificationTraceEvent<int, string>("<sc.SqlDependencyPerAppDomainDispatcher.RemoveDependencyFromCommandToDependenciesHash|DEP> Removed SqlDependency: {0}, with ID: '{1}'.", dependency.ObjectID, dependency.Id);
							if (value.Count == 0)
							{
								list.Add(keyValuePair.Key);
								list2.Add(keyValuePair.Value.CommandHash);
							}
						}
					}
					for (int i = 0; i < list.Count; i++)
					{
						this._notificationIdToDependenciesHash.Remove(list[i]);
						this._commandHashToNotificationId.Remove(list2[i]);
					}
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0001DA30 File Offset: 0x0001BC30
		internal void StartTimer(SqlDependency dep)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, int>("<sc.SqlDependencyPerAppDomainDispatcher.StartTimer|DEP> {0}, SqlDependency: {1}", this.ObjectID, dep.ObjectID);
			try
			{
				object instanceLock = this._instanceLock;
				lock (instanceLock)
				{
					if (!this._sqlDependencyTimeOutTimerStarted)
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyPerAppDomainDispatcher.StartTimer|DEP> Timer not yet started, starting.");
						this._timeoutTimer.Change(15000, 15000);
						this._nextTimeout = dep.ExpirationTime;
						this._sqlDependencyTimeOutTimerStarted = true;
					}
					else if (this._nextTimeout > dep.ExpirationTime)
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyPerAppDomainDispatcher.StartTimer|DEP> Timer already started, resetting time.");
						this._nextTimeout = dep.ExpirationTime;
					}
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0001DB10 File Offset: 0x0001BD10
		private static void TimeoutTimerCallback(object state)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<string>("<sc.SqlDependencyPerAppDomainDispatcher.TimeoutTimerCallback|DEP> AppDomainKey: '{0}'", SqlDependency.AppDomainKey);
			try
			{
				object instanceLock = SqlDependencyPerAppDomainDispatcher.SingletonInstance._instanceLock;
				SqlDependency[] array;
				lock (instanceLock)
				{
					if (SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Count == 0)
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyPerAppDomainDispatcher.TimeoutTimerCallback|DEP> No dependencies, exiting.");
						return;
					}
					if (SqlDependencyPerAppDomainDispatcher.SingletonInstance._nextTimeout > DateTime.UtcNow)
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyPerAppDomainDispatcher.TimeoutTimerCallback|DEP> No timeouts expired, exiting.");
						return;
					}
					array = new SqlDependency[SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Count];
					SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Values.CopyTo(array, 0);
				}
				DateTime utcNow = DateTime.UtcNow;
				DateTime dateTime = DateTime.MaxValue;
				int i = 0;
				while (i < array.Length)
				{
					if (array[i].ExpirationTime <= utcNow)
					{
						try
						{
							array[i].Invalidate(SqlNotificationType.Change, SqlNotificationInfo.Error, SqlNotificationSource.Timeout);
							goto IL_011B;
						}
						catch (Exception ex)
						{
							if (!ADP.IsCatchableExceptionType(ex))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(ex);
							goto IL_011B;
						}
						goto IL_00FB;
					}
					goto IL_00FB;
					IL_011B:
					i++;
					continue;
					IL_00FB:
					if (array[i].ExpirationTime < dateTime)
					{
						dateTime = array[i].ExpirationTime;
					}
					array[i] = null;
					goto IL_011B;
				}
				object instanceLock2 = SqlDependencyPerAppDomainDispatcher.SingletonInstance._instanceLock;
				lock (instanceLock2)
				{
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j] != null)
						{
							SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Remove(array[j].Id);
						}
					}
					if (dateTime < SqlDependencyPerAppDomainDispatcher.SingletonInstance._nextTimeout)
					{
						SqlDependencyPerAppDomainDispatcher.SingletonInstance._nextTimeout = dateTime;
					}
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x0400022F RID: 559
		internal static readonly SqlDependencyPerAppDomainDispatcher SingletonInstance = new SqlDependencyPerAppDomainDispatcher();

		// Token: 0x04000230 RID: 560
		internal object _instanceLock = new object();

		// Token: 0x04000231 RID: 561
		private readonly Dictionary<string, SqlDependency> _dependencyIdToDependencyHash;

		// Token: 0x04000232 RID: 562
		private readonly Dictionary<string, SqlDependencyPerAppDomainDispatcher.DependencyList> _notificationIdToDependenciesHash;

		// Token: 0x04000233 RID: 563
		private readonly Dictionary<string, string> _commandHashToNotificationId;

		// Token: 0x04000234 RID: 564
		private bool _sqlDependencyTimeOutTimerStarted;

		// Token: 0x04000235 RID: 565
		private DateTime _nextTimeout;

		// Token: 0x04000236 RID: 566
		private readonly Timer _timeoutTimer;

		// Token: 0x04000237 RID: 567
		private static int s_objectTypeCount;

		// Token: 0x020001CF RID: 463
		private sealed class DependencyList : List<SqlDependency>
		{
			// Token: 0x06001DC0 RID: 7616 RVA: 0x0007A916 File Offset: 0x00078B16
			internal DependencyList(string commandHash)
			{
				this.CommandHash = commandHash;
			}

			// Token: 0x040013FC RID: 5116
			public readonly string CommandHash;
		}
	}
}
