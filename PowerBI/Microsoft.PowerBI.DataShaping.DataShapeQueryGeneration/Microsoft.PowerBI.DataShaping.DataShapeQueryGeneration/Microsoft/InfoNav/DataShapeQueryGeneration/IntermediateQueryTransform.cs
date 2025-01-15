using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000D5 RID: 213
	internal sealed class IntermediateQueryTransform
	{
		// Token: 0x0600077C RID: 1916 RVA: 0x0001C43C File Offset: 0x0001A63C
		internal IntermediateQueryTransform(Identifier id, string queryName, string algorithm, IntermediateQueryTransformInput input, IntermediateQueryTransformOutput output)
		{
			this._id = id;
			this._queryName = queryName;
			this._algorithm = algorithm;
			this._input = input;
			this._output = output;
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0001C469 File Offset: 0x0001A669
		internal Identifier Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0001C471 File Offset: 0x0001A671
		internal string QueryName
		{
			get
			{
				return this._queryName;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0001C479 File Offset: 0x0001A679
		internal string Algorithm
		{
			get
			{
				return this._algorithm;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x0001C481 File Offset: 0x0001A681
		internal IntermediateQueryTransformInput Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x0001C489 File Offset: 0x0001A689
		internal IntermediateQueryTransformOutput Output
		{
			get
			{
				return this._output;
			}
		}

		// Token: 0x040003E2 RID: 994
		private readonly Identifier _id;

		// Token: 0x040003E3 RID: 995
		private readonly string _queryName;

		// Token: 0x040003E4 RID: 996
		private readonly string _algorithm;

		// Token: 0x040003E5 RID: 997
		private readonly IntermediateQueryTransformInput _input;

		// Token: 0x040003E6 RID: 998
		private readonly IntermediateQueryTransformOutput _output;
	}
}
