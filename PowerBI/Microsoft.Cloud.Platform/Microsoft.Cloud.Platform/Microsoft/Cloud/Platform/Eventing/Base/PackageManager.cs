using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003D0 RID: 976
	public class PackageManager : Block, IPackageManager
	{
		// Token: 0x06001E22 RID: 7714 RVA: 0x00071C53 File Offset: 0x0006FE53
		public PackageManager()
			: this(typeof(PackageManager).Name)
		{
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x00071C6A File Offset: 0x0006FE6A
		public PackageManager(string name)
			: base(name)
		{
			this.m_packages = new Dictionary<Guid, IPackage>();
			this.m_eventLock = new object();
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x00071C8C File Offset: 0x0006FE8C
		protected override BlockInitializationStatus OnInitialize()
		{
			BlockInitializationStatus blockInitializationStatus = BlockInitializationStatus.PartiallyDone;
			if (base.OnInitialize() == BlockInitializationStatus.Done)
			{
				base.BlockHost.PublishService(this, typeof(IPackageManager), BlockServiceProviderIdentity.Implementation, this);
				blockInitializationStatus = BlockInitializationStatus.Done;
			}
			return blockInitializationStatus;
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x00071CBF File Offset: 0x0006FEBF
		protected override void OnShutdown()
		{
			this.m_packages = null;
			this.m_eventHandler = null;
			this.m_eventLock = null;
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06001E26 RID: 7718 RVA: 0x00071CD8 File Offset: 0x0006FED8
		// (remove) Token: 0x06001E27 RID: 7719 RVA: 0x00071D54 File Offset: 0x0006FF54
		public event EventHandler<PackageChangeEventArgs> PackagesChanged
		{
			add
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<EventHandler<PackageChangeEventArgs>>(value, "value");
				object eventLock = this.m_eventLock;
				lock (eventLock)
				{
					this.m_eventHandler = (EventHandler<PackageChangeEventArgs>)Delegate.Combine(this.m_eventHandler, value);
				}
				IList<IPackage> packages = this.GetPackages();
				if (packages.Count > 0)
				{
					value(this, new PackageChangeEventArgs(PackageAction.Added, packages));
				}
			}
			remove
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<EventHandler<PackageChangeEventArgs>>(value, "value");
				object eventLock = this.m_eventLock;
				lock (eventLock)
				{
					this.m_eventHandler = (EventHandler<PackageChangeEventArgs>)Delegate.Remove(this.m_eventHandler, value);
				}
			}
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x00071DB0 File Offset: 0x0006FFB0
		public IPackage TryRegister([NotNull] IPackage package)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IPackage>(package, "package");
			Dictionary<Guid, IPackage> packages = this.m_packages;
			lock (packages)
			{
				IPackage package2 = null;
				if (this.m_packages.TryGetValue(package.Metadata.Id, out package2))
				{
					return package2;
				}
				this.m_packages.Add(package.Metadata.Id, package);
			}
			this.OnPackageChange(new PackageChangeEventArgs(PackageAction.Added, new List<IPackage> { package }));
			return package;
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x00071E48 File Offset: 0x00070048
		public void Register([NotNull] IPackage package)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IPackage>(package, "package");
			Dictionary<Guid, IPackage> packages = this.m_packages;
			lock (packages)
			{
				if (this.m_packages.ContainsKey(package.Metadata.Id))
				{
					throw new PackageRegisteredException(package);
				}
				this.m_packages.Add(package.Metadata.Id, package);
			}
			this.OnPackageChange(new PackageChangeEventArgs(PackageAction.Added, new List<IPackage> { package }));
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x00071EDC File Offset: 0x000700DC
		public void Unregister([NotNull] IPackage package)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IPackage>(package, "package");
			Dictionary<Guid, IPackage> packages = this.m_packages;
			lock (packages)
			{
				if (!this.m_packages.ContainsKey(package.Metadata.Id))
				{
					throw new PackageNotFoundException(package.Metadata.Id);
				}
				this.m_packages.Remove(package.Metadata.Id);
			}
			this.OnPackageChange(new PackageChangeEventArgs(PackageAction.Removed, new List<IPackage> { package }));
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00071F7C File Offset: 0x0007017C
		public EventMetadata GetEventMetadata(EventIdentifier eid)
		{
			Dictionary<Guid, IPackage> packages = this.m_packages;
			lock (packages)
			{
				IPackage package = null;
				if (this.m_packages.TryGetValue(EventsKitIdentifiers.GetEventsKitId(eid.EventId), out package))
				{
					return package.GetEventMetadata(eid);
				}
			}
			throw new EventNotInPackageException(eid, null);
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x00071FE8 File Offset: 0x000701E8
		public PackageMetadata GetPackageMetadata(Guid packageId)
		{
			Dictionary<Guid, IPackage> packages = this.m_packages;
			lock (packages)
			{
				IPackage package = null;
				if (this.m_packages.TryGetValue(packageId, out package))
				{
					return package.Metadata;
				}
			}
			throw new PackageNotFoundException(packageId);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x00072048 File Offset: 0x00070248
		public IList<IPackage> GetPackages()
		{
			Dictionary<Guid, IPackage> packages = this.m_packages;
			IList<IPackage> list;
			lock (packages)
			{
				list = this.m_packages.Values.ToList<IPackage>();
			}
			return list;
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x00072094 File Offset: 0x00070294
		public IPackage GetPackage(EventIdentifier eid)
		{
			IPackage package;
			try
			{
				package = (from p in this.GetPackages()
					where p.Contains(eid)
					select p).First<IPackage>();
			}
			catch (InvalidOperationException)
			{
				throw new EventNotFoundException(eid);
			}
			return package;
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x000720EC File Offset: 0x000702EC
		private void OnPackageChange(PackageChangeEventArgs args)
		{
			EventHandler<PackageChangeEventArgs> eventHandler = this.m_eventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, args);
			}
		}

		// Token: 0x04000A5F RID: 2655
		private Dictionary<Guid, IPackage> m_packages;

		// Token: 0x04000A60 RID: 2656
		private EventHandler<PackageChangeEventArgs> m_eventHandler;

		// Token: 0x04000A61 RID: 2657
		private object m_eventLock;
	}
}
