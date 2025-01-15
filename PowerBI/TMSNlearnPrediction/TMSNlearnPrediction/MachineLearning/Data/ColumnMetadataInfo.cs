using System;
using System.Collections.Generic;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000139 RID: 313
	public sealed class ColumnMetadataInfo
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x00021AEA File Offset: 0x0001FCEA
		public ColumnMetadataInfo(string name)
		{
			Contracts.CheckParam(!string.IsNullOrWhiteSpace(name), "index", "index must be >= 0");
			this.Name = name;
			this._infos = new Dictionary<string, MetadataInfo>();
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00021B1C File Offset: 0x0001FD1C
		public void Add(string kind, MetadataInfo info)
		{
			if (this._infos.ContainsKey(kind))
			{
				throw Contracts.Except("Already contains metadata of kind '{0}'", new object[] { kind });
			}
			this._infos.Add(kind, info);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00021B5B File Offset: 0x0001FD5B
		public Dictionary<string, MetadataInfo> Infos()
		{
			return this._infos;
		}

		// Token: 0x04000334 RID: 820
		public readonly string Name;

		// Token: 0x04000335 RID: 821
		private readonly Dictionary<string, MetadataInfo> _infos;
	}
}
