using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000A02 RID: 2562
	[InterfaceType(2)]
	[TypeLibType(4112)]
	[Guid("3050F532-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface DispHTMLTable
	{
		// Token: 0x06010418 RID: 66584
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06010419 RID: 66585
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0601041A RID: 66586
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x1700559F RID: 21919
		// (get) Token: 0x0601041C RID: 66588
		// (set) Token: 0x0601041B RID: 66587
		[DispId(-2147417111)]
		string className
		{
			[TypeLibFunc(4)]
			[DispId(-2147417111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417111)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170055A0 RID: 21920
		// (get) Token: 0x0601041E RID: 66590
		// (set) Token: 0x0601041D RID: 66589
		[DispId(-2147417110)]
		string id
		{
			[DispId(-2147417110)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147417110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170055A1 RID: 21921
		// (get) Token: 0x0601041F RID: 66591
		[DispId(-2147417108)]
		string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170055A2 RID: 21922
		// (get) Token: 0x06010420 RID: 66592
		[DispId(-2147418104)]
		IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170055A3 RID: 21923
		// (get) Token: 0x06010421 RID: 66593
		[DispId(-2147418038)]
		IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170055A4 RID: 21924
		// (get) Token: 0x06010423 RID: 66595
		// (set) Token: 0x06010422 RID: 66594
		[DispId(-2147412099)]
		object onhelp
		{
			[TypeLibFunc(20)]
			[DispId(-2147412099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055A5 RID: 21925
		// (get) Token: 0x06010425 RID: 66597
		// (set) Token: 0x06010424 RID: 66596
		[DispId(-2147412104)]
		object onclick
		{
			[DispId(-2147412104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055A6 RID: 21926
		// (get) Token: 0x06010427 RID: 66599
		// (set) Token: 0x06010426 RID: 66598
		[DispId(-2147412103)]
		object ondblclick
		{
			[TypeLibFunc(20)]
			[DispId(-2147412103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412103)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055A7 RID: 21927
		// (get) Token: 0x06010429 RID: 66601
		// (set) Token: 0x06010428 RID: 66600
		[DispId(-2147412107)]
		object onkeydown
		{
			[TypeLibFunc(20)]
			[DispId(-2147412107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055A8 RID: 21928
		// (get) Token: 0x0601042B RID: 66603
		// (set) Token: 0x0601042A RID: 66602
		[DispId(-2147412106)]
		object onkeyup
		{
			[DispId(-2147412106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055A9 RID: 21929
		// (get) Token: 0x0601042D RID: 66605
		// (set) Token: 0x0601042C RID: 66604
		[DispId(-2147412105)]
		object onkeypress
		{
			[DispId(-2147412105)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055AA RID: 21930
		// (get) Token: 0x0601042F RID: 66607
		// (set) Token: 0x0601042E RID: 66606
		[DispId(-2147412111)]
		object onmouseout
		{
			[DispId(-2147412111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055AB RID: 21931
		// (get) Token: 0x06010431 RID: 66609
		// (set) Token: 0x06010430 RID: 66608
		[DispId(-2147412112)]
		object onmouseover
		{
			[TypeLibFunc(20)]
			[DispId(-2147412112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055AC RID: 21932
		// (get) Token: 0x06010433 RID: 66611
		// (set) Token: 0x06010432 RID: 66610
		[DispId(-2147412108)]
		object onmousemove
		{
			[TypeLibFunc(20)]
			[DispId(-2147412108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055AD RID: 21933
		// (get) Token: 0x06010435 RID: 66613
		// (set) Token: 0x06010434 RID: 66612
		[DispId(-2147412110)]
		object onmousedown
		{
			[DispId(-2147412110)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412110)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055AE RID: 21934
		// (get) Token: 0x06010437 RID: 66615
		// (set) Token: 0x06010436 RID: 66614
		[DispId(-2147412109)]
		object onmouseup
		{
			[DispId(-2147412109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055AF RID: 21935
		// (get) Token: 0x06010438 RID: 66616
		[DispId(-2147417094)]
		object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170055B0 RID: 21936
		// (get) Token: 0x0601043A RID: 66618
		// (set) Token: 0x06010439 RID: 66617
		[DispId(-2147418043)]
		string title
		{
			[TypeLibFunc(20)]
			[DispId(-2147418043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170055B1 RID: 21937
		// (get) Token: 0x0601043C RID: 66620
		// (set) Token: 0x0601043B RID: 66619
		[DispId(-2147413012)]
		string language
		{
			[TypeLibFunc(20)]
			[DispId(-2147413012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170055B2 RID: 21938
		// (get) Token: 0x0601043E RID: 66622
		// (set) Token: 0x0601043D RID: 66621
		[DispId(-2147412075)]
		object onselectstart
		{
			[DispId(-2147412075)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412075)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0601043F RID: 66623
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06010440 RID: 66624
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x170055B3 RID: 21939
		// (get) Token: 0x06010441 RID: 66625
		[DispId(-2147417088)]
		int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170055B4 RID: 21940
		// (get) Token: 0x06010442 RID: 66626
		[DispId(-2147417087)]
		object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170055B5 RID: 21941
		// (get) Token: 0x06010444 RID: 66628
		// (set) Token: 0x06010443 RID: 66627
		[DispId(-2147413103)]
		string lang
		{
			[DispId(-2147413103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170055B6 RID: 21942
		// (get) Token: 0x06010445 RID: 66629
		[DispId(-2147417104)]
		int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170055B7 RID: 21943
		// (get) Token: 0x06010446 RID: 66630
		[DispId(-2147417103)]
		int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170055B8 RID: 21944
		// (get) Token: 0x06010447 RID: 66631
		[DispId(-2147417102)]
		int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170055B9 RID: 21945
		// (get) Token: 0x06010448 RID: 66632
		[DispId(-2147417101)]
		int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170055BA RID: 21946
		// (get) Token: 0x06010449 RID: 66633
		[DispId(-2147417100)]
		IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170055BB RID: 21947
		// (get) Token: 0x0601044B RID: 66635
		// (set) Token: 0x0601044A RID: 66634
		[DispId(-2147417086)]
		string innerHTML
		{
			[DispId(-2147417086)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417086)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170055BC RID: 21948
		// (get) Token: 0x0601044D RID: 66637
		// (set) Token: 0x0601044C RID: 66636
		[DispId(-2147417085)]
		string innerText
		{
			[DispId(-2147417085)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417085)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170055BD RID: 21949
		// (get) Token: 0x0601044F RID: 66639
		// (set) Token: 0x0601044E RID: 66638
		[DispId(-2147417084)]
		string outerHTML
		{
			[DispId(-2147417084)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417084)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170055BE RID: 21950
		// (get) Token: 0x06010451 RID: 66641
		// (set) Token: 0x06010450 RID: 66640
		[DispId(-2147417083)]
		string outerText
		{
			[DispId(-2147417083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06010452 RID: 66642
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06010453 RID: 66643
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x170055BF RID: 21951
		// (get) Token: 0x06010454 RID: 66644
		[DispId(-2147417080)]
		IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170055C0 RID: 21952
		// (get) Token: 0x06010455 RID: 66645
		[DispId(-2147417078)]
		bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06010456 RID: 66646
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void click();

		// Token: 0x170055C1 RID: 21953
		// (get) Token: 0x06010457 RID: 66647
		[DispId(-2147417077)]
		IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170055C2 RID: 21954
		// (get) Token: 0x06010459 RID: 66649
		// (set) Token: 0x06010458 RID: 66648
		[DispId(-2147412077)]
		object ondragstart
		{
			[DispId(-2147412077)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0601045A RID: 66650
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();

		// Token: 0x170055C3 RID: 21955
		// (get) Token: 0x0601045C RID: 66652
		// (set) Token: 0x0601045B RID: 66651
		[DispId(-2147412091)]
		object onbeforeupdate
		{
			[DispId(-2147412091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055C4 RID: 21956
		// (get) Token: 0x0601045E RID: 66654
		// (set) Token: 0x0601045D RID: 66653
		[DispId(-2147412090)]
		object onafterupdate
		{
			[DispId(-2147412090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055C5 RID: 21957
		// (get) Token: 0x06010460 RID: 66656
		// (set) Token: 0x0601045F RID: 66655
		[DispId(-2147412074)]
		object onerrorupdate
		{
			[DispId(-2147412074)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412074)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055C6 RID: 21958
		// (get) Token: 0x06010462 RID: 66658
		// (set) Token: 0x06010461 RID: 66657
		[DispId(-2147412094)]
		object onrowexit
		{
			[TypeLibFunc(20)]
			[DispId(-2147412094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055C7 RID: 21959
		// (get) Token: 0x06010464 RID: 66660
		// (set) Token: 0x06010463 RID: 66659
		[DispId(-2147412093)]
		object onrowenter
		{
			[DispId(-2147412093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055C8 RID: 21960
		// (get) Token: 0x06010466 RID: 66662
		// (set) Token: 0x06010465 RID: 66661
		[DispId(-2147412072)]
		object ondatasetchanged
		{
			[TypeLibFunc(20)]
			[DispId(-2147412072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412072)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055C9 RID: 21961
		// (get) Token: 0x06010468 RID: 66664
		// (set) Token: 0x06010467 RID: 66663
		[DispId(-2147412071)]
		object ondataavailable
		{
			[TypeLibFunc(20)]
			[DispId(-2147412071)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412071)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055CA RID: 21962
		// (get) Token: 0x0601046A RID: 66666
		// (set) Token: 0x06010469 RID: 66665
		[DispId(-2147412070)]
		object ondatasetcomplete
		{
			[TypeLibFunc(20)]
			[DispId(-2147412070)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412070)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055CB RID: 21963
		// (get) Token: 0x0601046C RID: 66668
		// (set) Token: 0x0601046B RID: 66667
		[DispId(-2147412069)]
		object onfilterchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412069)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412069)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055CC RID: 21964
		// (get) Token: 0x0601046D RID: 66669
		[DispId(-2147417075)]
		object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170055CD RID: 21965
		// (get) Token: 0x0601046E RID: 66670
		[DispId(-2147417074)]
		object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170055CE RID: 21966
		// (get) Token: 0x0601046F RID: 66671
		[DispId(-2147417073)]
		string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06010470 RID: 66672
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setCapture([In] bool containerCapture = true);

		// Token: 0x06010471 RID: 66673
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void releaseCapture();

		// Token: 0x170055CF RID: 21967
		// (get) Token: 0x06010473 RID: 66675
		// (set) Token: 0x06010472 RID: 66674
		[DispId(-2147412066)]
		object onlosecapture
		{
			[DispId(-2147412066)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06010474 RID: 66676
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06010475 RID: 66677
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x170055D0 RID: 21968
		// (get) Token: 0x06010477 RID: 66679
		// (set) Token: 0x06010476 RID: 66678
		[DispId(-2147412081)]
		object onscroll
		{
			[TypeLibFunc(20)]
			[DispId(-2147412081)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412081)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055D1 RID: 21969
		// (get) Token: 0x06010479 RID: 66681
		// (set) Token: 0x06010478 RID: 66680
		[DispId(-2147412063)]
		object ondrag
		{
			[DispId(-2147412063)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412063)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055D2 RID: 21970
		// (get) Token: 0x0601047B RID: 66683
		// (set) Token: 0x0601047A RID: 66682
		[DispId(-2147412062)]
		object ondragend
		{
			[DispId(-2147412062)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055D3 RID: 21971
		// (get) Token: 0x0601047D RID: 66685
		// (set) Token: 0x0601047C RID: 66684
		[DispId(-2147412061)]
		object ondragenter
		{
			[DispId(-2147412061)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412061)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055D4 RID: 21972
		// (get) Token: 0x0601047F RID: 66687
		// (set) Token: 0x0601047E RID: 66686
		[DispId(-2147412060)]
		object ondragover
		{
			[DispId(-2147412060)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412060)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055D5 RID: 21973
		// (get) Token: 0x06010481 RID: 66689
		// (set) Token: 0x06010480 RID: 66688
		[DispId(-2147412059)]
		object ondragleave
		{
			[TypeLibFunc(20)]
			[DispId(-2147412059)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412059)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055D6 RID: 21974
		// (get) Token: 0x06010483 RID: 66691
		// (set) Token: 0x06010482 RID: 66690
		[DispId(-2147412058)]
		object ondrop
		{
			[DispId(-2147412058)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055D7 RID: 21975
		// (get) Token: 0x06010485 RID: 66693
		// (set) Token: 0x06010484 RID: 66692
		[DispId(-2147412054)]
		object onbeforecut
		{
			[DispId(-2147412054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055D8 RID: 21976
		// (get) Token: 0x06010487 RID: 66695
		// (set) Token: 0x06010486 RID: 66694
		[DispId(-2147412057)]
		object oncut
		{
			[DispId(-2147412057)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055D9 RID: 21977
		// (get) Token: 0x06010489 RID: 66697
		// (set) Token: 0x06010488 RID: 66696
		[DispId(-2147412053)]
		object onbeforecopy
		{
			[DispId(-2147412053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055DA RID: 21978
		// (get) Token: 0x0601048B RID: 66699
		// (set) Token: 0x0601048A RID: 66698
		[DispId(-2147412056)]
		object oncopy
		{
			[TypeLibFunc(20)]
			[DispId(-2147412056)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412056)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055DB RID: 21979
		// (get) Token: 0x0601048D RID: 66701
		// (set) Token: 0x0601048C RID: 66700
		[DispId(-2147412052)]
		object onbeforepaste
		{
			[TypeLibFunc(20)]
			[DispId(-2147412052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055DC RID: 21980
		// (get) Token: 0x0601048F RID: 66703
		// (set) Token: 0x0601048E RID: 66702
		[DispId(-2147412055)]
		object onpaste
		{
			[DispId(-2147412055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055DD RID: 21981
		// (get) Token: 0x06010490 RID: 66704
		[DispId(-2147417105)]
		IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170055DE RID: 21982
		// (get) Token: 0x06010492 RID: 66706
		// (set) Token: 0x06010491 RID: 66705
		[DispId(-2147412065)]
		object onpropertychange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412065)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06010493 RID: 66707
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLRectCollection getClientRects();

		// Token: 0x06010494 RID: 66708
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLRect getBoundingClientRect();

		// Token: 0x06010495 RID: 66709
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06010496 RID: 66710
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06010497 RID: 66711
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x170055DF RID: 21983
		// (get) Token: 0x06010499 RID: 66713
		// (set) Token: 0x06010498 RID: 66712
		[DispId(-2147418097)]
		short tabIndex
		{
			[DispId(-2147418097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x0601049A RID: 66714
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void focus();

		// Token: 0x170055E0 RID: 21984
		// (get) Token: 0x0601049C RID: 66716
		// (set) Token: 0x0601049B RID: 66715
		[DispId(-2147416107)]
		string accessKey
		{
			[TypeLibFunc(20)]
			[DispId(-2147416107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147416107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170055E1 RID: 21985
		// (get) Token: 0x0601049E RID: 66718
		// (set) Token: 0x0601049D RID: 66717
		[DispId(-2147412097)]
		object onblur
		{
			[TypeLibFunc(20)]
			[DispId(-2147412097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055E2 RID: 21986
		// (get) Token: 0x060104A0 RID: 66720
		// (set) Token: 0x0601049F RID: 66719
		[DispId(-2147412098)]
		object onfocus
		{
			[DispId(-2147412098)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055E3 RID: 21987
		// (get) Token: 0x060104A2 RID: 66722
		// (set) Token: 0x060104A1 RID: 66721
		[DispId(-2147412076)]
		object onresize
		{
			[TypeLibFunc(20)]
			[DispId(-2147412076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060104A3 RID: 66723
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void blur();

		// Token: 0x060104A4 RID: 66724
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x060104A5 RID: 66725
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170055E4 RID: 21988
		// (get) Token: 0x060104A6 RID: 66726
		[DispId(-2147416093)]
		int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170055E5 RID: 21989
		// (get) Token: 0x060104A7 RID: 66727
		[DispId(-2147416092)]
		int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170055E6 RID: 21990
		// (get) Token: 0x060104A8 RID: 66728
		[DispId(-2147416091)]
		int clientTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147416091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170055E7 RID: 21991
		// (get) Token: 0x060104A9 RID: 66729
		[DispId(-2147416090)]
		int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060104AA RID: 66730
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x060104AB RID: 66731
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x170055E8 RID: 21992
		// (get) Token: 0x060104AC RID: 66732
		[DispId(-2147412996)]
		object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170055E9 RID: 21993
		// (get) Token: 0x060104AE RID: 66734
		// (set) Token: 0x060104AD RID: 66733
		[DispId(-2147412087)]
		object onreadystatechange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412087)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055EA RID: 21994
		// (get) Token: 0x060104B0 RID: 66736
		// (set) Token: 0x060104AF RID: 66735
		[DispId(-2147412050)]
		object onrowsdelete
		{
			[DispId(-2147412050)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412050)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055EB RID: 21995
		// (get) Token: 0x060104B2 RID: 66738
		// (set) Token: 0x060104B1 RID: 66737
		[DispId(-2147412049)]
		object onrowsinserted
		{
			[TypeLibFunc(20)]
			[DispId(-2147412049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055EC RID: 21996
		// (get) Token: 0x060104B4 RID: 66740
		// (set) Token: 0x060104B3 RID: 66739
		[DispId(-2147412048)]
		object oncellchange
		{
			[DispId(-2147412048)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055ED RID: 21997
		// (get) Token: 0x060104B6 RID: 66742
		// (set) Token: 0x060104B5 RID: 66741
		[DispId(-2147412995)]
		string dir
		{
			[TypeLibFunc(20)]
			[DispId(-2147412995)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412995)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x060104B7 RID: 66743
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object createControlRange();

		// Token: 0x170055EE RID: 21998
		// (get) Token: 0x060104B8 RID: 66744
		[DispId(-2147417055)]
		int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170055EF RID: 21999
		// (get) Token: 0x060104B9 RID: 66745
		[DispId(-2147417054)]
		int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170055F0 RID: 22000
		// (get) Token: 0x060104BB RID: 66747
		// (set) Token: 0x060104BA RID: 66746
		[DispId(-2147417053)]
		int scrollTop
		{
			[DispId(-2147417053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147417053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170055F1 RID: 22001
		// (get) Token: 0x060104BD RID: 66749
		// (set) Token: 0x060104BC RID: 66748
		[DispId(-2147417052)]
		int scrollLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147417052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147417052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x060104BE RID: 66750
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void clearAttributes();

		// Token: 0x170055F2 RID: 22002
		// (get) Token: 0x060104C0 RID: 66752
		// (set) Token: 0x060104BF RID: 66751
		[DispId(-2147412047)]
		object oncontextmenu
		{
			[DispId(-2147412047)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412047)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060104C1 RID: 66753
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x060104C2 RID: 66754
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x060104C3 RID: 66755
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x060104C4 RID: 66756
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x170055F3 RID: 22003
		// (get) Token: 0x060104C5 RID: 66757
		[DispId(-2147417040)]
		bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060104C6 RID: 66758
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x060104C7 RID: 66759
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeBehavior([In] int cookie);

		// Token: 0x170055F4 RID: 22004
		// (get) Token: 0x060104C8 RID: 66760
		[DispId(-2147417048)]
		IHTMLStyle runtimeStyle
		{
			[DispId(-2147417048)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170055F5 RID: 22005
		// (get) Token: 0x060104C9 RID: 66761
		[DispId(-2147417030)]
		object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170055F6 RID: 22006
		// (get) Token: 0x060104CB RID: 66763
		// (set) Token: 0x060104CA RID: 66762
		[DispId(-2147417029)]
		string tagUrn
		{
			[DispId(-2147417029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170055F7 RID: 22007
		// (get) Token: 0x060104CD RID: 66765
		// (set) Token: 0x060104CC RID: 66764
		[DispId(-2147412043)]
		object onbeforeeditfocus
		{
			[DispId(-2147412043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055F8 RID: 22008
		// (get) Token: 0x060104CE RID: 66766
		[DispId(-2147417028)]
		int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060104CF RID: 66767
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x060104D0 RID: 66768
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x170055F9 RID: 22009
		// (get) Token: 0x060104D1 RID: 66769
		[DispId(-2147417015)]
		bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170055FA RID: 22010
		// (get) Token: 0x060104D2 RID: 66770
		[DispId(-2147417014)]
		bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170055FB RID: 22011
		// (get) Token: 0x060104D4 RID: 66772
		// (set) Token: 0x060104D3 RID: 66771
		[DispId(-2147412039)]
		object onlayoutcomplete
		{
			[DispId(-2147412039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055FC RID: 22012
		// (get) Token: 0x060104D6 RID: 66774
		// (set) Token: 0x060104D5 RID: 66773
		[DispId(-2147412038)]
		object onpage
		{
			[TypeLibFunc(20)]
			[DispId(-2147412038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412038)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170055FD RID: 22013
		// (get) Token: 0x060104D8 RID: 66776
		// (set) Token: 0x060104D7 RID: 66775
		[DispId(-2147417012)]
		bool inflateBlock
		{
			[DispId(-2147417012)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147417012)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170055FE RID: 22014
		// (get) Token: 0x060104DA RID: 66778
		// (set) Token: 0x060104D9 RID: 66777
		[DispId(-2147412035)]
		object onbeforedeactivate
		{
			[DispId(-2147412035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060104DB RID: 66779
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setActive();

		// Token: 0x170055FF RID: 22015
		// (get) Token: 0x060104DD RID: 66781
		// (set) Token: 0x060104DC RID: 66780
		[DispId(-2147412950)]
		string contentEditable
		{
			[TypeLibFunc(20)]
			[DispId(-2147412950)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412950)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17005600 RID: 22016
		// (get) Token: 0x060104DE RID: 66782
		[DispId(-2147417010)]
		bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005601 RID: 22017
		// (get) Token: 0x060104E0 RID: 66784
		// (set) Token: 0x060104DF RID: 66783
		[DispId(-2147412949)]
		bool hideFocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412949)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147412949)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17005602 RID: 22018
		// (get) Token: 0x060104E2 RID: 66786
		// (set) Token: 0x060104E1 RID: 66785
		[DispId(-2147418036)]
		bool disabled
		{
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17005603 RID: 22019
		// (get) Token: 0x060104E3 RID: 66787
		[DispId(-2147417007)]
		bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005604 RID: 22020
		// (get) Token: 0x060104E5 RID: 66789
		// (set) Token: 0x060104E4 RID: 66788
		[DispId(-2147412034)]
		object onmove
		{
			[DispId(-2147412034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005605 RID: 22021
		// (get) Token: 0x060104E7 RID: 66791
		// (set) Token: 0x060104E6 RID: 66790
		[DispId(-2147412033)]
		object oncontrolselect
		{
			[DispId(-2147412033)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060104E8 RID: 66792
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17005606 RID: 22022
		// (get) Token: 0x060104EA RID: 66794
		// (set) Token: 0x060104E9 RID: 66793
		[DispId(-2147412029)]
		object onresizestart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412029)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005607 RID: 22023
		// (get) Token: 0x060104EC RID: 66796
		// (set) Token: 0x060104EB RID: 66795
		[DispId(-2147412028)]
		object onresizeend
		{
			[DispId(-2147412028)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412028)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005608 RID: 22024
		// (get) Token: 0x060104EE RID: 66798
		// (set) Token: 0x060104ED RID: 66797
		[DispId(-2147412031)]
		object onmovestart
		{
			[DispId(-2147412031)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412031)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005609 RID: 22025
		// (get) Token: 0x060104F0 RID: 66800
		// (set) Token: 0x060104EF RID: 66799
		[DispId(-2147412030)]
		object onmoveend
		{
			[DispId(-2147412030)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700560A RID: 22026
		// (get) Token: 0x060104F2 RID: 66802
		// (set) Token: 0x060104F1 RID: 66801
		[DispId(-2147412027)]
		object onmouseenter
		{
			[TypeLibFunc(20)]
			[DispId(-2147412027)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412027)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700560B RID: 22027
		// (get) Token: 0x060104F4 RID: 66804
		// (set) Token: 0x060104F3 RID: 66803
		[DispId(-2147412026)]
		object onmouseleave
		{
			[TypeLibFunc(20)]
			[DispId(-2147412026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412026)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700560C RID: 22028
		// (get) Token: 0x060104F6 RID: 66806
		// (set) Token: 0x060104F5 RID: 66805
		[DispId(-2147412025)]
		object onactivate
		{
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700560D RID: 22029
		// (get) Token: 0x060104F8 RID: 66808
		// (set) Token: 0x060104F7 RID: 66807
		[DispId(-2147412024)]
		object ondeactivate
		{
			[DispId(-2147412024)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060104F9 RID: 66809
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool dragDrop();

		// Token: 0x1700560E RID: 22030
		// (get) Token: 0x060104FA RID: 66810
		[DispId(-2147417004)]
		int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700560F RID: 22031
		// (get) Token: 0x060104FC RID: 66812
		// (set) Token: 0x060104FB RID: 66811
		[DispId(-2147412036)]
		object onmousewheel
		{
			[DispId(-2147412036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060104FD RID: 66813
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void normalize();

		// Token: 0x060104FE RID: 66814
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x060104FF RID: 66815
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06010500 RID: 66816
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17005610 RID: 22032
		// (get) Token: 0x06010502 RID: 66818
		// (set) Token: 0x06010501 RID: 66817
		[DispId(-2147412022)]
		object onbeforeactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005611 RID: 22033
		// (get) Token: 0x06010504 RID: 66820
		// (set) Token: 0x06010503 RID: 66819
		[DispId(-2147412021)]
		object onfocusin
		{
			[DispId(-2147412021)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005612 RID: 22034
		// (get) Token: 0x06010506 RID: 66822
		// (set) Token: 0x06010505 RID: 66821
		[DispId(-2147412020)]
		object onfocusout
		{
			[TypeLibFunc(20)]
			[DispId(-2147412020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005613 RID: 22035
		// (get) Token: 0x06010507 RID: 66823
		[DispId(-2147417058)]
		int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005614 RID: 22036
		// (get) Token: 0x06010508 RID: 66824
		[DispId(-2147417057)]
		string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005615 RID: 22037
		// (get) Token: 0x06010509 RID: 66825
		[DispId(-2147417066)]
		int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005616 RID: 22038
		// (get) Token: 0x0601050A RID: 66826
		[DispId(-2147417065)]
		IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0601050B RID: 66827
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool hasChildNodes();

		// Token: 0x17005617 RID: 22039
		// (get) Token: 0x0601050C RID: 66828
		[DispId(-2147417063)]
		object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005618 RID: 22040
		// (get) Token: 0x0601050D RID: 66829
		[DispId(-2147417062)]
		object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0601050E RID: 66830
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0601050F RID: 66831
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06010510 RID: 66832
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06010511 RID: 66833
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x06010512 RID: 66834
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06010513 RID: 66835
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06010514 RID: 66836
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06010515 RID: 66837
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17005619 RID: 22041
		// (get) Token: 0x06010516 RID: 66838
		[DispId(-2147417038)]
		string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700561A RID: 22042
		// (get) Token: 0x06010518 RID: 66840
		// (set) Token: 0x06010517 RID: 66839
		[DispId(-2147417037)]
		object nodeValue
		{
			[DispId(-2147417037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147417037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700561B RID: 22043
		// (get) Token: 0x06010519 RID: 66841
		[DispId(-2147417036)]
		IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700561C RID: 22044
		// (get) Token: 0x0601051A RID: 66842
		[DispId(-2147417035)]
		IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700561D RID: 22045
		// (get) Token: 0x0601051B RID: 66843
		[DispId(-2147417034)]
		IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700561E RID: 22046
		// (get) Token: 0x0601051C RID: 66844
		[DispId(-2147417033)]
		IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700561F RID: 22047
		// (get) Token: 0x0601051D RID: 66845
		[DispId(-2147416999)]
		object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005620 RID: 22048
		// (get) Token: 0x0601051F RID: 66847
		// (set) Token: 0x0601051E RID: 66846
		[DispId(-2147417091)]
		string dataFld
		{
			[DispId(-2147417091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17005621 RID: 22049
		// (get) Token: 0x06010521 RID: 66849
		// (set) Token: 0x06010520 RID: 66848
		[DispId(-2147417090)]
		string dataSrc
		{
			[DispId(-2147417090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17005622 RID: 22050
		// (get) Token: 0x06010523 RID: 66851
		// (set) Token: 0x06010522 RID: 66850
		[DispId(-2147417089)]
		string dataFormatAs
		{
			[DispId(-2147417089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17005623 RID: 22051
		// (get) Token: 0x06010525 RID: 66853
		// (set) Token: 0x06010524 RID: 66852
		[DispId(1001)]
		int cols
		{
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17005624 RID: 22052
		// (get) Token: 0x06010527 RID: 66855
		// (set) Token: 0x06010526 RID: 66854
		[DispId(1002)]
		object border
		{
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005625 RID: 22053
		// (get) Token: 0x06010529 RID: 66857
		// (set) Token: 0x06010528 RID: 66856
		[DispId(1004)]
		string frame
		{
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17005626 RID: 22054
		// (get) Token: 0x0601052B RID: 66859
		// (set) Token: 0x0601052A RID: 66858
		[DispId(1003)]
		string rules
		{
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17005627 RID: 22055
		// (get) Token: 0x0601052D RID: 66861
		// (set) Token: 0x0601052C RID: 66860
		[DispId(1005)]
		object cellSpacing
		{
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005628 RID: 22056
		// (get) Token: 0x0601052F RID: 66863
		// (set) Token: 0x0601052E RID: 66862
		[DispId(1006)]
		object cellPadding
		{
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005629 RID: 22057
		// (get) Token: 0x06010531 RID: 66865
		// (set) Token: 0x06010530 RID: 66864
		[DispId(-2147413111)]
		string background
		{
			[DispId(-2147413111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700562A RID: 22058
		// (get) Token: 0x06010533 RID: 66867
		// (set) Token: 0x06010532 RID: 66866
		[DispId(-501)]
		object bgColor
		{
			[DispId(-501)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-501)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700562B RID: 22059
		// (get) Token: 0x06010535 RID: 66869
		// (set) Token: 0x06010534 RID: 66868
		[DispId(-2147413084)]
		object borderColor
		{
			[DispId(-2147413084)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413084)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700562C RID: 22060
		// (get) Token: 0x06010537 RID: 66871
		// (set) Token: 0x06010536 RID: 66870
		[DispId(-2147413083)]
		object borderColorLight
		{
			[DispId(-2147413083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700562D RID: 22061
		// (get) Token: 0x06010539 RID: 66873
		// (set) Token: 0x06010538 RID: 66872
		[DispId(-2147413082)]
		object borderColorDark
		{
			[DispId(-2147413082)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413082)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700562E RID: 22062
		// (get) Token: 0x0601053B RID: 66875
		// (set) Token: 0x0601053A RID: 66874
		[DispId(-2147418039)]
		string align
		{
			[DispId(-2147418039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0601053C RID: 66876
		[DispId(1015)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void refresh();

		// Token: 0x1700562F RID: 22063
		// (get) Token: 0x0601053D RID: 66877
		[DispId(1016)]
		IHTMLElementCollection rows
		{
			[DispId(1016)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005630 RID: 22064
		// (get) Token: 0x0601053F RID: 66879
		// (set) Token: 0x0601053E RID: 66878
		[DispId(-2147418107)]
		object width
		{
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005631 RID: 22065
		// (get) Token: 0x06010541 RID: 66881
		// (set) Token: 0x06010540 RID: 66880
		[DispId(-2147418106)]
		object height
		{
			[DispId(-2147418106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005632 RID: 22066
		// (get) Token: 0x06010543 RID: 66883
		// (set) Token: 0x06010542 RID: 66882
		[DispId(1017)]
		int dataPageSize
		{
			[DispId(1017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06010544 RID: 66884
		[DispId(1018)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void nextPage();

		// Token: 0x06010545 RID: 66885
		[DispId(1019)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void previousPage();

		// Token: 0x17005633 RID: 22067
		// (get) Token: 0x06010546 RID: 66886
		[DispId(1020)]
		IHTMLTableSection tHead
		{
			[DispId(1020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005634 RID: 22068
		// (get) Token: 0x06010547 RID: 66887
		[DispId(1021)]
		IHTMLTableSection tFoot
		{
			[DispId(1021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005635 RID: 22069
		// (get) Token: 0x06010548 RID: 66888
		[DispId(1024)]
		IHTMLElementCollection tBodies
		{
			[DispId(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005636 RID: 22070
		// (get) Token: 0x06010549 RID: 66889
		[DispId(1025)]
		IHTMLTableCaption caption
		{
			[DispId(1025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0601054A RID: 66890
		[DispId(1026)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object createTHead();

		// Token: 0x0601054B RID: 66891
		[DispId(1027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void deleteTHead();

		// Token: 0x0601054C RID: 66892
		[DispId(1028)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object createTFoot();

		// Token: 0x0601054D RID: 66893
		[DispId(1029)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void deleteTFoot();

		// Token: 0x0601054E RID: 66894
		[DispId(1030)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLTableCaption createCaption();

		// Token: 0x0601054F RID: 66895
		[DispId(1031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void deleteCaption();

		// Token: 0x06010550 RID: 66896
		[DispId(1032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object insertRow([In] int index = -1);

		// Token: 0x06010551 RID: 66897
		[DispId(1033)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void deleteRow([In] int index = -1);

		// Token: 0x06010552 RID: 66898
		[DispId(1035)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void firstPage();

		// Token: 0x06010553 RID: 66899
		[DispId(1036)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void lastPage();

		// Token: 0x17005637 RID: 22071
		// (get) Token: 0x06010554 RID: 66900
		[DispId(1037)]
		IHTMLElementCollection cells
		{
			[DispId(1037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06010555 RID: 66901
		[DispId(1038)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object moveRow([In] int indexFrom = -1, [In] int indexTo = -1);

		// Token: 0x17005638 RID: 22072
		// (get) Token: 0x06010557 RID: 66903
		// (set) Token: 0x06010556 RID: 66902
		[DispId(1039)]
		string summary
		{
			[DispId(1039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}
	}
}
