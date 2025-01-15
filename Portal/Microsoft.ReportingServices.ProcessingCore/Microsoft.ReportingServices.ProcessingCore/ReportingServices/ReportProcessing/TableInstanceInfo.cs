using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000741 RID: 1857
	[Serializable]
	internal sealed class TableInstanceInfo : ReportItemInstanceInfo
	{
		// Token: 0x06006728 RID: 26408 RVA: 0x00193248 File Offset: 0x00191448
		internal TableInstanceInfo(ReportProcessing.ProcessingContext pc, Table reportItemDef, TableInstance owner)
			: base(pc, reportItemDef, owner, true)
		{
			this.m_columnInstances = new TableColumnInstance[reportItemDef.TableColumns.Count];
			reportItemDef.ColumnsStartHidden = new bool[reportItemDef.TableColumns.Count];
			for (int i = 0; i < reportItemDef.TableColumns.Count; i++)
			{
				this.m_columnInstances[i] = new TableColumnInstance(pc, reportItemDef.TableColumns[i], reportItemDef);
				reportItemDef.ColumnsStartHidden[i] = this.m_columnInstances[i].StartHidden;
			}
			this.m_noRows = pc.ReportRuntime.EvaluateDataRegionNoRowsExpression(reportItemDef, reportItemDef.ObjectType, reportItemDef.Name, "NoRows");
		}

		// Token: 0x06006729 RID: 26409 RVA: 0x001932F4 File Offset: 0x001914F4
		internal TableInstanceInfo(Table reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x17002476 RID: 9334
		// (get) Token: 0x0600672A RID: 26410 RVA: 0x001932FD File Offset: 0x001914FD
		// (set) Token: 0x0600672B RID: 26411 RVA: 0x00193305 File Offset: 0x00191505
		internal TableColumnInstance[] ColumnInstances
		{
			get
			{
				return this.m_columnInstances;
			}
			set
			{
				this.m_columnInstances = value;
			}
		}

		// Token: 0x17002477 RID: 9335
		// (get) Token: 0x0600672C RID: 26412 RVA: 0x0019330E File Offset: 0x0019150E
		// (set) Token: 0x0600672D RID: 26413 RVA: 0x00193316 File Offset: 0x00191516
		internal string NoRows
		{
			get
			{
				return this.m_noRows;
			}
			set
			{
				this.m_noRows = value;
			}
		}

		// Token: 0x0600672E RID: 26414 RVA: 0x00193320 File Offset: 0x00191520
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.ColumnInstances, Token.Array, ObjectType.TableColumnInstance),
				new MemberInfo(MemberName.NoRows, Token.String)
			});
		}

		// Token: 0x04003337 RID: 13111
		private TableColumnInstance[] m_columnInstances;

		// Token: 0x04003338 RID: 13112
		private string m_noRows;
	}
}
