using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000032 RID: 50
	internal sealed class ResultTable
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00005574 File Offset: 0x00003774
		internal ResultTable(string id, IList<Field> fields, bool isReusable)
		{
			this._id = id;
			this._fields = fields;
			this._isReusable = isReusable;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00005591 File Offset: 0x00003791
		internal string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00005599 File Offset: 0x00003799
		internal IList<Field> Fields
		{
			get
			{
				return this._fields;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000055A1 File Offset: 0x000037A1
		internal bool IsReusable
		{
			get
			{
				return this._isReusable;
			}
		}

		// Token: 0x040000DE RID: 222
		private readonly string _id;

		// Token: 0x040000DF RID: 223
		private readonly IList<Field> _fields;

		// Token: 0x040000E0 RID: 224
		private readonly bool _isReusable;
	}
}
