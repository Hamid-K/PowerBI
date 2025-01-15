using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Features.InputTransformation
{
	// Token: 0x020007E4 RID: 2020
	public class LambdaBodyInputTransformer : IInputTransformer, IEquatable<IInputTransformer>
	{
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002B09 RID: 11017 RVA: 0x0007865B File Offset: 0x0007685B
		public Symbol LambdaVariable { get; }

		// Token: 0x06002B0A RID: 11018 RVA: 0x00078663 File Offset: 0x00076863
		public LambdaBodyInputTransformer(Symbol lambdaVariable)
		{
			this.LambdaVariable = lambdaVariable;
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x00078672 File Offset: 0x00076872
		public bool Equals(IInputTransformer other)
		{
			return other != null && (other == this || this.Equals(other as LambdaBodyInputTransformer));
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x0007868B File Offset: 0x0007688B
		public bool Equals(LambdaBodyInputTransformer other)
		{
			return other != null && (other == this || this.LambdaVariable.Equals(other.LambdaVariable));
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x00078672 File Offset: 0x00076872
		public override bool Equals(object obj)
		{
			return obj != null && (obj == this || this.Equals(obj as LambdaBodyInputTransformer));
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000786A9 File Offset: 0x000768A9
		public override int GetHashCode()
		{
			return this.LambdaVariable.GetHashCode() * 64037;
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000786BC File Offset: 0x000768BC
		public IEnumerable<State> Transform(IEnumerable<State> inputs)
		{
			return inputs.Select((State state) => state.BindFunctionalInput(this.LambdaVariable));
		}
	}
}
