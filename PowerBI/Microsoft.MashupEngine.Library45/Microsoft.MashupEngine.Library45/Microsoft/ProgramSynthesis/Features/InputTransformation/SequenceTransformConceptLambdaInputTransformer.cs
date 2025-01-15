using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Features.InputTransformation
{
	// Token: 0x020007E6 RID: 2022
	public class SequenceTransformConceptLambdaInputTransformer : IInputTransformer, IEquatable<IInputTransformer>
	{
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06002B1A RID: 11034 RVA: 0x000787D0 File Offset: 0x000769D0
		public ProgramNode SequenceGenerator { get; }

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x000787D8 File Offset: 0x000769D8
		public Symbol SymbolToBindSequenceElementsTo { get; }

		// Token: 0x06002B1C RID: 11036 RVA: 0x000787E0 File Offset: 0x000769E0
		public SequenceTransformConceptLambdaInputTransformer(ProgramNode sequenceGenerator, Symbol symbolToBindSequenceElementsTo)
		{
			this.SequenceGenerator = sequenceGenerator;
			this.SymbolToBindSequenceElementsTo = symbolToBindSequenceElementsTo;
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000787F6 File Offset: 0x000769F6
		public bool Equals(IInputTransformer other)
		{
			return other == this || (other != null && this.Equals(other as SequenceTransformConceptLambdaInputTransformer));
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x0007880F File Offset: 0x00076A0F
		public bool Equals(SequenceTransformConceptLambdaInputTransformer other)
		{
			return other == this || (other != null && this.SequenceGenerator.Equals(other.SequenceGenerator) && object.Equals(this.SymbolToBindSequenceElementsTo, other.SymbolToBindSequenceElementsTo));
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000787F6 File Offset: 0x000769F6
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && this.Equals(obj as SequenceTransformConceptLambdaInputTransformer));
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x00078842 File Offset: 0x00076A42
		public override int GetHashCode()
		{
			int num = this.SequenceGenerator.GetHashCode() * 64231;
			Symbol symbolToBindSequenceElementsTo = this.SymbolToBindSequenceElementsTo;
			return num ^ ((symbolToBindSequenceElementsTo != null) ? symbolToBindSequenceElementsTo.GetHashCode() : 71011);
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x0007886C File Offset: 0x00076A6C
		public IEnumerable<State> Transform(IEnumerable<State> inputs)
		{
			if (!(this.SymbolToBindSequenceElementsTo == null))
			{
				return inputs.SelectMany((State state) => from elem in this.SequenceGenerator.Invoke(state).ToEnumerable<object>()
					select state.Bind(this.SymbolToBindSequenceElementsTo, elem));
			}
			return inputs.SelectMany((State state) => from elem in this.SequenceGenerator.Invoke(state).ToEnumerable<object>()
				select state.WithFunctionalInput(elem, false));
		}
	}
}
