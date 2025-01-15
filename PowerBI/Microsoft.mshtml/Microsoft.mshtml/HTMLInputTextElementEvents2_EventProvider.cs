using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E0B RID: 3595
	internal sealed class HTMLInputTextElementEvents2_EventProvider : HTMLInputTextElementEvents2_Event, IDisposable
	{
		// Token: 0x06018F1D RID: 102173 RVA: 0x000F01B0 File Offset: 0x000EF1B0
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				24, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06018F1E RID: 102174 RVA: 0x000F02C4 File Offset: 0x000EF2C4
		public override void add_onabort(HTMLInputTextElementEvents2_onabortEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onabortDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F1F RID: 102175 RVA: 0x000F0354 File Offset: 0x000EF354
		public override void remove_onabort(HTMLInputTextElementEvents2_onabortEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onabortDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onabortDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F20 RID: 102176 RVA: 0x000F0444 File Offset: 0x000EF444
		public override void add_onerror(HTMLInputTextElementEvents2_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F21 RID: 102177 RVA: 0x000F04D4 File Offset: 0x000EF4D4
		public override void remove_onerror(HTMLInputTextElementEvents2_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onerrorDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F22 RID: 102178 RVA: 0x000F05C4 File Offset: 0x000EF5C4
		public override void add_onload(HTMLInputTextElementEvents2_onloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F23 RID: 102179 RVA: 0x000F0654 File Offset: 0x000EF654
		public override void remove_onload(HTMLInputTextElementEvents2_onloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onloadDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F24 RID: 102180 RVA: 0x000F0744 File Offset: 0x000EF744
		public override void add_onselect(HTMLInputTextElementEvents2_onselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F25 RID: 102181 RVA: 0x000F07D4 File Offset: 0x000EF7D4
		public override void remove_onselect(HTMLInputTextElementEvents2_onselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onselectDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F26 RID: 102182 RVA: 0x000F08C4 File Offset: 0x000EF8C4
		public override void add_onchange(HTMLInputTextElementEvents2_onchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F27 RID: 102183 RVA: 0x000F0954 File Offset: 0x000EF954
		public override void remove_onchange(HTMLInputTextElementEvents2_onchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onchangeDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F28 RID: 102184 RVA: 0x000F0A44 File Offset: 0x000EFA44
		public override void add_onmousewheel(HTMLInputTextElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F29 RID: 102185 RVA: 0x000F0AD4 File Offset: 0x000EFAD4
		public override void remove_onmousewheel(HTMLInputTextElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F2A RID: 102186 RVA: 0x000F0BC4 File Offset: 0x000EFBC4
		public override void add_onresizeend(HTMLInputTextElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F2B RID: 102187 RVA: 0x000F0C54 File Offset: 0x000EFC54
		public override void remove_onresizeend(HTMLInputTextElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F2C RID: 102188 RVA: 0x000F0D44 File Offset: 0x000EFD44
		public override void add_onresizestart(HTMLInputTextElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F2D RID: 102189 RVA: 0x000F0DD4 File Offset: 0x000EFDD4
		public override void remove_onresizestart(HTMLInputTextElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F2E RID: 102190 RVA: 0x000F0EC4 File Offset: 0x000EFEC4
		public override void add_onmoveend(HTMLInputTextElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F2F RID: 102191 RVA: 0x000F0F54 File Offset: 0x000EFF54
		public override void remove_onmoveend(HTMLInputTextElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F30 RID: 102192 RVA: 0x000F1044 File Offset: 0x000F0044
		public override void add_onmovestart(HTMLInputTextElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F31 RID: 102193 RVA: 0x000F10D4 File Offset: 0x000F00D4
		public override void remove_onmovestart(HTMLInputTextElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F32 RID: 102194 RVA: 0x000F11C4 File Offset: 0x000F01C4
		public override void add_oncontrolselect(HTMLInputTextElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F33 RID: 102195 RVA: 0x000F1254 File Offset: 0x000F0254
		public override void remove_oncontrolselect(HTMLInputTextElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F34 RID: 102196 RVA: 0x000F1344 File Offset: 0x000F0344
		public override void add_onmove(HTMLInputTextElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F35 RID: 102197 RVA: 0x000F13D4 File Offset: 0x000F03D4
		public override void remove_onmove(HTMLInputTextElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onmoveDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F36 RID: 102198 RVA: 0x000F14C4 File Offset: 0x000F04C4
		public override void add_onfocusout(HTMLInputTextElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F37 RID: 102199 RVA: 0x000F1554 File Offset: 0x000F0554
		public override void remove_onfocusout(HTMLInputTextElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F38 RID: 102200 RVA: 0x000F1644 File Offset: 0x000F0644
		public override void add_onfocusin(HTMLInputTextElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F39 RID: 102201 RVA: 0x000F16D4 File Offset: 0x000F06D4
		public override void remove_onfocusin(HTMLInputTextElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F3A RID: 102202 RVA: 0x000F17C4 File Offset: 0x000F07C4
		public override void add_onbeforeactivate(HTMLInputTextElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F3B RID: 102203 RVA: 0x000F1854 File Offset: 0x000F0854
		public override void remove_onbeforeactivate(HTMLInputTextElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F3C RID: 102204 RVA: 0x000F1944 File Offset: 0x000F0944
		public override void add_onbeforedeactivate(HTMLInputTextElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F3D RID: 102205 RVA: 0x000F19D4 File Offset: 0x000F09D4
		public override void remove_onbeforedeactivate(HTMLInputTextElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F3E RID: 102206 RVA: 0x000F1AC4 File Offset: 0x000F0AC4
		public override void add_ondeactivate(HTMLInputTextElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F3F RID: 102207 RVA: 0x000F1B54 File Offset: 0x000F0B54
		public override void remove_ondeactivate(HTMLInputTextElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F40 RID: 102208 RVA: 0x000F1C44 File Offset: 0x000F0C44
		public override void add_onactivate(HTMLInputTextElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F41 RID: 102209 RVA: 0x000F1CD4 File Offset: 0x000F0CD4
		public override void remove_onactivate(HTMLInputTextElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onactivateDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F42 RID: 102210 RVA: 0x000F1DC4 File Offset: 0x000F0DC4
		public override void add_onmouseleave(HTMLInputTextElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F43 RID: 102211 RVA: 0x000F1E54 File Offset: 0x000F0E54
		public override void remove_onmouseleave(HTMLInputTextElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F44 RID: 102212 RVA: 0x000F1F44 File Offset: 0x000F0F44
		public override void add_onmouseenter(HTMLInputTextElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F45 RID: 102213 RVA: 0x000F1FD4 File Offset: 0x000F0FD4
		public override void remove_onmouseenter(HTMLInputTextElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F46 RID: 102214 RVA: 0x000F20C4 File Offset: 0x000F10C4
		public override void add_onpage(HTMLInputTextElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F47 RID: 102215 RVA: 0x000F2154 File Offset: 0x000F1154
		public override void remove_onpage(HTMLInputTextElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onpageDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F48 RID: 102216 RVA: 0x000F2244 File Offset: 0x000F1244
		public override void add_onlayoutcomplete(HTMLInputTextElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F49 RID: 102217 RVA: 0x000F22D4 File Offset: 0x000F12D4
		public override void remove_onlayoutcomplete(HTMLInputTextElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F4A RID: 102218 RVA: 0x000F23C4 File Offset: 0x000F13C4
		public override void add_onreadystatechange(HTMLInputTextElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F4B RID: 102219 RVA: 0x000F2454 File Offset: 0x000F1454
		public override void remove_onreadystatechange(HTMLInputTextElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F4C RID: 102220 RVA: 0x000F2544 File Offset: 0x000F1544
		public override void add_oncellchange(HTMLInputTextElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F4D RID: 102221 RVA: 0x000F25D4 File Offset: 0x000F15D4
		public override void remove_oncellchange(HTMLInputTextElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F4E RID: 102222 RVA: 0x000F26C4 File Offset: 0x000F16C4
		public override void add_onrowsinserted(HTMLInputTextElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F4F RID: 102223 RVA: 0x000F2754 File Offset: 0x000F1754
		public override void remove_onrowsinserted(HTMLInputTextElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F50 RID: 102224 RVA: 0x000F2844 File Offset: 0x000F1844
		public override void add_onrowsdelete(HTMLInputTextElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F51 RID: 102225 RVA: 0x000F28D4 File Offset: 0x000F18D4
		public override void remove_onrowsdelete(HTMLInputTextElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F52 RID: 102226 RVA: 0x000F29C4 File Offset: 0x000F19C4
		public override void add_oncontextmenu(HTMLInputTextElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F53 RID: 102227 RVA: 0x000F2A54 File Offset: 0x000F1A54
		public override void remove_oncontextmenu(HTMLInputTextElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F54 RID: 102228 RVA: 0x000F2B44 File Offset: 0x000F1B44
		public override void add_onpaste(HTMLInputTextElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F55 RID: 102229 RVA: 0x000F2BD4 File Offset: 0x000F1BD4
		public override void remove_onpaste(HTMLInputTextElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onpasteDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F56 RID: 102230 RVA: 0x000F2CC4 File Offset: 0x000F1CC4
		public override void add_onbeforepaste(HTMLInputTextElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F57 RID: 102231 RVA: 0x000F2D54 File Offset: 0x000F1D54
		public override void remove_onbeforepaste(HTMLInputTextElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F58 RID: 102232 RVA: 0x000F2E44 File Offset: 0x000F1E44
		public override void add_oncopy(HTMLInputTextElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F59 RID: 102233 RVA: 0x000F2ED4 File Offset: 0x000F1ED4
		public override void remove_oncopy(HTMLInputTextElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_oncopyDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F5A RID: 102234 RVA: 0x000F2FC4 File Offset: 0x000F1FC4
		public override void add_onbeforecopy(HTMLInputTextElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F5B RID: 102235 RVA: 0x000F3054 File Offset: 0x000F2054
		public override void remove_onbeforecopy(HTMLInputTextElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F5C RID: 102236 RVA: 0x000F3144 File Offset: 0x000F2144
		public override void add_oncut(HTMLInputTextElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F5D RID: 102237 RVA: 0x000F31D4 File Offset: 0x000F21D4
		public override void remove_oncut(HTMLInputTextElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_oncutDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F5E RID: 102238 RVA: 0x000F32C4 File Offset: 0x000F22C4
		public override void add_onbeforecut(HTMLInputTextElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F5F RID: 102239 RVA: 0x000F3354 File Offset: 0x000F2354
		public override void remove_onbeforecut(HTMLInputTextElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F60 RID: 102240 RVA: 0x000F3444 File Offset: 0x000F2444
		public override void add_ondrop(HTMLInputTextElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F61 RID: 102241 RVA: 0x000F34D4 File Offset: 0x000F24D4
		public override void remove_ondrop(HTMLInputTextElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_ondropDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F62 RID: 102242 RVA: 0x000F35C4 File Offset: 0x000F25C4
		public override void add_ondragleave(HTMLInputTextElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F63 RID: 102243 RVA: 0x000F3654 File Offset: 0x000F2654
		public override void remove_ondragleave(HTMLInputTextElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F64 RID: 102244 RVA: 0x000F3744 File Offset: 0x000F2744
		public override void add_ondragover(HTMLInputTextElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F65 RID: 102245 RVA: 0x000F37D4 File Offset: 0x000F27D4
		public override void remove_ondragover(HTMLInputTextElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F66 RID: 102246 RVA: 0x000F38C4 File Offset: 0x000F28C4
		public override void add_ondragenter(HTMLInputTextElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F67 RID: 102247 RVA: 0x000F3954 File Offset: 0x000F2954
		public override void remove_ondragenter(HTMLInputTextElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F68 RID: 102248 RVA: 0x000F3A44 File Offset: 0x000F2A44
		public override void add_ondragend(HTMLInputTextElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F69 RID: 102249 RVA: 0x000F3AD4 File Offset: 0x000F2AD4
		public override void remove_ondragend(HTMLInputTextElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_ondragendDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F6A RID: 102250 RVA: 0x000F3BC4 File Offset: 0x000F2BC4
		public override void add_ondrag(HTMLInputTextElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F6B RID: 102251 RVA: 0x000F3C54 File Offset: 0x000F2C54
		public override void remove_ondrag(HTMLInputTextElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_ondragDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F6C RID: 102252 RVA: 0x000F3D44 File Offset: 0x000F2D44
		public override void add_onresize(HTMLInputTextElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F6D RID: 102253 RVA: 0x000F3DD4 File Offset: 0x000F2DD4
		public override void remove_onresize(HTMLInputTextElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onresizeDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F6E RID: 102254 RVA: 0x000F3EC4 File Offset: 0x000F2EC4
		public override void add_onblur(HTMLInputTextElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F6F RID: 102255 RVA: 0x000F3F54 File Offset: 0x000F2F54
		public override void remove_onblur(HTMLInputTextElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onblurDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F70 RID: 102256 RVA: 0x000F4044 File Offset: 0x000F3044
		public override void add_onfocus(HTMLInputTextElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F71 RID: 102257 RVA: 0x000F40D4 File Offset: 0x000F30D4
		public override void remove_onfocus(HTMLInputTextElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onfocusDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F72 RID: 102258 RVA: 0x000F41C4 File Offset: 0x000F31C4
		public override void add_onscroll(HTMLInputTextElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F73 RID: 102259 RVA: 0x000F4254 File Offset: 0x000F3254
		public override void remove_onscroll(HTMLInputTextElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onscrollDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F74 RID: 102260 RVA: 0x000F4344 File Offset: 0x000F3344
		public override void add_onpropertychange(HTMLInputTextElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F75 RID: 102261 RVA: 0x000F43D4 File Offset: 0x000F33D4
		public override void remove_onpropertychange(HTMLInputTextElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F76 RID: 102262 RVA: 0x000F44C4 File Offset: 0x000F34C4
		public override void add_onlosecapture(HTMLInputTextElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F77 RID: 102263 RVA: 0x000F4554 File Offset: 0x000F3554
		public override void remove_onlosecapture(HTMLInputTextElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F78 RID: 102264 RVA: 0x000F4644 File Offset: 0x000F3644
		public override void add_ondatasetcomplete(HTMLInputTextElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F79 RID: 102265 RVA: 0x000F46D4 File Offset: 0x000F36D4
		public override void remove_ondatasetcomplete(HTMLInputTextElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F7A RID: 102266 RVA: 0x000F47C4 File Offset: 0x000F37C4
		public override void add_ondataavailable(HTMLInputTextElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F7B RID: 102267 RVA: 0x000F4854 File Offset: 0x000F3854
		public override void remove_ondataavailable(HTMLInputTextElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F7C RID: 102268 RVA: 0x000F4944 File Offset: 0x000F3944
		public override void add_ondatasetchanged(HTMLInputTextElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F7D RID: 102269 RVA: 0x000F49D4 File Offset: 0x000F39D4
		public override void remove_ondatasetchanged(HTMLInputTextElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F7E RID: 102270 RVA: 0x000F4AC4 File Offset: 0x000F3AC4
		public override void add_onrowenter(HTMLInputTextElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F7F RID: 102271 RVA: 0x000F4B54 File Offset: 0x000F3B54
		public override void remove_onrowenter(HTMLInputTextElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F80 RID: 102272 RVA: 0x000F4C44 File Offset: 0x000F3C44
		public override void add_onrowexit(HTMLInputTextElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F81 RID: 102273 RVA: 0x000F4CD4 File Offset: 0x000F3CD4
		public override void remove_onrowexit(HTMLInputTextElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F82 RID: 102274 RVA: 0x000F4DC4 File Offset: 0x000F3DC4
		public override void add_onerrorupdate(HTMLInputTextElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F83 RID: 102275 RVA: 0x000F4E54 File Offset: 0x000F3E54
		public override void remove_onerrorupdate(HTMLInputTextElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F84 RID: 102276 RVA: 0x000F4F44 File Offset: 0x000F3F44
		public override void add_onafterupdate(HTMLInputTextElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F85 RID: 102277 RVA: 0x000F4FD4 File Offset: 0x000F3FD4
		public override void remove_onafterupdate(HTMLInputTextElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F86 RID: 102278 RVA: 0x000F50C4 File Offset: 0x000F40C4
		public override void add_onbeforeupdate(HTMLInputTextElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F87 RID: 102279 RVA: 0x000F5154 File Offset: 0x000F4154
		public override void remove_onbeforeupdate(HTMLInputTextElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F88 RID: 102280 RVA: 0x000F5244 File Offset: 0x000F4244
		public override void add_ondragstart(HTMLInputTextElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F89 RID: 102281 RVA: 0x000F52D4 File Offset: 0x000F42D4
		public override void remove_ondragstart(HTMLInputTextElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F8A RID: 102282 RVA: 0x000F53C4 File Offset: 0x000F43C4
		public override void add_onfilterchange(HTMLInputTextElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F8B RID: 102283 RVA: 0x000F5454 File Offset: 0x000F4454
		public override void remove_onfilterchange(HTMLInputTextElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F8C RID: 102284 RVA: 0x000F5544 File Offset: 0x000F4544
		public override void add_onselectstart(HTMLInputTextElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F8D RID: 102285 RVA: 0x000F55D4 File Offset: 0x000F45D4
		public override void remove_onselectstart(HTMLInputTextElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F8E RID: 102286 RVA: 0x000F56C4 File Offset: 0x000F46C4
		public override void add_onmouseup(HTMLInputTextElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F8F RID: 102287 RVA: 0x000F5754 File Offset: 0x000F4754
		public override void remove_onmouseup(HTMLInputTextElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F90 RID: 102288 RVA: 0x000F5844 File Offset: 0x000F4844
		public override void add_onmousedown(HTMLInputTextElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F91 RID: 102289 RVA: 0x000F58D4 File Offset: 0x000F48D4
		public override void remove_onmousedown(HTMLInputTextElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F92 RID: 102290 RVA: 0x000F59C4 File Offset: 0x000F49C4
		public override void add_onmousemove(HTMLInputTextElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F93 RID: 102291 RVA: 0x000F5A54 File Offset: 0x000F4A54
		public override void remove_onmousemove(HTMLInputTextElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F94 RID: 102292 RVA: 0x000F5B44 File Offset: 0x000F4B44
		public override void add_onmouseover(HTMLInputTextElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F95 RID: 102293 RVA: 0x000F5BD4 File Offset: 0x000F4BD4
		public override void remove_onmouseover(HTMLInputTextElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F96 RID: 102294 RVA: 0x000F5CC4 File Offset: 0x000F4CC4
		public override void add_onmouseout(HTMLInputTextElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F97 RID: 102295 RVA: 0x000F5D54 File Offset: 0x000F4D54
		public override void remove_onmouseout(HTMLInputTextElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F98 RID: 102296 RVA: 0x000F5E44 File Offset: 0x000F4E44
		public override void add_onkeyup(HTMLInputTextElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F99 RID: 102297 RVA: 0x000F5ED4 File Offset: 0x000F4ED4
		public override void remove_onkeyup(HTMLInputTextElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F9A RID: 102298 RVA: 0x000F5FC4 File Offset: 0x000F4FC4
		public override void add_onkeydown(HTMLInputTextElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F9B RID: 102299 RVA: 0x000F6054 File Offset: 0x000F5054
		public override void remove_onkeydown(HTMLInputTextElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F9C RID: 102300 RVA: 0x000F6144 File Offset: 0x000F5144
		public override void add_onkeypress(HTMLInputTextElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F9D RID: 102301 RVA: 0x000F61D4 File Offset: 0x000F51D4
		public override void remove_onkeypress(HTMLInputTextElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018F9E RID: 102302 RVA: 0x000F62C4 File Offset: 0x000F52C4
		public override void add_ondblclick(HTMLInputTextElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018F9F RID: 102303 RVA: 0x000F6354 File Offset: 0x000F5354
		public override void remove_ondblclick(HTMLInputTextElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018FA0 RID: 102304 RVA: 0x000F6444 File Offset: 0x000F5444
		public override void add_onclick(HTMLInputTextElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018FA1 RID: 102305 RVA: 0x000F64D4 File Offset: 0x000F54D4
		public override void remove_onclick(HTMLInputTextElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onclickDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018FA2 RID: 102306 RVA: 0x000F65C4 File Offset: 0x000F55C4
		public override void add_onhelp(HTMLInputTextElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = new HTMLInputTextElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputTextElementEvents2_SinkHelper, ref num);
				htmlinputTextElementEvents2_SinkHelper.m_dwCookie = num;
				htmlinputTextElementEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputTextElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018FA3 RID: 102307 RVA: 0x000F6654 File Offset: 0x000F5654
		public override void remove_onhelp(HTMLInputTextElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper;
					for (;;)
					{
						htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputTextElementEvents2_SinkHelper.m_onhelpDelegate != null && ((htmlinputTextElementEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018FA4 RID: 102308 RVA: 0x000F6744 File Offset: 0x000F5744
		public HTMLInputTextElementEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06018FA5 RID: 102309 RVA: 0x000F676C File Offset: 0x000F576C
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
								HTMLInputTextElementEvents2_SinkHelper htmlinputTextElementEvents2_SinkHelper = (HTMLInputTextElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlinputTextElementEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x06018FA6 RID: 102310 RVA: 0x000F6820 File Offset: 0x000F5820
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000E05 RID: 3589
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000E06 RID: 3590
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000E07 RID: 3591
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
