using System;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x02000897 RID: 2199
	public class GETNXTCHK : AbstractDdmObject
	{
		// Token: 0x060045F4 RID: 17908 RVA: 0x000F322E File Offset: 0x000F142E
		public GETNXTCHK(long qryinsid, int frerefopt, byte[] getnxtref, long getnxtlen, int refrst, PKGNAMCSN pkgnamcsn)
		{
			this.Qryinsid = qryinsid;
			this.Frerefopt = frerefopt;
			this.Getnxtref = getnxtref;
			this.Getnxtlen = getnxtlen;
			this.Refrst = refrst;
			this.Pkgnamcsn = pkgnamcsn;
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x060045F5 RID: 17909 RVA: 0x000F3263 File Offset: 0x000F1463
		// (set) Token: 0x060045F6 RID: 17910 RVA: 0x000F326B File Offset: 0x000F146B
		public PKGNAMCSN Pkgnamcsn
		{
			get
			{
				return this._pkgnamcsn;
			}
			set
			{
				this._pkgnamcsn = value;
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x000F3274 File Offset: 0x000F1474
		// (set) Token: 0x060045F8 RID: 17912 RVA: 0x000F327C File Offset: 0x000F147C
		public long Qryinsid
		{
			get
			{
				return this._qryinsid;
			}
			set
			{
				this._qryinsid = value;
			}
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x060045F9 RID: 17913 RVA: 0x000F3285 File Offset: 0x000F1485
		// (set) Token: 0x060045FA RID: 17914 RVA: 0x000F328D File Offset: 0x000F148D
		public int Frerefopt
		{
			get
			{
				return this._frerefopt;
			}
			set
			{
				this._frerefopt = value;
			}
		}

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x060045FB RID: 17915 RVA: 0x000F3296 File Offset: 0x000F1496
		// (set) Token: 0x060045FC RID: 17916 RVA: 0x000F329E File Offset: 0x000F149E
		public byte[] Getnxtref
		{
			get
			{
				return this._getnxtref;
			}
			set
			{
				this._getnxtref = value;
			}
		}

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x060045FD RID: 17917 RVA: 0x000F32A7 File Offset: 0x000F14A7
		// (set) Token: 0x060045FE RID: 17918 RVA: 0x000F32AF File Offset: 0x000F14AF
		public long Getnxtlen
		{
			get
			{
				return this._getnxtlen;
			}
			set
			{
				this._getnxtlen = value;
			}
		}

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x060045FF RID: 17919 RVA: 0x000F32B8 File Offset: 0x000F14B8
		// (set) Token: 0x06004600 RID: 17920 RVA: 0x000F32C0 File Offset: 0x000F14C0
		public int Refrst
		{
			get
			{
				return this._refrst;
			}
			set
			{
				this._refrst = value;
			}
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x000F32CC File Offset: 0x000F14CC
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.GETNXTCHK);
			writer.WriteBeginDdm(CodePoint.PKGNAMCSN);
			this.Pkgnamcsn.Write(writer);
			writer.WriteEndDdm();
			writer.WriteBeginDdm(CodePoint.QRYINSID);
			writer.WriteInt64(this.Qryinsid, EndianType.BigEndian);
			writer.WriteEndDdm();
			writer.WriteBeginDdm(CodePoint.FREREFOPT);
			writer.WriteByte(this.Frerefopt);
			writer.WriteEndDdm();
			writer.WriteBeginDdm(CodePoint.GETNXTREF);
			writer.WriteBytes(this.Getnxtref);
			writer.WriteEndDdm();
			writer.WriteBeginDdm(CodePoint.GETNXTLEN);
			writer.WriteInt64(this.Getnxtlen, EndianType.BigEndian);
			writer.WriteEndDdm();
			writer.WriteBeginDdm(CodePoint.REFRST);
			writer.WriteByte(this.Refrst);
			writer.WriteEndDdm();
			writer.WriteEndDdm();
		}

		// Token: 0x040031D2 RID: 12754
		private PKGNAMCSN _pkgnamcsn;

		// Token: 0x040031D3 RID: 12755
		private long _qryinsid;

		// Token: 0x040031D4 RID: 12756
		private int _frerefopt;

		// Token: 0x040031D5 RID: 12757
		private byte[] _getnxtref;

		// Token: 0x040031D6 RID: 12758
		private long _getnxtlen;

		// Token: 0x040031D7 RID: 12759
		private int _refrst;
	}
}
