using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x0200023D RID: 573
	public class OutputIs<TInput, TOutput> : Constraint<TInput, TOutput>
	{
		// Token: 0x06000C3F RID: 3135 RVA: 0x00025002 File Offset: 0x00023202
		public OutputIs(IType type)
		{
			this.Type = type;
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00025011 File Offset: 0x00023211
		public IType Type { get; }

		// Token: 0x06000C41 RID: 3137 RVA: 0x00025019 File Offset: 0x00023219
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			OutputIs<TInput, TOutput> outputIs = other as OutputIs<TInput, TOutput>;
			return outputIs != null && outputIs.Type.Equals(this.Type);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00025037 File Offset: 0x00023237
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			OutputIs<TInput, TOutput> outputIs = other as OutputIs<TInput, TOutput>;
			return outputIs != null && !outputIs.Type.Equals(this.Type);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00025058 File Offset: 0x00023258
		private bool Valid(ITypedProgram<TInput, TOutput> typedProgram)
		{
			IEnumerable<IType> enumerable = ((typedProgram != null) ? typedProgram.OutputTypes.Distinct<IType>().Take(2) : null);
			return enumerable != null && enumerable.Count<IType>() == 1 && this.Type.IsAssignableFrom(enumerable.Single<IType>());
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0002509C File Offset: 0x0002329C
		public override bool Valid(Program<TInput, TOutput> program)
		{
			return this.Valid(program as ITypedProgram<TInput, TOutput>);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000250AC File Offset: 0x000232AC
		public override bool Valid(Program<TInput, TOutput> program, IEnumerable<TInput> inputs)
		{
			ITypedProgram<TInput, TOutput> typedProgram = program as ITypedProgram<TInput, TOutput>;
			if (typedProgram == null)
			{
				return inputs.All(delegate(TInput input)
				{
					ITypedValue typedValue = program.Run(input) as ITypedValue;
					if (typedValue == null)
					{
						throw new InvalidOperationException("Can only use OutputIs constraint when outputs are ITypedValues.");
					}
					return this.Type.IsAssignableFrom(typedValue.Type);
				});
			}
			return this.Valid(typedProgram);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000250F6 File Offset: 0x000232F6
		public override int GetHashCode()
		{
			return this.Type.GetHashCode();
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00025103 File Offset: 0x00023303
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("output is {0}", new object[] { this.Type }));
		}
	}
}
