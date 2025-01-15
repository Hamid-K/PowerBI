using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing.Diagram;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200282D RID: 10285
	[ChildElementInfo(typeof(NonVisualDrawingProperties), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class PtExtension : OpenXmlCompositeElement
	{
		// Token: 0x170065EF RID: 26095
		// (get) Token: 0x06014253 RID: 82515 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170065F0 RID: 26096
		// (get) Token: 0x06014254 RID: 82516 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170065F1 RID: 26097
		// (get) Token: 0x06014255 RID: 82517 RVA: 0x0030FBDB File Offset: 0x0030DDDB
		internal override int ElementTypeId
		{
			get
			{
				return 10318;
			}
		}

		// Token: 0x06014256 RID: 82518 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170065F2 RID: 26098
		// (get) Token: 0x06014257 RID: 82519 RVA: 0x0030FBE2 File Offset: 0x0030DDE2
		internal override string[] AttributeTagNames
		{
			get
			{
				return PtExtension.attributeTagNames;
			}
		}

		// Token: 0x170065F3 RID: 26099
		// (get) Token: 0x06014258 RID: 82520 RVA: 0x0030FBE9 File Offset: 0x0030DDE9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PtExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170065F4 RID: 26100
		// (get) Token: 0x06014259 RID: 82521 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601425A RID: 82522 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uri")]
		public StringValue Uri
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

		// Token: 0x0601425B RID: 82523 RVA: 0x00293ECF File Offset: 0x002920CF
		public PtExtension()
		{
		}

		// Token: 0x0601425C RID: 82524 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PtExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601425D RID: 82525 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PtExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601425E RID: 82526 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PtExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601425F RID: 82527 RVA: 0x0030FBF0 File Offset: 0x0030DDF0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (58 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			return null;
		}

		// Token: 0x06014260 RID: 82528 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014261 RID: 82529 RVA: 0x0030FC0B File Offset: 0x0030DE0B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PtExtension>(deep);
		}

		// Token: 0x06014262 RID: 82530 RVA: 0x0030FC14 File Offset: 0x0030DE14
		// Note: this type is marked as 'beforefieldinit'.
		static PtExtension()
		{
			byte[] array = new byte[1];
			PtExtension.attributeNamespaceIds = array;
		}

		// Token: 0x0400893B RID: 35131
		private const string tagName = "ext";

		// Token: 0x0400893C RID: 35132
		private const byte tagNsId = 10;

		// Token: 0x0400893D RID: 35133
		internal const int ElementTypeIdConst = 10318;

		// Token: 0x0400893E RID: 35134
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x0400893F RID: 35135
		private static byte[] attributeNamespaceIds;
	}
}
