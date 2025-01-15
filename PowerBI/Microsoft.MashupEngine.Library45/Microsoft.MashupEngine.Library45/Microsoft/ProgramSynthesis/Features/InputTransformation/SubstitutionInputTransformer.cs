using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Features.InputTransformation
{
	// Token: 0x020007E9 RID: 2025
	public class SubstitutionInputTransformer : IInputTransformer, IEquatable<IInputTransformer>
	{
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x00078958 File Offset: 0x00076B58
		public Symbol OldSymbol { get; }

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002B29 RID: 11049 RVA: 0x00078960 File Offset: 0x00076B60
		public Symbol NewSymbol { get; }

		// Token: 0x06002B2A RID: 11050 RVA: 0x00078968 File Offset: 0x00076B68
		public SubstitutionInputTransformer(Symbol oldSymbol, Symbol newSymbol)
		{
			this.OldSymbol = oldSymbol;
			this.NewSymbol = newSymbol;
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x0007897E File Offset: 0x00076B7E
		public bool Equals(SubstitutionInputTransformer other)
		{
			return this == other || (other != null && this.OldSymbol.Equals(other.OldSymbol) && this.NewSymbol.Equals(other.NewSymbol));
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x000789B1 File Offset: 0x00076BB1
		public override bool Equals(object obj)
		{
			return this == obj || (obj != null && this.Equals(obj as SubstitutionInputTransformer));
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x000789B1 File Offset: 0x00076BB1
		public bool Equals(IInputTransformer other)
		{
			return this == other || (other != null && this.Equals(other as SubstitutionInputTransformer));
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x000789CA File Offset: 0x00076BCA
		public override int GetHashCode()
		{
			return (this.OldSymbol.GetHashCode() * 33223) ^ this.NewSymbol.GetHashCode();
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x000789E9 File Offset: 0x00076BE9
		public IEnumerable<State> Transform(IEnumerable<State> inputs)
		{
			return inputs.Select((State s) => s.Substitute(this.OldSymbol, this.NewSymbol));
		}
	}
}
