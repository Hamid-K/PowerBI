using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200017D RID: 381
	public sealed class RangeVariableToken : QueryToken
	{
		// Token: 0x06000FC6 RID: 4038 RVA: 0x0002C1FE File Offset: 0x0002A3FE
		public RangeVariableToken(string name)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "visitor");
			this.name = name;
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00028C88 File Offset: 0x00026E88
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.RangeVariable;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x0002C219 File Offset: 0x0002A419
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0002C221 File Offset: 0x0002A421
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040007F9 RID: 2041
		private readonly string name;
	}
}
