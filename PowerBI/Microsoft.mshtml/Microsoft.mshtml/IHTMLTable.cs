using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009F5 RID: 2549
	[TypeLibType(4160)]
	[Guid("3050F21E-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLTable
	{
		// Token: 0x1700555C RID: 21852
		// (get) Token: 0x06010391 RID: 66449
		// (set) Token: 0x06010390 RID: 66448
		[DispId(1001)]
		int cols
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700555D RID: 21853
		// (get) Token: 0x06010393 RID: 66451
		// (set) Token: 0x06010392 RID: 66450
		[DispId(1002)]
		object border
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700555E RID: 21854
		// (get) Token: 0x06010395 RID: 66453
		// (set) Token: 0x06010394 RID: 66452
		[DispId(1004)]
		string frame
		{
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700555F RID: 21855
		// (get) Token: 0x06010397 RID: 66455
		// (set) Token: 0x06010396 RID: 66454
		[DispId(1003)]
		string rules
		{
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005560 RID: 21856
		// (get) Token: 0x06010399 RID: 66457
		// (set) Token: 0x06010398 RID: 66456
		[DispId(1005)]
		object cellSpacing
		{
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005561 RID: 21857
		// (get) Token: 0x0601039B RID: 66459
		// (set) Token: 0x0601039A RID: 66458
		[DispId(1006)]
		object cellPadding
		{
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005562 RID: 21858
		// (get) Token: 0x0601039D RID: 66461
		// (set) Token: 0x0601039C RID: 66460
		[DispId(-2147413111)]
		string background
		{
			[DispId(-2147413111)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413111)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005563 RID: 21859
		// (get) Token: 0x0601039F RID: 66463
		// (set) Token: 0x0601039E RID: 66462
		[DispId(-501)]
		object bgColor
		{
			[DispId(-501)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-501)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005564 RID: 21860
		// (get) Token: 0x060103A1 RID: 66465
		// (set) Token: 0x060103A0 RID: 66464
		[DispId(-2147413084)]
		object borderColor
		{
			[DispId(-2147413084)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413084)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005565 RID: 21861
		// (get) Token: 0x060103A3 RID: 66467
		// (set) Token: 0x060103A2 RID: 66466
		[DispId(-2147413083)]
		object borderColorLight
		{
			[DispId(-2147413083)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413083)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005566 RID: 21862
		// (get) Token: 0x060103A5 RID: 66469
		// (set) Token: 0x060103A4 RID: 66468
		[DispId(-2147413082)]
		object borderColorDark
		{
			[DispId(-2147413082)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413082)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005567 RID: 21863
		// (get) Token: 0x060103A7 RID: 66471
		// (set) Token: 0x060103A6 RID: 66470
		[DispId(-2147418039)]
		string align
		{
			[DispId(-2147418039)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418039)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x060103A8 RID: 66472
		[DispId(1015)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void refresh();

		// Token: 0x17005568 RID: 21864
		// (get) Token: 0x060103A9 RID: 66473
		[DispId(1016)]
		IHTMLElementCollection rows
		{
			[DispId(1016)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005569 RID: 21865
		// (get) Token: 0x060103AB RID: 66475
		// (set) Token: 0x060103AA RID: 66474
		[DispId(-2147418107)]
		object width
		{
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700556A RID: 21866
		// (get) Token: 0x060103AD RID: 66477
		// (set) Token: 0x060103AC RID: 66476
		[DispId(-2147418106)]
		object height
		{
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700556B RID: 21867
		// (get) Token: 0x060103AF RID: 66479
		// (set) Token: 0x060103AE RID: 66478
		[DispId(1017)]
		int dataPageSize
		{
			[DispId(1017)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1017)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x060103B0 RID: 66480
		[DispId(1018)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void nextPage();

		// Token: 0x060103B1 RID: 66481
		[DispId(1019)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void previousPage();

		// Token: 0x1700556C RID: 21868
		// (get) Token: 0x060103B2 RID: 66482
		[DispId(1020)]
		IHTMLTableSection tHead
		{
			[DispId(1020)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700556D RID: 21869
		// (get) Token: 0x060103B3 RID: 66483
		[DispId(1021)]
		IHTMLTableSection tFoot
		{
			[DispId(1021)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700556E RID: 21870
		// (get) Token: 0x060103B4 RID: 66484
		[DispId(1024)]
		IHTMLElementCollection tBodies
		{
			[DispId(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700556F RID: 21871
		// (get) Token: 0x060103B5 RID: 66485
		[DispId(1025)]
		IHTMLTableCaption caption
		{
			[DispId(1025)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060103B6 RID: 66486
		[DispId(1026)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object createTHead();

		// Token: 0x060103B7 RID: 66487
		[DispId(1027)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void deleteTHead();

		// Token: 0x060103B8 RID: 66488
		[DispId(1028)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object createTFoot();

		// Token: 0x060103B9 RID: 66489
		[DispId(1029)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void deleteTFoot();

		// Token: 0x060103BA RID: 66490
		[DispId(1030)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLTableCaption createCaption();

		// Token: 0x060103BB RID: 66491
		[DispId(1031)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void deleteCaption();

		// Token: 0x060103BC RID: 66492
		[DispId(1032)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object insertRow([In] int index = -1);

		// Token: 0x060103BD RID: 66493
		[DispId(1033)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void deleteRow([In] int index = -1);

		// Token: 0x17005570 RID: 21872
		// (get) Token: 0x060103BE RID: 66494
		[DispId(-2147412996)]
		string readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005571 RID: 21873
		// (get) Token: 0x060103C0 RID: 66496
		// (set) Token: 0x060103BF RID: 66495
		[DispId(-2147412087)]
		object onreadystatechange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}
	}
}
