using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028EF RID: 10479
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NameList))]
	internal abstract class NameType : OpenXmlCompositeElement
	{
		// Token: 0x06014A6E RID: 84590 RVA: 0x00315356 File Offset: 0x00313556
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (9 == namespaceId && "NameList" == name)
			{
				return new NameList();
			}
			return null;
		}

		// Token: 0x17006975 RID: 26997
		// (get) Token: 0x06014A6F RID: 84591 RVA: 0x00315371 File Offset: 0x00313571
		internal override string[] ElementTagNames
		{
			get
			{
				return NameType.eleTagNames;
			}
		}

		// Token: 0x17006976 RID: 26998
		// (get) Token: 0x06014A70 RID: 84592 RVA: 0x00315378 File Offset: 0x00313578
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NameType.eleNamespaceIds;
			}
		}

		// Token: 0x17006977 RID: 26999
		// (get) Token: 0x06014A71 RID: 84593 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006978 RID: 27000
		// (get) Token: 0x06014A72 RID: 84594 RVA: 0x0031537F File Offset: 0x0031357F
		// (set) Token: 0x06014A73 RID: 84595 RVA: 0x00315388 File Offset: 0x00313588
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

		// Token: 0x06014A74 RID: 84596 RVA: 0x00293ECF File Offset: 0x002920CF
		protected NameType()
		{
		}

		// Token: 0x06014A75 RID: 84597 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected NameType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014A76 RID: 84598 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected NameType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014A77 RID: 84599 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected NameType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04008F59 RID: 36697
		private static readonly string[] eleTagNames = new string[] { "NameList" };

		// Token: 0x04008F5A RID: 36698
		private static readonly byte[] eleNamespaceIds = new byte[] { 9 };
	}
}
