using System;
using System.Data.SqlTypes;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200012D RID: 301
	internal abstract class SmiTypedGetterSetter : ITypedGettersV3, ITypedSettersV3
	{
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06001757 RID: 5975
		internal abstract bool CanGet { get; }

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06001758 RID: 5976
		internal abstract bool CanSet { get; }

		// Token: 0x06001759 RID: 5977 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual bool IsDBNull(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual SmiMetaData GetVariantType(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual bool GetBoolean(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual byte GetByte(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual long GetBytesLength(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual int GetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual long GetCharsLength(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual int GetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual string GetString(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual short GetInt16(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual int GetInt32(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual long GetInt64(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual float GetSingle(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual double GetDouble(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual SqlDecimal GetSqlDecimal(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual DateTime GetDateTime(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual Guid GetGuid(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual TimeSpan GetTimeSpan(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0006225C File Offset: 0x0006045C
		public virtual DateTimeOffset GetDateTimeOffset(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00062173 File Offset: 0x00060373
		internal virtual SmiTypedGetterSetter GetTypedGetterSetter(SmiEventSink sink, int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0006225C File Offset: 0x0006045C
		internal virtual bool NextElement(SmiEventSink sink)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00062275 File Offset: 0x00060475
		public virtual void SetDBNull(SmiEventSink sink, int ordinal)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00062275 File Offset: 0x00060475
		public virtual void SetBoolean(SmiEventSink sink, int ordinal, bool value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00062275 File Offset: 0x00060475
		public virtual void SetByte(SmiEventSink sink, int ordinal, byte value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00062275 File Offset: 0x00060475
		public virtual int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00062275 File Offset: 0x00060475
		public virtual void SetBytesLength(SmiEventSink sink, int ordinal, long length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00062275 File Offset: 0x00060475
		public virtual int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x00062275 File Offset: 0x00060475
		public virtual void SetCharsLength(SmiEventSink sink, int ordinal, long length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00062275 File Offset: 0x00060475
		public virtual void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00062275 File Offset: 0x00060475
		public virtual void SetInt16(SmiEventSink sink, int ordinal, short value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00062275 File Offset: 0x00060475
		public virtual void SetInt32(SmiEventSink sink, int ordinal, int value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00062275 File Offset: 0x00060475
		public virtual void SetInt64(SmiEventSink sink, int ordinal, long value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00062275 File Offset: 0x00060475
		public virtual void SetSingle(SmiEventSink sink, int ordinal, float value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00062275 File Offset: 0x00060475
		public virtual void SetDouble(SmiEventSink sink, int ordinal, double value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00062275 File Offset: 0x00060475
		public virtual void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00062275 File Offset: 0x00060475
		public virtual void SetDateTime(SmiEventSink sink, int ordinal, DateTime value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00062275 File Offset: 0x00060475
		public virtual void SetGuid(SmiEventSink sink, int ordinal, Guid value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00062275 File Offset: 0x00060475
		public virtual void SetTimeSpan(SmiEventSink sink, int ordinal, TimeSpan value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00062275 File Offset: 0x00060475
		public virtual void SetDateTimeOffset(SmiEventSink sink, int ordinal, DateTimeOffset value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x00062275 File Offset: 0x00060475
		internal virtual void NewElement(SmiEventSink sink)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x00062275 File Offset: 0x00060475
		internal virtual void EndElements(SmiEventSink sink)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}
	}
}
