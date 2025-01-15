using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010D6 RID: 4310
	internal interface IQueryResultTableValue
	{
		// Token: 0x17001FB3 RID: 8115
		// (get) Token: 0x060070DD RID: 28893
		EnvironmentBase Environment { get; }

		// Token: 0x17001FB4 RID: 8116
		// (get) Token: 0x060070DE RID: 28894
		IEngineHost Host { get; }

		// Token: 0x17001FB5 RID: 8117
		// (get) Token: 0x060070DF RID: 28895
		IExpression SyntaxTree { get; }

		// Token: 0x17001FB6 RID: 8118
		// (get) Token: 0x060070E0 RID: 28896
		ValueBuilderBase ValueBuilder { get; }

		// Token: 0x17001FB7 RID: 8119
		// (get) Token: 0x060070E1 RID: 28897
		TypeValue Type { get; }
	}
}
