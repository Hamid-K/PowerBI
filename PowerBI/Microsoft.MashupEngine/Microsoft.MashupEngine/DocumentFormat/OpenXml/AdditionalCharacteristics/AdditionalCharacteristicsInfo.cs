using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.AdditionalCharacteristics
{
	// Token: 0x02002905 RID: 10501
	[ChildElementInfo(typeof(Characteristic))]
	[GeneratedCode("DomGen", "2.0")]
	internal class AdditionalCharacteristicsInfo : OpenXmlPartRootElement
	{
		// Token: 0x17006A01 RID: 27137
		// (get) Token: 0x06014BC2 RID: 84930 RVA: 0x00316535 File Offset: 0x00314735
		public override string LocalName
		{
			get
			{
				return "additionalCharacteristics";
			}
		}

		// Token: 0x17006A02 RID: 27138
		// (get) Token: 0x06014BC3 RID: 84931 RVA: 0x000024ED File Offset: 0x000006ED
		internal override byte NamespaceId
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x17006A03 RID: 27139
		// (get) Token: 0x06014BC4 RID: 84932 RVA: 0x0031653C File Offset: 0x0031473C
		internal override int ElementTypeId
		{
			get
			{
				return 10756;
			}
		}

		// Token: 0x06014BC5 RID: 84933 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014BC6 RID: 84934 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public AdditionalCharacteristicsInfo()
		{
		}

		// Token: 0x06014BC7 RID: 84935 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public AdditionalCharacteristicsInfo(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014BC8 RID: 84936 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public AdditionalCharacteristicsInfo(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014BC9 RID: 84937 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public AdditionalCharacteristicsInfo(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014BCA RID: 84938 RVA: 0x00316543 File Offset: 0x00314743
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (8 == namespaceId && "characteristic" == name)
			{
				return new Characteristic();
			}
			return null;
		}

		// Token: 0x06014BCB RID: 84939 RVA: 0x0031655D File Offset: 0x0031475D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AdditionalCharacteristicsInfo>(deep);
		}

		// Token: 0x06014BCC RID: 84940 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal AdditionalCharacteristicsInfo(CustomXmlPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06014BCD RID: 84941 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(CustomXmlPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x06014BCE RID: 84942 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(CustomXmlPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x04008FAC RID: 36780
		private const string tagName = "additionalCharacteristics";

		// Token: 0x04008FAD RID: 36781
		private const byte tagNsId = 8;

		// Token: 0x04008FAE RID: 36782
		internal const int ElementTypeIdConst = 10756;
	}
}
