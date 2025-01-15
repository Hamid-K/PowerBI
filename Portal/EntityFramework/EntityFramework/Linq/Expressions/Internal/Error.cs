using System;
using System.Data.Entity.Resources;

namespace System.Linq.Expressions.Internal
{
	// Token: 0x02000055 RID: 85
	internal static class Error
	{
		// Token: 0x0600021B RID: 539 RVA: 0x00009065 File Offset: 0x00007265
		internal static Exception UnhandledExpressionType(ExpressionType expressionType)
		{
			return new NotSupportedException(Strings.ELinq_UnhandledExpressionType(expressionType));
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00009077 File Offset: 0x00007277
		internal static Exception UnhandledBindingType(MemberBindingType memberBindingType)
		{
			return new NotSupportedException(Strings.ELinq_UnhandledBindingType(memberBindingType));
		}
	}
}
