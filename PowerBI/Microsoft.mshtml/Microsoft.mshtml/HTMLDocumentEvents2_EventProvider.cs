using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E0F RID: 3599
	internal sealed class HTMLDocumentEvents2_EventProvider : HTMLDocumentEvents2_Event, IDisposable
	{
		// Token: 0x06019090 RID: 102544 RVA: 0x000FDF00 File Offset: 0x000FCF00
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				19, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06019091 RID: 102545 RVA: 0x000FE014 File Offset: 0x000FD014
		public override void add_onbeforedeactivate(HTMLDocumentEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x06019092 RID: 102546 RVA: 0x000FE0A4 File Offset: 0x000FD0A4
		public override void remove_onbeforedeactivate(HTMLDocumentEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019093 RID: 102547 RVA: 0x000FE194 File Offset: 0x000FD194
		public override void add_onbeforeactivate(HTMLDocumentEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x06019094 RID: 102548 RVA: 0x000FE224 File Offset: 0x000FD224
		public override void remove_onbeforeactivate(HTMLDocumentEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019095 RID: 102549 RVA: 0x000FE314 File Offset: 0x000FD314
		public override void add_ondeactivate(HTMLDocumentEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x06019096 RID: 102550 RVA: 0x000FE3A4 File Offset: 0x000FD3A4
		public override void remove_ondeactivate(HTMLDocumentEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmldocumentEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019097 RID: 102551 RVA: 0x000FE494 File Offset: 0x000FD494
		public override void add_onactivate(HTMLDocumentEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x06019098 RID: 102552 RVA: 0x000FE524 File Offset: 0x000FD524
		public override void remove_onactivate(HTMLDocumentEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onactivateDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019099 RID: 102553 RVA: 0x000FE614 File Offset: 0x000FD614
		public override void add_onfocusout(HTMLDocumentEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x0601909A RID: 102554 RVA: 0x000FE6A4 File Offset: 0x000FD6A4
		public override void remove_onfocusout(HTMLDocumentEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601909B RID: 102555 RVA: 0x000FE794 File Offset: 0x000FD794
		public override void add_onfocusin(HTMLDocumentEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x0601909C RID: 102556 RVA: 0x000FE824 File Offset: 0x000FD824
		public override void remove_onfocusin(HTMLDocumentEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601909D RID: 102557 RVA: 0x000FE914 File Offset: 0x000FD914
		public override void add_onmousewheel(HTMLDocumentEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x0601909E RID: 102558 RVA: 0x000FE9A4 File Offset: 0x000FD9A4
		public override void remove_onmousewheel(HTMLDocumentEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601909F RID: 102559 RVA: 0x000FEA94 File Offset: 0x000FDA94
		public override void add_oncontrolselect(HTMLDocumentEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190A0 RID: 102560 RVA: 0x000FEB24 File Offset: 0x000FDB24
		public override void remove_oncontrolselect(HTMLDocumentEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmldocumentEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190A1 RID: 102561 RVA: 0x000FEC14 File Offset: 0x000FDC14
		public override void add_onselectionchange(HTMLDocumentEvents2_onselectionchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onselectionchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190A2 RID: 102562 RVA: 0x000FECA4 File Offset: 0x000FDCA4
		public override void remove_onselectionchange(HTMLDocumentEvents2_onselectionchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onselectionchangeDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onselectionchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190A3 RID: 102563 RVA: 0x000FED94 File Offset: 0x000FDD94
		public override void add_onbeforeeditfocus(HTMLDocumentEvents2_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190A4 RID: 102564 RVA: 0x000FEE24 File Offset: 0x000FDE24
		public override void remove_onbeforeeditfocus(HTMLDocumentEvents2_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190A5 RID: 102565 RVA: 0x000FEF14 File Offset: 0x000FDF14
		public override void add_ondatasetcomplete(HTMLDocumentEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190A6 RID: 102566 RVA: 0x000FEFA4 File Offset: 0x000FDFA4
		public override void remove_ondatasetcomplete(HTMLDocumentEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmldocumentEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190A7 RID: 102567 RVA: 0x000FF094 File Offset: 0x000FE094
		public override void add_ondataavailable(HTMLDocumentEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190A8 RID: 102568 RVA: 0x000FF124 File Offset: 0x000FE124
		public override void remove_ondataavailable(HTMLDocumentEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmldocumentEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190A9 RID: 102569 RVA: 0x000FF214 File Offset: 0x000FE214
		public override void add_ondatasetchanged(HTMLDocumentEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190AA RID: 102570 RVA: 0x000FF2A4 File Offset: 0x000FE2A4
		public override void remove_ondatasetchanged(HTMLDocumentEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmldocumentEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190AB RID: 102571 RVA: 0x000FF394 File Offset: 0x000FE394
		public override void add_onpropertychange(HTMLDocumentEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190AC RID: 102572 RVA: 0x000FF424 File Offset: 0x000FE424
		public override void remove_onpropertychange(HTMLDocumentEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190AD RID: 102573 RVA: 0x000FF514 File Offset: 0x000FE514
		public override void add_oncellchange(HTMLDocumentEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190AE RID: 102574 RVA: 0x000FF5A4 File Offset: 0x000FE5A4
		public override void remove_oncellchange(HTMLDocumentEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmldocumentEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190AF RID: 102575 RVA: 0x000FF694 File Offset: 0x000FE694
		public override void add_onrowsinserted(HTMLDocumentEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190B0 RID: 102576 RVA: 0x000FF724 File Offset: 0x000FE724
		public override void remove_onrowsinserted(HTMLDocumentEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190B1 RID: 102577 RVA: 0x000FF814 File Offset: 0x000FE814
		public override void add_onrowsdelete(HTMLDocumentEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190B2 RID: 102578 RVA: 0x000FF8A4 File Offset: 0x000FE8A4
		public override void remove_onrowsdelete(HTMLDocumentEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190B3 RID: 102579 RVA: 0x000FF994 File Offset: 0x000FE994
		public override void add_onstop(HTMLDocumentEvents2_onstopEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onstopDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190B4 RID: 102580 RVA: 0x000FFA24 File Offset: 0x000FEA24
		public override void remove_onstop(HTMLDocumentEvents2_onstopEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onstopDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onstopDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190B5 RID: 102581 RVA: 0x000FFB14 File Offset: 0x000FEB14
		public override void add_oncontextmenu(HTMLDocumentEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190B6 RID: 102582 RVA: 0x000FFBA4 File Offset: 0x000FEBA4
		public override void remove_oncontextmenu(HTMLDocumentEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmldocumentEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190B7 RID: 102583 RVA: 0x000FFC94 File Offset: 0x000FEC94
		public override void add_onerrorupdate(HTMLDocumentEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190B8 RID: 102584 RVA: 0x000FFD24 File Offset: 0x000FED24
		public override void remove_onerrorupdate(HTMLDocumentEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190B9 RID: 102585 RVA: 0x000FFE14 File Offset: 0x000FEE14
		public override void add_onselectstart(HTMLDocumentEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190BA RID: 102586 RVA: 0x000FFEA4 File Offset: 0x000FEEA4
		public override void remove_onselectstart(HTMLDocumentEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190BB RID: 102587 RVA: 0x000FFF94 File Offset: 0x000FEF94
		public override void add_ondragstart(HTMLDocumentEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190BC RID: 102588 RVA: 0x00100024 File Offset: 0x000FF024
		public override void remove_ondragstart(HTMLDocumentEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmldocumentEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190BD RID: 102589 RVA: 0x00100114 File Offset: 0x000FF114
		public override void add_onrowenter(HTMLDocumentEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190BE RID: 102590 RVA: 0x001001A4 File Offset: 0x000FF1A4
		public override void remove_onrowenter(HTMLDocumentEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190BF RID: 102591 RVA: 0x00100294 File Offset: 0x000FF294
		public override void add_onrowexit(HTMLDocumentEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190C0 RID: 102592 RVA: 0x00100324 File Offset: 0x000FF324
		public override void remove_onrowexit(HTMLDocumentEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190C1 RID: 102593 RVA: 0x00100414 File Offset: 0x000FF414
		public override void add_onafterupdate(HTMLDocumentEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190C2 RID: 102594 RVA: 0x001004A4 File Offset: 0x000FF4A4
		public override void remove_onafterupdate(HTMLDocumentEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190C3 RID: 102595 RVA: 0x00100594 File Offset: 0x000FF594
		public override void add_onbeforeupdate(HTMLDocumentEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190C4 RID: 102596 RVA: 0x00100624 File Offset: 0x000FF624
		public override void remove_onbeforeupdate(HTMLDocumentEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190C5 RID: 102597 RVA: 0x00100714 File Offset: 0x000FF714
		public override void add_onreadystatechange(HTMLDocumentEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190C6 RID: 102598 RVA: 0x001007A4 File Offset: 0x000FF7A4
		public override void remove_onreadystatechange(HTMLDocumentEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190C7 RID: 102599 RVA: 0x00100894 File Offset: 0x000FF894
		public override void add_onmouseover(HTMLDocumentEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190C8 RID: 102600 RVA: 0x00100924 File Offset: 0x000FF924
		public override void remove_onmouseover(HTMLDocumentEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190C9 RID: 102601 RVA: 0x00100A14 File Offset: 0x000FFA14
		public override void add_onmouseout(HTMLDocumentEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190CA RID: 102602 RVA: 0x00100AA4 File Offset: 0x000FFAA4
		public override void remove_onmouseout(HTMLDocumentEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190CB RID: 102603 RVA: 0x00100B94 File Offset: 0x000FFB94
		public override void add_onmouseup(HTMLDocumentEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190CC RID: 102604 RVA: 0x00100C24 File Offset: 0x000FFC24
		public override void remove_onmouseup(HTMLDocumentEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190CD RID: 102605 RVA: 0x00100D14 File Offset: 0x000FFD14
		public override void add_onmousemove(HTMLDocumentEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190CE RID: 102606 RVA: 0x00100DA4 File Offset: 0x000FFDA4
		public override void remove_onmousemove(HTMLDocumentEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190CF RID: 102607 RVA: 0x00100E94 File Offset: 0x000FFE94
		public override void add_onmousedown(HTMLDocumentEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190D0 RID: 102608 RVA: 0x00100F24 File Offset: 0x000FFF24
		public override void remove_onmousedown(HTMLDocumentEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190D1 RID: 102609 RVA: 0x00101014 File Offset: 0x00100014
		public override void add_onkeypress(HTMLDocumentEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190D2 RID: 102610 RVA: 0x001010A4 File Offset: 0x001000A4
		public override void remove_onkeypress(HTMLDocumentEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190D3 RID: 102611 RVA: 0x00101194 File Offset: 0x00100194
		public override void add_onkeyup(HTMLDocumentEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190D4 RID: 102612 RVA: 0x00101224 File Offset: 0x00100224
		public override void remove_onkeyup(HTMLDocumentEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190D5 RID: 102613 RVA: 0x00101314 File Offset: 0x00100314
		public override void add_onkeydown(HTMLDocumentEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190D6 RID: 102614 RVA: 0x001013A4 File Offset: 0x001003A4
		public override void remove_onkeydown(HTMLDocumentEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190D7 RID: 102615 RVA: 0x00101494 File Offset: 0x00100494
		public override void add_ondblclick(HTMLDocumentEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190D8 RID: 102616 RVA: 0x00101524 File Offset: 0x00100524
		public override void remove_ondblclick(HTMLDocumentEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmldocumentEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190D9 RID: 102617 RVA: 0x00101614 File Offset: 0x00100614
		public override void add_onclick(HTMLDocumentEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190DA RID: 102618 RVA: 0x001016A4 File Offset: 0x001006A4
		public override void remove_onclick(HTMLDocumentEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onclickDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190DB RID: 102619 RVA: 0x00101794 File Offset: 0x00100794
		public override void add_onhelp(HTMLDocumentEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = new HTMLDocumentEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmldocumentEvents2_SinkHelper, ref num);
				htmldocumentEvents2_SinkHelper.m_dwCookie = num;
				htmldocumentEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmldocumentEvents2_SinkHelper);
			}
		}

		// Token: 0x060190DC RID: 102620 RVA: 0x00101824 File Offset: 0x00100824
		public override void remove_onhelp(HTMLDocumentEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper;
					for (;;)
					{
						htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmldocumentEvents2_SinkHelper.m_onhelpDelegate != null && ((htmldocumentEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060190DD RID: 102621 RVA: 0x00101914 File Offset: 0x00100914
		public HTMLDocumentEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x060190DE RID: 102622 RVA: 0x0010193C File Offset: 0x0010093C
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
								HTMLDocumentEvents2_SinkHelper htmldocumentEvents2_SinkHelper = (HTMLDocumentEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmldocumentEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x060190DF RID: 102623 RVA: 0x001019F0 File Offset: 0x001009F0
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000E72 RID: 3698
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000E73 RID: 3699
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000E74 RID: 3700
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
