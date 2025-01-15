using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x02000009 RID: 9
	[DataContract]
	public sealed class ServiceError
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002180 File Offset: 0x00000380
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002188 File Offset: 0x00000388
		[DataMember(IsRequired = true, Order = 10, Name = "statusCode")]
		public ServiceErrorStatusCode StatusCode { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002191 File Offset: 0x00000391
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002199 File Offset: 0x00000399
		[DataMember(IsRequired = false, Order = 20, Name = "message")]
		public string Message { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021A2 File Offset: 0x000003A2
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000021AA File Offset: 0x000003AA
		[DataMember(IsRequired = false, Order = 30, Name = "stackTrace")]
		public string StackTrace { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021B3 File Offset: 0x000003B3
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000021BB File Offset: 0x000003BB
		[DataMember(IsRequired = false, Order = 40, Name = "errorDetails")]
		public IList<ErrorDetail> ErrorDetails { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000021C4 File Offset: 0x000003C4
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000021CC File Offset: 0x000003CC
		[DataMember(IsRequired = false, Order = 50, Name = "parameters", EmitDefaultValue = false)]
		public IDictionary<string, string> ErrorParameters { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000021D5 File Offset: 0x000003D5
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000021DD File Offset: 0x000003DD
		[DataMember(IsRequired = false, Order = 60, Name = "errorCode")]
		public string PowerBIErrorCode { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000021E6 File Offset: 0x000003E6
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000021EE File Offset: 0x000003EE
		[DataMember(IsRequired = false, Order = 70, Name = "exceptionCulprit", EmitDefaultValue = false)]
		public string PowerBIExceptionCulprit { get; set; }
	}
}
