using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BDB RID: 3035
	public class SortOption
	{
		// Token: 0x17001984 RID: 6532
		// (get) Token: 0x060052CC RID: 21196 RVA: 0x00117DED File Offset: 0x00115FED
		// (set) Token: 0x060052CD RID: 21197 RVA: 0x00117DF5 File Offset: 0x00115FF5
		public PropertyDefinitionBase PropertyDefinition { get; private set; }

		// Token: 0x17001985 RID: 6533
		// (get) Token: 0x060052CE RID: 21198 RVA: 0x00117DFE File Offset: 0x00115FFE
		// (set) Token: 0x060052CF RID: 21199 RVA: 0x00117E06 File Offset: 0x00116006
		public SortDirection SortDirection { get; private set; }

		// Token: 0x060052D0 RID: 21200 RVA: 0x00117E0F File Offset: 0x0011600F
		public SortOption(PropertyDefinitionBase property, SortDirection sortDirection)
		{
			this.PropertyDefinition = property;
			this.SortDirection = sortDirection;
		}

		// Token: 0x060052D1 RID: 21201 RVA: 0x00117E28 File Offset: 0x00116028
		public string GetSerializedString()
		{
			return this.PropertyDefinition.GetName() + this.SortDirection.ToString();
		}
	}
}
