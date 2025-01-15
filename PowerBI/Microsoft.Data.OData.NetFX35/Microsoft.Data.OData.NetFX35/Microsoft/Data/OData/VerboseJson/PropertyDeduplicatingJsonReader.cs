using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000179 RID: 377
	internal sealed class PropertyDeduplicatingJsonReader : BufferingJsonReader
	{
		// Token: 0x06000A68 RID: 2664 RVA: 0x00022ED7 File Offset: 0x000210D7
		internal PropertyDeduplicatingJsonReader(TextReader reader, int maxInnerErrorDepth)
			: base(reader, "error", maxInnerErrorDepth, ODataFormat.VerboseJson)
		{
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00022EEC File Offset: 0x000210EC
		protected override void ProcessObjectValue()
		{
			Stack<PropertyDeduplicatingJsonReader.ObjectRecordPropertyDeduplicationRecord> stack = new Stack<PropertyDeduplicatingJsonReader.ObjectRecordPropertyDeduplicationRecord>();
			for (;;)
			{
				if (this.currentBufferedNode.NodeType == JsonNodeType.StartObject)
				{
					stack.Push(new PropertyDeduplicatingJsonReader.ObjectRecordPropertyDeduplicationRecord());
					BufferingJsonReader.BufferedNode currentBufferedNode = this.currentBufferedNode;
					base.ProcessObjectValue();
					this.currentBufferedNode = currentBufferedNode;
				}
				else if (this.currentBufferedNode.NodeType == JsonNodeType.EndObject)
				{
					PropertyDeduplicatingJsonReader.ObjectRecordPropertyDeduplicationRecord objectRecordPropertyDeduplicationRecord = stack.Pop();
					if (objectRecordPropertyDeduplicationRecord.CurrentPropertyRecord != null)
					{
						objectRecordPropertyDeduplicationRecord.CurrentPropertyRecord.LastPropertyValueNode = this.currentBufferedNode.Previous;
					}
					foreach (List<PropertyDeduplicatingJsonReader.PropertyDeduplicationRecord> list in objectRecordPropertyDeduplicationRecord.Values)
					{
						if (list.Count > 1)
						{
							PropertyDeduplicatingJsonReader.PropertyDeduplicationRecord propertyDeduplicationRecord = list[0];
							for (int i = 1; i < list.Count; i++)
							{
								PropertyDeduplicatingJsonReader.PropertyDeduplicationRecord propertyDeduplicationRecord2 = list[i];
								propertyDeduplicationRecord2.PropertyNode.Previous.Next = propertyDeduplicationRecord2.LastPropertyValueNode.Next;
								propertyDeduplicationRecord2.LastPropertyValueNode.Next.Previous = propertyDeduplicationRecord2.PropertyNode.Previous;
								propertyDeduplicationRecord.PropertyNode.Previous.Next = propertyDeduplicationRecord2.PropertyNode;
								propertyDeduplicationRecord2.PropertyNode.Previous = propertyDeduplicationRecord.PropertyNode.Previous;
								propertyDeduplicationRecord.LastPropertyValueNode.Next.Previous = propertyDeduplicationRecord2.LastPropertyValueNode;
								propertyDeduplicationRecord2.LastPropertyValueNode.Next = propertyDeduplicationRecord.LastPropertyValueNode.Next;
								propertyDeduplicationRecord = propertyDeduplicationRecord2;
							}
						}
					}
					if (stack.Count == 0)
					{
						break;
					}
				}
				else if (this.currentBufferedNode.NodeType == JsonNodeType.Property)
				{
					PropertyDeduplicatingJsonReader.ObjectRecordPropertyDeduplicationRecord objectRecordPropertyDeduplicationRecord2 = stack.Peek();
					if (objectRecordPropertyDeduplicationRecord2.CurrentPropertyRecord != null)
					{
						objectRecordPropertyDeduplicationRecord2.CurrentPropertyRecord.LastPropertyValueNode = this.currentBufferedNode.Previous;
					}
					objectRecordPropertyDeduplicationRecord2.CurrentPropertyRecord = new PropertyDeduplicatingJsonReader.PropertyDeduplicationRecord(this.currentBufferedNode);
					string text = (string)this.currentBufferedNode.Value;
					List<PropertyDeduplicatingJsonReader.PropertyDeduplicationRecord> list2;
					if (!objectRecordPropertyDeduplicationRecord2.TryGetValue(text, ref list2))
					{
						list2 = new List<PropertyDeduplicatingJsonReader.PropertyDeduplicationRecord>();
						objectRecordPropertyDeduplicationRecord2.Add(text, list2);
					}
					list2.Add(objectRecordPropertyDeduplicationRecord2.CurrentPropertyRecord);
				}
				if (!base.ReadInternal())
				{
					return;
				}
			}
		}

		// Token: 0x0200017A RID: 378
		private sealed class ObjectRecordPropertyDeduplicationRecord : Dictionary<string, List<PropertyDeduplicatingJsonReader.PropertyDeduplicationRecord>>
		{
			// Token: 0x17000283 RID: 643
			// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00023124 File Offset: 0x00021324
			// (set) Token: 0x06000A6B RID: 2667 RVA: 0x0002312C File Offset: 0x0002132C
			internal PropertyDeduplicatingJsonReader.PropertyDeduplicationRecord CurrentPropertyRecord { get; set; }
		}

		// Token: 0x0200017B RID: 379
		private sealed class PropertyDeduplicationRecord
		{
			// Token: 0x06000A6D RID: 2669 RVA: 0x0002313D File Offset: 0x0002133D
			internal PropertyDeduplicationRecord(BufferingJsonReader.BufferedNode propertyNode)
			{
				this.propertyNode = propertyNode;
			}

			// Token: 0x17000284 RID: 644
			// (get) Token: 0x06000A6E RID: 2670 RVA: 0x0002314C File Offset: 0x0002134C
			internal BufferingJsonReader.BufferedNode PropertyNode
			{
				get
				{
					return this.propertyNode;
				}
			}

			// Token: 0x17000285 RID: 645
			// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00023154 File Offset: 0x00021354
			// (set) Token: 0x06000A70 RID: 2672 RVA: 0x0002315C File Offset: 0x0002135C
			internal BufferingJsonReader.BufferedNode LastPropertyValueNode
			{
				get
				{
					return this.lastPropertyValueNode;
				}
				set
				{
					this.lastPropertyValueNode = value;
				}
			}

			// Token: 0x040003F9 RID: 1017
			private readonly BufferingJsonReader.BufferedNode propertyNode;

			// Token: 0x040003FA RID: 1018
			private BufferingJsonReader.BufferedNode lastPropertyValueNode;
		}
	}
}
