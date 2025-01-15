using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000064 RID: 100
	public sealed class LinguisticSchemaXmlContainer : IXmlSerializable
	{
		// Token: 0x06000179 RID: 377 RVA: 0x0000442F File Offset: 0x0000262F
		public LinguisticSchemaXmlContainer()
		{
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004437 File Offset: 0x00002637
		public LinguisticSchemaXmlContainer(ModelLinguisticSchema schema, bool writeOnly = false)
		{
			this._schema = schema;
			this._writeOnly = writeOnly;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000444D File Offset: 0x0000264D
		public ModelLinguisticSchema GetLinguisticSchema()
		{
			Contract.Check(!this._writeOnly, "Attempted to GetLinguisticSchema for write-only container.");
			return this._schema;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00004468 File Offset: 0x00002668
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader.ReadStartElement();
			this._schema = ModelLinguisticSchema.Load(reader);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000447C File Offset: 0x0000267C
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this._schema.WriteTo(writer);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000448A File Offset: 0x0000268A
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x040001B9 RID: 441
		private readonly bool _writeOnly;

		// Token: 0x040001BA RID: 442
		private ModelLinguisticSchema _schema;
	}
}
