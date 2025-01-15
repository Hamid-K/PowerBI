using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200004C RID: 76
	[Guid("3050F3CF-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLRuleStyle
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600019B RID: 411
		// (set) Token: 0x0600019A RID: 410
		[DispId(-2147413094)]
		string fontFamily
		{
			[DispId(-2147413094)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413094)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600019D RID: 413
		// (set) Token: 0x0600019C RID: 412
		[DispId(-2147413088)]
		string fontStyle
		{
			[DispId(-2147413088)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413088)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600019F RID: 415
		// (set) Token: 0x0600019E RID: 414
		[DispId(-2147413087)]
		string fontVariant
		{
			[TypeLibFunc(20)]
			[DispId(-2147413087)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413087)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001A1 RID: 417
		// (set) Token: 0x060001A0 RID: 416
		[DispId(-2147413085)]
		string fontWeight
		{
			[DispId(-2147413085)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413085)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001A3 RID: 419
		// (set) Token: 0x060001A2 RID: 418
		[DispId(-2147413093)]
		object fontSize
		{
			[TypeLibFunc(20)]
			[DispId(-2147413093)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413093)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001A5 RID: 421
		// (set) Token: 0x060001A4 RID: 420
		[DispId(-2147413071)]
		string font
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413071)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413071)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001A7 RID: 423
		// (set) Token: 0x060001A6 RID: 422
		[DispId(-2147413110)]
		object color
		{
			[TypeLibFunc(20)]
			[DispId(-2147413110)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413110)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001A9 RID: 425
		// (set) Token: 0x060001A8 RID: 424
		[DispId(-2147413080)]
		string background
		{
			[DispId(-2147413080)]
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413080)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060001AB RID: 427
		// (set) Token: 0x060001AA RID: 426
		[DispId(-501)]
		object backgroundColor
		{
			[DispId(-501)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-501)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060001AD RID: 429
		// (set) Token: 0x060001AC RID: 428
		[DispId(-2147413111)]
		string backgroundImage
		{
			[DispId(-2147413111)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413111)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060001AF RID: 431
		// (set) Token: 0x060001AE RID: 430
		[DispId(-2147413068)]
		string backgroundRepeat
		{
			[TypeLibFunc(20)]
			[DispId(-2147413068)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413068)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001B1 RID: 433
		// (set) Token: 0x060001B0 RID: 432
		[DispId(-2147413067)]
		string backgroundAttachment
		{
			[TypeLibFunc(20)]
			[DispId(-2147413067)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413067)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060001B3 RID: 435
		// (set) Token: 0x060001B2 RID: 434
		[DispId(-2147413066)]
		string backgroundPosition
		{
			[DispId(-2147413066)]
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413066)]
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060001B5 RID: 437
		// (set) Token: 0x060001B4 RID: 436
		[DispId(-2147413079)]
		object backgroundPositionX
		{
			[DispId(-2147413079)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413079)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060001B7 RID: 439
		// (set) Token: 0x060001B6 RID: 438
		[DispId(-2147413078)]
		object backgroundPositionY
		{
			[TypeLibFunc(20)]
			[DispId(-2147413078)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413078)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060001B9 RID: 441
		// (set) Token: 0x060001B8 RID: 440
		[DispId(-2147413065)]
		object wordSpacing
		{
			[DispId(-2147413065)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413065)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060001BB RID: 443
		// (set) Token: 0x060001BA RID: 442
		[DispId(-2147413104)]
		object letterSpacing
		{
			[DispId(-2147413104)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413104)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060001BD RID: 445
		// (set) Token: 0x060001BC RID: 444
		[DispId(-2147413077)]
		string textDecoration
		{
			[TypeLibFunc(20)]
			[DispId(-2147413077)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413077)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060001BF RID: 447
		// (set) Token: 0x060001BE RID: 446
		[DispId(-2147413089)]
		bool textDecorationNone
		{
			[DispId(-2147413089)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060001C1 RID: 449
		// (set) Token: 0x060001C0 RID: 448
		[DispId(-2147413091)]
		bool textDecorationUnderline
		{
			[DispId(-2147413091)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413091)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060001C3 RID: 451
		// (set) Token: 0x060001C2 RID: 450
		[DispId(-2147413043)]
		bool textDecorationOverline
		{
			[TypeLibFunc(20)]
			[DispId(-2147413043)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147413043)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060001C5 RID: 453
		// (set) Token: 0x060001C4 RID: 452
		[DispId(-2147413092)]
		bool textDecorationLineThrough
		{
			[TypeLibFunc(20)]
			[DispId(-2147413092)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413092)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060001C7 RID: 455
		// (set) Token: 0x060001C6 RID: 454
		[DispId(-2147413090)]
		bool textDecorationBlink
		{
			[DispId(-2147413090)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147413090)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060001C9 RID: 457
		// (set) Token: 0x060001C8 RID: 456
		[DispId(-2147413064)]
		object verticalAlign
		{
			[DispId(-2147413064)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413064)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060001CB RID: 459
		// (set) Token: 0x060001CA RID: 458
		[DispId(-2147413108)]
		string textTransform
		{
			[DispId(-2147413108)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060001CD RID: 461
		// (set) Token: 0x060001CC RID: 460
		[DispId(-2147418040)]
		string textAlign
		{
			[DispId(-2147418040)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418040)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060001CF RID: 463
		// (set) Token: 0x060001CE RID: 462
		[DispId(-2147413105)]
		object textIndent
		{
			[DispId(-2147413105)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413105)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060001D1 RID: 465
		// (set) Token: 0x060001D0 RID: 464
		[DispId(-2147413106)]
		object lineHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060001D3 RID: 467
		// (set) Token: 0x060001D2 RID: 466
		[DispId(-2147413075)]
		object marginTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413075)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413075)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060001D5 RID: 469
		// (set) Token: 0x060001D4 RID: 468
		[DispId(-2147413074)]
		object marginRight
		{
			[DispId(-2147413074)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413074)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060001D7 RID: 471
		// (set) Token: 0x060001D6 RID: 470
		[DispId(-2147413073)]
		object marginBottom
		{
			[DispId(-2147413073)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413073)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060001D9 RID: 473
		// (set) Token: 0x060001D8 RID: 472
		[DispId(-2147413072)]
		object marginLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147413072)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413072)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060001DB RID: 475
		// (set) Token: 0x060001DA RID: 474
		[DispId(-2147413076)]
		string margin
		{
			[DispId(-2147413076)]
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413076)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060001DD RID: 477
		// (set) Token: 0x060001DC RID: 476
		[DispId(-2147413100)]
		object paddingTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413100)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413100)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060001DF RID: 479
		// (set) Token: 0x060001DE RID: 478
		[DispId(-2147413099)]
		object paddingRight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413099)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413099)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060001E1 RID: 481
		// (set) Token: 0x060001E0 RID: 480
		[DispId(-2147413098)]
		object paddingBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147413098)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413098)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060001E3 RID: 483
		// (set) Token: 0x060001E2 RID: 482
		[DispId(-2147413097)]
		object paddingLeft
		{
			[DispId(-2147413097)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413097)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060001E5 RID: 485
		// (set) Token: 0x060001E4 RID: 484
		[DispId(-2147413101)]
		string padding
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413101)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413101)]
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060001E7 RID: 487
		// (set) Token: 0x060001E6 RID: 486
		[DispId(-2147413063)]
		string border
		{
			[DispId(-2147413063)]
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413063)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060001E9 RID: 489
		// (set) Token: 0x060001E8 RID: 488
		[DispId(-2147413062)]
		string borderTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413062)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413062)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060001EB RID: 491
		// (set) Token: 0x060001EA RID: 490
		[DispId(-2147413061)]
		string borderRight
		{
			[DispId(-2147413061)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413061)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060001ED RID: 493
		// (set) Token: 0x060001EC RID: 492
		[DispId(-2147413060)]
		string borderBottom
		{
			[DispId(-2147413060)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413060)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060001EF RID: 495
		// (set) Token: 0x060001EE RID: 494
		[DispId(-2147413059)]
		string borderLeft
		{
			[DispId(-2147413059)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413059)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060001F1 RID: 497
		// (set) Token: 0x060001F0 RID: 496
		[DispId(-2147413058)]
		string borderColor
		{
			[DispId(-2147413058)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413058)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060001F3 RID: 499
		// (set) Token: 0x060001F2 RID: 498
		[DispId(-2147413057)]
		object borderTopColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413057)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413057)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060001F5 RID: 501
		// (set) Token: 0x060001F4 RID: 500
		[DispId(-2147413056)]
		object borderRightColor
		{
			[DispId(-2147413056)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413056)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060001F7 RID: 503
		// (set) Token: 0x060001F6 RID: 502
		[DispId(-2147413055)]
		object borderBottomColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413055)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413055)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060001F9 RID: 505
		// (set) Token: 0x060001F8 RID: 504
		[DispId(-2147413054)]
		object borderLeftColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413054)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413054)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060001FB RID: 507
		// (set) Token: 0x060001FA RID: 506
		[DispId(-2147413053)]
		string borderWidth
		{
			[DispId(-2147413053)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413053)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060001FD RID: 509
		// (set) Token: 0x060001FC RID: 508
		[DispId(-2147413052)]
		object borderTopWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413052)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413052)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060001FF RID: 511
		// (set) Token: 0x060001FE RID: 510
		[DispId(-2147413051)]
		object borderRightWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413051)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413051)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000201 RID: 513
		// (set) Token: 0x06000200 RID: 512
		[DispId(-2147413050)]
		object borderBottomWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413050)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413050)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000203 RID: 515
		// (set) Token: 0x06000202 RID: 514
		[DispId(-2147413049)]
		object borderLeftWidth
		{
			[DispId(-2147413049)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413049)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000205 RID: 517
		// (set) Token: 0x06000204 RID: 516
		[DispId(-2147413048)]
		string borderStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413048)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413048)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000207 RID: 519
		// (set) Token: 0x06000206 RID: 518
		[DispId(-2147413047)]
		string borderTopStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413047)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413047)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000209 RID: 521
		// (set) Token: 0x06000208 RID: 520
		[DispId(-2147413046)]
		string borderRightStyle
		{
			[DispId(-2147413046)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413046)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600020B RID: 523
		// (set) Token: 0x0600020A RID: 522
		[DispId(-2147413045)]
		string borderBottomStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413045)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413045)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600020D RID: 525
		// (set) Token: 0x0600020C RID: 524
		[DispId(-2147413044)]
		string borderLeftStyle
		{
			[DispId(-2147413044)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600020F RID: 527
		// (set) Token: 0x0600020E RID: 526
		[DispId(-2147418107)]
		object width
		{
			[TypeLibFunc(20)]
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000211 RID: 529
		// (set) Token: 0x06000210 RID: 528
		[DispId(-2147418106)]
		object height
		{
			[DispId(-2147418106)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000213 RID: 531
		// (set) Token: 0x06000212 RID: 530
		[DispId(-2147413042)]
		string styleFloat
		{
			[DispId(-2147413042)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413042)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000215 RID: 533
		// (set) Token: 0x06000214 RID: 532
		[DispId(-2147413096)]
		string clear
		{
			[DispId(-2147413096)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413096)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000217 RID: 535
		// (set) Token: 0x06000216 RID: 534
		[DispId(-2147413041)]
		string display
		{
			[TypeLibFunc(20)]
			[DispId(-2147413041)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413041)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000219 RID: 537
		// (set) Token: 0x06000218 RID: 536
		[DispId(-2147413032)]
		string visibility
		{
			[DispId(-2147413032)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413032)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600021B RID: 539
		// (set) Token: 0x0600021A RID: 538
		[DispId(-2147413040)]
		string listStyleType
		{
			[DispId(-2147413040)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413040)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600021D RID: 541
		// (set) Token: 0x0600021C RID: 540
		[DispId(-2147413039)]
		string listStylePosition
		{
			[TypeLibFunc(20)]
			[DispId(-2147413039)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413039)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600021F RID: 543
		// (set) Token: 0x0600021E RID: 542
		[DispId(-2147413038)]
		string listStyleImage
		{
			[DispId(-2147413038)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413038)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000221 RID: 545
		// (set) Token: 0x06000220 RID: 544
		[DispId(-2147413037)]
		string listStyle
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413037)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413037)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000223 RID: 547
		// (set) Token: 0x06000222 RID: 546
		[DispId(-2147413036)]
		string whiteSpace
		{
			[TypeLibFunc(20)]
			[DispId(-2147413036)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413036)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000225 RID: 549
		// (set) Token: 0x06000224 RID: 548
		[DispId(-2147418108)]
		object top
		{
			[TypeLibFunc(20)]
			[DispId(-2147418108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418108)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000227 RID: 551
		// (set) Token: 0x06000226 RID: 550
		[DispId(-2147418109)]
		object left
		{
			[TypeLibFunc(20)]
			[DispId(-2147418109)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418109)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000228 RID: 552
		[DispId(-2147413022)]
		string position
		{
			[TypeLibFunc(20)]
			[DispId(-2147413022)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600022A RID: 554
		// (set) Token: 0x06000229 RID: 553
		[DispId(-2147413021)]
		object zIndex
		{
			[DispId(-2147413021)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413021)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600022C RID: 556
		// (set) Token: 0x0600022B RID: 555
		[DispId(-2147413102)]
		string overflow
		{
			[TypeLibFunc(20)]
			[DispId(-2147413102)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413102)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600022E RID: 558
		// (set) Token: 0x0600022D RID: 557
		[DispId(-2147413035)]
		string pageBreakBefore
		{
			[TypeLibFunc(20)]
			[DispId(-2147413035)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413035)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000230 RID: 560
		// (set) Token: 0x0600022F RID: 559
		[DispId(-2147413034)]
		string pageBreakAfter
		{
			[TypeLibFunc(20)]
			[DispId(-2147413034)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413034)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000232 RID: 562
		// (set) Token: 0x06000231 RID: 561
		[DispId(-2147413013)]
		string cssText
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413013)]
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000234 RID: 564
		// (set) Token: 0x06000233 RID: 563
		[DispId(-2147413010)]
		string cursor
		{
			[DispId(-2147413010)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000236 RID: 566
		// (set) Token: 0x06000235 RID: 565
		[DispId(-2147413020)]
		string clip
		{
			[DispId(-2147413020)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413020)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000238 RID: 568
		// (set) Token: 0x06000237 RID: 567
		[DispId(-2147413030)]
		string filter
		{
			[TypeLibFunc(20)]
			[DispId(-2147413030)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413030)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x06000239 RID: 569
		[DispId(-2147417611)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600023A RID: 570
		[DispId(-2147417610)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600023B RID: 571
		[DispId(-2147417609)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);
	}
}
