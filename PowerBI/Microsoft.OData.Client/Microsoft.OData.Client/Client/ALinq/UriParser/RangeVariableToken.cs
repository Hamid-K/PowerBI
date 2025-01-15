using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000135 RID: 309
	public sealed class RangeVariableToken : QueryToken
	{
		// Token: 0x06000CA4 RID: 3236 RVA: 0x0002D287 File Offset: 0x0002B487
		public RangeVariableToken(string name)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "visitor");
			this.name = name;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0002D2A2 File Offset: 0x0002B4A2
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.RangeVariable;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0002D2A6 File Offset: 0x0002B4A6
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0002D2AE File Offset: 0x0002B4AE
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0002D2B8 File Offset: 0x0002B4B8
		public override bool Equals(object obj)
		{
			RangeVariableToken rangeVariableToken = obj as RangeVariableToken;
			return rangeVariableToken != null && this.Name.Equals(rangeVariableToken.Name);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002D2E2 File Offset: 0x0002B4E2
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x040006A7 RID: 1703
		private readonly string name;
	}
}
