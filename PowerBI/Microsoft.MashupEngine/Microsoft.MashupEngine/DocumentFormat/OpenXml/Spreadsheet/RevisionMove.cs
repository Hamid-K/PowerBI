using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BB2 RID: 11186
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Undo))]
	[ChildElementInfo(typeof(RevisionCellChange))]
	[ChildElementInfo(typeof(RevisionFormat))]
	internal class RevisionMove : OpenXmlCompositeElement
	{
		// Token: 0x17007BB0 RID: 31664
		// (get) Token: 0x0601736C RID: 95084 RVA: 0x00333FCF File Offset: 0x003321CF
		public override string LocalName
		{
			get
			{
				return "rm";
			}
		}

		// Token: 0x17007BB1 RID: 31665
		// (get) Token: 0x0601736D RID: 95085 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007BB2 RID: 31666
		// (get) Token: 0x0601736E RID: 95086 RVA: 0x00333FD6 File Offset: 0x003321D6
		internal override int ElementTypeId
		{
			get
			{
				return 11157;
			}
		}

		// Token: 0x0601736F RID: 95087 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007BB3 RID: 31667
		// (get) Token: 0x06017370 RID: 95088 RVA: 0x00333FDD File Offset: 0x003321DD
		internal override string[] AttributeTagNames
		{
			get
			{
				return RevisionMove.attributeTagNames;
			}
		}

		// Token: 0x17007BB4 RID: 31668
		// (get) Token: 0x06017371 RID: 95089 RVA: 0x00333FE4 File Offset: 0x003321E4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RevisionMove.attributeNamespaceIds;
			}
		}

		// Token: 0x17007BB5 RID: 31669
		// (get) Token: 0x06017372 RID: 95090 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017373 RID: 95091 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rId")]
		public UInt32Value RevisionId
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

		// Token: 0x17007BB6 RID: 31670
		// (get) Token: 0x06017374 RID: 95092 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017375 RID: 95093 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ua")]
		public BooleanValue Ua
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

		// Token: 0x17007BB7 RID: 31671
		// (get) Token: 0x06017376 RID: 95094 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017377 RID: 95095 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "ra")]
		public BooleanValue Ra
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

		// Token: 0x17007BB8 RID: 31672
		// (get) Token: 0x06017378 RID: 95096 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06017379 RID: 95097 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "sheetId")]
		public UInt32Value SheetId
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007BB9 RID: 31673
		// (get) Token: 0x0601737A RID: 95098 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0601737B RID: 95099 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "source")]
		public StringValue Source
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007BBA RID: 31674
		// (get) Token: 0x0601737C RID: 95100 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0601737D RID: 95101 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "destination")]
		public StringValue Destination
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007BBB RID: 31675
		// (get) Token: 0x0601737E RID: 95102 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x0601737F RID: 95103 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "sourceSheetId")]
		public UInt32Value SourceSheetId
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

		// Token: 0x06017380 RID: 95104 RVA: 0x00293ECF File Offset: 0x002920CF
		public RevisionMove()
		{
		}

		// Token: 0x06017381 RID: 95105 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RevisionMove(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017382 RID: 95106 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RevisionMove(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017383 RID: 95107 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RevisionMove(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017384 RID: 95108 RVA: 0x00333FEC File Offset: 0x003321EC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "undo" == name)
			{
				return new Undo();
			}
			if (22 == namespaceId && "rcc" == name)
			{
				return new RevisionCellChange();
			}
			if (22 == namespaceId && "rfmt" == name)
			{
				return new RevisionFormat();
			}
			return null;
		}

		// Token: 0x06017385 RID: 95109 RVA: 0x00334044 File Offset: 0x00332244
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "ua" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "ra" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "sheetId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "source" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "destination" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sourceSheetId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017386 RID: 95110 RVA: 0x003340F3 File Offset: 0x003322F3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RevisionMove>(deep);
		}

		// Token: 0x06017387 RID: 95111 RVA: 0x003340FC File Offset: 0x003322FC
		// Note: this type is marked as 'beforefieldinit'.
		static RevisionMove()
		{
			byte[] array = new byte[7];
			RevisionMove.attributeNamespaceIds = array;
		}

		// Token: 0x04009B9B RID: 39835
		private const string tagName = "rm";

		// Token: 0x04009B9C RID: 39836
		private const byte tagNsId = 22;

		// Token: 0x04009B9D RID: 39837
		internal const int ElementTypeIdConst = 11157;

		// Token: 0x04009B9E RID: 39838
		private static string[] attributeTagNames = new string[] { "rId", "ua", "ra", "sheetId", "source", "destination", "sourceSheetId" };

		// Token: 0x04009B9F RID: 39839
		private static byte[] attributeNamespaceIds;
	}
}
