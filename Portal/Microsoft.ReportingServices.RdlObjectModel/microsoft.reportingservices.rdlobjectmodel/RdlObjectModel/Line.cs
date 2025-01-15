using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000175 RID: 373
	public class Line : ReportItem
	{
		// Token: 0x06000BDE RID: 3038 RVA: 0x000208BB File Offset: 0x0001EABB
		public Line()
		{
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x000208C3 File Offset: 0x0001EAC3
		internal Line(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020003A3 RID: 931
		internal new class Definition : DefinitionStore<Line, Line.Definition.Properties>
		{
			// Token: 0x06001847 RID: 6215 RVA: 0x0003B5B9 File Offset: 0x000397B9
			private Definition()
			{
			}

			// Token: 0x020004BB RID: 1211
			internal enum Properties
			{
				// Token: 0x04000E10 RID: 3600
				Style,
				// Token: 0x04000E11 RID: 3601
				Name,
				// Token: 0x04000E12 RID: 3602
				ActionInfo,
				// Token: 0x04000E13 RID: 3603
				Top,
				// Token: 0x04000E14 RID: 3604
				Left,
				// Token: 0x04000E15 RID: 3605
				Height,
				// Token: 0x04000E16 RID: 3606
				Width,
				// Token: 0x04000E17 RID: 3607
				ZIndex,
				// Token: 0x04000E18 RID: 3608
				Visibility,
				// Token: 0x04000E19 RID: 3609
				ToolTip,
				// Token: 0x04000E1A RID: 3610
				ToolTipLocID,
				// Token: 0x04000E1B RID: 3611
				DocumentMapLabel,
				// Token: 0x04000E1C RID: 3612
				DocumentMapLabelLocID,
				// Token: 0x04000E1D RID: 3613
				Bookmark,
				// Token: 0x04000E1E RID: 3614
				RepeatWith,
				// Token: 0x04000E1F RID: 3615
				CustomProperties,
				// Token: 0x04000E20 RID: 3616
				DataElementName,
				// Token: 0x04000E21 RID: 3617
				DataElementOutput,
				// Token: 0x04000E22 RID: 3618
				PropertyCount
			}
		}
	}
}
