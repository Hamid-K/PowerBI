using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs
{
	// Token: 0x02000192 RID: 402
	internal static class WindowsDpiHelper
	{
		// Token: 0x060012EB RID: 4843 RVA: 0x00040140 File Offset: 0x0003E340
		static WindowsDpiHelper()
		{
			IntPtr dc = WindowsDpiHelper.GetDC(IntPtr.Zero);
			double num;
			double num2;
			if (dc != IntPtr.Zero)
			{
				num = (double)WindowsDpiHelper.GetDeviceCaps(dc, 88);
				num2 = (double)WindowsDpiHelper.GetDeviceCaps(dc, 90);
				WindowsDpiHelper.ReleaseDC(IntPtr.Zero, dc);
			}
			else
			{
				num = 96.0;
				num2 = 96.0;
			}
			int num3 = (int)(100.0 * (num / 96.0));
			int num4 = (int)(100.0 * (num2 / 96.0));
			WindowsDpiHelper.ZoomPercent = Math.Min(num3, num4);
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x000401D3 File Offset: 0x0003E3D3
		public static int ZoomPercent { get; }

		// Token: 0x060012ED RID: 4845
		[DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
		internal static extern IntPtr GetDC(IntPtr hWnd);

		// Token: 0x060012EE RID: 4846
		[DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
		internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		// Token: 0x060012EF RID: 4847
		[DllImport("Gdi32.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
		internal static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

		// Token: 0x060012F0 RID: 4848
		[DllImport("User32.dll", ExactSpelling = true)]
		internal static extern bool IsProcessDPIAware();
	}
}
