using System;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020030D3 RID: 12499
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class SchemaAttrAttribute : Attribute
	{
		// Token: 0x0601B271 RID: 111217 RVA: 0x0036E68C File Offset: 0x0036C88C
		public SchemaAttrAttribute(byte nsId, string tag)
		{
			if (string.IsNullOrEmpty(tag))
			{
				throw new ArgumentNullException("tag");
			}
			this._nsId = nsId;
			this._tag = tag;
		}

		// Token: 0x17009867 RID: 39015
		// (get) Token: 0x0601B272 RID: 111218 RVA: 0x0036E6B5 File Offset: 0x0036C8B5
		public string Tag
		{
			get
			{
				return this._tag;
			}
		}

		// Token: 0x17009868 RID: 39016
		// (get) Token: 0x0601B273 RID: 111219 RVA: 0x0036E6BD File Offset: 0x0036C8BD
		public string NamespaceUri
		{
			get
			{
				return NamespaceIdMap.GetNamespaceUri(this._nsId);
			}
		}

		// Token: 0x0400B3E9 RID: 46057
		private string _tag;

		// Token: 0x0400B3EA RID: 46058
		private byte _nsId;
	}
}
