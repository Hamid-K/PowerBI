using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Lucia.Yaml;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001D9 RID: 473
	public sealed class ValueList<T> : List<T>, IValueList, IList, ICollection, IEnumerable, ICustomSerializationOptions, IScalarForm<T>
	{
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00013303 File Offset: 0x00011503
		YamlSerializationOptions ICustomSerializationOptions.Options
		{
			get
			{
				if (base.Count >= 10)
				{
					return YamlSerializationOptions.None;
				}
				return YamlSerializationOptions.Compact;
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00013312 File Offset: 0x00011512
		bool IScalarForm<T>.TryGetScalarForm(out T value)
		{
			if (base.Count == 1)
			{
				value = base[0];
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00013334 File Offset: 0x00011534
		void IScalarForm<T>.SetFromScalarForm(T value)
		{
			base.Clear();
			base.Add(value);
		}

		// Token: 0x040007EF RID: 2031
		private const int MaxCompactItemCount = 10;
	}
}
