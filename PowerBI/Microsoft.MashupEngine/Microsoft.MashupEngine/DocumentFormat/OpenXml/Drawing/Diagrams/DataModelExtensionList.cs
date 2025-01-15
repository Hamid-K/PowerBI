using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002691 RID: 9873
	[ChildElementInfo(typeof(DataModelExtension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DataModelExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17005D20 RID: 23840
		// (get) Token: 0x06012E55 RID: 77397 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17005D21 RID: 23841
		// (get) Token: 0x06012E56 RID: 77398 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005D22 RID: 23842
		// (get) Token: 0x06012E57 RID: 77399 RVA: 0x003007BA File Offset: 0x002FE9BA
		internal override int ElementTypeId
		{
			get
			{
				return 10688;
			}
		}

		// Token: 0x06012E58 RID: 77400 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012E59 RID: 77401 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataModelExtensionList()
		{
		}

		// Token: 0x06012E5A RID: 77402 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataModelExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012E5B RID: 77403 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataModelExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012E5C RID: 77404 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataModelExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012E5D RID: 77405 RVA: 0x003007C1 File Offset: 0x002FE9C1
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new DataModelExtension();
			}
			return null;
		}

		// Token: 0x06012E5E RID: 77406 RVA: 0x003007DC File Offset: 0x002FE9DC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataModelExtensionList>(deep);
		}

		// Token: 0x0400821D RID: 33309
		private const string tagName = "extLst";

		// Token: 0x0400821E RID: 33310
		private const byte tagNsId = 14;

		// Token: 0x0400821F RID: 33311
		internal const int ElementTypeIdConst = 10688;
	}
}
