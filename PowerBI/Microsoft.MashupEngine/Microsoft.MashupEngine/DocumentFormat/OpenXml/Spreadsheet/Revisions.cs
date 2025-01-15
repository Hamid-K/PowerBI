using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B1F RID: 11039
	[ChildElementInfo(typeof(RevisionFormat))]
	[ChildElementInfo(typeof(RevisionCellChange))]
	[ChildElementInfo(typeof(RevisionAutoFormat))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RevisionMove))]
	[ChildElementInfo(typeof(RevisionCustomView))]
	[ChildElementInfo(typeof(RevisionSheetName))]
	[ChildElementInfo(typeof(RevisionInsertSheet))]
	[ChildElementInfo(typeof(RevisionRowColumn))]
	[ChildElementInfo(typeof(RevisionDefinedName))]
	[ChildElementInfo(typeof(RevisionComment))]
	[ChildElementInfo(typeof(RevisionQueryTable))]
	[ChildElementInfo(typeof(RevisionConflict))]
	internal class Revisions : OpenXmlPartRootElement
	{
		// Token: 0x17007695 RID: 30357
		// (get) Token: 0x0601680D RID: 92173 RVA: 0x002A85C9 File Offset: 0x002A67C9
		public override string LocalName
		{
			get
			{
				return "revisions";
			}
		}

		// Token: 0x17007696 RID: 30358
		// (get) Token: 0x0601680E RID: 92174 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007697 RID: 30359
		// (get) Token: 0x0601680F RID: 92175 RVA: 0x0032B454 File Offset: 0x00329654
		internal override int ElementTypeId
		{
			get
			{
				return 11037;
			}
		}

		// Token: 0x06016810 RID: 92176 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016811 RID: 92177 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Revisions(WorkbookRevisionLogPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06016812 RID: 92178 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(WorkbookRevisionLogPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17007698 RID: 30360
		// (get) Token: 0x06016813 RID: 92179 RVA: 0x0032B45B File Offset: 0x0032965B
		// (set) Token: 0x06016814 RID: 92180 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public WorkbookRevisionLogPart WorkbookRevisionLogPart
		{
			get
			{
				return base.OpenXmlPart as WorkbookRevisionLogPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06016815 RID: 92181 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Revisions(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016816 RID: 92182 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Revisions(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016817 RID: 92183 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Revisions(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016818 RID: 92184 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Revisions()
		{
		}

		// Token: 0x06016819 RID: 92185 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(WorkbookRevisionLogPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601681A RID: 92186 RVA: 0x0032B468 File Offset: 0x00329668
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "rrc" == name)
			{
				return new RevisionRowColumn();
			}
			if (22 == namespaceId && "rm" == name)
			{
				return new RevisionMove();
			}
			if (22 == namespaceId && "rcv" == name)
			{
				return new RevisionCustomView();
			}
			if (22 == namespaceId && "rsnm" == name)
			{
				return new RevisionSheetName();
			}
			if (22 == namespaceId && "ris" == name)
			{
				return new RevisionInsertSheet();
			}
			if (22 == namespaceId && "rcc" == name)
			{
				return new RevisionCellChange();
			}
			if (22 == namespaceId && "rfmt" == name)
			{
				return new RevisionFormat();
			}
			if (22 == namespaceId && "raf" == name)
			{
				return new RevisionAutoFormat();
			}
			if (22 == namespaceId && "rdn" == name)
			{
				return new RevisionDefinedName();
			}
			if (22 == namespaceId && "rcmt" == name)
			{
				return new RevisionComment();
			}
			if (22 == namespaceId && "rqt" == name)
			{
				return new RevisionQueryTable();
			}
			if (22 == namespaceId && "rcft" == name)
			{
				return new RevisionConflict();
			}
			return null;
		}

		// Token: 0x0601681B RID: 92187 RVA: 0x0032B596 File Offset: 0x00329796
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Revisions>(deep);
		}

		// Token: 0x040098FE RID: 39166
		private const string tagName = "revisions";

		// Token: 0x040098FF RID: 39167
		private const byte tagNsId = 22;

		// Token: 0x04009900 RID: 39168
		internal const int ElementTypeIdConst = 11037;
	}
}
