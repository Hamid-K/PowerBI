using System;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200187F RID: 6271
	internal static class ArgumentAccess
	{
		// Token: 0x06009F23 RID: 40739 RVA: 0x0020E4A5 File Offset: 0x0020C6A5
		public static Func<InvocationQueryExpression, bool> Or(params Func<InvocationQueryExpression, bool>[] conditions)
		{
			return delegate(InvocationQueryExpression rowAccessor)
			{
				for (int i = 0; i < conditions.Length; i++)
				{
					if (conditions[i](rowAccessor))
					{
						return true;
					}
				}
				return false;
			};
		}

		// Token: 0x06009F24 RID: 40740 RVA: 0x0020E4BE File Offset: 0x0020C6BE
		public static bool Measure(InvocationQueryExpression rowAccessor)
		{
			return rowAccessor != null && rowAccessor.Function.Kind == QueryExpressionKind.Constant && CubeContextCubeValue.TabularCubeQuery.IsMeasureApplication(((ConstantQueryExpression)rowAccessor.Function).Value.AsFunction);
		}

		// Token: 0x06009F25 RID: 40741 RVA: 0x0020E4ED File Offset: 0x0020C6ED
		public static Func<InvocationQueryExpression, bool> Function(FunctionValue function)
		{
			return (InvocationQueryExpression rowAccessor) => rowAccessor != null && rowAccessor.Function.Kind == QueryExpressionKind.Constant && ((ConstantQueryExpression)rowAccessor.Function).Value.AsFunction.FunctionIdentity.Equals(function.FunctionIdentity);
		}

		// Token: 0x0400538E RID: 21390
		public static readonly Func<InvocationQueryExpression, bool> Allow = (InvocationQueryExpression rowAccessor) => true;

		// Token: 0x0400538F RID: 21391
		public static readonly Func<InvocationQueryExpression, bool> Deny = (InvocationQueryExpression rowAccessor) => false;

		// Token: 0x04005390 RID: 21392
		public static readonly Func<InvocationQueryExpression, bool> Safe = ArgumentAccess.Or(new Func<InvocationQueryExpression, bool>[]
		{
			ArgumentAccess.Function(TableModule.Table.RowCount),
			ArgumentAccess.Function(LanguageLibrary.List.Count),
			new Func<InvocationQueryExpression, bool>(ArgumentAccess.Measure)
		});
	}
}
