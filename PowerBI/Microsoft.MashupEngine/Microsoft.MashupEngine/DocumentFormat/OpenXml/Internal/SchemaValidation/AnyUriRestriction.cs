using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003145 RID: 12613
	[Serializable]
	internal class AnyUriRestriction : StringRestriction
	{
		// Token: 0x1700999E RID: 39326
		// (get) Token: 0x0601B58A RID: 112010 RVA: 0x002D07C1 File Offset: 0x002CE9C1
		// (set) Token: 0x0601B58B RID: 112011 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.AnyURI;
			}
			set
			{
			}
		}

		// Token: 0x1700999F RID: 39327
		// (get) Token: 0x0601B58C RID: 112012 RVA: 0x00376828 File Offset: 0x00374A28
		public override string ClrTypeName
		{
			get
			{
				return AnyUriRestriction.clrTypeName;
			}
		}

		// Token: 0x0601B58D RID: 112013 RVA: 0x00376830 File Offset: 0x00374A30
		public override bool ValidateValueType(OpenXmlSimpleType attributeValue)
		{
			Uri uri = null;
			string text = attributeValue.InnerText;
			if (text != null && text.Length > 0)
			{
				text = text.Trim(AnyUriRestriction.WhitespaceChars);
				if (text.Length == 0 || text.IndexOf("##", StringComparison.Ordinal) != -1)
				{
					return false;
				}
			}
			return Uri.TryCreate(text, UriKind.RelativeOrAbsolute, out uri);
		}

		// Token: 0x0400B53A RID: 46394
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[NonSerialized]
		private static readonly string clrTypeName = typeof(Uri).Name;

		// Token: 0x0400B53B RID: 46395
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[NonSerialized]
		private static char[] WhitespaceChars = new char[] { ' ', '\t', '\n', '\r' };
	}
}
