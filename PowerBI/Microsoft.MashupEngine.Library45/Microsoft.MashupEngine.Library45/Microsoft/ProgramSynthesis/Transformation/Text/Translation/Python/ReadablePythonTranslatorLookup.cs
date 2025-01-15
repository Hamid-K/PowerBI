using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Python
{
	// Token: 0x02001DC3 RID: 7619
	internal class ReadablePythonTranslatorLookup
	{
		// Token: 0x0600FF7B RID: 65403 RVA: 0x0036BD40 File Offset: 0x00369F40
		internal static PartitionedCode ToReadableLookup(Lookup p)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonExpr(p.x.Variable);
			if (partitionedCode == null)
			{
				return null;
			}
			IReadOnlyDictionary<Optional<string>, string> value = p.lookupDictionary.Value;
			string text = string.Join(", ", value.Select((KeyValuePair<Optional<string>, string> kvp) => FormattableString.Invariant(FormattableStringFactory.Create("{0}: {1}", new object[]
			{
				ReadablePythonTranslatorLookup.<ToReadableLookup>g__Key2Str|0_0(kvp.Key),
				kvp.Value.ToPythonLiteral()
			}))));
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.ObjType, "lookup_dict");
			partitionedCode.LocalAddLine(new SSAStep(ssaregister, PythonExpressionUtils.MkLiteral(FormattableString.Invariant(FormattableStringFactory.Create("{{ {0} }}", new object[] { text }))), ""));
			partitionedCode.SetExpr(PythonExpressionUtils.Dot(new SSAValue[]
			{
				ssaregister,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "get", new SSAValue[] { partitionedCode.Expr })
			}));
			return partitionedCode;
		}

		// Token: 0x0600FF7D RID: 65405 RVA: 0x0036BE20 File Offset: 0x0036A020
		[CompilerGenerated]
		internal static string <ToReadableLookup>g__Key2Str|0_0(Optional<string> s)
		{
			return s.Select((string str) => str.ToPythonLiteral()).OrElse("None");
		}
	}
}
