using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200008F RID: 143
	[EditorBrowsable(1)]
	public sealed class UserConfigurationDictionary : ComplexProperty, IEnumerable, IJsonCollectionDeserializer
	{
		// Token: 0x0600065E RID: 1630 RVA: 0x000157B2 File Offset: 0x000147B2
		internal UserConfigurationDictionary()
		{
			this.dictionary = new Dictionary<object, object>();
		}

		// Token: 0x17000161 RID: 353
		public object this[object key]
		{
			get
			{
				return this.dictionary[key];
			}
			set
			{
				this.ValidateEntry(key, value);
				this.dictionary[key] = value;
				this.Changed();
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000157F0 File Offset: 0x000147F0
		public void Add(object key, object value)
		{
			this.ValidateEntry(key, value);
			this.dictionary.Add(key, value);
			this.Changed();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001580D File Offset: 0x0001480D
		public bool ContainsKey(object key)
		{
			return this.dictionary.ContainsKey(key);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001581C File Offset: 0x0001481C
		public bool Remove(object key)
		{
			bool flag = this.dictionary.Remove(key);
			if (flag)
			{
				this.Changed();
			}
			return flag;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00015840 File Offset: 0x00014840
		public bool TryGetValue(object key, out object value)
		{
			return this.dictionary.TryGetValue(key, ref value);
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001584F File Offset: 0x0001484F
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001585C File Offset: 0x0001485C
		public void Clear()
		{
			if (this.dictionary.Count != 0)
			{
				this.dictionary.Clear();
				this.Changed();
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001587C File Offset: 0x0001487C
		public IEnumerator GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001588E File Offset: 0x0001488E
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x00015896 File Offset: 0x00014896
		internal bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
			set
			{
				this.isDirty = value;
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001589F File Offset: 0x0001489F
		internal override void Changed()
		{
			base.Changed();
			this.isDirty = true;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x000158B0 File Offset: 0x000148B0
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			EwsUtilities.Assert(writer != null, "UserConfigurationDictionary.WriteElementsToXml", "writer is null");
			foreach (KeyValuePair<object, object> keyValuePair in this.dictionary)
			{
				writer.WriteStartElement(XmlNamespace.Types, "DictionaryEntry");
				this.WriteObjectToXml(writer, "DictionaryKey", keyValuePair.Key);
				this.WriteObjectToXml(writer, "DictionaryValue", keyValuePair.Value);
				writer.WriteEndElement();
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001594C File Offset: 0x0001494C
		internal override object InternalToJson(ExchangeService service)
		{
			List<object> list = new List<object>();
			foreach (KeyValuePair<object, object> keyValuePair in this.dictionary)
			{
				JsonObject jsonObject = new JsonObject();
				jsonObject.Add("DictionaryKey", this.GetJsonObject(keyValuePair.Key, service));
				jsonObject.Add("DictionaryValue", this.GetJsonObject(keyValuePair.Value, service));
				list.Add(jsonObject);
			}
			return list.ToArray();
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000159E4 File Offset: 0x000149E4
		private static void GetTypeCode(ExchangeServiceBase service, object dictionaryObject, ref UserConfigurationDictionaryObjectType dictionaryObjectType, ref string valueAsString)
		{
			TypeCode typeCode = Type.GetTypeCode(dictionaryObject.GetType());
			switch (typeCode)
			{
			case 3:
				dictionaryObjectType = UserConfigurationDictionaryObjectType.Boolean;
				valueAsString = EwsUtilities.BoolToXSBool((bool)dictionaryObject);
				return;
			case 4:
			case 5:
			case 7:
			case 8:
				break;
			case 6:
				dictionaryObjectType = UserConfigurationDictionaryObjectType.Byte;
				valueAsString = ((byte)dictionaryObject).ToString();
				return;
			case 9:
				dictionaryObjectType = UserConfigurationDictionaryObjectType.Integer32;
				valueAsString = ((int)dictionaryObject).ToString();
				return;
			case 10:
				dictionaryObjectType = UserConfigurationDictionaryObjectType.UnsignedInteger32;
				valueAsString = ((uint)dictionaryObject).ToString();
				return;
			case 11:
				dictionaryObjectType = UserConfigurationDictionaryObjectType.Integer64;
				valueAsString = ((long)dictionaryObject).ToString();
				return;
			case 12:
				dictionaryObjectType = UserConfigurationDictionaryObjectType.UnsignedInteger64;
				valueAsString = ((ulong)dictionaryObject).ToString();
				return;
			default:
				switch (typeCode)
				{
				case 16:
					dictionaryObjectType = UserConfigurationDictionaryObjectType.DateTime;
					valueAsString = service.ConvertDateTimeToUniversalDateTimeString((DateTime)dictionaryObject);
					return;
				case 18:
					dictionaryObjectType = UserConfigurationDictionaryObjectType.String;
					valueAsString = (string)dictionaryObject;
					return;
				}
				break;
			}
			EwsUtilities.Assert(false, "UserConfigurationDictionary.WriteObjectValueToXml", "Unsupported type: " + dictionaryObject.GetType().ToString());
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00015AFC File Offset: 0x00014AFC
		private static UserConfigurationDictionaryObjectType GetObjectType(string type)
		{
			return (UserConfigurationDictionaryObjectType)Enum.Parse(typeof(UserConfigurationDictionaryObjectType), type, false);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00015B14 File Offset: 0x00014B14
		private JsonObject GetJsonObject(object dictionaryObject, ExchangeService service)
		{
			UserConfigurationDictionaryObjectType userConfigurationDictionaryObjectType = UserConfigurationDictionaryObjectType.String;
			if (dictionaryObject == null)
			{
				return null;
			}
			string[] array;
			if (dictionaryObject is string[])
			{
				userConfigurationDictionaryObjectType = UserConfigurationDictionaryObjectType.StringArray;
				array = dictionaryObject as string[];
			}
			else if (dictionaryObject is byte[])
			{
				userConfigurationDictionaryObjectType = UserConfigurationDictionaryObjectType.ByteArray;
				array = new string[] { Convert.ToBase64String(dictionaryObject as byte[]) };
			}
			else
			{
				array = new string[1];
				UserConfigurationDictionary.GetTypeCode(service, dictionaryObject, ref userConfigurationDictionaryObjectType, ref array[0]);
			}
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Type", userConfigurationDictionaryObjectType);
			jsonObject.Add("Value", array);
			return jsonObject;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00015B9C File Offset: 0x00014B9C
		private void WriteObjectToXml(EwsServiceXmlWriter writer, string xmlElementName, object dictionaryObject)
		{
			EwsUtilities.Assert(writer != null, "UserConfigurationDictionary.WriteObjectToXml", "writer is null");
			EwsUtilities.Assert(xmlElementName != null, "UserConfigurationDictionary.WriteObjectToXml", "xmlElementName is null");
			writer.WriteStartElement(XmlNamespace.Types, xmlElementName);
			if (dictionaryObject == null)
			{
				EwsUtilities.Assert(xmlElementName != "DictionaryKey", "UserConfigurationDictionary.WriteObjectToXml", "Key is null");
				writer.WriteAttributeValue("xsi", "nil", "true");
			}
			else
			{
				this.WriteObjectValueToXml(writer, dictionaryObject);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00015C20 File Offset: 0x00014C20
		private void WriteObjectValueToXml(EwsServiceXmlWriter writer, object dictionaryObject)
		{
			EwsUtilities.Assert(writer != null, "UserConfigurationDictionary.WriteObjectValueToXml", "writer is null");
			EwsUtilities.Assert(dictionaryObject != null, "UserConfigurationDictionary.WriteObjectValueToXml", "dictionaryObject is null");
			string[] array = dictionaryObject as string[];
			if (array != null)
			{
				this.WriteEntryTypeToXml(writer, UserConfigurationDictionaryObjectType.StringArray);
				foreach (string text in array)
				{
					this.WriteEntryValueToXml(writer, text);
				}
				return;
			}
			UserConfigurationDictionaryObjectType userConfigurationDictionaryObjectType = UserConfigurationDictionaryObjectType.String;
			string text2 = null;
			byte[] array3 = dictionaryObject as byte[];
			if (array3 != null)
			{
				userConfigurationDictionaryObjectType = UserConfigurationDictionaryObjectType.ByteArray;
				text2 = Convert.ToBase64String(array3);
			}
			else
			{
				UserConfigurationDictionary.GetTypeCode(writer.Service, dictionaryObject, ref userConfigurationDictionaryObjectType, ref text2);
			}
			this.WriteEntryTypeToXml(writer, userConfigurationDictionaryObjectType);
			this.WriteEntryValueToXml(writer, text2);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00015CCD File Offset: 0x00014CCD
		private void WriteEntryTypeToXml(EwsServiceXmlWriter writer, UserConfigurationDictionaryObjectType dictionaryObjectType)
		{
			writer.WriteStartElement(XmlNamespace.Types, "Type");
			writer.WriteValue(dictionaryObjectType.ToString(), "Type");
			writer.WriteEndElement();
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00015CF7 File Offset: 0x00014CF7
		private void WriteEntryValueToXml(EwsServiceXmlWriter writer, string value)
		{
			writer.WriteStartElement(XmlNamespace.Types, "Value");
			if (value != null)
			{
				writer.WriteValue(value, "Value");
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00015D1A File Offset: 0x00014D1A
		internal override void LoadFromXml(EwsServiceXmlReader reader, XmlNamespace xmlNamespace, string xmlElementName)
		{
			base.LoadFromXml(reader, xmlNamespace, xmlElementName);
			this.isDirty = false;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00015D2C File Offset: 0x00014D2C
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			reader.EnsureCurrentNodeIsStartElement(base.Namespace, "DictionaryEntry");
			this.LoadEntry(reader);
			return true;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00015D48 File Offset: 0x00014D48
		private void LoadEntry(EwsServiceXmlReader reader)
		{
			EwsUtilities.Assert(reader != null, "UserConfigurationDictionary.LoadEntry", "reader is null");
			object obj = null;
			reader.ReadStartElement(base.Namespace, "DictionaryKey");
			object dictionaryObject = this.GetDictionaryObject(reader);
			reader.ReadStartElement(base.Namespace, "DictionaryValue");
			string text = reader.ReadAttributeValue(XmlNamespace.XmlSchemaInstance, "nil");
			bool flag = text == null || !Convert.ToBoolean(text);
			if (flag)
			{
				obj = this.GetDictionaryObject(reader);
			}
			this.dictionary.Add(dictionaryObject, obj);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00015DCC File Offset: 0x00014DCC
		public void CreateFromJsonCollection(object[] jsonCollection, ExchangeService service)
		{
			foreach (object obj in jsonCollection)
			{
				JsonObject jsonObject = obj as JsonObject;
				object dictionaryObject = this.GetDictionaryObject(jsonObject.ReadAsJsonObject("DictionaryKey"), service);
				object dictionaryObject2 = this.GetDictionaryObject(jsonObject.ReadAsJsonObject("DictionaryValue"), service);
				this.dictionary.Add(dictionaryObject, dictionaryObject2);
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00015E2F File Offset: 0x00014E2F
		public void UpdateFromJsonCollection(object[] jsonCollection, ExchangeService service)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00015E38 File Offset: 0x00014E38
		private object GetDictionaryObject(JsonObject jsonObject, ExchangeService service)
		{
			if (jsonObject == null)
			{
				return null;
			}
			UserConfigurationDictionaryObjectType objectType = UserConfigurationDictionary.GetObjectType(jsonObject.ReadAsString("Type"));
			List<string> objectValue = this.GetObjectValue(jsonObject.ReadAsArray("Value"));
			return this.ConstructObject(objectType, objectValue, service);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00015E78 File Offset: 0x00014E78
		private List<string> GetObjectValue(object[] valueArray)
		{
			List<string> list = new List<string>();
			foreach (object obj in valueArray)
			{
				list.Add(obj as string);
			}
			return list;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00015EAC File Offset: 0x00014EAC
		private object GetDictionaryObject(EwsServiceXmlReader reader)
		{
			EwsUtilities.Assert(reader != null, "UserConfigurationDictionary.LoadFromXml", "reader is null");
			UserConfigurationDictionaryObjectType objectType = this.GetObjectType(reader);
			List<string> objectValue = this.GetObjectValue(reader, objectType);
			return this.ConstructObject(objectType, objectValue, reader.Service);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00015EF0 File Offset: 0x00014EF0
		private List<string> GetObjectValue(EwsServiceXmlReader reader, UserConfigurationDictionaryObjectType type)
		{
			EwsUtilities.Assert(reader != null, "UserConfigurationDictionary.LoadFromXml", "reader is null");
			List<string> list = new List<string>();
			reader.ReadStartElement(base.Namespace, "Value");
			do
			{
				string text = null;
				if (reader.IsEmptyElement)
				{
					if (type == UserConfigurationDictionaryObjectType.String || type == UserConfigurationDictionaryObjectType.StringArray)
					{
						text = string.Empty;
					}
					else
					{
						EwsUtilities.Assert(false, "UserConfigurationDictionary.GetObjectValue", "Empty element passed for type: " + type.ToString());
					}
				}
				else
				{
					text = reader.ReadElementValue();
				}
				list.Add(text);
				reader.Read();
			}
			while (reader.IsStartElement(base.Namespace, "Value"));
			return list;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00015F90 File Offset: 0x00014F90
		private UserConfigurationDictionaryObjectType GetObjectType(EwsServiceXmlReader reader)
		{
			EwsUtilities.Assert(reader != null, "UserConfigurationDictionary.LoadFromXml", "reader is null");
			reader.ReadStartElement(base.Namespace, "Type");
			string text = reader.ReadElementValue();
			return UserConfigurationDictionary.GetObjectType(text);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00015FD4 File Offset: 0x00014FD4
		private object ConstructObject(UserConfigurationDictionaryObjectType type, List<string> value, ExchangeService service)
		{
			EwsUtilities.Assert(value != null, "UserConfigurationDictionary.ConstructObject", "value is null");
			EwsUtilities.Assert(value.Count == 1 || type == UserConfigurationDictionaryObjectType.StringArray, "UserConfigurationDictionary.ConstructObject", "value is array but type is not StringArray");
			object obj = null;
			switch (type)
			{
			case UserConfigurationDictionaryObjectType.DateTime:
			{
				DateTime? dateTime = service.ConvertUniversalDateTimeStringToLocalDateTime(value[0]);
				if (dateTime != null)
				{
					obj = dateTime.Value;
				}
				else
				{
					EwsUtilities.Assert(false, "UserConfigurationDictionary.ConstructObject", "DateTime is null");
				}
				break;
			}
			case UserConfigurationDictionaryObjectType.Boolean:
				obj = bool.Parse(value[0]);
				break;
			case UserConfigurationDictionaryObjectType.Byte:
				obj = byte.Parse(value[0]);
				break;
			case UserConfigurationDictionaryObjectType.String:
				obj = value[0];
				break;
			case UserConfigurationDictionaryObjectType.Integer32:
				obj = int.Parse(value[0]);
				break;
			case UserConfigurationDictionaryObjectType.UnsignedInteger32:
				obj = uint.Parse(value[0]);
				break;
			case UserConfigurationDictionaryObjectType.Integer64:
				obj = long.Parse(value[0]);
				break;
			case UserConfigurationDictionaryObjectType.UnsignedInteger64:
				obj = ulong.Parse(value[0]);
				break;
			case UserConfigurationDictionaryObjectType.StringArray:
				obj = value.ToArray();
				break;
			case UserConfigurationDictionaryObjectType.ByteArray:
				obj = Convert.FromBase64String(value[0]);
				break;
			default:
				EwsUtilities.Assert(false, "UserConfigurationDictionary.ConstructObject", "Type not recognized: " + type.ToString());
				break;
			}
			return obj;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001614F File Offset: 0x0001514F
		private void ValidateEntry(object key, object value)
		{
			this.ValidateObject(key);
			this.ValidateObject(value);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00016160 File Offset: 0x00015160
		private void ValidateObject(object dictionaryObject)
		{
			if (dictionaryObject != null)
			{
				Array array = dictionaryObject as Array;
				if (array != null)
				{
					this.ValidateArrayObject(array);
					return;
				}
				this.ValidateObjectType(dictionaryObject.GetType());
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00016190 File Offset: 0x00015190
		private void ValidateArrayObject(Array dictionaryObjectAsArray)
		{
			if (dictionaryObjectAsArray is string[])
			{
				if (dictionaryObjectAsArray.Length > 0)
				{
					using (IEnumerator enumerator = dictionaryObjectAsArray.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current == null)
							{
								throw new ServiceLocalException(Strings.NullStringArrayElementInvalid);
							}
						}
						return;
					}
				}
				throw new ServiceLocalException(Strings.ZeroLengthArrayInvalid);
			}
			if (!(dictionaryObjectAsArray is byte[]))
			{
				throw new ServiceLocalException(string.Format(Strings.ObjectTypeNotSupported, dictionaryObjectAsArray.GetType()));
			}
			if (dictionaryObjectAsArray.Length <= 0)
			{
				throw new ServiceLocalException(Strings.ZeroLengthArrayInvalid);
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00016248 File Offset: 0x00015248
		private void ValidateObjectType(Type type)
		{
			bool flag = false;
			TypeCode typeCode = Type.GetTypeCode(type);
			switch (typeCode)
			{
			case 3:
			case 6:
			case 9:
			case 10:
			case 11:
			case 12:
				break;
			case 4:
			case 5:
			case 7:
			case 8:
				goto IL_0052;
			default:
				switch (typeCode)
				{
				case 16:
				case 18:
					break;
				case 17:
					goto IL_0052;
				default:
					goto IL_0052;
				}
				break;
			}
			flag = true;
			IL_0052:
			if (!flag)
			{
				throw new ServiceLocalException(string.Format(Strings.ObjectTypeNotSupported, type));
			}
		}

		// Token: 0x04000213 RID: 531
		private Dictionary<object, object> dictionary;

		// Token: 0x04000214 RID: 532
		private bool isDirty;
	}
}
