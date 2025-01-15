using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000259 RID: 601
	internal sealed class ReorderingJsonReader : BufferingJsonReader
	{
		// Token: 0x06001B15 RID: 6933 RVA: 0x00053400 File Offset: 0x00051600
		internal ReorderingJsonReader(IJsonReader innerReader, int maxInnerErrorDepth)
			: base(innerReader, "error", maxInnerErrorDepth)
		{
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00053410 File Offset: 0x00051610
		public override Stream CreateReadStream()
		{
			Stream stream;
			try
			{
				stream = new MemoryStream((base.Value == null) ? new byte[0] : Convert.FromBase64String((string)base.Value));
			}
			catch (FormatException)
			{
				throw new ODataException(Strings.JsonReader_InvalidBinaryFormat(base.Value));
			}
			base.Read();
			return stream;
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00053470 File Offset: 0x00051670
		public override TextReader CreateTextReader()
		{
			if (base.NodeType == JsonNodeType.Property)
			{
				throw new ODataException("Reading JSON Streams not supported for beta.");
			}
			TextReader textReader = new StringReader((base.Value == null) ? "" : ((string)base.Value));
			base.Read();
			return textReader;
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x000534B9 File Offset: 0x000516B9
		public override bool CanStream()
		{
			return base.Value is string || base.Value == null;
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x000534D4 File Offset: 0x000516D4
		protected override void ProcessObjectValue()
		{
			Stack<ReorderingJsonReader.BufferedObject> stack = new Stack<ReorderingJsonReader.BufferedObject>();
			for (;;)
			{
				switch (this.currentBufferedNode.NodeType)
				{
				case JsonNodeType.StartObject:
				{
					ReorderingJsonReader.BufferedObject bufferedObject = new ReorderingJsonReader.BufferedObject
					{
						ObjectStart = this.currentBufferedNode
					};
					stack.Push(bufferedObject);
					base.ProcessObjectValue();
					this.currentBufferedNode = bufferedObject.ObjectStart;
					base.ReadInternal();
					continue;
				}
				case JsonNodeType.EndObject:
				{
					ReorderingJsonReader.BufferedObject bufferedObject2 = stack.Pop();
					if (bufferedObject2.CurrentProperty != null)
					{
						bufferedObject2.CurrentProperty.EndOfPropertyValueNode = this.currentBufferedNode.Previous;
					}
					bufferedObject2.Reorder();
					if (stack.Count == 0)
					{
						return;
					}
					base.ReadInternal();
					continue;
				}
				case JsonNodeType.Property:
				{
					ReorderingJsonReader.BufferedObject bufferedObject3 = stack.Peek();
					if (bufferedObject3.CurrentProperty != null)
					{
						bufferedObject3.CurrentProperty.EndOfPropertyValueNode = this.currentBufferedNode.Previous;
					}
					ReorderingJsonReader.BufferedProperty bufferedProperty = new ReorderingJsonReader.BufferedProperty();
					bufferedProperty.PropertyNameNode = this.currentBufferedNode;
					string text;
					string text2;
					this.ReadPropertyName(out text, out text2);
					bufferedProperty.PropertyAnnotationName = text2;
					bufferedObject3.AddBufferedProperty(text, text2, bufferedProperty);
					if (text2 != null)
					{
						this.BufferValue();
						continue;
					}
					continue;
				}
				}
				base.ReadInternal();
			}
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00053604 File Offset: 0x00051804
		private void ReadPropertyName(out string propertyName, out string annotationName)
		{
			string propertyName2 = this.GetPropertyName();
			base.ReadInternal();
			if (propertyName2.StartsWith("@", StringComparison.Ordinal))
			{
				propertyName = null;
				annotationName = propertyName2.Substring(1);
				if (annotationName.IndexOf('.') == -1)
				{
					annotationName = "odata." + annotationName;
					return;
				}
				return;
			}
			else
			{
				int num = propertyName2.IndexOf('@');
				if (num > 0)
				{
					propertyName = propertyName2.Substring(0, num);
					annotationName = propertyName2.Substring(num + 1);
					return;
				}
				int num2 = propertyName2.IndexOf('.');
				if (num2 < 0)
				{
					propertyName = propertyName2;
					annotationName = null;
					return;
				}
				if (ODataJsonLightUtils.IsMetadataReferenceProperty(propertyName2))
				{
					propertyName = null;
					annotationName = propertyName2;
					return;
				}
				throw new ODataException(Strings.JsonReaderExtensions_UnexpectedInstanceAnnotationName(propertyName2));
			}
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x000536A8 File Offset: 0x000518A8
		private void BufferValue()
		{
			int num = 0;
			do
			{
				switch (base.NodeType)
				{
				case JsonNodeType.StartObject:
				case JsonNodeType.StartArray:
					num++;
					break;
				case JsonNodeType.EndObject:
				case JsonNodeType.EndArray:
					num--;
					break;
				}
				base.ReadInternal();
			}
			while (num > 0);
		}

		// Token: 0x0200044A RID: 1098
		private sealed class BufferedObject
		{
			// Token: 0x060021D7 RID: 8663 RVA: 0x0005E3C8 File Offset: 0x0005C5C8
			internal BufferedObject()
			{
				this.propertyNamesWithAnnotations = new List<KeyValuePair<string, string>>();
				this.dataProperties = new HashSet<string>(StringComparer.Ordinal);
				this.propertyCache = new Dictionary<string, object>(StringComparer.Ordinal);
			}

			// Token: 0x17000672 RID: 1650
			// (get) Token: 0x060021D8 RID: 8664 RVA: 0x0005E3FB File Offset: 0x0005C5FB
			// (set) Token: 0x060021D9 RID: 8665 RVA: 0x0005E403 File Offset: 0x0005C603
			internal BufferingJsonReader.BufferedNode ObjectStart { get; set; }

			// Token: 0x17000673 RID: 1651
			// (get) Token: 0x060021DA RID: 8666 RVA: 0x0005E40C File Offset: 0x0005C60C
			// (set) Token: 0x060021DB RID: 8667 RVA: 0x0005E414 File Offset: 0x0005C614
			internal ReorderingJsonReader.BufferedProperty CurrentProperty { get; private set; }

			// Token: 0x060021DC RID: 8668 RVA: 0x0005E420 File Offset: 0x0005C620
			internal void AddBufferedProperty(string propertyName, string annotationName, ReorderingJsonReader.BufferedProperty bufferedProperty)
			{
				this.CurrentProperty = bufferedProperty;
				string text = propertyName ?? annotationName;
				if (propertyName == null)
				{
					this.propertyNamesWithAnnotations.Add(new KeyValuePair<string, string>(annotationName, null));
				}
				else if (!this.dataProperties.Contains(propertyName))
				{
					if (annotationName == null)
					{
						this.dataProperties.Add(propertyName);
					}
					this.propertyNamesWithAnnotations.Add(new KeyValuePair<string, string>(propertyName, annotationName));
				}
				object obj;
				if (this.propertyCache.TryGetValue(text, out obj))
				{
					ReorderingJsonReader.BufferedProperty bufferedProperty2 = obj as ReorderingJsonReader.BufferedProperty;
					List<ReorderingJsonReader.BufferedProperty> list;
					if (bufferedProperty2 != null)
					{
						list = new List<ReorderingJsonReader.BufferedProperty>(4);
						list.Add(bufferedProperty2);
						this.propertyCache[text] = list;
					}
					else
					{
						list = (List<ReorderingJsonReader.BufferedProperty>)obj;
					}
					list.Add(bufferedProperty);
					return;
				}
				this.propertyCache.Add(text, bufferedProperty);
			}

			// Token: 0x060021DD RID: 8669 RVA: 0x0005E4D8 File Offset: 0x0005C6D8
			internal void Reorder()
			{
				BufferingJsonReader.BufferedNode bufferedNode = this.ObjectStart;
				IEnumerable<string> enumerable = this.SortPropertyNames();
				foreach (string text in enumerable)
				{
					object obj = this.propertyCache[text];
					ReorderingJsonReader.BufferedProperty bufferedProperty = obj as ReorderingJsonReader.BufferedProperty;
					if (bufferedProperty != null)
					{
						bufferedProperty.InsertAfter(bufferedNode);
						bufferedNode = bufferedProperty.EndOfPropertyValueNode;
					}
					else
					{
						IEnumerable<ReorderingJsonReader.BufferedProperty> enumerable2 = ReorderingJsonReader.BufferedObject.SortBufferedProperties((IList<ReorderingJsonReader.BufferedProperty>)obj);
						foreach (ReorderingJsonReader.BufferedProperty bufferedProperty2 in enumerable2)
						{
							bufferedProperty2.InsertAfter(bufferedNode);
							bufferedNode = bufferedProperty2.EndOfPropertyValueNode;
						}
					}
				}
			}

			// Token: 0x060021DE RID: 8670 RVA: 0x0005E5AC File Offset: 0x0005C7AC
			private static IEnumerable<ReorderingJsonReader.BufferedProperty> SortBufferedProperties(IList<ReorderingJsonReader.BufferedProperty> bufferedProperties)
			{
				List<ReorderingJsonReader.BufferedProperty> delayedProperties = null;
				int num;
				for (int i = 0; i < bufferedProperties.Count; i = num)
				{
					ReorderingJsonReader.BufferedProperty bufferedProperty = bufferedProperties[i];
					string propertyAnnotationName = bufferedProperty.PropertyAnnotationName;
					if (propertyAnnotationName == null || !ReorderingJsonReader.BufferedObject.IsODataInstanceAnnotation(propertyAnnotationName))
					{
						if (delayedProperties == null)
						{
							delayedProperties = new List<ReorderingJsonReader.BufferedProperty>();
						}
						delayedProperties.Add(bufferedProperty);
					}
					else
					{
						yield return bufferedProperty;
					}
					num = i + 1;
				}
				if (delayedProperties != null)
				{
					for (int j = 0; j < delayedProperties.Count; j = num)
					{
						yield return delayedProperties[j];
						num = j + 1;
					}
				}
				yield break;
			}

			// Token: 0x060021DF RID: 8671 RVA: 0x00050E68 File Offset: 0x0004F068
			private static bool IsODataInstanceAnnotation(string annotationName)
			{
				return annotationName.StartsWith("odata.", StringComparison.Ordinal);
			}

			// Token: 0x060021E0 RID: 8672 RVA: 0x0005E5BC File Offset: 0x0005C7BC
			private static bool IsODataContextAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.context", annotationName) == 0;
			}

			// Token: 0x060021E1 RID: 8673 RVA: 0x0005E5CC File Offset: 0x0005C7CC
			private static bool IsODataRemovedAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.removed", annotationName) == 0;
			}

			// Token: 0x060021E2 RID: 8674 RVA: 0x0005E5DC File Offset: 0x0005C7DC
			private static bool IsODataTypeAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.type", annotationName) == 0;
			}

			// Token: 0x060021E3 RID: 8675 RVA: 0x0005E5EC File Offset: 0x0005C7EC
			private static bool IsODataIdAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.id", annotationName) == 0;
			}

			// Token: 0x060021E4 RID: 8676 RVA: 0x0005E5FC File Offset: 0x0005C7FC
			private static bool IsODataETagAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.etag", annotationName) == 0;
			}

			// Token: 0x060021E5 RID: 8677 RVA: 0x0005E60C File Offset: 0x0005C80C
			private IEnumerable<string> SortPropertyNames()
			{
				string text = null;
				string removedAnnotationName = null;
				string typeAnnotationName = null;
				string idAnnotationName = null;
				string etagAnnotationName = null;
				List<string> odataAnnotationNames = null;
				List<string> otherNames = null;
				foreach (KeyValuePair<string, string> keyValuePair in this.propertyNamesWithAnnotations)
				{
					string key = keyValuePair.Key;
					if (keyValuePair.Value == null || !this.dataProperties.Contains(key))
					{
						this.dataProperties.Add(key);
						if (ReorderingJsonReader.BufferedObject.IsODataContextAnnotation(key))
						{
							text = key;
						}
						else if (ReorderingJsonReader.BufferedObject.IsODataRemovedAnnotation(key))
						{
							removedAnnotationName = key;
						}
						else if (ReorderingJsonReader.BufferedObject.IsODataTypeAnnotation(key))
						{
							typeAnnotationName = key;
						}
						else if (ReorderingJsonReader.BufferedObject.IsODataIdAnnotation(key))
						{
							idAnnotationName = key;
						}
						else if (ReorderingJsonReader.BufferedObject.IsODataETagAnnotation(key))
						{
							etagAnnotationName = key;
						}
						else if (ReorderingJsonReader.BufferedObject.IsODataInstanceAnnotation(key))
						{
							if (odataAnnotationNames == null)
							{
								odataAnnotationNames = new List<string>();
							}
							odataAnnotationNames.Add(key);
						}
						else
						{
							if (otherNames == null)
							{
								otherNames = new List<string>();
							}
							otherNames.Add(key);
						}
					}
				}
				if (text != null)
				{
					yield return text;
				}
				if (removedAnnotationName != null)
				{
					yield return removedAnnotationName;
				}
				if (typeAnnotationName != null)
				{
					yield return typeAnnotationName;
				}
				if (idAnnotationName != null)
				{
					yield return idAnnotationName;
				}
				if (etagAnnotationName != null)
				{
					yield return etagAnnotationName;
				}
				if (odataAnnotationNames != null)
				{
					foreach (string text2 in odataAnnotationNames)
					{
						yield return text2;
					}
					List<string>.Enumerator enumerator2 = default(List<string>.Enumerator);
				}
				if (otherNames != null)
				{
					foreach (string text3 in otherNames)
					{
						yield return text3;
					}
					List<string>.Enumerator enumerator2 = default(List<string>.Enumerator);
				}
				yield break;
				yield break;
			}

			// Token: 0x04001074 RID: 4212
			private readonly Dictionary<string, object> propertyCache;

			// Token: 0x04001075 RID: 4213
			private readonly HashSet<string> dataProperties;

			// Token: 0x04001076 RID: 4214
			private readonly List<KeyValuePair<string, string>> propertyNamesWithAnnotations;
		}

		// Token: 0x0200044B RID: 1099
		private sealed class BufferedProperty
		{
			// Token: 0x17000674 RID: 1652
			// (get) Token: 0x060021E6 RID: 8678 RVA: 0x0005E61C File Offset: 0x0005C81C
			// (set) Token: 0x060021E7 RID: 8679 RVA: 0x0005E624 File Offset: 0x0005C824
			internal string PropertyAnnotationName { get; set; }

			// Token: 0x17000675 RID: 1653
			// (get) Token: 0x060021E8 RID: 8680 RVA: 0x0005E62D File Offset: 0x0005C82D
			// (set) Token: 0x060021E9 RID: 8681 RVA: 0x0005E635 File Offset: 0x0005C835
			internal BufferingJsonReader.BufferedNode PropertyNameNode { get; set; }

			// Token: 0x17000676 RID: 1654
			// (get) Token: 0x060021EA RID: 8682 RVA: 0x0005E63E File Offset: 0x0005C83E
			// (set) Token: 0x060021EB RID: 8683 RVA: 0x0005E646 File Offset: 0x0005C846
			internal BufferingJsonReader.BufferedNode EndOfPropertyValueNode { get; set; }

			// Token: 0x060021EC RID: 8684 RVA: 0x0005E650 File Offset: 0x0005C850
			internal void InsertAfter(BufferingJsonReader.BufferedNode node)
			{
				BufferingJsonReader.BufferedNode previous = this.PropertyNameNode.Previous;
				BufferingJsonReader.BufferedNode bufferedNode = this.EndOfPropertyValueNode.Next;
				previous.Next = bufferedNode;
				bufferedNode.Previous = previous;
				bufferedNode = node.Next;
				node.Next = this.PropertyNameNode;
				this.PropertyNameNode.Previous = node;
				this.EndOfPropertyValueNode.Next = bufferedNode;
				if (bufferedNode != null)
				{
					bufferedNode.Previous = this.EndOfPropertyValueNode;
				}
			}
		}
	}
}
