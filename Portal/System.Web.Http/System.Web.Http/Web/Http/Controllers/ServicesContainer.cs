using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Properties;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200010E RID: 270
	public abstract class ServicesContainer : IDisposable
	{
		// Token: 0x0600071D RID: 1821 RVA: 0x00011A74 File Offset: 0x0000FC74
		protected ServicesContainer()
		{
			this.ExceptionServicesLogger = new Lazy<IExceptionLogger>(new Func<IExceptionLogger>(this.CreateExceptionServicesLogger));
			this.ExceptionServicesHandler = new Lazy<IExceptionHandler>(new Func<IExceptionHandler>(this.CreateExceptionServicesHandler));
		}

		// Token: 0x0600071E RID: 1822
		public abstract object GetService(Type serviceType);

		// Token: 0x0600071F RID: 1823
		public abstract IEnumerable<object> GetServices(Type serviceType);

		// Token: 0x06000720 RID: 1824
		protected abstract List<object> GetServiceInstances(Type serviceType);

		// Token: 0x06000721 RID: 1825 RVA: 0x00005744 File Offset: 0x00003944
		protected virtual void ResetCache(Type serviceType)
		{
		}

		// Token: 0x06000722 RID: 1826
		public abstract bool IsSingleService(Type serviceType);

		// Token: 0x06000723 RID: 1827 RVA: 0x00011AAA File Offset: 0x0000FCAA
		public void Add(Type serviceType, object service)
		{
			this.Insert(serviceType, int.MaxValue, service);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00011AB9 File Offset: 0x0000FCB9
		public void AddRange(Type serviceType, IEnumerable<object> services)
		{
			this.InsertRange(serviceType, int.MaxValue, services);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00011AC8 File Offset: 0x0000FCC8
		public virtual void Clear(Type serviceType)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (this.IsSingleService(serviceType))
			{
				this.ClearSingle(serviceType);
			}
			else
			{
				this.ClearMultiple(serviceType);
			}
			this.ResetCache(serviceType);
		}

		// Token: 0x06000726 RID: 1830
		protected abstract void ClearSingle(Type serviceType);

		// Token: 0x06000727 RID: 1831 RVA: 0x00011AFE File Offset: 0x0000FCFE
		protected virtual void ClearMultiple(Type serviceType)
		{
			this.GetServiceInstances(serviceType).Clear();
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00011B0C File Offset: 0x0000FD0C
		public int FindIndex(Type serviceType, Predicate<object> match)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (match == null)
			{
				throw Error.ArgumentNull("match");
			}
			return this.GetServiceInstances(serviceType).FindIndex(match);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00011B40 File Offset: 0x0000FD40
		public void Insert(Type serviceType, int index, object service)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (service == null)
			{
				throw Error.ArgumentNull("service");
			}
			if (!serviceType.IsAssignableFrom(service.GetType()))
			{
				throw Error.Argument("service", SRResources.Common_TypeMustDriveFromType, new object[]
				{
					service.GetType().Name,
					serviceType.Name
				});
			}
			List<object> serviceInstances = this.GetServiceInstances(serviceType);
			if (index == 2147483647)
			{
				index = serviceInstances.Count;
			}
			serviceInstances.Insert(index, service);
			this.ResetCache(serviceType);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00011BD4 File Offset: 0x0000FDD4
		public void InsertRange(Type serviceType, int index, IEnumerable<object> services)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (services == null)
			{
				throw Error.ArgumentNull("services");
			}
			object[] array = services.Where((object svc) => svc != null).ToArray<object>();
			object obj = array.FirstOrDefault((object svc) => !serviceType.IsAssignableFrom(svc.GetType()));
			if (obj != null)
			{
				throw Error.Argument("services", SRResources.Common_TypeMustDriveFromType, new object[]
				{
					obj.GetType().Name,
					serviceType.Name
				});
			}
			List<object> serviceInstances = this.GetServiceInstances(serviceType);
			if (index == 2147483647)
			{
				index = serviceInstances.Count;
			}
			serviceInstances.InsertRange(index, array);
			this.ResetCache(serviceType);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00011CB9 File Offset: 0x0000FEB9
		public bool Remove(Type serviceType, object service)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (service == null)
			{
				throw Error.ArgumentNull("service");
			}
			bool flag = this.GetServiceInstances(serviceType).Remove(service);
			this.ResetCache(serviceType);
			return flag;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00011CF1 File Offset: 0x0000FEF1
		public int RemoveAll(Type serviceType, Predicate<object> match)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (match == null)
			{
				throw Error.ArgumentNull("match");
			}
			int num = this.GetServiceInstances(serviceType).RemoveAll(match);
			this.ResetCache(serviceType);
			return num;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00011D29 File Offset: 0x0000FF29
		public void RemoveAt(Type serviceType, int index)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			this.GetServiceInstances(serviceType).RemoveAt(index);
			this.ResetCache(serviceType);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00011D54 File Offset: 0x0000FF54
		public void Replace(Type serviceType, object service)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (service != null && !serviceType.IsAssignableFrom(service.GetType()))
			{
				throw Error.Argument("service", SRResources.Common_TypeMustDriveFromType, new object[]
				{
					service.GetType().Name,
					serviceType.Name
				});
			}
			if (this.IsSingleService(serviceType))
			{
				this.ReplaceSingle(serviceType, service);
			}
			else
			{
				this.ReplaceMultiple(serviceType, service);
			}
			this.ResetCache(serviceType);
		}

		// Token: 0x0600072F RID: 1839
		protected abstract void ReplaceSingle(Type serviceType, object service);

		// Token: 0x06000730 RID: 1840 RVA: 0x00011DD5 File Offset: 0x0000FFD5
		protected virtual void ReplaceMultiple(Type serviceType, object service)
		{
			this.RemoveAll(serviceType, (object _) => true);
			this.Insert(serviceType, 0, service);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00011E07 File Offset: 0x00010007
		public void ReplaceRange(Type serviceType, IEnumerable<object> services)
		{
			if (services == null)
			{
				throw Error.ArgumentNull("services");
			}
			this.RemoveAll(serviceType, (object _) => true);
			this.InsertRange(serviceType, 0, services);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00005744 File Offset: 0x00003944
		public virtual void Dispose()
		{
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00011E47 File Offset: 0x00010047
		private IExceptionLogger CreateExceptionServicesLogger()
		{
			return ExceptionServices.CreateLogger(this);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00011E4F File Offset: 0x0001004F
		private IExceptionHandler CreateExceptionServicesHandler()
		{
			return ExceptionServices.CreateHandler(this);
		}

		// Token: 0x040001D1 RID: 465
		internal readonly Lazy<IExceptionLogger> ExceptionServicesLogger;

		// Token: 0x040001D2 RID: 466
		internal readonly Lazy<IExceptionHandler> ExceptionServicesHandler;
	}
}
