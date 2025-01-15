using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001F1 RID: 497
	[DomName("TouchEvent")]
	public class TouchEvent : UiEvent
	{
		// Token: 0x0600106F RID: 4207 RVA: 0x00046FAB File Offset: 0x000451AB
		public TouchEvent()
		{
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x000478DA File Offset: 0x00045ADA
		[DomConstructor]
		[DomInitDict(1, true)]
		public TouchEvent(string type, bool bubbles = false, bool cancelable = false, IWindow view = null, int detail = 0, ITouchList touches = null, ITouchList targetTouches = null, ITouchList changedTouches = null, bool ctrlKey = false, bool altKey = false, bool shiftKey = false, bool metaKey = false)
		{
			base.Init(type, bubbles, cancelable, view, detail);
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x000478EF File Offset: 0x00045AEF
		// (set) Token: 0x06001072 RID: 4210 RVA: 0x000478F7 File Offset: 0x00045AF7
		[DomName("touches")]
		public ITouchList Touches { get; private set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x00047900 File Offset: 0x00045B00
		// (set) Token: 0x06001074 RID: 4212 RVA: 0x00047908 File Offset: 0x00045B08
		[DomName("targetTouches")]
		public ITouchList TargetTouches { get; private set; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x00047911 File Offset: 0x00045B11
		// (set) Token: 0x06001076 RID: 4214 RVA: 0x00047919 File Offset: 0x00045B19
		[DomName("changedTouches")]
		public ITouchList ChangedTouches { get; private set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x00047922 File Offset: 0x00045B22
		// (set) Token: 0x06001078 RID: 4216 RVA: 0x0004792A File Offset: 0x00045B2A
		[DomName("altKey")]
		public bool IsAltPressed { get; private set; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x00047933 File Offset: 0x00045B33
		// (set) Token: 0x0600107A RID: 4218 RVA: 0x0004793B File Offset: 0x00045B3B
		[DomName("metaKey")]
		public bool IsMetaPressed { get; private set; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x00047944 File Offset: 0x00045B44
		// (set) Token: 0x0600107C RID: 4220 RVA: 0x0004794C File Offset: 0x00045B4C
		[DomName("ctrlKey")]
		public bool IsCtrlPressed { get; private set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00047955 File Offset: 0x00045B55
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x0004795D File Offset: 0x00045B5D
		[DomName("shiftKey")]
		public bool IsShiftPressed { get; private set; }

		// Token: 0x0600107F RID: 4223 RVA: 0x00047968 File Offset: 0x00045B68
		[DomName("initTouchEvent")]
		public void Init(string type, bool bubbles, bool cancelable, IWindow view, int detail, ITouchList touches, ITouchList targetTouches, ITouchList changedTouches, bool ctrlKey, bool altKey, bool shiftKey, bool metaKey)
		{
			base.Init(type, bubbles, cancelable, view, detail);
			this.Touches = touches;
			this.TargetTouches = targetTouches;
			this.ChangedTouches = changedTouches;
			this.IsCtrlPressed = ctrlKey;
			this.IsShiftPressed = shiftKey;
			this.IsMetaPressed = metaKey;
			this.IsAltPressed = altKey;
		}
	}
}
