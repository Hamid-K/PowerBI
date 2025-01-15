using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Split.Text.Translation.Python
{
	// Token: 0x020013F7 RID: 5111
	public class PythonTranslator : PythonTranslator<PythonModule, SplitProgram, StringRegion, SplitCell[]>
	{
		// Token: 0x17001AC4 RID: 6852
		// (get) Token: 0x06009E00 RID: 40448 RVA: 0x00218284 File Offset: 0x00216484
		protected override IReadOnlyDictionary<string, string> OperatorMapping
		{
			get
			{
				return PythonTranslator.OperatorMappingDictionary;
			}
		}

		// Token: 0x06009E01 RID: 40449 RVA: 0x0021828C File Offset: 0x0021648C
		public override PythonHeaderModule GenerateHeaderModule(SplitProgram p, string headerModuleName)
		{
			Assembly assembly = typeof(PythonTranslator).GetTypeInfo().Assembly;
			string name = assembly.GetName().Name;
			string text = FormattableString.Invariant(FormattableStringFactory.Create("{0}.Translation.Python.split_python_semantics.py", new object[] { name }));
			return new PythonHeaderModule(headerModuleName, new string[] { AssemblyResourceUtils.LoadResourceFromAssembly(assembly, text) });
		}

		// Token: 0x06009E02 RID: 40450 RVA: 0x002182EA File Offset: 0x002164EA
		public override PythonModule CreateModule(string moduleName, string headerModuleReferenceName, string aliasName = "stext")
		{
			return new PythonModule(moduleName, headerModuleReferenceName, aliasName);
		}

		// Token: 0x06009E03 RID: 40451 RVA: 0x002182F4 File Offset: 0x002164F4
		protected override Optional<string> GenerateLiteralRepresentation(object literalValue, Type literalType)
		{
			if (literalType == typeof(RegularExpression))
			{
				RegularExpression regularExpression = (RegularExpression)literalValue;
				string text = string.Join(", ", from r in regularExpression.ToRegexJsonArray()
					select r.ToLiteral(null));
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}.RegularExpression([{1}])", new object[] { base.HeaderModuleNameForCurrentTranslation, text })).Some<string>();
			}
			if (literalType == typeof(Record<int, int, int, int>?))
			{
				Record<int, int, int, int>? record = (Record<int, int, int, int>?)literalValue;
				return FormattableString.Invariant(FormattableStringFactory.Create("({0},{1},{2},{3})", new object[]
				{
					record.Value.Item1,
					record.Value.Item2,
					record.Value.Item3,
					record.Value.Item4
				})).Some<string>();
			}
			if (literalType == typeof(Record<int, int, int, int>[]))
			{
				Record<int, int, int, int>[] array = (Record<int, int, int, int>[])literalValue;
				Func<Record<int, int, int, int>, Optional<string>> f = (Record<int, int, int, int> t) => FormattableString.Invariant(FormattableStringFactory.Create("({0},{1},{2},{3})", new object[] { t.Item1, t.Item2, t.Item3, t.Item4 })).Some<string>();
				return FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[] { string.Join<Optional<string>>(", ", array.Select((Record<int, int, int, int> t) => f(t))) })).Some<string>();
			}
			if (literalType == typeof(int[]))
			{
				int[] array2 = (int[])literalValue;
				return FormattableString.Invariant(FormattableStringFactory.Create("set([{0}])", new object[] { string.Join<int>(", ", array2) })).Some<string>();
			}
			if (literalType == typeof(bool))
			{
				return (((bool)literalValue) ? "True" : "False").Some<string>();
			}
			if (literalType == typeof(FillStrategy))
			{
				return Convert.ToString((int)((FillStrategy)literalValue), CultureInfo.InvariantCulture).Some<string>();
			}
			return base.GenerateLiteralRepresentation(literalValue, literalType);
		}

		// Token: 0x06009E04 RID: 40452 RVA: 0x0021851B File Offset: 0x0021671B
		public PythonTranslator()
			: base(null)
		{
		}

		// Token: 0x04003FF5 RID: 16373
		private static readonly Dictionary<string, string> OperatorMappingDictionary = typeof(Semantics).GetTypeInfo().GetMethods(BindingFlags.Static | BindingFlags.Public).ToDictionary((MethodInfo m) => m.Name, (MethodInfo m) => FormattableString.Invariant(FormattableStringFactory.Create("split_{0}", new object[] { PythonNameUtils.ConvertStringToSnakeCase(m.Name) })));
	}
}
