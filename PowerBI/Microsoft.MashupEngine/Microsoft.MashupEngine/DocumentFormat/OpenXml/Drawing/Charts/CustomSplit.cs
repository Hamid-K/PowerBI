using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025AE RID: 9646
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SecondPiePoint))]
	internal class CustomSplit : OpenXmlCompositeElement
	{
		// Token: 0x1700571F RID: 22303
		// (get) Token: 0x060120E3 RID: 73955 RVA: 0x002F51C7 File Offset: 0x002F33C7
		public override string LocalName
		{
			get
			{
				return "custSplit";
			}
		}

		// Token: 0x17005720 RID: 22304
		// (get) Token: 0x060120E4 RID: 73956 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005721 RID: 22305
		// (get) Token: 0x060120E5 RID: 73957 RVA: 0x002F51CE File Offset: 0x002F33CE
		internal override int ElementTypeId
		{
			get
			{
				return 10465;
			}
		}

		// Token: 0x060120E6 RID: 73958 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060120E7 RID: 73959 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomSplit()
		{
		}

		// Token: 0x060120E8 RID: 73960 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomSplit(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060120E9 RID: 73961 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomSplit(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060120EA RID: 73962 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomSplit(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060120EB RID: 73963 RVA: 0x002F51D5 File Offset: 0x002F33D5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "secondPiePt" == name)
			{
				return new SecondPiePoint();
			}
			return null;
		}

		// Token: 0x060120EC RID: 73964 RVA: 0x002F51F0 File Offset: 0x002F33F0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomSplit>(deep);
		}

		// Token: 0x04007DF5 RID: 32245
		private const string tagName = "custSplit";

		// Token: 0x04007DF6 RID: 32246
		private const byte tagNsId = 11;

		// Token: 0x04007DF7 RID: 32247
		internal const int ElementTypeIdConst = 10465;
	}
}
