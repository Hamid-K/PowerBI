using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.OData.Core.JsonLight;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x02000108 RID: 264
	internal class BufferingJsonReader : JsonReader
	{
		// Token: 0x060009F2 RID: 2546 RVA: 0x0002490C File Offset: 0x00022B0C
		internal BufferingJsonReader(TextReader reader, string inStreamErrorPropertyName, int maxInnerErrorDepth, ODataFormat jsonFormat, bool isIeee754Compatible)
			: base(reader, jsonFormat, isIeee754Compatible)
		{
			this.inStreamErrorPropertyName = inStreamErrorPropertyName;
			this.maxInnerErrorDepth = maxInnerErrorDepth;
			this.bufferedNodesHead = null;
			this.currentBufferedNode = null;
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00024935 File Offset: 0x00022B35
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

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00024965 File Offset: 0x00022B65
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

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00024995 File Offset: 0x00022B95
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x0002499D File Offset: 0x00022B9D
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

		// Token: 0x060009F7 RID: 2551 RVA: 0x000249A6 File Offset: 0x00022BA6
		public override bool Read()
		{
			return this.ReadInternal();
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x000249B0 File Offset: 0x00022BB0
		internal void StartBuffering()
		{
			if (this.bufferedNodesHead == null)
			{
				this.bufferedNodesHead = new BufferingJsonReader.BufferedNode(base.NodeType, base.Value);
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

		// Token: 0x060009F9 RID: 2553 RVA: 0x00024A00 File Offset: 0x00022C00
		internal object BookmarkCurrentPosition()
		{
			return this.currentBufferedNode;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00024A08 File Offset: 0x00022C08
		internal void MoveToBookmark(object bookmark)
		{
			BufferingJsonReader.BufferedNode bufferedNode = bookmark as BufferingJsonReader.BufferedNode;
			this.currentBufferedNode = bufferedNode;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00024A23 File Offset: 0x00022C23
		internal void StopBuffering()
		{
			this.isBuffering = false;
			this.removeOnNextRead = true;
			this.currentBufferedNode = null;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00024A3C File Offset: 0x00022C3C
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

		// Token: 0x060009FD RID: 2557 RVA: 0x00024A84 File Offset: 0x00022C84
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
					BufferingJsonReader.BufferedNode bufferedNode = new BufferingJsonReader.BufferedNode(base.NodeType, base.Value);
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

		// Token: 0x060009FE RID: 2558 RVA: 0x00024B84 File Offset: 0x00022D84
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

		// Token: 0x060009FF RID: 2559 RVA: 0x00024BF8 File Offset: 0x00022DF8
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

		// Token: 0x06000A00 RID: 2560 RVA: 0x00024C74 File Offset: 0x00022E74
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
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "code"))
					{
						if (!(text2 == "message"))
						{
							if (!(text2 == "target"))
							{
								if (!(text2 == "details"))
								{
									if (!(text2 == "innererror"))
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
								string text3;
								if (!this.TryReadErrorStringPropertyValue(out text3))
								{
									return false;
								}
								error.Target = text3;
							}
						}
						else
						{
							if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.Message))
							{
								return false;
							}
							string text4;
							if (!this.TryReadErrorStringPropertyValue(out text4))
							{
								return false;
							}
							error.Message = text4;
						}
					}
					else
					{
						if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.Code))
						{
							return false;
						}
						string text5;
						if (!this.TryReadErrorStringPropertyValue(out text5))
						{
							return false;
						}
						error.ErrorCode = text5;
					}
					this.ReadInternal();
					continue;
				}
				return false;
			}
			this.ReadInternal();
			return errorPropertyBitMask != ODataJsonLightReaderUtils.ErrorPropertyBitMask.None;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00024DF8 File Offset: 0x00022FF8
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

		// Token: 0x06000A02 RID: 2562 RVA: 0x00024E48 File Offset: 0x00023048
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
				string text2;
				if ((text2 = text) == null)
				{
					goto IL_00DA;
				}
				if (!(text2 == "code"))
				{
					if (!(text2 == "target"))
					{
						if (!(text2 == "message"))
						{
							goto IL_00DA;
						}
						if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.MessageValue))
						{
							return false;
						}
						string text3;
						if (!this.TryReadErrorStringPropertyValue(out text3))
						{
							return false;
						}
						detail.Message = text3;
					}
					else
					{
						if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.Target))
						{
							return false;
						}
						string text4;
						if (!this.TryReadErrorStringPropertyValue(out text4))
						{
							return false;
						}
						detail.Target = text4;
					}
				}
				else
				{
					if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.Code))
					{
						return false;
					}
					string text5;
					if (!this.TryReadErrorStringPropertyValue(out text5))
					{
						return false;
					}
					detail.ErrorCode = text5;
				}
				IL_00E0:
				this.ReadInternal();
				continue;
				IL_00DA:
				this.SkipValueInternal();
				goto IL_00E0;
			}
			return true;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00024F50 File Offset: 0x00023150
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
						else
						{
							if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.StackTrace))
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
						if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.TypeName))
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
					if (!ODataJsonLightReaderUtils.ErrorPropertyNotFound(ref errorPropertyBitMask, ODataJsonLightReaderUtils.ErrorPropertyBitMask.MessageValue))
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

		// Token: 0x06000A04 RID: 2564 RVA: 0x000250A4 File Offset: 0x000232A4
		private bool TryReadErrorStringPropertyValue(out string stringValue)
		{
			this.ReadInternal();
			stringValue = this.currentBufferedNode.Value as string;
			return this.currentBufferedNode.NodeType == JsonNodeType.PrimitiveValue && (this.currentBufferedNode.Value == null || stringValue != null);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000250F4 File Offset: 0x000232F4
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

		// Token: 0x06000A06 RID: 2566 RVA: 0x00025148 File Offset: 0x00023348
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

		// Token: 0x040003F9 RID: 1017
		protected BufferingJsonReader.BufferedNode bufferedNodesHead;

		// Token: 0x040003FA RID: 1018
		protected BufferingJsonReader.BufferedNode currentBufferedNode;

		// Token: 0x040003FB RID: 1019
		private readonly int maxInnerErrorDepth;

		// Token: 0x040003FC RID: 1020
		private readonly string inStreamErrorPropertyName;

		// Token: 0x040003FD RID: 1021
		private bool isBuffering;

		// Token: 0x040003FE RID: 1022
		private bool removeOnNextRead;

		// Token: 0x040003FF RID: 1023
		private bool parsingInStreamError;

		// Token: 0x04000400 RID: 1024
		private bool disableInStreamErrorDetection;

		// Token: 0x02000109 RID: 265
		protected internal sealed class BufferedNode
		{
			// Token: 0x06000A07 RID: 2567 RVA: 0x000251B7 File Offset: 0x000233B7
			internal BufferedNode(JsonNodeType nodeType, object value)
			{
				this.nodeType = nodeType;
				this.nodeValue = value;
				this.Previous = this;
				this.Next = this;
			}

			// Token: 0x17000229 RID: 553
			// (get) Token: 0x06000A08 RID: 2568 RVA: 0x000251DB File Offset: 0x000233DB
			internal JsonNodeType NodeType
			{
				get
				{
					return this.nodeType;
				}
			}

			// Token: 0x1700022A RID: 554
			// (get) Token: 0x06000A09 RID: 2569 RVA: 0x000251E3 File Offset: 0x000233E3
			internal object Value
			{
				get
				{
					return this.nodeValue;
				}
			}

			// Token: 0x1700022B RID: 555
			// (get) Token: 0x06000A0A RID: 2570 RVA: 0x000251EB File Offset: 0x000233EB
			// (set) Token: 0x06000A0B RID: 2571 RVA: 0x000251F3 File Offset: 0x000233F3
			internal BufferingJsonReader.BufferedNode Previous { get; set; }

			// Token: 0x1700022C RID: 556
			// (get) Token: 0x06000A0C RID: 2572 RVA: 0x000251FC File Offset: 0x000233FC
			// (set) Token: 0x06000A0D RID: 2573 RVA: 0x00025204 File Offset: 0x00023404
			internal BufferingJsonReader.BufferedNode Next { get; set; }

			// Token: 0x04000401 RID: 1025
			private readonly JsonNodeType nodeType;

			// Token: 0x04000402 RID: 1026
			private readonly object nodeValue;
		}
	}
}
