using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200028C RID: 652
	internal class ChunkStorage : DBInterface
	{
		// Token: 0x060017B7 RID: 6071 RVA: 0x0002D2D5 File Offset: 0x0002B4D5
		internal ChunkStorage()
		{
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0005FBE8 File Offset: 0x0005DDE8
		internal void IncreaseTransientSnapshotRefcount(Guid snapshotDataID, bool isPermanentSnapshot, int expirationMinutes)
		{
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("IncreaseTransientSnapshotRefcount"))
			{
				cancelableSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier).Value = snapshotDataID;
				cancelableSqlCommand.Parameters.Add("@ExpirationMinutes", SqlDbType.Int).Value = expirationMinutes;
				cancelableSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanentSnapshot;
				if (cancelableSqlCommand.ExecuteNonQuery() != 1)
				{
					throw new InternalCatalogException("IncreaseTransientSnapshotRefcount didn't find snapshot in the database!");
				}
			}
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0005FC88 File Offset: 0x0005DE88
		internal void DecreaseTransientSnapshotRefcount(Guid snapshotDataID, bool isPermanentSnapshot)
		{
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("DecreaseTransientSnapshotRefcount"))
			{
				cancelableSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier).Value = snapshotDataID;
				cancelableSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanentSnapshot;
				if (cancelableSqlCommand.ExecuteNonQuery() != 1)
				{
					throw new InternalCatalogException("DecreaseTransientSnapshotRefcount didn't find snapshot in the database!");
				}
			}
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0005FD0C File Offset: 0x0005DF0C
		internal void MarkSnapshotAsDependentOnUser(Guid snapshotDataID, bool isPermanentSnapshot)
		{
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("MarkSnapshotAsDependentOnUser"))
			{
				cancelableSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier).Value = snapshotDataID;
				cancelableSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanentSnapshot;
				if (cancelableSqlCommand.ExecuteNonQuery() != 1)
				{
					throw new InternalCatalogException("MarkSnapshotAsDependentOnUser didn't find snapshot in the database!");
				}
			}
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0005FD90 File Offset: 0x0005DF90
		internal void MarkSnapshotChunksAsUpdated(Guid snapshotDataID, bool isPermanentSnapshot, short version)
		{
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("SetSnapshotChunksVersion"))
			{
				cancelableSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier).Value = snapshotDataID;
				cancelableSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanentSnapshot;
				cancelableSqlCommand.Parameters.Add("@Version", SqlDbType.SmallInt).Value = version;
				if ((int)cancelableSqlCommand.ExecuteScalar() <= 0)
				{
					throw new InternalCatalogException("MarkSnapshotChunksAsUpdated didn't find snapshot in the database!");
				}
			}
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0005FE34 File Offset: 0x0005E034
		internal void WriteNewSnapshotToDB(ParameterInfoCollection parameters, DateTime createdDate, string description, Guid snapshotDataID, bool isPermanentSnapshot, int flags)
		{
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("InsertUnreferencedSnapshot"))
			{
				if (parameters != null)
				{
					string text = parameters.ToXml(true);
					string text2 = parameters.ToXml(false);
					cancelableSqlCommand.Parameters.Add("@EffectiveParams", SqlDbType.NText).Value = text2;
					cancelableSqlCommand.Parameters.Add("@QueryParams", SqlDbType.NText).Value = text;
					cancelableSqlCommand.Parameters.Add("@ParamsHash", SqlDbType.Int).Value = StackTraceUtil.GetInvariantHashCode<char>(text);
				}
				cancelableSqlCommand.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = createdDate;
				if (description != null)
				{
					cancelableSqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = description;
				}
				cancelableSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier).Value = snapshotDataID;
				cancelableSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanentSnapshot;
				cancelableSqlCommand.Parameters.Add("@ProcessingFlags", SqlDbType.Int).Value = flags;
				cancelableSqlCommand.Parameters.Add("@SnapshotTimeoutMinutes", SqlDbType.Int).Value = 1440;
				cancelableSqlCommand.Parameters.Add("@Machine", SqlDbType.NVarChar).Value = Environment.MachineName;
				cancelableSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0005FFAC File Offset: 0x0005E1AC
		internal void CopyImages(Guid fromSnapshotID, bool fromIsPermanent, Guid toSnapshotID, bool toIsPermanent, int imageChunkType)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### CopyImages({0},{1})", new object[] { fromSnapshotID, toSnapshotID });
			}
			this.CopyChunks(fromSnapshotID, fromIsPermanent, toSnapshotID, toIsPermanent, new int?(imageChunkType), null, null);
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00060004 File Offset: 0x0005E204
		internal void CopyChunks(Guid fromSnapshotID, bool fromIsPermanent, Guid toSnapshotID, bool toIsPermanent, int? chunkType, string sourceChunkName, string targetChunkName)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### CopyChunks ({0}, {1})", new object[] { fromSnapshotID, toSnapshotID });
			}
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("CopyChunksOfType"))
			{
				cancelableSqlCommand.Parameters.Add("@FromSnapshotID", SqlDbType.UniqueIdentifier).Value = fromSnapshotID;
				cancelableSqlCommand.Parameters.Add("@FromIsPermanent", SqlDbType.Bit).Value = fromIsPermanent;
				cancelableSqlCommand.Parameters.Add("@ToSnapshotID", SqlDbType.UniqueIdentifier).Value = toSnapshotID;
				cancelableSqlCommand.Parameters.Add("@ToIsPermanent", SqlDbType.Bit).Value = toIsPermanent;
				if (chunkType != null)
				{
					cancelableSqlCommand.Parameters.Add("@ChunkType", SqlDbType.Int).Value = chunkType.Value;
				}
				if (sourceChunkName != null)
				{
					cancelableSqlCommand.Parameters.Add("@ChunkName", SqlDbType.NVarChar).Value = sourceChunkName;
				}
				if (targetChunkName != null)
				{
					cancelableSqlCommand.Parameters.Add("@TargetChunkName", SqlDbType.NVarChar).Value = targetChunkName;
				}
				cancelableSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0006014C File Offset: 0x0005E34C
		internal void DeleteSnapshotAndChunks(Guid snapshotID, bool isPermanentSnapshot)
		{
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("DeleteSnapshotAndChunks"))
			{
				cancelableSqlCommand.Parameters.Add("@SnapshotID", SqlDbType.UniqueIdentifier).Value = snapshotID;
				cancelableSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanentSnapshot;
				cancelableSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x000601C4 File Offset: 0x0005E3C4
		internal bool DeleteOneChunk(Guid snapshotID, bool isPermanentSnapshot, string chunkName, int chunkType)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### DeleteOneChunk({0},'{1}',{2})", new object[] { snapshotID, chunkName, chunkType });
			}
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteOneChunk", null))
			{
				instrumentedSqlCommand.Parameters.Add("@SnapshotID", SqlDbType.UniqueIdentifier).Value = snapshotID;
				instrumentedSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanentSnapshot;
				instrumentedSqlCommand.Parameters.Add("@ChunkName", SqlDbType.NVarChar, 260).Value = chunkName;
				instrumentedSqlCommand.Parameters.Add("@ChunkType", SqlDbType.Int).Value = chunkType;
				flag = instrumentedSqlCommand.ExecuteNonQuery() > 0;
			}
			return flag;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x000602B4 File Offset: 0x0005E4B4
		internal void CreateChunk(Guid snapshotDataID, bool isPermanentSnapshot, string chunkName, int chunkType, string mimeType, ChunkFlags chunkFlags, short version, byte[] buffer, int offset, int length, out object chunkPointer)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### CreateChunk({0}, {1}, {2}, |{3}|, ofs {4}, L {5})", new object[] { snapshotDataID, chunkName, chunkType, buffer.Length, offset, length });
			}
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("CreateChunkAndGetPointer"))
			{
				cancelableSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier).Value = snapshotDataID;
				cancelableSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanentSnapshot;
				cancelableSqlCommand.Parameters.Add("@ChunkName", SqlDbType.NVarChar, 260).Value = chunkName;
				cancelableSqlCommand.Parameters.Add("@ChunkType", SqlDbType.Int).Value = chunkType;
				if (mimeType != null)
				{
					cancelableSqlCommand.Parameters.Add("@MimeType", SqlDbType.NVarChar, 260).Value = mimeType;
				}
				cancelableSqlCommand.Parameters.Add("@Version", SqlDbType.SmallInt).Value = version;
				cancelableSqlCommand.Parameters.Add("@ChunkFlags", SqlDbType.TinyInt).Value = (byte)chunkFlags;
				SqlParameter sqlParameter = cancelableSqlCommand.Parameters.Add("@Content", SqlDbType.Image);
				sqlParameter.Value = buffer;
				sqlParameter.Size = length;
				sqlParameter.Offset = offset;
				SqlParameter sqlParameter2 = cancelableSqlCommand.Parameters.Add("@ChunkPointer", SqlDbType.Binary, 16);
				sqlParameter2.Direction = ParameterDirection.Output;
				ConnectionState state = cancelableSqlCommand.Connection.State;
				if (state != ConnectionState.Open)
				{
					throw new ReportServerDatabaseUnavailableException("WriteChunkPortion - connection state = " + state.ToString());
				}
				try
				{
					using (IDataReader dataReader = cancelableSqlCommand.ExecuteReader())
					{
						if (dataReader.Read())
						{
							throw new InternalCatalogException("WriteChunkPortion - reader had something to read");
						}
						if (dataReader.NextResult())
						{
							throw new InternalCatalogException("WriteChunkPortion - reader has next result");
						}
					}
				}
				catch (Exception ex)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Error, "CreateChunkAndGetPointer: {0}", new object[] { ex.ToString() });
					throw;
				}
				chunkPointer = sqlParameter2.Value;
				if (chunkPointer == null)
				{
					throw new InternalCatalogException("CreateChunkAndGetPointer - null chunk pointer!");
				}
			}
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0006053C File Offset: 0x0005E73C
		internal void WriteChunkPortion(object chunkPointer, bool isPermanentSnapshot, long dataIndex, byte[] buffer, int offset, int length, ChunkStorage.WriteMode writeMode, long chunkLength)
		{
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("WriteChunkPortion"))
			{
				cancelableSqlCommand.Parameters.Add("@ChunkPointer", SqlDbType.Binary, 16).Value = chunkPointer;
				if (chunkPointer == null)
				{
					throw new InternalCatalogException("Chunk pointer is null on Write Chunk Portion.");
				}
				cancelableSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanentSnapshot;
				if (writeMode == ChunkStorage.WriteMode.Overwrite)
				{
					cancelableSqlCommand.Parameters.Add("@DataIndex", SqlDbType.Int).Value = dataIndex;
					cancelableSqlCommand.Parameters.Add("@DeleteLength", SqlDbType.Int).Value = Math.Min(length, (int)(chunkLength - dataIndex));
				}
				SqlParameter sqlParameter = cancelableSqlCommand.Parameters.Add("@Content", SqlDbType.Image);
				sqlParameter.Value = buffer;
				sqlParameter.Size = length;
				sqlParameter.Offset = offset;
				ConnectionState state = cancelableSqlCommand.Connection.State;
				if (state != ConnectionState.Open)
				{
					throw new ReportServerDatabaseUnavailableException("WriteChunkPortion - connection state = " + state.ToString());
				}
				try
				{
					using (IDataReader dataReader = cancelableSqlCommand.ExecuteReader())
					{
						if (dataReader.Read())
						{
							throw new InternalCatalogException("WriteChunkPortion - reader had something to read");
						}
						if (dataReader.NextResult())
						{
							throw new InternalCatalogException("WriteChunkPortion - reader has next result");
						}
					}
				}
				catch (Exception ex)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Error, "WriteChunkPortion: {0}", new object[] { ex.ToString() });
					throw;
				}
			}
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x000606EC File Offset: 0x0005E8EC
		internal bool GetChunkPointerAndLength(Guid snapshotDataID, bool isPermanentSnapshot, string chunkName, int chunkType, out object chunkPointer, out long chunkLength, out string mimeType, out ChunkFlags chunkFlags, out short version)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### GetChunkPointerAndLength({0}, {1}, {2})", new object[] { snapshotDataID, chunkName, chunkType });
			}
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("GetChunkPointerAndLength"))
			{
				cancelableSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier).Value = snapshotDataID;
				cancelableSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanentSnapshot;
				cancelableSqlCommand.Parameters.Add("@ChunkName", SqlDbType.NVarChar, 260).Value = chunkName;
				cancelableSqlCommand.Parameters.Add("@ChunkType", SqlDbType.Int).Value = chunkType;
				ConnectionState state = cancelableSqlCommand.Connection.State;
				if (state != ConnectionState.Open)
				{
					throw new ReportServerDatabaseUnavailableException("GetChunkPointerAndLength - connection state = " + state.ToString());
				}
				chunkPointer = null;
				chunkLength = 0L;
				mimeType = null;
				chunkFlags = ChunkFlags.None;
				version = ChunkHeader.MissingVersion;
				try
				{
					using (IDataReader dataReader = cancelableSqlCommand.ExecuteReader())
					{
						if (!dataReader.Read())
						{
							return false;
						}
						chunkPointer = dataReader.GetValue(0);
						chunkLength = (long)dataReader.GetInt32(1);
						if (!dataReader.IsDBNull(2))
						{
							mimeType = dataReader.GetString(2);
						}
						if (!dataReader.IsDBNull(3))
						{
							chunkFlags = (ChunkFlags)dataReader.GetByte(3);
						}
						if (!dataReader.IsDBNull(4))
						{
							version = dataReader.GetInt16(4);
						}
						if (dataReader.Read())
						{
							throw new InternalCatalogException("GetChunkPointerAndLength - reader had something to read");
						}
						if (dataReader.NextResult())
						{
							throw new InternalCatalogException("GetChunkPointerAndLength - reader has next result");
						}
					}
				}
				catch (Exception ex)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Error, "GetChunkPointerAndLength: {0}", new object[] { ex.ToString() });
				}
			}
			return true;
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0006090C File Offset: 0x0005EB0C
		internal void TruncateChunk(object chunkPointer, bool isPermanentSnapshot, long length)
		{
			if (Global.m_Tracer.TraceVerbose)
			{
				Global.m_Tracer.Trace(TraceLevel.Verbose, "### TruncateChunk()");
			}
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("WriteChunkPortion"))
			{
				cancelableSqlCommand.Parameters.Add("@ChunkPointer", SqlDbType.Binary, 16).Value = chunkPointer;
				if (chunkPointer == null)
				{
					throw new InternalCatalogException("Chunk pointer is null on Truncate.");
				}
				cancelableSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanentSnapshot;
				cancelableSqlCommand.Parameters.Add("@DataIndex", SqlDbType.Int).Value = length;
				cancelableSqlCommand.Parameters.Add("@Content", SqlDbType.Image).Value = DBNull.Value;
				ConnectionState state = cancelableSqlCommand.Connection.State;
				if (state != ConnectionState.Open)
				{
					throw new ReportServerDatabaseUnavailableException("TruncateChunk - connection state = " + state.ToString());
				}
				try
				{
					cancelableSqlCommand.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Error, "TruncateChunk: {0}", new object[] { ex.ToString() });
					throw;
				}
			}
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00060A3C File Offset: 0x0005EC3C
		internal bool ReadChunkPortion(object chunkPointer, bool isPermanentSnapshot, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("ReadChunkPortion"))
			{
				cancelableSqlCommand.Parameters.Add("@ChunkPointer", SqlDbType.Binary, 16).Value = chunkPointer;
				cancelableSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanentSnapshot;
				cancelableSqlCommand.Parameters.Add("@DataIndex", SqlDbType.Int).Value = dataIndex;
				cancelableSqlCommand.Parameters.Add("@Length", SqlDbType.Int).Value = length;
				if (cancelableSqlCommand.Connection.State != ConnectionState.Open)
				{
					return false;
				}
				try
				{
					using (IDataReader dataReader = cancelableSqlCommand.ExecuteReader())
					{
						if (!dataReader.Read())
						{
							return false;
						}
						dataReader.GetBytes(0, 0L, buffer, bufferIndex, length);
						if (dataReader.Read())
						{
							throw new InternalCatalogException("ReadChunkPortion - reader had something to read");
						}
						if (dataReader.NextResult())
						{
							throw new InternalCatalogException("ReadChunkPortion - reader has next result");
						}
					}
				}
				catch (Exception ex)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Error, "ReadChunkPortion: {0}", new object[] { ex.ToString() });
					throw;
				}
			}
			return true;
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00060B88 File Offset: 0x0005ED88
		internal string GetChunkInformation(Guid snapshotDataID, bool isPermanentSnapshot, string chunkName, int chunkType)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### GetChunkInformation({0}, {1}, {2})", new object[] { snapshotDataID, chunkName, chunkType });
			}
			string text2;
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("GetChunkInformation"))
			{
				cancelableSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier).Value = snapshotDataID;
				cancelableSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanentSnapshot;
				cancelableSqlCommand.Parameters.Add("@ChunkName", SqlDbType.NVarChar, 260).Value = chunkName;
				cancelableSqlCommand.Parameters.Add("@ChunkType", SqlDbType.Int).Value = chunkType;
				string text = null;
				ConnectionState state = cancelableSqlCommand.Connection.State;
				if (state != ConnectionState.Open)
				{
					throw new ReportServerDatabaseUnavailableException("GetChunkInformation - connection state = " + state.ToString());
				}
				try
				{
					using (IDataReader dataReader = cancelableSqlCommand.ExecuteReader())
					{
						if (!dataReader.Read())
						{
							return null;
						}
						text = dataReader.GetString(0);
						if (dataReader.Read())
						{
							throw new InternalCatalogException("GetChunkInformation - reader had something to read");
						}
						if (dataReader.NextResult())
						{
							throw new InternalCatalogException("GetChunkInformation - reader has next result");
						}
					}
				}
				catch (Exception ex)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Error, "GetChunkInformation: {0}", new object[] { ex.ToString() });
					throw;
				}
				text2 = text;
			}
			return text2;
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00060D50 File Offset: 0x0005EF50
		internal bool LockSnapshotForUpgrade(Guid snapshotDataID, bool isPermanentSnapshot)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### LockSnapshotForUpgrade({0}, {1})", new object[] { snapshotDataID, isPermanentSnapshot });
			}
			bool flag;
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("LockSnapshotForUpgrade"))
			{
				cancelableSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier).Value = snapshotDataID;
				cancelableSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanentSnapshot;
				ConnectionState state = cancelableSqlCommand.Connection.State;
				if (state != ConnectionState.Open)
				{
					throw new ReportServerDatabaseUnavailableException("LockSnapshotForUpgrade - connection state = " + state.ToString());
				}
				int num = 0;
				try
				{
					using (IDataReader dataReader = cancelableSqlCommand.ExecuteReader())
					{
						while (dataReader.Read())
						{
							dataReader.GetString(0);
							num++;
						}
						flag = num > 0;
					}
				}
				catch (Exception ex)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Error, "LockSnapshotForUpgrade: {0}", new object[] { ex.ToString() });
					throw;
				}
			}
			return flag;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00060E8C File Offset: 0x0005F08C
		internal IList<ChunkHeader> GetChunksForSnapshot(Guid snapshotDataID, bool isPermanentSnapshot)
		{
			List<ChunkHeader> list = new List<ChunkHeader>();
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### GetChunksForSnapshot({0}, {1})", new object[] { snapshotDataID, isPermanentSnapshot });
			}
			IList<ChunkHeader> list2;
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("GetSnapshotChunks"))
			{
				cancelableSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier).Value = snapshotDataID;
				cancelableSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanentSnapshot;
				ConnectionState state = cancelableSqlCommand.Connection.State;
				if (state != ConnectionState.Open)
				{
					throw new ReportServerDatabaseUnavailableException("GetChunksForSnapshot - connection state = " + state.ToString());
				}
				try
				{
					using (IDataReader dataReader = cancelableSqlCommand.ExecuteReader())
					{
						while (dataReader.Read())
						{
							string @string = dataReader.GetString(0);
							int @int = dataReader.GetInt32(1);
							ChunkFlags chunkFlags = ChunkFlags.None;
							if (!dataReader.IsDBNull(2))
							{
								chunkFlags = (ChunkFlags)dataReader.GetByte(2);
							}
							string text = null;
							if (!dataReader.IsDBNull(3))
							{
								text = dataReader.GetString(3);
							}
							short num = ChunkHeader.MissingVersion;
							if (!dataReader.IsDBNull(4))
							{
								num = dataReader.GetInt16(4);
							}
							long num2 = (long)dataReader.GetInt32(5);
							ChunkHeader chunkHeader = new ChunkHeader(@string, @int, chunkFlags, text, num, num2);
							list.Add(chunkHeader);
						}
						list2 = list;
					}
				}
				catch (Exception ex)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Error, "GetChunkInformation: {0}", new object[] { ex.ToString() });
					throw;
				}
			}
			return list2;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00061058 File Offset: 0x0005F258
		internal void SetSnapshotProcessingFlags(Guid snapshotId, bool isPermanent, ReportProcessingFlags processingFlags)
		{
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("SetSnapshotProcessingFlags"))
			{
				cancelableSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier).Value = snapshotId;
				cancelableSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = isPermanent;
				cancelableSqlCommand.Parameters.Add("@ProcessingFlags", SqlDbType.Int).Value = (int)processingFlags;
				cancelableSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x020004CF RID: 1231
		internal enum WriteMode
		{
			// Token: 0x04001116 RID: 4374
			Append,
			// Token: 0x04001117 RID: 4375
			Overwrite
		}
	}
}
