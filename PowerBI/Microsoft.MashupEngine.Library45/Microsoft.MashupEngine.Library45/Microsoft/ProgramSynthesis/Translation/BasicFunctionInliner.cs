using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002CB RID: 715
	public class BasicFunctionInliner : IModuleOptimizer
	{
		// Token: 0x06000F88 RID: 3976 RVA: 0x00002130 File Offset: 0x00000330
		protected BasicFunctionInliner()
		{
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0002D1CD File Offset: 0x0002B3CD
		public static BasicFunctionInliner Instance { get; } = new BasicFunctionInliner();

		// Token: 0x06000F8A RID: 3978 RVA: 0x0002D1D4 File Offset: 0x0002B3D4
		public Dictionary<string, IGeneratedFunction> Optimize(IReadOnlyDictionary<string, IGeneratedFunction> boundFunctions, out List<string> toBeRemovedFunctions)
		{
			IEnumerable<KeyValuePair<string, SSAGeneratedFunction>> enumerable = from kvp in boundFunctions
				where kvp.Value is SSAGeneratedFunction
				select KVP.Create<string, SSAGeneratedFunction>(kvp.Key, (SSAGeneratedFunction)kvp.Value);
			IEnumerable<string> enumerable2 = this.AllUsedFunctions(enumerable);
			HashSet<string> singlyUsedFunctions = (from f in enumerable2
				group f by f into g
				where g.Count<string>() == 1
				select g.Key).ConvertToHashSet<string>();
			IEnumerable<BasicFunctionInliner.CallerCalleePair> enumerable3 = from x in this.PassThroughFunctions(enumerable)
				where singlyUsedFunctions.Contains(x.Callee)
				select x;
			toBeRemovedFunctions = enumerable3.Select((BasicFunctionInliner.CallerCalleePair x) => x.Callee).ToList<string>();
			Dictionary<string, IGeneratedFunction> dictionary = this.TransitivelyCollapse(enumerable3).ToDictionary((KeyValuePair<string, string> x) => x.Key, (KeyValuePair<string, string> x) => boundFunctions[x.Value]);
			return this.ModifyBinding(dictionary, boundFunctions);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0002D34C File Offset: 0x0002B54C
		private IEnumerable<string> AllUsedFunctions(IEnumerable<KeyValuePair<string, SSAGeneratedFunction>> functions)
		{
			return (from kvp in functions
				select kvp.Value.SSASequence into steps
				where steps != null
				select steps).SelectMany((List<SSAStep> steps) => from x in steps.Select((SSAStep x) => x.RValue).OfType<SSAFunctionApplication>()
				select x.FunctionName);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0002D3C8 File Offset: 0x0002B5C8
		private List<BasicFunctionInliner.CallerCalleePair> PassThroughFunctions(IEnumerable<KeyValuePair<string, SSAGeneratedFunction>> functions)
		{
			List<BasicFunctionInliner.CallerCalleePair> list = new List<BasicFunctionInliner.CallerCalleePair>();
			foreach (KeyValuePair<string, SSAGeneratedFunction> keyValuePair in functions)
			{
				string text;
				SSAGeneratedFunction ssageneratedFunction;
				keyValuePair.Deconstruct(out text, out ssageneratedFunction);
				string text2 = text;
				SSAGeneratedFunction ssageneratedFunction2 = ssageneratedFunction;
				List<SSAStep> ssasequence = ssageneratedFunction2.SSASequence;
				if (ssasequence != null && ssasequence.Count == 1)
				{
					SSAFunctionApplication ssafunctionApplication = ssasequence[0].RValue as SSAFunctionApplication;
					if (ssafunctionApplication != null && ssageneratedFunction2.ReturnType == ssafunctionApplication.ValueType && ssageneratedFunction2.Parameters.Count == ssafunctionApplication.FunctionArguments.Count)
					{
						if (ssageneratedFunction2.Parameters.Zip(ssafunctionApplication.FunctionArguments, delegate(Record<string, Type> fi, SSAValue ai)
						{
							SSAVariable ssavariable = ai as SSAVariable;
							return ssavariable != null && fi.Item1 == ssavariable.VariableName;
						}).All((bool x) => x))
						{
							list.Add(new BasicFunctionInliner.CallerCalleePair(text2, ssafunctionApplication.FunctionName));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0002D4F8 File Offset: 0x0002B6F8
		private Dictionary<string, string> TransitivelyCollapse(IEnumerable<BasicFunctionInliner.CallerCalleePair> binaryRelation)
		{
			bool flag = false;
			Dictionary<string, string> binaryDict = binaryRelation.ToDictionary((BasicFunctionInliner.CallerCalleePair t) => t.Caller, (BasicFunctionInliner.CallerCalleePair t) => t.Callee);
			Func<KeyValuePair<string, string>, bool> <>9__2;
			do
			{
				IEnumerable<KeyValuePair<string, string>> binaryDict2 = binaryDict;
				Func<KeyValuePair<string, string>, bool> func;
				if ((func = <>9__2) == null)
				{
					func = (<>9__2 = (KeyValuePair<string, string> x) => binaryDict.ContainsKey(x.Value));
				}
				KeyValuePair<string, string> keyValuePair = binaryDict2.Where(func).FirstOrDefault<KeyValuePair<string, string>>();
				if (keyValuePair.Equals(default(KeyValuePair<string, string>)))
				{
					flag = true;
				}
				else
				{
					binaryDict[keyValuePair.Key] = binaryDict[keyValuePair.Value];
				}
			}
			while (!flag);
			return binaryDict;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0000E945 File Offset: 0x0000CB45
		protected virtual Dictionary<string, IGeneratedFunction> ModifyBinding(Dictionary<string, IGeneratedFunction> newBinding, IReadOnlyDictionary<string, IGeneratedFunction> boundFunctions)
		{
			return newBinding;
		}

		// Token: 0x020002CC RID: 716
		private struct CallerCalleePair
		{
			// Token: 0x17000360 RID: 864
			// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0002D5E2 File Offset: 0x0002B7E2
			public readonly string Caller { get; }

			// Token: 0x17000361 RID: 865
			// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0002D5EA File Offset: 0x0002B7EA
			public readonly string Callee { get; }

			// Token: 0x06000F92 RID: 3986 RVA: 0x0002D5F2 File Offset: 0x0002B7F2
			public CallerCalleePair(string caller, string callee)
			{
				this.Caller = caller;
				this.Callee = callee;
			}
		}
	}
}
