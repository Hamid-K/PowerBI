using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x02002334 RID: 9012
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapeTree : GroupShapeType
	{
		// Token: 0x170048FA RID: 18682
		// (get) Token: 0x06010163 RID: 65891 RVA: 0x002DF986 File Offset: 0x002DDB86
		public override string LocalName
		{
			get
			{
				return "spTree";
			}
		}

		// Token: 0x170048FB RID: 18683
		// (get) Token: 0x06010164 RID: 65892 RVA: 0x002DE7F3 File Offset: 0x002DC9F3
		internal override byte NamespaceId
		{
			get
			{
				return 56;
			}
		}

		// Token: 0x170048FC RID: 18684
		// (get) Token: 0x06010165 RID: 65893 RVA: 0x002DF98D File Offset: 0x002DDB8D
		internal override int ElementTypeId
		{
			get
			{
				return 13034;
			}
		}

		// Token: 0x06010166 RID: 65894 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06010167 RID: 65895 RVA: 0x002DF95A File Offset: 0x002DDB5A
		public ShapeTree()
		{
		}

		// Token: 0x06010168 RID: 65896 RVA: 0x002DF962 File Offset: 0x002DDB62
		public ShapeTree(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010169 RID: 65897 RVA: 0x002DF96B File Offset: 0x002DDB6B
		public ShapeTree(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601016A RID: 65898 RVA: 0x002DF974 File Offset: 0x002DDB74
		public ShapeTree(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601016B RID: 65899 RVA: 0x002DF994 File Offset: 0x002DDB94
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeTree>(deep);
		}

		// Token: 0x04007305 RID: 29445
		private const string tagName = "spTree";

		// Token: 0x04007306 RID: 29446
		private const byte tagNsId = 56;

		// Token: 0x04007307 RID: 29447
		internal const int ElementTypeIdConst = 13034;
	}
}
