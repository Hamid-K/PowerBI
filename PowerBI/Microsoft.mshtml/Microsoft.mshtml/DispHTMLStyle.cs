using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000050 RID: 80
	[Guid("3050F55A-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[ComImport]
	public interface DispHTMLStyle
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600029B RID: 667
		// (set) Token: 0x0600029A RID: 666
		[DispId(-2147413094)]
		string fontFamily
		{
			[DispId(-2147413094)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600029D RID: 669
		// (set) Token: 0x0600029C RID: 668
		[DispId(-2147413088)]
		string fontStyle
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

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600029F RID: 671
		// (set) Token: 0x0600029E RID: 670
		[DispId(-2147413087)]
		string fontVariant
		{
			[TypeLibFunc(20)]
			[DispId(-2147413087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060002A1 RID: 673
		// (set) Token: 0x060002A0 RID: 672
		[DispId(-2147413085)]
		string fontWeight
		{
			[DispId(-2147413085)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413085)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060002A3 RID: 675
		// (set) Token: 0x060002A2 RID: 674
		[DispId(-2147413093)]
		object fontSize
		{
			[DispId(-2147413093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060002A5 RID: 677
		// (set) Token: 0x060002A4 RID: 676
		[DispId(-2147413071)]
		string font
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413071)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413071)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060002A7 RID: 679
		// (set) Token: 0x060002A6 RID: 678
		[DispId(-2147413110)]
		object color
		{
			[TypeLibFunc(20)]
			[DispId(-2147413110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060002A9 RID: 681
		// (set) Token: 0x060002A8 RID: 680
		[DispId(-2147413080)]
		string background
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060002AB RID: 683
		// (set) Token: 0x060002AA RID: 682
		[DispId(-501)]
		object backgroundColor
		{
			[DispId(-501)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-501)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060002AD RID: 685
		// (set) Token: 0x060002AC RID: 684
		[DispId(-2147413111)]
		string backgroundImage
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

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060002AF RID: 687
		// (set) Token: 0x060002AE RID: 686
		[DispId(-2147413068)]
		string backgroundRepeat
		{
			[TypeLibFunc(20)]
			[DispId(-2147413068)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413068)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060002B1 RID: 689
		// (set) Token: 0x060002B0 RID: 688
		[DispId(-2147413067)]
		string backgroundAttachment
		{
			[DispId(-2147413067)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413067)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060002B3 RID: 691
		// (set) Token: 0x060002B2 RID: 690
		[DispId(-2147413066)]
		string backgroundPosition
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060002B5 RID: 693
		// (set) Token: 0x060002B4 RID: 692
		[DispId(-2147413079)]
		object backgroundPositionX
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

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060002B7 RID: 695
		// (set) Token: 0x060002B6 RID: 694
		[DispId(-2147413078)]
		object backgroundPositionY
		{
			[TypeLibFunc(20)]
			[DispId(-2147413078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060002B9 RID: 697
		// (set) Token: 0x060002B8 RID: 696
		[DispId(-2147413065)]
		object wordSpacing
		{
			[TypeLibFunc(20)]
			[DispId(-2147413065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060002BB RID: 699
		// (set) Token: 0x060002BA RID: 698
		[DispId(-2147413104)]
		object letterSpacing
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

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060002BD RID: 701
		// (set) Token: 0x060002BC RID: 700
		[DispId(-2147413077)]
		string textDecoration
		{
			[TypeLibFunc(20)]
			[DispId(-2147413077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060002BF RID: 703
		// (set) Token: 0x060002BE RID: 702
		[DispId(-2147413089)]
		bool textDecorationNone
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

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060002C1 RID: 705
		// (set) Token: 0x060002C0 RID: 704
		[DispId(-2147413091)]
		bool textDecorationUnderline
		{
			[DispId(-2147413091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147413091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060002C3 RID: 707
		// (set) Token: 0x060002C2 RID: 706
		[DispId(-2147413043)]
		bool textDecorationOverline
		{
			[TypeLibFunc(20)]
			[DispId(-2147413043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147413043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060002C5 RID: 709
		// (set) Token: 0x060002C4 RID: 708
		[DispId(-2147413092)]
		bool textDecorationLineThrough
		{
			[TypeLibFunc(20)]
			[DispId(-2147413092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147413092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060002C7 RID: 711
		// (set) Token: 0x060002C6 RID: 710
		[DispId(-2147413090)]
		bool textDecorationBlink
		{
			[DispId(-2147413090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147413090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060002C9 RID: 713
		// (set) Token: 0x060002C8 RID: 712
		[DispId(-2147413064)]
		object verticalAlign
		{
			[DispId(-2147413064)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413064)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060002CB RID: 715
		// (set) Token: 0x060002CA RID: 714
		[DispId(-2147413108)]
		string textTransform
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

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060002CD RID: 717
		// (set) Token: 0x060002CC RID: 716
		[DispId(-2147418040)]
		string textAlign
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

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060002CF RID: 719
		// (set) Token: 0x060002CE RID: 718
		[DispId(-2147413105)]
		object textIndent
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

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060002D1 RID: 721
		// (set) Token: 0x060002D0 RID: 720
		[DispId(-2147413106)]
		object lineHeight
		{
			[DispId(-2147413106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060002D3 RID: 723
		// (set) Token: 0x060002D2 RID: 722
		[DispId(-2147413075)]
		object marginTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060002D5 RID: 725
		// (set) Token: 0x060002D4 RID: 724
		[DispId(-2147413074)]
		object marginRight
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

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060002D7 RID: 727
		// (set) Token: 0x060002D6 RID: 726
		[DispId(-2147413073)]
		object marginBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147413073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060002D9 RID: 729
		// (set) Token: 0x060002D8 RID: 728
		[DispId(-2147413072)]
		object marginLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147413072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413072)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060002DB RID: 731
		// (set) Token: 0x060002DA RID: 730
		[DispId(-2147413076)]
		string margin
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060002DD RID: 733
		// (set) Token: 0x060002DC RID: 732
		[DispId(-2147413100)]
		object paddingTop
		{
			[DispId(-2147413100)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060002DF RID: 735
		// (set) Token: 0x060002DE RID: 734
		[DispId(-2147413099)]
		object paddingRight
		{
			[DispId(-2147413099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060002E1 RID: 737
		// (set) Token: 0x060002E0 RID: 736
		[DispId(-2147413098)]
		object paddingBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147413098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060002E3 RID: 739
		// (set) Token: 0x060002E2 RID: 738
		[DispId(-2147413097)]
		object paddingLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147413097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060002E5 RID: 741
		// (set) Token: 0x060002E4 RID: 740
		[DispId(-2147413101)]
		string padding
		{
			[DispId(-2147413101)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060002E7 RID: 743
		// (set) Token: 0x060002E6 RID: 742
		[DispId(-2147413063)]
		string border
		{
			[DispId(-2147413063)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413063)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060002E9 RID: 745
		// (set) Token: 0x060002E8 RID: 744
		[DispId(-2147413062)]
		string borderTop
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

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060002EB RID: 747
		// (set) Token: 0x060002EA RID: 746
		[DispId(-2147413061)]
		string borderRight
		{
			[DispId(-2147413061)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413061)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060002ED RID: 749
		// (set) Token: 0x060002EC RID: 748
		[DispId(-2147413060)]
		string borderBottom
		{
			[DispId(-2147413060)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413060)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060002EF RID: 751
		// (set) Token: 0x060002EE RID: 750
		[DispId(-2147413059)]
		string borderLeft
		{
			[DispId(-2147413059)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413059)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060002F1 RID: 753
		// (set) Token: 0x060002F0 RID: 752
		[DispId(-2147413058)]
		string borderColor
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

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060002F3 RID: 755
		// (set) Token: 0x060002F2 RID: 754
		[DispId(-2147413057)]
		object borderTopColor
		{
			[DispId(-2147413057)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060002F5 RID: 757
		// (set) Token: 0x060002F4 RID: 756
		[DispId(-2147413056)]
		object borderRightColor
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

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060002F7 RID: 759
		// (set) Token: 0x060002F6 RID: 758
		[DispId(-2147413055)]
		object borderBottomColor
		{
			[DispId(-2147413055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060002F9 RID: 761
		// (set) Token: 0x060002F8 RID: 760
		[DispId(-2147413054)]
		object borderLeftColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060002FB RID: 763
		// (set) Token: 0x060002FA RID: 762
		[DispId(-2147413053)]
		string borderWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060002FD RID: 765
		// (set) Token: 0x060002FC RID: 764
		[DispId(-2147413052)]
		object borderTopWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060002FF RID: 767
		// (set) Token: 0x060002FE RID: 766
		[DispId(-2147413051)]
		object borderRightWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413051)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413051)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000301 RID: 769
		// (set) Token: 0x06000300 RID: 768
		[DispId(-2147413050)]
		object borderBottomWidth
		{
			[DispId(-2147413050)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413050)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000303 RID: 771
		// (set) Token: 0x06000302 RID: 770
		[DispId(-2147413049)]
		object borderLeftWidth
		{
			[DispId(-2147413049)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000305 RID: 773
		// (set) Token: 0x06000304 RID: 772
		[DispId(-2147413048)]
		string borderStyle
		{
			[DispId(-2147413048)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000307 RID: 775
		// (set) Token: 0x06000306 RID: 774
		[DispId(-2147413047)]
		string borderTopStyle
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

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000309 RID: 777
		// (set) Token: 0x06000308 RID: 776
		[DispId(-2147413046)]
		string borderRightStyle
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

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600030B RID: 779
		// (set) Token: 0x0600030A RID: 778
		[DispId(-2147413045)]
		string borderBottomStyle
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

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600030D RID: 781
		// (set) Token: 0x0600030C RID: 780
		[DispId(-2147413044)]
		string borderLeftStyle
		{
			[DispId(-2147413044)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600030F RID: 783
		// (set) Token: 0x0600030E RID: 782
		[DispId(-2147418107)]
		object width
		{
			[DispId(-2147418107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000311 RID: 785
		// (set) Token: 0x06000310 RID: 784
		[DispId(-2147418106)]
		object height
		{
			[DispId(-2147418106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000313 RID: 787
		// (set) Token: 0x06000312 RID: 786
		[DispId(-2147413042)]
		string styleFloat
		{
			[TypeLibFunc(20)]
			[DispId(-2147413042)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413042)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000315 RID: 789
		// (set) Token: 0x06000314 RID: 788
		[DispId(-2147413096)]
		string clear
		{
			[DispId(-2147413096)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413096)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000317 RID: 791
		// (set) Token: 0x06000316 RID: 790
		[DispId(-2147413041)]
		string display
		{
			[TypeLibFunc(20)]
			[DispId(-2147413041)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413041)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000319 RID: 793
		// (set) Token: 0x06000318 RID: 792
		[DispId(-2147413032)]
		string visibility
		{
			[TypeLibFunc(20)]
			[DispId(-2147413032)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413032)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600031B RID: 795
		// (set) Token: 0x0600031A RID: 794
		[DispId(-2147413040)]
		string listStyleType
		{
			[DispId(-2147413040)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413040)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600031D RID: 797
		// (set) Token: 0x0600031C RID: 796
		[DispId(-2147413039)]
		string listStylePosition
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

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600031F RID: 799
		// (set) Token: 0x0600031E RID: 798
		[DispId(-2147413038)]
		string listStyleImage
		{
			[TypeLibFunc(20)]
			[DispId(-2147413038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000321 RID: 801
		// (set) Token: 0x06000320 RID: 800
		[DispId(-2147413037)]
		string listStyle
		{
			[DispId(-2147413037)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000323 RID: 803
		// (set) Token: 0x06000322 RID: 802
		[DispId(-2147413036)]
		string whiteSpace
		{
			[DispId(-2147413036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000325 RID: 805
		// (set) Token: 0x06000324 RID: 804
		[DispId(-2147418108)]
		object top
		{
			[TypeLibFunc(20)]
			[DispId(-2147418108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000327 RID: 807
		// (set) Token: 0x06000326 RID: 806
		[DispId(-2147418109)]
		object left
		{
			[TypeLibFunc(20)]
			[DispId(-2147418109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000329 RID: 809
		// (set) Token: 0x06000328 RID: 808
		[DispId(-2147413021)]
		object zIndex
		{
			[DispId(-2147413021)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600032B RID: 811
		// (set) Token: 0x0600032A RID: 810
		[DispId(-2147413102)]
		string overflow
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

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600032D RID: 813
		// (set) Token: 0x0600032C RID: 812
		[DispId(-2147413035)]
		string pageBreakBefore
		{
			[DispId(-2147413035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600032F RID: 815
		// (set) Token: 0x0600032E RID: 814
		[DispId(-2147413034)]
		string pageBreakAfter
		{
			[TypeLibFunc(20)]
			[DispId(-2147413034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000331 RID: 817
		// (set) Token: 0x06000330 RID: 816
		[DispId(-2147413013)]
		string cssText
		{
			[DispId(-2147413013)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000333 RID: 819
		// (set) Token: 0x06000332 RID: 818
		[DispId(-2147414112)]
		int pixelTop
		{
			[DispId(-2147414112)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147414112)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000335 RID: 821
		// (set) Token: 0x06000334 RID: 820
		[DispId(-2147414111)]
		int pixelLeft
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

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000337 RID: 823
		// (set) Token: 0x06000336 RID: 822
		[DispId(-2147414110)]
		int pixelWidth
		{
			[DispId(-2147414110)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147414110)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000339 RID: 825
		// (set) Token: 0x06000338 RID: 824
		[DispId(-2147414109)]
		int pixelHeight
		{
			[TypeLibFunc(84)]
			[DispId(-2147414109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147414109)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600033B RID: 827
		// (set) Token: 0x0600033A RID: 826
		[DispId(-2147414108)]
		float posTop
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

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600033D RID: 829
		// (set) Token: 0x0600033C RID: 828
		[DispId(-2147414107)]
		float posLeft
		{
			[DispId(-2147414107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147414107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600033F RID: 831
		// (set) Token: 0x0600033E RID: 830
		[DispId(-2147414106)]
		float posWidth
		{
			[DispId(-2147414106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147414106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000341 RID: 833
		// (set) Token: 0x06000340 RID: 832
		[DispId(-2147414105)]
		float posHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147414105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147414105)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000343 RID: 835
		// (set) Token: 0x06000342 RID: 834
		[DispId(-2147413010)]
		string cursor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413010)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000345 RID: 837
		// (set) Token: 0x06000344 RID: 836
		[DispId(-2147413020)]
		string clip
		{
			[TypeLibFunc(20)]
			[DispId(-2147413020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000347 RID: 839
		// (set) Token: 0x06000346 RID: 838
		[DispId(-2147413030)]
		string filter
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

		// Token: 0x06000348 RID: 840
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06000349 RID: 841
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600034A RID: 842
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x0600034B RID: 843
		[DispId(-2147414104)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600034D RID: 845
		// (set) Token: 0x0600034C RID: 844
		[DispId(-2147413014)]
		string tableLayout
		{
			[DispId(-2147413014)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600034F RID: 847
		// (set) Token: 0x0600034E RID: 846
		[DispId(-2147413028)]
		string borderCollapse
		{
			[DispId(-2147413028)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000351 RID: 849
		// (set) Token: 0x06000350 RID: 848
		[DispId(-2147412993)]
		string direction
		{
			[TypeLibFunc(20)]
			[DispId(-2147412993)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412993)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000353 RID: 851
		// (set) Token: 0x06000352 RID: 850
		[DispId(-2147412997)]
		string behavior
		{
			[DispId(-2147412997)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412997)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06000354 RID: 852
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06000355 RID: 853
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06000356 RID: 854
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000358 RID: 856
		// (set) Token: 0x06000357 RID: 855
		[DispId(-2147413022)]
		string position
		{
			[DispId(-2147413022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600035A RID: 858
		// (set) Token: 0x06000359 RID: 857
		[DispId(-2147412994)]
		string unicodeBidi
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

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600035C RID: 860
		// (set) Token: 0x0600035B RID: 859
		[DispId(-2147418034)]
		object bottom
		{
			[DispId(-2147418034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600035E RID: 862
		// (set) Token: 0x0600035D RID: 861
		[DispId(-2147418035)]
		object right
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

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000360 RID: 864
		// (set) Token: 0x0600035F RID: 863
		[DispId(-2147414103)]
		int pixelBottom
		{
			[DispId(-2147414103)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147414103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000362 RID: 866
		// (set) Token: 0x06000361 RID: 865
		[DispId(-2147414102)]
		int pixelRight
		{
			[TypeLibFunc(84)]
			[DispId(-2147414102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147414102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000364 RID: 868
		// (set) Token: 0x06000363 RID: 867
		[DispId(-2147414101)]
		float posBottom
		{
			[DispId(-2147414101)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147414101)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000366 RID: 870
		// (set) Token: 0x06000365 RID: 869
		[DispId(-2147414100)]
		float posRight
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

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000368 RID: 872
		// (set) Token: 0x06000367 RID: 871
		[DispId(-2147412992)]
		string imeMode
		{
			[DispId(-2147412992)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412992)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600036A RID: 874
		// (set) Token: 0x06000369 RID: 873
		[DispId(-2147412991)]
		string rubyAlign
		{
			[DispId(-2147412991)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412991)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600036C RID: 876
		// (set) Token: 0x0600036B RID: 875
		[DispId(-2147412990)]
		string rubyPosition
		{
			[DispId(-2147412990)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412990)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600036E RID: 878
		// (set) Token: 0x0600036D RID: 877
		[DispId(-2147412989)]
		string rubyOverhang
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

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000370 RID: 880
		// (set) Token: 0x0600036F RID: 879
		[DispId(-2147412985)]
		object layoutGridChar
		{
			[TypeLibFunc(20)]
			[DispId(-2147412985)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412985)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000372 RID: 882
		// (set) Token: 0x06000371 RID: 881
		[DispId(-2147412984)]
		object layoutGridLine
		{
			[DispId(-2147412984)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412984)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000374 RID: 884
		// (set) Token: 0x06000373 RID: 883
		[DispId(-2147412983)]
		string layoutGridMode
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

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000376 RID: 886
		// (set) Token: 0x06000375 RID: 885
		[DispId(-2147412982)]
		string layoutGridType
		{
			[DispId(-2147412982)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412982)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000378 RID: 888
		// (set) Token: 0x06000377 RID: 887
		[DispId(-2147412981)]
		string layoutGrid
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

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600037A RID: 890
		// (set) Token: 0x06000379 RID: 889
		[DispId(-2147412978)]
		string wordBreak
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

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600037C RID: 892
		// (set) Token: 0x0600037B RID: 891
		[DispId(-2147412979)]
		string lineBreak
		{
			[TypeLibFunc(20)]
			[DispId(-2147412979)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412979)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600037E RID: 894
		// (set) Token: 0x0600037D RID: 893
		[DispId(-2147412977)]
		string textJustify
		{
			[TypeLibFunc(20)]
			[DispId(-2147412977)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412977)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000380 RID: 896
		// (set) Token: 0x0600037F RID: 895
		[DispId(-2147412976)]
		string textJustifyTrim
		{
			[TypeLibFunc(20)]
			[DispId(-2147412976)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412976)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000382 RID: 898
		// (set) Token: 0x06000381 RID: 897
		[DispId(-2147412975)]
		object textKashida
		{
			[TypeLibFunc(20)]
			[DispId(-2147412975)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412975)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000384 RID: 900
		// (set) Token: 0x06000383 RID: 899
		[DispId(-2147412980)]
		string textAutospace
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

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000386 RID: 902
		// (set) Token: 0x06000385 RID: 901
		[DispId(-2147412973)]
		string overflowX
		{
			[TypeLibFunc(20)]
			[DispId(-2147412973)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412973)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000388 RID: 904
		// (set) Token: 0x06000387 RID: 903
		[DispId(-2147412972)]
		string overflowY
		{
			[DispId(-2147412972)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412972)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600038A RID: 906
		// (set) Token: 0x06000389 RID: 905
		[DispId(-2147412965)]
		string accelerator
		{
			[DispId(-2147412965)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412965)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600038C RID: 908
		// (set) Token: 0x0600038B RID: 907
		[DispId(-2147412957)]
		string layoutFlow
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

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600038E RID: 910
		// (set) Token: 0x0600038D RID: 909
		[DispId(-2147412959)]
		object zoom
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

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000390 RID: 912
		// (set) Token: 0x0600038F RID: 911
		[DispId(-2147412954)]
		string wordWrap
		{
			[TypeLibFunc(20)]
			[DispId(-2147412954)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412954)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000392 RID: 914
		// (set) Token: 0x06000391 RID: 913
		[DispId(-2147412953)]
		string textUnderlinePosition
		{
			[DispId(-2147412953)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412953)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000394 RID: 916
		// (set) Token: 0x06000393 RID: 915
		[DispId(-2147412932)]
		object scrollbarBaseColor
		{
			[DispId(-2147412932)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412932)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000396 RID: 918
		// (set) Token: 0x06000395 RID: 917
		[DispId(-2147412931)]
		object scrollbarFaceColor
		{
			[DispId(-2147412931)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412931)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000398 RID: 920
		// (set) Token: 0x06000397 RID: 919
		[DispId(-2147412930)]
		object scrollbar3dLightColor
		{
			[DispId(-2147412930)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412930)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600039A RID: 922
		// (set) Token: 0x06000399 RID: 921
		[DispId(-2147412929)]
		object scrollbarShadowColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412929)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412929)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600039C RID: 924
		// (set) Token: 0x0600039B RID: 923
		[DispId(-2147412928)]
		object scrollbarHighlightColor
		{
			[DispId(-2147412928)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412928)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600039E RID: 926
		// (set) Token: 0x0600039D RID: 925
		[DispId(-2147412927)]
		object scrollbarDarkShadowColor
		{
			[DispId(-2147412927)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412927)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060003A0 RID: 928
		// (set) Token: 0x0600039F RID: 927
		[DispId(-2147412926)]
		object scrollbarArrowColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412926)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412926)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060003A2 RID: 930
		// (set) Token: 0x060003A1 RID: 929
		[DispId(-2147412916)]
		object scrollbarTrackColor
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

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060003A4 RID: 932
		// (set) Token: 0x060003A3 RID: 931
		[DispId(-2147412920)]
		string writingMode
		{
			[DispId(-2147412920)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412920)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060003A6 RID: 934
		// (set) Token: 0x060003A5 RID: 933
		[DispId(-2147412909)]
		string textAlignLast
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

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060003A8 RID: 936
		// (set) Token: 0x060003A7 RID: 935
		[DispId(-2147412908)]
		object textKashidaSpace
		{
			[TypeLibFunc(20)]
			[DispId(-2147412908)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412908)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060003AA RID: 938
		// (set) Token: 0x060003A9 RID: 937
		[DispId(-2147412903)]
		string textOverflow
		{
			[TypeLibFunc(20)]
			[DispId(-2147412903)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412903)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060003AC RID: 940
		// (set) Token: 0x060003AB RID: 939
		[DispId(-2147412901)]
		object minHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147412901)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412901)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}
	}
}
