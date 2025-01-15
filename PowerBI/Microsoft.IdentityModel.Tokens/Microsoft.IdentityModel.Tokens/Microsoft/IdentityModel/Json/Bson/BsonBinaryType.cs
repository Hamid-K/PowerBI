using System;

namespace Microsoft.IdentityModel.Json.Bson
{
	// Token: 0x02000105 RID: 261
	internal enum BsonBinaryType : byte
	{
		// Token: 0x0400041E RID: 1054
		Binary,
		// Token: 0x0400041F RID: 1055
		Function,
		// Token: 0x04000420 RID: 1056
		[Obsolete("This type has been deprecated in the BSON specification. Use Binary instead.")]
		BinaryOld,
		// Token: 0x04000421 RID: 1057
		[Obsolete("This type has been deprecated in the BSON specification. Use Uuid instead.")]
		UuidOld,
		// Token: 0x04000422 RID: 1058
		Uuid,
		// Token: 0x04000423 RID: 1059
		Md5,
		// Token: 0x04000424 RID: 1060
		UserDefined = 128
	}
}
