using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000104 RID: 260
	internal static class BindingPathHelper
	{
		// Token: 0x0600090A RID: 2314 RVA: 0x00025DE2 File Offset: 0x00023FE2
		public static string ConvertBindingPath(this IEnumerable<MemberInfo> bindingPath)
		{
			if (bindingPath == null)
			{
				throw Error.ArgumentNull("bindingPath");
			}
			return string.Join("/", bindingPath.Select((MemberInfo e) => TypeHelper.GetQualifiedName(e)));
		}
	}
}
