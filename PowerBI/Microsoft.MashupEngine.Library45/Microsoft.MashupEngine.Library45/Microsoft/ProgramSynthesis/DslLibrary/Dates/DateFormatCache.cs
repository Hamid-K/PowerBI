using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x0200081C RID: 2076
	public class DateFormatCache : ICachefulObject<DateFormatCache>, ICachefulObject
	{
		// Token: 0x06002CB9 RID: 11449 RVA: 0x0007DDA3 File Offset: 0x0007BFA3
		internal DateFormatCache(StringLearningCache cache, DateFormatCache.Settings settings)
		{
			this._cache = cache;
			this._heuristics = Heuristics.For(settings.Mode);
			this._settings = settings;
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x0007DDD8 File Offset: 0x0007BFD8
		private DateFormatCache(DateFormatCache other)
		{
			this._cache = other._cache;
			this._initialized = other._initialized;
			this._heuristics = other._heuristics;
			if (!this._initialized)
			{
				return;
			}
			lock (other)
			{
				UnboundedCache<StringRegion, UnboundedCache<DateFormatCache.ParseMode, IReadOnlyList<DateTimeFormatMatch>>> allFormatMatches = other._allFormatMatches;
				this._allFormatMatches = ((allFormatMatches != null) ? allFormatMatches.DeepClone() : null);
				UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch> dtMatches = other._dtMatches;
				this._dtMatches = ((dtMatches != null) ? dtMatches.DeepClone() : null);
				if (other._startingTokenMatches != null)
				{
					this._startingTokenMatches = new SortedDictionary<uint, UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch>>();
					foreach (KeyValuePair<uint, UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch>> keyValuePair in other._startingTokenMatches)
					{
						this._startingTokenMatches[keyValuePair.Key] = keyValuePair.Value.DeepClone();
					}
				}
				UnboundedCache<uint, UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch>> endingTokenMatches = other._endingTokenMatches;
				this._endingTokenMatches = ((endingTokenMatches != null) ? endingTokenMatches.DeepClone() : null);
				this._dtParses = other._dtParses.DeepClone();
			}
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x0007DF10 File Offset: 0x0007C110
		internal static DateFormatCache For(LearningCacheSubstring sr, DateFormatCache.Settings settings)
		{
			return sr.Cache.GetSpecializedCache<UnboundedCache<DateFormatCache.Settings, DateFormatCache>>((StringLearningCache _) => new ConcurrentUnboundedCache<DateFormatCache.Settings, DateFormatCache>()).GetOrAdd(settings, (DateFormatCache.Settings _) => new DateFormatCache(sr.Cache, settings));
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x0007DF77 File Offset: 0x0007C177
		internal static DateFormatCache For(LearningCacheSubstring sr, HeuristicsMode heuristicsMode)
		{
			return DateFormatCache.For(sr, new DateFormatCache.Settings(heuristicsMode));
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x0007DF88 File Offset: 0x0007C188
		private void Initialize()
		{
			if (this._initialized)
			{
				return;
			}
			lock (this)
			{
				if (!this._initialized)
				{
					Optional<DateTimeFormat.Target> target = this._settings.Target;
					Func<DateTimeFormat.Target, DateTimeFormatTranslator> func;
					if ((func = DateFormatCache.<>O.<0>__For) == null)
					{
						func = (DateFormatCache.<>O.<0>__For = new Func<DateTimeFormat.Target, DateTimeFormatTranslator>(DateTimeFormatTranslator.For));
					}
					this._translator = target.Select(func).OrElseDefault<DateTimeFormatTranslator>();
					this.InitializeBaseFormats();
					this.InitializeAllFormatMatches();
					this._initialized = true;
				}
			}
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x0007E01C File Offset: 0x0007C21C
		private void AddDateFormat(DateTimeFormat format)
		{
			lock (this)
			{
				IReadOnlyCollection<DateTimeFormatMatch> readOnlyCollection;
				if (!this._dtMatches.Lookup(format, out readOnlyCollection))
				{
					List<DateTimeFormatMatch> list = new List<DateTimeFormatMatch>();
					if (this._translator != null && ((format.FormatParts.Count > 1) ? format.FormatWith(this._translator, this._settings.Strict) : format.FormatParts[0].FormatStringFor(this._settings.Target.Value, this._settings.Strict)) == null)
					{
						this._dtMatches.Add(format, list);
					}
					else
					{
						StringRegion contentRegion = new StringRegion(this._cache);
						CachedList cachedList;
						if (format.Token == null)
						{
							list = format.AllParses(contentRegion).ToList<DateTimeFormatMatch>();
						}
						else if (this._cache.TryGetMatchPositionsFor(format.Token, out cachedList))
						{
							list.AddRange(from match in cachedList
								from parsed in format.AllParses(contentRegion.Slice(match.Position, match.Right))
								select parsed);
						}
						if (format.Token != null || format.FormatString == "o" || format.FormatString[0] == 'Z')
						{
							foreach (DateTimeFormatMatch dateTimeFormatMatch in list)
							{
								Heuristics heuristics = this._heuristics;
								DateTimeFormatMatch dateTimeFormatMatch2 = dateTimeFormatMatch;
								DateTimeFormatTranslator translator = this._translator;
								if (heuristics.IsReasonablePartMatch(dateTimeFormatMatch2, translator == null || translator.OrdinalDaySupported))
								{
									this._startingTokenMatches.GetOrAdd(dateTimeFormatMatch.Region.Start, (uint _) => UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch>.Create(null, null, null)).Add(format, dateTimeFormatMatch);
									this._endingTokenMatches.LookupOrCompute(dateTimeFormatMatch.Region.End, (uint _) => UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch>.Create(null, null, null)).Add(format, dateTimeFormatMatch);
								}
							}
						}
						this._dtMatches.Add(format, list);
					}
				}
			}
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x0007E2E8 File Offset: 0x0007C4E8
		public static Optional<DateTimeFormatMatch> Parse(DateTimeFormat format, LearningCacheSubstring sr)
		{
			return DateFormatCache.For(sr, new DateFormatCache.Settings(HeuristicsMode.AllowMostFormats))._Parse(format, (sr as StringRegion) ?? new StringRegion(sr.Cache).Slice(sr.Start, sr.End));
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x0007E324 File Offset: 0x0007C524
		private Optional<DateTimeFormatMatch> _Parse(DateTimeFormat format, StringRegion sr)
		{
			Optional<DateTimeFormatMatch> optional;
			lock (this)
			{
				IReadOnlyCollection<DateTimeFormatMatch> readOnlyCollection;
				if (this._dtMatches != null && this._dtMatches.Lookup(format, out readOnlyCollection))
				{
					optional = readOnlyCollection.MaybeFirst((DateTimeFormatMatch m) => object.Equals(sr, m.Region));
				}
				else
				{
					Record<DateTimeFormat, StringRegion> record = Record.Create<DateTimeFormat, StringRegion>(format, sr);
					Optional<DateTimeFormatMatch> optional2;
					if (!this._dtParses.Lookup(record, out optional2))
					{
						optional2 = format.MaybeParse(sr);
						this._dtParses.Add(record, optional2);
					}
					optional = optional2;
				}
			}
			return optional;
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x0007E3D8 File Offset: 0x0007C5D8
		public static IReadOnlyList<DateTimeFormatMatch> AllFormatMatchesFor(string dateTimeString, DateFormatCache.ParseMode parseMode, DateFormatCache.Settings settings)
		{
			if (dateTimeString == null)
			{
				return new DateTimeFormatMatch[0];
			}
			return DateFormatCache.AllFormatMatchesFor(new StringRegion(dateTimeString, Token.DateTimeTokens), parseMode, settings);
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x0007E403 File Offset: 0x0007C603
		public static IReadOnlyList<DateTimeFormatMatch> AllFormatMatchesFor(string dateTimeString, DateFormatCache.ParseMode parseMode, HeuristicsMode heuristicsMode)
		{
			return DateFormatCache.AllFormatMatchesFor(dateTimeString, parseMode, new DateFormatCache.Settings(heuristicsMode));
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x0007E414 File Offset: 0x0007C614
		public static IReadOnlyList<DateTimeFormatMatch> AllFormatMatchesFor(LearningCacheSubstring sr, DateFormatCache.ParseMode parseMode, DateFormatCache.Settings settings)
		{
			if (sr == null)
			{
				return new DateTimeFormatMatch[0];
			}
			return DateFormatCache.For(sr, settings)._AllFormatMatchesFor(sr, parseMode);
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x0007E43B File Offset: 0x0007C63B
		public static IReadOnlyList<DateTimeFormatMatch> AllFormatMatchesFor(LearningCacheSubstring sr, DateFormatCache.ParseMode parseMode, HeuristicsMode heuristicsMode)
		{
			return DateFormatCache.AllFormatMatchesFor(sr, parseMode, new DateFormatCache.Settings(heuristicsMode));
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x0007E44C File Offset: 0x0007C64C
		private IReadOnlyList<DateTimeFormatMatch> _AllFormatMatchesFor(LearningCacheSubstring ss, DateFormatCache.ParseMode parseMode)
		{
			this.Initialize();
			bool flag = false;
			IReadOnlyList<DateTimeFormatMatch> readOnlyList2;
			try
			{
				Monitor.Enter(this, ref flag);
				StringRegion stringRegion = ss as StringRegion;
				StringRegion stringRegion2 = ((stringRegion != null) ? stringRegion.WholeRegion : null) ?? new StringRegion(ss.Cache);
				StringRegion sr = (ss as StringRegion) ?? stringRegion2.Slice(ss.Start, ss.End);
				IReadOnlyList<DateTimeFormatMatch> readOnlyList;
				if (this._allFormatMatches.LookupOrCreateValue(sr).Lookup(parseMode, out readOnlyList))
				{
					readOnlyList2 = readOnlyList;
				}
				else
				{
					IReadOnlyCollection<DateTimeFormatMatch> readOnlyCollection = this._allFormatMatches[stringRegion2][DateFormatCache.ParseMode.Partial];
					if (parseMode == DateFormatCache.ParseMode.FullLength)
					{
						readOnlyList = readOnlyCollection.Where((DateTimeFormatMatch m) => m.Region.Start == sr.Start && m.Region.End == sr.End).ToList<DateTimeFormatMatch>();
						this._allFormatMatches.LookupOrCreateValue(sr).Add(parseMode, readOnlyList);
						readOnlyList2 = readOnlyList;
					}
					else
					{
						IEnumerable<DateTimeFormatMatch> enumerable = readOnlyCollection.Where((DateTimeFormatMatch m) => m.Region.Start >= sr.Start && m.Region.End <= sr.End);
						if (parseMode == DateFormatCache.ParseMode.FullLengthAllowingEdgeConstants)
						{
							readOnlyList = enumerable.Collect(delegate(DateTimeFormatMatch match)
							{
								if (match.ParsedRegion == match.ParsedRegion.WholeRegion)
								{
									return match;
								}
								if (this._heuristics.AllowAnyNonNumericEdgeConstants)
								{
									List<DateTimeFormatPart> list = match.DateTimeFormat.AllFormatParts.ToList<DateTimeFormatPart>();
									if (match.ParsedRegion.Start != 0U)
									{
										StringRegion stringRegion3 = match.ParsedRegion.WholeRegion.Slice(0U, match.ParsedRegion.Start);
										if (char.IsDigit(stringRegion3.MaybeLastChar().Value) && char.IsDigit(match.ParsedRegion.MaybeFirstChar().Value))
										{
											return null;
										}
										list.Insert(0, new ConstantDateTimeFormatPart(stringRegion3));
									}
									if (match.ParsedRegion.End != match.ParsedRegion.WholeRegion.End)
									{
										StringRegion stringRegion4 = match.ParsedRegion.WholeRegion.Slice(match.ParsedRegion.End, match.ParsedRegion.WholeRegion.End);
										if (char.IsDigit(stringRegion4.MaybeFirstChar().Value) && char.IsDigit(match.ParsedRegion.MaybeLastChar().Value))
										{
											return null;
										}
										list.Add(new ConstantDateTimeFormatPart(stringRegion4));
									}
									return new DateTimeFormatMatch(match.ParsedRegion.WholeRegion, new DateTimeFormat(list), match.PartialDateTime);
								}
								if (match.ParsedRegion.Start != 0U)
								{
									StringRegion stringRegion5 = match.ParsedRegion.WholeRegion.Slice(0U, match.ParsedRegion.Start);
									match = this._heuristics.CombineWithIfReasonable(new DateTimeFormatMatch(stringRegion5, new DateTimeFormat(new DateTimeFormatPart[]
									{
										new ConstantDateTimeFormatPart(stringRegion5)
									}), PartialDateTime.Empty), match);
									if (match == null)
									{
										return null;
									}
								}
								if (match.ParsedRegion.End != match.ParsedRegion.WholeRegion.End)
								{
									StringRegion stringRegion6 = match.ParsedRegion.WholeRegion.Slice(match.ParsedRegion.End, match.ParsedRegion.WholeRegion.End);
									match = this._heuristics.CombineWithIfReasonable(match, new DateTimeFormatMatch(stringRegion6, new DateTimeFormat(new DateTimeFormatPart[]
									{
										new ConstantDateTimeFormatPart(stringRegion6)
									}), PartialDateTime.Empty));
									if (match == null)
									{
										return null;
									}
								}
								if (!this._heuristics.IsReasonableDateTimeMatch(match))
								{
									return null;
								}
								return match;
							}).ToList<DateTimeFormatMatch>();
						}
						else
						{
							if (parseMode != DateFormatCache.ParseMode.Partial)
							{
								throw new NotImplementedException("Unsupported ParseMode: " + parseMode.ToString());
							}
							readOnlyList = enumerable.ToList<DateTimeFormatMatch>();
						}
						this._allFormatMatches.LookupOrCreateValue(sr).Add(parseMode, readOnlyList);
						readOnlyList2 = readOnlyList;
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
			return readOnlyList2;
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x0007E5D4 File Offset: 0x0007C7D4
		private void InitializeAllFormatMatches()
		{
			lock (this)
			{
				StringRegion stringRegion = new StringRegion(this._cache);
				IReadOnlyList<DateTimeFormatMatch> readOnlyList = this.AllReasonableCombinedMatches(this._startingTokenMatches.Values.Select((UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch> matches) => matches.Values.SelectMany((IReadOnlyCollection<DateTimeFormatMatch> v) => v).ToList<DateTimeFormatMatch>())).ToList<DateTimeFormatMatch>();
				this._allFormatMatches.LookupOrCreateValue(stringRegion).Add(DateFormatCache.ParseMode.Partial, readOnlyList);
			}
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x0007E664 File Offset: 0x0007C864
		private IEnumerable<DateTimeFormatMatch> AllReasonableCombinedMatches(IEnumerable<ICollection<DateTimeFormatMatch>> matchParts)
		{
			return this.AllReasonableCombinedMatchesWithoutQuarter(matchParts).SelectMany(delegate(DateTimeFormatMatch m)
			{
				DateTimeFormatPart dateTimeFormatPart = m.DateTimeFormat.FormatParts.FirstOrDefault<DateTimeFormatPart>();
				if (!(((dateTimeFormatPart != null) ? dateTimeFormatPart.BaseFormatString : null) == "q") || m.Region.Start <= 0U || char.ToUpperInvariant(m.Region.Source[(int)(m.Region.Start - 1U)]) != 'Q')
				{
					return new DateTimeFormatMatch[] { m };
				}
				return new DateTimeFormatMatch[]
				{
					m,
					m.ExpandWithConstant(m.Region.WholeRegion.Slice(m.Region.Start - 1U, m.Region.End))
				};
			});
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x0007E694 File Offset: 0x0007C894
		private IEnumerable<DateTimeFormatMatch> AllReasonableCombinedMatchesWithoutQuarter(IEnumerable<ICollection<DateTimeFormatMatch>> matchParts)
		{
			List<DateTimeFormatMatch> list = new List<DateTimeFormatMatch>();
			IList<DateTimeFormatMatch> list2 = new List<DateTimeFormatMatch>();
			uint? noStringsEndingBefore = null;
			Func<DateTimeFormatMatch, bool> <>9__3;
			foreach (ICollection<DateTimeFormatMatch> collection in matchParts)
			{
				DateFormatCache.<>c__DisplayClass28_1 CS$<>8__locals2 = new DateFormatCache.<>c__DisplayClass28_1();
				CS$<>8__locals2.start = collection.First<DateTimeFormatMatch>().Region.Start;
				if (noStringsEndingBefore != null && CS$<>8__locals2.start > noStringsEndingBefore.Value)
				{
					noStringsEndingBefore = null;
				}
				uint? maxConstantLength = this._heuristics.MaxConstantLength;
				if (maxConstantLength != null)
				{
					CS$<>8__locals2.maxConstantLength = maxConstantLength.GetValueOrDefault();
					if (list2.Count > 0 && CS$<>8__locals2.start > CS$<>8__locals2.maxConstantLength)
					{
						IList<DateTimeFormatMatch> list3;
						IList<DateTimeFormatMatch> list4;
						list2.PartitionByPredicate(new Func<DateTimeFormatMatch, bool>(CS$<>8__locals2.<AllReasonableCombinedMatchesWithoutQuarter>g__Expires|0), out list3, out list4);
						list.AddRange(list3.Where(new Func<DateTimeFormatMatch, bool>(this._heuristics.IsReasonableDateTimeMatch)));
						list2 = list4;
					}
				}
				uint? num = collection.Where((DateTimeFormatMatch p) => p.DateTimeFormat.FormatParts.OnlyOrDefault<DateTimeFormatPart>() is StringDateTimeFormatPart).DefaultIfEmpty<DateTimeFormatMatch>().Max(delegate(DateTimeFormatMatch p)
				{
					if (p == null)
					{
						return null;
					}
					return new uint?(p.Region.End);
				});
				if (num != null)
				{
					noStringsEndingBefore = ((noStringsEndingBefore != null) ? new uint?(Math.Max(noStringsEndingBefore.Value, num.Value)) : num);
				}
				ICollection<DateTimeFormatMatch> collection2 = list2;
				ICollection<DateTimeFormatMatch> collection4;
				if (noStringsEndingBefore != null)
				{
					IEnumerable<DateTimeFormatMatch> enumerable = collection;
					Func<DateTimeFormatMatch, bool> func;
					if ((func = <>9__3) == null)
					{
						func = (<>9__3 = (DateTimeFormatMatch p) => !(p.DateTimeFormat.FormatParts.OnlyOrDefault<DateTimeFormatPart>() is StringDateTimeFormatPart) || p.Region.End >= noStringsEndingBefore.Value);
					}
					ICollection<DateTimeFormatMatch> collection3 = enumerable.Where(func).ToList<DateTimeFormatMatch>();
					collection4 = collection3;
				}
				else
				{
					collection4 = collection;
				}
				list2 = this.CombinePrefixes(collection2, collection4);
			}
			list.AddRange(list2.Where(new Func<DateTimeFormatMatch, bool>(this._heuristics.IsReasonableDateTimeMatch)));
			return list;
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x0007E8BC File Offset: 0x0007CABC
		private List<DateTimeFormatMatch> CombinePrefixes(ICollection<DateTimeFormatMatch> current, ICollection<DateTimeFormatMatch> part)
		{
			List<DateTimeFormatMatch> list = new List<DateTimeFormatMatch>(current.Count + part.Count);
			foreach (DateTimeFormatMatch dateTimeFormatMatch in current)
			{
				foreach (DateTimeFormatMatch dateTimeFormatMatch2 in part)
				{
					DateTimeFormatMatch dateTimeFormatMatch3 = this._heuristics.CombineWithIfReasonable(dateTimeFormatMatch, dateTimeFormatMatch2);
					if (dateTimeFormatMatch3 != null && this._heuristics.IsReasonableDateTimeMatchPrefix(dateTimeFormatMatch3))
					{
						list.Add(dateTimeFormatMatch3);
					}
				}
			}
			foreach (DateTimeFormatMatch dateTimeFormatMatch4 in part)
			{
				if (dateTimeFormatMatch4 != null && this._heuristics.IsReasonableDateTimeMatchPrefix(dateTimeFormatMatch4))
				{
					list.Add(dateTimeFormatMatch4);
				}
			}
			list.AddRange(current);
			return list;
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x0007E9CC File Offset: 0x0007CBCC
		public DateFormatCache CloneWithCurrentCacheState()
		{
			return new DateFormatCache(this);
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x0007E9D4 File Offset: 0x0007CBD4
		public void ClearCaches()
		{
			UnboundedCache<StringRegion, UnboundedCache<DateFormatCache.ParseMode, IReadOnlyList<DateTimeFormatMatch>>> allFormatMatches = this._allFormatMatches;
			if (allFormatMatches != null)
			{
				allFormatMatches.Clear();
			}
			this._dtParses.Clear();
			UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch> dtMatches = this._dtMatches;
			if (dtMatches != null)
			{
				dtMatches.Clear();
			}
			SortedDictionary<uint, UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch>> startingTokenMatches = this._startingTokenMatches;
			if (startingTokenMatches != null)
			{
				startingTokenMatches.Clear();
			}
			UnboundedCache<uint, UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch>> endingTokenMatches = this._endingTokenMatches;
			if (endingTokenMatches != null)
			{
				endingTokenMatches.Clear();
			}
			this._initialized = false;
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x0007EA37 File Offset: 0x0007CC37
		ICachefulObject ICachefulObject.CloneWithCurrentCacheState()
		{
			return this.CloneWithCurrentCacheState();
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x0007EA40 File Offset: 0x0007CC40
		private void InitializeBaseFormats()
		{
			if (this._baseFormatsAdded)
			{
				return;
			}
			lock (this)
			{
				if (!this._baseFormatsAdded)
				{
					this._allFormatMatches = new UnboundedCache<StringRegion, UnboundedCache<DateFormatCache.ParseMode, IReadOnlyList<DateTimeFormatMatch>>>(null, null, (UnboundedCache<DateFormatCache.ParseMode, IReadOnlyList<DateTimeFormatMatch>> c) => c.DeepClone());
					this._dtMatches = UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch>.Create(null, null, null);
					this._startingTokenMatches = new SortedDictionary<uint, UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch>>();
					this._endingTokenMatches = new UnboundedCache<uint, UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch>>(null, null, (UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch> c) => c.DeepClone());
					foreach (DateTimeFormat dateTimeFormat in DateFormatCache.Formats)
					{
						this.AddDateFormat(dateTimeFormat);
					}
					this._baseFormatsAdded = true;
				}
			}
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x0007EB24 File Offset: 0x0007CD24
		public IReadOnlyCollection<DateTimeFormatMatch> GetMatchesFor(DateTimeFormat format)
		{
			this.AddDateFormat(format);
			IReadOnlyCollection<DateTimeFormatMatch> readOnlyCollection = null;
			UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch> dtMatches = this._dtMatches;
			if (dtMatches != null && dtMatches.Lookup(format, out readOnlyCollection))
			{
				return readOnlyCollection;
			}
			return null;
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x0007EB54 File Offset: 0x0007CD54
		public IReadOnlyDictionary<DateTimeFormat, IReadOnlyCollection<DateTimeFormatMatch>> GetMatchesEndingAt(uint endPosition)
		{
			this.InitializeBaseFormats();
			UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch> unboundedMultiValueCache = null;
			UnboundedCache<uint, UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch>> endingTokenMatches = this._endingTokenMatches;
			if (endingTokenMatches != null && endingTokenMatches.Lookup(endPosition, out unboundedMultiValueCache))
			{
				return unboundedMultiValueCache.AsReadOnlyDictionary();
			}
			return null;
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x0007EB88 File Offset: 0x0007CD88
		public IReadOnlyDictionary<DateTimeFormat, IReadOnlyCollection<DateTimeFormatMatch>> GetMatchesStartingAt(uint startPosition)
		{
			this.InitializeBaseFormats();
			UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch> unboundedMultiValueCache = null;
			SortedDictionary<uint, UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch>> startingTokenMatches = this._startingTokenMatches;
			if (startingTokenMatches != null && startingTokenMatches.TryGetValue(startPosition, out unboundedMultiValueCache))
			{
				return unboundedMultiValueCache.AsReadOnlyDictionary();
			}
			return null;
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x0007EBBC File Offset: 0x0007CDBC
		public IReadOnlyCollection<DateTimeFormatMatch> GetFormatMatchesEndingAt(DateTimeFormat format, uint endPosition)
		{
			this.InitializeBaseFormats();
			this.AddDateFormat(format);
			IReadOnlyDictionary<DateTimeFormat, IReadOnlyCollection<DateTimeFormatMatch>> matchesEndingAt = this.GetMatchesEndingAt(endPosition);
			IReadOnlyCollection<DateTimeFormatMatch> readOnlyCollection = null;
			if (matchesEndingAt != null && matchesEndingAt.TryGetValue(format, out readOnlyCollection))
			{
				return readOnlyCollection;
			}
			return null;
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x0007EBF4 File Offset: 0x0007CDF4
		public IReadOnlyCollection<DateTimeFormatMatch> GetFormatMatchesStartingAt(DateTimeFormat format, uint startPosition)
		{
			this.InitializeBaseFormats();
			this.AddDateFormat(format);
			IReadOnlyDictionary<DateTimeFormat, IReadOnlyCollection<DateTimeFormatMatch>> matchesStartingAt = this.GetMatchesStartingAt(startPosition);
			IReadOnlyCollection<DateTimeFormatMatch> readOnlyCollection = null;
			if (matchesStartingAt != null && matchesStartingAt.TryGetValue(format, out readOnlyCollection))
			{
				return readOnlyCollection;
			}
			return null;
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x0007EC2C File Offset: 0x0007CE2C
		// Note: this type is marked as 'beforefieldinit'.
		static DateFormatCache()
		{
			Record<string, FormatAttributes>[] array = new Record<string, FormatAttributes>[38];
			array[0] = Record.Create<string, FormatAttributes>("d", null);
			array[1] = Record.Create<string, FormatAttributes>("dd", null);
			array[2] = Record.Create<string, FormatAttributes>("o", null);
			array[3] = Record.Create<string, FormatAttributes>("ddd", null);
			array[4] = Record.Create<string, FormatAttributes>("dddd", null);
			array[5] = Record.Create<string, FormatAttributes>("f", null);
			array[6] = Record.Create<string, FormatAttributes>("ff", null);
			array[7] = Record.Create<string, FormatAttributes>("fff", null);
			array[8] = Record.Create<string, FormatAttributes>("H", null);
			array[9] = Record.Create<string, FormatAttributes>("HH", null);
			array[10] = Record.Create<string, FormatAttributes>("h", null);
			array[11] = Record.Create<string, FormatAttributes>("hh", null);
			array[12] = Record.Create<string, FormatAttributes>("t", null);
			array[13] = Record.Create<string, FormatAttributes>("tt", null);
			int num = 14;
			string text = "t";
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["casing"] = "lower";
			array[num] = Record.Create<string, FormatAttributes>(text, new FormatAttributes(dictionary));
			int num2 = 15;
			string text2 = "tt";
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			dictionary2["casing"] = "lower";
			array[num2] = Record.Create<string, FormatAttributes>(text2, new FormatAttributes(dictionary2));
			array[16] = Record.Create<string, FormatAttributes>("m", null);
			array[17] = Record.Create<string, FormatAttributes>("mm", null);
			array[18] = Record.Create<string, FormatAttributes>("M", null);
			array[19] = Record.Create<string, FormatAttributes>("MM", null);
			array[20] = Record.Create<string, FormatAttributes>("MMM", null);
			array[21] = Record.Create<string, FormatAttributes>("MMMM", null);
			array[22] = Record.Create<string, FormatAttributes>("s", null);
			array[23] = Record.Create<string, FormatAttributes>("ss", null);
			array[24] = Record.Create<string, FormatAttributes>("yy", null);
			array[25] = Record.Create<string, FormatAttributes>("yyyy", null);
			array[26] = Record.Create<string, FormatAttributes>("q", null);
			array[27] = Record.Create<string, FormatAttributes>("j", null);
			array[28] = Record.Create<string, FormatAttributes>("jjj", null);
			array[29] = Record.Create<string, FormatAttributes>("i", null);
			array[30] = Record.Create<string, FormatAttributes>("V", null);
			array[31] = Record.Create<string, FormatAttributes>("VV", null);
			array[32] = Record.Create<string, FormatAttributes>("YY", null);
			array[33] = Record.Create<string, FormatAttributes>("YYYY", null);
			array[34] = Record.Create<string, FormatAttributes>("Z", null);
			array[35] = Record.Create<string, FormatAttributes>("ZZ", null);
			array[36] = Record.Create<string, FormatAttributes>("Z", DateTimeFormatPart.NumericZeroFormatAttributes);
			array[37] = Record.Create<string, FormatAttributes>("ZZ", DateTimeFormatPart.NumericZeroFormatAttributes);
			DateFormatCache.Formats = array.Select((Record<string, FormatAttributes> t) => new DateTimeFormat(new DateTimeFormatPart[] { DateTimeFormatPart.Create(t.Item1, t.Item2) })).ToArray<DateTimeFormat>();
		}

		// Token: 0x04001560 RID: 5472
		private static readonly DateTimeFormat[] Formats;

		// Token: 0x04001561 RID: 5473
		private UnboundedCache<StringRegion, UnboundedCache<DateFormatCache.ParseMode, IReadOnlyList<DateTimeFormatMatch>>> _allFormatMatches;

		// Token: 0x04001562 RID: 5474
		private readonly StringLearningCache _cache;

		// Token: 0x04001563 RID: 5475
		private UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch> _dtMatches;

		// Token: 0x04001564 RID: 5476
		private readonly UnboundedCache<Record<DateTimeFormat, StringRegion>, Optional<DateTimeFormatMatch>> _dtParses = new UnboundedCache<Record<DateTimeFormat, StringRegion>, Optional<DateTimeFormatMatch>>();

		// Token: 0x04001565 RID: 5477
		private UnboundedCache<uint, UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch>> _endingTokenMatches;

		// Token: 0x04001566 RID: 5478
		private SortedDictionary<uint, UnboundedMultiValueCache<DateTimeFormat, DateTimeFormatMatch>> _startingTokenMatches;

		// Token: 0x04001567 RID: 5479
		private readonly Heuristics _heuristics;

		// Token: 0x04001568 RID: 5480
		private readonly DateFormatCache.Settings _settings;

		// Token: 0x04001569 RID: 5481
		private DateTimeFormatTranslator _translator;

		// Token: 0x0400156A RID: 5482
		private bool _initialized;

		// Token: 0x0400156B RID: 5483
		private bool _baseFormatsAdded;

		// Token: 0x0200081D RID: 2077
		public enum ParseMode
		{
			// Token: 0x0400156D RID: 5485
			Partial,
			// Token: 0x0400156E RID: 5486
			FullLength,
			// Token: 0x0400156F RID: 5487
			FullLengthAllowingEdgeConstants
		}

		// Token: 0x0200081E RID: 2078
		public struct Settings
		{
			// Token: 0x06002CD5 RID: 11477 RVA: 0x0007F1B1 File Offset: 0x0007D3B1
			public Settings(HeuristicsMode mode, Optional<DateTimeFormat.Target> target, bool strict)
			{
				this.Mode = mode;
				this.Target = target;
				this.Strict = strict;
			}

			// Token: 0x06002CD6 RID: 11478 RVA: 0x0007F1C8 File Offset: 0x0007D3C8
			public Settings(HeuristicsMode mode, DateTimeFormat.Target target, bool strict)
			{
				this = new DateFormatCache.Settings(mode, target.Some<DateTimeFormat.Target>(), strict);
			}

			// Token: 0x06002CD7 RID: 11479 RVA: 0x0007F1D8 File Offset: 0x0007D3D8
			public Settings(HeuristicsMode mode)
			{
				this = new DateFormatCache.Settings(mode, Optional<DateTimeFormat.Target>.Nothing, false);
			}

			// Token: 0x170007CC RID: 1996
			// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x0007F1E7 File Offset: 0x0007D3E7
			public readonly HeuristicsMode Mode { get; }

			// Token: 0x170007CD RID: 1997
			// (get) Token: 0x06002CD9 RID: 11481 RVA: 0x0007F1EF File Offset: 0x0007D3EF
			public readonly Optional<DateTimeFormat.Target> Target { get; }

			// Token: 0x170007CE RID: 1998
			// (get) Token: 0x06002CDA RID: 11482 RVA: 0x0007F1F7 File Offset: 0x0007D3F7
			public readonly bool Strict { get; }
		}

		// Token: 0x0200081F RID: 2079
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001573 RID: 5491
			public static Func<DateTimeFormat.Target, DateTimeFormatTranslator> <0>__For;
		}
	}
}
