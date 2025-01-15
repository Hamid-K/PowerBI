using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.OData.Core.Json;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x0200010A RID: 266
	internal sealed class ReorderingJsonReader : BufferingJsonReader
	{
		// Token: 0x06000A0E RID: 2574 RVA: 0x0002520D File Offset: 0x0002340D
		internal ReorderingJsonReader(TextReader reader, int maxInnerErrorDepth, bool isIeee754Comaptible)
			: base(reader, "error", maxInnerErrorDepth, ODataFormat.Json, isIeee754Comaptible)
		{
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00025224 File Offset: 0x00023424
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

		// Token: 0x06000A10 RID: 2576 RVA: 0x00025358 File Offset: 0x00023558
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

		// Token: 0x06000A11 RID: 2577 RVA: 0x000253E0 File Offset: 0x000235E0
		private void BufferValue()
		{
			int num = 0;
			do
			{
				switch (this.NodeType)
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

		// Token: 0x0200010B RID: 267
		private sealed class BufferedObject
		{
			// Token: 0x06000A12 RID: 2578 RVA: 0x0002542D File Offset: 0x0002362D
			internal BufferedObject()
			{
				this.propertyNamesWithAnnotations = new List<KeyValuePair<string, string>>();
				this.dataProperties = new HashSet<string>(StringComparer.Ordinal);
				this.propertyCache = new Dictionary<string, object>(StringComparer.Ordinal);
			}

			// Token: 0x1700022D RID: 557
			// (get) Token: 0x06000A13 RID: 2579 RVA: 0x00025460 File Offset: 0x00023660
			// (set) Token: 0x06000A14 RID: 2580 RVA: 0x00025468 File Offset: 0x00023668
			internal BufferingJsonReader.BufferedNode ObjectStart { get; set; }

			// Token: 0x1700022E RID: 558
			// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00025471 File Offset: 0x00023671
			// (set) Token: 0x06000A16 RID: 2582 RVA: 0x00025479 File Offset: 0x00023679
			internal ReorderingJsonReader.BufferedProperty CurrentProperty { get; private set; }

			// Token: 0x06000A17 RID: 2583 RVA: 0x00025484 File Offset: 0x00023684
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

			// Token: 0x06000A18 RID: 2584 RVA: 0x0002553C File Offset: 0x0002373C
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

			// Token: 0x06000A19 RID: 2585 RVA: 0x000257E0 File Offset: 0x000239E0
			private static IEnumerable<ReorderingJsonReader.BufferedProperty> SortBufferedProperties(IList<ReorderingJsonReader.BufferedProperty> bufferedProperties)
			{
				List<ReorderingJsonReader.BufferedProperty> delayedProperties = null;
				for (int i = 0; i < bufferedProperties.Count; i++)
				{
					ReorderingJsonReader.BufferedProperty bufferedProperty = bufferedProperties[i];
					string annotationName = bufferedProperty.PropertyAnnotationName;
					if (annotationName == null || !ReorderingJsonReader.BufferedObject.IsODataInstanceAnnotation(annotationName))
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
				}
				if (delayedProperties != null)
				{
					for (int j = 0; j < delayedProperties.Count; j++)
					{
						yield return delayedProperties[j];
					}
				}
				yield break;
			}

			// Token: 0x06000A1A RID: 2586 RVA: 0x000257FD File Offset: 0x000239FD
			private static bool IsODataInstanceAnnotation(string annotationName)
			{
				return annotationName.StartsWith("odata.", 4);
			}

			// Token: 0x06000A1B RID: 2587 RVA: 0x0002580B File Offset: 0x00023A0B
			private static bool IsODataContextAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.context", annotationName) == 0;
			}

			// Token: 0x06000A1C RID: 2588 RVA: 0x0002581B File Offset: 0x00023A1B
			private static bool IsODataTypeAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.type", annotationName) == 0;
			}

			// Token: 0x06000A1D RID: 2589 RVA: 0x0002582B File Offset: 0x00023A2B
			private static bool IsODataIdAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.id", annotationName) == 0;
			}

			// Token: 0x06000A1E RID: 2590 RVA: 0x0002583B File Offset: 0x00023A3B
			private static bool IsODataETagAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.etag", annotationName) == 0;
			}

			// Token: 0x06000A1F RID: 2591 RVA: 0x00025C9C File Offset: 0x00023E9C
			private IEnumerable<string> SortPropertyNames()
			{
				string contextAnnotationName = null;
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
							contextAnnotationName = key;
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
				if (contextAnnotationName != null)
				{
					yield return contextAnnotationName;
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
					foreach (string propertyName in odataAnnotationNames)
					{
						yield return propertyName;
					}
				}
				if (otherNames != null)
				{
					foreach (string propertyName2 in otherNames)
					{
						yield return propertyName2;
					}
				}
				yield break;
			}

			// Token: 0x04000405 RID: 1029
			private readonly Dictionary<string, object> propertyCache;

			// Token: 0x04000406 RID: 1030
			private readonly HashSet<string> dataProperties;

			// Token: 0x04000407 RID: 1031
			private readonly List<KeyValuePair<string, string>> propertyNamesWithAnnotations;
		}

		// Token: 0x0200010C RID: 268
		private sealed class BufferedProperty
		{
			// Token: 0x1700022F RID: 559
			// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00025CB9 File Offset: 0x00023EB9
			// (set) Token: 0x06000A21 RID: 2593 RVA: 0x00025CC1 File Offset: 0x00023EC1
			internal string PropertyAnnotationName { get; set; }

			// Token: 0x17000230 RID: 560
			// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00025CCA File Offset: 0x00023ECA
			// (set) Token: 0x06000A23 RID: 2595 RVA: 0x00025CD2 File Offset: 0x00023ED2
			internal BufferingJsonReader.BufferedNode PropertyNameNode { get; set; }

			// Token: 0x17000231 RID: 561
			// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00025CDB File Offset: 0x00023EDB
			// (set) Token: 0x06000A25 RID: 2597 RVA: 0x00025CE3 File Offset: 0x00023EE3
			internal BufferingJsonReader.BufferedNode EndOfPropertyValueNode { get; set; }

			// Token: 0x06000A26 RID: 2598 RVA: 0x00025CEC File Offset: 0x00023EEC
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
