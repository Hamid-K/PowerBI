using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Rules;

namespace Microsoft.ProgramSynthesis.Features.InputTransformation
{
	// Token: 0x020007E5 RID: 2021
	public class LetBodyInputTransformer : IInputTransformer, IEquatable<IInputTransformer>
	{
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002B11 RID: 11025 RVA: 0x000786DE File Offset: 0x000768DE
		public ProgramNode BindingProgram { get; }

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x000786E6 File Offset: 0x000768E6
		public LetRule LetRule { get; }

		// Token: 0x06002B13 RID: 11027 RVA: 0x000786EE File Offset: 0x000768EE
		public LetBodyInputTransformer(LetRule letRule, ProgramNode bindingProgram)
		{
			this.LetRule = letRule;
			this.BindingProgram = bindingProgram;
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x00078704 File Offset: 0x00076904
		public bool Equals(IInputTransformer other)
		{
			return other != null && (other == this || this.Equals(other as LetBodyInputTransformer));
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x0007871D File Offset: 0x0007691D
		public bool Equals(LetBodyInputTransformer other)
		{
			return other == this || (other != null && this.LetRule.Equals(other.LetRule) && this.BindingProgram.Equals(other.BindingProgram));
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x00078704 File Offset: 0x00076904
		public override bool Equals(object obj)
		{
			return obj != null && (obj == this || this.Equals(obj as LetBodyInputTransformer));
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x00078750 File Offset: 0x00076950
		public override int GetHashCode()
		{
			if (this._hashCode == null)
			{
				this._hashCode = new int?((this.LetRule.GetHashCode() * 62987) ^ this.BindingProgram.GetHashCode());
			}
			return this._hashCode.Value;
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x0007879D File Offset: 0x0007699D
		public IEnumerable<State> Transform(IEnumerable<State> inputs)
		{
			return inputs.Select((State state) => state.Bind(this.LetRule.Variable, this.BindingProgram.Invoke(state)));
		}

		// Token: 0x040014B0 RID: 5296
		private int? _hashCode;
	}
}
