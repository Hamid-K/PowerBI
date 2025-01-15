using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007AC RID: 1964
	[Guid("3050F48B-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLEventObj2
	{
		// Token: 0x0600D500 RID: 54528
		[DispId(-2147417611)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600D501 RID: 54529
		[DispId(-2147417610)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600D502 RID: 54530
		[DispId(-2147417609)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x1700466C RID: 18028
		// (get) Token: 0x0600D504 RID: 54532
		// (set) Token: 0x0600D503 RID: 54531
		[DispId(1027)]
		string propertyName
		{
			[DispId(1027)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1027)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700466D RID: 18029
		// (get) Token: 0x0600D506 RID: 54534
		// (set) Token: 0x0600D505 RID: 54533
		[DispId(1031)]
		IHTMLBookmarkCollection bookmarks
		{
			[DispId(1031)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[DispId(1031)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			[param: In]
			set;
		}

		// Token: 0x1700466E RID: 18030
		// (get) Token: 0x0600D508 RID: 54536
		// (set) Token: 0x0600D507 RID: 54535
		[DispId(1032)]
		object recordset
		{
			[DispId(1032)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
			[DispId(1032)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(26)]
			[param: In]
			set;
		}

		// Token: 0x1700466F RID: 18031
		// (get) Token: 0x0600D50A RID: 54538
		// (set) Token: 0x0600D509 RID: 54537
		[DispId(1033)]
		string dataFld
		{
			[DispId(1033)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1033)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004670 RID: 18032
		// (get) Token: 0x0600D50C RID: 54540
		// (set) Token: 0x0600D50B RID: 54539
		[DispId(1034)]
		IHTMLElementCollection boundElements
		{
			[DispId(1034)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[DispId(1034)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			[param: In]
			set;
		}

		// Token: 0x17004671 RID: 18033
		// (get) Token: 0x0600D50E RID: 54542
		// (set) Token: 0x0600D50D RID: 54541
		[DispId(1035)]
		bool repeat
		{
			[DispId(1035)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1035)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004672 RID: 18034
		// (get) Token: 0x0600D510 RID: 54544
		// (set) Token: 0x0600D50F RID: 54543
		[DispId(1036)]
		string srcUrn
		{
			[DispId(1036)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1036)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004673 RID: 18035
		// (get) Token: 0x0600D512 RID: 54546
		// (set) Token: 0x0600D511 RID: 54545
		[DispId(1001)]
		IHTMLElement srcElement
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			[param: In]
			set;
		}

		// Token: 0x17004674 RID: 18036
		// (get) Token: 0x0600D514 RID: 54548
		// (set) Token: 0x0600D513 RID: 54547
		[DispId(1002)]
		bool altKey
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004675 RID: 18037
		// (get) Token: 0x0600D516 RID: 54550
		// (set) Token: 0x0600D515 RID: 54549
		[DispId(1003)]
		bool ctrlKey
		{
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004676 RID: 18038
		// (get) Token: 0x0600D518 RID: 54552
		// (set) Token: 0x0600D517 RID: 54551
		[DispId(1004)]
		bool shiftKey
		{
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004677 RID: 18039
		// (get) Token: 0x0600D51A RID: 54554
		// (set) Token: 0x0600D519 RID: 54553
		[DispId(1009)]
		IHTMLElement fromElement
		{
			[DispId(1009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[DispId(1009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			[param: In]
			set;
		}

		// Token: 0x17004678 RID: 18040
		// (get) Token: 0x0600D51C RID: 54556
		// (set) Token: 0x0600D51B RID: 54555
		[DispId(1010)]
		IHTMLElement toElement
		{
			[DispId(1010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[DispId(1010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			[param: In]
			set;
		}

		// Token: 0x17004679 RID: 18041
		// (get) Token: 0x0600D51E RID: 54558
		// (set) Token: 0x0600D51D RID: 54557
		[DispId(1012)]
		int button
		{
			[DispId(1012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700467A RID: 18042
		// (get) Token: 0x0600D520 RID: 54560
		// (set) Token: 0x0600D51F RID: 54559
		[DispId(1013)]
		string type
		{
			[DispId(1013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700467B RID: 18043
		// (get) Token: 0x0600D522 RID: 54562
		// (set) Token: 0x0600D521 RID: 54561
		[DispId(1014)]
		string qualifier
		{
			[DispId(1014)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1014)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700467C RID: 18044
		// (get) Token: 0x0600D524 RID: 54564
		// (set) Token: 0x0600D523 RID: 54563
		[DispId(1015)]
		int reason
		{
			[DispId(1015)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1015)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700467D RID: 18045
		// (get) Token: 0x0600D526 RID: 54566
		// (set) Token: 0x0600D525 RID: 54565
		[DispId(1005)]
		int x
		{
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700467E RID: 18046
		// (get) Token: 0x0600D528 RID: 54568
		// (set) Token: 0x0600D527 RID: 54567
		[DispId(1006)]
		int y
		{
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700467F RID: 18047
		// (get) Token: 0x0600D52A RID: 54570
		// (set) Token: 0x0600D529 RID: 54569
		[DispId(1020)]
		int clientX
		{
			[DispId(1020)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1020)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004680 RID: 18048
		// (get) Token: 0x0600D52C RID: 54572
		// (set) Token: 0x0600D52B RID: 54571
		[DispId(1021)]
		int clientY
		{
			[DispId(1021)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1021)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004681 RID: 18049
		// (get) Token: 0x0600D52E RID: 54574
		// (set) Token: 0x0600D52D RID: 54573
		[DispId(1022)]
		int offsetX
		{
			[DispId(1022)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1022)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004682 RID: 18050
		// (get) Token: 0x0600D530 RID: 54576
		// (set) Token: 0x0600D52F RID: 54575
		[DispId(1023)]
		int offsetY
		{
			[DispId(1023)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1023)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004683 RID: 18051
		// (get) Token: 0x0600D532 RID: 54578
		// (set) Token: 0x0600D531 RID: 54577
		[DispId(1024)]
		int screenX
		{
			[DispId(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004684 RID: 18052
		// (get) Token: 0x0600D534 RID: 54580
		// (set) Token: 0x0600D533 RID: 54579
		[DispId(1025)]
		int screenY
		{
			[DispId(1025)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1025)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004685 RID: 18053
		// (get) Token: 0x0600D536 RID: 54582
		// (set) Token: 0x0600D535 RID: 54581
		[DispId(1026)]
		object srcFilter
		{
			[DispId(1026)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
			[DispId(1026)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(26)]
			[param: In]
			set;
		}

		// Token: 0x17004686 RID: 18054
		// (get) Token: 0x0600D537 RID: 54583
		[DispId(1037)]
		IHTMLDataTransfer dataTransfer
		{
			[DispId(1037)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}
	}
}
