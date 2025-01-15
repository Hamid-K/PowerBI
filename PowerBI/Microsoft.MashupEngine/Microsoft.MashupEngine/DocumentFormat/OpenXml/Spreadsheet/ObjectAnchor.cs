using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C40 RID: 11328
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(FromMarker), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ToMarker), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ObjectAnchor : OpenXmlCompositeElement
	{
		// Token: 0x17008195 RID: 33173
		// (get) Token: 0x06018014 RID: 98324 RVA: 0x0030B3F0 File Offset: 0x003095F0
		public override string LocalName
		{
			get
			{
				return "anchor";
			}
		}

		// Token: 0x17008196 RID: 33174
		// (get) Token: 0x06018015 RID: 98325 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008197 RID: 33175
		// (get) Token: 0x06018016 RID: 98326 RVA: 0x0033D93F File Offset: 0x0033BB3F
		internal override int ElementTypeId
		{
			get
			{
				return 11310;
			}
		}

		// Token: 0x06018017 RID: 98327 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17008198 RID: 33176
		// (get) Token: 0x06018018 RID: 98328 RVA: 0x0033D946 File Offset: 0x0033BB46
		internal override string[] AttributeTagNames
		{
			get
			{
				return ObjectAnchor.attributeTagNames;
			}
		}

		// Token: 0x17008199 RID: 33177
		// (get) Token: 0x06018019 RID: 98329 RVA: 0x0033D94D File Offset: 0x0033BB4D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ObjectAnchor.attributeNamespaceIds;
			}
		}

		// Token: 0x1700819A RID: 33178
		// (get) Token: 0x0601801A RID: 98330 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601801B RID: 98331 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "moveWithCells")]
		public BooleanValue MoveWithCells
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

		// Token: 0x1700819B RID: 33179
		// (get) Token: 0x0601801C RID: 98332 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601801D RID: 98333 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sizeWithCells")]
		public BooleanValue SizeWithCells
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

		// Token: 0x1700819C RID: 33180
		// (get) Token: 0x0601801E RID: 98334 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x0601801F RID: 98335 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "z-order")]
		public UInt32Value ZOrder
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06018020 RID: 98336 RVA: 0x00293ECF File Offset: 0x002920CF
		public ObjectAnchor()
		{
		}

		// Token: 0x06018021 RID: 98337 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ObjectAnchor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018022 RID: 98338 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ObjectAnchor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018023 RID: 98339 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ObjectAnchor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018024 RID: 98340 RVA: 0x0033D954 File Offset: 0x0033BB54
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "from" == name)
			{
				return new FromMarker();
			}
			if (22 == namespaceId && "to" == name)
			{
				return new ToMarker();
			}
			return null;
		}

		// Token: 0x1700819D RID: 33181
		// (get) Token: 0x06018025 RID: 98341 RVA: 0x0033D987 File Offset: 0x0033BB87
		internal override string[] ElementTagNames
		{
			get
			{
				return ObjectAnchor.eleTagNames;
			}
		}

		// Token: 0x1700819E RID: 33182
		// (get) Token: 0x06018026 RID: 98342 RVA: 0x0033D98E File Offset: 0x0033BB8E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ObjectAnchor.eleNamespaceIds;
			}
		}

		// Token: 0x1700819F RID: 33183
		// (get) Token: 0x06018027 RID: 98343 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170081A0 RID: 33184
		// (get) Token: 0x06018028 RID: 98344 RVA: 0x0033D995 File Offset: 0x0033BB95
		// (set) Token: 0x06018029 RID: 98345 RVA: 0x0033D99E File Offset: 0x0033BB9E
		public FromMarker FromMarker
		{
			get
			{
				return base.GetElement<FromMarker>(0);
			}
			set
			{
				base.SetElement<FromMarker>(0, value);
			}
		}

		// Token: 0x170081A1 RID: 33185
		// (get) Token: 0x0601802A RID: 98346 RVA: 0x0033D9A8 File Offset: 0x0033BBA8
		// (set) Token: 0x0601802B RID: 98347 RVA: 0x0033D9B1 File Offset: 0x0033BBB1
		public ToMarker ToMarker
		{
			get
			{
				return base.GetElement<ToMarker>(1);
			}
			set
			{
				base.SetElement<ToMarker>(1, value);
			}
		}

		// Token: 0x0601802C RID: 98348 RVA: 0x0033D9BC File Offset: 0x0033BBBC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "moveWithCells" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "sizeWithCells" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "z-order" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601802D RID: 98349 RVA: 0x0033DA13 File Offset: 0x0033BC13
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ObjectAnchor>(deep);
		}

		// Token: 0x0601802E RID: 98350 RVA: 0x0033DA1C File Offset: 0x0033BC1C
		// Note: this type is marked as 'beforefieldinit'.
		static ObjectAnchor()
		{
			byte[] array = new byte[3];
			ObjectAnchor.attributeNamespaceIds = array;
			ObjectAnchor.eleTagNames = new string[] { "from", "to" };
			ObjectAnchor.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x04009E76 RID: 40566
		private const string tagName = "anchor";

		// Token: 0x04009E77 RID: 40567
		private const byte tagNsId = 22;

		// Token: 0x04009E78 RID: 40568
		internal const int ElementTypeIdConst = 11310;

		// Token: 0x04009E79 RID: 40569
		private static string[] attributeTagNames = new string[] { "moveWithCells", "sizeWithCells", "z-order" };

		// Token: 0x04009E7A RID: 40570
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009E7B RID: 40571
		private static readonly string[] eleTagNames;

		// Token: 0x04009E7C RID: 40572
		private static readonly byte[] eleNamespaceIds;
	}
}
