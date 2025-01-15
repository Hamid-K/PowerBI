using System;
using System.Linq;
using System.Reflection;
using Microsoft.Cloud.Platform.Common;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000365 RID: 869
	internal static class ParameterFactoryUtility
	{
		// Token: 0x060019F3 RID: 6643 RVA: 0x0005FFF0 File Offset: 0x0005E1F0
		internal static CustomAttributeData GetParameterAttribute(ParameterInfo methodParameter)
		{
			return (from a in CustomAttributeData.GetCustomAttributes(methodParameter)
				where a.Constructor.DeclaringType.BaseType.FullName.Equals(typeof(PromotedParameterAttribute).FullName, StringComparison.Ordinal)
				select a).FirstOrDefault<CustomAttributeData>();
		}
	}
}
