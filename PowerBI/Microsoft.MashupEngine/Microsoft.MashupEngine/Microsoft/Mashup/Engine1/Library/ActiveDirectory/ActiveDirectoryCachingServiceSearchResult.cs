using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FBE RID: 4030
	internal class ActiveDirectoryCachingServiceSearchResult : ActiveDirectoryServiceSearchResult
	{
		// Token: 0x060069DB RID: 27099 RVA: 0x0016C2FD File Offset: 0x0016A4FD
		public ActiveDirectoryCachingServiceSearchResult(Dictionary<string, object[]> dictionary)
		{
			this.properties = dictionary;
		}

		// Token: 0x17001E68 RID: 7784
		// (get) Token: 0x060069DC RID: 27100 RVA: 0x0016C30C File Offset: 0x0016A50C
		public override IList<string> AttributeNames
		{
			get
			{
				if (this.attributeNames == null)
				{
					this.attributeNames = this.properties.Keys.ToArray<string>();
				}
				return this.attributeNames;
			}
		}

		// Token: 0x060069DD RID: 27101 RVA: 0x0016C332 File Offset: 0x0016A532
		public override bool TryGetAttribute(string attributeName, out object[] value)
		{
			return this.properties.TryGetValue(attributeName, out value);
		}

		// Token: 0x04003AAB RID: 15019
		private readonly Dictionary<string, object[]> properties;

		// Token: 0x04003AAC RID: 15020
		private IList<string> attributeNames;
	}
}
