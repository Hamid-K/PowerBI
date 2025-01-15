using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.OleDb;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010B4 RID: 4276
	internal sealed class EmptyPageReader : IPageReader, IDisposable
	{
		// Token: 0x06006FFE RID: 28670 RVA: 0x00181971 File Offset: 0x0017FB71
		public EmptyPageReader(IPageReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x17001F87 RID: 8071
		// (get) Token: 0x06006FFF RID: 28671 RVA: 0x00181980 File Offset: 0x0017FB80
		public TableSchema Schema
		{
			get
			{
				return this.reader.Schema;
			}
		}

		// Token: 0x17001F88 RID: 8072
		// (get) Token: 0x06007000 RID: 28672 RVA: 0x0018198D File Offset: 0x0017FB8D
		public IProgress Progress
		{
			get
			{
				return EmptyPageReader.EmptyProgress.Instance;
			}
		}

		// Token: 0x17001F89 RID: 8073
		// (get) Token: 0x06007001 RID: 28673 RVA: 0x00002139 File Offset: 0x00000339
		public int MaxPageRowCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06007002 RID: 28674 RVA: 0x00181994 File Offset: 0x0017FB94
		public IPage CreatePage()
		{
			return new EmptyPageReader.EmptyPage(this.Schema.ColumnCount);
		}

		// Token: 0x06007003 RID: 28675 RVA: 0x0000336E File Offset: 0x0000156E
		public void Read(IPage page)
		{
		}

		// Token: 0x06007004 RID: 28676 RVA: 0x000020FA File Offset: 0x000002FA
		public IPageReader NextResult()
		{
			return null;
		}

		// Token: 0x06007005 RID: 28677 RVA: 0x001819A6 File Offset: 0x0017FBA6
		public void Dispose()
		{
			if (this.reader != null)
			{
				this.reader.Dispose();
				this.reader = null;
			}
		}

		// Token: 0x04003DF7 RID: 15863
		private IPageReader reader;

		// Token: 0x020010B5 RID: 4277
		private class EmptyProgress : IProgress
		{
			// Token: 0x06007006 RID: 28678 RVA: 0x000020FD File Offset: 0x000002FD
			private EmptyProgress()
			{
			}

			// Token: 0x17001F8A RID: 8074
			// (get) Token: 0x06007007 RID: 28679 RVA: 0x001819C2 File Offset: 0x0017FBC2
			public long Rows
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x17001F8B RID: 8075
			// (get) Token: 0x06007008 RID: 28680 RVA: 0x001819C2 File Offset: 0x0017FBC2
			public long ExceptionRows
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x04003DF8 RID: 15864
			public static readonly EmptyPageReader.EmptyProgress Instance = new EmptyPageReader.EmptyProgress();
		}

		// Token: 0x020010B6 RID: 4278
		private class EmptyPage : IPage, IDisposable
		{
			// Token: 0x0600700A RID: 28682 RVA: 0x001819D2 File Offset: 0x0017FBD2
			public EmptyPage(int columnCount)
			{
				this.columnCount = columnCount;
			}

			// Token: 0x17001F8C RID: 8076
			// (get) Token: 0x0600700B RID: 28683 RVA: 0x001819E1 File Offset: 0x0017FBE1
			public int ColumnCount
			{
				get
				{
					return this.columnCount;
				}
			}

			// Token: 0x17001F8D RID: 8077
			// (get) Token: 0x0600700C RID: 28684 RVA: 0x00002105 File Offset: 0x00000305
			public int RowCount
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x0600700D RID: 28685 RVA: 0x001819E9 File Offset: 0x0017FBE9
			public IColumn GetColumn(int ordinal)
			{
				if (ordinal < this.ColumnCount)
				{
					return EmptyPageReader.EmptyColumn.Instance;
				}
				throw new IndexOutOfRangeException();
			}

			// Token: 0x17001F8E RID: 8078
			// (get) Token: 0x0600700E RID: 28686 RVA: 0x001819FF File Offset: 0x0017FBFF
			public IDictionary<int, IExceptionRow> ExceptionRows
			{
				get
				{
					return EmptyPageReader.EmptyPage.emptyExceptionRows;
				}
			}

			// Token: 0x17001F8F RID: 8079
			// (get) Token: 0x0600700F RID: 28687 RVA: 0x000020FA File Offset: 0x000002FA
			public ISerializedException PageException
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06007010 RID: 28688 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x04003DF9 RID: 15865
			private static readonly Dictionary<int, IExceptionRow> emptyExceptionRows = new Dictionary<int, IExceptionRow>(0);

			// Token: 0x04003DFA RID: 15866
			private readonly int columnCount;
		}

		// Token: 0x020010B7 RID: 4279
		private class EmptyColumn : IOleDbColumn, IColumn
		{
			// Token: 0x06007012 RID: 28690 RVA: 0x000020FD File Offset: 0x000002FD
			private EmptyColumn()
			{
			}

			// Token: 0x06007013 RID: 28691 RVA: 0x00181A13 File Offset: 0x0017FC13
			public bool IsNull(int row)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x06007014 RID: 28692 RVA: 0x00181A13 File Offset: 0x0017FC13
			public object GetObject(int row)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x06007015 RID: 28693 RVA: 0x00181A13 File Offset: 0x0017FC13
			public bool GetBoolean(int row)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x06007016 RID: 28694 RVA: 0x00181A13 File Offset: 0x0017FC13
			public byte GetByte(int row)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x06007017 RID: 28695 RVA: 0x00181A13 File Offset: 0x0017FC13
			public short GetInt16(int row)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x06007018 RID: 28696 RVA: 0x00181A13 File Offset: 0x0017FC13
			public int GetInt32(int row)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x06007019 RID: 28697 RVA: 0x00181A13 File Offset: 0x0017FC13
			public long GetInt64(int row)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x0600701A RID: 28698 RVA: 0x00181A13 File Offset: 0x0017FC13
			public float GetFloat(int row)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x0600701B RID: 28699 RVA: 0x00181A13 File Offset: 0x0017FC13
			public Guid GetGuid(int row)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x0600701C RID: 28700 RVA: 0x00181A13 File Offset: 0x0017FC13
			public double GetDouble(int row)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x0600701D RID: 28701 RVA: 0x00181A13 File Offset: 0x0017FC13
			public decimal GetDecimal(int row)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x0600701E RID: 28702 RVA: 0x00181A13 File Offset: 0x0017FC13
			public DateTime GetDateTime(int row)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x0600701F RID: 28703 RVA: 0x00181A13 File Offset: 0x0017FC13
			public string GetString(int row)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x06007020 RID: 28704 RVA: 0x00181A13 File Offset: 0x0017FC13
			public unsafe DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x04003DFB RID: 15867
			public static readonly IColumn Instance = new EmptyPageReader.EmptyColumn();
		}
	}
}
