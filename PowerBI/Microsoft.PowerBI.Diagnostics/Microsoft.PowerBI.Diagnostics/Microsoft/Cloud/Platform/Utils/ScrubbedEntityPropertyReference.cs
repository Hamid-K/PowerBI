using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200000D RID: 13
	[DataContract]
	[Serializable]
	public sealed class ScrubbedEntityPropertyReference : IContainsTelemetryMarkup
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000021C3 File Offset: 0x000003C3
		public ScrubbedEntityPropertyReference(ScrubbedString scrubbedEntityReference, ScrubbedString scrubbedPropertyName, string schemaName = null)
		{
			this.m_scrubbedEntityReference = scrubbedEntityReference;
			this.m_scrubbedPropertyName = scrubbedPropertyName;
			this.m_schemaName = schemaName;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021E0 File Offset: 0x000003E0
		public ScrubbedEntityPropertyReference(string entityReferenceName, string propertyReferenceName, string schemaName = null)
		{
			this.m_scrubbedEntityReference = new ScrubbedString(entityReferenceName);
			this.m_scrubbedPropertyName = new ScrubbedString(propertyReferenceName);
			this.m_schemaName = schemaName;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002207 File Offset: 0x00000407
		public override string ToString()
		{
			return ScrubbedEntityPropertyReference.Format(this.m_schemaName, this.m_scrubbedEntityReference.ToString(), this.m_scrubbedPropertyName.ToString());
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000222A File Offset: 0x0000042A
		public string ToOriginalString()
		{
			return ScrubbedEntityPropertyReference.Format(this.m_schemaName, this.m_scrubbedEntityReference.ToOriginalString(), this.m_scrubbedPropertyName.ToOriginalString());
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000224D File Offset: 0x0000044D
		public string ToCustomerContentString()
		{
			return ScrubbedEntityPropertyReference.Format(this.m_schemaName, this.m_scrubbedEntityReference.ToCustomerContentString(), this.m_scrubbedPropertyName.ToCustomerContentString());
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002270 File Offset: 0x00000470
		public string ToEUIIString()
		{
			return ScrubbedEntityPropertyReference.Format(this.m_schemaName, this.m_scrubbedEntityReference.ToEUIIString(), this.m_scrubbedPropertyName.ToEUIIString());
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002293 File Offset: 0x00000493
		public string ToEUPIString()
		{
			return string.Empty;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000229A File Offset: 0x0000049A
		public string ToIPString()
		{
			return ScrubbedEntityPropertyReference.Format(this.m_schemaName, this.m_scrubbedEntityReference.ToIPString(), this.m_scrubbedPropertyName.ToIPString());
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022BD File Offset: 0x000004BD
		private static string Format(string schemaName, string markedEntityReferenceName, string markedPropertyReferenceName)
		{
			if (string.IsNullOrWhiteSpace(schemaName))
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}[{1}]", markedEntityReferenceName, markedPropertyReferenceName);
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}[{2}]", schemaName, markedEntityReferenceName, markedPropertyReferenceName);
		}

		// Token: 0x04000035 RID: 53
		private const string c_propertyReferenceTemplate = "{0}[{1}]";

		// Token: 0x04000036 RID: 54
		private const string c_propertyReferenceWithSchemaTemplate = "{0}.{1}[{2}]";

		// Token: 0x04000037 RID: 55
		[DataMember]
		private readonly ScrubbedString m_scrubbedEntityReference;

		// Token: 0x04000038 RID: 56
		[DataMember]
		private readonly ScrubbedString m_scrubbedPropertyName;

		// Token: 0x04000039 RID: 57
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		private readonly string m_schemaName;
	}
}
