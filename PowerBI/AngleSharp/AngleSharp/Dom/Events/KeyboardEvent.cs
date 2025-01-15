using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001EA RID: 490
	[DomName("KeyboardEvent")]
	public class KeyboardEvent : UiEvent
	{
		// Token: 0x06001023 RID: 4131 RVA: 0x00046FAB File Offset: 0x000451AB
		public KeyboardEvent()
		{
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x000474F0 File Offset: 0x000456F0
		[DomConstructor]
		[DomInitDict(1, true)]
		public KeyboardEvent(string type, bool bubbles = false, bool cancelable = false, IWindow view = null, int detail = 0, string key = null, KeyboardLocation location = KeyboardLocation.Standard, string modifiersList = null, bool repeat = false)
		{
			this.Init(type, bubbles, cancelable, view, detail, key ?? string.Empty, location, modifiersList ?? string.Empty, repeat);
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0004752A File Offset: 0x0004572A
		// (set) Token: 0x06001026 RID: 4134 RVA: 0x00047532 File Offset: 0x00045732
		[DomName("key")]
		public string Key { get; private set; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x0004753B File Offset: 0x0004573B
		// (set) Token: 0x06001028 RID: 4136 RVA: 0x00047543 File Offset: 0x00045743
		[DomName("location")]
		public KeyboardLocation Location { get; private set; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x0004754C File Offset: 0x0004574C
		[DomName("ctrlKey")]
		public bool IsCtrlPressed
		{
			get
			{
				return this._modifiers.IsCtrlPressed();
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x00047559 File Offset: 0x00045759
		[DomName("shiftKey")]
		public bool IsShiftPressed
		{
			get
			{
				return this._modifiers.IsShiftPressed();
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x00047566 File Offset: 0x00045766
		[DomName("altKey")]
		public bool IsAltPressed
		{
			get
			{
				return this._modifiers.IsAltPressed();
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x00047573 File Offset: 0x00045773
		[DomName("metaKey")]
		public bool IsMetaPressed
		{
			get
			{
				return this._modifiers.IsMetaPressed();
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x00047580 File Offset: 0x00045780
		// (set) Token: 0x0600102E RID: 4142 RVA: 0x00047588 File Offset: 0x00045788
		[DomName("repeat")]
		public bool IsRepeated { get; private set; }

		// Token: 0x0600102F RID: 4143 RVA: 0x00047591 File Offset: 0x00045791
		[DomName("getModifierState")]
		public bool GetModifierState(string key)
		{
			return this._modifiers.ContainsKey(key);
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0004759F File Offset: 0x0004579F
		[DomName("locale")]
		public string Locale
		{
			get
			{
				if (!base.IsTrusted)
				{
					return null;
				}
				return string.Empty;
			}
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x000475B0 File Offset: 0x000457B0
		[DomName("initKeyboardEvent")]
		public void Init(string type, bool bubbles, bool cancelable, IWindow view, int detail, string key, KeyboardLocation location, string modifiersList, bool repeat)
		{
			base.Init(type, bubbles, cancelable, view, detail);
			this.Key = key;
			this.Location = location;
			this.IsRepeated = repeat;
			this._modifiers = modifiersList;
		}

		// Token: 0x04000A51 RID: 2641
		private string _modifiers;
	}
}
