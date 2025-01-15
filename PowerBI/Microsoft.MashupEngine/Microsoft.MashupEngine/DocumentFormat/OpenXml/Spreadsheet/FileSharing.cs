using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C51 RID: 11345
	[GeneratedCode("DomGen", "2.0")]
	internal class FileSharing : OpenXmlLeafElement
	{
		// Token: 0x17008201 RID: 33281
		// (get) Token: 0x06018126 RID: 98598 RVA: 0x0033E1FB File Offset: 0x0033C3FB
		public override string LocalName
		{
			get
			{
				return "fileSharing";
			}
		}

		// Token: 0x17008202 RID: 33282
		// (get) Token: 0x06018127 RID: 98599 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008203 RID: 33283
		// (get) Token: 0x06018128 RID: 98600 RVA: 0x0033E202 File Offset: 0x0033C402
		internal override int ElementTypeId
		{
			get
			{
				return 11326;
			}
		}

		// Token: 0x06018129 RID: 98601 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008204 RID: 33284
		// (get) Token: 0x0601812A RID: 98602 RVA: 0x0033E209 File Offset: 0x0033C409
		internal override string[] AttributeTagNames
		{
			get
			{
				return FileSharing.attributeTagNames;
			}
		}

		// Token: 0x17008205 RID: 33285
		// (get) Token: 0x0601812B RID: 98603 RVA: 0x0033E210 File Offset: 0x0033C410
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FileSharing.attributeNamespaceIds;
			}
		}

		// Token: 0x17008206 RID: 33286
		// (get) Token: 0x0601812C RID: 98604 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601812D RID: 98605 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "readOnlyRecommended")]
		public BooleanValue ReadOnlyRecommended
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

		// Token: 0x17008207 RID: 33287
		// (get) Token: 0x0601812E RID: 98606 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601812F RID: 98607 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "userName")]
		public StringValue UserName
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

		// Token: 0x17008208 RID: 33288
		// (get) Token: 0x06018130 RID: 98608 RVA: 0x002E82CD File Offset: 0x002E64CD
		// (set) Token: 0x06018131 RID: 98609 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "reservationPassword")]
		public HexBinaryValue ReservationPassword
		{
			get
			{
				return (HexBinaryValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008209 RID: 33289
		// (get) Token: 0x06018132 RID: 98610 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06018133 RID: 98611 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "algorithmName")]
		public StringValue AlgorithmName
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

		// Token: 0x1700820A RID: 33290
		// (get) Token: 0x06018134 RID: 98612 RVA: 0x0033E217 File Offset: 0x0033C417
		// (set) Token: 0x06018135 RID: 98613 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "hashValue")]
		public Base64BinaryValue HashValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700820B RID: 33291
		// (get) Token: 0x06018136 RID: 98614 RVA: 0x003286C4 File Offset: 0x003268C4
		// (set) Token: 0x06018137 RID: 98615 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "saltValue")]
		public Base64BinaryValue SaltValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700820C RID: 33292
		// (get) Token: 0x06018138 RID: 98616 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x06018139 RID: 98617 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "spinCount")]
		public UInt32Value SpinCount
		{
			get
			{
				return (UInt32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x0601813B RID: 98619 RVA: 0x0033E228 File Offset: 0x0033C428
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "readOnlyRecommended" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "userName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "reservationPassword" == name)
			{
				return new HexBinaryValue();
			}
			if (namespaceId == 0 && "algorithmName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "hashValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "saltValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "spinCount" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601813C RID: 98620 RVA: 0x0033E2D7 File Offset: 0x0033C4D7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FileSharing>(deep);
		}

		// Token: 0x0601813D RID: 98621 RVA: 0x0033E2E0 File Offset: 0x0033C4E0
		// Note: this type is marked as 'beforefieldinit'.
		static FileSharing()
		{
			byte[] array = new byte[7];
			FileSharing.attributeNamespaceIds = array;
		}

		// Token: 0x04009EC6 RID: 40646
		private const string tagName = "fileSharing";

		// Token: 0x04009EC7 RID: 40647
		private const byte tagNsId = 22;

		// Token: 0x04009EC8 RID: 40648
		internal const int ElementTypeIdConst = 11326;

		// Token: 0x04009EC9 RID: 40649
		private static string[] attributeTagNames = new string[] { "readOnlyRecommended", "userName", "reservationPassword", "algorithmName", "hashValue", "saltValue", "spinCount" };

		// Token: 0x04009ECA RID: 40650
		private static byte[] attributeNamespaceIds;
	}
}
