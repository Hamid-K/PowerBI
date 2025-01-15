using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012BD RID: 4797
	internal class MetaCubeValue : DelegatingCubeValue
	{
		// Token: 0x06007E16 RID: 32278 RVA: 0x001B0368 File Offset: 0x001AE568
		public MetaCubeValue(CubeValue cube, RecordValue meta)
			: base(cube)
		{
			this.meta = meta;
		}

		// Token: 0x17002239 RID: 8761
		// (get) Token: 0x06007E17 RID: 32279 RVA: 0x001B0378 File Offset: 0x001AE578
		public override RecordValue MetaValue
		{
			get
			{
				return this.meta;
			}
		}

		// Token: 0x06007E18 RID: 32280 RVA: 0x001B0380 File Offset: 0x001AE580
		public override Value NewMeta(RecordValue metaValue)
		{
			return CubeValue.New(base.Cube, metaValue);
		}

		// Token: 0x04004547 RID: 17735
		private RecordValue meta;
	}
}
