using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002419 RID: 9241
	[ChildElementInfo(typeof(SetLevel), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SetLevels : OpenXmlCompositeElement
	{
		// Token: 0x17004F1F RID: 20255
		// (get) Token: 0x06010EE1 RID: 69345 RVA: 0x002E8B9F File Offset: 0x002E6D9F
		public override string LocalName
		{
			get
			{
				return "setLevels";
			}
		}

		// Token: 0x17004F20 RID: 20256
		// (get) Token: 0x06010EE2 RID: 69346 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F21 RID: 20257
		// (get) Token: 0x06010EE3 RID: 69347 RVA: 0x002E8BA6 File Offset: 0x002E6DA6
		internal override int ElementTypeId
		{
			get
			{
				return 12959;
			}
		}

		// Token: 0x06010EE4 RID: 69348 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004F22 RID: 20258
		// (get) Token: 0x06010EE5 RID: 69349 RVA: 0x002E8BAD File Offset: 0x002E6DAD
		internal override string[] AttributeTagNames
		{
			get
			{
				return SetLevels.attributeTagNames;
			}
		}

		// Token: 0x17004F23 RID: 20259
		// (get) Token: 0x06010EE6 RID: 69350 RVA: 0x002E8BB4 File Offset: 0x002E6DB4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SetLevels.attributeNamespaceIds;
			}
		}

		// Token: 0x17004F24 RID: 20260
		// (get) Token: 0x06010EE7 RID: 69351 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06010EE8 RID: 69352 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x06010EE9 RID: 69353 RVA: 0x00293ECF File Offset: 0x002920CF
		public SetLevels()
		{
		}

		// Token: 0x06010EEA RID: 69354 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SetLevels(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010EEB RID: 69355 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SetLevels(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010EEC RID: 69356 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SetLevels(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010EED RID: 69357 RVA: 0x002E8BBB File Offset: 0x002E6DBB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "setLevel" == name)
			{
				return new SetLevel();
			}
			return null;
		}

		// Token: 0x06010EEE RID: 69358 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010EEF RID: 69359 RVA: 0x002E8BD6 File Offset: 0x002E6DD6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SetLevels>(deep);
		}

		// Token: 0x06010EF0 RID: 69360 RVA: 0x002E8BE0 File Offset: 0x002E6DE0
		// Note: this type is marked as 'beforefieldinit'.
		static SetLevels()
		{
			byte[] array = new byte[1];
			SetLevels.attributeNamespaceIds = array;
		}

		// Token: 0x040076F2 RID: 30450
		private const string tagName = "setLevels";

		// Token: 0x040076F3 RID: 30451
		private const byte tagNsId = 53;

		// Token: 0x040076F4 RID: 30452
		internal const int ElementTypeIdConst = 12959;

		// Token: 0x040076F5 RID: 30453
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x040076F6 RID: 30454
		private static byte[] attributeNamespaceIds;
	}
}
