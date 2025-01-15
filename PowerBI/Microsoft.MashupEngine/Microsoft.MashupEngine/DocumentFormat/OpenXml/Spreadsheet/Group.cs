using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B65 RID: 11109
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GroupMembers))]
	internal class Group : OpenXmlCompositeElement
	{
		// Token: 0x170078FC RID: 30972
		// (get) Token: 0x06016D9C RID: 93596 RVA: 0x002C29FF File Offset: 0x002C0BFF
		public override string LocalName
		{
			get
			{
				return "group";
			}
		}

		// Token: 0x170078FD RID: 30973
		// (get) Token: 0x06016D9D RID: 93597 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170078FE RID: 30974
		// (get) Token: 0x06016D9E RID: 93598 RVA: 0x0032FCB7 File Offset: 0x0032DEB7
		internal override int ElementTypeId
		{
			get
			{
				return 11088;
			}
		}

		// Token: 0x06016D9F RID: 93599 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170078FF RID: 30975
		// (get) Token: 0x06016DA0 RID: 93600 RVA: 0x0032FCBE File Offset: 0x0032DEBE
		internal override string[] AttributeTagNames
		{
			get
			{
				return Group.attributeTagNames;
			}
		}

		// Token: 0x17007900 RID: 30976
		// (get) Token: 0x06016DA1 RID: 93601 RVA: 0x0032FCC5 File Offset: 0x0032DEC5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Group.attributeNamespaceIds;
			}
		}

		// Token: 0x17007901 RID: 30977
		// (get) Token: 0x06016DA2 RID: 93602 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016DA3 RID: 93603 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x17007902 RID: 30978
		// (get) Token: 0x06016DA4 RID: 93604 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06016DA5 RID: 93605 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "uniqueName")]
		public StringValue UniqueName
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

		// Token: 0x17007903 RID: 30979
		// (get) Token: 0x06016DA6 RID: 93606 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06016DA7 RID: 93607 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "caption")]
		public StringValue Caption
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

		// Token: 0x17007904 RID: 30980
		// (get) Token: 0x06016DA8 RID: 93608 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06016DA9 RID: 93609 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "uniqueParent")]
		public StringValue UniqueParent
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

		// Token: 0x17007905 RID: 30981
		// (get) Token: 0x06016DAA RID: 93610 RVA: 0x002C8292 File Offset: 0x002C6492
		// (set) Token: 0x06016DAB RID: 93611 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "id")]
		public Int32Value Id
		{
			get
			{
				return (Int32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06016DAC RID: 93612 RVA: 0x00293ECF File Offset: 0x002920CF
		public Group()
		{
		}

		// Token: 0x06016DAD RID: 93613 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Group(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016DAE RID: 93614 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Group(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016DAF RID: 93615 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Group(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016DB0 RID: 93616 RVA: 0x0032FCCC File Offset: 0x0032DECC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "groupMembers" == name)
			{
				return new GroupMembers();
			}
			return null;
		}

		// Token: 0x17007906 RID: 30982
		// (get) Token: 0x06016DB1 RID: 93617 RVA: 0x0032FCE7 File Offset: 0x0032DEE7
		internal override string[] ElementTagNames
		{
			get
			{
				return Group.eleTagNames;
			}
		}

		// Token: 0x17007907 RID: 30983
		// (get) Token: 0x06016DB2 RID: 93618 RVA: 0x0032FCEE File Offset: 0x0032DEEE
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Group.eleNamespaceIds;
			}
		}

		// Token: 0x17007908 RID: 30984
		// (get) Token: 0x06016DB3 RID: 93619 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007909 RID: 30985
		// (get) Token: 0x06016DB4 RID: 93620 RVA: 0x0032FCF5 File Offset: 0x0032DEF5
		// (set) Token: 0x06016DB5 RID: 93621 RVA: 0x0032FCFE File Offset: 0x0032DEFE
		public GroupMembers GroupMembers
		{
			get
			{
				return base.GetElement<GroupMembers>(0);
			}
			set
			{
				base.SetElement<GroupMembers>(0, value);
			}
		}

		// Token: 0x06016DB6 RID: 93622 RVA: 0x0032FD08 File Offset: 0x0032DF08
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "uniqueName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "caption" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "uniqueParent" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "id" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016DB7 RID: 93623 RVA: 0x0032FD8B File Offset: 0x0032DF8B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Group>(deep);
		}

		// Token: 0x06016DB8 RID: 93624 RVA: 0x0032FD94 File Offset: 0x0032DF94
		// Note: this type is marked as 'beforefieldinit'.
		static Group()
		{
			byte[] array = new byte[5];
			Group.attributeNamespaceIds = array;
			Group.eleTagNames = new string[] { "groupMembers" };
			Group.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009A2B RID: 39467
		private const string tagName = "group";

		// Token: 0x04009A2C RID: 39468
		private const byte tagNsId = 22;

		// Token: 0x04009A2D RID: 39469
		internal const int ElementTypeIdConst = 11088;

		// Token: 0x04009A2E RID: 39470
		private static string[] attributeTagNames = new string[] { "name", "uniqueName", "caption", "uniqueParent", "id" };

		// Token: 0x04009A2F RID: 39471
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009A30 RID: 39472
		private static readonly string[] eleTagNames;

		// Token: 0x04009A31 RID: 39473
		private static readonly byte[] eleNamespaceIds;
	}
}
