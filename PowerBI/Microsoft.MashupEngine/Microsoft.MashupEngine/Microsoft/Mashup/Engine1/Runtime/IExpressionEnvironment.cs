using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200133D RID: 4925
	public interface IExpressionEnvironment : ITypeflowEnvironment
	{
		// Token: 0x060081E0 RID: 33248
		IExpression SetType(IExpression expression, TypeValue type);

		// Token: 0x060081E1 RID: 33249
		bool TryAsListExpression(IExpression expression, int maxArguments, out IListExpression list);
	}
}
