using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200240F RID: 9231
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ArgumentDescriptions), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DefinedName : OpenXmlCompositeElement
	{
		// Token: 0x17004EE0 RID: 20192
		// (get) Token: 0x06010E4F RID: 69199 RVA: 0x002E8690 File Offset: 0x002E6890
		public override string LocalName
		{
			get
			{
				return "definedName";
			}
		}

		// Token: 0x17004EE1 RID: 20193
		// (get) Token: 0x06010E50 RID: 69200 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EE2 RID: 20194
		// (get) Token: 0x06010E51 RID: 69201 RVA: 0x002E8697 File Offset: 0x002E6897
		internal override int ElementTypeId
		{
			get
			{
				return 12949;
			}
		}

		// Token: 0x06010E52 RID: 69202 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004EE3 RID: 20195
		// (get) Token: 0x06010E53 RID: 69203 RVA: 0x002E869E File Offset: 0x002E689E
		internal override string[] AttributeTagNames
		{
			get
			{
				return DefinedName.attributeTagNames;
			}
		}

		// Token: 0x17004EE4 RID: 20196
		// (get) Token: 0x06010E54 RID: 69204 RVA: 0x002E86A5 File Offset: 0x002E68A5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DefinedName.attributeNamespaceIds;
			}
		}

		// Token: 0x17004EE5 RID: 20197
		// (get) Token: 0x06010E55 RID: 69205 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010E56 RID: 69206 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06010E57 RID: 69207 RVA: 0x00293ECF File Offset: 0x002920CF
		public DefinedName()
		{
		}

		// Token: 0x06010E58 RID: 69208 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DefinedName(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010E59 RID: 69209 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DefinedName(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010E5A RID: 69210 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DefinedName(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010E5B RID: 69211 RVA: 0x002E86AC File Offset: 0x002E68AC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "argumentDescriptions" == name)
			{
				return new ArgumentDescriptions();
			}
			return null;
		}

		// Token: 0x17004EE6 RID: 20198
		// (get) Token: 0x06010E5C RID: 69212 RVA: 0x002E86C7 File Offset: 0x002E68C7
		internal override string[] ElementTagNames
		{
			get
			{
				return DefinedName.eleTagNames;
			}
		}

		// Token: 0x17004EE7 RID: 20199
		// (get) Token: 0x06010E5D RID: 69213 RVA: 0x002E86CE File Offset: 0x002E68CE
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DefinedName.eleNamespaceIds;
			}
		}

		// Token: 0x17004EE8 RID: 20200
		// (get) Token: 0x06010E5E RID: 69214 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004EE9 RID: 20201
		// (get) Token: 0x06010E5F RID: 69215 RVA: 0x002E86D5 File Offset: 0x002E68D5
		// (set) Token: 0x06010E60 RID: 69216 RVA: 0x002E86DE File Offset: 0x002E68DE
		public ArgumentDescriptions ArgumentDescriptions
		{
			get
			{
				return base.GetElement<ArgumentDescriptions>(0);
			}
			set
			{
				base.SetElement<ArgumentDescriptions>(0, value);
			}
		}

		// Token: 0x06010E61 RID: 69217 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010E62 RID: 69218 RVA: 0x002E86E8 File Offset: 0x002E68E8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefinedName>(deep);
		}

		// Token: 0x06010E63 RID: 69219 RVA: 0x002E86F4 File Offset: 0x002E68F4
		// Note: this type is marked as 'beforefieldinit'.
		static DefinedName()
		{
			byte[] array = new byte[1];
			DefinedName.attributeNamespaceIds = array;
			DefinedName.eleTagNames = new string[] { "argumentDescriptions" };
			DefinedName.eleNamespaceIds = new byte[] { 53 };
		}

		// Token: 0x040076C2 RID: 30402
		private const string tagName = "definedName";

		// Token: 0x040076C3 RID: 30403
		private const byte tagNsId = 53;

		// Token: 0x040076C4 RID: 30404
		internal const int ElementTypeIdConst = 12949;

		// Token: 0x040076C5 RID: 30405
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x040076C6 RID: 30406
		private static byte[] attributeNamespaceIds;

		// Token: 0x040076C7 RID: 30407
		private static readonly string[] eleTagNames;

		// Token: 0x040076C8 RID: 30408
		private static readonly byte[] eleNamespaceIds;
	}
}
