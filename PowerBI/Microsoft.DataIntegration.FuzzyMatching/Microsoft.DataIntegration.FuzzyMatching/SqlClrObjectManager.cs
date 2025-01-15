using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000D9 RID: 217
	public class SqlClrObjectManager : ObjectManagerBase, ISqlObjectManager, IObjectManager
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0002934C File Offset: 0x0002754C
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x00029354 File Offset: 0x00027554
		public int NameColumnLength { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x0002935D File Offset: 0x0002755D
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x00029365 File Offset: 0x00027565
		public int TypeColumnLength { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0002936E File Offset: 0x0002756E
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x00029376 File Offset: 0x00027576
		public string ObjectsTableName { get; private set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0002937F File Offset: 0x0002757F
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x00029387 File Offset: 0x00027587
		public string SchemaNameExtendedPropertyName { get; private set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00029390 File Offset: 0x00027590
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x00029398 File Offset: 0x00027598
		public bool CommitTemporaryObjectsToTempDb { get; set; }

		// Token: 0x060008C8 RID: 2248 RVA: 0x000293A4 File Offset: 0x000275A4
		public SqlClrObjectManager()
		{
			this.ObjectsTableName = "ObjectStore";
			this.SchemaNameExtendedPropertyName = "SchemaName";
			this.NameColumnLength = 250;
			this.TypeColumnLength = 100;
			this.CommitTemporaryObjectsToTempDb = true;
			using (ConnectionManager connectionManager = new ConnectionManager())
			{
				this.LoadSchemaName(connectionManager);
				this.LoadCommitedReferences(connectionManager, base.SchemaName);
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00029420 File Offset: 0x00027620
		private void LoadSchemaName(ConnectionManager connectionManager)
		{
			SqlConnection connection = connectionManager.GetConnection(ConnectionManager.ContextConnectionName);
			using (SqlCommand sqlCommand = connection.CreateCommand())
			{
				sqlCommand.CommandTimeout = connection.ConnectionTimeout;
				sqlCommand.CommandText = string.Format("\r\n                    SELECT value\r\n                    FROM fn_listextendedproperty({0}, N'ASSEMBLY', {1}, default, default, default, default)", SqlName.CreateStringLiteral(this.SchemaNameExtendedPropertyName), SqlName.CreateStringLiteral(Assembly.GetExecutingAssembly().GetName().Name));
				object obj = sqlCommand.ExecuteScalar();
				if (obj == null || !(obj is string))
				{
					throw new Exception(string.Format("Unable to load the extended property {0} from assembly {1}.  This property must be set when the assembly is created.", this.SchemaNameExtendedPropertyName, Assembly.GetExecutingAssembly().GetName().Name));
				}
				base.SchemaName = (string)obj;
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000294DC File Offset: 0x000276DC
		private void LoadCommitedReferences(ConnectionManager connectionManager)
		{
			List<string> list = new List<string>();
			using (SqlCommand sqlCommand = connectionManager.GetConnection(ConnectionManager.ContextConnectionName).CreateCommand())
			{
				sqlCommand.CommandText = "\r\n                    select s.name, t.name\r\n                    from sys.tables as t with (nolock)\r\n                    join sys.schemas as s with (nolock)\r\n                    on t.schema_id = s.schema_id\r\n                    where t.name=@name";
				sqlCommand.Parameters.AddWithValue("@name", this.ObjectsTableName);
				using (IDataReader dataReader = sqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						list.Add(dataReader[0].ToString());
					}
				}
			}
			foreach (string text in list)
			{
				this.LoadCommitedReferences(connectionManager, text);
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000295B8 File Offset: 0x000277B8
		private void LoadCommitedReferences(ConnectionManager connectionManager, string schemaName)
		{
			SqlConnection connection = connectionManager.GetConnection(ConnectionManager.ContextConnectionName);
			using (SqlCommand sqlCommand = connection.CreateCommand())
			{
				SqlName sqlName = new SqlName(schemaName, this.ObjectsTableName);
				sqlCommand.CommandTimeout = connection.ConnectionTimeout;
				sqlCommand.CommandText = string.Format("if (select object_id({0})) is not null select Name, Type, Definition, len(Serialized) as Length from {1} with (nolock)", SqlName.CreateStringLiteral(sqlName.QualifiedName), sqlName.QualifiedName);
				using (IDataReader dataReader = sqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						ObjectReference objectReference = new ObjectReference
						{
							SqlSchemaName = schemaName,
							Name = (string)dataReader[0],
							Type = Type.GetType((string)dataReader[1]),
							PersistedSize = (long)dataReader[3]
						};
						this.m_referencesByName.Add(this.GetQualifiedObjectName(schemaName, objectReference.Name), objectReference);
					}
				}
			}
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x000296BC File Offset: 0x000278BC
		public void Drop(string objectName)
		{
			this.Drop(objectName, null);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x000296C6 File Offset: 0x000278C6
		public void Drop(string objectName, ConnectionManager connectionManager)
		{
			this.Drop(objectName, connectionManager, true, false);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x000296D4 File Offset: 0x000278D4
		public void Drop(string objectName, ConnectionManager connectionManager, bool dropTables, bool useTempDb)
		{
			string qualifiedObjectName = this.GetQualifiedObjectName(objectName);
			lock (this)
			{
				ObjectReference objectReference;
				if (!this.m_referencesByName.TryGetValue(qualifiedObjectName, ref objectReference))
				{
					throw new ArgumentException(string.Format("An object with name {0} does not exist.", objectName));
				}
				if (dropTables)
				{
					try
					{
						object obj = objectReference.TryGetStrongReference();
						if (obj == null && objectReference.PersistedSize >= 0L)
						{
							obj = this.Load(objectName, connectionManager);
						}
						if (obj is IDropTables)
						{
							(obj as IDropTables).TryDropTables(connectionManager);
						}
					}
					catch (Exception)
					{
					}
				}
				foreach (TemporalHandle temporalHandle in objectReference.Handles)
				{
					this.m_objectHandlesById.Remove(temporalHandle.Id);
				}
				this.m_referencesByName.Remove(qualifiedObjectName);
				if (objectReference.PersistedSize >= 0L)
				{
					ConnectionManager connectionManager2 = null;
					if (connectionManager == null)
					{
						connectionManager2 = new ConnectionManager();
						connectionManager = connectionManager2;
					}
					try
					{
						SqlConnection connection = connectionManager.GetConnection(ConnectionManager.ContextConnectionName);
						using (SqlCommand sqlCommand = connection.CreateCommand())
						{
							SqlName sqlName = new SqlName(base.SchemaName, this.ObjectsTableName);
							sqlCommand.CommandTimeout = connection.ConnectionTimeout;
							sqlCommand.CommandText = string.Format("if (select object_id({0})) is not null delete from {1} where Name=@Name", SqlName.CreateStringLiteral(sqlName.QualifiedName), sqlName.QualifiedName);
							sqlCommand.Parameters.Add("@Name", 12, this.NameColumnLength).Value = objectName;
							sqlCommand.ExecuteNonQuery();
						}
					}
					catch (Exception)
					{
					}
					finally
					{
						if (connectionManager2 != null)
						{
							connectionManager2.Dispose();
						}
					}
				}
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000298FC File Offset: 0x00027AFC
		public void DropAll()
		{
			lock (this)
			{
				using (ConnectionManager connectionManager = new ConnectionManager())
				{
					foreach (ObjectReference objectReference in Enumerable.ToArray<ObjectReference>(this.m_referencesByName.Values))
					{
						this.Drop(objectReference.Name, connectionManager);
					}
				}
				using (ConnectionManager connectionManager2 = new ConnectionManager())
				{
					using (SqlCommand sqlCommand = connectionManager2.GetConnection(ConnectionManager.ContextConnectionName).CreateCommand())
					{
						SqlName sqlName = new SqlName(base.SchemaName, this.ObjectsTableName);
						sqlCommand.CommandText = string.Format("if (select object_id({0})) is not null TRUNCATE TABLE {1}", SqlName.CreateStringLiteral(sqlName.QualifiedName), sqlName.QualifiedName);
						sqlCommand.ExecuteNonQuery();
					}
				}
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00029A04 File Offset: 0x00027C04
		public override TemporalHandle GetObjectHandle(string objectName, int timeout)
		{
			return this.GetObjectHandle(objectName, timeout, true);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00029A0F File Offset: 0x00027C0F
		public TemporalHandle GetObjectHandle(string objectName, int timeout, bool checkTempDb)
		{
			return this.GetObjectHandle(objectName, timeout, checkTempDb, true);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00029A1C File Offset: 0x00027C1C
		public TemporalHandle GetObjectHandle(string objectName, int timeout, bool checkTempDb, bool loadOnNewConnection)
		{
			string qualifiedObjectName = this.GetQualifiedObjectName(objectName);
			TemporalHandle temporalHandle3;
			lock (this)
			{
				if (!base.EnableCollectionTimer)
				{
					base.Collect();
				}
				object obj = null;
				DateTime dateTime = default(DateTime);
				ObjectReference reference;
				if (this.m_referencesByName.TryGetValue(qualifiedObjectName, ref reference))
				{
					dateTime = reference.LastLoaded;
					obj = reference.TryGetStrongReference();
					if (obj == null && reference.PersistedSize < 0L)
					{
						throw new InvalidOperationException(string.Format("The specified object {0} is no longer valid and has not been committed.  Consider increasing the timeout used to create the object.", objectName));
					}
				}
				long num = -1L;
				if (obj == null)
				{
					if (loadOnNewConnection)
					{
						using (ConnectionManager connectionManager = new ConnectionManager())
						{
							using (SqlConnection connection = connectionManager.GetConnection(ConnectionManager.ContextConnectionName))
							{
								using (SqlCommand sqlCommand = connection.CreateCommand())
								{
									sqlCommand.CommandText = string.Format("exec {0}.GetObjectHandle3 {1}, {2}, 0", SqlName.DelimitElement(base.SchemaName), SqlName.CreateStringLiteral(objectName), timeout.ToString());
									sqlCommand.ExecuteNonQuery();
									if (this.m_referencesByName.TryGetValue(qualifiedObjectName, ref reference))
									{
										dateTime = reference.LastLoaded;
										obj = reference.TryGetStrongReference();
										if (obj == null && reference.PersistedSize < 0L)
										{
											throw new InvalidOperationException(string.Format("The specified object {0} is no longer valid and has not been committed.  Consider increasing the timeout used to create the object.", objectName));
										}
									}
									if (obj == null)
									{
										throw new Exception(string.Format("Unable to load object {0}.", objectName));
									}
									goto IL_01E1;
								}
							}
						}
					}
					if (!this.TryLoad(objectName, null, base.SchemaName, this.ObjectsTableName, out obj, out num) && (!checkTempDb || !this.TryLoad(objectName, null, string.Empty, "#" + this.ObjectsTableName, out obj, out num)))
					{
						using (ConnectionManager connectionManager2 = new ConnectionManager())
						{
							using (SqlCommand sqlCommand2 = connectionManager2.GetConnection(ConnectionManager.ContextConnectionName).CreateCommand())
							{
								sqlCommand2.CommandText = "select db_name()";
								string text = (string)sqlCommand2.ExecuteScalar();
								throw new ArgumentException(string.Format("The specified object {0} does not exist.  It may have timed out and/or was never committed.  The current database is {1}.", objectName, text));
							}
						}
					}
					IL_01E1:
					dateTime = DateTime.Now;
				}
				DateTime now = DateTime.Now;
				if (reference != null)
				{
					reference.StrongReference = obj;
					reference.WeakReference = null;
					reference.LastLoaded = dateTime;
				}
				else
				{
					base.CreateReference(objectName, obj);
					reference = base.GetReference(objectName);
					reference.PersistedSize = num;
				}
				reference.LastAccessed = now;
				TemporalHandle temporalHandle = null;
				foreach (TemporalHandle temporalHandle2 in reference.Handles)
				{
					if (temporalHandle2.Timeout == timeout)
					{
						temporalHandle2.LastAccessed = now;
						temporalHandle = temporalHandle2;
						break;
					}
				}
				if (temporalHandle == null)
				{
					temporalHandle = new TemporalHandle
					{
						Id = base.NextObjectHandle(),
						Reference = reference,
						Timeout = timeout,
						LastAccessed = now
					};
					reference.Handles.Add(temporalHandle);
					this.m_objectHandlesById.Add(temporalHandle.Id, temporalHandle);
				}
				temporalHandle3 = temporalHandle;
			}
			return temporalHandle3;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00029DB0 File Offset: 0x00027FB0
		private bool TryLoad(string objectName, ConnectionManager connectionManager, string schemaName, string tableName, out object obj, out long persistedSize)
		{
			ConnectionManager connectionManager2 = null;
			byte[] array = null;
			try
			{
				if (connectionManager == null)
				{
					connectionManager2 = new ConnectionManager();
					connectionManager = connectionManager2;
				}
				SqlConnection connection = connectionManager.GetConnection(ConnectionManager.ContextConnectionName);
				using (SqlCommand sqlCommand = connection.CreateCommand())
				{
					SqlName sqlName = new SqlName(schemaName, tableName);
					sqlCommand.CommandTimeout = connection.ConnectionTimeout;
					sqlCommand.CommandText = string.Format("if (select object_id({0})) is not null\r\n                                                  select Serialized \r\n                                                  from {1} with (nolock)\r\n                                                  where Name=@Name", SqlName.CreateStringLiteral(sqlName.QualifiedName), sqlName.QualifiedName);
					sqlCommand.Parameters.Add("@Name", 12, this.NameColumnLength).Value = objectName;
					using (IDataReader dataReader = sqlCommand.ExecuteReader(16))
					{
						if (dataReader.Read())
						{
							long bytes = dataReader.GetBytes(0, 0L, null, 0, 0);
							array = new byte[bytes];
							dataReader.GetBytes(0, 0L, array, 0, (int)bytes);
						}
					}
				}
			}
			finally
			{
				if (connectionManager2 != null)
				{
					connectionManager2.Dispose();
				}
			}
			if (array != null)
			{
				MemoryStream memoryStream = new MemoryStream(array);
				FuzzyLookupFormatter fuzzyLookupFormatter = new FuzzyLookupFormatter();
				obj = fuzzyLookupFormatter.Deserialize(memoryStream);
				if (obj is IObjectReferenceContainer)
				{
					(obj as IObjectReferenceContainer).AcquireReferences();
				}
				persistedSize = (long)array.Length;
				return true;
			}
			obj = null;
			persistedSize = -1L;
			return false;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00029F04 File Offset: 0x00028104
		private object Load(string objectName, ConnectionManager connectionManager)
		{
			long num = -1L;
			object obj;
			if (!this.TryLoad(objectName, connectionManager, base.SchemaName, this.ObjectsTableName, out obj, out num))
			{
				throw new ArgumentException(string.Format("Unable to load object '{0}'.  It doesn't appear to exist in the objects table.  Ensure that the object was Commited.", objectName));
			}
			return obj;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00029F40 File Offset: 0x00028140
		public void CommitToTempDb(int handle)
		{
			using (ConnectionManager connectionManager = new ConnectionManager())
			{
				this.Commit(handle, null, connectionManager, ConnectionManager.ContextConnectionName, true);
			}
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00029F80 File Offset: 0x00028180
		public void Commit(string objectName, object obj, SqlXml xmlDefinition, bool overwriteExisting)
		{
			lock (this)
			{
				if (overwriteExisting && base.Contains(objectName))
				{
					this.Drop(objectName, null, true, false);
				}
				int num = base.CreateReference(objectName, obj);
				this.Commit(num, xmlDefinition);
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00029FD8 File Offset: 0x000281D8
		public void Commit(int handle)
		{
			using (ConnectionManager connectionManager = new ConnectionManager())
			{
				this.Commit(handle, null, connectionManager, ConnectionManager.ContextConnectionName);
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0002A018 File Offset: 0x00028218
		public void Commit(int handle, SqlXml xmlDefinition)
		{
			using (ConnectionManager connectionManager = new ConnectionManager())
			{
				this.Commit(handle, xmlDefinition, connectionManager, ConnectionManager.ContextConnectionName);
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0002A058 File Offset: 0x00028258
		public void Commit(int handle, SqlXml xmlDefinition, ConnectionManager connectionManager, string connectionName)
		{
			this.Commit(handle, xmlDefinition, connectionManager, connectionName, false);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002A068 File Offset: 0x00028268
		public void Commit(int handle, SqlXml xmlDefinition, ConnectionManager connectionManager, string connectionName, bool useTempDb)
		{
			lock (this)
			{
				TemporalHandle objectHandle = base.GetObjectHandle(handle);
				object @object = base.GetObject(handle);
				string text = (useTempDb ? "" : base.SchemaName);
				string text2 = (useTempDb ? ("#" + this.ObjectsTableName) : this.ObjectsTableName);
				this.CreateObjectsTable(connectionManager, connectionName, text, text2);
				FuzzyLookupFormatter fuzzyLookupFormatter = new FuzzyLookupFormatter();
				using (MemoryStream memoryStream = new MemoryStream())
				{
					fuzzyLookupFormatter.Serialize(memoryStream, @object);
					memoryStream.Flush();
					memoryStream.Seek(0L, 0);
					SqlConnection connection = connectionManager.GetConnection(connectionName);
					SqlName sqlName = new SqlName(base.SchemaName, this.ObjectsTableName);
					using (SqlCommand sqlCommand = connection.CreateCommand())
					{
						sqlCommand.CommandText = string.Format("\r\n                        MERGE {0} AS target\r\n                        USING (SELECT @Name, @Type, CURRENT_TIMESTAMP, @Definition, @Serialized) AS source (Name, Type, create_date, Definition, Serialized)\r\n                        ON (target.Name = source.Name)\r\n                        WHEN MATCHED THEN \r\n                            UPDATE SET Type = source.Type, create_date = source.create_date, Definition = source.Definition, Serialized = source.Serialized\r\n                        WHEN NOT MATCHED THEN\r\n                            INSERT (Name, Type, create_date, Definition, Serialized) VALUES (source.Name, source.Type, source.create_date, source.Definition, source.Serialized);", sqlName);
						sqlCommand.Parameters.Add("@Name", 12, this.NameColumnLength).Value = objectHandle.Reference.Name;
						sqlCommand.Parameters.Add("@Type", 12, this.TypeColumnLength).Value = @object.GetType().ToString();
						sqlCommand.Parameters.Add("@Definition", 25).Value = xmlDefinition;
						sqlCommand.Parameters.Add("@Serialized", 7).Value = memoryStream.ToArray();
						sqlCommand.ExecuteNonQuery();
					}
					objectHandle.Reference.PersistedSize = memoryStream.Length;
				}
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0002A23C File Offset: 0x0002843C
		public int Rollback(string objectName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0002A244 File Offset: 0x00028444
		private void CreateObjectsTable(ConnectionManager connectionManager, string connectionName, string schemaName, string tableName)
		{
			lock (this)
			{
				SqlConnection connection = connectionManager.GetConnection(connectionName);
				using (SqlCommand sqlCommand = connection.CreateCommand())
				{
					SqlName sqlName = new SqlName(schemaName, tableName);
					if (!SqlUtils.TableExists(connection, sqlName))
					{
						sqlCommand.CommandTimeout = connection.ConnectionTimeout;
						sqlCommand.CommandText = string.Format("create table {0}(Name nvarchar({1}) not null, Type nvarchar({2}), create_date datetime, Definition xml, Serialized varbinary(max))", sqlName.QualifiedName, this.NameColumnLength, this.TypeColumnLength);
						sqlCommand.ExecuteNonQuery();
						sqlCommand.CommandText = string.Format("ALTER TABLE {0} ADD CONSTRAINT [{1}] PRIMARY KEY ({2})", sqlName.QualifiedName, Guid.NewGuid().ToString(), "Name");
						sqlCommand.ExecuteNonQuery();
					}
				}
			}
		}
	}
}
