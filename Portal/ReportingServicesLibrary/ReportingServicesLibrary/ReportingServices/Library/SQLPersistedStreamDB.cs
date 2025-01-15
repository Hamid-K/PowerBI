using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000234 RID: 564
	internal class SQLPersistedStreamDB : Storage
	{
		// Token: 0x06001486 RID: 5254 RVA: 0x0004F958 File Offset: 0x0004DB58
		public SQLPersistedStreamDB(ConnectionManager connManager, string sessionID)
		{
			this.ConnectionManager = connManager;
			this.m_sessionID = sessionID;
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0004F970 File Offset: 0x0004DB70
		public int DeleteSessionStreams()
		{
			int num;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeletePersistedStreams", null))
			{
				instrumentedSqlCommand.AddParameter("@SessionID", SqlDbType.VarChar, this.m_sessionID);
				num = instrumentedSqlCommand.ExecuteNonQuery();
			}
			return num;
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0004F9C4 File Offset: 0x0004DBC4
		public int DeleteExpiredStreams()
		{
			int num;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteExpiredPersistedStreams", null))
				{
					instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCleanupTimeoutSeconds;
					num = instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in DeleteExpiredPersistedStreams: {0}", new object[] { ex });
				if (!(ex is RSException))
				{
					throw;
				}
				num = 0;
			}
			return num;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0004FA44 File Offset: 0x0004DC44
		public void AddNewSessionStream(int index)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("AddPersistedStream", null))
			{
				instrumentedSqlCommand.AddParameter("@SessionID", SqlDbType.VarChar, this.m_sessionID);
				instrumentedSqlCommand.AddParameter("@Index", SqlDbType.Int, index);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0004FAA8 File Offset: 0x0004DCA8
		public void LockSessionStream(int index)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("LockPersistedStream", null))
			{
				instrumentedSqlCommand.AddParameter("@SessionID", SqlDbType.VarChar, this.m_sessionID);
				instrumentedSqlCommand.AddParameter("@Index", SqlDbType.Int, index);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0004FB0C File Offset: 0x0004DD0C
		public void PersistStream(int index, Stream stream)
		{
			stream.Position = 0L;
			RSStream rsstream = stream as RSStream;
			int responseBufferSizeBytes = Global.ResponseBufferSizeBytes;
			byte[] array = new byte[responseBufferSizeBytes];
			int num = 0;
			num = stream.Read(array, 0, responseBufferSizeBytes);
			object obj;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("WriteFirstPortionPersistedStream", null))
			{
				instrumentedSqlCommand.AddParameter("@SessionID", SqlDbType.VarChar, this.m_sessionID);
				instrumentedSqlCommand.AddParameter("@Index", SqlDbType.Int, index);
				if (rsstream.Name != null)
				{
					instrumentedSqlCommand.AddParameter("@Name", SqlDbType.NVarChar, rsstream.Name);
				}
				if (rsstream.MimeType != null)
				{
					instrumentedSqlCommand.AddParameter("@MimeType", SqlDbType.NVarChar, rsstream.MimeType);
				}
				if (rsstream.Extension != null)
				{
					instrumentedSqlCommand.AddParameter("@Extension", SqlDbType.NVarChar, rsstream.Extension);
				}
				if (rsstream.Encoding != null)
				{
					instrumentedSqlCommand.AddParameter("@Encoding", SqlDbType.Int, rsstream.Encoding);
				}
				SqlParameter sqlParameter = instrumentedSqlCommand.AddParameter("@Content", SqlDbType.Image, array);
				sqlParameter.Size = num;
				sqlParameter.Offset = 0;
				obj = instrumentedSqlCommand.ExecuteScalar();
			}
			using (InstrumentedSqlCommand instrumentedSqlCommand2 = this.NewStandardSqlCommand("WriteNextPortionPersistedStream", null))
			{
				instrumentedSqlCommand2.AddParameter("@DataPointer", SqlDbType.Binary, obj);
				instrumentedSqlCommand2.Parameters.Add("@DataIndex", SqlDbType.Int);
				instrumentedSqlCommand2.AddParameter("@DeleteLength", SqlDbType.Int, DBNull.Value);
				instrumentedSqlCommand2.Parameters.Add("@Content", SqlDbType.Image);
				int num2;
				while ((num2 = stream.Read(array, 0, responseBufferSizeBytes)) > 0)
				{
					instrumentedSqlCommand2.Parameters["@DataIndex"].Value = num;
					instrumentedSqlCommand2.Parameters["@Content"].Value = array;
					instrumentedSqlCommand2.Parameters["@Content"].Size = num2;
					instrumentedSqlCommand2.Parameters["@Content"].Offset = 0;
					num += num2;
					instrumentedSqlCommand2.ExecuteNonQuery();
				}
			}
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0004FD24 File Offset: 0x0004DF24
		public void SetError(Exception e, int index, bool setOnAllRows)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetPersistedStreamError", null))
			{
				instrumentedSqlCommand.AddParameter("@SessionID", SqlDbType.VarChar, this.m_sessionID);
				instrumentedSqlCommand.AddParameter("@Index", SqlDbType.Int, index);
				instrumentedSqlCommand.AddParameter("@AllRows", SqlDbType.Bit, setOnAllRows ? 1 : 0);
				instrumentedSqlCommand.AddParameter("@Error", SqlDbType.NVarChar, e.Message);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0004FDB8 File Offset: 0x0004DFB8
		public RSStream GetNextStream()
		{
			RSStream rsstream = null;
			string text = null;
			int num = 0;
			object obj = null;
			int num2 = 0;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetFirstPortionPersistedStream", null))
			{
				instrumentedSqlCommand.AddParameter("@SessionID", SqlDbType.VarChar, this.m_sessionID);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (dataReader.Read())
					{
						if (!dataReader.IsDBNull(0))
						{
							obj = dataReader.GetValue(0);
							num2 = dataReader.GetInt32(1);
						}
						num = dataReader.GetInt32(2);
						if (obj != null)
						{
							rsstream = new RSStream(new MemoryThenFileStream(), false);
							if (!dataReader.IsDBNull(3))
							{
								rsstream.Name = dataReader.GetString(3);
							}
							if (!dataReader.IsDBNull(4))
							{
								rsstream.MimeType = dataReader.GetString(4);
							}
							if (!dataReader.IsDBNull(5))
							{
								rsstream.Extension = dataReader.GetString(5);
							}
							if (!dataReader.IsDBNull(6))
							{
								rsstream.Encoding = Encoding.GetEncoding(dataReader.GetString(6));
							}
							if (!dataReader.IsDBNull(7))
							{
								text = dataReader.GetString(7);
							}
						}
					}
				}
			}
			if (text != null)
			{
				throw new StreamNotFoundException(null);
			}
			if (obj != null && obj != DBNull.Value)
			{
				int responseBufferSizeBytes = Global.ResponseBufferSizeBytes;
				using (InstrumentedSqlCommand instrumentedSqlCommand2 = this.NewStandardSqlCommand("GetNextPortionPersistedStream", null))
				{
					instrumentedSqlCommand2.AddParameter("@DataPointer", SqlDbType.Binary, obj);
					instrumentedSqlCommand2.Parameters.Add("@DataIndex", SqlDbType.Int);
					instrumentedSqlCommand2.Parameters.Add("@Length", SqlDbType.Int);
					byte[] array = new byte[responseBufferSizeBytes];
					int num3 = 0;
					int num4 = 0;
					do
					{
						int num5 = Math.Min(num2, responseBufferSizeBytes);
						instrumentedSqlCommand2.Parameters["@Length"].Value = num5;
						instrumentedSqlCommand2.Parameters["@DataIndex"].Value = num4;
						using (IDataReader dataReader2 = instrumentedSqlCommand2.ExecuteReader())
						{
							if (dataReader2.Read())
							{
								num3 = (int)dataReader2.GetBytes(0, 0L, array, 0, responseBufferSizeBytes);
								rsstream.Write(array, 0, num3);
								num2 -= num3;
								num4 += num3;
							}
							else
							{
								num3 = 0;
							}
						}
					}
					while (num3 > 0 && num2 > 0);
				}
				rsstream.Position = 0L;
			}
			using (InstrumentedSqlCommand instrumentedSqlCommand3 = this.NewStandardSqlCommand("DeletePersistedStream", null))
			{
				instrumentedSqlCommand3.AddParameter("@SessionID", SqlDbType.VarChar, this.m_sessionID);
				instrumentedSqlCommand3.AddParameter("@Index", SqlDbType.Int, num);
				instrumentedSqlCommand3.ExecuteNonQuery();
			}
			return rsstream;
		}

		// Token: 0x0400074A RID: 1866
		private string m_sessionID;

		// Token: 0x020004B0 RID: 1200
		private enum GetFirstPortionProjection
		{
			// Token: 0x0400109E RID: 4254
			TextPointer,
			// Token: 0x0400109F RID: 4255
			ContentLength,
			// Token: 0x040010A0 RID: 4256
			Index,
			// Token: 0x040010A1 RID: 4257
			Name,
			// Token: 0x040010A2 RID: 4258
			MimeType,
			// Token: 0x040010A3 RID: 4259
			Extension,
			// Token: 0x040010A4 RID: 4260
			Encoding,
			// Token: 0x040010A5 RID: 4261
			Error
		}
	}
}
