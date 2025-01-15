using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023EC RID: 9196
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Filter : OpenXmlLeafElement
	{
		// Token: 0x17004DD3 RID: 19923
		// (get) Token: 0x06010C00 RID: 68608 RVA: 0x002E6B13 File Offset: 0x002E4D13
		public override string LocalName
		{
			get
			{
				return "filter";
			}
		}

		// Token: 0x17004DD4 RID: 19924
		// (get) Token: 0x06010C01 RID: 68609 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004DD5 RID: 19925
		// (get) Token: 0x06010C02 RID: 68610 RVA: 0x002E6B1A File Offset: 0x002E4D1A
		internal override int ElementTypeId
		{
			get
			{
				return 12922;
			}
		}

		// Token: 0x06010C03 RID: 68611 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004DD6 RID: 19926
		// (get) Token: 0x06010C04 RID: 68612 RVA: 0x002E6B21 File Offset: 0x002E4D21
		internal override string[] AttributeTagNames
		{
			get
			{
				return Filter.attributeTagNames;
			}
		}

		// Token: 0x17004DD7 RID: 19927
		// (get) Token: 0x06010C05 RID: 68613 RVA: 0x002E6B28 File Offset: 0x002E4D28
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Filter.attributeNamespaceIds;
			}
		}

		// Token: 0x17004DD8 RID: 19928
		// (get) Token: 0x06010C06 RID: 68614 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010C07 RID: 68615 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public StringValue Val
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

		// Token: 0x06010C09 RID: 68617 RVA: 0x002E6B2F File Offset: 0x002E4D2F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010C0A RID: 68618 RVA: 0x002E6B4F File Offset: 0x002E4D4F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Filter>(deep);
		}

		// Token: 0x06010C0B RID: 68619 RVA: 0x002E6B58 File Offset: 0x002E4D58
		// Note: this type is marked as 'beforefieldinit'.
		static Filter()
		{
			byte[] array = new byte[1];
			Filter.attributeNamespaceIds = array;
		}

		// Token: 0x04007635 RID: 30261
		private const string tagName = "filter";

		// Token: 0x04007636 RID: 30262
		private const byte tagNsId = 53;

		// Token: 0x04007637 RID: 30263
		internal const int ElementTypeIdConst = 12922;

		// Token: 0x04007638 RID: 30264
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007639 RID: 30265
		private static byte[] attributeNamespaceIds;
	}
}
