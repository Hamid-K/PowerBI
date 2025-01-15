using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine1.Library.Xml;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A4A RID: 2634
	internal static class CookieHelper
	{
		// Token: 0x06004940 RID: 18752 RVA: 0x000F54A8 File Offset: 0x000F36A8
		public static bool TryDeserializeCookies(string xml, out CookieCollection collection)
		{
			collection = null;
			if (!CookieHelper.HasXmlDeclaration(xml))
			{
				return false;
			}
			SerializeableCookie[] array = null;
			try
			{
				XmlReader xmlReader = XmlHelperUtility.XmlReaderCreate(new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(xml))), XmlModuleHelper.DefaultXmlReaderSettings);
				array = CookieHelper.serializer.Deserialize(xmlReader) as SerializeableCookie[];
			}
			catch (InvalidOperationException)
			{
				return false;
			}
			collection = new CookieCollection();
			if (array != null)
			{
				foreach (SerializeableCookie serializeableCookie in array)
				{
					collection.Add(new Cookie(serializeableCookie.Name, serializeableCookie.Value, serializeableCookie.Path, serializeableCookie.Domain));
				}
			}
			return true;
		}

		// Token: 0x06004941 RID: 18753 RVA: 0x000F5560 File Offset: 0x000F3760
		internal static bool HasXmlDeclaration(string xml)
		{
			return xml.StartsWith("<?xml", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x000F5570 File Offset: 0x000F3770
		internal static string Serialize(SerializeableCookie[] serializeableCookies)
		{
			TextWriter textWriter = new StringWriter(CultureInfo.InvariantCulture);
			CookieHelper.serializer.Serialize(textWriter, serializeableCookies);
			return textWriter.ToString();
		}

		// Token: 0x04002740 RID: 10048
		private static readonly XmlSerializer serializer = new XmlSerializer(typeof(SerializeableCookie[]));
	}
}
