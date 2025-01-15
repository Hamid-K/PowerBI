using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000124 RID: 292
	public struct TokenRange
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00007A0B File Offset: 0x00005C0B
		public static TokenRange Null
		{
			get
			{
				return new TokenRange(TokenReference.Null, TokenReference.Null);
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00007A14 File Offset: 0x00005C14
		public TokenRange(TokenReference token)
		{
			this.start = token;
			this.end = token;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00007A24 File Offset: 0x00005C24
		public TokenRange(TokenReference start, TokenReference end)
		{
			this.start = start;
			this.end = end;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00007A34 File Offset: 0x00005C34
		public TokenRange(ISyntaxNode node)
		{
			this = new TokenRange(node.Range.Start, node.Range.End);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00007A64 File Offset: 0x00005C64
		public TokenRange(TokenReference start, ISyntaxNode end)
		{
			this = new TokenRange(start, end.Range.End);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00007A88 File Offset: 0x00005C88
		public TokenRange(ISyntaxNode start, TokenReference end)
		{
			this = new TokenRange(start.Range.Start, end);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00007AAC File Offset: 0x00005CAC
		public TokenRange(ISyntaxNode start, ISyntaxNode end)
		{
			this = new TokenRange(start.Range.Start, end.Range.End);
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00007ADB File Offset: 0x00005CDB
		public TokenReference Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00007AE3 File Offset: 0x00005CE3
		public TokenReference End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00007AEB File Offset: 0x00005CEB
		public bool IsNull
		{
			get
			{
				return this.start == TokenReference.Null;
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00007AF6 File Offset: 0x00005CF6
		public bool Equals(TokenRange other)
		{
			return this == other;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00007B04 File Offset: 0x00005D04
		public override bool Equals(object obj)
		{
			return obj is TokenRange && this.Equals((TokenRange)obj);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00007B1C File Offset: 0x00005D1C
		public override int GetHashCode()
		{
			return (int)(((int)this.start << 16) + (int)this.end);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00007B2E File Offset: 0x00005D2E
		public static bool operator ==(TokenRange left, TokenRange right)
		{
			return left.start == right.start && left.end == right.end;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00007B4E File Offset: 0x00005D4E
		public static bool operator !=(TokenRange left, TokenRange right)
		{
			return !(left == right);
		}

		// Token: 0x040002D2 RID: 722
		private readonly TokenReference start;

		// Token: 0x040002D3 RID: 723
		private readonly TokenReference end;

		// Token: 0x02000125 RID: 293
		public sealed class EqualityComparer : IEqualityComparer<TokenRange>
		{
			// Token: 0x06000515 RID: 1301 RVA: 0x00007B5A File Offset: 0x00005D5A
			public int GetHashCode(TokenRange tokenRange)
			{
				return tokenRange.GetHashCode();
			}

			// Token: 0x06000516 RID: 1302 RVA: 0x00007B69 File Offset: 0x00005D69
			public bool Equals(TokenRange left, TokenRange right)
			{
				return left.Equals(right);
			}

			// Token: 0x040002D4 RID: 724
			public static readonly IEqualityComparer<TokenRange> Instance = new TokenRange.EqualityComparer();
		}
	}
}
