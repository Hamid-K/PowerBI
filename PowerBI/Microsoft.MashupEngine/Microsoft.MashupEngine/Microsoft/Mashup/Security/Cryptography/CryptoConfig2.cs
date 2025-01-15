using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Mashup.Security.Cryptography.Properties;

namespace Microsoft.Mashup.Security.Cryptography
{
	// Token: 0x02001FF4 RID: 8180
	public static class CryptoConfig2
	{
		// Token: 0x17003043 RID: 12355
		// (get) Token: 0x0600C75F RID: 51039 RVA: 0x0027B018 File Offset: 0x00279218
		private static Dictionary<string, Type> DefaultAlgorithmMap
		{
			get
			{
				Dictionary<string, Type> dictionary = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
				CryptoConfig2.AddAlgorithmToMap(dictionary, typeof(SHA256Cng), new string[0]);
				CryptoConfig2.AddAlgorithmToMap(dictionary, typeof(SHA256CryptoServiceProvider), new string[0]);
				CryptoConfig2.AddAlgorithmToMap(dictionary, typeof(HMACSHA256Cng), new string[0]);
				return dictionary;
			}
		}

		// Token: 0x0600C760 RID: 51040 RVA: 0x0027B074 File Offset: 0x00279274
		[SecurityCritical]
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		public static void AddAlgorithm(Type algorithm, params string[] aliases)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (aliases == null)
			{
				throw new ArgumentNullException("aliases");
			}
			CryptoConfig2.s_algorithmMapLock.EnterWriteLock();
			try
			{
				foreach (string text in aliases)
				{
					if (string.IsNullOrEmpty(text))
					{
						throw new InvalidOperationException(Resources.EmptyCryptoConfigAlias);
					}
					if (CryptoConfig2.s_algorithmMap.ContainsKey(text))
					{
						throw new InvalidOperationException(string.Format(Resources.Culture, Resources.DuplicateCryptoConfigAlias, new object[] { text }));
					}
				}
				CryptoConfig2.AddAlgorithmToMap(CryptoConfig2.s_algorithmMap, algorithm, aliases);
			}
			finally
			{
				CryptoConfig2.s_algorithmMapLock.ExitWriteLock();
			}
		}

		// Token: 0x0600C761 RID: 51041 RVA: 0x0027B124 File Offset: 0x00279324
		private static void AddAlgorithmToMap(Dictionary<string, Type> map, Type algorithm, params string[] aliases)
		{
			foreach (string text in aliases)
			{
				map.Add(text, algorithm);
			}
			if (!map.ContainsKey(algorithm.Name))
			{
				map.Add(algorithm.Name, algorithm);
			}
			if (!map.ContainsKey(algorithm.FullName))
			{
				map.Add(algorithm.FullName, algorithm);
			}
		}

		// Token: 0x0600C762 RID: 51042 RVA: 0x0027B184 File Offset: 0x00279384
		public static Func<object> CreateFactoryFromName(string name)
		{
			object obj = CryptoConfig2.CreateFromName(name);
			if (obj == null)
			{
				return null;
			}
			Type type = obj.GetType();
			IDisposable disposable = obj as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			return Expression.Lambda<Func<object>>(Expression.New(type), new ParameterExpression[0]).Compile() as Func<object>;
		}

		// Token: 0x0600C763 RID: 51043 RVA: 0x0027B1D0 File Offset: 0x002793D0
		public static object CreateFromName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = CryptoConfig.CreateFromName(name);
			if (obj != null)
			{
				return obj;
			}
			CryptoConfig2.s_algorithmMapLock.EnterReadLock();
			try
			{
				Type type = null;
				if (CryptoConfig2.s_algorithmMap.TryGetValue(name, out type))
				{
					return Activator.CreateInstance(type);
				}
			}
			finally
			{
				CryptoConfig2.s_algorithmMapLock.ExitReadLock();
			}
			return null;
		}

		// Token: 0x040065DB RID: 26075
		private static Dictionary<string, Type> s_algorithmMap = CryptoConfig2.DefaultAlgorithmMap;

		// Token: 0x040065DC RID: 26076
		private static ReaderWriterLockSlim s_algorithmMapLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
	}
}
