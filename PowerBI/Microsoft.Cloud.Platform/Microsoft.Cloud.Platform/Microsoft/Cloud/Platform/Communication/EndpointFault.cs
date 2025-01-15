using System;
using System.Runtime.Serialization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200049D RID: 1181
	[DataContract]
	public class EndpointFault
	{
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06002469 RID: 9321 RVA: 0x0008312B File Offset: 0x0008132B
		// (set) Token: 0x0600246A RID: 9322 RVA: 0x00083133 File Offset: 0x00081333
		[DataMember]
		public EndpointIdentifier Endpoint { get; private set; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600246B RID: 9323 RVA: 0x0008313C File Offset: 0x0008133C
		// (set) Token: 0x0600246C RID: 9324 RVA: 0x00083144 File Offset: 0x00081344
		[DataMember]
		public Exception Exception { get; private set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x0008314D File Offset: 0x0008134D
		// (set) Token: 0x0600246E RID: 9326 RVA: 0x00083155 File Offset: 0x00081355
		[DataMember]
		public MonitoredException ConvertedException { get; private set; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x0008315E File Offset: 0x0008135E
		// (set) Token: 0x06002470 RID: 9328 RVA: 0x00083166 File Offset: 0x00081366
		[DataMember]
		public RouterExceptionOrigin Origin { get; private set; }

		// Token: 0x06002471 RID: 9329 RVA: 0x0008316F File Offset: 0x0008136F
		public EndpointFault(EndpointIdentifier endpoint, Exception exception, MonitoredException convertedException, RouterExceptionOrigin origin)
		{
			this.Endpoint = endpoint;
			this.Exception = exception;
			this.Origin = origin;
			this.ConvertedException = convertedException;
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x00083194 File Offset: 0x00081394
		public override string ToString()
		{
			return "Exception type: {0}, Exception message: {1}, was thrown from endpoint: {2}".FormatWithInvariantCulture(new object[]
			{
				this.ConvertedException.GetType(),
				this.ConvertedException.Message,
				this.Endpoint.Uri.ToString()
			});
		}
	}
}
