using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200082C RID: 2092
	public class PKGNAMCT
	{
		// Token: 0x0600427D RID: 17021 RVA: 0x000DF526 File Offset: 0x000DD726
		public PKGNAMCT(int encoding)
		{
			if (encoding > 0)
			{
				this._encoding = encoding;
			}
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x000DF544 File Offset: 0x000DD744
		public override string ToString()
		{
			return string.Format("PKGNAMCT[rdbnam={0};rdbcolid={1};pkgid={2};cnstkn={3}]", new object[] { this._rdbnam, this._rdbcolid, this._pkgid, this._pkgcnstkn });
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x000DF57A File Offset: 0x000DD77A
		public void Reset()
		{
			this._rdbnam = null;
			this._rdbcolid = null;
			this._pkgid = null;
			this._pkgcnstkn = null;
			this._pkgcnstknstring = null;
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06004280 RID: 17024 RVA: 0x000DF59F File Offset: 0x000DD79F
		// (set) Token: 0x06004281 RID: 17025 RVA: 0x000DF5A7 File Offset: 0x000DD7A7
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

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06004282 RID: 17026 RVA: 0x000DF5B0 File Offset: 0x000DD7B0
		// (set) Token: 0x06004283 RID: 17027 RVA: 0x000DF5B8 File Offset: 0x000DD7B8
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

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06004284 RID: 17028 RVA: 0x000DF5C1 File Offset: 0x000DD7C1
		// (set) Token: 0x06004285 RID: 17029 RVA: 0x000DF5C9 File Offset: 0x000DD7C9
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

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06004286 RID: 17030 RVA: 0x000DF5D2 File Offset: 0x000DD7D2
		// (set) Token: 0x06004287 RID: 17031 RVA: 0x000DF5DA File Offset: 0x000DD7DA
		public byte[] Pkgcnstkn
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

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06004288 RID: 17032 RVA: 0x000DF5E3 File Offset: 0x000DD7E3
		public string Pkgcnstknstring
		{
			get
			{
				return this._pkgcnstknstring;
			}
		}

		// Token: 0x06004289 RID: 17033 RVA: 0x000DF5EC File Offset: 0x000DD7EC
		public void Write(DdmWriter writer)
		{
			writer.WriteScalarPaddedString(this._rdbnam, 18, this._encoding);
			writer.WriteScalarPaddedString(this._rdbcolid, 18, this._encoding);
			writer.WriteScalarPaddedString(this._pkgid, 18, this._encoding);
			writer.WriteScalarPaddedBytes(this._pkgcnstkn, 8, (this._encoding == 1208) ? 32 : 64);
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x000DF658 File Offset: 0x000DD858
		public void Read(DdmReader reader)
		{
			this.ReadAsync(reader, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x000DF680 File Offset: 0x000DD880
		public async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			if (reader.DdmObjectLength == 62L)
			{
				string text = await reader.ReadStringAsync(18, this._encoding, isAsync, cancellationToken);
				this._rdbnam = text.Trim();
				text = await reader.ReadStringAsync(18, this._encoding, isAsync, cancellationToken);
				this._rdbcolid = text.Trim();
				text = await reader.ReadStringAsync(18, this._encoding, isAsync, cancellationToken);
				this._pkgid = text.Trim();
				this._pkgcnstkn = await reader.ReadBytesAsync(8, isAsync, cancellationToken);
				this._pkgcnstknstring = BitConverter.ToString(this._pkgcnstkn).Replace("-", "");
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
				this._pkgcnstkn = await reader.ReadBytesAsync(8, isAsync, cancellationToken);
				this._pkgcnstknstring = BitConverter.ToString(this._pkgcnstkn).Replace("-", "");
			}
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x000DF6E0 File Offset: 0x000DD8E0
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
				num ^= this._pkgcnstknstring.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x000DF748 File Offset: 0x000DD948
		public override bool Equals(object obj)
		{
			if (obj is PKGNAMCT)
			{
				PKGNAMCT pkgnamct = (PKGNAMCT)obj;
				return string.Equals(this._rdbnam, pkgnamct.RDBNAM) && string.Equals(this._rdbcolid, pkgnamct.Rdbcolid) && string.Equals(this._pkgid, pkgnamct._pkgid) && string.Equals(this._pkgcnstknstring, pkgnamct._pkgcnstknstring);
			}
			return false;
		}

		// Token: 0x0600428E RID: 17038 RVA: 0x000DF7BB File Offset: 0x000DD9BB
		public string GetStoredProcNamePrefix()
		{
			return this._rdbcolid + "." + this._pkgid;
		}

		// Token: 0x04002E91 RID: 11921
		private string _rdbnam;

		// Token: 0x04002E92 RID: 11922
		private string _rdbcolid;

		// Token: 0x04002E93 RID: 11923
		private string _pkgid;

		// Token: 0x04002E94 RID: 11924
		private byte[] _pkgcnstkn;

		// Token: 0x04002E95 RID: 11925
		private string _pkgcnstknstring;

		// Token: 0x04002E96 RID: 11926
		private int _encoding = 1208;
	}
}
