using System;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000FF RID: 255
	// (Invoke) Token: 0x0600106C RID: 4204
	internal delegate void GetRestrictionsInfoByObjectType(ObjectType type, out string requestingPath, out CompatibilityRestrictionSet restrictions);
}
