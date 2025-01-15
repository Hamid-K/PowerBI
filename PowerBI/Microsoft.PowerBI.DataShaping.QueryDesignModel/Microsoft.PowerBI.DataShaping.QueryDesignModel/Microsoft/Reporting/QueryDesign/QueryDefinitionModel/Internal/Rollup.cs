using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000118 RID: 280
	internal sealed class Rollup
	{
		// Token: 0x0600100D RID: 4109 RVA: 0x0002C381 File Offset: 0x0002A581
		internal Rollup(IEnumerable<RollupGroup> rollupGroups)
		{
			ArgumentValidation.CheckNotNullOrEmpty<RollupGroup>(rollupGroups, "rollupGroups");
			this._rollupGroups = new QdmItemCollection<RollupGroup>(rollupGroups);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0002C3A1 File Offset: 0x0002A5A1
		internal Rollup(RollupGroup rollupGroup)
		{
			ArgumentValidation.CheckNotNull<RollupGroup>(rollupGroup, "rollupGroup");
			this._rollupGroups = new QdmItemCollection<RollupGroup>(new RollupGroup[] { rollupGroup });
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x0002C3CA File Offset: 0x0002A5CA
		public QdmItemCollection<RollupGroup> RollupGroups
		{
			get
			{
				return this._rollupGroups;
			}
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0002C3D4 File Offset: 0x0002A5D4
		internal Rollup OmitMissingGroups(IEnumerable<Group> groups)
		{
			RollupGroup[] array = (from rg in this.RollupGroups
				let newRg = rg.OmitMissingGroups(groups)
				where newRg != null
				select newRg).ToArray<RollupGroup>();
			if (array.Any<RollupGroup>())
			{
				return new Rollup(array);
			}
			return null;
		}

		// Token: 0x04000A5A RID: 2650
		private readonly QdmItemCollection<RollupGroup> _rollupGroups;
	}
}
