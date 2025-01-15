using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.Gateway
{
	// Token: 0x0200001C RID: 28
	[DataContract]
	public abstract class ServerAnnotation<TAnnotation> : ServerAnnotation where TAnnotation : ServerAnnotation
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00002C6C File Offset: 0x00000E6C
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

		// Token: 0x0600009C RID: 156 RVA: 0x00002CB4 File Offset: 0x00000EB4
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
