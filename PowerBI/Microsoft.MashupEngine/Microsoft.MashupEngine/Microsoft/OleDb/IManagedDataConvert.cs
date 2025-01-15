using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EA7 RID: 7847
	public interface IManagedDataConvert
	{
		// Token: 0x0600C1F7 RID: 49655
		int CanConvert(DBTYPE wSrcType, DBTYPE wDstType);

		// Token: 0x0600C1F8 RID: 49656
		unsafe DBSTATUS DataConvert(byte value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C1F9 RID: 49657
		unsafe DBSTATUS DataConvert(sbyte value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C1FA RID: 49658
		unsafe DBSTATUS DataConvert(ushort value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C1FB RID: 49659
		unsafe DBSTATUS DataConvert(short value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C1FC RID: 49660
		unsafe DBSTATUS DataConvert(uint value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C1FD RID: 49661
		unsafe DBSTATUS DataConvert(int value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C1FE RID: 49662
		unsafe DBSTATUS DataConvert(ulong value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C1FF RID: 49663
		unsafe DBSTATUS DataConvert(long value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C200 RID: 49664
		unsafe DBSTATUS DataConvert(bool value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C201 RID: 49665
		unsafe DBSTATUS DataConvert(float value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C202 RID: 49666
		unsafe DBSTATUS DataConvert(double value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C203 RID: 49667
		unsafe DBSTATUS DataConvert(decimal value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C204 RID: 49668
		unsafe DBSTATUS DataConvert(NUMERIC value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C205 RID: 49669
		unsafe DBSTATUS DataConvert(Guid value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C206 RID: 49670
		unsafe DBSTATUS DataConvert(TimeSpan value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C207 RID: 49671
		unsafe DBSTATUS DataConvert(Date value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C208 RID: 49672
		unsafe DBSTATUS DataConvert(DateTime value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C209 RID: 49673
		unsafe DBSTATUS DataConvert(Time value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C20A RID: 49674
		unsafe DBSTATUS DataConvert(DateTimeOffset value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C20B RID: 49675
		unsafe DBSTATUS DataConvert(byte[] value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C20C RID: 49676
		unsafe DBSTATUS DataConvert(string value, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600C20D RID: 49677
		unsafe DBSTATUS DataConvert(ErrorWrapper value, Binding binding, byte* destValue, out DBLENGTH destLength);
	}
}
