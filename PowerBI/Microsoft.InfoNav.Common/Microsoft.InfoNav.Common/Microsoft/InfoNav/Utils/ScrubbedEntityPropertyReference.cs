using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Utils
{
	// Token: 0x02000034 RID: 52
	[DataContract]
	[Serializable]
	public sealed class ScrubbedEntityPropertyReference : IContainsTelemetryMarkup
	{
		// Token: 0x06000244 RID: 580 RVA: 0x00007102 File Offset: 0x00005302
		public ScrubbedEntityPropertyReference(ScrubbedString scrubbedEntityReference, ScrubbedString scrubbedPropertyName, string schemaName = null)
		{
			this.m_scrubbedEntityReference = scrubbedEntityReference;
			this.m_scrubbedPropertyName = scrubbedPropertyName;
			this.m_schemaName = schemaName;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000711F File Offset: 0x0000531F
		public ScrubbedEntityPropertyReference(string entityReferenceName, string propertyReferenceName, string schemaName = null)
		{
			this.m_scrubbedEntityReference = new ScrubbedString(entityReferenceName);
			this.m_scrubbedPropertyName = new ScrubbedString(propertyReferenceName);
			this.m_schemaName = schemaName;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007146 File Offset: 0x00005346
		public override string ToString()
		{
			return ScrubbedEntityPropertyReference.Format(this.m_schemaName, this.m_scrubbedEntityReference.ToString(), this.m_scrubbedPropertyName.ToString());
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00007169 File Offset: 0x00005369
		public string ToOriginalString()
		{
			return ScrubbedEntityPropertyReference.Format(this.m_schemaName, this.m_scrubbedEntityReference.ToOriginalString(), this.m_scrubbedPropertyName.ToOriginalString());
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000718C File Offset: 0x0000538C
		public string ToCustomerContentString()
		{
			return ScrubbedEntityPropertyReference.Format(this.m_schemaName, this.m_scrubbedEntityReference.ToCustomerContentString(), this.m_scrubbedPropertyName.ToCustomerContentString());
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000071AF File Offset: 0x000053AF
		public string ToEUIIString()
		{
			return ScrubbedEntityPropertyReference.Format(this.m_schemaName, this.m_scrubbedEntityReference.ToEUIIString(), this.m_scrubbedPropertyName.ToEUIIString());
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000071D2 File Offset: 0x000053D2
		public string ToEUPIString()
		{
			return string.Empty;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000071D9 File Offset: 0x000053D9
		public string ToIPString()
		{
			return ScrubbedEntityPropertyReference.Format(this.m_schemaName, this.m_scrubbedEntityReference.ToIPString(), this.m_scrubbedPropertyName.ToIPString());
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000071FC File Offset: 0x000053FC
		private static string Format(string schemaName, string markedEntityReferenceName, string markedPropertyReferenceName)
		{
			if (string.IsNullOrWhiteSpace(schemaName))
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}[{1}]", new object[] { markedEntityReferenceName, markedPropertyReferenceName });
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}[{2}]", new object[] { schemaName, markedEntityReferenceName, markedPropertyReferenceName });
		}

		// Token: 0x04000070 RID: 112
		private const string c_propertyReferenceTemplate = "{0}[{1}]";

		// Token: 0x04000071 RID: 113
		private const string c_propertyReferenceWithSchemaTemplate = "{0}.{1}[{2}]";

		// Token: 0x04000072 RID: 114
		[DataMember]
		private readonly ScrubbedString m_scrubbedEntityReference;

		// Token: 0x04000073 RID: 115
		[DataMember]
		private readonly ScrubbedString m_scrubbedPropertyName;

		// Token: 0x04000074 RID: 116
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		private readonly string m_schemaName;
	}
}
