using System;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005A4 RID: 1444
	public static class AxisAligned
	{
		// Token: 0x06001F6C RID: 8044 RVA: 0x0005A2C0 File Offset: 0x000584C0
		public static AxisAligned<T> Create<T>(T horizontal, T vertical)
		{
			return new AxisAligned<T>(horizontal, vertical);
		}
	}
}
