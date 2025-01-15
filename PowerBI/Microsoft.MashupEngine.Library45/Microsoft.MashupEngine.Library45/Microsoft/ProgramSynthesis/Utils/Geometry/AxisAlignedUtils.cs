using System;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x0200059D RID: 1437
	public static class AxisAlignedUtils
	{
		// Token: 0x06001F5A RID: 8026 RVA: 0x0005A0FB File Offset: 0x000582FB
		public static AxisAligned<TResult> Select<T, TResult>(this AxisAligned<T> xs, Func<T, TResult> func)
		{
			return new AxisAligned<TResult>((Axis axis) => func(xs[axis]));
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x0005A120 File Offset: 0x00058320
		public static AxisAligned<TResult> Select<T, TResult>(this AxisAligned<T> xs, Func<Axis, T, TResult> func)
		{
			return new AxisAligned<TResult>((Axis axis) => func(axis, xs[axis]));
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x0005A148 File Offset: 0x00058348
		public static bool All<T>(this AxisAligned<T> xs, Func<Axis, T, bool> func)
		{
			return AxisUtilities.Axes.All((Axis axis) => func(axis, xs[axis]));
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x0005A180 File Offset: 0x00058380
		public static bool All<T>(this AxisAligned<T> xs, Func<T, bool> func)
		{
			return AxisUtilities.Axes.All((Axis axis) => func(xs[axis]));
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0005A1B8 File Offset: 0x000583B8
		public static bool Any<T>(this AxisAligned<T> xs, Func<Axis, T, bool> func)
		{
			return AxisUtilities.Axes.Any((Axis axis) => func(axis, xs[axis]));
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x0005A1F0 File Offset: 0x000583F0
		public static bool Any<T>(this AxisAligned<T> xs, Func<T, bool> func)
		{
			return AxisUtilities.Axes.Any((Axis axis) => func(xs[axis]));
		}
	}
}
