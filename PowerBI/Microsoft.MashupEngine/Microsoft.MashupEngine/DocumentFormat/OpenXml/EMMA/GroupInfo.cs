using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x02003079 RID: 12409
	[GeneratedCode("DomGen", "2.0")]
	internal class GroupInfo : OpenXmlCompositeElement
	{
		// Token: 0x17009705 RID: 38661
		// (get) Token: 0x0601AF00 RID: 110336 RVA: 0x00369C21 File Offset: 0x00367E21
		public override string LocalName
		{
			get
			{
				return "group-info";
			}
		}

		// Token: 0x17009706 RID: 38662
		// (get) Token: 0x0601AF01 RID: 110337 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x17009707 RID: 38663
		// (get) Token: 0x0601AF02 RID: 110338 RVA: 0x00369C28 File Offset: 0x00367E28
		internal override int ElementTypeId
		{
			get
			{
				return 12678;
			}
		}

		// Token: 0x0601AF03 RID: 110339 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009708 RID: 38664
		// (get) Token: 0x0601AF04 RID: 110340 RVA: 0x00369C2F File Offset: 0x00367E2F
		internal override string[] AttributeTagNames
		{
			get
			{
				return GroupInfo.attributeTagNames;
			}
		}

		// Token: 0x17009709 RID: 38665
		// (get) Token: 0x0601AF05 RID: 110341 RVA: 0x00369C36 File Offset: 0x00367E36
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GroupInfo.attributeNamespaceIds;
			}
		}

		// Token: 0x1700970A RID: 38666
		// (get) Token: 0x0601AF06 RID: 110342 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AF07 RID: 110343 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x0601AF08 RID: 110344 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupInfo()
		{
		}

		// Token: 0x0601AF09 RID: 110345 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupInfo(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AF0A RID: 110346 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupInfo(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AF0B RID: 110347 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupInfo(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AF0C RID: 110348 RVA: 0x000020FA File Offset: 0x000002FA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			return null;
		}

		// Token: 0x0601AF0D RID: 110349 RVA: 0x00303BE4 File Offset: 0x00301DE4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AF0E RID: 110350 RVA: 0x00369C3D File Offset: 0x00367E3D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupInfo>(deep);
		}

		// Token: 0x0601AF0F RID: 110351 RVA: 0x00369C48 File Offset: 0x00367E48
		// Note: this type is marked as 'beforefieldinit'.
		static GroupInfo()
		{
			byte[] array = new byte[1];
			GroupInfo.attributeNamespaceIds = array;
		}

		// Token: 0x0400B231 RID: 45617
		private const string tagName = "group-info";

		// Token: 0x0400B232 RID: 45618
		private const byte tagNsId = 44;

		// Token: 0x0400B233 RID: 45619
		internal const int ElementTypeIdConst = 12678;

		// Token: 0x0400B234 RID: 45620
		private static string[] attributeTagNames = new string[] { "ref" };

		// Token: 0x0400B235 RID: 45621
		private static byte[] attributeNamespaceIds;
	}
}
