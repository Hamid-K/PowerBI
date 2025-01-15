using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Owin;

namespace Microsoft.Owin.Hosting.ServerFactory
{
	// Token: 0x02000021 RID: 33
	public class ServerFactoryAdapter : IServerFactoryAdapter
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00003D99 File Offset: 0x00001F99
		public ServerFactoryAdapter(object serverFactory)
		{
			if (serverFactory == null)
			{
				throw new ArgumentNullException("serverFactory");
			}
			this._serverFactory = serverFactory;
			this._serverFactoryType = serverFactory.GetType();
			this._activator = null;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003DC9 File Offset: 0x00001FC9
		public ServerFactoryAdapter(Type serverFactoryType, IServerFactoryActivator activator)
		{
			if (serverFactoryType == null)
			{
				throw new ArgumentNullException("serverFactoryType");
			}
			if (activator == null)
			{
				throw new ArgumentNullException("activator");
			}
			this._serverFactoryType = serverFactoryType;
			this._activator = activator;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003E04 File Offset: 0x00002004
		public virtual void Initialize(IAppBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			MethodInfo initializeMethod = this._serverFactoryType.GetMethod("Initialize", new Type[] { typeof(IAppBuilder) });
			if (initializeMethod != null)
			{
				if (!initializeMethod.IsStatic && this._serverFactory == null)
				{
					this._serverFactory = this._activator.Activate(this._serverFactoryType);
				}
				initializeMethod.Invoke(this._serverFactory, new object[] { builder });
				return;
			}
			initializeMethod = this._serverFactoryType.GetMethod("Initialize", new Type[] { typeof(IDictionary<string, object>) });
			if (initializeMethod != null)
			{
				if (!initializeMethod.IsStatic && this._serverFactory == null)
				{
					this._serverFactory = this._activator.Activate(this._serverFactoryType);
				}
				initializeMethod.Invoke(this._serverFactory, new object[] { builder.Properties });
				return;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003EFC File Offset: 0x000020FC
		public virtual IDisposable Create(IAppBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			MethodInfo serverFactoryMethod = this._serverFactoryType.GetMethod("Create");
			if (serverFactoryMethod == null)
			{
				throw new MissingMethodException("ServerFactory", "Create");
			}
			ParameterInfo[] parameters = serverFactoryMethod.GetParameters();
			if (parameters.Length != 2)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_ServerFactoryParameterCount, new object[] { this._serverFactoryType }));
			}
			if (parameters[1].ParameterType != typeof(IDictionary<string, object>))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_ServerFactoryParameterType, new object[] { this._serverFactoryType }));
			}
			object app = builder.Build(parameters[0].ParameterType);
			if (!serverFactoryMethod.IsStatic && this._serverFactory == null)
			{
				this._serverFactory = this._activator.Activate(this._serverFactoryType);
			}
			return (IDisposable)serverFactoryMethod.Invoke(this._serverFactory, new object[] { app, builder.Properties });
		}

		// Token: 0x04000038 RID: 56
		private readonly IServerFactoryActivator _activator;

		// Token: 0x04000039 RID: 57
		private readonly Type _serverFactoryType;

		// Token: 0x0400003A RID: 58
		private object _serverFactory;
	}
}
