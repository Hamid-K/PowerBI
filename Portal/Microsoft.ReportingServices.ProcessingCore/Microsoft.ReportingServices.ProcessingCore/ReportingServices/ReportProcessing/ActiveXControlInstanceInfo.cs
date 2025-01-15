using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000733 RID: 1843
	[Serializable]
	internal sealed class ActiveXControlInstanceInfo : ReportItemInstanceInfo
	{
		// Token: 0x0600666B RID: 26219 RVA: 0x00191592 File Offset: 0x0018F792
		internal ActiveXControlInstanceInfo(ReportProcessing.ProcessingContext pc, ActiveXControl reportItemDef, ReportItemInstance owner, int index)
			: base(pc, reportItemDef, owner, index)
		{
			if (reportItemDef.Parameters != null)
			{
				this.m_parameterValues = new object[reportItemDef.Parameters.Count];
			}
		}

		// Token: 0x0600666C RID: 26220 RVA: 0x001915BD File Offset: 0x0018F7BD
		internal ActiveXControlInstanceInfo(ActiveXControl reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x17002435 RID: 9269
		// (get) Token: 0x0600666D RID: 26221 RVA: 0x001915C6 File Offset: 0x0018F7C6
		// (set) Token: 0x0600666E RID: 26222 RVA: 0x001915CE File Offset: 0x0018F7CE
		internal object[] ParameterValues
		{
			get
			{
				return this.m_parameterValues;
			}
			set
			{
				this.m_parameterValues = value;
			}
		}

		// Token: 0x0600666F RID: 26223 RVA: 0x001915D8 File Offset: 0x0018F7D8
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.ParameterValues, Token.Array, ObjectType.Variant)
			});
		}

		// Token: 0x040032FB RID: 13051
		private object[] m_parameterValues;
	}
}
