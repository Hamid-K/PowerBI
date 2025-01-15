using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000038 RID: 56
	[DataContract]
	public abstract class DataSourceAnnotation<TAnnotation> : DataSourceAnnotation where TAnnotation : DataSourceAnnotation
	{
		// Token: 0x06000142 RID: 322 RVA: 0x0000338C File Offset: 0x0000158C
		public static bool TryParse(string value, out TAnnotation serverAnnotation)
		{
			if (value == null)
			{
				serverAnnotation = default(TAnnotation);
				return false;
			}
			bool flag;
			try
			{
				serverAnnotation = JsonConvert.DeserializeObject<TAnnotation>(value);
				flag = true;
			}
			catch (JsonException)
			{
				serverAnnotation = default(TAnnotation);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000033D4 File Offset: 0x000015D4
		public static TAnnotation Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return JsonConvert.DeserializeObject<TAnnotation>(value);
		}
	}
}
