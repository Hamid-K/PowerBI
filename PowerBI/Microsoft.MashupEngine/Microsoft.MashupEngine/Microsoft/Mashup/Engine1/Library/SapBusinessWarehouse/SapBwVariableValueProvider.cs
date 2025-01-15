using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x02000504 RID: 1284
	internal abstract class SapBwVariableValueProvider
	{
		// Token: 0x060029CF RID: 10703 RVA: 0x0007D28F File Offset: 0x0007B48F
		protected SapBwVariableValueProvider(ISapBwService service, SapBwMdxCube mdxCube, SapBwVariable variable, bool allowNonAssigned)
		{
			this.service = service;
			this.mdxCube = mdxCube;
			this.variable = variable;
			this.allowNonAssigned = allowNonAssigned;
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x060029D0 RID: 10704
		public abstract bool HasValues { get; }

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x060029D1 RID: 10705 RVA: 0x0007D2B4 File Offset: 0x0007B4B4
		public ISapBwService Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x060029D2 RID: 10706 RVA: 0x0007D2BC File Offset: 0x0007B4BC
		public SapBwMdxCube MdxCube
		{
			get
			{
				return this.mdxCube;
			}
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x060029D3 RID: 10707 RVA: 0x0007D2C4 File Offset: 0x0007B4C4
		public SapBwVariable Variable
		{
			get
			{
				return this.variable;
			}
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x060029D4 RID: 10708 RVA: 0x0007D2CC File Offset: 0x0007B4CC
		public bool AllowNonAssigned
		{
			get
			{
				return this.allowNonAssigned;
			}
		}

		// Token: 0x060029D5 RID: 10709
		public abstract IEnumerable<IValueReference> GetValues();

		// Token: 0x060029D6 RID: 10710 RVA: 0x0007D2D4 File Offset: 0x0007B4D4
		public virtual IEnumerable<IValueReference> GetValues(long skip)
		{
			return this.GetValues().SkipLong(skip);
		}

		// Token: 0x04001230 RID: 4656
		public const int FirstPageStart = 0;

		// Token: 0x04001231 RID: 4657
		public const int PageSize = 2500;

		// Token: 0x04001232 RID: 4658
		protected const int maxMembersFromTable = 2500;

		// Token: 0x04001233 RID: 4659
		private readonly ISapBwService service;

		// Token: 0x04001234 RID: 4660
		private readonly SapBwMdxCube mdxCube;

		// Token: 0x04001235 RID: 4661
		private readonly SapBwVariable variable;

		// Token: 0x04001236 RID: 4662
		private readonly bool allowNonAssigned;
	}
}
