using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ServiceContract
{
	// Token: 0x02000006 RID: 6
	[DataContract]
	internal sealed class OdataError
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020A8 File Offset: 0x000002A8
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020B0 File Offset: 0x000002B0
		[DataMember(Name = "code", IsRequired = true, EmitDefaultValue = false)]
		internal string Code { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020B9 File Offset: 0x000002B9
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020C1 File Offset: 0x000002C1
		[DataMember(Name = "message", IsRequired = true, EmitDefaultValue = false)]
		internal LocalizedMessage Message { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020CA File Offset: 0x000002CA
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020D2 File Offset: 0x000002D2
		[DataMember(Name = "azure:values", IsRequired = false, EmitDefaultValue = false)]
		internal AzureValue[] AzureValuesArray { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020DB File Offset: 0x000002DB
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

		// Token: 0x0600000F RID: 15 RVA: 0x000020FC File Offset: 0x000002FC
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

		// Token: 0x0400002B RID: 43
		private AzureValue _azureValues;
	}
}
