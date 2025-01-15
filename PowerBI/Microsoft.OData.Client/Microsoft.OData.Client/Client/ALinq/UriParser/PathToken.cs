using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000130 RID: 304
	public abstract class PathToken : QueryToken
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000C81 RID: 3201
		// (set) Token: 0x06000C82 RID: 3202
		public abstract QueryToken NextToken { get; set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000C83 RID: 3203
		public abstract string Identifier { get; }

		// Token: 0x06000C84 RID: 3204 RVA: 0x0002D064 File Offset: 0x0002B264
		public override bool Equals(object obj)
		{
			PathToken pathToken = obj as PathToken;
			return pathToken != null && this.Identifier.Equals(pathToken.Identifier) && ((this.NextToken == null && pathToken.NextToken == null) || this.NextToken.Equals(pathToken.NextToken));
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0002D0B8 File Offset: 0x0002B2B8
		public override int GetHashCode()
		{
			int num = this.Identifier.GetHashCode();
			if (this.NextToken != null)
			{
				num = PathToken.Combine(num, this.NextToken.GetHashCode());
			}
			return num;
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0002D0EC File Offset: 0x0002B2EC
		private static int Combine(int h1, int h2)
		{
			uint num = (uint)((h1 << 5) | (int)((uint)h1 >> 27));
			return (int)((num + (uint)h1) ^ (uint)h2);
		}
	}
}
