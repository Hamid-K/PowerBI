using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001AC RID: 428
	[DataContract]
	[Serializable]
	public sealed class ScrubbedEntityPropertyReference : IContainsPrivateInformation
	{
		// Token: 0x06000B13 RID: 2835 RVA: 0x00026B74 File Offset: 0x00024D74
		public ScrubbedEntityPropertyReference(ScrubbedString scrubbedEntityReference, ScrubbedString scrubbedPropertyName, string schemaName = null)
		{
			this.m_scrubbedEntityReference = scrubbedEntityReference;
			this.m_scrubbedPropertyName = scrubbedPropertyName;
			this.m_schemaName = schemaName;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00026B91 File Offset: 0x00024D91
		public ScrubbedEntityPropertyReference(string entityReferenceName, string propertyReferenceName, string schemaName = null)
		{
			this.m_scrubbedEntityReference = new ScrubbedString(entityReferenceName);
			this.m_scrubbedPropertyName = new ScrubbedString(propertyReferenceName);
			this.m_schemaName = schemaName;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00026BB8 File Offset: 0x00024DB8
		public override string ToString()
		{
			return ScrubbedEntityPropertyReference.Format(this.m_schemaName, this.m_scrubbedEntityReference.ToString(), this.m_scrubbedPropertyName.ToString());
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00026BDB File Offset: 0x00024DDB
		public string ToPrivateString()
		{
			return ScrubbedEntityPropertyReference.Format(this.m_schemaName, this.m_scrubbedEntityReference.ToPrivateString(), this.m_scrubbedPropertyName.ToPrivateString());
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00026BFE File Offset: 0x00024DFE
		public string ToInternalString()
		{
			return ScrubbedEntityPropertyReference.Format(this.m_schemaName, this.m_scrubbedEntityReference.ToInternalString(), this.m_scrubbedPropertyName.ToInternalString());
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00026C21 File Offset: 0x00024E21
		public string ToOriginalString()
		{
			return ScrubbedEntityPropertyReference.Format(this.m_schemaName, this.m_scrubbedEntityReference.ToOriginalString(), this.m_scrubbedPropertyName.ToOriginalString());
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00026C44 File Offset: 0x00024E44
		private static string Format(string schemaName, string markedEntityReferenceName, string markedPropertyReferenceName)
		{
			if (string.IsNullOrWhiteSpace(schemaName))
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}[{1}]", new object[] { markedEntityReferenceName, markedPropertyReferenceName });
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}[{2}]", new object[] { schemaName, markedEntityReferenceName, markedPropertyReferenceName });
		}

		// Token: 0x0400045C RID: 1116
		private const string c_propertyReferenceTemplate = "{0}[{1}]";

		// Token: 0x0400045D RID: 1117
		private const string c_propertyReferenceWithSchemaTemplate = "{0}.{1}[{2}]";

		// Token: 0x0400045E RID: 1118
		[DataMember]
		private readonly ScrubbedString m_scrubbedEntityReference;

		// Token: 0x0400045F RID: 1119
		[DataMember]
		private readonly ScrubbedString m_scrubbedPropertyName;

		// Token: 0x04000460 RID: 1120
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		private readonly string m_schemaName;
	}
}
