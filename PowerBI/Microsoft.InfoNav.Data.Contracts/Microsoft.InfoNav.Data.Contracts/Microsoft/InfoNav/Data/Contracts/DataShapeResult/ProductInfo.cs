using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x0200011C RID: 284
	[DataContract]
	public sealed class ProductInfo
	{
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0000F67A File Offset: 0x0000D87A
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x0000F682 File Offset: 0x0000D882
		[DataMember(Name = "productName", IsRequired = true, Order = 0)]
		public string ProductName { get; set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0000F68B File Offset: 0x0000D88B
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x0000F693 File Offset: 0x0000D893
		[DataMember(Name = "productVersion", IsRequired = true, Order = 10)]
		public string ProductVersion { get; set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0000F69C File Offset: 0x0000D89C
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x0000F6A4 File Offset: 0x0000D8A4
		[DataMember(Name = "productLocaleId", IsRequired = true, Order = 20)]
		public long ProductLocaleId { get; set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0000F6AD File Offset: 0x0000D8AD
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x0000F6B5 File Offset: 0x0000D8B5
		[DataMember(Name = "operatingSystem", IsRequired = true, Order = 30)]
		public string OperatingSystem { get; set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0000F6BE File Offset: 0x0000D8BE
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x0000F6C6 File Offset: 0x0000D8C6
		[DataMember(Name = "countryLocaleId", IsRequired = true, Order = 40)]
		public long CountryLocaleId { get; set; }
	}
}
