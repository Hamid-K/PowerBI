using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000058 RID: 88
	internal abstract class ModelingXmlLoaderBase<T> : IXmlLoadable
	{
		// Token: 0x06000393 RID: 915 RVA: 0x0000C1D6 File Offset: 0x0000A3D6
		protected ModelingXmlLoaderBase(T item)
		{
			if (item == null)
			{
				throw new InternalModelingException("item is null");
			}
			this.m_item = item;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
		protected T Item
		{
			get
			{
				return this.m_item;
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000C200 File Offset: 0x0000A400
		public virtual bool LoadXmlAttribute(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000C203 File Offset: 0x0000A403
		public virtual bool LoadXmlElement(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x04000216 RID: 534
		private readonly T m_item;
	}
}
