using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Services;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000354 RID: 852
	internal class HtmlElement : Element, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x14000106 RID: 262
		// (add) Token: 0x060019A0 RID: 6560 RVA: 0x00040D19 File Offset: 0x0003EF19
		// (remove) Token: 0x060019A1 RID: 6561 RVA: 0x00040D28 File Offset: 0x0003EF28
		public event DomEventHandler Aborted
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

		// Token: 0x14000107 RID: 263
		// (add) Token: 0x060019A2 RID: 6562 RVA: 0x00040D37 File Offset: 0x0003EF37
		// (remove) Token: 0x060019A3 RID: 6563 RVA: 0x00040D46 File Offset: 0x0003EF46
		public event DomEventHandler Blurred
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

		// Token: 0x14000108 RID: 264
		// (add) Token: 0x060019A4 RID: 6564 RVA: 0x00040D55 File Offset: 0x0003EF55
		// (remove) Token: 0x060019A5 RID: 6565 RVA: 0x00040D64 File Offset: 0x0003EF64
		public event DomEventHandler Cancelled
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

		// Token: 0x14000109 RID: 265
		// (add) Token: 0x060019A6 RID: 6566 RVA: 0x00040D73 File Offset: 0x0003EF73
		// (remove) Token: 0x060019A7 RID: 6567 RVA: 0x00040D82 File Offset: 0x0003EF82
		public event DomEventHandler CanPlay
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

		// Token: 0x1400010A RID: 266
		// (add) Token: 0x060019A8 RID: 6568 RVA: 0x00040D91 File Offset: 0x0003EF91
		// (remove) Token: 0x060019A9 RID: 6569 RVA: 0x00040DA0 File Offset: 0x0003EFA0
		public event DomEventHandler CanPlayThrough
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

		// Token: 0x1400010B RID: 267
		// (add) Token: 0x060019AA RID: 6570 RVA: 0x00040DAF File Offset: 0x0003EFAF
		// (remove) Token: 0x060019AB RID: 6571 RVA: 0x00040DBE File Offset: 0x0003EFBE
		public event DomEventHandler Changed
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

		// Token: 0x1400010C RID: 268
		// (add) Token: 0x060019AC RID: 6572 RVA: 0x00040DCD File Offset: 0x0003EFCD
		// (remove) Token: 0x060019AD RID: 6573 RVA: 0x00040DDC File Offset: 0x0003EFDC
		public event DomEventHandler Clicked
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

		// Token: 0x1400010D RID: 269
		// (add) Token: 0x060019AE RID: 6574 RVA: 0x00040DEB File Offset: 0x0003EFEB
		// (remove) Token: 0x060019AF RID: 6575 RVA: 0x00040DFA File Offset: 0x0003EFFA
		public event DomEventHandler CueChanged
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

		// Token: 0x1400010E RID: 270
		// (add) Token: 0x060019B0 RID: 6576 RVA: 0x00040E09 File Offset: 0x0003F009
		// (remove) Token: 0x060019B1 RID: 6577 RVA: 0x00040E18 File Offset: 0x0003F018
		public event DomEventHandler DoubleClick
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

		// Token: 0x1400010F RID: 271
		// (add) Token: 0x060019B2 RID: 6578 RVA: 0x00040E27 File Offset: 0x0003F027
		// (remove) Token: 0x060019B3 RID: 6579 RVA: 0x00040E36 File Offset: 0x0003F036
		public event DomEventHandler Drag
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

		// Token: 0x14000110 RID: 272
		// (add) Token: 0x060019B4 RID: 6580 RVA: 0x00040E45 File Offset: 0x0003F045
		// (remove) Token: 0x060019B5 RID: 6581 RVA: 0x00040E54 File Offset: 0x0003F054
		public event DomEventHandler DragEnd
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

		// Token: 0x14000111 RID: 273
		// (add) Token: 0x060019B6 RID: 6582 RVA: 0x00040E63 File Offset: 0x0003F063
		// (remove) Token: 0x060019B7 RID: 6583 RVA: 0x00040E72 File Offset: 0x0003F072
		public event DomEventHandler DragEnter
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

		// Token: 0x14000112 RID: 274
		// (add) Token: 0x060019B8 RID: 6584 RVA: 0x00040E81 File Offset: 0x0003F081
		// (remove) Token: 0x060019B9 RID: 6585 RVA: 0x00040E90 File Offset: 0x0003F090
		public event DomEventHandler DragExit
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

		// Token: 0x14000113 RID: 275
		// (add) Token: 0x060019BA RID: 6586 RVA: 0x00040E9F File Offset: 0x0003F09F
		// (remove) Token: 0x060019BB RID: 6587 RVA: 0x00040EAE File Offset: 0x0003F0AE
		public event DomEventHandler DragLeave
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

		// Token: 0x14000114 RID: 276
		// (add) Token: 0x060019BC RID: 6588 RVA: 0x00040EBD File Offset: 0x0003F0BD
		// (remove) Token: 0x060019BD RID: 6589 RVA: 0x00040ECC File Offset: 0x0003F0CC
		public event DomEventHandler DragOver
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

		// Token: 0x14000115 RID: 277
		// (add) Token: 0x060019BE RID: 6590 RVA: 0x00040EDB File Offset: 0x0003F0DB
		// (remove) Token: 0x060019BF RID: 6591 RVA: 0x00040EEA File Offset: 0x0003F0EA
		public event DomEventHandler DragStart
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

		// Token: 0x14000116 RID: 278
		// (add) Token: 0x060019C0 RID: 6592 RVA: 0x00040EF9 File Offset: 0x0003F0F9
		// (remove) Token: 0x060019C1 RID: 6593 RVA: 0x00040F08 File Offset: 0x0003F108
		public event DomEventHandler Dropped
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

		// Token: 0x14000117 RID: 279
		// (add) Token: 0x060019C2 RID: 6594 RVA: 0x00040F17 File Offset: 0x0003F117
		// (remove) Token: 0x060019C3 RID: 6595 RVA: 0x00040F26 File Offset: 0x0003F126
		public event DomEventHandler DurationChanged
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

		// Token: 0x14000118 RID: 280
		// (add) Token: 0x060019C4 RID: 6596 RVA: 0x00040F35 File Offset: 0x0003F135
		// (remove) Token: 0x060019C5 RID: 6597 RVA: 0x00040F44 File Offset: 0x0003F144
		public event DomEventHandler Emptied
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

		// Token: 0x14000119 RID: 281
		// (add) Token: 0x060019C6 RID: 6598 RVA: 0x00040F53 File Offset: 0x0003F153
		// (remove) Token: 0x060019C7 RID: 6599 RVA: 0x00040F62 File Offset: 0x0003F162
		public event DomEventHandler Ended
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

		// Token: 0x1400011A RID: 282
		// (add) Token: 0x060019C8 RID: 6600 RVA: 0x00040F71 File Offset: 0x0003F171
		// (remove) Token: 0x060019C9 RID: 6601 RVA: 0x00040F80 File Offset: 0x0003F180
		public event DomEventHandler Error
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

		// Token: 0x1400011B RID: 283
		// (add) Token: 0x060019CA RID: 6602 RVA: 0x00040F8F File Offset: 0x0003F18F
		// (remove) Token: 0x060019CB RID: 6603 RVA: 0x00040F9E File Offset: 0x0003F19E
		public event DomEventHandler Focused
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

		// Token: 0x1400011C RID: 284
		// (add) Token: 0x060019CC RID: 6604 RVA: 0x00040FAD File Offset: 0x0003F1AD
		// (remove) Token: 0x060019CD RID: 6605 RVA: 0x00040FBC File Offset: 0x0003F1BC
		public event DomEventHandler Input
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

		// Token: 0x1400011D RID: 285
		// (add) Token: 0x060019CE RID: 6606 RVA: 0x00040FCB File Offset: 0x0003F1CB
		// (remove) Token: 0x060019CF RID: 6607 RVA: 0x00040FDA File Offset: 0x0003F1DA
		public event DomEventHandler Invalid
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

		// Token: 0x1400011E RID: 286
		// (add) Token: 0x060019D0 RID: 6608 RVA: 0x00040FE9 File Offset: 0x0003F1E9
		// (remove) Token: 0x060019D1 RID: 6609 RVA: 0x00040FF8 File Offset: 0x0003F1F8
		public event DomEventHandler KeyDown
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

		// Token: 0x1400011F RID: 287
		// (add) Token: 0x060019D2 RID: 6610 RVA: 0x00041007 File Offset: 0x0003F207
		// (remove) Token: 0x060019D3 RID: 6611 RVA: 0x00041016 File Offset: 0x0003F216
		public event DomEventHandler KeyPress
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

		// Token: 0x14000120 RID: 288
		// (add) Token: 0x060019D4 RID: 6612 RVA: 0x00041025 File Offset: 0x0003F225
		// (remove) Token: 0x060019D5 RID: 6613 RVA: 0x00041034 File Offset: 0x0003F234
		public event DomEventHandler KeyUp
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

		// Token: 0x14000121 RID: 289
		// (add) Token: 0x060019D6 RID: 6614 RVA: 0x00041043 File Offset: 0x0003F243
		// (remove) Token: 0x060019D7 RID: 6615 RVA: 0x00041052 File Offset: 0x0003F252
		public event DomEventHandler Loaded
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

		// Token: 0x14000122 RID: 290
		// (add) Token: 0x060019D8 RID: 6616 RVA: 0x00041061 File Offset: 0x0003F261
		// (remove) Token: 0x060019D9 RID: 6617 RVA: 0x00041070 File Offset: 0x0003F270
		public event DomEventHandler LoadedData
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

		// Token: 0x14000123 RID: 291
		// (add) Token: 0x060019DA RID: 6618 RVA: 0x0004107F File Offset: 0x0003F27F
		// (remove) Token: 0x060019DB RID: 6619 RVA: 0x0004108E File Offset: 0x0003F28E
		public event DomEventHandler LoadedMetadata
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

		// Token: 0x14000124 RID: 292
		// (add) Token: 0x060019DC RID: 6620 RVA: 0x0004109D File Offset: 0x0003F29D
		// (remove) Token: 0x060019DD RID: 6621 RVA: 0x000410AC File Offset: 0x0003F2AC
		public event DomEventHandler Loading
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

		// Token: 0x14000125 RID: 293
		// (add) Token: 0x060019DE RID: 6622 RVA: 0x000410BB File Offset: 0x0003F2BB
		// (remove) Token: 0x060019DF RID: 6623 RVA: 0x000410CA File Offset: 0x0003F2CA
		public event DomEventHandler MouseDown
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

		// Token: 0x14000126 RID: 294
		// (add) Token: 0x060019E0 RID: 6624 RVA: 0x000410D9 File Offset: 0x0003F2D9
		// (remove) Token: 0x060019E1 RID: 6625 RVA: 0x000410E8 File Offset: 0x0003F2E8
		public event DomEventHandler MouseEnter
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

		// Token: 0x14000127 RID: 295
		// (add) Token: 0x060019E2 RID: 6626 RVA: 0x000410F7 File Offset: 0x0003F2F7
		// (remove) Token: 0x060019E3 RID: 6627 RVA: 0x00041106 File Offset: 0x0003F306
		public event DomEventHandler MouseLeave
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

		// Token: 0x14000128 RID: 296
		// (add) Token: 0x060019E4 RID: 6628 RVA: 0x00041115 File Offset: 0x0003F315
		// (remove) Token: 0x060019E5 RID: 6629 RVA: 0x00041124 File Offset: 0x0003F324
		public event DomEventHandler MouseMove
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

		// Token: 0x14000129 RID: 297
		// (add) Token: 0x060019E6 RID: 6630 RVA: 0x00041133 File Offset: 0x0003F333
		// (remove) Token: 0x060019E7 RID: 6631 RVA: 0x00041142 File Offset: 0x0003F342
		public event DomEventHandler MouseOut
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

		// Token: 0x1400012A RID: 298
		// (add) Token: 0x060019E8 RID: 6632 RVA: 0x00041151 File Offset: 0x0003F351
		// (remove) Token: 0x060019E9 RID: 6633 RVA: 0x00041160 File Offset: 0x0003F360
		public event DomEventHandler MouseOver
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

		// Token: 0x1400012B RID: 299
		// (add) Token: 0x060019EA RID: 6634 RVA: 0x0004116F File Offset: 0x0003F36F
		// (remove) Token: 0x060019EB RID: 6635 RVA: 0x0004117E File Offset: 0x0003F37E
		public event DomEventHandler MouseUp
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

		// Token: 0x1400012C RID: 300
		// (add) Token: 0x060019EC RID: 6636 RVA: 0x0004118D File Offset: 0x0003F38D
		// (remove) Token: 0x060019ED RID: 6637 RVA: 0x0004119C File Offset: 0x0003F39C
		public event DomEventHandler MouseWheel
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

		// Token: 0x1400012D RID: 301
		// (add) Token: 0x060019EE RID: 6638 RVA: 0x000411AB File Offset: 0x0003F3AB
		// (remove) Token: 0x060019EF RID: 6639 RVA: 0x000411BA File Offset: 0x0003F3BA
		public event DomEventHandler Paused
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

		// Token: 0x1400012E RID: 302
		// (add) Token: 0x060019F0 RID: 6640 RVA: 0x000411C9 File Offset: 0x0003F3C9
		// (remove) Token: 0x060019F1 RID: 6641 RVA: 0x000411D8 File Offset: 0x0003F3D8
		public event DomEventHandler Played
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

		// Token: 0x1400012F RID: 303
		// (add) Token: 0x060019F2 RID: 6642 RVA: 0x000411E7 File Offset: 0x0003F3E7
		// (remove) Token: 0x060019F3 RID: 6643 RVA: 0x000411F6 File Offset: 0x0003F3F6
		public event DomEventHandler Playing
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

		// Token: 0x14000130 RID: 304
		// (add) Token: 0x060019F4 RID: 6644 RVA: 0x00041205 File Offset: 0x0003F405
		// (remove) Token: 0x060019F5 RID: 6645 RVA: 0x00041214 File Offset: 0x0003F414
		public event DomEventHandler Progress
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

		// Token: 0x14000131 RID: 305
		// (add) Token: 0x060019F6 RID: 6646 RVA: 0x00041223 File Offset: 0x0003F423
		// (remove) Token: 0x060019F7 RID: 6647 RVA: 0x00041232 File Offset: 0x0003F432
		public event DomEventHandler RateChanged
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

		// Token: 0x14000132 RID: 306
		// (add) Token: 0x060019F8 RID: 6648 RVA: 0x00041241 File Offset: 0x0003F441
		// (remove) Token: 0x060019F9 RID: 6649 RVA: 0x00041250 File Offset: 0x0003F450
		public event DomEventHandler Resetted
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

		// Token: 0x14000133 RID: 307
		// (add) Token: 0x060019FA RID: 6650 RVA: 0x0004125F File Offset: 0x0003F45F
		// (remove) Token: 0x060019FB RID: 6651 RVA: 0x0004126E File Offset: 0x0003F46E
		public event DomEventHandler Resized
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

		// Token: 0x14000134 RID: 308
		// (add) Token: 0x060019FC RID: 6652 RVA: 0x0004127D File Offset: 0x0003F47D
		// (remove) Token: 0x060019FD RID: 6653 RVA: 0x0004128C File Offset: 0x0003F48C
		public event DomEventHandler Scrolled
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

		// Token: 0x14000135 RID: 309
		// (add) Token: 0x060019FE RID: 6654 RVA: 0x0004129B File Offset: 0x0003F49B
		// (remove) Token: 0x060019FF RID: 6655 RVA: 0x000412AA File Offset: 0x0003F4AA
		public event DomEventHandler Seeked
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

		// Token: 0x14000136 RID: 310
		// (add) Token: 0x06001A00 RID: 6656 RVA: 0x000412B9 File Offset: 0x0003F4B9
		// (remove) Token: 0x06001A01 RID: 6657 RVA: 0x000412C8 File Offset: 0x0003F4C8
		public event DomEventHandler Seeking
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

		// Token: 0x14000137 RID: 311
		// (add) Token: 0x06001A02 RID: 6658 RVA: 0x000412D7 File Offset: 0x0003F4D7
		// (remove) Token: 0x06001A03 RID: 6659 RVA: 0x000412E6 File Offset: 0x0003F4E6
		public event DomEventHandler Selected
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

		// Token: 0x14000138 RID: 312
		// (add) Token: 0x06001A04 RID: 6660 RVA: 0x000412F5 File Offset: 0x0003F4F5
		// (remove) Token: 0x06001A05 RID: 6661 RVA: 0x00041304 File Offset: 0x0003F504
		public event DomEventHandler Shown
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

		// Token: 0x14000139 RID: 313
		// (add) Token: 0x06001A06 RID: 6662 RVA: 0x00041313 File Offset: 0x0003F513
		// (remove) Token: 0x06001A07 RID: 6663 RVA: 0x00041322 File Offset: 0x0003F522
		public event DomEventHandler Stalled
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

		// Token: 0x1400013A RID: 314
		// (add) Token: 0x06001A08 RID: 6664 RVA: 0x00041331 File Offset: 0x0003F531
		// (remove) Token: 0x06001A09 RID: 6665 RVA: 0x00041340 File Offset: 0x0003F540
		public event DomEventHandler Submitted
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

		// Token: 0x1400013B RID: 315
		// (add) Token: 0x06001A0A RID: 6666 RVA: 0x0004134F File Offset: 0x0003F54F
		// (remove) Token: 0x06001A0B RID: 6667 RVA: 0x0004135E File Offset: 0x0003F55E
		public event DomEventHandler Suspended
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

		// Token: 0x1400013C RID: 316
		// (add) Token: 0x06001A0C RID: 6668 RVA: 0x0004136D File Offset: 0x0003F56D
		// (remove) Token: 0x06001A0D RID: 6669 RVA: 0x0004137C File Offset: 0x0003F57C
		public event DomEventHandler TimeUpdated
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

		// Token: 0x1400013D RID: 317
		// (add) Token: 0x06001A0E RID: 6670 RVA: 0x0004138B File Offset: 0x0003F58B
		// (remove) Token: 0x06001A0F RID: 6671 RVA: 0x0004139A File Offset: 0x0003F59A
		public event DomEventHandler Toggled
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

		// Token: 0x1400013E RID: 318
		// (add) Token: 0x06001A10 RID: 6672 RVA: 0x000413A9 File Offset: 0x0003F5A9
		// (remove) Token: 0x06001A11 RID: 6673 RVA: 0x000413B8 File Offset: 0x0003F5B8
		public event DomEventHandler VolumeChanged
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

		// Token: 0x1400013F RID: 319
		// (add) Token: 0x06001A12 RID: 6674 RVA: 0x000413C7 File Offset: 0x0003F5C7
		// (remove) Token: 0x06001A13 RID: 6675 RVA: 0x000413D6 File Offset: 0x0003F5D6
		public event DomEventHandler Waiting
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

		// Token: 0x06001A14 RID: 6676 RVA: 0x00050672 File Offset: 0x0004E872
		public HtmlElement(Document owner, string localName, string prefix = null, NodeFlags flags = NodeFlags.None)
			: base(owner, HtmlElement.Combine(prefix, localName), localName, prefix, NamespaceNames.HtmlUri, flags | NodeFlags.HtmlMember)
		{
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x00050691 File Offset: 0x0004E891
		// (set) Token: 0x06001A16 RID: 6678 RVA: 0x0005069E File Offset: 0x0004E89E
		public bool IsHidden
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Hidden);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Hidden, value);
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001A17 RID: 6679 RVA: 0x000506AC File Offset: 0x0004E8AC
		// (set) Token: 0x06001A18 RID: 6680 RVA: 0x000506ED File Offset: 0x0004E8ED
		public IHtmlMenuElement ContextMenu
		{
			get
			{
				if (this._menu == null)
				{
					string ownAttribute = this.GetOwnAttribute(AttributeNames.ContextMenu);
					if (!string.IsNullOrEmpty(ownAttribute))
					{
						return base.Owner.GetElementById(ownAttribute) as IHtmlMenuElement;
					}
				}
				return this._menu;
			}
			set
			{
				this._menu = value;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x000506F6 File Offset: 0x0004E8F6
		public ISettableTokenList DropZone
		{
			get
			{
				if (this._dropZone == null)
				{
					this._dropZone = new SettableTokenList(this.GetOwnAttribute(AttributeNames.DropZone));
					this._dropZone.Changed += delegate(string value)
					{
						base.UpdateAttribute(AttributeNames.DropZone, value);
					};
				}
				return this._dropZone;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x00050733 File Offset: 0x0004E933
		// (set) Token: 0x06001A1B RID: 6683 RVA: 0x00050746 File Offset: 0x0004E946
		public bool IsDraggable
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Draggable).ToBoolean(false);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Draggable, value.ToString(), false);
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x0005075B File Offset: 0x0004E95B
		// (set) Token: 0x06001A1D RID: 6685 RVA: 0x00050771 File Offset: 0x0004E971
		public string AccessKey
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.AccessKey) ?? string.Empty;
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.AccessKey, value, false);
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x00050780 File Offset: 0x0004E980
		public string AccessKeyLabel
		{
			get
			{
				return this.AccessKey;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x00050788 File Offset: 0x0004E988
		// (set) Token: 0x06001A20 RID: 6688 RVA: 0x0005079F File Offset: 0x0004E99F
		public string Language
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Lang) ?? this.GetDefaultLanguage();
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Lang, value, false);
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x000507AE File Offset: 0x0004E9AE
		// (set) Token: 0x06001A22 RID: 6690 RVA: 0x000507BB File Offset: 0x0004E9BB
		public string Title
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Title);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Title, value, false);
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x000507CA File Offset: 0x0004E9CA
		// (set) Token: 0x06001A24 RID: 6692 RVA: 0x000507D7 File Offset: 0x0004E9D7
		public string Direction
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Dir);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Dir, value, false);
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x000507E6 File Offset: 0x0004E9E6
		// (set) Token: 0x06001A26 RID: 6694 RVA: 0x000507F9 File Offset: 0x0004E9F9
		public bool IsSpellChecked
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Spellcheck).ToBoolean(false);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Spellcheck, value.ToString(), false);
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x0005080E File Offset: 0x0004EA0E
		// (set) Token: 0x06001A28 RID: 6696 RVA: 0x00050821 File Offset: 0x0004EA21
		public int TabIndex
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.TabIndex).ToInteger(0);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.TabIndex, value.ToString(), false);
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x00050838 File Offset: 0x0004EA38
		public IStringMap Dataset
		{
			get
			{
				StringMap stringMap;
				if ((stringMap = this._dataset) == null)
				{
					stringMap = (this._dataset = new StringMap("data-", this));
				}
				return stringMap;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x00050863 File Offset: 0x0004EA63
		// (set) Token: 0x06001A2B RID: 6699 RVA: 0x00050870 File Offset: 0x0004EA70
		public string ContentEditable
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.ContentEditable);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.ContentEditable, value, false);
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001A2C RID: 6700 RVA: 0x00050880 File Offset: 0x0004EA80
		public bool IsContentEditable
		{
			get
			{
				ContentEditableMode contentEditableMode = this.ContentEditable.ToEnum(ContentEditableMode.Inherited);
				if (contentEditableMode != ContentEditableMode.True)
				{
					IHtmlElement htmlElement = base.ParentElement as IHtmlElement;
					return contentEditableMode == ContentEditableMode.Inherited && htmlElement != null && htmlElement.IsContentEditable;
				}
				return true;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001A2D RID: 6701 RVA: 0x000508BB File Offset: 0x0004EABB
		// (set) Token: 0x06001A2E RID: 6702 RVA: 0x000508D1 File Offset: 0x0004EAD1
		public bool IsTranslated
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Translate).ToEnum(SimpleChoice.Yes) == SimpleChoice.Yes;
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Translate, value ? Keywords.Yes : Keywords.No, false);
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001A2F RID: 6703 RVA: 0x000508F0 File Offset: 0x0004EAF0
		// (set) Token: 0x06001A30 RID: 6704 RVA: 0x00050A48 File Offset: 0x0004EC48
		public string InnerText
		{
			get
			{
				bool? flag = null;
				if (base.Owner == null)
				{
					flag = new bool?(true);
				}
				if (flag == null)
				{
					ICssStyleDeclaration cssStyleDeclaration = this.ComputeCurrentStyle();
					if (!string.IsNullOrEmpty((cssStyleDeclaration != null) ? cssStyleDeclaration.Display : null))
					{
						flag = new bool?(cssStyleDeclaration.Display == "none");
					}
				}
				if (flag == null)
				{
					flag = new bool?(this.IsHidden);
				}
				if (flag.Value)
				{
					return this.TextContent;
				}
				StringBuilder stringBuilder = Pool.NewStringBuilder();
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				StringBuilder stringBuilder2 = stringBuilder;
				Dictionary<int, int> dictionary2 = dictionary;
				IElement parentElement = base.ParentElement;
				HtmlElement.InnerTextCollection(this, stringBuilder2, dictionary2, (parentElement != null) ? parentElement.ComputeCurrentStyle() : null);
				dictionary.Remove(0);
				dictionary.Remove(stringBuilder.Length);
				int num = 0;
				foreach (KeyValuePair<int, int> keyValuePair in dictionary.OrderBy((KeyValuePair<int, int> kv) => kv.Key))
				{
					int num2 = keyValuePair.Key + num;
					stringBuilder.Insert(num2, new string('\n', keyValuePair.Value));
					num += keyValuePair.Value;
				}
				return stringBuilder.ToPool();
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.ReplaceAll(null, false);
					return;
				}
				DocumentFragment documentFragment = new DocumentFragment(base.Owner);
				StringBuilder stringBuilder = Pool.NewStringBuilder();
				for (int i = 0; i < value.Length; i++)
				{
					char c = value[i];
					if (c == '\n' || c == '\r')
					{
						if (c != '\r' || i + 1 >= value.Length || value[i + 1] != '\n')
						{
							if (stringBuilder.Length > 0)
							{
								documentFragment.AppendChild(new TextNode(base.Owner, stringBuilder.ToPool()));
								stringBuilder = Pool.NewStringBuilder();
							}
							documentFragment.AppendChild(new HtmlBreakRowElement(base.Owner, null));
						}
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				string text = stringBuilder.ToPool();
				if (text.Length > 0)
				{
					documentFragment.Append(new INode[]
					{
						new TextNode(base.Owner, text)
					});
				}
				base.ReplaceAll(documentFragment, false);
			}
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00050B38 File Offset: 0x0004ED38
		private static void InnerTextCollection(INode node, StringBuilder sb, Dictionary<int, int> requiredLineBreakCounts, ICssStyleDeclaration parentStyle)
		{
			if (!HtmlElement.HasCssBox(node))
			{
				return;
			}
			IElement element = node as IElement;
			ICssStyleDeclaration cssStyleDeclaration = ((element != null) ? element.ComputeCurrentStyle() : null);
			bool? flag = null;
			if (cssStyleDeclaration != null)
			{
				if (!string.IsNullOrEmpty(cssStyleDeclaration.Display))
				{
					flag = new bool?(cssStyleDeclaration.Display == "none");
				}
				if (!string.IsNullOrEmpty(cssStyleDeclaration.Visibility) && flag != true)
				{
					flag = new bool?(cssStyleDeclaration.Visibility != "visible");
				}
			}
			if (flag == null)
			{
				IHtmlElement htmlElement = node as IHtmlElement;
				flag = new bool?(htmlElement != null && htmlElement.IsHidden);
			}
			if (flag.Value)
			{
				return;
			}
			int length = sb.Length;
			foreach (INode node2 in node.ChildNodes)
			{
				HtmlElement.InnerTextCollection(node2, sb, requiredLineBreakCounts, cssStyleDeclaration);
			}
			if (node is IText)
			{
				HtmlElement.ProcessText(((IText)node).Data, sb, parentStyle);
			}
			else if (node is IHtmlBreakRowElement)
			{
				sb.Append('\n');
			}
			else if ((node is IHtmlTableCellElement && string.IsNullOrEmpty(cssStyleDeclaration.Display)) || cssStyleDeclaration.Display == "table-cell")
			{
				IElement element2 = node.NextSibling as IElement;
				if (element2 != null)
				{
					ICssStyleDeclaration cssStyleDeclaration2 = element2.ComputeCurrentStyle();
					if ((element2 is IHtmlTableCellElement && string.IsNullOrEmpty(cssStyleDeclaration2.Display)) || cssStyleDeclaration2.Display == "table-cell")
					{
						sb.Append('\t');
					}
				}
			}
			else if ((node is IHtmlTableRowElement && string.IsNullOrEmpty(cssStyleDeclaration.Display)) || cssStyleDeclaration.Display == "table-row")
			{
				IElement element3 = node.NextSibling as IElement;
				if (element3 != null)
				{
					ICssStyleDeclaration cssStyleDeclaration3 = element3.ComputeCurrentStyle();
					if ((element3 is IHtmlTableRowElement && string.IsNullOrEmpty(cssStyleDeclaration3.Display)) || cssStyleDeclaration3.Display == "table-row")
					{
						sb.Append('\n');
					}
				}
			}
			else if (node is IHtmlParagraphElement)
			{
				int num = 0;
				requiredLineBreakCounts.TryGetValue(length, out num);
				if (num < 2)
				{
					requiredLineBreakCounts[length] = 2;
				}
				int num2 = 0;
				requiredLineBreakCounts.TryGetValue(sb.Length, out num2);
				if (num2 < 2)
				{
					requiredLineBreakCounts[sb.Length] = 2;
				}
			}
			bool? flag2 = null;
			if (cssStyleDeclaration != null && HtmlElement.IsBlockLevelDisplay(cssStyleDeclaration.Display))
			{
				flag2 = new bool?(true);
			}
			if (flag2 == null)
			{
				flag2 = new bool?(HtmlElement.IsBlockLevel(node));
			}
			if (flag2.Value)
			{
				int num3 = 0;
				requiredLineBreakCounts.TryGetValue(length, out num3);
				if (num3 < 1)
				{
					requiredLineBreakCounts[length] = 1;
				}
				int num4 = 0;
				requiredLineBreakCounts.TryGetValue(sb.Length, out num4);
				if (num4 < 1)
				{
					requiredLineBreakCounts[sb.Length] = 1;
				}
			}
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00050E3C File Offset: 0x0004F03C
		private static bool HasCssBox(INode node)
		{
			string nodeName = node.NodeName;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(nodeName);
			if (num <= 1917552076U)
			{
				if (num <= 896002961U)
				{
					if (num <= 262383345U)
					{
						if (num != 187591081U)
						{
							if (num != 262383345U)
							{
								return true;
							}
							if (!(nodeName == "CANVAS"))
							{
								return true;
							}
						}
						else if (!(nodeName == "LINK"))
						{
							return true;
						}
					}
					else if (num != 275929992U)
					{
						if (num != 896002961U)
						{
							return true;
						}
						if (!(nodeName == "COL"))
						{
							return true;
						}
					}
					else if (!(nodeName == "COLGROUP"))
					{
						return true;
					}
				}
				else if (num <= 1450621046U)
				{
					if (num != 1168905659U)
					{
						if (num != 1450621046U)
						{
							return true;
						}
						if (!(nodeName == "STYLE"))
						{
							return true;
						}
					}
					else if (!(nodeName == "TEXTAREA"))
					{
						return true;
					}
				}
				else if (num != 1609624849U)
				{
					if (num != 1638025307U)
					{
						if (num != 1917552076U)
						{
							return true;
						}
						if (!(nodeName == "VIDEO"))
						{
							return true;
						}
					}
					else if (!(nodeName == "INPUT"))
					{
						return true;
					}
				}
				else if (!(nodeName == "NOSCRIPT"))
				{
					return true;
				}
			}
			else if (num <= 2913649441U)
			{
				if (num <= 2861125342U)
				{
					if (num != 2820093418U)
					{
						if (num != 2861125342U)
						{
							return true;
						}
						if (!(nodeName == "FRAMESET"))
						{
							return true;
						}
					}
					else if (!(nodeName == "WBR"))
					{
						return true;
					}
				}
				else if (num != 2892765957U)
				{
					if (num != 2910266123U)
					{
						if (num != 2913649441U)
						{
							return true;
						}
						if (!(nodeName == "IFRAME"))
						{
							return true;
						}
					}
					else if (!(nodeName == "TEMPLATE"))
					{
						return true;
					}
				}
				else if (!(nodeName == "DETAILS"))
				{
					return true;
				}
			}
			else if (num <= 3032449030U)
			{
				if (num != 3011136198U)
				{
					if (num != 3032449030U)
					{
						return true;
					}
					if (!(nodeName == "FRAME"))
					{
						return true;
					}
				}
				else if (!(nodeName == "PROGRESS"))
				{
					return true;
				}
			}
			else if (num != 3351329828U)
			{
				if (num != 3568162690U)
				{
					if (num != 4253975018U)
					{
						return true;
					}
					if (!(nodeName == "SCRIPT"))
					{
						return true;
					}
				}
				else if (!(nodeName == "METER"))
				{
					return true;
				}
			}
			else if (!(nodeName == "IMG"))
			{
				return true;
			}
			return false;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x000510E4 File Offset: 0x0004F2E4
		private static bool IsBlockLevelDisplay(string display)
		{
			return display == "block" || display == "flow-root" || display == "flex" || display == "grid" || display == "table" || display == "table-caption";
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00051144 File Offset: 0x0004F344
		private static bool IsBlockLevel(INode node)
		{
			string nodeName = node.NodeName;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(nodeName);
			if (num <= 1917552076U)
			{
				if (num <= 1180649959U)
				{
					if (num <= 751723973U)
					{
						if (num <= 262383345U)
						{
							if (num != 136495084U)
							{
								if (num != 262383345U)
								{
									return false;
								}
								if (!(nodeName == "CANVAS"))
								{
									return false;
								}
							}
							else if (!(nodeName == "GROUP"))
							{
								return false;
							}
						}
						else if (num != 352121151U)
						{
							if (num != 751723973U)
							{
								return false;
							}
							if (!(nodeName == "DT"))
							{
								return false;
							}
						}
						else if (!(nodeName == "TABLE"))
						{
							return false;
						}
					}
					else if (num <= 1072620361U)
					{
						if (num != 1020165877U)
						{
							if (num != 1072620361U)
							{
								return false;
							}
							if (!(nodeName == "FIGURE"))
							{
								return false;
							}
						}
						else if (!(nodeName == "DD"))
						{
							return false;
						}
					}
					else if (num != 1122648243U)
					{
						if (num != 1154386829U)
						{
							if (num != 1180649959U)
							{
								return false;
							}
							if (!(nodeName == "ASIDE"))
							{
								return false;
							}
						}
						else if (!(nodeName == "DL"))
						{
							return false;
						}
					}
					else if (!(nodeName == "ADDRESS"))
					{
						return false;
					}
				}
				else if (num <= 1608662470U)
				{
					if (num <= 1381882233U)
					{
						if (num != 1256681236U)
						{
							if (num != 1381882233U)
							{
								return false;
							}
							if (!(nodeName == "ARTICLE"))
							{
								return false;
							}
						}
						else if (!(nodeName == "OPTION"))
						{
							return false;
						}
					}
					else if (num != 1469629700U)
					{
						if (num != 1608662470U)
						{
							return false;
						}
						if (!(nodeName == "LI"))
						{
							return false;
						}
					}
					else if (!(nodeName == "OUTPUT"))
					{
						return false;
					}
				}
				else if (num <= 1780072296U)
				{
					if (num != 1609624849U)
					{
						if (num != 1780072296U)
						{
							return false;
						}
						if (!(nodeName == "DIV"))
						{
							return false;
						}
					}
					else if (!(nodeName == "NOSCRIPT"))
					{
						return false;
					}
				}
				else if (num != 1794184891U)
				{
					if (num != 1860915135U)
					{
						if (num != 1917552076U)
						{
							return false;
						}
						if (!(nodeName == "VIDEO"))
						{
							return false;
						}
					}
					else if (!(nodeName == "HR"))
					{
						return false;
					}
				}
				else if (!(nodeName == "TFOOT"))
				{
					return false;
				}
			}
			else if (num <= 2381021324U)
			{
				if (num <= 2196026230U)
				{
					if (num <= 2129901492U)
					{
						if (num != 2108357703U)
						{
							if (num != 2129901492U)
							{
								return false;
							}
							if (!(nodeName == "UL"))
							{
								return false;
							}
						}
						else if (!(nodeName == "FORM"))
						{
							return false;
						}
					}
					else if (num != 2141754732U)
					{
						if (num != 2196026230U)
						{
							return false;
						}
						if (!(nodeName == "OL"))
						{
							return false;
						}
					}
					else if (!(nodeName == "SECTION"))
					{
						return false;
					}
				}
				else if (num <= 2330688467U)
				{
					if (num != 2263853280U)
					{
						if (num != 2330688467U)
						{
							return false;
						}
						if (!(nodeName == "H6"))
						{
							return false;
						}
					}
					else if (!(nodeName == "HEADER"))
					{
						return false;
					}
				}
				else if (num != 2347466086U)
				{
					if (num != 2364243705U)
					{
						if (num != 2381021324U)
						{
							return false;
						}
						if (!(nodeName == "H3"))
						{
							return false;
						}
					}
					else if (!(nodeName == "H4"))
					{
						return false;
					}
				}
				else if (!(nodeName == "H5"))
				{
					return false;
				}
			}
			else if (num <= 2935185010U)
			{
				if (num <= 2414576562U)
				{
					if (num != 2397798943U)
					{
						if (num != 2414576562U)
						{
							return false;
						}
						if (!(nodeName == "H1"))
						{
							return false;
						}
					}
					else if (!(nodeName == "H2"))
					{
						return false;
					}
				}
				else if (num != 2519148680U)
				{
					if (num != 2580257042U)
					{
						if (num != 2935185010U)
						{
							return false;
						}
						if (!(nodeName == "FOOTER"))
						{
							return false;
						}
					}
					else if (!(nodeName == "NAV"))
					{
						return false;
					}
				}
				else if (!(nodeName == "MAIN"))
				{
					return false;
				}
			}
			else if (num <= 3476044038U)
			{
				if (num != 3270526110U)
				{
					if (num != 3476044038U)
					{
						return false;
					}
					if (!(nodeName == "PRE"))
					{
						return false;
					}
				}
				else if (!(nodeName == "BLOCKQUOTE"))
				{
					return false;
				}
			}
			else if (num != 3574337935U)
			{
				if (num != 3705940733U)
				{
					if (num != 3909703509U)
					{
						return false;
					}
					if (!(nodeName == "FIGCAPTION"))
					{
						return false;
					}
				}
				else if (!(nodeName == "FIELDSET"))
				{
					return false;
				}
			}
			else if (!(nodeName == "P"))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00051698 File Offset: 0x0004F898
		private static void ProcessText(string text, StringBuilder sb, ICssStyleDeclaration style)
		{
			int length = sb.Length;
			string text2 = ((style != null) ? style.WhiteSpace : null);
			string text3 = ((style != null) ? style.TextTransform : null);
			bool flag = length <= 0 || (char.IsWhiteSpace(sb[length - 1]) && sb[length - 1] != '\u00a0');
			int i = 0;
			while (i < text.Length)
			{
				char c = text[i];
				if (char.IsWhiteSpace(c) && c != '\u00a0')
				{
					if (!(text2 == "pre") && !(text2 == "pre-wrap"))
					{
						if (!(text2 == "pre-line"))
						{
							if (!(text2 == "nowrap") && !(text2 == "normal"))
							{
							}
							if (flag)
							{
								goto IL_0139;
							}
							c = ' ';
						}
						else if (c == ' ' || c == '\t')
						{
							if (flag)
							{
								goto IL_0139;
							}
							c = ' ';
						}
					}
					flag = true;
					goto IL_0130;
				}
				if (!(text3 == "uppercase"))
				{
					if (!(text3 == "lowercase"))
					{
						if (!(text3 == "capitalize"))
						{
							if (!(text3 == "none"))
							{
							}
						}
						else if (flag)
						{
							c = char.ToUpperInvariant(c);
						}
					}
					else
					{
						c = char.ToLowerInvariant(c);
					}
				}
				else
				{
					c = char.ToUpperInvariant(c);
				}
				flag = false;
				goto IL_0130;
				IL_0139:
				i++;
				continue;
				IL_0130:
				sb.Append(c);
				goto IL_0139;
			}
			if (flag)
			{
				for (int j = sb.Length - 1; j >= length; j--)
				{
					char c2 = sb[j];
					if (!char.IsWhiteSpace(c2) || c2 == '\u00a0')
					{
						sb.Remove(j + 1, sb.Length - 1 - j);
						return;
					}
				}
			}
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0005183E File Offset: 0x0004FA3E
		public void DoSpellCheck()
		{
			base.Owner.Options.GetSpellCheck(this.Language);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00051857 File Offset: 0x0004FA57
		public virtual void DoClick()
		{
			this.IsClickedCancelled();
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00003C25 File Offset: 0x00001E25
		public virtual void DoFocus()
		{
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00003C25 File Offset: 0x00001E25
		public virtual void DoBlur()
		{
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00051860 File Offset: 0x0004FA60
		public override INode Clone(bool deep = true)
		{
			HtmlElement htmlElement = base.Owner.Options.GetFactory<IElementFactory<HtmlElement>>().Create(base.Owner, base.LocalName, base.Prefix);
			base.CloneElement(htmlElement, deep);
			return htmlElement;
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000518A0 File Offset: 0x0004FAA0
		internal override void SetupElement()
		{
			base.SetupElement();
			string ownAttribute = this.GetOwnAttribute(AttributeNames.Style);
			if (ownAttribute != null)
			{
				base.UpdateStyle(ownAttribute);
			}
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x000518C9 File Offset: 0x0004FAC9
		internal void UpdateDropZone(string value)
		{
			SettableTokenList dropZone = this._dropZone;
			if (dropZone == null)
			{
				return;
			}
			dropZone.Update(value);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x000518DC File Offset: 0x0004FADC
		protected bool IsClickedCancelled()
		{
			return this.Fire(delegate(MouseEvent m)
			{
				m.Init(EventNames.Click, true, true, base.Owner.DefaultView, 0, 0, 0, 0, 0, false, false, false, false, MouseButton.Primary, this);
			}, null);
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x000518F4 File Offset: 0x0004FAF4
		protected IHtmlFormElement GetAssignedForm()
		{
			INode node = base.Parent;
			while (node != null && !(node is IHtmlFormElement))
			{
				node = node.ParentElement;
			}
			if (node == null)
			{
				string ownAttribute = this.GetOwnAttribute(AttributeNames.Form);
				Document owner = base.Owner;
				if (owner == null || node != null || string.IsNullOrEmpty(ownAttribute))
				{
					return null;
				}
				node = owner.GetElementById(ownAttribute);
			}
			return node as IHtmlFormElement;
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x00051950 File Offset: 0x0004FB50
		private string GetDefaultLanguage()
		{
			IHtmlElement htmlElement = base.ParentElement as IHtmlElement;
			if (htmlElement == null)
			{
				return base.Owner.Options.GetLanguage();
			}
			return htmlElement.Language;
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00051983 File Offset: 0x0004FB83
		private static string Combine(string prefix, string localName)
		{
			return ((prefix != null) ? (prefix + ":" + localName) : localName).ToUpperInvariant();
		}

		// Token: 0x04000CC7 RID: 3271
		private StringMap _dataset;

		// Token: 0x04000CC8 RID: 3272
		private IHtmlMenuElement _menu;

		// Token: 0x04000CC9 RID: 3273
		private SettableTokenList _dropZone;
	}
}
