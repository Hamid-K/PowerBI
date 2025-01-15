using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000523 RID: 1315
	public static class XmlUtils
	{
		// Token: 0x06001D46 RID: 7494 RVA: 0x00056F7C File Offset: 0x0005517C
		private static XElement KeyValuePairToXML<TKey, TValue>(KeyValuePair<TKey, TValue> pair)
		{
			return new XElement("KeyValuePair", new object[]
			{
				new XElement("Key", XmlUtils.ObjectToXml(pair.Key)),
				new XElement("Value", XmlUtils.ObjectToXml(pair.Value))
			});
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x00056FE4 File Offset: 0x000551E4
		public static XNode ObjectToXml(object obj)
		{
			IRenderableLiteral renderableLiteral = obj as IRenderableLiteral;
			if (renderableLiteral != null)
			{
				return renderableLiteral.RenderXML();
			}
			return XmlUtils._ObjectToXml(obj);
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x00057008 File Offset: 0x00055208
		private static XNode _ObjectToXml(object obj)
		{
			if (obj == null)
			{
				return new XCData(obj.ToLiteral(null));
			}
			Type type = obj.GetType();
			if (type.IsArray || obj is IList)
			{
				return obj.ToEnumerable<object>().CollectionToXML("Array", "Item", ObjectFormatting.Literal, null, Array.Empty<Func<object, XAttribute>>());
			}
			if (obj is IDictionary)
			{
				return obj.ToEnumerable<object>().CollectionToXML("Dictionary", "Item", ObjectFormatting.Literal, null, Array.Empty<Func<object, XAttribute>>());
			}
			IOptional optional = obj as IOptional;
			if (optional != null)
			{
				if (!optional.HasValue)
				{
					return new XElement("Nothing");
				}
				return new XElement("Some", XmlUtils.ObjectToXml(optional.Value));
			}
			else
			{
				if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<, >))
				{
					return (XElement)typeof(XmlUtils).GetMethod("KeyValuePairToXML", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(type.GenericTypeArguments).Invoke(null, new object[] { obj });
				}
				int? recordArity = type.GetRecordArity();
				if (recordArity == null)
				{
					return new XCData(obj.ToLiteral(null));
				}
				return Enumerable.Range(0, recordArity.Value).Select(new Func<int, object>(obj.GetRecordItem)).ToArray<object>()
					.CollectionToXML("Tuple", "Item", ObjectFormatting.Literal, null, Array.Empty<Func<object, XAttribute>>());
			}
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x0005716C File Offset: 0x0005536C
		public static XElement CollectionToXML<T>(this IEnumerable<T> seq, string name, string itemName = "Item", ObjectFormatting formatting = ObjectFormatting.Literal, Dictionary<object, int> identityCache = null, params Func<T, XAttribute>[] attributes)
		{
			T[] array = (seq as T[]) ?? seq.ToArray<T>();
			XElement[] array2 = new XElement[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				T t = array[i];
				array2[i] = new XElement(itemName, XmlUtils.ObjectToXml(t)).WithAttribute("i", i);
				foreach (Func<T, XAttribute> func in attributes)
				{
					array2[i].Add(func(t));
				}
			}
			XName xname = name;
			object[] array3 = array2;
			return new XElement(xname, array3).WithAttribute("size", array.Length);
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x00057222 File Offset: 0x00055422
		public static XElement WithAttribute(this XElement element, string attrName, object value)
		{
			element.SetAttributeValue(attrName, value);
			return element;
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x00057234 File Offset: 0x00055434
		public static T DeserializeFromXml<T>(XElement serializedObject)
		{
			if (serializedObject == null)
			{
				throw new ArgumentException("serializedObject cannot be null.", "serializedObject");
			}
			DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
			T t;
			using (XmlReader xmlReader = serializedObject.CreateReader())
			{
				t = (T)((object)dataContractSerializer.ReadObject(xmlReader, true));
			}
			return t;
		}

		// Token: 0x04000E36 RID: 3638
		public const string XmlReferenceElementName = "Reference";

		// Token: 0x04000E37 RID: 3639
		public const string XmlObjectIdAttributeName = "ObjectID";

		// Token: 0x04000E38 RID: 3640
		public const string XmlObjectTypeNameAttributeName = "ObjectTypeName";
	}
}
