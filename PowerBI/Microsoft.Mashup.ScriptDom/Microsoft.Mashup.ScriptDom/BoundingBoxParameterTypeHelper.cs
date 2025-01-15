using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000043 RID: 67
	internal class BoundingBoxParameterTypeHelper : OptionsHelper<BoundingBoxParameterType>
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x00005EDE File Offset: 0x000040DE
		private BoundingBoxParameterTypeHelper()
		{
			base.AddOptionMapping(BoundingBoxParameterType.XMin, "XMIN");
			base.AddOptionMapping(BoundingBoxParameterType.YMin, "YMIN");
			base.AddOptionMapping(BoundingBoxParameterType.XMax, "XMAX");
			base.AddOptionMapping(BoundingBoxParameterType.YMax, "YMAX");
		}

		// Token: 0x04000126 RID: 294
		internal static readonly BoundingBoxParameterTypeHelper Instance = new BoundingBoxParameterTypeHelper();
	}
}
