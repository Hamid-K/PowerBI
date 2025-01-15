using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C92 RID: 7314
	public class Substring : TransformationDescription
	{
		// Token: 0x0600F77B RID: 63355 RVA: 0x0034BC9C File Offset: 0x00349E9C
		internal Substring(SubStr programNode, string columnName)
			: base(programNode.Node, columnName, TransformationCategory.Substring, TransformationKind.Substring)
		{
			GrammarBuilders grammarBuilders = GrammarBuilders.Instance(base.ProgramNode.Grammar);
			PosPair posPair;
			if (programNode.PP.Is_PosPair(grammarBuilders, out posPair))
			{
				this.LeftPos = new Substring.Position(grammarBuilders, posPair.pos1);
				this.RightPos = new Substring.Position(grammarBuilders, posPair.pos2);
			}
		}

		// Token: 0x17002951 RID: 10577
		// (get) Token: 0x0600F77C RID: 63356 RVA: 0x0034BD04 File Offset: 0x00349F04
		protected override object ExtraIdentity
		{
			get
			{
				return base.ColumnName;
			}
		}

		// Token: 0x17002952 RID: 10578
		// (get) Token: 0x0600F77D RID: 63357 RVA: 0x0034BD0C File Offset: 0x00349F0C
		internal Substring.Position LeftPos { get; }

		// Token: 0x17002953 RID: 10579
		// (get) Token: 0x0600F77E RID: 63358 RVA: 0x0034BD14 File Offset: 0x00349F14
		internal Substring.Position RightPos { get; }

		// Token: 0x02001C93 RID: 7315
		internal struct RegexPair
		{
			// Token: 0x0600F77F RID: 63359 RVA: 0x0034BD1C File Offset: 0x00349F1C
			internal RegexPair(int matchNum, regexPair regexPair)
			{
				this.MatchNum = matchNum;
				this.BeforeMatch = regexPair.Cast_RegexPair().r1.Value;
				this.AfterMatch = regexPair.Cast_RegexPair().r2.Value;
			}

			// Token: 0x17002954 RID: 10580
			// (get) Token: 0x0600F780 RID: 63360 RVA: 0x0034BD6A File Offset: 0x00349F6A
			// (set) Token: 0x0600F781 RID: 63361 RVA: 0x0034BD72 File Offset: 0x00349F72
			public RegularExpression BeforeMatch { readonly get; set; }

			// Token: 0x17002955 RID: 10581
			// (get) Token: 0x0600F782 RID: 63362 RVA: 0x0034BD7B File Offset: 0x00349F7B
			// (set) Token: 0x0600F783 RID: 63363 RVA: 0x0034BD83 File Offset: 0x00349F83
			public RegularExpression AfterMatch { readonly get; set; }

			// Token: 0x17002956 RID: 10582
			// (get) Token: 0x0600F784 RID: 63364 RVA: 0x0034BD8C File Offset: 0x00349F8C
			// (set) Token: 0x0600F785 RID: 63365 RVA: 0x0034BD94 File Offset: 0x00349F94
			public int MatchNum { readonly get; set; }
		}

		// Token: 0x02001C94 RID: 7316
		internal struct Position
		{
			// Token: 0x0600F786 RID: 63366 RVA: 0x0034BDA0 File Offset: 0x00349FA0
			public Position(GrammarBuilders build, pos pos)
			{
				Substring.Position.<>c__DisplayClass0_0 CS$<>8__locals1 = new Substring.Position.<>c__DisplayClass0_0();
				Substring.Position.<>c__DisplayClass0_0 CS$<>8__locals2 = CS$<>8__locals1;
				Func<int?, Substring.RegexPair?, Record<int?, Substring.RegexPair?>> func;
				if ((func = Substring.Position.<>O.<0>__Create) == null)
				{
					func = (Substring.Position.<>O.<0>__Create = new Func<int?, Substring.RegexPair?, Record<int?, Substring.RegexPair?>>(Record.Create<int?, Substring.RegexPair?>));
				}
				CS$<>8__locals2.create = func;
				int? num;
				Substring.RegexPair? regexPair;
				pos.Switch<Record<int?, Substring.RegexPair?>>(build, (RelativePosition relativePosition) => CS$<>8__locals1.create(new int?(relativePosition.k.Value), null), (RegexPositionRelative regexPositionRelative) => CS$<>8__locals1.create(null, new Substring.RegexPair?(new Substring.RegexPair(regexPositionRelative.k.Value, regexPositionRelative.regexPair))), (AbsolutePosition absolutePositionNode) => CS$<>8__locals1.create(new int?(absolutePositionNode.k.Value), null), (RegexPosition regexPosition) => CS$<>8__locals1.create(null, new Substring.RegexPair?(new Substring.RegexPair(regexPosition.k.Value, regexPosition.regexPair)))).Deconstruct(out num, out regexPair);
				int? num2 = num;
				Substring.RegexPair? regexPair2 = regexPair;
				this.AbsolutePosition = num2;
				this.RegexPair = regexPair2;
			}

			// Token: 0x17002957 RID: 10583
			// (get) Token: 0x0600F787 RID: 63367 RVA: 0x0034BE28 File Offset: 0x0034A028
			// (set) Token: 0x0600F788 RID: 63368 RVA: 0x0034BE30 File Offset: 0x0034A030
			public Substring.RegexPair? RegexPair { readonly get; set; }

			// Token: 0x17002958 RID: 10584
			// (get) Token: 0x0600F789 RID: 63369 RVA: 0x0034BE39 File Offset: 0x0034A039
			// (set) Token: 0x0600F78A RID: 63370 RVA: 0x0034BE41 File Offset: 0x0034A041
			public int? AbsolutePosition { readonly get; set; }

			// Token: 0x02001C95 RID: 7317
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x04005BAA RID: 23466
				public static Func<int?, Substring.RegexPair?, Record<int?, Substring.RegexPair?>> <0>__Create;
			}
		}
	}
}
