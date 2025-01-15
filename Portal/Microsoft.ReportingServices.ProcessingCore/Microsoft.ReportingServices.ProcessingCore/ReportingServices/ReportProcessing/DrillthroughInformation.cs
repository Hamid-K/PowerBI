using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000713 RID: 1811
	[Serializable]
	internal sealed class DrillthroughInformation
	{
		// Token: 0x06006517 RID: 25879 RVA: 0x0018EF86 File Offset: 0x0018D186
		internal DrillthroughInformation()
		{
		}

		// Token: 0x06006518 RID: 25880 RVA: 0x0018EF8E File Offset: 0x0018D18E
		internal DrillthroughInformation(string reportName, DrillthroughParameters reportParameters, IntList dataSetTokenIDs)
		{
			this.m_reportName = reportName;
			this.m_reportParameters = reportParameters;
			this.m_dataSetTokenIDs = dataSetTokenIDs;
		}

		// Token: 0x170023CB RID: 9163
		// (get) Token: 0x06006519 RID: 25881 RVA: 0x0018EFAB File Offset: 0x0018D1AB
		// (set) Token: 0x0600651A RID: 25882 RVA: 0x0018EFB3 File Offset: 0x0018D1B3
		internal string ReportName
		{
			get
			{
				return this.m_reportName;
			}
			set
			{
				this.m_reportName = value;
			}
		}

		// Token: 0x170023CC RID: 9164
		// (get) Token: 0x0600651B RID: 25883 RVA: 0x0018EFBC File Offset: 0x0018D1BC
		// (set) Token: 0x0600651C RID: 25884 RVA: 0x0018EFC4 File Offset: 0x0018D1C4
		internal DrillthroughParameters ReportParameters
		{
			get
			{
				return this.m_reportParameters;
			}
			set
			{
				this.m_reportParameters = value;
			}
		}

		// Token: 0x170023CD RID: 9165
		// (get) Token: 0x0600651D RID: 25885 RVA: 0x0018EFCD File Offset: 0x0018D1CD
		// (set) Token: 0x0600651E RID: 25886 RVA: 0x0018EFD5 File Offset: 0x0018D1D5
		internal IntList DataSetTokenIDs
		{
			get
			{
				return this.m_dataSetTokenIDs;
			}
			set
			{
				this.m_dataSetTokenIDs = value;
			}
		}

		// Token: 0x0600651F RID: 25887 RVA: 0x0018EFE0 File Offset: 0x0018D1E0
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.DrillthroughReportName, Token.String),
				new MemberInfo(MemberName.DrillthroughParameters, ObjectType.DrillthroughParameters),
				new MemberInfo(MemberName.DataSets, ObjectType.IntList)
			});
		}

		// Token: 0x06006520 RID: 25888 RVA: 0x0018F03C File Offset: 0x0018D23C
		internal void ResolveDataSetTokenIDs(TokensHashtable dataSetTokenIDs)
		{
			if (dataSetTokenIDs == null || this.m_dataSetTokenIDs == null)
			{
				return;
			}
			DrillthroughParameters drillthroughParameters = new DrillthroughParameters();
			for (int i = 0; i < this.m_dataSetTokenIDs.Count; i++)
			{
				object obj;
				if (this.m_dataSetTokenIDs[i] >= 0)
				{
					obj = dataSetTokenIDs[this.m_dataSetTokenIDs[i]];
				}
				else
				{
					obj = this.m_reportParameters.GetValue(i);
				}
				drillthroughParameters.Add(this.m_reportParameters.GetKey(i), obj);
			}
			this.m_reportParameters = drillthroughParameters;
		}

		// Token: 0x0400329D RID: 12957
		private string m_reportName;

		// Token: 0x0400329E RID: 12958
		private DrillthroughParameters m_reportParameters;

		// Token: 0x0400329F RID: 12959
		private IntList m_dataSetTokenIDs;
	}
}
