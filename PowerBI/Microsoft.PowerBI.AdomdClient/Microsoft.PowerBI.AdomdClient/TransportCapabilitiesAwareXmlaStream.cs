using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200003A RID: 58
	internal abstract class TransportCapabilitiesAwareXmlaStream : XmlaStream
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x000120FF File Offset: 0x000102FF
		private protected TransportCapabilitiesAwareXmlaStream(bool isXmlaTracingSupported, XmlaDataType desiredRequestType, XmlaDataType desiredResponseType)
			: base(isXmlaTracingSupported)
		{
			this.desiredRequestType = desiredRequestType;
			this.desiredResponseType = desiredResponseType;
			this.InitTransportCapabilities();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0001211C File Offset: 0x0001031C
		private protected TransportCapabilitiesAwareXmlaStream(bool isXmlaTracingSupported, TransportCapabilitiesAwareXmlaStream originalStream)
			: base(isXmlaTracingSupported)
		{
			this.desiredRequestType = originalStream.desiredRequestType;
			this.desiredResponseType = originalStream.desiredResponseType;
			this.transportCapabilities = originalStream.transportCapabilities.Clone();
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0001214E File Offset: 0x0001034E
		private protected bool NegotiatedOptions
		{
			get
			{
				return this.transportCapabilities.ContentTypeNegotiated;
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0001215C File Offset: 0x0001035C
		public override XmlaDataType GetRequestDataType()
		{
			if (!this.finalRequestTypeCalculated)
			{
				this.finalRequestType = XmlaDataType.TextXml;
				switch (this.desiredRequestType)
				{
				case XmlaDataType.BinaryXml:
					if (this.transportCapabilities.RequestType == XmlaDataType.BinaryXml || this.transportCapabilities.RequestType == XmlaDataType.CompressedBinaryXml)
					{
						this.finalRequestType = XmlaDataType.BinaryXml;
					}
					break;
				case XmlaDataType.CompressedXml:
					if (this.transportCapabilities.RequestType == XmlaDataType.CompressedXml || this.transportCapabilities.RequestType == XmlaDataType.CompressedBinaryXml)
					{
						this.finalRequestType = XmlaDataType.CompressedXml;
					}
					break;
				case XmlaDataType.CompressedBinaryXml:
					if (this.transportCapabilities.RequestType == XmlaDataType.CompressedBinaryXml)
					{
						this.finalRequestType = XmlaDataType.CompressedBinaryXml;
					}
					else if (this.transportCapabilities.RequestType == XmlaDataType.CompressedXml)
					{
						this.finalRequestType = XmlaDataType.CompressedXml;
					}
					else if (this.transportCapabilities.RequestType == XmlaDataType.BinaryXml)
					{
						this.finalRequestType = XmlaDataType.BinaryXml;
					}
					break;
				}
				if (this.NegotiatedOptions)
				{
					this.finalRequestTypeCalculated = true;
				}
			}
			return this.finalRequestType;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0001223D File Offset: 0x0001043D
		protected internal void SetTransportCapabilities(TransportCapabilities capabilities)
		{
			if (capabilities != null)
			{
				this.transportCapabilities = capabilities.Clone();
				return;
			}
			this.InitTransportCapabilities();
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00012255 File Offset: 0x00010455
		internal TransportCapabilities GetTransportCapabilities()
		{
			return this.transportCapabilities.Clone();
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00012262 File Offset: 0x00010462
		internal string GetTransportCapabilitiesString()
		{
			return this.transportCapabilities.GetString();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0001226F File Offset: 0x0001046F
		internal void SetRequestDataType(XmlaDataType value)
		{
			this.finalRequestTypeCalculated = true;
			this.finalRequestType = value;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00012280 File Offset: 0x00010480
		private void InitTransportCapabilities()
		{
			this.transportCapabilities = new TransportCapabilities();
			this.transportCapabilities.ContentTypeNegotiated = false;
			this.transportCapabilities.ResponseBinary = this.desiredResponseType == XmlaDataType.CompressedBinaryXml || this.desiredResponseType == XmlaDataType.BinaryXml;
			this.transportCapabilities.ResponseCompression = this.desiredResponseType == XmlaDataType.CompressedBinaryXml || this.desiredResponseType == XmlaDataType.CompressedXml;
		}

		// Token: 0x04000237 RID: 567
		private XmlaDataType desiredRequestType;

		// Token: 0x04000238 RID: 568
		private XmlaDataType desiredResponseType;

		// Token: 0x04000239 RID: 569
		private TransportCapabilities transportCapabilities;

		// Token: 0x0400023A RID: 570
		private bool finalRequestTypeCalculated;

		// Token: 0x0400023B RID: 571
		private XmlaDataType finalRequestType;
	}
}
