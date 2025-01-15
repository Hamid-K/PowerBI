using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CA2 RID: 11426
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(OleObject))]
	internal class OleObjects : OpenXmlCompositeElement
	{
		// Token: 0x1700843F RID: 33855
		// (get) Token: 0x06018677 RID: 99959 RVA: 0x003414AF File Offset: 0x0033F6AF
		public override string LocalName
		{
			get
			{
				return "oleObjects";
			}
		}

		// Token: 0x17008440 RID: 33856
		// (get) Token: 0x06018678 RID: 99960 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008441 RID: 33857
		// (get) Token: 0x06018679 RID: 99961 RVA: 0x003414B6 File Offset: 0x0033F6B6
		internal override int ElementTypeId
		{
			get
			{
				return 11406;
			}
		}

		// Token: 0x0601867A RID: 99962 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601867B RID: 99963 RVA: 0x00293ECF File Offset: 0x002920CF
		public OleObjects()
		{
		}

		// Token: 0x0601867C RID: 99964 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OleObjects(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601867D RID: 99965 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OleObjects(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601867E RID: 99966 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OleObjects(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601867F RID: 99967 RVA: 0x003414BD File Offset: 0x0033F6BD
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "oleObject" == name)
			{
				return new OleObject();
			}
			return null;
		}

		// Token: 0x06018680 RID: 99968 RVA: 0x003414D8 File Offset: 0x0033F6D8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OleObjects>(deep);
		}

		// Token: 0x0400A01B RID: 40987
		private const string tagName = "oleObjects";

		// Token: 0x0400A01C RID: 40988
		private const byte tagNsId = 22;

		// Token: 0x0400A01D RID: 40989
		internal const int ElementTypeIdConst = 11406;
	}
}
