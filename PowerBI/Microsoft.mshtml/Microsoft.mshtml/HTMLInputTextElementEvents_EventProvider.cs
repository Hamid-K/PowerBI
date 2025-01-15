using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DCD RID: 3533
	internal sealed class HTMLInputTextElementEvents_EventProvider : HTMLInputTextElementEvents_Event, IDisposable
	{
		// Token: 0x06017926 RID: 96550 RVA: 0x00028028 File Offset: 0x00027028
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				167, 242, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06017927 RID: 96551 RVA: 0x0002813C File Offset: 0x0002713C
		public override void add_onabort(HTMLInputTextElementEvents_onabortEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onabortDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017928 RID: 96552 RVA: 0x000281CC File Offset: 0x000271CC
		public override void remove_onabort(HTMLInputTextElementEvents_onabortEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onabortDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onabortDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017929 RID: 96553 RVA: 0x000282BC File Offset: 0x000272BC
		public override void add_onerror(HTMLInputTextElementEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601792A RID: 96554 RVA: 0x0002834C File Offset: 0x0002734C
		public override void remove_onerror(HTMLInputTextElementEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onerrorDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601792B RID: 96555 RVA: 0x0002843C File Offset: 0x0002743C
		public override void add_onload(HTMLInputTextElementEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601792C RID: 96556 RVA: 0x000284CC File Offset: 0x000274CC
		public override void remove_onload(HTMLInputTextElementEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onloadDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601792D RID: 96557 RVA: 0x000285BC File Offset: 0x000275BC
		public override void add_onselect(HTMLInputTextElementEvents_onselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601792E RID: 96558 RVA: 0x0002864C File Offset: 0x0002764C
		public override void remove_onselect(HTMLInputTextElementEvents_onselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onselectDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601792F RID: 96559 RVA: 0x0002873C File Offset: 0x0002773C
		public override void add_onchange(HTMLInputTextElementEvents_onchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017930 RID: 96560 RVA: 0x000287CC File Offset: 0x000277CC
		public override void remove_onchange(HTMLInputTextElementEvents_onchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onchangeDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017931 RID: 96561 RVA: 0x000288BC File Offset: 0x000278BC
		public override void add_onfocusout(HTMLInputTextElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017932 RID: 96562 RVA: 0x0002894C File Offset: 0x0002794C
		public override void remove_onfocusout(HTMLInputTextElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017933 RID: 96563 RVA: 0x00028A3C File Offset: 0x00027A3C
		public override void add_onfocusin(HTMLInputTextElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017934 RID: 96564 RVA: 0x00028ACC File Offset: 0x00027ACC
		public override void remove_onfocusin(HTMLInputTextElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onfocusinDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017935 RID: 96565 RVA: 0x00028BBC File Offset: 0x00027BBC
		public override void add_ondeactivate(HTMLInputTextElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017936 RID: 96566 RVA: 0x00028C4C File Offset: 0x00027C4C
		public override void remove_ondeactivate(HTMLInputTextElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017937 RID: 96567 RVA: 0x00028D3C File Offset: 0x00027D3C
		public override void add_onactivate(HTMLInputTextElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017938 RID: 96568 RVA: 0x00028DCC File Offset: 0x00027DCC
		public override void remove_onactivate(HTMLInputTextElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onactivateDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017939 RID: 96569 RVA: 0x00028EBC File Offset: 0x00027EBC
		public override void add_onmousewheel(HTMLInputTextElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601793A RID: 96570 RVA: 0x00028F4C File Offset: 0x00027F4C
		public override void remove_onmousewheel(HTMLInputTextElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601793B RID: 96571 RVA: 0x0002903C File Offset: 0x0002803C
		public override void add_onmouseleave(HTMLInputTextElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601793C RID: 96572 RVA: 0x000290CC File Offset: 0x000280CC
		public override void remove_onmouseleave(HTMLInputTextElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601793D RID: 96573 RVA: 0x000291BC File Offset: 0x000281BC
		public override void add_onmouseenter(HTMLInputTextElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601793E RID: 96574 RVA: 0x0002924C File Offset: 0x0002824C
		public override void remove_onmouseenter(HTMLInputTextElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601793F RID: 96575 RVA: 0x0002933C File Offset: 0x0002833C
		public override void add_onresizeend(HTMLInputTextElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017940 RID: 96576 RVA: 0x000293CC File Offset: 0x000283CC
		public override void remove_onresizeend(HTMLInputTextElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onresizeendDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017941 RID: 96577 RVA: 0x000294BC File Offset: 0x000284BC
		public override void add_onresizestart(HTMLInputTextElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017942 RID: 96578 RVA: 0x0002954C File Offset: 0x0002854C
		public override void remove_onresizestart(HTMLInputTextElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onresizestartDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017943 RID: 96579 RVA: 0x0002963C File Offset: 0x0002863C
		public override void add_onmoveend(HTMLInputTextElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017944 RID: 96580 RVA: 0x000296CC File Offset: 0x000286CC
		public override void remove_onmoveend(HTMLInputTextElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onmoveendDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017945 RID: 96581 RVA: 0x000297BC File Offset: 0x000287BC
		public override void add_onmovestart(HTMLInputTextElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017946 RID: 96582 RVA: 0x0002984C File Offset: 0x0002884C
		public override void remove_onmovestart(HTMLInputTextElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onmovestartDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017947 RID: 96583 RVA: 0x0002993C File Offset: 0x0002893C
		public override void add_oncontrolselect(HTMLInputTextElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017948 RID: 96584 RVA: 0x000299CC File Offset: 0x000289CC
		public override void remove_oncontrolselect(HTMLInputTextElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017949 RID: 96585 RVA: 0x00029ABC File Offset: 0x00028ABC
		public override void add_onmove(HTMLInputTextElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601794A RID: 96586 RVA: 0x00029B4C File Offset: 0x00028B4C
		public override void remove_onmove(HTMLInputTextElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onmoveDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601794B RID: 96587 RVA: 0x00029C3C File Offset: 0x00028C3C
		public override void add_onbeforeactivate(HTMLInputTextElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601794C RID: 96588 RVA: 0x00029CCC File Offset: 0x00028CCC
		public override void remove_onbeforeactivate(HTMLInputTextElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601794D RID: 96589 RVA: 0x00029DBC File Offset: 0x00028DBC
		public override void add_onbeforedeactivate(HTMLInputTextElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601794E RID: 96590 RVA: 0x00029E4C File Offset: 0x00028E4C
		public override void remove_onbeforedeactivate(HTMLInputTextElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601794F RID: 96591 RVA: 0x00029F3C File Offset: 0x00028F3C
		public override void add_onpage(HTMLInputTextElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017950 RID: 96592 RVA: 0x00029FCC File Offset: 0x00028FCC
		public override void remove_onpage(HTMLInputTextElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onpageDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017951 RID: 96593 RVA: 0x0002A0BC File Offset: 0x000290BC
		public override void add_onlayoutcomplete(HTMLInputTextElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017952 RID: 96594 RVA: 0x0002A14C File Offset: 0x0002914C
		public override void remove_onlayoutcomplete(HTMLInputTextElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017953 RID: 96595 RVA: 0x0002A23C File Offset: 0x0002923C
		public override void add_onbeforeeditfocus(HTMLInputTextElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017954 RID: 96596 RVA: 0x0002A2CC File Offset: 0x000292CC
		public override void remove_onbeforeeditfocus(HTMLInputTextElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017955 RID: 96597 RVA: 0x0002A3BC File Offset: 0x000293BC
		public override void add_onreadystatechange(HTMLInputTextElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017956 RID: 96598 RVA: 0x0002A44C File Offset: 0x0002944C
		public override void remove_onreadystatechange(HTMLInputTextElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017957 RID: 96599 RVA: 0x0002A53C File Offset: 0x0002953C
		public override void add_oncellchange(HTMLInputTextElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017958 RID: 96600 RVA: 0x0002A5CC File Offset: 0x000295CC
		public override void remove_oncellchange(HTMLInputTextElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017959 RID: 96601 RVA: 0x0002A6BC File Offset: 0x000296BC
		public override void add_onrowsinserted(HTMLInputTextElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601795A RID: 96602 RVA: 0x0002A74C File Offset: 0x0002974C
		public override void remove_onrowsinserted(HTMLInputTextElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601795B RID: 96603 RVA: 0x0002A83C File Offset: 0x0002983C
		public override void add_onrowsdelete(HTMLInputTextElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601795C RID: 96604 RVA: 0x0002A8CC File Offset: 0x000298CC
		public override void remove_onrowsdelete(HTMLInputTextElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601795D RID: 96605 RVA: 0x0002A9BC File Offset: 0x000299BC
		public override void add_oncontextmenu(HTMLInputTextElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601795E RID: 96606 RVA: 0x0002AA4C File Offset: 0x00029A4C
		public override void remove_oncontextmenu(HTMLInputTextElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601795F RID: 96607 RVA: 0x0002AB3C File Offset: 0x00029B3C
		public override void add_onpaste(HTMLInputTextElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017960 RID: 96608 RVA: 0x0002ABCC File Offset: 0x00029BCC
		public override void remove_onpaste(HTMLInputTextElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onpasteDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017961 RID: 96609 RVA: 0x0002ACBC File Offset: 0x00029CBC
		public override void add_onbeforepaste(HTMLInputTextElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017962 RID: 96610 RVA: 0x0002AD4C File Offset: 0x00029D4C
		public override void remove_onbeforepaste(HTMLInputTextElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017963 RID: 96611 RVA: 0x0002AE3C File Offset: 0x00029E3C
		public override void add_oncopy(HTMLInputTextElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017964 RID: 96612 RVA: 0x0002AECC File Offset: 0x00029ECC
		public override void remove_oncopy(HTMLInputTextElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_oncopyDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017965 RID: 96613 RVA: 0x0002AFBC File Offset: 0x00029FBC
		public override void add_onbeforecopy(HTMLInputTextElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017966 RID: 96614 RVA: 0x0002B04C File Offset: 0x0002A04C
		public override void remove_onbeforecopy(HTMLInputTextElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017967 RID: 96615 RVA: 0x0002B13C File Offset: 0x0002A13C
		public override void add_oncut(HTMLInputTextElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017968 RID: 96616 RVA: 0x0002B1CC File Offset: 0x0002A1CC
		public override void remove_oncut(HTMLInputTextElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_oncutDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017969 RID: 96617 RVA: 0x0002B2BC File Offset: 0x0002A2BC
		public override void add_onbeforecut(HTMLInputTextElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601796A RID: 96618 RVA: 0x0002B34C File Offset: 0x0002A34C
		public override void remove_onbeforecut(HTMLInputTextElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601796B RID: 96619 RVA: 0x0002B43C File Offset: 0x0002A43C
		public override void add_ondrop(HTMLInputTextElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601796C RID: 96620 RVA: 0x0002B4CC File Offset: 0x0002A4CC
		public override void remove_ondrop(HTMLInputTextElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_ondropDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601796D RID: 96621 RVA: 0x0002B5BC File Offset: 0x0002A5BC
		public override void add_ondragleave(HTMLInputTextElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601796E RID: 96622 RVA: 0x0002B64C File Offset: 0x0002A64C
		public override void remove_ondragleave(HTMLInputTextElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601796F RID: 96623 RVA: 0x0002B73C File Offset: 0x0002A73C
		public override void add_ondragover(HTMLInputTextElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017970 RID: 96624 RVA: 0x0002B7CC File Offset: 0x0002A7CC
		public override void remove_ondragover(HTMLInputTextElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_ondragoverDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017971 RID: 96625 RVA: 0x0002B8BC File Offset: 0x0002A8BC
		public override void add_ondragenter(HTMLInputTextElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017972 RID: 96626 RVA: 0x0002B94C File Offset: 0x0002A94C
		public override void remove_ondragenter(HTMLInputTextElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_ondragenterDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017973 RID: 96627 RVA: 0x0002BA3C File Offset: 0x0002AA3C
		public override void add_ondragend(HTMLInputTextElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017974 RID: 96628 RVA: 0x0002BACC File Offset: 0x0002AACC
		public override void remove_ondragend(HTMLInputTextElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_ondragendDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017975 RID: 96629 RVA: 0x0002BBBC File Offset: 0x0002ABBC
		public override void add_ondrag(HTMLInputTextElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017976 RID: 96630 RVA: 0x0002BC4C File Offset: 0x0002AC4C
		public override void remove_ondrag(HTMLInputTextElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_ondragDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017977 RID: 96631 RVA: 0x0002BD3C File Offset: 0x0002AD3C
		public override void add_onresize(HTMLInputTextElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017978 RID: 96632 RVA: 0x0002BDCC File Offset: 0x0002ADCC
		public override void remove_onresize(HTMLInputTextElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onresizeDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017979 RID: 96633 RVA: 0x0002BEBC File Offset: 0x0002AEBC
		public override void add_onblur(HTMLInputTextElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601797A RID: 96634 RVA: 0x0002BF4C File Offset: 0x0002AF4C
		public override void remove_onblur(HTMLInputTextElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onblurDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601797B RID: 96635 RVA: 0x0002C03C File Offset: 0x0002B03C
		public override void add_onfocus(HTMLInputTextElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601797C RID: 96636 RVA: 0x0002C0CC File Offset: 0x0002B0CC
		public override void remove_onfocus(HTMLInputTextElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onfocusDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601797D RID: 96637 RVA: 0x0002C1BC File Offset: 0x0002B1BC
		public override void add_onscroll(HTMLInputTextElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601797E RID: 96638 RVA: 0x0002C24C File Offset: 0x0002B24C
		public override void remove_onscroll(HTMLInputTextElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onscrollDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601797F RID: 96639 RVA: 0x0002C33C File Offset: 0x0002B33C
		public override void add_onpropertychange(HTMLInputTextElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017980 RID: 96640 RVA: 0x0002C3CC File Offset: 0x0002B3CC
		public override void remove_onpropertychange(HTMLInputTextElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017981 RID: 96641 RVA: 0x0002C4BC File Offset: 0x0002B4BC
		public override void add_onlosecapture(HTMLInputTextElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017982 RID: 96642 RVA: 0x0002C54C File Offset: 0x0002B54C
		public override void remove_onlosecapture(HTMLInputTextElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017983 RID: 96643 RVA: 0x0002C63C File Offset: 0x0002B63C
		public override void add_ondatasetcomplete(HTMLInputTextElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017984 RID: 96644 RVA: 0x0002C6CC File Offset: 0x0002B6CC
		public override void remove_ondatasetcomplete(HTMLInputTextElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017985 RID: 96645 RVA: 0x0002C7BC File Offset: 0x0002B7BC
		public override void add_ondataavailable(HTMLInputTextElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017986 RID: 96646 RVA: 0x0002C84C File Offset: 0x0002B84C
		public override void remove_ondataavailable(HTMLInputTextElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017987 RID: 96647 RVA: 0x0002C93C File Offset: 0x0002B93C
		public override void add_ondatasetchanged(HTMLInputTextElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017988 RID: 96648 RVA: 0x0002C9CC File Offset: 0x0002B9CC
		public override void remove_ondatasetchanged(HTMLInputTextElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017989 RID: 96649 RVA: 0x0002CABC File Offset: 0x0002BABC
		public override void add_onrowenter(HTMLInputTextElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601798A RID: 96650 RVA: 0x0002CB4C File Offset: 0x0002BB4C
		public override void remove_onrowenter(HTMLInputTextElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601798B RID: 96651 RVA: 0x0002CC3C File Offset: 0x0002BC3C
		public override void add_onrowexit(HTMLInputTextElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601798C RID: 96652 RVA: 0x0002CCCC File Offset: 0x0002BCCC
		public override void remove_onrowexit(HTMLInputTextElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601798D RID: 96653 RVA: 0x0002CDBC File Offset: 0x0002BDBC
		public override void add_onerrorupdate(HTMLInputTextElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601798E RID: 96654 RVA: 0x0002CE4C File Offset: 0x0002BE4C
		public override void remove_onerrorupdate(HTMLInputTextElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601798F RID: 96655 RVA: 0x0002CF3C File Offset: 0x0002BF3C
		public override void add_onafterupdate(HTMLInputTextElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017990 RID: 96656 RVA: 0x0002CFCC File Offset: 0x0002BFCC
		public override void remove_onafterupdate(HTMLInputTextElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017991 RID: 96657 RVA: 0x0002D0BC File Offset: 0x0002C0BC
		public override void add_onbeforeupdate(HTMLInputTextElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017992 RID: 96658 RVA: 0x0002D14C File Offset: 0x0002C14C
		public override void remove_onbeforeupdate(HTMLInputTextElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017993 RID: 96659 RVA: 0x0002D23C File Offset: 0x0002C23C
		public override void add_ondragstart(HTMLInputTextElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017994 RID: 96660 RVA: 0x0002D2CC File Offset: 0x0002C2CC
		public override void remove_ondragstart(HTMLInputTextElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_ondragstartDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017995 RID: 96661 RVA: 0x0002D3BC File Offset: 0x0002C3BC
		public override void add_onfilterchange(HTMLInputTextElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017996 RID: 96662 RVA: 0x0002D44C File Offset: 0x0002C44C
		public override void remove_onfilterchange(HTMLInputTextElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017997 RID: 96663 RVA: 0x0002D53C File Offset: 0x0002C53C
		public override void add_onselectstart(HTMLInputTextElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017998 RID: 96664 RVA: 0x0002D5CC File Offset: 0x0002C5CC
		public override void remove_onselectstart(HTMLInputTextElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onselectstartDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017999 RID: 96665 RVA: 0x0002D6BC File Offset: 0x0002C6BC
		public override void add_onmouseup(HTMLInputTextElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601799A RID: 96666 RVA: 0x0002D74C File Offset: 0x0002C74C
		public override void remove_onmouseup(HTMLInputTextElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onmouseupDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601799B RID: 96667 RVA: 0x0002D83C File Offset: 0x0002C83C
		public override void add_onmousedown(HTMLInputTextElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601799C RID: 96668 RVA: 0x0002D8CC File Offset: 0x0002C8CC
		public override void remove_onmousedown(HTMLInputTextElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onmousedownDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601799D RID: 96669 RVA: 0x0002D9BC File Offset: 0x0002C9BC
		public override void add_onmousemove(HTMLInputTextElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601799E RID: 96670 RVA: 0x0002DA4C File Offset: 0x0002CA4C
		public override void remove_onmousemove(HTMLInputTextElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601799F RID: 96671 RVA: 0x0002DB3C File Offset: 0x0002CB3C
		public override void add_onmouseover(HTMLInputTextElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x060179A0 RID: 96672 RVA: 0x0002DBCC File Offset: 0x0002CBCC
		public override void remove_onmouseover(HTMLInputTextElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060179A1 RID: 96673 RVA: 0x0002DCBC File Offset: 0x0002CCBC
		public override void add_onmouseout(HTMLInputTextElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x060179A2 RID: 96674 RVA: 0x0002DD4C File Offset: 0x0002CD4C
		public override void remove_onmouseout(HTMLInputTextElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060179A3 RID: 96675 RVA: 0x0002DE3C File Offset: 0x0002CE3C
		public override void add_onkeyup(HTMLInputTextElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x060179A4 RID: 96676 RVA: 0x0002DECC File Offset: 0x0002CECC
		public override void remove_onkeyup(HTMLInputTextElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onkeyupDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060179A5 RID: 96677 RVA: 0x0002DFBC File Offset: 0x0002CFBC
		public override void add_onkeydown(HTMLInputTextElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x060179A6 RID: 96678 RVA: 0x0002E04C File Offset: 0x0002D04C
		public override void remove_onkeydown(HTMLInputTextElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onkeydownDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060179A7 RID: 96679 RVA: 0x0002E13C File Offset: 0x0002D13C
		public override void add_onkeypress(HTMLInputTextElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x060179A8 RID: 96680 RVA: 0x0002E1CC File Offset: 0x0002D1CC
		public override void remove_onkeypress(HTMLInputTextElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onkeypressDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060179A9 RID: 96681 RVA: 0x0002E2BC File Offset: 0x0002D2BC
		public override void add_ondblclick(HTMLInputTextElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x060179AA RID: 96682 RVA: 0x0002E34C File Offset: 0x0002D34C
		public override void remove_ondblclick(HTMLInputTextElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_ondblclickDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060179AB RID: 96683 RVA: 0x0002E43C File Offset: 0x0002D43C
		public override void add_onclick(HTMLInputTextElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x060179AC RID: 96684 RVA: 0x0002E4CC File Offset: 0x0002D4CC
		public override void remove_onclick(HTMLInputTextElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onclickDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060179AD RID: 96685 RVA: 0x0002E5BC File Offset: 0x0002D5BC
		public override void add_onhelp(HTMLInputTextElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = new HTMLInputTextElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents_SinkHelper, ref num);
				htmlinputTextElementEvents_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents_SinkHelper);
			}
		}

		// Token: 0x060179AE RID: 96686 RVA: 0x0002E64C File Offset: 0x0002D64C
		public override void remove_onhelp(HTMLInputTextElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents_SinkHelper.m_onhelpDelegate != null && ((htmlinputTextElementEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060179AF RID: 96687 RVA: 0x0002E73C File Offset: 0x0002D73C
		public HTMLInputTextElementEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x060179B0 RID: 96688 RVA: 0x0002E764 File Offset: 0x0002D764
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
								HTMLInputTextElementEvents_SinkHelper htmlinputTextElementEvents_SinkHelper = (HTMLInputTextElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x060179B1 RID: 96689 RVA: 0x0002E818 File Offset: 0x0002D818
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400066B RID: 1643
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x0400066C RID: 1644
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x0400066D RID: 1645
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
