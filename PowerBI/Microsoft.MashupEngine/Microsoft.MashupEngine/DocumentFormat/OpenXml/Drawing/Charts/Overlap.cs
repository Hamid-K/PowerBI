using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025D6 RID: 9686
	[GeneratedCode("DomGen", "2.0")]
	internal class Overlap : OpenXmlLeafElement
	{
		// Token: 0x17005832 RID: 22578
		// (get) Token: 0x06012341 RID: 74561 RVA: 0x002F71C5 File Offset: 0x002F53C5
		public override string LocalName
		{
			get
			{
				return "overlap";
			}
		}

		// Token: 0x17005833 RID: 22579
		// (get) Token: 0x06012342 RID: 74562 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005834 RID: 22580
		// (get) Token: 0x06012343 RID: 74563 RVA: 0x002F71CC File Offset: 0x002F53CC
		internal override int ElementTypeId
		{
			get
			{
				return 10526;
			}
		}

		// Token: 0x06012344 RID: 74564 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005835 RID: 22581
		// (get) Token: 0x06012345 RID: 74565 RVA: 0x002F71D3 File Offset: 0x002F53D3
		internal override string[] AttributeTagNames
		{
			get
			{
				return Overlap.attributeTagNames;
			}
		}

		// Token: 0x17005836 RID: 22582
		// (get) Token: 0x06012346 RID: 74566 RVA: 0x002F71DA File Offset: 0x002F53DA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Overlap.attributeNamespaceIds;
			}
		}

		// Token: 0x17005837 RID: 22583
		// (get) Token: 0x06012347 RID: 74567 RVA: 0x002F413F File Offset: 0x002F233F
		// (set) Token: 0x06012348 RID: 74568 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public SByteValue Val
		{
			get
			{
				return (SByteValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601234A RID: 74570 RVA: 0x002F414E File Offset: 0x002F234E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new SByteValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601234B RID: 74571 RVA: 0x002F71E1 File Offset: 0x002F53E1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Overlap>(deep);
		}

		// Token: 0x0601234C RID: 74572 RVA: 0x002F71EC File Offset: 0x002F53EC
		// Note: this type is marked as 'beforefieldinit'.
		static Overlap()
		{
			byte[] array = new byte[1];
			Overlap.attributeNamespaceIds = array;
		}

		// Token: 0x04007E9B RID: 32411
		private const string tagName = "overlap";

		// Token: 0x04007E9C RID: 32412
		private const byte tagNsId = 11;

		// Token: 0x04007E9D RID: 32413
		internal const int ElementTypeIdConst = 10526;

		// Token: 0x04007E9E RID: 32414
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007E9F RID: 32415
		private static byte[] attributeNamespaceIds;
	}
}
