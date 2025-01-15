using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;

namespace Microsoft.Mashup.Engine1.Library.DeltaLake
{
	// Token: 0x0200027D RID: 637
	public sealed class DeltaLakeModule : Module45
	{
		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x00034E95 File Offset: 0x00033095
		public override string Name
		{
			get
			{
				return "DeltaLake";
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x00034E9C File Offset: 0x0003309C
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "DeltaLake.Table";
						}
						throw new InvalidOperationException();
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x040007C5 RID: 1989
		private Keys exportKeys;

		// Token: 0x0200027E RID: 638
		private enum Exports
		{
			// Token: 0x040007C7 RID: 1991
			DeltaLakeTable,
			// Token: 0x040007C8 RID: 1992
			Count
		}
	}
}
