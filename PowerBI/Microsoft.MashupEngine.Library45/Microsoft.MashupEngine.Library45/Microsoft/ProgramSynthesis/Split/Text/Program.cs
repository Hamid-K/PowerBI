using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Split.Text.Learning;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x020012F4 RID: 4852
	[DebuggerDisplay("{ProgramNode}")]
	public class Program : IEquatable<Program>
	{
		// Token: 0x0600923F RID: 37439 RVA: 0x001EC4F9 File Offset: 0x001EA6F9
		private Program(ProgramNode node)
		{
			this.ProgramNode = node;
		}

		// Token: 0x17001923 RID: 6435
		// (get) Token: 0x06009240 RID: 37440 RVA: 0x001EC508 File Offset: 0x001EA708
		public ProgramNode ProgramNode { get; }

		// Token: 0x17001924 RID: 6436
		// (get) Token: 0x06009241 RID: 37441 RVA: 0x001EC510 File Offset: 0x001EA710
		public static IFeature ScoreFeature
		{
			get
			{
				return SplitProgramLearner.Instance.ScoreFeature;
			}
		}

		// Token: 0x06009242 RID: 37442 RVA: 0x001EC51C File Offset: 0x001EA71C
		public bool Equals(Program other)
		{
			return other != null && (this == other || object.Equals(this.ProgramNode, other.ProgramNode));
		}

		// Token: 0x06009243 RID: 37443 RVA: 0x001EC53A File Offset: 0x001EA73A
		public static Program Learn(IEnumerable<string> examples, CancellationToken cancel = default(CancellationToken))
		{
			return Program.Learn(examples.Select((string s) => new StringRegion(s, Semantics.Tokens)), cancel);
		}

		// Token: 0x06009244 RID: 37444 RVA: 0x001EC568 File Offset: 0x001EA768
		public static Program Learn(IEnumerable<StringRegion> examples, CancellationToken cancel = default(CancellationToken))
		{
			Grammar grammar = Language.Grammar;
			WithInputTopSpec withInputTopSpec = new WithInputTopSpec(examples.Select((StringRegion e) => State.CreateForLearning(grammar.InputSymbol, e)));
			Grammar grammar2 = grammar;
			SynthesisEngine.Config config = new SynthesisEngine.Config();
			SynthesisEngine.Config config2 = config;
			DeductiveSynthesis[] array = new DeductiveSynthesis[1];
			int num = 0;
			DomainLearningLogic domainLearningLogic = new Witnesses(Language.Grammar, null);
			DeductiveSynthesis.Config config3 = new DeductiveSynthesis.Config();
			config3.PrereqTopProgramsThreshold = (int k) => new int?(1);
			array[num] = new DeductiveSynthesis(domainLearningLogic, config3);
			ISynthesisStrategy[] array2 = array;
			config2.Strategies = array2;
			config.UseThreads = false;
			SynthesisEngine synthesisEngine = new SynthesisEngine(grammar2, config, null);
			IEnumerable<ProgramNode> realizedPrograms = synthesisEngine.LearnSymbolTopK(grammar.StartSymbol, withInputTopSpec, Program.ScoreFeature, 10, null, ProgramSamplingStrategy.UniformRandom, cancel, null).RealizedPrograms;
			LogListener logListener = synthesisEngine.Configuration.LogListener;
			if (logListener != null)
			{
				logListener.SaveLogToXML("log.xml");
			}
			ProgramNode programNode = realizedPrograms.FirstOrDefault<ProgramNode>();
			if (!(programNode == null))
			{
				return new Program(programNode);
			}
			return null;
		}

		// Token: 0x06009245 RID: 37445 RVA: 0x001EC670 File Offset: 0x001EA870
		public IList<StringRegion> Run(StringRegion input)
		{
			return this.ProgramNode.Invoke(State.CreateForExecution(Language.Grammar.InputSymbol, input)) as IList<StringRegion>;
		}

		// Token: 0x06009246 RID: 37446 RVA: 0x001EC692 File Offset: 0x001EA892
		public IList<StringRegion> Run(string input)
		{
			return this.Run(new StringRegion(input, Semantics.Tokens));
		}

		// Token: 0x06009247 RID: 37447 RVA: 0x001EC6A5 File Offset: 0x001EA8A5
		public string Serialize()
		{
			return this.ProgramNode.PrintAST(ASTSerializationFormat.XML);
		}

		// Token: 0x06009248 RID: 37448 RVA: 0x001EC6B3 File Offset: 0x001EA8B3
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((Program)obj)));
		}

		// Token: 0x06009249 RID: 37449 RVA: 0x001EC6E1 File Offset: 0x001EA8E1
		public override int GetHashCode()
		{
			ProgramNode programNode = this.ProgramNode;
			if (programNode == null)
			{
				return 0;
			}
			return programNode.GetHashCode();
		}

		// Token: 0x0600924A RID: 37450 RVA: 0x001EC6F4 File Offset: 0x001EA8F4
		public override string ToString()
		{
			return this.ProgramNode.ToString();
		}
	}
}
