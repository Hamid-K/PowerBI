using System;
using System.IO;
using Microsoft.Data.OData.VerboseJson;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x02000171 RID: 369
	internal class BufferingJsonReader : JsonReader
	{
		// Token: 0x06000A20 RID: 2592 RVA: 0x0002176C File Offset: 0x0001F96C
		internal BufferingJsonReader(TextReader reader, string inStreamErrorPropertyName, int maxInnerErrorDepth, ODataFormat jsonFormat)
			: base(reader, jsonFormat)
		{
			this.inStreamErrorPropertyName = inStreamErrorPropertyName;
			this.maxInnerErrorDepth = maxInnerErrorDepth;
			this.bufferedNodesHead = null;
			this.currentBufferedNode = null;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00021793 File Offset: 0x0001F993
		public override JsonNodeType NodeType
		{
			get
			{
				if (this.bufferedNodesHead == null)
				{
					return base.NodeType;
				}
				if (this.isBuffering)
				{
					return this.currentBufferedNode.NodeType;
				}
				return this.bufferedNodesHead.NodeType;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x000217C3 File Offset: 0x0001F9C3
		public override object Value
		{
			get
			{
				if (this.bufferedNodesHead == null)
				{
					return base.Value;
				}
				if (this.isBuffering)
				{
					return this.currentBufferedNode.Value;
				}
				return this.bufferedNodesHead.Value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x000217F3 File Offset: 0x0001F9F3
		public override string RawValue
		{
			get
			{
				if (this.bufferedNodesHead == null)
				{
					return base.RawValue;
				}
				if (this.isBuffering)
				{
					return this.currentBufferedNode.RawValue;
				}
				return this.bufferedNodesHead.RawValue;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00021823 File Offset: 0x0001FA23
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x0002182B File Offset: 0x0001FA2B
		internal bool DisableInStreamErrorDetection
		{
			get
			{
				return this.disableInStreamErrorDetection;
			}
			set
			{
				this.disableInStreamErrorDetection = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00021834 File Offset: 0x0001FA34
		internal bool IsBuffering
		{
			get
			{
				return this.isBuffering;
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002183C File Offset: 0x0001FA3C
		public override bool Read()
		{
			return this.ReadInternal();
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00021844 File Offset: 0x0001FA44
		internal void StartBuffering()
		{
			if (this.bufferedNodesHead == null)
			{
				this.bufferedNodesHead = new BufferingJsonReader.BufferedNode(base.NodeType, base.Value, base.RawValue);
			}
			else
			{
				this.removeOnNextRead = false;
			}
			if (this.currentBufferedNode == null)
			{
				this.currentBufferedNode = this.bufferedNodesHead;
			}
			this.isBuffering = true;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002189A File Offset: 0x0001FA9A
		internal object BookmarkCurrentPosition()
		{
			return this.currentBufferedNode;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x000218A4 File Offset: 0x0001FAA4
		internal void MoveToBookmark(object bookmark)
		{
			BufferingJsonReader.BufferedNode bufferedNode = bookmark as BufferingJsonReader.BufferedNode;
			this.currentBufferedNode = bufferedNode;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x000218BF File Offset: 0x0001FABF
		internal void StopBuffering()
		{
			this.isBuffering = false;
			this.removeOnNextRead = true;
			this.currentBufferedNode = null;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000218D8 File Offset: 0x0001FAD8
		internal bool StartBufferingAndTryToReadInStreamErrorPropertyValue(out ODataError error)
		{
			error = null;
			this.StartBuffering();
			this.parsingInStreamError = true;
			bool flag;
			try
			{
				flag = this.TryReadInStreamErrorPropertyValue(out error);
			}
			finally
			{
				this.StopBuffering();
				this.parsingInStreamError = false;
			}
			return flag;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00021920 File Offset: 0x0001FB20
		protected bool ReadInternal()
		{
			if (this.removeOnNextRead)
			{
				this.RemoveFirstNodeInBuffer();
				this.removeOnNextRead = false;
			}
			bool flag;
			if (this.isBuffering)
			{
				if (this.currentBufferedNode.Next != this.bufferedNodesHead)
				{
					this.currentBufferedNode = this.currentBufferedNode.Next;
					flag = true;
				}
				else if (this.parsingInStreamError)
				{
					flag = base.Read();
					BufferingJsonReader.BufferedNode bufferedNode = new BufferingJsonReader.BufferedNode(base.NodeType, base.Value, base.RawValue);
					bufferedNode.Previous = this.bufferedNodesHead.Previous;
					bufferedNode.Next = this.bufferedNodesHead;
					this.bufferedNodesHead.Previous.Next = bufferedNode;
					this.bufferedNodesHead.Previous = bufferedNode;
					this.currentBufferedNode = bufferedNode;
				}
				else
				{
					flag = this.ReadNextAndCheckForInStreamError();
				}
			}
			else if (this.bufferedNodesHead == null)
			{
				flag = (this.parsingInStreamError ? base.Read() : this.ReadNextAndCheckForInStreamError());
			}
			else
			{
				flag = this.bufferedNodesHead.NodeType != JsonNodeType.EndOfInput;
				this.removeOnNextRead = true;
			}
			return flag;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00021A28 File Offset: 0x0001FC28
		protected virtual void ProcessObjectValue()
		{
			ODataError odataError = null;
			if (!this.DisableInStreamErrorDetection)
			{
				this.ReadInternal();
				bool flag = false;
				while (this.currentBufferedNode.NodeType == JsonNodeType.Property)
				{
					string text = (string)this.currentBufferedNode.Value;
					if (string.CompareOrdinal(this.inStreamErrorPropertyName, text) != 0 || flag)
					{
						return;
					}
					flag = true;
					this.ReadInternal();
					if (!this.TryReadInStreamErrorPropertyValue(out odataError))
					{
						return;
					}
				}
				if (flag)
				{
					throw new ODataErrorException(odataError);
				}
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00021A9C File Offset: 0x0001FC9C
		private bool ReadNextAndCheckForInStreamError()
		{
			this.parsingInStreamError = true;
			bool flag3;
			try
			{
				bool flag = this.ReadInternal();
				if (base.NodeType == JsonNodeType.StartObject)
				{
					bool flag2 = this.isBuffering;
					BufferingJsonReader.BufferedNode bufferedNode = null;
					if (this.isBuffering)
					{
						bufferedNode = this.currentBufferedNode;
					}
					else
					{
						this.StartBuffering();
					}
					this.ProcessObjectValue();
					if (flag2)
					{
						this.currentBufferedNode = bufferedNode;
					}
					else
					{
						this.StopBuffering();
					}
				}
				flag3 = flag;
			}
			finally
			{
				this.parsingInStreamError = false;
			}
			return flag3;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00021B18 File Offset: 0x0001FD18
		private bool TryReadInStreamErrorPropertyValue(out ODataError error)
		{
			error = null;
			if (this.currentBufferedNode.NodeType != JsonNodeType.StartObject)
			{
				return false;
			}
			this.ReadInternal();
			error = new ODataError();
			ODataVerboseJsonReaderUtils.ErrorPropertyBitMask errorPropertyBitMask = ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.None;
			while (this.currentBufferedNode.NodeType == JsonNodeType.Property)
			{
				string text = (string)this.currentBufferedNode.Value;
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "code"))
					{
						if (!(text2 == "message"))
						{
							if (!(text2 == "innererror"))
							{
								return false;
							}
							if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.InnerError))
							{
								return false;
							}
							ODataInnerError odataInnerError;
							if (!this.TryReadInnerErrorPropertyValue(out odataInnerError, 0))
							{
								return false;
							}
							error.InnerError = odataInnerError;
						}
						else
						{
							if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.Message))
							{
								return false;
							}
							if (!this.TryReadMessagePropertyValue(error))
							{
								return false;
							}
						}
					}
					else
					{
						if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.Code))
						{
							return false;
						}
						string text3;
						if (!this.TryReadErrorStringPropertyValue(out text3))
						{
							return false;
						}
						error.ErrorCode = text3;
					}
					this.ReadInternal();
					continue;
				}
				return false;
			}
			this.ReadInternal();
			return errorPropertyBitMask != ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.None;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00021C1C File Offset: 0x0001FE1C
		private bool TryReadMessagePropertyValue(ODataError error)
		{
			this.ReadInternal();
			if (this.currentBufferedNode.NodeType != JsonNodeType.StartObject)
			{
				return false;
			}
			this.ReadInternal();
			ODataVerboseJsonReaderUtils.ErrorPropertyBitMask errorPropertyBitMask = ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.None;
			while (this.currentBufferedNode.NodeType == JsonNodeType.Property)
			{
				string text = (string)this.currentBufferedNode.Value;
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "lang"))
					{
						if (!(text2 == "value"))
						{
							return false;
						}
						if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.MessageValue))
						{
							return false;
						}
						string text3;
						if (!this.TryReadErrorStringPropertyValue(out text3))
						{
							return false;
						}
						error.Message = text3;
					}
					else
					{
						if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.MessageLanguage))
						{
							return false;
						}
						string text4;
						if (!this.TryReadErrorStringPropertyValue(out text4))
						{
							return false;
						}
						error.MessageLanguage = text4;
					}
					this.ReadInternal();
					continue;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00021CE4 File Offset: 0x0001FEE4
		private bool TryReadInnerErrorPropertyValue(out ODataInnerError innerError, int recursionDepth)
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref recursionDepth, this.maxInnerErrorDepth);
			this.ReadInternal();
			if (this.currentBufferedNode.NodeType != JsonNodeType.StartObject)
			{
				innerError = null;
				return false;
			}
			this.ReadInternal();
			innerError = new ODataInnerError();
			ODataVerboseJsonReaderUtils.ErrorPropertyBitMask errorPropertyBitMask = ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.None;
			while (this.currentBufferedNode.NodeType == JsonNodeType.Property)
			{
				string text = (string)this.currentBufferedNode.Value;
				string text2;
				if ((text2 = text) == null)
				{
					goto IL_0125;
				}
				if (!(text2 == "message"))
				{
					if (!(text2 == "type"))
					{
						if (!(text2 == "stacktrace"))
						{
							if (!(text2 == "internalexception"))
							{
								goto IL_0125;
							}
							if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.InnerError))
							{
								return false;
							}
							ODataInnerError odataInnerError;
							if (!this.TryReadInnerErrorPropertyValue(out odataInnerError, recursionDepth))
							{
								return false;
							}
							innerError.InnerError = odataInnerError;
						}
						else
						{
							if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.StackTrace))
							{
								return false;
							}
							string text3;
							if (!this.TryReadErrorStringPropertyValue(out text3))
							{
								return false;
							}
							innerError.StackTrace = text3;
						}
					}
					else
					{
						if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.TypeName))
						{
							return false;
						}
						string text4;
						if (!this.TryReadErrorStringPropertyValue(out text4))
						{
							return false;
						}
						innerError.TypeName = text4;
					}
				}
				else
				{
					if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask.MessageValue))
					{
						return false;
					}
					string text5;
					if (!this.TryReadErrorStringPropertyValue(out text5))
					{
						return false;
					}
					innerError.Message = text5;
				}
				IL_012B:
				this.ReadInternal();
				continue;
				IL_0125:
				this.SkipValueInternal();
				goto IL_012B;
			}
			return true;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00021E38 File Offset: 0x00020038
		private bool TryReadErrorStringPropertyValue(out string stringValue)
		{
			this.ReadInternal();
			stringValue = this.currentBufferedNode.Value as string;
			return this.currentBufferedNode.NodeType == JsonNodeType.PrimitiveValue && (this.currentBufferedNode.Value == null || stringValue != null);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00021E88 File Offset: 0x00020088
		private void SkipValueInternal()
		{
			int num = 0;
			do
			{
				switch (this.currentBufferedNode.NodeType)
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
				this.ReadInternal();
			}
			while (num > 0);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00021EDC File Offset: 0x000200DC
		private void RemoveFirstNodeInBuffer()
		{
			if (this.bufferedNodesHead.Next == this.bufferedNodesHead)
			{
				this.bufferedNodesHead = null;
				return;
			}
			this.bufferedNodesHead.Previous.Next = this.bufferedNodesHead.Next;
			this.bufferedNodesHead.Next.Previous = this.bufferedNodesHead.Previous;
			this.bufferedNodesHead = this.bufferedNodesHead.Next;
		}

		// Token: 0x040003DE RID: 990
		protected BufferingJsonReader.BufferedNode bufferedNodesHead;

		// Token: 0x040003DF RID: 991
		protected BufferingJsonReader.BufferedNode currentBufferedNode;

		// Token: 0x040003E0 RID: 992
		private readonly int maxInnerErrorDepth;

		// Token: 0x040003E1 RID: 993
		private readonly string inStreamErrorPropertyName;

		// Token: 0x040003E2 RID: 994
		private bool isBuffering;

		// Token: 0x040003E3 RID: 995
		private bool removeOnNextRead;

		// Token: 0x040003E4 RID: 996
		private bool parsingInStreamError;

		// Token: 0x040003E5 RID: 997
		private bool disableInStreamErrorDetection;

		// Token: 0x02000172 RID: 370
		protected internal sealed class BufferedNode
		{
			// Token: 0x06000A36 RID: 2614 RVA: 0x00021F4B File Offset: 0x0002014B
			internal BufferedNode(JsonNodeType nodeType, object value, string rawValue)
			{
				this.nodeType = nodeType;
				this.nodeValue = value;
				this.nodeRawValue = rawValue;
				this.Previous = this;
				this.Next = this;
			}

			// Token: 0x17000277 RID: 631
			// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00021F76 File Offset: 0x00020176
			internal JsonNodeType NodeType
			{
				get
				{
					return this.nodeType;
				}
			}

			// Token: 0x17000278 RID: 632
			// (get) Token: 0x06000A38 RID: 2616 RVA: 0x00021F7E File Offset: 0x0002017E
			internal object Value
			{
				get
				{
					return this.nodeValue;
				}
			}

			// Token: 0x17000279 RID: 633
			// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00021F86 File Offset: 0x00020186
			internal string RawValue
			{
				get
				{
					return this.nodeRawValue;
				}
			}

			// Token: 0x1700027A RID: 634
			// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00021F8E File Offset: 0x0002018E
			// (set) Token: 0x06000A3B RID: 2619 RVA: 0x00021F96 File Offset: 0x00020196
			internal BufferingJsonReader.BufferedNode Previous { get; set; }

			// Token: 0x1700027B RID: 635
			// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00021F9F File Offset: 0x0002019F
			// (set) Token: 0x06000A3D RID: 2621 RVA: 0x00021FA7 File Offset: 0x000201A7
			internal BufferingJsonReader.BufferedNode Next { get; set; }

			// Token: 0x040003E6 RID: 998
			private readonly JsonNodeType nodeType;

			// Token: 0x040003E7 RID: 999
			private readonly object nodeValue;

			// Token: 0x040003E8 RID: 1000
			private readonly string nodeRawValue;
		}
	}
}
