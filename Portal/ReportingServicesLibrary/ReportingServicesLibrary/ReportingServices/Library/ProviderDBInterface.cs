using System;
using System.Collections;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000081 RID: 129
	internal class ProviderDBInterface : Storage
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x00016250 File Offset: 0x00014450
		public void InvalidateDeletedProviders()
		{
			ArrayList arrayList = new ArrayList();
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ListUsedDeliveryProviders", null))
			{
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						if (!dataReader.IsDBNull(0))
						{
							string @string = dataReader.GetString(0);
							arrayList.Add(@string);
						}
					}
				}
			}
			if (arrayList.Count > 0)
			{
				for (int i = arrayList.Count - 1; i >= 0; i--)
				{
					if (Globals.Configuration.Extensions.Delivery[(string)arrayList[i]] != null)
					{
						arrayList.Remove(arrayList[i]);
					}
				}
				if (arrayList.Count > 0)
				{
					this.InactivateSubscriptionWithRemovedProviders(arrayList);
				}
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00016330 File Offset: 0x00014530
		public void InactivateSubscriptionWithRemovedProviders(ArrayList deliveryExtensions)
		{
			if (deliveryExtensions.Count <= 0)
			{
				return;
			}
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeliveryRemovedInactivateSubscription", null))
			{
				instrumentedSqlCommand.AddParameter("@Status", SqlDbType.NVarChar, RepLibRes.DeliveryExtensionRemoved);
				SqlParameter sqlParameter = instrumentedSqlCommand.AddParameter("@DeliveryExtension", SqlDbType.NVarChar, null);
				foreach (object obj in deliveryExtensions)
				{
					string text = (string)obj;
					if (Global.m_Tracer.TraceInfo)
					{
						Global.m_Tracer.Trace("Delivery extensions {0} was removed and subscription with this extension will be marked inactive.", new object[] { text });
					}
					sqlParameter.Value = text;
					instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
		}
	}
}
