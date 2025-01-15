using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CF4 RID: 3316
	[ClassInterface(0)]
	[Guid("3050F5EB-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class ThreadDialogProcParamClass : IHTMLModelessInit, ThreadDialogProcParam
	{
		// Token: 0x06016386 RID: 91014
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern ThreadDialogProcParamClass();

		// Token: 0x170075D5 RID: 30165
		// (get) Token: 0x06016387 RID: 91015
		[DispId(25000)]
		public virtual extern object parameters
		{
			[DispId(25000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170075D6 RID: 30166
		// (get) Token: 0x06016388 RID: 91016
		[DispId(25001)]
		public virtual extern object optionString
		{
			[DispId(25001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170075D7 RID: 30167
		// (get) Token: 0x06016389 RID: 91017
		[DispId(25006)]
		public virtual extern object moniker
		{
			[DispId(25006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(25)]
			get;
		}

		// Token: 0x170075D8 RID: 30168
		// (get) Token: 0x0601638A RID: 91018
		[DispId(25007)]
		public virtual extern object document
		{
			[DispId(25007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(25)]
			get;
		}
	}
}
