using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Contracts;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.ProgramFirst.Models;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Text;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.ProgramFirst
{
	// Token: 0x0200170E RID: 5902
	public class ProgramFirstConditionalStrategy
	{
		// Token: 0x0600C474 RID: 50292 RVA: 0x002A4A58 File Offset: 0x002A2C58
		public ProgramFirstConditionalStrategy(IEnumerable<Example<IRow, object>> examples, IEnumerable<IRow> additionalInputs, Func<IEnumerable<Example<IRow, object>>, ConditionalBranchMeta> discoverConditionalProgramMeta, Grammar grammar, bool enableMatch, int maxBranches, int branchMinExampleCount, LearnDebugTrace debugTrace = null)
		{
			this._examples = examples.ToReadOnlyList<Example<IRow, object>>();
			this._inputs = additionalInputs.ToReadOnlyList<IRow>();
			this._learnBranch = discoverConditionalProgramMeta;
			this._enableMatch = enableMatch;
			this._maxBranches = maxBranches;
			this._branchMinExampleCount = branchMinExampleCount;
			if (debugTrace != null)
			{
				this._localDebugTrace = new ProgramFirstConditionalStrategy.LocalDebugTrace();
				if (debugTrace.RenderConditional == null)
				{
					debugTrace.RenderConditional = () => this._localDebugTrace.Render();
				}
			}
			this._grammarBuilder = GrammarBuilders.Instance(grammar);
		}

		// Token: 0x1700217B RID: 8571
		// (get) Token: 0x0600C475 RID: 50293 RVA: 0x002A4ADD File Offset: 0x002A2CDD
		private GrammarBuilders.Nodes.RuleIs IsRule
		{
			get
			{
				return this._grammarBuilder.Node.IsRule;
			}
		}

		// Token: 0x0600C476 RID: 50294 RVA: 0x002A4AF0 File Offset: 0x002A2CF0
		public ConditionalPathList Paths()
		{
			ConditionalClusterList conditionalClusterList = this.ResolveClusters();
			if (!this.Valid(conditionalClusterList))
			{
				return ProgramFirstConditionalStrategy._emptyPathList;
			}
			conditionalClusterList = this.ClassifyInputs(conditionalClusterList);
			if (!this.Valid(conditionalClusterList))
			{
				return ProgramFirstConditionalStrategy._emptyPathList;
			}
			int num = conditionalClusterList.Sum((ConditionalCluster c) => c.ValidPredicates.Count);
			foreach (ConditionalCluster conditionalCluster in conditionalClusterList)
			{
				conditionalCluster.Score = new double?(this.ComputeScore(conditionalCluster, num));
			}
			ConditionalClusterList orderedClusters = new ConditionalClusterList(conditionalClusterList.OrderByDescending((ConditionalCluster c) => c.Score), conditionalClusterList.UnclusteredInputs);
			if (this._localDebugTrace != null)
			{
				this._localDebugTrace.ConditionalClusters = orderedClusters;
			}
			ConditionalBranch lastBranch;
			List<ConditionalCluster> list;
			if (orderedClusters.Last<ConditionalCluster>().ValidPredicates.Any<Predicate>())
			{
				list = new List<ConditionalCluster>(orderedClusters);
				lastBranch = new NullConditionalBranch();
			}
			else
			{
				list = orderedClusters.Take(orderedClusters.Count - 1).ToList<ConditionalCluster>();
				lastBranch = new ConditionalBranch(null, orderedClusters.Last<ConditionalCluster>());
			}
			List<ConditionalBranch[]> list2 = list.Select((ConditionalCluster cluster) => cluster.ValidPredicates.Select((Predicate predicate) => new ConditionalBranch(predicate, cluster)).ToArray<ConditionalBranch>()).ToList<ConditionalBranch[]>();
			ConditionalPathList conditionalPathList = list2.Skip(1).Aggregate(new ConditionalPathList(list2[0].Select((ConditionalBranch branch) => new ConditionalBranch[] { branch }), orderedClusters), (ConditionalPathList pathSet, ConditionalBranch[] branchSet) => new ConditionalPathList(from branch in branchSet
				from path in pathSet
				select path.AppendItem(branch).ToArray<ConditionalBranch>(), orderedClusters));
			conditionalPathList = new ConditionalPathList(conditionalPathList.Select((ConditionalBranch[] path) => path.AppendItem(lastBranch).ToArray<ConditionalBranch>()), orderedClusters);
			if (this._localDebugTrace != null)
			{
				this._localDebugTrace.ConditionalPaths = conditionalPathList;
			}
			return conditionalPathList;
		}

		// Token: 0x0600C477 RID: 50295 RVA: 0x002A4D10 File Offset: 0x002A2F10
		private ConditionalClusterList ClassifyInputs(ConditionalClusterList clusters)
		{
			if (this._inputs.None<IRow>())
			{
				return clusters;
			}
			List<IRow> list = new List<IRow>(this._inputs);
			Dictionary<ConditionalCluster, List<ClusterSourceAdditionalInput>> inputClusters = new Dictionary<ConditionalCluster, List<ClusterSourceAdditionalInput>>();
			foreach (ConditionalCluster conditionalCluster in clusters)
			{
				inputClusters[conditionalCluster] = new List<ClusterSourceAdditionalInput>();
			}
			int num = 1;
			using (IEnumerator<IRow> enumerator2 = this._inputs.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					IRow additionalInput = enumerator2.Current;
					var func;
					Func<ConditionalCluster, <>f__AnonymousType49<ConditionalCluster, object>> <>9__1;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (ConditionalCluster cluster) => new
						{
							cluster = cluster,
							additionalOutput = cluster.Program.Run(additionalInput)
						});
					}
					var enumerable = clusters.Select(func);
					var func2;
					Func<<>f__AnonymousType49<ConditionalCluster, object>, bool> <>9__2;
					if ((func2 = <>9__2) == null)
					{
						Func<Predicate, bool> <>9__4;
						func2 = (<>9__2 = delegate(<>h__TransparentIdentifier0)
						{
							if (<>h__TransparentIdentifier0.additionalOutput != null)
							{
								IEnumerable<Predicate> validPredicates = <>h__TransparentIdentifier0.cluster.ValidPredicates;
								Func<Predicate, bool> func3;
								if ((func3 = <>9__4) == null)
								{
									func3 = (<>9__4 = (Predicate p) => p.Evaluate(additionalInput));
								}
								return validPredicates.All(func3);
							}
							return false;
						});
					}
					foreach (ConditionalCluster conditionalCluster2 in from <>h__TransparentIdentifier0 in enumerable.Where(func2)
						select <>h__TransparentIdentifier0.cluster)
					{
						list.Remove(additionalInput);
						inputClusters[conditionalCluster2].Add(new ClusterSourceAdditionalInput
						{
							Input = additionalInput,
							Position = num
						});
						num++;
					}
				}
			}
			return new ConditionalClusterList(clusters.Select((ConditionalCluster c) => new ConditionalCluster(c, inputClusters[c])), list);
		}

		// Token: 0x0600C478 RID: 50296 RVA: 0x002A4F00 File Offset: 0x002A3100
		private ConditionalClusterList ResolveClusters()
		{
			Dictionary<ConditionalBranchMeta, List<ConditionalClusterSourceExample>> dictionary = new Dictionary<ConditionalBranchMeta, List<ConditionalClusterSourceExample>>();
			var list = this._examples.Select((Example<IRow, object> e, int index) => new
			{
				Position = index + 1,
				Example = e
			}).ToList();
			int num = 0;
			while (list.Any())
			{
				var <>f__AnonymousType = list.First();
				ConditionalBranchMeta programMeta = this._learnBranch(<>f__AnonymousType.Example.Yield<Example<IRow, object>>());
				if (programMeta == null)
				{
					return ProgramFirstConditionalStrategy._emptyClusterList;
				}
				list.Remove(<>f__AnonymousType);
				if (!dictionary.ContainsKey(programMeta))
				{
					dictionary.Add(programMeta, new List<ConditionalClusterSourceExample>());
				}
				num++;
				if (num > this._maxBranches)
				{
					return ProgramFirstConditionalStrategy._emptyClusterList;
				}
				dictionary[programMeta].Add(new ConditionalClusterSourceExample
				{
					Position = <>f__AnonymousType.Position,
					Example = <>f__AnonymousType.Example
				});
				foreach (var <>f__AnonymousType2 in list.ToList().Where(delegate(remainingExample)
				{
					object obj = programMeta.Program.Run(remainingExample.Example.Input);
					return obj != null && obj.Equals(remainingExample.Example.Output);
				}))
				{
					dictionary[programMeta].Add(new ConditionalClusterSourceExample
					{
						Position = <>f__AnonymousType2.Position,
						Example = <>f__AnonymousType2.Example
					});
					list.Remove(<>f__AnonymousType2);
				}
			}
			ConditionalClusterList conditionalClusterList = new ConditionalClusterList(dictionary.OrderBy(delegate(KeyValuePair<ConditionalBranchMeta, List<ConditionalClusterSourceExample>> pair)
			{
				KeyValuePair<ConditionalBranchMeta, List<ConditionalClusterSourceExample>> keyValuePair = pair;
				return keyValuePair.Key.Program.Score;
			}).Select(delegate(KeyValuePair<ConditionalBranchMeta, List<ConditionalClusterSourceExample>> pair)
			{
				KeyValuePair<ConditionalBranchMeta, List<ConditionalClusterSourceExample>> keyValuePair2 = pair;
				ConditionalBranchMeta key = keyValuePair2.Key;
				keyValuePair2 = pair;
				IEnumerable<ConditionalClusterSourceExample> value = keyValuePair2.Value;
				ProgramFirstConditionalStrategy <>4__this = this;
				keyValuePair2 = pair;
				ConditionalBranchMeta key2 = keyValuePair2.Key;
				keyValuePair2 = pair;
				return new ConditionalCluster(key, value, <>4__this.ResolvePredicates(key2, keyValuePair2.Value));
			}));
			if (this._localDebugTrace != null)
			{
				this._localDebugTrace.ConditionalRawClusters = conditionalClusterList;
			}
			if (!ProgramFirstConditionalStrategy.ValidCount(conditionalClusterList))
			{
				return ProgramFirstConditionalStrategy._emptyClusterList;
			}
			ConditionalClusterList conditionalClusterList2 = ProgramFirstConditionalStrategy.ResolveValidPredicates(conditionalClusterList.OrderBy((ConditionalCluster c) => c.Program.Score));
			if (this._localDebugTrace != null)
			{
				this._localDebugTrace.ConditionalOriginalClusters = conditionalClusterList2;
			}
			List<ConditionalCluster> list2 = new List<ConditionalCluster>();
			List<ConditionalCluster> unmergedClusters = new List<ConditionalCluster>(conditionalClusterList2);
			Action<ConditionalCluster> <>9__12;
			while (unmergedClusters.Any<ConditionalCluster>())
			{
				ConditionalCluster conditionalCluster = unmergedClusters.First<ConditionalCluster>();
				ConditionalCluster combinedCluster = null;
				List<ConditionalClusterSourceExample> list3 = conditionalCluster.SourceExamples.ToList<ConditionalClusterSourceExample>();
				foreach (ConditionalCluster conditionalCluster2 in unmergedClusters.Skip(1))
				{
					List<ConditionalClusterSourceExample> list4 = list3.Concat(conditionalCluster2.SourceExamples).ToList<ConditionalClusterSourceExample>();
					List<Example<IRow, object>> list5 = list4.Select((ConditionalClusterSourceExample s) => s.Example).ToList<Example<IRow, object>>();
					ConditionalBranchMeta conditionalBranchMeta = this._learnBranch(list5);
					if (!(conditionalBranchMeta == null))
					{
						combinedCluster = new ConditionalCluster(conditionalBranchMeta, list4, this.ResolvePredicates(conditionalBranchMeta, list4));
						if (ProgramFirstConditionalStrategy.<ResolveClusters>g__TryAddCombinedCluster|16_4(list2, combinedCluster))
						{
							unmergedClusters.Remove(conditionalCluster2);
							break;
						}
						combinedCluster = null;
					}
				}
				unmergedClusters.Remove(conditionalCluster);
				if (combinedCluster != null)
				{
					List<ConditionalCluster> list6 = unmergedClusters.Where((ConditionalCluster unmergedCluster) => unmergedCluster.SourceExamples.All(delegate(ConditionalClusterSourceExample e)
					{
						object obj2 = combinedCluster.Program.Run(e.Example.Input);
						return obj2 != null && obj2.Equals(e.Example.Output);
					})).ToList<ConditionalCluster>();
					if (list6.Any<ConditionalCluster>())
					{
						List<ConditionalClusterSourceExample> list7 = combinedCluster.SourceExamples.Concat(list6.SelectMany((ConditionalCluster c) => c.SourceExamples)).ToList<ConditionalClusterSourceExample>();
						ConditionalCluster conditionalCluster3 = new ConditionalCluster(combinedCluster.ProgramMeta, list7, this.ResolvePredicates(combinedCluster.ProgramMeta, list7));
						if (ProgramFirstConditionalStrategy.<ResolveClusters>g__TryAddCombinedCluster|16_4(list2, conditionalCluster3))
						{
							List<ConditionalCluster> list8 = list6;
							Action<ConditionalCluster> action;
							if ((action = <>9__12) == null)
							{
								action = (<>9__12 = delegate(ConditionalCluster c)
								{
									unmergedClusters.Remove(c);
								});
							}
							list8.ForEach(action);
							combinedCluster = conditionalCluster3;
						}
					}
				}
				list2.Add(combinedCluster ?? conditionalCluster);
			}
			if (!ProgramFirstConditionalStrategy.ValidCount(list2))
			{
				list2 = conditionalClusterList2.ToList<ConditionalCluster>();
			}
			ConditionalClusterList conditionalClusterList3 = ProgramFirstConditionalStrategy.ResolveValidPredicates(from c in list2
				orderby c.SourceExamples.Count descending, c.Program.Score descending
				select c);
			if (this._localDebugTrace != null)
			{
				this._localDebugTrace.ConditionalMergedClusters = conditionalClusterList3;
			}
			if (ProgramFirstConditionalStrategy.ValidPredicates(conditionalClusterList3))
			{
				return conditionalClusterList3;
			}
			return ProgramFirstConditionalStrategy._emptyClusterList;
		}

		// Token: 0x0600C479 RID: 50297 RVA: 0x002A53FC File Offset: 0x002A35FC
		private bool Valid(ConditionalClusterList clusters)
		{
			return ProgramFirstConditionalStrategy.ValidCount(clusters) && ProgramFirstConditionalStrategy.ValidPredicates(clusters) && this.ValidMinExampleCount(clusters);
		}

		// Token: 0x0600C47A RID: 50298 RVA: 0x002A5417 File Offset: 0x002A3617
		private static bool ValidCount(IReadOnlyList<ConditionalCluster> clusters)
		{
			return clusters.Count >= 2;
		}

		// Token: 0x0600C47B RID: 50299 RVA: 0x002A5425 File Offset: 0x002A3625
		private bool ValidMinExampleCount(IReadOnlyList<ConditionalCluster> clusters)
		{
			return clusters.All((ConditionalCluster c) => c.SourceExamples.HasAtLeast(this._branchMinExampleCount));
		}

		// Token: 0x0600C47C RID: 50300 RVA: 0x002A5439 File Offset: 0x002A3639
		private static bool ValidPredicates(IReadOnlyList<ConditionalCluster> clusters)
		{
			return clusters.Count((ConditionalCluster c) => c.ValidPredicates.None<Predicate>()) <= 1;
		}

		// Token: 0x0600C47D RID: 50301 RVA: 0x002A5468 File Offset: 0x002A3668
		private static IEnumerable<Predicate> ResolveNumberPredicates(ConditionalBranchMeta branchMeta, IEnumerable<ConditionalClusterSourceExample> sourceExamples)
		{
			List<Predicate> list = new List<Predicate>();
			List<ColumnDetail> list2 = (from d in sourceExamples.Select((ConditionalClusterSourceExample e) => e.Example).InputColumnDetails(null)
				where d.AllNumber
				where branchMeta.UsedColumnNames.None<string>() || branchMeta.UsedColumnNames.Contains(d.Name)
				select d).ToList<ColumnDetail>();
			if (list2.None<ColumnDetail>())
			{
				return list;
			}
			list.AddRange(list2.Select((ColumnDetail info) => new NumberEqualsPredicate(info.Name, 0m)));
			list.AddRange(list2.Select((ColumnDetail info) => new GreaterThanPredicate(info.Name, 0m)));
			list.AddRange(list2.Select((ColumnDetail info) => new LessThanPredicate(info.Name, 0m)));
			list.AddRange(from info in list2
				where info.HasNumber
				select new IsNumberPredicate(info.Name));
			return list;
		}

		// Token: 0x0600C47E RID: 50302 RVA: 0x002A55C9 File Offset: 0x002A37C9
		private ConditionalClusterList ResolvePredicates(IEnumerable<ConditionalCluster> clusters)
		{
			return new ConditionalClusterList(clusters.Select((ConditionalCluster cluster) => new ConditionalCluster(cluster, this.ResolvePredicates(cluster.ProgramMeta, cluster.SourceExamples), null)));
		}

		// Token: 0x0600C47F RID: 50303 RVA: 0x002A55E4 File Offset: 0x002A37E4
		private IEnumerable<Predicate> ResolvePredicates(ConditionalBranchMeta branchMeta, IReadOnlyList<ConditionalClusterSourceExample> sourceExamples)
		{
			return from predicate in this.ResolveStringPredicates(branchMeta, sourceExamples).Concat(ProgramFirstConditionalStrategy.ResolveNumberPredicates(branchMeta, sourceExamples)).Distinct<Predicate>()
				let localInputs = sourceExamples.Select((ConditionalClusterSourceExample se) => se.Example.Input)
				where localInputs.All(new Func<IRow, bool>(predicate.Evaluate))
				select predicate;
		}

		// Token: 0x0600C480 RID: 50304 RVA: 0x002A567A File Offset: 0x002A387A
		private IReadOnlyList<Predicate> ResolvePredicates(ConditionalCluster cluster)
		{
			return this.ResolveStringPredicates(cluster.ProgramMeta, cluster.SourceExamples).Concat(ProgramFirstConditionalStrategy.ResolveNumberPredicates(cluster.ProgramMeta, cluster.SourceExamples)).Distinct<Predicate>()
				.ToList<Predicate>();
		}

		// Token: 0x0600C481 RID: 50305 RVA: 0x002A56B0 File Offset: 0x002A38B0
		private IEnumerable<Predicate> ResolveStringPredicates(ConditionalBranchMeta branchMeta, IReadOnlyList<ConditionalClusterSourceExample> sourceExamples)
		{
			List<Predicate> list = new List<Predicate>();
			IReadOnlyList<ColumnDetail> readOnlyList = (from d in sourceExamples.Select((ConditionalClusterSourceExample s) => s.Example).InputColumnDetails(null)
				where d.AllString
				select d).ToList<ColumnDetail>();
			IReadOnlyList<string> readOnlyList2 = readOnlyList.Select((ColumnDetail c) => c.Name).ToList<string>();
			if (readOnlyList2.None<string>())
			{
				return list;
			}
			IReadOnlyList<string> targetColumnNames = (branchMeta.UsedColumnNames.Any<string>() ? branchMeta.UsedColumnNames : readOnlyList2);
			IReadOnlyList<Example<IRow, object>> examples = sourceExamples.Select((ConditionalClusterSourceExample e) => e.Example).ToList<Example<IRow, object>>();
			IEnumerable<ContainsPredicate> enumerable = from <>h__TransparentIdentifier1 in (from delimiter in branchMeta.Delimiters
					from sourceExample in examples
					select new { delimiter, sourceExample }).Select(delegate(<>h__TransparentIdentifier0)
				{
					string text = <>h__TransparentIdentifier0.sourceExample.Input.Get(<>h__TransparentIdentifier0.delimiter.ColumnName) as string;
					return new
					{
						<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
						delimiterCount = ((text != null) ? new int?(text.AllIndexesOf(<>h__TransparentIdentifier0.delimiter.Value, StringComparison.Ordinal).Count<int>()) : null)
					};
				})
				where <>h__TransparentIdentifier1.delimiterCount != null && <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.delimiter.Value != " "
				select new ContainsPredicate(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.delimiter.ColumnName, <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.delimiter.Value, <>h__TransparentIdentifier1.delimiterCount.Value);
			IEnumerable<ContainsPredicate> enumerable2 = from sourceExample in examples
				from columnName in targetColumnNames
				let columnValue = sourceExample.Input.Get(columnName) as string
				where columnValue != null
				from delimiter in (from c in columnValue
					where c.IsDelimiter()
					select c into d
					select d.ToString()).Distinct<string>()
				let delimiterCount = columnValue.AllIndexesOf(delimiter, StringComparison.Ordinal).Count<int>()
				select new ContainsPredicate(columnName, delimiter, delimiterCount);
			List<ContainsPredicate> list2 = enumerable.Union(enumerable2).ToList<ContainsPredicate>();
			list.AddRange(list2);
			list.AddRange(from predicateGroup in (from predicate in list2
					group predicate by new { predicate.ColumnName, predicate.FindText }).Where(delegate(predicateGroup)
				{
					int num = predicateGroup.Count<ContainsPredicate>();
					return num > 1 && num < 5;
				})
				select new OrPredicate(predicateGroup.OrderBy((ContainsPredicate p) => p.Count)));
			if (examples.Count > 1 || !branchMeta.Delimiters.Any<ConditionalBranchDelimiterInfo>())
			{
				list.AddRange((from column in readOnlyList
					let stringEqualsPredicates = (from columnValue in column.Values.OfType<string>().Distinct<string>()
						where !string.IsNullOrEmpty(columnValue) && !string.IsNullOrWhiteSpace(columnValue)
						select new StringEqualsPredicate(column.Name, columnValue)).ToList<StringEqualsPredicate>()
					where stringEqualsPredicates.Any<StringEqualsPredicate>() && stringEqualsPredicates.Count == 1
					select <>h__TransparentIdentifier0).Select(delegate(<>h__TransparentIdentifier0)
				{
					if (<>h__TransparentIdentifier0.stringEqualsPredicates.Count != 1)
					{
						return new OrPredicate(<>h__TransparentIdentifier0.stringEqualsPredicates);
					}
					return <>h__TransparentIdentifier0.stringEqualsPredicates.First<StringEqualsPredicate>();
				}));
			}
			list.AddRange((from column in readOnlyList
				from columnValue in column.Values.OfType<string>().Distinct<string>()
				where columnValue.Length > 1 && columnValue[0].IsDelimiter()
				select new StartsWithPredicate(column.Name, columnValue[0].ToString())).Distinct<StartsWithPredicate>());
			if (this._enableMatch)
			{
				list.AddRange(from column in readOnlyList
					from columnValue in column.Values.OfType<string>().Distinct<string>()
					from regex in ProgramFirstConditionalStrategy._regexes
					select new IsMatchPredicate(column.Name, regex));
				List<ContainsMatchPredicate> list3 = (from column in readOnlyList
					from columnValue in column.Values.OfType<string>().Distinct<string>()
					from regex in ProgramFirstConditionalStrategy._regexes
					let count = regex.NonCachingMatches(columnValue).Count<Match>()
					where count > 0
					select new ContainsMatchPredicate(column.Name, regex, count)).Distinct<ContainsMatchPredicate>().ToList<ContainsMatchPredicate>();
				list.AddRange(list3);
				list.AddRange(from <>h__TransparentIdentifier0 in (from predicate in list3
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
			list.AddRange(readOnlyList2.Select((string n) => new StartsWithDigitPredicate(n)));
			list.AddRange(readOnlyList2.Select((string n) => new EndsWithDigitPredicate(n)));
			list.AddRange(readOnlyList2.Select((string n) => new IsBlankPredicate(n)));
			list.AddRange(readOnlyList2.Select((string n) => new IsNotBlankPredicate(n)));
			list.AddRange(readOnlyList2.Select((string n) => new IsStringPredicate(n)));
			return list;
		}

		// Token: 0x0600C482 RID: 50306 RVA: 0x002A5E0C File Offset: 0x002A400C
		private static ConditionalClusterList ResolveValidPredicates(IEnumerable<ConditionalCluster> clusters)
		{
			IReadOnlyList<ConditionalCluster> iclusters = clusters.ToReadOnlyList<ConditionalCluster>();
			return new ConditionalClusterList(iclusters.Select((ConditionalCluster cluster) => ProgramFirstConditionalStrategy.ResolveValidPredicates(cluster, iclusters)));
		}

		// Token: 0x0600C483 RID: 50307 RVA: 0x002A5E48 File Offset: 0x002A4048
		private static ConditionalCluster ResolveValidPredicates(ConditionalCluster cluster, IEnumerable<ConditionalCluster> clusters)
		{
			IEnumerable<Predicate> enumerable = from localPredicate in cluster.Predicates
				let remoteInputs = from se in clusters.Where((ConditionalCluster c) => c != cluster).SelectMany((ConditionalCluster c) => c.SourceExamples)
					select se.Example.Input
				where remoteInputs.All((IRow i) => !localPredicate.Evaluate(i))
				orderby localPredicate.Score descending
				select localPredicate;
			return new ConditionalCluster(cluster, null, enumerable);
		}

		// Token: 0x0600C484 RID: 50308 RVA: 0x002A5F00 File Offset: 0x002A4100
		private double ComputeScore(ConditionalCluster cluster, int predicateTotalCount)
		{
			if (cluster.ValidPredicates.None<Predicate>())
			{
				return 0.0;
			}
			double num = 0.15 * ProgramFirstConditionalStrategy.<ComputeScore>g__Ratio|28_0(cluster.Program.Score, 100000.0);
			double num2 = (cluster.ValidPredicates.Any((Predicate p) => p is IsBlankPredicate) ? 1.0 : (0.5 * ProgramFirstConditionalStrategy.<ComputeScore>g__Ratio|28_0((double)cluster.ValidPredicates.Count, (double)predicateTotalCount)));
			double num3 = ((this._examples.Count == 0) ? 0.0 : (0.25 * ProgramFirstConditionalStrategy.<ComputeScore>g__Ratio|28_0((double)cluster.SourceExamples.Count, (double)this._examples.Count)));
			double num4 = ((this._inputs.Count == 0) ? 0.0 : (0.1 * ((double)cluster.SourceInputs.Count / Convert.ToDouble(this._inputs.Count))));
			return 100000.0 * ((num + num2 + num3 + num4) / 4.0);
		}

		// Token: 0x0600C485 RID: 50309 RVA: 0x002A6038 File Offset: 0x002A4238
		private static IReadOnlyList<Regex> LoadRegexes()
		{
			string[] array = new string[]
			{
				"\\p{N}", "\\p{N}+", "\\p{Lu}", "\\p{L}\\.", "[\\w\\d]+", "\\p{Lu}+", "\\p{Ll}+", "(:?\\p{Zs}+\\))", "(:?[0-9]+(\\,[0-9]{3})*(\\.[0-9]+)?(?<![\\p{Zs}\\t])[\\p{Zs}\\t]*((\\r)?\\n|^|(?<!\\n)$))", "(:?[-.\\p{Lu}\\p{Ll}0-9]+)",
				"(:?\\p{Zs}+[-.\\p{Lu}\\p{Ll}0-9]+)", "^[A-Za-z0-9._+\\-\\']+@[A-Za-z0-9.\\-]+\\.[A-Za-z]{2,}$", "(:?[0-9]+(\\,[0-9]{3})*(\\.[0-9]+)?(?<![\\p{Zs}\\t])[\\p{Zs}\\t]*((\\r)?\\n|^|(?<!\\n)$))", "^\\d{4}-\\d{2}-\\d{2}T\\d{2}:\\d{2}:\\d{2}Z?$"
			};
			List<Regex> list = new List<Regex>();
			list.AddRange(array.Select((string pattern) => pattern.ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant)));
			list.AddRange(from pattern in array
				where !pattern.StartsWith("^")
				select ("^" + pattern).ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant));
			list.AddRange(from pattern in array
				where !pattern.EndsWith("$")
				select (pattern + "$").ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant));
			return list;
		}

		// Token: 0x0600C488 RID: 50312 RVA: 0x002A61BD File Offset: 0x002A43BD
		[CompilerGenerated]
		internal static bool <ResolveClusters>g__TryAddCombinedCluster|16_4(IReadOnlyList<ConditionalCluster> sourceCombineClusters, ConditionalCluster candidateCombineCluster)
		{
			return ProgramFirstConditionalStrategy.ValidPredicates(ProgramFirstConditionalStrategy.ResolveValidPredicates(sourceCombineClusters.AppendItem(candidateCombineCluster)));
		}

		// Token: 0x0600C48B RID: 50315 RVA: 0x002A477E File Offset: 0x002A297E
		[CompilerGenerated]
		internal static double <ComputeScore>g__Ratio|28_0(double value, double maxValue)
		{
			return Math.Min(value, maxValue) / maxValue;
		}

		// Token: 0x04004CAA RID: 19626
		private readonly int _branchMinExampleCount;

		// Token: 0x04004CAB RID: 19627
		private static readonly IReadOnlyList<Regex> _regexes = ProgramFirstConditionalStrategy.LoadRegexes();

		// Token: 0x04004CAC RID: 19628
		private static readonly ConditionalClusterList _emptyClusterList = new ConditionalClusterList();

		// Token: 0x04004CAD RID: 19629
		private static readonly ConditionalPathList _emptyPathList = new ConditionalPathList();

		// Token: 0x04004CAE RID: 19630
		private readonly bool _enableMatch;

		// Token: 0x04004CAF RID: 19631
		private readonly IReadOnlyList<Example<IRow, object>> _examples;

		// Token: 0x04004CB0 RID: 19632
		private readonly GrammarBuilders _grammarBuilder;

		// Token: 0x04004CB1 RID: 19633
		private readonly IReadOnlyList<IRow> _inputs;

		// Token: 0x04004CB2 RID: 19634
		private readonly Func<IEnumerable<Example<IRow, object>>, ConditionalBranchMeta> _learnBranch;

		// Token: 0x04004CB3 RID: 19635
		private readonly ProgramFirstConditionalStrategy.LocalDebugTrace _localDebugTrace;

		// Token: 0x04004CB4 RID: 19636
		private readonly int _maxBranches;

		// Token: 0x0200170F RID: 5903
		private class LocalDebugTrace
		{
			// Token: 0x1700217C RID: 8572
			// (get) Token: 0x0600C48C RID: 50316 RVA: 0x002A61FE File Offset: 0x002A43FE
			// (set) Token: 0x0600C48D RID: 50317 RVA: 0x002A6206 File Offset: 0x002A4406
			public ConditionalClusterList ConditionalClusters { get; set; }

			// Token: 0x1700217D RID: 8573
			// (get) Token: 0x0600C48E RID: 50318 RVA: 0x002A620F File Offset: 0x002A440F
			// (set) Token: 0x0600C48F RID: 50319 RVA: 0x002A6217 File Offset: 0x002A4417
			public ConditionalClusterList ConditionalMergedClusters { get; set; }

			// Token: 0x1700217E RID: 8574
			// (get) Token: 0x0600C490 RID: 50320 RVA: 0x002A6220 File Offset: 0x002A4420
			// (set) Token: 0x0600C491 RID: 50321 RVA: 0x002A6228 File Offset: 0x002A4428
			public ConditionalClusterList ConditionalOriginalClusters { get; set; }

			// Token: 0x1700217F RID: 8575
			// (get) Token: 0x0600C492 RID: 50322 RVA: 0x002A6231 File Offset: 0x002A4431
			// (set) Token: 0x0600C493 RID: 50323 RVA: 0x002A6239 File Offset: 0x002A4439
			public ConditionalPathList ConditionalPaths { get; set; }

			// Token: 0x17002180 RID: 8576
			// (get) Token: 0x0600C494 RID: 50324 RVA: 0x002A6242 File Offset: 0x002A4442
			// (set) Token: 0x0600C495 RID: 50325 RVA: 0x002A624A File Offset: 0x002A444A
			public ConditionalClusterList ConditionalRawClusters { get; set; }

			// Token: 0x0600C496 RID: 50326 RVA: 0x002A6254 File Offset: 0x002A4454
			public string Render()
			{
				TextBuilder textBuilder = TextBuilder.Create(4);
				if (this.ConditionalClusters != null)
				{
					TextBuilder textBuilder2 = textBuilder;
					string text = "Conditional Clusters";
					ConditionalClusterList conditionalClusters = this.ConditionalClusters;
					textBuilder2.AddSection(text, (conditionalClusters != null) ? conditionalClusters.Render(false).TrimEnd(Array.Empty<char>()) : null, 2);
				}
				ConditionalClusterList conditionalClusters2 = this.ConditionalClusters;
				if (conditionalClusters2 == null || !conditionalClusters2.ValidPredicates)
				{
					if (this.ConditionalMergedClusters != null)
					{
						TextBuilder textBuilder3 = textBuilder;
						string text2 = "Merged Conditional Clusters";
						ConditionalClusterList conditionalMergedClusters = this.ConditionalMergedClusters;
						textBuilder3.AddSection(text2, (conditionalMergedClusters != null) ? conditionalMergedClusters.Render(false).TrimEnd(Array.Empty<char>()) : null, 2);
					}
					ConditionalClusterList conditionalMergedClusters2 = this.ConditionalMergedClusters;
					if (conditionalMergedClusters2 == null || !conditionalMergedClusters2.ValidPredicates)
					{
						if (this.ConditionalOriginalClusters != null)
						{
							TextBuilder textBuilder4 = textBuilder;
							string text3 = "Original Conditional Clusters";
							ConditionalClusterList conditionalOriginalClusters = this.ConditionalOriginalClusters;
							textBuilder4.AddSection(text3, (conditionalOriginalClusters != null) ? conditionalOriginalClusters.Render(false).TrimEnd(Array.Empty<char>()) : null, 2);
						}
						if (this.ConditionalRawClusters != null)
						{
							ConditionalClusterList conditionalOriginalClusters2 = this.ConditionalOriginalClusters;
							if (conditionalOriginalClusters2 == null || !conditionalOriginalClusters2.ValidPredicates)
							{
								TextBuilder textBuilder5 = textBuilder;
								string text4 = "Raw Conditional Clusters";
								ConditionalClusterList conditionalRawClusters = this.ConditionalRawClusters;
								textBuilder5.AddSection(text4, (conditionalRawClusters != null) ? conditionalRawClusters.Render(true).TrimEnd(Array.Empty<char>()) : null, 2);
							}
						}
					}
				}
				return textBuilder.Render();
			}
		}
	}
}
