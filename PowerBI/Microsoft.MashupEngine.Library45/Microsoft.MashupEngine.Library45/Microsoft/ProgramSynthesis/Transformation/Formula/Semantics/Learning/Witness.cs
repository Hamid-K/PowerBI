using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Concat;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Contracts;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.ProgramFirst;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.ProgramFirst.Models;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001681 RID: 5761
	internal class Witness : WitnessBase
	{
		// Token: 0x0600C075 RID: 49269 RVA: 0x0029745C File Offset: 0x0029565C
		private IEnumerable<decimal> AddLeft824(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			IEnumerable<Record<decimal, decimal>> enumerable;
			if (!this._recognition.TryAdd824(context.InputRow, operatorOutput, out enumerable))
			{
				return null;
			}
			return enumerable.Select((Record<decimal, decimal> pair) => pair.Item1);
		}

		// Token: 0x0600C076 RID: 49270 RVA: 0x002974C8 File Offset: 0x002956C8
		private IEnumerable<decimal> AddRight824(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			object dependentArg = context.DependentArg1;
			if (dependentArg is decimal)
			{
				decimal left = (decimal)dependentArg;
				IEnumerable<Record<decimal, decimal>> enumerable;
				if (this._recognition.TryAdd824(context.InputRow, operatorOutput, out enumerable))
				{
					return from pair in enumerable
						where pair.Item1 == left
						select pair.Item2;
				}
			}
			return null;
		}

		// Token: 0x0600C077 RID: 49271 RVA: 0x00297568 File Offset: 0x00295768
		private IEnumerable<decimal[]> AverageSubject824(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			IEnumerable<decimal[]> enumerable;
			if (!this._recognition.TryAverage824(context.InputRow, operatorOutput, out enumerable))
			{
				return null;
			}
			return enumerable;
		}

		// Token: 0x0600C078 RID: 49272 RVA: 0x002975B0 File Offset: 0x002957B0
		private IEnumerable<decimal> DivideLeft824(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			IEnumerable<Record<decimal, decimal>> enumerable;
			if (!this._recognition.TryDivide824(context.InputRow, operatorOutput, out enumerable))
			{
				return null;
			}
			return enumerable.Select((Record<decimal, decimal> pair) => pair.Item1);
		}

		// Token: 0x0600C079 RID: 49273 RVA: 0x0029761C File Offset: 0x0029581C
		private IEnumerable<decimal> DivideRight824(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			object dependentArg = context.DependentArg1;
			if (dependentArg is decimal)
			{
				decimal left = (decimal)dependentArg;
				IEnumerable<Record<decimal, decimal>> enumerable;
				if (this._recognition.TryDivide824(context.InputRow, operatorOutput, out enumerable))
				{
					return from pair in enumerable
						where pair.Item1 == left
						select pair.Item2;
				}
			}
			return null;
		}

		// Token: 0x0600C07A RID: 49274 RVA: 0x002976BC File Offset: 0x002958BC
		private IEnumerable<decimal> MultiplyLeft824(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			IEnumerable<Record<decimal, decimal>> enumerable;
			if (!this._recognition.TryMultiply824(context.InputRow, operatorOutput, out enumerable))
			{
				return null;
			}
			return enumerable.Select((Record<decimal, decimal> pair) => pair.Item1);
		}

		// Token: 0x0600C07B RID: 49275 RVA: 0x00297728 File Offset: 0x00295928
		private IEnumerable<decimal> MultiplyRight824(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			object dependentArg = context.DependentArg1;
			if (dependentArg is decimal)
			{
				decimal left = (decimal)dependentArg;
				IEnumerable<Record<decimal, decimal>> enumerable;
				if (this._recognition.TryMultiply824(context.InputRow, operatorOutput, out enumerable))
				{
					return from pair in enumerable
						where pair.Item1 == left
						select pair.Item2;
				}
			}
			return null;
		}

		// Token: 0x0600C07C RID: 49276 RVA: 0x002977C8 File Offset: 0x002959C8
		private IEnumerable<decimal[]> ProductSubject824(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			IEnumerable<decimal[]> enumerable;
			if (!this._recognition.TryProduct824(context.InputRow, operatorOutput, out enumerable))
			{
				return null;
			}
			return enumerable;
		}

		// Token: 0x0600C07D RID: 49277 RVA: 0x00297810 File Offset: 0x00295A10
		private IEnumerable<decimal> SubtractLeft824(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			IEnumerable<Record<decimal, decimal>> enumerable;
			if (!this._recognition.TrySubtract824(context.InputRow, operatorOutput, out enumerable))
			{
				return null;
			}
			return enumerable.Select((Record<decimal, decimal> pair) => pair.Item1);
		}

		// Token: 0x0600C07E RID: 49278 RVA: 0x0029787C File Offset: 0x00295A7C
		private IEnumerable<decimal> SubtractRight824(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			object dependentArg = context.DependentArg1;
			if (dependentArg is decimal)
			{
				decimal left = (decimal)dependentArg;
				IEnumerable<Record<decimal, decimal>> enumerable;
				if (this._recognition.TrySubtract824(context.InputRow, operatorOutput, out enumerable))
				{
					return from pair in enumerable
						where pair.Item1 == left
						select pair.Item2;
				}
			}
			return null;
		}

		// Token: 0x0600C07F RID: 49279 RVA: 0x0029791C File Offset: 0x00295B1C
		private IEnumerable<decimal[]> SumSubject824(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			IEnumerable<decimal[]> enumerable;
			if (!this._recognition.TrySum824(context.InputRow, operatorOutput, out enumerable))
			{
				return null;
			}
			return enumerable;
		}

		// Token: 0x0600C080 RID: 49280 RVA: 0x00297964 File Offset: 0x00295B64
		private static IEnumerable<string[]> FromNumbersColumnNames824(WitnessContext<decimal[]> context)
		{
			decimal[] result = context.OperatorOutput;
			decimal[] result2 = result;
			bool flag = !(((result2 != null) ? new bool?(result2.Any<decimal>()) : null) ?? false);
			if (flag || result.Length < 2)
			{
				return null;
			}
			string[] array = (from columnName in context.InputRow.ColumnNames
				let value = Operators.FromNumber(context.InputRow, columnName)
				where value != null && result.Contains(value.Value)
				orderby columnName
				select columnName).ToArray<string>();
			if (!array.Any<string>())
			{
				return null;
			}
			return array.Yield<string[]>();
		}

		// Token: 0x0600C081 RID: 49281 RVA: 0x00297A68 File Offset: 0x00295C68
		private IEnumerable<decimal> AddLeft(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			IReadOnlyList<ArithmeticPair> readOnlyList;
			if (!this._recognition.TryAdd(context.InputRow, operatorOutput, out readOnlyList))
			{
				return null;
			}
			return readOnlyList.Select((ArithmeticPair pair) => pair.Left);
		}

		// Token: 0x0600C082 RID: 49282 RVA: 0x00297AD4 File Offset: 0x00295CD4
		private IEnumerable<decimal> AddRight(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			object dependentArg = context.DependentArg1;
			if (dependentArg is decimal)
			{
				decimal left = (decimal)dependentArg;
				IReadOnlyList<ArithmeticPair> readOnlyList;
				if (this._recognition.TryAdd(context.InputRow, operatorOutput, out readOnlyList))
				{
					return from pair in readOnlyList
						where pair.Left == left
						select pair.Right;
				}
			}
			return null;
		}

		// Token: 0x0600C083 RID: 49283 RVA: 0x00297B74 File Offset: 0x00295D74
		private IEnumerable<decimal[]> AverageSubject(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			ArithmeticSetList arithmeticSetList;
			if (!this._recognition.TryAverage(context.InputRow, operatorOutput, out arithmeticSetList))
			{
				return null;
			}
			return arithmeticSetList.Select((ArithmeticSet set) => set.Values).Distinct(this._decimalArrayEquality);
		}

		// Token: 0x0600C084 RID: 49284 RVA: 0x00297BEC File Offset: 0x00295DEC
		private IEnumerable<decimal> DivideLeft(WitnessContext<decimal> context)
		{
			decimal operatorOutput = context.OperatorOutput;
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			Dictionary<IRow, IReadOnlyList<decimal>> dictionary = context.DisjunctiveContexts.ToLookupDictionary((WitnessDisjunctiveContext<decimal> c) => c.InputRow, (WitnessDisjunctiveContext<decimal> c) => c.OperatorOutputs);
			IReadOnlyList<ArithmeticPair> readOnlyList;
			if (!this._recognition.TryDivide(context.InputRow, operatorOutput, out readOnlyList, dictionary))
			{
				return null;
			}
			return readOnlyList.Select((ArithmeticPair pair) => pair.Left);
		}

		// Token: 0x0600C085 RID: 49285 RVA: 0x00297CA4 File Offset: 0x00295EA4
		private IEnumerable<decimal> DivideRight(WitnessContext<decimal> context)
		{
			decimal operatorOutput = context.OperatorOutput;
			if (base.Examples.Count >= base.Options.ArithmeticMinExampleCount)
			{
				object dependentArg = context.DependentArg1;
				if (dependentArg is decimal)
				{
					decimal left = (decimal)dependentArg;
					Dictionary<IRow, IReadOnlyList<decimal>> dictionary = context.DisjunctiveContexts.ToLookupDictionary((WitnessDisjunctiveContext<decimal> c) => c.InputRow, (WitnessDisjunctiveContext<decimal> c) => c.OperatorOutputs);
					IReadOnlyList<ArithmeticPair> readOnlyList;
					if (!this._recognition.TryDivide(context.InputRow, operatorOutput, out readOnlyList, dictionary))
					{
						return null;
					}
					return from pair in readOnlyList
						where pair.Left == left
						select pair.Right;
				}
			}
			return null;
		}

		// Token: 0x0600C086 RID: 49286 RVA: 0x00297D94 File Offset: 0x00295F94
		private IEnumerable<decimal> MultiplyLeft(WitnessContext<decimal> context)
		{
			decimal operatorOutput = context.OperatorOutput;
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			Dictionary<IRow, IReadOnlyList<decimal>> dictionary = context.DisjunctiveContexts.ToLookupDictionary((WitnessDisjunctiveContext<decimal> c) => c.InputRow, (WitnessDisjunctiveContext<decimal> c) => c.OperatorOutputs);
			IReadOnlyList<ArithmeticPair> readOnlyList;
			if (!this._recognition.TryMultiply(context.InputRow, operatorOutput, out readOnlyList, dictionary))
			{
				return null;
			}
			return readOnlyList.Select((ArithmeticPair pair) => pair.Left);
		}

		// Token: 0x0600C087 RID: 49287 RVA: 0x00297E4C File Offset: 0x0029604C
		private IEnumerable<decimal> MultiplyRight(WitnessContext<decimal> context)
		{
			decimal operatorOutput = context.OperatorOutput;
			if (base.Examples.Count >= base.Options.ArithmeticMinExampleCount)
			{
				object dependentArg = context.DependentArg1;
				if (dependentArg is decimal)
				{
					decimal left = (decimal)dependentArg;
					Dictionary<IRow, IReadOnlyList<decimal>> dictionary = context.DisjunctiveContexts.ToLookupDictionary((WitnessDisjunctiveContext<decimal> c) => c.InputRow, (WitnessDisjunctiveContext<decimal> c) => c.OperatorOutputs);
					IReadOnlyList<ArithmeticPair> readOnlyList;
					if (!this._recognition.TryMultiply(context.InputRow, operatorOutput, out readOnlyList, dictionary))
					{
						return null;
					}
					return from pair in readOnlyList
						where pair.Left == left
						select pair.Right;
				}
			}
			return null;
		}

		// Token: 0x0600C088 RID: 49288 RVA: 0x00297F3C File Offset: 0x0029613C
		private IEnumerable<decimal[]> ProductSubject(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			ArithmeticSetList arithmeticSetList;
			if (!this._recognition.TryProduct(context.InputRow, operatorOutput, out arithmeticSetList))
			{
				return null;
			}
			return arithmeticSetList.Select((ArithmeticSet set) => set.Values).Distinct(this._decimalArrayEquality);
		}

		// Token: 0x0600C089 RID: 49289 RVA: 0x00297FB4 File Offset: 0x002961B4
		private IEnumerable<decimal> SubtractLeft(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			IReadOnlyList<ArithmeticPair> readOnlyList;
			if (!this._recognition.TrySubtract(context.InputRow, operatorOutput, out readOnlyList))
			{
				return null;
			}
			return readOnlyList.Select((ArithmeticPair pair) => pair.Left);
		}

		// Token: 0x0600C08A RID: 49290 RVA: 0x00298020 File Offset: 0x00296220
		private IEnumerable<decimal> SubtractRight(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			object dependentArg = context.DependentArg1;
			if (dependentArg is decimal)
			{
				decimal left = (decimal)dependentArg;
				IReadOnlyList<ArithmeticPair> readOnlyList;
				if (this._recognition.TrySubtract(context.InputRow, operatorOutput, out readOnlyList))
				{
					return from pair in readOnlyList
						where pair.Left == left
						select pair.Right;
				}
			}
			return null;
		}

		// Token: 0x0600C08B RID: 49291 RVA: 0x002980C0 File Offset: 0x002962C0
		private IEnumerable<decimal[]> SumSubject(WitnessContext<decimal> context)
		{
			if (base.Examples.Count < base.Options.ArithmeticMinExampleCount)
			{
				return null;
			}
			decimal operatorOutput = context.OperatorOutput;
			ArithmeticSetList arithmeticSetList;
			if (!this._recognition.TrySum(context.InputRow, operatorOutput, out arithmeticSetList))
			{
				return null;
			}
			return arithmeticSetList.Select((ArithmeticSet set) => set.Values).Distinct(this._decimalArrayEquality);
		}

		// Token: 0x0600C08C RID: 49292 RVA: 0x00298138 File Offset: 0x00296338
		private IEnumerable<string[]> FromNumbersColumnNames(WitnessContext<decimal[]> context)
		{
			decimal[] result = context.OperatorOutput;
			decimal[] result2 = result;
			bool flag = !(((result2 != null) ? new bool?(result2.Any<decimal>()) : null) ?? false);
			if (flag || result.Length <= 2)
			{
				return null;
			}
			ArrayEquality<decimal> decimalArrayEquality = new ArrayEquality<decimal>();
			ArrayEquality<string> arrayEquality = new ArrayEquality<string>();
			return (from set in this._recognition.ArithmeticSourceSets(context.InputRow).Where(delegate(ArithmeticSet set)
				{
					ArithmeticSetList arithmeticSetList;
					return decimalArrayEquality.Equals(set.Values, result) && ((set.Sum != null && this._recognition.TrySum(context.InputRow, set.Sum.Value, out arithmeticSetList)) || (set.Product != null && this._recognition.TryProduct(context.InputRow, set.Product.Value, out arithmeticSetList)) || (set.Average != null && this._recognition.TryAverage(context.InputRow, set.Average.Value, out arithmeticSetList)));
				})
				select set.ColumnNames).Distinct(arrayEquality);
		}

		// Token: 0x0600C08D RID: 49293 RVA: 0x00298218 File Offset: 0x00296418
		private IEnumerable<string> LowerCase(WitnessContext<string> context)
		{
			string operatorOutput = context.OperatorOutput;
			if (string.IsNullOrEmpty(operatorOutput) || Operators.LowerCase(operatorOutput) != operatorOutput)
			{
				return null;
			}
			return this._recognition.ToShiftCase(context.InputRow, operatorOutput);
		}

		// Token: 0x0600C08E RID: 49294 RVA: 0x00298258 File Offset: 0x00296458
		private IEnumerable<string> ProperCase(WitnessContext<string> context)
		{
			if (!base.Options.EnableProperCase)
			{
				return null;
			}
			string operatorOutput = context.OperatorOutput;
			if (string.IsNullOrEmpty(operatorOutput) || Operators.ProperCase(operatorOutput) != operatorOutput)
			{
				return null;
			}
			return this._recognition.ToShiftCase(context.InputRow, operatorOutput);
		}

		// Token: 0x0600C08F RID: 49295 RVA: 0x002982A8 File Offset: 0x002964A8
		private IEnumerable<string> UpperCase(WitnessContext<string> context)
		{
			string operatorOutput = context.OperatorOutput;
			if (string.IsNullOrEmpty(operatorOutput) || Operators.UpperCase(operatorOutput) != operatorOutput)
			{
				return null;
			}
			return this._recognition.ToShiftCase(context.InputRow, operatorOutput);
		}

		// Token: 0x0600C090 RID: 49296 RVA: 0x002982E8 File Offset: 0x002964E8
		private IEnumerable<string> LengthSubject(WitnessContext<decimal> context)
		{
			if (!base.Options.EnableLength)
			{
				return null;
			}
			decimal result = context.OperatorOutput;
			return this._recognition.InputStrings(context.InputRow, null).DistinctValues.Where((string input) => result != 0m && result == input.Length);
		}

		// Token: 0x0600C091 RID: 49297 RVA: 0x00298340 File Offset: 0x00296540
		private IEnumerable<string> LetX(WitnessContext<string> context)
		{
			if (string.IsNullOrEmpty(context.OperatorOutput))
			{
				return null;
			}
			IReadOnlyList<string> distinctValues = this._recognition.InputStrings(context.InputRow, null).DistinctValues;
			IEnumerable<string> enumerable = distinctValues;
			IEnumerable<string> enumerable2 = distinctValues;
			Func<string, string> func;
			if ((func = Witness.<>O.<0>__Trim) == null)
			{
				func = (Witness.<>O.<0>__Trim = new Func<string, string>(Operators.Trim));
			}
			IEnumerable<string> enumerable3 = enumerable.Union(enumerable2.Select(func));
			IEnumerable<string> enumerable5;
			if (!base.Options.EnableTrimFull)
			{
				IEnumerable<string> enumerable4 = new string[0];
				enumerable5 = enumerable4;
			}
			else
			{
				IEnumerable<string> enumerable6 = distinctValues;
				Func<string, string> func2;
				if ((func2 = Witness.<>O.<1>__TrimFull) == null)
				{
					func2 = (Witness.<>O.<1>__TrimFull = new Func<string, string>(Operators.TrimFull));
				}
				enumerable5 = enumerable6.Select(func2);
			}
			return enumerable3.Union(enumerable5);
		}

		// Token: 0x0600C092 RID: 49298 RVA: 0x002983D8 File Offset: 0x002965D8
		private IEnumerable<string> TrimSubject(WitnessContext<string> context)
		{
			string result = context.OperatorOutput;
			IReadOnlyList<SubstringDescriptor> readOnlyList;
			if (!base.Options.EnableTrim || string.IsNullOrEmpty(result) || !this._recognition.TrySubstring(context.InputRow, result, out readOnlyList, false))
			{
				return null;
			}
			return from descriptor in readOnlyList
				let expandedResult = Utils.ExpandWhitespace(descriptor.Input, result, descriptor.StartIndex)
				let trimmed = Operators.Trim(expandedResult)
				where !string.IsNullOrEmpty(trimmed) && trimmed == result
				select expandedResult;
		}

		// Token: 0x0600C093 RID: 49299 RVA: 0x0029849C File Offset: 0x0029669C
		private IEnumerable<string> TrimFullSubject(WitnessContext<string> context)
		{
			string result = context.OperatorOutput;
			IReadOnlyList<SubstringDescriptor> readOnlyList;
			if (!base.Options.EnableTrimFull || string.IsNullOrEmpty(result) || !this._recognition.TrySubstring(context.InputRow, result, out readOnlyList, false))
			{
				return null;
			}
			return from descriptor in readOnlyList
				let expandedResult = Utils.ExpandWhitespace(descriptor.Input, result, descriptor.StartIndex)
				let trimmed = Operators.TrimFull(expandedResult)
				where !string.IsNullOrEmpty(trimmed) && trimmed == result
				select expandedResult;
		}

		// Token: 0x0600C094 RID: 49300 RVA: 0x00298560 File Offset: 0x00296760
		private IEnumerable<string> FromNumberStrColumnName(WitnessContext<string> context)
		{
			if (!base.Options.EnableFromNumberStr)
			{
				return null;
			}
			string result = context.OperatorOutput;
			return context.InputRow.ColumnNames.Where((string columnName) => Operators.FromNumberStr(context.InputRow, columnName) == result);
		}

		// Token: 0x0600C095 RID: 49301 RVA: 0x002985BC File Offset: 0x002967BC
		private static IEnumerable<string> FromStrColumnName(WitnessContext<string> context)
		{
			string result = context.OperatorOutput;
			return context.InputRow.ColumnNames.Where((string columnName) => Operators.FromStr(context.InputRow, columnName) == result);
		}

		// Token: 0x0600C096 RID: 49302 RVA: 0x00298608 File Offset: 0x00296808
		private static IEnumerable<string> ToStrSubject(WitnessContext<string> context)
		{
			return context.OperatorOutput.Yield<string>();
		}

		// Token: 0x0600C097 RID: 49303 RVA: 0x00298615 File Offset: 0x00296815
		private static IEnumerable<DateTime> Date(WitnessContext<DateTime> context)
		{
			return context.OperatorOutput.Yield<DateTime>();
		}

		// Token: 0x0600C098 RID: 49304 RVA: 0x00298624 File Offset: 0x00296824
		private static IEnumerable<decimal> AddRightNumber(WitnessContext<decimal> context)
		{
			decimal operatorOutput = context.OperatorOutput;
			if (!(operatorOutput > 0m))
			{
				return null;
			}
			return operatorOutput.Yield<decimal>();
		}

		// Token: 0x0600C099 RID: 49305 RVA: 0x00298650 File Offset: 0x00296850
		private static IEnumerable<decimal> SubtractRightNumber(WitnessContext<decimal> context)
		{
			decimal operatorOutput = context.OperatorOutput;
			if (!(operatorOutput > 0m))
			{
				return null;
			}
			return operatorOutput.Yield<decimal>();
		}

		// Token: 0x0600C09A RID: 49306 RVA: 0x0029867C File Offset: 0x0029687C
		private static IEnumerable<decimal> MultiplyRightNumber(WitnessContext<decimal> context)
		{
			decimal operatorOutput = context.OperatorOutput;
			if (!(operatorOutput <= -1m) && !(operatorOutput > 1m))
			{
				return null;
			}
			return operatorOutput.Yield<decimal>();
		}

		// Token: 0x0600C09B RID: 49307 RVA: 0x002986BC File Offset: 0x002968BC
		private static IEnumerable<decimal> DivideRightNumber(WitnessContext<decimal> context)
		{
			decimal operatorOutput = context.OperatorOutput;
			if (!(operatorOutput < -1m) && !(operatorOutput > 1m))
			{
				return null;
			}
			return operatorOutput.Yield<decimal>();
		}

		// Token: 0x0600C09C RID: 49308 RVA: 0x002986FB File Offset: 0x002968FB
		private static IEnumerable<decimal> Number(WitnessContext<decimal> context)
		{
			return context.OperatorOutput.Yield<decimal>();
		}

		// Token: 0x0600C09D RID: 49309 RVA: 0x00298708 File Offset: 0x00296908
		private IEnumerable<string> Str(WitnessContext<string> context)
		{
			string operatorOutput = context.OperatorOutput;
			if (operatorOutput == null)
			{
				return null;
			}
			if (operatorOutput == string.Empty || operatorOutput.AllDelimiters())
			{
				return operatorOutput.Yield<string>();
			}
			if (!this._recognition.Contains(context.InputRow, operatorOutput.AsSpan(), false))
			{
				return operatorOutput.Yield<string>();
			}
			return null;
		}

		// Token: 0x0600C09E RID: 49310 RVA: 0x0029875F File Offset: 0x0029695F
		private IEnumerable<string> ConcatPrefix(WitnessContext<string> context)
		{
			if (base.Options.EnableConcat)
			{
				return this._concatStrategy.Prefixes(context);
			}
			return null;
		}

		// Token: 0x0600C09F RID: 49311 RVA: 0x0029877C File Offset: 0x0029697C
		private IEnumerable<string> ConcatSuffix(WitnessContext<string> context)
		{
			string operatorOutput = context.OperatorOutput;
			if (base.Options.EnableConcat)
			{
				string text = context.DependentArg1 as string;
				if (text != null && !string.IsNullOrEmpty(text))
				{
					if (!text.Length.IsValidIndex(operatorOutput))
					{
						return null;
					}
					return operatorOutput.Substring(text.Length).Yield<string>();
				}
			}
			return null;
		}

		// Token: 0x0600C0A0 RID: 49312 RVA: 0x002987D8 File Offset: 0x002969D8
		[RuleLearner("If")]
		internal Optional<ProgramSet> LearnIf(SynthesisEngine engine, BlackBoxRule rule, LearningTask<Spec> task, CancellationToken cancel)
		{
			if (!base.Options.EnableConditional || this._learnBranch == null)
			{
				return OptionalUtils.Some((T)null);
			}
			LearnDebugTrace debugTrace = base.DebugTrace;
			Optional<ProgramSet> optional;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Learner", "If", false, true) : null))
			{
				IReadOnlyList<IConditionalBranch[]> readOnlyList = new ProgramFirstConditionalStrategy(base.Examples, base.AdditionalInputs, this._learnBranch, base.Grammar, base.Options.EnableMatch, base.Options.ConditionalMaxBranches, base.Options.ConditionalBranchMinExampleCount, base.DebugTrace).Paths().Take(1).ToReadOnlyList<ConditionalBranch[]>();
				LearnDebugTrace debugTrace2 = base.DebugTrace;
				using (TimedEvent timedEvent2 = ((debugTrace2 != null) ? debugTrace2.StartTimedEvent("Learner", "If/Construct", false, true) : null))
				{
					List<ProgramNode> list = (from path in readOnlyList
						where path != null && path.Length > 1
						select this.<LearnIf>g__ResolveIfNode|44_3(path, 0)).Select(delegate(result ifNode)
					{
						result result = ifNode;
						return result.Node;
					}).ToList<ProgramNode>();
					if (timedEvent2 != null)
					{
						timedEvent2.Stop();
					}
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					LearnDebugTrace debugTrace3 = base.DebugTrace;
					if (debugTrace3 != null)
					{
						debugTrace3.WitnessEvents.Add(new WitnessEvent
						{
							WitnessName = "If"
						});
					}
					optional = ProgramSet.List(base.Builder.Symbol.result, list).Some<ProgramSet>();
				}
			}
			return optional;
		}

		// Token: 0x0600C0A1 RID: 49313 RVA: 0x00298998 File Offset: 0x00296B98
		private condition ResolveConditionNode(Predicate predicate)
		{
			GrammarBuilders.Nodes.NodeRules ruleBuilder = base.Builder.Node.Rule;
			OrPredicate orPredicate = predicate as OrPredicate;
			if (orPredicate != null)
			{
				return orPredicate.Children.Skip(1).Aggregate(this.ResolveConditionNode(orPredicate.Children.First<Predicate>()), (condition current, Predicate childPredicate) => new condition_or(this.Builder, ruleBuilder.Or(current, this.ResolveConditionNode(childPredicate))));
			}
			StartsWithPredicate startsWithPredicate = predicate as StartsWithPredicate;
			condition condition;
			if (startsWithPredicate == null)
			{
				StartsWithDigitPredicate startsWithDigitPredicate = predicate as StartsWithDigitPredicate;
				if (startsWithDigitPredicate == null)
				{
					EndsWithDigitPredicate endsWithDigitPredicate = predicate as EndsWithDigitPredicate;
					if (endsWithDigitPredicate == null)
					{
						StringEqualsPredicate stringEqualsPredicate = predicate as StringEqualsPredicate;
						if (stringEqualsPredicate == null)
						{
							ContainsPredicate containsPredicate = predicate as ContainsPredicate;
							if (containsPredicate == null)
							{
								NumberEqualsPredicate numberEqualsPredicate = predicate as NumberEqualsPredicate;
								if (numberEqualsPredicate == null)
								{
									GreaterThanPredicate greaterThanPredicate = predicate as GreaterThanPredicate;
									if (greaterThanPredicate == null)
									{
										LessThanPredicate lessThanPredicate = predicate as LessThanPredicate;
										if (lessThanPredicate == null)
										{
											IsBlankPredicate isBlankPredicate = predicate as IsBlankPredicate;
											if (isBlankPredicate == null)
											{
												IsNotBlankPredicate isNotBlankPredicate = predicate as IsNotBlankPredicate;
												if (isNotBlankPredicate == null)
												{
													IsStringPredicate isStringPredicate = predicate as IsStringPredicate;
													if (isStringPredicate == null)
													{
														IsNumberPredicate isNumberPredicate = predicate as IsNumberPredicate;
														if (isNumberPredicate == null)
														{
															IsMatchPredicate isMatchPredicate = predicate as IsMatchPredicate;
															if (isMatchPredicate == null)
															{
																ContainsMatchPredicate containsMatchPredicate = predicate as ContainsMatchPredicate;
																if (containsMatchPredicate == null)
																{
																	throw new Exception(string.Format("Invalid predicate, {0}: {1}", predicate.GetType().Name, predicate));
																}
																condition = ruleBuilder.ContainsMatch(new row(base.Builder), ruleBuilder.columnName(containsMatchPredicate.ColumnName), ruleBuilder.containsMatchRegex(containsMatchPredicate.Pattern), ruleBuilder.matchCount(containsMatchPredicate.Count));
															}
															else
															{
																condition = ruleBuilder.IsMatch(new row(base.Builder), ruleBuilder.columnName(isMatchPredicate.ColumnName), ruleBuilder.isMatchRegex(isMatchPredicate.Pattern));
															}
														}
														else
														{
															condition = ruleBuilder.IsNumber(new row(base.Builder), ruleBuilder.columnName(isNumberPredicate.ColumnName));
														}
													}
													else
													{
														condition = ruleBuilder.IsString(new row(base.Builder), ruleBuilder.columnName(isStringPredicate.ColumnName));
													}
												}
												else
												{
													condition = ruleBuilder.IsNotBlank(new row(base.Builder), ruleBuilder.columnName(isNotBlankPredicate.ColumnName));
												}
											}
											else
											{
												condition = ruleBuilder.IsBlank(new row(base.Builder), ruleBuilder.columnName(isBlankPredicate.ColumnName));
											}
										}
										else
										{
											condition = ruleBuilder.NumberLessThan(new row(base.Builder), ruleBuilder.columnName(lessThanPredicate.ColumnName), ruleBuilder.numberLessThanValue(lessThanPredicate.Value));
										}
									}
									else
									{
										condition = ruleBuilder.NumberGreaterThan(new row(base.Builder), ruleBuilder.columnName(greaterThanPredicate.ColumnName), ruleBuilder.numberGreaterThanValue(greaterThanPredicate.Value));
									}
								}
								else
								{
									condition = ruleBuilder.NumberEquals(new row(base.Builder), ruleBuilder.columnName(numberEqualsPredicate.ColumnName), ruleBuilder.numberEqualsValue(numberEqualsPredicate.Value));
								}
							}
							else
							{
								condition = ruleBuilder.Contains(new row(base.Builder), ruleBuilder.columnName(containsPredicate.ColumnName), ruleBuilder.containsFindText(containsPredicate.FindText), ruleBuilder.containsCount(containsPredicate.Count));
							}
						}
						else
						{
							condition = ruleBuilder.StringEquals(new row(base.Builder), ruleBuilder.columnName(stringEqualsPredicate.ColumnName), ruleBuilder.equalsText(stringEqualsPredicate.Value));
						}
					}
					else
					{
						condition = ruleBuilder.EndsWithDigit(new row(base.Builder), ruleBuilder.columnName(endsWithDigitPredicate.ColumnName));
					}
				}
				else
				{
					condition = ruleBuilder.StartsWithDigit(new row(base.Builder), ruleBuilder.columnName(startsWithDigitPredicate.ColumnName));
				}
			}
			else
			{
				condition = ruleBuilder.StartsWith(new row(base.Builder), ruleBuilder.columnName(startsWithPredicate.ColumnName), ruleBuilder.startsWithFindText(startsWithPredicate.FindText));
			}
			return condition;
		}

		// Token: 0x0600C0A2 RID: 49314 RVA: 0x00298E2F File Offset: 0x0029702F
		internal Witness(Grammar grammar, LearnOptions options, Recognition recognition, IReadOnlyList<Example<IRow, object>> examples, IEnumerable<IRow> additionalInputs, CancellationToken cancellation, Func<IEnumerable<Example<IRow, object>>, ConditionalBranchMeta> learnBranch, LearnDebugTrace debugTrace = null)
			: base(grammar, options, examples, additionalInputs, cancellation, debugTrace)
		{
			this._learnBranch = learnBranch;
			this._recognition = recognition;
			this._concatStrategy = new DefaultConcatStrategy(this._recognition, cancellation);
		}

		// Token: 0x0600C0A3 RID: 49315 RVA: 0x00298E6E File Offset: 0x0029706E
		[WitnessFunction("LowerCase", 0)]
		[WitnessFunction("LowerCaseConcat", 0)]
		public DisjunctiveExamplesSpec WitnessLowerCase(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			if (!this._recognition.CanShiftCase())
			{
				return null;
			}
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.LowerCase), null, null, null, "WitnessLowerCase");
		}

		// Token: 0x0600C0A4 RID: 49316 RVA: 0x00298E9B File Offset: 0x0029709B
		[WitnessFunction("ProperCase", 0)]
		[WitnessFunction("ProperCaseConcat", 0)]
		public DisjunctiveExamplesSpec WitnessProperCase(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			if (!this._recognition.CanShiftCase())
			{
				return null;
			}
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.ProperCase), null, null, null, "WitnessProperCase");
		}

		// Token: 0x0600C0A5 RID: 49317 RVA: 0x00298EC8 File Offset: 0x002970C8
		[WitnessFunction("UpperCase", 0)]
		[WitnessFunction("UpperCaseConcat", 0)]
		public DisjunctiveExamplesSpec WitnessUpperCase(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			if (!this._recognition.CanShiftCase())
			{
				return null;
			}
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.UpperCase), null, null, null, "WitnessUpperCase");
		}

		// Token: 0x0600C0A6 RID: 49318 RVA: 0x00298EF5 File Offset: 0x002970F5
		[WitnessFunction("Concat", 0)]
		public DisjunctiveExamplesSpec WitnessConcatPrefix(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.ConcatPrefix), null, null, null, "WitnessConcatPrefix");
		}

		// Token: 0x0600C0A7 RID: 49319 RVA: 0x00298F13 File Offset: 0x00297113
		[WitnessFunction("Concat", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessConcatSuffix(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.ConcatSuffix), arg0Spec, null, null, "WitnessConcatSuffix");
		}

		// Token: 0x0600C0A8 RID: 49320 RVA: 0x00298F31 File Offset: 0x00297131
		[WitnessFunction("FromNumberStr", 1)]
		public DisjunctiveExamplesSpec WitnessFromNumberStrColumnName(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.FromNumberStrColumnName), null, null, null, "WitnessFromNumberStrColumnName");
		}

		// Token: 0x0600C0A9 RID: 49321 RVA: 0x00298F4F File Offset: 0x0029714F
		[WitnessFunction("FromStr", 1)]
		public DisjunctiveExamplesSpec WitnessFromStrColumnName(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<WitnessContext<string>, IEnumerable<string>> func;
			if ((func = Witness.<>O.<2>__FromStrColumnName) == null)
			{
				func = (Witness.<>O.<2>__FromStrColumnName = new Func<WitnessContext<string>, IEnumerable<string>>(Witness.FromStrColumnName));
			}
			return base.WitnessOutput<string, string>(rule, spec, func, null, null, null, "WitnessFromStrColumnName");
		}

		// Token: 0x0600C0AA RID: 49322 RVA: 0x00298F7C File Offset: 0x0029717C
		[WitnessFunction("LetX", 0)]
		public DisjunctiveExamplesSpec WitnessLetX(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.LetX), null, null, null, "WitnessLetX");
		}

		// Token: 0x0600C0AB RID: 49323 RVA: 0x00298F9A File Offset: 0x0029719A
		[WitnessFunction("AddRightNumber", 0)]
		public DisjunctiveExamplesSpec WitnessAddArithmeticNumber(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<WitnessContext<decimal>, IEnumerable<decimal>> func;
			if ((func = Witness.<>O.<3>__AddRightNumber) == null)
			{
				func = (Witness.<>O.<3>__AddRightNumber = new Func<WitnessContext<decimal>, IEnumerable<decimal>>(Witness.AddRightNumber));
			}
			return base.WitnessOutput<decimal, decimal>(rule, spec, func, null, null, null, "WitnessAddArithmeticNumber");
		}

		// Token: 0x0600C0AC RID: 49324 RVA: 0x00298FC7 File Offset: 0x002971C7
		[WitnessFunction("SubtractRightNumber", 0)]
		public DisjunctiveExamplesSpec WitnessSubtractArithmeticNumber(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<WitnessContext<decimal>, IEnumerable<decimal>> func;
			if ((func = Witness.<>O.<4>__SubtractRightNumber) == null)
			{
				func = (Witness.<>O.<4>__SubtractRightNumber = new Func<WitnessContext<decimal>, IEnumerable<decimal>>(Witness.SubtractRightNumber));
			}
			return base.WitnessOutput<decimal, decimal>(rule, spec, func, null, null, null, "WitnessSubtractArithmeticNumber");
		}

		// Token: 0x0600C0AD RID: 49325 RVA: 0x00298FF4 File Offset: 0x002971F4
		[WitnessFunction("MultiplyRightNumber", 0)]
		public DisjunctiveExamplesSpec WitnessMultiplyArithmeticNumber(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<WitnessContext<decimal>, IEnumerable<decimal>> func;
			if ((func = Witness.<>O.<5>__MultiplyRightNumber) == null)
			{
				func = (Witness.<>O.<5>__MultiplyRightNumber = new Func<WitnessContext<decimal>, IEnumerable<decimal>>(Witness.MultiplyRightNumber));
			}
			return base.WitnessOutput<decimal, decimal>(rule, spec, func, null, null, null, "WitnessMultiplyArithmeticNumber");
		}

		// Token: 0x0600C0AE RID: 49326 RVA: 0x00299021 File Offset: 0x00297221
		[WitnessFunction("DivideRightNumber", 0)]
		public DisjunctiveExamplesSpec WitnessDivideArithmeticNumber(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<WitnessContext<decimal>, IEnumerable<decimal>> func;
			if ((func = Witness.<>O.<6>__DivideRightNumber) == null)
			{
				func = (Witness.<>O.<6>__DivideRightNumber = new Func<WitnessContext<decimal>, IEnumerable<decimal>>(Witness.DivideRightNumber));
			}
			return base.WitnessOutput<decimal, decimal>(rule, spec, func, null, null, null, "WitnessDivideArithmeticNumber");
		}

		// Token: 0x0600C0AF RID: 49327 RVA: 0x0029904E File Offset: 0x0029724E
		[WitnessFunction("Date", 0)]
		public DisjunctiveExamplesSpec WitnessDate(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<WitnessContext<DateTime>, IEnumerable<DateTime>> func;
			if ((func = Witness.<>O.<7>__Date) == null)
			{
				func = (Witness.<>O.<7>__Date = new Func<WitnessContext<DateTime>, IEnumerable<DateTime>>(Witness.Date));
			}
			return base.WitnessOutput<DateTime, DateTime>(rule, spec, func, null, null, null, "WitnessDate");
		}

		// Token: 0x0600C0B0 RID: 49328 RVA: 0x0029907B File Offset: 0x0029727B
		[WitnessFunction("Number", 0)]
		public DisjunctiveExamplesSpec WitnessNumber(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<WitnessContext<decimal>, IEnumerable<decimal>> func;
			if ((func = Witness.<>O.<8>__Number) == null)
			{
				func = (Witness.<>O.<8>__Number = new Func<WitnessContext<decimal>, IEnumerable<decimal>>(Witness.Number));
			}
			return base.WitnessOutput<decimal, decimal>(rule, spec, func, null, null, null, "WitnessNumber");
		}

		// Token: 0x0600C0B1 RID: 49329 RVA: 0x002990A8 File Offset: 0x002972A8
		[WitnessFunction("Str", 0)]
		public DisjunctiveExamplesSpec WitnessStr(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.Str), null, null, null, "WitnessStr");
		}

		// Token: 0x0600C0B2 RID: 49330 RVA: 0x002990C6 File Offset: 0x002972C6
		[WitnessFunction("Split", 0)]
		public DisjunctiveExamplesSpec WitnessSplitSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.SplitSubject), null, null, null, "WitnessSplitSubject");
		}

		// Token: 0x0600C0B3 RID: 49331 RVA: 0x002990E4 File Offset: 0x002972E4
		[WitnessFunction("Split", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessSplitDelimiter(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.SplitDelimiter), arg0Spec, null, null, "WitnessSplitDelimiter");
		}

		// Token: 0x0600C0B4 RID: 49332 RVA: 0x00299102 File Offset: 0x00297302
		[WitnessFunction("Split", 2, DependsOnParameters = new int[] { 0, 1 })]
		public DisjunctiveExamplesSpec WitnessSplitInstance(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec, ExampleSpec arg1Spec)
		{
			return base.WitnessOutput<string, int>(rule, spec, new Func<WitnessContext<string>, IEnumerable<int>>(this.SplitInstance), arg0Spec, arg1Spec, null, "WitnessSplitInstance");
		}

		// Token: 0x0600C0B5 RID: 49333 RVA: 0x00299121 File Offset: 0x00297321
		[WitnessFunction("Length", 0)]
		public DisjunctiveExamplesSpec WitnessLengthSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<decimal, string>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<string>>(this.LengthSubject), null, null, null, "WitnessLengthSubject");
		}

		// Token: 0x0600C0B6 RID: 49334 RVA: 0x0029913F File Offset: 0x0029733F
		[WitnessFunction("Slice", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessSlicePos1(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			Func<WitnessContext<string>, IEnumerable<int>> func;
			if ((func = Witness.<>O.<9>__SlicePos1) == null)
			{
				func = (Witness.<>O.<9>__SlicePos1 = new Func<WitnessContext<string>, IEnumerable<int>>(Witness.SlicePos1));
			}
			return base.WitnessOutput<string, int>(rule, spec, func, arg0Spec, null, null, "WitnessSlicePos1");
		}

		// Token: 0x0600C0B7 RID: 49335 RVA: 0x0029916C File Offset: 0x0029736C
		[WitnessFunction("Slice", 2, DependsOnParameters = new int[] { 0, 1 })]
		public DisjunctiveExamplesSpec WitnessSlicePos2(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec, ExampleSpec arg1Spec)
		{
			Func<WitnessContext<string>, IEnumerable<int>> func;
			if ((func = Witness.<>O.<10>__SlicePos2) == null)
			{
				func = (Witness.<>O.<10>__SlicePos2 = new Func<WitnessContext<string>, IEnumerable<int>>(Witness.SlicePos2));
			}
			return base.WitnessOutput<string, int>(rule, spec, func, arg0Spec, arg1Spec, null, "WitnessSlicePos2");
		}

		// Token: 0x0600C0B8 RID: 49336 RVA: 0x0029919A File Offset: 0x0029739A
		[WitnessFunction("SlicePrefixAbs", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessSlicePrefixAbsPos(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<string, int>(rule, spec, new Func<WitnessContext<string>, IEnumerable<int>>(this.SlicePrefixAbsPos), arg0Spec, null, null, "WitnessSlicePrefixAbsPos");
		}

		// Token: 0x0600C0B9 RID: 49337 RVA: 0x002991B8 File Offset: 0x002973B8
		[WitnessFunction("SlicePrefix", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessSlicePrefixPos(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<string, int>(rule, spec, new Func<WitnessContext<string>, IEnumerable<int>>(this.SlicePrefixPos), arg0Spec, null, null, "WitnessSlicePrefixPos");
		}

		// Token: 0x0600C0BA RID: 49338 RVA: 0x002991D6 File Offset: 0x002973D6
		[WitnessFunction("SliceSuffix", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessSliceSuffixPos(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<string, int>(rule, spec, new Func<WitnessContext<string>, IEnumerable<int>>(this.SliceSuffixPos), arg0Spec, null, null, "WitnessSliceSuffixPos");
		}

		// Token: 0x0600C0BB RID: 49339 RVA: 0x002991F4 File Offset: 0x002973F4
		[WitnessFunction("Trim", 0)]
		[WitnessFunction("TrimSplit", 0)]
		[WitnessFunction("TrimSlice", 0)]
		public DisjunctiveExamplesSpec WitnessTrimSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.TrimSubject), null, null, null, "WitnessTrimSubject");
		}

		// Token: 0x0600C0BC RID: 49340 RVA: 0x00299212 File Offset: 0x00297412
		[WitnessFunction("TrimFull", 0)]
		[WitnessFunction("TrimFullSplit", 0)]
		[WitnessFunction("TrimFullSlice", 0)]
		public DisjunctiveExamplesSpec WitnessTrimFullSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.TrimFullSubject), null, null, null, "WitnessTrimFullSubject");
		}

		// Token: 0x0600C0BD RID: 49341 RVA: 0x00299230 File Offset: 0x00297430
		[WitnessFunction("Replace", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessReplaceFindText(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.ReplaceFindText), arg0Spec, null, null, "WitnessReplaceFindText");
		}

		// Token: 0x0600C0BE RID: 49342 RVA: 0x0029924E File Offset: 0x0029744E
		[WitnessFunction("Replace", 2, DependsOnParameters = new int[] { 0, 1 })]
		public DisjunctiveExamplesSpec WitnessReplaceReplaceText(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec, ExampleSpec arg1Spec)
		{
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.ReplaceReplaceText), arg0Spec, arg1Spec, null, "WitnessReplaceReplaceText");
		}

		// Token: 0x0600C0BF RID: 49343 RVA: 0x0029926D File Offset: 0x0029746D
		[WitnessFunction("Replace", 0)]
		public DisjunctiveExamplesSpec WitnessReplaceSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.ReplaceSubject), null, null, null, "WitnessReplaceSubject");
		}

		// Token: 0x0600C0C0 RID: 49344 RVA: 0x0029928B File Offset: 0x0029748B
		[WitnessFunction("SliceBetween", 2, DependsOnParameters = new int[] { 0, 1 })]
		public DisjunctiveExamplesSpec WitnessSliceBetweenEnd(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec, ExampleSpec arg1Spec)
		{
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.SliceBetweenEndText), arg0Spec, arg1Spec, null, "WitnessSliceBetweenEnd");
		}

		// Token: 0x0600C0C1 RID: 49345 RVA: 0x002992AA File Offset: 0x002974AA
		[WitnessFunction("SliceBetween", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessSliceBetweenStart(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<string, string>(rule, spec, new Func<WitnessContext<string>, IEnumerable<string>>(this.SliceBetweenStartText), arg0Spec, null, null, "WitnessSliceBetweenStart");
		}

		// Token: 0x0600C0C2 RID: 49346 RVA: 0x002992C8 File Offset: 0x002974C8
		[WitnessFunction("Abs", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessAbsPos(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<int, int>(rule, spec, new Func<WitnessContext<int>, IEnumerable<int>>(this.AbsPos), arg0Spec, null, null, "WitnessAbsPos");
		}

		// Token: 0x0600C0C3 RID: 49347 RVA: 0x002992E6 File Offset: 0x002974E6
		[WitnessFunction("Find", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessFindDelimiter(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<int, string>(rule, spec, new Func<WitnessContext<int>, IEnumerable<string>>(this.FindDelimiter), arg0Spec, null, null, "WitnessFindDelimiter");
		}

		// Token: 0x0600C0C4 RID: 49348 RVA: 0x00299304 File Offset: 0x00297504
		[WitnessFunction("Find", 2, DependsOnParameters = new int[] { 0, 1 })]
		public DisjunctiveExamplesSpec WitnessFindInstance(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec, ExampleSpec arg1Spec)
		{
			return base.WitnessOutput<int, int>(rule, spec, new Func<WitnessContext<int>, IEnumerable<int>>(this.FindInstance), arg0Spec, arg1Spec, null, "WitnessFindInstance");
		}

		// Token: 0x0600C0C5 RID: 49349 RVA: 0x00299323 File Offset: 0x00297523
		[WitnessFunction("Find", 3, DependsOnParameters = new int[] { 0, 1, 2 })]
		public DisjunctiveExamplesSpec WitnessFindOffset(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec, ExampleSpec arg1Spec, ExampleSpec arg2Spec)
		{
			return base.WitnessOutput<int, int>(rule, spec, new Func<WitnessContext<int>, IEnumerable<int>>(this.FindOffset), arg0Spec, arg1Spec, arg2Spec, "WitnessFindOffset");
		}

		// Token: 0x0600C0C6 RID: 49350 RVA: 0x00299343 File Offset: 0x00297543
		[WitnessFunction("Match", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessMatchDescriptor(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<int, MatchDescriptor>(rule, spec, new Func<WitnessContext<int>, IEnumerable<MatchDescriptor>>(this.MatchDescriptor), arg0Spec, null, null, "WitnessMatchDescriptor");
		}

		// Token: 0x0600C0C7 RID: 49351 RVA: 0x00299361 File Offset: 0x00297561
		[WitnessFunction("Match", 2, DependsOnParameters = new int[] { 0, 1 })]
		public DisjunctiveExamplesSpec WitnessMatchInstance(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec, ExampleSpec arg1Spec)
		{
			return base.WitnessOutput<int, int>(rule, spec, new Func<WitnessContext<int>, IEnumerable<int>>(this.MatchInstance), arg0Spec, arg1Spec, null, "WitnessMatchInstance");
		}

		// Token: 0x0600C0C8 RID: 49352 RVA: 0x00299380 File Offset: 0x00297580
		[WitnessFunction("MatchEnd", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessMatchEndDescriptor(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<int, MatchDescriptor>(rule, spec, new Func<WitnessContext<int>, IEnumerable<MatchDescriptor>>(this.MatchEndDescriptor), arg0Spec, null, null, "WitnessMatchEndDescriptor");
		}

		// Token: 0x0600C0C9 RID: 49353 RVA: 0x0029939E File Offset: 0x0029759E
		[WitnessFunction("MatchEnd", 2, DependsOnParameters = new int[] { 0, 1 })]
		public DisjunctiveExamplesSpec WitnessMatchEndInstance(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec, ExampleSpec arg1Spec)
		{
			return base.WitnessOutput<int, int>(rule, spec, new Func<WitnessContext<int>, IEnumerable<int>>(this.MatchEndInstance), arg0Spec, arg1Spec, null, "WitnessMatchEndInstance");
		}

		// Token: 0x0600C0CA RID: 49354 RVA: 0x002993BD File Offset: 0x002975BD
		[WitnessFunction("MatchFull", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessMatchFullDescriptor(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<string, MatchDescriptor>(rule, spec, new Func<WitnessContext<string>, IEnumerable<MatchDescriptor>>(this.MatchFullDescriptor), arg0Spec, null, null, "WitnessMatchFullDescriptor");
		}

		// Token: 0x0600C0CB RID: 49355 RVA: 0x002993DB File Offset: 0x002975DB
		[WitnessFunction("MatchFull", 2, DependsOnParameters = new int[] { 0, 1 })]
		public DisjunctiveExamplesSpec WitnessMatchFullInstance(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec, ExampleSpec arg1Spec)
		{
			return base.WitnessOutput<string, int>(rule, spec, new Func<WitnessContext<string>, IEnumerable<int>>(this.MatchFullInstance), arg0Spec, arg1Spec, null, "WitnessMatchFullInstance");
		}

		// Token: 0x0600C0CC RID: 49356 RVA: 0x002993FA File Offset: 0x002975FA
		[WitnessFunction("FormatNumber", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessFormatNumberFormat(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<string, FormatNumberDescriptor>(rule, spec, new Func<WitnessContext<string>, IEnumerable<FormatNumberDescriptor>>(this.FormatNumberFormat), arg0Spec, null, null, "WitnessFormatNumberFormat");
		}

		// Token: 0x0600C0CD RID: 49357 RVA: 0x00299418 File Offset: 0x00297618
		[WitnessFunction("FormatNumber", 0)]
		public DisjunctiveExamplesSpec WitnessFormatNumberSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<string, decimal>(rule, spec, new Func<WitnessContext<string>, IEnumerable<decimal>>(this.FormatNumberSubject), null, null, null, "WitnessFormatNumberSubject");
		}

		// Token: 0x0600C0CE RID: 49358 RVA: 0x00299436 File Offset: 0x00297636
		[WitnessFunction("FromNumber", 1)]
		public DisjunctiveExamplesSpec WitnessFromNumberColumnName(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<WitnessContext<decimal>, IEnumerable<string>> func;
			if ((func = Witness.<>O.<11>__FromNumberColumnName) == null)
			{
				func = (Witness.<>O.<11>__FromNumberColumnName = new Func<WitnessContext<decimal>, IEnumerable<string>>(Witness.FromNumberColumnName));
			}
			return base.WitnessOutput<decimal, string>(rule, spec, func, null, null, null, "WitnessFromNumberColumnName");
		}

		// Token: 0x0600C0CF RID: 49359 RVA: 0x00299463 File Offset: 0x00297663
		[WitnessFunction("FromNumberCoalesced", 1)]
		public DisjunctiveExamplesSpec WitnessFromNumberCoalescedColumnName(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<decimal, string>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<string>>(this.FromNumberCoalescedColumnName), null, null, null, "WitnessFromNumberCoalescedColumnName");
		}

		// Token: 0x0600C0D0 RID: 49360 RVA: 0x00299484 File Offset: 0x00297684
		[WitnessFunction("FromNumbers", 1)]
		public DisjunctiveExamplesSpec WitnessFromNumbersColumnNames(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			ArithmeticStrategy arithmeticStrategy = base.Options.ArithmeticStrategy;
			DisjunctiveExamplesSpec disjunctiveExamplesSpec;
			if (arithmeticStrategy != ArithmeticStrategy.Latest)
			{
				if (arithmeticStrategy != ArithmeticStrategy.Legacy824)
				{
					throw new Exception(string.Format("Unknown {0}: {1} ", "ArithmeticStrategy", base.Options.ArithmeticStrategy));
				}
				Func<WitnessContext<decimal[]>, IEnumerable<string[]>> func;
				if ((func = Witness.<>O.<12>__FromNumbersColumnNames824) == null)
				{
					func = (Witness.<>O.<12>__FromNumbersColumnNames824 = new Func<WitnessContext<decimal[]>, IEnumerable<string[]>>(Witness.FromNumbersColumnNames824));
				}
				disjunctiveExamplesSpec = base.WitnessOutput<decimal[], string[]>(rule, spec, func, null, null, null, "WitnessFromNumbersColumnNames");
			}
			else
			{
				disjunctiveExamplesSpec = base.WitnessOutput<decimal[], string[]>(rule, spec, new Func<WitnessContext<decimal[]>, IEnumerable<string[]>>(this.FromNumbersColumnNames), null, null, null, "WitnessFromNumbersColumnNames");
			}
			return disjunctiveExamplesSpec;
		}

		// Token: 0x0600C0D1 RID: 49361 RVA: 0x0029951A File Offset: 0x0029771A
		[WitnessFunction("ParseNumber", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessParseNumberLocale(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<decimal, string>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<string>>(this.ParseNumberLocale), arg0Spec, null, null, "WitnessParseNumberLocale");
		}

		// Token: 0x0600C0D2 RID: 49362 RVA: 0x00299538 File Offset: 0x00297738
		[WitnessFunction("ParseNumber", 0)]
		public DisjunctiveExamplesSpec WitnessParseNumberSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<decimal, string>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<string>>(this.ParseNumberSubject), null, null, null, "WitnessParseNumberSubject");
		}

		// Token: 0x0600C0D3 RID: 49363 RVA: 0x00299556 File Offset: 0x00297756
		[WitnessFunction("RoundNumber", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessRoundNumberFormat(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<decimal, RoundNumberDescriptor>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<RoundNumberDescriptor>>(this.RoundNumberFormat), arg0Spec, null, null, "WitnessRoundNumberFormat");
		}

		// Token: 0x0600C0D4 RID: 49364 RVA: 0x00299574 File Offset: 0x00297774
		[WitnessFunction("RoundNumber", 0)]
		public DisjunctiveExamplesSpec WitnessRoundNumberSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.RoundNumberSubject), null, null, null, "WitnessRoundNumberSubject");
		}

		// Token: 0x0600C0D5 RID: 49365 RVA: 0x00299592 File Offset: 0x00297792
		[WitnessFunction("ToDecimal", 0)]
		public DisjunctiveExamplesSpec WitnessToDecimalSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<WitnessContext<decimal>, IEnumerable<decimal>> func;
			if ((func = Witness.<>O.<13>__ToDecimalSubject) == null)
			{
				func = (Witness.<>O.<13>__ToDecimalSubject = new Func<WitnessContext<decimal>, IEnumerable<decimal>>(Witness.ToDecimalSubject));
			}
			return base.WitnessOutput<decimal, decimal>(rule, spec, func, null, null, null, "WitnessToDecimalSubject");
		}

		// Token: 0x0600C0D6 RID: 49366 RVA: 0x002995BF File Offset: 0x002977BF
		[WitnessFunction("ToDouble", 0)]
		public DisjunctiveExamplesSpec WitnessToDoubleSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<WitnessContext<double>, IEnumerable<decimal>> func;
			if ((func = Witness.<>O.<14>__ToDoubleSubject) == null)
			{
				func = (Witness.<>O.<14>__ToDoubleSubject = new Func<WitnessContext<double>, IEnumerable<decimal>>(Witness.ToDoubleSubject));
			}
			return base.WitnessOutput<double, decimal>(rule, spec, func, null, null, null, "WitnessToDoubleSubject");
		}

		// Token: 0x0600C0D7 RID: 49367 RVA: 0x002995EC File Offset: 0x002977EC
		[WitnessFunction("ToInt", 0)]
		public DisjunctiveExamplesSpec WitnessToIntSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<WitnessContext<int>, IEnumerable<decimal>> func;
			if ((func = Witness.<>O.<15>__ToIntSubject) == null)
			{
				func = (Witness.<>O.<15>__ToIntSubject = new Func<WitnessContext<int>, IEnumerable<decimal>>(Witness.ToIntSubject));
			}
			return base.WitnessOutput<int, decimal>(rule, spec, func, null, null, null, "WitnessToIntSubject");
		}

		// Token: 0x0600C0D8 RID: 49368 RVA: 0x00299619 File Offset: 0x00297819
		[WitnessFunction("ToStr", 0)]
		public DisjunctiveExamplesSpec WitnessToStrSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<WitnessContext<string>, IEnumerable<string>> func;
			if ((func = Witness.<>O.<16>__ToStrSubject) == null)
			{
				func = (Witness.<>O.<16>__ToStrSubject = new Func<WitnessContext<string>, IEnumerable<string>>(Witness.ToStrSubject));
			}
			return base.WitnessOutput<string, string>(rule, spec, func, null, null, null, "WitnessToStrSubject");
		}

		// Token: 0x0600C0D9 RID: 49369 RVA: 0x00299646 File Offset: 0x00297846
		[WitnessFunction("RowNumberLinearTransform", 1)]
		public DisjunctiveExamplesSpec WitnessRowNumberLinearTransformDescriptor(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<decimal, RowNumberLinearTransformDescriptor>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<RowNumberLinearTransformDescriptor>>(this.RowNumberLinearTransformDescriptor), null, null, null, "WitnessRowNumberLinearTransformDescriptor");
		}

		// Token: 0x0600C0DA RID: 49370 RVA: 0x00299664 File Offset: 0x00297864
		[WitnessFunction("Add", 0)]
		public DisjunctiveExamplesSpec WitnessAddLeft(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			ArithmeticStrategy arithmeticStrategy = base.Options.ArithmeticStrategy;
			DisjunctiveExamplesSpec disjunctiveExamplesSpec;
			if (arithmeticStrategy != ArithmeticStrategy.Latest)
			{
				if (arithmeticStrategy != ArithmeticStrategy.Legacy824)
				{
					throw new Exception(string.Format("Unknown {0}: {1} ", "ArithmeticStrategy", base.Options.ArithmeticStrategy));
				}
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.AddLeft824), null, null, null, "WitnessAddLeft");
			}
			else
			{
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.AddLeft), null, null, null, "WitnessAddLeft");
			}
			return disjunctiveExamplesSpec;
		}

		// Token: 0x0600C0DB RID: 49371 RVA: 0x002996EC File Offset: 0x002978EC
		[WitnessFunction("Add", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessAddRight(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			ArithmeticStrategy arithmeticStrategy = base.Options.ArithmeticStrategy;
			DisjunctiveExamplesSpec disjunctiveExamplesSpec;
			if (arithmeticStrategy != ArithmeticStrategy.Latest)
			{
				if (arithmeticStrategy != ArithmeticStrategy.Legacy824)
				{
					throw new Exception(string.Format("Unknown {0}: {1} ", "ArithmeticStrategy", base.Options.ArithmeticStrategy));
				}
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.AddRight824), arg0Spec, null, null, "WitnessAddRight");
			}
			else
			{
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.AddRight), arg0Spec, null, null, "WitnessAddRight");
			}
			return disjunctiveExamplesSpec;
		}

		// Token: 0x0600C0DC RID: 49372 RVA: 0x00299774 File Offset: 0x00297974
		[WitnessFunction("Average", 0)]
		public DisjunctiveExamplesSpec WitnessAverageSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			ArithmeticStrategy arithmeticStrategy = base.Options.ArithmeticStrategy;
			DisjunctiveExamplesSpec disjunctiveExamplesSpec;
			if (arithmeticStrategy != ArithmeticStrategy.Latest)
			{
				if (arithmeticStrategy != ArithmeticStrategy.Legacy824)
				{
					throw new Exception(string.Format("Unknown {0}: {1} ", "ArithmeticStrategy", base.Options.ArithmeticStrategy));
				}
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal[]>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal[]>>(this.AverageSubject824), null, null, null, "WitnessAverageSubject");
			}
			else
			{
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal[]>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal[]>>(this.AverageSubject), null, null, null, "WitnessAverageSubject");
			}
			return disjunctiveExamplesSpec;
		}

		// Token: 0x0600C0DD RID: 49373 RVA: 0x002997FC File Offset: 0x002979FC
		[WitnessFunction("Divide", 0)]
		public DisjunctiveExamplesSpec WitnessDivideLeft(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			ArithmeticStrategy arithmeticStrategy = base.Options.ArithmeticStrategy;
			DisjunctiveExamplesSpec disjunctiveExamplesSpec;
			if (arithmeticStrategy != ArithmeticStrategy.Latest)
			{
				if (arithmeticStrategy != ArithmeticStrategy.Legacy824)
				{
					throw new Exception(string.Format("Unknown {0}: {1} ", "ArithmeticStrategy", base.Options.ArithmeticStrategy));
				}
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.DivideLeft824), null, null, null, "WitnessDivideLeft");
			}
			else
			{
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.DivideLeft), null, null, null, "WitnessDivideLeft");
			}
			return disjunctiveExamplesSpec;
		}

		// Token: 0x0600C0DE RID: 49374 RVA: 0x00299884 File Offset: 0x00297A84
		[WitnessFunction("Divide", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessDivideRight(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			ArithmeticStrategy arithmeticStrategy = base.Options.ArithmeticStrategy;
			DisjunctiveExamplesSpec disjunctiveExamplesSpec;
			if (arithmeticStrategy != ArithmeticStrategy.Latest)
			{
				if (arithmeticStrategy != ArithmeticStrategy.Legacy824)
				{
					throw new Exception(string.Format("Unknown {0}: {1} ", "ArithmeticStrategy", base.Options.ArithmeticStrategy));
				}
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.DivideRight824), arg0Spec, null, null, "WitnessDivideRight");
			}
			else
			{
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.DivideRight), arg0Spec, null, null, "WitnessDivideRight");
			}
			return disjunctiveExamplesSpec;
		}

		// Token: 0x0600C0DF RID: 49375 RVA: 0x0029990C File Offset: 0x00297B0C
		[WitnessFunction("Multiply", 0)]
		public DisjunctiveExamplesSpec WitnessMultiplyLeft(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			ArithmeticStrategy arithmeticStrategy = base.Options.ArithmeticStrategy;
			DisjunctiveExamplesSpec disjunctiveExamplesSpec;
			if (arithmeticStrategy != ArithmeticStrategy.Latest)
			{
				if (arithmeticStrategy != ArithmeticStrategy.Legacy824)
				{
					throw new Exception(string.Format("Unknown {0}: {1} ", "ArithmeticStrategy", base.Options.ArithmeticStrategy));
				}
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.MultiplyLeft824), null, null, null, "WitnessMultiplyLeft");
			}
			else
			{
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.MultiplyLeft), null, null, null, "WitnessMultiplyLeft");
			}
			return disjunctiveExamplesSpec;
		}

		// Token: 0x0600C0E0 RID: 49376 RVA: 0x00299994 File Offset: 0x00297B94
		[WitnessFunction("Multiply", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessMultiplyRight(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			ArithmeticStrategy arithmeticStrategy = base.Options.ArithmeticStrategy;
			DisjunctiveExamplesSpec disjunctiveExamplesSpec;
			if (arithmeticStrategy != ArithmeticStrategy.Latest)
			{
				if (arithmeticStrategy != ArithmeticStrategy.Legacy824)
				{
					throw new Exception(string.Format("Unknown {0}: {1} ", "ArithmeticStrategy", base.Options.ArithmeticStrategy));
				}
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.MultiplyRight824), arg0Spec, null, null, "WitnessMultiplyRight");
			}
			else
			{
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.MultiplyRight), arg0Spec, null, null, "WitnessMultiplyRight");
			}
			return disjunctiveExamplesSpec;
		}

		// Token: 0x0600C0E1 RID: 49377 RVA: 0x00299A1C File Offset: 0x00297C1C
		[WitnessFunction("Product", 0)]
		public DisjunctiveExamplesSpec WitnessProductSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			ArithmeticStrategy arithmeticStrategy = base.Options.ArithmeticStrategy;
			DisjunctiveExamplesSpec disjunctiveExamplesSpec;
			if (arithmeticStrategy != ArithmeticStrategy.Latest)
			{
				if (arithmeticStrategy != ArithmeticStrategy.Legacy824)
				{
					throw new Exception(string.Format("Unknown {0}: {1} ", "ArithmeticStrategy", base.Options.ArithmeticStrategy));
				}
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal[]>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal[]>>(this.ProductSubject824), null, null, null, "WitnessProductSubject");
			}
			else
			{
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal[]>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal[]>>(this.ProductSubject), null, null, null, "WitnessProductSubject");
			}
			return disjunctiveExamplesSpec;
		}

		// Token: 0x0600C0E2 RID: 49378 RVA: 0x00299AA4 File Offset: 0x00297CA4
		[WitnessFunction("Subtract", 0)]
		public DisjunctiveExamplesSpec WitnessSubtractLeft(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			ArithmeticStrategy arithmeticStrategy = base.Options.ArithmeticStrategy;
			DisjunctiveExamplesSpec disjunctiveExamplesSpec;
			if (arithmeticStrategy != ArithmeticStrategy.Latest)
			{
				if (arithmeticStrategy != ArithmeticStrategy.Legacy824)
				{
					throw new Exception(string.Format("Unknown {0}: {1} ", "ArithmeticStrategy", base.Options.ArithmeticStrategy));
				}
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.SubtractLeft824), null, null, null, "WitnessSubtractLeft");
			}
			else
			{
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.SubtractLeft), null, null, null, "WitnessSubtractLeft");
			}
			return disjunctiveExamplesSpec;
		}

		// Token: 0x0600C0E3 RID: 49379 RVA: 0x00299B2C File Offset: 0x00297D2C
		[WitnessFunction("Subtract", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessSubtractRight(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			ArithmeticStrategy arithmeticStrategy = base.Options.ArithmeticStrategy;
			DisjunctiveExamplesSpec disjunctiveExamplesSpec;
			if (arithmeticStrategy != ArithmeticStrategy.Latest)
			{
				if (arithmeticStrategy != ArithmeticStrategy.Legacy824)
				{
					throw new Exception(string.Format("Unknown {0}: {1} ", "ArithmeticStrategy", base.Options.ArithmeticStrategy));
				}
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.SubtractRight824), arg0Spec, null, null, "WitnessSubtractRight");
			}
			else
			{
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal>>(this.SubtractRight), arg0Spec, null, null, "WitnessSubtractRight");
			}
			return disjunctiveExamplesSpec;
		}

		// Token: 0x0600C0E4 RID: 49380 RVA: 0x00299BB4 File Offset: 0x00297DB4
		[WitnessFunction("Sum", 0)]
		public DisjunctiveExamplesSpec WitnessSumSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			ArithmeticStrategy arithmeticStrategy = base.Options.ArithmeticStrategy;
			DisjunctiveExamplesSpec disjunctiveExamplesSpec;
			if (arithmeticStrategy != ArithmeticStrategy.Latest)
			{
				if (arithmeticStrategy != ArithmeticStrategy.Legacy824)
				{
					throw new Exception(string.Format("Unknown {0}: {1} ", "ArithmeticStrategy", base.Options.ArithmeticStrategy));
				}
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal[]>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal[]>>(this.SumSubject824), null, null, null, "WitnessSumSubject");
			}
			else
			{
				disjunctiveExamplesSpec = base.WitnessOutput<decimal, decimal[]>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<decimal[]>>(this.SumSubject), null, null, null, "WitnessSumSubject");
			}
			return disjunctiveExamplesSpec;
		}

		// Token: 0x0600C0E5 RID: 49381 RVA: 0x00299C3B File Offset: 0x00297E3B
		[WitnessFunction("DateTimePart", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessDateTimePartKind(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<decimal, DateTimePartKind>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<DateTimePartKind>>(this.DateTimePartKind), arg0Spec, null, null, "WitnessDateTimePartKind");
		}

		// Token: 0x0600C0E6 RID: 49382 RVA: 0x00299C59 File Offset: 0x00297E59
		[WitnessFunction("DateTimePart", 0)]
		public DisjunctiveExamplesSpec WitnessDateTimePartSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<decimal, DateTime>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<DateTime>>(this.DateTimePartSubject), null, null, null, "WitnessDateTimePartSubject");
		}

		// Token: 0x0600C0E7 RID: 49383 RVA: 0x00299C77 File Offset: 0x00297E77
		[WitnessFunction("FormatDateTime", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessFormatDateTimeFormat(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<string, DateTimeDescriptor>(rule, spec, new Func<WitnessContext<string>, IEnumerable<DateTimeDescriptor>>(this.FormatDateTimeFormat), arg0Spec, null, null, "WitnessFormatDateTimeFormat");
		}

		// Token: 0x0600C0E8 RID: 49384 RVA: 0x00299C95 File Offset: 0x00297E95
		[WitnessFunction("FormatDateTime", 0)]
		public DisjunctiveExamplesSpec WitnessFormatDateTimeSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<string, DateTime>(rule, spec, new Func<WitnessContext<string>, IEnumerable<DateTime>>(this.FormatDateTimeSubject), null, null, null, "WitnessFormatDateTimeSubject");
		}

		// Token: 0x0600C0E9 RID: 49385 RVA: 0x00299CB3 File Offset: 0x00297EB3
		[WitnessFunction("FromDateTime", 1)]
		public DisjunctiveExamplesSpec WitnessFromDateTimeColumnName(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<WitnessContext<DateTime>, IEnumerable<string>> func;
			if ((func = Witness.<>O.<17>__FromDateTimeColumnName) == null)
			{
				func = (Witness.<>O.<17>__FromDateTimeColumnName = new Func<WitnessContext<DateTime>, IEnumerable<string>>(Witness.FromDateTimeColumnName));
			}
			return base.WitnessOutput<DateTime, string>(rule, spec, func, null, null, null, "WitnessFromDateTimeColumnName");
		}

		// Token: 0x0600C0EA RID: 49386 RVA: 0x00299CE0 File Offset: 0x00297EE0
		[WitnessFunction("FromDateTimePart", 1)]
		public DisjunctiveExamplesSpec WitnessFromDateTimePartColumnName(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<DateTime, string>(rule, spec, new Func<WitnessContext<DateTime>, IEnumerable<string>>(this.FromDateTimePartColumnName), null, null, null, "WitnessFromDateTimePartColumnName");
		}

		// Token: 0x0600C0EB RID: 49387 RVA: 0x00299CFE File Offset: 0x00297EFE
		[WitnessFunction("FromDateTimePart", 2, DependsOnParameters = new int[] { 1 })]
		public DisjunctiveExamplesSpec WitnessFromDateTimePartKind(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<DateTime, DateTimePartKind>(rule, spec, new Func<WitnessContext<DateTime>, IEnumerable<DateTimePartKind>>(this.FromDateTimePartKind), arg0Spec, null, null, "WitnessFromDateTimePartKind");
		}

		// Token: 0x0600C0EC RID: 49388 RVA: 0x00299D1C File Offset: 0x00297F1C
		[WitnessFunction("FromTime", 1)]
		public DisjunctiveExamplesSpec WitnessFromTimeColumnName(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<WitnessContext<Time>, IEnumerable<string>> func;
			if ((func = Witness.<>O.<18>__FromTimeColumnName) == null)
			{
				func = (Witness.<>O.<18>__FromTimeColumnName = new Func<WitnessContext<Time>, IEnumerable<string>>(Witness.FromTimeColumnName));
			}
			return base.WitnessOutput<Time, string>(rule, spec, func, null, null, null, "WitnessFromTimeColumnName");
		}

		// Token: 0x0600C0ED RID: 49389 RVA: 0x00299D49 File Offset: 0x00297F49
		[WitnessFunction("ParseDateTime", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessParseDateTimeFormat(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<DateTime, DateTimeDescriptor>(rule, spec, new Func<WitnessContext<DateTime>, IEnumerable<DateTimeDescriptor>>(this.ParseDateTimeFormat), arg0Spec, null, null, "WitnessParseDateTimeFormat");
		}

		// Token: 0x0600C0EE RID: 49390 RVA: 0x00299D67 File Offset: 0x00297F67
		[WitnessFunction("ParseDateTime", 0)]
		public DisjunctiveExamplesSpec WitnessParseDateTimeSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<DateTime, string>(rule, spec, new Func<WitnessContext<DateTime>, IEnumerable<string>>(this.ParseDateTimeSubject), null, null, null, "WitnessParseDateTimeSubject");
		}

		// Token: 0x0600C0EF RID: 49391 RVA: 0x00299D85 File Offset: 0x00297F85
		[WitnessFunction("RoundDateTime", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessRoundDateTimeFormat(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<DateTime, RoundDateTimeDescriptor>(rule, spec, new Func<WitnessContext<DateTime>, IEnumerable<RoundDateTimeDescriptor>>(this.RoundDateTimeFormat), arg0Spec, null, null, "WitnessRoundDateTimeFormat");
		}

		// Token: 0x0600C0F0 RID: 49392 RVA: 0x00299DA3 File Offset: 0x00297FA3
		[WitnessFunction("RoundDateTime", 0)]
		public DisjunctiveExamplesSpec WitnessRoundDateTimeSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<DateTime, DateTime>(rule, spec, new Func<WitnessContext<DateTime>, IEnumerable<DateTime>>(this.RoundDateTimeSubject), null, null, null, "WitnessRoundDateTimeSubject");
		}

		// Token: 0x0600C0F1 RID: 49393 RVA: 0x00299DC1 File Offset: 0x00297FC1
		[WitnessFunction("TimePart", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessTimePartKind(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arg0Spec)
		{
			return base.WitnessOutput<decimal, TimePartKind>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<TimePartKind>>(this.TimePartKind), arg0Spec, null, null, "WitnessTimePartKind");
		}

		// Token: 0x0600C0F2 RID: 49394 RVA: 0x00299DDF File Offset: 0x00297FDF
		[WitnessFunction("TimePart", 0)]
		public DisjunctiveExamplesSpec WitnessTimePartSubject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return base.WitnessOutput<decimal, Time>(rule, spec, new Func<WitnessContext<decimal>, IEnumerable<Time>>(this.TimePartSubject), null, null, null, "WitnessTimePartSubject");
		}

		// Token: 0x0600C0F3 RID: 49395 RVA: 0x00299DFD File Offset: 0x00297FFD
		[WitnessFunction("ToDateTime", 0)]
		public DisjunctiveExamplesSpec WitnessToDateTime(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<WitnessContext<DateTime>, IEnumerable<DateTime>> func;
			if ((func = Witness.<>O.<19>__ToDateTimeSubject) == null)
			{
				func = (Witness.<>O.<19>__ToDateTimeSubject = new Func<WitnessContext<DateTime>, IEnumerable<DateTime>>(Witness.ToDateTimeSubject));
			}
			return base.WitnessOutput<DateTime, DateTime>(rule, spec, func, null, null, null, "WitnessToDateTime");
		}

		// Token: 0x0600C0F4 RID: 49396 RVA: 0x00299E2C File Offset: 0x0029802C
		public static IDictionary<int, IEnumerable<TOperatorOutput>> ResolveRowNumberDisjunctiveExamples<TOperatorOutput>(IReadOnlyList<WitnessDisjunctiveContext<TOperatorOutput>> contexts)
		{
			Dictionary<int, IEnumerable<TOperatorOutput>> dictionary = new Dictionary<int, IEnumerable<TOperatorOutput>>();
			foreach (WitnessDisjunctiveContext<TOperatorOutput> witnessDisjunctiveContext in contexts)
			{
				INumberedRow numberedRow = witnessDisjunctiveContext.InputRow as INumberedRow;
				if (numberedRow == null || dictionary.ContainsKey(numberedRow.RowNumber))
				{
					return new Dictionary<int, IEnumerable<TOperatorOutput>>();
				}
				dictionary.Add(numberedRow.RowNumber, witnessDisjunctiveContext.OperatorOutputs);
			}
			return dictionary;
		}

		// Token: 0x0600C0F5 RID: 49397 RVA: 0x00299EB0 File Offset: 0x002980B0
		[DefaultTactic]
		internal Optional<ProgramSet> DefaultTactic(IAlternatingLanguage language, Func<ILanguage, ProgramSet> learner)
		{
			if (!base.Options.EnableLearningShortCircuit)
			{
				return StdTactic.Instance.LearnAlternative(language, learner);
			}
			return ShortCircuitTactic.Instance.LearnAlternative(language, learner);
		}

		// Token: 0x0600C0F6 RID: 49398 RVA: 0x00299ED8 File Offset: 0x002980D8
		[Tactic("output")]
		[Tactic("outStr")]
		[Tactic("formatted")]
		[Tactic("segmentCase")]
		[Tactic("concatEntry")]
		[Tactic("concatCase")]
		[Tactic("concatPrefix")]
		[Tactic("concatSuffix")]
		[Tactic("arithmetic")]
		[Tactic("inumber")]
		[Tactic("arithmeticLeft")]
		[Tactic("number1")]
		[Tactic("date")]
		[Tactic("fromStrTrim")]
		[Tactic("splitTrim")]
		[Tactic("sliceTrim")]
		internal Optional<ProgramSet> StandardTactic(IAlternatingLanguage language, Func<ILanguage, ProgramSet> learner)
		{
			return StdTactic.Instance.LearnAlternative(language, learner);
		}

		// Token: 0x0600C0F7 RID: 49399 RVA: 0x00299EE8 File Offset: 0x002980E8
		private IEnumerable<DateTimeDescriptor> FormatDateTimeFormat(WitnessContext<string> context)
		{
			string operatorOutput = context.OperatorOutput;
			object dependentArg = context.DependentArg1;
			if (dependentArg is DateTime)
			{
				DateTime dateTime = (DateTime)dependentArg;
				IReadOnlyList<DateTimeDescriptor> readOnlyList;
				if (this._recognition.TryFormatDateTime(dateTime, operatorOutput, out readOnlyList, false))
				{
					return readOnlyList;
				}
			}
			return null;
		}

		// Token: 0x0600C0F8 RID: 49400 RVA: 0x00299F28 File Offset: 0x00298128
		private IEnumerable<DateTime> FormatDateTimeSubject(WitnessContext<string> context)
		{
			string result = context.OperatorOutput;
			if (result == null || !this._recognition.IsFormattedDateTime(result, false))
			{
				return null;
			}
			return from date in this._recognition.ReverseFormattedDateTime(context.InputRow, result, true)
				where this._recognition.TryFormatDateTime(date, result, false)
				select date;
		}

		// Token: 0x0600C0F9 RID: 49401 RVA: 0x00299F98 File Offset: 0x00298198
		private IEnumerable<DateTimeDescriptor> ParseDateTimeFormat(WitnessContext<DateTime> context)
		{
			DateTime result = context.OperatorOutput;
			object dependentArg = context.DependentArg1;
			string subject = dependentArg as string;
			if (subject == null || !base.Options.DateTimeSources.HasFlag(DateTimeSourceKind.Parsed))
			{
				return null;
			}
			return from <>h__TransparentIdentifier1 in (from input in this._recognition.InputStrings(context.InputRow, null).DistinctValues
					from findResult in this._recognition.FindParseableDateTimes(input, this.Options.EnableParseDateTimePartial)
					where findResult.Substring == subject
					select <>h__TransparentIdentifier0).SelectMany(delegate(<>h__TransparentIdentifier0)
				{
					IReadOnlyList<DateTimeDescriptor> readOnlyList;
					if (!this._recognition.TryFormatDateTime(result, <>h__TransparentIdentifier0.findResult.Substring, out readOnlyList, true))
					{
						return Utils.Empty<DateTimeDescriptor>();
					}
					return readOnlyList;
				}, (<>h__TransparentIdentifier0, DateTimeDescriptor descriptor) => new { <>h__TransparentIdentifier0, descriptor }).Where(delegate(<>h__TransparentIdentifier1)
				{
					DateTime? dateTime = Operators.ParseDateTime(subject, <>h__TransparentIdentifier1.descriptor);
					DateTime result2 = result;
					return dateTime != null && (dateTime == null || dateTime.GetValueOrDefault() == result2);
				})
				select <>h__TransparentIdentifier1.descriptor;
		}

		// Token: 0x0600C0FA RID: 49402 RVA: 0x0029A0B8 File Offset: 0x002982B8
		private IEnumerable<string> ParseDateTimeSubject(WitnessContext<DateTime> context)
		{
			DateTime result = context.OperatorOutput;
			if (!base.Options.DateTimeSources.HasFlag(DateTimeSourceKind.Parsed))
			{
				return null;
			}
			return from <>h__TransparentIdentifier0 in (from input in this._recognition.InputStrings(context.InputRow, null).DistinctValues
					from findResult in this._recognition.FindParseableDateTimes(input, this.Options.EnableParseDateTimePartial)
					select new { input, findResult }).Where(delegate(<>h__TransparentIdentifier0)
				{
					FindDateTimeCacheItem findResult = <>h__TransparentIdentifier0.findResult;
					return findResult != null && findResult.Values.Any((DateTime val) => val == result);
				})
				select <>h__TransparentIdentifier0.findResult.Substring;
		}

		// Token: 0x0600C0FB RID: 49403 RVA: 0x0029A17C File Offset: 0x0029837C
		private IEnumerable<RoundDateTimeDescriptor> RoundDateTimeFormat(WitnessContext<DateTime> context)
		{
			if (!base.Options.EnableRoundDateTime)
			{
				return null;
			}
			DateTime result = context.OperatorOutput;
			object dependentArg = context.DependentArg1;
			if (dependentArg is DateTime)
			{
				DateTime subject = (DateTime)dependentArg;
				IEnumerable<RoundDateTimeCacheItem> enumerable;
				if (this._recognition.TryRoundDateTime(subject, result, out enumerable))
				{
					return from item in enumerable
						where item.Result == result && item.Subject == subject
						select item.Descriptor;
				}
			}
			return null;
		}

		// Token: 0x0600C0FC RID: 49404 RVA: 0x0029A218 File Offset: 0x00298418
		private IEnumerable<DateTime> RoundDateTimeSubject(WitnessContext<DateTime> context)
		{
			if (!base.Options.EnableRoundDateTime)
			{
				return null;
			}
			DateTime result = context.OperatorOutput;
			return this._recognition.DateTimes(context.InputRow).DistinctValues.Where((DateTime date) => this._recognition.TryRoundDateTime(date, result));
		}

		// Token: 0x0600C0FD RID: 49405 RVA: 0x0029A274 File Offset: 0x00298474
		private IEnumerable<DateTimePartKind> DateTimePartKind(WitnessContext<decimal> context)
		{
			if (!base.Options.EnableDateTimePart)
			{
				return null;
			}
			decimal result = context.OperatorOutput;
			object dependentArg = context.DependentArg1;
			if (dependentArg is DateTime)
			{
				DateTime subject = (DateTime)dependentArg;
				IEnumerable<DateTimePartCacheItem> enumerable;
				if (this._recognition.TryDateTimePart(subject, result, out enumerable))
				{
					return from item in enumerable
						where item.Result == result && item.Subject == subject
						select item.Kind;
				}
			}
			return null;
		}

		// Token: 0x0600C0FE RID: 49406 RVA: 0x0029A310 File Offset: 0x00298510
		private IEnumerable<DateTime> DateTimePartSubject(WitnessContext<decimal> context)
		{
			if (!base.Options.EnableDateTimePart)
			{
				return null;
			}
			decimal result = context.OperatorOutput;
			if (this._recognition.HasOutputClassification(context.InputRow, StringClassification.FormattedOther))
			{
				return null;
			}
			return this._recognition.DateTimes(context.InputRow).DistinctValues.Where(delegate(DateTime input)
			{
				IEnumerable<DateTimePartCacheItem> enumerable;
				return this._recognition.TryDateTimePart(input, result, out enumerable);
			});
		}

		// Token: 0x0600C0FF RID: 49407 RVA: 0x0029A388 File Offset: 0x00298588
		private IEnumerable<TimePartKind> TimePartKind(WitnessContext<decimal> context)
		{
			if (!base.Options.EnableTimePart)
			{
				return null;
			}
			decimal result = context.OperatorOutput;
			object dependentArg = context.DependentArg1;
			if (dependentArg is Time)
			{
				Time subject = (Time)dependentArg;
				IEnumerable<TimePartCacheItem> enumerable;
				if (this._recognition.TryTimePart(subject, result, out enumerable))
				{
					return from item in enumerable
						where item.Result == result && item.Subject == subject
						select item.Kind;
				}
			}
			return null;
		}

		// Token: 0x0600C100 RID: 49408 RVA: 0x0029A424 File Offset: 0x00298624
		private IEnumerable<Time> TimePartSubject(WitnessContext<decimal> context)
		{
			if (!base.Options.EnableTimePart)
			{
				return null;
			}
			decimal result = context.OperatorOutput;
			return this._recognition.Times(context.InputRow).Where(delegate(Time input)
			{
				IEnumerable<TimePartCacheItem> enumerable;
				return this._recognition.TryTimePart(input, result, out enumerable);
			});
		}

		// Token: 0x0600C101 RID: 49409 RVA: 0x0029A47C File Offset: 0x0029867C
		private static IEnumerable<string> FromDateTimeColumnName(WitnessContext<DateTime> context)
		{
			DateTime result = context.OperatorOutput;
			return context.InputRow.ColumnNames.Where(delegate(string columnName)
			{
				DateTime? dateTime = Operators.FromDateTime(context.InputRow, columnName);
				DateTime result2 = result;
				return dateTime != null && (dateTime == null || dateTime.GetValueOrDefault() == result2);
			});
		}

		// Token: 0x0600C102 RID: 49410 RVA: 0x0029A4C8 File Offset: 0x002986C8
		private static IEnumerable<string> FromTimeColumnName(WitnessContext<Time> context)
		{
			Time result = context.OperatorOutput;
			return context.InputRow.ColumnNames.Where(delegate(string columnName)
			{
				Time? time = Operators.FromTime(context.InputRow, columnName);
				Time result2 = result;
				return time != null && (time == null || time.GetValueOrDefault() == result2);
			});
		}

		// Token: 0x0600C103 RID: 49411 RVA: 0x0029A514 File Offset: 0x00298714
		private static IEnumerable<DateTime> ToDateTimeSubject(WitnessContext<DateTime> context)
		{
			DateTime operatorOutput = context.OperatorOutput;
			if (!(Operators.ToDateTime(operatorOutput) == operatorOutput))
			{
				return null;
			}
			return operatorOutput.Yield<DateTime>();
		}

		// Token: 0x0600C104 RID: 49412 RVA: 0x0029A540 File Offset: 0x00298740
		private IEnumerable<string> FromDateTimePartColumnName(WitnessContext<DateTime> context)
		{
			if (!base.Options.EnableFromDateTimePart || base.Examples.Count < 2)
			{
				return null;
			}
			DateTime result = context.OperatorOutput;
			return from item in this._recognition.FindPartDateTimes(context.InputRow)
				where item.Value == result
				select item.ColumnName;
		}

		// Token: 0x0600C105 RID: 49413 RVA: 0x0029A5C4 File Offset: 0x002987C4
		private IEnumerable<DateTimePartKind> FromDateTimePartKind(WitnessContext<DateTime> context)
		{
			if (base.Options.EnableFromDateTimePart)
			{
				object dependentArg = context.DependentArg1;
				string columnName = dependentArg as string;
				if (columnName != null && base.Examples.Count >= 2)
				{
					DateTime result = context.OperatorOutput;
					return from item in this._recognition.FindPartDateTimes(context.InputRow)
						where item.ColumnName == columnName && item.Value == result
						select item.Kind;
				}
			}
			return null;
		}

		// Token: 0x0600C106 RID: 49414 RVA: 0x0029A664 File Offset: 0x00298864
		private IEnumerable<RowNumberLinearTransformDescriptor> RowNumberLinearTransformDescriptor(WitnessContext<decimal> context)
		{
			decimal operatorOutput = context.OperatorOutput;
			if (base.Options.EnableForwardFill && base.Examples.Count >= base.Options.ForwardFillMinExampleCount && base.Examples.Count <= base.Options.ForwardFillMaxExampleCount)
			{
				INumberedRow numberedRow = context.InputRow as INumberedRow;
				IReadOnlyList<RowNumberLinearTransformDescriptor> readOnlyList;
				if (numberedRow != null && this._recognition.TryRowNumberLinearTransform(numberedRow.RowNumber, operatorOutput, Witness.ResolveRowNumberDisjunctiveExamples<decimal>(context.DisjunctiveContexts), out readOnlyList))
				{
					return readOnlyList;
				}
			}
			return null;
		}

		// Token: 0x0600C107 RID: 49415 RVA: 0x0029A6E8 File Offset: 0x002988E8
		private IEnumerable<MatchDescriptor> MatchFullDescriptor(WitnessContext<string> context)
		{
			if (base.Options.EnableMatch)
			{
				string text = context.DependentArg1 as string;
				if (text != null)
				{
					string operatorOutput = context.OperatorOutput;
					if (operatorOutput.Length == 1 && operatorOutput.AnyDelimiters())
					{
						return null;
					}
					IReadOnlyList<MatchInstanceCacheItem> readOnlyList;
					if (!this._recognition.TryMatchFull(text, operatorOutput, out readOnlyList))
					{
						return null;
					}
					return readOnlyList.Select((MatchInstanceCacheItem m) => m.Descriptor);
				}
			}
			return null;
		}

		// Token: 0x0600C108 RID: 49416 RVA: 0x0029A768 File Offset: 0x00298968
		private IEnumerable<int> MatchFullInstance(WitnessContext<string> context)
		{
			if (base.Options.EnableMatch)
			{
				string text = context.DependentArg1 as string;
				if (text != null)
				{
					object dependentArg = context.DependentArg2;
					MatchDescriptor matchDesc = dependentArg as MatchDescriptor;
					if (matchDesc != null)
					{
						string operatorOutput = context.OperatorOutput;
						IReadOnlyList<MatchInstanceCacheItem> readOnlyList;
						if (!this._recognition.TryMatchFull(text, operatorOutput, out readOnlyList))
						{
							return null;
						}
						return readOnlyList.Where((MatchInstanceCacheItem m) => m.Descriptor == matchDesc).SelectMany(delegate(MatchInstanceCacheItem m)
						{
							if (!this.Options.EnableNegativePosition)
							{
								return m.Instance.Yield<int>();
							}
							return new int[] { m.Instance, m.InstanceFromEnd };
						}, (MatchInstanceCacheItem m, int i) => i);
					}
				}
			}
			return null;
		}

		// Token: 0x0600C109 RID: 49417 RVA: 0x0029A81C File Offset: 0x00298A1C
		private IEnumerable<MatchDescriptor> MatchDescriptor(WitnessContext<int> context)
		{
			if (base.Options.EnableMatch)
			{
				string text = context.DependentArg1 as string;
				if (text != null)
				{
					int num = context.OperatorOutput.ToValidIndex();
					IReadOnlyList<MatchInstanceCacheItem> readOnlyList;
					if (!this._recognition.TryMatch(text, num, out readOnlyList))
					{
						return null;
					}
					return readOnlyList.Select((MatchInstanceCacheItem m) => m.Descriptor);
				}
			}
			return null;
		}

		// Token: 0x0600C10A RID: 49418 RVA: 0x0029A88C File Offset: 0x00298A8C
		private IEnumerable<int> MatchInstance(WitnessContext<int> context)
		{
			if (base.Options.EnableMatch)
			{
				string text = context.DependentArg1 as string;
				if (text != null)
				{
					object dependentArg = context.DependentArg2;
					MatchDescriptor matchDesc = dependentArg as MatchDescriptor;
					if (matchDesc != null)
					{
						int num = context.OperatorOutput.ToValidIndex();
						IReadOnlyList<MatchInstanceCacheItem> readOnlyList;
						if (!this._recognition.TryMatch(text, num, out readOnlyList))
						{
							return null;
						}
						return readOnlyList.Where((MatchInstanceCacheItem match) => match.Descriptor == matchDesc).SelectMany(delegate(MatchInstanceCacheItem match)
						{
							if (!this.Options.EnableNegativePosition)
							{
								return match.Instance.Yield<int>();
							}
							return new int[] { match.Instance, match.InstanceFromEnd };
						}, (MatchInstanceCacheItem match, int instance) => instance);
					}
				}
			}
			return null;
		}

		// Token: 0x0600C10B RID: 49419 RVA: 0x0029A944 File Offset: 0x00298B44
		private IEnumerable<MatchDescriptor> MatchEndDescriptor(WitnessContext<int> context)
		{
			if (base.Options.EnableMatch)
			{
				string text = context.DependentArg1 as string;
				if (text != null)
				{
					int num = context.OperatorOutput.ToValidIndex();
					IReadOnlyList<MatchInstanceCacheItem> readOnlyList;
					if (!this._recognition.TryMatchEnd(text, num, out readOnlyList))
					{
						return null;
					}
					return readOnlyList.Select((MatchInstanceCacheItem m) => m.Descriptor);
				}
			}
			return null;
		}

		// Token: 0x0600C10C RID: 49420 RVA: 0x0029A9B4 File Offset: 0x00298BB4
		private IEnumerable<int> MatchEndInstance(WitnessContext<int> context)
		{
			if (base.Options.EnableMatch)
			{
				string text = context.DependentArg1 as string;
				if (text != null)
				{
					object dependentArg = context.DependentArg2;
					MatchDescriptor matchDesc = dependentArg as MatchDescriptor;
					if (matchDesc != null)
					{
						int num = context.OperatorOutput.ToValidIndex();
						IReadOnlyList<MatchInstanceCacheItem> readOnlyList;
						if (!this._recognition.TryMatchEnd(text, num, out readOnlyList))
						{
							return null;
						}
						return readOnlyList.Where((MatchInstanceCacheItem match) => match.Descriptor == matchDesc).SelectMany(delegate(MatchInstanceCacheItem match)
						{
							if (!this.Options.EnableNegativePosition)
							{
								return match.Instance.Yield<int>();
							}
							return new int[] { match.Instance, match.InstanceFromEnd };
						}, (MatchInstanceCacheItem match, int instance) => instance);
					}
				}
			}
			return null;
		}

		// Token: 0x0600C10D RID: 49421 RVA: 0x0029AA6C File Offset: 0x00298C6C
		private IEnumerable<FormatNumberDescriptor> FormatNumberFormat(WitnessContext<string> context)
		{
			string operatorOutput = context.OperatorOutput;
			object dependentArg = context.DependentArg1;
			if (dependentArg is decimal)
			{
				decimal num = (decimal)dependentArg;
				IEnumerable<FormatNumberDescriptor> enumerable;
				if (this._recognition.TryFormatNumber(num, operatorOutput, out enumerable))
				{
					return enumerable;
				}
			}
			return null;
		}

		// Token: 0x0600C10E RID: 49422 RVA: 0x0029AAAC File Offset: 0x00298CAC
		private IEnumerable<decimal> FormatNumberSubject(WitnessContext<string> context)
		{
			string result = context.OperatorOutput;
			if (!this._recognition.IsFormattedNumber(result))
			{
				return null;
			}
			IEnumerable<decimal> enumerable;
			if (base.Options.EnableForwardFill)
			{
				INumberedRow numberedRow = context.InputRow as INumberedRow;
				if (numberedRow != null)
				{
					enumerable = this._recognition.FindRowNumberTransforms(numberedRow.RowNumber, result, Witness.ResolveRowNumberDisjunctiveExamples<string>(context.DisjunctiveContexts));
					goto IL_0073;
				}
			}
			enumerable = Utils.Empty<decimal>();
			IL_0073:
			IEnumerable<decimal> enumerable2 = enumerable;
			return from number in this._recognition.ReverseFormattedNumber(context.InputRow, result).Union(enumerable2)
				where this._recognition.TryFormatNumber(number, result)
				select number;
		}

		// Token: 0x0600C10F RID: 49423 RVA: 0x0029AB5C File Offset: 0x00298D5C
		private IEnumerable<string> ParseNumberLocale(WitnessContext<decimal> context)
		{
			decimal result = context.OperatorOutput;
			object dependentArg = context.DependentArg1;
			string subject = dependentArg as string;
			if (subject == null || !base.Options.NumberSources.HasFlag(NumberSourceKind.Parsed))
			{
				return null;
			}
			return from <>h__TransparentIdentifier0 in (from input in this._recognition.InputStrings(context.InputRow, null).DistinctValues
					from findResult in this._recognition.FindNumbers(input, false, new int?(10))
					select new { input, findResult }).Where(delegate(<>h__TransparentIdentifier0)
				{
					if (<>h__TransparentIdentifier0.findResult.Substring == subject && <>h__TransparentIdentifier0.findResult.Value == result)
					{
						decimal? num = Operators.ParseNumber(subject, <>h__TransparentIdentifier0.findResult.Locale);
						decimal result2 = result;
						return (num.GetValueOrDefault() == result2) & (num != null);
					}
					return false;
				})
				select <>h__TransparentIdentifier0.findResult.Locale;
		}

		// Token: 0x0600C110 RID: 49424 RVA: 0x0029AC38 File Offset: 0x00298E38
		private IEnumerable<string> ParseNumberSubject(WitnessContext<decimal> context)
		{
			decimal result = context.OperatorOutput;
			if (!base.Options.NumberSources.HasFlag(NumberSourceKind.Parsed))
			{
				return null;
			}
			return from input in this._recognition.InputStrings(context.InputRow, null).DistinctValues
				from findResult in this._recognition.FindNumbers(input, false, new int?(10))
				where findResult.Value == result
				select findResult.Substring;
		}

		// Token: 0x0600C111 RID: 49425 RVA: 0x0029ACFC File Offset: 0x00298EFC
		private IEnumerable<RoundNumberDescriptor> RoundNumberFormat(WitnessContext<decimal> context)
		{
			if (!base.Options.EnableRoundNumber || base.Examples.Count < base.Options.NumberRoundMinExampleCount)
			{
				return null;
			}
			decimal result = context.OperatorOutput;
			object dependentArg = context.DependentArg1;
			if (dependentArg is decimal)
			{
				decimal subject = (decimal)dependentArg;
				IEnumerable<RoundNumberCacheItem> enumerable;
				if (this._recognition.TryRoundNumber(subject, result, out enumerable))
				{
					return from item in enumerable
						where item.Result == result && item.Subject == subject
						select item.Descriptor;
				}
			}
			return null;
		}

		// Token: 0x0600C112 RID: 49426 RVA: 0x0029ADB0 File Offset: 0x00298FB0
		private IEnumerable<decimal> RoundNumberSubject(WitnessContext<decimal> context)
		{
			if (!base.Options.EnableRoundNumber || base.Examples.Count < base.Options.NumberRoundMinExampleCount)
			{
				return null;
			}
			decimal result = context.OperatorOutput;
			return this._recognition.Numbers(context.InputRow, false).DistinctValues.Where((decimal number) => this._recognition.TryRoundNumber(number, result));
		}

		// Token: 0x0600C113 RID: 49427 RVA: 0x0029AE28 File Offset: 0x00299028
		private IEnumerable<string> FromNumberCoalescedColumnName(WitnessContext<decimal> context)
		{
			decimal result = context.OperatorOutput;
			if (!base.Options.EnableFromNumberCoalesced)
			{
				return null;
			}
			return context.InputRow.ColumnNames.Where(delegate(string columnName)
			{
				decimal? num = Operators.FromNumberCoalesced(context.InputRow, columnName);
				decimal result2 = result;
				return (num.GetValueOrDefault() == result2) & (num != null);
			});
		}

		// Token: 0x0600C114 RID: 49428 RVA: 0x0029AE84 File Offset: 0x00299084
		private static IEnumerable<string> FromNumberColumnName(WitnessContext<decimal> context)
		{
			decimal result = context.OperatorOutput;
			return context.InputRow.ColumnNames.Where(delegate(string columnName)
			{
				decimal? num = Operators.FromNumber(context.InputRow, columnName);
				decimal result2 = result;
				return (num.GetValueOrDefault() == result2) & (num != null);
			});
		}

		// Token: 0x0600C115 RID: 49429 RVA: 0x002986FB File Offset: 0x002968FB
		private static IEnumerable<decimal> ToDecimalSubject(WitnessContext<decimal> context)
		{
			return context.OperatorOutput.Yield<decimal>();
		}

		// Token: 0x0600C116 RID: 49430 RVA: 0x0029AED0 File Offset: 0x002990D0
		private static IEnumerable<decimal> ToDoubleSubject(WitnessContext<double> context)
		{
			IEnumerable<decimal> enumerable;
			try
			{
				double operatorOutput = context.OperatorOutput;
				if (operatorOutput == 0.0)
				{
					enumerable = 0m.Yield<decimal>();
				}
				else
				{
					decimal num = Convert.ToDecimal(operatorOutput);
					enumerable = ((Math.Abs(Operators.ToDouble(num)) > 1E-08) ? num.Yield<decimal>() : null);
				}
			}
			catch (OverflowException)
			{
				enumerable = null;
			}
			return enumerable;
		}

		// Token: 0x0600C117 RID: 49431 RVA: 0x0029AF3C File Offset: 0x0029913C
		private static IEnumerable<decimal> ToIntSubject(WitnessContext<int> context)
		{
			int operatorOutput = context.OperatorOutput;
			if (operatorOutput == 0)
			{
				return 0m.Yield<decimal>();
			}
			decimal num = Convert.ToDecimal(operatorOutput);
			if (Operators.ToInt(num) == 0)
			{
				return null;
			}
			return num.Yield<decimal>();
		}

		// Token: 0x0600C118 RID: 49432 RVA: 0x0029AF78 File Offset: 0x00299178
		private IEnumerable<string> ReplaceFindText(WitnessContext<string> context)
		{
			string operatorOutput = context.OperatorOutput;
			if (base.Options.EnableReplace && !string.IsNullOrEmpty(operatorOutput))
			{
				object dependentArg = context.DependentArg1;
				string subject = dependentArg as string;
				if (subject != null)
				{
					IEnumerable<string> enumerable = from findText in this._recognition.UniqueSubstrings(subject, operatorOutput)
						let multiple = subject.AllIndexesOf(findText, StringComparison.Ordinal).HasAtLeast(2)
						let allDelimiters = findText.AllDelimiters()
						where !string.IsNullOrEmpty(findText) && findText != subject && (this.Examples.Count > 1 || allDelimiters || multiple)
						select findText;
					IEnumerable<string> enumerable2 = from c in subject
						where c.IsDelimiter()
						select c.ToString();
					return enumerable.Concat(enumerable2).Distinct<string>();
				}
			}
			return null;
		}

		// Token: 0x0600C119 RID: 49433 RVA: 0x0029B0A4 File Offset: 0x002992A4
		private IEnumerable<string> ReplaceReplaceText(WitnessContext<string> context)
		{
			string result = context.OperatorOutput;
			if (base.Options.EnableReplace && !string.IsNullOrEmpty(result))
			{
				object obj = context.DependentArg1;
				string subject = obj as string;
				if (subject != null)
				{
					obj = context.DependentArg2;
					string findText = obj as string;
					if (findText != null)
					{
						IEnumerable<string> enumerable = this._recognition.UniqueSubstrings(result, subject);
						IEnumerable<string> enumerable2 = from c in result
							where c.IsDelimiter()
							select c.ToString();
						return from replaceText in enumerable.Concat(enumerable2).Concat(string.Empty.Yield<string>()).Distinct<string>()
							where replaceText != null && findText != replaceText && replaceText != result && Operators.Replace(subject, findText, replaceText) == result
							select replaceText;
					}
				}
			}
			return null;
		}

		// Token: 0x0600C11A RID: 49434 RVA: 0x0029B1A8 File Offset: 0x002993A8
		private IEnumerable<string> ReplaceSubject(WitnessContext<string> context)
		{
			if (!base.Options.EnableReplace)
			{
				return null;
			}
			return this._recognition.InputStrings(context.InputRow, null).DistinctValues.Where((string input) => !string.IsNullOrEmpty(input));
		}

		// Token: 0x0600C11B RID: 49435 RVA: 0x0029B200 File Offset: 0x00299400
		private static IEnumerable<int> SlicePos1(WitnessContext<string> context)
		{
			string operatorOutput = context.OperatorOutput;
			string text;
			bool flag;
			if (!string.IsNullOrEmpty(operatorOutput))
			{
				text = context.DependentArg1 as string;
				flag = text == null;
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag;
			if (!flag2)
			{
				char c = operatorOutput[0];
				bool flag3 = c == '.' || c == '@';
				flag2 = flag3;
			}
			if (flag2 || (operatorOutput.Length == 1 && operatorOutput.AnyDelimiters() && !operatorOutput.IsCurrencySymbol()) || !text.Contains(operatorOutput))
			{
				return null;
			}
			return from index in text.AllIndexesOf(operatorOutput, StringComparison.Ordinal)
				select index.ToValidPosition();
		}

		// Token: 0x0600C11C RID: 49436 RVA: 0x0029B2A8 File Offset: 0x002994A8
		private static IEnumerable<int> SlicePos2(WitnessContext<string> context)
		{
			string operatorOutput = context.OperatorOutput;
			if (!string.IsNullOrEmpty(operatorOutput))
			{
				string text = context.DependentArg1 as string;
				if (text != null)
				{
					object dependentArg = context.DependentArg2;
					if (dependentArg is int)
					{
						int num = (int)dependentArg;
						if (operatorOutput.Length != 1 || !operatorOutput.AnyDelimiters() || operatorOutput.IsCurrencySymbol())
						{
							int num2 = num + operatorOutput.Length;
							if (!(Operators.Slice(text, num, num2) == operatorOutput))
							{
								return null;
							}
							return num2.Yield<int>();
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600C11D RID: 49437 RVA: 0x0029B328 File Offset: 0x00299528
		private IEnumerable<string> FindDelimiter(WitnessContext<int> context)
		{
			string text = context.DependentArg1 as string;
			if (text == null)
			{
				return null;
			}
			int num = context.OperatorOutput.ToValidIndex();
			IReadOnlyList<FindDescriptor> readOnlyList;
			if (!this._recognition.TryFind(text, num, out readOnlyList))
			{
				return null;
			}
			return readOnlyList.Select((FindDescriptor desc) => desc.Delimiter);
		}

		// Token: 0x0600C11E RID: 49438 RVA: 0x0029B38C File Offset: 0x0029958C
		private IEnumerable<int> FindInstance(WitnessContext<int> context)
		{
			object obj = context.DependentArg1;
			string subject = obj as string;
			if (subject != null)
			{
				obj = context.DependentArg2;
				string delimiter = obj as string;
				if (delimiter != null)
				{
					int num = context.OperatorOutput.ToValidIndex();
					IReadOnlyList<FindDescriptor> readOnlyList;
					if (!this._recognition.TryFind(subject, num, out readOnlyList))
					{
						return null;
					}
					return from desc in readOnlyList
						where desc.Delimiter == delimiter
						from instance in this._recognition.ExpandInstance(subject, desc.Delimiter, desc.DelimiterIndex)
						select instance;
				}
			}
			return null;
		}

		// Token: 0x0600C11F RID: 49439 RVA: 0x0029B444 File Offset: 0x00299644
		private IEnumerable<int> FindOffset(WitnessContext<int> context)
		{
			string text = context.DependentArg1 as string;
			if (text != null)
			{
				string text2 = context.DependentArg2 as string;
				if (text2 != null)
				{
					object dependentArg = context.DependentArg3;
					if (dependentArg is int)
					{
						int num = (int)dependentArg;
						int operatorOutput = context.OperatorOutput;
						int num2 = operatorOutput.ToValidIndex();
						if ((num2 == 1 && num2 == text.Length - 1) || !this._recognition.TryFind(text, num2))
						{
							return null;
						}
						int? num3 = Operators.Find(text, text2, num, 0);
						int num4 = operatorOutput;
						int? num5 = num3;
						int? num6;
						int? num7;
						if (num5 == null)
						{
							num6 = null;
							num7 = num6;
						}
						else
						{
							num7 = new int?(num4 - num5.GetValueOrDefault());
						}
						num6 = num7;
						if (num6 == null)
						{
							return null;
						}
						return num6.GetValueOrDefault().Yield<int>();
					}
				}
			}
			return null;
		}

		// Token: 0x0600C120 RID: 49440 RVA: 0x0029B50C File Offset: 0x0029970C
		private IEnumerable<int> SlicePrefixAbsPos(WitnessContext<string> context)
		{
			if (base.Options.EnableSlice)
			{
				string text = context.DependentArg1 as string;
				if (text != null)
				{
					string operatorOutput = context.OperatorOutput;
					if (!text.StartsWith(operatorOutput))
					{
						return null;
					}
					if (this._recognition.OutputStrings().Count((string output) => this._recognition.HasClassification(output, StringClassification.PhoneNumber)) == base.Examples.Count)
					{
						return this.SlicePrefixPos(context);
					}
					if (text.Take(operatorOutput.Length + 1).AnyDelimiters())
					{
						return null;
					}
					return this.SlicePrefixPos(context);
				}
			}
			return null;
		}

		// Token: 0x0600C121 RID: 49441 RVA: 0x0029B59C File Offset: 0x0029979C
		private IEnumerable<int> SlicePrefixPos(WitnessContext<string> context)
		{
			if (base.Options.EnableSlice)
			{
				string text = context.DependentArg1 as string;
				if (text != null)
				{
					string operatorOutput = context.OperatorOutput;
					if (!(text != operatorOutput) || !text.StartsWith(operatorOutput))
					{
						return null;
					}
					return (operatorOutput.Length + 1).Yield<int>();
				}
			}
			return null;
		}

		// Token: 0x0600C122 RID: 49442 RVA: 0x0029B5F0 File Offset: 0x002997F0
		private IEnumerable<int> SliceSuffixPos(WitnessContext<string> context)
		{
			if (base.Options.EnableSlice)
			{
				string text = context.DependentArg1 as string;
				if (text != null)
				{
					string operatorOutput = context.OperatorOutput;
					if (!(text != operatorOutput) || !text.EndsWith(operatorOutput))
					{
						return null;
					}
					return (text.Length - operatorOutput.Length + 1).Yield<int>();
				}
			}
			return null;
		}

		// Token: 0x0600C123 RID: 49443 RVA: 0x0029B64C File Offset: 0x0029984C
		private IEnumerable<string> SliceBetweenEndText(WitnessContext<string> context)
		{
			string operatorOutput = context.OperatorOutput;
			if (base.Options.EnableSliceBetween && !string.IsNullOrEmpty(operatorOutput))
			{
				string text = context.DependentArg1 as string;
				if (text != null)
				{
					string text2 = context.DependentArg2 as string;
					if (text2 != null)
					{
						int num = text.IndexOf(operatorOutput, StringComparison.Ordinal) + operatorOutput.Length;
						string text3 = this._recognition.ResolveSliceBetweenEndText(text, num);
						if (string.IsNullOrEmpty(text3) || !(Operators.SliceBetween(text, text2, text3) == operatorOutput))
						{
							return null;
						}
						return text3.Yield<string>();
					}
				}
			}
			return null;
		}

		// Token: 0x0600C124 RID: 49444 RVA: 0x0029B6D8 File Offset: 0x002998D8
		private IEnumerable<string> SliceBetweenStartText(WitnessContext<string> context)
		{
			string operatorOutput = context.OperatorOutput;
			if (base.Options.EnableSliceBetween && !string.IsNullOrEmpty(operatorOutput))
			{
				string text = context.DependentArg1 as string;
				if (text != null && text.Contains(operatorOutput))
				{
					int num = text.IndexOf(operatorOutput, StringComparison.Ordinal);
					string text2 = this._recognition.ResolveSliceBetweenStartText(text, num);
					if (text2 == null)
					{
						return null;
					}
					return text2.Yield<string>();
				}
			}
			return null;
		}

		// Token: 0x0600C125 RID: 49445 RVA: 0x0029B73C File Offset: 0x0029993C
		private IEnumerable<int> AbsPos(WitnessContext<int> context)
		{
			string text = context.DependentArg1 as string;
			if (text == null)
			{
				return null;
			}
			int operatorOutput = context.OperatorOutput;
			return from pos in this._recognition.ExpandPosition(text, operatorOutput)
				select (pos);
		}

		// Token: 0x0600C126 RID: 49446 RVA: 0x0029B794 File Offset: 0x00299994
		private IEnumerable<string> SplitSubject(WitnessContext<string> context)
		{
			string result = context.OperatorOutput;
			IReadOnlyList<SubstringDescriptor> readOnlyList;
			if (!base.Options.EnableSplit || string.IsNullOrEmpty(result) || !this._recognition.TrySubstring(context.InputRow, result, out readOnlyList, false))
			{
				return null;
			}
			return from descriptor in readOnlyList
				where descriptor.Output == result && descriptor.AllowSplit
				select descriptor.Input;
		}

		// Token: 0x0600C127 RID: 49447 RVA: 0x0029B824 File Offset: 0x00299A24
		private IEnumerable<string> SplitDelimiter(WitnessContext<string> context)
		{
			string result = context.OperatorOutput;
			if (base.Options.EnableSplit && !string.IsNullOrEmpty(result))
			{
				string text = context.DependentArg1 as string;
				IReadOnlyList<SubstringDescriptor> readOnlyList;
				if (text != null && this._recognition.TrySubstring(text, result, out readOnlyList, false))
				{
					return from descriptor in readOnlyList
						where descriptor.Output == result && descriptor.AllowSplit
						select descriptor.SplitDescriptor.Delimiter.ToString();
				}
			}
			return null;
		}

		// Token: 0x0600C128 RID: 49448 RVA: 0x0029B8BC File Offset: 0x00299ABC
		private IEnumerable<int> SplitInstance(WitnessContext<string> context)
		{
			string result = context.OperatorOutput;
			if (base.Options.EnableSplit && !string.IsNullOrEmpty(result))
			{
				string text = context.DependentArg1 as string;
				if (text != null)
				{
					object dependentArg = context.DependentArg2;
					string delimiter = dependentArg as string;
					IReadOnlyList<SubstringDescriptor> readOnlyList;
					if (delimiter != null && this._recognition.TrySubstring(text, result, out readOnlyList, false))
					{
						return from descriptor in readOnlyList
							where descriptor.Output == result && descriptor.AllowSplit
							let split = descriptor.SplitDescriptor
							where split.Delimiter.ToString() == delimiter
							from instances in new int[] { split.Instance, split.NegativeInstance }
							select instances;
					}
				}
			}
			return null;
		}

		// Token: 0x0600C12A RID: 49450 RVA: 0x0029B9CC File Offset: 0x00299BCC
		[CompilerGenerated]
		private result <LearnIf>g__ResolveIfNode|44_3(IReadOnlyList<IConditionalBranch> path, int index = 0)
		{
			IConditionalBranch conditionalBranch = path[index];
			if (index != path.Count - 1)
			{
				condition condition = this.ResolveConditionNode(conditionalBranch.Predicate);
				result result = base.Builder.Node.Cast.result(conditionalBranch.Program.ProgramNode);
				result result2 = this.<LearnIf>g__ResolveIfNode|44_3(path, index + 1);
				return base.Builder.Node.Rule.If(condition, result, result2);
			}
			if (!(conditionalBranch is INullConditionalBranch))
			{
				return base.Builder.Node.Cast.result(path[index].Program.ProgramNode);
			}
			return new result_inull(base.Builder, new Null(base.Builder));
		}

		// Token: 0x04004A61 RID: 19041
		private readonly ArrayEquality<decimal> _decimalArrayEquality = new ArrayEquality<decimal>();

		// Token: 0x04004A62 RID: 19042
		private readonly Func<IEnumerable<Example<IRow, object>>, ConditionalBranchMeta> _learnBranch;

		// Token: 0x04004A63 RID: 19043
		private readonly Recognition _recognition;

		// Token: 0x04004A64 RID: 19044
		private readonly IConcatStrategy _concatStrategy;

		// Token: 0x02001682 RID: 5762
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04004A65 RID: 19045
			public static Func<string, string> <0>__Trim;

			// Token: 0x04004A66 RID: 19046
			public static Func<string, string> <1>__TrimFull;

			// Token: 0x04004A67 RID: 19047
			public static Func<WitnessContext<string>, IEnumerable<string>> <2>__FromStrColumnName;

			// Token: 0x04004A68 RID: 19048
			public static Func<WitnessContext<decimal>, IEnumerable<decimal>> <3>__AddRightNumber;

			// Token: 0x04004A69 RID: 19049
			public static Func<WitnessContext<decimal>, IEnumerable<decimal>> <4>__SubtractRightNumber;

			// Token: 0x04004A6A RID: 19050
			public static Func<WitnessContext<decimal>, IEnumerable<decimal>> <5>__MultiplyRightNumber;

			// Token: 0x04004A6B RID: 19051
			public static Func<WitnessContext<decimal>, IEnumerable<decimal>> <6>__DivideRightNumber;

			// Token: 0x04004A6C RID: 19052
			public static Func<WitnessContext<DateTime>, IEnumerable<DateTime>> <7>__Date;

			// Token: 0x04004A6D RID: 19053
			public static Func<WitnessContext<decimal>, IEnumerable<decimal>> <8>__Number;

			// Token: 0x04004A6E RID: 19054
			public static Func<WitnessContext<string>, IEnumerable<int>> <9>__SlicePos1;

			// Token: 0x04004A6F RID: 19055
			public static Func<WitnessContext<string>, IEnumerable<int>> <10>__SlicePos2;

			// Token: 0x04004A70 RID: 19056
			public static Func<WitnessContext<decimal>, IEnumerable<string>> <11>__FromNumberColumnName;

			// Token: 0x04004A71 RID: 19057
			public static Func<WitnessContext<decimal[]>, IEnumerable<string[]>> <12>__FromNumbersColumnNames824;

			// Token: 0x04004A72 RID: 19058
			public static Func<WitnessContext<decimal>, IEnumerable<decimal>> <13>__ToDecimalSubject;

			// Token: 0x04004A73 RID: 19059
			public static Func<WitnessContext<double>, IEnumerable<decimal>> <14>__ToDoubleSubject;

			// Token: 0x04004A74 RID: 19060
			public static Func<WitnessContext<int>, IEnumerable<decimal>> <15>__ToIntSubject;

			// Token: 0x04004A75 RID: 19061
			public static Func<WitnessContext<string>, IEnumerable<string>> <16>__ToStrSubject;

			// Token: 0x04004A76 RID: 19062
			public static Func<WitnessContext<DateTime>, IEnumerable<string>> <17>__FromDateTimeColumnName;

			// Token: 0x04004A77 RID: 19063
			public static Func<WitnessContext<Time>, IEnumerable<string>> <18>__FromTimeColumnName;

			// Token: 0x04004A78 RID: 19064
			public static Func<WitnessContext<DateTime>, IEnumerable<DateTime>> <19>__ToDateTimeSubject;
		}
	}
}
