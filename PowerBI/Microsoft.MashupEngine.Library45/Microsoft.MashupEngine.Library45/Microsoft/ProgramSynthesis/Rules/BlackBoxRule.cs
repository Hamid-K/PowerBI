using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Diagnostics;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Rules
{
	// Token: 0x0200037D RID: 893
	[DataContract]
	public class BlackBoxRule : OperatorRule
	{
		// Token: 0x060013F1 RID: 5105 RVA: 0x0003A738 File Offset: 0x00038938
		public BlackBoxRule(string id, Symbol head, params Symbol[] body)
			: this(id, head, body.AsEnumerable<Symbol>())
		{
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0003A748 File Offset: 0x00038948
		public BlackBoxRule(string id, Symbol head, IEnumerable<Symbol> body)
			: base(id, head, body)
		{
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0003A753 File Offset: 0x00038953
		public override TResult Accept<TResult, TArgs>(GrammarRuleVisitor<TResult, TArgs> visitor, TArgs args)
		{
			return visitor.VisitBlackboxRule(this, args);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0003A760 File Offset: 0x00038960
		protected override MethodReference<OperatorRule.OperatorSemantics> InitializeSemantics()
		{
			Type[] bodyTypes = base.Body.Select((Symbol s) => s.ResolvedType).ToArray<Type>();
			MethodInfo methodInfo = base.Grammar.SemanticsLocations.Collect((GrammarType l) => l.GetMethod(this.Name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, bodyTypes)).SingleOrDefault<MethodInfo>();
			if (methodInfo == null)
			{
				return null;
			}
			this.Lazy = methodInfo.GetCustomAttribute<LazySemanticsAttribute>() != null;
			return MethodReference.CreateWithParams<OperatorRule.OperatorSemantics>(methodInfo, false);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0003A7F4 File Offset: 0x000389F4
		private void OnMissingSemantics(DiagnosticsContext diagnosticsContext, Type[] bodyTypes)
		{
			string text = string.Join(", ", bodyTypes.Select((Type t) => t.CsName(true)));
			diagnosticsContext.AddDiagnostic(new Diagnostic.Semantics_NoSemantics(base.OriginLocation, new object[]
			{
				base.Name,
				base.ReturnGrammarType.CsName(),
				text
			}));
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0003A864 File Offset: 0x00038A64
		internal override void ValidateSemantics(DiagnosticsContext diagnosticsContext)
		{
			Type[] bodyTypes = base.Body.Select((Symbol s) => s.ResolvedType).ToArray<Type>();
			MethodInfo[] array = base.Grammar.SemanticsLocations.Collect((GrammarType l) => l.GetMethod(this.Name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, bodyTypes)).ToArray<MethodInfo>();
			if (array.Length == 0)
			{
				this.OnMissingSemantics(diagnosticsContext, bodyTypes);
				return;
			}
			if (array.Length > 1)
			{
				diagnosticsContext.AddDiagnostic(new Diagnostic.Semantics_AmbiguousSemantics(base.OriginLocation, new object[]
				{
					base.Name,
					array[0].FullName(),
					array[1].FullName()
				}));
				return;
			}
			if (!array[0].ReturnType.IsConvertibleTo(base.ReturnResolvedType))
			{
				Location.Assembly assembly = new Location.Assembly(array[0]);
				diagnosticsContext.AddDiagnostic(new Diagnostic.Semantics_IncompatibleSemanticsReturnType(assembly, new object[]
				{
					base.Name,
					array[0].ReturnType.CsName(true),
					base.ReturnResolvedType.CsName(true)
				}));
			}
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0003A97D File Offset: 0x00038B7D
		internal override IEnumerable<ProgramNode> GetTopKStream(JoinProgramSet programSet, IFeature feature, int k = 1, FeatureCalculationContext fcc = null, LogListener logListener = null)
		{
			return NonterminalRule.GenericTopKStream(programSet, feature, k, fcc, logListener);
		}
	}
}
