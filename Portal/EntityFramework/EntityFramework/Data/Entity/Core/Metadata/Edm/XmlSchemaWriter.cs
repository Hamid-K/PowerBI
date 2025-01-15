using System;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000505 RID: 1285
	internal abstract class XmlSchemaWriter
	{
		// Token: 0x06003F89 RID: 16265 RVA: 0x000D3918 File Offset: 0x000D1B18
		internal void WriteComment(string comment)
		{
			if (!string.IsNullOrEmpty(comment))
			{
				this._xmlWriter.WriteComment(comment);
			}
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x000D392E File Offset: 0x000D1B2E
		internal virtual void WriteEndElement()
		{
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x000D393B File Offset: 0x000D1B3B
		protected static string GetQualifiedTypeName(string prefix, string typeName)
		{
			return new StringBuilder().Append(prefix).Append(".").Append(typeName)
				.ToString();
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x000D395D File Offset: 0x000D1B5D
		internal static string GetLowerCaseStringFromBoolValue(bool value)
		{
			if (!value)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x04001630 RID: 5680
		protected XmlWriter _xmlWriter;

		// Token: 0x04001631 RID: 5681
		protected double _version;
	}
}
