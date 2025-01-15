using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x0200287F RID: 10367
	[ChildElementInfo(typeof(TwoCellAnchor))]
	[ChildElementInfo(typeof(AbsoluteAnchor))]
	[ChildElementInfo(typeof(OneCellAnchor))]
	[GeneratedCode("DomGen", "2.0")]
	internal class WorksheetDrawing : OpenXmlPartRootElement
	{
		// Token: 0x1700672E RID: 26414
		// (get) Token: 0x0601451F RID: 83231 RVA: 0x00311FFB File Offset: 0x003101FB
		public override string LocalName
		{
			get
			{
				return "wsDr";
			}
		}

		// Token: 0x1700672F RID: 26415
		// (get) Token: 0x06014520 RID: 83232 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x17006730 RID: 26416
		// (get) Token: 0x06014521 RID: 83233 RVA: 0x00312002 File Offset: 0x00310202
		internal override int ElementTypeId
		{
			get
			{
				return 10729;
			}
		}

		// Token: 0x06014522 RID: 83234 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014523 RID: 83235 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal WorksheetDrawing(DrawingsPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06014524 RID: 83236 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(DrawingsPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006731 RID: 26417
		// (get) Token: 0x06014525 RID: 83237 RVA: 0x00312009 File Offset: 0x00310209
		// (set) Token: 0x06014526 RID: 83238 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public DrawingsPart DrawingsPart
		{
			get
			{
				return base.OpenXmlPart as DrawingsPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06014527 RID: 83239 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public WorksheetDrawing(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014528 RID: 83240 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public WorksheetDrawing(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014529 RID: 83241 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public WorksheetDrawing(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601452A RID: 83242 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public WorksheetDrawing()
		{
		}

		// Token: 0x0601452B RID: 83243 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(DrawingsPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601452C RID: 83244 RVA: 0x00312018 File Offset: 0x00310218
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (18 == namespaceId && "twoCellAnchor" == name)
			{
				return new TwoCellAnchor();
			}
			if (18 == namespaceId && "oneCellAnchor" == name)
			{
				return new OneCellAnchor();
			}
			if (18 == namespaceId && "absoluteAnchor" == name)
			{
				return new AbsoluteAnchor();
			}
			return null;
		}

		// Token: 0x0601452D RID: 83245 RVA: 0x0031206E File Offset: 0x0031026E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WorksheetDrawing>(deep);
		}

		// Token: 0x04008D90 RID: 36240
		private const string tagName = "wsDr";

		// Token: 0x04008D91 RID: 36241
		private const byte tagNsId = 18;

		// Token: 0x04008D92 RID: 36242
		internal const int ElementTypeIdConst = 10729;
	}
}
