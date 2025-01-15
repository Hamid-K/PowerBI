using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent.DotNet4;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000F8 RID: 248
	[Serializable]
	public class EditTransformationProvider : ITransformationProvider, ITransformationFiltering, IObjectReferenceContainer, IProviderInitialize, IRowsetConsumer, IMemoryLimit, IMemoryUsage, ISessionable, IPairSpecificTransformationProvider
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0002DF36 File Offset: 0x0002C136
		// (set) Token: 0x06000A29 RID: 2601 RVA: 0x0002DF3E File Offset: 0x0002C13E
		public EditTransformationProvider.EditTransformationCache PrecomputedEditCache { get; private set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0002DF47 File Offset: 0x0002C147
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x0002DF4F File Offset: 0x0002C14F
		public ITransformationFilter TransformationFilter { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0002DF58 File Offset: 0x0002C158
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x0002DF60 File Offset: 0x0002C160
		public bool EnumerateSimilarTokens { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0002DF69 File Offset: 0x0002C169
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x0002DF71 File Offset: 0x0002C171
		public bool SortTransformationMatches { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0002DF7A File Offset: 0x0002C17A
		// (set) Token: 0x06000A31 RID: 2609 RVA: 0x0002DF82 File Offset: 0x0002C182
		public int MinPrefixMatchLength { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0002DF8B File Offset: 0x0002C18B
		// (set) Token: 0x06000A33 RID: 2611 RVA: 0x0002DF93 File Offset: 0x0002C193
		public double Threshold
		{
			get
			{
				return this.m_threshold;
			}
			set
			{
				this.m_threshold = value;
				this.ClearCache();
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0002DFA2 File Offset: 0x0002C1A2
		// (set) Token: 0x06000A35 RID: 2613 RVA: 0x0002DFAA File Offset: 0x0002C1AA
		public int MaxEdits
		{
			get
			{
				return this.m_maxEdits;
			}
			set
			{
				this.m_maxEdits = value;
				this.ClearCache();
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0002DFB9 File Offset: 0x0002C1B9
		// (set) Token: 0x06000A37 RID: 2615 RVA: 0x0002DFC1 File Offset: 0x0002C1C1
		public bool NormalizeByMaxLength { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0002DFCA File Offset: 0x0002C1CA
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x0002DFD2 File Offset: 0x0002C1D2
		public string ReferenceRowsetName { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0002DFDB File Offset: 0x0002C1DB
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x0002DFE3 File Offset: 0x0002C1E3
		public string PrecomputeEditsRowsetName { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0002DFEC File Offset: 0x0002C1EC
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x0002DFF4 File Offset: 0x0002C1F4
		public string DomainName { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0002DFFD File Offset: 0x0002C1FD
		public IList<IRowsetSink> RowsetSinks
		{
			get
			{
				return new IRowsetSink[] { this.m_referenceRowsetSink, this.m_precomputedEditsRowsetSink };
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0002E017 File Offset: 0x0002C217
		public IStatistics Statistics
		{
			get
			{
				return this.m_statistics;
			}
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002E020 File Offset: 0x0002C220
		public EditTransformationProvider()
		{
			this.ReferenceRowsetName = "default";
			this.PrecomputeEditsRowsetName = string.Empty;
			this.PrecomputedEditCache = new EditTransformationProvider.EditTransformationCache(this);
			this.m_referenceRowsetSink = new EditTransformationProvider.ReferenceRowsetSink
			{
				Name = "ReferenceRowset",
				Provider = this
			};
			this.m_precomputedEditsRowsetSink = new EditTransformationProvider.PrecomputedEditsRowsetSink
			{
				Name = "PrecomputedEditsRowset",
				Provider = this
			};
			this.SortTransformationMatches = false;
			this.EnumerateSimilarTokens = false;
			this.m_trie = new Trie();
			this.m_mruCache = new MruCachedDictionary<StringExtent, List<Transformation>>(StringExtentEqualityComparer.Instance, new Func<StringExtent, List<Transformation>, long>(EditTransformationProvider.GetMemoryUsage), null, null, 1);
			this.m_mruCache.RawSerializationDelegate = new Action<Stream, StringExtent, List<Transformation>>(this.SerializeEditTransformation);
			this.m_mruCache.RawDeserializationDelegate = new MruCachedDictionary<StringExtent, List<Transformation>>.DeserializationDelegate(this.DeserializeEditTransformation);
			this.m_mruCache2 = new ConcurrentDictionary<StringExtent, List<Transformation>>();
			this.MemoryLimit = 500L;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002E131 File Offset: 0x0002C331
		public void Initialize(IDomainManager domainManager, string domainName)
		{
			this.DomainName = domainName;
			this.m_domainId = domainManager.GetDomainId(this.DomainName);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002E14C File Offset: 0x0002C34C
		public void AcquireReferences()
		{
			if (this.TransformationFilter is IObjectReferenceContainer)
			{
				(this.TransformationFilter as IObjectReferenceContainer).AcquireReferences();
			}
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002E16B File Offset: 0x0002C36B
		public void UpdateReferences()
		{
			if (this.TransformationFilter is IObjectReferenceContainer)
			{
				(this.TransformationFilter as IObjectReferenceContainer).UpdateReferences();
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002E18A File Offset: 0x0002C38A
		public void ReleaseReferences()
		{
			if (this.TransformationFilter is IObjectReferenceContainer)
			{
				(this.TransformationFilter as IObjectReferenceContainer).ReleaseReferences();
			}
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002E1A9 File Offset: 0x0002C3A9
		void IRowsetConsumer.RequestRowsets(IRowsetDistributor rowsetDistributor)
		{
			if (!string.IsNullOrEmpty(this.ReferenceRowsetName))
			{
				rowsetDistributor.RequestRowset(this.ReferenceRowsetName, this.m_referenceRowsetSink);
			}
			if (!string.IsNullOrEmpty(this.PrecomputeEditsRowsetName))
			{
				rowsetDistributor.RequestRowset(this.PrecomputeEditsRowsetName, this.m_precomputedEditsRowsetSink);
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002E1EC File Offset: 0x0002C3EC
		public ISession CreateSession()
		{
			EditTransformationProvider.Session session = new EditTransformationProvider.Session();
			if (this.TransformationFilter != null && this.TransformationFilter is ISessionable)
			{
				session.m_filterSession = (this.TransformationFilter as ISessionable).CreateSession();
			}
			return session;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002E22B File Offset: 0x0002C42B
		public void Clear()
		{
			this.m_trie.Clear();
			this.ClearCache();
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002E23E File Offset: 0x0002C43E
		public void ClearCache()
		{
			this.m_mruCache.Clear();
			this.m_mruCache2.Clear();
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002E258 File Offset: 0x0002C458
		private void SerializeEditTransformation(Stream s, StringExtent token, List<Transformation> editTransforms)
		{
			StreamUtilities.WriteStringExtent(s, token);
			StreamUtilities.WriteInt32(s, editTransforms.Count);
			BinaryWriter binaryWriter = new BinaryWriter(s);
			for (int i = 0; i < editTransforms.Count; i++)
			{
				editTransforms[i].Write(binaryWriter);
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002E2A0 File Offset: 0x0002C4A0
		private void DeserializeEditTransformation(Stream s, out StringExtent token, out List<Transformation> editTransforms)
		{
			token = StreamUtilities.ReadStringExtent(s);
			int num = StreamUtilities.ReadInt32(s);
			editTransforms = new List<Transformation>(num);
			BinaryReader binaryReader = new BinaryReader(s);
			for (int i = 0; i < num; i++)
			{
				Transformation transformation = default(Transformation);
				transformation.Read(binaryReader, HeapSegmentAllocator<int>.Instance, HeapSegmentAllocator<byte>.Instance);
				editTransforms.Add(transformation);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0002E2FD File Offset: 0x0002C4FD
		public long MemoryUsage
		{
			get
			{
				return this.m_mruCache.MemoryUsage;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0002E30A File Offset: 0x0002C50A
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x0002E325 File Offset: 0x0002C525
		public long MemoryLimit
		{
			get
			{
				return this.m_mruCache.MemoryLimit / 1024L * 1024L;
			}
			set
			{
				this.m_mruCache.MemoryLimit = value * 1024L * 1024L;
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002E341 File Offset: 0x0002C541
		private static long GetMemoryUsage(StringExtent token, List<Transformation> matches)
		{
			return 20L + token.MemoryUsage + (long)(matches.Count * 92);
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0002E359 File Offset: 0x0002C559
		public IEnumerable<StringExtent> Dictionary
		{
			get
			{
				foreach (Trie.MatchResult matchResult in this.m_trie.Items)
				{
					yield return new StringExtent(matchResult.Word, 0, matchResult.Length);
				}
				IEnumerator<Trie.MatchResult> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0002E369 File Offset: 0x0002C569
		private void AssertMode(FLMode mode)
		{
			if (this.m_mode != mode)
			{
				throw new InvalidOperationException(string.Format(StringResources.InvalidMode, Enum.GetName(typeof(FLMode), mode)));
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002E39C File Offset: 0x0002C59C
		public void SetCharDeletionCost(string s, short cost)
		{
			for (int i = 0; i < s.Length; i++)
			{
				char c = s.get_Chars(i);
				this.m_trie.SetCharCost(c, '\0', cost);
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002E3D2 File Offset: 0x0002C5D2
		public void SetCharDeletionCost(char c, short cost)
		{
			this.m_trie.SetCharCost(c, '\0', cost);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002E3E4 File Offset: 0x0002C5E4
		public void SetCharInsertionCost(string s, short cost)
		{
			for (int i = 0; i < s.Length; i++)
			{
				char c = s.get_Chars(i);
				this.m_trie.SetCharCost('\0', c, cost);
			}
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002E41A File Offset: 0x0002C61A
		public void SetCharInsertionCost(char c, short cost)
		{
			this.m_trie.SetCharCost('\0', c, cost);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002E42A File Offset: 0x0002C62A
		public void SetCharCost(char from, short cost)
		{
			this.m_trie.SetCharCost(from, cost);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002E439 File Offset: 0x0002C639
		public void SetCharCost(char from, char to, short cost)
		{
			this.m_trie.SetCharCost(from, to, cost);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002E449 File Offset: 0x0002C649
		public void SetCharCost(char precedingChar, char from, char to, short cost)
		{
			this.m_trie.SetCharCost(precedingChar, from, to, cost);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002E45B File Offset: 0x0002C65B
		public void Add(IUpdateContext _updateContext, StringExtent token)
		{
			this.m_trie.Add<StringExtent>(token);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002E469 File Offset: 0x0002C669
		public void Add(IUpdateContext _updateContext, string token)
		{
			this.m_trie.Add(token);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0002E478 File Offset: 0x0002C678
		public void Match(ISession _session, ITokenIdProvider tokenIdProvider, IDataRecord leftRecord, IDataRecord rightRecord, RecordBinding leftBinding, RecordBinding rightBinding, TokenSequence leftTokenSeq, TokenSequence rightTokenSeq, out ArraySegment<TransformationMatch> leftTransformationMatchList, out ArraySegment<TransformationMatch> rightTransformationMatchList)
		{
			EditTransformationProvider.Session session = _session as EditTransformationProvider.Session;
			session.Reset();
			if (session.m_pairSpecificTrie == null)
			{
				session.m_pairSpecificTrie = new Trie();
			}
			Trie pairSpecificTrie = session.m_pairSpecificTrie;
			pairSpecificTrie.Clear();
			for (int i = 0; i < rightTokenSeq.Count; i++)
			{
				int num = rightTokenSeq[i];
				if (tokenIdProvider.GetDomainId(num) == this.m_domainId)
				{
					StringExtent token = tokenIdProvider.GetToken(num);
					pairSpecificTrie.Add<StringExtent>(token);
				}
			}
			this.MatchInternal(_session, tokenIdProvider, leftTokenSeq, pairSpecificTrie, true, out leftTransformationMatchList);
			rightTransformationMatchList = default(ArraySegment<TransformationMatch>);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002E503 File Offset: 0x0002C703
		public void Match(ISession _session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSeq, out ArraySegment<TransformationMatch> transformationMatchList)
		{
			this.MatchInternal(_session, tokenIdProvider, tokenSeq, this.m_trie, false, out transformationMatchList);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002E518 File Offset: 0x0002C718
		private void MatchInternal(ISession _session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSeq, Trie editTrie, bool noCache, out ArraySegment<TransformationMatch> transformationMatchList)
		{
			EditTransformationProvider.Session session = _session as EditTransformationProvider.Session;
			if (this.TransformationFilter != null)
			{
				this.TransformationFilter.Prepare(session.m_filterSession, tokenIdProvider, tokenSeq);
			}
			this.m_statistics.LeftTokenCount += (long)tokenSeq.Count;
			for (int i = 0; i < tokenSeq.Count; i++)
			{
				int num = tokenSeq[i];
				if (tokenIdProvider.GetDomainId(num) == this.m_domainId && (this.TransformationFilter == null || this.TransformationFilter.AllowTransformations(session.m_filterSession, i)))
				{
					List<Transformation> list = null;
					StringExtent token = tokenIdProvider.GetToken(num);
					if (!noCache)
					{
						this.m_mruCache2.TryGetValue(token, out list);
					}
					if (list == null)
					{
						session.m_tempTransformationList.Clear();
						list = session.m_tempTransformationList;
						bool flag = false;
						if (!noCache)
						{
							if (this.m_statistics.EnableTimers)
							{
								this.m_statistics.PrecomputeMatchTime.Start();
							}
							flag = this.PrecomputedEditCache.Match(session, tokenSeq, i, list);
							if (this.m_statistics.EnableTimers)
							{
								this.m_statistics.PrecomputeMatchTime.Stop();
							}
							if (flag)
							{
								EditTransformationProvider.s_globalStatistics.PrecomputedCacheHits += 1L;
							}
						}
						if (!flag)
						{
							EditTransformationProvider.s_globalStatistics.MruCacheMisses += 1L;
							if (this.m_statistics.EnableTimers)
							{
								this.m_statistics.TrieMatchTime.Start();
							}
							int length = token.Length;
							int num2 = (this.NormalizeByMaxLength ? Math.Min(this.MaxEdits, (int)Math.Ceiling(((double)length - this.m_threshold * (double)length) / this.m_threshold)) : ((int)Math.Ceiling((double)token.Length * (1.0 - this.m_threshold))));
							session.m_trieMatchContext.Threshold = this.m_threshold;
							List<int> tempSimilarTokenIds = session.m_tempSimilarTokenIds;
							List<EditTransformationMetadata> tempMetadataList = session.m_tempMetadataList;
							tempSimilarTokenIds.Clear();
							tempMetadataList.Clear();
							foreach (Trie.MatchResult matchResult in editTrie.FindSimilar<StringExtent>(session.m_trieMatchContext, token, num2 * 10))
							{
								StringExtent stringExtent = new StringExtent(matchResult.Word, 0, matchResult.Length);
								int orCreateTokenId = tokenIdProvider.GetOrCreateTokenId(stringExtent, this.m_domainId);
								if (orCreateTokenId != num)
								{
									int num3 = 0;
									int num4 = 0;
									while (num4 < token.Length && num4 < stringExtent.Length && token[num4] == stringExtent[num4])
									{
										num3++;
										num4++;
									}
									if (num3 >= this.MinPrefixMatchLength)
									{
										EditTransformationMetadata editTransformationMetadata = new EditTransformationMetadata
										{
											Numerator = (short)matchResult.Distance,
											Denominator = (short)(10 * (this.NormalizeByMaxLength ? Math.Max(token.Length, stringExtent.Length) : token.Length)),
											PrefixMatchLength = (short)num3,
											MaxLength = (short)Math.Max(token.Length, stringExtent.Length)
										};
										EditTransformationMetadata editTransformationMetadata2 = editTransformationMetadata;
										tempMetadataList.Add(editTransformationMetadata2);
										tempSimilarTokenIds.Add(orCreateTokenId);
									}
								}
							}
							ArraySegment<byte> arraySegment;
							ArraySegment<int> arraySegment2;
							if (noCache)
							{
								arraySegment = session.m_metadataAllocator.New(tempSimilarTokenIds.Count);
								arraySegment2 = session.m_tokenIdAllocator.New(1 + tempSimilarTokenIds.Count);
							}
							else
							{
								arraySegment = HeapSegmentAllocator<byte>.Instance.New(tempSimilarTokenIds.Count * 8);
								arraySegment2 = HeapSegmentAllocator<int>.Instance.New(1 + tempSimilarTokenIds.Count);
								list = new List<Transformation>(tempSimilarTokenIds.Count);
							}
							arraySegment2.Array[arraySegment2.Offset] = num;
							for (int j = 0; j < tempSimilarTokenIds.Count; j++)
							{
								arraySegment2.Array[arraySegment2.Offset + j + 1] = tempSimilarTokenIds[j];
								Transformation transformation = new Transformation
								{
									From = new TokenSequence(arraySegment2.Array, arraySegment2.Offset, 1),
									To = new TokenSequence(arraySegment2.Array, arraySegment2.Offset + j + 1, 1),
									Type = TransformationType.EditTransformation,
									Metadata = new ArraySegment<byte>(arraySegment.Array, arraySegment.Offset + j * 8, 8)
								};
								EditTransformationMetadata editTransformationMetadata = tempMetadataList[j];
								editTransformationMetadata.Write(transformation.Metadata);
								list.Add(transformation);
							}
							EditTransformationProvider.s_globalStatistics.MaxUnfilteredMatchesFound = Math.Max(EditTransformationProvider.s_globalStatistics.MaxUnfilteredMatchesFound, (long)list.Count);
							if (this.m_statistics.EnableTimers)
							{
								this.m_statistics.TrieMatchTime.Stop();
							}
							if (this.SortTransformationMatches)
							{
								list.Sort(new Comparison<Transformation>(EditTransformationProvider.CompareByEditDistanceAsc));
							}
							if (!noCache)
							{
								this.m_mruCache2.TryAdd(token.AllocClone(), list);
							}
						}
					}
					else
					{
						EditTransformationProvider.s_globalStatistics.MruCacheHits += 1L;
					}
					if (list.Count > 0)
					{
						session.NewTokenSequence(num);
						int num5 = 0;
						foreach (Transformation transformation2 in list)
						{
							EditTransformationMetadata editTransformationMetadata3 = new EditTransformationMetadata(transformation2.Metadata);
							if (1.0 - (double)editTransformationMetadata3.EditDistance >= this.Threshold && (int)editTransformationMetadata3.PrefixMatchLength >= this.MinPrefixMatchLength)
							{
								TokenSequence from = transformation2.From;
								if (from[0] == num)
								{
									if (this.TransformationFilter == null || this.TransformationFilter.AllowTransformation(session.m_filterSession, i, transformation2))
									{
										session.m_overallTranMatchBuilder.Add(new TransformationMatch
										{
											Position = i,
											Transformation = transformation2
										});
										num5++;
									}
								}
								else if (this.TransformationFilter == null || this.TransformationFilter.AllowTransformation(session.m_filterSession, i, transformation2))
								{
									Transformation transformation3 = new Transformation
									{
										From = session.NewTokenSequence(num),
										To = transformation2.To,
										Type = TransformationType.EditTransformation,
										Metadata = transformation2.Metadata
									};
									session.m_overallTranMatchBuilder.Add(new TransformationMatch
									{
										Position = i,
										Transformation = transformation3
									});
									num5++;
								}
							}
						}
						this.m_statistics.MatchedRuleCount += (long)num5;
					}
				}
			}
			if (this.TransformationFilter != null && session.m_overallTranMatchBuilder.Count > 0)
			{
				if (this.m_statistics.EnableTimers)
				{
					this.m_statistics.MatchFilterTime.Start();
				}
				this.TransformationFilter.FilterTransformations(session.m_filterSession, session.m_overallTranMatchBuilder, out transformationMatchList);
				if (this.m_statistics.EnableTimers)
				{
					this.m_statistics.MatchFilterTime.Stop();
				}
			}
			else
			{
				transformationMatchList = session.m_overallTranMatchBuilder;
			}
			this.m_statistics.FilteredRuleCount += (long)transformationMatchList.Count;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002EC70 File Offset: 0x0002CE70
		private static int CompareByEditDistanceAsc(Transformation x, Transformation y)
		{
			EditTransformationMetadata editTransformationMetadata = new EditTransformationMetadata(x.Metadata);
			EditTransformationMetadata editTransformationMetadata2 = new EditTransformationMetadata(y.Metadata);
			if (editTransformationMetadata.EditDistance == editTransformationMetadata2.EditDistance)
			{
				return 0;
			}
			if (editTransformationMetadata.EditDistance <= editTransformationMetadata2.EditDistance)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002ECBC File Offset: 0x0002CEBC
		public IEnumerable<Transformation> Transformations(ITokenIdProvider tokenIdProvider)
		{
			if (this.EnumerateSimilarTokens)
			{
				EditTransformationProvider.Session session = (EditTransformationProvider.Session)this.CreateSession();
				TokenSequence lhsSeq = new TokenSequence(new int[1]);
				foreach (Trie.MatchResult matchResult in this.m_trie.Items)
				{
					StringExtent stringExtent = new StringExtent(matchResult.Word, 0, matchResult.Length);
					int orCreateTokenId = tokenIdProvider.GetOrCreateTokenId(stringExtent, this.m_domainId);
					lhsSeq.Array[lhsSeq.Offset] = orCreateTokenId;
					session.Reset();
					ArraySegment<TransformationMatch> matches;
					this.Match(session, tokenIdProvider, lhsSeq, out matches);
					int num;
					for (int i = 0; i < matches.Count; i = num + 1)
					{
						yield return matches.Array[matches.Offset + i].Transformation;
						num = i;
					}
					matches = default(ArraySegment<TransformationMatch>);
				}
				IEnumerator<Trie.MatchResult> enumerator = null;
				session = null;
				lhsSeq = default(TokenSequence);
			}
			yield break;
			yield break;
		}

		// Token: 0x040003E0 RID: 992
		internal static EditTransformationProvider.GlobalStatistics s_globalStatistics = new EditTransformationProvider.GlobalStatistics();

		// Token: 0x040003E1 RID: 993
		private EditTransformationProvider.StatisticsInfo m_statistics = new EditTransformationProvider.StatisticsInfo();

		// Token: 0x040003E2 RID: 994
		private double m_threshold = 0.65;

		// Token: 0x040003E3 RID: 995
		private int m_maxEdits = int.MaxValue;

		// Token: 0x040003E4 RID: 996
		internal int m_domainId;

		// Token: 0x040003E5 RID: 997
		private FLMode m_mode;

		// Token: 0x040003E6 RID: 998
		private Trie m_trie;

		// Token: 0x040003E7 RID: 999
		private MruCachedDictionary<StringExtent, List<Transformation>> m_mruCache;

		// Token: 0x040003E8 RID: 1000
		private ConcurrentDictionary<StringExtent, List<Transformation>> m_mruCache2;

		// Token: 0x040003E9 RID: 1001
		private EditTransformationProvider.ReferenceRowsetSink m_referenceRowsetSink;

		// Token: 0x040003EA RID: 1002
		private EditTransformationProvider.PrecomputedEditsRowsetSink m_precomputedEditsRowsetSink;

		// Token: 0x02000194 RID: 404
		[Serializable]
		private sealed class PrecomputeEditDataReader : SimpleDataReader
		{
			// Token: 0x17000277 RID: 631
			// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00039408 File Offset: 0x00037608
			// (set) Token: 0x06000D6B RID: 3435 RVA: 0x00039410 File Offset: 0x00037610
			private string DomainName { get; set; }

			// Token: 0x06000D6C RID: 3436 RVA: 0x0003941C File Offset: 0x0003761C
			private static DataTable CreateSchemaTable()
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("From", typeof(string));
				dataTable.Columns.Add("To", typeof(string));
				dataTable.Columns.Add("Distance", typeof(float));
				return dataTable.CreateDataReader().GetSchemaTable();
			}

			// Token: 0x06000D6D RID: 3437 RVA: 0x0003948C File Offset: 0x0003768C
			public PrecomputeEditDataReader(EditTransformationProvider editProvider, ITokenIdProvider tokenIdProvider, int domainId, IEnumerator<StringExtent> leftSideTokens, bool skipTokensAlreadyInCache, bool addTransformationsToCache, EditTransformationProvider.EditTransformationCache cache)
				: base(EditTransformationProvider.PrecomputeEditDataReader.CreateSchemaTable())
			{
				this.m_editProvider = editProvider;
				this.m_updateSession = this.m_editProvider.CreateSession();
				this.m_leftSideTokens = leftSideTokens;
				this.m_skipTokensAlreadyInCache = skipTokensAlreadyInCache;
				this.m_addTransformationsToCache = addTransformationsToCache;
				this.m_cache = cache;
				this.m_idProvider = tokenIdProvider;
				this.m_domainId = domainId;
				DataTable schemaTable = this.GetSchemaTable();
				this.m_record = new SimpleDataRecord(schemaTable, new object[schemaTable.Rows.Count]);
			}

			// Token: 0x06000D6E RID: 3438 RVA: 0x00039530 File Offset: 0x00037730
			public PrecomputeEditDataReader(EditTransformationProvider editProvider, ITokenIdProvider tokenIdProvider, IDataReader records, RecordBinding recordDomainBinding, string domainName, IRecordTokenizer tokenizer, bool skipTokensAlreadyInCache, bool addTransformationsToCache, EditTransformationProvider.EditTransformationCache cache)
				: base(EditTransformationProvider.PrecomputeEditDataReader.CreateSchemaTable())
			{
				this.m_editProvider = editProvider;
				this.m_updateSession = this.m_editProvider.CreateSession();
				this.m_dataReader = records;
				this.m_tokenizer = tokenizer;
				this.m_skipTokensAlreadyInCache = skipTokensAlreadyInCache;
				this.m_addTransformationsToCache = addTransformationsToCache;
				this.m_cache = cache;
				this.m_recordDomainBinding = recordDomainBinding;
				this.DomainName = domainName;
				this.m_idProvider = tokenIdProvider;
				DataTable schemaTable = this.GetSchemaTable();
				this.m_record = new SimpleDataRecord(schemaTable, new object[schemaTable.Rows.Count]);
			}

			// Token: 0x17000278 RID: 632
			// (get) Token: 0x06000D6F RID: 3439 RVA: 0x000395E4 File Offset: 0x000377E4
			protected override IDataRecord Current
			{
				get
				{
					if (this.m_tranMatchIndex >= this.m_tranMatchList.Count)
					{
						throw new InvalidOperationException("There is no current record.  Be sure to call Read().");
					}
					return this.m_record;
				}
			}

			// Token: 0x06000D70 RID: 3440 RVA: 0x0003960C File Offset: 0x0003780C
			private bool NextQueryToken()
			{
				if (this.m_leftSideTokens != null && this.m_leftSideTokens.MoveNext())
				{
					this.m_currentQueryToken = this.m_idProvider.GetOrCreateTokenId(this.m_leftSideTokens.Current, this.m_domainId);
					return !this.m_skipTokensAlreadyInCache || !this.m_cache.Contains(this.m_currentQueryToken) || this.NextQueryToken();
				}
				if (this.m_recordTokenIdEnumerator != null && this.m_recordTokenIdEnumerator.MoveNext())
				{
					this.m_currentQueryToken = this.m_recordTokenIdEnumerator.Current;
					return !this.m_skipTokensAlreadyInCache || !this.m_cache.Contains(this.m_currentQueryToken) || this.NextQueryToken();
				}
				if (this.m_dataReader != null && this.m_dataReader.Read())
				{
					this.m_tokenIdSegmentBuilder.Reset();
					this.m_tokenizerContext.Reset();
					this.m_idProvider.GetOrCreateTokenIds(this.m_tokenizer.Tokenize(this.m_tokenizerContext, this.m_dataReader), this.m_domainId, this.m_tokenIdSegmentBuilder);
					TokenSequence tokenSequence = new TokenSequence(this.m_tokenIdSegmentBuilder);
					this.m_recordTokenIdEnumerator = new ArraySegment32<int>.Enumerator(tokenSequence);
					return this.NextQueryToken();
				}
				return false;
			}

			// Token: 0x06000D71 RID: 3441 RVA: 0x00039744 File Offset: 0x00037944
			public override bool Read()
			{
				if (this.m_tokenizerContext == null)
				{
					this.m_tokenizer.Prepare(this.m_recordDomainBinding.Schema, this.m_recordDomainBinding.GetDomainBinding(this.DomainName), out this.m_tokenizerContext);
				}
				if (this.m_tranMatchIndex < this.m_tranMatchList.Count - 1)
				{
					this.m_tranMatchIndex++;
					Transformation transformation = this.m_tranMatchList.Array[this.m_tranMatchList.Offset + this.m_tranMatchIndex].Transformation;
					this.m_record.Values[0] = this.m_idProvider.GetToken(transformation.From[0]).ToString();
					this.m_record.Values[1] = this.m_idProvider.GetToken(transformation.To[0]).ToString();
					this.m_record.Values[2] = new EditTransformationMetadata(transformation.Metadata).EditDistance;
					return true;
				}
				while (this.NextQueryToken())
				{
					this.m_tranMatchIndex = 1;
					this.m_tempTokenSeq.Array[this.m_tempTokenSeq.Offset] = this.m_currentQueryToken;
					this.m_updateSession.Reset();
					this.m_editProvider.Match(this.m_updateSession, this.m_idProvider, this.m_tempTokenSeq, out this.m_tranMatchList);
					if (this.m_tranMatchList.Count > 0)
					{
						if (this.m_addTransformationsToCache)
						{
							this.m_cache.Add(this.m_tranMatchList);
						}
						this.m_tranMatchIndex = -1;
						return this.Read();
					}
				}
				return false;
			}

			// Token: 0x04000689 RID: 1673
			private ITokenIdProvider m_idProvider;

			// Token: 0x0400068A RID: 1674
			private int m_domainId;

			// Token: 0x0400068B RID: 1675
			private ArraySegment<TransformationMatch> m_tranMatchList;

			// Token: 0x0400068C RID: 1676
			private TokenSequence m_tempTokenSeq = new TokenSequence(new int[1]);

			// Token: 0x0400068D RID: 1677
			private int m_tranMatchIndex = 1;

			// Token: 0x0400068E RID: 1678
			private SimpleDataRecord m_record;

			// Token: 0x0400068F RID: 1679
			private IEnumerator<StringExtent> m_leftSideTokens;

			// Token: 0x04000690 RID: 1680
			private IDataReader m_dataReader;

			// Token: 0x04000692 RID: 1682
			private RecordBinding m_recordDomainBinding;

			// Token: 0x04000693 RID: 1683
			private IRecordTokenizer m_tokenizer;

			// Token: 0x04000694 RID: 1684
			[NonSerialized]
			private TokenizerContext m_tokenizerContext;

			// Token: 0x04000695 RID: 1685
			private ArraySegmentBuilder<int> m_tokenIdSegmentBuilder = new ArraySegmentBuilder<int>();

			// Token: 0x04000696 RID: 1686
			private IEnumerator<int> m_recordTokenIdEnumerator;

			// Token: 0x04000697 RID: 1687
			private EditTransformationProvider m_editProvider;

			// Token: 0x04000698 RID: 1688
			private ISession m_updateSession;

			// Token: 0x04000699 RID: 1689
			private int m_currentQueryToken;

			// Token: 0x0400069A RID: 1690
			private bool m_skipTokensAlreadyInCache;

			// Token: 0x0400069B RID: 1691
			private bool m_addTransformationsToCache;

			// Token: 0x0400069C RID: 1692
			private EditTransformationProvider.EditTransformationCache m_cache;
		}

		// Token: 0x02000195 RID: 405
		[Serializable]
		public sealed class EditTransformationCache : IMemoryUsage
		{
			// Token: 0x06000D72 RID: 3442 RVA: 0x000398F4 File Offset: 0x00037AF4
			internal EditTransformationCache(EditTransformationProvider editProvider)
			{
				this.m_editProvider = editProvider;
				this.m_domainName = editProvider.DomainName;
				this.m_domainId = editProvider.m_domainId;
			}

			// Token: 0x06000D73 RID: 3443 RVA: 0x00039953 File Offset: 0x00037B53
			public void Clear()
			{
				this.m_fromId2ToIdsSegmentReference.Clear();
				this.m_transformIds.Reset();
				this.m_metadata.Reset();
			}

			// Token: 0x17000279 RID: 633
			// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00039976 File Offset: 0x00037B76
			public long MemoryUsage
			{
				get
				{
					return (long)(this.m_fromId2ToIdsSegmentReference.Count * 16) + this.m_transformIds.MemoryUsage + this.m_metadata.MemoryUsage;
				}
			}

			// Token: 0x06000D75 RID: 3445 RVA: 0x0003999F File Offset: 0x00037B9F
			public IDataReader PrecomputeTransformations(IEnumerable<StringExtent> leftSideTokens, ITokenIdProvider tokenIdProvider)
			{
				return this.PrecomputeTransformations(leftSideTokens, tokenIdProvider, true, true);
			}

			// Token: 0x06000D76 RID: 3446 RVA: 0x000399AB File Offset: 0x00037BAB
			public IDataReader PrecomputeTransformations(IEnumerable<StringExtent> leftSideTokens, ITokenIdProvider tokenIdProvider, bool skipTokensAlreadyInCache, bool addTransformationsToCache)
			{
				return new EditTransformationProvider.PrecomputeEditDataReader(this.m_editProvider, tokenIdProvider, this.m_domainId, leftSideTokens.GetEnumerator(), skipTokensAlreadyInCache, addTransformationsToCache, this);
			}

			// Token: 0x06000D77 RID: 3447 RVA: 0x000399C9 File Offset: 0x00037BC9
			public IDataReader PrecomputeTransformations(IDataReader records, RecordBinding recordDomainBinding, IDomainManager domainManager, ITokenIdProvider tokenIdProvider)
			{
				return this.PrecomputeTransformations(records, recordDomainBinding, domainManager, tokenIdProvider, true, true);
			}

			// Token: 0x06000D78 RID: 3448 RVA: 0x000399D8 File Offset: 0x00037BD8
			public IDataReader PrecomputeTransformations(IDataReader records, RecordBinding recordDomainBinding, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, bool skipTokensAlreadyInCache, bool addTransformationsToCache)
			{
				IRecordTokenizer tokenizer = domainManager.GetTokenizer(this.m_domainName);
				return new EditTransformationProvider.PrecomputeEditDataReader(this.m_editProvider, tokenIdProvider, records, recordDomainBinding, this.m_domainName, tokenizer, skipTokensAlreadyInCache, addTransformationsToCache, this);
			}

			// Token: 0x06000D79 RID: 3449 RVA: 0x00039A0D File Offset: 0x00037C0D
			public bool Contains(int tokenId)
			{
				return this.m_fromId2ToIdsSegmentReference.ContainsKey(tokenId);
			}

			// Token: 0x06000D7A RID: 3450 RVA: 0x00039A1C File Offset: 0x00037C1C
			public bool Match(ISession session, TokenSequence tokenSeq, int tokenPosition, List<Transformation> tranMatchList)
			{
				int num = tokenSeq[tokenPosition];
				long num2;
				if (this.m_fromId2ToIdsSegmentReference.TryGetValue(num, ref num2))
				{
					ArraySegment<int> arraySegment = this.m_transformIds[num2];
					ArraySegment<byte> arraySegment2 = this.m_metadata[num2];
					for (int i = 1; i < arraySegment.Count; i++)
					{
						tranMatchList.Add(new Transformation
						{
							From = new ArraySegment<int>(arraySegment.Array, arraySegment.Offset, 1),
							To = new ArraySegment<int>(arraySegment.Array, arraySegment.Offset + i, 1),
							Type = TransformationType.EditTransformation,
							Metadata = new ArraySegment<byte>(arraySegment2.Array, arraySegment2.Offset + i * 8, 8)
						});
					}
					return true;
				}
				return false;
			}

			// Token: 0x06000D7B RID: 3451 RVA: 0x00039AFC File Offset: 0x00037CFC
			internal void Add(IDataRecord record)
			{
				int num = (int)record[0];
				TransformationAggregate transformationAggregate = new TransformationAggregate();
				byte[] array = (byte[])record[1];
				transformationAggregate.Read(new BinaryReader(new MemoryStream(array)));
				int count = transformationAggregate.m_toTokens.Count;
				long num2;
				long num3;
				lock (this)
				{
					num2 = this.m_transformIds.New(count + 1);
					num3 = this.m_metadata.New(count + 1);
					this.m_fromId2ToIdsSegmentReference.Add(num, num2);
				}
				ArraySegment<int> arraySegment = this.m_transformIds[num2];
				ArraySegment<byte> arraySegment2 = this.m_metadata[num3];
				arraySegment.Array[arraySegment.Offset] = num;
				for (int i = 0; i < count; i++)
				{
					arraySegment.Array[arraySegment.Offset + i + 1] = transformationAggregate.m_toTokens[i];
					if (transformationAggregate.m_metadata[i] == null)
					{
						throw new Exception("Incoming metadata is null.");
					}
					if (transformationAggregate.m_metadata[i].Length != 8)
					{
						throw new Exception("Incoming metadata length does not match the expected length.");
					}
					transformationAggregate.m_metadata[i].CopyTo(arraySegment2.Array, arraySegment2.Offset + (i + 1) * 8);
				}
			}

			// Token: 0x06000D7C RID: 3452 RVA: 0x00039C5C File Offset: 0x00037E5C
			internal void Add(ArraySegment<TransformationMatch> editTransformations)
			{
				if (editTransformations.Count > 0)
				{
					long num = this.m_transformIds.New(editTransformations.Count + 1);
					long num2 = this.m_metadata.New(editTransformations.Count + 1);
					ArraySegment<int> arraySegment = this.m_transformIds[num];
					ArraySegment<byte> arraySegment2 = this.m_metadata[num2];
					int num3 = editTransformations.Array[editTransformations.Offset].Transformation.From[0];
					arraySegment.Array[arraySegment.Offset] = num3;
					for (int i = 0; i < editTransformations.Count; i++)
					{
						Transformation transformation = editTransformations.Array[editTransformations.Offset + i].Transformation;
						arraySegment.Array[arraySegment.Offset + i + 1] = transformation.To[0];
						Array.ConstrainedCopy(transformation.Metadata.Array, transformation.Metadata.Offset, arraySegment2.Array, arraySegment2.Offset + (i + 1) * 8, 8);
					}
					this.m_fromId2ToIdsSegmentReference.Add(num3, num);
				}
			}

			// Token: 0x06000D7D RID: 3453 RVA: 0x00039D88 File Offset: 0x00037F88
			public void Add(IDataReader editTransformationsReader, ITokenIdProvider tokenIdProvider)
			{
				int num = -1;
				List<int> list = new List<int>();
				List<float> list2 = new List<float>();
				while (editTransformationsReader.Read())
				{
					int orCreateTokenId = tokenIdProvider.GetOrCreateTokenId(new StringExtent(editTransformationsReader.GetString(0)), this.m_domainId);
					int orCreateTokenId2 = tokenIdProvider.GetOrCreateTokenId(new StringExtent(editTransformationsReader.GetString(1)), this.m_domainId);
					float num2 = (float)editTransformationsReader.GetDouble(2);
					if (orCreateTokenId != num)
					{
						if (num != -1)
						{
							if (!this.m_fromId2ToIdsSegmentReference.ContainsKey(num))
							{
								long num3 = this.m_transformIds.New(list.Count);
								long num4 = this.m_metadata.New(list2.Count);
								ArraySegment<int> arraySegment = this.m_transformIds[num3];
								ArraySegment<byte> arraySegment2 = this.m_metadata[num4];
								BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream(arraySegment2.Array, arraySegment2.Offset, arraySegment2.Count));
								for (int i = 0; i < list.Count; i++)
								{
									arraySegment.Array[arraySegment.Offset + i] = list[i];
									binaryWriter.Write(list2[i]);
								}
								this.m_fromId2ToIdsSegmentReference.Add(num, num3);
							}
							list.Clear();
							list2.Clear();
						}
						num = orCreateTokenId;
						list.Add(orCreateTokenId);
						list2.Add(0f);
					}
					list.Add(orCreateTokenId2);
					list2.Add(num2);
				}
			}

			// Token: 0x0400069D RID: 1693
			private EditTransformationProvider m_editProvider;

			// Token: 0x0400069E RID: 1694
			private string m_domainName;

			// Token: 0x0400069F RID: 1695
			private int m_domainId;

			// Token: 0x040006A0 RID: 1696
			private Dictionary<int, long> m_fromId2ToIdsSegmentReference = new Dictionary<int, long>();

			// Token: 0x040006A1 RID: 1697
			private BlockedSegmentArray64<int> m_transformIds = new BlockedSegmentArray64<int>(1, 262144);

			// Token: 0x040006A2 RID: 1698
			private BlockedSegmentArray64<byte> m_metadata = new BlockedSegmentArray64<byte>(8, 262144);
		}

		// Token: 0x02000196 RID: 406
		private class Session : ISession
		{
			// Token: 0x06000D7E RID: 3454 RVA: 0x00039EF0 File Offset: 0x000380F0
			public void Reset()
			{
				this.m_tokenIdAllocator.Reset();
				this.m_metadataAllocator.Reset();
				this.m_tempTransformationList.Clear();
				if (this.m_filterSession != null)
				{
					this.m_filterSession.Reset();
				}
				this.m_overallTranMatchBuilder.Reset();
			}

			// Token: 0x06000D7F RID: 3455 RVA: 0x00039F3C File Offset: 0x0003813C
			public Transformation NewEditTransformation()
			{
				return new Transformation
				{
					Type = TransformationType.EditTransformation,
					Metadata = this.m_metadataAllocator.New(1)
				};
			}

			// Token: 0x06000D80 RID: 3456 RVA: 0x00039F70 File Offset: 0x00038170
			public TokenSequence NewTokenSequence(int tokenId)
			{
				ArraySegment<int> arraySegment = this.m_tokenIdAllocator.New(1);
				arraySegment.Array[arraySegment.Offset] = tokenId;
				return new TokenSequence(arraySegment);
			}

			// Token: 0x040006A3 RID: 1699
			public ISession m_filterSession;

			// Token: 0x040006A4 RID: 1700
			public BlockedSegmentArray<int> m_tokenIdAllocator = new BlockedSegmentArray<int>();

			// Token: 0x040006A5 RID: 1701
			public BlockedSegmentArray<byte> m_metadataAllocator = new BlockedSegmentArray<byte>(8);

			// Token: 0x040006A6 RID: 1702
			public ArraySegmentBuilder<TransformationMatch> m_overallTranMatchBuilder = new ArraySegmentBuilder<TransformationMatch>();

			// Token: 0x040006A7 RID: 1703
			public List<Transformation> m_tempTransformationList = new List<Transformation>();

			// Token: 0x040006A8 RID: 1704
			public Trie.MatchContext m_trieMatchContext = new Trie.MatchContext(1);

			// Token: 0x040006A9 RID: 1705
			public Trie m_pairSpecificTrie;

			// Token: 0x040006AA RID: 1706
			public List<int> m_tempSimilarTokenIds = new List<int>();

			// Token: 0x040006AB RID: 1707
			public List<EditTransformationMetadata> m_tempMetadataList = new List<EditTransformationMetadata>();
		}

		// Token: 0x02000197 RID: 407
		[Serializable]
		private class StatisticsInfo : StatisticsBase, IDeserializationCallback
		{
			// Token: 0x06000D83 RID: 3459 RVA: 0x0003A02B File Offset: 0x0003822B
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				this.PrecomputeMatchTime = new Stopwatch();
				this.TrieMatchTime = new Stopwatch();
				this.MatchFilterTime = new Stopwatch();
			}

			// Token: 0x06000D84 RID: 3460 RVA: 0x0003A050 File Offset: 0x00038250
			public new void Reset()
			{
				this.LeftTokenCount = 0L;
				this.MatchedRuleCount = 0L;
				this.FilteredRuleCount = 0L;
				if (base.EnableTimers)
				{
					this.PrecomputeMatchTime.Reset();
					this.TrieMatchTime.Reset();
					this.MatchFilterTime.Reset();
				}
			}

			// Token: 0x06000D85 RID: 3461 RVA: 0x0003A0A0 File Offset: 0x000382A0
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(string.Format("LeftTokenCount: {0}", this.LeftTokenCount));
				stringBuilder.AppendLine(string.Format("MatchedRuleCount: {0}", this.MatchedRuleCount));
				stringBuilder.AppendLine(string.Format("FilteredRuleCount: {0}", this.FilteredRuleCount));
				if (base.EnableTimers)
				{
					stringBuilder.AppendLine(string.Format("PrecomputeMatchTime: {0:F3} ms", this.PrecomputeMatchTime.Elapsed.TotalMilliseconds));
					stringBuilder.AppendLine(string.Format("TrieMatchTime: {0:F3} ms", this.TrieMatchTime.Elapsed.TotalMilliseconds));
					stringBuilder.AppendLine(string.Format("MatchFilterTime: {0:F3} ms", this.MatchFilterTime.Elapsed.TotalMilliseconds));
				}
				return stringBuilder.ToString();
			}

			// Token: 0x040006AC RID: 1708
			[Statistic(0)]
			public long LeftTokenCount;

			// Token: 0x040006AD RID: 1709
			[Statistic(0)]
			public long MatchedRuleCount;

			// Token: 0x040006AE RID: 1710
			[Statistic(0)]
			public long FilteredRuleCount;

			// Token: 0x040006AF RID: 1711
			[NonSerialized]
			public Stopwatch PrecomputeMatchTime = new Stopwatch();

			// Token: 0x040006B0 RID: 1712
			[NonSerialized]
			public Stopwatch TrieMatchTime = new Stopwatch();

			// Token: 0x040006B1 RID: 1713
			[NonSerialized]
			public Stopwatch MatchFilterTime = new Stopwatch();
		}

		// Token: 0x02000198 RID: 408
		[Serializable]
		private class ReferenceRowsetSink : IRowsetSink, IRecordUpdate, IRecordContextUpdate
		{
			// Token: 0x1700027A RID: 634
			// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0003A190 File Offset: 0x00038390
			// (set) Token: 0x06000D87 RID: 3463 RVA: 0x0003A198 File Offset: 0x00038398
			public string Name { get; set; }

			// Token: 0x1700027B RID: 635
			// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0003A1A1 File Offset: 0x000383A1
			// (set) Token: 0x06000D89 RID: 3465 RVA: 0x0003A1A9 File Offset: 0x000383A9
			public EditTransformationProvider Provider { get; set; }

			// Token: 0x06000D8A RID: 3466 RVA: 0x0003A1B2 File Offset: 0x000383B2
			public IUpdateContext BeginUpdate()
			{
				return this.BeginUpdate(null);
			}

			// Token: 0x06000D8B RID: 3467 RVA: 0x0003A1BB File Offset: 0x000383BB
			public IUpdateContext BeginUpdate(DataTable schemaTable)
			{
				return new EditTransformationProvider.ReferenceRowsetSink.ReferenceRowsetUpdateContext
				{
					DomainName = this.Provider.DomainName
				};
			}

			// Token: 0x06000D8C RID: 3468 RVA: 0x0003A1D4 File Offset: 0x000383D4
			public void EndUpdate(IUpdateContext _updateContext)
			{
				EditTransformationProvider.ReferenceRowsetSink.ReferenceRowsetUpdateContext referenceRowsetUpdateContext = (EditTransformationProvider.ReferenceRowsetSink.ReferenceRowsetUpdateContext)_updateContext;
				EditTransformationProvider provider = this.Provider;
				lock (provider)
				{
					this.Provider.ClearCache();
				}
			}

			// Token: 0x06000D8D RID: 3469 RVA: 0x0003A21C File Offset: 0x0003841C
			void IRecordContextUpdate.AddRecordContext(IUpdateContext _updateContext, RecordContext recordContext)
			{
				EditTransformationProvider.ReferenceRowsetSink.ReferenceRowsetUpdateContext referenceRowsetUpdateContext = (EditTransformationProvider.ReferenceRowsetSink.ReferenceRowsetUpdateContext)_updateContext;
				EditTransformationProvider provider = this.Provider;
				lock (provider)
				{
					for (int i = 0; i < recordContext.TokenSequence.Tokens.Count; i++)
					{
						int num = recordContext.TokenSequence.Tokens[i];
						this.Provider.Add(referenceRowsetUpdateContext, referenceRowsetUpdateContext.TokenIdProvider.GetToken(num));
					}
					foreach (WeightedTransformationMatch weightedTransformationMatch in recordContext.TransformationMatchList)
					{
						int num2 = 0;
						for (;;)
						{
							int num3 = num2;
							WeightedTransformation weightedTransformation = weightedTransformationMatch.Transformation;
							if (num3 >= weightedTransformation.To.Count)
							{
								break;
							}
							weightedTransformation = weightedTransformationMatch.Transformation;
							int num4 = weightedTransformation.To[num2];
							this.Provider.Add(referenceRowsetUpdateContext, referenceRowsetUpdateContext.TokenIdProvider.GetToken(num4));
							num2++;
						}
					}
				}
			}

			// Token: 0x06000D8E RID: 3470 RVA: 0x0003A334 File Offset: 0x00038534
			void IRecordContextUpdate.RemoveRecordContext(IUpdateContext _updateContext, RecordContext recordContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000D8F RID: 3471 RVA: 0x0003A33C File Offset: 0x0003853C
			void IRecordUpdate.AddRecord(IUpdateContext _updateContext, IDataRecord record)
			{
				EditTransformationProvider.ReferenceRowsetSink.ReferenceRowsetUpdateContext referenceRowsetUpdateContext = (EditTransformationProvider.ReferenceRowsetSink.ReferenceRowsetUpdateContext)_updateContext;
				referenceRowsetUpdateContext.TokenIdProvider.Reset();
				EditTransformationProvider provider = this.Provider;
				lock (provider)
				{
					foreach (int num in referenceRowsetUpdateContext.DistinctTokenProvider.GetDistinctTokens(referenceRowsetUpdateContext.TokenIdProvider, referenceRowsetUpdateContext.m_domainId, record))
					{
						this.Provider.Add(referenceRowsetUpdateContext, referenceRowsetUpdateContext.TokenIdProvider.GetToken(num));
					}
				}
			}

			// Token: 0x06000D90 RID: 3472 RVA: 0x0003A3E4 File Offset: 0x000385E4
			void IRecordUpdate.RemoveRecord(IUpdateContext _updateContext, IDataRecord record)
			{
				throw new NotImplementedException();
			}

			// Token: 0x020001C1 RID: 449
			private class ReferenceRowsetUpdateContext : IUpdateContext, IRecordUpdateContextInitialize
			{
				// Token: 0x06000E5A RID: 3674 RVA: 0x0003CBC3 File Offset: 0x0003ADC3
				public void Initialize(IRowsetManager rowsetManager, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding recordBinding)
				{
					this.TokenIdProvider = new TransientTokenIdProvider(tokenIdProvider);
					this.m_domainId = domainManager.GetDomainId(this.DomainName);
					this.DistinctTokenProvider = new DistinctTokenProvider(recordBinding, this.DomainName, domainManager.GetTokenizer(this.DomainName), null);
				}

				// Token: 0x0400075C RID: 1884
				public string DomainName;

				// Token: 0x0400075D RID: 1885
				public int m_domainId;

				// Token: 0x0400075E RID: 1886
				public TransientTokenIdProvider TokenIdProvider;

				// Token: 0x0400075F RID: 1887
				public DistinctTokenProvider DistinctTokenProvider;
			}
		}

		// Token: 0x02000199 RID: 409
		[Serializable]
		private class PrecomputedEditsRowsetSink : IRowsetSink, IRecordUpdate
		{
			// Token: 0x1700027C RID: 636
			// (get) Token: 0x06000D92 RID: 3474 RVA: 0x0003A3F3 File Offset: 0x000385F3
			// (set) Token: 0x06000D93 RID: 3475 RVA: 0x0003A3FB File Offset: 0x000385FB
			public string Name { get; set; }

			// Token: 0x1700027D RID: 637
			// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0003A404 File Offset: 0x00038604
			// (set) Token: 0x06000D95 RID: 3477 RVA: 0x0003A40C File Offset: 0x0003860C
			public EditTransformationProvider Provider { get; set; }

			// Token: 0x06000D96 RID: 3478 RVA: 0x0003A415 File Offset: 0x00038615
			public IUpdateContext BeginUpdate()
			{
				return this.BeginUpdate(null);
			}

			// Token: 0x06000D97 RID: 3479 RVA: 0x0003A41E File Offset: 0x0003861E
			public IUpdateContext BeginUpdate(DataTable schemaTable)
			{
				return new EditTransformationProvider.PrecomputedEditsRowsetSink.PrecomputedEditsRowsetUpdateContext();
			}

			// Token: 0x06000D98 RID: 3480 RVA: 0x0003A428 File Offset: 0x00038628
			public void EndUpdate(IUpdateContext _updateContext)
			{
				EditTransformationProvider provider = this.Provider;
				lock (provider)
				{
					this.Provider.ClearCache();
				}
			}

			// Token: 0x06000D99 RID: 3481 RVA: 0x0003A468 File Offset: 0x00038668
			void IRecordUpdate.AddRecord(IUpdateContext _updateContext, IDataRecord record)
			{
				this.Provider.PrecomputedEditCache.Add(record);
			}

			// Token: 0x06000D9A RID: 3482 RVA: 0x0003A47B File Offset: 0x0003867B
			void IRecordUpdate.RemoveRecord(IUpdateContext _updateContext, IDataRecord record)
			{
				throw new NotImplementedException();
			}

			// Token: 0x020001C2 RID: 450
			private class PrecomputedEditsRowsetUpdateContext : IUpdateContext
			{
			}
		}

		// Token: 0x0200019A RID: 410
		public class GlobalStatistics
		{
			// Token: 0x06000D9C RID: 3484 RVA: 0x0003A48A File Offset: 0x0003868A
			public void Reset()
			{
				this.PrecomputedCacheHits = 0L;
				this.MruCacheHits = 0L;
				this.MruCacheMisses = 0L;
				this.MaxUnfilteredMatchesFound = 0L;
			}

			// Token: 0x06000D9D RID: 3485 RVA: 0x0003A4AC File Offset: 0x000386AC
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(string.Format("PrecomputedCacheHits: {0}", this.PrecomputedCacheHits));
				stringBuilder.AppendLine(string.Format("MruCacheHits: {0}", this.MruCacheHits));
				stringBuilder.AppendLine(string.Format("MruCacheMisses: {0}", this.MruCacheMisses));
				stringBuilder.AppendLine(string.Format("MaxUnfilteredMatchesFound: {0}", this.MaxUnfilteredMatchesFound));
				return stringBuilder.ToString();
			}

			// Token: 0x040006B6 RID: 1718
			public long PrecomputedCacheHits;

			// Token: 0x040006B7 RID: 1719
			public long MruCacheHits;

			// Token: 0x040006B8 RID: 1720
			public long MruCacheMisses;

			// Token: 0x040006B9 RID: 1721
			public long MaxUnfilteredMatchesFound;
		}
	}
}
