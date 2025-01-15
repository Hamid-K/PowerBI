using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DC9 RID: 3529
	internal sealed class HTMLControlElementEvents_EventProvider : HTMLControlElementEvents_Event, IDisposable
	{
		// Token: 0x06017839 RID: 96313 RVA: 0x0001FC08 File Offset: 0x0001EC08
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				234, 244, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x0601783A RID: 96314 RVA: 0x0001FD1C File Offset: 0x0001ED1C
		public override void add_onfocusout(HTMLControlElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601783B RID: 96315 RVA: 0x0001FDAC File Offset: 0x0001EDAC
		public override void remove_onfocusout(HTMLControlElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601783C RID: 96316 RVA: 0x0001FE9C File Offset: 0x0001EE9C
		public override void add_onfocusin(HTMLControlElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601783D RID: 96317 RVA: 0x0001FF2C File Offset: 0x0001EF2C
		public override void remove_onfocusin(HTMLControlElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onfocusinDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601783E RID: 96318 RVA: 0x0002001C File Offset: 0x0001F01C
		public override void add_ondeactivate(HTMLControlElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601783F RID: 96319 RVA: 0x000200AC File Offset: 0x0001F0AC
		public override void remove_ondeactivate(HTMLControlElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017840 RID: 96320 RVA: 0x0002019C File Offset: 0x0001F19C
		public override void add_onactivate(HTMLControlElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017841 RID: 96321 RVA: 0x0002022C File Offset: 0x0001F22C
		public override void remove_onactivate(HTMLControlElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onactivateDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017842 RID: 96322 RVA: 0x0002031C File Offset: 0x0001F31C
		public override void add_onmousewheel(HTMLControlElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017843 RID: 96323 RVA: 0x000203AC File Offset: 0x0001F3AC
		public override void remove_onmousewheel(HTMLControlElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017844 RID: 96324 RVA: 0x0002049C File Offset: 0x0001F49C
		public override void add_onmouseleave(HTMLControlElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017845 RID: 96325 RVA: 0x0002052C File Offset: 0x0001F52C
		public override void remove_onmouseleave(HTMLControlElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017846 RID: 96326 RVA: 0x0002061C File Offset: 0x0001F61C
		public override void add_onmouseenter(HTMLControlElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017847 RID: 96327 RVA: 0x000206AC File Offset: 0x0001F6AC
		public override void remove_onmouseenter(HTMLControlElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017848 RID: 96328 RVA: 0x0002079C File Offset: 0x0001F79C
		public override void add_onresizeend(HTMLControlElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017849 RID: 96329 RVA: 0x0002082C File Offset: 0x0001F82C
		public override void remove_onresizeend(HTMLControlElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onresizeendDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601784A RID: 96330 RVA: 0x0002091C File Offset: 0x0001F91C
		public override void add_onresizestart(HTMLControlElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601784B RID: 96331 RVA: 0x000209AC File Offset: 0x0001F9AC
		public override void remove_onresizestart(HTMLControlElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onresizestartDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601784C RID: 96332 RVA: 0x00020A9C File Offset: 0x0001FA9C
		public override void add_onmoveend(HTMLControlElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601784D RID: 96333 RVA: 0x00020B2C File Offset: 0x0001FB2C
		public override void remove_onmoveend(HTMLControlElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onmoveendDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601784E RID: 96334 RVA: 0x00020C1C File Offset: 0x0001FC1C
		public override void add_onmovestart(HTMLControlElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601784F RID: 96335 RVA: 0x00020CAC File Offset: 0x0001FCAC
		public override void remove_onmovestart(HTMLControlElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onmovestartDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017850 RID: 96336 RVA: 0x00020D9C File Offset: 0x0001FD9C
		public override void add_oncontrolselect(HTMLControlElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017851 RID: 96337 RVA: 0x00020E2C File Offset: 0x0001FE2C
		public override void remove_oncontrolselect(HTMLControlElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017852 RID: 96338 RVA: 0x00020F1C File Offset: 0x0001FF1C
		public override void add_onmove(HTMLControlElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017853 RID: 96339 RVA: 0x00020FAC File Offset: 0x0001FFAC
		public override void remove_onmove(HTMLControlElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onmoveDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017854 RID: 96340 RVA: 0x0002109C File Offset: 0x0002009C
		public override void add_onbeforeactivate(HTMLControlElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017855 RID: 96341 RVA: 0x0002112C File Offset: 0x0002012C
		public override void remove_onbeforeactivate(HTMLControlElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017856 RID: 96342 RVA: 0x0002121C File Offset: 0x0002021C
		public override void add_onbeforedeactivate(HTMLControlElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017857 RID: 96343 RVA: 0x000212AC File Offset: 0x000202AC
		public override void remove_onbeforedeactivate(HTMLControlElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017858 RID: 96344 RVA: 0x0002139C File Offset: 0x0002039C
		public override void add_onpage(HTMLControlElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017859 RID: 96345 RVA: 0x0002142C File Offset: 0x0002042C
		public override void remove_onpage(HTMLControlElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onpageDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601785A RID: 96346 RVA: 0x0002151C File Offset: 0x0002051C
		public override void add_onlayoutcomplete(HTMLControlElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601785B RID: 96347 RVA: 0x000215AC File Offset: 0x000205AC
		public override void remove_onlayoutcomplete(HTMLControlElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601785C RID: 96348 RVA: 0x0002169C File Offset: 0x0002069C
		public override void add_onbeforeeditfocus(HTMLControlElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601785D RID: 96349 RVA: 0x0002172C File Offset: 0x0002072C
		public override void remove_onbeforeeditfocus(HTMLControlElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601785E RID: 96350 RVA: 0x0002181C File Offset: 0x0002081C
		public override void add_onreadystatechange(HTMLControlElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601785F RID: 96351 RVA: 0x000218AC File Offset: 0x000208AC
		public override void remove_onreadystatechange(HTMLControlElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017860 RID: 96352 RVA: 0x0002199C File Offset: 0x0002099C
		public override void add_oncellchange(HTMLControlElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017861 RID: 96353 RVA: 0x00021A2C File Offset: 0x00020A2C
		public override void remove_oncellchange(HTMLControlElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017862 RID: 96354 RVA: 0x00021B1C File Offset: 0x00020B1C
		public override void add_onrowsinserted(HTMLControlElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017863 RID: 96355 RVA: 0x00021BAC File Offset: 0x00020BAC
		public override void remove_onrowsinserted(HTMLControlElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017864 RID: 96356 RVA: 0x00021C9C File Offset: 0x00020C9C
		public override void add_onrowsdelete(HTMLControlElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017865 RID: 96357 RVA: 0x00021D2C File Offset: 0x00020D2C
		public override void remove_onrowsdelete(HTMLControlElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017866 RID: 96358 RVA: 0x00021E1C File Offset: 0x00020E1C
		public override void add_oncontextmenu(HTMLControlElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017867 RID: 96359 RVA: 0x00021EAC File Offset: 0x00020EAC
		public override void remove_oncontextmenu(HTMLControlElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017868 RID: 96360 RVA: 0x00021F9C File Offset: 0x00020F9C
		public override void add_onpaste(HTMLControlElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017869 RID: 96361 RVA: 0x0002202C File Offset: 0x0002102C
		public override void remove_onpaste(HTMLControlElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onpasteDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601786A RID: 96362 RVA: 0x0002211C File Offset: 0x0002111C
		public override void add_onbeforepaste(HTMLControlElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601786B RID: 96363 RVA: 0x000221AC File Offset: 0x000211AC
		public override void remove_onbeforepaste(HTMLControlElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601786C RID: 96364 RVA: 0x0002229C File Offset: 0x0002129C
		public override void add_oncopy(HTMLControlElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601786D RID: 96365 RVA: 0x0002232C File Offset: 0x0002132C
		public override void remove_oncopy(HTMLControlElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_oncopyDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601786E RID: 96366 RVA: 0x0002241C File Offset: 0x0002141C
		public override void add_onbeforecopy(HTMLControlElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601786F RID: 96367 RVA: 0x000224AC File Offset: 0x000214AC
		public override void remove_onbeforecopy(HTMLControlElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017870 RID: 96368 RVA: 0x0002259C File Offset: 0x0002159C
		public override void add_oncut(HTMLControlElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017871 RID: 96369 RVA: 0x0002262C File Offset: 0x0002162C
		public override void remove_oncut(HTMLControlElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_oncutDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017872 RID: 96370 RVA: 0x0002271C File Offset: 0x0002171C
		public override void add_onbeforecut(HTMLControlElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017873 RID: 96371 RVA: 0x000227AC File Offset: 0x000217AC
		public override void remove_onbeforecut(HTMLControlElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017874 RID: 96372 RVA: 0x0002289C File Offset: 0x0002189C
		public override void add_ondrop(HTMLControlElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017875 RID: 96373 RVA: 0x0002292C File Offset: 0x0002192C
		public override void remove_ondrop(HTMLControlElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_ondropDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017876 RID: 96374 RVA: 0x00022A1C File Offset: 0x00021A1C
		public override void add_ondragleave(HTMLControlElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017877 RID: 96375 RVA: 0x00022AAC File Offset: 0x00021AAC
		public override void remove_ondragleave(HTMLControlElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017878 RID: 96376 RVA: 0x00022B9C File Offset: 0x00021B9C
		public override void add_ondragover(HTMLControlElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017879 RID: 96377 RVA: 0x00022C2C File Offset: 0x00021C2C
		public override void remove_ondragover(HTMLControlElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_ondragoverDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601787A RID: 96378 RVA: 0x00022D1C File Offset: 0x00021D1C
		public override void add_ondragenter(HTMLControlElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601787B RID: 96379 RVA: 0x00022DAC File Offset: 0x00021DAC
		public override void remove_ondragenter(HTMLControlElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_ondragenterDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601787C RID: 96380 RVA: 0x00022E9C File Offset: 0x00021E9C
		public override void add_ondragend(HTMLControlElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601787D RID: 96381 RVA: 0x00022F2C File Offset: 0x00021F2C
		public override void remove_ondragend(HTMLControlElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_ondragendDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601787E RID: 96382 RVA: 0x0002301C File Offset: 0x0002201C
		public override void add_ondrag(HTMLControlElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601787F RID: 96383 RVA: 0x000230AC File Offset: 0x000220AC
		public override void remove_ondrag(HTMLControlElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_ondragDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017880 RID: 96384 RVA: 0x0002319C File Offset: 0x0002219C
		public override void add_onresize(HTMLControlElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017881 RID: 96385 RVA: 0x0002322C File Offset: 0x0002222C
		public override void remove_onresize(HTMLControlElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onresizeDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017882 RID: 96386 RVA: 0x0002331C File Offset: 0x0002231C
		public override void add_onblur(HTMLControlElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017883 RID: 96387 RVA: 0x000233AC File Offset: 0x000223AC
		public override void remove_onblur(HTMLControlElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onblurDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017884 RID: 96388 RVA: 0x0002349C File Offset: 0x0002249C
		public override void add_onfocus(HTMLControlElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017885 RID: 96389 RVA: 0x0002352C File Offset: 0x0002252C
		public override void remove_onfocus(HTMLControlElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onfocusDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017886 RID: 96390 RVA: 0x0002361C File Offset: 0x0002261C
		public override void add_onscroll(HTMLControlElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017887 RID: 96391 RVA: 0x000236AC File Offset: 0x000226AC
		public override void remove_onscroll(HTMLControlElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onscrollDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017888 RID: 96392 RVA: 0x0002379C File Offset: 0x0002279C
		public override void add_onpropertychange(HTMLControlElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017889 RID: 96393 RVA: 0x0002382C File Offset: 0x0002282C
		public override void remove_onpropertychange(HTMLControlElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601788A RID: 96394 RVA: 0x0002391C File Offset: 0x0002291C
		public override void add_onlosecapture(HTMLControlElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601788B RID: 96395 RVA: 0x000239AC File Offset: 0x000229AC
		public override void remove_onlosecapture(HTMLControlElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601788C RID: 96396 RVA: 0x00023A9C File Offset: 0x00022A9C
		public override void add_ondatasetcomplete(HTMLControlElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601788D RID: 96397 RVA: 0x00023B2C File Offset: 0x00022B2C
		public override void remove_ondatasetcomplete(HTMLControlElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601788E RID: 96398 RVA: 0x00023C1C File Offset: 0x00022C1C
		public override void add_ondataavailable(HTMLControlElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601788F RID: 96399 RVA: 0x00023CAC File Offset: 0x00022CAC
		public override void remove_ondataavailable(HTMLControlElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017890 RID: 96400 RVA: 0x00023D9C File Offset: 0x00022D9C
		public override void add_ondatasetchanged(HTMLControlElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017891 RID: 96401 RVA: 0x00023E2C File Offset: 0x00022E2C
		public override void remove_ondatasetchanged(HTMLControlElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017892 RID: 96402 RVA: 0x00023F1C File Offset: 0x00022F1C
		public override void add_onrowenter(HTMLControlElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017893 RID: 96403 RVA: 0x00023FAC File Offset: 0x00022FAC
		public override void remove_onrowenter(HTMLControlElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017894 RID: 96404 RVA: 0x0002409C File Offset: 0x0002309C
		public override void add_onrowexit(HTMLControlElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017895 RID: 96405 RVA: 0x0002412C File Offset: 0x0002312C
		public override void remove_onrowexit(HTMLControlElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017896 RID: 96406 RVA: 0x0002421C File Offset: 0x0002321C
		public override void add_onerrorupdate(HTMLControlElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017897 RID: 96407 RVA: 0x000242AC File Offset: 0x000232AC
		public override void remove_onerrorupdate(HTMLControlElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017898 RID: 96408 RVA: 0x0002439C File Offset: 0x0002339C
		public override void add_onafterupdate(HTMLControlElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017899 RID: 96409 RVA: 0x0002442C File Offset: 0x0002342C
		public override void remove_onafterupdate(HTMLControlElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601789A RID: 96410 RVA: 0x0002451C File Offset: 0x0002351C
		public override void add_onbeforeupdate(HTMLControlElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601789B RID: 96411 RVA: 0x000245AC File Offset: 0x000235AC
		public override void remove_onbeforeupdate(HTMLControlElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601789C RID: 96412 RVA: 0x0002469C File Offset: 0x0002369C
		public override void add_ondragstart(HTMLControlElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601789D RID: 96413 RVA: 0x0002472C File Offset: 0x0002372C
		public override void remove_ondragstart(HTMLControlElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_ondragstartDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601789E RID: 96414 RVA: 0x0002481C File Offset: 0x0002381C
		public override void add_onfilterchange(HTMLControlElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601789F RID: 96415 RVA: 0x000248AC File Offset: 0x000238AC
		public override void remove_onfilterchange(HTMLControlElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178A0 RID: 96416 RVA: 0x0002499C File Offset: 0x0002399C
		public override void add_onselectstart(HTMLControlElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x060178A1 RID: 96417 RVA: 0x00024A2C File Offset: 0x00023A2C
		public override void remove_onselectstart(HTMLControlElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onselectstartDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178A2 RID: 96418 RVA: 0x00024B1C File Offset: 0x00023B1C
		public override void add_onmouseup(HTMLControlElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x060178A3 RID: 96419 RVA: 0x00024BAC File Offset: 0x00023BAC
		public override void remove_onmouseup(HTMLControlElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onmouseupDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178A4 RID: 96420 RVA: 0x00024C9C File Offset: 0x00023C9C
		public override void add_onmousedown(HTMLControlElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x060178A5 RID: 96421 RVA: 0x00024D2C File Offset: 0x00023D2C
		public override void remove_onmousedown(HTMLControlElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onmousedownDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178A6 RID: 96422 RVA: 0x00024E1C File Offset: 0x00023E1C
		public override void add_onmousemove(HTMLControlElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x060178A7 RID: 96423 RVA: 0x00024EAC File Offset: 0x00023EAC
		public override void remove_onmousemove(HTMLControlElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178A8 RID: 96424 RVA: 0x00024F9C File Offset: 0x00023F9C
		public override void add_onmouseover(HTMLControlElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x060178A9 RID: 96425 RVA: 0x0002502C File Offset: 0x0002402C
		public override void remove_onmouseover(HTMLControlElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178AA RID: 96426 RVA: 0x0002511C File Offset: 0x0002411C
		public override void add_onmouseout(HTMLControlElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x060178AB RID: 96427 RVA: 0x000251AC File Offset: 0x000241AC
		public override void remove_onmouseout(HTMLControlElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178AC RID: 96428 RVA: 0x0002529C File Offset: 0x0002429C
		public override void add_onkeyup(HTMLControlElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x060178AD RID: 96429 RVA: 0x0002532C File Offset: 0x0002432C
		public override void remove_onkeyup(HTMLControlElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onkeyupDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178AE RID: 96430 RVA: 0x0002541C File Offset: 0x0002441C
		public override void add_onkeydown(HTMLControlElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x060178AF RID: 96431 RVA: 0x000254AC File Offset: 0x000244AC
		public override void remove_onkeydown(HTMLControlElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onkeydownDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178B0 RID: 96432 RVA: 0x0002559C File Offset: 0x0002459C
		public override void add_onkeypress(HTMLControlElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x060178B1 RID: 96433 RVA: 0x0002562C File Offset: 0x0002462C
		public override void remove_onkeypress(HTMLControlElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onkeypressDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178B2 RID: 96434 RVA: 0x0002571C File Offset: 0x0002471C
		public override void add_ondblclick(HTMLControlElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x060178B3 RID: 96435 RVA: 0x000257AC File Offset: 0x000247AC
		public override void remove_ondblclick(HTMLControlElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_ondblclickDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178B4 RID: 96436 RVA: 0x0002589C File Offset: 0x0002489C
		public override void add_onclick(HTMLControlElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x060178B5 RID: 96437 RVA: 0x0002592C File Offset: 0x0002492C
		public override void remove_onclick(HTMLControlElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onclickDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178B6 RID: 96438 RVA: 0x00025A1C File Offset: 0x00024A1C
		public override void add_onhelp(HTMLControlElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = new HTMLControlElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents_SinkHelper, ref num);
				htmlcontrolElementEvents_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents_SinkHelper);
			}
		}

		// Token: 0x060178B7 RID: 96439 RVA: 0x00025AAC File Offset: 0x00024AAC
		public override void remove_onhelp(HTMLControlElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents_SinkHelper.m_onhelpDelegate != null && ((htmlcontrolElementEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178B8 RID: 96440 RVA: 0x00025B9C File Offset: 0x00024B9C
		public HTMLControlElementEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x060178B9 RID: 96441 RVA: 0x00025BC4 File Offset: 0x00024BC4
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
								HTMLControlElementEvents_SinkHelper htmlcontrolElementEvents_SinkHelper = (HTMLControlElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x060178BA RID: 96442 RVA: 0x00025C78 File Offset: 0x00024C78
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000614 RID: 1556
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000615 RID: 1557
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000616 RID: 1558
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
