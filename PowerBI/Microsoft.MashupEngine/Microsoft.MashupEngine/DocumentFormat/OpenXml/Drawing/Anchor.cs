using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027B5 RID: 10165
	[GeneratedCode("DomGen", "2.0")]
	internal class Anchor : OpenXmlLeafElement
	{
		// Token: 0x17006333 RID: 25395
		// (get) Token: 0x06013BD0 RID: 80848 RVA: 0x0030B3F0 File Offset: 0x003095F0
		public override string LocalName
		{
			get
			{
				return "anchor";
			}
		}

		// Token: 0x17006334 RID: 25396
		// (get) Token: 0x06013BD1 RID: 80849 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006335 RID: 25397
		// (get) Token: 0x06013BD2 RID: 80850 RVA: 0x0030B3F7 File Offset: 0x003095F7
		internal override int ElementTypeId
		{
			get
			{
				return 10198;
			}
		}

		// Token: 0x06013BD3 RID: 80851 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006336 RID: 25398
		// (get) Token: 0x06013BD4 RID: 80852 RVA: 0x0030B3FE File Offset: 0x003095FE
		internal override string[] AttributeTagNames
		{
			get
			{
				return Anchor.attributeTagNames;
			}
		}

		// Token: 0x17006337 RID: 25399
		// (get) Token: 0x06013BD5 RID: 80853 RVA: 0x0030B405 File Offset: 0x00309605
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Anchor.attributeNamespaceIds;
			}
		}

		// Token: 0x17006338 RID: 25400
		// (get) Token: 0x06013BD6 RID: 80854 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x06013BD7 RID: 80855 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "x")]
		public Int64Value X
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006339 RID: 25401
		// (get) Token: 0x06013BD8 RID: 80856 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x06013BD9 RID: 80857 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "y")]
		public Int64Value Y
		{
			get
			{
				return (Int64Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700633A RID: 25402
		// (get) Token: 0x06013BDA RID: 80858 RVA: 0x002E0CD2 File Offset: 0x002DEED2
		// (set) Token: 0x06013BDB RID: 80859 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "z")]
		public Int64Value Z
		{
			get
			{
				return (Int64Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06013BDD RID: 80861 RVA: 0x0030B40C File Offset: 0x0030960C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "x" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "y" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "z" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013BDE RID: 80862 RVA: 0x0030B463 File Offset: 0x00309663
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Anchor>(deep);
		}

		// Token: 0x06013BDF RID: 80863 RVA: 0x0030B46C File Offset: 0x0030966C
		// Note: this type is marked as 'beforefieldinit'.
		static Anchor()
		{
			byte[] array = new byte[3];
			Anchor.attributeNamespaceIds = array;
		}

		// Token: 0x04008787 RID: 34695
		private const string tagName = "anchor";

		// Token: 0x04008788 RID: 34696
		private const byte tagNsId = 10;

		// Token: 0x04008789 RID: 34697
		internal const int ElementTypeIdConst = 10198;

		// Token: 0x0400878A RID: 34698
		private static string[] attributeTagNames = new string[] { "x", "y", "z" };

		// Token: 0x0400878B RID: 34699
		private static byte[] attributeNamespaceIds;
	}
}
