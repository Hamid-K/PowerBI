using System;
using System.Diagnostics;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003147 RID: 12615
	[Serializable]
	internal class Base64BinaryRestriction : StringRestriction
	{
		// Token: 0x170099A2 RID: 39330
		// (get) Token: 0x0601B597 RID: 112023 RVA: 0x0000244F File Offset: 0x0000064F
		// (set) Token: 0x0601B598 RID: 112024 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.Base64Binary;
			}
			set
			{
			}
		}

		// Token: 0x170099A3 RID: 39331
		// (get) Token: 0x0601B599 RID: 112025 RVA: 0x00376922 File Offset: 0x00374B22
		public override string ClrTypeName
		{
			get
			{
				return Base64BinaryRestriction.clrTypeName;
			}
		}

		// Token: 0x0601B59A RID: 112026 RVA: 0x0037692C File Offset: 0x00374B2C
		public override bool ValidateValueType(OpenXmlSimpleType attributeValue)
		{
			if (attributeValue.InnerText == null)
			{
				return false;
			}
			if (attributeValue.InnerText.Length == 0)
			{
				return true;
			}
			try
			{
				Convert.FromBase64String(attributeValue.InnerText);
			}
			catch (FormatException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0601B59B RID: 112027 RVA: 0x00376978 File Offset: 0x00374B78
		internal override int GetValueLength(OpenXmlSimpleType attributeValue)
		{
			byte[] array = Convert.FromBase64String(attributeValue.InnerText);
			return array.Length;
		}

		// Token: 0x0400B53D RID: 46397
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[NonSerialized]
		private static readonly string clrTypeName = ValidationResources.TypeName_base64Binary;
	}
}
