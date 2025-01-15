using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000346 RID: 838
	internal sealed class HtmlBodyElement : HtmlElement, IHtmlBodyElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IWindowEventHandlers
	{
		// Token: 0x140000FA RID: 250
		// (add) Token: 0x0600193B RID: 6459 RVA: 0x000462D0 File Offset: 0x000444D0
		// (remove) Token: 0x0600193C RID: 6460 RVA: 0x000462DF File Offset: 0x000444DF
		public event DomEventHandler Printed
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

		// Token: 0x140000FB RID: 251
		// (add) Token: 0x0600193D RID: 6461 RVA: 0x000462EE File Offset: 0x000444EE
		// (remove) Token: 0x0600193E RID: 6462 RVA: 0x000462FD File Offset: 0x000444FD
		public event DomEventHandler Printing
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

		// Token: 0x140000FC RID: 252
		// (add) Token: 0x0600193F RID: 6463 RVA: 0x0004630C File Offset: 0x0004450C
		// (remove) Token: 0x06001940 RID: 6464 RVA: 0x0004631B File Offset: 0x0004451B
		public event DomEventHandler Unloading
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

		// Token: 0x140000FD RID: 253
		// (add) Token: 0x06001941 RID: 6465 RVA: 0x0004632A File Offset: 0x0004452A
		// (remove) Token: 0x06001942 RID: 6466 RVA: 0x00046339 File Offset: 0x00044539
		public event DomEventHandler HashChanged
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

		// Token: 0x140000FE RID: 254
		// (add) Token: 0x06001943 RID: 6467 RVA: 0x00046348 File Offset: 0x00044548
		// (remove) Token: 0x06001944 RID: 6468 RVA: 0x00046357 File Offset: 0x00044557
		public event DomEventHandler MessageReceived
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

		// Token: 0x140000FF RID: 255
		// (add) Token: 0x06001945 RID: 6469 RVA: 0x00046366 File Offset: 0x00044566
		// (remove) Token: 0x06001946 RID: 6470 RVA: 0x00046375 File Offset: 0x00044575
		public event DomEventHandler WentOffline
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

		// Token: 0x14000100 RID: 256
		// (add) Token: 0x06001947 RID: 6471 RVA: 0x00046384 File Offset: 0x00044584
		// (remove) Token: 0x06001948 RID: 6472 RVA: 0x00046393 File Offset: 0x00044593
		public event DomEventHandler WentOnline
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

		// Token: 0x14000101 RID: 257
		// (add) Token: 0x06001949 RID: 6473 RVA: 0x000463A2 File Offset: 0x000445A2
		// (remove) Token: 0x0600194A RID: 6474 RVA: 0x000463B1 File Offset: 0x000445B1
		public event DomEventHandler PageHidden
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

		// Token: 0x14000102 RID: 258
		// (add) Token: 0x0600194B RID: 6475 RVA: 0x000463C0 File Offset: 0x000445C0
		// (remove) Token: 0x0600194C RID: 6476 RVA: 0x000463CF File Offset: 0x000445CF
		public event DomEventHandler PageShown
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

		// Token: 0x14000103 RID: 259
		// (add) Token: 0x0600194D RID: 6477 RVA: 0x000463DE File Offset: 0x000445DE
		// (remove) Token: 0x0600194E RID: 6478 RVA: 0x000463ED File Offset: 0x000445ED
		public event DomEventHandler PopState
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

		// Token: 0x14000104 RID: 260
		// (add) Token: 0x0600194F RID: 6479 RVA: 0x000463FC File Offset: 0x000445FC
		// (remove) Token: 0x06001950 RID: 6480 RVA: 0x0004640B File Offset: 0x0004460B
		public event DomEventHandler Storage
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

		// Token: 0x14000105 RID: 261
		// (add) Token: 0x06001951 RID: 6481 RVA: 0x0004641A File Offset: 0x0004461A
		// (remove) Token: 0x06001952 RID: 6482 RVA: 0x00046429 File Offset: 0x00044629
		public event DomEventHandler Unloaded
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

		// Token: 0x06001953 RID: 6483 RVA: 0x0004FE50 File Offset: 0x0004E050
		public HtmlBodyElement(Document owner, string prefix = null)
			: base(owner, TagNames.Body, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed)
		{
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x0004FE61 File Offset: 0x0004E061
		// (set) Token: 0x06001955 RID: 6485 RVA: 0x0004FE6E File Offset: 0x0004E06E
		public string ALink
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Alink);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Alink, value, false);
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x0004FE7D File Offset: 0x0004E07D
		// (set) Token: 0x06001957 RID: 6487 RVA: 0x0004FE8A File Offset: 0x0004E08A
		public string Background
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Background);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Background, value, false);
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x0004FE99 File Offset: 0x0004E099
		// (set) Token: 0x06001959 RID: 6489 RVA: 0x0004FEA6 File Offset: 0x0004E0A6
		public string BgColor
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.BgColor);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.BgColor, value, false);
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x0004FEB5 File Offset: 0x0004E0B5
		// (set) Token: 0x0600195B RID: 6491 RVA: 0x0004FEC2 File Offset: 0x0004E0C2
		public string Link
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Link);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Link, value, false);
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x0004FED1 File Offset: 0x0004E0D1
		// (set) Token: 0x0600195D RID: 6493 RVA: 0x0004FEDE File Offset: 0x0004E0DE
		public string Text
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Text);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Text, value, false);
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x0004FEED File Offset: 0x0004E0ED
		// (set) Token: 0x0600195F RID: 6495 RVA: 0x0004FEFA File Offset: 0x0004E0FA
		public string VLink
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Vlink);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Vlink, value, false);
			}
		}
	}
}
