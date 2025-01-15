using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F39 RID: 12089
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DropDownListSelection))]
	[ChildElementInfo(typeof(DefaultDropDownListItemIndex))]
	[ChildElementInfo(typeof(ListEntryFormField))]
	internal class DropDownListFormField : OpenXmlCompositeElement
	{
		// Token: 0x17008F90 RID: 36752
		// (get) Token: 0x06019EF7 RID: 106231 RVA: 0x0035A093 File Offset: 0x00358293
		public override string LocalName
		{
			get
			{
				return "ddList";
			}
		}

		// Token: 0x17008F91 RID: 36753
		// (get) Token: 0x06019EF8 RID: 106232 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F92 RID: 36754
		// (get) Token: 0x06019EF9 RID: 106233 RVA: 0x0035A09A File Offset: 0x0035829A
		internal override int ElementTypeId
		{
			get
			{
				return 11734;
			}
		}

		// Token: 0x06019EFA RID: 106234 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019EFB RID: 106235 RVA: 0x00293ECF File Offset: 0x002920CF
		public DropDownListFormField()
		{
		}

		// Token: 0x06019EFC RID: 106236 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DropDownListFormField(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019EFD RID: 106237 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DropDownListFormField(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019EFE RID: 106238 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DropDownListFormField(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019EFF RID: 106239 RVA: 0x0035A0A4 File Offset: 0x003582A4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "result" == name)
			{
				return new DropDownListSelection();
			}
			if (23 == namespaceId && "default" == name)
			{
				return new DefaultDropDownListItemIndex();
			}
			if (23 == namespaceId && "listEntry" == name)
			{
				return new ListEntryFormField();
			}
			return null;
		}

		// Token: 0x17008F93 RID: 36755
		// (get) Token: 0x06019F00 RID: 106240 RVA: 0x0035A0FA File Offset: 0x003582FA
		internal override string[] ElementTagNames
		{
			get
			{
				return DropDownListFormField.eleTagNames;
			}
		}

		// Token: 0x17008F94 RID: 36756
		// (get) Token: 0x06019F01 RID: 106241 RVA: 0x0035A101 File Offset: 0x00358301
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DropDownListFormField.eleNamespaceIds;
			}
		}

		// Token: 0x17008F95 RID: 36757
		// (get) Token: 0x06019F02 RID: 106242 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008F96 RID: 36758
		// (get) Token: 0x06019F03 RID: 106243 RVA: 0x0035A108 File Offset: 0x00358308
		// (set) Token: 0x06019F04 RID: 106244 RVA: 0x0035A111 File Offset: 0x00358311
		public DropDownListSelection DropDownListSelection
		{
			get
			{
				return base.GetElement<DropDownListSelection>(0);
			}
			set
			{
				base.SetElement<DropDownListSelection>(0, value);
			}
		}

		// Token: 0x17008F97 RID: 36759
		// (get) Token: 0x06019F05 RID: 106245 RVA: 0x0035A11B File Offset: 0x0035831B
		// (set) Token: 0x06019F06 RID: 106246 RVA: 0x0035A124 File Offset: 0x00358324
		public DefaultDropDownListItemIndex DefaultDropDownListItemIndex
		{
			get
			{
				return base.GetElement<DefaultDropDownListItemIndex>(1);
			}
			set
			{
				base.SetElement<DefaultDropDownListItemIndex>(1, value);
			}
		}

		// Token: 0x06019F07 RID: 106247 RVA: 0x0035A12E File Offset: 0x0035832E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DropDownListFormField>(deep);
		}

		// Token: 0x0400AAFA RID: 43770
		private const string tagName = "ddList";

		// Token: 0x0400AAFB RID: 43771
		private const byte tagNsId = 23;

		// Token: 0x0400AAFC RID: 43772
		internal const int ElementTypeIdConst = 11734;

		// Token: 0x0400AAFD RID: 43773
		private static readonly string[] eleTagNames = new string[] { "result", "default", "listEntry" };

		// Token: 0x0400AAFE RID: 43774
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
