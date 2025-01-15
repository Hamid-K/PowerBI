using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DE1 RID: 3553
	internal sealed class HTMLFormElementEvents_EventProvider : HTMLFormElementEvents_Event, IDisposable
	{
		// Token: 0x0601804E RID: 98382 RVA: 0x000693D0 File Offset: 0x000683D0
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				100, 243, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x0601804F RID: 98383 RVA: 0x000694E4 File Offset: 0x000684E4
		public override void add_onreset(HTMLFormElementEvents_onresetEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onresetDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018050 RID: 98384 RVA: 0x00069574 File Offset: 0x00068574
		public override void remove_onreset(HTMLFormElementEvents_onresetEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onresetDelegate != null && ((htmlformElementEvents_SinkHelper.m_onresetDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018051 RID: 98385 RVA: 0x00069664 File Offset: 0x00068664
		public override void add_onsubmit(HTMLFormElementEvents_onsubmitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onsubmitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018052 RID: 98386 RVA: 0x000696F4 File Offset: 0x000686F4
		public override void remove_onsubmit(HTMLFormElementEvents_onsubmitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onsubmitDelegate != null && ((htmlformElementEvents_SinkHelper.m_onsubmitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018053 RID: 98387 RVA: 0x000697E4 File Offset: 0x000687E4
		public override void add_onfocusout(HTMLFormElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018054 RID: 98388 RVA: 0x00069874 File Offset: 0x00068874
		public override void remove_onfocusout(HTMLFormElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmlformElementEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018055 RID: 98389 RVA: 0x00069964 File Offset: 0x00068964
		public override void add_onfocusin(HTMLFormElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018056 RID: 98390 RVA: 0x000699F4 File Offset: 0x000689F4
		public override void remove_onfocusin(HTMLFormElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onfocusinDelegate != null && ((htmlformElementEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018057 RID: 98391 RVA: 0x00069AE4 File Offset: 0x00068AE4
		public override void add_ondeactivate(HTMLFormElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018058 RID: 98392 RVA: 0x00069B74 File Offset: 0x00068B74
		public override void remove_ondeactivate(HTMLFormElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmlformElementEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018059 RID: 98393 RVA: 0x00069C64 File Offset: 0x00068C64
		public override void add_onactivate(HTMLFormElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601805A RID: 98394 RVA: 0x00069CF4 File Offset: 0x00068CF4
		public override void remove_onactivate(HTMLFormElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onactivateDelegate != null && ((htmlformElementEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601805B RID: 98395 RVA: 0x00069DE4 File Offset: 0x00068DE4
		public override void add_onmousewheel(HTMLFormElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601805C RID: 98396 RVA: 0x00069E74 File Offset: 0x00068E74
		public override void remove_onmousewheel(HTMLFormElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmlformElementEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601805D RID: 98397 RVA: 0x00069F64 File Offset: 0x00068F64
		public override void add_onmouseleave(HTMLFormElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601805E RID: 98398 RVA: 0x00069FF4 File Offset: 0x00068FF4
		public override void remove_onmouseleave(HTMLFormElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmlformElementEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601805F RID: 98399 RVA: 0x0006A0E4 File Offset: 0x000690E4
		public override void add_onmouseenter(HTMLFormElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018060 RID: 98400 RVA: 0x0006A174 File Offset: 0x00069174
		public override void remove_onmouseenter(HTMLFormElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmlformElementEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018061 RID: 98401 RVA: 0x0006A264 File Offset: 0x00069264
		public override void add_onresizeend(HTMLFormElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018062 RID: 98402 RVA: 0x0006A2F4 File Offset: 0x000692F4
		public override void remove_onresizeend(HTMLFormElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onresizeendDelegate != null && ((htmlformElementEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018063 RID: 98403 RVA: 0x0006A3E4 File Offset: 0x000693E4
		public override void add_onresizestart(HTMLFormElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018064 RID: 98404 RVA: 0x0006A474 File Offset: 0x00069474
		public override void remove_onresizestart(HTMLFormElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onresizestartDelegate != null && ((htmlformElementEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018065 RID: 98405 RVA: 0x0006A564 File Offset: 0x00069564
		public override void add_onmoveend(HTMLFormElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018066 RID: 98406 RVA: 0x0006A5F4 File Offset: 0x000695F4
		public override void remove_onmoveend(HTMLFormElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onmoveendDelegate != null && ((htmlformElementEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018067 RID: 98407 RVA: 0x0006A6E4 File Offset: 0x000696E4
		public override void add_onmovestart(HTMLFormElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018068 RID: 98408 RVA: 0x0006A774 File Offset: 0x00069774
		public override void remove_onmovestart(HTMLFormElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onmovestartDelegate != null && ((htmlformElementEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018069 RID: 98409 RVA: 0x0006A864 File Offset: 0x00069864
		public override void add_oncontrolselect(HTMLFormElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601806A RID: 98410 RVA: 0x0006A8F4 File Offset: 0x000698F4
		public override void remove_oncontrolselect(HTMLFormElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmlformElementEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601806B RID: 98411 RVA: 0x0006A9E4 File Offset: 0x000699E4
		public override void add_onmove(HTMLFormElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601806C RID: 98412 RVA: 0x0006AA74 File Offset: 0x00069A74
		public override void remove_onmove(HTMLFormElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onmoveDelegate != null && ((htmlformElementEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601806D RID: 98413 RVA: 0x0006AB64 File Offset: 0x00069B64
		public override void add_onbeforeactivate(HTMLFormElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601806E RID: 98414 RVA: 0x0006ABF4 File Offset: 0x00069BF4
		public override void remove_onbeforeactivate(HTMLFormElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlformElementEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601806F RID: 98415 RVA: 0x0006ACE4 File Offset: 0x00069CE4
		public override void add_onbeforedeactivate(HTMLFormElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018070 RID: 98416 RVA: 0x0006AD74 File Offset: 0x00069D74
		public override void remove_onbeforedeactivate(HTMLFormElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlformElementEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018071 RID: 98417 RVA: 0x0006AE64 File Offset: 0x00069E64
		public override void add_onpage(HTMLFormElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018072 RID: 98418 RVA: 0x0006AEF4 File Offset: 0x00069EF4
		public override void remove_onpage(HTMLFormElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onpageDelegate != null && ((htmlformElementEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018073 RID: 98419 RVA: 0x0006AFE4 File Offset: 0x00069FE4
		public override void add_onlayoutcomplete(HTMLFormElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018074 RID: 98420 RVA: 0x0006B074 File Offset: 0x0006A074
		public override void remove_onlayoutcomplete(HTMLFormElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlformElementEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018075 RID: 98421 RVA: 0x0006B164 File Offset: 0x0006A164
		public override void add_onbeforeeditfocus(HTMLFormElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018076 RID: 98422 RVA: 0x0006B1F4 File Offset: 0x0006A1F4
		public override void remove_onbeforeeditfocus(HTMLFormElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmlformElementEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018077 RID: 98423 RVA: 0x0006B2E4 File Offset: 0x0006A2E4
		public override void add_onreadystatechange(HTMLFormElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018078 RID: 98424 RVA: 0x0006B374 File Offset: 0x0006A374
		public override void remove_onreadystatechange(HTMLFormElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlformElementEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018079 RID: 98425 RVA: 0x0006B464 File Offset: 0x0006A464
		public override void add_oncellchange(HTMLFormElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601807A RID: 98426 RVA: 0x0006B4F4 File Offset: 0x0006A4F4
		public override void remove_oncellchange(HTMLFormElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlformElementEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601807B RID: 98427 RVA: 0x0006B5E4 File Offset: 0x0006A5E4
		public override void add_onrowsinserted(HTMLFormElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601807C RID: 98428 RVA: 0x0006B674 File Offset: 0x0006A674
		public override void remove_onrowsinserted(HTMLFormElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlformElementEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601807D RID: 98429 RVA: 0x0006B764 File Offset: 0x0006A764
		public override void add_onrowsdelete(HTMLFormElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601807E RID: 98430 RVA: 0x0006B7F4 File Offset: 0x0006A7F4
		public override void remove_onrowsdelete(HTMLFormElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlformElementEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601807F RID: 98431 RVA: 0x0006B8E4 File Offset: 0x0006A8E4
		public override void add_oncontextmenu(HTMLFormElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018080 RID: 98432 RVA: 0x0006B974 File Offset: 0x0006A974
		public override void remove_oncontextmenu(HTMLFormElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmlformElementEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018081 RID: 98433 RVA: 0x0006BA64 File Offset: 0x0006AA64
		public override void add_onpaste(HTMLFormElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018082 RID: 98434 RVA: 0x0006BAF4 File Offset: 0x0006AAF4
		public override void remove_onpaste(HTMLFormElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onpasteDelegate != null && ((htmlformElementEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018083 RID: 98435 RVA: 0x0006BBE4 File Offset: 0x0006ABE4
		public override void add_onbeforepaste(HTMLFormElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018084 RID: 98436 RVA: 0x0006BC74 File Offset: 0x0006AC74
		public override void remove_onbeforepaste(HTMLFormElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmlformElementEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018085 RID: 98437 RVA: 0x0006BD64 File Offset: 0x0006AD64
		public override void add_oncopy(HTMLFormElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018086 RID: 98438 RVA: 0x0006BDF4 File Offset: 0x0006ADF4
		public override void remove_oncopy(HTMLFormElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_oncopyDelegate != null && ((htmlformElementEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018087 RID: 98439 RVA: 0x0006BEE4 File Offset: 0x0006AEE4
		public override void add_onbeforecopy(HTMLFormElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018088 RID: 98440 RVA: 0x0006BF74 File Offset: 0x0006AF74
		public override void remove_onbeforecopy(HTMLFormElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmlformElementEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018089 RID: 98441 RVA: 0x0006C064 File Offset: 0x0006B064
		public override void add_oncut(HTMLFormElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601808A RID: 98442 RVA: 0x0006C0F4 File Offset: 0x0006B0F4
		public override void remove_oncut(HTMLFormElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_oncutDelegate != null && ((htmlformElementEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601808B RID: 98443 RVA: 0x0006C1E4 File Offset: 0x0006B1E4
		public override void add_onbeforecut(HTMLFormElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601808C RID: 98444 RVA: 0x0006C274 File Offset: 0x0006B274
		public override void remove_onbeforecut(HTMLFormElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmlformElementEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601808D RID: 98445 RVA: 0x0006C364 File Offset: 0x0006B364
		public override void add_ondrop(HTMLFormElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601808E RID: 98446 RVA: 0x0006C3F4 File Offset: 0x0006B3F4
		public override void remove_ondrop(HTMLFormElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_ondropDelegate != null && ((htmlformElementEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601808F RID: 98447 RVA: 0x0006C4E4 File Offset: 0x0006B4E4
		public override void add_ondragleave(HTMLFormElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018090 RID: 98448 RVA: 0x0006C574 File Offset: 0x0006B574
		public override void remove_ondragleave(HTMLFormElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmlformElementEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018091 RID: 98449 RVA: 0x0006C664 File Offset: 0x0006B664
		public override void add_ondragover(HTMLFormElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018092 RID: 98450 RVA: 0x0006C6F4 File Offset: 0x0006B6F4
		public override void remove_ondragover(HTMLFormElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_ondragoverDelegate != null && ((htmlformElementEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018093 RID: 98451 RVA: 0x0006C7E4 File Offset: 0x0006B7E4
		public override void add_ondragenter(HTMLFormElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018094 RID: 98452 RVA: 0x0006C874 File Offset: 0x0006B874
		public override void remove_ondragenter(HTMLFormElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_ondragenterDelegate != null && ((htmlformElementEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018095 RID: 98453 RVA: 0x0006C964 File Offset: 0x0006B964
		public override void add_ondragend(HTMLFormElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018096 RID: 98454 RVA: 0x0006C9F4 File Offset: 0x0006B9F4
		public override void remove_ondragend(HTMLFormElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_ondragendDelegate != null && ((htmlformElementEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018097 RID: 98455 RVA: 0x0006CAE4 File Offset: 0x0006BAE4
		public override void add_ondrag(HTMLFormElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018098 RID: 98456 RVA: 0x0006CB74 File Offset: 0x0006BB74
		public override void remove_ondrag(HTMLFormElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_ondragDelegate != null && ((htmlformElementEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018099 RID: 98457 RVA: 0x0006CC64 File Offset: 0x0006BC64
		public override void add_onresize(HTMLFormElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601809A RID: 98458 RVA: 0x0006CCF4 File Offset: 0x0006BCF4
		public override void remove_onresize(HTMLFormElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onresizeDelegate != null && ((htmlformElementEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601809B RID: 98459 RVA: 0x0006CDE4 File Offset: 0x0006BDE4
		public override void add_onblur(HTMLFormElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601809C RID: 98460 RVA: 0x0006CE74 File Offset: 0x0006BE74
		public override void remove_onblur(HTMLFormElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onblurDelegate != null && ((htmlformElementEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601809D RID: 98461 RVA: 0x0006CF64 File Offset: 0x0006BF64
		public override void add_onfocus(HTMLFormElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601809E RID: 98462 RVA: 0x0006CFF4 File Offset: 0x0006BFF4
		public override void remove_onfocus(HTMLFormElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onfocusDelegate != null && ((htmlformElementEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601809F RID: 98463 RVA: 0x0006D0E4 File Offset: 0x0006C0E4
		public override void add_onscroll(HTMLFormElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180A0 RID: 98464 RVA: 0x0006D174 File Offset: 0x0006C174
		public override void remove_onscroll(HTMLFormElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onscrollDelegate != null && ((htmlformElementEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180A1 RID: 98465 RVA: 0x0006D264 File Offset: 0x0006C264
		public override void add_onpropertychange(HTMLFormElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180A2 RID: 98466 RVA: 0x0006D2F4 File Offset: 0x0006C2F4
		public override void remove_onpropertychange(HTMLFormElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmlformElementEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180A3 RID: 98467 RVA: 0x0006D3E4 File Offset: 0x0006C3E4
		public override void add_onlosecapture(HTMLFormElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180A4 RID: 98468 RVA: 0x0006D474 File Offset: 0x0006C474
		public override void remove_onlosecapture(HTMLFormElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmlformElementEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180A5 RID: 98469 RVA: 0x0006D564 File Offset: 0x0006C564
		public override void add_ondatasetcomplete(HTMLFormElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180A6 RID: 98470 RVA: 0x0006D5F4 File Offset: 0x0006C5F4
		public override void remove_ondatasetcomplete(HTMLFormElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlformElementEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180A7 RID: 98471 RVA: 0x0006D6E4 File Offset: 0x0006C6E4
		public override void add_ondataavailable(HTMLFormElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180A8 RID: 98472 RVA: 0x0006D774 File Offset: 0x0006C774
		public override void remove_ondataavailable(HTMLFormElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlformElementEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180A9 RID: 98473 RVA: 0x0006D864 File Offset: 0x0006C864
		public override void add_ondatasetchanged(HTMLFormElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180AA RID: 98474 RVA: 0x0006D8F4 File Offset: 0x0006C8F4
		public override void remove_ondatasetchanged(HTMLFormElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlformElementEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180AB RID: 98475 RVA: 0x0006D9E4 File Offset: 0x0006C9E4
		public override void add_onrowenter(HTMLFormElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180AC RID: 98476 RVA: 0x0006DA74 File Offset: 0x0006CA74
		public override void remove_onrowenter(HTMLFormElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlformElementEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180AD RID: 98477 RVA: 0x0006DB64 File Offset: 0x0006CB64
		public override void add_onrowexit(HTMLFormElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180AE RID: 98478 RVA: 0x0006DBF4 File Offset: 0x0006CBF4
		public override void remove_onrowexit(HTMLFormElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlformElementEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180AF RID: 98479 RVA: 0x0006DCE4 File Offset: 0x0006CCE4
		public override void add_onerrorupdate(HTMLFormElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180B0 RID: 98480 RVA: 0x0006DD74 File Offset: 0x0006CD74
		public override void remove_onerrorupdate(HTMLFormElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlformElementEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180B1 RID: 98481 RVA: 0x0006DE64 File Offset: 0x0006CE64
		public override void add_onafterupdate(HTMLFormElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180B2 RID: 98482 RVA: 0x0006DEF4 File Offset: 0x0006CEF4
		public override void remove_onafterupdate(HTMLFormElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlformElementEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180B3 RID: 98483 RVA: 0x0006DFE4 File Offset: 0x0006CFE4
		public override void add_onbeforeupdate(HTMLFormElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180B4 RID: 98484 RVA: 0x0006E074 File Offset: 0x0006D074
		public override void remove_onbeforeupdate(HTMLFormElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlformElementEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180B5 RID: 98485 RVA: 0x0006E164 File Offset: 0x0006D164
		public override void add_ondragstart(HTMLFormElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180B6 RID: 98486 RVA: 0x0006E1F4 File Offset: 0x0006D1F4
		public override void remove_ondragstart(HTMLFormElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_ondragstartDelegate != null && ((htmlformElementEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180B7 RID: 98487 RVA: 0x0006E2E4 File Offset: 0x0006D2E4
		public override void add_onfilterchange(HTMLFormElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180B8 RID: 98488 RVA: 0x0006E374 File Offset: 0x0006D374
		public override void remove_onfilterchange(HTMLFormElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmlformElementEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180B9 RID: 98489 RVA: 0x0006E464 File Offset: 0x0006D464
		public override void add_onselectstart(HTMLFormElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180BA RID: 98490 RVA: 0x0006E4F4 File Offset: 0x0006D4F4
		public override void remove_onselectstart(HTMLFormElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onselectstartDelegate != null && ((htmlformElementEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180BB RID: 98491 RVA: 0x0006E5E4 File Offset: 0x0006D5E4
		public override void add_onmouseup(HTMLFormElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180BC RID: 98492 RVA: 0x0006E674 File Offset: 0x0006D674
		public override void remove_onmouseup(HTMLFormElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onmouseupDelegate != null && ((htmlformElementEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180BD RID: 98493 RVA: 0x0006E764 File Offset: 0x0006D764
		public override void add_onmousedown(HTMLFormElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180BE RID: 98494 RVA: 0x0006E7F4 File Offset: 0x0006D7F4
		public override void remove_onmousedown(HTMLFormElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onmousedownDelegate != null && ((htmlformElementEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180BF RID: 98495 RVA: 0x0006E8E4 File Offset: 0x0006D8E4
		public override void add_onmousemove(HTMLFormElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180C0 RID: 98496 RVA: 0x0006E974 File Offset: 0x0006D974
		public override void remove_onmousemove(HTMLFormElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmlformElementEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180C1 RID: 98497 RVA: 0x0006EA64 File Offset: 0x0006DA64
		public override void add_onmouseover(HTMLFormElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180C2 RID: 98498 RVA: 0x0006EAF4 File Offset: 0x0006DAF4
		public override void remove_onmouseover(HTMLFormElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmlformElementEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180C3 RID: 98499 RVA: 0x0006EBE4 File Offset: 0x0006DBE4
		public override void add_onmouseout(HTMLFormElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180C4 RID: 98500 RVA: 0x0006EC74 File Offset: 0x0006DC74
		public override void remove_onmouseout(HTMLFormElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmlformElementEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180C5 RID: 98501 RVA: 0x0006ED64 File Offset: 0x0006DD64
		public override void add_onkeyup(HTMLFormElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180C6 RID: 98502 RVA: 0x0006EDF4 File Offset: 0x0006DDF4
		public override void remove_onkeyup(HTMLFormElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onkeyupDelegate != null && ((htmlformElementEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180C7 RID: 98503 RVA: 0x0006EEE4 File Offset: 0x0006DEE4
		public override void add_onkeydown(HTMLFormElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180C8 RID: 98504 RVA: 0x0006EF74 File Offset: 0x0006DF74
		public override void remove_onkeydown(HTMLFormElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onkeydownDelegate != null && ((htmlformElementEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180C9 RID: 98505 RVA: 0x0006F064 File Offset: 0x0006E064
		public override void add_onkeypress(HTMLFormElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180CA RID: 98506 RVA: 0x0006F0F4 File Offset: 0x0006E0F4
		public override void remove_onkeypress(HTMLFormElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onkeypressDelegate != null && ((htmlformElementEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180CB RID: 98507 RVA: 0x0006F1E4 File Offset: 0x0006E1E4
		public override void add_ondblclick(HTMLFormElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180CC RID: 98508 RVA: 0x0006F274 File Offset: 0x0006E274
		public override void remove_ondblclick(HTMLFormElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_ondblclickDelegate != null && ((htmlformElementEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180CD RID: 98509 RVA: 0x0006F364 File Offset: 0x0006E364
		public override void add_onclick(HTMLFormElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180CE RID: 98510 RVA: 0x0006F3F4 File Offset: 0x0006E3F4
		public override void remove_onclick(HTMLFormElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onclickDelegate != null && ((htmlformElementEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180CF RID: 98511 RVA: 0x0006F4E4 File Offset: 0x0006E4E4
		public override void add_onhelp(HTMLFormElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = new HTMLFormElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlformElementEvents_SinkHelper, ref num);
				htmlformElementEvents_SinkHelper.m_dwCookie = num;
				htmlformElementEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlformElementEvents_SinkHelper);
			}
		}

		// Token: 0x060180D0 RID: 98512 RVA: 0x0006F574 File Offset: 0x0006E574
		public override void remove_onhelp(HTMLFormElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper;
					for (;;)
					{
						htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlformElementEvents_SinkHelper.m_onhelpDelegate != null && ((htmlformElementEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060180D1 RID: 98513 RVA: 0x0006F664 File Offset: 0x0006E664
		public HTMLFormElementEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x060180D2 RID: 98514 RVA: 0x0006F68C File Offset: 0x0006E68C
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
								HTMLFormElementEvents_SinkHelper htmlformElementEvents_SinkHelper = (HTMLFormElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlformElementEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x060180D3 RID: 98515 RVA: 0x0006F740 File Offset: 0x0006E740
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x040008E3 RID: 2275
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x040008E4 RID: 2276
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x040008E5 RID: 2277
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
