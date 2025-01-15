using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CCB RID: 11467
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Kpi))]
	internal class Kpis : OpenXmlCompositeElement
	{
		// Token: 0x1700853D RID: 34109
		// (get) Token: 0x06018903 RID: 100611 RVA: 0x003429DF File Offset: 0x00340BDF
		public override string LocalName
		{
			get
			{
				return "kpis";
			}
		}

		// Token: 0x1700853E RID: 34110
		// (get) Token: 0x06018904 RID: 100612 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700853F RID: 34111
		// (get) Token: 0x06018905 RID: 100613 RVA: 0x003429E6 File Offset: 0x00340BE6
		internal override int ElementTypeId
		{
			get
			{
				return 11448;
			}
		}

		// Token: 0x06018906 RID: 100614 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008540 RID: 34112
		// (get) Token: 0x06018907 RID: 100615 RVA: 0x003429ED File Offset: 0x00340BED
		internal override string[] AttributeTagNames
		{
			get
			{
				return Kpis.attributeTagNames;
			}
		}

		// Token: 0x17008541 RID: 34113
		// (get) Token: 0x06018908 RID: 100616 RVA: 0x003429F4 File Offset: 0x00340BF4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Kpis.attributeNamespaceIds;
			}
		}

		// Token: 0x17008542 RID: 34114
		// (get) Token: 0x06018909 RID: 100617 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601890A RID: 100618 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601890B RID: 100619 RVA: 0x00293ECF File Offset: 0x002920CF
		public Kpis()
		{
		}

		// Token: 0x0601890C RID: 100620 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Kpis(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601890D RID: 100621 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Kpis(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601890E RID: 100622 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Kpis(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601890F RID: 100623 RVA: 0x003429FB File Offset: 0x00340BFB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "kpi" == name)
			{
				return new Kpi();
			}
			return null;
		}

		// Token: 0x06018910 RID: 100624 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018911 RID: 100625 RVA: 0x00342A16 File Offset: 0x00340C16
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Kpis>(deep);
		}

		// Token: 0x06018912 RID: 100626 RVA: 0x00342A20 File Offset: 0x00340C20
		// Note: this type is marked as 'beforefieldinit'.
		static Kpis()
		{
			byte[] array = new byte[1];
			Kpis.attributeNamespaceIds = array;
		}

		// Token: 0x0400A0D6 RID: 41174
		private const string tagName = "kpis";

		// Token: 0x0400A0D7 RID: 41175
		private const byte tagNsId = 22;

		// Token: 0x0400A0D8 RID: 41176
		internal const int ElementTypeIdConst = 11448;

		// Token: 0x0400A0D9 RID: 41177
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A0DA RID: 41178
		private static byte[] attributeNamespaceIds;
	}
}
