using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A0C RID: 10764
	[ChildElementInfo(typeof(Sound))]
	[GeneratedCode("DomGen", "2.0")]
	internal class StartSoundAction : OpenXmlCompositeElement
	{
		// Token: 0x17006F98 RID: 28568
		// (get) Token: 0x06015872 RID: 88178 RVA: 0x003203BF File Offset: 0x0031E5BF
		public override string LocalName
		{
			get
			{
				return "stSnd";
			}
		}

		// Token: 0x17006F99 RID: 28569
		// (get) Token: 0x06015873 RID: 88179 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006F9A RID: 28570
		// (get) Token: 0x06015874 RID: 88180 RVA: 0x003203C6 File Offset: 0x0031E5C6
		internal override int ElementTypeId
		{
			get
			{
				return 12189;
			}
		}

		// Token: 0x06015875 RID: 88181 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006F9B RID: 28571
		// (get) Token: 0x06015876 RID: 88182 RVA: 0x003203CD File Offset: 0x0031E5CD
		internal override string[] AttributeTagNames
		{
			get
			{
				return StartSoundAction.attributeTagNames;
			}
		}

		// Token: 0x17006F9C RID: 28572
		// (get) Token: 0x06015877 RID: 88183 RVA: 0x003203D4 File Offset: 0x0031E5D4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StartSoundAction.attributeNamespaceIds;
			}
		}

		// Token: 0x17006F9D RID: 28573
		// (get) Token: 0x06015878 RID: 88184 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06015879 RID: 88185 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "loop")]
		public BooleanValue Loop
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

		// Token: 0x0601587A RID: 88186 RVA: 0x00293ECF File Offset: 0x002920CF
		public StartSoundAction()
		{
		}

		// Token: 0x0601587B RID: 88187 RVA: 0x00293ED7 File Offset: 0x002920D7
		public StartSoundAction(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601587C RID: 88188 RVA: 0x00293EE0 File Offset: 0x002920E0
		public StartSoundAction(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601587D RID: 88189 RVA: 0x00293EE9 File Offset: 0x002920E9
		public StartSoundAction(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601587E RID: 88190 RVA: 0x003203DB File Offset: 0x0031E5DB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "snd" == name)
			{
				return new Sound();
			}
			return null;
		}

		// Token: 0x17006F9E RID: 28574
		// (get) Token: 0x0601587F RID: 88191 RVA: 0x003203F6 File Offset: 0x0031E5F6
		internal override string[] ElementTagNames
		{
			get
			{
				return StartSoundAction.eleTagNames;
			}
		}

		// Token: 0x17006F9F RID: 28575
		// (get) Token: 0x06015880 RID: 88192 RVA: 0x003203FD File Offset: 0x0031E5FD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return StartSoundAction.eleNamespaceIds;
			}
		}

		// Token: 0x17006FA0 RID: 28576
		// (get) Token: 0x06015881 RID: 88193 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006FA1 RID: 28577
		// (get) Token: 0x06015882 RID: 88194 RVA: 0x00320404 File Offset: 0x0031E604
		// (set) Token: 0x06015883 RID: 88195 RVA: 0x0032040D File Offset: 0x0031E60D
		public Sound Sound
		{
			get
			{
				return base.GetElement<Sound>(0);
			}
			set
			{
				base.SetElement<Sound>(0, value);
			}
		}

		// Token: 0x06015884 RID: 88196 RVA: 0x00320417 File Offset: 0x0031E617
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "loop" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015885 RID: 88197 RVA: 0x00320437 File Offset: 0x0031E637
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StartSoundAction>(deep);
		}

		// Token: 0x06015886 RID: 88198 RVA: 0x00320440 File Offset: 0x0031E640
		// Note: this type is marked as 'beforefieldinit'.
		static StartSoundAction()
		{
			byte[] array = new byte[1];
			StartSoundAction.attributeNamespaceIds = array;
			StartSoundAction.eleTagNames = new string[] { "snd" };
			StartSoundAction.eleNamespaceIds = new byte[] { 24 };
		}

		// Token: 0x040093B8 RID: 37816
		private const string tagName = "stSnd";

		// Token: 0x040093B9 RID: 37817
		private const byte tagNsId = 24;

		// Token: 0x040093BA RID: 37818
		internal const int ElementTypeIdConst = 12189;

		// Token: 0x040093BB RID: 37819
		private static string[] attributeTagNames = new string[] { "loop" };

		// Token: 0x040093BC RID: 37820
		private static byte[] attributeNamespaceIds;

		// Token: 0x040093BD RID: 37821
		private static readonly string[] eleTagNames;

		// Token: 0x040093BE RID: 37822
		private static readonly byte[] eleNamespaceIds;
	}
}
