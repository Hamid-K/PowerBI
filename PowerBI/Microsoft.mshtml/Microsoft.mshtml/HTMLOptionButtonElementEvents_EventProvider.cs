using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DEF RID: 3567
	internal sealed class HTMLOptionButtonElementEvents_EventProvider : HTMLOptionButtonElementEvents_Event, IDisposable
	{
		// Token: 0x060184E2 RID: 99554 RVA: 0x00092E88 File Offset: 0x00091E88
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				189, 242, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x060184E3 RID: 99555 RVA: 0x00092F9C File Offset: 0x00091F9C
		public override void add_onabort(HTMLOptionButtonElementEvents_onabortEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onabortDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x060184E4 RID: 99556 RVA: 0x0009302C File Offset: 0x0009202C
		public override void remove_onabort(HTMLOptionButtonElementEvents_onabortEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onabortDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onabortDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060184E5 RID: 99557 RVA: 0x0009311C File Offset: 0x0009211C
		public override void add_onerror(HTMLOptionButtonElementEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x060184E6 RID: 99558 RVA: 0x000931AC File Offset: 0x000921AC
		public override void remove_onerror(HTMLOptionButtonElementEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onerrorDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060184E7 RID: 99559 RVA: 0x0009329C File Offset: 0x0009229C
		public override void add_onload(HTMLOptionButtonElementEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x060184E8 RID: 99560 RVA: 0x0009332C File Offset: 0x0009232C
		public override void remove_onload(HTMLOptionButtonElementEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onloadDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060184E9 RID: 99561 RVA: 0x0009341C File Offset: 0x0009241C
		public override void add_onselect(HTMLOptionButtonElementEvents_onselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x060184EA RID: 99562 RVA: 0x000934AC File Offset: 0x000924AC
		public override void remove_onselect(HTMLOptionButtonElementEvents_onselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onselectDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060184EB RID: 99563 RVA: 0x0009359C File Offset: 0x0009259C
		public override void add_onchange(HTMLOptionButtonElementEvents_onchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x060184EC RID: 99564 RVA: 0x0009362C File Offset: 0x0009262C
		public override void remove_onchange(HTMLOptionButtonElementEvents_onchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onchangeDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060184ED RID: 99565 RVA: 0x0009371C File Offset: 0x0009271C
		public override void add_onfocusout(HTMLOptionButtonElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x060184EE RID: 99566 RVA: 0x000937AC File Offset: 0x000927AC
		public override void remove_onfocusout(HTMLOptionButtonElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060184EF RID: 99567 RVA: 0x0009389C File Offset: 0x0009289C
		public override void add_onfocusin(HTMLOptionButtonElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x060184F0 RID: 99568 RVA: 0x0009392C File Offset: 0x0009292C
		public override void remove_onfocusin(HTMLOptionButtonElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onfocusinDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060184F1 RID: 99569 RVA: 0x00093A1C File Offset: 0x00092A1C
		public override void add_ondeactivate(HTMLOptionButtonElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x060184F2 RID: 99570 RVA: 0x00093AAC File Offset: 0x00092AAC
		public override void remove_ondeactivate(HTMLOptionButtonElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060184F3 RID: 99571 RVA: 0x00093B9C File Offset: 0x00092B9C
		public override void add_onactivate(HTMLOptionButtonElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x060184F4 RID: 99572 RVA: 0x00093C2C File Offset: 0x00092C2C
		public override void remove_onactivate(HTMLOptionButtonElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onactivateDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060184F5 RID: 99573 RVA: 0x00093D1C File Offset: 0x00092D1C
		public override void add_onmousewheel(HTMLOptionButtonElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x060184F6 RID: 99574 RVA: 0x00093DAC File Offset: 0x00092DAC
		public override void remove_onmousewheel(HTMLOptionButtonElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060184F7 RID: 99575 RVA: 0x00093E9C File Offset: 0x00092E9C
		public override void add_onmouseleave(HTMLOptionButtonElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x060184F8 RID: 99576 RVA: 0x00093F2C File Offset: 0x00092F2C
		public override void remove_onmouseleave(HTMLOptionButtonElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060184F9 RID: 99577 RVA: 0x0009401C File Offset: 0x0009301C
		public override void add_onmouseenter(HTMLOptionButtonElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x060184FA RID: 99578 RVA: 0x000940AC File Offset: 0x000930AC
		public override void remove_onmouseenter(HTMLOptionButtonElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060184FB RID: 99579 RVA: 0x0009419C File Offset: 0x0009319C
		public override void add_onresizeend(HTMLOptionButtonElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x060184FC RID: 99580 RVA: 0x0009422C File Offset: 0x0009322C
		public override void remove_onresizeend(HTMLOptionButtonElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onresizeendDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060184FD RID: 99581 RVA: 0x0009431C File Offset: 0x0009331C
		public override void add_onresizestart(HTMLOptionButtonElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x060184FE RID: 99582 RVA: 0x000943AC File Offset: 0x000933AC
		public override void remove_onresizestart(HTMLOptionButtonElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onresizestartDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060184FF RID: 99583 RVA: 0x0009449C File Offset: 0x0009349C
		public override void add_onmoveend(HTMLOptionButtonElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018500 RID: 99584 RVA: 0x0009452C File Offset: 0x0009352C
		public override void remove_onmoveend(HTMLOptionButtonElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onmoveendDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018501 RID: 99585 RVA: 0x0009461C File Offset: 0x0009361C
		public override void add_onmovestart(HTMLOptionButtonElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018502 RID: 99586 RVA: 0x000946AC File Offset: 0x000936AC
		public override void remove_onmovestart(HTMLOptionButtonElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onmovestartDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018503 RID: 99587 RVA: 0x0009479C File Offset: 0x0009379C
		public override void add_oncontrolselect(HTMLOptionButtonElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018504 RID: 99588 RVA: 0x0009482C File Offset: 0x0009382C
		public override void remove_oncontrolselect(HTMLOptionButtonElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018505 RID: 99589 RVA: 0x0009491C File Offset: 0x0009391C
		public override void add_onmove(HTMLOptionButtonElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018506 RID: 99590 RVA: 0x000949AC File Offset: 0x000939AC
		public override void remove_onmove(HTMLOptionButtonElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onmoveDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018507 RID: 99591 RVA: 0x00094A9C File Offset: 0x00093A9C
		public override void add_onbeforeactivate(HTMLOptionButtonElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018508 RID: 99592 RVA: 0x00094B2C File Offset: 0x00093B2C
		public override void remove_onbeforeactivate(HTMLOptionButtonElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018509 RID: 99593 RVA: 0x00094C1C File Offset: 0x00093C1C
		public override void add_onbeforedeactivate(HTMLOptionButtonElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601850A RID: 99594 RVA: 0x00094CAC File Offset: 0x00093CAC
		public override void remove_onbeforedeactivate(HTMLOptionButtonElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601850B RID: 99595 RVA: 0x00094D9C File Offset: 0x00093D9C
		public override void add_onpage(HTMLOptionButtonElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601850C RID: 99596 RVA: 0x00094E2C File Offset: 0x00093E2C
		public override void remove_onpage(HTMLOptionButtonElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onpageDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601850D RID: 99597 RVA: 0x00094F1C File Offset: 0x00093F1C
		public override void add_onlayoutcomplete(HTMLOptionButtonElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601850E RID: 99598 RVA: 0x00094FAC File Offset: 0x00093FAC
		public override void remove_onlayoutcomplete(HTMLOptionButtonElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601850F RID: 99599 RVA: 0x0009509C File Offset: 0x0009409C
		public override void add_onbeforeeditfocus(HTMLOptionButtonElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018510 RID: 99600 RVA: 0x0009512C File Offset: 0x0009412C
		public override void remove_onbeforeeditfocus(HTMLOptionButtonElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018511 RID: 99601 RVA: 0x0009521C File Offset: 0x0009421C
		public override void add_onreadystatechange(HTMLOptionButtonElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018512 RID: 99602 RVA: 0x000952AC File Offset: 0x000942AC
		public override void remove_onreadystatechange(HTMLOptionButtonElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018513 RID: 99603 RVA: 0x0009539C File Offset: 0x0009439C
		public override void add_oncellchange(HTMLOptionButtonElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018514 RID: 99604 RVA: 0x0009542C File Offset: 0x0009442C
		public override void remove_oncellchange(HTMLOptionButtonElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018515 RID: 99605 RVA: 0x0009551C File Offset: 0x0009451C
		public override void add_onrowsinserted(HTMLOptionButtonElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018516 RID: 99606 RVA: 0x000955AC File Offset: 0x000945AC
		public override void remove_onrowsinserted(HTMLOptionButtonElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018517 RID: 99607 RVA: 0x0009569C File Offset: 0x0009469C
		public override void add_onrowsdelete(HTMLOptionButtonElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018518 RID: 99608 RVA: 0x0009572C File Offset: 0x0009472C
		public override void remove_onrowsdelete(HTMLOptionButtonElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018519 RID: 99609 RVA: 0x0009581C File Offset: 0x0009481C
		public override void add_oncontextmenu(HTMLOptionButtonElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601851A RID: 99610 RVA: 0x000958AC File Offset: 0x000948AC
		public override void remove_oncontextmenu(HTMLOptionButtonElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601851B RID: 99611 RVA: 0x0009599C File Offset: 0x0009499C
		public override void add_onpaste(HTMLOptionButtonElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601851C RID: 99612 RVA: 0x00095A2C File Offset: 0x00094A2C
		public override void remove_onpaste(HTMLOptionButtonElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onpasteDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601851D RID: 99613 RVA: 0x00095B1C File Offset: 0x00094B1C
		public override void add_onbeforepaste(HTMLOptionButtonElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601851E RID: 99614 RVA: 0x00095BAC File Offset: 0x00094BAC
		public override void remove_onbeforepaste(HTMLOptionButtonElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601851F RID: 99615 RVA: 0x00095C9C File Offset: 0x00094C9C
		public override void add_oncopy(HTMLOptionButtonElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018520 RID: 99616 RVA: 0x00095D2C File Offset: 0x00094D2C
		public override void remove_oncopy(HTMLOptionButtonElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_oncopyDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018521 RID: 99617 RVA: 0x00095E1C File Offset: 0x00094E1C
		public override void add_onbeforecopy(HTMLOptionButtonElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018522 RID: 99618 RVA: 0x00095EAC File Offset: 0x00094EAC
		public override void remove_onbeforecopy(HTMLOptionButtonElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018523 RID: 99619 RVA: 0x00095F9C File Offset: 0x00094F9C
		public override void add_oncut(HTMLOptionButtonElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018524 RID: 99620 RVA: 0x0009602C File Offset: 0x0009502C
		public override void remove_oncut(HTMLOptionButtonElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_oncutDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018525 RID: 99621 RVA: 0x0009611C File Offset: 0x0009511C
		public override void add_onbeforecut(HTMLOptionButtonElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018526 RID: 99622 RVA: 0x000961AC File Offset: 0x000951AC
		public override void remove_onbeforecut(HTMLOptionButtonElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018527 RID: 99623 RVA: 0x0009629C File Offset: 0x0009529C
		public override void add_ondrop(HTMLOptionButtonElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018528 RID: 99624 RVA: 0x0009632C File Offset: 0x0009532C
		public override void remove_ondrop(HTMLOptionButtonElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_ondropDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018529 RID: 99625 RVA: 0x0009641C File Offset: 0x0009541C
		public override void add_ondragleave(HTMLOptionButtonElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601852A RID: 99626 RVA: 0x000964AC File Offset: 0x000954AC
		public override void remove_ondragleave(HTMLOptionButtonElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601852B RID: 99627 RVA: 0x0009659C File Offset: 0x0009559C
		public override void add_ondragover(HTMLOptionButtonElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601852C RID: 99628 RVA: 0x0009662C File Offset: 0x0009562C
		public override void remove_ondragover(HTMLOptionButtonElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_ondragoverDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601852D RID: 99629 RVA: 0x0009671C File Offset: 0x0009571C
		public override void add_ondragenter(HTMLOptionButtonElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601852E RID: 99630 RVA: 0x000967AC File Offset: 0x000957AC
		public override void remove_ondragenter(HTMLOptionButtonElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_ondragenterDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601852F RID: 99631 RVA: 0x0009689C File Offset: 0x0009589C
		public override void add_ondragend(HTMLOptionButtonElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018530 RID: 99632 RVA: 0x0009692C File Offset: 0x0009592C
		public override void remove_ondragend(HTMLOptionButtonElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_ondragendDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018531 RID: 99633 RVA: 0x00096A1C File Offset: 0x00095A1C
		public override void add_ondrag(HTMLOptionButtonElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018532 RID: 99634 RVA: 0x00096AAC File Offset: 0x00095AAC
		public override void remove_ondrag(HTMLOptionButtonElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_ondragDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018533 RID: 99635 RVA: 0x00096B9C File Offset: 0x00095B9C
		public override void add_onresize(HTMLOptionButtonElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018534 RID: 99636 RVA: 0x00096C2C File Offset: 0x00095C2C
		public override void remove_onresize(HTMLOptionButtonElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onresizeDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018535 RID: 99637 RVA: 0x00096D1C File Offset: 0x00095D1C
		public override void add_onblur(HTMLOptionButtonElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018536 RID: 99638 RVA: 0x00096DAC File Offset: 0x00095DAC
		public override void remove_onblur(HTMLOptionButtonElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onblurDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018537 RID: 99639 RVA: 0x00096E9C File Offset: 0x00095E9C
		public override void add_onfocus(HTMLOptionButtonElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018538 RID: 99640 RVA: 0x00096F2C File Offset: 0x00095F2C
		public override void remove_onfocus(HTMLOptionButtonElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onfocusDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018539 RID: 99641 RVA: 0x0009701C File Offset: 0x0009601C
		public override void add_onscroll(HTMLOptionButtonElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601853A RID: 99642 RVA: 0x000970AC File Offset: 0x000960AC
		public override void remove_onscroll(HTMLOptionButtonElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onscrollDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601853B RID: 99643 RVA: 0x0009719C File Offset: 0x0009619C
		public override void add_onpropertychange(HTMLOptionButtonElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601853C RID: 99644 RVA: 0x0009722C File Offset: 0x0009622C
		public override void remove_onpropertychange(HTMLOptionButtonElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601853D RID: 99645 RVA: 0x0009731C File Offset: 0x0009631C
		public override void add_onlosecapture(HTMLOptionButtonElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601853E RID: 99646 RVA: 0x000973AC File Offset: 0x000963AC
		public override void remove_onlosecapture(HTMLOptionButtonElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601853F RID: 99647 RVA: 0x0009749C File Offset: 0x0009649C
		public override void add_ondatasetcomplete(HTMLOptionButtonElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018540 RID: 99648 RVA: 0x0009752C File Offset: 0x0009652C
		public override void remove_ondatasetcomplete(HTMLOptionButtonElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018541 RID: 99649 RVA: 0x0009761C File Offset: 0x0009661C
		public override void add_ondataavailable(HTMLOptionButtonElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018542 RID: 99650 RVA: 0x000976AC File Offset: 0x000966AC
		public override void remove_ondataavailable(HTMLOptionButtonElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018543 RID: 99651 RVA: 0x0009779C File Offset: 0x0009679C
		public override void add_ondatasetchanged(HTMLOptionButtonElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018544 RID: 99652 RVA: 0x0009782C File Offset: 0x0009682C
		public override void remove_ondatasetchanged(HTMLOptionButtonElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018545 RID: 99653 RVA: 0x0009791C File Offset: 0x0009691C
		public override void add_onrowenter(HTMLOptionButtonElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018546 RID: 99654 RVA: 0x000979AC File Offset: 0x000969AC
		public override void remove_onrowenter(HTMLOptionButtonElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onrowenterDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018547 RID: 99655 RVA: 0x00097A9C File Offset: 0x00096A9C
		public override void add_onrowexit(HTMLOptionButtonElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018548 RID: 99656 RVA: 0x00097B2C File Offset: 0x00096B2C
		public override void remove_onrowexit(HTMLOptionButtonElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onrowexitDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018549 RID: 99657 RVA: 0x00097C1C File Offset: 0x00096C1C
		public override void add_onerrorupdate(HTMLOptionButtonElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601854A RID: 99658 RVA: 0x00097CAC File Offset: 0x00096CAC
		public override void remove_onerrorupdate(HTMLOptionButtonElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601854B RID: 99659 RVA: 0x00097D9C File Offset: 0x00096D9C
		public override void add_onafterupdate(HTMLOptionButtonElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601854C RID: 99660 RVA: 0x00097E2C File Offset: 0x00096E2C
		public override void remove_onafterupdate(HTMLOptionButtonElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601854D RID: 99661 RVA: 0x00097F1C File Offset: 0x00096F1C
		public override void add_onbeforeupdate(HTMLOptionButtonElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601854E RID: 99662 RVA: 0x00097FAC File Offset: 0x00096FAC
		public override void remove_onbeforeupdate(HTMLOptionButtonElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601854F RID: 99663 RVA: 0x0009809C File Offset: 0x0009709C
		public override void add_ondragstart(HTMLOptionButtonElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018550 RID: 99664 RVA: 0x0009812C File Offset: 0x0009712C
		public override void remove_ondragstart(HTMLOptionButtonElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_ondragstartDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018551 RID: 99665 RVA: 0x0009821C File Offset: 0x0009721C
		public override void add_onfilterchange(HTMLOptionButtonElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018552 RID: 99666 RVA: 0x000982AC File Offset: 0x000972AC
		public override void remove_onfilterchange(HTMLOptionButtonElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018553 RID: 99667 RVA: 0x0009839C File Offset: 0x0009739C
		public override void add_onselectstart(HTMLOptionButtonElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018554 RID: 99668 RVA: 0x0009842C File Offset: 0x0009742C
		public override void remove_onselectstart(HTMLOptionButtonElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onselectstartDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018555 RID: 99669 RVA: 0x0009851C File Offset: 0x0009751C
		public override void add_onmouseup(HTMLOptionButtonElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018556 RID: 99670 RVA: 0x000985AC File Offset: 0x000975AC
		public override void remove_onmouseup(HTMLOptionButtonElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onmouseupDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018557 RID: 99671 RVA: 0x0009869C File Offset: 0x0009769C
		public override void add_onmousedown(HTMLOptionButtonElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018558 RID: 99672 RVA: 0x0009872C File Offset: 0x0009772C
		public override void remove_onmousedown(HTMLOptionButtonElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onmousedownDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018559 RID: 99673 RVA: 0x0009881C File Offset: 0x0009781C
		public override void add_onmousemove(HTMLOptionButtonElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601855A RID: 99674 RVA: 0x000988AC File Offset: 0x000978AC
		public override void remove_onmousemove(HTMLOptionButtonElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601855B RID: 99675 RVA: 0x0009899C File Offset: 0x0009799C
		public override void add_onmouseover(HTMLOptionButtonElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601855C RID: 99676 RVA: 0x00098A2C File Offset: 0x00097A2C
		public override void remove_onmouseover(HTMLOptionButtonElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601855D RID: 99677 RVA: 0x00098B1C File Offset: 0x00097B1C
		public override void add_onmouseout(HTMLOptionButtonElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601855E RID: 99678 RVA: 0x00098BAC File Offset: 0x00097BAC
		public override void remove_onmouseout(HTMLOptionButtonElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601855F RID: 99679 RVA: 0x00098C9C File Offset: 0x00097C9C
		public override void add_onkeyup(HTMLOptionButtonElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018560 RID: 99680 RVA: 0x00098D2C File Offset: 0x00097D2C
		public override void remove_onkeyup(HTMLOptionButtonElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onkeyupDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018561 RID: 99681 RVA: 0x00098E1C File Offset: 0x00097E1C
		public override void add_onkeydown(HTMLOptionButtonElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018562 RID: 99682 RVA: 0x00098EAC File Offset: 0x00097EAC
		public override void remove_onkeydown(HTMLOptionButtonElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onkeydownDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018563 RID: 99683 RVA: 0x00098F9C File Offset: 0x00097F9C
		public override void add_onkeypress(HTMLOptionButtonElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018564 RID: 99684 RVA: 0x0009902C File Offset: 0x0009802C
		public override void remove_onkeypress(HTMLOptionButtonElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onkeypressDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018565 RID: 99685 RVA: 0x0009911C File Offset: 0x0009811C
		public override void add_ondblclick(HTMLOptionButtonElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018566 RID: 99686 RVA: 0x000991AC File Offset: 0x000981AC
		public override void remove_ondblclick(HTMLOptionButtonElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_ondblclickDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018567 RID: 99687 RVA: 0x0009929C File Offset: 0x0009829C
		public override void add_onclick(HTMLOptionButtonElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018568 RID: 99688 RVA: 0x0009932C File Offset: 0x0009832C
		public override void remove_onclick(HTMLOptionButtonElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onclickDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018569 RID: 99689 RVA: 0x0009941C File Offset: 0x0009841C
		public override void add_onhelp(HTMLOptionButtonElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = new HTMLOptionButtonElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmloptionButtonElementEvents_SinkHelper, ref num);
				htmloptionButtonElementEvents_SinkHelper.m_dwCookie = num;
				htmloptionButtonElementEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmloptionButtonElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601856A RID: 99690 RVA: 0x000994AC File Offset: 0x000984AC
		public override void remove_onhelp(HTMLOptionButtonElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper;
					for (;;)
					{
						htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmloptionButtonElementEvents_SinkHelper.m_onhelpDelegate != null && ((htmloptionButtonElementEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601856B RID: 99691 RVA: 0x0009959C File Offset: 0x0009859C
		public HTMLOptionButtonElementEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x0601856C RID: 99692 RVA: 0x000995C4 File Offset: 0x000985C4
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
								HTMLOptionButtonElementEvents_SinkHelper htmloptionButtonElementEvents_SinkHelper = (HTMLOptionButtonElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmloptionButtonElementEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x0601856D RID: 99693 RVA: 0x00099678 File Offset: 0x00098678
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000A7C RID: 2684
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000A7D RID: 2685
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000A7E RID: 2686
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
