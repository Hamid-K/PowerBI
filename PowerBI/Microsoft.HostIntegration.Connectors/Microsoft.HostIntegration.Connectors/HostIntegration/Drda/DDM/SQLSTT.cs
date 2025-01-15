using System;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008DD RID: 2269
	public class SQLSTT : AbstractDdmObject
	{
		// Token: 0x060047E0 RID: 18400 RVA: 0x00104D3C File Offset: 0x00102F3C
		public SQLSTT(IDatabase database, int sqlamLevel, string accrdbTypedefname)
			: base(database, sqlamLevel, accrdbTypedefname)
		{
		}

		// Token: 0x060047E1 RID: 18401 RVA: 0x00104D52 File Offset: 0x00102F52
		public override string ToString()
		{
			return base.FixParenthis(string.Format("SQLSTT[sqlstt={0};cmdsrcid={1};]", this._cmdsrcid, this._sqlstt.Trim()));
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x00104D7A File Offset: 0x00102F7A
		public override void Reset()
		{
			base.Reset();
			this._sqlstt = null;
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x060047E3 RID: 18403 RVA: 0x00104D89 File Offset: 0x00102F89
		// (set) Token: 0x060047E4 RID: 18404 RVA: 0x00104D91 File Offset: 0x00102F91
		public string Sqlstt
		{
			get
			{
				return this._sqlstt;
			}
			set
			{
				this._sqlstt = value;
			}
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x00104D9A File Offset: 0x00102F9A
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.SQLSTT);
			base.WriteNOCMorNOCS(writer, this._sqlstt);
			writer.WriteEndDdm();
		}

		// Token: 0x0400347C RID: 13436
		private string _sqlstt = string.Empty;
	}
}
