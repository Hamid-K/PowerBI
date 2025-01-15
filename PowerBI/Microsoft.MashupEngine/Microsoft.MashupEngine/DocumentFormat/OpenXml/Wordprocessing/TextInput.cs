using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F3A RID: 12090
	[ChildElementInfo(typeof(MaxLength))]
	[ChildElementInfo(typeof(Format))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DefaultTextBoxFormFieldString))]
	[ChildElementInfo(typeof(TextBoxFormFieldType))]
	internal class TextInput : OpenXmlCompositeElement
	{
		// Token: 0x17008F98 RID: 36760
		// (get) Token: 0x06019F09 RID: 106249 RVA: 0x0035A180 File Offset: 0x00358380
		public override string LocalName
		{
			get
			{
				return "textInput";
			}
		}

		// Token: 0x17008F99 RID: 36761
		// (get) Token: 0x06019F0A RID: 106250 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F9A RID: 36762
		// (get) Token: 0x06019F0B RID: 106251 RVA: 0x0035A187 File Offset: 0x00358387
		internal override int ElementTypeId
		{
			get
			{
				return 11735;
			}
		}

		// Token: 0x06019F0C RID: 106252 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019F0D RID: 106253 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextInput()
		{
		}

		// Token: 0x06019F0E RID: 106254 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextInput(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019F0F RID: 106255 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextInput(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019F10 RID: 106256 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextInput(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019F11 RID: 106257 RVA: 0x0035A190 File Offset: 0x00358390
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "type" == name)
			{
				return new TextBoxFormFieldType();
			}
			if (23 == namespaceId && "default" == name)
			{
				return new DefaultTextBoxFormFieldString();
			}
			if (23 == namespaceId && "maxLength" == name)
			{
				return new MaxLength();
			}
			if (23 == namespaceId && "format" == name)
			{
				return new Format();
			}
			return null;
		}

		// Token: 0x17008F9B RID: 36763
		// (get) Token: 0x06019F12 RID: 106258 RVA: 0x0035A1FE File Offset: 0x003583FE
		internal override string[] ElementTagNames
		{
			get
			{
				return TextInput.eleTagNames;
			}
		}

		// Token: 0x17008F9C RID: 36764
		// (get) Token: 0x06019F13 RID: 106259 RVA: 0x0035A205 File Offset: 0x00358405
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextInput.eleNamespaceIds;
			}
		}

		// Token: 0x17008F9D RID: 36765
		// (get) Token: 0x06019F14 RID: 106260 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008F9E RID: 36766
		// (get) Token: 0x06019F15 RID: 106261 RVA: 0x0035A20C File Offset: 0x0035840C
		// (set) Token: 0x06019F16 RID: 106262 RVA: 0x0035A215 File Offset: 0x00358415
		public TextBoxFormFieldType TextBoxFormFieldType
		{
			get
			{
				return base.GetElement<TextBoxFormFieldType>(0);
			}
			set
			{
				base.SetElement<TextBoxFormFieldType>(0, value);
			}
		}

		// Token: 0x17008F9F RID: 36767
		// (get) Token: 0x06019F17 RID: 106263 RVA: 0x0035A21F File Offset: 0x0035841F
		// (set) Token: 0x06019F18 RID: 106264 RVA: 0x0035A228 File Offset: 0x00358428
		public DefaultTextBoxFormFieldString DefaultTextBoxFormFieldString
		{
			get
			{
				return base.GetElement<DefaultTextBoxFormFieldString>(1);
			}
			set
			{
				base.SetElement<DefaultTextBoxFormFieldString>(1, value);
			}
		}

		// Token: 0x17008FA0 RID: 36768
		// (get) Token: 0x06019F19 RID: 106265 RVA: 0x0035A232 File Offset: 0x00358432
		// (set) Token: 0x06019F1A RID: 106266 RVA: 0x0035A23B File Offset: 0x0035843B
		public MaxLength MaxLength
		{
			get
			{
				return base.GetElement<MaxLength>(2);
			}
			set
			{
				base.SetElement<MaxLength>(2, value);
			}
		}

		// Token: 0x17008FA1 RID: 36769
		// (get) Token: 0x06019F1B RID: 106267 RVA: 0x0035A245 File Offset: 0x00358445
		// (set) Token: 0x06019F1C RID: 106268 RVA: 0x0035A24E File Offset: 0x0035844E
		public Format Format
		{
			get
			{
				return base.GetElement<Format>(3);
			}
			set
			{
				base.SetElement<Format>(3, value);
			}
		}

		// Token: 0x06019F1D RID: 106269 RVA: 0x0035A258 File Offset: 0x00358458
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextInput>(deep);
		}

		// Token: 0x0400AAFF RID: 43775
		private const string tagName = "textInput";

		// Token: 0x0400AB00 RID: 43776
		private const byte tagNsId = 23;

		// Token: 0x0400AB01 RID: 43777
		internal const int ElementTypeIdConst = 11735;

		// Token: 0x0400AB02 RID: 43778
		private static readonly string[] eleTagNames = new string[] { "type", "default", "maxLength", "format" };

		// Token: 0x0400AB03 RID: 43779
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
