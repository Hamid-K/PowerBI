using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200004D RID: 77
	[Guid("3050F4AC-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLRuleStyle2
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600023D RID: 573
		// (set) Token: 0x0600023C RID: 572
		[DispId(-2147413014)]
		string tableLayout
		{
			[DispId(-2147413014)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413014)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600023F RID: 575
		// (set) Token: 0x0600023E RID: 574
		[DispId(-2147413028)]
		string borderCollapse
		{
			[DispId(-2147413028)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413028)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000241 RID: 577
		// (set) Token: 0x06000240 RID: 576
		[DispId(-2147412993)]
		string direction
		{
			[TypeLibFunc(20)]
			[DispId(-2147412993)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412993)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000243 RID: 579
		// (set) Token: 0x06000242 RID: 578
		[DispId(-2147412997)]
		string behavior
		{
			[DispId(-2147412997)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412997)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000245 RID: 581
		// (set) Token: 0x06000244 RID: 580
		[DispId(-2147413022)]
		string position
		{
			[TypeLibFunc(20)]
			[DispId(-2147413022)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413022)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000247 RID: 583
		// (set) Token: 0x06000246 RID: 582
		[DispId(-2147412994)]
		string unicodeBidi
		{
			[DispId(-2147412994)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412994)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000249 RID: 585
		// (set) Token: 0x06000248 RID: 584
		[DispId(-2147418034)]
		object bottom
		{
			[DispId(-2147418034)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418034)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600024B RID: 587
		// (set) Token: 0x0600024A RID: 586
		[DispId(-2147418035)]
		object right
		{
			[TypeLibFunc(20)]
			[DispId(-2147418035)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418035)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600024D RID: 589
		// (set) Token: 0x0600024C RID: 588
		[DispId(-2147414103)]
		int pixelBottom
		{
			[TypeLibFunc(84)]
			[DispId(-2147414103)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147414103)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600024F RID: 591
		// (set) Token: 0x0600024E RID: 590
		[DispId(-2147414102)]
		int pixelRight
		{
			[DispId(-2147414102)]
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147414102)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000251 RID: 593
		// (set) Token: 0x06000250 RID: 592
		[DispId(-2147414101)]
		float posBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147414101)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147414101)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000253 RID: 595
		// (set) Token: 0x06000252 RID: 594
		[DispId(-2147414100)]
		float posRight
		{
			[TypeLibFunc(20)]
			[DispId(-2147414100)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147414100)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000255 RID: 597
		// (set) Token: 0x06000254 RID: 596
		[DispId(-2147412992)]
		string imeMode
		{
			[TypeLibFunc(20)]
			[DispId(-2147412992)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412992)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000257 RID: 599
		// (set) Token: 0x06000256 RID: 598
		[DispId(-2147412991)]
		string rubyAlign
		{
			[TypeLibFunc(20)]
			[DispId(-2147412991)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412991)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000259 RID: 601
		// (set) Token: 0x06000258 RID: 600
		[DispId(-2147412990)]
		string rubyPosition
		{
			[TypeLibFunc(20)]
			[DispId(-2147412990)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412990)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600025B RID: 603
		// (set) Token: 0x0600025A RID: 602
		[DispId(-2147412989)]
		string rubyOverhang
		{
			[TypeLibFunc(20)]
			[DispId(-2147412989)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412989)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600025D RID: 605
		// (set) Token: 0x0600025C RID: 604
		[DispId(-2147412985)]
		object layoutGridChar
		{
			[DispId(-2147412985)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412985)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600025F RID: 607
		// (set) Token: 0x0600025E RID: 606
		[DispId(-2147412984)]
		object layoutGridLine
		{
			[TypeLibFunc(20)]
			[DispId(-2147412984)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412984)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000261 RID: 609
		// (set) Token: 0x06000260 RID: 608
		[DispId(-2147412983)]
		string layoutGridMode
		{
			[DispId(-2147412983)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412983)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000263 RID: 611
		// (set) Token: 0x06000262 RID: 610
		[DispId(-2147412982)]
		string layoutGridType
		{
			[TypeLibFunc(20)]
			[DispId(-2147412982)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412982)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000265 RID: 613
		// (set) Token: 0x06000264 RID: 612
		[DispId(-2147412981)]
		string layoutGrid
		{
			[TypeLibFunc(1044)]
			[DispId(-2147412981)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147412981)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000267 RID: 615
		// (set) Token: 0x06000266 RID: 614
		[DispId(-2147412980)]
		string textAutospace
		{
			[TypeLibFunc(20)]
			[DispId(-2147412980)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412980)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000269 RID: 617
		// (set) Token: 0x06000268 RID: 616
		[DispId(-2147412978)]
		string wordBreak
		{
			[TypeLibFunc(20)]
			[DispId(-2147412978)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412978)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600026B RID: 619
		// (set) Token: 0x0600026A RID: 618
		[DispId(-2147412979)]
		string lineBreak
		{
			[TypeLibFunc(20)]
			[DispId(-2147412979)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412979)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600026D RID: 621
		// (set) Token: 0x0600026C RID: 620
		[DispId(-2147412977)]
		string textJustify
		{
			[TypeLibFunc(20)]
			[DispId(-2147412977)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412977)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600026F RID: 623
		// (set) Token: 0x0600026E RID: 622
		[DispId(-2147412976)]
		string textJustifyTrim
		{
			[DispId(-2147412976)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412976)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000271 RID: 625
		// (set) Token: 0x06000270 RID: 624
		[DispId(-2147412975)]
		object textKashida
		{
			[TypeLibFunc(20)]
			[DispId(-2147412975)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412975)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000273 RID: 627
		// (set) Token: 0x06000272 RID: 626
		[DispId(-2147412973)]
		string overflowX
		{
			[TypeLibFunc(20)]
			[DispId(-2147412973)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412973)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000275 RID: 629
		// (set) Token: 0x06000274 RID: 628
		[DispId(-2147412972)]
		string overflowY
		{
			[DispId(-2147412972)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412972)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000277 RID: 631
		// (set) Token: 0x06000276 RID: 630
		[DispId(-2147412965)]
		string accelerator
		{
			[DispId(-2147412965)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412965)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
