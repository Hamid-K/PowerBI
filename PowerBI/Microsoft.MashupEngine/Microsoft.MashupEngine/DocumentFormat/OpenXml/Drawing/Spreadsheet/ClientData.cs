using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002890 RID: 10384
	[GeneratedCode("DomGen", "2.0")]
	internal class ClientData : OpenXmlLeafElement
	{
		// Token: 0x1700679F RID: 26527
		// (get) Token: 0x06014628 RID: 83496 RVA: 0x00312B83 File Offset: 0x00310D83
		public override string LocalName
		{
			get
			{
				return "clientData";
			}
		}

		// Token: 0x170067A0 RID: 26528
		// (get) Token: 0x06014629 RID: 83497 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170067A1 RID: 26529
		// (get) Token: 0x0601462A RID: 83498 RVA: 0x00312B8A File Offset: 0x00310D8A
		internal override int ElementTypeId
		{
			get
			{
				return 10745;
			}
		}

		// Token: 0x0601462B RID: 83499 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170067A2 RID: 26530
		// (get) Token: 0x0601462C RID: 83500 RVA: 0x00312B91 File Offset: 0x00310D91
		internal override string[] AttributeTagNames
		{
			get
			{
				return ClientData.attributeTagNames;
			}
		}

		// Token: 0x170067A3 RID: 26531
		// (get) Token: 0x0601462D RID: 83501 RVA: 0x00312B98 File Offset: 0x00310D98
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ClientData.attributeNamespaceIds;
			}
		}

		// Token: 0x170067A4 RID: 26532
		// (get) Token: 0x0601462E RID: 83502 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601462F RID: 83503 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "fLocksWithSheet")]
		public BooleanValue LockWithSheet
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170067A5 RID: 26533
		// (get) Token: 0x06014630 RID: 83504 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06014631 RID: 83505 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "fPrintsWithSheet")]
		public BooleanValue PrintWithSheet
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06014633 RID: 83507 RVA: 0x00312B9F File Offset: 0x00310D9F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "fLocksWithSheet" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "fPrintsWithSheet" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014634 RID: 83508 RVA: 0x00312BD5 File Offset: 0x00310DD5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ClientData>(deep);
		}

		// Token: 0x06014635 RID: 83509 RVA: 0x00312BE0 File Offset: 0x00310DE0
		// Note: this type is marked as 'beforefieldinit'.
		static ClientData()
		{
			byte[] array = new byte[2];
			ClientData.attributeNamespaceIds = array;
		}

		// Token: 0x04008DDA RID: 36314
		private const string tagName = "clientData";

		// Token: 0x04008DDB RID: 36315
		private const byte tagNsId = 18;

		// Token: 0x04008DDC RID: 36316
		internal const int ElementTypeIdConst = 10745;

		// Token: 0x04008DDD RID: 36317
		private static string[] attributeTagNames = new string[] { "fLocksWithSheet", "fPrintsWithSheet" };

		// Token: 0x04008DDE RID: 36318
		private static byte[] attributeNamespaceIds;
	}
}
