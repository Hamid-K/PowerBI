using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200257A RID: 9594
	[ChildElementInfo(typeof(FormatCode))]
	[ChildElementInfo(typeof(PointCount))]
	[ChildElementInfo(typeof(NumericPoint))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class NumberDataType : OpenXmlCompositeElement
	{
		// Token: 0x06011E57 RID: 73303 RVA: 0x002F36C0 File Offset: 0x002F18C0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "formatCode" == name)
			{
				return new FormatCode();
			}
			if (11 == namespaceId && "ptCount" == name)
			{
				return new PointCount();
			}
			if (11 == namespaceId && "pt" == name)
			{
				return new NumericPoint();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170055FD RID: 22013
		// (get) Token: 0x06011E58 RID: 73304 RVA: 0x002F372E File Offset: 0x002F192E
		internal override string[] ElementTagNames
		{
			get
			{
				return NumberDataType.eleTagNames;
			}
		}

		// Token: 0x170055FE RID: 22014
		// (get) Token: 0x06011E59 RID: 73305 RVA: 0x002F3735 File Offset: 0x002F1935
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NumberDataType.eleNamespaceIds;
			}
		}

		// Token: 0x170055FF RID: 22015
		// (get) Token: 0x06011E5A RID: 73306 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005600 RID: 22016
		// (get) Token: 0x06011E5B RID: 73307 RVA: 0x002F373C File Offset: 0x002F193C
		// (set) Token: 0x06011E5C RID: 73308 RVA: 0x002F3745 File Offset: 0x002F1945
		public FormatCode FormatCode
		{
			get
			{
				return base.GetElement<FormatCode>(0);
			}
			set
			{
				base.SetElement<FormatCode>(0, value);
			}
		}

		// Token: 0x17005601 RID: 22017
		// (get) Token: 0x06011E5D RID: 73309 RVA: 0x002F374F File Offset: 0x002F194F
		// (set) Token: 0x06011E5E RID: 73310 RVA: 0x002F3758 File Offset: 0x002F1958
		public PointCount PointCount
		{
			get
			{
				return base.GetElement<PointCount>(1);
			}
			set
			{
				base.SetElement<PointCount>(1, value);
			}
		}

		// Token: 0x06011E5F RID: 73311 RVA: 0x00293ECF File Offset: 0x002920CF
		protected NumberDataType()
		{
		}

		// Token: 0x06011E60 RID: 73312 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected NumberDataType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011E61 RID: 73313 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected NumberDataType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011E62 RID: 73314 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected NumberDataType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04007D25 RID: 32037
		private static readonly string[] eleTagNames = new string[] { "formatCode", "ptCount", "pt", "extLst" };

		// Token: 0x04007D26 RID: 32038
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11 };
	}
}
