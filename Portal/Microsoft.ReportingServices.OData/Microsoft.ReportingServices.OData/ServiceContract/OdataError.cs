using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ServiceContract
{
	// Token: 0x02000003 RID: 3
	[DataContract]
	internal sealed class OdataError
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002091 File Offset: 0x00000291
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002099 File Offset: 0x00000299
		[DataMember(Name = "code", IsRequired = true, EmitDefaultValue = false)]
		internal string Code { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020A2 File Offset: 0x000002A2
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020AA File Offset: 0x000002AA
		[DataMember(Name = "message", IsRequired = true, EmitDefaultValue = false)]
		internal LocalizedMessage Message { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020B3 File Offset: 0x000002B3
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020BB File Offset: 0x000002BB
		[DataMember(Name = "azure:values", IsRequired = false, EmitDefaultValue = false)]
		internal AzureValue[] AzureValuesArray { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020C4 File Offset: 0x000002C4
		internal AzureValue AzureValues
		{
			get
			{
				if (this._azureValues == null)
				{
					this._azureValues = OdataError.BuildAzureValueFromArray(this.AzureValuesArray);
				}
				return this._azureValues;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020E8 File Offset: 0x000002E8
		private static AzureValue BuildAzureValueFromArray(AzureValue[] azureValuesArray)
		{
			AzureValue azureValue6 = azureValuesArray.FirstOrDefault((AzureValue azureValue) => azureValue.Details != null);
			AzureValue azureValue2 = azureValuesArray.FirstOrDefault((AzureValue azureValue) => azureValue.TimeStamp != null);
			AzureValue azureValue3 = azureValuesArray.FirstOrDefault((AzureValue azureValue) => azureValue.AdditionalMessages != null);
			AzureValue azureValue4 = azureValuesArray.FirstOrDefault((AzureValue azureValue) => azureValue.MoreInformation != null);
			AzureValue azureValue5 = azureValuesArray.FirstOrDefault((AzureValue azureValue) => azureValue.PowerBIErrorDetails != null);
			return new AzureValue
			{
				Details = ((azureValue6 == null) ? null : azureValue6.Details),
				TimeStamp = ((azureValue2 == null) ? null : azureValue2.TimeStamp),
				AdditionalMessages = ((azureValue3 == null) ? null : azureValue3.AdditionalMessages),
				MoreInformation = ((azureValue4 == null) ? null : azureValue4.MoreInformation),
				PowerBIErrorDetails = ((azureValue5 == null) ? null : azureValue5.PowerBIErrorDetails)
			};
		}

		// Token: 0x04000003 RID: 3
		private AzureValue _azureValues;
	}
}
