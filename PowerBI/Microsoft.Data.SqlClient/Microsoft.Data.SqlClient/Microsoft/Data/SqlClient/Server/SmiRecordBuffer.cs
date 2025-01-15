using System;
using System.Data;
using System.Data.SqlTypes;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200012B RID: 299
	internal abstract class SmiRecordBuffer : SmiTypedGetterSetter, ITypedGettersV3, ITypedSettersV3, ITypedGetters, ITypedSetters, IDisposable
	{
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		internal override bool CanGet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void Dispose()
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00062173 File Offset: 0x00060373
		public virtual bool IsDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlDbType GetVariantType(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x00062173 File Offset: 0x00060373
		public virtual bool GetBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00062173 File Offset: 0x00060373
		public virtual byte GetByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00062173 File Offset: 0x00060373
		public virtual long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00062173 File Offset: 0x00060373
		public virtual char GetChar(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00062173 File Offset: 0x00060373
		public virtual long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00062173 File Offset: 0x00060373
		public virtual short GetInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00062173 File Offset: 0x00060373
		public virtual int GetInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00062173 File Offset: 0x00060373
		public virtual long GetInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00062173 File Offset: 0x00060373
		public virtual float GetFloat(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00062173 File Offset: 0x00060373
		public virtual double GetDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00062173 File Offset: 0x00060373
		public virtual string GetString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x00062173 File Offset: 0x00060373
		public virtual decimal GetDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00062173 File Offset: 0x00060373
		public virtual DateTime GetDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00062173 File Offset: 0x00060373
		public virtual Guid GetGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlBoolean GetSqlBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlByte GetSqlByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlInt16 GetSqlInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlInt32 GetSqlInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlInt64 GetSqlInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlSingle GetSqlSingle(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlDouble GetSqlDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlMoney GetSqlMoney(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlDateTime GetSqlDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlDecimal GetSqlDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlString GetSqlString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlBinary GetSqlBinary(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlGuid GetSqlGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlChars GetSqlChars(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlBytes GetSqlBytes(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlXml GetSqlXml(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlXml GetSqlXmlRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlBytes GetSqlBytesRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00062173 File Offset: 0x00060373
		public virtual SqlChars GetSqlCharsRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetBoolean(int ordinal, bool value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetByte(int ordinal, byte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetChar(int ordinal, char value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetInt16(int ordinal, short value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetInt32(int ordinal, int value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetInt64(int ordinal, long value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetFloat(int ordinal, float value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetDouble(int ordinal, double value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetString(int ordinal, string value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetString(int ordinal, string value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetDecimal(int ordinal, decimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetDateTime(int ordinal, DateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetGuid(int ordinal, Guid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlBoolean(int ordinal, SqlBoolean value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlByte(int ordinal, SqlByte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlInt16(int ordinal, SqlInt16 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlInt32(int ordinal, SqlInt32 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlInt64(int ordinal, SqlInt64 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlSingle(int ordinal, SqlSingle value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlDouble(int ordinal, SqlDouble value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlMoney(int ordinal, SqlMoney value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlDateTime(int ordinal, SqlDateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlDecimal(int ordinal, SqlDecimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlString(int ordinal, SqlString value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlString(int ordinal, SqlString value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlBinary(int ordinal, SqlBinary value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlBinary(int ordinal, SqlBinary value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlGuid(int ordinal, SqlGuid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlChars(int ordinal, SqlChars value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlChars(int ordinal, SqlChars value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlBytes(int ordinal, SqlBytes value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlBytes(int ordinal, SqlBytes value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x00062173 File Offset: 0x00060373
		public virtual void SetSqlXml(int ordinal, SqlXml value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}
	}
}
