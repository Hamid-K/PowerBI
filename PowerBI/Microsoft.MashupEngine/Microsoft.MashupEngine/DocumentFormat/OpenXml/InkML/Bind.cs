using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x02003087 RID: 12423
	[GeneratedCode("DomGen", "2.0")]
	internal class Bind : OpenXmlLeafElement
	{
		// Token: 0x17009757 RID: 38743
		// (get) Token: 0x0601AFCB RID: 110539 RVA: 0x0036A5DB File Offset: 0x003687DB
		public override string LocalName
		{
			get
			{
				return "bind";
			}
		}

		// Token: 0x17009758 RID: 38744
		// (get) Token: 0x0601AFCC RID: 110540 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x17009759 RID: 38745
		// (get) Token: 0x0601AFCD RID: 110541 RVA: 0x0036A5E2 File Offset: 0x003687E2
		internal override int ElementTypeId
		{
			get
			{
				return 12644;
			}
		}

		// Token: 0x0601AFCE RID: 110542 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700975A RID: 38746
		// (get) Token: 0x0601AFCF RID: 110543 RVA: 0x0036A5E9 File Offset: 0x003687E9
		internal override string[] AttributeTagNames
		{
			get
			{
				return Bind.attributeTagNames;
			}
		}

		// Token: 0x1700975B RID: 38747
		// (get) Token: 0x0601AFD0 RID: 110544 RVA: 0x0036A5F0 File Offset: 0x003687F0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Bind.attributeNamespaceIds;
			}
		}

		// Token: 0x1700975C RID: 38748
		// (get) Token: 0x0601AFD1 RID: 110545 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AFD2 RID: 110546 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "source")]
		public StringValue Source
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

		// Token: 0x1700975D RID: 38749
		// (get) Token: 0x0601AFD3 RID: 110547 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601AFD4 RID: 110548 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "target")]
		public StringValue Target
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700975E RID: 38750
		// (get) Token: 0x0601AFD5 RID: 110549 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601AFD6 RID: 110550 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "column")]
		public StringValue Column
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700975F RID: 38751
		// (get) Token: 0x0601AFD7 RID: 110551 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601AFD8 RID: 110552 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "variable")]
		public StringValue Variable
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x0601AFDA RID: 110554 RVA: 0x0036A5F8 File Offset: 0x003687F8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "source" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "target" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "column" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "variable" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AFDB RID: 110555 RVA: 0x0036A665 File Offset: 0x00368865
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Bind>(deep);
		}

		// Token: 0x0601AFDC RID: 110556 RVA: 0x0036A670 File Offset: 0x00368870
		// Note: this type is marked as 'beforefieldinit'.
		static Bind()
		{
			byte[] array = new byte[4];
			Bind.attributeNamespaceIds = array;
		}

		// Token: 0x0400B272 RID: 45682
		private const string tagName = "bind";

		// Token: 0x0400B273 RID: 45683
		private const byte tagNsId = 43;

		// Token: 0x0400B274 RID: 45684
		internal const int ElementTypeIdConst = 12644;

		// Token: 0x0400B275 RID: 45685
		private static string[] attributeTagNames = new string[] { "source", "target", "column", "variable" };

		// Token: 0x0400B276 RID: 45686
		private static byte[] attributeNamespaceIds;
	}
}
