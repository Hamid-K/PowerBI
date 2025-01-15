using System;
using System.Net;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common.DataContracts
{
	// Token: 0x02000159 RID: 345
	[DataContract]
	public sealed class PostDatabaseResult
	{
		// Token: 0x06001203 RID: 4611 RVA: 0x0004960A File Offset: 0x0004780A
		public PostDatabaseResult(ServiceEntity serviceEntity, DatabaseEntity databaseEntity, string displayName, string errorMessage, HttpStatusCode statusCode)
		{
			this.DisplayName = displayName;
			this.StatusCode = statusCode;
			this.ErrorMessage = errorMessage;
			this.ServiceEntity = serviceEntity;
			this.DatabaseEntity = databaseEntity;
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x00049637 File Offset: 0x00047837
		// (set) Token: 0x06001205 RID: 4613 RVA: 0x0004963F File Offset: 0x0004783F
		[DataMember]
		public HttpStatusCode StatusCode { get; private set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x00049648 File Offset: 0x00047848
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x00049650 File Offset: 0x00047850
		[DataMember]
		public string ErrorMessage { get; private set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x00049659 File Offset: 0x00047859
		// (set) Token: 0x06001209 RID: 4617 RVA: 0x00049661 File Offset: 0x00047861
		[DataMember]
		public string DisplayName { get; private set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x0004966A File Offset: 0x0004786A
		// (set) Token: 0x0600120B RID: 4619 RVA: 0x00049672 File Offset: 0x00047872
		[DataMember]
		public ServiceEntity ServiceEntity { get; private set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0004967B File Offset: 0x0004787B
		// (set) Token: 0x0600120D RID: 4621 RVA: 0x00049683 File Offset: 0x00047883
		[DataMember]
		public DatabaseEntity DatabaseEntity { get; private set; }
	}
}
