using System;
using System.Data.Entity.Utilities;
using System.Runtime.Serialization;
using System.Xml;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200042B RID: 1067
	public class ProxyDataContractResolver : DataContractResolver
	{
		// Token: 0x060033D8 RID: 13272 RVA: 0x000A7803 File Offset: 0x000A5A03
		public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
		{
			Check.NotEmpty(typeName, "typeName");
			Check.NotEmpty(typeNamespace, "typeNamespace");
			Check.NotNull<Type>(declaredType, "declaredType");
			Check.NotNull<DataContractResolver>(knownTypeResolver, "knownTypeResolver");
			return knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, null);
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x000A7844 File Offset: 0x000A5A44
		public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
		{
			Check.NotNull<Type>(type, "type");
			Check.NotNull<Type>(declaredType, "declaredType");
			Check.NotNull<DataContractResolver>(knownTypeResolver, "knownTypeResolver");
			Type objectType = ObjectContext.GetObjectType(type);
			if (objectType != type)
			{
				XmlQualifiedName schemaTypeName = this._exporter.GetSchemaTypeName(objectType);
				XmlDictionary xmlDictionary = new XmlDictionary(2);
				typeName = new XmlDictionaryString(xmlDictionary, schemaTypeName.Name, 0);
				typeNamespace = new XmlDictionaryString(xmlDictionary, schemaTypeName.Namespace, 1);
				return true;
			}
			return knownTypeResolver.TryResolveType(type, declaredType, null, out typeName, out typeNamespace);
		}

		// Token: 0x040010C5 RID: 4293
		private readonly XsdDataContractExporter _exporter = new XsdDataContractExporter();
	}
}
