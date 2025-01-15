using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001E9F RID: 7839
	public static class IDataConvertExtensions
	{
		// Token: 0x0600C1C5 RID: 49605 RVA: 0x0026F848 File Offset: 0x0026DA48
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, byte srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return dataConvert.DataConvert(DBTYPE.UI1, DbLength.One, (void*)(&srcValue), binding, destValue, out destLength);
		}

		// Token: 0x0600C1C6 RID: 49606 RVA: 0x0026F85E File Offset: 0x0026DA5E
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, sbyte srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return dataConvert.DataConvert(DBTYPE.I1, DbLength.One, (void*)(&srcValue), binding, destValue, out destLength);
		}

		// Token: 0x0600C1C7 RID: 49607 RVA: 0x0026F874 File Offset: 0x0026DA74
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, ushort srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return dataConvert.DataConvert(DBTYPE.UI2, DbLength.Two, (void*)(&srcValue), binding, destValue, out destLength);
		}

		// Token: 0x0600C1C8 RID: 49608 RVA: 0x0026F88A File Offset: 0x0026DA8A
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, short srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return dataConvert.DataConvert(DBTYPE.I2, DbLength.Two, (void*)(&srcValue), binding, destValue, out destLength);
		}

		// Token: 0x0600C1C9 RID: 49609 RVA: 0x0026F89F File Offset: 0x0026DA9F
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, uint srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return dataConvert.DataConvert(DBTYPE.UI4, DbLength.Four, (void*)(&srcValue), binding, destValue, out destLength);
		}

		// Token: 0x0600C1CA RID: 49610 RVA: 0x0026F8B5 File Offset: 0x0026DAB5
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, int srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return dataConvert.DataConvert(DBTYPE.I4, DbLength.Four, (void*)(&srcValue), binding, destValue, out destLength);
		}

		// Token: 0x0600C1CB RID: 49611 RVA: 0x0026F8CA File Offset: 0x0026DACA
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, ulong srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return dataConvert.DataConvert(DBTYPE.UI8, DbLength.Eight, (void*)(&srcValue), binding, destValue, out destLength);
		}

		// Token: 0x0600C1CC RID: 49612 RVA: 0x0026F8E0 File Offset: 0x0026DAE0
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, long srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return dataConvert.DataConvert(DBTYPE.I8, DbLength.Eight, (void*)(&srcValue), binding, destValue, out destLength);
		}

		// Token: 0x0600C1CD RID: 49613 RVA: 0x0026F8F6 File Offset: 0x0026DAF6
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, float srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return dataConvert.DataConvert(DBTYPE.R4, DbLength.Four, (void*)(&srcValue), binding, destValue, out destLength);
		}

		// Token: 0x0600C1CE RID: 49614 RVA: 0x0026F90B File Offset: 0x0026DB0B
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, double srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return dataConvert.DataConvert(DBTYPE.R8, DbLength.Eight, (void*)(&srcValue), binding, destValue, out destLength);
		}

		// Token: 0x0600C1CF RID: 49615 RVA: 0x0026F920 File Offset: 0x0026DB20
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, bool srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			VARIANT_BOOL variant_BOOL = (srcValue ? VARIANT_BOOL.TRUE : VARIANT_BOOL.FALSE);
			return dataConvert.DataConvert(DBTYPE.BOOL, DbLength.Two, (void*)(&variant_BOOL), binding, destValue, out destLength);
		}

		// Token: 0x0600C1D0 RID: 49616 RVA: 0x0026F94D File Offset: 0x0026DB4D
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, decimal srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return dataConvert.DataConvert(DBTYPE.DECIMAL, DbLength.Decimal, (void*)(&srcValue), binding, destValue, out destLength);
		}

		// Token: 0x0600C1D1 RID: 49617 RVA: 0x0026F963 File Offset: 0x0026DB63
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, Guid guid, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return dataConvert.DataConvert(DBTYPE.GUID, DbLength.Guid, (void*)(&guid), binding, destValue, out destLength);
		}

		// Token: 0x0600C1D2 RID: 49618 RVA: 0x0026F979 File Offset: 0x0026DB79
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, NUMERIC srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return dataConvert.DataConvert(DBTYPE.NUMERIC, DbLength.Numeric, (void*)(&srcValue), binding, destValue, out destLength);
		}

		// Token: 0x0600C1D3 RID: 49619 RVA: 0x0026F994 File Offset: 0x0026DB94
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, Date date, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			DBDATE date2 = OleDbConvert.GetDate(date);
			return dataConvert.DataConvert(DBTYPE.DBDATE, DbLength.DbDate, (void*)(&date2), binding, destValue, out destLength);
		}

		// Token: 0x0600C1D4 RID: 49620 RVA: 0x0026F9C0 File Offset: 0x0026DBC0
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, DateTime dateTime, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			DBTIMESTAMP timeStamp = OleDbConvert.GetTimeStamp(dateTime);
			return dataConvert.DataConvert(DBTYPE.DBTIMESTAMP, DbLength.TimeStamp, (void*)(&timeStamp), binding, destValue, out destLength);
		}

		// Token: 0x0600C1D5 RID: 49621 RVA: 0x0026F9EC File Offset: 0x0026DBEC
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, Time time, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			DBTIME2 time2 = OleDbConvert.GetTime(time);
			return dataConvert.DataConvert(DBTYPE.DBTIME2, DbLength.DbTime2, (void*)(&time2), binding, destValue, out destLength);
		}

		// Token: 0x0600C1D6 RID: 49622 RVA: 0x0026FA18 File Offset: 0x0026DC18
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, DateTimeOffset dateTimeOffset, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			DBTIMESTAMPOFFSET timeStampOffset = OleDbConvert.GetTimeStampOffset(dateTimeOffset);
			return dataConvert.DataConvert(DBTYPE.DBTIMESTAMPOFFSET, DbLength.TimeStampOffset, (void*)(&timeStampOffset), binding, destValue, out destLength);
		}

		// Token: 0x0600C1D7 RID: 49623 RVA: 0x0026FA44 File Offset: 0x0026DC44
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, TimeSpan timeSpan, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			DBDURATION dbduration;
			dbduration.ticks = timeSpan.Ticks;
			return dataConvert.DataConvert(DBTYPE.DBDURATION, DbLength.Duration, (void*)(&dbduration), binding, destValue, out destLength);
		}

		// Token: 0x0600C1D8 RID: 49624 RVA: 0x0026FA78 File Offset: 0x0026DC78
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, byte[] bytes, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			DBLENGTH length = DbLength.GetLength(bytes);
			byte b;
			return dataConvert.DataConvert(DBTYPE.BYTES, length, (void*)((ptr != null) ? ptr : (&b)), binding, destValue, out destLength);
		}

		// Token: 0x0600C1D9 RID: 49625 RVA: 0x0026FAC0 File Offset: 0x0026DCC0
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, string value, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			char* ptr = value;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			DBLENGTH length = DbLength.GetLength(value);
			return dataConvert.DataConvert(DBTYPE.WSTR, length, (void*)ptr, binding, destValue, out destLength);
		}

		// Token: 0x0600C1DA RID: 49626 RVA: 0x0026FAF8 File Offset: 0x0026DCF8
		public unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, ErrorWrapper value, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			int errorCode = value.ErrorCode;
			return dataConvert.DataConvert(DBTYPE.ERROR, DbLength.Four, (void*)(&errorCode), binding, destValue, out destLength);
		}

		// Token: 0x0600C1DB RID: 49627 RVA: 0x0026FB20 File Offset: 0x0026DD20
		private unsafe static DBSTATUS DataConvert(this IDataConvert dataConvert, DBTYPE srcType, DBLENGTH srcLength, void* srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			DBSTATUS dbstatus;
			dataConvert.DataConvert(srcType, binding.DestType, srcLength, out destLength, srcValue, (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
			return dbstatus;
		}
	}
}
