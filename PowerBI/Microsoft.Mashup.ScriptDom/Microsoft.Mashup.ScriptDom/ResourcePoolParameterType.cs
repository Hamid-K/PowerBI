using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000AC RID: 172
	[Serializable]
	internal enum ResourcePoolParameterType
	{
		// Token: 0x040003F1 RID: 1009
		Unknown,
		// Token: 0x040003F2 RID: 1010
		MaxCpuPercent,
		// Token: 0x040003F3 RID: 1011
		MaxMemoryPercent,
		// Token: 0x040003F4 RID: 1012
		MinCpuPercent,
		// Token: 0x040003F5 RID: 1013
		MinMemoryPercent,
		// Token: 0x040003F6 RID: 1014
		CapCpuPercent,
		// Token: 0x040003F7 RID: 1015
		TargetMemoryPercent,
		// Token: 0x040003F8 RID: 1016
		MinIoPercent,
		// Token: 0x040003F9 RID: 1017
		MaxIoPercent,
		// Token: 0x040003FA RID: 1018
		CapIoPercent,
		// Token: 0x040003FB RID: 1019
		Affinity
	}
}
