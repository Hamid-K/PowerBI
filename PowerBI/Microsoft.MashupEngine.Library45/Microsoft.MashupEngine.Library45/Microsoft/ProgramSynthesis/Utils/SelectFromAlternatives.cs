using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000505 RID: 1285
	public static class SelectFromAlternatives
	{
		// Token: 0x06001CB0 RID: 7344 RVA: 0x00055964 File Offset: 0x00053B64
		public static TProgram SelectAlternative<TProgram, TIn, TOut>(this TProgram program, IProgramLoader<TProgram, TIn, TOut> loader, Func<ProgramNode, IEnumerable<ProgramNode>> getAlternatives, IEnumerable<Constraint<TIn, TOut>> constraints, IEnumerable<TIn> additionalInputs = null) where TProgram : Program<TIn, TOut>
		{
			IEnumerable<Example<TIn, TOut>> enumerable = (additionalInputs ?? new List<TIn>()).Select((TIn x) => new Example<TIn, TOut>(x, program.Run(x), false));
			IEnumerable<Constraint<TIn, TOut>> enumerable2 = constraints ?? new List<Constraint<TIn, TOut>>();
			List<Constraint<TIn, TOut>> allConstraints = enumerable2.Concat(enumerable).ToList<Constraint<TIn, TOut>>();
			ProgramNode programNode = program.ProgramNode;
			bool flag = false;
			AlternativesGenerator alternativesGenerator = new AlternativesGenerator(getAlternatives);
			Func<ProgramNode, bool> <>9__1;
			while (!flag)
			{
				IEnumerable<ProgramNode> enumerable3 = programNode.AcceptVisitor<IEnumerable<ProgramNode>>(alternativesGenerator);
				ProgramNode programNode2;
				if (enumerable3 == null)
				{
					programNode2 = null;
				}
				else
				{
					Func<ProgramNode, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (ProgramNode x) => allConstraints.All((Constraint<TIn, TOut> c) => c.Valid(loader.Create(x))));
					}
					programNode2 = enumerable3.FirstOrDefault(func);
				}
				ProgramNode programNode3 = programNode2;
				if (programNode3 == null)
				{
					flag = true;
				}
				else
				{
					programNode = programNode3;
				}
			}
			return loader.Create(programNode);
		}
	}
}
