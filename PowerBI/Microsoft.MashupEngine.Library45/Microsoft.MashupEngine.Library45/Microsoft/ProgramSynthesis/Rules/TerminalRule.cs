using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Rules
{
	// Token: 0x020003A3 RID: 931
	[DataContract]
	public class TerminalRule : GrammarRule, IDirectSetLanguage, ILanguage
	{
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x0003D3C6 File Offset: 0x0003B5C6
		// (set) Token: 0x060014F5 RID: 5365 RVA: 0x0003D3CE File Offset: 0x0003B5CE
		[DataMember]
		public bool IsInput { get; private set; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0003D3D7 File Offset: 0x0003B5D7
		// (set) Token: 0x060014F7 RID: 5367 RVA: 0x0003D3DF File Offset: 0x0003B5DF
		[DataMember]
		internal string GeneratorReference { get; private set; }

		// Token: 0x060014F8 RID: 5368 RVA: 0x0003D3E8 File Offset: 0x0003B5E8
		public TerminalRule(Symbol head, bool isInput, string generatorReference = null)
			: base(head, Array.Empty<Symbol>())
		{
			this.IsInput = isInput;
			this.GeneratorReference = generatorReference;
			base.Id = head.Name;
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0003D410 File Offset: 0x0003B610
		public override TResult Accept<TResult, TArgs>(GrammarRuleVisitor<TResult, TArgs> visitor, TArgs args)
		{
			return visitor.VisitTerminalRule(this, args);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0003D41A File Offset: 0x0003B61A
		public override ProgramNode BuildASTNode(object data, params ProgramNode[] children)
		{
			return this.BuildASTNode(data);
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0003D424 File Offset: 0x0003B624
		public override ProgramNode BuildASTNode(object data)
		{
			if (base.Head.IsVariable)
			{
				return new VariableNode(base.Head);
			}
			if ((data != null && !base.ReturnResolvedType.IsInstanceOfType(data)) || (data == null && base.ReturnResolvedType.GetTypeInfo().IsValueType && !base.ReturnResolvedType.IsNullable()))
			{
				return null;
			}
			return new LiteralNode(base.Head, data);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00002188 File Offset: 0x00000388
		internal override FeatureCalculator BuildDefaultFeatureCalculator(FeatureInfo feature)
		{
			return null;
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0003D48B File Offset: 0x0003B68B
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} := {2}{1}", base.Head, base.Head.GrammarType, this.IsInput ? "@input " : "");
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x0003D4C1 File Offset: 0x0003B6C1
		public override IEnumerable<ProgramNode> AllElements
		{
			get
			{
				return (from ps in base.Head.TryGetAllPrograms(true, true)
					select ps.RealizedPrograms).OrElse(Enumerable.Empty<ProgramNode>());
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x0003D4FE File Offset: 0x0003B6FE
		public bool IsVariable
		{
			get
			{
				return base.Head.IsVariable;
			}
		}
	}
}
