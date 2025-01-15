using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001E5 RID: 485
	public abstract class ReportObjectBase : IContainedObject
	{
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x000262D4 File Offset: 0x000244D4
		[XmlIgnore]
		internal IPropertyStore PropertyStore
		{
			get
			{
				return this.m_propertyStore;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x000262DC File Offset: 0x000244DC
		// (set) Token: 0x06001012 RID: 4114 RVA: 0x000262E9 File Offset: 0x000244E9
		[XmlIgnore]
		public IContainedObject Parent
		{
			get
			{
				return this.m_propertyStore.Parent;
			}
			set
			{
				this.m_propertyStore.Parent = value;
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x000262F7 File Offset: 0x000244F7
		protected ReportObjectBase()
		{
			this.m_propertyStore = this.WrapPropertyStore(new PropertyStore((ReportObject)this));
			this.Initialize();
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0002631C File Offset: 0x0002451C
		internal ReportObjectBase(IPropertyStore propertyStore)
		{
			this.m_propertyStore = this.WrapPropertyStore(propertyStore);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00026331 File Offset: 0x00024531
		public virtual void Initialize()
		{
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00026333 File Offset: 0x00024533
		internal virtual IPropertyStore WrapPropertyStore(IPropertyStore propertyStore)
		{
			return propertyStore;
		}

		// Token: 0x0400056B RID: 1387
		private readonly IPropertyStore m_propertyStore;
	}
}
