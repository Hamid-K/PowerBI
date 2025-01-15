using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000352 RID: 850
	internal sealed class ShimTableRowCollection : TablixRowCollection
	{
		// Token: 0x060020A1 RID: 8353 RVA: 0x0007ECEC File Offset: 0x0007CEEC
		internal ShimTableRowCollection(Tablix owner)
			: base(owner)
		{
			this.m_rows = new List<TablixRow>();
			this.AppendTableRows(owner.RenderTable.TableHeader);
			if (owner.RenderTable.TableGroups != null)
			{
				this.AppendTableGroups(owner.RenderTable.TableGroups[0]);
			}
			else if (owner.RenderTable.DetailRows != null)
			{
				this.AppendTableRows(owner.RenderTable.DetailRows[0]);
			}
			this.AppendTableRows(owner.RenderTable.TableFooter);
		}

		// Token: 0x17001269 RID: 4713
		public override TablixRow this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_rows[index];
			}
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x0007EDCF File Offset: 0x0007CFCF
		public override int Count
		{
			get
			{
				return this.m_rows.Count;
			}
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x0007EDDC File Offset: 0x0007CFDC
		private void AppendTableGroups(Microsoft.ReportingServices.ReportRendering.TableGroup renderGroup)
		{
			if (renderGroup != null)
			{
				this.AppendTableRows(renderGroup.GroupHeader);
				if (renderGroup.SubGroups != null)
				{
					this.AppendTableGroups(renderGroup.SubGroups[0]);
				}
				else if (renderGroup.DetailRows != null)
				{
					this.AppendTableRows(renderGroup.DetailRows[0]);
				}
				this.AppendTableRows(renderGroup.GroupFooter);
			}
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x0007EE3C File Offset: 0x0007D03C
		private void AppendTableRows(TableRowCollection renderRows)
		{
			if (renderRows != null)
			{
				int count = renderRows.DetailRowDefinitions.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_rows.Add(new ShimTableRow(this.m_owner, this.m_rows.Count, renderRows[i]));
				}
			}
		}

		// Token: 0x04001068 RID: 4200
		private List<TablixRow> m_rows;
	}
}
