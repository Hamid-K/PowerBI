using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EAE RID: 7854
	public class ManagedDataConvert : IManagedDataConvert
	{
		// Token: 0x0600C213 RID: 49683 RVA: 0x0026FF04 File Offset: 0x0026E104
		public ManagedDataConvert(IDataConvert dataConvert)
		{
			this.dataConvert = dataConvert;
		}

		// Token: 0x0600C214 RID: 49684 RVA: 0x0026FF13 File Offset: 0x0026E113
		public int CanConvert(DBTYPE wSrcType, DBTYPE wDstType)
		{
			return this.dataConvert.CanConvert(wSrcType, wDstType);
		}

		// Token: 0x0600C215 RID: 49685 RVA: 0x0026FF22 File Offset: 0x0026E122
		public unsafe DBSTATUS DataConvert(byte srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C216 RID: 49686 RVA: 0x0026FF34 File Offset: 0x0026E134
		public unsafe DBSTATUS DataConvert(sbyte srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C217 RID: 49687 RVA: 0x0026FF46 File Offset: 0x0026E146
		public unsafe DBSTATUS DataConvert(ushort srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C218 RID: 49688 RVA: 0x0026FF58 File Offset: 0x0026E158
		public unsafe DBSTATUS DataConvert(short srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C219 RID: 49689 RVA: 0x0026FF6A File Offset: 0x0026E16A
		public unsafe DBSTATUS DataConvert(uint srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C21A RID: 49690 RVA: 0x0026FF7C File Offset: 0x0026E17C
		public unsafe DBSTATUS DataConvert(int srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C21B RID: 49691 RVA: 0x0026FF8E File Offset: 0x0026E18E
		public unsafe DBSTATUS DataConvert(ulong srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C21C RID: 49692 RVA: 0x0026FFA0 File Offset: 0x0026E1A0
		public unsafe DBSTATUS DataConvert(long srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C21D RID: 49693 RVA: 0x0026FFB2 File Offset: 0x0026E1B2
		public unsafe DBSTATUS DataConvert(float srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C21E RID: 49694 RVA: 0x0026FFC4 File Offset: 0x0026E1C4
		public unsafe DBSTATUS DataConvert(double srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C21F RID: 49695 RVA: 0x0026FFD6 File Offset: 0x0026E1D6
		public unsafe DBSTATUS DataConvert(bool srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C220 RID: 49696 RVA: 0x0026FFE8 File Offset: 0x0026E1E8
		public unsafe DBSTATUS DataConvert(decimal srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C221 RID: 49697 RVA: 0x0026FFFA File Offset: 0x0026E1FA
		public unsafe DBSTATUS DataConvert(Guid srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C222 RID: 49698 RVA: 0x0027000C File Offset: 0x0026E20C
		public unsafe DBSTATUS DataConvert(NUMERIC srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C223 RID: 49699 RVA: 0x0027001E File Offset: 0x0026E21E
		public unsafe DBSTATUS DataConvert(Date srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C224 RID: 49700 RVA: 0x00270030 File Offset: 0x0026E230
		public unsafe DBSTATUS DataConvert(DateTime srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C225 RID: 49701 RVA: 0x00270042 File Offset: 0x0026E242
		public unsafe DBSTATUS DataConvert(Time srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C226 RID: 49702 RVA: 0x00270054 File Offset: 0x0026E254
		public unsafe DBSTATUS DataConvert(DateTimeOffset srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C227 RID: 49703 RVA: 0x00270066 File Offset: 0x0026E266
		public unsafe DBSTATUS DataConvert(TimeSpan srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C228 RID: 49704 RVA: 0x00270078 File Offset: 0x0026E278
		public unsafe DBSTATUS DataConvert(byte[] srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C229 RID: 49705 RVA: 0x0027008A File Offset: 0x0026E28A
		public unsafe DBSTATUS DataConvert(string srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x0600C22A RID: 49706 RVA: 0x0027009C File Offset: 0x0026E29C
		public unsafe DBSTATUS DataConvert(ErrorWrapper srcValue, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.dataConvert.DataConvert(srcValue, binding, destValue, out destLength);
		}

		// Token: 0x040061B6 RID: 25014
		private readonly IDataConvert dataConvert;
	}
}
