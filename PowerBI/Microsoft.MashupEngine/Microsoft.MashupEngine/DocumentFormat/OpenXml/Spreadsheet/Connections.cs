using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B18 RID: 11032
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Connection))]
	internal class Connections : OpenXmlPartRootElement
	{
		// Token: 0x170075CB RID: 30155
		// (get) Token: 0x06016651 RID: 91729 RVA: 0x002A7937 File Offset: 0x002A5B37
		public override string LocalName
		{
			get
			{
				return "connections";
			}
		}

		// Token: 0x170075CC RID: 30156
		// (get) Token: 0x06016652 RID: 91730 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170075CD RID: 30157
		// (get) Token: 0x06016653 RID: 91731 RVA: 0x0032993F File Offset: 0x00327B3F
		internal override int ElementTypeId
		{
			get
			{
				return 11030;
			}
		}

		// Token: 0x06016654 RID: 91732 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016655 RID: 91733 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Connections(ConnectionsPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06016656 RID: 91734 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(ConnectionsPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x170075CE RID: 30158
		// (get) Token: 0x06016657 RID: 91735 RVA: 0x00329946 File Offset: 0x00327B46
		// (set) Token: 0x06016658 RID: 91736 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public ConnectionsPart ConnectionsPart
		{
			get
			{
				return base.OpenXmlPart as ConnectionsPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06016659 RID: 91737 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Connections(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601665A RID: 91738 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Connections(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601665B RID: 91739 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Connections(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601665C RID: 91740 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Connections()
		{
		}

		// Token: 0x0601665D RID: 91741 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(ConnectionsPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601665E RID: 91742 RVA: 0x00329953 File Offset: 0x00327B53
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "connection" == name)
			{
				return new Connection();
			}
			return null;
		}

		// Token: 0x0601665F RID: 91743 RVA: 0x0032996E File Offset: 0x00327B6E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Connections>(deep);
		}

		// Token: 0x040098D7 RID: 39127
		private const string tagName = "connections";

		// Token: 0x040098D8 RID: 39128
		private const byte tagNsId = 22;

		// Token: 0x040098D9 RID: 39129
		internal const int ElementTypeIdConst = 11030;
	}
}
