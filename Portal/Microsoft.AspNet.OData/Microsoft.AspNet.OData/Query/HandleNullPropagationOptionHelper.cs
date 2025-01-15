using System;
using System.Linq;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000CF RID: 207
	internal static class HandleNullPropagationOptionHelper
	{
		// Token: 0x060006D7 RID: 1751 RVA: 0x00017944 File Offset: 0x00015B44
		public static bool IsDefined(HandleNullPropagationOption value)
		{
			return value == HandleNullPropagationOption.Default || value == HandleNullPropagationOption.True || value == HandleNullPropagationOption.False;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00017953 File Offset: 0x00015B53
		public static void Validate(HandleNullPropagationOption value, string parameterValue)
		{
			if (!HandleNullPropagationOptionHelper.IsDefined(value))
			{
				throw Error.InvalidEnumArgument(parameterValue, (int)value, typeof(HandleNullPropagationOption));
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00017970 File Offset: 0x00015B70
		public static HandleNullPropagationOption GetDefaultHandleNullPropagationOption(IQueryable query)
		{
			string @namespace = query.Provider.GetType().Namespace;
			if (@namespace != null)
			{
				if (@namespace == "System.Data.Entity.Internal.Linq" || @namespace == "System.Data.Linq" || @namespace == "System.Data.Objects.ELinq" || @namespace == "System.Data.Entity.Core.Objects.ELinq" || @namespace == "Microsoft.EntityFrameworkCore.Query.Internal")
				{
					return HandleNullPropagationOption.False;
				}
				if (!(@namespace == "System.Linq"))
				{
				}
			}
			return HandleNullPropagationOption.True;
		}

		// Token: 0x040001FB RID: 507
		internal const string EntityFrameworkQueryProviderNamespace = "System.Data.Entity.Internal.Linq";

		// Token: 0x040001FC RID: 508
		internal const string ObjectContextQueryProviderNamespaceEF5 = "System.Data.Objects.ELinq";

		// Token: 0x040001FD RID: 509
		internal const string ObjectContextQueryProviderNamespaceEF6 = "System.Data.Entity.Core.Objects.ELinq";

		// Token: 0x040001FE RID: 510
		internal const string ObjectContextQueryProviderNamespaceEFCore2 = "Microsoft.EntityFrameworkCore.Query.Internal";

		// Token: 0x040001FF RID: 511
		internal const string Linq2SqlQueryProviderNamespace = "System.Data.Linq";

		// Token: 0x04000200 RID: 512
		internal const string Linq2ObjectsQueryProviderNamespace = "System.Linq";
	}
}
