using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001C6 RID: 454
	public abstract class PathToken : QueryToken
	{
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060014EB RID: 5355
		// (set) Token: 0x060014EC RID: 5356
		public abstract QueryToken NextToken { get; set; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060014ED RID: 5357
		public abstract string Identifier { get; }

		// Token: 0x060014EE RID: 5358 RVA: 0x0003C1BC File Offset: 0x0003A3BC
		public override bool Equals(object obj)
		{
			PathToken pathToken = obj as PathToken;
			return pathToken != null && this.Identifier.Equals(pathToken.Identifier) && ((this.NextToken == null && pathToken.NextToken == null) || this.NextToken.Equals(pathToken.NextToken));
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0003C210 File Offset: 0x0003A410
		public override int GetHashCode()
		{
			int num = this.Identifier.GetHashCode();
			if (this.NextToken != null)
			{
				num = PathToken.Combine(num, this.NextToken.GetHashCode());
			}
			return num;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0003C244 File Offset: 0x0003A444
		private static int Combine(int h1, int h2)
		{
			uint num = (uint)((h1 << 5) | (int)((uint)h1 >> 27));
			return (int)((num + (uint)h1) ^ (uint)h2);
		}
	}
}
