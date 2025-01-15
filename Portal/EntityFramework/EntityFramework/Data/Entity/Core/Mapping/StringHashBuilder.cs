using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200055C RID: 1372
	internal class StringHashBuilder
	{
		// Token: 0x0600430B RID: 17163 RVA: 0x000E6C10 File Offset: 0x000E4E10
		internal StringHashBuilder(HashAlgorithm hashAlgorithm)
		{
			this._hashAlgorithm = hashAlgorithm;
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x000E6C2A File Offset: 0x000E4E2A
		internal StringHashBuilder(HashAlgorithm hashAlgorithm, int startingBufferSize)
			: this(hashAlgorithm)
		{
			this._cachedBuffer = new byte[startingBufferSize];
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x0600430D RID: 17165 RVA: 0x000E6C3F File Offset: 0x000E4E3F
		internal int CharCount
		{
			get
			{
				return this._totalLength;
			}
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x000E6C47 File Offset: 0x000E4E47
		internal virtual void Append(string s)
		{
			this.InternalAppend(s);
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x000E6C50 File Offset: 0x000E4E50
		internal virtual void AppendLine(string s)
		{
			this.InternalAppend(s);
			this.InternalAppend("\n");
		}

		// Token: 0x06004310 RID: 17168 RVA: 0x000E6C64 File Offset: 0x000E4E64
		private void InternalAppend(string s)
		{
			if (s.Length == 0)
			{
				return;
			}
			this._strings.Add(s);
			this._totalLength += s.Length;
		}

		// Token: 0x06004311 RID: 17169 RVA: 0x000E6C90 File Offset: 0x000E4E90
		internal string ComputeHash()
		{
			int byteCount = this.GetByteCount();
			if (this._cachedBuffer == null)
			{
				this._cachedBuffer = new byte[byteCount];
			}
			else if (this._cachedBuffer.Length < byteCount)
			{
				int num = Math.Max(this._cachedBuffer.Length + this._cachedBuffer.Length / 2, byteCount);
				this._cachedBuffer = new byte[num];
			}
			int num2 = 0;
			foreach (string text in this._strings)
			{
				num2 += Encoding.Unicode.GetBytes(text, 0, text.Length, this._cachedBuffer, num2);
			}
			return StringHashBuilder.ConvertHashToString(this._hashAlgorithm.ComputeHash(this._cachedBuffer, 0, byteCount));
		}

		// Token: 0x06004312 RID: 17170 RVA: 0x000E6D64 File Offset: 0x000E4F64
		internal void Clear()
		{
			this._strings.Clear();
			this._totalLength = 0;
		}

		// Token: 0x06004313 RID: 17171 RVA: 0x000E6D78 File Offset: 0x000E4F78
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			this._strings.Each((string s) => builder.Append(s));
			return builder.ToString();
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x000E6DB8 File Offset: 0x000E4FB8
		private int GetByteCount()
		{
			int num = 0;
			foreach (string text in this._strings)
			{
				num += Encoding.Unicode.GetByteCount(text);
			}
			return num;
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x000E6E18 File Offset: 0x000E5018
		private static string ConvertHashToString(byte[] hash)
		{
			StringBuilder stringBuilder = new StringBuilder(hash.Length * 2);
			for (int i = 0; i < hash.Length; i++)
			{
				stringBuilder.Append(hash[i].ToString("x2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004316 RID: 17174 RVA: 0x000E6E61 File Offset: 0x000E5061
		public static string ComputeHash(HashAlgorithm hashAlgorithm, string source)
		{
			StringHashBuilder stringHashBuilder = new StringHashBuilder(hashAlgorithm);
			stringHashBuilder.Append(source);
			return stringHashBuilder.ComputeHash();
		}

		// Token: 0x040017EF RID: 6127
		private readonly HashAlgorithm _hashAlgorithm;

		// Token: 0x040017F0 RID: 6128
		private const string NewLine = "\n";

		// Token: 0x040017F1 RID: 6129
		private readonly List<string> _strings = new List<string>();

		// Token: 0x040017F2 RID: 6130
		private int _totalLength;

		// Token: 0x040017F3 RID: 6131
		private byte[] _cachedBuffer;
	}
}
