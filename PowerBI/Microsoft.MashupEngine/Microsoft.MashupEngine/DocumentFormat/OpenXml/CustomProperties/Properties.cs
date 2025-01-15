using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.CustomProperties
{
	// Token: 0x02002908 RID: 10504
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CustomDocumentProperty))]
	internal class Properties : OpenXmlPartRootElement
	{
		// Token: 0x17006A0D RID: 27149
		// (get) Token: 0x06014BE1 RID: 84961 RVA: 0x00316653 File Offset: 0x00314853
		public override string LocalName
		{
			get
			{
				return "Properties";
			}
		}

		// Token: 0x17006A0E RID: 27150
		// (get) Token: 0x06014BE2 RID: 84962 RVA: 0x0000244F File Offset: 0x0000064F
		internal override byte NamespaceId
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17006A0F RID: 27151
		// (get) Token: 0x06014BE3 RID: 84963 RVA: 0x0031665A File Offset: 0x0031485A
		internal override int ElementTypeId
		{
			get
			{
				return 10837;
			}
		}

		// Token: 0x06014BE4 RID: 84964 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014BE5 RID: 84965 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Properties(CustomFilePropertiesPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06014BE6 RID: 84966 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(CustomFilePropertiesPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006A10 RID: 27152
		// (get) Token: 0x06014BE7 RID: 84967 RVA: 0x00316661 File Offset: 0x00314861
		// (set) Token: 0x06014BE8 RID: 84968 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public CustomFilePropertiesPart CustomFilePropertiesPart
		{
			get
			{
				return base.OpenXmlPart as CustomFilePropertiesPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06014BE9 RID: 84969 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Properties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014BEA RID: 84970 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Properties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014BEB RID: 84971 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Properties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014BEC RID: 84972 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Properties()
		{
		}

		// Token: 0x06014BED RID: 84973 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(CustomFilePropertiesPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06014BEE RID: 84974 RVA: 0x0031666E File Offset: 0x0031486E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (4 == namespaceId && "property" == name)
			{
				return new CustomDocumentProperty();
			}
			return null;
		}

		// Token: 0x06014BEF RID: 84975 RVA: 0x00316688 File Offset: 0x00314888
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Properties>(deep);
		}

		// Token: 0x04008FBA RID: 36794
		private const string tagName = "Properties";

		// Token: 0x04008FBB RID: 36795
		private const byte tagNsId = 4;

		// Token: 0x04008FBC RID: 36796
		internal const int ElementTypeIdConst = 10837;
	}
}
