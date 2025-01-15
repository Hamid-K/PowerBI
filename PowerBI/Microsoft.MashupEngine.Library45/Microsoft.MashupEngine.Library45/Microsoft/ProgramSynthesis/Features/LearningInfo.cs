using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007DB RID: 2011
	public class LearningInfo : IEquatable<LearningInfo>
	{
		// Token: 0x06002AD8 RID: 10968 RVA: 0x00078252 File Offset: 0x00076452
		public LearningInfo(FeatureCalculationContext featureCalculationContext, ProgramNode programNode)
		{
			this.FeatureCalculationContext = featureCalculationContext;
			this.ProgramNode = programNode;
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x00078268 File Offset: 0x00076468
		public ProgramNode ProgramNode { get; }

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06002ADA RID: 10970 RVA: 0x00078270 File Offset: 0x00076470
		public FeatureCalculationContext FeatureCalculationContext { get; }

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x00078278 File Offset: 0x00076478
		public IFeatureOptions Options
		{
			get
			{
				return this.FeatureCalculationContext.Options;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002ADC RID: 10972 RVA: 0x00078285 File Offset: 0x00076485
		public IReadOnlyList<State> SpecInputs
		{
			get
			{
				return this.FeatureCalculationContext.MaterializeSpecInputs();
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x00078292 File Offset: 0x00076492
		public IReadOnlyList<State> AdditionalInputs
		{
			get
			{
				return this.FeatureCalculationContext.AdditionalInputs;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002ADE RID: 10974 RVA: 0x0007829F File Offset: 0x0007649F
		public IReadOnlyList<State> AllInputs
		{
			get
			{
				return this.FeatureCalculationContext.AllInputs;
			}
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x000782AC File Offset: 0x000764AC
		public IEnumerable<object> GetOutputs(InputKind kind)
		{
			return this.FeatureCalculationContext.GetInputs(kind).Select(new Func<State, object>(this.ProgramNode.Invoke));
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000782D0 File Offset: 0x000764D0
		public IEnumerable<KeyValuePair<State, object>> GetInputOutputPairs(InputKind kind)
		{
			return this.FeatureCalculationContext.GetInputs(kind).Zip(this.GetOutputs(kind), (State input, object output) => new KeyValuePair<State, object>(input, output));
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x00078309 File Offset: 0x00076509
		public IReadOnlyDictionary<State, object> GetOutputMapping(InputKind kind)
		{
			return this.GetInputOutputPairs(kind).ToDictionary<State, object>();
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x00078318 File Offset: 0x00076518
		public LearningInfo ForChild(int childIndex)
		{
			int num = this.ProgramNode.Children.Length;
			if (childIndex >= num || childIndex < 0)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Argument value {0} is out of bounds", new object[] { childIndex })), "childIndex");
			}
			return new LearningInfo(this.FeatureCalculationContext.TransformForChild(this.ProgramNode, childIndex), this.ProgramNode.Children[childIndex]);
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x00078388 File Offset: 0x00076588
		public bool Equals(LearningInfo other)
		{
			return other != null && (this == other || (this.ProgramNode.Equals(other.ProgramNode) && this.FeatureCalculationContext.Equals(other.FeatureCalculationContext)));
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000783BB File Offset: 0x000765BB
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((LearningInfo)obj)));
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x000783E9 File Offset: 0x000765E9
		public override int GetHashCode()
		{
			int num = this.ProgramNode.GetHashCode() * 1151;
			FeatureCalculationContext featureCalculationContext = this.FeatureCalculationContext;
			return num ^ ((featureCalculationContext != null) ? featureCalculationContext.GetHashCode() : 0);
		}
	}
}
