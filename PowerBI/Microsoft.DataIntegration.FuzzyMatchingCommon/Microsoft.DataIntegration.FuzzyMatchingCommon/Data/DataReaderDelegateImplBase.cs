using System;
using System.Data;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Data
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	public abstract class DataReaderDelegateImplBase : IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600028E RID: 654
		protected abstract IDataRecord Current { get; }

		// Token: 0x0600028F RID: 655
		public abstract DataTable GetSchemaTable();

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000290 RID: 656
		public abstract bool IsClosed { get; }

		// Token: 0x06000291 RID: 657
		public abstract void Close();

		// Token: 0x06000292 RID: 658
		public abstract bool Read();

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00014AD8 File Offset: 0x00012CD8
		public virtual int Depth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00014ADB File Offset: 0x00012CDB
		public virtual bool NextResult()
		{
			return false;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00014ADE File Offset: 0x00012CDE
		public virtual int RecordsAffected
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00014AE1 File Offset: 0x00012CE1
		public virtual int FieldCount
		{
			get
			{
				return this.GetSchemaTable().Rows.Count;
			}
		}

		// Token: 0x17000067 RID: 103
		public virtual object this[int i]
		{
			get
			{
				return this.Current[i];
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00014B01 File Offset: 0x00012D01
		public virtual object GetValue(int i)
		{
			return this.Current.GetValue(i);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00014B0F File Offset: 0x00012D0F
		public virtual bool GetBoolean(int i)
		{
			return this.Current.GetBoolean(i);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00014B1D File Offset: 0x00012D1D
		public virtual byte GetByte(int i)
		{
			return this.Current.GetByte(i);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00014B2B File Offset: 0x00012D2B
		public virtual char GetChar(int i)
		{
			return this.Current.GetChar(i);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00014B39 File Offset: 0x00012D39
		public virtual decimal GetDecimal(int i)
		{
			return this.Current.GetDecimal(i);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00014B47 File Offset: 0x00012D47
		public virtual double GetDouble(int i)
		{
			return this.Current.GetDouble(i);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00014B55 File Offset: 0x00012D55
		public virtual float GetFloat(int i)
		{
			return this.Current.GetFloat(i);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00014B63 File Offset: 0x00012D63
		public virtual short GetInt16(int i)
		{
			return this.Current.GetInt16(i);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00014B71 File Offset: 0x00012D71
		public virtual int GetInt32(int i)
		{
			return this.Current.GetInt32(i);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00014B7F File Offset: 0x00012D7F
		public virtual long GetInt64(int i)
		{
			return this.Current.GetInt64(i);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00014B8D File Offset: 0x00012D8D
		public virtual string GetString(int i)
		{
			return this.Current.GetString(i);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00014B9B File Offset: 0x00012D9B
		public virtual DateTime GetDateTime(int i)
		{
			return this.Current.GetDateTime(i);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00014BA9 File Offset: 0x00012DA9
		public virtual Guid GetGuid(int i)
		{
			return this.Current.GetGuid(i);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00014BB7 File Offset: 0x00012DB7
		public virtual bool IsDBNull(int i)
		{
			return this.Current == null || this.Current.IsDBNull(i);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00014BCF File Offset: 0x00012DCF
		public virtual Type GetFieldType(int i)
		{
			return this.Current.GetFieldType(i);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00014BDD File Offset: 0x00012DDD
		public virtual int GetValues(object[] values)
		{
			return this.Current.GetValues(values);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00014BEB File Offset: 0x00012DEB
		public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			return this.Current.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00014BFF File Offset: 0x00012DFF
		public virtual long GetChars(int i, long fieldOffset, char[] buffer, int bufferoffset, int length)
		{
			return this.Current.GetChars(i, fieldOffset, buffer, bufferoffset, length);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00014C13 File Offset: 0x00012E13
		public virtual IDataReader GetData(int i)
		{
			return this.Current.GetData(i);
		}

		// Token: 0x17000068 RID: 104
		public virtual object this[string name]
		{
			get
			{
				return this.Current[name];
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00014C2F File Offset: 0x00012E2F
		public virtual string GetDataTypeName(int i)
		{
			return this.Current.GetDataTypeName(i);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00014C3D File Offset: 0x00012E3D
		public virtual string GetName(int i)
		{
			return this.Current.GetName(i);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00014C4B File Offset: 0x00012E4B
		public virtual int GetOrdinal(string name)
		{
			return this.Current.GetOrdinal(name);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00014C59 File Offset: 0x00012E59
		void IDisposable.Dispose()
		{
		}
	}
}
