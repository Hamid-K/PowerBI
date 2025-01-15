using System;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Schema
{
	// Token: 0x02001FD4 RID: 8148
	internal sealed class SchemaConfig
	{
		// Token: 0x17002CDA RID: 11482
		// (get) Token: 0x0601105F RID: 69727 RVA: 0x003AB3AB File Offset: 0x003A95AB
		// (set) Token: 0x06011060 RID: 69728 RVA: 0x003AB3B3 File Offset: 0x003A95B3
		public int MaxDepth { get; set; }

		// Token: 0x17002CDB RID: 11483
		// (get) Token: 0x06011061 RID: 69729 RVA: 0x003AB3BC File Offset: 0x003A95BC
		// (set) Token: 0x06011062 RID: 69730 RVA: 0x003AB3C4 File Offset: 0x003A95C4
		public TypeMapping DefaultTypeMapping { get; set; }

		// Token: 0x17002CDC RID: 11484
		// (get) Token: 0x06011063 RID: 69731 RVA: 0x003AB3CD File Offset: 0x003A95CD
		public TimeUnit DefaultTimeUnit
		{
			get
			{
				if (this.DefaultTypeMapping != TypeMapping.Sql)
				{
					return TimeUnit.Nanos;
				}
				return TimeUnit.Micros;
			}
		}

		// Token: 0x17002CDD RID: 11485
		// (get) Token: 0x06011064 RID: 69732 RVA: 0x003AB3DB File Offset: 0x003A95DB
		public bool AllTimesUtcAdjusted
		{
			get
			{
				return this.DefaultTypeMapping == TypeMapping.Sql;
			}
		}

		// Token: 0x040066D7 RID: 26327
		public static readonly SchemaConfig Default = new SchemaConfig();
	}
}
