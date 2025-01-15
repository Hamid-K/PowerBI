using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Semantics;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.CustomExtraction;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.ExtractByEntity;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Python
{
	// Token: 0x02001DB7 RID: 7607
	public class PythonTranslator : PythonTranslator<PythonModule, Program, IRow, object>
	{
		// Token: 0x0600FF0E RID: 65294 RVA: 0x0036855F File Offset: 0x0036675F
		public PythonTranslator(IEnumerable<string> boundNames = null)
			: base(boundNames)
		{
		}

		// Token: 0x17002A7B RID: 10875
		// (get) Token: 0x0600FF0F RID: 65295 RVA: 0x00368568 File Offset: 0x00366768
		protected override IReadOnlyDictionary<string, string> OperatorMapping
		{
			get
			{
				return PythonTranslator.OperatorMappingDictionary;
			}
		}

		// Token: 0x0600FF10 RID: 65296 RVA: 0x0036856F File Offset: 0x0036676F
		protected override ISubprogramTranslator<PythonHeaderModule, PythonModule, Program, IRow, object> GetSubprogramTranslatorFor(OptimizeFor optimization)
		{
			if (optimization != OptimizeFor.Readability)
			{
				return null;
			}
			return new ReadablePythonTranslator();
		}

		// Token: 0x0600FF11 RID: 65297 RVA: 0x0036857C File Offset: 0x0036677C
		public override PythonModule CreateModule(string moduleName, string headerModuleReferenceName, string headerModuleReferenceAlias = "ttext")
		{
			return new PythonModule(moduleName, headerModuleReferenceName, headerModuleReferenceAlias);
		}

		// Token: 0x0600FF12 RID: 65298 RVA: 0x00368586 File Offset: 0x00366786
		protected override Record<string, Type>[] GetParameters(ProgramNode node)
		{
			return base.CurrentTranslationModule.Parameters ?? base.GetParameters(node);
		}

		// Token: 0x0600FF13 RID: 65299 RVA: 0x0036859E File Offset: 0x0036679E
		public override GeneratedFunction Translate(Program root, PythonModule translationModule, OptimizeFor optimization)
		{
			return this.Translate(root, translationModule, null, optimization);
		}

		// Token: 0x0600FF14 RID: 65300 RVA: 0x003685AC File Offset: 0x003667AC
		public GeneratedFunction Translate(Program root, PythonModule translationModule, IEnumerable<string> allColumns, OptimizeFor optimization)
		{
			base.CurrentTranslationModule = translationModule;
			base.CurrentTranslationModule.BindColumnNameMappings(allColumns ?? root.ColumnsUsed);
			base.CurrentTranslationModule.AddInternalToNameWhenBinding = optimization != OptimizeFor.Readability;
			Record<string, Type>[] parameters = this.GetParameters(root.ProgramNode);
			Type resolvedType = root.ProgramNode.Grammar.StartSymbol.ResolvedType;
			SSAGeneratedFunction ssageneratedFunction = null;
			IReadOnlyList<Record<GeneratedFunction, GeneratedFunction>> readOnlyList = null;
			if (root.Branches.Count == 1)
			{
				Branch branch2 = root.Branches.FirstOrDefault<Branch>();
				if (branch2 == null || branch2.Predicate == null)
				{
					ssageneratedFunction = (SSAGeneratedFunction)base.Translate(root, translationModule, optimization);
					goto IL_00EA;
				}
			}
			readOnlyList = root.Branches.Select((Branch branch) => Record.Create<GeneratedFunction, GeneratedFunction>((branch.Predicate == null) ? null : this.TranslateProgramNode(branch.Predicate.Value.Node, translationModule, false, optimization), this.TranslateProgramNode(branch.Body.Node, translationModule, false, optimization))).ToList<Record<GeneratedFunction, GeneratedFunction>>();
			IL_00EA:
			List<SSAStep> beforeBranchesSequence = null;
			if (readOnlyList != null)
			{
				foreach (Record<GeneratedFunction, GeneratedFunction> record in readOnlyList)
				{
					if (record.Item1 != null)
					{
						base.ApplyStandardOptimizations(record.Item1, OptimizeFor.Performance);
					}
				}
				beforeBranchesSequence = new List<SSAStep>();
				IReadOnlyDictionary<SSARValue, SSARValue> readOnlyDictionary = null;
				if (optimization == OptimizeFor.Performance)
				{
					Dictionary<SSAFunctionApplication, SSAVariable> dictionary = new Dictionary<SSAFunctionApplication, SSAVariable>();
					foreach (ref Record<GeneratedFunction, GeneratedFunction> ptr in readOnlyList)
					{
						PythonGeneratedFunction pythonGeneratedFunction = ptr.Item1 as PythonGeneratedFunction;
						if (pythonGeneratedFunction != null)
						{
							SSAFunctionApplication ssafunctionApplication = pythonGeneratedFunction.SSASequence.Last<SSAStep>().RValue as SSAFunctionApplication;
							if (ssafunctionApplication != null)
							{
								SSAValue ssavalue = ssafunctionApplication.FunctionArguments[0];
								SSAFunctionApplication predFuncAppArg = ssavalue as SSAFunctionApplication;
								if (predFuncAppArg != null && predFuncAppArg.FunctionName == "str_to_substring")
								{
									SSAVariable orAdd = dictionary.GetOrAdd(predFuncAppArg, delegate(SSAFunctionApplication _)
									{
										SSAStep ssastep = new SSAStep(new SSARegister(predFuncAppArg.ValueType), predFuncAppArg, "");
										beforeBranchesSequence.Add(ssastep);
										return new SSAVariable(predFuncAppArg.ValueType, ssastep.LValue.Name);
									});
									ssafunctionApplication.SubstituteArgument(predFuncAppArg, orAdd);
								}
							}
						}
					}
					readOnlyDictionary = dictionary.ToDictionary((KeyValuePair<SSAFunctionApplication, SSAVariable> kv) => new SSAFunctionApplication(typeof(ValueSubstring), kv.Key.FunctionName, kv.Key.FunctionArguments.ToArray<SSAValue>()), (KeyValuePair<SSAFunctionApplication, SSAVariable> kv) => kv.Value);
				}
				foreach (Record<GeneratedFunction, GeneratedFunction> record2 in readOnlyList)
				{
					if (readOnlyDictionary != null && readOnlyDictionary.Any<KeyValuePair<SSARValue, SSARValue>>())
					{
						record2.Item2.Optimize(new SubstituteValues(readOnlyDictionary));
					}
					base.ApplyStandardOptimizations(record2.Item2, optimization);
				}
			}
			return new TransformationTextPythonGeneratedFunction(parameters, resolvedType, root.ColumnsUsed, (ssageneratedFunction != null) ? ssageneratedFunction.SSASequence : null, readOnlyList, beforeBranchesSequence);
		}

		// Token: 0x0600FF15 RID: 65301 RVA: 0x003688DC File Offset: 0x00366ADC
		public override PythonHeaderModule GenerateHeaderModule(Program p, string headerModuleName)
		{
			Assembly assembly = typeof(PythonTranslator).GetTypeInfo().Assembly;
			string name = assembly.GetName().Name;
			string text = FormattableString.Invariant(FormattableStringFactory.Create("{0}.Translation.Python.transformation_text.py", new object[] { name }));
			return new PythonHeaderModule(headerModuleName, new string[] { AssemblyResourceUtils.LoadResourceFromAssembly(assembly, text) });
		}

		// Token: 0x0600FF16 RID: 65302 RVA: 0x0036893C File Offset: 0x00366B3C
		private string _translateDateTimeFormat(DateTimeFormat dateTimeFormat)
		{
			IReadOnlyList<DateTimeFormatPart> formatParts = dateTimeFormat.FormatParts;
			string text = string.Join(", ", formatParts.Select(delegate(DateTimeFormatPart part)
			{
				ConstantDateTimeFormatPart constantDateTimeFormatPart = part as ConstantDateTimeFormatPart;
				if (constantDateTimeFormatPart != null)
				{
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}.ConstantDateTimeFormatPart.create({1})", new object[]
					{
						base.HeaderModuleNameForCurrentTranslation,
						constantDateTimeFormatPart.ConstantString.ToPythonLiteral()
					}));
				}
				NumericDateTimeFormatPart numericDateTimeFormatPart = part as NumericDateTimeFormatPart;
				string text2 = PythonNameUtils.ConvertStringToSnakeCase(part.MatchedPart.Value.ToString()).ToPythonLiteral();
				if (numericDateTimeFormatPart != null)
				{
					string text3 = "None";
					if (numericDateTimeFormatPart.MatchedPart.Value == DateTimePart.Year && numericDateTimeFormatPart.MaximumLength <= 2)
					{
						int twoDigitYearMax = CultureInfo.InvariantCulture.Calendar.TwoDigitYearMax;
						int num = twoDigitYearMax % 100;
						int num2 = twoDigitYearMax - num;
						int num3 = num2 - 100;
						text3 = FormattableString.Invariant(FormattableStringFactory.Create("lambda x: x + {0} if x <= {1} else x + {2}", new object[] { num2, num, num3 }));
					}
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}.NumericDateTimeFormatPart.create({1}, {2}, {3}, {4}, {5}, {6}, {7})", new object[]
					{
						base.HeaderModuleNameForCurrentTranslation,
						numericDateTimeFormatPart.MinimumLength,
						numericDateTimeFormatPart.MaximumLength,
						text2,
						numericDateTimeFormatPart.MinimumValue,
						numericDateTimeFormatPart.MaximumValue,
						text3,
						this.GenerateLiteralRepresentation(part.AllowsLeadingZeros(), typeof(bool)).Value
					}));
				}
				StringDateTimeFormatPart stringDateTimeFormatPart = part as StringDateTimeFormatPart;
				if (stringDateTimeFormatPart != null)
				{
					string text4 = string.Join(", ", stringDateTimeFormatPart.StringLookup.Select((KeyValuePair<int, string> kv) => FormattableString.Invariant(FormattableStringFactory.Create("{0} : {1}", new object[]
					{
						kv.Key,
						kv.Value.ToPythonLiteral()
					}))));
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}.StringDateTimeFormatPart.create({{{1}}}, {2})", new object[] { base.HeaderModuleNameForCurrentTranslation, text4, text2 }));
				}
				TimeZoneOffsetFormatPart timeZoneOffsetFormatPart = part as TimeZoneOffsetFormatPart;
				if (timeZoneOffsetFormatPart != null)
				{
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}.TimeZoneOffsetFormatPart.create(separator={1}, matched_part={2}, zero_is_z={3}, allow_numeric_zero={4})", new object[]
					{
						base.HeaderModuleNameForCurrentTranslation,
						timeZoneOffsetFormatPart.Separator.ToPythonLiteral(),
						text2,
						timeZoneOffsetFormatPart.ZeroIsZ.ToPythonLiteral(),
						timeZoneOffsetFormatPart.AllowNumericZero.ToPythonLiteral()
					}));
				}
				throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Unsupported DateTimeFormatPart encountered: {0}", new object[] { part })));
			}));
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}.DateTimeFormat([{1}])", new object[] { base.HeaderModuleNameForCurrentTranslation, text }));
		}

		// Token: 0x0600FF17 RID: 65303 RVA: 0x00368990 File Offset: 0x00366B90
		protected override Optional<string> GenerateLiteralRepresentation(object literalValue, Type literalType)
		{
			Optional<string> optional = base.GenerateLiteralRepresentation(literalValue, literalType);
			if (optional.HasValue)
			{
				return optional;
			}
			if (literalType == typeof(uint?) || literalType == typeof(decimal?))
			{
				return (((literalValue != null) ? literalValue.ToString() : null) ?? "None").Some<string>();
			}
			if (literalType == typeof(char?))
			{
				return (((literalValue != null) ? literalValue.ToLiteral(null) : null) ?? "None").Some<string>();
			}
			if (literalType == typeof(NumberFormat))
			{
				NumberFormat numberFormat = (NumberFormat)literalValue;
				string text = "{0}.NumberFormat(min_trailing_zeros={1}, max_trailing_zeros={2}, min_trailing_zeros_and_whitespace={3}, min_leading_zeros={4}, min_leading_zeros_and_whitespace={5}, details={6})";
				object[] array = new object[7];
				array[0] = base.HeaderModuleNameForCurrentTranslation;
				array[1] = numberFormat.MinTrailingZeros.Select((uint n) => n.ToString()).OrElse("None");
				array[2] = numberFormat.MaxTrailingZeros.Select((uint n) => n.ToString()).OrElse("None");
				array[3] = numberFormat.MinTrailingZerosAndWhitespace.Select((uint n) => n.ToString()).OrElse("None");
				array[4] = numberFormat.MinLeadingZeros.Select((uint n) => n.ToString()).OrElse("None");
				array[5] = numberFormat.MinLeadingZerosAndWhitespace.Select((uint n) => n.ToString()).OrElse("None");
				array[6] = this.GenerateLiteralRepresentation(numberFormat.Details, typeof(NumberFormatDetails));
				return FormattableString.Invariant(FormattableStringFactory.Create(text, array)).Some<string>();
			}
			if (literalType == typeof(NumberFormatDetails))
			{
				NumberFormatDetails numberFormatDetails = (NumberFormatDetails)literalValue;
				base.CurrentTranslationModule.AddImports(new string[] { "decimal" });
				string text2 = "{0}.NumberFormatDetails(decimal_mark_char={1}, separator_char={2}, separated_section_sizes={3}, scale={4}, currency_symbol={5})";
				object[] array2 = new object[6];
				array2[0] = base.HeaderModuleNameForCurrentTranslation;
				array2[1] = numberFormatDetails.DecimalMarkChar.ToString().ToPythonLiteral();
				array2[2] = numberFormatDetails.SeparatorChar.Select((char ch) => ch.ToString().ToPythonLiteral()).OrElse("None");
				array2[3] = numberFormatDetails.SeparatedSectionSizes.Select((uint[] sizes) => FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[] { string.Join<uint>(", ", sizes) }))).OrElse("None");
				array2[4] = numberFormatDetails.Scale.Select((decimal scale) => FormattableString.Invariant(FormattableStringFactory.Create("decimal.Decimal(\"{0}\")", new object[] { scale }))).OrElse("None");
				array2[5] = (numberFormatDetails.CurrencySymbol.HasValue ? numberFormatDetails.CurrencySymbol.Value.ToPythonLiteral() : "None");
				return FormattableString.Invariant(FormattableStringFactory.Create(text2, array2)).Some<string>();
			}
			if (literalType == typeof(RoundingSpec))
			{
				RoundingSpec roundingSpec = (RoundingSpec)literalValue;
				base.CurrentTranslationModule.AddImports(new string[] { "decimal" });
				return FormattableString.Invariant(FormattableStringFactory.Create("({0}.decimal.Decimal({1}), {2}.decimal.Decimal({3}), \"{4}\")", new object[]
				{
					base.HeaderModuleNameForCurrentTranslation,
					roundingSpec.Zero.ToString(CultureInfo.InvariantCulture).ToPythonLiteral(),
					base.HeaderModuleNameForCurrentTranslation,
					roundingSpec.Delta.ToString(CultureInfo.InvariantCulture).ToPythonLiteral(),
					PythonNameUtils.ConvertStringToSnakeCase(roundingSpec.Mode.ToString())
				})).Some<string>();
			}
			if (literalType == typeof(DateTimeRoundingSpec))
			{
				return this.GenerateLiteralDatetimeRoundingSpecRepresentation(literalValue).Some<string>();
			}
			if (literalType == typeof(RegularExpression))
			{
				IEnumerable<string> enumerable = PythonRegexUtils.ConvertToPythonRegEx((RegularExpression)literalValue);
				string text3 = string.Join(", ", enumerable.Select((string r) => r.ToPythonLiteral()));
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}.RegularExpression([{1}])", new object[] { base.HeaderModuleNameForCurrentTranslation, text3 })).Some<string>();
			}
			if (literalType == typeof(DateTimeFormat))
			{
				DateTimeFormat dateTimeFormat = (DateTimeFormat)literalValue;
				return this._translateDateTimeFormat(dateTimeFormat).Some<string>();
			}
			if (literalType == typeof(DateTimeFormat[]))
			{
				DateTimeFormat[] array3 = (DateTimeFormat[])literalValue;
				return FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[] { string.Join(", ", array3.Select(new Func<DateTimeFormat, string>(this._translateDateTimeFormat))) })).Some<string>();
			}
			if (typeof(CustomExtractor).GetTypeInfo().IsAssignableFrom(literalType))
			{
				if (literalValue.GetType() == typeof(RegexBasedExtractor))
				{
					string text4 = ((RegexBasedExtractor)literalValue).Regex.ToString().ToPythonLiteral();
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}.external_extractor_position_pair_regex_extractor({1}.RegularExpression([{2}]))", new object[] { base.HeaderModuleNameForCurrentTranslation, base.HeaderModuleNameForCurrentTranslation, text4 })).Some<string>();
				}
				CustomExtractor customExtractor = (CustomExtractor)literalValue;
				string text5 = base.GenerateNewName("_extractor_fun_", null);
				customExtractor.BindTranslation(base.CurrentTranslationModule, text5, TargetLanguage.Python, base.HeaderModuleNameForCurrentTranslation);
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}", new object[] { text5 })).Some<string>();
			}
			else
			{
				if (literalType == typeof(EntityType))
				{
					EntityType entityType = (EntityType)literalValue;
					return EntityMappings.EntityDescriptors[entityType].PythonTypeName.Some<string>();
				}
				return Optional<string>.Nothing;
			}
		}

		// Token: 0x0600FF18 RID: 65304 RVA: 0x00368F9C File Offset: 0x0036719C
		private string GenerateLiteralDatetimeRoundingSpecRepresentation(object literalValue)
		{
			DateTimeRoundingSpec dateTimeRoundingSpec = (DateTimeRoundingSpec)literalValue;
			PartialDateTime zero = dateTimeRoundingSpec.Zero;
			string text = string.Join(", ", from part in zero.Parts.AsEnumerable()
				select PythonTranslator.GenerateLiteralDateTimePartRepresentation(part, zero));
			string text2 = dateTimeRoundingSpec.Delta.ToString(CultureInfo.InvariantCulture).ToPythonLiteral();
			string text3 = PythonNameUtils.ConvertStringToSnakeCase(dateTimeRoundingSpec.Unit.ToString());
			string text4 = PythonNameUtils.ConvertStringToSnakeCase(dateTimeRoundingSpec.Mode.ToString());
			string text5 = "";
			if (dateTimeRoundingSpec.UpperExcludePart != null)
			{
				string text6 = PythonNameUtils.ConvertStringToSnakeCase(dateTimeRoundingSpec.UpperExcludePart.Value.ToString());
				string text7 = dateTimeRoundingSpec.UpperExcludeAmount.ToString(CultureInfo.InvariantCulture).ToPythonLiteral();
				text5 = FormattableString.Invariant(FormattableStringFactory.Create(", \"{0}\", int({1})", new object[] { text6, text7 }));
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("({0}.PartialDateTime.create({1}), int({2}), \"{3}\", \"{4}\"{5})", new object[] { base.HeaderModuleNameForCurrentTranslation, text, text2, text3, text4, text5 }));
		}

		// Token: 0x0600FF19 RID: 65305 RVA: 0x003690F4 File Offset: 0x003672F4
		private static string GenerateLiteralDateTimePartRepresentation(DateTimePart part, PartialDateTime zero)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}=int({1})", new object[]
			{
				PythonNameUtils.ConvertStringToSnakeCase(part.ToString()),
				zero.Get(part).Value
			}));
		}

		// Token: 0x04005FBB RID: 24507
		private static readonly Dictionary<string, string> OperatorMappingDictionary = (from m in typeof(Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Semantics).GetTypeInfo().GetMethods(BindingFlags.Static | BindingFlags.Public)
			select new KeyValuePair<string, string>(m.Name, FormattableString.Invariant(FormattableStringFactory.Create("{0}", new object[] { PythonNameUtils.ConvertStringToSnakeCase(m.Name) })))).Concat(from m in typeof(Microsoft.ProgramSynthesis.Conditionals.Semantics.Semantics).GetTypeInfo().GetMethods(BindingFlags.Static | BindingFlags.Public)
			select new KeyValuePair<string, string>(m.Name, FormattableString.Invariant(FormattableStringFactory.Create("conditionals_{0}", new object[] { PythonNameUtils.ConvertStringToSnakeCase(m.Name) })))).ToDictionary<string, string>();
	}
}
