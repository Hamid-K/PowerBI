using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;
using System.Security;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000485 RID: 1157
	internal class AspProxy
	{
		// Token: 0x06003973 RID: 14707 RVA: 0x000BD2E4 File Offset: 0x000BB4E4
		internal bool IsAspNetEnvironment()
		{
			if (!this.TryInitializeWebAssembly())
			{
				return false;
			}
			bool flag;
			try
			{
				flag = this.InternalMapWebPath("~") != null;
			}
			catch (SecurityException)
			{
				flag = false;
			}
			catch (Exception ex)
			{
				if (!ex.IsCatchableExceptionType())
				{
					throw;
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x000BD33C File Offset: 0x000BB53C
		public bool TryInitializeWebAssembly()
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
			if (!AspProxy.IsSystemWebLoaded())
			{
				return false;
			}
			try
			{
				this._webAssembly = Assembly.Load("System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				return this._webAssembly != null;
			}
			catch (Exception ex)
			{
				if (!ex.IsCatchableExceptionType())
				{
					throw;
				}
			}
			return false;
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x000BD3B4 File Offset: 0x000BB5B4
		public static bool IsSystemWebLoaded()
		{
			try
			{
				return AppDomain.CurrentDomain.GetAssemblies().Any((Assembly a) => a.GetName().Name == "System.Web" && a.GetName().GetPublicKeyToken() != null && a.GetName().GetPublicKeyToken().SequenceEqual(AspProxy._systemWebPublicKeyToken));
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x000BD408 File Offset: 0x000BB608
		private void InitializeWebAssembly()
		{
			if (!this.TryInitializeWebAssembly())
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext);
			}
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x000BD41D File Offset: 0x000BB61D
		internal string MapWebPath(string path)
		{
			path = this.InternalMapWebPath(path);
			if (path == null)
			{
				throw new InvalidOperationException(Strings.InvalidUseOfWebPath("~"));
			}
			return path;
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x000BD43C File Offset: 0x000BB63C
		internal string InternalMapWebPath(string path)
		{
			this.InitializeWebAssembly();
			string text;
			try
			{
				text = (string)this._webAssembly.GetType("System.Web.Hosting.HostingEnvironment", true).GetDeclaredMethod("MapPath", new Type[] { typeof(string) }).Invoke(null, new object[] { path });
			}
			catch (TargetException ex)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, ex);
			}
			catch (ArgumentException ex2)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, ex2);
			}
			catch (TargetInvocationException ex3)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, ex3);
			}
			catch (TargetParameterCountException ex4)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, ex4);
			}
			catch (MethodAccessException ex5)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, ex5);
			}
			catch (MemberAccessException ex6)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, ex6);
			}
			catch (TypeLoadException ex7)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, ex7);
			}
			return text;
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x000BD554 File Offset: 0x000BB754
		internal bool HasBuildManagerType()
		{
			Type type;
			return this.TryGetBuildManagerType(out type);
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x000BD569 File Offset: 0x000BB769
		private bool TryGetBuildManagerType(out Type buildManager)
		{
			this.InitializeWebAssembly();
			buildManager = this._webAssembly.GetType("System.Web.Compilation.BuildManager", false);
			return buildManager != null;
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x000BD58C File Offset: 0x000BB78C
		internal IEnumerable<Assembly> GetBuildManagerReferencedAssemblies()
		{
			MethodInfo referencedAssembliesMethod = this.GetReferencedAssembliesMethod();
			if (referencedAssembliesMethod == null)
			{
				return new List<Assembly>();
			}
			IEnumerable<Assembly> enumerable;
			try
			{
				ICollection collection = (ICollection)referencedAssembliesMethod.Invoke(null, null);
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
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, ex);
			}
			catch (TargetInvocationException ex2)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, ex2);
			}
			catch (MethodAccessException ex3)
			{
				throw new InvalidOperationException(Strings.UnableToDetermineApplicationContext, ex3);
			}
			return enumerable;
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x000BD628 File Offset: 0x000BB828
		internal MethodInfo GetReferencedAssembliesMethod()
		{
			Type type;
			if (!this.TryGetBuildManagerType(out type))
			{
				throw new InvalidOperationException(Strings.UnableToFindReflectedType("System.Web.Compilation.BuildManager", "System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"));
			}
			return type.GetDeclaredMethod("GetReferencedAssemblies", new Type[0]);
		}

		// Token: 0x04001309 RID: 4873
		private const string BUILD_MANAGER_TYPE_NAME = "System.Web.Compilation.BuildManager";

		// Token: 0x0400130A RID: 4874
		private const string AspNetAssemblyName = "System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400130B RID: 4875
		private static readonly byte[] _systemWebPublicKeyToken = ScalarType.ConvertToByteArray("b03f5f7f11d50a3a");

		// Token: 0x0400130C RID: 4876
		private Assembly _webAssembly;

		// Token: 0x0400130D RID: 4877
		private bool _triedLoadingWebAssembly;
	}
}
