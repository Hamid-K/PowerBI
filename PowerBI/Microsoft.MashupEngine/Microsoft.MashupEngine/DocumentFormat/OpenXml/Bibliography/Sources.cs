using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028B6 RID: 10422
	[ChildElementInfo(typeof(Source))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Sources : OpenXmlPartRootElement
	{
		// Token: 0x170068C5 RID: 26821
		// (get) Token: 0x06014893 RID: 84115 RVA: 0x003146EC File Offset: 0x003128EC
		public override string LocalName
		{
			get
			{
				return "Sources";
			}
		}

		// Token: 0x170068C6 RID: 26822
		// (get) Token: 0x06014894 RID: 84116 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068C7 RID: 26823
		// (get) Token: 0x06014895 RID: 84117 RVA: 0x003146F3 File Offset: 0x003128F3
		internal override int ElementTypeId
		{
			get
			{
				return 10758;
			}
		}

		// Token: 0x06014896 RID: 84118 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170068C8 RID: 26824
		// (get) Token: 0x06014897 RID: 84119 RVA: 0x003146FA File Offset: 0x003128FA
		internal override string[] AttributeTagNames
		{
			get
			{
				return Sources.attributeTagNames;
			}
		}

		// Token: 0x170068C9 RID: 26825
		// (get) Token: 0x06014898 RID: 84120 RVA: 0x00314701 File Offset: 0x00312901
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Sources.attributeNamespaceIds;
			}
		}

		// Token: 0x170068CA RID: 26826
		// (get) Token: 0x06014899 RID: 84121 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601489A RID: 84122 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "SelectedStyle")]
		public StringValue SelectedStyle
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170068CB RID: 26827
		// (get) Token: 0x0601489B RID: 84123 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601489C RID: 84124 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "StyleName")]
		public StringValue StyleName
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170068CC RID: 26828
		// (get) Token: 0x0601489D RID: 84125 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601489E RID: 84126 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "URI")]
		public StringValue Uri
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601489F RID: 84127 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Sources()
		{
		}

		// Token: 0x060148A0 RID: 84128 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Sources(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060148A1 RID: 84129 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Sources(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060148A2 RID: 84130 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Sources(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060148A3 RID: 84131 RVA: 0x00314708 File Offset: 0x00312908
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (9 == namespaceId && "Source" == name)
			{
				return new Source();
			}
			return null;
		}

		// Token: 0x060148A4 RID: 84132 RVA: 0x00314724 File Offset: 0x00312924
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "SelectedStyle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "StyleName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "URI" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060148A5 RID: 84133 RVA: 0x0031477B File Offset: 0x0031297B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Sources>(deep);
		}

		// Token: 0x060148A6 RID: 84134 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Sources(CustomXmlPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x060148A7 RID: 84135 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(CustomXmlPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x060148A8 RID: 84136 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(CustomXmlPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x060148A9 RID: 84137 RVA: 0x00314784 File Offset: 0x00312984
		// Note: this type is marked as 'beforefieldinit'.
		static Sources()
		{
			byte[] array = new byte[3];
			Sources.attributeNamespaceIds = array;
		}

		// Token: 0x04008EAC RID: 36524
		private const string tagName = "Sources";

		// Token: 0x04008EAD RID: 36525
		private const byte tagNsId = 9;

		// Token: 0x04008EAE RID: 36526
		internal const int ElementTypeIdConst = 10758;

		// Token: 0x04008EAF RID: 36527
		private static string[] attributeTagNames = new string[] { "SelectedStyle", "StyleName", "URI" };

		// Token: 0x04008EB0 RID: 36528
		private static byte[] attributeNamespaceIds;
	}
}
