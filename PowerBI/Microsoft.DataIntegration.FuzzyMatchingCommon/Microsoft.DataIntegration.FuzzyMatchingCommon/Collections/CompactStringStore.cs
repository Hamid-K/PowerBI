using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200006B RID: 107
	[Serializable]
	public class CompactStringStore
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x0001BC33 File Offset: 0x00019E33
		public void BeginUpdate()
		{
			this.updateMemoryStream = new MemoryStream();
			if (this.m_utf8DataStore != null)
			{
				this.updateMemoryStream.Write(this.m_utf8DataStore, 0, this.m_utf8DataStore.Length);
				this.m_utf8DataStore = null;
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001BC6C File Offset: 0x00019E6C
		public void EndUpdate()
		{
			if (this.updateMemoryStream == null)
			{
				throw new InvalidOperationException("Can not call end update if begin update has not been called first");
			}
			this.m_utf8DataStore = this.updateMemoryStream.ToArray();
			this.updateMemoryStream.Dispose();
			this.updateMemoryStream = null;
			this.m_ridToOffset.Capacity = this.m_ridToOffset.Count;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001BCC8 File Offset: 0x00019EC8
		public bool TryGetString(int rid, out string str)
		{
			str = null;
			if (rid < 1 || rid > this.m_ridToOffset.Count)
			{
				return false;
			}
			int num = this.m_ridToOffset[rid - 1];
			if (num != 2147483647)
			{
				int num2 = (int)StreamUtilities.ReadVariableLengthInt(this.m_utf8DataStore, ref num);
				if (num2 == 0)
				{
					str = "";
				}
				else
				{
					str = this.m_utf8Codec.GetString(this.m_utf8DataStore, num, num2);
				}
			}
			return true;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001BD34 File Offset: 0x00019F34
		public bool TryGetString(int rid, ref ArraySegmentBuilder<char> str)
		{
			str.Reset();
			if (rid < 1 || rid > this.m_ridToOffset.Count)
			{
				return false;
			}
			int num = this.m_ridToOffset[rid - 1];
			if (num != 2147483647)
			{
				int num2 = (int)StreamUtilities.ReadVariableLengthInt(this.m_utf8DataStore, ref num);
				if (num2 > 0)
				{
					int charCount = this.m_utf8Codec.GetCharCount(this.m_utf8DataStore, num, num2);
					str.Capacity = charCount;
					this.m_utf8Codec.GetChars(this.m_utf8DataStore, num, num2, str.Array, 0);
					str.Count = charCount;
				}
			}
			return true;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001BDC8 File Offset: 0x00019FC8
		public int AddString(ArraySegment<char> str)
		{
			if (this.updateMemoryStream == null)
			{
				throw new InvalidOperationException("Must call begin update before adding a string");
			}
			this.m_ridToOffset.Add((int)this.updateMemoryStream.Position);
			if (str.Count == 0)
			{
				StreamUtilities.WriteVariableLengthInt(0UL, this.updateMemoryStream);
			}
			else
			{
				long byteCount = (long)this.m_utf8Codec.GetByteCount(str.Array, str.Offset, str.Count);
				byte[] bytes = this.m_utf8Codec.GetBytes(str.Array, str.Offset, str.Count);
				StreamUtilities.WriteVariableLengthInt((ulong)byteCount, this.updateMemoryStream);
				this.updateMemoryStream.Write(bytes, 0, bytes.Length);
			}
			return this.m_ridToOffset.Count;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001BE80 File Offset: 0x0001A080
		public int AddString(string str)
		{
			if (this.updateMemoryStream == null)
			{
				throw new InvalidOperationException("Must call begin update before adding a string");
			}
			if (str == null)
			{
				this.m_ridToOffset.Add(int.MaxValue);
			}
			else
			{
				this.m_ridToOffset.Add((int)this.updateMemoryStream.Position);
				if (str.Length == 0)
				{
					StreamUtilities.WriteVariableLengthInt(0UL, this.updateMemoryStream);
				}
				else
				{
					ulong num = (ulong)((long)this.m_utf8Codec.GetByteCount(str));
					byte[] bytes = this.m_utf8Codec.GetBytes(str);
					StreamUtilities.WriteVariableLengthInt(num, this.updateMemoryStream);
					this.updateMemoryStream.Write(bytes, 0, bytes.Length);
				}
			}
			return this.m_ridToOffset.Count;
		}

		// Token: 0x040000BF RID: 191
		private byte[] m_utf8DataStore;

		// Token: 0x040000C0 RID: 192
		private List<int> m_ridToOffset = new List<int>();

		// Token: 0x040000C1 RID: 193
		private UTF8Encoding m_utf8Codec = new UTF8Encoding();

		// Token: 0x040000C2 RID: 194
		[NonSerialized]
		private MemoryStream updateMemoryStream;
	}
}
