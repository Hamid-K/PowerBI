using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B94 RID: 11156
	[GeneratedCode("DomGen", "2.0")]
	internal class SharedStringItem : RstType
	{
		// Token: 0x17007B18 RID: 31512
		// (get) Token: 0x06017224 RID: 94756 RVA: 0x003332F4 File Offset: 0x003314F4
		public override string LocalName
		{
			get
			{
				return "si";
			}
		}

		// Token: 0x17007B19 RID: 31513
		// (get) Token: 0x06017225 RID: 94757 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B1A RID: 31514
		// (get) Token: 0x06017226 RID: 94758 RVA: 0x003332FB File Offset: 0x003314FB
		internal override int ElementTypeId
		{
			get
			{
				return 11134;
			}
		}

		// Token: 0x06017227 RID: 94759 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017228 RID: 94760 RVA: 0x00333302 File Offset: 0x00331502
		public SharedStringItem()
		{
		}

		// Token: 0x06017229 RID: 94761 RVA: 0x0033330A File Offset: 0x0033150A
		public SharedStringItem(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601722A RID: 94762 RVA: 0x00333313 File Offset: 0x00331513
		public SharedStringItem(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601722B RID: 94763 RVA: 0x0033331C File Offset: 0x0033151C
		public SharedStringItem(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601722C RID: 94764 RVA: 0x00333325 File Offset: 0x00331525
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SharedStringItem>(deep);
		}

		// Token: 0x04009B2C RID: 39724
		private const string tagName = "si";

		// Token: 0x04009B2D RID: 39725
		private const byte tagNsId = 22;

		// Token: 0x04009B2E RID: 39726
		internal const int ElementTypeIdConst = 11134;
	}
}
