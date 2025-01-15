using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Linq.JsonPath
{
	// Token: 0x020000DD RID: 221
	internal class ScanFilter : PathFilter
	{
		// Token: 0x06000C1E RID: 3102 RVA: 0x0003086B File Offset: 0x0002EA6B
		[NullableContext(2)]
		public ScanFilter(string name)
		{
			this.Name = name;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0003087A File Offset: 0x0002EA7A
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			foreach (JToken c in current)
			{
				if (this.Name == null)
				{
					yield return c;
				}
				JToken value = c;
				for (;;)
				{
					JContainer jcontainer = value as JContainer;
					value = PathFilter.GetNextScanValue(c, jcontainer, value);
					if (value == null)
					{
						break;
					}
					JProperty jproperty = value as JProperty;
					if (jproperty != null)
					{
						if (jproperty.Name == this.Name)
						{
							yield return jproperty.Value;
						}
					}
					else if (this.Name == null)
					{
						yield return value;
					}
				}
				value = null;
				c = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040003CE RID: 974
		[Nullable(2)]
		internal string Name;
	}
}
