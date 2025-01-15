using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000145 RID: 325
	internal class CursorOptionsHelper : OptionsHelper<CursorOptionKind>
	{
		// Token: 0x060014D8 RID: 5336 RVA: 0x00091260 File Offset: 0x0008F460
		private CursorOptionsHelper()
		{
			base.AddOptionMapping(CursorOptionKind.Local, "LOCAL");
			base.AddOptionMapping(CursorOptionKind.Global, "GLOBAL");
			base.AddOptionMapping(CursorOptionKind.Scroll, "SCROLL");
			base.AddOptionMapping(CursorOptionKind.ForwardOnly, "FORWARD_ONLY");
			base.AddOptionMapping(CursorOptionKind.Insensitive, "INSENSITIVE");
			base.AddOptionMapping(CursorOptionKind.Keyset, "KEYSET");
			base.AddOptionMapping(CursorOptionKind.Dynamic, "DYNAMIC");
			base.AddOptionMapping(CursorOptionKind.FastForward, "FAST_FORWARD");
			base.AddOptionMapping(CursorOptionKind.ScrollLocks, "SCROLL_LOCKS");
			base.AddOptionMapping(CursorOptionKind.Optimistic, "OPTIMISTIC");
			base.AddOptionMapping(CursorOptionKind.ReadOnly, "READ_ONLY");
			base.AddOptionMapping(CursorOptionKind.Static, "STATIC");
			base.AddOptionMapping(CursorOptionKind.TypeWarning, "TYPE_WARNING");
		}

		// Token: 0x040011DE RID: 4574
		internal static readonly CursorOptionsHelper Instance = new CursorOptionsHelper();
	}
}
