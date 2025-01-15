using System;

namespace Microsoft.BIServer.Configuration.WMI
{
	// Token: 0x0200002C RID: 44
	[CLSCompliant(false)]
	[Flags]
	public enum SecurityDescriptorControlFlags : uint
	{
		// Token: 0x04000115 RID: 277
		SE_OWNER_DEFAULTED = 1U,
		// Token: 0x04000116 RID: 278
		SE_GROUP_DEFAULTED = 2U,
		// Token: 0x04000117 RID: 279
		SE_DACL_PRESENT = 4U,
		// Token: 0x04000118 RID: 280
		SE_DACL_DEFAULTED = 8U,
		// Token: 0x04000119 RID: 281
		SE_SACL_PRESENT = 16U,
		// Token: 0x0400011A RID: 282
		SE_SACL_DEFAULTED = 32U,
		// Token: 0x0400011B RID: 283
		SE_DACL_AUTO_INHERIT_REQ = 256U,
		// Token: 0x0400011C RID: 284
		SE_SACL_AUTO_INHERIT_REQ = 512U,
		// Token: 0x0400011D RID: 285
		SE_DACL_AUTO_INHERITED = 1024U,
		// Token: 0x0400011E RID: 286
		SE_SACL_AUTO_INHERITED = 2048U,
		// Token: 0x0400011F RID: 287
		SE_DACL_PROTECTED = 4096U,
		// Token: 0x04000120 RID: 288
		SE_SACL_PROTECTED = 8192U,
		// Token: 0x04000121 RID: 289
		SE_RM_CONTROL_VALID = 16384U,
		// Token: 0x04000122 RID: 290
		SE_SELF_RELATIVE = 32768U
	}
}
