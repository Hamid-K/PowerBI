using System;
using System.Data;
using System.Globalization;
using System.Security.Permissions;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002D0 RID: 720
	[SqlClientPermission(SecurityAction.Assert, Unrestricted = true, AllowBlankPassword = true)]
	internal sealed class SegmentChunkDbInterface : ChunkStorage
	{
		// Token: 0x060019D2 RID: 6610 RVA: 0x00067BC8 File Offset: 0x00065DC8
		private string GetFormattedOpenSegmentedChunkQueryText()
		{
			if (SegmentChunkDbInterface.m_formattedOpenSegmentedChunk == null)
			{
				string text = base.Connection.Database + "TempDB";
				string text2 = this.ConnectionManager.EscapeAndBracketDBName(text);
				SegmentChunkDbInterface.m_formattedOpenSegmentedChunk = string.Format(CultureInfo.InvariantCulture, "\r\nif (@IsPermanent = 1) begin\t\t\r\n\tselect\t@ChunkId = ChunkId,\r\n\t\t\t@ChunkFlags = ChunkFlags,\r\n            @MimeType = MimeType\r\n\tfrom dbo.SegmentedChunk chunk\r\n\twhere chunk.SnapshotDataId = @SnapshotId and chunk.ChunkName = @ChunkName and chunk.ChunkType = @ChunkType\r\n\t\r\n\tselect\tcsm.SegmentId, \t\t\t\t\r\n\t\t\tcsm.LogicalByteCount as LogicalSegmentLength, \r\n\t\t\tcsm.ActualByteCount as ActualSegmentLength\t\t\r\n\tfrom ChunkSegmentMapping csm\t\t\r\n\twhere csm.ChunkId = @ChunkId\r\n\torder by csm.StartByte asc\r\nend\r\nelse begin\r\n\tselect\t@ChunkId = ChunkId,\r\n\t\t\t@ChunkFlags = ChunkFlags,\r\n            @MimeType = MimeType\r\n\tfrom {0}.dbo.SegmentedChunk chunk\r\n\twhere chunk.SnapshotDataId = @SnapshotId and chunk.ChunkName = @ChunkName and chunk.ChunkType = @ChunkType\r\n\t\r\n\tif @ChunkFlags & 0x4 > 0 begin\r\n\t\t-- Shallow copy: read chunk segments from catalog \r\n\t\tselect\tcsm.SegmentId, \t\t\t\t\r\n\t\t\t\tcsm.LogicalByteCount as LogicalSegmentLength, \r\n\t\t\t\tcsm.ActualByteCount as ActualSegmentLength\t\t\r\n\t\tfrom ChunkSegmentMapping csm\t\t\r\n\t\twhere csm.ChunkId = @ChunkId\r\n\t\torder by csm.StartByte asc\r\n\tend\r\n\telse begin\r\n\t\t-- Regular copy: read chunk segments from temp db\r\n\t\tselect\tcsm.SegmentId, \t\t\t\t\r\n\t\t\t\tcsm.LogicalByteCount as LogicalSegmentLength, \r\n\t\t\t\tcsm.ActualByteCount as ActualSegmentLength\t\t\r\n\t\tfrom {0}.dbo.ChunkSegmentMapping csm\t\t\r\n\t\twhere csm.ChunkId = @ChunkId\r\n\t\torder by csm.StartByte asc\r\n\tend\r\nend\r\n", text2);
			}
			return SegmentChunkDbInterface.m_formattedOpenSegmentedChunk;
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00067C1C File Offset: 0x00065E1C
		public bool OpenSegmentedChunk(Guid snapshotId, bool isPermanent, string chunkName, int chunkType, out Guid chunkId, out ChunkFlags flags, out string mimeType, SegmentChunkDbInterface.OpenChunkSegmentHandler perSegment)
		{
			chunkId = Guid.Empty;
			flags = ChunkFlags.None;
			mimeType = null;
			bool flag;
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("OpenSegmentedChunk"))
			{
				cancelableSqlCommand.CommandText = this.GetFormattedOpenSegmentedChunkQueryText();
				cancelableSqlCommand.CommandType = CommandType.Text;
				cancelableSqlCommand.Parameters.Add("@SnapshotId", SqlDbType.UniqueIdentifier).Value = snapshotId;
				cancelableSqlCommand.Parameters.Add("@IsPermanent", SqlDbType.Bit).Value = isPermanent;
				cancelableSqlCommand.Parameters.Add("@ChunkName", SqlDbType.NVarChar).Value = chunkName;
				cancelableSqlCommand.Parameters.Add("@ChunkType", SqlDbType.Int).Value = chunkType;
				SqlParameter sqlParameter;
				(sqlParameter = cancelableSqlCommand.Parameters.Add("@ChunkId", SqlDbType.UniqueIdentifier)).Direction = ParameterDirection.Output;
				SqlParameter sqlParameter2;
				(sqlParameter2 = cancelableSqlCommand.Parameters.Add("@ChunkFlags", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;
				SqlParameter sqlParameter3;
				(sqlParameter3 = cancelableSqlCommand.Parameters.Add("@MimeType", SqlDbType.NVarChar)).Direction = ParameterDirection.Output;
				sqlParameter3.Size = 260;
				using (IDataReader dataReader = cancelableSqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						Guid guid = dataReader.GetGuid(0);
						int @int = dataReader.GetInt32(1);
						int int2 = dataReader.GetInt32(2);
						if (perSegment != null)
						{
							perSegment(guid, @int, int2);
						}
					}
				}
				if (sqlParameter.Value == null || sqlParameter.Value == DBNull.Value)
				{
					flag = false;
				}
				else
				{
					RSTrace.ChunkTracer.Assert(sqlParameter2.Value != null, "paramChunkFlags");
					chunkId = (Guid)sqlParameter.Value;
					flags = (ChunkFlags)((byte)sqlParameter2.Value);
					mimeType = null;
					if (sqlParameter3.Value != null && sqlParameter3.Value != DBNull.Value)
					{
						mimeType = (string)sqlParameter3.Value;
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x00067E34 File Offset: 0x00066034
		public Guid CreateSegmentedChunk(Guid snapshotId, bool isPermanent, string chunkName, string mimeType, int chunkType, ChunkFlags chunkFlags, short version)
		{
			Guid guid;
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("CreateSegmentedChunk"))
			{
				cancelableSqlCommand.Parameters.Add("@SnapshotId", SqlDbType.UniqueIdentifier).Value = snapshotId;
				cancelableSqlCommand.Parameters.Add("@IsPermanent", SqlDbType.Bit).Value = isPermanent;
				cancelableSqlCommand.Parameters.Add("@ChunkName", SqlDbType.NVarChar).Value = chunkName;
				cancelableSqlCommand.Parameters.Add("@ChunkFlags", SqlDbType.TinyInt).Value = chunkFlags;
				cancelableSqlCommand.Parameters.Add("@ChunkType", SqlDbType.Int).Value = chunkType;
				cancelableSqlCommand.Parameters.Add("@Version", SqlDbType.SmallInt).Value = version;
				cancelableSqlCommand.Parameters.Add("@MimeType", SqlDbType.NVarChar).Value = mimeType;
				cancelableSqlCommand.Parameters.Add("@Machine", SqlDbType.NVarChar).Value = Environment.MachineName;
				SqlParameter sqlParameter = cancelableSqlCommand.Parameters.Add("@ChunkId", SqlDbType.UniqueIdentifier);
				sqlParameter.Direction = ParameterDirection.Output;
				cancelableSqlCommand.ExecuteNonQuery();
				guid = (Guid)sqlParameter.Value;
			}
			return guid;
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00067F80 File Offset: 0x00066180
		public void ReadChunkSegment(Guid chunkId, bool isPermanent, Guid segmentId, int dataIndex, byte[] buffer, int offset, int count)
		{
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("ReadChunkSegment"))
			{
				cancelableSqlCommand.Parameters.Add("@ChunkId", SqlDbType.UniqueIdentifier).Value = chunkId;
				cancelableSqlCommand.Parameters.Add("@IsPermanent", SqlDbType.Bit).Value = isPermanent;
				cancelableSqlCommand.Parameters.Add("@SegmentId", SqlDbType.UniqueIdentifier).Value = segmentId;
				cancelableSqlCommand.Parameters.Add("@DataIndex", SqlDbType.Int).Value = dataIndex;
				cancelableSqlCommand.Parameters.Add("@Length", SqlDbType.Int).Value = count;
				using (IDataReader dataReader = cancelableSqlCommand.ExecuteReader(CommandBehavior.SequentialAccess))
				{
					bool flag = dataReader.Read();
					RSTrace.CatalogTrace.Assert(flag, "no data to read");
					if (flag)
					{
						dataReader.GetBytes(0, 0L, buffer, offset, count);
					}
				}
			}
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00068090 File Offset: 0x00066290
		public void WriteChunkSegment(Guid chunkId, bool isPermanent, Guid segmentId, int dataIndex, int logicalByteCount, byte[] buffer, int offset, int count)
		{
			byte[] array = SegmentChunkDbInterface.CreateContent(buffer, offset, count);
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("WriteChunkSegment"))
			{
				cancelableSqlCommand.Parameters.Add("@ChunkId", SqlDbType.UniqueIdentifier).Value = chunkId;
				cancelableSqlCommand.Parameters.Add("@IsPermanent", SqlDbType.Bit).Value = isPermanent;
				cancelableSqlCommand.Parameters.Add("@SegmentId", SqlDbType.UniqueIdentifier).Value = segmentId;
				cancelableSqlCommand.Parameters.Add("@DataIndex", SqlDbType.Int).Value = dataIndex;
				cancelableSqlCommand.Parameters.Add("@Length", SqlDbType.Int).Value = count;
				cancelableSqlCommand.Parameters.Add("@LogicalByteCount", SqlDbType.Int).Value = logicalByteCount;
				cancelableSqlCommand.Parameters.Add("@Content", SqlDbType.Binary).Value = array;
				cancelableSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0006819C File Offset: 0x0006639C
		public Guid CreateChunkSegment(Guid snapshotId, bool isPermanent, Guid chunkId, long startByte, int logicalByteCount, byte[] buffer, int offset, int count)
		{
			byte[] array = SegmentChunkDbInterface.CreateContent(buffer, offset, count);
			Guid guid;
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("CreateChunkSegment"))
			{
				cancelableSqlCommand.Parameters.Add("@SnapshotId", SqlDbType.UniqueIdentifier).Value = snapshotId;
				cancelableSqlCommand.Parameters.Add("@IsPermanent", SqlDbType.Bit).Value = isPermanent;
				cancelableSqlCommand.Parameters.Add("@ChunkId", SqlDbType.UniqueIdentifier).Value = chunkId;
				cancelableSqlCommand.Parameters.Add("@StartByte", SqlDbType.BigInt).Value = startByte;
				cancelableSqlCommand.Parameters.Add("@Content", SqlDbType.Binary).Value = array;
				cancelableSqlCommand.Parameters.Add("@LogicalByteCount", SqlDbType.Int).Value = logicalByteCount;
				cancelableSqlCommand.Parameters.Add("@Length", SqlDbType.Int).Value = array.Length;
				SqlParameter sqlParameter = cancelableSqlCommand.Parameters.Add("@SegmentId", SqlDbType.UniqueIdentifier);
				sqlParameter.Direction = ParameterDirection.Output;
				cancelableSqlCommand.ExecuteNonQuery();
				guid = (Guid)sqlParameter.Value;
			}
			return guid;
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x000682D0 File Offset: 0x000664D0
		public bool IsSegmentedChunk(Guid snapshotId, bool isPermanent, string chunkName, int chunkType)
		{
			bool flag;
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("IsSegmentedChunk"))
			{
				cancelableSqlCommand.Parameters.Add("@SnapshotId", SqlDbType.UniqueIdentifier).Value = snapshotId;
				cancelableSqlCommand.Parameters.Add("@IsPermanent", SqlDbType.Bit).Value = isPermanent;
				cancelableSqlCommand.Parameters.Add("@ChunkName", SqlDbType.NVarChar).Value = chunkName;
				cancelableSqlCommand.Parameters.Add("@ChunkType", SqlDbType.Int).Value = chunkType;
				SqlParameter sqlParameter = cancelableSqlCommand.Parameters.Add("@IsSegmented", SqlDbType.Bit);
				sqlParameter.Direction = ParameterDirection.Output;
				cancelableSqlCommand.ExecuteNonQuery();
				if (sqlParameter.Value != null && sqlParameter.Value != DBNull.Value)
				{
					flag = (bool)sqlParameter.Value;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x000683BC File Offset: 0x000665BC
		public Guid ShallowCopyChunk(Guid snapshotId, Guid chunkId, bool isPermanent)
		{
			Guid guid;
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("ShallowCopyChunk"))
			{
				cancelableSqlCommand.Parameters.Add("@SnapshotId", SqlDbType.UniqueIdentifier).Value = snapshotId;
				cancelableSqlCommand.Parameters.Add("@ChunkId", SqlDbType.UniqueIdentifier).Value = chunkId;
				cancelableSqlCommand.Parameters.Add("@IsPermanent", SqlDbType.Bit).Value = isPermanent;
				cancelableSqlCommand.Parameters.Add("@Machine", SqlDbType.NVarChar).Value = Environment.MachineName;
				SqlParameter sqlParameter = cancelableSqlCommand.Parameters.Add("@NewChunkId", SqlDbType.UniqueIdentifier);
				sqlParameter.Direction = ParameterDirection.Output;
				cancelableSqlCommand.ExecuteNonQuery();
				if (sqlParameter.Value == null || sqlParameter.Value == DBNull.Value)
				{
					throw new InternalCatalogException("Expected new chunk id");
				}
				guid = (Guid)sqlParameter.Value;
			}
			return guid;
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x000684B4 File Offset: 0x000666B4
		public Guid DeepCopySegment(Guid chunkId, bool isPermanent, Guid segmentId)
		{
			Guid guid;
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("DeepCopySegment"))
			{
				cancelableSqlCommand.Parameters.Add("@ChunkId", SqlDbType.UniqueIdentifier).Value = chunkId;
				cancelableSqlCommand.Parameters.Add("@IsPermanent", SqlDbType.Bit).Value = isPermanent;
				cancelableSqlCommand.Parameters.Add("@SegmentId", SqlDbType.UniqueIdentifier).Value = segmentId;
				SqlParameter sqlParameter = cancelableSqlCommand.Parameters.Add("@NewSegmentId", SqlDbType.UniqueIdentifier);
				sqlParameter.Direction = ParameterDirection.Output;
				cancelableSqlCommand.ExecuteNonQuery();
				if (sqlParameter.Value == null || sqlParameter.Value == DBNull.Value)
				{
					throw new InternalCatalogException("No new segment id was generated");
				}
				guid = (Guid)sqlParameter.Value;
			}
			return guid;
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00068590 File Offset: 0x00066790
		private static byte[] CreateContent(byte[] buffer, int offset, int length)
		{
			if (offset == 0 && length == buffer.Length)
			{
				return buffer;
			}
			byte[] array = new byte[length];
			Array.Copy(buffer, offset, array, 0, length);
			return array;
		}

		// Token: 0x04000966 RID: 2406
		private const string OpenSegmentedChunkQueryTextTemplate = "\r\nif (@IsPermanent = 1) begin\t\t\r\n\tselect\t@ChunkId = ChunkId,\r\n\t\t\t@ChunkFlags = ChunkFlags,\r\n            @MimeType = MimeType\r\n\tfrom dbo.SegmentedChunk chunk\r\n\twhere chunk.SnapshotDataId = @SnapshotId and chunk.ChunkName = @ChunkName and chunk.ChunkType = @ChunkType\r\n\t\r\n\tselect\tcsm.SegmentId, \t\t\t\t\r\n\t\t\tcsm.LogicalByteCount as LogicalSegmentLength, \r\n\t\t\tcsm.ActualByteCount as ActualSegmentLength\t\t\r\n\tfrom ChunkSegmentMapping csm\t\t\r\n\twhere csm.ChunkId = @ChunkId\r\n\torder by csm.StartByte asc\r\nend\r\nelse begin\r\n\tselect\t@ChunkId = ChunkId,\r\n\t\t\t@ChunkFlags = ChunkFlags,\r\n            @MimeType = MimeType\r\n\tfrom {0}.dbo.SegmentedChunk chunk\r\n\twhere chunk.SnapshotDataId = @SnapshotId and chunk.ChunkName = @ChunkName and chunk.ChunkType = @ChunkType\r\n\t\r\n\tif @ChunkFlags & 0x4 > 0 begin\r\n\t\t-- Shallow copy: read chunk segments from catalog \r\n\t\tselect\tcsm.SegmentId, \t\t\t\t\r\n\t\t\t\tcsm.LogicalByteCount as LogicalSegmentLength, \r\n\t\t\t\tcsm.ActualByteCount as ActualSegmentLength\t\t\r\n\t\tfrom ChunkSegmentMapping csm\t\t\r\n\t\twhere csm.ChunkId = @ChunkId\r\n\t\torder by csm.StartByte asc\r\n\tend\r\n\telse begin\r\n\t\t-- Regular copy: read chunk segments from temp db\r\n\t\tselect\tcsm.SegmentId, \t\t\t\t\r\n\t\t\t\tcsm.LogicalByteCount as LogicalSegmentLength, \r\n\t\t\t\tcsm.ActualByteCount as ActualSegmentLength\t\t\r\n\t\tfrom {0}.dbo.ChunkSegmentMapping csm\t\t\r\n\t\twhere csm.ChunkId = @ChunkId\r\n\t\torder by csm.StartByte asc\r\n\tend\r\nend\r\n";

		// Token: 0x04000967 RID: 2407
		private static string m_formattedOpenSegmentedChunk;

		// Token: 0x020004E4 RID: 1252
		// (Invoke) Token: 0x060024A1 RID: 9377
		internal delegate void OpenChunkSegmentHandler(Guid segmentId, int logicalLength, int actualLength);
	}
}
