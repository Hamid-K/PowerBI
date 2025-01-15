using System;

namespace Microsoft.PowerBI.DataExtension.Msolap
{
	// Token: 0x0200000B RID: 11
	internal static class DataExtensionConstants
	{
		// Token: 0x04000043 RID: 67
		internal const string BaseCubeNameColumn = "BASE_CUBE_NAME";

		// Token: 0x04000044 RID: 68
		internal const string MdSchemaCubesPerspectiveRestrictionInvalid = "MdSchemaCubesPerspectiveRestrictionInvalidError";

		// Token: 0x04000045 RID: 69
		internal const string PerspectiveRestrictionValidationErrorForAS2012RTM = "InvalidNonDefaultPerspectiveRestrictionError";

		// Token: 0x04000046 RID: 70
		internal const string InvalidNonDefaultPerspective = "The datasource specifies the non-default perspective, but this version of Analysis Services does no support metadata for non-default perspectives.Choose the default perspective instead.";

		// Token: 0x04000047 RID: 71
		internal const int CSDLRestrictionCountForAS2012 = 2;
	}
}
