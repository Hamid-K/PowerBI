using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Azure.Core
{
	// Token: 0x0200004F RID: 79
	[NullableContext(1)]
	[Nullable(0)]
	internal class MultipartBoundary
	{
		// Token: 0x0600025A RID: 602 RVA: 0x00007618 File Offset: 0x00005818
		public MultipartBoundary(string boundary, bool expectLeadingCrlf = true)
		{
			if (boundary == null)
			{
				throw new ArgumentNullException("boundary");
			}
			this._boundary = boundary;
			this._expectLeadingCrlf = expectLeadingCrlf;
			this.Initialize(this._boundary, this._expectLeadingCrlf);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000766C File Offset: 0x0000586C
		private void Initialize(string boundary, bool expectLeadingCrlf)
		{
			if (expectLeadingCrlf)
			{
				this.BoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary);
			}
			else
			{
				this.BoundaryBytes = Encoding.UTF8.GetBytes("--" + boundary);
			}
			this.FinalBoundaryLength = this.BoundaryBytes.Length + 2;
			int num = this.BoundaryBytes.Length;
			for (int i = 0; i < this._skipTable.Length; i++)
			{
				this._skipTable[i] = num;
			}
			for (int j = 0; j < num; j++)
			{
				this._skipTable[(int)this.BoundaryBytes[j]] = Math.Max(1, num - 1 - j);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000770F File Offset: 0x0000590F
		public int GetSkipValue(byte input)
		{
			return this._skipTable[(int)input];
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00007719 File Offset: 0x00005919
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00007721 File Offset: 0x00005921
		public bool ExpectLeadingCrlf
		{
			get
			{
				return this._expectLeadingCrlf;
			}
			set
			{
				if (value != this._expectLeadingCrlf)
				{
					this._expectLeadingCrlf = value;
					this.Initialize(this._boundary, this._expectLeadingCrlf);
				}
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00007745 File Offset: 0x00005945
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000774D File Offset: 0x0000594D
		public byte[] BoundaryBytes { get; private set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00007756 File Offset: 0x00005956
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000775E File Offset: 0x0000595E
		public int FinalBoundaryLength { get; private set; }

		// Token: 0x04000106 RID: 262
		private readonly int[] _skipTable = new int[256];

		// Token: 0x04000107 RID: 263
		private readonly string _boundary;

		// Token: 0x04000108 RID: 264
		private bool _expectLeadingCrlf;
	}
}
