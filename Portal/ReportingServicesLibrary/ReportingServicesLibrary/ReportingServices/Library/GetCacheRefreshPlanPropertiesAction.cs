using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200021A RID: 538
	internal sealed class GetCacheRefreshPlanPropertiesAction : RSSoapAction<GetCacheRefreshPlanPropertiesActionParameters>
	{
		// Token: 0x0600131C RID: 4892 RVA: 0x00043990 File Offset: 0x00041B90
		public GetCacheRefreshPlanPropertiesAction(RSService service)
			: base("GetSubscriptionPropertiessAction", service)
		{
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x000439A0 File Offset: 0x00041BA0
		internal override void PerformActionNow()
		{
			string text;
			ActiveState activeState;
			string text2;
			string text3;
			string text4;
			ParameterValueOrFieldReference[] array;
			base.Service.SubscriptionManager.GetCacheRefreshPlanProperties(Globals.ParseGuidParameter(base.ActionParameters.CacheRefreshPlanID, "CacheRefreshPlanID"), out text, out activeState, out text2, out text3, out text4, out array, base.Service);
			base.ActionParameters.Description = text;
			base.ActionParameters.State = activeState;
			base.ActionParameters.Status = text2;
			base.ActionParameters.EventType = text3;
			base.ActionParameters.MatchData = text4;
			base.ActionParameters.Parameters = array;
		}
	}
}
