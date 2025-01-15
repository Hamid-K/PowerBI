using System;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200006A RID: 106
	[Serializable]
	public abstract class CompactStringSetBase<TId> where TId : struct
	{
		// Token: 0x06000434 RID: 1076
		protected abstract bool Hash2Id_TryGetValue(int hashCode, out TId id);

		// Token: 0x06000435 RID: 1077
		protected abstract void Hash2Id_SetValue(int hashCode, TId id);

		// Token: 0x06000436 RID: 1078
		protected abstract bool Hash2Id_TryAdd(int hashCode, TId id);

		// Token: 0x06000437 RID: 1079
		protected abstract bool Id2Position_TryGetValue(TId id, out int position);

		// Token: 0x06000438 RID: 1080
		protected abstract int Id2Position_GetValue(TId id);

		// Token: 0x06000439 RID: 1081
		protected abstract bool Id2Position_TryAdd(TId id, int position);

		// Token: 0x0600043A RID: 1082
		protected abstract void Id2Position_Remove(TId id);

		// Token: 0x0600043B RID: 1083
		protected abstract bool Id2Position_ContainsKey(TId id);

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600043C RID: 1084
		protected abstract TId ZeroId { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600043D RID: 1085
		protected abstract TId NegativeOneId { get; }

		// Token: 0x0600043E RID: 1086
		protected abstract TId ConvertFromInt(int i);

		// Token: 0x0600043F RID: 1087
		protected abstract TId Negate(TId id);

		// Token: 0x06000440 RID: 1088
		protected abstract bool IsNegative(TId id);

		// Token: 0x06000441 RID: 1089
		protected abstract bool Equals(TId id1, TId id2);

		// Token: 0x06000442 RID: 1090
		protected abstract TId NextId();

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0001B49E File Offset: 0x0001969E
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x0001B4A6 File Offset: 0x000196A6
		public bool CheckForCollisions { get; set; }

		// Token: 0x06000445 RID: 1093 RVA: 0x0001B4B0 File Offset: 0x000196B0
		public CompactStringSetBase()
		{
			this.CheckForCollisions = true;
			this.m_allocator.New(1);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001B503 File Offset: 0x00019703
		public CompactStringSetBase(Func<string, string> normalizeString)
			: this()
		{
			this.m_normalizeString = normalizeString;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001B512 File Offset: 0x00019712
		public CompactStringSetBase(Func<string, string> normalizeString, Func<string, string> normalizeRetrievedString)
			: this()
		{
			this.m_normalizeString = normalizeString;
			this.m_normalizeRetrievedString = normalizeRetrievedString;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001B528 File Offset: 0x00019728
		private Encoding GetEncoding()
		{
			if (this.s_encoding == null)
			{
				this.s_encoding = new UTF8Encoding();
			}
			return this.s_encoding;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001B544 File Offset: 0x00019744
		private bool Equals(byte[] a1, int offset1, int length1, byte[] a2, int offset2, int length2)
		{
			if (length1 == length2)
			{
				for (int i = 0; i < length1; i++)
				{
					if (a1[offset1 + i] != a2[offset2 + i])
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001B578 File Offset: 0x00019778
		private bool EncodingsAreEqual(string s, long position)
		{
			int byteCount = this.GetEncoding().GetByteCount(s);
			if (this.s_encodingBuffer == null || this.s_encodingBuffer.Length < byteCount)
			{
				this.s_encodingBuffer = new byte[byteCount * 2];
			}
			int bytes = this.GetEncoding().GetBytes(s, 0, s.Length, this.s_encodingBuffer, 0);
			ArraySegment<byte> segment = this.m_allocator.GetSegment(position);
			return this.Equals(this.s_encodingBuffer, 0, bytes, segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001B600 File Offset: 0x00019800
		private TId NewId(TId requestedId)
		{
			if (this.Equals(requestedId, this.NegativeOneId))
			{
				TId tid;
				if (!this.m_freeIds.TryDequeue(out tid))
				{
					tid = this.NextId();
				}
				return tid;
			}
			if (this.IsNegative(requestedId))
			{
				throw new ArgumentException("RequestedId may not be negative.");
			}
			if (this.Id2Position_ContainsKey(requestedId))
			{
				throw new InvalidOperationException("RequestedId is already taken.");
			}
			return requestedId;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001B65D File Offset: 0x0001985D
		private bool NormalizedStringsAreEqual(string normalizedString, TId id)
		{
			if (this.m_normalizeString != null)
			{
				return string.Equals(normalizedString, this.m_normalizeString.Invoke(this.GetString(id)));
			}
			return this.EncodingsAreEqual(normalizedString, (long)this.Id2Position_GetValue(id));
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001B690 File Offset: 0x00019890
		public TId GetId(string s)
		{
			TId tid;
			if (!this.TryGetId(s, out tid))
			{
				throw new Exception("The string was not found.");
			}
			return tid;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001B6B4 File Offset: 0x000198B4
		public TId GetOrCreateId(string s)
		{
			return this.GetOrCreateId(s, this.NegativeOneId);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001B6C4 File Offset: 0x000198C4
		public TId GetOrCreateId(string s, TId requestedId)
		{
			TId tid;
			if (!this.TryGetId(s, out tid, requestedId, true))
			{
				throw new Exception("Failed to add the string.");
			}
			return tid;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001B6EA File Offset: 0x000198EA
		public bool TryGetId(string s, out TId id)
		{
			return this.TryGetId(s, out id, this.NegativeOneId, false);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001B6FB File Offset: 0x000198FB
		private void CheckReturnedId(TId id, TId requestedId)
		{
			if (!this.Equals(requestedId, this.NegativeOneId) && !this.Equals(id, requestedId))
			{
				throw new Exception("Unable to assign requested id.");
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001B724 File Offset: 0x00019924
		[MethodImpl(32)]
		private bool TryGetId(string s, out TId id, TId requestedId, bool create)
		{
			if (string.IsNullOrEmpty(s))
			{
				id = this.ZeroId;
				this.CheckReturnedId(id, requestedId);
				return true;
			}
			string text = s;
			if (this.m_normalizeString != null)
			{
				text = this.m_normalizeString.Invoke(s);
			}
			int hashCode = text.GetHashCode();
			if (this.Hash2Id_TryGetValue(hashCode, out id))
			{
				if (!this.CheckForCollisions)
				{
					this.CheckReturnedId(id, requestedId);
					return true;
				}
				ConcurrentDictionary<TId, TId[]> concurrentDictionary;
				if (!this.IsNegative(id))
				{
					if (this.NormalizedStringsAreEqual(text, id))
					{
						this.CheckReturnedId(id, requestedId);
						return true;
					}
					concurrentDictionary = this.m_collisions;
					lock (concurrentDictionary)
					{
						if (this.m_collisions.ContainsKey(this.ConvertFromInt(hashCode)))
						{
							if (this.TryGetId(s, out id, requestedId, create))
							{
								return true;
							}
							if (create)
							{
								throw new Exception("Unexpected error: Id should already exist, but we were unable to retrieve it.");
							}
							return false;
						}
						else
						{
							if (create)
							{
								TId tid = this.NewId(requestedId);
								this.AddString(s, tid);
								TId[] array = new TId[] { id, tid };
								this.m_collisions[this.ConvertFromInt(hashCode)] = array;
								this.Hash2Id_SetValue(hashCode, this.ConvertFromInt(-1));
								id = tid;
								this.CheckReturnedId(id, requestedId);
								return true;
							}
							id = this.NegativeOneId;
							return false;
						}
					}
				}
				foreach (TId tid2 in this.m_collisions[this.ConvertFromInt(hashCode)])
				{
					if (this.NormalizedStringsAreEqual(text, tid2))
					{
						id = tid2;
						this.CheckReturnedId(id, requestedId);
						return true;
					}
				}
				concurrentDictionary = this.m_collisions;
				lock (concurrentDictionary)
				{
					TId[] array3 = this.m_collisions[this.ConvertFromInt(hashCode)];
					foreach (TId tid3 in array3)
					{
						if (this.NormalizedStringsAreEqual(text, tid3))
						{
							id = tid3;
							this.CheckReturnedId(id, requestedId);
							return true;
						}
					}
					if (create)
					{
						Array.Resize<TId>(ref array3, array3.Length + 1);
						TId tid4 = this.NewId(requestedId);
						this.AddString(s, tid4);
						array3[array3.Length - 1] = tid4;
						this.m_collisions[this.ConvertFromInt(hashCode)] = array3;
						id = tid4;
						this.CheckReturnedId(id, requestedId);
						return true;
					}
					id = this.NegativeOneId;
					return false;
				}
			}
			if (!create)
			{
				id = this.NegativeOneId;
				return false;
			}
			id = this.NewId(requestedId);
			this.AddString(s, id);
			if (!this.Hash2Id_TryAdd(hashCode, id))
			{
				this.RemoveString(id);
				return this.TryGetId(s, out id, requestedId, create);
			}
			this.CheckReturnedId(id, requestedId);
			return true;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001BA54 File Offset: 0x00019C54
		private void RemoveString(TId id)
		{
			if (!this.Equals(id, this.ZeroId))
			{
				int num = this.Id2Position_GetValue(id);
				ArraySegment<byte> segment = this.m_allocator.GetSegment((long)num);
				ConcurrentQueue<int> concurrentQueue;
				if (!this.m_freeSegments.TryGetValue(segment.Count, out concurrentQueue))
				{
					concurrentQueue = new ConcurrentQueue<int>();
				}
				if (!this.m_freeSegments.TryAdd(segment.Count, concurrentQueue))
				{
					concurrentQueue = this.m_freeSegments[segment.Count];
				}
				concurrentQueue.Enqueue(num);
				this.Id2Position_Remove(id);
				this.m_freeIds.Enqueue(id);
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001BAE4 File Offset: 0x00019CE4
		private void AddString(string s, TId id)
		{
			int byteCount = this.GetEncoding().GetByteCount(s);
			int num = -1;
			ArraySegment<byte> arraySegment = default(ArraySegment<byte>);
			bool flag = false;
			ConcurrentQueue<int> concurrentQueue;
			if (this.m_freeSegments.TryGetValue(byteCount, out concurrentQueue) && concurrentQueue.TryDequeue(out num))
			{
				arraySegment = this.m_allocator.GetSegment((long)num);
				flag = true;
			}
			if (!flag)
			{
				num = (int)this.m_allocator.New(byteCount, out arraySegment);
			}
			this.GetEncoding().GetBytes(s, 0, s.Length, arraySegment.Array, arraySegment.Offset);
			if (!this.Id2Position_TryAdd(id, num))
			{
				throw new Exception("Unexpected id collision. This should never happen.");
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001BB84 File Offset: 0x00019D84
		public bool TryGetString(TId id, out string str)
		{
			if (this.Equals(id, this.ZeroId))
			{
				str = string.Empty;
				return true;
			}
			int num;
			if (!this.Id2Position_TryGetValue(id, out num))
			{
				str = null;
				return false;
			}
			ArraySegment<byte> segment = this.m_allocator.GetSegment((long)num);
			str = this.GetEncoding().GetString(segment.Array, segment.Offset, segment.Count);
			if (this.m_normalizeRetrievedString != null)
			{
				str = this.m_normalizeRetrievedString.Invoke(str);
			}
			return true;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001BC04 File Offset: 0x00019E04
		public string GetString(TId id)
		{
			string text;
			if (!this.TryGetString(id, out text))
			{
				throw new Exception(string.Format("String with id {0} was not found.", id));
			}
			return text;
		}

		// Token: 0x040000B6 RID: 182
		private ConcurrentQueue<TId> m_freeIds = new ConcurrentQueue<TId>();

		// Token: 0x040000B7 RID: 183
		private ByteSegmentAllocator m_allocator = new ByteSegmentAllocator();

		// Token: 0x040000B8 RID: 184
		private Func<string, string> m_normalizeString;

		// Token: 0x040000B9 RID: 185
		private Func<string, string> m_normalizeRetrievedString;

		// Token: 0x040000BA RID: 186
		[ThreadStatic]
		private Encoding s_encoding;

		// Token: 0x040000BB RID: 187
		[ThreadStatic]
		private byte[] s_encodingBuffer;

		// Token: 0x040000BC RID: 188
		private ConcurrentDictionary<TId, TId[]> m_collisions = new ConcurrentDictionary<TId, TId[]>();

		// Token: 0x040000BD RID: 189
		private ConcurrentDictionary<int, ConcurrentQueue<int>> m_freeSegments = new ConcurrentDictionary<int, ConcurrentQueue<int>>();
	}
}
