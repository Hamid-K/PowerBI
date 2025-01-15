using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200041D RID: 1053
	internal sealed class SignatureEncodingBindingElement : MessageEncodingBindingElement
	{
		// Token: 0x060024B7 RID: 9399 RVA: 0x000707E3 File Offset: 0x0006E9E3
		public SignatureEncodingBindingElement()
			: this(new BinaryMessageEncodingBindingElement())
		{
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000707F0 File Offset: 0x0006E9F0
		public SignatureEncodingBindingElement(MessageEncodingBindingElement innerBindingElement)
		{
			this.m_innerBindingElement = innerBindingElement;
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000707FF File Offset: 0x0006E9FF
		public override MessageEncoderFactory CreateMessageEncoderFactory()
		{
			return new SignatureEncoderFactory(this.m_innerBindingElement.CreateMessageEncoderFactory());
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x00070811 File Offset: 0x0006EA11
		// (set) Token: 0x060024BB RID: 9403 RVA: 0x0007081E File Offset: 0x0006EA1E
		public override MessageVersion MessageVersion
		{
			get
			{
				return this.m_innerBindingElement.MessageVersion;
			}
			set
			{
				this.m_innerBindingElement.MessageVersion = value;
			}
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x0007082C File Offset: 0x0006EA2C
		public override BindingElement Clone()
		{
			return new SignatureEncodingBindingElement(this.m_innerBindingElement);
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x00070839 File Offset: 0x0006EA39
		public override T GetProperty<T>(BindingContext context)
		{
			if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
			{
				return this.m_innerBindingElement.GetProperty<T>(context);
			}
			return base.GetProperty<T>(context);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x00070865 File Offset: 0x0006EA65
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			context.BindingParameters.Add(this);
			return context.BuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x00070887 File Offset: 0x0006EA87
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			context.BindingParameters.Add(this);
			return context.BuildInnerChannelListener<TChannel>();
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000708A9 File Offset: 0x0006EAA9
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			context.BindingParameters.Add(this);
			return context.CanBuildInnerChannelListener<TChannel>();
		}

		// Token: 0x0400167B RID: 5755
		private MessageEncodingBindingElement m_innerBindingElement;
	}
}
