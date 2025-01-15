using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000069 RID: 105
	internal static class XmlaTypeHelper
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x0002204C File Offset: 0x0002024C
		static XmlaTypeHelper()
		{
			XmlaTypeHelper.AddTypeToMappings(typeof(string), "xsd:string");
			XmlaTypeHelper.AddTypeToMappings(typeof(bool), "xsd:boolean");
			XmlaTypeHelper.AddTypeToMappings(typeof(decimal), "xsd:decimal");
			XmlaTypeHelper.AddTypeToMappings(typeof(float), "xsd:float");
			XmlaTypeHelper.AddTypeToMappings(typeof(double), "xsd:double");
			XmlaTypeHelper.AddTypeToMappings(typeof(sbyte), "xsd:byte");
			XmlaTypeHelper.AddTypeToMappings(typeof(byte), "xsd:unsignedByte");
			XmlaTypeHelper.AddTypeToMappings(typeof(short), "xsd:short");
			XmlaTypeHelper.AddTypeToMappings(typeof(ushort), "xsd:unsignedShort");
			XmlaTypeHelper.AddTypeToMappings(typeof(int), "xsd:int");
			XmlaTypeHelper.AddTypeToMappings(typeof(uint), "xsd:unsignedInt");
			XmlaTypeHelper.AddTypeToMappings(typeof(long), "xsd:long");
			XmlaTypeHelper.AddTypeToMappings(typeof(ulong), "xsd:unsignedLong");
			XmlaTypeHelper.AddTypeToMappings(typeof(DateTime), "xsd:dateTime");
			XmlaTypeHelper.AddTypeToMappings(typeof(Guid), "uuid");
			XmlaTypeHelper.AddTypeToMappings(typeof(byte[]), "xsd:base64Binary");
			XmlaTypeHelper.AddTypeToMappings(typeof(DBNull), string.Empty);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000221C1 File Offset: 0x000203C1
		public static string GetXmlaType(Type type)
		{
			return XmlaTypeHelper.netTypeMapping[type];
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000221D0 File Offset: 0x000203D0
		public static Type GetNetType(string xmlaType)
		{
			Type type;
			if (string.IsNullOrEmpty(xmlaType) || !XmlaTypeHelper.xmlaTypeMapping.TryGetValue(xmlaType, out type))
			{
				return null;
			}
			return type;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000221F8 File Offset: 0x000203F8
		private static void AddTypeToMappings(Type type, string xmlaTypeWithPrefix)
		{
			if (string.IsNullOrEmpty(xmlaTypeWithPrefix))
			{
				XmlaTypeHelper.netTypeMapping.Add(type, string.Empty);
				return;
			}
			string xmlaTypeWitoutPrefix = XmlaTypeHelper.GetXmlaTypeWitoutPrefix(xmlaTypeWithPrefix);
			XmlaTypeHelper.netTypeMapping.Add(type, xmlaTypeWithPrefix);
			XmlaTypeHelper.xmlaTypeMapping.Add(xmlaTypeWitoutPrefix, type);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00022240 File Offset: 0x00020440
		private static string GetXmlaTypeWitoutPrefix(string xmlaTypeWithPrefix)
		{
			int num = xmlaTypeWithPrefix.IndexOf(':');
			if (num != -1)
			{
				return xmlaTypeWithPrefix.Substring(num + 1);
			}
			return xmlaTypeWithPrefix;
		}

		// Token: 0x04000405 RID: 1029
		private static readonly IDictionary<Type, string> netTypeMapping = new Dictionary<Type, string>();

		// Token: 0x04000406 RID: 1030
		private static readonly IDictionary<string, Type> xmlaTypeMapping = new Dictionary<string, Type>();
	}
}
