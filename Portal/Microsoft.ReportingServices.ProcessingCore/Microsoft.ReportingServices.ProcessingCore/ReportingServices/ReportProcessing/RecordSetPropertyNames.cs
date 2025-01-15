using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200070F RID: 1807
	[Serializable]
	internal sealed class RecordSetPropertyNames
	{
		// Token: 0x060064F2 RID: 25842 RVA: 0x0018EC6E File Offset: 0x0018CE6E
		internal RecordSetPropertyNames()
		{
		}

		// Token: 0x170023BE RID: 9150
		// (get) Token: 0x060064F3 RID: 25843 RVA: 0x0018EC76 File Offset: 0x0018CE76
		// (set) Token: 0x060064F4 RID: 25844 RVA: 0x0018EC7E File Offset: 0x0018CE7E
		internal StringList PropertyNames
		{
			get
			{
				return this.m_propertyNames;
			}
			set
			{
				this.m_propertyNames = value;
			}
		}

		// Token: 0x060064F5 RID: 25845 RVA: 0x0018EC88 File Offset: 0x0018CE88
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.PropertyNames, ObjectType.StringList)
			});
		}

		// Token: 0x04003292 RID: 12946
		private StringList m_propertyNames;
	}
}
