using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200004C RID: 76
	internal static class XmlaTypeHelper
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x0001E688 File Offset: 0x0001C888
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

		// Token: 0x06000502 RID: 1282 RVA: 0x0001E7FD File Offset: 0x0001C9FD
		public static bool IsXmlaSupportedType(Type type)
		{
			return XmlaTypeHelper.netTypeMapping.ContainsKey(type);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001E80A File Offset: 0x0001CA0A
		public static string GetXmlaType(Type type)
		{
			return XmlaTypeHelper.netTypeMapping[type];
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001E818 File Offset: 0x0001CA18
		public static Type GetNetType(string xmlaType)
		{
			Type type;
			if (string.IsNullOrEmpty(xmlaType) || !XmlaTypeHelper.xmlaTypeMapping.TryGetValue(xmlaType, out type))
			{
				return null;
			}
			return type;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001E83F File Offset: 0x0001CA3F
		public static Type GetNetTypeWithPrefix(string xmlaTypeWithPrefix)
		{
			return XmlaTypeHelper.GetNetType(string.IsNullOrEmpty(xmlaTypeWithPrefix) ? null : XmlaTypeHelper.GetXmlaTypeWitoutPrefix(xmlaTypeWithPrefix));
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001E858 File Offset: 0x0001CA58
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

		// Token: 0x06000507 RID: 1287 RVA: 0x0001E8A0 File Offset: 0x0001CAA0
		private static string GetXmlaTypeWitoutPrefix(string xmlaTypeWithPrefix)
		{
			int num = xmlaTypeWithPrefix.IndexOf(':');
			if (num != -1)
			{
				return xmlaTypeWithPrefix.Substring(num + 1);
			}
			return xmlaTypeWithPrefix;
		}

		// Token: 0x040003D6 RID: 982
		private static readonly IDictionary<Type, string> netTypeMapping = new Dictionary<Type, string>();

		// Token: 0x040003D7 RID: 983
		private static readonly IDictionary<string, Type> xmlaTypeMapping = new Dictionary<string, Type>();
	}
}
