using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C1A RID: 11290
	[ChildElementInfo(typeof(SheetName))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SheetNames : OpenXmlCompositeElement
	{
		// Token: 0x17008044 RID: 32836
		// (get) Token: 0x06017D28 RID: 97576 RVA: 0x0033B957 File Offset: 0x00339B57
		public override string LocalName
		{
			get
			{
				return "sheetNames";
			}
		}

		// Token: 0x17008045 RID: 32837
		// (get) Token: 0x06017D29 RID: 97577 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008046 RID: 32838
		// (get) Token: 0x06017D2A RID: 97578 RVA: 0x0033B95E File Offset: 0x00339B5E
		internal override int ElementTypeId
		{
			get
			{
				return 11271;
			}
		}

		// Token: 0x06017D2B RID: 97579 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017D2C RID: 97580 RVA: 0x00293ECF File Offset: 0x002920CF
		public SheetNames()
		{
		}

		// Token: 0x06017D2D RID: 97581 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SheetNames(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017D2E RID: 97582 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SheetNames(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017D2F RID: 97583 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SheetNames(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017D30 RID: 97584 RVA: 0x0033B965 File Offset: 0x00339B65
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "sheetName" == name)
			{
				return new SheetName();
			}
			return null;
		}

		// Token: 0x06017D31 RID: 97585 RVA: 0x0033B980 File Offset: 0x00339B80
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SheetNames>(deep);
		}

		// Token: 0x04009DB5 RID: 40373
		private const string tagName = "sheetNames";

		// Token: 0x04009DB6 RID: 40374
		private const byte tagNsId = 22;

		// Token: 0x04009DB7 RID: 40375
		internal const int ElementTypeIdConst = 11271;
	}
}
