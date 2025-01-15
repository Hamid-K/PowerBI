using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001EF RID: 495
	internal sealed class GetPoliciesAction : RSSoapAction<GetPoliciesActionParameters>
	{
		// Token: 0x060010EA RID: 4330 RVA: 0x0003AAE6 File Offset: 0x00038CE6
		internal GetPoliciesAction(RSService service)
			: base("GetPoliciesAction", service)
		{
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0003AAF4 File Offset: 0x00038CF4
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Item");
			bool flag;
			string text;
			base.Service.SecMgr.GetPolicies(catalogItemContext.ItemPath, out flag, out text);
			base.ActionParameters.InheritParent = flag;
			base.ActionParameters.Policies = Policy.XmlToPolicyArray(text);
		}
	}
}
