using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Html;
using AngleSharp.Dom.Navigator;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Services.Styling;

namespace AngleSharp.Dom
{
	// Token: 0x02000164 RID: 356
	internal sealed class Window : EventTarget, IWindow, IEventTarget, IGlobalEventHandlers, IWindowEventHandlers, IWindowTimers
	{
		// Token: 0x06000CDE RID: 3294 RVA: 0x000461AF File Offset: 0x000443AF
		public Window(Document document)
		{
			this._document = document;
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x000461BE File Offset: 0x000443BE
		public IWindow Proxy
		{
			get
			{
				return this._document.Context.Current;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x000461D0 File Offset: 0x000443D0
		public INavigator Navigator
		{
			get
			{
				INavigator navigator;
				if ((navigator = this._navigator) == null)
				{
					navigator = (this._navigator = this._document.Context.CreateService<INavigator>());
				}
				return navigator;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00046200 File Offset: 0x00044400
		public IDocument Document
		{
			get
			{
				return this._document;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00046208 File Offset: 0x00044408
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x00046210 File Offset: 0x00044410
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00046219 File Offset: 0x00044419
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x00046221 File Offset: 0x00044421
		public int OuterHeight
		{
			get
			{
				return this._outerHeight;
			}
			set
			{
				this._outerHeight = value;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0004622A File Offset: 0x0004442A
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x00046232 File Offset: 0x00044432
		public int OuterWidth
		{
			get
			{
				return this._outerWidth;
			}
			set
			{
				this._outerWidth = value;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0004623B File Offset: 0x0004443B
		// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x00046243 File Offset: 0x00044443
		public int ScreenX
		{
			get
			{
				return this._screenX;
			}
			set
			{
				this._screenX = value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0004624C File Offset: 0x0004444C
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x00046254 File Offset: 0x00044454
		public int ScreenY
		{
			get
			{
				return this._screenY;
			}
			set
			{
				this._screenY = value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0004625D File Offset: 0x0004445D
		public ILocation Location
		{
			get
			{
				return this.Document.Location;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x0004626A File Offset: 0x0004446A
		// (set) Token: 0x06000CEE RID: 3310 RVA: 0x00046272 File Offset: 0x00044472
		public string Status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x0004627B File Offset: 0x0004447B
		public bool IsClosed
		{
			get
			{
				return this._closed;
			}
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00046284 File Offset: 0x00044484
		public IMediaQueryList MatchMedia(string mediaText)
		{
			IConfiguration options = this._document.Options;
			StyleOptions styleOptions = new StyleOptions(this._document.Context);
			IMediaList mediaList = options.GetCssStyleEngine().ParseMedia(mediaText, styleOptions);
			return new CssMediaQueryList(this, mediaList);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x000462C1 File Offset: 0x000444C1
		public ICssStyleDeclaration GetComputedStyle(IElement element, string pseudo = null)
		{
			return this.GetStyleCollection().ComputeDeclarations(element, pseudo);
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06000CF2 RID: 3314 RVA: 0x00040D19 File Offset: 0x0003EF19
		// (remove) Token: 0x06000CF3 RID: 3315 RVA: 0x00040D28 File Offset: 0x0003EF28
		event DomEventHandler IGlobalEventHandlers.Aborted
		{
			add
			{
				base.AddEventListener(EventNames.Abort, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Abort, value, false);
			}
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06000CF4 RID: 3316 RVA: 0x00040D37 File Offset: 0x0003EF37
		// (remove) Token: 0x06000CF5 RID: 3317 RVA: 0x00040D46 File Offset: 0x0003EF46
		event DomEventHandler IGlobalEventHandlers.Blurred
		{
			add
			{
				base.AddEventListener(EventNames.Blur, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Blur, value, false);
			}
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06000CF6 RID: 3318 RVA: 0x00040D55 File Offset: 0x0003EF55
		// (remove) Token: 0x06000CF7 RID: 3319 RVA: 0x00040D64 File Offset: 0x0003EF64
		event DomEventHandler IGlobalEventHandlers.Cancelled
		{
			add
			{
				base.AddEventListener(EventNames.Cancel, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Cancel, value, false);
			}
		}

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06000CF8 RID: 3320 RVA: 0x00040D73 File Offset: 0x0003EF73
		// (remove) Token: 0x06000CF9 RID: 3321 RVA: 0x00040D82 File Offset: 0x0003EF82
		event DomEventHandler IGlobalEventHandlers.CanPlay
		{
			add
			{
				base.AddEventListener(EventNames.CanPlay, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.CanPlay, value, false);
			}
		}

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06000CFA RID: 3322 RVA: 0x00040D91 File Offset: 0x0003EF91
		// (remove) Token: 0x06000CFB RID: 3323 RVA: 0x00040DA0 File Offset: 0x0003EFA0
		event DomEventHandler IGlobalEventHandlers.CanPlayThrough
		{
			add
			{
				base.AddEventListener(EventNames.CanPlayThrough, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.CanPlayThrough, value, false);
			}
		}

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06000CFC RID: 3324 RVA: 0x00040DAF File Offset: 0x0003EFAF
		// (remove) Token: 0x06000CFD RID: 3325 RVA: 0x00040DBE File Offset: 0x0003EFBE
		event DomEventHandler IGlobalEventHandlers.Changed
		{
			add
			{
				base.AddEventListener(EventNames.Change, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Change, value, false);
			}
		}

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06000CFE RID: 3326 RVA: 0x00040DCD File Offset: 0x0003EFCD
		// (remove) Token: 0x06000CFF RID: 3327 RVA: 0x00040DDC File Offset: 0x0003EFDC
		event DomEventHandler IGlobalEventHandlers.Clicked
		{
			add
			{
				base.AddEventListener(EventNames.Click, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Click, value, false);
			}
		}

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06000D00 RID: 3328 RVA: 0x00040DEB File Offset: 0x0003EFEB
		// (remove) Token: 0x06000D01 RID: 3329 RVA: 0x00040DFA File Offset: 0x0003EFFA
		event DomEventHandler IGlobalEventHandlers.CueChanged
		{
			add
			{
				base.AddEventListener(EventNames.CueChange, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.CueChange, value, false);
			}
		}

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06000D02 RID: 3330 RVA: 0x00040E09 File Offset: 0x0003F009
		// (remove) Token: 0x06000D03 RID: 3331 RVA: 0x00040E18 File Offset: 0x0003F018
		event DomEventHandler IGlobalEventHandlers.DoubleClick
		{
			add
			{
				base.AddEventListener(EventNames.DblClick, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.DblClick, value, false);
			}
		}

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06000D04 RID: 3332 RVA: 0x00040E27 File Offset: 0x0003F027
		// (remove) Token: 0x06000D05 RID: 3333 RVA: 0x00040E36 File Offset: 0x0003F036
		event DomEventHandler IGlobalEventHandlers.Drag
		{
			add
			{
				base.AddEventListener(EventNames.Drag, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Drag, value, false);
			}
		}

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06000D06 RID: 3334 RVA: 0x00040E45 File Offset: 0x0003F045
		// (remove) Token: 0x06000D07 RID: 3335 RVA: 0x00040E54 File Offset: 0x0003F054
		event DomEventHandler IGlobalEventHandlers.DragEnd
		{
			add
			{
				base.AddEventListener(EventNames.DragEnd, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.DragEnd, value, false);
			}
		}

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06000D08 RID: 3336 RVA: 0x00040E63 File Offset: 0x0003F063
		// (remove) Token: 0x06000D09 RID: 3337 RVA: 0x00040E72 File Offset: 0x0003F072
		event DomEventHandler IGlobalEventHandlers.DragEnter
		{
			add
			{
				base.AddEventListener(EventNames.DragEnter, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.DragEnter, value, false);
			}
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06000D0A RID: 3338 RVA: 0x00040E81 File Offset: 0x0003F081
		// (remove) Token: 0x06000D0B RID: 3339 RVA: 0x00040E90 File Offset: 0x0003F090
		event DomEventHandler IGlobalEventHandlers.DragExit
		{
			add
			{
				base.AddEventListener(EventNames.DragExit, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.DragExit, value, false);
			}
		}

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06000D0C RID: 3340 RVA: 0x00040E9F File Offset: 0x0003F09F
		// (remove) Token: 0x06000D0D RID: 3341 RVA: 0x00040EAE File Offset: 0x0003F0AE
		event DomEventHandler IGlobalEventHandlers.DragLeave
		{
			add
			{
				base.AddEventListener(EventNames.DragLeave, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.DragLeave, value, false);
			}
		}

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06000D0E RID: 3342 RVA: 0x00040EBD File Offset: 0x0003F0BD
		// (remove) Token: 0x06000D0F RID: 3343 RVA: 0x00040ECC File Offset: 0x0003F0CC
		event DomEventHandler IGlobalEventHandlers.DragOver
		{
			add
			{
				base.AddEventListener(EventNames.DragOver, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.DragOver, value, false);
			}
		}

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06000D10 RID: 3344 RVA: 0x00040EDB File Offset: 0x0003F0DB
		// (remove) Token: 0x06000D11 RID: 3345 RVA: 0x00040EEA File Offset: 0x0003F0EA
		event DomEventHandler IGlobalEventHandlers.DragStart
		{
			add
			{
				base.AddEventListener(EventNames.DragStart, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.DragStart, value, false);
			}
		}

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06000D12 RID: 3346 RVA: 0x00040EF9 File Offset: 0x0003F0F9
		// (remove) Token: 0x06000D13 RID: 3347 RVA: 0x00040F08 File Offset: 0x0003F108
		event DomEventHandler IGlobalEventHandlers.Dropped
		{
			add
			{
				base.AddEventListener(EventNames.Drop, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Drop, value, false);
			}
		}

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06000D14 RID: 3348 RVA: 0x00040F17 File Offset: 0x0003F117
		// (remove) Token: 0x06000D15 RID: 3349 RVA: 0x00040F26 File Offset: 0x0003F126
		event DomEventHandler IGlobalEventHandlers.DurationChanged
		{
			add
			{
				base.AddEventListener(EventNames.DurationChange, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.DurationChange, value, false);
			}
		}

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06000D16 RID: 3350 RVA: 0x00040F35 File Offset: 0x0003F135
		// (remove) Token: 0x06000D17 RID: 3351 RVA: 0x00040F44 File Offset: 0x0003F144
		event DomEventHandler IGlobalEventHandlers.Emptied
		{
			add
			{
				base.AddEventListener(EventNames.Emptied, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Emptied, value, false);
			}
		}

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06000D18 RID: 3352 RVA: 0x00040F53 File Offset: 0x0003F153
		// (remove) Token: 0x06000D19 RID: 3353 RVA: 0x00040F62 File Offset: 0x0003F162
		event DomEventHandler IGlobalEventHandlers.Ended
		{
			add
			{
				base.AddEventListener(EventNames.Ended, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Ended, value, false);
			}
		}

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06000D1A RID: 3354 RVA: 0x00040F71 File Offset: 0x0003F171
		// (remove) Token: 0x06000D1B RID: 3355 RVA: 0x00040F80 File Offset: 0x0003F180
		event DomEventHandler IGlobalEventHandlers.Error
		{
			add
			{
				base.AddEventListener(EventNames.Error, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Error, value, false);
			}
		}

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06000D1C RID: 3356 RVA: 0x00040F8F File Offset: 0x0003F18F
		// (remove) Token: 0x06000D1D RID: 3357 RVA: 0x00040F9E File Offset: 0x0003F19E
		event DomEventHandler IGlobalEventHandlers.Focused
		{
			add
			{
				base.AddEventListener(EventNames.Focus, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Focus, value, false);
			}
		}

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06000D1E RID: 3358 RVA: 0x00040FAD File Offset: 0x0003F1AD
		// (remove) Token: 0x06000D1F RID: 3359 RVA: 0x00040FBC File Offset: 0x0003F1BC
		event DomEventHandler IGlobalEventHandlers.Input
		{
			add
			{
				base.AddEventListener(EventNames.Input, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Input, value, false);
			}
		}

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06000D20 RID: 3360 RVA: 0x00040FCB File Offset: 0x0003F1CB
		// (remove) Token: 0x06000D21 RID: 3361 RVA: 0x00040FDA File Offset: 0x0003F1DA
		event DomEventHandler IGlobalEventHandlers.Invalid
		{
			add
			{
				base.AddEventListener(EventNames.Invalid, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Invalid, value, false);
			}
		}

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x06000D22 RID: 3362 RVA: 0x00040FE9 File Offset: 0x0003F1E9
		// (remove) Token: 0x06000D23 RID: 3363 RVA: 0x00040FF8 File Offset: 0x0003F1F8
		event DomEventHandler IGlobalEventHandlers.KeyDown
		{
			add
			{
				base.AddEventListener(EventNames.Keydown, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Keydown, value, false);
			}
		}

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06000D24 RID: 3364 RVA: 0x00041007 File Offset: 0x0003F207
		// (remove) Token: 0x06000D25 RID: 3365 RVA: 0x00041016 File Offset: 0x0003F216
		event DomEventHandler IGlobalEventHandlers.KeyPress
		{
			add
			{
				base.AddEventListener(EventNames.Keypress, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Keypress, value, false);
			}
		}

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06000D26 RID: 3366 RVA: 0x00041025 File Offset: 0x0003F225
		// (remove) Token: 0x06000D27 RID: 3367 RVA: 0x00041034 File Offset: 0x0003F234
		event DomEventHandler IGlobalEventHandlers.KeyUp
		{
			add
			{
				base.AddEventListener(EventNames.Keyup, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Keyup, value, false);
			}
		}

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06000D28 RID: 3368 RVA: 0x00041043 File Offset: 0x0003F243
		// (remove) Token: 0x06000D29 RID: 3369 RVA: 0x00041052 File Offset: 0x0003F252
		event DomEventHandler IGlobalEventHandlers.Loaded
		{
			add
			{
				base.AddEventListener(EventNames.Load, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Load, value, false);
			}
		}

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06000D2A RID: 3370 RVA: 0x00041061 File Offset: 0x0003F261
		// (remove) Token: 0x06000D2B RID: 3371 RVA: 0x00041070 File Offset: 0x0003F270
		event DomEventHandler IGlobalEventHandlers.LoadedData
		{
			add
			{
				base.AddEventListener(EventNames.LoadedData, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.LoadedData, value, false);
			}
		}

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06000D2C RID: 3372 RVA: 0x0004107F File Offset: 0x0003F27F
		// (remove) Token: 0x06000D2D RID: 3373 RVA: 0x0004108E File Offset: 0x0003F28E
		event DomEventHandler IGlobalEventHandlers.LoadedMetadata
		{
			add
			{
				base.AddEventListener(EventNames.LoadedMetaData, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.LoadedMetaData, value, false);
			}
		}

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06000D2E RID: 3374 RVA: 0x0004109D File Offset: 0x0003F29D
		// (remove) Token: 0x06000D2F RID: 3375 RVA: 0x000410AC File Offset: 0x0003F2AC
		event DomEventHandler IGlobalEventHandlers.Loading
		{
			add
			{
				base.AddEventListener(EventNames.LoadStart, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.LoadStart, value, false);
			}
		}

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x06000D30 RID: 3376 RVA: 0x000410BB File Offset: 0x0003F2BB
		// (remove) Token: 0x06000D31 RID: 3377 RVA: 0x000410CA File Offset: 0x0003F2CA
		event DomEventHandler IGlobalEventHandlers.MouseDown
		{
			add
			{
				base.AddEventListener(EventNames.Mousedown, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Mousedown, value, false);
			}
		}

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06000D32 RID: 3378 RVA: 0x000410D9 File Offset: 0x0003F2D9
		// (remove) Token: 0x06000D33 RID: 3379 RVA: 0x000410E8 File Offset: 0x0003F2E8
		event DomEventHandler IGlobalEventHandlers.MouseEnter
		{
			add
			{
				base.AddEventListener(EventNames.Mouseenter, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Mouseenter, value, false);
			}
		}

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06000D34 RID: 3380 RVA: 0x000410F7 File Offset: 0x0003F2F7
		// (remove) Token: 0x06000D35 RID: 3381 RVA: 0x00041106 File Offset: 0x0003F306
		event DomEventHandler IGlobalEventHandlers.MouseLeave
		{
			add
			{
				base.AddEventListener(EventNames.Mouseleave, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Mouseleave, value, false);
			}
		}

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x06000D36 RID: 3382 RVA: 0x00041115 File Offset: 0x0003F315
		// (remove) Token: 0x06000D37 RID: 3383 RVA: 0x00041124 File Offset: 0x0003F324
		event DomEventHandler IGlobalEventHandlers.MouseMove
		{
			add
			{
				base.AddEventListener(EventNames.Mousemove, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Mousemove, value, false);
			}
		}

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x06000D38 RID: 3384 RVA: 0x00041133 File Offset: 0x0003F333
		// (remove) Token: 0x06000D39 RID: 3385 RVA: 0x00041142 File Offset: 0x0003F342
		event DomEventHandler IGlobalEventHandlers.MouseOut
		{
			add
			{
				base.AddEventListener(EventNames.Mouseout, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Mouseout, value, false);
			}
		}

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06000D3A RID: 3386 RVA: 0x00041151 File Offset: 0x0003F351
		// (remove) Token: 0x06000D3B RID: 3387 RVA: 0x00041160 File Offset: 0x0003F360
		event DomEventHandler IGlobalEventHandlers.MouseOver
		{
			add
			{
				base.AddEventListener(EventNames.Mouseover, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Mouseover, value, false);
			}
		}

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x06000D3C RID: 3388 RVA: 0x0004116F File Offset: 0x0003F36F
		// (remove) Token: 0x06000D3D RID: 3389 RVA: 0x0004117E File Offset: 0x0003F37E
		event DomEventHandler IGlobalEventHandlers.MouseUp
		{
			add
			{
				base.AddEventListener(EventNames.Mouseup, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Mouseup, value, false);
			}
		}

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06000D3E RID: 3390 RVA: 0x0004118D File Offset: 0x0003F38D
		// (remove) Token: 0x06000D3F RID: 3391 RVA: 0x0004119C File Offset: 0x0003F39C
		event DomEventHandler IGlobalEventHandlers.MouseWheel
		{
			add
			{
				base.AddEventListener(EventNames.Wheel, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Wheel, value, false);
			}
		}

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x06000D40 RID: 3392 RVA: 0x000411AB File Offset: 0x0003F3AB
		// (remove) Token: 0x06000D41 RID: 3393 RVA: 0x000411BA File Offset: 0x0003F3BA
		event DomEventHandler IGlobalEventHandlers.Paused
		{
			add
			{
				base.AddEventListener(EventNames.Pause, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Pause, value, false);
			}
		}

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x06000D42 RID: 3394 RVA: 0x000411C9 File Offset: 0x0003F3C9
		// (remove) Token: 0x06000D43 RID: 3395 RVA: 0x000411D8 File Offset: 0x0003F3D8
		event DomEventHandler IGlobalEventHandlers.Played
		{
			add
			{
				base.AddEventListener(EventNames.Play, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Play, value, false);
			}
		}

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x06000D44 RID: 3396 RVA: 0x000411E7 File Offset: 0x0003F3E7
		// (remove) Token: 0x06000D45 RID: 3397 RVA: 0x000411F6 File Offset: 0x0003F3F6
		event DomEventHandler IGlobalEventHandlers.Playing
		{
			add
			{
				base.AddEventListener(EventNames.Playing, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Playing, value, false);
			}
		}

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x06000D46 RID: 3398 RVA: 0x00041205 File Offset: 0x0003F405
		// (remove) Token: 0x06000D47 RID: 3399 RVA: 0x00041214 File Offset: 0x0003F414
		event DomEventHandler IGlobalEventHandlers.Progress
		{
			add
			{
				base.AddEventListener(EventNames.Progress, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Progress, value, false);
			}
		}

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x06000D48 RID: 3400 RVA: 0x00041223 File Offset: 0x0003F423
		// (remove) Token: 0x06000D49 RID: 3401 RVA: 0x00041232 File Offset: 0x0003F432
		event DomEventHandler IGlobalEventHandlers.RateChanged
		{
			add
			{
				base.AddEventListener(EventNames.RateChange, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.RateChange, value, false);
			}
		}

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x06000D4A RID: 3402 RVA: 0x00041241 File Offset: 0x0003F441
		// (remove) Token: 0x06000D4B RID: 3403 RVA: 0x00041250 File Offset: 0x0003F450
		event DomEventHandler IGlobalEventHandlers.Resetted
		{
			add
			{
				base.AddEventListener(EventNames.Reset, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Reset, value, false);
			}
		}

		// Token: 0x14000076 RID: 118
		// (add) Token: 0x06000D4C RID: 3404 RVA: 0x0004125F File Offset: 0x0003F45F
		// (remove) Token: 0x06000D4D RID: 3405 RVA: 0x0004126E File Offset: 0x0003F46E
		event DomEventHandler IGlobalEventHandlers.Resized
		{
			add
			{
				base.AddEventListener(EventNames.Resize, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Resize, value, false);
			}
		}

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06000D4E RID: 3406 RVA: 0x0004127D File Offset: 0x0003F47D
		// (remove) Token: 0x06000D4F RID: 3407 RVA: 0x0004128C File Offset: 0x0003F48C
		event DomEventHandler IGlobalEventHandlers.Scrolled
		{
			add
			{
				base.AddEventListener(EventNames.Scroll, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Scroll, value, false);
			}
		}

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x06000D50 RID: 3408 RVA: 0x0004129B File Offset: 0x0003F49B
		// (remove) Token: 0x06000D51 RID: 3409 RVA: 0x000412AA File Offset: 0x0003F4AA
		event DomEventHandler IGlobalEventHandlers.Seeked
		{
			add
			{
				base.AddEventListener(EventNames.Seeked, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Seeked, value, false);
			}
		}

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x06000D52 RID: 3410 RVA: 0x000412B9 File Offset: 0x0003F4B9
		// (remove) Token: 0x06000D53 RID: 3411 RVA: 0x000412C8 File Offset: 0x0003F4C8
		event DomEventHandler IGlobalEventHandlers.Seeking
		{
			add
			{
				base.AddEventListener(EventNames.Seeking, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Seeking, value, false);
			}
		}

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x06000D54 RID: 3412 RVA: 0x000412D7 File Offset: 0x0003F4D7
		// (remove) Token: 0x06000D55 RID: 3413 RVA: 0x000412E6 File Offset: 0x0003F4E6
		event DomEventHandler IGlobalEventHandlers.Selected
		{
			add
			{
				base.AddEventListener(EventNames.Select, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Select, value, false);
			}
		}

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x06000D56 RID: 3414 RVA: 0x000412F5 File Offset: 0x0003F4F5
		// (remove) Token: 0x06000D57 RID: 3415 RVA: 0x00041304 File Offset: 0x0003F504
		event DomEventHandler IGlobalEventHandlers.Shown
		{
			add
			{
				base.AddEventListener(EventNames.Show, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Show, value, false);
			}
		}

		// Token: 0x1400007C RID: 124
		// (add) Token: 0x06000D58 RID: 3416 RVA: 0x00041313 File Offset: 0x0003F513
		// (remove) Token: 0x06000D59 RID: 3417 RVA: 0x00041322 File Offset: 0x0003F522
		event DomEventHandler IGlobalEventHandlers.Stalled
		{
			add
			{
				base.AddEventListener(EventNames.Stalled, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Stalled, value, false);
			}
		}

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x06000D5A RID: 3418 RVA: 0x00041331 File Offset: 0x0003F531
		// (remove) Token: 0x06000D5B RID: 3419 RVA: 0x00041340 File Offset: 0x0003F540
		event DomEventHandler IGlobalEventHandlers.Submitted
		{
			add
			{
				base.AddEventListener(EventNames.Submit, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Submit, value, false);
			}
		}

		// Token: 0x1400007E RID: 126
		// (add) Token: 0x06000D5C RID: 3420 RVA: 0x0004134F File Offset: 0x0003F54F
		// (remove) Token: 0x06000D5D RID: 3421 RVA: 0x0004135E File Offset: 0x0003F55E
		event DomEventHandler IGlobalEventHandlers.Suspended
		{
			add
			{
				base.AddEventListener(EventNames.Suspend, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Suspend, value, false);
			}
		}

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x06000D5E RID: 3422 RVA: 0x0004136D File Offset: 0x0003F56D
		// (remove) Token: 0x06000D5F RID: 3423 RVA: 0x0004137C File Offset: 0x0003F57C
		event DomEventHandler IGlobalEventHandlers.TimeUpdated
		{
			add
			{
				base.AddEventListener(EventNames.TimeUpdate, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.TimeUpdate, value, false);
			}
		}

		// Token: 0x14000080 RID: 128
		// (add) Token: 0x06000D60 RID: 3424 RVA: 0x0004138B File Offset: 0x0003F58B
		// (remove) Token: 0x06000D61 RID: 3425 RVA: 0x0004139A File Offset: 0x0003F59A
		event DomEventHandler IGlobalEventHandlers.Toggled
		{
			add
			{
				base.AddEventListener(EventNames.Toggle, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Toggle, value, false);
			}
		}

		// Token: 0x14000081 RID: 129
		// (add) Token: 0x06000D62 RID: 3426 RVA: 0x000413A9 File Offset: 0x0003F5A9
		// (remove) Token: 0x06000D63 RID: 3427 RVA: 0x000413B8 File Offset: 0x0003F5B8
		event DomEventHandler IGlobalEventHandlers.VolumeChanged
		{
			add
			{
				base.AddEventListener(EventNames.VolumeChange, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.VolumeChange, value, false);
			}
		}

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x06000D64 RID: 3428 RVA: 0x000413C7 File Offset: 0x0003F5C7
		// (remove) Token: 0x06000D65 RID: 3429 RVA: 0x000413D6 File Offset: 0x0003F5D6
		event DomEventHandler IGlobalEventHandlers.Waiting
		{
			add
			{
				base.AddEventListener(EventNames.Waiting, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Waiting, value, false);
			}
		}

		// Token: 0x14000083 RID: 131
		// (add) Token: 0x06000D66 RID: 3430 RVA: 0x000462D0 File Offset: 0x000444D0
		// (remove) Token: 0x06000D67 RID: 3431 RVA: 0x000462DF File Offset: 0x000444DF
		event DomEventHandler IWindowEventHandlers.Printed
		{
			add
			{
				base.AddEventListener(EventNames.AfterPrint, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.AfterPrint, value, false);
			}
		}

		// Token: 0x14000084 RID: 132
		// (add) Token: 0x06000D68 RID: 3432 RVA: 0x000462EE File Offset: 0x000444EE
		// (remove) Token: 0x06000D69 RID: 3433 RVA: 0x000462FD File Offset: 0x000444FD
		event DomEventHandler IWindowEventHandlers.Printing
		{
			add
			{
				base.AddEventListener(EventNames.BeforePrint, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.BeforePrint, value, false);
			}
		}

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x06000D6A RID: 3434 RVA: 0x0004630C File Offset: 0x0004450C
		// (remove) Token: 0x06000D6B RID: 3435 RVA: 0x0004631B File Offset: 0x0004451B
		event DomEventHandler IWindowEventHandlers.Unloading
		{
			add
			{
				base.AddEventListener(EventNames.Unloading, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Unloading, value, false);
			}
		}

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x06000D6C RID: 3436 RVA: 0x0004632A File Offset: 0x0004452A
		// (remove) Token: 0x06000D6D RID: 3437 RVA: 0x00046339 File Offset: 0x00044539
		event DomEventHandler IWindowEventHandlers.HashChanged
		{
			add
			{
				base.AddEventListener(EventNames.HashChange, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.HashChange, value, false);
			}
		}

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x06000D6E RID: 3438 RVA: 0x00046348 File Offset: 0x00044548
		// (remove) Token: 0x06000D6F RID: 3439 RVA: 0x00046357 File Offset: 0x00044557
		event DomEventHandler IWindowEventHandlers.MessageReceived
		{
			add
			{
				base.AddEventListener(EventNames.Message, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Message, value, false);
			}
		}

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x06000D70 RID: 3440 RVA: 0x00046366 File Offset: 0x00044566
		// (remove) Token: 0x06000D71 RID: 3441 RVA: 0x00046375 File Offset: 0x00044575
		event DomEventHandler IWindowEventHandlers.WentOffline
		{
			add
			{
				base.AddEventListener(EventNames.Offline, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Offline, value, false);
			}
		}

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x06000D72 RID: 3442 RVA: 0x00046384 File Offset: 0x00044584
		// (remove) Token: 0x06000D73 RID: 3443 RVA: 0x00046393 File Offset: 0x00044593
		event DomEventHandler IWindowEventHandlers.WentOnline
		{
			add
			{
				base.AddEventListener(EventNames.Online, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Online, value, false);
			}
		}

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x06000D74 RID: 3444 RVA: 0x000463A2 File Offset: 0x000445A2
		// (remove) Token: 0x06000D75 RID: 3445 RVA: 0x000463B1 File Offset: 0x000445B1
		event DomEventHandler IWindowEventHandlers.PageHidden
		{
			add
			{
				base.AddEventListener(EventNames.PageHide, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.PageHide, value, false);
			}
		}

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x06000D76 RID: 3446 RVA: 0x000463C0 File Offset: 0x000445C0
		// (remove) Token: 0x06000D77 RID: 3447 RVA: 0x000463CF File Offset: 0x000445CF
		event DomEventHandler IWindowEventHandlers.PageShown
		{
			add
			{
				base.AddEventListener(EventNames.PageShow, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.PageShow, value, false);
			}
		}

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x06000D78 RID: 3448 RVA: 0x000463DE File Offset: 0x000445DE
		// (remove) Token: 0x06000D79 RID: 3449 RVA: 0x000463ED File Offset: 0x000445ED
		event DomEventHandler IWindowEventHandlers.PopState
		{
			add
			{
				base.AddEventListener(EventNames.PopState, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.PopState, value, false);
			}
		}

		// Token: 0x1400008D RID: 141
		// (add) Token: 0x06000D7A RID: 3450 RVA: 0x000463FC File Offset: 0x000445FC
		// (remove) Token: 0x06000D7B RID: 3451 RVA: 0x0004640B File Offset: 0x0004460B
		event DomEventHandler IWindowEventHandlers.Storage
		{
			add
			{
				base.AddEventListener(EventNames.Storage, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Storage, value, false);
			}
		}

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x06000D7C RID: 3452 RVA: 0x0004641A File Offset: 0x0004461A
		// (remove) Token: 0x06000D7D RID: 3453 RVA: 0x00046429 File Offset: 0x00044629
		event DomEventHandler IWindowEventHandlers.Unloaded
		{
			add
			{
				base.AddEventListener(EventNames.Unload, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Unload, value, false);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x00046438 File Offset: 0x00044638
		IHistory IWindow.History
		{
			get
			{
				return this._document.Context.SessionHistory;
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0004644A File Offset: 0x0004464A
		IWindow IWindow.Open(string url, string name, string features, string replace)
		{
			return new Window(new HtmlDocument(this._document.NewContext(name, Sandboxes.None))
			{
				Location = 
				{
					Href = url
				}
			})
			{
				Name = name
			};
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00046476 File Offset: 0x00044676
		void IWindow.Close()
		{
			this._closed = true;
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00003C25 File Offset: 0x00001E25
		void IWindow.Stop()
		{
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00003C25 File Offset: 0x00001E25
		void IWindow.Focus()
		{
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00003C25 File Offset: 0x00001E25
		void IWindow.Blur()
		{
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00003C25 File Offset: 0x00001E25
		void IWindow.Alert(string message)
		{
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0000EE9F File Offset: 0x0000D09F
		bool IWindow.Confirm(string message)
		{
			return false;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00003C25 File Offset: 0x00001E25
		void IWindow.Print()
		{
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0004647F File Offset: 0x0004467F
		int IWindowTimers.SetTimeout(Action<IWindow> handler, int timeout)
		{
			return this.QueueTask(new Func<Action<IWindow>, int, CancellationTokenSource, Task>(this.DoTimeoutAsync), handler, timeout);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00046495 File Offset: 0x00044695
		void IWindowTimers.ClearTimeout(int handle)
		{
			this.Clear(handle);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00046495 File Offset: 0x00044695
		void IWindowTimers.ClearInterval(int handle)
		{
			this.Clear(handle);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0004649E File Offset: 0x0004469E
		int IWindowTimers.SetInterval(Action<IWindow> handler, int timeout)
		{
			return this.QueueTask(new Func<Action<IWindow>, int, CancellationTokenSource, Task>(this.DoIntervalAsync), handler, timeout);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x000464B4 File Offset: 0x000446B4
		private async Task DoTimeoutAsync(Action<IWindow> callback, int timeout, CancellationTokenSource cts)
		{
			CancellationToken token = cts.Token;
			await TaskEx.Delay(timeout, token).ConfigureAwait(false);
			if (!token.IsCancellationRequested)
			{
				this._document.QueueTask(delegate
				{
					callback(this);
				});
			}
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00046514 File Offset: 0x00044714
		private async Task DoIntervalAsync(Action<IWindow> callback, int timeout, CancellationTokenSource cts)
		{
			while (!cts.Token.IsCancellationRequested)
			{
				await this.DoTimeoutAsync(callback, timeout, cts).ConfigureAwait(false);
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00046574 File Offset: 0x00044774
		private int QueueTask(Func<Action<IWindow>, int, CancellationTokenSource, Task> taskCreator, Action<IWindow> callback, int timeout)
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			taskCreator(callback, timeout, cancellationTokenSource);
			this._document.AttachReference(cancellationTokenSource);
			return cancellationTokenSource.GetHashCode();
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x000465A4 File Offset: 0x000447A4
		private void Clear(int handle)
		{
			CancellationTokenSource cancellationTokenSource = (from m in this._document.GetAttachedReferences<CancellationTokenSource>()
				where m.GetHashCode() == handle
				select m).FirstOrDefault<CancellationTokenSource>();
			if (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
			{
				cancellationTokenSource.Cancel();
			}
		}

		// Token: 0x0400096D RID: 2413
		private readonly Document _document;

		// Token: 0x0400096E RID: 2414
		private string _name;

		// Token: 0x0400096F RID: 2415
		private int _outerHeight;

		// Token: 0x04000970 RID: 2416
		private int _outerWidth;

		// Token: 0x04000971 RID: 2417
		private int _screenX;

		// Token: 0x04000972 RID: 2418
		private int _screenY;

		// Token: 0x04000973 RID: 2419
		private string _status;

		// Token: 0x04000974 RID: 2420
		private bool _closed;

		// Token: 0x04000975 RID: 2421
		private INavigator _navigator;
	}
}
