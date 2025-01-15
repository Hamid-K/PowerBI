using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003106 RID: 12550
	internal enum ParticleType : byte
	{
		// Token: 0x0400B48C RID: 46220
		Element,
		// Token: 0x0400B48D RID: 46221
		All,
		// Token: 0x0400B48E RID: 46222
		Any,
		// Token: 0x0400B48F RID: 46223
		Choice,
		// Token: 0x0400B490 RID: 46224
		Group,
		// Token: 0x0400B491 RID: 46225
		Sequence,
		// Token: 0x0400B492 RID: 46226
		AnyWithUri,
		// Token: 0x0400B493 RID: 46227
		Invalid = 255
	}
}
