using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000691 RID: 1681
	[Serializable]
	internal sealed class TableRowList : ArrayList
	{
		// Token: 0x06005C39 RID: 23609 RVA: 0x0017952B File Offset: 0x0017772B
		internal TableRowList()
		{
		}

		// Token: 0x06005C3A RID: 23610 RVA: 0x00179533 File Offset: 0x00177733
		internal TableRowList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700206B RID: 8299
		internal TableRow this[int index]
		{
			get
			{
				return (TableRow)base[index];
			}
		}

		// Token: 0x06005C3C RID: 23612 RVA: 0x0017954C File Offset: 0x0017774C
		internal void Register(InitializationContext context)
		{
			for (int i = 0; i < this.Count; i++)
			{
				Global.Tracer.Assert(this[i] != null);
				context.RegisterReportItems(this[i].ReportItems);
			}
		}

		// Token: 0x06005C3D RID: 23613 RVA: 0x00179594 File Offset: 0x00177794
		internal void UnRegister(InitializationContext context)
		{
			for (int i = 0; i < this.Count; i++)
			{
				Global.Tracer.Assert(this[i] != null);
				context.UnRegisterReportItems(this[i].ReportItems);
			}
		}

		// Token: 0x06005C3E RID: 23614 RVA: 0x001795DC File Offset: 0x001777DC
		internal double GetHeightValue()
		{
			double num = 0.0;
			for (int i = 0; i < this.Count; i++)
			{
				if (!this[i].StartHidden)
				{
					num += this[i].HeightValue;
				}
			}
			return num;
		}
	}
}
