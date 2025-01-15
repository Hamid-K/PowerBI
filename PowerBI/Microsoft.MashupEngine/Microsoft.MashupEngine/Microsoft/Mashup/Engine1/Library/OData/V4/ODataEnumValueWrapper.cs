using System;
using Microsoft.OData.Core;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000870 RID: 2160
	internal class ODataEnumValueWrapper : IODataEnumValueWrapper
	{
		// Token: 0x06003E36 RID: 15926 RVA: 0x000CB69A File Offset: 0x000C989A
		public ODataEnumValueWrapper(ODataEnumValue enumValue)
		{
			this.enumValue = enumValue;
		}

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x06003E37 RID: 15927 RVA: 0x000CB6A9 File Offset: 0x000C98A9
		public string TypeName
		{
			get
			{
				return this.enumValue.TypeName;
			}
		}

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x06003E38 RID: 15928 RVA: 0x000CB6B6 File Offset: 0x000C98B6
		public string Value
		{
			get
			{
				return this.enumValue.Value;
			}
		}

		// Token: 0x040020BA RID: 8378
		private readonly ODataEnumValue enumValue;
	}
}
