using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FF1 RID: 4081
	internal class DirectoryServicesActiveDirectoryServiceSearchResult : ActiveDirectoryServiceSearchResult
	{
		// Token: 0x06006B0F RID: 27407 RVA: 0x0017105C File Offset: 0x0016F25C
		public DirectoryServicesActiveDirectoryServiceSearchResult(SearchResult result)
		{
			this.result = result;
		}

		// Token: 0x06006B10 RID: 27408 RVA: 0x0017106C File Offset: 0x0016F26C
		public override bool TryGetAttribute(string attributeName, out object[] value)
		{
			if (this.result.Properties.Contains(attributeName))
			{
				ResultPropertyValueCollection resultPropertyValueCollection = this.result.Properties[attributeName];
				value = resultPropertyValueCollection.Cast<object>().ToArray<object>();
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x17001EA0 RID: 7840
		// (get) Token: 0x06006B11 RID: 27409 RVA: 0x001710B4 File Offset: 0x0016F2B4
		public override IList<string> AttributeNames
		{
			get
			{
				if (this.attributeNames == null)
				{
					this.attributeNames = new string[this.result.Properties.PropertyNames.Count];
					int num = 0;
					foreach (object obj in this.result.Properties.PropertyNames)
					{
						string text = (string)obj;
						this.attributeNames[num++] = text;
					}
				}
				return this.attributeNames;
			}
		}

		// Token: 0x04003B8C RID: 15244
		private readonly SearchResult result;

		// Token: 0x04003B8D RID: 15245
		private IList<string> attributeNames;
	}
}
