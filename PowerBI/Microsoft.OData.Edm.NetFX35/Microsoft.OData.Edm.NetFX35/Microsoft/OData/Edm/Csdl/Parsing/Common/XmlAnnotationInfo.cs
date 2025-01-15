using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x02000196 RID: 406
	internal class XmlAnnotationInfo
	{
		// Token: 0x060007D1 RID: 2001 RVA: 0x000134F4 File Offset: 0x000116F4
		internal XmlAnnotationInfo(CsdlLocation location, string namespaceName, string name, string value, bool isAttribute)
		{
			this.Location = location;
			this.NamespaceName = namespaceName;
			this.Name = name;
			this.Value = value;
			this.IsAttribute = isAttribute;
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00013521 File Offset: 0x00011721
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x00013529 File Offset: 0x00011729
		internal string NamespaceName { get; private set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00013532 File Offset: 0x00011732
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0001353A File Offset: 0x0001173A
		internal string Name { get; private set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00013543 File Offset: 0x00011743
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0001354B File Offset: 0x0001174B
		internal CsdlLocation Location { get; private set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00013554 File Offset: 0x00011754
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x0001355C File Offset: 0x0001175C
		internal string Value { get; private set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00013565 File Offset: 0x00011765
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0001356D File Offset: 0x0001176D
		internal bool IsAttribute { get; private set; }
	}
}
