using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200082A RID: 2090
	public class PKGNAM
	{
		// Token: 0x0600426D RID: 17005 RVA: 0x000DEE0E File Offset: 0x000DD00E
		public PKGNAM(int encoding)
		{
			if (encoding > 0)
			{
				this._encoding = encoding;
			}
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x000DEE2C File Offset: 0x000DD02C
		public override string ToString()
		{
			return string.Format("PKGNAM[rdbnam={0};rdbcolid={1};pkgid={2}]", this._rdbnam, this._rdbcolid, this._pkgid);
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x000DEE4A File Offset: 0x000DD04A
		public void Reset()
		{
			this._rdbnam = null;
			this._rdbcolid = null;
			this._pkgid = null;
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06004270 RID: 17008 RVA: 0x000DEE61 File Offset: 0x000DD061
		// (set) Token: 0x06004271 RID: 17009 RVA: 0x000DEE69 File Offset: 0x000DD069
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

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06004272 RID: 17010 RVA: 0x000DEE72 File Offset: 0x000DD072
		// (set) Token: 0x06004273 RID: 17011 RVA: 0x000DEE7A File Offset: 0x000DD07A
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

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06004274 RID: 17012 RVA: 0x000DEE83 File Offset: 0x000DD083
		// (set) Token: 0x06004275 RID: 17013 RVA: 0x000DEE8B File Offset: 0x000DD08B
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

		// Token: 0x06004276 RID: 17014 RVA: 0x000DEE94 File Offset: 0x000DD094
		public void Read(DdmReader reader)
		{
			this.ReadAsync(reader, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x000DEEBC File Offset: 0x000DD0BC
		public async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			if (reader.DdmObjectLength == 54L)
			{
				string text = await reader.ReadStringAsync(18, this._encoding, isAsync, cancellationToken);
				this._rdbnam = text.Trim();
				text = await reader.ReadStringAsync(18, this._encoding, isAsync, cancellationToken);
				this._rdbcolid = text.Trim();
				text = await reader.ReadStringAsync(18, this._encoding, isAsync, cancellationToken);
				this._pkgid = text.Trim();
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
			}
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x000DEF19 File Offset: 0x000DD119
		public void Write(DdmWriter writer)
		{
			writer.WriteScalarPaddedString(this._rdbnam, 18);
			writer.WriteScalarPaddedString(this._rdbcolid, 18);
			writer.WriteScalarPaddedString(this._pkgid, 18);
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x000DEF48 File Offset: 0x000DD148
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
			return num;
		}

		// Token: 0x0600427A RID: 17018 RVA: 0x000DEF9C File Offset: 0x000DD19C
		public override bool Equals(object obj)
		{
			if (obj is PKGNAM)
			{
				PKGNAM pkgnam = (PKGNAM)obj;
				return string.Equals(this._rdbnam, pkgnam.RDBNAM) && string.Equals(this._rdbcolid, pkgnam.Rdbcolid) && string.Equals(this._pkgid, pkgnam._pkgid);
			}
			return false;
		}

		// Token: 0x04002E85 RID: 11909
		private string _rdbnam;

		// Token: 0x04002E86 RID: 11910
		private string _rdbcolid;

		// Token: 0x04002E87 RID: 11911
		private string _pkgid;

		// Token: 0x04002E88 RID: 11912
		private int _encoding = 1208;
	}
}
