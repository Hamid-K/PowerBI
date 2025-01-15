using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002710 RID: 10000
	[GeneratedCode("DomGen", "2.0")]
	internal class AlphaReplace : OpenXmlLeafElement
	{
		// Token: 0x17005ED7 RID: 24279
		// (get) Token: 0x0601320E RID: 78350 RVA: 0x00303F73 File Offset: 0x00302173
		public override string LocalName
		{
			get
			{
				return "alphaRepl";
			}
		}

		// Token: 0x17005ED8 RID: 24280
		// (get) Token: 0x0601320F RID: 78351 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005ED9 RID: 24281
		// (get) Token: 0x06013210 RID: 78352 RVA: 0x00303F7A File Offset: 0x0030217A
		internal override int ElementTypeId
		{
			get
			{
				return 10062;
			}
		}

		// Token: 0x06013211 RID: 78353 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005EDA RID: 24282
		// (get) Token: 0x06013212 RID: 78354 RVA: 0x00303F81 File Offset: 0x00302181
		internal override string[] AttributeTagNames
		{
			get
			{
				return AlphaReplace.attributeTagNames;
			}
		}

		// Token: 0x17005EDB RID: 24283
		// (get) Token: 0x06013213 RID: 78355 RVA: 0x00303F88 File Offset: 0x00302188
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AlphaReplace.attributeNamespaceIds;
			}
		}

		// Token: 0x17005EDC RID: 24284
		// (get) Token: 0x06013214 RID: 78356 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06013215 RID: 78357 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "a")]
		public Int32Value Alpha
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06013217 RID: 78359 RVA: 0x00303F8F File Offset: 0x0030218F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "a" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013218 RID: 78360 RVA: 0x00303FAF File Offset: 0x003021AF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlphaReplace>(deep);
		}

		// Token: 0x06013219 RID: 78361 RVA: 0x00303FB8 File Offset: 0x003021B8
		// Note: this type is marked as 'beforefieldinit'.
		static AlphaReplace()
		{
			byte[] array = new byte[1];
			AlphaReplace.attributeNamespaceIds = array;
		}

		// Token: 0x040084D1 RID: 34001
		private const string tagName = "alphaRepl";

		// Token: 0x040084D2 RID: 34002
		private const byte tagNsId = 10;

		// Token: 0x040084D3 RID: 34003
		internal const int ElementTypeIdConst = 10062;

		// Token: 0x040084D4 RID: 34004
		private static string[] attributeTagNames = new string[] { "a" };

		// Token: 0x040084D5 RID: 34005
		private static byte[] attributeNamespaceIds;
	}
}
