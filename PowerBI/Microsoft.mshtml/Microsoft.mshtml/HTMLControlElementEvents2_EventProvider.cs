using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DBF RID: 3519
	internal sealed class HTMLControlElementEvents2_EventProvider : HTMLControlElementEvents2_Event, IDisposable
	{
		// Token: 0x0601750D RID: 95501 RVA: 0x00002E80 File Offset: 0x00001E80
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				18, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x0601750E RID: 95502 RVA: 0x00002F94 File Offset: 0x00001F94
		public override void add_onmousewheel(HTMLControlElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601750F RID: 95503 RVA: 0x00003024 File Offset: 0x00002024
		public override void remove_onmousewheel(HTMLControlElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017510 RID: 95504 RVA: 0x00003114 File Offset: 0x00002114
		public override void add_onresizeend(HTMLControlElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017511 RID: 95505 RVA: 0x000031A4 File Offset: 0x000021A4
		public override void remove_onresizeend(HTMLControlElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017512 RID: 95506 RVA: 0x00003294 File Offset: 0x00002294
		public override void add_onresizestart(HTMLControlElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017513 RID: 95507 RVA: 0x00003324 File Offset: 0x00002324
		public override void remove_onresizestart(HTMLControlElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017514 RID: 95508 RVA: 0x00003414 File Offset: 0x00002414
		public override void add_onmoveend(HTMLControlElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017515 RID: 95509 RVA: 0x000034A4 File Offset: 0x000024A4
		public override void remove_onmoveend(HTMLControlElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017516 RID: 95510 RVA: 0x00003594 File Offset: 0x00002594
		public override void add_onmovestart(HTMLControlElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017517 RID: 95511 RVA: 0x00003624 File Offset: 0x00002624
		public override void remove_onmovestart(HTMLControlElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017518 RID: 95512 RVA: 0x00003714 File Offset: 0x00002714
		public override void add_oncontrolselect(HTMLControlElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017519 RID: 95513 RVA: 0x000037A4 File Offset: 0x000027A4
		public override void remove_oncontrolselect(HTMLControlElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601751A RID: 95514 RVA: 0x00003894 File Offset: 0x00002894
		public override void add_onmove(HTMLControlElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601751B RID: 95515 RVA: 0x00003924 File Offset: 0x00002924
		public override void remove_onmove(HTMLControlElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onmoveDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601751C RID: 95516 RVA: 0x00003A14 File Offset: 0x00002A14
		public override void add_onfocusout(HTMLControlElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601751D RID: 95517 RVA: 0x00003AA4 File Offset: 0x00002AA4
		public override void remove_onfocusout(HTMLControlElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601751E RID: 95518 RVA: 0x00003B94 File Offset: 0x00002B94
		public override void add_onfocusin(HTMLControlElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601751F RID: 95519 RVA: 0x00003C24 File Offset: 0x00002C24
		public override void remove_onfocusin(HTMLControlElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017520 RID: 95520 RVA: 0x00003D14 File Offset: 0x00002D14
		public override void add_onbeforeactivate(HTMLControlElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017521 RID: 95521 RVA: 0x00003DA4 File Offset: 0x00002DA4
		public override void remove_onbeforeactivate(HTMLControlElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017522 RID: 95522 RVA: 0x00003E94 File Offset: 0x00002E94
		public override void add_onbeforedeactivate(HTMLControlElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017523 RID: 95523 RVA: 0x00003F24 File Offset: 0x00002F24
		public override void remove_onbeforedeactivate(HTMLControlElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017524 RID: 95524 RVA: 0x00004014 File Offset: 0x00003014
		public override void add_ondeactivate(HTMLControlElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017525 RID: 95525 RVA: 0x000040A4 File Offset: 0x000030A4
		public override void remove_ondeactivate(HTMLControlElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017526 RID: 95526 RVA: 0x00004194 File Offset: 0x00003194
		public override void add_onactivate(HTMLControlElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017527 RID: 95527 RVA: 0x00004224 File Offset: 0x00003224
		public override void remove_onactivate(HTMLControlElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onactivateDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017528 RID: 95528 RVA: 0x00004314 File Offset: 0x00003314
		public override void add_onmouseleave(HTMLControlElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017529 RID: 95529 RVA: 0x000043A4 File Offset: 0x000033A4
		public override void remove_onmouseleave(HTMLControlElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601752A RID: 95530 RVA: 0x00004494 File Offset: 0x00003494
		public override void add_onmouseenter(HTMLControlElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601752B RID: 95531 RVA: 0x00004524 File Offset: 0x00003524
		public override void remove_onmouseenter(HTMLControlElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601752C RID: 95532 RVA: 0x00004614 File Offset: 0x00003614
		public override void add_onpage(HTMLControlElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601752D RID: 95533 RVA: 0x000046A4 File Offset: 0x000036A4
		public override void remove_onpage(HTMLControlElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onpageDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601752E RID: 95534 RVA: 0x00004794 File Offset: 0x00003794
		public override void add_onlayoutcomplete(HTMLControlElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601752F RID: 95535 RVA: 0x00004824 File Offset: 0x00003824
		public override void remove_onlayoutcomplete(HTMLControlElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017530 RID: 95536 RVA: 0x00004914 File Offset: 0x00003914
		public override void add_onreadystatechange(HTMLControlElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017531 RID: 95537 RVA: 0x000049A4 File Offset: 0x000039A4
		public override void remove_onreadystatechange(HTMLControlElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017532 RID: 95538 RVA: 0x00004A94 File Offset: 0x00003A94
		public override void add_oncellchange(HTMLControlElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017533 RID: 95539 RVA: 0x00004B24 File Offset: 0x00003B24
		public override void remove_oncellchange(HTMLControlElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017534 RID: 95540 RVA: 0x00004C14 File Offset: 0x00003C14
		public override void add_onrowsinserted(HTMLControlElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017535 RID: 95541 RVA: 0x00004CA4 File Offset: 0x00003CA4
		public override void remove_onrowsinserted(HTMLControlElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017536 RID: 95542 RVA: 0x00004D94 File Offset: 0x00003D94
		public override void add_onrowsdelete(HTMLControlElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017537 RID: 95543 RVA: 0x00004E24 File Offset: 0x00003E24
		public override void remove_onrowsdelete(HTMLControlElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017538 RID: 95544 RVA: 0x00004F14 File Offset: 0x00003F14
		public override void add_oncontextmenu(HTMLControlElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017539 RID: 95545 RVA: 0x00004FA4 File Offset: 0x00003FA4
		public override void remove_oncontextmenu(HTMLControlElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601753A RID: 95546 RVA: 0x00005094 File Offset: 0x00004094
		public override void add_onpaste(HTMLControlElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601753B RID: 95547 RVA: 0x00005124 File Offset: 0x00004124
		public override void remove_onpaste(HTMLControlElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onpasteDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601753C RID: 95548 RVA: 0x00005214 File Offset: 0x00004214
		public override void add_onbeforepaste(HTMLControlElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601753D RID: 95549 RVA: 0x000052A4 File Offset: 0x000042A4
		public override void remove_onbeforepaste(HTMLControlElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601753E RID: 95550 RVA: 0x00005394 File Offset: 0x00004394
		public override void add_oncopy(HTMLControlElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601753F RID: 95551 RVA: 0x00005424 File Offset: 0x00004424
		public override void remove_oncopy(HTMLControlElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_oncopyDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017540 RID: 95552 RVA: 0x00005514 File Offset: 0x00004514
		public override void add_onbeforecopy(HTMLControlElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017541 RID: 95553 RVA: 0x000055A4 File Offset: 0x000045A4
		public override void remove_onbeforecopy(HTMLControlElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017542 RID: 95554 RVA: 0x00005694 File Offset: 0x00004694
		public override void add_oncut(HTMLControlElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017543 RID: 95555 RVA: 0x00005724 File Offset: 0x00004724
		public override void remove_oncut(HTMLControlElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_oncutDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017544 RID: 95556 RVA: 0x00005814 File Offset: 0x00004814
		public override void add_onbeforecut(HTMLControlElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017545 RID: 95557 RVA: 0x000058A4 File Offset: 0x000048A4
		public override void remove_onbeforecut(HTMLControlElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017546 RID: 95558 RVA: 0x00005994 File Offset: 0x00004994
		public override void add_ondrop(HTMLControlElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017547 RID: 95559 RVA: 0x00005A24 File Offset: 0x00004A24
		public override void remove_ondrop(HTMLControlElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_ondropDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017548 RID: 95560 RVA: 0x00005B14 File Offset: 0x00004B14
		public override void add_ondragleave(HTMLControlElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017549 RID: 95561 RVA: 0x00005BA4 File Offset: 0x00004BA4
		public override void remove_ondragleave(HTMLControlElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601754A RID: 95562 RVA: 0x00005C94 File Offset: 0x00004C94
		public override void add_ondragover(HTMLControlElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601754B RID: 95563 RVA: 0x00005D24 File Offset: 0x00004D24
		public override void remove_ondragover(HTMLControlElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601754C RID: 95564 RVA: 0x00005E14 File Offset: 0x00004E14
		public override void add_ondragenter(HTMLControlElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601754D RID: 95565 RVA: 0x00005EA4 File Offset: 0x00004EA4
		public override void remove_ondragenter(HTMLControlElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601754E RID: 95566 RVA: 0x00005F94 File Offset: 0x00004F94
		public override void add_ondragend(HTMLControlElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601754F RID: 95567 RVA: 0x00006024 File Offset: 0x00005024
		public override void remove_ondragend(HTMLControlElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_ondragendDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017550 RID: 95568 RVA: 0x00006114 File Offset: 0x00005114
		public override void add_ondrag(HTMLControlElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017551 RID: 95569 RVA: 0x000061A4 File Offset: 0x000051A4
		public override void remove_ondrag(HTMLControlElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_ondragDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017552 RID: 95570 RVA: 0x00006294 File Offset: 0x00005294
		public override void add_onresize(HTMLControlElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017553 RID: 95571 RVA: 0x00006324 File Offset: 0x00005324
		public override void remove_onresize(HTMLControlElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onresizeDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017554 RID: 95572 RVA: 0x00006414 File Offset: 0x00005414
		public override void add_onblur(HTMLControlElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017555 RID: 95573 RVA: 0x000064A4 File Offset: 0x000054A4
		public override void remove_onblur(HTMLControlElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onblurDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017556 RID: 95574 RVA: 0x00006594 File Offset: 0x00005594
		public override void add_onfocus(HTMLControlElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017557 RID: 95575 RVA: 0x00006624 File Offset: 0x00005624
		public override void remove_onfocus(HTMLControlElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onfocusDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017558 RID: 95576 RVA: 0x00006714 File Offset: 0x00005714
		public override void add_onscroll(HTMLControlElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017559 RID: 95577 RVA: 0x000067A4 File Offset: 0x000057A4
		public override void remove_onscroll(HTMLControlElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onscrollDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601755A RID: 95578 RVA: 0x00006894 File Offset: 0x00005894
		public override void add_onpropertychange(HTMLControlElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601755B RID: 95579 RVA: 0x00006924 File Offset: 0x00005924
		public override void remove_onpropertychange(HTMLControlElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601755C RID: 95580 RVA: 0x00006A14 File Offset: 0x00005A14
		public override void add_onlosecapture(HTMLControlElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601755D RID: 95581 RVA: 0x00006AA4 File Offset: 0x00005AA4
		public override void remove_onlosecapture(HTMLControlElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601755E RID: 95582 RVA: 0x00006B94 File Offset: 0x00005B94
		public override void add_ondatasetcomplete(HTMLControlElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601755F RID: 95583 RVA: 0x00006C24 File Offset: 0x00005C24
		public override void remove_ondatasetcomplete(HTMLControlElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017560 RID: 95584 RVA: 0x00006D14 File Offset: 0x00005D14
		public override void add_ondataavailable(HTMLControlElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017561 RID: 95585 RVA: 0x00006DA4 File Offset: 0x00005DA4
		public override void remove_ondataavailable(HTMLControlElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017562 RID: 95586 RVA: 0x00006E94 File Offset: 0x00005E94
		public override void add_ondatasetchanged(HTMLControlElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017563 RID: 95587 RVA: 0x00006F24 File Offset: 0x00005F24
		public override void remove_ondatasetchanged(HTMLControlElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017564 RID: 95588 RVA: 0x00007014 File Offset: 0x00006014
		public override void add_onrowenter(HTMLControlElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017565 RID: 95589 RVA: 0x000070A4 File Offset: 0x000060A4
		public override void remove_onrowenter(HTMLControlElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017566 RID: 95590 RVA: 0x00007194 File Offset: 0x00006194
		public override void add_onrowexit(HTMLControlElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017567 RID: 95591 RVA: 0x00007224 File Offset: 0x00006224
		public override void remove_onrowexit(HTMLControlElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017568 RID: 95592 RVA: 0x00007314 File Offset: 0x00006314
		public override void add_onerrorupdate(HTMLControlElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017569 RID: 95593 RVA: 0x000073A4 File Offset: 0x000063A4
		public override void remove_onerrorupdate(HTMLControlElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601756A RID: 95594 RVA: 0x00007494 File Offset: 0x00006494
		public override void add_onafterupdate(HTMLControlElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601756B RID: 95595 RVA: 0x00007524 File Offset: 0x00006524
		public override void remove_onafterupdate(HTMLControlElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601756C RID: 95596 RVA: 0x00007614 File Offset: 0x00006614
		public override void add_onbeforeupdate(HTMLControlElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601756D RID: 95597 RVA: 0x000076A4 File Offset: 0x000066A4
		public override void remove_onbeforeupdate(HTMLControlElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601756E RID: 95598 RVA: 0x00007794 File Offset: 0x00006794
		public override void add_ondragstart(HTMLControlElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601756F RID: 95599 RVA: 0x00007824 File Offset: 0x00006824
		public override void remove_ondragstart(HTMLControlElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017570 RID: 95600 RVA: 0x00007914 File Offset: 0x00006914
		public override void add_onfilterchange(HTMLControlElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017571 RID: 95601 RVA: 0x000079A4 File Offset: 0x000069A4
		public override void remove_onfilterchange(HTMLControlElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017572 RID: 95602 RVA: 0x00007A94 File Offset: 0x00006A94
		public override void add_onselectstart(HTMLControlElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017573 RID: 95603 RVA: 0x00007B24 File Offset: 0x00006B24
		public override void remove_onselectstart(HTMLControlElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017574 RID: 95604 RVA: 0x00007C14 File Offset: 0x00006C14
		public override void add_onmouseup(HTMLControlElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017575 RID: 95605 RVA: 0x00007CA4 File Offset: 0x00006CA4
		public override void remove_onmouseup(HTMLControlElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017576 RID: 95606 RVA: 0x00007D94 File Offset: 0x00006D94
		public override void add_onmousedown(HTMLControlElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017577 RID: 95607 RVA: 0x00007E24 File Offset: 0x00006E24
		public override void remove_onmousedown(HTMLControlElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017578 RID: 95608 RVA: 0x00007F14 File Offset: 0x00006F14
		public override void add_onmousemove(HTMLControlElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017579 RID: 95609 RVA: 0x00007FA4 File Offset: 0x00006FA4
		public override void remove_onmousemove(HTMLControlElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601757A RID: 95610 RVA: 0x00008094 File Offset: 0x00007094
		public override void add_onmouseover(HTMLControlElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601757B RID: 95611 RVA: 0x00008124 File Offset: 0x00007124
		public override void remove_onmouseover(HTMLControlElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601757C RID: 95612 RVA: 0x00008214 File Offset: 0x00007214
		public override void add_onmouseout(HTMLControlElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601757D RID: 95613 RVA: 0x000082A4 File Offset: 0x000072A4
		public override void remove_onmouseout(HTMLControlElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601757E RID: 95614 RVA: 0x00008394 File Offset: 0x00007394
		public override void add_onkeyup(HTMLControlElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601757F RID: 95615 RVA: 0x00008424 File Offset: 0x00007424
		public override void remove_onkeyup(HTMLControlElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017580 RID: 95616 RVA: 0x00008514 File Offset: 0x00007514
		public override void add_onkeydown(HTMLControlElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017581 RID: 95617 RVA: 0x000085A4 File Offset: 0x000075A4
		public override void remove_onkeydown(HTMLControlElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017582 RID: 95618 RVA: 0x00008694 File Offset: 0x00007694
		public override void add_onkeypress(HTMLControlElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017583 RID: 95619 RVA: 0x00008724 File Offset: 0x00007724
		public override void remove_onkeypress(HTMLControlElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017584 RID: 95620 RVA: 0x00008814 File Offset: 0x00007814
		public override void add_ondblclick(HTMLControlElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017585 RID: 95621 RVA: 0x000088A4 File Offset: 0x000078A4
		public override void remove_ondblclick(HTMLControlElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017586 RID: 95622 RVA: 0x00008994 File Offset: 0x00007994
		public override void add_onclick(HTMLControlElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017587 RID: 95623 RVA: 0x00008A24 File Offset: 0x00007A24
		public override void remove_onclick(HTMLControlElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onclickDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017588 RID: 95624 RVA: 0x00008B14 File Offset: 0x00007B14
		public override void add_onhelp(HTMLControlElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = new HTMLControlElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlcontrolElementEvents2_SinkHelper, ref num);
				htmlcontrolElementEvents2_SinkHelper.m_dwCookie = num;
				htmlcontrolElementEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlcontrolElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017589 RID: 95625 RVA: 0x00008BA4 File Offset: 0x00007BA4
		public override void remove_onhelp(HTMLControlElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper;
					for (;;)
					{
						htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlcontrolElementEvents2_SinkHelper.m_onhelpDelegate != null && ((htmlcontrolElementEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601758A RID: 95626 RVA: 0x00008C94 File Offset: 0x00007C94
		public HTMLControlElementEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x0601758B RID: 95627 RVA: 0x00008CBC File Offset: 0x00007CBC
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
								HTMLControlElementEvents2_SinkHelper htmlcontrolElementEvents2_SinkHelper = (HTMLControlElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlcontrolElementEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x0601758C RID: 95628 RVA: 0x00008D70 File Offset: 0x00007D70
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x040004F9 RID: 1273
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x040004FA RID: 1274
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x040004FB RID: 1275
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
