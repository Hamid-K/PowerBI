using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025F5 RID: 9717
	[GeneratedCode("DomGen", "2.0")]
	internal class HoleSize : OpenXmlLeafElement
	{
		// Token: 0x17005958 RID: 22872
		// (get) Token: 0x060125C0 RID: 75200 RVA: 0x002FA308 File Offset: 0x002F8508
		public override string LocalName
		{
			get
			{
				return "holeSize";
			}
		}

		// Token: 0x17005959 RID: 22873
		// (get) Token: 0x060125C1 RID: 75201 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700595A RID: 22874
		// (get) Token: 0x060125C2 RID: 75202 RVA: 0x002FA30F File Offset: 0x002F850F
		internal override int ElementTypeId
		{
			get
			{
				return 10562;
			}
		}

		// Token: 0x060125C3 RID: 75203 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700595B RID: 22875
		// (get) Token: 0x060125C4 RID: 75204 RVA: 0x002FA316 File Offset: 0x002F8516
		internal override string[] AttributeTagNames
		{
			get
			{
				return HoleSize.attributeTagNames;
			}
		}

		// Token: 0x1700595C RID: 22876
		// (get) Token: 0x060125C5 RID: 75205 RVA: 0x002FA31D File Offset: 0x002F851D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HoleSize.attributeNamespaceIds;
			}
		}

		// Token: 0x1700595D RID: 22877
		// (get) Token: 0x060125C6 RID: 75206 RVA: 0x002DE388 File Offset: 0x002DC588
		// (set) Token: 0x060125C7 RID: 75207 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public ByteValue Val
		{
			get
			{
				return (ByteValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060125C9 RID: 75209 RVA: 0x002DE397 File Offset: 0x002DC597
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new ByteValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060125CA RID: 75210 RVA: 0x002FA324 File Offset: 0x002F8524
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HoleSize>(deep);
		}

		// Token: 0x060125CB RID: 75211 RVA: 0x002FA330 File Offset: 0x002F8530
		// Note: this type is marked as 'beforefieldinit'.
		static HoleSize()
		{
			byte[] array = new byte[1];
			HoleSize.attributeNamespaceIds = array;
		}

		// Token: 0x04007F36 RID: 32566
		private const string tagName = "holeSize";

		// Token: 0x04007F37 RID: 32567
		private const byte tagNsId = 11;

		// Token: 0x04007F38 RID: 32568
		internal const int ElementTypeIdConst = 10562;

		// Token: 0x04007F39 RID: 32569
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007F3A RID: 32570
		private static byte[] attributeNamespaceIds;
	}
}
