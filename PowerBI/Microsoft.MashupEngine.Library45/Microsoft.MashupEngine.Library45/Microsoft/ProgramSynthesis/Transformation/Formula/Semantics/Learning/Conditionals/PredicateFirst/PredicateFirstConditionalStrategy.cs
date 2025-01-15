using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Contracts;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.PredicateFirst.Models;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Text;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.PredicateFirst
{
	// Token: 0x0200173E RID: 5950
	public class PredicateFirstConditionalStrategy
	{
		// Token: 0x0600C59A RID: 50586 RVA: 0x002A7CF0 File Offset: 0x002A5EF0
		public PredicateFirstConditionalStrategy(IEnumerable<Example<IRow, object>> examples, IEnumerable<IRow> additionalInputs, Func<IEnumerable<Example<IRow, object>>, ConditionalBranchMeta> learnBranch, IEnumerable<string> columnNamePriority, bool enableMatch, int maxBranches, int branchMinExampleCount, LearnDebugTrace debugTrace = null)
		{
			this._examples = examples.ToReadOnlyList<Example<IRow, object>>();
			this._inputs = additionalInputs.ToReadOnlyList<IRow>();
			this._learnBranch = learnBranch;
			this._columnNamePriority = columnNamePriority.ToReadOnlyList<string>();
			this._enableMatch = enableMatch;
			this._maxBranches = maxBranches;
			this._branchMinExampleCount = branchMinExampleCount;
			if (debugTrace != null)
			{
				this._debugTrace = debugTrace;
				this._localDebugTrace = new PredicateFirstConditionalStrategy.LocalDebugTrace();
				if (debugTrace.RenderConditional == null)
				{
					debugTrace.RenderConditional = () => this._localDebugTrace.Render();
				}
			}
		}

		// Token: 0x0600C59B RID: 50587 RVA: 0x002A7D88 File Offset: 0x002A5F88
		public IEnumerable<IConditionalBranch[]> Paths()
		{
			this._allClusterExamples = this._examples.Select((Example<IRow, object> example, int index) => new ClusterExample
			{
				Position = index + 1,
				Example = example
			}).ToReadOnlyList<ClusterExample>();
			foreach (IConditionalBranch[] array in this.ResolvePaths())
			{
				IConditionalBranch[] array2 = array;
				IReadOnlyList<IConditionalBranch> readOnlyList = array2.Where((IConditionalBranch p) => !(p is NullConditionalBranch)).ToReadOnlyList<IConditionalBranch>();
				foreach (IConditionalBranch conditionalBranch in readOnlyList)
				{
					this.LearnProgram((Cluster)conditionalBranch);
					if (conditionalBranch.Program == null)
					{
						break;
					}
				}
				if (readOnlyList.Any((IConditionalBranch c) => c.Program == null))
				{
					PredicateFirstConditionalStrategy.LocalDebugTrace localDebugTrace = this._localDebugTrace;
					if (localDebugTrace != null)
					{
						localDebugTrace.FailedPaths.Add(array2);
					}
				}
				else
				{
					PredicateFirstConditionalStrategy.LocalDebugTrace localDebugTrace2 = this._localDebugTrace;
					if (localDebugTrace2 != null)
					{
						localDebugTrace2.Paths.Add(array2);
					}
					yield return array2;
				}
			}
			IEnumerator<Cluster[]> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600C59C RID: 50588 RVA: 0x002A7D98 File Offset: 0x002A5F98
		private void LearnProgram(Cluster cluster)
		{
			bool? learnFailed = cluster.LearnFailed;
			bool flag = true;
			if ((learnFailed.GetValueOrDefault() == flag) & (learnFailed != null))
			{
				return;
			}
			if (cluster.Program != null)
			{
				return;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("PredicateFirstConditionalStrategy", "LearnProgram", false, true) : null))
			{
				ConditionalBranchMeta conditionalBranchMeta = this._learnBranch(cluster.SourceExamples);
				cluster.Program = ((conditionalBranchMeta != null) ? conditionalBranchMeta.Program : null);
				if (cluster.Program == null)
				{
					cluster.LearnFailed = new bool?(true);
				}
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
			}
		}

		// Token: 0x0600C59D RID: 50589 RVA: 0x002A7E4C File Offset: 0x002A604C
		private IEnumerable<Cluster[]> ResolvePaths()
		{
			IReadOnlyList<ColumnDetail> readOnlyList = this._examples.InputColumnDetails(this._columnNamePriority).OrderBy(delegate(ColumnDetail c)
			{
				int? num3 = this._columnNamePriority.IndexOf(c.Name);
				if (num3 == null)
				{
					return 1000;
				}
				return num3.GetValueOrDefault();
			}).ToReadOnlyList<ColumnDetail>();
			int exampleCount = this._examples.Count;
			Func<Predicate, <>f__AnonymousType41<Predicate, IReadOnlyList<ClusterExample>>> <>9__2;
			Func<<>f__AnonymousType41<Predicate, IReadOnlyList<ClusterExample>>, bool> <>9__3;
			foreach (ColumnDetail columnDetail in readOnlyList)
			{
				PredicateFirstConditionalStrategy.<>c__DisplayClass16_1 CS$<>8__locals2 = new PredicateFirstConditionalStrategy.<>c__DisplayClass16_1();
				List<Predicate> list = this.ResolvePredicates(columnDetail).ToList<Predicate>();
				List<Predicate> list2 = list;
				Action<Predicate> action;
				if ((action = PredicateFirstConditionalStrategy.<>O.<0>__ComputeScore) == null)
				{
					action = (PredicateFirstConditionalStrategy.<>O.<0>__ComputeScore = new Action<Predicate>(PredicateFirstConditionalStrategy.ComputeScore));
				}
				list2.ForEach(action);
				NullConditionalBranch nullBranch = new NullConditionalBranch();
				CS$<>8__locals2.previousBranches = new HashSet<Cluster>();
				LearnDebugTrace debugTrace = this._debugTrace;
				List<Cluster> clusters;
				List<Cluster> branches;
				Dictionary<int, List<Cluster>> availableByDepth;
				using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("PredicateFirstConditionalStrategy", "ResolveClusters", false, true) : null))
				{
					CS$<>8__locals2.position = 1;
					IEnumerable<Predicate> enumerable = list.OrderByDescending((Predicate predicate) => predicate.Score);
					var func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (Predicate predicate) => new
						{
							predicate = predicate,
							clusterExamples = this._allClusterExamples.Where((ClusterExample clusterExample) => predicate.Evaluate(clusterExample.Input)).ToReadOnlyList<ClusterExample>()
						});
					}
					var enumerable2 = enumerable.Select(func);
					var func2;
					if ((func2 = <>9__3) == null)
					{
						func2 = (<>9__3 = <>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.clusterExamples.Any<ClusterExample>() && <>h__TransparentIdentifier0.clusterExamples.Count < exampleCount && <>h__TransparentIdentifier0.clusterExamples.Count >= this._branchMinExampleCount);
					}
					clusters = enumerable2.Where(func2).Select(delegate(<>h__TransparentIdentifier0)
					{
						Predicate predicate = <>h__TransparentIdentifier0.predicate;
						IEnumerable<ClusterExample> clusterExamples = <>h__TransparentIdentifier0.clusterExamples;
						int position = CS$<>8__locals2.position;
						CS$<>8__locals2.position = position + 1;
						return new Cluster(predicate, clusterExamples, position);
					}).ToList<Cluster>();
					TimedEvent timedEvent2 = timedEvent;
					if (timedEvent2 != null)
					{
						timedEvent2.Stop();
					}
					PredicateFirstConditionalStrategy.LocalDebugTrace localDebugTrace = this._localDebugTrace;
					if (localDebugTrace != null)
					{
						localDebugTrace.Columns.Add(new PredicateFirstConditionalStrategy.ColumnDebugInfo
						{
							ColumnName = columnDetail.Name,
							Clusters = clusters
						});
					}
					branches = new List<Cluster>();
					int whileCount = 0;
					int num = 1;
					availableByDepth = new Dictionary<int, List<Cluster>>();
					for (;;)
					{
						int num2 = whileCount + 1;
						whileCount = num2;
						if (num2 >= 10000)
						{
							break;
						}
						List<Cluster> list3;
						if (!availableByDepth.TryGetValue(num, out list3) || list3 == null)
						{
							list3 = (availableByDepth[num] = clusters.ToList<Cluster>());
						}
						IReadOnlyList<Cluster> readOnlyList2 = list3.Except(branches).ToReadOnlyList<Cluster>();
						if (num == 1 && readOnlyList2.None<Cluster>())
						{
							break;
						}
						CS$<>8__locals2.usedExamples = branches.SelectMany((Cluster e) => e.SourceExamples).ToReadOnlyList<Example<IRow, object>>();
						IEnumerable<Cluster> enumerable3 = readOnlyList2;
						Func<Cluster, bool> func3;
						if ((func3 = CS$<>8__locals2.<>9__7) == null)
						{
							func3 = (CS$<>8__locals2.<>9__7 = (Cluster cluster) => cluster.SourceExamples.Intersect(CS$<>8__locals2.usedExamples).None<Example<IRow, object>>());
						}
						Cluster cluster2 = enumerable3.FirstOrDefault(func3);
						if (cluster2 == null)
						{
							num = 1;
							branches.Clear();
						}
						else
						{
							branches.Add(cluster2);
							list3.Remove(cluster2);
							availableByDepth[num].Remove(cluster2);
							num++;
							if (branches.Count > this._maxBranches)
							{
								num = 1;
								branches.Clear();
							}
							else if (CS$<>8__locals2.usedExamples.Count + cluster2.Examples.Count == exampleCount)
							{
								branches.Add(nullBranch);
								IEnumerable<Cluster> enumerable4 = branches;
								Func<Cluster, bool> func4;
								if ((func4 = CS$<>8__locals2.<>9__8) == null)
								{
									func4 = (CS$<>8__locals2.<>9__8 = (Cluster b) => CS$<>8__locals2.previousBranches.Contains(b));
								}
								if (enumerable4.All(func4))
								{
									num = 1;
									branches.Clear();
								}
								else
								{
									Cluster[] array = branches.ToArray();
									CS$<>8__locals2.previousBranches.AddRange(array);
									yield return array;
									num = 1;
									branches.Clear();
								}
							}
						}
					}
				}
				CS$<>8__locals2 = null;
				nullBranch = null;
				TimedEvent timedEvent = null;
				clusters = null;
				branches = null;
				availableByDepth = null;
			}
			IEnumerator<ColumnDetail> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600C59E RID: 50590 RVA: 0x002A7E5C File Offset: 0x002A605C
		private static void ComputeScore(Predicate predicate)
		{
			double num;
			if (!(predicate is IsBlankPredicate))
			{
				if (!(predicate is IsNotBlankPredicate))
				{
					if (!(predicate is ContainsPredicate))
					{
						if (!(predicate is StartsWithPredicate))
						{
							if (!(predicate is StartsWithDigitPredicate))
							{
								if (!(predicate is EndsWithDigitPredicate))
								{
									if (!(predicate is GreaterThanPredicate))
									{
										if (!(predicate is LessThanPredicate))
										{
											if (!(predicate is IsNumberPredicate))
											{
												if (!(predicate is IsStringPredicate))
												{
													if (!(predicate is IsMatchPredicate))
													{
														if (!(predicate is ContainsMatchPredicate))
														{
															if (!(predicate is NumberEqualsPredicate))
															{
																if (!(predicate is StringEqualsPredicate))
																{
																	if (!(predicate is OrPredicate))
																	{
																		num = 0.0;
																	}
																	else
																	{
																		num = 0.0;
																	}
																}
																else
																{
																	num = 30.0;
																}
															}
															else
															{
																num = 35.0;
															}
														}
														else
														{
															num = 40.0;
														}
													}
													else
													{
														num = 45.0;
													}
												}
												else
												{
													num = 50.0;
												}
											}
											else
											{
												num = 55.0;
											}
										}
										else
										{
											num = 60.0;
										}
									}
									else
									{
										num = 65.0;
									}
								}
								else
								{
									num = 70.0;
								}
							}
							else
							{
								num = 75.0;
							}
						}
						else
						{
							num = 80.0;
						}
					}
					else
					{
						num = 85.0;
					}
				}
				else
				{
					num = 90.0;
				}
			}
			else
			{
				num = 95.0;
			}
			double num2 = num;
			predicate.Score = new double?(num2);
		}

		// Token: 0x0600C59F RID: 50591 RVA: 0x002A7FF0 File Offset: 0x002A61F0
		private static void ComputeScore1(Predicate predicate)
		{
			double num = 0.0;
			double num2 = 10.0;
			if (predicate is StartsWithPredicate)
			{
				num = 1.0;
				num2 = 1.0;
			}
			if (predicate is IsMatchPredicate)
			{
				num = 1.0;
				num2 = 2.0;
			}
			if (predicate is IsBlankPredicate)
			{
				num = 1.0;
				num2 = 3.0;
			}
			ContainsPredicate containsPredicate = predicate as ContainsPredicate;
			if (containsPredicate != null)
			{
				num = 0.5 * PredicateFirstConditionalStrategy.<ComputeScore1>g__Ratio|18_3((double)containsPredicate.FindText.Length, 100.0) + 0.5 * PredicateFirstConditionalStrategy.<ComputeScore1>g__Ratio|18_3((double)containsPredicate.Count, 10.0);
				num2 = 6.0;
			}
			StringEqualsPredicate stringEqualsPredicate = predicate as StringEqualsPredicate;
			if (stringEqualsPredicate != null)
			{
				num = PredicateFirstConditionalStrategy.<ComputeScore1>g__InverseRatio|18_2((double)stringEqualsPredicate.Value.Length, 50);
				num2 = 20.0;
			}
			OrPredicate orPredicate = predicate as OrPredicate;
			if (orPredicate != null)
			{
				num = orPredicate.Children.Select((Predicate i) => i.Score).Collect<double>().Average((double score) => PredicateFirstConditionalStrategy.<ComputeScore1>g__Ratio|18_3(score, 100.0));
				num2 = 50.0;
			}
			double num3 = num * 1.0;
			double num4 = num2 * 10.0;
			predicate.Score = new double?(100.0 + num3 - num4);
		}

		// Token: 0x0600C5A0 RID: 50592 RVA: 0x002A8188 File Offset: 0x002A6388
		private IEnumerable<Predicate> ResolveNumberPredicates(ColumnDetail columnDetail)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			IEnumerable<Predicate> enumerable;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("PredicateFirstConditionalStrategy", "ResolveStringPredicates", false, true) : null))
			{
				List<Predicate> list = new List<Predicate>();
				if (!columnDetail.HasNumber)
				{
					enumerable = list;
				}
				else
				{
					string name = columnDetail.Name;
					if (columnDetail.AllNumber)
					{
						list.Add(new NumberEqualsPredicate(name, 0m));
						list.Add(new GreaterThanPredicate(name, 0m));
						list.Add(new LessThanPredicate(name, 0m));
					}
					if (!columnDetail.AllNumber)
					{
						list.Add(new IsNumberPredicate(name));
					}
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					enumerable = list;
				}
			}
			return enumerable;
		}

		// Token: 0x0600C5A1 RID: 50593 RVA: 0x002A8244 File Offset: 0x002A6444
		private IEnumerable<Predicate> ResolvePredicates(ColumnDetail columnDetail)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			IEnumerable<Predicate> enumerable2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("PredicateFirstConditionalStrategy", "ResolvePredicates", false, true) : null))
			{
				IEnumerable<Predicate> enumerable = this.ResolveStringPredicates(columnDetail).Concat(this.ResolveNumberPredicates(columnDetail)).Distinct<Predicate>()
					.ToList<Predicate>();
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				enumerable2 = enumerable;
			}
			return enumerable2;
		}

		// Token: 0x0600C5A2 RID: 50594 RVA: 0x002A82B4 File Offset: 0x002A64B4
		private IEnumerable<Predicate> ResolveStringPredicates(ColumnDetail columnDetail)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			IEnumerable<Predicate> enumerable;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("PredicateFirstConditionalStrategy", "ResolveStringPredicates", false, true) : null))
			{
				List<Predicate> list = new List<Predicate>();
				string columnName = columnDetail.Name;
				IReadOnlyList<string> readOnlyList = (from v in columnDetail.Values.OfType<string>()
					where v.Length < 50
					select v).Distinct<string>().ToReadOnlyList<string>();
				if (columnDetail.AllString)
				{
					IReadOnlyList<ContainsPredicate> readOnlyList2 = (from value in readOnlyList
						where value != null
						orderby value.Length
						from delimiter in value.Where((char character) => character.IsDelimiter()).Distinct<char>()
						let delimiterCount = value.Count((char c) => c == delimiter)
						where delimiterCount < 10
						select new ContainsPredicate(columnName, delimiter.ToString(), delimiterCount)).Distinct<ContainsPredicate>().ToReadOnlyList<ContainsPredicate>();
					list.AddRange(readOnlyList2);
					list.AddRange(from predicateGroup in (from predicate in readOnlyList2
							group predicate by new { predicate.ColumnName, predicate.FindText }).Where(delegate(predicateGroup)
						{
							int num2 = predicateGroup.Count<ContainsPredicate>();
							return num2 > 1 && num2 < 5;
						})
						select new OrPredicate(predicateGroup.OrderBy((ContainsPredicate p) => p.Count)));
					list.AddRange(from value in readOnlyList
						where !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value)
						select new StringEqualsPredicate(columnName, value));
					list.AddRange((from value in readOnlyList
						where value.Length > 1 && value[0].IsDelimiter()
						select new StartsWithPredicate(value, value[0].ToString())).Distinct<StartsWithPredicate>());
					if (this._enableMatch)
					{
						list.AddRange(PredicateFirstConditionalStrategy._regexes.Select((Regex regex) => new IsMatchPredicate(columnName, regex)));
						List<ContainsMatchPredicate> list2 = (from value in readOnlyList
							from regex in PredicateFirstConditionalStrategy._regexes
							let count = regex.NonCachingMatches(value).Count<Match>()
							where count > 0
							select new ContainsMatchPredicate(columnName, regex, count)).Distinct<ContainsMatchPredicate>().ToList<ContainsMatchPredicate>();
						list.AddRange(list2);
						list.AddRange(from <>h__TransparentIdentifier0 in (from predicate in list2
								group predicate by new
								{
									ColumnName = predicate.ColumnName,
									Pattern = predicate.Pattern.ToString()
								} into predicateGroup
								select new
								{
									predicateGroup = predicateGroup,
									count = predicateGroup.Count<ContainsMatchPredicate>()
								}).Where(delegate(<>h__TransparentIdentifier0)
							{
								int count = <>h__TransparentIdentifier0.count;
								return count > 1 && count < 5;
							})
							select new OrPredicate(<>h__TransparentIdentifier0.predicateGroup.OrderBy((ContainsMatchPredicate p) => p.Count)));
					}
					list.Add(new StartsWithDigitPredicate(columnName));
					list.Add(new EndsWithDigitPredicate(columnName));
				}
				if (!columnDetail.AllString)
				{
					list.Add(new IsStringPredicate(columnName));
				}
				int num = columnDetail.Values.Count(delegate(object v)
				{
					if (v != null)
					{
						string text = v as string;
						if (text == null || !(text == ""))
						{
							return false;
						}
					}
					return true;
				});
				if (num > 0 && num < columnDetail.Values.Count)
				{
					list.Add(new IsBlankPredicate(columnName));
					list.Add(new IsNotBlankPredicate(columnName));
				}
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				enumerable = list;
			}
			return enumerable;
		}

		// Token: 0x0600C5A3 RID: 50595 RVA: 0x002A8780 File Offset: 0x002A6980
		private static IReadOnlyList<Regex> LoadRegexes()
		{
			string[] array = new string[]
			{
				"\\p{N}", "\\p{N}+", "\\p{Lu}", "\\p{L}\\.", "[\\w\\d]+", "\\p{Lu}+", "\\p{Ll}+", "(:?\\p{Zs}+\\))", "(:?[0-9]+(\\,[0-9]{3})*(\\.[0-9]+)?(?<![\\p{Zs}\\t])[\\p{Zs}\\t]*((\\r)?\\n|^|(?<!\\n)$))", "(:?[-.\\p{Lu}\\p{Ll}0-9]+)",
				"(:?\\p{Zs}+[-.\\p{Lu}\\p{Ll}0-9]+)", "^[A-Za-z0-9._+\\-\\']+@[A-Za-z0-9.\\-]+\\.[A-Za-z]{2,}$", "(:?[0-9]+(\\,[0-9]{3})*(\\.[0-9]+)?(?<![\\p{Zs}\\t])[\\p{Zs}\\t]*((\\r)?\\n|^|(?<!\\n)$))", "^\\d{4}-\\d{2}-\\d{2}T\\d{2}:\\d{2}:\\d{2}Z?$"
			};
			List<Regex> list = new List<Regex>();
			list.AddRange(array.Select((string pattern) => pattern.ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant)));
			return list;
		}

		// Token: 0x0600C5A6 RID: 50598 RVA: 0x002A8853 File Offset: 0x002A6A53
		[CompilerGenerated]
		internal static double <ComputeScore1>g__InverseRatio|18_2(double value, int maxValue)
		{
			return 1.0 - PredicateFirstConditionalStrategy.<ComputeScore1>g__Ratio|18_3(value, (double)maxValue);
		}

		// Token: 0x0600C5A7 RID: 50599 RVA: 0x002A477E File Offset: 0x002A297E
		[CompilerGenerated]
		internal static double <ComputeScore1>g__Ratio|18_3(double value, double maxValue)
		{
			return Math.Min(value, maxValue) / maxValue;
		}

		// Token: 0x04004D58 RID: 19800
		private const int _stringPredicateLengthLimit = 50;

		// Token: 0x04004D59 RID: 19801
		private IReadOnlyList<ClusterExample> _allClusterExamples;

		// Token: 0x04004D5A RID: 19802
		private readonly int _branchMinExampleCount;

		// Token: 0x04004D5B RID: 19803
		private readonly IReadOnlyList<string> _columnNamePriority;

		// Token: 0x04004D5C RID: 19804
		private readonly IEnumerable<IConditionalBranch[]> _emptyPathList = Utils.Empty<IConditionalBranch[]>();

		// Token: 0x04004D5D RID: 19805
		private readonly bool _enableMatch;

		// Token: 0x04004D5E RID: 19806
		private readonly IReadOnlyList<Example<IRow, object>> _examples;

		// Token: 0x04004D5F RID: 19807
		private readonly IReadOnlyList<IRow> _inputs;

		// Token: 0x04004D60 RID: 19808
		private readonly Func<IEnumerable<Example<IRow, object>>, ConditionalBranchMeta> _learnBranch;

		// Token: 0x04004D61 RID: 19809
		private readonly PredicateFirstConditionalStrategy.LocalDebugTrace _localDebugTrace;

		// Token: 0x04004D62 RID: 19810
		private readonly int _maxBranches;

		// Token: 0x04004D63 RID: 19811
		private static readonly IReadOnlyList<Regex> _regexes = PredicateFirstConditionalStrategy.LoadRegexes();

		// Token: 0x04004D64 RID: 19812
		private readonly LearnDebugTrace _debugTrace;

		// Token: 0x0200173F RID: 5951
		private class ColumnDebugInfo
		{
			// Token: 0x1700219C RID: 8604
			// (get) Token: 0x0600C5A8 RID: 50600 RVA: 0x002A8867 File Offset: 0x002A6A67
			// (set) Token: 0x0600C5A9 RID: 50601 RVA: 0x002A886F File Offset: 0x002A6A6F
			public IReadOnlyList<Cluster> Clusters { get; set; }

			// Token: 0x1700219D RID: 8605
			// (get) Token: 0x0600C5AA RID: 50602 RVA: 0x002A8878 File Offset: 0x002A6A78
			// (set) Token: 0x0600C5AB RID: 50603 RVA: 0x002A8880 File Offset: 0x002A6A80
			public string ColumnName { get; set; }
		}

		// Token: 0x02001740 RID: 5952
		private class LocalDebugTrace
		{
			// Token: 0x1700219E RID: 8606
			// (get) Token: 0x0600C5AD RID: 50605 RVA: 0x002A8889 File Offset: 0x002A6A89
			public List<PredicateFirstConditionalStrategy.ColumnDebugInfo> Columns { get; } = new List<PredicateFirstConditionalStrategy.ColumnDebugInfo>();

			// Token: 0x1700219F RID: 8607
			// (get) Token: 0x0600C5AE RID: 50606 RVA: 0x002A8891 File Offset: 0x002A6A91
			public List<IConditionalBranch[]> Paths { get; } = new List<IConditionalBranch[]>();

			// Token: 0x170021A0 RID: 8608
			// (get) Token: 0x0600C5AF RID: 50607 RVA: 0x002A8899 File Offset: 0x002A6A99
			public List<IConditionalBranch[]> FailedPaths { get; } = new List<IConditionalBranch[]>();

			// Token: 0x0600C5B0 RID: 50608 RVA: 0x002A88A4 File Offset: 0x002A6AA4
			public string Render()
			{
				TextBuilder textBuilder = TextBuilder.Create(4);
				textBuilder.AddSection("Paths", this.Paths.Select((IConditionalBranch[] i) => i.Select((IConditionalBranch b) => b.ToString()).ToJoinString(" -> ")).RenderNumbered(1), 2).AddSection("Failed Paths", this.FailedPaths.Select((IConditionalBranch[] i) => i.Select((IConditionalBranch b) => b.ToString()).ToJoinString(" -> ")).RenderNumbered(1), 2);
				foreach (PredicateFirstConditionalStrategy.ColumnDebugInfo columnDebugInfo in this.Columns)
				{
					textBuilder.AddSection("Clusters: " + columnDebugInfo.ColumnName, columnDebugInfo.Clusters.Render(), 2);
				}
				return textBuilder.Render();
			}
		}

		// Token: 0x02001742 RID: 5954
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04004D6F RID: 19823
			public static Action<Predicate> <0>__ComputeScore;
		}
	}
}
