using System;
using System.Collections.Generic;
using Microsoft.Lucia.Yaml;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x02000196 RID: 406
	public sealed class CustomSerializableList<T> : List<T>, ICustomSerializationOptions
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x00010B44 File Offset: 0x0000ED44
		public CustomSerializableList(YamlSerializationOptions serializationOption)
		{
			this._options = serializationOption;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00010B53 File Offset: 0x0000ED53
		YamlSerializationOptions ICustomSerializationOptions.Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x0400071A RID: 1818
		private readonly YamlSerializationOptions _options;
	}
}
