using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DDD RID: 3549
	internal sealed class HTMLFormElementEvents2_EventProvider : HTMLFormElementEvents2_Event, IDisposable
	{
		// Token: 0x06017EC9 RID: 97993 RVA: 0x0005B6A4 File Offset: 0x0005A6A4
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				20, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06017ECA RID: 97994 RVA: 0x0005B7B8 File Offset: 0x0005A7B8
		public override void add_onreset(HTMLFormElementEvents2_onresetEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onresetDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017ECB RID: 97995 RVA: 0x0005B848 File Offset: 0x0005A848
		public override void remove_onreset(HTMLFormElementEvents2_onresetEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onresetDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onresetDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017ECC RID: 97996 RVA: 0x0005B938 File Offset: 0x0005A938
		public override void add_onsubmit(HTMLFormElementEvents2_onsubmitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onsubmitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017ECD RID: 97997 RVA: 0x0005B9C8 File Offset: 0x0005A9C8
		public override void remove_onsubmit(HTMLFormElementEvents2_onsubmitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onsubmitDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onsubmitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017ECE RID: 97998 RVA: 0x0005BAB8 File Offset: 0x0005AAB8
		public override void add_onmousewheel(HTMLFormElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017ECF RID: 97999 RVA: 0x0005BB48 File Offset: 0x0005AB48
		public override void remove_onmousewheel(HTMLFormElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017ED0 RID: 98000 RVA: 0x0005BC38 File Offset: 0x0005AC38
		public override void add_onresizeend(HTMLFormElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017ED1 RID: 98001 RVA: 0x0005BCC8 File Offset: 0x0005ACC8
		public override void remove_onresizeend(HTMLFormElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017ED2 RID: 98002 RVA: 0x0005BDB8 File Offset: 0x0005ADB8
		public override void add_onresizestart(HTMLFormElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017ED3 RID: 98003 RVA: 0x0005BE48 File Offset: 0x0005AE48
		public override void remove_onresizestart(HTMLFormElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017ED4 RID: 98004 RVA: 0x0005BF38 File Offset: 0x0005AF38
		public override void add_onmoveend(HTMLFormElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017ED5 RID: 98005 RVA: 0x0005BFC8 File Offset: 0x0005AFC8
		public override void remove_onmoveend(HTMLFormElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017ED6 RID: 98006 RVA: 0x0005C0B8 File Offset: 0x0005B0B8
		public override void add_onmovestart(HTMLFormElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017ED7 RID: 98007 RVA: 0x0005C148 File Offset: 0x0005B148
		public override void remove_onmovestart(HTMLFormElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017ED8 RID: 98008 RVA: 0x0005C238 File Offset: 0x0005B238
		public override void add_oncontrolselect(HTMLFormElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017ED9 RID: 98009 RVA: 0x0005C2C8 File Offset: 0x0005B2C8
		public override void remove_oncontrolselect(HTMLFormElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmlformElementEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EDA RID: 98010 RVA: 0x0005C3B8 File Offset: 0x0005B3B8
		public override void add_onmove(HTMLFormElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EDB RID: 98011 RVA: 0x0005C448 File Offset: 0x0005B448
		public override void remove_onmove(HTMLFormElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onmoveDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EDC RID: 98012 RVA: 0x0005C538 File Offset: 0x0005B538
		public override void add_onfocusout(HTMLFormElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EDD RID: 98013 RVA: 0x0005C5C8 File Offset: 0x0005B5C8
		public override void remove_onfocusout(HTMLFormElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EDE RID: 98014 RVA: 0x0005C6B8 File Offset: 0x0005B6B8
		public override void add_onfocusin(HTMLFormElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EDF RID: 98015 RVA: 0x0005C748 File Offset: 0x0005B748
		public override void remove_onfocusin(HTMLFormElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EE0 RID: 98016 RVA: 0x0005C838 File Offset: 0x0005B838
		public override void add_onbeforeactivate(HTMLFormElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EE1 RID: 98017 RVA: 0x0005C8C8 File Offset: 0x0005B8C8
		public override void remove_onbeforeactivate(HTMLFormElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EE2 RID: 98018 RVA: 0x0005C9B8 File Offset: 0x0005B9B8
		public override void add_onbeforedeactivate(HTMLFormElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EE3 RID: 98019 RVA: 0x0005CA48 File Offset: 0x0005BA48
		public override void remove_onbeforedeactivate(HTMLFormElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EE4 RID: 98020 RVA: 0x0005CB38 File Offset: 0x0005BB38
		public override void add_ondeactivate(HTMLFormElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EE5 RID: 98021 RVA: 0x0005CBC8 File Offset: 0x0005BBC8
		public override void remove_ondeactivate(HTMLFormElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmlformElementEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EE6 RID: 98022 RVA: 0x0005CCB8 File Offset: 0x0005BCB8
		public override void add_onactivate(HTMLFormElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EE7 RID: 98023 RVA: 0x0005CD48 File Offset: 0x0005BD48
		public override void remove_onactivate(HTMLFormElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onactivateDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EE8 RID: 98024 RVA: 0x0005CE38 File Offset: 0x0005BE38
		public override void add_onmouseleave(HTMLFormElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EE9 RID: 98025 RVA: 0x0005CEC8 File Offset: 0x0005BEC8
		public override void remove_onmouseleave(HTMLFormElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EEA RID: 98026 RVA: 0x0005CFB8 File Offset: 0x0005BFB8
		public override void add_onmouseenter(HTMLFormElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EEB RID: 98027 RVA: 0x0005D048 File Offset: 0x0005C048
		public override void remove_onmouseenter(HTMLFormElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EEC RID: 98028 RVA: 0x0005D138 File Offset: 0x0005C138
		public override void add_onpage(HTMLFormElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EED RID: 98029 RVA: 0x0005D1C8 File Offset: 0x0005C1C8
		public override void remove_onpage(HTMLFormElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onpageDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EEE RID: 98030 RVA: 0x0005D2B8 File Offset: 0x0005C2B8
		public override void add_onlayoutcomplete(HTMLFormElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EEF RID: 98031 RVA: 0x0005D348 File Offset: 0x0005C348
		public override void remove_onlayoutcomplete(HTMLFormElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EF0 RID: 98032 RVA: 0x0005D438 File Offset: 0x0005C438
		public override void add_onreadystatechange(HTMLFormElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EF1 RID: 98033 RVA: 0x0005D4C8 File Offset: 0x0005C4C8
		public override void remove_onreadystatechange(HTMLFormElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EF2 RID: 98034 RVA: 0x0005D5B8 File Offset: 0x0005C5B8
		public override void add_oncellchange(HTMLFormElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EF3 RID: 98035 RVA: 0x0005D648 File Offset: 0x0005C648
		public override void remove_oncellchange(HTMLFormElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmlformElementEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EF4 RID: 98036 RVA: 0x0005D738 File Offset: 0x0005C738
		public override void add_onrowsinserted(HTMLFormElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EF5 RID: 98037 RVA: 0x0005D7C8 File Offset: 0x0005C7C8
		public override void remove_onrowsinserted(HTMLFormElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EF6 RID: 98038 RVA: 0x0005D8B8 File Offset: 0x0005C8B8
		public override void add_onrowsdelete(HTMLFormElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EF7 RID: 98039 RVA: 0x0005D948 File Offset: 0x0005C948
		public override void remove_onrowsdelete(HTMLFormElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EF8 RID: 98040 RVA: 0x0005DA38 File Offset: 0x0005CA38
		public override void add_oncontextmenu(HTMLFormElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EF9 RID: 98041 RVA: 0x0005DAC8 File Offset: 0x0005CAC8
		public override void remove_oncontextmenu(HTMLFormElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmlformElementEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EFA RID: 98042 RVA: 0x0005DBB8 File Offset: 0x0005CBB8
		public override void add_onpaste(HTMLFormElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EFB RID: 98043 RVA: 0x0005DC48 File Offset: 0x0005CC48
		public override void remove_onpaste(HTMLFormElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onpasteDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EFC RID: 98044 RVA: 0x0005DD38 File Offset: 0x0005CD38
		public override void add_onbeforepaste(HTMLFormElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EFD RID: 98045 RVA: 0x0005DDC8 File Offset: 0x0005CDC8
		public override void remove_onbeforepaste(HTMLFormElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017EFE RID: 98046 RVA: 0x0005DEB8 File Offset: 0x0005CEB8
		public override void add_oncopy(HTMLFormElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017EFF RID: 98047 RVA: 0x0005DF48 File Offset: 0x0005CF48
		public override void remove_oncopy(HTMLFormElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_oncopyDelegate != null && ((htmlformElementEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F00 RID: 98048 RVA: 0x0005E038 File Offset: 0x0005D038
		public override void add_onbeforecopy(HTMLFormElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F01 RID: 98049 RVA: 0x0005E0C8 File Offset: 0x0005D0C8
		public override void remove_onbeforecopy(HTMLFormElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F02 RID: 98050 RVA: 0x0005E1B8 File Offset: 0x0005D1B8
		public override void add_oncut(HTMLFormElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F03 RID: 98051 RVA: 0x0005E248 File Offset: 0x0005D248
		public override void remove_oncut(HTMLFormElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_oncutDelegate != null && ((htmlformElementEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F04 RID: 98052 RVA: 0x0005E338 File Offset: 0x0005D338
		public override void add_onbeforecut(HTMLFormElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F05 RID: 98053 RVA: 0x0005E3C8 File Offset: 0x0005D3C8
		public override void remove_onbeforecut(HTMLFormElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F06 RID: 98054 RVA: 0x0005E4B8 File Offset: 0x0005D4B8
		public override void add_ondrop(HTMLFormElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F07 RID: 98055 RVA: 0x0005E548 File Offset: 0x0005D548
		public override void remove_ondrop(HTMLFormElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_ondropDelegate != null && ((htmlformElementEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F08 RID: 98056 RVA: 0x0005E638 File Offset: 0x0005D638
		public override void add_ondragleave(HTMLFormElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F09 RID: 98057 RVA: 0x0005E6C8 File Offset: 0x0005D6C8
		public override void remove_ondragleave(HTMLFormElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmlformElementEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F0A RID: 98058 RVA: 0x0005E7B8 File Offset: 0x0005D7B8
		public override void add_ondragover(HTMLFormElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F0B RID: 98059 RVA: 0x0005E848 File Offset: 0x0005D848
		public override void remove_ondragover(HTMLFormElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmlformElementEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F0C RID: 98060 RVA: 0x0005E938 File Offset: 0x0005D938
		public override void add_ondragenter(HTMLFormElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F0D RID: 98061 RVA: 0x0005E9C8 File Offset: 0x0005D9C8
		public override void remove_ondragenter(HTMLFormElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmlformElementEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F0E RID: 98062 RVA: 0x0005EAB8 File Offset: 0x0005DAB8
		public override void add_ondragend(HTMLFormElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F0F RID: 98063 RVA: 0x0005EB48 File Offset: 0x0005DB48
		public override void remove_ondragend(HTMLFormElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_ondragendDelegate != null && ((htmlformElementEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F10 RID: 98064 RVA: 0x0005EC38 File Offset: 0x0005DC38
		public override void add_ondrag(HTMLFormElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F11 RID: 98065 RVA: 0x0005ECC8 File Offset: 0x0005DCC8
		public override void remove_ondrag(HTMLFormElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_ondragDelegate != null && ((htmlformElementEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F12 RID: 98066 RVA: 0x0005EDB8 File Offset: 0x0005DDB8
		public override void add_onresize(HTMLFormElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F13 RID: 98067 RVA: 0x0005EE48 File Offset: 0x0005DE48
		public override void remove_onresize(HTMLFormElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onresizeDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F14 RID: 98068 RVA: 0x0005EF38 File Offset: 0x0005DF38
		public override void add_onblur(HTMLFormElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F15 RID: 98069 RVA: 0x0005EFC8 File Offset: 0x0005DFC8
		public override void remove_onblur(HTMLFormElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onblurDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F16 RID: 98070 RVA: 0x0005F0B8 File Offset: 0x0005E0B8
		public override void add_onfocus(HTMLFormElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F17 RID: 98071 RVA: 0x0005F148 File Offset: 0x0005E148
		public override void remove_onfocus(HTMLFormElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onfocusDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F18 RID: 98072 RVA: 0x0005F238 File Offset: 0x0005E238
		public override void add_onscroll(HTMLFormElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F19 RID: 98073 RVA: 0x0005F2C8 File Offset: 0x0005E2C8
		public override void remove_onscroll(HTMLFormElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onscrollDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F1A RID: 98074 RVA: 0x0005F3B8 File Offset: 0x0005E3B8
		public override void add_onpropertychange(HTMLFormElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F1B RID: 98075 RVA: 0x0005F448 File Offset: 0x0005E448
		public override void remove_onpropertychange(HTMLFormElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F1C RID: 98076 RVA: 0x0005F538 File Offset: 0x0005E538
		public override void add_onlosecapture(HTMLFormElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F1D RID: 98077 RVA: 0x0005F5C8 File Offset: 0x0005E5C8
		public override void remove_onlosecapture(HTMLFormElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F1E RID: 98078 RVA: 0x0005F6B8 File Offset: 0x0005E6B8
		public override void add_ondatasetcomplete(HTMLFormElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F1F RID: 98079 RVA: 0x0005F748 File Offset: 0x0005E748
		public override void remove_ondatasetcomplete(HTMLFormElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlformElementEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F20 RID: 98080 RVA: 0x0005F838 File Offset: 0x0005E838
		public override void add_ondataavailable(HTMLFormElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F21 RID: 98081 RVA: 0x0005F8C8 File Offset: 0x0005E8C8
		public override void remove_ondataavailable(HTMLFormElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmlformElementEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F22 RID: 98082 RVA: 0x0005F9B8 File Offset: 0x0005E9B8
		public override void add_ondatasetchanged(HTMLFormElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F23 RID: 98083 RVA: 0x0005FA48 File Offset: 0x0005EA48
		public override void remove_ondatasetchanged(HTMLFormElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlformElementEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F24 RID: 98084 RVA: 0x0005FB38 File Offset: 0x0005EB38
		public override void add_onrowenter(HTMLFormElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F25 RID: 98085 RVA: 0x0005FBC8 File Offset: 0x0005EBC8
		public override void remove_onrowenter(HTMLFormElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F26 RID: 98086 RVA: 0x0005FCB8 File Offset: 0x0005ECB8
		public override void add_onrowexit(HTMLFormElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F27 RID: 98087 RVA: 0x0005FD48 File Offset: 0x0005ED48
		public override void remove_onrowexit(HTMLFormElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F28 RID: 98088 RVA: 0x0005FE38 File Offset: 0x0005EE38
		public override void add_onerrorupdate(HTMLFormElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F29 RID: 98089 RVA: 0x0005FEC8 File Offset: 0x0005EEC8
		public override void remove_onerrorupdate(HTMLFormElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F2A RID: 98090 RVA: 0x0005FFB8 File Offset: 0x0005EFB8
		public override void add_onafterupdate(HTMLFormElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F2B RID: 98091 RVA: 0x00060048 File Offset: 0x0005F048
		public override void remove_onafterupdate(HTMLFormElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F2C RID: 98092 RVA: 0x00060138 File Offset: 0x0005F138
		public override void add_onbeforeupdate(HTMLFormElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F2D RID: 98093 RVA: 0x000601C8 File Offset: 0x0005F1C8
		public override void remove_onbeforeupdate(HTMLFormElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F2E RID: 98094 RVA: 0x000602B8 File Offset: 0x0005F2B8
		public override void add_ondragstart(HTMLFormElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F2F RID: 98095 RVA: 0x00060348 File Offset: 0x0005F348
		public override void remove_ondragstart(HTMLFormElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmlformElementEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F30 RID: 98096 RVA: 0x00060438 File Offset: 0x0005F438
		public override void add_onfilterchange(HTMLFormElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F31 RID: 98097 RVA: 0x000604C8 File Offset: 0x0005F4C8
		public override void remove_onfilterchange(HTMLFormElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F32 RID: 98098 RVA: 0x000605B8 File Offset: 0x0005F5B8
		public override void add_onselectstart(HTMLFormElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F33 RID: 98099 RVA: 0x00060648 File Offset: 0x0005F648
		public override void remove_onselectstart(HTMLFormElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F34 RID: 98100 RVA: 0x00060738 File Offset: 0x0005F738
		public override void add_onmouseup(HTMLFormElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F35 RID: 98101 RVA: 0x000607C8 File Offset: 0x0005F7C8
		public override void remove_onmouseup(HTMLFormElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F36 RID: 98102 RVA: 0x000608B8 File Offset: 0x0005F8B8
		public override void add_onmousedown(HTMLFormElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F37 RID: 98103 RVA: 0x00060948 File Offset: 0x0005F948
		public override void remove_onmousedown(HTMLFormElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F38 RID: 98104 RVA: 0x00060A38 File Offset: 0x0005FA38
		public override void add_onmousemove(HTMLFormElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F39 RID: 98105 RVA: 0x00060AC8 File Offset: 0x0005FAC8
		public override void remove_onmousemove(HTMLFormElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F3A RID: 98106 RVA: 0x00060BB8 File Offset: 0x0005FBB8
		public override void add_onmouseover(HTMLFormElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F3B RID: 98107 RVA: 0x00060C48 File Offset: 0x0005FC48
		public override void remove_onmouseover(HTMLFormElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F3C RID: 98108 RVA: 0x00060D38 File Offset: 0x0005FD38
		public override void add_onmouseout(HTMLFormElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F3D RID: 98109 RVA: 0x00060DC8 File Offset: 0x0005FDC8
		public override void remove_onmouseout(HTMLFormElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F3E RID: 98110 RVA: 0x00060EB8 File Offset: 0x0005FEB8
		public override void add_onkeyup(HTMLFormElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F3F RID: 98111 RVA: 0x00060F48 File Offset: 0x0005FF48
		public override void remove_onkeyup(HTMLFormElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F40 RID: 98112 RVA: 0x00061038 File Offset: 0x00060038
		public override void add_onkeydown(HTMLFormElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F41 RID: 98113 RVA: 0x000610C8 File Offset: 0x000600C8
		public override void remove_onkeydown(HTMLFormElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F42 RID: 98114 RVA: 0x000611B8 File Offset: 0x000601B8
		public override void add_onkeypress(HTMLFormElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F43 RID: 98115 RVA: 0x00061248 File Offset: 0x00060248
		public override void remove_onkeypress(HTMLFormElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F44 RID: 98116 RVA: 0x00061338 File Offset: 0x00060338
		public override void add_ondblclick(HTMLFormElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F45 RID: 98117 RVA: 0x000613C8 File Offset: 0x000603C8
		public override void remove_ondblclick(HTMLFormElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmlformElementEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F46 RID: 98118 RVA: 0x000614B8 File Offset: 0x000604B8
		public override void add_onclick(HTMLFormElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F47 RID: 98119 RVA: 0x00061548 File Offset: 0x00060548
		public override void remove_onclick(HTMLFormElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onclickDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F48 RID: 98120 RVA: 0x00061638 File Offset: 0x00060638
		public override void add_onhelp(HTMLFormElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = new HTMLFormElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents2_SinkHelper, ref num);
				htmlformElementEvents2_SinkHelper.m_dwCookie = num;
				htmlformElementEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F49 RID: 98121 RVA: 0x000616C8 File Offset: 0x000606C8
		public override void remove_onhelp(HTMLFormElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper;
					for (;;)
					{
						htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents2_SinkHelper.m_onhelpDelegate != null && ((htmlformElementEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F4A RID: 98122 RVA: 0x000617B8 File Offset: 0x000607B8
		public HTMLFormElementEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06017F4B RID: 98123 RVA: 0x000617E0 File Offset: 0x000607E0
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
								HTMLFormElementEvents2_SinkHelper htmlformElementEvents2_SinkHelper = (HTMLFormElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlformElementEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x06017F4C RID: 98124 RVA: 0x00061894 File Offset: 0x00060894
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400085C RID: 2140
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x0400085D RID: 2141
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x0400085E RID: 2142
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
