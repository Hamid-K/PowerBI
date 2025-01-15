using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Translation.Simplification;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Python
{
	// Token: 0x02001DC8 RID: 7624
	public class ReadablePythonTranslator : SubprogramTranslator<PythonHeaderModule, PythonModule, Program, IRow, object>
	{
		// Token: 0x0600FF9C RID: 65436 RVA: 0x0036D4CD File Offset: 0x0036B6CD
		internal ReadablePythonTranslator()
			: base("Readable TText Python Translator")
		{
			ReadablePythonTranslator.VariablesUsed.Clear();
		}

		// Token: 0x0600FF9D RID: 65437 RVA: 0x0036D4E4 File Offset: 0x0036B6E4
		public override Optional<SubprogramTranslationResult> Translate(ProgramNode subprogram, OptimizeFor optimization, Translator<PythonHeaderModule, PythonModule, Program, IRow, object> caller)
		{
			ReadablePythonTranslator._caller = caller;
			ReadablePythonTranslator.optimizeFor = optimization;
			ReadablePythonTranslator.VariablesUsed.Clear();
			Optional<SubprogramTranslationResult> nothing = Optional<SubprogramTranslationResult>.Nothing;
			if (subprogram == null || optimization != OptimizeFor.Readability || subprogram.GrammarRule == null)
			{
				return nothing;
			}
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonExpr(subprogram);
			if (partitionedCode == null)
			{
				return nothing;
			}
			IEnumerable<Record<string, IGeneratedFunction>> functionBindings = partitionedCode.GetFunctionBindings();
			List<SSAStep> local = partitionedCode.Local;
			SSAValue ssavalue = partitionedCode.Expr;
			SSARValue ssarvalue = partitionedCode.Expr as SSARValue;
			if (ssarvalue != null)
			{
				SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.StrType, "computed_value");
				local.Add(new SSAStep(ssaregister, ssarvalue, ""));
				ssavalue = ssaregister;
			}
			return new SubprogramTranslationResult(functionBindings, local, ssavalue, partitionedCode.Imports).Some<SubprogramTranslationResult>();
		}

		// Token: 0x0600FF9E RID: 65438 RVA: 0x0036D590 File Offset: 0x0036B790
		internal static bool IsConstStr(ProgramNode p, out string strValue)
		{
			strValue = null;
			ConstStr constStr;
			Atom atom;
			ConstStr constStr2;
			RangeConstStr rangeConstStr;
			if (Language.Build.Node.IsRule.ConstStr(p, out constStr))
			{
				strValue = constStr.s.Value;
			}
			else if (Language.Build.Node.IsRule.Atom(p, out atom) && atom.f.Is_ConstStr(Language.Build, out constStr2))
			{
				strValue = constStr2.s.Value;
			}
			else if (Language.Build.Node.IsRule.RangeConstStr(p, out rangeConstStr))
			{
				strValue = rangeConstStr.s.Value;
			}
			else
			{
				DtRangeConstStr dtRangeConstStr;
				if (!Language.Build.Node.IsRule.DtRangeConstStr(p, out dtRangeConstStr))
				{
					return false;
				}
				strValue = dtRangeConstStr.s.Value;
			}
			return true;
		}

		// Token: 0x0600FF9F RID: 65439 RVA: 0x0036D670 File Offset: 0x0036B870
		internal static PartitionedCode ToReadablePythonExpr(ProgramNode p)
		{
			if (ReadablePythonTranslator.ProgramContainsNotHandledPatterns(p))
			{
				return null;
			}
			VariableNode variableNode = p as VariableNode;
			if (variableNode != null)
			{
				SSAValue ssavalue = ReadablePythonTranslator.ResolveVariable(variableNode);
				return new PartitionedCode(ssavalue, null, null, new SSAValue[] { ssavalue });
			}
			ConversionRule conversionRule = p.GrammarRule as ConversionRule;
			if (conversionRule != null)
			{
				return ReadablePythonTranslator.ToReadablePythonConversion(conversionRule, p);
			}
			IfThenElse ifThenElse;
			if (Language.Build.Node.IsRule.IfThenElse(p, out ifThenElse))
			{
				return ReadablePythonTranslator.ToReadablePythonIfThenElse(ifThenElse);
			}
			Concat concat;
			if (Language.Build.Node.IsRule.Concat(p, out concat))
			{
				return ReadablePythonTranslator.ToReadablePythonConcat(concat);
			}
			SubStr subStr;
			if (Language.Build.Node.IsRule.SubStr(p, out subStr))
			{
				return ReadablePythonTranslator.ToReadablePythonSubStr(subStr);
			}
			ToLowercase toLowercase;
			if (Language.Build.Node.IsRule.ToLowercase(p, out toLowercase))
			{
				return ReadablePythonTranslator.ToReadablePythonToLower(toLowercase);
			}
			ToUppercase toUppercase;
			if (Language.Build.Node.IsRule.ToUppercase(p, out toUppercase))
			{
				return ReadablePythonTranslator.ToReadablePythonToUpper(toUppercase);
			}
			ToSimpleTitleCase toSimpleTitleCase;
			if (Language.Build.Node.IsRule.ToSimpleTitleCase(p, out toSimpleTitleCase))
			{
				return ReadablePythonTranslator.ToReadablePythonToTitle(toSimpleTitleCase);
			}
			IsNull isNull;
			if (Language.Build.Node.IsRule.IsNull(p, out isNull))
			{
				return ReadablePythonTranslator.ToReadablePythonIsNull(isNull);
			}
			IsWhiteSpace isWhiteSpace;
			if (Language.Build.Node.IsRule.IsWhiteSpace(p, out isWhiteSpace))
			{
				return ReadablePythonTranslator.ToReadablePythonIsWhiteSpace(isWhiteSpace);
			}
			IsNullOrWhiteSpace isNullOrWhiteSpace;
			if (Language.Build.Node.IsRule.IsNullOrWhiteSpace(p, out isNullOrWhiteSpace))
			{
				return ReadablePythonTranslator.ToReadablePythonIsNullOrWhiteSpace(isNullOrWhiteSpace);
			}
			StartsWith startsWith;
			if (Language.Build.Node.IsRule.StartsWith(p, out startsWith))
			{
				return ReadablePythonTranslator.ToReadablePythonStartsWith(startsWith);
			}
			EndsWith endsWith;
			if (Language.Build.Node.IsRule.EndsWith(p, out endsWith))
			{
				return ReadablePythonTranslator.ToReadablePythonEndsWith(endsWith);
			}
			Matches matches;
			if (Language.Build.Node.IsRule.Matches(p, out matches))
			{
				return ReadablePythonTranslator.ToReadablePythonMatches(matches);
			}
			string text;
			if (ReadablePythonTranslator.IsConstStr(p, out text))
			{
				return new PartitionedCode(PythonExpressionUtils.MkPyLiteral(text), null, null, null);
			}
			Lookup lookup;
			if (Language.Build.Node.IsRule.Lookup(p, out lookup))
			{
				return ReadablePythonTranslatorLookup.ToReadableLookup(lookup);
			}
			FormatPartialDateTime formatPartialDateTime;
			if (Language.Build.Node.IsRule.FormatPartialDateTime(p, out formatPartialDateTime))
			{
				return ReadablePythonTranslatorDateTime.ToReadableDateTime(formatPartialDateTime);
			}
			LetSharedParsedDateTime letSharedParsedDateTime;
			if (Language.Build.Node.IsRule.LetSharedParsedDateTime(p, out letSharedParsedDateTime))
			{
				return ReadablePythonTranslatorDateTime.ToReadableDateTime(letSharedParsedDateTime);
			}
			FormatNumber formatNumber;
			if (Language.Build.Node.IsRule.FormatNumber(p, out formatNumber))
			{
				return ReadablePythonTranslatorNumber.ToReadableNumber(formatNumber);
			}
			LetSharedParsedNumber letSharedParsedNumber;
			if (Language.Build.Node.IsRule.LetSharedParsedNumber(p, out letSharedParsedNumber))
			{
				return ReadablePythonTranslatorNumber.ToReadableNumber(letSharedParsedNumber);
			}
			return null;
		}

		// Token: 0x0600FFA0 RID: 65440 RVA: 0x0036D910 File Offset: 0x0036BB10
		private static bool ProgramContainsNotHandledPatterns(ProgramNode pNode)
		{
			conv conv = ReadablePythonTranslator.Hole.conv;
			ProgramNode node = ReadablePythonTranslator.<ProgramContainsNotHandledPatterns>g__GetOneColumnPattern|11_0(ReadablePythonTranslator.Hole.idx, conv).Node;
			return ProgramSetRewriter.ExtractMappings(pNode, node) != null || ProgramSetRewriter.ExtractMappings(pNode, ReadablePythonTranslator.Rule.LetX(ReadablePythonTranslator.Rule.ChooseInput(ReadablePythonTranslator.Var.vs, ReadablePythonTranslator.Var.columnName), conv).Node) != null || pNode.Equals(ReadablePythonTranslator.Var.columnName);
		}

		// Token: 0x0600FFA1 RID: 65441 RVA: 0x0036D9A4 File Offset: 0x0036BBA4
		internal static SSAValue ResolveVariable(string name)
		{
			SSAValue ssavalue = ReadablePythonTranslator._caller.ResolveVariable(name);
			if (!ReadablePythonTranslator.VariablesUsed.Contains(ssavalue))
			{
				ReadablePythonTranslator.VariablesUsed.Add(ssavalue);
			}
			return ssavalue;
		}

		// Token: 0x0600FFA2 RID: 65442 RVA: 0x0036D9D6 File Offset: 0x0036BBD6
		internal static SSAValue ResolveVariable(VariableNode variable)
		{
			return ReadablePythonTranslator.ResolveVariable(variable.Symbol.Name);
		}

		// Token: 0x0600FFA3 RID: 65443 RVA: 0x0036D9E8 File Offset: 0x0036BBE8
		private static PartitionedCode ToReadablePythonConversion(ConversionRule conversionRule, ProgramNode node)
		{
			ReadablePythonTranslator._caller.PushBindingScope(conversionRule.Substitutions.ToDictionary((KeyValuePair<Symbol, Symbol> p) => p.Key.Name, (KeyValuePair<Symbol, Symbol> p) => ReadablePythonTranslator.ResolveVariable(p.Value.Name)));
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonExpr(node.Children[0]);
			ReadablePythonTranslator._caller.PopBindingScope();
			return partitionedCode;
		}

		// Token: 0x0600FFA4 RID: 65444 RVA: 0x0036DA60 File Offset: 0x0036BC60
		private static PartitionedCode ToReadablePythonIfThenElse(IfThenElse p)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonExpr(p.b.Node);
			if (partitionedCode == null)
			{
				return null;
			}
			PartitionedCode partitionedCode2 = ReadablePythonTranslator.ToReadablePythonExpr(p.st.Node);
			if (partitionedCode2 == null)
			{
				return null;
			}
			PartitionedCode partitionedCode3 = ReadablePythonTranslator.ToReadablePythonExpr(p.@switch.Node);
			if (partitionedCode3 == null)
			{
				return null;
			}
			if (partitionedCode2.Local.Count == 0 && partitionedCode3.Local.Count == 0)
			{
				partitionedCode.Merge(partitionedCode2);
				partitionedCode.Merge(partitionedCode3);
				partitionedCode.SetExpr(PythonExpressionUtils.IfThenElse(partitionedCode.Expr, partitionedCode2.Expr, partitionedCode3.Expr));
				return partitionedCode;
			}
			SSAValue ssavalue;
			partitionedCode.AddContext("the_then_branch", partitionedCode2, out ssavalue);
			SSAValue ssavalue2;
			partitionedCode.AddContext("the_else_branch", partitionedCode3, out ssavalue2);
			partitionedCode.SetExpr(PythonExpressionUtils.IfThenElse(partitionedCode.Expr, ssavalue, ssavalue2));
			return partitionedCode;
		}

		// Token: 0x0600FFA5 RID: 65445 RVA: 0x0036DB38 File Offset: 0x0036BD38
		private static PartitionedCode ToReadablePythonConcat(Concat pNode)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonExpr(pNode.f.Node);
			if (partitionedCode == null)
			{
				return null;
			}
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.StrType, "part");
			ssaregister = partitionedCode.IntroduceNewVarIf(ssaregister);
			PartitionedCode partitionedCode2 = ReadablePythonTranslator.ToReadablePythonExpr(pNode.e.Node);
			if (partitionedCode2 == null)
			{
				return null;
			}
			partitionedCode.Merge(partitionedCode2);
			partitionedCode.SetExpr(PythonExpressionUtils.Add(new SSAValue[] { partitionedCode.Expr, partitionedCode2.Expr }));
			return partitionedCode;
		}

		// Token: 0x0600FFA6 RID: 65446 RVA: 0x0036DBBE File Offset: 0x0036BDBE
		private static PartitionedCode ToReadablePythonStartsWith(StartsWith p)
		{
			return ReadablePythonTranslator.ToReadablePythonMatch(p.s, p.r, true, false);
		}

		// Token: 0x0600FFA7 RID: 65447 RVA: 0x0036DBD5 File Offset: 0x0036BDD5
		private static PartitionedCode ToReadablePythonEndsWith(EndsWith p)
		{
			return ReadablePythonTranslator.ToReadablePythonMatch(p.s, p.r, false, true);
		}

		// Token: 0x0600FFA8 RID: 65448 RVA: 0x0036DBEC File Offset: 0x0036BDEC
		private static PartitionedCode ToReadablePythonMatches(Matches p)
		{
			return ReadablePythonTranslator.ToReadablePythonMatch(p.s, p.r, true, true);
		}

		// Token: 0x0600FFA9 RID: 65449 RVA: 0x0036DC04 File Offset: 0x0036BE04
		private static PartitionedCode ToReadablePythonMatch(Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.s prgm, Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.r regex, bool mustMatchAtBeginning = true, bool mustMatchAtEnd = true)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonExpr(prgm.Node);
			if (partitionedCode == null)
			{
				return null;
			}
			string text2;
			string text = PythonRegexUtils.ConvertToPythonRegExStr(regex.Value, out text2);
			if (!mustMatchAtBeginning)
			{
				text = "(.|\\n)*" + text;
			}
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.StrType, ReadablePythonTranslator.TokenName2PythonVariableName(text2));
			partitionedCode.LocalAddLine(new SSAStep(ssaregister, PythonExpressionUtils.MkPyLiteral(text), ""));
			partitionedCode.AddImport("regex");
			SSARValue ssarvalue;
			if (!mustMatchAtEnd)
			{
				ssarvalue = PythonExpressionUtils.RegexMatch(new SSAValue[] { ssaregister, partitionedCode.Expr });
			}
			else
			{
				ssarvalue = PythonExpressionUtils.RegexFullMatch(ssaregister, partitionedCode.Expr);
			}
			SSARValue ssarvalue2 = PythonExpressionUtils.And(new SSAValue[]
			{
				PythonExpressionUtils.IsInstanceStr(partitionedCode.Expr),
				ssarvalue
			});
			partitionedCode.SetExpr(ssarvalue2);
			return partitionedCode;
		}

		// Token: 0x0600FFAA RID: 65450 RVA: 0x0036DCCC File Offset: 0x0036BECC
		private static PartitionedCode ToReadablePythonIsNull(IsNull p)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonExpr(p.s.Node);
			if (partitionedCode != null)
			{
				partitionedCode.SetExpr(PythonExpressionUtils.Equals(partitionedCode.Expr, PythonExpressionUtils.None));
			}
			return partitionedCode;
		}

		// Token: 0x0600FFAB RID: 65451 RVA: 0x0036DD08 File Offset: 0x0036BF08
		private static PartitionedCode ToReadablePythonIsWhiteSpace(IsWhiteSpace p)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonExpr(p.s.Node);
			if (partitionedCode != null)
			{
				partitionedCode.SetExpr(PythonExpressionUtils.Equals(PythonExpressionUtils.Len(PythonExpressionUtils.Strip(partitionedCode.Expr, null)), PythonExpressionUtils.Zero));
			}
			return partitionedCode;
		}

		// Token: 0x0600FFAC RID: 65452 RVA: 0x0036DD50 File Offset: 0x0036BF50
		private static PartitionedCode ToReadablePythonIsNullOrWhiteSpace(IsNullOrWhiteSpace p)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonExpr(p.s.Node);
			if (partitionedCode != null)
			{
				partitionedCode.SetExpr(PythonExpressionUtils.Or(new SSAValue[]
				{
					PythonExpressionUtils.Equals(partitionedCode.Expr, PythonExpressionUtils.None),
					PythonExpressionUtils.Equals(PythonExpressionUtils.Len(PythonExpressionUtils.Strip(partitionedCode.Expr, null)), PythonExpressionUtils.Zero)
				}));
			}
			return partitionedCode;
		}

		// Token: 0x0600FFAD RID: 65453 RVA: 0x0036DDB8 File Offset: 0x0036BFB8
		private static PartitionedCode ToReadablePythonToLower(ToLowercase p)
		{
			SS ss = p.SS;
			Func<SSAValue, SSARValue> func;
			if ((func = ReadablePythonTranslator.<>O.<0>__Lower) == null)
			{
				func = (ReadablePythonTranslator.<>O.<0>__Lower = new Func<SSAValue, SSARValue>(PythonExpressionUtils.Lower));
			}
			return ReadablePythonTranslator.ToReadablePythonSS(ss, func);
		}

		// Token: 0x0600FFAE RID: 65454 RVA: 0x0036DDE1 File Offset: 0x0036BFE1
		private static PartitionedCode ToReadablePythonToUpper(ToUppercase p)
		{
			SS ss = p.SS;
			Func<SSAValue, SSARValue> func;
			if ((func = ReadablePythonTranslator.<>O.<1>__Upper) == null)
			{
				func = (ReadablePythonTranslator.<>O.<1>__Upper = new Func<SSAValue, SSARValue>(PythonExpressionUtils.Upper));
			}
			return ReadablePythonTranslator.ToReadablePythonSS(ss, func);
		}

		// Token: 0x0600FFAF RID: 65455 RVA: 0x0036DE0A File Offset: 0x0036C00A
		private static PartitionedCode ToReadablePythonToTitle(ToSimpleTitleCase p)
		{
			SS ss = p.SS;
			Func<SSAValue, SSARValue> func;
			if ((func = ReadablePythonTranslator.<>O.<2>__Title) == null)
			{
				func = (ReadablePythonTranslator.<>O.<2>__Title = new Func<SSAValue, SSARValue>(PythonExpressionUtils.Title));
			}
			return ReadablePythonTranslator.ToReadablePythonSS(ss, func);
		}

		// Token: 0x0600FFB0 RID: 65456 RVA: 0x0036DE34 File Offset: 0x0036C034
		private static PartitionedCode ToReadablePythonSS(SS p, Func<SSAValue, SSARValue> lowerOrUpper)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonExpr(p.Node);
			if (partitionedCode == null)
			{
				return null;
			}
			partitionedCode.SetExpr(lowerOrUpper(partitionedCode.Expr));
			return partitionedCode;
		}

		// Token: 0x0600FFB1 RID: 65457 RVA: 0x0036DE68 File Offset: 0x0036C068
		private static PartitionedCode ToReadablePythonSubStr(SubStr p)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonERRE(p);
			if (partitionedCode != null)
			{
				return partitionedCode;
			}
			PartitionedCode partitionedCode2 = ReadablePythonTranslator.ToReadablePythonREER(p);
			if (partitionedCode2 != null)
			{
				return partitionedCode2;
			}
			SSAValue ssavalue = ReadablePythonTranslator.ResolveVariable(p.x.Variable);
			PP pp = p.PP;
			PP pp2 = p.PP;
			GrammarBuilders build = Language.Build;
			Func<PosPair, Optional<Record<PartitionedCode, PartitionedCode>>> func;
			if ((func = ReadablePythonTranslator.<>O.<3>__TranslatePosPair) == null)
			{
				func = (ReadablePythonTranslator.<>O.<3>__TranslatePosPair = new Func<PosPair, Optional<Record<PartitionedCode, PartitionedCode>>>(ReadablePythonTranslator.TranslatePosPair));
			}
			Func<LetPL1, Optional<Record<PartitionedCode, PartitionedCode>>> func2;
			if ((func2 = ReadablePythonTranslator.<>O.<4>__TranslateLetPL1) == null)
			{
				func2 = (ReadablePythonTranslator.<>O.<4>__TranslateLetPL1 = new Func<LetPL1, Optional<Record<PartitionedCode, PartitionedCode>>>(ReadablePythonTranslator.TranslateLetPL1));
			}
			Func<RegexPositionPair, Optional<Record<PartitionedCode, PartitionedCode>>> func3;
			if ((func3 = ReadablePythonTranslator.<>O.<5>__TranslateRegexPosPair) == null)
			{
				func3 = (ReadablePythonTranslator.<>O.<5>__TranslateRegexPosPair = new Func<RegexPositionPair, Optional<Record<PartitionedCode, PartitionedCode>>>(ReadablePythonTranslator.TranslateRegexPosPair));
			}
			Func<ExternalExtractorPositionPair, Optional<Record<PartitionedCode, PartitionedCode>>> func4;
			if ((func4 = ReadablePythonTranslator.<>O.<6>__TranslateExternalExtractorPosPair) == null)
			{
				func4 = (ReadablePythonTranslator.<>O.<6>__TranslateExternalExtractorPosPair = new Func<ExternalExtractorPositionPair, Optional<Record<PartitionedCode, PartitionedCode>>>(ReadablePythonTranslator.TranslateExternalExtractorPosPair));
			}
			Optional<Record<PartitionedCode, PartitionedCode>> optional = pp2.Switch<Optional<Record<PartitionedCode, PartitionedCode>>>(build, func, func2, func3, func4);
			if (!optional.HasValue)
			{
				return null;
			}
			PartitionedCode item = optional.Value.Item1;
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.NumType, "index1");
			ssaregister = item.IntroduceNewVarIf(ssaregister);
			PartitionedCode item2 = optional.Value.Item2;
			SSARegister ssaregister2 = new SSARegister(null, PythonExpressionUtils.NumType, "index2");
			ssaregister2 = item2.IntroduceNewVarIf(ssaregister2);
			item.Merge(item2);
			SSARValue ssarvalue = PythonExpressionUtils.Slice(ssavalue, item.Expr, item2.Expr);
			item.SetExpr(ssarvalue);
			return item;
		}

		// Token: 0x0600FFB2 RID: 65458 RVA: 0x0036DFB8 File Offset: 0x0036C1B8
		private static Optional<Record<PartitionedCode, PartitionedCode>> TranslatePosPair(PosPair pp)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonIndex(pp.pos1);
			if (partitionedCode == null)
			{
				return Optional<Record<PartitionedCode, PartitionedCode>>.Nothing;
			}
			PartitionedCode partitionedCode2 = ReadablePythonTranslator.ToReadablePythonIndex(pp.pos2);
			if (partitionedCode2 == null)
			{
				return Optional<Record<PartitionedCode, PartitionedCode>>.Nothing;
			}
			return Record.Create<PartitionedCode, PartitionedCode>(partitionedCode, partitionedCode2).Some<Record<PartitionedCode, PartitionedCode>>();
		}

		// Token: 0x0600FFB3 RID: 65459 RVA: 0x0036E000 File Offset: 0x0036C200
		private static Optional<Record<PartitionedCode, PartitionedCode>> TranslateLetPL1(LetPL1 pp)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonIndex(pp.pos);
			if (partitionedCode == null)
			{
				return Optional<Record<PartitionedCode, PartitionedCode>>.Nothing;
			}
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.NumType, "index1");
			ssaregister = partitionedCode.IntroduceNewVarIf(ssaregister);
			VariableNode variable = pp._LetB7.Cast__LetB7()._LetB5.Cast_RSubStr().x.Variable;
			SSARValue ssarvalue = PythonExpressionUtils.RSubStr(ReadablePythonTranslator.ResolveVariable(variable), partitionedCode.Expr);
			ReadablePythonTranslator._caller.PushBindingScope(new Dictionary<string, SSAValue> { 
			{
				variable.Symbol.Name,
				ssarvalue
			} });
			PartitionedCode partitionedCode2 = ReadablePythonTranslator.ToReadablePythonIndex(pp._LetB7.Cast__LetB7()._LetB6.Cast_LetPL2().pos);
			if (partitionedCode2 == null)
			{
				ReadablePythonTranslator._caller.PopBindingScope();
				return Optional<Record<PartitionedCode, PartitionedCode>>.Nothing;
			}
			SSARegister ssaregister2 = new SSARegister(null, PythonExpressionUtils.NumType, "index2");
			partitionedCode2.LocalAddLine(new SSAStep(ssaregister2, PythonExpressionUtils.Add(new SSAValue[] { partitionedCode.Expr, partitionedCode2.Expr }), ""));
			partitionedCode2.SetExpr(ssaregister2);
			ReadablePythonTranslator._caller.PopBindingScope();
			return Record.Create<PartitionedCode, PartitionedCode>(partitionedCode, partitionedCode2).Some<Record<PartitionedCode, PartitionedCode>>();
		}

		// Token: 0x0600FFB4 RID: 65460 RVA: 0x0036E14F File Offset: 0x0036C34F
		private static Optional<Record<PartitionedCode, PartitionedCode>> TranslateRegexPosPair(RegexPositionPair pp)
		{
			return Optional<Record<PartitionedCode, PartitionedCode>>.Nothing;
		}

		// Token: 0x0600FFB5 RID: 65461 RVA: 0x0036E14F File Offset: 0x0036C34F
		private static Optional<Record<PartitionedCode, PartitionedCode>> TranslateExternalExtractorPosPair(ExternalExtractorPositionPair pp)
		{
			return Optional<Record<PartitionedCode, PartitionedCode>>.Nothing;
		}

		// Token: 0x0600FFB6 RID: 65462 RVA: 0x0036E158 File Offset: 0x0036C358
		private static bool IndexPatternMatch(ProgramNode p, ProgramNode pattern, out SSAValue vName, out int k, out AbstractRegexToken[] tokens)
		{
			IReadOnlyDictionary<Hole, ProgramNode> readOnlyDictionary = ProgramSetRewriter.ExtractMappings(p, pattern);
			if (readOnlyDictionary != null)
			{
				VariableNode variableNode = (VariableNode)readOnlyDictionary[(Hole)ReadablePythonTranslator.Hole.x.Node];
				vName = ReadablePythonTranslator.ResolveVariable(variableNode);
				LiteralNode literalNode = readOnlyDictionary[(Hole)ReadablePythonTranslator.Hole.k.Node] as LiteralNode;
				if (literalNode != null)
				{
					k = (int)literalNode.Value;
					RegularExpression regularExpression = ((LiteralNode)readOnlyDictionary[(Hole)ReadablePythonTranslator.Hole.r.Node]).Value as RegularExpression;
					if (regularExpression != null)
					{
						tokens = regularExpression.Tokens.Cast<AbstractRegexToken>().ToArray<AbstractRegexToken>();
						return true;
					}
				}
			}
			vName = null;
			k = 0;
			tokens = null;
			return false;
		}

		// Token: 0x0600FFB7 RID: 65463 RVA: 0x0036E228 File Offset: 0x0036C428
		private static bool IndexPatternERMatch(ProgramNode p, out SSAValue vName, out int k, out AbstractRegexToken[] tokens)
		{
			return ReadablePythonTranslator.IndexPatternMatch(p, ReadablePythonTranslator.Rule.RegexPositionRelative(ReadablePythonTranslator.Hole.x, ReadablePythonTranslator.Rule.RegexPair(ReadablePythonTranslator.EpsilonRegexNode, ReadablePythonTranslator.Hole.r), ReadablePythonTranslator.Hole.k).Node, out vName, out k, out tokens);
		}

		// Token: 0x0600FFB8 RID: 65464 RVA: 0x0036E280 File Offset: 0x0036C480
		private static bool IndexPatternREMatch(ProgramNode p, out SSAValue vName, out int k, out AbstractRegexToken[] tokens)
		{
			return ReadablePythonTranslator.IndexPatternMatch(p, ReadablePythonTranslator.Rule.RegexPositionRelative(ReadablePythonTranslator.Hole.x, ReadablePythonTranslator.Rule.RegexPair(ReadablePythonTranslator.Hole.r, ReadablePythonTranslator.EpsilonRegexNode), ReadablePythonTranslator.Hole.k).Node, out vName, out k, out tokens);
		}

		// Token: 0x17002A7C RID: 10876
		// (get) Token: 0x0600FFB9 RID: 65465 RVA: 0x0036E2D8 File Offset: 0x0036C4D8
		private static ProgramNode ERREPattern
		{
			get
			{
				return ReadablePythonTranslator.Rule.SubStr(ReadablePythonTranslator.Hole.x, ReadablePythonTranslator.Rule.PosPair(ReadablePythonTranslator.Rule.RegexPositionRelative(ReadablePythonTranslator.Hole.x, ReadablePythonTranslator.Rule.RegexPair(ReadablePythonTranslator.EpsilonRegexNode, ReadablePythonTranslator.Hole.r), ReadablePythonTranslator.Hole.k), ReadablePythonTranslator.Rule.RegexPositionRelative(ReadablePythonTranslator.Hole.x, ReadablePythonTranslator.Rule.RegexPair(ReadablePythonTranslator.Hole.r, ReadablePythonTranslator.EpsilonRegexNode), ReadablePythonTranslator.Hole.k))).Node;
			}
		}

		// Token: 0x0600FFBA RID: 65466 RVA: 0x0036E379 File Offset: 0x0036C579
		private static bool IndexPatternERREMatch(SubStr p, out SSAValue vName, out int k, out AbstractRegexToken[] tokens)
		{
			return ReadablePythonTranslator.IndexPatternMatch(p.Node, ReadablePythonTranslator.ERREPattern, out vName, out k, out tokens);
		}

		// Token: 0x0600FFBB RID: 65467 RVA: 0x0036E390 File Offset: 0x0036C590
		private static PartitionedCode ToReadablePythonERRE(SubStr p)
		{
			SSAValue ssavalue;
			int num;
			AbstractRegexToken[] array;
			if (!ReadablePythonTranslator.IndexPatternERREMatch(p, out ssavalue, out num, out array))
			{
				return null;
			}
			return ReadablePythonTranslator.ToReadablePythonIndexRegExPosE(ssavalue, num, array, ReadablePythonTranslator.PosType.Whole);
		}

		// Token: 0x0600FFBC RID: 65468 RVA: 0x0036E3B8 File Offset: 0x0036C5B8
		private static OpaqueGeneratedFunction DeclareKthMatchMultipleTokens()
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			List<string> list = new List<string> { "x", "regexes", "k" };
			using (codeBuilder.NewScope("def to_start_end(m):", 1U))
			{
				codeBuilder.AppendLine("return (m.start(), m.end())");
			}
			using (codeBuilder.NewScope("def zip(iter_list):", 1U))
			{
				codeBuilder.AppendLine("default = (-1, -1)");
				codeBuilder.AppendLine("starts, ends = [0]*N, [0]*N");
				using (codeBuilder.NewScope("while all([i >= 0 for i in starts]):", 1U))
				{
					codeBuilder.AppendLine("disconnects = (i for i in range(N) if (i != N-1 and ends[i] < starts[i+1]) or (i != 0 and ends[i-1] > starts[i]))");
					codeBuilder.AppendLine("index_to_move = next(disconnects, -1)");
					using (codeBuilder.NewScope("if index_to_move != -1:", 1U))
					{
						codeBuilder.AppendLine("starts[index_to_move], ends[index_to_move] = next(iter_list[index_to_move], default)");
					}
					using (codeBuilder.NewScope("else:", 1U))
					{
						codeBuilder.AppendLine("start_end_list = [next(iter_list[i], default) for i in range(N)]");
						codeBuilder.AppendLine("starts, ends = [i[0] for i in start_end_list], [i[1] for i in start_end_list]");
					}
					using (codeBuilder.NewScope("if all([i >= 0 for i in starts]):", 1U))
					{
						codeBuilder.AppendLine("yield (starts, ends)");
					}
				}
			}
			codeBuilder.AppendLine("N = len(regexes)");
			codeBuilder.AppendLine("r_matches = [map(to_start_end, regex.finditer(r, x)) for r in regexes]");
			codeBuilder.AppendLine("all_matches = ((starts[0], ends[-1]) for (starts, ends) in zip(r_matches) if all([ends[i] == starts[i+1] for i in range(N-1)]))");
			codeBuilder.AppendLine("return next(m for (m_index, m) in enumerate(all_matches) if m_index == k-1) if k > 0 else list(all_matches)[k]");
			return new OpaqueGeneratedFunction(list.Select((string x) => Record.Create<string, Type>(x, null)).ToArray<Record<string, Type>>(), null, codeBuilder);
		}

		// Token: 0x0600FFBD RID: 65469 RVA: 0x0036E59C File Offset: 0x0036C79C
		private static PartitionedCode ToReadablePythonIndex(pos p)
		{
			GrammarBuilders build = Language.Build;
			Func<RelativePosition, PartitionedCode> func;
			if ((func = ReadablePythonTranslator.<>O.<7>__TranslateRelativePosition) == null)
			{
				func = (ReadablePythonTranslator.<>O.<7>__TranslateRelativePosition = new Func<RelativePosition, PartitionedCode>(ReadablePythonTranslator.TranslateRelativePosition));
			}
			Func<RegexPositionRelative, PartitionedCode> func2;
			if ((func2 = ReadablePythonTranslator.<>O.<8>__TranslateRegexPositionRelative) == null)
			{
				func2 = (ReadablePythonTranslator.<>O.<8>__TranslateRegexPositionRelative = new Func<RegexPositionRelative, PartitionedCode>(ReadablePythonTranslator.TranslateRegexPositionRelative));
			}
			Func<AbsolutePosition, PartitionedCode> func3;
			if ((func3 = ReadablePythonTranslator.<>O.<9>__TranslateAbsolutePosition) == null)
			{
				func3 = (ReadablePythonTranslator.<>O.<9>__TranslateAbsolutePosition = new Func<AbsolutePosition, PartitionedCode>(ReadablePythonTranslator.TranslateAbsolutePosition));
			}
			Func<RegexPosition, PartitionedCode> func4;
			if ((func4 = ReadablePythonTranslator.<>O.<10>__TranslateRegexPosition) == null)
			{
				func4 = (ReadablePythonTranslator.<>O.<10>__TranslateRegexPosition = new Func<RegexPosition, PartitionedCode>(ReadablePythonTranslator.TranslateRegexPosition));
			}
			return p.Switch<PartitionedCode>(build, func, func2, func3, func4);
		}

		// Token: 0x0600FFBE RID: 65470 RVA: 0x00002188 File Offset: 0x00000388
		private static PartitionedCode TranslateAbsolutePosition(AbsolutePosition x)
		{
			return null;
		}

		// Token: 0x0600FFBF RID: 65471 RVA: 0x00002188 File Offset: 0x00000388
		private static PartitionedCode TranslateRegexPosition(RegexPosition x)
		{
			return null;
		}

		// Token: 0x0600FFC0 RID: 65472 RVA: 0x0036E624 File Offset: 0x0036C824
		private static PartitionedCode TranslateRelativePosition(RelativePosition rpos)
		{
			int value = rpos.k.Value;
			if (value >= 0)
			{
				return new PartitionedCode(PythonExpressionUtils.MkLiteral(value), null, null, null);
			}
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonExpr(rpos.x.Node);
			if (partitionedCode == null)
			{
				return null;
			}
			partitionedCode.SetExpr(PythonExpressionUtils.Add(new SSAValue[]
			{
				PythonExpressionUtils.Len(partitionedCode.Expr),
				PythonExpressionUtils.MkLiteral(value + 1)
			}));
			return partitionedCode;
		}

		// Token: 0x0600FFC1 RID: 65473 RVA: 0x0036E6A4 File Offset: 0x0036C8A4
		private static SSAValue PosType2MethodCall(ReadablePythonTranslator.PosType pos)
		{
			switch (pos)
			{
			case ReadablePythonTranslator.PosType.Begin:
				return PythonExpressionUtils.Start;
			case ReadablePythonTranslator.PosType.End:
				return PythonExpressionUtils.End;
			case ReadablePythonTranslator.PosType.Whole:
				return PythonExpressionUtils.Group(0);
			default:
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Unexpected position type found: {0}", new object[] { pos })));
			}
		}

		// Token: 0x0600FFC2 RID: 65474 RVA: 0x0036E6FC File Offset: 0x0036C8FC
		private static PartitionedCode TranslateRegexPositionRelative(RegexPositionRelative p)
		{
			SSAValue ssavalue = ReadablePythonTranslator.ResolveVariable(p.x.Variable);
			int value = p.k.Value;
			RegexPair regexPair = p.regexPair.Cast_RegexPair();
			RegularExpression value2 = regexPair.r1.Value;
			RegularExpression value3 = regexPair.r2.Value;
			AbstractRegexToken[] array = value2.Tokens.Cast<AbstractRegexToken>().ToArray<AbstractRegexToken>();
			AbstractRegexToken[] array2 = value3.Tokens.Cast<AbstractRegexToken>().ToArray<AbstractRegexToken>();
			if (array.Length == 0)
			{
				return ReadablePythonTranslator.ToReadablePythonIndexRegExPosE(ssavalue, value, array2, ReadablePythonTranslator.PosType.Begin);
			}
			if (array2.Length == 0)
			{
				return ReadablePythonTranslator.ToReadablePythonIndexRegExPosE(ssavalue, value, array, ReadablePythonTranslator.PosType.End);
			}
			return ReadablePythonTranslator.TranslateRegexPositionRelativeGeneralCase(ssavalue, value, array, array2);
		}

		// Token: 0x0600FFC3 RID: 65475 RVA: 0x0036E7B0 File Offset: 0x0036C9B0
		private static bool MayOverlap(IEnumerable<AbstractRegexToken> tokens)
		{
			if (!tokens.Any((AbstractRegexToken t) => TokenOverlap.MaySelfOverlap(t)))
			{
				IEnumerable<Record<AbstractRegexToken, AbstractRegexToken>> enumerable = tokens.Windowed<AbstractRegexToken>();
				Func<AbstractRegexToken, AbstractRegexToken, bool> func;
				if ((func = ReadablePythonTranslator.<>O.<11>__MayOverlap) == null)
				{
					func = (ReadablePythonTranslator.<>O.<11>__MayOverlap = new Func<AbstractRegexToken, AbstractRegexToken, bool>(TokenOverlap.MayOverlap));
				}
				return enumerable.Any2(func);
			}
			return true;
		}

		// Token: 0x0600FFC4 RID: 65476 RVA: 0x0036E80C File Offset: 0x0036CA0C
		private static PartitionedCode TranslateRegexPositionRelativeGeneralCase(SSAValue x, int k, AbstractRegexToken[] tokens1, AbstractRegexToken[] tokens2)
		{
			SSARegister ssaregister;
			if (ReadablePythonTranslator.MayOverlap(tokens1.Concat(tokens2)))
			{
				string[] array = tokens1.Select((AbstractRegexToken y) => ReadablePythonTranslator.Token2PythonRegEx(y)).ToArray<string>();
				string[] array2 = tokens2.Select((AbstractRegexToken y) => ReadablePythonTranslator.Token2PythonRegEx(y)).ToArray<string>();
				PartitionedCode partitionedCode = ReadablePythonTranslator.MultipleTokenMatchCode(array.Concat(array2).ToArray<string>(), k, x);
				string text = string.Concat(array);
				ssaregister = new SSARegister(null, PythonExpressionUtils.StrType, "left_regex");
				partitionedCode.LocalAddLine(new SSAStep(ssaregister, PythonExpressionUtils.MkPyLiteral(text), ""));
				SSARValue ssarvalue = PythonExpressionUtils.RegexMatch(new SSAValue[]
				{
					ssaregister,
					x,
					PythonExpressionUtils.NamedArg("pos", PythonExpressionUtils.GetItem(partitionedCode.Expr, PythonExpressionUtils.Zero))
				});
				partitionedCode.SetExpr(PythonExpressionUtils.Dot(new SSAValue[]
				{
					ssarvalue,
					PythonExpressionUtils.End
				}));
				return partitionedCode;
			}
			string text2 = ReadablePythonTranslator.TokenArray2PythonRegEx(tokens1);
			string text3 = ReadablePythonTranslator.TokenArray2PythonRegEx(tokens2);
			string text4 = "(?<=" + text2 + ")" + text3;
			string text5 = ReadablePythonTranslator.TokenArray2PythonVariableName(tokens1.Concat(tokens2));
			ssaregister = new SSARegister(null, PythonExpressionUtils.StrType, text5);
			return ReadablePythonTranslator.ToReadablePythonIndexRegEx(text4, k, x, ReadablePythonTranslator.PosType.Begin, ssaregister);
		}

		// Token: 0x0600FFC5 RID: 65477 RVA: 0x0036E960 File Offset: 0x0036CB60
		private static PartitionedCode ToReadablePythonIndexRegExPosE(SSAValue vName, int countK, AbstractRegexToken[] tokens, ReadablePythonTranslator.PosType pos)
		{
			if (tokens.Length == 1)
			{
				return ReadablePythonTranslator.ToReadablePythonIndexOneToken(tokens[0], countK, vName, pos);
			}
			if (!ReadablePythonTranslator.MayOverlap(tokens))
			{
				string text = ReadablePythonTranslator.TokenArray2PythonRegEx(tokens);
				string text2 = ReadablePythonTranslator.TokenArray2PythonVariableName(tokens);
				SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.StrType, text2);
				return ReadablePythonTranslator.ToReadablePythonIndexRegEx(text, countK, vName, pos, ssaregister);
			}
			return ReadablePythonTranslator.ToReadablePythonIndexMultipleRegExs(tokens.Select((AbstractRegexToken x) => ReadablePythonTranslator.Token2PythonRegEx(x)).ToArray<string>(), countK, vName, pos);
		}

		// Token: 0x0600FFC6 RID: 65478 RVA: 0x0036E9E0 File Offset: 0x0036CBE0
		private static PartitionedCode ToReadablePythonIndexOneToken(AbstractRegexToken t, int countK, SSAValue vName, ReadablePythonTranslator.PosType pos)
		{
			string text = ReadablePythonTranslator.Token2PythonRegEx(t);
			if (text == null)
			{
				return null;
			}
			string text2 = ReadablePythonTranslator.TokenName2PythonVariableName(t.Name + "_pattern");
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.StrType, text2);
			if (t.Name.Equals("Number") || t.Name.Equals("Digits"))
			{
				return ReadablePythonTranslator.ToReadablePythonIndexNumber(text, countK, vName, pos, ssaregister);
			}
			int num;
			Optional<string> optional = ReadablePythonTranslator.TokenAsAString(t, out num);
			if (optional.HasValue)
			{
				return ReadablePythonTranslator.ToReadablePythonIndexString(optional.Value, countK, vName, pos, ssaregister, num);
			}
			return ReadablePythonTranslator.ToReadablePythonIndexRegEx(text, countK, vName, pos, ssaregister);
		}

		// Token: 0x0600FFC7 RID: 65479 RVA: 0x0036EA7C File Offset: 0x0036CC7C
		private static PartitionedCode ToReadablePythonIndexNumber(string pattern, int countK, SSAValue vName, ReadablePythonTranslator.PosType pos, SSARegister patternName)
		{
			SSAValue ssavalue = ReadablePythonTranslator.PosType2MethodCall(pos);
			if (countK == 1)
			{
				string text = ((pos == ReadablePythonTranslator.PosType.Begin) ? "digit_match" : "number_match");
				SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.ObjType, text);
				SSARValue ssarvalue = PythonExpressionUtils.RegexSearch(PythonExpressionUtils.MkPyLiteral((pos == ReadablePythonTranslator.PosType.Begin) ? "\\d" : pattern), vName);
				SSAStep ssastep = new SSAStep(ssaregister, ssarvalue, "");
				return new PartitionedCode(PythonExpressionUtils.Dot(new SSAValue[] { ssaregister, ssavalue }), new SSAStep[] { ssastep }, new string[] { "regex" }, new SSAValue[] { vName });
			}
			return ReadablePythonTranslator.ToReadablePythonIndexRegEx(pattern, countK, vName, pos, patternName);
		}

		// Token: 0x0600FFC8 RID: 65480 RVA: 0x0036EB20 File Offset: 0x0036CD20
		private static PartitionedCode ToReadablePythonIndexString(string substr, int countK, SSAValue vName, ReadablePythonTranslator.PosType pos, SSARegister patternVarName, int baseSubstrLength)
		{
			SSALiteral ssaliteral = PythonExpressionUtils.MkLiteral(substr);
			if (pos == ReadablePythonTranslator.PosType.Whole)
			{
				return new PartitionedCode(ssaliteral, null, null, null);
			}
			SSAStep ssastep = new SSAStep(patternVarName, ssaliteral, "");
			SSAValue ssavalue;
			if (countK == 1 || countK == -1)
			{
				ssavalue = ((countK == 1) ? PythonExpressionUtils.Index(vName, patternVarName) : PythonExpressionUtils.RIndex(vName, patternVarName));
				if (pos == ReadablePythonTranslator.PosType.End)
				{
					ssavalue = PythonExpressionUtils.Add(new SSAValue[]
					{
						ssavalue,
						PythonExpressionUtils.MkLiteral(baseSubstrLength)
					});
				}
				return new PartitionedCode(ssavalue, new SSAStep[] { ssastep }, null, new SSAValue[] { vName });
			}
			SSALiteral ssaliteral2 = PythonExpressionUtils.MkLiteral(Math.Abs(countK));
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.ListType, "other_parts");
			SSARValue ssarvalue = ((countK > 1) ? PythonExpressionUtils.Split(vName, patternVarName, ssaliteral2) : PythonExpressionUtils.RSplit(vName, patternVarName, ssaliteral2));
			SSARValue ssarvalue2 = ((countK > 0) ? PythonExpressionUtils.SubString(ssarvalue, 0, -1) : PythonExpressionUtils.SubString(ssarvalue, 1, 0));
			SSAStep ssastep2 = new SSAStep(ssaregister, ssarvalue2, "");
			string text = ((countK > 1) ? "len_left_substr" : "len_right_substr");
			SSARegister ssaregister2 = new SSARegister(null, PythonExpressionUtils.NumType, text);
			SSARValue ssarvalue3 = PythonExpressionUtils.Len(PythonExpressionUtils.Join(patternVarName, ssaregister));
			SSAStep ssastep3 = new SSAStep(ssaregister2, ssarvalue3, "");
			if ((countK > 0 && pos == ReadablePythonTranslator.PosType.Begin) || (countK < 0 && pos == ReadablePythonTranslator.PosType.End))
			{
				ssavalue = ssaregister2;
			}
			else
			{
				ssavalue = PythonExpressionUtils.Add(new SSAValue[]
				{
					ssaregister2,
					PythonExpressionUtils.MkLiteral(baseSubstrLength)
				});
			}
			ssavalue = ((countK > 0) ? ssavalue : PythonExpressionUtils.Times(ssavalue, -1m));
			return new PartitionedCode(ssavalue, new SSAStep[] { ssastep, ssastep2, ssastep3 }, null, new SSAValue[] { vName });
		}

		// Token: 0x0600FFC9 RID: 65481 RVA: 0x0036ECC4 File Offset: 0x0036CEC4
		private static PartitionedCode ToReadablePythonIndexStringWrap(string substr, int countK, SSAValue vName, ReadablePythonTranslator.PosType pos, string substrName, string indexName)
		{
			string text = ReadablePythonTranslator.TokenName2PythonVariableName(substrName);
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.StrType, text);
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonIndexString(substr.ToPythonLiteral(), countK, vName, pos, ssaregister, substr.Length);
			SSARegister ssaregister2 = new SSARegister(null, PythonExpressionUtils.NumType, indexName);
			ssaregister2 = partitionedCode.IntroduceNewVarIf(ssaregister2);
			return partitionedCode;
		}

		// Token: 0x0600FFCA RID: 65482 RVA: 0x0036ED14 File Offset: 0x0036CF14
		private static PartitionedCode ToReadablePythonIndexRegEx(string regStr, int countK, SSAValue vName, ReadablePythonTranslator.PosType pos, SSARegister patternName)
		{
			SSAStep ssastep = new SSAStep(patternName, PythonExpressionUtils.MkPyLiteral(regStr), "");
			SSAValue ssavalue = ReadablePythonTranslator.PosType2MethodCall(pos);
			if (countK == 1)
			{
				SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.ObjType, "pattern_match");
				SSARValue ssarvalue = PythonExpressionUtils.RegexSearch(patternName, vName);
				SSAStep ssastep2 = new SSAStep(ssaregister, ssarvalue, "");
				return new PartitionedCode(PythonExpressionUtils.Dot(new SSAValue[] { ssaregister, ssavalue }), new SSAStep[] { ssastep, ssastep2 }, new string[] { "regex" }, null);
			}
			SSARegister ssaregister2 = new SSARegister(null, PythonExpressionUtils.ListType, "all_matches");
			SSARValue ssarvalue2 = PythonExpressionUtils.RegexFindIter(patternName, vName);
			SSAStep ssastep3 = new SSAStep(ssaregister2, ssarvalue2, "");
			SSARegister ssaregister3 = new SSARegister(null, PythonExpressionUtils.ObjType, "kth_match");
			SSAStep ssastep4;
			if (countK > 1)
			{
				SSARegister ssaregister4 = new SSARegister(null, PythonExpressionUtils.ObjType, "m");
				SSARegister ssaregister5 = new SSARegister(null, PythonExpressionUtils.NumType, "m_index");
				SSARValue ssarvalue3 = PythonExpressionUtils.Tuple(ssaregister5, ssaregister4);
				SSARValue ssarvalue4 = PythonExpressionUtils.Enumerate(ssaregister2);
				SSARValue ssarvalue5 = PythonExpressionUtils.Equals(ssaregister5, PythonExpressionUtils.MkLiteral(countK - 1));
				SSARValue ssarvalue6 = PythonExpressionUtils.Next(PythonExpressionUtils.ForInIf(ssaregister4, ssarvalue3, ssarvalue4, ssarvalue5));
				ssastep4 = new SSAStep(ssaregister3, ssarvalue6, "");
			}
			else
			{
				SSARValue item = PythonExpressionUtils.GetItem(PythonExpressionUtils.List(ssaregister2), PythonExpressionUtils.MkLiteral(countK));
				ssastep4 = new SSAStep(ssaregister3, item, "");
			}
			return new PartitionedCode(PythonExpressionUtils.Dot(new SSAValue[] { ssaregister3, ssavalue }), new SSAStep[] { ssastep, ssastep3, ssastep4 }, new string[] { "regex" }, new SSAValue[] { vName });
		}

		// Token: 0x0600FFCB RID: 65483 RVA: 0x0036EEC0 File Offset: 0x0036D0C0
		private static PartitionedCode MultipleTokenMatchCode(string[] regexes, int count, SSAValue vName)
		{
			SSAValue ssavalue = PythonExpressionUtils.MkLiteral(count);
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.ListType, "regexes");
			SSARValue ssarvalue = PythonExpressionUtils.MakeList(regexes);
			SSAStep ssastep = new SSAStep(ssaregister, ssarvalue, "");
			SSARegister ssaregister2 = new SSARegister(null, PythonExpressionUtils.ObjType, "kth_match");
			string text = "kth_match_multiple_tokens";
			SSARValue ssarvalue2 = PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ObjType, text, new SSAValue[] { vName, ssaregister, ssavalue });
			SSAStep ssastep2 = new SSAStep(ssaregister2, ssarvalue2, "");
			PartitionedCode partitionedCode = new PartitionedCode(ssaregister2, new SSAStep[] { ssastep, ssastep2 }, new string[] { "regex" }, new SSAValue[] { vName });
			string text2 = text;
			Func<IGeneratedFunction> func;
			if ((func = ReadablePythonTranslator.<>O.<12>__DeclareKthMatchMultipleTokens) == null)
			{
				func = (ReadablePythonTranslator.<>O.<12>__DeclareKthMatchMultipleTokens = new Func<IGeneratedFunction>(ReadablePythonTranslator.DeclareKthMatchMultipleTokens));
			}
			partitionedCode.AddContextIfNew(text2, func, 0);
			return partitionedCode;
		}

		// Token: 0x0600FFCC RID: 65484 RVA: 0x0036EF9C File Offset: 0x0036D19C
		private static PartitionedCode ToReadablePythonIndexMultipleRegExs(string[] regexes, int count, SSAValue vName, ReadablePythonTranslator.PosType pos)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslator.MultipleTokenMatchCode(regexes, count, vName);
			SSAValue ssavalue;
			switch (pos)
			{
			case ReadablePythonTranslator.PosType.Begin:
				ssavalue = PythonExpressionUtils.GetItem(partitionedCode.Expr, PythonExpressionUtils.Zero);
				break;
			case ReadablePythonTranslator.PosType.End:
				ssavalue = PythonExpressionUtils.GetItem(partitionedCode.Expr, PythonExpressionUtils.Minus1);
				break;
			case ReadablePythonTranslator.PosType.Whole:
				ssavalue = PythonExpressionUtils.Slice(vName, PythonExpressionUtils.GetItem(partitionedCode.Expr, PythonExpressionUtils.Zero), PythonExpressionUtils.GetItem(partitionedCode.Expr, PythonExpressionUtils.Minus1));
				break;
			default:
				throw new ArgumentException(string.Format("Unexpected position type found: {0}", pos));
			}
			partitionedCode.SetExpr(ssavalue);
			return partitionedCode;
		}

		// Token: 0x0600FFCD RID: 65485 RVA: 0x0036F033 File Offset: 0x0036D233
		private static bool IsSeparatorOrZeroLength(Token t, out string separator)
		{
			return RegexRemover.IsSeparatorWithWs(t, out separator) || RegexRemover.IsZeroLengthToken(t);
		}

		// Token: 0x0600FFCE RID: 65486 RVA: 0x0036F048 File Offset: 0x0036D248
		private static PartitionedCode ToReadablePythonREER(SubStr p)
		{
			pos pos = pos.CreateHole(Language.Build, "1");
			ProgramNode node = ReadablePythonTranslator.Rule.SubStr(ReadablePythonTranslator.Hole.x, ReadablePythonTranslator.Rule.PosPair(ReadablePythonTranslator.Hole.pos, pos)).Node;
			IReadOnlyDictionary<Hole, ProgramNode> readOnlyDictionary = ProgramSetRewriter.ExtractMappings(p.Node, node);
			if (readOnlyDictionary == null)
			{
				return null;
			}
			ProgramNode programNode = readOnlyDictionary[(Hole)ReadablePythonTranslator.Hole.pos.Node];
			ProgramNode programNode2 = readOnlyDictionary[(Hole)pos.Node];
			SSAValue ssavalue;
			int num;
			AbstractRegexToken[] array;
			if (!ReadablePythonTranslator.IndexPatternREMatch(programNode, out ssavalue, out num, out array))
			{
				if (ReadablePythonTranslator.IndexPatternERMatch(programNode, out ssavalue, out num, out array))
				{
					if (array.All((AbstractRegexToken tok) => RegexRemover.IsZeroLengthToken(tok)))
					{
						goto IL_00CF;
					}
				}
				return null;
			}
			IL_00CF:
			SSAValue ssavalue2;
			int num2;
			AbstractRegexToken[] array2;
			if (!ReadablePythonTranslator.IndexPatternERMatch(programNode2, out ssavalue2, out num2, out array2))
			{
				if (ReadablePythonTranslator.IndexPatternREMatch(programNode2, out ssavalue2, out num2, out array2))
				{
					array2.All((AbstractRegexToken tok) => RegexRemover.IsZeroLengthToken(tok));
				}
				return null;
			}
			if (array.Length != 1 || array2.Length != 1)
			{
				return null;
			}
			string text;
			if (!ReadablePythonTranslator.IsSeparatorOrZeroLength(array[0], out text))
			{
				return null;
			}
			string text2;
			if (!ReadablePythonTranslator.IsSeparatorOrZeroLength(array2[0], out text2))
			{
				return null;
			}
			if (text != null && text.Equals(text2))
			{
				return ReadablePythonTranslator.GenerateSplitProgram(ssavalue, text, num, num2);
			}
			PartitionedCode partitionedCode = null;
			PartitionedCode partitionedCode2 = null;
			if (text != null)
			{
				partitionedCode = ReadablePythonTranslator.ToReadablePythonIndexStringWrap(text, num, ssavalue, ReadablePythonTranslator.PosType.End, array[0].Name, "index1");
			}
			if (text2 != null)
			{
				partitionedCode2 = ReadablePythonTranslator.ToReadablePythonIndexStringWrap(text2, num2, ssavalue2, ReadablePythonTranslator.PosType.Begin, array2[0].Name, "index2");
			}
			SSARValue ssarvalue;
			if (array[0].Name.Equals("Begin") && partitionedCode2 != null)
			{
				ssarvalue = PythonExpressionUtils.SliceEndOnly(ssavalue, partitionedCode2.Expr);
				partitionedCode = partitionedCode2;
			}
			else if (array2[0].Name.Equals("End") && partitionedCode != null)
			{
				ssarvalue = PythonExpressionUtils.RSubStr(ssavalue, partitionedCode.Expr);
			}
			else
			{
				if (partitionedCode == null || partitionedCode2 == null)
				{
					return null;
				}
				partitionedCode.Merge(partitionedCode2);
				ssarvalue = PythonExpressionUtils.Slice(ssavalue, partitionedCode.Expr, partitionedCode2.Expr);
			}
			ssarvalue = PythonExpressionUtils.Strip(ssarvalue, null);
			partitionedCode.SetExpr(ssarvalue);
			return partitionedCode;
		}

		// Token: 0x0600FFCF RID: 65487 RVA: 0x0036F298 File Offset: 0x0036D498
		private static PartitionedCode GenerateSplitProgram(SSAValue e, string separator, int k1, int k2)
		{
			SSARValue ssarvalue = PythonExpressionUtils.MkPyLiteral(separator);
			SSARValue ssarvalue3;
			if (k1 > 0 && k2 > 0)
			{
				SSARValue ssarvalue2 = PythonExpressionUtils.Split(e, ssarvalue, PythonExpressionUtils.MkLiteral(k2));
				if (k2 == k1 + 1)
				{
					ssarvalue3 = PythonExpressionUtils.GetItem(ssarvalue2, PythonExpressionUtils.MkLiteral(-2m));
				}
				else
				{
					ssarvalue3 = PythonExpressionUtils.Join(ssarvalue, PythonExpressionUtils.Slice(ssarvalue2, PythonExpressionUtils.MkLiteral(k1), PythonExpressionUtils.Minus1));
				}
			}
			else if (k1 < 0 && k2 < 0)
			{
				SSARValue ssarvalue4 = PythonExpressionUtils.RSplit(e, ssarvalue, PythonExpressionUtils.MkLiteral(-k1));
				if (k2 == k1 + 1)
				{
					ssarvalue3 = PythonExpressionUtils.GetItem(ssarvalue4, PythonExpressionUtils.MkLiteral(1U));
				}
				else
				{
					ssarvalue3 = PythonExpressionUtils.Join(ssarvalue, PythonExpressionUtils.Slice(ssarvalue4, PythonExpressionUtils.MkLiteral(1U), PythonExpressionUtils.MkLiteral(k2)));
				}
			}
			else
			{
				SSARValue ssarvalue5 = PythonExpressionUtils.Split(e, ssarvalue);
				ssarvalue3 = PythonExpressionUtils.Join(ssarvalue, PythonExpressionUtils.Slice(ssarvalue5, PythonExpressionUtils.MkLiteral(k1), PythonExpressionUtils.MkLiteral(k2)));
			}
			ssarvalue3 = PythonExpressionUtils.Strip(ssarvalue3, null);
			return new PartitionedCode(ssarvalue3, null, null, null);
		}

		// Token: 0x0600FFD0 RID: 65488 RVA: 0x0036F398 File Offset: 0x0036D598
		private static Optional<string> TokenAsAString(AbstractRegexToken t, out int strLen)
		{
			StringToken stringToken = t as StringToken;
			if (stringToken != null)
			{
				string matchedString = stringToken.MatchedString;
				strLen = matchedString.Length;
				return matchedString.ToPythonLiteral().Some<string>();
			}
			strLen = 0;
			return default(Optional<string>);
		}

		// Token: 0x0600FFD1 RID: 65489 RVA: 0x0036F3D6 File Offset: 0x0036D5D6
		private static string Token2PythonRegEx(AbstractRegexToken t)
		{
			return PythonRegexUtils.ConvertToPythonRegEx(t.Regex);
		}

		// Token: 0x0600FFD2 RID: 65490 RVA: 0x0036F3E3 File Offset: 0x0036D5E3
		internal static string TokenArray2PythonRegEx(AbstractRegexToken[] tokens)
		{
			Func<AbstractRegexToken, string> func;
			if ((func = ReadablePythonTranslator.<>O.<13>__Token2PythonRegEx) == null)
			{
				func = (ReadablePythonTranslator.<>O.<13>__Token2PythonRegEx = new Func<AbstractRegexToken, string>(ReadablePythonTranslator.Token2PythonRegEx));
			}
			return string.Concat(tokens.Select(func));
		}

		// Token: 0x0600FFD3 RID: 65491 RVA: 0x0036F40C File Offset: 0x0036D60C
		private static string TokenName2PythonVariableName(string tokenName)
		{
			tokenName = ((tokenName.Length < 2) ? ("token_" + tokenName) : tokenName);
			string text;
			if (PythonNameUtils.IsValidIdentifier(tokenName, out text))
			{
				return PythonNameUtils.ConvertStringToSnakeCase(text);
			}
			return PythonNameUtils.NearestValidIdentifier(tokenName);
		}

		// Token: 0x0600FFD4 RID: 65492 RVA: 0x0036F449 File Offset: 0x0036D649
		internal static string TokenArray2PythonVariableName(IEnumerable<AbstractRegexToken> tokens)
		{
			return string.Join("_", tokens.Select((AbstractRegexToken t) => ReadablePythonTranslator.TokenName2PythonVariableName(t.Name)));
		}

		// Token: 0x0600FFD6 RID: 65494 RVA: 0x0036F4EA File Offset: 0x0036D6EA
		[CompilerGenerated]
		internal static f <ProgramContainsNotHandledPatterns>g__GetOneColumnPattern|11_0(idx indexHole, conv convHole)
		{
			return ReadablePythonTranslator.Rule.LetColumnName(indexHole, ReadablePythonTranslator.Rule.LetX(ReadablePythonTranslator.Rule.ChooseInput(ReadablePythonTranslator.Var.vs, ReadablePythonTranslator.Var.columnName), convHole));
		}

		// Token: 0x04006006 RID: 24582
		private static Translator<PythonHeaderModule, PythonModule, Program, IRow, object> _caller;

		// Token: 0x04006007 RID: 24583
		private static readonly GrammarBuilders.Nodes.NodeRules Rule = Language.Build.Node.Rule;

		// Token: 0x04006008 RID: 24584
		private static readonly GrammarBuilders.Nodes.NodeVariables Var = Language.Build.Node.Variable;

		// Token: 0x04006009 RID: 24585
		private static readonly GrammarBuilders.Nodes.NodeHoles Hole = Language.Build.Node.Hole;

		// Token: 0x0400600A RID: 24586
		private static readonly Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r EpsilonRegexNode = ReadablePythonTranslator.Rule.r(new RegularExpression(0));

		// Token: 0x0400600B RID: 24587
		private static OptimizeFor optimizeFor = OptimizeFor.Readability;

		// Token: 0x0400600C RID: 24588
		private static readonly List<SSAValue> VariablesUsed = new List<SSAValue>();

		// Token: 0x02001DC9 RID: 7625
		internal enum PosType
		{
			// Token: 0x0400600E RID: 24590
			Begin,
			// Token: 0x0400600F RID: 24591
			End,
			// Token: 0x04006010 RID: 24592
			Whole
		}

		// Token: 0x02001DCA RID: 7626
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006011 RID: 24593
			public static Func<SSAValue, SSARValue> <0>__Lower;

			// Token: 0x04006012 RID: 24594
			public static Func<SSAValue, SSARValue> <1>__Upper;

			// Token: 0x04006013 RID: 24595
			public static Func<SSAValue, SSARValue> <2>__Title;

			// Token: 0x04006014 RID: 24596
			public static Func<PosPair, Optional<Record<PartitionedCode, PartitionedCode>>> <3>__TranslatePosPair;

			// Token: 0x04006015 RID: 24597
			public static Func<LetPL1, Optional<Record<PartitionedCode, PartitionedCode>>> <4>__TranslateLetPL1;

			// Token: 0x04006016 RID: 24598
			public static Func<RegexPositionPair, Optional<Record<PartitionedCode, PartitionedCode>>> <5>__TranslateRegexPosPair;

			// Token: 0x04006017 RID: 24599
			public static Func<ExternalExtractorPositionPair, Optional<Record<PartitionedCode, PartitionedCode>>> <6>__TranslateExternalExtractorPosPair;

			// Token: 0x04006018 RID: 24600
			public static Func<RelativePosition, PartitionedCode> <7>__TranslateRelativePosition;

			// Token: 0x04006019 RID: 24601
			public static Func<RegexPositionRelative, PartitionedCode> <8>__TranslateRegexPositionRelative;

			// Token: 0x0400601A RID: 24602
			public static Func<AbsolutePosition, PartitionedCode> <9>__TranslateAbsolutePosition;

			// Token: 0x0400601B RID: 24603
			public static Func<RegexPosition, PartitionedCode> <10>__TranslateRegexPosition;

			// Token: 0x0400601C RID: 24604
			public static Func<AbstractRegexToken, AbstractRegexToken, bool> <11>__MayOverlap;

			// Token: 0x0400601D RID: 24605
			public static Func<IGeneratedFunction> <12>__DeclareKthMatchMultipleTokens;

			// Token: 0x0400601E RID: 24606
			public static Func<AbstractRegexToken, string> <13>__Token2PythonRegEx;
		}
	}
}
