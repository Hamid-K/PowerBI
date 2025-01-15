using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025CE RID: 9678
	[GeneratedCode("DomGen", "2.0")]
	internal class PageMargins : OpenXmlLeafElement
	{
		// Token: 0x170057DC RID: 22492
		// (get) Token: 0x06012286 RID: 74374 RVA: 0x002F66A6 File Offset: 0x002F48A6
		public override string LocalName
		{
			get
			{
				return "pageMargins";
			}
		}

		// Token: 0x170057DD RID: 22493
		// (get) Token: 0x06012287 RID: 74375 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170057DE RID: 22494
		// (get) Token: 0x06012288 RID: 74376 RVA: 0x002F66AD File Offset: 0x002F48AD
		internal override int ElementTypeId
		{
			get
			{
				return 10518;
			}
		}

		// Token: 0x06012289 RID: 74377 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170057DF RID: 22495
		// (get) Token: 0x0601228A RID: 74378 RVA: 0x002F66B4 File Offset: 0x002F48B4
		internal override string[] AttributeTagNames
		{
			get
			{
				return PageMargins.attributeTagNames;
			}
		}

		// Token: 0x170057E0 RID: 22496
		// (get) Token: 0x0601228B RID: 74379 RVA: 0x002F66BB File Offset: 0x002F48BB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PageMargins.attributeNamespaceIds;
			}
		}

		// Token: 0x170057E1 RID: 22497
		// (get) Token: 0x0601228C RID: 74380 RVA: 0x002E7DC5 File Offset: 0x002E5FC5
		// (set) Token: 0x0601228D RID: 74381 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "l")]
		public DoubleValue Left
		{
			get
			{
				return (DoubleValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170057E2 RID: 22498
		// (get) Token: 0x0601228E RID: 74382 RVA: 0x002E7DD4 File Offset: 0x002E5FD4
		// (set) Token: 0x0601228F RID: 74383 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "r")]
		public DoubleValue Right
		{
			get
			{
				return (DoubleValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170057E3 RID: 22499
		// (get) Token: 0x06012290 RID: 74384 RVA: 0x002E7DE3 File Offset: 0x002E5FE3
		// (set) Token: 0x06012291 RID: 74385 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "t")]
		public DoubleValue Top
		{
			get
			{
				return (DoubleValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170057E4 RID: 22500
		// (get) Token: 0x06012292 RID: 74386 RVA: 0x002F66C2 File Offset: 0x002F48C2
		// (set) Token: 0x06012293 RID: 74387 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "b")]
		public DoubleValue Bottom
		{
			get
			{
				return (DoubleValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170057E5 RID: 22501
		// (get) Token: 0x06012294 RID: 74388 RVA: 0x002E82DC File Offset: 0x002E64DC
		// (set) Token: 0x06012295 RID: 74389 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "header")]
		public DoubleValue Header
		{
			get
			{
				return (DoubleValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170057E6 RID: 22502
		// (get) Token: 0x06012296 RID: 74390 RVA: 0x002F66D1 File Offset: 0x002F48D1
		// (set) Token: 0x06012297 RID: 74391 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "footer")]
		public DoubleValue Footer
		{
			get
			{
				return (DoubleValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x06012299 RID: 74393 RVA: 0x002F66E0 File Offset: 0x002F48E0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "l" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "r" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "t" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "b" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "header" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "footer" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601229A RID: 74394 RVA: 0x002F6779 File Offset: 0x002F4979
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PageMargins>(deep);
		}

		// Token: 0x0601229B RID: 74395 RVA: 0x002F6784 File Offset: 0x002F4984
		// Note: this type is marked as 'beforefieldinit'.
		static PageMargins()
		{
			byte[] array = new byte[6];
			PageMargins.attributeNamespaceIds = array;
		}

		// Token: 0x04007E7A RID: 32378
		private const string tagName = "pageMargins";

		// Token: 0x04007E7B RID: 32379
		private const byte tagNsId = 11;

		// Token: 0x04007E7C RID: 32380
		internal const int ElementTypeIdConst = 10518;

		// Token: 0x04007E7D RID: 32381
		private static string[] attributeTagNames = new string[] { "l", "r", "t", "b", "header", "footer" };

		// Token: 0x04007E7E RID: 32382
		private static byte[] attributeNamespaceIds;
	}
}
