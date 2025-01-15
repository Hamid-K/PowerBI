using System;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000036 RID: 54
	internal class ReportParameter2005 : ReportParameter
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00003BEC File Offset: 0x00001DEC
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00003C1C File Offset: 0x00001E1C
		public new ReportExpression? Prompt
		{
			get
			{
				if (base.PropertyStore.ContainsObject(5))
				{
					return new ReportExpression?(base.Prompt);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					base.Prompt = value.Value;
				}
			}
		}
	}
}
