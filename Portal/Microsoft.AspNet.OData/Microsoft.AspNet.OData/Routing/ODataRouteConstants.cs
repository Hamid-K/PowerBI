using System;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x0200007B RID: 123
	public static class ODataRouteConstants
	{
		// Token: 0x040000ED RID: 237
		public static readonly string ODataPath = "odataPath";

		// Token: 0x040000EE RID: 238
		public static readonly string ODataPathTemplate = "{*" + ODataRouteConstants.ODataPath + "}";

		// Token: 0x040000EF RID: 239
		public static readonly string ConstraintName = "ODataConstraint";

		// Token: 0x040000F0 RID: 240
		public static readonly string VersionConstraintName = "ODataVersionConstraint";

		// Token: 0x040000F1 RID: 241
		public static readonly string Action = "action";

		// Token: 0x040000F2 RID: 242
		public static readonly string Controller = "controller";

		// Token: 0x040000F3 RID: 243
		public static readonly string Key = "key";

		// Token: 0x040000F4 RID: 244
		public static readonly string RelatedKey = "relatedKey";

		// Token: 0x040000F5 RID: 245
		public static readonly string NavigationProperty = "navigationProperty";

		// Token: 0x040000F6 RID: 246
		public static readonly string Batch = "$batch";

		// Token: 0x040000F7 RID: 247
		public static readonly string DynamicProperty = "dynamicProperty";

		// Token: 0x040000F8 RID: 248
		public static readonly string OptionalParameters = typeof(ODataOptionalParameter).FullName;
	}
}
