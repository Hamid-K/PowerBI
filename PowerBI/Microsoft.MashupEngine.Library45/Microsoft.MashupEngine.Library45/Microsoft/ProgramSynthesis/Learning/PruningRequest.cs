using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Learning
{
	// Token: 0x020006BD RID: 1725
	public class PruningRequest : IEquatable<PruningRequest>
	{
		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x0600254A RID: 9546 RVA: 0x00067873 File Offset: 0x00065A73
		public int? K { get; }

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x0006787B File Offset: 0x00065A7B
		public int? RandomK { get; }

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600254C RID: 9548 RVA: 0x00067883 File Offset: 0x00065A83
		public ProgramSamplingStrategy ProgramSamplingStrategy { get; }

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x0600254D RID: 9549 RVA: 0x0006788B File Offset: 0x00065A8B
		public IFeature TopProgramsFeature { get; }

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x00067893 File Offset: 0x00065A93
		public IFeatureOptions TopProgramsFeatureOptions { get; }

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x0006789B File Offset: 0x00065A9B
		public bool IsEmpty
		{
			get
			{
				return !this.HasTopKRequest && !this.HasRandomKRequest;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06002550 RID: 9552 RVA: 0x000678B0 File Offset: 0x00065AB0
		public static PruningRequest Empty { get; } = new PruningRequest(null, null, null, null, ProgramSamplingStrategy.UniformRandom);

		// Token: 0x06002551 RID: 9553 RVA: 0x000678B7 File Offset: 0x00065AB7
		private PruningRequest(int? topProgramsK, int? randomProgramsK, IFeature topProgramsFeature, IFeatureOptions topProgramsFeatureOptions, ProgramSamplingStrategy programSamplingStrategy)
		{
			this.K = topProgramsK;
			this.RandomK = randomProgramsK;
			this.TopProgramsFeature = topProgramsFeature;
			this.TopProgramsFeatureOptions = topProgramsFeatureOptions;
			this.ProgramSamplingStrategy = programSamplingStrategy;
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x000678E4 File Offset: 0x00065AE4
		public static PruningRequest Create(int? topProgramsK, int? randomProgramsK, IFeature topProgramsFeature, IFeatureOptions topProgramsFeatureOptions, ProgramSamplingStrategy programsSamplingStrategy)
		{
			if (topProgramsK != null && topProgramsFeature == null)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Cannot create a top programs pruning request with a null value for the feature.", Array.Empty<object>())));
			}
			if (topProgramsK == null && randomProgramsK == null)
			{
				return PruningRequest.Empty;
			}
			if (topProgramsK != null && topProgramsK.Value <= 0)
			{
				throw new ArgumentException("Value must be greater than 0.", "topProgramsK");
			}
			if (randomProgramsK != null && randomProgramsK.Value <= 0)
			{
				throw new ArgumentException("Value must be greater than 0", "randomProgramsK");
			}
			return new PruningRequest(topProgramsK, randomProgramsK, topProgramsFeature, topProgramsFeatureOptions, programsSamplingStrategy);
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x00067980 File Offset: 0x00065B80
		public bool HasTopKRequest
		{
			get
			{
				return this.K != null;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06002554 RID: 9556 RVA: 0x0006799C File Offset: 0x00065B9C
		public bool HasRandomKRequest
		{
			get
			{
				return this.RandomK != null;
			}
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x000679B8 File Offset: 0x00065BB8
		public bool Equals(PruningRequest other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			int? num = this.K;
			int? num2 = other.K;
			if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
			{
				num2 = this.RandomK;
				num = other.RandomK;
				if (((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null))) && object.Equals(this.TopProgramsFeature, other.TopProgramsFeature) && object.Equals(this.TopProgramsFeatureOptions, other.TopProgramsFeatureOptions))
				{
					return this.ProgramSamplingStrategy == other.ProgramSamplingStrategy;
				}
			}
			return false;
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x00067A68 File Offset: 0x00065C68
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((PruningRequest)obj)));
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x00067A98 File Offset: 0x00065C98
		public override int GetHashCode()
		{
			return (((((((this.K.GetHashCode() * 983) ^ this.RandomK.GetHashCode()) * 7481) ^ ((this.TopProgramsFeature != null) ? this.TopProgramsFeature.GetHashCode() : 0)) * 11503) ^ ((this.TopProgramsFeatureOptions != null) ? this.TopProgramsFeatureOptions.GetHashCode() : 0)) * 16883) ^ this.ProgramSamplingStrategy.GetHashCode();
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x00067B2C File Offset: 0x00065D2C
		public override string ToString()
		{
			if (this.K == null && this.RandomK == null)
			{
				return "No Pruning Request";
			}
			if (this.K != null && this.RandomK != null)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("Top {0} by {1} + Random {2} by sampling: {3}", new object[]
				{
					this.K.Value,
					this.TopProgramsFeature.Info.Name,
					this.RandomK,
					this.ProgramSamplingStrategy
				}));
			}
			if (this.K != null)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("Top {0} by {1}", new object[]
				{
					this.K.Value,
					this.TopProgramsFeature.Info.Name
				}));
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("Random {0} by sampling: {1}", new object[] { this.RandomK, this.ProgramSamplingStrategy }));
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x00067C5A File Offset: 0x00065E5A
		public PruningRequest AugmentWithTopProgramsK(int topProgramsK, IFeature topProgramsFeature, IFeatureOptions topProgramsFeatureOptions)
		{
			if (this.HasTopKRequest)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Augmenting a PruningRequest that already has a TopKRequest is not allowed, perhaps you meant to use {0}?", new object[] { "ReplaceWithTopProgramsK" })));
			}
			return this.ReplaceWithTopProgramsK(topProgramsK, topProgramsFeature, topProgramsFeatureOptions);
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x00067C90 File Offset: 0x00065E90
		public PruningRequest ReplaceWithTopProgramsK(int topProgramsK, IFeature topProgramsFeature, IFeatureOptions topProgramsFeatureOptions)
		{
			return PruningRequest.Create(new int?(topProgramsK), this.RandomK, topProgramsFeature, topProgramsFeatureOptions, this.ProgramSamplingStrategy);
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x00067CAB File Offset: 0x00065EAB
		public PruningRequest WithNewTopProgramsK(int newTopProgramsK)
		{
			if (!this.HasTopKRequest)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Cannot set a new topProgramsK value on a PruningRequest that doesn't have an existing top programs request.", Array.Empty<object>())));
			}
			return this.ReplaceWithTopProgramsK(newTopProgramsK, this.TopProgramsFeature, this.TopProgramsFeatureOptions);
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x00067CE4 File Offset: 0x00065EE4
		public PruningRequest AugmentWithRandomProgramsK(int randomK)
		{
			if (this.HasRandomKRequest)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Augmenting a PruningRequest that already has a RandomK request is not allowed, perhaps you meant to use {0}?", new object[] { "ReplaceWithRandomProgramsK" })));
			}
			return this.ReplaceWithRandomProgramsK(randomK, null);
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x00067D2C File Offset: 0x00065F2C
		public PruningRequest ReplaceWithRandomProgramsK(int newRandomK, ProgramSamplingStrategy? programSamplingStrategy = null)
		{
			return PruningRequest.Create(this.K, new int?(newRandomK), this.TopProgramsFeature, this.TopProgramsFeatureOptions, programSamplingStrategy ?? this.ProgramSamplingStrategy);
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x00067D70 File Offset: 0x00065F70
		public PruningRequest WithoutRandomK()
		{
			if (!this.HasRandomKRequest)
			{
				return this;
			}
			return PruningRequest.Create(this.K, null, this.TopProgramsFeature, this.TopProgramsFeatureOptions, this.ProgramSamplingStrategy);
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x00067DB0 File Offset: 0x00065FB0
		public PruningRequest WithoutTopK()
		{
			if (!this.HasTopKRequest)
			{
				return this;
			}
			return PruningRequest.Create(null, this.RandomK, null, null, this.ProgramSamplingStrategy);
		}
	}
}
