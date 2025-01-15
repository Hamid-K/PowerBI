using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning
{
	// Token: 0x020006C6 RID: 1734
	public sealed class SynthesisEngine
	{
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060025A8 RID: 9640 RVA: 0x00068BD8 File Offset: 0x00066DD8
		public SynthesisEngine.Config Configuration { get; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x00068BE0 File Offset: 0x00066DE0
		public Grammar Grammar { get; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060025AA RID: 9642 RVA: 0x00068BE8 File Offset: 0x00066DE8
		// (set) Token: 0x060025AB RID: 9643 RVA: 0x00068BF0 File Offset: 0x00066DF0
		internal BindingManager BindingManager { get; set; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x00068BF9 File Offset: 0x00066DF9
		internal EventFactory LogEvent { get; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060025AD RID: 9645 RVA: 0x00068C01 File Offset: 0x00066E01
		public Random RandomNumberGenerator { get; }

		// Token: 0x060025AE RID: 9646 RVA: 0x00068C0C File Offset: 0x00066E0C
		public SynthesisEngine(Grammar grammar, SynthesisEngine.Config config = null, int? randomSeed = null)
		{
			this.Configuration = config ?? new SynthesisEngine.Config();
			this.Grammar = grammar;
			this.BindingManager = BindingManager.Standard(grammar);
			this.LogEvent = ((this.Configuration.LogListener != null) ? new EventFactory(this.Configuration.LogListener.IncludedInfo) : null);
			ISynthesisStrategy[] strategies = this.Configuration.Strategies;
			for (int i = 0; i < strategies.Length; i++)
			{
				strategies[i].Initialize(this);
			}
			this.RandomNumberGenerator = new Random(randomSeed ?? 262254385);
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x00068CCA File Offset: 0x00066ECA
		public ProgramSet LearnGrammar(Spec spec, CancellationToken cancel = default(CancellationToken))
		{
			return this.LearnSymbol(this.Grammar.StartSymbol, spec, cancel);
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x00068CE0 File Offset: 0x00066EE0
		public ProgramSet LearnGrammarTopK(Spec spec, IFeature feature, int k = 1, int? randomK = null, ProgramSamplingStrategy samplingStrategy = ProgramSamplingStrategy.UniformRandom, CancellationToken cancel = default(CancellationToken))
		{
			return this.LearnSymbolTopK(this.Grammar.StartSymbol, spec, feature, k, randomK, samplingStrategy, cancel, null);
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x00068D08 File Offset: 0x00066F08
		public ProgramSet LearnSymbolTopK(Symbol symbol, Spec spec, IFeature feature, int k = 1, int? randomK = null, ProgramSamplingStrategy samplingStrategy = ProgramSamplingStrategy.UniformRandom, CancellationToken cancel = default(CancellationToken), IFeatureOptions featureOptions = null)
		{
			return this.Learn(LearningTask.Create(symbol, spec, randomK, samplingStrategy, new int?(k), feature, featureOptions), cancel);
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x00068D27 File Offset: 0x00066F27
		public ProgramSet LearnSymbol(Symbol symbol, Spec spec, CancellationToken cancel = default(CancellationToken))
		{
			return this.Learn(new LearningTask(symbol, spec), cancel);
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x00068D38 File Offset: 0x00066F38
		public ProgramSet Learn(LearningTask task, CancellationToken cancel = default(CancellationToken))
		{
			LogListener logListener = this.Configuration.LogListener;
			if (logListener != null)
			{
				logListener.EnterEvent(this.LogEvent.StartedLearn(task));
			}
			SynthesisEngine synthesisEngine = this;
			lock (synthesisEngine)
			{
				SynthesisEngine.CacheEntry cacheEntry;
				if (this._learningCache.TryGetValue(task, out cacheEntry))
				{
					int useFrequency = cacheEntry.UseFrequency;
					this._cacheUseFrequency[useFrequency].Remove(task);
					this._cacheUseFrequency.AddOrCreate(1 + useFrequency, task);
					cacheEntry.UseFrequency++;
					return this.LogResult(task, cacheEntry.Programs, true);
				}
			}
			bool flag2 = false;
			ISynthesisStrategy[] strategies = this.Configuration.Strategies;
			for (int i = 0; i < strategies.Length; i++)
			{
				ISynthesisStrategy strategy = strategies[i];
				if ((task.IsOneShot || strategy.Attributes.Contains(StrategyAttribute.SupportsIncrementalLearning)) && strategy.CanCall(task.Spec))
				{
					if (task.IsOrdered)
					{
						if (!strategy.Attributes.Contains(StrategyAttribute.SupportsOrderedTasks))
						{
							goto IL_0295;
						}
						flag2 = true;
					}
					using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
					{
						using (CancellationTokenSource combinedCts = CancellationTokenSource.CreateLinkedTokenSource(cancel, cancellationTokenSource.Token))
						{
							Optional<ProgramSet> optional = this.InvokeStrategy<Optional<ProgramSet>>(() => strategy.Learn(this, task, combinedCts.Token));
							if (optional.HasValue)
							{
								ProgramSet programSet = optional.Value.Normalize(task);
								if (!strategy.Attributes.Contains(StrategyAttribute.WithInfiniteResults))
								{
									this.LogResult(task, programSet, false);
								}
								this.ValidateResultSoundness(task, optional);
								synthesisEngine = this;
								lock (synthesisEngine)
								{
									this._learningCache[task] = new SynthesisEngine.CacheEntry
									{
										Programs = programSet
									};
									this._cacheUseFrequency.AddOrCreate(0, task);
								}
								this.TrimCacheIfNecessary();
								return programSet;
							}
							cancellationTokenSource.Cancel();
						}
					}
				}
				IL_0295:;
			}
			if (!task.IsOrdered)
			{
				return this.LogResult(task, ProgramSet.Empty(task.Symbol), false);
			}
			if (flag2)
			{
				return this.LogResult(task, ProgramSet.Empty(task.Symbol), false);
			}
			ProgramSet programSet2 = this.Learn(task.WithoutTopKRequest(), cancel);
			programSet2 = programSet2.Prune(task.PruningRequest, task.FeatureCalculationContext, this.RandomNumberGenerator, this.Configuration.LogListener);
			return this.LogResult(task, programSet2, false);
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x000690F4 File Offset: 0x000672F4
		private void ValidateResultSoundness(LearningTask task, Optional<ProgramSet> learned)
		{
			if (!this.Configuration.UseDynamicSoundnessCheck || task.Spec.IsExemptFromDynamicSoundnessCheck || ProgramSet.IsNullOrEmpty(learned.Value))
			{
				return;
			}
			ProgramSet programSet;
			ProgramSet programSet2;
			learned.Value.PartitionByValidity(task.Spec, out programSet, out programSet2);
			if (ProgramSet.IsNullOrEmpty(programSet2))
			{
				return;
			}
			Trace.WriteLine(FormattableString.Invariant(FormattableStringFactory.Create("{0} of {1} programs are not valid. Here's one:", new object[]
			{
				programSet2.Size,
				learned.Value.Size
			})));
			ProgramNode programNode = programSet2.RealizedPrograms.First<ProgramNode>();
			foreach (State state in task.Spec.ProvidedInputs)
			{
				object obj = programNode.Invoke(state);
				if (!task.Spec.Valid(state, obj))
				{
					Trace.WriteLine(FormattableString.Invariant(FormattableStringFactory.Create("{0} transforms {1} to {2}", new object[] { programNode, state, obj })));
				}
			}
			throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Unsound {0} result for {1}!", new object[] { task.Symbol, task.Spec })));
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x00069248 File Offset: 0x00067448
		public void ClearLearningCache()
		{
			lock (this)
			{
				this._learningCache.Clear();
			}
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x00069288 File Offset: 0x00067488
		private ProgramSet LogResult(LearningTask task, ProgramSet result, bool retrievedFromCache = false)
		{
			if (this.Configuration.LogListener != null)
			{
				if (this.Configuration.LogListener.Includes(LogInfo.ProgramSets))
				{
					this.Configuration.LogListener.CurrentEvent["result"] = result;
				}
				this.Configuration.LogListener.CurrentEvent["retrievedFromCache"] = retrievedFromCache;
				this.Configuration.LogListener.ExitEvent();
			}
			return result;
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x00069304 File Offset: 0x00067504
		private TResult InvokeStrategy<TResult>(Func<TResult> learner)
		{
			if (!this.Configuration.UseThreads)
			{
				return learner();
			}
			Task<TResult> task = Task.Factory.StartNew<TResult>(learner, TaskCreationOptions.LongRunning);
			if (task.Wait(this.Configuration.SynthesisTimeout) && task.Status == TaskStatus.RanToCompletion)
			{
				return task.Result;
			}
			return default(TResult);
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x00069360 File Offset: 0x00067560
		private void TrimCacheIfNecessary()
		{
			lock (this)
			{
				if (this._learningCache.Count > this.Configuration.CacheSize)
				{
					int num = this._cacheUseFrequency.Keys.First<int>();
					foreach (LearningTask learningTask in this._cacheUseFrequency[num])
					{
						this._learningCache.Remove(learningTask);
					}
					this._cacheUseFrequency.Remove(num);
				}
			}
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x00069420 File Offset: 0x00067620
		public IEnumerable<Spec> GetCachedSpecs()
		{
			IEnumerable<Spec> enumerable;
			lock (this)
			{
				enumerable = this._learningCache.Select((KeyValuePair<LearningTask, SynthesisEngine.CacheEntry> kvp) => kvp.Key.Spec);
			}
			return enumerable;
		}

		// Token: 0x040011DD RID: 4573
		private readonly Dictionary<LearningTask, SynthesisEngine.CacheEntry> _learningCache = new Dictionary<LearningTask, SynthesisEngine.CacheEntry>();

		// Token: 0x040011DE RID: 4574
		private readonly SortedDictionary<int, HashSet<LearningTask>> _cacheUseFrequency = new SortedDictionary<int, HashSet<LearningTask>>();

		// Token: 0x020006C7 RID: 1735
		public sealed class Config : StrategyConfig
		{
			// Token: 0x170006A0 RID: 1696
			// (get) Token: 0x060025BA RID: 9658 RVA: 0x00069484 File Offset: 0x00067684
			// (set) Token: 0x060025BB RID: 9659 RVA: 0x0006948C File Offset: 0x0006768C
			public bool UseThreads { get; set; }

			// Token: 0x170006A1 RID: 1697
			// (get) Token: 0x060025BC RID: 9660 RVA: 0x00069495 File Offset: 0x00067695
			// (set) Token: 0x060025BD RID: 9661 RVA: 0x0006949D File Offset: 0x0006769D
			public ISynthesisStrategy[] Strategies { get; set; } = new ISynthesisStrategy[]
			{
				new EnumerativeSynthesis(null),
				new DeductiveSynthesis(null),
				new ComponentBasedSynthesis(null, null, null, null)
			};

			// Token: 0x170006A2 RID: 1698
			// (get) Token: 0x060025BE RID: 9662 RVA: 0x000694A6 File Offset: 0x000676A6
			// (set) Token: 0x060025BF RID: 9663 RVA: 0x000694AE File Offset: 0x000676AE
			public TimeSpan SynthesisTimeout { get; set; } = (Debugger.IsAttached ? Timeout.InfiniteTimeSpan : TimeSpan.FromSeconds(7.0));

			// Token: 0x170006A3 RID: 1699
			// (get) Token: 0x060025C0 RID: 9664 RVA: 0x000694B7 File Offset: 0x000676B7
			// (set) Token: 0x060025C1 RID: 9665 RVA: 0x000694BF File Offset: 0x000676BF
			public int CacheSize { get; set; } = 2048;

			// Token: 0x170006A4 RID: 1700
			// (get) Token: 0x060025C2 RID: 9666 RVA: 0x000694C8 File Offset: 0x000676C8
			// (set) Token: 0x060025C3 RID: 9667 RVA: 0x000694D0 File Offset: 0x000676D0
			public bool UseDynamicSoundnessCheck { get; set; }

			// Token: 0x060025C4 RID: 9668 RVA: 0x000694DC File Offset: 0x000676DC
			public SynthesisEngine.Config Clone()
			{
				return new SynthesisEngine.Config
				{
					UseThreads = this.UseThreads,
					Strategies = this.Strategies.ToArray<ISynthesisStrategy>(),
					SynthesisTimeout = this.SynthesisTimeout,
					CacheSize = this.CacheSize,
					UseDynamicSoundnessCheck = this.UseDynamicSoundnessCheck,
					LogListener = base.LogListener
				};
			}
		}

		// Token: 0x020006C8 RID: 1736
		private class CacheEntry
		{
			// Token: 0x040011E4 RID: 4580
			public int UseFrequency;

			// Token: 0x040011E5 RID: 4581
			public ProgramSet Programs;
		}
	}
}
