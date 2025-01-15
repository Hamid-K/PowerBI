using System;
using System.Collections.Generic;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000220 RID: 544
	internal sealed class ReorderingJsonReader : BufferingJsonReader
	{
		// Token: 0x06001617 RID: 5655 RVA: 0x00043FFC File Offset: 0x000421FC
		internal ReorderingJsonReader(IJsonReader innerReader, int maxInnerErrorDepth)
			: base(innerReader, "error", maxInnerErrorDepth)
		{
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0004400C File Offset: 0x0004220C
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

		// Token: 0x06001619 RID: 5657 RVA: 0x0004413C File Offset: 0x0004233C
		private void ReadPropertyName(out string propertyName, out string annotationName)
		{
			string propertyName2 = this.GetPropertyName();
			base.ReadInternal();
			if (propertyName2.StartsWith("@", 4))
			{
				propertyName = null;
				annotationName = propertyName2.Substring(1);
				return;
			}
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

		// Token: 0x0600161A RID: 5658 RVA: 0x000441C4 File Offset: 0x000423C4
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

		// Token: 0x02000364 RID: 868
		private sealed class BufferedObject
		{
			// Token: 0x06001B33 RID: 6963 RVA: 0x0004CCE8 File Offset: 0x0004AEE8
			internal BufferedObject()
			{
				this.propertyNamesWithAnnotations = new List<KeyValuePair<string, string>>();
				this.dataProperties = new HashSet<string>(StringComparer.Ordinal);
				this.propertyCache = new Dictionary<string, object>(StringComparer.Ordinal);
			}

			// Token: 0x170005EC RID: 1516
			// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0004CD1B File Offset: 0x0004AF1B
			// (set) Token: 0x06001B35 RID: 6965 RVA: 0x0004CD23 File Offset: 0x0004AF23
			internal BufferingJsonReader.BufferedNode ObjectStart { get; set; }

			// Token: 0x170005ED RID: 1517
			// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0004CD2C File Offset: 0x0004AF2C
			// (set) Token: 0x06001B37 RID: 6967 RVA: 0x0004CD34 File Offset: 0x0004AF34
			internal ReorderingJsonReader.BufferedProperty CurrentProperty { get; private set; }

			// Token: 0x06001B38 RID: 6968 RVA: 0x0004CD40 File Offset: 0x0004AF40
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
				if (this.propertyCache.TryGetValue(text, ref obj))
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

			// Token: 0x06001B39 RID: 6969 RVA: 0x0004CDF8 File Offset: 0x0004AFF8
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

			// Token: 0x06001B3A RID: 6970 RVA: 0x0004CECC File Offset: 0x0004B0CC
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

			// Token: 0x06001B3B RID: 6971 RVA: 0x00042984 File Offset: 0x00040B84
			private static bool IsODataInstanceAnnotation(string annotationName)
			{
				return annotationName.StartsWith("odata.", 4);
			}

			// Token: 0x06001B3C RID: 6972 RVA: 0x0004CEDC File Offset: 0x0004B0DC
			private static bool IsODataContextAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.context", annotationName) == 0;
			}

			// Token: 0x06001B3D RID: 6973 RVA: 0x0004CEEC File Offset: 0x0004B0EC
			private static bool IsODataTypeAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.type", annotationName) == 0;
			}

			// Token: 0x06001B3E RID: 6974 RVA: 0x0004CEFC File Offset: 0x0004B0FC
			private static bool IsODataIdAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.id", annotationName) == 0;
			}

			// Token: 0x06001B3F RID: 6975 RVA: 0x0004CF0C File Offset: 0x0004B10C
			private static bool IsODataETagAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.etag", annotationName) == 0;
			}

			// Token: 0x06001B40 RID: 6976 RVA: 0x0004CF1C File Offset: 0x0004B11C
			private IEnumerable<string> SortPropertyNames()
			{
				string text = null;
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

			// Token: 0x04000DB9 RID: 3513
			private readonly Dictionary<string, object> propertyCache;

			// Token: 0x04000DBA RID: 3514
			private readonly HashSet<string> dataProperties;

			// Token: 0x04000DBB RID: 3515
			private readonly List<KeyValuePair<string, string>> propertyNamesWithAnnotations;
		}

		// Token: 0x02000365 RID: 869
		private sealed class BufferedProperty
		{
			// Token: 0x170005EE RID: 1518
			// (get) Token: 0x06001B41 RID: 6977 RVA: 0x0004CF2C File Offset: 0x0004B12C
			// (set) Token: 0x06001B42 RID: 6978 RVA: 0x0004CF34 File Offset: 0x0004B134
			internal string PropertyAnnotationName { get; set; }

			// Token: 0x170005EF RID: 1519
			// (get) Token: 0x06001B43 RID: 6979 RVA: 0x0004CF3D File Offset: 0x0004B13D
			// (set) Token: 0x06001B44 RID: 6980 RVA: 0x0004CF45 File Offset: 0x0004B145
			internal BufferingJsonReader.BufferedNode PropertyNameNode { get; set; }

			// Token: 0x170005F0 RID: 1520
			// (get) Token: 0x06001B45 RID: 6981 RVA: 0x0004CF4E File Offset: 0x0004B14E
			// (set) Token: 0x06001B46 RID: 6982 RVA: 0x0004CF56 File Offset: 0x0004B156
			internal BufferingJsonReader.BufferedNode EndOfPropertyValueNode { get; set; }

			// Token: 0x06001B47 RID: 6983 RVA: 0x0004CF60 File Offset: 0x0004B160
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
