using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.LoadBalancer
{
	// Token: 0x0200000F RID: 15
	[DataContract]
	public abstract class LoadBalancingSettings<TSettings> : LoadBalancingSettings where TSettings : LoadBalancingSettings
	{
		// Token: 0x0600004C RID: 76 RVA: 0x0000276C File Offset: 0x0000096C
		public static bool TryParse(string value, out TSettings settings)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				settings = default(TSettings);
				return false;
			}
			bool flag;
			try
			{
				settings = JsonConvert.DeserializeObject<TSettings>(value);
				flag = true;
			}
			catch (JsonException)
			{
				settings = default(TSettings);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000027B8 File Offset: 0x000009B8
		public static TSettings Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return JsonConvert.DeserializeObject<TSettings>(value);
		}
	}
}
