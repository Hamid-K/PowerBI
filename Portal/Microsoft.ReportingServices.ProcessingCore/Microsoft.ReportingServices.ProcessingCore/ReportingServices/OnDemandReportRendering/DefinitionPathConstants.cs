using System;
using System.Globalization;
using System.Text;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002C1 RID: 705
	internal static class DefinitionPathConstants
	{
		// Token: 0x06001AB9 RID: 6841 RVA: 0x0006B549 File Offset: 0x00069749
		internal static string GetCollectionDefinitionPath(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef)
		{
			if (parentDefinitionPath == null || parentDefinitionPath.DefinitionPath == null)
			{
				return indexIntoParentCollectionDef.ToString(CultureInfo.InvariantCulture);
			}
			return parentDefinitionPath.DefinitionPath + "x" + indexIntoParentCollectionDef.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0006B57F File Offset: 0x0006977F
		internal static string GetTablixHierarchyDefinitionPath(IDefinitionPath parentDefinitionPath, bool isColumn)
		{
			if (isColumn)
			{
				return parentDefinitionPath.DefinitionPath + "xC";
			}
			return parentDefinitionPath.DefinitionPath + "xR";
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x0006B5A8 File Offset: 0x000697A8
		internal static string GetTablixCellDefinitionPath(IDefinitionPath parentDefinitionPath, int rowIndex, int colIndex, bool isTablixBodyCell)
		{
			StringBuilder stringBuilder = new StringBuilder(parentDefinitionPath.DefinitionPath);
			if (isTablixBodyCell)
			{
				stringBuilder.Append("xD");
			}
			else
			{
				stringBuilder.Append("xT");
			}
			stringBuilder.Append('x');
			stringBuilder.Append(rowIndex.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append('x');
			stringBuilder.Append(colIndex.ToString(CultureInfo.InvariantCulture));
			return stringBuilder.ToString();
		}

		// Token: 0x04000D44 RID: 3396
		public const string TablixCorner = "xT";

		// Token: 0x04000D45 RID: 3397
		public const string TablixRowHierarchy = "xR";

		// Token: 0x04000D46 RID: 3398
		public const string TablixColHierarchy = "xC";

		// Token: 0x04000D47 RID: 3399
		public const string TablixBody = "xD";

		// Token: 0x04000D48 RID: 3400
		public const string TablixHeader = "xH";

		// Token: 0x04000D49 RID: 3401
		public const string TablixSubMembers = "xM";

		// Token: 0x04000D4A RID: 3402
		public const string Report = "xA";

		// Token: 0x04000D4B RID: 3403
		public const string ReportBody = "xB";

		// Token: 0x04000D4C RID: 3404
		public const string SubReportBody = "xS";

		// Token: 0x04000D4D RID: 3405
		public const string Page = "xP";

		// Token: 0x04000D4E RID: 3406
		public const string PageHeader = "xH";

		// Token: 0x04000D4F RID: 3407
		public const string PageFooter = "xF";

		// Token: 0x04000D50 RID: 3408
		public const string TextRun = "xL";

		// Token: 0x04000D51 RID: 3409
		public const string Paragraph = "xK";

		// Token: 0x04000D52 RID: 3410
		public const string ReportSection = "xE";

		// Token: 0x04000D53 RID: 3411
		public const char DefinitionPathDelimiter = 'x';
	}
}
