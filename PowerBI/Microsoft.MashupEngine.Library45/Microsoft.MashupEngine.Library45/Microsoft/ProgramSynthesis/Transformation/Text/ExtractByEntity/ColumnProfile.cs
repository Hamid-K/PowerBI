using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Transformation.Text.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.ExtractByEntity;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities;

namespace Microsoft.ProgramSynthesis.Transformation.Text.ExtractByEntity
{
	// Token: 0x02001DD1 RID: 7633
	public class ColumnProfile
	{
		// Token: 0x17002A7E RID: 10878
		// (get) Token: 0x0600FFF3 RID: 65523 RVA: 0x00370074 File Offset: 0x0036E274
		public string ColumnName { get; }

		// Token: 0x17002A7F RID: 10879
		// (get) Token: 0x0600FFF4 RID: 65524 RVA: 0x0037007C File Offset: 0x0036E27C
		public TokenizerCollection TokenizerCollection { get; }

		// Token: 0x17002A80 RID: 10880
		// (get) Token: 0x0600FFF5 RID: 65525 RVA: 0x00370084 File Offset: 0x0036E284
		public IReadOnlyList<string> ColumnData { get; }

		// Token: 0x17002A81 RID: 10881
		// (get) Token: 0x0600FFF6 RID: 65526 RVA: 0x0037008C File Offset: 0x0036E28C
		// (set) Token: 0x0600FFF7 RID: 65527 RVA: 0x00370094 File Offset: 0x0036E294
		public ImmutableDictionary<EntityType, int> EntityCounts { get; private set; }

		// Token: 0x17002A82 RID: 10882
		// (get) Token: 0x0600FFF8 RID: 65528 RVA: 0x0037009D File Offset: 0x0036E29D
		// (set) Token: 0x0600FFF9 RID: 65529 RVA: 0x003700A5 File Offset: 0x0036E2A5
		public ImmutableList<EntityInstance> EntityInstances { get; private set; }

		// Token: 0x17002A83 RID: 10883
		// (get) Token: 0x0600FFFA RID: 65530 RVA: 0x003700AE File Offset: 0x0036E2AE
		private static IReadOnlyList<KeyValuePair<EntityDescriptor, EntityBasedTokenizer>> AllExtractors
		{
			get
			{
				return ColumnProfile.AllExtractorsLazy.Value;
			}
		}

		// Token: 0x0600FFFB RID: 65531 RVA: 0x003700BC File Offset: 0x0036E2BC
		private static IReadOnlyList<KeyValuePair<EntityDescriptor, EntityBasedTokenizer>> BuildAllExtractorList()
		{
			return EntityMappings.EntityDescriptors.Select((KeyValuePair<EntityType, EntityDescriptor> d) => new KeyValuePair<EntityDescriptor, EntityBasedTokenizer>(d.Value, d.Value.ExtractorFactory())).Distinct((KeyValuePair<EntityDescriptor, EntityBasedTokenizer> kvp) => kvp.Value.GetType()).ToList<KeyValuePair<EntityDescriptor, EntityBasedTokenizer>>();
		}

		// Token: 0x0600FFFC RID: 65532 RVA: 0x0037011C File Offset: 0x0036E31C
		private ColumnProfile(string columnName, IReadOnlyList<string> columnData)
		{
			this.ColumnName = columnName;
			this.ColumnData = columnData;
			IReadOnlyList<EntityBasedTokenizer> readOnlyList = this.DetectEntities();
			this.TokenizerCollection = new TokenizerCollection(readOnlyList);
		}

