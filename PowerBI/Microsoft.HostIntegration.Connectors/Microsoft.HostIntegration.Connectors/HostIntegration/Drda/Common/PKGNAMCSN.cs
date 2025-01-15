using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000828 RID: 2088
	public class PKGNAMCSN
	{
		// Token: 0x06004256 RID: 16982 RVA: 0x000DE286 File Offset: 0x000DC486
		public PKGNAMCSN(int encoding)
		{
			if (encoding > 0)
			{
				this._encoding = encoding;
			}
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x000DE2A4 File Offset: 0x000DC4A4
		public override string ToString()
		{
			return string.Format("PKGNAMCSN[rdbnam={0};rdbcolid={1};pkgid={2};cnstkn={3};pkgsn={4}]", new object[] { this._rdbnam, this._rdbcolid, this._pkgid, this._pkgcnstkn, this._pkgsn });
		}

		// Token: 0x06004258 RID: 16984 RVA: 0x000DE2F3 File Offset: 0x000DC4F3
		public void Reset()
		{
			this._rdbnam = null;
			this._rdbcolid = null;
			this._pkgid = null;
			this._pkgcnstkn.Bytes = null;
			this._pkgsn = 0;
			this._pkgcnstknstring = null;
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06004259 RID: 16985 RVA: 0x000DE324 File Offset: 0x000DC524
		// (set) Token: 0x0600425A RID: 16986 RVA: 0x000DE32C File Offset: 0x000DC52C
		public string RDBNAM
		{
			get
			{
				return this._rdbnam;
			}
			set
			{
				this._rdbnam = value;
			}
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x0600425B RID: 16987 RVA: 0x000DE335 File Offset: 0x000DC535
		// (set) Token: 0x0600425C RID: 16988 RVA: 0x000DE33D File Offset: 0x000DC53D
		public string Rdbcolid
		{
			get
			{
				return this._rdbcolid;
			}
			set
			{
				this._rdbcolid = value;
			}
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x0600425D RID: 16989 RVA: 0x000DE346 File Offset: 0x000DC546
		// (set) Token: 0x0600425E RID: 16990 RVA: 0x000DE34E File Offset: 0x000DC54E
		public string Pkgid
		{
			get
			{
				return this._pkgid;
			}
			set
			{
				this._pkgid = value;
			}
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x0600425F RID: 16991 RVA: 0x000DE357 File Offset: 0x000DC557
		// (set) Token: 0x06004260 RID: 16992 RVA: 0x000DE35F File Offset: 0x000DC55F
		public ConsistencyToken Pkgcnstkn
		{
			get
			{
				return this._pkgcnstkn;
			}
			set
			{
				this._pkgcnstkn = value;
			}
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06004261 RID: 16993 RVA: 0x000DE368 File Offset: 0x000DC568
		// (set) Token: 0x06004262 RID: 16994 RVA: 0x000DE370 File Offset: 0x000DC570
		public int Pkgsn
		{
			get
			{
				return this._pkgsn;
			}
			set
			{
				this._pkgsn = value;
			}
		}

		// Token: 0x06004263 RID: 16995 RVA: 0x000DE37C File Offset: 0x000DC57C
		public void Read(DdmReader reader)
		{
			this.ReadAsync(reader, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x000DE3A4 File Offset: 0x000DC5A4
		public async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			if (reader.DdmObjectLength == 64L)
			{
				string text = await reader.ReadStringAsync(18, this._encoding, isAsync, cancellationToken);
				this._rdbnam = text.Trim();
				text = await reader.ReadStringAsync(18, this._encoding, isAsync, cancellationToken);
				this._rdbcolid = text.Trim();
				text = await reader.ReadStringAsync(18, this._encoding, isAsync, cancellationToken);
				this._pkgid = text.Trim();
				this._pkgcnstkn = new ConsistencyToken(await reader.ReadBytesAsync(8, isAsync, cancellationToken));
				this._pkgcnstknstring = BitConverter.ToString(this._pkgcnstkn.Bytes).Replace("-", "");
				this._pkgsn = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
			}
			else
			{
				int num = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
				if (num < 18 || num > 255)
				{
					DrdaException.BadObjectLength(CodePoint.RDBNAM);
				}
				string text = await reader.ReadStringAsync(18, this._encoding, isAsync, cancellationToken);
				this._rdbnam = text.Trim();
				text = await reader.ReadStringAsync((int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken)), this._encoding, isAsync, cancellationToken);
				this._rdbcolid = text.Trim();
				text = await reader.ReadStringAsync((int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken)), this._encoding, isAsync, cancellationToken);
				this._pkgid = text.Trim();
				num = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
				this._pkgcnstkn = new ConsistencyToken(await reader.ReadBytesAsync(8, isAsync, cancellationToken));
				this._pkgcnstknstring = BitConverter.ToString(this._pkgcnstkn.Bytes).Replace("-", "");
				this._pkgsn = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
			}
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x000DE404 File Offset: 0x000DC604
		public void Write(DdmWriter writer)
		{
			writer.WriteScalarPaddedString(this._rdbnam, 18, this._encoding);
			writer.WriteScalarPaddedString(this._rdbcolid, 18, this._encoding);
			writer.WriteScalarPaddedString(this._pkgid, 18, this._encoding);
			writer.WriteScalarPaddedBytes(this._pkgcnstkn.Bytes, 8, 64);
			writer.WriteInt16(this._pkgsn, EndianType.BigEndian);
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x000DE470 File Offset: 0x000DC670
		public override int GetHashCode()
		{
			int num = 0;
			if (this._rdbnam != null)
			{
				num ^= this._rdbnam.GetHashCode();
			}
			if (this._rdbcolid != null)
			{
				num ^= this._rdbcolid.GetHashCode();
			}
			if (this._pkgid != null)
			{
				num ^= this._pkgid.GetHashCode();
			}
			if (this._pkgcnstkn != null)
			{
				num ^= this.PkgConsistencyTokenString.GetHashCode();
			}
			return num ^ this._pkgsn.GetHashCode();
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x000DE4E8 File Offset: 0x000DC6E8
		public override bool Equals(object obj)
		{
			if (obj is PKGNAMCSN)
			{
				PKGNAMCSN pkgnamcsn = (PKGNAMCSN)obj;
				return string.Equals(this._rdbnam, pkgnamcsn.RDBNAM) && string.Equals(this._rdbcolid, pkgnamcsn.Rdbcolid) && string.Equals(this._pkgid, pkgnamcsn._pkgid) && string.Equals(this._pkgcnstknstring, pkgnamcsn._pkgcnstknstring) && this._pkgsn == pkgnamcsn._pkgsn;
			}
			return false;
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06004268 RID: 17000 RVA: 0x000DE56B File Offset: 0x000DC76B
		public string PkgConsistencyTokenString
		{
			get
			{
				return Convert.ToBase64String(this._pkgcnstkn.Bytes).Replace("-", "");
			}
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x000DE58C File Offset: 0x000DC78C
		public string GetStoredProcName(string nameSeparator)
		{
			return string.Concat(new string[]
			{
				this._rdbcolid,
				".",
				this._pkgid,
				nameSeparator,
				this._pkgcnstknstring,
				nameSeparator,
				this._pkgsn.ToString()
			});
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x000DE5DD File Offset: 0x000DC7DD
		public string ToDB2PackageNotFoundErrorString()
		{
			return string.Format("PACKAGE OR DBRM NOT FOUND: {0}.{1}.{2}.{3}", new object[] { this._rdbnam, this._rdbcolid, this._pkgid, this._pkgsn }).ToUpper();
		}

		// Token: 0x04002E75 RID: 11893
		private string _rdbnam;

		// Token: 0x04002E76 RID: 11894
		private string _rdbcolid;

		// Token: 0x04002E77 RID: 11895
		private string _pkgid;

		// Token: 0x04002E78 RID: 11896
		private ConsistencyToken _pkgcnstkn;

		// Token: 0x04002E79 RID: 11897
		private int _pkgsn;

		// Token: 0x04002E7A RID: 11898
		private string _pkgcnstknstring;

		// Token: 0x04002E7B RID: 11899
		private int _encoding = 1208;
	}
}
