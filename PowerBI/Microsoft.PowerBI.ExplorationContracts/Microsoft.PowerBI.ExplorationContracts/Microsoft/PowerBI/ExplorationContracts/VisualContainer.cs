using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.ExplorationContracts
{
	// Token: 0x02000009 RID: 9
	[DataContract]
	public class VisualContainer
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000218F File Offset: 0x0000038F
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002197 File Offset: 0x00000397
		[DataMember(Name = "x", EmitDefaultValue = true)]
		public decimal X { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000021A0 File Offset: 0x000003A0
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000021A8 File Offset: 0x000003A8
		[DataMember(Name = "y", EmitDefaultValue = true)]
		public decimal Y { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000021B1 File Offset: 0x000003B1
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000021B9 File Offset: 0x000003B9
		[DataMember(Name = "z", EmitDefaultValue = true)]
		public decimal Z { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000021C2 File Offset: 0x000003C2
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000021CA File Offset: 0x000003CA
		[DataMember(Name = "width", EmitDefaultValue = true)]
		public decimal Width { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000021D3 File Offset: 0x000003D3
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000021DB File Offset: 0x000003DB
		[DataMember(Name = "height", EmitDefaultValue = true)]
		public decimal Height { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000021E4 File Offset: 0x000003E4
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000021EC File Offset: 0x000003EC
		[DataMember(Name = "config", EmitDefaultValue = false)]
		public string Config { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000021F5 File Offset: 0x000003F5
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000021FD File Offset: 0x000003FD
		[DataMember(Name = "data", EmitDefaultValue = false)]
		public string Data { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002206 File Offset: 0x00000406
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000220E File Offset: 0x0000040E
		[DataMember(Name = "filters", EmitDefaultValue = false)]
		public string Filters { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002217 File Offset: 0x00000417
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000221F File Offset: 0x0000041F
		[DataMember(Name = "query", EmitDefaultValue = false)]
		public string Query { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002228 File Offset: 0x00000428
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000225B File Offset: 0x0000045B
		[DataMember(Name = "dataBinaryBase64Encoded", EmitDefaultValue = false)]
		public string DataBinaryBase64Encoded
		{
			get
			{
				if (!this.m_dataBinaryBase64EncodedUpToDate)
				{
					this.m_dataBinaryBase64Encoded = ((this.DataBinary == null) ? null : Convert.ToBase64String(this.DataBinary));
					this.m_dataBinaryBase64EncodedUpToDate = true;
				}
				return this.m_dataBinaryBase64Encoded;
			}
			set
			{
				this.DataBinary = ((value != null) ? Convert.FromBase64String(value) : null);
				this.m_dataBinaryBase64Encoded = value;
				this.m_dataBinaryBase64EncodedUpToDate = true;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000227D File Offset: 0x0000047D
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002285 File Offset: 0x00000485
		public byte[] DataBinary
		{
			get
			{
				return this.m_dataBinary;
			}
			set
			{
				this.m_dataBinary = value;
				this.m_dataBinaryBase64EncodedUpToDate = false;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002295 File Offset: 0x00000495
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000229D File Offset: 0x0000049D
		public long? QueryHash { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000022A6 File Offset: 0x000004A6
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000022AE File Offset: 0x000004AE
		[DataMember(Name = "dataUpdatedTime", EmitDefaultValue = false)]
		public DateTime? DataUpdatedTime { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000022B7 File Offset: 0x000004B7
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000022BF File Offset: 0x000004BF
		[DataMember(Name = "dataTransforms", EmitDefaultValue = false)]
		public string DataTransforms { get; set; }

		// Token: 0x0400003D RID: 61
		private bool m_dataBinaryBase64EncodedUpToDate;

		// Token: 0x0400003E RID: 62
		private byte[] m_dataBinary;

		// Token: 0x0400003F RID: 63
		private string m_dataBinaryBase64Encoded;
	}
}
