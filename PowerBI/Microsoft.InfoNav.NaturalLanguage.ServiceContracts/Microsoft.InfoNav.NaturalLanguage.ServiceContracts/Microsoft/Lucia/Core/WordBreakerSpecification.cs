using System;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200014D RID: 333
	[ImmutableObject(true)]
	public sealed class WordBreakerSpecification : PoolObjectSpecification
	{
		// Token: 0x0600069A RID: 1690 RVA: 0x0000B682 File Offset: 0x00009882
		public WordBreakerSpecification(int minPoolSize, int maxPoolSize, string dllLocation, Guid classId)
			: base(minPoolSize, maxPoolSize)
		{
			Contract.CheckNonEmpty(dllLocation, "We must have a dll filename to load");
			Contract.Check(classId != Guid.Empty, "We must have a class id");
			this._dllLocation = dllLocation;
			this._classId = classId;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0000B6BC File Offset: 0x000098BC
		public string DllLocation
		{
			get
			{
				return this._dllLocation;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0000B6C4 File Offset: 0x000098C4
		public Guid ClassId
		{
			get
			{
				return this._classId;
			}
		}

		// Token: 0x04000661 RID: 1633
		private readonly Guid _classId;

		// Token: 0x04000662 RID: 1634
		private readonly string _dllLocation;
	}
}
