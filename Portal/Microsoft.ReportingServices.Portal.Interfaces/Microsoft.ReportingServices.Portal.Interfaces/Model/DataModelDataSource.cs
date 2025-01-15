using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Model
{
	// Token: 0x02000009 RID: 9
	public sealed class DataModelDataSource
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000021A5 File Offset: 0x000003A5
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000021AD File Offset: 0x000003AD
		[ReadOnly(true)]
		[JsonConverter(typeof(StringEnumConverter))]
		public DataModelDataSourceType Type { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000021B6 File Offset: 0x000003B6
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000021BE File Offset: 0x000003BE
		[ReadOnly(true)]
		[JsonConverter(typeof(StringEnumConverter))]
		public DataModelDataSourceKind Kind { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000021C7 File Offset: 0x000003C7
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000021CF File Offset: 0x000003CF
		[JsonConverter(typeof(StringEnumConverter))]
		public DataModelDataSourceAuthType AuthType { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000021D8 File Offset: 0x000003D8
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000021E0 File Offset: 0x000003E0
		[JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
		public DataModelDataSourceAuthType[] SupportedAuthTypes { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000021E9 File Offset: 0x000003E9
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000021F1 File Offset: 0x000003F1
		public string Username { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000021FA File Offset: 0x000003FA
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002202 File Offset: 0x00000402
		public string Secret { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000220B File Offset: 0x0000040B
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002213 File Offset: 0x00000413
		public string ModelConnectionName { get; set; }
	}
}
