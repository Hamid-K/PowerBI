using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x02000015 RID: 21
	[DataContract]
	public class KeyValue
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004101 File Offset: 0x00002301
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00004109 File Offset: 0x00002309
		[DataMember(Name = "key")]
		public string Key { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004112 File Offset: 0x00002312
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000411A File Offset: 0x0000231A
		[DataMember(Name = "value")]
		public string Value { get; set; }

		// Token: 0x060000B1 RID: 177 RVA: 0x00004123 File Offset: 0x00002323
		public KeyValue(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004139 File Offset: 0x00002339
		public override string ToString()
		{
			return string.Format("[{0}, {1}]", this.Key, this.Value);
		}
	}
}
