using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DFF RID: 3583
	internal sealed class HTMLButtonElementEvents_EventProvider : HTMLButtonElementEvents_Event, IDisposable
	{
		// Token: 0x06018A66 RID: 100966 RVA: 0x000C5398 File Offset: 0x000C4398
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				179, 242, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06018A67 RID: 100967 RVA: 0x000C54AC File Offset: 0x000C44AC
		public override void add_onfocusout(HTMLButtonElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A68 RID: 100968 RVA: 0x000C553C File Offset: 0x000C453C
		public override void remove_onfocusout(HTMLButtonElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A69 RID: 100969 RVA: 0x000C562C File Offset: 0x000C462C
		public override void add_onfocusin(HTMLButtonElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A6A RID: 100970 RVA: 0x000C56BC File Offset: 0x000C46BC
		public override void remove_onfocusin(HTMLButtonElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onfocusinDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A6B RID: 100971 RVA: 0x000C57AC File Offset: 0x000C47AC
		public override void add_ondeactivate(HTMLButtonElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A6C RID: 100972 RVA: 0x000C583C File Offset: 0x000C483C
		public override void remove_ondeactivate(HTMLButtonElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A6D RID: 100973 RVA: 0x000C592C File Offset: 0x000C492C
		public override void add_onactivate(HTMLButtonElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A6E RID: 100974 RVA: 0x000C59BC File Offset: 0x000C49BC
		public override void remove_onactivate(HTMLButtonElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onactivateDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A6F RID: 100975 RVA: 0x000C5AAC File Offset: 0x000C4AAC
		public override void add_onmousewheel(HTMLButtonElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A70 RID: 100976 RVA: 0x000C5B3C File Offset: 0x000C4B3C
		public override void remove_onmousewheel(HTMLButtonElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A71 RID: 100977 RVA: 0x000C5C2C File Offset: 0x000C4C2C
		public override void add_onmouseleave(HTMLButtonElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A72 RID: 100978 RVA: 0x000C5CBC File Offset: 0x000C4CBC
		public override void remove_onmouseleave(HTMLButtonElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A73 RID: 100979 RVA: 0x000C5DAC File Offset: 0x000C4DAC
		public override void add_onmouseenter(HTMLButtonElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A74 RID: 100980 RVA: 0x000C5E3C File Offset: 0x000C4E3C
		public override void remove_onmouseenter(HTMLButtonElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A75 RID: 100981 RVA: 0x000C5F2C File Offset: 0x000C4F2C
		public override void add_onresizeend(HTMLButtonElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A76 RID: 100982 RVA: 0x000C5FBC File Offset: 0x000C4FBC
		public override void remove_onresizeend(HTMLButtonElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onresizeendDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A77 RID: 100983 RVA: 0x000C60AC File Offset: 0x000C50AC
		public override void add_onresizestart(HTMLButtonElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A78 RID: 100984 RVA: 0x000C613C File Offset: 0x000C513C
		public override void remove_onresizestart(HTMLButtonElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onresizestartDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A79 RID: 100985 RVA: 0x000C622C File Offset: 0x000C522C
		public override void add_onmoveend(HTMLButtonElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A7A RID: 100986 RVA: 0x000C62BC File Offset: 0x000C52BC
		public override void remove_onmoveend(HTMLButtonElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onmoveendDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A7B RID: 100987 RVA: 0x000C63AC File Offset: 0x000C53AC
		public override void add_onmovestart(HTMLButtonElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A7C RID: 100988 RVA: 0x000C643C File Offset: 0x000C543C
		public override void remove_onmovestart(HTMLButtonElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onmovestartDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A7D RID: 100989 RVA: 0x000C652C File Offset: 0x000C552C
		public override void add_oncontrolselect(HTMLButtonElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A7E RID: 100990 RVA: 0x000C65BC File Offset: 0x000C55BC
		public override void remove_oncontrolselect(HTMLButtonElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A7F RID: 100991 RVA: 0x000C66AC File Offset: 0x000C56AC
		public override void add_onmove(HTMLButtonElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A80 RID: 100992 RVA: 0x000C673C File Offset: 0x000C573C
		public override void remove_onmove(HTMLButtonElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onmoveDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A81 RID: 100993 RVA: 0x000C682C File Offset: 0x000C582C
		public override void add_onbeforeactivate(HTMLButtonElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A82 RID: 100994 RVA: 0x000C68BC File Offset: 0x000C58BC
		public override void remove_onbeforeactivate(HTMLButtonElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A83 RID: 100995 RVA: 0x000C69AC File Offset: 0x000C59AC
		public override void add_onbeforedeactivate(HTMLButtonElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A84 RID: 100996 RVA: 0x000C6A3C File Offset: 0x000C5A3C
		public override void remove_onbeforedeactivate(HTMLButtonElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A85 RID: 100997 RVA: 0x000C6B2C File Offset: 0x000C5B2C
		public override void add_onpage(HTMLButtonElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A86 RID: 100998 RVA: 0x000C6BBC File Offset: 0x000C5BBC
		public override void remove_onpage(HTMLButtonElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onpageDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A87 RID: 100999 RVA: 0x000C6CAC File Offset: 0x000C5CAC
		public override void add_onlayoutcomplete(HTMLButtonElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A88 RID: 101000 RVA: 0x000C6D3C File Offset: 0x000C5D3C
		public override void remove_onlayoutcomplete(HTMLButtonElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A89 RID: 101001 RVA: 0x000C6E2C File Offset: 0x000C5E2C
		public override void add_onbeforeeditfocus(HTMLButtonElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A8A RID: 101002 RVA: 0x000C6EBC File Offset: 0x000C5EBC
		public override void remove_onbeforeeditfocus(HTMLButtonElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A8B RID: 101003 RVA: 0x000C6FAC File Offset: 0x000C5FAC
		public override void add_onreadystatechange(HTMLButtonElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A8C RID: 101004 RVA: 0x000C703C File Offset: 0x000C603C
		public override void remove_onreadystatechange(HTMLButtonElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A8D RID: 101005 RVA: 0x000C712C File Offset: 0x000C612C
		public override void add_oncellchange(HTMLButtonElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A8E RID: 101006 RVA: 0x000C71BC File Offset: 0x000C61BC
		public override void remove_oncellchange(HTMLButtonElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A8F RID: 101007 RVA: 0x000C72AC File Offset: 0x000C62AC
		public override void add_onrowsinserted(HTMLButtonElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A90 RID: 101008 RVA: 0x000C733C File Offset: 0x000C633C
		public override void remove_onrowsinserted(HTMLButtonElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A91 RID: 101009 RVA: 0x000C742C File Offset: 0x000C642C
		public override void add_onrowsdelete(HTMLButtonElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A92 RID: 101010 RVA: 0x000C74BC File Offset: 0x000C64BC
		public override void remove_onrowsdelete(HTMLButtonElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A93 RID: 101011 RVA: 0x000C75AC File Offset: 0x000C65AC
		public override void add_oncontextmenu(HTMLButtonElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A94 RID: 101012 RVA: 0x000C763C File Offset: 0x000C663C
		public override void remove_oncontextmenu(HTMLButtonElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A95 RID: 101013 RVA: 0x000C772C File Offset: 0x000C672C
		public override void add_onpaste(HTMLButtonElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A96 RID: 101014 RVA: 0x000C77BC File Offset: 0x000C67BC
		public override void remove_onpaste(HTMLButtonElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onpasteDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A97 RID: 101015 RVA: 0x000C78AC File Offset: 0x000C68AC
		public override void add_onbeforepaste(HTMLButtonElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A98 RID: 101016 RVA: 0x000C793C File Offset: 0x000C693C
		public override void remove_onbeforepaste(HTMLButtonElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A99 RID: 101017 RVA: 0x000C7A2C File Offset: 0x000C6A2C
		public override void add_oncopy(HTMLButtonElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A9A RID: 101018 RVA: 0x000C7ABC File Offset: 0x000C6ABC
		public override void remove_oncopy(HTMLButtonElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_oncopyDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A9B RID: 101019 RVA: 0x000C7BAC File Offset: 0x000C6BAC
		public override void add_onbeforecopy(HTMLButtonElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A9C RID: 101020 RVA: 0x000C7C3C File Offset: 0x000C6C3C
		public override void remove_onbeforecopy(HTMLButtonElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A9D RID: 101021 RVA: 0x000C7D2C File Offset: 0x000C6D2C
		public override void add_oncut(HTMLButtonElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018A9E RID: 101022 RVA: 0x000C7DBC File Offset: 0x000C6DBC
		public override void remove_oncut(HTMLButtonElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_oncutDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A9F RID: 101023 RVA: 0x000C7EAC File Offset: 0x000C6EAC
		public override void add_onbeforecut(HTMLButtonElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AA0 RID: 101024 RVA: 0x000C7F3C File Offset: 0x000C6F3C
		public override void remove_onbeforecut(HTMLButtonElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AA1 RID: 101025 RVA: 0x000C802C File Offset: 0x000C702C
		public override void add_ondrop(HTMLButtonElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AA2 RID: 101026 RVA: 0x000C80BC File Offset: 0x000C70BC
		public override void remove_ondrop(HTMLButtonElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_ondropDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AA3 RID: 101027 RVA: 0x000C81AC File Offset: 0x000C71AC
		public override void add_ondragleave(HTMLButtonElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AA4 RID: 101028 RVA: 0x000C823C File Offset: 0x000C723C
		public override void remove_ondragleave(HTMLButtonElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AA5 RID: 101029 RVA: 0x000C832C File Offset: 0x000C732C
		public override void add_ondragover(HTMLButtonElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AA6 RID: 101030 RVA: 0x000C83BC File Offset: 0x000C73BC
		public override void remove_ondragover(HTMLButtonElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_ondragoverDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AA7 RID: 101031 RVA: 0x000C84AC File Offset: 0x000C74AC
		public override void add_ondragenter(HTMLButtonElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AA8 RID: 101032 RVA: 0x000C853C File Offset: 0x000C753C
		public override void remove_ondragenter(HTMLButtonElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_ondragenterDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AA9 RID: 101033 RVA: 0x000C862C File Offset: 0x000C762C
		public override void add_ondragend(HTMLButtonElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AAA RID: 101034 RVA: 0x000C86BC File Offset: 0x000C76BC
		public override void remove_ondragend(HTMLButtonElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_ondragendDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AAB RID: 101035 RVA: 0x000C87AC File Offset: 0x000C77AC
		public override void add_ondrag(HTMLButtonElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AAC RID: 101036 RVA: 0x000C883C File Offset: 0x000C783C
		public override void remove_ondrag(HTMLButtonElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_ondragDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AAD RID: 101037 RVA: 0x000C892C File Offset: 0x000C792C
		public override void add_onresize(HTMLButtonElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AAE RID: 101038 RVA: 0x000C89BC File Offset: 0x000C79BC
		public override void remove_onresize(HTMLButtonElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onresizeDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AAF RID: 101039 RVA: 0x000C8AAC File Offset: 0x000C7AAC
		public override void add_onblur(HTMLButtonElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AB0 RID: 101040 RVA: 0x000C8B3C File Offset: 0x000C7B3C
		public override void remove_onblur(HTMLButtonElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onblurDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AB1 RID: 101041 RVA: 0x000C8C2C File Offset: 0x000C7C2C
		public override void add_onfocus(HTMLButtonElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AB2 RID: 101042 RVA: 0x000C8CBC File Offset: 0x000C7CBC
		public override void remove_onfocus(HTMLButtonElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onfocusDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AB3 RID: 101043 RVA: 0x000C8DAC File Offset: 0x000C7DAC
		public override void add_onscroll(HTMLButtonElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AB4 RID: 101044 RVA: 0x000C8E3C File Offset: 0x000C7E3C
		public override void remove_onscroll(HTMLButtonElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onscrollDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AB5 RID: 101045 RVA: 0x000C8F2C File Offset: 0x000C7F2C
		public override void add_onpropertychange(HTMLButtonElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AB6 RID: 101046 RVA: 0x000C8FBC File Offset: 0x000C7FBC
		public override void remove_onpropertychange(HTMLButtonElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AB7 RID: 101047 RVA: 0x000C90AC File Offset: 0x000C80AC
		public override void add_onlosecapture(HTMLButtonElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AB8 RID: 101048 RVA: 0x000C913C File Offset: 0x000C813C
		public override void remove_onlosecapture(HTMLButtonElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AB9 RID: 101049 RVA: 0x000C922C File Offset: 0x000C822C
		public override void add_ondatasetcomplete(HTMLButtonElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018ABA RID: 101050 RVA: 0x000C92BC File Offset: 0x000C82BC
		public override void remove_ondatasetcomplete(HTMLButtonElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018ABB RID: 101051 RVA: 0x000C93AC File Offset: 0x000C83AC
		public override void add_ondataavailable(HTMLButtonElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018ABC RID: 101052 RVA: 0x000C943C File Offset: 0x000C843C
		public override void remove_ondataavailable(HTMLButtonElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018ABD RID: 101053 RVA: 0x000C952C File Offset: 0x000C852C
		public override void add_ondatasetchanged(HTMLButtonElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018ABE RID: 101054 RVA: 0x000C95BC File Offset: 0x000C85BC
		public override void remove_ondatasetchanged(HTMLButtonElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018ABF RID: 101055 RVA: 0x000C96AC File Offset: 0x000C86AC
		public override void add_onrowenter(HTMLButtonElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AC0 RID: 101056 RVA: 0x000C973C File Offset: 0x000C873C
		public override void remove_onrowenter(HTMLButtonElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AC1 RID: 101057 RVA: 0x000C982C File Offset: 0x000C882C
		public override void add_onrowexit(HTMLButtonElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AC2 RID: 101058 RVA: 0x000C98BC File Offset: 0x000C88BC
		public override void remove_onrowexit(HTMLButtonElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AC3 RID: 101059 RVA: 0x000C99AC File Offset: 0x000C89AC
		public override void add_onerrorupdate(HTMLButtonElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AC4 RID: 101060 RVA: 0x000C9A3C File Offset: 0x000C8A3C
		public override void remove_onerrorupdate(HTMLButtonElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AC5 RID: 101061 RVA: 0x000C9B2C File Offset: 0x000C8B2C
		public override void add_onafterupdate(HTMLButtonElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AC6 RID: 101062 RVA: 0x000C9BBC File Offset: 0x000C8BBC
		public override void remove_onafterupdate(HTMLButtonElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AC7 RID: 101063 RVA: 0x000C9CAC File Offset: 0x000C8CAC
		public override void add_onbeforeupdate(HTMLButtonElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AC8 RID: 101064 RVA: 0x000C9D3C File Offset: 0x000C8D3C
		public override void remove_onbeforeupdate(HTMLButtonElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AC9 RID: 101065 RVA: 0x000C9E2C File Offset: 0x000C8E2C
		public override void add_ondragstart(HTMLButtonElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018ACA RID: 101066 RVA: 0x000C9EBC File Offset: 0x000C8EBC
		public override void remove_ondragstart(HTMLButtonElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_ondragstartDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018ACB RID: 101067 RVA: 0x000C9FAC File Offset: 0x000C8FAC
		public override void add_onfilterchange(HTMLButtonElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018ACC RID: 101068 RVA: 0x000CA03C File Offset: 0x000C903C
		public override void remove_onfilterchange(HTMLButtonElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018ACD RID: 101069 RVA: 0x000CA12C File Offset: 0x000C912C
		public override void add_onselectstart(HTMLButtonElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018ACE RID: 101070 RVA: 0x000CA1BC File Offset: 0x000C91BC
		public override void remove_onselectstart(HTMLButtonElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onselectstartDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018ACF RID: 101071 RVA: 0x000CA2AC File Offset: 0x000C92AC
		public override void add_onmouseup(HTMLButtonElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AD0 RID: 101072 RVA: 0x000CA33C File Offset: 0x000C933C
		public override void remove_onmouseup(HTMLButtonElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onmouseupDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AD1 RID: 101073 RVA: 0x000CA42C File Offset: 0x000C942C
		public override void add_onmousedown(HTMLButtonElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AD2 RID: 101074 RVA: 0x000CA4BC File Offset: 0x000C94BC
		public override void remove_onmousedown(HTMLButtonElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onmousedownDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AD3 RID: 101075 RVA: 0x000CA5AC File Offset: 0x000C95AC
		public override void add_onmousemove(HTMLButtonElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AD4 RID: 101076 RVA: 0x000CA63C File Offset: 0x000C963C
		public override void remove_onmousemove(HTMLButtonElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AD5 RID: 101077 RVA: 0x000CA72C File Offset: 0x000C972C
		public override void add_onmouseover(HTMLButtonElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AD6 RID: 101078 RVA: 0x000CA7BC File Offset: 0x000C97BC
		public override void remove_onmouseover(HTMLButtonElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AD7 RID: 101079 RVA: 0x000CA8AC File Offset: 0x000C98AC
		public override void add_onmouseout(HTMLButtonElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AD8 RID: 101080 RVA: 0x000CA93C File Offset: 0x000C993C
		public override void remove_onmouseout(HTMLButtonElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AD9 RID: 101081 RVA: 0x000CAA2C File Offset: 0x000C9A2C
		public override void add_onkeyup(HTMLButtonElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018ADA RID: 101082 RVA: 0x000CAABC File Offset: 0x000C9ABC
		public override void remove_onkeyup(HTMLButtonElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onkeyupDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018ADB RID: 101083 RVA: 0x000CABAC File Offset: 0x000C9BAC
		public override void add_onkeydown(HTMLButtonElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018ADC RID: 101084 RVA: 0x000CAC3C File Offset: 0x000C9C3C
		public override void remove_onkeydown(HTMLButtonElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onkeydownDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018ADD RID: 101085 RVA: 0x000CAD2C File Offset: 0x000C9D2C
		public override void add_onkeypress(HTMLButtonElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018ADE RID: 101086 RVA: 0x000CADBC File Offset: 0x000C9DBC
		public override void remove_onkeypress(HTMLButtonElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onkeypressDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018ADF RID: 101087 RVA: 0x000CAEAC File Offset: 0x000C9EAC
		public override void add_ondblclick(HTMLButtonElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AE0 RID: 101088 RVA: 0x000CAF3C File Offset: 0x000C9F3C
		public override void remove_ondblclick(HTMLButtonElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_ondblclickDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AE1 RID: 101089 RVA: 0x000CB02C File Offset: 0x000CA02C
		public override void add_onclick(HTMLButtonElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AE2 RID: 101090 RVA: 0x000CB0BC File Offset: 0x000CA0BC
		public override void remove_onclick(HTMLButtonElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onclickDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AE3 RID: 101091 RVA: 0x000CB1AC File Offset: 0x000CA1AC
		public override void add_onhelp(HTMLButtonElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = new HTMLButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents_SinkHelper, ref num);
				htmlbuttonElementEvents_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018AE4 RID: 101092 RVA: 0x000CB23C File Offset: 0x000CA23C
		public override void remove_onhelp(HTMLButtonElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents_SinkHelper.m_onhelpDelegate != null && ((htmlbuttonElementEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018AE5 RID: 101093 RVA: 0x000CB32C File Offset: 0x000CA32C
		public HTMLButtonElementEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06018AE6 RID: 101094 RVA: 0x000CB354 File Offset: 0x000CA354
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
								HTMLButtonElementEvents_SinkHelper htmlbuttonElementEvents_SinkHelper = (HTMLButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x06018AE7 RID: 101095 RVA: 0x000CB408 File Offset: 0x000CA408
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000C62 RID: 3170
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000C63 RID: 3171
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000C64 RID: 3172
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
