using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000065 RID: 101
	[Serializable]
	public sealed class OneToOneTokenIdProvider : ITokenIdProvider, IRawSerializable, ISerializable, IMemoryUsage
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00012B01 File Offset: 0x00010D01
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x00012B09 File Offset: 0x00010D09
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00012B12 File Offset: 0x00010D12
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x00012B1A File Offset: 0x00010D1A
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00012B23 File Offset: 0x00010D23
		public bool SupportsGetToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00012B28 File Offset: 0x00010D28
		public void ClearTokenIds()
		{
			lock (this)
			{
				this.m_tokenAllocator.Reset();
				this.m_tokenIdToTokenInfo.Clear();
				this.m_tokenIdToTokenInfo.Add(default(OneToOneTokenIdProvider.TokenInfo));
				this.m_nextTokenId = 1;
				foreach (OneToOneTokenIdProvider.DomainInfo domainInfo in this.m_domainIdToDomainInfo.Values)
				{
					domainInfo.StringToIdMap.Clear();
				}
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00012BD0 File Offset: 0x00010DD0
		public long MemoryUsage
		{
			get
			{
				long num = 0L;
				foreach (OneToOneTokenIdProvider.DomainInfo domainInfo in this.m_domainIdToDomainInfo.Values)
				{
					num += (long)(domainInfo.StringToIdMap.Count * 20);
				}
				return num + this.m_tokenAllocator.MemoryUsage + (long)(20 * this.m_tokenIdToTokenInfo.Count);
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00012C54 File Offset: 0x00010E54
		public OneToOneTokenIdProvider()
		{
			this.m_tokenIdToTokenInfo = new List<OneToOneTokenIdProvider.TokenInfo>();
			this.m_tokenIdToTokenInfo.Add(default(OneToOneTokenIdProvider.TokenInfo));
			this.m_nextTokenId = 1;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00012CA4 File Offset: 0x00010EA4
		private OneToOneTokenIdProvider(SerializationInfo info, StreamingContext context)
		{
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.m_tokenAllocator = (BlockedSegmentArray<char>)info.GetValue("m_tokenAllocator", typeof(BlockedSegmentArray<char>));
			this.m_domainIdToDomainInfo = (Dictionary<int, OneToOneTokenIdProvider.DomainInfo>)info.GetValue("m_domainIdToDomainInfo", typeof(Dictionary<int, OneToOneTokenIdProvider.DomainInfo>));
			this.m_nextTokenId = (int)info.GetValue("m_nextTokenId", typeof(int));
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				this.m_tokenIdToTokenInfo = (List<OneToOneTokenIdProvider.TokenInfo>)info.GetValue("m_tokenIdToString", typeof(List<OneToOneTokenIdProvider.TokenInfo>));
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00012D98 File Offset: 0x00010F98
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EnableRawSerialization", ((IRawSerializable)this).EnableRawSerialization);
			info.AddValue("RawSerializationID", ((IRawSerializable)this).RawSerializationID);
			info.AddValue("m_tokenAllocator", this.m_tokenAllocator);
			info.AddValue("m_domainIdToDomainInfo", this.m_domainIdToDomainInfo);
			info.AddValue("m_nextTokenId", this.m_nextTokenId);
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				info.AddValue("m_tokenIdToString", this.m_tokenIdToTokenInfo);
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00012E14 File Offset: 0x00011014
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt32(s, this.m_tokenIdToTokenInfo.Count);
			Dictionary<char[], int> dictionary = new Dictionary<char[], int>();
			for (int i = 0; i < this.m_tokenAllocator.m_blocks.Count; i++)
			{
				dictionary.Add(this.m_tokenAllocator.m_blocks[i], i);
			}
			new byte[16];
			for (int j = 1; j < this.m_tokenIdToTokenInfo.Count; j++)
			{
				OneToOneTokenIdProvider.TokenInfo tokenInfo = this.m_tokenIdToTokenInfo[j];
				StreamUtilities.WriteInt32(s, tokenInfo.DomainId);
				if (tokenInfo.TokenString.Array != null)
				{
					StreamUtilities.WriteInt32(s, dictionary[tokenInfo.TokenString.Array]);
					StreamUtilities.WriteInt32(s, tokenInfo.TokenString.Offset);
					StreamUtilities.WriteInt32(s, tokenInfo.TokenString.Length);
				}
				else
				{
					StreamUtilities.WriteInt32(s, int.MaxValue);
				}
			}
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00012F14 File Offset: 0x00011114
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			int num = StreamUtilities.ReadInt32(s);
			this.m_tokenIdToTokenInfo = new List<OneToOneTokenIdProvider.TokenInfo>(num);
			this.m_tokenIdToTokenInfo.Add(default(OneToOneTokenIdProvider.TokenInfo));
			Dictionary<int, char[]> dictionary = new Dictionary<int, char[]>();
			for (int i = 0; i < this.m_tokenAllocator.m_blocks.Count; i++)
			{
				dictionary.Add(i, this.m_tokenAllocator.m_blocks[i]);
			}
			dictionary.Add(int.MaxValue, null);
			for (int j = 1; j < num; j++)
			{
				char[] array = null;
				int num2 = 0;
				int num3 = 0;
				int num4 = StreamUtilities.ReadInt32(s);
				int num5 = StreamUtilities.ReadInt32(s);
				if (num5 != 2147483647)
				{
					array = dictionary[num5];
					num2 = StreamUtilities.ReadInt32(s);
					num3 = StreamUtilities.ReadInt32(s);
				}
				OneToOneTokenIdProvider.TokenInfo tokenInfo = new OneToOneTokenIdProvider.TokenInfo
				{
					DomainId = num4,
					TokenString = new StringExtent
					{
						Array = array,
						Offset = num2,
						Length = num3
					}
				};
				this.m_tokenIdToTokenInfo.Add(tokenInfo);
				if (!tokenInfo.TokenString.IsEmpty)
				{
					this.m_domainIdToDomainInfo[tokenInfo.DomainId].StringToIdMap.Add(tokenInfo.TokenString, j);
				}
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00013094 File Offset: 0x00011294
		public void AddDomain(string domainName, int domainId)
		{
			lock (this)
			{
				this.m_domainIdToDomainInfo.Add(domainId, new OneToOneTokenIdProvider.DomainInfo(domainId, domainName));
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000130D8 File Offset: 0x000112D8
		public void DropDomain(int domainId)
		{
			lock (this)
			{
				this.m_domainIdToDomainInfo.Remove(domainId);
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00013114 File Offset: 0x00011314
		public bool IsTemporary(int tokenId)
		{
			return tokenId < 0;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001311A File Offset: 0x0001131A
		public int GetDomainId(int token)
		{
			return this.m_tokenIdToTokenInfo[token].DomainId;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00013130 File Offset: 0x00011330
		public bool TryGetTokenId(StringExtent token, int domainId, out int tokenId)
		{
			bool flag;
			lock (this)
			{
				OneToOneTokenIdProvider.DomainInfo domainInfo;
				if (this.m_domainIdToDomainInfo.TryGetValue(domainId, ref domainInfo))
				{
					flag = domainInfo.StringToIdMap.TryGetValue(token, ref tokenId);
				}
				else
				{
					tokenId = 0;
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00013184 File Offset: 0x00011384
		public int GetOrCreateTokenId(StringExtent token, int domainId)
		{
			if (token.IsEmpty)
			{
				throw new ArgumentException("Token must have length >= 1.");
			}
			int num2;
			lock (this)
			{
				OneToOneTokenIdProvider.DomainInfo domainInfo;
				if (!this.m_domainIdToDomainInfo.TryGetValue(domainId, ref domainInfo))
				{
					domainInfo = new OneToOneTokenIdProvider.DomainInfo(domainId, null);
					this.m_domainIdToDomainInfo.Add(domainId, domainInfo);
				}
				int num;
				if (!domainInfo.StringToIdMap.TryGetValue(token, ref num))
				{
					num2 = this.m_nextTokenId;
					this.m_nextTokenId = num2 + 1;
					num = num2;
					ArraySegment<char> arraySegment = this.m_tokenAllocator.New(token.Length);
					token.CopyTo(0, arraySegment.Array, arraySegment.Offset, token.Length);
					if (token.Array == null)
					{
						throw new Exception("Token was unexpectedly empty.");
					}
					this.m_tokenIdToTokenInfo.Add(new OneToOneTokenIdProvider.TokenInfo(arraySegment, domainId));
					this.m_domainIdToDomainInfo[domainId].StringToIdMap.Add(arraySegment, num);
				}
				num2 = num;
			}
			return num2;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00013290 File Offset: 0x00011490
		public int CreateTokenId(int domainId)
		{
			int num;
			lock (this)
			{
				int nextTokenId = this.m_nextTokenId;
				this.m_tokenIdToTokenInfo.Add(new OneToOneTokenIdProvider.TokenInfo(default(StringExtent), domainId));
				this.m_nextTokenId++;
				num = nextTokenId;
			}
			return num;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000132F0 File Offset: 0x000114F0
		public bool TryGetToken(int tokenId, out StringExtent token)
		{
			if (tokenId < this.m_tokenIdToTokenInfo.Count)
			{
				token = this.m_tokenIdToTokenInfo[tokenId].TokenString;
				return true;
			}
			token = default(StringExtent);
			return false;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00013321 File Offset: 0x00011521
		public StringExtent GetToken(int token)
		{
			return this.m_tokenIdToTokenInfo[token].TokenString;
		}

		// Token: 0x04000154 RID: 340
		private Dictionary<int, OneToOneTokenIdProvider.DomainInfo> m_domainIdToDomainInfo = new Dictionary<int, OneToOneTokenIdProvider.DomainInfo>();

		// Token: 0x04000155 RID: 341
		private int m_nextTokenId;

		// Token: 0x04000156 RID: 342
		private BlockedSegmentArray<char> m_tokenAllocator = new BlockedSegmentArray<char>();

		// Token: 0x04000157 RID: 343
		private List<OneToOneTokenIdProvider.TokenInfo> m_tokenIdToTokenInfo;

		// Token: 0x02000152 RID: 338
		[Serializable]
		private sealed class DomainInfo : IDeserializationCallback
		{
			// Token: 0x17000269 RID: 617
			// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x000370DC File Offset: 0x000352DC
			// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x000370E4 File Offset: 0x000352E4
			public int Id { get; private set; }

			// Token: 0x1700026A RID: 618
			// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x000370ED File Offset: 0x000352ED
			// (set) Token: 0x06000CBA RID: 3258 RVA: 0x000370F5 File Offset: 0x000352F5
			public string DomainName { get; private set; }

			// Token: 0x06000CBB RID: 3259 RVA: 0x000370FE File Offset: 0x000352FE
			public DomainInfo(int id, string domainName)
			{
				this.Id = id;
				this.DomainName = domainName;
			}

			// Token: 0x06000CBC RID: 3260 RVA: 0x00037124 File Offset: 0x00035324
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				this.StringToIdMap = new Dictionary<StringExtent, int>(StringExtent.EqualityComparer);
			}

			// Token: 0x0400059C RID: 1436
			[NonSerialized]
			internal Dictionary<StringExtent, int> StringToIdMap = new Dictionary<StringExtent, int>(StringExtent.EqualityComparer);
		}

		// Token: 0x02000153 RID: 339
		[Serializable]
		internal struct TokenInfo
		{
			// Token: 0x06000CBD RID: 3261 RVA: 0x00037136 File Offset: 0x00035336
			public TokenInfo(StringExtent tokenString, int domainId)
			{
				this.TokenString = tokenString;
				this.DomainId = domainId;
			}

			// Token: 0x0400059D RID: 1437
			public StringExtent TokenString;

			// Token: 0x0400059E RID: 1438
			public int DomainId;
		}
	}
}
