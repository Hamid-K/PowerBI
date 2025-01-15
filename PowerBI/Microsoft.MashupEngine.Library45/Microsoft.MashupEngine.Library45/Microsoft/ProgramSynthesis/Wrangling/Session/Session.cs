using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Logging;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.Session
{
	// Token: 0x02000127 RID: 295
	[DataContract]
	public abstract class Session<TProgram, TInput, TOutput> where TProgram : Program<TInput, TOutput>
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00015280 File Offset: 0x00013480
		public static JsonSerializerSettings JsonSerializerSettings { get; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000685 RID: 1669
		protected abstract JsonSerializerSettings JsonSerializerSettingsInstance { get; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00015287 File Offset: 0x00013487
		public IProgramLoader<TProgram, TInput, TOutput> ProgramLoader { get; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001528F File Offset: 0x0001348F
		public IJournalStorage JournalStorage { get; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00015297 File Offset: 0x00013497
		public CultureInfo Culture { get; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001529F File Offset: 0x0001349F
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x000152A7 File Offset: 0x000134A7
		public bool UseInputsInLearn { get; set; } = true;

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x000152B0 File Offset: 0x000134B0
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x000152B8 File Offset: 0x000134B8
		public double ConfidenceThreshold { get; set; } = 0.47;

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x000152C4 File Offset: 0x000134C4
		public virtual DeserializationContext DeserializationContext
		{
			get
			{
				return default(DeserializationContext);
			}
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000152DC File Offset: 0x000134DC
		protected Session(IProgramLoader<TProgram, TInput, TOutput> loader, IJournalStorage journalStorage, CultureInfo culture, ILogger logger = null)
		{
			this.ProgramLoader = loader;
			this.JournalStorage = journalStorage;
			this.Culture = culture ?? CultureInfo.InvariantCulture;
			this.SetLogger(logger);
			this.Constraints.Changed += this.UpdateConflicts;
			this.Constraints.Changed += delegate(object sender, CollectionEvent<Constraint<TInput, TOutput>> args)
			{
				if (args.Action == CollectionAction.PreAdd && args.ChangedItems.Contains(null))
				{
					throw new ArgumentException("null is not a valid constraint.");
				}
			};
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00015392 File Offset: 0x00013592
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x0001539A File Offset: 0x0001359A
		public ILogger Logger { get; private set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x000153A3 File Offset: 0x000135A3
		[DataMember]
		public NotifyingCollection<TInput> Inputs { get; } = new NotifyingCollection<TInput>();

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x000153AB File Offset: 0x000135AB
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x000153B3 File Offset: 0x000135B3
		[DataMember]
		public RankingMode RankingMode { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x000153BC File Offset: 0x000135BC
		[DataMember]
		public NotifyingCollection<Constraint<TInput, TOutput>> Constraints { get; } = new NotifyingCollection<Constraint<TInput, TOutput>>();

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x000153C4 File Offset: 0x000135C4
		public IReadOnlyCollection<Conflict> Conflicts
		{
			get
			{
				return this._conflicts.ToImmutable();
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x000153D1 File Offset: 0x000135D1
		public bool CanLearn
		{
			get
			{
				return !this.Conflicts.Any<Conflict>();
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000153E1 File Offset: 0x000135E1
		public void SetLogger(ILogger logger)
		{
			this.Logger = logger ?? new NullLogger();
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x000153F3 File Offset: 0x000135F3
		public TProgram Load(string serializedProgram, ASTSerializationFormat serializationFormat = ASTSerializationFormat.XML)
		{
			return this.ProgramLoader.Load(serializedProgram, serializationFormat, this.DeserializationContext);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00015408 File Offset: 0x00013608
		public TProgram Load(string serializedProgram, ASTSerializationFormat serializationFormat, ProgramNodeParser programNodeParser)
		{
			return this.ProgramLoader.Load(serializedProgram, serializationFormat, this.DeserializationContext, programNodeParser);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00015420 File Offset: 0x00013620
		private void UpdateConflicts(object sender, CollectionEvent<Constraint<TInput, TOutput>> args)
		{
			switch (args.Action)
			{
			case CollectionAction.PreAdd:
				return;
			case CollectionAction.Add:
			{
				IReadOnlyList<Constraint<TInput, TOutput>> newConstraints = args.ChangedItems;
				IEnumerable<Constraint<TInput, TOutput>> enumerable = this.Constraints.Except(newConstraints);
				this._conflicts.AddRange(from existingConstraint in enumerable
					from newConstraint in newConstraints
					where existingConstraint.ConflictsWith(newConstraint) || newConstraint.ConflictsWith(existingConstraint)
					select new Conflict(new IConstraint[] { existingConstraint, newConstraint }));
				this._conflicts.AddRange(from index in Enumerable.Range(0, newConstraints.Count)
					from otherConstraint in newConstraints.Skip(index + 1)
					where newConstraints[index].ConflictsWith(otherConstraint) || otherConstraint.ConflictsWith(newConstraints[index])
					select new Conflict(new IConstraint[]
					{
						newConstraints[index],
						otherConstraint
					}));
				return;
			}
			case CollectionAction.Remove:
			{
				IReadOnlyList<Constraint<TInput, TOutput>> removedConstraints = args.ChangedItems;
				this._conflicts.ExceptWith(this._conflicts.Where((Conflict conflict) => conflict.ConflictingConstraints.Overlaps(removedConstraints)));
				return;
			}
			case CollectionAction.Reset:
				this._conflicts.Clear();
				return;
			}
			throw new NotImplementedException("Unexpected NotifyCollectionChangedAction: " + args.Action.ToString());
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x000155CD File Offset: 0x000137CD
		protected IEnumerable<TInput> InputsAndConstraintInputs
		{
			get
			{
				return this.Inputs.Concat(from c in this.Constraints.OfType<ConstraintOnInput<TInput, TOutput>>()
					select c.Input);
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001560C File Offset: 0x0001380C
		protected virtual LearnProgramRequest<TProgram, TInput, TOutput> BuildLearnProgramRequest(bool? useInputsInLearn = null)
		{
			if (!this.CanLearn)
			{
				return null;
			}
			IImmutableList<TInput> immutableList;
			if (!(useInputsInLearn ?? this.UseInputsInLearn))
			{
				IImmutableList<TInput> empty = ImmutableList<TInput>.Empty;
				immutableList = empty;
			}
			else
			{
				immutableList = this.Inputs.AsImmutable();
			}
			return new LearnProgramRequest<TProgram, TInput, TOutput>(immutableList, this.Constraints.AsImmutable());
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600069D RID: 1693
		public abstract string LoggingTypeName { get; }

		// Token: 0x0600069E RID: 1694 RVA: 0x00015664 File Offset: 0x00013864
		protected virtual IEnumerable<KeyValuePair<string, double>> TrackedLearningMetrics(LearnProgramRequest<TProgram, TInput, TOutput> request, TProgram topProgram)
		{
			KeyValuePair<string, double>[] array = new KeyValuePair<string, double>[3];
			int num = 0;
			string text = "NumConstraints";
			int? num2 = ((request != null) ? new int?(request.Constraints.Count) : null);
			array[num] = KVP.Create<string, double>(text, ((num2 != null) ? new double?((double)num2.GetValueOrDefault()) : null).GetValueOrDefault());
			int num3 = 1;
			string text2 = "NumInputs";
			int? num4 = ((request != null) ? new int?(request.Inputs.Count) : null);
			array[num3] = KVP.Create<string, double>(text2, ((num4 != null) ? new double?((double)num4.GetValueOrDefault()) : null).GetValueOrDefault());
			array[2] = KVP.Create<string, double>("NumConflicts", (double)((request == null) ? this.Conflicts.Count : 0));
			return array;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00015752 File Offset: 0x00013952
		protected virtual IEnumerable<KeyValuePair<string, string>> TrackedProperties()
		{
			return new KeyValuePair<string, string>[]
			{
				KVP.Create<string, string>("ProseVersion", Session<TProgram, TInput, TOutput>.ProseVersion),
				KVP.Create<string, string>("SessionType", this.LoggingTypeName)
			};
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00015787 File Offset: 0x00013987
		protected virtual IEnumerable<KeyValuePair<string, string>> TrackedLearningProperties(LearnProgramRequest<TProgram, TInput, TOutput> request, TProgram topProgram)
		{
			return this.TrackedProperties();
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00015790 File Offset: 0x00013990
		protected virtual IEnumerable<KeyValuePair<string, string>> TrackedLearningUserProperties(LearnProgramRequest<TProgram, TInput, TOutput> request, TProgram topProgram, bool includeConstraints = true)
		{
			KeyValuePair<string, string> keyValuePair = KVP.Create<string, string>("Conflicts", (request == null) ? JsonConvert.SerializeObject(this.Conflicts, this.JsonSerializerSettingsInstance) : null);
			if (includeConstraints)
			{
				return new KeyValuePair<string, string>[]
				{
					KVP.Create<string, string>("Constraints", (((request != null) ? request.Constraints : null) == null) ? null : JsonConvert.SerializeObject(request.Constraints, this.JsonSerializerSettingsInstance)),
					keyValuePair
				};
			}
			return new KeyValuePair<string, string>[] { keyValuePair };
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00015818 File Offset: 0x00013A18
		private void LogLearnTopK(LearnProgramRequest<TProgram, TInput, TOutput> request, int k, IReadOnlyList<TProgram> result, TimeSpan time, Guid? guid)
		{
			int num = ((result != null) ? result.Count : 0);
			bool flag = num > 0;
			TProgram tprogram = ((result != null) ? result.FirstOrDefault<TProgram>() : default(TProgram));
			KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[2];
			array[0] = KVP.Create<string, string>("Success", flag ? "True" : "False");
			int num2 = 1;
			string text = "TopProgramAnonymized";
			TProgram tprogram2 = tprogram;
			array[num2] = KVP.Create<string, string>(text, (tprogram2 != null) ? tprogram2.SerializeAnonymized(ASTSerializationFormat.XML) : null);
			IEnumerable<KeyValuePair<string, string>> enumerable = array.Concat(this.TrackedLearningProperties(request, tprogram));
			if (guid != null)
			{
				enumerable = enumerable.AppendItem(KVP.Create<string, string>("Id", guid.Value.ToString()));
			}
			ILogger logger = this.Logger;
			string text2 = "LearnTopK";
			IReadOnlyCollection<KeyValuePair<string, double>> readOnlyCollection = new KeyValuePair<string, double>[]
			{
				KVP.Create<string, double>("LearnTime", time.TotalMilliseconds),
				KVP.Create<string, double>("NumProgramsRequested", (double)k),
				KVP.Create<string, double>("NumProgramsLearned", (double)num)
			}.Concat(this.TrackedLearningMetrics(request, tprogram)).ToArray<KeyValuePair<string, double>>();
			IReadOnlyCollection<KeyValuePair<string, string>> readOnlyCollection2 = enumerable.ToArray<KeyValuePair<string, string>>();
			KeyValuePair<string, string>[] array2 = new KeyValuePair<string, string>[1];
			int num3 = 0;
			string text3 = "TopProgram";
			TProgram tprogram3 = tprogram;
			array2[num3] = KVP.Create<string, string>(text3, (tprogram3 != null) ? tprogram3.Serialize(ASTSerializationFormat.XML) : null);
			logger.TrackEvent(text2, readOnlyCollection, readOnlyCollection2, array2.Concat(this.TrackedLearningUserProperties(request, tprogram, true)).ToArray<KeyValuePair<string, string>>());
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00015980 File Offset: 0x00013B80
		private void LogLearnAll(LearnProgramRequest<TProgram, TInput, TOutput> request, Session<TProgram, TInput, TOutput>.IProgramSetWrapper result, TimeSpan time, Guid? guid)
		{
			IEnumerable<KeyValuePair<string, string>> enumerable = new KeyValuePair<string, string>[] { KVP.Create<string, string>("Success", (result != null) ? "True" : "False") }.Concat(this.TrackedLearningProperties(request, default(TProgram)));
			if (guid != null)
			{
				enumerable = enumerable.AppendItem(KVP.Create<string, string>("Id", guid.Value.ToString()));
			}
			this.Logger.TrackEvent("LearnAll", new KeyValuePair<string, double>[]
			{
				KVP.Create<string, double>("LearnTime", time.TotalMilliseconds),
				KVP.Create<string, double>("NumProgramsLearned", (result == null) ? 0.0 : ((double)result.ProgramSet.Size))
			}.Concat(this.TrackedLearningMetrics(request, default(TProgram))).ToArray<KeyValuePair<string, double>>(), enumerable.ToArray<KeyValuePair<string, string>>(), this.TrackedLearningUserProperties(request, default(TProgram), true).ToArray<KeyValuePair<string, string>>());
		}

		// Token: 0x060006A4 RID: 1700
		internal abstract Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper LearnTopKRandomKCached(LearnProgramRequest<TProgram, TInput, TOutput> request, RankingMode rankingMode, int k, int? randomK, bool cachedOnly, CancellationToken cancel);

		// Token: 0x060006A5 RID: 1701 RVA: 0x00015A8C File Offset: 0x00013C8C
		public TProgram Learn(RankingMode rankingMode = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			return this.LearnTopK(1, rankingMode, cancel, guid).FirstOrDefault<TProgram>();
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00015A9D File Offset: 0x00013C9D
		public Task<TProgram> LearnAsync(RankingMode rankingMode = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			return this.LearnTopKAsync(1, rankingMode, cancel, guid).ContinueWith<TProgram>(delegate(Task<IReadOnlyList<TProgram>> task)
			{
				IReadOnlyList<TProgram> result = task.Result;
				if (result == null)
				{
					return default(TProgram);
				}
				return result.FirstOrDefault<TProgram>();
			}, cancel);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00015AD0 File Offset: 0x00013CD0
		public IReadOnlyList<TProgram> LearnTopK(int k, RankingMode rankingMode = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			IReadOnlyList<TProgram> readOnlyList = null;
			LearnProgramRequest<TProgram, TInput, TOutput> learnProgramRequest = this.BuildLearnProgramRequest(null);
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				if (learnProgramRequest == null)
				{
					readOnlyList = new TProgram[0];
				}
				else
				{
					if (rankingMode == null)
					{
						rankingMode = this.RankingMode ?? RankingMode.MostLikely;
					}
					Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper prunedProgramSetWrapper = this.LearnTopKRandomKCached(learnProgramRequest, rankingMode, k, null, false, cancel);
					readOnlyList = ((prunedProgramSetWrapper != null) ? prunedProgramSetWrapper.RealizedPrograms.ToList<TProgram>() : null);
				}
			}
			finally
			{
				stopwatch.Stop();
				this.LogLearnTopK(learnProgramRequest, k, readOnlyList, stopwatch.Elapsed, guid);
			}
			return readOnlyList;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00015B70 File Offset: 0x00013D70
		public Task<IReadOnlyList<TProgram>> LearnTopKAsync(int k, RankingMode rankingMode = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			LearnProgramRequest<TProgram, TInput, TOutput> request = this.BuildLearnProgramRequest(null);
			if (request == null)
			{
				TProgram[] array = new TProgram[0];
				this.LogLearnTopK(null, k, array, TimeSpan.Zero, guid);
				return new TaskCompletionSource<IReadOnlyList<TProgram>>(array).Task;
			}
			if (rankingMode == null)
			{
				rankingMode = this.RankingMode ?? RankingMode.MostLikely;
			}
			return Task.Run<IReadOnlyList<TProgram>>(delegate
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				IReadOnlyList<TProgram> readOnlyList = null;
				IReadOnlyList<TProgram> readOnlyList2;
				try
				{
					Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper prunedProgramSetWrapper = this.LearnTopKRandomKCached(request, rankingMode, k, null, false, cancel);
					readOnlyList = ((prunedProgramSetWrapper != null) ? prunedProgramSetWrapper.RealizedPrograms.ToList<TProgram>() : null);
					readOnlyList2 = readOnlyList;
				}
				finally
				{
					stopwatch.Stop();
					this.LogLearnTopK(request, k, readOnlyList, stopwatch.Elapsed, guid);
				}
				return readOnlyList2;
			}, cancel);
		}

		// Token: 0x060006A9 RID: 1705
		protected abstract Session<TProgram, TInput, TOutput>.IProgramSetWrapper LearnAllCached(LearnProgramRequest<TProgram, TInput, TOutput> request, bool cachedOnly, CancellationToken cancel);

		// Token: 0x060006AA RID: 1706 RVA: 0x00015C2A File Offset: 0x00013E2A
		protected Session<TProgram, TInput, TOutput>.IProgramSetWrapper LearnAllCached(LearnProgramRequest<TProgram, TInput, TOutput> request, CancellationToken cancel)
		{
			return this.LearnAllCached(request, false, cancel);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00015C38 File Offset: 0x00013E38
		public Session<TProgram, TInput, TOutput>.IProgramSetWrapper LearnAll(CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			LearnProgramRequest<TProgram, TInput, TOutput> learnProgramRequest = this.BuildLearnProgramRequest(null);
			if (learnProgramRequest == null)
			{
				this.LogLearnAll(null, null, TimeSpan.Zero, guid);
				return null;
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			Session<TProgram, TInput, TOutput>.IProgramSetWrapper programSetWrapper = this.LearnAllCached(learnProgramRequest, cancel);
			stopwatch.Stop();
			this.LogLearnAll(learnProgramRequest, programSetWrapper, stopwatch.Elapsed, guid);
			return programSetWrapper;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00015C94 File Offset: 0x00013E94
		public Task<Session<TProgram, TInput, TOutput>.IProgramSetWrapper> LearnAllAsync(CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			LearnProgramRequest<TProgram, TInput, TOutput> request = this.BuildLearnProgramRequest(null);
			if (request == null)
			{
				this.LogLearnAll(null, null, TimeSpan.Zero, guid);
				return new TaskCompletionSource<Session<TProgram, TInput, TOutput>.IProgramSetWrapper>(null).Task;
			}
			return Task.Run<Session<TProgram, TInput, TOutput>.IProgramSetWrapper>(delegate
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				Session<TProgram, TInput, TOutput>.IProgramSetWrapper programSetWrapper = this.LearnAllCached(request, cancel);
				stopwatch.Stop();
				this.LogLearnAll(request, programSetWrapper, stopwatch.Elapsed, guid);
				return programSetWrapper;
			}, cancel);
		}

		// Token: 0x060006AD RID: 1709
		public abstract Task<IReadOnlyList<SignificantInput<TInput>>> GetSignificantInputsAsync(double? confidenceThreshold = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null);

		// Token: 0x060006AE RID: 1710
		public abstract IObservable<IReadOnlyList<SignificantInput<TInput>>> GetSignificantInputs(double? confidenceThreshold = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null);

		// Token: 0x060006AF RID: 1711
		public abstract Task<IReadOnlyList<TOutput>> ComputeTopKOutputsAsync(TInput input, int k, RankingMode rankingMode = null, double? confidenceThreshold = null, CancellationToken cancel = default(CancellationToken));

		// Token: 0x040002E2 RID: 738
		private static readonly Lazy<JsonSerializerSettings> LazyJsonSerializerSettings = new Lazy<JsonSerializerSettings>(() => new SessionJsonSerializerSettings<TProgram, TInput, TOutput>().Initialize());

		// Token: 0x040002E4 RID: 740
		private readonly ImmutableHashSet<Conflict>.Builder _conflicts = ImmutableHashSet<Conflict>.Empty.ToBuilder();

		// Token: 0x040002E9 RID: 745
		private const double DefaultConfidenceThreshold = 0.47;

		// Token: 0x040002EF RID: 751
		private static readonly string ProseVersion = typeof(Session<TProgram, TInput, TOutput>).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

		// Token: 0x02000128 RID: 296
		public interface IProgramSetWrapper
		{
			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x060006B1 RID: 1713
			IEnumerable<TProgram> RealizedPrograms { get; }

			// Token: 0x170001BA RID: 442
			// (get) Token: 0x060006B2 RID: 1714
			ProgramSet ProgramSet { get; }
		}

		// Token: 0x02000129 RID: 297
		internal class ProgramSetWrapper : Session<TProgram, TInput, TOutput>.IProgramSetWrapper
		{
			// Token: 0x060006B3 RID: 1715 RVA: 0x00015D54 File Offset: 0x00013F54
			public ProgramSetWrapper(ProgramSet programSet, Func<ProgramNode, TProgram> buildProgram)
			{
				this.ProgramSet = programSet;
				this.BuildProgram = buildProgram;
			}

			// Token: 0x170001BB RID: 443
			// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00015D6A File Offset: 0x00013F6A
			public ProgramSet ProgramSet { get; }

			// Token: 0x170001BC RID: 444
			// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00015D72 File Offset: 0x00013F72
			public IEnumerable<TProgram> RealizedPrograms
			{
				get
				{
					return this.ProgramSet.RealizedPrograms.Select(this.BuildProgram);
				}
			}

			// Token: 0x040002F0 RID: 752
			public readonly Func<ProgramNode, TProgram> BuildProgram;
		}

		// Token: 0x0200012A RID: 298
		protected internal class PrunedProgramSetWrapper : Session<TProgram, TInput, TOutput>.IProgramSetWrapper
		{
			// Token: 0x060006B6 RID: 1718 RVA: 0x00015D8A File Offset: 0x00013F8A
			public PrunedProgramSetWrapper(IReadOnlyList<TProgram> topPrograms, IReadOnlyList<TProgram> randomPrograms)
			{
				this._topPrograms = topPrograms;
				this._sampledPrograms = randomPrograms;
			}

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00015DA0 File Offset: 0x00013FA0
			public IEnumerable<TProgram> RealizedPrograms
			{
				get
				{
					return this._topPrograms;
				}
			}

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00015DA8 File Offset: 0x00013FA8
			public ProgramSet ProgramSet
			{
				get
				{
					return ProgramSet.List(this.RealizedPrograms.First<TProgram>().ProgramNode.Symbol, this.RealizedPrograms.Select((TProgram program) => program.ProgramNode));
				}
			}

			// Token: 0x040002F2 RID: 754
			internal readonly IReadOnlyList<TProgram> _topPrograms;

			// Token: 0x040002F3 RID: 755
			internal readonly IReadOnlyList<TProgram> _sampledPrograms;
		}
	}
}
