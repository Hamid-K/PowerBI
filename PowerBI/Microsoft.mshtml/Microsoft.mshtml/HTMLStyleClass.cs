using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000051 RID: 81
	[TypeLibType(2)]
	[ClassInterface(0)]
	[Guid("3050F285-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLStyleClass : DispHTMLStyle, HTMLStyle, IHTMLStyle, IHTMLStyle2, IHTMLStyle3, IHTMLStyle4
	{
		// Token: 0x060003AD RID: 941
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLStyleClass();

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060003AF RID: 943
		// (set) Token: 0x060003AE RID: 942
		[DispId(-2147413094)]
		public virtual extern string fontFamily
		{
			[TypeLibFunc(20)]
			[DispId(-2147413094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060003B1 RID: 945
		// (set) Token: 0x060003B0 RID: 944
		[DispId(-2147413088)]
		public virtual extern string fontStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060003B3 RID: 947
		// (set) Token: 0x060003B2 RID: 946
		[DispId(-2147413087)]
		public virtual extern string fontVariant
		{
			[DispId(-2147413087)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413087)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060003B5 RID: 949
		// (set) Token: 0x060003B4 RID: 948
		[DispId(-2147413085)]
		public virtual extern string fontWeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413085)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413085)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060003B7 RID: 951
		// (set) Token: 0x060003B6 RID: 950
		[DispId(-2147413093)]
		public virtual extern object fontSize
		{
			[DispId(-2147413093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060003B9 RID: 953
		// (set) Token: 0x060003B8 RID: 952
		[DispId(-2147413071)]
		public virtual extern string font
		{
			[DispId(-2147413071)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413071)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060003BB RID: 955
		// (set) Token: 0x060003BA RID: 954
		[DispId(-2147413110)]
		public virtual extern object color
		{
			[TypeLibFunc(20)]
			[DispId(-2147413110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413110)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060003BD RID: 957
		// (set) Token: 0x060003BC RID: 956
		[DispId(-2147413080)]
		public virtual extern string background
		{
			[DispId(-2147413080)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413080)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060003BF RID: 959
		// (set) Token: 0x060003BE RID: 958
		[DispId(-501)]
		public virtual extern object backgroundColor
		{
			[TypeLibFunc(20)]
			[DispId(-501)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-501)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060003C1 RID: 961
		// (set) Token: 0x060003C0 RID: 960
		[DispId(-2147413111)]
		public virtual extern string backgroundImage
		{
			[DispId(-2147413111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060003C3 RID: 963
		// (set) Token: 0x060003C2 RID: 962
		[DispId(-2147413068)]
		public virtual extern string backgroundRepeat
		{
			[DispId(-2147413068)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413068)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060003C5 RID: 965
		// (set) Token: 0x060003C4 RID: 964
		[DispId(-2147413067)]
		public virtual extern string backgroundAttachment
		{
			[TypeLibFunc(20)]
			[DispId(-2147413067)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413067)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060003C7 RID: 967
		// (set) Token: 0x060003C6 RID: 966
		[DispId(-2147413066)]
		public virtual extern string backgroundPosition
		{
			[DispId(-2147413066)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413066)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060003C9 RID: 969
		// (set) Token: 0x060003C8 RID: 968
		[DispId(-2147413079)]
		public virtual extern object backgroundPositionX
		{
			[TypeLibFunc(20)]
			[DispId(-2147413079)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413079)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060003CB RID: 971
		// (set) Token: 0x060003CA RID: 970
		[DispId(-2147413078)]
		public virtual extern object backgroundPositionY
		{
			[DispId(-2147413078)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060003CD RID: 973
		// (set) Token: 0x060003CC RID: 972
		[DispId(-2147413065)]
		public virtual extern object wordSpacing
		{
			[DispId(-2147413065)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413065)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060003CF RID: 975
		// (set) Token: 0x060003CE RID: 974
		[DispId(-2147413104)]
		public virtual extern object letterSpacing
		{
			[DispId(-2147413104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060003D1 RID: 977
		// (set) Token: 0x060003D0 RID: 976
		[DispId(-2147413077)]
		public virtual extern string textDecoration
		{
			[TypeLibFunc(20)]
			[DispId(-2147413077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413077)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060003D3 RID: 979
		// (set) Token: 0x060003D2 RID: 978
		[DispId(-2147413089)]
		public virtual extern bool textDecorationNone
		{
			[DispId(-2147413089)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147413089)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060003D5 RID: 981
		// (set) Token: 0x060003D4 RID: 980
		[DispId(-2147413091)]
		public virtual extern bool textDecorationUnderline
		{
			[DispId(-2147413091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060003D7 RID: 983
		// (set) Token: 0x060003D6 RID: 982
		[DispId(-2147413043)]
		public virtual extern bool textDecorationOverline
		{
			[DispId(-2147413043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147413043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060003D9 RID: 985
		// (set) Token: 0x060003D8 RID: 984
		[DispId(-2147413092)]
		public virtual extern bool textDecorationLineThrough
		{
			[TypeLibFunc(20)]
			[DispId(-2147413092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060003DB RID: 987
		// (set) Token: 0x060003DA RID: 986
		[DispId(-2147413090)]
		public virtual extern bool textDecorationBlink
		{
			[TypeLibFunc(20)]
			[DispId(-2147413090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060003DD RID: 989
		// (set) Token: 0x060003DC RID: 988
		[DispId(-2147413064)]
		public virtual extern object verticalAlign
		{
			[TypeLibFunc(20)]
			[DispId(-2147413064)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413064)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060003DF RID: 991
		// (set) Token: 0x060003DE RID: 990
		[DispId(-2147413108)]
		public virtual extern string textTransform
		{
			[TypeLibFunc(20)]
			[DispId(-2147413108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060003E1 RID: 993
		// (set) Token: 0x060003E0 RID: 992
		[DispId(-2147418040)]
		public virtual extern string textAlign
		{
			[DispId(-2147418040)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418040)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060003E3 RID: 995
		// (set) Token: 0x060003E2 RID: 994
		[DispId(-2147413105)]
		public virtual extern object textIndent
		{
			[TypeLibFunc(20)]
			[DispId(-2147413105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060003E5 RID: 997
		// (set) Token: 0x060003E4 RID: 996
		[DispId(-2147413106)]
		public virtual extern object lineHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060003E7 RID: 999
		// (set) Token: 0x060003E6 RID: 998
		[DispId(-2147413075)]
		public virtual extern object marginTop
		{
			[DispId(-2147413075)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413075)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060003E9 RID: 1001
		// (set) Token: 0x060003E8 RID: 1000
		[DispId(-2147413074)]
		public virtual extern object marginRight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413074)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060003EB RID: 1003
		// (set) Token: 0x060003EA RID: 1002
		[DispId(-2147413073)]
		public virtual extern object marginBottom
		{
			[DispId(-2147413073)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413073)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060003ED RID: 1005
		// (set) Token: 0x060003EC RID: 1004
		[DispId(-2147413072)]
		public virtual extern object marginLeft
		{
			[DispId(-2147413072)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060003EF RID: 1007
		// (set) Token: 0x060003EE RID: 1006
		[DispId(-2147413076)]
		public virtual extern string margin
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413076)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060003F1 RID: 1009
		// (set) Token: 0x060003F0 RID: 1008
		[DispId(-2147413100)]
		public virtual extern object paddingTop
		{
			[DispId(-2147413100)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413100)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060003F3 RID: 1011
		// (set) Token: 0x060003F2 RID: 1010
		[DispId(-2147413099)]
		public virtual extern object paddingRight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060003F5 RID: 1013
		// (set) Token: 0x060003F4 RID: 1012
		[DispId(-2147413098)]
		public virtual extern object paddingBottom
		{
			[DispId(-2147413098)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413098)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060003F7 RID: 1015
		// (set) Token: 0x060003F6 RID: 1014
		[DispId(-2147413097)]
		public virtual extern object paddingLeft
		{
			[DispId(-2147413097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060003F9 RID: 1017
		// (set) Token: 0x060003F8 RID: 1016
		[DispId(-2147413101)]
		public virtual extern string padding
		{
			[DispId(-2147413101)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413101)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060003FB RID: 1019
		// (set) Token: 0x060003FA RID: 1018
		[DispId(-2147413063)]
		public virtual extern string border
		{
			[DispId(-2147413063)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060003FD RID: 1021
		// (set) Token: 0x060003FC RID: 1020
		[DispId(-2147413062)]
		public virtual extern string borderTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060003FF RID: 1023
		// (set) Token: 0x060003FE RID: 1022
		[DispId(-2147413061)]
		public virtual extern string borderRight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413061)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413061)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000401 RID: 1025
		// (set) Token: 0x06000400 RID: 1024
		[DispId(-2147413060)]
		public virtual extern string borderBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147413060)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413060)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000403 RID: 1027
		// (set) Token: 0x06000402 RID: 1026
		[DispId(-2147413059)]
		public virtual extern string borderLeft
		{
			[DispId(-2147413059)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413059)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000405 RID: 1029
		// (set) Token: 0x06000404 RID: 1028
		[DispId(-2147413058)]
		public virtual extern string borderColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000407 RID: 1031
		// (set) Token: 0x06000406 RID: 1030
		[DispId(-2147413057)]
		public virtual extern object borderTopColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000409 RID: 1033
		// (set) Token: 0x06000408 RID: 1032
		[DispId(-2147413056)]
		public virtual extern object borderRightColor
		{
			[DispId(-2147413056)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413056)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600040B RID: 1035
		// (set) Token: 0x0600040A RID: 1034
		[DispId(-2147413055)]
		public virtual extern object borderBottomColor
		{
			[DispId(-2147413055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600040D RID: 1037
		// (set) Token: 0x0600040C RID: 1036
		[DispId(-2147413054)]
		public virtual extern object borderLeftColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600040F RID: 1039
		// (set) Token: 0x0600040E RID: 1038
		[DispId(-2147413053)]
		public virtual extern string borderWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000411 RID: 1041
		// (set) Token: 0x06000410 RID: 1040
		[DispId(-2147413052)]
		public virtual extern object borderTopWidth
		{
			[DispId(-2147413052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000413 RID: 1043
		// (set) Token: 0x06000412 RID: 1042
		[DispId(-2147413051)]
		public virtual extern object borderRightWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413051)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413051)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000415 RID: 1045
		// (set) Token: 0x06000414 RID: 1044
		[DispId(-2147413050)]
		public virtual extern object borderBottomWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413050)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413050)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000417 RID: 1047
		// (set) Token: 0x06000416 RID: 1046
		[DispId(-2147413049)]
		public virtual extern object borderLeftWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000419 RID: 1049
		// (set) Token: 0x06000418 RID: 1048
		[DispId(-2147413048)]
		public virtual extern string borderStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600041B RID: 1051
		// (set) Token: 0x0600041A RID: 1050
		[DispId(-2147413047)]
		public virtual extern string borderTopStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600041D RID: 1053
		// (set) Token: 0x0600041C RID: 1052
		[DispId(-2147413046)]
		public virtual extern string borderRightStyle
		{
			[DispId(-2147413046)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413046)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600041F RID: 1055
		// (set) Token: 0x0600041E RID: 1054
		[DispId(-2147413045)]
		public virtual extern string borderBottomStyle
		{
			[DispId(-2147413045)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413045)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000421 RID: 1057
		// (set) Token: 0x06000420 RID: 1056
		[DispId(-2147413044)]
		public virtual extern string borderLeftStyle
		{
			[DispId(-2147413044)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413044)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000423 RID: 1059
		// (set) Token: 0x06000422 RID: 1058
		[DispId(-2147418107)]
		public virtual extern object width
		{
			[DispId(-2147418107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000425 RID: 1061
		// (set) Token: 0x06000424 RID: 1060
		[DispId(-2147418106)]
		public virtual extern object height
		{
			[DispId(-2147418106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000427 RID: 1063
		// (set) Token: 0x06000426 RID: 1062
		[DispId(-2147413042)]
		public virtual extern string styleFloat
		{
			[DispId(-2147413042)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413042)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000429 RID: 1065
		// (set) Token: 0x06000428 RID: 1064
		[DispId(-2147413096)]
		public virtual extern string clear
		{
			[TypeLibFunc(20)]
			[DispId(-2147413096)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413096)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600042B RID: 1067
		// (set) Token: 0x0600042A RID: 1066
		[DispId(-2147413041)]
		public virtual extern string display
		{
			[DispId(-2147413041)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413041)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600042D RID: 1069
		// (set) Token: 0x0600042C RID: 1068
		[DispId(-2147413032)]
		public virtual extern string visibility
		{
			[DispId(-2147413032)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413032)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600042F RID: 1071
		// (set) Token: 0x0600042E RID: 1070
		[DispId(-2147413040)]
		public virtual extern string listStyleType
		{
			[TypeLibFunc(20)]
			[DispId(-2147413040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413040)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000431 RID: 1073
		// (set) Token: 0x06000430 RID: 1072
		[DispId(-2147413039)]
		public virtual extern string listStylePosition
		{
			[TypeLibFunc(20)]
			[DispId(-2147413039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000433 RID: 1075
		// (set) Token: 0x06000432 RID: 1074
		[DispId(-2147413038)]
		public virtual extern string listStyleImage
		{
			[TypeLibFunc(20)]
			[DispId(-2147413038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413038)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000435 RID: 1077
		// (set) Token: 0x06000434 RID: 1076
		[DispId(-2147413037)]
		public virtual extern string listStyle
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413037)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000437 RID: 1079
		// (set) Token: 0x06000436 RID: 1078
		[DispId(-2147413036)]
		public virtual extern string whiteSpace
		{
			[TypeLibFunc(20)]
			[DispId(-2147413036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000439 RID: 1081
		// (set) Token: 0x06000438 RID: 1080
		[DispId(-2147418108)]
		public virtual extern object top
		{
			[TypeLibFunc(20)]
			[DispId(-2147418108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600043B RID: 1083
		// (set) Token: 0x0600043A RID: 1082
		[DispId(-2147418109)]
		public virtual extern object left
		{
			[DispId(-2147418109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600043D RID: 1085
		// (set) Token: 0x0600043C RID: 1084
		[DispId(-2147413021)]
		public virtual extern object zIndex
		{
			[TypeLibFunc(20)]
			[DispId(-2147413021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600043F RID: 1087
		// (set) Token: 0x0600043E RID: 1086
		[DispId(-2147413102)]
		public virtual extern string overflow
		{
			[TypeLibFunc(20)]
			[DispId(-2147413102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000441 RID: 1089
		// (set) Token: 0x06000440 RID: 1088
		[DispId(-2147413035)]
		public virtual extern string pageBreakBefore
		{
			[TypeLibFunc(20)]
			[DispId(-2147413035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000443 RID: 1091
		// (set) Token: 0x06000442 RID: 1090
		[DispId(-2147413034)]
		public virtual extern string pageBreakAfter
		{
			[TypeLibFunc(20)]
			[DispId(-2147413034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000445 RID: 1093
		// (set) Token: 0x06000444 RID: 1092
		[DispId(-2147413013)]
		public virtual extern string cssText
		{
			[DispId(-2147413013)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413013)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000447 RID: 1095
		// (set) Token: 0x06000446 RID: 1094
		[DispId(-2147414112)]
		public virtual extern int pixelTop
		{
			[TypeLibFunc(84)]
			[DispId(-2147414112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147414112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000449 RID: 1097
		// (set) Token: 0x06000448 RID: 1096
		[DispId(-2147414111)]
		public virtual extern int pixelLeft
		{
			[DispId(-2147414111)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147414111)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600044B RID: 1099
		// (set) Token: 0x0600044A RID: 1098
		[DispId(-2147414110)]
		public virtual extern int pixelWidth
		{
			[TypeLibFunc(84)]
			[DispId(-2147414110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147414110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600044D RID: 1101
		// (set) Token: 0x0600044C RID: 1100
		[DispId(-2147414109)]
		public virtual extern int pixelHeight
		{
			[DispId(-2147414109)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147414109)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600044F RID: 1103
		// (set) Token: 0x0600044E RID: 1102
		[DispId(-2147414108)]
		public virtual extern float posTop
		{
			[DispId(-2147414108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147414108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000451 RID: 1105
		// (set) Token: 0x06000450 RID: 1104
		[DispId(-2147414107)]
		public virtual extern float posLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147414107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147414107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000453 RID: 1107
		// (set) Token: 0x06000452 RID: 1106
		[DispId(-2147414106)]
		public virtual extern float posWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147414106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147414106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000455 RID: 1109
		// (set) Token: 0x06000454 RID: 1108
		[DispId(-2147414105)]
		public virtual extern float posHeight
		{
			[DispId(-2147414105)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147414105)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000457 RID: 1111
		// (set) Token: 0x06000456 RID: 1110
		[DispId(-2147413010)]
		public virtual extern string cursor
		{
			[DispId(-2147413010)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000459 RID: 1113
		// (set) Token: 0x06000458 RID: 1112
		[DispId(-2147413020)]
		public virtual extern string clip
		{
			[TypeLibFunc(20)]
			[DispId(-2147413020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413020)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600045B RID: 1115
		// (set) Token: 0x0600045A RID: 1114
		[DispId(-2147413030)]
		public virtual extern string filter
		{
			[DispId(-2147413030)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600045C RID: 1116
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600045D RID: 1117
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600045E RID: 1118
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x0600045F RID: 1119
		[DispId(-2147414104)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000461 RID: 1121
		// (set) Token: 0x06000460 RID: 1120
		[DispId(-2147413014)]
		public virtual extern string tableLayout
		{
			[TypeLibFunc(20)]
			[DispId(-2147413014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000463 RID: 1123
		// (set) Token: 0x06000462 RID: 1122
		[DispId(-2147413028)]
		public virtual extern string borderCollapse
		{
			[TypeLibFunc(20)]
			[DispId(-2147413028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413028)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000465 RID: 1125
		// (set) Token: 0x06000464 RID: 1124
		[DispId(-2147412993)]
		public virtual extern string direction
		{
			[DispId(-2147412993)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412993)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000467 RID: 1127
		// (set) Token: 0x06000466 RID: 1126
		[DispId(-2147412997)]
		public virtual extern string behavior
		{
			[TypeLibFunc(20)]
			[DispId(-2147412997)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412997)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06000468 RID: 1128
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06000469 RID: 1129
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600046A RID: 1130
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600046C RID: 1132
		// (set) Token: 0x0600046B RID: 1131
		[DispId(-2147413022)]
		public virtual extern string position
		{
			[TypeLibFunc(20)]
			[DispId(-2147413022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600046E RID: 1134
		// (set) Token: 0x0600046D RID: 1133
		[DispId(-2147412994)]
		public virtual extern string unicodeBidi
		{
			[TypeLibFunc(20)]
			[DispId(-2147412994)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412994)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000470 RID: 1136
		// (set) Token: 0x0600046F RID: 1135
		[DispId(-2147418034)]
		public virtual extern object bottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147418034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000472 RID: 1138
		// (set) Token: 0x06000471 RID: 1137
		[DispId(-2147418035)]
		public virtual extern object right
		{
			[TypeLibFunc(20)]
			[DispId(-2147418035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000474 RID: 1140
		// (set) Token: 0x06000473 RID: 1139
		[DispId(-2147414103)]
		public virtual extern int pixelBottom
		{
			[DispId(-2147414103)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147414103)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000476 RID: 1142
		// (set) Token: 0x06000475 RID: 1141
		[DispId(-2147414102)]
		public virtual extern int pixelRight
		{
			[DispId(-2147414102)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147414102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000478 RID: 1144
		// (set) Token: 0x06000477 RID: 1143
		[DispId(-2147414101)]
		public virtual extern float posBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147414101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147414101)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600047A RID: 1146
		// (set) Token: 0x06000479 RID: 1145
		[DispId(-2147414100)]
		public virtual extern float posRight
		{
			[DispId(-2147414100)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147414100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600047C RID: 1148
		// (set) Token: 0x0600047B RID: 1147
		[DispId(-2147412992)]
		public virtual extern string imeMode
		{
			[TypeLibFunc(20)]
			[DispId(-2147412992)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412992)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600047E RID: 1150
		// (set) Token: 0x0600047D RID: 1149
		[DispId(-2147412991)]
		public virtual extern string rubyAlign
		{
			[TypeLibFunc(20)]
			[DispId(-2147412991)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412991)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000480 RID: 1152
		// (set) Token: 0x0600047F RID: 1151
		[DispId(-2147412990)]
		public virtual extern string rubyPosition
		{
			[DispId(-2147412990)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412990)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000482 RID: 1154
		// (set) Token: 0x06000481 RID: 1153
		[DispId(-2147412989)]
		public virtual extern string rubyOverhang
		{
			[TypeLibFunc(20)]
			[DispId(-2147412989)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412989)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000484 RID: 1156
		// (set) Token: 0x06000483 RID: 1155
		[DispId(-2147412985)]
		public virtual extern object layoutGridChar
		{
			[DispId(-2147412985)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412985)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000486 RID: 1158
		// (set) Token: 0x06000485 RID: 1157
		[DispId(-2147412984)]
		public virtual extern object layoutGridLine
		{
			[TypeLibFunc(20)]
			[DispId(-2147412984)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412984)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000488 RID: 1160
		// (set) Token: 0x06000487 RID: 1159
		[DispId(-2147412983)]
		public virtual extern string layoutGridMode
		{
			[DispId(-2147412983)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412983)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600048A RID: 1162
		// (set) Token: 0x06000489 RID: 1161
		[DispId(-2147412982)]
		public virtual extern string layoutGridType
		{
			[TypeLibFunc(20)]
			[DispId(-2147412982)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412982)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600048C RID: 1164
		// (set) Token: 0x0600048B RID: 1163
		[DispId(-2147412981)]
		public virtual extern string layoutGrid
		{
			[DispId(-2147412981)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147412981)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600048E RID: 1166
		// (set) Token: 0x0600048D RID: 1165
		[DispId(-2147412978)]
		public virtual extern string wordBreak
		{
			[TypeLibFunc(20)]
			[DispId(-2147412978)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412978)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000490 RID: 1168
		// (set) Token: 0x0600048F RID: 1167
		[DispId(-2147412979)]
		public virtual extern string lineBreak
		{
			[DispId(-2147412979)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412979)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000492 RID: 1170
		// (set) Token: 0x06000491 RID: 1169
		[DispId(-2147412977)]
		public virtual extern string textJustify
		{
			[DispId(-2147412977)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412977)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000494 RID: 1172
		// (set) Token: 0x06000493 RID: 1171
		[DispId(-2147412976)]
		public virtual extern string textJustifyTrim
		{
			[DispId(-2147412976)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412976)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000496 RID: 1174
		// (set) Token: 0x06000495 RID: 1173
		[DispId(-2147412975)]
		public virtual extern object textKashida
		{
			[TypeLibFunc(20)]
			[DispId(-2147412975)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412975)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000498 RID: 1176
		// (set) Token: 0x06000497 RID: 1175
		[DispId(-2147412980)]
		public virtual extern string textAutospace
		{
			[DispId(-2147412980)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412980)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600049A RID: 1178
		// (set) Token: 0x06000499 RID: 1177
		[DispId(-2147412973)]
		public virtual extern string overflowX
		{
			[DispId(-2147412973)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412973)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600049C RID: 1180
		// (set) Token: 0x0600049B RID: 1179
		[DispId(-2147412972)]
		public virtual extern string overflowY
		{
			[TypeLibFunc(20)]
			[DispId(-2147412972)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412972)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600049E RID: 1182
		// (set) Token: 0x0600049D RID: 1181
		[DispId(-2147412965)]
		public virtual extern string accelerator
		{
			[TypeLibFunc(20)]
			[DispId(-2147412965)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412965)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060004A0 RID: 1184
		// (set) Token: 0x0600049F RID: 1183
		[DispId(-2147412957)]
		public virtual extern string layoutFlow
		{
			[TypeLibFunc(20)]
			[DispId(-2147412957)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412957)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060004A2 RID: 1186
		// (set) Token: 0x060004A1 RID: 1185
		[DispId(-2147412959)]
		public virtual extern object zoom
		{
			[DispId(-2147412959)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412959)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060004A4 RID: 1188
		// (set) Token: 0x060004A3 RID: 1187
		[DispId(-2147412954)]
		public virtual extern string wordWrap
		{
			[TypeLibFunc(20)]
			[DispId(-2147412954)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412954)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060004A6 RID: 1190
		// (set) Token: 0x060004A5 RID: 1189
		[DispId(-2147412953)]
		public virtual extern string textUnderlinePosition
		{
			[DispId(-2147412953)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412953)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060004A8 RID: 1192
		// (set) Token: 0x060004A7 RID: 1191
		[DispId(-2147412932)]
		public virtual extern object scrollbarBaseColor
		{
			[DispId(-2147412932)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412932)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060004AA RID: 1194
		// (set) Token: 0x060004A9 RID: 1193
		[DispId(-2147412931)]
		public virtual extern object scrollbarFaceColor
		{
			[DispId(-2147412931)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412931)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060004AC RID: 1196
		// (set) Token: 0x060004AB RID: 1195
		[DispId(-2147412930)]
		public virtual extern object scrollbar3dLightColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412930)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412930)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060004AE RID: 1198
		// (set) Token: 0x060004AD RID: 1197
		[DispId(-2147412929)]
		public virtual extern object scrollbarShadowColor
		{
			[DispId(-2147412929)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412929)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060004B0 RID: 1200
		// (set) Token: 0x060004AF RID: 1199
		[DispId(-2147412928)]
		public virtual extern object scrollbarHighlightColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412928)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412928)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060004B2 RID: 1202
		// (set) Token: 0x060004B1 RID: 1201
		[DispId(-2147412927)]
		public virtual extern object scrollbarDarkShadowColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412927)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412927)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060004B4 RID: 1204
		// (set) Token: 0x060004B3 RID: 1203
		[DispId(-2147412926)]
		public virtual extern object scrollbarArrowColor
		{
			[DispId(-2147412926)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412926)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060004B6 RID: 1206
		// (set) Token: 0x060004B5 RID: 1205
		[DispId(-2147412916)]
		public virtual extern object scrollbarTrackColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412916)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412916)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060004B8 RID: 1208
		// (set) Token: 0x060004B7 RID: 1207
		[DispId(-2147412920)]
		public virtual extern string writingMode
		{
			[TypeLibFunc(20)]
			[DispId(-2147412920)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412920)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060004BA RID: 1210
		// (set) Token: 0x060004B9 RID: 1209
		[DispId(-2147412909)]
		public virtual extern string textAlignLast
		{
			[DispId(-2147412909)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412909)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060004BC RID: 1212
		// (set) Token: 0x060004BB RID: 1211
		[DispId(-2147412908)]
		public virtual extern object textKashidaSpace
		{
			[TypeLibFunc(20)]
			[DispId(-2147412908)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412908)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060004BE RID: 1214
		// (set) Token: 0x060004BD RID: 1213
		[DispId(-2147412903)]
		public virtual extern string textOverflow
		{
			[DispId(-2147412903)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412903)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060004C0 RID: 1216
		// (set) Token: 0x060004BF RID: 1215
		[DispId(-2147412901)]
		public virtual extern object minHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147412901)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412901)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060004C2 RID: 1218
		// (set) Token: 0x060004C1 RID: 1217
		public virtual extern string IHTMLStyle_fontFamily
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060004C4 RID: 1220
		// (set) Token: 0x060004C3 RID: 1219
		public virtual extern string IHTMLStyle_fontStyle
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060004C6 RID: 1222
		// (set) Token: 0x060004C5 RID: 1221
		public virtual extern string IHTMLStyle_fontVariant
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060004C8 RID: 1224
		// (set) Token: 0x060004C7 RID: 1223
		public virtual extern string IHTMLStyle_fontWeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060004CA RID: 1226
		// (set) Token: 0x060004C9 RID: 1225
		public virtual extern object IHTMLStyle_fontSize
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060004CC RID: 1228
		// (set) Token: 0x060004CB RID: 1227
		public virtual extern string IHTMLStyle_font
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060004CE RID: 1230
		// (set) Token: 0x060004CD RID: 1229
		public virtual extern object IHTMLStyle_color
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060004D0 RID: 1232
		// (set) Token: 0x060004CF RID: 1231
		public virtual extern string IHTMLStyle_background
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060004D2 RID: 1234
		// (set) Token: 0x060004D1 RID: 1233
		public virtual extern object IHTMLStyle_backgroundColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060004D4 RID: 1236
		// (set) Token: 0x060004D3 RID: 1235
		public virtual extern string IHTMLStyle_backgroundImage
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060004D6 RID: 1238
		// (set) Token: 0x060004D5 RID: 1237
		public virtual extern string IHTMLStyle_backgroundRepeat
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060004D8 RID: 1240
		// (set) Token: 0x060004D7 RID: 1239
		public virtual extern string IHTMLStyle_backgroundAttachment
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060004DA RID: 1242
		// (set) Token: 0x060004D9 RID: 1241
		public virtual extern string IHTMLStyle_backgroundPosition
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060004DC RID: 1244
		// (set) Token: 0x060004DB RID: 1243
		public virtual extern object IHTMLStyle_backgroundPositionX
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060004DE RID: 1246
		// (set) Token: 0x060004DD RID: 1245
		public virtual extern object IHTMLStyle_backgroundPositionY
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060004E0 RID: 1248
		// (set) Token: 0x060004DF RID: 1247
		public virtual extern object IHTMLStyle_wordSpacing
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060004E2 RID: 1250
		// (set) Token: 0x060004E1 RID: 1249
		public virtual extern object IHTMLStyle_letterSpacing
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060004E4 RID: 1252
		// (set) Token: 0x060004E3 RID: 1251
		public virtual extern string IHTMLStyle_textDecoration
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060004E6 RID: 1254
		// (set) Token: 0x060004E5 RID: 1253
		public virtual extern bool IHTMLStyle_textDecorationNone
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060004E8 RID: 1256
		// (set) Token: 0x060004E7 RID: 1255
		public virtual extern bool IHTMLStyle_textDecorationUnderline
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060004EA RID: 1258
		// (set) Token: 0x060004E9 RID: 1257
		public virtual extern bool IHTMLStyle_textDecorationOverline
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060004EC RID: 1260
		// (set) Token: 0x060004EB RID: 1259
		public virtual extern bool IHTMLStyle_textDecorationLineThrough
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060004EE RID: 1262
		// (set) Token: 0x060004ED RID: 1261
		public virtual extern bool IHTMLStyle_textDecorationBlink
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060004F0 RID: 1264
		// (set) Token: 0x060004EF RID: 1263
		public virtual extern object IHTMLStyle_verticalAlign
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060004F2 RID: 1266
		// (set) Token: 0x060004F1 RID: 1265
		public virtual extern string IHTMLStyle_textTransform
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060004F4 RID: 1268
		// (set) Token: 0x060004F3 RID: 1267
		public virtual extern string IHTMLStyle_textAlign
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060004F6 RID: 1270
		// (set) Token: 0x060004F5 RID: 1269
		public virtual extern object IHTMLStyle_textIndent
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060004F8 RID: 1272
		// (set) Token: 0x060004F7 RID: 1271
		public virtual extern object IHTMLStyle_lineHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060004FA RID: 1274
		// (set) Token: 0x060004F9 RID: 1273
		public virtual extern object IHTMLStyle_marginTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060004FC RID: 1276
		// (set) Token: 0x060004FB RID: 1275
		public virtual extern object IHTMLStyle_marginRight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060004FE RID: 1278
		// (set) Token: 0x060004FD RID: 1277
		public virtual extern object IHTMLStyle_marginBottom
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000500 RID: 1280
		// (set) Token: 0x060004FF RID: 1279
		public virtual extern object IHTMLStyle_marginLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000502 RID: 1282
		// (set) Token: 0x06000501 RID: 1281
		public virtual extern string IHTMLStyle_margin
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000504 RID: 1284
		// (set) Token: 0x06000503 RID: 1283
		public virtual extern object IHTMLStyle_paddingTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000506 RID: 1286
		// (set) Token: 0x06000505 RID: 1285
		public virtual extern object IHTMLStyle_paddingRight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000508 RID: 1288
		// (set) Token: 0x06000507 RID: 1287
		public virtual extern object IHTMLStyle_paddingBottom
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600050A RID: 1290
		// (set) Token: 0x06000509 RID: 1289
		public virtual extern object IHTMLStyle_paddingLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600050C RID: 1292
		// (set) Token: 0x0600050B RID: 1291
		public virtual extern string IHTMLStyle_padding
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600050E RID: 1294
		// (set) Token: 0x0600050D RID: 1293
		public virtual extern string IHTMLStyle_border
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000510 RID: 1296
		// (set) Token: 0x0600050F RID: 1295
		public virtual extern string IHTMLStyle_borderTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000512 RID: 1298
		// (set) Token: 0x06000511 RID: 1297
		public virtual extern string IHTMLStyle_borderRight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000514 RID: 1300
		// (set) Token: 0x06000513 RID: 1299
		public virtual extern string IHTMLStyle_borderBottom
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000516 RID: 1302
		// (set) Token: 0x06000515 RID: 1301
		public virtual extern string IHTMLStyle_borderLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000518 RID: 1304
		// (set) Token: 0x06000517 RID: 1303
		public virtual extern string IHTMLStyle_borderColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600051A RID: 1306
		// (set) Token: 0x06000519 RID: 1305
		public virtual extern object IHTMLStyle_borderTopColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600051C RID: 1308
		// (set) Token: 0x0600051B RID: 1307
		public virtual extern object IHTMLStyle_borderRightColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600051E RID: 1310
		// (set) Token: 0x0600051D RID: 1309
		public virtual extern object IHTMLStyle_borderBottomColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000520 RID: 1312
		// (set) Token: 0x0600051F RID: 1311
		public virtual extern object IHTMLStyle_borderLeftColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000522 RID: 1314
		// (set) Token: 0x06000521 RID: 1313
		public virtual extern string IHTMLStyle_borderWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000524 RID: 1316
		// (set) Token: 0x06000523 RID: 1315
		public virtual extern object IHTMLStyle_borderTopWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000526 RID: 1318
		// (set) Token: 0x06000525 RID: 1317
		public virtual extern object IHTMLStyle_borderRightWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000528 RID: 1320
		// (set) Token: 0x06000527 RID: 1319
		public virtual extern object IHTMLStyle_borderBottomWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600052A RID: 1322
		// (set) Token: 0x06000529 RID: 1321
		public virtual extern object IHTMLStyle_borderLeftWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600052C RID: 1324
		// (set) Token: 0x0600052B RID: 1323
		public virtual extern string IHTMLStyle_borderStyle
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600052E RID: 1326
		// (set) Token: 0x0600052D RID: 1325
		public virtual extern string IHTMLStyle_borderTopStyle
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000530 RID: 1328
		// (set) Token: 0x0600052F RID: 1327
		public virtual extern string IHTMLStyle_borderRightStyle
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000532 RID: 1330
		// (set) Token: 0x06000531 RID: 1329
		public virtual extern string IHTMLStyle_borderBottomStyle
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000534 RID: 1332
		// (set) Token: 0x06000533 RID: 1331
		public virtual extern string IHTMLStyle_borderLeftStyle
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000536 RID: 1334
		// (set) Token: 0x06000535 RID: 1333
		public virtual extern object IHTMLStyle_width
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000538 RID: 1336
		// (set) Token: 0x06000537 RID: 1335
		public virtual extern object IHTMLStyle_height
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600053A RID: 1338
		// (set) Token: 0x06000539 RID: 1337
		public virtual extern string IHTMLStyle_styleFloat
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600053C RID: 1340
		// (set) Token: 0x0600053B RID: 1339
		public virtual extern string IHTMLStyle_clear
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600053E RID: 1342
		// (set) Token: 0x0600053D RID: 1341
		public virtual extern string IHTMLStyle_display
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000540 RID: 1344
		// (set) Token: 0x0600053F RID: 1343
		public virtual extern string IHTMLStyle_visibility
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000542 RID: 1346
		// (set) Token: 0x06000541 RID: 1345
		public virtual extern string IHTMLStyle_listStyleType
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000544 RID: 1348
		// (set) Token: 0x06000543 RID: 1347
		public virtual extern string IHTMLStyle_listStylePosition
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000546 RID: 1350
		// (set) Token: 0x06000545 RID: 1349
		public virtual extern string IHTMLStyle_listStyleImage
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000548 RID: 1352
		// (set) Token: 0x06000547 RID: 1351
		public virtual extern string IHTMLStyle_listStyle
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600054A RID: 1354
		// (set) Token: 0x06000549 RID: 1353
		public virtual extern string IHTMLStyle_whiteSpace
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600054C RID: 1356
		// (set) Token: 0x0600054B RID: 1355
		public virtual extern object IHTMLStyle_top
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600054E RID: 1358
		// (set) Token: 0x0600054D RID: 1357
		public virtual extern object IHTMLStyle_left
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600054F RID: 1359
		public virtual extern string IHTMLStyle_position
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000551 RID: 1361
		// (set) Token: 0x06000550 RID: 1360
		public virtual extern object IHTMLStyle_zIndex
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000553 RID: 1363
		// (set) Token: 0x06000552 RID: 1362
		public virtual extern string IHTMLStyle_overflow
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000555 RID: 1365
		// (set) Token: 0x06000554 RID: 1364
		public virtual extern string IHTMLStyle_pageBreakBefore
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000557 RID: 1367
		// (set) Token: 0x06000556 RID: 1366
		public virtual extern string IHTMLStyle_pageBreakAfter
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000559 RID: 1369
		// (set) Token: 0x06000558 RID: 1368
		public virtual extern string IHTMLStyle_cssText
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600055B RID: 1371
		// (set) Token: 0x0600055A RID: 1370
		public virtual extern int IHTMLStyle_pixelTop
		{
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600055D RID: 1373
		// (set) Token: 0x0600055C RID: 1372
		public virtual extern int IHTMLStyle_pixelLeft
		{
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600055F RID: 1375
		// (set) Token: 0x0600055E RID: 1374
		public virtual extern int IHTMLStyle_pixelWidth
		{
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000561 RID: 1377
		// (set) Token: 0x06000560 RID: 1376
		public virtual extern int IHTMLStyle_pixelHeight
		{
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000563 RID: 1379
		// (set) Token: 0x06000562 RID: 1378
		public virtual extern float IHTMLStyle_posTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000565 RID: 1381
		// (set) Token: 0x06000564 RID: 1380
		public virtual extern float IHTMLStyle_posLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000567 RID: 1383
		// (set) Token: 0x06000566 RID: 1382
		public virtual extern float IHTMLStyle_posWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000569 RID: 1385
		// (set) Token: 0x06000568 RID: 1384
		public virtual extern float IHTMLStyle_posHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600056B RID: 1387
		// (set) Token: 0x0600056A RID: 1386
		public virtual extern string IHTMLStyle_cursor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600056D RID: 1389
		// (set) Token: 0x0600056C RID: 1388
		public virtual extern string IHTMLStyle_clip
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600056F RID: 1391
		// (set) Token: 0x0600056E RID: 1390
		public virtual extern string IHTMLStyle_filter
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x06000570 RID: 1392
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLStyle_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06000571 RID: 1393
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLStyle_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06000572 RID: 1394
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLStyle_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x06000573 RID: 1395
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLStyle_toString();

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000575 RID: 1397
		// (set) Token: 0x06000574 RID: 1396
		public virtual extern string IHTMLStyle2_tableLayout
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000577 RID: 1399
		// (set) Token: 0x06000576 RID: 1398
		public virtual extern string IHTMLStyle2_borderCollapse
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000579 RID: 1401
		// (set) Token: 0x06000578 RID: 1400
		public virtual extern string IHTMLStyle2_direction
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600057B RID: 1403
		// (set) Token: 0x0600057A RID: 1402
		public virtual extern string IHTMLStyle2_behavior
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x0600057C RID: 1404
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLStyle2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600057D RID: 1405
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLStyle2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600057E RID: 1406
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLStyle2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000580 RID: 1408
		// (set) Token: 0x0600057F RID: 1407
		public virtual extern string IHTMLStyle2_position
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000582 RID: 1410
		// (set) Token: 0x06000581 RID: 1409
		public virtual extern string IHTMLStyle2_unicodeBidi
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000584 RID: 1412
		// (set) Token: 0x06000583 RID: 1411
		public virtual extern object IHTMLStyle2_bottom
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000586 RID: 1414
		// (set) Token: 0x06000585 RID: 1413
		public virtual extern object IHTMLStyle2_right
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000588 RID: 1416
		// (set) Token: 0x06000587 RID: 1415
		public virtual extern int IHTMLStyle2_pixelBottom
		{
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x0600058A RID: 1418
		// (set) Token: 0x06000589 RID: 1417
		public virtual extern int IHTMLStyle2_pixelRight
		{
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600058C RID: 1420
		// (set) Token: 0x0600058B RID: 1419
		public virtual extern float IHTMLStyle2_posBottom
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600058E RID: 1422
		// (set) Token: 0x0600058D RID: 1421
		public virtual extern float IHTMLStyle2_posRight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000590 RID: 1424
		// (set) Token: 0x0600058F RID: 1423
		public virtual extern string IHTMLStyle2_imeMode
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000592 RID: 1426
		// (set) Token: 0x06000591 RID: 1425
		public virtual extern string IHTMLStyle2_rubyAlign
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000594 RID: 1428
		// (set) Token: 0x06000593 RID: 1427
		public virtual extern string IHTMLStyle2_rubyPosition
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000596 RID: 1430
		// (set) Token: 0x06000595 RID: 1429
		public virtual extern string IHTMLStyle2_rubyOverhang
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000598 RID: 1432
		// (set) Token: 0x06000597 RID: 1431
		public virtual extern object IHTMLStyle2_layoutGridChar
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600059A RID: 1434
		// (set) Token: 0x06000599 RID: 1433
		public virtual extern object IHTMLStyle2_layoutGridLine
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x0600059C RID: 1436
		// (set) Token: 0x0600059B RID: 1435
		public virtual extern string IHTMLStyle2_layoutGridMode
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x0600059E RID: 1438
		// (set) Token: 0x0600059D RID: 1437
		public virtual extern string IHTMLStyle2_layoutGridType
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060005A0 RID: 1440
		// (set) Token: 0x0600059F RID: 1439
		public virtual extern string IHTMLStyle2_layoutGrid
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060005A2 RID: 1442
		// (set) Token: 0x060005A1 RID: 1441
		public virtual extern string IHTMLStyle2_wordBreak
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060005A4 RID: 1444
		// (set) Token: 0x060005A3 RID: 1443
		public virtual extern string IHTMLStyle2_lineBreak
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060005A6 RID: 1446
		// (set) Token: 0x060005A5 RID: 1445
		public virtual extern string IHTMLStyle2_textJustify
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060005A8 RID: 1448
		// (set) Token: 0x060005A7 RID: 1447
		public virtual extern string IHTMLStyle2_textJustifyTrim
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060005AA RID: 1450
		// (set) Token: 0x060005A9 RID: 1449
		public virtual extern object IHTMLStyle2_textKashida
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060005AC RID: 1452
		// (set) Token: 0x060005AB RID: 1451
		public virtual extern string IHTMLStyle2_textAutospace
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060005AE RID: 1454
		// (set) Token: 0x060005AD RID: 1453
		public virtual extern string IHTMLStyle2_overflowX
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060005B0 RID: 1456
		// (set) Token: 0x060005AF RID: 1455
		public virtual extern string IHTMLStyle2_overflowY
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060005B2 RID: 1458
		// (set) Token: 0x060005B1 RID: 1457
		public virtual extern string IHTMLStyle2_accelerator
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060005B4 RID: 1460
		// (set) Token: 0x060005B3 RID: 1459
		public virtual extern string IHTMLStyle3_layoutFlow
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060005B6 RID: 1462
		// (set) Token: 0x060005B5 RID: 1461
		public virtual extern object IHTMLStyle3_zoom
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060005B8 RID: 1464
		// (set) Token: 0x060005B7 RID: 1463
		public virtual extern string IHTMLStyle3_wordWrap
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060005BA RID: 1466
		// (set) Token: 0x060005B9 RID: 1465
		public virtual extern string IHTMLStyle3_textUnderlinePosition
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060005BC RID: 1468
		// (set) Token: 0x060005BB RID: 1467
		public virtual extern object IHTMLStyle3_scrollbarBaseColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060005BE RID: 1470
		// (set) Token: 0x060005BD RID: 1469
		public virtual extern object IHTMLStyle3_scrollbarFaceColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060005C0 RID: 1472
		// (set) Token: 0x060005BF RID: 1471
		public virtual extern object IHTMLStyle3_scrollbar3dLightColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060005C2 RID: 1474
		// (set) Token: 0x060005C1 RID: 1473
		public virtual extern object IHTMLStyle3_scrollbarShadowColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060005C4 RID: 1476
		// (set) Token: 0x060005C3 RID: 1475
		public virtual extern object IHTMLStyle3_scrollbarHighlightColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060005C6 RID: 1478
		// (set) Token: 0x060005C5 RID: 1477
		public virtual extern object IHTMLStyle3_scrollbarDarkShadowColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060005C8 RID: 1480
		// (set) Token: 0x060005C7 RID: 1479
		public virtual extern object IHTMLStyle3_scrollbarArrowColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060005CA RID: 1482
		// (set) Token: 0x060005C9 RID: 1481
		public virtual extern object IHTMLStyle3_scrollbarTrackColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060005CC RID: 1484
		// (set) Token: 0x060005CB RID: 1483
		public virtual extern string IHTMLStyle3_writingMode
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060005CE RID: 1486
		// (set) Token: 0x060005CD RID: 1485
		public virtual extern string IHTMLStyle3_textAlignLast
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060005D0 RID: 1488
		// (set) Token: 0x060005CF RID: 1487
		public virtual extern object IHTMLStyle3_textKashidaSpace
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060005D2 RID: 1490
		// (set) Token: 0x060005D1 RID: 1489
		public virtual extern string IHTMLStyle4_textOverflow
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060005D4 RID: 1492
		// (set) Token: 0x060005D3 RID: 1491
		public virtual extern object IHTMLStyle4_minHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}
	}
}
