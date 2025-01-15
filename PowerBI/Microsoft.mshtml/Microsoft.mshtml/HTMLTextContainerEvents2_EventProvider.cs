using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DD7 RID: 3543
	internal sealed class HTMLTextContainerEvents2_EventProvider : HTMLTextContainerEvents2_Event, IDisposable
	{
		// Token: 0x06017D13 RID: 97555 RVA: 0x0004BD94 File Offset: 0x0004AD94
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				36, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06017D14 RID: 97556 RVA: 0x0004BEA8 File Offset: 0x0004AEA8
		public override void add_onselect(HTMLTextContainerEvents2_onselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D15 RID: 97557 RVA: 0x0004BF38 File Offset: 0x0004AF38
		public override void remove_onselect(HTMLTextContainerEvents2_onselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onselectDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D16 RID: 97558 RVA: 0x0004C028 File Offset: 0x0004B028
		public override void add_onchange(HTMLTextContainerEvents2_onchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D17 RID: 97559 RVA: 0x0004C0B8 File Offset: 0x0004B0B8
		public override void remove_onchange(HTMLTextContainerEvents2_onchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onchangeDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D18 RID: 97560 RVA: 0x0004C1A8 File Offset: 0x0004B1A8
		public override void add_onmousewheel(HTMLTextContainerEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D19 RID: 97561 RVA: 0x0004C238 File Offset: 0x0004B238
		public override void remove_onmousewheel(HTMLTextContainerEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D1A RID: 97562 RVA: 0x0004C328 File Offset: 0x0004B328
		public override void add_onresizeend(HTMLTextContainerEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D1B RID: 97563 RVA: 0x0004C3B8 File Offset: 0x0004B3B8
		public override void remove_onresizeend(HTMLTextContainerEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D1C RID: 97564 RVA: 0x0004C4A8 File Offset: 0x0004B4A8
		public override void add_onresizestart(HTMLTextContainerEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D1D RID: 97565 RVA: 0x0004C538 File Offset: 0x0004B538
		public override void remove_onresizestart(HTMLTextContainerEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D1E RID: 97566 RVA: 0x0004C628 File Offset: 0x0004B628
		public override void add_onmoveend(HTMLTextContainerEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D1F RID: 97567 RVA: 0x0004C6B8 File Offset: 0x0004B6B8
		public override void remove_onmoveend(HTMLTextContainerEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D20 RID: 97568 RVA: 0x0004C7A8 File Offset: 0x0004B7A8
		public override void add_onmovestart(HTMLTextContainerEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D21 RID: 97569 RVA: 0x0004C838 File Offset: 0x0004B838
		public override void remove_onmovestart(HTMLTextContainerEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D22 RID: 97570 RVA: 0x0004C928 File Offset: 0x0004B928
		public override void add_oncontrolselect(HTMLTextContainerEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D23 RID: 97571 RVA: 0x0004C9B8 File Offset: 0x0004B9B8
		public override void remove_oncontrolselect(HTMLTextContainerEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D24 RID: 97572 RVA: 0x0004CAA8 File Offset: 0x0004BAA8
		public override void add_onmove(HTMLTextContainerEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D25 RID: 97573 RVA: 0x0004CB38 File Offset: 0x0004BB38
		public override void remove_onmove(HTMLTextContainerEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onmoveDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D26 RID: 97574 RVA: 0x0004CC28 File Offset: 0x0004BC28
		public override void add_onfocusout(HTMLTextContainerEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D27 RID: 97575 RVA: 0x0004CCB8 File Offset: 0x0004BCB8
		public override void remove_onfocusout(HTMLTextContainerEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D28 RID: 97576 RVA: 0x0004CDA8 File Offset: 0x0004BDA8
		public override void add_onfocusin(HTMLTextContainerEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D29 RID: 97577 RVA: 0x0004CE38 File Offset: 0x0004BE38
		public override void remove_onfocusin(HTMLTextContainerEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D2A RID: 97578 RVA: 0x0004CF28 File Offset: 0x0004BF28
		public override void add_onbeforeactivate(HTMLTextContainerEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D2B RID: 97579 RVA: 0x0004CFB8 File Offset: 0x0004BFB8
		public override void remove_onbeforeactivate(HTMLTextContainerEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D2C RID: 97580 RVA: 0x0004D0A8 File Offset: 0x0004C0A8
		public override void add_onbeforedeactivate(HTMLTextContainerEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D2D RID: 97581 RVA: 0x0004D138 File Offset: 0x0004C138
		public override void remove_onbeforedeactivate(HTMLTextContainerEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D2E RID: 97582 RVA: 0x0004D228 File Offset: 0x0004C228
		public override void add_ondeactivate(HTMLTextContainerEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D2F RID: 97583 RVA: 0x0004D2B8 File Offset: 0x0004C2B8
		public override void remove_ondeactivate(HTMLTextContainerEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D30 RID: 97584 RVA: 0x0004D3A8 File Offset: 0x0004C3A8
		public override void add_onactivate(HTMLTextContainerEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D31 RID: 97585 RVA: 0x0004D438 File Offset: 0x0004C438
		public override void remove_onactivate(HTMLTextContainerEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onactivateDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D32 RID: 97586 RVA: 0x0004D528 File Offset: 0x0004C528
		public override void add_onmouseleave(HTMLTextContainerEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D33 RID: 97587 RVA: 0x0004D5B8 File Offset: 0x0004C5B8
		public override void remove_onmouseleave(HTMLTextContainerEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D34 RID: 97588 RVA: 0x0004D6A8 File Offset: 0x0004C6A8
		public override void add_onmouseenter(HTMLTextContainerEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D35 RID: 97589 RVA: 0x0004D738 File Offset: 0x0004C738
		public override void remove_onmouseenter(HTMLTextContainerEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D36 RID: 97590 RVA: 0x0004D828 File Offset: 0x0004C828
		public override void add_onpage(HTMLTextContainerEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D37 RID: 97591 RVA: 0x0004D8B8 File Offset: 0x0004C8B8
		public override void remove_onpage(HTMLTextContainerEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onpageDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D38 RID: 97592 RVA: 0x0004D9A8 File Offset: 0x0004C9A8
		public override void add_onlayoutcomplete(HTMLTextContainerEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D39 RID: 97593 RVA: 0x0004DA38 File Offset: 0x0004CA38
		public override void remove_onlayoutcomplete(HTMLTextContainerEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D3A RID: 97594 RVA: 0x0004DB28 File Offset: 0x0004CB28
		public override void add_onreadystatechange(HTMLTextContainerEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D3B RID: 97595 RVA: 0x0004DBB8 File Offset: 0x0004CBB8
		public override void remove_onreadystatechange(HTMLTextContainerEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D3C RID: 97596 RVA: 0x0004DCA8 File Offset: 0x0004CCA8
		public override void add_oncellchange(HTMLTextContainerEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D3D RID: 97597 RVA: 0x0004DD38 File Offset: 0x0004CD38
		public override void remove_oncellchange(HTMLTextContainerEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D3E RID: 97598 RVA: 0x0004DE28 File Offset: 0x0004CE28
		public override void add_onrowsinserted(HTMLTextContainerEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D3F RID: 97599 RVA: 0x0004DEB8 File Offset: 0x0004CEB8
		public override void remove_onrowsinserted(HTMLTextContainerEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D40 RID: 97600 RVA: 0x0004DFA8 File Offset: 0x0004CFA8
		public override void add_onrowsdelete(HTMLTextContainerEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D41 RID: 97601 RVA: 0x0004E038 File Offset: 0x0004D038
		public override void remove_onrowsdelete(HTMLTextContainerEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D42 RID: 97602 RVA: 0x0004E128 File Offset: 0x0004D128
		public override void add_oncontextmenu(HTMLTextContainerEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D43 RID: 97603 RVA: 0x0004E1B8 File Offset: 0x0004D1B8
		public override void remove_oncontextmenu(HTMLTextContainerEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D44 RID: 97604 RVA: 0x0004E2A8 File Offset: 0x0004D2A8
		public override void add_onpaste(HTMLTextContainerEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D45 RID: 97605 RVA: 0x0004E338 File Offset: 0x0004D338
		public override void remove_onpaste(HTMLTextContainerEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onpasteDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D46 RID: 97606 RVA: 0x0004E428 File Offset: 0x0004D428
		public override void add_onbeforepaste(HTMLTextContainerEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D47 RID: 97607 RVA: 0x0004E4B8 File Offset: 0x0004D4B8
		public override void remove_onbeforepaste(HTMLTextContainerEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D48 RID: 97608 RVA: 0x0004E5A8 File Offset: 0x0004D5A8
		public override void add_oncopy(HTMLTextContainerEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D49 RID: 97609 RVA: 0x0004E638 File Offset: 0x0004D638
		public override void remove_oncopy(HTMLTextContainerEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_oncopyDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D4A RID: 97610 RVA: 0x0004E728 File Offset: 0x0004D728
		public override void add_onbeforecopy(HTMLTextContainerEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D4B RID: 97611 RVA: 0x0004E7B8 File Offset: 0x0004D7B8
		public override void remove_onbeforecopy(HTMLTextContainerEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D4C RID: 97612 RVA: 0x0004E8A8 File Offset: 0x0004D8A8
		public override void add_oncut(HTMLTextContainerEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D4D RID: 97613 RVA: 0x0004E938 File Offset: 0x0004D938
		public override void remove_oncut(HTMLTextContainerEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_oncutDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D4E RID: 97614 RVA: 0x0004EA28 File Offset: 0x0004DA28
		public override void add_onbeforecut(HTMLTextContainerEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D4F RID: 97615 RVA: 0x0004EAB8 File Offset: 0x0004DAB8
		public override void remove_onbeforecut(HTMLTextContainerEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D50 RID: 97616 RVA: 0x0004EBA8 File Offset: 0x0004DBA8
		public override void add_ondrop(HTMLTextContainerEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D51 RID: 97617 RVA: 0x0004EC38 File Offset: 0x0004DC38
		public override void remove_ondrop(HTMLTextContainerEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_ondropDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D52 RID: 97618 RVA: 0x0004ED28 File Offset: 0x0004DD28
		public override void add_ondragleave(HTMLTextContainerEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D53 RID: 97619 RVA: 0x0004EDB8 File Offset: 0x0004DDB8
		public override void remove_ondragleave(HTMLTextContainerEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D54 RID: 97620 RVA: 0x0004EEA8 File Offset: 0x0004DEA8
		public override void add_ondragover(HTMLTextContainerEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D55 RID: 97621 RVA: 0x0004EF38 File Offset: 0x0004DF38
		public override void remove_ondragover(HTMLTextContainerEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D56 RID: 97622 RVA: 0x0004F028 File Offset: 0x0004E028
		public override void add_ondragenter(HTMLTextContainerEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D57 RID: 97623 RVA: 0x0004F0B8 File Offset: 0x0004E0B8
		public override void remove_ondragenter(HTMLTextContainerEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D58 RID: 97624 RVA: 0x0004F1A8 File Offset: 0x0004E1A8
		public override void add_ondragend(HTMLTextContainerEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D59 RID: 97625 RVA: 0x0004F238 File Offset: 0x0004E238
		public override void remove_ondragend(HTMLTextContainerEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_ondragendDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D5A RID: 97626 RVA: 0x0004F328 File Offset: 0x0004E328
		public override void add_ondrag(HTMLTextContainerEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D5B RID: 97627 RVA: 0x0004F3B8 File Offset: 0x0004E3B8
		public override void remove_ondrag(HTMLTextContainerEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_ondragDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D5C RID: 97628 RVA: 0x0004F4A8 File Offset: 0x0004E4A8
		public override void add_onresize(HTMLTextContainerEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D5D RID: 97629 RVA: 0x0004F538 File Offset: 0x0004E538
		public override void remove_onresize(HTMLTextContainerEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onresizeDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D5E RID: 97630 RVA: 0x0004F628 File Offset: 0x0004E628
		public override void add_onblur(HTMLTextContainerEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D5F RID: 97631 RVA: 0x0004F6B8 File Offset: 0x0004E6B8
		public override void remove_onblur(HTMLTextContainerEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onblurDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D60 RID: 97632 RVA: 0x0004F7A8 File Offset: 0x0004E7A8
		public override void add_onfocus(HTMLTextContainerEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D61 RID: 97633 RVA: 0x0004F838 File Offset: 0x0004E838
		public override void remove_onfocus(HTMLTextContainerEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onfocusDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D62 RID: 97634 RVA: 0x0004F928 File Offset: 0x0004E928
		public override void add_onscroll(HTMLTextContainerEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D63 RID: 97635 RVA: 0x0004F9B8 File Offset: 0x0004E9B8
		public override void remove_onscroll(HTMLTextContainerEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onscrollDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D64 RID: 97636 RVA: 0x0004FAA8 File Offset: 0x0004EAA8
		public override void add_onpropertychange(HTMLTextContainerEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D65 RID: 97637 RVA: 0x0004FB38 File Offset: 0x0004EB38
		public override void remove_onpropertychange(HTMLTextContainerEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D66 RID: 97638 RVA: 0x0004FC28 File Offset: 0x0004EC28
		public override void add_onlosecapture(HTMLTextContainerEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D67 RID: 97639 RVA: 0x0004FCB8 File Offset: 0x0004ECB8
		public override void remove_onlosecapture(HTMLTextContainerEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D68 RID: 97640 RVA: 0x0004FDA8 File Offset: 0x0004EDA8
		public override void add_ondatasetcomplete(HTMLTextContainerEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D69 RID: 97641 RVA: 0x0004FE38 File Offset: 0x0004EE38
		public override void remove_ondatasetcomplete(HTMLTextContainerEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D6A RID: 97642 RVA: 0x0004FF28 File Offset: 0x0004EF28
		public override void add_ondataavailable(HTMLTextContainerEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D6B RID: 97643 RVA: 0x0004FFB8 File Offset: 0x0004EFB8
		public override void remove_ondataavailable(HTMLTextContainerEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D6C RID: 97644 RVA: 0x000500A8 File Offset: 0x0004F0A8
		public override void add_ondatasetchanged(HTMLTextContainerEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D6D RID: 97645 RVA: 0x00050138 File Offset: 0x0004F138
		public override void remove_ondatasetchanged(HTMLTextContainerEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D6E RID: 97646 RVA: 0x00050228 File Offset: 0x0004F228
		public override void add_onrowenter(HTMLTextContainerEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D6F RID: 97647 RVA: 0x000502B8 File Offset: 0x0004F2B8
		public override void remove_onrowenter(HTMLTextContainerEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D70 RID: 97648 RVA: 0x000503A8 File Offset: 0x0004F3A8
		public override void add_onrowexit(HTMLTextContainerEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D71 RID: 97649 RVA: 0x00050438 File Offset: 0x0004F438
		public override void remove_onrowexit(HTMLTextContainerEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D72 RID: 97650 RVA: 0x00050528 File Offset: 0x0004F528
		public override void add_onerrorupdate(HTMLTextContainerEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D73 RID: 97651 RVA: 0x000505B8 File Offset: 0x0004F5B8
		public override void remove_onerrorupdate(HTMLTextContainerEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D74 RID: 97652 RVA: 0x000506A8 File Offset: 0x0004F6A8
		public override void add_onafterupdate(HTMLTextContainerEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D75 RID: 97653 RVA: 0x00050738 File Offset: 0x0004F738
		public override void remove_onafterupdate(HTMLTextContainerEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D76 RID: 97654 RVA: 0x00050828 File Offset: 0x0004F828
		public override void add_onbeforeupdate(HTMLTextContainerEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D77 RID: 97655 RVA: 0x000508B8 File Offset: 0x0004F8B8
		public override void remove_onbeforeupdate(HTMLTextContainerEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D78 RID: 97656 RVA: 0x000509A8 File Offset: 0x0004F9A8
		public override void add_ondragstart(HTMLTextContainerEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D79 RID: 97657 RVA: 0x00050A38 File Offset: 0x0004FA38
		public override void remove_ondragstart(HTMLTextContainerEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D7A RID: 97658 RVA: 0x00050B28 File Offset: 0x0004FB28
		public override void add_onfilterchange(HTMLTextContainerEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D7B RID: 97659 RVA: 0x00050BB8 File Offset: 0x0004FBB8
		public override void remove_onfilterchange(HTMLTextContainerEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D7C RID: 97660 RVA: 0x00050CA8 File Offset: 0x0004FCA8
		public override void add_onselectstart(HTMLTextContainerEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D7D RID: 97661 RVA: 0x00050D38 File Offset: 0x0004FD38
		public override void remove_onselectstart(HTMLTextContainerEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D7E RID: 97662 RVA: 0x00050E28 File Offset: 0x0004FE28
		public override void add_onmouseup(HTMLTextContainerEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D7F RID: 97663 RVA: 0x00050EB8 File Offset: 0x0004FEB8
		public override void remove_onmouseup(HTMLTextContainerEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D80 RID: 97664 RVA: 0x00050FA8 File Offset: 0x0004FFA8
		public override void add_onmousedown(HTMLTextContainerEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D81 RID: 97665 RVA: 0x00051038 File Offset: 0x00050038
		public override void remove_onmousedown(HTMLTextContainerEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D82 RID: 97666 RVA: 0x00051128 File Offset: 0x00050128
		public override void add_onmousemove(HTMLTextContainerEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D83 RID: 97667 RVA: 0x000511B8 File Offset: 0x000501B8
		public override void remove_onmousemove(HTMLTextContainerEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D84 RID: 97668 RVA: 0x000512A8 File Offset: 0x000502A8
		public override void add_onmouseover(HTMLTextContainerEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D85 RID: 97669 RVA: 0x00051338 File Offset: 0x00050338
		public override void remove_onmouseover(HTMLTextContainerEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D86 RID: 97670 RVA: 0x00051428 File Offset: 0x00050428
		public override void add_onmouseout(HTMLTextContainerEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D87 RID: 97671 RVA: 0x000514B8 File Offset: 0x000504B8
		public override void remove_onmouseout(HTMLTextContainerEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D88 RID: 97672 RVA: 0x000515A8 File Offset: 0x000505A8
		public override void add_onkeyup(HTMLTextContainerEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D89 RID: 97673 RVA: 0x00051638 File Offset: 0x00050638
		public override void remove_onkeyup(HTMLTextContainerEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D8A RID: 97674 RVA: 0x00051728 File Offset: 0x00050728
		public override void add_onkeydown(HTMLTextContainerEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D8B RID: 97675 RVA: 0x000517B8 File Offset: 0x000507B8
		public override void remove_onkeydown(HTMLTextContainerEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D8C RID: 97676 RVA: 0x000518A8 File Offset: 0x000508A8
		public override void add_onkeypress(HTMLTextContainerEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D8D RID: 97677 RVA: 0x00051938 File Offset: 0x00050938
		public override void remove_onkeypress(HTMLTextContainerEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D8E RID: 97678 RVA: 0x00051A28 File Offset: 0x00050A28
		public override void add_ondblclick(HTMLTextContainerEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D8F RID: 97679 RVA: 0x00051AB8 File Offset: 0x00050AB8
		public override void remove_ondblclick(HTMLTextContainerEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D90 RID: 97680 RVA: 0x00051BA8 File Offset: 0x00050BA8
		public override void add_onclick(HTMLTextContainerEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D91 RID: 97681 RVA: 0x00051C38 File Offset: 0x00050C38
		public override void remove_onclick(HTMLTextContainerEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onclickDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D92 RID: 97682 RVA: 0x00051D28 File Offset: 0x00050D28
		public override void add_onhelp(HTMLTextContainerEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = new HTMLTextContainerEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents2_SinkHelper, ref num);
				htmltextContainerEvents2_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents2_SinkHelper);
			}
		}

		// Token: 0x06017D93 RID: 97683 RVA: 0x00051DB8 File Offset: 0x00050DB8
		public override void remove_onhelp(HTMLTextContainerEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents2_SinkHelper.m_onhelpDelegate != null && ((htmltextContainerEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017D94 RID: 97684 RVA: 0x00051EA8 File Offset: 0x00050EA8
		public HTMLTextContainerEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06017D95 RID: 97685 RVA: 0x00051ED0 File Offset: 0x00050ED0
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
								HTMLTextContainerEvents2_SinkHelper htmltextContainerEvents2_SinkHelper = (HTMLTextContainerEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmltextContainerEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x06017D96 RID: 97686 RVA: 0x00051F84 File Offset: 0x00050F84
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x040007C3 RID: 1987
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x040007C4 RID: 1988
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x040007C5 RID: 1989
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
