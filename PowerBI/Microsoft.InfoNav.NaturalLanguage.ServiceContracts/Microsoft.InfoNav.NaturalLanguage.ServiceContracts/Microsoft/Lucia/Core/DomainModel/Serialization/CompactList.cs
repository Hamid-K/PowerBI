using System;
using System.Collections.Generic;
using Microsoft.Lucia.Yaml;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x02000195 RID: 405
	internal class CompactList<T> : List<T>, ICustomSerializationOptions where T : IScalarForm<string>
	{
		// Token: 0x06000834 RID: 2100 RVA: 0x00010AC1 File Offset: 0x0000ECC1
		internal virtual IEnumerable<T> GetItemsToSerialize()
		{
			return this;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00010AC4 File Offset: 0x0000ECC4
		YamlSerializationOptions ICustomSerializationOptions.Options
		{
			get
			{
				int num = 0;
				foreach (T t in this.GetItemsToSerialize())
				{
					string text;
					if (!t.TryGetScalarForm(out text))
					{
						return YamlSerializationOptions.None;
					}
					num += text.Length;
					if (num > 80)
					{
						return YamlSerializationOptions.None;
					}
				}
				return YamlSerializationOptions.Compact;
			}
		}

		// Token: 0x04000719 RID: 1817
		private const int MaxCompactListLength = 80;
	}
}
