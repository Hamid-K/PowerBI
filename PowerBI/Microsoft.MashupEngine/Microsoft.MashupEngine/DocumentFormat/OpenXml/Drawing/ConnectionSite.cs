using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027CA RID: 10186
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Position))]
	internal class ConnectionSite : OpenXmlCompositeElement
	{
		// Token: 0x170063A7 RID: 25511
		// (get) Token: 0x06013CC3 RID: 81091 RVA: 0x002FDFE6 File Offset: 0x002FC1E6
		public override string LocalName
		{
			get
			{
				return "cxn";
			}
		}

		// Token: 0x170063A8 RID: 25512
		// (get) Token: 0x06013CC4 RID: 81092 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170063A9 RID: 25513
		// (get) Token: 0x06013CC5 RID: 81093 RVA: 0x0030BD3E File Offset: 0x00309F3E
		internal override int ElementTypeId
		{
			get
			{
				return 10219;
			}
		}

		// Token: 0x06013CC6 RID: 81094 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170063AA RID: 25514
		// (get) Token: 0x06013CC7 RID: 81095 RVA: 0x0030BD45 File Offset: 0x00309F45
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConnectionSite.attributeTagNames;
			}
		}

		// Token: 0x170063AB RID: 25515
		// (get) Token: 0x06013CC8 RID: 81096 RVA: 0x0030BD4C File Offset: 0x00309F4C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConnectionSite.attributeNamespaceIds;
			}
		}

		// Token: 0x170063AC RID: 25516
		// (get) Token: 0x06013CC9 RID: 81097 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06013CCA RID: 81098 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ang")]
		public StringValue Angle
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06013CCB RID: 81099 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConnectionSite()
		{
		}

		// Token: 0x06013CCC RID: 81100 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConnectionSite(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013CCD RID: 81101 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConnectionSite(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013CCE RID: 81102 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConnectionSite(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013CCF RID: 81103 RVA: 0x0030BA92 File Offset: 0x00309C92
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "pos" == name)
			{
				return new Position();
			}
			return null;
		}

		// Token: 0x170063AD RID: 25517
		// (get) Token: 0x06013CD0 RID: 81104 RVA: 0x0030BD53 File Offset: 0x00309F53
		internal override string[] ElementTagNames
		{
			get
			{
				return ConnectionSite.eleTagNames;
			}
		}

		// Token: 0x170063AE RID: 25518
		// (get) Token: 0x06013CD1 RID: 81105 RVA: 0x0030BD5A File Offset: 0x00309F5A
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ConnectionSite.eleNamespaceIds;
			}
		}

		// Token: 0x170063AF RID: 25519
		// (get) Token: 0x06013CD2 RID: 81106 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170063B0 RID: 25520
		// (get) Token: 0x06013CD3 RID: 81107 RVA: 0x0030BABB File Offset: 0x00309CBB
		// (set) Token: 0x06013CD4 RID: 81108 RVA: 0x0030BAC4 File Offset: 0x00309CC4
		public Position Position
		{
			get
			{
				return base.GetElement<Position>(0);
			}
			set
			{
				base.SetElement<Position>(0, value);
			}
		}

		// Token: 0x06013CD5 RID: 81109 RVA: 0x0030BD61 File Offset: 0x00309F61
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ang" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013CD6 RID: 81110 RVA: 0x0030BD81 File Offset: 0x00309F81
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConnectionSite>(deep);
		}

		// Token: 0x06013CD7 RID: 81111 RVA: 0x0030BD8C File Offset: 0x00309F8C
		// Note: this type is marked as 'beforefieldinit'.
		static ConnectionSite()
		{
			byte[] array = new byte[1];
			ConnectionSite.attributeNamespaceIds = array;
			ConnectionSite.eleTagNames = new string[] { "pos" };
			ConnectionSite.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x040087D2 RID: 34770
		private const string tagName = "cxn";

		// Token: 0x040087D3 RID: 34771
		private const byte tagNsId = 10;

		// Token: 0x040087D4 RID: 34772
		internal const int ElementTypeIdConst = 10219;

		// Token: 0x040087D5 RID: 34773
		private static string[] attributeTagNames = new string[] { "ang" };

		// Token: 0x040087D6 RID: 34774
		private static byte[] attributeNamespaceIds;

		// Token: 0x040087D7 RID: 34775
		private static readonly string[] eleTagNames;

		// Token: 0x040087D8 RID: 34776
		private static readonly byte[] eleNamespaceIds;
	}
}
