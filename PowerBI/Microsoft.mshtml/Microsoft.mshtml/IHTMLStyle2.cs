using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000049 RID: 73
	[TypeLibType(4160)]
	[Guid("3050F4A2-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLStyle2
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600013A RID: 314
		// (set) Token: 0x06000139 RID: 313
		[DispId(-2147413014)]
		string tableLayout
		{
			[TypeLibFunc(20)]
			[DispId(-2147413014)]
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

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600013C RID: 316
		// (set) Token: 0x0600013B RID: 315
		[DispId(-2147413028)]
		string borderCollapse
		{
			[DispId(-2147413028)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413028)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600013E RID: 318
		// (set) Token: 0x0600013D RID: 317
		[DispId(-2147412993)]
		string direction
		{
			[TypeLibFunc(20)]
			[DispId(-2147412993)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412993)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000140 RID: 320
		// (set) Token: 0x0600013F RID: 319
		[DispId(-2147412997)]
		string behavior
		{
			[TypeLibFunc(20)]
			[DispId(-2147412997)]
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

		// Token: 0x06000141 RID: 321
		[DispId(-2147417608)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06000142 RID: 322
		[DispId(-2147417607)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06000143 RID: 323
		[DispId(-2147417606)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000145 RID: 325
		// (set) Token: 0x06000144 RID: 324
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

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000147 RID: 327
		// (set) Token: 0x06000146 RID: 326
		[DispId(-2147412994)]
		string unicodeBidi
		{
			[TypeLibFunc(20)]
			[DispId(-2147412994)]
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

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000149 RID: 329
		// (set) Token: 0x06000148 RID: 328
		[DispId(-2147418034)]
		object bottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147418034)]
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

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600014B RID: 331
		// (set) Token: 0x0600014A RID: 330
		[DispId(-2147418035)]
		object right
		{
			[DispId(-2147418035)]
			[TypeLibFunc(20)]
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

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600014D RID: 333
		// (set) Token: 0x0600014C RID: 332
		[DispId(-2147414103)]
		int pixelBottom
		{
			[DispId(-2147414103)]
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147414103)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600014F RID: 335
		// (set) Token: 0x0600014E RID: 334
		[DispId(-2147414102)]
		int pixelRight
		{
			[TypeLibFunc(84)]
			[DispId(-2147414102)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147414102)]
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000151 RID: 337
		// (set) Token: 0x06000150 RID: 336
		[DispId(-2147414101)]
		float posBottom
		{
			[DispId(-2147414101)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147414101)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000153 RID: 339
		// (set) Token: 0x06000152 RID: 338
		[DispId(-2147414100)]
		float posRight
		{
			[DispId(-2147414100)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147414100)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000155 RID: 341
		// (set) Token: 0x06000154 RID: 340
		[DispId(-2147412992)]
		string imeMode
		{
			[DispId(-2147412992)]
			[TypeLibFunc(20)]
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

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000157 RID: 343
		// (set) Token: 0x06000156 RID: 342
		[DispId(-2147412991)]
		string rubyAlign
		{
			[DispId(-2147412991)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412991)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000159 RID: 345
		// (set) Token: 0x06000158 RID: 344
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

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600015B RID: 347
		// (set) Token: 0x0600015A RID: 346
		[DispId(-2147412989)]
		string rubyOverhang
		{
			[DispId(-2147412989)]
			[TypeLibFunc(20)]
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600015D RID: 349
		// (set) Token: 0x0600015C RID: 348
		[DispId(-2147412985)]
		object layoutGridChar
		{
			[TypeLibFunc(20)]
			[DispId(-2147412985)]
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600015F RID: 351
		// (set) Token: 0x0600015E RID: 350
		[DispId(-2147412984)]
		object layoutGridLine
		{
			[TypeLibFunc(20)]
			[DispId(-2147412984)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412984)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000161 RID: 353
		// (set) Token: 0x06000160 RID: 352
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

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000163 RID: 355
		// (set) Token: 0x06000162 RID: 354
		[DispId(-2147412982)]
		string layoutGridType
		{
			[DispId(-2147412982)]
			[TypeLibFunc(20)]
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

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000165 RID: 357
		// (set) Token: 0x06000164 RID: 356
		[DispId(-2147412981)]
		string layoutGrid
		{
			[TypeLibFunc(1044)]
			[DispId(-2147412981)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412981)]
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000167 RID: 359
		// (set) Token: 0x06000166 RID: 358
		[DispId(-2147412978)]
		string wordBreak
		{
			[TypeLibFunc(20)]
			[DispId(-2147412978)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412978)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000169 RID: 361
		// (set) Token: 0x06000168 RID: 360
		[DispId(-2147412979)]
		string lineBreak
		{
			[TypeLibFunc(20)]
			[DispId(-2147412979)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412979)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600016B RID: 363
		// (set) Token: 0x0600016A RID: 362
		[DispId(-2147412977)]
		string textJustify
		{
			[DispId(-2147412977)]
			[TypeLibFunc(20)]
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

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600016D RID: 365
		// (set) Token: 0x0600016C RID: 364
		[DispId(-2147412976)]
		string textJustifyTrim
		{
			[TypeLibFunc(20)]
			[DispId(-2147412976)]
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

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600016F RID: 367
		// (set) Token: 0x0600016E RID: 366
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

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000171 RID: 369
		// (set) Token: 0x06000170 RID: 368
		[DispId(-2147412980)]
		string textAutospace
		{
			[TypeLibFunc(20)]
			[DispId(-2147412980)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412980)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000173 RID: 371
		// (set) Token: 0x06000172 RID: 370
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

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000175 RID: 373
		// (set) Token: 0x06000174 RID: 372
		[DispId(-2147412972)]
		string overflowY
		{
			[TypeLibFunc(20)]
			[DispId(-2147412972)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412972)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000177 RID: 375
		// (set) Token: 0x06000176 RID: 374
		[DispId(-2147412965)]
		string accelerator
		{
			[TypeLibFunc(20)]
			[DispId(-2147412965)]
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
