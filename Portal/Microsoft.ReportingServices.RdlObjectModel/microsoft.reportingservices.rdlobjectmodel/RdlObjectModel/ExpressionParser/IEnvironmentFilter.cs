using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200021B RID: 539
	internal interface IEnvironmentFilter
	{
		// Token: 0x06001234 RID: 4660
		bool IsAllowedType(Type type, out bool allowNew, out bool allowNewArray);

		// Token: 0x06001235 RID: 4661
		bool IsAllowedMember(string memberName);
	}
}
