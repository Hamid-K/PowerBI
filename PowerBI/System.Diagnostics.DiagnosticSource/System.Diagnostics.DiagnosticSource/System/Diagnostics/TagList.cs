using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x02000030 RID: 48
	[SecuritySafeCritical]
	public struct TagList : IList<KeyValuePair<string, object>>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, IReadOnlyList<KeyValuePair<string, object>>, IReadOnlyCollection<KeyValuePair<string, object>>
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000677C File Offset: 0x0000497C
		public unsafe TagList([Nullable(new byte[] { 0, 0, 1, 2 })] ReadOnlySpan<KeyValuePair<string, object>> tagList)
		{
			this = default(TagList);
			this._tagsCount = tagList.Length;
			switch (this._tagsCount)
			{
			case 0:
				return;
			case 1:
				goto IL_00CF;
			case 2:
				goto IL_00BC;
			case 3:
				goto IL_00A9;
			case 4:
				goto IL_0096;
			case 5:
				goto IL_0083;
			case 6:
				goto IL_0070;
			case 7:
				break;
			case 8:
				this.Tag8 = *tagList[7];
				break;
			default:
				this._overflowTags = new KeyValuePair<string, object>[this._tagsCount + 8];
				tagList.CopyTo(this._overflowTags);
				return;
			}
			this.Tag7 = *tagList[6];
			IL_0070:
			this.Tag6 = *tagList[5];
			IL_0083:
			this.Tag5 = *tagList[4];
			IL_0096:
			this.Tag4 = *tagList[3];
			IL_00A9:
			this.Tag3 = *tagList[2];
			IL_00BC:
			this.Tag2 = *tagList[1];
			IL_00CF:
			this.Tag1 = *tagList[0];
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00006892 File Offset: 0x00004A92
		public readonly int Count
		{
			get
			{
				return this._tagsCount;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000689A File Offset: 0x00004A9A
		public readonly bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700006D RID: 109
		[Nullable(new byte[] { 0, 1, 2 })]
		public KeyValuePair<string, object> this[int index]
		{
			[return: Nullable(new byte[] { 0, 1, 2 })]
			readonly get
			{
				if (index >= this._tagsCount)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this._overflowTags != null)
				{
					return this._overflowTags[index];
				}
				KeyValuePair<string, object> keyValuePair;
				switch (index)
				{
				case 0:
					keyValuePair = this.Tag1;
					break;
				case 1:
					keyValuePair = this.Tag2;
					break;
				case 2:
					keyValuePair = this.Tag3;
					break;
				case 3:
					keyValuePair = this.Tag4;
					break;
				case 4:
					keyValuePair = this.Tag5;
					break;
				case 5:
					keyValuePair = this.Tag6;
					break;
				case 6:
					keyValuePair = this.Tag7;
					break;
				case 7:
					keyValuePair = this.Tag8;
					break;
				default:
					keyValuePair = default(KeyValuePair<string, object>);
					break;
				}
				return keyValuePair;
			}
			[param: Nullable(new byte[] { 0, 1, 2 })]
			set
			{
				if (index >= this._tagsCount)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this._overflowTags != null)
				{
					this._overflowTags[index] = value;
					return;
				}
				switch (index)
				{
				case 0:
					this.Tag1 = value;
					return;
				case 1:
					this.Tag2 = value;
					return;
				case 2:
					this.Tag3 = value;
					return;
				case 3:
					this.Tag4 = value;
					return;
				case 4:
					this.Tag5 = value;
					return;
				case 5:
					this.Tag6 = value;
					return;
				case 6:
					this.Tag7 = value;
					return;
				case 7:
					this.Tag8 = value;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000069F1 File Offset: 0x00004BF1
		[NullableContext(1)]
		public void Add(string key, [Nullable(2)] object value)
		{
			this.Add(new KeyValuePair<string, object>(key, value));
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006A00 File Offset: 0x00004C00
		public void Add([Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> tag)
		{
			if (this._overflowTags != null)
			{
				if (this._tagsCount == this._overflowTags.Length)
				{
					Array.Resize<KeyValuePair<string, object>>(ref this._overflowTags, this._tagsCount + 8);
				}
				KeyValuePair<string, object>[] overflowTags = this._overflowTags;
				int tagsCount = this._tagsCount;
				this._tagsCount = tagsCount + 1;
				overflowTags[tagsCount] = tag;
				return;
			}
			switch (this._tagsCount)
			{
			case 0:
				this.Tag1 = tag;
				break;
			case 1:
				this.Tag2 = tag;
				break;
			case 2:
				this.Tag3 = tag;
				break;
			case 3:
				this.Tag4 = tag;
				break;
			case 4:
				this.Tag5 = tag;
				break;
			case 5:
				this.Tag6 = tag;
				break;
			case 6:
				this.Tag7 = tag;
				break;
			case 7:
				this.Tag8 = tag;
				break;
			case 8:
				this.MoveTagsToTheArray();
				this._overflowTags[8] = tag;
				break;
			default:
				return;
			}
			this._tagsCount++;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006AF4 File Offset: 0x00004CF4
		public unsafe readonly void CopyTo([Nullable(new byte[] { 0, 0, 1, 2 })] Span<KeyValuePair<string, object>> tags)
		{
			if (tags.Length < this._tagsCount)
			{
				throw new ArgumentException(SR.Arg_BufferTooSmall);
			}
			if (this._overflowTags != null)
			{
				MemoryExtensions.AsSpan<KeyValuePair<string, object>>(this._overflowTags).Slice(0, this._tagsCount).CopyTo(tags);
				return;
			}
			switch (this._tagsCount)
			{
			case 0:
				return;
			case 1:
				goto IL_00FD;
			case 2:
				goto IL_00EA;
			case 3:
				goto IL_00D7;
			case 4:
				goto IL_00C4;
			case 5:
				goto IL_00B1;
			case 6:
				goto IL_009E;
			case 7:
				break;
			case 8:
				*tags[7] = this.Tag8;
				break;
			default:
				return;
			}
			*tags[6] = this.Tag7;
			IL_009E:
			*tags[5] = this.Tag6;
			IL_00B1:
			*tags[4] = this.Tag5;
			IL_00C4:
			*tags[3] = this.Tag4;
			IL_00D7:
			*tags[2] = this.Tag3;
			IL_00EA:
			*tags[1] = this.Tag2;
			IL_00FD:
			*tags[0] = this.Tag1;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006C14 File Offset: 0x00004E14
		public readonly void CopyTo([Nullable(new byte[] { 1, 0, 1, 2 })] KeyValuePair<string, object>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if ((ulong)arrayIndex >= (ulong)((long)array.Length))
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			this.CopyTo(MemoryExtensions.AsSpan<KeyValuePair<string, object>>(array).Slice(arrayIndex));
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006C58 File Offset: 0x00004E58
		public void Insert(int index, [Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> item)
		{
			if (index > this._tagsCount)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index == this._tagsCount)
			{
				this.Add(item);
				return;
			}
			if (this._tagsCount == 8 && this._overflowTags == null)
			{
				this.MoveTagsToTheArray();
			}
			if (this._overflowTags != null)
			{
				if (this._tagsCount == this._overflowTags.Length)
				{
					Array.Resize<KeyValuePair<string, object>>(ref this._overflowTags, this._tagsCount + 8);
				}
				for (int i = this._tagsCount; i > index; i--)
				{
					this._overflowTags[i] = this._overflowTags[i - 1];
				}
				this._overflowTags[index] = item;
				this._tagsCount++;
				return;
			}
			switch (index)
			{
			case 0:
				this.Tag8 = this.Tag7;
				this.Tag7 = this.Tag6;
				this.Tag6 = this.Tag5;
				this.Tag5 = this.Tag4;
				this.Tag4 = this.Tag3;
				this.Tag3 = this.Tag2;
				this.Tag2 = this.Tag1;
				this.Tag1 = item;
				break;
			case 1:
				this.Tag8 = this.Tag7;
				this.Tag7 = this.Tag6;
				this.Tag6 = this.Tag5;
				this.Tag5 = this.Tag4;
				this.Tag4 = this.Tag3;
				this.Tag3 = this.Tag2;
				this.Tag2 = item;
				break;
			case 2:
				this.Tag8 = this.Tag7;
				this.Tag7 = this.Tag6;
				this.Tag6 = this.Tag5;
				this.Tag5 = this.Tag4;
				this.Tag4 = this.Tag3;
				this.Tag3 = item;
				break;
			case 3:
				this.Tag8 = this.Tag7;
				this.Tag7 = this.Tag6;
				this.Tag6 = this.Tag5;
				this.Tag5 = this.Tag4;
				this.Tag4 = item;
				break;
			case 4:
				this.Tag8 = this.Tag7;
				this.Tag7 = this.Tag6;
				this.Tag6 = this.Tag5;
				this.Tag5 = item;
				break;
			case 5:
				this.Tag8 = this.Tag7;
				this.Tag7 = this.Tag6;
				this.Tag6 = item;
				break;
			case 6:
				this.Tag8 = this.Tag7;
				this.Tag7 = item;
				break;
			default:
				return;
			}
			this._tagsCount++;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006EDC File Offset: 0x000050DC
		public void RemoveAt(int index)
		{
			if (index >= this._tagsCount)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this._overflowTags != null)
			{
				for (int i = index; i < this._tagsCount - 1; i++)
				{
					this._overflowTags[i] = this._overflowTags[i + 1];
				}
				this._tagsCount--;
				return;
			}
			switch (index)
			{
			case 0:
				this.Tag1 = this.Tag2;
				break;
			case 1:
				break;
			case 2:
				goto IL_0098;
			case 3:
				goto IL_00A4;
			case 4:
				goto IL_00B0;
			case 5:
				goto IL_00BC;
			case 6:
				goto IL_00C8;
			case 7:
				goto IL_00D4;
			default:
				goto IL_00D4;
			}
			this.Tag2 = this.Tag3;
			IL_0098:
			this.Tag3 = this.Tag4;
			IL_00A4:
			this.Tag4 = this.Tag5;
			IL_00B0:
			this.Tag5 = this.Tag6;
			IL_00BC:
			this.Tag6 = this.Tag7;
			IL_00C8:
			this.Tag7 = this.Tag8;
			IL_00D4:
			this._tagsCount--;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006FCB File Offset: 0x000051CB
		public void Clear()
		{
			this._tagsCount = 0;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006FD4 File Offset: 0x000051D4
		public readonly bool Contains([Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006FE4 File Offset: 0x000051E4
		public bool Remove([Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007007 File Offset: 0x00005207
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		public readonly IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return new TagList.Enumerator(in this);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007014 File Offset: 0x00005214
		readonly IEnumerator IEnumerable.GetEnumerator()
		{
			return new TagList.Enumerator(in this);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007024 File Offset: 0x00005224
		public readonly int IndexOf([Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> item)
		{
			if (this._overflowTags != null)
			{
				for (int i = 0; i < this._tagsCount; i++)
				{
					if (TagList.TagsEqual(this._overflowTags[i], item))
					{
						return i;
					}
				}
				return -1;
			}
			switch (this._tagsCount)
			{
			case 1:
				if (TagList.TagsEqual(this.Tag1, item))
				{
					return 0;
				}
				break;
			case 2:
				if (TagList.TagsEqual(this.Tag1, item))
				{
					return 0;
				}
				if (TagList.TagsEqual(this.Tag2, item))
				{
					return 1;
				}
				break;
			case 3:
				if (TagList.TagsEqual(this.Tag1, item))
				{
					return 0;
				}
				if (TagList.TagsEqual(this.Tag2, item))
				{
					return 1;
				}
				if (TagList.TagsEqual(this.Tag3, item))
				{
					return 2;
				}
				break;
			case 4:
				if (TagList.TagsEqual(this.Tag1, item))
				{
					return 0;
				}
				if (TagList.TagsEqual(this.Tag2, item))
				{
					return 1;
				}
				if (TagList.TagsEqual(this.Tag3, item))
				{
					return 2;
				}
				if (TagList.TagsEqual(this.Tag4, item))
				{
					return 3;
				}
				break;
			case 5:
				if (TagList.TagsEqual(this.Tag1, item))
				{
					return 0;
				}
				if (TagList.TagsEqual(this.Tag2, item))
				{
					return 1;
				}
				if (TagList.TagsEqual(this.Tag3, item))
				{
					return 2;
				}
				if (TagList.TagsEqual(this.Tag4, item))
				{
					return 3;
				}
				if (TagList.TagsEqual(this.Tag5, item))
				{
					return 4;
				}
				break;
			case 6:
				if (TagList.TagsEqual(this.Tag1, item))
				{
					return 0;
				}
				if (TagList.TagsEqual(this.Tag2, item))
				{
					return 1;
				}
				if (TagList.TagsEqual(this.Tag3, item))
				{
					return 2;
				}
				if (TagList.TagsEqual(this.Tag4, item))
				{
					return 3;
				}
				if (TagList.TagsEqual(this.Tag5, item))
				{
					return 4;
				}
				if (TagList.TagsEqual(this.Tag6, item))
				{
					return 5;
				}
				break;
			case 7:
				if (TagList.TagsEqual(this.Tag1, item))
				{
					return 0;
				}
				if (TagList.TagsEqual(this.Tag2, item))
				{
					return 1;
				}
				if (TagList.TagsEqual(this.Tag3, item))
				{
					return 2;
				}
				if (TagList.TagsEqual(this.Tag4, item))
				{
					return 3;
				}
				if (TagList.TagsEqual(this.Tag5, item))
				{
					return 4;
				}
				if (TagList.TagsEqual(this.Tag6, item))
				{
					return 5;
				}
				if (TagList.TagsEqual(this.Tag7, item))
				{
					return 6;
				}
				break;
			case 8:
				if (TagList.TagsEqual(this.Tag1, item))
				{
					return 0;
				}
				if (TagList.TagsEqual(this.Tag2, item))
				{
					return 1;
				}
				if (TagList.TagsEqual(this.Tag3, item))
				{
					return 2;
				}
				if (TagList.TagsEqual(this.Tag4, item))
				{
					return 3;
				}
				if (TagList.TagsEqual(this.Tag5, item))
				{
					return 4;
				}
				if (TagList.TagsEqual(this.Tag6, item))
				{
					return 5;
				}
				if (TagList.TagsEqual(this.Tag7, item))
				{
					return 6;
				}
				if (TagList.TagsEqual(this.Tag8, item))
				{
					return 7;
				}
				break;
			}
			return -1;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000072EC File Offset: 0x000054EC
		[Nullable(new byte[] { 2, 0, 1, 2 })]
		internal readonly KeyValuePair<string, object>[] Tags
		{
			get
			{
				return this._overflowTags;
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000072F4 File Offset: 0x000054F4
		private static bool TagsEqual(KeyValuePair<string, object> tag1, KeyValuePair<string, object> tag2)
		{
			if (tag1.Key != tag2.Key)
			{
				return false;
			}
			if (tag1.Value == null)
			{
				if (tag2.Value != null)
				{
					return false;
				}
			}
			else if (!tag1.Value.Equals(tag2.Value))
			{
				return false;
			}
			return true;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007344 File Offset: 0x00005544
		private void MoveTagsToTheArray()
		{
			this._overflowTags = new KeyValuePair<string, object>[16];
			this._overflowTags[0] = this.Tag1;
			this._overflowTags[1] = this.Tag2;
			this._overflowTags[2] = this.Tag3;
			this._overflowTags[3] = this.Tag4;
			this._overflowTags[4] = this.Tag5;
			this._overflowTags[5] = this.Tag6;
			this._overflowTags[6] = this.Tag7;
			this._overflowTags[7] = this.Tag8;
		}

		// Token: 0x0400009D RID: 157
		internal KeyValuePair<string, object> Tag1;

		// Token: 0x0400009E RID: 158
		internal KeyValuePair<string, object> Tag2;

		// Token: 0x0400009F RID: 159
		internal KeyValuePair<string, object> Tag3;

		// Token: 0x040000A0 RID: 160
		internal KeyValuePair<string, object> Tag4;

		// Token: 0x040000A1 RID: 161
		internal KeyValuePair<string, object> Tag5;

		// Token: 0x040000A2 RID: 162
		internal KeyValuePair<string, object> Tag6;

		// Token: 0x040000A3 RID: 163
		internal KeyValuePair<string, object> Tag7;

		// Token: 0x040000A4 RID: 164
		internal KeyValuePair<string, object> Tag8;

		// Token: 0x040000A5 RID: 165
		private int _tagsCount;

		// Token: 0x040000A6 RID: 166
		private KeyValuePair<string, object>[] _overflowTags;

		// Token: 0x040000A7 RID: 167
		private const int OverflowAdditionalCapacity = 8;

		// Token: 0x02000085 RID: 133
		public struct Enumerator : IEnumerator<KeyValuePair<string, object>>, IDisposable, IEnumerator
		{
			// Token: 0x06000357 RID: 855 RVA: 0x0000CD82 File Offset: 0x0000AF82
			internal Enumerator(in TagList tagList)
			{
				this._index = -1;
				this._tagList = tagList;
			}

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x06000358 RID: 856 RVA: 0x0000CD97 File Offset: 0x0000AF97
			[Nullable(new byte[] { 0, 1, 2 })]
			public KeyValuePair<string, object> Current
			{
				[return: Nullable(new byte[] { 0, 1, 2 })]
				get
				{
					return this._tagList[this._index];
				}
			}

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x06000359 RID: 857 RVA: 0x0000CDAA File Offset: 0x0000AFAA
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this._tagList[this._index];
				}
			}

			// Token: 0x0600035A RID: 858 RVA: 0x0000CDC2 File Offset: 0x0000AFC2
			public void Dispose()
			{
				this._index = this._tagList.Count;
			}

			// Token: 0x0600035B RID: 859 RVA: 0x0000CDD5 File Offset: 0x0000AFD5
			public bool MoveNext()
			{
				this._index++;
				return this._index < this._tagList.Count;
			}

			// Token: 0x0600035C RID: 860 RVA: 0x0000CDF8 File Offset: 0x0000AFF8
			public void Reset()
			{
				this._index = -1;
			}

			// Token: 0x0400019C RID: 412
			private TagList _tagList;

			// Token: 0x0400019D RID: 413
			private int _index;
		}
	}
}
