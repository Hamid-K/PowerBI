using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002569 RID: 9577
	[ChildElementInfo(typeof(AbsoluteAnchorSize))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RelativeAnchorSize))]
	internal class UserShapes : OpenXmlPartRootElement
	{
		// Token: 0x170055BE RID: 21950
		// (get) Token: 0x06011DB6 RID: 73142 RVA: 0x002F3280 File Offset: 0x002F1480
		public override string LocalName
		{
			get
			{
				return "userShapes";
			}
		}

		// Token: 0x170055BF RID: 21951
		// (get) Token: 0x06011DB7 RID: 73143 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055C0 RID: 21952
		// (get) Token: 0x06011DB8 RID: 73144 RVA: 0x002F3287 File Offset: 0x002F1487
		internal override int ElementTypeId
		{
			get
			{
				return 10387;
			}
		}

		// Token: 0x06011DB9 RID: 73145 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011DBA RID: 73146 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal UserShapes(ChartDrawingPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06011DBB RID: 73147 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(ChartDrawingPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x170055C1 RID: 21953
		// (get) Token: 0x06011DBC RID: 73148 RVA: 0x002F328E File Offset: 0x002F148E
		// (set) Token: 0x06011DBD RID: 73149 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public ChartDrawingPart ChartDrawingPart
		{
			get
			{
				return base.OpenXmlPart as ChartDrawingPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06011DBE RID: 73150 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public UserShapes(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011DBF RID: 73151 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public UserShapes(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011DC0 RID: 73152 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public UserShapes(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011DC1 RID: 73153 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public UserShapes()
		{
		}

		// Token: 0x06011DC2 RID: 73154 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(ChartDrawingPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06011DC3 RID: 73155 RVA: 0x002F329B File Offset: 0x002F149B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (12 == namespaceId && "relSizeAnchor" == name)
			{
				return new RelativeAnchorSize();
			}
			if (12 == namespaceId && "absSizeAnchor" == name)
			{
				return new AbsoluteAnchorSize();
			}
			return null;
		}

		// Token: 0x06011DC4 RID: 73156 RVA: 0x002F32CE File Offset: 0x002F14CE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UserShapes>(deep);
		}

		// Token: 0x04007CED RID: 31981
		private const string tagName = "userShapes";

		// Token: 0x04007CEE RID: 31982
		private const byte tagNsId = 11;

		// Token: 0x04007CEF RID: 31983
		internal const int ElementTypeIdConst = 10387;
	}
}
