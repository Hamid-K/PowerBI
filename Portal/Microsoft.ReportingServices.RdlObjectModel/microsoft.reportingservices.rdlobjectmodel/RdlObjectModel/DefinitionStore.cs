using System;
using System.Threading;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001BD RID: 445
	internal class DefinitionStore<T, E>
	{
		// Token: 0x06000E95 RID: 3733 RVA: 0x00023C8C File Offset: 0x00021E8C
		public static IPropertyDefinition GetProperty(int index)
		{
			DefinitionStore<T, E>.m_lock.AcquireReaderLock(-1);
			IPropertyDefinition propertyDefinition;
			try
			{
				propertyDefinition = (IPropertyDefinition)DefinitionStore<T, E>.m_propertyDefinitions.GetObject(index);
				if (propertyDefinition != null)
				{
					return propertyDefinition;
				}
			}
			finally
			{
				DefinitionStore<T, E>.m_lock.ReleaseReaderLock();
			}
			propertyDefinition = PropertyDefinition.Create(typeof(T), Enum.GetName(typeof(E), index));
			DefinitionStore<T, E>.m_lock.AcquireWriterLock(-1);
			try
			{
				DefinitionStore<T, E>.m_propertyDefinitions.SetObject(index, propertyDefinition);
			}
			finally
			{
				DefinitionStore<T, E>.m_lock.ReleaseWriterLock();
			}
			return propertyDefinition;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00023D34 File Offset: 0x00021F34
		protected DefinitionStore()
		{
		}

		// Token: 0x04000545 RID: 1349
		private static readonly PropertyStore m_propertyDefinitions = new PropertyStore(null);

		// Token: 0x04000546 RID: 1350
		private static readonly ReaderWriterLock m_lock = new ReaderWriterLock();
	}
}
