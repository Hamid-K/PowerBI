using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x0200019C RID: 412
	internal abstract class XmlElementValue
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x00013BEE File Offset: 0x00011DEE
		internal XmlElementValue(string elementName, CsdlLocation elementLocation)
		{
			this.Name = elementName;
			this.Location = elementLocation;
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00013C04 File Offset: 0x00011E04
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x00013C0C File Offset: 0x00011E0C
		internal string Name { get; private set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00013C15 File Offset: 0x00011E15
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x00013C1D File Offset: 0x00011E1D
		internal CsdlLocation Location { get; private set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000804 RID: 2052
		internal abstract object UntypedValue { get; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000805 RID: 2053
		internal abstract bool IsUsed { get; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00013C26 File Offset: 0x00011E26
		internal virtual bool IsText
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00013C29 File Offset: 0x00011E29
		internal virtual string TextValue
		{
			get
			{
				return this.ValueAs<string>();
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00013C31 File Offset: 0x00011E31
		internal virtual TValue ValueAs<TValue>() where TValue : class
		{
			return this.UntypedValue as TValue;
		}
	}
}
