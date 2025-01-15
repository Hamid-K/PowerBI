using System;
using System.Text;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x0200089D RID: 2205
	public class QRYNOPRM : AbstractDdmObject
	{
		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x0600463A RID: 17978 RVA: 0x000F501A File Offset: 0x000F321A
		// (set) Token: 0x0600463B RID: 17979 RVA: 0x000F5022 File Offset: 0x000F3222
		public SeverityCode SeverityCode
		{
			get
			{
				return this._severityCode;
			}
			set
			{
				this._severityCode = value;
			}
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x0600463C RID: 17980 RVA: 0x000F502B File Offset: 0x000F322B
		// (set) Token: 0x0600463D RID: 17981 RVA: 0x000F5033 File Offset: 0x000F3233
		public RDBNAM Rdbnam
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

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x0600463E RID: 17982 RVA: 0x000F503C File Offset: 0x000F323C
		// (set) Token: 0x0600463F RID: 17983 RVA: 0x000F5044 File Offset: 0x000F3244
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

		// Token: 0x06004640 RID: 17984 RVA: 0x000F5050 File Offset: 0x000F3250
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.QRYNOPRM);
			writer.WriteScalar2Bytes(CodePoint.SVRCOD, 8);
			writer.WriteScalarPaddedBytes(CodePoint.RDBNAM, Encoding.GetEncoding(500).GetBytes(this._rdbnam.Name), 18, 64);
			writer.WriteEndDdm();
		}

		// Token: 0x0400321A RID: 12826
		private SeverityCode _severityCode;

		// Token: 0x0400321B RID: 12827
		private RDBNAM _rdbnam;

		// Token: 0x0400321C RID: 12828
		private PKGNAMCSN _pkgnamcsn;
	}
}
