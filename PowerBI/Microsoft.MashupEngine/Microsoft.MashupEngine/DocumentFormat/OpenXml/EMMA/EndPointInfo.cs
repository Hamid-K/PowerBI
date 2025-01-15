using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x0200307D RID: 12413
	[ChildElementInfo(typeof(EndPoint))]
	[GeneratedCode("DomGen", "2.0")]
	internal class EndPointInfo : OpenXmlCompositeElement
	{
		// Token: 0x1700971C RID: 38684
		// (get) Token: 0x0601AF3A RID: 110394 RVA: 0x00369DEF File Offset: 0x00367FEF
		public override string LocalName
		{
			get
			{
				return "endpoint-info";
			}
		}

		// Token: 0x1700971D RID: 38685
		// (get) Token: 0x0601AF3B RID: 110395 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x1700971E RID: 38686
		// (get) Token: 0x0601AF3C RID: 110396 RVA: 0x00369DF6 File Offset: 0x00367FF6
		internal override int ElementTypeId
		{
			get
			{
				return 12682;
			}
		}

		// Token: 0x0601AF3D RID: 110397 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700971F RID: 38687
		// (get) Token: 0x0601AF3E RID: 110398 RVA: 0x00369DFD File Offset: 0x00367FFD
		internal override string[] AttributeTagNames
		{
			get
			{
				return EndPointInfo.attributeTagNames;
			}
		}

		// Token: 0x17009720 RID: 38688
		// (get) Token: 0x0601AF3F RID: 110399 RVA: 0x00369E04 File Offset: 0x00368004
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return EndPointInfo.attributeNamespaceIds;
			}
		}

		// Token: 0x17009721 RID: 38689
		// (get) Token: 0x0601AF40 RID: 110400 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AF41 RID: 110401 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x0601AF42 RID: 110402 RVA: 0x00293ECF File Offset: 0x002920CF
		public EndPointInfo()
		{
		}

		// Token: 0x0601AF43 RID: 110403 RVA: 0x00293ED7 File Offset: 0x002920D7
		public EndPointInfo(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AF44 RID: 110404 RVA: 0x00293EE0 File Offset: 0x002920E0
		public EndPointInfo(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AF45 RID: 110405 RVA: 0x00293EE9 File Offset: 0x002920E9
		public EndPointInfo(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AF46 RID: 110406 RVA: 0x00369E0B File Offset: 0x0036800B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (44 == namespaceId && "endpoint" == name)
			{
				return new EndPoint();
			}
			return null;
		}

		// Token: 0x0601AF47 RID: 110407 RVA: 0x002BFD13 File Offset: 0x002BDF13
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AF48 RID: 110408 RVA: 0x00369E26 File Offset: 0x00368026
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndPointInfo>(deep);
		}

		// Token: 0x0601AF49 RID: 110409 RVA: 0x00369E30 File Offset: 0x00368030
		// Note: this type is marked as 'beforefieldinit'.
		static EndPointInfo()
		{
			byte[] array = new byte[1];
			EndPointInfo.attributeNamespaceIds = array;
		}

		// Token: 0x0400B243 RID: 45635
		private const string tagName = "endpoint-info";

		// Token: 0x0400B244 RID: 45636
		private const byte tagNsId = 44;

		// Token: 0x0400B245 RID: 45637
		internal const int ElementTypeIdConst = 12682;

		// Token: 0x0400B246 RID: 45638
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x0400B247 RID: 45639
		private static byte[] attributeNamespaceIds;
	}
}
