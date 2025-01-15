using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001C0 RID: 448
	public sealed class InnerPathToken : PathToken
	{
		// Token: 0x060014C3 RID: 5315 RVA: 0x0003BFFA File Offset: 0x0003A1FA
		public InnerPathToken(string identifier, QueryToken nextToken, IEnumerable<NamedValue> namedValues)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
			this.namedValues = ((namedValues == null) ? null : new ReadOnlyEnumerableForUriParser<NamedValue>(namedValues));
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x00038E6D File Offset: 0x0003706D
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.InnerPath;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x0003C02E File Offset: 0x0003A22E
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x0003C036 File Offset: 0x0003A236
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x0003C03E File Offset: 0x0003A23E
		public override QueryToken NextToken
		{
			get
			{
				return this.nextToken;
			}
			set
			{
				this.nextToken = value;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x0003C047 File Offset: 0x0003A247
		public IEnumerable<NamedValue> NamedValues
		{
			get
			{
				return this.namedValues;
			}
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0003C04F File Offset: 0x0003A24F
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000912 RID: 2322
		private readonly string identifier;

		// Token: 0x04000913 RID: 2323
		private readonly IEnumerable<NamedValue> namedValues;

		// Token: 0x04000914 RID: 2324
		private QueryToken nextToken;
	}
}
