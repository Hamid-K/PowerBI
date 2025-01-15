using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DC7 RID: 3527
	internal sealed class HTMLSelectElementEvents2_EventProvider : HTMLSelectElementEvents2_Event, IDisposable
	{
		// Token: 0x06017777 RID: 96119 RVA: 0x00018E08 File Offset: 0x00017E08
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				34, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06017778 RID: 96120 RVA: 0x00018F1C File Offset: 0x00017F1C
		public override void add_onchange(HTMLSelectElementEvents2_onchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017779 RID: 96121 RVA: 0x00018FAC File Offset: 0x00017FAC
		public override void remove_onchange(HTMLSelectElementEvents2_onchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onchangeDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601777A RID: 96122 RVA: 0x0001909C File Offset: 0x0001809C
		public override void add_onmousewheel(HTMLSelectElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601777B RID: 96123 RVA: 0x0001912C File Offset: 0x0001812C
		public override void remove_onmousewheel(HTMLSelectElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601777C RID: 96124 RVA: 0x0001921C File Offset: 0x0001821C
		public override void add_onresizeend(HTMLSelectElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601777D RID: 96125 RVA: 0x000192AC File Offset: 0x000182AC
		public override void remove_onresizeend(HTMLSelectElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601777E RID: 96126 RVA: 0x0001939C File Offset: 0x0001839C
		public override void add_onresizestart(HTMLSelectElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601777F RID: 96127 RVA: 0x0001942C File Offset: 0x0001842C
		public override void remove_onresizestart(HTMLSelectElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017780 RID: 96128 RVA: 0x0001951C File Offset: 0x0001851C
		public override void add_onmoveend(HTMLSelectElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017781 RID: 96129 RVA: 0x000195AC File Offset: 0x000185AC
		public override void remove_onmoveend(HTMLSelectElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017782 RID: 96130 RVA: 0x0001969C File Offset: 0x0001869C
		public override void add_onmovestart(HTMLSelectElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017783 RID: 96131 RVA: 0x0001972C File Offset: 0x0001872C
		public override void remove_onmovestart(HTMLSelectElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017784 RID: 96132 RVA: 0x0001981C File Offset: 0x0001881C
		public override void add_oncontrolselect(HTMLSelectElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017785 RID: 96133 RVA: 0x000198AC File Offset: 0x000188AC
		public override void remove_oncontrolselect(HTMLSelectElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017786 RID: 96134 RVA: 0x0001999C File Offset: 0x0001899C
		public override void add_onmove(HTMLSelectElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017787 RID: 96135 RVA: 0x00019A2C File Offset: 0x00018A2C
		public override void remove_onmove(HTMLSelectElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onmoveDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017788 RID: 96136 RVA: 0x00019B1C File Offset: 0x00018B1C
		public override void add_onfocusout(HTMLSelectElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017789 RID: 96137 RVA: 0x00019BAC File Offset: 0x00018BAC
		public override void remove_onfocusout(HTMLSelectElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601778A RID: 96138 RVA: 0x00019C9C File Offset: 0x00018C9C
		public override void add_onfocusin(HTMLSelectElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601778B RID: 96139 RVA: 0x00019D2C File Offset: 0x00018D2C
		public override void remove_onfocusin(HTMLSelectElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601778C RID: 96140 RVA: 0x00019E1C File Offset: 0x00018E1C
		public override void add_onbeforeactivate(HTMLSelectElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601778D RID: 96141 RVA: 0x00019EAC File Offset: 0x00018EAC
		public override void remove_onbeforeactivate(HTMLSelectElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601778E RID: 96142 RVA: 0x00019F9C File Offset: 0x00018F9C
		public override void add_onbeforedeactivate(HTMLSelectElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601778F RID: 96143 RVA: 0x0001A02C File Offset: 0x0001902C
		public override void remove_onbeforedeactivate(HTMLSelectElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017790 RID: 96144 RVA: 0x0001A11C File Offset: 0x0001911C
		public override void add_ondeactivate(HTMLSelectElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017791 RID: 96145 RVA: 0x0001A1AC File Offset: 0x000191AC
		public override void remove_ondeactivate(HTMLSelectElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017792 RID: 96146 RVA: 0x0001A29C File Offset: 0x0001929C
		public override void add_onactivate(HTMLSelectElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017793 RID: 96147 RVA: 0x0001A32C File Offset: 0x0001932C
		public override void remove_onactivate(HTMLSelectElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onactivateDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017794 RID: 96148 RVA: 0x0001A41C File Offset: 0x0001941C
		public override void add_onmouseleave(HTMLSelectElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017795 RID: 96149 RVA: 0x0001A4AC File Offset: 0x000194AC
		public override void remove_onmouseleave(HTMLSelectElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017796 RID: 96150 RVA: 0x0001A59C File Offset: 0x0001959C
		public override void add_onmouseenter(HTMLSelectElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017797 RID: 96151 RVA: 0x0001A62C File Offset: 0x0001962C
		public override void remove_onmouseenter(HTMLSelectElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017798 RID: 96152 RVA: 0x0001A71C File Offset: 0x0001971C
		public override void add_onpage(HTMLSelectElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017799 RID: 96153 RVA: 0x0001A7AC File Offset: 0x000197AC
		public override void remove_onpage(HTMLSelectElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onpageDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601779A RID: 96154 RVA: 0x0001A89C File Offset: 0x0001989C
		public override void add_onlayoutcomplete(HTMLSelectElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601779B RID: 96155 RVA: 0x0001A92C File Offset: 0x0001992C
		public override void remove_onlayoutcomplete(HTMLSelectElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601779C RID: 96156 RVA: 0x0001AA1C File Offset: 0x00019A1C
		public override void add_onreadystatechange(HTMLSelectElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601779D RID: 96157 RVA: 0x0001AAAC File Offset: 0x00019AAC
		public override void remove_onreadystatechange(HTMLSelectElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601779E RID: 96158 RVA: 0x0001AB9C File Offset: 0x00019B9C
		public override void add_oncellchange(HTMLSelectElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601779F RID: 96159 RVA: 0x0001AC2C File Offset: 0x00019C2C
		public override void remove_oncellchange(HTMLSelectElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177A0 RID: 96160 RVA: 0x0001AD1C File Offset: 0x00019D1C
		public override void add_onrowsinserted(HTMLSelectElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177A1 RID: 96161 RVA: 0x0001ADAC File Offset: 0x00019DAC
		public override void remove_onrowsinserted(HTMLSelectElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177A2 RID: 96162 RVA: 0x0001AE9C File Offset: 0x00019E9C
		public override void add_onrowsdelete(HTMLSelectElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177A3 RID: 96163 RVA: 0x0001AF2C File Offset: 0x00019F2C
		public override void remove_onrowsdelete(HTMLSelectElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177A4 RID: 96164 RVA: 0x0001B01C File Offset: 0x0001A01C
		public override void add_oncontextmenu(HTMLSelectElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177A5 RID: 96165 RVA: 0x0001B0AC File Offset: 0x0001A0AC
		public override void remove_oncontextmenu(HTMLSelectElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177A6 RID: 96166 RVA: 0x0001B19C File Offset: 0x0001A19C
		public override void add_onpaste(HTMLSelectElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177A7 RID: 96167 RVA: 0x0001B22C File Offset: 0x0001A22C
		public override void remove_onpaste(HTMLSelectElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onpasteDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177A8 RID: 96168 RVA: 0x0001B31C File Offset: 0x0001A31C
		public override void add_onbeforepaste(HTMLSelectElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177A9 RID: 96169 RVA: 0x0001B3AC File Offset: 0x0001A3AC
		public override void remove_onbeforepaste(HTMLSelectElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177AA RID: 96170 RVA: 0x0001B49C File Offset: 0x0001A49C
		public override void add_oncopy(HTMLSelectElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177AB RID: 96171 RVA: 0x0001B52C File Offset: 0x0001A52C
		public override void remove_oncopy(HTMLSelectElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_oncopyDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177AC RID: 96172 RVA: 0x0001B61C File Offset: 0x0001A61C
		public override void add_onbeforecopy(HTMLSelectElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177AD RID: 96173 RVA: 0x0001B6AC File Offset: 0x0001A6AC
		public override void remove_onbeforecopy(HTMLSelectElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177AE RID: 96174 RVA: 0x0001B79C File Offset: 0x0001A79C
		public override void add_oncut(HTMLSelectElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177AF RID: 96175 RVA: 0x0001B82C File Offset: 0x0001A82C
		public override void remove_oncut(HTMLSelectElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_oncutDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177B0 RID: 96176 RVA: 0x0001B91C File Offset: 0x0001A91C
		public override void add_onbeforecut(HTMLSelectElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177B1 RID: 96177 RVA: 0x0001B9AC File Offset: 0x0001A9AC
		public override void remove_onbeforecut(HTMLSelectElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177B2 RID: 96178 RVA: 0x0001BA9C File Offset: 0x0001AA9C
		public override void add_ondrop(HTMLSelectElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177B3 RID: 96179 RVA: 0x0001BB2C File Offset: 0x0001AB2C
		public override void remove_ondrop(HTMLSelectElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_ondropDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177B4 RID: 96180 RVA: 0x0001BC1C File Offset: 0x0001AC1C
		public override void add_ondragleave(HTMLSelectElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177B5 RID: 96181 RVA: 0x0001BCAC File Offset: 0x0001ACAC
		public override void remove_ondragleave(HTMLSelectElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177B6 RID: 96182 RVA: 0x0001BD9C File Offset: 0x0001AD9C
		public override void add_ondragover(HTMLSelectElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177B7 RID: 96183 RVA: 0x0001BE2C File Offset: 0x0001AE2C
		public override void remove_ondragover(HTMLSelectElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177B8 RID: 96184 RVA: 0x0001BF1C File Offset: 0x0001AF1C
		public override void add_ondragenter(HTMLSelectElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177B9 RID: 96185 RVA: 0x0001BFAC File Offset: 0x0001AFAC
		public override void remove_ondragenter(HTMLSelectElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177BA RID: 96186 RVA: 0x0001C09C File Offset: 0x0001B09C
		public override void add_ondragend(HTMLSelectElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177BB RID: 96187 RVA: 0x0001C12C File Offset: 0x0001B12C
		public override void remove_ondragend(HTMLSelectElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_ondragendDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177BC RID: 96188 RVA: 0x0001C21C File Offset: 0x0001B21C
		public override void add_ondrag(HTMLSelectElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177BD RID: 96189 RVA: 0x0001C2AC File Offset: 0x0001B2AC
		public override void remove_ondrag(HTMLSelectElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_ondragDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177BE RID: 96190 RVA: 0x0001C39C File Offset: 0x0001B39C
		public override void add_onresize(HTMLSelectElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177BF RID: 96191 RVA: 0x0001C42C File Offset: 0x0001B42C
		public override void remove_onresize(HTMLSelectElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onresizeDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177C0 RID: 96192 RVA: 0x0001C51C File Offset: 0x0001B51C
		public override void add_onblur(HTMLSelectElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177C1 RID: 96193 RVA: 0x0001C5AC File Offset: 0x0001B5AC
		public override void remove_onblur(HTMLSelectElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onblurDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177C2 RID: 96194 RVA: 0x0001C69C File Offset: 0x0001B69C
		public override void add_onfocus(HTMLSelectElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177C3 RID: 96195 RVA: 0x0001C72C File Offset: 0x0001B72C
		public override void remove_onfocus(HTMLSelectElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onfocusDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177C4 RID: 96196 RVA: 0x0001C81C File Offset: 0x0001B81C
		public override void add_onscroll(HTMLSelectElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177C5 RID: 96197 RVA: 0x0001C8AC File Offset: 0x0001B8AC
		public override void remove_onscroll(HTMLSelectElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onscrollDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177C6 RID: 96198 RVA: 0x0001C99C File Offset: 0x0001B99C
		public override void add_onpropertychange(HTMLSelectElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177C7 RID: 96199 RVA: 0x0001CA2C File Offset: 0x0001BA2C
		public override void remove_onpropertychange(HTMLSelectElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177C8 RID: 96200 RVA: 0x0001CB1C File Offset: 0x0001BB1C
		public override void add_onlosecapture(HTMLSelectElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177C9 RID: 96201 RVA: 0x0001CBAC File Offset: 0x0001BBAC
		public override void remove_onlosecapture(HTMLSelectElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177CA RID: 96202 RVA: 0x0001CC9C File Offset: 0x0001BC9C
		public override void add_ondatasetcomplete(HTMLSelectElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177CB RID: 96203 RVA: 0x0001CD2C File Offset: 0x0001BD2C
		public override void remove_ondatasetcomplete(HTMLSelectElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177CC RID: 96204 RVA: 0x0001CE1C File Offset: 0x0001BE1C
		public override void add_ondataavailable(HTMLSelectElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177CD RID: 96205 RVA: 0x0001CEAC File Offset: 0x0001BEAC
		public override void remove_ondataavailable(HTMLSelectElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177CE RID: 96206 RVA: 0x0001CF9C File Offset: 0x0001BF9C
		public override void add_ondatasetchanged(HTMLSelectElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177CF RID: 96207 RVA: 0x0001D02C File Offset: 0x0001C02C
		public override void remove_ondatasetchanged(HTMLSelectElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177D0 RID: 96208 RVA: 0x0001D11C File Offset: 0x0001C11C
		public override void add_onrowenter(HTMLSelectElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177D1 RID: 96209 RVA: 0x0001D1AC File Offset: 0x0001C1AC
		public override void remove_onrowenter(HTMLSelectElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177D2 RID: 96210 RVA: 0x0001D29C File Offset: 0x0001C29C
		public override void add_onrowexit(HTMLSelectElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177D3 RID: 96211 RVA: 0x0001D32C File Offset: 0x0001C32C
		public override void remove_onrowexit(HTMLSelectElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177D4 RID: 96212 RVA: 0x0001D41C File Offset: 0x0001C41C
		public override void add_onerrorupdate(HTMLSelectElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177D5 RID: 96213 RVA: 0x0001D4AC File Offset: 0x0001C4AC
		public override void remove_onerrorupdate(HTMLSelectElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177D6 RID: 96214 RVA: 0x0001D59C File Offset: 0x0001C59C
		public override void add_onafterupdate(HTMLSelectElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177D7 RID: 96215 RVA: 0x0001D62C File Offset: 0x0001C62C
		public override void remove_onafterupdate(HTMLSelectElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177D8 RID: 96216 RVA: 0x0001D71C File Offset: 0x0001C71C
		public override void add_onbeforeupdate(HTMLSelectElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177D9 RID: 96217 RVA: 0x0001D7AC File Offset: 0x0001C7AC
		public override void remove_onbeforeupdate(HTMLSelectElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177DA RID: 96218 RVA: 0x0001D89C File Offset: 0x0001C89C
		public override void add_ondragstart(HTMLSelectElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177DB RID: 96219 RVA: 0x0001D92C File Offset: 0x0001C92C
		public override void remove_ondragstart(HTMLSelectElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177DC RID: 96220 RVA: 0x0001DA1C File Offset: 0x0001CA1C
		public override void add_onfilterchange(HTMLSelectElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177DD RID: 96221 RVA: 0x0001DAAC File Offset: 0x0001CAAC
		public override void remove_onfilterchange(HTMLSelectElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177DE RID: 96222 RVA: 0x0001DB9C File Offset: 0x0001CB9C
		public override void add_onselectstart(HTMLSelectElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177DF RID: 96223 RVA: 0x0001DC2C File Offset: 0x0001CC2C
		public override void remove_onselectstart(HTMLSelectElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177E0 RID: 96224 RVA: 0x0001DD1C File Offset: 0x0001CD1C
		public override void add_onmouseup(HTMLSelectElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177E1 RID: 96225 RVA: 0x0001DDAC File Offset: 0x0001CDAC
		public override void remove_onmouseup(HTMLSelectElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177E2 RID: 96226 RVA: 0x0001DE9C File Offset: 0x0001CE9C
		public override void add_onmousedown(HTMLSelectElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177E3 RID: 96227 RVA: 0x0001DF2C File Offset: 0x0001CF2C
		public override void remove_onmousedown(HTMLSelectElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177E4 RID: 96228 RVA: 0x0001E01C File Offset: 0x0001D01C
		public override void add_onmousemove(HTMLSelectElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177E5 RID: 96229 RVA: 0x0001E0AC File Offset: 0x0001D0AC
		public override void remove_onmousemove(HTMLSelectElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177E6 RID: 96230 RVA: 0x0001E19C File Offset: 0x0001D19C
		public override void add_onmouseover(HTMLSelectElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177E7 RID: 96231 RVA: 0x0001E22C File Offset: 0x0001D22C
		public override void remove_onmouseover(HTMLSelectElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177E8 RID: 96232 RVA: 0x0001E31C File Offset: 0x0001D31C
		public override void add_onmouseout(HTMLSelectElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177E9 RID: 96233 RVA: 0x0001E3AC File Offset: 0x0001D3AC
		public override void remove_onmouseout(HTMLSelectElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177EA RID: 96234 RVA: 0x0001E49C File Offset: 0x0001D49C
		public override void add_onkeyup(HTMLSelectElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177EB RID: 96235 RVA: 0x0001E52C File Offset: 0x0001D52C
		public override void remove_onkeyup(HTMLSelectElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177EC RID: 96236 RVA: 0x0001E61C File Offset: 0x0001D61C
		public override void add_onkeydown(HTMLSelectElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177ED RID: 96237 RVA: 0x0001E6AC File Offset: 0x0001D6AC
		public override void remove_onkeydown(HTMLSelectElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177EE RID: 96238 RVA: 0x0001E79C File Offset: 0x0001D79C
		public override void add_onkeypress(HTMLSelectElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177EF RID: 96239 RVA: 0x0001E82C File Offset: 0x0001D82C
		public override void remove_onkeypress(HTMLSelectElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177F0 RID: 96240 RVA: 0x0001E91C File Offset: 0x0001D91C
		public override void add_ondblclick(HTMLSelectElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177F1 RID: 96241 RVA: 0x0001E9AC File Offset: 0x0001D9AC
		public override void remove_ondblclick(HTMLSelectElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177F2 RID: 96242 RVA: 0x0001EA9C File Offset: 0x0001DA9C
		public override void add_onclick(HTMLSelectElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177F3 RID: 96243 RVA: 0x0001EB2C File Offset: 0x0001DB2C
		public override void remove_onclick(HTMLSelectElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onclickDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177F4 RID: 96244 RVA: 0x0001EC1C File Offset: 0x0001DC1C
		public override void add_onhelp(HTMLSelectElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = new HTMLSelectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents2_SinkHelper, ref num);
				htmlselectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060177F5 RID: 96245 RVA: 0x0001ECAC File Offset: 0x0001DCAC
		public override void remove_onhelp(HTMLSelectElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents2_SinkHelper.m_onhelpDelegate != null && ((htmlselectElementEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060177F6 RID: 96246 RVA: 0x0001ED9C File Offset: 0x0001DD9C
		public HTMLSelectElementEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x060177F7 RID: 96247 RVA: 0x0001EDC4 File Offset: 0x0001DDC4
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
								HTMLSelectElementEvents2_SinkHelper htmlselectElementEvents2_SinkHelper = (HTMLSelectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlselectElementEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x060177F8 RID: 96248 RVA: 0x0001EE78 File Offset: 0x0001DE78
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x040005D1 RID: 1489
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x040005D2 RID: 1490
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x040005D3 RID: 1491
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
