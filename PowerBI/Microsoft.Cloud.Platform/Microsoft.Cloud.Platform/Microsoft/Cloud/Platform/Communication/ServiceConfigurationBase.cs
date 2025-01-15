using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004B3 RID: 1203
	public abstract class ServiceConfigurationBase : IServiceConfiguration
	{
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x00083A10 File Offset: 0x00081C10
		// (set) Token: 0x060024DA RID: 9434 RVA: 0x00083A18 File Offset: 0x00081C18
		public string ServiceName { get; private set; }

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060024DB RID: 9435 RVA: 0x00083A21 File Offset: 0x00081C21
		// (set) Token: 0x060024DC RID: 9436 RVA: 0x00083A29 File Offset: 0x00081C29
		public IEnumerable<Type> KnownTypes { get; private set; }

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060024DD RID: 9437 RVA: 0x00083A32 File Offset: 0x00081C32
		// (set) Token: 0x060024DE RID: 9438 RVA: 0x00083A3A File Offset: 0x00081C3A
		public IEnumerable<Type> KnownExceptions { get; private set; }

		// Token: 0x060024DF RID: 9439 RVA: 0x00083A43 File Offset: 0x00081C43
		protected ServiceConfigurationBase(string serviceName)
			: this(serviceName, new List<Type>(), new List<Type>())
		{
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x00083A56 File Offset: 0x00081C56
		protected ServiceConfigurationBase(string serviceName, IEnumerable<Type> knownTypes, IEnumerable<Type> knownExceptions)
		{
			this.ServiceName = serviceName;
			this.KnownTypes = knownTypes;
			this.KnownExceptions = knownExceptions;
		}
	}
}