		// Token: 0x0600FFFD RID: 65533 RVA: 0x00370150 File Offset: 0x0036E350
		private static Task<ImmutableList<ImmutableList<EntityToken>>> _MakeTokenizationTask(EntityBasedTokenizer tokenizer, IReadOnlyList<string> sample)
		{
			Func<string, ImmutableList<EntityToken>> <>9__1;
			return Task.Run<ImmutableList<ImmutableList<EntityToken>>>(delegate
			{
				IEnumerable<string> sample2 = sample;
				Func<string, ImmutableList<EntityToken>> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (string l) => (from tok in tokenizer.Tokenize(l)
						where EntityMappings.TypeToEntityType.ContainsKey(tok.GetType())
						group tok by tok.GetType()).SelectMany((IGrouping<Type, EntityToken> g) => g.Distinct((EntityToken tok) => new KeyValuePair<int, int>(tok.Start, tok.End))).ToImmutableList<EntityToken>());
				}
				return sample2.Select(func).ToImmutableList<ImmutableList<EntityToken>>();
			});
		}

		// Token: 0x0600FFFE RID: 65534 RVA: 0x00370178 File Offset: 0x0036E378
		private IReadOnlyList<EntityBasedTokenizer> DetectEntities()
		{
			IEnumerable<string> enumerable = this.ColumnData.DeterministicallySample(256);
			IReadOnlyList<string> sample = (enumerable as IReadOnlyList<string>) ?? enumerable.ToList<string>();
			List<EntityBasedTokenizer> list = new List<EntityBasedTokenizer>();
			Dictionary<EntityBasedTokenizer, Task<ImmutableList<ImmutableList<EntityToken>>>> dictionary = ColumnProfile.AllExtractors.ToDictionary((KeyValuePair<EntityDescriptor, EntityBasedTokenizer> t) => t.Value, (KeyValuePair<EntityDescriptor, EntityBasedTokenizer> t) => ColumnProfile._MakeTokenizationTask(t.Value, sample));
			List<List<EntityToken>> list2 = sample.Select((string _) => new List<EntityToken>()).ToList<List<EntityToken>>();
			foreach (KeyValuePair<EntityBasedTokenizer, Task<ImmutableList<ImmutableList<EntityToken>>>> keyValuePair in dictionary)
			{
				ImmutableList<ImmutableList<EntityToken>> result = keyValuePair.Value.Result;
				EntityBasedTokenizer key = keyValuePair.Key;
				if (result.Any((ImmutableList<EntityToken> tokens) => tokens.Any<EntityToken>()))
				{
					list.Add(key);
				}
				for (int i = 0; i < result.Count; i++)
				{
					list2[i].AddRange(result[i]);
				}
			}
			List<Dictionary<EntityType, int>> list3 = list2.Select(new Func<List<EntityToken>, Dictionary<EntityType, int>>(this.GetEntityCountsInLine)).ToList<Dictionary<EntityType, int>>();
			Dictionary<EntityType, List<int>> dictionary2 = (from EntityType v in Enum.GetValues(typeof(EntityType))
				where v > EntityType.Unknown
				select v).ToDictionary((EntityType entityType) => entityType, (EntityType entityType) => new List<int>(from _ in Enumerable.Range(0, sample.Count)
				select 0));
			for (int j = 0; j < list3.Count; j++)
			{
				foreach (KeyValuePair<EntityType, int> keyValuePair2 in list3[j])
				{
					List<int> list4 = dictionary2[keyValuePair2.Key];
					int num = j;
					list4[num] += keyValuePair2.Value;
				}
			}
			this.EntityCounts = dictionary2.ToImmutableDictionary((KeyValuePair<EntityType, List<int>> kvp) => kvp.Key, delegate(KeyValuePair<EntityType, List<int>> kvp)
			{
				List<int> list5 = kvp.Value.OrderByDescending((int c) => c).ToList<int>();
				int num2 = Math.Min((int)Math.Ceiling(0.51 * (double)kvp.Value.Count), list5.Count - 1);
				return list5[num2];
			});
			this.EntityInstances = this.EntityCounts.SelectMany((KeyValuePair<EntityType, int> kvp) => from idx in Enumerable.Range(0, kvp.Value)
				select new EntityInstance(kvp.Key, idx)).ToImmutableList<EntityInstance>();
			this._entityInstanceSet = this.EntityInstances.ToImmutableHashSet<EntityInstance>();
			return list;
		}

		// Token: 0x0600FFFF RID: 65535 RVA: 0x00370468 File Offset: 0x0036E668
		private Dictionary<EntityType, int> GetEntityCountsInLine(List<EntityToken> allTokensInLine)
		{
			return (from t in TokenFilters.ResolveSubsumptionByPrecedence(allTokensInLine)
				group t by t.GetType()).ToDictionary((IGrouping<Type, EntityToken> g) => EntityMappings.TypeToEntityType[g.Key], (IGrouping<Type, EntityToken> g) => g.Count<EntityToken>());
		}

		// Token: 0x06010000 RID: 65536 RVA: 0x003704E2 File Offset: 0x0036E6E2
		public static Task<ColumnProfile> CreateAsync(string columnName, IReadOnlyList<string> columnData, bool allowSubsumedEntities = false)
		{
			return Task.Run<ColumnProfile>(() => new ColumnProfile(columnName, columnData));
		}

		// Token: 0x06010001 RID: 65537 RVA: 0x00370508 File Offset: 0x0036E708
		public static Task<ImmutableList<ColumnProfile>> CreateAsync(IEnumerable<Tuple<string, IReadOnlyList<string>>> columns, bool allowSubsumedEntities = false)
		{
			Task<ColumnProfile>[] tasks = columns.Select((Tuple<string, IReadOnlyList<string>> c) => ColumnProfile.CreateAsync(c.Item1, c.Item2, false)).ToArray<Task<ColumnProfile>>();
			return Task.Run<ImmutableList<ColumnProfile>>(() => tasks.Select((Task<ColumnProfile> t) => t.Result).ToImmutableList<ColumnProfile>());
		}

		// Token: 0x06010002 RID: 65538 RVA: 0x0037055A File Offset: 0x0036E75A
		public IEnumerable<Constraint<IRow, object>> ExtractionConstraintsFor(EntityInstance entityInstance)
		{
			if (!this._entityInstanceSet.Contains(entityInstance))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("EntityInstance \"{0}\"  was not detected in the ColumnProfile object.", new object[] { entityInstance })));
			}
			yield return new ExternalEntityExtraction(this.TokenizerCollection.CreateExtractorFor(entityInstance.EntityType), entityInstance.InstanceNumber, this.ColumnName);
			yield break;
		}

		// Token: 0x06010003 RID: 65539 RVA: 0x00370574 File Offset: 0x0036E774
		public IEnumerable<Record<int, int>?> ExtractBoundaries(EntityInstance entityInstance)
		{
			TokenizerCollectionToExtractor extractor = this.TokenizerCollection.CreateExtractorFor(entityInstance.EntityType);
			int idx = entityInstance.InstanceNumber;
			return this.ColumnData.Select(delegate(string v)
			{
				IReadOnlyList<Record<uint, uint>> readOnlyList = extractor.Extract(v);
				if (idx >= readOnlyList.Count)
				{
					return null;
				}
				return new Record<int, int>?(Record.Create<int, int>((int)readOnlyList[idx].Item1, (int)readOnlyList[idx].Item2));
			});
		}

		// Token: 0x06010004 RID: 65540 RVA: 0x003705C3 File Offset: 0x0036E7C3
		public IEnumerable<string> ExtractValues(EntityInstance entityInstance)
		{
			return this.ExtractBoundaries(entityInstance).Zip(this.ColumnData, delegate(Record<int, int>? boundaries, string str)
			{
				if (boundaries == null)
				{
					return null;
				}
				return str.Substring(boundaries.Value.Item1, boundaries.Value.Item2 - boundaries.Value.Item1);
			});
		}

		// Token: 0x0400603A RID: 24634
		public const double Threshold = 0.51;

		// Token: 0x0400603B RID: 24635
		public const int RowSampleSize = 256;

		// Token: 0x04006041 RID: 24641
		private ImmutableHashSet<EntityInstance> _entityInstanceSet;

		// Token: 0x04006042 RID: 24642
		private static readonly Lazy<IReadOnlyList<KeyValuePair<EntityDescriptor, EntityBasedTokenizer>>> AllExtractorsLazy = new Lazy<IReadOnlyList<KeyValuePair<EntityDescriptor, EntityBasedTokenizer>>>(new Func<IReadOnlyList<KeyValuePair<EntityDescriptor, EntityBasedTokenizer>>>(ColumnProfile.BuildAllExtractorList));
	}
}
