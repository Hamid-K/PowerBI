using System;
using System.Data;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BE3 RID: 7139
	public abstract class DelegatingDataReader : IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x0600B224 RID: 45604 RVA: 0x002452EE File Offset: 0x002434EE
		protected DelegatingDataReader(IDataReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x17002CBE RID: 11454
		// (get) Token: 0x0600B225 RID: 45605 RVA: 0x002452FD File Offset: 0x002434FD
		public IDataReader Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x0600B226 RID: 45606 RVA: 0x00245305 File Offset: 0x00243505
		public virtual void Close()
		{
			this.reader.Close();
		}

		// Token: 0x17002CBF RID: 11455
		// (get) Token: 0x0600B227 RID: 45607 RVA: 0x00245312 File Offset: 0x00243512
		public virtual int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x0600B228 RID: 45608 RVA: 0x0024531F File Offset: 0x0024351F
		public virtual DataTable GetSchemaTable()
		{
			return this.reader.GetSchemaTable();
		}

		// Token: 0x17002CC0 RID: 11456
		// (get) Token: 0x0600B229 RID: 45609 RVA: 0x0024532C File Offset: 0x0024352C
		public virtual bool IsClosed
		{
			get
			{
				return this.reader.IsClosed;
			}
		}

		// Token: 0x0600B22A RID: 45610 RVA: 0x00245339 File Offset: 0x00243539
		public virtual bool NextResult()
		{
			return this.reader.NextResult();
		}

		// Token: 0x0600B22B RID: 45611 RVA: 0x00245346 File Offset: 0x00243546
		public virtual bool Read()
		{
			return this.reader.Read();
		}

		// Token: 0x17002CC1 RID: 11457
		// (get) Token: 0x0600B22C RID: 45612 RVA: 0x00245353 File Offset: 0x00243553
		public virtual int RecordsAffected
		{
			get
			{
				return this.reader.RecordsAffected;
			}
		}

		// Token: 0x0600B22D RID: 45613 RVA: 0x00245360 File Offset: 0x00243560
		public virtual void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x17002CC2 RID: 11458
		// (get) Token: 0x0600B22E RID: 45614 RVA: 0x0024536D File Offset: 0x0024356D
		public virtual int FieldCount
		{
			get
			{
				return this.reader.FieldCount;
			}
		}

		// Token: 0x0600B22F RID: 45615 RVA: 0x0024537A File Offset: 0x0024357A
		public virtual bool GetBoolean(int i)
		{
			return this.reader.GetBoolean(i);
		}

		// Token: 0x0600B230 RID: 45616 RVA: 0x00245388 File Offset: 0x00243588
		public virtual byte GetByte(int i)
		{
			return this.reader.GetByte(i);
		}

		// Token: 0x0600B231 RID: 45617 RVA: 0x00245396 File Offset: 0x00243596
		public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			return this.reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
		}

		// Token: 0x0600B232 RID: 45618 RVA: 0x002453AA File Offset: 0x002435AA
		public virtual char GetChar(int i)
		{
			return this.reader.GetChar(i);
		}

		// Token: 0x0600B233 RID: 45619 RVA: 0x002453B8 File Offset: 0x002435B8
		public virtual long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			return this.reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
		}

		// Token: 0x0600B234 RID: 45620 RVA: 0x002453CC File Offset: 0x002435CC
		public virtual IDataReader GetData(int i)
		{
			return this.reader.GetData(i);
		}

		// Token: 0x0600B235 RID: 45621 RVA: 0x002453DA File Offset: 0x002435DA
		public virtual string GetDataTypeName(int i)
		{
			return this.reader.GetDataTypeName(i);
		}

		// Token: 0x0600B236 RID: 45622 RVA: 0x002453E8 File Offset: 0x002435E8
		public virtual DateTime GetDateTime(int i)
		{
			return this.reader.GetDateTime(i);
		}

		// Token: 0x0600B237 RID: 45623 RVA: 0x002453F6 File Offset: 0x002435F6
		public virtual decimal GetDecimal(int i)
		{
			return this.reader.GetDecimal(i);
		}

		// Token: 0x0600B238 RID: 45624 RVA: 0x00245404 File Offset: 0x00243604
		public virtual double GetDouble(int i)
		{
			return this.reader.GetDouble(i);
		}

		// Token: 0x0600B239 RID: 45625 RVA: 0x00245412 File Offset: 0x00243612
		public virtual Type GetFieldType(int i)
		{
			return this.reader.GetFieldType(i);
		}

		// Token: 0x0600B23A RID: 45626 RVA: 0x00245420 File Offset: 0x00243620
		public virtual float GetFloat(int i)
		{
			return this.reader.GetFloat(i);
		}

		// Token: 0x0600B23B RID: 45627 RVA: 0x0024542E File Offset: 0x0024362E
		public virtual Guid GetGuid(int i)
		{
			return this.reader.GetGuid(i);
		}

		// Token: 0x0600B23C RID: 45628 RVA: 0x0024543C File Offset: 0x0024363C
		public virtual short GetInt16(int i)
		{
			return this.reader.GetInt16(i);
		}

		// Token: 0x0600B23D RID: 45629 RVA: 0x0024544A File Offset: 0x0024364A
		public virtual int GetInt32(int i)
		{
			return this.reader.GetInt32(i);
		}

		// Token: 0x0600B23E RID: 45630 RVA: 0x00245458 File Offset: 0x00243658
		public virtual long GetInt64(int i)
		{
			return this.reader.GetInt64(i);
		}

		// Token: 0x0600B23F RID: 45631 RVA: 0x00245466 File Offset: 0x00243666
		public virtual string GetName(int i)
		{
			return this.reader.GetName(i);
		}

		// Token: 0x0600B240 RID: 45632 RVA: 0x00245474 File Offset: 0x00243674
		public virtual int GetOrdinal(string name)
		{
			return this.reader.GetOrdinal(name);
		}

		// Token: 0x0600B241 RID: 45633 RVA: 0x00245482 File Offset: 0x00243682
		public virtual string GetString(int i)
		{
			return this.reader.GetString(i);
		}

		// Token: 0x0600B242 RID: 45634 RVA: 0x00245490 File Offset: 0x00243690
		public virtual object GetValue(int i)
		{
			return this.reader.GetValue(i);
		}

		// Token: 0x0600B243 RID: 45635 RVA: 0x0024549E File Offset: 0x0024369E
		public virtual int GetValues(object[] values)
		{
			return this.reader.GetValues(values);
		}

		// Token: 0x0600B244 RID: 45636 RVA: 0x002454AC File Offset: 0x002436AC
		public virtual bool IsDBNull(int i)
		{
			return this.reader.IsDBNull(i);
		}

		// Token: 0x17002CC3 RID: 11459
		public virtual object this[string name]
		{
			get
			{
				return this.reader[name];
			}
		}

		// Token: 0x17002CC4 RID: 11460
		public virtual object this[int i]
		{
			get
			{
				return this.reader[i];
			}
		}

		// Token: 0x04005B33 RID: 23347
		private readonly IDataReader reader;
	}
}
