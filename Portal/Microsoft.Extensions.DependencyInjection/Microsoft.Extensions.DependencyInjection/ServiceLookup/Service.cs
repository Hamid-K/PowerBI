using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x0200001A RID: 26
	internal class Service : IService
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00003716 File Offset: 0x00001916
		public Service(ServiceDescriptor descriptor)
		{
			this._descriptor = descriptor;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003725 File Offset: 0x00001925
		// (set) Token: 0x060000AB RID: 171 RVA: 0x0000372D File Offset: 0x0000192D
		public IService Next { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003736 File Offset: 0x00001936
		public ServiceLifetime Lifetime
		{
			get
			{
				return this._descriptor.Lifetime;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003744 File Offset: 0x00001944
		public IServiceCallSite CreateCallSite(ServiceProvider provider, ISet<Type> callSiteChain)
		{
			ConstructorInfo[] array = this._descriptor.ImplementationType.GetTypeInfo().DeclaredConstructors.Where((ConstructorInfo constructor) => constructor.IsPublic).ToArray<ConstructorInfo>();
			IServiceCallSite[] array2 = null;
			if (array.Length == 0)
			{
				throw new InvalidOperationException(Resources.FormatNoConstructorMatch(this._descriptor.ImplementationType));
			}
			if (array.Length == 1)
			{
				ConstructorInfo constructorInfo = array[0];
				ParameterInfo[] parameters = constructorInfo.GetParameters();
				if (parameters.Length == 0)
				{
					return new CreateInstanceCallSite(this._descriptor);
				}
				array2 = this.PopulateCallSites(provider, callSiteChain, parameters, true);
				return new ConstructorCallSite(constructorInfo, array2);
			}
			else
			{
				Array.Sort<ConstructorInfo>(array, (ConstructorInfo a, ConstructorInfo b) => b.GetParameters().Length.CompareTo(a.GetParameters().Length));
				ConstructorInfo constructorInfo2 = null;
				HashSet<Type> hashSet = null;
				for (int i = 0; i < array.Length; i++)
				{
					ParameterInfo[] parameters2 = array[i].GetParameters();
					IServiceCallSite[] array3 = this.PopulateCallSites(provider, callSiteChain, parameters2, false);
					if (array3 != null)
					{
						if (constructorInfo2 == null)
						{
							constructorInfo2 = array[i];
							array2 = array3;
						}
						else
						{
							if (hashSet == null)
							{
								hashSet = new HashSet<Type>(from p in constructorInfo2.GetParameters()
									select p.ParameterType);
							}
							if (!hashSet.IsSupersetOf(parameters2.Select((ParameterInfo p) => p.ParameterType)))
							{
								throw new InvalidOperationException(string.Join(Environment.NewLine, new object[]
								{
									Resources.FormatAmbigiousConstructorException(this._descriptor.ImplementationType),
									constructorInfo2,
									array[i]
								}));
							}
						}
					}
				}
				if (constructorInfo2 == null)
				{
					throw new InvalidOperationException(Resources.FormatUnableToActivateTypeException(this._descriptor.ImplementationType));
				}
				if (array2.Length != 0)
				{
					return new ConstructorCallSite(constructorInfo2, array2);
				}
				return new CreateInstanceCallSite(this._descriptor);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003923 File Offset: 0x00001B23
		public Type ServiceType
		{
			get
			{
				return this._descriptor.ServiceType;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003930 File Offset: 0x00001B30
		private bool IsSuperset(IEnumerable<Type> left, IEnumerable<Type> right)
		{
			return new HashSet<Type>(left).IsSupersetOf(right);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003940 File Offset: 0x00001B40
		private IServiceCallSite[] PopulateCallSites(ServiceProvider provider, ISet<Type> callSiteChain, ParameterInfo[] parameters, bool throwIfCallSiteNotFound)
		{
			IServiceCallSite[] array = new IServiceCallSite[parameters.Length];
			int i = 0;
			while (i < parameters.Length)
			{
				IServiceCallSite serviceCallSite = provider.GetServiceCallSite(parameters[i].ParameterType, callSiteChain);
				if (serviceCallSite == null && parameters[i].HasDefaultValue)
				{
					serviceCallSite = new ConstantCallSite(parameters[i].DefaultValue);
				}
				if (serviceCallSite == null)
				{
					if (throwIfCallSiteNotFound)
					{
						throw new InvalidOperationException(Resources.FormatCannotResolveService(parameters[i].ParameterType, this._descriptor.ImplementationType));
					}
					return null;
				}
				else
				{
					array[i] = serviceCallSite;
					i++;
				}
			}
			return array;
		}

		// Token: 0x0400002C RID: 44
		private readonly ServiceDescriptor _descriptor;
	}
}
