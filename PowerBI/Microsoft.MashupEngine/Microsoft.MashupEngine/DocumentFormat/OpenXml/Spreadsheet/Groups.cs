using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B64 RID: 11108
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Group))]
	internal class Groups : OpenXmlCompositeElement
	{
		// Token: 0x170078F6 RID: 30966
		// (get) Token: 0x06016D8C RID: 93580 RVA: 0x0032FC47 File Offset: 0x0032DE47
		public override string LocalName
		{
			get
			{
				return "groups";
			}
		}

		// Token: 0x170078F7 RID: 30967
		// (get) Token: 0x06016D8D RID: 93581 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170078F8 RID: 30968
		// (get) Token: 0x06016D8E RID: 93582 RVA: 0x0032FC4E File Offset: 0x0032DE4E
		internal override int ElementTypeId
		{
			get
			{
				return 11087;
			}
		}

		// Token: 0x06016D8F RID: 93583 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170078F9 RID: 30969
		// (get) Token: 0x06016D90 RID: 93584 RVA: 0x0032FC55 File Offset: 0x0032DE55
		internal override string[] AttributeTagNames
		{
			get
			{
				return Groups.attributeTagNames;
			}
		}

		// Token: 0x170078FA RID: 30970
		// (get) Token: 0x06016D91 RID: 93585 RVA: 0x0032FC5C File Offset: 0x0032DE5C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Groups.attributeNamespaceIds;
			}
		}

		// Token: 0x170078FB RID: 30971
		// (get) Token: 0x06016D92 RID: 93586 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016D93 RID: 93587 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06016D94 RID: 93588 RVA: 0x00293ECF File Offset: 0x002920CF
		public Groups()
		{
		}

		// Token: 0x06016D95 RID: 93589 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Groups(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016D96 RID: 93590 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Groups(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016D97 RID: 93591 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Groups(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016D98 RID: 93592 RVA: 0x0032FC63 File Offset: 0x0032DE63
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "group" == name)
			{
				return new Group();
			}
			return null;
		}

		// Token: 0x06016D99 RID: 93593 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016D9A RID: 93594 RVA: 0x0032FC7E File Offset: 0x0032DE7E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Groups>(deep);
		}

		// Token: 0x06016D9B RID: 93595 RVA: 0x0032FC88 File Offset: 0x0032DE88
		// Note: this type is marked as 'beforefieldinit'.
		static Groups()
		{
			byte[] array = new byte[1];
			Groups.attributeNamespaceIds = array;
		}

		// Token: 0x04009A26 RID: 39462
		private const string tagName = "groups";

		// Token: 0x04009A27 RID: 39463
		private const byte tagNsId = 22;

		// Token: 0x04009A28 RID: 39464
		internal const int ElementTypeIdConst = 11087;

		// Token: 0x04009A29 RID: 39465
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009A2A RID: 39466
		private static byte[] attributeNamespaceIds;
	}
}
