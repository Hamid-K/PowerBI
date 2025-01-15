using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200003E RID: 62
	[Serializable]
	public class CustomEditTransformationFilter : ITransformationFilter, IProviderInitialize, IRowsetConsumer, ISessionable
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000BF3B File Offset: 0x0000A13B
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000BF43 File Offset: 0x0000A143
		public int MaxTransformationsPerRecord { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000BF4C File Offset: 0x0000A14C
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000BF54 File Offset: 0x0000A154
		public string DomainName { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000BF5D File Offset: 0x0000A15D
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000BF6A File Offset: 0x0000A16A
		public string ReferenceRowsetName
		{
			get
			{
				return this.m_contextFilter.ReferenceRowsetName;
			}
			set
			{
				this.m_contextFilter.ReferenceRowsetName = value;
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000BF78 File Offset: 0x0000A178
		public CustomEditTransformationFilter()
		{
			this.MaxTransformationsPerRecord = int.MaxValue;
			this.m_contextFilter = new ContextualTransformationFilter();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000BF96 File Offset: 0x0000A196
		public void Initialize(IDomainManager domainManager, string domainName)
		{
			this.m_contextFilter.Initialize(domainManager, domainName);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000BFA5 File Offset: 0x0000A1A5
		public ISession CreateSession()
		{
			return new CustomEditTransformationFilter.Session
			{
				m_contextFilterSession = this.m_contextFilter.CreateSession()
			};
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000BFBD File Offset: 0x0000A1BD
		public IList<IRowsetSink> RowsetSinks
		{
			get
			{
				if (this.m_contextFilter != null)
				{
					return ((IRowsetConsumer)this.m_contextFilter).RowsetSinks;
				}
				return new IRowsetSink[0];
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000BFD9 File Offset: 0x0000A1D9
		public void RequestRowsets(IRowsetDistributor rowsetDistributor)
		{
			if (this.m_contextFilter != null)
			{
				((IRowsetConsumer)this.m_contextFilter).RequestRowsets(rowsetDistributor);
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000BFF0 File Offset: 0x0000A1F0
		public void Prepare(ISession session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSequence)
		{
			CustomEditTransformationFilter.Session session2 = (CustomEditTransformationFilter.Session)session;
			this.m_contextFilter.Prepare(session2.m_contextFilterSession, tokenIdProvider, tokenSequence);
			session2.m_tokenIdProvider = tokenIdProvider;
			session2.m_filterTokenSequence = tokenSequence;
			session2.m_positionGotTransformationThisRound.Length = session2.m_filterTokenSequence.Count;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000C03B File Offset: 0x0000A23B
		public bool AllowTransformations(ISession session, int fromTokenIndex)
		{
			return true;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000C03E File Offset: 0x0000A23E
		public bool AllowTransformation(ISession session, int fromTokenIndex, Transformation transformation)
		{
			return true;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000C041 File Offset: 0x0000A241
		public void FilterTransformations(ISession session, int fromTokenIndex, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			filteredTransformationMatchList = transformationMatchList;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000C04C File Offset: 0x0000A24C
		public void FilterTransformations(ISession session, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			CustomEditTransformationFilter.Session session2 = (CustomEditTransformationFilter.Session)session;
			session2.m_transformationInfoComparer.TokenIdProvider = session2.m_tokenIdProvider;
			for (int i = 0; i < transformationMatchList.Count; i++)
			{
				Transformation transformation = transformationMatchList.Array[transformationMatchList.Offset + i].Transformation;
				EditTransformationMetadata editTransformationMetadata = new EditTransformationMetadata(transformation.Metadata);
				CustomEditTransformationFilter.TransformationInfo transformationInfo = session2.m_transformationInfo.Add();
				transformationInfo.WasUsed = false;
				transformationInfo.IsTemporary = session2.m_tokenIdProvider.IsTemporary(transformation.From[0]);
				transformationInfo.HasContext = this.m_contextFilter.SatisfiesContext(session2.m_contextFilterSession, transformationMatchList, i);
				transformationInfo.EditDistance = (double)editTransformationMetadata.EditDistance;
				transformationInfo.PrefixMatchLength = (int)editTransformationMetadata.PrefixMatchLength;
				transformationInfo.ToTokenId = transformation.To[0];
				transformationInfo.MatchPosition = transformationMatchList.Array[transformationMatchList.Offset + i].Position;
				transformationInfo.Transformation = transformation;
				session2.m_transformationInfoHeap.Add(transformationInfo);
			}
			int num = this.MaxTransformationsPerRecord;
			while (session2.m_keptTransformations.Count < this.MaxTransformationsPerRecord)
			{
				int num2 = 0;
				session2.m_unkeptTransformations.Clear();
				num = Math.Max(1, num / 2);
				session2.m_positionGotTransformationThisRound.SetAll(false);
				while (session2.m_transformationInfoHeap.Count > 0)
				{
					CustomEditTransformationFilter.TransformationInfo transformationInfo2 = session2.m_transformationInfoHeap.Pop();
					if (!session2.m_positionGotTransformationThisRound[transformationInfo2.MatchPosition])
					{
						session2.m_keptTransformations.Add(transformationInfo2);
						transformationInfo2.WasUsed = true;
						session2.m_positionGotTransformationThisRound[transformationInfo2.MatchPosition] = true;
						if (++num2 == num)
						{
							break;
						}
					}
					else
					{
						session2.m_unkeptTransformations.Add(transformationInfo2);
					}
				}
				foreach (CustomEditTransformationFilter.TransformationInfo transformationInfo3 in session2.m_unkeptTransformations)
				{
					session2.m_transformationInfoHeap.Add(transformationInfo3);
				}
				if (num2 == 0)
				{
					break;
				}
			}
			session2.m_keptTransformations.Sort(session2.m_transformationInfoComparer_ByPosition);
			filteredTransformationMatchList = session2.m_transformationMatchAllocator.New(session2.m_keptTransformations.Count);
			for (int j = 0; j < session2.m_keptTransformations.Count; j++)
			{
				filteredTransformationMatchList.Array[filteredTransformationMatchList.Offset + j] = new TransformationMatch
				{
					Position = session2.m_keptTransformations[j].MatchPosition,
					Transformation = session2.m_keptTransformations[j].Transformation
				};
			}
		}

		// Token: 0x040000CC RID: 204
		private ContextualTransformationFilter m_contextFilter;

		// Token: 0x0200012D RID: 301
		private class Session : ISession
		{
			// Token: 0x06000BEF RID: 3055 RVA: 0x00033B48 File Offset: 0x00031D48
			public Session()
			{
				this.m_positionGotTransformationThisRound = new BitArray(0);
				this.m_transformationInfo = new ObjectVector<CustomEditTransformationFilter.TransformationInfo>(10);
				this.m_keptTransformations = new ArraySegmentBuilder<CustomEditTransformationFilter.TransformationInfo>();
				this.m_unkeptTransformations = new List<CustomEditTransformationFilter.TransformationInfo>();
				this.m_transformationInfoComparer_ByPosition = new CustomEditTransformationFilter.TransformationInfoComparer_ByPosition();
				this.m_transformationInfoComparer = new CustomEditTransformationFilter.TransformationInfoComparer();
				this.m_transformationInfoHeap = new BoundedHeap<CustomEditTransformationFilter.TransformationInfo>(new Comparison<CustomEditTransformationFilter.TransformationInfo>(this.m_transformationInfoComparer.NegCompare), int.MaxValue);
			}

			// Token: 0x06000BF0 RID: 3056 RVA: 0x00033BD8 File Offset: 0x00031DD8
			public void Reset()
			{
				this.m_transformationInfo.Clear();
				this.m_keptTransformations.Reset();
				this.m_unkeptTransformations.Clear();
				this.m_transformationInfoHeap.ClearFast();
				this.m_filterTokenSequence = default(TokenSequence);
				this.m_transformationInfoComparer.TokenIdProvider = null;
				this.m_transformationMatchAllocator.Reset();
				this.m_transformationInfoAllocator.Reset();
				if (this.m_contextFilterSession != null)
				{
					this.m_contextFilterSession.Reset();
				}
			}

			// Token: 0x040004A4 RID: 1188
			public BitArray m_positionGotTransformationThisRound;

			// Token: 0x040004A5 RID: 1189
			public ObjectVector<CustomEditTransformationFilter.TransformationInfo> m_transformationInfo;

			// Token: 0x040004A6 RID: 1190
			public CustomEditTransformationFilter.TransformationInfoComparer m_transformationInfoComparer;

			// Token: 0x040004A7 RID: 1191
			public CustomEditTransformationFilter.TransformationInfoComparer_ByPosition m_transformationInfoComparer_ByPosition;

			// Token: 0x040004A8 RID: 1192
			public ArraySegmentBuilder<CustomEditTransformationFilter.TransformationInfo> m_keptTransformations;

			// Token: 0x040004A9 RID: 1193
			public BoundedHeap<CustomEditTransformationFilter.TransformationInfo> m_transformationInfoHeap;

			// Token: 0x040004AA RID: 1194
			public List<CustomEditTransformationFilter.TransformationInfo> m_unkeptTransformations;

			// Token: 0x040004AB RID: 1195
			public ITokenIdProvider m_tokenIdProvider;

			// Token: 0x040004AC RID: 1196
			public TokenSequence m_filterTokenSequence;

			// Token: 0x040004AD RID: 1197
			public ISession m_contextFilterSession;

			// Token: 0x040004AE RID: 1198
			public BlockedSegmentArray<TransformationMatch> m_transformationMatchAllocator = new BlockedSegmentArray<TransformationMatch>();

			// Token: 0x040004AF RID: 1199
			public BlockedSegmentArray<CustomEditTransformationFilter.TransformationInfo> m_transformationInfoAllocator = new BlockedSegmentArray<CustomEditTransformationFilter.TransformationInfo>();
		}

		// Token: 0x0200012E RID: 302
		[Serializable]
		[StructLayout(0)]
		private class TransformationInfo
		{
			// Token: 0x040004B0 RID: 1200
			public Transformation Transformation;

			// Token: 0x040004B1 RID: 1201
			public double EditDistance;

			// Token: 0x040004B2 RID: 1202
			public int PrefixMatchLength;

			// Token: 0x040004B3 RID: 1203
			public int ToTokenId;

			// Token: 0x040004B4 RID: 1204
			public int MatchPosition;

			// Token: 0x040004B5 RID: 1205
			public bool WasUsed;

			// Token: 0x040004B6 RID: 1206
			public bool IsTemporary;

			// Token: 0x040004B7 RID: 1207
			public bool HasContext;
		}

		// Token: 0x0200012F RID: 303
		[Serializable]
		private class TransformationInfoComparer_ByPosition : IComparer<CustomEditTransformationFilter.TransformationInfo>
		{
			// Token: 0x06000BF2 RID: 3058 RVA: 0x00033C5C File Offset: 0x00031E5C
			public int Compare(CustomEditTransformationFilter.TransformationInfo x, CustomEditTransformationFilter.TransformationInfo y)
			{
				if (x.MatchPosition != y.MatchPosition)
				{
					if (x.MatchPosition >= y.MatchPosition)
					{
						return 1;
					}
					return -1;
				}
				else if (x.IsTemporary != y.IsTemporary)
				{
					if (!x.IsTemporary)
					{
						return 1;
					}
					return -1;
				}
				else if (x.HasContext != y.HasContext)
				{
					if (!x.HasContext)
					{
						return 1;
					}
					return -1;
				}
				else if (x.EditDistance != y.EditDistance)
				{
					if (x.EditDistance >= y.EditDistance)
					{
						return 1;
					}
					return -1;
				}
				else if (x.PrefixMatchLength != y.PrefixMatchLength)
				{
					if (x.PrefixMatchLength <= y.PrefixMatchLength)
					{
						return 1;
					}
					return -1;
				}
				else
				{
					if (x.ToTokenId == y.ToTokenId)
					{
						return 0;
					}
					if (x.ToTokenId >= y.ToTokenId)
					{
						return 1;
					}
					return -1;
				}
			}

			// Token: 0x040004B8 RID: 1208
			public ITokenIdProvider TokenIdProvider;
		}

		// Token: 0x02000130 RID: 304
		[Serializable]
		private class TransformationInfoComparer : IComparer<CustomEditTransformationFilter.TransformationInfo>
		{
			// Token: 0x06000BF4 RID: 3060 RVA: 0x00033D26 File Offset: 0x00031F26
			public int NegCompare(CustomEditTransformationFilter.TransformationInfo x, CustomEditTransformationFilter.TransformationInfo y)
			{
				return -this.Compare(x, y);
			}

			// Token: 0x06000BF5 RID: 3061 RVA: 0x00033D34 File Offset: 0x00031F34
			public int Compare(CustomEditTransformationFilter.TransformationInfo x, CustomEditTransformationFilter.TransformationInfo y)
			{
				if (x.IsTemporary != y.IsTemporary)
				{
					if (!x.IsTemporary)
					{
						return 1;
					}
					return -1;
				}
				else if (x.HasContext != y.HasContext)
				{
					if (!x.HasContext)
					{
						return 1;
					}
					return -1;
				}
				else if (x.EditDistance != y.EditDistance)
				{
					if (x.EditDistance >= y.EditDistance)
					{
						return 1;
					}
					return -1;
				}
				else if (x.PrefixMatchLength != y.PrefixMatchLength)
				{
					if (x.PrefixMatchLength <= y.PrefixMatchLength)
					{
						return 1;
					}
					return -1;
				}
				else if (x.ToTokenId != y.ToTokenId)
				{
					if (x.ToTokenId >= y.ToTokenId)
					{
						return 1;
					}
					return -1;
				}
				else
				{
					if (x.MatchPosition == y.MatchPosition)
					{
						return 0;
					}
					if (x.MatchPosition >= y.MatchPosition)
					{
						return 1;
					}
					return -1;
				}
			}

			// Token: 0x040004B9 RID: 1209
			public ITokenIdProvider TokenIdProvider;
		}
	}
}
