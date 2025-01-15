using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200133B RID: 4923
	public interface ITypeflowEnvironment
	{
		// Token: 0x060081DE RID: 33246
		TypeValue GetType(IExpression expression);
	}
}
