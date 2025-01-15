using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer.Resources;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000018 RID: 24
	internal class SqlTypesAssemblyLoader
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000E5C9 File Offset: 0x0000C7C9
		public static SqlTypesAssemblyLoader DefaultInstance
		{
			get
			{
				return SqlTypesAssemblyLoader._instance;
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
		public SqlTypesAssemblyLoader(IEnumerable<string> assemblyNames = null)
		{
			if (assemblyNames != null)
			{
				this._preferredSqlTypesAssemblies = assemblyNames;
			}
			else
			{
				List<string> list = new List<string>
				{
					SqlTypesAssemblyLoader.GenerateSqlServerTypesAssemblyName(11),
					SqlTypesAssemblyLoader.GenerateSqlServerTypesAssemblyName(10)
				};
				for (int i = 20; i > 11; i--)
				{
					list.Add(SqlTypesAssemblyLoader.GenerateSqlServerTypesAssemblyName(i));
				}
				this._preferredSqlTypesAssemblies = list.ToList<string>();
			}
			this._latestVersion = new Lazy<SqlTypesAssembly>(new Func<SqlTypesAssembly>(this.BindToLatest), true);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000E64D File Offset: 0x0000C84D
		private static string GenerateSqlServerTypesAssemblyName(int version)
		{
			return string.Format(CultureInfo.InvariantCulture, "Microsoft.SqlServer.Types, Version={0}.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91", new object[] { version });
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000E670 File Offset: 0x0000C870
		public SqlTypesAssemblyLoader(SqlTypesAssembly assembly)
		{
			this._latestVersion = new Lazy<SqlTypesAssembly>(() => assembly, true);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000E6A8 File Offset: 0x0000C8A8
		public virtual SqlTypesAssembly TryGetSqlTypesAssembly()
		{
			return this._latestVersion.Value;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000E6B5 File Offset: 0x0000C8B5
		public virtual SqlTypesAssembly GetSqlTypesAssembly()
		{
			SqlTypesAssembly value = this._latestVersion.Value;
			if (value == null)
			{
				throw new InvalidOperationException(Strings.SqlProvider_SqlTypesAssemblyNotFound);
			}
			return value;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000E6D0 File Offset: 0x0000C8D0
		public virtual bool TryGetSqlTypesAssembly(Assembly assembly, out SqlTypesAssembly sqlAssembly)
		{
			if (this.IsKnownAssembly(assembly))
			{
				sqlAssembly = new SqlTypesAssembly(assembly);
				return true;
			}
			sqlAssembly = null;
			return false;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000E6EC File Offset: 0x0000C8EC
		private SqlTypesAssembly BindToLatest()
		{
			Assembly assembly = null;
			IEnumerable<string> enumerable;
			if (SqlProviderServices.SqlServerTypesAssemblyName == null)
			{
				enumerable = this._preferredSqlTypesAssemblies;
			}
			else
			{
				IEnumerable<string> enumerable2 = new string[] { SqlProviderServices.SqlServerTypesAssemblyName };
				enumerable = enumerable2;
			}
			foreach (string text in enumerable)
			{
				AssemblyName assemblyName = new AssemblyName(text);
				try
				{
					assembly = Assembly.Load(assemblyName);
					break;
				}
				catch (FileNotFoundException)
				{
				}
				catch (FileLoadException)
				{
				}
			}
			if (assembly != null)
			{
				return new SqlTypesAssembly(assembly);
			}
			return null;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000E78C File Offset: 0x0000C98C
		private bool IsKnownAssembly(Assembly assembly)
		{
			foreach (string text in this._preferredSqlTypesAssemblies)
			{
				if (SqlTypesAssemblyLoader.AssemblyNamesMatch(assembly.FullName, new AssemblyName(text)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000E7EC File Offset: 0x0000C9EC
		private static bool AssemblyNamesMatch(string infoRowProviderAssemblyName, AssemblyName targetAssemblyName)
		{
			if (string.IsNullOrWhiteSpace(infoRowProviderAssemblyName))
			{
				return false;
			}
			AssemblyName assemblyName;
			try
			{
				assemblyName = new AssemblyName(infoRowProviderAssemblyName);
			}
			catch (Exception)
			{
				return false;
			}
			if (!string.Equals(targetAssemblyName.Name, assemblyName.Name, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (targetAssemblyName.Version == null || assemblyName.Version == null)
			{
				return false;
			}
			if (targetAssemblyName.Version.Major != assemblyName.Version.Major || targetAssemblyName.Version.Minor != assemblyName.Version.Minor)
			{
				return false;
			}
			byte[] publicKeyToken = targetAssemblyName.GetPublicKeyToken();
			return publicKeyToken != null && publicKeyToken.SequenceEqual(assemblyName.GetPublicKeyToken());
		}

		// Token: 0x040000C3 RID: 195
		private const string AssemblyNameTemplate = "Microsoft.SqlServer.Types, Version={0}.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";

		// Token: 0x040000C4 RID: 196
		private static readonly SqlTypesAssemblyLoader _instance = new SqlTypesAssemblyLoader(null);

		// Token: 0x040000C5 RID: 197
		private readonly IEnumerable<string> _preferredSqlTypesAssemblies;

		// Token: 0x040000C6 RID: 198
		private readonly Lazy<SqlTypesAssembly> _latestVersion;
	}
}
