using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027CC RID: 10188
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Point))]
	internal class MoveTo : OpenXmlCompositeElement
	{
		// Token: 0x170063B4 RID: 25524
		// (get) Token: 0x06013CDE RID: 81118 RVA: 0x0030BDF9 File Offset: 0x00309FF9
		public override string LocalName
		{
			get
			{
				return "moveTo";
			}
		}

		// Token: 0x170063B5 RID: 25525
		// (get) Token: 0x06013CDF RID: 81119 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170063B6 RID: 25526
		// (get) Token: 0x06013CE0 RID: 81120 RVA: 0x0030BE00 File Offset: 0x0030A000
		internal override int ElementTypeId
		{
			get
			{
				return 10222;
			}
		}

		// Token: 0x06013CE1 RID: 81121 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013CE2 RID: 81122 RVA: 0x00293ECF File Offset: 0x002920CF
		public MoveTo()
		{
		}

		// Token: 0x06013CE3 RID: 81123 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MoveTo(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013CE4 RID: 81124 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MoveTo(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013CE5 RID: 81125 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MoveTo(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013CE6 RID: 81126 RVA: 0x0030BE07 File Offset: 0x0030A007
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "pt" == name)
			{
				return new Point();
			}
			return null;
		}

		// Token: 0x170063B7 RID: 25527
		// (get) Token: 0x06013CE7 RID: 81127 RVA: 0x0030BE22 File Offset: 0x0030A022
		internal override string[] ElementTagNames
		{
			get
			{
				return MoveTo.eleTagNames;
			}
		}

		// Token: 0x170063B8 RID: 25528
		// (get) Token: 0x06013CE8 RID: 81128 RVA: 0x0030BE29 File Offset: 0x0030A029
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return MoveTo.eleNamespaceIds;
			}
		}

		// Token: 0x170063B9 RID: 25529
		// (get) Token: 0x06013CE9 RID: 81129 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170063BA RID: 25530
		// (get) Token: 0x06013CEA RID: 81130 RVA: 0x0030BE30 File Offset: 0x0030A030
		// (set) Token: 0x06013CEB RID: 81131 RVA: 0x0030BE39 File Offset: 0x0030A039
		public Point Point
		{
			get
			{
				return base.GetElement<Point>(0);
			}
			set
			{
				base.SetElement<Point>(0, value);
			}
		}

		// Token: 0x06013CEC RID: 81132 RVA: 0x0030BE43 File Offset: 0x0030A043
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MoveTo>(deep);
		}

		// Token: 0x040087DC RID: 34780
		private const string tagName = "moveTo";

		// Token: 0x040087DD RID: 34781
		private const byte tagNsId = 10;

		// Token: 0x040087DE RID: 34782
		internal const int ElementTypeIdConst = 10222;

		// Token: 0x040087DF RID: 34783
		private static readonly string[] eleTagNames = new string[] { "pt" };

		// Token: 0x040087E0 RID: 34784
		private static readonly byte[] eleNamespaceIds = new byte[] { 10 };
	}
}
