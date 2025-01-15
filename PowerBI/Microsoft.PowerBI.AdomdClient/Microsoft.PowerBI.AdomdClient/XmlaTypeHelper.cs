using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200004C RID: 76
	internal static class XmlaTypeHelper
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x0001E358 File Offset: 0x0001C558
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

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001E4CD File Offset: 0x0001C6CD
		public static bool IsXmlaSupportedType(Type type)
		{
			return XmlaTypeHelper.netTypeMapping.ContainsKey(type);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001E4DA File Offset: 0x0001C6DA
		public static string GetXmlaType(Type type)
		{
			return XmlaTypeHelper.netTypeMapping[type];
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001E4E8 File Offset: 0x0001C6E8
		public static Type GetNetType(string xmlaType)
		{
			Type type;
			if (string.IsNullOrEmpty(xmlaType) || !XmlaTypeHelper.xmlaTypeMapping.TryGetValue(xmlaType, out type))
			{
				return null;
			}
			return type;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001E50F File Offset: 0x0001C70F
		public static Type GetNetTypeWithPrefix(string xmlaTypeWithPrefix)
		{
			return XmlaTypeHelper.GetNetType(string.IsNullOrEmpty(xmlaTypeWithPrefix) ? null : XmlaTypeHelper.GetXmlaTypeWitoutPrefix(xmlaTypeWithPrefix));
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001E528 File Offset: 0x0001C728
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

		// Token: 0x060004FA RID: 1274 RVA: 0x0001E570 File Offset: 0x0001C770
		private static string GetXmlaTypeWitoutPrefix(string xmlaTypeWithPrefix)
		{
			int num = xmlaTypeWithPrefix.IndexOf(':');
			if (num != -1)
			{
				return xmlaTypeWithPrefix.Substring(num + 1);
			}
			return xmlaTypeWithPrefix;
		}

		// Token: 0x040003C9 RID: 969
		private static readonly IDictionary<Type, string> netTypeMapping = new Dictionary<Type, string>();

		// Token: 0x040003CA RID: 970
		private static readonly IDictionary<string, Type> xmlaTypeMapping = new Dictionary<string, Type>();
	}
}
