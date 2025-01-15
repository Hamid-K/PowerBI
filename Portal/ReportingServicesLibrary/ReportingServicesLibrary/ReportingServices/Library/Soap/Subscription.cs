using System;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000341 RID: 833
	public class Subscription
	{
		// Token: 0x06001BEA RID: 7146 RVA: 0x0007102C File Offset: 0x0006F22C
		internal static Subscription[] LibrarySubscriptionArrayToThisArray(SubscriptionImpl[] list, IPathTranslator pathTranslator)
		{
			if (list == null)
			{
				return null;
			}
			Subscription[] array = new Subscription[list.Length];
			int num = 0;
			foreach (SubscriptionImpl subscriptionImpl in list)
			{
				array[num] = new Subscription
				{
					SubscriptionID = subscriptionImpl.ID.ToString(),
					Owner = subscriptionImpl.Owner.UserName,
					Path = subscriptionImpl.m_itemName.Value,
					VirtualPath = pathTranslator.PathToExternal(subscriptionImpl.m_itemName.Value),
					Report = subscriptionImpl.ItemNameOnly,
					DeliverySettings = subscriptionImpl.m_extensionSettings,
					Description = subscriptionImpl.m_description,
					Status = subscriptionImpl.LastStatus,
					Active = new ActiveState(subscriptionImpl.ActiveState),
					LastExecuted = subscriptionImpl.m_lastRunTime,
					LastExecutedSpecified = (subscriptionImpl.m_lastRunTime != DateTime.MinValue),
					ModifiedBy = subscriptionImpl.ModifiedBy.UserName,
					ModifiedDate = subscriptionImpl.m_modifiedDate,
					EventType = subscriptionImpl.m_eventType,
					IsDataDriven = subscriptionImpl.IsDataDriven()
				};
				num++;
			}
			return array;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x00071180 File Offset: 0x0006F380
		public static void CheckParameterArray(ParameterValueOrFieldReference[] parameters, string parameterName)
		{
			if (parameters == null)
			{
				return;
			}
			foreach (ParameterValueOrFieldReference parameterValueOrFieldReference in parameters)
			{
				ParameterFieldReference parameterFieldReference = parameterValueOrFieldReference as ParameterFieldReference;
				if (parameterFieldReference != null)
				{
					if (parameterFieldReference.ParameterName == null || parameterFieldReference.FieldAlias == null)
					{
						throw new InvalidParameterException(parameterName);
					}
				}
				else
				{
					ParameterValue parameterValue = parameterValueOrFieldReference as ParameterValue;
					if (parameterValue == null || parameterValue.Name == null)
					{
						throw new InvalidParameterException(parameterName);
					}
				}
			}
		}

		// Token: 0x04000B50 RID: 2896
		public string SubscriptionID;

		// Token: 0x04000B51 RID: 2897
		public string Owner;

		// Token: 0x04000B52 RID: 2898
		public string Path;

		// Token: 0x04000B53 RID: 2899
		public string VirtualPath;

		// Token: 0x04000B54 RID: 2900
		public string Report;

		// Token: 0x04000B55 RID: 2901
		public ExtensionSettings DeliverySettings;

		// Token: 0x04000B56 RID: 2902
		public string Description;

		// Token: 0x04000B57 RID: 2903
		public string Status;

		// Token: 0x04000B58 RID: 2904
		public ActiveState Active;

		// Token: 0x04000B59 RID: 2905
		public DateTime LastExecuted;

		// Token: 0x04000B5A RID: 2906
		[XmlIgnore]
		public bool LastExecutedSpecified;

		// Token: 0x04000B5B RID: 2907
		public string ModifiedBy;

		// Token: 0x04000B5C RID: 2908
		public DateTime ModifiedDate;

		// Token: 0x04000B5D RID: 2909
		public string EventType;

		// Token: 0x04000B5E RID: 2910
		public bool IsDataDriven;
	}
}
