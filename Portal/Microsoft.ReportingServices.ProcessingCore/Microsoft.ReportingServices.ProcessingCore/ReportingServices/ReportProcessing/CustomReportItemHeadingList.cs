using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000758 RID: 1880
	[Serializable]
	internal sealed class CustomReportItemHeadingList : TablixHeadingList
	{
		// Token: 0x06006836 RID: 26678 RVA: 0x00195B45 File Offset: 0x00193D45
		internal CustomReportItemHeadingList()
		{
		}

		// Token: 0x06006837 RID: 26679 RVA: 0x00195B4D File Offset: 0x00193D4D
		internal CustomReportItemHeadingList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170024D5 RID: 9429
		internal CustomReportItemHeading this[int index]
		{
			get
			{
				return (CustomReportItemHeading)base[index];
			}
		}

		// Token: 0x06006839 RID: 26681 RVA: 0x00195B64 File Offset: 0x00193D64
		internal int Initialize(int level, DataCellsList dataRowCells, ref int currentIndex, ref int maxLevel, InitializationContext context)
		{
			int num = this.Count;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				Global.Tracer.Assert(this[i] != null);
				if (this[i].Initialize(level, this, i, dataRowCells, ref currentIndex, ref maxLevel, context))
				{
					num++;
					num2 += this[i].HeadingSpan;
				}
			}
			return num2;
		}

		// Token: 0x0600683A RID: 26682 RVA: 0x00195BC8 File Offset: 0x00193DC8
		internal void TransferHeadingAggregates()
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				Global.Tracer.Assert(this[i] != null);
				this[i].TransferHeadingAggregates();
			}
		}

		// Token: 0x0600683B RID: 26683 RVA: 0x00195C08 File Offset: 0x00193E08
		internal override TablixHeadingList InnerHeadings()
		{
			if (this.Count > 0)
			{
				return this[0].InnerHeadings;
			}
			return null;
		}
	}
}
