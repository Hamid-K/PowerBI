using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DD3 RID: 3539
	internal sealed class HTMLAreaEvents_EventProvider : HTMLAreaEvents_Event, IDisposable
	{
		// Token: 0x06017B85 RID: 97157 RVA: 0x0003DB40 File Offset: 0x0003CB40
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				102, 243, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06017B86 RID: 97158 RVA: 0x0003DC54 File Offset: 0x0003CC54
		public override void add_onfocusout(HTMLAreaEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017B87 RID: 97159 RVA: 0x0003DCE4 File Offset: 0x0003CCE4
		public override void remove_onfocusout(HTMLAreaEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmlareaEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B88 RID: 97160 RVA: 0x0003DDD4 File Offset: 0x0003CDD4
		public override void add_onfocusin(HTMLAreaEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017B89 RID: 97161 RVA: 0x0003DE64 File Offset: 0x0003CE64
		public override void remove_onfocusin(HTMLAreaEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onfocusinDelegate != null && ((htmlareaEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B8A RID: 97162 RVA: 0x0003DF54 File Offset: 0x0003CF54
		public override void add_ondeactivate(HTMLAreaEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017B8B RID: 97163 RVA: 0x0003DFE4 File Offset: 0x0003CFE4
		public override void remove_ondeactivate(HTMLAreaEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmlareaEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B8C RID: 97164 RVA: 0x0003E0D4 File Offset: 0x0003D0D4
		public override void add_onactivate(HTMLAreaEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017B8D RID: 97165 RVA: 0x0003E164 File Offset: 0x0003D164
		public override void remove_onactivate(HTMLAreaEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onactivateDelegate != null && ((htmlareaEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B8E RID: 97166 RVA: 0x0003E254 File Offset: 0x0003D254
		public override void add_onmousewheel(HTMLAreaEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017B8F RID: 97167 RVA: 0x0003E2E4 File Offset: 0x0003D2E4
		public override void remove_onmousewheel(HTMLAreaEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmlareaEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B90 RID: 97168 RVA: 0x0003E3D4 File Offset: 0x0003D3D4
		public override void add_onmouseleave(HTMLAreaEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017B91 RID: 97169 RVA: 0x0003E464 File Offset: 0x0003D464
		public override void remove_onmouseleave(HTMLAreaEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmlareaEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B92 RID: 97170 RVA: 0x0003E554 File Offset: 0x0003D554
		public override void add_onmouseenter(HTMLAreaEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017B93 RID: 97171 RVA: 0x0003E5E4 File Offset: 0x0003D5E4
		public override void remove_onmouseenter(HTMLAreaEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmlareaEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B94 RID: 97172 RVA: 0x0003E6D4 File Offset: 0x0003D6D4
		public override void add_onresizeend(HTMLAreaEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017B95 RID: 97173 RVA: 0x0003E764 File Offset: 0x0003D764
		public override void remove_onresizeend(HTMLAreaEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onresizeendDelegate != null && ((htmlareaEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B96 RID: 97174 RVA: 0x0003E854 File Offset: 0x0003D854
		public override void add_onresizestart(HTMLAreaEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017B97 RID: 97175 RVA: 0x0003E8E4 File Offset: 0x0003D8E4
		public override void remove_onresizestart(HTMLAreaEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onresizestartDelegate != null && ((htmlareaEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B98 RID: 97176 RVA: 0x0003E9D4 File Offset: 0x0003D9D4
		public override void add_onmoveend(HTMLAreaEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017B99 RID: 97177 RVA: 0x0003EA64 File Offset: 0x0003DA64
		public override void remove_onmoveend(HTMLAreaEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onmoveendDelegate != null && ((htmlareaEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B9A RID: 97178 RVA: 0x0003EB54 File Offset: 0x0003DB54
		public override void add_onmovestart(HTMLAreaEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017B9B RID: 97179 RVA: 0x0003EBE4 File Offset: 0x0003DBE4
		public override void remove_onmovestart(HTMLAreaEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onmovestartDelegate != null && ((htmlareaEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B9C RID: 97180 RVA: 0x0003ECD4 File Offset: 0x0003DCD4
		public override void add_oncontrolselect(HTMLAreaEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017B9D RID: 97181 RVA: 0x0003ED64 File Offset: 0x0003DD64
		public override void remove_oncontrolselect(HTMLAreaEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmlareaEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B9E RID: 97182 RVA: 0x0003EE54 File Offset: 0x0003DE54
		public override void add_onmove(HTMLAreaEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017B9F RID: 97183 RVA: 0x0003EEE4 File Offset: 0x0003DEE4
		public override void remove_onmove(HTMLAreaEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onmoveDelegate != null && ((htmlareaEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BA0 RID: 97184 RVA: 0x0003EFD4 File Offset: 0x0003DFD4
		public override void add_onbeforeactivate(HTMLAreaEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BA1 RID: 97185 RVA: 0x0003F064 File Offset: 0x0003E064
		public override void remove_onbeforeactivate(HTMLAreaEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlareaEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BA2 RID: 97186 RVA: 0x0003F154 File Offset: 0x0003E154
		public override void add_onbeforedeactivate(HTMLAreaEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BA3 RID: 97187 RVA: 0x0003F1E4 File Offset: 0x0003E1E4
		public override void remove_onbeforedeactivate(HTMLAreaEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlareaEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BA4 RID: 97188 RVA: 0x0003F2D4 File Offset: 0x0003E2D4
		public override void add_onpage(HTMLAreaEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BA5 RID: 97189 RVA: 0x0003F364 File Offset: 0x0003E364
		public override void remove_onpage(HTMLAreaEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onpageDelegate != null && ((htmlareaEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BA6 RID: 97190 RVA: 0x0003F454 File Offset: 0x0003E454
		public override void add_onlayoutcomplete(HTMLAreaEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BA7 RID: 97191 RVA: 0x0003F4E4 File Offset: 0x0003E4E4
		public override void remove_onlayoutcomplete(HTMLAreaEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlareaEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BA8 RID: 97192 RVA: 0x0003F5D4 File Offset: 0x0003E5D4
		public override void add_onbeforeeditfocus(HTMLAreaEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BA9 RID: 97193 RVA: 0x0003F664 File Offset: 0x0003E664
		public override void remove_onbeforeeditfocus(HTMLAreaEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmlareaEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BAA RID: 97194 RVA: 0x0003F754 File Offset: 0x0003E754
		public override void add_onreadystatechange(HTMLAreaEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BAB RID: 97195 RVA: 0x0003F7E4 File Offset: 0x0003E7E4
		public override void remove_onreadystatechange(HTMLAreaEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlareaEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BAC RID: 97196 RVA: 0x0003F8D4 File Offset: 0x0003E8D4
		public override void add_oncellchange(HTMLAreaEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BAD RID: 97197 RVA: 0x0003F964 File Offset: 0x0003E964
		public override void remove_oncellchange(HTMLAreaEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlareaEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BAE RID: 97198 RVA: 0x0003FA54 File Offset: 0x0003EA54
		public override void add_onrowsinserted(HTMLAreaEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BAF RID: 97199 RVA: 0x0003FAE4 File Offset: 0x0003EAE4
		public override void remove_onrowsinserted(HTMLAreaEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlareaEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BB0 RID: 97200 RVA: 0x0003FBD4 File Offset: 0x0003EBD4
		public override void add_onrowsdelete(HTMLAreaEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BB1 RID: 97201 RVA: 0x0003FC64 File Offset: 0x0003EC64
		public override void remove_onrowsdelete(HTMLAreaEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlareaEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BB2 RID: 97202 RVA: 0x0003FD54 File Offset: 0x0003ED54
		public override void add_oncontextmenu(HTMLAreaEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BB3 RID: 97203 RVA: 0x0003FDE4 File Offset: 0x0003EDE4
		public override void remove_oncontextmenu(HTMLAreaEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmlareaEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BB4 RID: 97204 RVA: 0x0003FED4 File Offset: 0x0003EED4
		public override void add_onpaste(HTMLAreaEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BB5 RID: 97205 RVA: 0x0003FF64 File Offset: 0x0003EF64
		public override void remove_onpaste(HTMLAreaEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onpasteDelegate != null && ((htmlareaEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BB6 RID: 97206 RVA: 0x00040054 File Offset: 0x0003F054
		public override void add_onbeforepaste(HTMLAreaEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BB7 RID: 97207 RVA: 0x000400E4 File Offset: 0x0003F0E4
		public override void remove_onbeforepaste(HTMLAreaEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmlareaEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BB8 RID: 97208 RVA: 0x000401D4 File Offset: 0x0003F1D4
		public override void add_oncopy(HTMLAreaEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BB9 RID: 97209 RVA: 0x00040264 File Offset: 0x0003F264
		public override void remove_oncopy(HTMLAreaEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_oncopyDelegate != null && ((htmlareaEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BBA RID: 97210 RVA: 0x00040354 File Offset: 0x0003F354
		public override void add_onbeforecopy(HTMLAreaEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BBB RID: 97211 RVA: 0x000403E4 File Offset: 0x0003F3E4
		public override void remove_onbeforecopy(HTMLAreaEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmlareaEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BBC RID: 97212 RVA: 0x000404D4 File Offset: 0x0003F4D4
		public override void add_oncut(HTMLAreaEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BBD RID: 97213 RVA: 0x00040564 File Offset: 0x0003F564
		public override void remove_oncut(HTMLAreaEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_oncutDelegate != null && ((htmlareaEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BBE RID: 97214 RVA: 0x00040654 File Offset: 0x0003F654
		public override void add_onbeforecut(HTMLAreaEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BBF RID: 97215 RVA: 0x000406E4 File Offset: 0x0003F6E4
		public override void remove_onbeforecut(HTMLAreaEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmlareaEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BC0 RID: 97216 RVA: 0x000407D4 File Offset: 0x0003F7D4
		public override void add_ondrop(HTMLAreaEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BC1 RID: 97217 RVA: 0x00040864 File Offset: 0x0003F864
		public override void remove_ondrop(HTMLAreaEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_ondropDelegate != null && ((htmlareaEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BC2 RID: 97218 RVA: 0x00040954 File Offset: 0x0003F954
		public override void add_ondragleave(HTMLAreaEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BC3 RID: 97219 RVA: 0x000409E4 File Offset: 0x0003F9E4
		public override void remove_ondragleave(HTMLAreaEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmlareaEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BC4 RID: 97220 RVA: 0x00040AD4 File Offset: 0x0003FAD4
		public override void add_ondragover(HTMLAreaEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BC5 RID: 97221 RVA: 0x00040B64 File Offset: 0x0003FB64
		public override void remove_ondragover(HTMLAreaEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_ondragoverDelegate != null && ((htmlareaEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BC6 RID: 97222 RVA: 0x00040C54 File Offset: 0x0003FC54
		public override void add_ondragenter(HTMLAreaEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BC7 RID: 97223 RVA: 0x00040CE4 File Offset: 0x0003FCE4
		public override void remove_ondragenter(HTMLAreaEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_ondragenterDelegate != null && ((htmlareaEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BC8 RID: 97224 RVA: 0x00040DD4 File Offset: 0x0003FDD4
		public override void add_ondragend(HTMLAreaEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BC9 RID: 97225 RVA: 0x00040E64 File Offset: 0x0003FE64
		public override void remove_ondragend(HTMLAreaEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_ondragendDelegate != null && ((htmlareaEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BCA RID: 97226 RVA: 0x00040F54 File Offset: 0x0003FF54
		public override void add_ondrag(HTMLAreaEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BCB RID: 97227 RVA: 0x00040FE4 File Offset: 0x0003FFE4
		public override void remove_ondrag(HTMLAreaEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_ondragDelegate != null && ((htmlareaEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BCC RID: 97228 RVA: 0x000410D4 File Offset: 0x000400D4
		public override void add_onresize(HTMLAreaEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BCD RID: 97229 RVA: 0x00041164 File Offset: 0x00040164
		public override void remove_onresize(HTMLAreaEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onresizeDelegate != null && ((htmlareaEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BCE RID: 97230 RVA: 0x00041254 File Offset: 0x00040254
		public override void add_onblur(HTMLAreaEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BCF RID: 97231 RVA: 0x000412E4 File Offset: 0x000402E4
		public override void remove_onblur(HTMLAreaEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onblurDelegate != null && ((htmlareaEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BD0 RID: 97232 RVA: 0x000413D4 File Offset: 0x000403D4
		public override void add_onfocus(HTMLAreaEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BD1 RID: 97233 RVA: 0x00041464 File Offset: 0x00040464
		public override void remove_onfocus(HTMLAreaEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onfocusDelegate != null && ((htmlareaEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BD2 RID: 97234 RVA: 0x00041554 File Offset: 0x00040554
		public override void add_onscroll(HTMLAreaEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BD3 RID: 97235 RVA: 0x000415E4 File Offset: 0x000405E4
		public override void remove_onscroll(HTMLAreaEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onscrollDelegate != null && ((htmlareaEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BD4 RID: 97236 RVA: 0x000416D4 File Offset: 0x000406D4
		public override void add_onpropertychange(HTMLAreaEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BD5 RID: 97237 RVA: 0x00041764 File Offset: 0x00040764
		public override void remove_onpropertychange(HTMLAreaEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmlareaEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BD6 RID: 97238 RVA: 0x00041854 File Offset: 0x00040854
		public override void add_onlosecapture(HTMLAreaEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BD7 RID: 97239 RVA: 0x000418E4 File Offset: 0x000408E4
		public override void remove_onlosecapture(HTMLAreaEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmlareaEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BD8 RID: 97240 RVA: 0x000419D4 File Offset: 0x000409D4
		public override void add_ondatasetcomplete(HTMLAreaEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BD9 RID: 97241 RVA: 0x00041A64 File Offset: 0x00040A64
		public override void remove_ondatasetcomplete(HTMLAreaEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlareaEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BDA RID: 97242 RVA: 0x00041B54 File Offset: 0x00040B54
		public override void add_ondataavailable(HTMLAreaEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BDB RID: 97243 RVA: 0x00041BE4 File Offset: 0x00040BE4
		public override void remove_ondataavailable(HTMLAreaEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlareaEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BDC RID: 97244 RVA: 0x00041CD4 File Offset: 0x00040CD4
		public override void add_ondatasetchanged(HTMLAreaEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BDD RID: 97245 RVA: 0x00041D64 File Offset: 0x00040D64
		public override void remove_ondatasetchanged(HTMLAreaEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlareaEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BDE RID: 97246 RVA: 0x00041E54 File Offset: 0x00040E54
		public override void add_onrowenter(HTMLAreaEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BDF RID: 97247 RVA: 0x00041EE4 File Offset: 0x00040EE4
		public override void remove_onrowenter(HTMLAreaEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlareaEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BE0 RID: 97248 RVA: 0x00041FD4 File Offset: 0x00040FD4
		public override void add_onrowexit(HTMLAreaEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BE1 RID: 97249 RVA: 0x00042064 File Offset: 0x00041064
		public override void remove_onrowexit(HTMLAreaEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlareaEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BE2 RID: 97250 RVA: 0x00042154 File Offset: 0x00041154
		public override void add_onerrorupdate(HTMLAreaEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BE3 RID: 97251 RVA: 0x000421E4 File Offset: 0x000411E4
		public override void remove_onerrorupdate(HTMLAreaEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlareaEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BE4 RID: 97252 RVA: 0x000422D4 File Offset: 0x000412D4
		public override void add_onafterupdate(HTMLAreaEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BE5 RID: 97253 RVA: 0x00042364 File Offset: 0x00041364
		public override void remove_onafterupdate(HTMLAreaEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlareaEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BE6 RID: 97254 RVA: 0x00042454 File Offset: 0x00041454
		public override void add_onbeforeupdate(HTMLAreaEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BE7 RID: 97255 RVA: 0x000424E4 File Offset: 0x000414E4
		public override void remove_onbeforeupdate(HTMLAreaEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlareaEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BE8 RID: 97256 RVA: 0x000425D4 File Offset: 0x000415D4
		public override void add_ondragstart(HTMLAreaEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BE9 RID: 97257 RVA: 0x00042664 File Offset: 0x00041664
		public override void remove_ondragstart(HTMLAreaEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_ondragstartDelegate != null && ((htmlareaEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BEA RID: 97258 RVA: 0x00042754 File Offset: 0x00041754
		public override void add_onfilterchange(HTMLAreaEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BEB RID: 97259 RVA: 0x000427E4 File Offset: 0x000417E4
		public override void remove_onfilterchange(HTMLAreaEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmlareaEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BEC RID: 97260 RVA: 0x000428D4 File Offset: 0x000418D4
		public override void add_onselectstart(HTMLAreaEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BED RID: 97261 RVA: 0x00042964 File Offset: 0x00041964
		public override void remove_onselectstart(HTMLAreaEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onselectstartDelegate != null && ((htmlareaEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BEE RID: 97262 RVA: 0x00042A54 File Offset: 0x00041A54
		public override void add_onmouseup(HTMLAreaEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BEF RID: 97263 RVA: 0x00042AE4 File Offset: 0x00041AE4
		public override void remove_onmouseup(HTMLAreaEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onmouseupDelegate != null && ((htmlareaEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BF0 RID: 97264 RVA: 0x00042BD4 File Offset: 0x00041BD4
		public override void add_onmousedown(HTMLAreaEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BF1 RID: 97265 RVA: 0x00042C64 File Offset: 0x00041C64
		public override void remove_onmousedown(HTMLAreaEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onmousedownDelegate != null && ((htmlareaEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BF2 RID: 97266 RVA: 0x00042D54 File Offset: 0x00041D54
		public override void add_onmousemove(HTMLAreaEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BF3 RID: 97267 RVA: 0x00042DE4 File Offset: 0x00041DE4
		public override void remove_onmousemove(HTMLAreaEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmlareaEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BF4 RID: 97268 RVA: 0x00042ED4 File Offset: 0x00041ED4
		public override void add_onmouseover(HTMLAreaEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BF5 RID: 97269 RVA: 0x00042F64 File Offset: 0x00041F64
		public override void remove_onmouseover(HTMLAreaEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmlareaEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BF6 RID: 97270 RVA: 0x00043054 File Offset: 0x00042054
		public override void add_onmouseout(HTMLAreaEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BF7 RID: 97271 RVA: 0x000430E4 File Offset: 0x000420E4
		public override void remove_onmouseout(HTMLAreaEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmlareaEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BF8 RID: 97272 RVA: 0x000431D4 File Offset: 0x000421D4
		public override void add_onkeyup(HTMLAreaEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BF9 RID: 97273 RVA: 0x00043264 File Offset: 0x00042264
		public override void remove_onkeyup(HTMLAreaEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onkeyupDelegate != null && ((htmlareaEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BFA RID: 97274 RVA: 0x00043354 File Offset: 0x00042354
		public override void add_onkeydown(HTMLAreaEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BFB RID: 97275 RVA: 0x000433E4 File Offset: 0x000423E4
		public override void remove_onkeydown(HTMLAreaEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onkeydownDelegate != null && ((htmlareaEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BFC RID: 97276 RVA: 0x000434D4 File Offset: 0x000424D4
		public override void add_onkeypress(HTMLAreaEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BFD RID: 97277 RVA: 0x00043564 File Offset: 0x00042564
		public override void remove_onkeypress(HTMLAreaEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onkeypressDelegate != null && ((htmlareaEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017BFE RID: 97278 RVA: 0x00043654 File Offset: 0x00042654
		public override void add_ondblclick(HTMLAreaEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017BFF RID: 97279 RVA: 0x000436E4 File Offset: 0x000426E4
		public override void remove_ondblclick(HTMLAreaEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_ondblclickDelegate != null && ((htmlareaEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C00 RID: 97280 RVA: 0x000437D4 File Offset: 0x000427D4
		public override void add_onclick(HTMLAreaEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017C01 RID: 97281 RVA: 0x00043864 File Offset: 0x00042864
		public override void remove_onclick(HTMLAreaEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onclickDelegate != null && ((htmlareaEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C02 RID: 97282 RVA: 0x00043954 File Offset: 0x00042954
		public override void add_onhelp(HTMLAreaEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = new HTMLAreaEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents_SinkHelper, ref num);
				htmlareaEvents_SinkHelper.m_dwCookie = num;
				htmlareaEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents_SinkHelper);
			}
		}

		// Token: 0x06017C03 RID: 97283 RVA: 0x000439E4 File Offset: 0x000429E4
		public override void remove_onhelp(HTMLAreaEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper;
					for (;;)
					{
						htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents_SinkHelper.m_onhelpDelegate != null && ((htmlareaEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C04 RID: 97284 RVA: 0x00043AD4 File Offset: 0x00042AD4
		public HTMLAreaEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06017C05 RID: 97285 RVA: 0x00043AFC File Offset: 0x00042AFC
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
								HTMLAreaEvents_SinkHelper htmlareaEvents_SinkHelper = (HTMLAreaEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlareaEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x06017C06 RID: 97286 RVA: 0x00043BB0 File Offset: 0x00042BB0
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000739 RID: 1849
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x0400073A RID: 1850
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x0400073B RID: 1851
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
