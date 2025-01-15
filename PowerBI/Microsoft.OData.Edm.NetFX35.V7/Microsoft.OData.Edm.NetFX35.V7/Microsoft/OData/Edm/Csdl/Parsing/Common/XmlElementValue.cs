using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001B8 RID: 440
	internal abstract class XmlElementValue
	{
		// Token: 0x06000C55 RID: 3157 RVA: 0x00023713 File Offset: 0x00021913
		internal XmlElementValue(string elementName, CsdlLocation elementLocation)
		{
			this.Name = elementName;
			this.Location = elementLocation;
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00023729 File Offset: 0x00021929
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x00023731 File Offset: 0x00021931
		internal string Name { get; private set; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0002373A File Offset: 0x0002193A
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x00023742 File Offset: 0x00021942
		internal CsdlLocation Location { get; private set; }

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000C5A RID: 3162
		internal abstract object UntypedValue { get; }

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000C5B RID: 3163
		internal abstract bool IsUsed { get; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00008EC3 File Offset: 0x000070C3
		internal virtual bool IsText
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0002374B File Offset: 0x0002194B
		internal virtual string TextValue
		{
			get
			{
				return this.ValueAs<string>();
			}
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00023753 File Offset: 0x00021953
		internal virtual TValue ValueAs<TValue>() where TValue : class
		{
			return this.UntypedValue as TValue;
		}
	}
}
