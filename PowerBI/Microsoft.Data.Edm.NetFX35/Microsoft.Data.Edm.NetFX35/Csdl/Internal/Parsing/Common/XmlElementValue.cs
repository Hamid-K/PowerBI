using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Common
{
	// Token: 0x0200015B RID: 347
	internal abstract class XmlElementValue
	{
		// Token: 0x060006D2 RID: 1746 RVA: 0x0001181A File Offset: 0x0000FA1A
		internal XmlElementValue(string elementName, CsdlLocation elementLocation)
		{
			this.Name = elementName;
			this.Location = elementLocation;
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00011830 File Offset: 0x0000FA30
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x00011838 File Offset: 0x0000FA38
		internal string Name { get; private set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00011841 File Offset: 0x0000FA41
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x00011849 File Offset: 0x0000FA49
		internal CsdlLocation Location { get; private set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060006D7 RID: 1751
		internal abstract object UntypedValue { get; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060006D8 RID: 1752
		internal abstract bool IsUsed { get; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00011852 File Offset: 0x0000FA52
		internal virtual bool IsText
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00011855 File Offset: 0x0000FA55
		internal virtual string TextValue
		{
			get
			{
				return this.ValueAs<string>();
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001185D File Offset: 0x0000FA5D
		internal virtual TValue ValueAs<TValue>() where TValue : class
		{
			return this.UntypedValue as TValue;
		}
	}
}
