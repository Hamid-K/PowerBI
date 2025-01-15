using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Exceptions;
using Microsoft.ProgramSynthesis.Wrangling.Logging;
using Microsoft.ProgramSynthesis.Wrangling.SignificantInputs;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.Session
{
	// Token: 0x02000119 RID: 281
	public class NonInteractiveSession<TProgram, TInput, TOutput> : Session<TProgram, TInput, TOutput> where TProgram : Program<TInput, TOutput>
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0001454E File Offset: 0x0001274E
		public new static JsonSerializerSettings JsonSerializerSettings
		{
			get
			{
				return NonInteractiveSession<TProgram, TInput, TOutput>.LazyJsonSerializerSettings.Value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0001455A File Offset: 0x0001275A
		protected override JsonSerializerSettings JsonSerializerSettingsInstance
		{
			get
			{
				return NonInteractiveSession<TProgram, TInput, TOutput>.JsonSerializerSettings;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00014561 File Offset: 0x00012761
		private Grammar Grammar { get; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00014569 File Offset: 0x00012769
		public override string LoggingTypeName { get; }

		// Token: 0x0600064B RID: 1611 RVA: 0x00014574 File Offset: 0x00012774
		public NonInteractiveSession(ProgramLearner<TProgram, TInput, TOutput> learner, IProgramLoader<TProgram, TInput, TOutput> loader, IJournalStorage journalStorage, CultureInfo culture, string loggingTypeName = null, ILogger logger = null, bool useCache = true)
			: base(loader, journalStorage, culture, logger)
		{
			this._learner = learner;
			this.Grammar = learner.ScoreFeature.Grammar;
			this.LoggingTypeName = loggingTypeName ?? this.Grammar.Name;
			this._useCache = useCache;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000145EC File Offset: 0x000127EC
		protected virtual ProgramLearner<TProgram, TInput, TOutput> LearnerFor(RankingMode rankingMode)
		{
			if (rankingMode != RankingMode.MostLikely && rankingMode != RankingMode.NoRanking)
			{
				throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Unsupported RankingMode: {0}", new object[] { rankingMode })));
			}
			return this._learner;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00014623 File Offset: 0x00012823
		protected virtual uint MaxCacheSize { get; } = 5U;

		// Token: 0x0600064E RID: 1614 RVA: 0x0001462B File Offset: 0x0001282B
		protected TProgram BuildProgram(ProgramNode programNode)
		{
			return base.ProgramLoader.Create(programNode);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001463C File Offset: 0x0001283C
		protected virtual Session<TProgram, TInput, TOutput>.IProgramSetWrapper LearnImpl(LearnProgramRequest<TProgram, TInput, TOutput> request, RankingMode rankingMode, int? k, int? randomK, CancellationToken cancel)
		{
			if (k == null)
			{
				return new Session<TProgram, TInput, TOutput>.ProgramSetWrapper(this.LearnerFor(rankingMode).LearnAll(request.Constraints, request.Inputs, cancel), new Func<ProgramNode, TProgram>(this.BuildProgram));
			}
			ProgramCollection<TProgram, TInput, TOutput, double> programCollection = this.LearnerFor(rankingMode).LearnTopK(request.Constraints, k.Value, randomK, Microsoft.ProgramSynthesis.Learning.ProgramSamplingStrategy.UniformAcrossUnions, request.Inputs, cancel);
			return new Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper(programCollection.TopPrograms, programCollection.RandomPrograms);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000146B4 File Offset: 0x000128B4
		protected virtual FeatureCalculationContext GetFeatureCalculationContext(LearnProgramRequest<TProgram, TInput, TOutput> request, RankingMode rankingMode)
		{
			return FeatureCalculationContext.Create((from c in request.Constraints.OfType<ConstraintOnInput<TInput, TOutput>>()
				select State.CreateForLearning(this.Grammar.InputSymbol, c.Input)).ToList<State>(), request.Inputs.Select((TInput input) => State.CreateForLearning(this.Grammar.InputSymbol, input)).ToList<State>(), this.LearnerFor(rankingMode).GetFeatureOptionsFor(request.Constraints, null));
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00014718 File Offset: 0x00012918
		private Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper TopKRandomK(Session<TProgram, TInput, TOutput>.IProgramSetWrapper programSetWrapper, LearnProgramRequest<TProgram, TInput, TOutput> request, RankingMode rankingMode, int k, int? randomK, CancellationToken cancel)
		{
			ProgramSet programSet = programSetWrapper.ProgramSet;
			ProgramLearner<TProgram, TInput, TOutput> programLearner = this.LearnerFor(rankingMode);
			PrunedProgramSet prunedProgramSet = programSet.Prune(new int?(k), randomK, programLearner.ScoreFeature, null, this.GetFeatureCalculationContext(request, rankingMode), Microsoft.ProgramSynthesis.Learning.ProgramSamplingStrategy.UniformAcrossUnions, new Random(0), null);
			return new Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper(prunedProgramSet.TopPrograms.Select(new Func<ProgramNode, TProgram>(this.BuildProgram)).ToList<TProgram>(), prunedProgramSet.RandomlySampledPrograms.Select(new Func<ProgramNode, TProgram>(this.BuildProgram)).ToList<TProgram>());
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00014798 File Offset: 0x00012998
		protected void CleanCache()
		{
			while ((long)this._recentRequests.Distinct<LearnProgramRequest<TProgram, TInput, TOutput>>().Count<LearnProgramRequest<TProgram, TInput, TOutput>>() > (long)((ulong)this.MaxCacheSize))
			{
				LearnProgramRequest<TProgram, TInput, TOutput> learnProgramRequest;
				if (this._recentRequests.TryDequeue(out learnProgramRequest) && !this._recentRequests.Contains(learnProgramRequest))
				{
					Session<TProgram, TInput, TOutput>.IProgramSetWrapper programSetWrapper;
					this._learnAllCache.TryRemove(learnProgramRequest, out programSetWrapper);
					ConcurrentDictionary<NonInteractiveSession<TProgram, TInput, TOutput>.CacheKey, Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper> concurrentDictionary;
					this._learnPrunedCache.TryRemove(learnProgramRequest, out concurrentDictionary);
				}
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000147FC File Offset: 0x000129FC
		internal sealed override Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper LearnTopKRandomKCached(LearnProgramRequest<TProgram, TInput, TOutput> request, RankingMode rankingMode, int k, int? randomK, bool cachedOnly, CancellationToken cancel)
		{
			if (!this._useCache)
			{
				return (Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper)this.LearnImpl(request, rankingMode, new int?(k), randomK, cancel);
			}
			this._recentRequests.Enqueue(request);
			rankingMode = this.ReifyRankingMode(rankingMode);
			NonInteractiveSession<TProgram, TInput, TOutput>.CacheKey cacheKey = new NonInteractiveSession<TProgram, TInput, TOutput>.CacheKey(rankingMode, k, randomK);
			Optional<Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper> cachedTopKRandomK = this.GetCachedTopKRandomK(request, cacheKey, cancel);
			if (cachedTopKRandomK.HasValue)
			{
				return cachedTopKRandomK.Value;
			}
			if (cachedOnly)
			{
				return null;
			}
			Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper prunedProgramSetWrapper = (Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper)this.LearnImpl(request, rankingMode, new int?(k), randomK, cancel);
			this._learnPrunedCache.GetOrAdd(request, (LearnProgramRequest<TProgram, TInput, TOutput> _) => new ConcurrentDictionary<NonInteractiveSession<TProgram, TInput, TOutput>.CacheKey, Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper>())[cacheKey] = prunedProgramSetWrapper;
			this.CleanCache();
			return prunedProgramSetWrapper;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000148BC File Offset: 0x00012ABC
		protected virtual TInput ChooseSignificantInputFromCluster(IReadOnlyList<TInput> cluster)
		{
			return cluster[0];
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000148C8 File Offset: 0x00012AC8
		protected Task<IReadOnlyList<SignificantInput<TInput>>> GetSignificantInputsAsync(Microsoft.ProgramSynthesis.Wrangling.SignificantInputs.ProgramSamplingStrategy programSamplingStrategy, double? confidenceThreshold = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null, LearnProgramRequest<TProgram, TInput, TOutput> request = null)
		{
			request = request ?? this.BuildLearnProgramRequest(new bool?(true));
			return Task.Run<IReadOnlyList<SignificantInput<TInput>>>(delegate
			{
				bool cachedOnly = Microsoft.ProgramSynthesis.Wrangling.SignificantInputs.ProgramSamplingStrategy.DefaultSamplingStrategy.CachedOnly;
				Stopwatch stopwatch = Stopwatch.StartNew();
				TProgram tprogram = delegate
				{
					Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper prunedProgramSetWrapper = this.LearnTopKRandomKCached(request.WithoutInputs(), RankingMode.MostLikely, 1, null, cachedOnly, cancel);
					if (prunedProgramSetWrapper == null)
					{
						return default(TProgram);
					}
					IReadOnlyList<TProgram> topPrograms = prunedProgramSetWrapper._topPrograms;
					if (topPrograms == null)
					{
						return default(TProgram);
					}
					return topPrograms.FirstOrDefault<TProgram>();
				}.DefaultIfException<TProgram, InputsRequiredException>();
				IReadOnlyList<TProgram> readOnlyList;
				IReadOnlyList<TProgram> readOnlyList2;
				programSamplingStrategy.SamplePrograms<NonInteractiveSession<TProgram, TInput, TOutput>, TProgram, TInput, TOutput>(request, confidenceThreshold ?? this.ConfidenceThreshold, this, cancel).Deconstruct(out readOnlyList, out readOnlyList2);
				IReadOnlyList<TProgram> readOnlyList3 = readOnlyList;
				IReadOnlyList<TProgram> readOnlyList4 = readOnlyList2;
				SignificantInputProgramProfile<TProgram, TInput, TOutput> significantInputProgramProfile = new SignificantInputProgramProfile<TProgram, TInput, TOutput>(readOnlyList3, readOnlyList4, tprogram);
				double num = (double)stopwatch.ElapsedMilliseconds;
				IReadOnlyList<SignificantInput<TInput>> significantInputs = this.GetSignificantInputs(new List<Distinguisher<TInput>>(), request, significantInputProgramProfile, cancel);
				double num2 = (double)stopwatch.ElapsedMilliseconds - num;
				this.Logger.TrackEvent("SignificantInputs", new KeyValuePair<string, double>[]
				{
					KVP.Create<string, double>("NumSignificantInputs", (double)((significantInputs != null) ? significantInputs.Count : 0)),
					KVP.Create<string, double>("TotalTime", stopwatch.Elapsed.TotalMilliseconds),
					KVP.Create<string, double>("ProgramSampleTime", num),
					KVP.Create<string, double>("SignificantInputsTime", num2)
				}.Concat(this.TrackedLearningMetrics(request, default(TProgram))).ToArray<KeyValuePair<string, double>>(), new KeyValuePair<string, string>[]
				{
					KVP.Create<string, string>("Id", (guid ?? Guid.NewGuid()).ToString()),
					KVP.Create<string, string>("Success", (significantInputs != null).AsLoggingValue()),
					KVP.Create<string, string>("ProgramSamplingStrategy", programSamplingStrategy.ToString())
				}.Concat(this.TrackedLearningProperties(request, default(TProgram))).ToArray<KeyValuePair<string, string>>(), this.TrackedLearningUserProperties(request, default(TProgram), true).ToArray<KeyValuePair<string, string>>());
				return significantInputs;
			});
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00014932 File Offset: 0x00012B32
		public override Task<IReadOnlyList<SignificantInput<TInput>>> GetSignificantInputsAsync(double? confidenceThreshold = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			return this.GetSignificantInputsAsync(Microsoft.ProgramSynthesis.Wrangling.SignificantInputs.ProgramSamplingStrategy.DefaultSamplingStrategy, confidenceThreshold, cancel, guid, null);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00014944 File Offset: 0x00012B44
		protected virtual IReadOnlyList<SignificantInput<TInput>> GetSignificantInputs(IReadOnlyList<Distinguisher<TInput>> distinguishers, LearnProgramRequest<TProgram, TInput, TOutput> request, SignificantInputProgramProfile<TProgram, TInput, TOutput> programProfile, CancellationToken cancel)
		{
			HashSet<TInput> hashSet = request.ConstrainedInputs();
			return new SignificantInputsEngine<TProgram, TInput, TOutput>(programProfile, request.Inputs, distinguishers, cancel).SignificantInputClustersCover(this, hashSet);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00014970 File Offset: 0x00012B70
		public override IObservable<IReadOnlyList<SignificantInput<TInput>>> GetSignificantInputs(double? confidenceThreshold = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			guid = new Guid?(guid ?? Guid.NewGuid());
			LearnProgramRequest<TProgram, TInput, TOutput> request = this.BuildLearnProgramRequest(new bool?(true));
			return Microsoft.ProgramSynthesis.Wrangling.SignificantInputs.ProgramSamplingStrategy.DefaultSamplingStrategies.Select((Microsoft.ProgramSynthesis.Wrangling.SignificantInputs.ProgramSamplingStrategy samplingStrategy) => () => this.GetSignificantInputsAsync(samplingStrategy, confidenceThreshold, cancel, guid, request).ContinueWith<Optional<IReadOnlyList<SignificantInput<TInput>>>>((Task<IReadOnlyList<SignificantInput<TInput>>> res) => res.Result.SomeIfNotNull<IReadOnlyList<SignificantInput<TInput>>>(), cancel)).ObserveResults(cancel);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x000149FB File Offset: 0x00012BFB
		private static double ComputeConfidence(double topProgramScore, double score)
		{
			return 1.0 - (topProgramScore - score) / 100.0;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00014A14 File Offset: 0x00012C14
		internal double ComputeConfidence(TProgram topProgram, TProgram program)
		{
			return NonInteractiveSession<TProgram, TInput, TOutput>.ComputeConfidence(topProgram.Score, program.Score);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00014A34 File Offset: 0x00012C34
		public override Task<IReadOnlyList<TOutput>> ComputeTopKOutputsAsync(TInput input, int k, RankingMode rankingMode = null, double? confidenceThreshold = null, CancellationToken cancel = default(CancellationToken))
		{
			if (k <= 0)
			{
				throw new ArgumentException("k must be positive", "k");
			}
			LearnProgramRequest<TProgram, TInput, TOutput> request = this.BuildLearnProgramRequest(new bool?(true));
			if (request == null)
			{
				return new TaskCompletionSource<IReadOnlyList<TOutput>>(new TOutput[0]).Task;
			}
			confidenceThreshold = new double?(confidenceThreshold ?? base.ConfidenceThreshold);
			rankingMode = this.ReifyRankingMode(rankingMode);
			int numPrograms = ((k == 1) ? 1 : Math.Max(15, k * 2));
			Func<TProgram, TOutput> <>9__2;
			return Task.Run<IReadOnlyList<TOutput>>(delegate
			{
				Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper prunedProgramSetWrapper = this.LearnTopKRandomKCached(request, rankingMode, numPrograms, null, false, cancel);
				IReadOnlyList<TProgram> readOnlyList = ((prunedProgramSetWrapper != null) ? prunedProgramSetWrapper.RealizedPrograms.ToList<TProgram>() : null);
				TProgram topProgram = ((readOnlyList != null) ? readOnlyList.First<TProgram>() : default(TProgram));
				if (readOnlyList == null)
				{
					return null;
				}
				IEnumerable<TProgram> enumerable = readOnlyList.TakeWhile((TProgram p) => this.ComputeConfidence(topProgram, p) >= confidenceThreshold.Value);
				Func<TProgram, TOutput> func;
				if ((func = <>9__2) == null)
				{
					func = (<>9__2 = (TProgram p) => p.Run(input));
				}
				return enumerable.Select(func).Distinct<TOutput>().Take(k)
					.ToList<TOutput>();
			}, cancel);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00014B31 File Offset: 0x00012D31
		private RankingMode ReifyRankingMode(RankingMode rankingMode)
		{
			return rankingMode ?? base.RankingMode ?? RankingMode.MostLikely;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00014B48 File Offset: 0x00012D48
		protected override Session<TProgram, TInput, TOutput>.IProgramSetWrapper LearnAllCached(LearnProgramRequest<TProgram, TInput, TOutput> request, bool cachedOnly, CancellationToken cancel)
		{
			if (this._useCache)
			{
				this._recentRequests.Enqueue(request);
				Session<TProgram, TInput, TOutput>.IProgramSetWrapper programSetWrapper;
				if (this._learnAllCache.TryGetValue(request, out programSetWrapper))
				{
					return programSetWrapper;
				}
				if (cachedOnly)
				{
					return null;
				}
			}
			Session<TProgram, TInput, TOutput>.IProgramSetWrapper programSetWrapper2 = this.LearnImpl(request, RankingMode.NoRanking, null, null, cancel);
			if (this._useCache)
			{
				this._learnAllCache[request] = programSetWrapper2;
				this.CleanCache();
			}
			return programSetWrapper2;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00014BBC File Offset: 0x00012DBC
		private Optional<Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper> GetCachedTopKRandomK(LearnProgramRequest<TProgram, TInput, TOutput> request, NonInteractiveSession<TProgram, TInput, TOutput>.CacheKey key, CancellationToken cancel = default(CancellationToken))
		{
			ConcurrentDictionary<NonInteractiveSession<TProgram, TInput, TOutput>.CacheKey, Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper> orAdd;
			if (this._learnPrunedCache.TryGetValue(request, out orAdd))
			{
				KeyValuePair<NonInteractiveSession<TProgram, TInput, TOutput>.CacheKey, Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper>? keyValuePair = orAdd.Where((KeyValuePair<NonInteractiveSession<TProgram, TInput, TOutput>.CacheKey, Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper> kv) => kv.Key.Covers(key)).FirstOrNull<KeyValuePair<NonInteractiveSession<TProgram, TInput, TOutput>.CacheKey, Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper>>();
				if (keyValuePair != null)
				{
					return keyValuePair.Value.Value.Some<Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper>();
				}
			}
			Session<TProgram, TInput, TOutput>.IProgramSetWrapper programSetWrapper;
			if (this._learnAllCache.TryGetValue(request, out programSetWrapper))
			{
				Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper prunedProgramSetWrapper = this.TopKRandomK(programSetWrapper, request, key.RankingMode, key.NumTopPrograms, key.NumRandomPrograms, cancel);
				orAdd = this._learnPrunedCache.GetOrAdd(request, new ConcurrentDictionary<NonInteractiveSession<TProgram, TInput, TOutput>.CacheKey, Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper>());
				orAdd[key] = prunedProgramSetWrapper;
				return prunedProgramSetWrapper.Some<Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper>();
			}
			return Optional<Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper>.Nothing;
		}

		// Token: 0x040002B4 RID: 692
		private static readonly Lazy<JsonSerializerSettings> LazyJsonSerializerSettings = new Lazy<JsonSerializerSettings>(() => new NonInteractiveSessionJsonSerializerSettings<TProgram, TInput, TOutput>().Initialize());

		// Token: 0x040002B5 RID: 693
		private readonly ConcurrentDictionary<LearnProgramRequest<TProgram, TInput, TOutput>, Session<TProgram, TInput, TOutput>.IProgramSetWrapper> _learnAllCache = new ConcurrentDictionary<LearnProgramRequest<TProgram, TInput, TOutput>, Session<TProgram, TInput, TOutput>.IProgramSetWrapper>();

		// Token: 0x040002B6 RID: 694
		private readonly ConcurrentDictionary<LearnProgramRequest<TProgram, TInput, TOutput>, ConcurrentDictionary<NonInteractiveSession<TProgram, TInput, TOutput>.CacheKey, Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper>> _learnPrunedCache = new ConcurrentDictionary<LearnProgramRequest<TProgram, TInput, TOutput>, ConcurrentDictionary<NonInteractiveSession<TProgram, TInput, TOutput>.CacheKey, Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper>>();

		// Token: 0x040002B7 RID: 695
		private readonly ProgramLearner<TProgram, TInput, TOutput> _learner;

		// Token: 0x040002B8 RID: 696
		private readonly ConcurrentQueue<LearnProgramRequest<TProgram, TInput, TOutput>> _recentRequests = new ConcurrentQueue<LearnProgramRequest<TProgram, TInput, TOutput>>();

		// Token: 0x040002BB RID: 699
		private bool _useCache;

		// Token: 0x0200011A RID: 282
		private struct CacheKey
		{
			// Token: 0x06000662 RID: 1634 RVA: 0x00014CD7 File Offset: 0x00012ED7
			public CacheKey(RankingMode rankingMode, int numTopPrograms, int? numRandomPrograms)
			{
				this.RankingMode = rankingMode;
				this.NumTopPrograms = numTopPrograms;
				this.NumRandomPrograms = numRandomPrograms;
			}

			// Token: 0x06000663 RID: 1635 RVA: 0x00014CF0 File Offset: 0x00012EF0
			public bool Covers(NonInteractiveSession<TProgram, TInput, TOutput>.CacheKey other)
			{
				if (this.RankingMode != other.RankingMode || this.NumTopPrograms < other.NumTopPrograms)
				{
					return false;
				}
				if (other.NumRandomPrograms == null)
				{
					return true;
				}
				if (this.NumRandomPrograms != null)
				{
					int value = other.NumRandomPrograms.Value;
					int? numRandomPrograms = this.NumRandomPrograms;
					return (value <= numRandomPrograms.GetValueOrDefault()) & (numRandomPrograms != null);
				}
				return false;
			}

			// Token: 0x040002BD RID: 701
			public readonly RankingMode RankingMode;

			// Token: 0x040002BE RID: 702
			public readonly int NumTopPrograms;

			// Token: 0x040002BF RID: 703
			public readonly int? NumRandomPrograms;
		}
	}
}
