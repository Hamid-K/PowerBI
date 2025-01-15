using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D6B RID: 3435
	internal abstract class SetContextProvider
	{
		// Token: 0x17001B8C RID: 7052
		// (get) Token: 0x06005D4C RID: 23884
		public abstract IResource Resource { get; }

		// Token: 0x17001B8D RID: 7053
		// (get) Token: 0x06005D4D RID: 23885
		public abstract IEngineHost EngineHost { get; }

		// Token: 0x06005D4E RID: 23886
		public abstract bool TryCreateContext(ICube cube, Set set, IList<ParameterArguments> parameters, out SetContext context);

		// Token: 0x06005D4F RID: 23887
		public abstract bool TryCompileScalarExpression(ICube cube, Dimensionality dimensionality, CubeExpression expression, out Set set);
	}
}
