using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.LayoutRenderers.Wrappers;
using NLog.MessageTemplates;

namespace NLog.Layouts
{
	// Token: 0x020000AE RID: 174
	[ThreadAgnostic]
	[ThreadSafe]
	public abstract class XmlElementBase : Layout
	{
		// Token: 0x06000B30 RID: 2864 RVA: 0x0001CF38 File Offset: 0x0001B138
		protected XmlElementBase(string elementName, Layout elementValue)
		{
			this.ElementNameInternal = elementName;
			this.ElementValueInternal = elementValue;
			this.Attributes = new List<XmlAttribute>();
			this.Elements = new List<XmlElement>();
			this.ExcludeProperties = new HashSet<string>();
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0001CFB8 File Offset: 0x0001B1B8
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x0001CFC0 File Offset: 0x0001B1C0
		internal string ElementNameInternal
		{
			get
			{
				return this._elementName;
			}
			set
			{
				this._elementName = XmlHelper.XmlConvertToElementName((value != null) ? value.Trim() : null, true);
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0001CFDA File Offset: 0x0001B1DA
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x0001CFE7 File Offset: 0x0001B1E7
		internal Layout ElementValueInternal
		{
			get
			{
				return this._elementValueWrapper.Inner;
			}
			set
			{
				this._elementValueWrapper.Inner = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0001CFF5 File Offset: 0x0001B1F5
		// (set) Token: 0x06000B36 RID: 2870 RVA: 0x0001D002 File Offset: 0x0001B202
		internal bool ElementEncodeInternal
		{
			get
			{
				return this._elementValueWrapper.XmlEncode;
			}
			set
			{
				this._elementValueWrapper.XmlEncode = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0001D010 File Offset: 0x0001B210
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x0001D018 File Offset: 0x0001B218
		[DefaultValue(false)]
		public bool IndentXml { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0001D021 File Offset: 0x0001B221
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x0001D029 File Offset: 0x0001B229
		[ArrayParameter(typeof(XmlElement), "element")]
		public IList<XmlElement> Elements { get; private set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0001D032 File Offset: 0x0001B232
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x0001D03A File Offset: 0x0001B23A
		[ArrayParameter(typeof(XmlAttribute), "attribute")]
		public IList<XmlAttribute> Attributes { get; private set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0001D043 File Offset: 0x0001B243
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x0001D04B File Offset: 0x0001B24B
		[DefaultValue(false)]
		public bool IncludeEmptyValue { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0001D054 File Offset: 0x0001B254
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x0001D05C File Offset: 0x0001B25C
		[DefaultValue(false)]
		public bool IncludeMdc { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0001D065 File Offset: 0x0001B265
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x0001D06D File Offset: 0x0001B26D
		[DefaultValue(false)]
		public bool IncludeMdlc { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0001D076 File Offset: 0x0001B276
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x0001D07E File Offset: 0x0001B27E
		[DefaultValue(false)]
		public bool IncludeAllProperties { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0001D087 File Offset: 0x0001B287
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x0001D08F File Offset: 0x0001B28F
		public ISet<string> ExcludeProperties { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0001D098 File Offset: 0x0001B298
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x0001D0A0 File Offset: 0x0001B2A0
		public string PropertiesElementName
		{
			get
			{
				return this._propertiesElementName;
			}
			set
			{
				this._propertiesElementName = value;
				this._propertiesElementNameHasFormat = value != null && value.IndexOf('{') >= 0;
				if (!this._propertiesElementNameHasFormat)
				{
					this._propertiesElementName = XmlHelper.XmlConvertToElementName((value != null) ? value.Trim() : null, true);
				}
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0001D0EE File Offset: 0x0001B2EE
		// (set) Token: 0x06000B4A RID: 2890 RVA: 0x0001D0F6 File Offset: 0x0001B2F6
		public string PropertiesElementKeyAttribute { get; set; } = "key";

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x0001D0FF File Offset: 0x0001B2FF
		// (set) Token: 0x06000B4C RID: 2892 RVA: 0x0001D107 File Offset: 0x0001B307
		public string PropertiesElementValueAttribute { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0001D110 File Offset: 0x0001B310
		// (set) Token: 0x06000B4E RID: 2894 RVA: 0x0001D118 File Offset: 0x0001B318
		public string PropertiesCollectionItemName { get; set; } = "item";

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0001D121 File Offset: 0x0001B321
		// (set) Token: 0x06000B50 RID: 2896 RVA: 0x0001D129 File Offset: 0x0001B329
		public int MaxRecursionLimit { get; set; } = 1;

		// Token: 0x06000B51 RID: 2897 RVA: 0x0001D134 File Offset: 0x0001B334
		protected override void InitializeLayout()
		{
			base.InitializeLayout();
			if (this.IncludeMdc)
			{
				base.ThreadAgnostic = false;
			}
			if (this.IncludeMdlc)
			{
				base.ThreadAgnostic = false;
			}
			if (this.IncludeAllProperties)
			{
				base.MutableUnsafe = true;
			}
			if (this.Attributes.Count > 1)
			{
				HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				foreach (XmlAttribute xmlAttribute in this.Attributes)
				{
					if (string.IsNullOrEmpty(xmlAttribute.Name))
					{
						InternalLogger.Warn("XmlElement(ElementName={0}): Contains attribute with missing name (Ignored)");
					}
					else if (hashSet.Contains(xmlAttribute.Name))
					{
						InternalLogger.Warn<string, string>("XmlElement(ElementName={0}): Contains duplicate attribute name: {1} (Invalid xml)", this.ElementNameInternal, xmlAttribute.Name);
					}
					else
					{
						hashSet.Add(xmlAttribute.Name);
					}
				}
			}
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0001D218 File Offset: 0x0001B418
		internal override void PrecalculateBuilder(LogEventInfo logEvent, StringBuilder target)
		{
			base.PrecalculateBuilderInternal(logEvent, target);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0001D224 File Offset: 0x0001B424
		protected override void RenderFormattedMessage(LogEventInfo logEvent, StringBuilder target)
		{
			int length = target.Length;
			this.RenderXmlFormattedMessage(logEvent, target);
			if (target.Length == length && this.IncludeEmptyValue && !string.IsNullOrEmpty(this.ElementNameInternal))
			{
				XmlElementBase.RenderSelfClosingElement(target, this.ElementNameInternal);
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0001D26A File Offset: 0x0001B46A
		protected override string GetFormattedMessage(LogEventInfo logEvent)
		{
			return base.RenderAllocateBuilder(logEvent, null);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0001D274 File Offset: 0x0001B474
		private void RenderXmlFormattedMessage(LogEventInfo logEvent, StringBuilder sb)
		{
			int length = sb.Length;
			if (!string.IsNullOrEmpty(this.ElementNameInternal))
			{
				for (int i = 0; i < this.Attributes.Count; i++)
				{
					XmlAttribute xmlAttribute = this.Attributes[i];
					int length2 = sb.Length;
					if (!this.RenderAppendXmlAttributeValue(xmlAttribute, logEvent, sb, sb.Length == length))
					{
						sb.Length = length2;
					}
				}
				if (sb.Length != length)
				{
					if (this.ElementValueInternal == null && this.Elements.Count <= 0 && !this.IncludeMdc && !this.IncludeMdlc && (!this.IncludeAllProperties || !logEvent.HasProperties))
					{
						sb.Append("/>");
						return;
					}
					sb.Append('>');
				}
				if (this.ElementValueInternal != null)
				{
					int length3 = sb.Length;
					if (sb.Length == length)
					{
						XmlElementBase.RenderStartElement(sb, this.ElementNameInternal);
					}
					int length4 = sb.Length;
					this._elementValueWrapper.RenderAppendBuilder(logEvent, sb);
					if (length4 == sb.Length && !this.IncludeEmptyValue)
					{
						sb.Length = length3;
					}
				}
				if (this.IndentXml && sb.Length != length)
				{
					sb.AppendLine();
				}
			}
			for (int j = 0; j < this.Elements.Count; j++)
			{
				XmlElement xmlElement = this.Elements[j];
				int length5 = sb.Length;
				if (!this.RenderAppendXmlElementValue(xmlElement, logEvent, sb, sb.Length == length))
				{
					sb.Length = length5;
				}
			}
			this.AppendLogEventXmlProperties(logEvent, sb, length);
			if (sb.Length > length && !string.IsNullOrEmpty(this.ElementNameInternal))
			{
				this.EndXmlDocument(sb, this.ElementNameInternal);
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0001D41C File Offset: 0x0001B61C
		private void AppendLogEventXmlProperties(LogEventInfo logEventInfo, StringBuilder sb, int orgLength)
		{
			if (this.IncludeMdc)
			{
				foreach (string text in MappedDiagnosticsContext.GetNames())
				{
					if (!string.IsNullOrEmpty(text))
					{
						object @object = MappedDiagnosticsContext.GetObject(text);
						this.AppendXmlPropertyValue(text, @object, sb, orgLength, false, false);
					}
				}
			}
			if (this.IncludeMdlc)
			{
				foreach (string text2 in MappedDiagnosticsLogicalContext.GetNames())
				{
					if (!string.IsNullOrEmpty(text2))
					{
						object object2 = MappedDiagnosticsLogicalContext.GetObject(text2);
						this.AppendXmlPropertyValue(text2, object2, sb, orgLength, false, false);
					}
				}
			}
			if (this.IncludeAllProperties && logEventInfo.HasProperties)
			{
				this.AppendLogEventProperties(logEventInfo, sb, orgLength);
			}
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0001D4F8 File Offset: 0x0001B6F8
		private void AppendLogEventProperties(LogEventInfo logEventInfo, StringBuilder sb, int orgLength)
		{
			foreach (MessageTemplateParameter messageTemplateParameter in ((IEnumerable<MessageTemplateParameter>)logEventInfo.CreateOrUpdatePropertiesInternal(true, null)))
			{
				if (!string.IsNullOrEmpty(messageTemplateParameter.Name) && !this.ExcludeProperties.Contains(messageTemplateParameter.Name))
				{
					object obj = messageTemplateParameter.Value;
					IFormattable formattable;
					if (!string.IsNullOrEmpty(messageTemplateParameter.Format) && (formattable = obj as IFormattable) != null)
					{
						IFormattable formattable2 = formattable;
						string format = messageTemplateParameter.Format;
						IFormatProvider formatProvider;
						if ((formatProvider = logEventInfo.FormatProvider) == null)
						{
							LoggingConfiguration loggingConfiguration = base.LoggingConfiguration;
							formatProvider = ((loggingConfiguration != null) ? loggingConfiguration.DefaultCultureInfo : null);
						}
						obj = formattable2.ToString(format, formatProvider);
					}
					else if (messageTemplateParameter.CaptureType == CaptureType.Stringify)
					{
						object obj2 = messageTemplateParameter.Value ?? string.Empty;
						IFormatProvider formatProvider2;
						if ((formatProvider2 = logEventInfo.FormatProvider) == null)
						{
							LoggingConfiguration loggingConfiguration2 = base.LoggingConfiguration;
							formatProvider2 = ((loggingConfiguration2 != null) ? loggingConfiguration2.DefaultCultureInfo : null);
						}
						obj = Convert.ToString(obj2, formatProvider2);
					}
					this.AppendXmlPropertyObjectValue(messageTemplateParameter.Name, obj, sb, orgLength, default(SingleItemOptimizedHashSet<object>), 0, false);
				}
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0001D614 File Offset: 0x0001B814
		private bool AppendXmlPropertyObjectValue(string propName, object propertyValue, StringBuilder sb, int orgLength, SingleItemOptimizedHashSet<object> objectsInPath, int depth, bool ignorePropertiesElementName = false)
		{
			IConvertible convertible = propertyValue as IConvertible;
			TypeCode typeCode = ((propertyValue == null) ? TypeCode.Empty : ((convertible != null) ? convertible.GetTypeCode() : TypeCode.Object));
			if (typeCode != TypeCode.Object)
			{
				string text = XmlHelper.XmlConvertToString(convertible, typeCode, true);
				this.AppendXmlPropertyValue(propName, text, sb, orgLength, false, ignorePropertiesElementName);
			}
			else
			{
				if (sb.Length > 524288)
				{
					return false;
				}
				int num = ((objectsInPath.Count == 0) ? depth : (depth + 1));
				if (num > this.MaxRecursionLimit)
				{
					return false;
				}
				if (objectsInPath.Contains(propertyValue))
				{
					return false;
				}
				IDictionary dictionary;
				if ((dictionary = propertyValue as IDictionary) != null)
				{
					using (XmlElementBase.StartCollectionScope(ref objectsInPath, dictionary))
					{
						this.AppendXmlDictionaryObject(propName, dictionary, sb, orgLength, objectsInPath, num, ignorePropertiesElementName);
						return true;
					}
				}
				IEnumerable enumerable;
				if ((enumerable = propertyValue as IEnumerable) != null)
				{
					ObjectReflectionCache.ObjectPropertyList objectPropertyList;
					if (this._objectReflectionCache.TryLookupExpandoObject(propertyValue, out objectPropertyList))
					{
						using (new SingleItemOptimizedHashSet<object>.SingleItemScopedInsert(propertyValue, ref objectsInPath, false, XmlElementBase._referenceEqualsComparer))
						{
							this.AppendXmlObjectPropertyValues(propName, ref objectPropertyList, sb, orgLength, ref objectsInPath, num, ignorePropertiesElementName);
							return true;
						}
					}
					using (XmlElementBase.StartCollectionScope(ref objectsInPath, enumerable))
					{
						this.AppendXmlCollectionObject(propName, enumerable, sb, orgLength, objectsInPath, num, ignorePropertiesElementName);
						return true;
					}
				}
				using (new SingleItemOptimizedHashSet<object>.SingleItemScopedInsert(propertyValue, ref objectsInPath, false, XmlElementBase._referenceEqualsComparer))
				{
					ObjectReflectionCache.ObjectPropertyList objectPropertyList2 = this._objectReflectionCache.LookupObjectProperties(propertyValue);
					this.AppendXmlObjectPropertyValues(propName, ref objectPropertyList2, sb, orgLength, ref objectsInPath, num, ignorePropertiesElementName);
				}
			}
			return true;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0001D7BC File Offset: 0x0001B9BC
		private static SingleItemOptimizedHashSet<object>.SingleItemScopedInsert StartCollectionScope(ref SingleItemOptimizedHashSet<object> objectsInPath, object value)
		{
			return new SingleItemOptimizedHashSet<object>.SingleItemScopedInsert(value, ref objectsInPath, true, XmlElementBase._referenceEqualsComparer);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0001D7CC File Offset: 0x0001B9CC
		private void AppendXmlCollectionObject(string propName, IEnumerable collection, StringBuilder sb, int orgLength, SingleItemOptimizedHashSet<object> objectsInPath, int depth, bool ignorePropertiesElementName)
		{
			string text = this.AppendXmlPropertyValue(propName, string.Empty, sb, orgLength, true, false);
			if (!string.IsNullOrEmpty(text))
			{
				foreach (object obj in collection)
				{
					int length = sb.Length;
					if (length > 524288)
					{
						break;
					}
					if (!this.AppendXmlPropertyObjectValue(this.PropertiesCollectionItemName, obj, sb, orgLength, objectsInPath, depth, true))
					{
						sb.Length = length;
					}
				}
				this.AppendClosingPropertyTag(text, sb, ignorePropertiesElementName);
			}
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0001D86C File Offset: 0x0001BA6C
		private void AppendXmlDictionaryObject(string propName, IDictionary dictionary, StringBuilder sb, int orgLength, SingleItemOptimizedHashSet<object> objectsInPath, int depth, bool ignorePropertiesElementName)
		{
			string text = this.AppendXmlPropertyValue(propName, string.Empty, sb, orgLength, true, ignorePropertiesElementName);
			if (!string.IsNullOrEmpty(text))
			{
				foreach (DictionaryEntry dictionaryEntry in new DictionaryEntryEnumerable(dictionary))
				{
					int length = sb.Length;
					if (length > 524288)
					{
						break;
					}
					object key = dictionaryEntry.Key;
					if (!this.AppendXmlPropertyObjectValue((key != null) ? key.ToString() : null, dictionaryEntry.Value, sb, orgLength, objectsInPath, depth, false))
					{
						sb.Length = length;
					}
				}
				this.AppendClosingPropertyTag(text, sb, ignorePropertiesElementName);
			}
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0001D928 File Offset: 0x0001BB28
		private void AppendXmlObjectPropertyValues(string propName, ref ObjectReflectionCache.ObjectPropertyList propertyValues, StringBuilder sb, int orgLength, ref SingleItemOptimizedHashSet<object> objectsInPath, int depth, bool ignorePropertiesElementName = false)
		{
			if (propertyValues.ConvertToString)
			{
				this.AppendXmlPropertyValue(propName, propertyValues.ToString(), sb, orgLength, false, ignorePropertiesElementName);
				return;
			}
			string text = this.AppendXmlPropertyValue(propName, string.Empty, sb, orgLength, true, ignorePropertiesElementName);
			if (!string.IsNullOrEmpty(text))
			{
				foreach (ObjectReflectionCache.ObjectPropertyList.PropertyValue propertyValue in propertyValues)
				{
					int length = sb.Length;
					if (length > 524288)
					{
						break;
					}
					TypeCode typeCode = propertyValue.TypeCode;
					if (typeCode != TypeCode.Object)
					{
						string text2 = XmlHelper.XmlConvertToString((IConvertible)propertyValue.Value, typeCode, true);
						this.AppendXmlPropertyStringValue(propertyValue.Name, text2, sb, orgLength, false, ignorePropertiesElementName);
					}
					else if (!this.AppendXmlPropertyObjectValue(propertyValue.Name, propertyValue.Value, sb, orgLength, objectsInPath, depth, false))
					{
						sb.Length = length;
					}
				}
				this.AppendClosingPropertyTag(text, sb, ignorePropertiesElementName);
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0001DA30 File Offset: 0x0001BC30
		private string AppendXmlPropertyValue(string propName, object propertyValue, StringBuilder sb, int orgLength, bool ignoreValue = false, bool ignorePropertiesElementName = false)
		{
			string text = (ignoreValue ? string.Empty : XmlHelper.XmlConvertToStringSafe(propertyValue));
			return this.AppendXmlPropertyStringValue(propName, text, sb, orgLength, ignoreValue, ignorePropertiesElementName);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0001DA60 File Offset: 0x0001BC60
		private string AppendXmlPropertyStringValue(string propName, string xmlValueString, StringBuilder sb, int orgLength, bool ignoreValue = false, bool ignorePropertiesElementName = false)
		{
			if (string.IsNullOrEmpty(this.PropertiesElementName))
			{
				return string.Empty;
			}
			propName = ((propName != null) ? propName.Trim() : null);
			if (string.IsNullOrEmpty(propName))
			{
				return string.Empty;
			}
			if (sb.Length == orgLength && !string.IsNullOrEmpty(this.ElementNameInternal))
			{
				this.BeginXmlDocument(sb, this.ElementNameInternal);
			}
			if (this.IndentXml && !string.IsNullOrEmpty(this.ElementNameInternal))
			{
				sb.Append("  ");
			}
			sb.Append('<');
			string text;
			if (ignorePropertiesElementName)
			{
				text = XmlHelper.XmlConvertToElementName(propName, true);
				sb.Append(text);
			}
			else
			{
				if (this._propertiesElementNameHasFormat)
				{
					text = XmlHelper.XmlConvertToElementName(propName, true);
					sb.AppendFormat(this.PropertiesElementName, text);
				}
				else
				{
					text = this.PropertiesElementName;
					sb.Append(this.PropertiesElementName);
				}
				XmlElementBase.RenderAttribute(sb, this.PropertiesElementKeyAttribute, propName);
			}
			if (!ignoreValue)
			{
				if (XmlElementBase.RenderAttribute(sb, this.PropertiesElementValueAttribute, xmlValueString))
				{
					sb.Append("/>");
					if (this.IndentXml)
					{
						sb.AppendLine();
					}
				}
				else
				{
					sb.Append('>');
					XmlHelper.EscapeXmlString(xmlValueString, false, sb);
					this.AppendClosingPropertyTag(text, sb, ignorePropertiesElementName);
				}
			}
			else
			{
				sb.Append('>');
				if (this.IndentXml)
				{
					sb.AppendLine();
				}
			}
			return text;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0001DBAC File Offset: 0x0001BDAC
		private void AppendClosingPropertyTag(string propNameElement, StringBuilder sb, bool ignorePropertiesElementName = false)
		{
			sb.Append("</");
			if (ignorePropertiesElementName)
			{
				sb.Append(propNameElement);
			}
			else
			{
				sb.AppendFormat(this.PropertiesElementName, propNameElement);
			}
			sb.Append('>');
			if (this.IndentXml)
			{
				sb.AppendLine();
			}
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0001DBF8 File Offset: 0x0001BDF8
		private static bool RenderAttribute(StringBuilder sb, string attributeName, string value)
		{
			if (!string.IsNullOrEmpty(attributeName))
			{
				sb.Append(' ');
				sb.Append(attributeName);
				sb.Append("=\"");
				XmlHelper.EscapeXmlString(value, true, sb);
				sb.Append('"');
				return true;
			}
			return false;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0001DC34 File Offset: 0x0001BE34
		private bool RenderAppendXmlElementValue(XmlElementBase xmlElement, LogEventInfo logEvent, StringBuilder sb, bool beginXmlDocument)
		{
			if (string.IsNullOrEmpty(xmlElement.ElementNameInternal))
			{
				return false;
			}
			if (beginXmlDocument && !string.IsNullOrEmpty(this.ElementNameInternal))
			{
				this.BeginXmlDocument(sb, this.ElementNameInternal);
			}
			if (this.IndentXml && !string.IsNullOrEmpty(this.ElementNameInternal))
			{
				sb.Append("  ");
			}
			int length = sb.Length;
			xmlElement.RenderAppendBuilder(logEvent, sb, false);
			if (sb.Length == length && !xmlElement.IncludeEmptyValue)
			{
				return false;
			}
			if (this.IndentXml)
			{
				sb.AppendLine();
			}
			return true;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0001DCC4 File Offset: 0x0001BEC4
		private bool RenderAppendXmlAttributeValue(XmlAttribute xmlAttribute, LogEventInfo logEvent, StringBuilder sb, bool beginXmlDocument)
		{
			string name = xmlAttribute.Name;
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			if (beginXmlDocument)
			{
				sb.Append('<');
				sb.Append(this.ElementNameInternal);
			}
			sb.Append(' ');
			sb.Append(name);
			sb.Append("=\"");
			int length = sb.Length;
			xmlAttribute.LayoutWrapper.RenderAppendBuilder(logEvent, sb);
			if (sb.Length == length && !xmlAttribute.IncludeEmptyValue)
			{
				return false;
			}
			sb.Append('"');
			return true;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0001DD4A File Offset: 0x0001BF4A
		private void BeginXmlDocument(StringBuilder sb, string elementName)
		{
			XmlElementBase.RenderStartElement(sb, elementName);
			if (this.IndentXml)
			{
				sb.AppendLine();
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0001DD62 File Offset: 0x0001BF62
		private void EndXmlDocument(StringBuilder sb, string elementName)
		{
			XmlElementBase.RenderEndElement(sb, elementName);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0001DD6C File Offset: 0x0001BF6C
		public override string ToString()
		{
			if (this.Elements.Count > 0)
			{
				return base.ToStringWithNestedItems<XmlElement>(this.Elements, (XmlElement l) => l.ToString());
			}
			if (this.Attributes.Count > 0)
			{
				return base.ToStringWithNestedItems<XmlAttribute>(this.Attributes, (XmlAttribute a) => "Attributes:" + a.Name);
			}
			if (this.ElementNameInternal != null)
			{
				return base.ToStringWithNestedItems<XmlElementBase>(new XmlElementBase[] { this }, (XmlElementBase n) => "Element:" + n.ElementNameInternal);
			}
			return base.GetType().Name;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0001DE30 File Offset: 0x0001C030
		private static void RenderSelfClosingElement(StringBuilder target, string elementName)
		{
			target.Append('<');
			target.Append(elementName);
			target.Append("/>");
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0001DE4F File Offset: 0x0001C04F
		private static void RenderStartElement(StringBuilder sb, string elementName)
		{
			sb.Append('<');
			sb.Append(elementName);
			sb.Append('>');
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0001DE6B File Offset: 0x0001C06B
		private static void RenderEndElement(StringBuilder sb, string elementName)
		{
			sb.Append("</");
			sb.Append(elementName);
			sb.Append('>');
		}

		// Token: 0x040002A1 RID: 673
		private const string DefaultPropertyName = "property";

		// Token: 0x040002A2 RID: 674
		private const string DefaultPropertyKeyAttribute = "key";

		// Token: 0x040002A3 RID: 675
		private const string DefaultCollectionItemName = "item";

		// Token: 0x040002A4 RID: 676
		private string _elementName;

		// Token: 0x040002A5 RID: 677
		private readonly XmlEncodeLayoutRendererWrapper _elementValueWrapper = new XmlEncodeLayoutRendererWrapper();

		// Token: 0x040002AE RID: 686
		private string _propertiesElementName = "property";

		// Token: 0x040002AF RID: 687
		private bool _propertiesElementNameHasFormat;

		// Token: 0x040002B4 RID: 692
		private readonly ObjectReflectionCache _objectReflectionCache = new ObjectReflectionCache();

		// Token: 0x040002B5 RID: 693
		private static readonly IEqualityComparer<object> _referenceEqualsComparer = SingleItemOptimizedHashSet<object>.ReferenceEqualityComparer.Default;

		// Token: 0x040002B6 RID: 694
		private const int MaxXmlLength = 524288;
	}
}
