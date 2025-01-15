using System;

namespace System.Text.Json
{
	// Token: 0x02000041 RID: 65
	public enum JsonTokenType : byte
	{
		// Token: 0x04000146 RID: 326
		None,
		// Token: 0x04000147 RID: 327
		StartObject,
		// Token: 0x04000148 RID: 328
		EndObject,
		// Token: 0x04000149 RID: 329
		StartArray,
		// Token: 0x0400014A RID: 330
		EndArray,
		// Token: 0x0400014B RID: 331
		PropertyName,
		// Token: 0x0400014C RID: 332
		Comment,
		// Token: 0x0400014D RID: 333
		String,
		// Token: 0x0400014E RID: 334
		Number,
		// Token: 0x0400014F RID: 335
		True,
		// Token: 0x04000150 RID: 336
		False,
		// Token: 0x04000151 RID: 337
		Null
	}
}
