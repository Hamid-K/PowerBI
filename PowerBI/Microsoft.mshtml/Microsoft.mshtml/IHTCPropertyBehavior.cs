using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200009C RID: 156
	[Guid("3050F5DF-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTCPropertyBehavior
	{
		// Token: 0x06000D72 RID: 3442
		[DispId(-2147417612)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void fireChange();

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06000D74 RID: 3444
		// (set) Token: 0x06000D73 RID: 3443
		[DispId(-2147412971)]
		object value
		{
			[DispId(-2147412971)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412971)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}
	}
}
