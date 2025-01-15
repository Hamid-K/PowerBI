using System;
using System.Collections.Generic;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000026 RID: 38
	public class SegmentQueryToken : QueryToken
	{
		// Token: 0x06000096 RID: 150 RVA: 0x000048BE File Offset: 0x00002ABE
		public SegmentQueryToken(string name, SegmentQueryToken parent, IEnumerable<NamedValue> namedValues)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			this.name = name;
			this.parent = parent;
			this.namedValues = ((namedValues == null) ? null : new ReadOnlyEnumerable<NamedValue>(namedValues));
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000048F1 File Offset: 0x00002AF1
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Segment;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000048F4 File Offset: 0x00002AF4
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000048FC File Offset: 0x00002AFC
		public SegmentQueryToken Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004904 File Offset: 0x00002B04
		public IEnumerable<NamedValue> NamedValues
		{
			get
			{
				return this.namedValues;
			}
		}

		// Token: 0x04000134 RID: 308
		private readonly string name;

		// Token: 0x04000135 RID: 309
		private readonly SegmentQueryToken parent;

		// Token: 0x04000136 RID: 310
		private readonly IEnumerable<NamedValue> namedValues;
	}
}
