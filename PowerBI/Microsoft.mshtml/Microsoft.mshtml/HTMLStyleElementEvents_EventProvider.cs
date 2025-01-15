using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E17 RID: 3607
	internal sealed class HTMLStyleElementEvents_EventProvider : HTMLStyleElementEvents_Event, IDisposable
	{
		// Token: 0x06019371 RID: 103281 RVA: 0x00117808 File Offset: 0x00116808
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				203, 243, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06019372 RID: 103282 RVA: 0x0011791C File Offset: 0x0011691C
		public override void add_onerror(HTMLStyleElementEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019373 RID: 103283 RVA: 0x001179AC File Offset: 0x001169AC
		public override void remove_onerror(HTMLStyleElementEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onerrorDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019374 RID: 103284 RVA: 0x00117A9C File Offset: 0x00116A9C
		public override void add_onload(HTMLStyleElementEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019375 RID: 103285 RVA: 0x00117B2C File Offset: 0x00116B2C
		public override void remove_onload(HTMLStyleElementEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onloadDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019376 RID: 103286 RVA: 0x00117C1C File Offset: 0x00116C1C
		public override void add_onfocusout(HTMLStyleElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019377 RID: 103287 RVA: 0x00117CAC File Offset: 0x00116CAC
		public override void remove_onfocusout(HTMLStyleElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019378 RID: 103288 RVA: 0x00117D9C File Offset: 0x00116D9C
		public override void add_onfocusin(HTMLStyleElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019379 RID: 103289 RVA: 0x00117E2C File Offset: 0x00116E2C
		public override void remove_onfocusin(HTMLStyleElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onfocusinDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601937A RID: 103290 RVA: 0x00117F1C File Offset: 0x00116F1C
		public override void add_ondeactivate(HTMLStyleElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601937B RID: 103291 RVA: 0x00117FAC File Offset: 0x00116FAC
		public override void remove_ondeactivate(HTMLStyleElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601937C RID: 103292 RVA: 0x0011809C File Offset: 0x0011709C
		public override void add_onactivate(HTMLStyleElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601937D RID: 103293 RVA: 0x0011812C File Offset: 0x0011712C
		public override void remove_onactivate(HTMLStyleElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onactivateDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601937E RID: 103294 RVA: 0x0011821C File Offset: 0x0011721C
		public override void add_onmousewheel(HTMLStyleElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601937F RID: 103295 RVA: 0x001182AC File Offset: 0x001172AC
		public override void remove_onmousewheel(HTMLStyleElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019380 RID: 103296 RVA: 0x0011839C File Offset: 0x0011739C
		public override void add_onmouseleave(HTMLStyleElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019381 RID: 103297 RVA: 0x0011842C File Offset: 0x0011742C
		public override void remove_onmouseleave(HTMLStyleElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019382 RID: 103298 RVA: 0x0011851C File Offset: 0x0011751C
		public override void add_onmouseenter(HTMLStyleElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019383 RID: 103299 RVA: 0x001185AC File Offset: 0x001175AC
		public override void remove_onmouseenter(HTMLStyleElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019384 RID: 103300 RVA: 0x0011869C File Offset: 0x0011769C
		public override void add_onresizeend(HTMLStyleElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019385 RID: 103301 RVA: 0x0011872C File Offset: 0x0011772C
		public override void remove_onresizeend(HTMLStyleElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onresizeendDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019386 RID: 103302 RVA: 0x0011881C File Offset: 0x0011781C
		public override void add_onresizestart(HTMLStyleElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019387 RID: 103303 RVA: 0x001188AC File Offset: 0x001178AC
		public override void remove_onresizestart(HTMLStyleElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onresizestartDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019388 RID: 103304 RVA: 0x0011899C File Offset: 0x0011799C
		public override void add_onmoveend(HTMLStyleElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019389 RID: 103305 RVA: 0x00118A2C File Offset: 0x00117A2C
		public override void remove_onmoveend(HTMLStyleElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onmoveendDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601938A RID: 103306 RVA: 0x00118B1C File Offset: 0x00117B1C
		public override void add_onmovestart(HTMLStyleElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601938B RID: 103307 RVA: 0x00118BAC File Offset: 0x00117BAC
		public override void remove_onmovestart(HTMLStyleElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onmovestartDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601938C RID: 103308 RVA: 0x00118C9C File Offset: 0x00117C9C
		public override void add_oncontrolselect(HTMLStyleElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601938D RID: 103309 RVA: 0x00118D2C File Offset: 0x00117D2C
		public override void remove_oncontrolselect(HTMLStyleElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601938E RID: 103310 RVA: 0x00118E1C File Offset: 0x00117E1C
		public override void add_onmove(HTMLStyleElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601938F RID: 103311 RVA: 0x00118EAC File Offset: 0x00117EAC
		public override void remove_onmove(HTMLStyleElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onmoveDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019390 RID: 103312 RVA: 0x00118F9C File Offset: 0x00117F9C
		public override void add_onbeforeactivate(HTMLStyleElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019391 RID: 103313 RVA: 0x0011902C File Offset: 0x0011802C
		public override void remove_onbeforeactivate(HTMLStyleElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019392 RID: 103314 RVA: 0x0011911C File Offset: 0x0011811C
		public override void add_onbeforedeactivate(HTMLStyleElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019393 RID: 103315 RVA: 0x001191AC File Offset: 0x001181AC
		public override void remove_onbeforedeactivate(HTMLStyleElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019394 RID: 103316 RVA: 0x0011929C File Offset: 0x0011829C
		public override void add_onpage(HTMLStyleElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019395 RID: 103317 RVA: 0x0011932C File Offset: 0x0011832C
		public override void remove_onpage(HTMLStyleElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onpageDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019396 RID: 103318 RVA: 0x0011941C File Offset: 0x0011841C
		public override void add_onlayoutcomplete(HTMLStyleElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019397 RID: 103319 RVA: 0x001194AC File Offset: 0x001184AC
		public override void remove_onlayoutcomplete(HTMLStyleElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019398 RID: 103320 RVA: 0x0011959C File Offset: 0x0011859C
		public override void add_onbeforeeditfocus(HTMLStyleElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019399 RID: 103321 RVA: 0x0011962C File Offset: 0x0011862C
		public override void remove_onbeforeeditfocus(HTMLStyleElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601939A RID: 103322 RVA: 0x0011971C File Offset: 0x0011871C
		public override void add_onreadystatechange(HTMLStyleElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601939B RID: 103323 RVA: 0x001197AC File Offset: 0x001187AC
		public override void remove_onreadystatechange(HTMLStyleElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601939C RID: 103324 RVA: 0x0011989C File Offset: 0x0011889C
		public override void add_oncellchange(HTMLStyleElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601939D RID: 103325 RVA: 0x0011992C File Offset: 0x0011892C
		public override void remove_oncellchange(HTMLStyleElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601939E RID: 103326 RVA: 0x00119A1C File Offset: 0x00118A1C
		public override void add_onrowsinserted(HTMLStyleElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601939F RID: 103327 RVA: 0x00119AAC File Offset: 0x00118AAC
		public override void remove_onrowsinserted(HTMLStyleElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193A0 RID: 103328 RVA: 0x00119B9C File Offset: 0x00118B9C
		public override void add_onrowsdelete(HTMLStyleElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193A1 RID: 103329 RVA: 0x00119C2C File Offset: 0x00118C2C
		public override void remove_onrowsdelete(HTMLStyleElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193A2 RID: 103330 RVA: 0x00119D1C File Offset: 0x00118D1C
		public override void add_oncontextmenu(HTMLStyleElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193A3 RID: 103331 RVA: 0x00119DAC File Offset: 0x00118DAC
		public override void remove_oncontextmenu(HTMLStyleElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193A4 RID: 103332 RVA: 0x00119E9C File Offset: 0x00118E9C
		public override void add_onpaste(HTMLStyleElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193A5 RID: 103333 RVA: 0x00119F2C File Offset: 0x00118F2C
		public override void remove_onpaste(HTMLStyleElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onpasteDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193A6 RID: 103334 RVA: 0x0011A01C File Offset: 0x0011901C
		public override void add_onbeforepaste(HTMLStyleElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193A7 RID: 103335 RVA: 0x0011A0AC File Offset: 0x001190AC
		public override void remove_onbeforepaste(HTMLStyleElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193A8 RID: 103336 RVA: 0x0011A19C File Offset: 0x0011919C
		public override void add_oncopy(HTMLStyleElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193A9 RID: 103337 RVA: 0x0011A22C File Offset: 0x0011922C
		public override void remove_oncopy(HTMLStyleElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_oncopyDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193AA RID: 103338 RVA: 0x0011A31C File Offset: 0x0011931C
		public override void add_onbeforecopy(HTMLStyleElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193AB RID: 103339 RVA: 0x0011A3AC File Offset: 0x001193AC
		public override void remove_onbeforecopy(HTMLStyleElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193AC RID: 103340 RVA: 0x0011A49C File Offset: 0x0011949C
		public override void add_oncut(HTMLStyleElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193AD RID: 103341 RVA: 0x0011A52C File Offset: 0x0011952C
		public override void remove_oncut(HTMLStyleElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_oncutDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193AE RID: 103342 RVA: 0x0011A61C File Offset: 0x0011961C
		public override void add_onbeforecut(HTMLStyleElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193AF RID: 103343 RVA: 0x0011A6AC File Offset: 0x001196AC
		public override void remove_onbeforecut(HTMLStyleElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193B0 RID: 103344 RVA: 0x0011A79C File Offset: 0x0011979C
		public override void add_ondrop(HTMLStyleElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193B1 RID: 103345 RVA: 0x0011A82C File Offset: 0x0011982C
		public override void remove_ondrop(HTMLStyleElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_ondropDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193B2 RID: 103346 RVA: 0x0011A91C File Offset: 0x0011991C
		public override void add_ondragleave(HTMLStyleElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193B3 RID: 103347 RVA: 0x0011A9AC File Offset: 0x001199AC
		public override void remove_ondragleave(HTMLStyleElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193B4 RID: 103348 RVA: 0x0011AA9C File Offset: 0x00119A9C
		public override void add_ondragover(HTMLStyleElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193B5 RID: 103349 RVA: 0x0011AB2C File Offset: 0x00119B2C
		public override void remove_ondragover(HTMLStyleElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_ondragoverDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193B6 RID: 103350 RVA: 0x0011AC1C File Offset: 0x00119C1C
		public override void add_ondragenter(HTMLStyleElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193B7 RID: 103351 RVA: 0x0011ACAC File Offset: 0x00119CAC
		public override void remove_ondragenter(HTMLStyleElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_ondragenterDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193B8 RID: 103352 RVA: 0x0011AD9C File Offset: 0x00119D9C
		public override void add_ondragend(HTMLStyleElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193B9 RID: 103353 RVA: 0x0011AE2C File Offset: 0x00119E2C
		public override void remove_ondragend(HTMLStyleElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_ondragendDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193BA RID: 103354 RVA: 0x0011AF1C File Offset: 0x00119F1C
		public override void add_ondrag(HTMLStyleElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193BB RID: 103355 RVA: 0x0011AFAC File Offset: 0x00119FAC
		public override void remove_ondrag(HTMLStyleElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_ondragDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193BC RID: 103356 RVA: 0x0011B09C File Offset: 0x0011A09C
		public override void add_onresize(HTMLStyleElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193BD RID: 103357 RVA: 0x0011B12C File Offset: 0x0011A12C
		public override void remove_onresize(HTMLStyleElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onresizeDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193BE RID: 103358 RVA: 0x0011B21C File Offset: 0x0011A21C
		public override void add_onblur(HTMLStyleElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193BF RID: 103359 RVA: 0x0011B2AC File Offset: 0x0011A2AC
		public override void remove_onblur(HTMLStyleElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onblurDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193C0 RID: 103360 RVA: 0x0011B39C File Offset: 0x0011A39C
		public override void add_onfocus(HTMLStyleElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193C1 RID: 103361 RVA: 0x0011B42C File Offset: 0x0011A42C
		public override void remove_onfocus(HTMLStyleElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onfocusDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193C2 RID: 103362 RVA: 0x0011B51C File Offset: 0x0011A51C
		public override void add_onscroll(HTMLStyleElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193C3 RID: 103363 RVA: 0x0011B5AC File Offset: 0x0011A5AC
		public override void remove_onscroll(HTMLStyleElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onscrollDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193C4 RID: 103364 RVA: 0x0011B69C File Offset: 0x0011A69C
		public override void add_onpropertychange(HTMLStyleElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193C5 RID: 103365 RVA: 0x0011B72C File Offset: 0x0011A72C
		public override void remove_onpropertychange(HTMLStyleElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193C6 RID: 103366 RVA: 0x0011B81C File Offset: 0x0011A81C
		public override void add_onlosecapture(HTMLStyleElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193C7 RID: 103367 RVA: 0x0011B8AC File Offset: 0x0011A8AC
		public override void remove_onlosecapture(HTMLStyleElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193C8 RID: 103368 RVA: 0x0011B99C File Offset: 0x0011A99C
		public override void add_ondatasetcomplete(HTMLStyleElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193C9 RID: 103369 RVA: 0x0011BA2C File Offset: 0x0011AA2C
		public override void remove_ondatasetcomplete(HTMLStyleElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193CA RID: 103370 RVA: 0x0011BB1C File Offset: 0x0011AB1C
		public override void add_ondataavailable(HTMLStyleElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193CB RID: 103371 RVA: 0x0011BBAC File Offset: 0x0011ABAC
		public override void remove_ondataavailable(HTMLStyleElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193CC RID: 103372 RVA: 0x0011BC9C File Offset: 0x0011AC9C
		public override void add_ondatasetchanged(HTMLStyleElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193CD RID: 103373 RVA: 0x0011BD2C File Offset: 0x0011AD2C
		public override void remove_ondatasetchanged(HTMLStyleElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193CE RID: 103374 RVA: 0x0011BE1C File Offset: 0x0011AE1C
		public override void add_onrowenter(HTMLStyleElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193CF RID: 103375 RVA: 0x0011BEAC File Offset: 0x0011AEAC
		public override void remove_onrowenter(HTMLStyleElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193D0 RID: 103376 RVA: 0x0011BF9C File Offset: 0x0011AF9C
		public override void add_onrowexit(HTMLStyleElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193D1 RID: 103377 RVA: 0x0011C02C File Offset: 0x0011B02C
		public override void remove_onrowexit(HTMLStyleElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193D2 RID: 103378 RVA: 0x0011C11C File Offset: 0x0011B11C
		public override void add_onerrorupdate(HTMLStyleElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193D3 RID: 103379 RVA: 0x0011C1AC File Offset: 0x0011B1AC
		public override void remove_onerrorupdate(HTMLStyleElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193D4 RID: 103380 RVA: 0x0011C29C File Offset: 0x0011B29C
		public override void add_onafterupdate(HTMLStyleElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193D5 RID: 103381 RVA: 0x0011C32C File Offset: 0x0011B32C
		public override void remove_onafterupdate(HTMLStyleElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193D6 RID: 103382 RVA: 0x0011C41C File Offset: 0x0011B41C
		public override void add_onbeforeupdate(HTMLStyleElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193D7 RID: 103383 RVA: 0x0011C4AC File Offset: 0x0011B4AC
		public override void remove_onbeforeupdate(HTMLStyleElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193D8 RID: 103384 RVA: 0x0011C59C File Offset: 0x0011B59C
		public override void add_ondragstart(HTMLStyleElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193D9 RID: 103385 RVA: 0x0011C62C File Offset: 0x0011B62C
		public override void remove_ondragstart(HTMLStyleElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_ondragstartDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193DA RID: 103386 RVA: 0x0011C71C File Offset: 0x0011B71C
		public override void add_onfilterchange(HTMLStyleElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193DB RID: 103387 RVA: 0x0011C7AC File Offset: 0x0011B7AC
		public override void remove_onfilterchange(HTMLStyleElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193DC RID: 103388 RVA: 0x0011C89C File Offset: 0x0011B89C
		public override void add_onselectstart(HTMLStyleElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193DD RID: 103389 RVA: 0x0011C92C File Offset: 0x0011B92C
		public override void remove_onselectstart(HTMLStyleElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onselectstartDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193DE RID: 103390 RVA: 0x0011CA1C File Offset: 0x0011BA1C
		public override void add_onmouseup(HTMLStyleElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193DF RID: 103391 RVA: 0x0011CAAC File Offset: 0x0011BAAC
		public override void remove_onmouseup(HTMLStyleElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onmouseupDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193E0 RID: 103392 RVA: 0x0011CB9C File Offset: 0x0011BB9C
		public override void add_onmousedown(HTMLStyleElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193E1 RID: 103393 RVA: 0x0011CC2C File Offset: 0x0011BC2C
		public override void remove_onmousedown(HTMLStyleElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onmousedownDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193E2 RID: 103394 RVA: 0x0011CD1C File Offset: 0x0011BD1C
		public override void add_onmousemove(HTMLStyleElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193E3 RID: 103395 RVA: 0x0011CDAC File Offset: 0x0011BDAC
		public override void remove_onmousemove(HTMLStyleElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193E4 RID: 103396 RVA: 0x0011CE9C File Offset: 0x0011BE9C
		public override void add_onmouseover(HTMLStyleElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193E5 RID: 103397 RVA: 0x0011CF2C File Offset: 0x0011BF2C
		public override void remove_onmouseover(HTMLStyleElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193E6 RID: 103398 RVA: 0x0011D01C File Offset: 0x0011C01C
		public override void add_onmouseout(HTMLStyleElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193E7 RID: 103399 RVA: 0x0011D0AC File Offset: 0x0011C0AC
		public override void remove_onmouseout(HTMLStyleElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193E8 RID: 103400 RVA: 0x0011D19C File Offset: 0x0011C19C
		public override void add_onkeyup(HTMLStyleElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193E9 RID: 103401 RVA: 0x0011D22C File Offset: 0x0011C22C
		public override void remove_onkeyup(HTMLStyleElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onkeyupDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193EA RID: 103402 RVA: 0x0011D31C File Offset: 0x0011C31C
		public override void add_onkeydown(HTMLStyleElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193EB RID: 103403 RVA: 0x0011D3AC File Offset: 0x0011C3AC
		public override void remove_onkeydown(HTMLStyleElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onkeydownDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193EC RID: 103404 RVA: 0x0011D49C File Offset: 0x0011C49C
		public override void add_onkeypress(HTMLStyleElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193ED RID: 103405 RVA: 0x0011D52C File Offset: 0x0011C52C
		public override void remove_onkeypress(HTMLStyleElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onkeypressDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193EE RID: 103406 RVA: 0x0011D61C File Offset: 0x0011C61C
		public override void add_ondblclick(HTMLStyleElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193EF RID: 103407 RVA: 0x0011D6AC File Offset: 0x0011C6AC
		public override void remove_ondblclick(HTMLStyleElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_ondblclickDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193F0 RID: 103408 RVA: 0x0011D79C File Offset: 0x0011C79C
		public override void add_onclick(HTMLStyleElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193F1 RID: 103409 RVA: 0x0011D82C File Offset: 0x0011C82C
		public override void remove_onclick(HTMLStyleElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onclickDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193F2 RID: 103410 RVA: 0x0011D91C File Offset: 0x0011C91C
		public override void add_onhelp(HTMLStyleElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = new HTMLStyleElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents_SinkHelper, ref num);
				htmlstyleElementEvents_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents_SinkHelper);
			}
		}

		// Token: 0x060193F3 RID: 103411 RVA: 0x0011D9AC File Offset: 0x0011C9AC
		public override void remove_onhelp(HTMLStyleElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents_SinkHelper.m_onhelpDelegate != null && ((htmlstyleElementEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060193F4 RID: 103412 RVA: 0x0011DA9C File Offset: 0x0011CA9C
		public HTMLStyleElementEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x060193F5 RID: 103413 RVA: 0x0011DAC4 File Offset: 0x0011CAC4
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
								HTMLStyleElementEvents_SinkHelper htmlstyleElementEvents_SinkHelper = (HTMLStyleElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x060193F6 RID: 103414 RVA: 0x0011DB78 File Offset: 0x0011CB78
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000F83 RID: 3971
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000F84 RID: 3972
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000F85 RID: 3973
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
