using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002DA RID: 730
	public abstract class RdlReaderWriterBase
	{
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x0003435A File Offset: 0x0003255A
		public RdlSerializerSettings Settings
		{
			get
			{
				return this.m_settings;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x00034362 File Offset: 0x00032562
		protected ISerializerHost Host
		{
			get
			{
				return this.m_host;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x0003436A File Offset: 0x0003256A
		protected XmlAttributeOverrides XmlOverrides
		{
			get
			{
				return this.m_xmlOverrides;
			}
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00034372 File Offset: 0x00032572
		public RdlReaderWriterBase(RdlSerializerSettings settings)
		{
			this.m_settings = settings;
			if (this.m_settings != null)
			{
				this.m_host = this.m_settings.Host;
				this.m_xmlOverrides = this.m_settings.XmlAttributeOverrides;
			}
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x000343AB File Offset: 0x000325AB
		protected Type GetSerializationType(object obj)
		{
			return this.GetSerializationType(obj.GetType());
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x000343B9 File Offset: 0x000325B9
		protected Type GetSerializationType(Type type)
		{
			if (this.m_host != null)
			{
				return this.m_host.GetSubstituteType(type);
			}
			return type;
		}

		// Token: 0x040006F3 RID: 1779
		private readonly RdlSerializerSettings m_settings;

		// Token: 0x040006F4 RID: 1780
		private readonly ISerializerHost m_host;

		// Token: 0x040006F5 RID: 1781
		private readonly XmlAttributeOverrides m_xmlOverrides;
	}
}
