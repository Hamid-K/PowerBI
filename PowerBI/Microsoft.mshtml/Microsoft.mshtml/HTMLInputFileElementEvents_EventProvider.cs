using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E07 RID: 3591
	internal sealed class HTMLInputFileElementEvents_EventProvider : HTMLInputFileElementEvents_Event, IDisposable
	{
		// Token: 0x06018D85 RID: 101765 RVA: 0x000E17DC File Offset: 0x000E07DC
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				175, 242, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06018D86 RID: 101766 RVA: 0x000E18F0 File Offset: 0x000E08F0
		public override void add_onabort(HTMLInputFileElementEvents_onabortEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onabortDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D87 RID: 101767 RVA: 0x000E1980 File Offset: 0x000E0980
		public override void remove_onabort(HTMLInputFileElementEvents_onabortEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onabortDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onabortDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D88 RID: 101768 RVA: 0x000E1A70 File Offset: 0x000E0A70
		public override void add_onerror(HTMLInputFileElementEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D89 RID: 101769 RVA: 0x000E1B00 File Offset: 0x000E0B00
		public override void remove_onerror(HTMLInputFileElementEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onerrorDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D8A RID: 101770 RVA: 0x000E1BF0 File Offset: 0x000E0BF0
		public override void add_onload(HTMLInputFileElementEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D8B RID: 101771 RVA: 0x000E1C80 File Offset: 0x000E0C80
		public override void remove_onload(HTMLInputFileElementEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onloadDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D8C RID: 101772 RVA: 0x000E1D70 File Offset: 0x000E0D70
		public override void add_onselect(HTMLInputFileElementEvents_onselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D8D RID: 101773 RVA: 0x000E1E00 File Offset: 0x000E0E00
		public override void remove_onselect(HTMLInputFileElementEvents_onselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onselectDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D8E RID: 101774 RVA: 0x000E1EF0 File Offset: 0x000E0EF0
		public override void add_onchange(HTMLInputFileElementEvents_onchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D8F RID: 101775 RVA: 0x000E1F80 File Offset: 0x000E0F80
		public override void remove_onchange(HTMLInputFileElementEvents_onchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onchangeDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D90 RID: 101776 RVA: 0x000E2070 File Offset: 0x000E1070
		public override void add_onfocusout(HTMLInputFileElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D91 RID: 101777 RVA: 0x000E2100 File Offset: 0x000E1100
		public override void remove_onfocusout(HTMLInputFileElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D92 RID: 101778 RVA: 0x000E21F0 File Offset: 0x000E11F0
		public override void add_onfocusin(HTMLInputFileElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D93 RID: 101779 RVA: 0x000E2280 File Offset: 0x000E1280
		public override void remove_onfocusin(HTMLInputFileElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onfocusinDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D94 RID: 101780 RVA: 0x000E2370 File Offset: 0x000E1370
		public override void add_ondeactivate(HTMLInputFileElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D95 RID: 101781 RVA: 0x000E2400 File Offset: 0x000E1400
		public override void remove_ondeactivate(HTMLInputFileElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D96 RID: 101782 RVA: 0x000E24F0 File Offset: 0x000E14F0
		public override void add_onactivate(HTMLInputFileElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D97 RID: 101783 RVA: 0x000E2580 File Offset: 0x000E1580
		public override void remove_onactivate(HTMLInputFileElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onactivateDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D98 RID: 101784 RVA: 0x000E2670 File Offset: 0x000E1670
		public override void add_onmousewheel(HTMLInputFileElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D99 RID: 101785 RVA: 0x000E2700 File Offset: 0x000E1700
		public override void remove_onmousewheel(HTMLInputFileElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D9A RID: 101786 RVA: 0x000E27F0 File Offset: 0x000E17F0
		public override void add_onmouseleave(HTMLInputFileElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D9B RID: 101787 RVA: 0x000E2880 File Offset: 0x000E1880
		public override void remove_onmouseleave(HTMLInputFileElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D9C RID: 101788 RVA: 0x000E2970 File Offset: 0x000E1970
		public override void add_onmouseenter(HTMLInputFileElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D9D RID: 101789 RVA: 0x000E2A00 File Offset: 0x000E1A00
		public override void remove_onmouseenter(HTMLInputFileElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D9E RID: 101790 RVA: 0x000E2AF0 File Offset: 0x000E1AF0
		public override void add_onresizeend(HTMLInputFileElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D9F RID: 101791 RVA: 0x000E2B80 File Offset: 0x000E1B80
		public override void remove_onresizeend(HTMLInputFileElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onresizeendDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DA0 RID: 101792 RVA: 0x000E2C70 File Offset: 0x000E1C70
		public override void add_onresizestart(HTMLInputFileElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DA1 RID: 101793 RVA: 0x000E2D00 File Offset: 0x000E1D00
		public override void remove_onresizestart(HTMLInputFileElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onresizestartDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DA2 RID: 101794 RVA: 0x000E2DF0 File Offset: 0x000E1DF0
		public override void add_onmoveend(HTMLInputFileElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DA3 RID: 101795 RVA: 0x000E2E80 File Offset: 0x000E1E80
		public override void remove_onmoveend(HTMLInputFileElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onmoveendDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DA4 RID: 101796 RVA: 0x000E2F70 File Offset: 0x000E1F70
		public override void add_onmovestart(HTMLInputFileElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DA5 RID: 101797 RVA: 0x000E3000 File Offset: 0x000E2000
		public override void remove_onmovestart(HTMLInputFileElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onmovestartDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DA6 RID: 101798 RVA: 0x000E30F0 File Offset: 0x000E20F0
		public override void add_oncontrolselect(HTMLInputFileElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DA7 RID: 101799 RVA: 0x000E3180 File Offset: 0x000E2180
		public override void remove_oncontrolselect(HTMLInputFileElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DA8 RID: 101800 RVA: 0x000E3270 File Offset: 0x000E2270
		public override void add_onmove(HTMLInputFileElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DA9 RID: 101801 RVA: 0x000E3300 File Offset: 0x000E2300
		public override void remove_onmove(HTMLInputFileElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onmoveDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DAA RID: 101802 RVA: 0x000E33F0 File Offset: 0x000E23F0
		public override void add_onbeforeactivate(HTMLInputFileElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DAB RID: 101803 RVA: 0x000E3480 File Offset: 0x000E2480
		public override void remove_onbeforeactivate(HTMLInputFileElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DAC RID: 101804 RVA: 0x000E3570 File Offset: 0x000E2570
		public override void add_onbeforedeactivate(HTMLInputFileElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DAD RID: 101805 RVA: 0x000E3600 File Offset: 0x000E2600
		public override void remove_onbeforedeactivate(HTMLInputFileElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DAE RID: 101806 RVA: 0x000E36F0 File Offset: 0x000E26F0
		public override void add_onpage(HTMLInputFileElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DAF RID: 101807 RVA: 0x000E3780 File Offset: 0x000E2780
		public override void remove_onpage(HTMLInputFileElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onpageDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DB0 RID: 101808 RVA: 0x000E3870 File Offset: 0x000E2870
		public override void add_onlayoutcomplete(HTMLInputFileElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DB1 RID: 101809 RVA: 0x000E3900 File Offset: 0x000E2900
		public override void remove_onlayoutcomplete(HTMLInputFileElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DB2 RID: 101810 RVA: 0x000E39F0 File Offset: 0x000E29F0
		public override void add_onbeforeeditfocus(HTMLInputFileElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DB3 RID: 101811 RVA: 0x000E3A80 File Offset: 0x000E2A80
		public override void remove_onbeforeeditfocus(HTMLInputFileElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DB4 RID: 101812 RVA: 0x000E3B70 File Offset: 0x000E2B70
		public override void add_onreadystatechange(HTMLInputFileElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DB5 RID: 101813 RVA: 0x000E3C00 File Offset: 0x000E2C00
		public override void remove_onreadystatechange(HTMLInputFileElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DB6 RID: 101814 RVA: 0x000E3CF0 File Offset: 0x000E2CF0
		public override void add_oncellchange(HTMLInputFileElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DB7 RID: 101815 RVA: 0x000E3D80 File Offset: 0x000E2D80
		public override void remove_oncellchange(HTMLInputFileElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DB8 RID: 101816 RVA: 0x000E3E70 File Offset: 0x000E2E70
		public override void add_onrowsinserted(HTMLInputFileElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DB9 RID: 101817 RVA: 0x000E3F00 File Offset: 0x000E2F00
		public override void remove_onrowsinserted(HTMLInputFileElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DBA RID: 101818 RVA: 0x000E3FF0 File Offset: 0x000E2FF0
		public override void add_onrowsdelete(HTMLInputFileElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DBB RID: 101819 RVA: 0x000E4080 File Offset: 0x000E3080
		public override void remove_onrowsdelete(HTMLInputFileElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DBC RID: 101820 RVA: 0x000E4170 File Offset: 0x000E3170
		public override void add_oncontextmenu(HTMLInputFileElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DBD RID: 101821 RVA: 0x000E4200 File Offset: 0x000E3200
		public override void remove_oncontextmenu(HTMLInputFileElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DBE RID: 101822 RVA: 0x000E42F0 File Offset: 0x000E32F0
		public override void add_onpaste(HTMLInputFileElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DBF RID: 101823 RVA: 0x000E4380 File Offset: 0x000E3380
		public override void remove_onpaste(HTMLInputFileElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onpasteDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DC0 RID: 101824 RVA: 0x000E4470 File Offset: 0x000E3470
		public override void add_onbeforepaste(HTMLInputFileElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DC1 RID: 101825 RVA: 0x000E4500 File Offset: 0x000E3500
		public override void remove_onbeforepaste(HTMLInputFileElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DC2 RID: 101826 RVA: 0x000E45F0 File Offset: 0x000E35F0
		public override void add_oncopy(HTMLInputFileElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DC3 RID: 101827 RVA: 0x000E4680 File Offset: 0x000E3680
		public override void remove_oncopy(HTMLInputFileElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_oncopyDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DC4 RID: 101828 RVA: 0x000E4770 File Offset: 0x000E3770
		public override void add_onbeforecopy(HTMLInputFileElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DC5 RID: 101829 RVA: 0x000E4800 File Offset: 0x000E3800
		public override void remove_onbeforecopy(HTMLInputFileElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DC6 RID: 101830 RVA: 0x000E48F0 File Offset: 0x000E38F0
		public override void add_oncut(HTMLInputFileElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DC7 RID: 101831 RVA: 0x000E4980 File Offset: 0x000E3980
		public override void remove_oncut(HTMLInputFileElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_oncutDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DC8 RID: 101832 RVA: 0x000E4A70 File Offset: 0x000E3A70
		public override void add_onbeforecut(HTMLInputFileElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DC9 RID: 101833 RVA: 0x000E4B00 File Offset: 0x000E3B00
		public override void remove_onbeforecut(HTMLInputFileElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DCA RID: 101834 RVA: 0x000E4BF0 File Offset: 0x000E3BF0
		public override void add_ondrop(HTMLInputFileElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DCB RID: 101835 RVA: 0x000E4C80 File Offset: 0x000E3C80
		public override void remove_ondrop(HTMLInputFileElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_ondropDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DCC RID: 101836 RVA: 0x000E4D70 File Offset: 0x000E3D70
		public override void add_ondragleave(HTMLInputFileElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DCD RID: 101837 RVA: 0x000E4E00 File Offset: 0x000E3E00
		public override void remove_ondragleave(HTMLInputFileElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DCE RID: 101838 RVA: 0x000E4EF0 File Offset: 0x000E3EF0
		public override void add_ondragover(HTMLInputFileElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DCF RID: 101839 RVA: 0x000E4F80 File Offset: 0x000E3F80
		public override void remove_ondragover(HTMLInputFileElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_ondragoverDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DD0 RID: 101840 RVA: 0x000E5070 File Offset: 0x000E4070
		public override void add_ondragenter(HTMLInputFileElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DD1 RID: 101841 RVA: 0x000E5100 File Offset: 0x000E4100
		public override void remove_ondragenter(HTMLInputFileElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_ondragenterDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DD2 RID: 101842 RVA: 0x000E51F0 File Offset: 0x000E41F0
		public override void add_ondragend(HTMLInputFileElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DD3 RID: 101843 RVA: 0x000E5280 File Offset: 0x000E4280
		public override void remove_ondragend(HTMLInputFileElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_ondragendDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DD4 RID: 101844 RVA: 0x000E5370 File Offset: 0x000E4370
		public override void add_ondrag(HTMLInputFileElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DD5 RID: 101845 RVA: 0x000E5400 File Offset: 0x000E4400
		public override void remove_ondrag(HTMLInputFileElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_ondragDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DD6 RID: 101846 RVA: 0x000E54F0 File Offset: 0x000E44F0
		public override void add_onresize(HTMLInputFileElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DD7 RID: 101847 RVA: 0x000E5580 File Offset: 0x000E4580
		public override void remove_onresize(HTMLInputFileElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onresizeDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DD8 RID: 101848 RVA: 0x000E5670 File Offset: 0x000E4670
		public override void add_onblur(HTMLInputFileElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DD9 RID: 101849 RVA: 0x000E5700 File Offset: 0x000E4700
		public override void remove_onblur(HTMLInputFileElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onblurDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DDA RID: 101850 RVA: 0x000E57F0 File Offset: 0x000E47F0
		public override void add_onfocus(HTMLInputFileElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DDB RID: 101851 RVA: 0x000E5880 File Offset: 0x000E4880
		public override void remove_onfocus(HTMLInputFileElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onfocusDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DDC RID: 101852 RVA: 0x000E5970 File Offset: 0x000E4970
		public override void add_onscroll(HTMLInputFileElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DDD RID: 101853 RVA: 0x000E5A00 File Offset: 0x000E4A00
		public override void remove_onscroll(HTMLInputFileElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onscrollDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DDE RID: 101854 RVA: 0x000E5AF0 File Offset: 0x000E4AF0
		public override void add_onpropertychange(HTMLInputFileElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DDF RID: 101855 RVA: 0x000E5B80 File Offset: 0x000E4B80
		public override void remove_onpropertychange(HTMLInputFileElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DE0 RID: 101856 RVA: 0x000E5C70 File Offset: 0x000E4C70
		public override void add_onlosecapture(HTMLInputFileElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DE1 RID: 101857 RVA: 0x000E5D00 File Offset: 0x000E4D00
		public override void remove_onlosecapture(HTMLInputFileElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DE2 RID: 101858 RVA: 0x000E5DF0 File Offset: 0x000E4DF0
		public override void add_ondatasetcomplete(HTMLInputFileElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DE3 RID: 101859 RVA: 0x000E5E80 File Offset: 0x000E4E80
		public override void remove_ondatasetcomplete(HTMLInputFileElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DE4 RID: 101860 RVA: 0x000E5F70 File Offset: 0x000E4F70
		public override void add_ondataavailable(HTMLInputFileElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DE5 RID: 101861 RVA: 0x000E6000 File Offset: 0x000E5000
		public override void remove_ondataavailable(HTMLInputFileElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DE6 RID: 101862 RVA: 0x000E60F0 File Offset: 0x000E50F0
		public override void add_ondatasetchanged(HTMLInputFileElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DE7 RID: 101863 RVA: 0x000E6180 File Offset: 0x000E5180
		public override void remove_ondatasetchanged(HTMLInputFileElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DE8 RID: 101864 RVA: 0x000E6270 File Offset: 0x000E5270
		public override void add_onrowenter(HTMLInputFileElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DE9 RID: 101865 RVA: 0x000E6300 File Offset: 0x000E5300
		public override void remove_onrowenter(HTMLInputFileElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DEA RID: 101866 RVA: 0x000E63F0 File Offset: 0x000E53F0
		public override void add_onrowexit(HTMLInputFileElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DEB RID: 101867 RVA: 0x000E6480 File Offset: 0x000E5480
		public override void remove_onrowexit(HTMLInputFileElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DEC RID: 101868 RVA: 0x000E6570 File Offset: 0x000E5570
		public override void add_onerrorupdate(HTMLInputFileElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DED RID: 101869 RVA: 0x000E6600 File Offset: 0x000E5600
		public override void remove_onerrorupdate(HTMLInputFileElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DEE RID: 101870 RVA: 0x000E66F0 File Offset: 0x000E56F0
		public override void add_onafterupdate(HTMLInputFileElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DEF RID: 101871 RVA: 0x000E6780 File Offset: 0x000E5780
		public override void remove_onafterupdate(HTMLInputFileElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DF0 RID: 101872 RVA: 0x000E6870 File Offset: 0x000E5870
		public override void add_onbeforeupdate(HTMLInputFileElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DF1 RID: 101873 RVA: 0x000E6900 File Offset: 0x000E5900
		public override void remove_onbeforeupdate(HTMLInputFileElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DF2 RID: 101874 RVA: 0x000E69F0 File Offset: 0x000E59F0
		public override void add_ondragstart(HTMLInputFileElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DF3 RID: 101875 RVA: 0x000E6A80 File Offset: 0x000E5A80
		public override void remove_ondragstart(HTMLInputFileElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_ondragstartDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DF4 RID: 101876 RVA: 0x000E6B70 File Offset: 0x000E5B70
		public override void add_onfilterchange(HTMLInputFileElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DF5 RID: 101877 RVA: 0x000E6C00 File Offset: 0x000E5C00
		public override void remove_onfilterchange(HTMLInputFileElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DF6 RID: 101878 RVA: 0x000E6CF0 File Offset: 0x000E5CF0
		public override void add_onselectstart(HTMLInputFileElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DF7 RID: 101879 RVA: 0x000E6D80 File Offset: 0x000E5D80
		public override void remove_onselectstart(HTMLInputFileElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onselectstartDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DF8 RID: 101880 RVA: 0x000E6E70 File Offset: 0x000E5E70
		public override void add_onmouseup(HTMLInputFileElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DF9 RID: 101881 RVA: 0x000E6F00 File Offset: 0x000E5F00
		public override void remove_onmouseup(HTMLInputFileElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onmouseupDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DFA RID: 101882 RVA: 0x000E6FF0 File Offset: 0x000E5FF0
		public override void add_onmousedown(HTMLInputFileElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DFB RID: 101883 RVA: 0x000E7080 File Offset: 0x000E6080
		public override void remove_onmousedown(HTMLInputFileElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onmousedownDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DFC RID: 101884 RVA: 0x000E7170 File Offset: 0x000E6170
		public override void add_onmousemove(HTMLInputFileElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DFD RID: 101885 RVA: 0x000E7200 File Offset: 0x000E6200
		public override void remove_onmousemove(HTMLInputFileElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018DFE RID: 101886 RVA: 0x000E72F0 File Offset: 0x000E62F0
		public override void add_onmouseover(HTMLInputFileElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018DFF RID: 101887 RVA: 0x000E7380 File Offset: 0x000E6380
		public override void remove_onmouseover(HTMLInputFileElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E00 RID: 101888 RVA: 0x000E7470 File Offset: 0x000E6470
		public override void add_onmouseout(HTMLInputFileElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018E01 RID: 101889 RVA: 0x000E7500 File Offset: 0x000E6500
		public override void remove_onmouseout(HTMLInputFileElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E02 RID: 101890 RVA: 0x000E75F0 File Offset: 0x000E65F0
		public override void add_onkeyup(HTMLInputFileElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018E03 RID: 101891 RVA: 0x000E7680 File Offset: 0x000E6680
		public override void remove_onkeyup(HTMLInputFileElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onkeyupDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E04 RID: 101892 RVA: 0x000E7770 File Offset: 0x000E6770
		public override void add_onkeydown(HTMLInputFileElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018E05 RID: 101893 RVA: 0x000E7800 File Offset: 0x000E6800
		public override void remove_onkeydown(HTMLInputFileElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onkeydownDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E06 RID: 101894 RVA: 0x000E78F0 File Offset: 0x000E68F0
		public override void add_onkeypress(HTMLInputFileElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018E07 RID: 101895 RVA: 0x000E7980 File Offset: 0x000E6980
		public override void remove_onkeypress(HTMLInputFileElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onkeypressDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E08 RID: 101896 RVA: 0x000E7A70 File Offset: 0x000E6A70
		public override void add_ondblclick(HTMLInputFileElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018E09 RID: 101897 RVA: 0x000E7B00 File Offset: 0x000E6B00
		public override void remove_ondblclick(HTMLInputFileElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_ondblclickDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E0A RID: 101898 RVA: 0x000E7BF0 File Offset: 0x000E6BF0
		public override void add_onclick(HTMLInputFileElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018E0B RID: 101899 RVA: 0x000E7C80 File Offset: 0x000E6C80
		public override void remove_onclick(HTMLInputFileElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onclickDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E0C RID: 101900 RVA: 0x000E7D70 File Offset: 0x000E6D70
		public override void add_onhelp(HTMLInputFileElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = new HTMLInputFileElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputFileElementEvents_SinkHelper, ref num);
				htmlinputFileElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputFileElementEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputFileElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018E0D RID: 101901 RVA: 0x000E7E00 File Offset: 0x000E6E00
		public override void remove_onhelp(HTMLInputFileElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputFileElementEvents_SinkHelper.m_onhelpDelegate != null && ((htmlinputFileElementEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E0E RID: 101902 RVA: 0x000E7EF0 File Offset: 0x000E6EF0
		public HTMLInputFileElementEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06018E0F RID: 101903 RVA: 0x000E7F18 File Offset: 0x000E6F18
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
								HTMLInputFileElementEvents_SinkHelper htmlinputFileElementEvents_SinkHelper = (HTMLInputFileElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlinputFileElementEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x06018E10 RID: 101904 RVA: 0x000E7FCC File Offset: 0x000E6FCC
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000D79 RID: 3449
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000D7A RID: 3450
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000D7B RID: 3451
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
