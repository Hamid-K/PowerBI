using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200004F RID: 79
	internal abstract class Scope
	{
		// Token: 0x0600020A RID: 522 RVA: 0x00006305 File Offset: 0x00004505
		protected Scope(string id)
		{
			this._id = id;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00006314 File Offset: 0x00004514
		internal string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000631C File Offset: 0x0000451C
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00006324 File Offset: 0x00004524
		internal IList<int> ApplicableLimits
		{
			get
			{
				return this._applicableLimits;
			}
			set
			{
				this._applicableLimits = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000632D File Offset: 0x0000452D
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00006335 File Offset: 0x00004535
		internal IList<int> ApplicableWindows
		{
			get
			{
				return this._applicableWindows;
			}
			set
			{
				this._applicableWindows = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000633E File Offset: 0x0000453E
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00006346 File Offset: 0x00004546
		internal IList<int> WithinLimits
		{
			get
			{
				return this._withinLimits;
			}
			set
			{
				this._withinLimits = value;
			}
		}

		// Token: 0x0400013D RID: 317
		private readonly string _id;

		// Token: 0x0400013E RID: 318
		private IList<int> _applicableLimits;

		// Token: 0x0400013F RID: 319
		private IList<int> _applicableWindows;

		// Token: 0x04000140 RID: 320
		private IList<int> _withinLimits;
	}
}
