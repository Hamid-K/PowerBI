using System;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001ED RID: 493
	internal static class ModifierExtensions
	{
		// Token: 0x06001046 RID: 4166 RVA: 0x0000EE9F File Offset: 0x0000D09F
		public static bool IsCtrlPressed(this string modifierList)
		{
			return false;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0000EE9F File Offset: 0x0000D09F
		public static bool IsMetaPressed(this string modifierList)
		{
			return false;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0000EE9F File Offset: 0x0000D09F
		public static bool IsShiftPressed(this string modifierList)
		{
			return false;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0000EE9F File Offset: 0x0000D09F
		public static bool IsAltPressed(this string modifierList)
		{
			return false;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x000476F4 File Offset: 0x000458F4
		public static bool ContainsKey(this string modifierList, string key)
		{
			return modifierList.Contains(key);
		}
	}
}
