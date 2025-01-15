using System;

namespace Microsoft.ReportingServices.OData.Json
{
	// Token: 0x02000017 RID: 23
	internal abstract class JsonWriterBase
	{
		// Token: 0x060000AB RID: 171
		internal abstract void StartObjectScope();

		// Token: 0x060000AC RID: 172
		internal abstract void EndObjectScope();

		// Token: 0x060000AD RID: 173
		internal abstract void StartArrayScope();

		// Token: 0x060000AE RID: 174
		internal abstract void EndArrayScope();

		// Token: 0x060000AF RID: 175
		internal abstract void WriteName(string name);

		// Token: 0x060000B0 RID: 176
		internal abstract void WriteValue(bool value);

		// Token: 0x060000B1 RID: 177
		internal abstract void WriteValue(int value);

		// Token: 0x060000B2 RID: 178
		internal abstract void WriteValue(float value);

		// Token: 0x060000B3 RID: 179
		internal abstract void WriteValue(short value);

		// Token: 0x060000B4 RID: 180
		internal abstract void WriteValue(long value);

		// Token: 0x060000B5 RID: 181
		internal abstract void WriteValue(double value);

		// Token: 0x060000B6 RID: 182
		internal abstract void WriteValue(Guid value);

		// Token: 0x060000B7 RID: 183
		internal abstract void WriteValue(decimal value);

		// Token: 0x060000B8 RID: 184
		internal abstract void WriteValue(DateTime value, ODataVersion odataVersion);

		// Token: 0x060000B9 RID: 185
		internal abstract void WriteValue(DateTimeOffset value, ODataVersion odataVersion);

		// Token: 0x060000BA RID: 186
		internal abstract void WriteValue(TimeSpan value);

		// Token: 0x060000BB RID: 187
		internal abstract void WriteValue(byte value);

		// Token: 0x060000BC RID: 188
		internal abstract void WriteValue(sbyte value);

		// Token: 0x060000BD RID: 189
		internal abstract void WriteValue(string value);

		// Token: 0x060000BE RID: 190
		internal abstract void WriteValue(byte[] value);

		// Token: 0x060000BF RID: 191
		internal abstract void WriteRawValue(string value);
	}
}
