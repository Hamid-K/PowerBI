using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E19 RID: 3609
	internal sealed class HTMLDocumentEvents_EventProvider : HTMLDocumentEvents_Event, IDisposable
	{
		// Token: 0x0601941E RID: 103454 RVA: 0x0011E3C0 File Offset: 0x0011D3C0
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				96, 242, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x0601941F RID: 103455 RVA: 0x0011E4D4 File Offset: 0x0011D4D4
		public override void add_onbeforedeactivate(HTMLDocumentEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019420 RID: 103456 RVA: 0x0011E564 File Offset: 0x0011D564
		public override void remove_onbeforedeactivate(HTMLDocumentEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmldocumentEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019421 RID: 103457 RVA: 0x0011E654 File Offset: 0x0011D654
		public override void add_onbeforeactivate(HTMLDocumentEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019422 RID: 103458 RVA: 0x0011E6E4 File Offset: 0x0011D6E4
		public override void remove_onbeforeactivate(HTMLDocumentEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmldocumentEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019423 RID: 103459 RVA: 0x0011E7D4 File Offset: 0x0011D7D4
		public override void add_ondeactivate(HTMLDocumentEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019424 RID: 103460 RVA: 0x0011E864 File Offset: 0x0011D864
		public override void remove_ondeactivate(HTMLDocumentEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmldocumentEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019425 RID: 103461 RVA: 0x0011E954 File Offset: 0x0011D954
		public override void add_onactivate(HTMLDocumentEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019426 RID: 103462 RVA: 0x0011E9E4 File Offset: 0x0011D9E4
		public override void remove_onactivate(HTMLDocumentEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onactivateDelegate != null && ((htmldocumentEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019427 RID: 103463 RVA: 0x0011EAD4 File Offset: 0x0011DAD4
		public override void add_onfocusout(HTMLDocumentEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019428 RID: 103464 RVA: 0x0011EB64 File Offset: 0x0011DB64
		public override void remove_onfocusout(HTMLDocumentEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmldocumentEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019429 RID: 103465 RVA: 0x0011EC54 File Offset: 0x0011DC54
		public override void add_onfocusin(HTMLDocumentEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x0601942A RID: 103466 RVA: 0x0011ECE4 File Offset: 0x0011DCE4
		public override void remove_onfocusin(HTMLDocumentEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onfocusinDelegate != null && ((htmldocumentEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601942B RID: 103467 RVA: 0x0011EDD4 File Offset: 0x0011DDD4
		public override void add_onmousewheel(HTMLDocumentEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x0601942C RID: 103468 RVA: 0x0011EE64 File Offset: 0x0011DE64
		public override void remove_onmousewheel(HTMLDocumentEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmldocumentEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601942D RID: 103469 RVA: 0x0011EF54 File Offset: 0x0011DF54
		public override void add_oncontrolselect(HTMLDocumentEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x0601942E RID: 103470 RVA: 0x0011EFE4 File Offset: 0x0011DFE4
		public override void remove_oncontrolselect(HTMLDocumentEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmldocumentEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601942F RID: 103471 RVA: 0x0011F0D4 File Offset: 0x0011E0D4
		public override void add_onselectionchange(HTMLDocumentEvents_onselectionchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onselectionchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019430 RID: 103472 RVA: 0x0011F164 File Offset: 0x0011E164
		public override void remove_onselectionchange(HTMLDocumentEvents_onselectionchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onselectionchangeDelegate != null && ((htmldocumentEvents_SinkHelper.m_onselectionchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019431 RID: 103473 RVA: 0x0011F254 File Offset: 0x0011E254
		public override void add_onbeforeeditfocus(HTMLDocumentEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019432 RID: 103474 RVA: 0x0011F2E4 File Offset: 0x0011E2E4
		public override void remove_onbeforeeditfocus(HTMLDocumentEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmldocumentEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019433 RID: 103475 RVA: 0x0011F3D4 File Offset: 0x0011E3D4
		public override void add_ondatasetcomplete(HTMLDocumentEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019434 RID: 103476 RVA: 0x0011F464 File Offset: 0x0011E464
		public override void remove_ondatasetcomplete(HTMLDocumentEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmldocumentEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019435 RID: 103477 RVA: 0x0011F554 File Offset: 0x0011E554
		public override void add_ondataavailable(HTMLDocumentEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019436 RID: 103478 RVA: 0x0011F5E4 File Offset: 0x0011E5E4
		public override void remove_ondataavailable(HTMLDocumentEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmldocumentEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019437 RID: 103479 RVA: 0x0011F6D4 File Offset: 0x0011E6D4
		public override void add_ondatasetchanged(HTMLDocumentEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019438 RID: 103480 RVA: 0x0011F764 File Offset: 0x0011E764
		public override void remove_ondatasetchanged(HTMLDocumentEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmldocumentEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019439 RID: 103481 RVA: 0x0011F854 File Offset: 0x0011E854
		public override void add_onpropertychange(HTMLDocumentEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x0601943A RID: 103482 RVA: 0x0011F8E4 File Offset: 0x0011E8E4
		public override void remove_onpropertychange(HTMLDocumentEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmldocumentEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601943B RID: 103483 RVA: 0x0011F9D4 File Offset: 0x0011E9D4
		public override void add_oncellchange(HTMLDocumentEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x0601943C RID: 103484 RVA: 0x0011FA64 File Offset: 0x0011EA64
		public override void remove_oncellchange(HTMLDocumentEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmldocumentEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601943D RID: 103485 RVA: 0x0011FB54 File Offset: 0x0011EB54
		public override void add_onrowsinserted(HTMLDocumentEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x0601943E RID: 103486 RVA: 0x0011FBE4 File Offset: 0x0011EBE4
		public override void remove_onrowsinserted(HTMLDocumentEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmldocumentEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601943F RID: 103487 RVA: 0x0011FCD4 File Offset: 0x0011ECD4
		public override void add_onrowsdelete(HTMLDocumentEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019440 RID: 103488 RVA: 0x0011FD64 File Offset: 0x0011ED64
		public override void remove_onrowsdelete(HTMLDocumentEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmldocumentEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019441 RID: 103489 RVA: 0x0011FE54 File Offset: 0x0011EE54
		public override void add_onstop(HTMLDocumentEvents_onstopEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onstopDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019442 RID: 103490 RVA: 0x0011FEE4 File Offset: 0x0011EEE4
		public override void remove_onstop(HTMLDocumentEvents_onstopEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onstopDelegate != null && ((htmldocumentEvents_SinkHelper.m_onstopDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019443 RID: 103491 RVA: 0x0011FFD4 File Offset: 0x0011EFD4
		public override void add_oncontextmenu(HTMLDocumentEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019444 RID: 103492 RVA: 0x00120064 File Offset: 0x0011F064
		public override void remove_oncontextmenu(HTMLDocumentEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmldocumentEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019445 RID: 103493 RVA: 0x00120154 File Offset: 0x0011F154
		public override void add_onerrorupdate(HTMLDocumentEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019446 RID: 103494 RVA: 0x001201E4 File Offset: 0x0011F1E4
		public override void remove_onerrorupdate(HTMLDocumentEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmldocumentEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019447 RID: 103495 RVA: 0x001202D4 File Offset: 0x0011F2D4
		public override void add_onselectstart(HTMLDocumentEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019448 RID: 103496 RVA: 0x00120364 File Offset: 0x0011F364
		public override void remove_onselectstart(HTMLDocumentEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onselectstartDelegate != null && ((htmldocumentEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019449 RID: 103497 RVA: 0x00120454 File Offset: 0x0011F454
		public override void add_ondragstart(HTMLDocumentEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x0601944A RID: 103498 RVA: 0x001204E4 File Offset: 0x0011F4E4
		public override void remove_ondragstart(HTMLDocumentEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_ondragstartDelegate != null && ((htmldocumentEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601944B RID: 103499 RVA: 0x001205D4 File Offset: 0x0011F5D4
		public override void add_onrowenter(HTMLDocumentEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x0601944C RID: 103500 RVA: 0x00120664 File Offset: 0x0011F664
		public override void remove_onrowenter(HTMLDocumentEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onrowenterDelegate != null && ((htmldocumentEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601944D RID: 103501 RVA: 0x00120754 File Offset: 0x0011F754
		public override void add_onrowexit(HTMLDocumentEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x0601944E RID: 103502 RVA: 0x001207E4 File Offset: 0x0011F7E4
		public override void remove_onrowexit(HTMLDocumentEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onrowexitDelegate != null && ((htmldocumentEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601944F RID: 103503 RVA: 0x001208D4 File Offset: 0x0011F8D4
		public override void add_onafterupdate(HTMLDocumentEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019450 RID: 103504 RVA: 0x00120964 File Offset: 0x0011F964
		public override void remove_onafterupdate(HTMLDocumentEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmldocumentEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019451 RID: 103505 RVA: 0x00120A54 File Offset: 0x0011FA54
		public override void add_onbeforeupdate(HTMLDocumentEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019452 RID: 103506 RVA: 0x00120AE4 File Offset: 0x0011FAE4
		public override void remove_onbeforeupdate(HTMLDocumentEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmldocumentEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019453 RID: 103507 RVA: 0x00120BD4 File Offset: 0x0011FBD4
		public override void add_onreadystatechange(HTMLDocumentEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019454 RID: 103508 RVA: 0x00120C64 File Offset: 0x0011FC64
		public override void remove_onreadystatechange(HTMLDocumentEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmldocumentEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019455 RID: 103509 RVA: 0x00120D54 File Offset: 0x0011FD54
		public override void add_onmouseover(HTMLDocumentEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019456 RID: 103510 RVA: 0x00120DE4 File Offset: 0x0011FDE4
		public override void remove_onmouseover(HTMLDocumentEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmldocumentEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019457 RID: 103511 RVA: 0x00120ED4 File Offset: 0x0011FED4
		public override void add_onmouseout(HTMLDocumentEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019458 RID: 103512 RVA: 0x00120F64 File Offset: 0x0011FF64
		public override void remove_onmouseout(HTMLDocumentEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmldocumentEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019459 RID: 103513 RVA: 0x00121054 File Offset: 0x00120054
		public override void add_onmouseup(HTMLDocumentEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x0601945A RID: 103514 RVA: 0x001210E4 File Offset: 0x001200E4
		public override void remove_onmouseup(HTMLDocumentEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onmouseupDelegate != null && ((htmldocumentEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601945B RID: 103515 RVA: 0x001211D4 File Offset: 0x001201D4
		public override void add_onmousemove(HTMLDocumentEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x0601945C RID: 103516 RVA: 0x00121264 File Offset: 0x00120264
		public override void remove_onmousemove(HTMLDocumentEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmldocumentEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601945D RID: 103517 RVA: 0x00121354 File Offset: 0x00120354
		public override void add_onmousedown(HTMLDocumentEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x0601945E RID: 103518 RVA: 0x001213E4 File Offset: 0x001203E4
		public override void remove_onmousedown(HTMLDocumentEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onmousedownDelegate != null && ((htmldocumentEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601945F RID: 103519 RVA: 0x001214D4 File Offset: 0x001204D4
		public override void add_onkeypress(HTMLDocumentEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019460 RID: 103520 RVA: 0x00121564 File Offset: 0x00120564
		public override void remove_onkeypress(HTMLDocumentEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onkeypressDelegate != null && ((htmldocumentEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019461 RID: 103521 RVA: 0x00121654 File Offset: 0x00120654
		public override void add_onkeyup(HTMLDocumentEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019462 RID: 103522 RVA: 0x001216E4 File Offset: 0x001206E4
		public override void remove_onkeyup(HTMLDocumentEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onkeyupDelegate != null && ((htmldocumentEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019463 RID: 103523 RVA: 0x001217D4 File Offset: 0x001207D4
		public override void add_onkeydown(HTMLDocumentEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019464 RID: 103524 RVA: 0x00121864 File Offset: 0x00120864
		public override void remove_onkeydown(HTMLDocumentEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onkeydownDelegate != null && ((htmldocumentEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019465 RID: 103525 RVA: 0x00121954 File Offset: 0x00120954
		public override void add_ondblclick(HTMLDocumentEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019466 RID: 103526 RVA: 0x001219E4 File Offset: 0x001209E4
		public override void remove_ondblclick(HTMLDocumentEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_ondblclickDelegate != null && ((htmldocumentEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019467 RID: 103527 RVA: 0x00121AD4 File Offset: 0x00120AD4
		public override void add_onclick(HTMLDocumentEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x06019468 RID: 103528 RVA: 0x00121B64 File Offset: 0x00120B64
		public override void remove_onclick(HTMLDocumentEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onclickDelegate != null && ((htmldocumentEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019469 RID: 103529 RVA: 0x00121C54 File Offset: 0x00120C54
		public override void add_onhelp(HTMLDocumentEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = new HTMLDocumentEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents_SinkHelper, ref num);
				htmldocumentEvents_SinkHelper.m_dwCookie = num;
				htmldocumentEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents_SinkHelper);
			}
		}

		// Token: 0x0601946A RID: 103530 RVA: 0x00121CE4 File Offset: 0x00120CE4
		public override void remove_onhelp(HTMLDocumentEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper;
					for (;;)
					{
						htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents_SinkHelper.m_onhelpDelegate != null && ((htmldocumentEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601946B RID: 103531 RVA: 0x00121DD4 File Offset: 0x00120DD4
		public HTMLDocumentEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x0601946C RID: 103532 RVA: 0x00121DFC File Offset: 0x00120DFC
		public override void Finalize()
		{
			lock (this)
			{
				try
				{
					if (this.m_ConnectionPoint != null)
					{
						int count = this.m_aEventSinkHelpers.Count;
						int num = 0;
						if (0 < count)
						{
							do
							{
								HTMLDocumentEvents_SinkHelper htmldocumentEvents_SinkHelper = (HTMLDocumentEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmldocumentEvents_SinkHelper.m_dwCookie);
								num++;
							}
							while (num < count);
						}
					}
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x0601946D RID: 103533 RVA: 0x00121EB0 File Offset: 0x00120EB0
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000FAD RID: 4013
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000FAE RID: 4014
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000FAF RID: 4015
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
