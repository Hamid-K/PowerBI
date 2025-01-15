using System;
using System.Threading;
using Microsoft.Data.ProviderBase;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000094 RID: 148
	internal sealed class SqlReferenceCollection : DbReferenceCollection
	{
		// Token: 0x06000C32 RID: 3122 RVA: 0x00024DC8 File Offset: 0x00022FC8
		public override void Add(object value, int tag)
		{
			base.AddItem(value, tag);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00024DD2 File Offset: 0x00022FD2
		internal void Deactivate()
		{
			base.Notify(0);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00024DDC File Offset: 0x00022FDC
		internal SqlDataReader FindLiveReader(SqlCommand command)
		{
			if (command == null)
			{
				return base.FindItem<SqlDataReader>(1, SqlReferenceCollection.s_hasOpenReaderFunc);
			}
			SqlReferenceCollection.FindLiveReaderContext findLiveReaderContext = Interlocked.Exchange<SqlReferenceCollection.FindLiveReaderContext>(ref SqlReferenceCollection.s_cachedFindLiveReaderContext, null) ?? new SqlReferenceCollection.FindLiveReaderContext();
			findLiveReaderContext.Setup(command);
			SqlDataReader sqlDataReader = base.FindItem<SqlDataReader>(1, findLiveReaderContext.Func);
			findLiveReaderContext.Clear();
			Interlocked.CompareExchange<SqlReferenceCollection.FindLiveReaderContext>(ref SqlReferenceCollection.s_cachedFindLiveReaderContext, findLiveReaderContext, null);
			return sqlDataReader;
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00024E38 File Offset: 0x00023038
		protected override void NotifyItem(int message, int tag, object value)
		{
			if (tag == 1)
			{
				SqlDataReader sqlDataReader = (SqlDataReader)value;
				if (!sqlDataReader.IsClosed)
				{
					sqlDataReader.CloseReaderFromConnection();
					return;
				}
			}
			else
			{
				if (tag == 2)
				{
					((SqlCommand)value).OnConnectionClosed();
					return;
				}
				if (tag == 3)
				{
					((SqlBulkCopy)value).OnConnectionClosed();
				}
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00024E7E File Offset: 0x0002307E
		public override void Remove(object value)
		{
			base.RemoveItem(value);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00024E87 File Offset: 0x00023087
		private static bool HasOpenReaderPredicate(SqlDataReader reader)
		{
			return !reader.IsClosed;
		}

		// Token: 0x04000321 RID: 801
		internal const int DataReaderTag = 1;

		// Token: 0x04000322 RID: 802
		internal const int CommandTag = 2;

		// Token: 0x04000323 RID: 803
		internal const int BulkCopyTag = 3;

		// Token: 0x04000324 RID: 804
		private static readonly Func<SqlDataReader, bool> s_hasOpenReaderFunc = new Func<SqlDataReader, bool>(SqlReferenceCollection.HasOpenReaderPredicate);

		// Token: 0x04000325 RID: 805
		private static SqlReferenceCollection.FindLiveReaderContext s_cachedFindLiveReaderContext;

		// Token: 0x020001D4 RID: 468
		private sealed class FindLiveReaderContext
		{
			// Token: 0x06001DC5 RID: 7621 RVA: 0x0007AE41 File Offset: 0x00079041
			public FindLiveReaderContext()
			{
				this.Func = new Func<SqlDataReader, bool>(this.Predicate);
			}

			// Token: 0x06001DC6 RID: 7622 RVA: 0x0007AE5B File Offset: 0x0007905B
			public void Setup(SqlCommand command)
			{
				this._command = command;
			}

			// Token: 0x06001DC7 RID: 7623 RVA: 0x0007AE64 File Offset: 0x00079064
			public void Clear()
			{
				this._command = null;
			}

			// Token: 0x06001DC8 RID: 7624 RVA: 0x0007AE6D File Offset: 0x0007906D
			private bool Predicate(SqlDataReader reader)
			{
				return !reader.IsClosed && this._command == reader.Command;
			}

			// Token: 0x0400142F RID: 5167
			public readonly Func<SqlDataReader, bool> Func;

			// Token: 0x04001430 RID: 5168
			private SqlCommand _command;
		}
	}
}
