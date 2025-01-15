using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200071A RID: 1818
	[Serializable]
	internal sealed class ReportInstanceInfo : ReportItemInstanceInfo
	{
		// Token: 0x0600657F RID: 25983 RVA: 0x0018FB2C File Offset: 0x0018DD2C
		internal ReportInstanceInfo(ReportProcessing.ProcessingContext pc, Report reportItemDef, ReportInstance owner, ParameterInfoCollection parameters, bool noRows)
			: base(pc, reportItemDef, owner, true)
		{
			this.m_bodyUniqueName = pc.CreateUniqueName();
			this.m_reportName = pc.ReportContext.ItemName;
			this.m_parameters = new ParameterInfoCollection();
			if (parameters != null && parameters.Count > 0)
			{
				parameters.CopyTo(this.m_parameters);
			}
			this.m_noRows = noRows;
		}

		// Token: 0x06006580 RID: 25984 RVA: 0x0018FB8E File Offset: 0x0018DD8E
		internal ReportInstanceInfo(Report reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x170023F2 RID: 9202
		// (get) Token: 0x06006581 RID: 25985 RVA: 0x0018FB97 File Offset: 0x0018DD97
		// (set) Token: 0x06006582 RID: 25986 RVA: 0x0018FB9F File Offset: 0x0018DD9F
		internal ParameterInfoCollection Parameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x170023F3 RID: 9203
		// (get) Token: 0x06006583 RID: 25987 RVA: 0x0018FBA8 File Offset: 0x0018DDA8
		// (set) Token: 0x06006584 RID: 25988 RVA: 0x0018FBB0 File Offset: 0x0018DDB0
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

		// Token: 0x170023F4 RID: 9204
		// (get) Token: 0x06006585 RID: 25989 RVA: 0x0018FBB9 File Offset: 0x0018DDB9
		// (set) Token: 0x06006586 RID: 25990 RVA: 0x0018FBC1 File Offset: 0x0018DDC1
		internal bool NoRows
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

		// Token: 0x170023F5 RID: 9205
		// (get) Token: 0x06006587 RID: 25991 RVA: 0x0018FBCA File Offset: 0x0018DDCA
		// (set) Token: 0x06006588 RID: 25992 RVA: 0x0018FBD2 File Offset: 0x0018DDD2
		internal int BodyUniqueName
		{
			get
			{
				return this.m_bodyUniqueName;
			}
			set
			{
				this.m_bodyUniqueName = value;
			}
		}

		// Token: 0x06006589 RID: 25993 RVA: 0x0018FBDC File Offset: 0x0018DDDC
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.Parameters, ObjectType.ParameterInfoCollection),
				new MemberInfo(MemberName.ReportName, Token.String),
				new MemberInfo(MemberName.NoRows, Token.Boolean),
				new MemberInfo(MemberName.BodyUniqueName, Token.Int32)
			});
		}

		// Token: 0x040032C0 RID: 12992
		private ParameterInfoCollection m_parameters;

		// Token: 0x040032C1 RID: 12993
		private string m_reportName;

		// Token: 0x040032C2 RID: 12994
		private bool m_noRows;

		// Token: 0x040032C3 RID: 12995
		private int m_bodyUniqueName;
	}
}
