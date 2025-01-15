using System;

namespace msclr
{
	// Token: 0x02000013 RID: 19
	internal struct _detail_class
	{
		// Token: 0x04000103 RID: 259
		public static string _safe_true = _detail_class.dummy_struct.dummy_string;

		// Token: 0x04000104 RID: 260
		public static string _safe_false = null;

		// Token: 0x02000014 RID: 20
		public struct dummy_struct
		{
			// Token: 0x04000105 RID: 261
			public static readonly string dummy_string = "";
		}
	}
}
