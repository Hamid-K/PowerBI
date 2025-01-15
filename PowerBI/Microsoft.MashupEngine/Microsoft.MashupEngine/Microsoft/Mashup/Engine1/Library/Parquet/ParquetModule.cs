using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02000274 RID: 628
	public sealed class ParquetModule : Module45
	{
		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x00034D4B File Offset: 0x00032F4B
		public override string Name
		{
			get
			{
				return "Parquet";
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x00034D52 File Offset: 0x00032F52
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "Parquet.Document";
						}
						if (index != 1)
						{
							throw new InvalidOperationException();
						}
						return "Parquet.Metadata";
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x040007B0 RID: 1968
		private Keys exportKeys;

		// Token: 0x02000275 RID: 629
		private enum Exports
		{
			// Token: 0x040007B2 RID: 1970
			ParquetDocument,
			// Token: 0x040007B3 RID: 1971
			ParquetMetadata,
			// Token: 0x040007B4 RID: 1972
			Count
		}
	}
}
