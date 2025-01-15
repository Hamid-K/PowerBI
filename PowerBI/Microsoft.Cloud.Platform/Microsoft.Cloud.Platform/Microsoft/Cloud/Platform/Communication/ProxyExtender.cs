using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004AE RID: 1198
	public class ProxyExtender
	{
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060024B4 RID: 9396 RVA: 0x0008362A File Offset: 0x0008182A
		// (set) Token: 0x060024B5 RID: 9397 RVA: 0x00083632 File Offset: 0x00081832
		public ChannelFactory ChannelFactory { get; private set; }

		// Token: 0x060024B6 RID: 9398 RVA: 0x0008363B File Offset: 0x0008183B
		public ProxyExtender([NotNull] ChannelFactory channelFactory)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ChannelFactory>(channelFactory, "channelFactory");
			this.ChannelFactory = channelFactory;
			this.m_knownExceptions = new List<Type>();
			this.m_lock = new object();
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060024B7 RID: 9399 RVA: 0x0008366C File Offset: 0x0008186C
		internal IEnumerable<Type> KnownExceptions
		{
			get
			{
				object @lock = this.m_lock;
				IEnumerable<Type> enumerable;
				lock (@lock)
				{
					enumerable = new ReadOnlyCollection<Type>(this.m_knownExceptions);
				}
				return enumerable;
			}
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000836B4 File Offset: 0x000818B4
		public void AddKnownExceptions([NotNull] IEnumerable<Type> exceptions)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<Type>>(exceptions, "exceptions");
			ExtendedDiagnostics.EnsureOperation(!exceptions.Any((Type e) => !typeof(Exception).IsAssignableFrom(e)), "List of 'excpetions' must contain objects inheriting from Excption.");
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_knownExceptions = this.m_knownExceptions.Union(exceptions).Distinct<Type>().ToList<Type>();
			}
		}

		// Token: 0x04000CED RID: 3309
		private List<Type> m_knownExceptions;

		// Token: 0x04000CEE RID: 3310
		private object m_lock;
	}
}
