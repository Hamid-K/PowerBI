using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B20 RID: 2848
	[TypeLibType(4160)]
	[Guid("3050F827-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLObjectElement3
	{
		// Token: 0x17006155 RID: 24917
		// (get) Token: 0x0601275D RID: 75613
		// (set) Token: 0x0601275C RID: 75612
		[DispId(-2147415097)]
		string archive
		{
			[TypeLibFunc(20)]
			[DispId(-2147415097)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147415097)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17006156 RID: 24918
		// (get) Token: 0x0601275F RID: 75615
		// (set) Token: 0x0601275E RID: 75614
		[DispId(-2147415096)]
		string alt
		{
			[TypeLibFunc(20)]
			[DispId(-2147415096)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147415096)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17006157 RID: 24919
		// (get) Token: 0x06012761 RID: 75617
		// (set) Token: 0x06012760 RID: 75616
		[DispId(-2147415095)]
		bool declare
		{
			[TypeLibFunc(20)]
			[DispId(-2147415095)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147415095)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17006158 RID: 24920
		// (get) Token: 0x06012763 RID: 75619
		// (set) Token: 0x06012762 RID: 75618
		[DispId(-2147415094)]
		string standby
		{
			[TypeLibFunc(20)]
			[DispId(-2147415094)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415094)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17006159 RID: 24921
		// (get) Token: 0x06012765 RID: 75621
		// (set) Token: 0x06012764 RID: 75620
		[DispId(-2147415093)]
		object border
		{
			[DispId(-2147415093)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147415093)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700615A RID: 24922
		// (get) Token: 0x06012767 RID: 75623
		// (set) Token: 0x06012766 RID: 75622
		[DispId(-2147415092)]
		string useMap
		{
			[TypeLibFunc(20)]
			[DispId(-2147415092)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415092)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
