using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001C8 RID: 456
	public sealed class RangeVariableToken : QueryToken
	{
		// Token: 0x060014F6 RID: 5366 RVA: 0x0003C274 File Offset: 0x0003A474
		public RangeVariableToken(string name)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "visitor");
			this.name = name;
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x000385BC File Offset: 0x000367BC
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.RangeVariable;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0003C28F File Offset: 0x0003A48F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0003C297 File Offset: 0x0003A497
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0003C2A0 File Offset: 0x0003A4A0
		public override bool Equals(object obj)
		{
			RangeVariableToken rangeVariableToken = obj as RangeVariableToken;
			return rangeVariableToken != null && this.Name.Equals(rangeVariableToken.Name);
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0003C2CA File Offset: 0x0003A4CA
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x04000922 RID: 2338
		private readonly string name;
	}
}
