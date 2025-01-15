using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002DB RID: 731
	public class RdlSerializerSettings
	{
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x000343D1 File Offset: 0x000325D1
		// (set) Token: 0x06001677 RID: 5751 RVA: 0x000343D9 File Offset: 0x000325D9
		public ISerializerHost Host
		{
			get
			{
				return this.m_serializerHost;
			}
			set
			{
				this.m_serializerHost = value;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x000343E2 File Offset: 0x000325E2
		// (set) Token: 0x06001679 RID: 5753 RVA: 0x000343EA File Offset: 0x000325EA
		internal XmlAttributeOverrides XmlAttributeOverrides
		{
			get
			{
				return this.m_xmlOverrides;
			}
			set
			{
				this.m_xmlOverrides = value;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x000343F3 File Offset: 0x000325F3
		// (set) Token: 0x0600167B RID: 5755 RVA: 0x000343FB File Offset: 0x000325FB
		internal XmlSchema XmlSchema
		{
			get
			{
				return this.m_xmlSchema;
			}
			set
			{
				this.m_xmlSchema = value;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x00034404 File Offset: 0x00032604
		// (set) Token: 0x0600167D RID: 5757 RVA: 0x0003440C File Offset: 0x0003260C
		internal bool ValidateXml
		{
			get
			{
				return this.m_validate;
			}
			set
			{
				this.m_validate = value;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x00034415 File Offset: 0x00032615
		// (set) Token: 0x0600167F RID: 5759 RVA: 0x0003441D File Offset: 0x0003261D
		internal ValidationEventHandler XmlValidationEventHandler
		{
			get
			{
				return this.m_validationEventHandler;
			}
			set
			{
				this.m_validationEventHandler = value;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x00034426 File Offset: 0x00032626
		// (set) Token: 0x06001681 RID: 5761 RVA: 0x0003442E File Offset: 0x0003262E
		internal bool IgnoreWhitespace
		{
			get
			{
				return this.m_ignoreWhitespace;
			}
			set
			{
				this.m_ignoreWhitespace = value;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x00034437 File Offset: 0x00032637
		// (set) Token: 0x06001683 RID: 5763 RVA: 0x0003443F File Offset: 0x0003263F
		internal bool Normalize
		{
			get
			{
				return this.m_normalize;
			}
			set
			{
				this.m_normalize = value;
			}
		}

		// Token: 0x040006F6 RID: 1782
		private ISerializerHost m_serializerHost;

		// Token: 0x040006F7 RID: 1783
		private XmlAttributeOverrides m_xmlOverrides;

		// Token: 0x040006F8 RID: 1784
		private XmlSchema m_xmlSchema;

		// Token: 0x040006F9 RID: 1785
		private bool m_validate = true;

		// Token: 0x040006FA RID: 1786
		private ValidationEventHandler m_validationEventHandler;

		// Token: 0x040006FB RID: 1787
		private bool m_normalize = true;

		// Token: 0x040006FC RID: 1788
		private bool m_ignoreWhitespace;
	}
}
