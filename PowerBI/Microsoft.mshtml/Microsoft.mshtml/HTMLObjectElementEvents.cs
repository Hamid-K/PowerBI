using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B1D RID: 2845
	[InterfaceType(2)]
	[TypeLibType(4112)]
	[Guid("3050F3C4-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLObjectElementEvents
	{
		// Token: 0x06012728 RID: 75560
		[DispId(-2147418108)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeupdate();

		// Token: 0x06012729 RID: 75561
		[DispId(-2147418107)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onafterupdate();

		// Token: 0x0601272A RID: 75562
		[DispId(-2147418099)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onerrorupdate();

		// Token: 0x0601272B RID: 75563
		[DispId(-2147418106)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onrowexit();

		// Token: 0x0601272C RID: 75564
		[DispId(-2147418105)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowenter();

		// Token: 0x0601272D RID: 75565
		[DispId(-2147418098)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetchanged();

		// Token: 0x0601272E RID: 75566
		[DispId(-2147418097)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondataavailable();

		// Token: 0x0601272F RID: 75567
		[DispId(-2147418096)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetcomplete();

		// Token: 0x06012730 RID: 75568
		[DispId(-2147418093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onerror();

		// Token: 0x06012731 RID: 75569
		[DispId(-2147418080)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsdelete();

		// Token: 0x06012732 RID: 75570
		[DispId(-2147418079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsinserted();

		// Token: 0x06012733 RID: 75571
		[DispId(-2147418078)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void oncellchange();

		// Token: 0x06012734 RID: 75572
		[DispId(-2147418092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onreadystatechange();
	}
}
