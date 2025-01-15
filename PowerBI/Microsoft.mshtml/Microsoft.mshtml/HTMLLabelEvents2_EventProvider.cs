using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DDF RID: 3551
	internal sealed class HTMLLabelEvents2_EventProvider : HTMLLabelEvents2_Event, IDisposable
	{
		// Token: 0x06017F8C RID: 98188 RVA: 0x000626E4 File Offset: 0x000616E4
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				28, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06017F8D RID: 98189 RVA: 0x000627F8 File Offset: 0x000617F8
		public override void add_onmousewheel(HTMLLabelEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F8E RID: 98190 RVA: 0x00062888 File Offset: 0x00061888
		public override void remove_onmousewheel(HTMLLabelEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmllabelEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F8F RID: 98191 RVA: 0x00062978 File Offset: 0x00061978
		public override void add_onresizeend(HTMLLabelEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F90 RID: 98192 RVA: 0x00062A08 File Offset: 0x00061A08
		public override void remove_onresizeend(HTMLLabelEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmllabelEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F91 RID: 98193 RVA: 0x00062AF8 File Offset: 0x00061AF8
		public override void add_onresizestart(HTMLLabelEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F92 RID: 98194 RVA: 0x00062B88 File Offset: 0x00061B88
		public override void remove_onresizestart(HTMLLabelEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmllabelEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F93 RID: 98195 RVA: 0x00062C78 File Offset: 0x00061C78
		public override void add_onmoveend(HTMLLabelEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F94 RID: 98196 RVA: 0x00062D08 File Offset: 0x00061D08
		public override void remove_onmoveend(HTMLLabelEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmllabelEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F95 RID: 98197 RVA: 0x00062DF8 File Offset: 0x00061DF8
		public override void add_onmovestart(HTMLLabelEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F96 RID: 98198 RVA: 0x00062E88 File Offset: 0x00061E88
		public override void remove_onmovestart(HTMLLabelEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmllabelEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F97 RID: 98199 RVA: 0x00062F78 File Offset: 0x00061F78
		public override void add_oncontrolselect(HTMLLabelEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F98 RID: 98200 RVA: 0x00063008 File Offset: 0x00062008
		public override void remove_oncontrolselect(HTMLLabelEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmllabelEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F99 RID: 98201 RVA: 0x000630F8 File Offset: 0x000620F8
		public override void add_onmove(HTMLLabelEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F9A RID: 98202 RVA: 0x00063188 File Offset: 0x00062188
		public override void remove_onmove(HTMLLabelEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onmoveDelegate != null && ((htmllabelEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F9B RID: 98203 RVA: 0x00063278 File Offset: 0x00062278
		public override void add_onfocusout(HTMLLabelEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F9C RID: 98204 RVA: 0x00063308 File Offset: 0x00062308
		public override void remove_onfocusout(HTMLLabelEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmllabelEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F9D RID: 98205 RVA: 0x000633F8 File Offset: 0x000623F8
		public override void add_onfocusin(HTMLLabelEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017F9E RID: 98206 RVA: 0x00063488 File Offset: 0x00062488
		public override void remove_onfocusin(HTMLLabelEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmllabelEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017F9F RID: 98207 RVA: 0x00063578 File Offset: 0x00062578
		public override void add_onbeforeactivate(HTMLLabelEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FA0 RID: 98208 RVA: 0x00063608 File Offset: 0x00062608
		public override void remove_onbeforeactivate(HTMLLabelEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmllabelEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FA1 RID: 98209 RVA: 0x000636F8 File Offset: 0x000626F8
		public override void add_onbeforedeactivate(HTMLLabelEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FA2 RID: 98210 RVA: 0x00063788 File Offset: 0x00062788
		public override void remove_onbeforedeactivate(HTMLLabelEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmllabelEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FA3 RID: 98211 RVA: 0x00063878 File Offset: 0x00062878
		public override void add_ondeactivate(HTMLLabelEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FA4 RID: 98212 RVA: 0x00063908 File Offset: 0x00062908
		public override void remove_ondeactivate(HTMLLabelEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmllabelEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FA5 RID: 98213 RVA: 0x000639F8 File Offset: 0x000629F8
		public override void add_onactivate(HTMLLabelEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FA6 RID: 98214 RVA: 0x00063A88 File Offset: 0x00062A88
		public override void remove_onactivate(HTMLLabelEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onactivateDelegate != null && ((htmllabelEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FA7 RID: 98215 RVA: 0x00063B78 File Offset: 0x00062B78
		public override void add_onmouseleave(HTMLLabelEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FA8 RID: 98216 RVA: 0x00063C08 File Offset: 0x00062C08
		public override void remove_onmouseleave(HTMLLabelEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmllabelEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FA9 RID: 98217 RVA: 0x00063CF8 File Offset: 0x00062CF8
		public override void add_onmouseenter(HTMLLabelEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FAA RID: 98218 RVA: 0x00063D88 File Offset: 0x00062D88
		public override void remove_onmouseenter(HTMLLabelEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmllabelEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FAB RID: 98219 RVA: 0x00063E78 File Offset: 0x00062E78
		public override void add_onpage(HTMLLabelEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FAC RID: 98220 RVA: 0x00063F08 File Offset: 0x00062F08
		public override void remove_onpage(HTMLLabelEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onpageDelegate != null && ((htmllabelEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FAD RID: 98221 RVA: 0x00063FF8 File Offset: 0x00062FF8
		public override void add_onlayoutcomplete(HTMLLabelEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FAE RID: 98222 RVA: 0x00064088 File Offset: 0x00063088
		public override void remove_onlayoutcomplete(HTMLLabelEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmllabelEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FAF RID: 98223 RVA: 0x00064178 File Offset: 0x00063178
		public override void add_onreadystatechange(HTMLLabelEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FB0 RID: 98224 RVA: 0x00064208 File Offset: 0x00063208
		public override void remove_onreadystatechange(HTMLLabelEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmllabelEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FB1 RID: 98225 RVA: 0x000642F8 File Offset: 0x000632F8
		public override void add_oncellchange(HTMLLabelEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FB2 RID: 98226 RVA: 0x00064388 File Offset: 0x00063388
		public override void remove_oncellchange(HTMLLabelEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmllabelEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FB3 RID: 98227 RVA: 0x00064478 File Offset: 0x00063478
		public override void add_onrowsinserted(HTMLLabelEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FB4 RID: 98228 RVA: 0x00064508 File Offset: 0x00063508
		public override void remove_onrowsinserted(HTMLLabelEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmllabelEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FB5 RID: 98229 RVA: 0x000645F8 File Offset: 0x000635F8
		public override void add_onrowsdelete(HTMLLabelEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FB6 RID: 98230 RVA: 0x00064688 File Offset: 0x00063688
		public override void remove_onrowsdelete(HTMLLabelEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmllabelEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FB7 RID: 98231 RVA: 0x00064778 File Offset: 0x00063778
		public override void add_oncontextmenu(HTMLLabelEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FB8 RID: 98232 RVA: 0x00064808 File Offset: 0x00063808
		public override void remove_oncontextmenu(HTMLLabelEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmllabelEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FB9 RID: 98233 RVA: 0x000648F8 File Offset: 0x000638F8
		public override void add_onpaste(HTMLLabelEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FBA RID: 98234 RVA: 0x00064988 File Offset: 0x00063988
		public override void remove_onpaste(HTMLLabelEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onpasteDelegate != null && ((htmllabelEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FBB RID: 98235 RVA: 0x00064A78 File Offset: 0x00063A78
		public override void add_onbeforepaste(HTMLLabelEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FBC RID: 98236 RVA: 0x00064B08 File Offset: 0x00063B08
		public override void remove_onbeforepaste(HTMLLabelEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmllabelEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FBD RID: 98237 RVA: 0x00064BF8 File Offset: 0x00063BF8
		public override void add_oncopy(HTMLLabelEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FBE RID: 98238 RVA: 0x00064C88 File Offset: 0x00063C88
		public override void remove_oncopy(HTMLLabelEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_oncopyDelegate != null && ((htmllabelEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FBF RID: 98239 RVA: 0x00064D78 File Offset: 0x00063D78
		public override void add_onbeforecopy(HTMLLabelEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FC0 RID: 98240 RVA: 0x00064E08 File Offset: 0x00063E08
		public override void remove_onbeforecopy(HTMLLabelEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmllabelEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FC1 RID: 98241 RVA: 0x00064EF8 File Offset: 0x00063EF8
		public override void add_oncut(HTMLLabelEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FC2 RID: 98242 RVA: 0x00064F88 File Offset: 0x00063F88
		public override void remove_oncut(HTMLLabelEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_oncutDelegate != null && ((htmllabelEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FC3 RID: 98243 RVA: 0x00065078 File Offset: 0x00064078
		public override void add_onbeforecut(HTMLLabelEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FC4 RID: 98244 RVA: 0x00065108 File Offset: 0x00064108
		public override void remove_onbeforecut(HTMLLabelEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmllabelEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FC5 RID: 98245 RVA: 0x000651F8 File Offset: 0x000641F8
		public override void add_ondrop(HTMLLabelEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FC6 RID: 98246 RVA: 0x00065288 File Offset: 0x00064288
		public override void remove_ondrop(HTMLLabelEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_ondropDelegate != null && ((htmllabelEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FC7 RID: 98247 RVA: 0x00065378 File Offset: 0x00064378
		public override void add_ondragleave(HTMLLabelEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FC8 RID: 98248 RVA: 0x00065408 File Offset: 0x00064408
		public override void remove_ondragleave(HTMLLabelEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmllabelEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FC9 RID: 98249 RVA: 0x000654F8 File Offset: 0x000644F8
		public override void add_ondragover(HTMLLabelEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FCA RID: 98250 RVA: 0x00065588 File Offset: 0x00064588
		public override void remove_ondragover(HTMLLabelEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmllabelEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FCB RID: 98251 RVA: 0x00065678 File Offset: 0x00064678
		public override void add_ondragenter(HTMLLabelEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FCC RID: 98252 RVA: 0x00065708 File Offset: 0x00064708
		public override void remove_ondragenter(HTMLLabelEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmllabelEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FCD RID: 98253 RVA: 0x000657F8 File Offset: 0x000647F8
		public override void add_ondragend(HTMLLabelEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FCE RID: 98254 RVA: 0x00065888 File Offset: 0x00064888
		public override void remove_ondragend(HTMLLabelEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_ondragendDelegate != null && ((htmllabelEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FCF RID: 98255 RVA: 0x00065978 File Offset: 0x00064978
		public override void add_ondrag(HTMLLabelEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FD0 RID: 98256 RVA: 0x00065A08 File Offset: 0x00064A08
		public override void remove_ondrag(HTMLLabelEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_ondragDelegate != null && ((htmllabelEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FD1 RID: 98257 RVA: 0x00065AF8 File Offset: 0x00064AF8
		public override void add_onresize(HTMLLabelEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FD2 RID: 98258 RVA: 0x00065B88 File Offset: 0x00064B88
		public override void remove_onresize(HTMLLabelEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onresizeDelegate != null && ((htmllabelEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FD3 RID: 98259 RVA: 0x00065C78 File Offset: 0x00064C78
		public override void add_onblur(HTMLLabelEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FD4 RID: 98260 RVA: 0x00065D08 File Offset: 0x00064D08
		public override void remove_onblur(HTMLLabelEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onblurDelegate != null && ((htmllabelEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FD5 RID: 98261 RVA: 0x00065DF8 File Offset: 0x00064DF8
		public override void add_onfocus(HTMLLabelEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FD6 RID: 98262 RVA: 0x00065E88 File Offset: 0x00064E88
		public override void remove_onfocus(HTMLLabelEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onfocusDelegate != null && ((htmllabelEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FD7 RID: 98263 RVA: 0x00065F78 File Offset: 0x00064F78
		public override void add_onscroll(HTMLLabelEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FD8 RID: 98264 RVA: 0x00066008 File Offset: 0x00065008
		public override void remove_onscroll(HTMLLabelEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onscrollDelegate != null && ((htmllabelEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FD9 RID: 98265 RVA: 0x000660F8 File Offset: 0x000650F8
		public override void add_onpropertychange(HTMLLabelEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FDA RID: 98266 RVA: 0x00066188 File Offset: 0x00065188
		public override void remove_onpropertychange(HTMLLabelEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmllabelEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FDB RID: 98267 RVA: 0x00066278 File Offset: 0x00065278
		public override void add_onlosecapture(HTMLLabelEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FDC RID: 98268 RVA: 0x00066308 File Offset: 0x00065308
		public override void remove_onlosecapture(HTMLLabelEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmllabelEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FDD RID: 98269 RVA: 0x000663F8 File Offset: 0x000653F8
		public override void add_ondatasetcomplete(HTMLLabelEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FDE RID: 98270 RVA: 0x00066488 File Offset: 0x00065488
		public override void remove_ondatasetcomplete(HTMLLabelEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmllabelEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FDF RID: 98271 RVA: 0x00066578 File Offset: 0x00065578
		public override void add_ondataavailable(HTMLLabelEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FE0 RID: 98272 RVA: 0x00066608 File Offset: 0x00065608
		public override void remove_ondataavailable(HTMLLabelEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmllabelEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FE1 RID: 98273 RVA: 0x000666F8 File Offset: 0x000656F8
		public override void add_ondatasetchanged(HTMLLabelEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FE2 RID: 98274 RVA: 0x00066788 File Offset: 0x00065788
		public override void remove_ondatasetchanged(HTMLLabelEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmllabelEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FE3 RID: 98275 RVA: 0x00066878 File Offset: 0x00065878
		public override void add_onrowenter(HTMLLabelEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FE4 RID: 98276 RVA: 0x00066908 File Offset: 0x00065908
		public override void remove_onrowenter(HTMLLabelEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmllabelEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FE5 RID: 98277 RVA: 0x000669F8 File Offset: 0x000659F8
		public override void add_onrowexit(HTMLLabelEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FE6 RID: 98278 RVA: 0x00066A88 File Offset: 0x00065A88
		public override void remove_onrowexit(HTMLLabelEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmllabelEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FE7 RID: 98279 RVA: 0x00066B78 File Offset: 0x00065B78
		public override void add_onerrorupdate(HTMLLabelEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FE8 RID: 98280 RVA: 0x00066C08 File Offset: 0x00065C08
		public override void remove_onerrorupdate(HTMLLabelEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmllabelEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FE9 RID: 98281 RVA: 0x00066CF8 File Offset: 0x00065CF8
		public override void add_onafterupdate(HTMLLabelEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FEA RID: 98282 RVA: 0x00066D88 File Offset: 0x00065D88
		public override void remove_onafterupdate(HTMLLabelEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmllabelEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FEB RID: 98283 RVA: 0x00066E78 File Offset: 0x00065E78
		public override void add_onbeforeupdate(HTMLLabelEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FEC RID: 98284 RVA: 0x00066F08 File Offset: 0x00065F08
		public override void remove_onbeforeupdate(HTMLLabelEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmllabelEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FED RID: 98285 RVA: 0x00066FF8 File Offset: 0x00065FF8
		public override void add_ondragstart(HTMLLabelEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FEE RID: 98286 RVA: 0x00067088 File Offset: 0x00066088
		public override void remove_ondragstart(HTMLLabelEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmllabelEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FEF RID: 98287 RVA: 0x00067178 File Offset: 0x00066178
		public override void add_onfilterchange(HTMLLabelEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FF0 RID: 98288 RVA: 0x00067208 File Offset: 0x00066208
		public override void remove_onfilterchange(HTMLLabelEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmllabelEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FF1 RID: 98289 RVA: 0x000672F8 File Offset: 0x000662F8
		public override void add_onselectstart(HTMLLabelEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FF2 RID: 98290 RVA: 0x00067388 File Offset: 0x00066388
		public override void remove_onselectstart(HTMLLabelEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmllabelEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FF3 RID: 98291 RVA: 0x00067478 File Offset: 0x00066478
		public override void add_onmouseup(HTMLLabelEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FF4 RID: 98292 RVA: 0x00067508 File Offset: 0x00066508
		public override void remove_onmouseup(HTMLLabelEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmllabelEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FF5 RID: 98293 RVA: 0x000675F8 File Offset: 0x000665F8
		public override void add_onmousedown(HTMLLabelEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FF6 RID: 98294 RVA: 0x00067688 File Offset: 0x00066688
		public override void remove_onmousedown(HTMLLabelEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmllabelEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FF7 RID: 98295 RVA: 0x00067778 File Offset: 0x00066778
		public override void add_onmousemove(HTMLLabelEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FF8 RID: 98296 RVA: 0x00067808 File Offset: 0x00066808
		public override void remove_onmousemove(HTMLLabelEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmllabelEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FF9 RID: 98297 RVA: 0x000678F8 File Offset: 0x000668F8
		public override void add_onmouseover(HTMLLabelEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FFA RID: 98298 RVA: 0x00067988 File Offset: 0x00066988
		public override void remove_onmouseover(HTMLLabelEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmllabelEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FFB RID: 98299 RVA: 0x00067A78 File Offset: 0x00066A78
		public override void add_onmouseout(HTMLLabelEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FFC RID: 98300 RVA: 0x00067B08 File Offset: 0x00066B08
		public override void remove_onmouseout(HTMLLabelEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmllabelEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FFD RID: 98301 RVA: 0x00067BF8 File Offset: 0x00066BF8
		public override void add_onkeyup(HTMLLabelEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06017FFE RID: 98302 RVA: 0x00067C88 File Offset: 0x00066C88
		public override void remove_onkeyup(HTMLLabelEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmllabelEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017FFF RID: 98303 RVA: 0x00067D78 File Offset: 0x00066D78
		public override void add_onkeydown(HTMLLabelEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06018000 RID: 98304 RVA: 0x00067E08 File Offset: 0x00066E08
		public override void remove_onkeydown(HTMLLabelEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmllabelEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018001 RID: 98305 RVA: 0x00067EF8 File Offset: 0x00066EF8
		public override void add_onkeypress(HTMLLabelEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06018002 RID: 98306 RVA: 0x00067F88 File Offset: 0x00066F88
		public override void remove_onkeypress(HTMLLabelEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmllabelEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018003 RID: 98307 RVA: 0x00068078 File Offset: 0x00067078
		public override void add_ondblclick(HTMLLabelEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06018004 RID: 98308 RVA: 0x00068108 File Offset: 0x00067108
		public override void remove_ondblclick(HTMLLabelEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmllabelEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018005 RID: 98309 RVA: 0x000681F8 File Offset: 0x000671F8
		public override void add_onclick(HTMLLabelEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06018006 RID: 98310 RVA: 0x00068288 File Offset: 0x00067288
		public override void remove_onclick(HTMLLabelEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onclickDelegate != null && ((htmllabelEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018007 RID: 98311 RVA: 0x00068378 File Offset: 0x00067378
		public override void add_onhelp(HTMLLabelEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = new HTMLLabelEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents2_SinkHelper, ref num);
				htmllabelEvents2_SinkHelper.m_dwCookie = num;
				htmllabelEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents2_SinkHelper);
			}
		}

		// Token: 0x06018008 RID: 98312 RVA: 0x00068408 File Offset: 0x00067408
		public override void remove_onhelp(HTMLLabelEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper;
					for (;;)
					{
						htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents2_SinkHelper.m_onhelpDelegate != null && ((htmllabelEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018009 RID: 98313 RVA: 0x000684F8 File Offset: 0x000674F8
		public HTMLLabelEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x0601800A RID: 98314 RVA: 0x00068520 File Offset: 0x00067520
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
								HTMLLabelEvents2_SinkHelper htmllabelEvents2_SinkHelper = (HTMLLabelEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmllabelEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x0601800B RID: 98315 RVA: 0x000685D4 File Offset: 0x000675D4
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400089E RID: 2206
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x0400089F RID: 2207
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x040008A0 RID: 2208
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
