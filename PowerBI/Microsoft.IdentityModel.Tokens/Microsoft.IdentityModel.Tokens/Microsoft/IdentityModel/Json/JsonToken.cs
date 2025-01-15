using System;

namespace Microsoft.IdentityModel.Json
{
	// Token: 0x02000031 RID: 49
	internal enum JsonToken
	{
		// Token: 0x040000EF RID: 239
		None,
		// Token: 0x040000F0 RID: 240
		StartObject,
		// Token: 0x040000F1 RID: 241
		StartArray,
		// Token: 0x040000F2 RID: 242
		StartConstructor,
		// Token: 0x040000F3 RID: 243
		PropertyName,
		// Token: 0x040000F4 RID: 244
		Comment,
		// Token: 0x040000F5 RID: 245
		Raw,
		// Token: 0x040000F6 RID: 246
		Integer,
		// Token: 0x040000F7 RID: 247
		Float,
		// Token: 0x040000F8 RID: 248
		String,
		// Token: 0x040000F9 RID: 249
		Boolean,
		// Token: 0x040000FA RID: 250
		Null,
		// Token: 0x040000FB RID: 251
		Undefined,
		// Token: 0x040000FC RID: 252
		EndObject,
		// Token: 0x040000FD RID: 253
		EndArray,
		// Token: 0x040000FE RID: 254
		EndConstructor,
		// Token: 0x040000FF RID: 255
		Date,
		// Token: 0x04000100 RID: 256
		Bytes
	}
}
