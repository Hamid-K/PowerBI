using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D39 RID: 3385
	internal sealed class ParameterArguments
	{
		// Token: 0x06005AFD RID: 23293 RVA: 0x0013DBBA File Offset: 0x0013BDBA
		public ParameterArguments(IdentifierCubeExpression parameter, params Value[] values)
		{
			this.parameter = parameter;
			this.values = values;
		}

		// Token: 0x17001AF2 RID: 6898
		// (get) Token: 0x06005AFE RID: 23294 RVA: 0x0013DBD0 File Offset: 0x0013BDD0
		public IdentifierCubeExpression Parameter
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x17001AF3 RID: 6899
		// (get) Token: 0x06005AFF RID: 23295 RVA: 0x0013DBD8 File Offset: 0x0013BDD8
		public Value[] Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x040032CD RID: 13005
		private readonly IdentifierCubeExpression parameter;

		// Token: 0x040032CE RID: 13006
		private readonly Value[] values;
	}
}
