using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002D5 RID: 725
	public class NameValueDictionary : Dictionary<string, string>
	{
		// Token: 0x0600136C RID: 4972 RVA: 0x0004370C File Offset: 0x0004190C
		public NameValueDictionary()
		{
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00043714 File Offset: 0x00041914
		public NameValueDictionary(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0004371D File Offset: 0x0004191D
		public NameValueDictionary(NameValueDictionary source, int additionalCapacity)
			: base(((source != null) ? source.Count : 0) + additionalCapacity)
		{
			this.CopyFrom(source);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0004373C File Offset: 0x0004193C
		public void CopyFrom(NameValueDictionary source)
		{
			if (source == null)
			{
				return;
			}
			foreach (string text in source.Keys)
			{
				string text2 = source[text];
				if (base.ContainsKey(text))
				{
					base[text] = text2;
				}
				else
				{
					base.Add(text, text2);
				}
			}
		}
	}
}
