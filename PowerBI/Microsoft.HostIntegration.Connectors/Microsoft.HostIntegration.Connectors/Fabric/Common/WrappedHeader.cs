using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000439 RID: 1081
	internal class WrappedHeader : DictionaryHeader
	{
		// Token: 0x060025A0 RID: 9632 RVA: 0x00073519 File Offset: 0x00071719
		internal WrappedHeader(DictionaryHeader header, string actor)
			: this(header, actor, header.MustUnderstand, header.Relay)
		{
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x0007352F File Offset: 0x0007172F
		internal WrappedHeader(DictionaryHeader header, bool mustUnderstand, bool relay)
			: this(header, header.Actor, mustUnderstand, relay)
		{
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x00073540 File Offset: 0x00071740
		public WrappedHeader(DictionaryHeader header, string actor, bool mustUnderstand, bool relay)
		{
			this.m_header = header;
			this.m_actor = actor;
			this.m_mustUnderstand = mustUnderstand;
			this.m_relay = relay;
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x00073565 File Offset: 0x00071765
		public DictionaryHeader Header
		{
			get
			{
				return this.m_header;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060025A4 RID: 9636 RVA: 0x0007356D File Offset: 0x0007176D
		public override string Actor
		{
			get
			{
				return this.m_actor;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x00073575 File Offset: 0x00071775
		public override bool MustUnderstand
		{
			get
			{
				return this.m_mustUnderstand;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x0007357D File Offset: 0x0007177D
		public override bool Relay
		{
			get
			{
				return this.m_relay;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x00073585 File Offset: 0x00071785
		public override XmlDictionaryString DictionaryName
		{
			get
			{
				return this.m_header.DictionaryName;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060025A8 RID: 9640 RVA: 0x00073592 File Offset: 0x00071792
		public override XmlDictionaryString DictionaryNamespace
		{
			get
			{
				return this.m_header.DictionaryNamespace;
			}
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x0007359F File Offset: 0x0007179F
		public override bool IsMessageVersionSupported(MessageVersion messageVersion)
		{
			return this.m_header.IsMessageVersionSupported(messageVersion);
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x000735AD File Offset: 0x000717AD
		protected override void OnWriteAttributes(XmlDictionaryWriter writer)
		{
			this.m_header.WriteAttributes(writer);
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000735BB File Offset: 0x000717BB
		protected override void OnWriteContent(XmlDictionaryWriter writer)
		{
			this.m_header.WriteContent(writer);
		}

		// Token: 0x040016BF RID: 5823
		private DictionaryHeader m_header;

		// Token: 0x040016C0 RID: 5824
		private string m_actor;

		// Token: 0x040016C1 RID: 5825
		private bool m_mustUnderstand;

		// Token: 0x040016C2 RID: 5826
		private bool m_relay;
	}
}
