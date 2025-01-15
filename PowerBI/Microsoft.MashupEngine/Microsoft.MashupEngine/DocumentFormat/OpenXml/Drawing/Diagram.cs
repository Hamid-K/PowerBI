using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200279D RID: 10141
	[GeneratedCode("DomGen", "2.0")]
	internal class Diagram : OpenXmlLeafElement
	{
		// Token: 0x1700625A RID: 25178
		// (get) Token: 0x06013A01 RID: 80385 RVA: 0x0030A067 File Offset: 0x00308267
		public override string LocalName
		{
			get
			{
				return "dgm";
			}
		}

		// Token: 0x1700625B RID: 25179
		// (get) Token: 0x06013A02 RID: 80386 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700625C RID: 25180
		// (get) Token: 0x06013A03 RID: 80387 RVA: 0x0030A06E File Offset: 0x0030826E
		internal override int ElementTypeId
		{
			get
			{
				return 10174;
			}
		}

		// Token: 0x06013A04 RID: 80388 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700625D RID: 25181
		// (get) Token: 0x06013A05 RID: 80389 RVA: 0x0030A075 File Offset: 0x00308275
		internal override string[] AttributeTagNames
		{
			get
			{
				return Diagram.attributeTagNames;
			}
		}

		// Token: 0x1700625E RID: 25182
		// (get) Token: 0x06013A06 RID: 80390 RVA: 0x0030A07C File Offset: 0x0030827C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Diagram.attributeNamespaceIds;
			}
		}

		// Token: 0x1700625F RID: 25183
		// (get) Token: 0x06013A07 RID: 80391 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06013A08 RID: 80392 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17006260 RID: 25184
		// (get) Token: 0x06013A09 RID: 80393 RVA: 0x0030A083 File Offset: 0x00308283
		// (set) Token: 0x06013A0A RID: 80394 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "bldStep")]
		public EnumValue<DiagramBuildStepValues> BuildStep
		{
			get
			{
				return (EnumValue<DiagramBuildStepValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06013A0C RID: 80396 RVA: 0x0030A092 File Offset: 0x00308292
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "bldStep" == name)
			{
				return new EnumValue<DiagramBuildStepValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013A0D RID: 80397 RVA: 0x0030A0C8 File Offset: 0x003082C8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Diagram>(deep);
		}

		// Token: 0x06013A0E RID: 80398 RVA: 0x0030A0D4 File Offset: 0x003082D4
		// Note: this type is marked as 'beforefieldinit'.
		static Diagram()
		{
			byte[] array = new byte[2];
			Diagram.attributeNamespaceIds = array;
		}

		// Token: 0x04008703 RID: 34563
		private const string tagName = "dgm";

		// Token: 0x04008704 RID: 34564
		private const byte tagNsId = 10;

		// Token: 0x04008705 RID: 34565
		internal const int ElementTypeIdConst = 10174;

		// Token: 0x04008706 RID: 34566
		private static string[] attributeTagNames = new string[] { "id", "bldStep" };

		// Token: 0x04008707 RID: 34567
		private static byte[] attributeNamespaceIds;
	}
}
