using System;

namespace Microsoft.OData
{
	// Token: 0x020000AD RID: 173
	public sealed class ODataProperty : ODataPropertyInfo
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x000124E9 File Offset: 0x000106E9
		// (set) Token: 0x06000792 RID: 1938 RVA: 0x00012500 File Offset: 0x00010700
		public object Value
		{
			get
			{
				if (this.ODataValue == null)
				{
					return null;
				}
				return this.ODataValue.FromODataValue();
			}
			set
			{
				this.ODataValue = value.ToODataValue();
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0001250E File Offset: 0x0001070E
		// (set) Token: 0x06000794 RID: 1940 RVA: 0x00012516 File Offset: 0x00010716
		internal ODataValue ODataValue { get; private set; }
	}
}
