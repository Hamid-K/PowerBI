using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028FE RID: 10494
	[ChildElementInfo(typeof(NameList))]
	[ChildElementInfo(typeof(Corporate))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class NameOrCorporateType : OpenXmlCompositeElement
	{
		// Token: 0x06014AF7 RID: 84727 RVA: 0x0031552D File Offset: 0x0031372D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (9 == namespaceId && "NameList" == name)
			{
				return new NameList();
			}
			if (9 == namespaceId && "Corporate" == name)
			{
				return new Corporate();
			}
			return null;
		}

		// Token: 0x170069A3 RID: 27043
		// (get) Token: 0x06014AF8 RID: 84728 RVA: 0x00315560 File Offset: 0x00313760
		internal override string[] ElementTagNames
		{
			get
			{
				return NameOrCorporateType.eleTagNames;
			}
		}

		// Token: 0x170069A4 RID: 27044
		// (get) Token: 0x06014AF9 RID: 84729 RVA: 0x00315567 File Offset: 0x00313767
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NameOrCorporateType.eleNamespaceIds;
			}
		}

		// Token: 0x170069A5 RID: 27045
		// (get) Token: 0x06014AFA RID: 84730 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170069A6 RID: 27046
		// (get) Token: 0x06014AFB RID: 84731 RVA: 0x0031537F File Offset: 0x0031357F
		// (set) Token: 0x06014AFC RID: 84732 RVA: 0x00315388 File Offset: 0x00313588
		public NameList NameList
		{
			get
			{
				return base.GetElement<NameList>(0);
			}
			set
			{
				base.SetElement<NameList>(0, value);
			}
		}

		// Token: 0x170069A7 RID: 27047
		// (get) Token: 0x06014AFD RID: 84733 RVA: 0x0031556E File Offset: 0x0031376E
		// (set) Token: 0x06014AFE RID: 84734 RVA: 0x00315577 File Offset: 0x00313777
		public Corporate Corporate
		{
			get
			{
				return base.GetElement<Corporate>(1);
			}
			set
			{
				base.SetElement<Corporate>(1, value);
			}
		}

		// Token: 0x06014AFF RID: 84735 RVA: 0x00293ECF File Offset: 0x002920CF
		protected NameOrCorporateType()
		{
		}

		// Token: 0x06014B00 RID: 84736 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected NameOrCorporateType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014B01 RID: 84737 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected NameOrCorporateType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014B02 RID: 84738 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected NameOrCorporateType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04008F85 RID: 36741
		private static readonly string[] eleTagNames = new string[] { "NameList", "Corporate" };

		// Token: 0x04008F86 RID: 36742
		private static readonly byte[] eleNamespaceIds = new byte[] { 9, 9 };
	}
}
