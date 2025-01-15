using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000212 RID: 530
	internal sealed class GetSubscriptionPropertiesAction : RSSoapAction<GetSubscriptionPropertiesActionParameters>
	{
		// Token: 0x060012D8 RID: 4824 RVA: 0x00043098 File Offset: 0x00041298
		public GetSubscriptionPropertiesAction(RSService service)
			: base("GetSubscriptionPropertiessAction", service)
		{
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x000430A8 File Offset: 0x000412A8
		internal override void PerformActionNow()
		{
			ExtensionSettings extensionSettings;
			string text;
			ActiveState activeState;
			string text2;
			string text3;
			string text4;
			string text5;
			ParameterValueOrFieldReference[] array;
			DataRetrievalPlan dataRetrievalPlan;
			base.Service.SubscriptionManager.GetSubscriptionProperties(Globals.ParseGuidParameter(base.ActionParameters.SubscriptionID, "SubscriptionID"), out extensionSettings, out text, out activeState, out text2, out text3, out text4, out text5, out array, out dataRetrievalPlan, base.ActionParameters.LookingForDataDriven, base.Service);
			base.ActionParameters.ExtensionSettings = extensionSettings;
			base.ActionParameters.Description = text;
			base.ActionParameters.Active = activeState;
			base.ActionParameters.Status = text2;
			base.ActionParameters.Owner = text3;
			base.ActionParameters.EventType = text4;
			base.ActionParameters.MatchData = text5;
			base.ActionParameters.Parameters = array;
			base.ActionParameters.DataSettings = dataRetrievalPlan;
		}
	}
}
