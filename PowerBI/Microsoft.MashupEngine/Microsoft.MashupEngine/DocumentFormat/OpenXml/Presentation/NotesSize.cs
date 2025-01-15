using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AB3 RID: 10931
	[GeneratedCode("DomGen", "2.0")]
	internal class NotesSize : OpenXmlLeafElement
	{
		// Token: 0x170074C7 RID: 29895
		// (get) Token: 0x06016404 RID: 91140 RVA: 0x00328343 File Offset: 0x00326543
		public override string LocalName
		{
			get
			{
				return "notesSz";
			}
		}

		// Token: 0x170074C8 RID: 29896
		// (get) Token: 0x06016405 RID: 91141 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170074C9 RID: 29897
		// (get) Token: 0x06016406 RID: 91142 RVA: 0x0032834A File Offset: 0x0032654A
		internal override int ElementTypeId
		{
			get
			{
				return 12346;
			}
		}

		// Token: 0x06016407 RID: 91143 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170074CA RID: 29898
		// (get) Token: 0x06016408 RID: 91144 RVA: 0x00328351 File Offset: 0x00326551
		internal override string[] AttributeTagNames
		{
			get
			{
				return NotesSize.attributeTagNames;
			}
		}

		// Token: 0x170074CB RID: 29899
		// (get) Token: 0x06016409 RID: 91145 RVA: 0x00328358 File Offset: 0x00326558
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NotesSize.attributeNamespaceIds;
			}
		}

		// Token: 0x170074CC RID: 29900
		// (get) Token: 0x0601640A RID: 91146 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601640B RID: 91147 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "cx")]
		public Int32Value Cx
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

		// Token: 0x170074CD RID: 29901
		// (get) Token: 0x0601640C RID: 91148 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x0601640D RID: 91149 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "cy")]
		public Int32Value Cy
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601640F RID: 91151 RVA: 0x0032835F File Offset: 0x0032655F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "cx" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "cy" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016410 RID: 91152 RVA: 0x00328395 File Offset: 0x00326595
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NotesSize>(deep);
		}

		// Token: 0x06016411 RID: 91153 RVA: 0x003283A0 File Offset: 0x003265A0
		// Note: this type is marked as 'beforefieldinit'.
		static NotesSize()
		{
			byte[] array = new byte[2];
			NotesSize.attributeNamespaceIds = array;
		}

		// Token: 0x040096DF RID: 38623
		private const string tagName = "notesSz";

		// Token: 0x040096E0 RID: 38624
		private const byte tagNsId = 24;

		// Token: 0x040096E1 RID: 38625
		internal const int ElementTypeIdConst = 12346;

		// Token: 0x040096E2 RID: 38626
		private static string[] attributeTagNames = new string[] { "cx", "cy" };

		// Token: 0x040096E3 RID: 38627
		private static byte[] attributeNamespaceIds;
	}
}
