using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001F4 RID: 500
	[DomName("WheelEvent")]
	public class WheelEvent : MouseEvent
	{
		// Token: 0x0600108C RID: 4236 RVA: 0x00047A43 File Offset: 0x00045C43
		public WheelEvent()
		{
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00047A4C File Offset: 0x00045C4C
		[DomConstructor]
		[DomInitDict(1, true)]
		public WheelEvent(string type, bool bubbles = false, bool cancelable = false, IWindow view = null, int detail = 0, int screenX = 0, int screenY = 0, int clientX = 0, int clientY = 0, MouseButton button = MouseButton.Primary, IEventTarget target = null, string modifiersList = null, double deltaX = 0.0, double deltaY = 0.0, double deltaZ = 0.0, WheelMode deltaMode = WheelMode.Pixel)
		{
			this.Init(type, bubbles, cancelable, view, detail, screenX, screenY, clientX, clientY, button, target, modifiersList ?? string.Empty, deltaX, deltaY, deltaZ, deltaMode);
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x00047A8B File Offset: 0x00045C8B
		// (set) Token: 0x0600108F RID: 4239 RVA: 0x00047A93 File Offset: 0x00045C93
		[DomName("deltaX")]
		public double DeltaX { get; private set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x00047A9C File Offset: 0x00045C9C
		// (set) Token: 0x06001091 RID: 4241 RVA: 0x00047AA4 File Offset: 0x00045CA4
		[DomName("deltaY")]
		public double DeltaY { get; private set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x00047AAD File Offset: 0x00045CAD
		// (set) Token: 0x06001093 RID: 4243 RVA: 0x00047AB5 File Offset: 0x00045CB5
		[DomName("deltaZ")]
		public double DeltaZ { get; private set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00047ABE File Offset: 0x00045CBE
		// (set) Token: 0x06001095 RID: 4245 RVA: 0x00047AC6 File Offset: 0x00045CC6
		[DomName("deltaMode")]
		public WheelMode DeltaMode { get; private set; }

		// Token: 0x06001096 RID: 4246 RVA: 0x00047AD0 File Offset: 0x00045CD0
		[DomName("initWheelEvent")]
		public void Init(string type, bool bubbles, bool cancelable, IWindow view, int detail, int screenX, int screenY, int clientX, int clientY, MouseButton button, IEventTarget target, string modifiersList, double deltaX, double deltaY, double deltaZ, WheelMode deltaMode)
		{
			base.Init(type, bubbles, cancelable, view, detail, screenX, screenY, clientX, clientY, modifiersList.IsCtrlPressed(), modifiersList.IsAltPressed(), modifiersList.IsShiftPressed(), modifiersList.IsMetaPressed(), button, target);
			this.DeltaX = deltaX;
			this.DeltaY = deltaY;
			this.DeltaZ = deltaZ;
			this.DeltaMode = deltaMode;
		}
	}
}
