using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000173 RID: 371
	internal sealed class ReorderingJsonReader : BufferingJsonReader
	{
		// Token: 0x06000A3E RID: 2622 RVA: 0x00021FB0 File Offset: 0x000201B0
		internal ReorderingJsonReader(TextReader reader, int maxInnerErrorDepth)
			: base(reader, "odata.error", maxInnerErrorDepth, ODataFormat.Json)
		{
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00021FC4 File Offset: 0x000201C4
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

		// Token: 0x06000A40 RID: 2624 RVA: 0x000220F8 File Offset: 0x000202F8
		private void ReadPropertyName(out string propertyName, out string annotationName)
		{
			string propertyName2 = this.GetPropertyName();
			base.ReadInternal();
			int num = propertyName2.IndexOf('@');
			if (num >= 0)
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
			propertyName = null;
			annotationName = propertyName2;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00022150 File Offset: 0x00020350
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

		// Token: 0x02000174 RID: 372
		private sealed class BufferedObject
		{
			// Token: 0x06000A42 RID: 2626 RVA: 0x0002219D File Offset: 0x0002039D
			internal BufferedObject()
			{
				this.propertyNamesWithAnnotations = new List<KeyValuePair<string, string>>();
				this.dataProperties = new HashSet<string>(StringComparer.Ordinal);
				this.propertyCache = new Dictionary<string, object>(StringComparer.Ordinal);
			}

			// Token: 0x1700027C RID: 636
			// (get) Token: 0x06000A43 RID: 2627 RVA: 0x000221D0 File Offset: 0x000203D0
			// (set) Token: 0x06000A44 RID: 2628 RVA: 0x000221D8 File Offset: 0x000203D8
			internal BufferingJsonReader.BufferedNode ObjectStart { get; set; }

			// Token: 0x1700027D RID: 637
			// (get) Token: 0x06000A45 RID: 2629 RVA: 0x000221E1 File Offset: 0x000203E1
			// (set) Token: 0x06000A46 RID: 2630 RVA: 0x000221E9 File Offset: 0x000203E9
			internal ReorderingJsonReader.BufferedProperty CurrentProperty { get; private set; }

			// Token: 0x06000A47 RID: 2631 RVA: 0x000221F4 File Offset: 0x000203F4
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

			// Token: 0x06000A48 RID: 2632 RVA: 0x000222AC File Offset: 0x000204AC
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

			// Token: 0x06000A49 RID: 2633 RVA: 0x00022550 File Offset: 0x00020750
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

			// Token: 0x06000A4A RID: 2634 RVA: 0x0002256D File Offset: 0x0002076D
			private static bool IsODataInstanceAnnotation(string annotationName)
			{
				return annotationName.StartsWith("odata.", 4);
			}

			// Token: 0x06000A4B RID: 2635 RVA: 0x0002257B File Offset: 0x0002077B
			private static bool IsODataMetadataAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.metadata", annotationName) == 0;
			}

			// Token: 0x06000A4C RID: 2636 RVA: 0x0002258B File Offset: 0x0002078B
			private static bool IsODataAnnotationGroupReferenceAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.annotationGroupReference", annotationName) == 0;
			}

			// Token: 0x06000A4D RID: 2637 RVA: 0x0002259B File Offset: 0x0002079B
			private static bool IsODataAnnotationGroupAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.annotationGroup", annotationName) == 0;
			}

			// Token: 0x06000A4E RID: 2638 RVA: 0x000225AB File Offset: 0x000207AB
			private static bool IsODataTypeAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.type", annotationName) == 0;
			}

			// Token: 0x06000A4F RID: 2639 RVA: 0x000225BB File Offset: 0x000207BB
			private static bool IsODataIdAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.id", annotationName) == 0;
			}

			// Token: 0x06000A50 RID: 2640 RVA: 0x000225CB File Offset: 0x000207CB
			private static bool IsODataETagAnnotation(string annotationName)
			{
				return string.CompareOrdinal("odata.etag", annotationName) == 0;
			}

			// Token: 0x06000A51 RID: 2641 RVA: 0x00022AC4 File Offset: 0x00020CC4
			private IEnumerable<string> SortPropertyNames()
			{
				string metadataAnnotationName = null;
				string typeAnnotationName = null;
				string idAnnotationName = null;
				string etagAnnotationName = null;
				string annotationGroupDeclarationName = null;
				string annotationGroupReferenceName = null;
				List<string> odataAnnotationNames = null;
				List<string> otherNames = null;
				foreach (KeyValuePair<string, string> keyValuePair in this.propertyNamesWithAnnotations)
				{
					string key = keyValuePair.Key;
					if (keyValuePair.Value == null || !this.dataProperties.Contains(key))
					{
						this.dataProperties.Add(key);
						if (ReorderingJsonReader.BufferedObject.IsODataMetadataAnnotation(key))
						{
							metadataAnnotationName = key;
						}
						else if (ReorderingJsonReader.BufferedObject.IsODataAnnotationGroupAnnotation(key))
						{
							annotationGroupDeclarationName = key;
						}
						else if (ReorderingJsonReader.BufferedObject.IsODataAnnotationGroupReferenceAnnotation(key))
						{
							annotationGroupReferenceName = key;
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
				if (metadataAnnotationName != null)
				{
					yield return metadataAnnotationName;
				}
				if (annotationGroupDeclarationName != null)
				{
					yield return annotationGroupDeclarationName;
				}
				if (annotationGroupReferenceName != null)
				{
					yield return annotationGroupReferenceName;
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

			// Token: 0x040003EB RID: 1003
			private readonly Dictionary<string, object> propertyCache;

			// Token: 0x040003EC RID: 1004
			private readonly HashSet<string> dataProperties;

			// Token: 0x040003ED RID: 1005
			private readonly List<KeyValuePair<string, string>> propertyNamesWithAnnotations;
		}

		// Token: 0x02000175 RID: 373
		private sealed class BufferedProperty
		{
			// Token: 0x1700027E RID: 638
			// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00022AE1 File Offset: 0x00020CE1
			// (set) Token: 0x06000A53 RID: 2643 RVA: 0x00022AE9 File Offset: 0x00020CE9
			internal string PropertyAnnotationName { get; set; }

			// Token: 0x1700027F RID: 639
			// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00022AF2 File Offset: 0x00020CF2
			// (set) Token: 0x06000A55 RID: 2645 RVA: 0x00022AFA File Offset: 0x00020CFA
			internal BufferingJsonReader.BufferedNode PropertyNameNode { get; set; }

			// Token: 0x17000280 RID: 640
			// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00022B03 File Offset: 0x00020D03
			// (set) Token: 0x06000A57 RID: 2647 RVA: 0x00022B0B File Offset: 0x00020D0B
			internal BufferingJsonReader.BufferedNode EndOfPropertyValueNode { get; set; }

			// Token: 0x06000A58 RID: 2648 RVA: 0x00022B14 File Offset: 0x00020D14
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
