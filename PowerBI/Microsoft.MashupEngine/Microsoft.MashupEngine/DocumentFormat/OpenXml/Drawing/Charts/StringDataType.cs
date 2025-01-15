using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200257F RID: 9599
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(StringPoint))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(PointCount))]
	internal abstract class StringDataType : OpenXmlCompositeElement
	{
		// Token: 0x06011E9F RID: 73375 RVA: 0x002F39A8 File Offset: 0x002F1BA8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "ptCount" == name)
			{
				return new PointCount();
			}
			if (11 == namespaceId && "pt" == name)
			{
				return new StringPoint();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700561B RID: 22043
		// (get) Token: 0x06011EA0 RID: 73376 RVA: 0x002F39FE File Offset: 0x002F1BFE
		internal override string[] ElementTagNames
		{
			get
			{
				return StringDataType.eleTagNames;
			}
		}

		// Token: 0x1700561C RID: 22044
		// (get) Token: 0x06011EA1 RID: 73377 RVA: 0x002F3A05 File Offset: 0x002F1C05
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return StringDataType.eleNamespaceIds;
			}
		}

		// Token: 0x1700561D RID: 22045
		// (get) Token: 0x06011EA2 RID: 73378 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700561E RID: 22046
		// (get) Token: 0x06011EA3 RID: 73379 RVA: 0x002F3A0C File Offset: 0x002F1C0C
		// (set) Token: 0x06011EA4 RID: 73380 RVA: 0x002F3A15 File Offset: 0x002F1C15
		public PointCount PointCount
		{
			get
			{
				return base.GetElement<PointCount>(0);
			}
			set
			{
				base.SetElement<PointCount>(0, value);
			}
		}

		// Token: 0x06011EA5 RID: 73381 RVA: 0x00293ECF File Offset: 0x002920CF
		protected StringDataType()
		{
		}

		// Token: 0x06011EA6 RID: 73382 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected StringDataType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011EA7 RID: 73383 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected StringDataType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011EA8 RID: 73384 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected StringDataType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04007D39 RID: 32057
		private static readonly string[] eleTagNames = new string[] { "ptCount", "pt", "extLst" };

		// Token: 0x04007D3A RID: 32058
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11 };
	}
}
