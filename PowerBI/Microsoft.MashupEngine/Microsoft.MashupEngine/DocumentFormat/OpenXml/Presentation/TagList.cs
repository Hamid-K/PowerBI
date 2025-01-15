using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A06 RID: 10758
	[ChildElementInfo(typeof(Tag))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TagList : OpenXmlPartRootElement
	{
		// Token: 0x17006F69 RID: 28521
		// (get) Token: 0x06015807 RID: 88071 RVA: 0x0031FEA6 File Offset: 0x0031E0A6
		public override string LocalName
		{
			get
			{
				return "tagLst";
			}
		}

		// Token: 0x17006F6A RID: 28522
		// (get) Token: 0x06015808 RID: 88072 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006F6B RID: 28523
		// (get) Token: 0x06015809 RID: 88073 RVA: 0x0031FEAD File Offset: 0x0031E0AD
		internal override int ElementTypeId
		{
			get
			{
				return 12185;
			}
		}

		// Token: 0x0601580A RID: 88074 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601580B RID: 88075 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal TagList(UserDefinedTagsPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x0601580C RID: 88076 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(UserDefinedTagsPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006F6C RID: 28524
		// (get) Token: 0x0601580D RID: 88077 RVA: 0x0031FEB4 File Offset: 0x0031E0B4
		// (set) Token: 0x0601580E RID: 88078 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public UserDefinedTagsPart UserDefinedTagsPart
		{
			get
			{
				return base.OpenXmlPart as UserDefinedTagsPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x0601580F RID: 88079 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public TagList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015810 RID: 88080 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public TagList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015811 RID: 88081 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public TagList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015812 RID: 88082 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public TagList()
		{
		}

		// Token: 0x06015813 RID: 88083 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(UserDefinedTagsPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06015814 RID: 88084 RVA: 0x0031FEC1 File Offset: 0x0031E0C1
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "tag" == name)
			{
				return new Tag();
			}
			return null;
		}

		// Token: 0x06015815 RID: 88085 RVA: 0x0031FEDC File Offset: 0x0031E0DC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TagList>(deep);
		}

		// Token: 0x0400939F RID: 37791
		private const string tagName = "tagLst";

		// Token: 0x040093A0 RID: 37792
		private const byte tagNsId = 24;

		// Token: 0x040093A1 RID: 37793
		internal const int ElementTypeIdConst = 12185;
	}
}
