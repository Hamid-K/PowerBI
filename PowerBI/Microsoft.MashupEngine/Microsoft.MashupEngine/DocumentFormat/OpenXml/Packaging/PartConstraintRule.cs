using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002146 RID: 8518
	internal class PartConstraintRule
	{
		// Token: 0x0600D3C8 RID: 54216 RVA: 0x002A00C2 File Offset: 0x0029E2C2
		internal PartConstraintRule(string partClassName, string partContentType, bool minOccursIsNonZero, bool maxOccursGreatThanOne, FileFormatVersions fileFormat)
		{
			this.PartClassName = partClassName;
			this.PartContentType = partContentType;
			this.MinOccursIsNonZero = minOccursIsNonZero;
			this.MaxOccursGreatThanOne = maxOccursGreatThanOne;
			this.FileFormat = fileFormat;
		}

		// Token: 0x040069A3 RID: 27043
		internal string PartClassName;

		// Token: 0x040069A4 RID: 27044
		internal string PartContentType;

		// Token: 0x040069A5 RID: 27045
		internal bool MinOccursIsNonZero;

		// Token: 0x040069A6 RID: 27046
		internal bool MaxOccursGreatThanOne;

		// Token: 0x040069A7 RID: 27047
		internal FileFormatVersions FileFormat;
	}
}
