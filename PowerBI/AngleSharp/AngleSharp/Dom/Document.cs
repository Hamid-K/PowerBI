using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Commands;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Html;
using AngleSharp.Dom.Mathml;
using AngleSharp.Dom.Svg;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Network;
using AngleSharp.Services;

namespace AngleSharp.Dom
{
	// Token: 0x0200014C RID: 332
	internal abstract class Document : Node, IDocument, INode, IEventTarget, IMarkupFormattable, IParentNode, IGlobalEventHandlers, IDocumentStyle, INonElementParentNode, IDisposable
	{
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000A26 RID: 2598 RVA: 0x00040CFB File Offset: 0x0003EEFB
		// (remove) Token: 0x06000A27 RID: 2599 RVA: 0x00040D0A File Offset: 0x0003EF0A
		public event DomEventHandler ReadyStateChanged
		{
			add
			{
				base.AddEventListener(EventNames.ReadyStateChanged, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.ReadyStateChanged, value, false);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000A28 RID: 2600 RVA: 0x00040D19 File Offset: 0x0003EF19
		// (remove) Token: 0x06000A29 RID: 2601 RVA: 0x00040D28 File Offset: 0x0003EF28
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

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000A2A RID: 2602 RVA: 0x00040D37 File Offset: 0x0003EF37
		// (remove) Token: 0x06000A2B RID: 2603 RVA: 0x00040D46 File Offset: 0x0003EF46
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

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000A2C RID: 2604 RVA: 0x00040D55 File Offset: 0x0003EF55
		// (remove) Token: 0x06000A2D RID: 2605 RVA: 0x00040D64 File Offset: 0x0003EF64
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

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000A2E RID: 2606 RVA: 0x00040D73 File Offset: 0x0003EF73
		// (remove) Token: 0x06000A2F RID: 2607 RVA: 0x00040D82 File Offset: 0x0003EF82
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

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000A30 RID: 2608 RVA: 0x00040D91 File Offset: 0x0003EF91
		// (remove) Token: 0x06000A31 RID: 2609 RVA: 0x00040DA0 File Offset: 0x0003EFA0
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

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000A32 RID: 2610 RVA: 0x00040DAF File Offset: 0x0003EFAF
		// (remove) Token: 0x06000A33 RID: 2611 RVA: 0x00040DBE File Offset: 0x0003EFBE
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

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000A34 RID: 2612 RVA: 0x00040DCD File Offset: 0x0003EFCD
		// (remove) Token: 0x06000A35 RID: 2613 RVA: 0x00040DDC File Offset: 0x0003EFDC
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

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000A36 RID: 2614 RVA: 0x00040DEB File Offset: 0x0003EFEB
		// (remove) Token: 0x06000A37 RID: 2615 RVA: 0x00040DFA File Offset: 0x0003EFFA
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

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000A38 RID: 2616 RVA: 0x00040E09 File Offset: 0x0003F009
		// (remove) Token: 0x06000A39 RID: 2617 RVA: 0x00040E18 File Offset: 0x0003F018
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

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000A3A RID: 2618 RVA: 0x00040E27 File Offset: 0x0003F027
		// (remove) Token: 0x06000A3B RID: 2619 RVA: 0x00040E36 File Offset: 0x0003F036
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

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000A3C RID: 2620 RVA: 0x00040E45 File Offset: 0x0003F045
		// (remove) Token: 0x06000A3D RID: 2621 RVA: 0x00040E54 File Offset: 0x0003F054
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

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000A3E RID: 2622 RVA: 0x00040E63 File Offset: 0x0003F063
		// (remove) Token: 0x06000A3F RID: 2623 RVA: 0x00040E72 File Offset: 0x0003F072
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

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000A40 RID: 2624 RVA: 0x00040E81 File Offset: 0x0003F081
		// (remove) Token: 0x06000A41 RID: 2625 RVA: 0x00040E90 File Offset: 0x0003F090
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

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000A42 RID: 2626 RVA: 0x00040E9F File Offset: 0x0003F09F
		// (remove) Token: 0x06000A43 RID: 2627 RVA: 0x00040EAE File Offset: 0x0003F0AE
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

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000A44 RID: 2628 RVA: 0x00040EBD File Offset: 0x0003F0BD
		// (remove) Token: 0x06000A45 RID: 2629 RVA: 0x00040ECC File Offset: 0x0003F0CC
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

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000A46 RID: 2630 RVA: 0x00040EDB File Offset: 0x0003F0DB
		// (remove) Token: 0x06000A47 RID: 2631 RVA: 0x00040EEA File Offset: 0x0003F0EA
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

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000A48 RID: 2632 RVA: 0x00040EF9 File Offset: 0x0003F0F9
		// (remove) Token: 0x06000A49 RID: 2633 RVA: 0x00040F08 File Offset: 0x0003F108
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

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000A4A RID: 2634 RVA: 0x00040F17 File Offset: 0x0003F117
		// (remove) Token: 0x06000A4B RID: 2635 RVA: 0x00040F26 File Offset: 0x0003F126
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

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000A4C RID: 2636 RVA: 0x00040F35 File Offset: 0x0003F135
		// (remove) Token: 0x06000A4D RID: 2637 RVA: 0x00040F44 File Offset: 0x0003F144
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

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000A4E RID: 2638 RVA: 0x00040F53 File Offset: 0x0003F153
		// (remove) Token: 0x06000A4F RID: 2639 RVA: 0x00040F62 File Offset: 0x0003F162
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

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000A50 RID: 2640 RVA: 0x00040F71 File Offset: 0x0003F171
		// (remove) Token: 0x06000A51 RID: 2641 RVA: 0x00040F80 File Offset: 0x0003F180
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

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000A52 RID: 2642 RVA: 0x00040F8F File Offset: 0x0003F18F
		// (remove) Token: 0x06000A53 RID: 2643 RVA: 0x00040F9E File Offset: 0x0003F19E
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

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000A54 RID: 2644 RVA: 0x00040FAD File Offset: 0x0003F1AD
		// (remove) Token: 0x06000A55 RID: 2645 RVA: 0x00040FBC File Offset: 0x0003F1BC
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

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000A56 RID: 2646 RVA: 0x00040FCB File Offset: 0x0003F1CB
		// (remove) Token: 0x06000A57 RID: 2647 RVA: 0x00040FDA File Offset: 0x0003F1DA
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

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000A58 RID: 2648 RVA: 0x00040FE9 File Offset: 0x0003F1E9
		// (remove) Token: 0x06000A59 RID: 2649 RVA: 0x00040FF8 File Offset: 0x0003F1F8
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

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000A5A RID: 2650 RVA: 0x00041007 File Offset: 0x0003F207
		// (remove) Token: 0x06000A5B RID: 2651 RVA: 0x00041016 File Offset: 0x0003F216
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

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000A5C RID: 2652 RVA: 0x00041025 File Offset: 0x0003F225
		// (remove) Token: 0x06000A5D RID: 2653 RVA: 0x00041034 File Offset: 0x0003F234
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

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000A5E RID: 2654 RVA: 0x00041043 File Offset: 0x0003F243
		// (remove) Token: 0x06000A5F RID: 2655 RVA: 0x00041052 File Offset: 0x0003F252
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

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000A60 RID: 2656 RVA: 0x00041061 File Offset: 0x0003F261
		// (remove) Token: 0x06000A61 RID: 2657 RVA: 0x00041070 File Offset: 0x0003F270
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

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06000A62 RID: 2658 RVA: 0x0004107F File Offset: 0x0003F27F
		// (remove) Token: 0x06000A63 RID: 2659 RVA: 0x0004108E File Offset: 0x0003F28E
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

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06000A64 RID: 2660 RVA: 0x0004109D File Offset: 0x0003F29D
		// (remove) Token: 0x06000A65 RID: 2661 RVA: 0x000410AC File Offset: 0x0003F2AC
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

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06000A66 RID: 2662 RVA: 0x000410BB File Offset: 0x0003F2BB
		// (remove) Token: 0x06000A67 RID: 2663 RVA: 0x000410CA File Offset: 0x0003F2CA
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

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06000A68 RID: 2664 RVA: 0x000410D9 File Offset: 0x0003F2D9
		// (remove) Token: 0x06000A69 RID: 2665 RVA: 0x000410E8 File Offset: 0x0003F2E8
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

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06000A6A RID: 2666 RVA: 0x000410F7 File Offset: 0x0003F2F7
		// (remove) Token: 0x06000A6B RID: 2667 RVA: 0x00041106 File Offset: 0x0003F306
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

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06000A6C RID: 2668 RVA: 0x00041115 File Offset: 0x0003F315
		// (remove) Token: 0x06000A6D RID: 2669 RVA: 0x00041124 File Offset: 0x0003F324
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

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06000A6E RID: 2670 RVA: 0x00041133 File Offset: 0x0003F333
		// (remove) Token: 0x06000A6F RID: 2671 RVA: 0x00041142 File Offset: 0x0003F342
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

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06000A70 RID: 2672 RVA: 0x00041151 File Offset: 0x0003F351
		// (remove) Token: 0x06000A71 RID: 2673 RVA: 0x00041160 File Offset: 0x0003F360
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

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06000A72 RID: 2674 RVA: 0x0004116F File Offset: 0x0003F36F
		// (remove) Token: 0x06000A73 RID: 2675 RVA: 0x0004117E File Offset: 0x0003F37E
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

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06000A74 RID: 2676 RVA: 0x0004118D File Offset: 0x0003F38D
		// (remove) Token: 0x06000A75 RID: 2677 RVA: 0x0004119C File Offset: 0x0003F39C
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

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06000A76 RID: 2678 RVA: 0x000411AB File Offset: 0x0003F3AB
		// (remove) Token: 0x06000A77 RID: 2679 RVA: 0x000411BA File Offset: 0x0003F3BA
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

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06000A78 RID: 2680 RVA: 0x000411C9 File Offset: 0x0003F3C9
		// (remove) Token: 0x06000A79 RID: 2681 RVA: 0x000411D8 File Offset: 0x0003F3D8
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

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06000A7A RID: 2682 RVA: 0x000411E7 File Offset: 0x0003F3E7
		// (remove) Token: 0x06000A7B RID: 2683 RVA: 0x000411F6 File Offset: 0x0003F3F6
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

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000A7C RID: 2684 RVA: 0x00041205 File Offset: 0x0003F405
		// (remove) Token: 0x06000A7D RID: 2685 RVA: 0x00041214 File Offset: 0x0003F414
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

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000A7E RID: 2686 RVA: 0x00041223 File Offset: 0x0003F423
		// (remove) Token: 0x06000A7F RID: 2687 RVA: 0x00041232 File Offset: 0x0003F432
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

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000A80 RID: 2688 RVA: 0x00041241 File Offset: 0x0003F441
		// (remove) Token: 0x06000A81 RID: 2689 RVA: 0x00041250 File Offset: 0x0003F450
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

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000A82 RID: 2690 RVA: 0x0004125F File Offset: 0x0003F45F
		// (remove) Token: 0x06000A83 RID: 2691 RVA: 0x0004126E File Offset: 0x0003F46E
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

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06000A84 RID: 2692 RVA: 0x0004127D File Offset: 0x0003F47D
		// (remove) Token: 0x06000A85 RID: 2693 RVA: 0x0004128C File Offset: 0x0003F48C
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

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06000A86 RID: 2694 RVA: 0x0004129B File Offset: 0x0003F49B
		// (remove) Token: 0x06000A87 RID: 2695 RVA: 0x000412AA File Offset: 0x0003F4AA
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

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06000A88 RID: 2696 RVA: 0x000412B9 File Offset: 0x0003F4B9
		// (remove) Token: 0x06000A89 RID: 2697 RVA: 0x000412C8 File Offset: 0x0003F4C8
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

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000A8A RID: 2698 RVA: 0x000412D7 File Offset: 0x0003F4D7
		// (remove) Token: 0x06000A8B RID: 2699 RVA: 0x000412E6 File Offset: 0x0003F4E6
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

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06000A8C RID: 2700 RVA: 0x000412F5 File Offset: 0x0003F4F5
		// (remove) Token: 0x06000A8D RID: 2701 RVA: 0x00041304 File Offset: 0x0003F504
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

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06000A8E RID: 2702 RVA: 0x00041313 File Offset: 0x0003F513
		// (remove) Token: 0x06000A8F RID: 2703 RVA: 0x00041322 File Offset: 0x0003F522
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

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06000A90 RID: 2704 RVA: 0x00041331 File Offset: 0x0003F531
		// (remove) Token: 0x06000A91 RID: 2705 RVA: 0x00041340 File Offset: 0x0003F540
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

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06000A92 RID: 2706 RVA: 0x0004134F File Offset: 0x0003F54F
		// (remove) Token: 0x06000A93 RID: 2707 RVA: 0x0004135E File Offset: 0x0003F55E
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

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06000A94 RID: 2708 RVA: 0x0004136D File Offset: 0x0003F56D
		// (remove) Token: 0x06000A95 RID: 2709 RVA: 0x0004137C File Offset: 0x0003F57C
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

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06000A96 RID: 2710 RVA: 0x0004138B File Offset: 0x0003F58B
		// (remove) Token: 0x06000A97 RID: 2711 RVA: 0x0004139A File Offset: 0x0003F59A
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

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06000A98 RID: 2712 RVA: 0x000413A9 File Offset: 0x0003F5A9
		// (remove) Token: 0x06000A99 RID: 2713 RVA: 0x000413B8 File Offset: 0x0003F5B8
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

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06000A9A RID: 2714 RVA: 0x000413C7 File Offset: 0x0003F5C7
		// (remove) Token: 0x06000A9B RID: 2715 RVA: 0x000413D6 File Offset: 0x0003F5D6
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

		// Token: 0x06000A9C RID: 2716 RVA: 0x000413E8 File Offset: 0x0003F5E8
		internal Document(IBrowsingContext context, TextSource source)
			: base(null, "#document", NodeType.Document, NodeFlags.None)
		{
			this.Referrer = string.Empty;
			this.ContentType = MimeTypeNames.ApplicationXml;
			this._attachedReferences = new List<WeakReference>();
			this._async = true;
			this._designMode = false;
			this._firedUnload = false;
			this._salvageable = true;
			this._shown = false;
			this._context = context;
			this._source = source;
			this._ready = DocumentReadyState.Loading;
			this._sandbox = Sandboxes.None;
			this._quirksMode = QuirksMode.Off;
			this._loadingScripts = new Queue<HtmlScriptElement>();
			this._location = new Location("about:blank");
			this._location.Changed += this.LocationChanged;
			this._view = new Window(this);
			this._loader = context.CreateService<IResourceLoader>();
			this._loop = context.CreateService<IEventLoop>();
			this._mutations = new MutationHost(this._loop);
			this._statusCode = HttpStatusCode.OK;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x000414DD File Offset: 0x0003F6DD
		public TextSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x000414E5 File Offset: 0x0003F6E5
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x000414ED File Offset: 0x0003F6ED
		public IDocument ImportAncestor { get; private set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x000414F6 File Offset: 0x0003F6F6
		public IEventLoop Loop
		{
			get
			{
				return this._loop;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x000414FE File Offset: 0x0003F6FE
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x00041513 File Offset: 0x0003F713
		public string DesignMode
		{
			get
			{
				if (!this._designMode)
				{
					return Keywords.Off;
				}
				return Keywords.On;
			}
			set
			{
				this._designMode = value.Isi(Keywords.On);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x00041528 File Offset: 0x0003F728
		public IHtmlAllCollection All
		{
			get
			{
				HtmlAllCollection htmlAllCollection;
				if ((htmlAllCollection = this._all) == null)
				{
					htmlAllCollection = (this._all = new HtmlAllCollection(this));
				}
				return htmlAllCollection;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00041550 File Offset: 0x0003F750
		public IHtmlCollection<IHtmlAnchorElement> Anchors
		{
			get
			{
				HtmlCollection<IHtmlAnchorElement> htmlCollection;
				if ((htmlCollection = this._anchors) == null)
				{
					htmlCollection = (this._anchors = new HtmlCollection<IHtmlAnchorElement>(this, true, new Predicate<IHtmlAnchorElement>(Document.IsAnchor)));
				}
				return htmlCollection;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x00041583 File Offset: 0x0003F783
		public int ChildElementCount
		{
			get
			{
				return base.ChildNodes.OfType<Element>().Count<Element>();
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00041598 File Offset: 0x0003F798
		public IHtmlCollection<IElement> Children
		{
			get
			{
				HtmlCollection<IElement> htmlCollection;
				if ((htmlCollection = this._children) == null)
				{
					htmlCollection = (this._children = new HtmlCollection<IElement>(base.ChildNodes.OfType<Element>()));
				}
				return htmlCollection;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x000415C8 File Offset: 0x0003F7C8
		public IElement FirstElementChild
		{
			get
			{
				NodeList childNodes = base.ChildNodes;
				int length = childNodes.Length;
				for (int i = 0; i < length; i++)
				{
					IElement element = childNodes[i] as IElement;
					if (element != null)
					{
						return element;
					}
				}
				return null;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00041604 File Offset: 0x0003F804
		public IElement LastElementChild
		{
			get
			{
				NodeList childNodes = base.ChildNodes;
				for (int i = childNodes.Length - 1; i >= 0; i--)
				{
					IElement element = childNodes[i] as IElement;
					if (element != null)
					{
						return element;
					}
				}
				return null;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0004163E File Offset: 0x0003F83E
		public bool IsAsync
		{
			get
			{
				return this._async;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00041646 File Offset: 0x0003F846
		public IHtmlScriptElement CurrentScript
		{
			get
			{
				if (this._loadingScripts.Count <= 0)
				{
					return null;
				}
				return this._loadingScripts.Peek();
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00041664 File Offset: 0x0003F864
		public IImplementation Implementation
		{
			get
			{
				DomImplementation domImplementation;
				if ((domImplementation = this._implementation) == null)
				{
					domImplementation = (this._implementation = new DomImplementation(this));
				}
				return domImplementation;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0004168A File Offset: 0x0003F88A
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x00041692 File Offset: 0x0003F892
		public string LastModified { get; protected set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0004169B File Offset: 0x0003F89B
		public IDocumentType Doctype
		{
			get
			{
				return this.FindChild<DocumentType>();
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x000416A3 File Offset: 0x0003F8A3
		// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x000416AB File Offset: 0x0003F8AB
		public string ContentType { get; protected set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x000416B4 File Offset: 0x0003F8B4
		// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x000416BC File Offset: 0x0003F8BC
		public DocumentReadyState ReadyState
		{
			get
			{
				return this._ready;
			}
			protected set
			{
				this._ready = value;
				this.FireSimpleEvent(EventNames.ReadyStateChanged, false, false);
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x000416D4 File Offset: 0x0003F8D4
		public IStyleSheetList StyleSheets
		{
			get
			{
				IStyleSheetList styleSheetList;
				if ((styleSheetList = this._styleSheets) == null)
				{
					styleSheetList = (this._styleSheets = this.CreateStyleSheets());
				}
				return styleSheetList;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x000416FC File Offset: 0x0003F8FC
		public IStringList StyleSheetSets
		{
			get
			{
				IStringList stringList;
				if ((stringList = this._styleSheetSets) == null)
				{
					stringList = (this._styleSheetSets = this.CreateStyleSheetSets());
				}
				return stringList;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00041722 File Offset: 0x0003F922
		// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x0004172A File Offset: 0x0003F92A
		public string Referrer { get; protected set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x00041733 File Offset: 0x0003F933
		public ILocation Location
		{
			get
			{
				return this._location;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0004173B File Offset: 0x0003F93B
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x00041748 File Offset: 0x0003F948
		public string DocumentUri
		{
			get
			{
				return this._location.Href;
			}
			protected set
			{
				this._location.Changed -= this.LocationChanged;
				this._location.Href = value;
				this._location.Changed += this.LocationChanged;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00041784 File Offset: 0x0003F984
		public Url DocumentUrl
		{
			get
			{
				return this._location.Original;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00041791 File Offset: 0x0003F991
		public IWindow DefaultView
		{
			get
			{
				return this._view;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00041799 File Offset: 0x0003F999
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x000417B6 File Offset: 0x0003F9B6
		public string Direction
		{
			get
			{
				return ((this.DocumentElement as IHtmlElement) ?? new HtmlHtmlElement(this, null)).Direction;
			}
			set
			{
				((this.DocumentElement as IHtmlElement) ?? new HtmlHtmlElement(this, null)).Direction = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x000417D4 File Offset: 0x0003F9D4
		public string CharacterSet
		{
			get
			{
				return this._source.CurrentEncoding.WebName;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000ABF RID: 2751
		public abstract IElement DocumentElement { get; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x000417E6 File Offset: 0x0003F9E6
		public IElement ActiveElement
		{
			get
			{
				return this.All.Where((IElement m) => m.IsFocused).FirstOrDefault<IElement>();
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00041817 File Offset: 0x0003FA17
		public string CompatMode
		{
			get
			{
				return this._quirksMode.GetCompatiblity();
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0004173B File Offset: 0x0003F93B
		public string Url
		{
			get
			{
				return this._location.Href;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00041824 File Offset: 0x0003FA24
		public IHtmlCollection<IHtmlFormElement> Forms
		{
			get
			{
				return new HtmlCollection<IHtmlFormElement>(this, true, null);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00041830 File Offset: 0x0003FA30
		public IHtmlCollection<IHtmlImageElement> Images
		{
			get
			{
				HtmlCollection<IHtmlImageElement> htmlCollection;
				if ((htmlCollection = this._images) == null)
				{
					htmlCollection = (this._images = new HtmlCollection<IHtmlImageElement>(this, true, null));
				}
				return htmlCollection;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00041858 File Offset: 0x0003FA58
		public IHtmlCollection<IHtmlScriptElement> Scripts
		{
			get
			{
				HtmlCollection<IHtmlScriptElement> htmlCollection;
				if ((htmlCollection = this._scripts) == null)
				{
					htmlCollection = (this._scripts = new HtmlCollection<IHtmlScriptElement>(this, true, null));
				}
				return htmlCollection;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00041880 File Offset: 0x0003FA80
		public IHtmlCollection<IHtmlEmbedElement> Plugins
		{
			get
			{
				HtmlCollection<IHtmlEmbedElement> htmlCollection;
				if ((htmlCollection = this._plugins) == null)
				{
					htmlCollection = (this._plugins = new HtmlCollection<IHtmlEmbedElement>(this, true, null));
				}
				return htmlCollection;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x000418A8 File Offset: 0x0003FAA8
		public IHtmlCollection<IElement> Commands
		{
			get
			{
				HtmlCollection<IElement> htmlCollection;
				if ((htmlCollection = this._commands) == null)
				{
					htmlCollection = (this._commands = new HtmlCollection<IElement>(this, true, new Predicate<IElement>(Document.IsCommand)));
				}
				return htmlCollection;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x000418DC File Offset: 0x0003FADC
		public IHtmlCollection<IElement> Links
		{
			get
			{
				HtmlCollection<IElement> htmlCollection;
				if ((htmlCollection = this._links) == null)
				{
					htmlCollection = (this._links = new HtmlCollection<IElement>(this, true, new Predicate<IElement>(Document.IsLink)));
				}
				return htmlCollection;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0004190F File Offset: 0x0003FB0F
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x00041917 File Offset: 0x0003FB17
		public string Title
		{
			get
			{
				return this.GetTitle();
			}
			set
			{
				this.SetTitle(value);
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00041920 File Offset: 0x0003FB20
		public IHtmlHeadElement Head
		{
			get
			{
				return this.DocumentElement.FindChild<IHtmlHeadElement>();
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00041930 File Offset: 0x0003FB30
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x000419A8 File Offset: 0x0003FBA8
		public IHtmlElement Body
		{
			get
			{
				IElement documentElement = this.DocumentElement;
				if (documentElement != null)
				{
					foreach (INode node in documentElement.ChildNodes)
					{
						HtmlBodyElement htmlBodyElement = node as HtmlBodyElement;
						if (htmlBodyElement != null)
						{
							return htmlBodyElement;
						}
						HtmlFrameSetElement htmlFrameSetElement = node as HtmlFrameSetElement;
						if (htmlFrameSetElement != null)
						{
							return htmlFrameSetElement;
						}
					}
				}
				return null;
			}
			set
			{
				if (!(value is IHtmlBodyElement) && !(value is HtmlFrameSetElement))
				{
					throw new DomException(DomError.HierarchyRequest);
				}
				IHtmlElement body = this.Body;
				if (body != value)
				{
					if (body == null)
					{
						IElement documentElement = this.DocumentElement;
						if (documentElement == null)
						{
							throw new DomException(DomError.HierarchyRequest);
						}
						documentElement.AppendChild(value);
						return;
					}
					else
					{
						base.ReplaceChild(value, body);
					}
				}
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x000419FB File Offset: 0x0003FBFB
		public IBrowsingContext Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00041A03 File Offset: 0x0003FC03
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x00041A0B File Offset: 0x0003FC0B
		public HttpStatusCode StatusCode
		{
			get
			{
				return this._statusCode;
			}
			private set
			{
				this._statusCode = value;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00041A14 File Offset: 0x0003FC14
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x00041A2C File Offset: 0x0003FC2C
		public string Cookie
		{
			get
			{
				return this.Options.GetCookie(this._location.Origin);
			}
			set
			{
				this.Options.SetCookie(this._location.Origin, value);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00041A45 File Offset: 0x0003FC45
		// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x00041A6A File Offset: 0x0003FC6A
		public string Domain
		{
			get
			{
				if (!string.IsNullOrEmpty(this.DocumentUri))
				{
					return new Uri(this.DocumentUri).Host;
				}
				return string.Empty;
			}
			set
			{
				if (this._location == null)
				{
					return;
				}
				this._location.Host = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x00041A81 File Offset: 0x0003FC81
		public string Origin
		{
			get
			{
				return this._location.Origin;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x00041A90 File Offset: 0x0003FC90
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x00041B19 File Offset: 0x0003FD19
		public string SelectedStyleSheetSet
		{
			get
			{
				IEnumerable<string> enabledStyleSheetSets = this.StyleSheets.GetEnabledStyleSheetSets();
				string enabledName = enabledStyleSheetSets.FirstOrDefault<string>();
				IEnumerable<IStyleSheet> enumerable = this.StyleSheets.Where((IStyleSheet m) => !string.IsNullOrEmpty(m.Title) && !m.IsDisabled);
				if (enabledStyleSheetSets.Count<string>() == 1 && !enumerable.Any((IStyleSheet m) => !m.Title.Is(enabledName)))
				{
					return enabledName;
				}
				if (enumerable.Any<IStyleSheet>())
				{
					return null;
				}
				return string.Empty;
			}
			set
			{
				if (value != null)
				{
					this.StyleSheets.EnableStyleSheetSet(value);
					this.LastStyleSheetSet = value;
				}
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00041B31 File Offset: 0x0003FD31
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x00041B39 File Offset: 0x0003FD39
		public string LastStyleSheetSet { get; private set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00041B44 File Offset: 0x0003FD44
		public string PreferredStyleSheetSet
		{
			get
			{
				return (from m in this.All.OfType<IHtmlLinkElement>()
					where m.IsPreferred()
					select m.Title).FirstOrDefault<string>();
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00041BA9 File Offset: 0x0003FDA9
		public bool IsReady
		{
			get
			{
				return this.ReadyState == DocumentReadyState.Complete;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00041BB4 File Offset: 0x0003FDB4
		public bool IsLoading
		{
			get
			{
				return this.ReadyState == DocumentReadyState.Loading;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00041BBF File Offset: 0x0003FDBF
		internal MutationHost Mutations
		{
			get
			{
				return this._mutations;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00041BC7 File Offset: 0x0003FDC7
		internal IConfiguration Options
		{
			get
			{
				return this._context.Configuration;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00041BD4 File Offset: 0x0003FDD4
		// (set) Token: 0x06000AE0 RID: 2784 RVA: 0x00041BDC File Offset: 0x0003FDDC
		internal QuirksMode QuirksMode
		{
			get
			{
				return this._quirksMode;
			}
			set
			{
				this._quirksMode = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00041BE5 File Offset: 0x0003FDE5
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x00041BED File Offset: 0x0003FDED
		internal Sandboxes ActiveSandboxing
		{
			get
			{
				return this._sandbox;
			}
			set
			{
				this._sandbox = value;
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00041BF6 File Offset: 0x0003FDF6
		internal void AddScript(HtmlScriptElement script)
		{
			this._loadingScripts.Enqueue(script);
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00041C04 File Offset: 0x0003FE04
		internal bool IsInBrowsingContext
		{
			get
			{
				return this._context.Active != null;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0000EE9F File Offset: 0x0000D09F
		internal bool IsToBePrinted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00041C14 File Offset: 0x0003FE14
		internal IElement FocusElement
		{
			get
			{
				return this._focus;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00041C1C File Offset: 0x0003FE1C
		internal IResourceLoader Loader
		{
			get
			{
				return this._loader;
			}
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00041C24 File Offset: 0x0003FE24
		public void Dispose()
		{
			base.ReplaceAll(null, true);
			this._loop.CancelAll();
			this._loadingScripts.Clear();
			this._source.Dispose();
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00041C4F File Offset: 0x0003FE4F
		public void EnableStyleSheetsForSet(string name)
		{
			if (name != null)
			{
				this.StyleSheets.EnableStyleSheetSet(name);
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00041C60 File Offset: 0x0003FE60
		public IDocument Open(string type = "text/html", string replace = null)
		{
			if (!this.ContentType.Is(MimeTypeNames.Html))
			{
				throw new DomException(DomError.InvalidState);
			}
			if (this.IsInBrowsingContext && this._context.Active != this)
			{
				return null;
			}
			IBrowsingContext context = this._context;
			IDocument document = ((context != null) ? context.Parent.Active : null);
			if (document != null && !document.Origin.Is(this.Origin))
			{
				throw new DomException(DomError.Security);
			}
			if (!this._firedUnload && this._loadingScripts.Count == 0)
			{
				bool flag = replace.Isi(Keywords.Replace);
				IHistory sessionHistory = this._context.SessionHistory;
				int num = ((type != null) ? type.IndexOf(';') : (-1));
				if (!flag && sessionHistory != null)
				{
					if (sessionHistory.Length == 1)
					{
						sessionHistory[0].Url.Is("about:blank");
					}
				}
				this._salvageable = false;
				if (!this.PromptToUnloadAsync().Result)
				{
					return this;
				}
				this.Unload(true);
				this.Abort(false);
				base.RemoveEventListeners();
				foreach (Element element in this.Descendents<Element>())
				{
					element.RemoveEventListeners();
				}
				this._loop.CancelAll();
				base.ReplaceAll(null, true);
				this._source.CurrentEncoding = TextEncoding.Utf8;
				this._salvageable = true;
				this._ready = DocumentReadyState.Loading;
				if (type.Isi(Keywords.Replace))
				{
					type = MimeTypeNames.Html;
				}
				else if (num >= 0)
				{
					type = type.Substring(0, num);
				}
				type = type.StripLeadingTrailingSpaces();
				type.Isi(MimeTypeNames.Html);
				this.ContentType = type;
				this._firedUnload = false;
				this._source.Index = this._source.Length;
			}
			return this;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00041E38 File Offset: 0x00040038
		public void Load(string url)
		{
			this.Location.Href = url;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00041E46 File Offset: 0x00040046
		void IDocument.Close()
		{
			if (this.IsLoading)
			{
				this.FinishLoadingAsync().Wait();
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00041E5C File Offset: 0x0004005C
		public void Write(string content)
		{
			if (this.IsReady)
			{
				string text = content ?? string.Empty;
				this.Open("text/html", null).Write(text);
				return;
			}
			this._source.InsertText(content);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00041E9B File Offset: 0x0004009B
		public void WriteLine(string content)
		{
			this.Write(content + "\n");
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00041EB0 File Offset: 0x000400B0
		public IHtmlCollection<IElement> GetElementsByName(string name)
		{
			List<IElement> list = new List<IElement>();
			base.ChildNodes.GetElementsByName(name, list);
			return new HtmlCollection<IElement>(list);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00041ED6 File Offset: 0x000400D6
		public INode Import(INode externalNode, bool deep = true)
		{
			if (externalNode.NodeType == NodeType.Document)
			{
				throw new DomException(DomError.NotSupported);
			}
			return externalNode.Clone(deep);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00041EF1 File Offset: 0x000400F1
		public INode Adopt(INode externalNode)
		{
			if (externalNode.NodeType == NodeType.Document)
			{
				throw new DomException(DomError.NotSupported);
			}
			this.AdoptNode(externalNode);
			return externalNode;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00041F0D File Offset: 0x0004010D
		public Event CreateEvent(string type)
		{
			Event @event = this.Options.GetFactory<IEventFactory>().Create(type);
			if (@event == null)
			{
				throw new DomException(DomError.NotSupported);
			}
			return @event;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00041F2B File Offset: 0x0004012B
		public INodeIterator CreateNodeIterator(INode root, FilterSettings settings = FilterSettings.All, NodeFilter filter = null)
		{
			return new NodeIterator(root, settings, filter);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00041F35 File Offset: 0x00040135
		public ITreeWalker CreateTreeWalker(INode root, FilterSettings settings = FilterSettings.All, NodeFilter filter = null)
		{
			return new TreeWalker(root, settings, filter);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00041F40 File Offset: 0x00040140
		public IRange CreateRange()
		{
			Range range = new Range(this);
			this.AttachReference(range);
			return range;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00041F5C File Offset: 0x0004015C
		public void Prepend(params INode[] nodes)
		{
			this.PrependNodes(nodes);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00041F65 File Offset: 0x00040165
		public void Append(params INode[] nodes)
		{
			this.AppendNodes(nodes);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00041F6E File Offset: 0x0004016E
		public IElement CreateElement(string localName)
		{
			if (localName.IsXmlName())
			{
				HtmlElement htmlElement = this.Options.GetFactory<IElementFactory<HtmlElement>>().Create(this, localName, null);
				htmlElement.SetupElement();
				return htmlElement;
			}
			throw new DomException(DomError.InvalidCharacter);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00041F98 File Offset: 0x00040198
		public IElement CreateElement(string namespaceUri, string qualifiedName)
		{
			string text = null;
			string text2 = null;
			Node.GetPrefixAndLocalName(qualifiedName, ref namespaceUri, out text2, out text);
			if (namespaceUri.Is(NamespaceNames.HtmlUri))
			{
				HtmlElement htmlElement = this.Options.GetFactory<IElementFactory<HtmlElement>>().Create(this, text, text2);
				htmlElement.SetupElement();
				return htmlElement;
			}
			if (namespaceUri.Is(NamespaceNames.SvgUri))
			{
				SvgElement svgElement = this.Options.GetFactory<IElementFactory<SvgElement>>().Create(this, text, text2);
				svgElement.SetupElement();
				return svgElement;
			}
			if (namespaceUri.Is(NamespaceNames.MathMlUri))
			{
				MathElement mathElement = this.Options.GetFactory<IElementFactory<MathElement>>().Create(this, text, text2);
				mathElement.SetupElement();
				return mathElement;
			}
			Element element = new Element(this, text, text2, namespaceUri, NodeFlags.None);
			element.SetupElement();
			return element;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0004203A File Offset: 0x0004023A
		public IComment CreateComment(string data)
		{
			return new Comment(this, data);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00042043 File Offset: 0x00040243
		public IDocumentFragment CreateDocumentFragment()
		{
			return new DocumentFragment(this);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0004204B File Offset: 0x0004024B
		public IProcessingInstruction CreateProcessingInstruction(string target, string data)
		{
			if (!target.IsXmlName() || data.Contains("?>"))
			{
				throw new DomException(DomError.InvalidCharacter);
			}
			return new ProcessingInstruction(this, target)
			{
				Data = data
			};
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00042077 File Offset: 0x00040277
		public IText CreateTextNode(string data)
		{
			return new TextNode(this, data);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00042080 File Offset: 0x00040280
		public IElement GetElementById(string elementId)
		{
			return base.ChildNodes.GetElementById(elementId);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0004208E File Offset: 0x0004028E
		public IElement QuerySelector(string selectors)
		{
			return base.ChildNodes.QuerySelector(selectors);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0004209C File Offset: 0x0004029C
		public IHtmlCollection<IElement> QuerySelectorAll(string selectors)
		{
			return base.ChildNodes.QuerySelectorAll(selectors);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000420AA File Offset: 0x000402AA
		public IHtmlCollection<IElement> GetElementsByClassName(string classNames)
		{
			return base.ChildNodes.GetElementsByClassName(classNames);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000420B8 File Offset: 0x000402B8
		public IHtmlCollection<IElement> GetElementsByTagName(string tagName)
		{
			return base.ChildNodes.GetElementsByTagName(tagName);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000420C6 File Offset: 0x000402C6
		public IHtmlCollection<IElement> GetElementsByTagName(string namespaceURI, string tagName)
		{
			return base.ChildNodes.GetElementsByTagName(namespaceURI, tagName);
		}

		// Token: 0x06000B04 RID: 2820
		public abstract override INode Clone(bool deep = true);

		// Token: 0x06000B05 RID: 2821 RVA: 0x000420D5 File Offset: 0x000402D5
		public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
		{
			base.ChildNodes.ToHtml(writer, formatter);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x000420E4 File Offset: 0x000402E4
		public bool HasFocus()
		{
			return this._context.Active == this;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000420F4 File Offset: 0x000402F4
		public IAttr CreateAttribute(string localName)
		{
			if (!localName.IsXmlName())
			{
				throw new DomException(DomError.InvalidCharacter);
			}
			return new Attr(localName);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0004210C File Offset: 0x0004030C
		public IAttr CreateAttribute(string namespaceUri, string qualifiedName)
		{
			string text = null;
			string text2 = null;
			Node.GetPrefixAndLocalName(qualifiedName, ref namespaceUri, out text2, out text);
			return new Attr(text2, text, string.Empty, namespaceUri);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00042138 File Offset: 0x00040338
		internal IEnumerable<T> GetAttachedReferences<T>() where T : class
		{
			return from m in this._attachedReferences.Select(delegate(WeakReference entry)
				{
					if (!entry.IsAlive)
					{
						return default(T);
					}
					return entry.Target as T;
				})
				where m != null
				select m;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00042193 File Offset: 0x00040393
		internal void AttachReference(object value)
		{
			this._attachedReferences.Add(new WeakReference(value));
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000421A6 File Offset: 0x000403A6
		internal void DelayLoad(Task task)
		{
			if (!this.IsReady && task != null && !task.IsCompleted)
			{
				this.AttachReference(task);
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000421C2 File Offset: 0x000403C2
		internal void SetFocus(IElement element)
		{
			this._focus = element;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x000421CC File Offset: 0x000403CC
		internal async Task FinishLoadingAsync()
		{
			Task[] tasks = this.GetAttachedReferences<Task>().ToArray<Task>();
			this.ReadyState = DocumentReadyState.Interactive;
			while (this._loadingScripts.Count > 0)
			{
				await this.WaitForReadyAsync().ConfigureAwait(false);
				await this._loadingScripts.Dequeue().RunAsync(CancellationToken.None).ConfigureAwait(false);
			}
			this.QueueTask(new Action(this.RaiseDomContentLoaded));
			await TaskEx.WhenAll(tasks).ConfigureAwait(false);
			this.QueueTask(new Action(this.RaiseLoadedEvent));
			if (this.IsInBrowsingContext)
			{
				this.QueueTask(new Action(this.ShowPage));
			}
			this.QueueTask(new Action(this.EmptyAppCache));
			if (this.IsToBePrinted)
			{
				await this.PrintAsync().ConfigureAwait(false);
			}
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00042214 File Offset: 0x00040414
		internal async Task<bool> PromptToUnloadAsync()
		{
			IEnumerable<IBrowsingContext> descendants = this.GetAttachedReferences<IBrowsingContext>();
			if (this._view.HasEventListener(EventNames.BeforeUnload))
			{
				Event @event = new Event(EventNames.BeforeUnload, false, true);
				bool flag = this._view.Fire(@event);
				this._salvageable = false;
				if (flag)
				{
					var data = new
					{
						Document = this,
						IsCancelled = true
					};
					await this._context.FireAsync(EventNames.ConfirmUnload, data).ConfigureAwait(false);
					if (data.IsCancelled)
					{
						return false;
					}
					data = null;
				}
			}
			foreach (IBrowsingContext browsingContext in descendants)
			{
				Document active = browsingContext.Active as Document;
				if (active != null)
				{
					if (!(await active.PromptToUnloadAsync().ConfigureAwait(false)))
					{
						return false;
					}
					this._salvageable = this._salvageable && active._salvageable;
				}
				active = null;
			}
			IEnumerator<IBrowsingContext> enumerator = null;
			return true;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0004225C File Offset: 0x0004045C
		internal void Unload(bool recycle)
		{
			IEnumerable<IBrowsingContext> attachedReferences = this.GetAttachedReferences<IBrowsingContext>();
			if (this._shown)
			{
				this._shown = false;
				this.Fire(delegate(PageTransitionEvent ev)
				{
					ev.Init(EventNames.PageHide, false, false, this._salvageable);
				}, this._view);
			}
			if (this._view.HasEventListener(EventNames.Unload))
			{
				if (!this._firedUnload)
				{
					this._view.FireSimpleEvent(EventNames.Unload, false, false);
					this._firedUnload = true;
				}
				this._salvageable = false;
			}
			this.CancelTasks();
			foreach (IBrowsingContext browsingContext in attachedReferences)
			{
				Document document = browsingContext.Active as Document;
				if (document != null)
				{
					document.Unload(false);
					this._salvageable = this._salvageable && document._salvageable;
				}
			}
			if (!recycle && !this._salvageable && this._context.Active == this)
			{
				this._context.Active = null;
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0004235C File Offset: 0x0004055C
		bool IDocument.ExecuteCommand(string commandId, bool showUserInterface, string value)
		{
			ICommand command = this.Options.GetCommand(commandId);
			return command != null && command.Execute(this, showUserInterface, value);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00042378 File Offset: 0x00040578
		bool IDocument.IsCommandEnabled(string commandId)
		{
			ICommand command = this.Options.GetCommand(commandId);
			return command != null && command.IsEnabled(this);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00042392 File Offset: 0x00040592
		bool IDocument.IsCommandIndeterminate(string commandId)
		{
			ICommand command = this.Options.GetCommand(commandId);
			return command != null && command.IsIndeterminate(this);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x000423AC File Offset: 0x000405AC
		bool IDocument.IsCommandExecuted(string commandId)
		{
			ICommand command = this.Options.GetCommand(commandId);
			return command != null && command.IsExecuted(this);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000423C6 File Offset: 0x000405C6
		bool IDocument.IsCommandSupported(string commandId)
		{
			ICommand command = this.Options.GetCommand(commandId);
			return command != null && command.IsSupported(this);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000423E0 File Offset: 0x000405E0
		string IDocument.GetCommandValue(string commandId)
		{
			ICommand command = this.Options.GetCommand(commandId);
			if (command == null)
			{
				return null;
			}
			return command.GetValue(this);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x000423FC File Offset: 0x000405FC
		private void Abort(bool fromUser = false)
		{
			if (fromUser && this._context.Active == this)
			{
				this.QueueTask(delegate
				{
					this._view.FireSimpleEvent(EventNames.Abort, false, false);
				});
			}
			foreach (IBrowsingContext browsingContext in this.GetAttachedReferences<IBrowsingContext>())
			{
				Document document = browsingContext.Active as Document;
				if (document != null)
				{
					document.Abort(false);
					this._salvageable = this._salvageable && document._salvageable;
				}
			}
			foreach (IDownload download in from m in this._loader.GetDownloads()
				where !m.IsCompleted
				select m)
			{
				download.Cancel();
				this._salvageable = false;
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000424FC File Offset: 0x000406FC
		private void CancelTasks()
		{
			foreach (CancellationTokenSource cancellationTokenSource in this.GetAttachedReferences<CancellationTokenSource>())
			{
				if (!cancellationTokenSource.IsCancellationRequested)
				{
					cancellationTokenSource.Cancel();
				}
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00042550 File Offset: 0x00040750
		private static bool IsCommand(IElement element)
		{
			return element is IHtmlMenuItemElement || element is IHtmlButtonElement || element is IHtmlAnchorElement;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00042570 File Offset: 0x00040770
		private static bool IsLink(IElement element)
		{
			if (element is IHtmlAnchorElement || element is IHtmlAreaElement)
			{
				return element.Attributes.Any((IAttr m) => m.Name.Is(AttributeNames.Href));
			}
			return false;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x000425BF File Offset: 0x000407BF
		private static bool IsAnchor(IHtmlAnchorElement element)
		{
			return element.Attributes.Any((IAttr m) => m.Name.Is(AttributeNames.Name));
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x000425EB File Offset: 0x000407EB
		private void RaiseDomContentLoaded()
		{
			this.FireSimpleEvent(EventNames.DomContentLoaded, false, false);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000425FB File Offset: 0x000407FB
		private void RaiseLoadedEvent()
		{
			this.ReadyState = DocumentReadyState.Complete;
			this.FireSimpleEvent(EventNames.Load, false, false);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00003C25 File Offset: 0x00001E25
		private void EmptyAppCache()
		{
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00042614 File Offset: 0x00040814
		private async Task PrintAsync()
		{
			var <>f__AnonymousType = new
			{
				Document = this
			};
			this.FireSimpleEvent(EventNames.BeforePrint, false, false);
			await this._context.FireAsync(EventNames.Print, <>f__AnonymousType).ConfigureAwait(false);
			this.FireSimpleEvent(EventNames.AfterPrint, false, false);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00042659 File Offset: 0x00040859
		private void ShowPage()
		{
			if (!this._shown)
			{
				this._shown = true;
				this.Fire(delegate(PageTransitionEvent ev)
				{
					ev.Init(EventNames.PageShow, false, false, false);
				}, this._view);
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00042698 File Offset: 0x00040898
		private async void LocationChanged(object sender, Location.LocationChangedEventArgs e)
		{
			if (e.IsHashChanged)
			{
				HashChangedEvent hashChangedEvent = new HashChangedEvent();
				hashChangedEvent.Init(EventNames.HashChange, false, false, e.PreviousLocation, e.CurrentLocation);
				hashChangedEvent.IsTrusted = true;
				hashChangedEvent.Dispatch(this);
			}
			else
			{
				DocumentRequest documentRequest = DocumentRequest.Get(new Url(e.CurrentLocation), this, this.DocumentUri);
				await this._context.OpenAsync(documentRequest, CancellationToken.None);
			}
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000426DC File Offset: 0x000408DC
		protected void Setup(CreateDocumentOptions options)
		{
			this.ContentType = options.ContentType.Content;
			this.StatusCode = options.Response.StatusCode;
			this.Referrer = options.Response.Headers.GetOrDefault(HeaderNames.Referer, string.Empty);
			this.DocumentUri = options.Response.Address.Href;
			this.Cookie = options.Response.Headers.GetOrDefault(HeaderNames.SetCookie, string.Empty);
			this.ImportAncestor = options.ImportAncestor;
			this.ReadyState = DocumentReadyState.Loading;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00042774 File Offset: 0x00040974
		protected sealed override string LocateNamespace(string prefix)
		{
			IElement documentElement = this.DocumentElement;
			if (documentElement == null)
			{
				return null;
			}
			return documentElement.LocateNamespaceFor(prefix);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00042788 File Offset: 0x00040988
		protected sealed override string LocatePrefix(string namespaceUri)
		{
			IElement documentElement = this.DocumentElement;
			if (documentElement == null)
			{
				return null;
			}
			return documentElement.LocatePrefixFor(namespaceUri);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0004279C File Offset: 0x0004099C
		protected void CloneDocument(Document document, bool deep)
		{
			base.CloneNode(document, deep);
			document._ready = this._ready;
			document.Referrer = this.Referrer;
			document._location.Href = this._location.Href;
			document._quirksMode = this._quirksMode;
			document._sandbox = this._sandbox;
			document._async = this._async;
			document.ContentType = this.ContentType;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0004280F File Offset: 0x00040A0F
		protected virtual string GetTitle()
		{
			return string.Empty;
		}

		// Token: 0x06000B26 RID: 2854
		protected abstract void SetTitle(string value);

		// Token: 0x0400090E RID: 2318
		private readonly List<WeakReference> _attachedReferences;

		// Token: 0x0400090F RID: 2319
		private readonly Queue<HtmlScriptElement> _loadingScripts;

		// Token: 0x04000910 RID: 2320
		private readonly MutationHost _mutations;

		// Token: 0x04000911 RID: 2321
		private readonly IBrowsingContext _context;

		// Token: 0x04000912 RID: 2322
		private readonly IEventLoop _loop;

		// Token: 0x04000913 RID: 2323
		private readonly Window _view;

		// Token: 0x04000914 RID: 2324
		private readonly IResourceLoader _loader;

		// Token: 0x04000915 RID: 2325
		private readonly Location _location;

		// Token: 0x04000916 RID: 2326
		private readonly TextSource _source;

		// Token: 0x04000917 RID: 2327
		private QuirksMode _quirksMode;

		// Token: 0x04000918 RID: 2328
		private Sandboxes _sandbox;

		// Token: 0x04000919 RID: 2329
		private bool _async;

		// Token: 0x0400091A RID: 2330
		private bool _designMode;

		// Token: 0x0400091B RID: 2331
		private bool _shown;

		// Token: 0x0400091C RID: 2332
		private bool _salvageable;

		// Token: 0x0400091D RID: 2333
		private bool _firedUnload;

		// Token: 0x0400091E RID: 2334
		private DocumentReadyState _ready;

		// Token: 0x0400091F RID: 2335
		private IElement _focus;

		// Token: 0x04000920 RID: 2336
		private HtmlAllCollection _all;

		// Token: 0x04000921 RID: 2337
		private HtmlCollection<IHtmlAnchorElement> _anchors;

		// Token: 0x04000922 RID: 2338
		private HtmlCollection<IElement> _children;

		// Token: 0x04000923 RID: 2339
		private DomImplementation _implementation;

		// Token: 0x04000924 RID: 2340
		private IStringList _styleSheetSets;

		// Token: 0x04000925 RID: 2341
		private HtmlCollection<IHtmlImageElement> _images;

		// Token: 0x04000926 RID: 2342
		private HtmlCollection<IHtmlScriptElement> _scripts;

		// Token: 0x04000927 RID: 2343
		private HtmlCollection<IHtmlEmbedElement> _plugins;

		// Token: 0x04000928 RID: 2344
		private HtmlCollection<IElement> _commands;

		// Token: 0x04000929 RID: 2345
		private HtmlCollection<IElement> _links;

		// Token: 0x0400092A RID: 2346
		private IStyleSheetList _styleSheets;

		// Token: 0x0400092B RID: 2347
		private HttpStatusCode _statusCode;
	}
}
