using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200006C RID: 108
	internal class AspProxy
	{
		// Token: 0x06000909 RID: 2313 RVA: 0x00014548 File Offset: 0x00012748
		internal bool IsAspNetEnvironment()
		{
			if (!this.TryInitializeWebAssembly())
			{
				return false;
			}
			bool flag;
			try
			{
				flag = this.PrivateMapWebPath("~") != null;
			}
			catch (Exception ex)
			{
				if (!EntityUtil.IsCatchableExceptionType(ex))
				{
					throw;
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00014590 File Offset: 0x00012790
		private bool TryInitializeWebAssembly()
		{
			if (this._webAssembly != null)
			{
				return true;
			}
			if (this._triedLoadingWebAssembly)
			{
				return false;
			}
			this._triedLoadingWebAssembly = true;
			try
			{
				this._webAssembly = Assembly.Load(AspProxy.SystemWeb);
				return this._webAssembly != null;
			}
			catch (Exception ex)
			{
				if (!EntityUtil.IsCatchableExceptionType(ex))
				{
					throw;
				}
			}
			return false;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000145FC File Offset: 0x000127FC
		private void InitializeWebAssembly()
		{
			if (!this.TryInitializeWebAssembly())
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext);
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00014611 File Offset: 0x00012811
		internal string MapWebPath(string path)
		{
			path = this.PrivateMapWebPath(path);
			if (path == null)
			{
				throw EntityUtil.InvalidOperation(Strings.InvalidUseOfWebPath("~"));
			}
			return path;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00014630 File Offset: 0x00012830
		private string PrivateMapWebPath(string path)
		{
			this.InitializeWebAssembly();
			string text;
			try
			{
				text = (string)this._webAssembly.GetType("System.Web.Hosting.HostingEnvironment", true).GetMethod("MapPath").Invoke(null, new object[] { path });
			}
			catch (TargetException ex)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, ex);
			}
			catch (ArgumentException ex2)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, ex2);
			}
			catch (TargetInvocationException ex3)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, ex3);
			}
			catch (TargetParameterCountException ex4)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, ex4);
			}
			catch (MethodAccessException ex5)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, ex5);
			}
			catch (MemberAccessException ex6)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, ex6);
			}
			catch (TypeLoadException ex7)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, ex7);
			}
			return text;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00014734 File Offset: 0x00012934
		internal bool HasBuildManagerType()
		{
			Type type;
			return this.TryGetBuildManagerType(out type);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00014749 File Offset: 0x00012949
		private bool TryGetBuildManagerType(out Type buildManager)
		{
			this.InitializeWebAssembly();
			buildManager = this._webAssembly.GetType("System.Web.Compilation.BuildManager", false);
			return buildManager != null;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0001476C File Offset: 0x0001296C
		internal IEnumerable<Assembly> GetBuildManagerReferencedAssemblies()
		{
			Type type;
			if (!this.TryGetBuildManagerType(out type))
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToFindReflectedType("System.Web.Compilation.BuildManager", AspProxy.SystemWeb));
			}
			MethodInfo method = type.GetMethod("GetReferencedAssemblies", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod);
			if (method == null)
			{
				return new List<Assembly>();
			}
			IEnumerable<Assembly> enumerable;
			try
			{
				ICollection collection = (ICollection)method.Invoke(null, null);
				if (collection == null)
				{
					enumerable = new List<Assembly>();
				}
				else
				{
					enumerable = collection.Cast<Assembly>();
				}
			}
			catch (TargetException ex)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, ex);
			}
			catch (TargetInvocationException ex2)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, ex2);
			}
			catch (MethodAccessException ex3)
			{
				throw EntityUtil.InvalidOperation(Strings.UnableToDetermineApplicationContext, ex3);
			}
			return enumerable;
		}

		// Token: 0x0400071A RID: 1818
		internal static string SystemWeb = "System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400071B RID: 1819
		private const string BUILD_MANAGER_TYPE_NAME = "System.Web.Compilation.BuildManager";

		// Token: 0x0400071C RID: 1820
		private Assembly _webAssembly;

		// Token: 0x0400071D RID: 1821
		private bool _triedLoadingWebAssembly;
	}
}
