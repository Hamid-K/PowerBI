using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B56 RID: 11094
	[ChildElementInfo(typeof(MemberPropertyIndex))]
	[GeneratedCode("DomGen", "2.0")]
	internal class BooleanItem : OpenXmlCompositeElement
	{
		// Token: 0x17007884 RID: 30852
		// (get) Token: 0x06016C8A RID: 93322 RVA: 0x0032F0BC File Offset: 0x0032D2BC
		public override string LocalName
		{
			get
			{
				return "b";
			}
		}

		// Token: 0x17007885 RID: 30853
		// (get) Token: 0x06016C8B RID: 93323 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007886 RID: 30854
		// (get) Token: 0x06016C8C RID: 93324 RVA: 0x0032F0C3 File Offset: 0x0032D2C3
		internal override int ElementTypeId
		{
			get
			{
				return 11077;
			}
		}

		// Token: 0x06016C8D RID: 93325 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007887 RID: 30855
		// (get) Token: 0x06016C8E RID: 93326 RVA: 0x0032F0CA File Offset: 0x0032D2CA
		internal override string[] AttributeTagNames
		{
			get
			{
				return BooleanItem.attributeTagNames;
			}
		}

		// Token: 0x17007888 RID: 30856
		// (get) Token: 0x06016C8F RID: 93327 RVA: 0x0032F0D1 File Offset: 0x0032D2D1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BooleanItem.attributeNamespaceIds;
			}
		}

		// Token: 0x17007889 RID: 30857
		// (get) Token: 0x06016C90 RID: 93328 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06016C91 RID: 93329 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "v")]
		public BooleanValue Val
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

		// Token: 0x1700788A RID: 30858
		// (get) Token: 0x06016C92 RID: 93330 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06016C93 RID: 93331 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "u")]
		public BooleanValue Unused
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

		// Token: 0x1700788B RID: 30859
		// (get) Token: 0x06016C94 RID: 93332 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06016C95 RID: 93333 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "f")]
		public BooleanValue Calculated
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700788C RID: 30860
		// (get) Token: 0x06016C96 RID: 93334 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06016C97 RID: 93335 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "c")]
		public StringValue Caption
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

		// Token: 0x1700788D RID: 30861
		// (get) Token: 0x06016C98 RID: 93336 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06016C99 RID: 93337 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "cp")]
		public UInt32Value PropertyCount
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06016C9A RID: 93338 RVA: 0x00293ECF File Offset: 0x002920CF
		public BooleanItem()
		{
		}

		// Token: 0x06016C9B RID: 93339 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BooleanItem(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016C9C RID: 93340 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BooleanItem(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016C9D RID: 93341 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BooleanItem(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016C9E RID: 93342 RVA: 0x0032F0D8 File Offset: 0x0032D2D8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "x" == name)
			{
				return new MemberPropertyIndex();
			}
			return null;
		}

		// Token: 0x06016C9F RID: 93343 RVA: 0x0032F0F4 File Offset: 0x0032D2F4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "v" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "u" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "f" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "c" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cp" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016CA0 RID: 93344 RVA: 0x0032F177 File Offset: 0x0032D377
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BooleanItem>(deep);
		}

		// Token: 0x06016CA1 RID: 93345 RVA: 0x0032F180 File Offset: 0x0032D380
		// Note: this type is marked as 'beforefieldinit'.
		static BooleanItem()
		{
			byte[] array = new byte[5];
			BooleanItem.attributeNamespaceIds = array;
		}

		// Token: 0x040099EC RID: 39404
		private const string tagName = "b";

		// Token: 0x040099ED RID: 39405
		private const byte tagNsId = 22;

		// Token: 0x040099EE RID: 39406
		internal const int ElementTypeIdConst = 11077;

		// Token: 0x040099EF RID: 39407
		private static string[] attributeTagNames = new string[] { "v", "u", "f", "c", "cp" };

		// Token: 0x040099F0 RID: 39408
		private static byte[] attributeNamespaceIds;
	}
}
