using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Data.Common;
using Microsoft.Data.ProviderBase;
using Microsoft.Data.Sql;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000074 RID: 116
	public sealed class SqlDependency
	{
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0001BBE1 File Offset: 0x00019DE1
		internal int ObjectID { get; } = Interlocked.Increment(ref SqlDependency.s_objectTypeCount);

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001BBE9 File Offset: 0x00019DE9
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public SqlDependency()
			: this(null, null, 0)
		{
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001BBF4 File Offset: 0x00019DF4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public SqlDependency(SqlCommand command)
			: this(command, null, 0)
		{
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001BC00 File Offset: 0x00019E00
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public SqlDependency(SqlCommand command, string options, int timeout)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, string, int>("<sc.SqlDependency|DEP> {0}, options: '{1}', timeout: '{2}'", this.ObjectID, options, timeout);
			try
			{
				if (InOutOfProcHelper.InProc)
				{
					throw SQL.SqlDepCannotBeCreatedInProc();
				}
				if (timeout < 0)
				{
					throw SQL.InvalidSqlDependencyTimeout("timeout");
				}
				this._timeout = timeout;
				if (options != null)
				{
					this._options = options;
				}
				this.AddCommandInternal(command);
				SqlDependencyPerAppDomainDispatcher.SingletonInstance.AddDependencyEntry(this);
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0001BCEC File Offset: 0x00019EEC
		[ResCategory("Data")]
		[ResDescription("Property to indicate if this dependency is invalid.")]
		public bool HasChanges
		{
			get
			{
				return this._dependencyFired;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0001BCF4 File Offset: 0x00019EF4
		[ResCategory("Data")]
		[ResDescription("A string that uniquely identifies this dependency object.")]
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x0001BCFC File Offset: 0x00019EFC
		internal static string AppDomainKey
		{
			get
			{
				return SqlDependency.s_appDomainKey;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0001BD03 File Offset: 0x00019F03
		internal DateTime ExpirationTime
		{
			get
			{
				return this._expirationTime;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x0001BD0B File Offset: 0x00019F0B
		internal string Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0001BD13 File Offset: 0x00019F13
		internal static SqlDependencyProcessDispatcher ProcessDispatcher
		{
			get
			{
				return SqlDependency.s_processDispatcher;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x0001BD1A File Offset: 0x00019F1A
		internal int Timeout
		{
			get
			{
				return this._timeout;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000A71 RID: 2673 RVA: 0x0001BD24 File Offset: 0x00019F24
		// (remove) Token: 0x06000A72 RID: 2674 RVA: 0x0001BE00 File Offset: 0x0001A000
		[ResCategory("Data")]
		[ResDescription("Event that can be used to subscribe for change notifications.")]
		public event OnChangeEventHandler OnChange
		{
			add
			{
				long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlDependency.OnChange-Add|DEP> {0}", this.ObjectID);
				try
				{
					if (value != null)
					{
						SqlNotificationEventArgs sqlNotificationEventArgs = null;
						object eventHandlerLock = this._eventHandlerLock;
						lock (eventHandlerLock)
						{
							if (this._dependencyFired)
							{
								SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.OnChange-Add|DEP> Dependency already fired, firing new event.");
								sqlNotificationEventArgs = new SqlNotificationEventArgs(SqlNotificationType.Subscribe, SqlNotificationInfo.AlreadyChanged, SqlNotificationSource.Client);
							}
							else
							{
								SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.OnChange-Add|DEP> Dependency has not fired, adding new event.");
								SqlDependency.EventContextPair eventContextPair = new SqlDependency.EventContextPair(value, this);
								if (this._eventList.Contains(eventContextPair))
								{
									throw SQL.SqlDependencyEventNoDuplicate();
								}
								this._eventList.Add(eventContextPair);
							}
						}
						if (sqlNotificationEventArgs != null)
						{
							value(this, sqlNotificationEventArgs);
						}
					}
				}
				finally
				{
					SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
				}
			}
			remove
			{
				long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlDependency.OnChange-Remove|DEP> {0}", this.ObjectID);
				try
				{
					if (value != null)
					{
						SqlDependency.EventContextPair eventContextPair = new SqlDependency.EventContextPair(value, this);
						object eventHandlerLock = this._eventHandlerLock;
						lock (eventHandlerLock)
						{
							int num2 = this._eventList.IndexOf(eventContextPair);
							if (0 <= num2)
							{
								this._eventList.RemoveAt(num2);
							}
						}
					}
				}
				finally
				{
					SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
				}
			}
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0001BE98 File Offset: 0x0001A098
		[ResCategory("Data")]
		[ResDescription("To add a command to existing dependency object.")]
		public void AddCommandDependency(SqlCommand command)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlDependency.AddCommandDependency|DEP> {0}", this.ObjectID);
			try
			{
				if (command == null)
				{
					throw ADP.ArgumentNull("command");
				}
				this.AddCommandInternal(command);
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private static ObjectHandle CreateProcessDispatcher(_AppDomain masterDomain)
		{
			return masterDomain.CreateInstance(SqlDependency.s_assemblyName, SqlDependency.s_typeName);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0001BF04 File Offset: 0x0001A104
		private static void ObtainProcessDispatcher()
		{
			byte[] data = SNINativeMethodWrapper.GetData();
			if (data == null)
			{
				SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.ObtainProcessDispatcher|DEP> nativeStorage null, obtaining dispatcher AppDomain and creating ProcessDispatcher.");
				_AppDomain defaultAppDomain = SNINativeMethodWrapper.GetDefaultAppDomain();
				if (defaultAppDomain == null)
				{
					SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.ObtainProcessDispatcher|DEP|ERR> ERROR - unable to obtain default AppDomain!");
					throw ADP.InternalError(ADP.InternalErrorCode.SqlDependencyProcessDispatcherFailureAppDomain);
				}
				ObjectHandle objectHandle = SqlDependency.CreateProcessDispatcher(defaultAppDomain);
				if (objectHandle != null)
				{
					SqlDependencyProcessDispatcher sqlDependencyProcessDispatcher = (SqlDependencyProcessDispatcher)objectHandle.Unwrap();
					if (sqlDependencyProcessDispatcher != null)
					{
						SqlDependency.s_processDispatcher = SqlDependencyProcessDispatcher.SingletonProcessDispatcher;
						using (MemoryStream memoryStream = new MemoryStream())
						{
							SqlDependency.SqlClientObjRef sqlClientObjRef = new SqlDependency.SqlClientObjRef(SqlDependency.s_processDispatcher);
							DataContractSerializer dataContractSerializer = new DataContractSerializer(sqlClientObjRef.GetType());
							SqlDependency.GetSerializedObject(sqlClientObjRef, dataContractSerializer, memoryStream);
							SNINativeMethodWrapper.SetData(memoryStream.ToArray());
							return;
						}
					}
					SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.ObtainProcessDispatcher|DEP|ERR> ERROR - ObjectHandle.Unwrap returned null!");
					throw ADP.InternalError(ADP.InternalErrorCode.SqlDependencyObtainProcessDispatcherFailureObjectHandle);
				}
				SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.ObtainProcessDispatcher|DEP|ERR> ERROR - AppDomain.CreateInstance returned null!");
				throw ADP.InternalError(ADP.InternalErrorCode.SqlDependencyProcessDispatcherFailureCreateInstance);
			}
			else
			{
				SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.ObtainProcessDispatcher|DEP> nativeStorage not null, obtaining existing dispatcher AppDomain and ProcessDispatcher.");
				using (MemoryStream memoryStream2 = new MemoryStream(data))
				{
					DataContractSerializer dataContractSerializer2 = new DataContractSerializer(typeof(SqlDependency.SqlClientObjRef));
					if (!SqlDependency.SqlClientObjRef.CanCastToSqlDependencyProcessDispatcher())
					{
						throw new ArgumentException(Strings.SqlDependency_UnexpectedValueOnDeserialize);
					}
					SqlDependency.s_processDispatcher = SqlDependency.GetDeserializedObject(dataContractSerializer2, memoryStream2);
					SqlClientEventSource.Log.TryNotificationTraceEvent<int>("<sc.SqlDependency.ObtainProcessDispatcher|DEP> processDispatcher obtained, ID: {0}", SqlDependency.s_processDispatcher.ObjectID);
				}
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0001C078 File Offset: 0x0001A278
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		private static void GetSerializedObject(SqlDependency.SqlClientObjRef objRef, DataContractSerializer serializer, MemoryStream stream)
		{
			serializer.WriteObject(stream, objRef);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0001C084 File Offset: 0x0001A284
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		private static SqlDependencyProcessDispatcher GetDeserializedObject(DataContractSerializer serializer, MemoryStream stream)
		{
			object obj = serializer.ReadObject(stream);
			object obj2 = RemotingServices.Unmarshal((obj as SqlDependency.SqlClientObjRef).GetObjRef());
			return obj2 as SqlDependencyProcessDispatcher;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0001C0B0 File Offset: 0x0001A2B0
		public static bool Start(string connectionString)
		{
			return SqlDependency.Start(connectionString, null, true);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001C0BA File Offset: 0x0001A2BA
		public static bool Start(string connectionString, string queue)
		{
			return SqlDependency.Start(connectionString, queue, false);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001C0C4 File Offset: 0x0001A2C4
		internal static bool Start(string connectionString, string queue, bool useDefaults)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<string, string>("<sc.SqlDependency.Start|DEP> AppDomainKey: '{0}', queue: '{1}'", SqlDependency.AppDomainKey, queue);
			bool flag5;
			try
			{
				if (InOutOfProcHelper.InProc)
				{
					throw SQL.SqlDepCannotBeCreatedInProc();
				}
				if (string.IsNullOrEmpty(connectionString))
				{
					if (connectionString == null)
					{
						throw ADP.ArgumentNull("connectionString");
					}
					throw ADP.Argument("connectionString");
				}
				else
				{
					if (!useDefaults && string.IsNullOrEmpty(queue))
					{
						useDefaults = true;
						queue = null;
					}
					SqlConnectionString sqlConnectionString = new SqlConnectionString(connectionString);
					sqlConnectionString.DemandPermission();
					if (sqlConnectionString.LocalDBInstance != null)
					{
						LocalDBAPI.DemandLocalDBPermissions();
					}
					bool flag = false;
					bool flag2 = false;
					object obj = SqlDependency.s_startStopLock;
					lock (obj)
					{
						try
						{
							if (SqlDependency.s_processDispatcher == null)
							{
								SqlDependency.ObtainProcessDispatcher();
							}
							if (useDefaults)
							{
								string text = null;
								DbConnectionPoolIdentity dbConnectionPoolIdentity = null;
								string text2 = null;
								string text3 = null;
								string text4 = null;
								bool flag4 = false;
								RuntimeHelpers.PrepareConstrainedRegions();
								try
								{
									flag2 = SqlDependency.s_processDispatcher.StartWithDefault(connectionString, out text, out dbConnectionPoolIdentity, out text2, out text3, ref text4, SqlDependency.s_appDomainKey, SqlDependencyPerAppDomainDispatcher.SingletonInstance, out flag, out flag4);
									SqlClientEventSource.Log.TryNotificationTraceEvent<bool, string, string, string>("<sc.SqlDependency.Start|DEP> Start (defaults) returned: '{0}', with service: '{1}', server: '{2}', database: '{3}'", flag2, text4, text, text3);
									goto IL_0177;
								}
								finally
								{
									if (flag4 && !flag)
									{
										SqlDependency.IdentityUserNamePair identityUserNamePair = new SqlDependency.IdentityUserNamePair(dbConnectionPoolIdentity, text2);
										SqlDependency.DatabaseServicePair databaseServicePair = new SqlDependency.DatabaseServicePair(text3, text4);
										if (!SqlDependency.AddToServerUserHash(text, identityUserNamePair, databaseServicePair))
										{
											try
											{
												SqlDependency.Stop(connectionString, queue, useDefaults, true);
											}
											catch (Exception ex)
											{
												if (!ADP.IsCatchableExceptionType(ex))
												{
													throw;
												}
												ADP.TraceExceptionWithoutRethrow(ex);
												SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.Start|DEP|ERR> Exception occurred from Stop() after duplicate was found on Start().");
											}
											throw SQL.SqlDependencyDuplicateStart();
										}
									}
								}
							}
							flag2 = SqlDependency.s_processDispatcher.Start(connectionString, queue, SqlDependency.s_appDomainKey, SqlDependencyPerAppDomainDispatcher.SingletonInstance);
							SqlClientEventSource.Log.TryNotificationTraceEvent<bool>("<sc.SqlDependency.Start|DEP> Start (user provided queue) returned: '{0}'", flag2);
							IL_0177:;
						}
						catch (Exception ex2)
						{
							if (!ADP.IsCatchableExceptionType(ex2))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(ex2);
							SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.Start|DEP|ERR> Exception occurred from _processDispatcher.Start(...), calling Invalidate(...).");
							throw;
						}
					}
					flag5 = flag2;
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
			return flag5;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0001C30C File Offset: 0x0001A50C
		public static bool Stop(string connectionString)
		{
			return SqlDependency.Stop(connectionString, null, true, false);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0001C317 File Offset: 0x0001A517
		public static bool Stop(string connectionString, string queue)
		{
			return SqlDependency.Stop(connectionString, queue, false, false);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0001C324 File Offset: 0x0001A524
		internal static bool Stop(string connectionString, string queue, bool useDefaults, bool startFailed)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<string, string>("<sc.SqlDependency.Stop|DEP> AppDomainKey: '{0}', queue: '{1}'", SqlDependency.AppDomainKey, queue);
			bool flag5;
			try
			{
				if (InOutOfProcHelper.InProc)
				{
					throw SQL.SqlDepCannotBeCreatedInProc();
				}
				if (string.IsNullOrEmpty(connectionString))
				{
					if (connectionString == null)
					{
						throw ADP.ArgumentNull("connectionString");
					}
					throw ADP.Argument("connectionString");
				}
				else
				{
					if (!useDefaults && string.IsNullOrEmpty(queue))
					{
						useDefaults = true;
						queue = null;
					}
					SqlConnectionString sqlConnectionString = new SqlConnectionString(connectionString);
					sqlConnectionString.DemandPermission();
					if (sqlConnectionString.LocalDBInstance != null)
					{
						LocalDBAPI.DemandLocalDBPermissions();
					}
					bool flag = false;
					object obj = SqlDependency.s_startStopLock;
					lock (obj)
					{
						if (SqlDependency.s_processDispatcher != null)
						{
							try
							{
								string text = null;
								DbConnectionPoolIdentity dbConnectionPoolIdentity = null;
								string text2 = null;
								string text3 = null;
								string text4 = null;
								if (useDefaults)
								{
									bool flag3 = false;
									RuntimeHelpers.PrepareConstrainedRegions();
									try
									{
										flag = SqlDependency.s_processDispatcher.Stop(connectionString, out text, out dbConnectionPoolIdentity, out text2, out text3, ref text4, SqlDependency.s_appDomainKey, out flag3);
										goto IL_010B;
									}
									finally
									{
										if (flag3 && !startFailed)
										{
											SqlDependency.IdentityUserNamePair identityUserNamePair = new SqlDependency.IdentityUserNamePair(dbConnectionPoolIdentity, text2);
											SqlDependency.DatabaseServicePair databaseServicePair = new SqlDependency.DatabaseServicePair(text3, text4);
											SqlDependency.RemoveFromServerUserHash(text, identityUserNamePair, databaseServicePair);
										}
									}
								}
								bool flag4;
								flag = SqlDependency.s_processDispatcher.Stop(connectionString, out text, out dbConnectionPoolIdentity, out text2, out text3, ref queue, SqlDependency.s_appDomainKey, out flag4);
								IL_010B:;
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
					flag5 = flag;
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
			return flag5;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001C4D8 File Offset: 0x0001A6D8
		private static bool AddToServerUserHash(string server, SqlDependency.IdentityUserNamePair identityUser, SqlDependency.DatabaseServicePair databaseService)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<string, string, string>("<sc.SqlDependency.AddToServerUserHash|DEP> server: '{0}', database: '{1}', service: '{2}'", server, databaseService.Database, databaseService.Service);
			bool flag3;
			try
			{
				bool flag = false;
				Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> dictionary = SqlDependency.s_serverUserHash;
				lock (dictionary)
				{
					Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> dictionary2;
					if (!SqlDependency.s_serverUserHash.ContainsKey(server))
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.AddToServerUserHash|DEP> Hash did not contain server, adding.");
						dictionary2 = new Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>();
						SqlDependency.s_serverUserHash.Add(server, dictionary2);
					}
					else
					{
						dictionary2 = SqlDependency.s_serverUserHash[server];
					}
					List<SqlDependency.DatabaseServicePair> list;
					if (!dictionary2.ContainsKey(identityUser))
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.AddToServerUserHash|DEP> Hash contained server but not user, adding user.");
						list = new List<SqlDependency.DatabaseServicePair>();
						dictionary2.Add(identityUser, list);
					}
					else
					{
						list = dictionary2[identityUser];
					}
					if (!list.Contains(databaseService))
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.AddToServerUserHash|DEP> Adding database.");
						list.Add(databaseService);
						flag = true;
					}
					else
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.AddToServerUserHash|DEP|ERR> ERROR - hash already contained server, user, and database - we will throw!.");
					}
				}
				flag3 = flag;
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
			return flag3;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001C5F8 File Offset: 0x0001A7F8
		private static void RemoveFromServerUserHash(string server, SqlDependency.IdentityUserNamePair identityUser, SqlDependency.DatabaseServicePair databaseService)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<string, string, string>("<sc.SqlDependency.RemoveFromServerUserHash|DEP> server: '{0}', database: '{1}', service: '{2}'", server, databaseService.Database, databaseService.Service);
			try
			{
				Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> dictionary = SqlDependency.s_serverUserHash;
				lock (dictionary)
				{
					if (SqlDependency.s_serverUserHash.ContainsKey(server))
					{
						Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> dictionary2 = SqlDependency.s_serverUserHash[server];
						if (dictionary2.ContainsKey(identityUser))
						{
							List<SqlDependency.DatabaseServicePair> list = dictionary2[identityUser];
							int num2 = list.IndexOf(databaseService);
							if (num2 >= 0)
							{
								SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.RemoveFromServerUserHash|DEP> Hash contained server, user, and database - removing database.");
								list.RemoveAt(num2);
								if (list.Count == 0)
								{
									SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.RemoveFromServerUserHash|DEP> databaseServiceList count 0, removing the list for this server and user.");
									dictionary2.Remove(identityUser);
									if (dictionary2.Count == 0)
									{
										SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.RemoveFromServerUserHash|DEP> identityDatabaseHash count 0, removing the hash for this server.");
										SqlDependency.s_serverUserHash.Remove(server);
									}
								}
							}
							else
							{
								SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.RemoveFromServerUserHash|DEP|ERR> ERROR - hash contained server and user but not database!");
							}
						}
						else
						{
							SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.RemoveFromServerUserHash|DEP|ERR> ERROR - hash contained server but not user!");
						}
					}
					else
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.RemoveFromServerUserHash|DEP|ERR> ERROR - hash did not contain server!");
					}
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001C734 File Offset: 0x0001A934
		internal static string GetDefaultComposedOptions(string server, string failoverServer, SqlDependency.IdentityUserNamePair identityUser, string database)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<string, string, string>("<sc.SqlDependency.GetDefaultComposedOptions|DEP> server: '{0}', failoverServer: '{1}', database: '{2}'", server, failoverServer, database);
			string text5;
			try
			{
				Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> dictionary = SqlDependency.s_serverUserHash;
				string text2;
				lock (dictionary)
				{
					if (!SqlDependency.s_serverUserHash.ContainsKey(server))
					{
						if (SqlDependency.s_serverUserHash.Count == 0)
						{
							SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.GetDefaultComposedOptions|DEP|ERR> ERROR - no start calls have been made, about to throw.");
							throw SQL.SqlDepDefaultOptionsButNoStart();
						}
						if (string.IsNullOrEmpty(failoverServer) || !SqlDependency.s_serverUserHash.ContainsKey(failoverServer))
						{
							SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.GetDefaultComposedOptions|DEP|ERR> ERROR - not listening to this server, about to throw.");
							throw SQL.SqlDependencyNoMatchingServerStart();
						}
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.GetDefaultComposedOptions|DEP> using failover server instead\n");
						server = failoverServer;
					}
					Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> dictionary2 = SqlDependency.s_serverUserHash[server];
					List<SqlDependency.DatabaseServicePair> list = null;
					if (!dictionary2.ContainsKey(identityUser))
					{
						if (dictionary2.Count > 1)
						{
							SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.GetDefaultComposedOptions|DEP|ERR> ERROR - not listening for this user, but listening to more than one other user, about to throw.");
							throw SQL.SqlDependencyNoMatchingServerStart();
						}
						using (Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>.Enumerator enumerator = dictionary2.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								goto IL_010A;
							}
							KeyValuePair<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> keyValuePair = enumerator.Current;
							list = keyValuePair.Value;
							goto IL_010A;
						}
					}
					list = dictionary2[identityUser];
					IL_010A:
					SqlDependency.DatabaseServicePair databaseServicePair = new SqlDependency.DatabaseServicePair(database, null);
					SqlDependency.DatabaseServicePair databaseServicePair2 = null;
					int num2 = list.IndexOf(databaseServicePair);
					if (num2 != -1)
					{
						databaseServicePair2 = list[num2];
					}
					if (databaseServicePair2 != null)
					{
						database = SqlDependency.FixupServiceOrDatabaseName(databaseServicePair2.Database);
						string text = SqlDependency.FixupServiceOrDatabaseName(databaseServicePair2.Service);
						text2 = "Service=" + text + ";Local Database=" + database;
					}
					else
					{
						if (list.Count != 1)
						{
							SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.GetDefaultComposedOptions|DEP|ERR> ERROR - SqlDependency.Start called multiple times for this server/user, but no matching database.");
							throw SQL.SqlDependencyNoMatchingServerDatabaseStart();
						}
						object[] array = list.ToArray();
						object[] array2 = array;
						databaseServicePair2 = (SqlDependency.DatabaseServicePair)array2[0];
						string text3 = SqlDependency.FixupServiceOrDatabaseName(databaseServicePair2.Database);
						string text4 = SqlDependency.FixupServiceOrDatabaseName(databaseServicePair2.Service);
						text2 = "Service=" + text4 + ";Local Database=" + text3;
					}
				}
				SqlClientEventSource.Log.TryNotificationTraceEvent<string>("<sc.SqlDependency.GetDefaultComposedOptions|DEP> resulting options: '{0}'.", text2);
				text5 = text2;
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
			return text5;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001C98C File Offset: 0x0001AB8C
		internal void AddToServerList(string server)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, string>("<sc.SqlDependency.AddToServerList|DEP> {0}, server: '{1}'", this.ObjectID, server);
			try
			{
				List<string> serverList = this._serverList;
				lock (serverList)
				{
					int num2 = this._serverList.BinarySearch(server, StringComparer.OrdinalIgnoreCase);
					if (0 > num2)
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent<string>("<sc.SqlDependency.AddToServerList|DEP> Server not present in hashtable, adding server: '{0}'.", server);
						num2 = ~num2;
						this._serverList.Insert(num2, server);
					}
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001CA2C File Offset: 0x0001AC2C
		internal bool ContainsServer(string server)
		{
			List<string> serverList = this._serverList;
			bool flag2;
			lock (serverList)
			{
				flag2 = this._serverList.Contains(server);
			}
			return flag2;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001CA74 File Offset: 0x0001AC74
		internal string ComputeHashAndAddToDispatcher(SqlCommand command)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, int>("<sc.SqlDependency.ComputeHashAndAddToDispatcher|DEP> {0}, SqlCommand: {1}", this.ObjectID, command.ObjectID);
			string text3;
			try
			{
				string text = this.ComputeCommandHash(command.Connection.ConnectionString, command);
				string text2 = SqlDependencyPerAppDomainDispatcher.SingletonInstance.AddCommandEntry(text, this);
				SqlClientEventSource.Log.TryNotificationTraceEvent<string>("<sc.SqlDependency.ComputeHashAndAddToDispatcher|DEP> computed id string: '{0}'.", text2);
				text3 = text2;
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
			return text3;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0001CAF0 File Offset: 0x0001ACF0
		internal void Invalidate(SqlNotificationType type, SqlNotificationInfo info, SqlNotificationSource source)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlDependency.Invalidate|DEP> {0}", this.ObjectID);
			try
			{
				List<SqlDependency.EventContextPair> list = null;
				object eventHandlerLock = this._eventHandlerLock;
				lock (eventHandlerLock)
				{
					if (this._dependencyFired && SqlNotificationInfo.AlreadyChanged != info && SqlNotificationSource.Client != source)
					{
						if (this.ExpirationTime >= DateTime.UtcNow)
						{
							SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.Invalidate|DEP|ERR> ERROR - notification received twice - we should never enter this state!");
						}
						else
						{
							SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.Invalidate|DEP> ignore notification received after timeout!");
						}
					}
					else
					{
						this._dependencyFired = true;
						list = this._eventList;
						this._eventList = new List<SqlDependency.EventContextPair>();
					}
				}
				if (list != null)
				{
					SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.Invalidate|DEP> Firing events.");
					foreach (SqlDependency.EventContextPair eventContextPair in list)
					{
						eventContextPair.Invoke(new SqlNotificationEventArgs(type, info, source));
					}
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001CC14 File Offset: 0x0001AE14
		internal void StartTimer(SqlNotificationRequest notificationRequest)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlDependency.StartTimer|DEP> {0}", this.ObjectID);
			try
			{
				if (this._expirationTime == DateTime.MaxValue)
				{
					SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.StartTimer|DEP> We've timed out, executing logic.");
					int num2 = 432000;
					if (this._timeout != 0)
					{
						num2 = this._timeout;
					}
					if (notificationRequest != null && notificationRequest.Timeout < num2 && notificationRequest.Timeout != 0)
					{
						num2 = notificationRequest.Timeout;
					}
					this._expirationTime = DateTime.UtcNow.AddSeconds((double)num2);
					SqlDependencyPerAppDomainDispatcher.SingletonInstance.StartTimer(this);
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001CCC8 File Offset: 0x0001AEC8
		private void AddCommandInternal(SqlCommand cmd)
		{
			if (cmd != null)
			{
				long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, int>("<sc.SqlDependency.AddCommandInternal|DEP> {0}, SqlCommand: {1}", this.ObjectID, cmd.ObjectID);
				try
				{
					SqlConnection connection = cmd.Connection;
					if (cmd.Notification != null)
					{
						if (cmd._sqlDep == null || cmd._sqlDep != this)
						{
							SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.AddCommandInternal|DEP|ERR> ERROR - throwing command has existing SqlNotificationRequest exception.");
							throw SQL.SqlCommandHasExistingSqlNotificationRequest();
						}
					}
					else
					{
						bool flag = false;
						object eventHandlerLock = this._eventHandlerLock;
						lock (eventHandlerLock)
						{
							if (!this._dependencyFired)
							{
								cmd.Notification = new SqlNotificationRequest
								{
									Timeout = this._timeout
								};
								if (this._options != null)
								{
									cmd.Notification.Options = this._options;
								}
								cmd._sqlDep = this;
							}
							else if (this._eventList.Count == 0)
							{
								SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependency.AddCommandInternal|DEP|ERR> ERROR - firing events, though it is unexpected we have events at this point.");
								flag = true;
							}
						}
						if (flag)
						{
							this.Invalidate(SqlNotificationType.Subscribe, SqlNotificationInfo.AlreadyChanged, SqlNotificationSource.Client);
						}
					}
				}
				finally
				{
					SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
				}
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001CDE8 File Offset: 0x0001AFE8
		private string ComputeCommandHash(string connectionString, SqlCommand command)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, int>("<sc.SqlDependency.ComputeCommandHash|DEP> {0}, SqlCommand: {1}", this.ObjectID, command.ObjectID);
			string text2;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0};{1}", connectionString, command.CommandText);
				for (int i = 0; i < command.Parameters.Count; i++)
				{
					object value = command.Parameters[i].Value;
					if (value == null || value == DBNull.Value)
					{
						stringBuilder.Append("; NULL");
					}
					else
					{
						Type type = value.GetType();
						if (type == typeof(byte[]))
						{
							stringBuilder.Append(";");
							byte[] array = (byte[])value;
							for (int j = 0; j < array.Length; j++)
							{
								stringBuilder.Append(array[j].ToString("x2", CultureInfo.InvariantCulture));
							}
						}
						else if (type == typeof(char[]))
						{
							stringBuilder.Append((char[])value);
						}
						else if (type == typeof(XmlReader))
						{
							stringBuilder.Append(";");
							stringBuilder.Append(Guid.NewGuid().ToString());
						}
						else
						{
							stringBuilder.Append(";");
							stringBuilder.Append(value.ToString());
						}
					}
				}
				string text = stringBuilder.ToString();
				SqlClientEventSource.Log.TryNotificationTraceEvent<string>("<sc.SqlDependency.ComputeCommandHash|DEP> ComputeCommandHash result: '{0}'.", text);
				text2 = text;
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
			return text2;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001CF9C File Offset: 0x0001B19C
		internal static string FixupServiceOrDatabaseName(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				return "\"" + name.Replace("\"", "\"\"") + "\"";
			}
			return name;
		}

		// Token: 0x0400021F RID: 543
		private readonly string _id = Guid.NewGuid().ToString() + ";" + SqlDependency.s_appDomainKey;

		// Token: 0x04000220 RID: 544
		private readonly string _options;

		// Token: 0x04000221 RID: 545
		private readonly int _timeout;

		// Token: 0x04000222 RID: 546
		private bool _dependencyFired;

		// Token: 0x04000223 RID: 547
		private List<SqlDependency.EventContextPair> _eventList = new List<SqlDependency.EventContextPair>();

		// Token: 0x04000224 RID: 548
		private readonly object _eventHandlerLock = new object();

		// Token: 0x04000225 RID: 549
		private DateTime _expirationTime = DateTime.MaxValue;

		// Token: 0x04000226 RID: 550
		private readonly List<string> _serverList = new List<string>();

		// Token: 0x04000227 RID: 551
		private static readonly object s_startStopLock = new object();

		// Token: 0x04000228 RID: 552
		private static readonly string s_appDomainKey = Guid.NewGuid().ToString();

		// Token: 0x04000229 RID: 553
		private static readonly Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> s_serverUserHash = new Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400022A RID: 554
		private static SqlDependencyProcessDispatcher s_processDispatcher = null;

		// Token: 0x0400022B RID: 555
		private static readonly string s_assemblyName = typeof(SqlDependencyProcessDispatcher).Assembly.FullName;

		// Token: 0x0400022C RID: 556
		private static readonly string s_typeName = typeof(SqlDependencyProcessDispatcher).FullName;

		// Token: 0x0400022D RID: 557
		private static int s_objectTypeCount;

		// Token: 0x020001CB RID: 459
		internal class IdentityUserNamePair
		{
			// Token: 0x06001DAC RID: 7596 RVA: 0x0007A6E9 File Offset: 0x000788E9
			internal IdentityUserNamePair(DbConnectionPoolIdentity identity, string userName)
			{
				this._identity = identity;
				this._userName = userName;
			}

			// Token: 0x17000A2C RID: 2604
			// (get) Token: 0x06001DAD RID: 7597 RVA: 0x0007A6FF File Offset: 0x000788FF
			internal DbConnectionPoolIdentity Identity
			{
				get
				{
					return this._identity;
				}
			}

			// Token: 0x17000A2D RID: 2605
			// (get) Token: 0x06001DAE RID: 7598 RVA: 0x0007A707 File Offset: 0x00078907
			internal string UserName
			{
				get
				{
					return this._userName;
				}
			}

			// Token: 0x06001DAF RID: 7599 RVA: 0x0007A710 File Offset: 0x00078910
			public override bool Equals(object value)
			{
				SqlDependency.IdentityUserNamePair identityUserNamePair = (SqlDependency.IdentityUserNamePair)value;
				bool flag = false;
				if (identityUserNamePair == null)
				{
					flag = false;
				}
				else if (this == identityUserNamePair)
				{
					flag = true;
				}
				else if (this._identity != null)
				{
					if (this._identity.Equals(identityUserNamePair._identity))
					{
						flag = true;
					}
				}
				else if (this._userName == identityUserNamePair._userName)
				{
					flag = true;
				}
				return flag;
			}

			// Token: 0x06001DB0 RID: 7600 RVA: 0x0007A76C File Offset: 0x0007896C
			public override int GetHashCode()
			{
				int num;
				if (this._identity != null)
				{
					num = this._identity.GetHashCode();
				}
				else
				{
					num = this._userName.GetHashCode();
				}
				return num;
			}

			// Token: 0x040013F1 RID: 5105
			private readonly DbConnectionPoolIdentity _identity;

			// Token: 0x040013F2 RID: 5106
			private readonly string _userName;
		}

		// Token: 0x020001CC RID: 460
		private class DatabaseServicePair
		{
			// Token: 0x06001DB1 RID: 7601 RVA: 0x0007A79C File Offset: 0x0007899C
			internal DatabaseServicePair(string database, string service)
			{
				this._database = database;
				this._service = service;
			}

			// Token: 0x17000A2E RID: 2606
			// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x0007A7B2 File Offset: 0x000789B2
			internal string Database
			{
				get
				{
					return this._database;
				}
			}

			// Token: 0x17000A2F RID: 2607
			// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x0007A7BA File Offset: 0x000789BA
			internal string Service
			{
				get
				{
					return this._service;
				}
			}

			// Token: 0x06001DB4 RID: 7604 RVA: 0x0007A7C4 File Offset: 0x000789C4
			public override bool Equals(object value)
			{
				SqlDependency.DatabaseServicePair databaseServicePair = (SqlDependency.DatabaseServicePair)value;
				bool flag = false;
				if (databaseServicePair == null)
				{
					flag = false;
				}
				else if (this == databaseServicePair)
				{
					flag = true;
				}
				else if (this._database == databaseServicePair._database)
				{
					flag = true;
				}
				return flag;
			}

			// Token: 0x06001DB5 RID: 7605 RVA: 0x0007A7FF File Offset: 0x000789FF
			public override int GetHashCode()
			{
				return this._database.GetHashCode();
			}

			// Token: 0x040013F3 RID: 5107
			private readonly string _database;

			// Token: 0x040013F4 RID: 5108
			private readonly string _service;
		}

		// Token: 0x020001CD RID: 461
		internal class EventContextPair
		{
			// Token: 0x06001DB6 RID: 7606 RVA: 0x0007A80C File Offset: 0x00078A0C
			internal EventContextPair(OnChangeEventHandler eventHandler, SqlDependency dependency)
			{
				this._eventHandler = eventHandler;
				this._context = ExecutionContext.Capture();
				this._dependency = dependency;
			}

			// Token: 0x06001DB7 RID: 7607 RVA: 0x0007A830 File Offset: 0x00078A30
			public override bool Equals(object value)
			{
				SqlDependency.EventContextPair eventContextPair = (SqlDependency.EventContextPair)value;
				bool flag = false;
				if (eventContextPair == null)
				{
					flag = false;
				}
				else if (this == eventContextPair)
				{
					flag = true;
				}
				else if (this._eventHandler == eventContextPair._eventHandler)
				{
					flag = true;
				}
				return flag;
			}

			// Token: 0x06001DB8 RID: 7608 RVA: 0x0007A86B File Offset: 0x00078A6B
			public override int GetHashCode()
			{
				return this._eventHandler.GetHashCode();
			}

			// Token: 0x06001DB9 RID: 7609 RVA: 0x0007A878 File Offset: 0x00078A78
			internal void Invoke(SqlNotificationEventArgs args)
			{
				this._args = args;
				ExecutionContext.Run(this._context, SqlDependency.EventContextPair.s_contextCallback, this);
			}

			// Token: 0x06001DBA RID: 7610 RVA: 0x0007A894 File Offset: 0x00078A94
			private static void InvokeCallback(object eventContextPair)
			{
				SqlDependency.EventContextPair eventContextPair2 = (SqlDependency.EventContextPair)eventContextPair;
				eventContextPair2._eventHandler(eventContextPair2._dependency, eventContextPair2._args);
			}

			// Token: 0x040013F5 RID: 5109
			private readonly OnChangeEventHandler _eventHandler;

			// Token: 0x040013F6 RID: 5110
			private readonly ExecutionContext _context;

			// Token: 0x040013F7 RID: 5111
			private readonly SqlDependency _dependency;

			// Token: 0x040013F8 RID: 5112
			private SqlNotificationEventArgs _args;

			// Token: 0x040013F9 RID: 5113
			private static readonly ContextCallback s_contextCallback = new ContextCallback(SqlDependency.EventContextPair.InvokeCallback);
		}

		// Token: 0x020001CE RID: 462
		[DataContract]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		private class SqlClientObjRef
		{
			// Token: 0x06001DBC RID: 7612 RVA: 0x000027D1 File Offset: 0x000009D1
			private SqlClientObjRef()
			{
			}

			// Token: 0x06001DBD RID: 7613 RVA: 0x0007A8D2 File Offset: 0x00078AD2
			public SqlClientObjRef(SqlDependencyProcessDispatcher dispatcher)
			{
				SqlDependency.SqlClientObjRef.s_sqlObjRef = RemotingServices.Marshal(dispatcher);
				SqlDependency.SqlClientObjRef.s_typeInfo = SqlDependency.SqlClientObjRef.s_sqlObjRef.TypeInfo;
			}

			// Token: 0x06001DBE RID: 7614 RVA: 0x0007A8F4 File Offset: 0x00078AF4
			internal static bool CanCastToSqlDependencyProcessDispatcher()
			{
				return SqlDependency.SqlClientObjRef.s_typeInfo.CanCastTo(typeof(SqlDependencyProcessDispatcher), SqlDependency.SqlClientObjRef.s_sqlObjRef);
			}

			// Token: 0x06001DBF RID: 7615 RVA: 0x0007A90F File Offset: 0x00078B0F
			internal ObjRef GetObjRef()
			{
				return SqlDependency.SqlClientObjRef.s_sqlObjRef;
			}

			// Token: 0x040013FA RID: 5114
			[DataMember]
			private static ObjRef s_sqlObjRef;

			// Token: 0x040013FB RID: 5115
			internal static IRemotingTypeInfo s_typeInfo;
		}
	}
}
