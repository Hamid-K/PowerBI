using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000EF RID: 239
	internal static class ContextFilterTranslatorUtils
	{
		// Token: 0x060009AA RID: 2474 RVA: 0x00025048 File Offset: 0x00023248
		public static List<DataMember> RemoveStaticMembers(List<DataMember> members)
		{
			if (members == null)
			{
				return null;
			}
			for (int i = members.Count - 1; i >= 0; i--)
			{
				DataMember dataMember = members[i];
				if (dataMember.IsDynamic)
				{
					dataMember.DataMembers = ContextFilterTranslatorUtils.RemoveStaticMembers(dataMember.DataMembers);
				}
				else
				{
					members.RemoveAt(i);
					if (dataMember.DataMembers != null)
					{
						members.AddRange(dataMember.DataMembers);
						i = members.Count;
					}
				}
			}
			if (members.Count == 0)
			{
				return null;
			}
			return members;
		}
	}
}
