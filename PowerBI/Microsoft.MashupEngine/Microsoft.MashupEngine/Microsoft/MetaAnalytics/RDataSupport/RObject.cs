using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.MetaAnalytics.RDataSupport
{
	// Token: 0x0200016C RID: 364
	[Serializable]
	public abstract class RObject
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0000B53F File Offset: 0x0000973F
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0000B547 File Offset: 0x00009747
		public KeyValuePair<string, RObject>[] Attributes { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060006EA RID: 1770
		public abstract Array Values { get; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0000B550 File Offset: 0x00009750
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x0000B558 File Offset: 0x00009758
		public bool IsRaw { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0000B564 File Offset: 0x00009764
		public string ClassName
		{
			get
			{
				RObject<string> robject = this.GetAttribute("class") as RObject<string>;
				if (robject == null || robject.TypedValues.Length != 1)
				{
					return null;
				}
				return robject.TypedValues[0];
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0000B59C File Offset: 0x0000979C
		public RObject GetAttribute(string name)
		{
			if (this.Attributes == null || this.Attributes.Length == 0)
			{
				return null;
			}
			KeyValuePair<string, RObject> keyValuePair = this.Attributes.Where((KeyValuePair<string, RObject> a) => a.Key.Equals(name, StringComparison.Ordinal)).FirstOrDefault<KeyValuePair<string, RObject>>();
			if (keyValuePair.Key == null)
			{
				return null;
			}
			return keyValuePair.Value;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0000B5F8 File Offset: 0x000097F8
		public RObject<T> GetAttribute<T>(string name)
		{
			return this.GetAttribute(name) as RObject<T>;
		}
	}
}
