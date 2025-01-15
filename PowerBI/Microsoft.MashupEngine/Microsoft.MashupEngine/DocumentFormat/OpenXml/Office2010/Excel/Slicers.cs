using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023F2 RID: 9202
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Slicer), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Slicers : OpenXmlPartRootElement
	{
		// Token: 0x17004E26 RID: 20006
		// (get) Token: 0x06010CB2 RID: 68786 RVA: 0x002E7453 File Offset: 0x002E5653
		public override string LocalName
		{
			get
			{
				return "slicers";
			}
		}

		// Token: 0x17004E27 RID: 20007
		// (get) Token: 0x06010CB3 RID: 68787 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004E28 RID: 20008
		// (get) Token: 0x06010CB4 RID: 68788 RVA: 0x002E745A File Offset: 0x002E565A
		internal override int ElementTypeId
		{
			get
			{
				return 12928;
			}
		}

		// Token: 0x06010CB5 RID: 68789 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010CB6 RID: 68790 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Slicers(SlicersPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06010CB7 RID: 68791 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(SlicersPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17004E29 RID: 20009
		// (get) Token: 0x06010CB8 RID: 68792 RVA: 0x002E7461 File Offset: 0x002E5661
		// (set) Token: 0x06010CB9 RID: 68793 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public SlicersPart SlicersPart
		{
			get
			{
				return base.OpenXmlPart as SlicersPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06010CBA RID: 68794 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Slicers(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010CBB RID: 68795 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Slicers(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010CBC RID: 68796 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Slicers(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010CBD RID: 68797 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Slicers()
		{
		}

		// Token: 0x06010CBE RID: 68798 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(SlicersPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06010CBF RID: 68799 RVA: 0x002E746E File Offset: 0x002E566E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "slicer" == name)
			{
				return new Slicer();
			}
			return null;
		}

		// Token: 0x06010CC0 RID: 68800 RVA: 0x002E7489 File Offset: 0x002E5689
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Slicers>(deep);
		}

		// Token: 0x04007657 RID: 30295
		private const string tagName = "slicers";

		// Token: 0x04007658 RID: 30296
		private const byte tagNsId = 53;

		// Token: 0x04007659 RID: 30297
		internal const int ElementTypeIdConst = 12928;
	}
}
