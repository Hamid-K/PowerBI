using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E01 RID: 3585
	internal sealed class HTMLMarqueeElementEvents_EventProvider : HTMLMarqueeElementEvents_Event, IDisposable
	{
		// Token: 0x06018B2D RID: 101165 RVA: 0x000CC2A4 File Offset: 0x000CB2A4
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				184, 242, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06018B2E RID: 101166 RVA: 0x000CC3B8 File Offset: 0x000CB3B8
		public override void add_onstart(HTMLMarqueeElementEvents_onstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B2F RID: 101167 RVA: 0x000CC448 File Offset: 0x000CB448
		public override void remove_onstart(HTMLMarqueeElementEvents_onstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onstartDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B30 RID: 101168 RVA: 0x000CC538 File Offset: 0x000CB538
		public override void add_onfinish(HTMLMarqueeElementEvents_onfinishEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onfinishDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B31 RID: 101169 RVA: 0x000CC5C8 File Offset: 0x000CB5C8
		public override void remove_onfinish(HTMLMarqueeElementEvents_onfinishEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onfinishDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onfinishDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B32 RID: 101170 RVA: 0x000CC6B8 File Offset: 0x000CB6B8
		public override void add_onbounce(HTMLMarqueeElementEvents_onbounceEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onbounceDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B33 RID: 101171 RVA: 0x000CC748 File Offset: 0x000CB748
		public override void remove_onbounce(HTMLMarqueeElementEvents_onbounceEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onbounceDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onbounceDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B34 RID: 101172 RVA: 0x000CC838 File Offset: 0x000CB838
		public override void add_onselect(HTMLMarqueeElementEvents_onselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B35 RID: 101173 RVA: 0x000CC8C8 File Offset: 0x000CB8C8
		public override void remove_onselect(HTMLMarqueeElementEvents_onselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onselectDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B36 RID: 101174 RVA: 0x000CC9B8 File Offset: 0x000CB9B8
		public override void add_onchange(HTMLMarqueeElementEvents_onchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B37 RID: 101175 RVA: 0x000CCA48 File Offset: 0x000CBA48
		public override void remove_onchange(HTMLMarqueeElementEvents_onchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onchangeDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B38 RID: 101176 RVA: 0x000CCB38 File Offset: 0x000CBB38
		public override void add_onfocusout(HTMLMarqueeElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B39 RID: 101177 RVA: 0x000CCBC8 File Offset: 0x000CBBC8
		public override void remove_onfocusout(HTMLMarqueeElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B3A RID: 101178 RVA: 0x000CCCB8 File Offset: 0x000CBCB8
		public override void add_onfocusin(HTMLMarqueeElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B3B RID: 101179 RVA: 0x000CCD48 File Offset: 0x000CBD48
		public override void remove_onfocusin(HTMLMarqueeElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onfocusinDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B3C RID: 101180 RVA: 0x000CCE38 File Offset: 0x000CBE38
		public override void add_ondeactivate(HTMLMarqueeElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B3D RID: 101181 RVA: 0x000CCEC8 File Offset: 0x000CBEC8
		public override void remove_ondeactivate(HTMLMarqueeElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B3E RID: 101182 RVA: 0x000CCFB8 File Offset: 0x000CBFB8
		public override void add_onactivate(HTMLMarqueeElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B3F RID: 101183 RVA: 0x000CD048 File Offset: 0x000CC048
		public override void remove_onactivate(HTMLMarqueeElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onactivateDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B40 RID: 101184 RVA: 0x000CD138 File Offset: 0x000CC138
		public override void add_onmousewheel(HTMLMarqueeElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B41 RID: 101185 RVA: 0x000CD1C8 File Offset: 0x000CC1C8
		public override void remove_onmousewheel(HTMLMarqueeElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B42 RID: 101186 RVA: 0x000CD2B8 File Offset: 0x000CC2B8
		public override void add_onmouseleave(HTMLMarqueeElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B43 RID: 101187 RVA: 0x000CD348 File Offset: 0x000CC348
		public override void remove_onmouseleave(HTMLMarqueeElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B44 RID: 101188 RVA: 0x000CD438 File Offset: 0x000CC438
		public override void add_onmouseenter(HTMLMarqueeElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B45 RID: 101189 RVA: 0x000CD4C8 File Offset: 0x000CC4C8
		public override void remove_onmouseenter(HTMLMarqueeElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B46 RID: 101190 RVA: 0x000CD5B8 File Offset: 0x000CC5B8
		public override void add_onresizeend(HTMLMarqueeElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B47 RID: 101191 RVA: 0x000CD648 File Offset: 0x000CC648
		public override void remove_onresizeend(HTMLMarqueeElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onresizeendDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B48 RID: 101192 RVA: 0x000CD738 File Offset: 0x000CC738
		public override void add_onresizestart(HTMLMarqueeElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B49 RID: 101193 RVA: 0x000CD7C8 File Offset: 0x000CC7C8
		public override void remove_onresizestart(HTMLMarqueeElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onresizestartDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B4A RID: 101194 RVA: 0x000CD8B8 File Offset: 0x000CC8B8
		public override void add_onmoveend(HTMLMarqueeElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B4B RID: 101195 RVA: 0x000CD948 File Offset: 0x000CC948
		public override void remove_onmoveend(HTMLMarqueeElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onmoveendDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B4C RID: 101196 RVA: 0x000CDA38 File Offset: 0x000CCA38
		public override void add_onmovestart(HTMLMarqueeElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B4D RID: 101197 RVA: 0x000CDAC8 File Offset: 0x000CCAC8
		public override void remove_onmovestart(HTMLMarqueeElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onmovestartDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B4E RID: 101198 RVA: 0x000CDBB8 File Offset: 0x000CCBB8
		public override void add_oncontrolselect(HTMLMarqueeElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B4F RID: 101199 RVA: 0x000CDC48 File Offset: 0x000CCC48
		public override void remove_oncontrolselect(HTMLMarqueeElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B50 RID: 101200 RVA: 0x000CDD38 File Offset: 0x000CCD38
		public override void add_onmove(HTMLMarqueeElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B51 RID: 101201 RVA: 0x000CDDC8 File Offset: 0x000CCDC8
		public override void remove_onmove(HTMLMarqueeElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onmoveDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B52 RID: 101202 RVA: 0x000CDEB8 File Offset: 0x000CCEB8
		public override void add_onbeforeactivate(HTMLMarqueeElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B53 RID: 101203 RVA: 0x000CDF48 File Offset: 0x000CCF48
		public override void remove_onbeforeactivate(HTMLMarqueeElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B54 RID: 101204 RVA: 0x000CE038 File Offset: 0x000CD038
		public override void add_onbeforedeactivate(HTMLMarqueeElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B55 RID: 101205 RVA: 0x000CE0C8 File Offset: 0x000CD0C8
		public override void remove_onbeforedeactivate(HTMLMarqueeElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B56 RID: 101206 RVA: 0x000CE1B8 File Offset: 0x000CD1B8
		public override void add_onpage(HTMLMarqueeElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B57 RID: 101207 RVA: 0x000CE248 File Offset: 0x000CD248
		public override void remove_onpage(HTMLMarqueeElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onpageDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B58 RID: 101208 RVA: 0x000CE338 File Offset: 0x000CD338
		public override void add_onlayoutcomplete(HTMLMarqueeElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B59 RID: 101209 RVA: 0x000CE3C8 File Offset: 0x000CD3C8
		public override void remove_onlayoutcomplete(HTMLMarqueeElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B5A RID: 101210 RVA: 0x000CE4B8 File Offset: 0x000CD4B8
		public override void add_onbeforeeditfocus(HTMLMarqueeElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B5B RID: 101211 RVA: 0x000CE548 File Offset: 0x000CD548
		public override void remove_onbeforeeditfocus(HTMLMarqueeElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B5C RID: 101212 RVA: 0x000CE638 File Offset: 0x000CD638
		public override void add_onreadystatechange(HTMLMarqueeElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B5D RID: 101213 RVA: 0x000CE6C8 File Offset: 0x000CD6C8
		public override void remove_onreadystatechange(HTMLMarqueeElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B5E RID: 101214 RVA: 0x000CE7B8 File Offset: 0x000CD7B8
		public override void add_oncellchange(HTMLMarqueeElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B5F RID: 101215 RVA: 0x000CE848 File Offset: 0x000CD848
		public override void remove_oncellchange(HTMLMarqueeElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B60 RID: 101216 RVA: 0x000CE938 File Offset: 0x000CD938
		public override void add_onrowsinserted(HTMLMarqueeElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B61 RID: 101217 RVA: 0x000CE9C8 File Offset: 0x000CD9C8
		public override void remove_onrowsinserted(HTMLMarqueeElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B62 RID: 101218 RVA: 0x000CEAB8 File Offset: 0x000CDAB8
		public override void add_onrowsdelete(HTMLMarqueeElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B63 RID: 101219 RVA: 0x000CEB48 File Offset: 0x000CDB48
		public override void remove_onrowsdelete(HTMLMarqueeElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B64 RID: 101220 RVA: 0x000CEC38 File Offset: 0x000CDC38
		public override void add_oncontextmenu(HTMLMarqueeElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B65 RID: 101221 RVA: 0x000CECC8 File Offset: 0x000CDCC8
		public override void remove_oncontextmenu(HTMLMarqueeElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B66 RID: 101222 RVA: 0x000CEDB8 File Offset: 0x000CDDB8
		public override void add_onpaste(HTMLMarqueeElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B67 RID: 101223 RVA: 0x000CEE48 File Offset: 0x000CDE48
		public override void remove_onpaste(HTMLMarqueeElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onpasteDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B68 RID: 101224 RVA: 0x000CEF38 File Offset: 0x000CDF38
		public override void add_onbeforepaste(HTMLMarqueeElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B69 RID: 101225 RVA: 0x000CEFC8 File Offset: 0x000CDFC8
		public override void remove_onbeforepaste(HTMLMarqueeElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B6A RID: 101226 RVA: 0x000CF0B8 File Offset: 0x000CE0B8
		public override void add_oncopy(HTMLMarqueeElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B6B RID: 101227 RVA: 0x000CF148 File Offset: 0x000CE148
		public override void remove_oncopy(HTMLMarqueeElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_oncopyDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B6C RID: 101228 RVA: 0x000CF238 File Offset: 0x000CE238
		public override void add_onbeforecopy(HTMLMarqueeElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B6D RID: 101229 RVA: 0x000CF2C8 File Offset: 0x000CE2C8
		public override void remove_onbeforecopy(HTMLMarqueeElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B6E RID: 101230 RVA: 0x000CF3B8 File Offset: 0x000CE3B8
		public override void add_oncut(HTMLMarqueeElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B6F RID: 101231 RVA: 0x000CF448 File Offset: 0x000CE448
		public override void remove_oncut(HTMLMarqueeElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_oncutDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B70 RID: 101232 RVA: 0x000CF538 File Offset: 0x000CE538
		public override void add_onbeforecut(HTMLMarqueeElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B71 RID: 101233 RVA: 0x000CF5C8 File Offset: 0x000CE5C8
		public override void remove_onbeforecut(HTMLMarqueeElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B72 RID: 101234 RVA: 0x000CF6B8 File Offset: 0x000CE6B8
		public override void add_ondrop(HTMLMarqueeElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B73 RID: 101235 RVA: 0x000CF748 File Offset: 0x000CE748
		public override void remove_ondrop(HTMLMarqueeElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_ondropDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B74 RID: 101236 RVA: 0x000CF838 File Offset: 0x000CE838
		public override void add_ondragleave(HTMLMarqueeElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B75 RID: 101237 RVA: 0x000CF8C8 File Offset: 0x000CE8C8
		public override void remove_ondragleave(HTMLMarqueeElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B76 RID: 101238 RVA: 0x000CF9B8 File Offset: 0x000CE9B8
		public override void add_ondragover(HTMLMarqueeElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B77 RID: 101239 RVA: 0x000CFA48 File Offset: 0x000CEA48
		public override void remove_ondragover(HTMLMarqueeElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_ondragoverDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B78 RID: 101240 RVA: 0x000CFB38 File Offset: 0x000CEB38
		public override void add_ondragenter(HTMLMarqueeElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B79 RID: 101241 RVA: 0x000CFBC8 File Offset: 0x000CEBC8
		public override void remove_ondragenter(HTMLMarqueeElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_ondragenterDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B7A RID: 101242 RVA: 0x000CFCB8 File Offset: 0x000CECB8
		public override void add_ondragend(HTMLMarqueeElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B7B RID: 101243 RVA: 0x000CFD48 File Offset: 0x000CED48
		public override void remove_ondragend(HTMLMarqueeElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_ondragendDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B7C RID: 101244 RVA: 0x000CFE38 File Offset: 0x000CEE38
		public override void add_ondrag(HTMLMarqueeElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B7D RID: 101245 RVA: 0x000CFEC8 File Offset: 0x000CEEC8
		public override void remove_ondrag(HTMLMarqueeElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_ondragDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B7E RID: 101246 RVA: 0x000CFFB8 File Offset: 0x000CEFB8
		public override void add_onresize(HTMLMarqueeElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B7F RID: 101247 RVA: 0x000D0048 File Offset: 0x000CF048
		public override void remove_onresize(HTMLMarqueeElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onresizeDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B80 RID: 101248 RVA: 0x000D0138 File Offset: 0x000CF138
		public override void add_onblur(HTMLMarqueeElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B81 RID: 101249 RVA: 0x000D01C8 File Offset: 0x000CF1C8
		public override void remove_onblur(HTMLMarqueeElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onblurDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B82 RID: 101250 RVA: 0x000D02B8 File Offset: 0x000CF2B8
		public override void add_onfocus(HTMLMarqueeElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B83 RID: 101251 RVA: 0x000D0348 File Offset: 0x000CF348
		public override void remove_onfocus(HTMLMarqueeElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onfocusDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B84 RID: 101252 RVA: 0x000D0438 File Offset: 0x000CF438
		public override void add_onscroll(HTMLMarqueeElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B85 RID: 101253 RVA: 0x000D04C8 File Offset: 0x000CF4C8
		public override void remove_onscroll(HTMLMarqueeElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onscrollDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B86 RID: 101254 RVA: 0x000D05B8 File Offset: 0x000CF5B8
		public override void add_onpropertychange(HTMLMarqueeElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B87 RID: 101255 RVA: 0x000D0648 File Offset: 0x000CF648
		public override void remove_onpropertychange(HTMLMarqueeElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B88 RID: 101256 RVA: 0x000D0738 File Offset: 0x000CF738
		public override void add_onlosecapture(HTMLMarqueeElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B89 RID: 101257 RVA: 0x000D07C8 File Offset: 0x000CF7C8
		public override void remove_onlosecapture(HTMLMarqueeElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B8A RID: 101258 RVA: 0x000D08B8 File Offset: 0x000CF8B8
		public override void add_ondatasetcomplete(HTMLMarqueeElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B8B RID: 101259 RVA: 0x000D0948 File Offset: 0x000CF948
		public override void remove_ondatasetcomplete(HTMLMarqueeElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B8C RID: 101260 RVA: 0x000D0A38 File Offset: 0x000CFA38
		public override void add_ondataavailable(HTMLMarqueeElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B8D RID: 101261 RVA: 0x000D0AC8 File Offset: 0x000CFAC8
		public override void remove_ondataavailable(HTMLMarqueeElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B8E RID: 101262 RVA: 0x000D0BB8 File Offset: 0x000CFBB8
		public override void add_ondatasetchanged(HTMLMarqueeElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B8F RID: 101263 RVA: 0x000D0C48 File Offset: 0x000CFC48
		public override void remove_ondatasetchanged(HTMLMarqueeElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B90 RID: 101264 RVA: 0x000D0D38 File Offset: 0x000CFD38
		public override void add_onrowenter(HTMLMarqueeElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B91 RID: 101265 RVA: 0x000D0DC8 File Offset: 0x000CFDC8
		public override void remove_onrowenter(HTMLMarqueeElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B92 RID: 101266 RVA: 0x000D0EB8 File Offset: 0x000CFEB8
		public override void add_onrowexit(HTMLMarqueeElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B93 RID: 101267 RVA: 0x000D0F48 File Offset: 0x000CFF48
		public override void remove_onrowexit(HTMLMarqueeElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B94 RID: 101268 RVA: 0x000D1038 File Offset: 0x000D0038
		public override void add_onerrorupdate(HTMLMarqueeElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B95 RID: 101269 RVA: 0x000D10C8 File Offset: 0x000D00C8
		public override void remove_onerrorupdate(HTMLMarqueeElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B96 RID: 101270 RVA: 0x000D11B8 File Offset: 0x000D01B8
		public override void add_onafterupdate(HTMLMarqueeElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B97 RID: 101271 RVA: 0x000D1248 File Offset: 0x000D0248
		public override void remove_onafterupdate(HTMLMarqueeElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B98 RID: 101272 RVA: 0x000D1338 File Offset: 0x000D0338
		public override void add_onbeforeupdate(HTMLMarqueeElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B99 RID: 101273 RVA: 0x000D13C8 File Offset: 0x000D03C8
		public override void remove_onbeforeupdate(HTMLMarqueeElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B9A RID: 101274 RVA: 0x000D14B8 File Offset: 0x000D04B8
		public override void add_ondragstart(HTMLMarqueeElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B9B RID: 101275 RVA: 0x000D1548 File Offset: 0x000D0548
		public override void remove_ondragstart(HTMLMarqueeElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_ondragstartDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B9C RID: 101276 RVA: 0x000D1638 File Offset: 0x000D0638
		public override void add_onfilterchange(HTMLMarqueeElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B9D RID: 101277 RVA: 0x000D16C8 File Offset: 0x000D06C8
		public override void remove_onfilterchange(HTMLMarqueeElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018B9E RID: 101278 RVA: 0x000D17B8 File Offset: 0x000D07B8
		public override void add_onselectstart(HTMLMarqueeElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018B9F RID: 101279 RVA: 0x000D1848 File Offset: 0x000D0848
		public override void remove_onselectstart(HTMLMarqueeElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onselectstartDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018BA0 RID: 101280 RVA: 0x000D1938 File Offset: 0x000D0938
		public override void add_onmouseup(HTMLMarqueeElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018BA1 RID: 101281 RVA: 0x000D19C8 File Offset: 0x000D09C8
		public override void remove_onmouseup(HTMLMarqueeElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onmouseupDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018BA2 RID: 101282 RVA: 0x000D1AB8 File Offset: 0x000D0AB8
		public override void add_onmousedown(HTMLMarqueeElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018BA3 RID: 101283 RVA: 0x000D1B48 File Offset: 0x000D0B48
		public override void remove_onmousedown(HTMLMarqueeElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onmousedownDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018BA4 RID: 101284 RVA: 0x000D1C38 File Offset: 0x000D0C38
		public override void add_onmousemove(HTMLMarqueeElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018BA5 RID: 101285 RVA: 0x000D1CC8 File Offset: 0x000D0CC8
		public override void remove_onmousemove(HTMLMarqueeElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018BA6 RID: 101286 RVA: 0x000D1DB8 File Offset: 0x000D0DB8
		public override void add_onmouseover(HTMLMarqueeElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018BA7 RID: 101287 RVA: 0x000D1E48 File Offset: 0x000D0E48
		public override void remove_onmouseover(HTMLMarqueeElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018BA8 RID: 101288 RVA: 0x000D1F38 File Offset: 0x000D0F38
		public override void add_onmouseout(HTMLMarqueeElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018BA9 RID: 101289 RVA: 0x000D1FC8 File Offset: 0x000D0FC8
		public override void remove_onmouseout(HTMLMarqueeElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018BAA RID: 101290 RVA: 0x000D20B8 File Offset: 0x000D10B8
		public override void add_onkeyup(HTMLMarqueeElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018BAB RID: 101291 RVA: 0x000D2148 File Offset: 0x000D1148
		public override void remove_onkeyup(HTMLMarqueeElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onkeyupDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018BAC RID: 101292 RVA: 0x000D2238 File Offset: 0x000D1238
		public override void add_onkeydown(HTMLMarqueeElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018BAD RID: 101293 RVA: 0x000D22C8 File Offset: 0x000D12C8
		public override void remove_onkeydown(HTMLMarqueeElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onkeydownDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018BAE RID: 101294 RVA: 0x000D23B8 File Offset: 0x000D13B8
		public override void add_onkeypress(HTMLMarqueeElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018BAF RID: 101295 RVA: 0x000D2448 File Offset: 0x000D1448
		public override void remove_onkeypress(HTMLMarqueeElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onkeypressDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018BB0 RID: 101296 RVA: 0x000D2538 File Offset: 0x000D1538
		public override void add_ondblclick(HTMLMarqueeElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018BB1 RID: 101297 RVA: 0x000D25C8 File Offset: 0x000D15C8
		public override void remove_ondblclick(HTMLMarqueeElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_ondblclickDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018BB2 RID: 101298 RVA: 0x000D26B8 File Offset: 0x000D16B8
		public override void add_onclick(HTMLMarqueeElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018BB3 RID: 101299 RVA: 0x000D2748 File Offset: 0x000D1748
		public override void remove_onclick(HTMLMarqueeElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onclickDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018BB4 RID: 101300 RVA: 0x000D2838 File Offset: 0x000D1838
		public override void add_onhelp(HTMLMarqueeElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = new HTMLMarqueeElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents_SinkHelper, ref num);
				htmlmarqueeElementEvents_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018BB5 RID: 101301 RVA: 0x000D28C8 File Offset: 0x000D18C8
		public override void remove_onhelp(HTMLMarqueeElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents_SinkHelper.m_onhelpDelegate != null && ((htmlmarqueeElementEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018BB6 RID: 101302 RVA: 0x000D29B8 File Offset: 0x000D19B8
		public HTMLMarqueeElementEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06018BB7 RID: 101303 RVA: 0x000D29E0 File Offset: 0x000D19E0
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
								HTMLMarqueeElementEvents_SinkHelper htmlmarqueeElementEvents_SinkHelper = (HTMLMarqueeElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x06018BB8 RID: 101304 RVA: 0x000D2A94 File Offset: 0x000D1A94
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000CAA RID: 3242
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000CAB RID: 3243
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000CAC RID: 3244
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
