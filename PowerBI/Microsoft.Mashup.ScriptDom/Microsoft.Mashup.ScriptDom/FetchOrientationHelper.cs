using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000148 RID: 328
	internal class FetchOrientationHelper : OptionsHelper<FetchOrientation>
	{
		// Token: 0x060014DA RID: 5338 RVA: 0x00091320 File Offset: 0x0008F520
		private FetchOrientationHelper()
		{
			base.AddOptionMapping(FetchOrientation.First, "FIRST");
			base.AddOptionMapping(FetchOrientation.Next, "NEXT");
			base.AddOptionMapping(FetchOrientation.Prior, "PRIOR");
			base.AddOptionMapping(FetchOrientation.Last, "LAST");
			base.AddOptionMapping(FetchOrientation.Relative, "RELATIVE");
			base.AddOptionMapping(FetchOrientation.Absolute, "ABSOLUTE");
		}

		// Token: 0x040011EB RID: 4587
		internal static readonly FetchOrientationHelper Instance = new FetchOrientationHelper();
	}
}
