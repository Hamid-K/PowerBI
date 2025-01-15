using System;
using Microsoft.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x02000797 RID: 1943
	internal class ODataEnumValueWrapper : IODataEnumValueWrapper
	{
		// Token: 0x060038FA RID: 14586 RVA: 0x000B78D5 File Offset: 0x000B5AD5
		public ODataEnumValueWrapper(ODataEnumValue enumValue)
		{
			this.enumValue = enumValue;
		}

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x060038FB RID: 14587 RVA: 0x000B78E4 File Offset: 0x000B5AE4
		public string TypeName
		{
			get
			{
				return this.enumValue.TypeName;
			}
		}

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x060038FC RID: 14588 RVA: 0x000B78F1 File Offset: 0x000B5AF1
		public string Value
		{
			get
			{
				return this.enumValue.Value;
			}
		}

		// Token: 0x04001D65 RID: 7525
		private readonly ODataEnumValue enumValue;
	}
}
