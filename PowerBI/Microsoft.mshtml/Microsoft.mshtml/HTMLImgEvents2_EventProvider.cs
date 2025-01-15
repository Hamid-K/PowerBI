using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E09 RID: 3593
	internal sealed class HTMLImgEvents2_EventProvider : HTMLImgEvents2_Event, IDisposable
	{
		// Token: 0x06018E53 RID: 101971 RVA: 0x000E8ECC File Offset: 0x000E7ECC
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				22, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06018E54 RID: 101972 RVA: 0x000E8FE0 File Offset: 0x000E7FE0
		public override void add_onabort(HTMLImgEvents2_onabortEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onabortDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E55 RID: 101973 RVA: 0x000E9070 File Offset: 0x000E8070
		public override void remove_onabort(HTMLImgEvents2_onabortEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onabortDelegate != null && ((htmlimgEvents2_SinkHelper.m_onabortDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E56 RID: 101974 RVA: 0x000E9160 File Offset: 0x000E8160
		public override void add_onerror(HTMLImgEvents2_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E57 RID: 101975 RVA: 0x000E91F0 File Offset: 0x000E81F0
		public override void remove_onerror(HTMLImgEvents2_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onerrorDelegate != null && ((htmlimgEvents2_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E58 RID: 101976 RVA: 0x000E92E0 File Offset: 0x000E82E0
		public override void add_onload(HTMLImgEvents2_onloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E59 RID: 101977 RVA: 0x000E9370 File Offset: 0x000E8370
		public override void remove_onload(HTMLImgEvents2_onloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onloadDelegate != null && ((htmlimgEvents2_SinkHelper.m_onloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E5A RID: 101978 RVA: 0x000E9460 File Offset: 0x000E8460
		public override void add_onmousewheel(HTMLImgEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E5B RID: 101979 RVA: 0x000E94F0 File Offset: 0x000E84F0
		public override void remove_onmousewheel(HTMLImgEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmlimgEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E5C RID: 101980 RVA: 0x000E95E0 File Offset: 0x000E85E0
		public override void add_onresizeend(HTMLImgEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E5D RID: 101981 RVA: 0x000E9670 File Offset: 0x000E8670
		public override void remove_onresizeend(HTMLImgEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmlimgEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E5E RID: 101982 RVA: 0x000E9760 File Offset: 0x000E8760
		public override void add_onresizestart(HTMLImgEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E5F RID: 101983 RVA: 0x000E97F0 File Offset: 0x000E87F0
		public override void remove_onresizestart(HTMLImgEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmlimgEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E60 RID: 101984 RVA: 0x000E98E0 File Offset: 0x000E88E0
		public override void add_onmoveend(HTMLImgEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E61 RID: 101985 RVA: 0x000E9970 File Offset: 0x000E8970
		public override void remove_onmoveend(HTMLImgEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmlimgEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E62 RID: 101986 RVA: 0x000E9A60 File Offset: 0x000E8A60
		public override void add_onmovestart(HTMLImgEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E63 RID: 101987 RVA: 0x000E9AF0 File Offset: 0x000E8AF0
		public override void remove_onmovestart(HTMLImgEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmlimgEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E64 RID: 101988 RVA: 0x000E9BE0 File Offset: 0x000E8BE0
		public override void add_oncontrolselect(HTMLImgEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E65 RID: 101989 RVA: 0x000E9C70 File Offset: 0x000E8C70
		public override void remove_oncontrolselect(HTMLImgEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmlimgEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E66 RID: 101990 RVA: 0x000E9D60 File Offset: 0x000E8D60
		public override void add_onmove(HTMLImgEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E67 RID: 101991 RVA: 0x000E9DF0 File Offset: 0x000E8DF0
		public override void remove_onmove(HTMLImgEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onmoveDelegate != null && ((htmlimgEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E68 RID: 101992 RVA: 0x000E9EE0 File Offset: 0x000E8EE0
		public override void add_onfocusout(HTMLImgEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E69 RID: 101993 RVA: 0x000E9F70 File Offset: 0x000E8F70
		public override void remove_onfocusout(HTMLImgEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmlimgEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E6A RID: 101994 RVA: 0x000EA060 File Offset: 0x000E9060
		public override void add_onfocusin(HTMLImgEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E6B RID: 101995 RVA: 0x000EA0F0 File Offset: 0x000E90F0
		public override void remove_onfocusin(HTMLImgEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmlimgEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E6C RID: 101996 RVA: 0x000EA1E0 File Offset: 0x000E91E0
		public override void add_onbeforeactivate(HTMLImgEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E6D RID: 101997 RVA: 0x000EA270 File Offset: 0x000E9270
		public override void remove_onbeforeactivate(HTMLImgEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlimgEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E6E RID: 101998 RVA: 0x000EA360 File Offset: 0x000E9360
		public override void add_onbeforedeactivate(HTMLImgEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E6F RID: 101999 RVA: 0x000EA3F0 File Offset: 0x000E93F0
		public override void remove_onbeforedeactivate(HTMLImgEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlimgEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E70 RID: 102000 RVA: 0x000EA4E0 File Offset: 0x000E94E0
		public override void add_ondeactivate(HTMLImgEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E71 RID: 102001 RVA: 0x000EA570 File Offset: 0x000E9570
		public override void remove_ondeactivate(HTMLImgEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmlimgEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E72 RID: 102002 RVA: 0x000EA660 File Offset: 0x000E9660
		public override void add_onactivate(HTMLImgEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E73 RID: 102003 RVA: 0x000EA6F0 File Offset: 0x000E96F0
		public override void remove_onactivate(HTMLImgEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onactivateDelegate != null && ((htmlimgEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E74 RID: 102004 RVA: 0x000EA7E0 File Offset: 0x000E97E0
		public override void add_onmouseleave(HTMLImgEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E75 RID: 102005 RVA: 0x000EA870 File Offset: 0x000E9870
		public override void remove_onmouseleave(HTMLImgEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmlimgEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E76 RID: 102006 RVA: 0x000EA960 File Offset: 0x000E9960
		public override void add_onmouseenter(HTMLImgEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E77 RID: 102007 RVA: 0x000EA9F0 File Offset: 0x000E99F0
		public override void remove_onmouseenter(HTMLImgEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmlimgEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E78 RID: 102008 RVA: 0x000EAAE0 File Offset: 0x000E9AE0
		public override void add_onpage(HTMLImgEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E79 RID: 102009 RVA: 0x000EAB70 File Offset: 0x000E9B70
		public override void remove_onpage(HTMLImgEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onpageDelegate != null && ((htmlimgEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E7A RID: 102010 RVA: 0x000EAC60 File Offset: 0x000E9C60
		public override void add_onlayoutcomplete(HTMLImgEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E7B RID: 102011 RVA: 0x000EACF0 File Offset: 0x000E9CF0
		public override void remove_onlayoutcomplete(HTMLImgEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlimgEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E7C RID: 102012 RVA: 0x000EADE0 File Offset: 0x000E9DE0
		public override void add_onreadystatechange(HTMLImgEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E7D RID: 102013 RVA: 0x000EAE70 File Offset: 0x000E9E70
		public override void remove_onreadystatechange(HTMLImgEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlimgEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E7E RID: 102014 RVA: 0x000EAF60 File Offset: 0x000E9F60
		public override void add_oncellchange(HTMLImgEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E7F RID: 102015 RVA: 0x000EAFF0 File Offset: 0x000E9FF0
		public override void remove_oncellchange(HTMLImgEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmlimgEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E80 RID: 102016 RVA: 0x000EB0E0 File Offset: 0x000EA0E0
		public override void add_onrowsinserted(HTMLImgEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E81 RID: 102017 RVA: 0x000EB170 File Offset: 0x000EA170
		public override void remove_onrowsinserted(HTMLImgEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlimgEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E82 RID: 102018 RVA: 0x000EB260 File Offset: 0x000EA260
		public override void add_onrowsdelete(HTMLImgEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E83 RID: 102019 RVA: 0x000EB2F0 File Offset: 0x000EA2F0
		public override void remove_onrowsdelete(HTMLImgEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlimgEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E84 RID: 102020 RVA: 0x000EB3E0 File Offset: 0x000EA3E0
		public override void add_oncontextmenu(HTMLImgEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E85 RID: 102021 RVA: 0x000EB470 File Offset: 0x000EA470
		public override void remove_oncontextmenu(HTMLImgEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmlimgEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E86 RID: 102022 RVA: 0x000EB560 File Offset: 0x000EA560
		public override void add_onpaste(HTMLImgEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E87 RID: 102023 RVA: 0x000EB5F0 File Offset: 0x000EA5F0
		public override void remove_onpaste(HTMLImgEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onpasteDelegate != null && ((htmlimgEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E88 RID: 102024 RVA: 0x000EB6E0 File Offset: 0x000EA6E0
		public override void add_onbeforepaste(HTMLImgEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E89 RID: 102025 RVA: 0x000EB770 File Offset: 0x000EA770
		public override void remove_onbeforepaste(HTMLImgEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmlimgEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E8A RID: 102026 RVA: 0x000EB860 File Offset: 0x000EA860
		public override void add_oncopy(HTMLImgEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E8B RID: 102027 RVA: 0x000EB8F0 File Offset: 0x000EA8F0
		public override void remove_oncopy(HTMLImgEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_oncopyDelegate != null && ((htmlimgEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E8C RID: 102028 RVA: 0x000EB9E0 File Offset: 0x000EA9E0
		public override void add_onbeforecopy(HTMLImgEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E8D RID: 102029 RVA: 0x000EBA70 File Offset: 0x000EAA70
		public override void remove_onbeforecopy(HTMLImgEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmlimgEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E8E RID: 102030 RVA: 0x000EBB60 File Offset: 0x000EAB60
		public override void add_oncut(HTMLImgEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E8F RID: 102031 RVA: 0x000EBBF0 File Offset: 0x000EABF0
		public override void remove_oncut(HTMLImgEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_oncutDelegate != null && ((htmlimgEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E90 RID: 102032 RVA: 0x000EBCE0 File Offset: 0x000EACE0
		public override void add_onbeforecut(HTMLImgEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E91 RID: 102033 RVA: 0x000EBD70 File Offset: 0x000EAD70
		public override void remove_onbeforecut(HTMLImgEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmlimgEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E92 RID: 102034 RVA: 0x000EBE60 File Offset: 0x000EAE60
		public override void add_ondrop(HTMLImgEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E93 RID: 102035 RVA: 0x000EBEF0 File Offset: 0x000EAEF0
		public override void remove_ondrop(HTMLImgEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_ondropDelegate != null && ((htmlimgEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E94 RID: 102036 RVA: 0x000EBFE0 File Offset: 0x000EAFE0
		public override void add_ondragleave(HTMLImgEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E95 RID: 102037 RVA: 0x000EC070 File Offset: 0x000EB070
		public override void remove_ondragleave(HTMLImgEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmlimgEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E96 RID: 102038 RVA: 0x000EC160 File Offset: 0x000EB160
		public override void add_ondragover(HTMLImgEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E97 RID: 102039 RVA: 0x000EC1F0 File Offset: 0x000EB1F0
		public override void remove_ondragover(HTMLImgEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmlimgEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E98 RID: 102040 RVA: 0x000EC2E0 File Offset: 0x000EB2E0
		public override void add_ondragenter(HTMLImgEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E99 RID: 102041 RVA: 0x000EC370 File Offset: 0x000EB370
		public override void remove_ondragenter(HTMLImgEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmlimgEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E9A RID: 102042 RVA: 0x000EC460 File Offset: 0x000EB460
		public override void add_ondragend(HTMLImgEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E9B RID: 102043 RVA: 0x000EC4F0 File Offset: 0x000EB4F0
		public override void remove_ondragend(HTMLImgEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_ondragendDelegate != null && ((htmlimgEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E9C RID: 102044 RVA: 0x000EC5E0 File Offset: 0x000EB5E0
		public override void add_ondrag(HTMLImgEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E9D RID: 102045 RVA: 0x000EC670 File Offset: 0x000EB670
		public override void remove_ondrag(HTMLImgEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_ondragDelegate != null && ((htmlimgEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018E9E RID: 102046 RVA: 0x000EC760 File Offset: 0x000EB760
		public override void add_onresize(HTMLImgEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018E9F RID: 102047 RVA: 0x000EC7F0 File Offset: 0x000EB7F0
		public override void remove_onresize(HTMLImgEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onresizeDelegate != null && ((htmlimgEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EA0 RID: 102048 RVA: 0x000EC8E0 File Offset: 0x000EB8E0
		public override void add_onblur(HTMLImgEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EA1 RID: 102049 RVA: 0x000EC970 File Offset: 0x000EB970
		public override void remove_onblur(HTMLImgEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onblurDelegate != null && ((htmlimgEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EA2 RID: 102050 RVA: 0x000ECA60 File Offset: 0x000EBA60
		public override void add_onfocus(HTMLImgEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EA3 RID: 102051 RVA: 0x000ECAF0 File Offset: 0x000EBAF0
		public override void remove_onfocus(HTMLImgEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onfocusDelegate != null && ((htmlimgEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EA4 RID: 102052 RVA: 0x000ECBE0 File Offset: 0x000EBBE0
		public override void add_onscroll(HTMLImgEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EA5 RID: 102053 RVA: 0x000ECC70 File Offset: 0x000EBC70
		public override void remove_onscroll(HTMLImgEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onscrollDelegate != null && ((htmlimgEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EA6 RID: 102054 RVA: 0x000ECD60 File Offset: 0x000EBD60
		public override void add_onpropertychange(HTMLImgEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EA7 RID: 102055 RVA: 0x000ECDF0 File Offset: 0x000EBDF0
		public override void remove_onpropertychange(HTMLImgEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmlimgEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EA8 RID: 102056 RVA: 0x000ECEE0 File Offset: 0x000EBEE0
		public override void add_onlosecapture(HTMLImgEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EA9 RID: 102057 RVA: 0x000ECF70 File Offset: 0x000EBF70
		public override void remove_onlosecapture(HTMLImgEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmlimgEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EAA RID: 102058 RVA: 0x000ED060 File Offset: 0x000EC060
		public override void add_ondatasetcomplete(HTMLImgEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EAB RID: 102059 RVA: 0x000ED0F0 File Offset: 0x000EC0F0
		public override void remove_ondatasetcomplete(HTMLImgEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlimgEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EAC RID: 102060 RVA: 0x000ED1E0 File Offset: 0x000EC1E0
		public override void add_ondataavailable(HTMLImgEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EAD RID: 102061 RVA: 0x000ED270 File Offset: 0x000EC270
		public override void remove_ondataavailable(HTMLImgEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmlimgEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EAE RID: 102062 RVA: 0x000ED360 File Offset: 0x000EC360
		public override void add_ondatasetchanged(HTMLImgEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EAF RID: 102063 RVA: 0x000ED3F0 File Offset: 0x000EC3F0
		public override void remove_ondatasetchanged(HTMLImgEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlimgEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EB0 RID: 102064 RVA: 0x000ED4E0 File Offset: 0x000EC4E0
		public override void add_onrowenter(HTMLImgEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EB1 RID: 102065 RVA: 0x000ED570 File Offset: 0x000EC570
		public override void remove_onrowenter(HTMLImgEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmlimgEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EB2 RID: 102066 RVA: 0x000ED660 File Offset: 0x000EC660
		public override void add_onrowexit(HTMLImgEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EB3 RID: 102067 RVA: 0x000ED6F0 File Offset: 0x000EC6F0
		public override void remove_onrowexit(HTMLImgEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmlimgEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EB4 RID: 102068 RVA: 0x000ED7E0 File Offset: 0x000EC7E0
		public override void add_onerrorupdate(HTMLImgEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EB5 RID: 102069 RVA: 0x000ED870 File Offset: 0x000EC870
		public override void remove_onerrorupdate(HTMLImgEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmlimgEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EB6 RID: 102070 RVA: 0x000ED960 File Offset: 0x000EC960
		public override void add_onafterupdate(HTMLImgEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EB7 RID: 102071 RVA: 0x000ED9F0 File Offset: 0x000EC9F0
		public override void remove_onafterupdate(HTMLImgEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmlimgEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EB8 RID: 102072 RVA: 0x000EDAE0 File Offset: 0x000ECAE0
		public override void add_onbeforeupdate(HTMLImgEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EB9 RID: 102073 RVA: 0x000EDB70 File Offset: 0x000ECB70
		public override void remove_onbeforeupdate(HTMLImgEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlimgEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EBA RID: 102074 RVA: 0x000EDC60 File Offset: 0x000ECC60
		public override void add_ondragstart(HTMLImgEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EBB RID: 102075 RVA: 0x000EDCF0 File Offset: 0x000ECCF0
		public override void remove_ondragstart(HTMLImgEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmlimgEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EBC RID: 102076 RVA: 0x000EDDE0 File Offset: 0x000ECDE0
		public override void add_onfilterchange(HTMLImgEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EBD RID: 102077 RVA: 0x000EDE70 File Offset: 0x000ECE70
		public override void remove_onfilterchange(HTMLImgEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmlimgEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EBE RID: 102078 RVA: 0x000EDF60 File Offset: 0x000ECF60
		public override void add_onselectstart(HTMLImgEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EBF RID: 102079 RVA: 0x000EDFF0 File Offset: 0x000ECFF0
		public override void remove_onselectstart(HTMLImgEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmlimgEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EC0 RID: 102080 RVA: 0x000EE0E0 File Offset: 0x000ED0E0
		public override void add_onmouseup(HTMLImgEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EC1 RID: 102081 RVA: 0x000EE170 File Offset: 0x000ED170
		public override void remove_onmouseup(HTMLImgEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmlimgEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EC2 RID: 102082 RVA: 0x000EE260 File Offset: 0x000ED260
		public override void add_onmousedown(HTMLImgEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EC3 RID: 102083 RVA: 0x000EE2F0 File Offset: 0x000ED2F0
		public override void remove_onmousedown(HTMLImgEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmlimgEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EC4 RID: 102084 RVA: 0x000EE3E0 File Offset: 0x000ED3E0
		public override void add_onmousemove(HTMLImgEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EC5 RID: 102085 RVA: 0x000EE470 File Offset: 0x000ED470
		public override void remove_onmousemove(HTMLImgEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmlimgEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EC6 RID: 102086 RVA: 0x000EE560 File Offset: 0x000ED560
		public override void add_onmouseover(HTMLImgEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EC7 RID: 102087 RVA: 0x000EE5F0 File Offset: 0x000ED5F0
		public override void remove_onmouseover(HTMLImgEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmlimgEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018EC8 RID: 102088 RVA: 0x000EE6E0 File Offset: 0x000ED6E0
		public override void add_onmouseout(HTMLImgEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018EC9 RID: 102089 RVA: 0x000EE770 File Offset: 0x000ED770
		public override void remove_onmouseout(HTMLImgEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmlimgEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018ECA RID: 102090 RVA: 0x000EE860 File Offset: 0x000ED860
		public override void add_onkeyup(HTMLImgEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018ECB RID: 102091 RVA: 0x000EE8F0 File Offset: 0x000ED8F0
		public override void remove_onkeyup(HTMLImgEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmlimgEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018ECC RID: 102092 RVA: 0x000EE9E0 File Offset: 0x000ED9E0
		public override void add_onkeydown(HTMLImgEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018ECD RID: 102093 RVA: 0x000EEA70 File Offset: 0x000EDA70
		public override void remove_onkeydown(HTMLImgEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmlimgEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018ECE RID: 102094 RVA: 0x000EEB60 File Offset: 0x000EDB60
		public override void add_onkeypress(HTMLImgEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018ECF RID: 102095 RVA: 0x000EEBF0 File Offset: 0x000EDBF0
		public override void remove_onkeypress(HTMLImgEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmlimgEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018ED0 RID: 102096 RVA: 0x000EECE0 File Offset: 0x000EDCE0
		public override void add_ondblclick(HTMLImgEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018ED1 RID: 102097 RVA: 0x000EED70 File Offset: 0x000EDD70
		public override void remove_ondblclick(HTMLImgEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmlimgEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018ED2 RID: 102098 RVA: 0x000EEE60 File Offset: 0x000EDE60
		public override void add_onclick(HTMLImgEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018ED3 RID: 102099 RVA: 0x000EEEF0 File Offset: 0x000EDEF0
		public override void remove_onclick(HTMLImgEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onclickDelegate != null && ((htmlimgEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018ED4 RID: 102100 RVA: 0x000EEFE0 File Offset: 0x000EDFE0
		public override void add_onhelp(HTMLImgEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = new HTMLImgEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents2_SinkHelper, ref num);
				htmlimgEvents2_SinkHelper.m_dwCookie = num;
				htmlimgEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents2_SinkHelper);
			}
		}

		// Token: 0x06018ED5 RID: 102101 RVA: 0x000EF070 File Offset: 0x000EE070
		public override void remove_onhelp(HTMLImgEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper;
					for (;;)
					{
						htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents2_SinkHelper.m_onhelpDelegate != null && ((htmlimgEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018ED6 RID: 102102 RVA: 0x000EF160 File Offset: 0x000EE160
		public HTMLImgEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06018ED7 RID: 102103 RVA: 0x000EF188 File Offset: 0x000EE188
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
								HTMLImgEvents2_SinkHelper htmlimgEvents2_SinkHelper = (HTMLImgEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlimgEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x06018ED8 RID: 102104 RVA: 0x000EF23C File Offset: 0x000EE23C
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000DBE RID: 3518
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000DBF RID: 3519
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000DC0 RID: 3520
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
