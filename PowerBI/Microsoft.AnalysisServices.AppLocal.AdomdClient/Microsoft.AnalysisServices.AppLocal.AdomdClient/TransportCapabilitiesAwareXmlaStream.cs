using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200003A RID: 58
	internal abstract class TransportCapabilitiesAwareXmlaStream : XmlaStream
	{
		// Token: 0x06000306 RID: 774 RVA: 0x0001242F File Offset: 0x0001062F
		private protected TransportCapabilitiesAwareXmlaStream(bool isXmlaTracingSupported, XmlaDataType desiredRequestType, XmlaDataType desiredResponseType)
			: base(isXmlaTracingSupported)
		{
			this.desiredRequestType = desiredRequestType;
			this.desiredResponseType = desiredResponseType;
			this.InitTransportCapabilities();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0001244C File Offset: 0x0001064C
		private protected TransportCapabilitiesAwareXmlaStream(bool isXmlaTracingSupported, TransportCapabilitiesAwareXmlaStream originalStream)
			: base(isXmlaTracingSupported)
		{
			this.desiredRequestType = originalStream.desiredRequestType;
			this.desiredResponseType = originalStream.desiredResponseType;
			this.transportCapabilities = originalStream.transportCapabilities.Clone();
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0001247E File Offset: 0x0001067E
		private protected bool NegotiatedOptions
		{
			get
			{
				return this.transportCapabilities.ContentTypeNegotiated;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0001248C File Offset: 0x0001068C
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

		// Token: 0x0600030A RID: 778 RVA: 0x0001256D File Offset: 0x0001076D
		protected internal void SetTransportCapabilities(TransportCapabilities capabilities)
		{
			if (capabilities != null)
			{
				this.transportCapabilities = capabilities.Clone();
				return;
			}
			this.InitTransportCapabilities();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00012585 File Offset: 0x00010785
		internal TransportCapabilities GetTransportCapabilities()
		{
			return this.transportCapabilities.Clone();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00012592 File Offset: 0x00010792
		internal string GetTransportCapabilitiesString()
		{
			return this.transportCapabilities.GetString();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001259F File Offset: 0x0001079F
		internal void SetRequestDataType(XmlaDataType value)
		{
			this.finalRequestTypeCalculated = true;
			this.finalRequestType = value;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000125B0 File Offset: 0x000107B0
		private void InitTransportCapabilities()
		{
			this.transportCapabilities = new TransportCapabilities();
			this.transportCapabilities.ContentTypeNegotiated = false;
			this.transportCapabilities.ResponseBinary = this.desiredResponseType == XmlaDataType.CompressedBinaryXml || this.desiredResponseType == XmlaDataType.BinaryXml;
			this.transportCapabilities.ResponseCompression = this.desiredResponseType == XmlaDataType.CompressedBinaryXml || this.desiredResponseType == XmlaDataType.CompressedXml;
		}

		// Token: 0x04000244 RID: 580
		private XmlaDataType desiredRequestType;

		// Token: 0x04000245 RID: 581
		private XmlaDataType desiredResponseType;

		// Token: 0x04000246 RID: 582
		private TransportCapabilities transportCapabilities;

		// Token: 0x04000247 RID: 583
		private bool finalRequestTypeCalculated;

		// Token: 0x04000248 RID: 584
		private XmlaDataType finalRequestType;
	}
}
