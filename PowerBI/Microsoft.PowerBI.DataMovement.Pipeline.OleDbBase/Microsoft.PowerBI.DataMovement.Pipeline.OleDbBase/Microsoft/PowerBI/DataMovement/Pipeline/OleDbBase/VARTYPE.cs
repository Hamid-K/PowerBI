using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200004F RID: 79
	[CLSCompliant(false)]
	public enum VARTYPE : ushort
	{
		// Token: 0x0400009B RID: 155
		EMPTY,
		// Token: 0x0400009C RID: 156
		NULL,
		// Token: 0x0400009D RID: 157
		I2,
		// Token: 0x0400009E RID: 158
		I4,
		// Token: 0x0400009F RID: 159
		R4,
		// Token: 0x040000A0 RID: 160
		R8,
		// Token: 0x040000A1 RID: 161
		CY,
		// Token: 0x040000A2 RID: 162
		DATE,
		// Token: 0x040000A3 RID: 163
		BSTR,
		// Token: 0x040000A4 RID: 164
		DISPATCH,
		// Token: 0x040000A5 RID: 165
		ERROR,
		// Token: 0x040000A6 RID: 166
		BOOL,
		// Token: 0x040000A7 RID: 167
		VARIANT,
		// Token: 0x040000A8 RID: 168
		UNKNOWN,
		// Token: 0x040000A9 RID: 169
		DECIMAL,
		// Token: 0x040000AA RID: 170
		I1 = 16,
		// Token: 0x040000AB RID: 171
		UI1,
		// Token: 0x040000AC RID: 172
		UI2,
		// Token: 0x040000AD RID: 173
		UI4,
		// Token: 0x040000AE RID: 174
		I8,
		// Token: 0x040000AF RID: 175
		UI8,
		// Token: 0x040000B0 RID: 176
		INT,
		// Token: 0x040000B1 RID: 177
		UINT,
		// Token: 0x040000B2 RID: 178
		VOID,
		// Token: 0x040000B3 RID: 179
		HRESULT,
		// Token: 0x040000B4 RID: 180
		PTR,
		// Token: 0x040000B5 RID: 181
		SAFEARRAY,
		// Token: 0x040000B6 RID: 182
		CARRAY,
		// Token: 0x040000B7 RID: 183
		USERDEFINED,
		// Token: 0x040000B8 RID: 184
		LPSTR,
		// Token: 0x040000B9 RID: 185
		LPWSTR,
		// Token: 0x040000BA RID: 186
		RECORD = 36,
		// Token: 0x040000BB RID: 187
		INT_PTR,
		// Token: 0x040000BC RID: 188
		UINT_PTR,
		// Token: 0x040000BD RID: 189
		FILETIME = 64,
		// Token: 0x040000BE RID: 190
		BLOB,
		// Token: 0x040000BF RID: 191
		STREAM,
		// Token: 0x040000C0 RID: 192
		STORAGE,
		// Token: 0x040000C1 RID: 193
		STREAMED_OBJECT,
		// Token: 0x040000C2 RID: 194
		STORED_OBJECT,
		// Token: 0x040000C3 RID: 195
		BLOB_OBJECT,
		// Token: 0x040000C4 RID: 196
		CF,
		// Token: 0x040000C5 RID: 197
		CLSID,
		// Token: 0x040000C6 RID: 198
		VERSIONED_STREAM,
		// Token: 0x040000C7 RID: 199
		BSTR_BLOB = 4095,
		// Token: 0x040000C8 RID: 200
		VECTOR,
		// Token: 0x040000C9 RID: 201
		ARRAY = 8192,
		// Token: 0x040000CA RID: 202
		BYREF = 16384,
		// Token: 0x040000CB RID: 203
		RESERVED = 32768,
		// Token: 0x040000CC RID: 204
		ILLEGAL = 65535,
		// Token: 0x040000CD RID: 205
		ILLEGALMASKED = 4095,
		// Token: 0x040000CE RID: 206
		TYPEMASK = 4095,
		// Token: 0x040000CF RID: 207
		Int3264 = 20
	}
}
