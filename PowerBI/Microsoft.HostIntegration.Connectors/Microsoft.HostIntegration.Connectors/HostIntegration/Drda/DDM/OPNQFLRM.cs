using System;
using System.Text;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x0200089E RID: 2206
	public class OPNQFLRM : AbstractDdmObject
	{
		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x06004642 RID: 17986 RVA: 0x000F50A3 File Offset: 0x000F32A3
		// (set) Token: 0x06004643 RID: 17987 RVA: 0x000F50AB File Offset: 0x000F32AB
		public SQLCARD Sqlcard
		{
			get
			{
				return this._sqlcard;
			}
			set
			{
				this._sqlcard = value;
			}
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x06004644 RID: 17988 RVA: 0x000F50B4 File Offset: 0x000F32B4
		// (set) Token: 0x06004645 RID: 17989 RVA: 0x000F50BC File Offset: 0x000F32BC
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

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x06004646 RID: 17990 RVA: 0x000F50C5 File Offset: 0x000F32C5
		// (set) Token: 0x06004647 RID: 17991 RVA: 0x000F50CD File Offset: 0x000F32CD
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

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x06004648 RID: 17992 RVA: 0x000F50D6 File Offset: 0x000F32D6
		// (set) Token: 0x06004649 RID: 17993 RVA: 0x000F50DE File Offset: 0x000F32DE
		public string ServerDiagnostic
		{
			get
			{
				return this._serverDiagnostic;
			}
			set
			{
				this._serverDiagnostic = value;
			}
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x000F50E8 File Offset: 0x000F32E8
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.OPNQFLRM);
			writer.WriteScalar2Bytes(CodePoint.SVRCOD, 8);
			writer.WriteScalarPaddedBytes(CodePoint.RDBNAM, Encoding.GetEncoding(500).GetBytes(this._rdbnam.Name), 18, 64);
			this._sqlcard.Write(writer);
			if (!string.IsNullOrWhiteSpace(this.ServerDiagnostic))
			{
				writer.WriteScalarString(CodePoint.SRVDGN, this.ServerDiagnostic, 1208);
			}
			writer.WriteEndDdm();
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x000F516A File Offset: 0x000F336A
		public override string ToString()
		{
			return string.Format("OPNQFLRM[rdbnam={0};svrcod={1};srvdgn={2}]", this._rdbnam, SeverityCode.Error, (this.ServerDiagnostic != null) ? this.ServerDiagnostic : " ");
		}

		// Token: 0x0400321D RID: 12829
		private SeverityCode _severityCode;

		// Token: 0x0400321E RID: 12830
		private RDBNAM _rdbnam;

		// Token: 0x0400321F RID: 12831
		private string _serverDiagnostic;

		// Token: 0x04003220 RID: 12832
		private SQLCARD _sqlcard;
	}
}
