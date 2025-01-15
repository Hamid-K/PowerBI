using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.Library.Soap2010;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200002E RID: 46
	internal class DBInterface : Storage, IDBInterface, IRSStorage
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00006634 File Offset: 0x00004834
		public DBInterface()
		{
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006647 File Offset: 0x00004847
		public void ThrowIfSchedulerNotRunning()
		{
			this.m_scheduleDB.ThrowIfSchedulerNotRunning();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006654 File Offset: 0x00004854
		public bool IsSchedulerRunning()
		{
			return this.m_scheduleDB.IsSchedulerRunning();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006661 File Offset: 0x00004861
		public DBInterface(UserContext userContext)
		{
			this.m_userContext = userContext;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000667B File Offset: 0x0000487B
		protected UserContext UserContext
		{
			get
			{
				RSTrace.CatalogTrace.Assert(this.m_userContext != null);
				return this.m_userContext;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000066AB File Offset: 0x000048AB
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00006696 File Offset: 0x00004896
		public override ConnectionManager ConnectionManager
		{
			get
			{
				return base.ConnectionManager;
			}
			set
			{
				base.ConnectionManager = value;
				this.m_scheduleDB.ConnectionManager = value;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000066B4 File Offset: 0x000048B4
		public void AddReportToExecutionCache(Guid reportId, ReportSnapshot snapshotData, DateTime executionDateTime, bool useEditSessionTimeout, out DateTime expirationDateTime)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("AddReportToCache", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ReportID", reportId).SqlDbType = SqlDbType.UniqueIdentifier;
				SqlParameter sqlParameter = instrumentedSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier);
				sqlParameter.Value = snapshotData.SnapshotDataID;
				instrumentedSqlCommand.Parameters.AddWithValue("@ExecutionDate", executionDateTime).SqlDbType = SqlDbType.DateTime;
				if (useEditSessionTimeout)
				{
					instrumentedSqlCommand.Parameters.Add("@EditSessionTimeout", SqlDbType.Int).Value = Global.EditSessionTimeoutMinutes;
					instrumentedSqlCommand.Parameters.Add("@CacheLimit", SqlDbType.Int).Value = Global.EditSessionCacheLimit;
				}
				RSTrace.CatalogTrace.Assert(snapshotData.SnapshotParameters != null, "snapshot parameters");
				instrumentedSqlCommand.Parameters.Add("@QueryParamsHash", SqlDbType.Int).Value = snapshotData.SnapshotQueryParametersHash;
				SqlParameter sqlParameter2 = instrumentedSqlCommand.Parameters.Add("@ExpirationDate", SqlDbType.DateTime);
				sqlParameter2.Direction = ParameterDirection.Output;
				SqlParameter sqlParameter3 = instrumentedSqlCommand.Parameters.Add("@ScheduleID", SqlDbType.UniqueIdentifier);
				sqlParameter3.Direction = ParameterDirection.Output;
				instrumentedSqlCommand.ExecuteNonQuery();
				Guid guid = (Guid)sqlParameter.Value;
				expirationDateTime = DateTime.MinValue;
				if (sqlParameter2.Value != DBNull.Value)
				{
					expirationDateTime = (DateTime)sqlParameter2.Value;
				}
				Guid guid2 = Guid.Empty;
				if (sqlParameter3.Value != DBNull.Value)
				{
					guid2 = (Guid)sqlParameter3.Value;
				}
				if (guid2 != Guid.Empty)
				{
					expirationDateTime = this.m_scheduleDB.CheckNextRunTime(guid2, expirationDateTime);
				}
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006888 File Offset: 0x00004A88
		public void SetCacheLastUsed(ServerSnapshot snapshot)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetCacheLastUsed", null))
			{
				instrumentedSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier).Value = snapshot.SnapshotDataID;
				instrumentedSqlCommand.Parameters.Add("@Timestamp", SqlDbType.DateTime).Value = DateTime.Now;
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006908 File Offset: 0x00004B08
		public bool AddToFavorites(string userName, Guid itemId)
		{
			bool flag;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("AddItemToFavorites", null))
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", itemId);
					instrumentedSqlCommand.AddParameter("@UserName", SqlDbType.NVarChar, userName);
					instrumentedSqlCommand.Parameters.AddWithValue("@UserSid", Global.NameToSid(this.UserContext));
					instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType).SqlDbType = SqlDbType.Int;
					flag = instrumentedSqlCommand.ExecuteNonQuery() > 0;
				}
			}
			catch (ReportServerStorageException ex)
			{
				if (!ex.IsSqlException)
				{
					throw;
				}
				if (ex.SqlErrorNumber == Native.SqlConstraintViolationCode || ex.SqlErrorNumber == Native.SqlUniqueIndexViolationCode)
				{
					throw new ItemNotFoundException(itemId.ToString(), "CatalogItem");
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000069FC File Offset: 0x00004BFC
		public bool RemoveFromFavorites(string userName, Guid itemId)
		{
			bool flag;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("RemoveItemFromFavorites", null))
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", itemId);
					instrumentedSqlCommand.AddParameter("@UserName", SqlDbType.NVarChar, userName);
					instrumentedSqlCommand.Parameters.AddWithValue("@UserSid", Global.NameToSid(this.UserContext));
					instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType).SqlDbType = SqlDbType.Int;
					flag = instrumentedSqlCommand.ExecuteNonQuery() > 0;
				}
			}
			catch (ReportServerStorageException ex)
			{
				if (!ex.IsSqlException)
				{
					throw;
				}
				if (ex.SqlErrorNumber == Native.SqlConstraintViolationCode || ex.SqlErrorNumber == Native.SqlUniqueIndexViolationCode)
				{
					throw new ItemNotFoundException(itemId.ToString(), "CatalogItem");
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006AF0 File Offset: 0x00004CF0
		public bool IsFavoriteItem(string userName, Guid itemId)
		{
			bool flag;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("IsFavoriteItem", null))
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", itemId);
					instrumentedSqlCommand.AddParameter("@UserName", SqlDbType.NVarChar, userName);
					instrumentedSqlCommand.Parameters.AddWithValue("@UserSid", Global.NameToSid(this.UserContext));
					instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType).SqlDbType = SqlDbType.Int;
					flag = (bool)instrumentedSqlCommand.ExecuteScalar();
				}
			}
			catch (ReportServerStorageException ex)
			{
				if (!ex.IsSqlException)
				{
					throw;
				}
				if (ex.SqlErrorNumber == Native.SqlConstraintViolationCode || ex.SqlErrorNumber == Native.SqlUniqueIndexViolationCode)
				{
					throw new ItemNotFoundException(itemId.ToString(), "CatalogItem");
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006BE8 File Offset: 0x00004DE8
		public Guid CreateObject(Guid id, string shortName, CatalogItemPath fullPath, ExternalItemPath parentPath, Guid parentId, ItemType objectType, byte[] objectContent, Guid intermediateSnapshotID, Guid link, string linkPath, ItemProperties objectProperties, string parameters, byte[] createdBySid, string createdBy, DateTime creationDate, DateTime modificationDate, string mimeType)
		{
			return this.CreateObject(id, shortName, fullPath, parentPath, parentId, objectType, objectContent, intermediateSnapshotID, link, linkPath, objectProperties, parameters, createdBySid, createdBy, creationDate, modificationDate, mimeType, null, Guid.Empty);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006C20 File Offset: 0x00004E20
		public virtual Guid CreateObject(Guid id, string shortName, CatalogItemPath fullPath, ExternalItemPath parentPath, Guid parentId, ItemType objectType, byte[] objectContent, Guid intermediateSnapshotID, Guid link, string linkPath, ItemProperties objectProperties, string parameters, string createdBy, DateTime creationDate, DateTime modificationDate, string mimeType, string subType, Guid componentId)
		{
			byte[] array = Global.NameToSid(createdBy, this.UserContext.AuthenticationType);
			return this.CreateObject(id, shortName, fullPath, parentPath, parentId, objectType, objectContent, intermediateSnapshotID, link, linkPath, objectProperties, parameters, array, createdBy, creationDate, modificationDate, mimeType, subType, componentId);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006C68 File Offset: 0x00004E68
		public Guid CreateObject(Guid id, string shortName, CatalogItemPath fullPath, ExternalItemPath parentPath, Guid parentId, ItemType objectType, byte[] objectContent, Guid intermediateSnapshotID, Guid link, string linkPath, ItemProperties objectProperties, string parameters, byte[] createdBySid, string createdByName, DateTime creationDate, DateTime modificationDate, string mimeType, string subType, Guid componentId)
		{
			Guid guid2;
			try
			{
				string text2;
				bool flag;
				string text = ItemProperties.PrepareForSaving(objectProperties, out text2, out flag);
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CreateObject", null))
				{
					Guid guid;
					if (id == Guid.Empty)
					{
						guid = Guid.NewGuid();
					}
					else
					{
						guid = id;
					}
					instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", guid);
					instrumentedSqlCommand.AddParameter("@Name", SqlDbType.NVarChar, shortName);
					instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, fullPath.Value);
					instrumentedSqlCommand.Parameters.AddWithValue("@ParentID", parentId);
					instrumentedSqlCommand.Parameters.AddWithValue("@Type", (int)objectType);
					if (objectContent != null)
					{
						instrumentedSqlCommand.Parameters.Add("@Content", SqlDbType.Image).Value = objectContent;
					}
					if (intermediateSnapshotID != Guid.Empty)
					{
						instrumentedSqlCommand.Parameters.Add("@Intermediate", SqlDbType.UniqueIdentifier).Value = intermediateSnapshotID;
					}
					if (link != Guid.Empty)
					{
						instrumentedSqlCommand.Parameters.AddWithValue("@LinkSourceID", link);
					}
					if (text != null)
					{
						instrumentedSqlCommand.AddParameter("@Property", SqlDbType.NText, text);
					}
					if (parameters != null)
					{
						instrumentedSqlCommand.AddParameter("@Parameter", SqlDbType.NText, parameters);
					}
					if (text2 != null)
					{
						instrumentedSqlCommand.AddParameter("@Description", SqlDbType.NText, text2);
					}
					instrumentedSqlCommand.Parameters.AddWithValue("@Hidden", flag);
					instrumentedSqlCommand.Parameters.AddWithValue("@CreatedBySid", createdBySid).SqlDbType = SqlDbType.VarBinary;
					instrumentedSqlCommand.Parameters.AddWithValue("@CreatedByName", createdByName).SqlDbType = SqlDbType.NVarChar;
					instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType).SqlDbType = SqlDbType.Int;
					instrumentedSqlCommand.Parameters.AddWithValue("@CreationDate", creationDate);
					instrumentedSqlCommand.Parameters.AddWithValue("@ModificationDate", modificationDate);
					if (mimeType != null)
					{
						instrumentedSqlCommand.AddParameter("@MimeType", SqlDbType.NVarChar, mimeType);
					}
					instrumentedSqlCommand.Parameters.AddWithValue("@SubType", subType);
					if (componentId != Guid.Empty)
					{
						RSTrace.CatalogTrace.Assert(objectType == ItemType.Component, "objectType & componentId");
						instrumentedSqlCommand.Parameters.AddWithValue("@ComponentID", componentId);
					}
					instrumentedSqlCommand.ExecuteNonQuery();
					guid2 = guid;
				}
			}
			catch (ReportServerStorageException ex)
			{
				if (!ex.IsSqlException)
				{
					throw;
				}
				if (ex.SqlErrorNumber == Native.SqlUniqueIndexViolationCode)
				{
					guid2 = Guid.Empty;
				}
				else
				{
					if (ex.SqlErrorNumber == Native.SqlConstraintViolationCode)
					{
						throw new ItemNotFoundException(linkPath, "link");
					}
					if (ex.SqlErrorMessage == "Parent Not Found")
					{
						throw new ItemNotFoundException(parentPath.Value);
					}
					throw;
				}
			}
			return guid2;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006F68 File Offset: 0x00005168
		public virtual bool DeleteObject(ExternalItemPath objectName)
		{
			return this.CatalogDeleteObject(objectName.NativeCatalogItemPath);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006F78 File Offset: 0x00005178
		public bool CatalogDeleteObject(CatalogItemPath objectName)
		{
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteObject", null))
				{
					Storage.BindItemPathToCommand(objectName, instrumentedSqlCommand, true, this.UserContext);
					instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
					instrumentedSqlCommand.AddParameter("@Prefix", SqlDbType.NVarChar, Storage.EncodeForLike(objectName.Value) + "/%");
					if (instrumentedSqlCommand.ExecuteNonQuery() > 0)
					{
						return true;
					}
					return false;
				}
			}
			catch (ReportServerStorageException ex)
			{
				if (!ex.IsSqlException)
				{
					throw;
				}
				if (ex.SqlErrorNumber != 14262)
				{
					throw;
				}
			}
			return true;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00007038 File Offset: 0x00005238
		public virtual bool CheckChildrenBeforeDelete(ExternalItemPath objectName, Security secMgr)
		{
			return objectName.IsEditSession || this.InnerCheckChildrenBeforeDelete(objectName, secMgr);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000704C File Offset: 0x0000524C
		protected virtual bool InnerCheckChildrenBeforeDelete(ExternalItemPath objectName, Security secMgr)
		{
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetChildrenBeforeDelete", null))
			{
				instrumentedSqlCommand.AddParameter("@Prefix", SqlDbType.NVarChar, Storage.EncodeForLike(objectName.NativeCatalogPath) + "/%");
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					SecurityCheckCache securityCheckCache = new SecurityCheckCache(secMgr, CommonOperation.Delete);
					while (dataReader.Read())
					{
						Guid guid = dataReader.GetGuid(0);
						ItemType @int = (ItemType)dataReader.GetInt32(1);
						SecurityCheckCache.CheckResult checkResult = securityCheckCache.CheckAccess(@int, guid);
						if (checkResult == SecurityCheckCache.CheckResult.NotChecked)
						{
							byte[] array = null;
							if (!dataReader.IsDBNull(2))
							{
								array = DataReaderHelper.ReadAllBytes(dataReader, 2);
							}
							checkResult = securityCheckCache.CheckAccess(@int, guid, array, objectName);
						}
						if (checkResult != SecurityCheckCache.CheckResult.AccessGranted)
						{
							return false;
						}
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00007148 File Offset: 0x00005348
		public bool MoveObject(CatalogItemPath oldExternalPath, string newShortName, CatalogItemPath newExternalPath, Guid newParentId, bool renameOnly)
		{
			string value = oldExternalPath.Value;
			string value2 = newExternalPath.Value;
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("MoveObject", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@OldPath", value).SqlDbType = SqlDbType.NVarChar;
				instrumentedSqlCommand.Parameters.AddWithValue("@OldPrefix", Storage.EncodeForLike(value) + "/%").SqlDbType = SqlDbType.NVarChar;
				instrumentedSqlCommand.Parameters.AddWithValue("@NewName", newShortName).SqlDbType = SqlDbType.NVarChar;
				instrumentedSqlCommand.Parameters.AddWithValue("@NewPath", value2).SqlDbType = SqlDbType.NVarChar;
				instrumentedSqlCommand.Parameters.AddWithValue("@NewParentID", newParentId).SqlDbType = SqlDbType.UniqueIdentifier;
				instrumentedSqlCommand.Parameters.AddWithValue("@RenameOnly", renameOnly).SqlDbType = SqlDbType.Bit;
				instrumentedSqlCommand.Parameters.Add("@MaxPathLength", SqlDbType.Int).Value = CatalogItemNameUtility.MaxItemPathLength;
				try
				{
					using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
					{
						if (dataReader.Read())
						{
							string @string = dataReader.GetString(0);
							throw new ItemPathLengthExceededException(value2 + @string.Substring(value.Length));
						}
						flag = true;
					}
				}
				catch (ReportServerStorageException ex)
				{
					if (!ex.IsSqlException)
					{
						throw;
					}
					if (ex.SqlErrorNumber != Native.SqlUniqueIndexViolationCode)
					{
						throw;
					}
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000072F0 File Offset: 0x000054F0
		public void SetObjectContent(CatalogItemPath objectName, ItemType objectType, byte[] objectContent, Guid intermediateSnapshotID, string parameters, Guid link, string mimeType)
		{
			byte[] array = null;
			this.SetObjectContent(objectName, objectType, objectContent, intermediateSnapshotID, parameters, link, mimeType, array);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007314 File Offset: 0x00005514
		public void SetObjectContent(CatalogItemPath objectName, ItemType objectType, byte[] objectContent, Guid intermediateSnapshotID, string parameters, Guid link, string mimeType, byte[] dataCacheHash)
		{
			this.SetObjectContent(objectName, objectType, objectContent, intermediateSnapshotID, parameters, link, mimeType, dataCacheHash, null, Guid.Empty);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000733C File Offset: 0x0000553C
		public void SetObjectContent(CatalogItemPath objectName, ItemType objectType, byte[] objectContent, Guid intermediateSnapshotID, string parameters, Guid link, string mimeType, byte[] dataCacheHash, string subType, Guid componentID)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetObjectContent", null))
			{
				Storage.BindItemPathToCommand(objectName, instrumentedSqlCommand, true, null);
				if (objectName.IsEditSession)
				{
					RSTrace.CatalogTrace.Assert(objectType == ItemType.Report, "objectType == Report");
				}
				instrumentedSqlCommand.Parameters.AddWithValue("@Type", (int)objectType);
				if (objectContent != null)
				{
					instrumentedSqlCommand.Parameters.Add("@Content", SqlDbType.Image).Value = objectContent;
				}
				if (intermediateSnapshotID != Guid.Empty)
				{
					instrumentedSqlCommand.Parameters.Add("@Intermediate", SqlDbType.UniqueIdentifier).Value = intermediateSnapshotID;
				}
				if (parameters != null)
				{
					instrumentedSqlCommand.AddParameter("@Parameter", SqlDbType.NText, parameters);
				}
				if (link != Guid.Empty)
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@LinkSourceID", link);
				}
				if (mimeType != null)
				{
					instrumentedSqlCommand.AddParameter("@MimeType", SqlDbType.NVarChar, mimeType);
				}
				if (dataCacheHash != null)
				{
					instrumentedSqlCommand.Parameters.Add("@DataCacheHash", SqlDbType.VarBinary).Value = dataCacheHash;
				}
				if (subType != null)
				{
					instrumentedSqlCommand.Parameters.Add("@SubType", SqlDbType.NVarChar).Value = subType;
				}
				if (componentID != Guid.Empty)
				{
					instrumentedSqlCommand.Parameters.Add("@ComponentID", SqlDbType.UniqueIdentifier).Value = componentID;
				}
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000074BC File Offset: 0x000056BC
		public bool ObjectExists(ExternalItemPath objectName)
		{
			ItemType itemType;
			Guid guid;
			int num;
			return this.ObjectExists(objectName, out itemType, out guid, out num);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000074D8 File Offset: 0x000056D8
		public bool ObjectExists(ExternalItemPath objectName, out ItemType type)
		{
			Guid guid;
			int num;
			return this.ObjectExists(objectName, out type, out guid, out num);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000074F4 File Offset: 0x000056F4
		public bool ObjectExists(ExternalItemPath objectName, out ItemType type, out byte[] secDesc)
		{
			Guid guid;
			int num;
			return this.ObjectExists(objectName, out type, out guid, out num, out secDesc);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00007510 File Offset: 0x00005710
		public bool ObjectExists(ExternalItemPath objectName, out ItemType type, out Guid id, out byte[] secDesc)
		{
			int num;
			return this.ObjectExists(objectName, out type, out id, out num, out secDesc);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000752C File Offset: 0x0000572C
		public bool ObjectExists(ExternalItemPath objectName, out ItemType type, out Guid id, out byte[] secDesc, out Guid snapshotId)
		{
			int num;
			int num2;
			Guid guid;
			return this.ObjectExists(objectName, out type, out id, out num, out secDesc, out num2, out snapshotId, out guid);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000754C File Offset: 0x0000574C
		public bool ObjectExists(ExternalItemPath objectName, out ItemType type, out Guid id, out int snapshotLimit)
		{
			byte[] array = null;
			return this.ObjectExists(objectName, out type, out id, out snapshotLimit, out array);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00007568 File Offset: 0x00005768
		public bool ObjectExists(ExternalItemPath objectName, out ItemType type, out Guid id, out int snapshotLimit, out byte[] secDesc)
		{
			int num;
			Guid guid;
			Guid guid2;
			return this.ObjectExists(objectName, out type, out id, out snapshotLimit, out secDesc, out num, out guid, out guid2);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007588 File Offset: 0x00005788
		public bool ObjectExists(ExternalItemPath objectName, out ItemType type, out Guid id, out int snapshotLimit, out byte[] secDesc, out int execOptions)
		{
			Guid guid;
			Guid guid2;
			return this.ObjectExists(objectName, out type, out id, out snapshotLimit, out secDesc, out execOptions, out guid, out guid2);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000075A8 File Offset: 0x000057A8
		public virtual bool ObjectExists(ExternalItemPath objectName, out ItemType type, out Guid id, out int snapshotLimit, out byte[] secDesc, out int execOptions, out Guid snapshotId, out Guid linkID)
		{
			bool flag;
			using (MonitoredScope.New("DBInterface.ObjectExists"))
			{
				flag = this.CatalogObjectExists(objectName.NativeCatalogItemPath, out type, out id, out snapshotLimit, out secDesc, out execOptions, out snapshotId, out linkID);
			}
			return flag;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000075F8 File Offset: 0x000057F8
		public bool CatalogObjectExists(CatalogItemPath objectName, out ItemType type, out Guid id, out int snapshotLimit, out byte[] secDesc, out int execOptions, out Guid snapshotId, out Guid linkID)
		{
			bool flag;
			using (MonitoredScope.New("DBInterface.CatalogObjectExists"))
			{
				execOptions = ExecutionOptions.Invalid;
				secDesc = null;
				type = ItemType.Unknown;
				id = Guid.Empty;
				snapshotLimit = -2;
				snapshotId = Guid.Empty;
				linkID = Guid.Empty;
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ObjectExists", null))
				{
					Storage.BindItemPathToCommand(objectName, instrumentedSqlCommand, true, this.UserContext);
					instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
					using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
					{
						if (!dataReader.Read())
						{
							flag = false;
						}
						else
						{
							type = (ItemType)dataReader.GetInt32(0);
							id = dataReader.GetGuid(1);
							if (!dataReader.IsDBNull(2))
							{
								snapshotLimit = dataReader.GetInt32(2);
							}
							if (!dataReader.IsDBNull(3))
							{
								secDesc = DataReaderHelper.ReadAllBytes(dataReader, 3);
							}
							execOptions = dataReader.GetInt32(4);
							if (!dataReader.IsDBNull(5))
							{
								snapshotId = dataReader.GetGuid(5);
							}
							if (!dataReader.IsDBNull(6))
							{
								linkID = dataReader.GetGuid(6);
							}
							flag = true;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000777C File Offset: 0x0000597C
		public virtual void SetAllProperties(Microsoft.ReportingServices.Library.CatalogItem item, ItemProperties objectProperties, string modifiedBy, DateTime modifiedDate)
		{
			string text2;
			bool flag;
			string text = ItemProperties.PrepareForSaving(objectProperties, out text2, out flag);
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetAllProperties", null))
			{
				Storage.BindItemPathToCommand(item.ItemContext.CatalogItemPath, instrumentedSqlCommand, true, null);
				instrumentedSqlCommand.AddParameter("@Property", SqlDbType.NText, text);
				instrumentedSqlCommand.AddParameter("@Description", SqlDbType.NText, text2);
				instrumentedSqlCommand.Parameters.AddWithValue("@Hidden", flag);
				instrumentedSqlCommand.Parameters.AddWithValue("@ModifiedBySid", Global.NameToSid(modifiedBy, this.UserContext.AuthenticationType)).SqlDbType = SqlDbType.VarBinary;
				instrumentedSqlCommand.AddParameter("@ModifiedByName", SqlDbType.NVarChar, modifiedBy);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				instrumentedSqlCommand.Parameters.AddWithValue("@ModifiedDate", modifiedDate);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000787C File Offset: 0x00005A7C
		public virtual bool GetObjectContent(ExternalItemPath objectName, out ItemType type, out byte[] objectContent, out Guid link, out string mimeType, out byte[] secDesc, out Guid itemID)
		{
			return this.CatalogGetObjectContent(objectName.NativeCatalogItemPath, out type, out objectContent, out link, out mimeType, out secDesc, out itemID);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00007894 File Offset: 0x00005A94
		public bool CatalogGetObjectContent(CatalogItemPath objectName, out ItemType type, out byte[] objectContent, out Guid link, out string mimeType, out byte[] secDesc, out Guid itemID)
		{
			type = ItemType.Unknown;
			objectContent = null;
			link = Guid.Empty;
			mimeType = null;
			secDesc = null;
			itemID = Guid.Empty;
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetObjectContent", null))
			{
				instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, objectName.Value);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						flag = false;
					}
					else
					{
						type = (ItemType)dataReader.GetInt32(0);
						if (!dataReader.IsDBNull(1))
						{
							objectContent = DataReaderHelper.ReadAllBytes(dataReader, 1);
						}
						if (!dataReader.IsDBNull(2))
						{
							link = dataReader.GetGuid(2);
						}
						if (!dataReader.IsDBNull(3))
						{
							mimeType = dataReader.GetString(3);
						}
						if (!dataReader.IsDBNull(4))
						{
							secDesc = DataReaderHelper.ReadAllBytes(dataReader, 4);
						}
						if (!dataReader.IsDBNull(5))
						{
							itemID = dataReader.GetGuid(5);
						}
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000079C4 File Offset: 0x00005BC4
		public bool GetSnapshotFromHistory(CatalogItemPath objectName, DateTime snapshotDate, out Guid reportId, out ItemType type, out ReportSnapshot snapshotData, out string description, out string propertiesXml, out byte[] secDesc)
		{
			type = ItemType.Unknown;
			reportId = Guid.Empty;
			snapshotData = null;
			description = null;
			secDesc = null;
			propertiesXml = null;
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetSnapshotFromHistory", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@Path", objectName.Value).SqlDbType = SqlDbType.NVarChar;
				instrumentedSqlCommand.Parameters.AddWithValue("@SnapshotDate", snapshotDate).SqlDbType = SqlDbType.DateTime;
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", this.UserContext.AuthenticationType).SqlDbType = SqlDbType.Int;
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						flag = false;
					}
					else
					{
						reportId = dataReader.GetGuid(0);
						type = (ItemType)dataReader.GetInt32(1);
						if (!dataReader.IsDBNull(2))
						{
							Guid guid = dataReader.GetGuid(2);
							bool flag2 = false;
							if (!dataReader.IsDBNull(3))
							{
								flag2 = dataReader.GetBoolean(3);
							}
							ReportProcessingFlags reportProcessingFlags = ReportProcessingFlags.NotSet;
							if (!dataReader.IsDBNull(7))
							{
								reportProcessingFlags = (ReportProcessingFlags)dataReader.GetInt32(7);
							}
							snapshotData = ReportSnapshot.Create(guid, true, flag2, reportProcessingFlags);
						}
						if (!dataReader.IsDBNull(4))
						{
							description = dataReader.GetString(4);
						}
						if (!dataReader.IsDBNull(5))
						{
							secDesc = DataReaderHelper.ReadAllBytes(dataReader, 5);
						}
						if (!dataReader.IsDBNull(6))
						{
							propertiesXml = dataReader.GetString(6);
						}
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007B58 File Offset: 0x00005D58
		public bool GetCompiledDefinition(CatalogItemPath objectName, out ItemType type, out ReportSnapshot compiledDefinition, out Guid link, out string properties, out string description, out byte[] secDesc, out Guid reportID, out int execOptions, out ReportSnapshot linkedCompiledDefinition, out string linkedProperties, out string linkedDescription, out Guid executionSnapshotID)
		{
			type = ItemType.Unknown;
			compiledDefinition = null;
			link = Guid.Empty;
			execOptions = ExecutionOptions.Invalid;
			properties = null;
			description = null;
			secDesc = null;
			reportID = Guid.Empty;
			linkedCompiledDefinition = null;
			bool flag = true;
			bool flag3;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetCompiledDefinition", null))
			{
				Storage.BindItemPathToCommand(objectName, instrumentedSqlCommand, true, this.UserContext);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType).SqlDbType = SqlDbType.NVarChar;
				Guid[] array = new Guid[]
				{
					Guid.Empty,
					Guid.Empty
				};
				ReportSnapshot[] array2 = new ReportSnapshot[2];
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					linkedProperties = null;
					linkedDescription = null;
					executionSnapshotID = Guid.Empty;
					if (!dataReader.Read())
					{
						return false;
					}
					type = (ItemType)dataReader.GetInt32(0);
					if (!dataReader.IsDBNull(1))
					{
						array[0] = dataReader.GetGuid(1);
					}
					if (!dataReader.IsDBNull(2))
					{
						link = dataReader.GetGuid(2);
					}
					if (!dataReader.IsDBNull(3))
					{
						properties = dataReader.GetString(3);
					}
					if (!dataReader.IsDBNull(4))
					{
						description = dataReader.GetString(4);
					}
					if (!dataReader.IsDBNull(5))
					{
						secDesc = DataReaderHelper.ReadAllBytes(dataReader, 5);
					}
					if (!dataReader.IsDBNull(6))
					{
						reportID = dataReader.GetGuid(6);
					}
					execOptions = dataReader.GetInt32(7);
					if (!dataReader.IsDBNull(8))
					{
						array[1] = dataReader.GetGuid(8);
					}
					if (!dataReader.IsDBNull(9))
					{
						linkedProperties = dataReader.GetString(9);
					}
					if (!dataReader.IsDBNull(10))
					{
						linkedDescription = dataReader.GetString(10);
					}
					if (!dataReader.IsDBNull(11))
					{
						executionSnapshotID = dataReader.GetGuid(11);
					}
					if (!dataReader.IsDBNull(12))
					{
						flag = dataReader.GetBoolean(12);
					}
				}
				for (int i = 0; i < array.Length; i++)
				{
					Guid guid = array[i];
					if (guid != Guid.Empty)
					{
						bool flag2 = i != 0 || flag;
						ReportProcessingFlags snapshotProcessingFlags = this.GetSnapshotProcessingFlags(guid, flag2);
						array2[i] = ReportSnapshot.Create(guid, flag2, false, snapshotProcessingFlags);
					}
				}
				compiledDefinition = array2[0];
				linkedCompiledDefinition = array2[1];
				flag3 = true;
			}
			return flag3;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00007DF8 File Offset: 0x00005FF8
		private string GetReportForExecutionQuery
		{
			get
			{
				if (DBInterface.s_getReportForExecutionQuery == null)
				{
					string text = base.Connection.Database + "TempDB";
					string text2 = this.ConnectionManager.EscapeAndBracketDBName(text);
					DBInterface.s_getReportForExecutionQuery = string.Format(CultureInfo.InvariantCulture, "\r\nDECLARE @OwnerID uniqueidentifier\r\nif(@EditSessionID is not null)\r\nBEGIN\r\n    EXEC GetUserID @OwnerSid, @OwnerName, @AuthType, @OwnerID OUTPUT\r\nEND\r\n\r\nDECLARE @now AS datetime\r\nSET @now = GETDATE()\r\n\r\nIF ( NOT EXISTS (\r\n    SELECT TOP 1 1\r\n        FROM\r\n            ExtendedCatalog(@OwnerID, @Path, @EditSessionID) AS C\r\n            INNER JOIN {0}.dbo.ExecutionCache AS EC ON C.ItemID = EC.ReportID\r\n\t\t\tINNER JOIN {0}.dbo.SnapshotData AS SN ON EC.SnapshotDataID = SN.SnapshotDataID AND EC.ParamsHash = SN.ParamsHash\r\n        WHERE\r\n            EC.AbsoluteExpiration > @now AND\r\n            EC.ParamsHash = @ParamsHash AND\r\n            SN.QueryParams LIKE @QueryParams\r\n   ) )\r\nBEGIN   -- no cache\r\n    SELECT\r\n        Cat.Type,\r\n        Cat.LinkSourceID,\r\n        Cat2.Path,\r\n        Cat.Property,\r\n        Cat.Description,\r\n        SecData.NtSecDescPrimary,\r\n        Cat.ItemID,\r\n        CAST (0 AS BIT), -- not found,\r\n        Cat.Intermediate,\r\n        Cat.ExecutionFlag,\r\n        SD.SnapshotDataID,\r\n        SD.DependsOnUser,\r\n        Cat.ExecutionTime,\r\n        (SELECT Schedule.NextRunTime\r\n         FROM\r\n             Schedule WITH (XLOCK)\r\n             INNER JOIN ReportSchedule ON Schedule.ScheduleID = ReportSchedule.ScheduleID\r\n         WHERE ReportSchedule.ReportID = Cat.ItemID AND ReportSchedule.ReportAction = 1), -- update snapshot\r\n        (SELECT Schedule.ScheduleID\r\n         FROM\r\n             Schedule\r\n             INNER JOIN ReportSchedule ON Schedule.ScheduleID = ReportSchedule.ScheduleID\r\n         WHERE ReportSchedule.ReportID = Cat.ItemID AND ReportSchedule.ReportAction = 1), -- update snapshot\r\n        (SELECT CachePolicy.ExpirationFlags FROM CachePolicy WHERE CachePolicy.ReportID = Cat.ItemID),\r\n        Cat2.Intermediate,\r\n        SD.ProcessingFlags,\r\n        Cat.IntermediateIsPermanent\r\n    FROM\r\n        ExtendedCatalog(@OwnerID, @Path, @EditSessionID) AS Cat\r\n        LEFT OUTER JOIN SecData ON Cat.PolicyID = SecData.PolicyID AND SecData.AuthType = @AuthType\r\n        LEFT OUTER JOIN Catalog AS Cat2 on Cat.LinkSourceID = Cat2.ItemID\r\n        LEFT OUTER JOIN SnapshotData AS SD ON Cat.SnapshotDataID = SD.SnapshotDataID\r\nEND\r\nELSE\r\nBEGIN   -- use cache\r\n    SELECT TOP 1\r\n        Cat.Type,\r\n        Cat.LinkSourceID,\r\n        Cat2.Path,\r\n        Cat.Property,\r\n        Cat.Description,\r\n        SecData.NtSecDescPrimary,\r\n        Cat.ItemID,\r\n        CAST (1 AS BIT), -- found,\r\n        SN.SnapshotDataID,\r\n        SN.DependsOnUser,\r\n        SN.EffectiveParams,  -- offset 10\r\n        SN.CreatedDate,\r\n        EC.AbsoluteExpiration,\r\n        (SELECT CachePolicy.ExpirationFlags FROM CachePolicy WHERE CachePolicy.ReportID = Cat.ItemID),\r\n        (SELECT Schedule.ScheduleID\r\n         FROM\r\n             Schedule WITH (XLOCK)\r\n             INNER JOIN ReportSchedule ON Schedule.ScheduleID = ReportSchedule.ScheduleID\r\n             WHERE ReportSchedule.ReportID = Cat.ItemID AND ReportSchedule.ReportAction = 1), -- update snapshot\r\n        SN.QueryParams,  -- offset 15\r\n        SN.ProcessingFlags,\r\n        Cat.IntermediateIsPermanent,\r\n        Cat.Intermediate\r\n    FROM\r\n        ExtendedCatalog(@OwnerID, @Path, @EditSessionID) AS Cat\r\n        INNER JOIN {0}.dbo.ExecutionCache AS EC ON Cat.ItemID = EC.ReportID\r\n        INNER JOIN {0}.dbo.SnapshotData AS SN ON EC.SnapshotDataID = SN.SnapshotDataID AND EC.ParamsHash = SN.ParamsHash\r\n        LEFT OUTER JOIN SecData ON Cat.PolicyID = SecData.PolicyID AND SecData.AuthType = @AuthType\r\n        LEFT OUTER JOIN Catalog AS Cat2 on Cat.LinkSourceID = Cat2.ItemID\r\n    WHERE\r\n        AbsoluteExpiration > @now AND\r\n        SN.ParamsHash = @ParamsHash AND\r\n        SN.QueryParams LIKE @QueryParams\r\n    ORDER BY SN.CreatedDate DESC\r\nEND", text2);
				}
				return DBInterface.s_getReportForExecutionQuery;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00007E4C File Offset: 0x0000604C
		public bool GetReportForExecution(CatalogItemPath objectName, string queryParametersXml, out bool foundInCache, out ItemType type, out ReportSnapshot intermediateSnapshot, out ReportSnapshot snapshotData, out Guid link, out string linkPath, out string properties, out string description, out byte[] secDesc, out Guid reportID, out int execOptions, out DateTime executionDateTime, out bool hasData, out bool cachingRequested, out DateTime expirationDateTime)
		{
			bool flag = false;
			bool flag2 = this.GetReportForExecution(objectName, queryParametersXml, out foundInCache, out type, out intermediateSnapshot, out snapshotData, out link, out linkPath, out properties, out description, out secDesc, out reportID, out execOptions, out executionDateTime, out hasData, out cachingRequested, out expirationDateTime, ref flag);
			if (!flag2 && flag)
			{
				flag2 = this.GetReportForExecution(objectName, queryParametersXml, out foundInCache, out type, out intermediateSnapshot, out snapshotData, out link, out linkPath, out properties, out description, out secDesc, out reportID, out execOptions, out executionDateTime, out hasData, out cachingRequested, out expirationDateTime, ref flag);
			}
			return flag2;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00007EB4 File Offset: 0x000060B4
		private bool GetReportForExecution(CatalogItemPath objectName, string queryParametersXml, out bool foundInCache, out ItemType type, out ReportSnapshot intermediateSnapshot, out ReportSnapshot snapshotData, out Guid link, out string linkPath, out string properties, out string description, out byte[] secDesc, out Guid reportID, out int execOptions, out DateTime executionDateTime, out bool hasData, out bool cachingRequested, out DateTime expirationDateTime, ref bool hashCollision)
		{
			int num = 0;
			Guid guid = Guid.Empty;
			execOptions = ExecutionOptions.Live;
			foundInCache = false;
			type = ItemType.Unknown;
			intermediateSnapshot = null;
			snapshotData = null;
			link = Guid.Empty;
			linkPath = null;
			properties = null;
			description = null;
			secDesc = null;
			reportID = Guid.Empty;
			executionDateTime = DateTime.MinValue;
			expirationDateTime = DateTime.MinValue;
			hasData = false;
			cachingRequested = false;
			Guid guid2 = Guid.Empty;
			bool flag = true;
			InstrumentedSqlCommand instrumentedSqlCommand;
			if (!hashCollision)
			{
				instrumentedSqlCommand = this.NewStandardSqlCommand("GetReportForExecution", null);
			}
			else
			{
				instrumentedSqlCommand = base.NewStandardSqlCommandQuery(this.GetReportForExecutionQuery);
			}
			using (instrumentedSqlCommand)
			{
				Storage.BindItemPathToCommand(objectName, instrumentedSqlCommand, true, this.UserContext);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", this.UserContext.AuthenticationType).SqlDbType = SqlDbType.Int;
				instrumentedSqlCommand.Parameters.AddWithValue("@ParamsHash", StackTraceUtil.GetInvariantHashCode<char>(queryParametersXml)).SqlDbType = SqlDbType.Int;
				if (hashCollision)
				{
					instrumentedSqlCommand.AddParameter("@QueryParams", SqlDbType.NText, queryParametersXml);
					if (!instrumentedSqlCommand.Parameters.Contains("@EditSessionID"))
					{
						instrumentedSqlCommand.AddParameter("@EditSessionID", SqlDbType.VarChar, DBNull.Value);
					}
					if (!instrumentedSqlCommand.Parameters.Contains("@OwnerSid"))
					{
						instrumentedSqlCommand.AddParameter("@OwnerSid", SqlDbType.VarBinary, DBNull.Value);
					}
					if (!instrumentedSqlCommand.Parameters.Contains("@OwnerName"))
					{
						instrumentedSqlCommand.AddParameter("@OwnerName", SqlDbType.NVarChar, DBNull.Value);
					}
				}
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						return false;
					}
					type = (ItemType)dataReader.GetInt32(0);
					if (!dataReader.IsDBNull(1))
					{
						link = dataReader.GetGuid(1);
					}
					if (!dataReader.IsDBNull(2))
					{
						linkPath = dataReader.GetString(2);
					}
					if (!dataReader.IsDBNull(3))
					{
						properties = dataReader.GetString(3);
					}
					if (!dataReader.IsDBNull(4))
					{
						description = dataReader.GetString(4);
					}
					if (!dataReader.IsDBNull(5))
					{
						secDesc = DataReaderHelper.ReadAllBytes(dataReader, 5);
					}
					if (!dataReader.IsDBNull(6))
					{
						reportID = dataReader.GetGuid(6);
					}
					if (!dataReader.IsDBNull(7))
					{
						foundInCache = dataReader.GetBoolean(7);
					}
					if (foundInCache)
					{
						if (!dataReader.IsDBNull(8))
						{
							Guid guid3 = dataReader.GetGuid(8);
							bool flag2 = false;
							if (!dataReader.IsDBNull(9))
							{
								flag2 = dataReader.GetBoolean(9);
							}
							ReportProcessingFlags reportProcessingFlags = ReportProcessingFlags.NotSet;
							if (!dataReader.IsDBNull(16))
							{
								reportProcessingFlags = (ReportProcessingFlags)dataReader.GetInt32(16);
							}
							snapshotData = ReportSnapshot.Create(guid3, false, flag2, reportProcessingFlags);
						}
						cachingRequested = true;
						hasData = true;
						dataReader.GetString(10);
						executionDateTime = dataReader.GetDateTime(11);
						if (!dataReader.IsDBNull(12))
						{
							expirationDateTime = dataReader.GetDateTime(12);
						}
						if (!dataReader.IsDBNull(13))
						{
							num = dataReader.GetInt32(13);
						}
						if (!dataReader.IsDBNull(14))
						{
							guid = dataReader.GetGuid(14);
						}
						string text = null;
						if (!dataReader.IsDBNull(15))
						{
							text = dataReader.GetString(15);
						}
						if (text != queryParametersXml)
						{
							if (!hashCollision)
							{
								hashCollision = true;
								return false;
							}
							throw new InternalCatalogException("Different query parameters resulted in a same hash. This is not supported.");
						}
						else
						{
							if (num != 2 && expirationDateTime == DateTime.MinValue)
							{
								throw new InternalCatalogException("No end time found for expiring cache on minutes");
							}
							if (!dataReader.IsDBNull(17))
							{
								flag = dataReader.GetBoolean(17);
							}
							if (!dataReader.IsDBNull(18) && objectName.IsEditSession)
							{
								guid2 = dataReader.GetGuid(18);
							}
						}
					}
					else
					{
						if (!dataReader.IsDBNull(8))
						{
							guid2 = dataReader.GetGuid(8);
						}
						if (!dataReader.IsDBNull(18))
						{
							flag = dataReader.GetBoolean(18);
						}
						execOptions = dataReader.GetInt32(9);
						if (ExecutionOptions.IsSnapshotExecution(execOptions))
						{
							if (!dataReader.IsDBNull(10))
							{
								Guid guid4 = dataReader.GetGuid(10);
								bool flag3 = false;
								if (!dataReader.IsDBNull(11))
								{
									flag3 = dataReader.GetBoolean(11);
								}
								ReportProcessingFlags reportProcessingFlags2 = ReportProcessingFlags.NotSet;
								if (!dataReader.IsDBNull(17))
								{
									reportProcessingFlags2 = (ReportProcessingFlags)dataReader.GetInt32(17);
								}
								snapshotData = ReportSnapshot.Create(guid4, true, flag3, reportProcessingFlags2);
								intermediateSnapshot = null;
							}
							else
							{
								snapshotData = null;
								intermediateSnapshot = null;
							}
							hasData = true;
							if (!dataReader.IsDBNull(12))
							{
								executionDateTime = dataReader.GetDateTime(12);
							}
							if (!dataReader.IsDBNull(13))
							{
								expirationDateTime = dataReader.GetDateTime(13);
							}
							if (!dataReader.IsDBNull(14))
							{
								guid = dataReader.GetGuid(14);
							}
						}
						if (!dataReader.IsDBNull(15))
						{
							cachingRequested = true;
							num = dataReader.GetInt32(15);
						}
						if (link != Guid.Empty && ExecutionOptions.IsLiveExecution(execOptions))
						{
							Global.m_Tracer.Assert(guid2 == Guid.Empty);
							Global.m_Tracer.Assert(intermediateSnapshot == null && !dataReader.IsDBNull(16));
							guid2 = dataReader.GetGuid(16);
						}
					}
				}
			}
			if (guid2 != Guid.Empty)
			{
				ReportProcessingFlags snapshotProcessingFlags = this.GetSnapshotProcessingFlags(guid2, flag);
				intermediateSnapshot = ReportSnapshot.Create(guid2, flag, false, snapshotProcessingFlags);
			}
			if (guid != Guid.Empty)
			{
				DateTime dateTime = this.m_scheduleDB.CheckNextRunTime(guid, expirationDateTime);
				if (expirationDateTime == DateTime.MinValue)
				{
					expirationDateTime = dateTime;
				}
			}
			return true;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00008478 File Offset: 0x00006678
		public bool GetDataSetForExecution(Guid ItemId, string queryParametersXml, out bool foundInCache, out ReportSnapshot snapshotData, out bool cachingRequested, out ItemProperties properties)
		{
			foundInCache = false;
			snapshotData = null;
			cachingRequested = false;
			properties = null;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetDataSetForExecution", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", ItemId).SqlDbType = SqlDbType.UniqueIdentifier;
				instrumentedSqlCommand.Parameters.AddWithValue("@ParamsHash", StackTraceUtil.GetInvariantHashCode<char>(queryParametersXml)).SqlDbType = SqlDbType.Int;
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						return false;
					}
					if (!dataReader.IsDBNull(0))
					{
						foundInCache = true;
						Guid guid = dataReader.GetGuid(0);
						snapshotData = ReportSnapshot.Create(guid, false, false, ReportProcessingFlags.OnDemandEngine);
						dataReader.GetString(1);
						if (dataReader.GetString(2) != queryParametersXml)
						{
							throw new InternalCatalogException("Different query parameters resulted in a same hash. This is not supported.");
						}
					}
					if (!dataReader.IsDBNull(3))
					{
						cachingRequested = true;
					}
					if (!dataReader.IsDBNull(4))
					{
						properties = new ItemProperties(dataReader.GetString(4));
					}
				}
			}
			return true;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00008590 File Offset: 0x00006790
		public bool GetReportParametersForExecution(CatalogItemPath itemPath, DateTime historyID, out Guid reportID, out ItemType itemType, out int executionOption, out byte[] secDescr, out string savedParametersXml, out ReportSnapshot compiledDefinition, out ReportSnapshot snapshotData, out Guid linkID, out DateTime executionDateTime)
		{
			reportID = Guid.Empty;
			executionOption = 0;
			itemType = ItemType.Unknown;
			secDescr = null;
			savedParametersXml = null;
			compiledDefinition = null;
			snapshotData = null;
			Guid guid = Guid.Empty;
			Guid guid2 = Guid.Empty;
			Guid guid3 = Guid.Empty;
			Guid guid4 = Guid.Empty;
			linkID = Guid.Empty;
			executionDateTime = DateTime.MinValue;
			bool flag = true;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetReportParametersForExecution", null))
			{
				Storage.BindItemPathToCommand(itemPath, instrumentedSqlCommand, true, this.UserContext);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				if (historyID != DateTime.MinValue)
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@HistoryID", historyID);
				}
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						return false;
					}
					if (!dataReader.IsDBNull(0))
					{
						reportID = dataReader.GetGuid(0);
					}
					if (!dataReader.IsDBNull(1))
					{
						itemType = (ItemType)dataReader.GetInt32(1);
					}
					if (!dataReader.IsDBNull(2))
					{
						executionOption = dataReader.GetInt32(2);
					}
					if (!dataReader.IsDBNull(3))
					{
						secDescr = DataReaderHelper.ReadAllBytes(dataReader, 3);
					}
					if (!dataReader.IsDBNull(4))
					{
						savedParametersXml = dataReader.GetString(4);
					}
					if (!dataReader.IsDBNull(5))
					{
						guid = dataReader.GetGuid(5);
					}
					if (!dataReader.IsDBNull(6))
					{
						guid2 = dataReader.GetGuid(6);
					}
					if (!dataReader.IsDBNull(7))
					{
						guid3 = dataReader.GetGuid(7);
					}
					if (!dataReader.IsDBNull(8))
					{
						guid4 = dataReader.GetGuid(8);
					}
					if (!dataReader.IsDBNull(9))
					{
						linkID = dataReader.GetGuid(9);
					}
					if (!dataReader.IsDBNull(10))
					{
						executionDateTime = dataReader.GetDateTime(10);
					}
					if (!dataReader.IsDBNull(11))
					{
						flag = dataReader.GetBoolean(11);
					}
				}
			}
			if (itemType != ItemType.LinkedReport && itemType != ItemType.Report && itemType != ItemType.DataSet)
			{
				return true;
			}
			if (itemType == ItemType.LinkedReport)
			{
				if (guid4 != Guid.Empty)
				{
					ReportProcessingFlags reportProcessingFlags = this.GetSnapshotProcessingFlags(guid4, true);
					compiledDefinition = ReportSnapshot.Create(guid4, true, false, reportProcessingFlags);
				}
			}
			else
			{
				ReportProcessingFlags reportProcessingFlags = this.GetSnapshotProcessingFlags(guid, flag);
				compiledDefinition = ReportSnapshot.Create(guid, flag, false, reportProcessingFlags);
			}
			if (historyID != DateTime.MinValue)
			{
				ReportProcessingFlags reportProcessingFlags;
				if (guid3 == Guid.Empty)
				{
					reportProcessingFlags = ReportProcessingFlags.NotSet;
				}
				else
				{
					reportProcessingFlags = this.GetSnapshotProcessingFlags(guid3, true);
				}
				snapshotData = ReportSnapshot.Create(guid3, true, false, reportProcessingFlags);
				executionDateTime = historyID.ToLocalTime();
				executionOption = ExecutionOptions.Snapshot;
			}
			else
			{
				if (ExecutionOptions.IsSnapshotExecution(executionOption))
				{
					if (guid2 != Guid.Empty)
					{
						ReportProcessingFlags reportProcessingFlags = this.GetSnapshotProcessingFlags(guid2, true);
						snapshotData = ReportSnapshot.Create(guid2, true, false, reportProcessingFlags);
						if (executionDateTime == DateTime.MinValue)
						{
							throw new InternalCatalogException("expected valid execution time for snapshot");
						}
					}
					else
					{
						RSTrace.CatalogTrace.Assert(executionDateTime == DateTime.MinValue, "found executionDateTime for snapshot, but no snapshotId");
					}
					return true;
				}
				if (executionDateTime != DateTime.MinValue)
				{
					RSTrace.CatalogTrace.Assert(snapshotData != null, "executionDateTime found, but no associated snapshotData");
				}
			}
			return true;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00008908 File Offset: 0x00006B08
		public virtual bool GetAllProperties(ExternalItemPath objectName, out ItemProperties properties, out Guid id, out Guid linkID, out ItemType type, out byte[] secDesc, out int executionOptions, out int snapshotLimit, out string subType)
		{
			return this.CatalogGetAllProperties(objectName.NativeCatalogItemPath, out properties, out id, out linkID, out type, out secDesc, out executionOptions, out snapshotLimit, out subType);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000892F File Offset: 0x00006B2F
		public virtual bool CatalogGetAllProperties(ExternalItemPath objectName, out ItemProperties properties)
		{
			return this.CatalogGetAllProperties(objectName.NativeCatalogItemPath, out properties);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00008940 File Offset: 0x00006B40
		protected bool CatalogGetAllProperties(CatalogItemPath objectName, out ItemProperties properties)
		{
			Guid guid;
			Guid guid2;
			ItemType itemType;
			byte[] array;
			int num;
			int num2;
			string text;
			return this.CatalogGetAllProperties(objectName, out properties, out guid, out guid2, out itemType, out array, out num, out num2, out text);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00008964 File Offset: 0x00006B64
		protected bool CatalogGetAllProperties(CatalogItemPath objectName, out ItemProperties properties, out Guid id, out Guid linkID, out ItemType type, out byte[] secDesc, out int executionOptions, out int snapshotLimit, out string subType)
		{
			bool flag2;
			using (MonitoredScope.New("DBInterface.CatalogGetAllProperties"))
			{
				properties = null;
				string text = null;
				string text2 = null;
				string text3 = null;
				type = ItemType.Unknown;
				int num = 0;
				id = Guid.Empty;
				DateTime dateTime = DateTime.MinValue;
				DateTime dateTime2 = DateTime.MinValue;
				string text4 = null;
				secDesc = null;
				DateTime dateTime3 = DateTime.MinValue;
				linkID = Guid.Empty;
				bool flag = false;
				executionOptions = 0;
				snapshotLimit = -2;
				subType = null;
				Guid? guid = null;
				Guid? guid2 = new Guid?(Guid.Empty);
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetAllProperties", null))
				{
					Storage.BindItemPathToCommand(objectName, instrumentedSqlCommand, true, this.UserContext);
					instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
					using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
					{
						if (!dataReader.Read())
						{
							flag2 = false;
						}
						else
						{
							if (!dataReader.IsDBNull(0))
							{
								text = dataReader.GetString(0);
							}
							if (!dataReader.IsDBNull(1))
							{
								text3 = dataReader.GetString(1);
							}
							type = (ItemType)dataReader.GetInt32(2);
							if (!dataReader.IsDBNull(3))
							{
								num = DBInterface.ReadInt64AsInt32(dataReader, 3);
							}
							id = dataReader.GetGuid(4);
							string userNameBySid = UserUtil.GetUserNameBySid(dataReader, 5, 6);
							dateTime = dataReader.GetDateTime(7);
							string userNameBySid2 = UserUtil.GetUserNameBySid(dataReader, 8, 9);
							dateTime2 = dataReader.GetDateTime(10);
							string text5 = null;
							if (!dataReader.IsDBNull(11))
							{
								text4 = dataReader.GetString(11);
							}
							if (!dataReader.IsDBNull(12))
							{
								dateTime3 = dataReader.GetDateTime(12);
							}
							if (!dataReader.IsDBNull(13))
							{
								secDesc = DataReaderHelper.ReadAllBytes(dataReader, 13);
							}
							if (!dataReader.IsDBNull(14))
							{
								linkID = dataReader.GetGuid(14);
							}
							if (!dataReader.IsDBNull(15))
							{
								flag = dataReader.GetBoolean(15);
							}
							if (!dataReader.IsDBNull(16))
							{
								executionOptions = dataReader.GetInt32(16);
							}
							if (!dataReader.IsDBNull(17))
							{
								snapshotLimit = dataReader.GetInt32(17);
							}
							if (!dataReader.IsDBNull(18))
							{
								text5 = dataReader.GetString(18);
							}
							if (!dataReader.IsDBNull(19))
							{
								subType = dataReader.GetString(19);
							}
							if (!dataReader.IsDBNull(20))
							{
								guid = new Guid?(dataReader.GetGuid(20));
							}
							if (!dataReader.IsDBNull(21))
							{
								guid2 = new Guid?(dataReader.GetGuid(21));
							}
							if (!dataReader.IsDBNull(22))
							{
								text2 = dataReader.GetString(22);
							}
							properties = new ItemProperties(text);
							properties = this.OverrideWithLinkedItemProperties(properties, text2);
							properties.AddStoredProperties(text5, type, num, id, userNameBySid, dateTime, userNameBySid2, dateTime2, text4, text3, dateTime3, flag, linkID, guid, subType, guid2);
							flag2 = true;
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00008C98 File Offset: 0x00006E98
		private ItemProperties OverrideWithLinkedItemProperties(ItemProperties properties, string propertiesToOverride)
		{
			if (!string.IsNullOrEmpty(propertiesToOverride))
			{
				ItemProperties itemProperties = new ItemProperties(propertiesToOverride);
				for (int i = 0; i < itemProperties.Count; i++)
				{
					string name = itemProperties.GetName(i);
					if (!ReportPageProperties.IsLayout(name))
					{
						properties[name] = itemProperties[name];
					}
				}
			}
			return properties;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00008CE4 File Offset: 0x00006EE4
		public bool GetParameters(CatalogItemPath objectName, out ItemType type, out string parameters, out Guid id, out byte[] secDesc)
		{
			Guid guid;
			int num;
			return this.GetParameters(objectName, out type, out parameters, out id, out secDesc, out guid, out num);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008D04 File Offset: 0x00006F04
		public bool GetParameters(CatalogItemPath objectName, out ItemType type, out string parameters, out Guid id, out byte[] secDesc, out Guid linkID, out int executionOptions)
		{
			type = ItemType.Unknown;
			parameters = null;
			id = Guid.Empty;
			secDesc = null;
			linkID = Guid.Empty;
			executionOptions = 0;
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetParameters", null))
			{
				instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, objectName.Value);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						flag = false;
					}
					else
					{
						type = (ItemType)dataReader.GetInt32(0);
						if (!dataReader.IsDBNull(1))
						{
							parameters = dataReader.GetString(1);
						}
						if (!dataReader.IsDBNull(2))
						{
							id = dataReader.GetGuid(2);
						}
						if (!dataReader.IsDBNull(3))
						{
							secDesc = DataReaderHelper.ReadAllBytes(dataReader, 3);
						}
						if (!dataReader.IsDBNull(4))
						{
							linkID = dataReader.GetGuid(4);
						}
						if (!dataReader.IsDBNull(5))
						{
							executionOptions = dataReader.GetInt32(5);
						}
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00008E34 File Offset: 0x00007034
		public void SetParameters(CatalogItemPath objectName, string parameters)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetParameters", null))
			{
				instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, objectName.Value);
				instrumentedSqlCommand.AddParameter("@Parameter", SqlDbType.NText, parameters);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008E94 File Offset: 0x00007094
		public string GetParametersById(Guid objectID)
		{
			CatalogItemPath pathById = this.GetPathById(objectID);
			ItemType itemType;
			string text;
			Guid guid;
			byte[] array;
			this.GetParameters(pathById, out itemType, out text, out guid, out array);
			return text;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00008EBC File Offset: 0x000070BC
		public void SetParametersById(Guid objectID, string parameters)
		{
			CatalogItemPath pathById = this.GetPathById(objectID);
			this.SetParameters(pathById, parameters);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00008EDC File Offset: 0x000070DC
		public void SetLastModified(CatalogItemPath Item, string modifiedBy, DateTime modifiedDate)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetLastModified", null))
			{
				instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, Item.Value);
				instrumentedSqlCommand.Parameters.AddWithValue("@ModifiedBySid", Global.NameToSid(modifiedBy, this.UserContext.AuthenticationType)).SqlDbType = SqlDbType.VarBinary;
				instrumentedSqlCommand.AddParameter("@ModifiedByName", SqlDbType.NVarChar, modifiedBy);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				instrumentedSqlCommand.Parameters.AddWithValue("@ModifiedDate", modifiedDate);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00008F9C File Offset: 0x0000719C
		public CatalogItemPath GetPathById(Guid id)
		{
			CatalogItemPath catalogItemPath;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetNameById", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", id);
				string text = (string)instrumentedSqlCommand.ExecuteScalar();
				if (text != null)
				{
					catalogItemPath = new CatalogItemPath(text);
				}
				else
				{
					catalogItemPath = null;
				}
			}
			return catalogItemPath;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public virtual void CleanupCatalog()
		{
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00009004 File Offset: 0x00007204
		public Guid CreateEditSession(CatalogItemPath itemPath, string editSessionID, string name, byte[] content, string description, Guid intermediateID, string propertyAsXml, string parameterAsXml, byte[] dataCacheHash)
		{
			RSTrace.CatalogTrace.Assert(itemPath != null && !string.IsNullOrEmpty(itemPath.Value), "itemPath");
			RSTrace.CatalogTrace.Assert(!string.IsNullOrEmpty(editSessionID), "editSessionID");
			Guid guid;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CreateEditSession", null))
			{
				instrumentedSqlCommand.Parameters.Add("@EditSessionID", SqlDbType.VarChar, 32).Value = editSessionID;
				instrumentedSqlCommand.Parameters.Add("@ContextPath", SqlDbType.NVarChar, 440).Value = itemPath.Value;
				instrumentedSqlCommand.Parameters.Add("@OwnerSid", SqlDbType.VarBinary, 85).Value = Global.NameToSid(this.UserContext);
				instrumentedSqlCommand.Parameters.Add("@OwnerName", SqlDbType.NVarChar, 260).Value = this.UserContext.UserName;
				instrumentedSqlCommand.Parameters.Add("@AuthType", SqlDbType.Int).Value = (int)this.UserContext.AuthenticationType;
				instrumentedSqlCommand.Parameters.Add("@Timeout", SqlDbType.Int).Value = Global.EditSessionTimeoutMinutes;
				instrumentedSqlCommand.Parameters.Add("@Content", SqlDbType.Image).Value = content;
				instrumentedSqlCommand.Parameters.Add("@Description", SqlDbType.NText).Value = description;
				instrumentedSqlCommand.Parameters.Add("@Property", SqlDbType.NText).Value = propertyAsXml;
				instrumentedSqlCommand.Parameters.Add("@Parameter", SqlDbType.NText).Value = parameterAsXml;
				instrumentedSqlCommand.Parameters.Add("@Intermediate", SqlDbType.UniqueIdentifier).Value = intermediateID;
				instrumentedSqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 440).Value = name;
				instrumentedSqlCommand.Parameters.Add("@DataCacheHash", SqlDbType.VarBinary).Value = dataCacheHash;
				SqlParameter sqlParameter = instrumentedSqlCommand.Parameters.Add("@NewItemID", SqlDbType.UniqueIdentifier);
				sqlParameter.Direction = ParameterDirection.Output;
				instrumentedSqlCommand.ExecuteNonQuery();
				guid = (Guid)sqlParameter.Value;
			}
			return guid;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00009234 File Offset: 0x00007434
		public Comment InsertComment(Comment comment)
		{
			Comment comment2;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("InsertComment", null))
			{
				instrumentedSqlCommand.Parameters.Add("@UserSid", SqlDbType.VarBinary, 85).Value = Global.NameToSid(this.UserContext);
				instrumentedSqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 260).Value = this.UserContext.UserName;
				instrumentedSqlCommand.Parameters.Add("@AuthType", SqlDbType.Int).Value = this.UserContext.AuthenticationType;
				instrumentedSqlCommand.Parameters.Add("@ItemID", SqlDbType.UniqueIdentifier).Value = comment.ItemId;
				instrumentedSqlCommand.Parameters.Add("@Text", SqlDbType.NVarChar, 2048).Value = comment.Text;
				if (comment.ThreadId != null)
				{
					instrumentedSqlCommand.Parameters.Add("@ThreadID", SqlDbType.BigInt).Value = comment.ThreadId;
				}
				if (comment.AttachmentPath != null)
				{
					instrumentedSqlCommand.Parameters.Add("@AttachmentPath", SqlDbType.NVarChar, 425).Value = comment.AttachmentPath;
				}
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader(CommandBehavior.SingleRow))
				{
					if (!dataReader.Read())
					{
						comment2 = null;
					}
					else
					{
						int ordinal = dataReader.GetOrdinal("UserName");
						Comment comment3 = new Comment
						{
							Id = dataReader.GetInt64(dataReader.GetOrdinal("CommentID")),
							ItemId = dataReader.GetGuid(dataReader.GetOrdinal("ItemID")),
							UserName = UserUtil.GetUserNameBySid(dataReader, ordinal, ordinal),
							Text = dataReader.GetString(dataReader.GetOrdinal("Text")),
							CreatedDate = dataReader.GetDateTime(dataReader.GetOrdinal("CreatedDate"))
						};
						int ordinal2 = dataReader.GetOrdinal("AttachmentPath");
						if (!dataReader.IsDBNull(ordinal2))
						{
							comment3.AttachmentPath = dataReader.GetString(ordinal2);
						}
						int ordinal3 = dataReader.GetOrdinal("ModifiedDate");
						if (!dataReader.IsDBNull(ordinal3))
						{
							comment3.ModifiedDate = new DateTime?(dataReader.GetDateTime(ordinal3));
						}
						int ordinal4 = dataReader.GetOrdinal("ThreadID");
						if (!dataReader.IsDBNull(ordinal4))
						{
							comment3.ThreadId = new long?(dataReader.GetInt64(ordinal4));
						}
						comment2 = comment3;
					}
				}
			}
			return comment2;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000094C0 File Offset: 0x000076C0
		public bool UpdateComment(Comment comment)
		{
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("UpdateComment", null))
			{
				instrumentedSqlCommand.Parameters.Add("@Text", SqlDbType.NVarChar, 2048).Value = comment.Text;
				instrumentedSqlCommand.Parameters.Add("@CommentID", SqlDbType.BigInt).Value = comment.Id;
				int num = instrumentedSqlCommand.ExecuteNonQuery();
				if (num != 0)
				{
					if (num != 1)
					{
						throw new InternalCatalogException("Update comment affected more than 1 row.");
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000955C File Offset: 0x0000775C
		public bool IsUserContextOwner(long id)
		{
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CommentBelongsToUser", null))
			{
				instrumentedSqlCommand.Parameters.Add("@CommentID", SqlDbType.BigInt).Value = id;
				instrumentedSqlCommand.Parameters.Add("@UserSid", SqlDbType.VarBinary, 85).Value = Global.NameToSid(this.UserContext);
				instrumentedSqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 260).Value = this.UserContext.UserName;
				instrumentedSqlCommand.Parameters.Add("@AuthType", SqlDbType.Int).Value = (int)this.UserContext.AuthenticationType;
				int num = Convert.ToInt32(instrumentedSqlCommand.ExecuteScalar());
				if (num != 0)
				{
					if (num != 1)
					{
						throw new InternalCatalogException("CommentBelongsToUser returned something not 0 or 1.");
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00009648 File Offset: 0x00007848
		public void AddBatchRecord(Guid batchId, string userName, CatalogCommand action, string item, string itemParameterName, string parent, string parentParameterName, string param, string paramParameterName, bool boolParam, byte[] content, string properties)
		{
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("AddBatchRecord", null))
				{
					instrumentedSqlCommand.Parameters.Add("@BatchID", SqlDbType.UniqueIdentifier).Value = batchId;
					instrumentedSqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 260).Value = userName;
					instrumentedSqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 32).Value = action.ToString();
					if (item != null)
					{
						if (item.Length > CatalogItemNameUtility.MaxItemPathLength)
						{
							throw new InvalidParameterException(itemParameterName);
						}
						instrumentedSqlCommand.Parameters.Add("@Item", SqlDbType.NVarChar, 425).Value = item;
					}
					if (parent != null)
					{
						if (parent.Length > CatalogItemNameUtility.MaxItemPathLength)
						{
							throw new InvalidParameterException(parentParameterName);
						}
						instrumentedSqlCommand.Parameters.Add("@Parent", SqlDbType.NVarChar, 425).Value = parent;
					}
					if (param != null)
					{
						if (param.Length > CatalogItemNameUtility.MaxItemPathLength)
						{
							throw new InvalidParameterException(paramParameterName);
						}
						instrumentedSqlCommand.Parameters.Add("@Param", SqlDbType.NVarChar, 425).Value = param;
					}
					instrumentedSqlCommand.Parameters.Add("@BoolParam", SqlDbType.Bit).Value = boolParam;
					if (content != null)
					{
						instrumentedSqlCommand.Parameters.Add("@Content", SqlDbType.Image).Value = content;
					}
					if (properties != null)
					{
						instrumentedSqlCommand.Parameters.Add("@Properties", SqlDbType.NText).Value = properties;
					}
					instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			catch (ReportServerStorageException ex)
			{
				if (ex.IsSqlException && ex.SqlErrorNumber == Native.SqlAdHocErrorCode)
				{
					throw new BatchNotFoundException(batchId.ToString());
				}
				throw;
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00009834 File Offset: 0x00007A34
		public ArrayList GetBatchRecords(Guid batchId)
		{
			ArrayList arrayList2;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetBatchRecords", null))
			{
				instrumentedSqlCommand.Parameters.Add("@BatchID", SqlDbType.UniqueIdentifier).Value = batchId;
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					ArrayList arrayList = new ArrayList();
					while (dataReader.Read())
					{
						CallParameters callParameters = new CallParameters();
						callParameters.Action = dataReader.GetString(0);
						if (!dataReader.IsDBNull(1))
						{
							callParameters.Item = dataReader.GetString(1);
						}
						else
						{
							callParameters.Item = null;
						}
						if (!dataReader.IsDBNull(2))
						{
							callParameters.Parent = dataReader.GetString(2);
						}
						else
						{
							callParameters.Parent = null;
						}
						if (!dataReader.IsDBNull(3))
						{
							callParameters.Param = dataReader.GetString(3);
						}
						else
						{
							callParameters.Param = null;
						}
						callParameters.BoolParam = dataReader.GetBoolean(4);
						if (!dataReader.IsDBNull(5))
						{
							callParameters.Content = DataReaderHelper.ReadAllBytes(dataReader, 5);
						}
						else
						{
							callParameters.Content = null;
						}
						if (!dataReader.IsDBNull(6))
						{
							callParameters.Properties = dataReader.GetString(6);
						}
						else
						{
							callParameters.Properties = null;
						}
						arrayList.Add(callParameters);
					}
					if (arrayList.Count > 0)
					{
						arrayList2 = arrayList;
					}
					else
					{
						arrayList2 = null;
					}
				}
			}
			return arrayList2;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000099A8 File Offset: 0x00007BA8
		public bool DeleteBatchRecords(Guid batchId)
		{
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteBatchRecords", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@BatchID", batchId);
				flag = instrumentedSqlCommand.ExecuteNonQuery() > 0;
			}
			return flag;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00009A00 File Offset: 0x00007C00
		public int CleanBatchRecords()
		{
			int num;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CleanBatchRecords", null))
				{
					instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCleanupTimeoutSeconds;
					instrumentedSqlCommand.Parameters.AddWithValue("@MaxAgeMinutes", Globals.Configuration.CleanupCycleMinutes);
					num = instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in CleanBatchRecords: {0}", new object[] { ex });
				if (!(ex is RSException))
				{
					throw;
				}
				num = 0;
			}
			return num;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00009AA0 File Offset: 0x00007CA0
		public int CleanOrphanedPolicies()
		{
			int num;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CleanOrphanedPolicies", null))
				{
					instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCleanupTimeoutSeconds;
					num = instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in CleanOrphanedPolicies: {0}", new object[] { ex });
				if (!(ex is RSException))
				{
					throw;
				}
				num = 0;
			}
			return num;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009B20 File Offset: 0x00007D20
		public void UpdateUsernameFromSID()
		{
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("UpdateUsernameFromSID", null))
				{
					instrumentedSqlCommand.CommandTimeout = Globals.Configuration.UserNameSIDRefreshSQLTimeout;
					instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			catch (RSException ex)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in UpdateUsernameFromSID: {0}", new object[] { ex });
			}
			catch (Exception ex2)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in UpdateUsernameFromSID: {0}", new object[] { ex2 });
				throw;
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009BC4 File Offset: 0x00007DC4
		public void CreateOrUpdateContentCache(Guid catalogItemID, int paramsHash, string effectiveParams, DBInterface.ContentCacheTypes contentType, int version, Stream content)
		{
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CreateOrUpdateContentCache", null))
				{
					instrumentedSqlCommand.Parameters.Add("@CatalogItemID", SqlDbType.UniqueIdentifier).Value = catalogItemID;
					instrumentedSqlCommand.Parameters.Add("@ParamsHash", SqlDbType.Int).Value = paramsHash;
					instrumentedSqlCommand.Parameters.Add("@EffectiveParams", SqlDbType.NVarChar).Value = effectiveParams;
					instrumentedSqlCommand.Parameters.Add("@ContentType", SqlDbType.NVarChar).Value = contentType.ToString();
					instrumentedSqlCommand.Parameters.Add("@Version", SqlDbType.SmallInt).Value = version;
					instrumentedSqlCommand.Parameters.Add("@Content", SqlDbType.VarBinary).Value = content;
					instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Error in CreateOrUpdateContentCache: {0}", new object[] { ex });
				throw;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009CD8 File Offset: 0x00007ED8
		public bool TryGetContentCache(Guid catalogItemID, int paramsHash, DBInterface.ContentCacheTypes contentType, out byte[] content, out int version)
		{
			content = null;
			version = 0;
			bool flag;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetContentCache", null))
				{
					instrumentedSqlCommand.AddParameter("@CatalogItemID", SqlDbType.UniqueIdentifier, catalogItemID);
					instrumentedSqlCommand.Parameters.AddWithValue("@ParamsHash", paramsHash);
					instrumentedSqlCommand.Parameters.AddWithValue("@ContentType", contentType.ToString());
					using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
					{
						if (dataReader.Read())
						{
							if (dataReader["Content"] != null)
							{
								content = DataReaderHelper.ReadAllBytes(dataReader, dataReader.GetOrdinal("Content"));
							}
							if (dataReader["Version"] != null)
							{
								version = (int)dataReader.GetInt16(dataReader.GetOrdinal("Version"));
							}
							flag = true;
						}
						else
						{
							flag = false;
						}
					}
				}
			}
			catch (Exception ex)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Error in GetContentCache: {0}", new object[] { ex });
				throw;
			}
			return flag;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00009DF8 File Offset: 0x00007FF8
		public bool TryGetContentCacheDetails(Guid catalogItemID, int paramsHash, DBInterface.ContentCacheTypes contentType, out int version)
		{
			bool flag;
			try
			{
				version = 0;
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetContentCacheDetails", null))
				{
					instrumentedSqlCommand.AddParameter("@CatalogItemID", SqlDbType.UniqueIdentifier, catalogItemID);
					instrumentedSqlCommand.Parameters.AddWithValue("@ParamsHash", paramsHash);
					instrumentedSqlCommand.Parameters.AddWithValue("@ContentType", contentType.ToString());
					using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
					{
						if (dataReader.Read())
						{
							if (dataReader["Version"] != null)
							{
								version = (int)dataReader.GetInt16(dataReader.GetOrdinal("Version"));
							}
							flag = true;
						}
						else
						{
							flag = false;
						}
					}
				}
			}
			catch (Exception ex)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Error in GetContentCacheDetails: {0}", new object[] { ex });
				throw;
			}
			return flag;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00009EF4 File Offset: 0x000080F4
		public int CleanExpiredContentCache()
		{
			int num = 0;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CleanExpiredContentCache", null))
				{
					num = (int)instrumentedSqlCommand.ExecuteScalar();
				}
			}
			catch (Exception ex)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in CleanExpiredContentCache: {0}", new object[] { ex });
				throw;
			}
			return num;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00009F64 File Offset: 0x00008164
		public int FlushContentCache(CatalogItemPath itemPath)
		{
			int num = 0;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("FlushContentCache", null))
				{
					instrumentedSqlCommand.Parameters.Add("@Path", SqlDbType.VarChar).Value = itemPath.Value;
					num = (int)instrumentedSqlCommand.ExecuteScalar();
				}
			}
			catch (Exception ex)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in FlushContentCache: {0}", new object[] { ex });
				throw;
			}
			return num;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00009FF4 File Offset: 0x000081F4
		public int GetSnapshotPromotedInfo(ReportSnapshot reportSnapshot, out bool hasDocMap, out PaginationMode paginationMode, out ReportProcessingFlags processingFlags)
		{
			return this.InternalGetSnapshotInfo(reportSnapshot.SnapshotDataID, reportSnapshot.IsPermanentSnapshot, false, out hasDocMap, out paginationMode, out processingFlags);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000A010 File Offset: 0x00008210
		public ReportProcessingFlags GetSnapshotProcessingFlags(Guid snapshotId, bool isPermanentSnapshot)
		{
			bool flag;
			PaginationMode paginationMode;
			ReportProcessingFlags reportProcessingFlags;
			this.InternalGetSnapshotInfo(snapshotId, isPermanentSnapshot, true, out flag, out paginationMode, out reportProcessingFlags);
			return reportProcessingFlags;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000A030 File Offset: 0x00008230
		public void PromoteSnapshotInfo(ReportSnapshot reportSnapshot, int pageCount, bool hasDocumentMap, PaginationMode paginationMode, ReportProcessingFlags processingFlags)
		{
			DBInterface.ThrowOnInvalidReportSnapshot(reportSnapshot);
			short num = SessionReportItem.PaginationModeToShort(paginationMode);
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("PromoteSnapshotInfo", null))
			{
				instrumentedSqlCommand.Parameters.Add("@SnapshotDataId", SqlDbType.UniqueIdentifier).Value = reportSnapshot.SnapshotDataID;
				instrumentedSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = reportSnapshot.IsPermanentSnapshot;
				instrumentedSqlCommand.Parameters.Add("@PageCount", SqlDbType.Int).Value = pageCount;
				instrumentedSqlCommand.Parameters.Add("@HasDocMap", SqlDbType.Bit).Value = hasDocumentMap;
				instrumentedSqlCommand.Parameters.Add("@PaginationMode", SqlDbType.SmallInt).Value = num;
				instrumentedSqlCommand.Parameters.Add("@ProcessingFlags", SqlDbType.Int).Value = (int)processingFlags;
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000A134 File Offset: 0x00008334
		public void UpdateSnapshotPaginationInfo(ReportSnapshot reportSnapshot, int pageCount, PaginationMode paginationMode)
		{
			short num = SessionReportItem.PaginationModeToShort(paginationMode);
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("UpdateSnapshotPaginationInfo", null))
			{
				instrumentedSqlCommand.Parameters.Add("@SnapshotDataId", SqlDbType.UniqueIdentifier).Value = reportSnapshot.SnapshotDataID;
				instrumentedSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = reportSnapshot.IsPermanentSnapshot;
				instrumentedSqlCommand.Parameters.Add("@PageCount", SqlDbType.Int).Value = pageCount;
				instrumentedSqlCommand.Parameters.Add("@PaginationMode", SqlDbType.SmallInt).Value = num;
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000A1F4 File Offset: 0x000083F4
		public void UpdateSnapshotReferences(ReportSnapshot oldSnapshot, ReportSnapshot newSnapshot, int transientRefCountModifier, out int updatedReferences)
		{
			DBInterface.ThrowOnInvalidReportSnapshot(oldSnapshot);
			DBInterface.ThrowOnInvalidReportSnapshot(newSnapshot);
			if (oldSnapshot.IsPermanentSnapshot != newSnapshot.IsPermanentSnapshot)
			{
				throw new InternalCatalogException("Attempt to mix permanent and non-permanent snapshots");
			}
			bool isPermanentSnapshot = oldSnapshot.IsPermanentSnapshot;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("UpdateSnapshotReferences", null))
			{
				instrumentedSqlCommand.Parameters.Add("@OldSnapshotId", SqlDbType.UniqueIdentifier).Value = oldSnapshot.SnapshotDataID;
				instrumentedSqlCommand.Parameters.Add("@NewSnapshotId", SqlDbType.UniqueIdentifier).Value = newSnapshot.SnapshotDataID;
				instrumentedSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanentSnapshot;
				instrumentedSqlCommand.Parameters.Add("@TransientRefCountModifier", SqlDbType.Int).Value = transientRefCountModifier;
				SqlParameter sqlParameter = instrumentedSqlCommand.Parameters.Add("@UpdatedReferences", SqlDbType.Int);
				sqlParameter.Direction = ParameterDirection.Output;
				instrumentedSqlCommand.ExecuteNonQuery();
				updatedReferences = (int)sqlParameter.Value;
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000A300 File Offset: 0x00008500
		public void CreateNewSnapshotVersion(ReportSnapshot oldSnapshot, ReportSnapshot newSnapshot)
		{
			DBInterface.ThrowOnInvalidReportSnapshot(oldSnapshot);
			DBInterface.ThrowOnInvalidReportSnapshot(newSnapshot);
			if (oldSnapshot.IsPermanentSnapshot != newSnapshot.IsPermanentSnapshot)
			{
				throw new InternalCatalogException("Attempt to mix permanent and non-permanent snapshots");
			}
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CreateNewSnapshotVersion", null))
			{
				instrumentedSqlCommand.Parameters.Add("@OldSnapshotId", SqlDbType.UniqueIdentifier).Value = oldSnapshot.SnapshotDataID;
				instrumentedSqlCommand.Parameters.Add("@NewSnapshotId", SqlDbType.UniqueIdentifier).Value = newSnapshot.SnapshotDataID;
				instrumentedSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = newSnapshot.IsPermanentSnapshot;
				instrumentedSqlCommand.Parameters.Add("@Machine", SqlDbType.NVarChar).Value = Environment.MachineName;
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000A3E4 File Offset: 0x000085E4
		private int InternalGetSnapshotInfo(Guid snapshotId, bool isPermanent, bool processingFlagsOnly, out bool hasDocumentMap, out PaginationMode paginationMode, out ReportProcessingFlags processingFlags)
		{
			RSTrace.CatalogTrace.Assert(snapshotId != Guid.Empty, "snapshotId");
			int num2;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetSnapshotPromotedInfo", null))
			{
				instrumentedSqlCommand.Parameters.Add("@SnapshotDataId", SqlDbType.UniqueIdentifier).Value = snapshotId;
				instrumentedSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanent;
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						throw new InternalCatalogException("no data found for snapshot");
					}
					processingFlags = ReportProcessingFlags.NotSet;
					hasDocumentMap = false;
					paginationMode = PaginationMode.Estimate;
					int num = 0;
					if (!dataReader.IsDBNull(3))
					{
						processingFlags = (ReportProcessingFlags)dataReader.GetInt32(3);
					}
					if (!processingFlagsOnly)
					{
						if (dataReader.IsDBNull(0))
						{
							throw new VersionMismatchException(snapshotId, isPermanent);
						}
						num = dataReader.GetInt32(0);
						hasDocumentMap = dataReader.GetBoolean(1);
						if (!dataReader.IsDBNull(2))
						{
							short @int = dataReader.GetInt16(2);
							paginationMode = SessionReportItem.PaginationModeFromShort(@int);
						}
					}
					num2 = num;
				}
			}
			return num2;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000A504 File Offset: 0x00008704
		public Guid UpdateCompiledDefinition(CatalogItemPath itemPath, Guid oldSnapshotId, Guid newSnapshotId)
		{
			Guid guid2;
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("UpdateCompiledDefinition"))
			{
				cancelableSqlCommand.Parameters.Add("@Path", SqlDbType.NVarChar).Value = itemPath.Value;
				cancelableSqlCommand.Parameters.Add("@OldSnapshotId", SqlDbType.UniqueIdentifier).Value = ((oldSnapshotId == Guid.Empty) ? DBNull.Value : oldSnapshotId);
				cancelableSqlCommand.Parameters.Add("@NewSnapshotId", SqlDbType.UniqueIdentifier).Value = newSnapshotId;
				SqlParameter sqlParameter = cancelableSqlCommand.Parameters.Add("@ItemId", SqlDbType.UniqueIdentifier);
				sqlParameter.Direction = ParameterDirection.Output;
				cancelableSqlCommand.ExecuteNonQuery();
				Guid guid = Guid.Empty;
				if (sqlParameter.Value != DBNull.Value)
				{
					guid = (Guid)sqlParameter.Value;
				}
				guid2 = guid;
			}
			return guid2;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000A5E8 File Offset: 0x000087E8
		protected static void ThrowOnInvalidReportSnapshot(ReportSnapshot snapshot)
		{
			if (snapshot == null || snapshot.SnapshotDataID == Guid.Empty)
			{
				throw new InternalCatalogException("Invalid ReportSnapshot");
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000A60C File Offset: 0x0000880C
		public bool AddHistoryRecord(Guid snapshotId, Guid reportId, DateTime snapshotDate, ReportSnapshot snapshotData, int SnapshotTransientRefcountChange)
		{
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("AddHistoryRecord", null))
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@HistoryID", snapshotId).SqlDbType = SqlDbType.UniqueIdentifier;
					instrumentedSqlCommand.Parameters.AddWithValue("@ReportID", reportId).SqlDbType = SqlDbType.UniqueIdentifier;
					instrumentedSqlCommand.Parameters.AddWithValue("@SnapshotDate", snapshotDate).SqlDbType = SqlDbType.DateTime;
					instrumentedSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier).Value = snapshotData.SnapshotDataID;
					instrumentedSqlCommand.Parameters.Add("@SnapshotTransientRefcountChange", SqlDbType.Int).Value = SnapshotTransientRefcountChange;
					instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			catch (ReportServerStorageException ex)
			{
				if (!ex.IsSqlException)
				{
					throw;
				}
				if (ex.SqlErrors.Count > 0 && ex.SqlErrors[0].Number == Native.SqlUniqueIndexViolationCode)
				{
					return false;
				}
				throw;
			}
			return true;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000A728 File Offset: 0x00008928
		public int CleanHistoryForReport(Guid reportId, int snapshotLimit)
		{
			int num;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CleanHistoryForReport", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ReportID", reportId);
				instrumentedSqlCommand.Parameters.AddWithValue("@SnapshotLimit", snapshotLimit);
				num = instrumentedSqlCommand.ExecuteNonQuery();
			}
			return num;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000A794 File Offset: 0x00008994
		public void SetHistoryLimit(CatalogItemPath item, int snapshotLimit)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetHistoryLimit", null))
			{
				instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, item.Value);
				if (snapshotLimit != -2)
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@SnapshotLimit", snapshotLimit);
				}
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000A804 File Offset: 0x00008A04
		public int CleanAllHistories(int snapshotLimit)
		{
			int num;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CleanAllHistories", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@SnapshotLimit", snapshotLimit);
				num = instrumentedSqlCommand.ExecuteNonQuery();
			}
			return num;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000A85C File Offset: 0x00008A5C
		public bool DeleteHistoryRecord(Guid reportId, DateTime snapshotDate)
		{
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteHistoryRecord", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ReportID", reportId);
				instrumentedSqlCommand.Parameters.AddWithValue("@SnapshotDate", snapshotDate);
				flag = instrumentedSqlCommand.ExecuteNonQuery() >= 1;
			}
			return flag;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		public bool DeleteHistoryRecord(Guid reportId, string historyId)
		{
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteHistoryRecordByHistoryId", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ReportID", reportId);
				instrumentedSqlCommand.Parameters.AddWithValue("@HistoryID", historyId);
				flag = instrumentedSqlCommand.ExecuteNonQuery() >= 1;
			}
			return flag;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000A940 File Offset: 0x00008B40
		public bool DeleteAllHistoryForReport(Guid reportId)
		{
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteAllHistoryForReport", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ReportID", reportId);
				flag = instrumentedSqlCommand.ExecuteNonQuery() >= 1;
			}
			return flag;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000A99C File Offset: 0x00008B9C
		public bool DeleteHistoriesWithNoPolicy()
		{
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteHistoriesWithNoPolicy", null))
			{
				flag = instrumentedSqlCommand.ExecuteNonQuery() >= 1;
			}
			return flag;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		public ReportHistorySnapshot[] ListHistory(Guid reportId)
		{
			ReportHistorySnapshot[] array;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ListHistory", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ReportID", reportId);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					ArrayList arrayList = new ArrayList();
					while (dataReader.Read())
					{
						ReportHistorySnapshot reportHistorySnapshot = new ReportHistorySnapshot();
						reportHistorySnapshot.CreationDate = dataReader.GetDateTime(0).ToLocalTime();
						reportHistorySnapshot.HistoryID = Globals.ToSnapshotDateFormat(dataReader.GetDateTime(0));
						if (!dataReader.IsDBNull(1))
						{
							long @int = dataReader.GetInt64(1);
							reportHistorySnapshot.Size = (int)Math.Min(2147483647L, @int);
						}
						arrayList.Add(reportHistorySnapshot);
					}
					array = (ReportHistorySnapshot[])arrayList.ToArray(typeof(ReportHistorySnapshot));
				}
			}
			return array;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000AAD0 File Offset: 0x00008CD0
		public HistorySnapshot[] ListHistorySnapshots(Guid reportId)
		{
			HistorySnapshot[] array;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ListHistorySnapshots", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ReportID", reportId);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					ArrayList arrayList = new ArrayList();
					DateTime dateTime = DateTime.Now;
					while (dataReader.Read())
					{
						HistorySnapshot historySnapshot = new HistorySnapshot();
						historySnapshot.Id = dataReader.GetGuid(0);
						dateTime = dataReader.GetDateTime(3);
						historySnapshot.Snapshot.HistoryID = Globals.ToSnapshotDateFormat(dateTime);
						historySnapshot.Snapshot.CreationDate = dateTime.ToLocalTime();
						if (!dataReader.IsDBNull(4))
						{
							long @int = dataReader.GetInt64(4);
							historySnapshot.Snapshot.Size = (int)Math.Min(2147483647L, @int);
						}
						arrayList.Add(historySnapshot);
					}
					array = (HistorySnapshot[])arrayList.ToArray(typeof(HistorySnapshot));
				}
			}
			return array;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		public HistorySnapshot[] ListHistorySnapshotsNoSize(Guid reportId)
		{
			HistorySnapshot[] array;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ListHistorySnapshotsNoSize", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ReportID", reportId);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					ArrayList arrayList = new ArrayList();
					DateTime dateTime = DateTime.Now;
					while (dataReader.Read())
					{
						HistorySnapshot historySnapshot = new HistorySnapshot();
						historySnapshot.Id = dataReader.GetGuid(0);
						dateTime = dataReader.GetDateTime(3);
						historySnapshot.Snapshot.HistoryID = Globals.ToSnapshotDateFormat(dateTime);
						historySnapshot.Snapshot.CreationDate = dateTime.ToLocalTime();
						if (!dataReader.IsDBNull(4))
						{
							historySnapshot.Snapshot.Size = 0;
						}
						arrayList.Add(historySnapshot);
					}
					array = (HistorySnapshot[])arrayList.ToArray(typeof(HistorySnapshot));
				}
			}
			return array;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000ACDC File Offset: 0x00008EDC
		public void CreateRdlChunk(Guid itemId, ReportSnapshot snapshot, string chunkName)
		{
			byte b = 0;
			short currentVersion = ChunkHeader.CurrentVersion;
			int num = 2;
			string text = null;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CreateRdlChunk", null))
			{
				instrumentedSqlCommand.Parameters.Add("@ItemId", SqlDbType.UniqueIdentifier).Value = itemId;
				instrumentedSqlCommand.Parameters.Add("@SnapshotId", SqlDbType.UniqueIdentifier).Value = snapshot.SnapshotDataID;
				instrumentedSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = snapshot.IsPermanentSnapshot;
				instrumentedSqlCommand.Parameters.Add("@ChunkName", SqlDbType.NVarChar).Value = chunkName;
				instrumentedSqlCommand.Parameters.Add("@ChunkFlags", SqlDbType.TinyInt).Value = b;
				instrumentedSqlCommand.Parameters.Add("@ChunkType", SqlDbType.Int).Value = num;
				instrumentedSqlCommand.Parameters.Add("@Version", SqlDbType.SmallInt).Value = currentVersion;
				instrumentedSqlCommand.Parameters.Add("@MimeType", SqlDbType.NVarChar).Value = text;
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000AE18 File Offset: 0x00009018
		public virtual bool FindObjectsNonRecursive(ExternalItemPath objectName, out CatalogItemList children, Security secMgr, IPathTranslator pathTranslator, bool appendMyReports)
		{
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("FindObjectsNonRecursive", null))
			{
				instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, pathTranslator.ExternalToCatalog(objectName.Value));
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				flag = this.FindChildren(instrumentedSqlCommand, out children, secMgr, pathTranslator, appendMyReports);
			}
			return flag;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000AE9C File Offset: 0x0000909C
		public virtual bool FindObjectsRecursive(ExternalItemPath namePrefix, out CatalogItemList children, Security secMgr, IPathTranslator pathTranslator, bool appendMyReports)
		{
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("FindObjectsRecursive", null))
			{
				instrumentedSqlCommand.AddParameter("@Prefix", SqlDbType.NVarChar, Storage.EncodeForLike(pathTranslator.ExternalToCatalog(namePrefix.Value)) + "/%");
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				flag = this.FindChildren(instrumentedSqlCommand, out children, secMgr, pathTranslator, appendMyReports);
			}
			return flag;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000AF2C File Offset: 0x0000912C
		public virtual bool FindFavoriteableItemsRecursive(ExternalItemPath objectName, out FavoriteableCatalogItemList children, Security secMgr, IPathTranslator pathTranslator, bool appendMyReports)
		{
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("FindFavoriteableItemsRecursive", null))
			{
				instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, Storage.EncodeForLike(pathTranslator.ExternalToCatalog(objectName.Value)) + "/%");
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				instrumentedSqlCommand.AddParameter("@UserName", SqlDbType.NVarChar, this.UserContext.UserName);
				instrumentedSqlCommand.Parameters.AddWithValue("@UserSid", Global.NameToSid(this.UserContext));
				flag = this.FindFavoriteableItems(instrumentedSqlCommand, out children, secMgr, pathTranslator, appendMyReports);
			}
			return flag;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000AFF4 File Offset: 0x000091F4
		public virtual bool FindFavoriteableItemsNonRecursive(ExternalItemPath objectName, out FavoriteableCatalogItemList children, Security secMgr, IPathTranslator pathTranslator, bool appendMyReports)
		{
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("FindFavoriteableItemsNonRecursive", null))
			{
				instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, pathTranslator.ExternalToCatalog(objectName.Value));
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				instrumentedSqlCommand.AddParameter("@UserName", SqlDbType.NVarChar, this.UserContext.UserName);
				instrumentedSqlCommand.Parameters.AddWithValue("@UserSid", Global.NameToSid(this.UserContext));
				flag = this.FindFavoriteableItems(instrumentedSqlCommand, out children, secMgr, pathTranslator, appendMyReports);
			}
			return flag;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000B0AC File Offset: 0x000092AC
		public virtual FavoriteableCatalogItemList GetAllFavoriteItems(Security secMgr, IPathTranslator pathTranslator)
		{
			FavoriteableCatalogItemList allFavoriteItems;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetAllFavoriteItems", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				instrumentedSqlCommand.AddParameter("@UserName", SqlDbType.NVarChar, this.UserContext.UserName);
				instrumentedSqlCommand.Parameters.AddWithValue("@UserSid", Global.NameToSid(this.UserContext));
				allFavoriteItems = this.GetAllFavoriteItems(instrumentedSqlCommand, secMgr, pathTranslator);
			}
			return allFavoriteItems;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000B144 File Offset: 0x00009344
		public virtual bool FindParents(ExternalItemPath objectName, out CatalogItemList parents, Security secMgr, IPathTranslator pathTranslator)
		{
			ICatalogItemDescriptorFactory catalogItemDescriptorFactory = this.CreateDescriptorFactory(pathTranslator, secMgr, ItemDescriptorOptions.None);
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("FindParents", null))
			{
				instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, pathTranslator.ExternalToCatalog(objectName.Value));
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				flag = this.FindObjects(instrumentedSqlCommand, out parents, catalogItemDescriptorFactory);
			}
			return flag;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000B1CC File Offset: 0x000093CC
		public bool FindObjectsByLink(Guid link, out CatalogItemList reports, Security secMgr, IPathTranslator pathTranslator)
		{
			ICatalogItemDescriptorFactory catalogItemDescriptorFactory = this.CreateDescriptorFactory(pathTranslator, secMgr, ItemDescriptorOptions.None);
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("FindObjectsByLink", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@Link", link);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				flag = this.FindObjects(instrumentedSqlCommand, out reports, catalogItemDescriptorFactory);
			}
			return flag;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000B250 File Offset: 0x00009450
		public bool FindItemsByDataSource(Guid dsItemID, out CatalogItemList reports, Security secMgr, IPathTranslator pathTranslator, bool recursive)
		{
			ICatalogItemDescriptorFactory catalogItemDescriptorFactory = this.CreateDescriptorFactory(pathTranslator, secMgr, ItemDescriptorOptions.None);
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand(recursive ? "FindItemsByDataSourceRecursive" : "FindItemsByDataSource", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", dsItemID);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				flag = this.FindObjects(instrumentedSqlCommand, out reports, catalogItemDescriptorFactory);
			}
			return flag;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000B2E0 File Offset: 0x000094E0
		public bool FindItemsByDataSet(Guid dsItemID, out CatalogItemList reports, Security secMgr, IPathTranslator pathTranslator)
		{
			ICatalogItemDescriptorFactory catalogItemDescriptorFactory = this.CreateDescriptorFactory(pathTranslator, secMgr, ItemDescriptorOptions.None);
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("FindItemsByDataSet", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", dsItemID);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				flag = this.FindObjects(instrumentedSqlCommand, out reports, catalogItemDescriptorFactory);
			}
			return flag;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000B364 File Offset: 0x00009564
		public bool HasRelatedItem(Guid datasetId, int relatedItemType)
		{
			bool flag;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("FindItemsByDataSet", null))
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", datasetId);
					instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
					instrumentedSqlCommand.Parameters.AddWithValue("@Type", relatedItemType);
					using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
					{
						flag = dataReader.Read();
					}
				}
			}
			catch (Exception ex)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Error in HasRelatedItem: {0}", new object[] { ex });
				throw;
			}
			return flag;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000B438 File Offset: 0x00009638
		public long CreateExtendedCatalogContent(ExtendedContentType contentType, Stream stream, out long contentSize)
		{
			long num = -1L;
			RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Create new, empty extended content record");
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("InitializeCatalogExtendedContentWrite", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@CatalogItemID", DBNull.Value);
				instrumentedSqlCommand.Parameters.AddWithValue("@ContentType", contentType.ToString());
				num = (long)instrumentedSqlCommand.ExecuteScalar();
			}
			RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Write content into the new extended content record");
			contentSize = this.WriteCatalogExtendedContentById(num, stream);
			return num;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000B4DC File Offset: 0x000096DC
		public void FinalizeNewExtendedCatalogContent(long id, Guid catalogItemId)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Link new extended content record to a catalog record and update content size");
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("FinalizeTempCatalogExtendedContentWrite", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@Id", id);
				instrumentedSqlCommand.Parameters.AddWithValue("@CatalogItemID", catalogItemId);
				if ((long)instrumentedSqlCommand.ExecuteNonQuery() == 0L)
				{
					throw new InternalCatalogException("FinalizeTempCatalogExtendedContentWrite could not find the entry.");
				}
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000B564 File Offset: 0x00009764
		public long WriteExtendedCatalogContent(Guid id, ExtendedContentType extendedContentType, Stream stream, DateTime? modifiedDate = null)
		{
			string text = extendedContentType.ToString();
			RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Initialize content for catalog item with extended content");
			long num;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("InitializeCatalogExtendedContentWrite", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@CatalogItemID", id);
				instrumentedSqlCommand.Parameters.AddWithValue("@ContentType", text);
				object obj = instrumentedSqlCommand.ExecuteScalar();
				if (obj is DBNull)
				{
					throw new InternalCatalogException("InitializeCatalogExtendedContentWrite could not find the entry.");
				}
				num = (long)obj;
			}
			RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Update content for catalog item with extended content");
			long num2 = this.WriteCatalogExtendedContentById(num, stream);
			this.UpdateLastModifieDate(id, extendedContentType, modifiedDate);
			return num2;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000B624 File Offset: 0x00009824
		public void WriteContentSize(Guid id, long contentSize)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Update content size for catalog item with extended content");
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("UpdateCatalogContentSize", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@CatalogItemID", id);
				instrumentedSqlCommand.Parameters.AddWithValue("@ContentSize", contentSize);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000B6A0 File Offset: 0x000098A0
		public void WriteDataModelParameters(Guid id, string parameters)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Update parameters for catalog item");
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("UpdateDataModelParametersById", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@CatalogItemID", id);
				instrumentedSqlCommand.Parameters.AddWithValue("@Parameters", parameters);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000B718 File Offset: 0x00009918
		public string GetDataModelParametersById(Guid id)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Get parameters for catalog item");
			string text;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetDataModelParametersById", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@CatalogItemID", id);
				object obj = instrumentedSqlCommand.ExecuteScalar();
				if (obj != null && DBNull.Value != obj)
				{
					text = (string)obj;
				}
				else
				{
					text = null;
				}
			}
			return text;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000B794 File Offset: 0x00009994
		private void UpdateLastModifieDate(Guid catalogItemId, ExtendedContentType extendedContentType, DateTime? modifiedDate = null)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("UpdateCatalogExtendedContentModifiedDate", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@CatalogItemID", catalogItemId);
				instrumentedSqlCommand.Parameters.AddWithValue("@ContentType", extendedContentType.ToString());
				if (modifiedDate != null)
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@ModifiedDate", modifiedDate.Value);
				}
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000B82C File Offset: 0x00009A2C
		private long WriteCatalogExtendedContentById(long Id, Stream stream)
		{
			byte[] array = new byte[5242880];
			int num = 0;
			stream.Position = 0L;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("WriteCatalogExtendedContentChunkById", null))
			{
				for (;;)
				{
					int num2 = stream.Read(array, 0, array.Length);
					if (num2 == 0)
					{
						break;
					}
					instrumentedSqlCommand.Parameters.Clear();
					instrumentedSqlCommand.Parameters.AddWithValue("@Id", Id);
					instrumentedSqlCommand.Parameters.AddWithValue("@Offset", num);
					instrumentedSqlCommand.Parameters.AddWithValue("@Length", num2);
					instrumentedSqlCommand.Parameters.Add(new SqlParameter("@chunk", SqlDbType.VarBinary, num2, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, array));
					instrumentedSqlCommand.ExecuteNonQuery();
					num += num2;
				}
			}
			return (long)num;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000B910 File Offset: 0x00009B10
		public bool GetCatalogExtendedContentData(Guid catalogItemId, ExtendedContentType extendedContentType, out byte[] objectContent)
		{
			objectContent = null;
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetCatalogExtendedContentData", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@CatalogItemID", catalogItemId);
				instrumentedSqlCommand.Parameters.AddWithValue("@ContentType", extendedContentType.ToString());
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						flag = false;
					}
					else
					{
						if (!dataReader.IsDBNull(0))
						{
							objectContent = DataReaderHelper.ReadAllBytes(dataReader, 1);
						}
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000B9BC File Offset: 0x00009BBC
		public bool FindObjectsGeneral(ExternalItemPath namePrefix, BooleanOperatorEnum boolOperator, ItemSearchOptions options, ItemSearchConditions conditions, out CatalogItemList reports, Security secMgr, IPathTranslator pathTranslator)
		{
			ItemDescriptorOptions itemDescriptorOptions = ItemDescriptorOptions.None;
			if (options.ComponentLookup)
			{
				itemDescriptorOptions |= ItemDescriptorOptions.MovedItemTracking;
			}
			ICatalogItemDescriptorFactory catalogItemDescriptorFactory = this.CreateDescriptorFactory(pathTranslator, secMgr, itemDescriptorOptions);
			string text = boolOperator.ToString();
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("", null))
			{
				instrumentedSqlCommand.CommandType = CommandType.Text;
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				StringBuilder stringBuilder = new StringBuilder("\r\nSELECT\r\n   C.Type,\r\n   C.PolicyID,\r\n   SD.NtSecDescPrimary,\r\n   C.Name,\r\n   C.Path,\r\n   C.ItemID,\r\n   C.ContentSize AS [Size],\r\n   C.Description,\r\n   C.CreationDate,\r\n   C.ModifiedDate,\r\n   SUSER_SNAME(CU.Sid),\r\n   CU.UserName,\r\n   SUSER_SNAME(MU.Sid),\r\n   MU.UserName,\r\n   C.MimeType,\r\n   C.ExecutionTime,\r\n   C.Hidden,\r\n   C.SubType,\r\n   C.ComponentID\r\nFROM\r\n   Catalog AS C\r\n   INNER JOIN Users AS CU ON C.CreatedByID = CU.UserID\r\n   INNER JOIN Users AS MU ON C.ModifiedByID = MU.UserID\r\n   LEFT OUTER JOIN SecData AS SD ON C.PolicyID = SD.PolicyID AND SD.AuthType = @AuthType\r\n");
				string text2 = null;
				string text3 = pathTranslator.ExternalToCatalog(namePrefix.Value);
				if (options.Recursive)
				{
					if (text3 != string.Empty)
					{
						text2 = "C.Path like @Prefix escape '*'";
						text3 = DBInterface.BuildRecursivePathPrefix(text3);
						instrumentedSqlCommand.AddParameter("@Prefix", SqlDbType.NVarChar, text3);
					}
				}
				else
				{
					stringBuilder.Append(" INNER JOIN Catalog AS P ON C.ParentID = P.ItemID");
					text2 = "P.Path = @Path";
					instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, text3);
				}
				StringBuilder stringBuilder2 = new StringBuilder();
				foreach (KeyValuePair<string, ItemSearchCondition> keyValuePair in conditions)
				{
					string key = keyValuePair.Key;
					ItemSearchCondition value = keyValuePair.Value;
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(key);
					if (num <= 2220975494U)
					{
						if (num <= 1284350966U)
						{
							if (num != 266367750U)
							{
								if (num == 1284350966U)
								{
									if (key == "ModifiedDate")
									{
										DBInterface.AddDateTimeSearchCondition(key, value, "C.ModifiedDate", text, stringBuilder2, instrumentedSqlCommand);
										continue;
									}
								}
							}
							else if (key == "Name")
							{
								DBInterface.AddSearchCondition(key, value, "C.Name", text, stringBuilder2, instrumentedSqlCommand);
								continue;
							}
						}
						else if (num != 1725856265U)
						{
							if (num != 1974461284U)
							{
								if (num == 2220975494U)
								{
									if (key == "CreatedBy")
									{
										DBInterface.AddSearchCondition(key, value, "ISNULL(SUSER_SNAME(CU.Sid), CU.UserName)", text, stringBuilder2, instrumentedSqlCommand);
										continue;
									}
								}
							}
							else if (key == "All")
							{
								if (stringBuilder2.Length > 0)
								{
									stringBuilder2.Append(" " + text + " ");
								}
								string text4 = Storage.EncodeForSearch(value.Values[0], true);
								stringBuilder2.Append("(C.Name like @Text escape '*' or C.Description like @Text escape '*')");
								instrumentedSqlCommand.AddParameter("@Text", SqlDbType.NVarChar, text4);
								continue;
							}
						}
						else if (key == "Description")
						{
							DBInterface.AddSearchCondition(key, value, "C.Description", text, stringBuilder2, instrumentedSqlCommand);
							continue;
						}
					}
					else if (num <= 2881822685U)
					{
						if (num != 2392923389U)
						{
							if (num == 2881822685U)
							{
								if (key == "Subtype")
								{
									DBInterface.AddSearchCondition(key, value, "C.SubType", text, stringBuilder2, instrumentedSqlCommand);
									continue;
								}
							}
						}
						else if (key == "ComponentID")
						{
							DBInterface.AddSearchCondition(key, value, "C.ComponentID", text, stringBuilder2, instrumentedSqlCommand);
							continue;
						}
					}
					else if (num != 2972576864U)
					{
						if (num != 3512062061U)
						{
							if (num == 3514669835U)
							{
								if (key == "ModifiedBy")
								{
									DBInterface.AddSearchCondition(key, value, "ISNULL(SUSER_SNAME(MU.Sid), MU.UserName)", text, stringBuilder2, instrumentedSqlCommand);
									continue;
								}
							}
						}
						else if (key == "Type")
						{
							DBInterface.AddSearchCondition(key, value, "C.Type", text, stringBuilder2, instrumentedSqlCommand);
							continue;
						}
					}
					else if (key == "CreationDate")
					{
						DBInterface.AddDateTimeSearchCondition(key, value, "C.CreationDate", text, stringBuilder2, instrumentedSqlCommand);
						continue;
					}
					RSTrace.CatalogTrace.Assert(false, "Invalid FindItems search condition");
				}
				stringBuilder.Append(" WHERE ");
				string text5 = "C.Path NOT LIKE '/68f0607b-9378-4bbb-9e70-4da3d7d66838%'";
				if (text2 != null)
				{
					stringBuilder.Append("(");
					stringBuilder.Append(text2);
					stringBuilder.Append(" AND ");
					stringBuilder.Append(text5);
					stringBuilder.Append(")");
				}
				else
				{
					stringBuilder.Append(text5);
				}
				if (stringBuilder2.Length > 0)
				{
					stringBuilder.Append(" AND ");
					stringBuilder.Append("(");
					stringBuilder.Append(stringBuilder2.ToString());
					stringBuilder.Append(")");
				}
				instrumentedSqlCommand.CommandText = stringBuilder.ToString();
				flag = this.FindObjects(instrumentedSqlCommand, out reports, catalogItemDescriptorFactory);
			}
			return flag;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000BE90 File Offset: 0x0000A090
		public static string BuildRecursivePathPrefix(string catalogPrefix)
		{
			if (string.IsNullOrEmpty(catalogPrefix))
			{
				return string.Empty;
			}
			string text = Storage.EncodeForLike(catalogPrefix);
			if (!text.EndsWith("/", StringComparison.OrdinalIgnoreCase))
			{
				return text + "/%";
			}
			return text + "%";
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000BED7 File Offset: 0x0000A0D7
		protected virtual ICatalogItemDescriptorFactory CreateDescriptorFactory(IPathTranslator pathTranslator, Security secMgr, ItemDescriptorOptions options)
		{
			return new CatalogItemDescriptorFactory(secMgr, pathTranslator, options);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		private static void AddDateTimeSearchCondition(string property, ItemSearchCondition condition, string field, string operation, StringBuilder query, InstrumentedSqlCommand command)
		{
			string text = field;
			if (condition.Domain == ComparisonDomain.String)
			{
				text = string.Format(CultureInfo.InvariantCulture, "CONVERT(char(25), {0}, 126)", field);
			}
			DBInterface.AddSearchCondition(property, condition, text, operation, query, command);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000BF1C File Offset: 0x0000A11C
		private static void AddSearchCondition(string property, ItemSearchCondition condition, string field, string operation, StringBuilder query, InstrumentedSqlCommand command)
		{
			RSTrace.CatalogTrace.Assert(condition.Values != null && condition.Values.Count > 0, "Invalid FindItems search condition");
			if (query.Length > 0)
			{
				query.Append(" " + operation + " ");
			}
			switch (condition.Condition)
			{
			case Microsoft.ReportingServices.Library.Soap2010.ConditionEnum.Contains:
				query.AppendFormat("{0} like @{1} escape '*'", field, property);
				command.AddParameter("@" + property, SqlDbType.NVarChar, Storage.EncodeForSearch(condition.Values[0], true));
				return;
			case Microsoft.ReportingServices.Library.Soap2010.ConditionEnum.Equals:
				query.AppendFormat("{0} = @{1}", field, property);
				condition.BindToSqlCommand(command, property, true);
				return;
			case Microsoft.ReportingServices.Library.Soap2010.ConditionEnum.In:
			{
				query.AppendFormat(CultureInfo.InvariantCulture, "{0} in (", field);
				for (int i = 0; i < condition.Values.Count; i++)
				{
					if (i > 0)
					{
						query.Append(", ");
					}
					query.AppendFormat(CultureInfo.InvariantCulture, "@{0}{1}", property, i);
				}
				query.Append(")");
				condition.BindToSqlCommand(command, property, false);
				return;
			}
			case Microsoft.ReportingServices.Library.Soap2010.ConditionEnum.Between:
				query.AppendFormat("(({0} between @{1}0 and @{1}1) OR ({0} between @{1}1 and @{1}0))", field, property);
				condition.BindToSqlCommand(command, property, false);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000C068 File Offset: 0x0000A268
		private bool FindChildren(InstrumentedSqlCommand command, out CatalogItemList result, Security secMgr, IPathTranslator pathTranslator, bool appendMyReports)
		{
			ICatalogItemDescriptorFactory catalogItemDescriptorFactory = this.CreateDescriptorFactory(pathTranslator, secMgr, ItemDescriptorOptions.None);
			bool flag = this.FindObjects(command, out result, catalogItemDescriptorFactory);
			if (appendMyReports)
			{
				CatalogItemDescriptor catalogItemDescriptor = new CatalogItemDescriptor();
				catalogItemDescriptor.Name = Global.VirtualMyReportsName;
				catalogItemDescriptor.VirtualPath = Global.VirtualMyReportsPath;
				catalogItemDescriptor.Path = pathTranslator.CatalogToExternal(pathTranslator.PathToInternal(Global.VirtualMyReportsPath));
				catalogItemDescriptor.Type = ItemType.Folder;
				catalogItemDescriptor.Description = RepLibRes.MyReportsFolderDescription;
				result.Add(catalogItemDescriptor);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		protected bool FindObjects(InstrumentedSqlCommand command, out CatalogItemList result, ICatalogItemDescriptorFactory descriptorFactory)
		{
			result = new CatalogItemList();
			bool flag = false;
			using (IDataReader dataReader = command.ExecuteReader())
			{
				CatalogItemDescriptor catalogItemDescriptor;
				while (dataReader.Read() && descriptorFactory.BuildFromDbRow(dataReader, out catalogItemDescriptor))
				{
					if (catalogItemDescriptor != null)
					{
						result.Add(catalogItemDescriptor);
						flag = true;
					}
				}
				dataReader.NextResult();
			}
			return flag;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000C144 File Offset: 0x0000A344
		private bool FindFavoriteableItems(InstrumentedSqlCommand command, out FavoriteableCatalogItemList result, Security secMgr, IPathTranslator pathTranslator, bool appendMyReports)
		{
			result = new FavoriteableCatalogItemList();
			bool flag = false;
			ICatalogItemDescriptorFactory catalogItemDescriptorFactory = this.CreateDescriptorFactory(pathTranslator, secMgr, ItemDescriptorOptions.None);
			using (IDataReader dataReader = command.ExecuteReader())
			{
				CatalogItemDescriptor catalogItemDescriptor;
				while (dataReader.Read() && catalogItemDescriptorFactory.BuildFromDbRow(dataReader, out catalogItemDescriptor))
				{
					if (catalogItemDescriptor != null)
					{
						FavoriteableCatalogItemDescriptor favoriteableCatalogItemDescriptor = new FavoriteableCatalogItemDescriptor(catalogItemDescriptor);
						favoriteableCatalogItemDescriptor.IsFavorite = dataReader.GetBoolean(DBInterface.FindItemColumns.IsFavorite);
						result.Add(favoriteableCatalogItemDescriptor);
						flag = true;
					}
				}
				dataReader.NextResult();
			}
			if (appendMyReports)
			{
				FavoriteableCatalogItemDescriptor favoriteableCatalogItemDescriptor2 = new FavoriteableCatalogItemDescriptor();
				favoriteableCatalogItemDescriptor2.Name = Global.VirtualMyReportsName;
				favoriteableCatalogItemDescriptor2.VirtualPath = Global.VirtualMyReportsPath;
				favoriteableCatalogItemDescriptor2.Path = pathTranslator.CatalogToExternal(pathTranslator.PathToInternal(Global.VirtualMyReportsPath));
				favoriteableCatalogItemDescriptor2.Type = ItemType.Folder;
				favoriteableCatalogItemDescriptor2.Description = RepLibRes.MyReportsFolderDescription;
				favoriteableCatalogItemDescriptor2.IsFavorite = false;
				result.Add(favoriteableCatalogItemDescriptor2);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000C234 File Offset: 0x0000A434
		private FavoriteableCatalogItemList GetAllFavoriteItems(InstrumentedSqlCommand command, Security secMgr, IPathTranslator pathTranslator)
		{
			FavoriteableCatalogItemList favoriteableCatalogItemList = new FavoriteableCatalogItemList();
			ICatalogItemDescriptorFactory catalogItemDescriptorFactory = this.CreateDescriptorFactory(pathTranslator, secMgr, ItemDescriptorOptions.None);
			using (IDataReader dataReader = command.ExecuteReader())
			{
				CatalogItemDescriptor catalogItemDescriptor;
				while (dataReader.Read() && catalogItemDescriptorFactory.BuildFromDbRow(dataReader, out catalogItemDescriptor))
				{
					if (catalogItemDescriptor != null)
					{
						favoriteableCatalogItemList.Add(new FavoriteableCatalogItemDescriptor(catalogItemDescriptor)
						{
							IsFavorite = dataReader.GetBoolean(DBInterface.FindItemColumns.IsFavorite)
						});
					}
				}
				dataReader.NextResult();
			}
			return favoriteableCatalogItemList;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
		public List<LinkedReportCatalogItem> GetLinkedReports(RSService service, Guid link)
		{
			List<LinkedReportCatalogItem> list;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetIDPairsByLink", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@Link", link);
				List<LinkInfo> idPairs = this.GetIdPairs(instrumentedSqlCommand);
				if (idPairs == null || idPairs.Count == 0)
				{
					list = null;
				}
				else
				{
					List<LinkedReportCatalogItem> list2 = new List<LinkedReportCatalogItem>();
					Guid reportID = idPairs[0].ReportID;
					foreach (LinkInfo linkInfo in idPairs)
					{
						RSTrace.CatalogTrace.Assert(reportID == linkInfo.ReportID);
						list2.Add((LinkedReportCatalogItem)service.CatalogItemFactory.GetCatalogItem(null, linkInfo.LinkID, ItemType.LinkedReport, null));
					}
					list = list2;
				}
			}
			return list;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000C3A8 File Offset: 0x0000A5A8
		private List<LinkInfo> GetIdPairs(InstrumentedSqlCommand command)
		{
			List<LinkInfo> list = new List<LinkInfo>();
			List<LinkInfo> list2;
			using (IDataReader dataReader = command.ExecuteReader())
			{
				while (dataReader.Read())
				{
					list.Add(new LinkInfo
					{
						ReportID = dataReader.GetGuid(0),
						LinkID = dataReader.GetGuid(1)
					});
				}
				if (list.Count > 0)
				{
					list2 = list;
				}
				else
				{
					list2 = null;
				}
			}
			return list2;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000C424 File Offset: 0x0000A624
		public string GetOneConfigurationInfo(string key)
		{
			string text;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetOneConfigurationInfo", null))
			{
				instrumentedSqlCommand.AddParameter("@Name", SqlDbType.NVarChar, key);
				text = (string)instrumentedSqlCommand.ExecuteScalar();
			}
			return text;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000C478 File Offset: 0x0000A678
		public SystemProperties GetAllConfigurationInfo()
		{
			SystemProperties systemProperties2;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetAllConfigurationInfo", null))
			{
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					SystemProperties systemProperties = new SystemProperties();
					while (dataReader.Read())
					{
						string @string = dataReader.GetString(0);
						string string2 = dataReader.GetString(1);
						systemProperties[@string] = string2;
					}
					systemProperties2 = systemProperties;
				}
			}
			return systemProperties2;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000C4FC File Offset: 0x0000A6FC
		public void SetConfigurationInfo(SystemProperties values)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetConfigurationInfo", null))
			{
				for (int i = 0; i < values.Count; i++)
				{
					string name = values.GetName(i);
					string value = values.GetValue(i);
					instrumentedSqlCommand.Parameters.Clear();
					instrumentedSqlCommand.AddParameter("@Name", SqlDbType.NVarChar, name);
					if (value == null)
					{
						instrumentedSqlCommand.Parameters.AddWithValue("@Value", DBNull.Value);
					}
					else
					{
						instrumentedSqlCommand.AddParameter("@Value", SqlDbType.NText, value);
					}
					instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		public void AddDataSource(Guid itemID, Guid subscriptionID, DataSourceInfo dataSource, IPathTranslator pathTranslator, string editSessionID, out Guid linkID, out ItemType linkType, out byte[] linkSecDesc)
		{
			linkID = Guid.Empty;
			linkType = ItemType.Unknown;
			linkSecDesc = null;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("AddDataSource", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				instrumentedSqlCommand.Parameters.AddWithValue("@DSID", dataSource.ID);
				if (!string.IsNullOrEmpty(editSessionID))
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@EditSessionID", editSessionID);
				}
				if (itemID != Guid.Empty)
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", itemID);
				}
				if (subscriptionID != Guid.Empty)
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@SubscriptionID", subscriptionID);
				}
				if (dataSource.OriginalName != null)
				{
					instrumentedSqlCommand.AddParameter("@Name", SqlDbType.NVarChar, dataSource.OriginalName);
				}
				if (dataSource.IsReference)
				{
					if (dataSource.LinkID != Guid.Empty)
					{
						instrumentedSqlCommand.Parameters.AddWithValue("@LinkID", dataSource.LinkID);
					}
					if (dataSource.DataSourceReference != null)
					{
						instrumentedSqlCommand.AddParameter("@LinkPath", SqlDbType.NVarChar, pathTranslator.ExternalToCatalog(dataSource.DataSourceReference));
					}
					instrumentedSqlCommand.Parameters.AddWithValue("@CredentialRetrieval", 1);
					instrumentedSqlCommand.Parameters.AddWithValue("@Flags", dataSource.FlagsForCatalogSerialization);
					if (dataSource.OriginalConnectionStringEncrypted != null)
					{
						instrumentedSqlCommand.Parameters.AddWithValue("@OriginalConnectionString", dataSource.OriginalConnectionStringEncrypted);
					}
					instrumentedSqlCommand.Parameters.AddWithValue("@OriginalConnectStringExpressionBased", dataSource.OriginalConnectStringExpressionBased);
				}
				else
				{
					instrumentedSqlCommand.AddParameter("@Extension", SqlDbType.NVarChar, dataSource.Extension);
					instrumentedSqlCommand.Parameters.AddWithValue("@CredentialRetrieval", (int)dataSource.CredentialsRetrieval);
					if (dataSource.Prompt != null)
					{
						instrumentedSqlCommand.AddParameter("@Prompt", SqlDbType.NText, dataSource.Prompt);
					}
					if (dataSource.ConnectionStringEncrypted != null)
					{
						instrumentedSqlCommand.Parameters.AddWithValue("@ConnectionString", dataSource.ConnectionStringEncrypted);
					}
					if (dataSource.OriginalConnectionStringEncrypted != null)
					{
						instrumentedSqlCommand.Parameters.AddWithValue("@OriginalConnectionString", dataSource.OriginalConnectionStringEncrypted);
					}
					if (dataSource.UserNameEncrypted != null)
					{
						instrumentedSqlCommand.Parameters.AddWithValue("@UserName", dataSource.UserNameEncrypted);
					}
					if (dataSource.PasswordEncrypted != null)
					{
						instrumentedSqlCommand.Parameters.AddWithValue("@Password", dataSource.PasswordEncrypted);
					}
					instrumentedSqlCommand.Parameters.AddWithValue("@Flags", dataSource.FlagsForCatalogSerialization);
					instrumentedSqlCommand.Parameters.AddWithValue("@OriginalConnectStringExpressionBased", dataSource.OriginalConnectStringExpressionBased);
				}
				instrumentedSqlCommand.Parameters.AddWithValue("@Version", Encryption.CurrentVersion);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (dataReader.Read())
					{
						Global.m_Tracer.Assert(dataSource.ReferenceByPath, "DBInterface.AddDataSource - read something when not expected to read");
						linkType = (ItemType)dataReader.GetInt32(0);
						linkID = dataReader.GetGuid(1);
						if (!dataReader.IsDBNull(2))
						{
							linkSecDesc = DataReaderHelper.ReadAllBytes(dataReader, 2);
						}
					}
					else
					{
						Global.m_Tracer.Assert(!dataSource.ReferenceByPath, "DBInterface.AddDataSource - expected to read something but got nothing");
					}
				}
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000C928 File Offset: 0x0000AB28
		private static byte[] ReencryptData(int originalDataVersion, byte[] originalData, string tag)
		{
			byte[] array = null;
			byte[] array2;
			try
			{
				array = CatalogEncryption.Instance.Decrypt(originalDataVersion, originalData, tag);
				array2 = CatalogEncryption.Instance.Encrypt(array, tag);
			}
			finally
			{
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = 0;
					}
					array = null;
				}
			}
			return array2;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000C980 File Offset: 0x0000AB80
		private DataSourceInfoCollection InnerGetDataSources(Guid itemID, out bool itemIDisModelID)
		{
			DataSourceInfoCollection dataSourceInfoCollection = new DataSourceInfoCollection();
			itemIDisModelID = false;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetDataSources", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", itemID);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						Guid empty = Guid.Empty;
						Guid guid = Guid.Empty;
						string text = null;
						string text2 = null;
						string text3 = null;
						string text4 = null;
						Guid guid2 = Guid.Empty;
						byte[] array = null;
						string text5 = null;
						byte[] array2 = null;
						byte[] array3 = null;
						byte[] array4 = null;
						byte[] array5 = null;
						bool flag = false;
						bool flag2 = false;
						Guid guid3 = dataReader.GetGuid(0);
						guid = dataReader.GetGuid(0);
						int num;
						int num2;
						int num3;
						if (dataReader.IsDBNull(4))
						{
							if (!dataReader.IsDBNull(2))
							{
								text2 = dataReader.GetString(2);
								text = text2;
							}
							if (!dataReader.IsDBNull(3))
							{
								text3 = dataReader.GetString(3);
							}
							guid2 = Guid.Empty;
							num = dataReader.GetInt32(5);
							if (!dataReader.IsDBNull(6))
							{
								text5 = dataReader.GetString(6);
							}
							if (!dataReader.IsDBNull(7))
							{
								array2 = DataReaderHelper.ReadAllBytes(dataReader, 7);
							}
							if (!dataReader.IsDBNull(8))
							{
								array3 = DataReaderHelper.ReadAllBytes(dataReader, 8);
							}
							if (!dataReader.IsDBNull(9))
							{
								array4 = DataReaderHelper.ReadAllBytes(dataReader, 9);
							}
							if (!dataReader.IsDBNull(10))
							{
								array5 = DataReaderHelper.ReadAllBytes(dataReader, 10);
							}
							num2 = dataReader.GetInt32(11);
							if (!dataReader.IsDBNull(25))
							{
								flag = dataReader.GetBoolean(25);
							}
							num3 = dataReader.GetInt32(26);
						}
						else
						{
							guid2 = dataReader.GetGuid(4);
							if (dataReader.IsDBNull(12))
							{
								throw new DataSourceNotFoundException(guid2.ToString());
							}
							guid = dataReader.GetGuid(12);
							if (!dataReader.IsDBNull(2))
							{
								text2 = dataReader.GetString(2);
							}
							if (!dataReader.IsDBNull(15))
							{
								text3 = dataReader.GetString(15);
							}
							num = dataReader.GetInt32(17);
							if (!dataReader.IsDBNull(18))
							{
								text5 = dataReader.GetString(18);
							}
							if (!dataReader.IsDBNull(19))
							{
								array2 = DataReaderHelper.ReadAllBytes(dataReader, 19);
							}
							if (!dataReader.IsDBNull(8))
							{
								array3 = DataReaderHelper.ReadAllBytes(dataReader, 8);
							}
							if (!dataReader.IsDBNull(20))
							{
								array4 = DataReaderHelper.ReadAllBytes(dataReader, 20);
							}
							if (!dataReader.IsDBNull(21))
							{
								array5 = DataReaderHelper.ReadAllBytes(dataReader, 21);
							}
							num2 = dataReader.GetInt32(22);
							text4 = dataReader.GetString(23);
							if (!dataReader.IsDBNull(24))
							{
								array = DataReaderHelper.ReadAllBytes(dataReader, 24);
							}
							string text6;
							CatalogItemNameUtility.SplitPath(text4, out text, out text6);
							if (!dataReader.IsDBNull(25))
							{
								flag = dataReader.GetBoolean(25);
							}
							num3 = dataReader.GetInt32(27);
							if (!dataReader.IsDBNull(28))
							{
								flag2 = true;
							}
						}
						if (!Encryption.IsCurrentVersion(num3))
						{
							array3 = DBInterface.ReencryptData(num3, array3, "OriginalConnectionString");
							array2 = DBInterface.ReencryptData(num3, array2, "ConnectionString");
							array4 = DBInterface.ReencryptData(num3, array4, "UserName");
							array5 = DBInterface.ReencryptData(num3, array5, "Password");
						}
						DataSourceInfo dataSourceInfo = new DataSourceInfo(guid3, guid, text, text2, text3, array2, array3, flag, text4, guid2, array, (DataSourceInfo.CredentialsRetrievalOption)num, text5, array4, array5, num2, flag2);
						if (!itemIDisModelID && DataSourceInfo.StaticIsModel(dataReader.GetInt32(11)) && !dataReader.IsDBNull(1) && itemID == dataReader.GetGuid(1))
						{
							itemIDisModelID = true;
						}
						dataSourceInfoCollection.Add(dataSourceInfo);
					}
				}
			}
			return dataSourceInfoCollection;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000CD1C File Offset: 0x0000AF1C
		public DataSourceInfoCollection GetDataSources(Guid itemID)
		{
			bool flag;
			DataSourceInfoCollection dataSourceInfoCollection = this.InnerGetDataSources(itemID, out flag);
			if (flag)
			{
				dataSourceInfoCollection.GetTheOnlyDataSource().InitializeAsEmbeddedInModel(itemID);
			}
			return dataSourceInfoCollection;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000CD44 File Offset: 0x0000AF44
		public DataSourceInfoCollection GetDataSources(Guid itemID, out bool itemIDisModelID)
		{
			DataSourceInfoCollection dataSourceInfoCollection = this.InnerGetDataSources(itemID, out itemIDisModelID);
			if (itemIDisModelID)
			{
				dataSourceInfoCollection.GetTheOnlyDataSource().InitializeAsEmbeddedInModel(itemID);
			}
			return dataSourceInfoCollection;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		public DataSourceInfoCollection GetDataSourcesAndResolveModelLink(Guid itemID)
		{
			bool flag;
			DataSourceInfoCollection dataSourceInfoCollection = this.InnerGetDataSources(itemID, out flag);
			if (flag)
			{
				dataSourceInfoCollection.GetTheOnlyDataSource().InitializeAsEmbeddedInModel(itemID);
			}
			else
			{
				foreach (object obj in dataSourceInfoCollection)
				{
					DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
					if (dataSourceInfo.IsModel && dataSourceInfo.ReferenceIsValid)
					{
						bool flag2;
						DataSourceInfoCollection dataSourceInfoCollection2 = this.InnerGetDataSources(dataSourceInfo.LinkID, out flag2);
						if (!flag2)
						{
							throw new InternalCatalogException("Found invalid model data source structure.");
						}
						DataSourceInfo theOnlyDataSource = dataSourceInfoCollection2.GetTheOnlyDataSource();
						dataSourceInfo.LinkModelToDataSource(theOnlyDataSource, dataSourceInfo.LinkID);
					}
				}
			}
			return dataSourceInfoCollection;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000CE1C File Offset: 0x0000B01C
		public bool HasDataModelDataSources(Guid itemId)
		{
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetDataModelDataSourcesByItemID", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", itemId);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					flag = dataReader.Read();
				}
			}
			return flag;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000CE90 File Offset: 0x0000B090
		public void ChangeStateOfDataSource(Guid itemID, bool enable)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ChangeStateOfDataSource", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", itemID);
				instrumentedSqlCommand.Parameters.AddWithValue("@Enable", enable);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000CEFC File Offset: 0x0000B0FC
		public void DeleteDataSources(Guid itemID)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteDataSources", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", itemID);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000CF50 File Offset: 0x0000B150
		public byte[] GetDataSourcePasswordForSubscription(Guid subscriptionId)
		{
			string text = "select Password from DataSource where SubscriptionId = @id";
			byte[] array;
			using (InstrumentedSqlCommand instrumentedSqlCommand = base.NewStandardSqlCommandQuery(text))
			{
				instrumentedSqlCommand.Parameters.Add("@id", SqlDbType.UniqueIdentifier);
				instrumentedSqlCommand.Parameters["@id"].Value = subscriptionId;
				array = (byte[])instrumentedSqlCommand.ExecuteScalar();
			}
			return array;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000CFC4 File Offset: 0x0000B1C4
		public Guid GetRootItemId()
		{
			Guid guid = Guid.Empty;
			string text = "select ItemID from catalog where ParentID is null";
			Guid guid2;
			using (InstrumentedSqlCommand instrumentedSqlCommand = base.NewStandardSqlCommandQuery(text))
			{
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (dataReader.Read())
					{
						guid = dataReader.GetGuid(0);
					}
					guid2 = guid;
				}
			}
			return guid2;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000D034 File Offset: 0x0000B234
		public DataSetInfoCollection GetSharedDataSets(Guid itemID)
		{
			DataSetInfoCollection dataSetInfoCollection = new DataSetInfoCollection();
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetDataSets", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", itemID);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						Guid guid = dataReader.GetGuid(0);
						Guid guid2 = ((!dataReader.IsDBNull(3)) ? dataReader.GetGuid(1) : Guid.Empty);
						string @string = dataReader.GetString(2);
						string text = ((!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : null);
						byte[] array = null;
						if (!dataReader.IsDBNull(4))
						{
							array = DataReaderHelper.ReadAllBytes(dataReader, 4);
						}
						Guid guid3 = Guid.Empty;
						if (!dataReader.IsDBNull(5))
						{
							guid3 = dataReader.GetGuid(5);
						}
						string text2 = ((!dataReader.IsDBNull(6)) ? dataReader.GetString(6) : null);
						DataSetInfo dataSetInfo = new DataSetInfo(guid, guid2, @string, text, array, guid3, text2);
						dataSetInfoCollection.Add(dataSetInfo);
					}
				}
			}
			return dataSetInfoCollection;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000D170 File Offset: 0x0000B370
		public void AddDataSet(Guid itemID, DataSetInfo dataSet, IPathTranslator pathTranslator, string editSessionID, out Guid linkID, out byte[] linkSecDesc)
		{
			linkID = Guid.Empty;
			linkSecDesc = null;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("AddDataSet", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				instrumentedSqlCommand.Parameters.AddWithValue("@ID", dataSet.ID);
				if (!string.IsNullOrEmpty(editSessionID))
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@EditSessionID", editSessionID);
				}
				instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", itemID);
				instrumentedSqlCommand.AddParameter("@Name", SqlDbType.NVarChar, dataSet.DataSetName);
				if (dataSet.LinkedSharedDataSetID != Guid.Empty)
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@LinkID", dataSet.LinkedSharedDataSetID);
				}
				else if (dataSet.AbsolutePath != null)
				{
					instrumentedSqlCommand.AddParameter("@LinkPath", SqlDbType.NVarChar, pathTranslator.ExternalToCatalog(dataSet.AbsolutePath));
				}
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (dataReader.Read())
					{
						Global.m_Tracer.Assert(dataSet.LinkedSharedDataSetID == Guid.Empty, "DBInterface.AddDataSet - read something when not expected to read");
						linkID = dataReader.GetGuid(0);
						if (!dataReader.IsDBNull(1))
						{
							linkSecDesc = DataReaderHelper.ReadAllBytes(dataReader, 1);
						}
					}
				}
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000D30C File Offset: 0x0000B50C
		public void DeleteDataSets(Guid itemID)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteDataSets", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ItemID", itemID);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000D360 File Offset: 0x0000B560
		public void AddExecutionLogEntry(string instanceName, CatalogItemPath report, DBInterface.RequestType requestType, string format, string parameters, DateTime timeStart, DateTime timeEnd, int percentDataRetrieval, int percentProcessing, int percentRendering, ExecutionLogExecType source, string status, long byteCount, long rowCount, string executionId, byte reportAction, string additionalInfo)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("AddExecutionLogEntry", null))
			{
				instrumentedSqlCommand.AddParameter("@InstanceName", SqlDbType.NVarChar, instanceName);
				instrumentedSqlCommand.AddParameter("@Report", SqlDbType.NVarChar, report.Value);
				instrumentedSqlCommand.Parameters.AddWithValue("@UserSid", Global.NameToSid(this.UserContext));
				string text = (Globals.IsServiceProcess ? UserUtil.GetWindowsIdentityName() : this.m_userContext.UserName);
				if (Globals.CurrentApplication == RunningApplication.ReportServerWebApp)
				{
					text = this.m_userContext.UserName;
				}
				instrumentedSqlCommand.AddParameter("@UserName", SqlDbType.NVarChar, text);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.UserContext.AuthenticationType);
				instrumentedSqlCommand.Parameters.AddWithValue("@RequestType", (byte)requestType);
				if (format == null)
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@Format", DBNull.Value);
				}
				else
				{
					instrumentedSqlCommand.AddParameter("@Format", SqlDbType.NVarChar, format);
				}
				if (parameters == null)
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@Parameters", DBNull.Value);
				}
				else
				{
					instrumentedSqlCommand.AddParameter("@Parameters", SqlDbType.NText, parameters);
				}
				instrumentedSqlCommand.Parameters.AddWithValue("@TimeStart", timeStart);
				instrumentedSqlCommand.Parameters.AddWithValue("@TimeEnd", timeEnd);
				instrumentedSqlCommand.Parameters.AddWithValue("@TimeDataRetrieval", percentDataRetrieval);
				instrumentedSqlCommand.Parameters.AddWithValue("@TimeProcessing", percentProcessing);
				instrumentedSqlCommand.Parameters.AddWithValue("@TimeRendering", percentRendering);
				instrumentedSqlCommand.Parameters.AddWithValue("@Source", (int)source);
				instrumentedSqlCommand.AddParameter("@Status", SqlDbType.NVarChar, status);
				instrumentedSqlCommand.Parameters.AddWithValue("@ByteCount", byteCount);
				instrumentedSqlCommand.Parameters.AddWithValue("@RowCount", rowCount);
				if (executionId == null)
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@ExecutionId", DBNull.Value);
				}
				else
				{
					instrumentedSqlCommand.AddParameter("@ExecutionId", SqlDbType.NVarChar, executionId);
				}
				instrumentedSqlCommand.Parameters.AddWithValue("@ReportAction", reportAction);
				instrumentedSqlCommand.Parameters.Add("@AdditionalInfo", SqlDbType.Xml).Value = ((additionalInfo != null) ? additionalInfo : DBNull.Value);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000D5F0 File Offset: 0x0000B7F0
		public int ExpireExecutionLogEntries()
		{
			int num;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ExpireExecutionLogEntries", null))
			{
				instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCleanupTimeoutSeconds;
				num = instrumentedSqlCommand.ExecuteNonQuery();
			}
			return num;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000D63C File Offset: 0x0000B83C
		public void SetDrillthroughReport(Guid reportId, Guid modelId, string entityId, short drillType)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetDrillthroughReports", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ReportID", reportId);
				instrumentedSqlCommand.Parameters.AddWithValue("@ModelID", modelId);
				instrumentedSqlCommand.Parameters.AddWithValue("@ModelItemID", entityId);
				instrumentedSqlCommand.Parameters.AddWithValue("@Type", drillType);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		public void DeleteDrillthroughReport(Guid modelId, string entityId)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteDrillthroughReports", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ModelID", modelId);
				instrumentedSqlCommand.Parameters.AddWithValue("@ModelItemID", entityId);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000D73C File Offset: 0x0000B93C
		public ModelDrillthroughReport[] GetDrillthroughReports(Guid modelId, string entityId, IPathTranslator pathTranslator)
		{
			ModelDrillthroughReport[] array;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetDrillthroughReports", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ModelID", modelId);
				instrumentedSqlCommand.Parameters.AddWithValue("@ModelItemID", entityId);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					List<ModelDrillthroughReport> list = new List<ModelDrillthroughReport>();
					while (dataReader.Read())
					{
						ModelDrillthroughReport modelDrillthroughReport = new ModelDrillthroughReport();
						modelDrillthroughReport.Type = (DrillthroughType)dataReader.GetByte(0);
						CatalogItemPath catalogItemPath = new CatalogItemPath(dataReader.GetString(1));
						modelDrillthroughReport.Path = pathTranslator.CatalogToExternal(catalogItemPath).Value;
						list.Add(modelDrillthroughReport);
					}
					array = list.ToArray();
				}
			}
			return array;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000D810 File Offset: 0x0000BA10
		public CatalogItemPath GetDrillThroughReport(CatalogItemPath modelPath, string entityId, short drillType)
		{
			CatalogItemPath catalogItemPath = null;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetDrillthroughReport", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ModelPath", modelPath.Value);
				instrumentedSqlCommand.Parameters.AddWithValue("@ModelItemID", entityId);
				instrumentedSqlCommand.Parameters.AddWithValue("@Type", drillType);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (dataReader.Read())
					{
						catalogItemPath = new CatalogItemPath(dataReader.GetString(0));
					}
				}
			}
			return catalogItemPath;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000D8BC File Offset: 0x0000BABC
		public int CleanExpiredEditSessions()
		{
			int num;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CleanExpiredEditSessions", null))
				{
					SqlParameter sqlParameter = instrumentedSqlCommand.Parameters.Add("@NumCleaned", SqlDbType.Int);
					sqlParameter.Direction = ParameterDirection.Output;
					instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCleanupTimeoutSeconds;
					instrumentedSqlCommand.ExecuteNonQuery();
					num = (int)sqlParameter.Value;
				}
			}
			catch (Exception ex)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in CleanExpiredEditSessions: {0}", new object[] { ex });
				if (!(ex is RSException))
				{
					throw;
				}
				num = 0;
			}
			return num;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000D960 File Offset: 0x0000BB60
		public static int ReadInt64AsInt32(IDataRecord record, int columnIndex)
		{
			long @int = record.GetInt64(columnIndex);
			return (int)Math.Min(2147483647L, @int);
		}

		// Token: 0x0400010E RID: 270
		private SchedulingDBInterface m_scheduleDB = new SchedulingDBInterface();

		// Token: 0x0400010F RID: 271
		private UserContext m_userContext;

		// Token: 0x04000110 RID: 272
		private const string getReportForExecutionQuery = "\r\nDECLARE @OwnerID uniqueidentifier\r\nif(@EditSessionID is not null)\r\nBEGIN\r\n    EXEC GetUserID @OwnerSid, @OwnerName, @AuthType, @OwnerID OUTPUT\r\nEND\r\n\r\nDECLARE @now AS datetime\r\nSET @now = GETDATE()\r\n\r\nIF ( NOT EXISTS (\r\n    SELECT TOP 1 1\r\n        FROM\r\n            ExtendedCatalog(@OwnerID, @Path, @EditSessionID) AS C\r\n            INNER JOIN {0}.dbo.ExecutionCache AS EC ON C.ItemID = EC.ReportID\r\n\t\t\tINNER JOIN {0}.dbo.SnapshotData AS SN ON EC.SnapshotDataID = SN.SnapshotDataID AND EC.ParamsHash = SN.ParamsHash\r\n        WHERE\r\n            EC.AbsoluteExpiration > @now AND\r\n            EC.ParamsHash = @ParamsHash AND\r\n            SN.QueryParams LIKE @QueryParams\r\n   ) )\r\nBEGIN   -- no cache\r\n    SELECT\r\n        Cat.Type,\r\n        Cat.LinkSourceID,\r\n        Cat2.Path,\r\n        Cat.Property,\r\n        Cat.Description,\r\n        SecData.NtSecDescPrimary,\r\n        Cat.ItemID,\r\n        CAST (0 AS BIT), -- not found,\r\n        Cat.Intermediate,\r\n        Cat.ExecutionFlag,\r\n        SD.SnapshotDataID,\r\n        SD.DependsOnUser,\r\n        Cat.ExecutionTime,\r\n        (SELECT Schedule.NextRunTime\r\n         FROM\r\n             Schedule WITH (XLOCK)\r\n             INNER JOIN ReportSchedule ON Schedule.ScheduleID = ReportSchedule.ScheduleID\r\n         WHERE ReportSchedule.ReportID = Cat.ItemID AND ReportSchedule.ReportAction = 1), -- update snapshot\r\n        (SELECT Schedule.ScheduleID\r\n         FROM\r\n             Schedule\r\n             INNER JOIN ReportSchedule ON Schedule.ScheduleID = ReportSchedule.ScheduleID\r\n         WHERE ReportSchedule.ReportID = Cat.ItemID AND ReportSchedule.ReportAction = 1), -- update snapshot\r\n        (SELECT CachePolicy.ExpirationFlags FROM CachePolicy WHERE CachePolicy.ReportID = Cat.ItemID),\r\n        Cat2.Intermediate,\r\n        SD.ProcessingFlags,\r\n        Cat.IntermediateIsPermanent\r\n    FROM\r\n        ExtendedCatalog(@OwnerID, @Path, @EditSessionID) AS Cat\r\n        LEFT OUTER JOIN SecData ON Cat.PolicyID = SecData.PolicyID AND SecData.AuthType = @AuthType\r\n        LEFT OUTER JOIN Catalog AS Cat2 on Cat.LinkSourceID = Cat2.ItemID\r\n        LEFT OUTER JOIN SnapshotData AS SD ON Cat.SnapshotDataID = SD.SnapshotDataID\r\nEND\r\nELSE\r\nBEGIN   -- use cache\r\n    SELECT TOP 1\r\n        Cat.Type,\r\n        Cat.LinkSourceID,\r\n        Cat2.Path,\r\n        Cat.Property,\r\n        Cat.Description,\r\n        SecData.NtSecDescPrimary,\r\n        Cat.ItemID,\r\n        CAST (1 AS BIT), -- found,\r\n        SN.SnapshotDataID,\r\n        SN.DependsOnUser,\r\n        SN.EffectiveParams,  -- offset 10\r\n        SN.CreatedDate,\r\n        EC.AbsoluteExpiration,\r\n        (SELECT CachePolicy.ExpirationFlags FROM CachePolicy WHERE CachePolicy.ReportID = Cat.ItemID),\r\n        (SELECT Schedule.ScheduleID\r\n         FROM\r\n             Schedule WITH (XLOCK)\r\n             INNER JOIN ReportSchedule ON Schedule.ScheduleID = ReportSchedule.ScheduleID\r\n             WHERE ReportSchedule.ReportID = Cat.ItemID AND ReportSchedule.ReportAction = 1), -- update snapshot\r\n        SN.QueryParams,  -- offset 15\r\n        SN.ProcessingFlags,\r\n        Cat.IntermediateIsPermanent,\r\n        Cat.Intermediate\r\n    FROM\r\n        ExtendedCatalog(@OwnerID, @Path, @EditSessionID) AS Cat\r\n        INNER JOIN {0}.dbo.ExecutionCache AS EC ON Cat.ItemID = EC.ReportID\r\n        INNER JOIN {0}.dbo.SnapshotData AS SN ON EC.SnapshotDataID = SN.SnapshotDataID AND EC.ParamsHash = SN.ParamsHash\r\n        LEFT OUTER JOIN SecData ON Cat.PolicyID = SecData.PolicyID AND SecData.AuthType = @AuthType\r\n        LEFT OUTER JOIN Catalog AS Cat2 on Cat.LinkSourceID = Cat2.ItemID\r\n    WHERE\r\n        AbsoluteExpiration > @now AND\r\n        SN.ParamsHash = @ParamsHash AND\r\n        SN.QueryParams LIKE @QueryParams\r\n    ORDER BY SN.CreatedDate DESC\r\nEND";

		// Token: 0x04000111 RID: 273
		private static string s_getReportForExecutionQuery;

		// Token: 0x02000438 RID: 1080
		public enum ContentCacheTypes
		{
			// Token: 0x04000F17 RID: 3863
			SharedDataSetJson
		}

		// Token: 0x02000439 RID: 1081
		public enum RequestType
		{
			// Token: 0x04000F19 RID: 3865
			Interactive,
			// Token: 0x04000F1A RID: 3866
			Subscription,
			// Token: 0x04000F1B RID: 3867
			RefreshCache
		}

		// Token: 0x0200043A RID: 1082
		public static class FindItemColumns
		{
			// Token: 0x04000F1C RID: 3868
			public static readonly int Type = 0;

			// Token: 0x04000F1D RID: 3869
			public static readonly int PolicyID = 1;

			// Token: 0x04000F1E RID: 3870
			public static readonly int SecurityDescriptor = 2;

			// Token: 0x04000F1F RID: 3871
			public static readonly int Name = 3;

			// Token: 0x04000F20 RID: 3872
			public static readonly int Path = 4;

			// Token: 0x04000F21 RID: 3873
			public static readonly int ItemID = 5;

			// Token: 0x04000F22 RID: 3874
			public static readonly int Size = 6;

			// Token: 0x04000F23 RID: 3875
			public static readonly int Description = 7;

			// Token: 0x04000F24 RID: 3876
			public static readonly int CreationDate = 8;

			// Token: 0x04000F25 RID: 3877
			public static readonly int ModifiedDate = 9;

			// Token: 0x04000F26 RID: 3878
			public static readonly int CreatedBySid = 10;

			// Token: 0x04000F27 RID: 3879
			public static readonly int CreatedByBackup = 11;

			// Token: 0x04000F28 RID: 3880
			public static readonly int ModifiedBySid = 12;

			// Token: 0x04000F29 RID: 3881
			public static readonly int ModifiedByBackup = 13;

			// Token: 0x04000F2A RID: 3882
			public static readonly int MimeType = 14;

			// Token: 0x04000F2B RID: 3883
			public static readonly int ExecutionDate = 15;

			// Token: 0x04000F2C RID: 3884
			public static readonly int Hidden = 16;

			// Token: 0x04000F2D RID: 3885
			public static readonly int SubType = 17;

			// Token: 0x04000F2E RID: 3886
			public static readonly int ComponentID = 18;

			// Token: 0x04000F2F RID: 3887
			public static readonly int IsFavorite = 19;
		}
	}
}
