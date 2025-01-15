using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace System.Data.Entity.Migrations.Utilities
{
	// Token: 0x020000A6 RID: 166
	internal class MigrationsConfigurationFinder
	{
		// Token: 0x06000EEC RID: 3820 RVA: 0x0001F86F File Offset: 0x0001DA6F
		public MigrationsConfigurationFinder()
		{
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0001F877 File Offset: 0x0001DA77
		public MigrationsConfigurationFinder(TypeFinder typeFinder)
		{
			this._typeFinder = typeFinder;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0001F888 File Offset: 0x0001DA88
		public virtual DbMigrationsConfiguration FindMigrationsConfiguration(Type contextType, string configurationTypeName, Func<string, Exception> noType = null, Func<string, IEnumerable<Type>, Exception> multipleTypes = null, Func<string, string, Exception> noTypeWithName = null, Func<string, string, Exception> multipleTypesWithName = null)
		{
			Type type = this._typeFinder.FindType((contextType == null) ? typeof(DbMigrationsConfiguration) : typeof(DbMigrationsConfiguration<>).MakeGenericType(new Type[] { contextType }), configurationTypeName, (IEnumerable<Type> types) => types.Where((Type t) => t.GetPublicConstructor(new Type[0]) != null && !t.IsAbstract() && !t.IsGenericType()).ToList<Type>(), noType, multipleTypes, noTypeWithName, multipleTypesWithName);
			DbMigrationsConfiguration dbMigrationsConfiguration2;
			try
			{
				DbMigrationsConfiguration dbMigrationsConfiguration;
				if (!(type == null))
				{
					dbMigrationsConfiguration = type.CreateInstance(new Func<string, string, string>(Strings.CreateInstance_BadMigrationsConfigurationType), (string s) => new MigrationsException(s));
				}
				else
				{
					dbMigrationsConfiguration = null;
				}
				dbMigrationsConfiguration2 = dbMigrationsConfiguration;
			}
			catch (TargetInvocationException ex)
			{
				ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				throw ex.InnerException;
			}
			return dbMigrationsConfiguration2;
		}

		// Token: 0x0400083B RID: 2107
		private readonly TypeFinder _typeFinder;
	}
}
