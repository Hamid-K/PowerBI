using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x02000306 RID: 774
	public class VariableNamesNormalizer : IOptimizer
	{
		// Token: 0x060010CD RID: 4301 RVA: 0x00030188 File Offset: 0x0002E388
		public VariableNamesNormalizer(IEnumerable<string> boundNames)
		{
			this._boundNames = new HashSet<string>(boundNames ?? Enumerable.Empty<string>());
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000301A8 File Offset: 0x0002E3A8
		private uint UintParser(string s)
		{
			uint num;
			if (!uint.TryParse(s, out num))
			{
				return 0U;
			}
			return num;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x000301C4 File Offset: 0x0002E3C4
		public List<SSAStep> Optimize(IReadOnlyList<SSAStep> steps)
		{
			int prefixWidth = this.GetMinPrefix(steps.Count);
			List<Record<string, string, uint>> lookupDict = (from step in steps
				let name = step.LValue.Name
				let groups = VariableNamesNormalizer.regex.Match(name).Groups
				let idGroup = groups["id"]
				let nameGroup = groups["name"]
				where idGroup.Success && nameGroup.Success
				let baseName = nameGroup.Value
				let id = this.UintParser(idGroup.Value)
				orderby id
				select Record.Create<string, string, uint>(name, baseName, id)).ToList<Record<string, string, uint>>();
			uint startIndex = lookupDict.FirstOrDefault<Record<string, string, uint>>().Item3;
			IReadOnlyDictionary<string, string> readOnlyDictionary = (from kvp in lookupDict.Select((Record<string, string, uint> entry, int i) => KVP.Create<string, string>(entry.Item1, entry.Item2 + "_" + ((long)((ulong)startIndex + (ulong)((long)i))).ToString(string.Format("D{0}", prefixWidth))))
				where !lookupDict.Any((Record<string, string, uint> x) => x.Item1.Equals(kvp.Value)) && !this._boundNames.Contains(kvp.Value)
				select kvp).ToDictionary<string, string>();
			foreach (SSAStep ssastep in steps)
			{
				SSARegister lvalue = ssastep.LValue;
				string text;
				if (readOnlyDictionary.TryGetValue(lvalue.Name, out text))
				{
					SSARegister ssaregister = lvalue.CloneWithNewName(text);
					foreach (SSAValue ssavalue in lvalue.ImmediateDownLinks.ToList<SSAValue>())
					{
						((SSARValue)ssavalue).SubstituteInPlace(lvalue, ssaregister);
					}
					ssastep.LValue = ssaregister;
				}
			}
			return steps.ToList<SSAStep>();
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00030428 File Offset: 0x0002E628
		private int GetMinPrefix(int maxSteps)
		{
			if (maxSteps < 10)
			{
				return 1;
			}
			if (maxSteps < 100)
			{
				return 2;
			}
			if (maxSteps < 1000)
			{
				return 3;
			}
			if (maxSteps < 10000)
			{
				return 4;
			}
			if (maxSteps < 100000)
			{
				return 5;
			}
			if (maxSteps < 1000000)
			{
				return 6;
			}
			if (maxSteps < 10000000)
			{
				return 7;
			}
			return 8;
		}

		// Token: 0x0400081F RID: 2079
		private HashSet<string> _boundNames;

		// Token: 0x04000820 RID: 2080
		private static readonly Regex regex = new Regex("(?<name>^.*)_0*(?<id>[1-9]?\\d*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
	}
}
