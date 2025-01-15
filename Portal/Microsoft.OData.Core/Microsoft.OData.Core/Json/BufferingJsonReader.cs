using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.OData.JsonLight;

namespace Microsoft.OData.Json
{
	// Token: 0x02000210 RID: 528
	internal class BufferingJsonReader : IJsonStreamReader, IJsonReader
	{
		// Token: 0x0600170E RID: 5902 RVA: 0x00041024 File Offset: 0x0003F224
		internal BufferingJsonReader(IJsonReader innerReader, string inStreamErrorPropertyName, int maxInnerErrorDepth)
		{
			this.innerReader = innerReader;
			this.inStreamErrorPropertyName = inStreamErrorPropertyName;
			this.maxInnerErrorDepth = maxInnerErrorDepth;
			this.bufferedNodesHead = null;
			this.currentBufferedNode = null;
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x0004104F File Offset: 0x0003F24F
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

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00041084 File Offset: 0x0003F284
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

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x000410B9 File Offset: 0x0003F2B9
		public bool IsIeee754Compatible
		{
			get
			{
				return this.innerReader.IsIeee754Compatible;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x000410C6 File Offset: 0x0003F2C6
		// (set) Token: 0x06001713 RID: 5907 RVA: 0x000410CE File Offset: 0x0003F2CE
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

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x000410D7 File Offset: 0x0003F2D7
		internal bool IsBuffering
		{
			get
			{
				return this.isBuffering;
			}
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x000410E0 File Offset: 0x0003F2E0
		public virtual Stream CreateReadStream()
		{
			IJsonStreamReader jsonStreamReader = this.innerReader as IJsonStreamReader;
			if (!this.isBuffering && jsonStreamReader != null)
			{
				return jsonStreamReader.CreateReadStream();
			}
			Stream stream = new MemoryStream((this.Value == null) ? new byte[0] : Convert.FromBase64String((string)this.Value));
			this.innerReader.Read();
			return stream;
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00041140 File Offset: 0x0003F340
		public virtual TextReader CreateTextReader()
		{
			IJsonStreamReader jsonStreamReader = this.innerReader as IJsonStreamReader;
			if (!this.isBuffering && jsonStreamReader != null)
			{
				return jsonStreamReader.CreateTextReader();
			}
			TextReader textReader = new StringReader((this.Value == null) ? "" : ((string)this.Value));
			this.innerReader.Read();
			return textReader;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00041198 File Offset: 0x0003F398
		public virtual bool CanStream()
		{
			IJsonStreamReader jsonStreamReader = this.innerReader as IJsonStreamReader;
			if (!this.isBuffering && jsonStreamReader != null)
			{
				return jsonStreamReader.CanStream();
			}
			return this.Value is string || this.Value == null || this.NodeType == JsonNodeType.StartArray || this.NodeType == JsonNodeType.StartObject;
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000411EC File Offset: 0x0003F3EC
		public bool Read()
		{
			return this.ReadInternal();
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x000411F4 File Offset: 0x0003F3F4
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

		// Token: 0x0600171A RID: 5914 RVA: 0x0004124E File Offset: 0x0003F44E
		internal object BookmarkCurrentPosition()
		{
			return this.currentBufferedNode;
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00041258 File Offset: 0x0003F458
		internal void MoveToBookmark(object bookmark)
		{
			BufferingJsonReader.BufferedNode bufferedNode = bookmark as BufferingJsonReader.BufferedNode;
			this.currentBufferedNode = bufferedNode;
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00041273 File Offset: 0x0003F473
		internal void StopBuffering()
		{
			this.isBuffering = false;
			this.removeOnNextRead = true;
			this.currentBufferedNode = null;
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0004128C File Offset: 0x0003F48C
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

		// Token: 0x0600171E RID: 5918 RVA: 0x000412D4 File Offset: 0x0003F4D4
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

		// Token: 0x0600171F RID: 5919 RVA: 0x000413E8 File Offset: 0x0003F5E8
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

		// Token: 0x06001720 RID: 5920 RVA: 0x0004145C File Offset: 0x0003F65C
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

		// Token: 0x06001721 RID: 5921 RVA: 0x000414DC File Offset: 0x0003F6DC
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

		// Token: 0x06001722 RID: 5922 RVA: 0x00041650 File Offset: 0x0003F850
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

		// Token: 0x06001723 RID: 5923 RVA: 0x000416A0 File Offset: 0x0003F8A0
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

		// Token: 0x06001724 RID: 5924 RVA: 0x0004179C File Offset: 0x0003F99C
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

		// Token: 0x06001725 RID: 5925 RVA: 0x000418E0 File Offset: 0x0003FAE0
		private bool TryReadErrorStringPropertyValue(out string stringValue)
		{
			this.ReadInternal();
			stringValue = this.currentBufferedNode.Value as string;
			return this.currentBufferedNode.NodeType == JsonNodeType.PrimitiveValue && (this.currentBufferedNode.Value == null || stringValue != null);
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00041920 File Offset: 0x0003FB20
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

		// Token: 0x06001727 RID: 5927 RVA: 0x00041974 File Offset: 0x0003FB74
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

		// Token: 0x04000A5E RID: 2654
		protected BufferingJsonReader.BufferedNode bufferedNodesHead;

		// Token: 0x04000A5F RID: 2655
		protected BufferingJsonReader.BufferedNode currentBufferedNode;

		// Token: 0x04000A60 RID: 2656
		private readonly IJsonReader innerReader;

		// Token: 0x04000A61 RID: 2657
		private readonly int maxInnerErrorDepth;

		// Token: 0x04000A62 RID: 2658
		private readonly string inStreamErrorPropertyName;

		// Token: 0x04000A63 RID: 2659
		private bool isBuffering;

		// Token: 0x04000A64 RID: 2660
		private bool removeOnNextRead;

		// Token: 0x04000A65 RID: 2661
		private bool parsingInStreamError;

		// Token: 0x04000A66 RID: 2662
		private bool disableInStreamErrorDetection;

		// Token: 0x020003D9 RID: 985
		protected internal sealed class BufferedNode
		{
			// Token: 0x060020AF RID: 8367 RVA: 0x0005C1BB File Offset: 0x0005A3BB
			internal BufferedNode(JsonNodeType nodeType, object value)
			{
				this.nodeType = nodeType;
				this.nodeValue = value;
				this.Previous = this;
				this.Next = this;
			}

			// Token: 0x17000640 RID: 1600
			// (get) Token: 0x060020B0 RID: 8368 RVA: 0x0005C1DF File Offset: 0x0005A3DF
			internal JsonNodeType NodeType
			{
				get
				{
					return this.nodeType;
				}
			}

			// Token: 0x17000641 RID: 1601
			// (get) Token: 0x060020B1 RID: 8369 RVA: 0x0005C1E7 File Offset: 0x0005A3E7
			internal object Value
			{
				get
				{
					return this.nodeValue;
				}
			}

			// Token: 0x17000642 RID: 1602
			// (get) Token: 0x060020B2 RID: 8370 RVA: 0x0005C1EF File Offset: 0x0005A3EF
			// (set) Token: 0x060020B3 RID: 8371 RVA: 0x0005C1F7 File Offset: 0x0005A3F7
			internal BufferingJsonReader.BufferedNode Previous { get; set; }

			// Token: 0x17000643 RID: 1603
			// (get) Token: 0x060020B4 RID: 8372 RVA: 0x0005C200 File Offset: 0x0005A400
			// (set) Token: 0x060020B5 RID: 8373 RVA: 0x0005C208 File Offset: 0x0005A408
			internal BufferingJsonReader.BufferedNode Next { get; set; }

			// Token: 0x04000F56 RID: 3926
			private readonly JsonNodeType nodeType;

			// Token: 0x04000F57 RID: 3927
			private readonly object nodeValue;
		}
	}
}
