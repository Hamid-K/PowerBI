using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000102 RID: 258
	[Serializable]
	public class PrefixTransformationProvider : ITransformationProvider, IPairSpecificTransformationProvider, IProviderInitialize, IRowsetConsumer, IRawSerializable, ISerializable, ISessionable
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0002F23B File Offset: 0x0002D43B
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x0002F243 File Offset: 0x0002D443
		public ITransformationFilter TransformationFilter { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0002F24C File Offset: 0x0002D44C
		// (set) Token: 0x06000A8B RID: 2699 RVA: 0x0002F254 File Offset: 0x0002D454
		public string DomainName { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0002F25D File Offset: 0x0002D45D
		// (set) Token: 0x06000A8D RID: 2701 RVA: 0x0002F265 File Offset: 0x0002D465
		public bool FoldVowels { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0002F26E File Offset: 0x0002D46E
		// (set) Token: 0x06000A8F RID: 2703 RVA: 0x0002F276 File Offset: 0x0002D476
		public int ContextWindowLength { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0002F27F File Offset: 0x0002D47F
		// (set) Token: 0x06000A91 RID: 2705 RVA: 0x0002F287 File Offset: 0x0002D487
		public int MaxExpansions { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0002F290 File Offset: 0x0002D490
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x0002F298 File Offset: 0x0002D498
		public bool Symmetric { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0002F2A1 File Offset: 0x0002D4A1
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x0002F2A9 File Offset: 0x0002D4A9
		public bool GenerateContextFreeTransformations { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0002F2B2 File Offset: 0x0002D4B2
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x0002F2BA File Offset: 0x0002D4BA
		public JoinSide PairSpecificSide { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0002F2C3 File Offset: 0x0002D4C3
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x0002F2CB File Offset: 0x0002D4CB
		public int MultiCharacterAbbreviationWindowLength { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0002F2D4 File Offset: 0x0002D4D4
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x0002F2DC File Offset: 0x0002D4DC
		public string ReferenceRowsetName { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0002F2E5 File Offset: 0x0002D4E5
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x0002F2ED File Offset: 0x0002D4ED
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0002F2F6 File Offset: 0x0002D4F6
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x0002F2FE File Offset: 0x0002D4FE
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002F308 File Offset: 0x0002D508
		public PrefixTransformationProvider()
		{
			this.ReferenceRowsetName = "default";
			this.m_referenceRowsetSink = new PrefixTransformationProvider.ReferenceRowsetSink
			{
				Name = "ReferenceRowset",
				Provider = this
			};
			this.FoldVowels = true;
			this.MultiCharacterAbbreviationWindowLength = 1;
			this.ContextWindowLength = 3;
			this.MaxExpansions = 10000;
			this.Symmetric = true;
			this.GenerateContextFreeTransformations = true;
			this.PairSpecificSide = JoinSide.Both;
			this.m_htValsLookup = new PrefixTransformationProvider.TokenFreq[0];
			this.m_referenceRowsetSink.EndUpdate(this.m_referenceRowsetSink.BeginUpdate());
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002F3A8 File Offset: 0x0002D5A8
		protected PrefixTransformationProvider(SerializationInfo info, StreamingContext context)
		{
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.m_tokenFrequencies = (Dictionary<int, int>)info.GetValue("m_tokenFrequencies", typeof(Dictionary<int, int>));
			this.m_htOffsetsLookup = (Dictionary<int, int>)info.GetValue("m_htOffsetsLookup", typeof(Dictionary<int, int>));
			this.TransformationFilter = (ITransformationFilter)info.GetValue("TransformationFilter", typeof(ITransformationFilter));
			this.FoldVowels = (bool)info.GetValue("FoldVowels", typeof(bool));
			this.ContextWindowLength = (int)info.GetValue("ContextWindowLength", typeof(int));
			this.MaxExpansions = (int)info.GetValue("MaxExpansions", typeof(int));
			this.GenerateContextFreeTransformations = (bool)info.GetValue("GenerateContextFreeTransformations", typeof(bool));
			this.DomainId = (int)info.GetValue("DomainId", typeof(int));
			this.DomainName = (string)info.GetValue("DomainName", typeof(string));
			this.ReferenceRowsetName = (string)info.GetValue("ReferenceRowsetName", typeof(string));
			this.m_referenceRowsetSink = (PrefixTransformationProvider.ReferenceRowsetSink)info.GetValue("m_referenceRowsetSink", typeof(PrefixTransformationProvider.ReferenceRowsetSink));
			this.MultiCharacterAbbreviationWindowLength = (int)info.GetValue("MultiCharacterAbbreviationWindowLength", typeof(int));
			this.PairSpecificSide = (JoinSide)info.GetValue("PairSpecificSide", typeof(JoinSide));
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				this.m_htValsLookup = (PrefixTransformationProvider.TokenFreq[])info.GetValue("m_htValsLookup", typeof(PrefixTransformationProvider.TokenFreq[]));
			}
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002F5D0 File Offset: 0x0002D7D0
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EnableRawSerialization", ((IRawSerializable)this).EnableRawSerialization);
			info.AddValue("RawSerializationID", ((IRawSerializable)this).RawSerializationID);
			info.AddValue("m_tokenFrequencies", this.m_tokenFrequencies);
			info.AddValue("m_htOffsetsLookup", this.m_htOffsetsLookup);
			info.AddValue("TransformationFilter", this.TransformationFilter);
			info.AddValue("FoldVowels", this.FoldVowels);
			info.AddValue("ContextWindowLength", this.ContextWindowLength);
			info.AddValue("MaxExpansions", this.MaxExpansions);
			info.AddValue("GenerateContextFreeTransformations", this.GenerateContextFreeTransformations);
			info.AddValue("DomainName", this.DomainName);
			info.AddValue("DomainId", this.DomainId);
			info.AddValue("ReferenceRowsetName", this.ReferenceRowsetName);
			info.AddValue("m_referenceRowsetSink", this.m_referenceRowsetSink);
			info.AddValue("MultiCharacterAbbreviationWindowLength", this.MultiCharacterAbbreviationWindowLength);
			info.AddValue("PairSpecificSide", this.PairSpecificSide);
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				info.AddValue("m_htValsLookup", this.m_htValsLookup);
			}
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002F6FC File Offset: 0x0002D8FC
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt32(s, this.m_htValsLookup.Length);
			for (int i = 0; i < this.m_htValsLookup.Length; i++)
			{
				StreamUtilities.WriteInt32(s, this.m_htValsLookup[i].TokenId);
				StreamUtilities.WriteInt32(s, this.m_htValsLookup[i].Frequency);
			}
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002F770 File Offset: 0x0002D970
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			int num = StreamUtilities.ReadInt32(s);
			this.m_htValsLookup = new PrefixTransformationProvider.TokenFreq[num];
			for (int i = 0; i < num; i++)
			{
				this.m_htValsLookup[i] = new PrefixTransformationProvider.TokenFreq(StreamUtilities.ReadInt32(s), StreamUtilities.ReadInt32(s));
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002F7EB File Offset: 0x0002D9EB
		public void Initialize(IDomainManager domainManager, string domainName)
		{
			this.DomainName = domainName;
			this.DomainId = domainManager.GetDomainId(domainName);
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0002F801 File Offset: 0x0002DA01
		public IList<IRowsetSink> RowsetSinks
		{
			get
			{
				return new IRowsetSink[] { this.m_referenceRowsetSink };
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002F812 File Offset: 0x0002DA12
		public void Clear()
		{
			this.m_htValsLookup = new PrefixTransformationProvider.TokenFreq[0];
			this.m_htOffsetsLookup.Clear();
			this.m_tokenFrequencies.Clear();
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002F836 File Offset: 0x0002DA36
		public void RequestRowsets(IRowsetDistributor rowsetDistributor)
		{
			if (!string.IsNullOrEmpty(this.ReferenceRowsetName))
			{
				rowsetDistributor.RequestRowset(this.ReferenceRowsetName, this.m_referenceRowsetSink);
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002F858 File Offset: 0x0002DA58
		private static int CompareTokenWeightByWeightDescTokenIdAsc(PrefixTransformationProvider.TokenFreq x, PrefixTransformationProvider.TokenFreq y)
		{
			if (x.Frequency > y.Frequency)
			{
				return -1;
			}
			if (x.Frequency < y.Frequency)
			{
				return 1;
			}
			if (x.TokenId < y.TokenId)
			{
				return -1;
			}
			if (x.TokenId >= y.TokenId)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0002F8A8 File Offset: 0x0002DAA8
		public ISession CreateSession()
		{
			PrefixTransformationProvider.Session session = new PrefixTransformationProvider.Session
			{
				DomainId = this.DomainId
			};
			if (this.TransformationFilter != null && this.TransformationFilter is ISessionable)
			{
				session.m_filterSession = (this.TransformationFilter as ISessionable).CreateSession();
			}
			return session;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002F8F4 File Offset: 0x0002DAF4
		public void Match(ISession _session, ITokenIdProvider tokenIdProvider, IDataRecord leftRecord, IDataRecord rightRecord, RecordBinding leftBinding, RecordBinding rightBinding, TokenSequence leftTokenSeq, TokenSequence rightTokenSeq, out ArraySegment<TransformationMatch> leftTransformationMatchList, out ArraySegment<TransformationMatch> rightTransformationMatchList)
		{
			PrefixTransformationProvider.Session session = _session as PrefixTransformationProvider.Session;
			session.Reset();
			if (JoinSide.Left == this.PairSpecificSide || JoinSide.Both == this.PairSpecificSide)
			{
				this.GeneratePairSpecificTransformations(session, tokenIdProvider, leftTokenSeq, rightTokenSeq, session.m_leftTranMatchListBuilder, out leftTransformationMatchList);
			}
			else
			{
				leftTransformationMatchList = default(ArraySegment<TransformationMatch>);
			}
			if (JoinSide.Right == this.PairSpecificSide || JoinSide.Both == this.PairSpecificSide)
			{
				this.GeneratePairSpecificTransformations(session, tokenIdProvider, rightTokenSeq, leftTokenSeq, session.m_rightTranMatchListBuilder, out rightTransformationMatchList);
				return;
			}
			rightTransformationMatchList = default(ArraySegment<TransformationMatch>);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002F970 File Offset: 0x0002DB70
		private void GeneratePairSpecificTransformations(PrefixTransformationProvider.Session session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSeqFrom, TokenSequence tokenSeqTo, ArraySegmentBuilder<TransformationMatch> tranMatchListBuilder, out ArraySegment<TransformationMatch> transformationMatchList)
		{
			Dictionary<char, List<StringExtent>> pairSpecificCharDictionary = session.m_pairSpecificCharDictionary;
			session.ResetPairSpecificCharDictionary();
			for (int i = 0; i < tokenSeqTo.Count; i++)
			{
				int num = tokenSeqTo[i];
				if (tokenIdProvider.GetDomainId(num) == session.DomainId)
				{
					StringExtent token = tokenIdProvider.GetToken(num);
					List<StringExtent> list;
					if (!pairSpecificCharDictionary.TryGetValue(token[0], ref list))
					{
						list = session.NewStringExtentList();
						pairSpecificCharDictionary.Add(token[0], list);
					}
					list.Add(token);
				}
			}
			for (int j = 0; j < tokenSeqFrom.Count; j++)
			{
				int num2 = tokenSeqFrom[j];
				if (tokenIdProvider.GetDomainId(num2) == session.DomainId)
				{
					StringExtent token2 = tokenIdProvider.GetToken(num2);
					List<StringExtent> list2;
					if (pairSpecificCharDictionary.TryGetValue(token2[0], ref list2))
					{
						foreach (StringExtent stringExtent in list2)
						{
							int num3;
							int num4;
							if (this.IsPrefix(token2, stringExtent, out num3, out num4))
							{
								this.AddTransformation(session, tokenIdProvider, j, num2, tokenIdProvider.GetOrCreateTokenId(stringExtent, this.DomainId), num3, num4, tranMatchListBuilder);
							}
						}
					}
				}
			}
			transformationMatchList = tranMatchListBuilder;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002FAC0 File Offset: 0x0002DCC0
		private void AddTransformation(PrefixTransformationProvider.Session session, ITokenIdProvider tokenIdProvider, int position, int token1, int token2, int prefixMatchLength, int maxLength, ArraySegmentBuilder<TransformationMatch> transformationMatchList)
		{
			if (token1 != token2)
			{
				TokenSequence tokenSequence = session.NewTokenSequence(1);
				tokenSequence[0] = token1;
				TokenSequence tokenSequence2 = session.NewTokenSequence(1);
				tokenSequence2[0] = token2;
				Transformation transformation = session.NewPrefixTransformation();
				transformation.From = tokenSequence;
				transformation.To = tokenSequence2;
				transformation.Type = TransformationType.PrefixTransformation;
				transformation.Metadata = session.NewMetaData();
				new PrefixTransformationMetadata
				{
					PrefixMatchLength = (short)prefixMatchLength,
					MaxLength = (short)maxLength
				}.Write(transformation.Metadata);
				transformationMatchList.Add(new TransformationMatch
				{
					Position = position,
					Transformation = transformation
				});
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002FB74 File Offset: 0x0002DD74
		public bool IsPrefix(StringExtent token1, StringExtent token2, out int prefixMatchLength, out int maxLength)
		{
			if (this.Symmetric)
			{
				return Utilities.IsPrefix(token1, token2, this.FoldVowels, out prefixMatchLength, out maxLength) || Utilities.IsPrefix(token2, token1, this.FoldVowels, out prefixMatchLength, out maxLength);
			}
			return Utilities.IsPrefix(token1, token2, this.FoldVowels, out prefixMatchLength, out maxLength);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002FBDC File Offset: 0x0002DDDC
		public void Match(ISession _session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSeq, out ArraySegment<TransformationMatch> transformationMatchList)
		{
			PrefixTransformationProvider.Session session = _session as PrefixTransformationProvider.Session;
			session.Reset();
			if (this.TransformationFilter != null)
			{
				this.TransformationFilter.Prepare(session.m_filterSession, tokenIdProvider, tokenSeq);
			}
			if (session.m_fixedLenRules.Length < this.MultiCharacterAbbreviationWindowLength)
			{
				session.m_fixedLenRules = new List<int>[this.MultiCharacterAbbreviationWindowLength];
				for (int i = 0; i < this.MultiCharacterAbbreviationWindowLength; i++)
				{
					session.m_fixedLenRules[i] = new List<int>();
				}
			}
			for (int j = 0; j < this.MultiCharacterAbbreviationWindowLength; j++)
			{
				session.m_fixedLenRules[j].Clear();
			}
			session.m_expansions2.Clear();
			for (int k = 0; k < tokenSeq.Count; k++)
			{
				if (tokenIdProvider.GetDomainId(tokenSeq[k]) == this.DomainId && (this.TransformationFilter == null || this.TransformationFilter.AllowTransformations(session.m_filterSession, k)))
				{
					session.m_expansions.Clear();
					this.FindExpansions(session, tokenSeq, k, session.m_expansions, tokenIdProvider);
					for (int l = 0; l < session.m_expansions.Count; l++)
					{
						session.m_fixedLenRules[0].Add(k);
						session.m_fixedLenRules[0].Add(session.m_expansions[l].TokenId);
					}
					List<PrefixTransformationProvider.TokenFreq> expansions = session.m_expansions2;
					session.m_expansions2 = session.m_expansions;
					session.m_expansions = expansions;
				}
			}
			this.GenerateMaximalRules(session, tokenIdProvider, tokenSeq, out transformationMatchList);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002FD4F File Offset: 0x0002DF4F
		public IEnumerable<Transformation> Transformations(ITokenIdProvider tokenIdProvider)
		{
			yield break;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002FD58 File Offset: 0x0002DF58
		private void IntersectExpansions(List<PrefixTransformationProvider.TokenFreq> list1, List<PrefixTransformationProvider.TokenFreq> list2, List<PrefixTransformationProvider.TokenFreq> outputList)
		{
			outputList.Clear();
			int num = 0;
			int num2 = 0;
			while (num < list1.Count && num2 < list2.Count)
			{
				int num3 = PrefixTransformationProvider.CompareTokenWeightByWeightDescTokenIdAsc(list1[num], list2[num2]);
				if (num3 < 0)
				{
					num++;
				}
				else if (num3 > 0)
				{
					num2++;
				}
				else
				{
					outputList.Add(list1[num]);
					num++;
					num2++;
				}
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002FDC0 File Offset: 0x0002DFC0
		private void GenerateMaximalRules(PrefixTransformationProvider.Session session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSeq, out ArraySegment<TransformationMatch> transformationMatchList)
		{
			for (int i = 0; i < session.m_fixedLenRules.Length; i++)
			{
				int num = (i + 1) * 2;
				int num2 = -1;
				for (int j = 0; j < session.m_fixedLenRules[i].Count; j += num)
				{
					if (session.m_fixedLenRules[i][j] != -2147483648)
					{
						int num3 = session.m_fixedLenRules[i][j];
						TokenSequence tokenSequence = session.NewTokenSequence(num / 2);
						for (int k = 0; k < num / 2; k++)
						{
							int num4 = session.m_fixedLenRules[i][j + k];
							tokenSequence[k] = tokenSeq[num4];
						}
						TokenSequence tokenSequence2 = session.NewTokenSequence(num - num / 2);
						int l = num / 2;
						int num5 = 0;
						while (l < num)
						{
							int num6 = session.m_fixedLenRules[i][j + l];
							tokenSequence2[num5] = num6;
							l++;
							num5++;
						}
						if (!tokenSequence.Equals(tokenSequence2))
						{
							int num7 = 0;
							int num8 = 0;
							for (int m = 0; m < tokenSequence.Count; m++)
							{
								int num9;
								int num10;
								if (this.IsPrefix(tokenIdProvider.GetToken(tokenSequence[m]), tokenIdProvider.GetToken(tokenSequence2[m]), out num9, out num10))
								{
									num7 += num9;
									num8 += num10;
								}
							}
							PrefixTransformationMetadata prefixTransformationMetadata = new PrefixTransformationMetadata
							{
								PrefixMatchLength = (short)Math.Min(num7, 32767),
								MaxLength = (short)Math.Min(num8, 32767)
							};
							Transformation transformation = session.NewPrefixTransformation();
							transformation.From = tokenSequence;
							transformation.To = tokenSequence2;
							transformation.Metadata = session.NewMetaData();
							prefixTransformationMetadata.Write(transformation.Metadata);
							if (i == 0 && num3 != num2 && num2 != -1)
							{
								this.FilterAndCopyUnitRules(session, num2);
							}
							if (i == 0)
							{
								session.m_perTokenList.Add(new TransformationMatch
								{
									Position = num3,
									Transformation = transformation
								});
							}
							else
							{
								session.m_overallTMList.Add(new TransformationMatch
								{
									Position = num3,
									Transformation = transformation
								});
							}
							if (num2 == -1)
							{
								num2 = num3;
							}
						}
					}
				}
				if (i == 0 && num2 != -1)
				{
					this.FilterAndCopyUnitRules(session, num2);
				}
			}
			if (this.TransformationFilter != null)
			{
				this.TransformationFilter.FilterTransformations(session.m_filterSession, session.m_overallTMList, out transformationMatchList);
				return;
			}
			transformationMatchList = session.m_overallTMList;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00030040 File Offset: 0x0002E240
		private void FilterAndCopyUnitRules(PrefixTransformationProvider.Session session, int prevRulePos)
		{
			ArraySegment<TransformationMatch> arraySegment = session.m_perTokenList;
			if (this.TransformationFilter != null)
			{
				this.TransformationFilter.FilterTransformations(session.m_filterSession, prevRulePos, arraySegment, out arraySegment);
			}
			session.m_overallTMList.Add(arraySegment);
			session.m_perTokenList.Reset();
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00030090 File Offset: 0x0002E290
		private void FindExpansions(PrefixTransformationProvider.Session session, TokenSequence tokenSeq, int pos, List<PrefixTransformationProvider.TokenFreq> expansions, ITokenIdProvider tokenIdProvider)
		{
			StringExtent token = tokenIdProvider.GetToken(tokenSeq[pos]);
			int num = Math.Max(0, pos - this.ContextWindowLength);
			int num2 = Math.Min(pos + this.ContextWindowLength, tokenSeq.Count - 1);
			session.m_findExpansionsDistinctHashSet.Clear();
			session.m_findExpansionsDistinctHashSet.Add(tokenSeq[pos]);
			int i = num;
			while (i <= num2)
			{
				StringExtent stringExtent;
				if (pos != i)
				{
					stringExtent = tokenIdProvider.GetToken(tokenSeq[i]);
					goto IL_0082;
				}
				if (this.GenerateContextFreeTransformations)
				{
					stringExtent = default(StringExtent);
					goto IL_0082;
				}
				IL_0126:
				i++;
				continue;
				IL_0082:
				int hashCode = this.GetHashCode(stringExtent, token[0], 1, ref session.m_getHashCodeBuffer);
				int num3;
				if (this.m_htOffsetsLookup.TryGetValue(hashCode, ref num3))
				{
					int num4 = num3;
					PrefixTransformationProvider.TokenFreq tokenFreq;
					while ((tokenFreq = this.m_htValsLookup[num4]).TokenId != PrefixTransformationProvider.EndMarker.TokenId)
					{
						StringExtent token2 = tokenIdProvider.GetToken(tokenFreq.TokenId);
						int num5;
						int num6;
						if (this.IsPrefix(token, token2, out num5, out num6) && !session.m_findExpansionsDistinctHashSet.Contains(tokenFreq.TokenId))
						{
							expansions.Add(tokenFreq);
							session.m_findExpansionsDistinctHashSet.Add(tokenFreq.TokenId);
						}
						num4++;
					}
					goto IL_0126;
				}
				goto IL_0126;
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000301D0 File Offset: 0x0002E3D0
		private void FindExpansions(PrefixTransformationProvider.Session session, TokenSequence tokenSeq, int pos, int contextTokId, List<PrefixTransformationProvider.TokenFreq> expansions, ITokenIdProvider tokenIdProvider)
		{
			StringExtent token = tokenIdProvider.GetToken(tokenSeq[pos]);
			StringExtent token2 = tokenIdProvider.GetToken(contextTokId);
			int hashCode = this.GetHashCode(token2, token[0], 1, ref session.m_getHashCodeBuffer);
			int num;
			if (this.m_htOffsetsLookup.TryGetValue(hashCode, ref num))
			{
				int num2 = num;
				PrefixTransformationProvider.TokenFreq tokenFreq;
				while ((tokenFreq = this.m_htValsLookup[num2]).TokenId != PrefixTransformationProvider.EndMarker.TokenId)
				{
					if (tokenSeq[pos] != tokenFreq.TokenId)
					{
						expansions.Add(tokenFreq);
					}
					num2++;
				}
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00030264 File Offset: 0x0002E464
		private int FindMinContext(PrefixTransformationProvider.Session querySession, TokenSequence tokenSeq, int pos, ITokenIdProvider tokenIdProvider)
		{
			int num = Math.Max(0, pos - this.ContextWindowLength);
			int num2 = Math.Min(pos + this.ContextWindowLength, tokenSeq.Count - 1);
			int num3 = -1;
			int num4 = -1;
			for (int i = num; i <= num2; i++)
			{
				if (pos != i)
				{
					querySession.m_expansions.Clear();
					this.FindExpansions(querySession, tokenSeq, pos, tokenSeq[i], querySession.m_expansions, tokenIdProvider);
					if (num4 == -1 || querySession.m_expansions.Count < num4)
					{
						num4 = querySession.m_expansions.Count;
						num3 = i;
					}
				}
			}
			return num3;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000302EE File Offset: 0x0002E4EE
		private int GetHashCode(StringExtent contextToken, char prefixTokenFirstChar, int distance, ref char[] buffer)
		{
			return Utilities.GetHashCode(Utilities.GetHashCode(this.GetVowelFoldingHashCode(contextToken, ref buffer), (int)prefixTokenFirstChar), distance);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00030308 File Offset: 0x0002E508
		private int GetVowelFoldingHashCode(StringExtent token, ref char[] buffer)
		{
			if (token.Length == 0)
			{
				return 0;
			}
			if (token.Length <= 2)
			{
				return Utilities.GetHashCode(token.Array, token.Offset, token.Length);
			}
			if (buffer.Length < token.Length)
			{
				Array.Resize<char>(ref buffer, 2 * token.Length);
			}
			int num = 0;
			for (int i = 0; i < token.Length; i++)
			{
				if (!this.FoldVowels || !Utilities.IsVowel(token[i]))
				{
					buffer[num++] = token[i];
				}
			}
			return Utilities.GetHashCode(buffer, 0, num);
		}

		// Token: 0x040003FB RID: 1019
		private static readonly PrefixTransformationProvider.TokenFreq EndMarker = new PrefixTransformationProvider.TokenFreq(int.MinValue, int.MinValue);

		// Token: 0x040003FC RID: 1020
		private PrefixTransformationProvider.TokenFreq[] m_htValsLookup;

		// Token: 0x040003FD RID: 1021
		private Dictionary<int, int> m_tokenFrequencies = new Dictionary<int, int>();

		// Token: 0x040003FE RID: 1022
		private Dictionary<int, int> m_htOffsetsLookup;

		// Token: 0x04000400 RID: 1024
		private int DomainId;

		// Token: 0x0400040A RID: 1034
		private PrefixTransformationProvider.ReferenceRowsetSink m_referenceRowsetSink;

		// Token: 0x0200019F RID: 415
		[DebuggerDisplay("TokenId={TokenId} Frequency={Frequency}")]
		[Serializable]
		private struct TokenFreq
		{
			// Token: 0x06000DB5 RID: 3509 RVA: 0x0003AA1C File Offset: 0x00038C1C
			public TokenFreq(int tokenId, int freq)
			{
				this.TokenId = tokenId;
				this.Frequency = freq;
			}

			// Token: 0x040006CE RID: 1742
			public int TokenId;

			// Token: 0x040006CF RID: 1743
			public int Frequency;
		}

		// Token: 0x020001A0 RID: 416
		private class Session : ISession
		{
			// Token: 0x06000DB6 RID: 3510 RVA: 0x0003AA2C File Offset: 0x00038C2C
			public void Reset()
			{
				this.m_prefixTransformations.Clear();
				this.m_tokenSequences.Clear();
				this.m_tokenIdArray.Reset();
				this.m_metadataArray.Reset();
				this.m_tranMatchAllocator.Reset();
				if (this.m_filterSession != null)
				{
					this.m_filterSession.Reset();
				}
				this.m_perTokenList.Reset();
				this.m_overallTMList.Reset();
				this.m_leftTranMatchListBuilder.Reset();
				this.m_rightTranMatchListBuilder.Reset();
			}

			// Token: 0x06000DB7 RID: 3511 RVA: 0x0003AAAF File Offset: 0x00038CAF
			public void ResetPairSpecificCharDictionary()
			{
				this.m_pairSpecificCharDictionary.Clear();
				this.m_nextFreeExtentIndex = 0;
			}

			// Token: 0x06000DB8 RID: 3512 RVA: 0x0003AAC4 File Offset: 0x00038CC4
			public List<StringExtent> NewStringExtentList()
			{
				int num;
				if (this.m_nextFreeExtentIndex < this.m_freeStringExtentLists.Count)
				{
					List<List<StringExtent>> freeStringExtentLists = this.m_freeStringExtentLists;
					num = this.m_nextFreeExtentIndex;
					this.m_nextFreeExtentIndex = num + 1;
					List<StringExtent> list = freeStringExtentLists[num];
					list.Clear();
					return list;
				}
				this.m_freeStringExtentLists.Add(new List<StringExtent>());
				List<List<StringExtent>> freeStringExtentLists2 = this.m_freeStringExtentLists;
				num = this.m_nextFreeExtentIndex;
				this.m_nextFreeExtentIndex = num + 1;
				return freeStringExtentLists2[num];
			}

			// Token: 0x06000DB9 RID: 3513 RVA: 0x0003AB34 File Offset: 0x00038D34
			public TokenSequence NewTokenSequence(int len)
			{
				ArraySegment<int> arraySegment = this.m_tokenIdArray.New(len);
				TokenSequence tokenSequence = new TokenSequence(arraySegment);
				this.m_tokenSequences.Add(tokenSequence);
				return tokenSequence;
			}

			// Token: 0x06000DBA RID: 3514 RVA: 0x0003AB64 File Offset: 0x00038D64
			public Transformation NewPrefixTransformation()
			{
				Transformation transformation = new Transformation
				{
					Type = TransformationType.PrefixTransformation
				};
				this.m_prefixTransformations.Add(transformation);
				return transformation;
			}

			// Token: 0x06000DBB RID: 3515 RVA: 0x0003AB90 File Offset: 0x00038D90
			public ArraySegment<byte> NewMetaData()
			{
				return this.m_metadataArray.New(1);
			}

			// Token: 0x040006D0 RID: 1744
			public ISession m_filterSession;

			// Token: 0x040006D1 RID: 1745
			private ObjectVector<Transformation> m_prefixTransformations = new ObjectVector<Transformation>();

			// Token: 0x040006D2 RID: 1746
			private List<TokenSequence> m_tokenSequences = new List<TokenSequence>();

			// Token: 0x040006D3 RID: 1747
			private BlockedSegmentArray<int> m_tokenIdArray = new BlockedSegmentArray<int>();

			// Token: 0x040006D4 RID: 1748
			public ArraySegmentBuilder<TransformationMatch> m_perTokenList = new ArraySegmentBuilder<TransformationMatch>();

			// Token: 0x040006D5 RID: 1749
			public ArraySegmentBuilder<TransformationMatch> m_overallTMList = new ArraySegmentBuilder<TransformationMatch>();

			// Token: 0x040006D6 RID: 1750
			public List<PrefixTransformationProvider.TokenFreq> m_expansions = new List<PrefixTransformationProvider.TokenFreq>();

			// Token: 0x040006D7 RID: 1751
			public List<PrefixTransformationProvider.TokenFreq> m_expansions1 = new List<PrefixTransformationProvider.TokenFreq>();

			// Token: 0x040006D8 RID: 1752
			public List<PrefixTransformationProvider.TokenFreq> m_expansions2 = new List<PrefixTransformationProvider.TokenFreq>();

			// Token: 0x040006D9 RID: 1753
			public List<PrefixTransformationProvider.TokenFreq> m_expansionsCum = new List<PrefixTransformationProvider.TokenFreq>();

			// Token: 0x040006DA RID: 1754
			public char[] m_getHashCodeBuffer = new char[1024];

			// Token: 0x040006DB RID: 1755
			public List<int>[] m_fixedLenRules = new List<int>[0];

			// Token: 0x040006DC RID: 1756
			public FastIntHashSet m_findExpansionsDistinctHashSet = new FastIntHashSet();

			// Token: 0x040006DD RID: 1757
			public Dictionary<char, List<StringExtent>> m_pairSpecificCharDictionary = new Dictionary<char, List<StringExtent>>();

			// Token: 0x040006DE RID: 1758
			private List<List<StringExtent>> m_freeStringExtentLists = new List<List<StringExtent>>();

			// Token: 0x040006DF RID: 1759
			private int m_nextFreeExtentIndex;

			// Token: 0x040006E0 RID: 1760
			private BlockedSegmentArray<byte> m_metadataArray = new BlockedSegmentArray<byte>(4);

			// Token: 0x040006E1 RID: 1761
			public BlockedSegmentArray<TransformationMatch> m_tranMatchAllocator = new BlockedSegmentArray<TransformationMatch>();

			// Token: 0x040006E2 RID: 1762
			public ArraySegmentBuilder<TransformationMatch> m_leftTranMatchListBuilder = new ArraySegmentBuilder<TransformationMatch>();

			// Token: 0x040006E3 RID: 1763
			public ArraySegmentBuilder<TransformationMatch> m_rightTranMatchListBuilder = new ArraySegmentBuilder<TransformationMatch>();

			// Token: 0x040006E4 RID: 1764
			public int DomainId;
		}

		// Token: 0x020001A1 RID: 417
		[Serializable]
		private class ReferenceRowsetSink : IRowsetSink, IRecordUpdate, IRecordContextUpdate
		{
			// Token: 0x17000282 RID: 642
			// (get) Token: 0x06000DBD RID: 3517 RVA: 0x0003AC80 File Offset: 0x00038E80
			// (set) Token: 0x06000DBE RID: 3518 RVA: 0x0003AC88 File Offset: 0x00038E88
			public string Name { get; set; }

			// Token: 0x17000283 RID: 643
			// (get) Token: 0x06000DBF RID: 3519 RVA: 0x0003AC91 File Offset: 0x00038E91
			// (set) Token: 0x06000DC0 RID: 3520 RVA: 0x0003AC99 File Offset: 0x00038E99
			public PrefixTransformationProvider Provider { get; set; }

			// Token: 0x06000DC1 RID: 3521 RVA: 0x0003ACA2 File Offset: 0x00038EA2
			public IUpdateContext BeginUpdate()
			{
				return this.BeginUpdate(null);
			}

			// Token: 0x06000DC2 RID: 3522 RVA: 0x0003ACAB File Offset: 0x00038EAB
			public IUpdateContext BeginUpdate(DataTable schemaTable)
			{
				return new PrefixTransformationProvider.ReferenceRowsetSink.ReferenceRowsetUpdateContext
				{
					DomainName = this.Provider.DomainName,
					m_htUpdate = new Dictionary<int, List<int>>(),
					m_Count = 0
				};
			}

			// Token: 0x06000DC3 RID: 3523 RVA: 0x0003ACD5 File Offset: 0x00038ED5
			void IRecordContextUpdate.AddRecordContext(IUpdateContext _updateContext, RecordContext recordContext)
			{
				this.Add((PrefixTransformationProvider.ReferenceRowsetSink.ReferenceRowsetUpdateContext)_updateContext, recordContext.TokenSequence.Tokens);
			}

			// Token: 0x06000DC4 RID: 3524 RVA: 0x0003ACF0 File Offset: 0x00038EF0
			void IRecordUpdate.AddRecord(IUpdateContext _updateContext, IDataRecord record)
			{
				PrefixTransformationProvider.ReferenceRowsetSink.ReferenceRowsetUpdateContext referenceRowsetUpdateContext = (PrefixTransformationProvider.ReferenceRowsetSink.ReferenceRowsetUpdateContext)_updateContext;
				TokenSequence tokenSequence = PrefixTransformationProvider.ReferenceRowsetSink.Tokenize(referenceRowsetUpdateContext, record);
				this.Add(referenceRowsetUpdateContext, tokenSequence);
			}

			// Token: 0x06000DC5 RID: 3525 RVA: 0x0003AD14 File Offset: 0x00038F14
			private void Add(PrefixTransformationProvider.ReferenceRowsetSink.ReferenceRowsetUpdateContext context, TokenSequence tokenSeq)
			{
				PrefixTransformationProvider provider = this.Provider;
				lock (provider)
				{
					for (int i = 0; i < tokenSeq.Count; i++)
					{
						if (context.TokenIdProvider.GetDomainId(tokenSeq[i]) == context.DomainId)
						{
							int num;
							if (!this.Provider.m_tokenFrequencies.TryGetValue(tokenSeq[i], ref num))
							{
								num = 0;
							}
							num = (this.Provider.m_tokenFrequencies[tokenSeq[i]] = num + 1);
							StringExtent token = context.TokenIdProvider.GetToken(tokenSeq[i]);
							int num2 = Math.Max(0, i - this.Provider.ContextWindowLength);
							int num3 = Math.Min(i + this.Provider.ContextWindowLength, tokenSeq.Count - 1);
							int j = num2;
							while (j <= num3)
							{
								StringExtent stringExtent;
								if (i == j)
								{
									if (this.Provider.GenerateContextFreeTransformations)
									{
										stringExtent = default(StringExtent);
										goto IL_010C;
									}
								}
								else if (context.TokenIdProvider.GetDomainId(tokenSeq[j]) == context.DomainId)
								{
									stringExtent = context.TokenIdProvider.GetToken(tokenSeq[j]);
									goto IL_010C;
								}
								IL_018F:
								j++;
								continue;
								IL_010C:
								int hashCode = this.Provider.GetHashCode(stringExtent, token[0], 1, ref context.m_getHashCodeBuffer);
								if (!context.m_htUpdate.ContainsKey(hashCode))
								{
									context.m_htUpdate.Add(hashCode, new List<int>());
								}
								if (!context.m_htUpdate[hashCode].Contains(tokenSeq[i]))
								{
									context.m_htUpdate[hashCode].Add(tokenSeq[i]);
									context.m_Count++;
									goto IL_018F;
								}
								goto IL_018F;
							}
						}
					}
				}
			}

			// Token: 0x06000DC6 RID: 3526 RVA: 0x0003AEF8 File Offset: 0x000390F8
			void IRecordUpdate.RemoveRecord(IUpdateContext _updateContext, IDataRecord r)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000DC7 RID: 3527 RVA: 0x0003AEFF File Offset: 0x000390FF
			void IRecordContextUpdate.RemoveRecordContext(IUpdateContext _updateContext, RecordContext recordContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000DC8 RID: 3528 RVA: 0x0003AF08 File Offset: 0x00039108
			private static TokenSequence Tokenize(PrefixTransformationProvider.ReferenceRowsetSink.ReferenceRowsetUpdateContext context, IDataRecord record)
			{
				context.m_intAllocator.Reset();
				context.m_tokenizerContext.Reset();
				return TokenSequence.Create(context.m_tokenizer.Tokenize(context.m_tokenizerContext, record), context.DomainId, context.TokenIdProvider, context.m_tokenIdSegmentBuilder, context.m_intAllocator);
			}

			// Token: 0x06000DC9 RID: 3529 RVA: 0x0003AF5C File Offset: 0x0003915C
			public void EndUpdate(IUpdateContext _updateContext)
			{
				PrefixTransformationProvider.ReferenceRowsetSink.ReferenceRowsetUpdateContext referenceRowsetUpdateContext = (PrefixTransformationProvider.ReferenceRowsetSink.ReferenceRowsetUpdateContext)_updateContext;
				PrefixTransformationProvider provider = this.Provider;
				lock (provider)
				{
					this.Provider.m_htValsLookup = new PrefixTransformationProvider.TokenFreq[referenceRowsetUpdateContext.m_Count + referenceRowsetUpdateContext.m_htUpdate.Count];
					this.Provider.m_htOffsetsLookup = new Dictionary<int, int>();
					int num = 0;
					List<PrefixTransformationProvider.TokenFreq> list = new List<PrefixTransformationProvider.TokenFreq>();
					Dictionary<int, List<int>>.Enumerator enumerator = referenceRowsetUpdateContext.m_htUpdate.GetEnumerator();
					while (enumerator.MoveNext())
					{
						Dictionary<int, int> htOffsetsLookup = this.Provider.m_htOffsetsLookup;
						KeyValuePair<int, List<int>> keyValuePair = enumerator.Current;
						htOffsetsLookup.Add(keyValuePair.Key, num);
						keyValuePair = enumerator.Current;
						foreach (int num2 in keyValuePair.Value)
						{
							list.Add(new PrefixTransformationProvider.TokenFreq(num2, this.Provider.m_tokenFrequencies[num2]));
						}
						list.Sort(new Comparison<PrefixTransformationProvider.TokenFreq>(PrefixTransformationProvider.CompareTokenWeightByWeightDescTokenIdAsc));
						int num3 = Math.Min(list.Count, this.Provider.MaxExpansions);
						for (int i = 0; i < num3; i++)
						{
							this.Provider.m_htValsLookup[num++] = list[i];
						}
						list.Clear();
						this.Provider.m_htValsLookup[num++] = PrefixTransformationProvider.EndMarker;
					}
				}
			}

			// Token: 0x020001C3 RID: 451
			private class ReferenceRowsetUpdateContext : IUpdateContext, IRecordUpdateContextInitialize
			{
				// Token: 0x06000E5D RID: 3677 RVA: 0x0003CC14 File Offset: 0x0003AE14
				public void Initialize(IRowsetManager rowsetManager, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding recordBinding)
				{
					this.TokenIdProvider = tokenIdProvider;
					this.DomainId = domainManager.GetDomainId(this.DomainName);
					this.m_tokenizer = domainManager.GetTokenizer(this.DomainName);
					this.m_tokenizer.Prepare(recordBinding.Schema, recordBinding.GetDomainBinding(this.DomainName), out this.m_tokenizerContext);
				}

				// Token: 0x04000760 RID: 1888
				public string DomainName;

				// Token: 0x04000761 RID: 1889
				public int DomainId;

				// Token: 0x04000762 RID: 1890
				public ITokenIdProvider TokenIdProvider;

				// Token: 0x04000763 RID: 1891
				public Dictionary<int, List<int>> m_htUpdate;

				// Token: 0x04000764 RID: 1892
				public IRecordTokenizer m_tokenizer;

				// Token: 0x04000765 RID: 1893
				public TokenizerContext m_tokenizerContext;

				// Token: 0x04000766 RID: 1894
				public ArraySegmentBuilder<int> m_tokenIdSegmentBuilder = new ArraySegmentBuilder<int>();

				// Token: 0x04000767 RID: 1895
				public BlockedSegmentArray<int> m_intAllocator = new BlockedSegmentArray<int>();

				// Token: 0x04000768 RID: 1896
				public int m_Count;

				// Token: 0x04000769 RID: 1897
				public char[] m_getHashCodeBuffer = new char[0];
			}
		}
	}
}
