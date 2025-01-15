using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025C0 RID: 9664
	[ChildElementInfo(typeof(DisplayUnitsLabel))]
	[ChildElementInfo(typeof(BuiltInUnit))]
	[ChildElementInfo(typeof(CustomDisplayUnit))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DisplayUnits : OpenXmlCompositeElement
	{
		// Token: 0x1700577C RID: 22396
		// (get) Token: 0x060121A7 RID: 74151 RVA: 0x002F5899 File Offset: 0x002F3A99
		public override string LocalName
		{
			get
			{
				return "dispUnits";
			}
		}

		// Token: 0x1700577D RID: 22397
		// (get) Token: 0x060121A8 RID: 74152 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700577E RID: 22398
		// (get) Token: 0x060121A9 RID: 74153 RVA: 0x002F58A0 File Offset: 0x002F3AA0
		internal override int ElementTypeId
		{
			get
			{
				return 10490;
			}
		}

		// Token: 0x060121AA RID: 74154 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060121AB RID: 74155 RVA: 0x00293ECF File Offset: 0x002920CF
		public DisplayUnits()
		{
		}

		// Token: 0x060121AC RID: 74156 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DisplayUnits(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060121AD RID: 74157 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DisplayUnits(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060121AE RID: 74158 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DisplayUnits(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060121AF RID: 74159 RVA: 0x002F58A8 File Offset: 0x002F3AA8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "custUnit" == name)
			{
				return new CustomDisplayUnit();
			}
			if (11 == namespaceId && "builtInUnit" == name)
			{
				return new BuiltInUnit();
			}
			if (11 == namespaceId && "dispUnitsLbl" == name)
			{
				return new DisplayUnitsLabel();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x060121B0 RID: 74160 RVA: 0x002F5916 File Offset: 0x002F3B16
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DisplayUnits>(deep);
		}

		// Token: 0x04007E3F RID: 32319
		private const string tagName = "dispUnits";

		// Token: 0x04007E40 RID: 32320
		private const byte tagNsId = 11;

		// Token: 0x04007E41 RID: 32321
		internal const int ElementTypeIdConst = 10490;
	}
}
