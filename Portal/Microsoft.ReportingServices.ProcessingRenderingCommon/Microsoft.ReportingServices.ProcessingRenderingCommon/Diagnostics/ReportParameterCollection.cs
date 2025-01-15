using System;
using System.Collections.Specialized;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200008A RID: 138
	internal class ReportParameterCollection : NameValueCollection
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x0000D11B File Offset: 0x0000B31B
		internal ReportParameterCollection(NameValueCollection other)
			: base(other)
		{
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000D124 File Offset: 0x0000B324
		public override int GetHashCode()
		{
			int num = 0;
			StringCollection stringCollection = new StringCollection();
			for (int i = 0; i < this.Count; i++)
			{
				string key = this.GetKey(i);
				stringCollection.Add(key);
				string[] values = this.GetValues(i);
				if (values != null)
				{
					foreach (string text in values)
					{
						if (text != null)
						{
							stringCollection.Add(text);
						}
					}
				}
			}
			foreach (string text2 in stringCollection)
			{
				num ^= text2.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000D1E0 File Offset: 0x0000B3E0
		public override bool Equals(object obj)
		{
			NameValueCollection nameValueCollection = obj as NameValueCollection;
			if (nameValueCollection == null)
			{
				return false;
			}
			if (this.Count != nameValueCollection.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (this.GetKey(i) != nameValueCollection.GetKey(i))
				{
					return false;
				}
				string[] values = this.GetValues(i);
				string[] values2 = nameValueCollection.GetValues(i);
				if (values == null != (values2 == null))
				{
					return false;
				}
				if (values != null)
				{
					if (values.Length != values2.Length)
					{
						return false;
					}
					for (int j = 0; j < values.Length; j++)
					{
						if (values[j] != values2[j])
						{
							return false;
						}
					}
				}
			}
			return true;
		}
	}
}
