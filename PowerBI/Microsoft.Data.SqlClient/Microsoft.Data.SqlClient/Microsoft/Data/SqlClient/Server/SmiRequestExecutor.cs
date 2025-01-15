using System;
using System.Data;
using System.Data.SqlTypes;
using System.Transactions;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200014E RID: 334
	internal abstract class SmiRequestExecutor : SmiTypedGetterSetter, ITypedSettersV3, ITypedSetters, ITypedGetters, IDisposable
	{
		// Token: 0x060019BA RID: 6586 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void Close(SmiEventSink eventSink)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00062173 File Offset: 0x00060373
		internal virtual SmiEventStream Execute(SmiConnection connection, long transactionId, Transaction associatedTransaction, CommandBehavior behavior, SmiExecuteType executeType)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x0001996E File Offset: 0x00017B6E
		internal override bool CanGet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060019BE RID: 6590
		internal abstract void SetDefault(int ordinal);

		// Token: 0x060019BF RID: 6591 RVA: 0x00062173 File Offset: 0x00060373
		internal virtual SmiEventStream Execute(SmiConnection connection, long transactionId, CommandBehavior behavior, SmiExecuteType executeType)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void Dispose()
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00062173 File Offset: 0x00060373
		internal virtual bool IsSetAsDefault(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x00062173 File Offset: 0x00060373
		public virtual int Count
		{
			get
			{
				throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
			}
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SmiParameterMetaData GetMetaData(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00062173 File Offset: 0x00060373
		public virtual bool IsDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlDbType GetVariantType(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00062173 File Offset: 0x00060373
		public virtual bool GetBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00062173 File Offset: 0x00060373
		public virtual byte GetByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00062173 File Offset: 0x00060373
		public virtual long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00062173 File Offset: 0x00060373
		public virtual char GetChar(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00062173 File Offset: 0x00060373
		public virtual long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00062173 File Offset: 0x00060373
		public virtual short GetInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00062173 File Offset: 0x00060373
		public virtual int GetInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x00062173 File Offset: 0x00060373
		public virtual long GetInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00062173 File Offset: 0x00060373
		public virtual float GetFloat(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00062173 File Offset: 0x00060373
		public virtual double GetDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00062173 File Offset: 0x00060373
		public virtual string GetString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x00062173 File Offset: 0x00060373
		public virtual decimal GetDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00062173 File Offset: 0x00060373
		public virtual DateTime GetDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00062173 File Offset: 0x00060373
		public virtual Guid GetGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlBoolean GetSqlBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlByte GetSqlByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlInt16 GetSqlInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlInt32 GetSqlInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlInt64 GetSqlInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlSingle GetSqlSingle(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlDouble GetSqlDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlMoney GetSqlMoney(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlDateTime GetSqlDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlDecimal GetSqlDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlString GetSqlString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlBinary GetSqlBinary(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlGuid GetSqlGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlChars GetSqlChars(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlBytes GetSqlBytes(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlXml GetSqlXml(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlXml GetSqlXmlRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlBytes GetSqlBytesRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlChars GetSqlCharsRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetBoolean(int ordinal, bool value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetByte(int ordinal, byte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetChar(int ordinal, char value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetInt16(int ordinal, short value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetInt32(int ordinal, int value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetInt64(int ordinal, long value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetFloat(int ordinal, float value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetDouble(int ordinal, double value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetString(int ordinal, string value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetString(int ordinal, string value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetDecimal(int ordinal, decimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetDateTime(int ordinal, DateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetGuid(int ordinal, Guid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlBoolean(int ordinal, SqlBoolean value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlByte(int ordinal, SqlByte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlInt16(int ordinal, SqlInt16 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlInt32(int ordinal, SqlInt32 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlInt64(int ordinal, SqlInt64 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlSingle(int ordinal, SqlSingle value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlDouble(int ordinal, SqlDouble value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlMoney(int ordinal, SqlMoney value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlDateTime(int ordinal, SqlDateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlDecimal(int ordinal, SqlDecimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlString(int ordinal, SqlString value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlString(int ordinal, SqlString value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlBinary(int ordinal, SqlBinary value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlBinary(int ordinal, SqlBinary value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlGuid(int ordinal, SqlGuid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlChars(int ordinal, SqlChars value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlChars(int ordinal, SqlChars value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlBytes(int ordinal, SqlBytes value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlBytes(int ordinal, SqlBytes value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlXml(int ordinal, SqlXml value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}
	}
}
