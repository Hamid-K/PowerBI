using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000056 RID: 86
	internal abstract class TransportCapabilitiesAwareXmlaStream : XmlaStream
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x000152C7 File Offset: 0x000134C7
		private protected TransportCapabilitiesAwareXmlaStream(bool isXmlaTracingSupported, XmlaDataType desiredRequestType, XmlaDataType desiredResponseType)
			: base(isXmlaTracingSupported)
		{
			this.desiredRequestType = desiredRequestType;
			this.desiredResponseType = desiredResponseType;
			this.InitTransportCapabilities();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000152E4 File Offset: 0x000134E4
		private protected TransportCapabilitiesAwareXmlaStream(bool isXmlaTracingSupported, TransportCapabilitiesAwareXmlaStream originalStream)
			: base(isXmlaTracingSupported)
		{
			this.desiredRequestType = originalStream.desiredRequestType;
			this.desiredResponseType = originalStream.desiredResponseType;
			this.transportCapabilities = originalStream.transportCapabilities.Clone();
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00015316 File Offset: 0x00013516
		private protected bool NegotiatedOptions
		{
			get
			{
				return this.transportCapabilities.ContentTypeNegotiated;
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00015324 File Offset: 0x00013524
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

		// Token: 0x060003AA RID: 938 RVA: 0x00015405 File Offset: 0x00013605
		protected internal void SetTransportCapabilities(TransportCapabilities capabilities)
		{
			if (capabilities != null)
			{
				this.transportCapabilities = capabilities.Clone();
				return;
			}
			this.InitTransportCapabilities();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001541D File Offset: 0x0001361D
		internal TransportCapabilities GetTransportCapabilities()
		{
			return this.transportCapabilities.Clone();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001542A File Offset: 0x0001362A
		internal string GetTransportCapabilitiesString()
		{
			return this.transportCapabilities.GetString();
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00015437 File Offset: 0x00013637
		internal void SetRequestDataType(XmlaDataType value)
		{
			this.finalRequestTypeCalculated = true;
			this.finalRequestType = value;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00015448 File Offset: 0x00013648
		private void InitTransportCapabilities()
		{
			this.transportCapabilities = new TransportCapabilities();
			this.transportCapabilities.ContentTypeNegotiated = false;
			this.transportCapabilities.ResponseBinary = this.desiredResponseType == XmlaDataType.CompressedBinaryXml || this.desiredResponseType == XmlaDataType.BinaryXml;
			this.transportCapabilities.ResponseCompression = this.desiredResponseType == XmlaDataType.CompressedBinaryXml || this.desiredResponseType == XmlaDataType.CompressedXml;
		}

		// Token: 0x04000288 RID: 648
		private XmlaDataType desiredRequestType;

		// Token: 0x04000289 RID: 649
		private XmlaDataType desiredResponseType;

		// Token: 0x0400028A RID: 650
		private TransportCapabilities transportCapabilities;

		// Token: 0x0400028B RID: 651
		private bool finalRequestTypeCalculated;

		// Token: 0x0400028C RID: 652
		private XmlaDataType finalRequestType;
	}
}
