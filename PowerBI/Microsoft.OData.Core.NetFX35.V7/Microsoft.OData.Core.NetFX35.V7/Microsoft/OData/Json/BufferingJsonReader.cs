using System;
using System.Collections.Generic;
using Microsoft.OData.JsonLight;

namespace Microsoft.OData.Json
{
	// Token: 0x020001DE RID: 478
	internal class BufferingJsonReader : IJsonReader
	{
		// Token: 0x060012AE RID: 4782 RVA: 0x00035A86 File Offset: 0x00033C86
		internal BufferingJsonReader(IJsonReader innerReader, string inStreamErrorPropertyName, int maxInnerErrorDepth)
		{
			this.innerReader = innerReader;
			this.inStreamErrorPropertyName = inStreamErrorPropertyName;
			this.maxInnerErrorDepth = maxInnerErrorDepth;
			this.bufferedNodesHead = null;
			this.currentBufferedNode = null;
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x00035AB1 File Offset: 0x00033CB1
		public JsonNodeType NodeType
		{
			get
			{
				if (this.bufferedNodesHead == null)
				{
					return this.innerReader.NodeType;
				}
				if (this.isBuffering)
				{
					return this.currentBufferedNode.NodeType;
				}
				return this.bufferedNodesHead.NodeType;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x00035AE6 File Offset: 0x00033CE6
		public object Value
		{
			get
			{
				if (this.bufferedNodesHead == null)
				{
					return this.innerReader.Value;
				}
				if (this.isBuffering)
				{
					return this.currentBufferedNode.Value;
				}
				return this.bufferedNodesHead.Value;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x00035B1B File Offset: 0x00033D1B
		public bool IsIeee754Compatible
		{
			get
			{
				return this.innerReader.IsIeee754Compatible;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x00035B28 File Offset: 0x00033D28
		// (set) Token: 0x060012B3 RID: 4787 RVA: 0x00035B30 File Offset: 0x00033D30
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

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00035B39 File Offset: 0x00033D39
		internal bool IsBuffering
		{
			get
			{
				return this.isBuffering;
			}
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00035B41 File Offset: 0x00033D41
		public bool Read()
		{
			return this.ReadInternal();
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00035B4C File Offset: 0x00033D4C
		internal void StartBuffering()
		{
			if (this.bufferedNodesHead == null)
			{
				this.bufferedNodesHead = new BufferingJsonReader.BufferedNode(this.innerReader.NodeType, this.innerReader.Value);
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

		// Token: 0x060012B7 RID: 4791 RVA: 0x00035BA6 File Offset: 0x00033DA6
		internal object BookmarkCurrentPosition()
		{
			return this.currentBufferedNode;
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00035BB0 File Offset: 0x00033DB0
		internal void MoveToBookmark(object bookmark)
		{
			BufferingJsonReader.BufferedNode bufferedNode = bookmark as BufferingJsonReader.BufferedNode;
			this.currentBufferedNode = bufferedNode;
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00035BCB File Offset: 0x00033DCB
		internal void StopBuffering()
		{
			this.isBuffering = false;
			this.removeOnNextRead = true;
			this.currentBufferedNode = null;
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00035BE4 File Offset: 0x00033DE4
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

		// Token: 0x060012BB RID: 4795 RVA: 0x00035C2C File Offset: 0x00033E2C
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
					flag = this.innerReader.Read();
					BufferingJsonReader.BufferedNode bufferedNode = new BufferingJsonReader.BufferedNode(this.innerReader.NodeType, this.innerReader.Value);
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
				flag = (this.parsingInStreamError ? this.innerReader.Read() : this.ReadNextAndCheckForInStreamError());
			}
			else
			{
				flag = this.bufferedNodesHead.NodeType != JsonNodeType.EndOfInput;
				this.removeOnNextRead = true;
			}
			return flag;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00035D40 File Offset: 0x00033F40
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

		// Token: 0x060012BD RID: 4797 RVA: 0x00035DB4 File Offset: 0x00033FB4
		private bool ReadNextAndCheckForInStreamError()
		{
			this.parsingInStreamError = true;
			bool flag3;
			try
			{
				bool flag = this.ReadInternal();
				if (this.innerReader.NodeType == JsonNodeType.StartObject)
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

		// Token: 0x060012BE RID: 4798 RVA: 0x00035E34 File Offset: 0x00034034
		private bool TryReadInStreamErrorPropertyValue(out ODataError error)
		{
			error = null;
			if (this.currentBufferedNode.NodeType != JsonNodeType.StartObject)
			{
				return false;
			}
			this.ReadInternal();
			error = new ODataError();
			ODataJsonLightReaderUtils.ErrorPropertyBitMask errorPropertyBitMask = ODataJsonLightReaderUtils.ErrorPropertyBitMask.None;
			while (this.currentBufferedNode.NodeType == JsonNodeType.Property)
			{
				string text = (string)this.currentBufferedNode.Value;
				if (!(text == "code"))
				{
					if (!(text == "message"))
					{
						if (!(text == "target"))
						{
							if (!(text == "details"))
							{
								if (!(text == "innererror"))
								{
									return false;
								}
								if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.InnerError))
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
								if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.Details))
								{
									return false;
								}
								ICollection<ODataErrorDetail> collection;
								if (!this.TryReadErrorDetailsPropertyValue(out collection))
								{
									return false;
								}
								error.Details = collection;
							}
						}
						else
						{
							if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.Target))
							{
								return false;
							}
							string text2;
							if (!this.TryReadErrorStringPropertyValue(out text2))
							{
								return false;
							}
							error.Target = text2;
						}
					}
					else
					{
						if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.Message))
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
				}
				else
				{
					if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.Code))
					{
						return false;
					}
					string text4;
					if (!this.TryReadErrorStringPropertyValue(out text4))
					{
						return false;
					}
					error.ErrorCode = text4;
				}
				this.ReadInternal();
			}
			this.ReadInternal();
			return errorPropertyBitMask > ODataJsonLightReaderUtils.ErrorPropertyBitMask.None;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00035FA8 File Offset: 0x000341A8
		private bool TryReadErrorDetailsPropertyValue(out ICollection<ODataErrorDetail> details)
		{
			this.ReadInternal();
			if (this.currentBufferedNode.NodeType != JsonNodeType.StartArray)
			{
				details = null;
				return false;
			}
			this.ReadInternal();
			details = new List<ODataErrorDetail>();
			ODataErrorDetail odataErrorDetail;
			if (this.TryReadErrorDetail(out odataErrorDetail))
			{
				details.Add(odataErrorDetail);
			}
			this.ReadInternal();
			return true;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00035FF8 File Offset: 0x000341F8
		private bool TryReadErrorDetail(out ODataErrorDetail detail)
		{
			if (this.currentBufferedNode.NodeType != JsonNodeType.StartObject)
			{
				detail = null;
				return false;
			}
			this.ReadInternal();
			detail = new ODataErrorDetail();
			ODataJsonLightReaderUtils.ErrorPropertyBitMask errorPropertyBitMask = ODataJsonLightReaderUtils.ErrorPropertyBitMask.None;
			while (this.currentBufferedNode.NodeType == JsonNodeType.Property)
			{
				string text = (string)this.currentBufferedNode.Value;
				if (!(text == "code"))
				{
					if (!(text == "target"))
					{
						if (!(text == "message"))
						{
							this.SkipValueInternal();
						}
						else
						{
							if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.MessageValue))
							{
								return false;
							}
							string text2;
							if (!this.TryReadErrorStringPropertyValue(out text2))
							{
								return false;
							}
							detail.Message = text2;
						}
					}
					else
					{
						if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.Target))
						{
							return false;
						}
						string text3;
						if (!this.TryReadErrorStringPropertyValue(out text3))
						{
							return false;
						}
						detail.Target = text3;
					}
				}
				else
				{
					if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.Code))
					{
						return false;
					}
					string text4;
					if (!this.TryReadErrorStringPropertyValue(out text4))
					{
						return false;
					}
					detail.ErrorCode = text4;
				}
				this.ReadInternal();
			}
			return true;
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x000360F4 File Offset: 0x000342F4
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
			ODataJsonLightReaderUtils.ErrorPropertyBitMask errorPropertyBitMask = ODataJsonLightReaderUtils.ErrorPropertyBitMask.None;
			while (this.currentBufferedNode.NodeType == JsonNodeType.Property)
			{
				string text = (string)this.currentBufferedNode.Value;
				if (!(text == "message"))
				{
					if (!(text == "type"))
					{
						if (!(text == "stacktrace"))
						{
							if (!(text == "internalexception"))
							{
								this.SkipValueInternal();
							}
							else
							{
								if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.InnerError))
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
						}
						else
						{
							if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.StackTrace))
							{
								return false;
							}
							string text2;
							if (!this.TryReadErrorStringPropertyValue(out text2))
							{
								return false;
							}
							innerError.StackTrace = text2;
						}
					}
					else
					{
						if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.TypeName))
						{
							return false;
						}
						string text3;
						if (!this.TryReadErrorStringPropertyValue(out text3))
						{
							return false;
						}
						innerError.TypeName = text3;
					}
				}
				else
				{
					if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.MessageValue))
					{
						return false;
					}
					string text4;
					if (!this.TryReadErrorStringPropertyValue(out text4))
					{
						return false;
					}
					innerError.Message = text4;
				}
				this.ReadInternal();
			}
			return true;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00036238 File Offset: 0x00034438
		private bool TryReadErrorStringPropertyValue(out string stringValue)
		{
			this.ReadInternal();
			stringValue = this.currentBufferedNode.Value as string;
			return this.currentBufferedNode.NodeType == JsonNodeType.PrimitiveValue && (this.currentBufferedNode.Value == null || stringValue != null);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00036278 File Offset: 0x00034478
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

		// Token: 0x060012C4 RID: 4804 RVA: 0x000362CC File Offset: 0x000344CC
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

		// Token: 0x0400097E RID: 2430
		protected BufferingJsonReader.BufferedNode bufferedNodesHead;

		// Token: 0x0400097F RID: 2431
		protected BufferingJsonReader.BufferedNode currentBufferedNode;

		// Token: 0x04000980 RID: 2432
		private readonly IJsonReader innerReader;

		// Token: 0x04000981 RID: 2433
		private readonly int maxInnerErrorDepth;

		// Token: 0x04000982 RID: 2434
		private readonly string inStreamErrorPropertyName;

		// Token: 0x04000983 RID: 2435
		private bool isBuffering;

		// Token: 0x04000984 RID: 2436
		private bool removeOnNextRead;

		// Token: 0x04000985 RID: 2437
		private bool parsingInStreamError;

		// Token: 0x04000986 RID: 2438
		private bool disableInStreamErrorDetection;

		// Token: 0x02000313 RID: 787
		protected internal sealed class BufferedNode
		{
			// Token: 0x06001A18 RID: 6680 RVA: 0x0004AEB4 File Offset: 0x000490B4
			internal BufferedNode(JsonNodeType nodeType, object value)
			{
				this.nodeType = nodeType;
				this.nodeValue = value;
				this.Previous = this;
				this.Next = this;
			}

			// Token: 0x1700059A RID: 1434
			// (get) Token: 0x06001A19 RID: 6681 RVA: 0x0004AED8 File Offset: 0x000490D8
			internal JsonNodeType NodeType
			{
				get
				{
					return this.nodeType;
				}
			}

			// Token: 0x1700059B RID: 1435
			// (get) Token: 0x06001A1A RID: 6682 RVA: 0x0004AEE0 File Offset: 0x000490E0
			internal object Value
			{
				get
				{
					return this.nodeValue;
				}
			}

			// Token: 0x1700059C RID: 1436
			// (get) Token: 0x06001A1B RID: 6683 RVA: 0x0004AEE8 File Offset: 0x000490E8
			// (set) Token: 0x06001A1C RID: 6684 RVA: 0x0004AEF0 File Offset: 0x000490F0
			internal BufferingJsonReader.BufferedNode Previous { get; set; }

			// Token: 0x1700059D RID: 1437
			// (get) Token: 0x06001A1D RID: 6685 RVA: 0x0004AEF9 File Offset: 0x000490F9
			// (set) Token: 0x06001A1E RID: 6686 RVA: 0x0004AF01 File Offset: 0x00049101
			internal BufferingJsonReader.BufferedNode Next { get; set; }

			// Token: 0x04000CD7 RID: 3287
			private readonly JsonNodeType nodeType;

			// Token: 0x04000CD8 RID: 3288
			private readonly object nodeValue;
		}
	}
}
