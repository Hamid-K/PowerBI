using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x020005DE RID: 1502
	public class EntityDataReader : DbDataReader, IExtendedDataRecord, IDataRecord
	{
		// Token: 0x0600491D RID: 18717 RVA: 0x00104083 File Offset: 0x00102283
		internal EntityDataReader(EntityCommand command, DbDataReader storeDataReader, CommandBehavior behavior)
		{
			this._command = command;
			this._storeDataReader = storeDataReader;
			this._storeExtendedDataRecord = storeDataReader as IExtendedDataRecord;
			this._behavior = behavior;
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x001040AC File Offset: 0x001022AC
		internal EntityDataReader()
		{
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x0600491F RID: 18719 RVA: 0x001040B4 File Offset: 0x001022B4
		public override int Depth
		{
			get
			{
				return this._storeDataReader.Depth;
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06004920 RID: 18720 RVA: 0x001040C1 File Offset: 0x001022C1
		public override int FieldCount
		{
			get
			{
				return this._storeDataReader.FieldCount;
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06004921 RID: 18721 RVA: 0x001040CE File Offset: 0x001022CE
		public override bool HasRows
		{
			get
			{
				return this._storeDataReader.HasRows;
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06004922 RID: 18722 RVA: 0x001040DB File Offset: 0x001022DB
		public override bool IsClosed
		{
			get
			{
				return this._storeDataReader.IsClosed;
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06004923 RID: 18723 RVA: 0x001040E8 File Offset: 0x001022E8
		public override int RecordsAffected
		{
			get
			{
				return this._storeDataReader.RecordsAffected;
			}
		}

		// Token: 0x17000E6F RID: 3695
		public override object this[int ordinal]
		{
			get
			{
				return this._storeDataReader[ordinal];
			}
		}

		// Token: 0x17000E70 RID: 3696
		public override object this[string name]
		{
			get
			{
				Check.NotNull<string>(name, "name");
				return this._storeDataReader[name];
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06004926 RID: 18726 RVA: 0x0010411D File Offset: 0x0010231D
		public override int VisibleFieldCount
		{
			get
			{
				return this._storeDataReader.VisibleFieldCount;
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06004927 RID: 18727 RVA: 0x0010412A File Offset: 0x0010232A
		public DataRecordInfo DataRecordInfo
		{
			get
			{
				if (this._storeExtendedDataRecord == null)
				{
					return null;
				}
				return this._storeExtendedDataRecord.DataRecordInfo;
			}
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x00104144 File Offset: 0x00102344
		public override void Close()
		{
			if (this._command != null)
			{
				this._storeDataReader.Close();
				this._command.NotifyDataReaderClosing();
				if ((this._behavior & CommandBehavior.CloseConnection) == CommandBehavior.CloseConnection)
				{
					this._command.Connection.Close();
				}
				this._command = null;
			}
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x00104193 File Offset: 0x00102393
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed && disposing)
			{
				this._storeDataReader.Dispose();
			}
			this._disposed = true;
			base.Dispose(disposing);
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x001041B9 File Offset: 0x001023B9
		public override bool GetBoolean(int ordinal)
		{
			return this._storeDataReader.GetBoolean(ordinal);
		}

		// Token: 0x0600492B RID: 18731 RVA: 0x001041C7 File Offset: 0x001023C7
		public override byte GetByte(int ordinal)
		{
			return this._storeDataReader.GetByte(ordinal);
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x001041D5 File Offset: 0x001023D5
		public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._storeDataReader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600492D RID: 18733 RVA: 0x001041E9 File Offset: 0x001023E9
		public override char GetChar(int ordinal)
		{
			return this._storeDataReader.GetChar(ordinal);
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x001041F7 File Offset: 0x001023F7
		public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._storeDataReader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600492F RID: 18735 RVA: 0x0010420B File Offset: 0x0010240B
		public override string GetDataTypeName(int ordinal)
		{
			return this._storeDataReader.GetDataTypeName(ordinal);
		}

		// Token: 0x06004930 RID: 18736 RVA: 0x00104219 File Offset: 0x00102419
		public override DateTime GetDateTime(int ordinal)
		{
			return this._storeDataReader.GetDateTime(ordinal);
		}

		// Token: 0x06004931 RID: 18737 RVA: 0x00104227 File Offset: 0x00102427
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			return this._storeDataReader.GetData(ordinal);
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x00104235 File Offset: 0x00102435
		public override decimal GetDecimal(int ordinal)
		{
			return this._storeDataReader.GetDecimal(ordinal);
		}

		// Token: 0x06004933 RID: 18739 RVA: 0x00104243 File Offset: 0x00102443
		public override double GetDouble(int ordinal)
		{
			return this._storeDataReader.GetDouble(ordinal);
		}

		// Token: 0x06004934 RID: 18740 RVA: 0x00104251 File Offset: 0x00102451
		public override Type GetFieldType(int ordinal)
		{
			return this._storeDataReader.GetFieldType(ordinal);
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x0010425F File Offset: 0x0010245F
		public override float GetFloat(int ordinal)
		{
			return this._storeDataReader.GetFloat(ordinal);
		}

		// Token: 0x06004936 RID: 18742 RVA: 0x0010426D File Offset: 0x0010246D
		public override Guid GetGuid(int ordinal)
		{
			return this._storeDataReader.GetGuid(ordinal);
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x0010427B File Offset: 0x0010247B
		public override short GetInt16(int ordinal)
		{
			return this._storeDataReader.GetInt16(ordinal);
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x00104289 File Offset: 0x00102489
		public override int GetInt32(int ordinal)
		{
			return this._storeDataReader.GetInt32(ordinal);
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x00104297 File Offset: 0x00102497
		public override long GetInt64(int ordinal)
		{
			return this._storeDataReader.GetInt64(ordinal);
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x001042A5 File Offset: 0x001024A5
		public override string GetName(int ordinal)
		{
			return this._storeDataReader.GetName(ordinal);
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x001042B3 File Offset: 0x001024B3
		public override int GetOrdinal(string name)
		{
			Check.NotNull<string>(name, "name");
			return this._storeDataReader.GetOrdinal(name);
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x001042CD File Offset: 0x001024CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			return this._storeDataReader.GetProviderSpecificFieldType(ordinal);
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x001042DB File Offset: 0x001024DB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override object GetProviderSpecificValue(int ordinal)
		{
			return this._storeDataReader.GetProviderSpecificValue(ordinal);
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x001042E9 File Offset: 0x001024E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetProviderSpecificValues(object[] values)
		{
			return this._storeDataReader.GetProviderSpecificValues(values);
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x001042F7 File Offset: 0x001024F7
		public override DataTable GetSchemaTable()
		{
			return this._storeDataReader.GetSchemaTable();
		}

		// Token: 0x06004940 RID: 18752 RVA: 0x00104304 File Offset: 0x00102504
		public override string GetString(int ordinal)
		{
			return this._storeDataReader.GetString(ordinal);
		}

		// Token: 0x06004941 RID: 18753 RVA: 0x00104312 File Offset: 0x00102512
		public override object GetValue(int ordinal)
		{
			return this._storeDataReader.GetValue(ordinal);
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x00104320 File Offset: 0x00102520
		public override int GetValues(object[] values)
		{
			return this._storeDataReader.GetValues(values);
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x0010432E File Offset: 0x0010252E
		public override bool IsDBNull(int ordinal)
		{
			return this._storeDataReader.IsDBNull(ordinal);
		}

		// Token: 0x06004944 RID: 18756 RVA: 0x0010433C File Offset: 0x0010253C
		public override bool NextResult()
		{
			bool flag;
			try
			{
				flag = this._storeDataReader.NextResult();
			}
			catch (Exception ex)
			{
				throw new EntityCommandExecutionException(Strings.EntityClient_StoreReaderFailed, ex);
			}
			return flag;
		}

		// Token: 0x06004945 RID: 18757 RVA: 0x00104378 File Offset: 0x00102578
		public override async Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			bool flag;
			try
			{
				flag = await this._storeDataReader.NextResultAsync(cancellationToken).WithCurrentCulture<bool>();
			}
			catch (Exception ex)
			{
				throw new EntityCommandExecutionException(Strings.EntityClient_StoreReaderFailed, ex);
			}
			return flag;
		}

		// Token: 0x06004946 RID: 18758 RVA: 0x001043C5 File Offset: 0x001025C5
		public override bool Read()
		{
			return this._storeDataReader.Read();
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x001043D2 File Offset: 0x001025D2
		public override Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return this._storeDataReader.ReadAsync(cancellationToken);
		}

		// Token: 0x06004948 RID: 18760 RVA: 0x001043E7 File Offset: 0x001025E7
		public override IEnumerator GetEnumerator()
		{
			return this._storeDataReader.GetEnumerator();
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x001043F4 File Offset: 0x001025F4
		public DbDataRecord GetDataRecord(int i)
		{
			if (this._storeExtendedDataRecord == null)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			return this._storeExtendedDataRecord.GetDataRecord(i);
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x00104415 File Offset: 0x00102615
		public DbDataReader GetDataReader(int i)
		{
			return this.GetDbDataReader(i);
		}

		// Token: 0x040019E4 RID: 6628
		private EntityCommand _command;

		// Token: 0x040019E5 RID: 6629
		private readonly CommandBehavior _behavior;

		// Token: 0x040019E6 RID: 6630
		private readonly DbDataReader _storeDataReader;

		// Token: 0x040019E7 RID: 6631
		private readonly IExtendedDataRecord _storeExtendedDataRecord;

		// Token: 0x040019E8 RID: 6632
		private bool _disposed;
	}
}
