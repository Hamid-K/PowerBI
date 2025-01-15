using System;

namespace Microsoft.InfoNav
{
	// Token: 0x0200003D RID: 61
	public interface IConceptualNavigationProperty
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060000F7 RID: 247
		string Name { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060000F8 RID: 248
		string EdmName { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060000F9 RID: 249
		bool IsActive { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060000FA RID: 250
		CrossFilterDirection CrossFilterDirection { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060000FB RID: 251
		IConceptualColumn SourceColumn { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060000FC RID: 252
		IConceptualColumn TargetColumn { get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060000FD RID: 253
		IConceptualEntity TargetEntity { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060000FE RID: 254
		ConceptualMultiplicity SourceMultiplicity { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060000FF RID: 255
		ConceptualMultiplicity TargetMultiplicity { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000100 RID: 256
		ConceptualNavigationBehavior Behavior { get; }
	}
}
