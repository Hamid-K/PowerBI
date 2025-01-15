using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.RdlUpgrade
{
	// Token: 0x02000210 RID: 528
	internal abstract class SerializerHostBase : ISerializerHost
	{
		// Token: 0x060011C1 RID: 4545 RVA: 0x0002832F File Offset: 0x0002652F
		internal SerializerHostBase(bool serializing)
		{
			this.m_serializing = serializing;
		}

		// Token: 0x060011C2 RID: 4546
		public abstract Type GetSubstituteType(Type type);

		// Token: 0x060011C3 RID: 4547 RVA: 0x0002833E File Offset: 0x0002653E
		public virtual void OnDeserialization(object value)
		{
		}

		// Token: 0x060011C4 RID: 4548
		public abstract IEnumerable<ExtensionNamespace> GetExtensionNamespaces();

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00028340 File Offset: 0x00026540
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x00028348 File Offset: 0x00026548
		public string ExtraStringData
		{
			get
			{
				return this.m_extraStringData;
			}
			set
			{
				this.m_extraStringData = value;
			}
		}

		// Token: 0x040005B7 RID: 1463
		protected string m_extraStringData;

		// Token: 0x040005B8 RID: 1464
		protected bool m_serializing;
	}
}
