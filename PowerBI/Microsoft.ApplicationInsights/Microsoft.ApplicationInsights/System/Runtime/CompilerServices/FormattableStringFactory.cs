using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200001A RID: 26
	internal class FormattableStringFactory
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00006269 File Offset: 0x00004469
		public static global::System.FormattableString Create(string format, params object[] args)
		{
			return new global::System.FormattableString(format, args);
		}
	}
}
