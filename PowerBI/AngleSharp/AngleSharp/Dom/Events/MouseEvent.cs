using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001EE RID: 494
	[DomName("MouseEvent")]
	public class MouseEvent : UiEvent
	{
		// Token: 0x0600104B RID: 4171 RVA: 0x00046FAB File Offset: 0x000451AB
		public MouseEvent()
		{
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00047700 File Offset: 0x00045900
		[DomConstructor]
		[DomInitDict(1, true)]
		public MouseEvent(string type, bool bubbles = false, bool cancelable = false, IWindow view = null, int detail = 0, int screenX = 0, int screenY = 0, int clientX = 0, int clientY = 0, bool ctrlKey = false, bool altKey = false, bool shiftKey = false, bool metaKey = false, MouseButton button = MouseButton.Primary, IEventTarget relatedTarget = null)
		{
			this.Init(type, bubbles, cancelable, view, detail, screenX, screenY, clientX, clientY, ctrlKey, altKey, shiftKey, metaKey, button, relatedTarget);
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x00047734 File Offset: 0x00045934
		// (set) Token: 0x0600104E RID: 4174 RVA: 0x0004773C File Offset: 0x0004593C
		[DomName("screenX")]
		public int ScreenX { get; private set; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x00047745 File Offset: 0x00045945
		// (set) Token: 0x06001050 RID: 4176 RVA: 0x0004774D File Offset: 0x0004594D
		[DomName("screenY")]
		public int ScreenY { get; private set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x00047756 File Offset: 0x00045956
		// (set) Token: 0x06001052 RID: 4178 RVA: 0x0004775E File Offset: 0x0004595E
		[DomName("clientX")]
		public int ClientX { get; private set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x00047767 File Offset: 0x00045967
		// (set) Token: 0x06001054 RID: 4180 RVA: 0x0004776F File Offset: 0x0004596F
		[DomName("clientY")]
		public int ClientY { get; private set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x00047778 File Offset: 0x00045978
		// (set) Token: 0x06001056 RID: 4182 RVA: 0x00047780 File Offset: 0x00045980
		[DomName("ctrlKey")]
		public bool IsCtrlPressed { get; private set; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x00047789 File Offset: 0x00045989
		// (set) Token: 0x06001058 RID: 4184 RVA: 0x00047791 File Offset: 0x00045991
		[DomName("shiftKey")]
		public bool IsShiftPressed { get; private set; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x0004779A File Offset: 0x0004599A
		// (set) Token: 0x0600105A RID: 4186 RVA: 0x000477A2 File Offset: 0x000459A2
		[DomName("altKey")]
		public bool IsAltPressed { get; private set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x000477AB File Offset: 0x000459AB
		// (set) Token: 0x0600105C RID: 4188 RVA: 0x000477B3 File Offset: 0x000459B3
		[DomName("metaKey")]
		public bool IsMetaPressed { get; private set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x000477BC File Offset: 0x000459BC
		// (set) Token: 0x0600105E RID: 4190 RVA: 0x000477C4 File Offset: 0x000459C4
		[DomName("button")]
		public MouseButton Button { get; private set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x000477CD File Offset: 0x000459CD
		// (set) Token: 0x06001060 RID: 4192 RVA: 0x000477D5 File Offset: 0x000459D5
		[DomName("buttons")]
		public MouseButtons Buttons { get; private set; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x000477DE File Offset: 0x000459DE
		// (set) Token: 0x06001062 RID: 4194 RVA: 0x000477E6 File Offset: 0x000459E6
		[DomName("relatedTarget")]
		public IEventTarget Target { get; private set; }

		// Token: 0x06001063 RID: 4195 RVA: 0x0000EE9F File Offset: 0x0000D09F
		[DomName("getModifierState")]
		public bool GetModifierState(string key)
		{
			return false;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x000477F0 File Offset: 0x000459F0
		[DomName("initMouseEvent")]
		public void Init(string type, bool bubbles, bool cancelable, IWindow view, int detail, int screenX, int screenY, int clientX, int clientY, bool ctrlKey, bool altKey, bool shiftKey, bool metaKey, MouseButton button, IEventTarget target)
		{
			base.Init(type, bubbles, cancelable, view, detail);
			this.ScreenX = screenX;
			this.ScreenY = screenY;
			this.ClientX = clientX;
			this.ClientY = clientY;
			this.IsCtrlPressed = ctrlKey;
			this.IsMetaPressed = metaKey;
			this.IsShiftPressed = shiftKey;
			this.IsAltPressed = altKey;
			this.Button = button;
			this.Target = target;
		}
	}
}
